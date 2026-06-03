Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI

Public Class frmEstadoEnviosNAV

    Private pEnviosNAVPC As New List(Of clsBeVW_EstadoEnviosNAV)
    Private pEnviosNAVPT As New List(Of clsBeVW_EstadoEnviosNAV)
    Private pEnviosNAV As New List(Of clsBeVW_EstadoEnviosNAV)

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Private Sub Imprimir_Grafica()
        Dim repEnviosNAV As New repEstadoEnviosNAV

        Try

            repEnviosNAV.XrChart1.DataSource = clsLnVW_EstadoEnviosNAV.Listar(dtpFechaDel.Value, dtpFechaAl.Value, "PT")
            repEnviosNAV.XrChart2.DataSource = clsLnVW_EstadoEnviosNAV.Listar(dtpFechaDel.Value, dtpFechaAl.Value, "PC")

            repEnviosNAV.Parameters("FechaIni").Value = dtpFechaDel.Value
            repEnviosNAV.Parameters("FechaFin").Value = dtpFechaAl.Value

            repEnviosNAV.Parameters("TotalPC").Value = clsLnVW_EstadoEnviosNAV.GetTotalByFechaByTipo(dtpFechaDel.Value, dtpFechaAl.Value, "PC")
            repEnviosNAV.Parameters("TotalPT").Value = clsLnVW_EstadoEnviosNAV.GetTotalByFechaByTipo(dtpFechaDel.Value, dtpFechaAl.Value, "PT")

            repEnviosNAV.Parameters("Usuario").Value = String.Format("{0} - {1} {2}", AP.UsuarioAp.Codigo, AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)

            repEnviosNAV.RequestParameters = False
            repEnviosNAV.ShowPreviewDialog()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Cargar()

        Dim DT As New DataTable

        Try

            DT = clsLnVW_EstadoEnviosNAV.ListarPorColumna(dtpFechaDel.Value, dtpFechaAl.Value)

            If DT IsNot Nothing Then

                grdEnviosNAV.DataSource = DT

                If GridView1.Columns.Count > 0 Then

                    GridView1.OptionsView.ShowFooter = True

                    GridView1.Columns("Fecha").Group()

                    GridView1.Columns("Enviados_PC").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Enviados_PC").SummaryItem.DisplayFormat = "{0}"

                    GridView1.Columns("Enviados_PC").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Enviados_PC").DisplayFormat.FormatString = "{0}"

                    GridView1.Columns("No_Enviados_PC").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("No_Enviados_PC").SummaryItem.DisplayFormat = "{0}"

                    GridView1.Columns("No_Enviados_PC").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("No_Enviados_PC").DisplayFormat.FormatString = "{0}"

                    GridView1.Columns("Total_PC").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Total_PC").SummaryItem.DisplayFormat = "{0}"

                    GridView1.Columns("Total_PC").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Total_PC").DisplayFormat.FormatString = "{0}"

                    GridView1.Columns("Enviados_PT").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Enviados_PT").SummaryItem.DisplayFormat = "{0}"

                    GridView1.Columns("Enviados_PT").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Enviados_PT").DisplayFormat.FormatString = "{0}"

                    GridView1.Columns("No_Enviados_PT").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("No_Enviados_PT").SummaryItem.DisplayFormat = "{0}"

                    GridView1.Columns("No_Enviados_PT").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("No_Enviados_PT").DisplayFormat.FormatString = "{0}"

                    GridView1.Columns("Total_PT").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Total_PT").SummaryItem.DisplayFormat = "{0}"

                    GridView1.Columns("Total_PT").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Total_PT").DisplayFormat.FormatString = "{0}"

                    GridView1.ExpandAllGroups()
                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                    GridView1.BestFitColumns(True)

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

    Private Sub dtpFechaAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaAl.ValueChanged

        Try

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then
            '    Throw New Exception("Seleccione un rango de fechas válido.")
            'Else
            '    Cargar()
            '    GridView1.Focus()
            'End If

            Cargar()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then
            '    Throw New Exception("Seleccione un rango de fechas válido.")
            'Else
            '    Cargar()
            '    GridView1.Focus()
            'End If

            Cargar()
            GridView1.Focus()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmMovimientosCardex_Load(sender As Object, e As EventArgs) Handles Me.Load
        clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
        Cargar()
    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar()
    End Sub

    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(grdEnviosNAV, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
            Exit Sub
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
            printLink.Component = grdEnviosNAV
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

        Dim reportHeader As String = vbNewLine & "TOMIMS,WMS. Reporte de: " & vbNewLine &
                                                 "Listado de Envios a NAV"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Grafica()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub mnuImprimirGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimirGrid.ItemClick
        Imprimir_Vista()
    End Sub
End Class



