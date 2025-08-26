Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmBodega_List

    Public pObjBodega As clsBeBodega_area
    Public Property IdBodegaAExcluir As Integer = 0
    Public Property lBodegasSeleccionadas As New List(Of clsBeBodegaSeleccion)

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub Listar_Bodegas()

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Listando bodegas...")

        Try

            Dim DT As New DataTable
            DT = clsLnBodega.Listar(AP.IdEmpresa, IdBodegaAExcluir, chkActivos.Checked)

            Dgrid.DataSource = DT

            If (GridView1.Columns.Count > 0) Then

                Try

                    GridView1.BestFitColumns()

                    GridView1.Columns("IdEmpresa").Visible = False

                    If Modo = pModo.Seleccion Then
                        GridView1.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True
                        GridView1.OptionsSelection.MultiSelect = True
                        GridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
                        grpSeleccionar.Visible = True
                    Else
                        GridView1.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False
                        grpSeleccionar.Visible = False
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Try

            End If

            lblRegs.Caption = "Registros: " & GridView1.RowCount

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Listar_Bodegas()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub Nueva_Bodega()

        Try

            Cierra_Instancia_Previa(frmBodega)


            With frmBodega
                .Modo = frmBodega.TipoTrans.Nuevo
                .Listar = AddressOf Listar_Bodegas
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
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

    Private Sub mnuNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNuevo.ItemClick
        mnuNuevo.Enabled = False
        Nueva_Bodega()
        mnuNuevo.Enabled = True
    End Sub

    Private Sub Procesa_Registro()

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmBodega)

                    With frmBodega
                        .Modo = frmBodega.TipoTrans.Editar
                        .OpcionesMenu = OpcionesMenu
                        .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                        .mnuActualizar.Enabled = .OpcionesMenu.Modificar
                        .mnuEliminar.Enabled = .OpcionesMenu.Eliminar
                        .Listar = AddressOf Listar_Bodegas
                        .pBeBodega.IdBodega = Dr.Item("Correlativo")
                        .pBeBodega.Nombre = Dr.Item("Nombre")
                        .WindowState = FormWindowState.Maximized
                        .Show()
                        .Focus()
                    End With

                    GridView1.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Then
                    Hide()
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Procesa_Registro()
    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkActivos.CheckedChanged
        Listar_Bodegas()
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
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & Text

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridView1.RowStyle

        Try

            GridView1.OptionsBehavior.Editable = False
            GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
            GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
            GridView1.OptionsSelection.EnableAppearanceFocusedRow = True
            GridView1.OptionsSelection.EnableAppearanceHideSelection = True
            GridView1.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView1.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView1.Appearance.FocusedRow.ForeColor = Color.White
            GridView1.Appearance.SelectedRow.ForeColor = Color.White
            GridView1.Appearance.SelectedRow.Options.UseBackColor = True
            GridView1.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Dgrid_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgrid.KeyDown
        If e.KeyCode = Keys.Enter Then Procesa_Registro()
    End Sub

    Private Sub frmBodega_List_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub frmBodega_List_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Listar_Bodegas()
    End Sub

    Private Sub mnuSeleccionar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSeleccionar.ItemClick

        Try

            Dim Rows As New ArrayList()
            Dim selectedRowHandles As Integer() = GridView1.GetSelectedRows()

            Dim I As Integer
            Dim vIdBodega As Integer = 0
            Dim vCodigoBodega As String = ""
            Dim vNombreBodega As String = ""

            If XtraMessageBox.Show("¿Unificar " & selectedRowHandles.Length & " bodegas?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                lBodegasSeleccionadas = New List(Of clsBeBodegaSeleccion)

                Dim BeBodegaSeleccion As New clsBeBodegaSeleccion()

                For I = 0 To selectedRowHandles.Length - 1

                    Dim selectedRowHandle As Integer = selectedRowHandles(I)

                    vIdBodega = GridView1.GetRowCellValue(selectedRowHandle, "Correlativo")
                    vCodigoBodega = GridView1.GetRowCellValue(selectedRowHandle, "Código")
                    vNombreBodega = GridView1.GetRowCellValue(selectedRowHandle, "nombre")

                    BeBodegaSeleccion = New clsBeBodegaSeleccion()

                    BeBodegaSeleccion.IdBodega = vIdBodega
                    BeBodegaSeleccion.Codigo = vCodigoBodega
                    BeBodegaSeleccion.Nombre = vNombreBodega
                    lBodegasSeleccionadas.Add(BeBodegaSeleccion)

                Next

                DialogResult = DialogResult.OK

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmdImportaExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImportaExcel.ItemClick

    End Sub
End Class