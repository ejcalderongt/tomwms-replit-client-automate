Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsSyncERPCliente : Inherits clsInterfaceBase
    Implements IDisposable

    Shared VContadorBitacoraTomims As Integer = 0
    Shared VContadorBitacoraIntermedia As Integer = 0
    Dim lClientes As New List(Of clsBeCEALSA_clientes)

    'Public Sub Dispose() Implements IDisposable.Dispose
    'End Sub

    'Private Shared oCompany As Company
    Private Shared lRetCode, lErrCode As Long
    Private Shared sErrMsg As String = ""

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing
    Private disposedValue As Boolean

    Public Function Importar_Clientes_Desde_ERP_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                       ByRef prg As ProgressBar,
                                                                       ByRef cnnLog As SqlConnection) As Boolean
        Importar_Clientes_Desde_ERP_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** PROCESANDO DOCUMENTO EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Consultando clientes nuevos en ERP.")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            lClientes = clsLnCEALSA_clientes.Get_All_Pendientes_De_Sincronizacion()

            BeNavEjecucionRes.Registros_ws = lClientes.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Application.DoEvents()

            lblprg.AppendText(String.Format("clientes encontrados en WS: {0} ", lClientes.Count & vbNewLine))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            prg.Maximum = lClientes.Count

            Dim vContador As Integer = 0

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If cnnLog.State = ConnectionState.Closed Then cnnLog.Open()

            Dim CliWMS As New clsBeI_nav_cliente()

            clsLnI_nav_cliente.Eliminar_Todos(lConnection, lTransaction)

            For Each Cli In lClientes

                Try

                    lblprg.AppendText(String.Format("Procesando Cliente: {0} ({1} de {2})", Cli.Codigo_cliente, vContador + 1, lClientes.Count, vbNewLine))
                    lblprg.AppendText(vbNewLine)
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    CliWMS = New clsBeI_nav_cliente()
                    '#CKFK 20210312 Modifiqué esto para que el IdCliente sea el mismo IdCliente que viene de la tabla del ERP
                    CliWMS.IdCliente = Cli.IdCliente 'clsLnI_nav_cliente.MaxID(lConnection, lTransaction) + 1
                    CliWMS.Codigo_cliente = Cli.Codigo_cliente
                    CliWMS.Nombre_cliente = Cli.Nombre_cliente.TrimEnd()
                    CliWMS.Nit = Cli.Nit
                    CliWMS.Razon_social = Cli.Razon_social
                    CliWMS.Procesado_wms = Cli.Procesado_wms
                    clsLnI_nav_cliente.Insertar(CliWMS, lConnection, lTransaction)


                    lblprg.AppendText(String.Format("Cliente Guardado: {0} ({1} de {2})", CliWMS.Codigo_cliente, vContador + 1, lClientes.Count, vbNewLine))
                    lblprg.AppendText(vbNewLine)
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    VContadorBitacoraIntermedia += 1

                    prg.Value = vContador

                    vContador += 1

                    Application.DoEvents()

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                          Cli.Codigo_cliente,
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          BeConfigDet.Idnavconfigdet, cnnLog)

                    lblprg.AppendText("Error al insertar desde ws a intermedia: " & Cli.Codigo_cliente & vbNewLine &
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

            Importar_Clientes_Desde_ERP_A_TablaIntermedia = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            lblprg.AppendText(String.Format("Error al insertar cliente desde ws a intermedia: {0}{1}", vbNewLine, ex.Message))

            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If Not lConnection Is Nothing Then
                If lConnection.State = ConnectionState.Open Then lConnection.Close()
                lConnection.Dispose()
            End If
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Function Insertar_Bodega_Origen_Como_Proveedor(ByVal NoProveedor As String,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef lTransaction As SqlTransaction,
                                                 ByRef lConnectionLog As SqlConnection,
                                                 ByRef lblprg As RichTextBox,
                                                 ByRef prg As ProgressBar) As clsBeProveedor_bodega

        Insertar_Bodega_Origen_Como_Proveedor = Nothing

        Try

            Dim BeProveedor As New clsBeProveedor
            Dim BeProveedorBodega As New clsBeProveedor_bodega
            Dim NavBodegaOrigen As New Object
            'GT25012022: se trae la lista de bodegas (fiscal/general) porque BeConfigEnc solo maneja 1 bodega.
            Dim lBodegas As New List(Of clsBeBodega)
            lBodegas = clsLnBodega.GetAll(lConnection, lTransaction)

            If Not NavBodegaOrigen Is Nothing Then

                lblprg.AppendText(String.Format("Procesando Proveedor: {0} ", NavBodegaOrigen.Code, vbNewLine))
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
                BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
                BeProveedor.IdProveedor = clsLnProveedor.MaxID(lConnection, lTransaction) + 1
                BeProveedor.Codigo = NavBodegaOrigen.Code
                BeProveedor.Nombre = NavBodegaOrigen.Name
                BeProveedor.Telefono = IIf(NavBodegaOrigen.Phone_No Is Nothing, "", NavBodegaOrigen.Phone_No)
                BeProveedor.Nit = IIf(NavBodegaOrigen.Code Is Nothing, "", NavBodegaOrigen.Code)
                BeProveedor.Direccion = IIf(NavBodegaOrigen.Address Is Nothing, "", NavBodegaOrigen.Address)
                BeProveedor.Contacto = IIf(NavBodegaOrigen.Contact Is Nothing, "", NavBodegaOrigen.Contact)
                BeProveedor.Activo = True
                BeProveedor.User_agr = BeConfigEnc.IdUsuario
                BeProveedor.Fec_agr = Date.UtcNow
                BeProveedor.User_mod = BeConfigEnc.IdUsuario
                BeProveedor.Fec_mod = Date.UtcNow

                Try

                    clsLnProveedor.Insertar(BeProveedor, lConnection, lTransaction)

                    VContadorBitacoraTomims += 1

                    BeProveedorBodega = New clsBeProveedor_bodega

                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                    'BeProveedorBodega.IdBodega = BeConfigEnc.Idbodega 'el valor de la bodega se obtiene en la iteración
                    BeProveedorBodega.Activo = True
                    BeProveedorBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                    BeProveedorBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                    BeProveedorBodega.Fec_agr = Now
                    BeProveedorBodega.Fec_mod = Now

                    'GT26012022: itero lista de bodegas, sino existe el proveedor, se agrega
                    For Each bodega In lBodegas

                        BeProveedorBodega.IdBodega = bodega.IdBodega

                        lblprg.AppendText(String.Format("Procesando Bodega para proveedor: {0} ", bodega.IdBodega, vbNewLine))
                        lblprg.AppendText(vbNewLine)
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()


                        If Not clsLnProveedor_bodega.Exist_By_IdBodega_And_IdProveedor(BeProveedor.IdProveedor, bodega.IdBodega,
                                                                                            lConnection,
                                                                                            lTransaction) Then

                            BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConnection, lTransaction) + 1
                            clsLnProveedor_bodega.InsertarFromInterface(BeProveedorBodega, lConnection, lTransaction)

                            lblprg.AppendText(String.Format("Procesando Proveedor-Bodega: {0} ", BeProveedorBodega.IdAsignacion, vbNewLine))
                            lblprg.AppendText(vbNewLine)
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End If

                    Next

                    Return BeProveedorBodega

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               BeProveedor.Codigo,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet, lConnectionLog)

                    lblprg.AppendText(String.Format("Error al insertar proveedor: {0}{1}{2}", BeProveedor.Codigo, vbNewLine, ex.Message))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End Try

            End If

        Catch ex As Exception
            lblprg.AppendText(String.Format("Error al insertar Proveedor a tabla de TOMIMS: {0}", ex.Message))
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

        Dim lConnectionWMS As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransactionWMS As SqlTransaction = Nothing
        Dim CnnLogWMS As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)

        Dim lConnectionERP As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))
        Dim lTransactionERP As SqlTransaction = Nothing

        VContadorBitacoraTomims = 0

        Try

            lblprg.AppendText("Force_Ejecución: " & ForzarEjecucion)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Clientes") Then

                    lblprg.AppendText("La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    Exit Function

                End If

            End If

            CnnLogWMS.Open()

            BeNavEjecucionEnc.IdEjecucionEnc = clsLnI_nav_ejecucion_enc.MaxID(CnnLogWMS)
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLogWMS)

            BeNavEjecucionRes.IdEjecucionRes = clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLogWMS) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLogWMS)

            BeNavEjecRes = BeNavEjecucionRes

            '#EJC20210306: Abrir conexión a BD WMS
            lConnectionWMS.Open() : lTransactionWMS = lConnectionWMS.BeginTransaction(IsolationLevel.ReadCommitted)

            '#EJC20210306: Abrir conexión a BD ERP
            lConnectionERP.Open() : lTransactionERP = lConnectionERP.BeginTransaction(IsolationLevel.ReadCommitted)

            'lblprg.AppendText("Iniciando transacción a BD: " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, lConnectionWMS, lTransactionWMS)

            If BeConfigEnc Is Nothing Then
                Throw New Exception("No está definida la configuración de la interface para el identificador: " & BD.Instancia.IdConfiguracionInterface)
            End If

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Clientes_Desde_ERP_A_TablaIntermedia(lblprg, prg, CnnLogWMS) Then
                    Exit Function
                End If

            Else

                If MessageBox.Show("¿Llenar tabla intermedia desde WS?", "Parametro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Not Importar_Clientes_Desde_ERP_A_TablaIntermedia(lblprg, prg, CnnLogWMS) Then
                        Exit Function
                    End If

                End If

            End If

            Dim lClientesFromERP As New List(Of clsBeI_nav_cliente)

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Consultando clientes en tabla intermedia ")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lClientesFromERP = clsLnI_nav_cliente.Get_All_Pendientes_De_Procesar(lblprg, prg, lConnectionERP, lTransactionERP)

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Clientes en tabla intermedia: {0}", lClientesFromERP.Count))
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If lClientesFromERP.Count > 0 Then

                Dim BePropietarioExistente As clsBePropietarios = Nothing
                Dim BePropietario As clsBePropietarios = Nothing
                Dim BePropietarioBodega As clsBePropietario_bodega = Nothing
                Dim BeCliente As clsBeCliente = Nothing
                Dim BeConsolidador As New clsBeConsolidador()
                Dim BeClienteBodega As clsBeCliente_bodega = Nothing
                Dim BeClienteExistente As clsBeCliente = Nothing
                Dim BeConsolidadorExistente As clsBeConsolidador = Nothing
                Dim vCantidadRegistrosAcuerdoComercial As Integer = 0
                Dim vEsConsolidador As Boolean = False
                ' Dim lAcuerdosPorClienteExistentes As New List(Of clsBeI_nav_acuerdo_det)
                'GT25012022: se trae la lista de bodegas (fiscal/general) porque BeConfigEnc solo maneja 1 bodega.
                Dim lBodegas As New List(Of clsBeBodega)
                lBodegas = clsLnBodega.GetAll(lConnectionWMS, lTransactionWMS)

                prg.Maximum = lClientesFromERP.Count

                Dim vContador As Integer = 0

                prg.Value = 0

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("********** INICIO DE INSERCIÓN EN TABLA DE TOMIMS ********** ")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                For Each ERPCliente As clsBeI_nav_cliente In lClientesFromERP

                    lblprg.AppendText(vbNewLine)
                    lblprg.AppendText(String.Format("Procesando Cliente: {0} ({1} de {2})", ERPCliente.Codigo_cliente, vContador + 1, lClientesFromERP.Count))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    prg.Value = vContador

                    vContador += 1

#Region "Insertar-Actualizar Consolidadores y sus acuerdos comerciales"

                    BeCliente = New clsBeCliente()
                    BeClienteExistente = New clsBeCliente
                    BeClienteExistente = clsLnCliente.Existe(ERPCliente.Codigo_cliente, lConnectionWMS, lTransactionWMS)
                    BeConsolidadorExistente = clsLnConsolidador.Existe(ERPCliente.Codigo_cliente, lConnectionWMS, lTransactionWMS)
                    vCantidadRegistrosAcuerdoComercial = ERPCliente.lAcuerdosDetERP.Count()
                    vEsConsolidador = (vCantidadRegistrosAcuerdoComercial > 0)

                    '#CKFK 20210312 08:37 AM Los clientes se van a insertar como consolidadores cuando tengan detalle de acuerdos comerciales
                    If vEsConsolidador Then

#Region "Procesa Consolidador"

#Region "Actualiza Consolidador"

                        If Not BeConsolidadorExistente Is Nothing Then

                            BeConsolidador = New clsBeConsolidador()
                            BeConsolidador.IdEmpresa = BeConfigEnc.Idempresa
                            BeConsolidador.Idconsolidador = BeConsolidadorExistente.Idconsolidador
                            BeConsolidador.Codigo = ERPCliente.Codigo_cliente
                            BeConsolidador.Nom_comercial = ERPCliente.Nombre_cliente
                            BeConsolidador.Telefono = ""
                            BeConsolidador.Nit = ERPCliente.Nit
                            BeConsolidador.Direccion = ""
                            BeConsolidador.Activo = True
                            BeConsolidador.User_mod = BeConfigEnc.IdUsuario
                            BeConsolidador.Fec_mod = Date.UtcNow
                            clsLnConsolidador.Actualizar(BeConsolidador, lConnectionWMS, lTransactionWMS)

                        Else

#Region "Inserta Consolidador"

                            BeConsolidador = New clsBeConsolidador()
                            BeConsolidador.IdEmpresa = BeConfigEnc.Idempresa
                            BeConsolidador.Idconsolidador = ERPCliente.Codigo_cliente
                            BeConsolidador.Codigo = ERPCliente.Codigo_cliente
                            BeConsolidador.Nom_comercial = ERPCliente.Nombre_cliente
                            BeConsolidador.Telefono = ""
                            BeConsolidador.Nit = ERPCliente.Nit
                            BeConsolidador.Direccion = ""
                            BeConsolidador.Activo = True
                            BeConsolidador.User_agr = BeConfigEnc.IdUsuario
                            BeConsolidador.Fec_agr = Date.UtcNow
                            BeConsolidador.User_mod = BeConfigEnc.IdUsuario
                            BeConsolidador.Fec_mod = Date.UtcNow
                            clsLnConsolidador.Insertar(BeConsolidador, lConnectionWMS, lTransactionWMS)
#End Region

                        End If

#End Region

#End Region


                        VContadorBitacoraTomims += 1

                    End If

                    Application.DoEvents()

#End Region

#Region "Insertar-Actualizar Propietario"

                    '#CKFK 20210312: Quité esta condición de si el cliente es consolidador o no
                    '#EJC20210305: Si no es consolidador, se inserta o valida como propietario.
                    If Not vEsConsolidador Then
                    End If

                    lblprg.AppendText(String.Format("Procesando Propietario: {0}", ERPCliente.Codigo_cliente))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    BePropietario = New clsBePropietarios()
                    BePropietarioExistente = New clsBePropietarios()
                    BePropietarioExistente = clsLnPropietarios.Existe(ERPCliente.Codigo_cliente, lConnectionWMS, lTransactionWMS)

                    If Not BeClienteExistente Is Nothing Then

#Region "Actualiza Cliente"

                        BeCliente.IdEmpresa = BeConfigEnc.Idempresa
                        BeCliente.IdPropietario = BePropietarioExistente.IdPropietario
                        BeCliente.Codigo = ERPCliente.Codigo_cliente
                        BeCliente.Nombre_comercial = ERPCliente.Nombre_cliente
                        BeCliente.Nit = ERPCliente.Nit

                        Try

                            clsLnCliente.Actualizar(BeCliente, lConnectionWMS, lTransactionWMS)

                        Catch ex As Exception
                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                            BeCliente.Codigo,
                                                                            BeNavEjecucionEnc.IdEjecucionEnc,
                                                                            BeConfigDet.Idnavconfigdet, CnnLogWMS)

                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText(String.Format("Error al actualizar cliente: {0}{1}{2}", BeCliente.Codigo, vbNewLine, ex.Message))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                        End Try

