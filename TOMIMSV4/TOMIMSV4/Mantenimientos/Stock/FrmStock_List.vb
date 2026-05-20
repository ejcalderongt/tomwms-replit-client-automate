Imports System.IO
Imports System.Reflection
Imports System.Threading
Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class FrmStock_List

    Public Property IdPropietarioBodega As Integer

    Private IdBodega As Integer
    Public pSingleBEVWStockRes As clsBeVW_stock_res
    Public listaBeVWSstockRes As New List(Of clsBeVW_stock_res)
    Public listSeleccionObjVWStockRes As New List(Of clsBeVW_stock_res)
    Public pListObjDet As New List(Of clsBeTrans_ubic_hh_det)
    Public threadListar_Stock As New Thread(AddressOf Listar_Stock_With_Obj) With {.IsBackground = True}
    ReadOnly CallBindProductos_To_Grid As New MethodInvoker(AddressOf BindProductos_To_Grid)
    Public Property ForceUpdateList As Boolean = False
    Public Property Termino_Carga_De_Datos As Boolean = False
    Public Property SeleccionMultiple As Boolean = False

    '#GT21112022_0900: cargo el tipo de ajuste, para confirmar si producto tiene esa propiedad
    Public Property varTipoAjuste As pTipoAjuste

    Public Enum pTipoAjuste

        Lote = 1
        Vencimiento = 2
        Cantidad = 3

    End Enum

    Public Sub New(ByVal pIdBodega As Integer, ByVal pIdPropietarioBodega As Integer)
        InitializeComponent()
        Init_DataTable()
        Listar_Stock_DesdeHilo()
        IdBodega = pIdBodega
        IdPropietarioBodega = pIdPropietarioBodega
    End Sub

    Public Property Modo As pModo

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Dt As New DataTable("Stock")

    Private Sub Init_DataTable()

        Dt = New DataTable("Stock")
        Dt.Columns.Add(("IdStock"), GetType(Integer))
        Dt.Columns.Add(("IdPb"), GetType(Integer))
        Dt.Columns.Add(("IdUbic"), GetType(Integer))
        Dt.Columns.Add(("Ubicacion"), GetType(String)) '*
        Dt.Columns.Add(("Codigo"), GetType(String)) '#EJC2017090905
        Dt.Columns.Add(("Barra"), GetType(String)) '#EJC2017090906
        Dt.Columns.Add(("Nombre"), GetType(String))
        Dt.Columns.Add(("U.M."), GetType(String))
        Dt.Columns.Add(("Presentacion"), GetType(String))
        Dt.Columns.Add(("Estado"), GetType(String)) '#EJC20171014_12:17AM: PLGP! NO PUEDE SER QUE ESTE LISTADO NO TUVIERA ESTADO!
        Dt.Columns.Add(("IdRecepción"), GetType(Integer))
        Dt.Columns.Add(("Ingreso"), GetType(Date))
        Dt.Columns.Add(("Lote"), GetType(String))
        Dt.Columns.Add(("Licencia"), GetType(String)) '#EJC2017090907
        Dt.Columns.Add(("Serial"), GetType(String))
        Dt.Columns.Add(("Cantidad_Presentacion"), GetType(Double)) '#EJC2017090908
        Dt.Columns.Add(("Cantidad_UMBas"), GetType(Double)) '#EJC2017090909
        Dt.Columns.Add(("Factor"), GetType(Integer))
        Dt.Columns.Add(("Añada"), GetType(Integer))

    End Sub

    Private IsLoading As Boolean = False
    Public Sub BindProductos_To_Grid()

        IsLoading = True

        Try

            If (IsHandleCreated) Then

                SyncLock DGrid

                    DGrid.DataSource = Nothing

                    DGrid.BeginUpdate()

                    DGrid.DataSource = DTStock

                    If grdvStock.Columns.Count > 0 Then
                        grdvStock.BestFitColumns(True)
                    End If

                    DGrid.EndUpdate()

                    lblRegistros.Caption = String.Format("Registros: {0}", grdvStock.RowCount)

                    grdvStock.LayoutChanged()

                    Try

                        If grdvStock.Columns.Count > 1 Then

                            grdvStock.Columns("IdPropietario").Visible = False
                            grdvStock.Columns("IdPropietarioBodega").Visible = False
                            grdvStock.Columns("IdProducto").Visible = False
                            grdvStock.Columns("IdProductoBodega").Visible = False
                            grdvStock.Columns("IdUbicacion_anterior").Visible = False
                            grdvStock.Columns("IdUnidadMedida").Visible = False
                            grdvStock.Columns("IdProductoEstado").Visible = False
                            grdvStock.Columns("IdPresentacion").Visible = False
                        End If

                        '#EJC20171006_0339: Footer totales.
                        Dim item As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Sum, "Cantidad_Presentacion", "Sum={0:n2}")
                        Dim item1 As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Sum, "Cantidad_UMBas", "Sum={0:n2}")

                        grdvStock.Columns("CantidadReservadaUmBas").Caption = "Reservado U.M.Bas"

                        If grdvStock.Columns("Cantidad_Presentacion").Summary.Count = 0 Then
                            grdvStock.Columns("Cantidad_Presentacion").Summary.Add(item)
                        End If

                        If grdvStock.Columns("Cantidad_UMBas").Summary.Count = 0 Then
                            grdvStock.Columns("Cantidad_UMBas").Summary.Add(item1)
                        End If

                        grdvStock.Columns("Cantidad_Presentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        grdvStock.Columns("Cantidad_Presentacion").DisplayFormat.FormatString = "{0:n2}"

                        grdvStock.Columns("Cantidad_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        grdvStock.Columns("Cantidad_UMBas").DisplayFormat.FormatString = "{0:n2}"

                        grdvStock.Columns("CantidadReservadaUmBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        grdvStock.Columns("CantidadReservadaUmBas").DisplayFormat.FormatString = "{0:n2}"

                        '#EJC20171014_1040PM: Footer totales, faltaba count de registros.
                        grdvStock.Columns("IdStock").SummaryItem.SummaryType = SummaryItemType.Count
                        grdvStock.Columns("IdStock").SummaryItem.DisplayFormat = "Count = {0}"

                    Catch ex As Exception
                        Debug.Print(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), "No es tan grave que de error al formatear columnas..."))
                    End Try

                End SyncLock

            End If

            Restore_LayOut_Grid()

            Try

                If Not ExisteLayOut Then
                    grdvStock.OptionsView.ColumnAutoWidth = False
                    grdvStock.BestFitColumns()
                End If

            Catch ex As Exception
            End Try

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        Finally
            IsLoading = False
        End Try

    End Sub

    Public Delegate Sub ChangeLabelDelegate(ByVal pMensaje As String)

    Public Sub ReportProgress(ByVal pMensaje As String)
        If (IsHandleCreated) Then
            BeginInvoke(Sub() lblProgress.Caption = pMensaje)
        End If
    End Sub

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
    Private Sub Listar_Stock_With_Obj()

        Dim clsTransaccion As New clsTransaccion

        Try

            Dim watch As Stopwatch = Stopwatch.StartNew()

            clsTransaccion.Begin_Transaction()

            If listaBeVWSstockRes.Count = 0 OrElse ForceUpdateList Then
                listaBeVWSstockRes = clsLnStock.Get_All_Stock_By_IdBodega_And_IdPropietario(AddressOf ChangeLabelMsg, IdBodega, IdPropietarioBodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            End If

            Dim vCantReservada As Double = 0
            Dim vCantidadResEnMemoriaUMBas As Double = 0
            Dim vCantidadResEnMemoriaPrese As Double = 0
            Dim BeUbicacionActual As New clsBeBodega_ubicacion
            Dim vCantPres As Double = 0
            Dim vCantUMBas As Double = 0
            'Dim Prese As clsBeProducto_presentacion

            prg.Properties.Maximum = listaBeVWSstockRes.Count
            prg.Properties.Step = 1

            '#EJC20171015_1121PM_R01: Restar cantidades reservadas
            If listaBeVWSstockRes.Count > 0 Then

                Init_DataTable()

                For Each St In listaBeVWSstockRes

                    Debug.Print("Procesando: " & St.Codigo_Producto)

                    ChangeLabelMsg("Procesando: " & St.Codigo_Producto)

                    'EJC20180125: Se comentarió porque se obtiene la cantidad reservada en la vista, me queda la duda de cuando la presentación es pallet si la vista funciona.
                    'vCantReservada = clsLnStock_res.GetCantidadReservadaByIdStock(St.IdStock, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                    vCantReservada = St.CantidadReservadaUMBas

                    If St.CantidadUmBas = 0 Then
                        Debug.Print("¿Porqué no se cargó la cantidad en umbas 1?")
                    End If

                    If St.IdPresentacion <> 0 Then
                        St.CantidadPresentacion = St.CantidadPresentacion - vCantReservada
                    Else
                        St.CantidadUmBas = St.CantidadUmBas - vCantReservada
                    End If

                    If St.CantidadUmBas = 0 Then
                        Debug.Print("¿Por qué no se cargó la cantidad en umbas 2?")
                        Debug.Print(" - Sí se cargó, pero se le restó la cantidad reservada")
                        Debug.Print(" Uf, que bueno...")
                    End If

                    If pListObjDet.Count > 0 Then

                        vCantidadResEnMemoriaUMBas = 0 'Hay que limpiar las variables.
                        vCantidadResEnMemoriaPrese = 0

                        pListObjDet.FindAll(Function(x) x.IdStock = St.IdStock).ForEach(Sub(y)
                                                                                            If y.ProductoPresentacion.IdPresentacion <> 0 Then
                                                                                                '#EJC_20180125: En comentario, obtener factor desde consulta sql al inicio para optimizar.
                                                                                                '#CM_20171115: Resta la cantidad reservada a la cantidad de presentación y multiplica el factor para la cantidad de umbas.
                                                                                                'Prese = clsLnProducto_presentacion.GetSingle(y.ProductoPresentacion.IdPresentacion, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                                                                                                vCantidadResEnMemoriaPrese += y.Cantidad
                                                                                                vCantidadResEnMemoriaUMBas += y.Cantidad * St.Factor 'Prese.Factor
                                                                                            Else
                                                                                                vCantidadResEnMemoriaUMBas += y.Cantidad
                                                                                            End If
                                                                                        End Sub
                                                                                                                             )

                        'vCantidadResEnMemoriaUMBas = pListObjDet.FindAll(Function(x) x.IdStock = St.IdStock).Sum(Function(y) y.Cantidad)
                        St.CantidadUmBas -= vCantidadResEnMemoriaUMBas
                        St.CantidadPresentacion -= vCantidadResEnMemoriaPrese


                    End If

                    If St.CantidadUmBas = 0 Then
                        'listaStock.Remove(St)
                        Debug.Print("No lo puedo eliminar por una condición de competencia, sí otro hilo está a la espera del recurso y lo eliminó se crea un deadlock ")
                    Else

                        '#EJC20180125: Llenar valores de ubicación desde consulta por optimización
                        BeUbicacionActual.IdUbicacion = St.IdUbicacion
                        BeUbicacionActual.Nivel = St.Ubicacion_Nivel
                        BeUbicacionActual.Indice_x = St.Ubicacion_Indice_x
                        BeUbicacionActual.IdTramo = St.IdTramo
                        BeUbicacionActual.Tramo = New clsBeBodega_tramo
                        BeUbicacionActual.Tramo.IdTramo = St.IdTramo
                        BeUbicacionActual.Tramo.Descripcion = St.Ubicacion_Tramo
                        BeUbicacionActual.Descripcion = St.Ubicacion_Nombre

                        '#EJC201709F001
                        If St.IdPresentacion <> 0 Then
                            vCantUMBas = St.CantidadUmBas
                            vCantPres = St.CantidadPresentacion
                        Else
                            vCantPres = 0
                            vCantUMBas = St.CantidadUmBas '- Obj.CantidadReservadaUMBas
                        End If

                        Dt.Rows.Add(
                                    St.IdStock,
                                    St.IdProductoBodega,
                                    St.IdUbicacion,
                                    BeUbicacionActual.NombreCompleto,
                                    St.Codigo_Producto,
                                    St.Codigo_Barra,
                                    St.Nombre_Producto,
                                    St.UMBas,
                                    St.Nombre_Presentacion,
                                    St.NomEstado,
                                    St.IdRecepcionEnc,
                                    St.Fecha_ingreso,
                                    St.Lote,
                                    St.Lic_plate,
                                    St.Serial,
                                    vCantPres,
                                    vCantUMBas,
                                    St.Factor,
                                    St.Añada)

                    End If

                    prg.PerformStep()

                Next

                prg.EditValue = 0

            End If

            clsTransaccion.Commit_Transaction()

            Termino_Carga_De_Datos = True

            If IsHandleCreated Then
                BeginInvoke(CallBindProductos_To_Grid)
                ForceUpdateList = False
            End If

            watch.Stop()

            Debug.Print("Tiempo transcurrido: " & watch.Elapsed.TotalSeconds)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    Dim DTStock As New DataTable

    Private Sub Listar_Stock_With_DT()

        Dim clsTransaccion As New clsTransaccion

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormCaption("Cargando stock...")

        Try

            Dim watch As Stopwatch = Stopwatch.StartNew()

            If listaBeVWSstockRes.Count = 0 OrElse ForceUpdateList Then
                DTStock = clsLnStock.Get_All_Stock_DT(AP.IdBodega,
                                                      IdPropietarioBodega)
            End If

            Termino_Carga_De_Datos = True

            If IsHandleCreated Then
                BeginInvoke(CallBindProductos_To_Grid)
                ForceUpdateList = False
            End If

            watch.Stop()

            Debug.Print("Tiempo transcurrido: " & watch.Elapsed.TotalSeconds)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles DGrid.DoubleClick

        Dim AplicaAjustePorPropiedadProducto As Boolean = False
        Dim BeProducto As New clsBeProducto
        Dim vIdStock As Integer = 0

        Try

            If (grdvStock.RowCount > 0) Then

                If Modo = pModo.Seleccion Then

                    Dim Dr As DataRowView = grdvStock.GetFocusedRow
                    vIdStock = IIf(IsDBNull(Dr.Item("IdStock")), 0, Dr.Item("IdStock"))

                    pSingleBEVWStockRes = clsLnStock.Get_Single_By_IdStock(vIdStock)
                    pSingleBEVWStockRes.CantidadUmBas = IIf(IsDBNull(Dr.Item("Cantidad_UMBas")), 0, Dr.Item("Cantidad_UMBas"))

                    If Not pSingleBEVWStockRes Is Nothing Then

                        BeProducto = clsLnProducto.Get_Single_By_IdProducto(pSingleBEVWStockRes.IdProducto)

                        If Not BeProducto Is Nothing Then

                            If varTipoAjuste = pTipoAjuste.Lote AndAlso Not BeProducto.Control_lote Then
                                Throw New Exception("ERROR_202211211256A: No se puede aplicar ajuste por lote, porque el producto no tiene habilitado el control por lote.")
                            ElseIf varTipoAjuste = pTipoAjuste.Vencimiento AndAlso Not BeProducto.Control_vencimiento Then
                                Throw New Exception("ERROR_202211211256B: No se puede aplicar ajuste por fecha de vencimiento, porque el producto no tiene habilitado el control por vencimiento.")
                            Else
                                AplicaAjustePorPropiedadProducto = True
                            End If

                        Else
                            Throw New Exception("ERROR_202211211302: No se pudo obtener el objeto de producto asociado al identificador: " & pSingleBEVWStockRes.IdProducto)
                        End If

                        If AplicaAjustePorPropiedadProducto Then
                            DialogResult = DialogResult.OK
                        Else
                            DialogResult = DialogResult.Cancel
                        End If

                    Else

                        Throw New Exception("ERROR_202211211300: No se pudo obtener el objeto de stock asociado al identificador: " & vIdStock)

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grdvStock.RowStyle

        Try

            grdvStock.OptionsBehavior.Editable = False
            grdvStock.OptionsSelection.EnableAppearanceFocusedCell = False
            grdvStock.FocusRectStyle = Views.Grid.DrawFocusRectStyle.RowFocus
            grdvStock.OptionsSelection.EnableAppearanceFocusedRow = True
            grdvStock.OptionsSelection.EnableAppearanceHideSelection = True
            grdvStock.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            grdvStock.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            grdvStock.Appearance.FocusedRow.ForeColor = Color.White
            grdvStock.Appearance.SelectedRow.ForeColor = Color.White
            grdvStock.Appearance.SelectedRow.Options.UseBackColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub mnuFinalizarSeleccion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        DialogResult = DialogResult.OK
    End Sub

    Public Sub Listar_Stock_DesdeHilo()

        Try

            Dim watch As Stopwatch = Stopwatch.StartNew()

            If threadListar_Stock.ThreadState = ThreadState.Stopped OrElse threadListar_Stock.ThreadState = 12 Then
                threadListar_Stock = New Thread(AddressOf Listar_Stock_With_DT)
                threadListar_Stock.Start()
            End If

            watch.Stop()

            Debug.Print("Tiempo transcurrido: " & watch.Elapsed.TotalSeconds)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Try

            If IsHandleCreated Then

                'If XtraMessageBox.Show("¿Listar de nuevo el stock?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                '    ForceUpdateList = True
                'Else
                '    ForceUpdateList = False
                'End If
                ForceUpdateList = True

            End If

            Listar_Stock_DesdeHilo()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
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
            printLink.Component = DGrid
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Stock"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub FrmStock_List_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            '#EJC20210716:Restaurar LayoutGrid en LotesPorUbi.
            vNombreArchivoLayOutGrid = "frmstocklistajuste.xml"

            Listar_Stock_DesdeHilo()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try


    End Sub

    Private Sub FrmStock_List_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        'If threadListar_Stock.IsAlive Then
        '    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        '    SplashScreenManager.Default.SetWaitFormCaption("Cargando stock...")
        'End If

        'If Termino_Carga_De_Datos Then
        '    BeginInvoke(CallBindProductos_To_Grid)
        'End If

    End Sub

    Private Sub chkSeleccionMultiple_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkSeleccionMultiple.CheckedChanged

        Try

            If chkSeleccionMultiple.Checked Then

                If XtraMessageBox.Show("Si habilita la selección múltiple, los registros tomaran el mismo tipo de ajuste. ¿Continuar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    grdvStock.OptionsSelection.MultiSelect = True
                    grdvStock.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect
                    mnuTomarSeleccionados.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                Else
                    chkSeleccionMultiple.Checked = False
                End If

            Else
                SeleccionMultiple = False
                chkSeleccionMultiple.Checked = False
                grdvStock.OptionsSelection.MultiSelect = False
                grdvStock.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect
                mnuTomarSeleccionados.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If


        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                             Text,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuTomarSeleccionados_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuTomarSeleccionados.ItemClick

        Try

            Dim selectedRowHandles As Integer() = grdvStock.GetSelectedRows()
            listSeleccionObjVWStockRes = New List(Of clsBeVW_stock_res)

            If selectedRowHandles.Length = 0 Then
                XtraMessageBox.Show("Seleccione al menos un registro",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            Else

                Dim IdStock As Integer = 0
                Dim DisponibleUMBAs As Double = 0
                Dim Aplica As String = False

                For i As Integer = 0 To selectedRowHandles.Length - 1

                    IdStock = grdvStock.GetRowCellValue(selectedRowHandles(i), "IdStock")

                    '#GT1600: quién hizo el favor de setear DisponibleUMBAs con IdStock ??
                    'DisponibleUMBAs = grdvStock.GetRowCellValue(selectedRowHandles(i), "IdStock")
                    DisponibleUMBAs = grdvStock.GetRowCellValue(selectedRowHandles(i), "Cantidad_UMBas")

                    pSingleBEVWStockRes = New clsBeVW_stock_res()
                    pSingleBEVWStockRes = clsLnStock.Get_Single_By_IdStock(IdStock)
                    pSingleBEVWStockRes.CantidadUmBas = DisponibleUMBAs

                    listSeleccionObjVWStockRes.Add(pSingleBEVWStockRes)

                Next

                If listSeleccionObjVWStockRes.Count > 0 Then

                    SeleccionMultiple = True
                    DialogResult = DialogResult.OK

                End If

            End If


        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                             Text,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try


    End Sub

    Private vNombreArchivoLayOutGrid As String = ""
    Private Sub GridView1_Layout(sender As Object, e As EventArgs) Handles grdvStock.Layout
        Guardar_Layout()
    End Sub

    Private Sub Guardar_Layout()

        If IsLoading Then Exit Sub

        Try

            If IsLoading Then Exit Sub

            Dim Ms As New MemoryStream
            grdvStock.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid,
                                                          LayoutToText)

            mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
        Text,
        MessageBoxButtons.OK,
        MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try


    End Sub

    Private ExisteLayOut As Boolean = False
    Private Sub Restore_LayOut_Grid()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGrid)

            If Not BeConfiguracionUsuarioDet Is Nothing Then
                grdvStock.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub mnuEliminarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick

        Try

            clsLnConfiguracion_usuario_enc.Delete_Layout(AP.IdEmpresa,
                                                         AP.UsuarioAp.IdUsuario,
                                                         AP.HostName,
                                                         vNombreArchivoLayOutGrid)


            XtraMessageBox.Show("Diseño de grid eliminado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Private Sub DGrid_Click(sender As Object, e As EventArgs) Handles DGrid.Click

    End Sub

    'Private Sub mnuEliminarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick

    '    Try

    '        If File.Exists(vNombreArchivoLayOutGrid) Then
    '            File.Delete(vNombreArchivoLayOutGrid)
    '            mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
    '        End If

    '        XtraMessageBox.Show("Diseño de grid eliminado!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

    '        Close()

    '    Catch ex As Exception

    '        XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
    '                          Text,
    '                          MessageBoxButtons.OK,
    '                          MessageBoxIcon.Error)

    '        Dim vMsgError As String = ex.Message
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)

    '    End Try

    'End Sub

End Class