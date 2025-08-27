<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMenu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMenu))
        Me.rbMain = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuPrueba = New DevExpress.XtraBars.BarButtonItem()
        Me.lblNombrePC = New DevExpress.XtraBars.BarStaticItem()
        Me.lblServerAPP = New DevExpress.XtraBars.BarStaticItem()
        Me.lblBDAPP = New DevExpress.XtraBars.BarStaticItem()
        Me.lblEmpresa = New DevExpress.XtraBars.BarStaticItem()
        Me.lblBodega = New DevExpress.XtraBars.BarStaticItem()
        Me.BarStaticItem1 = New DevExpress.XtraBars.BarStaticItem()
        Me.txtTipoInterface = New DevExpress.XtraBars.BarStaticItem()
        Me.lblInstancia = New DevExpress.XtraBars.BarStaticItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.rpgInterfaceMenuPrincipal = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        CType(Me.rbMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rbMain
        '
        Me.rbMain.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(39, 36, 39, 36)
        Me.rbMain.ExpandCollapseItem.Id = 0
        Me.rbMain.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.rbMain.ExpandCollapseItem, Me.mnuPrueba, Me.lblNombrePC, Me.lblServerAPP, Me.lblBDAPP, Me.lblEmpresa, Me.lblBodega, Me.BarStaticItem1, Me.txtTipoInterface, Me.lblInstancia})
        Me.rbMain.Location = New System.Drawing.Point(0, 0)
        Me.rbMain.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbMain.MaxItemId = 13
        Me.rbMain.Name = "rbMain"
        Me.rbMain.OptionsMenuMinWidth = 424
        Me.rbMain.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.rbMain.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.rbMain.Size = New System.Drawing.Size(1459, 231)
        Me.rbMain.StatusBar = Me.RibbonStatusBar
        '
        'mnuPrueba
        '
        Me.mnuPrueba.Caption = "Ejecución"
        Me.mnuPrueba.Id = 2
        Me.mnuPrueba.ImageOptions.Image = CType(resources.GetObject("mnuPrueba.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuPrueba.ImageOptions.LargeImage = CType(resources.GetObject("mnuPrueba.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuPrueba.Name = "mnuPrueba"
        '
        'lblNombrePC
        '
        Me.lblNombrePC.Caption = "Host:"
        Me.lblNombrePC.Id = 4
        Me.lblNombrePC.ImageOptions.Image = CType(resources.GetObject("lblNombrePC.ImageOptions.Image"), System.Drawing.Image)
        Me.lblNombrePC.ImageOptions.LargeImage = CType(resources.GetObject("lblNombrePC.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.lblNombrePC.Name = "lblNombrePC"
        Me.lblNombrePC.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'lblServerAPP
        '
        Me.lblServerAPP.Caption = "Server:"
        Me.lblServerAPP.Id = 5
        Me.lblServerAPP.ImageOptions.Image = CType(resources.GetObject("lblServerAPP.ImageOptions.Image"), System.Drawing.Image)
        Me.lblServerAPP.Name = "lblServerAPP"
        Me.lblServerAPP.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'lblBDAPP
        '
        Me.lblBDAPP.Caption = "BD:"
        Me.lblBDAPP.Id = 6
        Me.lblBDAPP.ImageOptions.Image = CType(resources.GetObject("lblBDAPP.ImageOptions.Image"), System.Drawing.Image)
        Me.lblBDAPP.Name = "lblBDAPP"
        Me.lblBDAPP.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'lblEmpresa
        '
        Me.lblEmpresa.Caption = "Empresa:"
        Me.lblEmpresa.Id = 7
        Me.lblEmpresa.ImageOptions.Image = CType(resources.GetObject("lblEmpresa.ImageOptions.Image"), System.Drawing.Image)
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'lblBodega
        '
        Me.lblBodega.Caption = "Bodega:"
        Me.lblBodega.Id = 8
        Me.lblBodega.ImageOptions.Image = CType(resources.GetObject("lblBodega.ImageOptions.Image"), System.Drawing.Image)
        Me.lblBodega.Name = "lblBodega"
        Me.lblBodega.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'BarStaticItem1
        '
        Me.BarStaticItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.BarStaticItem1.Caption = "1.0.3 - 20240124"
        Me.BarStaticItem1.Id = 9
        Me.BarStaticItem1.ImageOptions.Image = CType(resources.GetObject("BarStaticItem1.ImageOptions.Image"), System.Drawing.Image)
        Me.BarStaticItem1.ImageOptions.LargeImage = CType(resources.GetObject("BarStaticItem1.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarStaticItem1.Name = "BarStaticItem1"
        Me.BarStaticItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'txtTipoInterface
        '
        Me.txtTipoInterface.Caption = "Interface SAP Becofarma"
        Me.txtTipoInterface.Id = 11
        Me.txtTipoInterface.Name = "txtTipoInterface"
        '
        'lblInstancia
        '
        Me.lblInstancia.Caption = "Instancia"
        Me.lblInstancia.Id = 12
        Me.lblInstancia.ImageOptions.SvgImage = CType(resources.GetObject("lblInstancia.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.lblInstancia.Name = "lblInstancia"
        Me.lblInstancia.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.rpgInterfaceMenuPrincipal})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menu"
        '
        'rpgInterfaceMenuPrincipal
        '
        Me.rpgInterfaceMenuPrincipal.ItemLinks.Add(Me.mnuPrueba)
        Me.rpgInterfaceMenuPrincipal.Name = "rpgInterfaceMenuPrincipal"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblNombrePC)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblServerAPP)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblBDAPP)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblEmpresa)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblBodega)
        Me.RibbonStatusBar.ItemLinks.Add(Me.BarStaticItem1)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblInstancia)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 643)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.rbMain
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1459, 36)
        '
        'frmMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1459, 679)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.rbMain)
        Me.IsMdiContainer = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmMenu"
        Me.Ribbon = Me.rbMain
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Interface"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.rbMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents rbMain As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents rpgInterfaceMenuPrincipal As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuPrueba As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblNombrePC As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblServerAPP As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblBDAPP As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblEmpresa As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblBodega As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents BarStaticItem1 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents txtTipoInterface As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblInstancia As DevExpress.XtraBars.BarStaticItem
End Class
