Imports System.IO
Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmMovimientosDoc

    Private pListaMov As New List(Of clsBeVW_Movimientos_Propietario)
    Private DT As New DataTable("MovsCardex")
    Private DT2 As New DataTable("MovsPropietario")
    Public Property IsLoading As Boolean = True
    Public pBeListaProductos As New List(Of Integer)
    Public Property ProductoEspecifico As New clsBeProducto
    Private vNombreArchivoLayOutGrid As String = ""

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub SetDatataTable()

        DT.Columns.Add("Propietario", GetType(String))
        DT.Columns.Add("Póliza", GetType(String))
        DT.Columns.Add("NombreArea", GetType(String))
        DT.Columns.Add("Producto", GetType(String))
        DT.Columns.Add("Código", GetType(String))
        DT.Columns.Add("Código Barra", GetType(String))
        DT.Columns.Add("Cantidad U.M.Bas", GetType(Double))
        DT.Columns.Add("UMBas", GetType(String))
        DT.Columns.Add("Cantidad Presentación", GetType(Double))
        DT.Columns.Add("Presentación", GetType(String))
        DT.Columns.Add("Peso", GetType(Double))
        DT.Columns.Add("Lote", GetType(String))
        DT.Columns.Add("FechaVence", GetType(Date))
        DT.Columns.Add("Licencia", GetType(String))
        DT.Columns.Add("FechaMovimiento", GetType(Date))
        DT.Columns.Add("Estado Origen", GetType(String))
        DT.Columns.Add("Estado Destino", GetType(String))
        DT.Columns.Add("Ubicación Origen", GetType(String))
        DT.Columns.Add("Ubicación Destino", GetType(String))
        DT.Columns.Add("Tipo Tarea", GetType(String))
        DT.Columns.Add("IdBodegaOrigen", GetType(Integer))
        DT.Columns.Add("IdProducto", GetType(Integer))
        DT.Columns.Add("Clasificacion", GetType(String))
        DT.Columns.Add("Area_Origen", GetType(String))
        '#GT24032022: campos para reporte cealsa
        DT.Columns.Add("regimen_ingreso", GetType(String))
        DT.Columns.Add("no_ticket_tms", GetType(Integer))
        DT.Columns.Add("fecha_ingreso", GetType(String))
        DT.Columns.Add("placa_ingreso", GetType(String))
        DT.Columns.Add("marca_ingreso", GetType(String))
        DT.Columns.Add("tipo_ingreso", GetType(String))
        DT.Columns.Add("contenedor_ingreso", GetType(String))
        DT.Columns.Add("Regimen_Salida", GetType(String))
        DT.Columns.Add("Fecha_Salida", GetType(String))
        DT.Columns.Add("placa_salida", GetType(String))
        DT.Columns.Add("marca_salida", GetType(String))
        DT.Columns.Add("tipo_salida", GetType(String))
        DT.Columns.Add("contenedor_salida", GetType(String))
        DT.Columns.Add("Poliza_Salida", GetType(String))
        DT.Columns.Add("numero_referencia", GetType(String))

    End Sub


    Private Sub SetDatataTable2()

        DT2.Columns.Add("TipoTarea", GetType(String))
        DT2.Columns.Add("idpedidoenc", GetType(Integer))
        DT2.Columns.Add("iddespachoenc", GetType(Integer))
        DT2.Columns.Add("IdDespachoDet", GetType(Integer))
        DT2.Columns.Add("observacion", GetType(String))
        DT2.Columns.Add("no_ticket_tms", GetType(Integer))
        DT2.Columns.Add("fecha_ingreso", GetType(Date))

        DT2.Columns.Add("poliza", GetType(String))
        DT2.Columns.Add("numero_orden", GetType(String))
        DT2.Columns.Add("referencia", GetType(String))
        DT2.Columns.Add("valor_aduana", GetType(Double))
        DT2.Columns.Add("valor_dai", GetType(Double))
        DT2.Columns.Add("valor_iva", GetType(Double))

        DT2.Columns.Add("Propietario", GetType(String))
        DT2.Columns.Add("Producto", GetType(String))
        DT2.Columns.Add("presentacion", GetType(String))
        DT2.Columns.Add("UMBas", GetType(String))
        DT2.Columns.Add("cantidad", GetType(Double))
        DT2.Columns.Add("cantidad_presentacion", GetType(Double))
        DT2.Columns.Add("fecha", GetType(Date))
        DT2.Columns.Add("codigo", GetType(String))
        DT2.Columns.Add("codigo_barra", GetType(String))
        DT2.Columns.Add("fecha_vence", GetType(Date))
        DT2.Columns.Add("licencia", GetType(String))
        DT2.Columns.Add("Clasificacion", GetType(String))
        DT2.Columns.Add("Familia", GetType(String))

        DT2.Columns.Add("Nombre_Bodega_Origen", GetType(String))
        DT2.Columns.Add("NombreArea", GetType(String))
        DT2.Columns.Add("factor", GetType(Integer))

        DT2.Columns.Add("numero_orden_salida", GetType(String))
        DT2.Columns.Add("codigo_poliza_salida", GetType(String))

        DT2.Columns.Add("regimen_ingreso", GetType(String))
        DT2.Columns.Add("regimen_salida", GetType(String))

    End Sub

    Private Sub frmMovimientosDoc_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
            mnuGuardarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

            vNombreArchivoLayOutGrid = "gridMovimientosDoc.xml"
            AP.Listar_Bodegas_By_Usuario(cmbRegimen)
            cmbRegimen.EditValue = Integer.Parse(AP.IdBodega)
            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbRegimen.EditValue)
            IMS.Listar_Areas_By_Bodega(cmbArea, cmbRegimen.EditValue)

            'SetDatataTable()
            SetDatataTable2()

            '#GT10012023_1200: cealsa no necesariamente requiere usar el area, pero si el régimen (fiscal o general)
            cmbArea.EditValue = 0

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
        cmdActualizar.Enabled = False
        Cargar()
        cmdActualizar.Enabled = True
    End Sub

    Public Property Skip_Saving_Layout As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Private Sub Cargar()

        Skip_Saving_Layout = True
        Dim IdPropietarioBodega As Integer
        IsLoading = True


        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando Movimientos...")

            Dim pCantidad As Double = 0.00
            Dim pValor_dai As Double = 0.00
            Dim pValor_iva As Double = 0.00
            Dim pValor_aduana As Double = 0.00
            Dim cantidad_presentacion As Double = 0.00
            Dim fecha_ingreso As Date = Now

            grdKardex.DataSource = Nothing
            DT2.Clear()

            If cmbPropietario.EditValue <> 0 Then
                'GT 16032021 se obtiene IdPropietarioBodega del combo en lugar de idpropietario
                Dim fila As Object = cmbPropietario.GetSelectedDataRow
                IdPropietarioBodega = fila.Item("IdPropietarioBodega")
            Else
                IdPropietarioBodega = 0
            End If

            pListaMov = New List(Of clsBeVW_Movimientos_Propietario)

            pListaMov = clsLnTrans_movimientos.Get_Movimientos_by_Propietario(cmbRegimen.EditValue, IdPropietarioBodega, cmbArea.EditValue, dtpFechaDel.Value, dtpFechaAl.Value)

            If pListaMov.Count > 0 Then

                For Each BeBWMovimientosPropietario As clsBeVW_Movimientos_Propietario In pListaMov

                    cantidad_presentacion = 0

                    If BeBWMovimientosPropietario.IdTipoTarea = 5 OrElse BeBWMovimientosPropietario.IdTipoTarea = 4 Then
                        pCantidad = (BeBWMovimientosPropietario.cantidad * -1)
                        pValor_aduana = (BeBWMovimientosPropietario.valor_aduana * -1)
                        pValor_dai = (BeBWMovimientosPropietario.valor_dai * -1)
                        pValor_iva = (BeBWMovimientosPropietario.valor_iva * -1)
                    Else
                        pCantidad = BeBWMovimientosPropietario.cantidad
                        pValor_aduana = (BeBWMovimientosPropietario.valor_aduana)
                        pValor_dai = (BeBWMovimientosPropietario.valor_dai)
                        pValor_iva = (BeBWMovimientosPropietario.valor_iva)
                    End If


                    If BeBWMovimientosPropietario.IdPresentacion <> 0 Then
                        cantidad_presentacion = (pCantidad / BeBWMovimientosPropietario.factor)
                    Else
                        cantidad_presentacion = 0
                    End If


                    If BeBWMovimientosPropietario.no_ticket_tms > 0 Then
                        fecha_ingreso = BeBWMovimientosPropietario.fecha_ingreso_ticket
                    Else
                        fecha_ingreso = BeBWMovimientosPropietario.fecha_ingreso_rec
                    End If

                    DT2.Rows.Add(BeBWMovimientosPropietario.TipoTarea,
                                 BeBWMovimientosPropietario.idpedidoenc,
                                 BeBWMovimientosPropietario.iddespachoenc,
                                 BeBWMovimientosPropietario.IdDespachoDet,
                                 BeBWMovimientosPropietario.observacion,
                                 BeBWMovimientosPropietario.no_ticket_tms,
                                 fecha_ingreso,
                                 BeBWMovimientosPropietario.poliza,
                                 BeBWMovimientosPropietario.numero_orden,
                                 BeBWMovimientosPropietario.referencia,
                                 pValor_aduana,
                                 pValor_dai,
                                 pValor_iva,
                                 BeBWMovimientosPropietario.Propietario,
                                 BeBWMovimientosPropietario.Producto,
                                 BeBWMovimientosPropietario.presentacion,
                                 BeBWMovimientosPropietario.UMBas,
                                 pCantidad,
                                 cantidad_presentacion,
                                 BeBWMovimientosPropietario.fecha,
                                 BeBWMovimientosPropietario.codigo,
                                 BeBWMovimientosPropietario.codigo_barra,
                                 BeBWMovimientosPropietario.fecha_vence,
                                 BeBWMovimientosPropietario.licencia,
                                 BeBWMovimientosPropietario.Clasificacion,
                                 BeBWMovimientosPropietario.Familia,
                                 BeBWMovimientosPropietario.Nombre_Bodega_Origen,
                                 BeBWMovimientosPropietario.NombreArea,
                                 BeBWMovimientosPropietario.factor,
                                 BeBWMovimientosPropietario.numero_orden_salida,
                                 BeBWMovimientosPropietario.codigo_poliza_salida,
                                 BeBWMovimientosPropietario.regimen_ingreso,
                                 BeBWMovimientosPropietario.regimen_salida)
                Next

                grdKardex.DataSource = DT2

            End If

            If GridView1.Columns.Count > 0 Then


                GridView1.Columns("cantidad").Caption = "cantidad"

                GridView1.Columns("idpedidoenc").Caption = "Pedido"
                GridView1.Columns("iddespachoenc").Caption = "Despacho"
                GridView1.Columns("IdDespachoDet").Caption = "Despacho detalle"

                GridView1.Columns("valor_aduana").Caption = "Valor Aduana"
                GridView1.Columns("valor_dai").Caption = "Valor DAI"
                GridView1.Columns("valor_iva").Caption = "Valor IVA"

                GridView1.Columns("cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("cantidad").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("cantidad").SummaryItem.SummaryType = SummaryItemType.Sum
                GridView1.Columns("cantidad").SummaryItem.DisplayFormat = "Saldo {0:n6}"

                GridView1.Columns("valor_aduana").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("valor_aduana").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("valor_aduana").SummaryItem.SummaryType = SummaryItemType.Sum
                GridView1.Columns("valor_aduana").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("valor_dai").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("valor_dai").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("valor_dai").SummaryItem.SummaryType = SummaryItemType.Sum
                GridView1.Columns("valor_dai").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("valor_iva").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("valor_iva").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("valor_iva").SummaryItem.SummaryType = SummaryItemType.Sum
                GridView1.Columns("valor_iva").SummaryItem.DisplayFormat = "{0:n6}"

                Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "cantidad",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "Total: {0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("cantidad")}
                GridView1.GroupSummary.Add(item)

                Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "valor_aduana",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "Total: {0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("valor_aduana")}
                GridView1.GroupSummary.Add(item3)

                Dim item4 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "valor_dai",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "Total: {0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("valor_dai")}
                GridView1.GroupSummary.Add(item4)

                Dim item5 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "valor_iva",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "Total: {0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("valor_iva")}
                GridView1.GroupSummary.Add(item5)

                GridView1.ExpandAllGroups()

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                GridView1.BestFitColumns(True)

                Restore_LayOut()

            End If

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            Skip_Saving_Layout = False
            IsLoading = False
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub Guardar_Layout()

        Try

            If IsLoading Then Exit Sub

            Dim Ms As New MemoryStream
            GridView1.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid,
                                                          LayoutToText)

            mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

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

        ' #EJC20260603_ROWSTYLE_PRINT_GUARD: evitar costo de formato por celda durante impresión.
        If clsUiPrintHelper.IsPrintingPreviewInProgress Then Exit Sub

        If e.Column.FieldName = "cantidad" Then

            Dim View As GridView = sender
            Dim TipoT As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("TipoTarea"))

            If TipoT = "DESP" OrElse TipoT = "TRAS" Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            ElseIf TipoT = "AJCANTN" Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            ElseIf TipoT = "AJCANTP" Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                e.Appearance.BackColor = Color.Green
                e.Appearance.BackColor2 = Color.White
            ElseIf TipoT = "RECE" Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                e.Appearance.BackColor = Color.Green
                e.Appearance.BackColor2 = Color.White
            End If

        End If

    End Sub

    Private Sub Restore_LayOut()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGrid)

            If Not BeConfiguracionUsuarioDet Is Nothing Then
                GridView1.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmbRegimen_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbRegimen.KeyDown

        Try

            If e.KeyCode = Keys.Back Then
                cmbRegimen.EditValue = 0
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub cmbPropietario_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbPropietario.KeyDown

        Try

            If e.KeyCode = Keys.Back Then
                cmbPropietario.EditValue = 0
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub cmbArea_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbArea.KeyDown

        Try

            If e.KeyCode = Keys.Back Then
                cmbArea.EditValue = 0
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub fchDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub fchAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaAl.ValueChanged

        Try


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub
    Private Sub cmbRegimen_EditValueChanged(sender As Object, e As EventArgs) Handles cmbRegimen.EditValueChanged
        Try

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbRegimen.EditValue)
            IMS.Listar_Areas_By_Bodega(cmbArea, cmbRegimen.EditValue)

            cmbArea.EditValue = 0

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Sub mnuGuardarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardarLayoutGrid.ItemClick

        Try

            Dim Ms As New MemoryStream
            GridView1.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid,
                                                          LayoutToText)

            XtraMessageBox.Show("Diseño de grid guardado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuEliminarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick
        Try

            'If File.Exists(vNombreArchivoLayOutGrid) Then
            '    File.Delete(vNombreArchivoLayOutGrid)
            '    mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            'End If

            clsLnConfiguracion_usuario_enc.Delete_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid)


            XtraMessageBox.Show("Diseño de grid eliminado!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Close()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                              Text,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridView1_Layout(sender As Object, e As EventArgs) Handles GridView1.Layout
        Guardar_Layout()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        cmdImprimir.Enabled = False
        Imprimir_Vista()
        cmdImprimir.Enabled = True
    End Sub

    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(grdKardex, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
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
            printLink.Component = grdKardex
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

        Dim reportHeader As String = vbNewLine & "Detalle de Movimientos por documento"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

End Class



