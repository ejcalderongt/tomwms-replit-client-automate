Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Text
Imports Newtonsoft.Json.Linq
Imports TOMWMS.clsDataContractDI

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

    Private Shared Function Get_Factura_Deudor_SAP_SL(pCodigoBodegaInterface As String,
                                                      lConnection As SqlConnection,
                                                      lTransaction As SqlTransaction,
                                                      lblprg As RichTextBox,
                                                      Optional pNoDocumentoSAP As String = "") As List(Of clsBeI_nav_ped_traslado_enc)

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
            Dim filtroEstado As String = "DocumentStatus eq 'bost_Close'"
            Dim filtroEnviado As String = "U_ENVIADO_WMS eq 2"
            Dim filtroDocNum As String = If(Not String.IsNullOrWhiteSpace(pNoDocumentoSAP), $" and DocNum eq {pNoDocumentoSAP}", "")
            Dim filtroFinal As String = $"{filtroFacturaDeudor} and {filtroEstado} and {filtroEnviado}{filtroDocNum}"

            Dim url As String = $"{BD.Instancia.HANA_SL}Invoices?$filter={Uri.EscapeDataString(filtroFinal)}"

            Using handler As New HttpClientHandler()
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, sslPolicyErrors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)
                    client.DefaultRequestHeaders.Add("Cookie", vHanaService.SessionCookie)
                    client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

                    Dim response = client.GetAsync(url).GetAwaiter().GetResult()

                    If Not response.IsSuccessStatusCode Then
                        Dim errorDetail = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
                        Throw New Exception($"Error al obtener facturas de deudor desde SL: {response.StatusCode}, {errorDetail}")
                    End If

                    Dim json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
                    Dim parsed = JObject.Parse(json)

                    For Each factura_deudor In parsed("value")

                        ' Filtrar por bodega en líneas del documento
                        Dim contieneBodega As Boolean = False
                        For Each linea In factura_deudor("DocumentLines")
                            If linea("WarehouseCode")?.ToString() = pCodigoBodegaInterface Then
                                contieneBodega = True
                                Exit For
                            End If
                        Next

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
                        For Each linea In factura_deudor("DocumentLines")
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

            Dim facturas As List(Of clsBeI_nav_ped_traslado_enc) = Get_Factura_Deudor_SAP_SL(codigoBodega, clsTrans.lConnection, clsTrans.lTransaction, lblprg, pNoDocumento)
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

End Class
