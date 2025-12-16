<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPedido_List
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If pBePedidoEnc IsNot Nothing Then
                pBePedidoEnc.Dispose()
                pBePedidoEnc = Nothing
            End If
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPedido_List))
        Dim PushTransition1 As DevExpress.Utils.Animation.PushTransition = New DevExpress.Utils.Animation.PushTransition()
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuNuevo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.chkActivos = New DevExpress.XtraBars.BarCheckItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.cmdImportarExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMI3Sync = New DevExpress.XtraBars.BarButtonItem()
        Me.BarCheckItem2 = New DevExpress.XtraBars.BarCheckItem()
        Me.chkAnulados = New DevExpress.XtraBars.BarCheckItem()
        Me.mnuEliminarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.chkDespachados = New DevExpress.XtraBars.BarCheckItem()
        Me.chkMostrarGridDetalle = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.mnuGuardarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminarPedido = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuExportarExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.chkSinExistencias = New DevExpress.XtraBars.BarCheckItem()
        Me.chkSinExistenciasERP = New DevExpress.XtraBars.BarCheckItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup4 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.WorkspaceManager1 = New DevExpress.Utils.WorkspaceManager(Me.components)
        Me.DgridPedido = New DevExpress.XtraGrid.GridControl()
        Me.gviewEncabezadoPedido = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemPictureEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblHasta = New System.Windows.Forms.Label()
        Me.lbldesde = New System.Windows.Forms.Label()
        Me.dtpFechaAl = New System.Windows.Forms.DateTimePicker()
        Me.dtpFechaDel = New System.Windows.Forms.DateTimePicker()
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection(Me.components)
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.BarCheckItem1 = New DevExpress.XtraBars.BarCheckItem()
        Me.lblPrg = New System.Windows.Forms.RichTextBox()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.BarCheckItem3 = New DevExpress.XtraBars.BarCheckItem()
        Me.dgridDetalle = New DevExpress.XtraGrid.GridControl()
        Me.gviewDetallePedido = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemPictureEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.chkTemporales = New DevExpress.XtraBars.BarCheckItem()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgridPedido, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gviewEncabezadoPedido, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgridDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gviewDetallePedido, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.AutoSaveLayoutToXml = True
        Me.RibbonControl.AutoSaveLayoutToXmlPath = "frmPedidosListRibbonSettings.xml"
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuNuevo, Me.mnuActualizar, Me.mnuSalir, Me.chkActivos, Me.lblRegs, Me.cmdImportarExcel, Me.BarButtonItem1, Me.cmdImprimir, Me.mnuMI3Sync, Me.BarCheckItem2, Me.chkAnulados, Me.mnuEliminarLayoutGrid, Me.chkDespachados, Me.chkMostrarGridDetalle, Me.mnuGuardarLayoutGrid, Me.mnuEliminarPedido, Me.mnuExportarExcel, Me.chkSinExistencias, Me.chkSinExistenciasERP, Me.chkTemporales})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonControl.MaxItemId = 23
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1345, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuNuevo
        '
        Me.mnuNuevo.Caption = "Nuevo"
        Me.mnuNuevo.Id = 1
        Me.mnuNuevo.ImageOptions.SvgImage = CType(resources.GetObject("mnuNuevo.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuNuevo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N))
        Me.mnuNuevo.Name = "mnuNuevo"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuSalir
        '
        Me.mnuSalir.Caption = "Salir"
        Me.mnuSalir.Id = 3
        Me.mnuSalir.ImageOptions.SvgImage = CType(resources.GetObject("mnuSalir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuSalir.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
        Me.mnuSalir.Name = "mnuSalir"
        '
        'chkActivos
        '
        Me.chkActivos.BindableChecked = True
        Me.chkActivos.Caption = "Activos"
        Me.chkActivos.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.chkActivos.Checked = True
        Me.chkActivos.Id = 4
        Me.chkActivos.Name = "chkActivos"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 5
        Me.lblRegs.Name = "lblRegs"
        '
        'cmdImportarExcel
        '
        Me.cmdImportarExcel.Caption = "Importar Excel"
        Me.cmdImportarExcel.Id = 7
        Me.cmdImportarExcel.ImageOptions.LargeImage = Global.TOMWMS.My.Resources.Resources.excel_icon
        Me.cmdImportarExcel.Name = "cmdImportarExcel"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.BarButtonItem1.Caption = "WCF"
        Me.BarButtonItem1.Id = 8
        Me.BarButtonItem1.ImageOptions.Image = CType(resources.GetObject("BarButtonItem1.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 9
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'mnuMI3Sync
        '
        Me.mnuMI3Sync.Caption = "MI3 Sync"
        Me.mnuMI3Sync.Id = 10
        Me.mnuMI3Sync.ImageOptions.SvgImage = CType(resources.GetObject("mnuMI3Sync.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuMI3Sync.Name = "mnuMI3Sync"
        '
        'BarCheckItem2
        '
        Me.BarCheckItem2.Caption = "BarCheckItem2"
        Me.BarCheckItem2.Id = 11
        Me.BarCheckItem2.Name = "BarCheckItem2"
        '
        'chkAnulados
        '
        Me.chkAnulados.Caption = "Anulados"
        Me.chkAnulados.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.chkAnulados.Id = 12
        Me.chkAnulados.Name = "chkAnulados"
        '
        'mnuEliminarLayoutGrid
        '
        Me.mnuEliminarLayoutGrid.Caption = "Eliminar diseño grid"
        Me.mnuEliminarLayoutGrid.Id = 13
        Me.mnuEliminarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminarLayoutGrid.Name = "mnuEliminarLayoutGrid"
        '
        'chkDespachados
        '
        Me.chkDespachados.BindableChecked = True
        Me.chkDespachados.Caption = "Despachados"
        Me.chkDespachados.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.chkDespachados.Checked = True
        Me.chkDespachados.Id = 14
        Me.chkDespachados.Name = "chkDespachados"
        '
        'chkMostrarGridDetalle
        '
        Me.chkMostrarGridDetalle.Caption = "Mostrar grid de detalle"
        Me.chkMostrarGridDetalle.Id = 15
        Me.chkMostrarGridDetalle.Name = "chkMostrarGridDetalle"
        '
        'mnuGuardarLayoutGrid
        '
        Me.mnuGuardarLayoutGrid.Caption = "Guardar diseño de grid"
        Me.mnuGuardarLayoutGrid.Id = 17
        Me.mnuGuardarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardarLayoutGrid.Name = "mnuGuardarLayoutGrid"
        '
        'mnuEliminarPedido
        '
        Me.mnuEliminarPedido.Caption = "Eliminar Pedido"
        Me.mnuEliminarPedido.Id = 18
        Me.mnuEliminarPedido.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminarPedido.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminarPedido.Name = "mnuEliminarPedido"
        '
        'mnuExportarExcel
        '
        Me.mnuExportarExcel.Caption = "Exportar a Excel"
        Me.mnuExportarExcel.Id = 19
        Me.mnuExportarExcel.ImageOptions.SvgImage = CType(resources.GetObject("mnuExportarExcel.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuExportarExcel.Name = "mnuExportarExcel"
        '
        'chkSinExistencias
        '
        Me.chkSinExistencias.Caption = "Sin existencias WMS"
        Me.chkSinExistencias.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.chkSinExistencias.Id = 20
        Me.chkSinExistencias.Name = "chkSinExistencias"
        '
        'chkSinExistenciasERP
        '
        Me.chkSinExistenciasERP.Caption = "Sin existencias ERP"
        Me.chkSinExistenciasERP.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.chkSinExistenciasERP.Id = 21
        Me.chkSinExistenciasERP.Name = "chkSinExistenciasERP"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2, Me.RibbonPageGroup4})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Lista de Pedidos"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuNuevo)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuExportarExcel)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuMI3Sync)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardarLayoutGrid)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminarLayoutGrid)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminarPedido)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkActivos)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkAnulados)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkDespachados)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkSinExistencias)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkSinExistenciasERP)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkTemporales)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        Me.RibbonPageGroup2.Text = "Filtros"
        '
        'RibbonPageGroup4
        '
        Me.RibbonPageGroup4.ItemLinks.Add(Me.chkMostrarGridDetalle)
        Me.RibbonPageGroup4.Name = "RibbonPageGroup4"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar.ItemLinks.Add(Me.BarButtonItem1)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 686)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1345, 30)
        '
        'WorkspaceManager1
        '
        Me.WorkspaceManager1.TargetControl = Me
        Me.WorkspaceManager1.TransitionType = PushTransition1
        '
        'DgridPedido
        '
        Me.DgridPedido.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridPedido.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridPedido.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode1.RelationName = "Level1"
        Me.DgridPedido.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.DgridPedido.Location = New System.Drawing.Point(0, 0)
        Me.DgridPedido.MainView = Me.gviewEncabezadoPedido
        Me.DgridPedido.Margin = New System.Windows.Forms.Padding(4)
        Me.DgridPedido.MenuManager = Me.RibbonControl
        Me.DgridPedido.Name = "DgridPedido"
        Me.DgridPedido.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPictureEdit1})
        Me.DgridPedido.Size = New System.Drawing.Size(1345, 396)
        Me.DgridPedido.TabIndex = 1
        Me.DgridPedido.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gviewEncabezadoPedido})
        '
        'gviewEncabezadoPedido
        '
        Me.gviewEncabezadoPedido.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gviewEncabezadoPedido.Appearance.HeaderPanel.Options.UseFont = True
        Me.gviewEncabezadoPedido.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gviewEncabezadoPedido.Appearance.Row.Options.UseFont = True
        Me.gviewEncabezadoPedido.DetailHeight = 431
        Me.gviewEncabezadoPedido.GridControl = Me.DgridPedido
        Me.gviewEncabezadoPedido.Name = "gviewEncabezadoPedido"
        Me.gviewEncabezadoPedido.OptionsBehavior.Editable = False
        Me.gviewEncabezadoPedido.OptionsView.ColumnAutoWidth = False
        Me.gviewEncabezadoPedido.OptionsView.ShowAutoFilterRow = True
        Me.gviewEncabezadoPedido.OptionsView.ShowFooter = True
        '
        'RepositoryItemPictureEdit1
        '
        Me.RepositoryItemPictureEdit1.Name = "RepositoryItemPictureEdit1"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblHasta)
        Me.GroupBox1.Controls.Add(Me.lbldesde)
        Me.GroupBox1.Controls.Add(Me.dtpFechaAl)
        Me.GroupBox1.Controls.Add(Me.dtpFechaDel)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 193)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(1345, 68)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Tag = ""
        Me.GroupBox1.Text = "Filtro por Fecha"
        '
        'lblHasta
        '
        Me.lblHasta.AutoSize = True
        Me.lblHasta.Location = New System.Drawing.Point(191, 31)
        Me.lblHasta.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHasta.Name = "lblHasta"
        Me.lblHasta.Size = New System.Drawing.Size(18, 16)
        Me.lblHasta.TabIndex = 2
        Me.lblHasta.Text = "Al"
        '
        'lbldesde
        '
        Me.lbldesde.AutoSize = True
        Me.lbldesde.Location = New System.Drawing.Point(14, 31)
        Me.lbldesde.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbldesde.Name = "lbldesde"
        Me.lbldesde.Size = New System.Drawing.Size(25, 16)
        Me.lbldesde.TabIndex = 0
        Me.lbldesde.Text = "Del"
        '
        'dtpFechaAl
        '
        Me.dtpFechaAl.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaAl.Location = New System.Drawing.Point(217, 27)
        Me.dtpFechaAl.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFechaAl.Name = "dtpFechaAl"
        Me.dtpFechaAl.Size = New System.Drawing.Size(123, 23)
        Me.dtpFechaAl.TabIndex = 3
        '
        'dtpFechaDel
        '
        Me.dtpFechaDel.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaDel.Location = New System.Drawing.Point(47, 27)
        Me.dtpFechaDel.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFechaDel.Name = "dtpFechaDel"
        Me.dtpFechaDel.Size = New System.Drawing.Size(123, 23)
        Me.dtpFechaDel.TabIndex = 1
        '
        'ImageCollection1
        '
        Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImageCollection1.InsertGalleryImage("iconsetredtoblack4_16x16.png", "images/conditional%20formatting/iconsetredtoblack4_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/conditional%20formatting/iconsetredtoblack4_16x16.png"), 0)
        Me.ImageCollection1.Images.SetKeyName(0, "iconsetredtoblack4_16x16.png")
        Me.ImageCollection1.InsertGalleryImage("iconsetsigns3_16x16.png", "images/conditional%20formatting/iconsetsigns3_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/conditional%20formatting/iconsetsigns3_16x16.png"), 1)
        Me.ImageCollection1.Images.SetKeyName(1, "iconsetsigns3_16x16.png")
        Me.ImageCollection1.InsertGalleryImage("time_16x16.png", "images/scheduling/time_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/scheduling/time_16x16.png"), 2)
        Me.ImageCollection1.Images.SetKeyName(2, "time_16x16.png")
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.chkActivos)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'BarCheckItem1
        '
        Me.BarCheckItem1.BindableChecked = True
        Me.BarCheckItem1.Caption = "Activos"
        Me.BarCheckItem1.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.BarCheckItem1.Checked = True
        Me.BarCheckItem1.Id = 4
        Me.BarCheckItem1.Name = "BarCheckItem1"
        '
        'lblPrg
        '
        Me.lblPrg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblPrg.Location = New System.Drawing.Point(0, 657)
        Me.lblPrg.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.lblPrg.Name = "lblPrg"
        Me.lblPrg.Size = New System.Drawing.Size(1345, 29)
        Me.lblPrg.TabIndex = 4
        Me.lblPrg.Text = ""
        '
        'prg
        '
        Me.prg.Location = New System.Drawing.Point(915, 140)
        Me.prg.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(257, 23)
        Me.prg.TabIndex = 4
        Me.prg.Visible = False
        '
        'BarCheckItem3
        '
        Me.BarCheckItem3.Caption = "Anulados"
        Me.BarCheckItem3.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.BarCheckItem3.Id = 12
        Me.BarCheckItem3.Name = "BarCheckItem3"
        '
        'dgridDetalle
        '
        Me.dgridDetalle.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgridDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridDetalle.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode2.RelationName = "Level1"
        Me.dgridDetalle.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.dgridDetalle.Location = New System.Drawing.Point(0, 0)
        Me.dgridDetalle.MainView = Me.gviewDetallePedido
        Me.dgridDetalle.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridDetalle.MenuManager = Me.RibbonControl
        Me.dgridDetalle.Name = "dgridDetalle"
        Me.dgridDetalle.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPictureEdit2})
        Me.dgridDetalle.Size = New System.Drawing.Size(150, 46)
        Me.dgridDetalle.TabIndex = 7
        Me.dgridDetalle.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gviewDetallePedido})
        Me.dgridDetalle.Visible = False
        '
        'gviewDetallePedido
        '
        Me.gviewDetallePedido.DetailHeight = 431
        Me.gviewDetallePedido.GridControl = Me.dgridDetalle
        Me.gviewDetallePedido.Name = "gviewDetallePedido"
        Me.gviewDetallePedido.OptionsBehavior.Editable = False
        Me.gviewDetallePedido.OptionsView.ColumnAutoWidth = False
        Me.gviewDetallePedido.OptionsView.ShowAutoFilterRow = True
        Me.gviewDetallePedido.OptionsView.ShowFooter = True
        Me.gviewDetallePedido.OptionsView.ShowGroupPanel = False
        '
        'RepositoryItemPictureEdit2
        '
        Me.RepositoryItemPictureEdit2.Name = "RepositoryItemPictureEdit2"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 261)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DgridPedido)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgridDetalle)
        Me.SplitContainer1.Panel2Collapsed = True
        Me.SplitContainer1.Size = New System.Drawing.Size(1345, 396)
        Me.SplitContainer1.SplitterDistance = 198
        Me.SplitContainer1.SplitterWidth = 6
        Me.SplitContainer1.TabIndex = 10
        '
        'chkTemporales
        '
        Me.chkTemporales.Caption = "Temporales"
        Me.chkTemporales.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.chkTemporales.Id = 22
        Me.chkTemporales.Name = "chkTemporales"
        '
        'frmPedido_List
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1345, 716)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.prg)
        Me.Controls.Add(Me.lblPrg)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmPedido_List"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Lista de pedidos"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgridPedido, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gviewEncabezadoPedido, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgridDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gviewDetallePedido, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuNuevo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents DgridPedido As DevExpress.XtraGrid.GridControl
    Friend WithEvents gviewEncabezadoPedido As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkActivos As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents cmdImportarExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblHasta As Label
    Friend WithEvents lbldesde As Label
    Friend WithEvents dtpFechaAl As DateTimePicker
    Friend WithEvents dtpFechaDel As DateTimePicker
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMI3Sync As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Friend WithEvents RepositoryItemPictureEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents BarCheckItem2 As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents chkAnulados As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents BarCheckItem1 As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents lblPrg As RichTextBox
    Friend WithEvents prg As ProgressBar
    Friend WithEvents mnuEliminarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkDespachados As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents BarCheckItem3 As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents chkMostrarGridDetalle As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup4 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents dgridDetalle As DevExpress.XtraGrid.GridControl
    Friend WithEvents gviewDetallePedido As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemPictureEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents WorkspaceManager1 As DevExpress.Utils.WorkspaceManager
    Friend WithEvents mnuGuardarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminarPedido As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuExportarExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkSinExistencias As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents chkSinExistenciasERP As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents chkTemporales As DevExpress.XtraBars.BarCheckItem
End Class
