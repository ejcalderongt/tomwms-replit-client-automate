Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class SapServiceLayerClient

    Public Shared Property baseUrl As String = BD.Instancia.HANA_SL
    Private Property companyDb As String = BD.Instancia.SAP_COMPANY_DB
    Private Property user As String = BD.Instancia.HANA_SL_USR
    Private Property password As String = BD.Instancia.HANA_SL_PWD
    Public Property SessionCookie As String
    Shared Property SessionId As String = ""
    Private Shared Property RouteId As String = ""

    Public Sub New()
    End Sub

    Public Async Function GetBusinessPartnerAsync(cardCode As String) As Task(Of BusinessPartnerDto)
        Try
            ' Permitir certificados autofirmados
            ServicePointManager.ServerCertificateValidationCallback = Function(sender, cert, chain, sslPolicyErrors) True

            ' Crear el handler con el contenedor de cookies
            Dim handler As New HttpClientHandler()
            handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, sslPolicyErrors) True
            handler.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate

            Dim cookieContainer As New CookieContainer()
            cookieContainer.SetCookies(New Uri(baseUrl), SessionCookie) ' Reusar la cookie completa
            handler.CookieContainer = cookieContainer

            ' Crear el cliente con el handler
            Using client As New HttpClient(handler)
                client.BaseAddress = New Uri(baseUrl)

                ' Preparar la URL
                Dim requestUrl As String = $"BusinessPartners('{cardCode}')"

                ' Enviar la solicitud GET
                Dim response = Await client.GetAsync(requestUrl).ConfigureAwait(False)

                ' Procesar la respuesta
                If response.IsSuccessStatusCode Then
                    Dim jsonResponse = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                    Dim partner As BusinessPartnerDto = JsonConvert.DeserializeObject(Of BusinessPartnerDto)(jsonResponse)
                    Return partner
                Else
                    Dim errContent = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                    Throw New Exception($"Error al obtener el socio de negocio. Código: {response.StatusCode}, Detalle: {errContent}")
                End If
            End Using

        Catch ex As Exception
            Debug.WriteLine($"Excepción en GetBusinessPartnerAsync: {ex.Message}")
            Throw
        End Try
    End Function

    Public Async Function LoginAsync() As Task(Of LoginResponseDto)

        Try

            If baseUrl = "" Then
                If Init_App() Then
                    baseUrl = BD.Instancia.HANA_SL
                    companyDb = BD.Instancia.SAP_COMPANY_DB
                    user = BD.Instancia.HANA_SL_USR
                    password = BD.Instancia.HANA_SL_PWD
                End If
            End If

            Dim loginUrl As String = $"{baseUrl}Login"
            Dim loginData As New With {
                                        Key .CompanyDB = companyDb,
                                        Key .UserName = user,
                                        Key .Password = password
                                      }

            Dim json As String = JsonConvert.SerializeObject(loginData)
            Dim content As New StringContent(json, Encoding.UTF8, "application/json")

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
            ServicePointManager.Expect100Continue = False

            Using handler As New HttpClientHandler()

                handler.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)
                    'client.Timeout = TimeSpan.FromMinutes(3)

                    'client.DefaultRequestHeaders.ConnectionClose = True

                    Using request As New HttpRequestMessage(HttpMethod.Post, loginUrl)

                        request.Content = content
                        'request.Headers.ConnectionClose = True

                        'Dim response As HttpResponseMessage = Await client.SendAsync(request).ConfigureAwait(False)
                        Dim response As HttpResponseMessage = Await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(False)

                        If response.IsSuccessStatusCode Then

                            Dim responseBody As String = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                            Dim loginResult As LoginResponseDto = JsonConvert.DeserializeObject(Of LoginResponseDto)(responseBody)

                            If response.Headers.TryGetValues("Set-Cookie", Nothing) Then

                                Dim cookies = response.Headers.GetValues("Set-Cookie")
                                Dim allCookies As String = String.Join("; ", cookies.Select(Function(c) c.Split(";"c)(0)))

                                SessionCookie = allCookies

                                For Each c In cookies
                                    If c.Contains("B1SESSION") Then
                                        SessionId = ExtractCookieValue(c, "B1SESSION")
                                    ElseIf c.Contains("ROUTEID") Then
                                        RouteId = ExtractCookieValue(c, "ROUTEID")
                                    End If
                                Next

                                loginResult.SessionId = SessionId
                                loginResult.RouteId = RouteId

                            End If

                            Return loginResult
                        Else
                            Dim errorContent As String = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                            Throw New Exception($"Error al autenticar. Código: {response.StatusCode}, Detalle: {errorContent}")
                        End If

                    End Using

                End Using

            End Using

        Catch ex As TaskCanceledException
            Throw New Exception("Login cancelado por timeout o red. Verifica conectividad/Service Layer y aumenta HttpClient.Timeout.", ex)
        End Try

    End Function

    Public Async Function GetPurchaseOrderAsync(docEntry As Integer) As Task(Of FacturaReservaDto)

        Dim requestUrl As String = $"PurchaseInvoices({docEntry})"

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
        ServicePointManager.Expect100Continue = False

        Using handler As New HttpClientHandler()
            handler.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
            handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True
            handler.UseCookies = False ' Se usa manualmente la cookie

            Using client As New HttpClient(handler)
                client.DefaultRequestHeaders.ConnectionClose = True

                Using request As New HttpRequestMessage(HttpMethod.Get, baseUrl & requestUrl)
                    request.Headers.ConnectionClose = True
                    request.Headers.Add("Cookie", SessionCookie)
                    request.Headers.Accept.Add(New Headers.MediaTypeWithQualityHeaderValue("application/json"))

                    Dim response As HttpResponseMessage = Await client.SendAsync(request).ConfigureAwait(False)

                    If Not response.IsSuccessStatusCode Then
                        Dim errContent = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                        Throw New Exception($"Error al obtener la orden. Código: {response.StatusCode}, Detalle: {errContent}")
                    End If

                    Dim jsonResponse = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                    Dim order = JsonConvert.DeserializeObject(Of FacturaReservaDto)(jsonResponse)
                    Return order
                End Using
            End Using
        End Using

    End Function

    Function ExtractCookieValue(cookieHeader As String, key As String) As String
        Dim parts = cookieHeader.Split(";"c)
        For Each part In parts
            If part.Trim().StartsWith(key & "=") Then
                Return part.Split("="c)(1).Trim()
            End If
        Next
        Return ""

    End Function

    Public Async Function Procesar_Detalle_Ingreso_HANA2(oOrderPurchase As FacturaReservaDto,
                                                        BeINavConfigEnc As clsBeI_nav_config_enc,
                                                        BeTransOCEnc As clsBeTrans_oc_enc,
                                                        IdRecepcionEnc As Integer,
                                                        lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                        lblprg As RichTextBox,
                                                        lConnection As SqlConnection,
                                                        lTransaction As SqlTransaction) As Task(Of Boolean)

        Dim result As Boolean = False
        Dim BeTransOCTi As clsBeTrans_oc_ti = Nothing
        Dim vEsImportacion As Boolean = False
        Dim vCodigoBodegaImportacion As String = ""
        Dim BeTransReOc As New clsBeTrans_re_oc

        Try

            Dim vIdRecepcionEnc As Integer = lINavTransaccionesOut.FirstOrDefault.Idrecepcionenc
            BeTransReOc = clsLnTrans_re_oc.Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc(BeTransOCEnc.IdOrdenCompraEnc, vIdRecepcionEnc, lConnection, lTransaction)

            If BeTransReOc Is Nothing Then
                Throw New Exception("❌ ERROR: No se encontró la recepción de orden de compra para el IdRecepcionEnc: " & vIdRecepcionEnc)
            End If

            '--- Tipo de ingreso (importación) ---
            BeTransOCTi = clsLnTrans_oc_ti.GetSingle(BeTransOCEnc.IdTipoIngresoOC, lConnection, lTransaction)
            If BeTransOCTi IsNot Nothing Then
                vEsImportacion = BeTransOCTi.Es_Importacion
                vCodigoBodegaImportacion = BeINavConfigEnc.Bodega_Prorrateo
            End If

            If vEsImportacion Then
                If String.IsNullOrWhiteSpace(vCodigoBodegaImportacion) Then
                    Throw New Exception("❌ ERROR: No está configurada la bodega de prorrateo en la configuración de integración.")
                End If
            End If

            '--- Documento ÚNICO de entrega ---
            Dim entrega As New FacturaReservaEntregaDto With {
            .CardCode = oOrderPurchase.CardCode,
            .U_DOCUMENTO_WMS = vIdRecepcionEnc,
            .U_ENVIADO_SAP_WMS = FormatoFechas.tFechaHoraSAP(Now),
            .DocDate = Date.Today,
            .DocDueDate = Date.Today,
            .Comments = $"{oOrderPurchase.Comments} - Entrega generada desde WMS IdRecepcion: {IdRecepcionEnc} IdOcEnc: {BeTransOCEnc.IdOrdenCompraEnc}",
            .DocumentLines = New List(Of FacturaReservaEntregaLineDto)()
        }

            ' Helper local: agrega o fusiona una línea en 'entrega'
            Dim AddOrMergeLine As Action(Of FacturaReservaEntregaLineDto) =
            Sub(newLine As FacturaReservaEntregaLineDto)
                Dim existing = entrega.DocumentLines.FirstOrDefault(Function(x) _
                    x.BaseEntry = newLine.BaseEntry AndAlso
                    x.BaseLine = newLine.BaseLine AndAlso
                    String.Equals(x.ItemCode, newLine.ItemCode, StringComparison.OrdinalIgnoreCase) AndAlso
                    String.Equals(x.WarehouseCode, newLine.WarehouseCode, StringComparison.OrdinalIgnoreCase) AndAlso
                    String.Equals(x.U_Color, newLine.U_Color, StringComparison.OrdinalIgnoreCase) AndAlso
                    String.Equals(x.U_Talla, newLine.U_Talla, StringComparison.OrdinalIgnoreCase))

                If existing Is Nothing Then
                    entrega.DocumentLines.Add(newLine)
                Else
                    ' Suma cantidades y fusiona lotes
                    existing.Quantity += newLine.Quantity
                    If newLine.BatchNumbers IsNot Nothing Then
                        If existing.BatchNumbers Is Nothing Then existing.BatchNumbers = New List(Of BatchNumberDto)()
                        For Each bn In newLine.BatchNumbers
                            Dim exBn = existing.BatchNumbers.FirstOrDefault(Function(b) String.Equals(b.BatchNumber, bn.BatchNumber, StringComparison.OrdinalIgnoreCase))
                            If exBn Is Nothing Then
                                existing.BatchNumbers.Add(New BatchNumberDto With {.BatchNumber = bn.BatchNumber, .Quantity = bn.Quantity})
                            Else
                                exBn.Quantity += bn.Quantity
                            End If
                        Next
                    End If
                End If
            End Sub

            '--- Recorre líneas del documento base y arma líneas por bodega ---
            For Each docLine In oOrderPurchase.DocumentLines
                Dim vCodigoProductoSAP As String = docLine.ItemCode
                Dim vNoLineaOCSAP As Integer = docLine.LineNum

                ' Obtiene IdProductoBodega y detalle OC esperado (para saber cantidad esperada)
                Dim vIdProducto As Integer = clsLnProducto.Get_IdProductoBodega_By_Codigo(docLine.ItemCode, lConnection, lTransaction)
                Dim vIdProductoBodega As Integer = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(vIdProducto, BeTransOCEnc.IdBodega)
                Dim vProductoTc As String = docLine.ItemCode & docLine.U_Talla & docLine.U_Color
                Dim BeTransOcDet = clsLnTrans_oc_det.Get_Single_By_Recepcion_Det_For_Inav(BeTransOCEnc.IdOrdenCompraEnc, vIdProductoBodega, docLine.LineNum, vProductoTc, lConnection, lTransaction)

                If BeTransOcDet Is Nothing Then
                    Throw New Exception("❌ ERROR: No se encontró el detalle de la orden de compra para el producto: " & vProductoTc)
                End If

                Dim vCantidadEsperada As Double = BeTransOcDet.Cantidad

                ' Agrupa lo recibido en WMS para este producto + línea
                Dim DistinctProductosLineas = lINavTransaccionesOut.
                Where(Function(x) x.Codigo_producto = vCodigoProductoSAP AndAlso x.No_linea = vNoLineaOCSAP).
                GroupBy(Function(x) New With {Key x.Codigo_producto, Key x.No_linea, Key x.IdProductoTallaColor, Key x.Idrecepcionenc, Key x.Idproductobodega}).
                Select(Function(g) New With {
                    g.Key.Codigo_producto,
                    g.Key.No_linea,
                    g.Key.IdProductoTallaColor,
                    g.Key.Idrecepcionenc,
                    g.Key.Idproductobodega,
                    .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                }).ToList()

                If DistinctProductosLineas.Any() Then
                    For Each ProductoIngreso In DistinctProductosLineas
                        ' Talla/Color
                        Dim vColor As String = ""
                        Dim vTalla As String = ""
                        Dim DT As DataTable = clsLnProducto_talla_color.Get_All_Dt_By_IdProductoTallaColor(ProductoIngreso.IdProductoTallaColor, lConnection, lTransaction)
                        If DT IsNot Nothing AndAlso DT.Rows.Count > 0 Then
                            vColor = DT.Rows(0)("Color").ToString()
                            vTalla = DT.Rows(0)("Talla").ToString()
                        Else
                            Dim erromsg As String = "❌ ERROR: No se encontró la combinación de talla/color para IdProductoTallaColor: " & ProductoIngreso.IdProductoTallaColor
                            clsPublic.Actualizar_Progreso(lblprg, erromsg)
                            Throw New Exception(erromsg)
                        End If

                        Dim qtyRecibida As Double = Math.Max(0, ProductoIngreso.Cantidad_Total)
                        Dim qtyFaltante As Double = Math.Max(0, vCantidadEsperada - qtyRecibida)

                        ' Lote recibido (si aplica)
                        Dim batchRec As List(Of BatchNumberDto) = Nothing
                        If qtyRecibida > 0 Then
                            batchRec = New List(Of BatchNumberDto) From {
                            New BatchNumberDto With {.BatchNumber = vColor & vTalla, .Quantity = qtyRecibida}
                        }
                        End If

                        ' Línea a bodega de recibido / importación
                        If qtyRecibida > 0 Then
                            AddOrMergeLine(New FacturaReservaEntregaLineDto With {
                            .BaseType = 18,                   ' Orden de compra (ajusta si tu base es distinta)
                            .BaseEntry = oOrderPurchase.DocEntry,
                            .BaseLine = vNoLineaOCSAP,
                            .ItemCode = vCodigoProductoSAP,
                            .Quantity = qtyRecibida,
                            .WarehouseCode = If(vEsImportacion, vCodigoBodegaImportacion, docLine.WarehouseCode),
                            .U_Color = vColor,
                            .U_Talla = vTalla,
                            .BatchNumbers = batchRec
                        })
                        End If

                        ' Línea a bodega de faltantes
                        If qtyFaltante > 0 Then
                            If String.IsNullOrWhiteSpace(BeINavConfigEnc.Bodega_Faltante) Then
                                Dim erromsg As String = "❌ ERROR: No está configurada la bodega de faltantes en la configuración de integración."
                                clsPublic.Actualizar_Progreso(lblprg, erromsg)
                                Throw New Exception(erromsg)
                            End If

                            Dim batchFal As New List(Of BatchNumberDto) From {
                            New BatchNumberDto With {.BatchNumber = vColor & vTalla, .Quantity = qtyFaltante}
                        }

                            AddOrMergeLine(New FacturaReservaEntregaLineDto With {
                            .BaseType = 18,
                            .BaseEntry = oOrderPurchase.DocEntry,
                            .BaseLine = vNoLineaOCSAP,
                            .ItemCode = vCodigoProductoSAP,
                            .Quantity = qtyFaltante,
                            .WarehouseCode = BeINavConfigEnc.Bodega_Faltante,
                            .U_Color = vColor,
                            .U_Talla = vTalla,
                            .BatchNumbers = batchFal
                        })
                        End If

                        ' Marca transacciones para actualizar (enviadas)
                        Dim sublista = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = oOrderPurchase.DocEntry _
                                                                 AndAlso x.No_linea = vNoLineaOCSAP _
                                                                 AndAlso x.Codigo_producto = vCodigoProductoSAP _
                                                                 AndAlso x.Enviado = False)
                        If sublista IsNot Nothing AndAlso sublista.Count > 0 Then
                            ' Si requieres usarlas luego, mantén tu lista. Aquí no era usada fuera.
                            ' Lista_A_Actualizar.AddRange(sublista)
                        End If
                    Next
                Else
                    ' No hubo recepción: todo a bodega de faltantes
                    If String.IsNullOrWhiteSpace(BeINavConfigEnc.Bodega_Faltante) Then
                        Throw New Exception("❌ ERROR: No está configurada la bodega de faltantes en la configuración de integración.")
                    End If

                    Dim batchFal As New List(Of BatchNumberDto) From {
                    New BatchNumberDto With {.BatchNumber = docLine.U_Color & docLine.U_Talla, .Quantity = docLine.Quantity}
                }

                    AddOrMergeLine(New FacturaReservaEntregaLineDto With {
                    .BaseType = 18,
                    .BaseEntry = oOrderPurchase.DocEntry,
                    .BaseLine = vNoLineaOCSAP,
                    .ItemCode = docLine.ItemCode,
                    .Quantity = docLine.Quantity,
                    .WarehouseCode = BeINavConfigEnc.Bodega_Faltante,
                    .U_Color = docLine.U_Color,
                    .U_Talla = docLine.U_Talla,
                    .BatchNumbers = batchFal
                })
                End If
            Next

            '--- Si no hay líneas, nada que enviar ---
            If entrega.DocumentLines Is Nothing OrElse entrega.DocumentLines.Count = 0 Then
                clsPublic.Actualizar_Progreso(lblprg, "No se generaron líneas para la entrega única.")
                Return False
            End If

            '--- POST del documento ÚNICO ---
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
            ServicePointManager.Expect100Continue = False
            ServicePointManager.FindServicePoint(New Uri(baseUrl)).ConnectionLeaseTimeout = 0

            Dim handler As New HttpClientHandler With {
            .AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate,
            .ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True,
            .UseCookies = False
        }

            Using httpClient As New HttpClient(handler) With {.BaseAddress = New Uri(baseUrl)}
                Try
                    Dim entregaJson As String = JsonConvert.SerializeObject(entrega)
                    Dim content = New StringContent(entregaJson, Encoding.UTF8)
                    Dim mediaType = New Headers.MediaTypeHeaderValue("application/json") : mediaType.CharSet = "utf-8"
                    content.Headers.ContentType = mediaType

                    Dim postRequest As New HttpRequestMessage(HttpMethod.Post, "PurchaseDeliveryNotes") With {.Content = content}
                    postRequest.Headers.Add("Cookie", $"B1SESSION={SessionId}; ROUTEID={RouteId}")
                    postRequest.Headers.Host = "hanab1"
                    postRequest.Headers.ConnectionClose = True

                    Dim postResp = Await httpClient.SendAsync(postRequest).ConfigureAwait(False)
                    Dim postContent = Await postResp.Content.ReadAsStringAsync().ConfigureAwait(False)

                    If Not postResp.IsSuccessStatusCode Then
                        clsPublic.Actualizar_Progreso(lblprg, $"❌ ERROR {postResp.StatusCode}:")
                        clsPublic.Actualizar_Progreso(lblprg, postContent)
                        Return False
                    End If

                    ' Parseo seguro de la respuesta
                    Dim docEntry As Integer = -1, docNum As Integer = -1
                    Try
                        Dim jsonObj As JObject = JObject.Parse(postContent)
                        docEntry = jsonObj.Value(Of Integer?)("DocEntry").GetValueOrDefault(-1)
                        docNum = jsonObj.Value(Of Integer?)("DocNum").GetValueOrDefault(-1)
                    Catch
                        ' Ignorar parseo si el SL devolvió algo inesperado
                    End Try

                    BeTransReOc.No_Erp_Docnum_Entrega = docNum
                    BeTransReOc.No_Erp_Docentry_Entrega = docEntry

                    clsLnTrans_re_oc.Actualizar_No_Entrega_ERP(BeTransReOc, lConnection, lTransaction)
                    clsLnLog_error_wms.Agregar_Error($"Se envió la entrega a SAP para IdOrdenCompraEnc: {BeTransOCEnc.IdOrdenCompraEnc} NoDocumento: {docNum} DocEntry: {docEntry}")

                    result = True
                Catch ex As Exception
                    clsPublic.Actualizar_Progreso(lblprg, $"❌ EXCEPCIÓN POST: {ex.Message}")
                    result = False
                End Try
            End Using

            Return result

        Catch ex As HttpRequestException
            Debug.WriteLine($"Excepción en Procesar_Detalle_Ingreso_HANA: {ex.Message}")
            Throw
        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, $"❌ ERROR: {ex.Message}")
            Return False
        End Try
    End Function

    Public Async Function Procesar_Detalle_Ingreso_HANA_Entregas_Separadas(oOrderPurchase As FacturaReservaDto,
                                                                          BeINavConfigEnc As clsBeI_nav_config_enc,
                                                                          BeTransOCEnc As clsBeTrans_oc_enc,
                                                                          IdRecepcionEnc As Integer,
                                                                          lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                                          lblprg As RichTextBox,
                                                                          lConnection As SqlConnection,
                                                                          lTransaction As SqlTransaction) As Task(Of Boolean)

        Dim result As Boolean = False
        Dim vCodigoAnterior As String = ""
        Dim vNoLineaAnterior As Integer = -1
        Dim vAgregarEntrega As Boolean = False
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim BeTransOCTi As New clsBeTrans_oc_ti
        Dim vEsImportacion As Boolean = False
        Dim vCodigoBodegaImportacion As String = ""
        Dim vGeneroDocumentoFaltante As Boolean = False
        Dim BeTransOcDet As clsBeTrans_oc_det = Nothing
        Dim vCantidadEsperada As Double = 0
        Dim vRecibioParcial As Boolean = False
        Dim vResultEntrega As Boolean = False
        Dim vResultEntregaFaltante As Boolean = False
        Dim BeReOc As clsBeTrans_re_oc = Nothing

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Enviando documento: " & BeTransOCEnc.No_Documento)

            BeTransOCTi = clsLnTrans_oc_ti.GetSingle(BeTransOCEnc.IdTipoIngresoOC, lConnection, lTransaction)

            If Not BeTransOCTi Is Nothing Then
                vEsImportacion = BeTransOCTi.Es_Importacion
                vCodigoBodegaImportacion = BeINavConfigEnc.Bodega_Prorrateo
            End If

            If vEsImportacion Then
                If String.IsNullOrWhiteSpace(vCodigoBodegaImportacion) Then
                    Throw New Exception("❌ ERROR: No está configurada la bodega de prorrateo en la configuración de integración.")
                End If
            End If

            BeReOc = clsLnTrans_re_oc.Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc(BeTransOCEnc.IdOrdenCompraEnc, lINavTransaccionesOut.FirstOrDefault.Idrecepcionenc, lConnection, lTransaction)

            Dim vOperadorHHWMS As String = clsLnTrans_re_det.Get_IdOperadorDefecto_By_IdRecepcionEnc(IdRecepcionEnc)

            ' Armar objeto de entrega
            Dim entrega As New FacturaReservaEntregaDto With {
            .CardCode = oOrderPurchase.CardCode,
            .DocDate = Date.Today,
            .DocDueDate = Date.Today,
            .Comments = oOrderPurchase.Comments & " - Entrega generada desde WMS IdRecepcion: " & IdRecepcionEnc & " IdOcEnc: " & BeTransOCEnc.IdOrdenCompraEnc,
            .U_OPERADOR_WMS = vOperadorHHWMS,
            .U_DOCUMENTO_WMS = IdRecepcionEnc,
            .U_ENVIADO_SAP_WMS = FormatoFechas.tFechaHoraSAP(Now),
            .DocumentLines = New List(Of FacturaReservaEntregaLineDto)()
            }

            ' Armar objeto de entrega
            Dim entregaProductoFaltante As New FacturaReservaEntregaDto With {
            .CardCode = oOrderPurchase.CardCode,
            .DocDate = Date.Today,
            .DocDueDate = Date.Today,
            .Comments = oOrderPurchase.Comments & " - Entrega generada desde WMS IdRecepcion: " & IdRecepcionEnc & " IdOcEnc: " & BeTransOCEnc.IdOrdenCompraEnc,
            .U_OPERADOR_WMS = vOperadorHHWMS,
            .U_DOCUMENTO_WMS = IdRecepcionEnc,
            .U_ENVIADO_SAP_WMS = FormatoFechas.tFechaHoraSAP(Now),
            .DocumentLines = New List(Of FacturaReservaEntregaLineDto)()
            }

            For Each docLine In oOrderPurchase.DocumentLines

                Dim vProductoTc = docLine.ItemCode & docLine.U_Color & docLine.U_Talla
                Dim vIdProductoBodega As Integer = clsLnProducto.Get_IdProductoBodega_By_Codigo_And_IdBodega(docLine.ItemCode, BeTransOCEnc.IdBodega, lConnection, lTransaction)
                'Dim vIdProductoBodega As Integer = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(vIdProducto, BeTransOCEnc.IdBodega, lConnection, lTransaction)

                BeTransOcDet = clsLnTrans_oc_det.Get_Single_By_Recepcion_Det_For_Inav(BeTransOCEnc.IdOrdenCompraEnc, vIdProductoBodega, docLine.LineNum, vProductoTc, lConnection, lTransaction)

                Dim vCodigoProductoSAP As String = docLine.ItemCode
                Dim vNoLineaOCSAP As Integer = docLine.LineNum

                Dim DistinctProductosLineas = lINavTransaccionesOut.
                Where(Function(x) x.Codigo_producto = vCodigoProductoSAP AndAlso x.No_linea = vNoLineaOCSAP).
                GroupBy(Function(x) New With {Key x.Codigo_producto, Key x.No_linea, Key x.IdProductoTallaColor, Key x.Idrecepcionenc, Key x.Idproductobodega}).
                Select(Function(g) New With {
                    g.Key.Codigo_producto,
                    g.Key.No_linea,
                    g.Key.IdProductoTallaColor,
                    g.Key.Idrecepcionenc,
                    g.Key.Idproductobodega,
                    .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                }).ToList()

                If DistinctProductosLineas.Any() Then

                    For Each ProductoIngreso In DistinctProductosLineas

                        ' Obtener Talla y Color
                        Dim vColor As String = ""
                        Dim vTalla As String = ""

                        Dim DT As DataTable = clsLnProducto_talla_color.Get_All_Dt_By_IdProductoTallaColor(ProductoIngreso.IdProductoTallaColor, lConnection, lTransaction)
                        If DT IsNot Nothing AndAlso DT.Rows.Count > 0 Then
                            vColor = DT.Rows(0)("Color").ToString()
                            vTalla = DT.Rows(0)("Talla").ToString()
                        End If

                        If BeTransOcDet Is Nothing Then
                            Throw New Exception("❌ ERROR: No se encontró el detalle de la orden de compra para el producto: " & vProductoTc)
                        Else
                            vCantidadEsperada = BeTransOcDet.Cantidad
                        End If

                        vRecibioParcial = (vCantidadEsperada - ProductoIngreso.Cantidad_Total) <> 0

                        Dim nuevaLineaEntrega = (vCodigoAnterior <> ProductoIngreso.Codigo_producto OrElse vNoLineaAnterior <> ProductoIngreso.No_linea)

                        If nuevaLineaEntrega Then

                            If Not vRecibioParcial Then

                                Dim batchList As New List(Of BatchNumberDto) From {
                               New BatchNumberDto With {
                                                   .BatchNumber = vColor & vTalla,
                                                   .Quantity = ProductoIngreso.Cantidad_Total
                                                       }
                                                   }

                                ' Agregar línea a la entrega
                                entrega.DocumentLines.Add(New FacturaReservaEntregaLineDto With {
                                .BaseType = 18, ' Orden de compra
                                .BaseEntry = oOrderPurchase.DocEntry,
                                .BaseLine = vNoLineaOCSAP,
                                .ItemCode = ProductoIngreso.Codigo_producto,
                                .Quantity = ProductoIngreso.Cantidad_Total,
                                .WarehouseCode = IIf(vEsImportacion, vCodigoBodegaImportacion, docLine.WarehouseCode),
                                .U_Color = vColor,
                                .U_Talla = vTalla,
                                .BatchNumbers = batchList
                            })

                                ' Marcar líneas a actualizar
                                Dim Sublista = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = oOrderPurchase.DocEntry _
                                                                      AndAlso x.No_linea = vNoLineaOCSAP _
                                                                      AndAlso x.Codigo_producto = vCodigoProductoSAP _
                                                                      AndAlso x.Enviado = False)
                                If Sublista IsNot Nothing AndAlso Sublista.Count > 0 Then
                                    Lista_A_Actualizar.AddRange(Sublista)
                                End If

                                vCodigoAnterior = ProductoIngreso.Codigo_producto
                                vNoLineaAnterior = ProductoIngreso.No_linea
                                vAgregarEntrega = True

                            Else

                                Dim vDifFaltante As Double = vCantidadEsperada - ProductoIngreso.Cantidad_Total

                                '#CKFK20251119 quité aquí .Quantity = ProductoIngreso.Cantidad_Total + vDifFaltante
                                Dim batchList As New List(Of BatchNumberDto) From {
                               New BatchNumberDto With {
                                                   .BatchNumber = vColor & vTalla,
                                                   .Quantity = ProductoIngreso.Cantidad_Total
                                                       }
                                                   }

                                '#CKFK20251119 quité aquí .Quantity = ProductoIngreso.Cantidad_Total + vDifFaltante
                                ' Agregar línea a la entrega de lo recibido parcialmente
                                entrega.DocumentLines.Add(New FacturaReservaEntregaLineDto With {
                                .BaseType = 18, ' Orden de compra
                                .BaseEntry = oOrderPurchase.DocEntry,
                                .BaseLine = vNoLineaOCSAP,
                                .ItemCode = ProductoIngreso.Codigo_producto,
                                .Quantity = ProductoIngreso.Cantidad_Total,
                                .WarehouseCode = IIf(vEsImportacion, vCodigoBodegaImportacion, docLine.WarehouseCode),
                                .U_Color = vColor,
                                .U_Talla = vTalla,
                                .BatchNumbers = batchList
                            })

                                If BeTransOCEnc.IdEstadoOC = clsDataContractDI.tEstadoOC.CERRADA Then

                                    Dim batchListParcial As New List(Of BatchNumberDto) From {
                                      New BatchNumberDto With {
                                                          .BatchNumber = vColor & vTalla,
                                                          .Quantity = vCantidadEsperada - ProductoIngreso.Cantidad_Total
                                                              }
                                                          }

                                    ' Agregar línea a la entrega
                                    entregaProductoFaltante.DocumentLines.Add(New FacturaReservaEntregaLineDto With {
                                            .BaseType = 18, ' Orden de compra
                                            .BaseEntry = oOrderPurchase.DocEntry,
                                            .BaseLine = vNoLineaOCSAP,
                                            .ItemCode = ProductoIngreso.Codigo_producto,
                                            .Quantity = vCantidadEsperada - ProductoIngreso.Cantidad_Total,
                                            .WarehouseCode = BeINavConfigEnc.Bodega_Faltante,
                                            .U_Color = vColor,
                                            .U_Talla = vTalla,
                                            .BatchNumbers = batchListParcial
                                        })
                                End If

                                ' Marcar líneas a actualizar
                                Dim Sublista = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = oOrderPurchase.DocEntry _
                                                                      AndAlso x.No_linea = vNoLineaOCSAP _
                                                                      AndAlso x.Codigo_producto = vCodigoProductoSAP _
                                                                      AndAlso x.Enviado = False)
                                If Sublista IsNot Nothing AndAlso Sublista.Count > 0 Then
                                    Lista_A_Actualizar.AddRange(Sublista)
                                End If

                                vCodigoAnterior = ProductoIngreso.Codigo_producto
                                vNoLineaAnterior = ProductoIngreso.No_linea
                                vAgregarEntrega = True

                                vRecibioParcial = True

                            End If

                        End If

                    Next

                Else

                    If BeINavConfigEnc.Bodega_Faltante = "" Then
                        Throw New Exception("❌ ERROR: No está configurada la bodega de faltantes en la configuración de integración.")
                    End If

                    vGeneroDocumentoFaltante = True

                    Dim nuevaLineaEntrega = (vCodigoAnterior <> docLine.ItemCode OrElse vNoLineaAnterior <> docLine.LineNum)

                    If nuevaLineaEntrega Then

                        Dim batchList As New List(Of BatchNumberDto) From {
                            New BatchNumberDto With {
                                                .BatchNumber = docLine.U_Color & docLine.U_Talla,
                                                .Quantity = docLine.Quantity
                                                    }
                                                }

                        'Agregar línea a la entrega
                        entregaProductoFaltante.DocumentLines.Add(New FacturaReservaEntregaLineDto With {
                        .BaseType = 18, 'Orden de compra
                        .BaseEntry = oOrderPurchase.DocEntry,
                        .BaseLine = vNoLineaOCSAP,
                        .ItemCode = docLine.ItemCode,
                        .Quantity = docLine.Quantity,
                        .WarehouseCode = BeINavConfigEnc.Bodega_Faltante,
                        .U_Color = docLine.U_Color,
                        .U_Talla = docLine.U_Talla,
                        .BatchNumbers = batchList
                    })

                        vCodigoAnterior = docLine.ItemCode
                        vNoLineaAnterior = docLine.LineNum
                        vAgregarEntrega = True

                    End If

                End If

            Next

            If vAgregarEntrega Then

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                ServicePointManager.Expect100Continue = False
                ServicePointManager.FindServicePoint(New Uri(baseUrl)).ConnectionLeaseTimeout = 0

                Dim handler As New HttpClientHandler With {
                    .AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate,
                    .ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True,
                    .UseCookies = False
                }

                Using httpClient As New HttpClient(handler) With {.BaseAddress = New Uri(baseUrl)}

                    Dim entregaJson As String = JsonConvert.SerializeObject(entrega)
                    Dim content = New StringContent(entregaJson, Encoding.UTF8)
                    Dim mediaType = New Headers.MediaTypeHeaderValue("application/json")
                    mediaType.CharSet = "utf-8"
                    content.Headers.ContentType = mediaType

                    Dim postRequest As New HttpRequestMessage(HttpMethod.Post, "PurchaseDeliveryNotes") With {
                        .Content = content
                    }
                    postRequest.Headers.Add("Cookie", $"B1SESSION={SessionId}; ROUTEID={RouteId}")
                    postRequest.Headers.Host = "hanab1"
                    postRequest.Headers.ConnectionClose = True

                    '#EJC 2024-06-06 Se agrega envío de productos recibidos.
                    Try

                        Dim postResp = Await httpClient.SendAsync(postRequest)
                        Dim postContent = Await postResp.Content.ReadAsStringAsync()

                        ' Parsear el JSON
                        Dim jsonObj As JObject = JObject.Parse(postContent)

                        If postResp.IsSuccessStatusCode Then

                            vResultEntrega = True

                            ' Capturar los valores
                            Dim docEntry As Integer = jsonObj("DocEntry")
                            Dim docNum As Integer = jsonObj("DocNum")

                            clsPublic.Actualizar_Progreso(lblprg, "✅ Respuesta:")
                            clsPublic.Actualizar_Progreso(lblprg, "Se envió la entrega a sap para el IdOrdenCompraEnc: " & BeTransOCEnc.IdOrdenCompraEnc & " NoDocumento: " & docNum & " DocEntry: " & docEntry)
                            'clsPublic.Actualizar_Progreso(lblprg, postContent)

                            clsLnTrans_oc_enc.Actualizar_No_Documento_Recepcion_ERP(docNum, BeTransOCEnc.IdOrdenCompraEnc, lConnection, lTransaction)
                            clsLnTrans_oc_enc.Actualizar_NoMarchamo(docEntry, BeTransOCEnc.IdOrdenCompraEnc, lConnection, lTransaction)

                            '#CKFK20251119: Actualizar las transacciones enviadas a entregas faltantes
                            clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(Lista_A_Actualizar, lConnection, lTransaction)

                            BeReOc.No_Erp_Docentry_Entrega = docEntry
                            BeReOc.No_Erp_Docnum_Entrega = docNum
                            clsLnTrans_re_oc.Actualizar(BeReOc, lConnection, lTransaction)

                            clsLnLog_error_wms.Agregar_Error("Se envió la entrega a sap para el IdOrdenCompraEnc: " & BeTransOCEnc.IdOrdenCompraEnc & " NoDocumento: " & docNum & " DocEntry: " & docEntry)

                            ' ====== PATCH: Actualizar UDF U_DOCUMENTO_WMS del Purchase Order ======
                            Try

                                Dim vOperadorWMSBof As String = clsLnTrans_oc_enc.Get_Usuario_Defecto_By_IdOrdenCompraEnc(IdRecepcionEnc)

                                ' Construir el cuerpo con solo el UDF que quieres actualizar
                                Dim patchObj As New JObject From {
                                {"U_DOCUMENTO_WMS", BeTransOCEnc.IdOrdenCompraEnc.ToString()},
                                {"U_OPERADOR_WMS", vOperadorWMSBof}
                                }

                                Dim patchContent As New StringContent(patchObj.ToString(), Encoding.UTF8, "application/json")

                                ' Importante: PATCH al documento base (PurchaseOrders) usando el DocEntry del PO original
                                Dim patchRequest As New HttpRequestMessage(New HttpMethod("PATCH"), $"PurchaseInvoices({BeTransOCEnc.Referencia})") _
                                    With {.Content = patchContent}

                                ' Reutiliza las mismas cookies / headers de sesión que usaste para el POST
                                patchRequest.Headers.Add("Cookie", $"B1SESSION={SessionId}; ROUTEID={RouteId}")
                                patchRequest.Headers.Host = "hanab1"
                                patchRequest.Headers.ConnectionClose = True

                                Dim patchResp = Await httpClient.SendAsync(patchRequest)
                                Dim patchContentResp = Await patchResp.Content.ReadAsStringAsync()

                                If patchResp.IsSuccessStatusCode Then
                                    clsPublic.Actualizar_Progreso(lblprg, "✅ UDF U_DOCUMENTO_WMS actualizado en la OC.")
                                Else
                                    clsPublic.Actualizar_Progreso(lblprg, $"❌ ERROR PATCH OC {patchResp.StatusCode}:")
                                    clsPublic.Actualizar_Progreso(lblprg, patchContentResp)
                                End If

                            Catch ex As Exception
                                clsPublic.Actualizar_Progreso(lblprg, $"❌ EXCEPCIÓN PATCH OC: {ex.Message}")
                            End Try
                            ' ====== FIN PATCH ======

                        Else
                            clsPublic.Actualizar_Progreso(lblprg, $"❌ ERROR {postResp.StatusCode}:")
                            clsPublic.Actualizar_Progreso(lblprg, postContent)
                        End If

                    Catch ex As Exception
                        clsPublic.Actualizar_Progreso(lblprg, $"❌ EXCEPCIÓN POST: {ex.Message}")
                    End Try

                End Using

                If vGeneroDocumentoFaltante And vResultEntrega Then

                    Dim handlerFaltante As New HttpClientHandler With {
                    .AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate,
                    .ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True,
                    .UseCookies = False
                }

                    Using httpClientFaltante As New HttpClient(handlerFaltante) With {.BaseAddress = New Uri(baseUrl)}

                        '#EJC 2024-06-06 Se agrega envío de productos faltantes.
                        Try

                            Dim entregaJsonFaltantes As String = JsonConvert.SerializeObject(entregaProductoFaltante)
                            Dim contentFaltante = New StringContent(entregaJsonFaltantes, Encoding.UTF8)
                            Dim mediaTypeFaltante = New Headers.MediaTypeHeaderValue("application/json")
                            mediaTypeFaltante.CharSet = "utf-8"
                            contentFaltante.Headers.ContentType = mediaTypeFaltante

                            Dim postRequestFaltante As New HttpRequestMessage(HttpMethod.Post, "PurchaseDeliveryNotes") With {
                            .Content = contentFaltante
                        }
                            postRequestFaltante.Headers.Add("Cookie", $"B1SESSION={SessionId}; ROUTEID={RouteId}")
                            postRequestFaltante.Headers.Host = "hanab1"
                            postRequestFaltante.Headers.ConnectionClose = True

                            Dim postRespFaltante = Await httpClientFaltante.SendAsync(postRequestFaltante)
                            Dim postContentFaltante = Await postRespFaltante.Content.ReadAsStringAsync()

                            ' Parsear el JSON
                            Dim jsonObj As JObject = JObject.Parse(postContentFaltante)

                            ' Capturar los valores
                            Dim docEntry As Integer = jsonObj("DocEntry")
                            Dim docNum As Integer = jsonObj("DocNum")

                            If postRespFaltante.IsSuccessStatusCode Then

                                clsPublic.Actualizar_Progreso(lblprg, "✅ Respuesta:")
                                clsPublic.Actualizar_Progreso(lblprg, "Se creó la entrega (faltante): " & docNum)

                                '#EJC20251014: Aquí actualizar en la re_oc por favor carolina.
                                clsLnTrans_oc_enc.Actualizar_No_Documento_Recepcion_ERP(docNum, BeTransOCEnc.IdOrdenCompraEnc, lConnection, lTransaction)
                                clsLnTrans_oc_enc.Actualizar_NoMarchamo(docEntry, BeTransOCEnc.IdOrdenCompraEnc, lConnection, lTransaction)

                                '#CKFK20251119: Actualizar las transacciones enviadas a entregas faltantes
                                clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(Lista_A_Actualizar, lConnection, lTransaction)

                                clsLnLog_error_wms.Agregar_Error("Se envió la entrega a sap para el IdOrdenCompraEnc: " & BeTransOCEnc.IdOrdenCompraEnc & " NoDocumento: " & docNum & " DocEntry: " & docEntry)

                                BeReOc.No_Erp_Docentry_Faltante = docEntry
                                BeReOc.No_Erp_Docnum_Faltante = docNum
                                clsLnTrans_re_oc.Actualizar(BeReOc, lConnection, lTransaction)

                                vResultEntregaFaltante = True

                            Else
                                clsPublic.Actualizar_Progreso(lblprg, $"❌ ERROR {postRespFaltante.StatusCode}:")
                                clsPublic.Actualizar_Progreso(lblprg, postContentFaltante)
                            End If

                        Catch ex As Exception
                            clsPublic.Actualizar_Progreso(lblprg, $"❌ EXCEPCIÓN POST: {ex.Message}")
                        End Try

                    End Using

                End If

            End If

            result = If(vGeneroDocumentoFaltante, (vResultEntrega AndAlso vResultEntregaFaltante), vResultEntrega)

            Return result

        Catch ex As HttpRequestException
            Debug.WriteLine($"Excepción en Procesar_Detalle_Ingreso_HANA: {ex.Message}")
            Throw
        End Try

    End Function

    '#EJCCKF20260519_Notificar_SAP_Hana_MAMPA: Estados SAP HANA SL para el flujo operativo MAMAPA desde Pedido.
    ' 1=Nueva / disponible para reasignar picking; 2=Asignado; 3=Pickeando; 4=Pickeado; 5=Verificando; 6=Verificado.
    ' 8=Cerrada/entregada; 11=Anulada al anular/eliminar pedido; 12=Back order. Se notifica despues del commit WMS.
    Private Const TAG_NOTIFICAR_SAP_HANA_MAMPA As String = "#EJCCKF20260519_Notificar_SAP_Hana_MAMPA"

    Public Shared Async Function Notificar_Estado_SAP_Hana_MAMPA_Pedido_Async(ByVal pPedidoEnc As clsBeTrans_pe_enc,
                                                                              ByVal pEstadoPedido As Integer,
                                                                              ByVal pEstadoFactura As Integer,
                                                                              ByVal pEstadoGuia As Integer,
                                                                              ByVal pInterface_SAP As Boolean,
                                                                              ByVal pIdUsuario As Integer,
                                                                              ByVal pIdEmpresa As Integer) As Task

        If pPedidoEnc Is Nothing Then Return
        If String.IsNullOrWhiteSpace(pPedidoEnc.Referencia) Then Return
        If Not pInterface_SAP Then Return
        If String.IsNullOrWhiteSpace(baseUrl) Then Return

        Try
            If pPedidoEnc.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Factura_Deudor OrElse
               pPedidoEnc.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Factura_Reserva_Cliente Then

                Dim vHanaService As New SapServiceLayerClient()
                Dim loginResponse As LoginResponseDto = Await vHanaService.LoginAsync()

                Await Cambiar_Estado_Traslado_SLAsync(pPedidoEnc.Referencia,
                                                      vHanaService.SessionCookie,
                                                      baseUrl,
                                                      pEstadoPedido,
                                                      pEstadoFactura,
                                                      pEstadoGuia,
                                                      Now,
                                                      pIdUsuario,
                                                      Now,
                                                      Now,
                                                      pIdUsuario,
                                                      Now)

            End If

        Catch ex As Exception
            clsLnLog_error_wms_pe.Agregar_Error(TAG_NOTIFICAR_SAP_HANA_MAMPA & ": No se pudo notificar estado SAP HANA SL desde pedido. EstadoPedido=" & pEstadoPedido & ". " & ex.Message,
                                                pIdEmpresa:=pIdEmpresa,
                                                pIdBodega:=pPedidoEnc.IdBodega,
                                                pUsrAgr:=pIdUsuario,
                                                pIdPedidoEnc:=pPedidoEnc.IdPedidoEnc)
        End Try

    End Function

    Public Shared Async Function Cambiar_Estado_Traslado_SLAsync(docEntry As String,
                                                                 sessionCookie As String,
                                                                 baseUrl As String,
                                                                 estado_pedido As Integer,
                                                                 estado_factura As Integer,
                                                                 estado_guia As Integer,
                                                                 inicio_pick As Date,
                                                                 usr_pick As String,
                                                                 fin_pick As Date,
                                                                 inicio_pack As Date,
                                                                 usr_pack As String,
                                                                 fin_pack As Date) As Task(Of Boolean)

        Try

            If String.IsNullOrWhiteSpace(docEntry) Then Return False

            Dim requestUrl As String = $"Invoices({docEntry})"
            Dim payload As String = $"{{" &
                                    $"""U_ESTADO_GUIA"": ""{estado_guia}""," &
                                    $"""U_ESTADO_PEDIDO"": ""{estado_pedido}""," &
                                    $"""U_ESTADO_FACTURA"": ""{estado_factura}"""

            If estado_pedido = 3 Then
                payload &= $",""U_INICIO_PICK"": ""{inicio_pick}""," &
               $"""U_USR_PICK"": ""{usr_pick}"""
            End If

            If estado_pedido = 4 Then
                payload &= $",""U_FIN_PICK"": ""{fin_pick}"""
            End If

            If estado_pedido = 5 Then
                payload &= $",""U_INICIO_PACK"": ""{inicio_pack}""," &
               $"""U_USR_PACK"": ""{usr_pack}"""
            End If

            If estado_pedido = 6 Then
                payload &= $",""U_FIN_PACK"": ""{fin_pack}"""
            End If

            payload &= "}"

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
                            Throw New Exception($"Error al actualizar OWTQ. Código: {response.StatusCode}, Detalle: {errContent}")
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception($"(SL) {MethodBase.GetCurrentMethod().Name} {ex.Message}", ex)
        End Try

    End Function

    Public Shared Sub Notificar_Estado_SAP_Hana_MAMPA_By_Picking(ByVal pIdPickingEnc As Integer,
                                                                  ByVal pEstadoPedido As Integer,
                                                                  ByVal pEstadoFactura As Integer,
                                                                  ByVal pEstadoGuia As Integer,
                                                                  ByVal pUsuario As String,
                                                                  Optional ByVal pIdBodega As Integer = 0)

        If pIdPickingEnc <= 0 Then Return
        If String.IsNullOrWhiteSpace(SapServiceLayerClient.baseUrl) Then Return
        If pIdBodega > 0 AndAlso Not Bodega_Tiene_Interface_SAP(pIdBodega) Then Return

        Dim lPedidos As New List(Of clsBeTrans_pe_enc)

        Try
            Using lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                    lPedidos = clsLnTrans_pe_enc.Get_All_Pedido_By_IdPickingEnc(pIdPickingEnc, lConnection, lTransaction)
                    lTransaction.Commit()
                End Using
            End Using

            If lPedidos Is Nothing OrElse lPedidos.Count = 0 Then Return

            Dim vHanaService As New SapServiceLayerClient()
            Dim loginResponse As LoginResponseDto = vHanaService.LoginAsync().GetAwaiter().GetResult()

            For Each ped In lPedidos

                If ped Is Nothing Then Continue For
                If String.IsNullOrWhiteSpace(ped.Referencia) Then Continue For
                If Not Bodega_Tiene_Interface_SAP(ped.IdBodega) Then Continue For

                If ped.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Factura_Deudor OrElse
                   ped.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Factura_Reserva_Cliente Then

                    Cambiar_Estado_Traslado_SLAsync(ped.Referencia,
                                                    vHanaService.SessionCookie,
                                                    SapServiceLayerClient.baseUrl,
                                                    pEstadoPedido,
                                                    pEstadoFactura,
                                                    pEstadoGuia,
                                                    Now,
                                                    pUsuario,
                                                    Now,
                                                    Now,
                                                    pUsuario,
                                                    Now).GetAwaiter().GetResult()
                End If
            Next

        Catch ex As Exception
            clsLnLog_error_wms_pick.Agregar_Error(TAG_NOTIFICAR_SAP_HANA_MAMPA & ": No se pudo notificar estado SAP HANA SL. EstadoPedido=" & pEstadoPedido & " IdPickingEnc=" & pIdPickingEnc & ". " & ex.Message,
                                                  pIdBodega:=pIdBodega,
                                                  pUserAgr:=pUsuario,
                                                  pIdPickingEnc:=pIdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)
        End Try

    End Sub

    Private Shared Function Bodega_Tiene_Interface_SAP(ByVal pIdBodega As Integer) As Boolean

        Try
            Const vSQL As String = "SELECT ISNULL(Interface_SAP,0) FROM bodega WHERE IdBodega=@IdBodega"

            Using lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()

                Using lCommand As New SqlCommand(vSQL, lConnection)
                    lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                    Return Convert.ToBoolean(lCommand.ExecuteScalar())
                End Using
            End Using

        Catch ex As Exception
            clsLnLog_error_wms_pick.Agregar_Error(TAG_NOTIFICAR_SAP_HANA_MAMPA & ": No se pudo validar Interface_SAP de bodega. " & ex.Message,
                                                  pIdBodega:=pIdBodega,
                                                  pStackTrace:=ex.StackTrace)
            Return False
        End Try

    End Function
End Class