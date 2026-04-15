Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Text
Imports System.Web.Routing
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports TOMWMS.clsDataContractDI
Imports TOMWMS.clsSyncSapTrasladosEnvio

Public Class clsSyncSapFacturaDeudor : Inherits clsInterfaceBase

    Private Shared vHanaService As SapServiceLayerClient

    Public Shared Async Function Procesar_Facturas_de_Deudor_SAP(ByVal lblprg As RichTextBox,
                                                                 ByVal prg As ProgressBar,
                                                                 Optional ByVal pNoDocumento As String = "") As Task(Of Boolean)
        Dim clsTrans As New clsTransaccion
        Dim sw As New Stopwatch()

        Try
            ' Inicia cronómetro y anuncia inicio
            sw.Start()
            clsPublic.Actualizar_Progreso(lblprg, "Iniciando proceso de sincronización de facturas de deudor desde SAP.")

            clsTrans.Begin_Transaction()

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                      clsTrans.lConnection,
                                                      clsTrans.lTransaction)

            Dim sessionCookie As String = ""
            Dim baseUrl As String = BD.Instancia.HANA_SL
            Dim BeBodega As clsBeBodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega,
                                                                        clsTrans.lConnection,
                                                                        clsTrans.lTransaction)

            If BeBodega Is Nothing Then
                Throw New Exception("ERROR_202311271751: Error no se pudo obtener el objeto de bodega asociado a la configuración de interface: " & BeConfigEnc.Idbodega)
            End If

            Await Procesar_Documentos(BeBodega.Codigo,
                                      pNoDocumento,
                                      BeConfigEnc,
                                      lblprg,
                                      clsTrans)

            clsTrans.Commit_Transaction()

            ' Éxito: detener cronómetro y reportar tiempo
            sw.Stop()
            clsPublic.Actualizar_Progreso(lblprg, $"Proceso completado correctamente. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.")
            Return True

        Catch ex As Exception
            ' Error: detener cronómetro y reportar tiempo + log
            If sw.IsRunning Then sw.Stop()
            clsTrans.RollBack_Transaction()
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, pNoDocumento, 1900, 900)
            clsPublic.Actualizar_Progreso(lblprg, $"Error en el proceso: {ex.Message}. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.")
            Throw

        Finally
            clsTrans.Close_Conection()
            ' Mensaje de fin (independiente del resultado)
            If sw.IsRunning Then sw.Stop()
            clsPublic.Actualizar_Progreso(lblprg, $"Fin del proceso de sincronización de las facturas de deudor desde SAP. Tiempo total: {sw.Elapsed.TotalSeconds:F2} segundos.")
        End Try

    End Function

    Private Shared Async Function Get_Factura_Deudor_SAP_SLAsync(pCodigoBodegaInterface As String,
                                                                 lConnection As SqlConnection,
                                                                 lTransaction As SqlTransaction,
                                                                 lblprg As RichTextBox,
                                                                 Optional pNoDocumentoSAP As String = "") As Task(Of List(Of clsBeI_nav_ped_traslado_enc))

        Dim lFacturasDeudor As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePropietario As clsBePropietarios = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario, lConnection, lTransaction)

        If BePropietario Is Nothing Then
            Throw New Exception($"#Error: No se encontró el propietario con ID {BeConfigEnc.IdPropietario}")
        End If

        Try
            vHanaService = New SapServiceLayerClient()
            Dim loginResponse As LoginResponseDto = vHanaService.LoginAsync().GetAwaiter().GetResult()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener sesión.")
                Return lFacturasDeudor
            Else
                clsPublic.Actualizar_Progreso(lblprg, "Conexión correcta.")
                Debug.WriteLine(vHanaService.SessionCookie)
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Obteniendo facturas de deudor...")

            ' Filtro a nivel de encabezado (no se puede filtrar por WarehouseCode aquí)
            Dim filtroFacturaDeudor As String = "ReserveInvoice eq 'tNO'"
            Dim filtroGuia As String = "U_Guia ne null and U_Guia ne ''"
            Dim filtroEnviado As String = "U_ENVIADO_WMS eq 2"
            Dim filtroDocNum As String = If(Not String.IsNullOrWhiteSpace(pNoDocumentoSAP), $" and DocNum eq {pNoDocumentoSAP}", "")
            Dim filtroFinal As String = $"{filtroFacturaDeudor} and {filtroEnviado}{filtroDocNum} and {filtroGuia}"

            Dim url As String = $"{BD.Instancia.HANA_SL}Invoices?$filter={Uri.EscapeDataString(filtroFinal)}"

            Using handler As New HttpClientHandler()
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, sslPolicyErrors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)
                    client.DefaultRequestHeaders.Remove("Cookie")
                    client.DefaultRequestHeaders.Add("Cookie", vHanaService.SessionCookie)
                    client.DefaultRequestHeaders.Accept.Clear()
                    client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

                    Dim response As HttpResponseMessage =
                    Await client.GetAsync(url).ConfigureAwait(False)

                    If Not response.IsSuccessStatusCode Then
                        Dim errorDetail As String =
                        Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                        Throw New Exception(
                        $"Error al obtener facturas de Deudor_Cliente desde SL: {response.StatusCode}, {errorDetail}"
                    )
                    End If

                    Dim json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
                    Dim parsed = JObject.Parse(json)

                    Dim valueArr = TryCast(parsed("value"), JArray)
                    If valueArr Is Nothing OrElse valueArr.Count = 0 Then
                        Return lFacturasDeudor
                    End If

                    ' Cache OITM: crear UNA vez para todas las facturas
                    Dim cache As New OitmCache(client, BD.Instancia.HANA_SL)

                    ' Si necesitas IDs únicos sin recalcular MaxID en cada línea
                    Dim nextDetId As Integer = 0

                    For Each factura_deudor As JObject In valueArr

                        Dim docLines = TryCast(factura_deudor("DocumentLines"), JArray)
                        If docLines Is Nothing OrElse docLines.Count = 0 Then Continue For

                        ' Filtrar por bodega
                        Dim contieneBodega As Boolean = docLines.Any(
                        Function(l) l?("WarehouseCode")?.ToString() = pCodigoBodegaInterface
                    )
                        If Not contieneBodega Then Continue For

                        ' Mapeo del documento
                        Dim beFacturaDeudor As New clsBeI_nav_ped_traslado_enc With {
                        .No = factura_deudor("DocEntry").Value(Of Integer),
                        .Posting_Date = factura_deudor("DocDate").Value(Of Date),
                        .Receipt_Date = factura_deudor("DocDate").Value(Of Date),
                        .Shipment_Date = factura_deudor("DocDate").Value(Of Date),
                        .Status = 1,
                        .Transfer_from_Code = pCodigoBodegaInterface,
                        .Transfer_from_Contact = factura_deudor("JournalMemo")?.ToString(),
                        .Transfer_to_Contact = factura_deudor("CardName")?.ToString(),
                        .Transfer_to_CodeField = factura_deudor("CardCode")?.ToString(),
                        .Transfer_to_Code = factura_deudor("CardCode")?.ToString(),
                        .Product_Owner_Code = BePropietario.Codigo,
                        .Receipt_Document_Reference = factura_deudor("DocNum").ToString(),
                        .Company_Code = "",
                        .Comments = factura_deudor("Comments")?.ToString(),
                        .Document_Type = tTipoDocumentoSalida.Factura_Deudor,
                        .Transportation_Guide = factura_deudor("U_Guia")?.ToString(),
                        .Lineas_Detalle = New List(Of clsBeI_nav_ped_traslado_det)
                         }

                        ' Mapeo de líneas
                        For Each linea As JObject In docLines

                            If linea?("WarehouseCode")?.ToString() <> pCodigoBodegaInterface Then Continue For

                            Dim itemCode As String = linea?("ItemCode")?.ToString()

                            ' Sin ItemCode => normalmente servicio/texto => excluir de WMS
                            If String.IsNullOrWhiteSpace(itemCode) Then Continue For

                            ' Obtener U_Grupo desde OITM (async)
                            Dim grupo As String = Await cache.GetUGrupoAsync(itemCode).ConfigureAwait(False)

                            ' Excluir grupo 19
                            If Not String.IsNullOrEmpty(grupo) AndAlso grupo = "19" Then Continue For

                            Dim beDet As New clsBeI_nav_ped_traslado_det With {
                            .NoEnc = beFacturaDeudor.No,
                            .No = clsLnI_nav_ped_traslado_det.MaxID() + 1,
                            .Item_No = linea("ItemCode")?.ToString(),
                            .Line_No = linea("LineNum").Value(Of Integer),
                            .Shipment_Date = Date.Now,
                            .Quantity = linea("Quantity").Value(Of Decimal),
                            .Description = linea("ItemDescription")?.ToString(),
                            .Unit_of_Measure_Code = linea("MeasureUnit")?.ToString(),
                            .Status = 1,
                            .Transfer_to_CodeField = linea("WarehouseCode")?.ToString(),
                            .Price = linea("Price").Value(Of Double),
                            .Color = linea("U_Color")?.ToString(),
                            .Size = linea("U_Talla")?.ToString(),
                            .Variant_Code = Nothing
                        }

                            beFacturaDeudor.Lineas_Detalle.Add(beDet)
                        Next

                        If beFacturaDeudor.Lineas_Detalle.Any() Then
                            lFacturasDeudor.Add(beFacturaDeudor)
                        End If

                    Next

                End Using

            End Using

            Return lFacturasDeudor

        Catch ex As Exception
            Throw New Exception("(SL) Get_Factura_Deudor_SAP_SL: " & ex.Message, ex)
        End Try
    End Function

    Private Shared Async Function Procesar_Documentos(ByVal codigoBodega As String,
                                                      ByVal pNoDocumento As String,
                                                      ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                      ByVal lblprg As RichTextBox,
                                                      ByVal clsTrans As clsTransaccion) As Task(Of Boolean)

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Conectando a SAP.")

            Dim facturas As List(Of clsBeI_nav_ped_traslado_enc) = Await Get_Factura_Deudor_SAP_SLAsync(codigoBodega, clsTrans.lConnection, clsTrans.lTransaction, lblprg, pNoDocumento)
            Dim pBePedidoEnc As New clsBeTrans_pe_enc
            Dim PedidoClienteExistenteByCompany As New clsBeTrans_pe_enc
            Dim PedidoClienteExistente As New clsBeTrans_pe_enc

            If facturas.Count = 0 Then
                clsPublic.Actualizar_Progreso(lblprg, "No hay documentos para importar.")
                Return False
            End If

            For Each factura In facturas

                clsPublic.Actualizar_Progreso(lblprg, $"Procesando factura de deudor de SAP (OINV): {factura.Receipt_Document_Reference}/{factura.No}{vbNewLine}")

                '#MECR 202508080524: Verifica si el proveedor ya existe como cliente en WMS.
                If Await clsSyncSapTrasladosEnvio.Validar_Cliente_WMS(factura.Transfer_to_Code, "C", lblprg, clsTrans, vHanaService.SessionCookie, BD.Instancia.HANA_SL) Then

                    Dim pedidoEnc As clsBeTrans_pe_enc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia_If(factura, lblprg, clsTrans.lConnection, clsTrans.lTransaction)

                    Dim trasladoSincronizado As Boolean = Marcar_Factura_Deudor_Sincronizada_SLAsync(factura.No, vHanaService.SessionCookie, BD.Instancia.HANA_SL).GetAwaiter().GetResult()

                    If pedidoEnc IsNot Nothing AndAlso trasladoSincronizado Then
                        Return True
                    End If

                End If

            Next

            Return False

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Shared Async Function Marcar_Factura_Deudor_Sincronizada_SLAsync(docEntry As String,
                                                                             sessionCookie As String,
                                                                             baseUrl As String) As Task(Of Boolean)

        Try
            If String.IsNullOrWhiteSpace(docEntry) Then Return False

            Dim requestUrl As String = $"Invoices({docEntry})"
            Dim payload As String = "{""U_ENVIADO_WMS"": ""1""}"
            Dim httpPatch As New HttpMethod("PATCH")

            Using handler As New HttpClientHandler()
                handler.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)
                    client.DefaultRequestHeaders.ConnectionClose = True

                    Using request As New HttpRequestMessage(httpPatch, baseUrl & requestUrl)
                        request.Headers.ConnectionClose = True
                        request.Headers.Add("Cookie", sessionCookie)
                        request.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                        request.Content = New StringContent(payload, Encoding.UTF8, "application/json")

                        Dim response = Await client.SendAsync(request).ConfigureAwait(False)

                        If response.IsSuccessStatusCode Then
                            Return True
                        Else
                            Dim errContent = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                            Throw New Exception($"Error al actualizar Invoices. Código: {response.StatusCode}, Detalle: {errContent}")
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception($"(SL) {MethodBase.GetCurrentMethod().Name} {ex.Message}", ex)
        End Try

    End Function

    Public Shared Async Function Enviar_Factura_Deudor_ClienteAsync(pIdBodega As Integer,
                                                                     lblprg As RichTextBox,
                                                                     prg As Windows.Forms.ProgressBar) As Task

        Try

            Dim vHanaService As New SapServiceLayerClient()
            Dim loginResponse As LoginResponseDto = Await vHanaService.LoginAsync()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener sesión.")
                Throw New Exception("No se pudo obtener sesión en SAP Service Layer.")
            Else
                clsPublic.Actualizar_Progreso(lblprg, "Conexión correcta.")
            End If

            Dim lTransaccionesSalida As New List(Of clsBeI_nav_transacciones_out)

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(clsDataContractDI.tTipoDocumentoSalida.Factura_Deudor)

            If lTransaccionesSalida IsNot Nothing AndAlso lTransaccionesSalida.Count > 0 Then

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones a enviar: {0}", lTransaccionesSalida.Count))

                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc, Key i.Idbodega} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc, Key Keys.Idbodega}).
                                          Where(Function(x) x.Idbodega = pIdBodega).ToList()

                For Each PT In ListaPedidosTransf

                    Dim clsTrans As New clsTransaccion()
                    Dim BePedidoEnc As clsBeTrans_pe_enc = Nothing
                    Dim vOperadorVerificoDefecto As String = ""
                    Dim Enviado_A_Erp As Boolean = False
                    Dim vUsuarioWMS As String = ""
                    Dim vEmpresaTransporte As String = ""
                    Dim vFechaInicioPack As DateTime = Now
                    Dim vFechaFinPack As DateTime = Now
                    Dim fechaEnvio As DateTime = Now

                    Try
                        clsTrans.Begin_Transaction()

                        BePedidoEnc = clsLnTrans_pe_enc.GetSingle(PT.Idpedidoenc, clsTrans.lConnection, clsTrans.lTransaction)

                        If BePedidoEnc Is Nothing OrElse BePedidoEnc.Picking Is Nothing Then
                            Throw New Exception("El pedido no tiene Picking asociado.")
                        End If

                        Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP(PT.No_pedido,
                                                                                   clsTrans.lConnection,
                                                                                   clsTrans.lTransaction)

                        If Enviado_A_Erp Then
                            clsTrans.Commit_Transaction()
                            clsPublic.Actualizar_Progreso(lblprg, $"ℹ️ La Factura de Deudor DocEntry {PT.No_pedido} ya estaba enviada a ERP.")
                            Continue For
                        End If

                        vOperadorVerificoDefecto = clsLnTrans_picking_ubic.Get_Op_Verifico_Defecto_By_IdPickingEnc(BePedidoEnc.Picking.IdPickingEnc,
                                                                                                                   clsTrans.lConnection,
                                                                                                                   clsTrans.lTransaction)
                        If String.IsNullOrWhiteSpace(vOperadorVerificoDefecto) Then
                            vOperadorVerificoDefecto = ""
                        End If

                        Dim BeUsuario As clsBeUsuario = clsLnUsuario.GetSingle(IdUsuario,
                                                                               clsTrans.lConnection,
                                                                               clsTrans.lTransaction)
                        If BeUsuario IsNot Nothing Then
                            vUsuarioWMS = String.Format("{0} {1}", BeUsuario.Nombres, BeUsuario.Apellidos).Trim()
                        End If

                        Dim BeEmpresa As New clsBeEmpresa_transporte
                        BeEmpresa.IdEmpresaTransporte = BePedidoEnc.IdEmpresaTransporte

                        clsLnEmpresa_transporte.GetSingle(BeEmpresa, clsTrans.lConnection, clsTrans.lTransaction)

                        If BeEmpresa IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(BeEmpresa.Nombre) Then
                            vEmpresaTransporte = BeEmpresa.Nombre
                        End If

                        clsLnTrans_picking_ubic.Get_Fechas_Picking(vFechaInicioPack, vFechaFinPack, BePedidoEnc.IdPedidoEnc,
                                                                   clsTrans.lConnection, clsTrans.lTransaction)

                        clsTrans.Commit_Transaction()

                    Catch ex As Exception
                        Try
                            clsTrans.RollBack_Transaction()

                        Catch
                        End Try

                        clsPublic.Actualizar_Progreso(lblprg, $"❌ Error leyendo datos de la factura de Deudor DocEntry {PT.No_pedido}: {ex.Message}")
                        Continue For
                    Finally
                        If clsTrans.lConnection IsNot Nothing AndAlso clsTrans.lConnection.State = ConnectionState.Open Then
                            clsTrans.Close_Conection()
                        End If
                    End Try

                    If Not Enviado_A_Erp Then

                        Try

                            Dim dto As New UDF_FacturaDeudor With {
                                    .U_USR_PACK = vOperadorVerificoDefecto,
                                    .U_USR_ENVIO = vUsuarioWMS,
                                    .U_OPERADOR_WMS = vUsuarioWMS,
                                    .U_GUIA_TRANSPORTISTA = vEmpresaTransporte,
                                    .U_ESTADO_GUIA = "8",
                                    .U_DOCUMENTO_WMS = BePedidoEnc.IdPedidoEnc,
                                    .U_INICIO_PACK = vFechaInicioPack,
                                    .U_FIN_PACK = vFechaFinPack,
                                    .U_INICIO_ENVIO = fechaEnvio.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                                    .U_FIN_ENVIO = fechaEnvio.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                                    .U_ENVIADO_WMS = 1,
                                    .U_ENVIADO_SAP_WMS = BePedidoEnc.Fec_agr.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}

                            Dim payloadStockTransfer = dto
                            Dim json As String = JsonConvert.SerializeObject(payloadStockTransfer, New JsonSerializerSettings With {.NullValueHandling = NullValueHandling.Ignore})

                            Dim resultado As Boolean = Await Enviar_UDF_Factura_Deudor_SLAsync(PT.No_pedido, json, vHanaService.SessionCookie, BD.Instancia.HANA_SL)

                            If resultado Then

                                Dim clsTransUpd As New clsTransaccion()

                                Try
                                    clsTransUpd.Begin_Transaction()

                                    clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc(BePedidoEnc.IdPedidoEnc,
                                                                                                      True,
                                                                                                      IdUsuario,
                                                                                                      clsTransUpd.lConnection,
                                                                                                      clsTransUpd.lTransaction)

                                    clsTransUpd.Commit_Transaction()

                                    clsPublic.Actualizar_Progreso(lblprg, $"✅ UDFs actualizados en la Factura de Deudor DocEntry {PT.No_pedido}, DocNum {BePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino}")

                                Catch ex2 As Exception
                                    Try
                                        clsTransUpd.RollBack_Transaction()
                                    Catch
                                    End Try

                                    clsPublic.Actualizar_Progreso(lblprg, $"⚠ No se pudo marcar como enviada a ERP la Factura de Deudor DocEntry {PT.No_pedido}, DocNum {BePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino}: {ex2.Message}")
                                Finally
                                    If clsTransUpd.lConnection IsNot Nothing AndAlso clsTransUpd.lConnection.State = ConnectionState.Open Then
                                        clsTransUpd.Close_Conection()
                                    End If
                                End Try

                            End If

                        Catch ex As Exception
                            clsPublic.Actualizar_Progreso(lblprg, $"❌ EXCEPCIÓN PATCH UDF Factura Deudor DocEntry {PT.No_pedido} DocNum {BePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino} : {ex.Message}")
                        End Try
                        '' ====== FIN PATCH ======

                    End If

                Next

            Else

                clsPublic.Actualizar_Progreso(lblprg, "MSG_240117: No hay transacciones para enviar.")

            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            prg.Value = 0
            prg.Visible = False
        End Try

    End Function

    Private Shared Async Function Enviar_UDF_Factura_Deudor_SLAsync(docEntry As String,
                                                                    payload As String,
                                                                    sessionCookie As String,
                                                                    baseUrl As String) As Task(Of Boolean)

        Try
            If String.IsNullOrWhiteSpace(docEntry) Then Return False

            Dim requestUrl As String = $"Invoices({docEntry})"

            Dim httpPatch As New HttpMethod("PATCH")

            Using handler As New HttpClientHandler()
                handler.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)
                    client.DefaultRequestHeaders.ConnectionClose = True

                    Using request As New HttpRequestMessage(httpPatch, baseUrl & requestUrl)
                        request.Headers.ConnectionClose = True
                        request.Headers.Add("Cookie", sessionCookie)
                        request.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                        request.Content = New StringContent(payload, Encoding.UTF8, "application/json")

                        Dim response = Await client.SendAsync(request).ConfigureAwait(False)

                        If response.IsSuccessStatusCode Then
                            Return True
                        Else
                            Dim errContent = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                            Throw New Exception($"Error al actualizar Invoices. Código: {response.StatusCode}, Detalle: {errContent}")
                        End If

                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception($"(SL) {MethodBase.GetCurrentMethod().Name} {ex.Message}", ex)
        End Try

    End Function

