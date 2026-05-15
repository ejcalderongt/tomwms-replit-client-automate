Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Runtime.InteropServices.WindowsRuntime
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
                                                                 Optional ByVal pNoDocumento As String = "",
                                                                 Optional ByVal pEsProrrateo As Boolean = True,
                                                                 Optional ByVal pEsTrasladoBodegaVirtual As Boolean = False) As Task(Of Boolean)

        Dim clsTrans As New clsTransaccion
        Dim sw As New Stopwatch()
        Dim primeraParteConfirmada As Boolean = False

        Try
            sw.Start()

            clsPublic.Actualizar_Progreso(lblprg, "Iniciando proceso de importación de solicitudes de traslado desde SAP.")

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
                Throw New Exception("ERROR_202311271751: No se pudo obtener la bodega asociada a la configuración de interface: " & BeConfigEnc.Idbodega)
            End If

            If pEsProrrateo Then
                If BeBodega.Codigo <> BeConfigEnc.Bodega_Prorrateo Then
                    clsPublic.Actualizar_Progreso(lblprg, $"La bodega de origen y la de prorrateo no coinciden ({BeBodega.Codigo} <> {BeConfigEnc.Bodega_Prorrateo}), no se puede importar el documento.")

                    clsTrans.RollBack_Transaction()
                    clsTrans.Close_Conection()

                    sw.Stop()
                    clsPublic.Actualizar_Progreso(lblprg, $"Fin del proceso. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.")

                    Return False
                End If
            End If

            clsTrans.Commit_Transaction()
            primeraParteConfirmada = True

            clsTrans.Close_Conection()

            Dim ok As Boolean = Await Procesar_Documentos(BeBodega.Codigo,
                                                      pNoDocumento,
                                                      BeConfigEnc,
                                                      lblprg,
                                                      pEsProrrateo,
                                                      pEsTrasladoBodegaVirtual)

            sw.Stop()

            clsPublic.Actualizar_Progreso(lblprg, $"Fin del proceso de sincronización. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.")

            Return ok

        Catch ex As Exception
            sw.Stop()

            If Not primeraParteConfirmada Then
                Try
                    clsTrans.RollBack_Transaction()
                Catch
                End Try
            End If

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, pNoDocumento, 1900, 900)
            clsPublic.Actualizar_Progreso(lblprg, $"Error en el proceso. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.")

            Throw

        Finally
            Try
                clsTrans.Close_Conection()
            Catch
            End Try
        End Try

    End Function

    Private Shared Async Function Procesar_Documentos(ByVal codigoBodega As String,
                                                      ByVal pNoDocumento As String,
                                                      ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                      ByVal lblprg As RichTextBox,
                                                      Optional ByVal pEsProrrateo As Boolean = True,
                                                      Optional ByVal pEsTrasladoBodegaVirtual As Boolean = False) As Task(Of Boolean)

        Try
            clsPublic.Actualizar_Progreso(lblprg, "Conectando a SAP.")

            ' Usa una conexión/transacción solo para leer la lista si tu método la necesita.
            Dim clsTransConsulta As New clsTransaccion
            clsTransConsulta.Begin_Transaction()

            Dim solicitudes As List(Of clsBeI_nav_ped_traslado_enc) =
            Get_Traslados_SAP_Prorrateo_SL(codigoBodega,
                                           clsTransConsulta.lConnection,
                                           clsTransConsulta.lTransaction,
                                           lblprg,
                                           pNoDocumento)

            clsTransConsulta.Commit_Transaction()

            If solicitudes Is Nothing OrElse solicitudes.Count = 0 Then
                clsPublic.Actualizar_Progreso(lblprg, "No hay documentos para importar.")
                Return False
            End If

            Dim alMenosUnoProcesado As Boolean = False

            For Each solicitud In solicitudes

                Dim clsTransDoc As New clsTransaccion

                Try
                    clsTransDoc.Begin_Transaction()

                    Dim ok As Boolean = Await Procesar_Documento_Individual(
                    solicitud,
                    BeConfigEnc,
                    lblprg,
                    clsTransDoc,
                    pEsProrrateo,
                    pEsTrasladoBodegaVirtual
                )

                    If ok Then
                        clsTransDoc.Commit_Transaction()
                        alMenosUnoProcesado = True
                        clsPublic.Actualizar_Progreso(lblprg, $"Documento {solicitud.No} confirmado.")
                    Else
                        clsTransDoc.RollBack_Transaction()
                        clsPublic.Actualizar_Progreso(lblprg, $"Documento {solicitud.No} revertido.")
                    End If

                Catch ex As Exception
                    clsTransDoc.RollBack_Transaction()
                    clsPublic.Actualizar_Progreso(lblprg, $"Error en documento {solicitud.No}: {ex.Message}")
                End Try

            Next

            Return alMenosUnoProcesado

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, ex.Message)
            Return False
        End Try

    End Function

    Private Shared Async Function Procesar_Documento_Individual(ByVal solicitud As clsBeI_nav_ped_traslado_enc,
                                                            ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                            ByVal lblprg As RichTextBox,
                                                            ByVal clsTrans As clsTransaccion,
                                                            Optional ByVal pEsProrrateo As Boolean = True,
                                                            Optional ByVal pEsTrasladoBodegaVirtual As Boolean = False) As Task(Of Boolean)

        If pEsProrrateo Then
            If Not solicitud.Transfer_from_Code = BeConfigEnc.Bodega_Prorrateo Then
                clsPublic.Actualizar_Progreso(lblprg, $"La bodega de origen {solicitud.Transfer_from_Code} no coincide con la bodega de prorrateo {BeConfigEnc.Bodega_Prorrateo}, se omite el documento {solicitud.No}.")
                Return False
            End If
        End If

        clsPublic.Actualizar_Progreso(lblprg, $"Procesando solicitud de traslado SAP (OWTQ): {solicitud.Receipt_Document_Reference}/{solicitud.No}{vbNewLine}")

        If Not Await Validar_Cliente_WMS(solicitud.Transfer_to_Code, "C", lblprg, clsTrans, vHanaService.SessionCookie, BD.Instancia.HANA_SL) Then
            Return False
        End If

        Dim origenEsWMS As Boolean = clsLnBodega.Exists_By_Codigo(solicitud.Transfer_from_Code, clsTrans.lConnection, clsTrans.lTransaction)
        Dim destinoEsWMS As Boolean = clsLnBodega.Exists_By_Codigo(solicitud.Transfer_to_CodeField, clsTrans.lConnection, clsTrans.lTransaction)
        Dim debeProcesar As Boolean = Not destinoEsWMS OrElse Not origenEsWMS OrElse (origenEsWMS AndAlso destinoEsWMS)

        If Not debeProcesar Then
            Return False
        End If

        If solicitud.Transfer_to_CodeField.Trim <> "" Then

            If Not clsLnCliente.Existe_Cliente_By_Codigo(solicitud.Transfer_to_CodeField, clsTrans.lConnection, clsTrans.lTransaction) Then
                clsPublic.Actualizar_Progreso(lblprg, $"La bodega destino {solicitud.Transfer_to_CodeField} no existe como cliente, se omite el documento {solicitud.Receipt_Document_Reference}/{solicitud.No}.")
                Return False
            End If

            If clsLnCliente.Get_IdUbicacionVirtual_By_Codigo(solicitud.Transfer_to_CodeField, clsTrans.lConnection, clsTrans.lTransaction) = 0 Then
                clsPublic.Actualizar_Progreso(lblprg, $"La bodega destino {solicitud.Transfer_to_CodeField} no tiene ubicación virtual definida, se omite el documento {solicitud.Receipt_Document_Reference}/{solicitud.No}.")
                Return False
            End If

            If clsLnCliente.Get_IdUbicacionVirtual_By_Codigo(solicitud.Transfer_to_CodeField, clsTrans.lConnection, clsTrans.lTransaction) <> solicitud.Transfer_to_CodeField AndAlso
           solicitud.Transfer_to_Code = solicitud.Transfer_to_CodeField Then
                clsPublic.Actualizar_Progreso(lblprg, $"La ubicación virtual no tiene el mismo código de la bodega destino {solicitud.Transfer_to_CodeField}, se omite el documento {solicitud.Receipt_Document_Reference}/{solicitud.No}.")
                Return False
            End If

        End If

        Dim pedidoEnc As clsBeTrans_pe_enc =
        clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia_If(
            solicitud,
            lblprg,
            clsTrans.lConnection,
            clsTrans.lTransaction
        )

        If pedidoEnc Is Nothing Then
            Return False
        End If

        Dim trasladoSincronizado As Boolean =
        Await Marcar_Traslado_Sincronizado_SLAsync(
            solicitud.No,
            vHanaService.SessionCookie,
            BD.Instancia.HANA_SL,
            1
        )

        If Not trasladoSincronizado Then
            Return False
        End If

        Return True

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
                clsPublic.Actualizar_Progreso(lblprg, $"No se pudo insertar el cliente {codigoSocioNegocio} en WMS, es posible que sea una bodega y sea necesario crearla manualmente")
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

            If jsonCliente IsNot Nothing AndAlso jsonCliente("value") IsNot Nothing AndAlso jsonCliente("value").HasValues Then
                Dim cliente As clsBeCliente = ConstruirClienteDesdeServiceLayer(jsonCliente, lConnection, lTransaction)

                clsLnCliente.Insertar(cliente, lConnection, lTransaction)
                InsertarClienteEnBodegas(cliente, lConnection, lTransaction)
                Await MarcarClienteComoEnviadoSAPAsync(sessionCookie, baseUrl, pCodigo)

                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw New Exception("No se pudo insertar el cliente nuevo proveniente de SAP: " & ex.Message)
        End Try

    End Function

    Public Shared Async Function Marcar_Traslado_Sincronizado_SLAsync(docEntry As String,
                                                                      sessionCookie As String,
                                                                      baseUrl As String,
                                                                      enviado As Integer) As Task(Of Boolean)

        Try

            If String.IsNullOrWhiteSpace(docEntry) Then Return False

            Dim requestUrl As String = $"InventoryTransferRequests({docEntry})"
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
                            Throw New Exception($"Error al actualizar OWTQ. Código: {response.StatusCode}, Detalle: {errContent}")
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception($"(SL) {MethodBase.GetCurrentMethod().Name} {ex.Message}", ex)
        End Try

    End Function

    Private Shared Function Get_Traslados_SAP_Prorrateo_SL(pCodigoBodegaInterface As String,
                                                           lConnection As SqlConnection,
                                                           lTransaction As SqlTransaction,
                                                           lblprg As RichTextBox,
                                                           Optional pNoDocumentoSAP As String = "") As List(Of clsBeI_nav_ped_traslado_enc)

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePropietario As clsBePropietarios = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario, lConnection, lTransaction)
        Dim BeBodega As clsBeBodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega, lConnection, lTransaction)
        Dim vEsTransferenciaDirecta As Boolean = False

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
            filtroEnviado = ""
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

                    If BeConfigEnc.Bodega_Prorrateo = BeBodega.Codigo Then
                        vEsTransferenciaDirecta = True
                    End If

                    For Each traslado In parsed("value")

                        Dim U_Transito = traslado("U_Transito").Value(Of String)
                        Dim Bodega_Destino = traslado("ToWarehouse").Value(Of String)

                        If Bodega_Destino IsNot Nothing AndAlso
                            Bodega_Destino.Length > 0 AndAlso
                            Bodega_Destino.Contains("-") Then
                            Dim pBodDestino As String() = Bodega_Destino.Split("-")
                            Bodega_Destino = pBodDestino.GetValue(0)
                        End If

                        Dim bePedido As New clsBeI_nav_ped_traslado_enc With {
                        .No = traslado("DocEntry").Value(Of Integer),
                        .Posting_Date = traslado("DocDate").Value(Of Date),
                        .Receipt_Date = traslado("DocDate").Value(Of Date),
                        .Shipment_Date = traslado("DocDate").Value(Of Date),
                        .Status = 1,
                        .Transfer_from_Code = traslado("FromWarehouse")?.ToString(),
                        .Transfer_from_Contact = traslado("JournalMemo")?.ToString(),
                        .Transfer_to_Contact = traslado("CardName")?.ToString(),
                        .Transfer_to_CodeField = Bodega_Destino,  'Transfer_to_CodeField
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
                            .No = clsLnI_nav_ped_traslado_det.MaxID() + 1,
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

    Public Shared Async Function Enviar_Traslados_Salida_Desde_Solicitud_Prorrateo(ByVal lblprg As RichTextBox,
                                                                                    ByVal prg As ProgressBar,
                                                                                    ByVal pTipo As tTipoDocumentoSalida) As Task(Of Boolean)

        Dim envioExitosoCount As Integer = 0
        Dim huboError As Boolean = False
        Dim totalPedidos As Integer = 0

        Dim clsTrans As New clsTransaccion()
        Dim sw As New Stopwatch()

        Using CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
            Try
                ' Inicia medición de tiempo
                sw.Start()

                clsPublic.Actualizar_Progreso(lblprg, "Iniciando proceso de envío de traslados desde solicitud de prorrateo SAP...")

                CnnLog.Open()
                clsTrans.Begin_Transaction()

                Dim lTransaccionesSalida As List(Of clsBeI_nav_transacciones_out) =
                clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(pTipo,
                                                                               clsTrans.lConnection,
                                                                               clsTrans.lTransaction)

                If lTransaccionesSalida Is Nothing OrElse lTransaccionesSalida.Count = 0 Then
                    sw.Stop()
                    clsPublic.Actualizar_Progreso(lblprg, $"MSG_240117: No hay transacciones para enviar. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.")
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
                    Dim BePedidoEnc As clsBeTrans_pe_enc =
                    clsLnTrans_pe_enc.GetSingle(PT.Idpedidoenc, clsTrans.lConnection, clsTrans.lTransaction)

                    clsPublic.Actualizar_Progreso(lblprg, $"Procesando Solicitud de traslado: {PT.Idpedidoenc}-{BePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino}")

                    Dim yaEnviado As Boolean =
                    clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP(PT.No_pedido, clsTrans.lConnection, clsTrans.lTransaction)

                    If yaEnviado Then
                        clsPublic.Actualizar_Progreso(lblprg, "El pedido ya está marcado como enviado a ERP; se omite su reenvío.")
                        Continue For
                    End If

                    Dim lTransaccionesSalidaSingle As List(Of clsBeI_nav_transacciones_out) =
                    lTransaccionesSalida.FindAll(Function(x) x.No_pedido = PT.No_pedido AndAlso x.Idpedidoenc = PT.Idpedidoenc)

                    Dim enviadoOk As Boolean = Await Enviar_Traslado_Cedis_Desde_Solicitud_SAP(PT.No_pedido,
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

                ' Detiene el cronómetro
                sw.Stop()

                Dim resultado As Boolean = (envioExitosoCount > 0 AndAlso Not huboError)
                Dim mensajeFinal As String =
                If(resultado,
                   $"Proceso finalizado correctamente. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.",
                   $"Proceso finalizado con errores. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.")

                clsPublic.Actualizar_Progreso(lblprg, mensajeFinal)

                Return resultado

            Catch ex As Exception
                sw.Stop()
                clsTrans.RollBack_Transaction()
                clsPublic.Actualizar_Progreso(lblprg, $"Error en el proceso de envío. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos. Error: {ex.Message}")
                Return False

            Finally
                prg.Value = 0
                prg.Visible = False
                If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
                clsTrans.Close_Conection()
            End Try
        End Using
    End Function

    Public Shared Async Function Enviar_Traslados_Salida_Desde_Solicitud_Tienda(ByVal lblprg As RichTextBox,
                                                                                ByVal prg As ProgressBar,
                                                                                ByVal pTipo As tTipoDocumentoSalida) As Task(Of Boolean)

        Dim envioExitosoCount As Integer = 0
        Dim huboError As Boolean = False
        Dim totalPedidos As Integer = 0

        Dim clsTrans As New clsTransaccion()
        Dim sw As New Stopwatch()

        Using CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)

            Try

                sw.Start()

                clsPublic.Actualizar_Progreso(lblprg, "Iniciando proceso de envío de traslados desde solicitud de prorrateo SAP...")

                CnnLog.Open()
                clsTrans.Begin_Transaction()

                Dim lTransaccionesSalida As List(Of clsBeI_nav_transacciones_out) = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(pTipo,
                                                                                                                                                   clsTrans.lConnection,
                                                                                                                                                   clsTrans.lTransaction)

                If lTransaccionesSalida Is Nothing OrElse lTransaccionesSalida.Count = 0 Then
                    sw.Stop()
                    clsPublic.Actualizar_Progreso(lblprg, $"MSG_240117: No hay transacciones para enviar. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.")
                    clsTrans.Commit_Transaction()
                    Return False
                End If

                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc}).ToList()

                totalPedidos = ListaPedidosTransf.Count

                clsPublic.Actualizar_Progreso(lblprg, $"Documentos a enviar: {totalPedidos}")

                For Each PT In ListaPedidosTransf

                    Dim BePedidoEnc As clsBeTrans_pe_enc = clsLnTrans_pe_enc.GetSingle(PT.Idpedidoenc, clsTrans.lConnection, clsTrans.lTransaction)

                    clsPublic.Actualizar_Progreso(lblprg, $"Procesando Solicitud de traslado: {PT.Idpedidoenc}-{BePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino}")

                    Dim yaEnviado As Boolean = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP(PT.No_pedido, clsTrans.lConnection, clsTrans.lTransaction)

                    If yaEnviado Then
                        clsPublic.Actualizar_Progreso(lblprg, "El pedido ya está marcado como enviado a ERP; se omite su reenvío.")
                        Continue For
                    End If

                    Dim lTransaccionesSalidaSingle As List(Of clsBeI_nav_transacciones_out) = lTransaccionesSalida.FindAll(Function(x) x.No_pedido = PT.No_pedido _
                                                                                                                           AndAlso x.Idpedidoenc = PT.Idpedidoenc)

                    Dim enviadoOk As Boolean = Await Enviar_Traslado_Salida_Tienda_Desde_Solicitud_SAP(PT.No_pedido,
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

                ' Detiene el cronómetro
                sw.Stop()

                Dim resultado As Boolean = (envioExitosoCount > 0 AndAlso Not huboError)
                Dim mensajeFinal As String =
                If(resultado,
                   $"Proceso finalizado correctamente. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.",
                   $"Proceso finalizado con errores. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.")

                clsPublic.Actualizar_Progreso(lblprg, mensajeFinal)

                Return resultado

            Catch ex As Exception
                sw.Stop()
                clsTrans.RollBack_Transaction()
                clsPublic.Actualizar_Progreso(lblprg, $"Error en el proceso de envío. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos. Error: {ex.Message}")
                Return False

            Finally
                prg.Value = 0
                prg.Visible = False
                If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
                clsTrans.Close_Conection()
            End Try
        End Using

    End Function

    Public Shared Async Function Enviar_Traslados_Ingreso_Desde_Solicitud_Tiendas(ByVal lblprg As RichTextBox,
                                                                      ByVal prg As ProgressBar,
                                                                      ByVal pTipo As tTipoDocumentoIngreso,
                                                                      ByVal BeConfigEnc As clsBeI_nav_config_enc) As Task(Of Boolean)

        Dim envioExitosoCount As Integer = 0
        Dim huboError As Boolean = False
        Dim totalPedidos As Integer = 0

        Dim clsTrans As New clsTransaccion()
        Dim sw As New Stopwatch()

        Using CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
            Try
                ' Inicia medición de tiempo
                sw.Start()

                clsPublic.Actualizar_Progreso(lblprg, "Iniciando proceso de envío de traslados desde solicitudes de tiendas...")

                CnnLog.Open()
                clsTrans.Begin_Transaction()

                Dim lTransaccionesIngreso As List(Of clsBeI_nav_transacciones_out) =
                clsLnI_nav_transacciones_out.Get_Lotes_Ingreso_Pendientes_Envio_By_Tipo(pTipo,
                                                                                        clsTrans.lConnection,
                                                                                        clsTrans.lTransaction,
                                                                                        BeConfigEnc.Idbodega)

                If lTransaccionesIngreso Is Nothing OrElse lTransaccionesIngreso.Count = 0 Then
                    sw.Stop()
                    clsPublic.Actualizar_Progreso(lblprg, $"MSG_240117: No hay transacciones para enviar. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.")
                    clsTrans.Commit_Transaction()
                    Return False
                End If

                Dim ListaPedidosTransf =
                (From i In lTransaccionesIngreso
                 Group i By Keys = New With {Key i.No_pedido, Key i.Idordencompra} Into Group
                 Select New With {Key Keys.No_pedido, Key Keys.Idordencompra}).ToList()

                totalPedidos = ListaPedidosTransf.Count
                clsPublic.Actualizar_Progreso(lblprg, $"Documentos a enviar: {totalPedidos}")

                For Each PT In ListaPedidosTransf

                    Dim BeTransOcEnc As clsBeTrans_oc_enc =
                    clsLnTrans_oc_enc.GetSingle(PT.Idordencompra, clsTrans.lConnection, clsTrans.lTransaction)

                    If BeTransOcEnc Is Nothing Then
                        clsPublic.Actualizar_Progreso(lblprg, $"No se encontró el documento base con IdOrdencompraEnc: {PT.Idordencompra}")
                        Continue For
                    End If

                    clsPublic.Actualizar_Progreso(lblprg, $"Procesando Traslado: {PT.Idordencompra}-{BeTransOcEnc.Referencia}")

                    Dim yaEnviado As Boolean = BeTransOcEnc.Enviado_A_ERP

                    If yaEnviado Then
                        clsPublic.Actualizar_Progreso(lblprg, "El pedido ya está marcado como enviado a ERP; se omite su reenvío.")
                        Continue For
                    End If

                    Dim lTransaccionesSalidaSingle As List(Of clsBeI_nav_transacciones_out) =
                    lTransaccionesIngreso.FindAll(Function(x) x.No_pedido = PT.No_pedido AndAlso x.Idordencompra = PT.Idordencompra)

                    Dim enviadoOk As Boolean = Await Enviar_Traslado_Ingreso_Desde_Solicitud_SAP_Tiendas(PT.No_pedido,
                                                                                                         BeTransOcEnc,
                                                                                                         lTransaccionesSalidaSingle,
                                                                                                         clsTrans,
                                                                                                         lblprg).ConfigureAwait(False)

                    If enviadoOk Then
                        Try
                            clsPublic.Actualizar_Progreso(lblprg, "Transacciones de salida enviadas correctamente.")
                            clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idordencompra, True, BeConfigEnc.IdUsuario, clsTrans.lConnection, clsTrans.lTransaction)
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
                        'Throw New Exception(vMsgErr)
                    End If
                Next

                clsTrans.Commit_Transaction()

                ' Detiene el cronómetro
                sw.Stop()

                Dim resultado As Boolean = (envioExitosoCount > 0 AndAlso Not huboError)
                Dim mensajeFinal As String =
                If(resultado,
                   $"Proceso finalizado correctamente. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.",
                   $"Proceso finalizado con errores. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos.")

                clsPublic.Actualizar_Progreso(lblprg, mensajeFinal)

                Return resultado

            Catch ex As Exception
                sw.Stop()
                clsTrans.RollBack_Transaction()
                clsPublic.Actualizar_Progreso(lblprg, $"Error en el proceso de envío. Tiempo transcurrido: {sw.Elapsed.TotalSeconds:F2} segundos. Error: {ex.Message}")
                Return False

            Finally
                prg.Value = 0
                prg.Visible = False
                If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
                clsTrans.Close_Conection()
            End Try
        End Using
    End Function

    Private Shared Async Function Enviar_Traslado_Cedis_Desde_Solicitud_SAP(ByVal _DocEntry As Integer,
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
                Dim payloadStockTransfer = Build_StockTransfer_Payload_Cedis(BePedidoEnc,
                                                                             clsTrans,
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

                            Dim docEntrySolicitud As Integer = BePedidoEnc.Referencia
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
                            BePedidoRef.Observacion = $"Traslado generado por WMS sobre Solicitud SAP: {docEntrySolicitud} - Ref: {docNumSolicitud} - IdDocumentoWMS: {BePedidoEnc.IdPedidoEnc}"
                            clsLnTrans_pe_ref_mi3.Insertar(BePedidoRef, clsTrans.lConnection, clsTrans.lTransaction)

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
                                clsLnTrans_despacho_enc.Actualizar_No_Documento_Externo(BeDespacho, clsTrans.lConnection, clsTrans.lTransaction)

                                BePedidoRef.Docnumentrega = docNumTransferRequest
                                BePedidoRef.Docentryentrega = docEntryTransferRequest
                                clsLnTrans_pe_ref_mi3.Actualizar(BePedidoRef, clsTrans.lConnection, clsTrans.lTransaction)

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

    Private Shared Async Function Enviar_Traslado_Salida_Tienda_Desde_Solicitud_SAP(ByVal _DocEntry As Integer,
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
                Dim payloadStockTransfer = Build_StockTransfer_Payload_Cedis(BePedidoEnc,
                                                                             clsTrans,
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
                            '#EJC20251008: No utilice transacción porque en service layer ya se creó el documento.
                            'Si llegaran a haber interbloqueos debería considerarse agregar.
                            clsLnTrans_despacho_enc.Actualizar_No_Pase(BeDespacho)
                            Dim docEntrySolicitud As Integer = BePedidoEnc.Referencia
                            Dim docNumSolicitud As String = BePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino
                            Dim Fromwarehouse As String = BePedidoEnc.Bodega_Origen
                            Dim ToWarehouse As String = BePedidoEnc.Bodega_Destino

                            BePedidoRef.Idpedidoencrefmi3 = clsLnTrans_pe_ref_mi3.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                            BePedidoRef.Idpedidoenc = BePedidoEnc.IdPedidoEnc
                            BePedidoRef.Iddespachoenc = BeDespacho.IdDespachoEnc
                            BePedidoRef.Docnumtraslado = docEntry
                            BePedidoRef.Docentrytraslado = docNum
                            BePedidoRef.Fec_agr = Now
                            BePedidoRef.Usr_agr = BeConfigEnc.IdUsuario
                            BePedidoRef.Codigo_bodega_origen = Fromwarehouse
                            BePedidoRef.Codigo_bodega_destino = ToWarehouse
                            BePedidoRef.Referencia_documento_origen = docNumSolicitud
                            BePedidoRef.Referencia_documento_destino = docEntry 'Este es el documento que llega a esa bodega X.
                            BePedidoRef.Observacion = $"Traslado generado por WMS sobre Solicitud SAP: {docEntrySolicitud} - Ref: {docNumSolicitud} - IdDocumentoWMS: {BePedidoEnc.IdPedidoEnc}"
                            clsLnTrans_pe_ref_mi3.Insertar(BePedidoRef, clsTrans.lConnection, clsTrans.lTransaction)

                        End If

                        vTraslado_Creado = True

                    Else
                        vTraslado_Creado = False
                        clsPublic.Actualizar_Progreso(lblprg, $"❌ Error SL {resp.StatusCode}:")
                        clsPublic.Actualizar_Progreso(lblprg, body)

                    End If

                End Using

            End If

            ' 4) Marcar enviados (si aplica)
            If vTraslado_Creado AndAlso BeDespacho.No_Documento_Externo = "" Then

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
                            clsPublic.Actualizar_Progreso(lblprg, "Se creó la Solicitud de transferencia (bodega final): " & docNumTransferRequest & " en SAP")

                            If BeDespacho IsNot Nothing Then
                                BeDespacho.No_Documento_Externo = docNumTransferRequest
                                clsLnTrans_despacho_enc.Actualizar_No_Documento_Externo(BeDespacho, clsTrans.lConnection, clsTrans.lTransaction)
                                BePedidoRef.Docnumentrega = docNumTransferRequest
                                BePedidoRef.Docentryentrega = docEntryTransferRequest
                                clsLnTrans_pe_ref_mi3.Actualizar(BePedidoRef, clsTrans.lConnection, clsTrans.lTransaction)
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

    Private Shared Async Function Enviar_Traslado_Ingreso_Desde_Solicitud_SAP_Tiendas(ByVal _DocEntry As Integer,
                                                                                      ByVal BeTransOcEnc As clsBeTrans_oc_enc,
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

            Dim vIdOrdenCompraEnc = transaccionesOut.FirstOrDefault.Idordencompra
            Dim vIdRecepcionEnc = transaccionesOut.FirstOrDefault.Idrecepcionenc
            Dim BeTransReOC As clsBeTrans_re_oc = clsLnTrans_re_oc.GetSingle(vIdRecepcionEnc, vIdOrdenCompraEnc, clsTrans.lConnection, clsTrans.lTransaction)

            ' 1) POST /StockTransfers
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
            ServicePointManager.Expect100Continue = False
            ServicePointManager.FindServicePoint(New Uri(SapServiceLayerClient.baseUrl)).ConnectionLeaseTimeout = 0

            If BeTransReOC.No_Erp_Docnum_Entrega = "" Then

                ' 2) Payload StockTransfer
                Dim payloadStockTransfer = Build_StockTransfer_Payload_Tiendas(BeTransOcEnc,
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

                        If BeTransReOC IsNot Nothing Then
                            BeTransReOC.No_Erp_Docnum_Entrega = docNum
                            BeTransReOC.No_Erp_Docentry_Entrega = docEntry
                            clsLnTrans_re_oc.Actualizar_No_Entrega_ERP(BeTransReOC)
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

    Private Shared Function Build_StockTransfer_Payload_Tiendas(BeTransOcEnc As clsBeTrans_oc_enc,
                                                                lTransaccionesSalida As List(Of clsBeI_nav_transacciones_out)) As StockTransferDto


        Dim docEntrySolicitud As Integer = BeTransOcEnc.Referencia
        Dim docNumSolicitud As String = BeTransOcEnc.No_Documento
        Dim Fromwarehouse As String = BeTransOcEnc.ProveedorBodega.Proveedor.Codigo
        Dim ToWarehouse As String = clsLnBodega.Get_Codigo_By_IdBodega(BeTransOcEnc.IdBodega)

        Dim BeUsuario = clsLnUsuario.GetSingle(BeTransOcEnc.User_Agr)
        Dim vUsuarioWMS As String = ""

        If Not BeUsuario Is Nothing Then vUsuarioWMS = BeUsuario.Nombres + " " + BeUsuario.Apellidos

        Dim IdRecepcionEnc As Integer = clsLnTrans_re_oc.Get_IdRecepcionEnc_By_IdOrdenCompraEnc(BeTransOcEnc.IdOrdenCompraEnc).FirstOrDefault
        Dim vIdOperadorDefecto = clsLnTrans_re_det.Get_IdOperadorDefecto_By_IdRecepcionEnc(IdRecepcionEnc)

        Dim vOperadorWMS As Integer = 0

        Dim dto As New StockTransferDto With {
        .FromWarehouse = Fromwarehouse,
        .ToWarehouse = ToWarehouse,
        .DocDate = Today,
        .Comments = $"Traslado generado por WMS sobre Solicitud SAP: {docEntrySolicitud} - Ref: {docNumSolicitud} - IdDocumentoWMS: {lTransaccionesSalida.FirstOrDefault.Idordencompra}",
        .JournalMemo = $"WMS Transfer from OWTQ {docNumSolicitud}",
        .U_ENVIADO_WMS = 2,
        .U_DOCUMENTO_WMS = BeTransOcEnc.IdOrdenCompraEnc,
        .U_INICIO_PICK = BeTransOcEnc.Hora_Inicio_Recepcion.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        .U_FIN_PICK = BeTransOcEnc.Hora_Fin_Recepcion.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        .U_INICIO_ENVIO = Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        .U_FIN_ENVIO = Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        .U_ENVIADO_SAP_WMS = FormatoFechas.tFechaHoraSAP(Now),
        .U_Tipo = 1,
        .U_USR_PICK = vIdOperadorDefecto,
        .StockTransferLines = New List(Of StockTransferLineDto)()}

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

    Private Shared Function Build_StockTransfer_Payload_Cedis(BePedidoEnc As clsBeTrans_pe_enc,
                                                              clsTrans As clsTransaccion,
                                                              lTransaccionesSalida As List(Of clsBeI_nav_transacciones_out)) As StockTransferDto


        Dim docEntrySolicitud As Integer = BePedidoEnc.Referencia
        Dim docNumSolicitud As String = BePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino
        Dim Fromwarehouse As String = BePedidoEnc.Bodega_Origen
        Dim ToWarehouse As String = BePedidoEnc.Bodega_Destino

        If Not BePedidoEnc.Bodega_Destino = "" AndAlso BePedidoEnc.Bodega_Destino <> BePedidoEnc.Cliente.Codigo Then
            ToWarehouse = BePedidoEnc.Cliente.Codigo
        End If

        Dim vOperadorPickingDefecto As String = clsLnTrans_picking_ubic.Get_Operador_Defecto_By_IdPickingEnc(BePedidoEnc.Picking.IdPickingEnc, clsTrans.lConnection, clsTrans.lTransaction)

        Dim dto As New StockTransferDto With {
        .FromWarehouse = Fromwarehouse,
        .ToWarehouse = ToWarehouse,
        .DocDate = Today,
        .Comments = $"Traslado generado por WMS sobre Solicitud SAP: {docEntrySolicitud} - Ref: {docNumSolicitud} - IdDocumentoWMS: {BePedidoEnc.IdPedidoEnc}",
        .JournalMemo = $"WMS Transfer from OWTQ {docNumSolicitud}",
        .U_USR_PICK = vOperadorPickingDefecto,
        .U_ENVIADO_WMS = 2,
        .U_DOCUMENTO_WMS = BePedidoEnc.IdPedidoEnc,
        .U_INICIO_PICK = BePedidoEnc.Picking.Hora_ini.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        .U_FIN_PICK = BePedidoEnc.Picking.Hora_fin.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        .U_INICIO_ENVIO = Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        .U_FIN_ENVIO = Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        .U_ENVIADO_SAP_WMS = FormatoFechas.tFechaHoraSAP(Now),
        .U_Tipo = 1,
        .StockTransferLines = New List(Of StockTransferLineDto)()}

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

        Dim vMensaje As String = $"IdPedidoEncWMS:{BePedidoEnc.IdPedidoEnc} Despacho: {BePedidoEnc.No_despacho}"


        Dim dto As New StockTransferRequestDto With {
        .FromWarehouse = FromWarehouse,
        .DocDate = Today,
        .ToWarehouse = ToWarehouse,
        .Comments = vMensaje,
        .JournalMemo = vMensaje,
        .U_ENVIADO_WMS = 2,
        .U_DOCUMENTO_WMS = BePedidoEnc.IdPedidoEnc,
        .U_INICIO_PICK = BePedidoEnc.Picking.Hora_ini.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        .U_FIN_PICK = BePedidoEnc.Picking.Hora_fin.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        .U_INICIO_ENVIO = Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        .U_FIN_ENVIO = Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        .U_ENVIADO_SAP_WMS = FormatoFechas.tFechaHoraSAP(Now),
        .U_Tipo = 1,
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
    <JsonObject(MemberSerialization:=MemberSerialization.OptOut)>
    Public Class StockTransferDto

        <JsonProperty("DocDate", Order:=1)>
        Public Property DocDate As Date = Today

        <JsonProperty("FromWarehouse", Order:=2)>
        Public Property FromWarehouse As String = ""

        <JsonProperty("ToWarehouse", Order:=3)>
        Public Property ToWarehouse As String = ""

        <JsonProperty("Comments", Order:=4)>
        Public Property Comments As String = ""

        <JsonProperty("JournalMemo", Order:=5)>
        Public Property JournalMemo As String = ""
        <JsonProperty("U_USR_PICK", Order:=6)>
        Public Property U_USR_PICK As String = ""
        <JsonProperty("U_DOCUMENTO_WMS", Order:=7)>
        Public Property U_DOCUMENTO_WMS As Integer = 0
        <JsonProperty("U_INICIO_PICK", Order:=8)>
        Public Property U_INICIO_PICK As DateTime = Now
        <JsonProperty("U_FIN_PICK", Order:=9)>
        Public Property U_FIN_PICK As DateTime = Now
        <JsonProperty("U_ESTADO_PEDIDO", Order:=10)>
        Public Property U_ESTADO_PEDIDO As Integer = 0
        <JsonProperty("U_INICIO_ENVIO", Order:=11)>
        Public Property U_INICIO_ENVIO As DateTime = Now

        <JsonProperty("U_FIN_ENVIO", Order:=12)>
        Public Property U_FIN_ENVIO As DateTime = Now

        <JsonProperty("U_Tipo", Order:=13)>
        Public Property U_Tipo As String = "" '1Manual, 2Resurtido Auto, 3Pedido Inicial

        <JsonProperty("U_ENVIADO_WMS", Order:=14)>
        Public Property U_ENVIADO_WMS As Integer = 1

        <JsonProperty("U_ENVIADO_SAP_WMS", Order:=15)>
        Public Property U_ENVIADO_SAP_WMS As String = ""

        <JsonProperty("StockTransferLines", Order:=16)>
        Public Property StockTransferLines As List(Of StockTransferLineDto)

    End Class
    Private Class StockTransferRequestDto
        Public Property FromWarehouse As String
        Public Property Comments As String
        Public Property JournalMemo As String
        Public Property ToWarehouse As String
        Public Property DocDate As Date = Today
        Public Property U_ENVIADO_WMS = 2
        Public Property U_USR_PICK As String = ""
        Public Property U_DOCUMENTO_WMS As Integer = 0
        Public Property U_INICIO_PICK As Date = Now
        Public Property U_FIN_PICK As Date = Now
        Public Property U_ESTADO_PEDIDO As Integer = 0
        Public Property U_INICIO_ENVIO As Date = Now
        Public Property U_FIN_ENVIO As Date = Now
        Public Property U_Tipo As String = "" '1Manual, 2Resurtido Auto, 3Pedido Inicial
        Public Property U_ENVIADO_SAP_WMS As String = ""
        Public Property StockTransferLines As List(Of StockTransferRequestLineDto)
    End Class

    <JsonObject(MemberSerialization:=MemberSerialization.OptOut)>
    Public Class StockTransferLineDto
        <JsonProperty("BaseType", Order:=1)>
        Public Property BaseType As Integer

        <JsonProperty("BaseEntry", Order:=2)>
        Public Property BaseEntry As Integer

        <JsonProperty("BaseLine", Order:=3)>
        Public Property BaseLine As Integer

        <JsonProperty("ItemCode", Order:=4)>
        Public Property ItemCode As String

        <JsonProperty("Quantity", Order:=5)>
        Public Property Quantity As Decimal

        <JsonProperty("FromWarehouseCode", Order:=6)>
        Public Property FromWarehouseCode As String

        <JsonProperty("WarehouseCode", Order:=7)>
        Public Property WarehouseCode As String

        <JsonProperty("U_Color", Order:=8)>
        Public Property U_Color As String

        <JsonProperty("U_Talla", Order:=9)>
        Public Property U_Talla As String

        <JsonProperty("BatchNumbers", Order:=10)>
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