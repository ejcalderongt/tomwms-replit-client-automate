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

            'Args = New String() {"20-2-0-1-1-0-LA_CUMBRE_LOCAL_QA"}

            'No vienen parámetros
            If Args.Length = 0 Then

                Init_App()

                RemoteCallBack = False
                MenuPrincipal = New frmMenu(IdConfiguracion)
                'MsgBox("No hay argumento remoto: ")
            Else

                RemoteCallBack = True

                arAgs = Args(0).Split("-")

                InterfaceAEjecutar = Trim(arAgs(0))
                IdConfiguracion = arAgs(1)
                IndiceInstanciaDefecto = arAgs(2)
                IdUsuario = arAgs(3)
                NoDocEntrySAP = arAgs(4)
                EstadoEnviadoSAP = arAgs(5)
                gNombreInstancia = arAgs(6)

                Init_App()

                If InterfaceAEjecutar = "" Then
                    MenuPrincipal = New frmMenu(IdConfiguracion)
                Else
                    MenuPrincipal = New frmMenu(InterfaceAEjecutar, IdConfiguracion, IdUsuario)
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

    Sub Main_Ante(ByVal Args As String())

        Try

            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)

            'No vienen parámetros
            If Args.Length = 0 Then
                RemoteCallBack = False
                MenuPrincipal = New frmMenu
                'MsgBox("No hay argumento remoto: ")
            Else
                Dim InterfaceAEjecutar As String = Args(0)
                MenuPrincipal = New frmMenu(InterfaceAEjecutar)
                RemoteCallBack = True
            End If

            If Init_App() Then
                Application.Run(MenuPrincipal) ' I can reference 'mainView' from anywhere in my app, toggle its Visible  property etc.               
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("Error al iniciar la interface: {0} {1}, Ruta Ini: {2}", MethodBase.GetCurrentMethod.Name(), ex.Message, CurDir() & "/" & "Conn.ini"),
            "Interface",
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Sub Main_ant2(ByVal Args As String())

        Dim arAgs As String() = Nothing
        Dim InterfaceAEjecutar As String = ""
        Dim IdConfiguracion As String = ""

        Try

            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)

            'No vienen parámetros
            If Args.Length = 0 Then

                RemoteCallBack = False
                MenuPrincipal = New frmMenu
                'MsgBox("No hay argumento remoto: ")
            Else

                RemoteCallBack = True

                arAgs = Args(0).Split("-")

                InterfaceAEjecutar = Trim(arAgs(0))
                IdConfiguracion = arAgs(1)

                If InterfaceAEjecutar = "" Then
                    MenuPrincipal = New frmMenu(IdConfiguracion)
                Else
                    MenuPrincipal = New frmMenu(InterfaceAEjecutar, IdConfiguracion)
                End If

            End If

            If Init_App() Then
                Application.Run(MenuPrincipal) ' I can reference 'mainView' from anywhere in my app, toggle its Visible  property etc.               
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("Error al iniciar la interface: {0} {1}, Ruta Ini: {2}, Remoto {3}, Argumentos {4}", MethodBase.GetCurrentMethod.Name(), ex.Message, CurDir() & "\" & "Conn.ini", RemoteCallBack, Args(0)),
            "Interface",
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
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

                lInstancias = clsPublic.Leer_Archivo_Configuracion_Ini(IndiceInstanciaDefecto)

                If lInstancias Is Nothing Then
                    XtraMessageBox.Show(String.Format("No se pudo procesar correctamente el archivo de configuración ini: {0}\Conn.ini", CurDir()), "Conexión a BD", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Application.Exit()
                Else

                    If IndiceInstanciaDefecto = -1 Then IndiceInstanciaDefecto = 0

                    BD.Instancia = lInstancias(IndiceInstanciaDefecto)

                    If Abrio_Conexion(lInstancias) Then

                        If IdConfiguracion = "" Then
                            IdConfiguracion = BD.Instancia.IdConfiguracionInterface
                        Else
                            BD.Instancia.IdConfiguracionInterface = IdConfiguracion
                        End If

                        Configuration.ConfigurationManager.AppSettings("CST") = BD.Instancia.CadenaConexionSQLClient

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