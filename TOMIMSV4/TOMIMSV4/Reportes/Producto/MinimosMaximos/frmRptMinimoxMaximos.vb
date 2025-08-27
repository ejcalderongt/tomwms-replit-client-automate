Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmRptMinimoxMaximos

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private cantidad As Double = 0

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs)
        Listar_Productos_Sin_Minimos_Y_Maximos()
    End Sub

    Private Sub Listar_Productos_Sin_Minimos_Y_Maximos()

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormCaption("Calculando datos...")

        Try

            Dgrid.DataSource = Nothing

            Dim lista As New List(Of clsBeVW_stock_res)

            lista = clsLnStock.GetRptProductsMinMax(0).ToList

            If lista IsNot Nothing AndAlso lista.Count > 0 Then

                Dim ValorComparativo As Double = 0
                Dim ValorPorcentual As Double = 0
                Dim a, b, c As Double
                Dim ReglaDeTres As Double = 0

                Dim DT As New DataTable("Productos")
                DT.Columns.Add("Propietario", GetType(String))
                DT.Columns.Add("UM", GetType(String))
                DT.Columns.Add("Presentación", GetType(String))
                DT.Columns.Add("Código", GetType(String))
                DT.Columns.Add("Barra", GetType(String))
                DT.Columns.Add("Serie", GetType(String))
                DT.Columns.Add("Nombre", GetType(String))
                DT.Columns.Add("Lote", GetType(String))
                DT.Columns.Add("Disp_UM_Bas", GetType(Double))
                DT.Columns.Add("Disp_Presentación", GetType(Double))
                DT.Columns.Add("Vence", GetType(Date))
                DT.Columns.Add("Estado", GetType(String))
                DT.Columns.Add("Ubicación", GetType(Integer))
                DT.Columns.Add("Min", GetType(Double))
                DT.Columns.Add("Max", GetType(Double))
                DT.Columns.Add("Rate", GetType(Double))
                DT.Columns.Add("%", GetType(Double))

                For Each Obj As clsBeVW_stock_res In lista

                    If (Obj.Existencia_min_umbas = 0) Then
                        ValorComparativo = 2
                    ElseIf (Obj.CantidadUmBas > Obj.Existencia_min_umbas) AndAlso (Obj.CantidadUmBas <= Obj.Existencia_max_umbas) Then
                        ValorComparativo = 0
                    ElseIf (Obj.CantidadUmBas <= Obj.Existencia_min_umbas) Then 'Está debajo o en el límite mínimo, a la baja.
                        ValorComparativo = -1
                    ElseIf (Obj.CantidadUmBas >= Obj.Existencia_max_umbas) Then 'Al borde del alza
                        ValorComparativo = 1
                    ElseIf Obj.Existencia_max_umbas = 0 Then 'Al borde del alza
                        ValorComparativo = 2
                    End If

                    b = 100
                    a = Obj.Existencia_min_umbas
                    c = Obj.CantidadUmBas

                    If a > 0 Then
                        ReglaDeTres = Math.Round((((b * c) / a) / 100), 2)
                        ValorPorcentual = ReglaDeTres
                    Else
                        ValorPorcentual = 0
                    End If


                    Dgrid.DataSource = DT

                    DT.Rows.Add(Obj.Propietario,
                                Obj.UMBas,
                                Obj.Nombre_Presentacion,
                                Obj.Codigo_Producto,
                                Obj.Codigo_Barra,
                                Obj.Serial,
                                Obj.Nombre_Producto,
                                Obj.Lote,
                                Obj.CantidadUmBas,
                                Obj.CantidadPresentacion,
                                Obj.Fecha_Vence,
                                Obj.NomEstado,
                                Obj.IdUbicacion,
                                Obj.Existencia_min_umbas,
                                Obj.Existencia_max_umbas,
                                ValorComparativo,
                                ValorPorcentual)

                    cantidad += Obj.CantidadPresentacion

                    Application.DoEvents()

                Next

                lblReg.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                lblCant.Caption = String.Format("Cantidad: {0}", cantidad)

                GridView1.Columns(0).GroupIndex = 0
                GridView1.OptionsBehavior.AutoExpandAllGroups = True
                GridView1.OptionsView.ShowFooter = True

                GridView1.Columns("Disp_Presentación").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Disp_Presentación").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Disp_Presentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Disp_Presentación").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Disp_UM_Bas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Disp_UM_Bas").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Disp_UM_Bas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Disp_UM_Bas").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Min").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Min").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Max").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Max").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("%").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("%").DisplayFormat.FormatString = "P"

                Dim textEdit As New RepositoryItemTextEdit()
                textEdit.ContextImageOptions.Image = My.Resources.yellow_ball
                GridView1.Columns("Rate").ColumnEdit = textEdit
                Dgrid.RepositoryItems.Add(textEdit)

                'GridView1.Columns("Rate").Visible = False

                GridView1.BestFitColumns()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm()
        End Try

    End Sub

    Private Sub txtIdProducto_KeyPress(sender As Object, e As KeyPressEventArgs)
        Try
            If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
                e.Handled = True
            End If
            If e.KeyChar = "." Then
                e.Handled = True
            End If
            If Char.IsDigit(e.KeyChar) Then
                e.Handled = False
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    Private Sub frmRptMinimoxMaximos_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try
            Listar_Productos_Sin_Minimos_Y_Maximos()
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle

        Try

            If e.Column.FieldName = "Rate" Then

                Dim view As ColumnView = CType(sender, ColumnView)
                'Dim CantidadDisponible As Double = Val(view.GetRowCellValue(e.RowHandle, "Disp_UM_Bas").ToString())
                ''Dim vCantPres As Double = Val(view.GetRowCellValue(e.RowHandle, "Cantidad_Presentacion").ToString())
                'Dim Min As Double = Val(view.GetListSourceRowCellValue(e.RowHandle, "Min").ToString())
                'Dim Max As Double = Val(view.GetListSourceRowCellValue(e.RowHandle, "Max").ToString())
                Dim Max As Double = Val(view.GetListSourceRowCellValue(e.RowHandle, "Rate").ToString())
                Dim Rate As Double = 0
                'Dim ValorComparativoMinimo As Double = -1
                'Dim ValorComparativoMaximo As Double = 1

                Dim gridFormatRule As New GridFormatRule()

                Dim formatConditionRuleIconSet As New FormatConditionRuleIconSet()
                formatConditionRuleIconSet.IconSet = New FormatConditionIconSet()

                Dim iconSet As FormatConditionIconSet = formatConditionRuleIconSet.IconSet

                Dim icon1 As New FormatConditionIconSetIcon()
                Dim icon2 As New FormatConditionIconSetIcon()
                Dim icon3 As New FormatConditionIconSetIcon()
                Dim icon4 As New FormatConditionIconSetIcon()

                'Choose predefined icons. 
                icon1.Icon = My.Resources.green_ball '"Stars3_1.png"
                icon2.Icon = My.Resources.knob_blue '"Stars3_2.png"
                icon3.Icon = My.Resources.red_ball '"Stars3_3.png"
                icon4.Icon = My.Resources.yellow_ball '"Stars3_3.png"


                'If (CantidadDisponible > Min) AndAlso (CantidadDisponible <= Max) Then
                '    ValorComparativoStatusQuo = 0
                'ElseIf (CantidadDisponible <= Min) Then 'Está debajo o en el límite mínimo, a la baja.
                '    ValorComparativoMinimo = -1
                'ElseIf (CantidadDisponible >= Max) Then 'Al borde del alza
                '    ValorComparativoMaximo = 1
                'End If

                'Specify the type of threshold values. 
                iconSet.ValueType = FormatConditionValueType.Number

                'Define ranges to which icons are applied by setting threshold values. 
                icon1.Value = 0 ' target range: 67% <= value 
                icon1.ValueComparison = FormatConditionComparisonType.GreaterOrEqual

                icon2.Value = 1 ' target range: 33% <= value < 67% 
                icon2.ValueComparison = FormatConditionComparisonType.GreaterOrEqual

                icon3.Value = -1 ' target range: 0% <= value < 33% 
                icon3.ValueComparison = FormatConditionComparisonType.GreaterOrEqual

                icon4.Value = 2 ' target range: 0% <= value < 33% 
                icon4.ValueComparison = FormatConditionComparisonType.GreaterOrEqual

                'Add icons to the icon set. 
                iconSet.Icons.Add(icon1)
                iconSet.Icons.Add(icon2)
                iconSet.Icons.Add(icon3)
                iconSet.Icons.Add(icon4)

                'Specify the rule type. 
                gridFormatRule.Rule = formatConditionRuleIconSet
                'Specify the column to which formatting is applied. 
                gridFormatRule.Column = GridView1.Columns("Rate")
                'Add the formatting rule to the GridView. 
                GridView1.FormatRules.Add(gridFormatRule)

            End If


        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try

    End Sub

End Class