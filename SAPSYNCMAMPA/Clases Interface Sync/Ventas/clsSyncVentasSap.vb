Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports DevExpress.Utils.Internal
Imports DevExpress.XtraEditors.ViewInfo.BaseListBoxViewInfo
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports TOMWMS.clsDataContractDI

Public Class clsSyncVentasSap

    Private Shared vHanaService As SapServiceLayerClient

    Private Shared Function Get_Ventas_TMK(pCodigoBodegaInterface As String,
                                           lConnection As SqlConnection,
                                           lTransaction As SqlTransaction,
                                           lblprg As RichTextBox,
                                           Optional pNoDocumentoSAP As String = "") As List(Of clsBeI_nav_ped_traslado_enc)

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
            Dim filtroBodegaOrigen As String = $"( and U_Transfer_from_Code eq '{pCodigoBodegaInterface}')"
            Dim filtroFinal As String = $"{filtroEnviado} and {filtroBodegaOrigen} "

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

                    If BeConfigEnc.Bodega_Prorrateo = BeBodega.Codigo Then
                        vEsTransferenciaDirecta = True
                    End If

                    For Each traslado In parsed("value")


                        Dim U_Transito = traslado("U_Transito").Value(Of String)

                        Dim bePedido As New clsBeI_nav_ped_traslado_enc With {
                        .No = traslado("U_NoEnc").Value(Of Integer),
                        .External_Document_No = traslado("U_External_Document_No").Value(Of Integer),
                        .Posting_Date = traslado("DocDate").Value(Of Date),
                        .Receipt_Date = traslado("DocDate").Value(Of Date),
                        .Shipment_Date = traslado("DocDate").Value(Of Date),
                        .Status = 1,
                        .Transfer_from_Code = traslado("FromWarehouse")?.ToString(),
                        .Transfer_from_Contact = traslado("JournalMemo")?.ToString(),
                        .Transfer_to_Contact = traslado("CardName")?.ToString(),
                        .Transfer_to_CodeField = traslado("ToWarehouse")?.ToString(), 'Transfer_to_CodeField
                        .Transfer_to_Code = IIf(U_Transito IsNot Nothing, U_Transito, traslado("ToWarehouse")?.ToString()), 'Cliente = Bodega_Virtual - U_Transito
                        .Product_Owner_Code = BePropietario.Codigo,
                        .Receipt_Document_Reference = traslado("DocNum").ToString(),
                        .Company_Code = "",
                        .Comments = traslado("Comments").ToString(),
                        .Document_Type = IIf(vEsTransferenciaDirecta, tTipoDocumentoSalida.Transferencia_Directa, tTipoDocumentoSalida.Transferencia_Interna_WMS),
                        .Lineas_Detalle = New List(Of clsBeI_nav_ped_traslado_det)
                    }

                        Dim oBodega = clsSyncSAPBodega.Get_Bodega_SAP_By_Codigo(bePedido.Transfer_to_Code, vHanaService.SessionCookie, BD.Instancia.HANA_SL).GetAwaiter().GetResult()

                        If oBodega IsNot Nothing Then
                            bePedido.Transfer_to_Name = oBodega.Bodega_name
                        End If

                        If String.IsNullOrWhiteSpace(bePedido.Transfer_to_CodeField) Then
                            Throw New Exception("Error: No se definió la bodega destino (U_ToWhsCode) en el traslado SAP.")
                        End If

                        For Each linea In traslado("StockTransferLines")
                            Dim beDet As New clsBeI_nav_ped_traslado_det With {
                            .NoEnc = bePedido.No,
                            .No = clsLnTrans_pe_det.MaxID() + 1,
                            .Item_No = linea("ItemCode")?.ToString(),
                            .Line_No = linea("LineNum").Value(Of Integer),
                            .Shipment_Date = Date.Now,
                            .Quantity = linea("Quantity").Value(Of Decimal),
                            .Description = linea("ItemDescription")?.ToString(),
                            .Unit_of_Measure_Code = linea("MeasureUnit")?.ToString(),
                            .Status = 1,
                            .Variant_Code = Nothing,
                            .Transfer_to_CodeField = linea("WarehouseCode")?.ToString(),
                            .Price = linea("Price").Value(Of Double),
                            .Color = linea("U_Color").Value(Of String),
                            .Size = linea("U_Talla").Value(Of String)
                        }

                            bePedido.Lineas_Detalle.Add(beDet)
                        Next

                        lPedidosCliente.Add(bePedido)
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

    Public Sub Construir_Listas(json As String,
                                ByRef lstEnc As List(Of clsBeI_nav_ped_traslado_enc),
                                ByRef lstDet As List(Of clsBeI_nav_ped_traslado_det))

        Dim Ventas = JsonConvert.DeserializeObject(Of List(Of dto_TransacWMS))(json)

        lstEnc = New List(Of clsBeI_nav_ped_traslado_enc)()
        lstDet = New List(Of clsBeI_nav_ped_traslado_det)()

        If Ventas Is Nothing OrElse Ventas.Count = 0 Then Exit Sub

        ' Agrupa SOLO por U_NoEnc
        Dim grupos = Ventas.GroupBy(Function(r) r.U_NoEnc)

        For Each g In grupos

            Dim first = g.First()
            Dim noEnc As Integer = If(g.Key.HasValue, g.Key.Value, 0)

            ' ===== Encabezado (clsBeI_nav_trans_pe_enc) =====
            Dim enc As New clsBeI_nav_ped_traslado_enc With {
            .No = If(noEnc > 0, noEnc, 0),
            .External_Document_No = If(first.U_External_Document_No, Nothing),
            .Company_Code = If(first.U_Company_Code, Nothing),
            .Posting_Date = ParseDateOrNull(first.U_Posting_Date),
            .Transfer_from_Code = If(first.U_Transfer_from_Code, Nothing),
            .Transfer_from_Contact = If(first.U_Transfer_from_Contact, Nothing),
            .Transfer_to_Code = If(first.U_Transfer_to_Code, Nothing),
            .Transfer_to_Name = If(first.U_Transfer_to_Name, Nothing),
            .Receipt_Document_Reference = If(first.U_Receip_Document_Reference, Nothing),
            .Document_Type = If(first.U_Document_Type, Nothing),
            .Shipment_Date = ParseDateOrNull(first.U_Fec_Agr),
            .Receipt_Date = ParseDateOrNull(first.U_Fec_Agr)
        }
            lstEnc.Add(enc)

            ' ===== Detalles (clsBeI_nav_trans_pe_det) =====
            Dim idx As Integer = 0
            For Each r In g
                idx += 1
                Dim line = If(r.U_Line_No.HasValue AndAlso r.U_Line_No.Value > 0, r.U_Line_No.Value, idx)

                Dim det As New clsBeI_nav_ped_traslado_det With {
                .NoEnc = If(noEnc > 0, noEnc, 0),                  ' FK al encabezado
                .No = If(r.U_Item_No, Nothing),                    ' (dejaste este nombre en tu ciclo)
                .Line_No = line,
                .Item_No = If(r.U_Item_No, Nothing),
                .Description = If(r.U_Descripcion, Nothing),
                .Unit_of_Measure_Code = If(r.U_Unit_of_Mesasure_Code, Nothing),
                .Qty_to_Ship = r.U_Qty_to_Ship,
                .Quantity = r.U_Qty_WMS,
                .Color = If(r.U_Color, Nothing),
                .Size = If(r.U_Size, Nothing)
            }
                lstDet.Add(det)
            Next
        Next
    End Sub



    Private Shared Async Function Procesar_Documentos(ByVal codigoBodega As String,
                                                      ByVal pNoDocumento As String,
                                                      ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                      ByVal lblprg As RichTextBox,
                                                      ByVal clsTrans As clsTransaccion,
                                                      Optional ByVal pEsProrrateo As Boolean = True,
                                                      Optional ByVal pEsTrasladoBodegaVirtual As Boolean = False) As Task(Of Boolean)

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Conectando a SAP.")

            Dim solicitudes As List(Of clsBeI_nav_ped_traslado_enc) = Get_Ventas_TMK(codigoBodega, clsTrans.lConnection, clsTrans.lTransaction, lblprg, pNoDocumento)
            Dim pBePedidoEnc As New clsBeTrans_pe_enc
            Dim PedidoClienteExistenteByCompany As New clsBeTrans_pe_enc
            Dim PedidoClienteExistente As New clsBeTrans_pe_enc

            If solicitudes.Count = 0 Then
                clsPublic.Actualizar_Progreso(lblprg, "No hay documentos para importar.")
                Return False
            End If

            For Each solicitud In solicitudes
                'Tener un cliente por defecto


                '#EJC20251009: En esta opción solo deben importarse documentos cuya bodega de origen sea la de prorrateo.
                If pEsProrrateo Then
                    If Not solicitud.Transfer_from_Code = BeConfigEnc.Bodega_Prorrateo Then
                        clsPublic.Actualizar_Progreso(lblprg, $"La bodega de origen {solicitud.Transfer_from_Code} no coincide con la bodega de prorrateo {BeConfigEnc.Bodega_Prorrateo}, se omite el documento {solicitud.No}.")
                        Continue For
                    End If
                End If

                clsPublic.Actualizar_Progreso(lblprg, $"Procesando solicitud de traslado SAP (OWTQ): {solicitud.Receipt_Document_Reference}/{solicitud.No}{vbNewLine}")

                'If Await Validar_Cliente_WMS(solicitud.Transfer_to_Code, "C", lblprg, clsTrans, vHanaService.SessionCookie, BD.Instancia.HANA_SL) Then

                '    Dim origenEsWMS As Boolean = clsLnBodega_area.Existe_Codigo_By_IdBodega(solicitud.Transfer_to_Code, BeConfigEnc.Idbodega, clsTrans.lConnection, clsTrans.lTransaction)
                '    Dim destinoEsWMS As Boolean = clsLnBodega_area.Existe_Codigo_By_IdBodega(solicitud.Transfer_to_Code, BeConfigEnc.Idbodega, clsTrans.lConnection, clsTrans.lTransaction)
                '    Dim debeProcesar As Boolean = Not destinoEsWMS OrElse Not origenEsWMS OrElse (origenEsWMS AndAlso destinoEsWMS)

                '    If debeProcesar Then
                '        Dim pedidoEnc As clsBeTrans_pe_enc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia_If(solicitud, lblprg, clsTrans.lConnection, clsTrans.lTransaction)

                '        Dim trasladoSincronizado As Boolean = Marcar_Traslado_Sincronizado_SLAsync(solicitud.No, vHanaService.SessionCookie, BD.Instancia.HANA_SL).GetAwaiter().GetResult()

                '        If pedidoEnc IsNot Nothing AndAlso trasladoSincronizado Then
                '            Return True
                '        End If
                '    End If

                'End If

            Next

            Return False

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, ex.Message)
            Return False
        End Try

    End Function

End Class
