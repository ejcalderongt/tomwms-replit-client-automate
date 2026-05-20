Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraEditors
Imports System.Reflection

Public Class frmMenu

    Private Property Interface_A_Ejecutar As pInterfaceAEjecutar = -1
    Private Property IdConfiguracionInterface As Integer = -1

    Public Sub New(ByVal InterfaceAEjecutar As Integer, ByVal pIdConfiguracion As Integer, ByVal pIdUsuario As Integer)

        InitializeComponent()

        Interface_A_Ejecutar = InterfaceAEjecutar
        IdConfiguracionInterface = pIdConfiguracion
        IdUsuario = pIdUsuario

    End Sub

    Public Sub New(ByVal pIdConfiguracion As Integer)

        InitializeComponent()

        IdConfiguracionInterface = pIdConfiguracion

    End Sub

    Public Sub New(ByVal InterfaceAEjecutar As Integer, ByVal pIdConfiguracion As Integer)

        InitializeComponent()

        Interface_A_Ejecutar = InterfaceAEjecutar
        IdConfiguracionInterface = pIdConfiguracion

    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub mnuPrueba_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPrueba.ItemClick

        Try

            With frmEjecucion
                .MdiParent = Me
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With

            rbMain.SelectedPage = rbMain.MergedPages(0)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub frmMenu_Load(sender As Object, e As EventArgs) Handles Me.Load

        UseWaitCursor = True

        Try

            NotifyIcon1.Icon = Me.Icon
            NotifyIcon1.Visible = True
            NotifyIcon1.Text = "MI3_TOMWMS"

            Set_Info_Conexion()

            BarStaticItem1.Caption = gVersionApp & " " & FormatoFechas.tFecha(gFechaVersion)

            rbMain.MdiMergeStyle = RibbonMdiMergeStyle.OnlyWhenMaximized

            If RemoteCallBack Then

                mnuPrueba.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

                With frmEjecucion
                    .MdiParent = Me
                    .WindowState = FormWindowState.Maximized
                    .Interface_A_Ejecutar = Interface_A_Ejecutar
                    .Show()
                    .Focus()
                End With

                rbMain.SelectedPage = rbMain.MergedPages(0)

            End If

        Catch ex As Exception
            XtraMessageBox.Show("Error: " & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            UseWaitCursor = False
        End Try

    End Sub

    Private Sub Set_Info_Conexion()

        Try

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

            lblServerAPP.Caption += " " & BD.Instancia.Server
            lblBDAPP.Caption += " " & BD.Instancia.NombreBD
            lblNombrePC.Caption = Net.Dns.GetHostName()
            lblEmpresa.Caption += " " & BD.Instancia.SAP_COMPANY_DB

            If Not BeConfigEnc Is Nothing Then
                lblBodega.Caption += " " & clsLnBodega.Get_Nombre_Bodega_By_IdBodega(BeConfigEnc.Idbodega)
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

    Private Sub frmMenu_DragLeave(sender As Object, e As EventArgs) Handles Me.DragLeave

    End Sub

    Private Sub frmMenu_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        If Me.WindowState = FormWindowState.Minimized Then
            NotifyIcon1.Visible = True
            Me.Hide()
        End If

    End Sub

    Private Sub frmMenu_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        If e.CloseReason = CloseReason.UserClosing Then
            e.Cancel = True
            Me.Hide()
        End If

    End Sub

    Private Sub NotifyIcon1_DoubleClick(sender As Object, e As EventArgs) Handles NotifyIcon1.DoubleClick

        Me.Show()
        Me.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub mnuSalir_Click(sender As Object, e As EventArgs) Handles mnuSalir.Click

        NotifyIcon1.Visible = False
        Application.Exit()

    End Sub
End Class