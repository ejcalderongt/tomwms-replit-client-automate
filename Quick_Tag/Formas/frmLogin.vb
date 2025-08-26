

Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen
Imports System.Reflection
Imports TOMWMS

Public Class frmLogin

    Public Sub New()
        InitializeComponent()
    End Sub

    Private lUsuario As New clsLnUsuario
    Private Usuario As New clsBeUsuario
    Private AbrioIni As Boolean = False
    Private IsLoading As Boolean = True
    Private MenuP As New frmmenu()
    Private ListaInstancias As New List(Of clsCadenaConexion)
    'Private Imprimir_Etiqueta As New frmPrint_Label()
    Private frmMenu As New frmmenu()
    Private frmPrintEtiqueta As New frmPrint_Label()

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
                            'MenuP.lblUsuario.Caption = AP.UsuarioAp.Nombres
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

    Private Sub Actualiza_Ultimo_Ingreso()
        clsLnUsuario.Actualiza_Ultimo_Ingreso(Usuario)
    End Sub

    Private Sub frmLogin_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            Me.Opacity = 0

            txtUsuario.Focus()

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

                    If Not ListaInstancias(0).ID_STANDALONE = 0 Then

                        If Not ListaInstancias(0).ID_EMPRESA_QUICKTAG = 0 Then
                            carga_impresion_directa()
                        End If

                    Else

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


                            If Not ListaInstancias(0).ID_STANDALONE = 0 Then
                                If Not ListaInstancias(0).ID_EMPRESA_QUICKTAG = 0 Then
                                    carga_impresion_directa()
                                End If

                            Else
                                '#GT27022025: validar proceso normal de licenciamiento
                                If Licenciamiento_Activo() Then

                                    Dim t1 As Threading.Thread = New Threading.Thread(AddressOf Registra_Ingreso)
                                    t1.Start()

                                    Listar_Empresas()

                                    '#GT20022025: si la lista de instancia no esta vacia, pero el indice dice -1 tomar la instancia por defecto.
                                    If ListaInstancias IsNot Nothing AndAlso IndiceInstanciaDefecto = -1 Then

                                        '#GT09052022: Este valor se debe configurar en el ini bajo el nombre ID_EMPRESA_QUICKTAG
                                        If Not ListaInstancias(0).ID_EMPRESA_QUICKTAG = 0 Then

                                            Dim vIdEmpresaDefecto As Integer = ListaInstancias(0).ID_EMPRESA_QUICKTAG
                                            Dim BeEmpresa As New clsBeEmpresa
                                            BeEmpresa = clsLnEmpresa.Get_Single_By_IdEmpresa(vIdEmpresaDefecto)

                                            '#GT20022025: cargar datos genericos solo para que inicie sesión
                                            If Not BeEmpresa Is Nothing Then
                                                txtUsuario.Text = "Garita"
                                                txtContraseña.Text = "Garita123"
                                                carga_menu_impresion()
                                            End If

                                        End If

                                    End If

                                Else
                                    XtraMessageBox.Show("Licenciamiento inactivo", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Close()
                                End If

                            End If

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

    Private Sub Registra_Ingreso()

        Try

            clsLnLicencia_item.Registra_Ingreso(AP.HostName)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    'Private Sub carga_impresion_directa()

    '    Try

    '        Hide()

    '        '#GT20022025: carga el form principal donde se imprime sin pasar por el menu
    '        'frmPrintEtiqueta.Id_StandAlone = True ' Asigna el valor antes de mostrar el formulario
    '        'frmPrintEtiqueta.ShowDialog()
    '        'frmPrintEtiqueta.Dispose()

    '        SplashScreenManager.ShowForm(Nothing, GetType(frmSplashQuickTag), True, True, False)

    '            ' Simular carga sin bloquear la UI
    '            Task.Run(Async Sub()
    '                         Await Task.Delay(3000) ' Espera 3 segundos sin bloquear
    '                         SplashScreenManager.CloseForm()

    '                         ' #GT20022025: carga el form principal donde se imprime sin pasar por el menú
    '                         Dim frm As New frmPrint_Label()
    '                         frm.Id_StandAlone = True ' Asigna el valor antes de mostrar el formulario
    '                         frm.ShowDialog()
    '                         frm.Dispose()
    '                     End Sub)

    '        Usuario = Nothing : lUsuario = Nothing

    '        Close()

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub


    'Private Async Sub carga_impresion_directa()
    '    Try
    '        'Hide() ' Oculta el formulario actual (formLogin)

    '        ' 1️⃣ Mostrar SplashScreen en el hilo principal
    '        SplashScreenManager.ShowForm(Nothing, GetType(frmSplashQuickTag), True, True, False)

    '        ' 2️⃣ Forzar actualización de la UI para que el Splash se renderice correctamente
    '        'Application.DoEvents()

    '        Me.Opacity = 0 ' En lugar de Hide(), hacer que el formulario sea completamente transparente
    '        Application.DoEvents() ' Asegurar que el cambio se refleje antes de continuar


    '        ' 3️⃣ Simular carga sin bloquear la UI
    '        Await Task.Delay(4000)

    '        ' 4️⃣ Cerrar el Splash Screen de manera segura
    '        If SplashScreenManager.Default IsNot Nothing Then
    '            SplashScreenManager.CloseForm()
    '        End If

    '        ' 5️⃣ Cargar el formulario principal (frmPrint_Label)
    '        Using frm As New frmPrint_Label()
    '            frm.Id_StandAlone = True ' Asigna el valor antes de mostrar el formulario
    '            frm.ShowDialog()
    '        End Using ' Se asegura de liberar los recursos sin necesidad de Dispose()

    '        ' 6️⃣ Limpiar variables y cerrar formLogin
    '        Usuario = Nothing
    '        lUsuario = Nothing
    '        Close()

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub


    Private Async Sub carga_impresion_directa()
        Try
            ' 1️⃣ Ocultar el formulario antes de continuar
            Me.Opacity = 0 ' En lugar de Hide(), hacer que el formulario sea completamente transparente
            Application.DoEvents() ' Asegurar que el cambio se refleje antes de continuar

            ' 2️⃣ Mostrar el Splash Screen
            SplashScreenManager.ShowForm(Nothing, GetType(frmSplashQuickTag), True, True, False)
            Application.DoEvents() ' Asegurar que se renderice correctamente

            ' 3️⃣ Simular carga sin bloquear la UI
            Await Task.Delay(4000)

            ' 4️⃣ Cerrar el Splash Screen de manera segura
            If SplashScreenManager.Default IsNot Nothing Then
                SplashScreenManager.CloseForm()
            End If

            ' 5️⃣ Mostrar el formulario de impresión directamente
            Using frm As New frmPrint_Label()
                frm.Id_StandAlone = True
                frm.ShowDialog()
            End Using

            ' 6️⃣ Liberar variables y cerrar formLogin
            Usuario = Nothing
            lUsuario = Nothing
            Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub carga_menu_impresion()

        Try

            Hide()

            Try
                '#GT20022025: carga el form principal donde se imprime sin pasar por el menu
                frmMenu.ShowDialog()
                frmMenu.Dispose()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            Usuario = Nothing : lUsuario = Nothing

            Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class