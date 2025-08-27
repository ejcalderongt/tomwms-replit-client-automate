<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPropietarioDestinatario
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If pObjDestinatario IsNot Nothing Then
                pObjDestinatario.Dispose()
                pObjDestinatario = Nothing
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
        Dim Label54 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPropietarioDestinatario))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl8 = New DevExpress.XtraEditors.GroupControl()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.cmdGuardar = New System.Windows.Forms.ToolStripButton()
        Me.cmdDelete = New System.Windows.Forms.ToolStripButton()
        Me.txtCargoDestinatario = New DevExpress.XtraEditors.TextEdit()
        Me.txtEmailDestinatario = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreDestinatario = New DevExpress.XtraEditors.TextEdit()
        Me.txtApellidoDestinatario = New DevExpress.XtraEditors.TextEdit()
        Me.txtTelefono1 = New DevExpress.XtraEditors.TextEdit()
        Me.txtTelefono2 = New DevExpress.XtraEditors.TextEdit()
        Me.lblCodigoD = New System.Windows.Forms.Label()
        Me.lblNombrePropietario = New System.Windows.Forms.Label()
        Me.GroupControl9 = New DevExpress.XtraEditors.GroupControl()
        Me.GridDestinatarios = New DevExpress.XtraGrid.GridControl()
        Me.ViewDestinatario = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.chkActivoD = New DevExpress.XtraEditors.CheckEdit()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Label54 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl8.SuspendLayout()
        Me.ToolStrip.SuspendLayout()
        CType(Me.txtCargoDestinatario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmailDestinatario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreDestinatario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApellidoDestinatario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTelefono1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTelefono2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl9.SuspendLayout()
        CType(Me.GridDestinatarios, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewDestinatario, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivoD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        'Label1
        '
        Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(29, 281)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(50, 17)
        Label1.TabIndex = 15
        Label1.Text = "Cargo:"
        '
        'Label3
        '
        Label3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(29, 135)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(62, 17)
        Label3.TabIndex = 5
        Label3.Text = "Nombre:"
        '
        'Label4
        '
        Label4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(29, 164)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(58, 17)
        Label4.TabIndex = 7
        Label4.Text = "Apellido:"
        '
        'Label5
        '
        Label5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(31, 193)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(44, 17)
        Label5.TabIndex = 9
        Label5.Text = "Email:"
        '
        'Label6
        '
        Label6.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(29, 223)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(65, 17)
        Label6.TabIndex = 11
        Label6.Text = "Teléfono:"
        '
        'Label7
        '
        Label7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(29, 252)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(77, 17)
        Label7.TabIndex = 13
        Label7.Text = "Teléfono 2:"
        '
        'Label9
        '
        Label9.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(29, 107)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(51, 17)
        Label9.TabIndex = 3
        Label9.Text = "Código"
        '
        'Label2
        '
        Label2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(29, 75)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(78, 17)
        Label2.TabIndex = 1
        Label2.Text = "Propietario:"
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
        Me.mnuActualizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.mnuEliminar.Name = "mnuEliminar"
        Me.mnuEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
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
        'GroupControl8
        '
        Me.GroupControl8.Controls.Add(Me.ToolStrip)
        Me.GroupControl8.Controls.Add(Label2)
        Me.GroupControl8.Controls.Add(Label1)
        Me.GroupControl8.Controls.Add(Me.txtCargoDestinatario)
        Me.GroupControl8.Controls.Add(Me.txtEmailDestinatario)
        Me.GroupControl8.Controls.Add(Label3)
        Me.GroupControl8.Controls.Add(Me.txtNombreDestinatario)
        Me.GroupControl8.Controls.Add(Label4)
        Me.GroupControl8.Controls.Add(Me.txtApellidoDestinatario)
        Me.GroupControl8.Controls.Add(Label5)
        Me.GroupControl8.Controls.Add(Label6)
        Me.GroupControl8.Controls.Add(Me.txtTelefono1)
        Me.GroupControl8.Controls.Add(Label7)
        Me.GroupControl8.Controls.Add(Me.txtTelefono2)
        Me.GroupControl8.Controls.Add(Me.lblCodigoD)
        Me.GroupControl8.Controls.Add(Label9)
        Me.GroupControl8.Controls.Add(Me.lblNombrePropietario)
        Me.GroupControl8.Controls.Add(Me.GroupControl9)
        Me.GroupControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl8.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl8.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl8.Name = "GroupControl8"
        Me.GroupControl8.Size = New System.Drawing.Size(1098, 619)
        Me.GroupControl8.TabIndex = 0
        '
        'ToolStrip
        '
        Me.ToolStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdGuardar, Me.cmdDelete})
        Me.ToolStrip.Location = New System.Drawing.Point(2, 28)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(1094, 27)
        Me.ToolStrip.TabIndex = 0
        Me.ToolStrip.Text = "ToolStrip4"
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGuardar.Image = My.Resources.Resources.greencheck
        Me.cmdGuardar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdGuardar.Name = "cmdGuardar"
        Me.cmdGuardar.Size = New System.Drawing.Size(90, 24)
        Me.cmdGuardar.Text = "Guardar"
        '
        'cmdDelete
        '
        Me.cmdDelete.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(90, 24)
        Me.cmdDelete.Text = "Eliminar"
        '
        'txtCargoDestinatario
        '
        Me.txtCargoDestinatario.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCargoDestinatario.Location = New System.Drawing.Point(152, 277)
        Me.txtCargoDestinatario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCargoDestinatario.MenuManager = Me.RibbonControl
        Me.txtCargoDestinatario.Name = "txtCargoDestinatario"
        Me.txtCargoDestinatario.Size = New System.Drawing.Size(241, 22)
        Me.txtCargoDestinatario.TabIndex = 16
        '
        'txtEmailDestinatario
        '
        Me.txtEmailDestinatario.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEmailDestinatario.Location = New System.Drawing.Point(152, 190)
        Me.txtEmailDestinatario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtEmailDestinatario.MenuManager = Me.RibbonControl
        Me.txtEmailDestinatario.Name = "txtEmailDestinatario"
        Me.txtEmailDestinatario.Properties.Mask.EditMask = "[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"
        Me.txtEmailDestinatario.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.txtEmailDestinatario.Size = New System.Drawing.Size(241, 22)
        Me.txtEmailDestinatario.TabIndex = 10
        '
        'txtNombreDestinatario
        '
        Me.txtNombreDestinatario.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNombreDestinatario.Location = New System.Drawing.Point(152, 132)
        Me.txtNombreDestinatario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreDestinatario.MenuManager = Me.RibbonControl
        Me.txtNombreDestinatario.Name = "txtNombreDestinatario"
        Me.txtNombreDestinatario.Size = New System.Drawing.Size(241, 22)
        Me.txtNombreDestinatario.TabIndex = 6
        '
        'txtApellidoDestinatario
        '
        Me.txtApellidoDestinatario.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtApellidoDestinatario.Location = New System.Drawing.Point(152, 160)
        Me.txtApellidoDestinatario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtApellidoDestinatario.MenuManager = Me.RibbonControl
        Me.txtApellidoDestinatario.Name = "txtApellidoDestinatario"
        Me.txtApellidoDestinatario.Size = New System.Drawing.Size(241, 22)
        Me.txtApellidoDestinatario.TabIndex = 8
        '
        'txtTelefono1
        '
        Me.txtTelefono1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTelefono1.Location = New System.Drawing.Point(152, 219)
        Me.txtTelefono1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtTelefono1.MenuManager = Me.RibbonControl
        Me.txtTelefono1.Name = "txtTelefono1"
        Me.txtTelefono1.Size = New System.Drawing.Size(241, 22)
        Me.txtTelefono1.TabIndex = 12
        '
        'txtTelefono2
        '
        Me.txtTelefono2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTelefono2.Location = New System.Drawing.Point(152, 249)
        Me.txtTelefono2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtTelefono2.MenuManager = Me.RibbonControl
        Me.txtTelefono2.Name = "txtTelefono2"
        Me.txtTelefono2.Size = New System.Drawing.Size(241, 22)
        Me.txtTelefono2.TabIndex = 14
        '
        'lblCodigoD
        '
        Me.lblCodigoD.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCodigoD.AutoSize = True
        Me.lblCodigoD.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodigoD.Location = New System.Drawing.Point(150, 107)
        Me.lblCodigoD.Name = "lblCodigoD"
        Me.lblCodigoD.Size = New System.Drawing.Size(14, 17)
        Me.lblCodigoD.TabIndex = 4
        Me.lblCodigoD.Text = "-"
        '
        'lblNombrePropietario
        '
        Me.lblNombrePropietario.AutoSize = True
        Me.lblNombrePropietario.Font = New System.Drawing.Font("Tahoma", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombrePropietario.Location = New System.Drawing.Point(148, 73)
        Me.lblNombrePropietario.Name = "lblNombrePropietario"
        Me.lblNombrePropietario.Size = New System.Drawing.Size(0, 21)
        Me.lblNombrePropietario.TabIndex = 2
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
        Me.GroupControl9.Location = New System.Drawing.Point(2, 318)
        Me.GroupControl9.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl9.Name = "GroupControl9"
        Me.GroupControl9.Size = New System.Drawing.Size(1093, 443)
        Me.GroupControl9.TabIndex = 17
        Me.GroupControl9.Text = "Destinatarios"
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
        Me.GridDestinatarios.Size = New System.Drawing.Size(1088, 236)
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
        Me.MenuStrip1.Location = New System.Drawing.Point(2, 28)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(7, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(1089, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'frmPropietarioDestinatario
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1098, 842)
        Me.Controls.Add(Me.GroupControl8)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmPropietarioDestinatario"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Destinatario"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl8.ResumeLayout(False)
        Me.GroupControl8.PerformLayout()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        CType(Me.txtCargoDestinatario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmailDestinatario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreDestinatario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApellidoDestinatario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTelefono1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTelefono2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl9.ResumeLayout(False)
        Me.GroupControl9.PerformLayout()
        CType(Me.GridDestinatarios, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewDestinatario, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivoD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents GroupControl8 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl9 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkActivoD As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents lblNombrePropietario As Label
    Friend WithEvents txtCargoDestinatario As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtEmailDestinatario As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreDestinatario As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtApellidoDestinatario As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTelefono1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTelefono2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblCodigoD As System.Windows.Forms.Label
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdGuardar As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents GridDestinatarios As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewDestinatario As DevExpress.XtraGrid.Views.Grid.GridView
End Class
