Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports DevExpress.Drawing.Printing.Internal.DXPageSizeInfo
Imports DevExpress.Skins
Imports Newtonsoft.Json.Linq

Public Class clsSyncSapColor
    Implements IDisposable

    Private disposedValue As Boolean
    Private Shared vHanaService As SapServiceLayerClient

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
            ' TODO: set large fields to null
            disposedValue = True
        End If
    End Sub

    ' ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
    ' Protected Overrides Sub Finalize()
    '     ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub

    Public Shared Async Function Insertar_Color_From_Sap_HanaAsync(ByVal codigo As String,
                                                                   SessionCookie As String,
                                                                   baseUrl As String,
                                                                   Optional lbl As RichTextBox = Nothing) As Task(Of Integer)

        Dim Color As clsBeColor

        Try

            Using lConnection As New SqlConnection((BD.Instancia.CadenaConexionSQLClient))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Color = Await Get_Color_SAP_SL(SessionCookie, baseUrl, lbl, codigo, lConnection, lTransaction)

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            If Color IsNot Nothing Then
                Return Color.IdColor
            Else
                Return 0
            End If


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Async Function Insertar_Color_From_Sap_HanaAsync(ByVal codigo As String,
                                                                   SessionCookie As String,
                                                                   baseUrl As String,
                                                                   lConnection As SqlConnection,
                                                                   lTransaction As SqlTransaction,
                                                                   Optional lbl As RichTextBox = Nothing) As Task(Of Integer)

        Dim Color As clsBeColor

        Try

            Color = Await Get_Color_SAP_SL(SessionCookie, baseUrl, lbl, codigo, lConnection, lTransaction)

            If Color IsNot Nothing Then
                Return Color.IdColor
            Else
                Return 0
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Async Function Get_Color_SAP_SL(sessionCookie As String,
                                                    baseUrl As String,
                                                    lbl As RichTextBox,
                                                    codigo As String,
                                                    lConnection As SqlConnection,
                                                    lTransaction As SqlTransaction) As Task(Of clsBeColor)

        Dim BeColor As New clsBeColor

        Try

            Dim filtro As New Text.StringBuilder()
            filtro.Append("Code eq '{0}' ")

            Dim filterEncoded = Uri.EscapeDataString(filtro.ToString())

            Using handler As New HttpClientHandler()
                handler.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)
                    client.DefaultRequestHeaders.ConnectionClose = True
                    client.DefaultRequestHeaders.Add("Cookie", sessionCookie)
                    client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                    Dim requestUrl As String =
                        $"{baseUrl}U_COLOR?$filter={filterEncoded}"

                    Using request As New HttpRequestMessage(HttpMethod.Get, requestUrl)
                        request.Headers.ConnectionClose = True

                        Dim response As HttpResponseMessage = Await client.SendAsync(request).ConfigureAwait(False)

                        If Not response.IsSuccessStatusCode Then
                            Dim errContent = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                            Throw New Exception($"Error al obtener el color desde Service Layer. Código: {response.StatusCode}, Detalle: {errContent}")
                        End If

                        Dim jsonResponse = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                        Dim obj = JObject.Parse(jsonResponse)
                        Dim rows = obj("value")

                        For Each row In rows
                            BeColor = New clsBeColor With {
                                                        .IdColor = clsLnColor.MaxID(lConnection, lTransaction) + 1,
                                                        .Codigo = row("Code").ToString(),
                                                        .Nombre = row("Name").ToString(),
                                                        .IdPropietario = BeConfigEnc.IdPropietario,
                                                        .Fec_agr = Now,
                                                        .Fec_mod = Now,
                                                        .User_agr = BeConfigEnc.User_agr,
                                                        .User_mod = BeConfigEnc.User_mod,
                                                        .Activo = True
                                                         }
                            If Not clsLnColor.Existe_By_Codigo(BeColor.Codigo, lConnection, lTransaction) Then
                                clsLnColor.Insertar(BeColor, lConnection, lTransaction)
                                Exit For ' Si solo se espera un registro, salir después de insertar el primero encontrado
                            End If

                            clsPublic.Actualizar_Progreso(lbl, $"Procesando Color: {BeColor.Codigo} - {BeColor.Nombre}")
                        Next

                    End Using
                End Using

            End Using

            Return BeColor

        Catch ex As Exception
            Throw New Exception("Error en Get_Productos_SAP_SL: " & ex.Message, ex)
        End Try
    End Function

    Public Shared Async Function Get_Colores_SAP_SL(vBeConfigEnc As clsBeI_nav_config_enc,
                                                   lbl As RichTextBox) As Task(Of Boolean)

        Dim BeColor As New clsBeColor

        Try

            vHanaService = New SapServiceLayerClient()
            Dim loginResponse As LoginResponseDto = Await vHanaService.LoginAsync()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lbl, "No se pudo obtener sesión.")
                Return False
            Else
                clsPublic.Actualizar_Progreso(lbl, "Conexión correcta.")
            End If

            clsPublic.Actualizar_Progreso(lbl, "Consultando colores en SAP (U_COLOR).")

            Dim baseUrl As String = BD.Instancia.HANA_SL
            Dim pageSize As Integer = 100
            Dim skip As Integer = 0

            Using handler As New HttpClientHandler()
                handler.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)
                    client.DefaultRequestHeaders.ConnectionClose = True
                    client.DefaultRequestHeaders.Add("Cookie", vHanaService.SessionCookie)
                    client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

                    Using lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)

                        lConnection.Open()

                        Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                            Dim hayMas As Boolean = True

                            While hayMas

                                Dim requestUrl As String = $"{baseUrl}U_COLOR?$top={pageSize}&$skip={skip}"

                                Using request As New HttpRequestMessage(HttpMethod.Get, requestUrl)

                                    request.Headers.ConnectionClose = True

                                    Dim response As HttpResponseMessage = Await client.SendAsync(request).ConfigureAwait(False)

                                    If Not response.IsSuccessStatusCode Then
                                        Dim errContent = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                                        Throw New Exception($"Error al obtener los colores desde Service Layer. Código: {response.StatusCode}, Detalle: {errContent}")
                                    End If

                                    Dim jsonResponse = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                                    Dim obj = JObject.Parse(jsonResponse)
                                    Dim rows = obj("value")

                                    If rows Is Nothing OrElse Not rows.HasValues Then
                                        ' Ya no hay más páginas
                                        hayMas = False
                                        Exit While
                                    End If

                                    Dim filasPagina As Integer = rows.Count()

                                    For Each row In rows
                                        BeColor = New clsBeColor With {
                                                            .IdColor = clsLnColor.MaxID(lConnection, lTransaction) + 1,
                                                            .Codigo = row("Code").ToString(),
                                                            .Nombre = row("Name").ToString(),
                                                            .IdPropietario = vBeConfigEnc.IdPropietario,
                                                            .Fec_agr = Now,
                                                            .Fec_mod = Now,
                                                            .User_agr = vBeConfigEnc.User_agr,
                                                            .User_mod = vBeConfigEnc.User_mod,
                                                            .Activo = True
                                                             }
                                        If Not clsLnColor.Existe_By_Codigo(BeColor.Codigo, lConnection, lTransaction) Then
                                            clsLnColor.Insertar(BeColor, lConnection, lTransaction)
                                            clsPublic.Actualizar_Progreso(lbl, $"Procesando Colores: {BeColor.Codigo} - {BeColor.Nombre}")
                                        End If

                                    Next
                                    ' Avanzar al siguiente bloque
                                    skip += filasPagina

                                End Using

                            End While

                            lTransaction.Commit()

                        End Using

                        lConnection.Close()

                    End Using

                End Using

            End Using

            clsPublic.Actualizar_Progreso(lbl, "Fin de procesamiento de colores -> " & Now)

            Return True

        Catch ex As Exception
            Throw New Exception("Error en Get_Productos_SAP_SL: " & ex.Message, ex)
        End Try
    End Function

End Class
