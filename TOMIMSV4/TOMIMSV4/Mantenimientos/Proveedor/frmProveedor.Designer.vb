<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProveedor
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
        Dim IdEmpresaLabel As System.Windows.Forms.Label
        Dim NombreLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim lblReferencia As System.Windows.Forms.Label
        Dim Label17 As System.Windows.Forms.Label
        Dim Label16 As System.Windows.Forms.Label
        Dim Label15 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim lblBodegaArea As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProveedor))
        Dim Label8 As System.Windows.Forms.Label
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.chkEsProveedorServicio = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkActivo = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkSistema = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkBodegaRec = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkBodegaTras = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkMuestraPrecio = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkActualizaCostoOC = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup4 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup5 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.cmbBodegaAreaSAP = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cmbBodegaWMS = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtReferenciaCliente = New DevExpress.XtraEditors.TextEdit()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbEmpresa = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtCodProveedor = New System.Windows.Forms.TextBox()
        Me.lblCodProv = New System.Windows.Forms.Label()
        Me.txtContacto = New DevExpress.XtraEditors.TextEdit()
        Me.txtCorreo = New DevExpress.XtraEditors.TextEdit()
        Me.txtDireccion = New DevExpress.XtraEditors.TextEdit()
        Me.txtNit = New DevExpress.XtraEditors.TextEdit()
        Me.txtTelefono = New DevExpress.XtraEditors.TextEdit()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.NombreTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.GrpEmpresaTB = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.Grid = New DevExpress.XtraGrid.GridControl()
        Me.DataBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsProveedor = New TOMWMS.DsProveedor()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colAsignar = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colIdBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdAsignacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdInterno = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdAreaOrigen = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AreaBodegaGridLookUpEdit = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.dkProveedor = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.xtrTabProveedor = New DevExpress.XtraTab.XtraTabControl()
        Me.datosProveedor = New DevExpress.XtraTab.XtraTabPage()
        Me.tiempoAceptacion = New DevExpress.XtraTab.XtraTabPage()
        Me.GrpTiempoAceptacion = New DevExpress.XtraEditors.GroupControl()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmdGuardar = New System.Windows.Forms.Button()
        Me.txtDiaExterior = New System.Windows.Forms.NumericUpDown()
        Me.txtDiaLocal = New System.Windows.Forms.NumericUpDown()
        Me.lnkFamilia = New System.Windows.Forms.LinkLabel()
        Me.txtIdFamilia = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreFamilia = New DevExpress.XtraEditors.TextEdit()
        Me.lnkClasificacion = New System.Windows.Forms.LinkLabel()
        Me.txtIdClasificacion = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreClasificacion = New DevExpress.XtraEditors.TextEdit()
        Me.GrpTiempo = New DevExpress.XtraEditors.GroupControl()
        Me.GridTiempo = New DevExpress.XtraGrid.GridControl()
        Me.GridViewTiempo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ProveedorBodega = New DevExpress.XtraTab.XtraTabPage()
        Me.cmbPais = New DevExpress.XtraEditors.LookUpEdit()
        IdEmpresaLabel = New System.Windows.Forms.Label()
        NombreLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        lblReferencia = New System.Windows.Forms.Label()
        Label17 = New System.Windows.Forms.Label()
        Label16 = New System.Windows.Forms.Label()
        Label15 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        lblBodegaArea = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.cmbBodegaAreaSAP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodegaWMS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReferenciaCliente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtContacto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCorreo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDireccion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTelefono.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NombreTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpEmpresaTB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpEmpresaTB.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsProveedor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AreaBodegaGridLookUpEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkProveedor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtrTabProveedor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtrTabProveedor.SuspendLayout()
        Me.datosProveedor.SuspendLayout()
        Me.tiempoAceptacion.SuspendLayout()
        CType(Me.GrpTiempoAceptacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTiempoAceptacion.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtDiaExterior, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiaLocal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdFamilia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreFamilia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdClasificacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreClasificacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpTiempo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTiempo.SuspendLayout()
        CType(Me.GridTiempo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewTiempo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ProveedorBodega.SuspendLayout()
        CType(Me.cmbPais.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'IdEmpresaLabel
        '
        IdEmpresaLabel.AutoSize = True
        IdEmpresaLabel.Location = New System.Drawing.Point(26, 75)
        IdEmpresaLabel.Name = "IdEmpresaLabel"
        IdEmpresaLabel.Size = New System.Drawing.Size(62, 16)
        IdEmpresaLabel.TabIndex = 2
        IdEmpresaLabel.Text = "Empresa:"
        '
        'NombreLabel
        '
        NombreLabel.AutoSize = True
        NombreLabel.Location = New System.Drawing.Point(24, 177)
        NombreLabel.Name = "NombreLabel"
        NombreLabel.Size = New System.Drawing.Size(57, 16)
        NombreLabel.TabIndex = 8
        NombreLabel.Text = "Nombre:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(24, 209)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(62, 16)
        Label1.TabIndex = 10
        Label1.Text = "Teléfono:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(23, 241)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(27, 16)
        Label2.TabIndex = 12
        Label2.Text = "Nit:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(24, 273)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(64, 16)
        Label3.TabIndex = 14
        Label3.Text = "Dirección:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(24, 305)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(117, 16)
        Label4.TabIndex = 16
        Label4.Text = "Correo Electrónico:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(24, 337)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(62, 16)
        Label5.TabIndex = 18
        Label5.Text = "Contacto:"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(26, 108)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(74, 16)
        Label6.TabIndex = 4
        Label6.Text = "Propietario:"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(26, 44)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(46, 16)
        Label12.TabIndex = 0
        Label12.Text = "Código"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(44, 47)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(44, 15)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(100, 16)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(528, 47)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(528, 15)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'lblReferencia
        '
        lblReferencia.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblReferencia.AutoSize = True
        lblReferencia.Location = New System.Drawing.Point(591, 145)
        lblReferencia.Name = "lblReferencia"
        lblReferencia.Size = New System.Drawing.Size(99, 16)
        lblReferencia.TabIndex = 54
        lblReferencia.Text = "Referencia ERP:"
        '
        'Label17
        '
        Label17.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label17.AutoSize = True
        Label17.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label17.ForeColor = System.Drawing.Color.Red
        Label17.Location = New System.Drawing.Point(245, 138)
        Label17.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label17.Name = "Label17"
        Label17.Size = New System.Drawing.Size(21, 21)
        Label17.TabIndex = 12
        Label17.Text = "*"
        '
        'Label16
        '
        Label16.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label16.AutoSize = True
        Label16.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label16.ForeColor = System.Drawing.Color.Red
        Label16.Location = New System.Drawing.Point(245, 103)
        Label16.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label16.Name = "Label16"
        Label16.Size = New System.Drawing.Size(21, 21)
        Label16.TabIndex = 9
        Label16.Text = "*"
        '
        'Label15
        '
        Label15.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label15.AutoSize = True
        Label15.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label15.ForeColor = System.Drawing.Color.Red
        Label15.Location = New System.Drawing.Point(245, 72)
        Label15.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label15.Name = "Label15"
        Label15.Size = New System.Drawing.Size(21, 21)
        Label15.TabIndex = 5
        Label15.Text = "*"
        '
        'Label7
        '
        Label7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label7.AutoSize = True
        Label7.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label7.ForeColor = System.Drawing.Color.Red
        Label7.Location = New System.Drawing.Point(245, 32)
        Label7.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(21, 21)
        Label7.TabIndex = 1
        Label7.Text = "*"
        '
        'Label10
        '
        Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(90, 103)
        Label10.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(82, 16)
        Label10.TabIndex = 8
        Label10.Text = "Días Locales:"
        '
        'Label11
        '
        Label11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label11.AutoSize = True
        Label11.Location = New System.Drawing.Point(90, 138)
        Label11.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(97, 16)
        Label11.TabIndex = 11
        Label11.Text = "Días Exteriores:"
        '
        'lblBodegaArea
        '
        lblBodegaArea.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblBodegaArea.AutoSize = True
        lblBodegaArea.Location = New System.Drawing.Point(591, 179)
        lblBodegaArea.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        lblBodegaArea.Name = "lblBodegaArea"
        lblBodegaArea.Size = New System.Drawing.Size(122, 16)
        lblBodegaArea.TabIndex = 85
        lblBodegaArea.Text = "Área (Bodega SAP):"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.chkEsProveedorServicio, Me.chkActivo, Me.chkSistema, Me.chkBodegaRec, Me.chkBodegaTras, Me.chkMuestraPrecio, Me.chkActualizaCostoOC})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 12
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1615, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
        Me.mnuGuardar.Name = "mnuGuardar"
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
        'chkEsProveedorServicio
        '
        Me.chkEsProveedorServicio.Caption = "Es Proveedor de Servicios"
        Me.chkEsProveedorServicio.Id = 5
        Me.chkEsProveedorServicio.Name = "chkEsProveedorServicio"
        '
        'chkActivo
        '
        Me.chkActivo.BindableChecked = True
        Me.chkActivo.Caption = "Activo"
        Me.chkActivo.Checked = True
        Me.chkActivo.Id = 6
        Me.chkActivo.Name = "chkActivo"
        '
        'chkSistema
        '
        Me.chkSistema.Caption = "Sistema"
        Me.chkSistema.Hint = "Es un proveedor de sistema"
        Me.chkSistema.Id = 7
        Me.chkSistema.Name = "chkSistema"
        '
        'chkBodegaRec
        '
        Me.chkBodegaRec.Caption = "Bodega Recepción"
        Me.chkBodegaRec.Id = 8
        Me.chkBodegaRec.Name = "chkBodegaRec"
        '
        'chkBodegaTras
        '
        Me.chkBodegaTras.Caption = "Bodega Traslado"
        Me.chkBodegaTras.Id = 9
        Me.chkBodegaTras.Name = "chkBodegaTras"
        '
        'chkMuestraPrecio
        '
        Me.chkMuestraPrecio.Caption = "Mostrar valores en ingresos"
        Me.chkMuestraPrecio.Id = 10
        Me.chkMuestraPrecio.Name = "chkMuestraPrecio"
        '
        'chkActualizaCostoOC
        '
        Me.chkActualizaCostoOC.Caption = "Actualizar costos DI"
        Me.chkActualizaCostoOC.Hint = "Actualizar costos con documento de ingreso"
        Me.chkActualizaCostoOC.Id = 11
        Me.chkActualizaCostoOC.Name = "chkActualizaCostoOC"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup3, Me.RibbonPageGroup4, Me.RibbonPageGroup5, Me.RibbonPageGroup2})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Proveedor"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.chkActivo)
        Me.RibbonPageGroup3.ItemLinks.Add(Me.chkSistema)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'RibbonPageGroup4
        '
        Me.RibbonPageGroup4.ItemLinks.Add(Me.chkBodegaRec)
        Me.RibbonPageGroup4.ItemLinks.Add(Me.chkBodegaTras)
        Me.RibbonPageGroup4.Name = "RibbonPageGroup4"
        '
        'RibbonPageGroup5
        '
        Me.RibbonPageGroup5.ItemLinks.Add(Me.chkMuestraPrecio)
        Me.RibbonPageGroup5.ItemLinks.Add(Me.chkActualizaCostoOC)
        Me.RibbonPageGroup5.Name = "RibbonPageGroup5"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkEsProveedorServicio)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 843)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1615, 30)
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.cmbPais)
        Me.GroupControl1.Controls.Add(Label8)
        Me.GroupControl1.Controls.Add(lblBodegaArea)
        Me.GroupControl1.Controls.Add(Me.cmbBodegaAreaSAP)
        Me.GroupControl1.Controls.Add(Me.cmbBodegaWMS)
        Me.GroupControl1.Controls.Add(Me.Label14)
        Me.GroupControl1.Controls.Add(lblReferencia)
        Me.GroupControl1.Controls.Add(Me.txtReferenciaCliente)
        Me.GroupControl1.Controls.Add(Me.cmbPropietario)
        Me.GroupControl1.Controls.Add(Me.cmbEmpresa)
        Me.GroupControl1.Controls.Add(Me.txtCodProveedor)
        Me.GroupControl1.Controls.Add(Me.lblCodProv)
        Me.GroupControl1.Controls.Add(Label6)
        Me.GroupControl1.Controls.Add(Label5)
        Me.GroupControl1.Controls.Add(Label4)
        Me.GroupControl1.Controls.Add(Label3)
        Me.GroupControl1.Controls.Add(Label2)
        Me.GroupControl1.Controls.Add(Label1)
        Me.GroupControl1.Controls.Add(Me.txtContacto)
        Me.GroupControl1.Controls.Add(Me.txtCorreo)
        Me.GroupControl1.Controls.Add(Me.txtDireccion)
        Me.GroupControl1.Controls.Add(Me.txtNit)
        Me.GroupControl1.Controls.Add(Me.txtTelefono)
        Me.GroupControl1.Controls.Add(Me.lblCodigo)
        Me.GroupControl1.Controls.Add(Label12)
        Me.GroupControl1.Controls.Add(IdEmpresaLabel)
        Me.GroupControl1.Controls.Add(NombreLabel)
        Me.GroupControl1.Controls.Add(Me.NombreTextEdit)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1613, 594)
        Me.GroupControl1.TabIndex = 0
        '
        'cmbBodegaAreaSAP
        '
        Me.cmbBodegaAreaSAP.Location = New System.Drawing.Point(721, 174)
        Me.cmbBodegaAreaSAP.MenuManager = Me.RibbonControl
        Me.cmbBodegaAreaSAP.Name = "cmbBodegaAreaSAP"
        Me.cmbBodegaAreaSAP.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodegaAreaSAP.Properties.PopupView = Me.GridView2
        Me.cmbBodegaAreaSAP.Size = New System.Drawing.Size(279, 22)
        Me.cmbBodegaAreaSAP.TabIndex = 84
        Me.cmbBodegaAreaSAP.ToolTip = "Config = Activo =1 Bloqueada = 0 Sistema = 0 Despacho = 1 "
        '
        'GridView2
        '
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'cmbBodegaWMS
        '
        Me.cmbBodegaWMS.Location = New System.Drawing.Point(721, 100)
        Me.cmbBodegaWMS.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbBodegaWMS.MenuManager = Me.RibbonControl
        Me.cmbBodegaWMS.Name = "cmbBodegaWMS"
        Me.cmbBodegaWMS.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodegaWMS.Properties.NullText = ""
        Me.cmbBodegaWMS.Properties.ReadOnly = True
        Me.cmbBodegaWMS.Size = New System.Drawing.Size(279, 22)
        Me.cmbBodegaWMS.TabIndex = 57
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(591, 103)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(88, 16)
        Me.Label14.TabIndex = 56
        Me.Label14.Text = "Bodega WMS:"
        '
        'txtReferenciaCliente
        '
        Me.txtReferenciaCliente.Location = New System.Drawing.Point(721, 139)
        Me.txtReferenciaCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtReferenciaCliente.MenuManager = Me.RibbonControl
        Me.txtReferenciaCliente.Name = "txtReferenciaCliente"
        Me.txtReferenciaCliente.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtReferenciaCliente.Properties.MaxLength = 25
        Me.txtReferenciaCliente.Size = New System.Drawing.Size(279, 22)
        Me.txtReferenciaCliente.TabIndex = 55
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Location = New System.Drawing.Point(161, 105)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPropietario.MenuManager = Me.RibbonControl
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Size = New System.Drawing.Size(240, 22)
        Me.cmbPropietario.TabIndex = 27
        '
        'cmbEmpresa
        '
        Me.cmbEmpresa.Location = New System.Drawing.Point(161, 71)
        Me.cmbEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbEmpresa.MenuManager = Me.RibbonControl
        Me.cmbEmpresa.Name = "cmbEmpresa"
        Me.cmbEmpresa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEmpresa.Properties.NullText = ""
        Me.cmbEmpresa.Size = New System.Drawing.Size(240, 22)
        Me.cmbEmpresa.TabIndex = 26
        '
        'txtCodProveedor
        '
        Me.txtCodProveedor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCodProveedor.Location = New System.Drawing.Point(160, 142)
        Me.txtCodProveedor.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCodProveedor.Name = "txtCodProveedor"
        Me.txtCodProveedor.Size = New System.Drawing.Size(243, 23)
        Me.txtCodProveedor.TabIndex = 7
        '
        'lblCodProv
        '
        Me.lblCodProv.AutoSize = True
        Me.lblCodProv.Location = New System.Drawing.Point(23, 145)
        Me.lblCodProv.Name = "lblCodProv"
        Me.lblCodProv.Size = New System.Drawing.Size(113, 16)
        Me.lblCodProv.TabIndex = 6
        Me.lblCodProv.Text = "Código Proveedor:"
        '
        'txtContacto
        '
        Me.txtContacto.Location = New System.Drawing.Point(160, 334)
        Me.txtContacto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtContacto.MenuManager = Me.RibbonControl
        Me.txtContacto.Name = "txtContacto"
        Me.txtContacto.Size = New System.Drawing.Size(241, 22)
        Me.txtContacto.TabIndex = 19
        '
        'txtCorreo
        '
        Me.txtCorreo.Location = New System.Drawing.Point(160, 302)
        Me.txtCorreo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCorreo.MenuManager = Me.RibbonControl
        Me.txtCorreo.Name = "txtCorreo"
        Me.txtCorreo.Properties.Mask.EditMask = "[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"
        Me.txtCorreo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.txtCorreo.Size = New System.Drawing.Size(241, 22)
        Me.txtCorreo.TabIndex = 17
        '
        'txtDireccion
        '
        Me.txtDireccion.Location = New System.Drawing.Point(160, 270)
        Me.txtDireccion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtDireccion.MenuManager = Me.RibbonControl
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.Size = New System.Drawing.Size(241, 22)
        Me.txtDireccion.TabIndex = 15
        '
        'txtNit
        '
        Me.txtNit.Location = New System.Drawing.Point(160, 238)
        Me.txtNit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNit.MenuManager = Me.RibbonControl
        Me.txtNit.Name = "txtNit"
        Me.txtNit.Size = New System.Drawing.Size(241, 22)
        Me.txtNit.TabIndex = 13
        '
        'txtTelefono
        '
        Me.txtTelefono.Location = New System.Drawing.Point(160, 206)
        Me.txtTelefono.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtTelefono.MenuManager = Me.RibbonControl
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.Size = New System.Drawing.Size(241, 22)
        Me.txtTelefono.TabIndex = 11
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodigo.Location = New System.Drawing.Point(161, 44)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(0, 17)
        Me.lblCodigo.TabIndex = 1
        '
        'NombreTextEdit
        '
        Me.NombreTextEdit.Location = New System.Drawing.Point(160, 174)
        Me.NombreTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.NombreTextEdit.MenuManager = Me.RibbonControl
        Me.NombreTextEdit.Name = "NombreTextEdit"
        Me.NombreTextEdit.Size = New System.Drawing.Size(241, 22)
        Me.NombreTextEdit.TabIndex = 9
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(147, 43)
        Me.Fec_agrDateEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_agrDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_agrDateEdit.TabIndex = 5
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(147, 11)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_agrTextEdit.TabIndex = 1
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(631, 43)
        Me.Fec_modDateEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_modDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_modDateEdit.TabIndex = 7
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(631, 11)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 2
        '
        'GrpEmpresaTB
        '
        Me.GrpEmpresaTB.Controls.Add(Me.GroupControl3)
        Me.GrpEmpresaTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpEmpresaTB.Location = New System.Drawing.Point(0, 0)
        Me.GrpEmpresaTB.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GrpEmpresaTB.Name = "GrpEmpresaTB"
        Me.GrpEmpresaTB.Size = New System.Drawing.Size(1613, 594)
        Me.GrpEmpresaTB.TabIndex = 0
        Me.GrpEmpresaTB.Tag = ""
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.Grid)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(2, 28)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(1609, 564)
        Me.GroupControl3.TabIndex = 0
        Me.GroupControl3.Text = "Selección de Bodega"
        '
        'Grid
        '
        Me.Grid.DataSource = Me.DataBindingSource
        Me.Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Grid.Location = New System.Drawing.Point(2, 28)
        Me.Grid.MainView = Me.GridView1
        Me.Grid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Grid.MenuManager = Me.RibbonControl
        Me.Grid.Name = "Grid"
        Me.Grid.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.AreaBodegaGridLookUpEdit})
        Me.Grid.Size = New System.Drawing.Size(1605, 534)
        Me.Grid.TabIndex = 0
        Me.Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'DataBindingSource
        '
        Me.DataBindingSource.DataMember = "Data"
        Me.DataBindingSource.DataSource = Me.DsProveedor
        '
        'DsProveedor
        '
        Me.DsProveedor.DataSetName = "DsProveedor"
        Me.DsProveedor.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colAsignar, Me.colIdBodega, Me.colIdAsignacion, Me.colBodega, Me.colIdInterno, Me.colIdAreaOrigen})
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.Grid
        Me.GridView1.Name = "GridView1"
        '
        'colAsignar
        '
        Me.colAsignar.Caption = "Asignar"
        Me.colAsignar.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.colAsignar.FieldName = "Asignar"
        Me.colAsignar.MinWidth = 23
        Me.colAsignar.Name = "colAsignar"
        Me.colAsignar.Visible = True
        Me.colAsignar.VisibleIndex = 0
        Me.colAsignar.Width = 87
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'colIdBodega
        '
        Me.colIdBodega.FieldName = "IdBodega"
        Me.colIdBodega.MinWidth = 23
        Me.colIdBodega.Name = "colIdBodega"
        Me.colIdBodega.OptionsColumn.ReadOnly = True
        Me.colIdBodega.Width = 87
        '
        'colIdAsignacion
        '
        Me.colIdAsignacion.FieldName = "IdAsignacion"
        Me.colIdAsignacion.MinWidth = 23
        Me.colIdAsignacion.Name = "colIdAsignacion"
        Me.colIdAsignacion.OptionsColumn.ReadOnly = True
        Me.colIdAsignacion.Width = 87
        '
        'colBodega
        '
        Me.colBodega.FieldName = "Bodega"
        Me.colBodega.MinWidth = 23
        Me.colBodega.Name = "colBodega"
        Me.colBodega.OptionsColumn.AllowEdit = False
        Me.colBodega.OptionsColumn.ReadOnly = True
        Me.colBodega.Visible = True
        Me.colBodega.VisibleIndex = 1
        Me.colBodega.Width = 87
        '
        'colIdInterno
        '
        Me.colIdInterno.FieldName = "IdInterno"
        Me.colIdInterno.MinWidth = 23
        Me.colIdInterno.Name = "colIdInterno"
        Me.colIdInterno.OptionsColumn.ReadOnly = True
        Me.colIdInterno.Width = 87
        '
        'colIdAreaOrigen
        '
        Me.colIdAreaOrigen.Caption = "IdArea Origen"
        Me.colIdAreaOrigen.ColumnEdit = Me.AreaBodegaGridLookUpEdit
        Me.colIdAreaOrigen.FieldName = "IdAreaOrigen"
        Me.colIdAreaOrigen.MinWidth = 25
        Me.colIdAreaOrigen.Name = "colIdAreaOrigen"
        Me.colIdAreaOrigen.Visible = True
        Me.colIdAreaOrigen.VisibleIndex = 2
        Me.colIdAreaOrigen.Width = 94
        '
        'AreaBodegaGridLookUpEdit
        '
        Me.AreaBodegaGridLookUpEdit.AutoHeight = False
        Me.AreaBodegaGridLookUpEdit.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.AreaBodegaGridLookUpEdit.Name = "AreaBodegaGridLookUpEdit"
        Me.AreaBodegaGridLookUpEdit.PopupView = Me.RepositoryItemGridLookUpEdit1View
        '
        'RepositoryItemGridLookUpEdit1View
        '
        Me.RepositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemGridLookUpEdit1View.Name = "RepositoryItemGridLookUpEdit1View"
        Me.RepositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'dkProveedor
        '
        Me.dkProveedor.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkProveedor.Form = Me
        Me.dkProveedor.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 817)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1615, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("8ac863ee-1e24-43a9-98cc-bcf72b6f306e")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 101)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(953, 124)
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
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 31)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(946, 90)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'xtrTabProveedor
        '
        Me.xtrTabProveedor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtrTabProveedor.Location = New System.Drawing.Point(0, 193)
        Me.xtrTabProveedor.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.xtrTabProveedor.Name = "xtrTabProveedor"
        Me.xtrTabProveedor.SelectedTabPage = Me.datosProveedor
        Me.xtrTabProveedor.Size = New System.Drawing.Size(1615, 624)
        Me.xtrTabProveedor.TabIndex = 4
        Me.xtrTabProveedor.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.datosProveedor, Me.tiempoAceptacion, Me.ProveedorBodega})
        '
        'datosProveedor
        '
        Me.datosProveedor.Controls.Add(Me.GroupControl1)
        Me.datosProveedor.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.datosProveedor.Name = "datosProveedor"
        Me.datosProveedor.Size = New System.Drawing.Size(1613, 594)
        Me.datosProveedor.Text = "Datos Proveedor"
        '
        'tiempoAceptacion
        '
        Me.tiempoAceptacion.Controls.Add(Me.GrpTiempoAceptacion)
        Me.tiempoAceptacion.Name = "tiempoAceptacion"
        Me.tiempoAceptacion.Size = New System.Drawing.Size(1613, 594)
        Me.tiempoAceptacion.Text = "Tiempos de Aceptación"
        '
        'GrpTiempoAceptacion
        '
        Me.GrpTiempoAceptacion.Controls.Add(Me.GroupBox2)
        Me.GrpTiempoAceptacion.Controls.Add(Me.GrpTiempo)
        Me.GrpTiempoAceptacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpTiempoAceptacion.Location = New System.Drawing.Point(0, 0)
        Me.GrpTiempoAceptacion.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GrpTiempoAceptacion.Name = "GrpTiempoAceptacion"
        Me.GrpTiempoAceptacion.Size = New System.Drawing.Size(1613, 594)
        Me.GrpTiempoAceptacion.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Label17)
        Me.GroupBox2.Controls.Add(Label16)
        Me.GroupBox2.Controls.Add(Label15)
        Me.GroupBox2.Controls.Add(Label7)
        Me.GroupBox2.Controls.Add(Me.cmdGuardar)
        Me.GroupBox2.Controls.Add(Me.txtDiaExterior)
        Me.GroupBox2.Controls.Add(Me.txtDiaLocal)
        Me.GroupBox2.Controls.Add(Me.lnkFamilia)
        Me.GroupBox2.Controls.Add(Me.txtIdFamilia)
        Me.GroupBox2.Controls.Add(Me.txtNombreFamilia)
        Me.GroupBox2.Controls.Add(Me.lnkClasificacion)
        Me.GroupBox2.Controls.Add(Me.txtIdClasificacion)
        Me.GroupBox2.Controls.Add(Me.txtNombreClasificacion)
        Me.GroupBox2.Controls.Add(Label10)
        Me.GroupBox2.Controls.Add(Label11)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(2, 28)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GroupBox2.Size = New System.Drawing.Size(1609, 245)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdGuardar.Location = New System.Drawing.Point(1388, 113)
        Me.cmdGuardar.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.cmdGuardar.Name = "cmdGuardar"
        Me.cmdGuardar.Size = New System.Drawing.Size(130, 44)
        Me.cmdGuardar.TabIndex = 14
        Me.cmdGuardar.Text = "Guardar"
        Me.cmdGuardar.UseVisualStyleBackColor = True
        '
        'txtDiaExterior
        '
        Me.txtDiaExterior.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDiaExterior.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiaExterior.Location = New System.Drawing.Point(286, 134)
        Me.txtDiaExterior.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtDiaExterior.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtDiaExterior.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtDiaExterior.Name = "txtDiaExterior"
        Me.txtDiaExterior.Size = New System.Drawing.Size(886, 23)
        Me.txtDiaExterior.TabIndex = 13
        '
        'txtDiaLocal
        '
        Me.txtDiaLocal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDiaLocal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiaLocal.Location = New System.Drawing.Point(286, 99)
        Me.txtDiaLocal.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtDiaLocal.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtDiaLocal.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtDiaLocal.Name = "txtDiaLocal"
        Me.txtDiaLocal.Size = New System.Drawing.Size(886, 23)
        Me.txtDiaLocal.TabIndex = 10
        '
        'lnkFamilia
        '
        Me.lnkFamilia.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkFamilia.AutoSize = True
        Me.lnkFamilia.Location = New System.Drawing.Point(90, 72)
        Me.lnkFamilia.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lnkFamilia.Name = "lnkFamilia"
        Me.lnkFamilia.Size = New System.Drawing.Size(53, 16)
        Me.lnkFamilia.TabIndex = 4
        Me.lnkFamilia.TabStop = True
        Me.lnkFamilia.Text = "Familia:"
        '
        'txtIdFamilia
        '
        Me.txtIdFamilia.Location = New System.Drawing.Point(286, 65)
        Me.txtIdFamilia.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtIdFamilia.MenuManager = Me.RibbonControl
        Me.txtIdFamilia.Name = "txtIdFamilia"
        Me.txtIdFamilia.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtIdFamilia.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtIdFamilia.Properties.MaskSettings.Set("mask", "n0")
        Me.txtIdFamilia.Size = New System.Drawing.Size(281, 22)
        Me.txtIdFamilia.TabIndex = 6
        '
        'txtNombreFamilia
        '
        Me.txtNombreFamilia.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNombreFamilia.Location = New System.Drawing.Point(578, 65)
        Me.txtNombreFamilia.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtNombreFamilia.MenuManager = Me.RibbonControl
        Me.txtNombreFamilia.Name = "txtNombreFamilia"
        Me.txtNombreFamilia.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtNombreFamilia.Properties.ReadOnly = True
        Me.txtNombreFamilia.Size = New System.Drawing.Size(940, 22)
        Me.txtNombreFamilia.TabIndex = 7
        '
        'lnkClasificacion
        '
        Me.lnkClasificacion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkClasificacion.AutoSize = True
        Me.lnkClasificacion.Location = New System.Drawing.Point(90, 36)
        Me.lnkClasificacion.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lnkClasificacion.Name = "lnkClasificacion"
        Me.lnkClasificacion.Size = New System.Drawing.Size(82, 16)
        Me.lnkClasificacion.TabIndex = 0
        Me.lnkClasificacion.TabStop = True
        Me.lnkClasificacion.Text = "Clasificación:"
        '
        'txtIdClasificacion
        '
        Me.txtIdClasificacion.Location = New System.Drawing.Point(286, 31)
        Me.txtIdClasificacion.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtIdClasificacion.MenuManager = Me.RibbonControl
        Me.txtIdClasificacion.Name = "txtIdClasificacion"
        Me.txtIdClasificacion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtIdClasificacion.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtIdClasificacion.Properties.MaskSettings.Set("mask", "n0")
        Me.txtIdClasificacion.Size = New System.Drawing.Size(281, 22)
        Me.txtIdClasificacion.TabIndex = 2
        '
        'txtNombreClasificacion
        '
        Me.txtNombreClasificacion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNombreClasificacion.Location = New System.Drawing.Point(578, 31)
        Me.txtNombreClasificacion.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtNombreClasificacion.MenuManager = Me.RibbonControl
        Me.txtNombreClasificacion.Name = "txtNombreClasificacion"
        Me.txtNombreClasificacion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtNombreClasificacion.Properties.ReadOnly = True
        Me.txtNombreClasificacion.Size = New System.Drawing.Size(940, 22)
        Me.txtNombreClasificacion.TabIndex = 3
        '
        'GrpTiempo
        '
        Me.GrpTiempo.Controls.Add(Me.GridTiempo)
        Me.GrpTiempo.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GrpTiempo.Location = New System.Drawing.Point(2, 273)
        Me.GrpTiempo.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GrpTiempo.Name = "GrpTiempo"
        Me.GrpTiempo.Size = New System.Drawing.Size(1609, 319)
        Me.GrpTiempo.TabIndex = 1
        Me.GrpTiempo.Text = "Tiempos Detalle"
        '
        'GridTiempo
        '
        Me.GridTiempo.Cursor = System.Windows.Forms.Cursors.Default
        Me.GridTiempo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridTiempo.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GridTiempo.Location = New System.Drawing.Point(2, 28)
        Me.GridTiempo.MainView = Me.GridViewTiempo
        Me.GridTiempo.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GridTiempo.MenuManager = Me.RibbonControl
        Me.GridTiempo.Name = "GridTiempo"
        Me.GridTiempo.Size = New System.Drawing.Size(1605, 289)
        Me.GridTiempo.TabIndex = 0
        Me.GridTiempo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewTiempo})
        '
        'GridViewTiempo
        '
        Me.GridViewTiempo.DetailHeight = 674
        Me.GridViewTiempo.GridControl = Me.GridTiempo
        Me.GridViewTiempo.Name = "GridViewTiempo"
        Me.GridViewTiempo.OptionsBehavior.Editable = False
        Me.GridViewTiempo.OptionsView.ShowGroupPanel = False
        '
        'ProveedorBodega
        '
        Me.ProveedorBodega.Controls.Add(Me.GrpEmpresaTB)
        Me.ProveedorBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ProveedorBodega.Name = "ProveedorBodega"
        Me.ProveedorBodega.Size = New System.Drawing.Size(1613, 594)
        Me.ProveedorBodega.Text = "Proveedor Bodega"
        '
        'Label8
        '
        Label8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(591, 211)
        Label8.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(35, 16)
        Label8.TabIndex = 87
        Label8.Text = "País:"
        '
        'cmbPais
        '
        Me.cmbPais.Location = New System.Drawing.Point(721, 206)
        Me.cmbPais.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPais.Name = "cmbPais"
        Me.cmbPais.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPais.Properties.NullText = ""
        Me.cmbPais.Size = New System.Drawing.Size(279, 22)
        Me.cmbPais.TabIndex = 88
        '
        'frmProveedor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1615, 873)
        Me.Controls.Add(Me.xtrTabProveedor)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmProveedor"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Proveedor"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.cmbBodegaAreaSAP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodegaWMS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReferenciaCliente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtContacto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCorreo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDireccion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTelefono.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NombreTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpEmpresaTB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpEmpresaTB.ResumeLayout(False)
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsProveedor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AreaBodegaGridLookUpEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkProveedor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.xtrTabProveedor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtrTabProveedor.ResumeLayout(False)
        Me.datosProveedor.ResumeLayout(False)
        Me.tiempoAceptacion.ResumeLayout(False)
        CType(Me.GrpTiempoAceptacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpTiempoAceptacion.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.txtDiaExterior, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiaLocal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdFamilia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreFamilia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdClasificacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreClasificacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpTiempo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpTiempo.ResumeLayout(False)
        CType(Me.GridTiempo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewTiempo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ProveedorBodega.ResumeLayout(False)
        CType(Me.cmbPais.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents NombreTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblCodigo As System.Windows.Forms.Label
    Friend WithEvents GrpEmpresaTB As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtContacto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCorreo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDireccion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTelefono As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Grid As DevExpress.XtraGrid.GridControl
    Friend WithEvents DataBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsProveedor As TOMWMS.DsProveedor
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colAsignar As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents colIdBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdAsignacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdInterno As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodProveedor As TextBox
    Friend WithEvents lblCodProv As Label
    Friend WithEvents dkProveedor As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents xtrTabProveedor As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents datosProveedor As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ProveedorBodega As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents cmbEmpresa As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtReferenciaCliente As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbBodegaWMS As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label14 As Label
    Friend WithEvents chkEsProveedorServicio As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkActivo As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkSistema As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkBodegaRec As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkBodegaTras As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkMuestraPrecio As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkActualizaCostoOC As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPageGroup4 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPageGroup5 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents tiempoAceptacion As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GrpTiempoAceptacion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents cmdGuardar As Button
    Friend WithEvents txtDiaExterior As NumericUpDown
    Friend WithEvents txtDiaLocal As NumericUpDown
    Friend WithEvents lnkFamilia As LinkLabel
    Friend WithEvents txtIdFamilia As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreFamilia As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkClasificacion As LinkLabel
    Friend WithEvents txtIdClasificacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreClasificacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GrpTiempo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridTiempo As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewTiempo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colIdAreaOrigen As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AreaBodegaGridLookUpEdit As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbBodegaAreaSAP As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbPais As DevExpress.XtraEditors.LookUpEdit
End Class
