Imports System.IO
Imports System.Reflection
Imports System.Threading
Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraSplashScreen

Public Class frmInventarioEnLinea

    Public pIdPropietarioBodega As Integer
    Public pObjStock As clsBeVW_stock_res
    Public listaStock As New List(Of clsBeVW_stock_res)
    Public listaStockSeleccionado As New List(Of clsBeVW_stock_res)
    Public pListObjDet As New List(Of clsBeTrans_ubic_hh_det)

    Public threadListar_Stock As New Thread(AddressOf Listar_Stock_With_Obj) With {.IsBackground = True}

    ReadOnly CallBindProductos_To_Grid As New MethodInvoker(AddressOf BindProductos_To_Grid)

    Public Property ForceUpdateList As Boolean = False

    Public Property Termino_Carga_De_Datos As Boolean = False

    '#EJC20210716:Guardar LayoutGrid en LotesPorUbi.
    Private vNombreArchivoLayOutGrid As String = ""

    Public Sub New()
        InitializeComponent()
        Init_DataTable()
        'Listar_Stock_DesdeHilo()
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

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
        Dt.Columns.Add(("Ubicacion"), GetType(String))
        Dt.Columns.Add(("Codigo"), GetType(String))
        Dt.Columns.Add(("Barra"), GetType(String))
        Dt.Columns.Add(("Nombre"), GetType(String))
        Dt.Columns.Add(("U.M."), GetType(String))
        Dt.Columns.Add(("Presentacion"), GetType(String))
        Dt.Columns.Add(("Estado"), GetType(String))
        Dt.Columns.Add(("IdRecepción"), GetType(Integer))
        Dt.Columns.Add(("Ingreso"), GetType(Date))
        Dt.Columns.Add(("Lote"), GetType(String))
        Dt.Columns.Add(("Licencia"), GetType(String))
        Dt.Columns.Add(("Serial"), GetType(String))
        Dt.Columns.Add(("Cantidad_Presentacion"), GetType(Double))
        Dt.Columns.Add(("Cantidad_UMBas"), GetType(Double))
        Dt.Columns.Add(("Factor"), GetType(Integer))
        Dt.Columns.Add(("Añada"), GetType(Integer))
        Dt.Columns.Add(("Area"), GetType(String))
        Dt.Columns.Add(("Clasificacion"), GetType(String))
        Dt.Columns.Add(("IdPropietario"), GetType(String))

    End Sub

    Private Is_Loading As Boolean = False

    Public Sub BindProductos_To_Grid()

        Is_Loading = True

        Try

            If (IsHandleCreated) Then

                SyncLock grdvStock

                    grdvStock.DataSource = Nothing

                    grdvStock.BeginUpdate()


                    grdvStock.DataSource = DTStock

                    grdvStock.EndUpdate()

                    GridView1.LayoutChanged()

                    Try

                        Dim item As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Sum, "Cantidad_Presentacion", "Sum={0:n6}")
                        Dim item1 As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Sum, "Cantidad_UMBas", "Sum={0:n6}")

                        GridView1.Columns("IdPropietario").VisibleIndex = 0
                        GridView1.Columns("Propietario").VisibleIndex = 1
                        GridView1.Columns("codigo").VisibleIndex = 2
                        GridView1.Columns("codigo_barra").VisibleIndex = 3
                        GridView1.Columns("nombre").VisibleIndex = 4
                        GridView1.Columns("NomEstado").VisibleIndex = 5
                        GridView1.Columns("Cantidad_UMBas").VisibleIndex = 6
                        GridView1.Columns("UnidadMedida").VisibleIndex = 7
                        GridView1.Columns("Cantidad_Presentacion").VisibleIndex = 8
                        GridView1.Columns("Presentacion").VisibleIndex = 9
                        GridView1.Columns("lote").VisibleIndex = 10
                        GridView1.Columns("lic_plate").VisibleIndex = 11
                        GridView1.Columns("fecha_ingreso").VisibleIndex = 12
                        GridView1.Columns("fecha_vence").VisibleIndex = 13
                        GridView1.Columns("CantidadReservadaUmBas").VisibleIndex = 14
                        GridView1.Columns("Disponible_UMBas").VisibleIndex = 15
                        GridView1.Columns("peso").VisibleIndex = 16
                        GridView1.Columns("dañado").Visible = False
                        GridView1.Columns("factor").Visible = False
                        GridView1.Columns("EstadoUtilizable").Visible = False
                        GridView1.Columns("IdUbicacion").Visible = False
                        GridView1.Columns("serial").Visible = False
                        GridView1.Columns("añada").Visible = False
                        GridView1.Columns("IdIndiceRotacion").Visible = False
                        GridView1.Columns("alto").Visible = False
                        GridView1.Columns("largo").Visible = False
                        GridView1.Columns("ancho").Visible = False
                        GridView1.Columns("IdTramo").Visible = False
                        GridView1.Columns("Ancho_ubicacion").Visible = False
                        GridView1.Columns("Largo_ubicacion").Visible = False
                        GridView1.Columns("IndiceRotacion").Visible = False
                        GridView1.Columns("Existencia_min_umbas").Visible = False
                        GridView1.Columns("Existencia_max_umbas").Visible = False
                        GridView1.Columns("Existencia_min_pres").Visible = False
                        GridView1.Columns("Existencia_max_pres").Visible = False
                        GridView1.Columns("atributo_variante_1").Visible = False
                        GridView1.Columns("IdUbicacionActual").Visible = False
                        GridView1.Columns("Ubicacion_Nivel").Visible = False
                        GridView1.Columns("Ubicacion_Indice_X").Visible = False
                        GridView1.Columns("Ubicacion_Nombre").Visible = False
                        GridView1.Columns("Ubicacion_Tramo").Visible = False
                        GridView1.Columns("costo").Visible = False

                        GridView1.Columns("IdPropietarioBodega").Visible = False
                        GridView1.Columns("IdProducto").Visible = False
                        GridView1.Columns("IdProductoBodega").Visible = False
                        GridView1.Columns("IdStock").Visible = True
                        GridView1.Columns("IdUbicacion_anterior").Visible = False
                        GridView1.Columns("IdUnidadMedida").Visible = False
                        GridView1.Columns("IdProductoEstado").Visible = False
                        GridView1.Columns("IdPresentacion").Visible = False
                        GridView1.Columns("IdRecepcionEnc").Visible = False
                        GridView1.Columns("Alto_ubicacion").Visible = False

                        GridView1.Columns("IdPropietario").Caption = "Cliente"
                        GridView1.Columns("codigo").Caption = "Código"
                        GridView1.Columns("nombre").Caption = "Producto"
                        GridView1.Columns("NomEstado").Caption = "Estado"
                        GridView1.Columns("lic_plate").Caption = "Licencia"
                        GridView1.Columns("CantidadReservadaUmBas").Caption = "Reservado U.M.Bas"

                        GridView1.Columns("Disponible_UMBas").Caption = "Disponible U.M. Bas"
                        GridView1.Columns("Codigo_Poliza").Caption = "Código Póliza"
                        GridView1.Columns("Numero_poliza").Caption = "Número Póliza"
                        GridView1.Columns("doc_ingreso").Caption = "Doc. Ingreso"
                        GridView1.Columns("Bodega").Caption = "Bodega"
                        GridView1.Columns("parametro_personalizado").Caption = "Parametro Personalizado"
                        GridView1.Columns("posiciones").Caption = "Posiciones"
                        GridView1.Columns("valor_unitario").Caption = "Valor Unitario"
                        GridView1.Columns("valor_total").Caption = "Valor Total"

                        If GridView1.Columns("Cantidad_Presentacion").Summary.Count = 0 Then
                            GridView1.Columns("Cantidad_Presentacion").Summary.Add(item)
                        End If

                        If GridView1.Columns("Cantidad_UMBas").Summary.Count = 0 Then
                            GridView1.Columns("Cantidad_UMBas").Summary.Add(item1)
                        End If

                        GridView1.Columns("Cantidad_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Cantidad_UMBas").DisplayFormat.FormatString = "{0:n6}"
                        GridView1.Columns("Cantidad_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView1.Columns("Cantidad_UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                        GridView1.Columns("Cantidad_Presentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Cantidad_Presentacion").DisplayFormat.FormatString = "{0:n6}"
                        GridView1.Columns("Cantidad_Presentacion").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView1.Columns("Cantidad_Presentacion").SummaryItem.DisplayFormat = "{0:n6}"

                        GridView1.Columns("CantidadReservadaUmBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("CantidadReservadaUmBas").DisplayFormat.FormatString = "{0:n6}"
                        GridView1.Columns("CantidadReservadaUmBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView1.Columns("CantidadReservadaUmBas").SummaryItem.DisplayFormat = "{0:n6}"

                        GridView1.Columns("Disponible_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView1.Columns("Disponible_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                        GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatString = "{0:n6}"

                        GridView1.Columns("IdEmbarcador").Visible = False

                        GridView1.Columns("IdStock").SummaryItem.SummaryType = SummaryItemType.Count
                        GridView1.Columns("IdStock").SummaryItem.DisplayFormat = "Count = {0}"

                        GridView1.Columns("Cantidad_Presentacion").Caption = "Disponible Presentacion"

                        If GridView1.Columns.Count > 0 Then
                            If Not ExisteLayOut Then
                                Try
                                    GridView1.OptionsView.ColumnAutoWidth = False
                                    GridView1.BestFitColumns()
                                Catch ex As Exception
                                End Try
                            End If
                        End If

                        lblRegistros.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                    Catch ex As Exception
                        Debug.Print(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), "No es tan grave que de error al formatear columnas..."))
                    End Try

                End SyncLock

                Restore_LayOut()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        Finally
            Is_Loading = False
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

            If listaStock.Count = 0 OrElse ForceUpdateList Then
                listaStock = clsLnStock.Get_All_Stock_By_IdBodega(AddressOf ChangeLabelMsg,
                                                                  clsTransaccion.lConnection,
                                                                  clsTransaccion.lTransaction)
            End If

            Dim vCantReservada As Double = 0
            Dim vCantidadResEnMemoriaUMBas As Double = 0
            Dim vCantidadResEnMemoriaPrese As Double = 0
            Dim BeUbicacionActual As New clsBeBodega_ubicacion
            Dim vCantPres As Double = 0
            Dim vCantUMBas As Double = 0
            'Dim Prese As clsBeProducto_presentacion

            prg.Properties.Maximum = listaStock.Count
            prg.Properties.Step = 1

            '#EJC20171015_1121PM_R01: Restar cantidades reservadas
            If listaStock.Count > 0 Then

                Init_DataTable()

                For Each St In listaStock

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

                        Dt.Rows.Add(St.IdStock,
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
                                    St.Añada,
                                    St.Area,
                                    St.Nombre_Clasificacion)

                    End If

                    prg.PerformStep()

                Next

                prg.EditValue = 0

            End If

            clsTransaccion.Commit_Transaction()

            Termino_Carga_De_Datos = True

            If IsHandleCreated Then
                '#EJC20171112_1109PM:
                ' Make asynchronous function call to Form's thread.            
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

    Dim DTStock As New DataTable

    Private Sub Listar_Stock_With_DT()

        Try
            mnuActualizar.Enabled = False

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Cargando stock...")

            Dim watch As Stopwatch = Stopwatch.StartNew()

            If listaStock.Count = 0 OrElse ForceUpdateList Then
                '#GT16032022: consulta sin bodega, porque se deben cargar ambas (fiscal/general)
                DTStock = New DataTable()
                DTStock = clsLnStock.Get_All_Stock_DT()
            End If

            Termino_Carga_De_Datos = True

            If IsHandleCreated Then
                '#EJC20171112_1109PM:
                ' Make asynchronous function call to Form's thread.            
                BeginInvoke(CallBindProductos_To_Grid)
                ForceUpdateList = False
            End If

            watch.Stop()

            Debug.Print("Tiempo transcurrido: " & watch.Elapsed.TotalSeconds)

            SplashScreenManager.CloseForm(False)

            mnuActualizar.Enabled = OpcionesMenu.Leer

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Try
                SplashScreenManager.CloseForm(False)
            Catch ex As Exception
            End Try
        End Try

    End Sub

    Public Sub Listar_Stock_DesdeHilo()

        Try


            'Dim watch As Stopwatch = Stopwatch.StartNew()

            'If threadListar_Stock.ThreadState = ThreadState.Stopped OrElse threadListar_Stock.ThreadState = 12 Then
            '    threadListar_Stock = New Thread(AddressOf Listar_Stock_With_DT)
            '    threadListar_Stock.Start()
            'End If

            'watch.Stop()

            'Debug.Print("Tiempo transcurrido: " & watch.Elapsed.TotalSeconds)


            If Not BW.IsBusy Then
                BW.RunWorkerAsync()
            End If

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
            clsUiPrintHelper.PrintGridPreview(grdvStock, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
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
            printLink.Component = grdvStock
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Inventario en Linea"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub frmInventarioEnLinea_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
            mnuGuardarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

            vNombreArchivoLayOutGrid = "frmInventarioEnLinea.xml"

            '#GT29032022: para evitar el multitreading 
            CheckForIllegalCrossThreadCalls = False

            If threadListar_Stock.IsAlive Then
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Cargando stock...")
            End If

            '#GT07032022_0954: se usa una función definida en stock_especifico para guardar layout
            Listar_Stock_DesdeHilo()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Try

            mnuActualizar.Enabled = False

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando Stock...")

            If IsHandleCreated Then
                ForceUpdateList = True
            End If

            If Not BW.IsBusy Then
                BW.RunWorkerAsync()
            End If

            'Listar_Stock_DesdeHilo()

            mnuActualizar.Enabled = False

        Catch ex As Exception
            'mnuActualizar.Enabled = True
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
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
    Private Sub cmdExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdExcel.ItemClick

        cmdExcel.Enabled = False
        Exportar_Grid_A_Excel(grdvStock, "WMS_InventarioEnLinea.xlsx")
        cmdExcel.Enabled = True

    End Sub

    Private ExisteLayOut As Boolean = False
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

            mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always


            XtraMessageBox.Show("Diseño de grid guardado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridView1_Layout(sender As Object, e As EventArgs) Handles GridView1.Layout

        If Is_Loading Then Exit Sub

        Guardar_Layout()

    End Sub

    Private Sub Guardar_Layout()

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

    Private Sub BW_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BW.DoWork

        Try

            Listar_Stock_With_DT()

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



