<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMov_Reporte
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMov_Reporte))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuGuardarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.OpcionesLista = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RepositoryItemProgressBar1 = New DevExpress.XtraEditors.Repository.RepositoryItemProgressBar()
        Me.RepositoryItemProgressBar2 = New DevExpress.XtraEditors.Repository.RepositoryItemProgressBar()
        Me.RepositoryItemMarqueeProgressBar1 = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.RepositoryItemProgressBar3 = New DevExpress.XtraEditors.Repository.RepositoryItemProgressBar()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.dgrid = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grpFechas = New DevExpress.XtraEditors.GroupControl()
        Me.lblLote = New System.Windows.Forms.Label()
        Me.txtLote = New System.Windows.Forms.TextBox()
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
        Me.lblDel = New System.Windows.Forms.Label()
        Me.dtpFechaDesde = New System.Windows.Forms.DateTimePicker()
        Me.prg = New DevExpress.XtraEditors.ProgressBarControl()
        Me.sf = New System.Windows.Forms.SaveFileDialog()
        Me.chkSoloProductosConStock = New DevExpress.XtraBars.BarToggleSwitchItem()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemProgressBar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMarqueeProgressBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemProgressBar3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpFechas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFechas.SuspendLayout()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdImprimir, Me.cmdActualizar, Me.BarButtonItem3, Me.lblRegs, Me.BarButtonItem1, Me.mnuEliminarLayoutGrid, Me.mnuGuardarLayoutGrid, Me.chkSoloProductosConStock})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 13
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.OpcionesLista})
        Me.RibbonControl.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemProgressBar1, Me.RepositoryItemProgressBar2, Me.RepositoryItemMarqueeProgressBar1, Me.RepositoryItemProgressBar3})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1255, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 1
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Caption = "Generar"
        Me.cmdActualizar.Id = 2
        Me.cmdActualizar.ImageOptions.SvgImage = CType(resources.GetObject("cmdActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizar.Name = "cmdActualizar"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Salir"
        Me.BarButtonItem3.Id = 3
        Me.BarButtonItem3.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem3.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Regs: 0"
        Me.lblRegs.Id = 4
        Me.lblRegs.Name = "lblRegs"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Exportar Excel"
        Me.BarButtonItem1.Id = 9
        Me.BarButtonItem1.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'mnuEliminarLayoutGrid
        '
        Me.mnuEliminarLayoutGrid.Caption = "Eliminar diseño grid"
        Me.mnuEliminarLayoutGrid.Id = 10
        Me.mnuEliminarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminarLayoutGrid.Name = "mnuEliminarLayoutGrid"
        '
        'mnuGuardarLayoutGrid
        '
        Me.mnuGuardarLayoutGrid.Caption = "Guardar diseño de grid"
        Me.mnuGuardarLayoutGrid.Id = 11
        Me.mnuGuardarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardarLayoutGrid.Name = "mnuGuardarLayoutGrid"
        '
        'OpcionesLista
        '
        Me.OpcionesLista.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.OpcionesLista.Name = "OpcionesLista"
        Me.OpcionesLista.Text = "Stock en una fecha"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarButtonItem1)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardarLayoutGrid)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminarLayoutGrid)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarButtonItem3)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.chkSoloProductosConStock)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RepositoryItemProgressBar1
        '
        Me.RepositoryItemProgressBar1.Name = "RepositoryItemProgressBar1"
        '
        'RepositoryItemProgressBar2
        '
        Me.RepositoryItemProgressBar2.Name = "RepositoryItemProgressBar2"
        '
        'RepositoryItemMarqueeProgressBar1
        '
        Me.RepositoryItemMarqueeProgressBar1.Name = "RepositoryItemMarqueeProgressBar1"
        '
        'RepositoryItemProgressBar3
        '
        Me.RepositoryItemProgressBar3.Maximum = 10000
        Me.RepositoryItemProgressBar3.Name = "RepositoryItemProgressBar3"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 747)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1255, 30)
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
        Me.dgrid.Size = New System.Drawing.Size(951, 532)
        Me.dgrid.TabIndex = 0
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
        'grpFechas
        '
        Me.grpFechas.Controls.Add(Me.lblLote)
        Me.grpFechas.Controls.Add(Me.txtLote)
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
        Me.grpFechas.Controls.Add(Me.lblDel)
        Me.grpFechas.Controls.Add(Me.dtpFechaDesde)
        Me.grpFechas.Dock = System.Windows.Forms.DockStyle.Left
        Me.grpFechas.Location = New System.Drawing.Point(0, 193)
        Me.grpFechas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpFechas.Name = "grpFechas"
        Me.grpFechas.Size = New System.Drawing.Size(304, 554)
        Me.grpFechas.TabIndex = 3
        Me.grpFechas.Text = "Filtros"
        '
        'lblLote
        '
        Me.lblLote.AutoSize = True
        Me.lblLote.Location = New System.Drawing.Point(14, 216)
        Me.lblLote.Name = "lblLote"
        Me.lblLote.Size = New System.Drawing.Size(36, 16)
        Me.lblLote.TabIndex = 11
        Me.lblLote.Text = "Lote:"
        '
        'txtLote
        '
        Me.txtLote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLote.Location = New System.Drawing.Point(14, 234)
        Me.txtLote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLote.Name = "txtLote"
        Me.txtLote.Size = New System.Drawing.Size(283, 23)
        Me.txtLote.TabIndex = 10
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
        Me.Label1.Location = New System.Drawing.Point(14, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 16)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Bodega:"
        '
        'lblPrg
        '
        Me.lblPrg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPrg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblPrg.Location = New System.Drawing.Point(2, 440)
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
        Me.lblPropietario.Location = New System.Drawing.Point(14, 106)
        Me.lblPropietario.Name = "lblPropietario"
        Me.lblPropietario.Size = New System.Drawing.Size(74, 16)
        Me.lblPropietario.TabIndex = 6
        Me.lblPropietario.Text = "Propietario:"
        '
        'txtNombreProducto
        '
        Me.txtNombreProducto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNombreProducto.Location = New System.Drawing.Point(105, 185)
        Me.txtNombreProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreProducto.Name = "txtNombreProducto"
        Me.txtNombreProducto.Size = New System.Drawing.Size(192, 23)
        Me.txtNombreProducto.TabIndex = 6
        '
        'txtIdProducto
        '
        Me.txtIdProducto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdProducto.Location = New System.Drawing.Point(14, 185)
        Me.txtIdProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdProducto.Name = "txtIdProducto"
        Me.txtIdProducto.Size = New System.Drawing.Size(84, 23)
        Me.txtIdProducto.TabIndex = 6
        '
        'lblProducto
        '
        Me.lblProducto.AutoSize = True
        Me.lblProducto.Location = New System.Drawing.Point(14, 164)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(57, 16)
        Me.lblProducto.TabIndex = 6
        Me.lblProducto.TabStop = True
        Me.lblProducto.Text = "Producto"
        '
        'dtpfechaHasta
        '
        Me.dtpfechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfechaHasta.Location = New System.Drawing.Point(49, 350)
        Me.dtpfechaHasta.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpfechaHasta.Name = "dtpfechaHasta"
        Me.dtpfechaHasta.Size = New System.Drawing.Size(233, 23)
        Me.dtpfechaHasta.TabIndex = 4
        '
        'lblAl
        '
        Me.lblAl.AutoSize = True
        Me.lblAl.Location = New System.Drawing.Point(12, 350)
        Me.lblAl.Name = "lblAl"
        Me.lblAl.Size = New System.Drawing.Size(23, 16)
        Me.lblAl.TabIndex = 3
        Me.lblAl.Text = "Al:"
        '
        'lblDel
        '
        Me.lblDel.AutoSize = True
        Me.lblDel.Location = New System.Drawing.Point(12, 314)
        Me.lblDel.Name = "lblDel"
        Me.lblDel.Size = New System.Drawing.Size(30, 16)
        Me.lblDel.TabIndex = 3
        Me.lblDel.Text = "Del:"
        '
        'dtpFechaDesde
        '
        Me.dtpFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaDesde.Location = New System.Drawing.Point(49, 306)
        Me.dtpFechaDesde.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpFechaDesde.Name = "dtpFechaDesde"
        Me.dtpFechaDesde.Size = New System.Drawing.Size(233, 23)
        Me.dtpFechaDesde.TabIndex = 3
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.prg.Location = New System.Drawing.Point(304, 725)
        Me.prg.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.prg.MenuManager = Me.RibbonControl
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(951, 22)
        Me.prg.TabIndex = 6
        '
        'sf
        '
        Me.sf.CheckFileExists = True
        '
        'chkSoloProductosConStock
        '
        Me.chkSoloProductosConStock.BindableChecked = True
        Me.chkSoloProductosConStock.Caption = "Incluir solo artículos con stock actual"
        Me.chkSoloProductosConStock.Checked = True
        Me.chkSoloProductosConStock.Id = 12
        Me.chkSoloProductosConStock.Name = "chkSoloProductosConStock"
        '
        'frmMov_Reporte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1255, 777)
        Me.Controls.Add(Me.dgrid)
        Me.Controls.Add(Me.prg)
        Me.Controls.Add(Me.grpFechas)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmMov_Reporte"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Stock en una fecha"
        CType(Me.RibbonControl,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemProgressBar1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemProgressBar2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemMarqueeProgressBar1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemProgressBar3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dgrid,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grpFechas,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpFechas.ResumeLayout(false)
        Me.grpFechas.PerformLayout
        CType(Me.cmbBodega.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cmbPropietario.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.prg.Properties,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents OpcionesLista As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents dgrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grpFechas As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtpFechaDesde As DateTimePicker
    Friend WithEvents lblAl As Label
    Friend WithEvents lblDel As Label
    Friend WithEvents dtpfechaHasta As DateTimePicker
    Friend WithEvents txtNombreProducto As TextBox
    Friend WithEvents txtIdProducto As TextBox
    Friend WithEvents lblProducto As LinkLabel
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblPropietario As Label
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RepositoryItemProgressBar2 As DevExpress.XtraEditors.Repository.RepositoryItemProgressBar
    Friend WithEvents RepositoryItemProgressBar1 As DevExpress.XtraEditors.Repository.RepositoryItemProgressBar
    Friend WithEvents RepositoryItemMarqueeProgressBar1 As DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar
    Friend WithEvents RepositoryItemProgressBar3 As DevExpress.XtraEditors.Repository.RepositoryItemProgressBar
    Friend WithEvents prg As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents lblPrg As Label
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents sf As SaveFileDialog
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label1 As Label
    Friend WithEvents mnuEliminarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuGuardarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblLote As Label
    Friend WithEvents txtLote As TextBox
    Friend WithEvents chkSoloProductosConStock As DevExpress.XtraBars.BarToggleSwitchItem
End Class
