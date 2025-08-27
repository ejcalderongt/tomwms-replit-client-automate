<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAuditoriaPicking
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAuditoriaPicking))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode3 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.cmdExToExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuGuardarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.OpcionesLista = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RepositoryItemProgressBar1 = New DevExpress.XtraEditors.Repository.RepositoryItemProgressBar()
        Me.RepositoryItemProgressBar2 = New DevExpress.XtraEditors.Repository.RepositoryItemProgressBar()
        Me.RepositoryItemMarqueeProgressBar1 = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.RepositoryItemProgressBar3 = New DevExpress.XtraEditors.Repository.RepositoryItemProgressBar()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.dgAuditoriaPicking = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grpFechas = New DevExpress.XtraEditors.GroupControl()
        Me.lblPrg = New System.Windows.Forms.Label()
        Me.dtpfechaHasta = New System.Windows.Forms.DateTimePicker()
        Me.lblAl = New System.Windows.Forms.Label()
        Me.lblDel = New System.Windows.Forms.Label()
        Me.dtpFechaDesde = New System.Windows.Forms.DateTimePicker()
        Me.prg = New DevExpress.XtraEditors.ProgressBarControl()
        Me.sf = New System.Windows.Forms.SaveFileDialog()
        Me.tabtabAuditoriaPicking = New DevExpress.XtraTab.XtraTabControl()
        Me.tabDetalle = New DevExpress.XtraTab.XtraTabPage()
        Me.tabResumen = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridResumen = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabGrafico = New DevExpress.XtraTab.XtraTabPage()
        Me.ChartControl1 = New DevExpress.XtraCharts.ChartControl()
        Me.tabTiemposPreparacionByPedido = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridTiemposPedido = New DevExpress.XtraGrid.GridControl()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemProgressBar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMarqueeProgressBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemProgressBar3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgAuditoriaPicking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpFechas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFechas.SuspendLayout()
        CType(Me.prg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tabtabAuditoriaPicking, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabtabAuditoriaPicking.SuspendLayout()
        Me.tabDetalle.SuspendLayout()
        Me.tabResumen.SuspendLayout()
        CType(Me.dgridResumen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabGrafico.SuspendLayout()
        CType(Me.ChartControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabTiemposPreparacionByPedido.SuspendLayout()
        CType(Me.dgridTiemposPedido, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdImprimir, Me.cmdActualizar, Me.cmdSalir, Me.lblRegs, Me.cmdExToExcel, Me.mnuEliminarLayoutGrid, Me.mnuGuardarLayoutGrid})
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
        'cmdSalir
        '
        Me.cmdSalir.Caption = "Salir"
        Me.cmdSalir.Id = 3
        Me.cmdSalir.ImageOptions.SvgImage = CType(resources.GetObject("cmdSalir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdSalir.Name = "cmdSalir"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Regs: 0"
        Me.lblRegs.Id = 4
        Me.lblRegs.Name = "lblRegs"
        '
        'cmdExToExcel
        '
        Me.cmdExToExcel.Caption = "Exportar Excel"
        Me.cmdExToExcel.Id = 9
        Me.cmdExToExcel.ImageOptions.SvgImage = CType(resources.GetObject("cmdExToExcel.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdExToExcel.Name = "cmdExToExcel"
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
        Me.mnuGuardarLayoutGrid.Id = 12
        Me.mnuGuardarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardarLayoutGrid.Name = "mnuGuardarLayoutGrid"
        '
        'OpcionesLista
        '
        Me.OpcionesLista.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.OpcionesLista.Name = "OpcionesLista"
        Me.OpcionesLista.Text = "Auditoría de picking"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdExToExcel)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardarLayoutGrid)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminarLayoutGrid)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdSalir)
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
        'dgAuditoriaPicking
        '
        Me.dgAuditoriaPicking.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgAuditoriaPicking.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.RelationName = "Level1"
        Me.dgAuditoriaPicking.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.dgAuditoriaPicking.Location = New System.Drawing.Point(0, 0)
        Me.dgAuditoriaPicking.MainView = Me.GridView1
        Me.dgAuditoriaPicking.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgAuditoriaPicking.MenuManager = Me.RibbonControl
        Me.dgAuditoriaPicking.Name = "dgAuditoriaPicking"
        Me.dgAuditoriaPicking.Size = New System.Drawing.Size(949, 502)
        Me.dgAuditoriaPicking.TabIndex = 0
        Me.dgAuditoriaPicking.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.dgAuditoriaPicking
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowFooter = True
        '
        'grpFechas
        '
        Me.grpFechas.Controls.Add(Me.lblPrg)
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
        'lblPrg
        '
        Me.lblPrg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPrg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblPrg.Location = New System.Drawing.Point(2, 440)
        Me.lblPrg.Name = "lblPrg"
        Me.lblPrg.Size = New System.Drawing.Size(300, 112)
        Me.lblPrg.TabIndex = 7
        '
        'dtpfechaHasta
        '
        Me.dtpfechaHasta.CustomFormat = ""
        Me.dtpfechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfechaHasta.Location = New System.Drawing.Point(50, 92)
        Me.dtpfechaHasta.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpfechaHasta.Name = "dtpfechaHasta"
        Me.dtpfechaHasta.Size = New System.Drawing.Size(233, 23)
        Me.dtpfechaHasta.TabIndex = 4
        '
        'lblAl
        '
        Me.lblAl.AutoSize = True
        Me.lblAl.Location = New System.Drawing.Point(13, 92)
        Me.lblAl.Name = "lblAl"
        Me.lblAl.Size = New System.Drawing.Size(23, 16)
        Me.lblAl.TabIndex = 3
        Me.lblAl.Text = "Al:"
        '
        'lblDel
        '
        Me.lblDel.AutoSize = True
        Me.lblDel.Location = New System.Drawing.Point(13, 56)
        Me.lblDel.Name = "lblDel"
        Me.lblDel.Size = New System.Drawing.Size(30, 16)
        Me.lblDel.TabIndex = 3
        Me.lblDel.Text = "Del:"
        '
        'dtpFechaDesde
        '
        Me.dtpFechaDesde.CustomFormat = ""
        Me.dtpFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaDesde.Location = New System.Drawing.Point(50, 48)
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
        'tabtabAuditoriaPicking
        '
        Me.tabtabAuditoriaPicking.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabtabAuditoriaPicking.Location = New System.Drawing.Point(304, 193)
        Me.tabtabAuditoriaPicking.Name = "tabtabAuditoriaPicking"
        Me.tabtabAuditoriaPicking.SelectedTabPage = Me.tabDetalle
        Me.tabtabAuditoriaPicking.Size = New System.Drawing.Size(951, 532)
        Me.tabtabAuditoriaPicking.TabIndex = 9
        Me.tabtabAuditoriaPicking.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabDetalle, Me.tabResumen, Me.tabGrafico, Me.tabTiemposPreparacionByPedido})
        '
        'tabDetalle
        '
        Me.tabDetalle.Controls.Add(Me.dgAuditoriaPicking)
        Me.tabDetalle.Name = "tabDetalle"
        Me.tabDetalle.Size = New System.Drawing.Size(949, 502)
        Me.tabDetalle.Text = "Detalle"
        '
        'tabResumen
        '
        Me.tabResumen.Controls.Add(Me.dgridResumen)
        Me.tabResumen.Name = "tabResumen"
        Me.tabResumen.Size = New System.Drawing.Size(949, 502)
        Me.tabResumen.Text = "Resumen"
        '
        'dgridResumen
        '
        Me.dgridResumen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridResumen.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode2.RelationName = "Level1"
        Me.dgridResumen.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.dgridResumen.Location = New System.Drawing.Point(0, 0)
        Me.dgridResumen.MainView = Me.GridView2
        Me.dgridResumen.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridResumen.MenuManager = Me.RibbonControl
        Me.dgridResumen.Name = "dgridResumen"
        Me.dgridResumen.Size = New System.Drawing.Size(949, 502)
        Me.dgridResumen.TabIndex = 1
        Me.dgridResumen.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.dgridResumen
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsBehavior.ReadOnly = True
        Me.GridView2.OptionsFind.AlwaysVisible = True
        Me.GridView2.OptionsView.ColumnAutoWidth = False
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowFooter = True
        '
        'tabGrafico
        '
        Me.tabGrafico.Controls.Add(Me.ChartControl1)
        Me.tabGrafico.Name = "tabGrafico"
        Me.tabGrafico.Size = New System.Drawing.Size(949, 502)
        Me.tabGrafico.Text = "Grafico Resumen"
        '
        'ChartControl1
        '
        Me.ChartControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChartControl1.Location = New System.Drawing.Point(0, 0)
        Me.ChartControl1.Name = "ChartControl1"
        Me.ChartControl1.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.ChartControl1.Size = New System.Drawing.Size(949, 502)
        Me.ChartControl1.TabIndex = 0
        '
        'tabTiemposPreparacionByPedido
        '
        Me.tabTiemposPreparacionByPedido.Controls.Add(Me.dgridTiemposPedido)
        Me.tabTiemposPreparacionByPedido.Name = "tabTiemposPreparacionByPedido"
        Me.tabTiemposPreparacionByPedido.Size = New System.Drawing.Size(949, 502)
        Me.tabTiemposPreparacionByPedido.Text = "Tiempos por Pedido"
        '
        'dgridTiemposPedido
        '
        Me.dgridTiemposPedido.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridTiemposPedido.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode3.RelationName = "Level1"
        Me.dgridTiemposPedido.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode3})
        Me.dgridTiemposPedido.Location = New System.Drawing.Point(0, 0)
        Me.dgridTiemposPedido.MainView = Me.GridView3
        Me.dgridTiemposPedido.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridTiemposPedido.MenuManager = Me.RibbonControl
        Me.dgridTiemposPedido.Name = "dgridTiemposPedido"
        Me.dgridTiemposPedido.Size = New System.Drawing.Size(949, 502)
        Me.dgridTiemposPedido.TabIndex = 2
        Me.dgridTiemposPedido.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView3})
        '
        'GridView3
        '
        Me.GridView3.GridControl = Me.dgridTiemposPedido
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsBehavior.ReadOnly = True
        Me.GridView3.OptionsFind.AlwaysVisible = True
        Me.GridView3.OptionsView.ColumnAutoWidth = False
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowFooter = True
        '
        'frmAuditoriaPicking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1255, 777)
        Me.Controls.Add(Me.tabtabAuditoriaPicking)
        Me.Controls.Add(Me.prg)
        Me.Controls.Add(Me.grpFechas)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmAuditoriaPicking"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Auditoría de picking"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemProgressBar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMarqueeProgressBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemProgressBar3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgAuditoriaPicking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpFechas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFechas.ResumeLayout(False)
        Me.grpFechas.PerformLayout()
        CType(Me.prg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tabtabAuditoriaPicking, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabtabAuditoriaPicking.ResumeLayout(False)
        Me.tabDetalle.ResumeLayout(False)
        Me.tabResumen.ResumeLayout(False)
        CType(Me.dgridResumen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabGrafico.ResumeLayout(False)
        CType(Me.ChartControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabTiemposPreparacionByPedido.ResumeLayout(False)
        CType(Me.dgridTiemposPedido, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents OpcionesLista As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents dgAuditoriaPicking As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grpFechas As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtpFechaDesde As DateTimePicker
    Friend WithEvents lblAl As Label
    Friend WithEvents lblDel As Label
    Friend WithEvents dtpfechaHasta As DateTimePicker
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RepositoryItemProgressBar2 As DevExpress.XtraEditors.Repository.RepositoryItemProgressBar
    Friend WithEvents RepositoryItemProgressBar1 As DevExpress.XtraEditors.Repository.RepositoryItemProgressBar
    Friend WithEvents RepositoryItemMarqueeProgressBar1 As DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar
    Friend WithEvents RepositoryItemProgressBar3 As DevExpress.XtraEditors.Repository.RepositoryItemProgressBar
    Friend WithEvents prg As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents lblPrg As Label
    Friend WithEvents cmdExToExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents sf As SaveFileDialog
    Friend WithEvents mnuEliminarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuGuardarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tabtabAuditoriaPicking As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tabDetalle As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabResumen As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgridResumen As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tabGrafico As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ChartControl1 As DevExpress.XtraCharts.ChartControl
    Friend WithEvents tabTiemposPreparacionByPedido As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgridTiemposPedido As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
End Class
