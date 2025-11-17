Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Text
Imports DevExpress.Utils.Internal
Imports DevExpress.XtraEditors.ViewInfo.BaseListBoxViewInfo
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports TOMWMS.clsDataContractDI

Public Class clsSyncTransacWMS

    Private Shared vHanaService As SapServiceLayerClient

#Region "Ventas"
    Private Shared Function Get_Ventas_TMK(pCodigoBodegaInterface As String,
                                           lConnection As SqlConnection,
                                           lTransaction As SqlTransaction,
                                           lblprg As RichTextBox) As List(Of clsBeI_nav_ped_traslado_enc)

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePropietario As clsBePropietarios = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario, lConnection, lTransaction)
        Dim BeBodega As clsBeBodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega, lConnection, lTransaction)
        Dim vEsTransferenciaDirecta As Boolean = False
        Dim vClienteVirtual As Integer = clsLnCliente.Get_IdCliente_By_Codigo(BeConfigEnc.Codigo_Cliente_Virtual, lConnection, lTransaction)

        If BePropietario Is Nothing Then
            Throw New Exception($"#ERROR: No se encontró el propietario con ID {BeConfigEnc.IdPropietario}")
        End If

        Try

            vHanaService = New SapServiceLayerClient()

            Dim loginResponse As LoginResponseDto = vHanaService.LoginAsync().GetAwaiter().GetResult()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener sesión.")
                Return lPedidosCliente
            Else
                clsPublic.Actualizar_Progreso(lblprg, "Conexión correcta.")
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Obteniendo documento(s).")

            Dim filtroEnviado As String = "U_Procesado_WMS eq 2"
            Dim filtroBodegaOrigen As String = $"(U_Transfer_from_Code eq '{pCodigoBodegaInterface}')"
            Dim filtroVentas As String = "( U_Document_Type eq '3')"
            Dim filtroFinal As String = $"{filtroEnviado} and {filtroBodegaOrigen} and {filtroVentas} "

            Dim url As String = $"{BD.Instancia.HANA_SL}TRANSAC_WMS?$filter={Uri.EscapeDataString(filtroFinal)}"

            Using handler As New HttpClientHandler()

                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, sslPolicyErrors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)

                    client.DefaultRequestHeaders.Add("Cookie", vHanaService.SessionCookie)
                    client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

                    Dim response = client.GetAsync(url).GetAwaiter().GetResult()

                    If Not response.IsSuccessStatusCode Then
                        Dim errorDetail = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
                        Throw New Exception($"Error al obtener transacciones de la bodega: {pCodigoBodegaInterface}-{response.StatusCode}, {errorDetail}")
                    End If

                    Dim json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
                    Dim parsed = JObject.Parse(json)

                    For Each pedido_cliente In parsed("value")

                        ' Filtrar por bodega en líneas del documento
                        Dim contieneBodega As Boolean = False
                        For Each linea In pedido_cliente("DocumentLines")
                            If linea("WarehouseCode")?.ToString() = pCodigoBodegaInterface Then
                                contieneBodega = True
                                Exit For
                            End If
                        Next

                        If Not contieneBodega Then Continue For

                        ' Mapeo del documento
                        Dim beFacturaDeudor As New clsBeI_nav_ped_traslado_enc With {
                        .No = pedido_cliente("DocEntry").Value(Of Integer),
                        .Posting_Date = pedido_cliente("DocDate").Value(Of Date),
                        .Receipt_Date = pedido_cliente("DocDate").Value(Of Date),
                        .Shipment_Date = pedido_cliente("DocDate").Value(Of Date),
                        .Status = 1,
                        .Transfer_from_Code = pCodigoBodegaInterface,
                        .Transfer_from_Contact = pedido_cliente("JournalMemo")?.ToString(),
                        .Transfer_to_Contact = pedido_cliente("CardName")?.ToString(),
                        .Transfer_to_CodeField = pedido_cliente("CardCode")?.ToString(),
                        .Transfer_to_Code = pedido_cliente("CardCode")?.ToString(),
                        .Product_Owner_Code = BePropietario.Codigo,
                        .Receipt_Document_Reference = pedido_cliente("DocNum").ToString(),
                        .Company_Code = "",
                        .Comments = pedido_cliente("Comments")?.ToString(),
                        .Document_Type = tTipoDocumentoSalida.Factura_Deudor,
                        .Transportation_Guide = pedido_cliente("U_Guia")?.ToString(),
                        .Lineas_Detalle = New List(Of clsBeI_nav_ped_traslado_det)
                         }

                        ' Mapeo de líneas
                        For Each linea In pedido_cliente("DocumentLines")
                            If linea("WarehouseCode")?.ToString() <> pCodigoBodegaInterface Then Continue For

                            Dim beDet As New clsBeI_nav_ped_traslado_det With {
                            .NoEnc = beFacturaDeudor.No,
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

                            beFacturaDeudor.Lineas_Detalle.Add(beDet)
                        Next

                        If beFacturaDeudor.Lineas_Detalle.Any() Then
                            lPedidosCliente.Add(beFacturaDeudor)
                        End If
                    Next

                End Using

            End Using

            Return lPedidosCliente

        Catch ex As Exception
            Throw New Exception("(SL) Get_Traslados_SAP_SL: " & ex.Message, ex)
        End Try
    End Function

    Private ReadOnly DateFormats As String() = {
       "yyyy-MM-ddTHH:mm:ssK",      ' ISO con zona
       "yyyy-MM-ddTHH:mm:ss",       ' ISO sin zona
       "yyyy-MM-dd",                ' Solo fecha
       "yyyyMMdd",                  ' Compacto
       "dd/MM/yyyy HH:mm:ss",
       "dd/MM/yyyy"
   }
    Private Function ParseDateOrNull(s As String) As Date?
        If String.IsNullOrWhiteSpace(s) Then Return Nothing
        Dim d As DateTime
        If DateTime.TryParseExact(s.Trim(),
                                  DateFormats,
                                  CultureInfo.InvariantCulture,
                                  DateTimeStyles.AssumeLocal Or DateTimeStyles.AdjustToUniversal,
                                  d) Then
            Return d
        End If

        ' Soporte para /Date(1696742400000)/ de SAP (ticks en ms)
        If s.StartsWith("/Date(") AndAlso s.EndsWith(")/") Then
            Dim inside = s.Substring(6, s.Length - 8)
            Dim ticksMs As Long
            If Long.TryParse(New String(inside.TakeWhile(Function(c) Char.IsDigit(c) OrElse c = "-"c).ToArray()), ticksMs) Then
                Dim epoch = New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                Return epoch.AddMilliseconds(ticksMs)
            End If
        End If

        Return Nothing
    End Function

    Public Shared Async Function Procesar_Pedido_de_Cliente_SAP(ByVal lblprg As RichTextBox,
                                                                 ByVal prg As ProgressBar,
                                                                 Optional ByVal pNoDocumento As String = "") As Task(Of Boolean)
        Dim clsTrans As New clsTransaccion
        Dim sw As New Stopwatch()

        Try
            ' Inicia cronómetro y anuncia inicio
            sw.Start()
            clsPublic.Actualizar_Progreso(lblprg, "Iniciando proceso de sincronización de pedidos de cliente desde SAP.")

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
            clsPublic.Actualizar_Progreso(lblprg, $"Fin del proceso de sincronización de los pedidos de cliente desde SAP. Tiempo total: {sw.Elapsed.TotalSeconds:F2} segundos.")
        End Try

    End Function

    Private Shared Async Function Procesar_Documentos(ByVal codigoBodega As String,
                                                      ByVal pNoDocumento As String,
                                                      ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                      ByVal lblprg As RichTextBox,
                                                      ByVal clsTrans As clsTransaccion) As Task(Of Boolean)

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Conectando a SAP.")

            Dim facturas As List(Of clsBeI_nav_ped_traslado_enc) = Get_Ventas_TMK(codigoBodega, clsTrans.lConnection, clsTrans.lTransaction, lblprg)
            Dim pBePedidoEnc As New clsBeTrans_pe_enc
            Dim PedidoClienteExistenteByCompany As New clsBeTrans_pe_enc
            Dim PedidoClienteExistente As New clsBeTrans_pe_enc

            If facturas.Count = 0 Then
                clsPublic.Actualizar_Progreso(lblprg, "No hay documentos para importar.")
                Return False
            End If

            For Each factura In facturas

                clsPublic.Actualizar_Progreso(lblprg, $"Procesando pedido de cliente de SAP (@Transac_WMS): {factura.Receipt_Document_Reference}/{factura.No}{vbNewLine}")

                '#MECR 202508080524: Verifica si el proveedor ya existe como cliente en WMS.
                If Await clsSyncSapTrasladosEnvio.Validar_Cliente_WMS(factura.Transfer_to_Code, "C", lblprg, clsTrans, vHanaService.SessionCookie, BD.Instancia.HANA_SL) Then

                    Dim pedidoEnc As clsBeTrans_pe_enc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia_If(factura, lblprg, clsTrans.lConnection, clsTrans.lTransaction)

                    Dim trasladoSincronizado As Boolean = Marcar_Pedido_Cliente_Sincronizado_SLAsync(factura.No, vHanaService.SessionCookie, BD.Instancia.HANA_SL).GetAwaiter().GetResult()

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

    Private Shared Async Function Marcar_Pedido_Cliente_Sincronizado_SLAsync(docEntry As String,
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

#End Region

#Region "Notas de Crédito (Devoluciones)"
    Private Shared Function Get_Devoluciones_Tiendas(pCodigoBodegaInterface As String,
                                                     lConnection As SqlConnection,
                                                     lTransaction As SqlTransaction,
                                                     lblprg As RichTextBox) As List(Of clsBeI_nav_ped_traslado_enc)

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePropietario As clsBePropietarios = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario, lConnection, lTransaction)
        Dim BeBodega As clsBeBodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega, lConnection, lTransaction)
        Dim vEsTransferenciaDirecta As Boolean = False
        Dim vClienteVirtual As Integer = clsLnCliente.Get_IdCliente_By_Codigo(BeConfigEnc.Codigo_Cliente_Virtual, lConnection, lTransaction)

        If BePropietario Is Nothing Then
            Throw New Exception($"#ERROR: No se encontró el propietario con ID {BeConfigEnc.IdPropietario}")
        End If

        Try

            vHanaService = New SapServiceLayerClient()

            Dim loginResponse As LoginResponseDto = vHanaService.LoginAsync().GetAwaiter().GetResult()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener sesión.")
                Return lPedidosCliente
            Else
                clsPublic.Actualizar_Progreso(lblprg, "Conexión correcta.")
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Obteniendo documento(s).")

            Dim filtroEnviado As String = "U_Procesado_WMS eq 2"
            Dim filtroBodegaOrigen As String = $"(U_Transfer_from_Code eq '{pCodigoBodegaInterface}')"
            Dim filtroVentas As String = "( U_Document_Type eq '17')"
            Dim filtroFinal As String = $"{filtroEnviado} and {filtroBodegaOrigen} and {filtroVentas} "

            Dim url As String = $"{BD.Instancia.HANA_SL}TRANSAC_WMS?$filter={Uri.EscapeDataString(filtroFinal)}"

            Using handler As New HttpClientHandler()

                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, sslPolicyErrors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)

                    client.DefaultRequestHeaders.Add("Cookie", vHanaService.SessionCookie)
                    client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

                    Dim response = client.GetAsync(url).GetAwaiter().GetResult()

                    If Not response.IsSuccessStatusCode Then
                        Dim errorDetail = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
                        Throw New Exception($"Error al obtener transacciones de la bodega: {pCodigoBodegaInterface}-{response.StatusCode}, {errorDetail}")
                    End If

                    Dim json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
                    Dim parsed = JObject.Parse(json)

                    For Each pedido_cliente In parsed("value")

                        ' Filtrar por bodega en líneas del documento
                        Dim contieneBodega As Boolean = False
                        For Each linea In pedido_cliente("DocumentLines")
                            If linea("WarehouseCode")?.ToString() = pCodigoBodegaInterface Then
                                contieneBodega = True
                                Exit For
                            End If
                        Next

                        If Not contieneBodega Then Continue For

                        ' Mapeo del documento
                        Dim beFacturaDeudor As New clsBeI_nav_ped_traslado_enc With {
                        .No = pedido_cliente("DocEntry").Value(Of Integer),
                        .Posting_Date = pedido_cliente("DocDate").Value(Of Date),
                        .Receipt_Date = pedido_cliente("DocDate").Value(Of Date),
                        .Shipment_Date = pedido_cliente("DocDate").Value(Of Date),
                        .Status = 1,
                        .Transfer_from_Code = pCodigoBodegaInterface,
                        .Transfer_from_Contact = pedido_cliente("JournalMemo")?.ToString(),
                        .Transfer_to_Contact = pedido_cliente("CardName")?.ToString(),
                        .Transfer_to_CodeField = pedido_cliente("CardCode")?.ToString(),
                        .Transfer_to_Code = pedido_cliente("CardCode")?.ToString(),
                        .Product_Owner_Code = BePropietario.Codigo,
                        .Receipt_Document_Reference = pedido_cliente("DocNum").ToString(),
                        .Company_Code = "",
                        .Comments = pedido_cliente("Comments")?.ToString(),
                        .Document_Type = tTipoDocumentoSalida.Factura_Deudor,
                        .Transportation_Guide = pedido_cliente("U_Guia")?.ToString(),
                        .Lineas_Detalle = New List(Of clsBeI_nav_ped_traslado_det)
                         }

                        ' Mapeo de líneas
                        For Each linea In pedido_cliente("DocumentLines")
                            If linea("WarehouseCode")?.ToString() <> pCodigoBodegaInterface Then Continue For

                            Dim beDet As New clsBeI_nav_ped_traslado_det With {
                            .NoEnc = beFacturaDeudor.No,
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

                            beFacturaDeudor.Lineas_Detalle.Add(beDet)
                        Next

                        If beFacturaDeudor.Lineas_Detalle.Any() Then
                            lPedidosCliente.Add(beFacturaDeudor)
                        End If
                    Next

                End Using

            End Using

            Return lPedidosCliente

        Catch ex As Exception
            Throw New Exception("(SL) Get_Traslados_SAP_SL: " & ex.Message, ex)
        End Try
    End Function

    Public Shared Async Function Procesar_Devoluciones_de_Cliente_SAP(ByVal lblprg As RichTextBox,
                                                                 ByVal prg As ProgressBar,
                                                                 Optional ByVal pNoDocumento As String = "") As Task(Of Boolean)
        Dim clsTrans As New clsTransaccion
        Dim sw As New Stopwatch()

        Try
            ' Inicia cronómetro y anuncia inicio
            sw.Start()
            clsPublic.Actualizar_Progreso(lblprg, "Iniciando proceso de sincronización de notas de crédito de cliente desde SAP (Devoluciones).")

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

            Await Procesar_Documentos_Devolucion(BeBodega.Codigo,
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
            clsPublic.Actualizar_Progreso(lblprg, $"Fin del proceso de sincronización de los pedidos de cliente desde SAP. Tiempo total: {sw.Elapsed.TotalSeconds:F2} segundos.")
        End Try

    End Function

    Private Shared Async Function Procesar_Documentos_Devolucion(ByVal codigoBodega As String,
                                                                 ByVal pNoDocumento As String,
                                                                 ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                                 ByVal lblprg As RichTextBox,
                                                                 ByVal clsTrans As clsTransaccion) As Task(Of Boolean)

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Conectando a SAP.")

            Dim facturas As List(Of clsBeI_nav_ped_traslado_enc) = Get_Ventas_TMK(codigoBodega, clsTrans.lConnection, clsTrans.lTransaction, lblprg)
            Dim pBePedidoEnc As New clsBeTrans_pe_enc
            Dim PedidoClienteExistenteByCompany As New clsBeTrans_pe_enc
            Dim PedidoClienteExistente As New clsBeTrans_pe_enc

            If facturas.Count = 0 Then
                clsPublic.Actualizar_Progreso(lblprg, "No hay documentos para importar.")
                Return False
            End If

            For Each factura In facturas

                clsPublic.Actualizar_Progreso(lblprg, $"Procesando pedido de cliente de SAP (@Transac_WMS): {factura.Receipt_Document_Reference}/{factura.No}{vbNewLine}")

                '#MECR 202508080524: Verifica si el proveedor ya existe como cliente en WMS.
                If Await clsSyncSapTrasladosEnvio.Validar_Cliente_WMS(factura.Transfer_to_Code, "C", lblprg, clsTrans, vHanaService.SessionCookie, BD.Instancia.HANA_SL) Then

                    Dim pedidoEnc As clsBeTrans_pe_enc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia_If(factura, lblprg, clsTrans.lConnection, clsTrans.lTransaction)

                    Dim trasladoSincronizado As Boolean = Marcar_Devolucion_Sincronizada_SLAsync(factura.No, vHanaService.SessionCookie, BD.Instancia.HANA_SL).GetAwaiter().GetResult()

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

    Private Shared Async Function Marcar_Devolucion_Sincronizada_SLAsync(docEntry As String,
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

#End Region
End Class
