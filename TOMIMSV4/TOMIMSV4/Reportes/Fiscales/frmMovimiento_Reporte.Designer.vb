<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMovimiento_Reporte
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMovimiento_Reporte))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImpExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grpFechas = New DevExpress.XtraEditors.GroupControl()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPrg = New System.Windows.Forms.Label()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblPropietario = New System.Windows.Forms.Label()
        Me.txtNombreProducto = New System.Windows.Forms.TextBox()
        Me.txtIdProducto = New System.Windows.Forms.TextBox()
        Me.lblProducto = New System.Windows.Forms.LinkLabel()
        Me.dtpfechaHasta = New System.Windows.Forms.DateTimePicker()
        Me.lblAl = New System.Windows.Forms.Label()
        Me.dgrid = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.prg = New DevExpress.XtraEditors.ProgressBarControl()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpFechas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFechas.SuspendLayout()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.cmdActualizar, Me.cmdImprimir, Me.cmdSalir, Me.cmdImpExcel, Me.lblRegs})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 6
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1294, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Caption = "Generar"
        Me.cmdActualizar.Id = 1
        Me.cmdActualizar.ImageOptions.SvgImage = CType(resources.GetObject("cmdActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizar.Name = "cmdActualizar"
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
        'cmdImpExcel
        '
        Me.cmdImpExcel.Caption = "Exportar Excel"
        Me.cmdImpExcel.Id = 4
        Me.cmdImpExcel.ImageOptions.SvgImage = CType(resources.GetObject("cmdImpExcel.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImpExcel.Name = "cmdImpExcel"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Regs: 0"
        Me.lblRegs.Id = 5
        Me.lblRegs.Name = "lblRegs"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Inventario para Retroactivo"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImpExcel)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        Me.RibbonPageGroup1.Text = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 726)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1294, 30)
        '
        'grpFechas
        '
        Me.grpFechas.Controls.Add(Me.cmbBodega)
        Me.grpFechas.Controls.Add(Me.Label1)
        Me.grpFechas.Controls.Add(Me.lblPrg)
        Me.grpFechas.Controls.Add(Me.cmbPropietario)
        Me.grpFechas.Controls.Add(Me.lblPropietario)
        Me.grpFechas.Controls.Add(Me.txtNombreProducto)
        Me.grpFechas.Controls.Add(Me.txtIdProducto)
        Me.grpFechas.Controls.Add(Me.lblProducto)
        Me.grpFechas.Controls.Add(Me.dtpfechaHasta)
        Me.grpFechas.Controls.Add(Me.lblAl)
        Me.grpFechas.Dock = System.Windows.Forms.DockStyle.Left
        Me.grpFechas.Location = New System.Drawing.Point(0, 193)
        Me.grpFechas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpFechas.Name = "grpFechas"
        Me.grpFechas.Size = New System.Drawing.Size(304, 533)
        Me.grpFechas.TabIndex = 4
        Me.grpFechas.Text = "Filtros"
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(14, 63)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbBodega.MenuManager = Me.RibbonControl
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(283, 22)
        Me.cmbBodega.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 17)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Bodega:"
        '
        'lblPrg
        '
        Me.lblPrg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPrg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblPrg.Location = New System.Drawing.Point(2, 419)
        Me.lblPrg.Name = "lblPrg"
        Me.lblPrg.Size = New System.Drawing.Size(300, 112)
        Me.lblPrg.TabIndex = 7
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Location = New System.Drawing.Point(14, 126)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPropietario.MenuManager = Me.RibbonControl
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Size = New System.Drawing.Size(283, 22)
        Me.cmbPropietario.TabIndex = 6
        '
        'lblPropietario
        '
        Me.lblPropietario.AutoSize = True
        Me.lblPropietario.Location = New System.Drawing.Point(14, 102)
        Me.lblPropietario.Name = "lblPropietario"
        Me.lblPropietario.Size = New System.Drawing.Size(78, 17)
        Me.lblPropietario.TabIndex = 6
        Me.lblPropietario.Text = "Propietario:"
        '
        'txtNombreProducto
        '
        Me.txtNombreProducto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNombreProducto.Location = New System.Drawing.Point(105, 223)
        Me.txtNombreProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreProducto.Name = "txtNombreProducto"
        Me.txtNombreProducto.Size = New System.Drawing.Size(192, 23)
        Me.txtNombreProducto.TabIndex = 6
        '
        'txtIdProducto
        '
        Me.txtIdProducto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdProducto.Location = New System.Drawing.Point(14, 223)
        Me.txtIdProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdProducto.Name = "txtIdProducto"
        Me.txtIdProducto.Size = New System.Drawing.Size(84, 23)
        Me.txtIdProducto.TabIndex = 6
        '
        'lblProducto
        '
        Me.lblProducto.AutoSize = True
        Me.lblProducto.Location = New System.Drawing.Point(14, 192)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(65, 17)
        Me.lblProducto.TabIndex = 6
        Me.lblProducto.TabStop = True
        Me.lblProducto.Text = "Producto"
        '
        'dtpfechaHasta
        '
        Me.dtpfechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfechaHasta.Location = New System.Drawing.Point(49, 293)
        Me.dtpfechaHasta.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpfechaHasta.Name = "dtpfechaHasta"
        Me.dtpfechaHasta.Size = New System.Drawing.Size(233, 23)
        Me.dtpfechaHasta.TabIndex = 4
        '
        'lblAl
        '
        Me.lblAl.AutoSize = True
        Me.lblAl.Location = New System.Drawing.Point(12, 293)
        Me.lblAl.Name = "lblAl"
        Me.lblAl.Size = New System.Drawing.Size(23, 17)
        Me.lblAl.TabIndex = 3
        Me.lblAl.Text = "Al:"
        '
        'dgrid
        '
        Me.dgrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrid.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.RelationName = "Level1"
        Me.dgrid.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.dgrid.Location = New System.Drawing.Point(304, 193)
        Me.dgrid.MainView = Me.GridView1
        Me.dgrid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgrid.MenuManager = Me.RibbonControl
        Me.dgrid.Name = "dgrid"
        Me.dgrid.Size = New System.Drawing.Size(990, 533)
        Me.dgrid.TabIndex = 5
        Me.dgrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.dgrid
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowFooter = True
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.prg.Location = New System.Drawing.Point(304, 704)
        Me.prg.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.prg.MenuManager = Me.RibbonControl
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(990, 22)
        Me.prg.TabIndex = 7
        '
        'frmMovimiento_Reporte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1294, 756)
        Me.Controls.Add(Me.prg)
        Me.Controls.Add(Me.dgrid)
        Me.Controls.Add(Me.grpFechas)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmMovimiento_Reporte"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Reporte Movimiento Retroactivo"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpFechas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFechas.ResumeLayout(False)
        Me.grpFechas.PerformLayout()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents grpFechas As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label1 As Label
    Friend WithEvents lblPrg As Label
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblPropietario As Label
    Friend WithEvents txtNombreProducto As TextBox
    Friend WithEvents txtIdProducto As TextBox
    Friend WithEvents lblProducto As LinkLabel
    Friend WithEvents dtpfechaHasta As DateTimePicker
    Friend WithEvents lblAl As Label
    Friend WithEvents dgrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImpExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents prg As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
End Class
