<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPropietarioReglaRecepcion
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
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim Label As System.Windows.Forms.Label
        Dim Label54 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPropietarioReglaRecepcion))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl8 = New DevExpress.XtraEditors.GroupControl()
        Me.lblNombrePropietario = New System.Windows.Forms.Label()
        Me.lnkMensaje = New System.Windows.Forms.LinkLabel()
        Me.txtNombreMensaje = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdMensaje = New DevExpress.XtraEditors.TextEdit()
        Me.lnkRegla = New System.Windows.Forms.LinkLabel()
        Me.txtIdRegla = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreRegla = New DevExpress.XtraEditors.TextEdit()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupControl9 = New DevExpress.XtraEditors.GroupControl()
        Me.GridDestinatarios = New DevExpress.XtraGrid.GridControl()
        Me.ViewDestinatario = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.chkActivoD = New DevExpress.XtraEditors.CheckEdit()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.cmdAgregar = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdDesactivar = New System.Windows.Forms.ToolStripMenuItem()
        Me.dkPropietarioReglaRec = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        Label = New System.Windows.Forms.Label()
        Label54 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.Fec_agrDateEdit.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.User_agrTextEdit.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.Fec_modDateEdit.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.User_modTextEdit.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GroupControl8,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupControl8.SuspendLayout
        CType(Me.txtNombreMensaje.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtIdMensaje.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtIdRegla.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtNombreRegla.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.chkActivo.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GroupControl9,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupControl9.SuspendLayout
        CType(Me.GridDestinatarios,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ViewDestinatario,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.chkActivoD.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        Me.MenuStrip1.SuspendLayout
        CType(Me.dkPropietarioReglaRec,System.ComponentModel.ISupportInitialize).BeginInit
        Me.hideContainerBottom.SuspendLayout
        Me.DockPanel1.SuspendLayout
        Me.DockPanel1_Container.SuspendLayout
        Me.SuspendLayout
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(135, 20)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(63, 17)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "user agr:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(135, 52)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(55, 17)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "fec agr:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(549, 20)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(71, 17)
        User_modLabel.TabIndex = 2
        User_modLabel.Text = "user mod:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(549, 52)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(63, 17)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "fec mod:"
        '
        'Label
        '
        Label.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label.AutoSize = True
        Label.Location = New System.Drawing.Point(50, 166)
        Label.Name = "Label"
        Label.Size = New System.Drawing.Size(46, 17)
        Label.TabIndex = 9
        Label.Text = "Activo"
        '
        'Label54
        '
        Label54.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label54.AutoSize = True
        Label54.BackColor = System.Drawing.Color.White
        Label54.Location = New System.Drawing.Point(995, 32)
        Label54.Name = "Label54"
        Label54.Size = New System.Drawing.Size(57, 17)
        Label54.TabIndex = 1
        Label54.Text = "Activos:"
        '
        'Label12
        '
        Label12.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(50, 73)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(74, 17)
        Label12.TabIndex = 1
        Label12.Text = "Correlativo"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 5
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1098, 193)
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 812)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1098, 30)
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(265, 48)
        Me.Fec_agrDateEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_agrDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(206, 22)
        Me.Fec_agrDateEdit.TabIndex = 5
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(265, 16)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(206, 22)
        Me.User_agrTextEdit.TabIndex = 1
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(679, 48)
        Me.Fec_modDateEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_modDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(206, 22)
        Me.Fec_modDateEdit.TabIndex = 7
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(679, 16)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(206, 22)
        Me.User_modTextEdit.TabIndex = 3
        '
        'GroupControl8
        '
        Me.GroupControl8.Controls.Add(Me.lblNombrePropietario)
        Me.GroupControl8.Controls.Add(Me.lnkMensaje)
        Me.GroupControl8.Controls.Add(Me.txtNombreMensaje)
        Me.GroupControl8.Controls.Add(Me.txtIdMensaje)
        Me.GroupControl8.Controls.Add(Me.lnkRegla)
        Me.GroupControl8.Controls.Add(Me.txtIdRegla)
        Me.GroupControl8.Controls.Add(Me.txtNombreRegla)
        Me.GroupControl8.Controls.Add(Me.lblCodigo)
        Me.GroupControl8.Controls.Add(Label12)
        Me.GroupControl8.Controls.Add(Label)
        Me.GroupControl8.Controls.Add(Me.chkActivo)
        Me.GroupControl8.Controls.Add(Me.GroupControl9)
        Me.GroupControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl8.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl8.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl8.Name = "GroupControl8"
        Me.GroupControl8.Size = New System.Drawing.Size(1098, 593)
        Me.GroupControl8.TabIndex = 0
        Me.GroupControl8.Text = "Datos Regla"
        '
        'lblNombrePropietario
        '
        Me.lblNombrePropietario.AutoSize = True
        Me.lblNombrePropietario.Font = New System.Drawing.Font("Tahoma", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombrePropietario.Location = New System.Drawing.Point(50, 36)
        Me.lblNombrePropietario.Name = "lblNombrePropietario"
        Me.lblNombrePropietario.Size = New System.Drawing.Size(0, 21)
        Me.lblNombrePropietario.TabIndex = 0
        '
        'lnkMensaje
        '
        Me.lnkMensaje.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkMensaje.AutoSize = True
        Me.lnkMensaje.Location = New System.Drawing.Point(50, 134)
        Me.lnkMensaje.Name = "lnkMensaje"
        Me.lnkMensaje.Size = New System.Drawing.Size(57, 17)
        Me.lnkMensaje.TabIndex = 6
        Me.lnkMensaje.TabStop = True
        Me.lnkMensaje.Text = "Mensaje"
        '
        'txtNombreMensaje
        '
        Me.txtNombreMensaje.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNombreMensaje.Location = New System.Drawing.Point(362, 130)
        Me.txtNombreMensaje.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreMensaje.MenuManager = Me.RibbonControl
        Me.txtNombreMensaje.Name = "txtNombreMensaje"
        Me.txtNombreMensaje.Properties.ReadOnly = True
        Me.txtNombreMensaje.Size = New System.Drawing.Size(689, 22)
        Me.txtNombreMensaje.TabIndex = 8
        '
        'txtIdMensaje
        '
        Me.txtIdMensaje.Location = New System.Drawing.Point(220, 130)
        Me.txtIdMensaje.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdMensaje.MenuManager = Me.RibbonControl
        Me.txtIdMensaje.Name = "txtIdMensaje"
        Me.txtIdMensaje.Properties.Mask.EditMask = "n0"
        Me.txtIdMensaje.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtIdMensaje.Size = New System.Drawing.Size(134, 22)
        Me.txtIdMensaje.TabIndex = 7
        '
        'lnkRegla
        '
        Me.lnkRegla.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkRegla.AutoSize = True
        Me.lnkRegla.Location = New System.Drawing.Point(50, 102)
        Me.lnkRegla.Name = "lnkRegla"
        Me.lnkRegla.Size = New System.Drawing.Size(41, 17)
        Me.lnkRegla.TabIndex = 3
        Me.lnkRegla.TabStop = True
        Me.lnkRegla.Text = "Regla"
        '
        'txtIdRegla
        '
        Me.txtIdRegla.Location = New System.Drawing.Point(220, 98)
        Me.txtIdRegla.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdRegla.MenuManager = Me.RibbonControl
        Me.txtIdRegla.Name = "txtIdRegla"
        Me.txtIdRegla.Properties.Mask.EditMask = "n0"
        Me.txtIdRegla.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtIdRegla.Size = New System.Drawing.Size(134, 22)
        Me.txtIdRegla.TabIndex = 4
        '
        'txtNombreRegla
        '
        Me.txtNombreRegla.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNombreRegla.Location = New System.Drawing.Point(362, 98)
        Me.txtNombreRegla.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreRegla.MenuManager = Me.RibbonControl
        Me.txtNombreRegla.Name = "txtNombreRegla"
        Me.txtNombreRegla.Properties.ReadOnly = True
        Me.txtNombreRegla.Size = New System.Drawing.Size(689, 22)
        Me.txtNombreRegla.TabIndex = 5
        '
        'lblCodigo
        '
        Me.lblCodigo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodigo.Location = New System.Drawing.Point(217, 73)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(14, 17)
        Me.lblCodigo.TabIndex = 2
        Me.lblCodigo.Text = "-"
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(220, 162)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(117, 24)
        Me.chkActivo.TabIndex = 10
        '
        'GroupControl9
        '
        Me.GroupControl9.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl9.Controls.Add(Label54)
        Me.GroupControl9.Controls.Add(Me.GridDestinatarios)
        Me.GroupControl9.Controls.Add(Me.chkActivoD)
        Me.GroupControl9.Controls.Add(Me.MenuStrip1)
        Me.GroupControl9.Location = New System.Drawing.Point(2, 204)
        Me.GroupControl9.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl9.Name = "GroupControl9"
        Me.GroupControl9.Size = New System.Drawing.Size(1093, 531)
        Me.GroupControl9.TabIndex = 11
        Me.GroupControl9.Text = "Enviar Mensaje a:"
        '
        'GridDestinatarios
        '
        Me.GridDestinatarios.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridDestinatarios.Cursor = System.Windows.Forms.Cursors.Default
        Me.GridDestinatarios.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.RelationName = "Level1"
        Me.GridDestinatarios.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.GridDestinatarios.Location = New System.Drawing.Point(2, 59)
        Me.GridDestinatarios.MainView = Me.ViewDestinatario
        Me.GridDestinatarios.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GridDestinatarios.MenuManager = Me.RibbonControl
        Me.GridDestinatarios.Name = "GridDestinatarios"
        Me.GridDestinatarios.Size = New System.Drawing.Size(1088, 324)
        Me.GridDestinatarios.TabIndex = 3
        Me.GridDestinatarios.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewDestinatario})
        '
        'ViewDestinatario
        '
        Me.ViewDestinatario.DetailHeight = 431
        Me.ViewDestinatario.GridControl = Me.GridDestinatarios
        Me.ViewDestinatario.Name = "ViewDestinatario"
        Me.ViewDestinatario.OptionsBehavior.Editable = False
        Me.ViewDestinatario.OptionsFind.AlwaysVisible = True
        Me.ViewDestinatario.OptionsView.ShowGroupPanel = False
        '
        'chkActivoD
        '
        Me.chkActivoD.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkActivoD.EditValue = True
        Me.chkActivoD.Location = New System.Drawing.Point(1056, 28)
        Me.chkActivoD.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivoD.MenuManager = Me.RibbonControl
        Me.chkActivoD.Name = "chkActivoD"
        Me.chkActivoD.Properties.Caption = ""
        Me.chkActivoD.Size = New System.Drawing.Size(21, 24)
        Me.chkActivoD.TabIndex = 2
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdAgregar, Me.cmdDesactivar})
        Me.MenuStrip1.Location = New System.Drawing.Point(2, 28)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(7, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(1089, 28)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'cmdAgregar
        '
        Me.cmdAgregar.Checked = True
        Me.cmdAgregar.CheckState = System.Windows.Forms.CheckState.Checked
        'Me.cmdAgregar.Image = Global.TOMWMS.My.Resources.Resources.add
        Me.cmdAgregar.Name = "cmdAgregar"
        Me.cmdAgregar.Size = New System.Drawing.Size(182, 24)
        Me.cmdAgregar.Text = "Agregar Destinatario"
        '
        'cmdDesactivar
        '
        Me.cmdDesactivar.Image = Global.TOMWMS.My.Resources.Resources.incorrect_294245_640
        Me.cmdDesactivar.Name = "cmdDesactivar"
        Me.cmdDesactivar.Size = New System.Drawing.Size(197, 24)
        Me.cmdDesactivar.Text = "Desactivar Destinatario"
        '
        'dkPropietarioReglaRec
        '
        Me.dkPropietarioReglaRec.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkPropietarioReglaRec.Form = Me
        Me.dkPropietarioReglaRec.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 786)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1098, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("909968c9-3d3c-44df-a352-b3a770fb9428")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 104)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1098, 128)
        Me.DockPanel1.Text = "Bitácora"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(User_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_agrTextEdit)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_agrDateEdit)
        Me.DockPanel1_Container.Controls.Add(Me.User_modTextEdit)
        Me.DockPanel1_Container.Controls.Add(Fec_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Fec_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_modDateEdit)
        Me.DockPanel1_Container.Controls.Add(User_modLabel)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 31)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1091, 94)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'frmPropietarioReglaRecepcion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1098, 842)
        Me.Controls.Add(Me.GroupControl8)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmPropietarioReglaRecepcion"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Propietario Reglas Recepción"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_agrDateEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.User_agrTextEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_modDateEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.User_modTextEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GroupControl8,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupControl8.ResumeLayout(false)
        Me.GroupControl8.PerformLayout
        CType(Me.txtNombreMensaje.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtIdMensaje.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtIdRegla.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtNombreRegla.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkActivo.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GroupControl9,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupControl9.ResumeLayout(false)
        Me.GroupControl9.PerformLayout
        CType(Me.GridDestinatarios,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.ViewDestinatario,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkActivoD.Properties,System.ComponentModel.ISupportInitialize).EndInit
        Me.MenuStrip1.ResumeLayout(false)
        Me.MenuStrip1.PerformLayout
        CType(Me.dkPropietarioReglaRec,System.ComponentModel.ISupportInitialize).EndInit
        Me.hideContainerBottom.ResumeLayout(false)
        Me.DockPanel1.ResumeLayout(false)
        Me.DockPanel1_Container.ResumeLayout(false)
        Me.DockPanel1_Container.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl8 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl9 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridDestinatarios As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewDestinatario As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkActivoD As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents cmdDesactivar As ToolStripMenuItem
    Friend WithEvents lblCodigo As Label
    Friend WithEvents lnkRegla As System.Windows.Forms.LinkLabel
    Friend WithEvents txtNombreRegla As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkMensaje As System.Windows.Forms.LinkLabel
    Friend WithEvents txtNombreMensaje As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmdAgregar As ToolStripMenuItem
    Friend WithEvents lblNombrePropietario As Label
    Friend WithEvents txtIdMensaje As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdRegla As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dkPropietarioReglaRec As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
End Class
