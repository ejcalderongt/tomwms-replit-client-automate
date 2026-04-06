<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmNotificacionPlantillaMnt
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmNotificacionPlantillaMnt))
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
        Me.gcPlantillas = New DevExpress.XtraGrid.GridControl()
        Me.gvPlantillas = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtBuscar = New DevExpress.XtraEditors.SearchControl()
        Me.LayoutMain = New DevExpress.XtraLayout.LayoutControl()
        Me.txtIdPlantilla = New DevExpress.XtraEditors.TextEdit()
        Me.lueEvento = New DevExpress.XtraEditors.LookUpEdit()
        Me.lueLayout = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtCodigoPlantilla = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombrePlantilla = New DevExpress.XtraEditors.TextEdit()
        Me.txtCanal = New DevExpress.XtraEditors.TextEdit()
        Me.txtVersionPlantilla = New DevExpress.XtraEditors.TextEdit()
        Me.txtAsuntoTemplate = New DevExpress.XtraEditors.MemoEdit()
        Me.txtBodyHtmlTemplate = New DevExpress.XtraEditors.MemoEdit()
        Me.chkUsaLayoutComun = New DevExpress.XtraEditors.CheckEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutDatos = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.txtIdPlantillaItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lueEventoItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lueLayoutItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtCodigoPlantillaItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtNombrePlantillaItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtCanalItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtVersionPlantillaItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtAsuntoTemplateItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtBodyHtmlTemplateItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.chkUsaLayoutComunItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.chkActivoItem = New DevExpress.XtraLayout.LayoutControlItem()
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
        CType(Me.gcPlantillas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPlantillas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBuscar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutMain.SuspendLayout()
        CType(Me.txtIdPlantilla.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueEvento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueLayout.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoPlantilla.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombrePlantilla.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCanal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVersionPlantilla.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAsuntoTemplate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBodyHtmlTemplate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkUsaLayoutComun.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdPlantillaItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueEventoItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueLayoutItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoPlantillaItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombrePlantillaItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCanalItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVersionPlantillaItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAsuntoTemplateItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBodyHtmlTemplateItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkUsaLayoutComunItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivoItem, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RibbonControl.Size = New System.Drawing.Size(1500, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuNuevo
        '
        Me.mnuNuevo.Caption = "Nuevo"
        Me.mnuNuevo.Id = 1
        Me.mnuNuevo.ImageOptions.SvgImage = CType(resources.GetObject("mnuNuevo.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuNuevo.Name = "mnuNuevo"
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 2
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'mnuRefrescar
        '
        Me.mnuRefrescar.Caption = "Refrescar"
        Me.mnuRefrescar.Id = 4
        Me.mnuRefrescar.ImageOptions.SvgImage = CType(resources.GetObject("mnuRefrescar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuRefrescar.Name = "mnuRefrescar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Mantenimiento de plantilla"
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 864)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1500, 30)
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 193)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        '
        'SplitContainerControl1.Panel1
        '
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.gcPlantillas)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.txtBuscar)
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.LayoutMain)
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1500, 671)
        Me.SplitContainerControl1.SplitterPosition = 520
        Me.SplitContainerControl1.TabIndex = 1
        '
        'gcPlantillas
        '
        Me.gcPlantillas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcPlantillas.Location = New System.Drawing.Point(0, 22)
        Me.gcPlantillas.MainView = Me.gvPlantillas
        Me.gcPlantillas.MenuManager = Me.RibbonControl
        Me.gcPlantillas.Name = "gcPlantillas"
        Me.gcPlantillas.Size = New System.Drawing.Size(520, 649)
        Me.gcPlantillas.TabIndex = 0
        Me.gcPlantillas.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvPlantillas})
        '
        'gvPlantillas
        '
        Me.gvPlantillas.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus
        Me.gvPlantillas.GridControl = Me.gcPlantillas
        Me.gvPlantillas.Name = "gvPlantillas"
        Me.gvPlantillas.OptionsBehavior.Editable = False
        Me.gvPlantillas.OptionsBehavior.ReadOnly = True
        Me.gvPlantillas.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvPlantillas.OptionsView.ShowGroupPanel = False
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
        Me.txtBuscar.Size = New System.Drawing.Size(520, 22)
        Me.txtBuscar.TabIndex = 1
        '
        'LayoutMain
        '
        Me.LayoutMain.Controls.Add(Me.txtIdPlantilla)
        Me.LayoutMain.Controls.Add(Me.lueEvento)
        Me.LayoutMain.Controls.Add(Me.lueLayout)
        Me.LayoutMain.Controls.Add(Me.txtCodigoPlantilla)
        Me.LayoutMain.Controls.Add(Me.txtNombrePlantilla)
        Me.LayoutMain.Controls.Add(Me.txtCanal)
        Me.LayoutMain.Controls.Add(Me.txtVersionPlantilla)
        Me.LayoutMain.Controls.Add(Me.txtAsuntoTemplate)
        Me.LayoutMain.Controls.Add(Me.txtBodyHtmlTemplate)
        Me.LayoutMain.Controls.Add(Me.chkUsaLayoutComun)
        Me.LayoutMain.Controls.Add(Me.chkActivo)
        Me.LayoutMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutMain.Location = New System.Drawing.Point(0, 0)
        Me.LayoutMain.Name = "LayoutMain"
        Me.LayoutMain.Root = Me.LayoutControlGroup1
        Me.LayoutMain.Size = New System.Drawing.Size(968, 671)
        Me.LayoutMain.TabIndex = 0
        '
        'txtIdPlantilla
        '
        Me.txtIdPlantilla.Enabled = False
        Me.txtIdPlantilla.Location = New System.Drawing.Point(110, 54)
        Me.txtIdPlantilla.MenuManager = Me.RibbonControl
        Me.txtIdPlantilla.Name = "txtIdPlantilla"
        Me.txtIdPlantilla.Size = New System.Drawing.Size(830, 22)
        Me.txtIdPlantilla.StyleController = Me.LayoutMain
        Me.txtIdPlantilla.TabIndex = 4
        '
        'lueEvento
        '
        Me.lueEvento.Location = New System.Drawing.Point(110, 621)
        Me.lueEvento.MenuManager = Me.RibbonControl
        Me.lueEvento.Name = "lueEvento"
        Me.lueEvento.Properties.NullText = ""
        Me.lueEvento.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoSearch
        Me.lueEvento.Properties.ShowHeader = False
        Me.lueEvento.Size = New System.Drawing.Size(830, 22)
        Me.lueEvento.StyleController = Me.LayoutMain
        Me.lueEvento.TabIndex = 5
        '
        'lueLayout
        '
        Me.lueLayout.Location = New System.Drawing.Point(110, 595)
        Me.lueLayout.MenuManager = Me.RibbonControl
        Me.lueLayout.Name = "lueLayout"
        Me.lueLayout.Properties.NullText = ""
        Me.lueLayout.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoSearch
        Me.lueLayout.Properties.ShowHeader = False
        Me.lueLayout.Size = New System.Drawing.Size(830, 22)
        Me.lueLayout.StyleController = Me.LayoutMain
        Me.lueLayout.TabIndex = 6
        '
        'txtCodigoPlantilla
        '
        Me.txtCodigoPlantilla.Location = New System.Drawing.Point(110, 569)
        Me.txtCodigoPlantilla.MenuManager = Me.RibbonControl
        Me.txtCodigoPlantilla.Name = "txtCodigoPlantilla"
        Me.txtCodigoPlantilla.Size = New System.Drawing.Size(830, 22)
        Me.txtCodigoPlantilla.StyleController = Me.LayoutMain
        Me.txtCodigoPlantilla.TabIndex = 7
        '
        'txtNombrePlantilla
        '
        Me.txtNombrePlantilla.Location = New System.Drawing.Point(110, 543)
        Me.txtNombrePlantilla.MenuManager = Me.RibbonControl
        Me.txtNombrePlantilla.Name = "txtNombrePlantilla"
        Me.txtNombrePlantilla.Size = New System.Drawing.Size(830, 22)
        Me.txtNombrePlantilla.StyleController = Me.LayoutMain
        Me.txtNombrePlantilla.TabIndex = 8
        '
        'txtCanal
        '
        Me.txtCanal.Location = New System.Drawing.Point(110, 517)
        Me.txtCanal.MenuManager = Me.RibbonControl
        Me.txtCanal.Name = "txtCanal"
        Me.txtCanal.Size = New System.Drawing.Size(830, 22)
        Me.txtCanal.StyleController = Me.LayoutMain
        Me.txtCanal.TabIndex = 9
        '
        'txtVersionPlantilla
        '
        Me.txtVersionPlantilla.Location = New System.Drawing.Point(110, 491)
        Me.txtVersionPlantilla.MenuManager = Me.RibbonControl
        Me.txtVersionPlantilla.Name = "txtVersionPlantilla"
        Me.txtVersionPlantilla.Size = New System.Drawing.Size(830, 22)
        Me.txtVersionPlantilla.StyleController = Me.LayoutMain
        Me.txtVersionPlantilla.TabIndex = 10
        '
        'txtAsuntoTemplate
        '
        Me.txtAsuntoTemplate.Location = New System.Drawing.Point(110, 200)
        Me.txtAsuntoTemplate.MenuManager = Me.RibbonControl
        Me.txtAsuntoTemplate.Name = "txtAsuntoTemplate"
        Me.txtAsuntoTemplate.Size = New System.Drawing.Size(830, 287)
        Me.txtAsuntoTemplate.StyleController = Me.LayoutMain
        Me.txtAsuntoTemplate.TabIndex = 11
        '
        'txtBodyHtmlTemplate
        '
        Me.txtBodyHtmlTemplate.Location = New System.Drawing.Point(110, 136)
        Me.txtBodyHtmlTemplate.MenuManager = Me.RibbonControl
        Me.txtBodyHtmlTemplate.Name = "txtBodyHtmlTemplate"
        Me.txtBodyHtmlTemplate.Size = New System.Drawing.Size(830, 60)
        Me.txtBodyHtmlTemplate.StyleController = Me.LayoutMain
        Me.txtBodyHtmlTemplate.TabIndex = 12
        '
        'chkUsaLayoutComun
        '
        Me.chkUsaLayoutComun.Location = New System.Drawing.Point(110, 108)
        Me.chkUsaLayoutComun.MenuManager = Me.RibbonControl
        Me.chkUsaLayoutComun.Name = "chkUsaLayoutComun"
        Me.chkUsaLayoutComun.Properties.Caption = ""
        Me.chkUsaLayoutComun.Size = New System.Drawing.Size(830, 24)
        Me.chkUsaLayoutComun.StyleController = Me.LayoutMain
        Me.chkUsaLayoutComun.TabIndex = 13
        '
        'chkActivo
        '
        Me.chkActivo.Location = New System.Drawing.Point(110, 80)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(830, 24)
        Me.chkActivo.StyleController = Me.LayoutMain
        Me.chkActivo.TabIndex = 14
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutDatos})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(968, 671)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutDatos
        '
        Me.LayoutDatos.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.txtIdPlantillaItem, Me.lueEventoItem, Me.lueLayoutItem, Me.txtCodigoPlantillaItem, Me.txtNombrePlantillaItem, Me.txtCanalItem, Me.txtVersionPlantillaItem, Me.txtAsuntoTemplateItem, Me.txtBodyHtmlTemplateItem, Me.chkUsaLayoutComunItem, Me.chkActivoItem})
        Me.LayoutDatos.Location = New System.Drawing.Point(0, 0)
        Me.LayoutDatos.Name = "LayoutDatos"
        Me.LayoutDatos.Size = New System.Drawing.Size(944, 647)
        Me.LayoutDatos.Text = "Datos de la plantilla"
        '
        'txtIdPlantillaItem
        '
        Me.txtIdPlantillaItem.Control = Me.txtIdPlantilla
        Me.txtIdPlantillaItem.Location = New System.Drawing.Point(0, 0)
        Me.txtIdPlantillaItem.Name = "txtIdPlantillaItem"
        Me.txtIdPlantillaItem.Size = New System.Drawing.Size(916, 26)
        Me.txtIdPlantillaItem.Text = "Id Plantilla:"
        Me.txtIdPlantillaItem.TextSize = New System.Drawing.Size(67, 16)
        '
        'lueEventoItem
        '
        Me.lueEventoItem.Control = Me.lueEvento
        Me.lueEventoItem.Location = New System.Drawing.Point(0, 567)
        Me.lueEventoItem.Name = "lueEventoItem"
        Me.lueEventoItem.Size = New System.Drawing.Size(916, 26)
        Me.lueEventoItem.Text = "Evento:"
        Me.lueEventoItem.TextSize = New System.Drawing.Size(67, 16)
        '
        'lueLayoutItem
        '
        Me.lueLayoutItem.Control = Me.lueLayout
        Me.lueLayoutItem.Location = New System.Drawing.Point(0, 541)
        Me.lueLayoutItem.Name = "lueLayoutItem"
        Me.lueLayoutItem.Size = New System.Drawing.Size(916, 26)
        Me.lueLayoutItem.Text = "Layout:"
        Me.lueLayoutItem.TextSize = New System.Drawing.Size(67, 16)
        '
        'txtCodigoPlantillaItem
        '
        Me.txtCodigoPlantillaItem.Control = Me.txtCodigoPlantilla
        Me.txtCodigoPlantillaItem.Location = New System.Drawing.Point(0, 515)
        Me.txtCodigoPlantillaItem.Name = "txtCodigoPlantillaItem"
        Me.txtCodigoPlantillaItem.Size = New System.Drawing.Size(916, 26)
        Me.txtCodigoPlantillaItem.Text = "Código:"
        Me.txtCodigoPlantillaItem.TextSize = New System.Drawing.Size(67, 16)
        '
        'txtNombrePlantillaItem
        '
        Me.txtNombrePlantillaItem.Control = Me.txtNombrePlantilla
        Me.txtNombrePlantillaItem.Location = New System.Drawing.Point(0, 489)
        Me.txtNombrePlantillaItem.Name = "txtNombrePlantillaItem"
        Me.txtNombrePlantillaItem.Size = New System.Drawing.Size(916, 26)
        Me.txtNombrePlantillaItem.Text = "Nombre:"
        Me.txtNombrePlantillaItem.TextSize = New System.Drawing.Size(67, 16)
        '
        'txtCanalItem
        '
        Me.txtCanalItem.Control = Me.txtCanal
        Me.txtCanalItem.Location = New System.Drawing.Point(0, 463)
        Me.txtCanalItem.Name = "txtCanalItem"
        Me.txtCanalItem.Size = New System.Drawing.Size(916, 26)
        Me.txtCanalItem.Text = "Canal:"
        Me.txtCanalItem.TextSize = New System.Drawing.Size(67, 16)
        '
        'txtVersionPlantillaItem
        '
        Me.txtVersionPlantillaItem.Control = Me.txtVersionPlantilla
        Me.txtVersionPlantillaItem.Location = New System.Drawing.Point(0, 437)
        Me.txtVersionPlantillaItem.Name = "txtVersionPlantillaItem"
        Me.txtVersionPlantillaItem.Size = New System.Drawing.Size(916, 26)
        Me.txtVersionPlantillaItem.Text = "Versión:"
        Me.txtVersionPlantillaItem.TextSize = New System.Drawing.Size(67, 16)
        '
        'txtAsuntoTemplateItem
        '
        Me.txtAsuntoTemplateItem.Control = Me.txtAsuntoTemplate
        Me.txtAsuntoTemplateItem.Location = New System.Drawing.Point(0, 146)
        Me.txtAsuntoTemplateItem.Name = "txtAsuntoTemplateItem"
        Me.txtAsuntoTemplateItem.Size = New System.Drawing.Size(916, 291)
        Me.txtAsuntoTemplateItem.Text = "Asunto:"
        Me.txtAsuntoTemplateItem.TextSize = New System.Drawing.Size(67, 16)
        '
        'txtBodyHtmlTemplateItem
        '
        Me.txtBodyHtmlTemplateItem.Control = Me.txtBodyHtmlTemplate
        Me.txtBodyHtmlTemplateItem.Location = New System.Drawing.Point(0, 82)
        Me.txtBodyHtmlTemplateItem.Name = "txtBodyHtmlTemplateItem"
        Me.txtBodyHtmlTemplateItem.Size = New System.Drawing.Size(916, 64)
        Me.txtBodyHtmlTemplateItem.Text = "Body Html:"
        Me.txtBodyHtmlTemplateItem.TextSize = New System.Drawing.Size(67, 16)
        '
        'chkUsaLayoutComunItem
        '
        Me.chkUsaLayoutComunItem.Control = Me.chkUsaLayoutComun
        Me.chkUsaLayoutComunItem.Location = New System.Drawing.Point(0, 54)
        Me.chkUsaLayoutComunItem.Name = "chkUsaLayoutComunItem"
        Me.chkUsaLayoutComunItem.Size = New System.Drawing.Size(916, 28)
        Me.chkUsaLayoutComunItem.Text = "Usa Layout:"
        Me.chkUsaLayoutComunItem.TextSize = New System.Drawing.Size(67, 16)
        '
        'chkActivoItem
        '
        Me.chkActivoItem.Control = Me.chkActivo
        Me.chkActivoItem.Location = New System.Drawing.Point(0, 26)
        Me.chkActivoItem.Name = "chkActivoItem"
        Me.chkActivoItem.Size = New System.Drawing.Size(916, 28)
        Me.chkActivoItem.Text = "Activo:"
        Me.chkActivoItem.TextSize = New System.Drawing.Size(67, 16)
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
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 838)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1500, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("b2bba94a-9ecf-4336-b1c5-601901001111")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 110)
        Me.DockPanel1.Size = New System.Drawing.Size(200, 200)
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
        Me.DockPanel1_Container.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(200, 100)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = New Date(2026, 3, 19, 0, 0, 0, 0)
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(0, 0)
        Me.Fec_modDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(125, 22)
        Me.Fec_modDateEdit.TabIndex = 0
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = New Date(2026, 3, 19, 0, 0, 0, 0)
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(0, 0)
        Me.Fec_agrDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(125, 22)
        Me.Fec_agrDateEdit.TabIndex = 1
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(0, 0)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(125, 22)
        Me.User_modTextEdit.TabIndex = 4
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(0, 0)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(125, 22)
        Me.User_agrTextEdit.TabIndex = 7
        '
        'FrmNotificacionPlantillaMnt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1500, 894)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmNotificacionPlantillaMnt"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Mantenimiento de plantilla de notificación"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.gcPlantillas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPlantillas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBuscar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutMain.ResumeLayout(False)
        CType(Me.txtIdPlantilla.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueEvento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueLayout.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoPlantilla.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombrePlantilla.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCanal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVersionPlantilla.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAsuntoTemplate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBodyHtmlTemplate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkUsaLayoutComun.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutDatos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdPlantillaItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueEventoItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueLayoutItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoPlantillaItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombrePlantillaItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCanalItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVersionPlantillaItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAsuntoTemplateItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBodyHtmlTemplateItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkUsaLayoutComunItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivoItem, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents gcPlantillas As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvPlantillas As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutMain As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents txtIdPlantilla As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lueEvento As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lueLayout As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtCodigoPlantilla As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombrePlantilla As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCanal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtVersionPlantilla As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtAsuntoTemplate As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtBodyHtmlTemplate As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents chkUsaLayoutComun As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutDatos As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents txtIdPlantillaItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lueEventoItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lueLayoutItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtCodigoPlantillaItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtNombrePlantillaItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtCanalItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtVersionPlantillaItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtAsuntoTemplateItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtBodyHtmlTemplateItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkUsaLayoutComunItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkActivoItem As DevExpress.XtraLayout.LayoutControlItem
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