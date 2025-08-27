Imports System.IO
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmCtasOrden

    Private IsLoading As Boolean = False
    Public Property Modo As pModo
    Public Property ModoDepuracion As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum



    Private Sub frmCtasOrden_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            IsLoading = True
            IMS.ListarRegimen(cmbRegimen)
            IMS.ListarMovimiento(cmbTransaccion)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            IsLoading = False
        End Try

    End Sub


    Private Sub cmdActualizar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdActualizar.ItemClick

        Generar_Reporte()

    End Sub

    Private Sub Generar_Reporte()


        Dim DT As New DataTable
        grdExistencias.DataSource = Nothing

        Try

            If Not IsLoading Then
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Obteniendo registros...")
            End If

            DT.Clear()

            DT = clsLnReportesFiscales.Get_Fiscal_Det_CtasOrden(dtpfechaHasta.Value, cmbTransaccion.EditValue, cmbRegimen.EditValue)


            If Not DT Is Nothing Then

                'If ProductoEspecifico IsNot Nothing AndAlso ProductoEspecifico.IdProducto > 0 Then

                '    Dim query =
                '            From c In DT.AsEnumerable()
                '            Where c.Field(Of String)("codigo") = (ProductoEspecifico.Codigo)

                '    If query.Count > 0 Then
                '        DT = query.CopyToDataTable
                '    Else
                '        DT.Clear()
                '    End If

                'End If

                If DT.Rows.Count > 0 Then

                    grdExistencias.DataSource = DT

                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                    GridView1.OptionsView.ShowFooter = True


                    GridView1.Columns("cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("cantidad").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("cantidad").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("valor_dai").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("valor_dai").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("valor_dai").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("valor_dai").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("valor_iva").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("valor_iva").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("valor_iva").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("valor_iva").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("suma_dai_iva").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("suma_dai_iva").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("suma_dai_iva").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("suma_dai_iva").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("valor_aduana").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("valor_aduana").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("valor_aduana").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("valor_aduana").DisplayFormat.FormatString = "{0:n6}"



                    GridView1.BestFitColumns(True)

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
            printLink.Component = grdExistencias
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

        Dim reportHeader As String = vbNewLine & "TOM, WMS" &
                              vbNewLine & "Resumen Ctas de Orden "

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdSalir.ItemClick

        Close()

    End Sub

    Private Sub cmdImpExcel_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImpExcel.ItemClick

        Try

            'dgrid.ExportToXlsx()

            Dim myStream As Stream
            Dim saveFileDialog1 As New SaveFileDialog()

            saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
            saveFileDialog1.FilterIndex = 1
            saveFileDialog1.RestoreDirectory = True
            saveFileDialog1.FileName = "VW_Fiscal_CtasOrden_" & "_Al_" & FormatoFechas.tFecha(dtpfechaHasta.Value) & ".xlsx"

            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                myStream = saveFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    ' Code to write the stream goes here.
                    grdExistencias.ExportToXlsx(myStream)
                    myStream.Close()
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


End Class