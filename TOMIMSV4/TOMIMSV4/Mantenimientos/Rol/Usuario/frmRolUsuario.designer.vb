<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRolUsuario
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
        Dim IdUsuarioLabel As System.Windows.Forms.Label
        Dim NombresLabel As System.Windows.Forms.Label
        Dim ActivoLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim lblEstadoDestino As System.Windows.Forms.Label
        Dim lblPropietarioEstado As System.Windows.Forms.Label
        Dim lblEstado As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim lblCodigoEstadoS As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRolUsuario))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.txtIdRol = New DevExpress.XtraEditors.SpinEdit()
        Me.txtNombreRol = New DevExpress.XtraEditors.TextEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.PanDatosUsuario = New DevExpress.XtraEditors.GroupControl()
        Me.chkRegistraClave = New DevExpress.XtraEditors.CheckEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.chkListaOpciones = New System.Windows.Forms.CheckedListBox()
        Me.cmdAgregarOpciones = New System.Windows.Forms.Button()
        Me.chkMenuRol = New DevExpress.XtraEditors.CheckEdit()
        Me.chkMenuSistema = New DevExpress.XtraEditors.CheckEdit()
        Me.dgMenuRol = New System.Windows.Forms.DataGridView()
        Me.SelMR = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.MenuRol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdMenuRol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdMenuRol1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgMenuSistema = New System.Windows.Forms.DataGridView()
        Me.Sel = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.MenuSistema = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SolicitarClave = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.txtrol = New DevExpress.XtraEditors.TextEdit()
        Me.txtmenu = New DevExpress.XtraEditors.TextEdit()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.cmdQuitar = New System.Windows.Forms.Button()
        Me.cmdAgregar = New System.Windows.Forms.Button()
        Me.dkRolUsuario = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.chkPermitirFlujoEstado = New DevExpress.XtraEditors.ToggleSwitch()
        Me.cmbEstadoDestino = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbPropietarioBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.cmdNuevoEstado = New System.Windows.Forms.ToolStripButton()
        Me.cmdGuardarEstado = New System.Windows.Forms.ToolStripButton()
        Me.cmdDesactivarEstado = New System.Windows.Forms.ToolStripButton()
        Me.cmbEstadoOrigen = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblCodigoEstado = New System.Windows.Forms.Label()
        Me.chkEstadoActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.dGridEstados = New DevExpress.XtraGrid.GridControl()
        Me.grdEstados = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.chkEstadosActivos = New DevExpress.XtraEditors.CheckEdit()
        IdUsuarioLabel = New System.Windows.Forms.Label()
        NombresLabel = New System.Windows.Forms.Label()
        ActivoLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        lblEstadoDestino = New System.Windows.Forms.Label()
        lblPropietarioEstado = New System.Windows.Forms.Label()
        lblEstado = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        lblCodigoEstadoS = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdRol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreRol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanDatosUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanDatosUsuario.SuspendLayout()
        CType(Me.chkRegistraClave.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.chkMenuRol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMenuSistema.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgMenuRol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgMenuSistema, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtrol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtmenu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkRolUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.chkPermitirFlujoEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEstadoDestino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietarioBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.cmbEstadoOrigen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEstadoActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.dGridEstados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdEstados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEstadosActivos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'IdUsuarioLabel
        '
        IdUsuarioLabel.AutoSize = True
        IdUsuarioLabel.Location = New System.Drawing.Point(41, 48)
        IdUsuarioLabel.Name = "IdUsuarioLabel"
        IdUsuarioLabel.Size = New System.Drawing.Size(46, 16)
        IdUsuarioLabel.TabIndex = 0
        IdUsuarioLabel.Text = "Código"
        '
        'NombresLabel
        '
        NombresLabel.AutoSize = True
        NombresLabel.Location = New System.Drawing.Point(41, 80)
        NombresLabel.Name = "NombresLabel"
        NombresLabel.Size = New System.Drawing.Size(57, 16)
        NombresLabel.TabIndex = 2
        NombresLabel.Text = "Nombre:"
        '
        'ActivoLabel
        '
        ActivoLabel.AutoSize = True
        ActivoLabel.Location = New System.Drawing.Point(41, 150)
        ActivoLabel.Name = "ActivoLabel"
        ActivoLabel.Size = New System.Drawing.Size(46, 16)
        ActivoLabel.TabIndex = 4
        ActivoLabel.Text = "Activo:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(110, 18)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(60, 16)
        User_agrLabel.TabIndex = 2
        User_agrLabel.Text = "user agr:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(110, 50)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(52, 16)
        Fec_agrLabel.TabIndex = 6
        Fec_agrLabel.Text = "fec agr:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(451, 11)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(66, 16)
        User_modLabel.TabIndex = 0
        User_modLabel.Text = "user mod:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(451, 43)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(58, 16)
        Fec_modLabel.TabIndex = 4
        Fec_modLabel.Text = "fec mod:"
        '
        'lblEstadoDestino
        '
        lblEstadoDestino.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblEstadoDestino.AutoSize = True
        lblEstadoDestino.ForeColor = System.Drawing.Color.Black
        lblEstadoDestino.Location = New System.Drawing.Point(30, 165)
        lblEstadoDestino.Name = "lblEstadoDestino"
        lblEstadoDestino.Size = New System.Drawing.Size(96, 16)
        lblEstadoDestino.TabIndex = 19
        lblEstadoDestino.Text = "Estado Destino:"
        '
        'lblPropietarioEstado
        '
        lblPropietarioEstado.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblPropietarioEstado.AutoSize = True
        lblPropietarioEstado.ForeColor = System.Drawing.Color.Black
        lblPropietarioEstado.Location = New System.Drawing.Point(30, 109)
        lblPropietarioEstado.Name = "lblPropietarioEstado"
        lblPropietarioEstado.Size = New System.Drawing.Size(74, 16)
        lblPropietarioEstado.TabIndex = 17
        lblPropietarioEstado.Text = "Propietario:"
        '
        'lblEstado
        '
        lblEstado.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblEstado.AutoSize = True
        lblEstado.ForeColor = System.Drawing.Color.Black
        lblEstado.Location = New System.Drawing.Point(30, 137)
        lblEstado.Name = "lblEstado"
        lblEstado.Size = New System.Drawing.Size(92, 16)
        lblEstado.TabIndex = 10
        lblEstado.Text = "Estado Origen:"
        '
        'Label7
        '
        Label7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(449, 78)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(46, 16)
        Label7.TabIndex = 6
        Label7.Text = "Activo:"
        '
        'lblCodigoEstadoS
        '
        lblCodigoEstadoS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCodigoEstadoS.AutoSize = True
        lblCodigoEstadoS.Location = New System.Drawing.Point(30, 78)
        lblCodigoEstadoS.Name = "lblCodigoEstadoS"
        lblCodigoEstadoS.Size = New System.Drawing.Size(51, 16)
        lblCodigoEstadoS.TabIndex = 0
        lblCodigoEstadoS.Text = "Código:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 6
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1140, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Rol Usuario"
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 854)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1140, 30)
        '
        'txtIdRol
        '
        Me.txtIdRol.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtIdRol.Enabled = False
        Me.txtIdRol.Location = New System.Drawing.Point(233, 44)
        Me.txtIdRol.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdRol.MenuManager = Me.RibbonControl
        Me.txtIdRol.Name = "txtIdRol"
        Me.txtIdRol.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtIdRol.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtIdRol.Size = New System.Drawing.Size(247, 24)
        Me.txtIdRol.TabIndex = 1
        '
        'txtNombreRol
        '
        Me.txtNombreRol.Location = New System.Drawing.Point(233, 76)
        Me.txtNombreRol.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreRol.MenuManager = Me.RibbonControl
        Me.txtNombreRol.Name = "txtNombreRol"
        Me.txtNombreRol.Size = New System.Drawing.Size(247, 22)
        Me.txtNombreRol.TabIndex = 3
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(233, 143)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(20, 24)
        Me.chkActivo.TabIndex = 5
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(191, 15)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(218, 22)
        Me.User_agrTextEdit.TabIndex = 3
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(191, 47)
        Me.Fec_agrDateEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_agrDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.Fec_agrDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(218, 22)
        Me.Fec_agrDateEdit.TabIndex = 7
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(533, 7)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(218, 22)
        Me.User_modTextEdit.TabIndex = 1
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(533, 39)
        Me.Fec_modDateEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_modDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.Fec_modDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(218, 22)
        Me.Fec_modDateEdit.TabIndex = 5
        '
        'PanDatosUsuario
        '
        Me.PanDatosUsuario.Controls.Add(Me.chkRegistraClave)
        Me.PanDatosUsuario.Controls.Add(Me.Label1)
        Me.PanDatosUsuario.Controls.Add(IdUsuarioLabel)
        Me.PanDatosUsuario.Controls.Add(Me.txtIdRol)
        Me.PanDatosUsuario.Controls.Add(NombresLabel)
        Me.PanDatosUsuario.Controls.Add(ActivoLabel)
        Me.PanDatosUsuario.Controls.Add(Me.txtNombreRol)
        Me.PanDatosUsuario.Controls.Add(Me.chkActivo)
        Me.PanDatosUsuario.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanDatosUsuario.Location = New System.Drawing.Point(0, 0)
        Me.PanDatosUsuario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanDatosUsuario.Name = "PanDatosUsuario"
        Me.PanDatosUsuario.Size = New System.Drawing.Size(1138, 177)
        Me.PanDatosUsuario.TabIndex = 0
        Me.PanDatosUsuario.Text = "Rol"
        '
        'chkRegistraClave
        '
        Me.chkRegistraClave.Location = New System.Drawing.Point(233, 113)
        Me.chkRegistraClave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkRegistraClave.MenuManager = Me.RibbonControl
        Me.chkRegistraClave.Name = "chkRegistraClave"
        Me.chkRegistraClave.Properties.Caption = ""
        Me.chkRegistraClave.Size = New System.Drawing.Size(20, 24)
        Me.chkRegistraClave.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(41, 117)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(188, 16)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Registrar clave de autorización:"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.chkListaOpciones)
        Me.GroupControl1.Controls.Add(Me.cmdAgregarOpciones)
        Me.GroupControl1.Controls.Add(Me.chkMenuRol)
        Me.GroupControl1.Controls.Add(Me.chkMenuSistema)
        Me.GroupControl1.Controls.Add(Me.dgMenuRol)
        Me.GroupControl1.Controls.Add(Me.dgMenuSistema)
        Me.GroupControl1.Controls.Add(Me.txtrol)
        Me.GroupControl1.Controls.Add(Me.txtmenu)
        Me.GroupControl1.Controls.Add(Me.prg)
        Me.GroupControl1.Controls.Add(Me.cmdQuitar)
        Me.GroupControl1.Controls.Add(Me.cmdAgregar)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 177)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1138, 428)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "Permisos"
        '
        'chkListaOpciones
        '
        Me.chkListaOpciones.FormattingEnabled = True
        Me.chkListaOpciones.Items.AddRange(New Object() {"Leer", "Modificar", "Eliminar"})
        Me.chkListaOpciones.Location = New System.Drawing.Point(979, 155)
        Me.chkListaOpciones.Name = "chkListaOpciones"
        Me.chkListaOpciones.Size = New System.Drawing.Size(115, 76)
        Me.chkListaOpciones.TabIndex = 17
        '
        'cmdAgregarOpciones
        '
        Me.cmdAgregarOpciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAgregarOpciones.Location = New System.Drawing.Point(873, 181)
        Me.cmdAgregarOpciones.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdAgregarOpciones.Name = "cmdAgregarOpciones"
        Me.cmdAgregarOpciones.Size = New System.Drawing.Size(87, 28)
        Me.cmdAgregarOpciones.TabIndex = 16
        Me.cmdAgregarOpciones.Text = "<<"
        Me.cmdAgregarOpciones.UseVisualStyleBackColor = True
        '
        'chkMenuRol
        '
        Me.chkMenuRol.Location = New System.Drawing.Point(487, 367)
        Me.chkMenuRol.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkMenuRol.MenuManager = Me.RibbonControl
        Me.chkMenuRol.Name = "chkMenuRol"
        Me.chkMenuRol.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.chkMenuRol.Properties.Appearance.ForeColor = System.Drawing.Color.DimGray
        Me.chkMenuRol.Properties.Appearance.Options.UseFont = True
        Me.chkMenuRol.Properties.Appearance.Options.UseForeColor = True
        Me.chkMenuRol.Properties.Caption = "Todos"
        Me.chkMenuRol.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkMenuRol.Size = New System.Drawing.Size(87, 25)
        Me.chkMenuRol.TabIndex = 14
        '
        'chkMenuSistema
        '
        Me.chkMenuSistema.Location = New System.Drawing.Point(24, 367)
        Me.chkMenuSistema.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkMenuSistema.MenuManager = Me.RibbonControl
        Me.chkMenuSistema.Name = "chkMenuSistema"
        Me.chkMenuSistema.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.chkMenuSistema.Properties.Appearance.ForeColor = System.Drawing.Color.DimGray
        Me.chkMenuSistema.Properties.Appearance.Options.UseFont = True
        Me.chkMenuSistema.Properties.Appearance.Options.UseForeColor = True
        Me.chkMenuSistema.Properties.Caption = "Todos"
        Me.chkMenuSistema.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkMenuSistema.Size = New System.Drawing.Size(79, 25)
        Me.chkMenuSistema.TabIndex = 13
        '
        'dgMenuRol
        '
        Me.dgMenuRol.AllowUserToAddRows = False
        Me.dgMenuRol.AllowUserToDeleteRows = False
        Me.dgMenuRol.AllowUserToOrderColumns = True
        Me.dgMenuRol.BackgroundColor = System.Drawing.Color.White
        Me.dgMenuRol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgMenuRol.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SelMR, Me.MenuRol, Me.IdMenuRol, Me.IdMenuRol1})
        Me.dgMenuRol.Location = New System.Drawing.Point(488, 70)
        Me.dgMenuRol.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgMenuRol.Name = "dgMenuRol"
        Me.dgMenuRol.RowHeadersVisible = False
        Me.dgMenuRol.RowHeadersWidth = 51
        Me.dgMenuRol.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgMenuRol.Size = New System.Drawing.Size(360, 289)
        Me.dgMenuRol.TabIndex = 12
        '
        'SelMR
        '
        Me.SelMR.HeaderText = ""
        Me.SelMR.MinimumWidth = 6
        Me.SelMR.Name = "SelMR"
        Me.SelMR.Width = 30
        '
        'MenuRol
        '
        Me.MenuRol.HeaderText = "Menu rol"
        Me.MenuRol.MinimumWidth = 6
        Me.MenuRol.Name = "MenuRol"
        Me.MenuRol.Width = 250
        '
        'IdMenuRol
        '
        Me.IdMenuRol.HeaderText = "IdMenu"
        Me.IdMenuRol.MinimumWidth = 6
        Me.IdMenuRol.Name = "IdMenuRol"
        Me.IdMenuRol.Visible = False
        Me.IdMenuRol.Width = 125
        '
        'IdMenuRol1
        '
        Me.IdMenuRol1.HeaderText = "IdMenuRol"
        Me.IdMenuRol1.MinimumWidth = 6
        Me.IdMenuRol1.Name = "IdMenuRol1"
        Me.IdMenuRol1.Visible = False
        Me.IdMenuRol1.Width = 125
        '
        'dgMenuSistema
        '
        Me.dgMenuSistema.AllowUserToAddRows = False
        Me.dgMenuSistema.AllowUserToDeleteRows = False
        Me.dgMenuSistema.AllowUserToOrderColumns = True
        Me.dgMenuSistema.AllowUserToResizeRows = False
        Me.dgMenuSistema.BackgroundColor = System.Drawing.Color.White
        Me.dgMenuSistema.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgMenuSistema.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Sel, Me.MenuSistema, Me.MenuID, Me.SolicitarClave})
        Me.dgMenuSistema.Location = New System.Drawing.Point(29, 70)
        Me.dgMenuSistema.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgMenuSistema.Name = "dgMenuSistema"
        Me.dgMenuSistema.RowHeadersVisible = False
        Me.dgMenuSistema.RowHeadersWidth = 51
        Me.dgMenuSistema.Size = New System.Drawing.Size(360, 289)
        Me.dgMenuSistema.TabIndex = 11
        '
        'Sel
        '
        Me.Sel.HeaderText = ""
        Me.Sel.MinimumWidth = 6
        Me.Sel.Name = "Sel"
        Me.Sel.Width = 30
        '
        'MenuSistema
        '
        Me.MenuSistema.HeaderText = "Menu sistema"
        Me.MenuSistema.MinimumWidth = 6
        Me.MenuSistema.Name = "MenuSistema"
        Me.MenuSistema.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MenuSistema.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MenuSistema.Width = 170
        '
        'MenuID
        '
        Me.MenuID.HeaderText = "MenuID"
        Me.MenuID.MinimumWidth = 6
        Me.MenuID.Name = "MenuID"
        Me.MenuID.Visible = False
        Me.MenuID.Width = 6
        '
        'SolicitarClave
        '
        Me.SolicitarClave.HeaderText = "Solicitar clave"
        Me.SolicitarClave.MinimumWidth = 6
        Me.SolicitarClave.Name = "SolicitarClave"
        Me.SolicitarClave.Width = 80
        '
        'txtrol
        '
        Me.txtrol.Location = New System.Drawing.Point(488, 38)
        Me.txtrol.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtrol.MenuManager = Me.RibbonControl
        Me.txtrol.Name = "txtrol"
        Me.txtrol.Properties.NullText = "Busque aquí"
        Me.txtrol.Size = New System.Drawing.Size(360, 22)
        Me.txtrol.TabIndex = 10
        '
        'txtmenu
        '
        Me.txtmenu.Location = New System.Drawing.Point(29, 38)
        Me.txtmenu.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtmenu.MenuManager = Me.RibbonControl
        Me.txtmenu.Name = "txtmenu"
        Me.txtmenu.Properties.NullText = "Busque aquí"
        Me.txtmenu.Size = New System.Drawing.Size(360, 22)
        Me.txtmenu.TabIndex = 9
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.prg.Location = New System.Drawing.Point(2, 398)
        Me.prg.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(1134, 28)
        Me.prg.TabIndex = 6
        Me.prg.Visible = False
        '
        'cmdQuitar
        '
        Me.cmdQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdQuitar.Location = New System.Drawing.Point(394, 201)
        Me.cmdQuitar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdQuitar.Name = "cmdQuitar"
        Me.cmdQuitar.Size = New System.Drawing.Size(87, 28)
        Me.cmdQuitar.TabIndex = 2
        Me.cmdQuitar.Text = "<<"
        Me.cmdQuitar.UseVisualStyleBackColor = True
        '
        'cmdAgregar
        '
        Me.cmdAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAgregar.Location = New System.Drawing.Point(394, 154)
        Me.cmdAgregar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdAgregar.Name = "cmdAgregar"
        Me.cmdAgregar.Size = New System.Drawing.Size(87, 28)
        Me.cmdAgregar.TabIndex = 1
        Me.cmdAgregar.Text = ">>"
        Me.cmdAgregar.UseVisualStyleBackColor = True
        '
        'dkRolUsuario
        '
        Me.dkRolUsuario.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkRolUsuario.Form = Me
        Me.dkRolUsuario.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.White
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 828)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1140, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("dafbe697-e6dc-4363-bb8e-d95a091a48ce")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 91)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(888, 112)
        Me.DockPanel1.Text = "Bitácora"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(User_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_agrTextEdit)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_modDateEdit)
        Me.DockPanel1_Container.Controls.Add(Fec_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Fec_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_agrDateEdit)
        Me.DockPanel1_Container.Controls.Add(Me.User_modTextEdit)
        Me.DockPanel1_Container.Controls.Add(User_modLabel)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 31)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(881, 78)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 193)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage1
        Me.XtraTabControl1.Size = New System.Drawing.Size(1140, 635)
        Me.XtraTabControl1.TabIndex = 5
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.GroupControl1)
        Me.XtraTabPage1.Controls.Add(Me.PanDatosUsuario)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1138, 605)
        Me.XtraTabPage1.Text = "Configuración de Rol"
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.GroupControl3)
        Me.XtraTabPage2.Controls.Add(Me.GroupControl2)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(1138, 605)
        Me.XtraTabPage2.Text = "Estados"
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.chkPermitirFlujoEstado)
        Me.GroupControl3.Controls.Add(lblEstadoDestino)
        Me.GroupControl3.Controls.Add(Me.cmbEstadoDestino)
        Me.GroupControl3.Controls.Add(lblPropietarioEstado)
        Me.GroupControl3.Controls.Add(Me.cmbPropietarioBodega)
        Me.GroupControl3.Controls.Add(Me.ToolStrip1)
        Me.GroupControl3.Controls.Add(lblEstado)
        Me.GroupControl3.Controls.Add(Me.cmbEstadoOrigen)
        Me.GroupControl3.Controls.Add(Me.lblCodigoEstado)
        Me.GroupControl3.Controls.Add(Label7)
        Me.GroupControl3.Controls.Add(Me.chkEstadoActivo)
        Me.GroupControl3.Controls.Add(lblCodigoEstadoS)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(1138, 224)
        Me.GroupControl3.TabIndex = 35
        Me.GroupControl3.Text = "Estados por Usuario"
        '
        'chkPermitirFlujoEstado
        '
        Me.chkPermitirFlujoEstado.Location = New System.Drawing.Point(177, 192)
        Me.chkPermitirFlujoEstado.MenuManager = Me.RibbonControl
        Me.chkPermitirFlujoEstado.Name = "chkPermitirFlujoEstado"
        Me.chkPermitirFlujoEstado.Properties.OffText = "No Permitir"
        Me.chkPermitirFlujoEstado.Properties.OnText = "Permitir"
        Me.chkPermitirFlujoEstado.Size = New System.Drawing.Size(241, 24)
        Me.chkPermitirFlujoEstado.TabIndex = 21
        '
        'cmbEstadoDestino
        '
        Me.cmbEstadoDestino.Location = New System.Drawing.Point(177, 164)
        Me.cmbEstadoDestino.Name = "cmbEstadoDestino"
        Me.cmbEstadoDestino.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbEstadoDestino.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEstadoDestino.Properties.NullText = ""
        Me.cmbEstadoDestino.Size = New System.Drawing.Size(241, 22)
        Me.cmbEstadoDestino.TabIndex = 20
        '
        'cmbPropietarioBodega
        '
        Me.cmbPropietarioBodega.Location = New System.Drawing.Point(177, 108)
        Me.cmbPropietarioBodega.Name = "cmbPropietarioBodega"
        Me.cmbPropietarioBodega.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbPropietarioBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietarioBodega.Properties.NullText = ""
        Me.cmbPropietarioBodega.Size = New System.Drawing.Size(241, 22)
        Me.cmbPropietarioBodega.TabIndex = 18
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdNuevoEstado, Me.cmdGuardarEstado, Me.cmdDesactivarEstado})
        Me.ToolStrip1.Location = New System.Drawing.Point(2, 28)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1134, 27)
        Me.ToolStrip1.TabIndex = 16
        Me.ToolStrip1.Text = "ToolStrip2"
        '
        'cmdNuevoEstado
        '
        Me.cmdNuevoEstado.Image = CType(resources.GetObject("cmdNuevoEstado.Image"), System.Drawing.Image)
        Me.cmdNuevoEstado.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdNuevoEstado.Name = "cmdNuevoEstado"
        Me.cmdNuevoEstado.Size = New System.Drawing.Size(76, 24)
        Me.cmdNuevoEstado.Text = "Nuevo"
        '
        'cmdGuardarEstado
        '
        Me.cmdGuardarEstado.Image = CType(resources.GetObject("cmdGuardarEstado.Image"), System.Drawing.Image)
        Me.cmdGuardarEstado.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdGuardarEstado.Name = "cmdGuardarEstado"
        Me.cmdGuardarEstado.Size = New System.Drawing.Size(86, 24)
        Me.cmdGuardarEstado.Text = "Guardar"
        '
        'cmdDesactivarEstado
        '
        Me.cmdDesactivarEstado.Image = CType(resources.GetObject("cmdDesactivarEstado.Image"), System.Drawing.Image)
        Me.cmdDesactivarEstado.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDesactivarEstado.Name = "cmdDesactivarEstado"
        Me.cmdDesactivarEstado.Size = New System.Drawing.Size(102, 24)
        Me.cmdDesactivarEstado.Text = "Desactivar"
        '
        'cmbEstadoOrigen
        '
        Me.cmbEstadoOrigen.Location = New System.Drawing.Point(177, 136)
        Me.cmbEstadoOrigen.Name = "cmbEstadoOrigen"
        Me.cmbEstadoOrigen.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbEstadoOrigen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEstadoOrigen.Properties.NullText = ""
        Me.cmbEstadoOrigen.Size = New System.Drawing.Size(241, 22)
        Me.cmbEstadoOrigen.TabIndex = 11
        '
        'lblCodigoEstado
        '
        Me.lblCodigoEstado.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar
        Me.lblCodigoEstado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCodigoEstado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodigoEstado.Location = New System.Drawing.Point(177, 76)
        Me.lblCodigoEstado.Name = "lblCodigoEstado"
        Me.lblCodigoEstado.Size = New System.Drawing.Size(241, 26)
        Me.lblCodigoEstado.TabIndex = 1
        '
        'chkEstadoActivo
        '
        Me.chkEstadoActivo.EditValue = True
        Me.chkEstadoActivo.Location = New System.Drawing.Point(522, 74)
        Me.chkEstadoActivo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkEstadoActivo.MenuManager = Me.RibbonControl
        Me.chkEstadoActivo.Name = "chkEstadoActivo"
        Me.chkEstadoActivo.Properties.Caption = ""
        Me.chkEstadoActivo.Size = New System.Drawing.Size(41, 24)
        Me.chkEstadoActivo.TabIndex = 7
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.dGridEstados)
        Me.GroupControl2.Controls.Add(Me.chkEstadosActivos)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl2.Location = New System.Drawing.Point(0, 224)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1138, 381)
        Me.GroupControl2.TabIndex = 34
        Me.GroupControl2.Text = "Flujo de estados permitidos"
        '
        'dGridEstados
        '
        Me.dGridEstados.Cursor = System.Windows.Forms.Cursors.Default
        Me.dGridEstados.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dGridEstados.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.RelationName = "Level1"
        Me.dGridEstados.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.dGridEstados.Location = New System.Drawing.Point(2, 52)
        Me.dGridEstados.MainView = Me.grdEstados
        Me.dGridEstados.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dGridEstados.MenuManager = Me.RibbonControl
        Me.dGridEstados.Name = "dGridEstados"
        Me.dGridEstados.Size = New System.Drawing.Size(1134, 327)
        Me.dGridEstados.TabIndex = 0
        Me.dGridEstados.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdEstados, Me.GridView2})
        '
        'grdEstados
        '
        Me.grdEstados.DetailHeight = 431
        Me.grdEstados.GridControl = Me.dGridEstados
        Me.grdEstados.Name = "grdEstados"
        Me.grdEstados.OptionsBehavior.Editable = False
        Me.grdEstados.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grdEstados.OptionsView.ShowGroupPanel = False
        '
        'GridView2
        '
        Me.GridView2.DetailHeight = 431
        Me.GridView2.GridControl = Me.dGridEstados
        Me.GridView2.Name = "GridView2"
        '
        'chkEstadosActivos
        '
        Me.chkEstadosActivos.Dock = System.Windows.Forms.DockStyle.Top
        Me.chkEstadosActivos.EditValue = True
        Me.chkEstadosActivos.Location = New System.Drawing.Point(2, 28)
        Me.chkEstadosActivos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkEstadosActivos.MenuManager = Me.RibbonControl
        Me.chkEstadosActivos.Name = "chkEstadosActivos"
        Me.chkEstadosActivos.Properties.Caption = "Registros Activos"
        Me.chkEstadosActivos.Size = New System.Drawing.Size(1134, 24)
        Me.chkEstadosActivos.TabIndex = 2
        '
        'frmRolUsuario
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1140, 884)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmRolUsuario"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Rol Usuario"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdRol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreRol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanDatosUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanDatosUsuario.ResumeLayout(False)
        Me.PanDatosUsuario.PerformLayout()
        CType(Me.chkRegistraClave.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.chkMenuRol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMenuSistema.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgMenuRol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgMenuSistema, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtrol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtmenu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkRolUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.GroupControl3.PerformLayout()
        CType(Me.chkPermitirFlujoEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEstadoDestino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPropietarioBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.cmbEstadoOrigen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEstadoActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.dGridEstados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdEstados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEstadosActivos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtIdRol As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtNombreRol As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Private WithEvents PanDatosUsuario As DevExpress.XtraEditors.GroupControl
    Private WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmdAgregar As System.Windows.Forms.Button
    Friend WithEvents cmdQuitar As System.Windows.Forms.Button
    Friend WithEvents prg As System.Windows.Forms.ProgressBar
    Friend WithEvents dkRolUsuario As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents txtmenu As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtrol As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label1 As Label
    Friend WithEvents chkRegistraClave As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents dgMenuRol As DataGridView
    Friend WithEvents dgMenuSistema As DataGridView
    Friend WithEvents SelMR As DataGridViewCheckBoxColumn
    Friend WithEvents MenuRol As DataGridViewTextBoxColumn
    Friend WithEvents IdMenuRol As DataGridViewTextBoxColumn
    Friend WithEvents IdMenuRol1 As DataGridViewTextBoxColumn
    Friend WithEvents Sel As DataGridViewCheckBoxColumn
    Friend WithEvents MenuSistema As DataGridViewTextBoxColumn
    Friend WithEvents MenuID As DataGridViewTextBoxColumn
    Friend WithEvents SolicitarClave As DataGridViewCheckBoxColumn
    Friend WithEvents chkMenuRol As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkMenuSistema As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkListaOpciones As CheckedListBox
    Friend WithEvents cmdAgregarOpciones As Button
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkPermitirFlujoEstado As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents cmbEstadoDestino As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbPropietarioBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents cmdNuevoEstado As ToolStripButton
    Friend WithEvents cmdGuardarEstado As ToolStripButton
    Friend WithEvents cmdDesactivarEstado As ToolStripButton
    Friend WithEvents cmbEstadoOrigen As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblCodigoEstado As Label
    Friend WithEvents chkEstadoActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dGridEstados As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdEstados As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkEstadosActivos As DevExpress.XtraEditors.CheckEdit
End Class
