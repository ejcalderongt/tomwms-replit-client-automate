Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmListStockSeek

    Public ListRegistros As New List(Of clsBeVW_stock_res)

    Dim a As clsLnVW_Despacho_Rep
    Private DT As New DataTable("StockReservado")

    Public Property rpCodigoProducto As String
    Public Property rpLote As String = ""
    Public Property rpFechaVence = "01/01/1900"
    Public Property IdBodega As Integer = 0

    Public Property Diferencia As Double = 0

    Public Property Modo As pModo
    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub SetDatataTable()

        DT.Columns.Add("Código", GetType(String))
        DT.Columns.Add("Producto", GetType(String))
        DT.Columns.Add("UMBas", GetType(String))
        DT.Columns.Add("Presentación", GetType(String))
        DT.Columns.Add("EstadoProducto", GetType(String))
        DT.Columns.Add("Lote", GetType(String))
        DT.Columns.Add("FechaVence", GetType(Date))
        DT.Columns.Add("Cantidad", GetType(Double))
        DT.Columns.Add("IdUbicacion", GetType(Integer))
        DT.Columns.Add("IdStock", GetType(Integer))
        DT.Columns.Add("FechaIngreso", GetType(Date))
        DT.Columns.Add("IdProductoBodega", GetType(Integer))
        DT.Columns.Add("NomUbic", GetType(String))

    End Sub

    Private Sub Cargar_Datos()

        Try

            ListRegistros.Clear() : DT.Clear() : grdStockRes.DataSource = Nothing

            ListRegistros = clsLnVW_stock_res.Get_Lista_Stock(rpCodigoProducto, rpLote, rpFechaVence, IdBodega)

            If ListRegistros.Count > 0 Then

                For Each obj As clsBeVW_stock_res In ListRegistros

                    DT.Rows.Add(obj.Codigo_Producto,
                                obj.Nombre_Producto,
                                obj.UMBas,
                                obj.Nombre_Presentacion,
                                obj.NomEstado,
                                obj.Lote,
                                obj.Fecha_Vence,
                                obj.CantidadUmBas,
                                obj.IdUbicacion,
                                obj.IdStock,
                                obj.Fecha_ingreso,
                                obj.IdProductoBodega,
                                obj.Ubicacion_Nombre)

                Next

                grdStockRes.DataSource = DT

                If GridView1.RowCount > 0 Then

                    GridView1.OptionsView.ShowFooter = True
                    GridView1.OptionsView.ColumnAutoWidth = False
                    GridView1.BestFitColumns(True)

                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                    GridView1.Columns("Cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cantidad").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("Cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"

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

    Private Sub frmListStockSeek_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetDatataTable()
        Cargar_Datos()
    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle

        Try

            GridView1.OptionsBehavior.Editable = False
            GridView1.OptionsSelection.EnableAppearanceFocusedCell = False

            GridView1.FocusRectStyle = DrawFocusRectStyle.RowFocus

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

    Private Sub grdStockRes_DoubleClick(sender As Object, e As EventArgs) Handles grdStockRes.DoubleClick

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim Obj As New clsBeVW_stock_res
                Obj.IdStock = Dr.Item("IdStock")
                clsLnVW_stock_res.GetSingle(Obj)

                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                With frmStockSeek
                    .Modo = frmStockReservado.TipoTrans.Editar
                    .CantidadDiferencia = Diferencia
                    .pBeStock = Obj
                    .ShowDialog()
                    .Focus()
                End With

                GridView1.FocusedRowHandle = lSelectionIndex

                Cargar_Datos()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar_Datos()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

End Class