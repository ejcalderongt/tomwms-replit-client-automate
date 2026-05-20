Imports System.Reflection
Imports DevExpress.XtraEditors

Module ModuleMain

    Private ReadOnly Instances() As Process = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName)

    Public MenuPrincipal As frmMenu
    Public IdConfiguracion As String = ""

    Sub Main(ByVal Args As String())

        Dim arAgs As String() = Nothing
        Dim InterfaceAEjecutar As String = ""

        Try

            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)

            'No vienen parámetros
            If Args.Length = 0 Then

                Init_App()

                RemoteCallBack = False
                MenuPrincipal = New frmMenu(IdConfiguracion, 0)
                'MsgBox("No hay argumento remoto: ")
            Else

                RemoteCallBack = True

                'ReDim Preserve Args(0)
                'Args(0) = ("4-1-0-1")

                arAgs = Args(0).Split("-")

                If arAgs.Count > 3 Then

                    InterfaceAEjecutar = Trim(arAgs(0))
                    IdConfiguracion = arAgs(1)
                    IndiceInstanciaDefecto = arAgs(2)
                    '#CKFK20230801 Agregué usuario en los argumentos
                    IdUsuario = arAgs(3)

                    Init_App()

                    If InterfaceAEjecutar = "" Then
                        MenuPrincipal = New frmMenu(IdConfiguracion, IdUsuario)
                    Else
                        MenuPrincipal = New frmMenu(InterfaceAEjecutar, IdConfiguracion, IdUsuario)
                    End If

                Else
                    MsgBox("Error en llamado a la interface con los parámetros: arAgs.Count = " & arAgs.Count)
                End If

            End If

            'If Init_App() Then
            Application.Run(MenuPrincipal) ' I can reference 'mainView' from anywhere in my app, toggle its Visible  property etc.               
            'End If

        Catch ex As Exception
            If Not (Args.Length = 0) Then
                XtraMessageBox.Show(String.Format("Error al iniciar la interface: {0} {1}, Ruta Ini: {2}, Remoto {3}, Argumentos {4}", MethodBase.GetCurrentMethod.Name(), ex.Message, CurDir() & "\" & "Conn.ini", RemoteCallBack, Args(0)),
                    "Interface",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation)
            Else
                XtraMessageBox.Show(String.Format("Error al iniciar la interface: {0} {1}, Ruta Ini: {2}, Remoto {3}, Argumentos {4}", MethodBase.GetCurrentMethod.Name(), ex.Message, CurDir() & "\" & "Conn.ini", RemoteCallBack, 0),
                    "Interface",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation)

            End If

        End Try

    End Sub

    Private Function Init_App() As Boolean

        Init_App = False

        Try

            If Not Existe_Ini() Then
                XtraMessageBox.Show(String.Format("No existe archivo de conexión ini en: {0}\Conn.ini", CurDir()), "Conexión a BD", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.Exit()
            Else

                Dim lInstancias As New List(Of clsCadenaConexion)

                'Dim IndiceInstanciaDefecto As Integer =0 '#CKFK 20180603 03:08 PM Puse esto en comentario porque no me podia conectar a mi servidor local
                lInstancias = clsPublic.Leer_Archivo_Configuracion_Ini(IndiceInstanciaDefecto, True)

                If lInstancias Is Nothing Then
                    XtraMessageBox.Show(String.Format("No se pudo procesar correctamente el archivo de configuración ini: {0}\Conn.ini", CurDir()), "Conexión a BD", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Application.Exit()
                Else

                    If IndiceInstanciaDefecto = -1 Then IndiceInstanciaDefecto = 0

                    BD.Instancia = lInstancias(IndiceInstanciaDefecto)

                    If Abrio_Conexion(lInstancias) Then

                        'Inicializar endpoint de WS.
                        My.MySettings.Default.DynamicsNavInterface_WSFichaBodegas_Ficha_Bodegas_Service = BD.Instancia.URLBodegas
                        My.MySettings.Default.DynamicsNavInterface_wsProductos_Productos_Service = BD.Instancia.URLProductos
                        My.MySettings.Default.DynamicsNavInterface_WSProveedores_Proveedores_Service = BD.Instancia.URLProveedores
                        My.MySettings.Default.DynamicsNavInterface_WSPedidoCompra_Pedidos_Compra_Service = BD.Instancia.URLPedidosCompra
                        My.MySettings.Default.DynamicsNavInterface_WsPedidoTransferencia_Pedidos_Transferencia_Service = BD.Instancia.URLPedidosTransferencia
                        My.MySettings.Default.DynamicsNavInterface_wsCategoriasProductos_Categorias_Productos_Service = BD.Instancia.URLCategoriasProducto
                        My.MySettings.Default.DynamicsNavInterface_WSGruposProductos_Grupos_Productos_Service = BD.Instancia.URLGruposProducto
                        My.MySettings.Default.NavSync_wsTablaConversiones_Tabla_Conversiones_Service = BD.Instancia.URLTablaConversiones
                        My.MySettings.Default.DynamicsNavInterface_WSNavLotePedidoCompra_Lote_PedidoCompra = BD.Instancia.URLLotePedidoCompra
                        My.MySettings.Default.DynamicsNavInterface_WSNavCantidadRecibirPedidoCompra_CantidadRecibir_PedidoCompra = BD.Instancia.URLCantidadPedidoCompra
                        My.MySettings.Default.DynamicsNavInterface_WSLotePedidoTransferencia_Lote_PedidoTransferencia = BD.Instancia.URLLotePedidoTrans
                        My.MySettings.Default.DynamicsNavInterface_WSCantidadPedidoTransferencia_CantidadEnviar_PedidoTransferencia = BD.Instancia.URLCantidadPedidoTrans
                        My.MySettings.Default.DynamicsNavInterface_WSRegistraRecepcionCompra_Registra_Recepcion_Compra = BD.Instancia.URLRegistroRecepCompra
                        My.MySettings.Default.DynamicsNavInterface_WSRegistraTransferEnvio_Registra_Transfer_Envio = BD.Instancia.URLRegistroTransfEnvio
                        My.MySettings.Default.DynamicsNavInterface_WSRegistraTransferRecepcion_Registra_Transfer_Recepcion = BD.Instancia.URLRegistroTransfRecep
                        My.MySettings.Default.NavSync_WSPaginaLotes_Pagina_lotes_Service = BD.Instancia.URLLotesTransfRec
                        My.MySettings.Default.NavSync_wsAjusteInventario_Ajustes_Inventario = BD.Instancia.URLAjusteInventario
                        My.MySettings.Default.NavSync_WSSeries_Series_Service = BD.Instancia.URLSeries
                        My.MySettings.Default.NavSync_WSClientes_Clientes_Service = BD.Instancia.URLClientes
                        My.MySettings.Default.NavSync_WSListaClientes_Lista_clientes_Service = BD.Instancia.URLClientes
                        My.MySettings.Default.NavSync_WSOrdenesProduccion_OP_Lanzadas_Service = BD.Instancia.URLOrdenesProduccion
                        My.MySettings.Default.NavSync_WSPedidosVenta_Pedidos_Service = BD.Instancia.URLPedidosVenta
                        My.MySettings.Default.NavSync_WSEnvioAlm_Envio_alm_Service = BD.Instancia.URLEnviosAlm
                        My.MySettings.Default.NavSync_CUWMS_CUWMS = BD.Instancia.URLCUWMS
                        My.MySettings.Default.NavSync_WSCreaPicking_Crea_picking_Service = BD.Instancia.URLCreaPicking
                        My.MySettings.Default.NavSync_WSRecepcionesAlm_Recep_Almacen_Service = BD.Instancia.URLReceAlm
                        My.MySettings.Default.NavSync_WSDiarioAlmacen_Diario_Almacen_Service = BD.Instancia.WSDiarioAlmacen
                        My.MySettings.Default.NavSync_WSDevolucion_Devolucion_Service = BD.Instancia.WSDevolucion
                        My.MySettings.Default.NavSync_wsDevolucionVenta_Devolucion_venta_Service = BD.Instancia.WSDevolucionVenta
                        My.MySettings.Default.NavSync_WSPicking_Picking_Service = BD.Instancia.WSPicking
                        My.MySettings.Default.NavSync_WSUbicarAlmacen_Ubicar_Almacen_Service = BD.Instancia.WSUbicarAlmacen
                        My.Settings.clavews = BD.Instancia.ClaveWS
                        My.Settings.usuariows = BD.Instancia.UsuarioWS

                        If IdConfiguracion = "" Then
                            IdConfiguracion = BD.Instancia.IdConfiguracionInterface
                        Else
                            BD.Instancia.IdConfiguracionInterface = IdConfiguracion
                        End If

                        BD.Instancia.Modo_Debug = True

                        Configuration.ConfigurationManager.AppSettings("CST") = BD.Instancia.CadenaConexionSQLClient
                        Configuration.ConfigurationManager.AppSettings("WMS_MODO_DEBUG") = IIf(BD.Instancia.Modo_Debug, "ON", "OFF")
                        Configuration.ConfigurationManager.AppSettings("WMS_RESERVA_MI3_TRACE") = IIf(BD.Instancia.Modo_Debug, "ON", "OFF")

                        If BD.Instancia.WSTOMHH.Trim <> "" Then
                            'Dim BasicHttpBinding As ServiceModel.BasicHttpBinding = New ServiceModel.BasicHttpBinding
                            'Dim address As ServiceModel.EndpointAddress = New ServiceModel.EndpointAddress(BD.Instancia.WSTOMHH)
                            wsTOMHHInstance = New WebReference.TOMHHWS(BD.Instancia.WSTOMHH)
                        Else
                            wsTOMHHInstance = Nothing
                        End If

                        Init_App = True

                    End If

                End If

            End If

        Catch ex1 As SqlClient.SqlException
            If ex1.HResult = -2146233088 OrElse ex1.HResult = -2146232060 Then
                Throw New Exception("La instancia seleccionada no está disponible o 
                                     no es accesible, 
                                    verifique datos de conexion para: " & BD.Instancia.Server)
            Else
                Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex1.Message))
            End If
        Catch ex As Exception
            If ex.HResult = -2146233088 OrElse ex.HResult = -2146232060 Then
                Throw New Exception("La instancia seleccionada no está disponible o 
                                     no es accesible, 
                                    verifique datos de conexion para: " & BD.Instancia.Server)
            Else
                Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
            End If
        End Try

    End Function

    Public Function Abrio_Conexion(ByVal ListaInstancias As List(Of clsCadenaConexion)) As Boolean

        Abrio_Conexion = False

        '#EJC20171031_1259AM_REF: Cambiar timeOut de archivo de configuración para verificación de conexión a BD de forma temporal.
        Dim vTimeOutConfig As Double
        Dim CadenaConexion As String = ""

        If IndiceInstanciaDefecto = -1 Then
            vTimeOutConfig = ListaInstancias(0).TimeOutConBD
            ListaInstancias(0).TimeOutConBD = 5
            CadenaConexion = ListaInstancias(0).CadenaConexionSQLClient
        Else
            vTimeOutConfig = ListaInstancias(IndiceInstanciaDefecto).TimeOutConBD
            ListaInstancias(IndiceInstanciaDefecto).TimeOutConBD = 5
            CadenaConexion = ListaInstancias(IndiceInstanciaDefecto).CadenaConexionSQLClient
        End If

        Dim lConnection As New SqlClient.SqlConnection(CadenaConexion)

        Try

            lConnection.Open()
            lConnection.Close()

            Abrio_Conexion = True

        Catch ex1 As SqlClient.SqlException
            Throw ex1
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", "Error al intenar conectarse a la base de datos: {2} ", ex.Message, CadenaConexion))
        Finally
            Try
                lConnection.Dispose()
                ListaInstancias(0).TimeOutConBD = vTimeOutConfig
            Catch ex As Exception
            End Try
        End Try

    End Function

    Private Function Identical_Instance() As Boolean

        'Open arrays with length determined by number of instances
        Dim Str1(Instances.Length - 1) As String
        Dim Str2(Instances.Length - 1) As String

        'Final string in message box
        Dim MsgString As String = Nothing

        For i = 0 To Instances.Length - 1
            'For each instance store related info
            Str1(i) = "ID: " & Instances(i).Id
            Str2(i) = "    Handle: " & Instances(i).Handle.ToInt32

            'Join strings and carriage return & append to message string
            MsgString = MsgString & Str1(i) & Str2(i) & Chr(13)
        Next

        'Display complete message string (qty of instances = qty of lines of data)
        '        MsgBox(MsgString)

        If Instances.Length = 1 Then
            Return False
        Else
            'Assuming this will prevent any more than 1 instance from opening,
            'it should not get beyond Instances(0) and Instances(1)
            If Instances(0).Handle = Instances(1).Handle Then
                'If handles are the same
                Return True
            Else
                'If handles are different
                Return False
            End If
        End If

    End Function

End Module
