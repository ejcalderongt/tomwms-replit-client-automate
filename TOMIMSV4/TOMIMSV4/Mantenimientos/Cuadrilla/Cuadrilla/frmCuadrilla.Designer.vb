<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCuadrilla
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
        Dim lblIdCuadrilla As System.Windows.Forms.Label
        Dim lblNombre As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim lblBodega As System.Windows.Forms.Label
        Dim lblTipo As System.Windows.Forms.Label
        Dim lblActivo As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCuadrilla))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.RibbonStatusBar1 = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.RibbonStatusBar2 = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.BarStaticItem1 = New DevExpress.XtraBars.BarStaticItem()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.lblCostoPorHora = New DevExpress.XtraEditors.LabelControl()
        Me.TabPane1 = New DevExpress.XtraBars.Navigation.TabPane()
        Me.TabNavigationPage1 = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.GroupControl13 = New DevExpress.XtraEditors.GroupControl()
        Me.DGridOperadores = New DevExpress.XtraGrid.GridControl()
        Me.GrdOperadorBobega = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.TabNavigationPage2 = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.DgridMontaCargas = New DevExpress.XtraGrid.GridControl()
        Me.grdviewMontaCarga = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.cmbTipoCuadrilla = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtIdCuadrilla = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.dkCamara = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.Cliente_direccionTableAdapter1 = New cliente_direccion_dsetTableAdapters.cliente_direccionTableAdapter()
        lblIdCuadrilla = New System.Windows.Forms.Label()
        lblNombre = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        lblBodega = New System.Windows.Forms.Label()
        lblTipo = New System.Windows.Forms.Label()
        lblActivo = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.TabPane1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPane1.SuspendLayout()
        Me.TabNavigationPage1.SuspendLayout()
        CType(Me.GroupControl13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl13.SuspendLayout()
        CType(Me.DGridOperadores, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdOperadorBobega, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabNavigationPage2.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.DgridMontaCargas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdviewMontaCarga, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTipoCuadrilla.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdCuadrilla.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkCamara, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblIdCuadrilla
        '
        lblIdCuadrilla.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblIdCuadrilla.AutoSize = True
        lblIdCuadrilla.Location = New System.Drawing.Point(26, 46)
        lblIdCuadrilla.Name = "lblIdCuadrilla"
        lblIdCuadrilla.Size = New System.Drawing.Size(64, 13)
        lblIdCuadrilla.TabIndex = 0
        lblIdCuadrilla.Text = "Correlativo:"
        '
        'lblNombre
        '
        lblNombre.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblNombre.AutoSize = True
        lblNombre.Location = New System.Drawing.Point(26, 119)
        lblNombre.Name = "lblNombre"
        lblNombre.Size = New System.Drawing.Size(48, 13)
        lblNombre.TabIndex = 4
        lblNombre.Text = "Nombre:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(30, 32)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(78, 13)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(30, 6)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(85, 13)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(433, 32)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(82, 13)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(433, 6)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(89, 13)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'lblBodega
        '
        lblBodega.AutoSize = True
        lblBodega.Location = New System.Drawing.Point(26, 70)
        lblBodega.Name = "lblBodega"
        lblBodega.Size = New System.Drawing.Size(47, 13)
        lblBodega.TabIndex = 17
        lblBodega.Text = "Bodega:"
        '
        'lblTipo
        '
        lblTipo.AutoSize = True
        lblTipo.Location = New System.Drawing.Point(26, 94)
        lblTipo.Name = "lblTipo"
        lblTipo.Size = New System.Drawing.Size(31, 13)
        lblTipo.TabIndex = 21
        lblTipo.Text = "Tipo:"
        '
        'lblActivo
        '
        lblActivo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblActivo.AutoSize = True
        lblActivo.Location = New System.Drawing.Point(26, 144)
        lblActivo.Name = "lblActivo"
        lblActivo.Size = New System.Drawing.Size(41, 13)
        lblActivo.TabIndex = 24
        lblActivo.Text = "Activo:"
        '
        'RibbonControl
        '
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(26, 24, 26, 24)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 5
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 283
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(817, 158)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar2
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
        Me.mnuGuardar.Name = "mnuGuardar"
        Me.mnuGuardar.ShortcutKeyDisplayString = "G"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menu"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 710)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(817, 24)
        '
        'RibbonStatusBar1
        '
        Me.RibbonStatusBar1.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar1.Location = New System.Drawing.Point(2, 243)
        Me.RibbonStatusBar1.Name = "RibbonStatusBar1"
        Me.RibbonStatusBar1.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar1.Size = New System.Drawing.Size(809, 27)
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros:"
        Me.lblRegs.Id = 20
        Me.lblRegs.Name = "lblRegs"
        '
        'RibbonStatusBar2
        '
        Me.RibbonStatusBar2.ItemLinks.Add(Me.BarStaticItem1)
        Me.RibbonStatusBar2.Location = New System.Drawing.Point(2, 246)
        Me.RibbonStatusBar2.Name = "RibbonStatusBar2"
        Me.RibbonStatusBar2.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar2.Size = New System.Drawing.Size(809, 27)
        '
        'BarStaticItem1
        '
        Me.BarStaticItem1.Caption = "Registros:"
        Me.BarStaticItem1.Id = 20
        Me.BarStaticItem1.Name = "BarStaticItem1"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.lblCostoPorHora)
        Me.GroupControl1.Controls.Add(Me.TabPane1)
        Me.GroupControl1.Controls.Add(lblActivo)
        Me.GroupControl1.Controls.Add(Me.chkActivo)
        Me.GroupControl1.Controls.Add(Me.cmbTipoCuadrilla)
        Me.GroupControl1.Controls.Add(lblTipo)
        Me.GroupControl1.Controls.Add(Me.cmbBodega)
        Me.GroupControl1.Controls.Add(lblBodega)
        Me.GroupControl1.Controls.Add(Me.txtIdCuadrilla)
        Me.GroupControl1.Controls.Add(lblNombre)
        Me.GroupControl1.Controls.Add(Me.txtNombre)
        Me.GroupControl1.Controls.Add(lblIdCuadrilla)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 158)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(817, 531)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Datos Cámara"
        '
        'lblCostoPorHora
        '
        Me.lblCostoPorHora.Appearance.Font = New System.Drawing.Font("Tahoma", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostoPorHora.Appearance.Options.UseFont = True
        Me.lblCostoPorHora.Appearance.Options.UseTextOptions = True
        Me.lblCostoPorHora.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblCostoPorHora.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblCostoPorHora.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblCostoPorHora.Location = New System.Drawing.Point(2, 189)
        Me.lblCostoPorHora.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblCostoPorHora.Name = "lblCostoPorHora"
        Me.lblCostoPorHora.Size = New System.Drawing.Size(813, 35)
        Me.lblCostoPorHora.TabIndex = 27
        Me.lblCostoPorHora.Text = "Q 000.00"
        '
        'TabPane1
        '
        Me.TabPane1.Controls.Add(Me.TabNavigationPage1)
        Me.TabPane1.Controls.Add(Me.TabNavigationPage2)
        Me.TabPane1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TabPane1.Location = New System.Drawing.Point(2, 224)
        Me.TabPane1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPane1.Name = "TabPane1"
        Me.TabPane1.Pages.AddRange(New DevExpress.XtraBars.Navigation.NavigationPageBase() {Me.TabNavigationPage1, Me.TabNavigationPage2})
        Me.TabPane1.RegularSize = New System.Drawing.Size(813, 305)
        Me.TabPane1.SelectedPage = Me.TabNavigationPage1
        Me.TabPane1.Size = New System.Drawing.Size(813, 305)
        Me.TabPane1.TabIndex = 26
        '
        'TabNavigationPage1
        '
        Me.TabNavigationPage1.Caption = "Dimensión de carga operativa (DCO)"
        Me.TabNavigationPage1.Controls.Add(Me.GroupControl13)
        Me.TabNavigationPage1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabNavigationPage1.Name = "TabNavigationPage1"
        Me.TabNavigationPage1.Size = New System.Drawing.Size(813, 272)
        '
        'GroupControl13
        '
        Me.GroupControl13.Controls.Add(Me.RibbonStatusBar1)
        Me.GroupControl13.Controls.Add(Me.DGridOperadores)
        Me.GroupControl13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl13.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl13.Name = "GroupControl13"
        Me.GroupControl13.Size = New System.Drawing.Size(813, 272)
        Me.GroupControl13.TabIndex = 23
        Me.GroupControl13.Text = "Selección de Operador"
        '
        'DGridOperadores
        '
        Me.DGridOperadores.Cursor = System.Windows.Forms.Cursors.Default
        Me.DGridOperadores.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGridOperadores.Location = New System.Drawing.Point(2, 23)
        Me.DGridOperadores.MainView = Me.GrdOperadorBobega
        Me.DGridOperadores.Name = "DGridOperadores"
        Me.DGridOperadores.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2})
        Me.DGridOperadores.Size = New System.Drawing.Size(809, 247)
        Me.DGridOperadores.TabIndex = 0
        Me.DGridOperadores.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GrdOperadorBobega})
        '
        'GrdOperadorBobega
        '
        Me.GrdOperadorBobega.DetailHeight = 284
        Me.GrdOperadorBobega.GridControl = Me.DGridOperadores
        Me.GrdOperadorBobega.Name = "GrdOperadorBobega"
        Me.GrdOperadorBobega.OptionsEditForm.PopupEditFormWidth = 686
        Me.GrdOperadorBobega.OptionsFind.AlwaysVisible = True
        Me.GrdOperadorBobega.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.[True]
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'TabNavigationPage2
        '
        Me.TabNavigationPage2.Caption = "Dimensión de requerimiento de equipo (DRE)"
        Me.TabNavigationPage2.Controls.Add(Me.GroupControl2)
        Me.TabNavigationPage2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabNavigationPage2.Name = "TabNavigationPage2"
        Me.TabNavigationPage2.Size = New System.Drawing.Size(813, 275)
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.RibbonStatusBar2)
        Me.GroupControl2.Controls.Add(Me.DgridMontaCargas)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(813, 275)
        Me.GroupControl2.TabIndex = 24
        Me.GroupControl2.Text = "Selección de Equipo"
        '
        'DgridMontaCargas
        '
        Me.DgridMontaCargas.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridMontaCargas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridMontaCargas.Location = New System.Drawing.Point(2, 23)
        Me.DgridMontaCargas.MainView = Me.grdviewMontaCarga
        Me.DgridMontaCargas.Name = "DgridMontaCargas"
        Me.DgridMontaCargas.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit3, Me.RepositoryItemCheckEdit4})
        Me.DgridMontaCargas.Size = New System.Drawing.Size(809, 250)
        Me.DgridMontaCargas.TabIndex = 0
        Me.DgridMontaCargas.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdviewMontaCarga})
        '
        'grdviewMontaCarga
        '
        Me.grdviewMontaCarga.DetailHeight = 284
        Me.grdviewMontaCarga.GridControl = Me.DgridMontaCargas
        Me.grdviewMontaCarga.Name = "grdviewMontaCarga"
        Me.grdviewMontaCarga.OptionsEditForm.PopupEditFormWidth = 686
        Me.grdviewMontaCarga.OptionsFind.AlwaysVisible = True
        Me.grdviewMontaCarga.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.[True]
        '
        'RepositoryItemCheckEdit3
        '
        Me.RepositoryItemCheckEdit3.AutoHeight = False
        Me.RepositoryItemCheckEdit3.Name = "RepositoryItemCheckEdit3"
        '
        'RepositoryItemCheckEdit4
        '
        Me.RepositoryItemCheckEdit4.AutoHeight = False
        Me.RepositoryItemCheckEdit4.Name = "RepositoryItemCheckEdit4"
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(99, 141)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(26, 20)
        Me.chkActivo.TabIndex = 25
        '
        'cmbTipoCuadrilla
        '
        Me.cmbTipoCuadrilla.Location = New System.Drawing.Point(99, 92)
        Me.cmbTipoCuadrilla.Name = "cmbTipoCuadrilla"
        Me.cmbTipoCuadrilla.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbTipoCuadrilla.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoCuadrilla.Properties.NullText = ""
        Me.cmbTipoCuadrilla.Size = New System.Drawing.Size(207, 20)
        Me.cmbTipoCuadrilla.TabIndex = 22
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(99, 67)
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(207, 20)
        Me.cmbBodega.TabIndex = 18
        '
        'txtIdCuadrilla
        '
        Me.txtIdCuadrilla.Location = New System.Drawing.Point(99, 43)
        Me.txtIdCuadrilla.MenuManager = Me.RibbonControl
        Me.txtIdCuadrilla.Name = "txtIdCuadrilla"
        Me.txtIdCuadrilla.Size = New System.Drawing.Size(207, 20)
        Me.txtIdCuadrilla.TabIndex = 16
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(99, 116)
        Me.txtNombre.MenuManager = Me.RibbonControl
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(207, 20)
        Me.txtNombre.TabIndex = 5
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(118, 29)
        Me.Fec_agrDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(224, 20)
        Me.Fec_agrDateEdit.TabIndex = 5
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(118, 3)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(224, 20)
        Me.User_agrTextEdit.TabIndex = 1
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(533, 29)
        Me.Fec_modDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(224, 20)
        Me.Fec_modDateEdit.TabIndex = 7
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(533, 3)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(224, 20)
        Me.User_modTextEdit.TabIndex = 2
        '
        'dkCamara
        '
        Me.dkCamara.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkCamara.Form = Me
        Me.dkCamara.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 689)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(817, 21)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("c8fa840f-926a-41fd-a758-0cbb958092ef")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 94)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(817, 94)
        Me.DockPanel1.Text = "Bitácora"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.Fec_agrDateEdit)
        Me.DockPanel1_Container.Controls.Add(Me.User_agrTextEdit)
        Me.DockPanel1_Container.Controls.Add(Fec_agrLabel)
        Me.DockPanel1_Container.Controls.Add(User_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_modTextEdit)
        Me.DockPanel1_Container.Controls.Add(User_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Fec_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_modDateEdit)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 28)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(810, 63)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'Cliente_direccionTableAdapter1
        '
        Me.Cliente_direccionTableAdapter1.ClearBeforeFill = True
        '
        'frmCuadrilla
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(817, 734)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Name = "frmCuadrilla"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Cuadrilla"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.TabPane1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPane1.ResumeLayout(False)
        Me.TabNavigationPage1.ResumeLayout(False)
        CType(Me.GroupControl13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl13.ResumeLayout(False)
        CType(Me.DGridOperadores, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrdOperadorBobega, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabNavigationPage2.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.DgridMontaCargas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdviewMontaCarga, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTipoCuadrilla.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdCuadrilla.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkCamara, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dkCamara As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents txtIdCuadrilla As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbTipoCuadrilla As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents RibbonStatusBar1 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents GroupControl13 As DevExpress.XtraEditors.GroupControl
    Private WithEvents DGridOperadores As DevExpress.XtraGrid.GridControl
    Private WithEvents GrdOperadorBobega As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents TabPane1 As DevExpress.XtraBars.Navigation.TabPane
    Friend WithEvents TabNavigationPage1 As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents TabNavigationPage2 As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents Cliente_direccionTableAdapter1 As cliente_direccion_dsetTableAdapters.cliente_direccionTableAdapter
    Friend WithEvents RibbonStatusBar2 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents BarStaticItem1 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Private WithEvents DgridMontaCargas As DevExpress.XtraGrid.GridControl
    Private WithEvents grdviewMontaCarga As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents lblCostoPorHora As DevExpress.XtraEditors.LabelControl
End Class
