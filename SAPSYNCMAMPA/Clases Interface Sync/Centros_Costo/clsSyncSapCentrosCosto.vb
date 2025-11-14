Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports Newtonsoft.Json.Linq

Public Class clsSyncSapCentrosCosto : Inherits clsInterfaceBase

    Private Shared fichaCentrosCosto As List(Of clsBeCentro_costo)
    Shared VContadorBitacoraTOMWMS As Integer = 0
    Shared VContadorBitacoraIntermedia As Integer = 0

    Public Shared Async Function Get_Centros_Costo_SAP(sessionCookie As String,
                                                       baseUrl As String) As Task(Of List(Of clsBeCentro_costo))

        Dim centros_costo As New List(Of clsBeCentro_costo)

        Try
            Dim requestUrl As String = "ProfitCenters?$filter=Active eq 'tYES' and (InWhichDimension eq 1 or InWhichDimension eq 2 or InWhichDimension eq 3)"

            Using handler As New HttpClientHandler()
                handler.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)
                    client.DefaultRequestHeaders.ConnectionClose = True

                    Using request As New HttpRequestMessage(HttpMethod.Get, baseUrl & requestUrl)
                        request.Headers.ConnectionClose = True
                        request.Headers.Add("Cookie", sessionCookie)
                        request.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

                        Dim response As HttpResponseMessage = Await client.SendAsync(request).ConfigureAwait(False)

                        If Not response.IsSuccessStatusCode Then
                            Dim errContent = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                            Throw New Exception($"Error al obtener centro de costo. Código: {response.StatusCode}, Detalle: {errContent}")
                        End If

                        Dim jsonResponse = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                        Dim obj = JObject.Parse(jsonResponse)
                        Dim rows = obj("value")

                        If rows Is Nothing OrElse Not rows.HasValues Then
                            Return centros_costo ' Vacía
                        End If

                        For Each row In rows
                            Dim centro_costo As New clsBeCentro_costo()
                            centro_costo.Codigo = row.Value(Of String)("CenterCode")
                            centro_costo.Nombre = row.Value(Of String)("CenterName")
                            centro_costo.Referencia = row.Value(Of String)("InWhichDimension")
                            centro_costo.Activo = True
                            centro_costo.User_agr = "MI3"
                            centro_costo.User_mod = "MI3"
                            centro_costo.Control_Inventario = True
                            centros_costo.Add(centro_costo)
                        Next

                        Return centros_costo
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception("Error en Get_Centros_Costo_SAP: " & ex.Message, ex)
        End Try

    End Function

    Public Shared Async Function Importar_Centros_Costo_Desde_SAP(ByVal lblprg As RichTextBox,
                                                                  prg As ProgressBar) As Task(Of Boolean)

        Dim Cnn As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTrans As SqlTransaction = Nothing
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim ok As Boolean = False

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Conectando a Hdb.")

            Dim vHanaService As New SapServiceLayerClient
            Dim loginResponse As LoginResponseDto = Await vHanaService.LoginAsync()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener sesión.")
                Return False
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Sesión iniciada correctamente.")

            fichaCentrosCosto = Await Get_Centros_Costo_SAP(vHanaService.SessionCookie, SapServiceLayerClient.baseUrl)

            clsPublic.Actualizar_Progreso(lblprg, "Consultando bodegas en SAP (OWHS).")

            Application.DoEvents()

            prg.Maximum = fichaCentrosCosto.Count

            Dim vContador As Integer = 0

            Cnn.Open() : lTrans = Cnn.BeginTransaction(IsolationLevel.ReadUncommitted)
            CnnLog.Open()

            BeNavEjecucionRes.Registros_ws = fichaCentrosCosto.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)

            Dim vCodigoCentroCosto As String = ""

            For Each centro In fichaCentrosCosto

                vCodigoCentroCosto = centro.Codigo

                Try

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Bodega: {0} ", centro.Nombre, vbNewLine))

                    Dim beCentroCostoExistente As clsBeCentro_costo = clsLnCentro_costo.Existe_By_Codigo(centro.Codigo, Cnn, lTrans)

                    If beCentroCostoExistente IsNot Nothing Then
                        clsLnCentro_costo.Actualizar(centro, Cnn, lTrans)
                    Else
                        centro.IdCentroCosto = clsLnCentro_costo.MaxID(Cnn, lTrans) + 1
                        clsLnCentro_costo.Insertar(centro, Cnn, lTrans)
                    End If

                    VContadorBitacoraIntermedia += 1

                    prg.Value = vContador

                    vContador += 1

                    Application.DoEvents()

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               vCodigoCentroCosto,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet,
                                                               CnnLog)

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: {0} ", ex.Message, vbNewLine))

                End Try

            Next

            ok = True

            lTrans.Commit()

            clsPublic.Actualizar_Progreso(lblprg, "Fin de proceso: " & Now)

        Catch ex As Exception

            If Not lTrans Is Nothing Then lTrans.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet,
                                                       CnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: {0} ", ex.Message, vbNewLine))

            Throw ex

        Finally
            If Cnn.State = ConnectionState.Open Then Cnn.Close()
            prg.Value = 0
        End Try

        Return ok

    End Function

End Class
