<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEjecucion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEjecucion))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdProductos = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdPropietarios = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdIngresos = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSalidas = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdFechaBaseSync = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.lblprg = New System.Windows.Forms.RichTextBox()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdProductos, Me.cmdPropietarios, Me.BarButtonItem2, Me.cmdIngresos, Me.cmdSalidas, Me.cmdFechaBaseSync})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 8
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(1370, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdProductos
        '
        Me.cmdProductos.Caption = "Productos"
        Me.cmdProductos.Id = 1
        Me.cmdProductos.ImageOptions.SvgImage = CType(resources.GetObject("cmdProductos.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdProductos.Name = "cmdProductos"
        '
        'cmdPropietarios
        '
        Me.cmdPropietarios.Caption = "Propietarios"
        Me.cmdPropietarios.Id = 2
        Me.cmdPropietarios.ImageOptions.SvgImage = CType(resources.GetObject("cmdPropietarios.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdPropietarios.Name = "cmdPropietarios"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Id = 4
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'cmdIngresos
        '
        Me.cmdIngresos.Caption = "Ingresos"
        Me.cmdIngresos.Id = 5
        Me.cmdIngresos.ImageOptions.SvgImage = CType(resources.GetObject("cmdIngresos.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdIngresos.Name = "cmdIngresos"
        '
        'cmdSalidas
        '
        Me.cmdSalidas.Caption = "Salidas"
        Me.cmdSalidas.Id = 6
        Me.cmdSalidas.ImageOptions.SvgImage = CType(resources.GetObject("cmdSalidas.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdSalidas.Name = "cmdSalidas"
        '
        'cmdFechaBaseSync
        '
        Me.cmdFechaBaseSync.Caption = "Registrar Fecha Base Sync"
        Me.cmdFechaBaseSync.Id = 7
        Me.cmdFechaBaseSync.ImageOptions.SvgImage = CType(resources.GetObject("cmdFechaBaseSync.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdFechaBaseSync.Name = "cmdFechaBaseSync"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2, Me.RibbonPageGroup3})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Servicio de Migracion de Datos"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdProductos)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdPropietarios)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        Me.RibbonPageGroup1.Text = "Catálogos"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.cmdIngresos)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.cmdSalidas)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        Me.RibbonPageGroup2.Text = "Transacciones"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.cmdFechaBaseSync)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 620)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1370, 30)
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Top
        Me.prg.Location = New System.Drawing.Point(0, 193)
        Me.prg.Margin = New System.Windows.Forms.Padding(4)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(1370, 29)
        Me.prg.TabIndex = 2
        Me.prg.Visible = False
        '
        'lblprg
        '
        Me.lblprg.BackColor = System.Drawing.Color.OldLace
        Me.lblprg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblprg.Location = New System.Drawing.Point(0, 222)
        Me.lblprg.Margin = New System.Windows.Forms.Padding(4)
        Me.lblprg.Name = "lblprg"
        Me.lblprg.Size = New System.Drawing.Size(1370, 398)
        Me.lblprg.TabIndex = 3
        Me.lblprg.Text = ""
        '
        'frmEjecucion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1370, 650)
        Me.Controls.Add(Me.lblprg)
        Me.Controls.Add(Me.prg)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmEjecucion"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "frmEjecucion"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdProductos As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdPropietarios As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmdIngresos As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdSalidas As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents prg As ProgressBar
    Friend WithEvents lblprg As RichTextBox
    Friend WithEvents cmdFechaBaseSync As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
End Class
