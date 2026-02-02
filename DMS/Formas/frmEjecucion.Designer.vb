<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEjecucion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEjecucion))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdProductos = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdIngresos = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSalidas = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdFechaBaseSync = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdLogErrores = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdParametrizacion = New DevExpress.XtraBars.BarButtonItem()
        Me.lblServerAPP = New DevExpress.XtraBars.BarStaticItem()
        Me.lblBDAPP = New DevExpress.XtraBars.BarStaticItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup4 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.lblTLog = New DevExpress.XtraEditors.LabelControl()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.lblprg = New System.Windows.Forms.RichTextBox()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdProductos, Me.BarButtonItem2, Me.cmdIngresos, Me.cmdSalidas, Me.cmdFechaBaseSync, Me.cmdLogErrores, Me.cmdParametrizacion, Me.lblServerAPP, Me.lblBDAPP, Me.BarButtonItem1})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 13
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1104, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdProductos
        '
        Me.cmdProductos.Caption = "Productos"
        Me.cmdProductos.Id = 1
        Me.cmdProductos.ImageOptions.SvgImage = CType(resources.GetObject("cmdProductos.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdProductos.Name = "cmdProductos"
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
        Me.cmdFechaBaseSync.Caption = "Registra Fecha Inicial Sincronización"
        Me.cmdFechaBaseSync.Id = 7
        Me.cmdFechaBaseSync.ImageOptions.SvgImage = CType(resources.GetObject("cmdFechaBaseSync.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdFechaBaseSync.Name = "cmdFechaBaseSync"
        '
        'cmdLogErrores
        '
        Me.cmdLogErrores.Caption = "Log de eventos en exportación"
        Me.cmdLogErrores.Id = 8
        Me.cmdLogErrores.ImageOptions.SvgImage = CType(resources.GetObject("cmdLogErrores.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdLogErrores.Name = "cmdLogErrores"
        '
        'cmdParametrizacion
        '
        Me.cmdParametrizacion.Caption = "Configurar horarios"
        Me.cmdParametrizacion.Id = 9
        Me.cmdParametrizacion.ImageOptions.SvgImage = CType(resources.GetObject("cmdParametrizacion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdParametrizacion.Name = "cmdParametrizacion"
        '
        'lblServerAPP
        '
        Me.lblServerAPP.Caption = "SERVER"
        Me.lblServerAPP.Id = 10
        Me.lblServerAPP.Name = "lblServerAPP"
        Me.lblServerAPP.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'lblBDAPP
        '
        Me.lblBDAPP.Caption = "BD"
        Me.lblBDAPP.Id = 11
        Me.lblBDAPP.Name = "lblBDAPP"
        Me.lblBDAPP.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Reiniciar Fecha Sincronizacion"
        Me.BarButtonItem1.Id = 12
        Me.BarButtonItem1.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2, Me.RibbonPageGroup3, Me.RibbonPageGroup4})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Servicio de Migracion de Datos"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdProductos)
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
        Me.RibbonPageGroup3.ItemLinks.Add(Me.cmdParametrizacion)
        Me.RibbonPageGroup3.ItemLinks.Add(Me.BarButtonItem1)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        Me.RibbonPageGroup3.Text = "Configuración"
        '
        'RibbonPageGroup4
        '
        Me.RibbonPageGroup4.ItemLinks.Add(Me.cmdLogErrores)
        Me.RibbonPageGroup4.Name = "RibbonPageGroup4"
        Me.RibbonPageGroup4.Text = "Control de eventos"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblServerAPP)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblBDAPP)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 524)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1104, 30)
        '
        'lblTLog
        '
        Me.lblTLog.Appearance.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTLog.Appearance.Options.UseFont = True
        Me.lblTLog.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblTLog.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblTLog.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTLog.Location = New System.Drawing.Point(0, 193)
        Me.lblTLog.Margin = New System.Windows.Forms.Padding(4)
        Me.lblTLog.Name = "lblTLog"
        Me.lblTLog.Size = New System.Drawing.Size(1104, 75)
        Me.lblTLog.TabIndex = 2
        Me.lblTLog.Text = "Log"
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Top
        Me.prg.Location = New System.Drawing.Point(0, 268)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(1104, 91)
        Me.prg.TabIndex = 5
        '
        'lblprg
        '
        Me.lblprg.BackColor = System.Drawing.Color.OldLace
        Me.lblprg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblprg.Location = New System.Drawing.Point(0, 359)
        Me.lblprg.Margin = New System.Windows.Forms.Padding(4)
        Me.lblprg.Name = "lblprg"
        Me.lblprg.Size = New System.Drawing.Size(1104, 165)
        Me.lblprg.TabIndex = 6
        Me.lblprg.Text = ""
        '
        'frmEjecucion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1104, 554)
        Me.Controls.Add(Me.lblprg)
        Me.Controls.Add(Me.prg)
        Me.Controls.Add(Me.lblTLog)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmEjecucion"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Exportación hacia la nube"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdProductos As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmdIngresos As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblTLog As DevExpress.XtraEditors.LabelControl
    Friend WithEvents prg As ProgressBar
    Friend WithEvents lblprg As RichTextBox
    Friend WithEvents cmdSalidas As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdFechaBaseSync As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPageGroup4 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmdLogErrores As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdParametrizacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblServerAPP As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblBDAPP As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
End Class
