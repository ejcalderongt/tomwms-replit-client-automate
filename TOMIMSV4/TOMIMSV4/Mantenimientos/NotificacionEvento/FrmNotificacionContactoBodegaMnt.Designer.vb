<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmNotificacionContactoBodegaMnt
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmNotificacionContactoBodegaMnt))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuNuevo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRefrescar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.gcContactos = New DevExpress.XtraGrid.GridControl()
        Me.gvContactos = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtBuscarContacto = New DevExpress.XtraEditors.SearchControl()
        Me.SplitContainerControl2 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.gcBodegas = New DevExpress.XtraGrid.GridControl()
        Me.gvBodegas = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutMain = New DevExpress.XtraLayout.LayoutControl()
        Me.txtIdContacto = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.txtCorreo = New DevExpress.XtraEditors.TextEdit()
        Me.cboTipoContacto = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.chkPermiteTo = New DevExpress.XtraEditors.CheckEdit()
        Me.chkPermiteCc = New DevExpress.XtraEditors.CheckEdit()
        Me.chkPermiteBcc = New DevExpress.XtraEditors.CheckEdit()
        Me.chkEsPrincipal = New DevExpress.XtraEditors.CheckEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.txtObservaciones = New DevExpress.XtraEditors.MemoEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutDatos = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.txtIdContactoItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtNombreItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtCorreoItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.cboTipoContactoItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.chkPermiteToItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.chkPermiteCcItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.chkPermiteBccItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.chkEsPrincipalItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.chkActivoItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtObservacionesItem = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.gcContactos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvContactos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBuscarContacto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl2.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl2.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.Panel2.SuspendLayout()
        Me.SplitContainerControl2.SuspendLayout()
        CType(Me.gcBodegas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvBodegas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutMain.SuspendLayout()
        CType(Me.txtIdContacto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCorreo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTipoContacto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermiteTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermiteCc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermiteBcc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEsPrincipal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtObservaciones.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdContactoItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCorreoItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTipoContactoItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermiteToItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermiteCcItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermiteBccItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEsPrincipalItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivoItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtObservacionesItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.RibbonPage1.Text = "Mantenimiento de contactos"
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 870)
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
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.gcContactos)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.txtBuscarContacto)
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.SplitContainerControl2)
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1500, 677)
        Me.SplitContainerControl1.SplitterPosition = 430
        Me.SplitContainerControl1.TabIndex = 0
        '
        'gcContactos
        '
        Me.gcContactos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcContactos.Location = New System.Drawing.Point(0, 22)
        Me.gcContactos.MainView = Me.gvContactos
        Me.gcContactos.MenuManager = Me.RibbonControl
        Me.gcContactos.Name = "gcContactos"
        Me.gcContactos.Size = New System.Drawing.Size(430, 655)
        Me.gcContactos.TabIndex = 0
        Me.gcContactos.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvContactos})
        '
        'gvContactos
        '
        Me.gvContactos.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus
        Me.gvContactos.GridControl = Me.gcContactos
        Me.gvContactos.Name = "gvContactos"
        Me.gvContactos.OptionsBehavior.Editable = False
        Me.gvContactos.OptionsBehavior.ReadOnly = True
        Me.gvContactos.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvContactos.OptionsView.ShowGroupPanel = False
        '
        'txtBuscarContacto
        '
        Me.txtBuscarContacto.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtBuscarContacto.Location = New System.Drawing.Point(0, 0)
        Me.txtBuscarContacto.MenuManager = Me.RibbonControl
        Me.txtBuscarContacto.Name = "txtBuscarContacto"
        Me.txtBuscarContacto.Properties.NullValuePrompt = "Buscar por nombre, correo o tipo..."
        Me.txtBuscarContacto.Properties.ShowClearButton = False
        Me.txtBuscarContacto.Properties.ShowSearchButton = False
        Me.txtBuscarContacto.Size = New System.Drawing.Size(430, 22)
        Me.txtBuscarContacto.TabIndex = 1
        '
        'SplitContainerControl2
        '
        Me.SplitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl2.Horizontal = False
        Me.SplitContainerControl2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl2.Name = "SplitContainerControl2"
        '
        'SplitContainerControl2.Panel1
        '
        Me.SplitContainerControl2.Panel1.Controls.Add(Me.gcBodegas)
        '
        'SplitContainerControl2.Panel2
        '
        Me.SplitContainerControl2.Panel2.Controls.Add(Me.LayoutMain)
        Me.SplitContainerControl2.Size = New System.Drawing.Size(1058, 677)
        Me.SplitContainerControl2.SplitterPosition = 340
        Me.SplitContainerControl2.TabIndex = 0
        '
        'gcBodegas
        '
        Me.gcBodegas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcBodegas.Location = New System.Drawing.Point(0, 0)
        Me.gcBodegas.MainView = Me.gvBodegas
        Me.gcBodegas.MenuManager = Me.RibbonControl
        Me.gcBodegas.Name = "gcBodegas"
        Me.gcBodegas.Size = New System.Drawing.Size(1058, 340)
        Me.gcBodegas.TabIndex = 0
        Me.gcBodegas.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvBodegas})
        '
        'gvBodegas
        '
        Me.gvBodegas.GridControl = Me.gcBodegas
        Me.gvBodegas.Name = "gvBodegas"
        Me.gvBodegas.OptionsBehavior.Editable = False
        Me.gvBodegas.OptionsView.ShowGroupPanel = False
        '
        'LayoutMain
        '
        Me.LayoutMain.Controls.Add(Me.txtIdContacto)
        Me.LayoutMain.Controls.Add(Me.txtNombre)
        Me.LayoutMain.Controls.Add(Me.txtCorreo)
        Me.LayoutMain.Controls.Add(Me.cboTipoContacto)
        Me.LayoutMain.Controls.Add(Me.chkPermiteTo)
        Me.LayoutMain.Controls.Add(Me.chkPermiteCc)
        Me.LayoutMain.Controls.Add(Me.chkPermiteBcc)
        Me.LayoutMain.Controls.Add(Me.chkEsPrincipal)
        Me.LayoutMain.Controls.Add(Me.chkActivo)
        Me.LayoutMain.Controls.Add(Me.txtObservaciones)
        Me.LayoutMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutMain.Location = New System.Drawing.Point(0, 0)
        Me.LayoutMain.Name = "LayoutMain"
        Me.LayoutMain.Root = Me.LayoutControlGroup1
        Me.LayoutMain.Size = New System.Drawing.Size(1058, 325)
        Me.LayoutMain.TabIndex = 0
        '
        'txtIdContacto
        '
        Me.txtIdContacto.Enabled = False
        Me.txtIdContacto.Location = New System.Drawing.Point(131, 54)
        Me.txtIdContacto.MenuManager = Me.RibbonControl
        Me.txtIdContacto.Name = "txtIdContacto"
        Me.txtIdContacto.Size = New System.Drawing.Size(878, 22)
        Me.txtIdContacto.StyleController = Me.LayoutMain
        Me.txtIdContacto.TabIndex = 4
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(131, 298)
        Me.txtNombre.MenuManager = Me.RibbonControl
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(878, 22)
        Me.txtNombre.StyleController = Me.LayoutMain
        Me.txtNombre.TabIndex = 5
        '
        'txtCorreo
        '
        Me.txtCorreo.Location = New System.Drawing.Point(131, 272)
        Me.txtCorreo.MenuManager = Me.RibbonControl
        Me.txtCorreo.Name = "txtCorreo"
        Me.txtCorreo.Size = New System.Drawing.Size(878, 22)
        Me.txtCorreo.StyleController = Me.LayoutMain
        Me.txtCorreo.TabIndex = 6
        '
        'cboTipoContacto
        '
        Me.cboTipoContacto.Location = New System.Drawing.Point(131, 246)
        Me.cboTipoContacto.MenuManager = Me.RibbonControl
        Me.cboTipoContacto.Name = "cboTipoContacto"
        Me.cboTipoContacto.Properties.Items.AddRange(New Object() {"OPERATIVO", "SUPERVISOR", "JEFE", "AUDITORIA", "DESPACHO", "RECEPCION"})
        Me.cboTipoContacto.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboTipoContacto.Size = New System.Drawing.Size(878, 22)
        Me.cboTipoContacto.StyleController = Me.LayoutMain
        Me.cboTipoContacto.TabIndex = 7
        '
        'chkPermiteTo
        '
        Me.chkPermiteTo.Location = New System.Drawing.Point(131, 218)
        Me.chkPermiteTo.MenuManager = Me.RibbonControl
        Me.chkPermiteTo.Name = "chkPermiteTo"
        Me.chkPermiteTo.Properties.Caption = ""
        Me.chkPermiteTo.Size = New System.Drawing.Size(878, 24)
        Me.chkPermiteTo.StyleController = Me.LayoutMain
        Me.chkPermiteTo.TabIndex = 8
        '
        'chkPermiteCc
        '
        Me.chkPermiteCc.Location = New System.Drawing.Point(131, 190)
        Me.chkPermiteCc.MenuManager = Me.RibbonControl
        Me.chkPermiteCc.Name = "chkPermiteCc"
        Me.chkPermiteCc.Properties.Caption = ""
        Me.chkPermiteCc.Size = New System.Drawing.Size(878, 24)
        Me.chkPermiteCc.StyleController = Me.LayoutMain
        Me.chkPermiteCc.TabIndex = 9
        '
        'chkPermiteBcc
        '
        Me.chkPermiteBcc.Location = New System.Drawing.Point(131, 162)
        Me.chkPermiteBcc.MenuManager = Me.RibbonControl
        Me.chkPermiteBcc.Name = "chkPermiteBcc"
        Me.chkPermiteBcc.Properties.Caption = ""
        Me.chkPermiteBcc.Size = New System.Drawing.Size(878, 24)
        Me.chkPermiteBcc.StyleController = Me.LayoutMain
        Me.chkPermiteBcc.TabIndex = 10
        '
        'chkEsPrincipal
        '
        Me.chkEsPrincipal.Location = New System.Drawing.Point(131, 134)
        Me.chkEsPrincipal.MenuManager = Me.RibbonControl
        Me.chkEsPrincipal.Name = "chkEsPrincipal"
        Me.chkEsPrincipal.Properties.Caption = ""
        Me.chkEsPrincipal.Size = New System.Drawing.Size(878, 24)
        Me.chkEsPrincipal.StyleController = Me.LayoutMain
        Me.chkEsPrincipal.TabIndex = 11
        '
        'chkActivo
        '
        Me.chkActivo.Location = New System.Drawing.Point(131, 106)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(878, 24)
        Me.chkActivo.StyleController = Me.LayoutMain
        Me.chkActivo.TabIndex = 12
        '
        'txtObservaciones
        '
        Me.txtObservaciones.Location = New System.Drawing.Point(131, 80)
        Me.txtObservaciones.MenuManager = Me.RibbonControl
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.Size = New System.Drawing.Size(878, 22)
        Me.txtObservaciones.StyleController = Me.LayoutMain
        Me.txtObservaciones.TabIndex = 13
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutDatos})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1037, 348)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutDatos
        '
        Me.LayoutDatos.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.txtIdContactoItem, Me.txtNombreItem, Me.txtCorreoItem, Me.cboTipoContactoItem, Me.chkPermiteToItem, Me.chkPermiteCcItem, Me.chkPermiteBccItem, Me.chkEsPrincipalItem, Me.chkActivoItem, Me.txtObservacionesItem})
        Me.LayoutDatos.Location = New System.Drawing.Point(0, 0)
        Me.LayoutDatos.Name = "LayoutDatos"
        Me.LayoutDatos.Size = New System.Drawing.Size(1013, 324)
        Me.LayoutDatos.Text = "Datos del contacto"
        '
        'txtIdContactoItem
        '
        Me.txtIdContactoItem.Control = Me.txtIdContacto
        Me.txtIdContactoItem.Location = New System.Drawing.Point(0, 0)
        Me.txtIdContactoItem.Name = "txtIdContactoItem"
        Me.txtIdContactoItem.Size = New System.Drawing.Size(985, 26)
        Me.txtIdContactoItem.Text = "Id:"
        Me.txtIdContactoItem.TextSize = New System.Drawing.Size(88, 16)
        '
        'txtNombreItem
        '
        Me.txtNombreItem.Control = Me.txtNombre
        Me.txtNombreItem.Location = New System.Drawing.Point(0, 244)
        Me.txtNombreItem.Name = "txtNombreItem"
        Me.txtNombreItem.Size = New System.Drawing.Size(985, 26)
        Me.txtNombreItem.Text = "Nombre:"
        Me.txtNombreItem.TextSize = New System.Drawing.Size(88, 16)
        '
        'txtCorreoItem
        '
        Me.txtCorreoItem.Control = Me.txtCorreo
        Me.txtCorreoItem.Location = New System.Drawing.Point(0, 218)
        Me.txtCorreoItem.Name = "txtCorreoItem"
        Me.txtCorreoItem.Size = New System.Drawing.Size(985, 26)
        Me.txtCorreoItem.Text = "Correo:"
        Me.txtCorreoItem.TextSize = New System.Drawing.Size(88, 16)
        '
        'cboTipoContactoItem
        '
        Me.cboTipoContactoItem.Control = Me.cboTipoContacto
        Me.cboTipoContactoItem.Location = New System.Drawing.Point(0, 192)
        Me.cboTipoContactoItem.Name = "cboTipoContactoItem"
        Me.cboTipoContactoItem.Size = New System.Drawing.Size(985, 26)
        Me.cboTipoContactoItem.Text = "Tipo Contacto:"
        Me.cboTipoContactoItem.TextSize = New System.Drawing.Size(88, 16)
        '
        'chkPermiteToItem
        '
        Me.chkPermiteToItem.Control = Me.chkPermiteTo
        Me.chkPermiteToItem.Location = New System.Drawing.Point(0, 164)
        Me.chkPermiteToItem.Name = "chkPermiteToItem"
        Me.chkPermiteToItem.Size = New System.Drawing.Size(985, 28)
        Me.chkPermiteToItem.Text = "Permite To:"
        Me.chkPermiteToItem.TextSize = New System.Drawing.Size(88, 16)
        '
        'chkPermiteCcItem
        '
        Me.chkPermiteCcItem.Control = Me.chkPermiteCc
        Me.chkPermiteCcItem.Location = New System.Drawing.Point(0, 136)
        Me.chkPermiteCcItem.Name = "chkPermiteCcItem"
        Me.chkPermiteCcItem.Size = New System.Drawing.Size(985, 28)
        Me.chkPermiteCcItem.Text = "Permite Cc:"
        Me.chkPermiteCcItem.TextSize = New System.Drawing.Size(88, 16)
        '
        'chkPermiteBccItem
        '
        Me.chkPermiteBccItem.Control = Me.chkPermiteBcc
        Me.chkPermiteBccItem.Location = New System.Drawing.Point(0, 108)
        Me.chkPermiteBccItem.Name = "chkPermiteBccItem"
        Me.chkPermiteBccItem.Size = New System.Drawing.Size(985, 28)
        Me.chkPermiteBccItem.Text = "Permite Bcc:"
        Me.chkPermiteBccItem.TextSize = New System.Drawing.Size(88, 16)
        '
        'chkEsPrincipalItem
        '
        Me.chkEsPrincipalItem.Control = Me.chkEsPrincipal
        Me.chkEsPrincipalItem.Location = New System.Drawing.Point(0, 80)
        Me.chkEsPrincipalItem.Name = "chkEsPrincipalItem"
        Me.chkEsPrincipalItem.Size = New System.Drawing.Size(985, 28)
        Me.chkEsPrincipalItem.Text = "Es Principal:"
        Me.chkEsPrincipalItem.TextSize = New System.Drawing.Size(88, 16)
        '
        'chkActivoItem
        '
        Me.chkActivoItem.Control = Me.chkActivo
        Me.chkActivoItem.Location = New System.Drawing.Point(0, 52)
        Me.chkActivoItem.Name = "chkActivoItem"
        Me.chkActivoItem.Size = New System.Drawing.Size(985, 28)
        Me.chkActivoItem.Text = "Activo:"
        Me.chkActivoItem.TextSize = New System.Drawing.Size(88, 16)
        '
        'txtObservacionesItem
        '
        Me.txtObservacionesItem.Control = Me.txtObservaciones
        Me.txtObservacionesItem.Location = New System.Drawing.Point(0, 26)
        Me.txtObservacionesItem.Name = "txtObservacionesItem"
        Me.txtObservacionesItem.Size = New System.Drawing.Size(985, 26)
        Me.txtObservacionesItem.Text = "Observaciones:"
        Me.txtObservacionesItem.TextSize = New System.Drawing.Size(88, 16)
        '
        'FrmNotificacionContactoBodegaMnt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1500, 900)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmNotificacionContactoBodegaMnt"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Mantenimiento de contactos por bodega"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.gcContactos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvContactos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBuscarContacto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitContainerControl2.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl2.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.ResumeLayout(False)
        CType(Me.gcBodegas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvBodegas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutMain.ResumeLayout(False)
        CType(Me.txtIdContacto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCorreo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTipoContacto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermiteTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermiteCc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermiteBcc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEsPrincipal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtObservaciones.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutDatos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdContactoItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCorreoItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTipoContactoItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermiteToItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermiteCcItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermiteBccItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEsPrincipalItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivoItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtObservacionesItem, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtBuscarContacto As DevExpress.XtraEditors.SearchControl
    Friend WithEvents gcContactos As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvContactos As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SplitContainerControl2 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gcBodegas As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvBodegas As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutMain As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents txtIdContacto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCorreo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cboTipoContacto As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents chkPermiteTo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkPermiteCc As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkPermiteBcc As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkEsPrincipal As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtObservaciones As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutDatos As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents txtIdContactoItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtNombreItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtCorreoItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cboTipoContactoItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkPermiteToItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkPermiteCcItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkPermiteBccItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkEsPrincipalItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkActivoItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtObservacionesItem As DevExpress.XtraLayout.LayoutControlItem
End Class