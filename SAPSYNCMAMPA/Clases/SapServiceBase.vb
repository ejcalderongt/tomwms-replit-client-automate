Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports Newtonsoft.Json

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
        client.DefaultRequestHeaders.ConnectionClose = True

        Return client
    End Function

End Class