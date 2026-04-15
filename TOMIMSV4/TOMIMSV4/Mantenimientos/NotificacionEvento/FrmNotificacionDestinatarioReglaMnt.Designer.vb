<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmNotificacionDestinatarioReglaMnt
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmNotificacionDestinatarioReglaMnt))
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
        Me.gcReglas = New DevExpress.XtraGrid.GridControl()
        Me.gvReglas = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtBuscar = New DevExpress.XtraEditors.SearchControl()
        Me.LayoutMain = New DevExpress.XtraLayout.LayoutControl()
        Me.txtIdReglaDestinatario = New DevExpress.XtraEditors.TextEdit()
        Me.lueEvento = New DevExpress.XtraEditors.LookUpEdit()
        Me.cboTipoDestinatario = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cboOrigenDestinatario = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtValorOrigen = New DevExpress.XtraEditors.TextEdit()
        Me.txtEmpresaCodigo = New DevExpress.XtraEditors.TextEdit()
        Me.txtSucursalCodigo = New DevExpress.XtraEditors.TextEdit()
        Me.txtTipoDocumento = New DevExpress.XtraEditors.TextEdit()
        Me.cboContextoBodega = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtCodigoBodegaFiltro = New DevExpress.XtraEditors.TextEdit()
        Me.spnPrioridad = New DevExpress.XtraEditors.SpinEdit()
        Me.chkRequiereCoincidenciaExacta = New DevExpress.XtraEditors.CheckEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.txtObservaciones = New DevExpress.XtraEditors.MemoEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutDatos = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.txtIdReglaDestinatarioItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lueEventoItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.cboTipoDestinatarioItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.cboOrigenDestinatarioItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtValorOrigenItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtEmpresaCodigoItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtSucursalCodigoItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtTipoDocumentoItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.cboContextoBodegaItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtCodigoBodegaFiltroItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.spnPrioridadItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.chkRequiereCoincidenciaExactaItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.chkActivoItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtObservacionesItem = New DevExpress.XtraLayout.LayoutControlItem()
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
        CType(Me.gcReglas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvReglas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBuscar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutMain.SuspendLayout()
        CType(Me.txtIdReglaDestinatario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueEvento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTipoDestinatario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboOrigenDestinatario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtValorOrigen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmpresaCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSucursalCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTipoDocumento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboContextoBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoBodegaFiltro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spnPrioridad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRequiereCoincidenciaExacta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtObservaciones.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdReglaDestinatarioItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueEventoItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTipoDestinatarioItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboOrigenDestinatarioItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtValorOrigenItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmpresaCodigoItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSucursalCodigoItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTipoDocumentoItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboContextoBodegaItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoBodegaFiltroItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spnPrioridadItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRequiereCoincidenciaExactaItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivoItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtObservacionesItem, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RibbonPage1.Text = "Mantenimiento de reglas"
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
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.gcReglas)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.txtBuscar)
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.LayoutMain)
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1500, 671)
        Me.SplitContainerControl1.SplitterPosition = 560
        Me.SplitContainerControl1.TabIndex = 1
        '
        'gcReglas
        '
        Me.gcReglas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcReglas.Location = New System.Drawing.Point(0, 22)
        Me.gcReglas.MainView = Me.gvReglas
        Me.gcReglas.MenuManager = Me.RibbonControl
        Me.gcReglas.Name = "gcReglas"
        Me.gcReglas.Size = New System.Drawing.Size(560, 649)
        Me.gcReglas.TabIndex = 0
        Me.gcReglas.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvReglas})
        '
        'gvReglas
        '
        Me.gvReglas.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus
        Me.gvReglas.GridControl = Me.gcReglas
        Me.gvReglas.Name = "gvReglas"
        Me.gvReglas.OptionsBehavior.Editable = False
        Me.gvReglas.OptionsBehavior.ReadOnly = True
        Me.gvReglas.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvReglas.OptionsView.ShowGroupPanel = False
        '
        'txtBuscar
        '
        Me.txtBuscar.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtBuscar.Location = New System.Drawing.Point(0, 0)
        Me.txtBuscar.MenuManager = Me.RibbonControl
        Me.txtBuscar.Name = "txtBuscar"
        Me.txtBuscar.Properties.NullValuePrompt = "Buscar por evento, tipo, origen o valor..."
        Me.txtBuscar.Properties.ShowClearButton = False
        Me.txtBuscar.Properties.ShowSearchButton = False
        Me.txtBuscar.Size = New System.Drawing.Size(560, 22)
        Me.txtBuscar.TabIndex = 1
        '
        'LayoutMain
        '
        Me.LayoutMain.Controls.Add(Me.txtIdReglaDestinatario)
        Me.LayoutMain.Controls.Add(Me.lueEvento)
        Me.LayoutMain.Controls.Add(Me.cboTipoDestinatario)
        Me.LayoutMain.Controls.Add(Me.cboOrigenDestinatario)
        Me.LayoutMain.Controls.Add(Me.txtValorOrigen)
        Me.LayoutMain.Controls.Add(Me.txtEmpresaCodigo)
        Me.LayoutMain.Controls.Add(Me.txtSucursalCodigo)
        Me.LayoutMain.Controls.Add(Me.txtTipoDocumento)
        Me.LayoutMain.Controls.Add(Me.cboContextoBodega)
        Me.LayoutMain.Controls.Add(Me.txtCodigoBodegaFiltro)
        Me.LayoutMain.Controls.Add(Me.spnPrioridad)
        Me.LayoutMain.Controls.Add(Me.chkRequiereCoincidenciaExacta)
        Me.LayoutMain.Controls.Add(Me.chkActivo)
        Me.LayoutMain.Controls.Add(Me.txtObservaciones)
        Me.LayoutMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutMain.Location = New System.Drawing.Point(0, 0)
        Me.LayoutMain.Name = "LayoutMain"
        Me.LayoutMain.Root = Me.LayoutControlGroup1
        Me.LayoutMain.Size = New System.Drawing.Size(928, 671)
        Me.LayoutMain.TabIndex = 0
        '
        'txtIdReglaDestinatario
        '
        Me.txtIdReglaDestinatario.Enabled = False
        Me.txtIdReglaDestinatario.Location = New System.Drawing.Point(160, 54)
        Me.txtIdReglaDestinatario.MenuManager = Me.RibbonControl
        Me.txtIdReglaDestinatario.Name = "txtIdReglaDestinatario"
        Me.txtIdReglaDestinatario.Size = New System.Drawing.Size(740, 22)
        Me.txtIdReglaDestinatario.StyleController = Me.LayoutMain
        Me.txtIdReglaDestinatario.TabIndex = 4
        '
        'lueEvento
        '
        Me.lueEvento.Location = New System.Drawing.Point(160, 621)
        Me.lueEvento.MenuManager = Me.RibbonControl
        Me.lueEvento.Name = "lueEvento"
        Me.lueEvento.Properties.NullText = ""
        Me.lueEvento.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoSearch
        Me.lueEvento.Properties.ShowHeader = False
        Me.lueEvento.Size = New System.Drawing.Size(740, 22)
        Me.lueEvento.StyleController = Me.LayoutMain
        Me.lueEvento.TabIndex = 5
        '
        'cboTipoDestinatario
        '
        Me.cboTipoDestinatario.Location = New System.Drawing.Point(160, 595)
        Me.cboTipoDestinatario.MenuManager = Me.RibbonControl
        Me.cboTipoDestinatario.Name = "cboTipoDestinatario"
        Me.cboTipoDestinatario.Properties.Items.AddRange(New Object() {"TO", "CC", "BCC"})
        Me.cboTipoDestinatario.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboTipoDestinatario.Size = New System.Drawing.Size(740, 22)
        Me.cboTipoDestinatario.StyleController = Me.LayoutMain
        Me.cboTipoDestinatario.TabIndex = 6
        '
        'cboOrigenDestinatario
        '
        Me.cboOrigenDestinatario.Location = New System.Drawing.Point(160, 569)
        Me.cboOrigenDestinatario.MenuManager = Me.RibbonControl
        Me.cboOrigenDestinatario.Name = "cboOrigenDestinatario"
        Me.cboOrigenDestinatario.Properties.Items.AddRange(New Object() {"FIJO", "CLIENTE", "BODEGA", "USUARIO", "ROL", "QUERY"})
        Me.cboOrigenDestinatario.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboOrigenDestinatario.Size = New System.Drawing.Size(740, 22)
        Me.cboOrigenDestinatario.StyleController = Me.LayoutMain
        Me.cboOrigenDestinatario.TabIndex = 7
        '
        'txtValorOrigen
        '
        Me.txtValorOrigen.Location = New System.Drawing.Point(160, 543)
        Me.txtValorOrigen.MenuManager = Me.RibbonControl
        Me.txtValorOrigen.Name = "txtValorOrigen"
        Me.txtValorOrigen.Size = New System.Drawing.Size(740, 22)
        Me.txtValorOrigen.StyleController = Me.LayoutMain
        Me.txtValorOrigen.TabIndex = 8
        '
        'txtEmpresaCodigo
        '
        Me.txtEmpresaCodigo.Location = New System.Drawing.Point(160, 517)
        Me.txtEmpresaCodigo.MenuManager = Me.RibbonControl
        Me.txtEmpresaCodigo.Name = "txtEmpresaCodigo"
        Me.txtEmpresaCodigo.Size = New System.Drawing.Size(740, 22)
        Me.txtEmpresaCodigo.StyleController = Me.LayoutMain
        Me.txtEmpresaCodigo.TabIndex = 9
        '
        'txtSucursalCodigo
        '
        Me.txtSucursalCodigo.Location = New System.Drawing.Point(160, 491)
        Me.txtSucursalCodigo.MenuManager = Me.RibbonControl
        Me.txtSucursalCodigo.Name = "txtSucursalCodigo"
        Me.txtSucursalCodigo.Size = New System.Drawing.Size(740, 22)
        Me.txtSucursalCodigo.StyleController = Me.LayoutMain
        Me.txtSucursalCodigo.TabIndex = 10
        '
        'txtTipoDocumento
        '
        Me.txtTipoDocumento.Location = New System.Drawing.Point(160, 465)
        Me.txtTipoDocumento.MenuManager = Me.RibbonControl
        Me.txtTipoDocumento.Name = "txtTipoDocumento"
        Me.txtTipoDocumento.Size = New System.Drawing.Size(740, 22)
        Me.txtTipoDocumento.StyleController = Me.LayoutMain
        Me.txtTipoDocumento.TabIndex = 11
        '
        'cboContextoBodega
        '
        Me.cboContextoBodega.Location = New System.Drawing.Point(160, 439)
        Me.cboContextoBodega.MenuManager = Me.RibbonControl
        Me.cboContextoBodega.Name = "cboContextoBodega"
        Me.cboContextoBodega.Properties.Items.AddRange(New Object() {"", "ORIGEN", "DESTINO", "AMBAS"})
        Me.cboContextoBodega.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboContextoBodega.Size = New System.Drawing.Size(740, 22)
        Me.cboContextoBodega.StyleController = Me.LayoutMain
        Me.cboContextoBodega.TabIndex = 12
        '
        'txtCodigoBodegaFiltro
        '
        Me.txtCodigoBodegaFiltro.Location = New System.Drawing.Point(160, 413)
        Me.txtCodigoBodegaFiltro.MenuManager = Me.RibbonControl
        Me.txtCodigoBodegaFiltro.Name = "txtCodigoBodegaFiltro"
        Me.txtCodigoBodegaFiltro.Size = New System.Drawing.Size(740, 22)
        Me.txtCodigoBodegaFiltro.StyleController = Me.LayoutMain
        Me.txtCodigoBodegaFiltro.TabIndex = 13
        '
        'spnPrioridad
        '
        Me.spnPrioridad.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.spnPrioridad.Location = New System.Drawing.Point(160, 387)
        Me.spnPrioridad.MenuManager = Me.RibbonControl
        Me.spnPrioridad.Name = "spnPrioridad"
        Me.spnPrioridad.Properties.IsFloatValue = False
        Me.spnPrioridad.Properties.Mask.EditMask = "N00"
        Me.spnPrioridad.Properties.MaxValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.spnPrioridad.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.spnPrioridad.Size = New System.Drawing.Size(740, 22)
        Me.spnPrioridad.StyleController = Me.LayoutMain
        Me.spnPrioridad.TabIndex = 14
        '
        'chkRequiereCoincidenciaExacta
        '
        Me.chkRequiereCoincidenciaExacta.Location = New System.Drawing.Point(160, 359)
        Me.chkRequiereCoincidenciaExacta.MenuManager = Me.RibbonControl
        Me.chkRequiereCoincidenciaExacta.Name = "chkRequiereCoincidenciaExacta"
        Me.chkRequiereCoincidenciaExacta.Properties.Caption = ""
        Me.chkRequiereCoincidenciaExacta.Size = New System.Drawing.Size(740, 24)
        Me.chkRequiereCoincidenciaExacta.StyleController = Me.LayoutMain
        Me.chkRequiereCoincidenciaExacta.TabIndex = 15
        '
        'chkActivo
        '
        Me.chkActivo.Location = New System.Drawing.Point(160, 331)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(740, 24)
        Me.chkActivo.StyleController = Me.LayoutMain
        Me.chkActivo.TabIndex = 16
        '
        'txtObservaciones
        '
        Me.txtObservaciones.Location = New System.Drawing.Point(160, 80)
        Me.txtObservaciones.MenuManager = Me.RibbonControl
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.Size = New System.Drawing.Size(740, 247)
        Me.txtObservaciones.StyleController = Me.LayoutMain
        Me.txtObservaciones.TabIndex = 17
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutDatos})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(928, 671)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutDatos
        '
        Me.LayoutDatos.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.txtIdReglaDestinatarioItem, Me.lueEventoItem, Me.cboTipoDestinatarioItem, Me.cboOrigenDestinatarioItem, Me.txtValorOrigenItem, Me.txtEmpresaCodigoItem, Me.txtSucursalCodigoItem, Me.txtTipoDocumentoItem, Me.cboContextoBodegaItem, Me.txtCodigoBodegaFiltroItem, Me.spnPrioridadItem, Me.chkRequiereCoincidenciaExactaItem, Me.chkActivoItem, Me.txtObservacionesItem})
        Me.LayoutDatos.Location = New System.Drawing.Point(0, 0)
        Me.LayoutDatos.Name = "LayoutDatos"
        Me.LayoutDatos.Size = New System.Drawing.Size(904, 647)
        Me.LayoutDatos.Text = "Datos de la regla"
        '
        'txtIdReglaDestinatarioItem
        '
        Me.txtIdReglaDestinatarioItem.Control = Me.txtIdReglaDestinatario
        Me.txtIdReglaDestinatarioItem.Location = New System.Drawing.Point(0, 0)
        Me.txtIdReglaDestinatarioItem.Name = "txtIdReglaDestinatarioItem"
        Me.txtIdReglaDestinatarioItem.Size = New System.Drawing.Size(876, 26)
        Me.txtIdReglaDestinatarioItem.Text = "Id:"
        Me.txtIdReglaDestinatarioItem.TextSize = New System.Drawing.Size(117, 16)
        '
        'lueEventoItem
        '
        Me.lueEventoItem.Control = Me.lueEvento
        Me.lueEventoItem.Location = New System.Drawing.Point(0, 567)
        Me.lueEventoItem.Name = "lueEventoItem"
        Me.lueEventoItem.Size = New System.Drawing.Size(876, 26)
        Me.lueEventoItem.Text = "Evento:"
        Me.lueEventoItem.TextSize = New System.Drawing.Size(117, 16)
        '
        'cboTipoDestinatarioItem
        '
        Me.cboTipoDestinatarioItem.Control = Me.cboTipoDestinatario
        Me.cboTipoDestinatarioItem.Location = New System.Drawing.Point(0, 541)
        Me.cboTipoDestinatarioItem.Name = "cboTipoDestinatarioItem"
        Me.cboTipoDestinatarioItem.Size = New System.Drawing.Size(876, 26)
        Me.cboTipoDestinatarioItem.Text = "Tipo Destinatario:"
        Me.cboTipoDestinatarioItem.TextSize = New System.Drawing.Size(117, 16)
        '
        'cboOrigenDestinatarioItem
        '
        Me.cboOrigenDestinatarioItem.Control = Me.cboOrigenDestinatario
        Me.cboOrigenDestinatarioItem.Location = New System.Drawing.Point(0, 515)
        Me.cboOrigenDestinatarioItem.Name = "cboOrigenDestinatarioItem"
        Me.cboOrigenDestinatarioItem.Size = New System.Drawing.Size(876, 26)
        Me.cboOrigenDestinatarioItem.Text = "Origen:"
        Me.cboOrigenDestinatarioItem.TextSize = New System.Drawing.Size(117, 16)
        '
        'txtValorOrigenItem
        '
        Me.txtValorOrigenItem.Control = Me.txtValorOrigen
        Me.txtValorOrigenItem.Location = New System.Drawing.Point(0, 489)
        Me.txtValorOrigenItem.Name = "txtValorOrigenItem"
        Me.txtValorOrigenItem.Size = New System.Drawing.Size(876, 26)
        Me.txtValorOrigenItem.Text = "Valor Origen:"
        Me.txtValorOrigenItem.TextSize = New System.Drawing.Size(117, 16)
        '
        'txtEmpresaCodigoItem
        '
        Me.txtEmpresaCodigoItem.Control = Me.txtEmpresaCodigo
        Me.txtEmpresaCodigoItem.Location = New System.Drawing.Point(0, 463)
        Me.txtEmpresaCodigoItem.Name = "txtEmpresaCodigoItem"
        Me.txtEmpresaCodigoItem.Size = New System.Drawing.Size(876, 26)
        Me.txtEmpresaCodigoItem.Text = "Empresa:"
        Me.txtEmpresaCodigoItem.TextSize = New System.Drawing.Size(117, 16)
        '
        'txtSucursalCodigoItem
        '
        Me.txtSucursalCodigoItem.Control = Me.txtSucursalCodigo
        Me.txtSucursalCodigoItem.Location = New System.Drawing.Point(0, 437)
        Me.txtSucursalCodigoItem.Name = "txtSucursalCodigoItem"
        Me.txtSucursalCodigoItem.Size = New System.Drawing.Size(876, 26)
        Me.txtSucursalCodigoItem.Text = "Sucursal:"
        Me.txtSucursalCodigoItem.TextSize = New System.Drawing.Size(117, 16)
        '
        'txtTipoDocumentoItem
        '
        Me.txtTipoDocumentoItem.Control = Me.txtTipoDocumento
        Me.txtTipoDocumentoItem.Location = New System.Drawing.Point(0, 411)
        Me.txtTipoDocumentoItem.Name = "txtTipoDocumentoItem"
        Me.txtTipoDocumentoItem.Size = New System.Drawing.Size(876, 26)
        Me.txtTipoDocumentoItem.Text = "Tipo Documento:"
        Me.txtTipoDocumentoItem.TextSize = New System.Drawing.Size(117, 16)
        '
        'cboContextoBodegaItem
        '
        Me.cboContextoBodegaItem.Control = Me.cboContextoBodega
        Me.cboContextoBodegaItem.Location = New System.Drawing.Point(0, 385)
        Me.cboContextoBodegaItem.Name = "cboContextoBodegaItem"
        Me.cboContextoBodegaItem.Size = New System.Drawing.Size(876, 26)
        Me.cboContextoBodegaItem.Text = "Contexto Bodega:"
        Me.cboContextoBodegaItem.TextSize = New System.Drawing.Size(117, 16)
        '
        'txtCodigoBodegaFiltroItem
        '
        Me.txtCodigoBodegaFiltroItem.Control = Me.txtCodigoBodegaFiltro
        Me.txtCodigoBodegaFiltroItem.Location = New System.Drawing.Point(0, 359)
        Me.txtCodigoBodegaFiltroItem.Name = "txtCodigoBodegaFiltroItem"
        Me.txtCodigoBodegaFiltroItem.Size = New System.Drawing.Size(876, 26)
        Me.txtCodigoBodegaFiltroItem.Text = "Bodega Filtro:"
        Me.txtCodigoBodegaFiltroItem.TextSize = New System.Drawing.Size(117, 16)
        '
        'spnPrioridadItem
        '
        Me.spnPrioridadItem.Control = Me.spnPrioridad
        Me.spnPrioridadItem.Location = New System.Drawing.Point(0, 333)
        Me.spnPrioridadItem.Name = "spnPrioridadItem"
        Me.spnPrioridadItem.Size = New System.Drawing.Size(876, 26)
        Me.spnPrioridadItem.Text = "Prioridad:"
        Me.spnPrioridadItem.TextSize = New System.Drawing.Size(117, 16)
        '
        'chkRequiereCoincidenciaExactaItem
        '
        Me.chkRequiereCoincidenciaExactaItem.Control = Me.chkRequiereCoincidenciaExacta
        Me.chkRequiereCoincidenciaExactaItem.Location = New System.Drawing.Point(0, 305)
        Me.chkRequiereCoincidenciaExactaItem.Name = "chkRequiereCoincidenciaExactaItem"
        Me.chkRequiereCoincidenciaExactaItem.Size = New System.Drawing.Size(876, 28)
        Me.chkRequiereCoincidenciaExactaItem.Text = "Coincidencia Exacta:"
        Me.chkRequiereCoincidenciaExactaItem.TextSize = New System.Drawing.Size(117, 16)
        '
        'chkActivoItem
        '
        Me.chkActivoItem.Control = Me.chkActivo
        Me.chkActivoItem.Location = New System.Drawing.Point(0, 277)
        Me.chkActivoItem.Name = "chkActivoItem"
        Me.chkActivoItem.Size = New System.Drawing.Size(876, 28)
        Me.chkActivoItem.Text = "Activo:"
        Me.chkActivoItem.TextSize = New System.Drawing.Size(117, 16)
        '
        'txtObservacionesItem
        '
        Me.txtObservacionesItem.Control = Me.txtObservaciones
        Me.txtObservacionesItem.Location = New System.Drawing.Point(0, 26)
        Me.txtObservacionesItem.Name = "txtObservacionesItem"
        Me.txtObservacionesItem.Size = New System.Drawing.Size(876, 251)
        Me.txtObservacionesItem.Text = "Observaciones:"
        Me.txtObservacionesItem.TextSize = New System.Drawing.Size(117, 16)
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
        Me.DockPanel1.ID = New System.Guid("3f4c9801-1234-4fe5-a123-123456781234")
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
        'FrmNotificacionDestinatarioReglaMnt
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
        Me.Name = "FrmNotificacionDestinatarioReglaMnt"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Mantenimiento de reglas de destinatarios"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.gcReglas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvReglas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBuscar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutMain.ResumeLayout(False)
        CType(Me.txtIdReglaDestinatario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueEvento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTipoDestinatario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboOrigenDestinatario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtValorOrigen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmpresaCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSucursalCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTipoDocumento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboContextoBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoBodegaFiltro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spnPrioridad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRequiereCoincidenciaExacta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtObservaciones.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutDatos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdReglaDestinatarioItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueEventoItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTipoDestinatarioItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboOrigenDestinatarioItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtValorOrigenItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmpresaCodigoItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSucursalCodigoItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTipoDocumentoItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboContextoBodegaItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoBodegaFiltroItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spnPrioridadItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRequiereCoincidenciaExactaItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivoItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtObservacionesItem, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents gcReglas As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvReglas As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutMain As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents txtIdReglaDestinatario As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lueEvento As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cboTipoDestinatario As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cboOrigenDestinatario As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtValorOrigen As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtEmpresaCodigo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtSucursalCodigo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTipoDocumento As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cboContextoBodega As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtCodigoBodegaFiltro As DevExpress.XtraEditors.TextEdit
    Friend WithEvents spnPrioridad As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents chkRequiereCoincidenciaExacta As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtObservaciones As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutDatos As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents txtIdReglaDestinatarioItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lueEventoItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cboTipoDestinatarioItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cboOrigenDestinatarioItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtValorOrigenItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtEmpresaCodigoItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtSucursalCodigoItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtTipoDocumentoItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cboContextoBodegaItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtCodigoBodegaFiltroItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents spnPrioridadItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkRequiereCoincidenciaExactaItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkActivoItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtObservacionesItem As DevExpress.XtraLayout.LayoutControlItem
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