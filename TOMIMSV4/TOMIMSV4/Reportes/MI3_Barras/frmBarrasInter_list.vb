Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmBarrasInter_list

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Private DT As New DataTable("StockPorLote")

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub Cargar_Datos()

        Try

            Dim DT As New DataTable
            grdListBarras.DataSource = Nothing

            DT = clsLnI_nav_barras_pallet.Get_All_DT(dtpFechaDel.Value, dtpFechaAl.Value)

            If Not DT Is Nothing Then

                If DT.Rows.Count > 0 Then

                    grdListBarras.DataSource = DT

                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                    GridView1.OptionsView.ShowFooter = True

                    GridView1.Columns("Cantidad_Presentacion").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("Cantidad_Presentacion").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("Cantidad_Presentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad_Presentacion").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Cantidad_UMP").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("Cantidad_UMP").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("Cantidad_UMP").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad_UMP").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.ExpandAllGroups()

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

    Private Sub frmBarrasInter_list_Load(sender As Object, e As EventArgs) Handles Me.Load
        clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
        Cargar_Datos()
    End Sub

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then
            '    Throw New Exception("Seleccione un rango de fechas válido.")
            'Else
            '    Cargar_Datos()
            '    GridView1.Focus()
            'End If

            Cargar_Datos()
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

    Private Sub dtpFechaAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaAl.ValueChanged

        Try

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then
            '    Throw New Exception("Seleccione un rango de fechas válido.")
            'Else
            '    Cargar_Datos()
            '    GridView1.Focus()
            'End If

            Cargar_Datos()

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
            clsUiPrintHelper.PrintGridPreview(grdListBarras, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
            Exit Sub
            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
            TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {"Páginas: [Page # of Pages #] "})
            phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", "Fecha: [Date Printed] [Time Printed] "})
            phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = grdListBarras
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            SplashScreenManager.CloseForm(False)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & Text

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar_Datos()
    End Sub
End Class



