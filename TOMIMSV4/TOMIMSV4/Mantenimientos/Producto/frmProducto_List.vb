Imports System.Drawing.Printing
Imports System.IO
Imports System.Reflection
Imports System.Threading
Imports System.Threading.Tasks
Imports DevExpress.Data
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen
Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Public Class frmProductoList

    Public pIdProductoExcepto As Integer
    Public pObjProducto As clsBeProducto
    Public pIdBodega As Integer
    Public pIdPropietario As Integer
    Public Property IsLoading As Boolean = True
    Public pIdPropietarioBodega As Integer
    Public EsKit As Boolean = False
    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
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

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
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
        DT.Columns.Add("Control_Vencimiento", GetType(Boolean))
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
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

                Parallel.ForEach(lista, Sub(ByVal BeProducto)

                                            SyncLock DT

                                                DT.Rows.Add(BeProducto.IdProductoBodega,
                                                            BeProducto.IdProducto,
                                                            BeProducto.Propietario.Nombre_comercial,
                                                            BeProducto.Clasificacion.Nombre,
                                                            BeProducto.Familia.Nombre,
                                                            BeProducto.Marca.Nombre,
                                                            BeProducto.TipoProducto.NombreTipoProducto,
                                                            BeProducto.UnidadMedida.Nombre,
                                                            BeProducto.Codigo,
                                                            BeProducto.Codigo_barra,
                                                            BeProducto.Nombre,
                                                            BeProducto.Costo,
                                                            BeProducto.Precio,
                                                            BeProducto.Kit,
                                                            BeProducto.Control_vencimiento,
                                                            BeProducto.Fec_agr)

                                            End SyncLock

                                        End Sub)

            End If

            '#EJC20171112_1153PM:
            ' Make asynchronous function call to Form's thread.
            'GridControl1.DataSource = DTTareas
            BeginInvoke(CallBindProductos_To_Grid)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Get_All_ProductosKit()

        Try

            Grd.DataSource = Nothing

            Dim lista As New List(Of clsBeProducto)

            lista = clsLnProducto.Get_All_By_Kit(True, chkActivos.Checked, pIdBodega, pIdPropietario, pIdProductoExcepto)

            If lista IsNot Nothing AndAlso lista.Count > 0 Then

                Parallel.ForEach(lista, Sub(ByVal BeProductoKit)

                                            SyncLock DT

                                                DT.Rows.Add(BeProductoKit.IdProductoBodega,
                                                            BeProductoKit.IdProducto,
                                                            BeProductoKit.Propietario.Nombre_comercial,
                                                            BeProductoKit.Clasificacion.Nombre,
                                                            BeProductoKit.Familia.Nombre,
                                                            BeProductoKit.Marca.Nombre,
                                                            BeProductoKit.TipoProducto.NombreTipoProducto,
                                                            BeProductoKit.UnidadMedida.Nombre,
                                                            BeProductoKit.Codigo,
                                                            BeProductoKit.Codigo_barra,
                                                            BeProductoKit.Nombre,
                                                            BeProductoKit.Costo,
                                                            BeProductoKit.Precio,
                                                            BeProductoKit.Kit,
                                                            BeProductoKit.Control_vencimiento,
                                                            BeProductoKit.Fec_agr)

                                            End SyncLock

                                        End Sub)

            End If

            '#EJC20171112_1153PM:
            ' Make asynchronous function call to Form's thread.
            'GridControl1.DataSource = DTTareas
            BeginInvoke(CallBindProductos_To_Grid)

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

        IsLoading = True

        Try

            Grd.DataSource = Nothing

            Dim dataView As New DataView(DT)
            dataView.Sort = " Código ASC, Nombre DESC"
            Dim DTOrdenado As DataTable = dataView.ToTable()

            Grd.DataSource = DTOrdenado

            If GridView.Columns.Count > 0 Then

                Set_LayOut_Grid()

                GridView.BestFitColumns()

                GridView.Columns("Correlativo").SummaryItem.SummaryType = SummaryItemType.Count
                GridView.Columns("Correlativo").SummaryItem.DisplayFormat = "{0:n2}"

                GridView.Columns("Fecha_Creacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                GridView.Columns("Fecha_Creacion").DisplayFormat.FormatString = "G"

                SetupGridView()

            End If

            '#EJC201906312: Uitlicé el footer del grid para desplegar la cantida dde registros.            
            lblRegs.Caption = String.Format("Registros: {0}", String.Format("{0:#,##0.00}", GridView.RowCount))

            Set_Label_Personalizados()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        Finally
            IsLoading = False
        End Try

    End Sub

    Private Sub Nuevo_Producto()

        Try

            Cierra_Instancia_Previa(frmProducto)

            With frmProducto
                .Modo = frmProducto.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .OpcionesMenu = OpcionesMenu()
                .InvokeListarProductos = AddressOf Listar
                .WindowState = FormWindowState.Normal
                .Show()
                .Focus()
            End With

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Public Sub Cierra_Instancia_Previa(ByRef Myform As Form)

        Try

            For Each objForm In My.Application.OpenForms
                If (Trim(objForm.Name) = Trim(Myform.Name)) Then
                    Myform.Close()
                    Exit For
                End If
            Next

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuNuevo_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuNuevo.ItemClick
        'GT 16042021 prueba para evitar el doble click por retraso o lentitud del sistema
        mnuNuevo.Enabled = False
        Nuevo_Producto()
        mnuNuevo.Enabled = True
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

                    If Dr.Item("Correlativo") IsNot Nothing Then
                        Dim IdProducto As Integer = Integer.Parse(Dr.Item("Correlativo"))
                        pObjProducto = clsLnProducto.Get_Single_By_IdProducto(IdProducto)
                    End If

                Else
                    Dim IdProducto As Integer = Integer.Parse(Dr.Item("Correlativo"))
                    pObjProducto = clsLnProducto.Get_Single_By_IdProductoBodega(IdProducto)
                End If

                Dim lSelectionIndex As Integer = GridView.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmProducto)

                    With frmProducto
                        .Modo = frmProducto.TipoTrans.Editar
                        .OpcionesMenu = OpcionesMenu()
                        .pBeProducto = pObjProducto
                        .InvokeListarProductos = AddressOf Listar
                        .MdiParent = MdiParent
                        .WindowState = FormWindowState.Normal
                        .Show()
                        .Focus()
                    End With

                    GridView.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Then
                    Hide()
                    SplashScreenManager.CloseForm(False)
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

    Private Sub cmdImportarExcel_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImportarExcel.ItemClick

        cmdImportarExcel.Enabled = False
        Dim Carga As New frmCargaExcel() With {.pNombreMantenimiento = "Producto", .pTipoMantenimiento = "Producto", .Listar = New frmCargaExcel.Operar(AddressOf Listar)}
        Carga.ShowDialog()
        Carga.Dispose()
        cmdImportarExcel.Enabled = True

    End Sub

    Private Sub frmProducto_List_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        'Listar()
    End Sub

    Private Sub GridView_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridView.RowStyle

        Try

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

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
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

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Set_LayOut_Grid()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGrid)

            If Not BeConfiguracionUsuarioDet Is Nothing Then
                GridView.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub frmProductoList_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            mnuNuevo.Enabled = OpcionesMenu.Modificar
            mnuActualizar.Enabled = OpcionesMenu.Leer

            vNombreArchivoLayOutGrid = "frmProducto_List.xml"

            Listar()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub cmdImportarReabasto_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImportarReabasto.ItemClick

        cmdImportarReabasto.Enabled = False
        Importar_Excel()
        cmdImportarReabasto.Enabled = True

    End Sub

    Private Sub Importar_Excel()

        Try

            '#GT20062022_1520: enviamos la bodega para comparar contra la bodega leida x linea.
            Dim Carga As New frmCargaExcel() With {.pNombreMantenimiento = "Importación de Reabasto",
                .pTipoMantenimiento = "Reabasto",
                .Listar = Nothing,
                .pBodegaOrigen = AP.Bodega,
                .IdInventarioEnc = -1}

            If Carga.ShowDialog() = DialogResult.OK Then


            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        Finally
            RibbonControl.Enabled = True
        End Try

    End Sub


    Private Sub Imprimir_Etiqueta(ByVal CodigoProd As String,
                                  ByVal PrinterName As String)
        Dim vIdTipoEtiqueta As Integer
        Dim vZPL As String = ""
        Dim BeProducto As New clsBeProducto
        Dim CodigoBodega As String = ""
        Dim NombreBodega As String = ""
        Dim NombreEmpresa As String = ""

        Try

            BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(CodigoProd, AP.Bodega.IdBodega)

            If BeProducto Is Nothing Then
                Throw New Exception("El producto no está asociado a ninguna bodega")
            End If

            vIdTipoEtiqueta = BeProducto.IdTipoEtiqueta
            CodigoBodega = AP.Bodega.Codigo
            NombreBodega = AP.Bodega.Nombre
            NombreEmpresa = AP.NomEmpresa

            If vIdTipoEtiqueta = 1 Then

