<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmQA
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmQA))
        Me.cmbIdEmpresaOrigen = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.cmbIdBodegaOrigen = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.cmbIdPropietarioOrigen = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.cmbIdProducto = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemSpinEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.RepositoryItemCheckActivo = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.BarSubItem1 = New DevExpress.XtraBars.BarSubItem()
        Me.mnuCaso1 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCaso2 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCaso3 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCaso4 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCaso5 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCaso6 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCaso7 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCaso8 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCaso9 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCaso10 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCaso11 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCaso12 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCaso13 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCaso14 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCaso15 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCaso16 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCaso17 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCaso18 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCaso19 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCaso20 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImportarInventario = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuInventarioDemo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuDatosDemo = New DevExpress.XtraBars.BarSubItem()
        Me.mnuInsertarInventarioDemo = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.lblprg = New System.Windows.Forms.RichTextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.dgridPedido = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.dgridStock = New DevExpress.XtraGrid.GridControl()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.dgridStockReservado = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xtcQAReservas = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridConfiguracion = New DevExpress.XtraGrid.GridControl()
        Me.Configuracion_qaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Configuracion_QADataSet = New TOMWMS.Configuracion_QADataSet()
        Me.gvConfiguracion = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colIdConfiguracionQA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNombre = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colFechaEjecucion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdEmpresaOrigen = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdBodegaOrigen = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdPropietarioOrigen = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdCliente = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdProducto = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCantidad_Pedido_Presentacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCantidad_Pedido_UMBas = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.coluser_agr = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colfec_agr = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.coluser_mod = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colfec_mod = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colactivo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colResultado = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colObservaciones = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDisponiblePresentacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDisponibleUMBas = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.Configuracion_qaTableAdapter = New TOMWMS.Configuracion_QADataSetTableAdapters.configuracion_qaTableAdapter()
        Me.TableAdapterManager = New TOMWMS.Configuracion_QADataSetTableAdapters.TableAdapterManager()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.Configuracion_qaBindingNavigatorSaveItem = New System.Windows.Forms.ToolStripButton()
        Me.Configuracion_qaBindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.mnuReservaInventario = New System.Windows.Forms.ToolStripButton()
        CType(Me.cmbIdEmpresaOrigen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbIdBodegaOrigen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbIdPropietarioOrigen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbIdProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckActivo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.dgridPedido, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgridStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgridStockReservado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcQAReservas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcQAReservas.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.dgridConfiguracion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Configuracion_qaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Configuracion_QADataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvConfiguracion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.Configuracion_qaBindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Configuracion_qaBindingNavigator.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbIdEmpresaOrigen
        '
        Me.cmbIdEmpresaOrigen.AutoHeight = False
        Me.cmbIdEmpresaOrigen.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbIdEmpresaOrigen.DisplayMember = "empresa.nombre"
        Me.cmbIdEmpresaOrigen.Name = "cmbIdEmpresaOrigen"
        Me.cmbIdEmpresaOrigen.ValueMember = "empresa.IdEmpresa"
        '
        'cmbIdBodegaOrigen
        '
        Me.cmbIdBodegaOrigen.AutoHeight = False
        Me.cmbIdBodegaOrigen.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbIdBodegaOrigen.DisplayMember = "bodega.codigo"
        Me.cmbIdBodegaOrigen.Name = "cmbIdBodegaOrigen"
        Me.cmbIdBodegaOrigen.ValueMember = "bodega.IdBodega"
        '
        'cmbIdPropietarioOrigen
        '
        Me.cmbIdPropietarioOrigen.AutoHeight = False
        Me.cmbIdPropietarioOrigen.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbIdPropietarioOrigen.DisplayMember = "propietarios.nombre_comercial"
        Me.cmbIdPropietarioOrigen.Name = "cmbIdPropietarioOrigen"
        Me.cmbIdPropietarioOrigen.ValueMember = "propietarios.IdPropietario"
        '
        'cmbIdProducto
        '
        Me.cmbIdProducto.AutoHeight = False
        Me.cmbIdProducto.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbIdProducto.Name = "cmbIdProducto"
        Me.cmbIdProducto.NullText = ""
        '
        'RepositoryItemSpinEdit1
        '
        Me.RepositoryItemSpinEdit1.AutoHeight = False
        Me.RepositoryItemSpinEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemSpinEdit1.Name = "RepositoryItemSpinEdit1"
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'RepositoryItemCheckActivo
        '
        Me.RepositoryItemCheckActivo.AutoHeight = False
        Me.RepositoryItemCheckActivo.Name = "RepositoryItemCheckActivo"
        '
        'RibbonControl
        '
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(111)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.BarButtonItem1, Me.BarSubItem1, Me.mnuCaso1, Me.mnuCaso2, Me.mnuCaso3, Me.mnuCaso4, Me.mnuCaso5, Me.mnuCaso6, Me.mnuCaso7, Me.mnuCaso8, Me.mnuCaso9, Me.mnuCaso10, Me.mnuCaso11, Me.mnuCaso12, Me.mnuCaso13, Me.mnuCaso14, Me.mnuImportarInventario, Me.mnuCaso15, Me.mnuCaso16, Me.mnuCaso17, Me.mnuInventarioDemo, Me.mnuDatosDemo, Me.mnuInsertarInventarioDemo, Me.mnuCaso18, Me.mnuCaso19, Me.mnuCaso20})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(12)
        Me.RibbonControl.MaxItemId = 28
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 1256
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(1461, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.ActAsDropDown = True
        Me.BarButtonItem1.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.BarButtonItem1.Caption = "Reservas"
        Me.BarButtonItem1.DropDownControl = Me.PopupMenu1
        Me.BarButtonItem1.Id = 1
        Me.BarButtonItem1.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'PopupMenu1
        '
        Me.PopupMenu1.Name = "PopupMenu1"
        Me.PopupMenu1.Ribbon = Me.RibbonControl
        '
        'BarSubItem1
        '
        Me.BarSubItem1.Caption = "Reservas"
        Me.BarSubItem1.Id = 2
        Me.BarSubItem1.ImageOptions.SvgImage = CType(resources.GetObject("BarSubItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso1), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso2), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso3), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso4), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso5), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso6), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso7), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso8), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso9), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso10), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso11), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso12), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso13), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso14), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso15), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso16), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso17), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso18), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso19), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCaso20)})
        Me.BarSubItem1.Name = "BarSubItem1"
        '
        'mnuCaso1
        '
        Me.mnuCaso1.Caption = "Caso 1 - IDEAL_20231002011101 - CJ"
        Me.mnuCaso1.Id = 3
        Me.mnuCaso1.Name = "mnuCaso1"
        '
        'mnuCaso2
        '
        Me.mnuCaso2.Caption = "Caso 2 - IDEAL_20231002011120 - CJ"
        Me.mnuCaso2.Id = 4
        Me.mnuCaso2.Name = "mnuCaso2"
        '
        'mnuCaso3
        '
        Me.mnuCaso3.Caption = "Caso 3 - IDEAL_20231002011128 - CJ"
        Me.mnuCaso3.Id = 5
        Me.mnuCaso3.Name = "mnuCaso3"
        '
        'mnuCaso4
        '
        Me.mnuCaso4.Caption = "Caso 4 - IDEAL_20231002011132 - CJ"
        Me.mnuCaso4.Id = 6
        Me.mnuCaso4.Name = "mnuCaso4"
        '
        'mnuCaso5
        '
        Me.mnuCaso5.Caption = "Caso 5 - IDEAL_20231002011134 - CJ"
        Me.mnuCaso5.Id = 7
        Me.mnuCaso5.Name = "mnuCaso5"
        '
        'mnuCaso6
        '
        Me.mnuCaso6.Caption = "Caso 6 - IDEAL_20231002011136 - UND"
        Me.mnuCaso6.Id = 8
        Me.mnuCaso6.Name = "mnuCaso6"
        '
        'mnuCaso7
        '
        Me.mnuCaso7.Caption = "Caso 7 - IDEAL_20231002011140 - UND"
        Me.mnuCaso7.Id = 9
        Me.mnuCaso7.Name = "mnuCaso7"
        '
        'mnuCaso8
        '
        Me.mnuCaso8.Caption = "Caso 8 - IDEAL_20231002011142 - UND"
        Me.mnuCaso8.Id = 10
        Me.mnuCaso8.Name = "mnuCaso8"
        '
        'mnuCaso9
        '
        Me.mnuCaso9.Caption = "Caso 9 - IDEAL_20231002011144 - UND"
        Me.mnuCaso9.Id = 11
        Me.mnuCaso9.Name = "mnuCaso9"
        '
        'mnuCaso10
        '
        Me.mnuCaso10.Caption = "Caso 10 - IDEAL_20231002011146 - UND"
        Me.mnuCaso10.Id = 12
        Me.mnuCaso10.Name = "mnuCaso10"
        '
        'mnuCaso11
        '
        Me.mnuCaso11.Caption = "Caso 11 - IDEAL_20231002011153 - UND"
        Me.mnuCaso11.Id = 13
        Me.mnuCaso11.Name = "mnuCaso11"
        '
        'mnuCaso12
        '
        Me.mnuCaso12.Caption = "Caso 12 - IDEAL_20231002011159 - UND"
        Me.mnuCaso12.Id = 14
        Me.mnuCaso12.Name = "mnuCaso12"
        '
        'mnuCaso13
        '
        Me.mnuCaso13.Caption = "Caso 13 - IDEAL_20231002011201 - UND"
        Me.mnuCaso13.Id = 15
        Me.mnuCaso13.Name = "mnuCaso13"
        '
        'mnuCaso14
        '
        Me.mnuCaso14.Caption = "Caso 14 - IDEAL_20231002011201 - UND"
        Me.mnuCaso14.Id = 16
        Me.mnuCaso14.Name = "mnuCaso14"
        '
        'mnuCaso15
        '
        Me.mnuCaso15.Caption = "Caso 15 - IDEAL_202310181300 - UND"
        Me.mnuCaso15.Id = 19
        Me.mnuCaso15.Name = "mnuCaso15"
        '
        'mnuCaso16
        '
        Me.mnuCaso16.Caption = "Caso 16 - IDEAL_202310200156 - UND"
        Me.mnuCaso16.Id = 20
        Me.mnuCaso16.Name = "mnuCaso16"
        '
        'mnuCaso17
        '
        Me.mnuCaso17.Caption = "Caso 17 - IDEAL_202311040904 - UND"
        Me.mnuCaso17.Id = 21
        Me.mnuCaso17.Name = "mnuCaso17"
        '
        'mnuCaso18
        '
        Me.mnuCaso18.Caption = "Caso 18 - BAB_202311141033 - CJ"
        Me.mnuCaso18.Id = 25
        Me.mnuCaso18.Name = "mnuCaso18"
        '
        'mnuCaso19
        '
        Me.mnuCaso19.Caption = "Caso 19 - BYB_202311162103 - CJ"
        Me.mnuCaso19.Id = 26
        Me.mnuCaso19.Name = "mnuCaso19"
        '
        'mnuCaso20
        '
        Me.mnuCaso20.Caption = "Caso 20 - BYB_202311171219 - CJ"
        Me.mnuCaso20.Id = 27
        Me.mnuCaso20.Name = "mnuCaso20"
        '
        'mnuImportarInventario
        '
        Me.mnuImportarInventario.Caption = "importar Inventario"
        Me.mnuImportarInventario.Id = 18
        Me.mnuImportarInventario.ImageOptions.SvgImage = CType(resources.GetObject("mnuImportarInventario.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuImportarInventario.Name = "mnuImportarInventario"
        '
        'mnuInventarioDemo
        '
        Me.mnuInventarioDemo.Caption = "BarButtonItem2"
        Me.mnuInventarioDemo.Id = 22
        Me.mnuInventarioDemo.Name = "mnuInventarioDemo"
        '
        'mnuDatosDemo
        '
        Me.mnuDatosDemo.Caption = "Datos Demo"
        Me.mnuDatosDemo.Id = 23
        Me.mnuDatosDemo.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuInsertarInventarioDemo)})
        Me.mnuDatosDemo.Name = "mnuDatosDemo"
        '
        'mnuInsertarInventarioDemo
        '
        Me.mnuInsertarInventarioDemo.Caption = "Inventario"
        Me.mnuInsertarInventarioDemo.Id = 24
        Me.mnuInsertarInventarioDemo.Name = "mnuInsertarInventarioDemo"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "QA"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarSubItem1)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuImportarInventario)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuDatosDemo)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 692)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(12)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1461, 30)
        '
        'lblprg
        '
        Me.lblprg.BackColor = System.Drawing.Color.OldLace
        Me.lblprg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblprg.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.lblprg.Location = New System.Drawing.Point(4, 4)
        Me.lblprg.Margin = New System.Windows.Forms.Padding(4)
        Me.lblprg.Name = "lblprg"
        Me.lblprg.Size = New System.Drawing.Size(721, 246)
        Me.lblprg.TabIndex = 3
        Me.lblprg.Text = ""
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.dgridPedido, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.dgridStock, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.dgridStockReservado, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblprg, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.6!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.4!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1459, 442)
        Me.TableLayoutPanel1.TabIndex = 6
        '
        'dgridPedido
        '
        Me.dgridPedido.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridPedido.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridPedido.Location = New System.Drawing.Point(4, 258)
        Me.dgridPedido.MainView = Me.GridView2
        Me.dgridPedido.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridPedido.MenuManager = Me.RibbonControl
        Me.dgridPedido.Name = "dgridPedido"
        Me.dgridPedido.Size = New System.Drawing.Size(721, 180)
        Me.dgridPedido.TabIndex = 8
        Me.dgridPedido.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.DetailHeight = 431
        Me.GridView2.GridControl = Me.dgridPedido
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsBehavior.ReadOnly = True
        Me.GridView2.OptionsFind.AlwaysVisible = True
        Me.GridView2.OptionsView.ColumnAutoWidth = False
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        '
        'dgridStock
        '
        Me.dgridStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridStock.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridStock.Location = New System.Drawing.Point(733, 258)
        Me.dgridStock.MainView = Me.GridView3
        Me.dgridStock.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridStock.MenuManager = Me.RibbonControl
        Me.dgridStock.Name = "dgridStock"
        Me.dgridStock.Size = New System.Drawing.Size(722, 180)
        Me.dgridStock.TabIndex = 7
        Me.dgridStock.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView3})
        '
        'GridView3
        '
        Me.GridView3.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView3.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.Row.Options.UseFont = True
        Me.GridView3.DetailHeight = 431
        Me.GridView3.GridControl = Me.dgridStock
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsBehavior.ReadOnly = True
        Me.GridView3.OptionsFind.AlwaysVisible = True
        Me.GridView3.OptionsView.ColumnAutoWidth = False
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        '
        'dgridStockReservado
        '
        Me.dgridStockReservado.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridStockReservado.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridStockReservado.Location = New System.Drawing.Point(733, 4)
        Me.dgridStockReservado.MainView = Me.GridView1
        Me.dgridStockReservado.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridStockReservado.MenuManager = Me.RibbonControl
        Me.dgridStockReservado.Name = "dgridStockReservado"
        Me.dgridStockReservado.Size = New System.Drawing.Size(722, 246)
        Me.dgridStockReservado.TabIndex = 4
        Me.dgridStockReservado.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.dgridStockReservado
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        '
        'xtcQAReservas
        '
        Me.xtcQAReservas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcQAReservas.Location = New System.Drawing.Point(0, 224)
        Me.xtcQAReservas.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.xtcQAReservas.Name = "xtcQAReservas"
        Me.xtcQAReservas.SelectedTabPage = Me.XtraTabPage1
        Me.xtcQAReservas.Size = New System.Drawing.Size(1461, 468)
        Me.xtcQAReservas.TabIndex = 9
        Me.xtcQAReservas.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.dgridConfiguracion)
        Me.XtraTabPage1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1459, 438)
        Me.XtraTabPage1.Text = "Parámetros"
        '
        'dgridConfiguracion
        '
        Me.dgridConfiguracion.DataSource = Me.Configuracion_qaBindingSource
        Me.dgridConfiguracion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridConfiguracion.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.dgridConfiguracion.Location = New System.Drawing.Point(0, 0)
        Me.dgridConfiguracion.MainView = Me.gvConfiguracion
        Me.dgridConfiguracion.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.dgridConfiguracion.MenuManager = Me.RibbonControl
        Me.dgridConfiguracion.Name = "dgridConfiguracion"
        Me.dgridConfiguracion.Size = New System.Drawing.Size(1459, 438)
        Me.dgridConfiguracion.TabIndex = 1
        Me.dgridConfiguracion.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvConfiguracion})
        '
        'Configuracion_qaBindingSource
        '
        Me.Configuracion_qaBindingSource.DataMember = "configuracion_qa"
        Me.Configuracion_qaBindingSource.DataSource = Me.Configuracion_QADataSet
        '
        'Configuracion_QADataSet
        '
        Me.Configuracion_QADataSet.DataSetName = "Configuracion_QADataSet"
        Me.Configuracion_QADataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'gvConfiguracion
        '
        Me.gvConfiguracion.Appearance.ColumnFilterButton.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvConfiguracion.Appearance.ColumnFilterButton.Options.UseFont = True
        Me.gvConfiguracion.Appearance.ColumnFilterButtonActive.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvConfiguracion.Appearance.ColumnFilterButtonActive.Options.UseFont = True
        Me.gvConfiguracion.Appearance.CustomizationFormHint.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.CustomizationFormHint.Options.UseFont = True
        Me.gvConfiguracion.Appearance.DetailTip.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.DetailTip.Options.UseFont = True
        Me.gvConfiguracion.Appearance.Empty.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.Empty.Options.UseFont = True
        Me.gvConfiguracion.Appearance.EvenRow.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.EvenRow.Options.UseFont = True
        Me.gvConfiguracion.Appearance.FilterCloseButton.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.FilterCloseButton.Options.UseFont = True
        Me.gvConfiguracion.Appearance.FilterPanel.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.FilterPanel.Options.UseFont = True
        Me.gvConfiguracion.Appearance.FixedLine.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.FixedLine.Options.UseFont = True
        Me.gvConfiguracion.Appearance.FocusedCell.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.FocusedCell.Options.UseFont = True
        Me.gvConfiguracion.Appearance.FocusedRow.BackColor = System.Drawing.Color.Honeydew
        Me.gvConfiguracion.Appearance.FocusedRow.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gvConfiguracion.Appearance.FocusedRow.Options.UseFont = True
        Me.gvConfiguracion.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.FooterPanel.Options.UseFont = True
        Me.gvConfiguracion.Appearance.GroupButton.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.GroupButton.Options.UseFont = True
        Me.gvConfiguracion.Appearance.GroupFooter.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.GroupFooter.Options.UseFont = True
        Me.gvConfiguracion.Appearance.GroupPanel.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.GroupPanel.Options.UseFont = True
        Me.gvConfiguracion.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.GroupRow.Options.UseFont = True
        Me.gvConfiguracion.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvConfiguracion.Appearance.HideSelectionRow.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.HideSelectionRow.Options.UseFont = True
        Me.gvConfiguracion.Appearance.HorzLine.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.HorzLine.Options.UseFont = True
        Me.gvConfiguracion.Appearance.HotTrackedRow.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.HotTrackedRow.Options.UseFont = True
        Me.gvConfiguracion.Appearance.OddRow.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.OddRow.Options.UseFont = True
        Me.gvConfiguracion.Appearance.Preview.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.Preview.Options.UseFont = True
        Me.gvConfiguracion.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.Row.Options.UseFont = True
        Me.gvConfiguracion.Appearance.RowSeparator.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.RowSeparator.Options.UseFont = True
        Me.gvConfiguracion.Appearance.SelectedRow.BackColor = System.Drawing.Color.PaleGreen
        Me.gvConfiguracion.Appearance.SelectedRow.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.SelectedRow.Options.UseBackColor = True
        Me.gvConfiguracion.Appearance.SelectedRow.Options.UseFont = True
        Me.gvConfiguracion.Appearance.TopNewRow.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.TopNewRow.Options.UseFont = True
        Me.gvConfiguracion.Appearance.VertLine.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.VertLine.Options.UseFont = True
        Me.gvConfiguracion.Appearance.ViewCaption.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.gvConfiguracion.Appearance.ViewCaption.Options.UseFont = True
        Me.gvConfiguracion.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colIdConfiguracionQA, Me.colNombre, Me.colFechaEjecucion, Me.colIdEmpresaOrigen, Me.colIdBodegaOrigen, Me.colIdPropietarioOrigen, Me.colIdCliente, Me.colIdProducto, Me.colCantidad_Pedido_Presentacion, Me.colCantidad_Pedido_UMBas, Me.coluser_agr, Me.colfec_agr, Me.coluser_mod, Me.colfec_mod, Me.colactivo, Me.colResultado, Me.colObservaciones, Me.colDisponiblePresentacion, Me.colDisponibleUMBas})
        Me.gvConfiguracion.GridControl = Me.dgridConfiguracion
        Me.gvConfiguracion.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "IdConfiguracionQA", Me.colIdConfiguracionQA, "(Registros: {0})")})
        Me.gvConfiguracion.Name = "gvConfiguracion"
        Me.gvConfiguracion.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full
        Me.gvConfiguracion.OptionsView.ColumnAutoWidth = False
        Me.gvConfiguracion.OptionsView.ShowAutoFilterRow = True
        '
        'colIdConfiguracionQA
        '
        Me.colIdConfiguracionQA.FieldName = "IdConfiguracionQA"
        Me.colIdConfiguracionQA.MinWidth = 24
        Me.colIdConfiguracionQA.Name = "colIdConfiguracionQA"
        Me.colIdConfiguracionQA.Visible = True
        Me.colIdConfiguracionQA.VisibleIndex = 0
        Me.colIdConfiguracionQA.Width = 100
        '
        'colNombre
        '
        Me.colNombre.FieldName = "Nombre"
        Me.colNombre.MinWidth = 24
        Me.colNombre.Name = "colNombre"
        Me.colNombre.UnboundDataType = GetType(String)
        Me.colNombre.Visible = True
        Me.colNombre.VisibleIndex = 1
        Me.colNombre.Width = 199
        '
        'colFechaEjecucion
        '
        Me.colFechaEjecucion.DisplayFormat.FormatString = "d"
        Me.colFechaEjecucion.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.colFechaEjecucion.FieldName = "FechaEjecucion"
        Me.colFechaEjecucion.MinWidth = 24
        Me.colFechaEjecucion.Name = "colFechaEjecucion"
        Me.colFechaEjecucion.Visible = True
        Me.colFechaEjecucion.VisibleIndex = 2
        Me.colFechaEjecucion.Width = 140
        '
        'colIdEmpresaOrigen
        '
        Me.colIdEmpresaOrigen.ColumnEdit = Me.cmbIdEmpresaOrigen
        Me.colIdEmpresaOrigen.FieldName = "IdEmpresaOrigen"
        Me.colIdEmpresaOrigen.MinWidth = 24
        Me.colIdEmpresaOrigen.Name = "colIdEmpresaOrigen"
        Me.colIdEmpresaOrigen.Visible = True
        Me.colIdEmpresaOrigen.VisibleIndex = 3
        Me.colIdEmpresaOrigen.Width = 150
        '
        'colIdBodegaOrigen
        '
        Me.colIdBodegaOrigen.ColumnEdit = Me.cmbIdBodegaOrigen
        Me.colIdBodegaOrigen.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.colIdBodegaOrigen.FieldName = "IdBodegaOrigen"
        Me.colIdBodegaOrigen.MinWidth = 24
        Me.colIdBodegaOrigen.Name = "colIdBodegaOrigen"
        Me.colIdBodegaOrigen.Visible = True
        Me.colIdBodegaOrigen.VisibleIndex = 4
        Me.colIdBodegaOrigen.Width = 150
        '
        'colIdPropietarioOrigen
        '
        Me.colIdPropietarioOrigen.ColumnEdit = Me.cmbIdPropietarioOrigen
        Me.colIdPropietarioOrigen.FieldName = "IdPropietarioOrigen"
        Me.colIdPropietarioOrigen.MinWidth = 24
        Me.colIdPropietarioOrigen.Name = "colIdPropietarioOrigen"
        Me.colIdPropietarioOrigen.Visible = True
        Me.colIdPropietarioOrigen.VisibleIndex = 5
        Me.colIdPropietarioOrigen.Width = 150
        '
        'colIdCliente
        '
        Me.colIdCliente.FieldName = "IdCliente"
        Me.colIdCliente.MinWidth = 24
        Me.colIdCliente.Name = "colIdCliente"
        Me.colIdCliente.Visible = True
        Me.colIdCliente.VisibleIndex = 13
        Me.colIdCliente.Width = 94
        '
        'colIdProducto
        '
        Me.colIdProducto.ColumnEdit = Me.cmbIdProducto
        Me.colIdProducto.FieldName = "IdProducto"
        Me.colIdProducto.MinWidth = 24
        Me.colIdProducto.Name = "colIdProducto"
        Me.colIdProducto.Visible = True
        Me.colIdProducto.VisibleIndex = 6
        Me.colIdProducto.Width = 199
        '
        'colCantidad_Pedido_Presentacion
        '
        Me.colCantidad_Pedido_Presentacion.ColumnEdit = Me.RepositoryItemSpinEdit1
        Me.colCantidad_Pedido_Presentacion.FieldName = "Cantidad_Pedido_Presentacion"
        Me.colCantidad_Pedido_Presentacion.MinWidth = 24
        Me.colCantidad_Pedido_Presentacion.Name = "colCantidad_Pedido_Presentacion"
        Me.colCantidad_Pedido_Presentacion.Visible = True
        Me.colCantidad_Pedido_Presentacion.VisibleIndex = 7
        Me.colCantidad_Pedido_Presentacion.Width = 100
        '
        'colCantidad_Pedido_UMBas
        '
        Me.colCantidad_Pedido_UMBas.ColumnEdit = Me.RepositoryItemSpinEdit1
        Me.colCantidad_Pedido_UMBas.FieldName = "Cantidad_Pedido_UMBas"
        Me.colCantidad_Pedido_UMBas.MinWidth = 24
        Me.colCantidad_Pedido_UMBas.Name = "colCantidad_Pedido_UMBas"
        Me.colCantidad_Pedido_UMBas.Visible = True
        Me.colCantidad_Pedido_UMBas.VisibleIndex = 8
        Me.colCantidad_Pedido_UMBas.Width = 100
        '
        'coluser_agr
        '
        Me.coluser_agr.ColumnEdit = Me.RepositoryItemTextEdit1
        Me.coluser_agr.FieldName = "user_agr"
        Me.coluser_agr.MinWidth = 24
        Me.coluser_agr.Name = "coluser_agr"
        Me.coluser_agr.Width = 94
        '
        'colfec_agr
        '
        Me.colfec_agr.ColumnEdit = Me.RepositoryItemTextEdit1
        Me.colfec_agr.FieldName = "fec_agr"
        Me.colfec_agr.MinWidth = 24
        Me.colfec_agr.Name = "colfec_agr"
        Me.colfec_agr.Visible = True
        Me.colfec_agr.VisibleIndex = 9
        Me.colfec_agr.Width = 150
        '
        'coluser_mod
        '
        Me.coluser_mod.FieldName = "user_mod"
        Me.coluser_mod.MinWidth = 24
        Me.coluser_mod.Name = "coluser_mod"
        Me.coluser_mod.Width = 150
        '
        'colfec_mod
        '
        Me.colfec_mod.FieldName = "fec_mod"
        Me.colfec_mod.MinWidth = 24
        Me.colfec_mod.Name = "colfec_mod"
        Me.colfec_mod.Width = 94
        '
        'colactivo
        '
        Me.colactivo.ColumnEdit = Me.RepositoryItemCheckActivo
        Me.colactivo.FieldName = "activo"
        Me.colactivo.MinWidth = 24
        Me.colactivo.Name = "colactivo"
        Me.colactivo.Visible = True
        Me.colactivo.VisibleIndex = 10
        Me.colactivo.Width = 100
        '
        'colResultado
        '
        Me.colResultado.FieldName = "Resultado"
        Me.colResultado.MinWidth = 24
        Me.colResultado.Name = "colResultado"
        Me.colResultado.Visible = True
        Me.colResultado.VisibleIndex = 11
        Me.colResultado.Width = 199
        '
        'colObservaciones
        '
        Me.colObservaciones.FieldName = "Observaciones"
        Me.colObservaciones.MinWidth = 24
        Me.colObservaciones.Name = "colObservaciones"
        Me.colObservaciones.Visible = True
        Me.colObservaciones.VisibleIndex = 12
        Me.colObservaciones.Width = 199
        '
        'colDisponiblePresentacion
        '
        Me.colDisponiblePresentacion.Caption = "GridColumn1"
        Me.colDisponiblePresentacion.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colDisponiblePresentacion.FieldName = "DisponiblePresentacion"
        Me.colDisponiblePresentacion.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colDisponiblePresentacion.MinWidth = 24
        Me.colDisponiblePresentacion.Name = "colDisponiblePresentacion"
        Me.colDisponiblePresentacion.Visible = True
        Me.colDisponiblePresentacion.VisibleIndex = 14
        Me.colDisponiblePresentacion.Width = 94
        '
        'colDisponibleUMBas
        '
        Me.colDisponibleUMBas.Caption = "GridColumn1"
        Me.colDisponibleUMBas.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colDisponibleUMBas.FieldName = "DisponibleUMBas"
        Me.colDisponibleUMBas.MinWidth = 24
        Me.colDisponibleUMBas.Name = "colDisponibleUMBas"
        Me.colDisponibleUMBas.Visible = True
        Me.colDisponibleUMBas.VisibleIndex = 15
        Me.colDisponibleUMBas.Width = 94
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.TableLayoutPanel1)
        Me.XtraTabPage2.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(1459, 442)
        Me.XtraTabPage2.Text = "Resultados"
        '
        'Configuracion_qaTableAdapter
        '
        Me.Configuracion_qaTableAdapter.ClearBeforeFill = True
        '
        'TableAdapterManager
        '
        Me.TableAdapterManager.BackupDataSetBeforeUpdate = False
        Me.TableAdapterManager.bodegaTableAdapter = Nothing
        Me.TableAdapterManager.clienteTableAdapter = Nothing
        Me.TableAdapterManager.configuracion_qaTableAdapter = Me.Configuracion_qaTableAdapter
        Me.TableAdapterManager.empresaTableAdapter = Nothing
        Me.TableAdapterManager.productoTableAdapter = Nothing
        Me.TableAdapterManager.propietario_bodegaTableAdapter = Nothing
        Me.TableAdapterManager.propietariosTableAdapter = Nothing
        Me.TableAdapterManager.UpdateOrder = TOMWMS.Configuracion_QADataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(29, 28)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(29, 28)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 31)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Position"
        Me.BindingNavigatorPositionItem.AutoSize = False
        Me.BindingNavigatorPositionItem.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 27)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "Current position"
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(45, 28)
        Me.BindingNavigatorCountItem.Text = "of {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 31)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(29, 28)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(29, 28)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 31)
        '
        'BindingNavigatorAddNewItem
        '
        Me.BindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorAddNewItem.Image = CType(resources.GetObject("BindingNavigatorAddNewItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorAddNewItem.Name = "BindingNavigatorAddNewItem"
        Me.BindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorAddNewItem.Size = New System.Drawing.Size(29, 28)
        Me.BindingNavigatorAddNewItem.Text = "Add new"
        '
        'BindingNavigatorDeleteItem
        '
        Me.BindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorDeleteItem.Image = CType(resources.GetObject("BindingNavigatorDeleteItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorDeleteItem.Name = "BindingNavigatorDeleteItem"
        Me.BindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorDeleteItem.Size = New System.Drawing.Size(29, 28)
        Me.BindingNavigatorDeleteItem.Text = "Delete"
        '
        'Configuracion_qaBindingNavigatorSaveItem
        '
        Me.Configuracion_qaBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Configuracion_qaBindingNavigatorSaveItem.Image = CType(resources.GetObject("Configuracion_qaBindingNavigatorSaveItem.Image"), System.Drawing.Image)
        Me.Configuracion_qaBindingNavigatorSaveItem.Name = "Configuracion_qaBindingNavigatorSaveItem"
        Me.Configuracion_qaBindingNavigatorSaveItem.Size = New System.Drawing.Size(29, 28)
        Me.Configuracion_qaBindingNavigatorSaveItem.Text = "Save Data"
        '
        'Configuracion_qaBindingNavigator
        '
        Me.Configuracion_qaBindingNavigator.AddNewItem = Me.BindingNavigatorAddNewItem
        Me.Configuracion_qaBindingNavigator.BindingSource = Me.Configuracion_qaBindingSource
        Me.Configuracion_qaBindingNavigator.CountItem = Me.BindingNavigatorCountItem
        Me.Configuracion_qaBindingNavigator.DeleteItem = Me.BindingNavigatorDeleteItem
        Me.Configuracion_qaBindingNavigator.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.Configuracion_qaBindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorAddNewItem, Me.BindingNavigatorDeleteItem, Me.Configuracion_qaBindingNavigatorSaveItem, Me.mnuReservaInventario})
        Me.Configuracion_qaBindingNavigator.Location = New System.Drawing.Point(0, 193)
        Me.Configuracion_qaBindingNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.Configuracion_qaBindingNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.Configuracion_qaBindingNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.Configuracion_qaBindingNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.Configuracion_qaBindingNavigator.Name = "Configuracion_qaBindingNavigator"
        Me.Configuracion_qaBindingNavigator.PositionItem = Me.BindingNavigatorPositionItem
        Me.Configuracion_qaBindingNavigator.Size = New System.Drawing.Size(1461, 31)
        Me.Configuracion_qaBindingNavigator.TabIndex = 12
        Me.Configuracion_qaBindingNavigator.Text = "BindingNavigator1"
        '
        'mnuReservaInventario
        '
        Me.mnuReservaInventario.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mnuReservaInventario.Image = Global.TOMWMS.My.Resources.Resources.Biometric_data_04_32
        Me.mnuReservaInventario.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuReservaInventario.Name = "mnuReservaInventario"
        Me.mnuReservaInventario.Size = New System.Drawing.Size(29, 28)
        Me.mnuReservaInventario.Text = "Reserva inventario"
        '
        'frmQA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1461, 722)
        Me.Controls.Add(Me.xtcQAReservas)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.Configuracion_qaBindingNavigator)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.Name = "frmQA"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "frmQA"
        CType(Me.cmbIdEmpresaOrigen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbIdBodegaOrigen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbIdPropietarioOrigen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbIdProducto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckActivo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.dgridPedido, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgridStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgridStockReservado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcQAReservas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtcQAReservas.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.dgridConfiguracion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Configuracion_qaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Configuracion_QADataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvConfiguracion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.Configuracion_qaBindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Configuracion_qaBindingNavigator.ResumeLayout(False)
        Me.Configuracion_qaBindingNavigator.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents BarSubItem1 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuCaso1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblprg As RichTextBox
    Friend WithEvents mnuCaso2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCaso3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCaso4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCaso5 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCaso6 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCaso7 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCaso8 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCaso9 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCaso10 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCaso11 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCaso12 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCaso13 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents dgridStock As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents dgridStockReservado As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents dgridPedido As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents mnuCaso14 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents xtcQAReservas As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents mnuImportarInventario As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents Configuracion_QADataSet As Configuracion_QADataSet
    Friend WithEvents Configuracion_qaBindingSource As BindingSource
    Friend WithEvents Configuracion_qaTableAdapter As Configuracion_QADataSetTableAdapters.configuracion_qaTableAdapter
    Friend WithEvents TableAdapterManager As Configuracion_QADataSetTableAdapters.TableAdapterManager
    Friend WithEvents dgridConfiguracion As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvConfiguracion As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colIdConfiguracionQA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNombre As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colFechaEjecucion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdEmpresaOrigen As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdBodegaOrigen As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdPropietarioOrigen As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdCliente As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdProducto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCantidad_Pedido_Presentacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCantidad_Pedido_UMBas As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents coluser_agr As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colfec_agr As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents coluser_mod As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colfec_mod As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colactivo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colResultado As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colObservaciones As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDisponiblePresentacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDisponibleUMBas As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BindingNavigatorMoveFirstItem As ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As ToolStripTextBox
    Friend WithEvents BindingNavigatorCountItem As ToolStripLabel
    Friend WithEvents BindingNavigatorSeparator1 As ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As ToolStripSeparator
    Friend WithEvents BindingNavigatorAddNewItem As ToolStripButton
    Friend WithEvents BindingNavigatorDeleteItem As ToolStripButton
    Friend WithEvents Configuracion_qaBindingNavigatorSaveItem As ToolStripButton
    Friend WithEvents Configuracion_qaBindingNavigator As BindingNavigator
    Friend WithEvents mnuReservaInventario As ToolStripButton
    Friend WithEvents cmbIdEmpresaOrigen As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents cmbIdBodegaOrigen As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents cmbIdPropietarioOrigen As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents cmbIdProducto As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemSpinEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents RepositoryItemCheckActivo As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents mnuCaso15 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCaso16 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCaso17 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuInventarioDemo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuDatosDemo As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuInsertarInventarioDemo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCaso18 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCaso19 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCaso20 As DevExpress.XtraBars.BarButtonItem
End Class
