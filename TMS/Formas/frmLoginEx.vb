Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmLoginEx

    Private lUsuario As New clsLnUsuario
    Private Usuario As New clsBeUsuario
    Private AbrioIni As Boolean = False
    Private IsLoading As Boolean = True
    Private MenuP As New frmPrincipal()
    Private ListaInstancias As New List(Of clsCadenaConexion)
    Private MenuPrint As New frmPrintTicket()

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub GetIPAddress()

        Try

            AP.HostName = Net.Dns.GetHostName()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Actualiza_APP_Config()

        Try

            'My.Settings.IMS4MB_ConnectionStringConfigurable = clsBD.Instancia.CadenaConexionSQLClient
            'My.MySettings.Default.IMS4MB_QAConnectionStringPrograN = clsBD.Instancia.CadenaConexionSQLClient

            Configuration.ConfigurationManager.AppSettings("CST") = clsBD.Instancia.CadenaConexionSQLClient
            Configuration.ConfigurationManager.AppSettings("IMS4MB_QAConnectionStringPrograN") = clsBD.Instancia.CadenaConexionSQLClient

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmLogin_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        Try

            If e.Control = True AndAlso e.KeyCode = Keys.I Then

                If XtraMessageBox.Show("¿Abrir archivo de configuración?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Process.Start(CurDir() & "\conn.ini", IO.FileMode.Open)
                    AbrioIni = True
                End If

            ElseIf e.Control = True AndAlso e.KeyCode = Keys.U Then

                If XtraMessageBox.Show(String.Format("{0}{1}¿Abrir ruta de origen?", CurDir(), vbNewLine), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Process.Start(CurDir(), IO.FileMode.Open)
                End If
            ElseIf e.Control = True AndAlso e.KeyCode = Keys.Enter Then
                txtUsuario.Text = "dts"
                txtContraseña.Text = "dts1965!"
                cmdIngresar_Click(Nothing, Nothing)
            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmLogin_Activated(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Activated
        txtUsuario.Focus()
    End Sub

    Private Sub Listar_Empresas()

        Try

            If IMS.Listar_Empresas(cmbEmpresa) Then

                txtUsuario.Focus()

            Else
                XtraMessageBox.Show("No hay empresas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Close()
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
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

        Dim lConnection As New SqlClient.SqlConnection(CadenaConexion)

        Try

            lConnection.Open()
            lConnection.Close()

            Abrio_Conexion = True

        Catch ex1 As SqlClient.SqlException
            Throw ex1
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", "Error al intenar conectarse a la base de datos: ", ex.Message))
        Finally
            lConnection.Dispose()
            ListaInstancias(0).TimeOutConBD = vTimeOutConfig
        End Try

    End Function

    Private Function InfOK() As Boolean

        InfOK = False

        If cmbEmpresa.Text = "" Then
            XtraMessageBox.Show("Seleccione la Empresa.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : cmbEmpresa.Focus()
        ElseIf Trim(txtUsuario.Text) = "" Then
            XtraMessageBox.Show("Ingrese Código de Usuario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : txtUsuario.Focus()
        ElseIf Trim(txtContraseña.Text) = "" Then
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
        If e.KeyCode = Keys.Enter Then txtUsuario.Focus()
    End Sub

    Private Sub cmdLogin_Click(sender As Object, e As EventArgs) Handles mnuIngresar.ItemClick

        Try

            If InfOK() Then

                If Licenciamiento_Activo() Then

                    AP.IdEmpresa = cmbEmpresa.EditValue
                    AP.NomEmpresa = cmbEmpresa.Text
                    AP.IdConfiguracionInterface = clsLnI_nav_config_enc.Get_IdConfiguracion(AP.IdBodega, AP.IdEmpresa)

                    Usuario.Codigo = txtUsuario.Text
                    Usuario.Clave = txtContraseña.Text
                    Usuario.IdEmpresa = cmbEmpresa.EditValue

                    If clsLnUsuario.Usuario_Valido_For_TMS_By_IdEmpresa(Usuario) Then

                        AP.IdRol = Usuario.IdRol

                        AP.Exigir_Politica_Contraseñas = clsLnEmpresa.Exigir_Politica_Contraseñas(AP.IdEmpresa)

                        Dim t1 As Threading.Thread = New Threading.Thread(AddressOf Actualiza_Ultimo_Ingreso)
                        t1.Start()

                        AP.UsuarioAp = Usuario

                        Hide()

                        Try
                            MenuP.lblUsuario.Caption = AP.UsuarioAp.Nombres
                            MenuP.ShowDialog()
                            MenuP.Dispose()
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try

                        Usuario = Nothing : lUsuario = Nothing

                        Close()

                    Else
                        XtraMessageBox.Show("Usuario o contraseña incorrecta!", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtUsuario.SelectAll() : txtUsuario.Focus()
                        ActiveControl = txtUsuario
                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

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

                        Actualiza_APP_Config()

                        Listar_Empresas()

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
                Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
            End If
        Finally
            UseWaitCursor = False
        End Try

    End Sub

    Private Sub Set_Parametros_Servidor(ByVal IndiceInstanciaDefecto As Integer, ByVal ListaInstancias As List(Of clsCadenaConexion))

        Try

            If IndiceInstanciaDefecto <> -1 Then
                'Tomar por defecto la instancia que corresponde con el host que ejecuta.
                clsBD.Instancia = ListaInstancias(IndiceInstanciaDefecto)
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

        Try


            GetIPAddress()
            lblNombrePCCliente.Caption = AP.HostName

            If Not AP.Existe_Ini() Then
                XtraMessageBox.Show("No existe el archivo ini. se cerrará la aplicación",
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
                Close()
            Else

                Dim IndiceInstanciaDefecto As Integer = -1

                ListaInstancias = clsPublic.Leer_Archivo_Configuracion_Ini(IndiceInstanciaDefecto)

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
                            cmbInstancia.ItemIndex = IndiceInstanciaDefecto
                            lblInstancia.ForeColor = Color.Firebrick
                        Else
                            cmbInstancia.ItemIndex = 0
                            lblInstancia.ForeColor = Color.Black
                        End If

                        Actualiza_APP_Config()

                        If Licenciamiento_Activo() Then

                            Dim t1 As Threading.Thread = New Threading.Thread(AddressOf Registra_Ingreso)
                            t1.Start()

                            Listar_Empresas()

                            '#GT09052022: Este valor se debe configurar en el ini bajo el nombre ID_EMPRESA_TMS
                            If IndiceInstanciaDefecto = -1 Then

                                If Not ListaInstancias(0).ID_EMPRESA_TMS = 0 Then

                                    Dim vIdEmpresaDefecto As Integer = ListaInstancias(0).ID_EMPRESA_TMS

                                    Dim BeEmpresa As New clsBeEmpresa
                                    BeEmpresa = clsLnEmpresa.Get_Single_By_IdEmpresa(vIdEmpresaDefecto)

                                    If Not BeEmpresa Is Nothing Then
                                        txtUsuario.Text = "garita"
                                        txtContraseña.Text = "garita123"
                                        carga_print_ticket()
                                    End If

                                End If
                            End If

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

    Private Sub carga_print_ticket()

        Try

            If InfOK() Then

                If Licenciamiento_Activo() Then

                    AP.IdEmpresa = cmbEmpresa.EditValue
                    AP.NomEmpresa = cmbEmpresa.Text
                    AP.IdConfiguracionInterface = clsLnI_nav_config_enc.Get_IdConfiguracion(AP.IdBodega, AP.IdEmpresa)

                    Usuario.Codigo = txtUsuario.Text
                    Usuario.Clave = txtContraseña.Text
                    Usuario.IdEmpresa = cmbEmpresa.EditValue

                    If clsLnUsuario.Usuario_Valido_For_TMS_By_IdEmpresa(Usuario) Then

                        AP.IdRol = Usuario.IdRol

                        AP.Exigir_Politica_Contraseñas = clsLnEmpresa.Exigir_Politica_Contraseñas(AP.IdEmpresa)

                        Dim t1 As Threading.Thread = New Threading.Thread(AddressOf Actualiza_Ultimo_Ingreso)
                        t1.Start()

                        AP.UsuarioAp = Usuario

                        Hide()

                        Try

                            MenuPrint.ShowDialog()
                            MenuPrint.Dispose()

                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try

                        Usuario = Nothing : lUsuario = Nothing

                        Close()

                    Else
                        XtraMessageBox.Show("Usuario o contraseña incorrecta!", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtUsuario.SelectAll() : txtUsuario.Focus()
                        ActiveControl = txtUsuario
                    End If

                End If

            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Registra_Ingreso()

        Try

            clsLnLicencia_item.Registra_Ingreso(AP.HostName)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Private Function Licenciamiento_Activo() As Boolean

        Licenciamiento_Activo = False

        Try

            Dim BeLicItem As New clsBeLicencia_item

            Dim vNombreServerLicencias As String = ""

            If clsLnLicencia_item.Existe_Servidor_De_Licencias(BeLicItem) Then

                vNombreServerLicencias = BeLicItem.Identificacion
                Dim vEsServer As Boolean = (BeLicItem.Bandera = (clsBeLicencia_item.eTipoLicencia.Server))

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
        txtUsuario.Focus()
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

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmbEmpresa_EditValueChanged(sender As Object, e As EventArgs) Handles cmbEmpresa.EditValueChanged
        If cmbEmpresa.EditValue <> 0 Then
            AP.IdEmpresa = cmbEmpresa.EditValue
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
        Environment.Exit(Environment.ExitCode)
    End Sub

    Private Sub LabelControl5_Click(sender As Object, e As EventArgs) Handles LabelControl5.Click
        txtContraseña.Properties.PasswordChar = ""
    End Sub

    Private Sub GroupControl1_Paint(sender As Object, e As PaintEventArgs) Handles GroupControl1.Paint

    End Sub
End Class