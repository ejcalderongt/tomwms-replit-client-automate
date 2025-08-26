Imports System.ComponentModel
Imports System.IO
Imports System.Reflection
Imports System.Threading
Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraSplashScreen

Public Class frmStock_Especifico_List

    Public pIdPropietarioBodega As Integer
    Public pObjStock As clsBeVW_stock_res
    Public Property SeleccionMultiple As Boolean = False
    Public listaStockSeleccionado As New List(Of clsBeVW_stock_res)
    Public pListObjDet As New List(Of clsBeTrans_ubic_hh_det)
    Public IdBodega As Integer = 0
    Public IdCliente As Integer = 0
    Private TieneTiempos As Boolean = False

    Public threadListar_Stock As New Thread(AddressOf Listar_Stock_With_DT) With {.IsBackground = True}

    ReadOnly CallBindProductos_To_Grid As New MethodInvoker(AddressOf BindProductos_To_Grid)

    Public Property ForceUpdateList As Boolean = True

    Public Property Termino_Carga_De_Datos As Boolean = False

    Dim DTStock As New DataTable
    Dim DTStockForMemory As New DataTable
    Public Property ProductoEspecifico As New clsBeProducto

    Private lPresentacion As New List(Of clsBeProducto_Presentacion)

    Public BuscarPoliza As Boolean = False

    Public IdProductoEstadoDefault As Integer = 0

    Public Sub New()
        InitializeComponent()
        'Listar_Stock_DesdeHilo()
    End Sub

    Public Property Modo As pModo

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private IsLoading As Boolean = False

    Public Sub BindProductos_To_Grid()

        IsLoading = True

        Try

            If (IsHandleCreated) Then

                SyncLock grdStock

                    grdStock.DataSource = Nothing

                    grdStock.BeginUpdate()

                    Restar_Stock_En_Memoria()

                    grdStock.DataSource = DTStock
                    grdStock.RefreshDataSource()
                    grdStock.EndUpdate()

                    lblRegistros.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                    Restore_LayOut()

                    If GridView1.Columns.Count > 0 Then
                        GridView1.BestFitColumns(True)
                    End If

                    Try

                        If DTStock.Rows.Count > 0 Then

                            GridView1.Columns("IdBodega").Visible = False
                            GridView1.Columns("IdPropietario").Visible = False
                            GridView1.Columns("IdPropietarioBodega").Visible = False
                            GridView1.Columns("IdProducto").Visible = False
                            GridView1.Columns("IdProductoBodega").Visible = False
                            GridView1.Columns("IdUbicacion_anterior").Visible = False
                            GridView1.Columns("IdUnidadMedida").Visible = False
                            GridView1.Columns("IdProductoEstado").Visible = False
                            GridView1.Columns("IdPresentacion").Visible = False
                            GridView1.Columns("IdRecepcionEnc").Visible = False
                            GridView1.Columns("IdIndiceRotacion").Visible = False
                            GridView1.Columns("Alto_ubicacion").Visible = False
                            GridView1.Columns("Largo_ubicacion").Visible = False
                            GridView1.Columns("Ancho_ubicacion").Visible = False
                            GridView1.Columns("alto").Visible = False
                            GridView1.Columns("largo").Visible = False
                            GridView1.Columns("ancho").Visible = False
                            GridView1.Columns("IdTramo").Visible = False
                            GridView1.Columns("Existencia_min_umbas").Visible = False
                            GridView1.Columns("Existencia_max_umbas").Visible = False
                            GridView1.Columns("Existencia_min_pres").Visible = False
                            GridView1.Columns("Existencia_max_pres").Visible = False
                            GridView1.Columns("IdUbicacionActual").Visible = False
                            GridView1.Columns("Ubicacion_Nivel").Visible = False
                            GridView1.Columns("Ubicacion_Indice_X").Visible = False
                            GridView1.Columns("Ubicacion_Nombre").Visible = False
                            GridView1.Columns("Ubicacion_Tramo").Visible = False
                            GridView1.Columns("Ubicacion_Tramo").Visible = False
                            GridView1.Columns("IndiceRotacion").Visible = False
                            GridView1.Columns("IdUbicacion").Visible = False
                            GridView1.Columns("IdUbicacion").Visible = False

                            If Not GridView1.Columns.ColumnByFieldName("IdClasificacion") Is Nothing Then
                                GridView1.Columns("IdClasificacion").Visible = False
                            End If

                            If Not GridView1.Columns.ColumnByFieldName("IdFamilia") Is Nothing Then
                                GridView1.Columns("IdFamilia").Visible = False
                            End If

                            If Not GridView1.Columns.ColumnByFieldName("Dias_Local") Is Nothing Then
                                GridView1.Columns("Dias_Local").Visible = False
                            End If

                            If Not GridView1.Columns.ColumnByFieldName("Dias_Exterior") Is Nothing Then
                                GridView1.Columns("Dias_Exterior").Visible = False
                            End If


                            '#EJC20171006_0339: Footer totales.
                            Dim item As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Sum, "Cantidad_Presentacion", "Sum={0:n2}")
                            Dim item1 As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Sum, "Cantidad_UMBas", "Sum={0:n2}")
                            Dim item2 As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Sum, "Disponible_UMBas", "Sum={0:n2}")
                            Dim item3 As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Sum, "CantidadReservadaUmBas", "Sum={0:n2}")

                            If Not TieneTiempos Then
                                lblDispcon.Text = "Disponible con reserva"
                                lblDispsin.Text = "Disponible sin reserva"
                                lblDispNo.Enabled = False
                                lblDispNo.Visible = False
                                lblInfo.Visible = True
                            End If

                            GridView1.Columns("CantidadReservadaUmBas").Caption = "Reservado U.M.Bas"

                            GridView1.Columns("Cantidad_Presentacion").Caption = "Disponible_Presentación"

                            If GridView1.Columns("Cantidad_Presentacion").Summary.Count = 0 Then
                                GridView1.Columns("Cantidad_Presentacion").Summary.Add(item)
                            End If

                            If GridView1.Columns("Cantidad_UMBas").Summary.Count = 0 Then
                                GridView1.Columns("Cantidad_UMBas").Summary.Add(item1)
                            End If

                            If GridView1.Columns("Disponible_UMBas").Summary.Count = 0 Then
                                GridView1.Columns("Disponible_UMBas").Summary.Add(item2)
                            End If

                            If GridView1.Columns("CantidadReservadaUmBas").Summary.Count = 0 Then
                                GridView1.Columns("CantidadReservadaUmBas").Summary.Add(item3)
                            End If

                            GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatString = "{0:n6}"

                            GridView1.Columns("Cantidad_Presentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            GridView1.Columns("Cantidad_Presentacion").DisplayFormat.FormatString = "{0:n6}"

                            GridView1.Columns("Cantidad_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            GridView1.Columns("Cantidad_UMBas").DisplayFormat.FormatString = "{0:n6}"

                            GridView1.Columns("CantidadReservadaUmBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            GridView1.Columns("CantidadReservadaUmBas").DisplayFormat.FormatString = "{0:n6}"

                            '#EJC20171014_1040PM: Footer totales, faltaba count de registros.
                            GridView1.Columns("IdStock").SummaryItem.SummaryType = SummaryItemType.Count
                            GridView1.Columns("IdStock").SummaryItem.DisplayFormat = "Count = {0}"
                            GridView1.Columns("IdStock").Fixed = FixedStyle.Left

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

                GridView1.OptionsView.ColumnAutoWidth = False
                GridView1.BestFitColumns()

                Set_Label_Personalizados()

            Catch ex As Exception
            End Try

            'Application.DoEvents()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        Finally
            IsLoading = False
            SplashScreenManager.CloseForm(False)
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
            Dim Presentacion As New clsBeProducto_Presentacion
            Dim Factor As Double = 0
            Dim vIdxPresentacion As Integer = 0
            Dim vIdPresentacion As Integer = 0

            If pListObjDet.Count > 0 Then

                For Each S As DataRow In DTStock.Rows

                    Presentacion = New clsBeProducto_Presentacion

                    If vContadorActualizacion = pListObjDet.Count Then
                        Exit For
                    Else

                        vCantidadResEnMemoria = pListObjDet.FindAll(Function(x) x.IdStock = S.Item("IdStock") And x.Activo = True).Sum(Function(y) y.Cantidad)

                        Debug.WriteLine("Restando stock en memoria: " & vCantidadResEnMemoria)

                        If pListObjDet.Exists(Function(x) x.IdStock = S.Item("IdStock")) Then

                            Dim qry = From dr As DataRow In DTStockForMemory.AsEnumerable()
                                      Where dr.Field(Of Integer)("IdStock") = S.Item("IdStock")
                                      Select dr

                            If S.Item("IdPresentacion") IsNot DBNull.Value Then

                                If S.Item("IdPresentacion") > 0 Then

                                    vIdPresentacion = S.Item("IdPresentacion")
                                    vIdxPresentacion = lPresentacion.FindIndex(Function(x) x.IdPresentacion = vIdPresentacion)

                                    If vIdxPresentacion = -1 Then

                                        Presentacion = New clsBeProducto_Presentacion
                                        Presentacion = clsLnProducto_presentacion.Get_BeProductoPresentacion_By_IdPresentacion(S.Item("IdPresentacion"))

                                        If Not Presentacion Is Nothing Then
                                            lPresentacion.Add(Presentacion)
                                        End If

                                    End If

                                    If Not Presentacion Is Nothing Then
                                        vCantidadResEnMemoria = Math.Round(Presentacion.Factor * vCantidadResEnMemoria, 6)
                                    End If

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

                                'Else
                                '    S.Item("Disponible_umbas") = S.Item("Cantidad_UMBas") - vCantidadResEnMemoria
                            End If

                            vContadorActualizacion += 1

                        End If

                    End If

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

    Private Sub Listar_Stock_With_DT()

        Dim cTrans As New clsTransaccion

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Cargando stock...")

            DTStock.Clear()

            Dim watch As Stopwatch = Stopwatch.StartNew()

            If chkFiltroPolizaActivo.Checked AndAlso txtNoPoliza.Text.Trim <> "" Then

                If IdBodega = 0 Then
                    DTStock = clsLnStock.Get_All_Stock_Especifico_DT(AP.IdBodega, IdCliente, TieneTiempos, txtNoPoliza.Text, 0, IdProductoEstadoDefault)
                Else
                    DTStock = clsLnStock.Get_All_Stock_Especifico_DT(IdBodega, IdCliente, TieneTiempos, txtNoPoliza.Text, 0, IdProductoEstadoDefault)
                End If

            Else

                If IdBodega = 0 Then
                    DTStock = clsLnStock.Get_All_Stock_Especifico_DT(AP.IdBodega, IdCliente, TieneTiempos, "", pIdPropietarioBodega, IdProductoEstadoDefault)
                Else
                    DTStock = clsLnStock.Get_All_Stock_Especifico_DT(IdBodega, IdCliente, TieneTiempos, "", pIdPropietarioBodega, IdProductoEstadoDefault)
                End If

            End If

            If Not ProductoEspecifico Is Nothing Then

                If ProductoEspecifico.Codigo <> "" Then

                    Dim query =
                    From c In DTStock.AsEnumerable()
                    Where c.Field(Of String)("codigo") = (ProductoEspecifico.Codigo)

                    If query.Count > 0 Then
                        DTStock = query.CopyToDataTable
                    End If

                End If

            End If

            If DTStock.Rows.Count = 0 Then
                'XtraMessageBox.Show("El cliente no tiene definidos tiempos.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
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
        End Try

    End Sub

    Private Sub grdStock_DoubleClick(sender As Object, e As EventArgs) Handles grdStock.DoubleClick

        Try

            If (GridView1.RowCount > 0) Then

                If Modo = pModo.Seleccion Then

                    Dim Dr As DataRowView = GridView1.GetFocusedRow

                    pObjStock = clsLnStock.Get_Single_By_IdStock(Dr.Item("IdStock"))
                    pObjStock.CantidadUmBas = Dr.Item("Disponible_UMBas")

                    If Dr.Item("Aplica") = "No" AndAlso TieneTiempos Then
                        If XtraMessageBox.Show("No aplica a los tiempos del cliente. ¿Desea agregar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Me.Invalidate()
                            DialogResult = DialogResult.OK
                        End If
                    Else
                        Me.Invalidate()
                        DialogResult = DialogResult.OK
                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        DialogResult = DialogResult.Cancel
        Hide()
    End Sub

    Public Sub Listar_Stock_DesdeHilo()

        Try

            Dim watch As Stopwatch = Stopwatch.StartNew()

            Listar_Stock_With_DT()

            'BeginInvoke(CallBindProductos_To_Grid)

            'BindProductos_To_Grid()

            watch.Stop()

            Debug.Print("Tiempo transcurrido: " & watch.Elapsed.TotalSeconds)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick

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

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
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
            printLink.Component = grdStock
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

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle

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

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle

        Try

            If e.Column.FieldName = "Disponible_UMBas" Then

                If e.CellValue > 0 Then
                    Dim vFontNumero As Font = New Font("Arial monospaced for SAP", 12, FontStyle.Bold)
                    e.Appearance.Font = vFontNumero
                End If

            ElseIf e.Column.FieldName = "Cantidad_Presentacion" Then

                Dim vFontNumero As Font = New Font("Arial monospaced for SAP", 12, FontStyle.Bold)
                e.Appearance.Font = vFontNumero

            End If

            Dim View1 As GridView = sender

            If View1.Columns Is Nothing Then Exit Sub
            If View1.Columns.Count = 0 Then Exit Sub

            Dim Aplica As Object = IIf(IsDBNull(View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("Aplica"))), False, View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("Aplica")))
            Dim Reservado As Object = IIf(IsDBNull(View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("CantidadReservadaUmBas"))), 0, View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("CantidadReservadaUmBas")))
            'Dim IdCliente As Object = IIf(IsDBNull(View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("IdCliente"))), 0, View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("IdCliente")))

            If Val(IdCliente) = 0 Then

                e.Appearance.ForeColor = Color.Black
                If Val(Reservado) = 0 Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                    e.Appearance.BackColor = Color.LightGreen
                    e.Appearance.BackColor2 = Color.White
                ElseIf Val(Reservado) > 0 Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                    e.Appearance.BackColor = Color.LightYellow
                    e.Appearance.BackColor2 = Color.Yellow
                End If

            Else
                e.Appearance.ForeColor = Color.Black
                If Aplica = "Si" And Val(Reservado) = 0 Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                    e.Appearance.BackColor = Color.LightGreen
                    e.Appearance.BackColor2 = Color.White
                ElseIf Aplica = "Si" And Val(Reservado) > 0 Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                    e.Appearance.BackColor = Color.LightYellow
                    e.Appearance.BackColor2 = Color.Yellow
                ElseIf Aplica = "No" Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub GridView1_CustomRowFilter(sender As Object, e As RowFilterEventArgs) Handles GridView1.CustomRowFilter

        Try

            Dim view As ColumnView = CType(sender, ColumnView)
            Dim CantidadDisponible As String = Val(view.GetListSourceRowCellValue(e.ListSourceRow, "Disponible_UMBas").ToString())

            If CantidadDisponible = 0 Then
                e.Visible = False
                e.Handled = True
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

    Private Sub frmStock_Especifico_List_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.Ribbon.Refresh()
        Me.Refresh()
        Application.DoEvents()
    End Sub

    Private Sub lblProducto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblProducto.LinkClicked

        Try

            Dim Rec As New frmProductoList() With {
                   .Modo = frmProductoList.pModo.Seleccion,
                   .StartPosition = FormStartPosition.CenterParent,
                   .WindowState = FormWindowState.Maximized}
            Rec.ShowDialog()

            grdStock.DataSource = Nothing

            If Rec.pObjProducto IsNot Nothing AndAlso Rec.pObjProducto.IdProducto <> 0 Then

                ProductoEspecifico = Nothing

                txtIdProducto.Text = Rec.pObjProducto.Codigo
                'ProductoEspecifico.Codigo = Rec.pObjProducto.Codigo
                ProductoEspecifico = Rec.pObjProducto
                txtNombreProducto.Text = Rec.pObjProducto.Nombre
                ForceUpdateList = True
            End If

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

    Private Sub txtIdProducto_Validated(sender As Object, e As EventArgs) Handles txtIdProducto.Validated

        Try

            If String.IsNullOrEmpty(txtIdProducto.Text.Trim()) = False AndAlso txtIdProducto.Text > "0" Then

                ProductoEspecifico = clsLnProducto.Get_Single_By_Codigo(txtIdProducto.Text)

                If ProductoEspecifico IsNot Nothing AndAlso ProductoEspecifico.IdProducto > 0 Then
                    txtNombreProducto.Text = Trim(String.Format("{0}", ProductoEspecifico.Nombre))
                    ForceUpdateList = True
                    Listar_Stock_DesdeHilo()
                Else
                    XtraMessageBox.Show(String.Format("No existe producto con código {0}", txtIdProducto.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtIdProducto.Focus()
                    txtIdProducto.SelectAll()
                    ProductoEspecifico = Nothing
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdProducto_TextChanged(sender As Object, e As EventArgs) Handles txtIdProducto.TextChanged
        txtNombreProducto.Text = ""
        ProductoEspecifico = Nothing
        ForceUpdateList = True
        Listar_Stock_DesdeHilo()
    End Sub

    Private Sub LabelControl1_Click(sender As Object, e As EventArgs) Handles lblDispsin.Click

        If TieneTiempos = False Then
            Dim filterInt As String = "[CantidadReservadaUmBas] = 0"
            GridView1.Columns("CantidadReservadaUmBas").FilterInfo = New ColumnFilterInfo(filterInt)
        Else

            Dim filterString As String = "[Aplica] = 'Si'"
            GridView1.Columns("Aplica").FilterInfo = New ColumnFilterInfo(filterString)
            Dim filterInt As String = "[CantidadReservadaUmBas] = 0"
            GridView1.Columns("CantidadReservadaUmBas").FilterInfo = New ColumnFilterInfo(filterInt)

        End If

    End Sub

    Private Sub LabelControl2_Click(sender As Object, e As EventArgs) Handles lblDispcon.Click

        If TieneTiempos = False Then
            Dim filterInt As String = "[CantidadReservadaUmBas] > 0"
            GridView1.Columns("CantidadReservadaUmBas").FilterInfo = New ColumnFilterInfo(filterInt)
        Else

            Dim filterString As String = "[Aplica] = 'Si'"
            GridView1.Columns("Aplica").FilterInfo = New ColumnFilterInfo(filterString)
            Dim filterInt As String = "[CantidadReservadaUmBas] > 0"
            GridView1.Columns("CantidadReservadaUmBas").FilterInfo = New ColumnFilterInfo(filterInt)

        End If
    End Sub

    Private Sub LabelControl3_Click(sender As Object, e As EventArgs) Handles lblDispNo.Click
        Dim filterString As String = "[Aplica] = 'No'"
        GridView1.Columns("Aplica").FilterInfo = New ColumnFilterInfo(filterString)
    End Sub

    '#EJC20210716_1159AM:Guardar LayoutGrid en frmStock_Especifico_List.vb
    Private vNombreArchivoLayOutGrid As String = ""

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

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

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

    Private Sub frmStock_Especifico_List_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormCaption("Cargando stock...")

        mnuGuardarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '#EJC20210716:Restaurar LayoutGrid en frmstock_especifico_list.vb
        vNombreArchivoLayOutGrid = "frmstock_especifico_list.xml"

        '#GT21062022_1620: se deja false si se reabre el form.
        chkSeleccionMultiple.Checked = False

        selectedRows.Clear()

        Try

            BarButtonItem1.Enabled = False


            If Not BuscarPoliza Then
                txtNoPoliza.Visible = False
                txtNomPoliza.Visible = False
                lblPoliza.Visible = False
            Else
                txtNoPoliza.Visible = True
                txtNomPoliza.Visible = True
                lblPoliza.Visible = True
            End If

            Listar_Stock_DesdeHilo()

            listaStockSeleccionado = New List(Of clsBeVW_stock_res)

            RemoveHandler GridView1.MouseDown, AddressOf gridView1_MouseDown
            AddHandler GridView1.MouseDown, AddressOf gridView1_MouseDown

            RemoveHandler GridView1.MouseUp, AddressOf gridView1_MouseUp
            AddHandler GridView1.MouseUp, AddressOf gridView1_MouseUp

            RemoveHandler GridView1.ColumnFilterChanged, AddressOf gridView1_ColumnFilterChanged
            AddHandler GridView1.ColumnFilterChanged, AddressOf gridView1_ColumnFilterChanged

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
            BarButtonItem1.Enabled = True
        End Try


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

    Private Sub chkSeleccionMultiple_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkSeleccionMultiple.CheckedChanged

        Try

            If chkSeleccionMultiple.Checked Then

                If XtraMessageBox.Show("Si habilita la selección múltiple se tomarán las cantidades completas de cada línea. ¿Continuar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    GridView1.OptionsSelection.MultiSelect = True
                    GridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect
                    mnuTomarSeleccionados.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                Else
                    chkSeleccionMultiple.Checked = False
                End If

            Else
                SeleccionMultiple = False
                chkSeleccionMultiple.Checked = False
                GridView1.OptionsSelection.MultiSelect = False
                GridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect
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

            Dim selectedRowHandles As Integer() = GridView1.GetSelectedRows()

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

                    IdStock = IIf(IsDBNull(GridView1.GetRowCellValue(selectedRowHandles(i), "IdStock")), 0, GridView1.GetRowCellValue(selectedRowHandles(i), "IdStock"))
                    DisponibleUMBAs = IIf(IsDBNull(GridView1.GetRowCellValue(selectedRowHandles(i), "Disponible_UMBas")), 0, GridView1.GetRowCellValue(selectedRowHandles(i), "Disponible_UMBas"))
                    Aplica = IIf(IsDBNull(GridView1.GetRowCellValue(selectedRowHandles(i), "Aplica")), False, GridView1.GetRowCellValue(selectedRowHandles(i), "Aplica"))

                    pObjStock = New clsBeVW_stock_res()
                    pObjStock = clsLnStock.Get_Single_By_IdStock(IdStock)
                    'pObjStock.CantidadUmBas = DisponibleUMBAs

                    If Aplica = "No" AndAlso TieneTiempos AndAlso Not chkSeleccionMultiple.Checked Then
                        If XtraMessageBox.Show("No aplica a los tiempos del cliente. ¿Agregar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            listaStockSeleccionado.Add(pObjStock)
                        End If
                    Else
                        listaStockSeleccionado.Add(pObjStock)
                    End If

                    SplashScreenManager.Default.SetWaitFormCaption("IdStock: " & IdStock)

                    Application.DoEvents()

                Next

                If listaStockSeleccionado.Count > 0 Then

                    SeleccionMultiple = True
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

    Private Sub chkFiltroPolizaActivo_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkFiltroPolizaActivo.CheckedChanged

        Try

            txtNoPoliza.Visible = chkFiltroPolizaActivo.Checked
            txtNomPoliza.Visible = chkFiltroPolizaActivo.Checked
            lblPoliza.Visible = chkFiltroPolizaActivo.Checked

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                             Text,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub txtNoPoliza_Validating(sender As Object, e As CancelEventArgs) Handles txtNoPoliza.Validating

        Try

            If txtNoPoliza.Text.Trim <> "" Then

                e.Cancel = False
                'txtNoPoliza.Text = ""
                ProductoEspecifico = Nothing
                ForceUpdateList = True
                Listar_Stock_DesdeHilo()

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

    Private Sub GridView1_Layout(sender As Object, e As EventArgs) Handles GridView1.Layout

        If IsLoading Then Exit Sub

        Guardar_Layout()

        'Try


        '    Dim Ms As New MemoryStream
        '    GridView1.SaveLayoutToStream(Ms)
        '    Ms.Seek(0, SeekOrigin.Begin)
        '    Dim MsReader As New StreamReader(Ms)
        '    Dim LayoutToText As String = MsReader.ReadToEnd()

        '    clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
        '                                                  AP.UsuarioAp.IdUsuario,
        '                                                  AP.HostName,
        '                                                  vNombreArchivoLayOutGrid,
        '                                                  LayoutToText)

        '    mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        'Catch ex As Exception

        '    XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
        '                        Text,
        '                        MessageBoxButtons.OK,
        '                        MessageBoxIcon.Error)

        '    Dim vMsgError As String = ex.Message
        '    clsLnLog_error_wms.Agregar_Error(vMsgError)

        'End Try

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

    Private Sub gridView1_ColumnFilterChanged(ByVal sender As Object, ByVal e As EventArgs)
        RestoreSelection(TryCast(sender, GridView))
    End Sub

    Private Sub RestoreSelection(ByVal view As GridView)

        Try

            BeginInvoke(New Action(Sub()
                                       For i As Integer = 0 To selectedRows.Count - 1
                                           view.SelectRow(view.GetRowHandle(selectedRows(i)))
                                       Next
                                   End Sub))

            lblSeleccion.Text = "Registros seleccionados: {" & selectedRows.Count & "}"

        Catch ex As Exception

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try



    End Sub

    Private selectedRows As List(Of Integer) = New List(Of Integer)()

    Private Sub gridView1_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
        Dim view As GridView = TryCast(sender, GridView)
        RestoreSelection(view)
    End Sub

    Private Sub gridView1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)

        Try

            Dim view As GridView = TryCast(sender, GridView)
            Dim hi As GridHitInfo = view.CalcHitInfo(e.Location)


            If Not hi.Column Is Nothing Then

                If hi.Column.FieldName = "DX$CheckboxSelectorColumn" Then

                    If Not hi.InRow Then
                        Dim allSelected As Boolean = view.DataController.Selection.Count = view.DataRowCount

                        If Not allSelected Then

                            For i As Integer = 0 To view.RowCount - 1
                                Dim sourceHandle As Integer = view.GetDataSourceRowIndex(i)
                                If Not selectedRows.Contains(sourceHandle) Then selectedRows.Add(sourceHandle)
                            Next
                        Else
                            selectedRows.Clear()
                        End If
                    Else

                        Dim sourceHandle As Integer = view.GetDataSourceRowIndex(hi.RowHandle)

                        If Not selectedRows.Contains(sourceHandle) Then
                            selectedRows.Add(sourceHandle)
                        Else
                            selectedRows.Remove(sourceHandle)
                        End If

                    End If

                End If

            End If

            RestoreSelection(view)

        Catch ex As Exception

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuExportarExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuExportarExcel.ItemClick
        Exportar_Grid_A_Excel(grdStock, "WMS_StockEspecifico.xlsx")
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


    Public Sub Set_Label_Personalizados()

        Try

            Dim BeConfiguracion As New clsBeConfiguracion_alias_campos
            Dim TheColumnToChange As GridColumn = Nothing

            If Not lConfiguracionAliasCampos Is Nothing Then

                If lConfiguracionAliasCampos.Count > 0 Then

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "parametro_a")

                    If Not BeConfiguracion Is Nothing Then

                        TheColumnToChange = GridView1.Columns.ColumnByFieldName("parametro_a")

                        If Not TheColumnToChange Is Nothing Then
                            TheColumnToChange.Caption = BeConfiguracion.Alias_WMS
                        End If

                    End If

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "parametro_b")

                    If Not BeConfiguracion Is Nothing Then

                        TheColumnToChange = GridView1.Columns.ColumnByFieldName("parametro_b")

                        If Not TheColumnToChange Is Nothing Then
                            TheColumnToChange.Caption = BeConfiguracion.Alias_WMS
                        End If

                    End If

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "familia")

                    If Not BeConfiguracion Is Nothing Then

                        TheColumnToChange = GridView1.Columns.ColumnByFieldName("familia")

                        If Not TheColumnToChange Is Nothing Then
                            TheColumnToChange.Caption = BeConfiguracion.Alias_WMS
                        End If

                    End If

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "clasificacion")

                    If Not BeConfiguracion Is Nothing Then

                        TheColumnToChange = GridView1.Columns.ColumnByFieldName("clasificacion")

                        If Not TheColumnToChange Is Nothing Then
                            TheColumnToChange.Caption = BeConfiguracion.Alias_WMS
                        End If

                    End If

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
