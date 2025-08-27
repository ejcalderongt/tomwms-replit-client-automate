Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraSplashScreen

Public Class frmStockResJornada


    Private IsLoading As Boolean = False
    Private fecha_inicia As Date
    Private fecha_final As Date
    Private prima As Decimal
    Private emision As Decimal
    Private decreto As Decimal
    Private redondeo As Integer
    Private DT As New DataTable
    Private DT2 As New DataTable("Jornada")
    Private DT3 As New DataTable("Resumen")
    Public Property Modo As pModo
    Public Property ModoDepuracion As Boolean = False

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum


    Private Sub SetDatataTable()

        Try

            DT2.Columns.Add("Regimen", GetType(String))
            DT2.Columns.Add("Sector", GetType(String))
            DT2.Columns.Add("Codigo", GetType(String))
            DT2.Columns.Add("Bodega", GetType(String))
            DT2.Columns.Add("Fecha", GetType(Date))
            DT2.Columns.Add("Valor_aduana", GetType(Double))
            DT2.Columns.Add("Prima", GetType(Double))
            DT2.Columns.Add("Emision", GetType(Double))
            DT2.Columns.Add("Decreto", GetType(Double))
            DT2.Columns.Add("SubTotal", GetType(Double))
            DT2.Columns.Add("IVA", GetType(Double))
            DT2.Columns.Add("TotalGastos", GetType(Double))


        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub SetDatataTable3()

        Try

            DT3.Columns.Add("Sector", GetType(String))
            DT3.Columns.Add("Regimen", GetType(String))
            DT3.Columns.Add("Codigo", GetType(String))
            DT3.Columns.Add("Bodega", GetType(String))
            DT3.Columns.Add("Prima/Seguro", GetType(Double))
            DT3.Columns.Add("Emision", GetType(Double))
            DT3.Columns.Add("Decreto", GetType(Double))
            DT3.Columns.Add("SubTotal", GetType(Double))
            DT3.Columns.Add("IVA (12%)", GetType(Double))
            DT3.Columns.Add("Prima C/Gastos", GetType(Double))


        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub


    Private Sub frmStockResJornada_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            IMS.ListarRegimen(cmbRegimen)
            SetDatataTable()
            SetDatataTable3()


            deMesReporte.EditValue = DateTime.Now

            'GT 05102021: se obtiene fecha inicial y final del mes por defecto
            Dim Fecha_ As Date = deMesReporte.EditValue
            fecha_inicia = DateSerial(Fecha_.Year, Fecha_.Month, 1)
            fecha_final = DateSerial(Fecha_.Year, Fecha_.Month + 1, 0)


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If
        End Try

    End Sub

    Private Sub btnGenerar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnGenerar.ItemClick

        '#GT16062022: se inhabilitan los botones para evitar instanciar sin necesidad
        btnGenerar.Enabled = False
        Generar_Reporte()
        btnGenerar.Enabled = True

    End Sub

    Private Sub Generar_Reporte()

        Dim DT As New DataTable
        dgridDetallePorBodega.DataSource = Nothing
        prima = txtPrima.Text
        emision = txtEmision.Text
        decreto = txtDecreto.Text
        redondeo = txtRedondeo.Text

        DT.Clear()

        Try

            If Not IsLoading Then
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Obteniendo registros...")
            End If

            DT = clsLnReportesFiscales.Get_Stock_res_jornada(fecha_inicia, fecha_final, cmbRegimen.EditValue)

            If Not DT Is Nothing Then

                If DT.Rows.Count > 0 Then

                    DT2.Clear()
                    DT3.Clear()

                    Dim vIvaBodega As Double = 0

                    For Each dr As DataRow In DT.Rows

                        Dim vRegimen = dr.Item("Regimen")
                        Dim vSector = dr.Item("Sector")
                        Dim vIdBodega = dr.Item("idbodega")
                        Dim vCodigoBodega = dr.Item("Codigo")
                        Dim vbodega = dr.Item("Bodega")
                        Dim vFecha = dr.Item("Fecha")
                        Dim vAduana = Math.Round(dr.Item("Valor_aduana"), redondeo)
                        Dim vPrima = Math.Round((vAduana * prima) / 100, redondeo)
                        Dim vEmision = Math.Round((vPrima * emision) / 100, redondeo)
                        Dim vDecreto = Math.Round((vPrima * decreto) / 100, redondeo)
                        Dim vSub = Math.Round(vPrima + vEmision + vDecreto, redondeo)

                        vIvaBodega = clsLnBodega.Get_Valor_Porcentaje_IVA_By_CodigoBodega(vIdBodega)
                        vIvaBodega = Math.Round(vIvaBodega / 100, redondeo)

                        Dim vIVA = Math.Round((vPrima + vEmision) * vIvaBodega, redondeo)
                        Dim vTotal = Math.Round(vSub + vIVA, 2)

                        DT2.Rows.Add(vRegimen, vSector, vCodigoBodega, vbodega, vFecha, vAduana, vPrima, vEmision, vDecreto, vSub, vIVA, vTotal)

                    Next

                    Dim groupByQuery = From row In DT2.AsEnumerable()
                                       Group row By Name = New With {
                        Key .Sector = row("Sector"),
                        Key .Bodega = row("Bodega"),
                        Key .Regimen = row("Regimen"),
                        Key .Codigo = row("Codigo")
                        } Into NameGroup = Group
                                       Select New With {
                        .Sector = Name.Sector,
                        .Bodega = Name.Bodega,
                        .Regimen = Name.Regimen,
                        .Codigo = Name.Codigo
                                           }


                    For Each item In groupByQuery

                        Dim cSector = item.Sector
                        Dim cRegimen = item.Regimen
                        Dim cBodega = item.Bodega
                        Dim cCodigo = item.Codigo
                        Dim cPrima = (From c In DT2.AsEnumerable() Where c.Field(Of String)("Bodega") = cBodega AndAlso c.Field(Of String)("Regimen") = cRegimen).Sum(Function(x) Convert.ToDecimal(x.Item("Prima")))
                        Dim cEmision = (From c In DT2.AsEnumerable() Where c.Field(Of String)("Bodega") = cBodega AndAlso c.Field(Of String)("Regimen") = cRegimen).Sum(Function(x) Convert.ToDecimal(x.Item("Emision")))
                        Dim cDecreto = (From c In DT2.AsEnumerable() Where c.Field(Of String)("Bodega") = cBodega AndAlso c.Field(Of String)("Regimen") = cRegimen).Sum(Function(x) Convert.ToDecimal(x.Item("Decreto")))
                        Dim cSubTotal = (From c In DT2.AsEnumerable() Where c.Field(Of String)("Bodega") = cBodega AndAlso c.Field(Of String)("Regimen") = cRegimen).Sum(Function(x) Convert.ToDecimal(x.Item("SubTotal")))
                        Dim cIVA = (From c In DT2.AsEnumerable() Where c.Field(Of String)("Bodega") = cBodega AndAlso c.Field(Of String)("Regimen") = cRegimen).Sum(Function(x) Convert.ToDecimal(x.Item("IVA")))
                        Dim cTotal = (From c In DT2.AsEnumerable() Where c.Field(Of String)("Bodega") = cBodega).Sum(Function(x) Convert.ToDecimal(x.Item("TotalGastos")))
                        DT3.Rows.Add(cSector, cRegimen, cCodigo, cBodega, Math.Round(cPrima, redondeo), Math.Round(cEmision, redondeo), Math.Round(cDecreto, redondeo), Math.Round(cSubTotal, redondeo), Math.Round(cIVA, redondeo), Math.Round(cTotal, redondeo))

                    Next


                    dgridDetallePorBodega.DataSource = DT2
                    dgridResumenPorBodega.DataSource = DT3

                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                    GridView1.OptionsView.ShowFooter = True


                    GridView1.Columns("Sector").Visible = False
                    GridView1.Columns("Valor_aduana").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Valor_aduana").DisplayFormat.FormatString = "{0:n" & redondeo & "}"
                    GridView1.Columns("Valor_aduana").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Valor_aduana").SummaryItem.DisplayFormat = "{0:n" & redondeo & "}"
                    GridView1.Columns("Prima").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Prima").DisplayFormat.FormatString = "{0:n" & redondeo & "}"
                    GridView1.Columns("Prima").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Prima").SummaryItem.DisplayFormat = "{0:n" & redondeo & "}"
                    GridView1.Columns("Emision").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Emision").DisplayFormat.FormatString = "{0:n" & redondeo & "}"
                    GridView1.Columns("Emision").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Emision").SummaryItem.DisplayFormat = "{0:n" & redondeo & "}"
                    GridView1.Columns("Decreto").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Decreto").DisplayFormat.FormatString = "{0:n" & redondeo & "}"
                    GridView1.Columns("Decreto").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Decreto").SummaryItem.DisplayFormat = "{0:n" & redondeo & "}"
                    GridView1.Columns("SubTotal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("SubTotal").DisplayFormat.FormatString = "{0:n" & redondeo & "}"
                    GridView1.Columns("SubTotal").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("SubTotal").SummaryItem.DisplayFormat = "{0:n" & redondeo & "}"
                    GridView1.Columns("IVA").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("IVA").SummaryItem.DisplayFormat = "{0:n" & redondeo & "}"
                    GridView1.Columns("IVA").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("IVA").DisplayFormat.FormatString = "{0:n" & redondeo & "}"
                    GridView1.Columns("TotalGastos").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("TotalGastos").DisplayFormat.FormatString = "{0:n" & redondeo & "}"
                    GridView1.Columns("TotalGastos").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("TotalGastos").SummaryItem.DisplayFormat = "{0:n" & redondeo & "}"


                    GridView2.OptionsView.ShowFooter = True
                    GridView2.Columns("Prima/Seguro").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView2.Columns("Prima/Seguro").DisplayFormat.FormatString = "{0:n" & redondeo & "}"
                    GridView2.Columns("Prima/Seguro").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView2.Columns("Prima/Seguro").SummaryItem.DisplayFormat = "{0:n" & redondeo & "}"
                    GridView2.Columns("Emision").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView2.Columns("Emision").DisplayFormat.FormatString = "{0:n" & redondeo & "}"
                    GridView2.Columns("Emision").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView2.Columns("Emision").SummaryItem.DisplayFormat = "{0:n" & redondeo & "}"
                    GridView2.Columns("Decreto").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView2.Columns("Decreto").DisplayFormat.FormatString = "{0:n" & redondeo & "}"
                    GridView2.Columns("Decreto").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView2.Columns("Decreto").SummaryItem.DisplayFormat = "{0:n" & redondeo & "}"
                    GridView2.Columns("SubTotal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView2.Columns("SubTotal").DisplayFormat.FormatString = "{0:n" & redondeo & "}"
                    GridView2.Columns("SubTotal").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView2.Columns("SubTotal").SummaryItem.DisplayFormat = "{0:n" & redondeo & "}"
                    GridView2.Columns("IVA (12%)").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView2.Columns("IVA (12%)").DisplayFormat.FormatString = "{0:n" & redondeo & "}"
                    GridView2.Columns("IVA (12%)").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView2.Columns("IVA (12%)").SummaryItem.DisplayFormat = "{0:n" & redondeo & "}"
                    GridView2.Columns("Prima C/Gastos").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView2.Columns("Prima C/Gastos").DisplayFormat.FormatString = "{0:n" & redondeo & "}"
                    GridView2.Columns("Prima C/Gastos").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView2.Columns("Prima C/Gastos").SummaryItem.DisplayFormat = "{0:n" & redondeo & "}"


                    GridView1.Columns("Bodega").Group()
                    GridView2.Columns("Sector").Group()

                    Dim vaduana_ As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Valor_aduana",
                        .SummaryType = DevExpress.Data.SummaryItemType.Average,
                        .DisplayFormat = "Total: {0:n" & redondeo & "}",
                        .ShowInGroupColumnFooter = GridView1.Columns("Valor_aduana")}
                    GridView1.GroupSummary.Add(vaduana_)

                    Dim vprima_ As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Prima",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "Total: {0:n" & redondeo & "}",
                        .ShowInGroupColumnFooter = GridView1.Columns("Prima")}
                    GridView1.GroupSummary.Add(vprima_)

                    Dim vemision_ As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Emision",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "Total: {0:n" & redondeo & "}",
                        .ShowInGroupColumnFooter = GridView1.Columns("Emision")}
                    GridView1.GroupSummary.Add(vemision_)

                    Dim vdecreto_ As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Decreto",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "Total: {0:n" & redondeo & "}",
                        .ShowInGroupColumnFooter = GridView1.Columns("Decreto")}
                    GridView1.GroupSummary.Add(vdecreto_)

                    Dim vsubtotal_ As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "SubTotal",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "Total: {0:n" & redondeo & "}",
                        .ShowInGroupColumnFooter = GridView1.Columns("SubTotal")}
                    GridView1.GroupSummary.Add(vsubtotal_)

                    Dim viva_ As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "IVA",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "Total: {0:n" & redondeo & "}",
                        .ShowInGroupColumnFooter = GridView1.Columns("IVA")}
                    GridView1.GroupSummary.Add(viva_)

                    Dim vtotal_ As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "TotalGastos",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "Total: {0:n" & redondeo & "}",
                        .ShowInGroupColumnFooter = GridView1.Columns("TotalGastos")}
                    GridView1.GroupSummary.Add(vtotal_)


                    'GT 05102021 aqui van los totales para el segundo tab del reporte

                    Dim vprima2_ As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Prima/Seguro",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "Total: {0:n" & redondeo & "}",
                        .ShowInGroupColumnFooter = GridView2.Columns("Prima/Seguro")}
                    GridView2.GroupSummary.Add(vprima2_)

                    Dim vemision2_ As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Emision",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "Total: {0:n" & redondeo & "}",
                        .ShowInGroupColumnFooter = GridView2.Columns("Emision")}
                    GridView2.GroupSummary.Add(vemision2_)

                    Dim vdecreto2_ As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Decreto",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "Total: {0:n" & redondeo & "}",
                        .ShowInGroupColumnFooter = GridView2.Columns("Decreto")}
                    GridView2.GroupSummary.Add(vdecreto2_)

                    Dim vsubtotal2_ As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "SubTotal",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "Total: {0:n" & redondeo & "}",
                        .ShowInGroupColumnFooter = GridView2.Columns("SubTotal")}
                    GridView2.GroupSummary.Add(vsubtotal2_)

                    Dim viva2_ As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "IVA (12%)",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "Total: {0:n" & redondeo & "}",
                        .ShowInGroupColumnFooter = GridView2.Columns("IVA (12%)")}
                    GridView2.GroupSummary.Add(viva2_)

                    Dim vtotal2_ As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Prima C/Gastos",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "Total: {0:n" & redondeo & "}",
                        .ShowInGroupColumnFooter = GridView2.Columns("Prima C/Gastos")}
                    GridView2.GroupSummary.Add(vtotal2_)



                    GridView1.ExpandAllGroups()
                    GridView2.ExpandAllGroups()
                    GridView1.BestFitColumns(True)
                    GridView2.BestFitColumns(True)

                End If

            End If



        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If
        End Try

        Application.DoEvents()

    End Sub

    Private Sub btnImport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnImport.ItemClick
        Exportar_Grid_A_Excel(dgridDetallePorBodega, "WMS_ValorizacionStockJornada.xlsx")
    End Sub

    Private Sub Exportar_Grid_A_Excel(ByRef dGrid As GridControl, ByVal NomArchivo As String)

        Try

            Try

                Dim myStream As Stream
                Dim saveFileDialog1 As New SaveFileDialog()

                saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
                saveFileDialog1.FilterIndex = 1
                saveFileDialog1.RestoreDirectory = True
                saveFileDialog1.FileName = NomArchivo

                If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                    myStream = saveFileDialog1.OpenFile()
                    If (myStream IsNot Nothing) Then
                        ' Code to write the stream goes here.
                        dGrid.ExportToXlsx(myStream)
                        myStream.Close()
                    End If
                End If

            Catch ex As Exception
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub btnExit_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnExit.ItemClick
        Close()
    End Sub

    Private Sub mnuDetallePorBodega_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDetallePorBodega.ItemClick

        Try

            TituloReporte = "Detalle por bodega"
            Imprimir_Vista(dgridDetallePorBodega)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuResumenPorBodega_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuResumenPorBodega.ItemClick

        Try

            TituloReporte = "Resumen por bodega"
            Imprimir_Vista(dgridResumenPorBodega)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub Imprimir_Vista(ByVal pGrid As GridControl)

        Try

            GridView1.OptionsPrint.ExpandAllDetails = True
            GridView1.OptionsPrint.PrintDetails = True

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
            printLink.Component = pGrid
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

    Public Property TituloReporte As String = ""
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & TituloReporte

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub deMesReporte_TextChanged(sender As Object, e As EventArgs) Handles deMesReporte.TextChanged

        Try

            Dim fecha_ As Date = deMesReporte.EditValue
            fecha_inicia = DateSerial(fecha_.Year, fecha_.Month, 1)
            fecha_final = DateSerial(fecha_.Year, fecha_.Month + 1, 0)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub
End Class