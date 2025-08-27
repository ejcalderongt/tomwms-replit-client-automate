
Imports System.Configuration
Imports System.Net.Http
Imports System.Reflection
Imports System.Text
Imports Newtonsoft.Json
Imports TOMWMS

Public Class ApiService

    Private Shared ReadOnly client As HttpClient = New HttpClient()
    'Dim urlProducto As String = GetFullUrl("/WMSWEB/api/Productos/sincronizar")
    'Dim urlIngreso As String = GetFullUrl("/WMSWEB/api/sync/ingresos/documento-ingreso")
    'Dim urlPedido As String = GetFullUrl("/WMSWEB/api/sync/salidas/documento-salida")

    Dim urlProducto As String = GetFullUrl("/api/Productos/sincronizar")
    Dim urlIngreso As String = GetFullUrl("/api/sync/ingresos/documento-ingreso")
    Dim urlPedido As String = GetFullUrl("/api/sync/salidas/documento-salida")


    ''' <summary>
    ''' Concatena la URL base del archivo de configuración con el endpoint, asegurando el formato correcto.
    ''' </summary>
    ''' <param name="relativeEndpoint">Endpoint relativo, debe comenzar con una barra (ej. "/api/...")</param>
    ''' <returns>URL completa bien formada</returns>
    Public Shared Function GetFullUrl(relativeEndpoint As String) As String
        Try

            'Dim baseUrl As String = ConfigurationManager.AppSettings("ApiBaseUrl")

            Dim baseUrl As String = ApiConfig.ObtenerApiBaseUrl()

            If String.IsNullOrWhiteSpace(baseUrl) Then
                Throw New Exception("No se ha definido 'ApiBaseUrl' en ApiConfig.json")
            End If

            If Not relativeEndpoint.StartsWith("/") Then
                relativeEndpoint = "/" & relativeEndpoint
            End If

            Dim fullUrl As String = baseUrl.TrimEnd("/"c) & relativeEndpoint

            If Not Uri.IsWellFormedUriString(fullUrl, UriKind.Absolute) Then
                Throw New UriFormatException("URL malformada: " & fullUrl)
            End If

            Return fullUrl

        Catch ex As Exception
            Throw New Exception("GetFullUrl Error: " & ex.Message)
        End Try
    End Function

    Public Async Function EnviarJsonProductoAsync(pJsonOC As String, lblprg As RichTextBox) As Task(Of String)
        Dim respuesta As String = ""
        Try

            If Not Uri.IsWellFormedUriString(urlProducto, UriKind.Absolute) Then
                Throw New Exception("URL malformada: " & urlProducto)
            End If

            clsHelper.LogMensaje(lblprg, "Enviando producto a la nube...", clsHelper.TipoMensaje.Info)

            Using cliente As New HttpClient()
                Dim content As New StringContent(pJsonOC, Encoding.UTF8, "application/json")

                Try
                    Dim response = Await cliente.PostAsync(urlProducto, content)
                    Dim responseBody = Await response.Content.ReadAsStringAsync()

                    If response.IsSuccessStatusCode Then
                        clsHelper.LogMensaje(lblprg, "Envío correcto.", clsHelper.TipoMensaje.Exito)
                        respuesta = "Ok"
                    Else
                        respuesta = $"Error HTTP {(CInt(response.StatusCode))}: {responseBody} ({response.ReasonPhrase})"
                        clsHelper.LogMensaje(lblprg, respuesta, clsHelper.TipoMensaje.Error_)
                    End If

                Catch ex As Exception
                    respuesta = $"Excepción en envío: {ex.Message}"
                    clsHelper.LogMensaje(lblprg, respuesta, clsHelper.TipoMensaje.Error_)
                End Try
            End Using

        Catch ex As TaskCanceledException
            respuesta = "Timeout al llamar a la API (TaskCanceledException)"
            clsHelper.LogMensaje(lblprg, respuesta, clsHelper.TipoMensaje.Error_)

        Catch ex As HttpRequestException
            respuesta = $"Error de red o DNS: {ex.Message}"
            clsHelper.LogMensaje(lblprg, respuesta, clsHelper.TipoMensaje.Error_)

        Catch ex As Exception
            respuesta = $"Excepción general: {ex.Message}"
            clsHelper.LogMensaje(lblprg, respuesta, clsHelper.TipoMensaje.Error_)
        End Try

        Return respuesta

    End Function

    Public Async Function EnviarJsonOCAsync(pJsonOC As String, lblprg As RichTextBox) As Task(Of String)
        Dim respuesta As String = ""
        Try

            If Not Uri.IsWellFormedUriString(urlIngreso, UriKind.Absolute) Then
                Throw New Exception("URL malformada: " & urlIngreso)
            End If

            clsHelper.LogMensaje(lblprg, "Enviando ingreso a la nube...", clsHelper.TipoMensaje.Info)

            Using cliente As New HttpClient()
                Dim content As New StringContent(pJsonOC, Encoding.UTF8, "application/json")

                Try
                    Dim response = Await cliente.PostAsync(urlIngreso, content)
                    Dim responseBody = Await response.Content.ReadAsStringAsync()

                    If response.IsSuccessStatusCode Then
                        clsHelper.LogMensaje(lblprg, "Envío correcto.", clsHelper.TipoMensaje.Exito)
                        respuesta = "Ok"
                    Else
                        respuesta = $"Error HTTP {(CInt(response.StatusCode))}: {responseBody} ({response.ReasonPhrase})"
                        clsHelper.LogMensaje(lblprg, respuesta, clsHelper.TipoMensaje.Error_)
                    End If

                Catch ex As Exception
                    respuesta = $"Excepción en envío: {ex.Message}"
                    clsHelper.LogMensaje(lblprg, respuesta, clsHelper.TipoMensaje.Error_)
                End Try
            End Using

        Catch ex As TaskCanceledException
            respuesta = "Timeout al llamar a la API (TaskCanceledException)"
            clsHelper.LogMensaje(lblprg, respuesta, clsHelper.TipoMensaje.Error_)

        Catch ex As HttpRequestException
            respuesta = $"Error de red o DNS: {ex.Message}"
            clsHelper.LogMensaje(lblprg, respuesta, clsHelper.TipoMensaje.Error_)

        Catch ex As Exception
            respuesta = $"Excepción general: {ex.Message}"
            clsHelper.LogMensaje(lblprg, respuesta, clsHelper.TipoMensaje.Error_)
        End Try

        Return respuesta
    End Function


    Public Async Function EnviarJsonPEAsync(pJsonOC As String, lblprg As RichTextBox) As Task(Of String)
        Dim respuesta As String = ""
        Try

            If Not Uri.IsWellFormedUriString(urlPedido, UriKind.Absolute) Then
                Throw New Exception("URL malformada: " & urlPedido)
            End If

            clsHelper.LogMensaje(lblprg, "Enviando pedido a la nube...", clsHelper.TipoMensaje.Info)

            Using cliente As New HttpClient()
                Dim content As New StringContent(pJsonOC, Encoding.UTF8, "application/json")

                Try
                    Dim response = Await cliente.PostAsync(urlPedido, content)
                    Dim responseBody = Await response.Content.ReadAsStringAsync()

                    If response.IsSuccessStatusCode Then
                        clsHelper.LogMensaje(lblprg, "Envío correcto.", clsHelper.TipoMensaje.Exito)
                        respuesta = "Ok"
                    Else
                        respuesta = $"Error HTTP {(CInt(response.StatusCode))}: {responseBody} ({response.ReasonPhrase})"
                        clsHelper.LogMensaje(lblprg, respuesta, clsHelper.TipoMensaje.Error_)
                    End If

                Catch ex As Exception
                    respuesta = $"Excepción en envío: {ex.Message}"
                    clsHelper.LogMensaje(lblprg, respuesta, clsHelper.TipoMensaje.Error_)
                End Try
            End Using

        Catch ex As TaskCanceledException
            respuesta = "Timeout al llamar a la API (TaskCanceledException)"
            clsHelper.LogMensaje(lblprg, respuesta, clsHelper.TipoMensaje.Error_)

        Catch ex As HttpRequestException
            respuesta = $"Error de red o DNS: {ex.Message}"
            clsHelper.LogMensaje(lblprg, respuesta, clsHelper.TipoMensaje.Error_)

        Catch ex As Exception
            respuesta = $"Excepción general: {ex.Message}"
            clsHelper.LogMensaje(lblprg, respuesta, clsHelper.TipoMensaje.Error_)
        End Try

        Return respuesta
    End Function



End Class


