<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmmenu
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
        Me.xtMdi = New DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(Me.components)
        Me.rbMain = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdSucursal = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdProducto = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImpresionRapida = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.lblNombrePCCliente = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdVersion4 = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImpresora = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpTransacciones = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar1 = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        CType(Me.xtMdi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtMdi
        '
        Me.xtMdi.MdiParent = Me
        '
        'rbMain
        '
        Me.rbMain.ExpandCollapseItem.Id = 0
        Me.rbMain.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.rbMain.ExpandCollapseItem, Me.cmdSucursal, Me.cmdProducto, Me.cmdImpresionRapida, Me.BarButtonItem1, Me.lblNombrePCCliente, Me.cmdVersion4, Me.cmdImpresora})
        Me.rbMain.Location = New System.Drawing.Point(0, 0)
        Me.rbMain.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.rbMain.MaxItemId = 8
        Me.rbMain.Name = "rbMain"
        Me.rbMain.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1, Me.rpTransacciones})
        Me.rbMain.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.rbMain.Size = New System.Drawing.Size(923, 193)
        Me.rbMain.StatusBar = Me.RibbonStatusBar1
        '
        'cmdSucursal
        '
        Me.cmdSucursal.Caption = "Sucursales"
        Me.cmdSucursal.Id = 1
        Me.cmdSucursal.Name = "cmdSucursal"
        '
        'cmdProducto
        '
        Me.cmdProducto.Caption = "Productos"
        Me.cmdProducto.Id = 2
        Me.cmdProducto.Name = "cmdProducto"
        '
        'cmdImpresionRapida
        '
        Me.cmdImpresionRapida.Caption = "Impresion Rapida"
        Me.cmdImpresionRapida.Id = 3
        Me.cmdImpresionRapida.Name = "cmdImpresionRapida"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "BarButtonItem1"
        Me.BarButtonItem1.Id = 4
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'lblNombrePCCliente
        '
        Me.lblNombrePCCliente.Caption = "PC"
        Me.lblNombrePCCliente.Id = 5
        Me.lblNombrePCCliente.Name = "lblNombrePCCliente"
        '
        'cmdVersion4
        '
        Me.cmdVersion4.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.cmdVersion4.Caption = "Versión 8.0.0 FP: 20260528"
        Me.cmdVersion4.Id = 6
        Me.cmdVersion4.ItemAppearance.Normal.BackColor = System.Drawing.Color.MistyRose
        Me.cmdVersion4.ItemAppearance.Normal.Options.UseBackColor = True
        Me.cmdVersion4.Name = "cmdVersion4"
        '
        'cmdImpresora
        '
        Me.cmdImpresora.Caption = "Impresora"
        Me.cmdImpresora.Id = 7
        Me.cmdImpresora.Name = "cmdImpresora"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup3})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Catálogos"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.cmdSucursal)
        Me.RibbonPageGroup3.ItemLinks.Add(Me.cmdProducto)
        Me.RibbonPageGroup3.ItemLinks.Add(Me.cmdImpresora)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'rpTransacciones
        '
        Me.rpTransacciones.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.rpTransacciones.Name = "rpTransacciones"
        Me.rpTransacciones.Text = "Transacciones"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImpresionRapida)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar1
        '
        Me.RibbonStatusBar1.ItemLinks.Add(Me.lblNombrePCCliente)
        Me.RibbonStatusBar1.ItemLinks.Add(Me.cmdVersion4)
        Me.RibbonStatusBar1.Location = New System.Drawing.Point(0, 515)
        Me.RibbonStatusBar1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.RibbonStatusBar1.Name = "RibbonStatusBar1"
        Me.RibbonStatusBar1.Ribbon = Me.rbMain
        Me.RibbonStatusBar1.Size = New System.Drawing.Size(923, 30)
        '
        'frmmenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(923, 545)
        Me.Controls.Add(Me.RibbonStatusBar1)
        Me.Controls.Add(Me.rbMain)
        Me.IsMdiContainer = True
        Me.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.Name = "frmmenu"
        Me.Ribbon = Me.rbMain
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.StatusBar = Me.RibbonStatusBar1
        Me.Text = "Menu Principal"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.xtMdi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents xtMdi As DevExpress.XtraTabbedMdi.XtraTabbedMdiManager
    Friend WithEvents rbMain As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmdSucursal As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdProducto As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImpresionRapida As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rpTransacciones As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonStatusBar1 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents lblNombrePCCliente As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdVersion4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImpresora As DevExpress.XtraBars.BarButtonItem
End Class
