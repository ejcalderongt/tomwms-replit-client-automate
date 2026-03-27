Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports Newtonsoft.Json.Linq

Public Class clsSyncSapTalla
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

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub

    Public Shared Async Function Insertar_Talla_From_Sap_HanaAsync(ByVal codigo As String,
                                                                   SessionCookie As String,
                                                                   baseUrl As String,
                                                                   Optional lbl As RichTextBox = Nothing) As Task(Of Integer)

        Dim Talla As clsBeTalla

        Try

            Using lConnection As New SqlConnection((BD.Instancia.CadenaConexionSQLClient))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Talla = Await Get_Talla_SAP_SL(SessionCookie, baseUrl, lbl, codigo, lConnection, lTransaction)

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            If Talla IsNot Nothing Then
                Return Talla.IdTalla
            Else
                Return 0
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Async Function Insertar_Talla_From_Sap_HanaAsync(ByVal codigo As String,
                                                                   SessionCookie As String,
                                                                   baseUrl As String,
                                                                   lConnection As SqlConnection,
                                                                   lTransaction As SqlTransaction,
                                                                   Optional lbl As RichTextBox = Nothing) As Task(Of Integer)

        Dim Talla As clsBeTalla

        Try

            Talla = Await Get_Talla_SAP_SL(SessionCookie, baseUrl, lbl, codigo, lConnection, lTransaction)

            lTransaction.Commit()

            If Talla IsNot Nothing Then
                Return Talla.IdTalla
            Else
                Return 0
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Async Function Get_Talla_SAP_SL(sessionCookie As String,
                                                  baseUrl As String,
                                                  lbl As RichTextBox,
                                                  codigo As String,
                                                  lConnection As SqlConnection,
                                                  lTransaction As SqlTransaction) As Task(Of clsBeTalla)

        Dim BeTalla As New clsBeTalla

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
                        $"{baseUrl}U_TALLAS?$filter={filterEncoded}"

                    Using request As New HttpRequestMessage(HttpMethod.Get, requestUrl)
                        request.Headers.ConnectionClose = True

                        Dim response As HttpResponseMessage = Await client.SendAsync(request).ConfigureAwait(False)

                        If Not response.IsSuccessStatusCode Then
                            Dim errContent = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                            Throw New Exception($"Error al obtener la talla desde Service Layer. Código: {response.StatusCode}, Detalle: {errContent}")
                        End If

                        Dim jsonResponse = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                        Dim obj = JObject.Parse(jsonResponse)
                        Dim rows = obj("value")

                        For Each row In rows
                            BeTalla = New clsBeTalla With {
                                                        .IdTalla = clsLnTalla.MaxID(lConnection, lTransaction) + 1,
                                                        .Codigo = row("Code").ToString(),
                                                        .Nombre = row("Name").ToString(),
                                                        .IdPropietario = BeConfigEnc.IdPropietario,
                                                        .Fec_agr = Now,
                                                        .Fec_mod = Now,
                                                        .User_agr = BeConfigEnc.User_agr,
                                                        .User_mod = BeConfigEnc.User_mod,
                                                        .Activo = True
                                                         }
                            If Not clsLnTalla.Existe_By_Codigo(BeTalla.Codigo, lConnection, lTransaction) Then
                                clsLnTalla.Insertar(BeTalla, lConnection, lTransaction)
                                Exit For ' Si solo se espera un registro, salir después de insertar el primero encontrado
                            End If

                            clsPublic.Actualizar_Progreso(lbl, $"Procesando Talla: {BeTalla.Codigo} - {BeTalla.Nombre}")
                        Next

                    End Using
                End Using

            End Using

            Return BeTalla

        Catch ex As Exception
            Throw New Exception("Error en Get_Productos_SAP_SL: " & ex.Message, ex)
        End Try
    End Function

    Public Shared Async Function Get_Tallas_SAP_SL(vBeConfigEnc As clsBeI_nav_config_enc,
                                                   lbl As RichTextBox) As Task(Of Boolean)

        Dim BeTalla As New clsBeTalla

        Try

            vHanaService = New SapServiceLayerClient()
            Dim loginResponse As LoginResponseDto = Await vHanaService.LoginAsync()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lbl, "No se pudo obtener sesión.")
                Return False
            Else
                clsPublic.Actualizar_Progreso(lbl, "Conexión correcta.")
            End If

            clsPublic.Actualizar_Progreso(lbl, "Consultando tallas en SAP (U_TALLAS).")

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

                                Dim requestUrl As String = $"{baseUrl}U_TALLAS?$top={pageSize}&$skip={skip}"

                                Using request As New HttpRequestMessage(HttpMethod.Get, requestUrl)

                                    request.Headers.ConnectionClose = True

                                    Dim response As HttpResponseMessage = Await client.SendAsync(request).ConfigureAwait(False)

                                    If Not response.IsSuccessStatusCode Then
                                        Dim errContent = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                                        Throw New Exception($"Error al obtener las tallas desde Service Layer. Código: {response.StatusCode}, Detalle: {errContent}")
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
                                        BeTalla = New clsBeTalla With {
                                                            .IdTalla = clsLnTalla.MaxID(lConnection, lTransaction) + 1,
                                                            .Codigo = row("Code").ToString(),
                                                            .Nombre = row("Name").ToString(),
                                                            .IdPropietario = vBeConfigEnc.IdPropietario,
                                                            .Fec_agr = Now,
                                                            .Fec_mod = Now,
                                                            .User_agr = vBeConfigEnc.User_agr,
                                                            .User_mod = vBeConfigEnc.User_mod,
                                                            .Activo = True
                                                             }
                                        If Not clsLnTalla.Existe_By_Codigo(BeTalla.Codigo, lConnection, lTransaction) Then
                                            clsLnTalla.Insertar(BeTalla, lConnection, lTransaction)
                                            clsPublic.Actualizar_Progreso(lbl, $"Procesando Tallas: {BeTalla.Codigo} - {BeTalla.Nombre}")
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

            clsPublic.Actualizar_Progreso(lbl, "Fin de procesamiento de tallas -> " & Now)

            Return True

        Catch ex As Exception
            Throw New Exception("Error en Get_Productos_SAP_SL: " & ex.Message, ex)
        End Try
    End Function

End Class
