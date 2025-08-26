<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImpresora
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim Label1 As System.Windows.Forms.Label
        Dim lblNombre As System.Windows.Forms.Label
        Dim lblModelo As System.Windows.Forms.Label
        Dim lblActivo As System.Windows.Forms.Label
        Dim IdEmpresaLabel As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim lblMarca As System.Windows.Forms.Label
        Dim lblLenguaje As System.Windows.Forms.Label
        Dim lblTipoConexion As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImpresora))
        Me.lblEsMovil = New System.Windows.Forms.Label()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.chkEsMovil = New DevExpress.XtraEditors.CheckEdit()
        Me.txtVelocidad = New DevExpress.XtraEditors.TextEdit()
        Me.lblVelocidad = New System.Windows.Forms.Label()
        Me.txtPuerto = New DevExpress.XtraEditors.TextEdit()
        Me.lblPuerto = New System.Windows.Forms.Label()
        Me.cmbTipoConexion = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbLenguaje = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbMarca = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtNoSerie = New DevExpress.XtraEditors.TextEdit()
        Me.cmdBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtAddress = New DevExpress.XtraEditors.TextEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbEmpresa = New DevExpress.XtraEditors.LookUpEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.txtDireccionIP = New DevExpress.XtraEditors.TextEdit()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.txtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.dkImpresora = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Label1 = New System.Windows.Forms.Label()
        lblNombre = New System.Windows.Forms.Label()
        lblModelo = New System.Windows.Forms.Label()
        lblActivo = New System.Windows.Forms.Label()
        IdEmpresaLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        lblMarca = New System.Windows.Forms.Label()
        lblLenguaje = New System.Windows.Forms.Label()
        lblTipoConexion = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.chkEsMovil.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVelocidad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPuerto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTipoConexion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbLenguaje.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbMarca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddress.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDireccionIP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkImpresora, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(22, 38)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(64, 13)
        Label1.TabIndex = 0
        Label1.Text = "Correlativo:"
        '
        'lblNombre
        '
        lblNombre.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblNombre.AutoSize = True
        lblNombre.Location = New System.Drawing.Point(22, 116)
        lblNombre.Name = "lblNombre"
        lblNombre.Size = New System.Drawing.Size(48, 13)
        lblNombre.TabIndex = 4
        lblNombre.Text = "Nombre:"
        '
        'lblModelo
        '
        lblModelo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblModelo.AutoSize = True
        lblModelo.Location = New System.Drawing.Point(22, 142)
        lblModelo.Name = "lblModelo"
        lblModelo.Size = New System.Drawing.Size(67, 13)
        lblModelo.TabIndex = 6
        lblModelo.Text = "Dirección IP:"
        '
        'lblActivo
        '
        lblActivo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblActivo.AutoSize = True
        lblActivo.Location = New System.Drawing.Point(22, 239)
        lblActivo.Name = "lblActivo"
        lblActivo.Size = New System.Drawing.Size(41, 13)
        lblActivo.TabIndex = 8
        lblActivo.Text = "Activo:"
        '
        'IdEmpresaLabel
        '
        IdEmpresaLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        IdEmpresaLabel.AutoSize = True
        IdEmpresaLabel.Location = New System.Drawing.Point(22, 64)
        IdEmpresaLabel.Name = "IdEmpresaLabel"
        IdEmpresaLabel.Size = New System.Drawing.Size(52, 13)
        IdEmpresaLabel.TabIndex = 2
        IdEmpresaLabel.Text = "Empresa:"
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
        Fec_modLabel.Location = New System.Drawing.Point(445, 32)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(82, 13)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(445, 6)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(89, 13)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Label3
        '
        Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(22, 90)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(47, 13)
        Label3.TabIndex = 12
        Label3.Text = "Bodega:"
        '
        'Label4
        '
        Label4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(375, 64)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(35, 13)
        Label4.TabIndex = 14
        Label4.Text = "Serie:"
        '
        'lblMarca
        '
        lblMarca.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblMarca.AutoSize = True
        lblMarca.Location = New System.Drawing.Point(375, 86)
        lblMarca.Name = "lblMarca"
        lblMarca.Size = New System.Drawing.Size(40, 13)
        lblMarca.TabIndex = 16
        lblMarca.Text = "Marca:"
        '
        'lblLenguaje
        '
        lblLenguaje.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblLenguaje.AutoSize = True
        lblLenguaje.Location = New System.Drawing.Point(375, 108)
        lblLenguaje.Name = "lblLenguaje"
        lblLenguaje.Size = New System.Drawing.Size(55, 13)
        lblLenguaje.TabIndex = 18
        lblLenguaje.Text = "Lenguaje:"
        '
        'lblTipoConexion
        '
        lblTipoConexion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblTipoConexion.AutoSize = True
        lblTipoConexion.Location = New System.Drawing.Point(375, 136)
        lblTipoConexion.Name = "lblTipoConexion"
        lblTipoConexion.Size = New System.Drawing.Size(79, 13)
        lblTipoConexion.TabIndex = 20
        lblTipoConexion.Text = "Tipo Conexion:"
        '
        'lblEsMovil
        '
        Me.lblEsMovil.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEsMovil.AutoSize = True
        Me.lblEsMovil.Location = New System.Drawing.Point(22, 215)
        Me.lblEsMovil.Name = "lblEsMovil"
        Me.lblEsMovil.Size = New System.Drawing.Size(49, 13)
        Me.lblEsMovil.TabIndex = 26
        Me.lblEsMovil.Text = "Es móvil:"
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
        Me.RibbonControl.Size = New System.Drawing.Size(819, 158)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 463)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(819, 24)
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.lblEsMovil)
        Me.GroupControl1.Controls.Add(Me.chkEsMovil)
        Me.GroupControl1.Controls.Add(Me.txtVelocidad)
        Me.GroupControl1.Controls.Add(Me.lblVelocidad)
        Me.GroupControl1.Controls.Add(Me.txtPuerto)
        Me.GroupControl1.Controls.Add(Me.lblPuerto)
        Me.GroupControl1.Controls.Add(Me.cmbTipoConexion)
        Me.GroupControl1.Controls.Add(lblTipoConexion)
        Me.GroupControl1.Controls.Add(Me.cmbLenguaje)
        Me.GroupControl1.Controls.Add(lblLenguaje)
        Me.GroupControl1.Controls.Add(Me.cmbMarca)
        Me.GroupControl1.Controls.Add(lblMarca)
        Me.GroupControl1.Controls.Add(Label4)
        Me.GroupControl1.Controls.Add(Me.txtNoSerie)
        Me.GroupControl1.Controls.Add(Me.cmdBodega)
        Me.GroupControl1.Controls.Add(Label3)
        Me.GroupControl1.Controls.Add(Me.txtAddress)
        Me.GroupControl1.Controls.Add(Me.Label2)
        Me.GroupControl1.Controls.Add(Me.cmbEmpresa)
        Me.GroupControl1.Controls.Add(IdEmpresaLabel)
        Me.GroupControl1.Controls.Add(lblActivo)
        Me.GroupControl1.Controls.Add(Me.chkActivo)
        Me.GroupControl1.Controls.Add(lblModelo)
        Me.GroupControl1.Controls.Add(Me.txtDireccionIP)
        Me.GroupControl1.Controls.Add(lblNombre)
        Me.GroupControl1.Controls.Add(Me.lblCodigo)
        Me.GroupControl1.Controls.Add(Me.txtNombre)
        Me.GroupControl1.Controls.Add(Label1)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 158)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(819, 296)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Datos Impresora"
        '
        'chkEsMovil
        '
        Me.chkEsMovil.Location = New System.Drawing.Point(139, 212)
        Me.chkEsMovil.MenuManager = Me.RibbonControl
        Me.chkEsMovil.Name = "chkEsMovil"
        Me.chkEsMovil.Properties.Caption = ""
        Me.chkEsMovil.Size = New System.Drawing.Size(26, 20)
        Me.chkEsMovil.TabIndex = 27
        '
        'txtVelocidad
        '
        Me.txtVelocidad.Location = New System.Drawing.Point(491, 158)
        Me.txtVelocidad.MenuManager = Me.RibbonControl
        Me.txtVelocidad.Name = "txtVelocidad"
        Me.txtVelocidad.Size = New System.Drawing.Size(207, 20)
        Me.txtVelocidad.TabIndex = 25
        '
        'lblVelocidad
        '
        Me.lblVelocidad.AutoSize = True
        Me.lblVelocidad.Location = New System.Drawing.Point(375, 161)
        Me.lblVelocidad.Name = "lblVelocidad"
        Me.lblVelocidad.Size = New System.Drawing.Size(56, 13)
        Me.lblVelocidad.TabIndex = 24
        Me.lblVelocidad.Text = "Velocidad:"
        '
        'txtPuerto
        '
        Me.txtPuerto.Location = New System.Drawing.Point(139, 189)
        Me.txtPuerto.MenuManager = Me.RibbonControl
        Me.txtPuerto.Name = "txtPuerto"
        Me.txtPuerto.Size = New System.Drawing.Size(207, 20)
        Me.txtPuerto.TabIndex = 23
        '
        'lblPuerto
        '
        Me.lblPuerto.AutoSize = True
        Me.lblPuerto.Location = New System.Drawing.Point(22, 191)
        Me.lblPuerto.Name = "lblPuerto"
        Me.lblPuerto.Size = New System.Drawing.Size(43, 13)
        Me.lblPuerto.TabIndex = 22
        Me.lblPuerto.Text = "Puerto:"
        '
        'cmbTipoConexion
        '
        Me.cmbTipoConexion.Location = New System.Drawing.Point(491, 132)
        Me.cmbTipoConexion.MenuManager = Me.RibbonControl
        Me.cmbTipoConexion.Name = "cmbTipoConexion"
        Me.cmbTipoConexion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoConexion.Properties.NullText = ""
        Me.cmbTipoConexion.Size = New System.Drawing.Size(207, 20)
        Me.cmbTipoConexion.TabIndex = 21
        '
        'cmbLenguaje
        '
        Me.cmbLenguaje.Location = New System.Drawing.Point(491, 108)
        Me.cmbLenguaje.MenuManager = Me.RibbonControl
        Me.cmbLenguaje.Name = "cmbLenguaje"
        Me.cmbLenguaje.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbLenguaje.Properties.NullText = ""
        Me.cmbLenguaje.Size = New System.Drawing.Size(207, 20)
        Me.cmbLenguaje.TabIndex = 19
        '
        'cmbMarca
        '
        Me.cmbMarca.Location = New System.Drawing.Point(491, 84)
        Me.cmbMarca.MenuManager = Me.RibbonControl
        Me.cmbMarca.Name = "cmbMarca"
        Me.cmbMarca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMarca.Properties.NullText = ""
        Me.cmbMarca.Size = New System.Drawing.Size(207, 20)
        Me.cmbMarca.TabIndex = 17
        '
        'txtNoSerie
        '
        Me.txtNoSerie.Location = New System.Drawing.Point(491, 59)
        Me.txtNoSerie.MenuManager = Me.RibbonControl
        Me.txtNoSerie.Name = "txtNoSerie"
        Me.txtNoSerie.Size = New System.Drawing.Size(207, 20)
        Me.txtNoSerie.TabIndex = 15
        '
        'cmdBodega
        '
        Me.cmdBodega.Location = New System.Drawing.Point(139, 85)
        Me.cmdBodega.MenuManager = Me.RibbonControl
        Me.cmdBodega.Name = "cmdBodega"
        Me.cmdBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmdBodega.Properties.NullText = ""
        Me.cmdBodega.Size = New System.Drawing.Size(207, 20)
        Me.cmdBodega.TabIndex = 13
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(139, 163)
        Me.txtAddress.MenuManager = Me.RibbonControl
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(207, 20)
        Me.txtAddress.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 166)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Mac Address:"
        '
        'cmbEmpresa
        '
        Me.cmbEmpresa.Location = New System.Drawing.Point(139, 59)
        Me.cmbEmpresa.MenuManager = Me.RibbonControl
        Me.cmbEmpresa.Name = "cmbEmpresa"
        Me.cmbEmpresa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEmpresa.Properties.NullText = ""
        Me.cmbEmpresa.Size = New System.Drawing.Size(207, 20)
        Me.cmbEmpresa.TabIndex = 3
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(139, 236)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(26, 20)
        Me.chkActivo.TabIndex = 9
        '
        'txtDireccionIP
        '
        Me.txtDireccionIP.Location = New System.Drawing.Point(139, 137)
        Me.txtDireccionIP.MenuManager = Me.RibbonControl
        Me.txtDireccionIP.Name = "txtDireccionIP"
        Me.txtDireccionIP.Size = New System.Drawing.Size(207, 20)
        Me.txtDireccionIP.TabIndex = 7
        '
        'lblCodigo
        '
        Me.lblCodigo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodigo.Location = New System.Drawing.Point(138, 38)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(0, 13)
        Me.lblCodigo.TabIndex = 1
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(139, 111)
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
        'dkImpresora
        '
        Me.dkImpresora.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkImpresora.Form = Me
        Me.dkImpresora.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 454)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(819, 9)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("89f12dd7-1eb4-4d36-a12f-5b60e803f2e7")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 315)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 89)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(819, 89)
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
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(813, 61)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'frmImpresora
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(819, 487)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Name = "frmImpresora"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Impresora"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.chkEsMovil.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVelocidad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPuerto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTipoConexion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbLenguaje.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbMarca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddress.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDireccionIP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkImpresora, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents lblCodigo As System.Windows.Forms.Label
    Friend WithEvents txtDireccionIP As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dkImpresora As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents cmbEmpresa As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtAddress As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents cmdBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtNoSerie As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbMarca As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbTipoConexion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbLenguaje As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtVelocidad As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblVelocidad As Label
    Friend WithEvents txtPuerto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblPuerto As Label
    Friend WithEvents chkEsMovil As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblEsMovil As Label
End Class
