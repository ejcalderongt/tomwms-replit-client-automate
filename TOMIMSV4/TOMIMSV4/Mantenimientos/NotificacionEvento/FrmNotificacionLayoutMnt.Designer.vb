<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmNotificacionLayoutMnt
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmNotificacionLayoutMnt))
        Me.lblUserAgr = New System.Windows.Forms.Label()
        Me.lblFecAgr = New System.Windows.Forms.Label()
        Me.lblUserMod = New System.Windows.Forms.Label()
        Me.lblFecMod = New System.Windows.Forms.Label()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuNuevo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRefrescar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.gcLayouts = New DevExpress.XtraGrid.GridControl()
        Me.gvLayouts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtBuscar = New DevExpress.XtraEditors.SearchControl()
        Me.LayoutMain = New DevExpress.XtraLayout.LayoutControl()
        Me.txtIdLayout = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigoLayout = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreLayout = New DevExpress.XtraEditors.TextEdit()
        Me.txtHeaderHtml = New DevExpress.XtraEditors.MemoEdit()
        Me.txtFooterHtml = New DevExpress.XtraEditors.MemoEdit()
        Me.txtCssInline = New DevExpress.XtraEditors.MemoEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.chkEsDefault = New DevExpress.XtraEditors.CheckEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutDatos = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.txtIdLayoutItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtCodigoLayoutItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtNombreLayoutItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtHeaderHtmlItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtFooterHtmlItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtCssInlineItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.chkActivoItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.chkEsDefaultItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.dkBitacora = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.gcLayouts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvLayouts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBuscar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutMain.SuspendLayout()
        CType(Me.txtIdLayout.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoLayout.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreLayout.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHeaderHtml.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFooterHtml.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCssInline.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEsDefault.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdLayoutItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoLayoutItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreLayoutItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHeaderHtmlItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFooterHtmlItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCssInlineItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivoItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEsDefaultItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkBitacora, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblUserAgr
        '
        Me.lblUserAgr.AutoSize = True
        Me.lblUserAgr.Location = New System.Drawing.Point(55, 11)
        Me.lblUserAgr.Name = "lblUserAgr"
        Me.lblUserAgr.Size = New System.Drawing.Size(100, 16)
        Me.lblUserAgr.TabIndex = 3
        Me.lblUserAgr.Text = "Usuario Agregó:"
        '
        'lblFecAgr
        '
        Me.lblFecAgr.AutoSize = True
        Me.lblFecAgr.Location = New System.Drawing.Point(55, 43)
        Me.lblFecAgr.Name = "lblFecAgr"
        Me.lblFecAgr.Size = New System.Drawing.Size(91, 16)
        Me.lblFecAgr.TabIndex = 5
        Me.lblFecAgr.Text = "Fecha Agregó:"
        '
        'lblUserMod
        '
        Me.lblUserMod.AutoSize = True
        Me.lblUserMod.Location = New System.Drawing.Point(443, 11)
        Me.lblUserMod.Name = "lblUserMod"
        Me.lblUserMod.Size = New System.Drawing.Size(106, 16)
        Me.lblUserMod.TabIndex = 6
        Me.lblUserMod.Text = "Usuario Modificó:"
        '
        'lblFecMod
        '
        Me.lblFecMod.AutoSize = True
        Me.lblFecMod.Location = New System.Drawing.Point(443, 43)
        Me.lblFecMod.Name = "lblFecMod"
        Me.lblFecMod.Size = New System.Drawing.Size(97, 16)
        Me.lblFecMod.TabIndex = 2
        Me.lblFecMod.Text = "Fecha Modificó:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuNuevo, Me.mnuGuardar, Me.mnuEliminar, Me.mnuRefrescar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 10
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1360, 193)
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
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 2
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'mnuRefrescar
        '
        Me.mnuRefrescar.Caption = "Refrescar"
        Me.mnuRefrescar.Id = 4
        Me.mnuRefrescar.ImageOptions.SvgImage = CType(resources.GetObject("mnuRefrescar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuRefrescar.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
        Me.mnuRefrescar.Name = "mnuRefrescar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Mantenimiento de layout"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuNuevo)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuRefrescar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 804)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1360, 30)
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 193)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        '
        'SplitContainerControl1.Panel1
        '
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.gcLayouts)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.txtBuscar)
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.LayoutMain)
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1360, 611)
        Me.SplitContainerControl1.SplitterPosition = 500
        Me.SplitContainerControl1.TabIndex = 1
        '
        'gcLayouts
        '
        Me.gcLayouts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcLayouts.Location = New System.Drawing.Point(0, 22)
        Me.gcLayouts.MainView = Me.gvLayouts
        Me.gcLayouts.MenuManager = Me.RibbonControl
        Me.gcLayouts.Name = "gcLayouts"
        Me.gcLayouts.Size = New System.Drawing.Size(500, 589)
        Me.gcLayouts.TabIndex = 1
        Me.gcLayouts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvLayouts})
        '
        'gvLayouts
        '
        Me.gvLayouts.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus
        Me.gvLayouts.GridControl = Me.gcLayouts
        Me.gvLayouts.Name = "gvLayouts"
        Me.gvLayouts.OptionsBehavior.Editable = False
        Me.gvLayouts.OptionsBehavior.ReadOnly = True
        Me.gvLayouts.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvLayouts.OptionsView.ShowGroupPanel = False
        '
        'txtBuscar
        '
        Me.txtBuscar.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtBuscar.Location = New System.Drawing.Point(0, 0)
        Me.txtBuscar.MenuManager = Me.RibbonControl
        Me.txtBuscar.Name = "txtBuscar"
        Me.txtBuscar.Properties.NullValuePrompt = "Buscar por código o nombre..."
        Me.txtBuscar.Properties.ShowClearButton = False
        Me.txtBuscar.Properties.ShowSearchButton = False
        Me.txtBuscar.Size = New System.Drawing.Size(500, 22)
        Me.txtBuscar.TabIndex = 0
        '
        'LayoutMain
        '
        Me.LayoutMain.Controls.Add(Me.txtIdLayout)
        Me.LayoutMain.Controls.Add(Me.txtCodigoLayout)
        Me.LayoutMain.Controls.Add(Me.txtNombreLayout)
        Me.LayoutMain.Controls.Add(Me.txtHeaderHtml)
        Me.LayoutMain.Controls.Add(Me.txtFooterHtml)
        Me.LayoutMain.Controls.Add(Me.txtCssInline)
        Me.LayoutMain.Controls.Add(Me.chkActivo)
        Me.LayoutMain.Controls.Add(Me.chkEsDefault)
        Me.LayoutMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutMain.Location = New System.Drawing.Point(0, 0)
        Me.LayoutMain.Name = "LayoutMain"
        Me.LayoutMain.Root = Me.LayoutControlGroup1
        Me.LayoutMain.Size = New System.Drawing.Size(848, 611)
        Me.LayoutMain.TabIndex = 0
        '
        'txtIdLayout
        '
        Me.txtIdLayout.Enabled = False
        Me.txtIdLayout.Location = New System.Drawing.Point(119, 54)
        Me.txtIdLayout.MenuManager = Me.RibbonControl
        Me.txtIdLayout.Name = "txtIdLayout"
        Me.txtIdLayout.Size = New System.Drawing.Size(701, 22)
        Me.txtIdLayout.StyleController = Me.LayoutMain
        Me.txtIdLayout.TabIndex = 0
        '
        'txtCodigoLayout
        '
        Me.txtCodigoLayout.Location = New System.Drawing.Point(119, 80)
        Me.txtCodigoLayout.MenuManager = Me.RibbonControl
        Me.txtCodigoLayout.Name = "txtCodigoLayout"
        Me.txtCodigoLayout.Size = New System.Drawing.Size(701, 22)
        Me.txtCodigoLayout.StyleController = Me.LayoutMain
        Me.txtCodigoLayout.TabIndex = 1
        '
        'txtNombreLayout
        '
        Me.txtNombreLayout.Location = New System.Drawing.Point(119, 106)
        Me.txtNombreLayout.MenuManager = Me.RibbonControl
        Me.txtNombreLayout.Name = "txtNombreLayout"
        Me.txtNombreLayout.Size = New System.Drawing.Size(701, 22)
        Me.txtNombreLayout.StyleController = Me.LayoutMain
        Me.txtNombreLayout.TabIndex = 2
        '
        'txtHeaderHtml
        '
        Me.txtHeaderHtml.Location = New System.Drawing.Point(119, 132)
        Me.txtHeaderHtml.MenuManager = Me.RibbonControl
        Me.txtHeaderHtml.Name = "txtHeaderHtml"
        Me.txtHeaderHtml.Size = New System.Drawing.Size(701, 117)
        Me.txtHeaderHtml.StyleController = Me.LayoutMain
        Me.txtHeaderHtml.TabIndex = 3
        '
        'txtFooterHtml
        '
        Me.txtFooterHtml.Location = New System.Drawing.Point(119, 253)
        Me.txtFooterHtml.MenuManager = Me.RibbonControl
        Me.txtFooterHtml.Name = "txtFooterHtml"
        Me.txtFooterHtml.Size = New System.Drawing.Size(701, 118)
        Me.txtFooterHtml.StyleController = Me.LayoutMain
        Me.txtFooterHtml.TabIndex = 4
        '
        'txtCssInline
        '
        Me.txtCssInline.Location = New System.Drawing.Point(119, 375)
        Me.txtCssInline.MenuManager = Me.RibbonControl
        Me.txtCssInline.Name = "txtCssInline"
        Me.txtCssInline.Size = New System.Drawing.Size(701, 152)
        Me.txtCssInline.StyleController = Me.LayoutMain
        Me.txtCssInline.TabIndex = 5
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(119, 531)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(701, 24)
        Me.chkActivo.StyleController = Me.LayoutMain
        Me.chkActivo.TabIndex = 6
        '
        'chkEsDefault
        '
        Me.chkEsDefault.Location = New System.Drawing.Point(119, 559)
        Me.chkEsDefault.MenuManager = Me.RibbonControl
        Me.chkEsDefault.Name = "chkEsDefault"
        Me.chkEsDefault.Properties.Caption = ""
        Me.chkEsDefault.Size = New System.Drawing.Size(701, 24)
        Me.chkEsDefault.StyleController = Me.LayoutMain
        Me.chkEsDefault.TabIndex = 7
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutDatos})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(848, 611)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutDatos
        '
        Me.LayoutDatos.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.txtIdLayoutItem, Me.txtCodigoLayoutItem, Me.txtNombreLayoutItem, Me.txtHeaderHtmlItem, Me.txtFooterHtmlItem, Me.txtCssInlineItem, Me.chkActivoItem, Me.chkEsDefaultItem})
        Me.LayoutDatos.Location = New System.Drawing.Point(0, 0)
        Me.LayoutDatos.Name = "LayoutDatos"
        Me.LayoutDatos.Size = New System.Drawing.Size(824, 587)
        Me.LayoutDatos.Text = "Datos del layout"
        '
        'txtIdLayoutItem
        '
        Me.txtIdLayoutItem.Control = Me.txtIdLayout
        Me.txtIdLayoutItem.Location = New System.Drawing.Point(0, 0)
        Me.txtIdLayoutItem.Name = "txtIdLayoutItem"
        Me.txtIdLayoutItem.Size = New System.Drawing.Size(796, 26)
        Me.txtIdLayoutItem.Text = "Id Layout:"
        Me.txtIdLayoutItem.TextSize = New System.Drawing.Size(76, 16)
        '
        'txtCodigoLayoutItem
        '
        Me.txtCodigoLayoutItem.Control = Me.txtCodigoLayout
        Me.txtCodigoLayoutItem.Location = New System.Drawing.Point(0, 26)
        Me.txtCodigoLayoutItem.Name = "txtCodigoLayoutItem"
        Me.txtCodigoLayoutItem.Size = New System.Drawing.Size(796, 26)
        Me.txtCodigoLayoutItem.Text = "Código:"
        Me.txtCodigoLayoutItem.TextSize = New System.Drawing.Size(76, 16)
        '
        'txtNombreLayoutItem
        '
        Me.txtNombreLayoutItem.Control = Me.txtNombreLayout
        Me.txtNombreLayoutItem.Location = New System.Drawing.Point(0, 52)
        Me.txtNombreLayoutItem.Name = "txtNombreLayoutItem"
        Me.txtNombreLayoutItem.Size = New System.Drawing.Size(796, 26)
        Me.txtNombreLayoutItem.Text = "Nombre:"
        Me.txtNombreLayoutItem.TextSize = New System.Drawing.Size(76, 16)
        '
        'txtHeaderHtmlItem
        '
        Me.txtHeaderHtmlItem.Control = Me.txtHeaderHtml
        Me.txtHeaderHtmlItem.Location = New System.Drawing.Point(0, 78)
        Me.txtHeaderHtmlItem.Name = "txtHeaderHtmlItem"
        Me.txtHeaderHtmlItem.Size = New System.Drawing.Size(796, 121)
        Me.txtHeaderHtmlItem.Text = "Header Html:"
        Me.txtHeaderHtmlItem.TextSize = New System.Drawing.Size(76, 16)
        '
        'txtFooterHtmlItem
        '
        Me.txtFooterHtmlItem.Control = Me.txtFooterHtml
        Me.txtFooterHtmlItem.Location = New System.Drawing.Point(0, 199)
        Me.txtFooterHtmlItem.Name = "txtFooterHtmlItem"
        Me.txtFooterHtmlItem.Size = New System.Drawing.Size(796, 122)
        Me.txtFooterHtmlItem.Text = "Footer Html:"
        Me.txtFooterHtmlItem.TextSize = New System.Drawing.Size(76, 16)
        '
        'txtCssInlineItem
        '
        Me.txtCssInlineItem.Control = Me.txtCssInline
        Me.txtCssInlineItem.Location = New System.Drawing.Point(0, 321)
        Me.txtCssInlineItem.Name = "txtCssInlineItem"
        Me.txtCssInlineItem.Size = New System.Drawing.Size(796, 156)
        Me.txtCssInlineItem.Text = "Css Inline:"
        Me.txtCssInlineItem.TextSize = New System.Drawing.Size(76, 16)
        '
        'chkActivoItem
        '
        Me.chkActivoItem.Control = Me.chkActivo
        Me.chkActivoItem.Location = New System.Drawing.Point(0, 477)
        Me.chkActivoItem.Name = "chkActivoItem"
        Me.chkActivoItem.Size = New System.Drawing.Size(796, 28)
        Me.chkActivoItem.Text = "Activo:"
        Me.chkActivoItem.TextSize = New System.Drawing.Size(76, 16)
        '
        'chkEsDefaultItem
        '
        Me.chkEsDefaultItem.Control = Me.chkEsDefault
        Me.chkEsDefaultItem.Location = New System.Drawing.Point(0, 505)
        Me.chkEsDefaultItem.Name = "chkEsDefaultItem"
        Me.chkEsDefaultItem.Size = New System.Drawing.Size(796, 28)
        Me.chkEsDefaultItem.Text = "Es Default:"
        Me.chkEsDefaultItem.TextSize = New System.Drawing.Size(76, 16)
        '
        'dkBitacora
        '
        Me.dkBitacora.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkBitacora.Form = Me
        Me.dkBitacora.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 778)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1360, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("b5794ab4-7b15-48ba-9a6f-76d4c9981111")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 110)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1360, 110)
        Me.DockPanel1.Text = "Bitácora"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.Fec_modDateEdit)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_agrDateEdit)
        Me.DockPanel1_Container.Controls.Add(Me.lblFecMod)
        Me.DockPanel1_Container.Controls.Add(Me.lblUserAgr)
        Me.DockPanel1_Container.Controls.Add(Me.User_modTextEdit)
        Me.DockPanel1_Container.Controls.Add(Me.lblFecAgr)
        Me.DockPanel1_Container.Controls.Add(Me.lblUserMod)
        Me.DockPanel1_Container.Controls.Add(Me.User_agrTextEdit)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 34)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1352, 72)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(546, 39)
        Me.Fec_modDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_modDateEdit.TabIndex = 0
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(157, 39)
        Me.Fec_agrDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_agrDateEdit.TabIndex = 1
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(546, 7)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 4
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(157, 7)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_agrTextEdit.TabIndex = 7
        '
        'FrmNotificacionLayoutMnt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1360, 834)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmNotificacionLayoutMnt"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Mantenimiento de layout de notificación"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.gcLayouts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvLayouts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBuscar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutMain.ResumeLayout(False)
        CType(Me.txtIdLayout.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoLayout.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreLayout.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHeaderHtml.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFooterHtml.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCssInline.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEsDefault.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutDatos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdLayoutItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoLayoutItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreLayoutItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHeaderHtmlItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFooterHtmlItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCssInlineItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivoItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEsDefaultItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkBitacora, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuNuevo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuRefrescar As DevExpress.XtraBars.BarButtonItem

    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents txtBuscar As DevExpress.XtraEditors.SearchControl
    Friend WithEvents gcLayouts As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvLayouts As DevExpress.XtraGrid.Views.Grid.GridView

    Friend WithEvents LayoutMain As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents txtIdLayout As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodigoLayout As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreLayout As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtHeaderHtml As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtFooterHtml As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtCssInline As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkEsDefault As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutDatos As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents txtIdLayoutItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtCodigoLayoutItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtNombreLayoutItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtHeaderHtmlItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtFooterHtmlItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtCssInlineItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkActivoItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkEsDefaultItem As DevExpress.XtraLayout.LayoutControlItem

    Friend WithEvents dkBitacora As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblUserAgr As Label
    Friend WithEvents lblFecAgr As Label
    Friend WithEvents lblUserMod As Label
    Friend WithEvents lblFecMod As Label
End Class