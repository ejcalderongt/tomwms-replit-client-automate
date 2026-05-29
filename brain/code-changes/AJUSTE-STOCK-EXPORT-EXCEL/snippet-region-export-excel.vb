    ' Construye un DataTable con solo las columnas utiles (omite imagenes,
    ' Ids internos y campos auxiliares) y lo exporta sin formato decorativo.
    ' Usa GridControl in-memory de DevExpress (mismo patron que
    ' frmPicking_List.Exportar_Grid_A_Excel) para garantizar XLSX nativo.
    ' Fallback CSV UTF-8 BOM con separador ';' para compatibilidad Excel ES.

    Private Sub mnuExportarExcelLimpio_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuExportarExcelLimpio.ItemClick

        Try

            If dgrid Is Nothing OrElse dgrid.Rows Is Nothing OrElse dgrid.Rows.Count = 0 Then

                XtraMessageBox.Show("No hay registros en la grilla para exportar.",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)

                Return

            End If

            Dim dt As DataTable = Construir_DataTable_Limpio_AjusteStock(dgrid)

            Dim nombreSugerido As String = "AjusteStock_" &
                pBeTransAjustEnc.IdAjusteenc.ToString() & "_" &
                DateTime.Now.ToString("yyyyMMdd_HHmm")

            Using sfd As New SaveFileDialog()

                sfd.Filter = "Excel (*.xlsx)|*.xlsx|CSV UTF-8 (*.csv)|*.csv"
                sfd.FilterIndex = 1
                sfd.FileName = nombreSugerido
                sfd.RestoreDirectory = True

                If sfd.ShowDialog() <> DialogResult.OK Then Return

                Dim ext As String = Path.GetExtension(sfd.FileName)

                If String.Equals(ext, ".xlsx", StringComparison.OrdinalIgnoreCase) Then
                    Exportar_DataTable_A_Xlsx(dt, sfd.FileName)
                Else
                    Exportar_DataTable_A_Csv(dt, sfd.FileName)
                End If

                XtraMessageBox.Show("Exportacion completada:" & vbCrLf & sfd.FileName,
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)

            End Using

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)

            clsLnLog_error_wms.Agregar_Error("mnuExportarExcelLimpio_ItemClick: " & ex.Message)

        End Try

    End Sub

    Private Function Construir_DataTable_Limpio_AjusteStock(dGrid As DataGridView) As DataTable

        Dim dt As New DataTable("AjusteStockDet")

        ' Orden y nombres de columnas pensados para reporte limpio.
        ' Omitidos a proposito:
        '   ColDiferencia (es DataGridViewImageColumn, sin valor util)
        '   ColIdAjusteDEt (id interno, opcional al final)
        '   colIdProductoTallaColor (id interno, sin valor para usuario)
        dt.Columns.Add("Codigo Producto", GetType(String))
        dt.Columns.Add("Nombre Producto", GetType(String))
        dt.Columns.Add("Bodega", GetType(String))
        dt.Columns.Add("Ubicacion", GetType(String))
        dt.Columns.Add("Lic Plate", GetType(String))
        dt.Columns.Add("Lote", GetType(String))
        dt.Columns.Add("Lote Original", GetType(String))
        dt.Columns.Add("Talla", GetType(String))
        dt.Columns.Add("Color", GetType(String))
        dt.Columns.Add("Presentacion", GetType(String))
        dt.Columns.Add("UM Base", GetType(String))
        dt.Columns.Add("Tipo Ajuste", GetType(String))
        dt.Columns.Add("Motivo Ajuste", GetType(String))
        dt.Columns.Add("Cant. Sistema", GetType(Decimal))
        dt.Columns.Add("Cant. Fisica", GetType(Decimal))
        dt.Columns.Add("Diferencia", GetType(Decimal))
        dt.Columns.Add("Proveedor", GetType(String))
        dt.Columns.Add("Observacion", GetType(String))
        dt.Columns.Add("Enviado a ERP", GetType(Boolean))
        dt.Columns.Add("Id Ajuste Det", GetType(Long))

        For Each row As DataGridViewRow In dGrid.Rows

            If row.IsNewRow Then Continue For

            Dim r As DataRow = dt.NewRow()

            r("Codigo Producto") = SafeStr_AS(row, "ColCodigoProducto")
            r("Nombre Producto") = SafeStr_AS(row, "colNombreProducto")
            r("Bodega") = SafeFmt_AS(row, "ColBodega")
            r("Ubicacion") = SafeStr_AS(row, "colUbicacion")
            r("Lic Plate") = SafeStr_AS(row, "ColLicPlate")
            r("Lote") = SafeStr_AS(row, "colLote")
            r("Lote Original") = SafeStr_AS(row, "LoteOrig")
            r("Talla") = SafeFmt_AS(row, "colTalla")
            r("Color") = SafeFmt_AS(row, "colColor")
            r("Presentacion") = SafeStr_AS(row, "colPresentacion")
            r("UM Base") = SafeStr_AS(row, "UmBas")
            r("Tipo Ajuste") = SafeFmt_AS(row, "tipoajuste")
            r("Motivo Ajuste") = SafeFmt_AS(row, "motivoajuste")

            Dim cantSistema As Decimal = SafeDec_AS(row, "CantidadP")
            Dim cantFisica As Decimal = SafeDec_AS(row, "ColCantidad")
            r("Cant. Sistema") = cantSistema
            r("Cant. Fisica") = cantFisica
            r("Diferencia") = cantFisica - cantSistema

            r("Proveedor") = SafeStr_AS(row, "colProveedor")
            r("Observacion") = SafeStr_AS(row, "ColObservacion")
            r("Enviado a ERP") = SafeBool_AS(row, "ColEnviadoAErp")
            r("Id Ajuste Det") = SafeLong_AS(row, "ColIdAjusteDEt")

            dt.Rows.Add(r)

        Next

        Return dt

    End Function

    Private Function SafeStr_AS(row As DataGridViewRow, colName As String) As String

        Try
            If Not row.DataGridView.Columns.Contains(colName) Then Return ""
            Dim cell = row.Cells(colName)
            If cell Is Nothing OrElse cell.Value Is Nothing OrElse cell.Value Is DBNull.Value Then Return ""
            Return cell.Value.ToString().Trim()
        Catch
            Return ""
        End Try

    End Function

    Private Function SafeFmt_AS(row As DataGridViewRow, colName As String) As String

        Try
            If Not row.DataGridView.Columns.Contains(colName) Then Return ""
            Dim cell = row.Cells(colName)
            If cell Is Nothing Then Return ""
            If cell.FormattedValue IsNot Nothing Then Return cell.FormattedValue.ToString().Trim()
        Catch
        End Try

        Return SafeStr_AS(row, colName)

    End Function

    Private Function SafeDec_AS(row As DataGridViewRow, colName As String) As Decimal

        Dim s As String = SafeStr_AS(row, colName)
        If s.Length = 0 Then Return 0D

        Dim d As Decimal
        If Decimal.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, d) Then Return d
        If Decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, d) Then Return d
        Return 0D

    End Function

    Private Function SafeLong_AS(row As DataGridViewRow, colName As String) As Long

        Dim s As String = SafeStr_AS(row, colName)
        If s.Length = 0 Then Return 0L

        Dim l As Long
        If Long.TryParse(s, l) Then Return l
        Return 0L

    End Function

    Private Function SafeBool_AS(row As DataGridViewRow, colName As String) As Boolean

        Try
            If Not row.DataGridView.Columns.Contains(colName) Then Return False
            Dim cell = row.Cells(colName)
            If cell Is Nothing OrElse cell.Value Is Nothing OrElse cell.Value Is DBNull.Value Then Return False
            Dim v = cell.Value
            If TypeOf v Is Boolean Then Return CBool(v)
            Dim s As String = v.ToString().Trim().ToLowerInvariant()
            Return s = "1" OrElse s = "true" OrElse s = "verdadero" OrElse s = "si" OrElse s = "sí"
        Catch
            Return False
        End Try

    End Function

    Private Sub Exportar_DataTable_A_Xlsx(dt As DataTable, filePath As String)

        ' GridControl in-memory: aprovecha la lib DevExpress ya referenciada
        ' (DevExpress.XtraGrid.v24.2). Genera xlsx limpio con headers + datos,
        ' sin estilos del DataGridView de pantalla.
        Using gc As New DevExpress.XtraGrid.GridControl()

            Dim gv As New DevExpress.XtraGrid.Views.Grid.GridView()
            gc.MainView = gv
            gc.ViewCollection.Add(gv)
            gc.DataSource = dt
            gv.PopulateColumns()

            ' Forzar el orden definido en el DataTable
            For i As Integer = 0 To dt.Columns.Count - 1
                Dim col = gv.Columns.ColumnByFieldName(dt.Columns(i).ColumnName)
                If col IsNot Nothing Then
                    col.VisibleIndex = i
                    col.Caption = dt.Columns(i).ColumnName
                End If
            Next

            gv.OptionsView.ShowGroupPanel = False
            gv.BestFitColumns()

            gc.ExportToXlsx(filePath)

        End Using

    End Sub

    Private Sub Exportar_DataTable_A_Csv(dt As DataTable, filePath As String)

        Const sep As String = ";"
        Dim sb As New StringBuilder()

        Dim headers(dt.Columns.Count - 1) As String
        For i As Integer = 0 To dt.Columns.Count - 1
            headers(i) = EscaparCsv_AS(dt.Columns(i).ColumnName, sep)
        Next
        sb.AppendLine(String.Join(sep, headers))

        For Each row As DataRow In dt.Rows
            Dim vals(dt.Columns.Count - 1) As String
            For i As Integer = 0 To dt.Columns.Count - 1
                Dim v = row(i)
                Dim s As String
                If v Is Nothing OrElse v Is DBNull.Value Then
                    s = ""
                ElseIf TypeOf v Is Decimal OrElse TypeOf v Is Double OrElse TypeOf v Is Single Then
                    s = Convert.ToDecimal(v).ToString("0.####", CultureInfo.CurrentCulture)
                ElseIf TypeOf v Is Boolean Then
                    s = If(CBool(v), "Si", "No")
                Else
                    s = v.ToString()
                End If
                vals(i) = EscaparCsv_AS(s, sep)
            Next
            sb.AppendLine(String.Join(sep, vals))
        Next

        ' UTF-8 con BOM para que Excel ES detecte acentos correctamente.
        File.WriteAllText(filePath, sb.ToString(), New UTF8Encoding(True))

    End Sub

    Private Function EscaparCsv_AS(s As String, sep As String) As String

        If s Is Nothing Then Return ""

        If s.Contains(sep) OrElse s.Contains("""") OrElse s.Contains(vbCr) OrElse s.Contains(vbLf) Then
            Return """" & s.Replace("""", """""") & """"
        End If

        Return s

    End Function

#End Region

End Class