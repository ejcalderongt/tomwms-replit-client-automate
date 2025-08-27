<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImpresiones
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImpresiones))
        Me.rbMain = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.lblNombrePC = New DevExpress.XtraBars.BarStaticItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.lblServerAPP = New DevExpress.XtraBars.BarStaticItem()
        Me.lblBDAPP = New DevExpress.XtraBars.BarStaticItem()
        Me.lblEmpresa = New DevExpress.XtraBars.BarStaticItem()
        Me.lblBodega = New DevExpress.XtraBars.BarHeaderItem()
        Me.BarStaticItem1 = New DevExpress.XtraBars.BarStaticItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.txtLog = New System.Windows.Forms.RichTextBox()
        CType(Me.rbMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rbMain
        '
        Me.rbMain.ExpandCollapseItem.Id = 0
        Me.rbMain.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.rbMain.ExpandCollapseItem, Me.lblNombrePC, Me.cmdImprimir, Me.cmdSalir, Me.lblServerAPP, Me.lblBDAPP, Me.lblEmpresa, Me.lblBodega, Me.BarStaticItem1})
        Me.rbMain.Location = New System.Drawing.Point(0, 0)
        Me.rbMain.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.rbMain.MaxItemId = 9
        Me.rbMain.Name = "rbMain"
        Me.rbMain.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.rbMain.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.rbMain.Size = New System.Drawing.Size(883, 193)
        Me.rbMain.StatusBar = Me.RibbonStatusBar
        '
        'lblNombrePC
        '
        Me.lblNombrePC.Caption = "Host:"
        Me.lblNombrePC.Id = 1
        Me.lblNombrePC.ImageOptions.Image = CType(resources.GetObject("lblNombrePC.ImageOptions.Image"), System.Drawing.Image)
        Me.lblNombrePC.ImageOptions.LargeImage = CType(resources.GetObject("lblNombrePC.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.lblNombrePC.Name = "lblNombrePC"
        Me.lblNombrePC.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 2
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'cmdSalir
        '
        Me.cmdSalir.Caption = "Salir"
        Me.cmdSalir.Id = 3
        Me.cmdSalir.ImageOptions.SvgImage = CType(resources.GetObject("cmdSalir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdSalir.Name = "cmdSalir"
        '
        'lblServerAPP
        '
        Me.lblServerAPP.Caption = "Server:"
        Me.lblServerAPP.Id = 4
        Me.lblServerAPP.ImageOptions.Image = CType(resources.GetObject("lblServerAPP.ImageOptions.Image"), System.Drawing.Image)
        Me.lblServerAPP.ImageOptions.LargeImage = CType(resources.GetObject("lblServerAPP.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.lblServerAPP.Name = "lblServerAPP"
        Me.lblServerAPP.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'lblBDAPP
        '
        Me.lblBDAPP.Caption = "BD:"
        Me.lblBDAPP.Id = 5
        Me.lblBDAPP.ImageOptions.Image = CType(resources.GetObject("lblBDAPP.ImageOptions.Image"), System.Drawing.Image)
        Me.lblBDAPP.ImageOptions.LargeImage = CType(resources.GetObject("lblBDAPP.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.lblBDAPP.Name = "lblBDAPP"
        Me.lblBDAPP.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'lblEmpresa
        '
        Me.lblEmpresa.Caption = "Empresa:"
        Me.lblEmpresa.Id = 6
        Me.lblEmpresa.ImageOptions.Image = CType(resources.GetObject("lblEmpresa.ImageOptions.Image"), System.Drawing.Image)
        Me.lblEmpresa.ImageOptions.LargeImage = CType(resources.GetObject("lblEmpresa.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'lblBodega
        '
        Me.lblBodega.Caption = "Bodega:"
        Me.lblBodega.Id = 7
        Me.lblBodega.ImageOptions.Image = CType(resources.GetObject("lblBodega.ImageOptions.Image"), System.Drawing.Image)
        Me.lblBodega.ImageOptions.LargeImage = CType(resources.GetObject("lblBodega.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.lblBodega.Name = "lblBodega"
        '
        'BarStaticItem1
        '
        Me.BarStaticItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.BarStaticItem1.Caption = "Versión 4.0 FP: 20190924"
        Me.BarStaticItem1.Id = 8
        Me.BarStaticItem1.ItemAppearance.Normal.BackColor = System.Drawing.Color.Gold
        Me.BarStaticItem1.ItemAppearance.Normal.Options.UseBackColor = True
        Me.BarStaticItem1.Name = "BarStaticItem1"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Impresión de códigos de barra"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblNombrePC)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblServerAPP)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblBDAPP)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblEmpresa)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblBodega)
        Me.RibbonStatusBar.ItemLinks.Add(Me.BarStaticItem1)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 667)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.rbMain
        Me.RibbonStatusBar.Size = New System.Drawing.Size(883, 30)
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 3000
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.BalloonTipTitle = "WMS: Servicio de impresión"
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "WMS: Servicio de impresión"
        Me.NotifyIcon1.Visible = True
        '
        'txtLog
        '
        Me.txtLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLog.Location = New System.Drawing.Point(0, 193)
        Me.txtLog.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLog.Name = "txtLog"
        Me.txtLog.Size = New System.Drawing.Size(883, 474)
        Me.txtLog.TabIndex = 2
        Me.txtLog.Text = ""
        '
        'frmImpresiones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(883, 697)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.rbMain)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmImpresiones"
        Me.Ribbon = Me.rbMain
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Impresión de códigos de barra"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.rbMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents rbMain As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblNombrePC As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblServerAPP As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblBDAPP As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblEmpresa As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblBodega As DevExpress.XtraBars.BarHeaderItem
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents txtLog As RichTextBox
    Friend WithEvents BarStaticItem1 As DevExpress.XtraBars.BarStaticItem
End Class
