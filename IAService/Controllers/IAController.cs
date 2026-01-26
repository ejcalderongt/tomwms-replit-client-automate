using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace IAService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class IAController : ControllerBase
    {
        private readonly HttpClient _http;

        public IAController(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient();
            _http.Timeout = TimeSpan.FromMinutes(5);
        }

        // ------- NUEVO: Chat con messages[] + streaming SSE + RAG opcional ----------
        [HttpPost("chat")]
        public async Task Chat([FromBody] ChatRequest req)
        {
            Response.Headers.Append("Cache-Control", "no-store");

            // Normaliza: si solo vino Prompt, lo convertimos a messages[]
            if (req.Messages == null || req.Messages.Count == 0)
            {
                if (!string.IsNullOrWhiteSpace(req.Prompt))
                {
                    req.Messages = new List<ChatMsg> { new() { Role = "user", Content = req.Prompt } };
                }
                else
                {
                    Response.StatusCode = 400;
                    await Response.WriteAsJsonAsync(new { error = "Debe enviar messages[] o Prompt" });
                    return;
                }
            }

            // (Opcional) RAG: recupera contexto y lo antepone
            string? ragContext = null;
            if (req.Rag?.Enabled == true)
            {
                ragContext = await RetrieveContextAsync(req, HttpContext.RequestAborted); // implementar a tu gusto
                if (!string.IsNullOrWhiteSpace(ragContext))
                {
                    req.Messages.Insert(0, new ChatMsg
                    {
                        Role = "system",
                        Content = $"Usa EXCLUSIVAMENTE este contexto para responder y cita los IDs al final:\n{ragContext}"
                    });
                }
            }

            // Construye payload para Ollama /api/chat (recomendado para multi-turno)
            var ollamaPayload = new
            {
                model = string.IsNullOrWhiteSpace(req.Model) ? "deepseek-r1:8b" : req.Model,
                messages = req.Messages.Select(m => new { role = m.Role, content = m.Content }).ToArray(),
                stream = req.Stream,
                options = new
                {
                    temperature = req.Temperature ?? 0.3f,
                    num_predict = req.MaxTokens ?? 1024
                }
            };

            // Streaming SSE
            if (req.Stream)
            {
                Response.Headers.Append("Content-Type", "text/event-stream");
                using var httpReq = new HttpRequestMessage(HttpMethod.Post, "http://localhost:11434/api/chat")
                {
                    Content = JsonContent.Create(ollamaPayload, options: JsonOptions)
                };

                using var resp = await _http.SendAsync(httpReq, HttpCompletionOption.ResponseHeadersRead, HttpContext.RequestAborted);
                if (!resp.IsSuccessStatusCode)
                {
                    var err = await resp.Content.ReadAsStringAsync();
                    Response.StatusCode = (int)resp.StatusCode;
                    await WriteSse("error", new { message = err });
                    return;
                }

                await using var stream = await resp.Content.ReadAsStreamAsync(HttpContext.RequestAborted);
                using var reader = new StreamReader(stream);

                // Ollama streamea JSON línea por línea
                while (!reader.EndOfStream)
                {
                    HttpContext.RequestAborted.ThrowIfCancellationRequested();
                    var line = await reader.ReadLineAsync();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    try
                    {
                        using var doc = JsonDocument.Parse(line);
                        var root = doc.RootElement;

                        // 1) Delta de texto
                        if (root.TryGetProperty("message", out var msg) && msg.TryGetProperty("content", out var content))
                        {
                            var delta = content.GetString() ?? "";
                            if (!string.IsNullOrEmpty(delta))
                                await WriteSse("message", new { delta });
                        }

                        // 2) Evento done + usage cuando "done": true
                        if (root.TryGetProperty("done", out var doneProp) && doneProp.GetBoolean())
                        {
                            int? promptTokens = root.TryGetProperty("prompt_eval_count", out var p) ? p.GetInt32() : null;
                            int? completionTokens = root.TryGetProperty("eval_count", out var c) ? c.GetInt32() : null;

                            await WriteSse("done", new
                            {
                                usage = new { promptTokens, completionTokens }
                            });
                            break;
                        }
                    }
                    catch
                    {
                        // Si hay una línea no-JSON, la ignoramos para robustez
                    }
                }

                await Response.Body.FlushAsync();
                return;
            }

            // No streaming: devolvemos JSON compacto { content, usage, model }
            using (var resp = await _http.PostAsJsonAsync("http://localhost:11434/api/chat", ollamaPayload, JsonOptions, HttpContext.RequestAborted))
            {
                var json = await resp.Content.ReadAsStringAsync(HttpContext.RequestAborted);
                if (!resp.IsSuccessStatusCode)
                {
                    Response.StatusCode = (int)resp.StatusCode;
                    await Response.WriteAsync(json);
                    return;
                }

                try
                {
                    using var doc = JsonDocument.Parse(json);
                    var root = doc.RootElement;

                    string content = "";
                    if (root.TryGetProperty("message", out var msg) && msg.TryGetProperty("content", out var cnt))
                        content = cnt.GetString() ?? "";

                    var model = root.TryGetProperty("model", out var m) ? m.GetString() : null;
                    int? promptTokens = root.TryGetProperty("prompt_eval_count", out var p) ? p.GetInt32() : null;
                    int? completionTokens = root.TryGetProperty("eval_count", out var c) ? c.GetInt32() : null;

                    await Response.WriteAsJsonAsync(new
                    {
                        id = Guid.NewGuid().ToString("N"),
                        model,
                        content,
                        citations = (object?)null, // si implementas RAG, rellena aquí
                        usage = new { promptTokens, completionTokens },
                        finishReason = "stop"
                    });
                }
                catch
                {
                    // Fallback: retorna crudo
                    await Response.WriteAsJsonAsync(new { raw = json });
                }
            }
        }

        // ------- COMPATIBILIDAD: tu endpoint original para QA rápida -----------------
        [HttpPost("ollama")]
        public async Task<IActionResult> GenerateResponse([FromBody] LegacyQaRequest request)
        {
            var payload = new
            {
                model = "deepseek-r1:8b",
                prompt = request.Prompt,
                stream = false
            };

            var response = await _http.PostAsJsonAsync("http://localhost:11434/api/generate", payload, JsonOptions);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, error);
            }

            var json = await response.Content.ReadAsStringAsync();

            try
            {
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                string text = string.Empty;
                if (root.TryGetProperty("response", out var resp))
                    text = resp.GetString() ?? string.Empty;
                else if (root.TryGetProperty("thinking", out var think))
                    text = think.GetString() ?? string.Empty;

                string? model = root.TryGetProperty("model", out var m) ? m.GetString() : null;

                return Ok(new
                {
                    model,
                    response = text
                });
            }
            catch (JsonException)
            {
                return Ok(new { raw = json });
            }
        }

        // ------------------------ Helpers & modelos ---------------------------------

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        private async Task<string?> RetrieveContextAsync(ChatRequest req, CancellationToken ct)
        {
            // Stub de RAG: aquí conectas a tu vector DB (Qdrant/pgvector) y devuelves
            // un bloque de contexto con IDs, por ejemplo:
            // "[id=faq#42] Política de devoluciones ...\n[id=manual#15] Proceso ..."
            await Task.Yield();
            return null;
        }

        public sealed class ChatRequest
        {
            public string? Model { get; set; }
            public string? SessionId { get; set; }
            public List<ChatMsg>? Messages { get; set; }
            // Compatibilidad con tu modelo actual:
            public string? Prompt { get; set; }

            public RagOptions? Rag { get; set; } = new();
            public bool Stream { get; set; } = false;
            public float? Temperature { get; set; } = 0.3f;
            public int? MaxTokens { get; set; } = 1024;
            public Dictionary<string, string>? Metadata { get; set; }
        }

        public sealed class ChatMsg
        {
            public string Role { get; set; } = "user"; // "system" | "user" | "assistant"
            public string Content { get; set; } = "";
        }

        public sealed class RagOptions
        {
            public bool Enabled { get; set; } = false;
            public int TopK { get; set; } = 4;
            public string[] Collections { get; set; } = Array.Empty<string>();
        }

        public sealed class LegacyQaRequest
        {
            public string Prompt { get; set; } = string.Empty;
        }

        private async Task WriteSse(string @event, object payload)
        {
            await Response.WriteAsync($"event: {@event}\n");
            await Response.WriteAsync("data: " + JsonSerializer.Serialize(payload, JsonOptions) + "\n\n");
            await Response.Body.FlushAsync();
        }
    }


}
