<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmConsolidador
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
        Dim lblNit As System.Windows.Forms.Label
        Dim TelefonoLabel As System.Windows.Forms.Label
        Dim Nombre_comercialLabel As System.Windows.Forms.Label
        Dim CodigoLabel As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim lblDireccion As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label13 As System.Windows.Forms.Label
        Dim Label18 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim IdEmpresaLabel As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConsolidador))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.xtrtabConsolidador = New DevExpress.XtraTab.XtraTabControl()
        Me.datosConsolidador = New DevExpress.XtraTab.XtraTabPage()
        Me.GrpConsolidador = New DevExpress.XtraEditors.GroupControl()
        Me.cmbEmpresa = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtTelefono = New DevExpress.XtraEditors.TextEdit()
        Me.lblConsolidador = New DevExpress.XtraEditors.TextEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.txtDireccion = New DevExpress.XtraEditors.TextEdit()
        Me.txtRazonSocial = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigo = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreComercial = New DevExpress.XtraEditors.TextEdit()
        Me.txtNit = New DevExpress.XtraEditors.TextEdit()
        lblNit = New System.Windows.Forms.Label()
        TelefonoLabel = New System.Windows.Forms.Label()
        Nombre_comercialLabel = New System.Windows.Forms.Label()
        CodigoLabel = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        lblDireccion = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label13 = New System.Windows.Forms.Label()
        Label18 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        IdEmpresaLabel = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtrtabConsolidador, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtrtabConsolidador.SuspendLayout()
        Me.datosConsolidador.SuspendLayout()
        CType(Me.GrpConsolidador, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpConsolidador.SuspendLayout()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTelefono.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConsolidador.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDireccion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRazonSocial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreComercial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblNit
        '
        lblNit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblNit.AutoSize = True
        lblNit.Location = New System.Drawing.Point(26, 226)
        lblNit.Name = "lblNit"
        lblNit.Size = New System.Drawing.Size(29, 17)
        lblNit.TabIndex = 29
        lblNit.Text = "Nit:"
        '
        'TelefonoLabel
        '
        TelefonoLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        TelefonoLabel.AutoSize = True
        TelefonoLabel.Location = New System.Drawing.Point(26, 190)
        TelefonoLabel.Name = "TelefonoLabel"
        TelefonoLabel.Size = New System.Drawing.Size(89, 17)
        TelefonoLabel.TabIndex = 21
        TelefonoLabel.Text = "Razón Social:"
        '
        'Nombre_comercialLabel
        '
        Nombre_comercialLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Nombre_comercialLabel.AutoSize = True
        Nombre_comercialLabel.Location = New System.Drawing.Point(26, 158)
        Nombre_comercialLabel.Name = "Nombre_comercialLabel"
        Nombre_comercialLabel.Size = New System.Drawing.Size(125, 17)
        Nombre_comercialLabel.TabIndex = 16
        Nombre_comercialLabel.Text = "Nombre Comercial:"
        '
        'CodigoLabel
        '
        CodigoLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        CodigoLabel.AutoSize = True
        CodigoLabel.Location = New System.Drawing.Point(27, 120)
        CodigoLabel.Name = "CodigoLabel"
        CodigoLabel.Size = New System.Drawing.Size(56, 17)
        CodigoLabel.TabIndex = 13
        CodigoLabel.Text = "Código:"
        '
        'Label12
        '
        Label12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(26, 44)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(22, 17)
        Label12.TabIndex = 0
        Label12.Text = "ID"
        '
        'lblDireccion
        '
        lblDireccion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblDireccion.AutoSize = True
        lblDireccion.Location = New System.Drawing.Point(26, 297)
        lblDireccion.Name = "lblDireccion"
        lblDireccion.Size = New System.Drawing.Size(69, 17)
        lblDireccion.TabIndex = 24
        lblDireccion.Text = "Dirección:"
        '
        'Label7
        '
        Label7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label7.AutoSize = True
        Label7.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label7.ForeColor = System.Drawing.Color.Red
        Label7.Location = New System.Drawing.Point(147, 118)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(21, 21)
        Label7.TabIndex = 14
        Label7.Text = "*"
        '
        'Label8
        '
        Label8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label8.AutoSize = True
        Label8.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label8.ForeColor = System.Drawing.Color.Red
        Label8.Location = New System.Drawing.Point(146, 157)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(21, 21)
        Label8.TabIndex = 17
        Label8.Text = "*"
        '
        'Label9
        '
        Label9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label9.AutoSize = True
        Label9.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label9.ForeColor = System.Drawing.Color.Red
        Label9.Location = New System.Drawing.Point(146, 189)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(21, 21)
        Label9.TabIndex = 22
        Label9.Text = "*"
        '
        'Label13
        '
        Label13.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label13.AutoSize = True
        Label13.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label13.ForeColor = System.Drawing.Color.Red
        Label13.Location = New System.Drawing.Point(146, 300)
        Label13.Name = "Label13"
        Label13.Size = New System.Drawing.Size(21, 21)
        Label13.TabIndex = 26
        Label13.Text = "*"
        '
        'Label18
        '
        Label18.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label18.AutoSize = True
        Label18.Location = New System.Drawing.Point(27, 341)
        Label18.Name = "Label18"
        Label18.Size = New System.Drawing.Size(51, 17)
        Label18.TabIndex = 35
        Label18.Text = "Activo:"
        '
        'Label2
        '
        Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(27, 261)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(65, 17)
        Label2.TabIndex = 44
        Label2.Text = "Teléfono:"
        '
        'Label1
        '
        Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label1.AutoSize = True
        Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.Red
        Label1.Location = New System.Drawing.Point(147, 264)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(21, 21)
        Label1.TabIndex = 46
        Label1.Text = "*"
        '
        'IdEmpresaLabel
        '
        IdEmpresaLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        IdEmpresaLabel.AutoSize = True
        IdEmpresaLabel.Location = New System.Drawing.Point(26, 81)
        IdEmpresaLabel.Name = "IdEmpresaLabel"
        IdEmpresaLabel.Size = New System.Drawing.Size(66, 17)
        IdEmpresaLabel.TabIndex = 47
        IdEmpresaLabel.Text = "Empresa:"
        '
        'Label4
        '
        Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label4.AutoSize = True
        Label4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label4.ForeColor = System.Drawing.Color.Red
        Label4.Location = New System.Drawing.Point(146, 82)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(21, 21)
        Label4.TabIndex = 48
        Label4.Text = "*"
        '
        'Label3
        '
        Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label3.AutoSize = True
        Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label3.ForeColor = System.Drawing.Color.Red
        Label3.Location = New System.Drawing.Point(147, 222)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(21, 21)
        Label3.TabIndex = 50
        Label3.Text = "*"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 8
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1245, 193)
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
        Me.RibbonPage1.Text = "Nuevo Consolidador"
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 780)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1245, 30)
        '
        'xtrtabConsolidador
        '
        Me.xtrtabConsolidador.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtrtabConsolidador.Location = New System.Drawing.Point(0, 193)
        Me.xtrtabConsolidador.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.xtrtabConsolidador.Name = "xtrtabConsolidador"
        Me.xtrtabConsolidador.SelectedTabPage = Me.datosConsolidador
        Me.xtrtabConsolidador.Size = New System.Drawing.Size(1245, 587)
        Me.xtrtabConsolidador.TabIndex = 2
        Me.xtrtabConsolidador.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.datosConsolidador})
        '
        'datosConsolidador
        '
        Me.datosConsolidador.Controls.Add(Me.GrpConsolidador)
        Me.datosConsolidador.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.datosConsolidador.Name = "datosConsolidador"
        Me.datosConsolidador.Size = New System.Drawing.Size(1243, 557)
        Me.datosConsolidador.Text = "Datos Consolidador"
        '
        'GrpConsolidador
        '
        Me.GrpConsolidador.Controls.Add(Label3)
        Me.GrpConsolidador.Controls.Add(Me.cmbEmpresa)
        Me.GrpConsolidador.Controls.Add(Label4)
        Me.GrpConsolidador.Controls.Add(IdEmpresaLabel)
        Me.GrpConsolidador.Controls.Add(Label1)
        Me.GrpConsolidador.Controls.Add(Label2)
        Me.GrpConsolidador.Controls.Add(Me.txtTelefono)
        Me.GrpConsolidador.Controls.Add(Me.lblConsolidador)
        Me.GrpConsolidador.Controls.Add(Label18)
        Me.GrpConsolidador.Controls.Add(Me.chkActivo)
        Me.GrpConsolidador.Controls.Add(Label13)
        Me.GrpConsolidador.Controls.Add(Label9)
        Me.GrpConsolidador.Controls.Add(Label8)
        Me.GrpConsolidador.Controls.Add(Label7)
        Me.GrpConsolidador.Controls.Add(lblDireccion)
        Me.GrpConsolidador.Controls.Add(Me.txtDireccion)
        Me.GrpConsolidador.Controls.Add(Label12)
        Me.GrpConsolidador.Controls.Add(Me.txtRazonSocial)
        Me.GrpConsolidador.Controls.Add(CodigoLabel)
        Me.GrpConsolidador.Controls.Add(Me.txtCodigo)
        Me.GrpConsolidador.Controls.Add(Nombre_comercialLabel)
        Me.GrpConsolidador.Controls.Add(Me.txtNombreComercial)
        Me.GrpConsolidador.Controls.Add(TelefonoLabel)
        Me.GrpConsolidador.Controls.Add(lblNit)
        Me.GrpConsolidador.Controls.Add(Me.txtNit)
        Me.GrpConsolidador.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpConsolidador.Location = New System.Drawing.Point(0, 0)
        Me.GrpConsolidador.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GrpConsolidador.Name = "GrpConsolidador"
        Me.GrpConsolidador.Size = New System.Drawing.Size(1243, 557)
        Me.GrpConsolidador.TabIndex = 0
        '
        'cmbEmpresa
        '
        Me.cmbEmpresa.Location = New System.Drawing.Point(171, 81)
        Me.cmbEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbEmpresa.MenuManager = Me.RibbonControl
        Me.cmbEmpresa.Name = "cmbEmpresa"
        Me.cmbEmpresa.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbEmpresa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEmpresa.Properties.NullText = ""
        Me.cmbEmpresa.Size = New System.Drawing.Size(241, 22)
        Me.cmbEmpresa.TabIndex = 49
        '
        'txtTelefono
        '
        Me.txtTelefono.Location = New System.Drawing.Point(172, 258)
        Me.txtTelefono.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtTelefono.MenuManager = Me.RibbonControl
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtTelefono.Size = New System.Drawing.Size(241, 22)
        Me.txtTelefono.TabIndex = 45
        '
        'lblConsolidador
        '
        Me.lblConsolidador.Location = New System.Drawing.Point(171, 41)
        Me.lblConsolidador.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblConsolidador.MenuManager = Me.RibbonControl
        Me.lblConsolidador.Name = "lblConsolidador"
        Me.lblConsolidador.Properties.Appearance.BackColor = System.Drawing.Color.Lavender
        Me.lblConsolidador.Properties.Appearance.Options.UseBackColor = True
        Me.lblConsolidador.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblConsolidador.Properties.Mask.EditMask = "[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"
        Me.lblConsolidador.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.lblConsolidador.Size = New System.Drawing.Size(241, 22)
        Me.lblConsolidador.TabIndex = 1
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(176, 337)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(84, 24)
        Me.chkActivo.TabIndex = 36
        '
        'txtDireccion
        '
        Me.txtDireccion.Location = New System.Drawing.Point(171, 294)
        Me.txtDireccion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtDireccion.MenuManager = Me.RibbonControl
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtDireccion.Size = New System.Drawing.Size(242, 22)
        Me.txtDireccion.TabIndex = 25
        '
        'txtRazonSocial
        '
        Me.txtRazonSocial.Location = New System.Drawing.Point(171, 187)
        Me.txtRazonSocial.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtRazonSocial.MenuManager = Me.RibbonControl
        Me.txtRazonSocial.Name = "txtRazonSocial"
        Me.txtRazonSocial.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtRazonSocial.Size = New System.Drawing.Size(241, 22)
        Me.txtRazonSocial.TabIndex = 23
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(172, 117)
        Me.txtCodigo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCodigo.MenuManager = Me.RibbonControl
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCodigo.Size = New System.Drawing.Size(240, 22)
        Me.txtCodigo.TabIndex = 15
        '
        'txtNombreComercial
        '
        Me.txtNombreComercial.Location = New System.Drawing.Point(171, 155)
        Me.txtNombreComercial.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreComercial.MenuManager = Me.RibbonControl
        Me.txtNombreComercial.Name = "txtNombreComercial"
        Me.txtNombreComercial.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtNombreComercial.Size = New System.Drawing.Size(241, 22)
        Me.txtNombreComercial.TabIndex = 19
        '
        'txtNit
        '
        Me.txtNit.Location = New System.Drawing.Point(171, 223)
        Me.txtNit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNit.MenuManager = Me.RibbonControl
        Me.txtNit.Name = "txtNit"
        Me.txtNit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtNit.Size = New System.Drawing.Size(241, 22)
        Me.txtNit.TabIndex = 30
        '
        'frmConsolidador
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1245, 810)
        Me.Controls.Add(Me.xtrtabConsolidador)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmConsolidador"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "frmConsolidador"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtrtabConsolidador, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtrtabConsolidador.ResumeLayout(False)
        Me.datosConsolidador.ResumeLayout(False)
        CType(Me.GrpConsolidador, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpConsolidador.ResumeLayout(False)
        Me.GrpConsolidador.PerformLayout()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTelefono.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConsolidador.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDireccion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRazonSocial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreComercial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents xtrtabConsolidador As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents datosConsolidador As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GrpConsolidador As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbEmpresa As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtTelefono As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblConsolidador As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtDireccion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtRazonSocial As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodigo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreComercial As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNit As DevExpress.XtraEditors.TextEdit
End Class