#End Region

                        VContadorBitacoraTomims += 1

                    End If


                    If Not BePropietarioExistente Is Nothing Then

                        BePropietario.IdEmpresa = BeConfigEnc.Idempresa
                        BePropietario.IdPropietario = BePropietarioExistente.IdPropietario
                        BePropietario.Codigo = ERPCliente.Codigo_cliente
                        BePropietario.Nombre_comercial = ERPCliente.Nombre_cliente
                        BePropietario.Contacto = "NDF"
                        BePropietario.NIT = ERPCliente.Nit
                        BePropietario.IdTipoActualizacionCosto = 1
                        BePropietario.Activo = True
                        BePropietario.User_mod = BeConfigEnc.IdUsuario

                        Try

                            clsLnPropietarios.Actualizar(BePropietario, lConnectionWMS, lTransactionWMS)

                        Catch ex As Exception
                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                BePropietario.Codigo,
                                                                                BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                BeConfigDet.Idnavconfigdet, CnnLogWMS)

                            lblprg.AppendText(String.Format("Error al actualizar propietario: {0}{1}{2}", BeCliente.Codigo, vbNewLine, ex.Message))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                        End Try

                        VContadorBitacoraTomims += 1

                    Else

                        BePropietario.IdEmpresa = BeConfigEnc.Idempresa
                        '#CKFK 20210312 Modifiqué esto para que el IdPropietario sea el mismo codigo
                        BePropietario.IdPropietario = ERPCliente.Codigo_cliente 'clsLnPropietarios.MaxID(lConnectionWMS, lTransactionWMS)
                        BePropietario.Codigo = ERPCliente.Codigo_cliente
                        BePropietario.Nombre_comercial = ERPCliente.Nombre_cliente
                        BePropietario.Contacto = "NDF"
                        BePropietario.Telefono = ""
                        BePropietario.NIT = ERPCliente.Nit
                        BePropietario.Direccion = ""
                        BePropietario.Sistema = False
                        BePropietario.Activo = True
                        BePropietario.User_agr = BeConfigEnc.IdUsuario
                        BePropietario.Fec_agr = Date.UtcNow
                        BePropietario.User_mod = BeConfigEnc.IdUsuario
                        BePropietario.Fec_mod = Date.UtcNow
                        BePropietario.IdTipoActualizacionCosto = 1 'si nuevo costo es mayor (>)

                        Try

                            '#EJC20210305: Guardar e insertar valores por defecto (UM, Clas, Fam)
                            clsLnPropietarios.Insertar_Nuevo_Propietario(BePropietario,
                                                                         BeConfigEnc.Idbodega,
                                                                         True,
                                                                         lConnectionWMS,
                                                                         lTransactionWMS)

                            VContadorBitacoraTomims += 1


                            lblprg.AppendText(String.Format("Propietario procesado : {0}", BePropietario.Nombre_comercial))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()


                            BePropietarioBodega = New clsBePropietario_bodega()
                            'BePropietarioBodega.IdPropietarioBodega = BePropietario.IdPropietario 'clsLnPropietario_bodega.MaxID(lConnectionWMS, lTransactionWMS)
                            BePropietarioBodega.IdPropietario = BePropietario.IdPropietario
                            'BePropietarioBodega.IdBodega = BeConfigEnc.Idbodega 'IdBodega se determina en la existencia sobre la iteración
                            BePropietarioBodega.Activo = True
                            BePropietarioBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                            BePropietarioBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                            BePropietarioBodega.Fec_agr = Now
                            BePropietarioBodega.Fec_mod = Now

                            'GT25012022: iteramos las bodegas existentes para insertar sino existe
                            'Se cambia BeConfigEnc.Idbodega por bodega, porque solo valida en 1 bodega, la iteración lo hace en las existentes
                            For Each bodega In lBodegas

                                lblprg.AppendText(String.Format("Procesando Propietario para bodega: {0}", bodega.Nombre))
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                                If Not clsLnPropietario_bodega.Existe_IdPropietario_And_IdBodega(BePropietario.IdPropietario,
                                                                                            bodega.IdBodega,
                                                                                            lConnectionWMS,
                                                                                            lTransactionWMS) Then

                                    'If clsLnPropietario_bodega.Existe_IdPropietarioBodega(BePropietarioBodega.IdPropietarioBodega, lConnectionWMS, lTransactionWMS) Then
                                    'End If

                                    BePropietarioBodega.IdBodega = bodega.IdBodega
                                    BePropietarioBodega.IdPropietarioBodega = clsLnPropietario_bodega.MaxID(lConnectionWMS, lTransactionWMS)
                                    clsLnPropietario_bodega.Insertar(BePropietarioBodega, lConnectionWMS, lTransactionWMS)

                                End If

                            Next

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                               BePropietario.Codigo,
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet, CnnLogWMS)

                            lblprg.AppendText(String.Format("Error al insertar propietario: {0}{1}{2}", BeCliente.Codigo, vbNewLine, ex.Message))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End Try

                        Application.DoEvents()

                    End If

