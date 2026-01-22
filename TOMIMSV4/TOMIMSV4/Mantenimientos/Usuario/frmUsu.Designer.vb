<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmUsu
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
        Dim IdUsuarioLabel As System.Windows.Forms.Label
        Dim NombresLabel As System.Windows.Forms.Label
        Dim ApellidosLabel As System.Windows.Forms.Label
        Dim CedulaLabel As System.Windows.Forms.Label
        Dim DireccionLabel As System.Windows.Forms.Label
        Dim TelefonoLabel As System.Windows.Forms.Label
        Dim EmailLabel As System.Windows.Forms.Label
        Dim IdEmpresaLabel As System.Windows.Forms.Label
        Dim CodigoLabel As System.Windows.Forms.Label
        Dim ClaveLabel As System.Windows.Forms.Label
        Dim lblConfirmarClave As System.Windows.Forms.Label
        Dim Ultimo_loginLabel As System.Windows.Forms.Label
        Dim ActivoLabel As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim lblCorrelativoActual As System.Windows.Forms.Label
        Dim lblCorrelativoFinal As System.Windows.Forms.Label
        Dim lblBodega As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim lblCorrelativoInicial As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim lblSerie As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUsu))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUsu))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim lblUsuarioSap As System.Windows.Forms.Label
        Dim lblClaveSap As System.Windows.Forms.Label
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimirCarnet = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.PanDatosUsuario = New DevExpress.XtraEditors.GroupControl()
        Me.cbxEmpresa = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblSistema = New System.Windows.Forms.Label()
        Me.chkSistema = New System.Windows.Forms.CheckBox()
        Me.picFoto = New DevExpress.XtraEditors.PictureEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.lblReptClaAut = New System.Windows.Forms.Label()
        Me.txtReptClaveAuto = New System.Windows.Forms.TextBox()
        Me.txtClaveAutoriza = New System.Windows.Forms.TextBox()
        Me.lblClaveAuto = New System.Windows.Forms.Label()
        Me.ConfirmarClaveTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Ultimo_loginDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.ClaveTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.CodigoTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.IdUsuarioSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.NombresTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.ApellidosTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.CedulaTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.DireccionTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.TelefonoTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.EmailTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Dgrid = New System.Windows.Forms.DataGridView()
        Me.XtraTabControl = New DevExpress.XtraTab.XtraTabControl()
        Me.General = New DevExpress.XtraTab.XtraTabPage()
        Me.RolesBodega = New DevExpress.XtraTab.XtraTabPage()
        Me.Resoluciones_licencia = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.dGridResoluciones = New DevExpress.XtraGrid.GridControl()
        Me.GrdResolucion = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.chkActivoPR = New DevExpress.XtraEditors.CheckEdit()
        Me.ToolStripPR = New System.Windows.Forms.ToolStrip()
        Me.cmdNewPR = New System.Windows.Forms.ToolStripButton()
        Me.cmdSavePR = New System.Windows.Forms.ToolStripButton()
        Me.cmdDesactivarResolucion = New System.Windows.Forms.ToolStripButton()
        Me.txtCorrelativoActual = New System.Windows.Forms.NumericUpDown()
        Me.txtCorrelativoFinal = New System.Windows.Forms.NumericUpDown()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblIdResolucionLP = New System.Windows.Forms.Label()
        Me.chkResolucionLPActiva = New DevExpress.XtraEditors.CheckEdit()
        Me.txtCorrelativoInicial = New System.Windows.Forms.NumericUpDown()
        Me.txtNoSerie = New DevExpress.XtraEditors.TextEdit()
        Me.dkUsuario = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.txtUsuarioSap = New DevExpress.XtraEditors.TextEdit()
        Me.txtClaveSap = New DevExpress.XtraEditors.TextEdit()
        IdUsuarioLabel = New System.Windows.Forms.Label()
        NombresLabel = New System.Windows.Forms.Label()
        ApellidosLabel = New System.Windows.Forms.Label()
        CedulaLabel = New System.Windows.Forms.Label()
        DireccionLabel = New System.Windows.Forms.Label()
        TelefonoLabel = New System.Windows.Forms.Label()
        EmailLabel = New System.Windows.Forms.Label()
        IdEmpresaLabel = New System.Windows.Forms.Label()
        CodigoLabel = New System.Windows.Forms.Label()
        ClaveLabel = New System.Windows.Forms.Label()
        lblConfirmarClave = New System.Windows.Forms.Label()
        Ultimo_loginLabel = New System.Windows.Forms.Label()
        ActivoLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        lblCorrelativoActual = New System.Windows.Forms.Label()
        lblCorrelativoFinal = New System.Windows.Forms.Label()
        lblBodega = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        lblCorrelativoInicial = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        lblSerie = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        lblUsuarioSap = New System.Windows.Forms.Label()
        lblClaveSap = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanDatosUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanDatosUsuario.SuspendLayout()
        CType(Me.cbxEmpresa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picFoto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.ConfirmarClaveTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Ultimo_loginDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Ultimo_loginDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClaveTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CodigoTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IdUsuarioSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NombresTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ApellidosTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CedulaTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DireccionTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TelefonoTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmailTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Dgrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl.SuspendLayout()
        Me.General.SuspendLayout()
        Me.RolesBodega.SuspendLayout()
        Me.Resoluciones_licencia.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        CType(Me.dGridResoluciones, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdResolucion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivoPR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripPR.SuspendLayout()
        CType(Me.txtCorrelativoActual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCorrelativoFinal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkResolucionLPActiva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCorrelativoInicial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.txtUsuarioSap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtClaveSap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'IdUsuarioLabel
        '
        IdUsuarioLabel.AutoSize = True
        IdUsuarioLabel.Location = New System.Drawing.Point(26, 49)
        IdUsuarioLabel.Name = "IdUsuarioLabel"
        IdUsuarioLabel.Size = New System.Drawing.Size(46, 16)
        IdUsuarioLabel.TabIndex = 0
        IdUsuarioLabel.Text = "Código"
        '
        'NombresLabel
        '
        NombresLabel.AutoSize = True
        NombresLabel.Location = New System.Drawing.Point(26, 90)
        NombresLabel.Name = "NombresLabel"
        NombresLabel.Size = New System.Drawing.Size(63, 16)
        NombresLabel.TabIndex = 4
        NombresLabel.Text = "Nombres:"
        '
        'ApellidosLabel
        '
        ApellidosLabel.AutoSize = True
        ApellidosLabel.Location = New System.Drawing.Point(353, 92)
        ApellidosLabel.Name = "ApellidosLabel"
        ApellidosLabel.Size = New System.Drawing.Size(63, 16)
        ApellidosLabel.TabIndex = 6
        ApellidosLabel.Text = "Apellidos:"
        '
        'CedulaLabel
        '
        CedulaLabel.AutoSize = True
        CedulaLabel.Location = New System.Drawing.Point(26, 132)
        CedulaLabel.Name = "CedulaLabel"
        CedulaLabel.Size = New System.Drawing.Size(51, 16)
        CedulaLabel.TabIndex = 8
        CedulaLabel.Text = "Cedula:"
        '
        'DireccionLabel
        '
        DireccionLabel.AutoSize = True
        DireccionLabel.Location = New System.Drawing.Point(352, 176)
        DireccionLabel.Name = "DireccionLabel"
        DireccionLabel.Size = New System.Drawing.Size(64, 16)
        DireccionLabel.TabIndex = 14
        DireccionLabel.Text = "Direccion:"
        '
        'TelefonoLabel
        '
        TelefonoLabel.AutoSize = True
        TelefonoLabel.Location = New System.Drawing.Point(353, 134)
        TelefonoLabel.Name = "TelefonoLabel"
        TelefonoLabel.Size = New System.Drawing.Size(62, 16)
        TelefonoLabel.TabIndex = 10
        TelefonoLabel.Text = "Teléfono:"
        '
        'EmailLabel
        '
        EmailLabel.AutoSize = True
        EmailLabel.Location = New System.Drawing.Point(26, 174)
        EmailLabel.Name = "EmailLabel"
        EmailLabel.Size = New System.Drawing.Size(51, 16)
        EmailLabel.TabIndex = 12
        EmailLabel.Text = "Correo:"
        '
        'IdEmpresaLabel
        '
        IdEmpresaLabel.AutoSize = True
        IdEmpresaLabel.Location = New System.Drawing.Point(353, 49)
        IdEmpresaLabel.Name = "IdEmpresaLabel"
        IdEmpresaLabel.Size = New System.Drawing.Size(62, 16)
        IdEmpresaLabel.TabIndex = 2
        IdEmpresaLabel.Text = "Empresa:"
        '
        'CodigoLabel
        '
        CodigoLabel.AutoSize = True
        CodigoLabel.Location = New System.Drawing.Point(19, 18)
        CodigoLabel.Name = "CodigoLabel"
        CodigoLabel.Size = New System.Drawing.Size(50, 16)
        CodigoLabel.TabIndex = 0
        CodigoLabel.Text = "Usuario"
        '
        'ClaveLabel
        '
        ClaveLabel.AutoSize = True
        ClaveLabel.Location = New System.Drawing.Point(20, 50)
        ClaveLabel.Name = "ClaveLabel"
        ClaveLabel.Size = New System.Drawing.Size(77, 16)
        ClaveLabel.TabIndex = 2
        ClaveLabel.Text = "Contraseña:"
        '
        'lblConfirmarClave
        '
        lblConfirmarClave.AutoSize = True
        lblConfirmarClave.Location = New System.Drawing.Point(20, 85)
        lblConfirmarClave.Name = "lblConfirmarClave"
        lblConfirmarClave.Size = New System.Drawing.Size(120, 16)
        lblConfirmarClave.TabIndex = 4
        lblConfirmarClave.Text = "Repetir contraseña:"
        '
        'Ultimo_loginLabel
        '
        Ultimo_loginLabel.AutoSize = True
        Ultimo_loginLabel.Location = New System.Drawing.Point(20, 117)
        Ultimo_loginLabel.Name = "Ultimo_loginLabel"
        Ultimo_loginLabel.Size = New System.Drawing.Size(94, 16)
        Ultimo_loginLabel.TabIndex = 6
        Ultimo_loginLabel.Text = "Último ingreso:"
        '
        'ActivoLabel
        '
        ActivoLabel.AutoSize = True
        ActivoLabel.Location = New System.Drawing.Point(29, 215)
        ActivoLabel.Name = "ActivoLabel"
        ActivoLabel.Size = New System.Drawing.Size(46, 16)
        ActivoLabel.TabIndex = 16
        ActivoLabel.Text = "Activo:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(38, 52)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(38, 20)
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
        Fec_modLabel.Location = New System.Drawing.Point(523, 52)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(523, 20)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'lblCorrelativoActual
        '
        lblCorrelativoActual.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCorrelativoActual.AutoSize = True
        lblCorrelativoActual.Location = New System.Drawing.Point(30, 233)
        lblCorrelativoActual.Name = "lblCorrelativoActual"
        lblCorrelativoActual.Size = New System.Drawing.Size(113, 16)
        lblCorrelativoActual.TabIndex = 14
        lblCorrelativoActual.Text = "Correlativo Actual:"
        '
        'lblCorrelativoFinal
        '
        lblCorrelativoFinal.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCorrelativoFinal.AutoSize = True
        lblCorrelativoFinal.Location = New System.Drawing.Point(30, 202)
        lblCorrelativoFinal.Name = "lblCorrelativoFinal"
        lblCorrelativoFinal.Size = New System.Drawing.Size(105, 16)
        lblCorrelativoFinal.TabIndex = 12
        lblCorrelativoFinal.Text = "Correlativo Final:"
        '
        'lblBodega
        '
        lblBodega.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblBodega.AutoSize = True
        lblBodega.ForeColor = System.Drawing.Color.Black
        lblBodega.Location = New System.Drawing.Point(30, 111)
        lblBodega.Name = "lblBodega"
        lblBodega.Size = New System.Drawing.Size(54, 16)
        lblBodega.TabIndex = 10
        lblBodega.Text = "Bodega:"
        '
        'Label1
        '
        Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(449, 78)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(46, 16)
        Label1.TabIndex = 6
        Label1.Text = "Activo:"
        '
        'lblCorrelativoInicial
        '
        lblCorrelativoInicial.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCorrelativoInicial.AutoSize = True
        lblCorrelativoInicial.Location = New System.Drawing.Point(30, 171)
        lblCorrelativoInicial.Name = "lblCorrelativoInicial"
        lblCorrelativoInicial.Size = New System.Drawing.Size(111, 16)
        lblCorrelativoInicial.TabIndex = 4
        lblCorrelativoInicial.Text = "Correlativo Inicial:"
        '
        'Label2
        '
        Label2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(30, 78)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(51, 16)
        Label2.TabIndex = 0
        Label2.Text = "Código:"
        '
        'lblSerie
        '
        lblSerie.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblSerie.AutoSize = True
        lblSerie.Location = New System.Drawing.Point(30, 142)
        lblSerie.Name = "lblSerie"
        lblSerie.Size = New System.Drawing.Size(42, 16)
        lblSerie.TabIndex = 2
        lblSerie.Text = "Serie:"
        '
        'Label3
        '
        Label3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label3.AutoSize = True
        Label3.ForeColor = System.Drawing.Color.Red
        Label3.Location = New System.Drawing.Point(449, 202)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(112, 16)
        Label3.TabIndex = 32
        Label3.Text = "*máximo 9 digitos"
        '
        'Label4
        '
        Label4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label4.AutoSize = True
        Label4.ForeColor = System.Drawing.Color.Red
        Label4.Location = New System.Drawing.Point(449, 142)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(135, 16)
        Label4.TabIndex = 33
        Label4.Text = "*máximo 3 caracteres"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.cmdImprimirCarnet})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 6
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1168, 193)
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
        'cmdImprimirCarnet
        '
        Me.cmdImprimirCarnet.Caption = "Imprimir Carnet"
        Me.cmdImprimirCarnet.Id = 5
        Me.cmdImprimirCarnet.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimirCarnet.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimirCarnet.Name = "cmdImprimirCarnet"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Usuario"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimirCarnet)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 889)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1168, 30)
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(141, 48)
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
        Me.User_agrTextEdit.Location = New System.Drawing.Point(141, 16)
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
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(625, 48)
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
        Me.User_modTextEdit.Location = New System.Drawing.Point(625, 16)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 2
        '
        'PanDatosUsuario
        '
        Me.PanDatosUsuario.Controls.Add(Me.cbxEmpresa)
        Me.PanDatosUsuario.Controls.Add(Me.lblSistema)
        Me.PanDatosUsuario.Controls.Add(Me.chkSistema)
        Me.PanDatosUsuario.Controls.Add(Me.picFoto)
        Me.PanDatosUsuario.Controls.Add(ActivoLabel)
        Me.PanDatosUsuario.Controls.Add(Me.chkActivo)
        Me.PanDatosUsuario.Controls.Add(Me.TabControl2)
        Me.PanDatosUsuario.Controls.Add(IdEmpresaLabel)
        Me.PanDatosUsuario.Controls.Add(IdUsuarioLabel)
        Me.PanDatosUsuario.Controls.Add(Me.IdUsuarioSpinEdit)
        Me.PanDatosUsuario.Controls.Add(NombresLabel)
        Me.PanDatosUsuario.Controls.Add(Me.NombresTextEdit)
        Me.PanDatosUsuario.Controls.Add(ApellidosLabel)
        Me.PanDatosUsuario.Controls.Add(Me.ApellidosTextEdit)
        Me.PanDatosUsuario.Controls.Add(CedulaLabel)
        Me.PanDatosUsuario.Controls.Add(Me.CedulaTextEdit)
        Me.PanDatosUsuario.Controls.Add(DireccionLabel)
        Me.PanDatosUsuario.Controls.Add(Me.DireccionTextEdit)
        Me.PanDatosUsuario.Controls.Add(TelefonoLabel)
        Me.PanDatosUsuario.Controls.Add(Me.TelefonoTextEdit)
        Me.PanDatosUsuario.Controls.Add(EmailLabel)
        Me.PanDatosUsuario.Controls.Add(Me.EmailTextEdit)
        Me.PanDatosUsuario.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanDatosUsuario.Location = New System.Drawing.Point(0, 0)
        Me.PanDatosUsuario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanDatosUsuario.Name = "PanDatosUsuario"
        Me.PanDatosUsuario.Size = New System.Drawing.Size(1166, 640)
        Me.PanDatosUsuario.TabIndex = 0
        Me.PanDatosUsuario.Text = "Datos Usuario"
        '
        'cbxEmpresa
        '
        Me.cbxEmpresa.Location = New System.Drawing.Point(422, 43)
        Me.cbxEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbxEmpresa.MenuManager = Me.RibbonControl
        Me.cbxEmpresa.Name = "cbxEmpresa"
        Me.cbxEmpresa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbxEmpresa.Properties.NullText = ""
        Me.cbxEmpresa.Size = New System.Drawing.Size(258, 22)
        Me.cbxEmpresa.TabIndex = 22
        '
        'lblSistema
        '
        Me.lblSistema.AutoSize = True
        Me.lblSistema.Location = New System.Drawing.Point(231, 218)
        Me.lblSistema.Name = "lblSistema"
        Me.lblSistema.Size = New System.Drawing.Size(58, 16)
        Me.lblSistema.TabIndex = 21
        Me.lblSistema.Text = "Sistema:"
        '
        'chkSistema
        '
        Me.chkSistema.AutoSize = True
        Me.chkSistema.Location = New System.Drawing.Point(294, 218)
        Me.chkSistema.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkSistema.Name = "chkSistema"
        Me.chkSistema.Size = New System.Drawing.Size(18, 17)
        Me.chkSistema.TabIndex = 20
        Me.chkSistema.UseVisualStyleBackColor = True
        '
        'picFoto
        '
        Me.picFoto.Cursor = System.Windows.Forms.Cursors.Default
        Me.picFoto.Location = New System.Drawing.Point(729, 39)
        Me.picFoto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picFoto.MenuManager = Me.RibbonControl
        Me.picFoto.Name = "picFoto"
        Me.picFoto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat
        Me.picFoto.Properties.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray
        Me.picFoto.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Always
        Me.picFoto.Size = New System.Drawing.Size(197, 207)
        Me.picFoto.TabIndex = 18
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(86, 206)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(31, 24)
        Me.chkActivo.TabIndex = 17
        '
        'TabControl2
        '
        Me.TabControl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl2.Controls.Add(Me.TabPage3)
        Me.TabControl2.Location = New System.Drawing.Point(2, 258)
        Me.TabControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(1167, 375)
        Me.TabControl2.TabIndex = 19
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.TabPage3.Controls.Add(Me.lblReptClaAut)
        Me.TabPage3.Controls.Add(Me.txtReptClaveAuto)
        Me.TabPage3.Controls.Add(Me.txtClaveAutoriza)
        Me.TabPage3.Controls.Add(Me.lblClaveAuto)
        Me.TabPage3.Controls.Add(Me.ConfirmarClaveTextEdit)
        Me.TabPage3.Controls.Add(Ultimo_loginLabel)
        Me.TabPage3.Controls.Add(Me.Ultimo_loginDateEdit)
        Me.TabPage3.Controls.Add(lblConfirmarClave)
        Me.TabPage3.Controls.Add(ClaveLabel)
        Me.TabPage3.Controls.Add(Me.ClaveTextEdit)
        Me.TabPage3.Controls.Add(CodigoLabel)
        Me.TabPage3.Controls.Add(Me.CodigoTextEdit)
        Me.TabPage3.Location = New System.Drawing.Point(4, 25)
        Me.TabPage3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabPage3.Size = New System.Drawing.Size(1159, 346)
        Me.TabPage3.TabIndex = 0
        Me.TabPage3.Text = "Ingreso a TOM IMS"
        '
        'lblReptClaAut
        '
        Me.lblReptClaAut.AutoSize = True
        Me.lblReptClaAut.Location = New System.Drawing.Point(20, 188)
        Me.lblReptClaAut.Name = "lblReptClaAut"
        Me.lblReptClaAut.Size = New System.Drawing.Size(82, 48)
        Me.lblReptClaAut.TabIndex = 26
        Me.lblReptClaAut.Text = "Repetir " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Clave de " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Autorización:"
        Me.lblReptClaAut.Visible = False
        '
        'txtReptClaveAuto
        '
        Me.txtReptClaveAuto.Location = New System.Drawing.Point(147, 210)
        Me.txtReptClaveAuto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtReptClaveAuto.Name = "txtReptClaveAuto"
        Me.txtReptClaveAuto.PasswordChar = Global.Microsoft.VisualBasic.ChrW(35)
        Me.txtReptClaveAuto.Size = New System.Drawing.Size(249, 23)
        Me.txtReptClaveAuto.TabIndex = 25
        Me.txtReptClaveAuto.Visible = False
        '
        'txtClaveAutoriza
        '
        Me.txtClaveAutoriza.Location = New System.Drawing.Point(147, 149)
        Me.txtClaveAutoriza.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtClaveAutoriza.Name = "txtClaveAutoriza"
        Me.txtClaveAutoriza.PasswordChar = Global.Microsoft.VisualBasic.ChrW(35)
        Me.txtClaveAutoriza.Size = New System.Drawing.Size(249, 23)
        Me.txtClaveAutoriza.TabIndex = 24
        Me.txtClaveAutoriza.Visible = False
        '
        'lblClaveAuto
        '
        Me.lblClaveAuto.AutoSize = True
        Me.lblClaveAuto.Location = New System.Drawing.Point(19, 153)
        Me.lblClaveAuto.Name = "lblClaveAuto"
        Me.lblClaveAuto.Size = New System.Drawing.Size(117, 16)
        Me.lblClaveAuto.TabIndex = 23
        Me.lblClaveAuto.Text = "Clave Autorización:"
        Me.lblClaveAuto.Visible = False
        '
        'ConfirmarClaveTextEdit
        '
        Me.ConfirmarClaveTextEdit.Location = New System.Drawing.Point(147, 81)
        Me.ConfirmarClaveTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ConfirmarClaveTextEdit.MenuManager = Me.RibbonControl
        Me.ConfirmarClaveTextEdit.Name = "ConfirmarClaveTextEdit"
        Me.ConfirmarClaveTextEdit.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(35)
        Me.ConfirmarClaveTextEdit.Size = New System.Drawing.Size(250, 22)
        Me.ConfirmarClaveTextEdit.TabIndex = 5
        '
        'Ultimo_loginDateEdit
        '
        Me.Ultimo_loginDateEdit.EditValue = Nothing
        Me.Ultimo_loginDateEdit.Enabled = False
        Me.Ultimo_loginDateEdit.Location = New System.Drawing.Point(147, 113)
        Me.Ultimo_loginDateEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Ultimo_loginDateEdit.MenuManager = Me.RibbonControl
        Me.Ultimo_loginDateEdit.Name = "Ultimo_loginDateEdit"
        Me.Ultimo_loginDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Ultimo_loginDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Ultimo_loginDateEdit.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.Ultimo_loginDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.Ultimo_loginDateEdit.Size = New System.Drawing.Size(250, 22)
        Me.Ultimo_loginDateEdit.TabIndex = 7
        '
        'ClaveTextEdit
        '
        Me.ClaveTextEdit.Location = New System.Drawing.Point(147, 47)
        Me.ClaveTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ClaveTextEdit.MenuManager = Me.RibbonControl
        Me.ClaveTextEdit.Name = "ClaveTextEdit"
        Me.ClaveTextEdit.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(35)
        Me.ClaveTextEdit.Size = New System.Drawing.Size(250, 22)
        Me.ClaveTextEdit.TabIndex = 3
        '
        'CodigoTextEdit
        '
        Me.CodigoTextEdit.Location = New System.Drawing.Point(147, 15)
        Me.CodigoTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CodigoTextEdit.MenuManager = Me.RibbonControl
        Me.CodigoTextEdit.Name = "CodigoTextEdit"
        Me.CodigoTextEdit.Size = New System.Drawing.Size(250, 22)
        Me.CodigoTextEdit.TabIndex = 1
        '
        'IdUsuarioSpinEdit
        '
        Me.IdUsuarioSpinEdit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.IdUsuarioSpinEdit.Enabled = False
        Me.IdUsuarioSpinEdit.Location = New System.Drawing.Point(90, 43)
        Me.IdUsuarioSpinEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IdUsuarioSpinEdit.MenuManager = Me.RibbonControl
        Me.IdUsuarioSpinEdit.Name = "IdUsuarioSpinEdit"
        Me.IdUsuarioSpinEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IdUsuarioSpinEdit.Size = New System.Drawing.Size(247, 24)
        Me.IdUsuarioSpinEdit.TabIndex = 1
        '
        'NombresTextEdit
        '
        Me.NombresTextEdit.Location = New System.Drawing.Point(90, 84)
        Me.NombresTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.NombresTextEdit.MenuManager = Me.RibbonControl
        Me.NombresTextEdit.Name = "NombresTextEdit"
        Me.NombresTextEdit.Size = New System.Drawing.Size(247, 22)
        Me.NombresTextEdit.TabIndex = 5
        '
        'ApellidosTextEdit
        '
        Me.ApellidosTextEdit.Location = New System.Drawing.Point(422, 84)
        Me.ApellidosTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ApellidosTextEdit.MenuManager = Me.RibbonControl
        Me.ApellidosTextEdit.Name = "ApellidosTextEdit"
        Me.ApellidosTextEdit.Size = New System.Drawing.Size(258, 22)
        Me.ApellidosTextEdit.TabIndex = 7
        '
        'CedulaTextEdit
        '
        Me.CedulaTextEdit.Location = New System.Drawing.Point(90, 126)
        Me.CedulaTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CedulaTextEdit.MenuManager = Me.RibbonControl
        Me.CedulaTextEdit.Name = "CedulaTextEdit"
        Me.CedulaTextEdit.Size = New System.Drawing.Size(247, 22)
        Me.CedulaTextEdit.TabIndex = 9
        '
        'DireccionTextEdit
        '
        Me.DireccionTextEdit.Location = New System.Drawing.Point(422, 167)
        Me.DireccionTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DireccionTextEdit.MenuManager = Me.RibbonControl
        Me.DireccionTextEdit.Name = "DireccionTextEdit"
        Me.DireccionTextEdit.Size = New System.Drawing.Size(258, 22)
        Me.DireccionTextEdit.TabIndex = 15
        '
        'TelefonoTextEdit
        '
        Me.TelefonoTextEdit.Location = New System.Drawing.Point(422, 126)
        Me.TelefonoTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TelefonoTextEdit.MenuManager = Me.RibbonControl
        Me.TelefonoTextEdit.Name = "TelefonoTextEdit"
        Me.TelefonoTextEdit.Size = New System.Drawing.Size(258, 22)
        Me.TelefonoTextEdit.TabIndex = 11
        '
        'EmailTextEdit
        '
        Me.EmailTextEdit.Location = New System.Drawing.Point(90, 167)
        Me.EmailTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.EmailTextEdit.MenuManager = Me.RibbonControl
        Me.EmailTextEdit.Name = "EmailTextEdit"
        Me.EmailTextEdit.Properties.Mask.EditMask = "[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"
        Me.EmailTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.EmailTextEdit.Size = New System.Drawing.Size(247, 22)
        Me.EmailTextEdit.TabIndex = 13
        '
        'Dgrid
        '
        Me.Dgrid.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.Dgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dgrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Dgrid.Location = New System.Drawing.Point(0, 0)
        Me.Dgrid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Dgrid.Name = "Dgrid"
        Me.Dgrid.RowHeadersWidth = 51
        Me.Dgrid.Size = New System.Drawing.Size(1166, 640)
        Me.Dgrid.TabIndex = 0
        '
        'XtraTabControl
        '
        Me.XtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl.Location = New System.Drawing.Point(0, 193)
        Me.XtraTabControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.XtraTabControl.Name = "XtraTabControl"
        Me.XtraTabControl.SelectedTabPage = Me.General
        Me.XtraTabControl.Size = New System.Drawing.Size(1168, 670)
        Me.XtraTabControl.TabIndex = 4
        Me.XtraTabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.General, Me.RolesBodega, Me.Resoluciones_licencia})
        '
        'General
        '
        Me.General.Controls.Add(Me.PanDatosUsuario)
        Me.General.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.General.Name = "General"
        Me.General.Size = New System.Drawing.Size(1166, 640)
        Me.General.Text = "General"
        '
        'RolesBodega
        '
        Me.RolesBodega.Controls.Add(Me.Dgrid)
        Me.RolesBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RolesBodega.Name = "RolesBodega"
        Me.RolesBodega.Size = New System.Drawing.Size(1166, 640)
        Me.RolesBodega.Text = "Roles Bodega"
        '
        'Resoluciones_licencia
        '
        Me.Resoluciones_licencia.Controls.Add(Me.GroupControl2)
        Me.Resoluciones_licencia.Name = "Resoluciones_licencia"
        Me.Resoluciones_licencia.Size = New System.Drawing.Size(1166, 640)
        Me.Resoluciones_licencia.Text = "Resolución licencias"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Label4)
        Me.GroupControl2.Controls.Add(Label3)
        Me.GroupControl2.Controls.Add(Me.GroupControl5)
        Me.GroupControl2.Controls.Add(Me.ToolStripPR)
        Me.GroupControl2.Controls.Add(lblCorrelativoActual)
        Me.GroupControl2.Controls.Add(Me.txtCorrelativoActual)
        Me.GroupControl2.Controls.Add(lblCorrelativoFinal)
        Me.GroupControl2.Controls.Add(Me.txtCorrelativoFinal)
        Me.GroupControl2.Controls.Add(lblBodega)
        Me.GroupControl2.Controls.Add(Me.cmbBodega)
        Me.GroupControl2.Controls.Add(Me.lblIdResolucionLP)
        Me.GroupControl2.Controls.Add(Label1)
        Me.GroupControl2.Controls.Add(Me.chkResolucionLPActiva)
        Me.GroupControl2.Controls.Add(lblCorrelativoInicial)
        Me.GroupControl2.Controls.Add(Me.txtCorrelativoInicial)
        Me.GroupControl2.Controls.Add(Label2)
        Me.GroupControl2.Controls.Add(lblSerie)
        Me.GroupControl2.Controls.Add(Me.txtNoSerie)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1166, 640)
        Me.GroupControl2.TabIndex = 2
        Me.GroupControl2.Text = "Datos de Resolución"
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.dGridResoluciones)
        Me.GroupControl5.Controls.Add(Me.chkActivoPR)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl5.Location = New System.Drawing.Point(2, 262)
        Me.GroupControl5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(1162, 376)
        Me.GroupControl5.TabIndex = 31
        Me.GroupControl5.Text = "Listado de resoluciones"
        '
        'dGridResoluciones
        '
        Me.dGridResoluciones.Cursor = System.Windows.Forms.Cursors.Default
        Me.dGridResoluciones.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dGridResoluciones.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.RelationName = "Level1"
        Me.dGridResoluciones.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.dGridResoluciones.Location = New System.Drawing.Point(2, 52)
        Me.dGridResoluciones.MainView = Me.GrdResolucion
        Me.dGridResoluciones.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dGridResoluciones.MenuManager = Me.RibbonControl
        Me.dGridResoluciones.Name = "dGridResoluciones"
        Me.dGridResoluciones.Size = New System.Drawing.Size(1158, 322)
        Me.dGridResoluciones.TabIndex = 0
        Me.dGridResoluciones.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GrdResolucion, Me.GridView6})
        '
        'GrdResolucion
        '
        Me.GrdResolucion.DetailHeight = 431
        Me.GrdResolucion.GridControl = Me.dGridResoluciones
        Me.GrdResolucion.Name = "GrdResolucion"
        Me.GrdResolucion.OptionsBehavior.Editable = False
        Me.GrdResolucion.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GrdResolucion.OptionsView.ShowGroupPanel = False
        '
        'GridView6
        '
        Me.GridView6.DetailHeight = 431
        Me.GridView6.GridControl = Me.dGridResoluciones
        Me.GridView6.Name = "GridView6"
        '
        'chkActivoPR
        '
        Me.chkActivoPR.Dock = System.Windows.Forms.DockStyle.Top
        Me.chkActivoPR.EditValue = True
        Me.chkActivoPR.Location = New System.Drawing.Point(2, 28)
        Me.chkActivoPR.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivoPR.MenuManager = Me.RibbonControl
        Me.chkActivoPR.Name = "chkActivoPR"
        Me.chkActivoPR.Properties.Caption = "Resoluciones Activas"
        Me.chkActivoPR.Size = New System.Drawing.Size(1158, 24)
        Me.chkActivoPR.TabIndex = 2
        '
        'ToolStripPR
        '
        Me.ToolStripPR.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStripPR.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdNewPR, Me.cmdSavePR, Me.cmdDesactivarResolucion})
        Me.ToolStripPR.Location = New System.Drawing.Point(2, 28)
        Me.ToolStripPR.Name = "ToolStripPR"
        Me.ToolStripPR.Size = New System.Drawing.Size(1162, 27)
        Me.ToolStripPR.TabIndex = 16
        Me.ToolStripPR.Text = "ToolStrip2"
        '
        'cmdNewPR
        '
        Me.cmdNewPR.Image = CType(resources.GetObject("cmdNewPR.Image"), System.Drawing.Image)
        Me.cmdNewPR.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdNewPR.Name = "cmdNewPR"
        Me.cmdNewPR.Size = New System.Drawing.Size(76, 24)
        Me.cmdNewPR.Text = "Nuevo"
        '
        'cmdSavePR
        '
        Me.cmdSavePR.Image = CType(resources.GetObject("cmdSavePR.Image"), System.Drawing.Image)
        Me.cmdSavePR.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSavePR.Name = "cmdSavePR"
        Me.cmdSavePR.Size = New System.Drawing.Size(86, 24)
        Me.cmdSavePR.Text = "Guardar"
        '
        'cmdDesactivarResolucion
        '
        Me.cmdDesactivarResolucion.Image = CType(resources.GetObject("cmdDesactivarResolucion.Image"), System.Drawing.Image)
        Me.cmdDesactivarResolucion.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDesactivarResolucion.Name = "cmdDesactivarResolucion"
        Me.cmdDesactivarResolucion.Size = New System.Drawing.Size(102, 24)
        Me.cmdDesactivarResolucion.Text = "Desactivar"
        '
        'txtCorrelativoActual
        '
        Me.txtCorrelativoActual.Location = New System.Drawing.Point(177, 231)
        Me.txtCorrelativoActual.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCorrelativoActual.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.txtCorrelativoActual.Name = "txtCorrelativoActual"
        Me.txtCorrelativoActual.ReadOnly = True
        Me.txtCorrelativoActual.Size = New System.Drawing.Size(241, 23)
        Me.txtCorrelativoActual.TabIndex = 15
        '
        'txtCorrelativoFinal
        '
        Me.txtCorrelativoFinal.Location = New System.Drawing.Point(177, 200)
        Me.txtCorrelativoFinal.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCorrelativoFinal.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.txtCorrelativoFinal.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtCorrelativoFinal.Name = "txtCorrelativoFinal"
        Me.txtCorrelativoFinal.Size = New System.Drawing.Size(241, 23)
        Me.txtCorrelativoFinal.TabIndex = 13
        Me.txtCorrelativoFinal.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(177, 110)
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(241, 22)
        Me.cmbBodega.TabIndex = 11
        '
        'lblIdResolucionLP
        '
        Me.lblIdResolucionLP.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar
        Me.lblIdResolucionLP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIdResolucionLP.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIdResolucionLP.Location = New System.Drawing.Point(177, 76)
        Me.lblIdResolucionLP.Name = "lblIdResolucionLP"
        Me.lblIdResolucionLP.Size = New System.Drawing.Size(241, 26)
        Me.lblIdResolucionLP.TabIndex = 1
        '
        'chkResolucionLPActiva
        '
        Me.chkResolucionLPActiva.EditValue = True
        Me.chkResolucionLPActiva.Location = New System.Drawing.Point(522, 74)
        Me.chkResolucionLPActiva.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkResolucionLPActiva.MenuManager = Me.RibbonControl
        Me.chkResolucionLPActiva.Name = "chkResolucionLPActiva"
        Me.chkResolucionLPActiva.Properties.Caption = ""
        Me.chkResolucionLPActiva.Size = New System.Drawing.Size(41, 24)
        Me.chkResolucionLPActiva.TabIndex = 7
        '
        'txtCorrelativoInicial
        '
        Me.txtCorrelativoInicial.Location = New System.Drawing.Point(177, 169)
        Me.txtCorrelativoInicial.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCorrelativoInicial.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.txtCorrelativoInicial.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtCorrelativoInicial.Name = "txtCorrelativoInicial"
        Me.txtCorrelativoInicial.Size = New System.Drawing.Size(241, 23)
        Me.txtCorrelativoInicial.TabIndex = 5
        Me.txtCorrelativoInicial.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtNoSerie
        '
        Me.txtNoSerie.Location = New System.Drawing.Point(177, 139)
        Me.txtNoSerie.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNoSerie.MenuManager = Me.RibbonControl
        Me.txtNoSerie.Name = "txtNoSerie"
        Me.txtNoSerie.Properties.MaxLength = 3
        Me.txtNoSerie.Size = New System.Drawing.Size(241, 22)
        Me.txtNoSerie.TabIndex = 3
        '
        'dkUsuario
        '
        Me.dkUsuario.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkUsuario.Form = Me
        Me.dkUsuario.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.White
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 863)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1168, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("d1a9e1f1-6268-49f5-9a2b-af914ff13c1a")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 638)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 101)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(958, 124)
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
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 32)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(951, 89)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'Label3
        '
        Label3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label3.AutoSize = True
        Label3.ForeColor = System.Drawing.Color.Red
        Label3.Location = New System.Drawing.Point(449, 202)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(112, 16)
        Label3.TabIndex = 32
        Label3.Text = "*máximo 9 digitos"
        '
        'Label4
        '
        Label4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label4.AutoSize = True
        Label4.ForeColor = System.Drawing.Color.Red
        Label4.Location = New System.Drawing.Point(449, 142)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(135, 16)
        Label4.TabIndex = 33
        Label4.Text = "*máximo 3 caracteres"
        '
        'frmUsu
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1168, 919)
        Me.Controls.Add(Me.XtraTabControl)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmUsu"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Usuario"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanDatosUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanDatosUsuario.ResumeLayout(False)
        Me.PanDatosUsuario.PerformLayout()
        CType(Me.cbxEmpresa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picFoto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.ConfirmarClaveTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Ultimo_loginDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Ultimo_loginDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClaveTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CodigoTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IdUsuarioSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NombresTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ApellidosTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CedulaTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DireccionTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TelefonoTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmailTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Dgrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl.ResumeLayout(False)
        Me.General.ResumeLayout(False)
        Me.RolesBodega.ResumeLayout(False)
        Me.Resoluciones_licencia.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        CType(Me.dGridResoluciones, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrdResolucion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivoPR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripPR.ResumeLayout(False)
        Me.ToolStripPR.PerformLayout()
        CType(Me.txtCorrelativoActual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCorrelativoFinal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkResolucionLPActiva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCorrelativoInicial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.GroupControl3.PerformLayout()
        CType(Me.txtUsuarioSap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtClaveSap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents cmdImprimirCarnet As DevExpress.XtraBars.BarButtonItem
    Private WithEvents PanDatosUsuario As DevExpress.XtraEditors.GroupControl
    Friend WithEvents IdUsuarioSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents NombresTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ApellidosTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents CedulaTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents DireccionTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TelefonoTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents EmailTextEdit As DevExpress.XtraEditors.TextEdit
    Private WithEvents Dgrid As System.Windows.Forms.DataGridView
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Ultimo_loginDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents ClaveTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents CodigoTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ConfirmarClaveTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents picFoto As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents XtraTabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents General As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents RolesBodega As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dkUsuario As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents lblSistema As Label
    Friend WithEvents chkSistema As CheckBox
    Friend WithEvents cbxEmpresa As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblClaveAuto As Label
    Friend WithEvents txtClaveAutoriza As TextBox
    Friend WithEvents lblReptClaAut As Label
    Friend WithEvents txtReptClaveAuto As TextBox
    Friend WithEvents Resoluciones_licencia As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ToolStripPR As ToolStrip
    Friend WithEvents cmdNewPR As ToolStripButton
    Friend WithEvents cmdSavePR As ToolStripButton
    Friend WithEvents cmdDesactivarResolucion As ToolStripButton
    Friend WithEvents txtCorrelativoActual As NumericUpDown
    Friend WithEvents txtCorrelativoFinal As NumericUpDown
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblIdResolucionLP As Label
    Friend WithEvents chkResolucionLPActiva As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtCorrelativoInicial As NumericUpDown
    Friend WithEvents txtNoSerie As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dGridResoluciones As DevExpress.XtraGrid.GridControl
    Friend WithEvents GrdResolucion As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkActivoPR As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtUsuarioSap As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtClaveSap As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
End Class
