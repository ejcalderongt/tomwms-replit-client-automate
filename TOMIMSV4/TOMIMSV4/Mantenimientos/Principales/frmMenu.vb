Imports System.IO
Imports System.Net.Http
Imports System.Reflection
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Timers
Imports DevExpress.LookAndFeel
Imports DevExpress.Mvvm.Native
Imports DevExpress.Skins
Imports DevExpress.XtraBars
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

'#EJC20220407: PickingReference.
'#CKFK20220425 Si funciona la aplicacion
Public Class frmMenu

    Public Property HostName As String
    Public Property IpAdress As String
    Public Property Guardar_Configuracion_Menu_Al_Salir As Boolean = True
    Private Usuario As New clsBeUsuario

    Private pHorario_Ejecucion_Historico As TimeSpan

    Private WithEvents backgroundWorker As New System.ComponentModel.BackgroundWorker

    Public Property ConexionActivaWebServiceTOMWMS As Boolean = False

    Public Sub New()

        Try

            'BonusSkins.Register()
            SkinManager.EnableFormSkins()

            InitializeComponent()

            rbMain.Minimized = True

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub CreateMDIChildMonitor()

        Try

            With frmPrincipal02
                .MdiParent = Me
                .Show()
                .Focus()
            End With

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub Mostrar_Monitor()

        Try

            If Not clsBD.Instancia.Modo_Debug Then

                Dim ThreadMon As New Thread(Sub()
                                                BeginInvoke(Sub()
                                                                CreateMDIChildMonitor()
                                                            End Sub)
                                            End Sub)
                ThreadMon.Start()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
        End Try


    End Sub

    Sub TimerElapsed(ByVal sender As Object, ByVal e As ElapsedEventArgs)

        Try

            Dim time As DateTime = e.SignalTime
            Dim horario = time.ToString("HH:mm:ss")
            Console.WriteLine("TIME_PC: " + horario)
            Console.WriteLine("horario_programado: " + pHorario_Ejecucion_Historico.ToString())

            If horario = pHorario_Ejecucion_Historico.ToString() Then
                Dim hora_terminal = Now.TimeOfDay.ToString()
                stock_jornada_subproceso(hora_terminal)
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub


    Private Sub stock_jornada_subproceso(horario As String)

        Dim BeLicItem As New clsBeLicencia_item()
        Dim vNombreHost As String = ""

        Try

            If AP.Empresa.Generar_Stock_Jornada AndAlso clsLnLicencia_item.Existe_Servidor_De_Licencias(BeLicItem) Then

                vNombreHost = Net.Dns.GetHostName().ToString()
                Dim vMac As String = clsLnLicencia_item.Get_Mac_Host(AP.HostName)

                If clsLnLicencia_item.Es_Server(vNombreHost, BeLicItem, vMac) Then

                    Dim vStopwatch As Stopwatch = Stopwatch.StartNew()
                    vStopwatch.Start()

                    If SplashScreenManager.Default IsNot Nothing Then
                        SplashScreenManager.Default.SetWaitFormDescription("Generando jornada de sistema...")
                    End If

                    Task_Jornada()

                    '#EJC202303291528: No ejecutar como proceso asincrono, sino, sincrono.
                    'Dim st As Thread = New Thread(AddressOf Task_Jornada)
                    'st.Start()
                    'vStopwatch.Stop()

                    Dim vTiempoSegundos As Double = vStopwatch.Elapsed.TotalSeconds
                    Console.WriteLine("Fin de proceso - " & Now & " Tiempo transcurrido en segundos: " & vTiempoSegundos)

                    Dim vMsgError As String = ("ADVERTENCIA_20230308: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " y host: " & vNombreHost & " ejecutó stock jornada en: " & horario)
                    clsLnLog_error_wms.Agregar_Error(vMsgError)

                Else

                    Dim vMsgError As String = ("ADVERTENCIA_20230308_1: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " y host: " & vNombreHost & " intentó ejecutar stock jornada en: " & horario)
                    clsLnLog_error_wms.Agregar_Error(vMsgError)

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Sub frmMenu_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        Try

            '#EJC20210714: Desplegar versión en menú principal.
            lblVersion.Caption = gVersionApp & " " & FormatoFechas.tFecha(gFechaVersion)

            Mostrar_Monitor()

            setIPAddress() : setAPP()

            LlenaBodegas()

            Set_Label_Personalizados()

            pHorario_Ejecucion_Historico = AP.Bodega.Horario_Ejecucion_Historico

            '#GT13032023: controla la hora a ejecutar el stock jornada
            Dim timer As Timers.Timer = New Timers.Timer(1000)
            AddHandler timer.Elapsed, New ElapsedEventHandler(AddressOf TimerElapsed)
            timer.Start()

            If Not backgroundWorker.IsBusy Then
                backgroundWorker.RunWorkerAsync()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            CheckForIllegalCrossThreadCalls = False

            rbMain.ResumeLayout(True)
            rbMain.MdiMergeStyle = RibbonMdiMergeStyle.Always

            Try

                If File.Exists(CurDir() & "\RibbonSettings.xml") Then

                    Try
                        rbMain.RestoreLayoutFromXml("RibbonSettings.xml")
                    Catch ex As Exception
                        If ex.Message.Contains("La clave proporcionada no se encontró en el diccionario.") Then
                            File.Delete(CurDir() & "\RibbonSettings.xml")
                        End If
                    End Try

                End If

            Catch ex As Exception
            End Try

            Dim vDebug As Boolean = False

#If DEBUG Then
            vDebug = True
#End If
            If Not vDebug Then
                DesactivarMenu()
                HabilitarMenuRol(AP.IdRol, rbMain)
            Else
                DesactivarMenu()
                Task.Run(Sub()
                             HabilitarMenuRol(AP.IdRol, rbMain)
                         End Sub)
            End If

            pgIndicadores.Visible = True
            mnuIndicadores.Visibility = BarItemVisibility.Always

            grpRepTablero.Visible = True
            mnuDashBoardDesigner.Visibility = BarItemVisibility.Always
            mnuDashBoardDesigner.Enabled = True

            If Not clsBD.Instancia.WSTOMHH Is Nothing Then
                If Not clsBD.Instancia.WSTOMHH.IsEmptyOrSingle() Then
                    'Verificar si hay conexión con el WS de la HH
                    backgroundWorker.WorkerSupportsCancellation = True
                    backgroundWorker.WorkerReportsProgress = True
                    backgroundWorker.RunWorkerAsync()
                End If
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub backgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles backgroundWorker.DoWork

        Try

            'While Not backgroundWorker.CancellationPending

            Dim url As String = clsBD.Instancia.WSTOMHH
            Dim estaConectado As Boolean = VerificarConexion(url)
            e.Result = estaConectado

            If e.Result Then
                ConexionActivaWebServiceTOMWMS = True
            End If

            ' Esperar un tiempo antes de la próxima verificación (por ejemplo, 10 segundos)
            'Thread.Sleep(10000)

            'End While

        Catch ex As Exception

        End Try

    End Sub

    Private Sub backgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles backgroundWorker.RunWorkerCompleted
        ' Manejar el resultado aquí, sin reiniciar el BackgroundWorker

        Try

            If e.Cancelled Then
                ' La tarea fue cancelada
                ' Actualizar la UI para reflejar la cancelación
                lblWSHHURL.ItemAppearance.Normal.ForeColor = Color.Firebrick
            ElseIf e.Error IsNot Nothing Then
                ' Hubo un error durante la ejecución
                ' Actualizar la UI para reflejar el error
                lblWSHHURL.ItemAppearance.Normal.ForeColor = Color.Firebrick
            Else
                Dim estaConectado As Boolean = e.Result
                If estaConectado Then
                    lblWSHHURL.ItemAppearance.Normal.ForeColor = Color.Green
                    ' La conexión fue exitosa
                    ' Actualizar la UI para reflejar la conexión exitosa
                Else
                    ' La conexión falló
                    ' Actualizar la UI para reflejar la falla de conexión
                    lblWSHHURL.ItemAppearance.Normal.ForeColor = Color.Firebrick
                End If
            End If

            ' Reiniciar o no el BackgroundWorker según tu lógica

        Catch ex As Exception

        End Try

    End Sub

    Private Function VerificarConexion(url As String) As Boolean

        VerificarConexion = False

        Try
            Using cliente As New HttpClient()
                cliente.Timeout = TimeSpan.FromSeconds(5)
                If url <> "" Then
                    Dim response As HttpResponseMessage = cliente.GetAsync(url).Result
                    Return response.IsSuccessStatusCode
                End If
            End Using
        Catch ex As HttpRequestException
            ' Manejar específicamente los errores de la solicitud HTTP
            Return False
        Catch ex As Exception
            ' Manejar otros tipos de excepciones
            Return False
        End Try
    End Function

    Private Sub frmMenu_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Try
            Cierra_Proceso_Impresion()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Try
            '#EJC20171108_REF08_1218AM: desconectaConexion = Cancela conexión?            
            clsLnLicencia_item.Registra_Conexion(AP.HostName)
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

        Try

            If Guardar_Configuracion_Menu_Al_Salir Then

                rbMain.SaveLayoutToXml("RibbonSettings.xml")

            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub MnuMantEmpresaItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantEmpresa.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmEmpresaList
            .Modo = frmEmpresaList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .WindowState = FormWindowState.Normal
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMantUsuarios_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantUsuarios.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmUsuList
            .Modo = frmUsuList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuCambiarContraseña_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuCambiarContraseña.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmUsu
            .Modo = frmUsu.TipoTrans.EditarClave
            .Usuario.IdUsuario = AP.UsuarioAp.IdUsuario
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuActualizar.Enabled = .OpcionesMenu.Modificar
            .WindowState = FormWindowState.Normal
            .Show()

            If .Modo = 1 Or .Modo = 2 Then
                .NombresTextEdit.Focus()
            Else
                .ClaveTextEdit.Focus()
            End If

        End With
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMantMotivoAnul_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantMotivoAnul.ItemClick

        mnuMantMotivoAnul.Enabled = False

        If Not permiteMenu(e.Link) Then Return

        With frmMotivo_AnulacionList
            .Modo = frmMotivo_AnulacionList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        mnuMantMotivoAnul.Enabled = True

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMantMotivoDevol_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantMotivoDevol.ItemClick

        mnuMantMotivoDevol.Enabled = False
        If Not permiteMenu(e.Link) Then Return

        With frmMotivo_DevolucionList
            .Modo = frmMotivo_DevolucionList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        mnuMantMotivoDevol.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMuelles_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMuelles.ItemClick


        mnuMuelles.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuMuelles.Enabled = True
            Return
        End If

        With frmBodega_MuellesList
            .Modo = frmBodega_MuellesList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        mnuMuelles.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMantOperadores_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantOperadores.ItemClick

        mnuMantOperadores.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuMantOperadores.Enabled = True
            Return
        End If

        With frmOperador_List
            .Modo = frmOperador_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mmuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        mnuMantOperadores.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMantJornadaLab_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantJornadaLab.ItemClick

        mnuMantJornadaLab.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuMantJornadaLab.Enabled = True
            Return
        End If

        With frmJornada_LaboralList
            .Modo = frmJornada_LaboralList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        mnuMantJornadaLab.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMantClas_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantClas.ItemClick

        mnuMantClas.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuMantClas.Enabled = True
            Return
        End If

        With frmProducto_ClasificacionList
            .Modo = frmProducto_ClasificacionList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        mnuMantClas.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuFamilia_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuFamilia.ItemClick

        mnuFamilia.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuFamilia.Enabled = True
            Return
        End If

        With frmProducto_FamiliaList
            .Modo = frmProducto_FamiliaList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        mnuFamilia.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMantBodega_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuUbicacion.ItemClick

        With frmBodega_List
            .Modo = frmBodega_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .cmdImprimir.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMantPais_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantPais.ItemClick

        mnuMantPais.Enabled = False
        If Not permiteMenu(e.Link) Then Return

        With frmPais_List
            .Modo = frmPais_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        mnuMantPais.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMantEstados_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantEstados.ItemClick

        mnuMantEstados.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuMantEstados.Enabled = True
            Return
        End If

        With frmProducto_EstadoList
            .Modo = frmProducto_EstadoList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        mnuMantEstados.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMarca_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMarca.ItemClick

        mnuMarca.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuMarca.Enabled = True
            Return
        End If

        With frmProducto_MarcaList
            .Modo = frmProducto_MarcaList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        mnuMarca.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMantTipo_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantTipo.ItemClick

        mnuMantTipo.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuMantTipo.Enabled = True
            Return
        End If

        With frmProducto_TipoList
            .Modo = frmProducto_TipoList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        mnuMantTipo.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMantUnidMed_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantUnidMed.ItemClick

        mnuMantUnidMed.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuMantUnidMed.Enabled = True
            Return
        End If

        With frmUnidad_MedidaList
            .Modo = frmUnidad_MedidaList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        mnuMantUnidMed.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMantProducto_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantProducto.ItemClick

        'GT 16042021 PRUEBA PARA EVITAR DOBLE CLICK
        mnuMantProducto.Enabled = False

        If Not e Is Nothing Then
            If Not permiteMenu(e.Link) Then
                mnuMantProducto.Enabled = True
                Return
            End If
        End If

        With frmProductoList
            .Modo = frmProductoList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        mnuMantProducto.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMantHorarioLab_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantHorarioLab.ItemClick

        mnuMantHorarioLab.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuMantHorarioLab.Enabled = True
            Return
        End If

        With frmHorario_LaboralList
            .Modo = frmHorario_LaboralList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        mnuMantHorarioLab.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMantTurnoLab_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantTurnoLab.ItemClick

        mnuMantTurnoLab.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuMantTurnoLab.Enabled = True
            Return
        End If

        With frmTurno_List
            .Modo = frmTurno_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        mnuMantTurnoLab.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuEmpTrans_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEmpTrans.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmEmpresa_TransporteList
            .Modo = frmEmpresa_TransporteList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mmuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuVehiculos_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuVehiculos.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmEmpresa_Transporte_VehiculoList
            .Modo = frmEmpresa_Transporte_VehiculoList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mmuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuPiloto_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuPiloto.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmEmpresa_Transporte_PilotoList
            .Modo = frmEmpresa_Transporte_PilotoList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mmuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuTipoCliente_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuTipoCliente.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmCienteTipo_List
            .Modo = frmCienteTipo_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMantBodega_ItemClick_1(sender As Object, e As ItemClickEventArgs) Handles mnuMantBodega.ItemClick

        mnuMantBodega.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuMantBodega.Enabled = True
            Return
        End If

        With frmBodega_List
            .Modo = frmBodega_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        mnuMantBodega.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub cmdProveedores_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdProveedores.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmProveedor_List
            .Modo = frmProveedor_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMantCliente_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantCliente.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmCliente_List
            .Modo = frmCliente_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub cmdArancel_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdArancel.ItemClick

        cmdArancel.Enabled = False
        If Not permiteMenu(e.Link) Then Return

        With frmArancel_List
            .Modo = frmArancel_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .cmdImprimir.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        cmdArancel.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuPedidoVenta_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuPedidoVenta.ItemClick

        If Not e Is Nothing Then
            If Not permiteMenu(e.Link) Then Return
        End If

        Cierra_Instancia_Previa(frmPedido_List)

        With frmPedido_List
            .Modo = frmPedido_List.pModo.Lista
            If Not e Is Nothing Then
                .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            End If
            .MdiParent = Me
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuVendedorRoad_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuVendedorRoad.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmListaRoadVendedor
            .Modo = frmListaRoadVendedor.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuVendedorRuta_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuVendedorRuta.ItemClick

        With frmListaRoadRuta
            .Modo = frmListaRoadRuta.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuUbicSug_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuUbicSug.ItemClick
        If Not permiteMenu(e.Link) Then Return
        frmUbicSugTest.ShowDialog()
    End Sub

    'Metodo creado por Bismarck para formualario Montacarga Y MontacargaBodega
    Private Sub mnuControlMontacargas_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuControlMontacargas.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmMontacargaList
            .Modo = frmMontacargaList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuFallaMontacarga_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuFallaMontacarga.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmMontacargaTipoFallaList
            .Modo = frmMontacargaTipoFallaList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuCambioUbicacion_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuCambioUbicacion.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmCambioUbicacion_List
            .Modo = frmCambioUbicacion_List.pModo.Lista
            .tipoOperacion = frmCambioUbicacion_List.pTipoOperacion.CambioUbic
            .ListarTransaccionUbicHhEnc()
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuCambioEstado_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuCambioEstado.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmCambioUbicacion_List
            .Modo = frmCambioUbicacion_List.pModo.Lista
            .tipoOperacion = frmCambioUbicacion_List.pTipoOperacion.CambioEst
            .ListarTransaccionUbicHhEnc()
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuRolesOperador_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuRolesOperador.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmRolOperadorList
            .Modo = frmRolOperadorList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuRolesUsuario_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuRolesUsuario.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmRolUsuarioList
            .Modo = frmRolUsuarioList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub cmdCalendario_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdCalendario.ItemClick
        If Not permiteMenu(e.Link) Then Return

        SplashScreenManager.CloseForm(False)
        Dim c As New frmCalendar01
        c.ShowDialog()
    End Sub

    Private Sub mnuMover_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuTraslados.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmTraslado_ProductoList
            .Modo = frmTraslado_ProductoList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub BarButtonItem6_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarButtonItem6.ItemClick
        SplashScreenManager.CloseForm(False)
        Dim bodegaTree As New frmBodegaTree(frmBodegaTree.TipoTrans.Nuevo)
        bodegaTree.ShowDialog()
    End Sub

    Private Sub mnuDepartamento_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuDepartamento.ItemClick

        mnuDepartamento.Enabled = False
        If Not permiteMenu(e.Link) Then Return

        With frmDepartamentoList
            .Modo = frmDepartamentoList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        mnuDepartamento.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMunicpio_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMunicpio.ItemClick

        mnuMunicpio.Enabled = False
        If Not permiteMenu(e.Link) Then Return

        With frmMunicipioList
            .Modo = frmMunicipioList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        mnuMunicpio.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuRegion_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuRegion.ItemClick

        mnuRegion.Enabled = False
        If Not permiteMenu(e.Link) Then Return

        With frmRegionList
            .Modo = frmRegionList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        mnuRegion.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub BarButtonItem8_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMotivoUbic.ItemClick

        mnuMotivoUbic.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuMotivoUbic.Enabled = True
            Return
        End If

        With frmMotivo_UbicacionList
            .Modo = frmMotivo_UbicacionList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        mnuMotivoUbic.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub frmMenu_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Try

            ' Producto
            If e.KeyCode = Keys.A And e.Control Then
                mnuMantProducto_ItemClick(Nothing, Nothing)
            ElseIf e.KeyCode = Keys.O AndAlso e.Control Then
                ' Orden de Compra
                mnuOrdenCompra_ItemClick(e, Nothing)
            ElseIf e.KeyCode = Keys.R And e.Control Then
                ' Recepcion
                mnuRecepcion_ItemClick(e, Nothing)
            ElseIf e.KeyCode = Keys.P And e.Control Then
                ' Pedido
                mnuPedidoVenta_ItemClick(e, Nothing)
            ElseIf e.KeyCode = Keys.D And e.Control Then
                ' Base de datos
                Dim frm As New frmBD
                frm.Show()
            ElseIf e.KeyCode = Keys.Y AndAlso e.Control Then
                Actualiza_IdOrdenCompra_En_Recepcion_Det()
            ElseIf e.KeyCode = Keys.S AndAlso e.Control Then
                cmdExistenciasPorLote_ItemClick(e, Nothing)
            ElseIf e.KeyCode = Keys.M AndAlso e.Control Then
                cmdMovimientosCardex_ItemClick(e, Nothing)
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

    Private Sub cmdDetalleSerie_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdDetalleSerie.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            With frmRptStock
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdDetalleParametro_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdDetalleParametro.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmRptStockParametro
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuTipoTarima_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuTipoTarima.ItemClick

        mnuTipoTarima.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuTipoTarima.Enabled = True
            Return
        End If

        With frmTipoTarima_List
            .Modo = frmTipoTarima_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        mnuTipoTarima.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuTipoConte_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuTipoConte.ItemClick

        mnuTipoConte.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuTipoConte.Enabled = True
            Return
        End If

        With frmTipoContenedor_List
            .Modo = frmTipoContenedor_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        mnuTipoConte.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuTarima_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuTarima.ItemClick

        mnuTarima.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuTarima.Enabled = True
            Return
        End If

        With frmTarima_Lista
            .Modo = frmTarima_Lista.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        mnuTarima.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMantReglaMsj_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantReglaMsj.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmMensajeRegla_List
            .Modo = frmMensajeRegla_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMantReglaRc_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantReglaRc.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmReglas_recepcion_List
            .Modo = frmReglas_recepcion_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub BarButtonItem15_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantPropietario.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmPropietario_List
            .Modo = frmPropietario_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub BarButtonItem18_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarButtonItem18.ItemClick
        With frmPropietarioReglaRecepcion_List
            .Modo = frmPropietarioReglaRecepcion_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuConfigurarMonitor_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuConfigurarMonitor.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmBodegaMonitor_List
            .Modo = frmBodegaMonitor_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuMostrarMonitor_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMostrarMonitor.ItemClick

        If Not permiteMenu(e.Link) Then Return

        If frmPrincipal02.Visible Then
            BeginInvoke(New Action(Sub() frmPrincipal02.Close()))
        Else
            Mostrar_Monitor()
        End If

    End Sub

    Private Sub mnuPicking_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuPicking.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmPicking_List
            .Modo = frmPicking_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub cmdConexionBD_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdConexionBD.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmConnIni

            If Not AP.Existe_Ini() Then
                .lblMod = "NUEVO"
            Else
                .lblMod = "EDITAR"
            End If

            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuLicencias_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuLicencias.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmLicencia
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With '

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub mnuDespachos_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuDespachos.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmDespacho_List
            .Modo = frmDespacho_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuRecepcion_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuRecepcion.ItemClick

        Try

            If Not e Is Nothing Then If Not permiteMenu(e.Link) Then Return

            With frmRecepcion_List
                .Modo = frmRecepcion_List.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuNuevo.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub mnuOrdenCompra_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuOrdenCompra.ItemClick


        If Not e Is Nothing Then If Not permiteMenu(e.Link) Then Return

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormCaption("Documento de Ingreso")

        With frmOrdenCompra_List
            .Modo = frmOrdenCompra_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub IndiceRotacion_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuIndiceRot.ItemClick

        mnuIndiceRot.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuIndiceRot.Enabled = True
            Return
        End If

        With frmIndiceRotacion_List
            .Modo = frmIndiceRotacion_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        mnuIndiceRot.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuReglasList_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuReglasList.ItemClick

        mnuReglasList.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuReglasList.Enabled = True
            Return
        End If
        With frmReglaUbicEnc_List
            .Modo = frmReglaUbicEnc_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        mnuReglasList.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub BarButtonItem26_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuReglaUbicPrio.ItemClick

        mnuReglaUbicPrio.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuReglaUbicPrio.Enabled = True
            Return
        End If

        With frmReglaUbicPrioList
            .Modo = frmReglaUbicPrioList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        mnuReglaUbicPrio.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub cmdLetra_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdLetra.ItemClick

        cmdLetra.Enabled = False
        If Not permiteMenu(e.Link) Then
            cmdLetra.Enabled = True
            Return
        End If

        With frmFont_Tramo_List
            .Modo = frmFont_Tramo_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        cmdLetra.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub BarButtonItem23_ItemClick_1(sender As Object, e As ItemClickEventArgs) Handles cmdTipoEtiqueta.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmTipo_Etiqueta_List
            .Modo = frmTipo_Etiqueta_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuImpEtiqueta_ItemClick_2(sender As Object, e As ItemClickEventArgs) Handles mnuImpEtiqueta.ItemClick

        mnuImpEtiqueta.Enabled = False
        If Not permiteMenu(e.Link) Then
            mnuImpEtiqueta.Enabled = True
            Return
        End If

        Dim Etiqueta As New frmUbicacion_Etiqueta(frmUbicacion_Etiqueta.TipoTrans.Nuevo)
        Etiqueta.OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
        Etiqueta.ShowDialog()
        Etiqueta.Dispose()
        mnuImpEtiqueta.Enabled = True

    End Sub

    Private Sub mnuEstructura_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEstructuraInicial.ItemClick

        mnuEstructuraInicial.Enabled = False

        If Not permiteMenu(e.Link) Then
            mnuEstructuraInicial.Enabled = True
            Return
        End If

        Dim est As New frmEstBodega
        est.IdBodega = AP.IdBodega
        est.NombreBodega = AP.Bodega.Nombre
        est.OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
        est.ShowDialog()
        est.Dispose()

        mnuEstructuraInicial.Enabled = True

    End Sub

    Private Sub mnuInterfaceNav_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuInterfaceNav.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            If AP.IdConfiguracionInterface <> 0 Then

                Ejecutar_Interface(" -" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-0-0" & "-" & clsBD.Instancia.NombreInstancia, Me)

            Else

                XtraMessageBox.Show(String.Format("La Bodega {0} de la Empresa {1} no  tiene definida configuración para interface", AP.NomBodega, AP.NomEmpresa),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Public Shared Sub HabilitarMenuRol(ByVal pIdRol As Integer, ByVal prbMain As RibbonControl)

        Dim mCurrentItem As BarItem
        Dim mBarSubItem As BarSubItem
        Dim mSubLink As BarItemLink

        ' Este procedimiento verifica que cada item del menu 
        ' esté disponible para el usuario que se ha logeado        
        Dim lMenuRol As New List(Of clsBeMenu_sistema)

        Try

            prbMain.Enabled = True

            lMenuRol = clsLnMenu_sistema.Get_All_By_IdRol(pIdRol)

            Dim BeMenuSistema As New clsBeMenu_sistema

            For Each currentPage As RibbonPage In prbMain.Pages

                BeMenuSistema = Nothing

                BeMenuSistema = lMenuRol.Find(Function(x) (x.Titulo = currentPage.Text OrElse x.Nombre_lgco = currentPage.Name) AndAlso x.Nivel = 1)

                If Not BeMenuSistema Is Nothing Then
                    currentPage.Visible = BeMenuSistema.Visible
                Else
                    currentPage.Visible = False
                    '#EJC20180419:No deshabilitar el ribon principal.
                    'currentPage.Ribbon.Enabled = False
                End If

                Debug.Print(String.Format("Habilita Page: {0} - {1}", currentPage.Text, currentPage.Name))

            Next

            For Each currentPage As RibbonPage In prbMain.Pages

                For Each currentGroup As RibbonPageGroup In currentPage.Groups

                    BeMenuSistema = lMenuRol.Find(Function(x) (x.Titulo = currentGroup.Text OrElse x.Nombre_lgco = currentGroup.Name) AndAlso x.Nivel = 2)

                    BeMenuSistema = lMenuRol.Find(Function(x) (x.Nombre_lgco = currentGroup.Name))

                    If Not BeMenuSistema Is Nothing Then
                        currentGroup.Visible = BeMenuSistema.Visible
                        currentGroup.Enabled = BeMenuSistema.Visible
                    Else
                        currentGroup.Visible = False
                        currentGroup.Enabled = False
                    End If

                    If currentPage.Name = "rpReportes" Then
                        Debug.Print(String.Format("Habilita Group: {0}/{1} - {2}/{3} ", currentPage.Text, currentPage.Name, currentGroup.Text, currentGroup.Name))
                    End If

                    Debug.Print(String.Format("Habilita Group: {0}/{1} - {2}/{3} ", currentPage.Text, currentPage.Name, currentGroup.Text, currentGroup.Name))


                Next currentGroup

            Next currentPage

            For Each currentPage As RibbonPage In prbMain.Pages

                For Each currentGroup As RibbonPageGroup In currentPage.Groups

                    For Each currenLink As BarItemLink In currentGroup.ItemLinks

                        BeMenuSistema = lMenuRol.Find(Function(x) (x.Titulo = currenLink.Item.Caption OrElse x.Nombre_lgco = currenLink.Item.Name) AndAlso x.Nivel = 3)

                        If Not BeMenuSistema Is Nothing Then
                            currenLink.KeyTip = BeMenuSistema.IdMenu

                            currenLink.Visible = BeMenuSistema.Visible
                            currenLink.Item.Visibility = BeMenuSistema.Visible
                            currenLink.Item.Enabled = BeMenuSistema.Visible
                        Else
                            currenLink.Visible = False
                            currenLink.Item.Visibility = False
                            currenLink.Item.Enabled = False
                        End If

                        Debug.Print(String.Format("Habilita Link: {0} - {1} - {2}", currentPage.Text, currentGroup.Text, currenLink.Item.Caption))

                    Next

                Next

            Next

            For Each currentPage As RibbonPage In prbMain.Pages

                For Each currentGroup As RibbonPageGroup In currentPage.Groups

                    For Each currenLink As BarItemLink In currentGroup.ItemLinks

                        mCurrentItem = currenLink.Item

                        If mCurrentItem.GetType.FullName = "DevExpress.XtraBars.BarSubItem" Then

                            mBarSubItem = mCurrentItem

                            Debug.Print("Ha: " & mBarSubItem.Caption)

                            For Each mSubLink In mBarSubItem.ItemLinks

                                If mSubLink.Item.Name = "mnuTareasPreIngreso" OrElse mSubLink.Item.Caption = "mnuTareasPreIngreso" Then
                                    Debug.Print("Espera")
                                Else
                                    Debug.Print(String.Format("Habilita: {0} - {1} - {2} - {3} - {4}", currentPage.Text, currentGroup.Text, mSubLink.Item.Caption, mSubLink.Item.Name, mSubLink.Item.GetType.FullName))
                                End If

                                BeMenuSistema = lMenuRol.Find(Function(x) (x.Titulo = mSubLink.Item.Name OrElse x.Nombre_lgco = mSubLink.Item.Name) AndAlso x.Nivel = 4)

                                If Not BeMenuSistema Is Nothing Then
                                    mSubLink.KeyTip = BeMenuSistema.IdMenu
                                    mSubLink.Visible = BeMenuSistema.Visible
                                    mSubLink.Item.Visibility = BeMenuSistema.Visible
                                    mSubLink.Item.Enabled = BeMenuSistema.Visible
                                Else
                                    mSubLink.Visible = False
                                    mSubLink.Item.Visibility = False
                                    mSubLink.Item.Enabled = False
                                End If

                            Next

                        Else

                            If mCurrentItem.GetType.FullName = "DevExpress.XtraBars.BarButtonItem" Then

                                BeMenuSistema = lMenuRol.Find(Function(x) (x.Titulo = mCurrentItem.Name OrElse x.Nombre_lgco = mCurrentItem.Name))

                                If Not BeMenuSistema Is Nothing Then
                                    Debug.Print("Opción de menú está configurada en nivel: " & BeMenuSistema.Nivel &
                                                " Se encontró en iteración de nivel 4 - Opción: " & mCurrentItem.Name)
                                Else
                                    Debug.Print("Ha_Na: " & mCurrentItem.GetType.FullName & " " & mCurrentItem.Name)
                                End If

                            Else
                                Debug.Print("Ha_Na: " & mCurrentItem.GetType.FullName & " " & mCurrentItem.Name)
                            End If

                        End If

                    Next

                Next

            Next

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub DesactivarMenu()

        Try

            Dim mCurrentItem As BarItem
            Dim mBarSubItem As BarSubItem
            Dim mSubLink As BarItemLink

            For Each currentPage As RibbonPage In rbMain.Pages

                For Each currentGroup As RibbonPageGroup In currentPage.Groups

                    currentGroup.Visible = False
                    currentGroup.Enabled = False

                    For Each currenLink As BarItemLink In currentGroup.ItemLinks

                        mCurrentItem = currenLink.Item

                        currenLink.Item.Visibility = BarItemVisibility.Never
                        currenLink.Item.Enabled = False

                        If mCurrentItem.GetType.FullName = "DevExpress.XtraBars.BarSubItem" Then

                            mBarSubItem = mCurrentItem

                            For Each mSubLink In mBarSubItem.ItemLinks

                                mSubLink.Item.Visibility = BarItemVisibility.Never
                                mSubLink.Item.Enabled = False

                                Debug.Print(String.Format("{0} - {1} - {2} - {3} - {4}", currentPage.Text, currentGroup.Text, mSubLink.Item.Caption, mSubLink.Item.Name, mSubLink.Item.GetType.FullName))

                            Next

                        End If

                    Next currenLink

                Next currentGroup

            Next currentPage

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub setIPAddress()

        Try

            lblNombrePCCliente.Caption = AP.HostName 'HostName ' & "/" & IpAdress

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub setAPP()

        Try

            'clsBD.Instancia.WSTOMHH
            If clsBD.Instancia.WSTOMHH.Trim <> "" Then
                lblWSHHURL.Caption = "WSHHURL: " & clsBD.Instancia.WSTOMHH
                lblWSHHURL.ItemAppearance.Normal.ForeColor = Color.Green
            Else
                lblWSHHURL.Caption = "WSHHURL: N/D."
                lblWSHHURL.ItemAppearance.Normal.ForeColor = Color.Firebrick
            End If

            lblServerAPP.Caption += " " & clsBD.Instancia.Server
            lblBDAPP.Caption += " " & clsBD.Instancia.NombreBD
            lblEmpresa.Caption += " " & AP.NomEmpresa
            lblBodega.Caption += " " & AP.NomBodega
            bbiCambiaBodega.Caption = lblBodega.Caption
            lblModoDebug.Caption = IIf(clsBD.Instancia.Modo_Debug, "DEBUG = ON", "DEBUG = OFF")

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdExistenciasProductos_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdExistenciasProductos.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmExistenciasConReserva
                .MdiParent = Me
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdValorizacion_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdValorizacion.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            With frmValorizacionStock
                .MdiParent = Me
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdMovimiento_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdMovimiento.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmReportMovimiento
                .MdiParent = Me
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdMovimientosDet_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdMovimientosDet.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            With frmMovimientos_Detalle
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdUnidadMedidaconversion_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdUnidadMedidaconversion.ItemClick

        cmdUnidadMedidaconversion.Enabled = False
        If Not permiteMenu(e.Link) Then
            cmdUnidadMedidaconversion.Enabled = True
            Return
        End If

        With frmUnidad_Medida_Conversion_List
            .Modo = frmUnidad_Medida_Conversion_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With

        cmdUnidadMedidaconversion.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuInventarios_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuInventarios.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmInventarioList
            .Modo = frmInventarioList.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub cmdResumenExistencia_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdResumenExistencia.ItemClick

        Try

            If Not permiteMenu(e.Link) Then Return

            With frmResumenExistencias
                .Modo = frmResumenExistencias.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub cmdResumenExistenciasUMBas_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdResumenExistenciasUMBas.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            With frmResumenExistenciasUMBas
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdMovimientosCardex_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdMovimientosCardex.ItemClick

        If Not e Is Nothing Then If Not permiteMenu(e.Link) Then Return

        Try

            With frmMovimientosKardex
                .MdiParent = Me
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdMotivoAjuste_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdMotivoAjuste.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmAjusteMotivo_List
                .Modo = frmAjusteMotivo_List.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .cmdNuevo.Enabled = .OpcionesMenu.Modificar
                .cmdActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdTipoAjuste_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdTipoAjuste.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmAjusteTipo_List
                .Modo = frmAjusteTipo_List.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .cmdActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdAjusteInventario_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdAjusteInventario.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            With frmAjusteStock_List
                .Modo = frmAjusteStock_List.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuNuevo.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdExistenciasEstado_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdExistenciasEstado.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmExistenciasPorEstado
                .Modo = frmExistenciasPorEstado.pModo.Lista
                .MdiParent = Me
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdMovimientosUbic_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdMovimientosUbic.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmReportCambiosUbicacion
                .Modo = frmReportCambiosUbicacion.pModo.Lista
                .MdiParent = Me
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdProximosVencer_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdProximosVencer.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmProximos_A_Vencer
                .Modo = frmProximos_A_Vencer.pModo.Lista
                .MdiParent = Me
                .Show()
                .Focus()
            End With '

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdMinMax_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdMinMax.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmRptMinimoxMaximos
                .Modo = frmRptMinimoxMaximos.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuNuevo.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With '

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdPendientesReq_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdPendientesReq.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmPendientesRequisicion
                .Modo = frmPendientesRequisicion.pModo.Lista
                .MdiParent = Me
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdStockTrans_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdStockTrans.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmRptStockTransito
                .MdiParent = Me
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdRotacionProd_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdRotacionProd.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmProductosSinMovimientos
                .Modo = frmProductosSinMovimientos.pModo.Lista
                .MdiParent = Me
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdTrazaLote_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdTrazaLote.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmTrazaPorLote
                .Modo = frmTrazaPorLote.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Restart()
        Process.Start(
        Application.ExecutablePath)
        Application.Exit()
    End Sub

    Private Sub mnuResetMenuLayOut_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuResetMenuLayOut.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            If IO.File.Exists(CurDir() & "\RibbonSettings.xml") Then

                XtraMessageBox.Show("El archivo de configuración existe, se eliminará y se reiniciará la aplicación.",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

                IO.File.Delete(CurDir() & "\RibbonSettings.xml")

                Guardar_Configuracion_Menu_Al_Salir = False

                Restart()

            Else
                XtraMessageBox.Show("El archivo de configuración no existe, no se puede reiniciar el layout",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
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

    Private Sub cmdStockEnFecha_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdStockEnFecha.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmMov_Reporte
                '.Modo = frmStockEnUnaFecha.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub xtMdi_SelectedPageChanged(sender As Object, e As EventArgs) Handles xtMdi.SelectedPageChanged

        Try

            If rbMain.MergedPages.Count > 0 Then
                rbMain.SelectedPage = rbMain.MergedPages(0)
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub mnuTareasPreIngreso_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuTareasPreIngreso.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmPreIngreso_List
                .Modo = frmPreIngreso_List.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuNuevo.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)

        End Try

    End Sub

    'Reporte 8
    Private Sub cmdExistenciasUbic_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdExistenciasUbic.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmExistenciasUbicacion
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdExistenciaspordocumento_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdExistenciaspordocumento.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmExistenciasNumDocu
            .Modo = frmExistenciasNumDocu.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub cmdExistenciasPorLote_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdExistenciasPorLote.ItemClick

        If Not e Is Nothing Then If Not permiteMenu(e.Link) Then Return

        With frmStockPorLote
            .Modo = frmStockPorLote.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Function permiteMenu(link As BarItemLink) As Boolean

        Dim us As New clsBeUsuario
        Dim ms As New clsBeMenu_sistema
        Dim clave As String

        Try

            ms.IdMenu = link.KeyTip
            'MsgBox(link.KeyTip)
            clsLnMenu_sistema.GetSingle(ms)

            If (ms.Solicitar_clave_autorizacion) Then

                us.IdUsuario = AP.UsuarioAp.IdUsuario
                clsLnUsuario.GetSingle(us)

                Try

                    clave = clsPublic.Desencriptar(us.Clave_autorizacion)

                    If (clave = "") Then Throw New Exception

                Catch ex As Exception
                    MsgBox("No se ha registrado la clave de autorización para el usuario y esta transacción necesita clave de supervisor.") : Return False
                End Try

                Dim frmlog As New frmAjusteLogin() With {.clave = clave}

                If frmlog.ShowDialog() <> DialogResult.Yes Then
                    frmlog.Dispose() : Return False
                End If

                frmlog.Dispose()

                Return True

            Else
                Return True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End Try

    End Function

    Private Sub mnuCotizar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuCotizar.ItemClick
        If Not permiteMenu(e.Link) Then Return

    End Sub


    Private Sub cmdUbicacionPicking_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdUbicacionPicking.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With fmrUbicaciones_Picking
            .Modo = fmrUbicaciones_Picking.pModo.Lista
            .MdiParent = Me
            .Show()
            .Focus()
        End With
        SplashScreenManager.CloseForm(False)

    End Sub

    'Reporte 10
    Private Sub cmdDetalleLotePorUbi_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdDetalleLotePorUbi.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmLotePorUbi
            .Modo = frmLotePorUbi.pModo.Lista
            .MdiParent = Me
            .Show()
            .Focus()
        End With
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub cmdMovCardexConDocs_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdMovCardexConDocs.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmMovimientosKardexConDocs
            .MdiParent = Me
            .Show()
            .Focus()
        End With
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub cmdStockRes_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdStockRes.ItemClick

        Try
            If Not permiteMenu(e.Link) Then Return

            With frmListStockReservado
                .MdiParent = Me
                .Modo = frmListStockReservado.pModo.Lista
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With '

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuEstadoEnviosNAV_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEstadoEnviosNAV.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmEstadoEnviosNAV
            .MdiParent = Me
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub cmdExistenciaPorTipoProd_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdExistenciaPorTipoProd.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmStockPorTipo
            .Modo = frmStockPorTipo.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuBackup_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuBackup.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmBackup
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuGenerarBackup.Enabled = .OpcionesMenu.Modificar
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuCambioDeUsuario_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuCambioDeUsuario.ItemClick

        If XtraMessageBox.Show("Está seguro(a) de cambiar de usuario(a)?",
                Text,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) = DialogResult.Yes Then

            Restart()

        End If

    End Sub

    Private Sub cmdDetalleInventario_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdDetalleInventario.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmReporteInventarios
            .Modo = frmReporteInventarios.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With
        SplashScreenManager.CloseForm(False)
    End Sub

    Private Sub cmdSerieDocs_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdSerieDocs.ItemClick

        cmdSerieDocs.Enabled = False
        If Not permiteMenu(e.Link) Then
            cmdSerieDocs.Enabled = True
            Return
        End If

        With frmSeriesDoc_List
            .Modo = frmSeriesDoc_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        cmdSerieDocs.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub cmdRpExitLP_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdRpExitLP.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmStockConLp
            .Modo = frmStockConLp.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With
        SplashScreenManager.CloseForm(False)
    End Sub

    Private Sub cmdExistCnRec_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdExistCnRec.ItemClick
        If Not permiteMenu(e.Link) Then Return

        With frmStockConDocument
            .Modo = frmStockConDocument.pModo.Lista
            .MdiParent = Me
            .Show()
            .Focus()
        End With
        SplashScreenManager.CloseForm(False)
    End Sub

    Private Sub BarButtonItem15_ItemClick_1(sender As Object, e As ItemClickEventArgs) Handles cmdTransaccionesOut.ItemClick

        Try
            If Not permiteMenu(e.Link) Then Return

            With frmRegistrosInterface
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Modo = frmRegistrosInterface.pModo.Lista
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdBarrasPallet_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdBarrasPallet.ItemClick

        Try
            If Not permiteMenu(e.Link) Then Return

            With frmBarrasInter_list
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Modo = frmBarrasInter_list.pModo.Lista
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdCodBarras_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdCodBarras.ItemClick

        cmdCodBarras.Enabled = False

        If Not permiteMenu(e.Link) Then
            cmdCodBarras.Enabled = True
            Return
        End If

        Dim Etiqueta As New Producto_Etiquetas(Producto_Etiquetas.TipoTrans.Nuevo)
        Etiqueta.OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
        Etiqueta.ShowDialog()
        Etiqueta.Dispose()
        cmdCodBarras.Enabled = True

    End Sub

    Private Sub cmdPrintSvr_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdPrintSvr.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            Ejecutar_Servicio("", Me)

        Catch ex As Exception
            XtraMessageBox.Show("No se pudo ejecutar el servicio de impresión.",
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Cierra_Proceso_Impresion()

        Try

            '#EJC20220410:Actualmente no tenemos proceso de impresión de BOF en ningún cliente.
            'Dim procesos() As Process = Process.GetProcessesByName("WMS_PrintService")

            'If procesos.Length > 0 Then

            '    procesos(0).CloseMainWindow()

            '    If procesos(0).HasExited = False Then
            '        procesos(0).Kill()
            '        procesos(0).Close()
            '    End If

            'End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub cmdLogInterface_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdLogInterface.ItemClick

        Try
            If Not permiteMenu(e.Link) Then Return

            With frmLogMI3
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Modo = frmLogMI3.pModo.Lista
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdPrint_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdPrint.ItemClick

        cmdPrint.Enabled = False
        With frmImpresora_List
            .Modo = frmImpresora_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        cmdPrint.Enabled = True
        SplashScreenManager.CloseForm(False)
    End Sub

    Private Sub mnuDashBoardDesigner_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuDashBoardDesigner.ItemClick

        With frmDashboardDesigner
            .WindowState = FormWindowState.Maximized
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuTipoCuadrilla_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuTipoCuadrilla.ItemClick

        mnuTipoCuadrilla.Enabled = False
        With frmTipoCuadrilla_List
            .Modo = frmTipoCuadrilla_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With
        mnuTipoCuadrilla.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuCuadrilla_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuCuadrilla.ItemClick

        With frmCuadrilla_List
            .Modo = frmCuadrilla_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub BarButtonItem15_ItemClick_2(sender As Object, e As ItemClickEventArgs) Handles mnuReporteDistribucionPorTramo.ItemClick

        Try

            With frmDistribucionPorTramo
                .Modo = frmDistribucionPorTramo.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub mnuTarifas_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuTarifas.ItemClick


        Try

            With frmTarifaTipoTransaccion_List
                .Modo = frmTarifaTipoTransaccion_List.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuNuevo.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub mnuRegimenFiscal_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuRegimenFiscal.ItemClick

        Try

            mnuRegimenFiscal.Enabled = False

            With frmRegimenFiscal_List
                .Modo = frmRegimenFiscal_List.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuNuevo.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With

            mnuRegimenFiscal.Enabled = True
            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            mnuRegimenFiscal.Enabled = True
        End Try

    End Sub

    Private Sub mnuConfiguracionInt_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuConfiguracionInt.ItemClick

        Try

            If AP.IdConfiguracionInterface <> 0 Then

                With frmConfiguracionList
                    .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                    .mnuNuevo.Enabled = .OpcionesMenu.Modificar
                    .mnuActualizar.Enabled = .OpcionesMenu.Leer
                    .IdConfiguracionInterface = AP.IdConfiguracionInterface
                    .Show()
                    .Focus()
                End With

            Else

                XtraMessageBox.Show(String.Format("La Bodega {0} de la Empresa {1} no  tiene definida configuración para interface", AP.NomBodega, AP.NomEmpresa),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub cmdDocConDiferencias_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdDocConDiferencias.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmDocConDiferencias
            .Modo = frmDocConDiferencias.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub cmdDocPeConDiferencias_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdDocPeConDiferencias.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmDocPeConDiferencias
            .Modo = frmDocPeConDiferencias.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuRepSalidasRapidoD_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuRepSalidasRapidoD.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmDetalleSalidas
            .Modo = frmDetalleSalidas.pModo.Lista
            .MdiParent = Me
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub cmdExistFiscal_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdExistFiscal.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With FrmStock_Fiscal
            .Modo = FrmStock_Fiscal.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuTiemposTareas_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuTiemposTareas.ItemClick

        Try

            mnuTiemposTareas.Enabled = False

            If Not permiteMenu(e.Link) Then
                mnuTiemposTareas.Enabled = True
                Return
            End If

            With frmTipoTareaTiempos_List
                .Modo = frmTipoTareaTiempos_List.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .Focus()
            End With

            mnuTiemposTareas.Enabled = True
            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            mnuTiemposTareas.Enabled = True
        End Try

    End Sub


    Private Sub cmdHistResGeneral_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdHistResGeneral.ItemClick

        cmdHistResGeneral.Enabled = False

        If Not permiteMenu(e.Link) Then
            cmdHistResGeneral.Enabled = True
            Return
        End If

        With frmResumenHistorico
            .Modo = frmResumenHistorico.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With

        cmdHistResGeneral.Enabled = True

    End Sub

    Private Sub cmdResumenCliente_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdResumenCliente.ItemClick


        cmdResumenCliente.Enabled = False
        If Not permiteMenu(e.Link) Then
            cmdResumenCliente.Enabled = True
            Return
        End If

        With frmResumenPorCliente
            .Modo = frmResumenPorCliente.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With
        cmdResumenCliente.Enabled = True

    End Sub

    Private Sub mnuMantConsolidador_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantConsolidador.ItemClick

        mnuMantConsolidador.Enabled = False

        If Not permiteMenu(e.Link) Then
            mnuMantConsolidador.Enabled = True
            Return
        End If

        With frmConsolidador_List
            .Modo = frmCliente_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuNuevo.Enabled = .OpcionesMenu.Modificar
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        mnuMantConsolidador.Enabled = True
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub cmdCtasOrden_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdCtasOrden.ItemClick

        cmdCtasOrden.Enabled = False
        If Not permiteMenu(e.Link) Then
            cmdCtasOrden.Enabled = True
            Return
        End If

        With frmCtasOrden
            .Modo = frmCtasOrden.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With

        cmdCtasOrden.Enabled = True

    End Sub

    Private Sub cmdCtaOrdenPoliza_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdCtaOrdenPoliza.ItemClick

        cmdCtaOrdenPoliza.Enabled = False
        If Not permiteMenu(e.Link) Then
            cmdCtaOrdenPoliza.Enabled = True
            Return
        End If

        With frmCtaOrdenPoliza
            .Modo = frmCtaOrdenPoliza.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With

        cmdCtaOrdenPoliza.Enabled = True

    End Sub

    Private Sub cmdMovimiento_Reporte_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdMovimiento_Reporte.ItemClick


        cmdMovimiento_Reporte.Enabled = False
        If Not permiteMenu(e.Link) Then
            cmdMovimiento_Reporte.Enabled = True
            Return
        End If

        With frmMovimiento_Reporte
            .Modo = frmMovimiento_Reporte.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With

        cmdMovimiento_Reporte.Enabled = True

    End Sub

    Private Sub Actualiza_Ultimo_Ingreso()
        clsLnUsuario.Actualiza_Ultimo_Ingreso(Usuario)
    End Sub

    Private Sub LlenaBodegas()

        Dim DT As New DataTable
        Dim barManager1 As New BarManager
        Dim itemBodega As BarButtonItem

        Try

            barManager1.ForceInitialize()
            pmBodegas.Manager = barManager1

            DT = clsLnBodega.Get_All_By_Empresa_ForCombo(AP.IdEmpresa)

            pmBodegas.ClearLinks()

            barManager1.ForceInitialize()

            pmBodegas.Manager = barManager1

            For Each r As DataRow In DT.Rows
                itemBodega = New BarButtonItem(barManager1, r.Item("IdBodega") & "-" & r.Item("Nombre"), 0)
                itemBodega.Tag = r.Item("IdBodega")
                pmBodegas.AddItems(New BarItem() {itemBodega})
            Next

            AddHandler barManager1.ItemClick, AddressOf BarManager1_ItemClick
            ' Associate the popup menu with the form.
            barManager1.SetPopupContextMenu(Me, pmBodegas)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Public Sub Task_Jornada()

        Dim frmJornada As New frmJornadaSistema()
        frmJornada.WindowState = FormWindowState.Maximized
        frmJornada.CloseBox = False
        frmJornada.SendToBack()
        Application.Run(frmJornada)

    End Sub

    Private Sub BarManager1_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cambiando bodega...")

            Thread.Sleep(1000)

            Dim vIdBodega As Integer = e.Item.Tag
            Dim vCaptionBodega As String = ""

            If AP.IdBodega = vIdBodega Then
                XtraMessageBox.Show(AP.NomBodega & " Es la bodega actual.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else

                If clsLnUsuario.Usuario_Valido(AP.UsuarioAp, vIdBodega) Then

                    '#EJC20210317: Obtener el objeto de bodega para tener todos sus parámetros y configuraciones.
                    AP.Bodega = clsLnBodega.GetSingle_By_Idbodega(vIdBodega)
                    AP.IdBodega = vIdBodega
                    AP.NomBodega = AP.Bodega.Codigo & " - " & AP.Bodega.Nombre
                    AP.IdConfiguracionInterface = clsLnI_nav_config_enc.Get_IdConfiguracion(AP.IdBodega, AP.IdEmpresa)

                    vCaptionBodega = "Bodega: " & " " & AP.NomBodega
                    lblBodega.Caption = vCaptionBodega
                    bbiCambiaBodega.Caption = vCaptionBodega

                    If AP.Empresa.Generar_Stock_Jornada Then

                        If Not clsLnJornada_sistema.Existe_Jornada() Then

                            If SplashScreenManager.Default IsNot Nothing Then
                                SplashScreenManager.Default.SetWaitFormDescription("Generando jornada de sistema...")
                            End If

                            Dim A As Thread = New Thread(AddressOf Task_Jornada)
                            A.Start()

                        End If


                    End If

                    If SplashScreenManager.Default IsNot Nothing Then
                        SplashScreenManager.Default.SetWaitFormDescription("Actualizando monitor de tareas...")
                    End If

                    Dim t1 As Thread = New Thread(AddressOf Actualiza_Ultimo_Ingreso)
                    t1.Start()

                    Cerrar_Abrir_Lista_Orden_Compra(Me)

                Else
                    XtraMessageBox.Show("Usuario no válido en la bodega seleccionada!", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            End If

            Application.DoEvents()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Me.Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Public Shared Sub Cerrar_Abrir_Lista_Orden_Compra(ByVal padre As frmMenu)

        Dim abierta As Boolean = False

        Try

            For Each objForm In My.Application.OpenForms

                If (Trim(objForm.Name)) = "frmOrdenCompra_List" Then

                    objForm.Close()

                    SplashScreenManager.ShowForm(padre, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormCaption("Documento de Ingreso")

                    With frmOrdenCompra_List
                        .Modo = frmOrdenCompra_List.pModo.Lista
                        .MdiParent = padre
                        .Show()
                    End With

                    Exit For

                End If

            Next

        Catch ex As Exception
            'XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), frmMenu.Text,
            'MessageBoxButtons.OK,
            'MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub cmdExistConsolidador_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdExistConsolidador.ItemClick


        If Not permiteMenu(e.Link) Then Return

        With frmExistenciasConsolidador
            .Modo = frmExistenciasConsolidador.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub cmdExistenciasPropietario_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdExistenciasPropietario.ItemClick

        Try

            If Not permiteMenu(e.Link) Then Return

            With frmResumenExistenciaPropietario
                .Modo = frmResumenExistenciaPropietario.pModo.Lista
                .MdiParent = Me
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdExistenciasPorLote_Posicion_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdExistenciasPorLote_Posicion.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmStockPorLote_Posicion
            .Modo = frmStockPorLote_Posicion.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub cmdStockJornadaSistema_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdStockJornadaSistema.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            With frmStockJornada
                .Modo = frmStockJornada.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .Focus()
            End With '

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub btnServicios_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnServicios.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            With frmServicios
                .Modo = frmServicios.pModo.Lista
                .MdiParent = Me
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuReportesGallery_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuReportesGallery.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmReportesGallery
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuServiciosIngreso_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuServiciosIngreso.ItemClick

        Try

            With frmServicios_List
                .Modo = frmServicios_List.pModo.Lista
                .TipoTransaccion = frmServicios_List.pTipoTransaccion.Ingreso
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuNuevo.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub mnuServiciosSalidas_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuServiciosSalidas.ItemClick

        Try

            With frmServicios_List
                .Modo = frmServicios_List.pModo.Lista
                .TipoTransaccion = frmServicios_List.pTipoTransaccion.Salida
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuNuevo.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub mnuTiposDocumentoIngreso_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuTiposDocumentoIngreso.ItemClick

        Try

            With frmTipoDocumentoIngresoList
                .Modo = frmServicios_List.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuNuevo.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub mnuTiposDocumentoSalida_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuTiposDocumentoSalida.ItemClick

        Try

            With frmTipoDocumentoSalidaLista
                .Modo = frmServicios_List.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuNuevo.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub mnuKardexLote_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuKardexLote.ItemClick

        Try

            If Not permiteMenu(e.Link) Then Return

            With frmKardexLote
                .MdiParent = Me
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub cmdValorizacionOC_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdValorizacionOC.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            With frmValorizacionStockOC
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdResValorizacion_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdResValorizacion.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmStockResJornada
            .Modo = frmStockResJornada.pModo.Lista
            .MdiParent = Me
            .Show()
            .Focus()
        End With
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub cmdMovKardexLote_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdMovporLote.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmMovimientosporLote
            .Modo = frmMovimientosporLote.pModo.Lista
            .MdiParent = Me
            .Show()
            .Focus()
        End With
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub cmdResValorizacionMerca_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdResValorizacionMerca.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmStockResJornadaMerca
            .Modo = frmStockResJornadaMerca.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .WindowState = FormWindowState.Maximized
            .Focus()
        End With
        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub btInvInicial_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btInvInicial.ItemClick


        Dim TipoInventario = "Importación inicial"
        Dim idInventario = 0

        Try

            Dim Carga As New frmCargaExcel_Inv_Ini_Op_Log() With {.pNombreMantenimiento = "Inventario " + TipoInventario,
                .pTipoMantenimiento = "Inventario",
                .Listar = Nothing}

            If Carga.ShowDialog() = DialogResult.OK Then

                Dim i As Integer = 0
                Dim vContador As Integer = 1

            End If

            Carga.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            'RibbonControl.Enabled = True
        End Try

    End Sub

    Private Sub mnuAnalitica1_ItemClick_3(sender As Object, e As ItemClickEventArgs) Handles mnuAnalitica1.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            Dim vInstanciaDVSalidas As New frmDashViewer1

            With vInstanciaDVSalidas
                vInstanciaDVSalidas.Text = "Productividad_Picking_Por_Operador"
                .IdentificadorTablero = frmDashViewer1.pTipoTablero.Productividad_Picking_Por_Operador
                .MdiParent = Me.MdiParent : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .WindowState = FormWindowState.Maximized
                .Focus()
            End With

            Dim vInstanciaDVSalidasCliente As New frmDashViewer1

            With vInstanciaDVSalidasCliente
                vInstanciaDVSalidasCliente.Text = "Cantidad_De_Pedidos_Por_Cliente"
                .IdentificadorTablero = frmDashViewer1.pTipoTablero.Cantidad_De_Pedidos_Por_Cliente
                .MdiParent = Me.MdiParent : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .WindowState = FormWindowState.Maximized
                .Focus()
            End With

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuAnalitica2_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuAnalitica2.ItemClick

        Try

            If Not permiteMenu(e.Link) Then Return

            Dim vInstanciaDVIngresos As New frmDashViewer1

            With vInstanciaDVIngresos
                vInstanciaDVIngresos.Text = "Productividad_Ingresos_Por_Operador_Y_Tipo_Documento"
                .IdentificadorTablero = frmDashViewer1.pTipoTablero.Productividad_Ingresos_Por_Operador_Y_Tipo_Documento
                .MdiParent = Me.MdiParent : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuAnalitica3_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuAnalitica3.ItemClick

        Try

            If Not permiteMenu(e.Link) Then Return

            Dim vInstanciaDVProductos As New frmDashViewer1

            With vInstanciaDVProductos
                vInstanciaDVProductos.Text = "Top_15_Productos_Salida_Por_Mes"
                .IdentificadorTablero = frmDashViewer1.pTipoTablero.Top_15_Productos_Salida_Por_Mes
                .MdiParent = Me.MdiParent : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With

            Dim vInstanciaDVProductos1 As New frmDashViewer1

            With vInstanciaDVProductos1
                vInstanciaDVProductos1.Text = "Productos_con_menor_rotación_Por_Mes_Y_Año"
                .IdentificadorTablero = frmDashViewer1.pTipoTablero.Productos_con_menor_rotación_Por_Mes_Y_Año
                .MdiParent = Me.MdiParent : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With

            Dim vInstanciaDVProductos2 As New frmDashViewer1

            With vInstanciaDVProductos2
                vInstanciaDVProductos2.Text = "Dash_Movimientos_Por_Periodo"
                .IdentificadorTablero = frmDashViewer1.pTipoTablero.Dash_Movimientos_Por_Periodo
                .MdiParent = Me.MdiParent : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With


        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdEstacionalidadProducto_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdEstacionalidadProducto.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmEstacionalidadProducto
                .Modo = frmEstacionalidadProducto.pModo.Lista
                .MdiParent = Me
                .Show()
                .Focus()
            End With '

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub sddiSkinWMS_ItemPress(sender As Object, e As ItemClickEventArgs) Handles sddiSkinWMS.ItemPress

        'XtraMessageBox.Show("item press", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Try

            Dim style As String
            style = e.Item.Caption

            style = Replace(style, "(", "")
            style = Replace(style, ")", "")

            clsPublic.Escribir_Archivo_Configuracion_Skin(style)

            Dim defaultLF As UserLookAndFeel = UserLookAndFeel.Default
            defaultLF.SkinName = style

            Dim vNombreSkin As String = clsPublic.Leer_Archivo_Configuracion_Skin()

            If vNombreSkin <> "" Then
                XtraMessageBox.Show("Se guardó el formato de Skin: " & vNombreSkin, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            'XtraMessageBox.Show("Se guardó el formato de menú: " & style, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdStockEnLinea_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdStockEnLinea.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try


            With frmInventarioEnLinea
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdMovimientosPoliza_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdMovimientosPoliza.ItemClick

        '#GT24032022 kardex por poliza cealsa
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmMovimientosPoliza
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuMantCentroCostos_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMantCentroCostos.ItemClick


        If Not permiteMenu(e.Link) Then Return

        Try

            With frmCentro_Costos_List
                .Modo = frmCentro_Costos_List.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuNuevo.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Actualiza_IdOrdenCompra_En_Recepcion_Det()

        Dim lRecepcionDet As New List(Of clsBeTrans_re_det)
        Dim lBeTransOcDet As New List(Of clsBeTrans_oc_det)
        Dim BeTransOCDetSingle As New clsBeTrans_oc_det
        Dim lIdOrdenCompraEncByIdRecepcionEnc As List(Of Integer)
        Dim clsTransaccion As New clsTransaccion()
        Dim vResultadoUpdate As Integer = 0
        Dim vCantidadRegistrosAActualizar As Integer = 0
        Dim lRecepcionEncSinOC As New List(Of Integer)

        Try

            clsTransaccion.Begin_Transaction()

            lRecepcionDet = clsLnTrans_re_det.Get_All_Sin_IdOrdenCompra(clsTransaccion.lConnection,
                                                                        clsTransaccion.lTransaction)

            If Not lRecepcionDet Is Nothing Then

                vCantidadRegistrosAActualizar = lRecepcionDet.Count()

                For Each rdet In lRecepcionDet.OrderBy(Function(x) x.IdRecepcionEnc)

                    '#EJC20220410:This means that we've already known that the OC document donsn't exist.
                    If Not lRecepcionEncSinOC.Contains(rdet.IdRecepcionEnc) Then

                        lIdOrdenCompraEncByIdRecepcionEnc = clsLnTrans_re_oc.Get_IdOrdenCompraEnc_By_IdRecepcionEnc(rdet.IdRecepcionEnc,
                                                                                                               clsTransaccion.lConnection,
                                                                                                               clsTransaccion.lTransaction)

                        If Not lIdOrdenCompraEncByIdRecepcionEnc Is Nothing Then

                            If lIdOrdenCompraEncByIdRecepcionEnc.Count > 0 Then

                                For Each IdOrdenCompraEnc In lIdOrdenCompraEncByIdRecepcionEnc

                                    lBeTransOcDet = clsLnTrans_oc_det.Get_All_By_IdOrdenCompraEnc(IdOrdenCompraEnc,
                                                                                                 clsTransaccion.lConnection,
                                                                                                 clsTransaccion.lTransaction)

                                    If Not lBeTransOcDet Is Nothing Then


                                        BeTransOCDetSingle = lBeTransOcDet.Find(Function(x) x.No_Linea = rdet.No_Linea _
                                                                                AndAlso x.IdProductoBodega = rdet.IdProductoBodega _
                                                                                AndAlso x.IdUnidadMedidaBasica = rdet.IdUnidadMedida _
                                                                                AndAlso x.IdPresentacion = rdet.IdPresentacion)

                                        If Not BeTransOCDetSingle Is Nothing Then

                                            rdet.IdOrdenCompraEnc = BeTransOCDetSingle.IdOrdenCompraEnc
                                            rdet.IdOrdenCompraDet = BeTransOCDetSingle.IdOrdenCompraDet

                                            vResultadoUpdate += clsLnTrans_re_det.Actualizar_IdOrdenCompraEnc_And_IdOrdenCompraDet(rdet,
                                                                                                                                   clsTransaccion.lConnection,
                                                                                                                                   clsTransaccion.lTransaction)
                                        Else
                                            Throw New Exception("ERROR_202204101102C: No se encontró el detalle del documento de ingreso para la recepciónEnc: " & rdet.IdRecepcionEnc & " y recepcionDet: " & rdet.IdRecepcionDet)
                                        End If

                                    End If

                                Next

                            Else

                                '#EJC20220410: This is possible because the manual recepcion on BOF without inbound document.
                                'Throw New Exception("ERROR_202204101102B: No se encontró el documento de ingreso para la recepción: " & rdet.IdRecepcionEnc)

                                Dim vMsgNoEnc As String = "ERROR_202204101102B: No se encontró el documento de ingreso para la recepción: " & rdet.IdRecepcionEnc
                                XtraMessageBox.Show(vMsgNoEnc,
                                  Text,
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error)

                                Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), vMsgNoEnc)
                                clsLnLog_error_wms.Agregar_Error(vMsgError)

                                lRecepcionEncSinOC.Add(rdet.IdRecepcionEnc)

                            End If

                        Else
                            Throw New Exception("ERROR_202204101102A: No se encontró el documento de ingreso para la recepción: " & rdet.IdRecepcionEnc)
                        End If

                    End If

                Next

                XtraMessageBox.Show("Se actualizaron: " & vResultadoUpdate & " de: " & vCantidadRegistrosAActualizar & " registros, si las cantidades no son iguales es probable que existan recepciones sin documento de referencia",
                                     Text,
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Information)

                'If vResultadoUpdate = vCantidadRegistrosAActualizar Then
                '    MsgBox("Ok :)")
                'Else
                '    Throw New Exception("Something went wrong: updated: " & vResultadoUpdate & " Needed:" & vCantidadRegistrosAActualizar)
                'End If

            End If

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception

            clsTransaccion.RollBack_Transaction()

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    Private Sub cmdProgresoPicking_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdAuditoriaPicking.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            With frmAuditoriaPicking
                .Modo = 1
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .Focus()
            End With '

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuZonaPicking_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuZonaPicking.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            With frmZona_Picking_List
                .Modo = 1
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuNuevo.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With '

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdStockJornada_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdStockJornada.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            With FrmStockJornadaGeneral
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .Focus()
            End With '

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdParametroA_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdParametroA.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            With frmProducto_Parametro_AList
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuNuevo.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Modo = 1
                .Show()
                .Focus()
            End With '

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdParametroB_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdParametroB.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            With frmProducto_Parametro_BList
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuNuevo.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Modo = 1
                .Show()
                .Focus()
            End With '

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdMovimientosDoc_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdMovimientosDoc.ItemClick

        '#GT24032022 kardex por poliza cealsa
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmMovimientosDoc
                .MdiParent = Me
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Public Sub Set_Label_Personalizados()

        Try

            Dim BeConfiguracion As New clsBeConfiguracion_alias_campos

            If Not lConfiguracionAliasCampos Is Nothing Then

                If lConfiguracionAliasCampos.Count > 0 Then

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "parametro_a")

                    If Not BeConfiguracion Is Nothing Then

                        If cmdParametroA.Caption = "Parámetros A" Then
                            cmdParametroA.Caption = BeConfiguracion.Alias_WMS
                        End If

                    End If

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "parametro_b")

                    If Not BeConfiguracion Is Nothing Then
                        If cmdParametroB.Caption = "Parámetros B" Then
                            cmdParametroB.Caption = BeConfiguracion.Alias_WMS
                        End If
                    End If


                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "clasificacion")

                    If Not BeConfiguracion Is Nothing Then
                        If mnuMantClas.Caption = "Clasificación" Then
                            mnuMantClas.Caption = BeConfiguracion.Alias_WMS

                        End If
                    End If


                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "familia")

                    If Not BeConfiguracion Is Nothing Then
                        If mnuFamilia.Caption = "Familia" Then
                            mnuFamilia.Caption = BeConfiguracion.Alias_WMS
                        End If
                    End If

                End If

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

    Private Sub cmdSalidasDiasPiso_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdSalidasDiasPiso.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmDetalleSalidas_DiasPiso
            .Modo = frmDetalleSalidas.pModo.Lista
            .MdiParent = Me
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuLogErrorWMS_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuLogErrorWMS.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmLogErrorWMS
            .Modo = frmLogErrorWMS.pModo.Lista
            .MdiParent = Me
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Function cmdAuditoriaRetroactivo_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdAuditoriaRetroactivo.ItemClick

        Dim us As New clsBeUsuario
        Dim ms As New clsBeMenu_sistema
        Dim clave As String

        Try

            ms.IdMenu = e.Link.KeyTip
            clsLnMenu_sistema.GetSingle(ms)

            If (ms.Solicitar_clave_autorizacion) Then

                us.IdUsuario = AP.UsuarioAp.IdUsuario
                clsLnUsuario.GetSingle(us)

                Try

                    clave = clsPublic.Desencriptar(us.Clave_autorizacion)

                    If (clave = "") Then Throw New Exception

                Catch ex As Exception
                    MsgBox("No se ha registrado la clave de autorización para el usuario y esta transacción necesita clave de supervisor.") : Return False
                End Try

                Dim frmlog As New frmAjusteLogin() With {.clave = clave}

                If frmlog.ShowDialog() <> DialogResult.Yes Then
                    frmlog.Dispose() : Return False
                End If

                frmlog.Dispose()

                With frmMovimientos_Retroactivo
                    .Modo = frmMovimientos_Retroactivo.pModo.Lista
                    .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                    .Show()
                    .Focus()
                End With

                SplashScreenManager.CloseForm(False)

                Return True

            Else
                Return True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End Try
    End Function

    Private Sub cmdLicenciasPorUbicacion_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdLicenciasPorUbicacion.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmLicenciasPorUbicacion
            .Modo = frmLicenciasPorUbicacion.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuAnalitica_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuAnalitica.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmIndicadores
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub cmdIngresoPoliza_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdIngresoPoliza.ItemClick


        cmdHistResGeneral.Enabled = False

        If Not permiteMenu(e.Link) Then
            cmdHistResGeneral.Enabled = True
            Return
        End If

        With frmIngresoFiscal
            .Modo = frmIngresoFiscal.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        cmdHistResGeneral.Enabled = True

    End Sub

    Private Sub mnuQAReservas_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuQAReservas.ItemClick

        If Not permiteMenu(e.Link) Then
            mnuQAReservas.Enabled = True
            Return
        End If

        With frmQA
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With

        mnuQAReservas.Enabled = True

    End Sub

    Private Sub mnuRptIngresosSAT_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuRptIngresosSAT.ItemClick

        cmdHistResGeneral.Enabled = False

        If Not permiteMenu(e.Link) Then
            cmdHistResGeneral.Enabled = True
            Return
        End If

        With frmIngresoFiscal
            .Modo = frmIngresoFiscal.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        cmdHistResGeneral.Enabled = True

    End Sub

    Private Sub mnuRptSalidasSAT_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuRptSalidasSAT.ItemClick

        cmdHistResGeneral.Enabled = False

        If Not permiteMenu(e.Link) Then
            cmdHistResGeneral.Enabled = True
            Return
        End If

        With frmSalidaFiscal
            .Modo = frmSalidaFiscal.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With

        cmdHistResGeneral.Enabled = True

    End Sub

    Private Sub mnuActualizarBD_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuActualizarBD.ItemClick

        '#EJC20220801_Actualización_Automática_De_BD

        Try

            If XtraMessageBox.Show("¿Actualizar base de datos?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                clsMantenimientoBD.Update_Database_Version(lblprg)
            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)

        End Try


    End Sub

    Private Sub mnurptExistenciasSAT_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnurptExistenciasSAT.ItemClick

        cmdHistResGeneral.Enabled = False

        If Not permiteMenu(e.Link) Then
            cmdHistResGeneral.Enabled = True
            Return
        End If

        With frmExistenciaFiscal
            .Modo = frmExistenciaFiscal.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .mnuActualizar.Enabled = .OpcionesMenu.Leer
            .Show()
            .Focus()
        End With

        cmdHistResGeneral.Enabled = True

    End Sub

    Private Sub mnuActualizarIndices_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuActualizarIndices.ItemClick

        '#EJC20220801_Actualización_Automática_De_BD

        Try

            If XtraMessageBox.Show("¿Reconstruir índices?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                clsMantenimientoBD.Ejecutar_Mantenimiento_Indices_BD(lblprg)

                XtraMessageBox.Show("Índices reconstruidos correctamente",
                                   Text,
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Information)
            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)

        End Try

    End Sub

    Private Sub mnuHabilitacionLotes_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuHabilitacionLotes.ItemClick

        If Not permiteMenu(e.Link) Then Return


        With frmListaStockControlCalidad
            .Modo = frmListaStockControlCalidad.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuGestionInventarioCalidad_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuGestionInventarioCalidad.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmListaStockControlCalidad
            .Modo = frmCienteTipo_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuTamañoTablas_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuTamañoTablas.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmTablasBD
            .Modo = frmCienteTipo_List.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .cmdActualizar.Enabled = .OpcionesMenu.Leer
            .cmdImprimir.Enabled = .OpcionesMenu.Leer
            .mnuGuardarLayoutGrid.Enabled = .OpcionesMenu.Modificar
            .mnuEliminarLayoutGrid.Enabled = .OpcionesMenu.Eliminar
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub lblWSHHURL_ItemClick(sender As Object, e As ItemClickEventArgs) Handles lblWSHHURL.ItemClick

        If Not clsBD.Instancia.WSTOMHH = "" Then

            Try
                Process.Start(clsBD.Instancia.WSTOMHH)
            Catch ex As Exception
                MessageBox.Show("No se pudo abrir la URL: " & ex.Message)
            End Try

        End If

    End Sub

    Private Sub mnuServicios_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuServicios.ItemClick

        Dim Servicio As New frmRegServicio(frmServicioC.TipoTrans.Nuevo)
        Servicio.OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
        Servicio.mnuActualizar.Enabled = Servicio.OpcionesMenu.Modificar
        Servicio.mnuGuardar.Enabled = Servicio.OpcionesMenu.Modificar
        Servicio.mnuEliminar.Enabled = Servicio.OpcionesMenu.Eliminar
        Servicio.ShowDialog()
        Servicio.Dispose()
        'listar_Servicios()

    End Sub

    Private Sub cmdReglasVencimiento_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdReglasVencimiento.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmReglaVencimiento
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)
    End Sub

    Private Sub cmdTransaccionesManufactura_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdTransaccionesManufactura.ItemClick

        Try

            If Not permiteMenu(e.Link) Then Return

            With frmManufacturaList
                .Modo = frmManufacturaList.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuNuevo.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
        Text,
        MessageBoxButtons.OK,
        MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmdTipo_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdTipo.ItemClick

        Try

            If Not permiteMenu(e.Link) Then Return

            With frmTipoTranManufactura_List
                .Modo = frmTipoTranManufactura_List.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuNuevo.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmdPreFacturacion_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdPreFacturar.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            With frmPreFactura_List
                .Modo = frmPreFactura_List.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .mnuNuevo.Enabled = .OpcionesMenu.Modificar
                .mnuActualizar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdAcuerdosyServicios_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdAcuerdosyServicios.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            With frmAcuerdoComercial
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .cmdAplicar.Enabled = .OpcionesMenu.Modificar
                .cmdRecargar.Enabled = .OpcionesMenu.Leer
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdMercaVencida_ItemClick_1(sender As Object, e As ItemClickEventArgs) Handles cmdMercaVencida.ItemClick

        cmdMercaVencida.Enabled = False

        If Not permiteMenu(e.Link) Then
            cmdMercaVencida.Enabled = True
            Return
        End If

        With frmMercaVencida
            .Modo = frmMercaVencida.pModo.Lista
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With

        cmdMercaVencida.Enabled = True

    End Sub

    Private Sub mnuTiemposRecepcion_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuTiemposRecepcion.ItemClick

        Try

            cmdMercaVencida.Enabled = False

            If Not permiteMenu(e.Link) Then
                cmdMercaVencida.Enabled = True
                Return
            End If

            With frmTiemposRecepcion
                .Modo = frmTiemposRecepcion.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .Focus()
            End With

            cmdMercaVencida.Enabled = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)


        End Try

    End Sub

    Private Sub mnuRptAjustesInventario_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuRptAjustesInventario.ItemClick


        Try

            If Not permiteMenu(e.Link) Then
                Return
            End If

            With frmReporteAjustesDet
                .Modo = frmReporteAjustesDet.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .Focus()
            End With

            cmdMercaVencida.Enabled = True

        Catch ex As Exception

        End Try

    End Sub

    Private Sub mnuTiemposDespacho_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuTiemposDespacho.ItemClick

        Try

            If Not permiteMenu(e.Link) Then
                Return
            End If

            With frmTiemposDespacho
                .Modo = frmTiemposDespacho.pModo.Lista
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .Focus()
            End With

            cmdMercaVencida.Enabled = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)


        End Try

    End Sub

    Private Sub mnuKPIResumen_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuKPIResumen.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmIndicadores
            .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub mnuAbrirTableroWMS_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuVisualizarTableroWMS.ItemClick

        Try

            With frmDashViewer1
                .IdentificadorTablero = -1
                .MdiParent = Me
                .Show()
                .Focus()
            End With

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuTransaccionesPendientesReenvio_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuTransaccionesPendientesReenvio.ItemClick

        Try
            If Not permiteMenu(e.Link) Then Return

            With frmRegistrosInterfaceRes
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Modo = frmRegistrosInterface.pModo.Transacciones_Reenvio
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdMovimientosEstado_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdMovimientosEstado.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmReportCambiosEstado
                .Modo = frmReportCambiosEstado.pModo.Lista
                .MdiParent = Me
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdPackingDespachados_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdPackingDespachados.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            With frmPackingDespachado
                .Modo = frmPackingDespachado.pModo.Lista
                .MdiParent = Me
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    'Private Sub cmdMovimientosControlCalidad_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdMovimientosControlCalidad.ItemClick

    '    Try

    '    Catch ex As Exception

    '    End Try

    'End Sub

    Private Sub cmdTalla_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuTalla.ItemClick

        Try
            If Not permiteMenu(e.Link) Then Return

            With frmProducto_TallaList
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .cmdNuevo.Enabled = .OpcionesMenu.Modificar
                .cmdActualizar.Enabled = .OpcionesMenu.Leer
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdColor_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuColor.ItemClick
        Try
            If Not permiteMenu(e.Link) Then Return

            With frmProducto_ColorList
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .cmdNuevo.Enabled = .OpcionesMenu.Modificar
                .cmdActualizar.Enabled = .OpcionesMenu.Leer
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub mnuProductividadPicking_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuProductividadPicking.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            With frmProductividadPicking
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnurptTransaccionesOP_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnurptTransaccionesOP.ItemClick

        If Not permiteMenu(e.Link) Then Return

        Try

            With frmRptTransaccionesOP
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuCampaña_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuCampaña.ItemClick
        Try
            If Not permiteMenu(e.Link) Then Return

            With frmCampaña_List
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .cmdNuevo.Enabled = .OpcionesMenu.Modificar
                .cmdActualizar.Enabled = .OpcionesMenu.Leer
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub mnuInterfaceDMS_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuInterfaceDMS.ItemClick
        If Not permiteMenu(e.Link) Then Return

        Try

            Dim pEjecutable As String = "DMS.exe"

            If AP.IdConfiguracionInterface <> 0 Then

                Ejecutar_Interface(" -" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-0-0" & "-" & clsBD.Instancia.NombreInstancia, Me, pEjecutable)

            Else

                XtraMessageBox.Show(String.Format("La Bodega {0} de la Empresa {1} no  tiene definida configuración para interface", AP.NomBodega, AP.NomEmpresa),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub frmMenu_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        If XtraMessageBox.Show("¿Está seguro de salir de la aplicación?",
                       Text,
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question) = DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub mnuVerificacionBOF_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuVerificacionBOF.ItemClick
        If Not permiteMenu(e.Link) Then Return
        Try

            Cierra_Instancia_Previa(frmPedido_List)

            With frmPedido_List
                .MdiParent = Me : .OpcionesMenu = clsLnRol.Get_MenuRol_Opciones(AP.UsuarioAp.IdRol, e.Link.KeyTip)
                .Modo = frmPedido_List.pModo.verificacion
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("La Bodega {0} de la Empresa {1} no  tiene definida configuración para interface", AP.NomBodega, AP.NomEmpresa),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Public Sub Cierra_Instancia_Previa(ByRef Myform As Form)

        Try

            For Each objForm In My.Application.OpenForms
                If (Trim(objForm.Name) = Trim(Myform.Name)) Then
                    Myform.Close()
                    Exit For
                End If
            Next

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuImpresionBarraPallet_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuImpresionBarraPallet.ItemClick
        Try

            If Not e Is Nothing Then
                If Not permiteMenu(e.Link) Then Return
            End If

            Cierra_Instancia_Previa(frmImpresion_OC_RFID)

            With frmImpresion_OC_RFID
                .pTransOC_Enc = Nothing
                .MdiParent = Me
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub mnuListaIngresoTag_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuListaIngresoTag.ItemClick
        Try

            If Not e Is Nothing Then
                If Not permiteMenu(e.Link) Then Return
            End If

            Cierra_Instancia_Previa(frmDocIngresoRFID_List)

            With frmDocIngresoRFID_List
                .Modo = frmDocIngresoRFID_List.pModo.Lista
                .MdiParent = Me
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub mnuListaSalidaTag_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuListaSalidaTag.ItemClick
        Try

            If Not e Is Nothing Then
                If Not permiteMenu(e.Link) Then Return
            End If

            Cierra_Instancia_Previa(frmDocSalidaRFID_List)

            With frmDocSalidaRFID_List
                .Modo = frmDocSalidaRFID_List.pModo.Lista
                .MdiParent = Me
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub mnuStock_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuStockTag.ItemClick
        Try

            If Not e Is Nothing Then
                If Not permiteMenu(e.Link) Then Return
            End If

            Cierra_Instancia_Previa(frmStockRFID)

            With frmStockRFID
                .MdiParent = Me
                .Show()
                .Focus()
            End With

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

End Class