
Imports System.Configuration
Imports System.Net.Http
Imports System.Reflection
Imports System.Text
Imports Newtonsoft.Json
Imports TOMWMS

Public Class ApiService

    Private Shared ReadOnly client As HttpClient = New HttpClient()
    Dim baseUrl As String = ConfigurationManager.AppSettings("ApiBaseUrl")
    Private EndPointProducto As String = "/api/Productos/sincronizar"
    Private EndPointIngreso As String = "/api/sync/ingresos/documento-ingreso"
    Private EndPointPedido As String = "/api/sync/salidas/documento-salida"

    'Dim urlProducto As String = GetFullUrl("/api/Productos/sincronizar")
    Dim urlProducto As String = GetFullUrl("/api/Productos/list/insert")
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
        Dim baseUrlCompleta As String = baseUrl & EndPointProducto

        Try

            If Not Uri.IsWellFormedUriString(urlProducto, UriKind.Absolute) Then
                Throw New Exception("URL malformada: " & urlProducto)
            End If

            clsHelper.LogMensaje(lblprg, "Enviando producto a la nube...", clsHelper.TipoMensaje.Info)

            Using cliente As New HttpClient()
                Dim content As New StringContent(pJsonOC, Encoding.UTF8, "application/json")

                Try
                    Dim response = Await cliente.PostAsync(baseUrlCompleta, content)
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

    'Public Async Function EnviarProductoAsync(fragmentos As List(Of String), lblprg As RichTextBox) As Task(Of String)

    '    Try

    '        Dim Respuesta As String = ""
    '        baseUrl = baseUrl & EndPointProducto

    '        clsHelper.LogMensaje(lblprg, "Enviando productos a la nube...", clsHelper.TipoMensaje.Info)

    '        Dim lote As Integer = 0

    '        Using cliente As New HttpClient()

    '            Dim fragmentosAEnviar As IEnumerable(Of String) = fragmentos

    '            If fragmentos.Count = 1 Then
    '                fragmentosAEnviar = {fragmentos.First()}
    '            End If

    '            For Each fragmento As String In fragmentosAEnviar
    '                lote += 1

    '                Dim content As New StringContent(fragmento, Encoding.UTF8, "application/json")

    '                Try
    '                    Dim response = Await cliente.PostAsync(baseUrl, content)
    '                    Dim responseBody = Await response.Content.ReadAsStringAsync()

    '                    If Not response.IsSuccessStatusCode Then
    '                        Respuesta &= $"Error {CInt(response.StatusCode)}: {responseBody} - {response.ReasonPhrase}" & vbNewLine
    '                        clsHelper.LogMensaje(lblprg, "Envio incorrecto de lote: " & lote, clsHelper.TipoMensaje.Error_)
    '                    Else
    '                        clsHelper.LogMensaje(lblprg, "Envio correcto de lote: " & lote, clsHelper.TipoMensaje.Exito)
    '                    End If

    '                Catch ex As Exception
    '                    Respuesta &= $"Excepción en lote {lote}: {ex.Message}" & vbNewLine
    '                    clsHelper.LogMensaje(lblprg, "Error inesperado en lote: " & lote, clsHelper.TipoMensaje.Error_)
    '                End Try

    '            Next

    '        End Using

    '        If String.IsNullOrWhiteSpace(Respuesta) Then
    '            Respuesta = "Proceso finalizado correctamente. " & Now
    '        End If

    '        Return Respuesta

    '    Catch ex As HttpRequestException
    '        ' Manejo específico para errores HTTP
    '        Throw New Exception($"EnviarProducto - HttpRequestException: {ex.Message}")
    '    Catch ex As TaskCanceledException
    '        Throw New Exception("EnviarProducto - Timeout al llamar a la API.")
    '    Catch ex As Exception
    '        Throw New Exception($"EnviarProducto - Excepción general: {ex.Message}")
    '    End Try


    'End Function

    'Public Async Function EnviarIngresoAsync(fragmentos As List(Of String), lblprg As RichTextBox) As Task(Of String)

    '    Try

    '        Dim respuesta As String = ""
    '        baseUrl = baseUrl & EndPointIngreso

    '        clsHelper.LogMensaje(lblprg, "Enviando ingresos a la nube...", clsHelper.TipoMensaje.Info)

    '        Dim lote As Integer = 0

    '        Using cliente As New HttpClient()

    '            Dim fragmentosAEnviar As IEnumerable(Of String) = fragmentos

    '            If fragmentos.Count = 1 Then
    '                fragmentosAEnviar = {fragmentos.First()}
    '            End If

    '            For Each fragmento As String In fragmentosAEnviar
    '                lote += 1

    '                Dim content As New StringContent(fragmento, Encoding.UTF8, "application/json")

    '                Try
    '                    Dim response = Await cliente.PostAsync(baseUrl, content)
    '                    Dim responseBody = Await response.Content.ReadAsStringAsync()

    '                    If Not response.IsSuccessStatusCode Then
    '                        respuesta &= $"Error {CInt(response.StatusCode)}: {responseBody} - {response.ReasonPhrase}" & vbNewLine
    '                        clsHelper.LogMensaje(lblprg, "Envio incorrecto de lote: " & lote, clsHelper.TipoMensaje.Error_)
    '                    Else
    '                        clsHelper.LogMensaje(lblprg, "Envio correcto de lote: " & lote, clsHelper.TipoMensaje.Exito)
    '                    End If

    '                Catch ex As Exception
    '                    respuesta &= $"Excepción en lote {lote}: {ex.Message}" & vbNewLine
    '                    clsHelper.LogMensaje(lblprg, "Error inesperado al enviar lote: " & lote, clsHelper.TipoMensaje.Error_)
    '                End Try

    '            Next

    '        End Using

    '        If String.IsNullOrWhiteSpace(respuesta) Then
    '            respuesta = "Proceso finalizado correctamente. " & Now
    '        End If


    '        Return respuesta

    '    Catch ex As HttpRequestException
    '        ' Manejo específico para errores HTTP
    '        Throw New Exception($"EnviarIngresoAsync - HttpRequestException: {ex.Message}")
    '    Catch ex As TaskCanceledException
    '        Throw New Exception("EnviarIngresoAsync - Timeout al llamar a la API.")
    '    Catch ex As Exception
    '        Throw New Exception($"EnviarIngresoAsync - Excepción general: {ex.Message}")
    '    End Try

    'End Function


    Public Async Function EnviarPedidosAsync(fragmentos As List(Of String), lblprg As RichTextBox) As Task(Of String)

        Try

            Dim Respuesta As String = ""
            baseUrl = baseUrl & EndPointPedido

            clsHelper.LogMensaje(lblprg, "Enviando pedidos a la nube...", clsHelper.TipoMensaje.Info)

            Dim lote As Integer = 0

            Using cliente As New HttpClient()

                Dim fragmentosAEnviar As IEnumerable(Of String) = fragmentos

                If fragmentos.Count = 1 Then
                    fragmentosAEnviar = {fragmentos.First()}
                End If

                For Each fragmento As String In fragmentosAEnviar
                    lote += 1

                    Dim content As New StringContent(fragmento, Encoding.UTF8, "application/json")

                    Try
                        Dim response = Await cliente.PostAsync(baseUrl, content)
                        Dim responseBody = Await response.Content.ReadAsStringAsync()

                        If Not response.IsSuccessStatusCode Then
                            Respuesta &= $"Error {CInt(response.StatusCode)}: {responseBody} - {response.ReasonPhrase}" & vbNewLine
                            clsHelper.LogMensaje(lblprg, "Envio incorrecto de lote: " & lote, clsHelper.TipoMensaje.Error_)
                        Else
                            clsHelper.LogMensaje(lblprg, "Envio correcto de lote: " & lote, clsHelper.TipoMensaje.Exito)
                        End If

                    Catch ex As Exception
                        Respuesta &= $"Excepción en lote {lote}: {ex.Message}" & vbNewLine
                        clsHelper.LogMensaje(lblprg, "Error inesperado al enviar lote: " & lote, clsHelper.TipoMensaje.Error_)
                    End Try

                Next

            End Using

            If String.IsNullOrWhiteSpace(Respuesta) Then
                Respuesta = "Proceso finalizado correctamente. " & Now
            End If


            Return Respuesta

        Catch ex As HttpRequestException
            ' Manejo específico para errores HTTP
            Throw New Exception($"EnviarProducto - HttpRequestException: {ex.Message}")
        Catch ex As TaskCanceledException
            Throw New Exception("EnviarProducto - Timeout al llamar a la API.")
        Catch ex As Exception
            Throw New Exception($"EnviarProducto - Excepción general: {ex.Message}")
        End Try

        Return respuesta
    End Function


    Public Async Function EnviarJsonOCAsync(pJsonOC As String, lblprg As RichTextBox) As Task(Of String)
        Dim respuesta As String = ""
        Dim baseUrlCompleta As String = baseUrl & EndPointIngreso

        Try
            clsHelper.LogMensaje(lblprg, "Enviando ingreso a la nube...", clsHelper.TipoMensaje.Info)

            Using cliente As New HttpClient()
                Dim content As New StringContent(pJsonOC, Encoding.UTF8, "application/json")

                Try
                    Dim response = Await cliente.PostAsync(baseUrlCompleta, content)
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


    'Public Async Function EnviarJsonOCAsync(pJsonOC As String, lblprg As RichTextBox) As Task(Of String)
    '    Dim respuesta As String = ""
    '    Try

    '        baseUrl = baseUrl & EndPointIngreso

    '        clsHelper.LogMensaje(lblprg, "Enviando ingreso a la nube...", clsHelper.TipoMensaje.Info)

    '        Using cliente As New HttpClient()

    '            Dim content As New StringContent(pJsonOC, Encoding.UTF8, "application/json")

    '            Try
    '                Dim response = Await cliente.PostAsync(baseUrl, content)
    '                Dim responseBody = Await response.Content.ReadAsStringAsync()

    '                If Not response.IsSuccessStatusCode Then
    '                    respuesta &= $"Error {CInt(response.StatusCode)}: {responseBody} - {response.ReasonPhrase}" & vbNewLine
    '                    clsHelper.LogMensaje(lblprg, "Envío incorrecto.", clsHelper.TipoMensaje.Error_)
    '                Else
    '                    clsHelper.LogMensaje(lblprg, "Envío correcto.", clsHelper.TipoMensaje.Exito)
    '                End If

    '            Catch ex As Exception
    '                respuesta &= $"Excepción en envío: {ex.Message}" & vbNewLine
    '                clsHelper.LogMensaje(lblprg, "Error inesperado al enviar.", clsHelper.TipoMensaje.Error_)
    '            End Try
    '        End Using

    '        If String.IsNullOrWhiteSpace(respuesta) Then
    '            respuesta = "Ok"
    '        End If

    '        Return respuesta

    '    Catch ex As HttpRequestException
    '        ' Manejo específico para errores HTTP
    '        Throw New Exception($"EnviarIngresoAsync - HttpRequestException: {ex.Message}")
    '    Catch ex As TaskCanceledException
    '        Throw New Exception("EnviarIngresoAsync - Timeout al llamar a la API.")
    '    Catch ex As Exception
    '        Throw New Exception($"EnviarIngresoAsync - Excepción general: {ex.Message}")
    '    End Try

    'End Function


End Class