#Region "vIdTipoEtiqueta = 1"

                vZPL = String.Format("^XA " +
                        "^MMT " +
                        "^PW700 " +
                        "^LL0406 " +
                        "^LS0 " +
                        "^FT450,21^A0I,20,14^FH^FD{4}^FS " +
                        "^FO2,40^GB670,0,5^FS " +
                        "^FT270,61^A0I,30,24^FH^FD{0}^FS " +
                        "^FT550,61^A0I,30,24^FH^FD{1}^FS " +
                        "^FT670,306^A0I,30,24^FH^FD{2}^FS " +
                        "^FT360,61^A0I,30,24^FH^FDBodega:^FS " +
                        "^FT670,61^A0I,30,24^FH^FDEmpresa:^FS " +
                        "^FT670,367^A0I,25,24^FH^FDTOMWMS Codigo de Producto^FS " +
                        "^FO2,340^GB670,0,14^FS " +
                        "^BY3,3,160^FT670,131^BCI,,Y,N " +
                        "^FD{3}^FS " +
                        "^PQ1,0,1,Y " +
                        "^XZ", CodigoBodega + " - " + NombreBodega, NombreEmpresa,
                        BeProducto.Codigo + " - " + BeProducto.Nombre,
                        BeProducto.Codigo_barra,
                        AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos + " / " + Format(Now, "dd-MMM-yyyy"))

