<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRegServicio
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
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
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim lblCodigo As System.Windows.Forms.Label
        Dim lblEstados As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim lblFechaDocumento As System.Windows.Forms.Label
        Dim IdPropietarioLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim lblFechaServicio As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRegServicio))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAsignacion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEstadoEnvioERP = New DevExpress.XtraBars.BarButtonItem()
        Me.BarCheckItem1 = New DevExpress.XtraBars.BarCheckItem()
        Me.mnuCerrar = New DevExpress.XtraBars.BarButtonItem()
        Me.chkEsIngreso = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpTipoTransaccion = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.dkArancel = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.GrpTransaccion = New DevExpress.XtraEditors.GroupControl()
        Me.txtNomPedidoEnc = New DevExpress.XtraEditors.TextEdit()
        Me.lnkPedidoCliente = New System.Windows.Forms.LinkLabel()
        Me.txtIdPedidoEnc = New DevExpress.XtraEditors.TextEdit()
        Me.dtpFechaServicio = New System.Windows.Forms.DateTimePicker()
        Me.dtpFechaDocumento = New DevExpress.XtraEditors.DateEdit()
        Me.lblId = New System.Windows.Forms.Label()
        Me.lblMotivoAnulacion = New System.Windows.Forms.Label()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblEstado = New System.Windows.Forms.Label()
        Me.txtNombreDocIngreso = New DevExpress.XtraEditors.TextEdit()
        Me.lblIdServicioEnc = New System.Windows.Forms.Label()
        Me.lnkDocumentoIngreso = New System.Windows.Forms.LinkLabel()
        Me.txtIdOrdenCompra = New DevExpress.XtraEditors.TextEdit()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.dgridServiciosAsociados = New DevExpress.XtraGrid.GridControl()
        Me.gvDetalleServicios = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.mnuEstadoEnviadoAERP = New DevExpress.XtraBars.BarButtonItem()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        lblCodigo = New System.Windows.Forms.Label()
        lblEstados = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        lblFechaDocumento = New System.Windows.Forms.Label()
        IdPropietarioLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        lblFechaServicio = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkArancel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.GrpTransaccion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTransaccion.SuspendLayout()
        CType(Me.txtNomPedidoEnc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdPedidoEnc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaDocumento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaDocumento.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreDocIngreso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdOrdenCompra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dgridServiciosAsociados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetalleServicios, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(47, 9)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(85, 13)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(47, 35)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(78, 13)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(380, 9)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(89, 13)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(380, 35)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(82, 13)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'lblCodigo
        '
        lblCodigo.AutoSize = True
        lblCodigo.Location = New System.Drawing.Point(157, 30)
        lblCodigo.Name = "lblCodigo"
        lblCodigo.Size = New System.Drawing.Size(0, 13)
        lblCodigo.TabIndex = 10
        '
        'lblEstados
        '
        lblEstados.AutoSize = True
        lblEstados.Location = New System.Drawing.Point(15, 86)
        lblEstados.Name = "lblEstados"
        lblEstados.Size = New System.Drawing.Size(40, 13)
        lblEstados.TabIndex = 2
        lblEstados.Text = "Estado"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(15, 57)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(40, 13)
        Label12.TabIndex = 0
        Label12.Text = "Código"
        '
        'lblFechaDocumento
        '
        lblFechaDocumento.AutoSize = True
        lblFechaDocumento.Location = New System.Drawing.Point(14, 115)
        lblFechaDocumento.Name = "lblFechaDocumento"
        lblFechaDocumento.Size = New System.Drawing.Size(97, 13)
        lblFechaDocumento.TabIndex = 7
        lblFechaDocumento.Text = "Fecha Documento:"
        '
        'IdPropietarioLabel
        '
        IdPropietarioLabel.AutoSize = True
        IdPropietarioLabel.Location = New System.Drawing.Point(15, 141)
        IdPropietarioLabel.Name = "IdPropietarioLabel"
        IdPropietarioLabel.Size = New System.Drawing.Size(47, 13)
        IdPropietarioLabel.TabIndex = 9
        IdPropietarioLabel.Text = "Bodega:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(15, 168)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(63, 13)
        Label1.TabIndex = 11
        Label1.Text = "Propietario:"
        '
        'lblFechaServicio
        '
        lblFechaServicio.AutoSize = True
        lblFechaServicio.Location = New System.Drawing.Point(429, 62)
        lblFechaServicio.Name = "lblFechaServicio"
        lblFechaServicio.Size = New System.Drawing.Size(76, 13)
        lblFechaServicio.TabIndex = 17
        lblFechaServicio.Text = "Fecha Servicio"
        '
        'RibbonControl
        '
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(26, 24, 26, 24)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.mnuAsignacion, Me.mnuEstadoEnvioERP, Me.BarCheckItem1, Me.mnuCerrar, Me.chkEsIngreso})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RibbonControl.MaxItemId = 12
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 283
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(794, 158)
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
        Me.mnuEliminar.Caption = "Desactivar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'mnuAsignacion
        '
        Me.mnuAsignacion.Caption = "Asignacion"
        Me.mnuAsignacion.Id = 4
        Me.mnuAsignacion.ImageOptions.Image = CType(resources.GetObject("mnuAsignacion.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuAsignacion.ImageOptions.LargeImage = CType(resources.GetObject("mnuAsignacion.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuAsignacion.Name = "mnuAsignacion"
        '
        'mnuEstadoEnvioERP
        '
        Me.mnuEstadoEnvioERP.Caption = "No enviado"
        Me.mnuEstadoEnvioERP.Id = 7
        Me.mnuEstadoEnvioERP.ImageOptions.LargeImage = Global.TOMWMS.My.Resources.Resources.red_ball
        Me.mnuEstadoEnvioERP.Name = "mnuEstadoEnvioERP"
        '
        'BarCheckItem1
        '
        Me.BarCheckItem1.BindableChecked = True
        Me.BarCheckItem1.Caption = "Activo"
        Me.BarCheckItem1.Checked = True
        Me.BarCheckItem1.Id = 9
        Me.BarCheckItem1.ImageOptions.SvgImage = CType(resources.GetObject("BarCheckItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarCheckItem1.Name = "BarCheckItem1"
        '
        'mnuCerrar
        '
        Me.mnuCerrar.Caption = "Cerrar"
        Me.mnuCerrar.Id = 10
        Me.mnuCerrar.ImageOptions.Image = CType(resources.GetObject("mnuCerrar.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuCerrar.ImageOptions.LargeImage = CType(resources.GetObject("mnuCerrar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuCerrar.Name = "mnuCerrar"
        '
        'chkEsIngreso
        '
        Me.chkEsIngreso.BindableChecked = True
        Me.chkEsIngreso.Caption = "Es ingreso"
        Me.chkEsIngreso.Checked = True
        Me.chkEsIngreso.Id = 11
        Me.chkEsIngreso.Name = "chkEsIngreso"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.rpTipoTransaccion})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Registro de servicios"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEstadoEnvioERP)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarCheckItem1)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuCerrar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'rpTipoTransaccion
        '
        Me.rpTipoTransaccion.Enabled = False
        Me.rpTipoTransaccion.ItemLinks.Add(Me.chkEsIngreso)
        Me.rpTipoTransaccion.Name = "rpTipoTransaccion"
        Me.rpTipoTransaccion.Text = "Tipo Registro"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 668)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(794, 24)
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(135, 32)
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
        Me.User_agrTextEdit.Location = New System.Drawing.Point(135, 6)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(224, 20)
        Me.User_agrTextEdit.TabIndex = 1
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(468, 32)
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
        Me.User_modTextEdit.Location = New System.Drawing.Point(468, 6)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(224, 20)
        Me.User_modTextEdit.TabIndex = 2
        '
        'dkArancel
        '
        Me.dkArancel.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkArancel.Form = Me
        Me.dkArancel.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 654)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(794, 14)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("7c7484eb-6749-462a-9bd0-98c989935c63")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 89)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(794, 72)
        Me.DockPanel1.Text = "Bitácora"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.Fec_modDateEdit)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_agrDateEdit)
        Me.DockPanel1_Container.Controls.Add(Fec_modLabel)
        Me.DockPanel1_Container.Controls.Add(User_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_modTextEdit)
        Me.DockPanel1_Container.Controls.Add(Fec_agrLabel)
        Me.DockPanel1_Container.Controls.Add(User_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_agrTextEdit)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 28)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(787, 41)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'GrpTransaccion
        '
        Me.GrpTransaccion.Controls.Add(Me.txtNomPedidoEnc)
        Me.GrpTransaccion.Controls.Add(Me.lnkPedidoCliente)
        Me.GrpTransaccion.Controls.Add(Me.txtIdPedidoEnc)
        Me.GrpTransaccion.Controls.Add(lblFechaServicio)
        Me.GrpTransaccion.Controls.Add(Me.dtpFechaServicio)
        Me.GrpTransaccion.Controls.Add(Me.dtpFechaDocumento)
        Me.GrpTransaccion.Controls.Add(Me.lblId)
        Me.GrpTransaccion.Controls.Add(Me.lblMotivoAnulacion)
        Me.GrpTransaccion.Controls.Add(Me.cmbPropietario)
        Me.GrpTransaccion.Controls.Add(Me.cmbBodega)
        Me.GrpTransaccion.Controls.Add(Me.lblEstado)
        Me.GrpTransaccion.Controls.Add(lblEstados)
        Me.GrpTransaccion.Controls.Add(Label12)
        Me.GrpTransaccion.Controls.Add(lblFechaDocumento)
        Me.GrpTransaccion.Controls.Add(IdPropietarioLabel)
        Me.GrpTransaccion.Controls.Add(Me.txtNombreDocIngreso)
        Me.GrpTransaccion.Controls.Add(Me.lblIdServicioEnc)
        Me.GrpTransaccion.Controls.Add(Me.lnkDocumentoIngreso)
        Me.GrpTransaccion.Controls.Add(Label1)
        Me.GrpTransaccion.Controls.Add(Me.txtIdOrdenCompra)
        Me.GrpTransaccion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpTransaccion.Location = New System.Drawing.Point(0, 0)
        Me.GrpTransaccion.Name = "GrpTransaccion"
        Me.GrpTransaccion.Size = New System.Drawing.Size(794, 263)
        Me.GrpTransaccion.TabIndex = 1
        Me.GrpTransaccion.Text = "Transacción de Ingreso"
        '
        'txtNomPedidoEnc
        '
        Me.txtNomPedidoEnc.Location = New System.Drawing.Point(197, 217)
        Me.txtNomPedidoEnc.MenuManager = Me.RibbonControl
        Me.txtNomPedidoEnc.Name = "txtNomPedidoEnc"
        Me.txtNomPedidoEnc.Properties.ReadOnly = True
        Me.txtNomPedidoEnc.Size = New System.Drawing.Size(532, 20)
        Me.txtNomPedidoEnc.TabIndex = 20
        '
        'lnkPedidoCliente
        '
        Me.lnkPedidoCliente.AutoSize = True
        Me.lnkPedidoCliente.Location = New System.Drawing.Point(15, 220)
        Me.lnkPedidoCliente.Name = "lnkPedidoCliente"
        Me.lnkPedidoCliente.Size = New System.Drawing.Size(75, 13)
        Me.lnkPedidoCliente.TabIndex = 18
        Me.lnkPedidoCliente.TabStop = True
        Me.lnkPedidoCliente.Text = "Doc. de Salida"
        '
        'txtIdPedidoEnc
        '
        Me.txtIdPedidoEnc.Location = New System.Drawing.Point(121, 217)
        Me.txtIdPedidoEnc.MenuManager = Me.RibbonControl
        Me.txtIdPedidoEnc.Name = "txtIdPedidoEnc"
        Me.txtIdPedidoEnc.Properties.Mask.EditMask = "n0"
        Me.txtIdPedidoEnc.Size = New System.Drawing.Size(70, 20)
        Me.txtIdPedidoEnc.TabIndex = 19
        '
        'dtpFechaServicio
        '
        Me.dtpFechaServicio.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaServicio.Location = New System.Drawing.Point(515, 59)
        Me.dtpFechaServicio.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dtpFechaServicio.Name = "dtpFechaServicio"
        Me.dtpFechaServicio.Size = New System.Drawing.Size(172, 21)
        Me.dtpFechaServicio.TabIndex = 16
        '
        'dtpFechaDocumento
        '
        Me.dtpFechaDocumento.EditValue = New Date(2017, 11, 19, 11, 13, 59, 0)
        Me.dtpFechaDocumento.Location = New System.Drawing.Point(121, 114)
        Me.dtpFechaDocumento.MenuManager = Me.RibbonControl
        Me.dtpFechaDocumento.Name = "dtpFechaDocumento"
        Me.dtpFechaDocumento.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaDocumento.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaDocumento.Properties.ReadOnly = True
        Me.dtpFechaDocumento.Size = New System.Drawing.Size(282, 20)
        Me.dtpFechaDocumento.TabIndex = 8
        '
        'lblId
        '
        Me.lblId.AutoSize = True
        Me.lblId.Location = New System.Drawing.Point(180, 89)
        Me.lblId.Name = "lblId"
        Me.lblId.Size = New System.Drawing.Size(0, 13)
        Me.lblId.TabIndex = 5
        Me.lblId.Visible = False
        '
        'lblMotivoAnulacion
        '
        Me.lblMotivoAnulacion.AutoSize = True
        Me.lblMotivoAnulacion.Location = New System.Drawing.Point(194, 89)
        Me.lblMotivoAnulacion.Name = "lblMotivoAnulacion"
        Me.lblMotivoAnulacion.Size = New System.Drawing.Size(0, 13)
        Me.lblMotivoAnulacion.TabIndex = 6
        Me.lblMotivoAnulacion.Visible = False
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Location = New System.Drawing.Point(121, 167)
        Me.cmbPropietario.MenuManager = Me.RibbonControl
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Properties.ReadOnly = True
        Me.cmbPropietario.Size = New System.Drawing.Size(282, 20)
        Me.cmbPropietario.TabIndex = 12
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(121, 140)
        Me.cmbBodega.MenuManager = Me.RibbonControl
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Properties.ReadOnly = True
        Me.cmbBodega.Size = New System.Drawing.Size(282, 20)
        Me.cmbBodega.TabIndex = 10
        '
        'lblEstado
        '
        Me.lblEstado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEstado.Location = New System.Drawing.Point(123, 85)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(281, 20)
        Me.lblEstado.TabIndex = 3
        '
        'txtNombreDocIngreso
        '
        Me.txtNombreDocIngreso.Location = New System.Drawing.Point(197, 193)
        Me.txtNombreDocIngreso.MenuManager = Me.RibbonControl
        Me.txtNombreDocIngreso.Name = "txtNombreDocIngreso"
        Me.txtNombreDocIngreso.Properties.ReadOnly = True
        Me.txtNombreDocIngreso.Size = New System.Drawing.Size(206, 20)
        Me.txtNombreDocIngreso.TabIndex = 15
        '
        'lblIdServicioEnc
        '
        Me.lblIdServicioEnc.BackColor = System.Drawing.Color.Bisque
        Me.lblIdServicioEnc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIdServicioEnc.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIdServicioEnc.Location = New System.Drawing.Point(123, 55)
        Me.lblIdServicioEnc.Name = "lblIdServicioEnc"
        Me.lblIdServicioEnc.Size = New System.Drawing.Size(281, 22)
        Me.lblIdServicioEnc.TabIndex = 1
        '
        'lnkDocumentoIngreso
        '
        Me.lnkDocumentoIngreso.AutoSize = True
        Me.lnkDocumentoIngreso.Location = New System.Drawing.Point(15, 196)
        Me.lnkDocumentoIngreso.Name = "lnkDocumentoIngreso"
        Me.lnkDocumentoIngreso.Size = New System.Drawing.Size(84, 13)
        Me.lnkDocumentoIngreso.TabIndex = 13
        Me.lnkDocumentoIngreso.TabStop = True
        Me.lnkDocumentoIngreso.Text = "Doc. de Ingreso"
        '
        'txtIdOrdenCompra
        '
        Me.txtIdOrdenCompra.Location = New System.Drawing.Point(121, 193)
        Me.txtIdOrdenCompra.MenuManager = Me.RibbonControl
        Me.txtIdOrdenCompra.Name = "txtIdOrdenCompra"
        Me.txtIdOrdenCompra.Properties.Mask.EditMask = "n0"
        Me.txtIdOrdenCompra.Size = New System.Drawing.Size(70, 20)
        Me.txtIdOrdenCompra.TabIndex = 14
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 158)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GrpTransaccion)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgridServiciosAsociados)
        Me.SplitContainer1.Size = New System.Drawing.Size(794, 496)
        Me.SplitContainer1.SplitterDistance = 263
        Me.SplitContainer1.SplitterWidth = 3
        Me.SplitContainer1.TabIndex = 4
        '
        'dgridServiciosAsociados
        '
        Me.dgridServiciosAsociados.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridServiciosAsociados.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgridServiciosAsociados.Location = New System.Drawing.Point(0, 0)
        Me.dgridServiciosAsociados.MainView = Me.gvDetalleServicios
        Me.dgridServiciosAsociados.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgridServiciosAsociados.Name = "dgridServiciosAsociados"
        Me.dgridServiciosAsociados.Size = New System.Drawing.Size(794, 230)
        Me.dgridServiciosAsociados.TabIndex = 18
        Me.dgridServiciosAsociados.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvDetalleServicios})
        '
        'gvDetalleServicios
        '
        Me.gvDetalleServicios.DetailHeight = 284
        Me.gvDetalleServicios.GridControl = Me.dgridServiciosAsociados
        Me.gvDetalleServicios.Name = "gvDetalleServicios"
        Me.gvDetalleServicios.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvDetalleServicios.OptionsEditForm.PopupEditFormWidth = 686
        Me.gvDetalleServicios.OptionsView.ColumnAutoWidth = False
        Me.gvDetalleServicios.OptionsView.ShowGroupPanel = False
        '
        'mnuEstadoEnviadoAERP
        '
        Me.mnuEstadoEnviadoAERP.Caption = "No enviado"
        Me.mnuEstadoEnviadoAERP.Id = 21
        Me.mnuEstadoEnviadoAERP.ImageOptions.LargeImage = Global.TOMWMS.My.Resources.Resources.red_ball
        Me.mnuEstadoEnviadoAERP.Name = "mnuEstadoEnviadoAERP"
        '
        'frmRegServicio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 692)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmRegServicio"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Registro de servicios"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkArancel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.GrpTransaccion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpTransaccion.ResumeLayout(False)
        Me.GrpTransaccion.PerformLayout()
        CType(Me.txtNomPedidoEnc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdPedidoEnc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaDocumento.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaDocumento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreDocIngreso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdOrdenCompra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dgridServiciosAsociados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetalleServicios, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents mnuAsignacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dkArancel As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents GrpTransaccion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtpFechaDocumento As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblId As Label
    Friend WithEvents lblMotivoAnulacion As Label
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblEstado As Label
    Friend WithEvents txtNombreDocIngreso As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblIdServicioEnc As Label
    Friend WithEvents lnkDocumentoIngreso As LinkLabel
    Friend WithEvents txtIdOrdenCompra As DevExpress.XtraEditors.TextEdit
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents dgridServiciosAsociados As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDetalleServicios As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents dtpFechaServicio As DateTimePicker
    Friend WithEvents mnuEstadoEnviadoAERP As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEstadoEnvioERP As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarCheckItem1 As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents mnuCerrar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkEsIngreso As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents rpTipoTransaccion As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents txtNomPedidoEnc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkPedidoCliente As LinkLabel
    Friend WithEvents txtIdPedidoEnc As DevExpress.XtraEditors.TextEdit
End Class
