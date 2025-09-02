Imports System.Data.SqlClient
Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports TOMWMS.clsDataContractDI

Public Class clsSyncSapTrasladosEnvio

    Private Shared vHanaService As SapServiceLayerClient
    Private Const ENTITY_TARGET_STOCK_TRANSFER As String = "StockTransfers"
    Private Const ENTITY_TARGET_STOCK_TRANSFER_REQUEST As String = "InventoryTransferRequests"
    Private Const BASETYPE_INVENTORY_TRANSFER As Integer = 1250000001 ' OWTQ    
    Public Shared Async Function Procesar_Solicitud_Traslado_SAP(ByVal lblprg As RichTextBox,
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
            Throw

        Finally
            clsTrans.Close_Conection()
        End Try
    End Function

    Private Shared Async Function Procesar_Documentos(ByVal codigoBodega As String,
                                                      ByVal pNoDocumento As String,
                                                      ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                      ByVal lblprg As RichTextBox,
                                                      ByVal clsTrans As clsTransaccion) As Task(Of Boolean)

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Conectando a SAP.")

            Dim solicitudes As List(Of clsBeI_nav_ped_traslado_enc) = Get_Traslados_SAP_SL(codigoBodega, clsTrans.lConnection, clsTrans.lTransaction, lblprg, pNoDocumento)
            Dim pBePedidoEnc As New clsBeTrans_pe_enc
            Dim PedidoClienteExistenteByCompany As New clsBeTrans_pe_enc
            Dim PedidoClienteExistente As New clsBeTrans_pe_enc

            If solicitudes.Count = 0 Then
                clsPublic.Actualizar_Progreso(lblprg, "No hay documentos para importar.")
                Return False
            End If

            For Each solicitud In solicitudes

                clsPublic.Actualizar_Progreso(lblprg, $"Procesando solicitud de traslado SAP (OWTQ): {solicitud.Receipt_Document_Reference}/{solicitud.No}{vbNewLine}")

                If Await Validar_Cliente_WMS(solicitud.Transfer_to_Code, "C", lblprg, clsTrans, vHanaService.SessionCookie, BD.Instancia.HANA_SL) Then

                    Dim origenEsWMS As Boolean = clsLnBodega_area.Existe_Codigo_By_IdBodega(solicitud.Transfer_to_Code, BeConfigEnc.Idbodega, clsTrans.lConnection, clsTrans.lTransaction)
                    Dim destinoEsWMS As Boolean = clsLnBodega_area.Existe_Codigo_By_IdBodega(solicitud.Transfer_to_Code, BeConfigEnc.Idbodega, clsTrans.lConnection, clsTrans.lTransaction)
                    Dim debeProcesar As Boolean = Not destinoEsWMS OrElse Not origenEsWMS OrElse (origenEsWMS AndAlso destinoEsWMS)

                    If debeProcesar Then
                        Dim pedidoEnc As clsBeTrans_pe_enc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia_If(solicitud, lblprg, clsTrans.lConnection, clsTrans.lTransaction)

                        Dim trasladoSincronizado As Boolean = Marcar_Traslado_Sincronizado_SLAsync(solicitud.No, vHanaService.SessionCookie, BD.Instancia.HANA_SL).GetAwaiter().GetResult()

                        If pedidoEnc IsNot Nothing AndAlso trasladoSincronizado Then
                            Return True
                        End If
                    End If

                End If

            Next

            Return False

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, ex.Message)
            Return False
        End Try

    End Function

    Public Shared Async Function Validar_Cliente_WMS(ByVal codigoSocioNegocio As String,
                                                     ByVal pTipoSocioNegocio As String,
                                                     ByVal lblprg As RichTextBox,
                                                     ByVal clsTrans As clsTransaccion,
                                                     ByVal sessioncookie As String,
                                                     ByVal baseurl As String) As Task(Of Boolean)

        If String.IsNullOrWhiteSpace(codigoSocioNegocio) Then
            clsPublic.Actualizar_Progreso(lblprg, "Validar_Cliente_WMS: Código de cliente vacío.")
            Return False
        End If

        Try
            Dim clienteWMS As clsBeCliente = clsLnCliente.Existe(codigoSocioNegocio, clsTrans.lConnection, clsTrans.lTransaction)
            If clienteWMS IsNot Nothing Then
                Return True
            End If

            ' Insertar desde SAP (ignoramos el posible retorno y verificamos)
            Await Inserta_Cliente_SAP(codigoSocioNegocio, pTipoSocioNegocio, sessioncookie, baseurl, clsTrans.lConnection, clsTrans.lTransaction)

            ' Verificar nuevamente
            clienteWMS = clsLnCliente.Existe(codigoSocioNegocio, clsTrans.lConnection, clsTrans.lTransaction)
            If clienteWMS IsNot Nothing Then
                clsPublic.Actualizar_Progreso(lblprg, $"El cliente {codigoSocioNegocio} no existía en WMS y fue insertado.")
                Return True
            Else
                clsPublic.Actualizar_Progreso(lblprg, $"No se pudo insertar el cliente {codigoSocioNegocio} en WMS.")
                Return False
            End If

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, $"Validar_Cliente_WMS: {ex.Message}")
            Return False
        End Try
    End Function

    Private Shared Sub InsertarClienteEnBodegas(cliente As clsBeCliente, conn As SqlConnection, trx As SqlTransaction)

        Try

            Dim bodegas = clsLnBodega.Get_All_By_IdEmpresa(BeConfigEnc.Idempresa, conn, trx)

            For Each bodega In bodegas
                Dim clienteBodega As New clsBeCliente_bodega With {
                .IdClienteBodega = clsLnCliente_bodega.MaxID(conn, trx) + 1,
                .IdCliente = cliente.IdCliente,
                .IdBodega = bodega.IdBodega,
                .Activo = True,
                .User_agr = BeConfigEnc.IdUsuario,
                .User_mod = BeConfigEnc.IdUsuario,
                .Fec_agr = Now,
                .Fec_mod = Now,
                .Cliente = cliente
            }

                clsLnCliente_bodega.Insertar_From_Interface(clienteBodega, conn, trx)
            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Shared Async Function MarcarClienteComoEnviadoSAPAsync(ByVal sessionCookie As String,
                                                                   ByVal baseUrl As String,
                                                                   ByVal codigoCliente As String) As Task(Of Boolean)

        Try
            If String.IsNullOrWhiteSpace(codigoCliente) Then Return False

            Dim requestUrl As String = $"BusinessPartners('{codigoCliente}')"
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
                            Throw New Exception($"Error al actualizar OCRD. Código: {response.StatusCode}, Detalle: {errContent}")
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception($"(SL) {MethodBase.GetCurrentMethod().Name} {ex.Message}", ex)
        End Try
    End Function

    Private Shared Async Function Get_Socio_Negocio_SL(ByVal pCodigo As String,
                                                   ByVal pCardType As String,
                                                   ByVal sessionCookie As String,
                                                   ByVal baseUrl As String,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction) As Task(Of JObject)

        Try
            If String.IsNullOrWhiteSpace(pCodigo) Then
                Throw New ArgumentException("El código de cliente no puede ser vacío.")
            End If

            Dim requestUrl As String = $"BusinessPartners?$filter=CardType eq '{pCardType}' and CardCode eq '{pCodigo}'"

            Using handler As New HttpClientHandler()
                handler.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)
                    client.DefaultRequestHeaders.ConnectionClose = True

                    Using request As New HttpRequestMessage(HttpMethod.Get, baseUrl & requestUrl)
                        request.Headers.ConnectionClose = True
                        request.Headers.Add("Cookie", sessionCookie)
                        request.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

                        Dim response = Await client.SendAsync(request).ConfigureAwait(False)

                        If Not response.IsSuccessStatusCode Then
                            Dim err = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                            Throw New Exception($"Error al consultar cliente. Código: {response.StatusCode}, Detalle: {err}")
                        End If

                        Dim json As String = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                        Dim parsed As JObject = JObject.Parse(json)

                        ' Si hay resultados
                        If parsed("value") IsNot Nothing AndAlso parsed("value").HasValues Then
                            Dim firstItem = parsed("value")(0)

                            ' Verificar si existe ContactEmployees y si tiene al menos un elemento
                            If firstItem("ContactEmployees") IsNot Nothing AndAlso firstItem("ContactEmployees").HasValues Then
                                Dim firstContact = firstItem("ContactEmployees")(0)
                                Dim contactoNombre As String = firstContact("FirstName")?.ToString()
                                Dim contactoPuesto As String = firstContact("Name")?.ToString()
                                ' Puedes guardar como texto combinado
                                firstItem("Contacto") = $"{contactoNombre} - {contactoPuesto}"

                            Else
                                ' Si no hay contactos, dejar nulo o vacío
                                firstItem("Contacto") = Nothing
                            End If
                        End If


                        Return parsed
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception($"(SL) {MethodBase.GetCurrentMethod().Name} {ex.Message}", ex)
        End Try

    End Function

    Private Shared Function ConstruirClienteDesdeServiceLayer(json As JObject,
                                                           conn As SqlConnection,
                                                           trx As SqlTransaction) As clsBeCliente
        Try
            If json Is Nothing OrElse json("value") Is Nothing OrElse Not json("value").HasValues Then
                Throw New Exception("El JSON recibido no contiene datos de cliente.")
            End If

            Dim data = json("value")(0)

            Return New clsBeCliente With {
            .IdCliente = clsLnCliente.MaxID(conn, trx) + 1,
            .IdPropietario = BeConfigEnc.IdPropietario,
            .Codigo = data("CardCode")?.ToString(),
            .Nombre_comercial = data("CardName")?.ToString(),
            .Nombre_contacto = data("Contacto")?.ToString(),
            .Sistema = True,
            .Activo = True,
            .IdEmpresa = BeConfigEnc.Idempresa,
            .Nit = data("U_NIT")?.ToString(),
            .IdTipoCliente = 1,
            .Es_bodega_recepcion = False,
            .Es_Bodega_Traslado = False
        }

        Catch ex As Exception
            Throw New Exception($"Error al construir cliente desde JSON: {ex.Message}", ex)
        End Try
    End Function


    Private Shared Async Function Inserta_Cliente_SAP(ByVal pCodigo As String,
                                                      ByVal pTipo As String,
                                                      ByVal sessionCookie As String,
                                                      ByVal baseUrl As String,
                                                      ByVal lConnection As SqlConnection,
                                                      ByVal lTransaction As SqlTransaction) As Task(Of Boolean)

        Try


            Dim jsonCliente As JObject = Await Get_Socio_Negocio_SL(pCodigo, pTipo.ToUpper(), sessionCookie, baseUrl, lConnection, lTransaction)
            Dim cliente As clsBeCliente = ConstruirClienteDesdeServiceLayer(jsonCliente, lConnection, lTransaction)

            clsLnCliente.Insertar(cliente, lConnection, lTransaction)
            InsertarClienteEnBodegas(cliente, lConnection, lTransaction)
            Await MarcarClienteComoEnviadoSAPAsync(sessionCookie, baseUrl, pCodigo)

            Return True

        Catch ex As Exception
            Throw New Exception("No se pudo insertar el cliente nuevo proveniente de SAP: " & ex.Message)
        End Try

    End Function

    Private Shared Async Function Marcar_Traslado_Sincronizado_SLAsync(docEntry As String,
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
                            Throw New Exception($"Error al actualizar OWTQ. Código: {response.StatusCode}, Detalle: {errContent}")
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception($"(SL) {MethodBase.GetCurrentMethod().Name} {ex.Message}", ex)
        End Try

    End Function

    Private Shared Function Get_Traslados_SAP_SL(pCodigoBodegaInterface As String,
                                                 lConnection As SqlConnection,
                                                 lTransaction As SqlTransaction,
                                                 lblprg As RichTextBox,
                                                 Optional pNoDocumentoSAP As String = "") As List(Of clsBeI_nav_ped_traslado_enc)

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePropietario As clsBePropietarios = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario, lConnection, lTransaction)

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

            Dim filtroEstado As String = "DocumentStatus eq 'bost_Open'"
            Dim filtroEnviado As String = "U_ENVIADO_WMS eq 2"
            Dim filtroBodega As String = $"( and FromWarehouse eq '{pCodigoBodegaInterface}')"
            Dim filtroDocNum As String = If(Not String.IsNullOrWhiteSpace(pNoDocumentoSAP), $" and DocNum eq {pNoDocumentoSAP}", "")
            Dim filtroFinal As String = $"{filtroEstado} and {filtroEnviado} and {filtroBodega}{filtroDocNum}"
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

                    For Each traslado In parsed("value")

                        Dim U_Transito = traslado("U_Transito").Value(Of String)

                        Dim bePedido As New clsBeI_nav_ped_traslado_enc With {
                        .No = traslado("DocEntry").Value(Of Integer),
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
                        .Document_Type = tTipoDocumentoSalida.Transferencia_Interna_WMS,
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

    Public Shared Async Function Enviar_Traslados_Desde_Solicitud(ByVal lblprg As RichTextBox,
                                                                  ByVal prg As ProgressBar,
                                                                  ByVal pTipo As tTipoDocumentoSalida) As Task(Of Boolean)

        Dim envioExitosoCount As Integer = 0
        Dim huboError As Boolean = False
        Dim totalPedidos As Integer = 0

        Dim clsTrans As New clsTransaccion()

        Using CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
            Try
                CnnLog.Open()
                clsTrans.Begin_Transaction()

                Dim lTransaccionesSalida As List(Of clsBeI_nav_transacciones_out) =
                clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(pTipo, clsTrans.lConnection, clsTrans.lTransaction)

                If lTransaccionesSalida Is Nothing OrElse lTransaccionesSalida.Count = 0 Then
                    clsPublic.Actualizar_Progreso(lblprg, "MSG_240117: No hay transacciones para enviar.")
                    clsTrans.Commit_Transaction()
                    Return False
                End If

                Dim ListaPedidosTransf =
                (From i In lTransaccionesSalida
                 Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc} Into Group
                 Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc}).ToList()

                totalPedidos = ListaPedidosTransf.Count
                clsPublic.Actualizar_Progreso(lblprg, $"Documentos a enviar: {totalPedidos}")

                For Each PT In ListaPedidosTransf

                    Dim BePedidoEnc As clsBeTrans_pe_enc = clsLnTrans_pe_enc.GetSingle(PT.Idpedidoenc, clsTrans.lConnection, clsTrans.lTransaction)

                    clsPublic.Actualizar_Progreso(lblprg, $"Procesando Pedido: {PT.Idpedidoenc}-{BePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino}")

                    Dim yaEnviado As Boolean =
                    clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP(PT.No_pedido, clsTrans.lConnection, clsTrans.lTransaction)

                    If yaEnviado Then
                        clsPublic.Actualizar_Progreso(lblprg, "El pedido ya está marcado como enviado a ERP; se omite su reenvío.")
                        Continue For
                    End If

                    Dim lTransaccionesSalidaSingle As List(Of clsBeI_nav_transacciones_out) =
                    lTransaccionesSalida.FindAll(Function(x) x.No_pedido = PT.No_pedido AndAlso x.Idpedidoenc = PT.Idpedidoenc)

                    Dim enviadoOk As Boolean = Await Enviar_Traslado_Desde_Solicitud_SAP(PT.No_pedido,
                                                                                         BePedidoEnc,
                                                                                         lTransaccionesSalidaSingle,
                                                                                         clsTrans,
                                                                                         lblprg).ConfigureAwait(False)

                    If enviadoOk Then
                        Try
                            clsPublic.Actualizar_Progreso(lblprg, "Transacciones de salida enviadas correctamente.")
                            clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario, clsTrans.lConnection, clsTrans.lTransaction)
                            envioExitosoCount += 1
                        Catch ex As Exception
                            huboError = True
                            clsPublic.Actualizar_Progreso(lblprg, $"Error al registrar el pedido {PT.No_pedido} en el ERP. Error: {ex.Message}")
                            clsLnLog_error_wms.Agregar_Error(ex.Message)
                        End Try
                    Else
                        huboError = True
                        Dim vMsgErr As String = $"No se pudo enviar el pedido {PT.No_pedido} al ERP."
                        clsPublic.Actualizar_Progreso(lblprg, vMsgErr)
                        Throw New Exception(vMsgErr)
                    End If
                Next

                clsTrans.Commit_Transaction()

                ' Resultado final:
                ' True  → hubo al menos un envío exitoso y no se detectaron errores.
                ' False → no hubo envíos exitosos o hubo algún error.
                Dim resultado As Boolean = (envioExitosoCount > 0 AndAlso Not huboError)
                Return resultado

            Catch ex As Exception
                clsTrans.RollBack_Transaction()
                Return False
            Finally
                prg.Value = 0
                prg.Visible = False
                If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
                clsTrans.Close_Conection()
            End Try
        End Using
    End Function

    Private Shared Async Function Enviar_Traslado_Desde_Solicitud_SAP(ByVal _DocEntry As Integer,
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
                Dim payloadStockTransfer = Build_StockTransfer_Payload(_DocEntry,
                                                                       BePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino,
                                                                       BePedidoEnc.Bodega_Origen,
                                                                       BePedidoEnc.Bodega_Destino,
                                                                       transaccionesOut)

                Dim handler As New HttpClientHandler With {
                .AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate,
                .ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True,
                .UseCookies = False
            }

                Using http As New HttpClient(handler) With {.BaseAddress = New Uri(SapServiceLayerClient.baseUrl)}
                    Dim json As String = JsonConvert.SerializeObject(payloadStockTransfer, New JsonSerializerSettings With {.NullValueHandling = NullValueHandling.Ignore})
                    Dim content = New StringContent(json, Encoding.UTF8)
                    Dim mediaType = New MediaTypeHeaderValue("application/json")
                    mediaType.CharSet = "utf-8"
                    content.Headers.ContentType = mediaType

                    Dim req As New HttpRequestMessage(HttpMethod.Post, ENTITY_TARGET_STOCK_TRANSFER) With {.Content = content}
                    req.Headers.Add("Cookie", vHanaService.SessionCookie)
                    req.Headers.ConnectionClose = True

                    Dim resp = Await http.SendAsync(req).ConfigureAwait(False)
                    Dim body = Await resp.Content.ReadAsStringAsync().ConfigureAwait(False)

                    ' Parsear el JSON
                    Dim jsonObj As JObject = JObject.Parse(body)

                    Dim docEntry As Integer = 0
                    Dim docNum As Integer = 0

                    If resp.IsSuccessStatusCode Then
                        ' Capturar los valores
                        docEntry = jsonObj("DocEntry")
                        docNum = jsonObj("DocNum")
                    End If

                    If resp.IsSuccessStatusCode Then

                        clsPublic.Actualizar_Progreso(lblprg, "✅ Respuesta:")
                        clsPublic.Actualizar_Progreso(lblprg, "Se creó la transferencia: " & docNum & " en SAP")

                        If BeDespacho IsNot Nothing Then
                            BeDespacho.No_pase = docNum
                            clsLnTrans_despacho_enc.Actualizar_No_Pase(BeDespacho)
                        End If

                        vTraslado_Creado = True

                    Else
                        clsPublic.Actualizar_Progreso(lblprg, $"❌ Error SL {resp.StatusCode}:")
                        clsPublic.Actualizar_Progreso(lblprg, body)

                    End If

                End Using

            End If

            ' 4) Marcar enviados (si aplica)
            If vTraslado_Creado OrElse BeDespacho.No_Documento_Externo = "" Then

                If Not BePedidoEnc.Bodega_Destino = "" AndAlso BePedidoEnc.Bodega_Destino <> BePedidoEnc.Cliente.Codigo Then

                    vDebeGenerarSolicitud = True

                    Dim vDocNumTraslado As String = BePedidoEnc.No_Documento_Externo
                    Dim vFromWarehouse As String = BePedidoEnc.Cliente.Codigo
                    Dim vToWarehouse As String = BePedidoEnc.Bodega_Destino

                    ' 2) Payload StockTransfer
                    Dim payloadStockTransferRequest = Build_StockTransferRequest_Payload(BePedidoEnc,
                                                                                         vDocNumTraslado,
                                                                                         vFromWarehouse,
                                                                                         vToWarehouse,
                                                                                         transaccionesOut)

                    Dim handlerSol As New HttpClientHandler With {
                        .AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate,
                        .ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True,
                        .UseCookies = False
                    }

                    Using http As New HttpClient(handlerSol) With {.BaseAddress = New Uri(SapServiceLayerClient.baseUrl)}
                        Dim json As String = JsonConvert.SerializeObject(payloadStockTransferRequest, New JsonSerializerSettings With {.NullValueHandling = NullValueHandling.Ignore})
                        Dim content = New StringContent(json, Encoding.UTF8)
                        Dim mediaType = New MediaTypeHeaderValue("application/json")
                        mediaType.CharSet = "utf-8"
                        content.Headers.ContentType = mediaType

                        Dim req As New HttpRequestMessage(HttpMethod.Post, ENTITY_TARGET_STOCK_TRANSFER_REQUEST) With {.Content = content}
                        req.Headers.Add("Cookie", vHanaService.SessionCookie)
                        req.Headers.ConnectionClose = True

                        Dim resp = Await http.SendAsync(req).ConfigureAwait(False)
                        Dim body = Await resp.Content.ReadAsStringAsync().ConfigureAwait(False)

                        ' Parsear el JSON
                        Dim jsonObj As JObject = JObject.Parse(body)

                        Dim docEntryTransferRequest As Integer = 0
                        Dim docNumTransferRequest As Integer = 0

                        If resp.IsSuccessStatusCode Then
                            docEntryTransferRequest = jsonObj("DocEntry")
                            docNumTransferRequest = jsonObj("DocNum")
                        End If

                        If resp.IsSuccessStatusCode Then

                            clsPublic.Actualizar_Progreso(lblprg, "✅ Respuesta:")
                            clsPublic.Actualizar_Progreso(lblprg, "Se creó la Solicitud de transferencia: " & docNumTransferRequest & " en SAP")

                            If BeDespacho IsNot Nothing Then
                                BeDespacho.No_Documento_Externo = docNumTransferRequest
                                clsLnTrans_despacho_enc.Actualizar_No_Documento_Externo(BeDespacho)
                            End If

                            vSolicitud_Creada = True

                        Else
                            clsPublic.Actualizar_Progreso(lblprg, $"❌ Error SL {resp.StatusCode}:")
                            clsPublic.Actualizar_Progreso(lblprg, body)
                        End If

                    End Using

                End If

                If vTraslado_Creado OrElse (vDebeGenerarSolicitud AndAlso vSolicitud_Creada) Then
                    Dim marcados = clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(transaccionesOut)
                    If marcados = 0 Then
                        clsPublic.Actualizar_Progreso(lblprg, "⚠️ Transferencia creada, pero no se marcaron como enviadas en WMS.")
                    End If
                End If

            End If

            Return vTraslado_Creado

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, $"❌ Error al crear la transferencia: {ex.Message}")
            Return False
        End Try
    End Function

    Private Shared Function Build_StockTransfer_Payload(docEntrySolicitud As Integer,
                                                        docNumSolicitud As String,
                                                        Fromwarehouse As String,
                                                        ToWarehouse As String,
                                                        lTransaccionesSalida As List(Of clsBeI_nav_transacciones_out)) As StockTransferDto

        Dim dto As New StockTransferDto With {
        .FromWarehouse = Fromwarehouse,
        .ToWarehouse = ToWarehouse,
        .DocDate = Today,
        .Comments = $"Traslado generado por WMS sobre Solicitud SAP: {docEntrySolicitud} - Ref: {docNumSolicitud}",
        .JournalMemo = $"WMS Transfer from OWTQ {docEntrySolicitud}",
        .StockTransferLines = New List(Of StockTransferLineDto)()
    }

        ' Agrupar por Item + Línea + Talla + Color + Lote para construir lotes correctos
        Dim grupos = lTransaccionesSalida.
        GroupBy(Function(x) New With {
            Key .ItemCode = x.Codigo_producto,
            Key .No_Linea = x.No_linea,
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

        ' Agrupar por Item + Línea para sumar y luego repartir BatchNumbers
        Dim lineas = grupos.
        GroupBy(Function(k) New With {Key k.ItemCode, Key k.No_Linea}).
        Select(Function(g) New With {
            g.Key.ItemCode,
            g.Key.No_Linea,
            .QtyLinea = g.Sum(Function(r) r.Qty),
            .Batches = g.Select(Function(r) r).ToList()
        }).ToList()

        Dim i As Integer = 0
        For Each ln In lineas
            Dim line As New StockTransferLineDto With {
            .BaseType = BASETYPE_INVENTORY_TRANSFER,
            .BaseEntry = docEntrySolicitud,
            .BaseLine = ln.No_Linea,
            .ItemCode = ln.ItemCode,
            .Quantity = Decimal.Round(ln.QtyLinea, 6),
            .FromWarehouseCode = Fromwarehouse,
            .WarehouseCode = ToWarehouse,
            .BatchNumbers = New List(Of BatchNumberDto)()
        }

            ' UDFs de referencia: se llenan con el primer batch por simplicidad
            Dim first = ln.Batches.FirstOrDefault()
            If first IsNot Nothing Then
                line.U_Color = If(first.Color, String.Empty)
                line.U_Talla = If(first.Talla, String.Empty)
            End If

            For Each b In ln.Batches
                line.BatchNumbers.Add(New BatchNumberDto With {
                .BatchNumber = BuildBatchNumber(b.Color, b.Talla),
                .Quantity = Decimal.Round(b.Qty, 6)
            })
            Next

            dto.StockTransferLines.Add(line)
            i += 1
        Next

        Return dto
    End Function
    Private Shared Function Build_StockTransferRequest_Payload(BePedidoEnc As clsBeTrans_pe_enc,
                                                               docNumSolicitud As String,
                                                               FromWarehouse As String,
                                                               ToWarehouse As String,
                                                               lTransaccionesSalida As List(Of clsBeI_nav_transacciones_out)) As StockTransferRequestDto

        Dim vMensaje As String = $"Solicitud Traslado generado por WMS sobre Solicitud SAP: Ref: {docNumSolicitud} IdPedidoEnc:{BePedidoEnc.IdPedidoEnc} Despacho: {BePedidoEnc.No_despacho}"

        Dim dto As New StockTransferRequestDto With {
        .FromWarehouse = FromWarehouse,
        .DocDate = Today,
        .ToWarehouse = ToWarehouse,
        .Comments = vMensaje,
        .JournalMemo = vMensaje,
        .U_ENVIADO_WMS = 2,
        .StockTransferLines = New List(Of StockTransferRequestLineDto)()}

        ' Agrupar por Item + Línea + Talla + Color + Lote para construir lotes correctos
        Dim grupos = lTransaccionesSalida.
        GroupBy(Function(x) New With {
            Key .ItemCode = x.Codigo_producto,
            Key .No_Linea = x.No_linea,
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

        ' Agrupar por Item + Línea para sumar y luego repartir BatchNumbers
        Dim lineas = grupos.
        GroupBy(Function(k) New With {Key k.ItemCode, Key k.No_Linea}).
        Select(Function(g) New With {
            g.Key.ItemCode,
            g.Key.No_Linea,
            .QtyLinea = g.Sum(Function(r) r.Qty),
            .Batches = g.Select(Function(r) r).ToList()
        }).ToList()

        Dim i As Integer = 0
        For Each ln In lineas
            Dim line As New StockTransferRequestLineDto With {
            .ItemCode = ln.ItemCode,
            .Quantity = Decimal.Round(ln.QtyLinea, 6),
            .FromWarehouseCode = Nothing,
            .WarehouseCode = Nothing}

            ' UDFs de referencia: se llenan con el primer batch por simplicidad
            Dim first = ln.Batches.FirstOrDefault()
            If first IsNot Nothing Then
                line.U_Color = If(first.Color, String.Empty)
                line.U_Talla = If(first.Talla, String.Empty)
            End If

            dto.StockTransferLines.Add(line)
            i += 1
        Next

        Return dto
    End Function
    Private Shared Function BuildBatchNumber(color As String, talla As String) As String
        Return $"{color.Trim()}{talla.Trim()}"
    End Function

    <Serializable>
    Private Class StockTransferDto
        Public Property DocDate As Date = Today
        Public Property FromWarehouse As String
        Public Property Comments As String
        Public Property JournalMemo As String
        Public Property StockTransferLines As List(Of StockTransferLineDto)
        Public Property ToWarehouse As String
    End Class
    Private Class StockTransferRequestDto
        Public Property FromWarehouse As String
        Public Property Comments As String
        Public Property JournalMemo As String
        Public Property StockTransferLines As List(Of StockTransferRequestLineDto)
        Public Property ToWarehouse As String
        Public Property DocDate As Date = Today
        Public Property U_ENVIADO_WMS = 2
    End Class

    <Serializable>
    Public Class StockTransferLineDto
        Public Property BaseType As Integer?
        Public Property BaseEntry As Integer?
        Public Property BaseLine As Integer?
        Public Property ItemCode As String
        Public Property Quantity As Decimal
        Public Property FromWarehouseCode As String
        Public Property WarehouseCode As String
        Public Property U_Color As String
        Public Property U_Talla As String
        Public Property BatchNumbers As List(Of BatchNumberDto)
    End Class

    Public Class StockTransferRequestLineDto
        Public Property BaseType As Integer?
        Public Property ItemCode As String
        Public Property Quantity As Decimal
        Public Property FromWarehouseCode As String
        Public Property WarehouseCode As String
        Public Property U_Color As String
        Public Property U_Talla As String
        
    End Class

    Public Class ProductoTransferSAPProrrateo
        Public Property IdPedidoEnc As Integer
        Public Property CodigoProductoSAP As String
        Public Property CodigoProductoWMS As String
        Public Property CantidadBase As Decimal
        Public Property CodigoPresentacion As String
        Public Property No_Pedido As String
        Public Property No_Linea As Integer
        Public Property Factor As Double = 1
    End Class

End Class