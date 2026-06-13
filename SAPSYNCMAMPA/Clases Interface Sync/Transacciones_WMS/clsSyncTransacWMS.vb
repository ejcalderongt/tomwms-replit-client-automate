Imports System.Configuration
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

Public Class clsSyncTransacWMS

    Private Shared vHanaService As SapServiceLayerClient
    Private Const TRANSAC_WMS_OK As Integer = 1
    Private Const TRANSAC_WMS_ERROR As Integer = 2
    Private Const TRANSAC_WMS_ID_EJECUCION_ENC_FALLBACK As Integer = 1900
    Private Const TRANSAC_WMS_ID_NAV_CONFIG_DET_FALLBACK As Integer = 900
    Private Const TRANSAC_WMS_ORIGEN As String = "TRANSAC_WMS"
    Private Const TRANSAC_WMS_SENTIDO As String = "PULL_SAP"
    Private Const TRANSAC_WMS_PROCESS_RESULT_MAX As Integer = 250
    Private Shared mTransacWmsIdEjecucionEnc As Integer = 0
    Private Shared mTransacWmsIdNavConfigDet As Integer = 0

    Private ReadOnly DateFormats As String() = {
       "yyyy-MM-ddTHH:mm:ssK",      ' ISO con zona
       "yyyy-MM-ddTHH:mm:ss",       ' ISO sin zona
       "yyyy-MM-dd",                ' Solo fecha
       "yyyyMMdd",                  ' Compacto
       "dd/MM/yyyy HH:mm:ss",
       "dd/MM/yyyy"
   }

#Region "Traza TRANSAC_WMS"

    '#EJCCKFK20260520: Traza operativa TRANSAC_WMS para errores por documento.
    Private Class TransacWmsTraceContext
        Public Property Proceso As String
        Public Property TipoDocumento As String
        Public Property Documento As String
        Public Property Referencia As String
        Public Property BodegaOrigen As String
        Public Property BodegaDestino As String
        Public Property DocEntries As List(Of Integer)
    End Class

    Private Shared Function CrearContextoTransacWms(proceso As String,
                                                    tipoDocumento As String,
                                                    documento As String,
                                                    referencia As String,
                                                    bodegaOrigen As String,
                                                    bodegaDestino As String,
                                                    docEntries As List(Of Integer)) As TransacWmsTraceContext

        Return New TransacWmsTraceContext With {
            .Proceso = proceso,
            .TipoDocumento = tipoDocumento,
            .Documento = If(documento, ""),
            .Referencia = If(referencia, ""),
            .BodegaOrigen = If(bodegaOrigen, ""),
            .BodegaDestino = If(bodegaDestino, ""),
            .DocEntries = If(docEntries, New List(Of Integer)())
        }
    End Function

    Private Shared Function FormatearDocEntries(docEntries As List(Of Integer)) As String
        If docEntries Is Nothing OrElse docEntries.Count = 0 Then Return ""
        Return String.Join(",", docEntries.Distinct())
    End Function

    Private Shared Function LimitarTexto(valor As String, maxLength As Integer) As String
        If String.IsNullOrEmpty(valor) Then Return ""
        If valor.Length <= maxLength Then Return valor
        Return valor.Substring(0, maxLength)
    End Function

    Private Shared Sub ActualizarProgresoSeguro(lblprg As RichTextBox, mensaje As String)
        If lblprg Is Nothing OrElse String.IsNullOrWhiteSpace(mensaje) Then Return

        Try
            If lblprg.IsDisposed Then Return

            If lblprg.InvokeRequired Then
                lblprg.BeginInvoke(New Action(Sub()
                                                  clsPublic.Actualizar_Progreso(lblprg, mensaje)
                                              End Sub))
            Else
                clsPublic.Actualizar_Progreso(lblprg, mensaje)
            End If
        Catch
        End Try
    End Sub

    Private Shared Function NormalizarMensajeError(ex As Exception) As String
        If ex Is Nothing Then Return ""
        Return ex.Message.Replace(vbCr, " ").Replace(vbLf, " ").Replace("|", "/").Trim()
    End Function

    Private Shared Function ClasificarFalloTransacWms(ex As Exception, etapa As String) As String
        Dim mensaje As String = NormalizarMensajeError(ex).ToUpperInvariant()

        If mensaje.Contains("LOGIN") OrElse mensaje.Contains("SESION") OrElse mensaje.Contains("SESIÓN") Then Return "SAP_LOGIN"
        If mensaje.Contains("TRANSAC_WMS") AndAlso mensaje.Contains("OBTENER") Then Return "SAP_GET_TRANSAC_WMS"
        If mensaje.Contains("JSON") OrElse mensaje.Contains("DESERIAL") OrElse mensaje.Contains("MAPEAR") Then Return "MAPEO_JSON"
        If mensaje.Contains("BODEGA") Then Return "BODEGA_NO_EXISTE"
        If mensaje.Contains("PROPIETARIO") Then Return "PROPIETARIO_NO_EXISTE"
        If mensaje.Contains("CLIENTE") Then Return "CLIENTE_NO_EXISTE"
        If mensaje.Contains("PROVEEDOR") Then Return "PROVEEDOR_NO_EXISTE"
        If mensaje.Contains("PRODUCTO") OrElse mensaje.Contains("ITEM") Then Return "PRODUCTO_NO_EXISTE"
        If mensaje.Contains("UNIDAD") OrElse mensaje.Contains("UM") Then Return "UM_NO_EXISTE"
        If mensaje.Contains("TALLA") OrElse mensaje.Contains("COLOR") Then Return "TALLA_COLOR_NO_EXISTE"
        If mensaje.Contains("RESERV") OrElse mensaje.Contains("STOCK") Then Return "SIN_STOCK_RESERVADO"
        If mensaje.Contains("PICKING") Then Return "PICKING_FALLO"
        If mensaje.Contains("DESPACH") Then Return "DESPACHO_FALLO"
        If mensaje.Contains("ACTUALIZAR TRANSAC_WMS") OrElse mensaje.Contains("MARCAR") Then Return "SAP_MARK_PROCESADO_FALLO"
        If etapa.ToUpperInvariant().Contains("AJUSTE_INTERMEDIA") Then Return "AJUSTE_INTERMEDIA_FALLO"
        If etapa.ToUpperInvariant().Contains("AJUSTE_APLICACION") Then Return "AJUSTE_APLICACION_WMS_FALLO"

        Return "EXCEPCION_NO_CLASIFICADA"
    End Function

    Private Shared Function CrearResultadoTransacWms(ctx As TransacWmsTraceContext,
                                                     etapa As String,
                                                     resultado As String,
                                                     causa As String,
                                                     mensaje As String) As String

        Dim texto As String = $"ORIGEN={TRANSAC_WMS_ORIGEN};SENTIDO={TRANSAC_WMS_SENTIDO};PROCESO={ctx.Proceso};TIPO_DOC={ctx.TipoDocumento};NOENC={ctx.Documento};REF={ctx.Referencia};DOCENTRY={FormatearDocEntries(ctx.DocEntries)};ETAPA={etapa};RESULTADO={resultado};CAUSA={causa};MSG={mensaje}"
        Return LimitarTexto(texto, TRANSAC_WMS_PROCESS_RESULT_MAX)
    End Function

    Private Shared Function ObtenerIdEjecucionEncTransacWms() As Integer

        Try

            If mTransacWmsIdEjecucionEnc > 0 Then Return mTransacWmsIdEjecucionEnc

            Dim idConfigEnc As Integer = BD.Instancia.IdConfiguracionInterface

            If BeConfigEnc IsNot Nothing AndAlso BeConfigEnc.Idnavconfigenc > 0 Then
                idConfigEnc = BeConfigEnc.Idnavconfigenc
            End If

            Dim beEjecucion As New clsBeI_nav_ejecucion_enc With {
                .IdNavConfigEnc = idConfigEnc,
                .Fecha = Now,
                .Exitosa = False
            }

            mTransacWmsIdEjecucionEnc = clsLnI_nav_ejecucion_enc.Insertar_From_Interface(beEjecucion)

            Return mTransacWmsIdEjecucionEnc

        Catch
            Return TRANSAC_WMS_ID_EJECUCION_ENC_FALLBACK
        End Try

    End Function

    Private Shared Function ObtenerIdNavConfigDetTransacWms() As Integer

        If mTransacWmsIdNavConfigDet > 0 Then Return mTransacWmsIdNavConfigDet

        Try

            Dim idConfigEnc As Integer = BD.Instancia.IdConfiguracionInterface

            If BeConfigEnc IsNot Nothing AndAlso BeConfigEnc.Idnavconfigenc > 0 Then
                idConfigEnc = BeConfigEnc.Idnavconfigenc
            End If

            Const sql As String = "SELECT TOP 1 det.idnavconfigdet " &
                                  "FROM i_nav_config_det det " &
                                  "INNER JOIN i_nav_ent ent ON det.idnavent = ent.idnavent " &
                                  "WHERE det.idnavconfigenc = @idnavconfigenc " &
                                  "AND (ent.nombre = @nombre OR ent.nombre LIKE @nombreLike) " &
                                  "ORDER BY det.activo DESC, det.idnavconfigdet"

            Using connection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
                Using command As New SqlCommand(sql, connection) With {.CommandType = CommandType.Text}
                    command.Parameters.Add("@idnavconfigenc", SqlDbType.Int).Value = idConfigEnc
                    command.Parameters.Add("@nombre", SqlDbType.NVarChar, 100).Value = TRANSAC_WMS_ORIGEN
                    command.Parameters.Add("@nombreLike", SqlDbType.NVarChar, 110).Value = "%TRANSAC%"

                    connection.Open()
                    Dim value As Object = command.ExecuteScalar()

                    If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                        mTransacWmsIdNavConfigDet = CInt(value)
                    End If
                End Using
            End Using

        Catch
            mTransacWmsIdNavConfigDet = 0
        End Try

        If mTransacWmsIdNavConfigDet <= 0 Then mTransacWmsIdNavConfigDet = TRANSAC_WMS_ID_NAV_CONFIG_DET_FALLBACK
        Return mTransacWmsIdNavConfigDet

    End Function

    Private Shared Sub ReiniciarContextoLogTransacWms()
        mTransacWmsIdEjecucionEnc = 0
        mTransacWmsIdNavConfigDet = 0
    End Sub

    Private Shared Sub TrazaDebugTransacWms(mensaje As String)
        Try
            Debug.WriteLine($"[TRANSAC_WMS][DEBUG] {DateTime.Now:O} {mensaje}")
        Catch
        End Try
    End Sub

    Private Shared Sub RegistrarTrazaTransacWms(ctx As TransacWmsTraceContext,
                                                etapa As String,
                                                resultado As String,
                                                causa As String,
                                                mensaje As String)

        Dim detalle As String = CrearResultadoTransacWms(ctx, etapa, resultado, causa, mensaje)
        Dim referencia As String = If(String.IsNullOrWhiteSpace(ctx.Referencia), ctx.Documento, ctx.Referencia)

        TrazaDebugTransacWms($"TRAZA|ETAPA={etapa}|RESULTADO={resultado}|CAUSA={causa}|REF={referencia}|DOCENTRY={FormatearDocEntries(ctx.DocEntries)}|MSG={mensaje}")

        If String.Equals(resultado, "OK", StringComparison.OrdinalIgnoreCase) Then Return

        Try
            clsLnI_nav_ejecucion_det_error.Inserta_Log(detalle,
                                                       referencia,
                                                       ObtenerIdEjecucionEncTransacWms(),
                                                       ObtenerIdNavConfigDetTransacWms())
        Catch logEx As Exception
            Try
                clsLnLog_error_wms.Agregar_Error($"TRANSAC_WMS_LOG_FALLO|{detalle}|LOG_ERROR={NormalizarMensajeError(logEx)}")
            Catch
            End Try
        End Try

        Try
            clsLnLog_error_wms.Agregar_Error($"TRANSAC_WMS|{detalle}")
        Catch
        End Try
    End Sub

    Private Shared Async Function RegistrarFalloTransacWmsAsync(ctx As TransacWmsTraceContext,
                                                                etapa As String,
                                                                ex As Exception,
                                                                sessionCookie As String,
                                                                baseUrl As String,
                                                                lblprg As RichTextBox) As Task

        Dim causa As String = ClasificarFalloTransacWms(ex, etapa)
        Dim mensaje As String = NormalizarMensajeError(ex)
        Dim resultadoSap As String = CrearResultadoTransacWms(ctx, etapa, "ERROR", causa, mensaje)

        RegistrarTrazaTransacWms(ctx, etapa, "ERROR", causa, mensaje)
        clsPublic.Actualizar_Progreso(lblprg, resultadoSap)

        Try
            Await Marcar_Transac_Wms_Por_DocEntries_SLAsync(ctx.DocEntries,
                                                            sessionCookie,
                                                            baseUrl,
                                                            TRANSAC_WMS_ERROR,
                                                            resultadoSap).ConfigureAwait(False)
        Catch sapEx As Exception
            RegistrarTrazaTransacWms(ctx,
                                     "SAP_MARK_ERROR",
                                     "ERROR",
                                     "SAP_MARK_PROCESADO_FALLO",
                                     NormalizarMensajeError(sapEx))
        End Try
    End Function

