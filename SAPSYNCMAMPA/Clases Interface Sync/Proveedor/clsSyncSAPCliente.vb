Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Text
Imports Newtonsoft.Json.Linq
Public Class clsSyncSAPCliente

    Private Shared BeNavEjecucionEnc As New clsBeI_nav_ejecucion_enc()
    Private Shared BeNavEjecucionRes As New clsBeI_nav_ejecucion_res()
    Private Shared BeConfigDet As New clsBeI_nav_config_det()
    Private Shared BeConfigEnc As New clsBeI_nav_config_enc()
    Public Shared ListaDetalleConfigDet As New List(Of clsBeI_nav_config_det)()
    Private Shared VContadorBitacoraTOMWMS As Integer = 0
    Private Shared VContadorBitacoraIntermedia As Integer = 0
    Shared vHanaService As SapServiceLayerClient

    Public Shared Sub Iniciar_Ejecucion(lbl As RichTextBox, cnnLog As SqlConnection)
        Try
            BeNavEjecucionEnc = New clsBeI_nav_ejecucion_enc With {
                .IdEjecucionEnc = clsLnI_nav_ejecucion_enc.MaxID(cnnLog),
                .IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface,
                .Fecha = Now
            }

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, cnnLog)

            BeNavEjecucionRes = New clsBeI_nav_ejecucion_res With {
                .IdEjecucionRes = clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(cnnLog) + 1,
                .IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc,
                .IdNavConfigDet = BeConfigDet.Idnavconfigdet,
                .Registros_ws = 0,
                .Registros_ti = 0,
                .Registros_WMS = 0,
                .Exitosa = False
            }

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, cnnLog)
            clsPublic.Actualizar_Progreso(lbl, $"Inicio de ejecución {BeNavEjecucionEnc.IdEjecucionEnc}")
        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lbl, $"Error al iniciar ejecución: {ex.Message}")
            Throw
        End Try
    End Sub

    Public Shared Sub ProcesarClientes(clientes As List(Of clsBeI_nav_cliente),
                                        cnn As SqlConnection,
                                        tran As SqlTransaction,
                                        cnnLog As SqlConnection,
                                        lbl As RichTextBox,
                                        prg As ProgressBar,
                                        ByRef actualizados As List(Of String))

        Cargar_Config_Desde_DB(cnn, tran)

        prg.Maximum = clientes.Count
        prg.Value = 0
        VContadorBitacoraTOMWMS = 0

        For Each navCli In clientes

            prg.Value += 1

            clsPublic.Actualizar_Progreso(lbl, $"Procesando Cliente: {navCli.No}")

            Dim existente = clsLnCliente.Existe(navCli.No, cnn, tran)

            If existente IsNot Nothing Then
                Try
                    Dim cliente As New clsBeCliente With {
                        .IdEmpresa = BeConfigEnc.Idempresa,
                        .IdPropietario = BeConfigEnc.IdPropietario,
                        .IdCliente = existente.IdCliente,
                        .Codigo = navCli.No,
                        .Nombre_comercial = navCli.Name,
                        .Telefono = navCli.Phone_No,
                        .Nit = navCli.VAT_Registratrion_No,
                        .Direccion = navCli.Adress,
                        .Nombre_contacto = navCli.ContactName,
                        .Activo = True,
                        .IdTipoCliente = 1
                    }

                    clsLnCliente.Actualizar(cliente, cnn, tran)
                    actualizados.Add(cliente.Codigo)
                    VContadorBitacoraTOMWMS += 1

                Catch ex As Exception
                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, navCli.No, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, cnnLog)
                    clsPublic.Actualizar_Progreso(lbl, $"Error al actualizar cliente: {navCli.No} -> {ex.Message}")
                End Try
            Else
                Try
                    Dim nuevoId = clsLnCliente.MaxID(cnn, tran) + 1
                    Dim cliente As New clsBeCliente With {
                        .IdEmpresa = BeConfigEnc.Idempresa,
                        .IdPropietario = BeConfigEnc.IdPropietario,
                        .IdCliente = nuevoId,
                        .Codigo = navCli.No,
                        .Nombre_comercial = navCli.Name,
                        .Telefono = navCli.Phone_No,
                        .Nit = navCli.VAT_Registratrion_No,
                        .Direccion = navCli.Adress,
                        .Nombre_contacto = navCli.ContactName,
                        .Activo = True,
                        .User_agr = BeConfigEnc.IdUsuario,
                        .Fec_agr = Date.UtcNow,
                        .User_mod = BeConfigEnc.IdUsuario,
                        .Fec_mod = Date.UtcNow,
                        .IdTipoCliente = 1
                    }

                    clsLnCliente.Insertar(cliente, cnn, tran)

                    Dim BeClienteBodega As New clsBeCliente_bodega
                    BeClienteBodega = clsLnCliente_bodega.GetSingle(cliente.IdCliente, BeConfigEnc.Idbodega, cnn, tran)

                    If BeClienteBodega Is Nothing Then

                        BeClienteBodega = New clsBeCliente_bodega
                        BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID(cnn, tran) + 1
                        BeClienteBodega.IdCliente = cliente.IdCliente
                        BeClienteBodega.IdBodega = BeConfigEnc.Idbodega
                        BeClienteBodega.Activo = True
                        BeClienteBodega.User_agr = BeConfigEnc.IdUsuario
                        BeClienteBodega.User_mod = BeConfigEnc.IdUsuario
                        BeClienteBodega.Fec_agr = Now
                        BeClienteBodega.Fec_mod = Now

                        clsLnCliente_bodega.Insertar(BeClienteBodega, cnn, tran)

                    End If

                    actualizados.Add(cliente.Codigo)
                    VContadorBitacoraTOMWMS += 1

                Catch ex As Exception
                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, navCli.No, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, cnnLog)
                    clsPublic.Actualizar_Progreso(lbl, $"Error al insertar cliente: {navCli.No} -> {ex.Message}")
                End Try
            End If

            Application.DoEvents()
        Next
    End Sub

    Public Shared Async Function Insertar_Clientes_Desde_TablaIntermedia_A_Tabla_TOMWMSAsync(lbl As RichTextBox,
                                                                                   prg As ProgressBar,
                                                                                   Optional ForzarEjecucion As Boolean = False,
                                                                                   Optional Preguntar As Boolean = False) As Task(Of Boolean)

        Dim cnn As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim cnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim tran As SqlTransaction = Nothing
        Dim clientesActualizados As New List(Of String)

        Try
            clsPublic.Actualizar_Progreso(lbl, $"Force_Ejecución: {ForzarEjecucion}")
            If Not ForzarEjecucion AndAlso Not clsSyncSAPProveedor.Ejecutar_Interfaz("Cliente") Then
                clsPublic.Actualizar_Progreso(lbl, "La configuración de la interfaz indica que no debe ejecutarse ahora.")
                Return True
            End If

            cnnLog.Open()
            clsSyncSAPCliente.Iniciar_Ejecucion(lbl, cnnLog)

            cnn.Open()
            tran = cnn.BeginTransaction()
            Cargar_Config_Desde_DB(cnn, tran)

            If Preguntar Then
                If MessageBox.Show("¿Deseas llenar tabla intermedia de clientes desde SAP?", "Confirmar importación", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    Await Importar_Clientes_Desde_SAP_A_TablaIntermediaAsync(lbl, prg, cnnLog)
                Else
                    Return False
                End If
            End If

            Dim lista = clsLnI_nav_cliente.Get_All(cnn, tran)
            If lista.Count = 0 Then
                clsPublic.Actualizar_Progreso(lbl, "No se encontraron clientes en tabla intermedia.")
                Return True
            End If

            ProcesarClientes(lista, cnn, tran, cnnLog, lbl, prg, clientesActualizados)

            For Each codigo In clientesActualizados
                Await Marcar_Cliente_Sincronizado_SLAsync(codigo, vHanaService.SessionCookie, BD.Instancia.HANA_SL)
                clsPublic.Actualizar_Progreso(lbl, "Cliente sincronizado " & codigo)
            Next

            tran.Commit()

            Finalizar_Ejecucion(lbl, cnnLog, "Clientes procesados correctamente")

            Return True

        Catch ex As Exception
            If tran IsNot Nothing Then tran.Rollback()
            clsPublic.Actualizar_Progreso(lbl, $"Error general: {ex.Message}")
            Throw New Exception($" (M) {MethodBase.GetCurrentMethod.Name} {ex.Message}")
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close()
            If cnnLog.State = ConnectionState.Open Then cnnLog.Close()
            prg.Value = 0 : prg.Visible = False
        End Try
    End Function

    Public Shared Sub Finalizar_Ejecucion(lbl As RichTextBox, cnnLog As SqlConnection, resumen As String)
        Try
            clsPublic.Actualizar_Progreso(lbl, "Fin de inserción en TOMWMS.")
            clsPublic.Actualizar_Progreso(lbl, resumen & $": {VContadorBitacoraTOMWMS}")
            clsPublic.Actualizar_Progreso(lbl, $"Tiempo transcurrido: {DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)} segundo(s)")

            BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
            BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTOMWMS
            BeNavEjecucionRes.Exitosa = (VContadorBitacoraTOMWMS = VContadorBitacoraIntermedia)

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)
        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lbl, $"Error al finalizar ejecución: {ex.Message}")
        End Try
    End Sub

    Public Shared Function Continuar_Importacion(preguntar As Boolean,
                                                 mensajeConfirmacion As String,
                                                 importacionHandler As Func(Of Boolean),
                                                 lbl As RichTextBox) As Boolean
        Try
            If Not preguntar Then
                Return importacionHandler()
            End If

            Dim respuesta = MessageBox.Show(mensajeConfirmacion, "Confirmar importación", MessageBoxButtons.YesNo)
            If respuesta = DialogResult.Yes Then
                Return importacionHandler()
            Else
                clsPublic.Actualizar_Progreso(lbl, "Importación desde SAP cancelada por el usuario.")
                Return False
            End If
        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lbl, $"Error en la decisión de importación: {ex.Message}")
            Throw
        End Try
    End Function

    Public Shared Sub Cargar_Config_Desde_DB(cnn As SqlConnection, tran As SqlTransaction)
        BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, cnn, tran)
        If BeConfigEnc Is Nothing Then
            Throw New Exception("No se pudo cargar la configuración de interface (BeConfigEnc).")
        End If
    End Sub

    Public Shared Async Function Get_Clientes_SAP_SLAsync(sessionCookie As String,
                                                          baseUrl As String,
                                                          lbl As RichTextBox) As Task(Of List(Of clsBeI_nav_cliente))

        Try

            ' Filtro OData para clientes activos
            Dim filtro = $"CardType eq 'cCustomer' and Valid eq 'tYES' and (U_ENVIADO_WMS eq 2 or U_ENVIADO_WMS eq null) "
            Dim pageSize As Integer = 100
            Dim skip As Integer = 0

            Dim lClientes As New List(Of clsBeI_nav_cliente)

            Using handler As New HttpClientHandler()
                handler.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)
                    client.DefaultRequestHeaders.ConnectionClose = True
                    Dim hayMas As Boolean = True

                    While hayMas

                        Dim requestUrl As String = $"BusinessPartners?$filter={Uri.EscapeDataString(filtro)}&$top={pageSize}&$skip={skip}"

                        Using request As New HttpRequestMessage(HttpMethod.Get, baseUrl & requestUrl)

                            request.Headers.ConnectionClose = True
                            request.Headers.Add("Cookie", sessionCookie)
                            request.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

                            Dim response = Await client.SendAsync(request).ConfigureAwait(False)

                            If Not response.IsSuccessStatusCode Then
                                Dim errContent = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                                Throw New Exception($"Error al obtener cliente. Código: {response.StatusCode}, Detalle: {errContent}")
                            End If

                            Dim jsonResponse = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                            Dim obj = JObject.Parse(jsonResponse)
                            Dim rows = obj("value")

                            If rows Is Nothing OrElse Not rows.HasValues Then
                                ' Ya no hay más páginas
                                hayMas = False
                                Exit While
                            End If

                            Dim filasPagina As Integer = rows.Count()

                            For Each clienteJson In rows

                                Dim cliente As New clsBeI_nav_cliente With {
                                    .IdCliente = 1,
                                    .Nombre_cliente = clienteJson.Value(Of String)("CardName"),
                                    .Nit = clienteJson.Value(Of String)("U_NIT"),
                                    .Razon_social = clienteJson.Value(Of String)("CardName"),
                                    .Procesado_wms = False,
                                    .No = clienteJson.Value(Of String)("CardCode"),
                                    .Name = clienteJson.Value(Of String)("CardName"),
                                    .Adress = clienteJson.Value(Of String)("Address"),
                                    .City = clienteJson.Value(Of String)("City"),
                                    .Country = clienteJson.Value(Of String)("Country"),
                                    .Phone_No = clienteJson.Value(Of String)("Phone1"),
                                    .ContactName = "",
                                    .Search_Name = "",
                                    .Location_Code = ""
                                }

                                If clsLnCliente.Existe(cliente.No) Is Nothing Then
                                    lClientes.Add(cliente)
                                    If lbl IsNot Nothing Then
                                        clsPublic.Actualizar_Progreso(lbl, $"Cliente agregado: {cliente.No}")
                                    End If
                                End If

                            Next

                            ' Avanzar al siguiente bloque
                            skip += filasPagina

                        End Using

                    End While

                End Using

            End Using

            Return lClientes

        Catch ex As Exception
            Throw New Exception("Error en Get_Proveedores_SAP_SLAsync: " & ex.Message, ex)
        End Try

    End Function

    Public Shared Async Function Importar_Clientes_Desde_SAP_A_TablaIntermediaAsync(lbl As RichTextBox,
                                                                                    prg As ProgressBar,
                                                                                    cnnLog As SqlConnection) As Task(Of Boolean)
        Dim cnn As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim tran As SqlTransaction = Nothing

        Try
            clsPublic.Actualizar_Progreso(lbl, "Consultando clientes nuevos en SAP...")

            vHanaService = New SapServiceLayerClient()
            Dim loginResponse As LoginResponseDto = Await vHanaService.LoginAsync()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lbl, "No se pudo obtener sesión.")
                Return False
            Else
                clsPublic.Actualizar_Progreso(lbl, "Conexión correcta.")
            End If

            Dim lista As List(Of clsBeI_nav_cliente) = Await Get_Clientes_SAP_SLAsync(vHanaService.SessionCookie, BD.Instancia.HANA_SL, lbl)
            BeNavEjecucionRes.Registros_ws = lista.Count
            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)
            Application.DoEvents()

            prg.Maximum = lista.Count
            prg.Value = 0
            VContadorBitacoraIntermedia = 0

            cnn.Open() : tran = cnn.BeginTransaction(IsolationLevel.ReadUncommitted)
            clsLnI_nav_cliente.EliminarTodos(cnn, tran)
            If cnnLog.State = ConnectionState.Closed Then cnnLog.Open()

            For Each cliente In lista
                Try
                    clsPublic.Actualizar_Progreso(lbl, $"Insertando cliente: {cliente.No}")
                    cliente.IdCliente = clsLnI_nav_cliente.MaxID(cnn, tran) + 1
                    clsLnI_nav_cliente.Insertar(cliente, cnn, tran)
                    VContadorBitacoraIntermedia += 1
                    prg.Value += 1
                    Application.DoEvents()
                Catch ex As Exception
                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, cliente.No, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, cnnLog)
                    clsPublic.Actualizar_Progreso(lbl, $"Error al insertar {cliente.No}: {ex.Message}")
                End Try
            Next

            tran.Commit()
            clsPublic.Actualizar_Progreso(lbl, "Importación a tabla intermedia finalizada.")

            Return True

        Catch ex As Exception
            If tran IsNot Nothing Then tran.Rollback()
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, "", BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, cnnLog)
            clsPublic.Actualizar_Progreso(lbl, $"Error general en importación: {ex.Message}")
            Throw
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close()
        End Try
    End Function

    Public Shared Async Function Marcar_Proveedor_Sincronizado_SLAsync(codigo As String,
                                                                       sessionCookie As String,
                                                                       baseUrl As String) As Task(Of Boolean)
        Try
            If String.IsNullOrWhiteSpace(codigo) Then Return False

            Dim requestUrl As String = $"BusinessPartners('{codigo}')"
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
            Throw New Exception($"Error al marcar proveedor como sincronizado (SL): {ex.Message}", ex)
        End Try
    End Function

    Public Shared Async Function Marcar_Cliente_Sincronizado_SLAsync(codigo As String,
                                                                     sessionCookie As String,
                                                                     baseUrl As String) As Task(Of Boolean)
        Try
            If String.IsNullOrWhiteSpace(codigo) Then Return False

            Dim requestUrl As String = $"BusinessPartners('{codigo}')"
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
            Throw New Exception($"Error al marcar proveedor como sincronizado (SL): {ex.Message}", ex)
        End Try
    End Function

    Public Shared Async Function Get_Cliente_SAP_SLAsync(sessionCookie As String,
                                                         baseUrl As String,
                                                         cardCode As String,
                                                         lbl As RichTextBox) As Task(Of clsBeI_nav_cliente)

        Try
            If String.IsNullOrWhiteSpace(cardCode) Then
                Throw New ArgumentException("CardCode es obligatorio.")
            End If

            ' Filtro OData para un cliente específico, activo y no enviado (o null)
            Dim filtro = $"CardCode eq '{cardCode.Replace("'", "''")}' and CardType eq 'cCustomer' and Valid eq 'tYES' and (U_ENVIADO_WMS eq 2 or U_ENVIADO_WMS eq null)"

            ' Traer solo campos necesarios
            Dim selectFields = "CardCode,CardName,Address,City,Country,Phone1,U_NIT"

            Dim requestUrl As String = $"BusinessPartners?$select={selectFields}&$filter={Uri.EscapeDataString(filtro)}&$top=1"

            Using handler As New HttpClientHandler()
                handler.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)
                    ' Recomendado: no forzar ConnectionClose
                    ' client.DefaultRequestHeaders.ConnectionClose = True

                    Using request As New HttpRequestMessage(HttpMethod.Get, baseUrl & requestUrl)
                        request.Headers.Add("Cookie", sessionCookie)
                        request.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

                        Dim response = Await client.SendAsync(request).ConfigureAwait(False)

                        If Not response.IsSuccessStatusCode Then
                            Dim errContent = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                            Throw New Exception($"Error al obtener cliente. Código: {response.StatusCode}, Detalle: {errContent}")
                        End If

                        Dim jsonResponse = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                        Dim obj = JObject.Parse(jsonResponse)
                        Dim rows = obj("value")

                        If rows Is Nothing OrElse Not rows.HasValues Then
                            ' No encontrado
                            Return Nothing
                        End If

                        Dim clienteJson = rows(0)

                        Dim cliente As New clsBeI_nav_cliente With {
                            .IdCliente = 1,
                            .Nombre_cliente = clienteJson.Value(Of String)("CardName"),
                            .Nit = clienteJson.Value(Of String)("U_NIT"),
                            .Razon_social = clienteJson.Value(Of String)("CardName"),
                            .Procesado_wms = False,
                            .No = clienteJson.Value(Of String)("CardCode"),
                            .Name = clienteJson.Value(Of String)("CardName"),
                            .Adress = clienteJson.Value(Of String)("Address"),
                            .City = clienteJson.Value(Of String)("City"),
                            .Country = clienteJson.Value(Of String)("Country"),
                            .Phone_No = clienteJson.Value(Of String)("Phone1"),
                            .ContactName = "",
                            .Search_Name = "",
                            .Location_Code = ""
                        }

                        ' Mantengo tu validación local
                        If clsLnCliente.Existe(cliente.No) Is Nothing Then
                            If lbl IsNot Nothing Then
                                clsPublic.Actualizar_Progreso(lbl, $"Cliente encontrado: {cliente.No}")
                            End If
                            Return cliente
                        Else
                            If lbl IsNot Nothing Then
                                clsPublic.Actualizar_Progreso(lbl, $"Cliente ya existe localmente: {cliente.No}")
                            End If
                            Return Nothing
                        End If

                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception("Error en Get_Cliente_SAP_SLAsync: " & ex.Message, ex)
        End Try

    End Function

End Class