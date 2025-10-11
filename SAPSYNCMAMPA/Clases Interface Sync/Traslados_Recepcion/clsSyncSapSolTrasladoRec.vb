Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Text
Imports Newtonsoft.Json.Linq
Imports TOMWMS.clsDataContractDI

Public Class clsSyncSapSolTrasladoRec

    Private Shared vHanaService As SapServiceLayerClient

    Public Shared Async Function Procesar_Solicitud_Traslado_Tienda_SAP(ByVal lblprg As RichTextBox,
                                                                       ByVal prg As ProgressBar,
                                                                       Optional ByVal pNoDocumento As String = "") As Task(Of Boolean)
        Dim clsTrans As New clsTransaccion

        Try
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

            If BeConfigEnc.Bodega_Prorrateo <> "" Then
                Dim mensaje As String = "La configuración de interface indica que la Bodega: " & BeBodega.Codigo & " es de prorrateo, No se puede importar " &
                            If(pNoDocumento = "", "documentos por esta opción.", "el documento: " & pNoDocumento & " por esta opción.")

                clsPublic.Actualizar_Progreso(lblprg, mensaje)
                Return False
            End If

            Await Procesar_Documentos(BeBodega.Codigo,
                                      pNoDocumento,
                                      BeConfigEnc,
                                      lblprg,
                                      clsTrans)

            clsTrans.Commit_Transaction()

            Return True

        Catch ex As Exception
            clsTrans.RollBack_Transaction()
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, pNoDocumento, 1900, 900)
            clsPublic.Actualizar_Progreso(lblprg, ex.Message)
            Throw

        Finally
            clsTrans.Close_Conection()
            clsPublic.Actualizar_Progreso(lblprg, "Fin del proceso de sincronización de solicitudes de traslado a tienda en SAP.")
        End Try

    End Function

    Private Shared Async Function Procesar_Documentos(ByVal codigoBodega As String,
                                                      ByVal pNoDocumento As String,
                                                      ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                      ByVal lblprg As RichTextBox,
                                                      ByVal clsTrans As clsTransaccion) As Task(Of Boolean)

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Conectando a SAP.")

            Dim lPedidosCompra As List(Of clsBeI_nav_ped_compra_enc) = Get_Solicitudes_Traslado_Rec_SAP_SL(codigoBodega, clsTrans.lConnection, clsTrans.lTransaction, lblprg, pNoDocumento)
            Dim pBePedidoEnc As New clsBeTrans_pe_enc
            Dim PedidoClienteExistenteByCompany As New clsBeTrans_pe_enc
            Dim PedidoClienteExistente As New clsBeTrans_pe_enc

            If lPedidosCompra.Count = 0 Then
                clsPublic.Actualizar_Progreso(lblprg, "No hay documentos para importar.")
                Return False
            End If


            If clsLnI_nav_ped_compra_det.EliminarTodos(clsTrans.lConnection, clsTrans.lTransaction) _
                AndAlso clsLnI_nav_ped_compra_enc.EliminarTodos(clsTrans.lConnection, clsTrans.lTransaction) Then

                Dim BeProveedorBodega As New clsBeProveedor_bodega

                For Each BeINavPedCompra In lPedidosCompra

                    clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format("Procesando Pedido Compra: {0} ", BeINavPedCompra.No & " - " & BeINavPedCompra.Vendor_Invoice_No, vbNewLine))

                    If Not clsLnProveedor.Existe_Proveedor(BeINavPedCompra.Buy_From_Vendor_No) Then

                        BeConfigEnc = BeConfigEnc

                        If Await Validar_Proveedor_WMS(BeConfigEnc, BeINavPedCompra.Buy_From_Vendor_No,lblprg,clsTrans, vHanaService.SessionCookie, BD.Instancia.HANA_SL) Then
                            clsPublic.Actualizar_Progreso(lblprg, vbTab & "El proveedor: " & BeINavPedCompra.Buy_From_Vendor_No & " No existía en WMS y fue insertado.")
                        End If

                    End If

                    Dim BePedidoCompraEnc As New clsBeTrans_oc_enc
                    Dim vResult As String = ""

                    If clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(BeINavPedCompra,
                                                                            BePedidoCompraEnc,
                                                                            vResult) Then

                        Await Marcar_Sol_Traslado_Tienda_Rec_Sincronizada_SLAsync(BeINavPedCompra.No, vHanaService.SessionCookie, BD.Instancia.HANA_SL)

                    End If

                    clsPublic.Actualizar_Progreso(lblprg, vResult)

                Next

            End If

            Return False

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Shared Function Get_Solicitudes_Traslado_Rec_SAP_SL(pCodigoBodegaInterface As String,
                                                              lConnection As SqlConnection,
                                                              lTransaction As SqlTransaction,
                                                              lblprg As RichTextBox,
                                                              Optional pNoDocumentoSAP As String = "") As List(Of clsBeI_nav_ped_compra_enc)

        Dim lSolDevolTiendaRec As New List(Of clsBeI_nav_ped_compra_enc)
        Dim BePropietario As clsBePropietarios = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario, lConnection, lTransaction)

        If BePropietario Is Nothing Then
            Throw New Exception($"#Error: No se encontró el propietario con ID {BeConfigEnc.IdPropietario}")
        End If

        Try

            vHanaService = New SapServiceLayerClient()
            Dim loginResponse As LoginResponseDto = vHanaService.LoginAsync().GetAwaiter().GetResult()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener sesión.")
                Return lSolDevolTiendaRec
            Else
                clsPublic.Actualizar_Progreso(lblprg, "Conexión correcta.")
                Debug.WriteLine(vHanaService.SessionCookie)
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Obteniendo traslados a tienda...")

            ' Filtro a nivel de encabezado (no se puede filtrar por WarehouseCode aquí)
            Dim filtroEstado As String = "DocumentStatus eq 'bost_Open'"
            Dim filtroEnviado As String = "U_ENVIADO_WMS eq 2"
            Dim filtroDocNum As String = If(Not String.IsNullOrWhiteSpace(pNoDocumentoSAP), $" and DocNum eq {pNoDocumentoSAP}", "")
            Dim filtroFinal As String = $"{filtroEstado} and {filtroEnviado}{filtroDocNum}"

            Dim url As String = $"{BD.Instancia.HANA_SL}InventoryTransferRequests?$filter={Uri.EscapeDataString(filtroFinal)}"

            Using handler As New HttpClientHandler()
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, sslPolicyErrors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)

                    client.DefaultRequestHeaders.Add("Cookie", vHanaService.SessionCookie)
                    client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

                    Dim response = client.GetAsync(url).GetAwaiter().GetResult()

                    If Not response.IsSuccessStatusCode Then
                        Dim errorDetail = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
                        Throw New Exception($"Error al obtener traslados desde SL: {response.StatusCode}, {errorDetail}")
                    End If

                    Dim json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
                    Dim parsed = JObject.Parse(json)

                    For Each enc In parsed("value")

                        ' Filtrar por bodega en líneas del documento
                        Dim contieneBodega As Boolean = False
                        For Each linea In enc("StockTransferLines")
                            If linea("WarehouseCode")?.ToString() = pCodigoBodegaInterface Then
                                contieneBodega = True
                                Exit For
                            End If
                        Next

                        If Not contieneBodega Then Continue For

                        Dim BeTrasladoRecEnc As New clsBeI_nav_ped_compra_enc()
                        BeTrasladoRecEnc.No = Convert.ToInt32(enc("DocEntry"))
                        Dim datePart As Date = Convert.ToDateTime(enc("DocDate")).Date
                        Dim timeStr As String = enc("DocTime")?.ToString()
                        Dim ts As TimeSpan
                        If Not String.IsNullOrWhiteSpace(timeStr) AndAlso TimeSpan.TryParseExact(timeStr, "hh\:mm\:ss", CultureInfo.InvariantCulture, ts) Then
                            BeTrasladoRecEnc.Posting_Date = datePart.Add(ts)
                        Else
                            BeTrasladoRecEnc.Posting_Date = datePart
                        End If
                        BeTrasladoRecEnc.Order_Date = BeTrasladoRecEnc.Posting_Date
                        BeTrasladoRecEnc.Document_Date = BeTrasladoRecEnc.Posting_Date
                        BeTrasladoRecEnc.Expected_Receipt_Date = BeTrasladoRecEnc.Posting_Date
                        BeTrasladoRecEnc.Status = 1
                        BeTrasladoRecEnc.Buy_From_Vendor_No = enc("FromWarehouse").ToString()
                        BeTrasladoRecEnc.Buy_From_Vendor_Name = enc("CardName").ToString()
                        BeTrasladoRecEnc.Is_Internal_Transfer = False
                        BeTrasladoRecEnc.Location_Code = enc("ToWarehouse").ToString()
                        BeTrasladoRecEnc.Vendor_Invoice_No = enc("DocNum").ToString()
                        BeTrasladoRecEnc.Posting_Description = enc("JournalMemo").ToString()
                        BeTrasladoRecEnc.Comments = enc("Comments").ToString()
                        BeTrasladoRecEnc.Series = enc("Series").ToString() 'Descripción de la serie.                        
                        BeTrasladoRecEnc.Product_Owner_Code = BeConfigEnc.IdPropietario
                        If String.IsNullOrEmpty(BeTrasladoRecEnc.Vendor_Invoice_No) Then
                            BeTrasladoRecEnc.Vendor_Invoice_No = BeTrasladoRecEnc.No.ToString()
                        End If
                        BeTrasladoRecEnc.Campaign_No = enc("U_Campania").ToString()
                        BeTrasladoRecEnc.IsImport = enc("U_Estado").ToString() = "3"
                        BeTrasladoRecEnc.Internal_Transfer_Document_No = ""
                        BeTrasladoRecEnc.Document_Type = tTipoDocumentoIngreso.Transferencia_de_Ingreso

                        ' Mapeo de líneas
                        For Each linea In enc("StockTransferLines")
                            If linea("WarehouseCode")?.ToString() <> pCodigoBodegaInterface Then Continue For

                            Dim beDet As New clsBeI_nav_ped_compra_det() With {
                            .NoEnc = linea("DocEntry")?.ToString(),
                            .No = linea("ItemCode")?.ToString(),
                            .Line_No = If(linea("LineNum") IsNot Nothing, Convert.ToInt32(linea("LineNum")), 0),
                            .Planed_Receipt_Date = Date.Now(),
                            .Quantity = Convert.ToDecimal(linea("Quantity")),
                            .Quantity_Received = 0,
                            .Description = clsPublic.Quitar_Caracteres_No_Permitidos(linea("ItemDescription")?.ToString()),
                            .Unit_of_Measure_Code = linea("MeasureUnit")?.ToString(),
                            .Barcode = $"{linea("ItemCode")}{linea("U_Color")}{linea("U_Talla")}",
                            .Type = 2,
                            .Variant_Code = Nothing,
                            .Location_Code = linea("ToWarehouse")?.ToString(),
                            .Size = linea("U_Talla")?.ToString(),
                            .Color = linea("U_Color")?.ToString()
                        }

                            BeTrasladoRecEnc.Lineas_Detalle.Add(beDet)
                        Next

                        If BeTrasladoRecEnc.Lineas_Detalle.Any() Then
                            lSolDevolTiendaRec.Add(BeTrasladoRecEnc)
                        End If
                    Next
                End Using
            End Using

            Return lSolDevolTiendaRec

        Catch ex As Exception
            Throw New Exception("(SL) Get_Traslados_Tienda_Rec: " & ex.Message, ex)
        End Try
    End Function

    Private Shared Async Function Marcar_Sol_Traslado_Tienda_Rec_Sincronizada_SLAsync(docEntry As String,
                                                                                       sessionCookie As String,
                                                                                       baseUrl As String) As Task(Of Boolean)

        Try
            If String.IsNullOrWhiteSpace(docEntry) Then Return False

            Dim requestUrl As String = $"InventoryTransferRequests({docEntry})"
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
                            Throw New Exception($"Error al actualizar SolTraslado. Código: {response.StatusCode}, Detalle: {errContent}")
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception($"(SL) {MethodBase.GetCurrentMethod().Name} {ex.Message}", ex)
        End Try

    End Function

    Public Shared Async Function Validar_Proveedor_WMS(ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                       ByVal codigoSocioNegocio As String,
                                                       ByVal lblprg As RichTextBox,
                                                       ByVal clsTrans As clsTransaccion,
                                                       ByVal sessioncookie As String,
                                                       ByVal baseurl As String) As Task(Of Boolean)

        If String.IsNullOrWhiteSpace(codigoSocioNegocio) Then
            clsPublic.Actualizar_Progreso(lblprg, "Validar_Proveedor_WMS: Código de cliente vacío.")
            Return False
        End If

        Try
            Dim provWMS As clsBeProveedor = clsLnProveedor.Existe(codigoSocioNegocio, clsTrans.lConnection, clsTrans.lTransaction)
            If provWMS IsNot Nothing Then
                Return True
            End If

            ' Insertar desde SAP (ignoramos el posible retorno y verificamos)
            Dim BeBodega = Await clsSyncSAPBodega.Get_Bodega_SAP_By_Codigo(codigoSocioNegocio, sessioncookie, baseurl)

            Return clsLnProveedor.Insert_Proveedor_Interface(BeBodega, BeConfigEnc, IdUsuario)

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, $"Validar_Proveedor_WMS: {ex.Message}")
            Throw ex
        End Try
    End Function

End Class
