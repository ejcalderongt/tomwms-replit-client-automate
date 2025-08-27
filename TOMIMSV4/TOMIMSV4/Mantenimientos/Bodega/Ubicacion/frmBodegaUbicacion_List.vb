Imports DevExpress.XtraEditors

Public Class frmBodegaUbicacion_List

    Public pIdBodega As Integer
    Public pUbicacionPicking As Boolean
    Public pUbicacionRecepcion As Boolean
    Public pUbicacionesTodas As Boolean = False
    Public pObj As clsBeBodega_ubicacion
    Public IdInventario As Integer = 0

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
        Inventario = 3
    End Enum

    Private Sub frmBodegaUbicacion_List_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListarUbicaciones()
    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkActivos.CheckedChanged
        ListarUbicaciones()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub mmuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mmuActualizar.ItemClick
        mmuActualizar.Enabled = False
        ListarUbicaciones()
        mmuActualizar.Enabled = True
    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow

                If Modo = pModo.Seleccion Then

                    If pUbicacionPicking Then
                        pObj = clsLnBodega_ubicacion.GetSingle(Dr.Item("Código"), AP.IdBodega, "ubicacion_picking", False)
                    ElseIf pUbicacionRecepcion Then
                        pObj = clsLnBodega_ubicacion.GetSingle(Dr.Item("Código"), AP.IdBodega, "ubicacion_recepcion", False)

                    Else
                        pObj = clsLnBodega_ubicacion.GetSingle(Dr.Item("Código"), AP.IdBodega)
                    End If


                    If (pUbicacionesTodas) Then
                        pObj = clsLnBodega_ubicacion.GetSingle(Dr.Item("Código"), AP.IdBodega, "ubicaciones", False)
                    End If

                    Hide()

                ElseIf Modo = pModo.Inventario Then
                    pObj = clsLnBodega_ubicacion.GetSingle(Dr.Item("Código"), AP.IdBodega)
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

    Private Sub ListarUbicaciones()

        Dgrid.DataSource = Nothing

        Try

            Dim lista As New List(Of clsBeBodega_ubicacion)

            If Modo = pModo.Inventario Then

                lista = clsLnBodega_ubicacion.Get_All_By_Bodega_By_IdInventarioEnc(IdInventario)

            Else

                If pUbicacionesTodas Then
                    lista = clsLnBodega_ubicacion.Get_All_By_IdBodega(chkActivos.Checked, pIdBodega, "")
                Else
                    If pUbicacionPicking Then
                        lista = clsLnBodega_ubicacion.Get_Ubicaciones_Picking_By_IdBodega(pIdBodega)
                    Else
                        lista = clsLnBodega_ubicacion.Get_All_By_IdBodega(chkActivos.Checked, pIdBodega, "ubicacion_recepcion")
                    End If
                End If

            End If

            If lista.Count > 0 Then

                Dim DT As New DataTable("BodegaUbicacion")
                DT.Columns.Add("Código", GetType(Integer))
                DT.Columns.Add("Descripción", GetType(String))

                For Each obj As clsBeBodega_ubicacion In lista
                    DT.Rows.Add(obj.IdUbicacion, obj.Descripcion)
                Next

                Dgrid.DataSource = DT

                If GridView1.RowCount > 0 Then

                    Try
                        lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                    Catch ex As Exception
                        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End Try
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

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        cmdImprimir.Enabled = False
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

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Ubicaciones"

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

End Class