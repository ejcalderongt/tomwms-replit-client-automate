<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmStockPorLote
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStockPorLote))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.cmdExToExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdPrintLP = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuGuardarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.chkObtenerExistenciaNAV = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.mnuSinExistencia = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grdStockPorLote = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BarStaticItem1 = New DevExpress.XtraBars.BarStaticItem()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbPropietarioBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.tabDatos = New DevExpress.XtraTab.XtraTabPage()
        Me.tabGraficoDispersion = New DevExpress.XtraTab.XtraTabPage()
        Me.chartDispersionVence = New DevExpress.XtraCharts.ChartControl()
        Me.tabGraficoProducto = New DevExpress.XtraTab.XtraTabPage()
        Me.chartProducto = New DevExpress.XtraCharts.ChartControl()
        Me.tabGraficoPorFamilia = New DevExpress.XtraTab.XtraTabPage()
        Me.chartFamilia = New DevExpress.XtraCharts.ChartControl()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdStockPorLote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.cmbPropietarioBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.tabDatos.SuspendLayout()
        Me.tabGraficoDispersion.SuspendLayout()
        CType(Me.chartDispersionVence, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabGraficoProducto.SuspendLayout()
        CType(Me.chartProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabGraficoPorFamilia.SuspendLayout()
        CType(Me.chartFamilia, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdActualizar, Me.cmdImprimir, Me.cmdSalir, Me.lblRegs, Me.cmdExToExcel, Me.cmdPrintLP, Me.mnuGuardarLayoutGrid, Me.mnuEliminarLayoutGrid, Me.chkObtenerExistenciaNAV, Me.mnuSinExistencia})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 13
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1340, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Caption = "Actualizar"
        Me.cmdActualizar.Id = 1
        Me.cmdActualizar.ImageOptions.SvgImage = CType(resources.GetObject("cmdActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizar.Name = "cmdActualizar"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir reporte"
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
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 4
        Me.lblRegs.Name = "lblRegs"
        '
        'cmdExToExcel
        '
        Me.cmdExToExcel.Caption = "Exportar a Excel"
        Me.cmdExToExcel.Id = 6
        Me.cmdExToExcel.ImageOptions.SvgImage = CType(resources.GetObject("cmdExToExcel.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdExToExcel.Name = "cmdExToExcel"
        '
        'cmdPrintLP
        '
        Me.cmdPrintLP.Caption = "Imprimir Licencia"
        Me.cmdPrintLP.Id = 7
        Me.cmdPrintLP.ImageOptions.SvgImage = CType(resources.GetObject("cmdPrintLP.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdPrintLP.Name = "cmdPrintLP"
        '
        'mnuGuardarLayoutGrid
        '
        Me.mnuGuardarLayoutGrid.Caption = "Guardar diseño de grid"
        Me.mnuGuardarLayoutGrid.Id = 8
        Me.mnuGuardarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardarLayoutGrid.Name = "mnuGuardarLayoutGrid"
        '
        'mnuEliminarLayoutGrid
        '
        Me.mnuEliminarLayoutGrid.Caption = "Eliminar diseño de grid"
        Me.mnuEliminarLayoutGrid.Id = 9
        Me.mnuEliminarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminarLayoutGrid.Name = "mnuEliminarLayoutGrid"
        '
        'chkObtenerExistenciaNAV
        '
        Me.chkObtenerExistenciaNAV.Caption = "Obtener existencias NAV"
        Me.chkObtenerExistenciaNAV.Id = 10
        Me.chkObtenerExistenciaNAV.Name = "chkObtenerExistenciaNAV"
        Me.chkObtenerExistenciaNAV.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuSinExistencia
        '
        Me.mnuSinExistencia.BindableChecked = True
        Me.mnuSinExistencia.Caption = "Con existencia"
        Me.mnuSinExistencia.Checked = True
        Me.mnuSinExistencia.Id = 12
        Me.mnuSinExistencia.Name = "mnuSinExistencia"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Existencias por Lote"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdExToExcel)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdPrintLP)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardarLayoutGrid)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminarLayoutGrid)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdSalir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.chkObtenerExistenciaNAV)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuSinExistencia)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 760)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1340, 30)
        '
        'grdStockPorLote
        '
        Me.grdStockPorLote.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdStockPorLote.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdStockPorLote.Location = New System.Drawing.Point(0, 0)
        Me.grdStockPorLote.MainView = Me.GridView1
        Me.grdStockPorLote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdStockPorLote.MenuManager = Me.RibbonControl
        Me.grdStockPorLote.Name = "grdStockPorLote"
        Me.grdStockPorLote.Size = New System.Drawing.Size(1052, 537)
        Me.grdStockPorLote.TabIndex = 2
        Me.grdStockPorLote.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.grdStockPorLote
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        '
        'BarStaticItem1
        '
        Me.BarStaticItem1.Caption = "Registros: 0"
        Me.BarStaticItem1.Id = 4
        Me.BarStaticItem1.Name = "BarStaticItem1"
        '
        'PanelControl1
        '
        Me.PanelControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.PanelControl1.Controls.Add(Me.Label2)
        Me.PanelControl1.Controls.Add(Me.cmbPropietarioBodega)
        Me.PanelControl1.Controls.Add(Me.cmbBodega)
        Me.PanelControl1.Controls.Add(Me.Label1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(276, 531)
        Me.PanelControl1.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 114)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 16)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Propietario:"
        '
        'cmbPropietarioBodega
        '
        Me.cmbPropietarioBodega.Location = New System.Drawing.Point(9, 134)
        Me.cmbPropietarioBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPropietarioBodega.MenuManager = Me.RibbonControl
        Me.cmbPropietarioBodega.Name = "cmbPropietarioBodega"
        Me.cmbPropietarioBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietarioBodega.Properties.NullText = ""
        Me.cmbPropietarioBodega.Size = New System.Drawing.Size(258, 22)
        Me.cmbPropietarioBodega.TabIndex = 11
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(6, 53)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbBodega.MenuManager = Me.RibbonControl
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(262, 22)
        Me.cmbBodega.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 16)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Bodega:"
        '
        'DockManager1
        '
        Me.DockManager1.Form = Me
        Me.DockManager1.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.DockPanel1})
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane", "DevExpress.XtraBars.TabFormControl", "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl", "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"})
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.DockPanel1.ID = New System.Guid("debed316-c05b-4e1e-9172-e8fd7a2db078")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 193)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(286, 200)
        Me.DockPanel1.Size = New System.Drawing.Size(286, 567)
        Me.DockPanel1.Text = "Filtros"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.PanelControl1)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 32)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(276, 531)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(286, 193)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.tabDatos
        Me.XtraTabControl1.Size = New System.Drawing.Size(1054, 567)
        Me.XtraTabControl1.TabIndex = 6
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabDatos, Me.tabGraficoProducto, Me.tabGraficoDispersion, Me.tabGraficoPorFamilia})
        '
        'tabDatos
        '
        Me.tabDatos.Controls.Add(Me.grdStockPorLote)
        Me.tabDatos.Name = "tabDatos"
        Me.tabDatos.Size = New System.Drawing.Size(1052, 537)
        Me.tabDatos.Text = "Existencias por Lote"
        '
        'tabGraficoDispersion
        '
        Me.tabGraficoDispersion.Controls.Add(Me.chartDispersionVence)
        Me.tabGraficoDispersion.Name = "tabGraficoDispersion"
        Me.tabGraficoDispersion.Size = New System.Drawing.Size(1052, 537)
        Me.tabGraficoDispersion.Text = "Dispersión por vencimiento"
        '
        'chartDispersionVence
        '
        Me.chartDispersionVence.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartDispersionVence.Location = New System.Drawing.Point(0, 0)
        Me.chartDispersionVence.Name = "chartDispersionVence"
        Me.chartDispersionVence.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.chartDispersionVence.Size = New System.Drawing.Size(1052, 537)
        Me.chartDispersionVence.TabIndex = 1
        '
        'tabGraficoProducto
        '
        Me.tabGraficoProducto.Controls.Add(Me.chartProducto)
        Me.tabGraficoProducto.Name = "tabGraficoProducto"
        Me.tabGraficoProducto.Size = New System.Drawing.Size(1052, 537)
        Me.tabGraficoProducto.Text = "Gráfico por producto"
        '
        'chartProducto
        '
        Me.chartProducto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartProducto.Location = New System.Drawing.Point(0, 0)
        Me.chartProducto.Name = "chartProducto"
        Me.chartProducto.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.chartProducto.Size = New System.Drawing.Size(1052, 537)
        Me.chartProducto.TabIndex = 2
        '
        'tabGraficoPorFamilia
        '
        Me.tabGraficoPorFamilia.Controls.Add(Me.chartFamilia)
        Me.tabGraficoPorFamilia.Name = "tabGraficoPorFamilia"
        Me.tabGraficoPorFamilia.Size = New System.Drawing.Size(1052, 537)
        Me.tabGraficoPorFamilia.Text = "Gráfico por familia"
        '
        'chartFamilia
        '
        Me.chartFamilia.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartFamilia.Location = New System.Drawing.Point(0, 0)
        Me.chartFamilia.Name = "chartFamilia"
        Me.chartFamilia.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.chartFamilia.Size = New System.Drawing.Size(1052, 537)
        Me.chartFamilia.TabIndex = 3
        '
        'frmStockPorLote
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1340, 790)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Controls.Add(Me.DockPanel1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmStockPorLote"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Existencias por Lote"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdStockPorLote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.cmbPropietarioBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.tabDatos.ResumeLayout(False)
        Me.tabGraficoDispersion.ResumeLayout(False)
        CType(Me.chartDispersionVence, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabGraficoProducto.ResumeLayout(False)
        CType(Me.chartProducto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabGraficoPorFamilia.ResumeLayout(False)
        CType(Me.chartFamilia, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grdStockPorLote As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents BarStaticItem1 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbPropietarioBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label1 As Label
    Friend WithEvents cmdExToExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdPrintLP As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuGuardarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkObtenerExistenciaNAV As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents mnuSinExistencia As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tabDatos As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabGraficoDispersion As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents chartDispersionVence As DevExpress.XtraCharts.ChartControl
    Friend WithEvents tabGraficoProducto As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents chartProducto As DevExpress.XtraCharts.ChartControl
    Friend WithEvents tabGraficoPorFamilia As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents chartFamilia As DevExpress.XtraCharts.ChartControl
End Class