#End Region

#Region "Ventas"

    Private Shared Function Get_Ventas_Tiendas(pCodigoBodegaInterface As String,
                                               lblprg As RichTextBox) As List(Of clsBeI_nav_ped_traslado_enc)

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePropietario As clsBePropietarios = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario)

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

            clsPublic.Actualizar_Progreso(lblprg, "Obteniendo venta(s).")

            Dim filtroEnviado As String = "(U_Procesado_WMS eq null or U_Procesado_WMS eq 2)"
            Dim filtroVentas As String = "(U_Document_Type eq '2')"
            Dim filtroFinal As String = $"{filtroEnviado} and {filtroVentas}"

            Dim allRows As JArray = SapServiceBase.ObtenerTransacWmsPaginado(filtroFinal,
                                                                             vHanaService.SessionCookie,
                                                                             BD.Instancia.HANA_SL,
                                                                             lblprg)

            If allRows.Count = 0 Then
                Return lPedidosCliente
            End If

            Dim jsonResponse As String = SapServiceBase.CrearJsonResponseDesdeRows(allRows)
            lPedidosCliente = ProcesarTransaccionesWMSCompleto(jsonResponse, pCodigoBodegaInterface, BePropietario, clsDataContractDI.tTipoDocumentoSalida.Pedido_Consolidado)

            Return lPedidosCliente

        Catch ex As Exception
            Throw New Exception("(SL) Get_Ventas_Tiendas: " & ex.Message, ex)
        End Try
    End Function

    Public Shared Function ProcesarTransaccionesWMSCompleto(jsonResponse As String,
                                                            pCodigoBodegaInterface As String,
                                                            BePropietario As clsBePropietarios,
                                                            pIdTipoDocumento As clsDataContractDI.tTipoDocumentoSalida) As List(Of clsBeI_nav_ped_traslado_enc)
        Try
            ' 1. Deserializar JSON
            Dim response As TRANSAC_WMS_Response = JsonConvert.DeserializeObject(Of TRANSAC_WMS_Response)(jsonResponse)

            ' 2. Agrupar por número de encabezado
            Dim transaccionesAgrupadas As New List(Of PedidoTrasladoEncabezado)()
            Dim agrupamiento = response.Value.GroupBy(Function(x) x.U_NoEnc)

            For Each grupo In agrupamiento
                Dim primerRegistro = grupo.First()
                Dim encabezado As New PedidoTrasladoEncabezado With {
                .NoEnc = primerRegistro.U_NoEnc,
                .ExternalDocumentNo = primerRegistro.U_External_Document_No,
                .Serie = primerRegistro.U_Serie,
                .CompanyCode = primerRegistro.U_Company_Code,
                .PostingDate = primerRegistro.U_Posting_Date,
                .CreateDate = primerRegistro.CreateDate,
                .TransferFromCode = primerRegistro.U_Transfer_from_Code,
                .TransferFromContact = primerRegistro.U_Transfer_from_Contact,
                .TransferToCode = primerRegistro.U_Transfer_to_Code,
                .TransferToName = primerRegistro.U_Transfer_to_Name,
                .ReceipDocumentReference = primerRegistro.U_Receip_Document_Reference,
                .DocumentType = pIdTipoDocumento,
                .LineasDetalle = New List(Of PedidoTrasladoDetalle)()
            }

                ' Agregar líneas
                For Each transaccion In grupo
                    encabezado.LineasDetalle.Add(New PedidoTrasladoDetalle With {
                .LineNo = transaccion.U_Line_No,
                .ItemNo = transaccion.U_Item_No,
                .Descripcion = transaccion.U_Descripcion,
                .UnitOfMeasureCode = transaccion.U_Unit_of_Mesasure_Code,
                .QtyToShip = transaccion.U_Qty_to_Ship,
                .QtyWMS = transaccion.U_Qty_WMS,
                .Color = transaccion.U_Color,
                .Size = transaccion.U_Size,
                .ProcesadoWMS = transaccion.U_Procesado_WMS,
                .ProcessResult = transaccion.U_Process_Result,
                .DocEntry = transaccion.DocEntry
            })
                Next

                ' Ordenar líneas
                encabezado.LineasDetalle = encabezado.LineasDetalle.OrderBy(Function(x) x.LineNo).ToList()
                transaccionesAgrupadas.Add(encabezado)
            Next

            ' 3. Mapear a clases de negocio
            Return MapearAClasesNegocio(transaccionesAgrupadas, pCodigoBodegaInterface, BePropietario)

        Catch ex As Exception
            Throw New Exception("Error completo en procesamiento de transacciones WMS: " & ex.Message, ex)
        End Try
    End Function

    Public Shared Function MapearAClasesNegocio(transaccionesAgrupadas As List(Of PedidoTrasladoEncabezado),
                                                pCodigoBodegaInterface As String,
                                                BePropietario As clsBePropietarios) As List(Of clsBeI_nav_ped_traslado_enc)

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)()

        For Each pedido In transaccionesAgrupadas
            ' Parsear fechas de forma segura
            Dim postingDate As Date
            If Not Date.TryParse(pedido.CreateDate, postingDate) Then
                postingDate = Date.Now
            End If

            ' Mapeo del encabezado según tu referencia
            Dim beFacturaDeudor As New clsBeI_nav_ped_traslado_enc With {
            .No = pedido.NoEnc,
            .Posting_Date = postingDate,
            .Receipt_Date = postingDate,
            .Shipment_Date = postingDate,
            .Status = 1,
            .Transfer_from_Code = pedido.TransferFromCode,
            .Transfer_from_Contact = pedido.TransferFromContact,
            .Transfer_to_Contact = pedido.TransferToName,
            .Transfer_to_CodeField = pedido.TransferToCode,
            .Transfer_to_Code = pedido.TransferToCode,
            .Product_Owner_Code = BePropietario.Codigo,
            .Receipt_Document_Reference = pedido.ExternalDocumentNo,
            .Company_Code = pedido.CompanyCode,
            .Comments = $"Serie: {pedido.Serie} - Documento: {pedido.ExternalDocumentNo}",
            .Document_Type = pedido.DocumentType,
            .Transportation_Guide = pedido.ReceipDocumentReference,
            .External_Document_No = pedido.ExternalDocumentNo,
            .Transfer_to_Name = pedido.TransferToName,
            .Lineas_Detalle = New List(Of clsBeI_nav_ped_traslado_det)()
        }

            ' Mapeo de las líneas de detalle según tu referencia
            For Each detalle In pedido.LineasDetalle
                ' Verificar filtro por bodega si es necesario
                ' If detalle.AlgunCampoBodega <> pCodigoBodegaInterface Then Continue For

                Dim beDet As New clsBeI_nav_ped_traslado_det With {
                .NoEnc = beFacturaDeudor.No,
                .No = (clsLnI_nav_ped_traslado_det.MaxID() + 1).ToString(), ' Asegurar que sea string según la propiedad
                .Item_No = detalle.ItemNo,
                .Line_No = detalle.LineNo,
                .Shipment_Date = Date.Now,
                .Quantity = CDec(detalle.QtyToShip),
                .Qty_to_Ship = CDec(detalle.QtyToShip),
                .Description = detalle.Descripcion,
                .Unit_of_Measure_Code = detalle.UnitOfMeasureCode,
                .Status = 1,
                .Transfer_to_CodeField = beFacturaDeudor.Transfer_to_Code,
                .Price = 0.0, ' No disponible en el JSON original, establecer valor por defecto
                .Color = detalle.Color,
                .Size = detalle.Size,
                .Variant_Code = Nothing,
                .Process_Result = detalle.ProcessResult,
                .DocEntry = detalle.DocEntry
            }

                beFacturaDeudor.Lineas_Detalle.Add(beDet)
            Next

            ' Solo agregar si tiene líneas de detalle
            If beFacturaDeudor.Lineas_Detalle.Any() Then
                lPedidosCliente.Add(beFacturaDeudor)
            End If
        Next

        Return lPedidosCliente
    End Function

    ' Clases para el agrupamiento
    Public Class PedidoTrasladoEncabezado
        Public Property NoEnc As String
        Public Property ExternalDocumentNo As String
        Public Property Serie As String
        Public Property CompanyCode As String
        Public Property PostingDate As String
        Public Property CreateDate As String
        Public Property TransferFromCode As String
        Public Property TransferFromContact As String
        Public Property TransferToCode As String
        Public Property TransferToName As String
        Public Property ReceipDocumentReference As String
        Public Property DocumentType As String
        Public Property LineasDetalle As List(Of PedidoTrasladoDetalle)
        Public Sub New()
            LineasDetalle = New List(Of PedidoTrasladoDetalle)()
        End Sub
    End Class

    Public Class PedidoTrasladoDetalle
        Public Property LineNo As Integer
        Public Property ItemNo As String
        Public Property Descripcion As String
        Public Property UnitOfMeasureCode As String
        Public Property QtyToShip As Double
        Public Property QtyWMS As Double?
        Public Property Color As String
        Public Property Size As String
        Public Property ProcesadoWMS As String
        Public Property ProcessResult As String

        Public DocEntry As Integer = 0
    End Class

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
                                                                ByVal prg As System.Windows.Forms.ProgressBar,
                                                                Optional ByVal pNoDocumento As String = "") As Task(Of Boolean)
        Dim sw As New Stopwatch()

        Try
            ReiniciarContextoLogTransacWms()

            ' Inicia cronómetro y anuncia inicio
            sw.Start()
            clsPublic.Actualizar_Progreso(lblprg, "Iniciando proceso de sincronización de pedidos de cliente desde SAP.")


            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

            Dim sessionCookie As String = ""
            Dim baseUrl As String = BD.Instancia.HANA_SL
            Dim BeBodega As clsBeBodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega)

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
            Dim ctx As TransacWmsTraceContext = CrearContextoTransacWms("PULL_TRANSAC_WMS",
                                                                        "",
                                                                        pNoDocumento,
                                                                        pNoDocumento,
                                                                        "",
                                                                        "",
                                                                        New List(Of Integer)())
            RegistrarTrazaTransacWms(ctx, "PULL_TRANSAC_WMS", "ERROR", "EXCEPCION_GENERAL", NormalizarMensajeError(ex))
            clsPublic.Actualizar_Progreso(lblprg, $"Error en el proceso: {ex.Message}. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.")
            Throw
        Finally
            If sw.IsRunning Then sw.Stop()
            clsPublic.Actualizar_Progreso(lblprg, $"Fin del proceso de sincronización de los pedidos de cliente desde SAP. Tiempo total: {sw.Elapsed.TotalSeconds:F2} segundos.")
        End Try

    End Function

    Private Shared Async Function Procesar_Documentos(ByVal codigoBodega As String,
                                                      ByVal pNoDocumento As String,
                                                      ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                      ByVal lblprg As RichTextBox) As Task(Of Boolean)

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Conectando a SAP.")

            Dim facturas As List(Of clsBeI_nav_ped_traslado_enc) = Get_Ventas_Tiendas(codigoBodega, lblprg)
            Dim pBePedidoEnc As New clsBeTrans_pe_enc
            Dim PedidoClienteExistenteByCompany As New clsBeTrans_pe_enc
            Dim PedidoClienteExistente As New clsBeTrans_pe_enc

            If facturas.Count = 0 Then
                clsPublic.Actualizar_Progreso(lblprg, "No hay pedidos de cliente para importar.")
                Return False
            End If

            For Each factura In facturas

                If factura.Transfer_from_Code <> "01" Then
                    Debug.Print(factura.Transfer_from_Code)
                End If
                clsPublic.Actualizar_Progreso(lblprg, $"Procesando pedido de cliente de SAP (@Transac_WMS): {factura.Receipt_Document_Reference}/{factura.No}{vbNewLine}")

                Dim listaDocEntryDistintos As List(Of Integer) = factura.Lineas_Detalle.Select(Function(x) x.DocEntry) _
                                                                                                     .Distinct() _
                                                                                                     .ToList()
                Dim ctx As TransacWmsTraceContext = CrearContextoTransacWms("VENTA",
                                                                            "2",
                                                                            factura.No,
                                                                            factura.Receipt_Document_Reference,
                                                                            factura.Transfer_from_Code,
                                                                            factura.Transfer_to_Code,
                                                                            listaDocEntryDistintos)
                Dim clsTrans As New clsTransaccion
                clsTrans.Begin_Transaction()

                Try


                    '#MECR 202508080524: Verifica si el proveedor ya existe como cliente en WMS.
                    If Await clsSyncSapTrasladosEnvio.Validar_Cliente_WMS(factura.Transfer_to_Code, "C", lblprg, clsTrans, vHanaService.SessionCookie, BD.Instancia.HANA_SL) Then

                        Dim pedidoEnc As clsBeTrans_pe_enc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia_Transac_WMS(factura, lblprg, clsTrans.lConnection, clsTrans.lTransaction)

                        If pedidoEnc IsNot Nothing Then

                            Dim trasladoSincronizado As Boolean = Await Marcar_Transac_Wms_Por_DocEntries_SLAsync(listaDocEntryDistintos,
                                                                                                                  vHanaService.SessionCookie,
                                                                                                                  BD.Instancia.HANA_SL)

                            If pedidoEnc IsNot Nothing AndAlso trasladoSincronizado Then
                                RegistrarTrazaTransacWms(ctx, "APLICAR_WMS", "OK", "OK", "Documento procesado correctamente.")
                                clsPublic.Actualizar_Progreso(lblprg, "Documento procesado correctamente :) !")
                            End If

                        Else
                            Throw New Exception("No se generó pedido WMS para la transacción TRANSAC_WMS. Posible reserva o validación incompleta.")
                        End If

                    Else

                        Throw New Exception("No se pudo procesar en su totalidad la venta")

                    End If

                    clsTrans.Commit_Transaction()

                Catch ex As Exception
                    clsTrans.RollBack_Transaction()
                    RegistrarFalloTransacWmsAsync(ctx,
                                                  "APLICAR_WMS",
                                                  ex,
                                                  vHanaService.SessionCookie,
                                                  BD.Instancia.HANA_SL,
                                                  lblprg).GetAwaiter().GetResult()
                Finally
                    clsTrans.Close_Conection()
                End Try

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

    Private Shared Async Function Get_Devoluciones_Tiendas(pCodigoBodegaInterface As String,
                                                       lblprg As RichTextBox) As Task(Of List(Of clsBeI_nav_ped_compra_enc))

        Dim lDevolucionesCliente As New List(Of clsBeI_nav_ped_compra_enc)
        Dim BePropietario As clsBePropietarios = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario)

        If BePropietario Is Nothing Then
            Throw New Exception($"#ERROR: No se encontró el propietario con ID {BeConfigEnc.IdPropietario}")
        End If

        Try

            vHanaService = New SapServiceLayerClient()

            Dim loginResponse As LoginResponseDto = vHanaService.LoginAsync().GetAwaiter().GetResult()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener sesión.")
                Return lDevolucionesCliente
            Else
                clsPublic.Actualizar_Progreso(lblprg, "Conexión correcta.")
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Obteniendo devolucion(es) de tienda.")

            Dim filtroEnviado As String = "(U_Procesado_WMS eq null or U_Procesado_WMS eq 2)"
            Dim filtroVentas As String = "(U_Document_Type eq '17')"
            Dim filtroFinal As String = $"{filtroEnviado} and {filtroVentas}"

            Dim allRows As JArray = SapServiceBase.ObtenerTransacWmsPaginado(
            filtroFinal,
            vHanaService.SessionCookie,
            BD.Instancia.HANA_SL,
            lblprg)

            If allRows.Count = 0 Then
                Return lDevolucionesCliente
            End If

            Dim grupos = allRows _
            .OfType(Of JObject)() _
            .GroupBy(Function(r) New With {
                Key .DocEntry = r.Value(Of Integer?)("U_NoEnc").GetValueOrDefault(-1),
                Key .CodBodega = r.Value(Of String)("U_Transfer_From_Code")
            })

            For Each grupo In grupos

                Try
                    Dim docEntry As Integer = grupo.Key.DocEntry
                    Dim rowEnc As JObject = grupo.First()
                    Dim documentLines As New JArray()

                    For Each row As JObject In grupo
                        Dim linea As New JObject()
                        linea("ItemCode") = row("U_Item_No")
                        linea("LineNum") = row("U_Line_No")
                        linea("Planed_Receipt_Date") = row("U_Posting_Date")
                        linea("Quantity") = row("U_Qty_to_Ship")
                        linea("ItemDescription") = row("U_Descripcion")
                        linea("U_Color") = row("U_Color")
                        linea("U_Talla") = row("U_Size")
                        linea("WarehouseCode") = row("U_Transfer_From_Code")
                        linea("BaseEntry") = row("BaseEntry")
                        linea("MeasureUnit") = row("U_Unit_of_Mesasure_Code")
                        documentLines.Add(linea)
                    Next

                    Dim dtDetTallaColor As DataTable =
                    DevolucionTransacWMS_Mapper.ConstruirTablaDesdeJsonTallasColores_Devolucion(documentLines, docEntry, 0)

                    Dim encabezado As clsBeI_nav_ped_compra_enc =
                    DevolucionTransacWMS_Mapper.MapearEncabezado_Devolucion(rowEnc)

                    encabezado.Lineas_Detalle =
                    DevolucionTransacWMS_Mapper.MapearDetalle_Devolucion(documentLines)

                    encabezado.DocEntriesTransacWms = grupo.
                    Select(Function(r) r.Value(Of Integer)("DocEntry")).
                    Distinct().
                    ToList()

                    If dtDetTallaColor IsNot Nothing AndAlso dtDetTallaColor.Rows.Count > 0 Then
                        encabezado.Lineas_Detalle_Talla_Color =
                        Await DevolucionTransacWMS_Mapper.MapearDetalleTallaColor_Devolucion(dtDetTallaColor,
                                                                                            IdUsuario,
                                                                                            vHanaService.SessionCookie,
                                                                                            BD.Instancia.HANA_SL).ConfigureAwait(False)
                    Else
                        encabezado.Lineas_Detalle_Talla_Color = New List(Of clsBeProducto_talla_color)()
                    End If

                    Dim BeCampaña As clsBeCampaña = clsLnCampaña.Get_Single_By_IdCampaña(0)
                    encabezado.Campaña = BeCampaña

                    lDevolucionesCliente.Add(encabezado)

                Catch ex As Exception
                    clsPublic.Actualizar_Progreso(lblprg, "La Nota de Crédito " & grupo.Key.DocEntry & " tiene datos inconsistentes.")
                End Try

            Next

            Return lDevolucionesCliente

        Catch ex As Exception
            Throw New Exception("(SL) Get_Devoluciones_Tiendas: " & ex.Message, ex)
        End Try
    End Function

    Public Shared Async Function Procesar_Documentos_Devolucion(ByVal lblprg As RichTextBox,
                                                                prg As System.Windows.Forms.ProgressBar) As Task(Of Boolean)

        Dim vResult As String = ""
        Dim vContador As Integer = 0
        Dim BeBodega As New clsBeBodega
        Dim BePedidoCompraEnc As New clsBeTrans_oc_enc

        Try

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega)

            clsPublic.Actualizar_Progreso(lblprg, "Obteniendo Notas de crédito(Devoluciones de cliente).")

            Dim lDevolucionesCliente As New List(Of clsBeI_nav_ped_compra_enc)
            lDevolucionesCliente = Await Get_Devoluciones_Tiendas(BeBodega.Codigo,
                                                                  lblprg)

            If lDevolucionesCliente Is Nothing Then
                clsPublic.Actualizar_Progreso(lblprg, "No se obtuvieron Notas de crédito(Devoluciones de cliente).")
                Return False
            End If

            clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format("Notas de crédito(Devoluciones de cliente) (TRANSAC_WMS): {0} ", lDevolucionesCliente.Count))

            prg.Maximum = lDevolucionesCliente.Count

            If clsLnI_nav_ped_compra_det.EliminarTodos() _
                AndAlso clsLnI_nav_ped_compra_enc.EliminarTodos() Then

                Dim BeProveedorBodega As New clsBeProveedor_bodega

                For Each BeINavPedCompra In lDevolucionesCliente

                    Dim ctx As TransacWmsTraceContext = CrearContextoTransacWms("DEVOLUCION_CLIENTE",
                                                                                "17",
                                                                                BeINavPedCompra.No,
                                                                                BeINavPedCompra.Vendor_Invoice_No,
                                                                                BeINavPedCompra.Location_Code,
                                                                                BeINavPedCompra.Buy_From_Vendor_No,
                                                                                BeINavPedCompra.DocEntriesTransacWms)
                    Dim clsTrans As New clsTransaccion
                    clsTrans.Begin_Transaction()

                    Try

                        If Not clsLnProveedor.Existe_Proveedor(BeINavPedCompra.Buy_From_Vendor_No, clsTrans.lConnection, clsTrans.lTransaction) Then

                            BeConfigEnc = BeConfigEnc

                            If Await Inserta_Proveedor_Desde_SAP(BeINavPedCompra.Buy_From_Vendor_No, vHanaService.SessionCookie, BD.Instancia.HANA_SL) Then
                                clsPublic.Actualizar_Progreso(lblprg, vbTab & "El proveedor: " & BeINavPedCompra.Buy_From_Vendor_No & " No existía en WMS y fue insertado.")
                            End If

                        End If

                        clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format("Procesando Nota de crédito(Devolución de cliente): {0} ", BeINavPedCompra.No & " - " & BeINavPedCompra.Vendor_Invoice_No, vbNewLine))

                        Dim procesoOk As Boolean = clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(BeINavPedCompra,
                                                                                                        BePedidoCompraEnc,
                                                                                                        vResult,
                                                                                                        Nothing,
                                                                                                        clsTrans.lConnection,
                                                                                                        clsTrans.lTransaction)

                        If procesoOk Then

                            'Await Marcar_Devolucion_Sincronizada_SLAsync(BeINavPedCompra.No, vHanaService.SessionCookie, BD.Instancia.HANA_SL)
                            Await Marcar_Transac_Wms_Por_DocEntries_SLAsync(BeINavPedCompra.DocEntriesTransacWms,
                                                                            vHanaService.SessionCookie,
                                                                            BD.Instancia.HANA_SL)
                            RegistrarTrazaTransacWms(ctx, "APLICAR_WMS", "OK", "OK", "Documento procesado correctamente.")
                        Else
                            Throw New Exception(If(String.IsNullOrWhiteSpace(vResult),
                                                   "No se pudo procesar la devolución de cliente en WMS.",
                                                   vResult))
                        End If

                        clsPublic.Actualizar_Progreso(lblprg, vResult)

                        clsTrans.Commit_Transaction()

                    Catch ex As Exception
                        clsTrans.RollBack_Transaction()
                        RegistrarFalloTransacWmsAsync(ctx,
                                                      "APLICAR_WMS",
                                                      ex,
                                                      vHanaService.SessionCookie,
                                                      BD.Instancia.HANA_SL,
                                                      lblprg).GetAwaiter().GetResult()
                    Finally
                        clsTrans.Close_Conection()
                    End Try
                Next

            End If

            Return True

        Catch ex As Exception
            clsLnLog_error_wms.Agregar_Error("Error_20250422_Fact_Res:" & ex.Message)
            Throw ex
        Finally
            prg.Value = 0
        End Try

    End Function

    Public Shared Async Function Inserta_Proveedor_Desde_SAP(ByVal pCodigo As String,
                                                             SessionCookie As String,
                                                             BaseUrl As String) As Task(Of Boolean)


        Dim BeProveedor As New clsBeProveedor
        Dim BeProveedorBodega As New clsBeProveedor_bodega
        Dim BeSAPProveedor As New clsBeI_nav_proveedor
        Dim clstrans As New clsTransaccion
        Dim vResult As Boolean = False

        Try

            clstrans.Begin_Transaction()

            BeSAPProveedor = Await clsSyncSAPProveedor.Get_Proveedor_SAP_SLAsync(pCodigo,
                                                                                 SessionCookie,
                                                                                 BaseUrl)

            If Not BeSAPProveedor Is Nothing Then

                BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
                BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
                BeProveedor.IdProveedor = clsLnProveedor.MaxID(clstrans.lConnection, clstrans.lTransaction) + 1
                BeProveedor.Codigo = BeSAPProveedor.No
                BeProveedor.Nombre = BeSAPProveedor.Name
                BeProveedor.Telefono = IIf(BeSAPProveedor.Phone_No = Nothing, "", BeSAPProveedor.Phone_No)
                BeProveedor.Nit = BeSAPProveedor.VAT_Registratrion_No
                BeProveedor.Direccion = BeSAPProveedor.Adress
                BeProveedor.Contacto = BeSAPProveedor.Contact
                BeProveedor.Activo = True
                BeProveedor.User_agr = BeConfigEnc.IdUsuario
                BeProveedor.Fec_agr = Date.UtcNow
                BeProveedor.User_mod = BeConfigEnc.IdUsuario
                BeProveedor.Fec_mod = Date.UtcNow

                Try

                    clsLnProveedor.Insertar(BeProveedor, clstrans.lConnection, clstrans.lTransaction)

                    BeProveedorBodega = New clsBeProveedor_bodega
                    BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(clstrans.lConnection, clstrans.lTransaction) + 1
                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                    BeProveedorBodega.IdBodega = BeConfigEnc.Idbodega
                    BeProveedorBodega.Activo = True
                    BeProveedorBodega.User_agr = BeConfigEnc.IdUsuario
                    BeProveedorBodega.User_mod = BeConfigEnc.IdUsuario
                    BeProveedorBodega.Fec_agr = Now
                    BeProveedorBodega.Fec_mod = Now

                    clsLnProveedor_bodega.Insertar(BeProveedorBodega, clstrans.lConnection, clstrans.lTransaction)

                    Await clsSyncSAPProveedor.Marcar_Proveedor_Sincronizado_SLAsync(BeProveedor.Codigo, SessionCookie, BaseUrl)

                    clstrans.lTransaction.Commit()

                    vResult = True

                Catch ex As Exception

                    clstrans.RollBack_Transaction()

                    clsLnLog_error_wms.Agregar_Error("Error_20250422_Inteface_Proveedor: " & ex.Message & " " & BeProveedor.Codigo)

                    Throw ex

                End Try

            End If

            Return vResult

        Catch ex As Exception
            Throw ex
        Finally
            clstrans.Close_Conection()
        End Try

    End Function

    Public Shared Async Function Procesar_Devoluciones_de_Cliente_SAP(lblprg As RichTextBox,
                                                                      prg As System.Windows.Forms.ProgressBar,
                                                                      Optional ByVal ForzarEjecucion As Boolean = False) As Task(Of Boolean)

        Dim inicio As Date = Now
        Dim ok As Boolean = False

        Try
            ReiniciarContextoLogTransacWms()

            Dim ejecutarImportacion As Boolean = True

            If ejecutarImportacion Then
                Dim importo As Boolean = Await Procesar_Documentos_Devolucion(lblprg,
                                                                              prg).ConfigureAwait(False)

                If Not importo Then
                    prg.Value = 0
                    clsPublic.Actualizar_Progreso(lblprg, "No se importaron notas de crédito (devoluciones de cliente).")
                    Return False
                End If
            End If

            ok = True

            ' Log del tiempo transcurrido (fuera de la transacción)
            Dim difSegundos As Double = DateDiff(DateInterval.Second, inicio, Now)
            clsPublic.Actualizar_Progreso(lblprg, vbTab & $" -> Fin de proceso, tiempo transcurrido: {difSegundos} segundo(s)")

            Return ok

        Catch ex As Exception
            prg.Value = 0
            clsLnLog_error_wms.Agregar_Error("Error_20250422_Insert_Fact_Res: " & ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, $"Error al insertar pedido de compra a tabla de TOMWMS: {ex.Message}{vbNewLine}")
            Return False
        End Try
    End Function

    Private Shared Async Function Marcar_Transac_Wms_Por_DocEntries_SLAsync(docEntries As List(Of Integer),
                                                                             sessionCookie As String,
                                                                             baseUrl As String,
                                                                             Optional estadoProcesado As Integer = TRANSAC_WMS_OK,
                                                                             Optional processResult As String = "OK") As Task(Of Boolean)
        Try
            If docEntries Is Nothing OrElse docEntries.Count = 0 Then Return False

            If Not baseUrl.EndsWith("/") Then
                baseUrl &= "/"
            End If

            Dim httpPatch As New HttpMethod("PATCH")
            Dim payloadObj As New JObject()
            payloadObj("U_Procesado_WMS") = estadoProcesado
            payloadObj("U_Process_Result") = LimitarTexto(processResult, TRANSAC_WMS_PROCESS_RESULT_MAX)
            TrazaDebugTransacWms($"PATCH_PREP|DOCENTRIES={FormatearDocEntries(docEntries)}|BASEURL={baseUrl}|ESTADO={estadoProcesado}|RESULT_LEN={If(processResult, String.Empty).Length}|JSON_TYPE={payloadObj.GetType().FullName}|JSON_ASM={payloadObj.GetType().Assembly.FullName}")

            Dim payload As String = ""
            Try
                payload = payloadObj.ToString(Formatting.None)
                TrazaDebugTransacWms($"PATCH_PAYLOAD_OK|LEN={payload.Length}|PAYLOAD={payload}")
            Catch exPayload As Exception
                TrazaDebugTransacWms($"PATCH_PAYLOAD_FAIL|JSON_TYPE={payloadObj.GetType().FullName}|JSON_ASM={payloadObj.GetType().Assembly.FullName}|EX={exPayload.GetType().FullName}|MSG={NormalizarMensajeError(exPayload)}")
                Throw
            End Try

            Using handler As New HttpClientHandler()
                handler.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)
                    client.DefaultRequestHeaders.ConnectionClose = True

                    For Each docEntry In docEntries.Distinct()

                        Dim requestUrl As String = $"TRANSAC_WMS('{docEntry}')"
                        Dim fullUrl As String = baseUrl & requestUrl

                        TrazaDebugTransacWms($"PATCH_SEND|DOCENTRY={docEntry}|URL={fullUrl}")

                        Using request As New HttpRequestMessage(httpPatch, fullUrl)
                            request.Headers.ConnectionClose = True
                            request.Headers.Add("Cookie", sessionCookie)
                            request.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                            request.Content = New StringContent(payload, Encoding.UTF8, "application/json")

                            Dim response = Await client.SendAsync(request).ConfigureAwait(False)

                            If Not response.IsSuccessStatusCode Then
                                Dim errContent = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                                TrazaDebugTransacWms($"PATCH_FAIL|DOCENTRY={docEntry}|STATUS={response.StatusCode}|DETAIL={errContent}")
                                Throw New Exception($"Error al actualizar TRANSAC_WMS('{docEntry}'). Código: {response.StatusCode}, Detalle: {errContent}")
                            End If
                        End Using

                    Next

                    Return True

                End Using

            End Using

        Catch ex As Exception
            TrazaDebugTransacWms($"PATCH_EXCEPTION|EX={ex.GetType().FullName}|MSG={NormalizarMensajeError(ex)}")
            Throw New Exception($"(SL) {MethodBase.GetCurrentMethod().Name} {ex.Message}", ex)
        End Try
    End Function

