Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Text
Imports System.Threading
Imports DevExpress.LookAndFeel
Imports DevExpress.XtraEditors
Imports DevExpress.XtraRichEdit.API.Native
Imports DevExpress.XtraSplashScreen
Imports TOMWMS.clsDataContractDI
Imports TOMWMS.wsTOMHH

Public Class frmLoginEx

    Private lUsuario As New clsLnUsuario
    Private Usuario As New clsBeUsuario
    Private AbrioIni As Boolean = False
    Private IsLoading As Boolean = True
    Private MenuP As New frmMenu()
    Private ListaInstancias As New List(Of clsCadenaConexion)
    Public Property ModoDTS As Boolean = False

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub GetIPAddress()

        Try

            AP.HostName = Net.Dns.GetHostName()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Actualiza_APP_Config()

        Try


            My.Settings.IMS4MB_ConnectionStringConfigurable = clsBD.Instancia.CadenaConexionSQLClient
            My.MySettings.Default.IMS4MB_QAConnectionStringPrograN = clsBD.Instancia.CadenaConexionSQLClient
            My.Settings.IMS4MB_PRDConnectionString = clsBD.Instancia.CadenaConexionSQLClient

            Configuration.ConfigurationManager.AppSettings("CST") = clsBD.Instancia.CadenaConexionSQLClient
            Configuration.ConfigurationManager.AppSettings("EFREN_IMS4MB_CEALSA_PRD_Connection") = clsBD.Instancia.CadenaConexionSQLClient
            Configuration.ConfigurationManager.AppSettings("IMS4MB_QAConnectionStringPrograN") = clsBD.Instancia.CadenaConexionSQLClient
            Configuration.ConfigurationManager.AppSettings("IMS4MB_PRDConnectionString") = clsBD.Instancia.CadenaConexionSQLClient
            Configuration.ConfigurationManager.AppSettings("CST_ERP") = clsBD.Instancia.Cadena_Conexion_SQL_ERP
            Configuration.ConfigurationManager.AppSettings("WMS_MODO_DEBUG") = IIf(clsBD.Instancia.Modo_Debug, "ON", "OFF")
            Configuration.ConfigurationManager.AppSettings("WMS_RESERVA_MI3_TRACE") = IIf(clsBD.Instancia.Modo_Debug, "ON", "OFF")
            'clsBD.Instancia.WSTOMHH
            If clsBD.Instancia.WSTOMHH.Trim <> "" Then

                Dim BasicHttpBinding As ServiceModel.BasicHttpBinding = New ServiceModel.BasicHttpBinding
                Dim address As ServiceModel.EndpointAddress = New ServiceModel.EndpointAddress(clsBD.Instancia.WSTOMHH)
                wsTOMHHInstance = New TOMHHWSSoapClient(BasicHttpBinding, address)

            Else
                wsTOMHHInstance = Nothing
            End If


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmLogin_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        Try

            If e.Control = True AndAlso e.KeyCode = Keys.I Then

                If XtraMessageBox.Show("¿Abrir archivo de configuración?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Dim a As New frmEditorIni
                    a.WindowState = FormWindowState.Maximized
                    a.ShowDialog()
                    AbrioIni = True

                End If

            ElseIf e.Control = True AndAlso e.KeyCode = Keys.U Then

                If XtraMessageBox.Show(String.Format("{0}{1}¿Abrir ruta de origen?", CurDir(), vbNewLine), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Process.Start(CurDir(), IO.FileMode.Open)
                End If

            ElseIf e.Control = True AndAlso e.KeyCode = Keys.Enter Then
                txtUsuario.Text = "dts"
                txtContraseña.Text = "dts1965!"
                ModoDTS = True
                cmdIngresar_Click(Nothing, Nothing)
            ElseIf e.Control = True AndAlso e.KeyCode = Keys.T Then
                Dim ColView As New frmColumViewervb
                ColView.ShowDialog()
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmLogin_Activated(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Activated
        txtUsuario.Focus()
    End Sub

    Private Sub Listar_Empresas()

        Try

            If IMS.Listar_Empresas(cmbEmpresa) Then

                If AP.Listar_BodegasLogin(cmbBodegas) Then
                    txtUsuario.Focus()
                Else

                    XtraMessageBox.Show("No hay bodegas definidas para la empresa: " & cmbEmpresa.Text, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Dim Wz As New frmAsistente() With {.EmpresaConfigurada = True, .BodegaConfigurada = False, .UsuariosConfigurados = False}
                    Dim Res As DialogResult = Wz.ShowDialog()

                    If Res = DialogResult.Cancel Then
                        Close()
                    End If

                End If

            Else

                XtraMessageBox.Show("No hay empresas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Dim Wz As New frmAsistente() With {.EmpresaConfigurada = False, .BodegaConfigurada = False, .UsuariosConfigurados = False}
                Dim Res As DialogResult = Wz.ShowDialog()

                If Res = DialogResult.Cancel Then
                    Close()
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Function Abrio_Conexion(ByVal IndiceInstanciaDefecto As Integer,
                                   ByVal ListaInstancias As List(Of clsCadenaConexion)) As Boolean

        Abrio_Conexion = False

        '#EJC20171031_1259AM_REF: Cambiar timeOut de archivo de configuración para verificación de conexión a BD de forma temporal.
        Dim vTimeOutConfig As Double
        Dim CadenaConexion As String = ""

        If IndiceInstanciaDefecto = -1 Then
            vTimeOutConfig = ListaInstancias(0).TimeOutConBD
            ListaInstancias(0).TimeOutConBD = 10
            CadenaConexion = ListaInstancias(0).CadenaConexionSQLClient
        Else
            vTimeOutConfig = ListaInstancias(IndiceInstanciaDefecto).TimeOutConBD
            ListaInstancias(IndiceInstanciaDefecto).TimeOutConBD = 10
            CadenaConexion = ListaInstancias(IndiceInstanciaDefecto).CadenaConexionSQLClient
        End If

        Dim lConnection As New SqlConnection
        lConnection.ConnectionString = CadenaConexion

        Try

            lConnection.Open()
            lConnection.Close()

            Abrio_Conexion = True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", "Error al intenar conectarse a la base de datos: ", ex.Message))
        Finally
            lConnection.Dispose()
            ListaInstancias(0).TimeOutConBD = vTimeOutConfig
        End Try

    End Function

    Private Function InfOK() As Boolean


        Dim vFecha As Date = Now
        Dim vFecha2 As Date = vFecha.AddDays(2)

        InfOK = False

        If cmbEmpresa.Text = "" Then
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show("Seleccione la Empresa.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : cmbEmpresa.Focus()
        ElseIf cmbBodegas.Text = "" Then
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show("Seleccione una Bodega válida.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : cmbBodegas.Focus()
        ElseIf Trim(txtUsuario.Text) = "" Then
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show("Ingrese Código de Usuario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : txtUsuario.Focus()
        ElseIf Trim(txtContraseña.Text) = "" Then
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show("Ingrese Contraseña", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : txtContraseña.Focus()
        Else : InfOK = True
        End If

    End Function

    Private Sub txtUsuario_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtUsuario.KeyPress

        If e.KeyChar = Chr(13) Then

            If txtUsuario.Text <> "" Then
                txtContraseña.SelectAll()
                txtContraseña.Focus()
            Else
                XtraMessageBox.Show("Ingrese código de usuario", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtUsuario.SelectAll()
                txtUsuario.Focus()
            End If

        End If

    End Sub

    Private Sub cmbEmpresa_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles cmbEmpresa.KeyDown
        If e.KeyCode = Keys.Enter Then cmbBodegas.Focus()
    End Sub

    Private Sub cmdLogin_Click(sender As Object, e As EventArgs) Handles mnuIngresar.ItemClick

        Try


            If Not clsBD.Instancia.Modo_Debug Then
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Iniciando sesión...")
            End If

            If InfOK() Then

                If Licenciamiento_Activo() Then

                    AP.IdEmpresa = cmbEmpresa.EditValue
                    AP.IdBodega = cmbBodegas.EditValue
                    AP.NomBodega = cmbBodegas.Text
                    AP.NomEmpresa = cmbEmpresa.Text
                    AP.IdConfiguracionInterface = clsLnI_nav_config_enc.Get_IdConfiguracion(AP.IdBodega, AP.IdEmpresa)

                    Usuario.Codigo = txtUsuario.Text
                    Usuario.Clave = txtContraseña.Text
                    Usuario.IdEmpresa = cmbEmpresa.EditValue

                    AP.Empresa.IdEmpresa = AP.IdEmpresa
                    clsLnEmpresa.GetSingle(AP.Empresa)

                    If ModoDTS Then
                        If clsLnUsuario.Get_Usuario_Valido_For_DTS(Usuario, cmbBodegas.EditValue) Then

                        End If
                    End If

                    If clsLnUsuario.Usuario_Valido(Usuario, AP.IdBodega) Then

                        If AP.Empresa.Generar_Stock_Jornada Then

                            If Not clsBD.Instancia.Modo_Debug Then
                                SplashScreenManager.Default.SetWaitFormDescription("Generando jornada de sistema...")
                            End If

                            '#EJC20210622: Run on background inside the form
                            Dim A As Thread = New Thread(AddressOf Task_Jornada)
                            A.Start()

                        End If

                        '#EJC20210317: Obtener el objeto de bodega para tener todos sus parámetros y configuraciones.
                        AP.Bodega = clsLnBodega.GetSingle_By_Idbodega(AP.IdBodega)

                        If Not clsBD.Instancia.Modo_Debug Then

                            If SplashScreenManager.Default Is Nothing Then
                                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                            End If

                            SplashScreenManager.Default.SetWaitFormDescription("Actualizando monitor de tareas...")

                        End If

                        AP.IdRol = Usuario.IdRol

                        AP.Exigir_Politica_Contraseñas = clsLnEmpresa.Exigir_Politica_Contraseñas(AP.IdEmpresa)

                        Dim t1 As Thread = New Thread(AddressOf Actualiza_Ultimo_Ingreso)
                        t1.Start()

                        AP.UsuarioAp = Usuario
                        AP.InterfaceSAP = clsLnI_nav_config_enc.Get_Interface_SAP(AP.IdConfiguracionInterface)

                        Hide()

                        Try
                            MenuP.lblUsuario.Caption = AP.UsuarioAp.Nombres
                            MenuP.ShowDialog()
                            If Not MenuP Is Nothing Then MenuP.Dispose()
                        Catch ex As Exception
                            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
                        End Try

                        Usuario = Nothing : lUsuario = Nothing

                        Close()

                    Else
                        SplashScreenManager.CloseForm(False)
                        XtraMessageBox.Show("Usuario o contraseña incorrecta!", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtUsuario.SelectAll() : txtUsuario.Focus()
                        ActiveControl = txtUsuario
                    End If

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Public Sub Task_Jornada()
        Dim frmJornada As New frmJornadaSistema()
        frmJornada.WindowState = FormWindowState.Maximized
        frmJornada.CloseBox = False
        frmJornada.SendToBack()
        Application.Run(frmJornada)
    End Sub

    Private Sub Actualiza_Ultimo_Ingreso()
        clsLnUsuario.Actualiza_Ultimo_Ingreso(Usuario)
    End Sub

    Shared Function ObtenerDominioUsuario() As String
        If TypeOf My.User.CurrentPrincipal Is
          Security.Principal.WindowsPrincipal Then
            ' My.User is using Windows authentication.
            ' The name format is DOMAIN\USERNAME.
            Dim parts() As String = Split(My.User.Name, "\")
            Dim domain As String = parts(0)
            Return domain
        Else
            ' My.User is using custom authentication.
            Return ""
        End If
    End Function

    Private Sub BarHeaderItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdLinkDTS.ItemClick
        Process.Start("http://www.dts.com.gt")
    End Sub

    Private Sub Cambia_Mandante()

        If IsLoading Then Exit Sub

        Try

            UseWaitCursor = True

            If ListaInstancias.Count > 0 Then

                If cmbInstancia.ItemIndex <> -1 Then

                    Set_Parametros_Servidor(cmbInstancia.ItemIndex, ListaInstancias)

                    Dim IndiceInstanciaDefecto As Integer = cmbInstancia.ItemIndex

                    If Abrio_Conexion(IndiceInstanciaDefecto, ListaInstancias) Then

                        gIndiceInstancia = IndiceInstanciaDefecto

                        Actualiza_APP_Config()

                        Listar_Empresas()

                        Get_Lista_Alias_Campos()

                        'Deshabilitado para desarrollo por #EJC20170706
                        'clsTabla.Verificar_Tablas_Sistema()
                        ' MsgBox("Mandante actualizado", MsgBoxStyle.Information)

                    End If

                End If

            End If

        Catch ex As Exception
            If ex.HResult = -2146233088 OrElse ex.HResult = -2146232060 Then
                XtraMessageBox.Show("La instancia seleccionada no está disponible o no es accesible, verifique datos de conexion para :" & clsBD.Instancia.Server, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Throw ex
            End If
        Finally
            UseWaitCursor = False
        End Try

    End Sub

    Private Sub Get_Lista_Alias_Campos()

        Try

            lConfiguracionAliasCampos = clsLnConfiguracion_alias_campos.Get_All()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub Set_Parametros_Servidor(ByVal IndiceInstanciaDefecto As Integer, ByVal ListaInstancias As List(Of clsCadenaConexion))

        Try

            If IndiceInstanciaDefecto <> -1 Then
                'Tomar por defecto la instancia que corresponde con el host que ejecuta.
                clsBD.Instancia = ListaInstancias(IndiceInstanciaDefecto)
                gIndiceInstancia = IndiceInstanciaDefecto
            Else
                'Tomar por defecto la primera instancia.
                clsBD.Instancia = ListaInstancias(0)
            End If

            mnuBD.Caption = String.Format("{0}/{1}", clsBD.Instancia.Server, clsBD.Instancia.NombreBD)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
             Text,
             MessageBoxButtons.OK,
             MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmLoginEx_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CheckForIllegalCrossThreadCalls = False

        Try

            GetIPAddress()

            lblNombrePCCliente.Caption = AP.HostName

            If Not AP.Existe_Ini() Then

                Dim c As New frmIniWCF
                c.ShowDialog()
                c.Dispose()

                If AP.Existe_Ini() Then
                    Application.Restart()
                Else
                    Close()
                End If

            Else

                Dim IndiceInstanciaDefecto As Integer = -1

                ListaInstancias = clsPublic.Leer_Archivo_Configuracion_Ini(IndiceInstanciaDefecto)

                AP.Nombre_Skin = clsPublic.Leer_Archivo_Configuracion_Skin()

                If Not (AP.Nombre_Skin = "") Then

                    Try
                        Dim defaultLF As UserLookAndFeel = UserLookAndFeel.Default
                        defaultLF.SkinName = AP.Nombre_Skin
                    Catch ex As Exception
                        XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                       Text,
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation)
                    End Try

                End If


                If ListaInstancias Is Nothing Then

                    '#CKFK 20180509 07:38 Corregí mensaje, le puse el No
                    XtraMessageBox.Show("No se encontró ninguna cadena de conexión válida en el archivo ini. se cerrará la aplicación",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)

                    Close()

                Else

                    '#EJC20180508: Refactoring
                    Set_Parametros_Servidor(IndiceInstanciaDefecto, ListaInstancias)

                    cmbInstancia.Properties.DataSource = ListaInstancias
                    cmbInstancia.Properties.DisplayMember = "NombreInstancia"
                    cmbInstancia.Properties.ValueMember = "CadenaConexionSQLClient"

                    cmbInstancia.Properties.PopulateColumns()

                    For i As Integer = 0 To cmbInstancia.Properties.Columns.Count - 1

                        If cmbInstancia.Properties.Columns(i).FieldName = "NombreInstancia" OrElse
                            cmbInstancia.Properties.Columns(i).FieldName = "Server" OrElse
                            cmbInstancia.Properties.Columns(i).FieldName = "NombreBD" Then
                            cmbInstancia.Properties.Columns(i).Visible = True
                        Else
                            cmbInstancia.Properties.Columns(i).Visible = False
                        End If

                    Next

                    cmbInstancia.Properties.Columns("Usuario").Visible = False
                    cmbInstancia.Properties.Columns("Clave").Visible = False
                    cmbInstancia.Properties.Columns("Seguridad_Integrada").Visible = False
                    cmbInstancia.Properties.Columns("IpWCF").Visible = False
                    cmbInstancia.Properties.Columns("TimeOutConBD").Visible = False
                    cmbInstancia.Properties.Columns("CadenaConexionSQLClient").Visible = False

                    If Abrio_Conexion(IndiceInstanciaDefecto, ListaInstancias) Then

                        'Deshabilitado para desarrollo por #EJC20170706
                        'clsTabla.Verificar_Tablas_Sistema()

                        If IndiceInstanciaDefecto <> -1 Then

                            gIndiceInstancia = IndiceInstanciaDefecto

                            cmbInstancia.ItemIndex = IndiceInstanciaDefecto
                            lblInstancia.ForeColor = Color.Firebrick
                        Else
                            gIndiceInstancia = 0
                            cmbInstancia.ItemIndex = 0
                            lblInstancia.ForeColor = Color.Black
                        End If

                        Actualiza_APP_Config()

                        If Licenciamiento_Activo() Then

                            Dim t1 As Thread = New Thread(AddressOf Registra_Ingreso)
                            t1.Start()

                            Listar_Empresas()

                            Get_Lista_Alias_Campos()

                        Else
                            XtraMessageBox.Show("Licenciamiento inactivo",
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
                            Close()
                        End If

                    End If

                End If

            End If

        Catch ex1 As SqlClient.SqlException

            If ex1.HResult = -2146233088 OrElse ex1.HResult = -2146232060 Then

                If Not ListaInstancias Is Nothing Then
                    XtraMessageBox.Show("No se pudo abrir la conexión a la base de datos con los parámetros de conexión para la instancia: " & ListaInstancias(0).NombreInstancia,
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)

                    Close()

                Else
                    XtraMessageBox.Show("No se pudo abrir la conexión a la base de datos los parámetros de conexión están vacíos",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)
                End If

            Else
                XtraMessageBox.Show("Error: " & ex1.Message,
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            End If

        Catch ex As Exception

            XtraMessageBox.Show("Error: " & ex.Message,
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)

            Close()

        Finally
            IsLoading = False
        End Try

    End Sub

    Private Sub Registra_Ingreso()

        Try

            clsLnLicencia_item.Registra_Ingreso(AP.HostName)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private lLicenciasProgramacion As New List(Of String)

    Private Function Licenciamiento_Activo() As Boolean

        Licenciamiento_Activo = False

        Try

            Dim BeLicItem As New clsBeLicencia_item()
            Dim vNombreServerLicencias As String = ""

            lLicenciasProgramacion.Add("PROGRAX")
            lLicenciasProgramacion.Add("DESARROLLO8-PC")
            lLicenciasProgramacion.Add("PROGRA12")
            lLicenciasProgramacion.Add("PROGRA14")
            lLicenciasProgramacion.Add("DESKTOP-DUB9IAH")
            lLicenciasProgramacion.Add("LAPTOP-5GDJFUCN")
            lLicenciasProgramacion.Add("DESKTOP-9U7ICLN")
            lLicenciasProgramacion.Add("ColonialWMS")
            lLicenciasProgramacion.Add("DESKTOP-790O7S2")
            lLicenciasProgramacion.Add("DESKTOP-5BM5P11")
            lLicenciasProgramacion.Add("DESKTOP-790O7S2")
            lLicenciasProgramacion.Add("Marcela1306")
            lLicenciasProgramacion.Add("AbigailAlvarado")


            '#EJC20210517: Cuando lo encuentren, 
            'agreguen sus Identificadores en la lista para que en caso de que no tenga licencia, no les pida.
            'Con cariño, Erik.
            If lLicenciasProgramacion.Contains(AP.HostName) Then
                Licenciamiento_Activo = True
                AP.LicenciaServidor = True
                Exit Function
            End If

            If clsLnLicencia_item.Existe_Servidor_De_Licencias(BeLicItem) Then

                vNombreServerLicencias = IIf(BeLicItem.Identificacion = "", BeLicItem.IdDisp, BeLicItem.Identificacion)

                Dim vMac As String = clsLnLicencia_item.Get_Mac_Host(AP.HostName)
                Dim vEsServer As Boolean = (BeLicItem.Bandera = (clsBeLicencia_item.eTipoLicencia.Server))

                '#EJC20210702: Verificar si por macadress el la maquina actual es el servidor de licencias.
                If Not vEsServer Then
                    vEsServer = (BeLicItem.IdDisp.ToString() = vMac.ToString())
                End If

                If clsLnLicencia_item.Licencia_Server_Activa(BeLicItem) Then

                    Dim StatusLicHost As clsBeLicencia_item.eEstatusLicencia = clsLnLicencia_item.Get_Estatus_Licencia_Host(AP.HostName, False)

                    '#EJC20180106: Cambie el valor BuscarMac = True si no se encuentrapor nombre de HostName
                    If StatusLicHost = clsBeLicencia_item.eEstatusLicencia.Pendiente_Solicitud Then
                        StatusLicHost = clsLnLicencia_item.Get_Estatus_Licencia_Host(AP.HostName, True)
                    End If

                    Select Case StatusLicHost

                        Case clsBeLicencia_item.eEstatusLicencia.Activa

                            Licenciamiento_Activo = True
                            AP.LicenciaServidor = vEsServer

                        Case clsBeLicencia_item.eEstatusLicencia.Pendiente_Solicitud

                            Dim pBeLicenciaItem As New clsBeLicencia_item
                            pBeLicenciaItem = clsLnLicencia_item.Get_BeLicencia_Item(AP.HostName, False)

                            '#EJC20171108_REF10_1247AM: Solicitud de licencia.
                            Dim Belicencia_solic As New clsBeLicencia_solic() With {.IdDisp = pBeLicenciaItem.IdDisp,
                                .Identificacion = pBeLicenciaItem.Identificacion,
                                .Tipo = 1} 'Tipo = 1 -> BOF

                            If Not clsLnLicencia_solic.Exist(pBeLicenciaItem.IdDisp) Then
                                clsLnLicencia_solic.Insertar(Belicencia_solic)
                            End If

                            clsLnLicencia_item.Registra_Ingreso(AP.HostName)

                            XtraMessageBox.Show(String.Format("El ordenador: {0} ha enviado una solicitud de licencia al servidor de licencias: {1}", AP.HostName, vNombreServerLicencias),
                             Text,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Information)

                            Dim frmSol As New frmLicenciaSolicitada
                            frmSol.txtHostSolicitante.Text = String.Format("{0} - {1}", AP.HostName, clsLnLicencia_item.Get_Mac_Host(AP.HostName))
                            frmSol.txtServidorLicencias.Text = BeLicItem.Identificacion

                            If frmSol.ShowDialog() = DialogResult.OK Then

                                XtraMessageBox.Show("Licencia activada correctamente, se reiniciará la aplicación.",
                                                    Text,
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information)

                                Application.Restart()

                            End If

                        Case Else
                            Exit Select
                    End Select

                Else 'de Licencia_Activa(

                    Dim LicVen As New frmLicVencida()
                    LicVen.FechaLicencia = BeLicItem.Vence
                    LicVen.FechaServer = clsServidor.Get_Fecha_Servidor()
                    LicVen.HostEsNuevoServidorDeLicencias = True

                    If Not LicVen.ShowDialog() = DialogResult.OK Then
                        Close()
                    Else
                        XtraMessageBox.Show("Licencia activada correctamente, se reiniciará la aplicación.",
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information)
                        Application.Restart()
                    End If

                End If 'de Licencia_Activa

            Else 'de Existe_Servidor_De_Licencias(

                If XtraMessageBox.Show("No existe un servidor de licencias activo, ¿Registrar éste ordenador como servidor de licencias?",
                Text,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) = DialogResult.Yes Then

                    Dim frmSol As New frmLicSolicitud
                    frmSol.Modo = frmLicSolicitud.pModo.SRV
                    frmSol.mac = clsLnLicencia_item.Get_Mac_Host(AP.HostName)

                    If frmSol.ShowDialog() = DialogResult.OK Then
                        Licenciamiento_Activo = True
                    Else
                        Close()
                    End If

                Else
                    Close()
                End If

            End If 'de Existe_Servidor_De_Licencias(

            clsLnLicencia_item.Registra_Ingreso(AP.HostName)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
             Text,
             MessageBoxButtons.OK,
             MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub cmbInstancia_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbInstancia.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbEmpresa.Focus()
        End If
    End Sub

    Private Sub frmLoginEx_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            '#EJC20210714: Desplegar versión y fecha del publicable en login.
            If My.Application.IsNetworkDeployed Then
                '#EJC20220321:Manejar manualmente el número de la versión, en la variable gVersion en Globals.
                'gVersion = My.Application.Deployment.CurrentVersion.ToString()
                cmdVersion4.Caption = gVersionApp & " " & FormatoFechas.tFecha(gFechaVersion)
            Else
                cmdVersion4.Caption = gVersionApp & " " & FormatoFechas.tFecha(gFechaVersion)
            End If

            '#EJC20220404: Deshabilitado por las burocracias de las conectividades.
            'ShowReleaseNotes()            

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
             Text,
             MessageBoxButtons.OK,
             MessageBoxIcon.Exclamation)
        End Try

        txtUsuario.Focus()

        Application.DoEvents()

    End Sub

    Private Sub txtContraseña_KeyDown(sender As Object, e As KeyEventArgs) Handles txtContraseña.KeyDown

        If e.KeyCode = Keys.Enter Then
            If txtContraseña.Text <> "" Then
                cmdLogin_Click(Nothing, Nothing)
            Else
                XtraMessageBox.Show("Ingrese clave de usuario",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
                txtContraseña.SelectAll()
                txtContraseña.Focus()
            End If
        End If

    End Sub

    Private Sub txtUsuario_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUsuario.KeyDown

        If e.KeyCode = Keys.Enter Then

            If txtUsuario.Text.Trim <> "" Then
                txtContraseña.Focus()
            End If
        End If

    End Sub

    Private Sub cmbInstancia_EditValueChanged(sender As Object, e As EventArgs) Handles cmbInstancia.EditValueChanged

        Try

            Cambia_Mandante()

        Catch ex1 As SqlClient.SqlException

            If ex1.HResult = -2146233088 Then

                If Not ListaInstancias Is Nothing Then
                    XtraMessageBox.Show("No se pudo abrir la conexión a la base de datos con los parámetros de conexión para la instancia: " & ListaInstancias(0).NombreInstancia,
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)
                Else
                    XtraMessageBox.Show("No se pudo abrir la conexión a la base de datos los parámetros de conexión están vacíos",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)
                End If

            Else
                XtraMessageBox.Show("Error: " & ex1.Message,
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            End If
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmbEmpresa_EditValueChanged(sender As Object, e As EventArgs) Handles cmbEmpresa.EditValueChanged
        If cmbEmpresa.EditValue <> 0 Then
            AP.IdEmpresa = cmbEmpresa.EditValue
            AP.Listar_BodegasLogin(cmbBodegas)
        End If
    End Sub

    Private Sub cmbBodegas_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbBodegas.KeyDown
        If e.KeyCode = Keys.Enter Then
            If cmbBodegas.Text.Trim <> "" Then
                txtUsuario.Focus()
            Else
                XtraMessageBox.Show("Seleccione una Bodega válida", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbBodegas.Focus()
            End If
        End If
    End Sub

    Private Sub cmdIngresar_Click(sender As Object, e As EventArgs) Handles cmdIngresar.Click

        Try
            cmdLogin_Click(sender, e)
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdSalir_Click(sender As Object, e As EventArgs) Handles cmdSalir.Click
        Close()
    End Sub

    Private Sub frmLoginEx_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        Try
            Environment.Exit(Environment.ExitCode)
        Catch ex As Exception
            Debug.WriteLine("Error al salir. " & ex.Message)
        End Try

    End Sub

    Private Sub LabelControl5_Click(sender As Object, e As EventArgs) Handles LabelControl5.Click
        txtContraseña.Properties.PasswordChar = ""
    End Sub

    Private Sub LabelControl1_Click(sender As Object, e As EventArgs) Handles LabelControl1.Click

        Dim Dash1 As New frmDashViewer1
        Dash1.ShowDialog()
        Dash1.Dispose()

    End Sub

    Private Sub Actualizar_Lotes_Magicos()

        'If XtraMessageBox.Show("¿Corregir lotes?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

        '    Dim lLotesACorregir As New List(Of clsBeProducto_lote_correccion)
        '    lLotesACorregir = clsLnProducto_lote_correccion.Get_All()

        '    Dim lStockCoincidencias As New List(Of clsBeStock)
        '    Dim lStockListaACorregir As New List(Of clsBeProducto_lote_correccion)

        '    Dim BeStock As New clsBeStock
        '    Dim BeProductoLoteCorreccion As New clsBeProducto_lote_correccion

        '    Dim vIndice As Integer = 0

        '    If Not lLotesACorregir Is Nothing Then

        '        For Each L In lLotesACorregir

        '            lStockCoincidencias = clsLnStock.Get_All_By_NoLote(L.Codigo, L.Lote)

        '            If Not lStockCoincidencias Is Nothing Then

        '                For Each Encontrado In lStockCoincidencias

        '                    vIndice = lStockListaACorregir.FindIndex(Function(x) x.IdStock = Encontrado.IdStock)

        '                    If vIndice = -1 Then

        '                        BeProductoLoteCorreccion = New clsBeProducto_lote_correccion
        '                        BeProductoLoteCorreccion.IdStock = Encontrado.IdStock
        '                        BeProductoLoteCorreccion.FechaVenceActual = Encontrado.Fecha_vence
        '                        BeProductoLoteCorreccion.Vence = L.Vence
        '                        lStockListaACorregir.Add(BeProductoLoteCorreccion)

        '                    End If

        '                Next

        '            End If

        '        Next


        '    End If

        '    If Not lStockListaACorregir Is Nothing Then

        '        Dim vREgistrosAfectados As Integer = clsLnStock.Corregir_Lotes(lStockListaACorregir)

        '        XtraMessageBox.Show("Lotes actualizados, se encontraron " & vREgistrosAfectados & " Registros.")

        '    End If


        'End If

    End Sub

    Private Sub mnuBD_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuBD.ItemClick

        'Try

        '    If XtraMessageBox.Show("¿Actualizar base de datos?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
        '        '#EJC20220801_Actualización_Automática_De_BD
        '        Update_Database_Version()
        '    End If

        'Catch ex As Exception

        '    XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
        '    Text,
        '    MessageBoxButtons.OK,
        '    MessageBoxIcon.Exclamation)

        'End Try        

    End Sub

    Private Sub ShowReleaseNotes()

        Try

            Dim vRutaReleaseNotes = CurDir() & "/ReleaseNotes_" & gVersionApp & ".txt"

            If Not IO.File.Exists(vRutaReleaseNotes) Then

                If clsPublic.isOnline() Then

                    Dim myBotNewVersionURL As String = "https://raw.githubusercontent.com/ejcalderongt/DBA/master/ReleaseNotes_" & gVersionApp
                    Dim myBotNewVersionClient As Net.WebClient = New Net.WebClient()
                    Dim stream As IO.Stream = Nothing

                    Try
                        stream = myBotNewVersionClient.OpenRead(myBotNewVersionURL)
                    Catch ex As Exception
                        If ex.Message.Contains("Error en el servidor remoto: (404) No se encontró.") Then
                            Dim vMsgError As String = ex.Message
                            clsLnLog_error_wms.Agregar_Error(vMsgError)
                        Else
                            Throw ex
                        End If
                    End Try

                    If Not stream Is Nothing Then

                        Dim reader As IO.StreamReader = New IO.StreamReader(stream)
                        Dim content As String = reader.ReadToEnd()
                        Dim sb = New StringBuilder(content.Length)

                        For Each i As Char In content

                            If i = vbLf Then
                                sb.Append(Environment.NewLine)
                            ElseIf i <> vbCr AndAlso i <> vbTab Then
                                sb.Append(i)
                            End If

                        Next

                        content = sb.ToString()

                        Dim file As IO.StreamWriter
                        file = My.Computer.FileSystem.OpenTextFileWriter(vRutaReleaseNotes, True)
                        file.WriteLine(content)
                        file.Close()

                        Dim frmRN As New ReleaseNotesReview
                        frmRN.RichEditControl1.Text = ""

                        Dim Range1 As DocumentRange = frmRN.RichEditControl1.Document.AppendText("Release Notes - " & gVersionApp)
                        Dim cp1 As CharacterProperties = frmRN.RichEditControl1.Document.BeginUpdateCharacters(Range1)
                        cp1.FontName = "Courier New"
                        cp1.FontSize = 14
                        cp1.ForeColor = Color.Black
                        cp1.Bold = True
                        frmRN.RichEditControl1.Document.EndUpdateCharacters(cp1)

                        Dim pp1 As ParagraphProperties = frmRN.RichEditControl1.Document.BeginUpdateParagraphs(Range1)
                        ' Center a paragraph
                        pp1.Alignment = ParagraphAlignment.Center
                        ' Set triple spacing
                        pp1.LineSpacingType = ParagraphLineSpacing.Multiple
                        pp1.LineSpacingMultiplier = 3
                        'Finalize the update
                        frmRN.RichEditControl1.Document.EndUpdateParagraphs(pp1)

                        frmRN.RichEditControl1.Document.AppendText(vbNewLine)

                        Dim Range As DocumentRange = frmRN.RichEditControl1.Document.AppendText(content)
                        Dim cp As CharacterProperties = frmRN.RichEditControl1.Document.BeginUpdateCharacters(Range)
                        cp.FontName = "Courier New"
                        cp.FontSize = 10
                        cp.ForeColor = Color.Black

                        Dim pp As ParagraphProperties = frmRN.RichEditControl1.Document.BeginUpdateParagraphs(Range)
                        ' Center a paragraph
                        pp.Alignment = ParagraphAlignment.Justify
                        ' Set triple spacing
                        pp.LineSpacingType = ParagraphLineSpacing.Multiple
                        pp.LineSpacingMultiplier = 2
                        'pp.LineSpacing = 1.5

                        ' Set left indent at 0.5".
                        ' Default unit is 1/300 of an inch (a document unit).
                        'pp.LeftIndent = Units.InchesToDocumentsF(0.5F)

                        ' Set a tab stop at 1.5"
                        Dim tbiColl As TabInfoCollection = pp.BeginUpdateTabs(True)
                        Dim tbi As TabInfo = New TabInfo()
                        tbi.Alignment = TabAlignmentType.Center
                        'tbi.Position = Units.InchesToDocumentsF(1.5F)
                        tbiColl.Add(tbi)
                        pp.EndUpdateTabs(tbiColl)

                        'Finalize the update
                        frmRN.RichEditControl1.Document.EndUpdateParagraphs(pp)

                        frmRN.RichEditControl1.Document.EndUpdateCharacters(cp)
                        frmRN.ShowDialog()

                    End If

                Else
                    'XtraMessageBox.Show("El ordenador no puede llegar hasta el servidor para descargar las notas de liberación",
                    '                      Text,
                    '                      MessageBoxButtons.OK,
                    '                      MessageBoxIcon.Exclamation)
                End If

            End If

            'Console.WriteLine(content)

        Catch ex As Exception
            '#EJC202211091749
            'XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            'Text,
            'MessageBoxButtons.OK,
            'MessageBoxIcon.Exclamation)

        End Try

    End Sub

    Private Sub BarButtonItem5_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem5.ItemClick

        Try

            'Dim frmRV As New frmReglaVence
            'frmRV.ShowDialog()
            'Dim nombre As String = "GAMABENCENO Plus 1% CHAMPU 60 ML<>&''"";,....ñÑ°"
            'MsgBox("original: " & nombre)
            'nombre = clsPublic.Quitar_Caracteres_No_Permitidos("GAMABENCENO Plus 1% CHAMPU 60 ML<>&''"";,....ñÑ°")
            'MsgBox(nombre)

            'Enviar_Transacciones_De_Salida(clsDataContractDI.tTipoDocumentoSalida.Transferencia_Directa)

            'GenerateVoicePickCode("17404003900174", "000455", "250225")

            'Dim FRM As New FrmNotificacionEventoMnt
            'FRM.ShowDialog()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs)
        Dim nombre As String = clsPublic.Quitar_Caracteres_No_Permitidos("GAMABENCENO Plus 1% CHAMPU 60 ML<>&''"";,....ñÑ")
        MsgBox(nombre)
    End Sub

    Public Sub Enviar_Transacciones_De_Salida(ByVal pTipo As tTipoDocumentoSalida)

        Dim lTransaccionesSalida As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesSalidaSingle As New List(Of clsBeI_nav_transacciones_out)

        Dim lTransaccionesSalidaReproceso As New List(Of clsBeI_nav_transacciones_out)
        'Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        'Dim Sl As New clsSyncLotes()
        Dim vContadorReproceso As Integer = 0
        Dim lTransPtPendienteRegistroEnNav As New List(Of clsBeTrans_pe_enc)

        Try

            'CnnLog.Open()

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(pTipo)

            If Not lTransaccionesSalida Is Nothing AndAlso lTransaccionesSalida.Count > 0 Then

                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc})
                Dim Enviado_A_Erp As Boolean = False '#CKFK 20180518 10:20 PM Agregué la validación de si el registro ya fue enviado o no a NAV

                For Each PT In ListaPedidosTransf

                    If PT.No_pedido = "" Then
                        Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP_By_IdPedidoEnc(PT.Idpedidoenc)
                    Else
                        Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP_By_Referencia_TipoDoc(PT.No_pedido, pTipo)
                    End If

                    If Not Enviado_A_Erp Then

                        If PT.No_pedido = "" Then
                            lTransaccionesSalidaSingle = lTransaccionesSalida.FindAll(Function(x) x.Idpedidoenc = PT.Idpedidoenc)
                        Else
                            lTransaccionesSalidaSingle = lTransaccionesSalida.FindAll(Function(x) x.No_pedido = PT.No_pedido)
                        End If

                        Dim DistinctIdPedidoEncByTraslado = lTransaccionesSalidaSingle.Where(Function(x) x.IdTipoDocumento = tTipoDocumentoSalida.Transferencia_Directa AndAlso x.Enviado = False).
                                                               GroupBy(Function(x) New With {Key x.Idpedidoenc, Key x.Codigo_producto, Key x.No_pedido}).
                                                               Select(Function(g) New With {
                                                                   .IdPedidoEnc = g.Key.Idpedidoenc,
                                                                   .Codigo_Producto = g.Key.Codigo_producto,
                                                                   .No_Pedido = g.Key.No_pedido,
                                                                   .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                                                               }).ToList()

                    End If

                Next

            Else

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

End Class
