Imports System.Data.SqlClient
Imports System.Reflection
Imports TOMWMS.WSListaCliente

Public Class clsSyncNavCliente : Inherits clsInterfaceBase
    Implements IDisposable

    Private fichaClientes() As Lista_clientes
    Dim VContadorBitacoraTomims As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0

    Private wsClienteService As New Lista_clientes_Service() With
            {
            .UseDefaultCredentials = UsarCredencialesPorDefecto,
            .Credentials = CredencialesConexion
            }

    Public Sub Dispose() Implements IDisposable.Dispose
        If wsClienteService IsNot Nothing Then
            wsClienteService.Dispose()
            wsClienteService = Nothing
        End If
    End Sub

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing
    Private Function Importar_Clientes_DesdeWSNav_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                       ByRef prg As ProgressBar,
                                                                       ByRef cnnLog As SqlConnection) As Boolean
        Importar_Clientes_DesdeWSNav_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing
        Dim vContador As Integer = 0
        Dim TieneFiltros As Boolean = False

        Try

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** PROCESANDO DOCUMENTO EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Consultando Clientes desde webservice.")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim BeNavEnt As New clsBeI_nav_ent
            BeNavEnt = clsLnI_nav_ent.Get_Single_By_Nombre("Cliente")
            Dim vCriteria As String = ""
            Dim ModifiedDate As String = "14032022.."

            If Not BeNavEnt Is Nothing Then

                If Not BeNavEnt.lDetalleFiltros Is Nothing Then

                    If BeNavEnt.lDetalleFiltros.Count > 0 Then

                        TieneFiltros = True

                        Dim lFiltros As New List(Of clsBeI_nav_ent_filtros)
                        lFiltros = BeNavEnt.lDetalleFiltros

                        For Each FiltroCategoria In lFiltros

                            If FiltroCategoria.Tipo_Filtro = "" OrElse FiltroCategoria.Tipo_Filtro = "BODEGA" Then

                                If vContador = 0 Then
                                    vCriteria = FiltroCategoria.Valor
                                Else
                                    vCriteria += "|" & FiltroCategoria.Valor
                                End If

                            ElseIf FiltroCategoria.Tipo_Filtro = "FECHA_MODIFICACION" Then
                                ModifiedDate = FiltroCategoria.Valor
                            End If

                            vContador += 1

                        Next

                    End If

                End If

            End If

            wsClienteService.Url = BD.Instancia.URLClientes

            If TieneFiltros Then
                Dim vFiltro1 As New Lista_clientes_Filter() With {.Field = Lista_clientes_Fields.Location_Code, .Criteria = vCriteria}
                Dim vFiltro2 As New Lista_clientes_Filter() With {.Field = Lista_clientes_Fields.Blocked, .Criteria = 0}
                Dim vFiltroFechaDesde As New Lista_clientes_Filter() With {.Field = Lista_clientes_Fields.Last_Date_Modified, .Criteria = ModifiedDate}
                Dim vFiltros As Lista_clientes_Filter() = New Lista_clientes_Filter() {vFiltro1, vFiltroFechaDesde}
                fichaClientes = wsClienteService.ReadMultiple(vFiltros, Nothing, 0)
            Else
                fichaClientes = wsClienteService.ReadMultiple(Nothing, "", 0)
            End If

            BeNavEjecucionRes.Registros_ws = fichaClientes.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Application.DoEvents()

            Dim BeI_nav_Cliente As clsBeI_nav_cliente

            lblprg.AppendText(String.Format("cleientes encontrados en WS: {0} ", fichaClientes.Count))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            prg.Maximum = fichaClientes.Count

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsLnI_nav_cliente.Eliminar_Todos(lConnection, lTransaction)

            If cnnLog.State = ConnectionState.Closed Then cnnLog.Open()

            Dim beCliente As Lista_clientes

            vContador = 0

            For Each Cli As Lista_clientes In fichaClientes

                beCliente = Cli

                Try

                    BeI_nav_Cliente = New clsBeI_nav_cliente
                    BeI_nav_Cliente.IdCliente = clsLnI_nav_cliente.MaxID(lConnection, lTransaction) + 1
                    BeI_nav_Cliente.Codigo_cliente = Cli.No
                    BeI_nav_Cliente.Nombre_cliente = Cli.Name
                    BeI_nav_Cliente.Nit = Cli.VAT_Registration_No
                    BeI_nav_Cliente.Razon_social = Cli.Name
                    BeI_nav_Cliente.No = Cli.No
                    BeI_nav_Cliente.Name = Cli.Name
                    BeI_nav_Cliente.Adress = Cli.Address
                    BeI_nav_Cliente.City = Cli.City
                    BeI_nav_Cliente.Country = Cli.County
                    BeI_nav_Cliente.Phone_No = Cli.Phone_No
                    'BeI_nav_Cliente.ContactName = Cli.ContactName
                    BeI_nav_Cliente.Search_Name = Cli.Search_Name
                    BeI_nav_Cliente.VAT_Registratrion_No = Cli.VAT_Registration_No
                    BeI_nav_Cliente.Location_Code = Cli.Location_Code

                    lblprg.AppendText(String.Format("Procesando Cliente: {0} ", BeI_nav_Cliente.No, vbNewLine))
                    lblprg.AppendText(vbNewLine)
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    clsLnI_nav_cliente.Insertar(BeI_nav_Cliente,
                                                lConnection,
                                                lTransaction)

                    VContadorBitacoraIntermedia += 1

                    prg.Value = vContador

                    vContador += 1

                    Application.DoEvents()

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                              beCliente.No,
                                                              BeNavEjecucionEnc.IdEjecucionEnc,
                                                              BeConfigDet.Idnavconfigdet, cnnLog)

                    lblprg.AppendText("Error al insertar desde ws a intermedia: " & beCliente.No & vbNewLine &
                                                   ex.Message)

                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End Try

            Next

            lTransaction.Commit()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** FIN DE INSERCIÓN EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Importar_Clientes_DesdeWSNav_A_TablaIntermedia = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            lblprg.AppendText(String.Format("Error al insertar Clientes desde ws a intermedia: {0}{1}", vbNewLine, ex.Message))

            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Function Insertar_Cliente_Single(ByVal NoCliente As String,
                                            ByRef lConnection As SqlConnection,
                                            ByRef lTransaction As SqlTransaction,
                                            ByRef lConnectionLog As SqlConnection,
                                            ByRef lblprg As RichTextBox,
                                            ByRef prg As ProgressBar) As clsBeCliente_bodega

        Insertar_Cliente_Single = Nothing

        Try

            Dim navCliente As New Lista_clientes

            navCliente = wsClienteService.Read(NoCliente)

            Dim BeCliente As New clsBeCliente
            Dim BeClienteBodega As New clsBeCliente_bodega

            If Not navCliente Is Nothing Then

                lblprg.AppendText(String.Format("Procesando Cliente: {0} ", navCliente.No, vbNewLine))
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                BeCliente.IdEmpresa = BeConfigEnc.Idempresa
                BeCliente.IdPropietario = BeConfigEnc.IdPropietario
                BeCliente.IdCliente = clsLnCliente.MaxID(lConnection, lTransaction) + 1
                BeCliente.Codigo = navCliente.No
                BeCliente.Nombre_comercial = navCliente.Name
                BeCliente.Telefono = IIf(navCliente.Phone_No Is Nothing, "", navCliente.Phone_No)
                BeCliente.Nit = IIf(navCliente.VAT_Registration_No Is Nothing, "", navCliente.VAT_Registration_No)
                BeCliente.Direccion = IIf(navCliente.Address Is Nothing, "", navCliente.Address)
                BeCliente.Activo = True
                BeCliente.User_agr = BeConfigEnc.IdUsuario
                BeCliente.Fec_agr = Date.UtcNow
                BeCliente.User_mod = BeConfigEnc.IdUsuario
                BeCliente.Fec_mod = Date.UtcNow

                Try

                    clsLnCliente.Insertar(BeCliente, lConnection, lTransaction)

                    VContadorBitacoraTomims += 1

                    BeClienteBodega = New clsBeCliente_bodega
                    BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID(lConnection, lTransaction) + 1
                    BeClienteBodega.IdCliente = BeCliente.IdCliente
                    BeClienteBodega.IdBodega = BeConfigEnc.Idbodega
                    BeClienteBodega.Activo = True
                    BeClienteBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                    BeClienteBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                    BeClienteBodega.Fec_agr = Now
                    BeClienteBodega.Fec_mod = Now

                    clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega,
                                                                lConnection,
                                                                lTransaction)

                    Return BeClienteBodega

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               BeCliente.Codigo,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet, lConnectionLog)

                    lblprg.AppendText(String.Format("Error al insertar Cliente: {0}{1}{2}", BeCliente.Codigo, vbNewLine, ex.Message))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End Try

            End If

        Catch ex As Exception
            lblprg.AppendText(String.Format("Error al insertar Cliente a tabla DE TOMWMS: {0}", ex.Message))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Function Insertar_Bodega_Origen_Como_Cliente(ByVal NoCliente As String,
                                                        ByRef lConnection As SqlConnection,
                                                        ByRef lTransaction As SqlTransaction,
                                                        ByRef lConnectionLog As SqlConnection,
                                                        ByRef lblprg As RichTextBox,
                                                        ByRef prg As ProgressBar) As clsBeCliente_bodega

        Insertar_Bodega_Origen_Como_Cliente = Nothing

        Try

            Dim BodSrv As New WSFichaBodegas.Ficha_Bodegas_Service With {
                                                                        .UseDefaultCredentials = UsarCredencialesPorDefecto,
                                                                        .Credentials = CredencialesConexion}
            Dim NavBodegaOrigen As New WSFichaBodegas.Ficha_Bodegas

            NavBodegaOrigen = BodSrv.Read(NoCliente)

            Dim BeCliente As New clsBeCliente
            Dim BeClienteBodega As New clsBeCliente_bodega

            If Not NavBodegaOrigen Is Nothing Then

                lblprg.AppendText(String.Format("Procesando Cliente: {0} ", NavBodegaOrigen.Code, vbNewLine))
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                BeCliente.IdEmpresa = BeConfigEnc.Idempresa
                BeCliente.IdPropietario = BeConfigEnc.IdPropietario
                BeCliente.IdCliente = clsLnCliente.MaxID(lConnection, lTransaction) + 1

                BeCliente.Codigo = NavBodegaOrigen.Code
                BeCliente.Nombre_comercial = NavBodegaOrigen.Name
                BeCliente.Telefono = IIf(NavBodegaOrigen.Phone_No Is Nothing, "", NavBodegaOrigen.Phone_No)
                BeCliente.Nit = IIf(NavBodegaOrigen.Code Is Nothing, "", NavBodegaOrigen.Code)
                BeCliente.Direccion = IIf(NavBodegaOrigen.Address Is Nothing, "", NavBodegaOrigen.Address)
                BeCliente.Nombre_contacto = IIf(NavBodegaOrigen.Contact Is Nothing, "", NavBodegaOrigen.Contact)
                BeCliente.Activo = True
                BeCliente.User_agr = BeConfigEnc.IdUsuario
                BeCliente.Fec_agr = Date.UtcNow
                BeCliente.User_mod = BeConfigEnc.IdUsuario
                BeCliente.Fec_mod = Date.UtcNow

                Try

                    clsLnCliente.Insertar(BeCliente, lConnection, lTransaction)

                    VContadorBitacoraTomims += 1

                    BeClienteBodega = New clsBeCliente_bodega
                    BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID(lConnection, lTransaction) + 1
                    BeClienteBodega.IdCliente = BeCliente.IdCliente
                    BeClienteBodega.IdBodega = BeConfigEnc.Idbodega
                    BeClienteBodega.Activo = True
                    BeClienteBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                    BeClienteBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                    BeClienteBodega.Fec_agr = Now
                    BeClienteBodega.Fec_mod = Now

                    clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega,
                                                                lConnection,
                                                                lTransaction)

                    Return BeClienteBodega

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               BeCliente.Codigo,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet, lConnectionLog)

                    lblprg.AppendText(String.Format("Error al insertar Cliente: {0}{1}{2}", BeCliente.Codigo, vbNewLine, ex.Message))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End Try

            End If

        Catch ex As Exception
            lblprg.AppendText(String.Format("Error al insertar Cliente a tabla DE TOMWMS: {0}", ex.Message))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Function Insertar_Clientes_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(ByRef lblprg As RichTextBox,
                                                                            ByRef prg As ProgressBar,
                                                                            Optional ByVal ForzarEjecucion As Boolean = False,
                                                                            Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_Clientes_Desde_Tabla_Intermedia_A_Tabla_TOMIMS = False

        Dim CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransInterface As SqlTransaction = Nothing
        Dim lFamilias As New List(Of clsBeProducto_familia)
        Dim lClasificacion As New List(Of clsBeProducto_clasificacion)

        VContadorBitacoraTomims = 0

        Try

            lblprg.AppendText("Force_Ejecución: " & ForzarEjecucion)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Cliente") Then

                    lblprg.AppendText("La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    Exit Function

                End If

            End If

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc = 0 'clsLnI_nav_ejecucion_enc.MaxID(CnnLog)
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            'lblprg.AppendText(String.Format("Conectando a BD: {0} Sever: {1}", BD.Instancia.NombreBD, BD.Instancia.Server))
            'lblprg.AppendText(vbNewLine)
            'lblprg.Refresh()

            BeNavEjecucionRes.IdEjecucionRes = 0 'clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeNavEjecRes = BeNavEjecucionRes

            CnnInterface.Open()

            lTransInterface = CnnInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

            'lblprg.AppendText("Iniciando transacción a BD: " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lblprg.AppendText("Consultando WebService de Cliente en: " & My.MySettings.Default.NavSync_WSClientes_Clientes_Service)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Clientes_DesdeWSNav_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If MessageBox.Show("¿Llenar tabla intermedia desde WS?", "Parametro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Not Importar_Clientes_DesdeWSNav_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If

                End If

            End If

            Dim lClientesFromNav As New List(Of clsBeI_nav_cliente)

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Consultando Clientes en tabla intermedia ")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lClientesFromNav = clsLnI_nav_cliente.Get_All(CnnInterface, lTransInterface)

            lblprg.AppendText(String.Format("Clientes en tabla intermedia: {0}", lClientesFromNav.Count))
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If lClientesFromNav.Count > 0 Then

                Dim BeCliente As clsBeCliente = Nothing
                Dim BeClienteBodega As clsBeCliente_bodega = Nothing
                Dim BeClienteExistente As clsBeCliente = Nothing

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, CnnInterface, lTransInterface)

                If BeConfigEnc Is Nothing Then
                    If BD.Instancia.IdConfiguracionInterface = 0 Then
                        Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique el el conn.ini que se especificó el identificador de configuración para la interface.")
                    Else
                        Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique en la bd que existe el registro asociado al identificador de inteface: " & BD.Instancia.IdConfiguracionInterface)
                    End If
                End If

                prg.Maximum = lClientesFromNav.Count

                Dim vContador As Integer = 0

                prg.Value = 0

                lblprg.AppendText("********** TRASLADANDO DOCUMENTO A TOMWMS ********** ")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                For Each navCliente As clsBeI_nav_cliente In lClientesFromNav

                    BeCliente = New clsBeCliente
                    BeClienteExistente = New clsBeCliente

                    BeClienteExistente = clsLnCliente.Existe(navCliente.No, CnnInterface, lTransInterface)

                    lblprg.AppendText(String.Format("Procesando Cliente: {0}", navCliente.No))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    prg.Value = vContador

                    vContador += 1

                    If Not BeClienteExistente Is Nothing Then

                        BeCliente.IdEmpresa = BeConfigEnc.Idempresa
                        BeCliente.IdPropietario = BeConfigEnc.IdPropietario
                        BeCliente.IdCliente = BeClienteExistente.IdCliente
                        BeCliente.Codigo = navCliente.No
                        BeCliente.Nombre_comercial = navCliente.Name
                        BeCliente.Telefono = navCliente.Phone_No
                        BeCliente.Nit = navCliente.VAT_Registratrion_No
                        BeCliente.Direccion = navCliente.Adress
                        BeCliente.Nombre_contacto = navCliente.ContactName
                        BeCliente.Activo = True
                        BeCliente.IdTipoCliente = 1

                        Try
                            clsLnCliente.Actualizar(BeCliente, CnnInterface, lTransInterface)
                        Catch ex As Exception
                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                               BeCliente.Codigo,
                                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                               BeConfigDet.Idnavconfigdet, CnnLog)

                            lblprg.AppendText(String.Format("Error al actualizar Cliente: {0}{1}{2}", BeCliente.Codigo, vbNewLine, ex.Message))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                        End Try

                        VContadorBitacoraTomims += 1

                    Else

                        BeCliente.IdEmpresa = BeConfigEnc.Idempresa
                        BeCliente.IdPropietario = BeConfigEnc.IdPropietario
                        BeCliente.IdCliente = clsLnCliente.MaxID(CnnInterface, lTransInterface) + 1
                        BeCliente.Codigo = navCliente.No
                        BeCliente.Nombre_comercial = navCliente.Name
                        BeCliente.Telefono = navCliente.Phone_No
                        BeCliente.Nit = navCliente.VAT_Registratrion_No
                        BeCliente.Direccion = navCliente.Adress
                        BeCliente.Nombre_contacto = navCliente.ContactName
                        BeCliente.Activo = True
                        BeCliente.User_agr = BeConfigEnc.IdUsuario
                        BeCliente.Fec_agr = Date.UtcNow
                        BeCliente.User_mod = BeConfigEnc.IdUsuario
                        BeCliente.Fec_mod = Date.UtcNow
                        BeCliente.IdTipoCliente = 1

                        Try

                            clsLnCliente.Insertar(BeCliente,
                                                  CnnInterface,
                                                  lTransInterface)

                            VContadorBitacoraTomims += 1

                            BeClienteBodega = New clsBeCliente_bodega
                            BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID(CnnInterface, lTransInterface) + 1
                            BeClienteBodega.IdCliente = BeCliente.IdCliente
                            BeClienteBodega.IdBodega = BeConfigEnc.Idbodega
                            BeClienteBodega.Activo = True
                            BeClienteBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                            BeClienteBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                            BeClienteBodega.Fec_agr = Now
                            BeClienteBodega.Fec_mod = Now

                            clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega,
                                                                        CnnInterface,
                                                                        lTransInterface)

                            '#EJC202303031646: Insertar días por defecto para clientes.
                            If BeConfigEnc.Dias_Vida_Defecto_Perecederos > 0 Then

                                lFamilias = clsLnProducto_familia.Get_All_Filtro(True,
                                                                             BeConfigEnc.IdPropietario,
                                                                             CnnInterface,
                                                                             lTransInterface)

                                lClasificacion = clsLnProducto_clasificacion.Get_All_Filtro(True,
                                                                                            BeConfigEnc.IdPropietario,
                                                                                            CnnInterface,
                                                                                            lTransInterface)

                                If Not lFamilias Is Nothing AndAlso Not lClasificacion Is Nothing Then

                                    Dim BeTiempoCliente As New clsBeCliente_tiempos

                                    For Each F In lFamilias

                                        For Each C In lClasificacion


                                            BeTiempoCliente = New clsBeCliente_tiempos()
                                            BeTiempoCliente.IdTiempoCliente = clsLnCliente_tiempos.MaxID(CnnInterface, lTransInterface) + 1
                                            BeTiempoCliente.IdCliente = BeCliente.IdCliente
                                            BeTiempoCliente.IdFamilia = F.IdFamilia
                                            BeTiempoCliente.IdClasificacion = C.IdClasificacion
                                            BeTiempoCliente.Dias_Local = BeConfigEnc.Dias_Vida_Defecto_Perecederos
                                            BeTiempoCliente.Dias_Exterior = BeConfigEnc.Dias_Vida_Defecto_Perecederos
                                            BeTiempoCliente.User_agr = BeConfigEnc.IdUsuario
                                            BeTiempoCliente.User_mod = BeConfigEnc.IdUsuario
                                            BeTiempoCliente.Fec_agr = Now
                                            BeTiempoCliente.Fec_mod = Now
                                            BeTiempoCliente.Activo = True
                                            clsLnCliente_tiempos.Insertar(BeTiempoCliente, CnnInterface, lTransInterface)

                                        Next

                                    Next

                                End If

                            End If

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                       BeCliente.Codigo,
                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                       BeConfigDet.Idnavconfigdet, CnnLog)

                            lblprg.AppendText(String.Format("Error al insertar Cliente: {0}{1}{2}", BeCliente.Codigo, vbNewLine, ex.Message))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End Try

                        Application.DoEvents()

                    End If

                Next

            End If

            lTransInterface.Commit()

            '#EJC20171107_REF04_0254AM: Desplegar cantidad de registros de Clientes procesados
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** FIN DE INSERCIÓN EN TOMWMS ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Clientes procesados correctamente: {0}", VContadorBitacoraTomims))
            lblprg.AppendText(vbNewLine)
            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
            lblprg.AppendText(String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
            BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTomims

            If VContadorBitacoraIntermedia = VContadorBitacoraTomims Then
                BeNavEjecucionRes.Exitosa = True
            Else
                BeNavEjecucionRes.Exitosa = False
            End If

            '#EJC20171006_0106AM: Se quitó la tnrasacción porque se mandaba la de la interface.
            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)

        Catch ex As Exception
            If Not lTransInterface Is Nothing Then lTransInterface.Rollback()
            lblprg.AppendText(String.Format("Error al insertar Cliente a tabla DE TOMWMS: {0}", ex.Message))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
            prg.Value = 0
            prg.Visible = False
        End Try

    End Function

End Class