#End Region

            ElseIf vIdTipoEtiqueta = 2 Then

#Region "vIdTipoEtiqueta = 2"
                vZPL = String.Format("^XA " +
                                        "^MMT " +
                                        "^PW600 " +
                                        "^LL0406 " +
                                        "^LS0 " +
                                        "^FT450,21^A0I,20,14^FH^FD{4}^FS  " +
                                        "^FO2,40^GB670,0,5^FS  " +
                                        "^FT440,90^A0I,28,30^FH^FD{0}^FS " +
                                        "^FT560,90^A0I,26,30^FH^FDBodega:^FS " +
                                        "^FT440,125^A0I,28,30^FH^FD{1}^FS " +
                                        "^FT560,125^A0I,26,30^FH^FDEmpresa:^FS " +
                                        "^BY2,3,160^FT550,200^BCI,,Y,N " +
                                        "^FD{2}^FS " +
                                        "^PQ1,0,1,Y  " +
                                        "^FT560,400^A0I,35,40^FH^FD{3}^FS " +
                                        "^FO2,440^GB670,14,14^FS " +
                                        "^FT560,470^A0I,25,24^FH^FDTOMWMS  Codigo de Producto^FS " +
                                        "^XZ", CodigoBodega + "-" + NombreBodega,
                                        NombreEmpresa,
                                        BeProducto.Codigo_barra,
                                        BeProducto.Codigo + " - " + BeProducto.Nombre,
                                        AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos + " / " + Format(Now, "dd-MMM-yyyy"))

#End Region

            ElseIf vIdTipoEtiqueta = 4 Then

#Region "vIdTipoEtiqueta = 4"
                vZPL = String.Format("^XA " +
                                    "^MMT " +
                                    "^PW812 " +
                                    "^LL609 " +
                                    "^LS0 " +
                                    "^FT450,21^A0I,20,14^FH^FD{4}^FS  " +
                                    "^FO2,40^GB670,0,5^FS  " +
                                    "^FT440,90^A0I,28,30^FH^FD{0}^FS " +
                                    "^FT560,90^A0I,26,30^FH^FDBodega:^FS " +
                                    "^FT440,125^A0I,28,30^FH^FD{1}^FS " +
                                    "^FT560,125^A0I,26,30^FH^FDEmpresa:^FS " +
                                    "^BY3,3,160^FT550,200^BCI,,Y,N " +
                                    "^FD{2}^FS " +
                                    "^PQ1,0,1,Y  " +
                                    "^FT600,400^A0I,35,40^FH^FD{3}^FS " +
                                    "^FO2,440^GB670,14,14^FS " +
                                    "^FT600,470^A0I,25,24^FH^FDTOMWMS Codigo de Producto^FS " +
                                    "^XZ", CodigoBodega + "-" + NombreBodega,
                                    NombreEmpresa,
                                    BeProducto.Codigo_barra,
                                    BeProducto.Codigo + " - " + BeProducto.Nombre,
                                    AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos + " / " + Format(Now, "dd-MMM-yyyy"))
#End Region

            ElseIf vIdTipoEtiqueta = 5 Then