End Class

<Serializable>
<JsonObject(MemberSerialization:=MemberSerialization.OptOut)>
Public Class UDF_FacturaDeudor
    <JsonProperty("U_USR_PACK", Order:=4)>
    Public Property U_USR_PACK As String = ""
    <JsonProperty("U_USR_ENVIO", Order:=5)>
    Public Property U_USR_ENVIO As String = ""
    <JsonProperty("U_OPERADOR_WMS", Order:=6)>
    Public Property U_OPERADOR_WMS As String = ""
    <JsonProperty("U_GUIA_TRANSPORTISTA", Order:=7)>
    Public Property U_GUIA_TRANSPORTISTA As String = ""
    <JsonProperty("U_ESTADO_GUIA", Order:=8)>
    Public Property U_ESTADO_GUIA As String = ""
    <JsonProperty("U_DOCUMENTO_WMS", Order:=9)>
    Public Property U_DOCUMENTO_WMS As Integer = 0
    <JsonProperty("U_INICIO_PACK", Order:=10)>
    Public Property U_INICIO_PACK As DateTime = Now
    <JsonProperty("U_FIN_PACK", Order:=11)>
    Public Property U_FIN_PACK As DateTime = Now
    <JsonProperty("U_INICIO_ENVIO", Order:=12)>
    Public Property U_INICIO_ENVIO As DateTime = Now
    <JsonProperty("U_FIN_ENVIO", Order:=13)>
    Public Property U_FIN_ENVIO As DateTime = Now

    <JsonProperty("U_ENVIADO_WMS", Order:=14)>
    Public Property U_ENVIADO_WMS As Integer = 1

    <JsonProperty("U_ENVIADO_SAP_WMS", Order:=15)>
    Public Property U_ENVIADO_SAP_WMS As DateTime = Now

End Class
