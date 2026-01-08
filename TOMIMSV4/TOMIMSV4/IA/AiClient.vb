Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Threading.Tasks

Public Class AiClient

    Private ReadOnly _http As HttpClient
    Private Shared ReadOnly _json As New JsonSerializerOptions With {
        .PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    }

    Public Sub New(baseAddress As String)

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        ServicePointManager.ServerCertificateValidationCallback = Function(sender, cert, chain, sslPolicyErrors) True

        _http = New HttpClient() With {
            .BaseAddress = New Uri(baseAddress),
            .Timeout = TimeSpan.FromMinutes(2)
        }
    End Sub

    Public Async Function ChatAsync(messages As IEnumerable(Of ChatMsg),
                                   Optional ragEnabled As Boolean = False,
                                   Optional ct As Threading.CancellationToken = Nothing) As Task(Of String)

        Dim payload = New With {
            .messages = messages,
            .rag = If(ragEnabled, New With {.enabled = True, .topK = 4, .collections = New String() {"manuales"}}, Nothing),
            .stream = False,
            .temperature = 0.3,
            .maxTokens = 1024
        }

        Dim jsonBody As String = JsonSerializer.Serialize(payload, _json)
        Dim content As New StringContent(jsonBody, Encoding.UTF8, "application/json")

        'Try
        '    Using resp = Await _http.PostAsync("api/IA/chat", content, ct)
        '        resp.EnsureSuccessStatusCode()
        '        Dim json = Await resp.Content.ReadAsStringAsync()
        '        Return json
        '    End Using
        'Catch ex As HttpRequestException
        '    MessageBox.Show($"Error HTTP: {ex.Message}")
        'Catch ex As Exception
        '    MessageBox.Show($"Error general: {ex.Message}")
        'End Try

        Using resp = Await _http.PostAsync("api/IA/chat", content, ct)
            resp.EnsureSuccessStatusCode()

            Dim json = Await resp.Content.ReadAsStringAsync()
            Using doc = JsonDocument.Parse(json)
                Dim root = doc.RootElement

                Dim contentEl As JsonElement
                If root.TryGetProperty("content", contentEl) Then
                    Return contentEl.GetString()
                End If

                Return json
            End Using
        End Using
    End Function
End Class