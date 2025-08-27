Imports DevExpress.XtraEditors

Public Class frmRptProximosVencimiento

    Public Property Modo As pModo

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private cantidad As Double = 0

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub listarReporte()

        Try

            Dgrid.DataSource = Nothing
            'Dgrid.DataSource = clsLnStock.getRptProductosProximosVencimientoDT(0)
            Dim IdBodega As Integer = Integer.Parse(IIf(cmbBodega.EditValue IsNot Nothing, cmbBodega.EditValue, 0))
            Dim IdPropietarioBodega As Integer = Integer.Parse(IIf(cmbPropietarioBodega.EditValue IsNot Nothing, cmbPropietarioBodega.EditValue, 0))
            Dim lista As New List(Of clsBeVW_stock_res)
            Dim rango As Integer = IIf(TrackBarControl1.Value > 0, TrackBarControl1.Value, 0)

            lista = clsLnStock.getRptProductosProximosVencimiento_By_Bodega_And_PropietarioBodega(0, IdBodega, IdPropietarioBodega, rango)

            If lista IsNot Nothing AndAlso lista.Count > 0 Then

                Dim DT As New DataTable("Productos")
                DT.Columns.Add("Propietario", GetType(String))
                DT.Columns.Add("UM", GetType(String))
                DT.Columns.Add("Presentación", GetType(String))
                DT.Columns.Add("Código", GetType(String))
                DT.Columns.Add("Barra", GetType(String))
                DT.Columns.Add("Nombre", GetType(String))
                DT.Columns.Add("Lote", GetType(String))
                DT.Columns.Add("Serie", GetType(String))
                DT.Columns.Add("Cant. Presentación", GetType(Double))
                DT.Columns.Add("Cant U.M Bas", GetType(Double))
                DT.Columns.Add("Ubicación", GetType(String))
                DT.Columns.Add("Estado", GetType(String))
                DT.Columns.Add("Vence", GetType(Date))
                DT.Columns.Add("Tolerancia", GetType(Integer))

                For Each Obj As clsBeVW_stock_res In lista
                    DT.Rows.Add(Obj.Propietario,
                        Obj.UMBas,
                        Obj.Nombre_Presentacion,
                        Obj.Codigo_Producto,
                        Obj.Codigo_Barra,
                        Obj.Nombre_Producto,
                        Obj.Lote,
                        Obj.Serial,
                        Obj.CantidadPresentacion,
                        Obj.CantidadUmBas,
                        Obj.Ubicacion_Nombre,
                        Obj.NomEstado,
                        Obj.Fecha_Vence,
                        Obj.Tolerancia)
                    cantidad += Obj.CantidadPresentacion
                Next

                Dgrid.DataSource = DT
                lblReg.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                lblCant.Caption = String.Format("Cantidad: {0}", cantidad)

                GridView1.Columns(0).GroupIndex = 0
                GridView1.OptionsBehavior.AutoExpandAllGroups = True

                If GridView1.RowCount > 0 Then

                    GridView1.OptionsView.ShowFooter = True

                    GridView1.Columns("Cant U.M Bas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cant U.M Bas").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Cant U.M Bas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cant U.M Bas").SummaryItem.DisplayFormat = "{0:n6}"

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

    Private Sub frmRptProximosVencimiento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DiasTol As Integer = 0

            DiasTol = clsLnProducto.MaxTolerancia

            TrackBarControl1.Value = 0

            If DiasTol > 0 Then
                TrackBarControl1.Properties.Maximum = DiasTol
                TrackBarControl1.Value = 1
            End If

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, cmbBodega.EditValue)

            listarReporte()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

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

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, cmbBodega.EditValue)
        listarReporte()

    End Sub

    Private Sub TrackBarControl1_EditValueChanged(sender As Object, e As EventArgs) Handles TrackBarControl1.EditValueChanged
        listarReporte()
    End Sub
End Class