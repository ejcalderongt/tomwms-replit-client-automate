Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmResumenStock

    Public listaStock As New List(Of clsBeVW_stock_res)

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub ListarEncabezado()

        Dim lRow As DataRow

        Try

            listaStock = clsLnStock.Get_All_Stock_By_IdBodega_And_IdPropietarioBodega(cmbBodega.EditValue, cmbPropietario.EditValue)

            '#EJC20180111: REFLINQ
            Dim ListaEncabezadoStock = From i In listaStock Group i By Keys = New With {Key i.IdProducto, Key i.Codigo_Producto,
                                                                Key i.Propietario, Key i.Nombre_Producto, Key i.Nombre_Presentacion, Key i.Codigo_Barra, Key i.UMBas} Into Group
                                       Select New With {.id = Keys.IdProducto,
                                           .cod = Keys.Codigo_Producto,
                                           .prop = Keys.Propietario,
                                           .nom = Keys.Nombre_Producto,
                                           .pres = Keys.Nombre_Presentacion,
                                            .barra = Keys.Codigo_barra, .um = Keys.UMBas,
                                            .CantidadUMBas = Group.Sum(Function(x) x.CantidadUmBas),
                                            .CantidadPresentacion = Group.Sum(Function(x) x.CantidadPresentacion)}

            If ListaEncabezadoStock IsNot Nothing AndAlso ListaEncabezadoStock.Count > 0 Then

                For Each Obj In ListaEncabezadoStock

                    lRow = DsResumenStock.Encabezado.NewRow

                    lRow.Item("IdProducto") = Obj.id
                    lRow.Item("Código") = Obj.cod
                    lRow.Item("Propietario") = Obj.prop
                    lRow.Item("Producto") = Obj.nom
                    lRow.Item("Presentación") = Obj.pres
                    lRow.Item("Código_Barra") = Obj.barra
                    lRow.Item("CantidadUMBas") = Obj.CantidadUMBas
                    lRow.Item("UM_Bas") = Obj.um
                    lRow.Item("CantidadPresentación") = Obj.CantidadPresentacion

                    DsResumenStock.Encabezado.AddEncabezadoRow(lRow)

                Next

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

    Private Sub ListarDetalle()

        Dim lRow As DataRow

        Try

            listaStock = clsLnStock.Get_All_Stock_Detalle_Resumen()

            Dim ListaEncabezadoStock = From i In listaStock Group i By Keys = New With {Key i.IdProducto, Key i.Codigo_Producto,
                                                                Key i.Propietario, Key i.Nombre_Producto, Key i.Nombre_Presentacion,
                                                                Key i.Codigo_Barra, Key i.UMBas, Key i.IdProductoBodega,
                                                                Key i.IdStock, Key i.NomEstado,
                                                                Key i.Serial, Key i.CantidadPresentacion,
                                                                Key i.CantidadUmBas, Key i.Fecha_ingreso, Key i.Fecha_Vence,
                                                                Key i.Lote, Key i.IdRecepcionEnc,
                                                                Key i.IdUbicacion, Key i.Ubicacion_Tramo, Key i.Ubicacion_Nombre,
                                                                Key i.LargoUbicacion} Into Group
                                       Select New With {.id = Keys.IdProducto, .cod = Keys.Codigo_Producto, .prop = Keys.Propietario, .nom = Keys.Nombre_Producto, .pres = Keys.Nombre_Presentacion,
                                                        .barra = Keys.Codigo_barra, .um = Keys.UMBas, Keys.IdStock,
                                                        .idProdBodega = Keys.IdProductoBodega, .estado = Keys.NomEstado, Keys.Serial,
                                                        .cant_pres = Keys.CantidadPresentacion, .cant_umbas = Keys.CantidadUmBas, .fechaing = Keys.Fecha_ingreso, Keys.Fecha_Vence,
                                                        Keys.Lote, .idrec = Keys.IdRecepcionEnc,
                                                        .idubic = Keys.IdUbicacion, .tramo = Keys.Ubicacion_Tramo,
                                                        .ubic = Keys.Ubicacion_Nombre, .largo = Keys.LargoUbicacion}

            If ListaEncabezadoStock IsNot Nothing AndAlso ListaEncabezadoStock.Count > 0 Then

                DsResumenStock.Detalle.Clear()

                For Each Obj In ListaEncabezadoStock

                    lRow = DsResumenStock.Detalle.NewRow

                    lRow.Item("IdProducto") = Obj.id
                    lRow.Item("IdProductoBodega") = Obj.idProdBodega
                    lRow.Item("IdStock") = Obj.idStock
                    lRow.Item("Codigo") = Obj.cod
                    lRow.Item("Propietario") = Obj.prop
                    lRow.Item("Producto") = Obj.nom
                    lRow.Item("Barra") = Obj.barra
                    lRow.Item("Estado") = Obj.estado
                    lRow.Item("Presentacion") = Obj.pres
                    lRow.Item("UMBas") = Obj.um
                    lRow.Item("serial") = Obj.serial
                    lRow.Item("Cant_Presentacion") = Obj.cant_pres
                    lRow.Item("Cant_UMBas") = Obj.cant_umbas
                    lRow.Item("Fecha_Ingreso") = Obj.fechaing
                    lRow.Item("Fecha_Vence") = Obj.Fecha_Vence
                    lRow.Item("lote") = Obj.lote
                    lRow.Item("NoRecepcion") = Obj.idrec
                    lRow.Item("IdUbicacion") = Obj.idubic
                    lRow.Item("Tramo") = Obj.tramo
                    lRow.Item("Ubicacion") = Obj.ubic
                    lRow.Item("largo") = Obj.largo

                    DsResumenStock.Detalle.AddDetalleRow(lRow)

                Next

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

    Private Sub Cargar_Datos()

        Try

            grdResumenStock.BeginUpdate()

            ListarEncabezado()

            ListarDetalle()
            DsResumenStock.Detalle.IdProductoBodegaColumn.ReadOnly = True And Visible = False
            DsResumenStock.Detalle.IdProductoColumn.ReadOnly = True And Visible = False
            DsResumenStock.Detalle.NoRecepcionColumn.ReadOnly = True And Visible = False
            DsResumenStock.Detalle.IdStockColumn.ReadOnly = True And Visible = False
            DsResumenStock.Detalle.CodigoColumn.ReadOnly = True And Visible = False
            DsResumenStock.Detalle.EstadoColumn.ReadOnly = True
            DsResumenStock.Detalle.loteColumn.ReadOnly = True
            DsResumenStock.Detalle.fecha_venceColumn.ReadOnly = True
            DsResumenStock.Detalle.PropietarioColumn.ReadOnly = True

            grdResumenStock.EndUpdate()

            grdResumenStock.ForceInitialize()

            If GridView1.RowCount > 0 Then
                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
            End If

            GridView1.BestFitColumns()

            If GridView1.Columns.Count > 0 Then

                GridView1.OptionsView.ShowFooter = True

                GridView1.Columns("CantidadUMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("CantidadUMBas").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("CantidadUMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("CantidadUMBas").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("CantidadPresentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("CantidadPresentación").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("CantidadPresentación").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("CantidadPresentación").SummaryItem.DisplayFormat = "{0:n6}"

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(grdResumenStock, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
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
            printLink.Component = grdResumenStock
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

        Dim reportHeader As String = vbNewLine & "Reporte de Stock"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub btnImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub btnActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnActualizar.ItemClick
        Cargar_Datos()
    End Sub

    Private Sub btnSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSalir.ItemClick
        Close()
    End Sub

    Private Sub frmResumenStock_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
            AP.Listar_Bodegas_By_Usuario(cmbBodega)
            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

            Cargar_Datos()

        Catch ex As Exception
            XtraMessageBox.Show("Error: " & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    '#EJC20171004_0454AM: Ocultar columnas del grid de detalle
    Private Sub grdResumenStock_ViewRegistered(sender As Object, e As ViewOperationEventArgs) Handles grdResumenStock.ViewRegistered

        Try

            Dim gridView As GridView = e.View

            If gridView.IsDetailView Then

                gridView.Columns("IdProductoBodega").Visible = False
                gridView.Columns("Producto").Visible = False
                gridView.Columns("IdProducto").Visible = False
                gridView.Columns("IdStock").Visible = False
                gridView.Columns("Codigo").Visible = False
                gridView.Columns("Propietario").Visible = False
                gridView.Columns("lote").Caption = "Lote"
                gridView.Columns("Estado").Caption = "Estado"
                gridView.Columns("Fecha_Vence").Caption = "Vence"
                gridView.Columns("NoRecepcion").Caption = "Recepción"

                '#CM20171026_1010AM: filtro para detalle de reporte de existencias.
                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim vPresActual As String = IIf(IsDBNull(Dr.Item("Presentación")), "", Dr.Item("Presentación"))
                Dim vUMBas As String = IIf(IsDBNull(Dr.Item("UM_Bas")), "", Dr.Item("UM_Bas"))

                If vPresActual <> "" Then
                    gridView.ActiveFilterCriteria = New DevExpress.Data.Filtering.BinaryOperator("Presentación", vPresActual)
                Else
                    gridView.ActiveFilterCriteria = New DevExpress.Data.Filtering.BinaryOperator("UMBas", vUMBas)
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

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)
        Cargar_Datos()

    End Sub
End Class



