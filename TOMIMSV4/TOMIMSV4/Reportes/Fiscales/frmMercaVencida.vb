Imports System.IO
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmMercaVencida

    Public Property Modo As pModo
    Private IsLoading As Boolean = False
    Public Property ModoDepuracion As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmMercaVencida_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            IsLoading = True
            AP.Listar_Bodegas_By_Usuario(cmbBodega)
            Listar_Propietarios()


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            IsLoading = False
        End Try

    End Sub


    Private Sub Listar_Propietarios()

        Try

            Dim DT1 As New DataTable
            DT1 = clsLnPropietario_bodega.Get_All_By_IdBodega_For_Combo(cmbBodega.EditValue)
            lcmbPropietario.Properties.DataSource = DT1
            lcmbPropietario.Properties.ValueMember = "IdPropietarioBodega"
            lcmbPropietario.Properties.DisplayMember = "Nombre"
            lcmbPropietario.Properties.PopupWidth = 700
            lcmbPropietario.Properties.BestFit()
            lcmbPropietario.Properties.PopulateColumns()
            lcmbPropietario.Properties.Columns(0).Visible = False
            lcmbPropietario.Properties.Columns(1).Visible = False
            lcmbPropietario.Properties.NullText = ""

            If DT1.Rows.Count = 1 Then
                lcmbPropietario.Text = DT1.Rows(0).Item("Nombre").ToString
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

    Private Sub cmdActualizar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdActualizar.ItemClick

        cmdActualizar.Enabled = False
        Generar_Reporte()
        cmdActualizar.Enabled = True

    End Sub

    Private Sub Generar_Reporte()

        Dim DT As New DataTable
        Dim IdPropietario As Integer
        dgridMercanciaVencida.DataSource = Nothing

        Try

            If Not IsLoading Then
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Obteniendo registros...")
            End If

            If lcmbPropietario.EditValue <> 0 Then
                Dim fila As Object = lcmbPropietario.GetSelectedDataRow
                IdPropietario = fila.Item("IdPropietario")
            Else
                IdPropietario = 0

            End If

            DT = clsLnReportesFiscales.Get_Fiscal_Merca_vencida(dtpfechaHasta.Value, IdPropietario, cmbBodega.EditValue)

            If Not DT Is Nothing Then

                If DT.Rows.Count > 0 Then

                    dgridMercanciaVencida.DataSource = DT

                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                    GridView1.OptionsView.ShowFooter = True


                    GridView1.Columns("cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("cantidad").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("cantidad").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("net_weigth").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("net_weigth").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("net_weigth").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("net_weigth").SummaryItem.DisplayFormat = "{0:n6}"

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

    Private Sub cmdImprimir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub Imprimir_Vista()

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
            printLink.Component = dgridMercanciaVencida
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

        Dim reportHeader As String = vbNewLine & "Salidas"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImpExcel_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImpExcel.ItemClick

        Try

            Dim myStream As Stream
            Dim saveFileDialog1 As New SaveFileDialog()

            saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
            saveFileDialog1.FilterIndex = 1
            saveFileDialog1.RestoreDirectory = True
            saveFileDialog1.FileName = "Mercaderia_Vencida_" & "_Al_" & FormatoFechas.tFecha(dtpfechaHasta.Value) & ".xlsx"

            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                myStream = saveFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    ' Code to write the stream goes here.
                    dgridMercanciaVencida.ExportToXlsx(myStream)
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