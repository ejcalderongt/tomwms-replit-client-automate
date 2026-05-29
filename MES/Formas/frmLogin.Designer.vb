<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogin))
        Me.RibbonControl1 = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuIngresar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAsignacion = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdLinkDTS = New DevExpress.XtraBars.BarHeaderItem()
        Me.lblNombrePCCliente = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdVersion4 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuBD = New DevExpress.XtraBars.BarStaticItem()
        Me.SkinRibbonGalleryBarItem1 = New DevExpress.XtraBars.SkinRibbonGalleryBarItem()
        Me.RibbonPage2 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar1 = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.lblClave = New DevExpress.XtraEditors.LabelControl()
        Me.lblInstancia = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.lblUsuario = New DevExpress.XtraEditors.LabelControl()
        Me.cmdIngresar = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.txtContraseña = New DevExpress.XtraEditors.TextEdit()
        Me.cmbEmpresa = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmdSalir = New DevExpress.XtraEditors.SimpleButton()
        Me.cmbInstancia = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtUsuario = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lblEmpresa = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtContraseña.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbInstancia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUsuario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl1
        '
        Me.RibbonControl1.ExpandCollapseItem.Id = 0
        Me.RibbonControl1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl1.ExpandCollapseItem, Me.mnuIngresar, Me.mnuAsignacion, Me.cmdLinkDTS, Me.lblNombrePCCliente, Me.cmdVersion4, Me.mnuBD, Me.SkinRibbonGalleryBarItem1})
        Me.RibbonControl1.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl1.MaxItemId = 2
        Me.RibbonControl1.Name = "RibbonControl1"
        Me.RibbonControl1.PageHeaderItemLinks.Add(Me.cmdLinkDTS)
        Me.RibbonControl1.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage2})
        Me.RibbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.OfficeUniversal
        Me.RibbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl1.Size = New System.Drawing.Size(719, 108)
        Me.RibbonControl1.StatusBar = Me.RibbonStatusBar1
        '
        'mnuIngresar
        '
        Me.mnuIngresar.Caption = "Ingresar"
        Me.mnuIngresar.Id = 3
        Me.mnuIngresar.ImageOptions.Image = CType(resources.GetObject("mnuIngresar.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuIngresar.ImageOptions.LargeImage = CType(resources.GetObject("mnuIngresar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuIngresar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.mnuIngresar.Name = "mnuIngresar"
        '
        'mnuAsignacion
        '
        Me.mnuAsignacion.Caption = "Asignacion"
        Me.mnuAsignacion.Id = 4
        Me.mnuAsignacion.ImageOptions.Image = CType(resources.GetObject("mnuAsignacion.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuAsignacion.ImageOptions.LargeImage = CType(resources.GetObject("mnuAsignacion.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuAsignacion.Name = "mnuAsignacion"
        '
        'cmdLinkDTS
        '
        Me.cmdLinkDTS.AllowHtmlText = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmdLinkDTS.Caption = "www.dts.com.gt"
        Me.cmdLinkDTS.Id = 8
        Me.cmdLinkDTS.ImageOptions.Image = CType(resources.GetObject("cmdLinkDTS.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdLinkDTS.ImageOptions.LargeImage = CType(resources.GetObject("cmdLinkDTS.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdLinkDTS.Name = "cmdLinkDTS"
        '
        'lblNombrePCCliente
        '
        Me.lblNombrePCCliente.ActAsDropDown = True
        Me.lblNombrePCCliente.Caption = "PC"
        Me.lblNombrePCCliente.Id = 15
        Me.lblNombrePCCliente.ImageOptions.Image = CType(resources.GetObject("lblNombrePCCliente.ImageOptions.Image"), System.Drawing.Image)
        Me.lblNombrePCCliente.ImageOptions.LargeImage = CType(resources.GetObject("lblNombrePCCliente.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.lblNombrePCCliente.Name = "lblNombrePCCliente"
        '
        'cmdVersion4
        '
        Me.cmdVersion4.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.cmdVersion4.Caption = "Versión 8.0.0 FP: 20260528"
        Me.cmdVersion4.Id = 16
        Me.cmdVersion4.ImageOptions.Image = CType(resources.GetObject("cmdVersion4.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdVersion4.ImageOptions.LargeImage = CType(resources.GetObject("cmdVersion4.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdVersion4.ItemAppearance.Disabled.BackColor = System.Drawing.Color.Transparent
        Me.cmdVersion4.ItemAppearance.Disabled.Options.UseBackColor = True
        Me.cmdVersion4.ItemAppearance.Normal.BackColor = System.Drawing.Color.LightGray
        Me.cmdVersion4.ItemAppearance.Normal.ForeColor = System.Drawing.Color.Black
        Me.cmdVersion4.ItemAppearance.Normal.Options.UseBackColor = True
        Me.cmdVersion4.ItemAppearance.Normal.Options.UseForeColor = True
        Me.cmdVersion4.Name = "cmdVersion4"
        '
        'mnuBD
        '
        Me.mnuBD.Caption = "BD:00"
        Me.mnuBD.Id = 20
        Me.mnuBD.ImageOptions.Image = CType(resources.GetObject("mnuBD.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuBD.Name = "mnuBD"
        Me.mnuBD.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'SkinRibbonGalleryBarItem1
        '
        Me.SkinRibbonGalleryBarItem1.Caption = "SkinRibbonGalleryBarItem1"
        Me.SkinRibbonGalleryBarItem1.Id = 1
        Me.SkinRibbonGalleryBarItem1.Name = "SkinRibbonGalleryBarItem1"
        '
        'RibbonPage2
        '
        Me.RibbonPage2.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup2, Me.RibbonPageGroup3})
        Me.RibbonPage2.Name = "RibbonPage2"
        Me.RibbonPage2.Text = "Menu"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuIngresar)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.mnuBD)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'RibbonStatusBar1
        '
        Me.RibbonStatusBar1.ItemLinks.Add(Me.lblNombrePCCliente)
        Me.RibbonStatusBar1.ItemLinks.Add(Me.cmdVersion4)
        Me.RibbonStatusBar1.Location = New System.Drawing.Point(0, 663)
        Me.RibbonStatusBar1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar1.Name = "RibbonStatusBar1"
        Me.RibbonStatusBar1.Ribbon = Me.RibbonControl1
        Me.RibbonStatusBar1.Size = New System.Drawing.Size(719, 30)
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.lblClave)
        Me.GroupControl1.Controls.Add(Me.lblInstancia)
        Me.GroupControl1.Controls.Add(Me.LabelControl4)
        Me.GroupControl1.Controls.Add(Me.lblUsuario)
        Me.GroupControl1.Controls.Add(Me.cmdIngresar)
        Me.GroupControl1.Controls.Add(Me.LabelControl5)
        Me.GroupControl1.Controls.Add(Me.txtContraseña)
        Me.GroupControl1.Controls.Add(Me.cmbEmpresa)
        Me.GroupControl1.Controls.Add(Me.cmdSalir)
        Me.GroupControl1.Controls.Add(Me.cmbInstancia)
        Me.GroupControl1.Controls.Add(Me.txtUsuario)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.lblEmpresa)
        Me.GroupControl1.Location = New System.Drawing.Point(55, 200)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(578, 417)
        Me.GroupControl1.TabIndex = 35
        '
        'lblClave
        '
        Me.lblClave.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.lblClave.Appearance.Options.UseFont = True
        Me.lblClave.Location = New System.Drawing.Point(41, 240)
        Me.lblClave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblClave.Name = "lblClave"
        Me.lblClave.Size = New System.Drawing.Size(51, 25)
        Me.lblClave.TabIndex = 32
        Me.lblClave.Text = "Clave"
        '
        'lblInstancia
        '
        Me.lblInstancia.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.lblInstancia.Appearance.Options.UseFont = True
        Me.lblInstancia.Location = New System.Drawing.Point(41, 80)
        Me.lblInstancia.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.lblInstancia.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.lblInstancia.LookAndFeel.UseDefaultLookAndFeel = False
        Me.lblInstancia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblInstancia.Name = "lblInstancia"
        Me.lblInstancia.Size = New System.Drawing.Size(78, 25)
        Me.lblInstancia.TabIndex = 28
        Me.lblInstancia.Text = "Instancia"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Image = CType(resources.GetObject("LabelControl4.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl4.Appearance.Options.UseImage = True
        Me.LabelControl4.Location = New System.Drawing.Point(470, 190)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(16, 16)
        Me.LabelControl4.TabIndex = 24
        '
        'lblUsuario
        '
        Me.lblUsuario.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.lblUsuario.Appearance.Options.UseFont = True
        Me.lblUsuario.Location = New System.Drawing.Point(41, 186)
        Me.lblUsuario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(67, 25)
        Me.lblUsuario.TabIndex = 31
        Me.lblUsuario.Text = "Usuario"
        '
        'cmdIngresar
        '
        Me.cmdIngresar.Appearance.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdIngresar.Appearance.Options.UseFont = True
        Me.cmdIngresar.Location = New System.Drawing.Point(156, 307)
        Me.cmdIngresar.LookAndFeel.SkinName = "Metropolis"
        Me.cmdIngresar.LookAndFeel.UseDefaultLookAndFeel = False
        Me.cmdIngresar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdIngresar.Name = "cmdIngresar"
        Me.cmdIngresar.Size = New System.Drawing.Size(133, 55)
        Me.cmdIngresar.TabIndex = 21
        Me.cmdIngresar.Text = "Ingresar"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Image = CType(resources.GetObject("LabelControl5.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl5.Appearance.Options.UseImage = True
        Me.LabelControl5.Location = New System.Drawing.Point(470, 244)
        Me.LabelControl5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(16, 16)
        Me.LabelControl5.TabIndex = 25
        '
        'txtContraseña
        '
        Me.txtContraseña.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtContraseña.EditValue = ""
        Me.txtContraseña.Location = New System.Drawing.Point(156, 237)
        Me.txtContraseña.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtContraseña.MenuManager = Me.RibbonControl1
        Me.txtContraseña.Name = "txtContraseña"
        Me.txtContraseña.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.txtContraseña.Properties.Appearance.Options.UseFont = True
        Me.txtContraseña.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.txtContraseña.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtContraseña.Size = New System.Drawing.Size(347, 34)
        Me.txtContraseña.TabIndex = 20
        '
        'cmbEmpresa
        '
        Me.cmbEmpresa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbEmpresa.Location = New System.Drawing.Point(156, 128)
        Me.cmbEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbEmpresa.MenuManager = Me.RibbonControl1
        Me.cmbEmpresa.Name = "cmbEmpresa"
        Me.cmbEmpresa.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.cmbEmpresa.Properties.Appearance.Options.UseFont = True
        Me.cmbEmpresa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEmpresa.Properties.NullText = ""
        Me.cmbEmpresa.Size = New System.Drawing.Size(344, 32)
        Me.cmbEmpresa.TabIndex = 17
        '
        'cmdSalir
        '
        Me.cmdSalir.Appearance.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSalir.Appearance.Options.UseFont = True
        Me.cmdSalir.Location = New System.Drawing.Point(328, 307)
        Me.cmdSalir.LookAndFeel.SkinName = "Metropolis"
        Me.cmdSalir.LookAndFeel.UseDefaultLookAndFeel = False
        Me.cmdSalir.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(133, 55)
        Me.cmdSalir.TabIndex = 22
        Me.cmdSalir.Text = "Salir"
        '
        'cmbInstancia
        '
        Me.cmbInstancia.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbInstancia.Location = New System.Drawing.Point(156, 76)
        Me.cmbInstancia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbInstancia.MenuManager = Me.RibbonControl1
        Me.cmbInstancia.Name = "cmbInstancia"
        Me.cmbInstancia.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.cmbInstancia.Properties.Appearance.Options.UseFont = True
        Me.cmbInstancia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbInstancia.Properties.NullText = ""
        Me.cmbInstancia.Properties.PopupFormMinSize = New System.Drawing.Size(600, 200)
        Me.cmbInstancia.Properties.PopupWidth = 100
        Me.cmbInstancia.Size = New System.Drawing.Size(344, 32)
        Me.cmbInstancia.TabIndex = 16
        '
        'txtUsuario
        '
        Me.txtUsuario.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUsuario.EditValue = ""
        Me.txtUsuario.Location = New System.Drawing.Point(156, 183)
        Me.txtUsuario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtUsuario.MenuManager = Me.RibbonControl1
        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.txtUsuario.Properties.Appearance.Options.UseFont = True
        Me.txtUsuario.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.txtUsuario.Size = New System.Drawing.Size(347, 34)
        Me.txtUsuario.TabIndex = 19
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Image = CType(resources.GetObject("LabelControl2.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl2.Appearance.Options.UseImage = True
        Me.LabelControl2.Location = New System.Drawing.Point(470, 131)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(16, 16)
        Me.LabelControl2.TabIndex = 22
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Image = CType(resources.GetObject("LabelControl1.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl1.Appearance.Options.UseImage = True
        Me.LabelControl1.Location = New System.Drawing.Point(470, 80)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(16, 16)
        Me.LabelControl1.TabIndex = 21
        '
        'lblEmpresa
        '
        Me.lblEmpresa.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.lblEmpresa.Appearance.Options.UseFont = True
        Me.lblEmpresa.Location = New System.Drawing.Point(41, 131)
        Me.lblEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.Size = New System.Drawing.Size(78, 25)
        Me.lblEmpresa.TabIndex = 29
        Me.lblEmpresa.Text = "Empresa"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl6.Appearance.ForeColor = System.Drawing.Color.SteelBlue
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Appearance.Options.UseForeColor = True
        Me.LabelControl6.Appearance.Options.UseTextOptions = True
        Me.LabelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.LabelControl6.Dock = System.Windows.Forms.DockStyle.Top
        Me.LabelControl6.LineLocation = DevExpress.XtraEditors.LineLocation.Center
        Me.LabelControl6.Location = New System.Drawing.Point(0, 108)
        Me.LabelControl6.LookAndFeel.SkinName = "Office 2013 Light Gray"
        Me.LabelControl6.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(719, 76)
        Me.LabelControl6.TabIndex = 34
        Me.LabelControl6.Text = "QUICKTAG"
        '
        'frmLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(719, 693)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.RibbonStatusBar1)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.RibbonControl1)
        Me.Name = "frmLogin"
        Me.Ribbon = Me.RibbonControl1
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar1
        Me.Text = "frmLogin"
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtContraseña.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbInstancia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUsuario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RibbonControl1 As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuIngresar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuAsignacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdLinkDTS As DevExpress.XtraBars.BarHeaderItem
    Friend WithEvents lblNombrePCCliente As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdVersion4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuBD As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents SkinRibbonGalleryBarItem1 As DevExpress.XtraBars.SkinRibbonGalleryBarItem
    Friend WithEvents RibbonPage2 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar1 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblClave As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblInstancia As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblUsuario As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmdIngresar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtContraseña As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbEmpresa As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmdSalir As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmbInstancia As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtUsuario As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblEmpresa As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl

End Class
