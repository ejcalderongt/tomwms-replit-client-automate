Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public MustInherit Class SapServiceBase

    Protected ReadOnly baseUrl As String
    Protected ReadOnly sessionCookie As String

    Protected Sub New(baseUrl As String, sessionCookie As String)
        Me.baseUrl = baseUrl
        Me.sessionCookie = sessionCookie
    End Sub

    Protected Async Function GetJsonAsync(Of T)(relativeUrl As String) As Task(Of T)
        Using client As HttpClient = CreateClient()
            Dim response = Await client.GetAsync(relativeUrl).ConfigureAwait(False)
            Dim json = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)

            If response.IsSuccessStatusCode Then
                Return JsonConvert.DeserializeObject(Of T)(json)
            Else
                Throw New Exception($"GET {relativeUrl} - Status: {response.StatusCode} - {json}")
            End If
        End Using
    End Function

    Protected Async Function PostJsonAsync(Of TRequest, TResponse)(relativeUrl As String, body As TRequest) As Task(Of TResponse)
        Using client As HttpClient = CreateClient()
            Dim jsonContent = New StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
            Dim response = Await client.PostAsync(relativeUrl, jsonContent).ConfigureAwait(False)
            Dim json = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)

            If response.IsSuccessStatusCode Then
                Return JsonConvert.DeserializeObject(Of TResponse)(json)
            Else
                Throw New Exception($"POST {relativeUrl} - Status: {response.StatusCode} - {json}")
            End If
        End Using
    End Function

    Protected Async Function PatchJsonAsync(relativeUrl As String, body As Object) As Task(Of Boolean)
        Using client As HttpClient = CreateClient()
            Dim jsonContent = New StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
            Dim method = New HttpMethod("PATCH")
            Dim request = New HttpRequestMessage(method, relativeUrl) With {.Content = jsonContent}

            Dim response = Await client.SendAsync(request).ConfigureAwait(False)
            Dim result = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)

            If Not response.IsSuccessStatusCode Then
                Throw New Exception($"PATCH {relativeUrl} - Status: {response.StatusCode} - {result}")
            End If

            Return True
        End Using
    End Function

    Private Function CreateClient() As HttpClient
        Dim handler As New HttpClientHandler With {
            .AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate,
            .ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True,
            .UseCookies = False
        }

        Dim client As New HttpClient(handler)
        client.BaseAddress = New Uri(baseUrl)
        client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
        client.DefaultRequestHeaders.Add("Cookie", sessionCookie)

        Return client
    End Function

    Public Shared Function ObtenerTransacWmsPaginado(filtroFinal As String,
                                                 sessionCookie As String,
                                                 baseUrl As String,
                                                 lblprg As RichTextBox,
                                                 Optional mensajeProgreso As String = "Líneas obtenidas",
                                                 Optional pageSize As Integer = 20) As JArray

        Dim skip As Integer = 0
        Dim hayMas As Boolean = True
        Dim urlBase As String = baseUrl.TrimEnd("/"c) & "/"
        Dim allRows As New JArray()

            Using handler As New HttpClientHandler()
                handler.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, sslPolicyErrors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)
                    client.DefaultRequestHeaders.Add("Cookie", sessionCookie)
                    client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

                    While hayMas
                        Dim url As String =
                            $"{urlBase}TRANSAC_WMS?$filter={Uri.EscapeDataString(filtroFinal)}&$top={pageSize}&$skip={skip}"

                        Using request As New HttpRequestMessage(HttpMethod.Get, url)
                            Dim response = client.SendAsync(request).GetAwaiter().GetResult()

                        If Not response.IsSuccessStatusCode Then
                            Dim errorDetail = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
                            Throw New Exception($"Error al obtener TRANSAC_WMS. Código: {response.StatusCode}, Detalle: {errorDetail}")
                        End If

                        Dim json As String = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
                        Dim parsed As JObject = JObject.Parse(json)
                        Dim rows As JToken = parsed("value")

                        If rows Is Nothing OrElse Not rows.HasValues Then
                            hayMas = False
                            Exit While
                        End If

                        Dim filasPagina As Integer = rows.Count()

                        For Each row As JToken In rows
                            allRows.Add(row)
                        Next

                        If lblprg IsNot Nothing Then
                            clsPublic.Actualizar_Progreso(lblprg, $"{mensajeProgreso}: {allRows.Count}")
                        End If

                        skip += filasPagina
                    End Using
                End While
            End Using
        End Using

        Return allRows
    End Function

    Public Shared Function CrearJsonResponseDesdeRows(rows As JArray) As String
        Dim jsonFinal As New JObject(
            New JProperty("value", rows)
        )
        Return jsonFinal.ToString()
    End Function

End Class
