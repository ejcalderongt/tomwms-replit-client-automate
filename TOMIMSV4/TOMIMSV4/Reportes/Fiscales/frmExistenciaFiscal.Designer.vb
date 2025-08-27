<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExistenciaFiscal
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExistenciaFiscal))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuExportarExc = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuGuardarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.lblReg = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtNombreProducto = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdProducto = New DevExpress.XtraEditors.TextEdit()
        Me.linklblProducto = New System.Windows.Forms.LinkLabel()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dgridStockFiscal = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtNombreProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgridStockFiscal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuActualizar, Me.mnuImprimir, Me.mnuExportarExc, Me.mnuSalir, Me.mnuGuardarLayoutGrid, Me.mnuEliminarLayoutGrid, Me.lblReg})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 8
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1164, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 1
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuImprimir
        '
        Me.mnuImprimir.Caption = "Imprimir"
        Me.mnuImprimir.Id = 2
        Me.mnuImprimir.ImageOptions.SvgImage = CType(resources.GetObject("mnuImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuImprimir.Name = "mnuImprimir"
        '
        'mnuExportarExc
        '
        Me.mnuExportarExc.Caption = "Exportar"
        Me.mnuExportarExc.Id = 3
        Me.mnuExportarExc.ImageOptions.SvgImage = CType(resources.GetObject("mnuExportarExc.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuExportarExc.Name = "mnuExportarExc"
        '
        'mnuSalir
        '
        Me.mnuSalir.Caption = "Salir"
        Me.mnuSalir.Id = 4
        Me.mnuSalir.ImageOptions.SvgImage = CType(resources.GetObject("mnuSalir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuSalir.Name = "mnuSalir"
        '
        'mnuGuardarLayoutGrid
        '
        Me.mnuGuardarLayoutGrid.Caption = "Guardar diseño"
        Me.mnuGuardarLayoutGrid.Id = 5
        Me.mnuGuardarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardarLayoutGrid.Name = "mnuGuardarLayoutGrid"
        '
        'mnuEliminarLayoutGrid
        '
        Me.mnuEliminarLayoutGrid.Caption = "Eliminar diseño"
        Me.mnuEliminarLayoutGrid.Id = 6
        Me.mnuEliminarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminarLayoutGrid.Name = "mnuEliminarLayoutGrid"
        '
        'lblReg
        '
        Me.lblReg.Caption = "Registros:0"
        Me.lblReg.Id = 7
        Me.lblReg.Name = "lblReg"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Existencia Fiscal"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuExportarExc)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuSalir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardarLayoutGrid)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminarLayoutGrid)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblReg)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 639)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1164, 30)
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtNombreProducto)
        Me.GroupBox1.Controls.Add(Me.txtIdProducto)
        Me.GroupBox1.Controls.Add(Me.linklblProducto)
        Me.GroupBox1.Controls.Add(Me.cmbPropietario)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox1.Location = New System.Drawing.Point(0, 193)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(327, 446)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filtros"
        '
        'txtNombreProducto
        '
        Me.txtNombreProducto.Location = New System.Drawing.Point(101, 152)
        Me.txtNombreProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreProducto.MenuManager = Me.RibbonControl
        Me.txtNombreProducto.Name = "txtNombreProducto"
        Me.txtNombreProducto.Size = New System.Drawing.Size(218, 22)
        Me.txtNombreProducto.TabIndex = 6
        '
        'txtIdProducto
        '
        Me.txtIdProducto.Location = New System.Drawing.Point(3, 152)
        Me.txtIdProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdProducto.MenuManager = Me.RibbonControl
        Me.txtIdProducto.Name = "txtIdProducto"
        Me.txtIdProducto.Size = New System.Drawing.Size(91, 22)
        Me.txtIdProducto.TabIndex = 5
        '
        'linklblProducto
        '
        Me.linklblProducto.AutoSize = True
        Me.linklblProducto.Location = New System.Drawing.Point(3, 111)
        Me.linklblProducto.Name = "linklblProducto"
        Me.linklblProducto.Size = New System.Drawing.Size(57, 16)
        Me.linklblProducto.TabIndex = 4
        Me.linklblProducto.TabStop = True
        Me.linklblProducto.Text = "Producto"
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Location = New System.Drawing.Point(7, 64)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPropietario.MenuManager = Me.RibbonControl
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Size = New System.Drawing.Size(313, 22)
        Me.cmbPropietario.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Propietario:"
        '
        'dgridStockFiscal
        '
        Me.dgridStockFiscal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridStockFiscal.Location = New System.Drawing.Point(327, 193)
        Me.dgridStockFiscal.MainView = Me.GridView1
        Me.dgridStockFiscal.Name = "dgridStockFiscal"
        Me.dgridStockFiscal.Size = New System.Drawing.Size(837, 446)
        Me.dgridStockFiscal.TabIndex = 8
        Me.dgridStockFiscal.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.dgridStockFiscal
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        '
        'frmExistenciaFiscal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1164, 669)
        Me.Controls.Add(Me.dgridStockFiscal)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmExistenciaFiscal"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Existencias Fiscales"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtNombreProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgridStockFiscal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuExportarExc As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuGuardarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblReg As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtNombreProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents linklblProducto As LinkLabel
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents dgridStockFiscal As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
End Class