#End Region

#Region "Ajustes de inventario positivos"

    Public Shared Async Function Procesar_Ajustes_SAP(lblprg As RichTextBox,
                                                      prg As System.Windows.Forms.ProgressBar,
                                                      Optional ByVal ForzarEjecucion As Boolean = False) As Task(Of Boolean)

        Dim inicio As Date = Now
        Dim ok As Boolean = False

        Try
            ReiniciarContextoLogTransacWms()

            Dim ejecutarImportacion As Boolean = True

            If ejecutarImportacion Then
                Dim BeBodega As clsBeBodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega)

                If BeBodega Is Nothing Then
                    Throw New Exception("ERROR_202311271751: Error no se pudo obtener el objeto de bodega asociado a la configuración de interface: " & BeConfigEnc.Idbodega)
                End If

                Dim importo As Boolean = Await Procesar_Documentos_Ajustes(BeBodega.Codigo, BeConfigEnc, lblprg).ConfigureAwait(False)

                If Not importo Then
                    prg.Value = 0
                    clsPublic.Actualizar_Progreso(lblprg, "No se importaron los ajustes.")
                    Return False
                End If

            End If

            ok = True

            ' Log del tiempo transcurrido (fuera de la transacción)
            Dim difSegundos As Double = DateDiff(DateInterval.Second, inicio, Now)
            clsPublic.Actualizar_Progreso(lblprg, vbTab & $" -> Fin de proceso, tiempo transcurrido: {difSegundos} segundo(s)")

            Return ok

        Catch ex As Exception
            prg.Value = 0
            clsLnLog_error_wms.Agregar_Error("Error_20250422_Insert_Fact_Res: " & ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, $"Error al insertar pedido de compra a tabla de TOMWMS: {ex.Message}{vbNewLine}")
            Return False
        End Try
    End Function

    Private Shared Async Function Get_Ajustes_Tiendas(pCodigoBodegaInterface As String,
                                                  lblprg As RichTextBox) As Task(Of List(Of clsBeTrans_ajuste_enc))

        Dim lAjustes As New List(Of clsBeTrans_ajuste_enc)
        Dim BePropietario As clsBePropietarios = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario)

        If BePropietario Is Nothing Then
            Throw New Exception($"#ERROR: No se encontró el propietario con ID {BeConfigEnc.IdPropietario}")
        End If

        Try

            vHanaService = New SapServiceLayerClient()

            Dim loginResponse As LoginResponseDto = Await vHanaService.LoginAsync()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener sesión.")
                Return lAjustes
            Else
                clsPublic.Actualizar_Progreso(lblprg, "Conexión correcta.")
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Obteniendo ajuste(s).")

            Dim filtroEnviado As String = "(U_Procesado_WMS eq null or U_Procesado_WMS eq 2)"
            Dim filtroVentas As String = "(U_Document_Type eq '100')"
            Dim filtroFinal As String = $"{filtroEnviado} and {filtroVentas}"

            Dim allRows As JArray = SapServiceBase.ObtenerTransacWmsPaginado(
            filtroFinal,
            vHanaService.SessionCookie,
            BD.Instancia.HANA_SL,
            lblprg)

            If allRows.Count = 0 Then
                Return lAjustes
            End If

            Dim jsonResponse As String = SapServiceBase.CrearJsonResponseDesdeRows(allRows)
            lAjustes = ProcesarTransaccionesWMS_Ajustes(jsonResponse, pCodigoBodegaInterface, BePropietario, lblprg)

            Return lAjustes

        Catch ex As Exception
            Throw New Exception("(SL) Get_Ajustes_Tiendas: " & ex.Message, ex)
        End Try
    End Function

    Public Shared Function ProcesarTransaccionesWMS_Ajustes(jsonResponse As String,
                                                            pCodigoBodegaInterface As String,
                                                            BePropietario As clsBePropietarios,
                                                            Optional lblprg As RichTextBox = Nothing) As List(Of clsBeTrans_ajuste_enc)
        Try
            ' 1. Deserializar JSON
            Dim response As TRANSAC_WMS_Response = JsonConvert.DeserializeObject(Of TRANSAC_WMS_Response)(jsonResponse)

            ' 2. Agrupar por número de encabezado
            Dim transaccionesAgrupadas As New List(Of PedidoTrasladoEncabezado)()
            Dim agrupamiento = response.Value.GroupBy(Function(x) x.U_NoEnc)

            For Each grupo In agrupamiento
                Dim primerRegistro = grupo.First()
                Dim encabezado As New PedidoTrasladoEncabezado With {
                .NoEnc = primerRegistro.U_NoEnc,
                .ExternalDocumentNo = primerRegistro.U_External_Document_No,
                .Serie = primerRegistro.U_Serie,
                .CompanyCode = primerRegistro.U_Company_Code,
                .PostingDate = primerRegistro.U_Posting_Date,
                .CreateDate = primerRegistro.CreateDate,
                .TransferFromCode = primerRegistro.U_Transfer_from_Code,
                .TransferFromContact = primerRegistro.U_Transfer_from_Contact,
                .TransferToCode = primerRegistro.U_Transfer_to_Code,
                .TransferToName = primerRegistro.U_Transfer_to_Name,
                .ReceipDocumentReference = primerRegistro.U_Receip_Document_Reference,
                .DocumentType = primerRegistro.U_Document_Type,
                .LineasDetalle = New List(Of PedidoTrasladoDetalle)()
            }

                ' Agregar líneas
                For Each transaccion In grupo
                    encabezado.LineasDetalle.Add(New PedidoTrasladoDetalle With {
                .LineNo = transaccion.U_Line_No,
                .ItemNo = transaccion.U_Item_No,
                .Descripcion = transaccion.U_Descripcion,
                .UnitOfMeasureCode = transaccion.U_Unit_of_Mesasure_Code,
                .QtyToShip = transaccion.U_Qty_to_Ship,
                .QtyWMS = transaccion.U_Qty_WMS,
                .Color = transaccion.U_Color,
                .Size = transaccion.U_Size,
                .ProcesadoWMS = transaccion.U_Procesado_WMS,
                .ProcessResult = transaccion.U_Process_Result,
                .DocEntry = transaccion.DocEntry
            })
                Next

                ' Ordenar líneas
                encabezado.LineasDetalle = encabezado.LineasDetalle.OrderBy(Function(x) x.LineNo).ToList()
                transaccionesAgrupadas.Add(encabezado)
            Next

            ' 3. Mapear a clases de negocio
            Return MapearAAjustes(transaccionesAgrupadas, pCodigoBodegaInterface, BePropietario, lblprg)

        Catch ex As Exception
            Throw New Exception("Error completo en procesamiento de transacciones WMS: " & ex.Message, ex)
        End Try
    End Function

    Public Shared Function MapearAAjustes(transaccionesAgrupadas As List(Of PedidoTrasladoEncabezado),
                                          pCodigoBodegaInterface As String,
                                          BePropietario As clsBePropietarios,
                                          Optional lblprg As RichTextBox = Nothing) As List(Of clsBeTrans_ajuste_enc)

        Dim lAjustes As New List(Of clsBeTrans_ajuste_enc)()
        If transaccionesAgrupadas Is Nothing OrElse transaccionesAgrupadas.Count = 0 Then Return lAjustes

        ' [TAG:PERF] Cachés locales para evitar consultas repetidas por detalle.
        Dim cacheBodegas As New Dictionary(Of String, clsBeBodega)(StringComparer.OrdinalIgnoreCase)
        Dim cacheProductos As New Dictionary(Of String, clsBeProducto)(StringComparer.OrdinalIgnoreCase)
        Dim cacheUnidadMedida As New Dictionary(Of String, clsBeUnidad_medida)(StringComparer.OrdinalIgnoreCase)
        Dim cacheIdProductoBodega As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        Dim cacheIdPropietarioBodega As New Dictionary(Of Integer, Integer)()
        Dim cacheIdTalla As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        Dim cacheIdColor As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        Dim cacheIdProductoTallaColor As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        Dim nextIdAjusteEnc As Integer = 0
        Dim nextIdAjusteDet As Integer = 0
        Dim nextIdProductoTallaColor As Integer = 0

        ' [TAG:UI] Progreso seguro para no tocar el RichTextBox desde otro hilo.
        ActualizarProgresoSeguro(lblprg, $"Mapeando ajustes ({pCodigoBodegaInterface}): {transaccionesAgrupadas.Count} grupo(s).")

        Using lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))

            lConnection.Open()

            Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                nextIdAjusteEnc = clsLnTrans_ajuste_enc.MaxID(lConnection, lTransaction) + 1
                nextIdAjusteDet = clsLnTrans_ajuste_det.MaxID(lConnection, lTransaction) + 1
                nextIdProductoTallaColor = clsLnProducto_talla_color.MaxID(lConnection, lTransaction) + 1

                For Each ajuste In transaccionesAgrupadas

                    ActualizarProgresoSeguro(lblprg, $"Mapeando ajuste {ajuste.NoEnc} con {If(ajuste.LineasDetalle Is Nothing, 0, ajuste.LineasDetalle.Count)} detalle(s).")

                    Dim BeBodega As clsBeBodega = Nothing
                    If Not cacheBodegas.TryGetValue(ajuste.TransferFromCode, BeBodega) Then
                        BeBodega = clsLnBodega.GetSingle_By_Codigo(ajuste.TransferFromCode, lConnection, lTransaction)
                        cacheBodegas(ajuste.TransferFromCode) = BeBodega
                    End If

                    If BeBodega Is Nothing Then
                        Throw New Exception("No se encontró la bodega: " & ajuste.TransferFromCode)
                    End If

                    Dim idPropietarioBodega As Integer = 0
                    If Not cacheIdPropietarioBodega.TryGetValue(BeBodega.IdBodega, idPropietarioBodega) Then
                        idPropietarioBodega = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(BePropietario.IdPropietario,
                                                                                                                           BeBodega.IdBodega,
                                                                                                                           lConnection,
                                                                                                                           lTransaction)
                        cacheIdPropietarioBodega(BeBodega.IdBodega) = idPropietarioBodega
                    End If

                    ' Parsear fechas de forma segura
                    Dim postingDate As Date
                    If Not Date.TryParse(ajuste.CreateDate, postingDate) Then
                        postingDate = Date.Now
                    End If

                    ' Mapeo del encabezado según tu referencia
                    Dim beAjustes As New clsBeTrans_ajuste_enc With {
                        .IdAjusteenc = nextIdAjusteEnc,
                        .Fecha = postingDate,
                        .Idusuario = 1,
                        .Referencia = ajuste.NoEnc,
                        .Fec_agr = Date.Now,
                        .User_agr = "MI3",
                        .Fec_mod = Date.Now,
                        .User_mod = "MI3",
                        .IdBodega = BeBodega.IdBodega,
                        .IdProductoFamilia = 0,
                        .Enviado_A_ERP = False,
                        .IdPropietarioBodega = idPropietarioBodega,
                        .Ajuste_Por_Inventario = 0,
                        .IdCentroCosto = 0,
                        .Auditado = False,
                        .Centro_Costo_Erp = "",
                        .Centro_Costo_Dir_Erp = "",
                        .Centro_Costo_Dep_Erp = "",
                        .Lineas_Detalle = New List(Of clsBeTrans_ajuste_det)()
                    }

                    nextIdAjusteEnc += 1

                    Dim RowsEncabezadoAjuste As Integer = clsLnTrans_ajuste_enc.Insertar(beAjustes)

                    If RowsEncabezadoAjuste = 0 Then
                        Throw New Exception("No se pudo insertar el encabezado del ajuste " & ajuste.NoEnc)
                    End If

                    ' Mapeo de las líneas de detalle según tu referencia
                    Dim indiceDetalle As Integer = 0
                    For Each detalle In ajuste.LineasDetalle
                        indiceDetalle += 1
                        If indiceDetalle = 1 OrElse indiceDetalle Mod 10 = 0 OrElse indiceDetalle = ajuste.LineasDetalle.Count Then
                            ActualizarProgresoSeguro(lblprg, $"Mapeando ajuste {ajuste.NoEnc}: {indiceDetalle}/{ajuste.LineasDetalle.Count} detalle(s).")
                        End If

                        Dim productoKey As String = beAjustes.IdBodega.ToString() & "|" & detalle.ItemNo
                        Dim BeProducto As clsBeProducto = Nothing
                        If Not cacheProductos.TryGetValue(productoKey, BeProducto) Then
                            BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(detalle.ItemNo, beAjustes.IdBodega, lConnection, lTransaction)
                            cacheProductos(productoKey) = BeProducto
                        End If

                        If BeProducto Is Nothing Then
                            Dim vMsgExProd As String = "El producto: " & detalle.ItemNo & " no existe"
                            Throw New Exception(vMsgExProd)
                        End If

                        Dim vIdProductoBodega As Integer = 0
                        If Not cacheIdProductoBodega.TryGetValue(productoKey, vIdProductoBodega) Then
                            vIdProductoBodega = clsLnProducto.Get_IdProductoBodega_By_Codigo_And_IdBodega(detalle.ItemNo, beAjustes.IdBodega, lConnection, lTransaction)
                            cacheIdProductoBodega(productoKey) = vIdProductoBodega
                        End If
                        'Dim vIdProductoBodega As Integer = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(BeProducto.IdProducto, beAjustes.IdBodega, lConnection, lTransaction)
                        Dim vIdProductoEstado As Integer = clsLnProducto_estado.Get_Buen_Estado_Producto_By_IdPropietario(BePropietario.IdPropietario, lConnection, lTransaction)
                        Dim BeUnidadMedida As clsBeUnidad_medida = Nothing

                        If Not cacheUnidadMedida.TryGetValue(detalle.UnitOfMeasureCode, BeUnidadMedida) Then
                            BeUnidadMedida = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario(detalle.UnitOfMeasureCode,
                                                                                                   BeConfigEnc.IdPropietario,
                                                                                                   lConnection,
                                                                                                   lTransaction)
                            cacheUnidadMedida(detalle.UnitOfMeasureCode) = BeUnidadMedida
                        End If

                        If BeUnidadMedida Is Nothing Then
                            Dim vMsgEx2 As String = "La U.M básica de producto: " & detalle.ItemNo & " no existe o no está definida: " & detalle.UnitOfMeasureCode
                            Throw New Exception(vMsgEx2)
                        Else
                            BeProducto.UnidadMedida = BeUnidadMedida
                        End If

                        Dim IdTalla As Integer = 0
                        If Not cacheIdTalla.TryGetValue(detalle.Size, IdTalla) Then
                            Dim beTallaTmp = clsLnTalla.Get_Single_By_Codigo(detalle.Size)
                            If beTallaTmp Is Nothing Then
                                Throw New Exception("La talla no existe: " & detalle.Size)
                            End If
                            IdTalla = beTallaTmp.IdTalla
                            cacheIdTalla(detalle.Size) = IdTalla
                        End If

                        Dim IdColor As Integer = 0
                        If Not cacheIdColor.TryGetValue(detalle.Color, IdColor) Then
                            Dim beColorTmp = clsLnColor.GetSingle_By_CodigoColor(detalle.Color)
                            If beColorTmp Is Nothing Then
                                Throw New Exception("El color no existe: " & detalle.Color)
                            End If
                            IdColor = beColorTmp.IdColor
                            cacheIdColor(detalle.Color) = IdColor
                        End If

                        Dim ptcKey As String = BeProducto.IdProducto.ToString() & "|" & detalle.Size & "|" & detalle.Color
                        Dim IdProductoTallaColor As Integer = 0
                        If Not cacheIdProductoTallaColor.TryGetValue(ptcKey, IdProductoTallaColor) Then
                            IdProductoTallaColor = clsLnProducto_talla_color.Get_IdProductoTallaColor_By_CodTalla_and_CodColor(detalle.Size,
                                                                                                                               detalle.Color,
                                                                                                                               BeProducto.IdProducto,
                                                                                                                               lConnection,
                                                                                                                               lTransaction)
                            If IdProductoTallaColor = 0 Then
                                Dim BeProductoTallaColor As New clsBeProducto_talla_color With {
                                    .IdProductoTallaColor = nextIdProductoTallaColor,
                                    .IdProducto = BeProducto.IdProducto,
                                    .IdTalla = IdTalla,
                                    .IdColor = IdColor,
                                    .CodigoSKU = BeProducto.Codigo & detalle.Color & detalle.Size,
                                    .IdCampaña = 0,
                                    .Fec_agr = Now,
                                    .User_agr = beAjustes.Idusuario,
                                    .Fec_mod = Now,
                                    .User_mod = beAjustes.Idusuario,
                                    .Activo = True
                                }

                                Dim RowInsertadas As Integer = clsLnProducto_talla_color.Insertar(BeProductoTallaColor,
                                                                                                  lConnection,
                                                                                                  lTransaction)
                                If RowInsertadas = 0 Then
                                    Throw New Exception("No se pudo insertar la talla y el color")
                                End If

                                IdProductoTallaColor = BeProductoTallaColor.IdProductoTallaColor
                                nextIdProductoTallaColor += 1
                            End If

                            cacheIdProductoTallaColor(ptcKey) = IdProductoTallaColor
                        End If

                        Dim beAjusteDet As New clsBeTrans_ajuste_det With {
                        .IdAjusteDet = nextIdAjusteDet,
                        .IdAjusteEnc = beAjustes.IdAjusteenc,
                        .IdStock = 0,
                        .IdPropietarioBodega = beAjustes.IdPropietarioBodega,
                        .IdProductoBodega = vIdProductoBodega,
                        .IdProductoEstado = vIdProductoEstado,
                        .IdPresentacion = 0,
                        .IdUnidadMedida = BeUnidadMedida.IdUnidadMedida,
                        .IdUbicacion = BeBodega.Ubic_recepcion,
                        .Lote_original = "",
                        .Lote_nuevo = "",
                        .Fecha_vence_original = New Date(1900, 1, 1),
                        .Fecha_vence_nueva = New Date(1900, 1, 1),
                        .Peso_original = 0,
                        .Peso_nuevo = 0,
                        .Cantidad_original = 0,
                        .Cantidad_nueva = detalle.QtyToShip,
                        .Codigo_producto = detalle.ItemNo,
                        .Nombre_producto = BeProducto.Nombre,
                        .Idtipoajuste = 3,
                        .IdMotivoAjuste = 1,
                        .Observacion = "Ajuste positivo por transacción en Tienda",
                        .Codigo_ajuste = 13,
                        .Enviado = False,
                        .Presentacion = Nothing,
                        .IdBodegaERP = 0,
                        .lic_plate = "",
                        .referencia_ajuste_erp = detalle.DocEntry,
                        .estado_ajuste_erp = False,
                        .IdProductoTallaColor_origen = IdProductoTallaColor,
                        .Talla_origen = detalle.Size,
                        .Color_origen = detalle.Color,
                        .Talla_destino = detalle.Size,
                        .Color_destino = detalle.Color
                        }

                        beAjustes.Lineas_Detalle.Add(beAjusteDet)
                        nextIdAjusteDet += 1

                        Dim RowsDetalleAjuste As Integer = clsLnTrans_ajuste_det.Insertar(beAjusteDet)

                        If RowsDetalleAjuste = 0 Then
                            Throw New Exception("No se pudo insertar el detalle del ajuste " & detalle.DocEntry)
                        End If

                    Next

                    ' Solo agregar si tiene líneas de detalle
                    If beAjustes.Lineas_Detalle.Any() Then
                        lAjustes.Add(beAjustes)
                    End If
                Next

                lTransaction.Commit()

            End Using

            lConnection.Close()

        End Using

        Return lAjustes

    End Function

    Private Shared Async Function Procesar_Documentos_Ajustes(ByVal codigoBodega As String,
                                                              ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                              ByVal lblprg As RichTextBox) As Task(Of Boolean)

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Conectando a SAP.")

            Dim ajustes As List(Of clsBeTrans_ajuste_enc) = Await Get_Ajustes_Tiendas(codigoBodega, lblprg)
            Dim ajustes_correctos As Integer = 0
            Dim ajustes_incorrectos As Integer = 0

            If ajustes.Count = 0 Then
                clsPublic.Actualizar_Progreso(lblprg, "No hay documentos para importar.")
                Return False
            End If

            For Each ajuste In ajustes

                clsPublic.Actualizar_Progreso(lblprg, $"Procesando pedido de cliente de SAP (@Transac_WMS): {ajuste.Referencia}/{ajuste.IdBodega}{vbNewLine}")

                Dim listaDocEntryDistintos As New List(Of Integer)
                For Each detalle In ajuste.Lineas_Detalle
                    Dim docEntry As Integer = 0
                    If Integer.TryParse(Convert.ToString(detalle.referencia_ajuste_erp), docEntry) Then
                        listaDocEntryDistintos.Add(docEntry)
                    End If
                Next
                listaDocEntryDistintos = listaDocEntryDistintos.Distinct().ToList()

                Dim ctx As TransacWmsTraceContext = CrearContextoTransacWms("AJUSTE",
                                                                            "100",
                                                                            ajuste.Referencia,
                                                                            ajuste.Referencia,
                                                                            Convert.ToString(ajuste.IdBodega),
                                                                            "",
                                                                            listaDocEntryDistintos)
                Dim clsTrans As New clsTransaccion
                clsTrans.Begin_Transaction()

                Try

                    Dim pIdEmpresa As Integer = BeConfigEnc.Idempresa
                    Dim CreoAjuste As Boolean = clsLnTrans_ajuste_enc.Inserta_Stock_Y_Movimiento(ajuste,
                                                                                                 pIdEmpresa,
                                                                                                 clsTrans.lConnection,
                                                                                                 clsTrans.lTransaction)

                    If CreoAjuste Then

                        Dim trasladoSincronizado As Boolean = Await Marcar_Transac_Wms_Por_DocEntries_SLAsync(listaDocEntryDistintos,
                                                                                                               vHanaService.SessionCookie,
                                                                                                               BD.Instancia.HANA_SL)

                        If trasladoSincronizado Then
                            clsTrans.Commit_Transaction()
                            RegistrarTrazaTransacWms(ctx, "AJUSTE_APLICACION_WMS", "OK", "OK", "Documento procesado correctamente.")
                            clsPublic.Actualizar_Progreso(lblprg, "Documento procesado correctamente :) !")
                        Else
                            Throw New Exception("No se pudo marcar en SAP (SL). Se revierte la transacción.")
                        End If

                        ajustes_correctos += 1

                    Else
                        ajustes_incorrectos += 1
                        Throw New Exception("No se pudo aplicar el ajuste en WMS.")
                    End If

                Catch ex As Exception
                    clsTrans.RollBack_Transaction()
                    RegistrarFalloTransacWmsAsync(ctx,
                                                  "AJUSTE_APLICACION_WMS",
                                                  ex,
                                                  vHanaService.SessionCookie,
                                                  BD.Instancia.HANA_SL,
                                                  lblprg).GetAwaiter().GetResult()
                Finally
                    clsTrans.Close_Conection()
                End Try

            Next

            Return True

        Catch ex As Exception
            Throw
        End Try

    End Function

