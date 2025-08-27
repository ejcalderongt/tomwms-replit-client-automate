Imports System.Reflection
Imports System.Threading
Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class FrmStockList

    Public pIdPropietarioBodega As Integer
    Public pObjStock As clsBeVW_stock_res
    Public listaStockSeleccionado As New List(Of clsBeVW_stock_res)
    Public pListObjDet As New List(Of clsBeTrans_ubic_hh_det)
    Public IdBodega As Integer = 0

    Public threadListar_Stock As New Thread(AddressOf Listar_Stock_With_DT) With {.IsBackground = True}

    ReadOnly CallBindProductos_To_Grid As New MethodInvoker(AddressOf BindProductos_To_Grid)

    Public Property ForceUpdateList As Boolean = True

    Public Property Termino_Carga_De_Datos As Boolean = False

    Dim DTStock As New DataTable
    Dim DTStockForMemory As New DataTable

    Private ReviewGridPres As Boolean = True

    Dim lBePres As New List(Of clsBeProducto_Presentacion)

    Public Property IdEstadoDestino As Integer = 0
    Public Property IdUbicacionDestinto As Integer = 0

    '#GT28102024: validar si mostrar seleccion multiple para cambio de ubicacion y no de estado.
    Public Property Es_Seleccion_Multiple As Boolean

    '#GT28112024: guardar selección multiple, previo a aplicar filtros
    Private selectedKeys As New HashSet(Of Object)()
    Public Property AdvancedMode As Boolean = False

    Public Sub New()
        InitializeComponent()
        Listar_Stock_DesdeHilo()
    End Sub

    Public Property Modo As pModo

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Public Sub BindProductos_To_Grid()

        Try

            If (IsHandleCreated) Then

                SyncLock DGrid

                    DGrid.DataSource = Nothing

                    DGrid.BeginUpdate()

                    Restar_Stock_En_Memoria()

                    DGrid.DataSource = DTStock

                    If grdvStock.Columns.Count > 0 Then
                        grdvStock.BestFitColumns(True)
                    End If

                    DGrid.EndUpdate()

                    lblRegistros.Caption = String.Format("Registros: {0}", grdvStock.RowCount)

                    grdvStock.LayoutChanged()

                    Try

                        If DTStock.Rows.Count > 0 Then

                            grdvStock.Columns("IdBodega").Visible = False
                            grdvStock.Columns("IdPropietario").Visible = False
                            grdvStock.Columns("IdPropietarioBodega").Visible = False
                            grdvStock.Columns("IdProducto").Visible = False
                            grdvStock.Columns("IdProductoBodega").Visible = False
                            grdvStock.Columns("IdUbicacion_anterior").Visible = False
                            grdvStock.Columns("IdUnidadMedida").Visible = False
                            grdvStock.Columns("IdProductoEstado").Visible = False
                            grdvStock.Columns("IdPresentacion").Visible = False
                            grdvStock.Columns("IdRecepcionEnc").Visible = False
                            grdvStock.Columns("IdIndiceRotacion").Visible = False
                            grdvStock.Columns("Alto_ubicacion").Visible = False
                            grdvStock.Columns("Largo_ubicacion").Visible = False
                            grdvStock.Columns("Ancho_ubicacion").Visible = False
                            grdvStock.Columns("alto").Visible = False
                            grdvStock.Columns("largo").Visible = False
                            grdvStock.Columns("ancho").Visible = False
                            grdvStock.Columns("IdTramo").Visible = False
                            grdvStock.Columns("Existencia_min_umbas").Visible = False
                            grdvStock.Columns("Existencia_max_umbas").Visible = False
                            grdvStock.Columns("Existencia_min_pres").Visible = False
                            grdvStock.Columns("Existencia_max_pres").Visible = False
                            grdvStock.Columns("IdUbicacionActual").Visible = False
                            grdvStock.Columns("Ubicacion_Nivel").Visible = False
                            grdvStock.Columns("Ubicacion_Indice_X").Visible = False
                            grdvStock.Columns("Ubicacion_Nombre").Visible = False
                            grdvStock.Columns("Ubicacion_Tramo").Visible = False
                            grdvStock.Columns("Ubicacion_Tramo").Visible = False
                            grdvStock.Columns("IndiceRotacion").Visible = False
                            grdvStock.Columns("IdUbicacion").Visible = False
                            grdvStock.Columns("IdUbicacion").Visible = False

                            '#EJC20171006_0339: Footer totales.
                            Dim item As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Sum, "Cantidad_Presentacion", "Sum={0:n2}")
                            Dim item1 As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Sum, "Cantidad_UMBas", "Sum={0:n2}")
                            Dim item2 As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Sum, "Disponible_UMBas", "Sum={0:n2}")
                            Dim item3 As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Sum, "CantidadReservadaUmBas", "Sum={0:n2}")
                            Dim item4 As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Sum, "Disponible_Presentacion", "Sum={0:n2}")

                            grdvStock.Columns("CantidadReservadaUmBas").Caption = "Reservado U.M.Bas"

                            '#CKFK20220602: Puse esto en comentario porque la cantidad en presentacion no es lo disponible en presentacion
                            'grdvStock.Columns("Cantidad_Presentacion").Caption = "Disponible_Presentación"

                            If grdvStock.Columns("Cantidad_Presentacion").Summary.Count = 0 Then
                                grdvStock.Columns("Cantidad_Presentacion").Summary.Add(item)
                            End If

                            If grdvStock.Columns("Cantidad_UMBas").Summary.Count = 0 Then
                                grdvStock.Columns("Cantidad_UMBas").Summary.Add(item1)
                            End If

                            If grdvStock.Columns("Disponible_UMBas").Summary.Count = 0 Then
                                grdvStock.Columns("Disponible_UMBas").Summary.Add(item2)
                            End If

                            If grdvStock.Columns("CantidadReservadaUmBas").Summary.Count = 0 Then
                                grdvStock.Columns("CantidadReservadaUmBas").Summary.Add(item3)
                            End If

                            grdvStock.Columns("CantidadSF").Visible = False

                            grdvStock.Columns("factor").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            grdvStock.Columns("factor").DisplayFormat.FormatString = "{0:n6}"

                            grdvStock.Columns("factor").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            grdvStock.Columns("factor").DisplayFormat.FormatString = "{0:n6}"

                            grdvStock.Columns("Disponible_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            grdvStock.Columns("Disponible_UMBas").DisplayFormat.FormatString = "{0:n6}"

                            grdvStock.Columns("Cantidad_Presentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            grdvStock.Columns("Cantidad_Presentacion").DisplayFormat.FormatString = "{0:n6}"

                            grdvStock.Columns("Cantidad_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            grdvStock.Columns("Cantidad_UMBas").DisplayFormat.FormatString = "{0:n6}"

                            grdvStock.Columns("CantidadReservadaUmBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            grdvStock.Columns("CantidadReservadaUmBas").DisplayFormat.FormatString = "{0:n6}"

                            '#EJC20230811: No poner esto en comentario, revisen su vista. las columnas son casesensitive y generalmente cuando se edita una vista  o se hace un alter, 
                            'SQL, no respeta las palabras capitalizadas, y las coloca como minúsculas, lo correcto es: que el WMS debería mostrar capitalizadas.,                            
                            grdvStock.Columns("Disponible_Presentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            grdvStock.Columns("Disponible_Presentacion").DisplayFormat.FormatString = "{0:n6}"

                            'GT lanza excepcion!
                            '#EJC20230811: No poner esto en comentario, revisen su vista. las columnas son casesensitive y generalmente cuando se edita una vista  o se hace un alter, 
                            'SQL, no respeta las palabras capitalizadas, y las coloca como minúsculas, lo correcto es: que el WMS debería mostrar capitalizadas.
                            grdvStock.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            grdvStock.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatString = "{0:n6}"

                            '#EJC20171014_1040PM: Footer totales, faltaba count de registros.
                            grdvStock.Columns("IdStock").SummaryItem.SummaryType = SummaryItemType.Count
                            grdvStock.Columns("IdStock").SummaryItem.DisplayFormat = "Count = {0}"

                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
                    End Try

                End SyncLock

            End If

            Try

                grdvStock.OptionsView.ColumnAutoWidth = False
                grdvStock.BestFitColumns()

            Catch ex As Exception
            End Try


            Application.DoEvents()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
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

    Private Sub Restar_Stock_En_Memoria()

        Try

            Dim vCantidadResEnMemoria As Double = 0
            Dim vContadorActualizacion As Double = 0
            Dim vDisponibleMemoria As Double = 0
            Dim Factor As Double = 0
            Dim vIdxPresentacion As Integer = -1
            Dim Presentacion As New clsBeProducto_Presentacion

            If pListObjDet.Count > 0 Then

                For Each S As DataRow In DTStock.Rows

                    If vContadorActualizacion = pListObjDet.Count Then
                        Exit For
                    Else

                        vCantidadResEnMemoria = pListObjDet.FindAll(Function(x) x.IdStock = S.Item("IdStock") And x.Activo = True).Sum(Function(y) y.Cantidad)

                        If pListObjDet.Exists(Function(x) x.IdStock = S.Item("IdStock")) Then

                            Dim qry = From dr As DataRow In DTStockForMemory.AsEnumerable()
                                      Where dr.Field(Of Integer)("IdStock") = S.Item("IdStock")
                                      Select dr

                            If S.Item("IdPresentacion") IsNot DBNull.Value Then

                                If S.Item("IdPresentacion") > 0 Then

                                    Dim vIdPresentacion As Integer = S.Item("IdPresentacion")
                                    Dim IdxPres As Integer = lBePres.FindIndex(Function(x) x.IdPresentacion = vIdPresentacion)

                                    If IdxPres <> -1 Then 'Existe en la lista
                                        Presentacion = lBePres(IdxPres)
                                    Else
                                        Presentacion = New clsBeProducto_Presentacion
                                        Presentacion = clsLnProducto_presentacion.GetSingle(vIdPresentacion)
                                        lBePres.Add(Presentacion)
                                    End If

                                    vCantidadResEnMemoria = Math.Round(Presentacion.Factor * vCantidadResEnMemoria, 6)

                                End If

                            End If

                            If Not qry Is Nothing Then

                                vDisponibleMemoria = qry(0).Item("Cantidad_UMBas")

                                S.Item("Disponible_umbas") = vDisponibleMemoria - vCantidadResEnMemoria

                                If S.Item("IdPresentacion") IsNot DBNull.Value Then
                                    If S.Item("IdPresentacion") > 0 Then
                                        S.Item("Cantidad_Presentacion") = Math.Round(S.Item("Disponible_umbas") / Presentacion.Factor, 6)
                                    End If
                                End If

                            End If

                            vContadorActualizacion += 1

                        End If

                    End If

                Next

            Else

                '#EJC20200204: Que hace esto aquí ???
                'For Each S As DataRow In DTStock.Rows

                '    Presentacion = New clsBeProducto_Presentacion

                '    If S.Item("IdPresentacion") IsNot DBNull.Value Then
                '        If S.Item("IdPresentacion") > 0 Then
                '            Presentacion = clsLnProducto_presentacion.Get_BeProductoPresentacion_By_IdPresentacion(S.Item("IdPresentacion"))
                '            S.Item("Cantidad_Presentacion") = Math.Round(S.Item("Disponible_umbas") / Presentacion.Factor, 6)
                '        End If
                '    End If

                'Next


            End If


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
        'Que raro, proque yo recuerdo que esta lista yo la cargaba y desplegaba el progreso en ese progressbar y no veo código de ese progressbar porningun lado
        'No me asombraría que se haya perdido en alguna de las gracias de sincronización.
    End Sub

    Private Sub Listar_Stock_With_DT()

        Dim clsTransaccion As New clsTransaccion

        Try

            Dim watch As Stopwatch = Stopwatch.StartNew()

            clsTransaccion.Begin_Transaction()

            If ForceUpdateList Then

                If IdBodega = 0 Then
                    DTStock = clsLnStock.Get_All_Stock_DT(AP.IdBodega, pIdPropietarioBodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                Else
                    If Not AdvancedMode Then
                        DTStock = clsLnStock.Get_All_Stock_DT(IdBodega, pIdPropietarioBodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                    Else
                        DTStock = clsLnStock.Get_All_Stock_DT_AM(IdBodega, pIdPropietarioBodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                    End If
                End If

                DTStockForMemory = DTStock.Copy()

            End If

            Restar_Stock_En_Memoria()

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
            Try
                SplashScreenManager.CloseForm(False)
            Catch ex As Exception
            End Try
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles DGrid.DoubleClick

        Try

            If Not chkSeleccionMultiple.Checked Then

                If (grdvStock.RowCount > 0) Then

                    If Modo = pModo.Seleccion Then

                        ReviewGridPres = False

                        Dim Dr As DataRowView = grdvStock.GetFocusedRow
                        pObjStock = clsLnStock.Get_Single_By_IdStock(Dr.Item("IdStock"))
                        If pObjStock IsNot Nothing Then
                            pObjStock.CantidadUmBas = Dr.Item("Disponible_UMBas")
                        End If
                        DialogResult = DialogResult.OK

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles grdvStock.RowStyle

        Try

            grdvStock.OptionsBehavior.Editable = False
            grdvStock.OptionsSelection.EnableAppearanceFocusedCell = False
            grdvStock.FocusRectStyle = DrawFocusRectStyle.RowFocus
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
        Hide()
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

            If IsHandleCreated Then ForceUpdateList = True

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

    Private Sub FrmStockList_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            If Termino_Carga_De_Datos Then
                BeginInvoke(CallBindProductos_To_Grid)
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

    Private Sub FrmStockList_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            If threadListar_Stock.IsAlive Then
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Cargando stock...")
            Else
                Me.Ribbon.Refresh()
                Me.Refresh()
                Application.DoEvents()
            End If

            If Es_Seleccion_Multiple Then
                '#GT16102024: si el cambio de ubicación requiere seleccion multple, mostrar los botones
                chkSeleccionMultiple.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                chkSeleccionMultiple.Checked = False
                mnuTomarSeleccionados.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Else
                chkSeleccionMultiple.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                chkSeleccionMultiple.Checked = False
                mnuTomarSeleccionados.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

            SplashScreenManager.CloseForm(False)

            ReviewGridPres = True

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub grdvStock_RowStyle(sender As Object, e As RowStyleEventArgs) Handles grdvStock.RowStyle

        Try

            grdvStock.OptionsBehavior.Editable = False
            grdvStock.OptionsSelection.EnableAppearanceFocusedCell = False
            grdvStock.FocusRectStyle = DrawFocusRectStyle.RowFocus
            grdvStock.OptionsSelection.EnableAppearanceFocusedRow = True
            grdvStock.OptionsSelection.EnableAppearanceHideSelection = True
            grdvStock.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            grdvStock.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            grdvStock.Appearance.FocusedRow.ForeColor = Color.White
            grdvStock.Appearance.SelectedRow.ForeColor = Color.White
            grdvStock.Appearance.SelectedRow.Options.UseBackColor = True
            grdvStock.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub grdvStock_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles grdvStock.RowCellStyle

        Try

            If e.Column.FieldName = "Disponible_UMBas" OrElse e.Column.FieldName = "Disponible_Presentacion" Then

                If e.CellValue > 0 Then
                    Dim vFontNumero As Font = New Font("Arial monospaced for SAP", 12, FontStyle.Bold)
                    e.Appearance.Font = vFontNumero
                End If

                'e.Appearance.BackColor = Color.SteelBlue

            ElseIf e.Column.FieldName = "Cantidad_Presentacion" Then

                'Dim view As ColumnView = CType(sender, ColumnView)
                'Dim vIdStock As Integer = Val(view.GetRowCellValue(e.RowHandle, "IdStock").ToString())
                'Dim CantidadDisponible As String = Val(view.GetRowCellValue(e.RowHandle, "Disponible_UMBas").ToString())
                'Dim CantidadDisponible As String = Val(view.GetRowCellValue(e.RowHandle, "Disponible_UMBas").ToString())
                'Dim vCantPres As Double = Val(view.GetRowCellValue(e.RowHandle, "Cantidad_Presentacion").ToString())
                'Dim vIdPresentacion As Integer = Val(view.GetListSourceRowCellValue(e.RowHandle, "IdPresentacion").ToString())
                'Dim vNuevaCantPres As Double = 0

                'If vCantPres > 0 Then

                '    If ReviewGridPres Then

                '        Dim IdxPres As Integer = lBePres.FindIndex(Function(x) x.IdPresentacion = vIdPresentacion)
                '        Dim BePres As clsBeProducto_Presentacion

                '        If IdxPres <> -1 Then 'Existe en la lista
                '            BePres = lBePres(IdxPres)
                '        Else
                '            BePres = clsLnProducto_presentacion.GetSingle(vIdPresentacion)
                '            lBePres.Add(BePres)
                '        End If

                '        If BePres.EsPallet Then
                '            vNuevaCantPres = CantidadDisponible / (BePres.Factor * BePres.CamasPorTarima * BePres.CajasPorCama)
                '        Else
                '            vNuevaCantPres = CantidadDisponible / BePres.Factor
                '        End If

                '        view.SetRowCellValue(e.RowHandle, "Cantidad_Presentacion", vNuevaCantPres)

                '    End If

                'End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub grdvStock_CustomRowFilter(sender As Object, e As RowFilterEventArgs) Handles grdvStock.CustomRowFilter

        Try

            Dim view As ColumnView = CType(sender, ColumnView)
            Dim CantidadDisponible As String = Val(view.GetListSourceRowCellValue(e.ListSourceRow, "Disponible_UMBas").ToString())
            'Dim vCantPres As Double = Val(view.GetListSourceRowCellValue(e.ListSourceRow, "Cantidad_Presentacion").ToString())
            'Dim vIdPresentacion As Integer = val(view.GetListSourceRowCellValue(e.ListSourceRow, "IdPresentacion").ToString())
            'Dim vNuevaCantPres As Double = 0

            If CantidadDisponible = 0 Then
                e.Visible = False
                e.Handled = True
            End If

            'If vCantPres > 0 Then

            '    Dim IdxPres As Integer = lBePres.FindIndex(Function(x) x.IdPresentacion = vIdPresentacion)
            '    Dim BePres As clsBeProducto_Presentacion

            '    If IdxPres > 0 Then 'Existe en la lista
            '        BePres = lBePres(IdxPres)
            '    Else
            '        BePres = clsLnProducto_presentacion.GetSingle(vIdPresentacion)
            '        lBePres.Add(BePres)
            '    End If

            '    If BePres.EsPallet Then
            '        vNuevaCantPres = CantidadDisponible / (BePres.Factor * BePres.CamasPorTarima * BePres.CajasPorCama)
            '    Else
            '        vNuevaCantPres = CantidadDisponible / BePres.Factor
            '    End If

            'End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try


    End Sub

    Private Sub FrmStockList_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.Ribbon.Refresh()
        Me.Refresh()
        Application.DoEvents()
    End Sub

    Private Sub chkSeleccionMultiple_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkSeleccionMultiple.CheckedChanged

        Try

            If chkSeleccionMultiple.Checked Then

                If XtraMessageBox.Show("Si habilita la selección múltiple se tomarán las cantidades completas de cada línea. ¿Continuar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    grdvStock.OptionsSelection.MultiSelect = True
                    grdvStock.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect
                    mnuTomarSeleccionados.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                Else
                    chkSeleccionMultiple.Checked = False
                End If

            Else
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

            If selectedRowHandles.Length = 0 Then
                XtraMessageBox.Show("Seleccione al menos un registro",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            Else

                Dim IdStock As Integer = 0
                Dim DisponibleUMBAs As Double = 0
                Dim Aplica As String = False
                '#GT21062022_1623: limpiados objetos para nueva selección multiple
                listaStockSeleccionado = New List(Of clsBeVW_stock_res)

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Llenando lista...")

                For i As Integer = 0 To selectedRowHandles.Length - 1

                    IdStock = IIf(IsDBNull(grdvStock.GetRowCellValue(selectedRowHandles(i), "IdStock")), 0, grdvStock.GetRowCellValue(selectedRowHandles(i), "IdStock"))
                    DisponibleUMBAs = IIf(IsDBNull(grdvStock.GetRowCellValue(selectedRowHandles(i), "Disponible_UMBas")), 0, grdvStock.GetRowCellValue(selectedRowHandles(i), "Disponible_UMBas"))
                    Aplica = IIf(IsDBNull(grdvStock.GetRowCellValue(selectedRowHandles(i), "Aplica")), False, grdvStock.GetRowCellValue(selectedRowHandles(i), "Aplica"))

                    Dim vObjStock = New clsBeVW_stock_res()
                    vObjStock = clsLnStock.Get_Single_By_IdStock(IdStock)
                    listaStockSeleccionado.Add(vObjStock)

                    SplashScreenManager.Default.SetWaitFormCaption("IdStock: " & IdStock)
                    Application.DoEvents()

                Next

                If listaStockSeleccionado.Count > 0 Then
                    DialogResult = DialogResult.OK
                End If

            End If

        Catch ex As Exception

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                             Text,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            SplashScreenManager.CloseForm(False)
        End Try


    End Sub

    '#GT18072025
    Private Sub grdvStock_ColumnFilterChanged(sender As Object, e As EventArgs) Handles grdvStock.ColumnFilterChanged
        SaveSelectedRows()
        RestoreSelectedRows()
    End Sub

    Private Sub SaveSelectedRows()

        Dim selectedRowHandles As Integer() = grdvStock.GetSelectedRows()

        For Each handle As Integer In selectedRowHandles
            If handle >= 0 Then
                Dim key As Object = grdvStock.GetRowCellValue(handle, "IdStock") ' Cambia "ID" a tu columna clave única
                If key IsNot Nothing Then
                    selectedKeys.Add(key)
                End If
            End If
        Next
    End Sub

    Private Sub RestoreSelectedRows()
        grdvStock.ClearSelection()

        For i As Integer = 0 To grdvStock.RowCount - 1
            Dim key As Object = grdvStock.GetRowCellValue(i, "IdStock") ' Cambia "ID" a tu columna clave única
            If key IsNot Nothing AndAlso selectedKeys.Contains(key) Then
                grdvStock.SelectRow(i)
            End If
        Next
    End Sub

End Class