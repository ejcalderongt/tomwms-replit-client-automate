Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen
Imports TOMWMS.frmProducto_Clasificacion

Public Class frmCampaña_List

    Private vNombreArchivoLayOutGrid As String = ""

    Public pObjCampaña As clsBeCampaña

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum


    Private Sub frmCampaña_List_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Listando campañas...")

            vNombreArchivoLayOutGrid = "frmCampaña_List.xml"
            Listar_Campañas()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            SplashScreenManager.CloseForm(False)
        End Try
    End Sub

    Private Sub Listar_Campañas()

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Listando campañas...")

        Try

            Dim DT As New DataTable
            DT = clsLnCampaña.Listar_By_Estado(chkActivo.Checked)

            If DT IsNot Nothing AndAlso DT.Rows.Count > 0 Then
                Dgrid.DataSource = DT

                If (GridView1.Columns.Count > 0) Then

                    GridView1.Columns("IdCampaña").Caption = "Correlativo"
                    GridView1.Columns("FechaInicio").Caption = "Campaña Inicia"
                    GridView1.Columns("FechaFin").Caption = "Campaña Finaliza"
                    GridView1.Columns("user_agr").Visible = False
                    GridView1.Columns("fec_agr").Visible = False
                    GridView1.Columns("user_mod").Visible = False
                    GridView1.Columns("fec_mod").Visible = False
                    GridView1.OptionsView.ColumnAutoWidth = False
                    GridView1.BestFitColumns()
                    lblRegs.Caption = "Registros: " & GridView1.RowCount

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
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

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdNuevo.ItemClick
        Nuevo()
    End Sub

    Private Sub Nuevo()
        Try
            Cierra_Instancia_Previa(frmCampaña)

            With frmCampaña
                .Modo = frmCampaña.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .OpcionesMenu = OpcionesMenu()
                .cmdGuardar.Enabled = OpcionesMenu.Modificar
                .cmdActualizar.Enabled = OpcionesMenu.Modificar
                .cmdEliminar.Enabled = OpcionesMenu.Eliminar
                .InvokeListarCampañas = AddressOf Listar_Campañas
                .WindowState = FormWindowState.Normal
                .Show()
                .Focus()
            End With

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Procesar_Registro()
    End Sub
    Private Sub Procesar_Registro()

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle
                Dim objCampaña As New clsBeCampaña
                objCampaña = clsLnCampaña.GetSingle(Dr.Item("IdCampaña"))

                Cierra_Instancia_Previa(frmCampaña)

                With frmCampaña
                    .Modo = frmCampaña.TipoTrans.Editar
                    .pObjCampaña = objCampaña
                    .InvokeListarCampañas = AddressOf Listar_Campañas
                    .MdiParent = MdiParent
                    .OpcionesMenu = OpcionesMenu()
                    .cmdGuardar.Enabled = OpcionesMenu.Modificar
                    .cmdActualizar.Enabled = OpcionesMenu.Modificar
                    .cmdEliminar.Enabled = OpcionesMenu.Eliminar
                    .WindowState = FormWindowState.Normal
                    .Show()
                    .Focus()
                End With

                GridView1.FocusedRowHandle = lSelectionIndex
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        cmdImprimir.Enabled = False

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Imprimiendo...")

        Imprimir_Vista()

        cmdImprimir.Enabled = True
    End Sub

    Private Sub Imprimir_Vista()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

            Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
            TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {leftColumnFoot})
            phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
            phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = Dgrid
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            SplashScreenManager.CloseForm(False)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & Text

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

End Class