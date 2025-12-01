Imports System.Globalization
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports Newtonsoft.Json

Public Class clsSyncSapAjustes

    Public Enum InvAdjType
        Receipt ' InventoryGenEntries +
        Issue   ' InventoryGenExits -
    End Enum

    Private Const ENTITY_TARGET_INV_GEN_EXITS As String = "InventoryGenExits"
    Private Const ENTITY_TARGET_INV_GEN_ENTRIES As String = "InventoryGenEntries"
    Public Shared Async Function Enviar_Ajustes_WMS_SAP(ByVal lblprg As RichTextBox) As Task(Of Boolean)

        Dim sw As New Stopwatch()
        sw.Start()
        clsPublic.Actualizar_Progreso(lblprg, "Iniciando envío de ajustes WMS a SAP Service Layer...")

        Try
            Dim pResult As String = ""
            Dim ajustes As List(Of clsBeAjustesMI3) = clsLnI_nav_transacciones_out.Get_Ajustes_Pendientes_Envio_MI3(pResult)

            If ajustes Is Nothing OrElse ajustes.Count = 0 Then
                sw.Stop()
                clsPublic.Actualizar_Progreso(lblprg, $"No hay transacciones para procesar. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.")
                Return False
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Conectando a SAP vía SL...")

            Dim vHanaService As New SapServiceLayerClient
            Dim loginResponse As LoginResponseDto = Await vHanaService.LoginAsync().ConfigureAwait(False)

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                sw.Stop()
                clsPublic.Actualizar_Progreso(lblprg, $"No se pudo obtener sesión. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.")
                Return False
            Else
                clsPublic.Actualizar_Progreso(lblprg, "Conexión correcta.")
            End If

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
            ServicePointManager.Expect100Continue = False
            ServicePointManager.FindServicePoint(New Uri(SapServiceLayerClient.baseUrl)).ConnectionLeaseTimeout = 0

            Dim handler As New HttpClientHandler With {
            .AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate,
            .ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True,
            .UseCookies = False
        }

            Dim okGlobal As Boolean = False

            Using http As New HttpClient(handler) With {.BaseAddress = New Uri(SapServiceLayerClient.baseUrl)}

                ' Agrupar por documento y tipo (entrada/salida)
                Dim grupos = ajustes.GroupBy(Function(a) New With {
                                         Key .Doc = a.NoDocumento,
                                         Key .IdAjusteWMS = a.IdAjusteEnc,
                                         Key .Usr_Agr = a.Usr_Agr,
                                         Key .Tipo = If(EsSalida(a), InvAdjType.Issue, InvAdjType.Receipt),
                                         Key .CentroCostoErp = a.Centro_Costo_Erp,
                                         Key .CentroCostoDirErp = a.Centro_Costo_Dir_Erp,
                                         Key .CentroCostoDepErp = a.Centro_Costo_Dep_Erp
                                     })

                For Each g In grupos

                    Dim EndPoint As String = If(g.Key.Tipo = InvAdjType.Issue, ENTITY_TARGET_INV_GEN_EXITS, ENTITY_TARGET_INV_GEN_ENTRIES)
                    Dim docTxt As String = If(g.Key.Tipo = InvAdjType.Issue, "Salida", "Entrada")

                    ' ---------- 2) Construcción de payload ----------
                    Dim payload As InventoryPayload = Build_Inventory_Payload(
                    docDate:=Date.Today,
                    comments:=$"WMS Ajuste {g.Key.Doc} ({docTxt}) IdAjusteWMS:({g.Key.IdAjusteWMS}) ",
                    journalMemo:=$"WMS {docTxt} – MI3",
                    series:=Nothing,
                    centroCostoErp:=clsLnCentro_costo.Get_Codigo_By_IdCentroCosto(g.Key.CentroCostoErp),
                    centroCostoDirErp:=clsLnCentro_costo.Get_Codigo_By_IdCentroCosto(g.Key.CentroCostoDirErp),
                    centroCostoDepErp:=clsLnCentro_costo.Get_Codigo_By_IdCentroCosto(g.Key.CentroCostoDepErp),
                    detalles:=g.ToList()
                )

                    '#CKFK20251028 Agregamos los campos UDFs necesarios
                    payload.U_ENVIADO_WMS = 1
                    payload.U_MOTIVO_WMS = "1"
                    payload.U_OPERADOR_WMS = g.Key.Usr_Agr
                    payload.U_DOCUMENTO_WMS = g.Key.IdAjusteWMS
                    payload.U_INICIO_ENVIO = Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                    payload.U_FIN_ENVIO = Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                    payload.U_ENVIADO_SAP_WMS = FormatoFechas.tFechaHoraSAP(Now)

                    Dim json As String = JsonConvert.SerializeObject(payload, New JsonSerializerSettings With {.NullValueHandling = NullValueHandling.Ignore})
                    Dim content = New StringContent(json, Encoding.UTF8)
                    Dim mediaType = New MediaTypeHeaderValue("application/json") : mediaType.CharSet = "utf-8"
                    content.Headers.ContentType = mediaType

                    ' ---------- 3) POST /InventoryGenEntries|Exits ----------
                    Dim req As New HttpRequestMessage(HttpMethod.Post, EndPoint) With {.Content = content}
                    req.Headers.Add("Cookie", vHanaService.SessionCookie)
                    req.Headers.ConnectionClose = True

                    clsPublic.Actualizar_Progreso(lblprg, $"Publicando {docTxt} {g.Key.Doc} ({payload.DocumentLines.Count} línea(s)) ...")

                    Dim resp = Await http.SendAsync(req).ConfigureAwait(False)
                    Dim body = Await resp.Content.ReadAsStringAsync().ConfigureAwait(False)

                    Dim docEntry As Integer = 0
                    Dim docNum As Integer = 0

                    If resp.IsSuccessStatusCode Then

                        Try
                            Dim jo = Linq.JObject.Parse(body)
                            docEntry = jo.Value(Of Integer?)("DocEntry").GetValueOrDefault()
                            docNum = jo.Value(Of Integer?)("DocNum").GetValueOrDefault()
                        Catch
                        End Try

                        clsPublic.Actualizar_Progreso(lblprg, "✅ Respuesta:")
                        clsPublic.Actualizar_Progreso(lblprg, $"{docTxt} creada: DocNum {docNum} (DocEntry {docEntry})")

                        ' -------- 4) Marcar enviados en WMS --------
                        For Each aj In g
                            Try
                                clsLnTrans_ajuste_det.Actualizar_Estado_Enviado_A_ERP(aj.IdAjusteDet, True)
                                clsLnTrans_ajuste_enc.Actualizar_Referencia(aj.IdAjusteEnc, docNum)
                            Catch
                            End Try
                        Next

                        okGlobal = True
                    Else
                        ' Manejo de error coherente con tus logs
                        clsPublic.Actualizar_Progreso(lblprg, $"❌ Error SL {resp.StatusCode}:")
                        clsPublic.Actualizar_Progreso(lblprg, body)

                        ' log por cada detalle del grupo
                        For Each aj In g
                            Try
                                clsLnI_nav_ejecucion_det_error.Inserta_Log(
                                $"Error SL {CInt(resp.StatusCode)}: {body}",
                                aj.IdAjusteEnc, aj.IdAjusteDet, 300)
                            Catch
                            End Try
                        Next
                    End If

                Next
            End Using

            sw.Stop()
            Dim finMsg As String = If(okGlobal,
                                  $"Proceso finalizado correctamente. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.",
                                  $"Proceso finalizado sin cambios o con errores. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.")
            clsPublic.Actualizar_Progreso(lblprg, finMsg)

            Return okGlobal

        Catch ex As Exception
            If sw.IsRunning Then sw.Stop()
            clsPublic.Actualizar_Progreso(lblprg, $"Error en el envío de ajustes: {ex.Message}. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.")
            Return False
        End Try

    End Function

    Private Shared Function Build_Inventory_Payload(ByVal docDate As Date,
                                                    ByVal comments As String,
                                                    ByVal journalMemo As String,
                                                    ByVal series As Integer?,
                                                    ByVal centroCostoErp As String,
                                                    ByVal centroCostoDirErp As String,
                                                    ByVal centroCostoDepErp As String,
                                                    ByVal detalles As List(Of clsBeAjustesMI3)) As InventoryPayload

        Dim payload As New InventoryPayload With {
        .DocDate = docDate.ToString("yyyy-MM-dd"),
        .Comments = comments,
        .JournalMemo = journalMemo,
        .Series = series,
        .DocumentLines = New List(Of InventoryDocumentLine)
    }

        If detalles Is Nothing OrElse detalles.Count = 0 Then Return payload

        For Each d In detalles
            If d Is Nothing Then Continue For

            ' Cantidad SIEMPRE desde d.Cantidad (positiva; el signo lo determina el endpoint Entries/Exits)
            Dim qty As Decimal = CDec(Math.Abs(d.Cantidad))
            If qty <= 0D Then Continue For

            ' Bodega: prioriza la de ERP si viene
            Dim wh As String = If(Not String.IsNullOrWhiteSpace(d.Codigo_Bodega_ERP), d.Codigo_Bodega_ERP, d.Codigo_Bodega)

            ' Lote = d.Color & d.Talla
            Dim color As String = If(d.Color, String.Empty).Trim()
            Dim talla As String = If(d.Talla, String.Empty).Trim()
            Dim lote As String = color & talla

            ' Construcción de la línea con los campos adicionales
            Dim ln As New InventoryDocumentLine With {
            .ItemCode = d.Codigo_Producto,
            .Quantity = qty,
            .WarehouseCode = wh,
            .U_Color = color,
            .U_Talla = talla,
            .U_MotivoDev = If(d.Motivo_Ajuste, String.Empty),
            .unitMsr = If(d.UMBas, String.Empty),
            .CostingCode = centroCostoErp,
            .CostingCode2 = centroCostoDirErp,
            .CostingCode3 = centroCostoDepErp
             }

            ' BatchNumbers desde Color&Talla
            If Not String.IsNullOrWhiteSpace(lote) Then
                ln.BatchNumbers = New List(Of BatchNumber) From {
                New BatchNumber With {
                    .BatchNumber = lote,
                    .Quantity = qty
                }
            }
            End If

            ' Sanidad: suma de lotes = cantidad de la línea
            If ln.BatchNumbers IsNot Nothing AndAlso ln.BatchNumbers.Count > 0 Then
                Dim sumBatches = ln.BatchNumbers.Sum(Function(b) b.Quantity)
                If sumBatches <> ln.Quantity Then
                    ln.BatchNumbers(0).Quantity = ln.Quantity
                End If
            End If

            payload.DocumentLines.Add(ln)
        Next

        Return payload
    End Function

    ' Decide si un ajuste MI3 debe ir como Salida (Exits) o Entrada (Entries)
    Private Shared Function EsSalida(ByVal a As clsBeAjustesMI3) As Boolean
        ' Usa tus propios criterios; estos son seguros con los datos que ya tenés
        Dim t = (If(a.TipoAjusteWMS, "")).ToUpperInvariant()
        Dim m = (If(a.Motivo_Ajuste, "")).ToUpperInvariant()

        If t.Contains("NEG") OrElse t.Contains("SALID") OrElse t.StartsWith("-") Then Return True
        If m.Contains("MERMA") OrElse m.Contains("DISMIN") OrElse m.Contains("BAJA") Then Return True

        ' Si la lógica del WMS te da el signo con Cantidad (<0 salida, >0 entrada):
        If a.Cantidad < 0 Then Return True

        Return False
    End Function

    Public Class InventoryPayload
        Public Property DocDate As String
        Public Property Comments As String
        Public Property JournalMemo As String
        Public Property Series As Integer?
        Public Property Ref2 As Integer?
        Public Property U_MOTIVO_WMS As String = ""
        Public Property U_OPERADOR_WMS As String = ""
        Public Property U_DOCUMENTO_WMS As Integer = 0
        Public Property U_INICIO_PICK As DateTime = Now
        Public Property U_FIN_PICK As DateTime = Now
        Public Property U_ESTADO_PEDIDO As Integer = 0
        Public Property U_INICIO_ENVIO As DateTime = Now
        Public Property U_FIN_ENVIO As DateTime = Now
        Public Property U_ENVIADO_WMS As Integer = 1
        Public Property U_ENVIADO_SAP_WMS As String = ""
        Public Property DocumentLines As List(Of InventoryDocumentLine)
    End Class

    Public Class InventoryDocumentLine
        Public Property ItemCode As String
        Public Property Quantity As Decimal
        Public Property WarehouseCode As String
        Public Property U_Color As String = ""
        Public Property U_Talla As String = ""
        Public Property U_MotivoDev As String = ""
        Public Property unitMsr As String = ""
        Public Property CostingCode As String = ""
        Public Property CostingCode2 As String = ""
        Public Property CostingCode3 As String = ""
        Public Property BatchNumbers As List(Of BatchNumber)
    End Class

    Public Class BatchNumber
        Public Property BatchNumber As String
        Public Property Quantity As Decimal
    End Class


End Class