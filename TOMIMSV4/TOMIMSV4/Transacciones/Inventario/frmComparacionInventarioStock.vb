Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmComparacionInventarioStock

    Private DT As New DataTable("ComparacionStock")
    Public ListaComparacionStock As New List(Of clsBeTrans_inv_enc)
    Public listar As New List(Of clsBeTrans_inv_enc)
    Public BeInventarioEnc As New clsBeTrans_inv_enc
    Public BeComparacionStock As New clsBeTrans_inv_enc

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Private Sub Set_Datata_Table()

        DT.Columns.Add("Inventario", GetType(Integer))
        DT.Columns.Add("IdProducto", GetType(Integer))
        DT.Columns.Add("Producto", GetType(String))
        DT.Columns.Add("Código", GetType(String))
        DT.Columns.Add("Presentación", GetType(String))
        DT.Columns.Add("Cantidad Conteo", GetType(Double))
        DT.Columns.Add("Cantidad Verificacion", GetType(Double))
        DT.Columns.Add("Cantidad Stock", GetType(Double))
        DT.Columns.Add("Diferencia", GetType(Double))
        DT.Columns.Add("Peso", GetType(Double))

    End Sub

    Private lTrans_inv_enc As New List(Of clsBeTrans_inv_enc)
    Private Sub Calcular_Reporte_Resumen()

        Try

            prg.Visible = True

            Dim Comparacion As New clsBeTrans_inv_enc

            prg.Text = "Creando tabla temporal..."
            prg.Refresh()

            clsLnTrans_inv_enc.Crea_Tabla_Temporal()

            prg.Text = "Consultando inventario..."
            prg.Refresh()

            ListaComparacionStock = clsLnTrans_inv_enc.Get_Inventario_Vrs_Stock_Res(BeInventarioEnc.Idinventarioenc)

            If ListaComparacionStock.Count > 0 Then

                prg.Properties.Minimum = 0
                prg.Properties.Maximum = ListaComparacionStock.Count
                prg.Properties.Step = 1
                prg.Properties.PercentView = True
                prg.Refresh()

                For Each Obj In ListaComparacionStock

                    Comparacion = clsLnTrans_inv_enc.Get_Single_By_Presentacion_TemporalTable(Obj.Idinventarioenc, Obj.IdProducto, Obj.IdPresentacion)

                    Application.DoEvents()

                    lblRegs.Caption = "Rep. Res -> Procesando: " & Obj.Codigo
                    lblRegs.Refresh()

                    If Comparacion Is Nothing Then

                        BeComparacionStock.Idinventarioenc = Obj.Idinventarioenc
                        BeComparacionStock.IdProducto = Obj.IdProducto
                        BeComparacionStock.IdPresentacion = Obj.IdPresentacion
                        BeComparacionStock.Codigo = Obj.Codigo
                        BeComparacionStock.Producto = Obj.Producto
                        BeComparacionStock.Presentacion = Obj.Presentacion
                        BeComparacionStock.Detalle = Obj.Detalle
                        BeComparacionStock.Resumen = Obj.Resumen
                        BeComparacionStock.Stock = Obj.Stock
                        BeComparacionStock.Peso = Obj.Peso
                        BeComparacionStock.IsNew = True
                        lTrans_inv_enc.Add(BeComparacionStock)

                        'clsLnTrans_inv_enc.Insertar_Comparacion_Stock(BeComparacionStock)

                    Else

                        If Obj.Detalle > 0 Then
                            BeComparacionStock.Detalle = Obj.Detalle

                        ElseIf Obj.Resumen > 0 Then
                            BeComparacionStock.Resumen = Obj.Resumen

                        ElseIf Obj.Stock > 0 Then
                            BeComparacionStock.Stock = Obj.Stock

                        ElseIf Obj.Peso > 0 Then
                            BeComparacionStock.Peso = Obj.Peso

                        End If

                        BeComparacionStock.IsNew = False
                        lTrans_inv_enc.Add(BeComparacionStock)

                        'clsLnTrans_inv_enc.Actualizar_Comparacion_Stock(BeComparacionStock)

                    End If

                    prg.PerformStep()

                Next

                Llenar_Grid_Res()

            End If

            lblRegs.Caption = "Regs: 0"

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            prg.Visible = False
        End Try

    End Sub

    Private Sub Calcular_Reporte_Detalle()

        Try

            prg.Visible = True

            Dim DT As New DataTable

            DT = clsLnTrans_inv_enc.Get_Inventario_Vrs_Stock_Det_WMS(BeInventarioEnc.Idinventarioenc,
                                                                     BeInventarioEnc.IdBodega,
                                                                     chkCoincidencias.Checked)

            lblRegs.Caption = "Consultando inventario..."
            prg.Refresh()

            dgridDetalle.DataSource = DT

            If GridView2.Columns.Count > 0 Then

                GridView2.OptionsView.ShowFooter = True

                GridView2.Columns("Inv").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView2.Columns("Inv").DisplayFormat.FormatString = "{0:n6}"

                GridView2.Columns("Inv").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView2.Columns("Inv").SummaryItem.DisplayFormat = "{0:n6}"

                GridView2.Columns("Stock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView2.Columns("Stock").DisplayFormat.FormatString = "{0:n6}"

                GridView2.Columns("Stock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView2.Columns("Stock").SummaryItem.DisplayFormat = "{0:n6}"

                GridView2.Columns("Dif").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView2.Columns("Dif").DisplayFormat.FormatString = "{0:n6}"

                GridView2.Columns("Dif").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView2.Columns("Dif").SummaryItem.DisplayFormat = "{0:n6}"

                GridView2.BestFitColumns(True)

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                Dim gridFormatRule As New GridFormatRule()
                Dim formatConditionRuleExpression As New FormatConditionRuleExpression()
                gridFormatRule.Column = GridView2.Columns("Dif")
                gridFormatRule.ApplyToRow = False
                formatConditionRuleExpression.PredefinedName = "Red Fill, Red Text"
                formatConditionRuleExpression.Expression = "[Dif] < 0"
                gridFormatRule.Rule = formatConditionRuleExpression
                GridView2.FormatRules.Add(gridFormatRule)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            prg.Visible = False
        End Try

    End Sub

    Private Sub Llenar_Grid_Res()

        Try

            Dim vDif As Double = 0.0

            listar = clsLnTrans_inv_enc.GetComparacionStock(BeInventarioEnc.Idinventarioenc)

            DT.Clear()

            For Each Obj In lTrans_inv_enc 'listar

                vDif = (Obj.Stock - Obj.Detalle)

                DT.Rows.Add(Obj.Idinventarioenc, Obj.IdProducto,
                    Obj.Producto, Obj.Codigo, Obj.Presentacion,
                    Obj.Detalle, Obj.Resumen, Obj.Stock, vDif, Obj.Peso)

            Next

            grdStockCompara.DataSource = DT

            If GridView1.Columns.Count > 0 Then

                GridView1.OptionsView.ShowFooter = True

                GridView1.Columns("Cantidad Conteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad Conteo").DisplayFormat.FormatString = "{0:n2}"

                GridView1.Columns("Cantidad Conteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad Conteo").SummaryItem.DisplayFormat = "{0:n2}"

                GridView1.Columns("Cantidad Verificacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad Verificacion").DisplayFormat.FormatString = "{0:n2}"

                GridView1.Columns("Cantidad Verificacion").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad Verificacion").SummaryItem.DisplayFormat = "{0:n2}"

                GridView1.Columns("Cantidad Stock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad Stock").DisplayFormat.FormatString = "{0:n2}"

                GridView1.Columns("Cantidad Stock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad Stock").SummaryItem.DisplayFormat = "{0:n2}"

                GridView1.Columns("Peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Peso").DisplayFormat.FormatString = "{0:n2}"

                GridView1.Columns("Peso").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Peso").SummaryItem.DisplayFormat = "{0:n2}"

                GridView1.Columns("Diferencia").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Diferencia").DisplayFormat.FormatString = "{0:n2}"

                GridView1.Columns("Diferencia").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Diferencia").SummaryItem.DisplayFormat = "{0:n2}"

                GridView1.BestFitColumns(True)

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

            End If

            clsLnTrans_inv_enc.Elimina_Tabla_Temporal_ComparacionStock()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Llenar_Grid_Det()

        Try

            Dim vDif As Double = 0.0

            listar = clsLnTrans_inv_enc.GetComparacionStock(BeInventarioEnc.Idinventarioenc)

            DT.Clear()

            For Each Obj In listar

                vDif = (Obj.Detalle - Obj.Resumen - Obj.Stock)

                If vDif < 0 Then
                    vDif = (vDif * -1)
                End If

                DT.Rows.Add(Obj.Idinventarioenc, Obj.IdProducto,
                    Obj.Producto, Obj.Codigo, Obj.Presentacion,
                    Obj.Detalle, Obj.Resumen, Obj.Stock, vDif, Obj.Peso)

            Next

            dgridDetalle.DataSource = DT

            If GridView2.Columns.Count > 0 Then

                GridView2.OptionsView.ShowFooter = True

                GridView2.Columns("Cantidad Conteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView2.Columns("Cantidad Conteo").DisplayFormat.FormatString = "{0:n2}"

                GridView2.Columns("Cantidad Conteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView2.Columns("Cantidad Conteo").SummaryItem.DisplayFormat = "{0:n2}"

                GridView2.Columns("Cantidad Verificacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView2.Columns("Cantidad Verificacion").DisplayFormat.FormatString = "{0:n2}"

                GridView2.Columns("Cantidad Verificacion").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView2.Columns("Cantidad Verificacion").SummaryItem.DisplayFormat = "{0:n2}"

                GridView2.Columns("Cantidad Stock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView2.Columns("Cantidad Stock").DisplayFormat.FormatString = "{0:n2}"

                GridView2.Columns("Cantidad Stock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView2.Columns("Cantidad Stock").SummaryItem.DisplayFormat = "{0:n2}"

                GridView2.Columns("Peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView2.Columns("Peso").DisplayFormat.FormatString = "{0:n2}"

                GridView2.Columns("Peso").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView2.Columns("Peso").SummaryItem.DisplayFormat = "{0:n2}"

                GridView2.Columns("Diferencia").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView2.Columns("Diferencia").DisplayFormat.FormatString = "{0:n2}"

                GridView2.Columns("Diferencia").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView2.Columns("Diferencia").SummaryItem.DisplayFormat = "{0:n2}"

                GridView2.BestFitColumns(True)

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

            End If

            clsLnTrans_inv_enc.Elimina_Tabla_Temporal_ComparacionStock()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Try
            Calcular_Reporte_Resumen()
            Calcular_Reporte_Detalle()
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
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
            printLink.Component = grdStockCompara
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Comparación de Inventario Stock"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub frmComparacionInventarioStock_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try
            Select Case Modo

                Case TipoTrans.Editar

                    Set_Datata_Table()

                    Calcular_Reporte_Resumen()

                    Calcular_Reporte_Detalle()

            End Select

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView2.RowCellStyle

        ' #EJC20260603_ROWSTYLE_PRINT_GUARD: evitar costo de formato por celda durante impresión.
        If clsUiPrintHelper.IsPrintingPreviewInProgress Then Exit Sub

        If e.Column.FieldName = "Diferencia" Then

            Dim View As GridView = sender

            Dim CantidadCont As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Diferencia"))

            If CantidadCont > 0 Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                e.Appearance.ForeColor = Color.Black
                e.Appearance.BackColor = Color.LightBlue
                e.Appearance.BackColor2 = Color.SteelBlue
            ElseIf CantidadCont < 0 Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                e.Appearance.ForeColor = Color.Black
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.Firebrick
            End If

        End If

    End Sub

End Class