#End Region

                    '#EJC20210305: Marcar en tabla WMS como procesado.
                    ERPCliente.Procesado_wms = True
                    clsLnI_nav_cliente.Actualizar_Procesado_WMS(ERPCliente, lConnectionWMS, lTransactionWMS)

                    '#EJC20210305: Marcar en tabla ERP como procesado.
                    clsLnCEALSA_clientes.Actualizar_Procesado_WMS(ERPCliente, lConnectionERP, lTransactionERP)

                Next

            End If

            Crea_Clientes_No_Existentes(lblprg, prg, lConnectionWMS, lTransactionWMS)

            Crea_Propietarios_No_Existentes(lblprg, prg, lConnectionWMS, lTransactionWMS)

            Crea_Proveedores_No_Existentes(lblprg, prg, lConnectionWMS, lTransactionWMS)

            Asignacion_Clientes_Por_Bodega(lblprg, prg, lConnectionWMS, lTransactionWMS)

            Crea_Clientes_Y_Provedores_Como_Bodegas_Virtuales(lblprg, prg, lConnectionWMS, lTransactionWMS)

            lTransactionWMS.Commit() : lTransactionERP.Commit()

            lblprg.AppendText(vbNewLine)
            '#EJC20171107_REF04_0254AM: Desplegar cantidad de registros de proveedores procesados
            lblprg.AppendText("********** FIN DE INSERCIÓN EN TABLA DE TOMIMS ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Clientes/Propietarios procesados correctamente: {0}", VContadorBitacoraTomims))
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
            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLogWMS)

        Catch ex As Exception

            lblprg.AppendText(String.Format("Error al insertar propietario/cliente a tabla de TOMIMS: {0}", ex.Message))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If Not lTransactionWMS Is Nothing Then lTransactionWMS.Rollback()
            If Not lTransactionERP Is Nothing Then lTransactionERP.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally

            If lConnectionWMS.State = ConnectionState.Open Then lConnectionWMS.Close()
            If lConnectionERP.State = ConnectionState.Open Then lConnectionERP.Close()
            If CnnLogWMS.State = ConnectionState.Open Then CnnLogWMS.Close()
            prg.Value = 0
            prg.Visible = False

        End Try

    End Function

    Private Sub Asignacion_Clientes_Por_Bodega(ByRef lblprg As RichTextBox,
                                                   ByRef prg As ProgressBar,
                                                   ByVal lConnection As SqlConnection,
                                                  ByVal lTransaction As SqlTransaction)

        Try
            Dim ListaClientes As New List(Of clsBeCliente)
            Dim bodegas As New List(Of clsBeBodega)
            bodegas = clsLnBodega.GetAll(lConnection, lTransaction)

            Dim correlativo As Integer = 0

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Procesando clientes sin bodega asignada.")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            For Each bodega As clsBeBodega In bodegas

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("Procesando Bodega: " & bodega.Nombre)
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                ListaClientes = New List(Of clsBeCliente)
                ListaClientes = clsLnCliente.Get_Clientes_No_Existentes_En_Bodega(bodega.IdBodega, lConnection, lTransaction)

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("Clientes a procesar: " & ListaClientes.Count)
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                Dim BeClienteBodega As New clsBeCliente_bodega()
                correlativo = 1

                For Each cliente As clsBeCliente In ListaClientes

                    lblprg.AppendText(vbNewLine)
                    lblprg.AppendText("Procesando correlativo:" & correlativo & " y cliente: " & cliente.Nombre_comercial)
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    BeClienteBodega = New clsBeCliente_bodega()
                    BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID(lConnection, lTransaction) + 1
                    BeClienteBodega.IdCliente = cliente.IdCliente
                    BeClienteBodega.IdBodega = bodega.IdBodega
                    BeClienteBodega.Activo = True
                    BeClienteBodega.User_agr = cliente.User_agr '1 Esto debería ser parametrizable?
                    BeClienteBodega.User_mod = cliente.User_agr  '1 Esto debería ser parametrizable?
                    BeClienteBodega.Fec_agr = Now
                    BeClienteBodega.Fec_mod = Now

                    clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega, lConnection, lTransaction)

                    correlativo += 1
                Next

            Next


        Catch ex As Exception

        End Try

    End Sub

    Public Shared Function Marcar_Cliente_Sincronizado_En_ERP(ByVal pCodigoCliente As String) As Boolean

        Marcar_Cliente_Sincronizado_En_ERP = False

        Try

            'Actualizar bandera en ERP.
            Marcar_Cliente_Sincronizado_En_ERP = (clsLnCEALSA_clientes.Actualizar_Bandera(pCodigoCliente) > 0)

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Friend Shared Function Insertar_Cliente_Single(buy_From_Vendor_No As String, cnnInterface As SqlConnection, lTransInterface As SqlTransaction, cnnLog As SqlConnection, lblprg As RichTextBox, prg As ProgressBar) As clsBeProveedor_bodega
        Throw New NotImplementedException()
    End Function

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
            ' TODO: set large fields to null
            disposedValue = True
        End If
    End Sub

    ' ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
    ' Protected Overrides Sub Finalize()
    '     ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub

    Public Function Insertar_Clientes_Desde_Tabla_Intermedia_A_Tabla_TOMIMS_Original(ByRef lblprg As RichTextBox,
                                                                            ByRef prg As ProgressBar,
                                                                            Optional ByVal ForzarEjecucion As Boolean = False,
                                                                            Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_Clientes_Desde_Tabla_Intermedia_A_Tabla_TOMIMS_Original = False

        Dim lConnectionWMS As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransactionWMS As SqlTransaction = Nothing
        Dim CnnLogWMS As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)

        Dim lConnectionERP As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))
        Dim lTransactionERP As SqlTransaction = Nothing

        VContadorBitacoraTomims = 0

        Try

            Dim bodegas As New List(Of clsBeBodega)
            bodegas = clsLnBodega.GetAll(lConnectionWMS, lTransactionWMS)

            lblprg.AppendText("Force_Ejecución: " & ForzarEjecucion)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Clientes") Then

                    lblprg.AppendText("La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    Exit Function

                End If

            End If

            CnnLogWMS.Open()

            BeNavEjecucionEnc.IdEjecucionEnc = clsLnI_nav_ejecucion_enc.MaxID(CnnLogWMS)
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLogWMS)

            BeNavEjecucionRes.IdEjecucionRes = clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLogWMS) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLogWMS)

            BeNavEjecRes = BeNavEjecucionRes

            '#EJC20210306: Abrir conexión a BD WMS
            lConnectionWMS.Open() : lTransactionWMS = lConnectionWMS.BeginTransaction(IsolationLevel.ReadCommitted)

            '#EJC20210306: Abrir conexión a BD ERP
            lConnectionERP.Open() : lTransactionERP = lConnectionERP.BeginTransaction(IsolationLevel.ReadCommitted)

            'lblprg.AppendText("Iniciando transacción a BD: " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, lConnectionWMS, lTransactionWMS)

            If BeConfigEnc Is Nothing Then
                Throw New Exception("No está definida la configuración de la interface para el identificador: " & BD.Instancia.IdConfiguracionInterface)
            End If

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Clientes_Desde_ERP_A_TablaIntermedia(lblprg, prg, CnnLogWMS) Then
                    Exit Function
                End If

            Else

                If MessageBox.Show("¿Llenar tabla intermedia desde WS?", "Parametro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Not Importar_Clientes_Desde_ERP_A_TablaIntermedia(lblprg, prg, CnnLogWMS) Then
                        Exit Function
                    End If

                End If

            End If

            Dim lClientesFromERP As New List(Of clsBeI_nav_cliente)

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Consultando clientes en tabla intermedia ")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lClientesFromERP = clsLnI_nav_cliente.Get_All_Pendientes_De_Procesar(lblprg, prg, lConnectionERP, lTransactionERP)

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Clientes en tabla intermedia: {0}", lClientesFromERP.Count))
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If lClientesFromERP.Count > 0 Then

                Dim BePropietarioExistente As clsBePropietarios = Nothing
                Dim BePropietario As clsBePropietarios = Nothing
                Dim BePropietarioBodega As clsBePropietario_bodega = Nothing
                Dim BeCliente As clsBeCliente = Nothing
                Dim BeConsolidador As New clsBeConsolidador()
                Dim BeClienteBodega As clsBeCliente_bodega = Nothing
                Dim BeClienteExistente As clsBeCliente = Nothing
                Dim BeConsolidadorExistente As clsBeConsolidador = Nothing
                Dim vCantidadRegistrosAcuerdoComercial As Integer = 0
                Dim vEsConsolidador As Boolean = False
                Dim lAcuerdosPorClienteExistentes As New List(Of clsBeI_nav_acuerdo_det)

                prg.Maximum = lClientesFromERP.Count

                Dim vContador As Integer = 0

                prg.Value = 0

                lblprg.AppendText("********** INICIO DE INSERCIÓN EN TABLA DE TOMIMS ********** ")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                For Each ERPCliente As clsBeI_nav_cliente In lClientesFromERP

                    lblprg.AppendText(vbNewLine)
                    lblprg.AppendText(String.Format("Procesando Cliente: {0} ({1} de {2})", ERPCliente.Codigo_cliente, vContador + 1, lClientesFromERP.Count))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    prg.Value = vContador

                    vContador += 1

#Region "Insertar-Actualizar Consolidadores y sus acuerdos comerciales"

                    BeCliente = New clsBeCliente()
                    BeClienteExistente = New clsBeCliente
                    BeClienteExistente = clsLnCliente.Existe(ERPCliente.Codigo_cliente, lConnectionWMS, lTransactionWMS)
                    BeConsolidadorExistente = clsLnConsolidador.Existe(ERPCliente.Codigo_cliente, lConnectionWMS, lTransactionWMS)
                    vCantidadRegistrosAcuerdoComercial = ERPCliente.lAcuerdosDetERP.Count()
                    vEsConsolidador = (vCantidadRegistrosAcuerdoComercial > 0)

                    '#CKFK 20210312 08:37 AM Los clientes se van a insertar como consolidadores cuando tengan detalle de acuerdos comerciales
                    If vEsConsolidador Then

#Region "Procesa Consolidador"

#Region "Actualiza Consolidador"

                        If Not BeConsolidadorExistente Is Nothing Then

                            BeConsolidador = New clsBeConsolidador()
                            BeConsolidador.IdEmpresa = BeConfigEnc.Idempresa
                            BeConsolidador.Idconsolidador = BeConsolidadorExistente.Idconsolidador
                            BeConsolidador.Codigo = ERPCliente.Codigo_cliente
                            BeConsolidador.Nom_comercial = ERPCliente.Nombre_cliente
                            BeConsolidador.Telefono = ""
                            BeConsolidador.Nit = ERPCliente.Nit
                            BeConsolidador.Direccion = ""
                            BeConsolidador.Activo = True
                            BeConsolidador.User_mod = BeConfigEnc.IdUsuario
                            BeConsolidador.Fec_mod = Date.UtcNow
                            clsLnConsolidador.Actualizar(BeConsolidador, lConnectionWMS, lTransactionWMS)

                        Else

#Region "Inserta Consolidador"

                            BeConsolidador = New clsBeConsolidador()
                            BeConsolidador.IdEmpresa = BeConfigEnc.Idempresa
                            BeConsolidador.Idconsolidador = ERPCliente.Codigo_cliente
                            BeConsolidador.Codigo = ERPCliente.Codigo_cliente
                            BeConsolidador.Nom_comercial = ERPCliente.Nombre_cliente
                            BeConsolidador.Telefono = ""
                            BeConsolidador.Nit = ERPCliente.Nit
                            BeConsolidador.Direccion = ""
                            BeConsolidador.Activo = True
                            BeConsolidador.User_agr = BeConfigEnc.IdUsuario
                            BeConsolidador.Fec_agr = Date.UtcNow
                            BeConsolidador.User_mod = BeConfigEnc.IdUsuario
                            BeConsolidador.Fec_mod = Date.UtcNow
                            clsLnConsolidador.Insertar(BeConsolidador, lConnectionWMS, lTransactionWMS)
#End Region

                        End If

#End Region

#End Region


                        VContadorBitacoraTomims += 1

                    End If

                    Application.DoEvents()

                    '#GT02052024: HAY QUE ACTUALIZAR ESTA PARTE CON LAS TABLAS AJUSTADAS DE I_NAV ACUERDOS COMERCIALES ENCABEZADO Y DETALLE.
                    'lAcuerdosPorClienteExistentes = clsLnI_nav_acuerdo_det.Get_All_By_IdCliente(ERPCliente.Codigo_cliente, lConnectionWMS, lTransactionWMS)
                    'Dim BeAcuerdoComercial As New clsBeI_nav_acuerdo_det()
                    'Dim lMaxIdAcuerdoDet As Integer = clsLnI_nav_acuerdo_det.MaxID(lConnectionWMS, lTransactionWMS) + 1
                    'Dim vIndiceAcuerdoExistente As Integer = 0
                    'Dim lAcuerdosDetByAcuerdoEnc As New List(Of clsBeI_nav_detacuerdoscomerciales)

                    ''#EJC20210305: INSERTAR ENCABEZADO DE ACUERDOS COMERCIALES...
                    'For Each ACEnc In ERPCliente.lAcuerdosEncERP

                    '    If Not clsLnI_nav_acuerdo_enc.Existe_Acuerdo(ACEnc.IdCliente, ACEnc.IdAcuerdo, lConnectionWMS, lTransactionWMS) Then

                    '        lblprg.AppendText(vbNewLine)
                    '        lblprg.AppendText(String.Format("Insertando Acuerdo (Enc): {0}", ACEnc.Codigo_acuerdo & " " & ACEnc.Descripcion))
                    '        lblprg.AppendText(vbNewLine)
                    '        lblprg.Refresh()
                    '        lblprg.SelectionStart = lblprg.TextLength
                    '        lblprg.ScrollToCaret()

                    '        ACEnc.Procesado_wms = True

                    '        clsLnI_nav_acuerdo_enc.Insertar(ACEnc, lConnectionWMS, lTransactionWMS)

                    '    End If

                    '    lAcuerdosDetByAcuerdoEnc = ERPCliente.lAcuerdosDetERP.FindAll(Function(x) x.Codacuerdo = ACEnc.Codigo_acuerdo)

                    '    If lAcuerdosDetByAcuerdoEnc IsNot Nothing Then

                    '        '#EJC20210305: INSERTAR DETALLE DE ACUERDO COMERCIAL...
                    '        For Each AC In lAcuerdosDetByAcuerdoEnc

                    '            If lAcuerdosDetByAcuerdoEnc.Count > 0 Then

                    '                lblprg.AppendText(vbNewLine)
                    '                lblprg.AppendText(String.Format("Procesando [Acuerdo: {0}] ---> [Servicio: {1}]", ACEnc.Codigo_acuerdo & " " & ACEnc.Descripcion, AC.Servicio))
                    '                lblprg.AppendText(vbNewLine)
                    '                lblprg.Refresh()
                    '                lblprg.SelectionStart = lblprg.TextLength
                    '                lblprg.ScrollToCaret()

                    '                vIndiceAcuerdoExistente = lAcuerdosPorClienteExistentes.FindIndex(Function(X) X.IdAcuerdo = AC.Codacuerdo AndAlso X.Codigo_producto = AC.Codigo_producto AndAlso X.Corre_detalleacuerdo = AC.Corre_detalleacuerdo AndAlso X.Corre_catalogoproductos = AC.Corre_catalogoproductos)

                    '                If vIndiceAcuerdoExistente <> -1 Then

                    '                    BeAcuerdoComercial = New clsBeI_nav_acuerdo_det()
                    '                    BeAcuerdoComercial.IdAcuerdoDet = lAcuerdosPorClienteExistentes(vIndiceAcuerdoExistente).IdAcuerdoDet
                    '                    BeAcuerdoComercial.IdAcuerdo = AC.Codacuerdo
                    '                    BeAcuerdoComercial.Codigo_producto = AC.Codigo_producto
                    '                    BeAcuerdoComercial.Servicio = AC.Servicio
                    '                    BeAcuerdoComercial.Nemonico = AC.Nemonico
                    '                    BeAcuerdoComercial.Corre_detalleacuerdo = AC.Corre_detalleacuerdo
                    '                    BeAcuerdoComercial.Corre_catalogoproductos = AC.Corre_catalogoproductos
                    '                    BeAcuerdoComercial.Unid_medida = AC.Unid_medida
                    '                    BeAcuerdoComercial.Nombre_unidad = AC.Nombre_unidad
                    '                    BeAcuerdoComercial.Procesado_wms = True
                    '                    BeAcuerdoComercial.Estado = AC.Estado
                    '                    clsLnI_nav_acuerdo_det.Actualizar(BeAcuerdoComercial, lConnectionWMS, lTransactionWMS)

                    '                Else

                    '                    BeAcuerdoComercial = New clsBeI_nav_acuerdo_det()
                    '                    BeAcuerdoComercial.IdAcuerdoDet = lMaxIdAcuerdoDet
                    '                    BeAcuerdoComercial.IdAcuerdo = AC.Codacuerdo
                    '                    BeAcuerdoComercial.Codigo_producto = AC.Codigo_producto
                    '                    BeAcuerdoComercial.Servicio = AC.Servicio
                    '                    BeAcuerdoComercial.Nemonico = AC.Nemonico
                    '                    BeAcuerdoComercial.Corre_detalleacuerdo = AC.Corre_detalleacuerdo
                    '                    BeAcuerdoComercial.Corre_catalogoproductos = AC.Corre_catalogoproductos
                    '                    BeAcuerdoComercial.Unid_medida = AC.Unid_medida
                    '                    BeAcuerdoComercial.Nombre_unidad = AC.Nombre_unidad
                    '                    BeAcuerdoComercial.Procesado_wms = True
                    '                    BeAcuerdoComercial.Estado = AC.Estado

                    '                    Try
                    '                        clsLnI_nav_acuerdo_det.Insertar(BeAcuerdoComercial, lConnectionWMS, lTransactionWMS)
                    '                    Catch ex As Exception
                    '                        lblprg.AppendText(String.Format("Error al insertar detalle de Acuerdo: {0} Servicio {1} {2}Error: {3}  ", ACEnc.Codigo_acuerdo & " " & ACEnc.Descripcion, AC.Servicio, vbNewLine, ex.Message))
                    '                        lblprg.AppendText(vbNewLine)
                    '                        lblprg.Refresh()
                    '                        lblprg.SelectionStart = lblprg.TextLength
                    '                        lblprg.ScrollToCaret()
                    '                    End Try

                    '                    lMaxIdAcuerdoDet += 1

                    '                End If

                    '                '#EJC20210306: Marcar el registro como procesado en el ERP.
                    '                AC.Procesado_wms = True
                    '                clsLnI_nav_detacuerdoscomerciales.Actualizar_Procesado_WMS(AC, lConnectionERP, lTransactionERP)

                    '            End If

                    '        Next

                    '    End If

                    'Next


#End Region

#Region "Insertar-Actualizar Propietario"

                    '#CKFK 20210312: Quité esta condición de si el cliente es consolidador o no
                    '#EJC20210305: Si no es consolidador, se inserta o valida como propietario.
                    If Not vEsConsolidador Then
                    End If

                    lblprg.AppendText(String.Format("Procesando Propietario: {0}", ERPCliente.Codigo_cliente))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    BePropietario = New clsBePropietarios()
                    BePropietarioExistente = New clsBePropietarios()
                    BePropietarioExistente = clsLnPropietarios.Existe(ERPCliente.Codigo_cliente, lConnectionWMS, lTransactionWMS)

                    If Not BeClienteExistente Is Nothing Then

#Region "Actualiza Cliente"

                        BeCliente.IdEmpresa = BeConfigEnc.Idempresa
                        BeCliente.IdPropietario = BePropietarioExistente.IdPropietario
                        BeCliente.Codigo = ERPCliente.Codigo_cliente
                        BeCliente.Nombre_comercial = ERPCliente.Nombre_cliente
                        BeCliente.Nit = ERPCliente.Nit

                        Try

                            clsLnCliente.Actualizar(BeCliente, lConnectionWMS, lTransactionWMS)

                        Catch ex As Exception
                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                            BeCliente.Codigo,
                                                                            BeNavEjecucionEnc.IdEjecucionEnc,
                                                                            BeConfigDet.Idnavconfigdet, CnnLogWMS)

                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText(String.Format("Error al actualizar cliente: {0}{1}{2}", BeCliente.Codigo, vbNewLine, ex.Message))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                        End Try

