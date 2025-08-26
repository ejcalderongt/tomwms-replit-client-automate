Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmTraslado_ProductoList
    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Public pObjeto As New clsBeTrans_tras_enc               'objeto a obtener

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmTraslado_ProductoList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        cargarListadeTraslados()
    End Sub

    Private Sub cargarListadeTraslados()
        Dim datos As New DataTable()
        Try
            '  Dim lista As New List(Of clsBeTrans_tras_enc)
            Try
                datos = clsLnTrans_tras_enc.ListarTraslados(chkActivos.Checked, txtFiltro.Text) '  BDRol.ListarxFiltro(txtFiltro.Text, AP.IdEmpresa)
                If datos.Rows.Count > 0 Then
                    Dgrid.DataSource = datos

                    If GridView1.RowCount > 0 Then
                        lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                        GridView1.OptionsView.ColumnAutoWidth = False
                        GridView1.BestFitColumns()
                    End If
                Else
                    Dgrid.DataSource = Nothing
                End If
            Catch ex As Exception
                XtraMessageBox.Show(" El error es :" + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtFiltro_EditValueChanged(sender As Object, e As EventArgs) Handles txtFiltro.EditValueChanged
        cargarListadeTraslados()
    End Sub

    Private Sub mnuNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNuevo.ItemClick

        mnuNuevo.Enabled = False

        Dim usuario As New frmTraslado_Producto(frmTraslado_Producto.TipoTrans.Nuevo)

        usuario.OpcionesMenu = OpcionesMenu()
        usuario.mnuGuardar.Enabled = OpcionesMenu.Modificar
        usuario.mnuActualizar.Enabled = OpcionesMenu.Modificar
        usuario.mnuEliminar.Enabled = OpcionesMenu.Eliminar

        usuario.ShowDialog()
        usuario.Dispose()

        cargarListadeTraslados()

        mnuNuevo.Enabled = True

    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub mnuImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimir.ItemClick

        mnuImprimir.Enabled = False
        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Imprimiendo...")
        Imprimir_Vista()
        mnuImprimir.Enabled = True
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

        Dim reportHeader As String = vbNewLine & "Listado de Translado " + AP.NomEmpresa

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImportarExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImportarExcel.ItemClick
        cmdImportarExcel.Enabled = False
        XtraMessageBox.Show("En contruccion", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        'Dim Carga As New frmCargaExcel()
        'Carga.pNombreMantenimiento = "Roloperador"
        'Carga.pTipoMantenimiento = "RolOperador"
        'Carga.Listar = New frmCargaExcel.Operar(AddressOf cargarListadeTraslados)
        'Carga.ShowDialog()
        'Carga.Dispose()
        cmdImportarExcel.Enabled = True
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        mnuActualizar.Enabled = False
        cargarListadeTraslados()
        mnuActualizar.Enabled = True
    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick

        Try
            If (GridView1.RowCount > 0) Then
                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle
                If Modo = pModo.Lista Then
                    Dim frmproductoTrasladoBodega As New frmTraslado_Producto(frmTraslado_Producto.TipoTrans.Editar)

                    frmproductoTrasladoBodega.pObjetoTraslado.IdTrasladoEnc = Dr.Item("Traslado")

                    frmproductoTrasladoBodega.OpcionesMenu = OpcionesMenu()
                    frmproductoTrasladoBodega.mnuGuardar.Enabled = OpcionesMenu.Modificar
                    frmproductoTrasladoBodega.mnuActualizar.Enabled = OpcionesMenu.Modificar
                    frmproductoTrasladoBodega.mnuEliminar.Enabled = OpcionesMenu.Eliminar

                    frmproductoTrasladoBodega.ShowDialog()
                    frmproductoTrasladoBodega.Dispose()
                    cargarListadeTraslados()
                    GridView1.FocusedRowHandle = lSelectionIndex
                ElseIf Modo = pModo.Seleccion Then
                    pObjeto.IdTrasladoEnc = Dr.Item("Traslado")
                    pObjeto.NoGuia = Dr.Item("Guia")
                    Hide()
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

    Private Sub cbxActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        cargarListadeTraslados()
    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkActivos.CheckedChanged
        cargarListadeTraslados()
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
End Class