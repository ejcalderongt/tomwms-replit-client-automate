Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports TOMWMS.clsDataContractDI
Imports TOMWMS.clsSyncSapTrasladosEnvio

Public Class clsSyncSapDevolProveedor

    Private Shared vHanaService As SapServiceLayerClient

    Public Shared Async Function Procesar_Solicitud_Devol_Prov_SAP(ByVal lblprg As RichTextBox,
                                                               ByVal prg As ProgressBar,
                                                               Optional ByVal pNoDocumento As String = "") As Task(Of Boolean)
        Dim clsTrans As New clsTransaccion
        Dim sw As New Stopwatch()

        Try
            ' Inicia cronómetro y anuncia inicio
            sw.Start()
            clsPublic.Actualizar_Progreso(lblprg, "Iniciando proceso de sincronización de devoluciones de proveedor desde SAP.")

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
            clsPublic.Actualizar_Progreso(lblprg, $"Fin del proceso de sincronización de devoluciones de proveedor desde SAP. Tiempo total: {sw.Elapsed.TotalSeconds:F2} segundos.")
        End Try

    End Function

    Private Shared Function Get_Devoluciones_Proveedor_SAP_SL(pCodigoBodegaInterface As String,
                                                          lConnection As SqlConnection,
                                                          lTransaction As SqlTransaction,
                                                          lblprg As RichTextBox,
                                                          Optional pNoDocumentoSAP As String = "") As List(Of clsBeI_nav_ped_traslado_enc)

        Dim lDevolucionesProveedor As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePropietario As clsBePropietarios = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario, lConnection, lTransaction)

        If BePropietario Is Nothing Then
            Throw New Exception($"#Error: No se encontró el propietario con ID {BeConfigEnc.IdPropietario}")
        End If

        Try
            vHanaService = New SapServiceLayerClient()
            Dim loginResponse As LoginResponseDto = vHanaService.LoginAsync().GetAwaiter().GetResult()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener sesión.")
                Return lDevolucionesProveedor
            Else
                clsPublic.Actualizar_Progreso(lblprg, "Conexión correcta.")
                Debug.WriteLine(vHanaService.SessionCookie)
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Obteniendo devoluciones a proveedor...")

            ' Filtro a nivel de encabezado (no se puede filtrar por WarehouseCode aquí)
            Dim filtroEstado As String = "DocumentStatus eq 'bost_Open'"
            Dim filtroEnviado As String = "U_ENVIADO_WMS eq 2"
            Dim filtroDocNum As String = If(Not String.IsNullOrWhiteSpace(pNoDocumentoSAP), $" and DocNum eq {pNoDocumentoSAP}", "")
            Dim filtroFinal As String = $"{filtroEstado} and {filtroEnviado}{filtroDocNum}"

            Dim url As String = $"{BD.Instancia.HANA_SL}GoodsReturnRequest?$filter={Uri.EscapeDataString(filtroFinal)}"

            Using handler As New HttpClientHandler()
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, sslPolicyErrors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)
                    client.DefaultRequestHeaders.Add("Cookie", vHanaService.SessionCookie)
                    client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

                    Dim response = client.GetAsync(url).GetAwaiter().GetResult()

                    If Not response.IsSuccessStatusCode Then
                        Dim errorDetail = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
                        Throw New Exception($"Error al obtener devoluciones desde SL: {response.StatusCode}, {errorDetail}")
                    End If

                    Dim json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
                    Dim parsed = JObject.Parse(json)

                    For Each devolucion In parsed("value")

                        ' Filtrar por bodega en líneas del documento
                        Dim contieneBodega As Boolean = False
                        For Each linea In devolucion("DocumentLines")
                            If linea("WarehouseCode")?.ToString() = pCodigoBodegaInterface Then
                                contieneBodega = True
                                Exit For
                            End If
                        Next
                        If Not contieneBodega Then Continue For

                        ' Mapeo del documento
                        Dim beDevolucion As New clsBeI_nav_ped_traslado_enc With {
                        .No = devolucion("DocEntry").Value(Of Integer),
                        .Posting_Date = devolucion("DocDate").Value(Of Date),
                        .Receipt_Date = devolucion("DocDate").Value(Of Date),
                        .Shipment_Date = devolucion("DocDate").Value(Of Date),
                        .Status = 1,
                        .Transfer_from_Code = pCodigoBodegaInterface,
                        .Transfer_from_Contact = devolucion("JournalMemo")?.ToString(),
                        .Transfer_to_Contact = devolucion("CardName")?.ToString(),
                        .Transfer_to_CodeField = devolucion("CardCode")?.ToString(),
                        .Transfer_to_Code = devolucion("CardCode")?.ToString(),
                        .Product_Owner_Code = BePropietario.Codigo,
                        .Receipt_Document_Reference = devolucion("DocNum").ToString(),
                        .Company_Code = "",
                        .Comments = devolucion("Comments")?.ToString(),
                        .Document_Type = tTipoDocumentoSalida.Devolucion_Proveedor,
                        .Lineas_Detalle = New List(Of clsBeI_nav_ped_traslado_det)
                    }

                        ' Mapeo de líneas
                        For Each linea In devolucion("DocumentLines")
                            If linea("WarehouseCode")?.ToString() <> pCodigoBodegaInterface Then Continue For

                            Dim beDet As New clsBeI_nav_ped_traslado_det With {
                            .NoEnc = beDevolucion.No,
                            .No = clsLnTrans_pe_det.MaxID() + 1,
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

                            beDevolucion.Lineas_Detalle.Add(beDet)
                        Next

                        If beDevolucion.Lineas_Detalle.Any() Then
                            lDevolucionesProveedor.Add(beDevolucion)
                        End If
                    Next
                End Using
            End Using

            Return lDevolucionesProveedor

        Catch ex As Exception
            Throw New Exception("(SL) Get_Devoluciones_Proveedor_SAP_SL: " & ex.Message, ex)
        End Try
    End Function
    Private Shared Async Function Procesar_Documentos(ByVal codigoBodega As String,
                                                    ByVal pNoDocumento As String,
                                                    ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                    ByVal lblprg As RichTextBox,
                                                    ByVal clsTrans As clsTransaccion) As Task(Of Boolean)

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Conectando a SAP.")

            Dim solicitudes As List(Of clsBeI_nav_ped_traslado_enc) = Get_Devoluciones_Proveedor_SAP_SL(codigoBodega, clsTrans.lConnection, clsTrans.lTransaction, lblprg, pNoDocumento)
            Dim pBePedidoEnc As New clsBeTrans_pe_enc
            Dim PedidoClienteExistenteByCompany As New clsBeTrans_pe_enc
            Dim PedidoClienteExistente As New clsBeTrans_pe_enc

            If solicitudes.Count = 0 Then
                clsPublic.Actualizar_Progreso(lblprg, "No hay documentos para importar.")
                Return False
            End If

            For Each solicitud In solicitudes

                clsPublic.Actualizar_Progreso(lblprg, $"Procesando solicitud de traslado SAP (OWTQ): {solicitud.Receipt_Document_Reference}/{solicitud.No}{vbNewLine}")

                '#MECR 202508080524: Verifica si el proveedor ya existe como cliente en WMS.
                If Await clsSyncSapTrasladosEnvio.Validar_Cliente_WMS(solicitud.Transfer_to_Code, "S", lblprg, clsTrans, vHanaService.SessionCookie, BD.Instancia.HANA_SL) Then

                    Dim origenEsWMS As Boolean = clsLnBodega_area.Existe_Codigo_By_IdBodega(solicitud.Transfer_to_Code, BeConfigEnc.Idbodega, clsTrans.lConnection, clsTrans.lTransaction)
                    Dim destinoEsWMS As Boolean = clsLnBodega_area.Existe_Codigo_By_IdBodega(solicitud.Transfer_to_Code, BeConfigEnc.Idbodega, clsTrans.lConnection, clsTrans.lTransaction)
                    Dim debeProcesar As Boolean = Not destinoEsWMS OrElse Not origenEsWMS OrElse (origenEsWMS AndAlso destinoEsWMS)

                    If debeProcesar Then

                        Dim pedidoEnc As clsBeTrans_pe_enc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia_If(solicitud, lblprg, clsTrans.lConnection, clsTrans.lTransaction)

                        Dim trasladoSincronizado As Boolean = Marcar_Devolucion_Proveedor_Sincronizada_SLAsync(solicitud.No, vHanaService.SessionCookie, BD.Instancia.HANA_SL).GetAwaiter().GetResult()

                        If pedidoEnc IsNot Nothing AndAlso trasladoSincronizado Then
                            Return True
                        End If
                    End If

                End If

            Next

            Return False

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Private Shared Async Function Marcar_Devolucion_Proveedor_Sincronizada_SLAsync(docEntry As String,
                                                                               sessionCookie As String,
                                                                               baseUrl As String) As Task(Of Boolean)

        Try
            If String.IsNullOrWhiteSpace(docEntry) Then Return False

            Dim requestUrl As String = $"GoodsReturnRequest({docEntry})"
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
                            Throw New Exception($"Error al actualizar PurchaseReturns. Código: {response.StatusCode}, Detalle: {errContent}")
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception($"(SL) {MethodBase.GetCurrentMethod().Name} {ex.Message}", ex)
        End Try

    End Function

    Public Shared Sub Enviar_Transacciones_De_Salida(ByRef lblprg As RichTextBox,
                                                 ByRef prg As ProgressBar,
                                                 ByVal pTipo As tTipoDocumentoSalida)

        Dim lTransaccionesSalida As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesSalidaSingle As New List(Of clsBeI_nav_transacciones_out)

        Dim lTransaccionesSalidaReproceso As New List(Of clsBeI_nav_transacciones_out)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim vContadorReproceso As Integer = 0
        Dim lTransPtPendienteRegistroEnNav As New List(Of clsBeTrans_pe_enc)
        Dim BePedidoEnc As New clsBeTrans_pe_enc

        Dim sw As New Stopwatch()

        Try
            ' Inicio y anuncio
            sw.Start()
            clsPublic.Actualizar_Progreso(lblprg, "Iniciando envío de transacciones de salida...")

            CnnLog.Open()

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(pTipo)

            If lTransaccionesSalida IsNot Nothing AndAlso lTransaccionesSalida.Count > 0 Then

                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc})

                Dim Enviado_A_Erp As Boolean = False

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Documentos a enviar: {0}", ListaPedidosTransf.Count))

                For Each PT In ListaPedidosTransf

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Solicitud de Devolución: {0}-{1}", PT.Idpedidoenc, PT.No_pedido))

                    Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP_By_Referencia_TipoDoc(PT.No_pedido, pTipo)

                    If Not Enviado_A_Erp Then

                        lTransaccionesSalidaSingle = lTransaccionesSalida.FindAll(Function(x) x.No_pedido = PT.No_pedido)

                        BePedidoEnc = clsLnTrans_pe_enc.GetSingle(PT.Idpedidoenc)

                        If Crear_Devolucion_Desde_Solicitud_Aprobada(PT.No_pedido,
                                                                     lTransaccionesSalidaSingle,
                                                                     BePedidoEnc,
                                                                     lblprg,
                                                                     prg) Then

                            Try
                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones de salida enviadas correctamente: {0}", lTransaccionesSalida.Count))
                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)
                            Catch ex As Exception
                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))
                                clsLnLog_error_wms.Agregar_Error(ex.Message)
                            End Try

                        End If

                    End If

                Next

                ' Fin OK
                sw.Stop()
                clsPublic.Actualizar_Progreso(lblprg, $"Proceso finalizado. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.")

            Else
                ' No hay pendientes
                sw.Stop()
                clsPublic.Actualizar_Progreso(lblprg, $"MSG_240117: No hay transacciones para enviar. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.")
            End If

        Catch ex As Exception
            If sw.IsRunning Then sw.Stop()
            clsPublic.Actualizar_Progreso(lblprg, String.Format("{0} {1}. Tiempo transcurrido: {2:F2} segundos.",
                                                            MethodBase.GetCurrentMethod.Name(),
                                                            ex.Message,
                                                            sw.Elapsed.TotalSeconds))
        Finally
            prg.Value = 0
            prg.Visible = False
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Sub

    <Serializable>
    Private Class GoodsReturnDto
        Public Property CardCode As String
        Public Property DocDate As Date
        Public Property DocDueDate As Date
        Public Property Comments As String = ""
        Public Property U_USR_PICK As String = ""
        Public Property U_DOCUMENTO_WMS As Integer = 0
        Public Property U_INICIO_PICK As DateTime = Now
        Public Property U_FIN_PICK As DateTime = Now
        Public Property U_ESTADO_PEDIDO As Integer = 0
        Public Property U_INICIO_ENVIO As DateTime = Now
        Public Property U_FIN_ENVIO As DateTime = Now
        Public Property U_ENVIADO_WMS As Integer = 1
        Public Property U_ENVIADO_SAP_WMS As String = ""
        Public Property DocumentLines As List(Of GoodsReturnLineDto)
    End Class

    <Serializable>
    Private Class GoodsReturnLineDto
        Public Property BaseType As Integer?
        Public Property BaseEntry As Integer?
        Public Property BaseLine As Integer?
        Public Property ItemCode As String
        Public Property Quantity As Decimal
        Public Property WarehouseCode As String
        Public Property U_Color As String
        Public Property U_Talla As String
        Public Property BatchNumbers As List(Of BatchNumberDto)
    End Class

    <Serializable>
    Private Class BatchNumberDto
        Public Property BatchNumber As String
        Public Property Quantity As Decimal
    End Class

    Private Const ENTITY_TARGET As String = "PurchaseReturns"         ' Entidad a crear en Service Layer
    Private Const BASETYPE_GOODS_RETURN_REQUEST As Integer = 234000032 ' BaseType de Solicitud Devolución

    Private ReadOnly JsonSettings As New JsonSerializerSettings With {
        .NullValueHandling = NullValueHandling.Ignore
    }
    Private Shared Function Crear_Devolucion_Desde_Solicitud_Aprobada(no_pedido As String,
                                                                       lTransaccionesSalidaSingle As List(Of clsBeI_nav_transacciones_out),
                                                                       bePedidoEnc As clsBeTrans_pe_enc,
                                                                       lblprg As RichTextBox,
                                                                       prg As ProgressBar) As Boolean
        Try
            If String.IsNullOrWhiteSpace(no_pedido) Then Throw New Exception("El parámetro no_pedido está vacío.")
            If lTransaccionesSalidaSingle Is Nothing OrElse lTransaccionesSalidaSingle.Count = 0 Then
                clsPublic.Actualizar_Progreso(lblprg, "No hay transacciones para procesar.")
                Return False
            End If

            Dim baseUrl As String = BD.Instancia.HANA_SL   ' Ej: "https://servidor:50000/b1s/v1/"
            If String.IsNullOrWhiteSpace(baseUrl) Then Throw New Exception("No se encontró BD.Instancia.HANA_SL.")

            ' -------------------------
            ' Login via SapServiceLayerClient
            ' -------------------------
            Dim vHanaService As SapServiceLayerClient = New SapServiceLayerClient()
            Dim loginResponse As LoginResponseDto = vHanaService.LoginAsync().GetAwaiter().GetResult()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener sesión.")
                Return False
            Else
                clsPublic.Actualizar_Progreso(lblprg, "Conexión correcta.")
                Debug.WriteLine(vHanaService.SessionCookie) ' Debe contener "B1SESSION=...; ROUTEID=..."
            End If

            ' -------------------------
            ' Progreso UI
            ' -------------------------
            prg.Visible = True
            prg.Minimum = 0
            prg.Maximum = lTransaccionesSalidaSingle.Count
            prg.Value = 0

            ' -------------------------
            ' Encabezado GoodsReturn
            ' -------------------------
            Dim docEntrySolicitud As Integer = CInt(no_pedido)

            Dim vOperadorPickingDefecto As String = clsLnTrans_picking_ubic.Get_Operador_Defecto_By_IdPickingEnc(bePedidoEnc.Picking.IdPickingEnc)

            Dim devolucion As New GoodsReturnDto With {
                .CardCode = If(bePedidoEnc IsNot Nothing AndAlso bePedidoEnc.Cliente IsNot Nothing, bePedidoEnc.Cliente.Codigo, Nothing),
                .DocDate = Date.Today,
                .DocDueDate = Date.Today,
                .Comments = $"Devolución generada por WMS sobre Solicitud SAP: {no_pedido} - Pedido WMS: {If(bePedidoEnc IsNot Nothing, bePedidoEnc.IdPedidoEnc.ToString(), "")}",
                .U_USR_PICK = vOperadorPickingDefecto,
                .U_ENVIADO_WMS = 2,
                .U_DOCUMENTO_WMS = bePedidoEnc.IdPedidoEnc,
                .U_INICIO_PICK = bePedidoEnc.Picking.Hora_ini.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                .U_FIN_PICK = bePedidoEnc.Picking.Hora_fin.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                .U_INICIO_ENVIO = Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                .U_FIN_ENVIO = Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                .U_ENVIADO_SAP_WMS = FormatoFechas.tFechaHoraSAP(Now),
                .DocumentLines = New List(Of GoodsReturnLineDto)()
            }

            ' -------------------------
            ' Agrupar por Producto + Línea + IdProductoTallaColor
            ' -------------------------
            Dim grupos = lTransaccionesSalidaSingle.
                GroupBy(Function(x) New With {
                    Key .Codigo_producto = x.Codigo_producto,
                    Key .No_linea = x.No_linea,
                    Key .IdProductoTallaColor = x.IdProductoTallaColor
                }).
                Select(Function(g) New With {
                    g.Key.Codigo_producto,
                    g.Key.No_linea,
                    g.Key.IdProductoTallaColor,
                    .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                }).ToList()

            Dim listaActualizar As New List(Of clsBeI_nav_transacciones_out)
            Dim contador As Integer = 0

            For Each g In grupos
                contador += 1
                prg.Value = Math.Min(prg.Maximum, contador)
                clsPublic.Actualizar_Progreso(lblprg, $"Procesando: {g.Codigo_producto} - Línea {g.No_linea} - PTC {g.IdProductoTallaColor} - Cant: {g.Cantidad_Total}")

                ' -------------------------
                ' Obtener Talla / Color
                ' -------------------------
                Dim vColor As String = ""
                Dim vTalla As String = ""
                Try
                    Dim dt As DataTable = clsLnProducto_talla_color.Get_Single_Dt_By_IdProductoTallaColor(g.IdProductoTallaColor)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        vColor = If(dt.Rows(0)("Color") IsNot DBNull.Value, dt.Rows(0)("Color").ToString(), "")
                        vTalla = If(dt.Rows(0)("Talla") IsNot DBNull.Value, dt.Rows(0)("Talla").ToString(), "")
                    End If
                Catch ex As Exception
                    clsPublic.Actualizar_Progreso(lblprg, $"Aviso: No se pudo leer talla/color para PTC {g.IdProductoTallaColor}. {ex.Message}")
                End Try

                ' -------------------------
                ' Batch numbers (si corresponde)
                ' -------------------------
                Dim batchList As New List(Of BatchNumberDto)
                If g.Cantidad_Total > 0D Then
                    batchList.Add(New BatchNumberDto With {
                        .BatchNumber = $"{vColor}{vTalla}",
                        .Quantity = g.Cantidad_Total
                    })
                End If

                ' -------------------------
                ' WarehouseCode:
                ' Si tus transacciones tienen bodega por línea, puedes derivarla aquí; si no,
                ' al estar referenciando BaseEntry/Line, SAP toma de la solicitud.
                ' -------------------------
                Dim whs As String = Nothing
                ' Ejemplo (descomenta si lo tienes en tu entidad):
                'whs = lTransaccionesSalidaSingle.FirstOrDefault(Function(x) x.Codigo_producto = g.Codigo_producto AndAlso x.No_linea = g.No_linea AndAlso x.IdProductoTallaColor = g.IdProductoTallaColor)?.WhsCode

                ' -------------------------
                ' Agregar línea referenciada a la solicitud
                ' -------------------------
                devolucion.DocumentLines.Add(New GoodsReturnLineDto With {
                    .BaseType = BASETYPE_GOODS_RETURN_REQUEST,
                    .BaseEntry = docEntrySolicitud,
                    .BaseLine = g.No_linea,
                    .ItemCode = g.Codigo_producto,
                    .Quantity = g.Cantidad_Total,
                    .U_Color = vColor,
                    .U_Talla = vTalla,
                    .BatchNumbers = batchList
                })

                ' Preparar marcación de enviados en WMS
                Dim subLista = lTransaccionesSalidaSingle.Where(Function(x) x.No_linea = g.No_linea AndAlso
                                                                       x.Codigo_producto = g.Codigo_producto AndAlso
                                                                       x.IdProductoTallaColor = g.IdProductoTallaColor AndAlso
                                                                       Not x.Enviado).ToList()
                If subLista IsNot Nothing AndAlso subLista.Count > 0 Then
                    listaActualizar.AddRange(subLista)
                End If
            Next

            ' -------------------------
            ' POST al Service Layer
            ' -------------------------
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
            ServicePointManager.Expect100Continue = False
            ServicePointManager.FindServicePoint(New Uri(baseUrl)).ConnectionLeaseTimeout = 0

            Dim creado As Boolean = False

            Dim handler As New HttpClientHandler With {
                .AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate,
                .ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True,
                .UseCookies = False
            }

            Using httpClient As New HttpClient(handler) With {.BaseAddress = New Uri(baseUrl)}
                Dim json As String = JsonConvert.SerializeObject(devolucion)
                Dim content = New StringContent(json, Encoding.UTF8)
                Dim mediaType = New Headers.MediaTypeHeaderValue("application/json")
                mediaType.CharSet = "utf-8"
                content.Headers.ContentType = mediaType

                Dim req As New HttpRequestMessage(HttpMethod.Post, ENTITY_TARGET) With {.Content = content}
                ' Usa la cookie de sesión que ya maneja tu cliente
                req.Headers.Add("Cookie", vHanaService.SessionCookie)
                req.Headers.ConnectionClose = True

                Dim resp = httpClient.SendAsync(req).GetAwaiter().GetResult()
                Dim body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult()

                If resp.IsSuccessStatusCode Then

                    Dim docEntryTransferPrimary As Integer = 0
                    Dim docNumTransferPrimary As Integer = 0
                    ' Parsear el JSON
                    Dim jsonObj As JObject = JObject.Parse(body)

                    ' Capturar los valores
                    docEntryTransferPrimary = jsonObj("DocEntry")
                    docNumTransferPrimary = jsonObj("DocNum")

                    creado = True
                    clsPublic.Actualizar_Progreso(lblprg, $"✅ Devolución creada en SAP B1 DocNum {docNumTransferPrimary}")
                Else
                    clsPublic.Actualizar_Progreso(lblprg, $"❌ Error SL {resp.StatusCode}:")
                    clsPublic.Actualizar_Progreso(lblprg, body)
                End If
            End Using

            ' -------------------------
            ' Marcar enviados en WMS
            ' -------------------------
            If creado AndAlso listaActualizar.Count > 0 Then
                Dim marcados = clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(listaActualizar)
                If marcados = 0 Then
                    clsPublic.Actualizar_Progreso(lblprg, "⚠️ La devolución se creó en SAP, pero no se marcaron como enviadas en WMS.")
                End If
            End If

            Return creado

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, $"❌ Error al crear la devolución sobre la solicitud: {ex.Message}")
            Return False
        End Try

    End Function


End Class