#End Region

                        VContadorBitacoraTomims += 1

                    End If


                    If Not BePropietarioExistente Is Nothing Then

                        BePropietario.IdEmpresa = BeConfigEnc.Idempresa
                        BePropietario.IdPropietario = BePropietarioExistente.IdPropietario
                        BePropietario.Codigo = ERPCliente.Codigo_cliente
                        BePropietario.Nombre_comercial = ERPCliente.Nombre_cliente
                        BePropietario.Contacto = "NDF"
                        BePropietario.NIT = ERPCliente.Nit
                        BePropietario.IdTipoActualizacionCosto = 1
                        BePropietario.Activo = True
                        BePropietario.User_mod = BeConfigEnc.IdUsuario

                        Try

                            clsLnPropietarios.Actualizar(BePropietario, lConnectionWMS, lTransactionWMS)

                        Catch ex As Exception
                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                BePropietario.Codigo,
                                                                                BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                BeConfigDet.Idnavconfigdet, CnnLogWMS)

                            lblprg.AppendText(String.Format("Error al actualizar propietario: {0}{1}{2}", BeCliente.Codigo, vbNewLine, ex.Message))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                        End Try

                        VContadorBitacoraTomims += 1

                    Else

                        BePropietario.IdEmpresa = BeConfigEnc.Idempresa
                        '#CKFK 20210312 Modifiqué esto para que el IdPropietario sea el mismo codigo
                        BePropietario.IdPropietario = ERPCliente.Codigo_cliente 'clsLnPropietarios.MaxID(lConnectionWMS, lTransactionWMS)
                        BePropietario.Codigo = ERPCliente.Codigo_cliente
                        BePropietario.Nombre_comercial = ERPCliente.Nombre_cliente
                        BePropietario.Contacto = "NDF"
                        BePropietario.Telefono = ""
                        BePropietario.NIT = ERPCliente.Nit
                        BePropietario.Direccion = ""
                        BePropietario.Sistema = False
                        BePropietario.Activo = True
                        BePropietario.User_agr = BeConfigEnc.IdUsuario
                        BePropietario.Fec_agr = Date.UtcNow
                        BePropietario.User_mod = BeConfigEnc.IdUsuario
                        BePropietario.Fec_mod = Date.UtcNow
                        BePropietario.IdTipoActualizacionCosto = 1 'si nuevo costo es mayor (>)

                        Try

                            '#EJC20210305: Guardar e insertar valores por defecto (UM, Clas, Fam)
                            clsLnPropietarios.Insertar_Nuevo_Propietario(BePropietario,
                                                                         BeConfigEnc.Idbodega,
                                                                         True,
                                                                         lConnectionWMS,
                                                                         lTransactionWMS)

                            VContadorBitacoraTomims += 1

                            BePropietarioBodega = New clsBePropietario_bodega()
                            BePropietarioBodega.IdPropietarioBodega = BePropietario.IdPropietario 'clsLnPropietario_bodega.MaxID(lConnectionWMS, lTransactionWMS)
                            BePropietarioBodega.IdPropietario = BePropietario.IdPropietario
                            'BePropietarioBodega.IdBodega = BeConfigEnc.Idbodega
                            BePropietarioBodega.Activo = True
                            BePropietarioBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                            BePropietarioBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                            BePropietarioBodega.Fec_agr = Now
                            BePropietarioBodega.Fec_mod = Now

                            For Each bodega In bodegas

                                BePropietarioBodega.IdBodega = bodega.IdBodega

                                If Not clsLnPropietario_bodega.Existe_IdPropietario_And_IdBodega(BePropietario.IdPropietario,
                                                                                                 bodega.IdBodega,
                                                                                                 lConnectionWMS,
                                                                                                 lTransactionWMS) Then

                                    'If clsLnPropietario_bodega.Existe_IdPropietarioBodega(BePropietarioBodega.IdPropietarioBodega, lConnectionWMS, lTransactionWMS) Then
                                    '    BePropietarioBodega.IdPropietarioBodega = clsLnPropietario_bodega.MaxID(lConnectionWMS, lTransactionWMS)
                                    'End If

                                    BePropietarioBodega.IdPropietarioBodega = clsLnPropietario_bodega.MaxID(lConnectionWMS, lTransactionWMS)
                                    clsLnPropietario_bodega.Insertar(BePropietarioBodega, lConnectionWMS, lTransactionWMS)

                                End If
                            Next

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                               BePropietario.Codigo,
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet, CnnLogWMS)

                            lblprg.AppendText(String.Format("Error al insertar propietario: {0}{1}{2}", BeCliente.Codigo, vbNewLine, ex.Message))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End Try

                        Application.DoEvents()

                    End If