#Region "vIdTipoEtiqueta = 5"

                vZPL = String.Format("
                                    ^XA
                                    ^MMT
                                    ^PW609
                                    ^LL0406
                                    ^LS0
                                    ^FT181,250^A0I,25,14^FH\^FD{0}^FS
                                    ^FT455,250^A0I,25,14^FH\^FD{1}^FS
                                    ^FT530,506^A0I,25,14^FH\^FD{2}^FS
                                    ^FT455,160^A0I,25,14^FH\^FD{3}^FS
                                    ^FT455,110^A0I,25,14^FH\^FD{4}^FS
                                    ^FT455,60^A0I,25,14^FH\^FD{5}^FS
                                    ^FT180,160^A0I,25,14^FH\^FD{6}^FS
                                    ^FT180,115^A0I,25,14^FH\^FD{7}^FS
                                    ^FT310,250^A0I,25,24^FH\^FDPropietario:^FS
                                    ^FT550,250^A0I,25,24^FH\^FDEmpresa:^FS
                                    ^FT550,160^A0I,25,24^FH\^FDMarca:^FS
                                    ^FT550,110^A0I,25,24^FH\^FDLinea:^FS
                                    ^FT550,60^A0I,25,24^FH\^FDModelo:^FS
                                    ^FT310,160^A0I,25,24^FH\^FDEstado:^FS
                                    ^FT310,115^A0I,25,24^FH\^FDLado:^FS
                                    ^FT530,560^A0I,25,24^FH\^FDTOM, WMS. - Product Barcode^FS
                                    ^FO2,540^GB606,0,12^FS
                                    ^BY2,3,155^FT530,331^BCI,,Y,N
                                    ^FD{8}^FS
                                    ^PQ1,0,1,Y
                                    ^XZ",
                                    BeProducto.Propietario.Nombre_comercial,
                                    AP.NomEmpresa,
                                    BeProducto.Nombre,
                                    BeProducto.Marca.Nombre,
                                    BeProducto.Familia.Nombre,
                                    BeProducto.Clasificacion.Nombre,
                                    BeProducto.ParametroA.Nombre,
                                    BeProducto.ParametroB.Nombre,
                                    BeProducto.Codigo_barra)


