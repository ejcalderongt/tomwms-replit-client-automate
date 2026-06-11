Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmValorizacionStockOC

    Public DTValoracionStockEnc As New DataTable
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmValorizacionStockOC_Load(sender As Object, e As EventArgs) Handles Me.Load


        Try

            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub


    Public Sub Cargar_Datos()

        Try

            grdValorizacionStock.BeginUpdate()

            DTValoracionStockEnc = clsLnStock.Get_Reporte_Valoracion_By_OC_DataTable(cmbBodega.EditValue,
                                                                                     cmbPropietario.EditValue)

            grdValorizacionStock.DataSource = DTValoracionStockEnc

            grdValorizacionStock.EndUpdate()

            grdValorizacionStock.ForceInitialize()

            If GridView1.Columns.Count > 0 Then

                GridView1.OptionsView.ShowFooter = True

                Try

                    GridView1.Columns("Cantidad_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad_UMBas").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Cantidad_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cantidad_UMBas").SummaryItem.DisplayFormat = "Sum = {0:n6}"

                    GridView1.Columns("Cantidad_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad_Pres").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Cantidad_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cantidad_Pres").SummaryItem.DisplayFormat = "Sum = {0:n6}"

                    GridView1.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Cantidad_Reservada_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cantidad_Reservada_UMBas").SummaryItem.DisplayFormat = "Sum = {0:n6}"

                    GridView1.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Cantidad_Reservada_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cantidad_Reservada_Pres").SummaryItem.DisplayFormat = "Sum = {0:n6}"

                    GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Disponible_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Disponible_UMBas").SummaryItem.DisplayFormat = "Sum = {0:n6}"

                    GridView1.Columns("Disponible_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Disponible_Pres").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Disponible_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Disponible_Pres").SummaryItem.DisplayFormat = "Sum = {0:n6}"

                    GridView1.Columns("Costo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Costo").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Total").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Total").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Total").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Total").SummaryItem.DisplayFormat = "Sum = {0:n6}"

                Catch ex As Exception
                    XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Try

            End If

            'lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

            GridView1.OptionsView.ColumnAutoWidth = False

            GridView1.BestFitColumns()


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
            clsUiPrintHelper.PrintGridPreview(grdValorizacionStock, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
            Exit Sub
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
            printLink.Component = grdValorizacionStock
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

        Dim reportHeader As String = vbNewLine & "Reporte de Stock por OC"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub


    Private Sub grdValorizacionStock_ViewRegistered(sender As Object, e As ViewOperationEventArgs) Handles grdValorizacionStock.ViewRegistered
        Try

            Dim gridView As GridView = e.View

            If gridView.IsDetailView Then

                gridView.Columns("codigo").Caption = "Código"
                gridView.Columns("nombre").Caption = "Nombre"
                gridView.Columns("fecha_ingreso").Caption = "Fecha Ingreso"
                gridView.Columns("CantidadSF").Caption = "Cantidad UMBAS"
                gridView.Columns("fecha_vence").Caption = "Fecha Vence"
                gridView.Columns("costo").Caption = "Costo"

            End If

            If gridView.Columns.Count > 0 Then

                gridView.OptionsView.ShowFooter = True

                gridView.Columns("CantidadSF").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridView.Columns("CantidadSF").SummaryItem.DisplayFormat = "{0:n6}"
                gridView.Columns("CantidadSF").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gridView.Columns("CantidadSF").DisplayFormat.FormatString = "{0:n6}"

                gridView.Columns("CantidadReservada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gridView.Columns("CantidadReservada").DisplayFormat.FormatString = "{0:n6}"
                gridView.Columns("CantidadReservada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridView.Columns("CantidadReservada").SummaryItem.DisplayFormat = "{0:n6}"

                gridView.Columns("costo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gridView.Columns("costo").DisplayFormat.FormatString = "{0:n6}"
                gridView.Columns("costo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridView.Columns("costo").SummaryItem.DisplayFormat = "{0:n6}"

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

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        Try

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

            Cargar_Datos()
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub frmValorizacionStockOC_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            Cargar_Datos()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Cargar_Datos()
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Close()
    End Sub

    Private Sub frmValorizacionStockOC_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.Escape Then
            Close()
        End If

    End Sub

    Private Sub mnuImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimir.ItemClick
        Imprimir_Vista()
    End Sub


End Class



