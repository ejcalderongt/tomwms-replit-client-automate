Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports DevExpress.Drawing.Internal.Images
Imports Newtonsoft.Json.Linq
Imports Sap.Data.Hana
Public Class clsSyncSAPBodega : Inherits clsInterfaceBase

    Private Shared fichaBodegas As List(Of clsBeI_nav_bodega)
    Shared VContadorBitacoraTOMWMS As Integer = 0
    Shared VContadorBitacoraIntermedia As Integer = 0
    Private Shared Async Function Importar_Bodegas_Desde_SAP_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                               prg As ProgressBar,
                                                                               cnnLog As SqlConnection) As Task(Of Boolean)

        Dim Cnn As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTrans As SqlTransaction = Nothing

        Try

            clsLnI_nav_bodega.EliminarTodos(Cnn, lTrans)

            clsPublic.Actualizar_Progreso(lblprg, "Conectando a Hdb.")

            Dim vHanaService As New SapServiceLayerClient
            Dim loginResponse As LoginResponseDto = Await vHanaService.LoginAsync()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener sesión.")
                Return False
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Sesión iniciada correctamente.")

            fichaBodegas = Await Get_Bodegas_SAP(vHanaService.SessionCookie, SapServiceLayerClient.baseUrl)

            clsPublic.Actualizar_Progreso(lblprg, "Consultando bodegas en SAP (OWHS).")

            Application.DoEvents()

            prg.Maximum = fichaBodegas.Count

            Dim vContador As Integer = 0

            Cnn.Open() : lTrans = Cnn.BeginTransaction(IsolationLevel.ReadUncommitted)

            BeNavEjecucionRes.Registros_ws = fichaBodegas.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Dim vCodigoBodega As String = ""

            For Each bodega In fichaBodegas

                vCodigoBodega = bodega.Bodega_code

                Try

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Bodega: {0} ", bodega.Bodega_code, vbNewLine))

                    clsLnI_nav_bodega.Insertar(bodega, Cnn, lTrans)

                    VContadorBitacoraIntermedia += 1

                    prg.Value = vContador

                    vContador += 1

                    Application.DoEvents()

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               vCodigoBodega,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet,
                                                               cnnLog)

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: {0} ", ex.Message, vbNewLine))

                End Try

            Next

            lTrans.Commit()

            clsPublic.Actualizar_Progreso(lblprg, "Fin de proceso: " & Now)

            Return True

        Catch ex As Exception

            If Not lTrans Is Nothing Then lTrans.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet,
                                                       cnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: {0} ", ex.Message, vbNewLine))

            Throw ex

        Finally
            If Cnn.State = ConnectionState.Open Then Cnn.Close()
            prg.Value = 0
        End Try

    End Function

    Private Shared Function Get_Bodegas_SAP() As List(Of clsBeI_nav_bodega)

        Get_Bodegas_SAP = Nothing

        Dim lBodegasWMS As New List(Of clsBeI_nav_bodega)
        Dim BeBodega As New clsBeI_nav_bodega

        Try

            Dim query As String = "SELECT ""WhsCode"", ""WhsName"", ""TransferAc"" FROM ""OWHS"" 
                                   WHERE ""Inactive"" = 'N'"

            Using conn As HanaConnection = HanaHelper.OpenDB()
                Dim dt As DataTable = HanaHelper.OpenDT(query, conn)

                For Each row As DataRow In dt.Rows
                    BeBodega = New clsBeI_nav_bodega
                    BeBodega.Bodega_code = If(IsDBNull(row("WhsCode")), "0", row("WhsCode").ToString())
                    BeBodega.Bodega_name = If(IsDBNull(row("WhsName")), "", row("WhsName").ToString())
                    lBodegasWMS.Add(BeBodega)
                Next

            End Using

            Return lBodegasWMS

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Async Function Insertar_Bodegas_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(ByVal lblprg As RichTextBox,
                                                                                        prg As ProgressBar,
                                                                                        Optional ByVal ForzarEjecucion As Boolean = False,
                                                                                        Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Task(Of Boolean)

        Dim ok As Boolean = False

        Try
            clsPublic.Actualizar_Progreso(lblprg, $"Force_Ejecución: {ForzarEjecucion}")

            ' 1) Validación de ejecución (si no se fuerza)
            If Not ForzarEjecucion AndAlso Not Ejecutar_Interfaz("Bodega") Then
                clsPublic.Actualizar_Progreso(lblprg, "La configuración de la interface indica que no se debe ejecutar en este momento.")
                Return False
            End If

            Using CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient),
              CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)

                Await CnnLog.OpenAsync().ConfigureAwait(False)
                Await CnnInterface.OpenAsync().ConfigureAwait(False)

                ' 2) Encabezado de ejecución
                BeNavEjecucionEnc.IdEjecucionEnc = clsLnI_nav_ejecucion_enc.MaxID(CnnLog)
                BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
                BeNavEjecucionEnc.Fecha = Now
                clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

                ' 3) Crear resultado de ejecución
                BeNavEjecucionRes.IdEjecucionRes = clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
                BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
                BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
                BeNavEjecucionRes.Registros_ws = 0
                BeNavEjecucionRes.Registros_ti = 0
                BeNavEjecucionRes.Registros_WMS = 0
                BeNavEjecucionRes.Exitosa = False
                clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

                clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

                ' 4) Transacción principal (lectura e inserción)
                Using lTrans As SqlTransaction = CnnInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

                    ' 4.1) ¿Llenar intermedia desde SAP?
                    Dim ejecutarImportacion As Boolean = True
                    If Pregunta_Si_LLena_Intermedia Then
                        Dim r = MessageBox.Show("¿Llenar tabla intermedia desde SAP?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        ejecutarImportacion = (r = DialogResult.Yes)
                        If Not ejecutarImportacion Then
                            clsPublic.Actualizar_Progreso(lblprg, "Se omitió el llenado de tabla intermedia por selección del usuario.")
                        End If
                    End If

                    If ejecutarImportacion Then
                        Dim importo As Boolean = Await Importar_Bodegas_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog).ConfigureAwait(False)
                        If Not importo Then
                            Try : lTrans.Rollback() : Catch : End Try
                            prg.Value = 0
                            clsPublic.Actualizar_Progreso(lblprg, "No se importaron bodegas a la tabla intermedia.")
                            Return False
                        End If
                    End If

                    ' 4.2) Cargar bodegas intermedias
                    clsPublic.Actualizar_Progreso(lblprg, "Consultando bodegas en tabla intermedia")
                    Dim lBodegas As List(Of clsBeI_nav_bodega) = clsLnI_nav_bodega.GetAll(CnnInterface, lTrans)
                    clsPublic.Actualizar_Progreso(lblprg, $"Bodegas en tabla intermedia: {lBodegas.Count}")

                    If lBodegas IsNot Nothing AndAlso lBodegas.Count > 0 Then
                        BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, CnnInterface, lTrans)

                        prg.Minimum = 0
                        prg.Maximum = lBodegas.Count
                        prg.Value = 0

                        clsPublic.Actualizar_Progreso(lblprg, "Trasladando bodegas como clientes en TOMWMS.")

                        Dim vContador As Integer = 0

                        For Each navBodega As clsBeI_nav_bodega In lBodegas
                            vContador += 1
                            prg.Value = vContador
                            clsPublic.Actualizar_Progreso(lblprg, $"Procesando Bodega: {navBodega.Bodega_code}")

                            Dim beClienteExistente As clsBeCliente = clsLnCliente.Existe(navBodega.Bodega_code, CnnInterface, lTrans)

                            If beClienteExistente IsNot Nothing Then
                                ' Update
                                Try
                                    Dim beCliente As New clsBeCliente With {
                                    .IdCliente = beClienteExistente.IdCliente,
                                    .IdPropietario = BeConfigEnc.IdPropietario,
                                    .Codigo = navBodega.Bodega_code,
                                    .Nombre_comercial = navBodega.Bodega_name,
                                    .Sistema = True,
                                    .Activo = True,
                                    .IdEmpresa = BeConfigEnc.Idempresa,
                                    .Nit = navBodega.Bodega_code,
                                    .IdTipoCliente = 1,
                                    .Es_bodega_recepcion = beClienteExistente.Es_bodega_recepcion,
                                    .Es_Bodega_Traslado = beClienteExistente.Es_Bodega_Traslado
                                }

                                    clsLnCliente.ActualizarFromInterface(beCliente, CnnInterface, lTrans)
                                    VContadorBitacoraTOMWMS += 1

                                Catch ex As Exception
                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                           navBodega.Bodega_code,
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet,
                                                                           CnnLog)
                                    clsPublic.Actualizar_Progreso(lblprg, $"Error al actualizar bodega: {navBodega.Bodega_code}{vbNewLine}{ex.Message}")
                                End Try

                            Else
                                ' Insert
                                Dim beCliente As New clsBeCliente With {
                                .IdEmpresa = BeConfigEnc.Idempresa,
                                .IdPropietario = BeConfigEnc.IdPropietario,
                                .Codigo = navBodega.Bodega_code,
                                .Nombre_comercial = navBodega.Bodega_name,
                                .IdCliente = clsLnCliente.MaxID(CnnInterface, lTrans) + 1,
                                .Nit = navBodega.Bodega_code,
                                .IdTipoCliente = 1,
                                .Activo = True,
                                .User_agr = BeConfigEnc.IdUsuario,
                                .Fec_agr = Now,
                                .User_mod = BeConfigEnc.IdUsuario,
                                .Fec_mod = Now,
                                .Sistema = True
                            }

                                Try
                                    clsLnCliente.Insertar(beCliente, CnnInterface, lTrans)
                                    VContadorBitacoraTOMWMS += 1

                                    Dim beClienteBodega As New clsBeCliente_bodega With {
                                    .IdClienteBodega = clsLnCliente_bodega.MaxID(CnnInterface, lTrans) + 1,
                                    .IdCliente = beCliente.IdCliente,
                                    .IdBodega = BeConfigEnc.Idbodega,
                                    .Activo = True,
                                    .User_agr = BeConfigEnc.IdUsuario,
                                    .User_mod = BeConfigEnc.IdUsuario,
                                    .Fec_agr = Now,
                                    .Fec_mod = Now
                                }

                                    clsLnCliente_bodega.Insertar_From_Interface(beClienteBodega, CnnInterface, lTrans)
                                    clsPublic.Actualizar_Progreso(lblprg, $"Fin de inserción para: {beCliente.Codigo}")

                                Catch ex As Exception
                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                           navBodega.Bodega_code,
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet,
                                                                           CnnLog)
                                    clsPublic.Actualizar_Progreso(lblprg, $"Error al insertar bodega: {navBodega.Bodega_code}{vbNewLine}{ex.Message}")
                                End Try
                            End If
                        Next
                    End If

                    ' 4.3) Commit
                    lTrans.Commit()
                    ok = True
                End Using

                ' 5) Mensajes finales y actualización de resultados
                clsPublic.Actualizar_Progreso(lblprg, "Fin de proceso.")
                clsPublic.Actualizar_Progreso(lblprg, $"Bodegas procesadas correctamente: {VContadorBitacoraTOMWMS}")
                Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
                clsPublic.Actualizar_Progreso(lblprg, $"Tiempo transcurrido: {difSegundos} segundo(s)")

                BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
                BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTOMWMS
                BeNavEjecucionRes.Exitosa = (VContadorBitacoraIntermedia = VContadorBitacoraTOMWMS)

                Try
                    clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)
                Catch ex As Exception
                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                           "clsLnI_nav_ejecucion_res.Actualizar",
                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                           BeConfigDet.Idnavconfigdet,
                                                           CnnLog)
                    clsPublic.Actualizar_Progreso(lblprg, $"Error al actualizar resultado de ejecución: {ex.Message}")
                    Return False
                End Try
            End Using

            prg.Value = 0
            Return ok

        Catch ex As Exception
            prg.Value = 0
            clsPublic.Actualizar_Progreso(lblprg, $"Error al insertar Bodega-Cliente a tabla de TOMWMS: {ex.Message}")
            ' Log de error con la conexión que está fuera del Using no es posible; se asume logger global o dentro de bloques previos.
            Return False
        End Try

    End Function

    Public Shared Async Function Get_Bodegas_SAP(sessionCookie As String, baseUrl As String) As Task(Of List(Of clsBeI_nav_bodega))

        Dim bodegas As New List(Of clsBeI_nav_bodega)

        Try
            Dim requestUrl As String = "Warehouses?$filter=Inactive eq 'tNO'"

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
                            Throw New Exception($"Error al obtener bodegas. Código: {response.StatusCode}, Detalle: {errContent}")
                        End If

                        Dim jsonResponse = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                        Dim obj = JObject.Parse(jsonResponse)
                        Dim rows = obj("value")

                        If rows Is Nothing OrElse Not rows.HasValues Then
                            Return bodegas ' Vacía
                        End If

                        For Each row In rows
                            Dim bodega As New clsBeI_nav_bodega()
                            bodega.Bodega_code = row.Value(Of String)("WarehouseCode")
                            bodega.Bodega_name = row.Value(Of String)("WarehouseName")
                            bodegas.Add(bodega)
                        Next

                        Return bodegas
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception("Error en Get_Bodegas_SAP: " & ex.Message, ex)
        End Try

    End Function

    Public Shared Async Function Get_Bodega_SAP_By_Codigo(whsCode As String, sessionCookie As String, baseUrl As String) As Task(Of clsBeI_nav_bodega)

        Dim bodega As New clsBeI_nav_bodega()

        Try

            Dim requestUrl As String = $"Warehouses('{whsCode}')"

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
                            Throw New Exception($"Error al obtener bodegas. Código: {response.StatusCode}, Detalle: {errContent}")
                        End If

                        Dim jsonResponse = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                        Dim obj = JObject.Parse(jsonResponse)

                        bodega = New clsBeI_nav_bodega With {
                        .Bodega_code = obj("WarehouseCode")?.ToString(),
                        .Bodega_name = obj("WarehouseName")?.ToString()}

                        Return bodega

                    End Using
                End Using
            End Using

            Return bodega

        Catch ex As Exception
            Throw New Exception("Error en Get_Bodegas_SAP: " & ex.Message, ex)
        End Try

    End Function

End Class