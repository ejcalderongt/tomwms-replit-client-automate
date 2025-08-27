Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmRevisionProducto

    Public Delegate Sub Operar()
    Public Listar As Operar
    Private pListTL As New List(Of clsBeTransacciones_log)

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmRevisionProducto_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Cargar()

    End Sub

    Private Sub Cargar()

        Try

            DsRevisionProducto.Revision.Rows.Clear()
            Grid.BeginUpdate()

            Dim ListRevision As List(Of clsBeReabasto) = clsLnProducto.Get_Reabastecimientos_Productos.ToList

            For Each r As clsBeReabasto In ListRevision

                Dim lRow As DataRow = Nothing
                lRow = DsRevisionProducto.Revision.NewRow
                lRow.Item("Seleccionar") = False
                lRow.Item("Producto") = r.NombreProducto
                lRow.Item("Presentacion") = r.Presentacion
                lRow.Item("Estado") = r.Estado
                lRow.Item("Ubicacion") = r.Ubicacion

                Dim lMaximo As Double = Math.Round(r.Maximo, 2)
                Dim lDisp As Double = Math.Round(r.Disponible, 2)

                lRow.Item("Minimo") = Math.Round(r.Minimo, 2)
                lRow.Item("Maximo") = lMaximo
                lRow.Item("Cantidad") = lDisp

                ' CONSULTA CUANTO HAY EN BODEGA
                Dim ObjS As New clsBeStock() With {.IdProductoBodega = r.IdProductoBodega, .ProductoEstado = New clsBeProducto_estado()}
                ObjS.ProductoEstado.IdEstado = r.IdProductoEstado
                ObjS.Presentacion = New clsBeProducto_Presentacion
                ObjS.Presentacion.IdPresentacion = r.IdPresentacion
                ObjS.IdUnidadMedida = r.IdUnidadMedida
                clsLnStock.Get_Existencia_Disp_By_IdProducto(ObjS, AP.IdBodega)

                lRow.Item("CantBodega") = Math.Round(ObjS.Cantidad, 2)
                ' ------------------------------

                If ObjS.Cantidad < (lMaximo - lDisp) Then
                    lRow.Item("StockInferior") = True
                Else
                    lRow.Item("StockInferior") = False
                End If

                lRow.Item("IdPropietarioBodega") = r.IdPropietarioBodega
                lRow.Item("IdProductoBodega") = r.IdProductoBodega
                lRow.Item("IdPresentacion") = r.IdPresentacion
                lRow.Item("IdProductoEstado") = r.IdProductoEstado
                lRow.Item("IdUnidadMedida") = r.IdUnidadMedida
                lRow.Item("IdUbicacion") = r.IdUbicacion

                DsRevisionProducto.Revision.AddRevisionRow(lRow)

            Next

            Grid.EndUpdate()
            Grid.ForceInitialize()

            GridView.OptionsView.ShowFooter = True

            If GridView.Columns.Count > 0 Then

                GridView.Columns("Minimo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView.Columns("Minimo").SummaryItem.DisplayFormat = "{0:n2}"

                GridView.Columns("Minimo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView.Columns("Minimo").DisplayFormat.FormatString = "{0:n2}"

                GridView.Columns("Maximo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView.Columns("Maximo").SummaryItem.DisplayFormat = "{0:n2}"

                GridView.Columns("Maximo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView.Columns("Maximo").DisplayFormat.FormatString = "{0:n2}"

                GridView.Columns("Cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView.Columns("Cantidad").SummaryItem.DisplayFormat = "{0:n2}"

                GridView.Columns("Cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView.Columns("Cantidad").DisplayFormat.FormatString = "{0:n2}"

                GridView.Columns("CantBodega").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView.Columns("CantBodega").SummaryItem.DisplayFormat = "{0:n2}"

                GridView.Columns("CantBodega").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView.Columns("CantBodega").DisplayFormat.FormatString = "{0:n2}"

            End If

            Dim ritem As RepositoryItemCheckEdit = TryCast(GridView.Columns("Seleccionar").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ritem_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If Not ritem Is Nothing Then

                Dim Dr As DataRowView = GridView.GetFocusedRow
                Dim lIndex As Integer = -1

                lIndex = pListTL.FindIndex(Function(b) b.IdProductoBodega = Dr.Item("IdProductoBodega") _
                                               AndAlso b.IdPresentacion = Dr.Item("IdPresentacion") _
                                               AndAlso b.IdProductoEstado = Dr.Item("IdProductoEstado") _
                                               AndAlso b.IdUnidadMedida = Dr.Item("IdUnidadMedida") _
                                               AndAlso b.IdUbicacion = Dr.Item("IdUbicacion"))

                If lIndex > -1 Then

                    If ritem.Checked = False Then
                        pListTL.RemoveAll(Function(b) b.IdProductoBodega = Dr.Item("IdProductoBodega") _
                                               AndAlso b.IdPresentacion = Dr.Item("IdPresentacion") _
                                               AndAlso b.IdProductoEstado = Dr.Item("IdProductoEstado") _
                                               AndAlso b.IdUnidadMedida = Dr.Item("IdUnidadMedida") _
                                               AndAlso b.IdUbicacion = Dr.Item("IdUbicacion"))
                        ' MsgBox("Existìa, pero lo desmarcaron y lo eliminè y todos felices")
                    End If

                Else

                    Dim stockInferior As Boolean = Dr.Item("NA")

                    If stockInferior Then
                        Dim lProducto As String = Dr.Item("Producto").ToString
                        If (MessageBox.Show(String.Format("No se puede abastecer el producto {0} porque no hay suficiente stock en bodega. ¿Desea continuar?", lProducto),
                                           Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) = False Then
                            Return
                        End If
                    End If

                    'MsgBox("No existe, pero le voy a hacer huevos y lo agrego")
                    'Dim bo As clsBeStock = gWCFStock.GetByIdProductoBodega(CInt(Dr.Item("IdProductoBodega")), CInt(Dr.Item("IdPresentacion")))
                    Dim obj As New clsBeTransacciones_log
                    obj.IdEmpresa = AP.UsuarioAp.IdEmpresa
                    obj.IdPropietarioBodega = CInt(Dr.Item("IdPropietarioBodega"))
                    obj.IdProductoBodega = CInt(Dr.Item("IdProductoBodega"))
                    obj.IdPresentacion = CInt(Dr.Item("IdPresentacion"))
                    obj.IdProductoEstado = CInt(Dr.Item("IdProductoEstado"))
                    obj.IdUnidadMedida = CInt(Dr.Item("IdUnidadMedida"))
                    obj.IdUbicacion = CInt(Dr.Item("IdUbicacion"))
                    '   obj.= Cantidad_reabasto = CDbl(Dr.Item("CantUbicar"))
                    obj.MinimoTemp = CDbl(Dr.Item("Minimo"))
                    obj.MaximoTemp = CDbl(Dr.Item("Maximo"))
                    obj.StockInferior = stockInferior
                    obj.User_agr = AP.UsuarioAp.IdUsuario
                    obj.Fec_agr = Now
                    obj.User_mod = AP.UsuarioAp.IdUsuario
                    obj.Fec_mod = Now
                    obj.Activo = True
                    obj.IsNew = True
                    obj.Checked = True
                    pListTL.Add(obj)

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar()
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
            printLink.Component = Grid
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Revisión de Producto"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick

        Imprimir_Vista()

    End Sub

    Private Sub cmdPosponerTodo_Click(sender As Object, e As EventArgs) Handles cmdPosponerTodo.Click

        Try

            If GridView.RowCount > 0 Then

                If MessageBox.Show("¿Desea posponer todos los productos de la lista?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Dim lista As New List(Of clsBeTransacciones_log)

                    For i As Integer = 0 To GridView.RowCount - 1

                        Dim Dr As DataRowView = GridView.GetRow(i)

                        'Dim bo As clsBeStock = gWCFStock.GetByIdProductoBodega(CInt(Dr.Item("IdProductoBodega")), CInt(Dr.Item("IdPresentacion")))

                        Dim Obj As New clsBeTransacciones_log
                        Obj.IdEmpresa = AP.UsuarioAp.IdEmpresa
                        Obj.IdPropietarioBodega = CInt(Dr.Item("IdPropietarioBodega"))
                        Obj.IdProductoBodega = CInt(Dr.Item("IdProductoBodega"))
                        Obj.IdPresentacion = CInt(Dr.Item("IdPresentacion"))
                        Obj.IdProductoEstado = CInt(Dr.Item("IdProductoEstado"))
                        Obj.IdUnidadMedida = CInt(Dr.Item("IdUnidadMedida"))
                        Obj.IdUbicacion = CInt(Dr.Item("IdUbicacion"))
                        Obj.Cantidad_reabasto = CDbl(Dr.Item("CantUbicar"))
                        Obj.User_agr = AP.UsuarioAp.IdUsuario
                        Obj.Fec_agr = Now
                        Obj.User_mod = AP.UsuarioAp.IdUsuario
                        Obj.Fec_mod = Now
                        Obj.Activo = True
                        Obj.IsNew = True
                        Obj.Checked = True
                        pListTL.Add(Obj)

                    Next

                    If lista.Count > 0 Then
                        If clsLnTransacciones_log.Posponer(lista) Then
                            XtraMessageBox.Show("Se pospusieron todos los productos de la lista.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Cargar()
                            Listar.Invoke()
                        End If
                    End If

                End If

            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdPosponerSeleccionado_Click(sender As Object, e As EventArgs) Handles cmdPosponerSeleccionado.Click

        Try

            If pListTL IsNot Nothing Then
                If pListTL.TrueForAll(Function(b) b.Checked = False) Then
                    XtraMessageBox.Show("Seleccione que productos desea posponer.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    If MessageBox.Show("¿Desea posponer los productos seleccionados?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        If clsLnTransacciones_log.Posponer(pListTL) Then
                            pListTL = New List(Of clsBeTransacciones_log)
                            XtraMessageBox.Show("Se pospusieron los productos seleccionados.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Cargar()
                            Listar.Invoke()
                        End If
                    End If
                End If
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdEnviarTarea_Click(sender As Object, e As EventArgs) Handles cmdEnviarTarea.Click

        Try

            If pListTL IsNot Nothing Then
                If pListTL.TrueForAll(Function(b) b.Checked = False) Then
                    XtraMessageBox.Show("Seleccione que productos desea enviar a tarea.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    If MessageBox.Show("¿Desea enviar a tarea los productos seleccionados?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        'If clsLnTransacciones_log.Enviar_Tareas(pListTL, AP.HostName, AP.IdBodega, AP.UsuarioAp.IdUsuario) Then
                        '    pListTL = New List(Of clsBeTransacciones_log)
                        '    XtraMessageBox.Show("Se pospusieron los productos seleccionados.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    Cargar()
                        '    Listar.Invoke()
                        'End If
                    End If
                End If
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub GridView_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView.RowStyle

        Try

            If e.RowHandle >= 0 Then

                Try

                    GridView.OptionsBehavior.Editable = False
                    GridView.OptionsSelection.EnableAppearanceFocusedCell = False

                    GridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

                    GridView.OptionsSelection.EnableAppearanceFocusedRow = True
                    GridView.OptionsSelection.EnableAppearanceHideSelection = True
                    GridView.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
                    GridView.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

                    GridView.Appearance.FocusedRow.ForeColor = Color.White
                    GridView.Appearance.SelectedRow.ForeColor = Color.White

                    GridView.Appearance.SelectedRow.Options.UseBackColor = True
                    GridView.Appearance.SelectedRow.Options.UseForeColor = True

                Catch ex As Exception
                    XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Try

                Dim View As GridView = sender

                Dim max As Double = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Maximo"))
                Dim dis As Double = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Cantidad"))
                Dim cant As Double = View.GetRowCellDisplayText(e.RowHandle, View.Columns("CantBodega"))

                Dim val As Double = Math.Round(max - dis, 2)

                If Math.Round(cant, 2) < val Then
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                    'GridView.Appearance.SelectedRow.BackColor = Color.Salmon
                    'GridView.Appearance.SelectedRow.BackColor2 = Color.SeaShell
                Else
                    'e.Appearance.BackColor = Color.Green
                    e.Appearance.BackColor = ColorTranslator.FromHtml("#63C76B")
                    'e.Appearance.BackColor2 = Color.LightGreen
                    e.Appearance.BackColor2 = Color.Transparent
                    'GridView.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#63C76B")
                    'GridView.Appearance.SelectedRow.BackColor2 = Color.Transparent
                    'e.Appearance.BackColor2 = ColorTranslator.FromHtml("#DBF4DD")
                End If

                GridView.OptionsSelection.EnableAppearanceFocusedCell = False
                GridView.OptionsSelection.EnableAppearanceFocusedRow = False
                'GridView.Appearance.FocusedRow.BackColor = Color.FromArgb(255, 255, 192)
                'GridView.Appearance.SelectedRow.BackColor = Color.FromArgb(255, 255, 192)
                GridView.Appearance.SelectedRow.Options.UseBackColor = False

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

    Private Sub GridView_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GridView.ShowingEditor

        If GridView.FocusedColumn.FieldName = "Seleccionar" Then
            e.Cancel = False
        End If

    End Sub
End Class