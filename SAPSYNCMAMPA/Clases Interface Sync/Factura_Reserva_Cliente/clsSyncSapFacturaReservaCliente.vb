Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Text
Imports DevExpress.Data.Filtering.Helpers.SubExprHelper.ThreadHoppingFiltering
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports TOMWMS.clsDataContractDI

Public Class clsSyncSapFacturaReservaCliente : Inherits clsInterfaceBase
    Private Shared vHanaService As SapServiceLayerClient
    Private Const ENTITY_TARGET_DELIVERY_NOTE As String = "DeliveryNotes"
    Private Const BASETYPE_AR_RESERVE_INVOICE As Integer = 13 ' OINV/ODPI (Factura de reserva cliente)

    Public Shared Async Function Procesar_Facturas_de_Reserva_Cliente_SAP(ByVal lblprg As RichTextBox,
                                                                          ByVal prg As ProgressBar,
                                                                          Optional ByVal pNoDocumento As String = "") As Task(Of Boolean)
        Dim clsTrans As New clsTransaccion
        Dim sw As New Stopwatch()

        Try
            ' Inicia cronómetro y anuncia inicio
            sw.Start()
            clsPublic.Actualizar_Progreso(lblprg, "Iniciando proceso de sincronización de facturas de Reserva_Cliente desde SAP.")

            clsTrans.Begin_Transaction()

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                      clsTrans.lConnection,
                                                      clsTrans.lTransaction)

            Dim sessionCookie As String = ""
            Dim baseUrl As String = BD.Instancia.HANA_SL
            Dim BeBodega As clsBeBodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega,
                                                                        clsTrans.lConnection,
                                                                        clsTrans.lTransaction)

            clsTrans.Commit_Transaction()

            If BeBodega Is Nothing Then
                Throw New Exception("ERROR_202311271751: Error no se pudo obtener el objeto de bodega asociado a la configuración de interface: " & BeConfigEnc.Idbodega)
            End If

            Await Procesar_Documentos(BeBodega.Codigo,
                                      pNoDocumento,
                                      BeConfigEnc,
                                      lblprg)

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
            clsPublic.Actualizar_Progreso(lblprg, $"Fin del proceso de sincronización de las facturas de Reserva_Cliente desde SAP. Tiempo total: {sw.Elapsed.TotalSeconds:F2} segundos.")
        End Try

    End Function

    'Private Shared Function Get_Factura_Reserva_Cliente_SAP_SL(pCodigoBodegaInterface As String,
    '                                                           lConnection As SqlConnection,
    '                                                           lTransaction As SqlTransaction,
    '                                                           lblprg As RichTextBox,
    '                                                           Optional pNoDocumentoSAP As String = "") As List(Of clsBeI_nav_ped_traslado_enc)

    '    Dim lFacturasReserva_Cliente As New List(Of clsBeI_nav_ped_traslado_enc)
    '    Dim BePropietario As clsBePropietarios = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario, lConnection, lTransaction)

    '    If BePropietario Is Nothing Then
    '        Throw New Exception($"#Error: No se encontró el propietario con ID {BeConfigEnc.IdPropietario}")
    '    End If

    '    Try
    '        vHanaService = New SapServiceLayerClient()
    '        Dim loginResponse As LoginResponseDto = vHanaService.LoginAsync().GetAwaiter().GetResult()

    '        If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
    '            clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener sesión.")
    '            Return lFacturasReserva_Cliente
    '        Else
    '            clsPublic.Actualizar_Progreso(lblprg, "Conexión correcta.")
    '            Debug.WriteLine(vHanaService.SessionCookie)
    '        End If

    '        clsPublic.Actualizar_Progreso(lblprg, "Obteniendo facturas de Reserva_Cliente...")

    '        ' Filtro a nivel de encabezado (no se puede filtrar por WarehouseCode aquí)
    '        Dim filtroFacturaReserva_Cliente As String = "ReserveInvoice eq 'tYES'"
    '        'Dim filtroEstado As String = "DocumentStatus eq 'bost_Close'"
    '        Dim filtroEnviado As String = "U_ENVIADO_WMS eq 2"
    '        Dim filtroDocNum As String = If(Not String.IsNullOrWhiteSpace(pNoDocumentoSAP), $" and DocNum eq {pNoDocumentoSAP}", "")
    '        Dim filtroFinal As String = $"{filtroFacturaReserva_Cliente} and {filtroEnviado}{filtroDocNum}"

    '        Dim url As String = $"{BD.Instancia.HANA_SL}Invoices?$filter={Uri.EscapeDataString(filtroFinal)}"

    '        Using handler As New HttpClientHandler()
    '            handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, sslPolicyErrors) True
    '            handler.UseCookies = False

    '            Using client As New HttpClient(handler)
    '                client.DefaultRequestHeaders.Add("Cookie", vHanaService.SessionCookie)
    '                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

    '                Dim response = client.GetAsync(url).GetAwaiter().GetResult()

    '                If Not response.IsSuccessStatusCode Then
    '                    Dim errorDetail = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
    '                    Throw New Exception($"Error al obtener facturas de Reserva_Cliente desde SL: {response.StatusCode}, {errorDetail}")
    '                End If

    '                Dim json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
    '                Dim parsed = JObject.Parse(json)

    '                For Each factura_Reserva_Cliente In parsed("value")

    '                    ' Filtrar por bodega en líneas del documento
    '                    Dim contieneBodega As Boolean = False
    '                    For Each linea In factura_Reserva_Cliente("DocumentLines")
    '                        If linea("WarehouseCode")?.ToString() = pCodigoBodegaInterface Then
    '                            contieneBodega = True
    '                            Exit For
    '                        End If
    '                    Next

    '                    If Not contieneBodega Then Continue For

    '                    ' Mapeo del documento
    '                    Dim beFacturaReserva_Cliente As New clsBeI_nav_ped_traslado_enc With {
    '                    .No = factura_Reserva_Cliente("DocEntry").Value(Of Integer),
    '                    .Posting_Date = factura_Reserva_Cliente("DocDate").Value(Of Date),
    '                    .Receipt_Date = factura_Reserva_Cliente("DocDate").Value(Of Date),
    '                    .Shipment_Date = factura_Reserva_Cliente("DocDate").Value(Of Date),
    '                    .Status = 1,
    '                    .Transfer_from_Code = pCodigoBodegaInterface,
    '                    .Transfer_from_Contact = factura_Reserva_Cliente("JournalMemo")?.ToString(),
    '                    .Transfer_to_Contact = factura_Reserva_Cliente("CardName")?.ToString(),
    '                    .Transfer_to_CodeField = factura_Reserva_Cliente("CardCode")?.ToString(),
    '                    .Transfer_to_Code = factura_Reserva_Cliente("CardCode")?.ToString(),
    '                    .Product_Owner_Code = BePropietario.Codigo,
    '                    .Receipt_Document_Reference = factura_Reserva_Cliente("DocNum").ToString(),
    '                    .Company_Code = "",
    '                    .Comments = factura_Reserva_Cliente("Comments")?.ToString(),
    '                    .Document_Type = tTipoDocumentoSalida.Pedido_De_Cliente,
    '                    .Transportation_Guide = factura_Reserva_Cliente("U_Guia")?.ToString(),
    '                    .Lineas_Detalle = New List(Of clsBeI_nav_ped_traslado_det)
    '                }

    '                    ' Crear cache una sola vez por petición
    '                    Dim cache As New OitmCache(client, BD.Instancia.HANA_SL)

    '                    Dim lineas = factura_Reserva_Cliente("DocumentLines").ToList

    '                    ' Mapeo de líneas
    '                    For Each linea In lineas
    '                        If linea("WarehouseCode")?.ToString() <> pCodigoBodegaInterface Then Continue For

    '                        Dim itemCode As String = linea("ItemCode")?.ToString()

    '                        ' Si no hay ItemCode, normalmente es servicio/texto -> excluir del WMS
    '                        If String.IsNullOrWhiteSpace(itemCode) Then Continue For

    '                        ' Obtener U_Grupo desde OITM (con cache)
    '                        Dim grupo As String = cache.GetUGrupoAsync(itemCode).GetAwaiter().GetResult()

    '                        ' Excluir grupo 19
    '                        If Not String.IsNullOrEmpty(grupo) AndAlso grupo = "19" Then Continue For

    '                        Dim beDet As New clsBeI_nav_ped_traslado_det With {
    '                                                                .NoEnc = beFacturaReserva_Cliente.No,
    '                                                                .No = clsLnTrans_pe_det.MaxID() + 1,
    '                                                                .Item_No = linea("ItemCode")?.ToString(),
    '                                                                .Line_No = linea("LineNum").Value(Of Integer),
    '                                                                .Shipment_Date = Date.Now,
    '                                                                .Quantity = linea("Quantity").Value(Of Decimal),
    '                                                                .Description = linea("ItemDescription")?.ToString(),
    '                                                                .Unit_of_Measure_Code = linea("MeasureUnit")?.ToString(),
    '                                                                .Status = 1,
    '                                                                .Transfer_to_CodeField = linea("WarehouseCode")?.ToString(),
    '                                                                .Price = linea("Price").Value(Of Double),
    '                                                                .Color = linea("U_Color")?.ToString(),
    '                                                                .Size = linea("U_Talla")?.ToString(),
    '                                                                .Variant_Code = Nothing
    '                                                            }

    '                        beFacturaReserva_Cliente.Lineas_Detalle.Add(beDet)
    '                    Next

    '                Next

    '            End Using

    '        End Using

    '        Return lFacturasReserva_Cliente

    '    Catch ex As Exception
    '        Throw New Exception("(SL) Get_Factura_Reserva_Cliente_SAP_SL: " & ex.Message, ex)
    '    End Try
    'End Function

    Private Shared Async Function Get_Factura_Reserva_Cliente_SAP_SLAsync(pCodigoBodegaInterface As String,
                                                                          lConnection As SqlConnection,
                                                                          lTransaction As SqlTransaction,
                                                                          lblprg As RichTextBox,
                                                                          Optional pNoDocumentoSAP As String = "") As Task(Of List(Of clsBeI_nav_ped_traslado_enc))

        Dim lFacturasReserva_Cliente As New List(Of clsBeI_nav_ped_traslado_enc)

        Dim BePropietario As clsBePropietarios =
        clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario, lConnection, lTransaction)

        If BePropietario Is Nothing Then
            Throw New Exception($"#Error: No se encontró el propietario con ID {BeConfigEnc.IdPropietario}")
        End If

        Try
            vHanaService = New SapServiceLayerClient()

            ' Login async (NO bloquear con GetResult)
            Dim loginResponse As LoginResponseDto = Await vHanaService.LoginAsync().ConfigureAwait(False)

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener sesión.")
                Return lFacturasReserva_Cliente
            Else
                clsPublic.Actualizar_Progreso(lblprg, "Conexión correcta.")
                Debug.WriteLine(vHanaService.SessionCookie)
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Obteniendo facturas de Reserva_Cliente...")

            ' Filtro encabezado
            Dim filtroFacturaReserva_Cliente As String = "ReserveInvoice eq 'tYES'"
            Dim filtroGuia As String = "U_Guia ne null and U_Guia ne ''"
            Dim filtroEnviado As String = "U_ENVIADO_WMS eq 2"
            Dim filtroDocNum As String = If(Not String.IsNullOrWhiteSpace(pNoDocumentoSAP), $" and DocNum eq {pNoDocumentoSAP}", "")
            Dim filtroFinal As String = $"{filtroFacturaReserva_Cliente} and {filtroEnviado}{filtroDocNum} and {filtroGuia}"

            Dim url As String = $"{BD.Instancia.HANA_SL}Invoices?$filter={Uri.EscapeDataString(filtroFinal)}"

            Using handler As New HttpClientHandler()
                handler.ServerCertificateCustomValidationCallback =
                Function(sender, cert, chain, sslPolicyErrors) True
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
                        $"Error al obtener facturas de Reserva_Cliente desde SL: {response.StatusCode}, {errorDetail}"
                    )
                    End If

                    Dim json As String = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                    Dim parsed As JObject = JObject.Parse(json)

                    Dim valueArr = TryCast(parsed("value"), JArray)
                    If valueArr Is Nothing OrElse valueArr.Count = 0 Then
                        Return lFacturasReserva_Cliente
                    End If

                    ' Cache OITM: crear UNA vez para todas las facturas
                    Dim cache As New OitmCache(client, BD.Instancia.HANA_SL)

                    ' Si necesitas IDs únicos sin recalcular MaxID en cada línea
                    Dim nextDetId As Integer = 0

                    For Each factura_Reserva_Cliente As JObject In valueArr

                        Dim docLines = TryCast(factura_Reserva_Cliente("DocumentLines"), JArray)
                        If docLines Is Nothing OrElse docLines.Count = 0 Then Continue For

                        ' Filtrar por bodega
                        Dim contieneBodega As Boolean = docLines.Any(
                        Function(l) l?("WarehouseCode")?.ToString() = pCodigoBodegaInterface
                    )
                        If Not contieneBodega Then Continue For

                        ' Mapeo encabezado
                        Dim beFacturaReserva_Cliente As New clsBeI_nav_ped_traslado_enc With {
                        .No = factura_Reserva_Cliente("DocEntry").Value(Of Integer),
                        .Posting_Date = factura_Reserva_Cliente("DocDate").Value(Of Date),
                        .Receipt_Date = factura_Reserva_Cliente("DocDate").Value(Of Date),
                        .Shipment_Date = factura_Reserva_Cliente("DocDate").Value(Of Date),
                        .Status = 1,
                        .Transfer_from_Code = pCodigoBodegaInterface,
                        .Transfer_from_Contact = factura_Reserva_Cliente("JournalMemo")?.ToString(),
                        .Transfer_to_Contact = factura_Reserva_Cliente("CardName")?.ToString(),
                        .Transfer_to_CodeField = factura_Reserva_Cliente("CardCode")?.ToString(),
                        .Transfer_to_Code = factura_Reserva_Cliente("CardCode")?.ToString(),
                        .Product_Owner_Code = BePropietario.Codigo,
                        .Receipt_Document_Reference = factura_Reserva_Cliente("DocNum").ToString(),
                        .Company_Code = "",
                        .Comments = factura_Reserva_Cliente("Comments")?.ToString(),
                        .Document_Type = tTipoDocumentoSalida.Factura_Reserva_Cliente,
                        .Transportation_Guide = factura_Reserva_Cliente("U_Guia")?.ToString(),
                        .Transport_Company = factura_Reserva_Cliente("U_TIPO_GUIA")?.ToString(),
                        .Lineas_Detalle = New List(Of clsBeI_nav_ped_traslado_det)
                    }

                        ' Mapeo líneas (filtra: bodega + NO servicio via U_Grupo (OITM))
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
                            .NoEnc = beFacturaReserva_Cliente.No,
                            .No = nextDetId,
                            .Item_No = itemCode,
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

                            nextDetId += 1
                            beFacturaReserva_Cliente.Lineas_Detalle.Add(beDet)
                        Next

                        If beFacturaReserva_Cliente.Lineas_Detalle.Any() Then
                            lFacturasReserva_Cliente.Add(beFacturaReserva_Cliente)
                        End If

                    Next

                End Using
            End Using

            Return lFacturasReserva_Cliente

        Catch ex As Exception
            Throw New Exception("(SL) Get_Factura_Reserva_Cliente_SAP_SLAsync: " & ex.Message, ex)
        End Try
    End Function

    Private Shared Async Function Procesar_Documentos(ByVal codigoBodega As String,
                                                  ByVal pNoDocumento As String,
                                                  ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                  ByVal lblprg As RichTextBox) As Task(Of Boolean)

        Dim procesoExitoso As Boolean = False
        Dim clsTrans As New clsTransaccion

        Try
            clsPublic.Actualizar_Progreso(lblprg, "Conectando a SAP.")

            clsTrans.Begin_Transaction()

            Dim facturas As List(Of clsBeI_nav_ped_traslado_enc) =
            Await Get_Factura_Reserva_Cliente_SAP_SLAsync(codigoBodega,
                                                          clsTrans.lConnection,
                                                          clsTrans.lTransaction,
                                                          lblprg,
                                                          pNoDocumento)

            clsTrans.Commit_Transaction()
            clsTrans.Close_Conection()

            If facturas Is Nothing OrElse facturas.Count = 0 Then
                clsPublic.Actualizar_Progreso(lblprg, "No hay documentos para importar.")
                Return False
            End If

            For Each factura In facturas

                Try

                    clsTrans.Begin_Transaction()

                    clsPublic.Actualizar_Progreso(lblprg, $"Procesando factura de Reserva_Cliente de SAP (OINV): {factura.Receipt_Document_Reference}/{factura.No}{vbNewLine}")

                    Dim clienteValido As Boolean =
                    Await clsSyncSapTrasladosEnvio.Validar_Cliente_WMS(factura.Transfer_to_Code,
                                                                       "C",
                                                                       lblprg,
                                                                       clsTrans,
                                                                       vHanaService.SessionCookie,
                                                                       BD.Instancia.HANA_SL)

                    If Not clienteValido Then
                        clsPublic.Actualizar_Progreso(lblprg, $"Cliente no válido en WMS para documento: {factura.No}{vbNewLine}")

                        clsTrans.RollBack_Transaction()
                        Continue For
                    End If

                    Dim pedidoEnc As clsBeTrans_pe_enc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia(factura,
                                                                                                                                lblprg,
                                                                                                                                clsTrans.lConnection,
                                                                                                                                clsTrans.lTransaction)

                    If pedidoEnc Is Nothing Then
                        clsPublic.Actualizar_Progreso(lblprg, $"No se pudo importar el pedido para documento: {factura.No}{vbNewLine}")

                        clsTrans.RollBack_Transaction()
                        Continue For
                    End If

                    Dim pickingCreado As Boolean = clsLnI_nav_ped_traslado_enc.Nuevo_Picking(pedidoEnc,
                                                                                             clsTrans.lConnection,
                                                                                             clsTrans.lTransaction)

                    If Not pickingCreado Then
                        clsPublic.Actualizar_Progreso(lblprg, $"No se pudo crear picking para documento: {factura.No}{vbNewLine}")

                        clsTrans.RollBack_Transaction()
                        Continue For
                    End If

                    clsPublic.Actualizar_Progreso(lblprg,
                    String.Format("Picking creado para el documento: {0}/{1}{2}",
                                  pedidoEnc.Referencia,
                                  pedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino,
                                  vbNewLine))

                    Dim trasladoSincronizado As Boolean =
                    Await Marcar_Factura_Reserva_Cliente_Sincronizada_SLAsync(factura.No,
                                                                              vHanaService.SessionCookie,
                                                                              BD.Instancia.HANA_SL,
                                                                              2)

                    If Not trasladoSincronizado Then
                        clsPublic.Actualizar_Progreso(lblprg, $"No se pudo marcar como sincronizado en SAP el documento: {factura.No}{vbNewLine}")

                        clsTrans.RollBack_Transaction()
                        Continue For
                    End If

                    clsTrans.Commit_Transaction()

                    procesoExitoso = True

                    clsPublic.Actualizar_Progreso(lblprg, $"Documento procesado correctamente: {factura.No}{vbNewLine}")

                Catch exDoc As Exception

                    Try
                        clsTrans.RollBack_Transaction()
                    Catch
                    End Try

                    clsPublic.Actualizar_Progreso(lblprg, $"Error procesando documento {factura.No}: {exDoc.Message}{vbNewLine}")

                    ' Continúa con el siguiente documento
                    Continue For

                End Try

            Next

            Return procesoExitoso

        Catch ex As Exception
            Throw
        End Try

    End Function
    Public Shared Async Function Marcar_Factura_Reserva_Cliente_Sincronizada_SLAsync(docEntry As String,
                                                                                     sessionCookie As String,
                                                                                     baseUrl As String,
                                                                                     enviado As Integer) As Task(Of Boolean)

        Try
            If String.IsNullOrWhiteSpace(docEntry) Then Return False

            Dim requestUrl As String = $"Invoices({docEntry})"
            Dim payload As String = $"{{""U_ENVIADO_WMS"": ""{enviado}""}}"
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

    Public Shared Async Function Enviar_Factura_Reserva_ClienteAsync(pIdBodega As Integer,
                                                                     lblprg As RichTextBox,
                                                                     prg As Windows.Forms.ProgressBar) As Task

        Dim lTransaccionesSalida As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesSalidaSingle As New List(Of clsBeI_nav_transacciones_out)

        Dim lTransaccionesSalidaReproceso As New List(Of clsBeI_nav_transacciones_out)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim Sl As New clsSyncLotes()
        Dim vContadorReproceso As Integer = 0
        Dim lTransPtPendienteRegistroEnNav As New List(Of clsBeTrans_pe_enc)
        Dim clsTrans As New clsTransaccion()

        Try

            CnnLog.Open()
            clsTrans.Begin_Transaction()

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(clsDataContractDI.tTipoDocumentoSalida.Factura_Reserva_Cliente)

            If Not lTransaccionesSalida Is Nothing AndAlso lTransaccionesSalida.Count > 0 Then

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones a enviar: {0}", lTransaccionesSalida.Count))

                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc, Key i.Idbodega} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc, Key Keys.Idbodega}).
                                          Where(Function(x) x.Idbodega = pIdBodega).ToList()

                Dim Enviado_A_Erp As Boolean = False

                For Each PT In ListaPedidosTransf

                    Dim BePedidoEnc As clsBeTrans_pe_enc =
                    clsLnTrans_pe_enc.GetSingle(PT.Idpedidoenc, clsTrans.lConnection, clsTrans.lTransaction)

                    Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP(PT.No_pedido)

                    If Not Enviado_A_Erp Then

                        lTransaccionesSalidaSingle = lTransaccionesSalida.FindAll(Function(x) x.No_pedido = PT.No_pedido)

                        Dim enviadoOk As Boolean = Await Enviar_Entrega_Mercancia(PT.No_pedido,
                                                                                  BePedidoEnc,
                                                                                  lTransaccionesSalidaSingle,
                                                                                  clsTrans,
                                                                                  lblprg).ConfigureAwait(False)

                        If enviadoOk Then

                            Try

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Facturas de reserva de cliente enviadas correctamente: {0}", lTransaccionesSalida.Count))

                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)

                            Catch ex As Exception

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al registrar la factura de reserva de cliente:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar la factura de reserva de cliente:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message),
                                                                           PT.No_pedido,
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet,
                                                                           CnnLog)

                            End Try

                        End If

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
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

    Private Shared Async Function Enviar_Entrega_Mercancia(ByVal _DocEntry As Integer,
                                                           ByVal BePedidoEnc As clsBeTrans_pe_enc,
                                                           ByVal transaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                           ByVal clsTrans As clsTransaccion,
                                                           ByVal lblprg As RichTextBox) As Task(Of Boolean)


        ' Progreso básico
        If transaccionesOut Is Nothing OrElse transaccionesOut.Count = 0 Then
            clsPublic.Actualizar_Progreso(lblprg, "No hay transacciones para procesar.")
            Return False
        End If

        Dim vTraslado_Creado As Boolean = False
        Dim vSolicitud_Creada As Boolean = False
        Dim vDebeGenerarSolicitud As Boolean = False
        Dim BePedidoRef As New clsBeTrans_pe_ref_mi3

        Try

            Dim vHanaService As New SapServiceLayerClient()
            Dim loginResponse As LoginResponseDto = Await vHanaService.LoginAsync()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener sesión.")
                Return False
            Else
                clsPublic.Actualizar_Progreso(lblprg, "Conexión correcta.")
            End If

            Dim vIdDespacho = transaccionesOut.FirstOrDefault.Iddespachoenc
            Dim BeDespacho As clsBeTrans_despacho_enc = clsLnTrans_despacho_enc.GetSingle(vIdDespacho, clsTrans.lConnection, clsTrans.lTransaction)

            ' 1) POST /StockTransfers
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
            ServicePointManager.Expect100Continue = False
            ServicePointManager.FindServicePoint(New Uri(SapServiceLayerClient.baseUrl)).ConnectionLeaseTimeout = 0

            If BeDespacho.No_pase = 0 Then

                ' 2) Payload StockTransfer
                Dim payloadDeliveryNotes As DeliveryNoteDto = Build_DeliveryNotes_Payload(BePedidoEnc,
                                                                                          clsTrans,
                                                                                          transaccionesOut)

                Dim handler As New HttpClientHandler With {
                .AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate,
                .ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True,
                .UseCookies = False
            }

                Using http As New HttpClient(handler) With {.BaseAddress = New Uri(SapServiceLayerClient.baseUrl)}

                    Dim json As String = JsonConvert.SerializeObject(payloadDeliveryNotes, New JsonSerializerSettings With {.NullValueHandling = NullValueHandling.Ignore})
                    Dim content = New StringContent(json, Encoding.UTF8)
                    Dim mediaType = New MediaTypeHeaderValue("application/json")

                    mediaType.CharSet = "utf-8"
                    content.Headers.ContentType = mediaType

                    Dim req As New HttpRequestMessage(HttpMethod.Post, ENTITY_TARGET_DELIVERY_NOTE) With {.Content = content}
                    req.Headers.Add("Cookie", vHanaService.SessionCookie)
                    req.Headers.ConnectionClose = True

                    Dim resp = Await http.SendAsync(req).ConfigureAwait(False)
                    Dim body = Await resp.Content.ReadAsStringAsync().ConfigureAwait(False)

                    ' Parsear el JSON
                    Dim jsonObj As JObject = JObject.Parse(body)

                    Dim docEntryTransferPrimary As Integer = 0
                    Dim docNumTransferPrimary As Integer = 0

                    If resp.IsSuccessStatusCode Then
                        ' Capturar los valores
                        docEntryTransferPrimary = jsonObj("DocEntry")
                        docNumTransferPrimary = jsonObj("DocNum")
                    End If

                    If resp.IsSuccessStatusCode Then

                        clsPublic.Actualizar_Progreso(lblprg, "✅ Respuesta:")
                        clsPublic.Actualizar_Progreso(lblprg, "Se creó la transferencia: " & docNumTransferPrimary & " en SAP")

                        If BeDespacho IsNot Nothing Then
                            BeDespacho.No_pase = docNumTransferPrimary
                            '#EJC20251008: No utilice transacción porque en service layer ya se creó el documento.
                            'Si llegaran a haber interbloqueos debería considerarse agregar.
                            clsLnTrans_despacho_enc.Actualizar_No_Pase(BeDespacho, clsTrans.lConnection, clsTrans.lTransaction)

                            Dim docEntryFacturaReserva As Integer = BePedidoEnc.Referencia
                            Dim docNumSolicitud As String = BePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino
                            Dim Fromwarehouse As String = BePedidoEnc.Bodega_Origen
                            Dim ToWarehouse As String = BePedidoEnc.Bodega_Destino

                            BePedidoRef.Idpedidoencrefmi3 = clsLnTrans_pe_ref_mi3.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                            BePedidoRef.Idpedidoenc = BePedidoEnc.IdPedidoEnc
                            BePedidoRef.Iddespachoenc = BeDespacho.IdDespachoEnc
                            BePedidoRef.Docnumtraslado = docEntryTransferPrimary
                            BePedidoRef.Docentrytraslado = docNumTransferPrimary
                            BePedidoRef.Fec_agr = Now
                            BePedidoRef.Usr_agr = BeConfigEnc.IdUsuario
                            BePedidoRef.Codigo_bodega_origen = Fromwarehouse
                            BePedidoRef.Codigo_bodega_destino = ToWarehouse
                            BePedidoRef.Referencia_documento_origen = docNumSolicitud
                            BePedidoRef.Referencia_documento_destino = docNumTransferPrimary 'Este es el documento que llega a esa bodega X.
                            BePedidoRef.Observacion = $"Entrega generada por WMS sobre Factura de Reserva de Cliente de SAP: {docEntryFacturaReserva} - Ref: {docNumSolicitud} - IdDocumentoWMS: {BePedidoEnc.IdPedidoEnc}"
                            clsLnTrans_pe_ref_mi3.Insertar(BePedidoRef, clsTrans.lConnection, clsTrans.lTransaction)

                        End If

                        vTraslado_Creado = True

                    Else
                        clsPublic.Actualizar_Progreso(lblprg, $"❌ Error SL {resp.StatusCode}:")
                        clsPublic.Actualizar_Progreso(lblprg, body)

                    End If

                End Using

            End If

            Return vTraslado_Creado

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, $"❌ Error al crear la transferencia: {ex.Message}")
            Return False
        End Try
    End Function

    Private Shared Function Build_DeliveryNotes_Payload(BePedidoEnc As clsBeTrans_pe_enc,
                                                        clsTrans As clsTransaccion,
                                                        lTransaccionesSalida As List(Of clsBeI_nav_transacciones_out)) As DeliveryNoteDto

        ' OJO: esto debe ser el DocEntry de la FACTURA DE RESERVA
        Dim docEntryFacturaReserva As Integer = BePedidoEnc.Referencia
        Dim docNumFacturaReserva As String = BePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino

        Dim vOperadorPickingDefecto As String =
        clsLnTrans_picking_ubic.Get_Operador_Defecto_By_IdPickingEnc(
            BePedidoEnc.Picking.IdPickingEnc, clsTrans.lConnection, clsTrans.lTransaction)

        Dim dto As New DeliveryNoteDto With {
        .CardCode = clsLnCliente.Get_Codigo_By_IdCliente(BePedidoEnc.IdCliente),
        .DocDate = BePedidoEnc.Fecha_Pedido.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        .TaxDate = BePedidoEnc.Fecha_Pedido.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        .DocDueDate = Now.Date.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        .Comments = $"Entrega generada por WMS sobre Factura de Reserva: {docEntryFacturaReserva} - Ref: {docNumFacturaReserva} - IdWMS: {BePedidoEnc.IdPedidoEnc}",
        .JournalMemo = $"WMS Delivery from ODPI/OINV {docNumFacturaReserva}",
        .U_USR_PICK = vOperadorPickingDefecto,
        .U_ENVIADO_WMS = 2,
        .U_DOCUMENTO_WMS = BePedidoEnc.IdPedidoEnc,
        .U_INICIO_PICK = BePedidoEnc.Picking.Hora_ini.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        .U_FIN_PICK = BePedidoEnc.Picking.Hora_fin.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        .U_INICIO_ENVIO = Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        .U_FIN_ENVIO = Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        .U_ENVIADO_SAP_WMS = FormatoFechas.tFechaHoraSAP(Now),
        .U_Tipo = 1,
        .DocumentLines = New List(Of DeliveryNoteLineDto)()}

        ' Agrupar para armar cantidades y lotes
        Dim grupos = lTransaccionesSalida.
        GroupBy(Function(x) New With {
            Key .ItemCode = x.Codigo_producto,
            Key .No_Linea = x.No_linea,   ' debe ser LineNum del base
            Key .Talla = x.Talla,
            Key .Color = x.Color,
            Key .Lote = x.Lote
        }).
        Select(Function(g) New With {
            g.Key.ItemCode,
            g.Key.No_Linea,
            g.Key.Talla,
            g.Key.Color,
            g.Key.Lote,
            .Qty = g.Sum(Function(r) CDec(r.Cantidad))
        }).
        ToList()

        Dim lineas = grupos.
        GroupBy(Function(k) New With {Key k.ItemCode, Key k.No_Linea}).
        Select(Function(g) New With {
            g.Key.ItemCode,
            g.Key.No_Linea,
            .QtyLinea = g.Sum(Function(r) r.Qty),
            .Batches = g.Select(Function(r) r).ToList()
        }).ToList()

        For Each ln In lineas
            Dim line As New DeliveryNoteLineDto With {
            .ItemCode = ln.ItemCode,
            .BaseType = BASETYPE_AR_RESERVE_INVOICE,    ' 13
            .BaseEntry = docEntryFacturaReserva,
            .BaseLine = ln.No_Linea,
            .Quantity = Decimal.Round(ln.QtyLinea, 6),
            .BatchNumbers = New List(Of BatchNumberDto)()
        }

            ' UDFs en línea (si existen en DLN1)
            Dim first = ln.Batches.FirstOrDefault()
            If first IsNot Nothing Then
                line.U_Color = If(first.Color, String.Empty)
                line.U_Talla = If(first.Talla, String.Empty)
            End If

            ' Lotes existentes en SAP
            For Each b In ln.Batches
                line.BatchNumbers.Add(New BatchNumberDto With {
                .BatchNumber = BuildBatchNumber(b.Color, b.Talla),
                .Quantity = Decimal.Round(b.Qty, 6)
            })
            Next

            dto.DocumentLines.Add(line)
        Next

        Return dto
    End Function

    Private Shared Function BuildBatchNumber(color As String, talla As String) As String
        Return $"{color.Trim()}{talla.Trim()}"
    End Function

    <Serializable>
    <JsonObject(MemberSerialization:=MemberSerialization.OptOut)>
    Public Class DeliveryNoteDto

        Public Property CardCode As String = ""
        Public Property DocDate As Date = Today
        Public Property TaxDate As Date = Today
        Public Property DocDueDate As Date = Today
        Public Property Comments As String = ""
        Public Property JournalMemo As String = ""
        Public Property U_USR_PICK As String = ""
        Public Property U_DOCUMENTO_WMS As Integer = 0
        Public Property U_INICIO_PICK As DateTime = Now
        Public Property U_FIN_PICK As DateTime = Now
        Public Property U_ESTADO_PEDIDO As Integer = 0
        Public Property U_INICIO_ENVIO As DateTime = Now
        Public Property U_FIN_ENVIO As DateTime = Now
        Public Property U_Tipo As String = "" '1Manual, 2Resurtido Auto, 3Pedido Inicial
        Public Property U_ENVIADO_WMS As Integer = 1
        Public Property U_ENVIADO_SAP_WMS As String = ""
        Public Property DocumentLines As List(Of DeliveryNoteLineDto)

    End Class

    <JsonObject(MemberSerialization:=MemberSerialization.OptOut)>
    Public Class DeliveryNoteLineDto
        Public Property BaseType As Integer
        Public Property BaseEntry As Integer
        Public Property BaseLine As Integer
        Public Property ItemCode As String
        Public Property Quantity As Decimal
        Public Property U_Color As String
        Public Property U_Talla As String
        Public Property BatchNumbers As List(Of BatchNumberDto)
    End Class

End Class