#End Region

                    '#EJC20210305: Marcar en tabla WMS como procesado.
                    ERPCliente.Procesado_wms = True
                    clsLnI_nav_cliente.Actualizar_Procesado_WMS(ERPCliente, lConnectionWMS, lTransactionWMS)

                    '#EJC20210305: Marcar en tabla ERP como procesado.
                    clsLnCEALSA_clientes.Actualizar_Procesado_WMS(ERPCliente, lConnectionERP, lTransactionERP)

                Next

            End If


            lTransactionWMS.Commit() : lTransactionERP.Commit()

            lblprg.AppendText(vbNewLine)
            '#EJC20171107_REF04_0254AM: Desplegar cantidad de registros de proveedores procesados
            lblprg.AppendText("********** FIN DE INSERCIÓN EN TABLA DE TOMIMS ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Clientes/Propietarios procesados correctamente: {0}", VContadorBitacoraTomims))
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
            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLogWMS)

        Catch ex As Exception

            lblprg.AppendText(String.Format("Error al insertar propietario/cliente a tabla de TOMIMS: {0}", ex.Message))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If Not lTransactionWMS Is Nothing Then lTransactionWMS.Rollback()
            If Not lTransactionERP Is Nothing Then lTransactionERP.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally

            If lConnectionWMS.State = ConnectionState.Open Then lConnectionWMS.Close()
            If lConnectionERP.State = ConnectionState.Open Then lConnectionERP.Close()
            If CnnLogWMS.State = ConnectionState.Open Then CnnLogWMS.Close()
            prg.Value = 0
            prg.Visible = False

        End Try

    End Function

    '#CKFK 20210720 2257 Función modificada para insertar los clientes y proveedores como Bodegas Virtuales de cada una de las bodegas del sistema
    Public Function Crea_Clientes_Y_Provedores_Como_Bodegas_Virtuales(ByRef lblprg As RichTextBox,
                                                                      ByRef prg As ProgressBar,
                                                                      ByVal lConnection As SqlConnection,
                                                                      ByVal lTransaction As SqlTransaction) As Integer

        Crea_Clientes_Y_Provedores_Como_Bodegas_Virtuales = 0

        Dim vResult As Integer = 0
        Dim ListaClientes As New List(Of clsBeCliente)
        Dim BeProveedorBodega As New clsBeProveedor_bodega()
        Dim BeProveedorBodegaExistente As New clsBeProveedor_bodega()

        Try

            Dim bodegas As New List(Of clsBeBodega)
            bodegas = clsLnBodega.GetAll(lConnection, lTransaction)


            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Procesando proveedores")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            For Each bv As clsBeBodega In bodegas

                ListaClientes = clsLnCliente.GetAllNotBF(bv.IdBodega, lConnection, lTransaction)

                prg.Maximum = ListaClientes.Count

                Dim vContador As Integer = 0

                prg.Value = 0

                'GT22062022: mejorar la perfomance
                Dim IdTranProveedor As Integer = 0
                IdTranProveedor = clsLnProveedor.MaxID(lConnection, lTransaction) + 1

                For Each c As clsBeCliente In ListaClientes

                    vContador += 1

                    prg.Value = vContador

                    If Not Existe_BF(bv.IdBodega, c.IdPropietario, lConnection, lTransaction) Then

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText(String.Format("Creando al proveedor: {0} con Bodega Virtual: {1}", c.Codigo, bv.IdBodega))
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        '#CKFK 20210716 2353 Crear un proveedor como Bodega Fiscal 
                        Dim BeProveedor As New clsBeProveedor()
                        BeProveedor.IdEmpresa = c.IdEmpresa
                        BeProveedor.IdPropietario = c.IdPropietario
                        'BeProveedor.IdProveedor = clsLnProveedor.MaxID(lConnection, lTransaction) + 1
                        BeProveedor.IdProveedor = IdTranProveedor
                        BeProveedor.Codigo = "BF" & BeProveedor.IdProveedor
                        BeProveedor.Nombre = c.Nombre_comercial
                        BeProveedor.Telefono = c.Telefono
                        BeProveedor.Nit = c.Nit
                        BeProveedor.Direccion = c.Direccion
                        BeProveedor.Email = c.Correo_electronico
                        BeProveedor.Contacto = c.Nombre_comercial
                        BeProveedor.Activo = True
                        BeProveedor.Muestra_precio = False
                        BeProveedor.Fec_agr = Now
                        BeProveedor.Fec_mod = Now
                        BeProveedor.User_agr = c.User_agr
                        BeProveedor.User_mod = c.User_mod
                        BeProveedor.Actualiza_costo_oc = False
                        BeProveedor.IdUbicacionVirtual = bv.IdBodega
                        BeProveedor.Es_Bodega_Recepcion = True
                        BeProveedor.Es_Bodega_Traslado = True
                        BeProveedor.Referencia = "MI3_" & c.Codigo
                        BeProveedor.Sistema = True
                        BeProveedor.IdConfiguracionBarraPallet = 0

                        Try

                            vResult += clsLnProveedor.Insertar(BeProveedor, lConnection, lTransaction)

                            IdTranProveedor += 1

                        Catch ex As Exception
                            Throw New Exception("Error al insertar al proveedor: " & c.Nombre_comercial & " con idproveedor " & BeProveedor.IdProveedor & "-" & ex.Message)
                        End Try


                        'For Each b As clsBeBodega In bodegas
                        BeProveedorBodega = New clsBeProveedor_bodega
                        BeProveedorBodegaExistente = New clsBeProveedor_bodega
                        'BeProveedorBodega.IdAsignacion = BeProveedor.IdProveedor
                        BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                        BeProveedorBodega.IdBodega = bv.IdBodega
                        BeProveedorBodega.Activo = True
                        BeProveedorBodega.User_agr = c.User_agr
                        BeProveedorBodega.User_mod = c.User_agr
                        BeProveedorBodega.Fec_agr = Now
                        BeProveedorBodega.Fec_mod = Now
                        BeProveedorBodegaExistente = BeProveedorBodega

                        clsLnProveedor_bodega.Get_Single_By_IdBodega_And_IdProveedor(BeProveedorBodegaExistente, lConnection, lTransaction)

                        If BeProveedorBodegaExistente Is Nothing Then

                            'If clsLnProveedor_bodega.Existe_Id(BeProveedorBodega.IdAsignacion, lConnection, lTransaction) Then
                            BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConnection, lTransaction) + 1
                            'End If

                            clsLnProveedor_bodega.InsertarFromInterface(BeProveedorBodega, lConnection, lTransaction)

                            lblprg.AppendText(String.Format("Creando al proveedor-bodega {0} con Bodega Virtual {1}", c.Codigo, bv.Nombre))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                        Else
                            lblprg.AppendText(String.Format("Proveedor-bodega ya existe con Bodega {0}", bv.Nombre))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End If

                    Else

                        lblprg.AppendText(String.Format("El cliente {0} ya existe con Bodega Virtual {1}", c.Codigo, bv.IdBodega))
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End If

                Next

                'GT22082022: limpio la clase porque se ha utilizado en un proceso anterior en el mismo método
                ListaClientes = New List(Of clsBeCliente)
                ListaClientes = clsLnCliente.GetAllNotBG(bv.IdBodega, lConnection, lTransaction)

                prg.Maximum = ListaClientes.Count

                vContador = 0

                prg.Value = 0

                'GT22082022_1030: mejora performance en la iteración
                Dim pIdTransTipocliente = clsLnCliente_tipo.MaxID(lConnection, lTransaction) + 1
                Dim pIdCliente = clsLnCliente.MaxID(lConnection, lTransaction) + 1

                For Each c As clsBeCliente In ListaClientes

                    vContador += 1

                    prg.Value = vContador

                    If Not Existe_BG(bv.IdBodega, c.IdPropietario, lConnection, lTransaction) Then

                        lblprg.AppendText(String.Format("Creando al cliente {0} con Bodega Virtual {1}", c.Codigo, bv.IdBodega))
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        Dim BeClienteTipo As New clsBeCliente_tipo
                        'BeClienteTipo.IdTipoCliente = clsLnCliente_tipo.MaxID(lConnection, lTransaction) + 1
                        BeClienteTipo.IdTipoCliente = pIdTransTipocliente
                        BeClienteTipo.NombreTipoCliente = "Almacenadora"
                        BeClienteTipo.IdPropietario = c.IdPropietario
                        BeClienteTipo.Fec_agr = Now
                        BeClienteTipo.Fec_mod = Now
                        BeClienteTipo.User_agr = c.User_agr
                        BeClienteTipo.User_mod = c.User_mod
                        vResult += clsLnCliente_tipo.Insertar(BeClienteTipo, lConnection, lTransaction)

                        Dim BeCliente As New clsBeCliente
                        BeCliente.IdEmpresa = c.IdEmpresa
                        BeCliente.IdPropietario = c.IdPropietario
                        'BeCliente.IdCliente = clsLnCliente.MaxID(lConnection, lTransaction) + 1
                        BeCliente.IdCliente = pIdCliente
                        BeCliente.Codigo = "BG" & BeCliente.IdCliente
                        BeCliente.Nombre_comercial = c.Nombre_comercial.TrimEnd()
                        BeCliente.Telefono = c.Telefono
                        BeCliente.Nit = c.Nit
                        BeCliente.Direccion = c.Direccion
                        BeCliente.Control_Ultimo_Lote = False
                        BeCliente.Es_bodega_recepcion = True
                        BeCliente.Es_Bodega_Traslado = True
                        BeCliente.Realiza_manufactura = False
                        BeCliente.Sistema = True
                        BeCliente.IdTipoCliente = BeClienteTipo.IdTipoCliente
                        BeCliente.IdUbicacionVirtual = bv.IdBodega
                        BeCliente.Activo = True
                        BeCliente.User_agr = c.User_agr
                        BeCliente.Fec_agr = Date.UtcNow
                        BeCliente.User_mod = c.User_agr
                        BeCliente.Fec_mod = Date.UtcNow

                        If Not clsLnCliente.Existe_Cliente_By_Codigo(BeCliente.Codigo, lConnection, lTransaction) Then

                            Dim BeClienteBodega As New clsBeCliente_bodega()

                            If clsLnCliente.Existe_Cliente_By_IdCliente(BeCliente.IdCliente, lConnection, lTransaction) Then
                                'BeCliente.IdCliente = clsLnCliente.MaxID(lConnection, lTransaction) + 1
                                pIdCliente += 1
                                BeCliente.IdCliente = pIdCliente
                            End If

                            clsLnCliente.Insertar(BeCliente, lConnection, lTransaction)

                            For Each b As clsBeBodega In bodegas

                                BeClienteBodega = New clsBeCliente_bodega()
                                BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID(lConnection, lTransaction) + 1
                                BeClienteBodega.IdCliente = BeCliente.IdCliente
                                BeClienteBodega.IdBodega = b.IdBodega
                                BeClienteBodega.Activo = True
                                BeClienteBodega.User_agr = c.User_agr '1 Esto debería ser parametrizable?
                                BeClienteBodega.User_mod = c.User_agr  '1 Esto debería ser parametrizable?
                                BeClienteBodega.Fec_agr = Now
                                BeClienteBodega.Fec_mod = Now

                                clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega, lConnection, lTransaction)

                            Next

                        End If


                        pIdCliente += 1
                        pIdTransTipocliente += 1

                    Else

                        lblprg.AppendText(String.Format("El cliente {0} ya existe como Bodega con Bodega Virtual {1}", c.Codigo, bv.IdBodega))
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()
                    End If
                Next

            Next

            Crea_Clientes_Y_Provedores_Como_Bodegas_Virtuales = vResult

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    '#CKFK 20210717 1627 Función creada para saber si el cliente ya existe como Bodega Virtual
    Public Shared Function Existe_BF(ByVal IdUbicacionVirtual As Integer,
                                     ByVal IdPropietario As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Boolean

        Dim lReturn As Boolean = False

        Try

            Const lSQl As String = "SELECT IdProveedor FROM proveedor 
                                    WHERE IdPropietario = @IdPropietario 
                                      AND codigo like 'BF%' 
                                      AND es_bodega_traslado = 1
                                      AND es_bodega_recepcion = 1  
                                      AND idubicacionvirtual=@idubicacionvirtual
                                      AND sistema = 1"

            Using lDTA As New SqlDataAdapter(lSQl, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", IdPropietario)
                lDTA.SelectCommand.Parameters.AddWithValue("@idubicacionvirtual", IdUbicacionVirtual)
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing Then

                    lReturn = lDataTable.Rows.Count > 0

                End If

            End Using

        Catch ex As Exception
            Throw New Exception("Existe_BF: " & ex.Message)
        End Try

        Return lReturn

    End Function

    '#CKFK 20210717 1615 Función creada para saber si el cliente ya existe como Bodega General
    Public Shared Function Existe_BG(ByVal IdUbicacionVirtual As Integer,
                                     ByVal IdPropietario As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Boolean

        Dim lReturn As Boolean = False

        Try

            Const lSQl As String = "SELECT IdCliente FROM Cliente 
                                    WHERE IdPropietario = @IdPropietario 
                                      AND codigo like 'BG%' 
                                      AND es_bodega_traslado = 1
                                      AND es_bodega_recepcion = 1  
                                      AND idubicacionvirtual=@idubicacionvirtual
                                      AND sistema = 1"

            Using lDTA As New SqlDataAdapter(lSQl, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", IdPropietario)
                lDTA.SelectCommand.Parameters.AddWithValue("@idubicacionvirtual", IdUbicacionVirtual)
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing Then

                    lReturn = lDataTable.Rows.Count > 0

                End If

            End Using

        Catch ex As Exception
            Throw New Exception("Existe_BF: " & ex.Message)
        End Try

        Return lReturn

    End Function

    '#CKFK 20220902 Función creada para insertar los clientes no existen en el WMS
    Public Function Crea_Clientes_No_Existentes(ByRef lblprg As RichTextBox,
                                                ByRef prg As ProgressBar,
                                                ByVal lConnection As SqlConnection,
                                                ByVal lTransaction As SqlTransaction) As Integer

        Crea_Clientes_No_Existentes = 0

        Dim vResult As Integer = 0
        Dim ListaPropietarios As New List(Of clsBePropietarios)
        Dim ListaProveedores As New List(Of clsBeProveedor)

        Try

            Dim bodegas As New List(Of clsBeBodega)
            bodegas = clsLnBodega.GetAll(lConnection, lTransaction)

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Procesando clientes no existentes y que son propietarios")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            ListaPropietarios = New List(Of clsBePropietarios)
            ListaPropietarios = clsLnCliente.Get_Propietarios_No_Existentes_En_Clientes(lConnection, lTransaction)

            prg.Maximum = ListaPropietarios.Count

            Dim vContador As Integer = 0

            prg.Value = 0

            Dim pIdTransTipocliente = clsLnCliente_tipo.MaxID(lConnection, lTransaction) + 1
            Dim pIdCliente = clsLnCliente.MaxID(lConnection, lTransaction) + 1

            For Each propietario As clsBePropietarios In ListaPropietarios

                vContador += 1

                prg.Value = vContador

                lblprg.AppendText(String.Format("Creando al cliente {0}", propietario.Codigo))
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                Dim BeClienteTipo As New clsBeCliente_tipo
                'BeClienteTipo.IdTipoCliente = clsLnCliente_tipo.MaxID(lConnection, lTransaction) + 1
                BeClienteTipo.IdTipoCliente = pIdTransTipocliente
                BeClienteTipo.NombreTipoCliente = "Almacenadora"
                BeClienteTipo.IdPropietario = propietario.IdPropietario
                BeClienteTipo.Fec_agr = Now
                BeClienteTipo.Fec_mod = Now
                BeClienteTipo.User_agr = propietario.User_agr
                BeClienteTipo.User_mod = propietario.User_mod
                vResult += clsLnCliente_tipo.Insertar(BeClienteTipo, lConnection, lTransaction)

                Dim BeCliente As New clsBeCliente
                BeCliente.IdEmpresa = propietario.IdEmpresa
                BeCliente.IdPropietario = propietario.IdPropietario
                'BeCliente.IdCliente = clsLnCliente.MaxID(lConnection, lTransaction) + 1
                BeCliente.IdCliente = pIdCliente
                BeCliente.Codigo = pIdCliente
                BeCliente.Nombre_comercial = propietario.Nombre_comercial.TrimEnd()
                BeCliente.Telefono = propietario.Telefono
                BeCliente.Nit = propietario.NIT
                BeCliente.Direccion = propietario.Direccion
                BeCliente.Control_Ultimo_Lote = False
                BeCliente.Es_bodega_recepcion = True
                BeCliente.Es_Bodega_Traslado = True
                BeCliente.Realiza_manufactura = False
                BeCliente.Sistema = True
                BeCliente.IdTipoCliente = BeClienteTipo.IdTipoCliente
                BeCliente.IdUbicacionVirtual = 0
                BeCliente.Activo = True
                BeCliente.User_agr = propietario.User_agr
                BeCliente.Fec_agr = Date.UtcNow
                BeCliente.User_mod = propietario.User_agr
                BeCliente.Fec_mod = Date.UtcNow

                clsLnCliente.Insertar(BeCliente, lConnection, lTransaction)

                For Each bodega As clsBeBodega In bodegas

                    Dim BeClienteBodega As New clsBeCliente_bodega()

                    BeClienteBodega = New clsBeCliente_bodega()
                    BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID(lConnection, lTransaction) + 1
                    BeClienteBodega.IdCliente = pIdCliente
                    BeClienteBodega.IdBodega = bodega.IdBodega
                    BeClienteBodega.Activo = True
                    BeClienteBodega.User_agr = propietario.User_agr '1 Esto debería ser parametrizable?
                    BeClienteBodega.User_mod = propietario.User_agr  '1 Esto debería ser parametrizable?
                    BeClienteBodega.Fec_agr = Now
                    BeClienteBodega.Fec_mod = Now

                    clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega, lConnection, lTransaction)

                Next

                pIdCliente += 1
                pIdTransTipocliente += 1

            Next

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Procesando clientes no existentes y que son proveedores")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            ListaProveedores = New List(Of clsBeProveedor)
            ListaProveedores = clsLnCliente.Get_Proveedores_No_Existentes_En_Clientes(lConnection, lTransaction)

            prg.Maximum = ListaProveedores.Count

            vContador = 0

            prg.Value = 0

            pIdTransTipocliente = clsLnCliente_tipo.MaxID(lConnection, lTransaction) + 1
            pIdCliente = clsLnCliente.MaxID(lConnection, lTransaction) + 1

            For Each proveedor As clsBeProveedor In ListaProveedores

                vContador += 1

                prg.Value = vContador

                lblprg.AppendText(String.Format("Creando al cliente {0}", proveedor.Codigo))
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                Dim BeClienteTipo As New clsBeCliente_tipo
                'BeClienteTipo.IdTipoCliente = clsLnCliente_tipo.MaxID(lConnection, lTransaction) + 1
                BeClienteTipo.IdTipoCliente = pIdTransTipocliente
                BeClienteTipo.NombreTipoCliente = "Almacenadora"
                BeClienteTipo.IdPropietario = proveedor.IdPropietario
                BeClienteTipo.Fec_agr = Now
                BeClienteTipo.Fec_mod = Now
                BeClienteTipo.User_agr = proveedor.User_agr
                BeClienteTipo.User_mod = proveedor.User_mod
                vResult += clsLnCliente_tipo.Insertar(BeClienteTipo, lConnection, lTransaction)

                Dim BeCliente As New clsBeCliente
                BeCliente.IdEmpresa = proveedor.IdEmpresa
                BeCliente.IdPropietario = proveedor.IdPropietario
                'BeCliente.IdCliente = clsLnCliente.MaxID(lConnection, lTransaction) + 1
                BeCliente.IdCliente = pIdCliente
                BeCliente.Codigo = pIdCliente
                BeCliente.Nombre_comercial = proveedor.Nombre.TrimEnd()
                BeCliente.Telefono = proveedor.Telefono
                BeCliente.Nit = proveedor.Nit
                BeCliente.Direccion = proveedor.Direccion
                BeCliente.Control_Ultimo_Lote = False
                BeCliente.Es_bodega_recepcion = True
                BeCliente.Es_Bodega_Traslado = True
                BeCliente.Realiza_manufactura = False
                BeCliente.Sistema = True
                BeCliente.IdTipoCliente = BeClienteTipo.IdTipoCliente
                BeCliente.IdUbicacionVirtual = 0
                BeCliente.Activo = True
                BeCliente.User_agr = proveedor.User_agr
                BeCliente.Fec_agr = Date.UtcNow
                BeCliente.User_mod = proveedor.User_agr
                BeCliente.Fec_mod = Date.UtcNow

                clsLnCliente.Insertar(BeCliente, lConnection, lTransaction)

                For Each bodega As clsBeBodega In bodegas

                    Dim BeClienteBodega As New clsBeCliente_bodega()

                    BeClienteBodega = New clsBeCliente_bodega()
                    BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID(lConnection, lTransaction) + 1
                    BeClienteBodega.IdCliente = BeCliente.IdCliente
                    BeClienteBodega.IdBodega = bodega.IdBodega
                    BeClienteBodega.Activo = True
                    BeClienteBodega.User_agr = proveedor.User_agr '1 Esto debería ser parametrizable?
                    BeClienteBodega.User_mod = proveedor.User_agr  '1 Esto debería ser parametrizable?
                    BeClienteBodega.Fec_agr = Now
                    BeClienteBodega.Fec_mod = Now

                    clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega, lConnection, lTransaction)

                Next

                pIdCliente += 1
                pIdTransTipocliente += 1

            Next


            Crea_Clientes_No_Existentes = vResult

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    '#CKFK 20220902 Función creada para insertar los clientes no existen en el WMS
    Public Function Crea_Proveedores_No_Existentes(ByRef lblprg As RichTextBox,
                                                   ByRef prg As ProgressBar,
                                                   ByVal lConnection As SqlConnection,
                                                  ByVal lTransaction As SqlTransaction) As Integer

        Crea_Proveedores_No_Existentes = 0

        Dim vResult As Integer = 0
        Dim ListaClientes As New List(Of clsBeCliente)
        Dim ListaPropietarios As New List(Of clsBePropietarios)
        Dim BeProveedorBodega As New clsBeProveedor_bodega()
        Dim BeProveedorBodegaExistente As New clsBeProveedor_bodega()

        Try

            Dim bodegas As New List(Of clsBeBodega)
            bodegas = clsLnBodega.GetAll(lConnection, lTransaction)


            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Procesando proveedores")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            ListaClientes = clsLnProveedor.Get_Clientes_No_Existentes_En_Proveedores(lConnection, lTransaction)

            prg.Maximum = ListaClientes.Count

            Dim vContador As Integer = 0

            prg.Value = 0

            Dim IdTranProveedor As Integer = 0
            IdTranProveedor = clsLnProveedor.MaxID(lConnection, lTransaction) + 1

            For Each cliente As clsBeCliente In ListaClientes

                vContador += 1

                prg.Value = vContador

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText(String.Format("Creando al proveedor: {0}", cliente.Codigo))
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                Dim BeProveedor As New clsBeProveedor()
                BeProveedor.IdEmpresa = cliente.IdEmpresa
                BeProveedor.IdPropietario = cliente.IdPropietario
                BeProveedor.IdProveedor = IdTranProveedor
                BeProveedor.Codigo = BeProveedor.IdProveedor
                BeProveedor.Nombre = cliente.Nombre_comercial
                BeProveedor.Telefono = cliente.Telefono
                BeProveedor.Nit = cliente.Nit
                BeProveedor.Direccion = cliente.Direccion
                BeProveedor.Email = cliente.Correo_electronico
                BeProveedor.Contacto = cliente.Nombre_comercial
                BeProveedor.Activo = True
                BeProveedor.Muestra_precio = False
                BeProveedor.Fec_agr = Now
                BeProveedor.Fec_mod = Now
                BeProveedor.User_agr = cliente.User_agr
                BeProveedor.User_mod = cliente.User_mod
                BeProveedor.Actualiza_costo_oc = False
                BeProveedor.IdUbicacionVirtual = 0
                '#GT31072024: para cealsa esto debe ser false en el proveedor principal, en los virtuales es true
                BeProveedor.Es_Bodega_Recepcion = False
                BeProveedor.Es_Bodega_Traslado = False
                BeProveedor.Referencia = "MI3_" & cliente.Codigo
                BeProveedor.Sistema = True
                BeProveedor.IdConfiguracionBarraPallet = 0

                Try

                    vResult += clsLnProveedor.Insertar(BeProveedor, lConnection, lTransaction)

                    IdTranProveedor += 1

                Catch ex As Exception
                    Throw New Exception("Error al insertar al proveedor: " & cliente.Nombre_comercial & " con idproveedor " & BeProveedor.IdProveedor & "-" & ex.Message)
                End Try

                For Each bodega As clsBeBodega In bodegas

                    BeProveedorBodega = New clsBeProveedor_bodega
                    BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConnection, lTransaction) + 1
                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                    BeProveedorBodega.IdBodega = bodega.IdBodega
                    BeProveedorBodega.Activo = True
                    BeProveedorBodega.User_agr = cliente.User_agr
                    BeProveedorBodega.User_mod = cliente.User_agr
                    BeProveedorBodega.Fec_agr = Now
                    BeProveedorBodega.Fec_mod = Now

                    clsLnProveedor_bodega.InsertarFromInterface(BeProveedorBodega, lConnection, lTransaction)


                    lblprg.AppendText(vbNewLine)
                    lblprg.AppendText(String.Format("Creando proveedor-bodega: {0} en bodega {1}", BeProveedorBodega.IdAsignacion, bodega.IdBodega))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                Next

            Next

            ListaPropietarios = New List(Of clsBePropietarios)
            ListaPropietarios = clsLnProveedor.Get_Propietarios_No_Existentes_En_Proveedores(lConnection, lTransaction)

            prg.Maximum = ListaPropietarios.Count

            vContador = 0

            prg.Value = 0

            Dim pIdTransTipocliente = clsLnCliente_tipo.MaxID(lConnection, lTransaction) + 1
            Dim pIdCliente = clsLnCliente.MaxID(lConnection, lTransaction) + 1

            For Each propietario As clsBePropietarios In ListaPropietarios

                vContador += 1

                prg.Value = vContador

                lblprg.AppendText(String.Format("Creando al cliente {0}", propietario.Codigo))
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                Dim BeProveedor As New clsBeProveedor()
                BeProveedor.IdEmpresa = propietario.IdEmpresa
                BeProveedor.IdPropietario = propietario.IdPropietario
                BeProveedor.IdProveedor = IdTranProveedor
                BeProveedor.Codigo = BeProveedor.IdProveedor
                BeProveedor.Nombre = propietario.Nombre_comercial
                BeProveedor.Telefono = propietario.Telefono
                BeProveedor.Nit = propietario.NIT
                BeProveedor.Direccion = propietario.Direccion
                BeProveedor.Email = ""
                BeProveedor.Contacto = propietario.Nombre_comercial
                BeProveedor.Activo = True
                BeProveedor.Muestra_precio = False
                BeProveedor.Fec_agr = Now
                BeProveedor.Fec_mod = Now
                BeProveedor.User_agr = propietario.User_agr
                BeProveedor.User_mod = propietario.User_mod
                BeProveedor.Actualiza_costo_oc = False
                BeProveedor.IdUbicacionVirtual = 0
                BeProveedor.Es_Bodega_Recepcion = True
                BeProveedor.Es_Bodega_Traslado = True
                BeProveedor.Referencia = "MI3_" & propietario.Codigo
                BeProveedor.Sistema = True
                BeProveedor.IdConfiguracionBarraPallet = 0

                Try

                    vResult += clsLnProveedor.Insertar(BeProveedor, lConnection, lTransaction)

                    IdTranProveedor += 1

                Catch ex As Exception
                    Throw New Exception("Error al insertar al proveedor: " & propietario.Nombre_comercial & " con idproveedor " & BeProveedor.IdProveedor & "-" & ex.Message)
                End Try


                For Each bodega As clsBeBodega In bodegas

                    BeProveedorBodega = New clsBeProveedor_bodega
                    BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConnection, lTransaction) + 1
                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                    BeProveedorBodega.IdBodega = bodega.IdBodega
                    BeProveedorBodega.Activo = True
                    BeProveedorBodega.User_agr = propietario.User_agr
                    BeProveedorBodega.User_mod = propietario.User_agr
                    BeProveedorBodega.Fec_agr = Now
                    BeProveedorBodega.Fec_mod = Now

                    clsLnProveedor_bodega.InsertarFromInterface(BeProveedorBodega, lConnection, lTransaction)

                Next

            Next

            Crea_Proveedores_No_Existentes = vResult

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    '#CKFK 20220902 Función creada para insertar los clientes no existen en el WMS
    Public Function Crea_Propietarios_No_Existentes(ByRef lblprg As RichTextBox,
                                                    ByRef prg As ProgressBar,
                                                    ByVal lConnection As SqlConnection,
                                                    ByVal lTransaction As SqlTransaction) As Integer

        Crea_Propietarios_No_Existentes = 0

        Dim vResult As Integer = 0
        Dim ListaClientes As New List(Of clsBeCliente)
        Dim ListaProveedores As New List(Of clsBeProveedor)
        Dim BePropietarioBodega As New clsBePropietario_bodega()
        Dim BePropietario As New clsBePropietarios()

        Try

            Dim bodegas As New List(Of clsBeBodega)
            bodegas = clsLnBodega.GetAll(lConnection, lTransaction)

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Procesando propietarios")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()


            ListaClientes = clsLnPropietarios.Get_Clientes_No_Existentes_En_Propietarios(lConnection, lTransaction)

            prg.Maximum = ListaClientes.Count

            Dim vContador As Integer = 0

            prg.Value = 0

            'GT22062022: mejorar la perfomance
            Dim IdPropietario As Integer = 0
            IdPropietario = clsLnPropietarios.MaxID(lConnection, lTransaction) + 1

            For Each cliente As clsBeCliente In ListaClientes

                vContador += 1

                prg.Value = vContador

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText(String.Format("Creando al propietario: {0}", IdPropietario))
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                BePropietario.IdEmpresa = BeConfigEnc.Idempresa
                BePropietario.IdPropietario = clsLnPropietarios.MaxID(lConnection, lTransaction)
                BePropietario.Codigo = cliente.Codigo
                BePropietario.Nombre_comercial = cliente.Nombre_comercial
                BePropietario.Contacto = "NDF"
                BePropietario.Telefono = ""
                BePropietario.NIT = cliente.Nit
                BePropietario.Direccion = ""
                BePropietario.Sistema = False
                BePropietario.Activo = True
                BePropietario.User_agr = BeConfigEnc.IdUsuario
                BePropietario.Fec_agr = Date.UtcNow
                BePropietario.User_mod = BeConfigEnc.IdUsuario
                BePropietario.Fec_mod = Date.UtcNow
                BePropietario.IdTipoActualizacionCosto = 1 'si nuevo costo es mayor (>)


                '#EJC20210305: Guardar e insertar valores por defecto (UM, Clas, Fam)
                clsLnPropietarios.Insertar_Nuevo_Propietario(BePropietario,
                                                                 BeConfigEnc.Idbodega,
                                                                 True,
                                                                 lConnection,
                                                                 lTransaction)

                lblprg.AppendText(String.Format("Propietario procesado : {0}", BePropietario.Nombre_comercial))
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                BePropietarioBodega = New clsBePropietario_bodega()
                BePropietarioBodega.IdPropietario = BePropietario.IdPropietario
                BePropietarioBodega.Activo = True
                BePropietarioBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                BePropietarioBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                BePropietarioBodega.Fec_agr = Now
                BePropietarioBodega.Fec_mod = Now

                For Each bodega In bodegas

                    lblprg.AppendText(String.Format("Procesando Propietario para bodega: {0}", bodega.Nombre))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    BePropietarioBodega.IdBodega = bodega.IdBodega
                    BePropietarioBodega.IdPropietarioBodega = clsLnPropietario_bodega.MaxID(lConnection, lTransaction)
                    clsLnPropietario_bodega.Insertar(BePropietarioBodega, lConnection, lTransaction)

                Next

            Next

            'GT22082022: limpio la clase porque se ha utilizado en un proceso anterior en el mismo método
            ListaProveedores = New List(Of clsBeProveedor)
            ListaProveedores = clsLnPropietarios.Get_Proveedores_No_Existentes_En_Propietarios(lConnection, lTransaction)

            prg.Maximum = ListaProveedores.Count

            vContador = 0

            prg.Value = 0

            'GT22082022_1030: mejora performance en la iteración
            IdPropietario = clsLnPropietarios.MaxID(lConnection, lTransaction) + 1

            For Each proveedor As clsBeProveedor In ListaProveedores

                vContador += 1

                prg.Value = vContador

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText(String.Format("Creando al propietario: {0}", IdPropietario))
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                BePropietario.IdEmpresa = BeConfigEnc.Idempresa
                BePropietario.IdPropietario = clsLnPropietarios.MaxID(lConnection, lTransaction)
                BePropietario.Codigo = proveedor.Codigo
                BePropietario.Nombre_comercial = proveedor.Nombre
                BePropietario.Contacto = "NDF"
                BePropietario.Telefono = ""
                BePropietario.NIT = proveedor.Nit
                BePropietario.Direccion = ""
                BePropietario.Sistema = False
                BePropietario.Activo = True
                BePropietario.User_agr = BeConfigEnc.IdUsuario
                BePropietario.Fec_agr = Date.UtcNow
                BePropietario.User_mod = BeConfigEnc.IdUsuario
                BePropietario.Fec_mod = Date.UtcNow
                BePropietario.IdTipoActualizacionCosto = 1 'si nuevo costo es mayor (>)

                '#EJC20210305: Guardar e insertar valores por defecto (UM, Clas, Fam)
                clsLnPropietarios.Insertar_Nuevo_Propietario(BePropietario,
                                                                 BeConfigEnc.Idbodega,
                                                                 True,
                                                                 lConnection,
                                                                 lTransaction)

                lblprg.AppendText(String.Format("Propietario procesado : {0}", BePropietario.Nombre_comercial))
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                BePropietarioBodega = New clsBePropietario_bodega()
                BePropietarioBodega.IdPropietario = BePropietario.IdPropietario
                BePropietarioBodega.Activo = True
                BePropietarioBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                BePropietarioBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                BePropietarioBodega.Fec_agr = Now
                BePropietarioBodega.Fec_mod = Now

                For Each bodega In bodegas

                    lblprg.AppendText(String.Format("Procesando Propietario para bodega: {0}", bodega.Nombre))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    BePropietarioBodega.IdBodega = bodega.IdBodega
                    BePropietarioBodega.IdPropietarioBodega = clsLnPropietario_bodega.MaxID(lConnection, lTransaction)
                    clsLnPropietario_bodega.Insertar(BePropietarioBodega, lConnection, lTransaction)

                Next

            Next

            Crea_Propietarios_No_Existentes = vResult

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class

