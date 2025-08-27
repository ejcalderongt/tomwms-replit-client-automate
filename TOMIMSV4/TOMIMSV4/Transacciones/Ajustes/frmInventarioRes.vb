Imports DevExpress.XtraPivotGrid

Public Class frmInventarioRes
    Public Property FechaInicio As Date
    Public Property FechaHasta As Date

    Private Sub Listar_Inventarios_Rango_Fechas()

        Try

            Dim dtAjustes = clsLnTrans_ajuste_enc.ObtenerAjustesInventario(FechaInicio, FechaHasta)

            With cmbInventario.Properties
                .DataSource = dtAjustes
                .DisplayMember = "NoInventario"
                .ValueMember = "Correlativo"
                .NullText = "Seleccione un inventario"
                .PopupFormSize = New Size(500, 300)
            End With

        Catch ex As Exception

        End Try

    End Sub

    Private Sub frmInventarioRes_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Listar_Inventarios_Rango_Fechas()
    End Sub

    Private Sub cmbInventario_EditValueChanged(sender As Object, e As EventArgs) Handles cmbInventario.EditValueChanged
        ' Validar que se haya seleccionado un valor
        If cmbInventario.EditValue Is Nothing Then Return

        Dim idInventarioEnc As Integer
        If Integer.TryParse(cmbInventario.EditValue.ToString(), idInventarioEnc) Then
            Dim dtAjustes As DataTable = clsLnTrans_ajuste_enc.ObtenerPivotAjustes(idInventarioEnc)

            With PivotGridControl1
                .DataSource = dtAjustes
                .Fields.Clear()

                ' Fila: código y nombre del producto
                Dim fCodigo = .Fields.Add("codigo_producto", PivotArea.RowArea)
                fCodigo.Caption = "Código"
                fCodigo.Width = 130

                Dim fProducto = .Fields.Add("nombre_producto", PivotArea.RowArea)
                fProducto.Caption = "Producto"
                fProducto.Width = 250

                ' Datos simples por sistema
                .Fields.Add("cantidad_originalWMS", PivotArea.DataArea).Caption = "WMS Original"
                .Fields.Add("cantidad_originalSAP", PivotArea.DataArea).Caption = "SAP Original"
                .Fields.Add("diferencia_original", PivotArea.DataArea).Caption = "Dif. Original"

                .Fields.Add("cantidad_nuevaWMS", PivotArea.DataArea).Caption = "WMS Nueva"
                .Fields.Add("cantidad_nuevaSAP", PivotArea.DataArea).Caption = "SAP Nueva"
                .Fields.Add("diferencia_nueva", PivotArea.DataArea).Caption = "Dif. Nueva"

                ' Formato
                For Each f In .Fields
                    If f.Area = PivotArea.DataArea Then
                        f.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        f.CellFormat.FormatString = "n2"
                    End If
                Next

                .OptionsView.ShowColumnGrandTotals = True
                .OptionsView.ShowRowGrandTotals = True
                .OptionsView.ShowFilterHeaders = False
                .BestFit()
            End With
        End If


    End Sub

    Private Sub ExportarPivotAExcel()
        Try
            Dim sfd As New SaveFileDialog()
            sfd.Title = "Exportar Pivot a Excel"
            sfd.Filter = "Excel Files (*.xlsx)|*.xlsx"
            sfd.FileName = "AjustesInventario_" & DateTime.Now.ToString("yyyyMMdd_HHmm") & ".xlsx"

            If sfd.ShowDialog() = DialogResult.OK Then
                PivotGridControl1.ExportToXlsx(sfd.FileName)

                ' Preguntar si desea abrir el archivo
                If MessageBox.Show("Exportación exitosa. ¿Desea abrir el archivo?", "Exportar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Process.Start(sfd.FileName)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Ocurrió un error al exportar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        ExportarPivotAExcel()
    End Sub

End Class