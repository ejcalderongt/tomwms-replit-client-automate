Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmProducto_FamiliaList

    Public pIdPropietario As Integer
    Public pObjPF As clsBeProducto_familia
    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum
    Private Sub frmProducto_FamiliaList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        listarProductoFamilia()
    End Sub

    Private Sub listarProductoFamilia()

        Try

            Dgrid.DataSource = Nothing

            Dim lista As New List(Of clsBeProducto_familia)
            lista = clsLnProducto_familia.Get_All_Filtro(chkActivos.Checked, "", pIdPropietario).ToList()

            If lista IsNot Nothing AndAlso lista.Count > 0 Then

                Dim DT As New DataTable("Familia")
                DT.Columns.Add("IdFamilia", GetType(Integer))
                DT.Columns.Add("IdPropietario", GetType(Integer))
                DT.Columns.Add("Propietario", GetType(String))
                DT.Columns.Add("Familia", GetType(String))
                DT.Columns.Add("Activo", GetType(Boolean))
                DT.Columns.Add("User_agr", GetType(String))
                DT.Columns.Add("Fec_agr", GetType(DateTime))
                DT.Columns.Add("User_mod", GetType(String))
                DT.Columns.Add("Fec_mod", GetType(DateTime))

                For Each Obj As clsBeProducto_familia In lista
                    DT.Rows.Add(Obj.IdFamilia, Obj.Propietario.IdPropietario, Obj.Propietario.Nombre_comercial, Obj.Nombre, Obj.Activo, Obj.User_agr, Obj.Fec_agr, Obj.User_mod, Obj.Fec_mod)
                Next

                Dgrid.DataSource = DT

                If (GridView1.Columns.Count <> 0) Then
                    Try

                        GridView1.Columns("IdFamilia").Caption = "Código"
                        GridView1.Columns("IdPropietario").Visible = False
                        GridView1.Columns("Activo").Visible = False
                        GridView1.Columns("User_agr").Visible = False
                        GridView1.Columns("Fec_agr").Visible = False
                        GridView1.Columns("User_mod").Visible = False
                        GridView1.Columns("Fec_mod").Visible = False
                        GridView1.OptionsView.ColumnAutoWidth = False
                        GridView1.BestFitColumns()

                    Catch ex As Exception
                        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End Try
                End If

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtFiltro_EditValueChanged(sender As Object, e As EventArgs)
        listarProductoFamilia()
    End Sub

    Private Sub Nueva_FamiliaProducto()

        Try

            Cierra_Instancia_Previa(frmProducto_Familia)

            With frmProducto_Familia
                .Modo = frmProducto_Familia.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .OpcionesMenu = OpcionesMenu()
                .mnuGuardar.Enabled = OpcionesMenu.Modificar
                .mnuActualizar.Enabled = OpcionesMenu.Modificar
                .mnuEliminar.Enabled = OpcionesMenu.Eliminar
                .InvokeListarProductoFamilia = AddressOf listarProductoFamilia
                .WindowState = FormWindowState.Normal
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
        Nueva_FamiliaProducto()
        mnuNuevo.Enabled = True
    End Sub

    Private Sub Procesar_Registro()

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim Obj As New clsBeProducto_familia

                Obj = clsLnProducto_familia.GetSingle(Dr.Item("IdFamilia"))
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmProducto_Familia)

                    With frmProducto_Familia
                        .Modo = frmProducto_Familia.TipoTrans.Editar
                        .pObjPF = Obj
                        .InvokeListarProductoFamilia = AddressOf listarProductoFamilia
                        .MdiParent = MdiParent
                        .OpcionesMenu = OpcionesMenu()
                        .mnuGuardar.Enabled = OpcionesMenu.Modificar
                        .mnuActualizar.Enabled = OpcionesMenu.Modificar
                        .mnuEliminar.Enabled = OpcionesMenu.Eliminar
                        .WindowState = FormWindowState.Normal
                        .Show()
                        .Focus()
                    End With

                    GridView1.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Then
                    pObjPF = Obj
                    Hide()
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Procesar_Registro()
    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkActivos.CheckedChanged
        listarProductoFamilia()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        mnuActualizar.Enabled = False
        listarProductoFamilia()
        mnuActualizar.Enabled = True
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

        Dim reportHeader As String = vbNewLine & "Listado de Familias"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImportarExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImportarExcel.ItemClick

        cmdImportarExcel.Enabled = False
        Dim Carga As New frmCargaExcel()
        Carga.pPropietario = True
        Carga.pPropietario = True
        Carga.pNombreMantenimiento = "Producto Familia"
        Carga.pTipoMantenimiento = "ProductoFamilia"
        Carga.Listar = New frmCargaExcel.Operar(AddressOf listarProductoFamilia)
        Carga.ShowDialog()
        Carga.Dispose()
        cmdImportarExcel.Enabled = True

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
        If e.KeyCode = Keys.Enter Then Procesar_Registro()
    End Sub

    Private Sub frmProducto_FamiliaList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class