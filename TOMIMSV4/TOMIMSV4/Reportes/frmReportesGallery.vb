Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmReportesGallery

    Sub New()
        InitializeComponent()
    End Sub

    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Private Sub frmReportesGallery_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Get_Lista_Reportes_Disponibles()
    End Sub

    Private Sub Get_Lista_Reportes_Disponibles()

        Try

            Dim DT As New DataTable
            DT = clsLnMenu_sistema.Get_All_For_Menu(AP.UsuarioAp.IdRol)
            dgrid.DataSource = DT

            GridView1.Columns("IDMENU").SummaryItem.SummaryType = SummaryItemType.Count
            GridView1.Columns("IDMENU").SummaryItem.DisplayFormat = "REGS = {0:n6}"
            GridView1.Columns("NOMBRE_LGCO").Visible = False
            GridView1.Columns("NIVEL").Visible = False
            GridView1.Columns("PADRE").Visible = False
            GridView1.Columns("VISIBLE").Visible = False
            GridView1.Columns("IDMODULO").Visible = False
            GridView1.Columns("TIENE_HIJOS").Visible = False

            GridView1.BestFitColumns(True)
            GridView1.Columns("TITULO").Width = 400

            Try
                GridView1.ClearSorting()
                GridView1.Columns("IDMENU").SortOrder = ColumnSortOrder.Ascending
            Finally

            End Try

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Get_Lista_Reportes_Disponibles()
    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView1.RowCellStyle

        Try

            Dim view As GridView = TryCast(sender, GridView)
            Dim _mark As Boolean = view.GetRowCellValue(e.RowHandle, "TIENE_HIJOS")

            If e.Column.FieldName = "TITULO" Then
                e.Appearance.ForeColor = If(_mark, Color.Black, Color.DarkGreen)
            End If

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles dgrid.DoubleClick
        Procesar_Registro()
    End Sub

    Private Sub Procesar_Registro()

        Try

            If (GridView1.RowCount > 0) Then

                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim vIdMenuSistema As String = Dr.Item("IDMENU")
                Dim vNombreLogico As String = Dr.Item("NOMBRE_LGCO")
                Dim vTieneHijos As String = Dr.Item("TIENE_HIJOS")

                If Not vTieneHijos Then

                    If vNombreLogico.StartsWith("tsmiPedidos") Then


                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

End Class


