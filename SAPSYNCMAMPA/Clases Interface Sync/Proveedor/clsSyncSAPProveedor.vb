Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Text
Imports Newtonsoft.Json.Linq
Public Class clsSyncSAPProveedor
    Implements IDisposable

    Private Shared BeNavEjecucionEnc As New clsBeI_nav_ejecucion_enc()
    Private Shared BeNavEjecucionRes As New clsBeI_nav_ejecucion_res()
    Private Shared BeConfigDet As New clsBeI_nav_config_det()
    Public Shared BeConfigEnc As New clsBeI_nav_config_enc()
    Public Shared ListaDetalleConfigDet As New List(Of clsBeI_nav_config_det)
    Private Shared VContadorBitacoraTOMWMS As Integer = 0
    Private Shared VContadorBitacoraIntermedia As Integer = 0

    Shared vHanaService As SapServiceLayerClient
    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub

    Public Shared Function ConstruirQuery(tipo As String, Optional codigo As String = Nothing) As String
        Dim query As String = "SELECT " &
            "T0.""CardCode"" AS ""CODIGO"", " &
            "T0.""CardName"" AS ""NOMBRE_COMERCIAL"", " &
            "T0.""Phone1"", " &
            "IFNULL(T1.""FirstName"", 'ND') AS ""CONTACTO"", " &
            "T0.""U_NIT"" AS ""NIT"", " &
            "T0.""Address"" AS ""DIRECCION"", " &
            "T0.""E_Mail"", " &
            "T0.""GroupCode"" " &
            "FROM ""OCRD"" T0 " &
            "LEFT JOIN (SELECT ""CardCode"", MIN(""CntctCode"") AS ""MinCntctCode"" FROM ""OCPR"" GROUP BY ""CardCode"") AS TMin " &
            "ON T0.""CardCode"" = TMin.""CardCode"" " &
            "LEFT JOIN ""OCPR"" T1 ON T0.""CardCode"" = T1.""CardCode"" AND T1.""CntctCode"" = TMin.""MinCntctCode"" " &
            $"WHERE T0.""CardType"" = '{tipo}' AND T0.""validFor"" = 'Y' " &
            $"AND (T0.""U_ENVIADO_WMS"" = '2' OR T0.""UpdateDate"" BETWEEN ADD_DAYS(CURRENT_DATE, -{BeConfigEnc.Rango_Dias_Importacion}) AND CURRENT_DATE) "

        If Not String.IsNullOrEmpty(codigo) Then
            query &= $"AND T0.""CardCode"" = '{codigo}' "
        End If

        Return query
    End Function

    Private Shared Function MapearProveedor(row As DataRow) As clsBeI_nav_proveedor
        Return New clsBeI_nav_proveedor With {
            .No = row("CODIGO").ToString(),
            .Name = row("NOMBRE_COMERCIAL").ToString(),
            .Phone_No = row("Phone1").ToString(),
            .Contact = row("CONTACTO").ToString(),
            .VAT_Registratrion_No = row("NIT").ToString(),
            .Adress = row("DIRECCION").ToString(),
            .Search_Name = row("E_Mail").ToString()
        }
    End Function
    Private Shared Function MapearCliente(row As DataRow) As clsBeI_nav_cliente
        Return New clsBeI_nav_cliente With {
            .No = row("CODIGO").ToString(),
            .Name = row("NOMBRE_COMERCIAL").ToString(),
            .Phone_No = row("Phone1").ToString(),
            .ContactName = row("CONTACTO").ToString(),
            .VAT_Registratrion_No = row("NIT").ToString(),
            .Adress = row("DIRECCION").ToString(),
            .Search_Name = row("E_Mail").ToString()
        }
    End Function

    Public Shared Async Function Importar_Proveedores_Desde_SAP_A_TablaIntermediaAsync(lbl As RichTextBox,
                                                                                       prg As ProgressBar,
                                                                                       cnnLog As SqlConnection) As Task(Of Boolean)
        Dim cnn As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim tran As SqlTransaction = Nothing

        Try
            clsPublic.Actualizar_Progreso(lbl, "Consultando proveedores nuevos en SAP...")

            vHanaService = New SapServiceLayerClient()
            Dim loginResponse As LoginResponseDto = Await vHanaService.LoginAsync()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lbl, "No se pudo obtener sesión.")
                Return False
            Else
                clsPublic.Actualizar_Progreso(lbl, "Conexión correcta.")
            End If

            Dim lista As List(Of clsBeI_nav_proveedor) = Await Get_Proveedores_SAP_SLAsync(vHanaService.SessionCookie, BD.Instancia.HANA_SL, lbl)
            BeNavEjecucionRes.Registros_ws = lista.Count
            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)
            Application.DoEvents()

            prg.Maximum = lista.Count
            prg.Value = 0
            VContadorBitacoraIntermedia = 0

            cnn.Open() : tran = cnn.BeginTransaction(IsolationLevel.ReadUncommitted)
            clsLnI_nav_proveedor.EliminarTodos(cnn, tran)
            If cnnLog.State = ConnectionState.Closed Then cnnLog.Open()

            For Each prov In lista
                Try
                    clsPublic.Actualizar_Progreso(lbl, $"Insertando proveedor: {prov.No}")
                    clsLnI_nav_proveedor.Insertar(prov, cnn, tran)
                    VContadorBitacoraIntermedia += 1
                    prg.Value += 1
                    Application.DoEvents()
                Catch ex As Exception
                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, prov.No, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, cnnLog)
                    clsPublic.Actualizar_Progreso(lbl, $"Error al insertar {prov.No}: {ex.Message}")
                End Try
            Next

            tran.Commit()
            clsPublic.Actualizar_Progreso(lbl, "Importación a tabla intermedia finalizada.")

        Catch ex As Exception
            If tran IsNot Nothing Then tran.Rollback()
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, "", BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, cnnLog)
            clsPublic.Actualizar_Progreso(lbl, $"Error general en importación: {ex.Message}")
            Throw
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close()
        End Try
    End Function

    Public Shared Sub Cargar_Config_Desde_DB(cnn As SqlConnection, tran As SqlTransaction)
        BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, cnn, tran)
        If BeConfigEnc Is Nothing Then
            Throw New Exception("No se pudo cargar la configuración de interface (BeConfigEnc).")
        End If
    End Sub

    Public Shared Sub ProcesarProveedores(proveedores As List(Of clsBeI_nav_proveedor),
                                              cnn As SqlConnection,
                                              tran As SqlTransaction,
                                              cnnLog As SqlConnection,
                                              lbl As RichTextBox,
                                              prg As ProgressBar,
                                              ByRef actualizados As List(Of String))

        Cargar_Config_Desde_DB(cnn, tran)

        prg.Maximum = proveedores.Count
        prg.Value = 0
        VContadorBitacoraTOMWMS = 0

        For Each navProv In proveedores
            prg.Value += 1
            clsPublic.Actualizar_Progreso(lbl, $"Procesando Proveedor: {navProv.No}")

            Dim existente = clsLnProveedor.Existe(navProv.No, cnn, tran)

            If existente IsNot Nothing Then

                Try
                    Dim proveedor As New clsBeProveedor With {
                        .IdEmpresa = BeConfigEnc.Idempresa,
                        .IdPropietario = BeConfigEnc.IdPropietario,
                        .IdProveedor = existente.IdProveedor,
                        .Codigo = navProv.No,
                        .Nombre = navProv.Name,
                        .Telefono = navProv.Phone_No,
                        .Nit = navProv.VAT_Registratrion_No,
                        .Direccion = navProv.Adress,
                        .Contacto = navProv.Contact,
                        .Activo = True
                    }

                    clsLnProveedor.Actualizar(proveedor, cnn, tran)
                    actualizados.Add(proveedor.Codigo)
                    VContadorBitacoraTOMWMS += 1

                Catch ex As Exception
                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, navProv.No, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, cnnLog)
                    clsPublic.Actualizar_Progreso(lbl, $"Error al actualizar proveedor: {navProv.No} -> {ex.Message}")
                End Try
            Else

                Try
                    Dim nuevoId = clsLnProveedor.MaxID(cnn, tran) + 1
                    Dim proveedor As New clsBeProveedor With {
                        .IdEmpresa = BeConfigEnc.Idempresa,
                        .IdPropietario = BeConfigEnc.IdPropietario,
                        .IdProveedor = nuevoId,
                        .Codigo = navProv.No,
                        .Nombre = navProv.Name,
                        .Telefono = navProv.Phone_No,
                        .Nit = navProv.VAT_Registratrion_No,
                        .Direccion = navProv.Adress,
                        .Contacto = navProv.Contact,
                        .Activo = True,
                        .User_agr = BeConfigEnc.IdUsuario,
                        .Fec_agr = Date.UtcNow,
                        .User_mod = BeConfigEnc.IdUsuario,
                        .Fec_mod = Date.UtcNow
                    }

                    clsLnProveedor.Insertar(proveedor, cnn, tran)

                    Dim provBodega As New clsBeProveedor_bodega With {
                        .IdAsignacion = clsLnProveedor_bodega.MaxID(cnn, tran) + 1,
                        .IdProveedor = proveedor.IdProveedor,
                        .IdBodega = BeConfigEnc.Idbodega,
                        .Activo = True,
                        .User_agr = BeConfigEnc.IdUsuario,
                        .User_mod = BeConfigEnc.IdUsuario,
                        .Fec_agr = Now,
                        .Fec_mod = Now
                    }

                    clsLnProveedor_bodega.InsertarFromInterface(provBodega, cnn, tran)

                    actualizados.Add(proveedor.Codigo)
                    VContadorBitacoraTOMWMS += 1

                Catch ex As Exception
                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, navProv.No, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, cnnLog)
                    clsPublic.Actualizar_Progreso(lbl, $"Error al insertar proveedor: {navProv.No} -> {ex.Message}")
                End Try
            End If

            Application.DoEvents()
        Next
    End Sub

    Public Shared Async Function Insertar_Proveedores_Desde_TablaIntermedia_A_Tabla_TOMWMS(lbl As RichTextBox,
                                                                                       prg As ProgressBar,
                                                                                       Optional ForzarEjecucion As Boolean = False,
                                                                                       Optional Preguntar As Boolean = False) As Task(Of Boolean)

        Dim cnn As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim cnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim tran As SqlTransaction = Nothing
        Dim proveedoresActualizados As New List(Of String)

        Try

            clsPublic.Actualizar_Progreso(lbl, $"Force_Ejecución: {ForzarEjecucion}")
            If Not ForzarEjecucion AndAlso Not Ejecutar_Interfaz("Proveedor") Then
                clsPublic.Actualizar_Progreso(lbl, "La configuración de la interfaz indica que no debe ejecutarse ahora.")
                Return False
            End If

            cnnLog.Open()
            Iniciar_Ejecucion(lbl, cnnLog)

            cnn.Open()
            tran = cnn.BeginTransaction()
            Cargar_Config_Desde_DB(cnn, tran)

            If Preguntar Then
                If MessageBox.Show("¿Deseas llenar tabla intermedia de proveedores desde SAP?", "Confirmar importación", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    Await Importar_Proveedores_Desde_SAP_A_TablaIntermediaAsync(lbl, prg, cnnLog)
                Else
                    Return False
                End If
            End If

            Dim lista = clsLnI_nav_proveedor.GetAll(cnn, tran)
            If lista.Count = 0 Then
                clsPublic.Actualizar_Progreso(lbl, "No se encontraron proveedores en tabla intermedia.")
                Return False
            End If

            ProcesarProveedores(lista, cnn, tran, cnnLog, lbl, prg, proveedoresActualizados)

            For Each codigo In proveedoresActualizados
                Await Marcar_Proveedor_Sincronizado_SLAsync(codigo, vHanaService.SessionCookie, BD.Instancia.HANA_SL)
                clsPublic.Actualizar_Progreso(lbl, "Proveedor sincronizado " & codigo)
            Next

            tran.Commit()
            Finalizar_Ejecucion(lbl, cnnLog, "Proveedores procesados correctamente")
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

    'Public Shared Function Marcar_Proveedor_Sincronizado_SAP(codigos As List(Of String)) As Boolean
    '    Try
    '        For Each codigo In codigos
    '            Dim query As String = $"UPDATE OCRD SET U_ENVIADO_WMS = '1' WHERE ""CardCode"" = '{codigo}'"
    '            HanaHelper.Xcute(query)
    '        Next
    '        Return True
    '    Catch ex As Exception
    '        Throw New Exception($"Error al marcar proveedor como sincronizado: {ex.Message}")
    '    End Try
    'End Function

    'Public Shared Function Marcar_Proveedor_Sincronizado_SAP(codigo As String) As Boolean
    '    Try
    '        Dim query As String = $"UPDATE OCRD SET U_ENVIADO_WMS = '1' WHERE ""CardCode"" = '{codigo}'"
    '        HanaHelper.Xcute(query)
    '        Return True
    '    Catch ex As Exception
    '        Throw New Exception($"Error al marcar proveedor como sincronizado: {ex.Message}")
    '    End Try
    'End Function

    Public Shared Function Ejecutar_Interfaz(ByVal NombreEntidad As String) As Boolean

        Ejecutar_Interfaz = False

        Dim diaActual As Integer = Now.Day
        Dim horaActual As Date = TimeOfDay

        Try

            ListaDetalleConfigDet = clsLnI_nav_config_det.Get_All_By_IdEnc(BD.Instancia.IdConfiguracionInterface, NombreEntidad)

            If ListaDetalleConfigDet.Count > 0 Then

                Dim vBeConfigDet As New clsBeI_nav_config_det
                vBeConfigDet = ListaDetalleConfigDet(0)
                BeConfigDet = vBeConfigDet

            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Insertar_Proveedor_From_SAP(navProveedor As clsBeI_nav_proveedor,
                                                       config As clsBeI_nav_config_enc,
                                                       cnn As SqlConnection,
                                                       tran As SqlTransaction,
                                                       cnnLog As SqlConnection,
                                                       lbl As RichTextBox) As clsBeProveedor_bodega

        Dim proveedorBodega As clsBeProveedor_bodega = Nothing

        Try
            Dim nuevoId = clsLnProveedor.MaxID(cnn, tran) + 1
            Dim proveedor As New clsBeProveedor With {
            .IdEmpresa = config.Idempresa,
            .IdPropietario = config.IdPropietario,
            .IdProveedor = nuevoId,
            .Codigo = navProveedor.No,
            .Nombre = navProveedor.Name,
            .Telefono = If(navProveedor.Phone_No, ""),
            .Nit = If(navProveedor.VAT_Registratrion_No, ""),
            .Direccion = If(navProveedor.Adress, ""),
            .Contacto = If(navProveedor.Contact, ""),
            .Activo = True,
            .User_agr = config.IdUsuario,
            .Fec_agr = Date.UtcNow,
            .User_mod = config.IdUsuario,
            .Fec_mod = Date.UtcNow
        }

            clsLnProveedor.Insertar(proveedor, cnn, tran)
            VContadorBitacoraTOMWMS += 1

            proveedorBodega = New clsBeProveedor_bodega With {
            .IdAsignacion = clsLnProveedor_bodega.MaxID(cnn, tran) + 1,
            .IdProveedor = proveedor.IdProveedor,
            .IdBodega = config.Idbodega,
            .Activo = True,
            .User_agr = config.IdUsuario,
            .User_mod = config.IdUsuario,
            .Fec_agr = Now,
            .Fec_mod = Now
        }

            clsLnProveedor_bodega.InsertarFromInterface(proveedorBodega, cnn, tran)

        Catch ex As Exception
            Dim mensaje = $"Error al insertar proveedor desde SAP: {ex.Message}"
            clsLnI_nav_ejecucion_det_error.Inserta_Log(mensaje, navProveedor.No, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, cnnLog)
            clsPublic.Actualizar_Progreso(lbl, $"Error al insertar proveedor: {navProveedor.No}{vbNewLine}{mensaje}")
            proveedorBodega = Nothing
        End Try

        Return proveedorBodega
    End Function

    Public Shared Async Function Get_Proveedor_SAP_SLAsync(codigo As String, sessionCookie As String, baseUrl As String) As Task(Of clsBeI_nav_proveedor)

        Try
            If String.IsNullOrWhiteSpace(codigo) Then Return Nothing

            ' Filtro OData para proveedores activos
            Dim filtro = $"CardType eq 'cSupplier' and CardCode eq '{codigo}' and Valid eq 'tYES'"
            Dim requestUrl As String = $"BusinessPartners?$filter={Uri.EscapeDataString(filtro)}"

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
                            Dim errContent = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                            Throw New Exception($"Error al obtener proveedor. Código: {response.StatusCode}, Detalle: {errContent}")
                        End If

                        Dim jsonResponse = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                        Dim obj = JObject.Parse(jsonResponse)
                        Dim rows = obj("value")

                        If rows Is Nothing OrElse Not rows.HasValues Then
                            Return Nothing
                        End If

                        Dim proveedorJson = rows.First()

                        Dim proveedor As New clsBeI_nav_proveedor With {
                            .No = proveedorJson.Value(Of String)("CardCode"),
                            .Name = proveedorJson.Value(Of String)("CardName"),
                            .Adress = proveedorJson.Value(Of String)("Address"),
                            .City = proveedorJson.Value(Of String)("City"),
                            .Country = proveedorJson.Value(Of String)("Country"),
                            .Phone_No = proveedorJson.Value(Of String)("Phone1"),
                            .VAT_Registratrion_No = proveedorJson.Value(Of String)("FederalTaxID"),
                            .Search_Name = proveedorJson.Value(Of String)("AliasName"),
                            .Location_Code = proveedorJson.Value(Of String)("U_LocationCode")
                        }

                        ' Obtener contacto (si existe)
                        Dim contactos = proveedorJson("ContactEmployees")
                        If contactos IsNot Nothing AndAlso contactos.HasValues Then
                            proveedor.Contact = contactos.First.Value(Of String)("FirstName")
                        Else
                            proveedor.Contact = ""
                        End If

                        Return proveedor
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception("Error en Get_Proveedor_SAP_SLAsync: " & ex.Message, ex)
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

    Public Shared Async Function Get_Proveedores_SAP_SLAsync(sessionCookie As String, baseUrl As String, lbl As RichTextBox) As Task(Of List(Of clsBeI_nav_proveedor))

        Try

            ' Filtro OData para proveedores activos
            Dim filtro = $"CardType eq 'cSupplier' and Valid eq 'tYES' and (U_ENVIADO_WMS eq 2 or U_ENVIADO_WMS eq null)"

            Dim pageSize As Integer = 100
            Dim skip As Integer = 0

            Dim lProveedores As New List(Of clsBeI_nav_proveedor)

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
                                Throw New Exception($"Error al obtener proveedor. Código: {response.StatusCode}, Detalle: {errContent}")
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

                            For Each proveedorJson In rows

                                Dim proveedor As New clsBeI_nav_proveedor With {
                                    .No = proveedorJson.Value(Of String)("CardCode"),
                                    .Name = proveedorJson.Value(Of String)("CardName"),
                                    .Adress = proveedorJson.Value(Of String)("Address"),
                                    .City = proveedorJson.Value(Of String)("City"),
                                    .Country = proveedorJson.Value(Of String)("Country"),
                                    .Phone_No = proveedorJson.Value(Of String)("Phone1"),
                                    .VAT_Registratrion_No = proveedorJson.Value(Of String)("FederalTaxID"),
                                    .Search_Name = proveedorJson.Value(Of String)("AliasName"),
                                    .Location_Code = proveedorJson.Value(Of String)("U_LocationCode")
                                }
                                ' Obtener contacto (si existe)
                                Dim contactos = proveedorJson("ContactEmployees")
                                If contactos IsNot Nothing AndAlso contactos.HasValues Then
                                    proveedor.Contact = contactos.First.Value(Of String)("FirstName")
                                Else
                                    proveedor.Contact = ""
                                End If

                                If clsLnProveedor.Existe(proveedor.No) Is Nothing Then
                                    lProveedores.Add(proveedor)
                                    If lbl IsNot Nothing Then
                                        clsPublic.Actualizar_Progreso(lbl, $"Proveedor agregado: {proveedor.No}")
                                    End If
                                End If

                            Next

                            ' Avanzar al siguiente bloque
                            skip += filasPagina

                        End Using

                    End While

                End Using

            End Using

            Return lProveedores

        Catch ex As Exception
            Throw New Exception("Error en Get_Proveedores_SAP_SLAsync: " & ex.Message, ex)
        End Try

    End Function

End Class