#End Region

#Region "Anulación devolución de cliente"

    Private Shared Function Get_Anulacion_Devolucion_Tiendas(pCodigoBodegaInterface As String,
                                                         lblprg As RichTextBox) As List(Of clsBeI_nav_ped_traslado_enc)

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePropietario As clsBePropietarios = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario)

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

            clsPublic.Actualizar_Progreso(lblprg, "Obteniendo anulacion(es) de devoluciones de tienda.")

            Dim filtroEnviado As String = "(U_Procesado_WMS eq null or U_Procesado_WMS eq 2)"
            Dim filtroVentas As String = "(U_Document_Type eq '15')"
            Dim filtroFinal As String = $"{filtroEnviado} and {filtroVentas}"

            Dim allRows As JArray = SapServiceBase.ObtenerTransacWmsPaginado(
            filtroFinal,
            vHanaService.SessionCookie,
            BD.Instancia.HANA_SL,
            lblprg)

            If allRows.Count = 0 Then
                Return lPedidosCliente
            End If

            Dim jsonResponse As String = SapServiceBase.CrearJsonResponseDesdeRows(allRows)
            lPedidosCliente = ProcesarTransaccionesWMSCompleto(jsonResponse, pCodigoBodegaInterface, BePropietario, clsDataContractDI.tTipoDocumentoSalida.Anulacion_Devolucion)

            Return lPedidosCliente

        Catch ex As Exception
            Throw New Exception("(SL) Get_Anulacion_Devolucion_Tiendas: " & ex.Message, ex)
        End Try
    End Function

    Public Shared Function Procesar_Anulacion_Devolucion(jsonResponse As String,
                                                         pCodigoBodegaInterface As String,
                                                         BePropietario As clsBePropietarios) As List(Of clsBeI_nav_ped_traslado_enc)
        Try
            ' 1. Deserializar JSON
            Dim response As TRANSAC_WMS_Response = JsonConvert.DeserializeObject(Of TRANSAC_WMS_Response)(jsonResponse)

            ' 2. Agrupar por número de encabezado
            Dim transaccionesAgrupadas As New List(Of PedidoTrasladoEncabezado)()
            Dim agrupamiento = response.Value.GroupBy(Function(x) x.U_NoEnc)

            For Each grupo In agrupamiento
                Dim primerRegistro = grupo.First()
                Dim encabezado As New PedidoTrasladoEncabezado With {
                .NoEnc = primerRegistro.U_NoEnc,
                .ExternalDocumentNo = primerRegistro.U_External_Document_No,
                .Serie = primerRegistro.U_Serie,
                .CompanyCode = primerRegistro.U_Company_Code,
                .PostingDate = primerRegistro.U_Posting_Date,
                .CreateDate = primerRegistro.CreateDate,
                .TransferFromCode = primerRegistro.U_Transfer_from_Code,
                .TransferFromContact = primerRegistro.U_Transfer_from_Contact,
                .TransferToCode = primerRegistro.U_Transfer_to_Code,
                .TransferToName = primerRegistro.U_Transfer_to_Name,
                .ReceipDocumentReference = primerRegistro.U_Receip_Document_Reference,
                .DocumentType = primerRegistro.U_Document_Type,
                .LineasDetalle = New List(Of PedidoTrasladoDetalle)()
            }

                ' Agregar líneas
                For Each transaccion In grupo
                    encabezado.LineasDetalle.Add(New PedidoTrasladoDetalle With {
                .LineNo = transaccion.U_Line_No,
                .ItemNo = transaccion.U_Item_No,
                .Descripcion = transaccion.U_Descripcion,
                .UnitOfMeasureCode = transaccion.U_Unit_of_Mesasure_Code,
                .QtyToShip = transaccion.U_Qty_to_Ship,
                .QtyWMS = transaccion.U_Qty_WMS,
                .Color = transaccion.U_Color,
                .Size = transaccion.U_Size,
                .ProcesadoWMS = transaccion.U_Procesado_WMS,
                .ProcessResult = transaccion.U_Process_Result,
                .DocEntry = transaccion.DocEntry
            })
                Next

                ' Ordenar líneas
                encabezado.LineasDetalle = encabezado.LineasDetalle.OrderBy(Function(x) x.LineNo).ToList()
                transaccionesAgrupadas.Add(encabezado)
            Next

            ' 3. Mapear a clases de negocio
            Return MapearAClasesNegocio(transaccionesAgrupadas, pCodigoBodegaInterface, BePropietario)

        Catch ex As Exception
            Throw New Exception("Error completo en procesamiento de transacciones WMS: " & ex.Message, ex)
        End Try
    End Function

    Public Shared Function Mapear_A_Clases_Anulacion_Devolucion(transaccionesAgrupadas As List(Of PedidoTrasladoEncabezado),
                                                                pCodigoBodegaInterface As String,
                                                                BePropietario As Object) As List(Of clsBeI_nav_ped_traslado_enc)

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)()

        For Each pedido In transaccionesAgrupadas
            ' Parsear fechas de forma segura
            Dim postingDate As Date
            If Not Date.TryParse(pedido.CreateDate, postingDate) Then
                postingDate = Date.Now
            End If

            ' Mapeo del encabezado según tu referencia
            Dim beFacturaDeudor As New clsBeI_nav_ped_traslado_enc With {
            .No = pedido.NoEnc,
            .Posting_Date = postingDate,
            .Receipt_Date = postingDate,
            .Shipment_Date = postingDate,
            .Status = 1,
            .Transfer_from_Code = pCodigoBodegaInterface,
            .Transfer_from_Contact = pedido.TransferFromContact,
            .Transfer_to_Contact = pedido.TransferToName,
            .Transfer_to_CodeField = pedido.TransferToCode,
            .Transfer_to_Code = pedido.TransferToCode,
            .Product_Owner_Code = BePropietario.Codigo,
            .Receipt_Document_Reference = pedido.ExternalDocumentNo,
            .Company_Code = pedido.CompanyCode,
            .Comments = $"Serie: {pedido.Serie} - Documento: {pedido.ExternalDocumentNo}",
            .Document_Type = tTipoDocumentoSalida.Factura_Deudor,
            .Transportation_Guide = pedido.ReceipDocumentReference,
            .External_Document_No = pedido.ExternalDocumentNo,
            .Transfer_to_Name = pedido.TransferToName,
            .Lineas_Detalle = New List(Of clsBeI_nav_ped_traslado_det)()
        }

            ' Mapeo de las líneas de detalle según tu referencia
            For Each detalle In pedido.LineasDetalle
                ' Verificar filtro por bodega si es necesario
                ' If detalle.AlgunCampoBodega <> pCodigoBodegaInterface Then Continue For

                Dim beDet As New clsBeI_nav_ped_traslado_det With {
                .NoEnc = beFacturaDeudor.No,
                .No = (clsLnI_nav_ped_traslado_det.MaxID() + 1).ToString(), ' Asegurar que sea string según la propiedad
                .Item_No = detalle.ItemNo,
                .Line_No = detalle.LineNo,
                .Shipment_Date = Date.Now,
                .Quantity = CDec(detalle.QtyToShip),
                .Qty_to_Ship = CDec(detalle.QtyToShip),
                .Description = detalle.Descripcion,
                .Unit_of_Measure_Code = detalle.UnitOfMeasureCode,
                .Status = 1,
                .Transfer_to_CodeField = beFacturaDeudor.Transfer_to_Code,
                .Price = 0.0, ' No disponible en el JSON original, establecer valor por defecto
                .Color = detalle.Color,
                .Size = detalle.Size,
                .Variant_Code = Nothing,
                .Process_Result = detalle.ProcessResult,
                .DocEntry = detalle.DocEntry
            }

                beFacturaDeudor.Lineas_Detalle.Add(beDet)
            Next

            ' Solo agregar si tiene líneas de detalle
            If beFacturaDeudor.Lineas_Detalle.Any() Then
                lPedidosCliente.Add(beFacturaDeudor)
            End If
        Next

        Return lPedidosCliente
    End Function

    ' Clases para el agrupamiento
    Public Class AnulacionDevolucionEncabezado
        Public Property NoEnc As String
        Public Property ExternalDocumentNo As String
        Public Property Serie As String
        Public Property CompanyCode As String
        Public Property PostingDate As String
        Public Property CreateDate As String
        Public Property TransferFromCode As String
        Public Property TransferFromContact As String
        Public Property TransferToCode As String
        Public Property TransferToName As String
        Public Property ReceipDocumentReference As String
        Public Property DocumentType As String
        Public Property LineasDetalle As List(Of PedidoTrasladoDetalle)
        Public Sub New()
            LineasDetalle = New List(Of PedidoTrasladoDetalle)()
        End Sub
    End Class

    Public Class AnulacionDevolucionDetalle
        Public Property LineNo As Integer
        Public Property ItemNo As String
        Public Property Descripcion As String
        Public Property UnitOfMeasureCode As String
        Public Property QtyToShip As Double
        Public Property QtyWMS As Double?
        Public Property Color As String
        Public Property Size As String
        Public Property ProcesadoWMS As String
        Public Property ProcessResult As String

        Public DocEntry As Integer = 0
    End Class

    Public Shared Async Function Procesar_Anulacion_Devolucion_SAP(ByVal lblprg As RichTextBox,
                                                                   ByVal prg As System.Windows.Forms.ProgressBar,
                                                                   Optional ByVal pNoDocumento As String = "") As Task(Of Boolean)
        Dim sw As New Stopwatch()

        Try
            ReiniciarContextoLogTransacWms()

            ' Inicia cronómetro y anuncia inicio
            sw.Start()
            clsPublic.Actualizar_Progreso(lblprg, "Iniciando proceso de sincronización de anulación de devoluciones de cliente desde SAP.")


            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

            Dim sessionCookie As String = ""
            Dim baseUrl As String = BD.Instancia.HANA_SL
            Dim BeBodega As clsBeBodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega)

            If BeBodega Is Nothing Then
                Throw New Exception("ERROR_202311271751: Error no se pudo obtener el objeto de bodega asociado a la configuración de interface: " & BeConfigEnc.Idbodega)
            End If

            Await Procesar_Documentos_Anulacion_Devolucion(BeBodega.Codigo,
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
            Dim ctx As TransacWmsTraceContext = CrearContextoTransacWms("PULL_TRANSAC_WMS",
                                                                        "15",
                                                                        pNoDocumento,
                                                                        pNoDocumento,
                                                                        "",
                                                                        "",
                                                                        New List(Of Integer)())
            RegistrarTrazaTransacWms(ctx, "PULL_TRANSAC_WMS", "ERROR", "EXCEPCION_GENERAL", NormalizarMensajeError(ex))
            clsPublic.Actualizar_Progreso(lblprg, $"Error en el proceso: {ex.Message}. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.")
            Throw
        Finally
            If sw.IsRunning Then sw.Stop()
            clsPublic.Actualizar_Progreso(lblprg, $"Fin del proceso de sincronización de los pedidos de cliente desde SAP. Tiempo total: {sw.Elapsed.TotalSeconds:F2} segundos.")
        End Try

    End Function

    Private Shared Async Function Procesar_Documentos_Anulacion_Devolucion(ByVal codigoBodega As String,
                                                                           ByVal pNoDocumento As String,
                                                                           ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                                           ByVal lblprg As RichTextBox) As Task(Of Boolean)

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Conectando a SAP.")

            Dim anulacion_devolucion As List(Of clsBeI_nav_ped_traslado_enc) = Get_Anulacion_Devolucion_Tiendas(codigoBodega, lblprg)
            Dim pBePedidoEnc As New clsBeTrans_pe_enc
            Dim PedidoClienteExistenteByCompany As New clsBeTrans_pe_enc
            Dim PedidoClienteExistente As New clsBeTrans_pe_enc

            If anulacion_devolucion.Count = 0 Then
                clsPublic.Actualizar_Progreso(lblprg, "No hay documentos para importar.")
                Return False
            End If

            For Each anul_devol In anulacion_devolucion

                clsPublic.Actualizar_Progreso(lblprg, $"Procesando anulación de devolución de cliente de SAP (@Transac_WMS): {anul_devol.Receipt_Document_Reference}/{anul_devol.No}{vbNewLine}")

                Dim listaDocEntryDistintos As List(Of Integer) = anul_devol.Lineas_Detalle.Select(Function(x) x.DocEntry) _
                                                                                                     .Distinct() _
                                                                                                     .ToList()
                Dim ctx As TransacWmsTraceContext = CrearContextoTransacWms("ANULACION_DEVOLUCION",
                                                                            "15",
                                                                            anul_devol.No,
                                                                            anul_devol.Receipt_Document_Reference,
                                                                            anul_devol.Transfer_from_Code,
                                                                            anul_devol.Transfer_to_Code,
                                                                            listaDocEntryDistintos)
                Dim clsTrans As New clsTransaccion
                clsTrans.Begin_Transaction()

                Try

                    '#MECR 202508080524: Verifica si el cliente ya existe en WMS.
                    If Await clsSyncSapTrasladosEnvio.Validar_Cliente_WMS(anul_devol.Transfer_to_Code, "C", lblprg, clsTrans, vHanaService.SessionCookie, BD.Instancia.HANA_SL) Then

                        Dim pedidoEnc As clsBeTrans_pe_enc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia_Transac_WMS(anul_devol, lblprg, clsTrans.lConnection, clsTrans.lTransaction)

                        If pedidoEnc IsNot Nothing Then

                            Dim trasladoSincronizado As Boolean = Await Marcar_Transac_Wms_Por_DocEntries_SLAsync(listaDocEntryDistintos,
                                                                                                                  vHanaService.SessionCookie,
                                                                                                                  BD.Instancia.HANA_SL)

                            If pedidoEnc IsNot Nothing AndAlso trasladoSincronizado Then
                                RegistrarTrazaTransacWms(ctx, "APLICAR_WMS", "OK", "OK", "Documento procesado correctamente.")
                                clsPublic.Actualizar_Progreso(lblprg, "Documento procesado correctamente :) !")
                            End If

                        Else
                            Throw New Exception("No se generó pedido WMS para la anulación de devolución TRANSAC_WMS.")
                        End If

                    Else
                        Throw New Exception("No se pudo validar el cliente para la anulación de devolución.")
                    End If

                    clsTrans.Commit_Transaction()

                Catch ex As Exception
                    clsTrans.RollBack_Transaction()
                    RegistrarFalloTransacWmsAsync(ctx,
                                                  "APLICAR_WMS",
                                                  ex,
                                                  vHanaService.SessionCookie,
                                                  BD.Instancia.HANA_SL,
                                                  lblprg).GetAwaiter().GetResult()
                Finally
                    clsTrans.Close_Conection()
                End Try

            Next

            Return False

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Shared Async Function Marcar_Anulacion_Devolucion_Sincronizado_SLAsync(docEntry As String,
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

#Region "Anulación ventas"

    Private Shared Async Function Get_Anulacion_Ventas_Tiendas(pCodigoBodegaInterface As String,
                                                           lblprg As RichTextBox) As Task(Of List(Of clsBeI_nav_ped_compra_enc))

        Dim lAnulacionVentas As New List(Of clsBeI_nav_ped_compra_enc)
        Dim BePropietario As clsBePropietarios = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario)

        If BePropietario Is Nothing Then
            Throw New Exception($"#ERROR: No se encontró el propietario con ID {BeConfigEnc.IdPropietario}")
        End If

        Try

            vHanaService = New SapServiceLayerClient()

            Dim loginResponse As LoginResponseDto = vHanaService.LoginAsync().GetAwaiter().GetResult()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener sesión.")
                Return lAnulacionVentas
            Else
                clsPublic.Actualizar_Progreso(lblprg, "Conexión correcta.")
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Obteniendo anulacion(es) de ventas.")

            Dim filtroEnviado As String = "(U_Procesado_WMS eq null or U_Procesado_WMS eq 2)"
            Dim filtroVentas As String = "(U_Document_Type eq '18')"
            Dim filtroFinal As String = $"{filtroEnviado} and {filtroVentas}"

            Dim allRows As JArray = SapServiceBase.ObtenerTransacWmsPaginado(filtroFinal,
                                                                             vHanaService.SessionCookie,
                                                                             BD.Instancia.HANA_SL,
                                                                             lblprg)

            If allRows.Count = 0 Then
                Return lAnulacionVentas
            End If

            Dim grupos = allRows _
            .OfType(Of JObject)() _
            .GroupBy(Function(r) New With {
                Key .DocEntry = r.Value(Of Integer?)("U_NoEnc").GetValueOrDefault(-1),
                Key .CodBodega = r.Value(Of String)("U_Transfer_From_Code")
            })

            For Each grupo In grupos

                Dim docEntry As Integer = grupo.Key.DocEntry
                Dim rowEnc As JObject = grupo.First()
                Dim documentLines As New JArray()

                For Each row As JObject In grupo
                    Dim linea As New JObject()
                    linea("ItemCode") = row("U_Item_No")
                    linea("LineNum") = row("U_Line_No")
                    linea("Planed_Receipt_Date") = row("U_Posting_Date")
                    linea("Quantity") = row("U_Qty_to_Ship")
                    linea("ItemDescription") = row("U_Descripcion")
                    linea("U_Color") = row("U_Color")
                    linea("U_Talla") = row("U_Size")
                    linea("WarehouseCode") = row("U_Transfer_From_Code")
                    linea("BaseEntry") = row("BaseEntry")
                    linea("MeasureUnit") = row("U_Unit_of_Mesasure_Code")
                    documentLines.Add(linea)
                Next

                Dim dtDetTallaColor As DataTable =
                DevolucionTransacWMS_Mapper.ConstruirTablaDesdeJsonTallasColores_Devolucion(documentLines, docEntry, 0)

                Dim encabezado As clsBeI_nav_ped_compra_enc =
                DevolucionTransacWMS_Mapper.MapearEncabezado_Devolucion(rowEnc)

                encabezado.Lineas_Detalle =
                DevolucionTransacWMS_Mapper.MapearDetalle_Devolucion(documentLines)

                encabezado.DocEntriesTransacWms = grupo.
                Select(Function(r) r.Value(Of Integer)("DocEntry")).
                Distinct().
                ToList()

                If dtDetTallaColor IsNot Nothing AndAlso dtDetTallaColor.Rows.Count > 0 Then
                    encabezado.Lineas_Detalle_Talla_Color =
                    Await DevolucionTransacWMS_Mapper.MapearDetalleTallaColor_Devolucion(
                        dtDetTallaColor,
                        IdUsuario,
                        vHanaService.SessionCookie,
                        BD.Instancia.HANA_SL).ConfigureAwait(False)
                Else
                    encabezado.Lineas_Detalle_Talla_Color = New List(Of clsBeProducto_talla_color)()
                End If

                Dim BeCampaña As clsBeCampaña = clsLnCampaña.Get_Single_By_IdCampaña(0)
                encabezado.Campaña = BeCampaña

                lAnulacionVentas.Add(encabezado)
            Next

            Return lAnulacionVentas

        Catch ex As Exception
            Throw New Exception("(SL) Get_Anulacion_Ventas_Tiendas: " & ex.Message, ex)
        End Try
    End Function

    Public Shared Async Function Procesar_Documentos_Anulacion_Ventas(ByVal lblprg As RichTextBox,
                                                                      prg As System.Windows.Forms.ProgressBar) As Task(Of Boolean)

        Dim vResult As String = ""
        Dim vContador As Integer = 0
        Dim BeBodega As New clsBeBodega
        Dim BePedidoCompraEnc As New clsBeTrans_oc_enc

        Try

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega)

            clsPublic.Actualizar_Progreso(lblprg, "Obteniendo documento(s).")

            Dim lAnulacionVenta As New List(Of clsBeI_nav_ped_compra_enc)
            lAnulacionVenta = Await Get_Anulacion_Ventas_Tiendas(BeBodega.Codigo,
                                                                 lblprg)

            If lAnulacionVenta Is Nothing Then
                clsPublic.Actualizar_Progreso(lblprg, "No se obtuvieron anulaciones de ventas.")
                Return False
            End If

            clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format("Anulaciones de venta (TRANSAC_WMS): {0} ", lAnulacionVenta.Count))

            prg.Maximum = lAnulacionVenta.Count

            If clsLnI_nav_ped_compra_det.EliminarTodos() _
                AndAlso clsLnI_nav_ped_compra_enc.EliminarTodos() Then

                Dim BeProveedorBodega As New clsBeProveedor_bodega

                For Each BeINavPedCompra In lAnulacionVenta

                    Dim ctx As TransacWmsTraceContext = CrearContextoTransacWms("ANULACION_VENTA",
                                                                                "18",
                                                                                BeINavPedCompra.No,
                                                                                BeINavPedCompra.Vendor_Invoice_No,
                                                                                BeINavPedCompra.Location_Code,
                                                                                BeINavPedCompra.Buy_From_Vendor_No,
                                                                                BeINavPedCompra.DocEntriesTransacWms)
                    Dim clsTrans As New clsTransaccion
                    clsTrans.Begin_Transaction()

                    Try

                        If Not clsLnProveedor.Existe_Proveedor(BeINavPedCompra.Buy_From_Vendor_No, clsTrans.lConnection, clsTrans.lTransaction) Then

                            BeConfigEnc = BeConfigEnc

                            If Await Inserta_Proveedor_Desde_SAP(BeINavPedCompra.Buy_From_Vendor_No, vHanaService.SessionCookie, BD.Instancia.HANA_SL) Then
                                clsPublic.Actualizar_Progreso(lblprg, vbTab & "El proveedor: " & BeINavPedCompra.Buy_From_Vendor_No & " No existía en WMS y fue insertado.")
                            End If

                        End If

                        clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format("Procesando Anulación Venta: {0} ", BeINavPedCompra.No & " - " & BeINavPedCompra.Vendor_Invoice_No, vbNewLine))

                        Dim procesoOk As Boolean = clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(BeINavPedCompra,
                                                                                                        BePedidoCompraEnc,
                                                                                                        vResult,
                                                                                                        Nothing,
                                                                                                        clsTrans.lConnection,
                                                                                                        clsTrans.lTransaction)

                        If procesoOk Then

                            'Await Marcar_Devolucion_Sincronizada_SLAsync(BeINavPedCompra.No, vHanaService.SessionCookie, BD.Instancia.HANA_SL)
                            Await Marcar_Transac_Wms_Por_DocEntries_SLAsync(BeINavPedCompra.DocEntriesTransacWms,
                                                                            vHanaService.SessionCookie,
                                                                            BD.Instancia.HANA_SL)
                            RegistrarTrazaTransacWms(ctx, "APLICAR_WMS", "OK", "OK", "Documento procesado correctamente.")
                        Else
                            Throw New Exception(If(String.IsNullOrWhiteSpace(vResult),
                                                   "No se pudo procesar la anulación de venta en WMS.",
                                                   vResult))
                        End If

                        clsPublic.Actualizar_Progreso(lblprg, vResult)

                        clsTrans.Commit_Transaction()

                    Catch ex As Exception
                        clsTrans.RollBack_Transaction()
                        RegistrarFalloTransacWmsAsync(ctx,
                                                      "APLICAR_WMS",
                                                      ex,
                                                      vHanaService.SessionCookie,
                                                      BD.Instancia.HANA_SL,
                                                      lblprg).GetAwaiter().GetResult()
                    Finally
                        clsTrans.Close_Conection()
                    End Try
                Next

            End If

            Return True

        Catch ex As Exception
            clsLnLog_error_wms.Agregar_Error("Error_20250422_Fact_Res:" & ex.Message)
            Throw ex
        Finally
            prg.Value = 0
        End Try

    End Function

    Public Shared Async Function Procesar_Anulacion_Ventas_SAP(lblprg As RichTextBox,
                                                               prg As System.Windows.Forms.ProgressBar,
                                                               Optional ByVal ForzarEjecucion As Boolean = False) As Task(Of Boolean)

        Dim inicio As Date = Now
        Dim ok As Boolean = False

        Try
            ReiniciarContextoLogTransacWms()

            Dim ejecutarImportacion As Boolean = True

            If ejecutarImportacion Then
                Dim importo As Boolean = Await Procesar_Documentos_Anulacion_Ventas(lblprg,
                                                                                    prg).ConfigureAwait(False)

                If Not importo Then
                    prg.Value = 0
                    clsPublic.Actualizar_Progreso(lblprg, "No se importaron notas de crédito (devoluciones de cliente).")
                    Return False
                End If
            End If

            ok = True

            ' Log del tiempo transcurrido (fuera de la transacción)
            Dim difSegundos As Double = DateDiff(DateInterval.Second, inicio, Now)
            clsPublic.Actualizar_Progreso(lblprg, vbTab & $" -> Fin de proceso, tiempo transcurrido: {difSegundos} segundo(s)")

            Return ok

        Catch ex As Exception
            prg.Value = 0
            clsLnLog_error_wms.Agregar_Error("Error_20250422_Insert_Fact_Res: " & ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, $"Error al insertar pedido de compra a tabla de TOMWMS: {ex.Message}{vbNewLine}")
            Return False
        End Try
    End Function

    Private Shared Async Function Marcar_Devolucion_Sincronizada_SLAsync(docEntry As String,
                                                                         sessionCookie As String,
                                                                         baseUrl As String) As Task(Of Boolean)

        Try
            If String.IsNullOrWhiteSpace(docEntry) Then Return False

            Dim requestUrl As String = $"TRANSAC_WMS({docEntry})"
            Dim payload As String = "{""U_Procesado_WMS"": 1}"
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

    Public Shared Async Function Inserta_Proveedor_Desde_SAP(ByVal pCodigo As String,
                                                             SessionCookie As String,
                                                             BaseUrl As String,
                                                             lConnection As SqlConnection,
                                                             lTransaction As SqlTransaction) As Task(Of Boolean)


        Dim BeProveedor As New clsBeProveedor
        Dim BeProveedorBodega As New clsBeProveedor_bodega
        Dim BeSAPProveedor As New clsBeI_nav_proveedor
        'Dim clstrans As New clsTransaccion
        Dim vResult As Boolean = False

        Try

            'clstrans.Begin_Transaction()

            BeSAPProveedor = Await clsSyncSAPProveedor.Get_Proveedor_SAP_SLAsync(pCodigo,
                                                                                 SessionCookie,
                                                                                 BaseUrl)

            If Not BeSAPProveedor Is Nothing Then

                BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
                BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
                BeProveedor.IdProveedor = clsLnProveedor.MaxID(lConnection, lTransaction) + 1
                BeProveedor.Codigo = BeSAPProveedor.No
                BeProveedor.Nombre = BeSAPProveedor.Name
                BeProveedor.Telefono = IIf(BeSAPProveedor.Phone_No = Nothing, "", BeSAPProveedor.Phone_No)
                BeProveedor.Nit = BeSAPProveedor.VAT_Registratrion_No
                BeProveedor.Direccion = BeSAPProveedor.Adress
                BeProveedor.Contacto = BeSAPProveedor.Contact
                BeProveedor.Activo = True
                BeProveedor.User_agr = BeConfigEnc.IdUsuario
                BeProveedor.Fec_agr = Date.UtcNow
                BeProveedor.User_mod = BeConfigEnc.IdUsuario
                BeProveedor.Fec_mod = Date.UtcNow

                Try

                    clsLnProveedor.Insertar(BeProveedor, lConnection, lTransaction)

                    BeProveedorBodega = New clsBeProveedor_bodega
                    BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConnection, lTransaction) + 1
                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                    BeProveedorBodega.IdBodega = BeConfigEnc.Idbodega
                    BeProveedorBodega.Activo = True
                    BeProveedorBodega.User_agr = BeConfigEnc.IdUsuario
                    BeProveedorBodega.User_mod = BeConfigEnc.IdUsuario
                    BeProveedorBodega.Fec_agr = Now
                    BeProveedorBodega.Fec_mod = Now

                    clsLnProveedor_bodega.Insertar(BeProveedorBodega, lConnection, lTransaction)

                    Await clsSyncSAPProveedor.Marcar_Proveedor_Sincronizado_SLAsync(BeProveedor.Codigo, SessionCookie, BaseUrl)

                    vResult = True

                Catch ex As Exception

                    clsLnLog_error_wms.Agregar_Error("Error_20250422_Inteface_Proveedor: " & ex.Message & " " & BeProveedor.Codigo)

                    Throw ex

                End Try

            End If

            Return vResult

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

