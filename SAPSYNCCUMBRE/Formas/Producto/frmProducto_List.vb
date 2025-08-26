Imports System.Drawing.Printing
Imports System.IO
Imports System.Reflection
Imports System.Threading
Imports DevExpress.Data
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmProductoList

    Public pIdProductoExcepto As Integer
    Public pObjProducto As clsBeProducto
    Public pIdBodega As Integer
    Public pIdPropietario As Integer
    Public Property IsLoading As Boolean = True
    Public pIdPropietarioBodega As Integer
    Public EsKit As Boolean = False
    Public Property Modo As pModo

    '#EJC20210716:Guardar LayoutGrid en LotesPorUbi.
    Private vNombreArchivoLayOutGrid As String = ""
    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Public Property Aplica_Filtro As Boolean = False
    Public threadListar_Productos As New Thread(AddressOf Listar_Productos)

    ReadOnly CallBindProductos_To_Grid As New MethodInvoker(AddressOf BindProductos_To_Grid)

    Sub New(Optional ByVal pAplicarFiltro As Boolean = False)

        ' This call is required by the designer.
        InitializeComponent()
        Aplica_Filtro = pAplicarFiltro
        IsLoading = False
        ' Add any initialization after the InitializeComponent() call.
        Init_DataTable_Productos()

    End Sub

    Public Sub New()
        InitializeComponent()
        IsLoading = False
    End Sub

    Private Sub Listar_Productos()

        Try

            If Not Aplica_Filtro Then
                Get_All_Productos()
            Else
                Get_All_Productos_By_PropietarioBodega()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try


    End Sub

    Private Sub Listar()

        If IsLoading Then Exit Sub

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Listando productos...")

            Listar_Productos()

        Catch ex As Exception

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private DT As New DataTable("Producto")

    Private Sub Init_DataTable_Productos()

        DT = New DataTable("Producto")
        DT.Columns.Add("Correlativo", GetType(Integer))

        If Aplica_Filtro Then
            DT.Columns.Add("ID", GetType(Integer))
            DT.Columns("ID").ColumnMapping = MappingType.Hidden
        End If

        DT.Columns.Add("Propietario", GetType(String))
        DT.Columns.Add("Clasificación", GetType(String))
        DT.Columns.Add("Familia", GetType(String))
        DT.Columns.Add("Marca", GetType(String))
        DT.Columns.Add("Tipo Producto", GetType(String))
        DT.Columns.Add("Unidad Medida", GetType(String))
        DT.Columns.Add("Código", GetType(String))
        DT.Columns.Add("Código de Barra", GetType(String))
        DT.Columns.Add("Nombre", GetType(String))
        'DT.Columns.Add("Existencia Mínima", GetType(Double))
        'DT.Columns.Add("Existencia Máxima", GetType(Double))
        'DT.Columns.Add("ExistenciaUMBas", GetType(Double))
        DT.Columns.Add("Costo", GetType(Double))
        DT.Columns.Add("Precio", GetType(Double))
        DT.Columns.Add("Es_Producto_Kit", GetType(Boolean))
        DT.Columns.Add("Fecha_Creacion", GetType(DateTime))

    End Sub

    Private Sub Get_All_Productos()

        Try

            'Dim lista As New List(Of clsBeProducto)           
            If EsKit Then
                DT = clsLnProducto.Get_All_ProdKit(True, pIdProductoExcepto, pIdBodega, pIdPropietario)
            Else
                DT = clsLnProducto.Get_All_Lista_Producto(chkActivos.Checked)
            End If

            '#EJC20171112_1109PM:
            ' Make asynchronous function call to Form's thread.
            'GridControl1.DataSource = DTTareas
            BeginInvoke(CallBindProductos_To_Grid)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Get_All_Productos_By_PropietarioBodega()

        Try

            Grd.DataSource = Nothing

            Dim lista As New List(Of clsBeProducto)

            If pIdPropietarioBodega <> 0 Then
                lista = clsLnProducto.Get_All_By_PropietarioBodega(True, chkActivos.Checked, pIdBodega, pIdPropietarioBodega)
            ElseIf pIdPropietario <> 0 Then
                lista = clsLnProducto.Get_All_By_Propietario(True, chkActivos.Checked, pIdBodega, pIdPropietario)
            End If

            DT.Rows.Clear()

            If lista IsNot Nothing AndAlso lista.Count > 0 Then

                Parallel.ForEach(lista, Sub(ByVal Obj)

                                            SyncLock DT

                                                DT.Rows.Add(Obj.IdProductoBodega,
                                                            Obj.IdProducto,
                                                            Obj.Propietario.Nombre_comercial,
                                                            Obj.Clasificacion.Nombre,
                                                            Obj.Familia.Nombre,
                                                            Obj.Marca.Nombre,
                                                            Obj.TipoProducto.NombreTipoProducto,
                                                            Obj.UnidadMedida.Nombre,
                                                            Obj.Codigo,
                                                            Obj.Codigo_barra,
                                                            Obj.Nombre,
                                                            Obj.Costo,
                                                            Obj.Precio,
                                                            Obj.Kit,
                                                            Obj.Fec_agr)

                                            End SyncLock

                                        End Sub)

            End If

            '#EJC20171112_1153PM:
            ' Make asynchronous function call to Form's thread.
            'GridControl1.DataSource = DTTareas
            BeginInvoke(CallBindProductos_To_Grid)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Get_All_ProductosKit()

        Try

            Grd.DataSource = Nothing

            Dim lista As New List(Of clsBeProducto)

            lista = clsLnProducto.Get_All_By_Kit(True, chkActivos.Checked, pIdBodega, pIdPropietario, pIdProductoExcepto)

            If lista IsNot Nothing AndAlso lista.Count > 0 Then

                Parallel.ForEach(lista, Sub(ByVal Obj)

                                            SyncLock DT

                                                DT.Rows.Add(Obj.IdProductoBodega,
                                                            Obj.IdProducto,
                                                            Obj.Propietario.Nombre_comercial,
                                                            Obj.Clasificacion.Nombre,
                                                            Obj.Familia.Nombre,
                                                            Obj.Marca.Nombre,
                                                            Obj.TipoProducto.NombreTipoProducto,
                                                            Obj.UnidadMedida.Nombre,
                                                            Obj.Codigo,
                                                            Obj.Codigo_barra,
                                                            Obj.Nombre,
                                                            Obj.Costo,
                                                            Obj.Precio,
                                                            Obj.Kit,
                                                            Obj.Fec_agr)

                                            End SyncLock

                                        End Sub)

            End If

            '#EJC20171112_1153PM:
            ' Make asynchronous function call to Form's thread.
            'GridControl1.DataSource = DTTareas
            BeginInvoke(CallBindProductos_To_Grid)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Public Sub BindProductos_To_Grid()

        IsLoading = True

        Try

            Grd.DataSource = Nothing

            Dim dataView As New DataView(DT)
            dataView.Sort = " Código ASC, Nombre DESC"
            Dim DTOrdenado As DataTable = dataView.ToTable()

            Grd.DataSource = DTOrdenado

            If GridView.Columns.Count > 0 Then

                GridView.BestFitColumns()

                GridView.Columns("Correlativo").SummaryItem.SummaryType = SummaryItemType.Count
                GridView.Columns("Correlativo").SummaryItem.DisplayFormat = "{0:n2}"

                GridView.Columns("Fecha_Creacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                GridView.Columns("Fecha_Creacion").DisplayFormat.FormatString = "G"

            End If

            '#EJC201906312: Uitlicé el footer del grid para desplegar la cantida dde registros.            
            lblRegs.Caption = String.Format("Registros: {0}", String.Format("{0:#,##0.00}", GridView.RowCount))

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        Finally
            IsLoading = False
        End Try

    End Sub

    Private Sub Procesar_Registro()

        Try

            Try
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Producto")
            Catch ex As Exception
            End Try

            If GridView.RowCount > 0 Then

                Dim Dr As DataRowView = GridView.GetFocusedRow

                pObjProducto = New clsBeProducto

                If pIdBodega = 0 AndAlso pIdPropietarioBodega = 0 Then
                    Dim IdProducto As Integer = Integer.Parse(Dr.Item("Correlativo"))
                    pObjProducto = clsLnProducto.Get_Single_By_IdProducto(IdProducto)
                Else
                    Dim IdProducto As Integer = Integer.Parse(Dr.Item("Correlativo"))
                    pObjProducto = clsLnProducto.Get_Single_By_IdProductoBodega(IdProducto)
                End If

                Dim lSelectionIndex As Integer = GridView.FocusedRowHandle

                If Modo = pModo.Lista Then

                    GridView.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Then
                    Hide()
                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Grd_DoubleClick(sender As Object, e As EventArgs) Handles Grd.DoubleClick
        Procesar_Registro()
    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkActivos.CheckedChanged
        Listar()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuActualizar.ItemClick
        mnuActualizar.Enabled = False
        Listar()
        mnuActualizar.Enabled = True
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImprimir.ItemClick
        cmdImprimir.Enabled = False
        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Imprimiendo...")
        Imprimir_Vista()
        cmdImprimir.Enabled = True
    End Sub

    Private Sub Imprimir_Vista()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - MI3 "

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
            printLink.Component = Grd
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            SplashScreenManager.CloseForm(False)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Productos"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub GridView_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridView.RowStyle

        Try

            GridView.OptionsBehavior.Editable = False
            GridView.OptionsSelection.EnableAppearanceFocusedCell = False

            GridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            GridView.OptionsSelection.EnableAppearanceFocusedRow = True
            GridView.OptionsSelection.EnableAppearanceHideSelection = True
            GridView.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridView.Appearance.FocusedRow.ForeColor = Color.White
            GridView.Appearance.SelectedRow.ForeColor = Color.White

            GridView.Appearance.SelectedRow.Options.UseBackColor = True
            GridView.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Grd_KeyDown(sender As Object, e As KeyEventArgs) Handles Grd.KeyDown
        If e.KeyCode = Keys.Enter Then Procesar_Registro()
    End Sub

    Private Sub frmProductoList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub GridView_Layout(sender As Object, e As EventArgs) Handles GridView.Layout

        Try

            If IsLoading Then Exit Sub

            Dim Ms As New MemoryStream
            GridView.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuEliminarLayoutGrid_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick

        Try

            If File.Exists(vNombreArchivoLayOutGrid) Then
                File.Delete(vNombreArchivoLayOutGrid)
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Never
            End If

            XtraMessageBox.Show("Diseño de grid eliminado!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Close()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                              Text,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmProductoList_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            vNombreArchivoLayOutGrid = "frmProducto_List.xml"

            Listar()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub cmdImprimirCodigoBarra_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImprimirCodigoBarra.ItemClick

        Try

            If (GridView.RowCount > 0) Then

                If GridView.SelectedRowsCount > 0 Then

                    Dim pd As PrintDialog = New PrintDialog()
                    pd.PrinterSettings = New PrinterSettings()

                    '#GT22112022_0800: se pide el Ok de impresión solo una vez para DyD
                    If DialogResult.OK = pd.ShowDialog(Me) Then

                        cmdImprimirCodigoBarra.Enabled = False

                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormCaption("Imprimiendo código de barra de producto")

                        For i As Integer = 0 To GridView.SelectedRowsCount - 1

                            Dim Dr As DataRow
                            Dim Cod As String = ""

                            If GridView.GetSelectedRows(i) >= 0 Then

                                Dr = GridView.GetDataRow(GridView.GetSelectedRows(i))

                                '#EJC20210714: Validar que el codigo no sea nulo.
                                Cod = IIf(IsDBNull(Dr.Item("Código")), "", Dr.Item("Código"))
                                If Cod <> "" Then
                                    SplashScreenManager.Default.SetWaitFormCaption("Imprimiendo código de barra de producto")
                                    'Imprimir_Etiqueta(Cod, pd.PrinterSettings.PrinterName)
                                End If
                            End If

                        Next

                    End If


                    cmdImprimirCodigoBarra.Enabled = True

                End If

            End If

        Catch ex As Exception
            cmdImprimirCodigoBarra.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub cmdPlantilla_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdPlantilla.ItemClick

        Try

            Process.Start("Excel", CurDir() & "\Mantenimientos\plantillas\WMS_plantilla_Importacion_Productos.xlsx")

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

End Class