#End Region

            End If

            If vZPL <> "" Then
                RawPrinterHelper.SendStringToPrinter(PrinterName, vZPL)
            Else
                XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), "No está definido el tipo de etiqueta"),
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error)
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
                                    Imprimir_Etiqueta(Cod, pd.PrinterSettings.PrinterName)
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

    Public Sub Set_Label_Personalizados()

        Try

            Dim BeConfiguracion As New clsBeConfiguracion_alias_campos
            Dim TheColumnToChange As GridColumn = Nothing

            If Not lConfiguracionAliasCampos Is Nothing Then

                If lConfiguracionAliasCampos.Count > 0 Then

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "parametro_a")

                    If Not BeConfiguracion Is Nothing Then

                        TheColumnToChange = GridView.Columns.ColumnByFieldName("Parámetro A")

                        If Not TheColumnToChange Is Nothing Then
                            TheColumnToChange.Caption = BeConfiguracion.Alias_WMS
                        End If

                    End If

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "parametro_b")

                    If Not BeConfiguracion Is Nothing Then

                        TheColumnToChange = GridView.Columns.ColumnByFieldName("Parámetro B")

                        If Not TheColumnToChange Is Nothing Then
                            TheColumnToChange.Caption = BeConfiguracion.Alias_WMS
                        End If

                    End If

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "familia")

                    If Not BeConfiguracion Is Nothing Then

                        TheColumnToChange = GridView.Columns.ColumnByFieldName("Familia")

                        If Not TheColumnToChange Is Nothing Then
                            TheColumnToChange.Caption = BeConfiguracion.Alias_WMS
                        End If

                    End If

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "clasificacion")

                    If Not BeConfiguracion Is Nothing Then

                        TheColumnToChange = GridView.Columns.ColumnByFieldName("Clasificación")

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

    Private Sub cmdPlantilla_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdPlantilla.ItemClick

        Dim vRutaArchivo As String = CurDir() & "\Mantenimientos\plantillas\WMS_plantilla_Importacion_Productos.xlsx"

        Try
            If File.Exists(vRutaArchivo) Then
                ' Crear un nuevo SaveFileDialog
                Using saveDialog As New SaveFileDialog()
                    saveDialog.Title = "Guardar plantilla de importación de productos"
                    saveDialog.Filter = "Archivos de Excel (*.xlsx)|*.xlsx"
                    saveDialog.FileName = "WMS_plantilla_Importacion_Productos.xlsx"
                    ' Mostrar el cuadro de diálogo de guardado
                    If saveDialog.ShowDialog() = DialogResult.OK Then
                        ' Copiar el archivo a la ruta seleccionada por el usuario
                        File.Copy(vRutaArchivo, saveDialog.FileName, True)
                        XtraMessageBox.Show("Archivo guardado exitosamente en: " & saveDialog.FileName, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            Else
                XtraMessageBox.Show("No existe el formato en: " & vRutaArchivo, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Public Sub SetupGridView()

        Try

            ' Configuración para hacer todas las columnas no editables primero
            For Each col As GridColumn In GridView.Columns
                col.OptionsColumn.AllowEdit = False
            Next

            ' Encuentra la columna booleana por nombre o índice
            Dim booleanColumn As GridColumn = GridView.Columns("Control_Vencimiento")

            ' Hacer la columna booleana editable
            booleanColumn.OptionsColumn.AllowEdit = True

            ' Asociar un editor de checkbox a la columna booleana
            Dim checkEdit As New Repository.RepositoryItemCheckEdit()
            Grd.RepositoryItems.Add(checkEdit)
            booleanColumn.ColumnEdit = checkEdit

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Sub mnuActualizarControlVencimiento_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuActualizarControlVencimiento.ItemClick

        If XtraMessageBox.Show("¿Cambiar configuración de control por vencimiento?", "Datos maestros", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Actualizar_Control_Vencimiento()
        End If

    End Sub

    Private Actualizacion_Masiva_Vencimiento As Boolean = False
    Private Function Actualizar_Control_Vencimiento() As Boolean

        Dim clsTrans As New clsTransaccion
        Actualizacion_Masiva_Vencimiento = True

        Try

            Dim gridView As GridView = CType(Grd.MainView, GridView)
            Dim controlVencimientoValor As Object = Nothing
            Dim IdProducto As Integer = 0

            ' Asegúrate de que el nombre de la columna es correcto y existe en el GridView
            If gridView.Columns("Control_Vencimiento") Is Nothing Then
                Throw New Exception("La columna 'Control_Vencimiento' no existe en el GridView.")
            End If

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Actualizando propiedad.")

            clsTrans.Begin_Transaction()

            ' Iterar sobre todas las filas visibles
            For i As Integer = 0 To gridView.RowCount - 1

                ' Acceder al valor de la columna 'Control_Vencimiento' en la fila actual
                controlVencimientoValor = gridView.GetRowCellValue(i, "Control_Vencimiento")
                IdProducto = gridView.GetRowCellValue(i, "Correlativo")

                Dim oBeProducto As New clsBeProducto
                oBeProducto.IdProducto = IdProducto
                oBeProducto.Control_vencimiento = Not CBool(controlVencimientoValor)
                oBeProducto.User_mod = AP.UsuarioAp.IdUsuario
                oBeProducto.Fec_mod = Now

                clsLnProducto.Actualizar_Control_Vencimiento(oBeProducto, clsTrans.lConnection, clsTrans.lTransaction)

                gridView.SetRowCellValue(i, "Control_Vencimiento", Not CBool(controlVencimientoValor))

            Next

            clsTrans.Commit_Transaction()

            XtraMessageBox.Show("Se actualizó la configuración de los productos, se recargará la lista.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            clsTrans.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            clsTrans.Close_Conection() : SplashScreenManager.CloseForm(False)
            Actualizacion_Masiva_Vencimiento = False
        End Try

    End Function

    Private Sub GridView_ColumnFilterChanged(sender As Object, e As EventArgs) Handles GridView.ColumnFilterChanged
        lblRegs.Caption = String.Format("Registros: {0}", String.Format("{0:#,##0.00}", GridView.DataRowCount.ToString()))
    End Sub

    Private Sub GridView_CellValueChanging(sender As Object, e As CellValueChangedEventArgs) Handles GridView.CellValueChanging

        Try

            If Not Actualizacion_Masiva_Vencimiento Then

                If e.Column.FieldName = "Control_Vencimiento" Then

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormCaption("Actualizando propiedad.")

                    Dim IdProducto As Integer = GridView.GetRowCellValue(e.RowHandle, "Correlativo")
                    Dim ControlVencimiento As Boolean = GridView.GetRowCellValue(e.RowHandle, "Control_Vencimiento")
                    Dim CodigoProducto As String = GridView.GetRowCellValue(e.RowHandle, "Código")

                    Dim oBeProducto As New clsBeProducto
                    Dim ControlVencimientoActual As Boolean = clsLnProducto.Get_Control_Vencimiento_By_Codigo(CodigoProducto)

                    oBeProducto.IdProducto = IdProducto
                    oBeProducto.Control_vencimiento = Not (ControlVencimientoActual)
                    oBeProducto.User_mod = AP.UsuarioAp.IdUsuario
                    oBeProducto.Fec_mod = Now

                    clsLnProducto.Actualizar_Control_Vencimiento(oBeProducto)

                    SplashScreenManager.CloseForm(False)

                    XtraMessageBox.Show("Se actualizó la configuración de vencimiento a: " & IIf(Not ControlVencimientoActual, "Si", "No"), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarButtonItem2.ItemClick

        Dim vRutaArchivo As String = CurDir() & "\Mantenimientos\plantillas\WMS_plantilla_Actualizacion_IndiceRotacion.xlsx"

        Try
            If File.Exists(vRutaArchivo) Then
                ' Crear un nuevo SaveFileDialog
                Using saveDialog As New SaveFileDialog()
                    saveDialog.Title = "Guardar plantilla de importación de productos"
                    saveDialog.Filter = "Archivos de Excel (*.xlsx)|*.xlsx"
                    saveDialog.FileName = "WMS_plantilla_Actualizacion_IndiceRotacion.xlsx"
                    ' Mostrar el cuadro de diálogo de guardado
                    If saveDialog.ShowDialog() = DialogResult.OK Then
                        ' Copiar el archivo a la ruta seleccionada por el usuario
                        File.Copy(vRutaArchivo, saveDialog.FileName, True)
                        XtraMessageBox.Show("Archivo guardado exitosamente en: " & saveDialog.FileName, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            Else
                XtraMessageBox.Show("No existe el formato en: " & vRutaArchivo, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuImportarIndicesRotacion_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuImportarIndicesRotacion.ItemClick

        Try

            cmdImportarExcel.Enabled = False
            Dim Carga As New frmCargaExcel() With {.pNombreMantenimiento = "Producto", .pTipoMantenimiento = "Indices_Rotacion", .Listar = New frmCargaExcel.Operar(AddressOf Listar)}
            Carga.ShowDialog()
            Carga.Dispose()
            cmdImportarExcel.Enabled = True

        Catch ex As Exception

        End Try

    End Sub

    Private Async Sub mnuImagenesAWS_ItemClickAsync(sender As Object, e As ItemClickEventArgs) Handles mnuImagenesAWS.ItemClick

        Dim apiUrl As String = "https://ufgibcar4c.execute-api.us-west-2.amazonaws.com/dev/catalog/upload"
        Dim vRutaCDN As String = AP.Bodega.Ruta_CDN

        Try
            ' Validar DataTable
            If DT Is Nothing OrElse DT.Rows.Count = 0 Then
                XtraMessageBox.Show("No hay productos cargados en el DataTable.",
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
                Exit Sub
            End If

            ' Validar ruta de imágenes
            If String.IsNullOrWhiteSpace(vRutaCDN) OrElse Not Directory.Exists(vRutaCDN) Then
                XtraMessageBox.Show("La ruta local de imágenes no existe o está vacía.",
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' AGREGAR COLUMNAS TEMPORALMENTE (sin afectar el grid visiblemente)
            Dim columnasAgregadas As New List(Of String)

            If Not DT.Columns.Contains("EstadoAWS") Then
                DT.Columns.Add("EstadoAWS", GetType(String))
                columnasAgregadas.Add("EstadoAWS")
            End If
            If Not DT.Columns.Contains("MensajeAWS") Then
                DT.Columns.Add("MensajeAWS", GetType(String))
                columnasAgregadas.Add("MensajeAWS")
            End If
            If Not DT.Columns.Contains("RutaImagenAWS") Then
                DT.Columns.Add("RutaImagenAWS", GetType(String))
                columnasAgregadas.Add("RutaImagenAWS")
            End If

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

            Dim total As Integer = DT.Rows.Count
            Dim enviados As Integer = 0
            Dim errores As Integer = 0

            For i As Integer = 0 To DT.Rows.Count - 1
                Dim row As DataRow = DT.Rows(i)

                SplashScreenManager.Default.SetWaitFormDescription(
                String.Format("Enviando producto {0} de {1}...", i + 1, total)
            )

                ' Obtener valores
                Dim codigo As String = ObtenerValorRow(row, "Código")
                Dim marca As String = ObtenerValorRow(row, "Marca")
                Dim familia As String = ObtenerValorRow(row, "Familia")
                Dim clasificacion As String = ObtenerValorRow(row, "Clasificación")
                Dim tipoProducto As String = ObtenerValorRow(row, "Tipo Producto")

                If String.IsNullOrWhiteSpace(codigo) Then
                    row("EstadoAWS") = "ERROR"
                    row("MensajeAWS") = "Código vacío"
                    errores += 1
                    Continue For
                End If

                Dim rutaImagen As String = BuscarImagenLocalPorCodigo(codigo, vRutaCDN)

                If String.IsNullOrWhiteSpace(rutaImagen) OrElse Not File.Exists(rutaImagen) Then
                    row("EstadoAWS") = "SIN IMAGEN"
                    row("MensajeAWS") = "No se encontró imagen local"
                    errores += 1
                    Continue For
                End If

                Dim result = Await EnviarProductoCatalogoAWSAsync(apiUrl,
                                                                  codigo,
                                                                  marca,
                                                                  familia,
                                                                  clasificacion,
                                                                  tipoProducto,
                                                                  rutaImagen)

                If result IsNot Nothing AndAlso String.IsNullOrWhiteSpace(result.verror) Then
                    row("EstadoAWS") = "ENVIADO"
                    row("MensajeAWS") = If(result.message, "OK")
                    row("RutaImagenAWS") = If(result.key, "")
                    enviados += 1
                Else
                    row("EstadoAWS") = "ERROR"
                    row("MensajeAWS") = If(result IsNot Nothing, result.verror, "Sin respuesta")
                    errores += 1
                End If

                ' SOLO actualizar el DataTable, NO el grid
                ' Los cambios quedarán reflejados cuando el grid se refresque naturalmente
            Next

            SplashScreenManager.CloseForm(False)

            ' OPCIONAL: Notificar al usuario que puede refrescar el grid manualmente si lo desea
            If columnasAgregadas.Count > 0 Then
                XtraMessageBox.Show(
                String.Format("Proceso finalizado.{0}Enviados: {1}{0}Errores: {2}{0}{0}Nota: Se agregaron nuevas columnas al DataTable. Refresque el grid manualmente para verlas.",
                              Environment.NewLine, enviados, errores),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )
            Else
                XtraMessageBox.Show(
                String.Format("Proceso finalizado.{0}Enviados: {1}{0}Errores: {2}",
                              Environment.NewLine, enviados, errores),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("Error en mnuImagenesAWS_ItemClickAsync: {0}",
                                          ex.Message),
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try

    End Sub

    ' 🔧 FUNCIÓN AUXILIAR PARA MANEJAR DBNull
    Private Function ObtenerValorRow(row As DataRow, nombreColumna As String) As String
        If row.Table.Columns.Contains(nombreColumna) Then
            Dim valor = row(nombreColumna)
            If valor Is DBNull.Value Then
                Return ""
            End If
            Return valor.ToString().Trim()
        End If
        Return ""
    End Function

    Private Function ImagenAStringBase64(rutaArchivo As String) As String
        Dim bytes As Byte() = File.ReadAllBytes(rutaArchivo)
        Return Convert.ToBase64String(bytes)
    End Function

    Private Async Function EnviarProductoCatalogoAWSAsync(apiUrl As String,
                                                         codigo As String,
                                                         marca As String,
                                                         familia As String,
                                                         clasificacion As String,
                                                         tipoProducto As String,
                                                         rutaImagen As String) As Task(Of AWSResponse)

        Dim response As New AWSResponse()

        Try
            ' Leer y convertir imagen a Base64
            Dim imageBytes As Byte() = File.ReadAllBytes(rutaImagen)
            Dim imageBase64 As String = Convert.ToBase64String(imageBytes)

            ' Obtener nombre del archivo
            Dim filename As String = Path.GetFileName(rutaImagen)

            ' Validar extensión
            Dim extensionesPermitidas As String() = {".jpg", ".jpeg", ".png", ".webp"}
            Dim extension As String = Path.GetExtension(filename).ToLower()

            If Not extensionesPermitidas.Contains(extension) Then
                response.success = False
                response.verror = $"Extensión no permitida: {extension}"
                response.statusCode = 400
                Return response
            End If

            ' Validar tamaño (8MB máximo)
            If imageBytes.Length > 8 * 1024 * 1024 Then
                response.success = False
                response.verror = "La imagen excede 8MB"
                response.statusCode = 400
                Return response
            End If

            ' Crear payload
            Dim payload As New Dictionary(Of String, Object)
            payload.Add("product_id", codigo)
            payload.Add("marca", marca)
            payload.Add("familia", familia)
            payload.Add("clasificacion", clasificacion)
            payload.Add("tipo_producto", tipoProducto)
            payload.Add("filename", filename)
            payload.Add("image_base64", imageBase64)

            Dim jsonPayload As String = Newtonsoft.Json.JsonConvert.SerializeObject(payload)

            Using client As New HttpClient()
                client.DefaultRequestHeaders.Accept.Clear()
                client.DefaultRequestHeaders.Accept.Add(New Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"))
                client.Timeout = TimeSpan.FromSeconds(60)

                Dim content As New StringContent(jsonPayload, Encoding.UTF8, "application/json")
                Dim httpResponse As HttpResponseMessage = Await client.PostAsync(apiUrl, content)
                Dim responseBody As String = Await httpResponse.Content.ReadAsStringAsync()

                ' Guardar respuesta cruda para depuración
                response.raw_response = responseBody
                response.statusCode = CInt(httpResponse.StatusCode)

                If httpResponse.IsSuccessStatusCode Then
                    Try
                        ' Deserializar respuesta completa
                        Dim result = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(responseBody)

                        response.success = True

                        ' Mapear propiedades básicas
                        If result.ContainsKey("message") Then
                            response.message = result("message").ToString()
                        End If

                        If result.ContainsKey("bucket") Then
                            response.bucket = result("bucket").ToString()
                        End If

                        If result.ContainsKey("key") Then
                            response.key = result("key").ToString()
                        End If

                        If result.ContainsKey("product_id") Then
                            response.product_id = result("product_id").ToString()
                        End If

                        If result.ContainsKey("environment") Then
                            response.environment = result("environment").ToString()
                        End If

                        ' Procesar objetos procesados (para respuestas con múltiples archivos)
                        If result.ContainsKey("processed_objects") Then
                            Dim objectsJson = Newtonsoft.Json.JsonConvert.SerializeObject(result("processed_objects"))
                            response.processed_objects = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of ProcessedObject))(objectsJson)
                        End If

                        If result.ContainsKey("total_processed") Then
                            response.total_processed = Convert.ToInt32(result("total_processed"))
                        End If

                    Catch ex As Exception
                        response.success = True
                        response.message = "Respuesta recibida pero no se pudo parsear completamente"
                        response.verror = ex.Message
                    End Try
                Else
                    response.success = False
                    response.verror = $"HTTP {httpResponse.StatusCode}: {responseBody}"

                    ' Intentar extraer mensaje de error del cuerpo
                    Try
                        Dim errorResult = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(responseBody)
                        If errorResult.ContainsKey("error") Then
                            response.verror = errorResult("error").ToString()
                        End If
                    Catch
                        ' Mantener el error original
                    End Try
                End If
            End Using

        Catch ex As Exception
            response.success = False
            response.verror = ex.Message
            response.statusCode = 500
        End Try

        Return response
    End Function

    Private Function BuscarImagenLocalPorCodigo(codigo As String, rutaBase As String) As String
        Try
            ' Extensiones permitidas
            Dim extensiones As String() = {".jpg", ".jpeg", ".png", ".webp"}

            ' Buscar archivos que contengan el código
            For Each ext In extensiones
                ' Opción 1: Nombre exacto + extensión
                Dim rutaExacta As String = Path.Combine(rutaBase, codigo & ext)
                If File.Exists(rutaExacta) Then
                    Return rutaExacta
                End If

                ' Opción 2: Código como parte del nombre (ej: "COD123_imagen.jpg")
                Dim archivos As String() = Directory.GetFiles(rutaBase, $"*{codigo}*{ext}")
                If archivos.Length > 0 Then
                    Return archivos(0)
                End If
            Next

            Return String.Empty

        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

End Class

Public Class CatalogUploadRequest
    Public Property product_id As String
    Public Property marca As String
    Public Property familia As String
    Public Property clasificacion As String
    Public Property tipo_producto As String
    Public Property filename As String
    Public Property image_base64 As String
End Class

Public Class CatalogUploadResponse
    Public Property message As String
    Public Property bucket As String
    Public Property key As String
    Public Property product_id As String
    Public Property environment As String
    Public Property [error] As String
End Class

Public Class AWSResponse
    ' Propiedades básicas
    Public Property success As Boolean
    Public Property message As String
    Public Property verror As String

    ' Propiedades de la imagen/proceso
    Public Property bucket As String
    Public Property key As String
    Public Property product_id As String
    Public Property environment As String

    ' Propiedades para embeddings múltiples
    Public Property processed_objects As List(Of ProcessedObject)
    Public Property total_processed As Integer

    ' Propiedades adicionales
    Public Property statusCode As Integer
    Public Property raw_response As String  ' Para depuración

    ' Constructor
    Public Sub New()
        processed_objects = New List(Of ProcessedObject)()
    End Sub
End Class

' Clase para objetos procesados individualmente
Public Class ProcessedObject
    Public Property object_key As String
    Public Property status As String
    Public Property product_id As String
    Public Property process_id As String
    Public Property embeddings_count As Integer
    Public Property hash As String
End Class

' Clase para respuesta de embedding individual
Public Class EmbeddingResult
    Public Property model_name As String
    Public Property embedding_vector As List(Of Double)
    Public Property model_version As String
    Public Property processing_time_ms As Integer
    Public Property status As String
End Class