End Class

Public Class TRANSAC_WMS_Response
    <JsonProperty("odata.metadata")>
    Public Property ODataMetadata As String

    <JsonProperty("value")>
    Public Property Value As List(Of TRANSAC_WMS_DTO)

    <JsonProperty("odata.nextLink")>
    Public Property ODataNextLink As String
End Class

Public Class TRANSAC_WMS_DTO
    <JsonProperty("Code")>
    Public Property Code As String

    <JsonProperty("Name")>
    Public Property Name As String

    <JsonProperty("DocEntry")>
    Public Property DocEntry As Integer

    <JsonProperty("Canceled")>
    Public Property Canceled As String

    <JsonProperty("LogInst")>
    Public Property LogInst As Integer?

    <JsonProperty("UserSign")>
    Public Property UserSign As Integer?

    <JsonProperty("Transfered")>
    Public Property Transfered As String

    <JsonProperty("CreateDate")>
    Public Property CreateDate As String

    <JsonProperty("CreateTime")>
    Public Property CreateTime As String

    <JsonProperty("UpdateDate")>
    Public Property UpdateDate As String

    <JsonProperty("UpdateTime")>
    Public Property UpdateTime As String

    <JsonProperty("DataSource")>
    Public Property DataSource As String

    ' Campos UDF (User Defined Fields)
    <JsonProperty("U_NoEnc")>
    Public Property U_NoEnc As String

    <JsonProperty("U_External_Document_No")>
    Public Property U_External_Document_No As String

    <JsonProperty("U_Serie")>
    Public Property U_Serie As String

    <JsonProperty("U_Company_Code")>
    Public Property U_Company_Code As String

    <JsonProperty("U_Posting_Date")>
    Public Property U_Posting_Date As String

    <JsonProperty("U_Transfer_from_Code")>
    Public Property U_Transfer_from_Code As String

    <JsonProperty("U_Transfer_from_Contact")>
    Public Property U_Transfer_from_Contact As String

    <JsonProperty("U_Transfer_to_Code")>
    Public Property U_Transfer_to_Code As String

    <JsonProperty("U_Transfer_to_Name")>
    Public Property U_Transfer_to_Name As String

    <JsonProperty("U_Receip_Document_Reference")>
    Public Property U_Receip_Document_Reference As String

    <JsonProperty("U_Document_Type")>
    Public Property U_Document_Type As String

    <JsonProperty("U_Line_No")>
    Public Property U_Line_No As Integer

    <JsonProperty("U_Item_No")>
    Public Property U_Item_No As String

    <JsonProperty("U_Descripcion")>
    Public Property U_Descripcion As String

    <JsonProperty("U_Unit_of_Mesasure_Code")>
    Public Property U_Unit_of_Mesasure_Code As String

    <JsonProperty("U_Qty_to_Ship")>
    Public Property U_Qty_to_Ship As Double

    <JsonProperty("U_Qty_WMS")>
    Public Property U_Qty_WMS As Double?

    <JsonProperty("U_Color")>
    Public Property U_Color As String

    <JsonProperty("U_Size")>
    Public Property U_Size As String

    <JsonProperty("U_Fec_Agr")>
    Public Property U_Fec_Agr As String

    <JsonProperty("U_Fec_WMS")>
    Public Property U_Fec_WMS As String

    <JsonProperty("U_Procesado_WMS")>
    Public Property U_Procesado_WMS As String

    <JsonProperty("U_Process_Result")>
    Public Property U_Process_Result As String

    ' Métodos auxiliares para facilitar el uso
    Public ReadOnly Property EsProcesado As Boolean
        Get
            Return U_Procesado_WMS IsNot Nothing AndAlso U_Procesado_WMS <> "N"
        End Get
    End Property

    Public ReadOnly Property TieneFechaWMS As Boolean
        Get
            Return Not String.IsNullOrEmpty(U_Fec_WMS)
        End Get
    End Property

    Public ReadOnly Property EsCancelado As Boolean
        Get
            Return Canceled = "Y"
        End Get
    End Property

    Public ReadOnly Property EsTransferido As Boolean
        Get
            Return Transfered = "Y"
        End Get
    End Property

End Class
