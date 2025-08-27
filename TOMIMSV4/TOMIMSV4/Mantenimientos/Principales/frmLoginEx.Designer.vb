<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLoginEx
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If MenuP IsNot Nothing Then
                MenuP.Dispose()
                MenuP = Nothing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLoginEx))
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.lblNombrePCCliente = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdVersion4 = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuIngresar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAsignacion = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdLinkDTS = New DevExpress.XtraBars.BarHeaderItem()
        Me.mnuBD = New DevExpress.XtraBars.BarStaticItem()
        Me.SkinRibbonGalleryBarItem1 = New DevExpress.XtraBars.SkinRibbonGalleryBarItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTestDirect = New DevExpress.XtraBars.BarSubItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem5 = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.lblClave = New DevExpress.XtraEditors.LabelControl()
        Me.lblUsuario = New DevExpress.XtraEditors.LabelControl()
        Me.lblBodega = New DevExpress.XtraEditors.LabelControl()
        Me.lblEmpresa = New DevExpress.XtraEditors.LabelControl()
        Me.lblInstancia = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.cmdSalir = New DevExpress.XtraEditors.SimpleButton()
        Me.cmdIngresar = New DevExpress.XtraEditors.SimpleButton()
        Me.cmbBodegas = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbEmpresa = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbInstancia = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtUsuario = New DevExpress.XtraEditors.TextEdit()
        Me.txtContraseña = New DevExpress.XtraEditors.TextEdit()
        Me.DefaultLookAndFeel1 = New DevExpress.LookAndFeel.DefaultLookAndFeel(Me.components)
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.PictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodegas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbInstancia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUsuario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtContraseña.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblNombrePCCliente)
        Me.RibbonStatusBar.ItemLinks.Add(Me.cmdVersion4)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 839)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(833, 30)
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
        Me.cmdVersion4.Caption = "Versión 6.0.4 FP: 20240830"
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
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuIngresar, Me.mnuAsignacion, Me.cmdLinkDTS, Me.lblNombrePCCliente, Me.cmdVersion4, Me.mnuBD, Me.SkinRibbonGalleryBarItem1, Me.BarButtonItem1, Me.BarButtonItem2, Me.mnuTestDirect, Me.BarButtonItem3, Me.BarButtonItem4, Me.BarButtonItem5})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 8
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.PageHeaderItemLinks.Add(Me.cmdLinkDTS)
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.OfficeUniversal
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(833, 108)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
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
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "BarButtonItem1"
        Me.BarButtonItem1.Id = 2
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "BarButtonItem2"
        Me.BarButtonItem2.Id = 3
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'mnuTestDirect
        '
        Me.mnuTestDirect.Caption = "Tableros"
        Me.mnuTestDirect.Id = 4
        Me.mnuTestDirect.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem3), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem4)})
        Me.mnuTestDirect.Name = "mnuTestDirect"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Op1"
        Me.BarButtonItem3.Id = 5
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'BarButtonItem4
        '
        Me.BarButtonItem4.Caption = "Op2"
        Me.BarButtonItem4.Id = 6
        Me.BarButtonItem4.Name = "BarButtonItem4"
        '
        'BarButtonItem5
        '
        Me.BarButtonItem5.Caption = "Test"
        Me.BarButtonItem5.Id = 7
        Me.BarButtonItem5.Name = "BarButtonItem5"
        Me.BarButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2, Me.RibbonPageGroup3})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menu"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuIngresar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuBD)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.BarButtonItem5)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        Me.RibbonPageGroup3.Text = "RibbonPageGroup3"
        '
        'lblClave
        '
        Me.lblClave.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClave.Appearance.Options.UseFont = True
        Me.lblClave.Location = New System.Drawing.Point(69, 298)
        Me.lblClave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblClave.Name = "lblClave"
        Me.lblClave.Size = New System.Drawing.Size(42, 20)
        Me.lblClave.TabIndex = 32
        Me.lblClave.Text = "Clave"
        '
        'lblUsuario
        '
        Me.lblUsuario.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsuario.Appearance.Options.UseFont = True
        Me.lblUsuario.Location = New System.Drawing.Point(69, 244)
        Me.lblUsuario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(58, 20)
        Me.lblUsuario.TabIndex = 31
        Me.lblUsuario.Text = "Usuario"
        '
        'lblBodega
        '
        Me.lblBodega.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBodega.Appearance.Options.UseFont = True
        Me.lblBodega.Location = New System.Drawing.Point(69, 191)
        Me.lblBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblBodega.Name = "lblBodega"
        Me.lblBodega.Size = New System.Drawing.Size(57, 20)
        Me.lblBodega.TabIndex = 30
        Me.lblBodega.Text = "Bodega"
        '
        'lblEmpresa
        '
        Me.lblEmpresa.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpresa.Appearance.Options.UseFont = True
        Me.lblEmpresa.Location = New System.Drawing.Point(69, 137)
        Me.lblEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.Size = New System.Drawing.Size(67, 20)
        Me.lblEmpresa.TabIndex = 29
        Me.lblEmpresa.Text = "Empresa"
        '
        'lblInstancia
        '
        Me.lblInstancia.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstancia.Appearance.Options.UseFont = True
        Me.lblInstancia.Location = New System.Drawing.Point(69, 86)
        Me.lblInstancia.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.lblInstancia.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.lblInstancia.LookAndFeel.UseDefaultLookAndFeel = False
        Me.lblInstancia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblInstancia.Name = "lblInstancia"
        Me.lblInstancia.Size = New System.Drawing.Size(67, 20)
        Me.lblInstancia.TabIndex = 28
        Me.lblInstancia.Text = "Instancia"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Image = CType(resources.GetObject("LabelControl5.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl5.Appearance.Options.UseImage = True
        Me.LabelControl5.Location = New System.Drawing.Point(500, 301)
        Me.LabelControl5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(16, 16)
        Me.LabelControl5.TabIndex = 25
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Image = CType(resources.GetObject("LabelControl4.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl4.Appearance.Options.UseImage = True
        Me.LabelControl4.Location = New System.Drawing.Point(500, 244)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(16, 16)
        Me.LabelControl4.TabIndex = 24
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Image = CType(resources.GetObject("LabelControl3.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl3.Appearance.Options.UseImage = True
        Me.LabelControl3.Location = New System.Drawing.Point(500, 195)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(16, 16)
        Me.LabelControl3.TabIndex = 23
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Image = CType(resources.GetObject("LabelControl2.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl2.Appearance.Options.UseImage = True
        Me.LabelControl2.Location = New System.Drawing.Point(500, 139)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(16, 16)
        Me.LabelControl2.TabIndex = 22
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Image = CType(resources.GetObject("LabelControl1.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl1.Appearance.Options.UseImage = True
        Me.LabelControl1.Location = New System.Drawing.Point(500, 86)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(16, 16)
        Me.LabelControl1.TabIndex = 21
        '
        'cmdSalir
        '
        Me.cmdSalir.Appearance.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSalir.Appearance.Options.UseFont = True
        Me.cmdSalir.Location = New System.Drawing.Point(361, 360)
        Me.cmdSalir.LookAndFeel.SkinName = "Metropolis"
        Me.cmdSalir.LookAndFeel.UseDefaultLookAndFeel = False
        Me.cmdSalir.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(133, 55)
        Me.cmdSalir.TabIndex = 22
        Me.cmdSalir.Text = "Salir"
        '
        'cmdIngresar
        '
        Me.cmdIngresar.Appearance.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdIngresar.Appearance.Options.UseFont = True
        Me.cmdIngresar.Location = New System.Drawing.Point(156, 360)
        Me.cmdIngresar.LookAndFeel.SkinName = "Metropolis"
        Me.cmdIngresar.LookAndFeel.UseDefaultLookAndFeel = False
        Me.cmdIngresar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdIngresar.Name = "cmdIngresar"
        Me.cmdIngresar.Size = New System.Drawing.Size(133, 55)
        Me.cmdIngresar.TabIndex = 21
        Me.cmdIngresar.Text = "Ingresar"
        '
        'cmbBodegas
        '
        Me.cmbBodegas.Location = New System.Drawing.Point(156, 182)
        Me.cmbBodegas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbBodegas.MenuManager = Me.RibbonControl
        Me.cmbBodegas.Name = "cmbBodegas"
        Me.cmbBodegas.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBodegas.Properties.Appearance.Options.UseFont = True
        Me.cmbBodegas.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodegas.Properties.NullText = ""
        Me.cmbBodegas.Size = New System.Drawing.Size(338, 26)
        Me.cmbBodegas.TabIndex = 18
        '
        'cmbEmpresa
        '
        Me.cmbEmpresa.Location = New System.Drawing.Point(156, 128)
        Me.cmbEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbEmpresa.MenuManager = Me.RibbonControl
        Me.cmbEmpresa.Name = "cmbEmpresa"
        Me.cmbEmpresa.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbEmpresa.Properties.Appearance.Options.UseFont = True
        Me.cmbEmpresa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEmpresa.Properties.NullText = ""
        Me.cmbEmpresa.Size = New System.Drawing.Size(338, 26)
        Me.cmbEmpresa.TabIndex = 17
        '
        'cmbInstancia
        '
        Me.cmbInstancia.Location = New System.Drawing.Point(156, 76)
        Me.cmbInstancia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbInstancia.MenuManager = Me.RibbonControl
        Me.cmbInstancia.Name = "cmbInstancia"
        Me.cmbInstancia.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbInstancia.Properties.Appearance.Options.UseFont = True
        Me.cmbInstancia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbInstancia.Properties.NullText = ""
        Me.cmbInstancia.Properties.PopupFormMinSize = New System.Drawing.Size(600, 200)
        Me.cmbInstancia.Properties.PopupWidth = 100
        Me.cmbInstancia.Size = New System.Drawing.Size(338, 26)
        Me.cmbInstancia.TabIndex = 16
        '
        'txtUsuario
        '
        Me.txtUsuario.EditValue = ""
        Me.txtUsuario.Location = New System.Drawing.Point(156, 235)
        Me.txtUsuario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtUsuario.MenuManager = Me.RibbonControl
        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsuario.Properties.Appearance.Options.UseFont = True
        Me.txtUsuario.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.txtUsuario.Size = New System.Drawing.Size(338, 28)
        Me.txtUsuario.TabIndex = 19
        '
        'txtContraseña
        '
        Me.txtContraseña.EditValue = ""
        Me.txtContraseña.Location = New System.Drawing.Point(156, 289)
        Me.txtContraseña.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtContraseña.MenuManager = Me.RibbonControl
        Me.txtContraseña.Name = "txtContraseña"
        Me.txtContraseña.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContraseña.Properties.Appearance.Options.UseFont = True
        Me.txtContraseña.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.txtContraseña.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtContraseña.Size = New System.Drawing.Size(338, 28)
        Me.txtContraseña.TabIndex = 20
        '
        'DefaultLookAndFeel1
        '
        Me.DefaultLookAndFeel1.EnableBonusSkins = True
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.lblClave)
        Me.GroupControl1.Controls.Add(Me.lblInstancia)
        Me.GroupControl1.Controls.Add(Me.LabelControl4)
        Me.GroupControl1.Controls.Add(Me.cmbBodegas)
        Me.GroupControl1.Controls.Add(Me.lblUsuario)
        Me.GroupControl1.Controls.Add(Me.cmdIngresar)
        Me.GroupControl1.Controls.Add(Me.LabelControl5)
        Me.GroupControl1.Controls.Add(Me.txtContraseña)
        Me.GroupControl1.Controls.Add(Me.LabelControl3)
        Me.GroupControl1.Controls.Add(Me.cmbEmpresa)
        Me.GroupControl1.Controls.Add(Me.lblBodega)
        Me.GroupControl1.Controls.Add(Me.cmdSalir)
        Me.GroupControl1.Controls.Add(Me.cmbInstancia)
        Me.GroupControl1.Controls.Add(Me.txtUsuario)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.lblEmpresa)
        Me.GroupControl1.Location = New System.Drawing.Point(119, 147)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(592, 460)
        Me.GroupControl1.TabIndex = 28
        Me.GroupControl1.Text = "TOMWMS - Credenciales para autenticación"
        '
        'PictureEdit1
        '
        Me.PictureEdit1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PictureEdit1.EditValue = CType(resources.GetObject("PictureEdit1.EditValue"), Object)
        Me.PictureEdit1.Location = New System.Drawing.Point(0, 711)
        Me.PictureEdit1.MenuManager = Me.RibbonControl
        Me.PictureEdit1.Name = "PictureEdit1"
        Me.PictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PictureEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.PictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureEdit1.Properties.PictureAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.PictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.PictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
        Me.PictureEdit1.Size = New System.Drawing.Size(833, 128)
        Me.PictureEdit1.TabIndex = 31
        '
        'frmLoginEx
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(833, 869)
        Me.ControlBox = False
        Me.Controls.Add(Me.PictureEdit1)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.InactiveGlowColor = System.Drawing.Color.Blue
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLoginEx"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Inicio de sesión TOMWMS"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodegas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbInstancia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUsuario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtContraseña.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuIngresar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuAsignacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdLinkDTS As DevExpress.XtraBars.BarHeaderItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents lblNombrePCCliente As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdVersion4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuBD As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents DefaultLookAndFeel1 As DevExpress.LookAndFeel.DefaultLookAndFeel
    Friend WithEvents SkinRibbonGalleryBarItem1 As DevExpress.XtraBars.SkinRibbonGalleryBarItem
    Friend WithEvents txtContraseña As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtUsuario As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbInstancia As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbBodegas As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbEmpresa As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmdIngresar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmdSalir As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblEmpresa As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblInstancia As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblClave As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblUsuario As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblBodega As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents PictureEdit1 As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuTestDirect As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem5 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
End Class
