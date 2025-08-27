Imports System.Reflection
Imports DevExpress.XtraBars
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen
Imports TOMWMS

Public Class frmmenu

    Public Id_StandAlone As Boolean
    Public Property BotonesVisibles As Boolean = True
    Public Property pFincaDefault As String = ""
    Public Property pImpresoraDefault As String = ""

    Private Sub frmmenu_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            Me.IsMdiContainer = True

            rbMain.ResumeLayout(True)
            rbMain.MdiMergeStyle = RibbonMdiMergeStyle.Always

            AlternarBotonesRibbon()

            cmdImpresora.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

            If Not Id_StandAlone Then
                With frmImprimir_Etiqueta
                    .pImpresoraDefault = pImpresoraDefault
                    .MdiParent = Me
                    .Show()
                    .Focus()
                End With
            End If

            '#GT05032025: mantener los menus colapsados para que no ocupen espacio, pero si se da doble clic en la pestaña, se expandiran.
            Ribbon.Minimized = True
            Ribbon.ShowExpandCollapseButton = False
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
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub cmdSucursal_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdSucursal.ItemClick

        Try
            With frmSucursalList
                .Modo = frmSucursalList.pModo.Lista
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

    Private Sub cmdProducto_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdProducto.ItemClick
        Try
            With frmProductoList
                .Modo = frmProductoList.pModo.Lista
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

    Private Sub cmdImpresionRapida_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImpresionRapida.ItemClick
        Try
            With frmImprimir_Etiqueta
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



    Public Sub AlternarBotonesRibbon()

        Try

            BotonesVisibles = Not BotonesVisibles
            cmdSucursal.Visibility = If(BotonesVisibles, DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never)
            cmdProducto.Visibility = If(BotonesVisibles, DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never)
            cmdImpresora.Visibility = If(BotonesVisibles, DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
        Text,
        MessageBoxButtons.OK,
        MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdImpresora_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImpresora.ItemClick
        Try
            With frmPrintManager
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