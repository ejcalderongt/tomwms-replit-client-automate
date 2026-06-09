Imports System.IO
Imports System.Reflection
Imports System.Threading
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmExistenciasConReserva

    Public listaStock As New List(Of clsBeVW_stock_res)
    Public threadListar_Stock As New Thread(AddressOf Cargar_Datos) With {.IsBackground = True}
    ReadOnly CallBindProductos_To_Grid As New MethodInvoker(AddressOf BindProductos_To_Grid)
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub frmExistenciasConReserva_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            Try

                AP.Listar_Bodegas_By_Usuario(cmbBodega)

                cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

                IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, cmbBodega.EditValue)

            Catch ex As Exception

            End Try

            'Cargar_Datos()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub
    Public Sub ReportProgress(ByVal pMensaje As String)
        If (IsHandleCreated) Then
            BeginInvoke(Sub() lblProgress.Caption = pMensaje)
        End If
    End Sub

    Public Delegate Sub ChangeLabelDelegate(ByVal pMensaje As String)
    Public Sub ChangeLabelMsg(ByVal pMensaje As String)

        Try
            If (IsHandleCreated) Then
                If (InvokeRequired) Then
                    Dim del As New ChangeLabelDelegate(AddressOf ReportProgress)
                    Invoke(del, pMensaje)
                Else
                    ReportProgress(pMensaje)
                End If
            End If
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub Listar_Encabezado()

        Dim lRow As DataRow

        Try

            listaStock = clsLnStock.Get_All_Stock_By_IdBodega_And_IdPropietario(AddressOf ChangeLabelMsg, cmbBodega.EditValue, cmbPropietarioBodega.EditValue)

            Dim ListaEncabezadoStock = From i In listaStock Group i By Keys = New With {Key i.IdProducto, Key i.Codigo_Producto,
                                                                Key i.Propietario, Key i.Nombre_Producto, Key i.Nombre_Presentacion, Key i.Codigo_Barra,
                                                                Key i.UMBas,
                                                                Key i.BePresentacionProductoEnStock.Factor,
                                                                Key i.Nombre_Clasificacion,
                                                                Key i.Area
                                                                } Into Group
                                       Select New With {.id = Keys.IdProducto,
                                                        .cod = Keys.Codigo_Producto,
                                                        .prop = Keys.Propietario,
                                                        .nom = Keys.Nombre_Producto, .pres = Keys.Nombre_Presentacion, .factor = Keys.Factor,
                                                        .barra = Keys.Codigo_Barra, .um = Keys.UMBas,
                                                        .CantidadUMBas = Group.Sum(Function(x) x.CantidadUmBas),
                                                        .CantidadPresentacion = Group.Sum(Function(x) x.CantidadPresentacion),
                                                        .CantidadReservada = Group.Sum(Function(x) x.CantidadReservadaUMBas),
                                                        .Nombre_clasificacion = Keys.Nombre_Clasificacion,
                                                        .Area = Keys.Area
                                                        }

            If ListaEncabezadoStock IsNot Nothing AndAlso ListaEncabezadoStock.Count > 0 Then

                DsExistenciasConReserva.Encabezado.Clear()

                DsExistenciasConReserva.Encabezado.BeginLoadData()

                For Each Obj In ListaEncabezadoStock

                    lRow = DsExistenciasConReserva.Encabezado.NewRow

                    lRow.Item("IdProducto") = Obj.id
                    lRow.Item("Código") = Obj.cod
                    lRow.Item("Propietario") = Obj.prop
                    lRow.Item("Producto") = Obj.nom
                    lRow.Item("Presentación") = Obj.pres
                    lRow.Item("Código_Barra") = Obj.barra
                    lRow.Item("CantidadUMBas") = Obj.CantidadUMBas
                    lRow.Item("UM_Bas") = Obj.um
                    lRow.Item("CantidadPresentación") = Obj.CantidadPresentacion
                    lRow.Item("Cantidad_Reservada") = Obj.CantidadReservada
                    lRow.Item("Factor") = Obj.factor
                    '#GT11032022: se agrega clasificacion y area
                    lRow.Item("Clasificacion") = Obj.Nombre_clasificacion
                    lRow.Item("Area") = Obj.Area
                    DsExistenciasConReserva.Encabezado.AddEncabezadoRow(lRow)

                    ChangeLabelMsg("Encabezado: " & Obj.cod)

                Next

                DsExistenciasConReserva.Encabezado.EndLoadData()

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

    Private Sub Listar_Detalle()

        Dim lRow As DataRow

        Try

            Dim Ubicacion As New clsBeBodega_ubicacion
            'listaStock = clsLnStock.GetAllStockDetalle()

            Dim ListaEncabezadoStock = From i In listaStock Group i By Keys = New With {Key i.IdProducto, Key i.Codigo_Producto,
                                                                Key i.Propietario, Key i.Nombre_Producto, Key i.Nombre_Presentacion, Key i.Codigo_Barra, Key i.UMBas, Key i.IdProductoBodega, Key i.IdStock, Key i.NomEstado,
                                                                Key i.Serial, Key i.CantidadPresentacion, Key i.CantidadUmBas, Key i.CantidadReservadaUMBas, Key i.Fecha_ingreso, Key i.Fecha_Vence,
                                                                Key i.Lote, Key i.Lic_plate, Key i.IdRecepcionEnc, Key i.IdUbicacion, Key i.Ubicacion_Tramo, Key i.Ubicacion_Nombre, Key i.Ubicacion_Nivel, Key i.LargoUbicacion,
                                                                Key i.BePresentacionProductoEnStock.Factor, Key i.codigo_poliza, Key i.Numero_poliza} Into Group
                                       Select New With {.id = Keys.IdProducto, .cod = Keys.Codigo_Producto, .prop = Keys.Propietario, .nom = Keys.Nombre_Producto, .pres = Keys.Nombre_Presentacion,
                                                        .barra = Keys.Codigo_Barra, .um = Keys.UMBas, Keys.IdStock, .idProdBodega = Keys.IdProductoBodega, .estado = Keys.NomEstado, Keys.Serial,
                                                        .cant_pres = Keys.CantidadPresentacion, .cant_umbas = Keys.CantidadUmBas, .cant_res = Keys.CantidadReservadaUMBas, .fact = Keys.Factor, .fechaing = Keys.Fecha_ingreso, Keys.Fecha_Vence,
                                                         Keys.Lote, .idrec = Keys.IdRecepcionEnc,
                                                        .idubic = Keys.IdUbicacion,
                                                        .tramo = Keys.Ubicacion_Tramo,
                                                        .ubic = Keys.Ubicacion_Nombre,
                                                        .niv = Keys.Ubicacion_Nivel,
                                                        .largo = Keys.LargoUbicacion,
                                                        .codigo_poliza = Keys.codigo_poliza,
                                                        .numero_orden = Keys.Numero_poliza,
                                                        .Licencia = Keys.Lic_plate}

            If ListaEncabezadoStock IsNot Nothing AndAlso ListaEncabezadoStock.Count > 0 Then

                DsExistenciasConReserva.Detalle.Clear()

                DsExistenciasConReserva.Detalle.BeginLoadData()

                For Each Obj In ListaEncabezadoStock

                    Ubicacion.IdUbicacion = Obj.idubic
                    Ubicacion.IdBodega = AP.IdBodega

                    clsLnBodega_ubicacion.GetSingle(Ubicacion)

                    lRow = DsExistenciasConReserva.Detalle.NewRow

                    If Obj.cod = "01008076" Then
                        Debug.Print("Espera")
                    End If

                    lRow.Item("IdProducto") = Obj.id
                    lRow.Item("IdProductoBodega") = Obj.idProdBodega
                    lRow.Item("IdStock") = Obj.IdStock
                    lRow.Item("Codigo") = Obj.cod
                    lRow.Item("Propietario") = Obj.prop
                    lRow.Item("Producto") = Obj.nom
                    lRow.Item("Barra") = Obj.barra
                    lRow.Item("Estado") = Obj.estado
                    lRow.Item("Presentación") = Obj.pres
                    lRow.Item("UMBas") = Obj.um
                    lRow.Item("serial") = Obj.Serial
                    lRow.Item("Cant_Presentacion") = Obj.cant_pres
                    lRow.Item("Cant_UMBas") = Obj.cant_umbas
                    lRow.Item("Cantidad_Reservada") = Obj.cant_res
                    lRow.Item("Fecha_Ingreso") = Obj.fechaing
                    lRow.Item("Fecha_Vence") = Obj.Fecha_Vence
                    lRow.Item("lote") = Obj.Lote
                    lRow.Item("NoRecepcion") = Obj.idrec
                    lRow.Item("IdUbicacion") = Obj.idubic
                    lRow.Item("Tramo") = Obj.tramo
                    lRow.Item("Ubicacion") = Ubicacion.NombreCompleto
                    lRow.Item("nivel") = Obj.niv
                    lRow.Item("largo") = Obj.largo
                    lRow.Item("Disponible_UMBas") = Obj.cant_umbas - Obj.cant_res
                    lRow.Item("Disponible_Presentacion") = IIf(Obj.cant_pres > 0, (Obj.cant_umbas - Obj.cant_res) / Obj.fact, 0)
                    lRow.Item("Factor") = Obj.fact
                    lRow.Item("codigo_poliza") = Obj.codigo_poliza
                    lRow.Item("numero_orden") = Obj.numero_orden
                    lRow.Item("Licencia") = Obj.Licencia

                    DsExistenciasConReserva.Detalle.AddDetalleRow(lRow)

                    ChangeLabelMsg("Detalle: " & Obj.cod)

                Next

                DsExistenciasConReserva.Detalle.EndLoadData()

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


        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormCaption("Cargando stock...")

        Try

            Listar_Encabezado()

            Listar_Detalle()

            DsExistenciasConReserva.Detalle.BeginLoadData()

            DsExistenciasConReserva.Detalle.Disponible_UMBasColumn.ReadOnly = False
            DsExistenciasConReserva.Detalle.Disponible_PresentacionColumn.ReadOnly = False
            DsExistenciasConReserva.Detalle.Cantidad_ReservadaColumn.ReadOnly = False
            DsExistenciasConReserva.Detalle.Cant_PresentacionColumn.ReadOnly = False
            DsExistenciasConReserva.Detalle.Cant_UMBasColumn.ReadOnly = False
            DsExistenciasConReserva.Detalle.FactorColumn.ReadOnly = False

            For Each P In DsExistenciasConReserva.Detalle()

                ChangeLabelMsg("Actualizando_Reservas: " & P.Codigo)

                If P.Codigo = "01008076" Then
                    Debug.Print("Espera")
                End If

                'If P.Cant_Presentacion = 0 Then
                '    P.Cant_Presentacion = 0
                'End If

                If P.Cant_UMBas = 0 Then
                    P.Cant_UMBas = 0
                End If

                If P.Cant_UMBas > 0 Then
                    P.Disponible_UMBas = P.Cant_UMBas - IIf(IsDBNull(P.Cantidad_Reservada), 0, P.Cantidad_Reservada)
                Else
                    P.Disponible_UMBas = 0
                End If

                If P.Cant_Presentacion > 0 Then
                    P.Disponible_Presentacion = IIf(IsDBNull(P.Cant_Presentacion), 0, Math.Round(P.Disponible_UMBas / P.Factor, 6))
                Else
                    P.Disponible_Presentacion = 0
                End If

            Next

            DsExistenciasConReserva.Detalle.EndLoadData()

            DsExistenciasConReserva.Encabezado.BeginLoadData()

            For Each P In DsExistenciasConReserva.Encabezado()

                If P.CantidadPresentación = 0 Then
                    P.Disponible_Presentación = 0
                End If

                If P.CantidadUMBas > 0 Then
                    P.Disponible_UMBas = P.CantidadUMBas - IIf(IsDBNull(P.Cantidad_Reservada), 0, P.Cantidad_Reservada)
                Else
                    P.Disponible_UMBas = 0
                End If

                If P.CantidadPresentación > 0 Then
                    P.Disponible_Presentación = IIf(IsDBNull(P.CantidadPresentación), 0, Math.Round(P.Disponible_UMBas / P.Factor, 6))
                Else
                    P.Disponible_Presentación = 0
                End If

            Next

            DsExistenciasConReserva.Encabezado.EndLoadData()

            If IsHandleCreated Then
                '#EJC20171112_1109PM:
                ' Make asynchronous function call to Form's thread.            
                BeginInvoke(CallBindProductos_To_Grid)
            End If

            If GridView1.RowCount > 0 Then
                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
            End If

            ChangeLabelMsg("Listo")

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub


    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs)

        Try

            GridView1.OptionsBehavior.Editable = False
            GridView1.OptionsSelection.EnableAppearanceFocusedCell = False

            GridView1.FocusRectStyle = DrawFocusRectStyle.RowFocus

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

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar_Datos()
    End Sub

    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(grdExistenciasStock, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True, True, 12)
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
            printLink.Component = grdExistenciasStock
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

    Private Sub grdExistenciasStock_KeyDown(sender As Object, e As KeyEventArgs)
        '#CM_20171120: Combinación de teclado para copiar el código del producto y pasarlo a memoria.
        If e.KeyData = Keys.Control + Keys.T Then

            Dim Dr As DataRowView = GridView1.GetFocusedRow

            Clipboard.SetDataObject(Dr.Item("Propietario"))

        End If

    End Sub

    Private Sub grdExistenciasStock_ViewRegistered(sender As Object, e As ViewOperationEventArgs) Handles grdExistenciasStock.ViewRegistered

        Try

            Dim gridView As GridView = e.View

            '#CM20172410_0238PM: Deshabilita autosize del grid
            gridView.OptionsView.ColumnAutoWidth = False

            gridView.Columns("Estado").GroupIndex = 0

            If gridView.IsDetailView Then

                gridView.Columns("IdProductoBodega").Visible = False
                gridView.Columns("Factor").Visible = False
                gridView.Columns("Producto").Visible = False
                gridView.Columns("IdProducto").Visible = False
                gridView.Columns("Codigo").Visible = False
                gridView.Columns("Propietario").Visible = False
                gridView.Columns("lote").Caption = "Lote"
                gridView.Columns("Estado").Caption = "Estado"
                gridView.Columns("Fecha_Vence").Caption = "Vence"
                gridView.Columns("NoRecepcion").Caption = "Recepción"

            End If

            '#CM20172410_0532PM: Activa footer y muestra totales de columnas para reporte de existencias.
            If gridView.Columns.Count > 0 Then

                gridView.OptionsView.ShowFooter = True

                gridView.Columns("Disponible_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridView.Columns("Disponible_UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                gridView.Columns("Disponible_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gridView.Columns("Disponible_UMBas").DisplayFormat.FormatString = "{0:n6}"

                gridView.Columns("Disponible_Presentacion").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridView.Columns("Disponible_Presentacion").SummaryItem.DisplayFormat = "{0:n6}"

                gridView.Columns("Disponible_Presentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gridView.Columns("Disponible_Presentacion").DisplayFormat.FormatString = "{0:n6}"

                gridView.Columns("Cant_Presentacion").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridView.Columns("Cant_Presentacion").SummaryItem.DisplayFormat = "{0:n6}"

                gridView.Columns("Cant_Presentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gridView.Columns("Cant_Presentacion").DisplayFormat.FormatString = "{0:n6}"

                gridView.Columns("Cant_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridView.Columns("Cant_UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                gridView.Columns("Cant_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gridView.Columns("Cant_UMBas").DisplayFormat.FormatString = "{0:n6}"

                gridView.Columns("Cantidad_Reservada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridView.Columns("Cantidad_Reservada").SummaryItem.DisplayFormat = "{0:n6}"

                gridView.Columns("Cantidad_Reservada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gridView.Columns("Cantidad_Reservada").DisplayFormat.FormatString = "{0:n6}"

                gridView.Columns("Fecha_Ingreso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                gridView.Columns("Fecha_Ingreso").DisplayFormat.FormatString = "G"

                '#CM20171026_1010AM: filtro para detalle de reporte de existencias.
                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim vPresActual As String = IIf(IsDBNull(Dr.Item("Presentación")), "", Dr.Item("Presentación"))
                Dim vUMBas As String = IIf(IsDBNull(Dr.Item("UM_Bas")), "", Dr.Item("UM_Bas"))

                If vPresActual <> "" Then
                    gridView.ActiveFilterCriteria = New DevExpress.Data.Filtering.BinaryOperator("Presentación", vPresActual)
                Else
                    '#EJC20171027_0439AM: Filtrar por UMBAS sin presentacion 
                    gridView.ActiveFilterCriteria = New DevExpress.Data.Filtering.BinaryOperator("UMBas", vUMBas)
                    '#EJC20220317: Filtrar por UMBAS sin presentacion  
                    gridView.ActiveFilterCriteria = New DevExpress.Data.Filtering.BinaryOperator("Presentación", vPresActual)
                End If

                gridView.BestFitColumns()

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

    Public Sub BindProductos_To_Grid()

        Try

            If (IsHandleCreated) Then

                SyncLock grdExistenciasStock

                    grdExistenciasStock.BeginUpdate()

                    GridView1.Columns("Propietario").GroupIndex = 0

                    DsExistenciasConReserva.Detalle.Cantidad_ReservadaColumn.ReadOnly = False
                    DsExistenciasConReserva.Detalle.Cant_PresentacionColumn.ReadOnly = False
                    DsExistenciasConReserva.Detalle.Cant_UMBasColumn.ReadOnly = False
                    DsExistenciasConReserva.Detalle.Disponible_UMBasColumn.ReadOnly = False
                    DsExistenciasConReserva.Detalle.Disponible_PresentacionColumn.ReadOnly = False
                    DsExistenciasConReserva.Detalle.UbicacionColumn.ReadOnly = False

                    DsExistenciasConReserva.Detalle.IdProductoBodegaColumn.ReadOnly = True And Visible = False
                    DsExistenciasConReserva.Detalle.IdProductoColumn.ReadOnly = True And Visible = False
                    DsExistenciasConReserva.Detalle.NoRecepcionColumn.ReadOnly = True And Visible = False
                    DsExistenciasConReserva.Detalle.IdStockColumn.ReadOnly = True And Visible = False
                    DsExistenciasConReserva.Detalle.CodigoColumn.ReadOnly = True And Visible = False
                    DsExistenciasConReserva.Detalle.EstadoColumn.ReadOnly = True
                    DsExistenciasConReserva.Detalle.loteColumn.ReadOnly = True
                    DsExistenciasConReserva.Detalle.Fecha_VenceColumn.ReadOnly = True
                    DsExistenciasConReserva.Detalle.PropietarioColumn.ReadOnly = True
                    DsExistenciasConReserva.Detalle.Cantidad_ReservadaColumn.ReadOnly = True
                    DsExistenciasConReserva.Detalle.Cant_PresentacionColumn.ReadOnly = True
                    DsExistenciasConReserva.Detalle.Cant_UMBasColumn.ReadOnly = True
                    DsExistenciasConReserva.Detalle.Disponible_UMBasColumn.ReadOnly = True
                    DsExistenciasConReserva.Detalle.Disponible_PresentacionColumn.ReadOnly = True
                    DsExistenciasConReserva.Detalle.UbicacionColumn.ReadOnly = True
                    DsExistenciasConReserva.Detalle.nivelColumn.ReadOnly = True
                    DsExistenciasConReserva.Detalle.SerialColumn.ReadOnly = True
                    DsExistenciasConReserva.Detalle.PresentaciónColumn.ReadOnly = True
                    DsExistenciasConReserva.Detalle.IdStockColumn.ReadOnly = True
                    DsExistenciasConReserva.Detalle.largoColumn.ReadOnly = True
                    DsExistenciasConReserva.Detalle.IdUbicacionColumn.ReadOnly = True
                    DsExistenciasConReserva.Detalle.BarraColumn.ReadOnly = True
                    DsExistenciasConReserva.Detalle.UMBasColumn.ReadOnly = True
                    DsExistenciasConReserva.Detalle.NoRecepcionColumn.ReadOnly = True
                    DsExistenciasConReserva.Detalle.TramoColumn.ReadOnly = True

                    grdExistenciasStock.EndUpdate()

                    grdExistenciasStock.ForceInitialize()

                    colCódigo.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

                    If GridView1.Columns.Count > 0 Then
                        GridView1.OptionsView.ShowFooter = True
                    End If

                    Try

                        GridView1.Columns("CantidadUMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("CantidadUMBas").DisplayFormat.FormatString = "{0:n6}"

                        GridView1.Columns("CantidadUMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView1.Columns("CantidadUMBas").SummaryItem.DisplayFormat = "{0:n6}"

                        GridView1.Columns("CantidadPresentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("CantidadPresentación").DisplayFormat.FormatString = "{0:n6}"

                        GridView1.Columns("CantidadPresentación").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView1.Columns("CantidadPresentación").SummaryItem.DisplayFormat = "{0:n6}"

                        GridView1.Columns("Cantidad_Reservada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Cantidad_Reservada").DisplayFormat.FormatString = "{0:n6}"

                        GridView1.Columns("Cantidad_Reservada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView1.Columns("Cantidad_Reservada").SummaryItem.DisplayFormat = "{0:n6}"

                        GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatString = "{0:n6}"

                        GridView1.Columns("Disponible_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView1.Columns("Disponible_UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                        GridView1.Columns("Disponible Presentación").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView1.Columns("Disponible Presentación").SummaryItem.DisplayFormat = "{0:n6}"

                        GridView1.Columns("Disponible Presentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Disponible Presentación").DisplayFormat.FormatString = "{0:n6}"

                        GridView1.Columns("Código_Barra").Caption = "Código Barra"
                        GridView1.Columns("UM_Bas").Caption = "UMBas"
                        GridView1.Columns("Cantidad_Reservada").Caption = "Cantidad Reservada"
                        GridView1.Columns("Disponible_UMBas").Caption = "Disponible UMBas"

                    Catch ex As Exception
                    End Try


                End SyncLock

            End If

            Try

                GridView1.OptionsView.ColumnAutoWidth = False
                GridView1.BestFitColumns()

            Catch ex As Exception
            End Try

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmExistenciasConReserva_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
            'If threadListar_Stock.ThreadState = ThreadState.Stopped OrElse threadListar_Stock.ThreadState = 12 Then
            '    threadListar_Stock = New Thread(AddressOf Cargar_Datos)
            '    threadListar_Stock.Start()
            'End If

            Cargar_Datos()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

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

    Private Sub BarButtonItem1_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Exportar_Grid_A_Excel(grdExistenciasStock, "WMS_ExistenciasConReserva.xlsx")
    End Sub

End Class



