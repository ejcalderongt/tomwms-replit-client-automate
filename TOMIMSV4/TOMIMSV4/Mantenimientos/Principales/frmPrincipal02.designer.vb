<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPrincipal02
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
        Dim lblFechaInicioTareas As System.Windows.Forms.Label
        Dim lblFechaFinTareas As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrincipal02))
        Dim TimeRuler8 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Dim TimeRuler9 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Dim TimeRuler1 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Dim TimeRuler2 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Dim ArcScaleRange4 As DevExpress.XtraGauges.Core.Model.ArcScaleRange = New DevExpress.XtraGauges.Core.Model.ArcScaleRange()
        Dim ArcScaleRange5 As DevExpress.XtraGauges.Core.Model.ArcScaleRange = New DevExpress.XtraGauges.Core.Model.ArcScaleRange()
        Dim ArcScaleRange6 As DevExpress.XtraGauges.Core.Model.ArcScaleRange = New DevExpress.XtraGauges.Core.Model.ArcScaleRange()
        Dim TimeRuler3 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Dim TimeRuler4 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Dim TimeRuler10 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizarFromMenu = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPosponerTodo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPosponerSeleccionados = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarTarea = New DevExpress.XtraBars.BarButtonItem()
        Me.lblprg = New DevExpress.XtraBars.BarStaticItem()
        Me.BarButtonGroup1 = New DevExpress.XtraBars.BarButtonGroup()
        Me.mnuDashBoard = New DevExpress.XtraBars.BarButtonItem()
        Me.BarSubItem1 = New DevExpress.XtraBars.BarSubItem()
        Me.mnuReabastecimiento = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCalcularIndicesRotacion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuExcluirArribaDelMinimo = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.mnuExcluirArribaDeMaximo = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.mnuExcluirSinExistencia = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.mnuExcluirStockInferior = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.mnuTimerMonitor = New DevExpress.XtraBars.BarButtonItem()
        Me.chkOcultarTareasPendientesReabasto = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkExcluirUbicacionDestinoLlena = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.mnuOcultarLogReabasto = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgReabasto = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup4 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup5 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.DgridTareas = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ToolTipController1 = New DevExpress.Utils.ToolTipController(Me.components)
        Me.SchedulerM = New DevExpress.XtraScheduler.SchedulerControl()
        Me.TareahhBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DSCalendarioTarea = New TOMWMS.DSCalendarioTarea()
        Me.BodegamuellesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ResourcesPopupCheckedListBoxControl3 = New DevExpress.XtraScheduler.UI.ResourcesPopupCheckedListBoxControl()
        Me.PanelControl7 = New DevExpress.XtraEditors.PanelControl()
        Me.SchedulerP = New DevExpress.XtraScheduler.SchedulerControl()
        Me.DateNavigator4 = New DevExpress.XtraScheduler.DateNavigator()
        Me.ResourcesPopupCheckedListBoxControl4 = New DevExpress.XtraScheduler.UI.ResourcesPopupCheckedListBoxControl()
        Me.PropietariosBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Tarea_hhTableAdapter = New TOMWMS.DSCalendarioTareaTableAdapters.tarea_hhTableAdapter()
        Me.Bodega_muellesTableAdapter = New TOMWMS.DSCalendarioTareaTableAdapters.bodega_muellesTableAdapter()
        Me.PropietariosTableAdapter = New TOMWMS.DSCalendarioTareaTableAdapters.propietariosTableAdapter()
        Me.TabPane1 = New DevExpress.XtraBars.Navigation.TabPane()
        Me.TabTareas = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.dtpFechaInicio = New System.Windows.Forms.DateTimePicker()
        Me.dtpFechaFin = New System.Windows.Forms.DateTimePicker()
        Me.TabRellenado = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.dgridRellenado = New DevExpress.XtraGrid.GridControl()
        Me.gvdRellenado = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtprgreabasto = New System.Windows.Forms.RichTextBox()
        Me.dgridTareasPendientesReabasto = New DevExpress.XtraGrid.GridControl()
        Me.gvReabastoPendiente = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.prg = New DevExpress.XtraEditors.ProgressBarControl()
        Me.TabTareasMuelle = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.PanelControl6 = New DevExpress.XtraEditors.PanelControl()
        Me.DateNavigator3 = New DevExpress.XtraScheduler.DateNavigator()
        Me.TabTareasCalendario = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.tabOcupacion = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.lblCantPosiciones = New System.Windows.Forms.Label()
        Me.lblGauguePorcentajeOcupacion = New System.Windows.Forms.Label()
        Me.ccOcupacion = New DevExpress.XtraCharts.ChartControl()
        Me.GaugeControl = New DevExpress.XtraGauges.Win.GaugeControl()
        Me.cgOcupacionBodega = New DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge()
        Me.ArcScaleBackgroundLayerComponent1 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent()
        Me.ArcScaleComponent1 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent()
        Me.ArcScaleNeedleComponent1 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent()
        Me.ArcScaleSpindleCapComponent1 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent()
        Me.DefaultToolTipController1 = New DevExpress.Utils.DefaultToolTipController(Me.components)
        Me.LabelGrafica = New System.Windows.Forms.Label()
        Me.txtUbicacionesVacias = New System.Windows.Forms.LinkLabel()
        Me.tabInidcadores = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.dgridInidicesRotacion = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TabDashPicking = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.dvPicking = New DevExpress.DashboardWin.DashboardViewer(Me.components)
        Me.tabIndicadoresBodProp = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.dtpFechaAl = New System.Windows.Forms.DateTimePicker()
        Me.dtpFechaDel = New System.Windows.Forms.DateTimePicker()
        Me.lblAl = New System.Windows.Forms.Label()
        Me.lblDel = New System.Windows.Forms.Label()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblBodega = New System.Windows.Forms.Label()
        Me.lblProducto = New System.Windows.Forms.LinkLabel()
        Me.TabNavigationPage1 = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.chkConexionInternet = New DevExpress.XtraEditors.ToggleSwitch()
        Me.lblDotNetVersion = New DevExpress.XtraEditors.LabelControl()
        Me.lblSqlServerVersion = New DevExpress.XtraEditors.LabelControl()
        Me.lblAppMemoryUsage = New DevExpress.XtraEditors.LabelControl()
        Me.lblAvailableMemory = New DevExpress.XtraEditors.LabelControl()
        Me.lblUsedMemory = New DevExpress.XtraEditors.LabelControl()
        Me.lblProcessor = New DevExpress.XtraEditors.LabelControl()
        Me.lblMacAddress = New DevExpress.XtraEditors.LabelControl()
        Me.lblSerialNumber = New DevExpress.XtraEditors.LabelControl()
        Me.lblDiskSpace = New DevExpress.XtraEditors.LabelControl()
        Me.lblOSVersion = New DevExpress.XtraEditors.LabelControl()
        Me.Calendario = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.SchedulerControl1 = New DevExpress.XtraScheduler.SchedulerControl()
        Me.SchedulerDataStorage1 = New DevExpress.XtraScheduler.SchedulerDataStorage(Me.components)
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.lblUbicacionesVacias = New DevExpress.XtraEditors.LabelControl()
        Me.txtUbicacionesOcupadas = New DevExpress.XtraEditors.TextEdit()
        Me.lblUbicacionesOcupadas = New DevExpress.XtraEditors.LabelControl()
        Me.txtCantidadPosiciones = New DevExpress.XtraEditors.TextEdit()
        Me.lblCantidadPosiciones = New DevExpress.XtraEditors.LabelControl()
        Me.tmrTareas = New System.Windows.Forms.Timer(Me.components)
        Me.bgwTareas = New System.ComponentModel.BackgroundWorker()
        lblFechaInicioTareas = New System.Windows.Forms.Label()
        lblFechaFinTareas = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgridTareas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SchedulerM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TareahhBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DSCalendarioTarea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BodegamuellesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResourcesPopupCheckedListBoxControl3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResourcesPopupCheckedListBoxControl3.ResourcesCheckedListBoxControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl7.SuspendLayout()
        CType(Me.SchedulerP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateNavigator4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateNavigator4.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResourcesPopupCheckedListBoxControl4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResourcesPopupCheckedListBoxControl4.ResourcesCheckedListBoxControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PropietariosBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabPane1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPane1.SuspendLayout()
        Me.TabTareas.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.TabRellenado.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dgridRellenado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvdRellenado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgridTareasPendientesReabasto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvReabastoPendiente, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabTareasMuelle.SuspendLayout()
        CType(Me.PanelControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl6.SuspendLayout()
        CType(Me.DateNavigator3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateNavigator3.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabTareasCalendario.SuspendLayout()
        Me.tabOcupacion.SuspendLayout()
        Me.TableLayoutPanel.SuspendLayout()
        CType(Me.ccOcupacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cgOcupacionBodega, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleBackgroundLayerComponent1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleComponent1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleNeedleComponent1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleSpindleCapComponent1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabInidcadores.SuspendLayout()
        CType(Me.dgridInidicesRotacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabDashPicking.SuspendLayout()
        CType(Me.dvPicking, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabIndicadoresBodProp.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabNavigationPage1.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.chkConexionInternet.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Calendario.SuspendLayout()
        CType(Me.SchedulerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SchedulerDataStorage1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtUbicacionesOcupadas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidadPosiciones.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblFechaInicioTareas
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(lblFechaInicioTareas, DevExpress.Utils.DefaultBoolean.[Default])
        lblFechaInicioTareas.AutoSize = True
        lblFechaInicioTareas.Location = New System.Drawing.Point(94, 20)
        lblFechaInicioTareas.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        lblFechaInicioTareas.Name = "lblFechaInicioTareas"
        lblFechaInicioTareas.Size = New System.Drawing.Size(68, 13)
        lblFechaInicioTareas.TabIndex = 8
        lblFechaInicioTareas.Text = "Fecha Inicio:"
        '
        'lblFechaFinTareas
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(lblFechaFinTareas, DevExpress.Utils.DefaultBoolean.[Default])
        lblFechaFinTareas.AutoSize = True
        lblFechaFinTareas.Location = New System.Drawing.Point(421, 20)
        lblFechaFinTareas.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        lblFechaFinTareas.Name = "lblFechaFinTareas"
        lblFechaFinTareas.Size = New System.Drawing.Size(57, 13)
        lblFechaFinTareas.TabIndex = 10
        lblFechaFinTareas.Text = "Fecha Fin:"
        '
        'RibbonControl
        '
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(39, 37, 39, 37)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.BarButtonItem1, Me.mnuActualizarFromMenu, Me.mnuPosponerTodo, Me.mnuPosponerSeleccionados, Me.mnuEnviarTarea, Me.lblprg, Me.BarButtonGroup1, Me.mnuDashBoard, Me.BarSubItem1, Me.mnuReabastecimiento, Me.mnuCalcularIndicesRotacion, Me.mnuExcluirArribaDelMinimo, Me.mnuExcluirArribaDeMaximo, Me.mnuExcluirSinExistencia, Me.mnuExcluirStockInferior, Me.mnuTimerMonitor, Me.chkOcultarTareasPendientesReabasto, Me.chkExcluirUbicacionDestinoLlena, Me.mnuOcultarLogReabasto})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.RibbonControl.MaxItemId = 24
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 441
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(1332, 158)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.ImageOptions.Image = CType(resources.GetObject("mnuActualizar.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuActualizar.ImageOptions.LargeImage = CType(resources.GetObject("mnuActualizar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.Image = CType(resources.GetObject("mnuEliminar.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuEliminar.ImageOptions.LargeImage = CType(resources.GetObject("mnuEliminar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Gracias"
        Me.BarButtonItem1.Id = 4
        Me.BarButtonItem1.ImageOptions.Image = CType(resources.GetObject("BarButtonItem1.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem1.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'mnuActualizarFromMenu
        '
        Me.mnuActualizarFromMenu.Caption = "Actualizar"
        Me.mnuActualizarFromMenu.Id = 5
        Me.mnuActualizarFromMenu.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizarFromMenu.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizarFromMenu.Name = "mnuActualizarFromMenu"
        '
        'mnuPosponerTodo
        '
        Me.mnuPosponerTodo.Caption = "Posponer todo"
        Me.mnuPosponerTodo.Id = 6
        Me.mnuPosponerTodo.ImageOptions.SvgImage = CType(resources.GetObject("mnuPosponerTodo.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuPosponerTodo.Name = "mnuPosponerTodo"
        Me.mnuPosponerTodo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuPosponerSeleccionados
        '
        Me.mnuPosponerSeleccionados.Caption = "Posponer seleccionados"
        Me.mnuPosponerSeleccionados.Id = 7
        Me.mnuPosponerSeleccionados.ImageOptions.SvgImage = CType(resources.GetObject("mnuPosponerSeleccionados.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuPosponerSeleccionados.Name = "mnuPosponerSeleccionados"
        Me.mnuPosponerSeleccionados.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuEnviarTarea
        '
        Me.mnuEnviarTarea.Caption = "Enviar tarea"
        Me.mnuEnviarTarea.Id = 8
        Me.mnuEnviarTarea.ImageOptions.SvgImage = CType(resources.GetObject("mnuEnviarTarea.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEnviarTarea.Name = "mnuEnviarTarea"
        '
        'lblprg
        '
        Me.lblprg.Caption = "BarStaticItem1"
        Me.lblprg.Id = 9
        Me.lblprg.ImageOptions.Image = CType(resources.GetObject("lblprg.ImageOptions.Image"), System.Drawing.Image)
        Me.lblprg.ImageOptions.LargeImage = CType(resources.GetObject("lblprg.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.lblprg.Name = "lblprg"
        Me.lblprg.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'BarButtonGroup1
        '
        Me.BarButtonGroup1.Caption = "BarButtonGroup1"
        Me.BarButtonGroup1.Id = 10
        Me.BarButtonGroup1.Name = "BarButtonGroup1"
        '
        'mnuDashBoard
        '
        Me.mnuDashBoard.Caption = "Analítica de Movimientos"
        Me.mnuDashBoard.Id = 11
        Me.mnuDashBoard.ImageOptions.SvgImage = CType(resources.GetObject("mnuDashBoard.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuDashBoard.Name = "mnuDashBoard"
        '
        'BarSubItem1
        '
        Me.BarSubItem1.Caption = "Imprimir"
        Me.BarSubItem1.Id = 12
        Me.BarSubItem1.ImageOptions.SvgImage = CType(resources.GetObject("BarSubItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuReabastecimiento)})
        Me.BarSubItem1.Name = "BarSubItem1"
        '
        'mnuReabastecimiento
        '
        Me.mnuReabastecimiento.Caption = "Reabastecimiento"
        Me.mnuReabastecimiento.Id = 13
        Me.mnuReabastecimiento.Name = "mnuReabastecimiento"
        '
        'mnuCalcularIndicesRotacion
        '
        Me.mnuCalcularIndicesRotacion.Caption = "Calcular índices de rotación"
        Me.mnuCalcularIndicesRotacion.Id = 14
        Me.mnuCalcularIndicesRotacion.Name = "mnuCalcularIndicesRotacion"
        '
        'mnuExcluirArribaDelMinimo
        '
        Me.mnuExcluirArribaDelMinimo.BindableChecked = True
        Me.mnuExcluirArribaDelMinimo.Caption = "Excluir arriba del mínimo"
        Me.mnuExcluirArribaDelMinimo.Checked = True
        Me.mnuExcluirArribaDelMinimo.Id = 15
        Me.mnuExcluirArribaDelMinimo.Name = "mnuExcluirArribaDelMinimo"
        '
        'mnuExcluirArribaDeMaximo
        '
        Me.mnuExcluirArribaDeMaximo.BindableChecked = True
        Me.mnuExcluirArribaDeMaximo.Caption = "Excluir arriba de máximo"
        Me.mnuExcluirArribaDeMaximo.Checked = True
        Me.mnuExcluirArribaDeMaximo.Id = 16
        Me.mnuExcluirArribaDeMaximo.Name = "mnuExcluirArribaDeMaximo"
        '
        'mnuExcluirSinExistencia
        '
        Me.mnuExcluirSinExistencia.BindableChecked = True
        Me.mnuExcluirSinExistencia.Caption = "Excluir sin existencia"
        Me.mnuExcluirSinExistencia.Checked = True
        Me.mnuExcluirSinExistencia.Id = 17
        Me.mnuExcluirSinExistencia.Name = "mnuExcluirSinExistencia"
        '
        'mnuExcluirStockInferior
        '
        Me.mnuExcluirStockInferior.BindableChecked = True
        Me.mnuExcluirStockInferior.Caption = "Excluir stock inferior"
        Me.mnuExcluirStockInferior.Checked = True
        Me.mnuExcluirStockInferior.Id = 18
        Me.mnuExcluirStockInferior.Name = "mnuExcluirStockInferior"
        '
        'mnuTimerMonitor
        '
        Me.mnuTimerMonitor.Caption = "Activar/Desactivar"
        Me.mnuTimerMonitor.Id = 19
        Me.mnuTimerMonitor.ImageOptions.SvgImage = CType(resources.GetObject("mnuTimerMonitor.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuTimerMonitor.Name = "mnuTimerMonitor"
        '
        'chkOcultarTareasPendientesReabasto
        '
        Me.chkOcultarTareasPendientesReabasto.BindableChecked = True
        Me.chkOcultarTareasPendientesReabasto.Caption = "Ocultar Tareas Pendientes"
        Me.chkOcultarTareasPendientesReabasto.Checked = True
        Me.chkOcultarTareasPendientesReabasto.Id = 20
        Me.chkOcultarTareasPendientesReabasto.Name = "chkOcultarTareasPendientesReabasto"
        '
        'chkExcluirUbicacionDestinoLlena
        '
        Me.chkExcluirUbicacionDestinoLlena.BindableChecked = True
        Me.chkExcluirUbicacionDestinoLlena.Caption = "Excluir ubicaciones con producto"
        Me.chkExcluirUbicacionDestinoLlena.Checked = True
        Me.chkExcluirUbicacionDestinoLlena.Id = 22
        Me.chkExcluirUbicacionDestinoLlena.Name = "chkExcluirUbicacionDestinoLlena"
        '
        'mnuOcultarLogReabasto
        '
        Me.mnuOcultarLogReabasto.Caption = "Ocultar Log Reabasto"
        Me.mnuOcultarLogReabasto.Id = 23
        Me.mnuOcultarLogReabasto.ImageOptions.SvgImage = CType(resources.GetObject("mnuOcultarLogReabasto.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuOcultarLogReabasto.Name = "mnuOcultarLogReabasto"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.rpgReabasto, Me.RibbonPageGroup3, Me.RibbonPageGroup4, Me.RibbonPageGroup5})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Monitor"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizarFromMenu)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuTimerMonitor)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'rpgReabasto
        '
        Me.rpgReabasto.ItemLinks.Add(Me.mnuPosponerTodo)
        Me.rpgReabasto.ItemLinks.Add(Me.mnuPosponerSeleccionados)
        Me.rpgReabasto.ItemLinks.Add(Me.mnuEnviarTarea)
        Me.rpgReabasto.ItemLinks.Add(Me.mnuExcluirArribaDelMinimo)
        Me.rpgReabasto.ItemLinks.Add(Me.mnuExcluirArribaDeMaximo)
        Me.rpgReabasto.ItemLinks.Add(Me.mnuExcluirSinExistencia)
        Me.rpgReabasto.ItemLinks.Add(Me.mnuExcluirStockInferior)
        Me.rpgReabasto.ItemLinks.Add(Me.chkOcultarTareasPendientesReabasto)
        Me.rpgReabasto.ItemLinks.Add(Me.chkExcluirUbicacionDestinoLlena)
        Me.rpgReabasto.ItemLinks.Add(Me.mnuOcultarLogReabasto)
        Me.rpgReabasto.Name = "rpgReabasto"
        Me.rpgReabasto.Text = "Reabastecimiento"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.mnuDashBoard)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'RibbonPageGroup4
        '
        Me.RibbonPageGroup4.ItemLinks.Add(Me.BarSubItem1)
        Me.RibbonPageGroup4.Name = "RibbonPageGroup4"
        '
        'RibbonPageGroup5
        '
        Me.RibbonPageGroup5.ItemLinks.Add(Me.mnuCalcularIndicesRotacion)
        Me.RibbonPageGroup5.Name = "RibbonPageGroup5"
        Me.RibbonPageGroup5.Visible = False
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblprg)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 641)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1332, 24)
        Me.RibbonStatusBar.Visible = False
        '
        'DgridTareas
        '
        Me.DgridTareas.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridTareas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridTareas.EmbeddedNavigator.Appearance.Options.UseBackColor = True
        Me.DgridTareas.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.DgridTareas.Location = New System.Drawing.Point(0, 53)
        Me.DgridTareas.MainView = Me.GridView1
        Me.DgridTareas.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.DgridTareas.Name = "DgridTareas"
        Me.DgridTareas.Size = New System.Drawing.Size(1332, 397)
        Me.DgridTareas.TabIndex = 0
        Me.DgridTareas.ToolTipController = Me.ToolTipController1
        Me.DgridTareas.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.DetailHeight = 554
        Me.GridView1.GridControl = Me.DgridTareas
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsEditForm.PopupEditFormWidth = 857
        Me.GridView1.OptionsFilter.FilterEditorUseMenuForOperandsAndOperators = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways
        Me.GridView1.OptionsView.ShowFooter = True
        Me.GridView1.RowHeight = 50
        '
        'ToolTipController1
        '
        Me.ToolTipController1.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolTipController1.Appearance.ForeColor = System.Drawing.Color.SteelBlue
        Me.ToolTipController1.Appearance.Options.UseFont = True
        Me.ToolTipController1.Appearance.Options.UseForeColor = True
        '
        'SchedulerM
        '
        Me.SchedulerM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SchedulerM.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.Resource
        Me.SchedulerM.Location = New System.Drawing.Point(2, 42)
        Me.SchedulerM.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.SchedulerM.MenuManager = Me.RibbonControl
        Me.SchedulerM.Name = "SchedulerM"
        Me.SchedulerM.OptionsRangeControl.RangeMaximum = New Date(2021, 10, 1, 0, 0, 0, 0)
        Me.SchedulerM.OptionsRangeControl.RangeMinimum = New Date(2021, 7, 1, 0, 0, 0, 0)
        Me.SchedulerM.OptionsView.ResourceHeaders.Height = 75
        Me.SchedulerM.OptionsView.ResourceHeaders.ImageAlign = DevExpress.XtraScheduler.HeaderImageAlign.Top
        Me.SchedulerM.OptionsView.ResourceHeaders.ImageSize = New System.Drawing.Size(150, 50)
        Me.SchedulerM.OptionsView.ResourceHeaders.ImageSizeMode = DevExpress.XtraScheduler.HeaderImageSizeMode.ZoomImage
        Me.SchedulerM.Size = New System.Drawing.Size(747, 439)
        Me.SchedulerM.Start = New Date(2016, 5, 18, 0, 0, 0, 0)
        Me.SchedulerM.TabIndex = 1
        Me.SchedulerM.Text = "SchedulerControl1"
        Me.SchedulerM.ToolTipController = Me.ToolTipController1
        Me.SchedulerM.Views.DayView.TimeRulers.Add(TimeRuler8)
        Me.SchedulerM.Views.WeekView.ResourcesPerPage = 3
        Me.SchedulerM.Views.WorkWeekView.TimeRulers.Add(TimeRuler9)
        Me.SchedulerM.Views.YearView.UseOptimizedScrolling = False
        '
        'TareahhBindingSource
        '
        Me.TareahhBindingSource.DataMember = "tarea_hh"
        Me.TareahhBindingSource.DataSource = Me.DSCalendarioTarea
        '
        'DSCalendarioTarea
        '
        Me.DSCalendarioTarea.DataSetName = "DSCalendarioTarea"
        Me.DSCalendarioTarea.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'BodegamuellesBindingSource
        '
        Me.BodegamuellesBindingSource.DataMember = "bodega_muelles"
        Me.BodegamuellesBindingSource.DataSource = Me.DSCalendarioTarea
        '
        'ResourcesPopupCheckedListBoxControl3
        '
        Me.ResourcesPopupCheckedListBoxControl3.Dock = System.Windows.Forms.DockStyle.Top
        Me.ResourcesPopupCheckedListBoxControl3.EditValue = "(None)"
        Me.ResourcesPopupCheckedListBoxControl3.Location = New System.Drawing.Point(2, 2)
        Me.ResourcesPopupCheckedListBoxControl3.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.ResourcesPopupCheckedListBoxControl3.Name = "ResourcesPopupCheckedListBoxControl3"
        Me.ResourcesPopupCheckedListBoxControl3.Properties.AutoHeight = False
        Me.ResourcesPopupCheckedListBoxControl3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        '
        '
        '
        Me.ResourcesPopupCheckedListBoxControl3.ResourcesCheckedListBoxControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ResourcesPopupCheckedListBoxControl3.ResourcesCheckedListBoxControl.Location = New System.Drawing.Point(0, 0)
        Me.ResourcesPopupCheckedListBoxControl3.ResourcesCheckedListBoxControl.Name = ""
        Me.ResourcesPopupCheckedListBoxControl3.ResourcesCheckedListBoxControl.SchedulerControl = Me.SchedulerM
        Me.ResourcesPopupCheckedListBoxControl3.ResourcesCheckedListBoxControl.Size = New System.Drawing.Size(200, 100)
        Me.ResourcesPopupCheckedListBoxControl3.ResourcesCheckedListBoxControl.TabIndex = 0
        Me.ResourcesPopupCheckedListBoxControl3.SchedulerControl = Me.SchedulerM
        Me.ResourcesPopupCheckedListBoxControl3.Size = New System.Drawing.Size(747, 40)
        Me.ResourcesPopupCheckedListBoxControl3.TabIndex = 0
        '
        'PanelControl7
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.PanelControl7, DevExpress.Utils.DefaultBoolean.[Default])
        Me.PanelControl7.Appearance.BackColor = System.Drawing.Color.LightGray
        Me.PanelControl7.Appearance.Options.UseBackColor = True
        Me.PanelControl7.Controls.Add(Me.SchedulerP)
        Me.PanelControl7.Controls.Add(Me.DateNavigator4)
        Me.PanelControl7.Controls.Add(Me.ResourcesPopupCheckedListBoxControl4)
        Me.PanelControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl7.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl7.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.PanelControl7.Name = "PanelControl7"
        Me.PanelControl7.Size = New System.Drawing.Size(1113, 483)
        Me.PanelControl7.TabIndex = 0
        '
        'SchedulerP
        '
        Me.SchedulerP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SchedulerP.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.Resource
        Me.SchedulerP.Location = New System.Drawing.Point(2, 50)
        Me.SchedulerP.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.SchedulerP.MenuManager = Me.RibbonControl
        Me.SchedulerP.Name = "SchedulerP"
        Me.SchedulerP.OptionsRangeControl.RangeMaximum = New Date(2021, 10, 1, 0, 0, 0, 0)
        Me.SchedulerP.OptionsRangeControl.RangeMinimum = New Date(2021, 7, 1, 0, 0, 0, 0)
        Me.SchedulerP.OptionsView.ResourceHeaders.Height = 75
        Me.SchedulerP.OptionsView.ResourceHeaders.ImageAlign = DevExpress.XtraScheduler.HeaderImageAlign.Top
        Me.SchedulerP.OptionsView.ResourceHeaders.ImageSize = New System.Drawing.Size(150, 50)
        Me.SchedulerP.OptionsView.ResourceHeaders.ImageSizeMode = DevExpress.XtraScheduler.HeaderImageSizeMode.ZoomImage
        Me.SchedulerP.Size = New System.Drawing.Size(807, 431)
        Me.SchedulerP.Start = New Date(2016, 5, 18, 0, 0, 0, 0)
        Me.SchedulerP.TabIndex = 1
        Me.SchedulerP.Text = "SchedulerControl2"
        Me.SchedulerP.ToolTipController = Me.ToolTipController1
        Me.SchedulerP.Views.DayView.TimeRulers.Add(TimeRuler1)
        Me.SchedulerP.Views.WeekView.ResourcesPerPage = 3
        Me.SchedulerP.Views.WorkWeekView.TimeRulers.Add(TimeRuler2)
        Me.SchedulerP.Views.YearView.UseOptimizedScrolling = False
        '
        'DateNavigator4
        '
        Me.DateNavigator4.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateNavigator4.Cursor = System.Windows.Forms.Cursors.Default
        Me.DateNavigator4.DateTime = New Date(2016, 5, 18, 0, 0, 0, 0)
        Me.DateNavigator4.Dock = System.Windows.Forms.DockStyle.Right
        Me.DateNavigator4.EditValue = New Date(2016, 5, 18, 0, 0, 0, 0)
        Me.DateNavigator4.FirstDayOfWeek = System.DayOfWeek.Monday
        Me.DateNavigator4.Location = New System.Drawing.Point(809, 50)
        Me.DateNavigator4.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.DateNavigator4.Name = "DateNavigator4"
        Me.DateNavigator4.SchedulerControl = Me.SchedulerP
        Me.DateNavigator4.Size = New System.Drawing.Size(302, 431)
        Me.DateNavigator4.TabIndex = 2
        '
        'ResourcesPopupCheckedListBoxControl4
        '
        Me.ResourcesPopupCheckedListBoxControl4.Dock = System.Windows.Forms.DockStyle.Top
        Me.ResourcesPopupCheckedListBoxControl4.EditValue = "(None)"
        Me.ResourcesPopupCheckedListBoxControl4.Location = New System.Drawing.Point(2, 2)
        Me.ResourcesPopupCheckedListBoxControl4.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.ResourcesPopupCheckedListBoxControl4.Name = "ResourcesPopupCheckedListBoxControl4"
        Me.ResourcesPopupCheckedListBoxControl4.Properties.AutoHeight = False
        Me.ResourcesPopupCheckedListBoxControl4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        '
        '
        '
        Me.ResourcesPopupCheckedListBoxControl4.ResourcesCheckedListBoxControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ResourcesPopupCheckedListBoxControl4.ResourcesCheckedListBoxControl.Location = New System.Drawing.Point(0, 0)
        Me.ResourcesPopupCheckedListBoxControl4.ResourcesCheckedListBoxControl.Name = ""
        Me.ResourcesPopupCheckedListBoxControl4.ResourcesCheckedListBoxControl.SchedulerControl = Me.SchedulerP
        Me.ResourcesPopupCheckedListBoxControl4.ResourcesCheckedListBoxControl.Size = New System.Drawing.Size(200, 100)
        Me.ResourcesPopupCheckedListBoxControl4.ResourcesCheckedListBoxControl.TabIndex = 0
        Me.ResourcesPopupCheckedListBoxControl4.SchedulerControl = Me.SchedulerP
        Me.ResourcesPopupCheckedListBoxControl4.Size = New System.Drawing.Size(1109, 48)
        Me.ResourcesPopupCheckedListBoxControl4.TabIndex = 0
        '
        'PropietariosBindingSource
        '
        Me.PropietariosBindingSource.DataMember = "propietarios"
        Me.PropietariosBindingSource.DataSource = Me.DSCalendarioTarea
        '
        'Tarea_hhTableAdapter
        '
        Me.Tarea_hhTableAdapter.ClearBeforeFill = True
        '
        'Bodega_muellesTableAdapter
        '
        Me.Bodega_muellesTableAdapter.ClearBeforeFill = True
        '
        'PropietariosTableAdapter
        '
        Me.PropietariosTableAdapter.ClearBeforeFill = True
        '
        'TabPane1
        '
        Me.TabPane1.Controls.Add(Me.TabTareas)
        Me.TabPane1.Controls.Add(Me.TabRellenado)
        Me.TabPane1.Controls.Add(Me.TabTareasMuelle)
        Me.TabPane1.Controls.Add(Me.TabTareasCalendario)
        Me.TabPane1.Controls.Add(Me.tabOcupacion)
        Me.TabPane1.Controls.Add(Me.tabInidcadores)
        Me.TabPane1.Controls.Add(Me.TabDashPicking)
        Me.TabPane1.Controls.Add(Me.tabIndicadoresBodProp)
        Me.TabPane1.Controls.Add(Me.TabNavigationPage1)
        Me.TabPane1.Controls.Add(Me.Calendario)
        Me.TabPane1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabPane1.Location = New System.Drawing.Point(0, 158)
        Me.TabPane1.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.TabPane1.Name = "TabPane1"
        Me.TabPane1.Pages.AddRange(New DevExpress.XtraBars.Navigation.NavigationPageBase() {Me.TabTareas, Me.TabRellenado, Me.TabTareasMuelle, Me.TabTareasCalendario, Me.tabOcupacion, Me.tabInidcadores, Me.TabDashPicking, Me.tabIndicadoresBodProp, Me.TabNavigationPage1, Me.Calendario})
        Me.TabPane1.RegularSize = New System.Drawing.Size(1332, 483)
        Me.TabPane1.SelectedPage = Me.TabTareas
        Me.TabPane1.Size = New System.Drawing.Size(1332, 483)
        Me.TabPane1.TabIndex = 3
        Me.TabPane1.Text = "TabPane1"
        '
        'TabTareas
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.TabTareas, DevExpress.Utils.DefaultBoolean.[Default])
        Me.TabTareas.Caption = "Tareas"
        Me.TabTareas.Controls.Add(Me.DgridTareas)
        Me.TabTareas.Controls.Add(Me.PanelControl1)
        Me.TabTareas.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.TabTareas.Name = "TabTareas"
        Me.TabTareas.Size = New System.Drawing.Size(1332, 450)
        '
        'PanelControl1
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.PanelControl1, DevExpress.Utils.DefaultBoolean.[Default])
        Me.PanelControl1.Controls.Add(Me.dtpFechaInicio)
        Me.PanelControl1.Controls.Add(Me.dtpFechaFin)
        Me.PanelControl1.Controls.Add(lblFechaInicioTareas)
        Me.PanelControl1.Controls.Add(lblFechaFinTareas)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1332, 53)
        Me.PanelControl1.TabIndex = 1
        '
        'dtpFechaInicio
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.dtpFechaInicio, DevExpress.Utils.DefaultBoolean.[Default])
        Me.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaInicio.Location = New System.Drawing.Point(173, 18)
        Me.dtpFechaInicio.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.dtpFechaInicio.Name = "dtpFechaInicio"
        Me.dtpFechaInicio.Size = New System.Drawing.Size(170, 21)
        Me.dtpFechaInicio.TabIndex = 9
        '
        'dtpFechaFin
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.dtpFechaFin, DevExpress.Utils.DefaultBoolean.[Default])
        Me.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaFin.Location = New System.Drawing.Point(489, 18)
        Me.dtpFechaFin.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.dtpFechaFin.Name = "dtpFechaFin"
        Me.dtpFechaFin.Size = New System.Drawing.Size(170, 21)
        Me.dtpFechaFin.TabIndex = 11
        '
        'TabRellenado
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.TabRellenado, DevExpress.Utils.DefaultBoolean.[Default])
        Me.TabRellenado.Caption = "Reabastecimiento"
        Me.TabRellenado.Controls.Add(Me.SplitContainer1)
        Me.TabRellenado.Controls.Add(Me.prg)
        Me.TabRellenado.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.TabRellenado.Name = "TabRellenado"
        Me.TabRellenado.Size = New System.Drawing.Size(1332, 450)
        '
        'SplitContainer1
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.SplitContainer1, DevExpress.Utils.DefaultBoolean.[Default])
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.SplitContainer1.Panel1, DevExpress.Utils.DefaultBoolean.[Default])
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgridRellenado)
        '
        'SplitContainer1.Panel2
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.SplitContainer1.Panel2, DevExpress.Utils.DefaultBoolean.[Default])
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtprgreabasto)
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgridTareasPendientesReabasto)
        Me.SplitContainer1.Size = New System.Drawing.Size(1332, 407)
        Me.SplitContainer1.SplitterDistance = 933
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 4
        '
        'dgridRellenado
        '
        Me.dgridRellenado.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridRellenado.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5)
        Me.dgridRellenado.Location = New System.Drawing.Point(0, 0)
        Me.dgridRellenado.MainView = Me.gvdRellenado
        Me.dgridRellenado.Margin = New System.Windows.Forms.Padding(5)
        Me.dgridRellenado.Name = "dgridRellenado"
        Me.dgridRellenado.Size = New System.Drawing.Size(933, 407)
        Me.dgridRellenado.TabIndex = 1
        Me.dgridRellenado.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvdRellenado})
        '
        'gvdRellenado
        '
        Me.gvdRellenado.Appearance.SelectedRow.BackColor = System.Drawing.Color.Transparent
        Me.gvdRellenado.Appearance.SelectedRow.Options.UseBackColor = True
        Me.gvdRellenado.DetailHeight = 554
        Me.gvdRellenado.GridControl = Me.dgridRellenado
        Me.gvdRellenado.Name = "gvdRellenado"
        Me.gvdRellenado.OptionsEditForm.PopupEditFormWidth = 857
        Me.gvdRellenado.OptionsView.ColumnAutoWidth = False
        Me.gvdRellenado.OptionsView.ShowAutoFilterRow = True
        Me.gvdRellenado.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways
        '
        'txtprgreabasto
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.txtprgreabasto, DevExpress.Utils.DefaultBoolean.[Default])
        Me.txtprgreabasto.BackColor = System.Drawing.Color.White
        Me.txtprgreabasto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtprgreabasto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtprgreabasto.Location = New System.Drawing.Point(0, 216)
        Me.txtprgreabasto.Name = "txtprgreabasto"
        Me.txtprgreabasto.Size = New System.Drawing.Size(394, 191)
        Me.txtprgreabasto.TabIndex = 4
        Me.txtprgreabasto.Text = ""
        '
        'dgridTareasPendientesReabasto
        '
        Me.dgridTareasPendientesReabasto.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgridTareasPendientesReabasto.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5)
        Me.dgridTareasPendientesReabasto.Location = New System.Drawing.Point(0, 0)
        Me.dgridTareasPendientesReabasto.MainView = Me.gvReabastoPendiente
        Me.dgridTareasPendientesReabasto.Margin = New System.Windows.Forms.Padding(5)
        Me.dgridTareasPendientesReabasto.Name = "dgridTareasPendientesReabasto"
        Me.dgridTareasPendientesReabasto.Size = New System.Drawing.Size(394, 216)
        Me.dgridTareasPendientesReabasto.TabIndex = 2
        Me.dgridTareasPendientesReabasto.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvReabastoPendiente})
        '
        'gvReabastoPendiente
        '
        Me.gvReabastoPendiente.Appearance.SelectedRow.BackColor = System.Drawing.Color.Transparent
        Me.gvReabastoPendiente.Appearance.SelectedRow.Options.UseBackColor = True
        Me.gvReabastoPendiente.DetailHeight = 554
        Me.gvReabastoPendiente.GridControl = Me.dgridTareasPendientesReabasto
        Me.gvReabastoPendiente.Name = "gvReabastoPendiente"
        Me.gvReabastoPendiente.OptionsBehavior.Editable = False
        Me.gvReabastoPendiente.OptionsBehavior.ReadOnly = True
        Me.gvReabastoPendiente.OptionsEditForm.PopupEditFormWidth = 857
        Me.gvReabastoPendiente.OptionsFind.AllowFindPanel = False
        Me.gvReabastoPendiente.OptionsView.ColumnAutoWidth = False
        Me.gvReabastoPendiente.OptionsView.ShowAutoFilterRow = True
        Me.gvReabastoPendiente.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways
        Me.gvReabastoPendiente.OptionsView.ShowFooter = True
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.prg.Location = New System.Drawing.Point(0, 407)
        Me.prg.Margin = New System.Windows.Forms.Padding(4)
        Me.prg.MenuManager = Me.RibbonControl
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(1332, 43)
        Me.prg.TabIndex = 2
        '
        'TabTareasMuelle
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.TabTareasMuelle, DevExpress.Utils.DefaultBoolean.[Default])
        Me.TabTareasMuelle.Caption = "Ocupación por muelle"
        Me.TabTareasMuelle.Controls.Add(Me.PanelControl6)
        Me.TabTareasMuelle.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.TabTareasMuelle.Name = "TabTareasMuelle"
        Me.TabTareasMuelle.PageVisible = False
        Me.TabTareasMuelle.Size = New System.Drawing.Size(1113, 483)
        '
        'PanelControl6
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.PanelControl6, DevExpress.Utils.DefaultBoolean.[Default])
        Me.PanelControl6.Controls.Add(Me.SchedulerM)
        Me.PanelControl6.Controls.Add(Me.ResourcesPopupCheckedListBoxControl3)
        Me.PanelControl6.Controls.Add(Me.DateNavigator3)
        Me.PanelControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl6.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl6.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.PanelControl6.Name = "PanelControl6"
        Me.PanelControl6.Size = New System.Drawing.Size(1113, 483)
        Me.PanelControl6.TabIndex = 0
        '
        'DateNavigator3
        '
        Me.DateNavigator3.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateNavigator3.Cursor = System.Windows.Forms.Cursors.Default
        Me.DateNavigator3.DateTime = New Date(2016, 5, 18, 0, 0, 0, 0)
        Me.DateNavigator3.Dock = System.Windows.Forms.DockStyle.Right
        Me.DateNavigator3.EditValue = New Date(2016, 5, 18, 0, 0, 0, 0)
        Me.DateNavigator3.FirstDayOfWeek = System.DayOfWeek.Monday
        Me.DateNavigator3.Location = New System.Drawing.Point(749, 2)
        Me.DateNavigator3.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.DateNavigator3.Name = "DateNavigator3"
        Me.DateNavigator3.SchedulerControl = Me.SchedulerM
        Me.DateNavigator3.Size = New System.Drawing.Size(362, 479)
        Me.DateNavigator3.TabIndex = 2
        '
        'TabTareasCalendario
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.TabTareasCalendario, DevExpress.Utils.DefaultBoolean.[Default])
        Me.TabTareasCalendario.Caption = "Planificación"
        Me.TabTareasCalendario.Controls.Add(Me.PanelControl7)
        Me.TabTareasCalendario.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.TabTareasCalendario.Name = "TabTareasCalendario"
        Me.TabTareasCalendario.PageVisible = False
        Me.TabTareasCalendario.Size = New System.Drawing.Size(1113, 483)
        '
        'tabOcupacion
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.tabOcupacion, DevExpress.Utils.DefaultBoolean.[Default])
        Me.tabOcupacion.Caption = "Ocupación"
        Me.tabOcupacion.Controls.Add(Me.TableLayoutPanel)
        Me.tabOcupacion.Margin = New System.Windows.Forms.Padding(5)
        Me.tabOcupacion.Name = "tabOcupacion"
        Me.tabOcupacion.Size = New System.Drawing.Size(1332, 450)
        '
        'TableLayoutPanel
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.TableLayoutPanel, DevExpress.Utils.DefaultBoolean.[Default])
        Me.TableLayoutPanel.ColumnCount = 3
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.24868!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.75132!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 276.0!))
        Me.TableLayoutPanel.Controls.Add(Me.lblCantPosiciones, 2, 0)
        Me.TableLayoutPanel.Controls.Add(Me.lblGauguePorcentajeOcupacion, 0, 0)
        Me.TableLayoutPanel.Controls.Add(Me.ccOcupacion, 1, 1)
        Me.TableLayoutPanel.Controls.Add(Me.GaugeControl, 0, 1)
        Me.TableLayoutPanel.Controls.Add(Me.LabelGrafica, 1, 0)
        Me.TableLayoutPanel.Controls.Add(Me.GroupControl1, 2, 1)
        Me.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel.Margin = New System.Windows.Forms.Padding(5)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        Me.TableLayoutPanel.RowCount = 2
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.148936!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.85107!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel.Size = New System.Drawing.Size(1332, 450)
        Me.TableLayoutPanel.TabIndex = 0
        '
        'lblCantPosiciones
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.lblCantPosiciones, DevExpress.Utils.DefaultBoolean.[Default])
        Me.lblCantPosiciones.BackColor = System.Drawing.Color.Honeydew
        Me.lblCantPosiciones.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCantPosiciones.Font = New System.Drawing.Font("Tahoma", 13.8!, System.Drawing.FontStyle.Bold)
        Me.lblCantPosiciones.Location = New System.Drawing.Point(1060, 0)
        Me.lblCantPosiciones.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblCantPosiciones.Name = "lblCantPosiciones"
        Me.lblCantPosiciones.Size = New System.Drawing.Size(267, 41)
        Me.lblCantPosiciones.TabIndex = 5
        Me.lblCantPosiciones.Text = "Cantidad de ubicaciones"
        Me.lblCantPosiciones.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblGauguePorcentajeOcupacion
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.lblGauguePorcentajeOcupacion, DevExpress.Utils.DefaultBoolean.[Default])
        Me.lblGauguePorcentajeOcupacion.BackColor = System.Drawing.Color.OldLace
        Me.lblGauguePorcentajeOcupacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblGauguePorcentajeOcupacion.Font = New System.Drawing.Font("Tahoma", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGauguePorcentajeOcupacion.Location = New System.Drawing.Point(5, 0)
        Me.lblGauguePorcentajeOcupacion.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblGauguePorcentajeOcupacion.Name = "lblGauguePorcentajeOcupacion"
        Me.lblGauguePorcentajeOcupacion.Size = New System.Drawing.Size(277, 41)
        Me.lblGauguePorcentajeOcupacion.TabIndex = 2
        Me.lblGauguePorcentajeOcupacion.Text = "% Ocupación bodega"
        Me.lblGauguePorcentajeOcupacion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ccOcupacion
        '
        Me.ccOcupacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ccOcupacion.Legend.Name = "Default Legend"
        Me.ccOcupacion.Location = New System.Drawing.Point(292, 46)
        Me.ccOcupacion.Margin = New System.Windows.Forms.Padding(5)
        Me.ccOcupacion.Name = "ccOcupacion"
        Me.ccOcupacion.PaletteName = "Violet II"
        Me.ccOcupacion.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.ccOcupacion.Size = New System.Drawing.Size(758, 399)
        Me.ccOcupacion.TabIndex = 1
        '
        'GaugeControl
        '
        Me.GaugeControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GaugeControl.Gauges.AddRange(New DevExpress.XtraGauges.Base.IGauge() {Me.cgOcupacionBodega})
        Me.GaugeControl.LayoutInterval = 8
        Me.GaugeControl.LayoutPadding = New DevExpress.XtraGauges.Core.Base.Thickness(8, 7, 8, 7)
        Me.GaugeControl.Location = New System.Drawing.Point(5, 46)
        Me.GaugeControl.Margin = New System.Windows.Forms.Padding(5)
        Me.GaugeControl.Name = "GaugeControl"
        Me.GaugeControl.Size = New System.Drawing.Size(277, 399)
        Me.GaugeControl.TabIndex = 0
        Me.GaugeControl.ToolTipController = Me.DefaultToolTipController1.DefaultController
        '
        'cgOcupacionBodega
        '
        Me.cgOcupacionBodega.BackgroundLayers.AddRange(New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent() {Me.ArcScaleBackgroundLayerComponent1})
        Me.cgOcupacionBodega.Bounds = New System.Drawing.Rectangle(8, 7, 261, 385)
        Me.cgOcupacionBodega.Name = "cgOcupacionBodega"
        Me.cgOcupacionBodega.Needles.AddRange(New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent() {Me.ArcScaleNeedleComponent1})
        Me.cgOcupacionBodega.Scales.AddRange(New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent() {Me.ArcScaleComponent1})
        Me.cgOcupacionBodega.SpindleCaps.AddRange(New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent() {Me.ArcScaleSpindleCapComponent1})
        '
        'ArcScaleBackgroundLayerComponent1
        '
        Me.ArcScaleBackgroundLayerComponent1.ArcScale = Me.ArcScaleComponent1
        Me.ArcScaleBackgroundLayerComponent1.Name = "bg"
        Me.ArcScaleBackgroundLayerComponent1.ScaleCenterPos = New DevExpress.XtraGauges.Core.Base.PointF2D(0.5!, 0.685!)
        Me.ArcScaleBackgroundLayerComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.Linear_Style1
        Me.ArcScaleBackgroundLayerComponent1.Size = New System.Drawing.SizeF(250.0!, 179.0!)
        Me.ArcScaleBackgroundLayerComponent1.ZOrder = 1000
        '
        'ArcScaleComponent1
        '
        Me.ArcScaleComponent1.AppearanceMajorTickmark.BorderBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent1.AppearanceMajorTickmark.ContentBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent1.AppearanceMinorTickmark.BorderBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent1.AppearanceMinorTickmark.ContentBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent1.AppearanceTickmarkText.DXFont = New DevExpress.Drawing.DXFont("Tahoma", 9.0!)
        Me.ArcScaleComponent1.AppearanceTickmarkText.Spacing = New DevExpress.XtraGauges.Core.Base.TextSpacing(2, 2, 2, 2)
        Me.ArcScaleComponent1.AppearanceTickmarkText.TextBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#3A3832")
        Me.ArcScaleComponent1.Center = New DevExpress.XtraGauges.Core.Base.PointF2D(125.0!, 170.0!)
        Me.ArcScaleComponent1.EndAngle = 0!
        Me.ArcScaleComponent1.MajorTickCount = 6
        Me.ArcScaleComponent1.MajorTickmark.FormatString = "{0:F0}"
        Me.ArcScaleComponent1.MajorTickmark.ShapeOffset = -10.0!
        Me.ArcScaleComponent1.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style20_1
        Me.ArcScaleComponent1.MajorTickmark.TextOffset = -18.0!
        Me.ArcScaleComponent1.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight
        Me.ArcScaleComponent1.MaxValue = 100.0!
        Me.ArcScaleComponent1.MinorTickCount = 4
        Me.ArcScaleComponent1.MinorTickmark.ShapeOffset = -6.0!
        Me.ArcScaleComponent1.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style20_2
        Me.ArcScaleComponent1.Name = "scale1"
        Me.ArcScaleComponent1.RadiusY = 98.0!
        ArcScaleRange4.AppearanceRange.ContentBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#96C562")
        ArcScaleRange4.EndThickness = 5.0!
        ArcScaleRange4.EndValue = 33.0!
        ArcScaleRange4.Name = "Range0"
        ArcScaleRange4.ShapeOffset = 6.0!
        ArcScaleRange4.StartThickness = 5.0!
        ArcScaleRange5.AppearanceRange.ContentBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#FCD66B")
        ArcScaleRange5.EndThickness = 5.0!
        ArcScaleRange5.EndValue = 66.0!
        ArcScaleRange5.Name = "Range1"
        ArcScaleRange5.ShapeOffset = 6.0!
        ArcScaleRange5.StartThickness = 5.0!
        ArcScaleRange5.StartValue = 33.0!
        ArcScaleRange6.AppearanceRange.ContentBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#EA836D")
        ArcScaleRange6.EndThickness = 5.0!
        ArcScaleRange6.EndValue = 100.0!
        ArcScaleRange6.Name = "Range2"
        ArcScaleRange6.ShapeOffset = 6.0!
        ArcScaleRange6.StartThickness = 5.0!
        ArcScaleRange6.StartValue = 66.0!
        Me.ArcScaleComponent1.Ranges.AddRange(New DevExpress.XtraGauges.Core.Model.IRange() {ArcScaleRange4, ArcScaleRange5, ArcScaleRange6})
        Me.ArcScaleComponent1.StartAngle = -180.0!
        Me.ArcScaleComponent1.Value = 22.0!
        '
        'ArcScaleNeedleComponent1
        '
        Me.ArcScaleNeedleComponent1.ArcScale = Me.ArcScaleComponent1
        Me.ArcScaleNeedleComponent1.EndOffset = 2.0!
        Me.ArcScaleNeedleComponent1.Name = "needle"
        Me.ArcScaleNeedleComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.NeedleShapeType.CircularFull_Style20
        Me.ArcScaleNeedleComponent1.StartOffset = -39.0!
        Me.ArcScaleNeedleComponent1.ZOrder = -50
        '
        'ArcScaleSpindleCapComponent1
        '
        Me.ArcScaleSpindleCapComponent1.ArcScale = Me.ArcScaleComponent1
        Me.ArcScaleSpindleCapComponent1.Name = "circularGauge1_SpindleCap1"
        Me.ArcScaleSpindleCapComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.SpindleCapShapeType.CircularFull_Style20
        Me.ArcScaleSpindleCapComponent1.Size = New System.Drawing.SizeF(10.0!, 10.0!)
        Me.ArcScaleSpindleCapComponent1.ZOrder = -100
        '
        'LabelGrafica
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.LabelGrafica, DevExpress.Utils.DefaultBoolean.[Default])
        Me.LabelGrafica.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabelGrafica.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelGrafica.Font = New System.Drawing.Font("Tahoma", 13.8!, System.Drawing.FontStyle.Bold)
        Me.LabelGrafica.Location = New System.Drawing.Point(292, 0)
        Me.LabelGrafica.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LabelGrafica.Name = "LabelGrafica"
        Me.LabelGrafica.Size = New System.Drawing.Size(758, 41)
        Me.LabelGrafica.TabIndex = 3
        Me.LabelGrafica.Text = "Dispersión"
        Me.LabelGrafica.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtUbicacionesVacias
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.txtUbicacionesVacias, DevExpress.Utils.DefaultBoolean.[Default])
        Me.txtUbicacionesVacias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUbicacionesVacias.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtUbicacionesVacias.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.txtUbicacionesVacias.Location = New System.Drawing.Point(2, 294)
        Me.txtUbicacionesVacias.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.txtUbicacionesVacias.Name = "txtUbicacionesVacias"
        Me.txtUbicacionesVacias.Size = New System.Drawing.Size(265, 52)
        Me.txtUbicacionesVacias.TabIndex = 5
        Me.txtUbicacionesVacias.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tabInidcadores
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.tabInidcadores, DevExpress.Utils.DefaultBoolean.[Default])
        Me.tabInidcadores.Caption = "Indicadores"
        Me.tabInidcadores.Controls.Add(Me.dgridInidicesRotacion)
        Me.tabInidcadores.Margin = New System.Windows.Forms.Padding(4)
        Me.tabInidcadores.Name = "tabInidcadores"
        Me.tabInidcadores.PageVisible = False
        Me.tabInidcadores.Size = New System.Drawing.Size(1113, 483)
        '
        'dgridInidicesRotacion
        '
        Me.dgridInidicesRotacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridInidicesRotacion.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridInidicesRotacion.Location = New System.Drawing.Point(0, 0)
        Me.dgridInidicesRotacion.MainView = Me.GridView2
        Me.dgridInidicesRotacion.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridInidicesRotacion.MenuManager = Me.RibbonControl
        Me.dgridInidicesRotacion.Name = "dgridInidicesRotacion"
        Me.dgridInidicesRotacion.Size = New System.Drawing.Size(1113, 483)
        Me.dgridInidicesRotacion.TabIndex = 0
        Me.dgridInidicesRotacion.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.DetailHeight = 444
        Me.GridView2.GridControl = Me.dgridInidicesRotacion
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsEditForm.PopupEditFormWidth = 857
        '
        'TabDashPicking
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.TabDashPicking, DevExpress.Utils.DefaultBoolean.[Default])
        Me.TabDashPicking.Caption = "Tablero de picking"
        Me.TabDashPicking.Controls.Add(Me.dvPicking)
        Me.TabDashPicking.Margin = New System.Windows.Forms.Padding(4)
        Me.TabDashPicking.Name = "TabDashPicking"
        Me.TabDashPicking.PageVisible = False
        Me.TabDashPicking.Size = New System.Drawing.Size(1113, 483)
        '
        'dvPicking
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.dvPicking, DevExpress.Utils.DefaultBoolean.[Default])
        Me.dvPicking.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.dvPicking.Appearance.Options.UseBackColor = True
        Me.dvPicking.AsyncMode = True
        Me.dvPicking.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dvPicking.Location = New System.Drawing.Point(0, 0)
        Me.dvPicking.Margin = New System.Windows.Forms.Padding(4)
        Me.dvPicking.Name = "dvPicking"
        Me.dvPicking.Size = New System.Drawing.Size(1113, 483)
        Me.dvPicking.TabIndex = 0
        '
        'tabIndicadoresBodProp
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.tabIndicadoresBodProp, DevExpress.Utils.DefaultBoolean.[Default])
        Me.tabIndicadoresBodProp.Caption = "Indicadores"
        Me.tabIndicadoresBodProp.Controls.Add(Me.TableLayoutPanel1)
        Me.tabIndicadoresBodProp.Controls.Add(Me.GroupControl2)
        Me.tabIndicadoresBodProp.Margin = New System.Windows.Forms.Padding(4)
        Me.tabIndicadoresBodProp.Name = "tabIndicadoresBodProp"
        Me.tabIndicadoresBodProp.PageVisible = False
        Me.tabIndicadoresBodProp.Size = New System.Drawing.Size(1113, 483)
        '
        'TableLayoutPanel1
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.TableLayoutPanel1, DevExpress.Utils.DefaultBoolean.[Default])
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(879, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(234, 483)
        Me.TableLayoutPanel1.TabIndex = 17
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.Label1)
        Me.GroupControl2.Controls.Add(Me.cmbPropietario)
        Me.GroupControl2.Controls.Add(Me.GroupControl4)
        Me.GroupControl2.Controls.Add(Me.cmbBodega)
        Me.GroupControl2.Controls.Add(Me.lblBodega)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(879, 483)
        Me.GroupControl2.TabIndex = 8
        Me.GroupControl2.ToolTipController = Me.DefaultToolTipController1.DefaultController
        '
        'Label1
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.Label1, DevExpress.Utils.DefaultBoolean.[Default])
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 106)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Propietario:"
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbPropietario.Location = New System.Drawing.Point(7, 128)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPropietario.MenuManager = Me.RibbonControl
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Size = New System.Drawing.Size(450, 20)
        Me.cmbPropietario.TabIndex = 6
        '
        'GroupControl4
        '
        Me.GroupControl4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl4.Controls.Add(Me.dtpFechaAl)
        Me.GroupControl4.Controls.Add(Me.dtpFechaDel)
        Me.GroupControl4.Controls.Add(Me.lblAl)
        Me.GroupControl4.Controls.Add(Me.lblDel)
        Me.GroupControl4.Location = New System.Drawing.Point(9, 284)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(450, 46)
        Me.GroupControl4.TabIndex = 4
        Me.GroupControl4.Text = "Rango de Fechas"
        Me.GroupControl4.ToolTipController = Me.DefaultToolTipController1.DefaultController
        '
        'dtpFechaAl
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.dtpFechaAl, DevExpress.Utils.DefaultBoolean.[Default])
        Me.dtpFechaAl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpFechaAl.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaAl.Location = New System.Drawing.Point(39, 88)
        Me.dtpFechaAl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpFechaAl.Name = "dtpFechaAl"
        Me.dtpFechaAl.Size = New System.Drawing.Size(404, 21)
        Me.dtpFechaAl.TabIndex = 4
        '
        'dtpFechaDel
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.dtpFechaDel, DevExpress.Utils.DefaultBoolean.[Default])
        Me.dtpFechaDel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpFechaDel.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaDel.Location = New System.Drawing.Point(39, 42)
        Me.dtpFechaDel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpFechaDel.Name = "dtpFechaDel"
        Me.dtpFechaDel.Size = New System.Drawing.Size(404, 21)
        Me.dtpFechaDel.TabIndex = 4
        '
        'lblAl
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.lblAl, DevExpress.Utils.DefaultBoolean.[Default])
        Me.lblAl.AutoSize = True
        Me.lblAl.Location = New System.Drawing.Point(8, 96)
        Me.lblAl.Name = "lblAl"
        Me.lblAl.Size = New System.Drawing.Size(20, 13)
        Me.lblAl.TabIndex = 4
        Me.lblAl.Text = "Al:"
        '
        'lblDel
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.lblDel, DevExpress.Utils.DefaultBoolean.[Default])
        Me.lblDel.AutoSize = True
        Me.lblDel.Location = New System.Drawing.Point(9, 50)
        Me.lblDel.Name = "lblDel"
        Me.lblDel.Size = New System.Drawing.Size(26, 13)
        Me.lblDel.TabIndex = 4
        Me.lblDel.Text = "Del:"
        '
        'cmbBodega
        '
        Me.cmbBodega.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbBodega.Location = New System.Drawing.Point(7, 66)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbBodega.MenuManager = Me.RibbonControl
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(450, 20)
        Me.cmbBodega.TabIndex = 4
        '
        'lblBodega
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.lblBodega, DevExpress.Utils.DefaultBoolean.[Default])
        Me.lblBodega.AutoSize = True
        Me.lblBodega.Location = New System.Drawing.Point(7, 47)
        Me.lblBodega.Name = "lblBodega"
        Me.lblBodega.Size = New System.Drawing.Size(47, 13)
        Me.lblBodega.TabIndex = 4
        Me.lblBodega.Text = "Bodega:"
        '
        'lblProducto
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.lblProducto, DevExpress.Utils.DefaultBoolean.[Default])
        Me.lblProducto.AutoSize = True
        Me.lblProducto.Location = New System.Drawing.Point(8, 36)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(57, 16)
        Me.lblProducto.TabIndex = 7
        Me.lblProducto.TabStop = True
        '
        'TabNavigationPage1
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.TabNavigationPage1, DevExpress.Utils.DefaultBoolean.[Default])
        Me.TabNavigationPage1.Caption = "Información de sistema"
        Me.TabNavigationPage1.Controls.Add(Me.GroupControl3)
        Me.TabNavigationPage1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabNavigationPage1.Name = "TabNavigationPage1"
        Me.TabNavigationPage1.Size = New System.Drawing.Size(1491, 450)
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.chkConexionInternet)
        Me.GroupControl3.Controls.Add(Me.lblDotNetVersion)
        Me.GroupControl3.Controls.Add(Me.lblSqlServerVersion)
        Me.GroupControl3.Controls.Add(Me.lblAppMemoryUsage)
        Me.GroupControl3.Controls.Add(Me.lblAvailableMemory)
        Me.GroupControl3.Controls.Add(Me.lblUsedMemory)
        Me.GroupControl3.Controls.Add(Me.lblProcessor)
        Me.GroupControl3.Controls.Add(Me.lblMacAddress)
        Me.GroupControl3.Controls.Add(Me.lblSerialNumber)
        Me.GroupControl3.Controls.Add(Me.lblDiskSpace)
        Me.GroupControl3.Controls.Add(Me.lblOSVersion)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(1491, 450)
        Me.GroupControl3.TabIndex = 0
        Me.GroupControl3.Text = "Información de sistema"
        Me.GroupControl3.ToolTipController = Me.DefaultToolTipController1.DefaultController
        '
        'chkConexionInternet
        '
        Me.chkConexionInternet.Location = New System.Drawing.Point(45, 335)
        Me.chkConexionInternet.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkConexionInternet.MenuManager = Me.RibbonControl
        Me.chkConexionInternet.Name = "chkConexionInternet"
        Me.chkConexionInternet.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.chkConexionInternet.Properties.Appearance.Options.UseFont = True
        Me.chkConexionInternet.Properties.Appearance.Options.UseTextOptions = True
        Me.chkConexionInternet.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.chkConexionInternet.Properties.OffText = "Sin conexión a internet"
        Me.chkConexionInternet.Properties.OnText = "Conectado a internet"
        Me.chkConexionInternet.Properties.ReadOnly = True
        Me.chkConexionInternet.Size = New System.Drawing.Size(248, 21)
        Me.chkConexionInternet.TabIndex = 1
        '
        'lblDotNetVersion
        '
        Me.lblDotNetVersion.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDotNetVersion.Appearance.Options.UseFont = True
        Me.lblDotNetVersion.Appearance.Options.UseTextOptions = True
        Me.lblDotNetVersion.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.lblDotNetVersion.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lblDotNetVersion.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.lblDotNetVersion.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal
        Me.lblDotNetVersion.Location = New System.Drawing.Point(45, 304)
        Me.lblDotNetVersion.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblDotNetVersion.Name = "lblDotNetVersion"
        Me.lblDotNetVersion.Size = New System.Drawing.Size(104, 17)
        Me.lblDotNetVersion.TabIndex = 9
        Me.lblDotNetVersion.Text = "lblVersionDotNET"
        '
        'lblSqlServerVersion
        '
        Me.lblSqlServerVersion.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSqlServerVersion.Appearance.Options.UseFont = True
        Me.lblSqlServerVersion.Appearance.Options.UseTextOptions = True
        Me.lblSqlServerVersion.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.lblSqlServerVersion.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lblSqlServerVersion.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.lblSqlServerVersion.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblSqlServerVersion.Location = New System.Drawing.Point(45, 220)
        Me.lblSqlServerVersion.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblSqlServerVersion.Name = "lblSqlServerVersion"
        Me.lblSqlServerVersion.Size = New System.Drawing.Size(446, 72)
        Me.lblSqlServerVersion.TabIndex = 8
        Me.lblSqlServerVersion.Text = "lblSqlServerVersion"
        '
        'lblAppMemoryUsage
        '
        Me.lblAppMemoryUsage.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppMemoryUsage.Appearance.Options.UseFont = True
        Me.lblAppMemoryUsage.Location = New System.Drawing.Point(45, 198)
        Me.lblAppMemoryUsage.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblAppMemoryUsage.Name = "lblAppMemoryUsage"
        Me.lblAppMemoryUsage.Size = New System.Drawing.Size(123, 17)
        Me.lblAppMemoryUsage.TabIndex = 7
        Me.lblAppMemoryUsage.Text = "lblAppMemoryUsage"
        '
        'lblAvailableMemory
        '
        Me.lblAvailableMemory.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvailableMemory.Appearance.Options.UseFont = True
        Me.lblAvailableMemory.Location = New System.Drawing.Point(45, 176)
        Me.lblAvailableMemory.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblAvailableMemory.Name = "lblAvailableMemory"
        Me.lblAvailableMemory.Size = New System.Drawing.Size(113, 17)
        Me.lblAvailableMemory.TabIndex = 6
        Me.lblAvailableMemory.Text = "lblAvailableMemory"
        '
        'lblUsedMemory
        '
        Me.lblUsedMemory.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsedMemory.Appearance.Options.UseFont = True
        Me.lblUsedMemory.Location = New System.Drawing.Point(45, 154)
        Me.lblUsedMemory.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblUsedMemory.Name = "lblUsedMemory"
        Me.lblUsedMemory.Size = New System.Drawing.Size(92, 17)
        Me.lblUsedMemory.TabIndex = 5
        Me.lblUsedMemory.Text = "lblUsedMemory"
        '
        'lblProcessor
        '
        Me.lblProcessor.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProcessor.Appearance.Options.UseFont = True
        Me.lblProcessor.Location = New System.Drawing.Point(45, 132)
        Me.lblProcessor.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblProcessor.Name = "lblProcessor"
        Me.lblProcessor.Size = New System.Drawing.Size(72, 17)
        Me.lblProcessor.TabIndex = 4
        Me.lblProcessor.Text = "lblProcessor"
        '
        'lblMacAddress
        '
        Me.lblMacAddress.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMacAddress.Appearance.Options.UseFont = True
        Me.lblMacAddress.Location = New System.Drawing.Point(45, 110)
        Me.lblMacAddress.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblMacAddress.Name = "lblMacAddress"
        Me.lblMacAddress.Size = New System.Drawing.Size(84, 17)
        Me.lblMacAddress.TabIndex = 3
        Me.lblMacAddress.Text = "lblMacAddress"
        '
        'lblSerialNumber
        '
        Me.lblSerialNumber.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSerialNumber.Appearance.Options.UseFont = True
        Me.lblSerialNumber.Location = New System.Drawing.Point(45, 89)
        Me.lblSerialNumber.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblSerialNumber.Name = "lblSerialNumber"
        Me.lblSerialNumber.Size = New System.Drawing.Size(92, 17)
        Me.lblSerialNumber.TabIndex = 2
        Me.lblSerialNumber.Text = "lblSerialNumber"
        '
        'lblDiskSpace
        '
        Me.lblDiskSpace.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiskSpace.Appearance.Options.UseFont = True
        Me.lblDiskSpace.Location = New System.Drawing.Point(45, 67)
        Me.lblDiskSpace.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblDiskSpace.Name = "lblDiskSpace"
        Me.lblDiskSpace.Size = New System.Drawing.Size(74, 17)
        Me.lblDiskSpace.TabIndex = 1
        Me.lblDiskSpace.Text = "lblDiskSpace"
        '
        'lblOSVersion
        '
        Me.lblOSVersion.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOSVersion.Appearance.Options.UseFont = True
        Me.lblOSVersion.Location = New System.Drawing.Point(45, 45)
        Me.lblOSVersion.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblOSVersion.Name = "lblOSVersion"
        Me.lblOSVersion.Size = New System.Drawing.Size(74, 17)
        Me.lblOSVersion.TabIndex = 0
        Me.lblOSVersion.Text = "lblOSVersion"
        '
        'Calendario
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me.Calendario, DevExpress.Utils.DefaultBoolean.[Default])
        Me.Calendario.Caption = "Calendario"
        Me.Calendario.Controls.Add(Me.SchedulerControl1)
        Me.Calendario.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Calendario.Name = "Calendario"
        Me.Calendario.Size = New System.Drawing.Size(1491, 450)
        '
        'SchedulerControl1
        '
        Me.SchedulerControl1.DataStorage = Me.SchedulerDataStorage1
        Me.SchedulerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SchedulerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SchedulerControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SchedulerControl1.MenuManager = Me.RibbonControl
        Me.SchedulerControl1.Name = "SchedulerControl1"
        Me.SchedulerControl1.Size = New System.Drawing.Size(1491, 450)
        Me.SchedulerControl1.Start = New Date(2024, 6, 12, 0, 0, 0, 0)
        Me.SchedulerControl1.TabIndex = 0
        Me.SchedulerControl1.Text = "SchedulerControl1"
        Me.SchedulerControl1.ToolTipController = Me.ToolTipController1
        Me.SchedulerControl1.Views.DayView.TimeRulers.Add(TimeRuler3)
        Me.SchedulerControl1.Views.FullWeekView.Enabled = True
        Me.SchedulerControl1.Views.FullWeekView.TimeRulers.Add(TimeRuler4)
        Me.SchedulerControl1.Views.WeekView.Enabled = False
        Me.SchedulerControl1.Views.WorkWeekView.TimeRulers.Add(TimeRuler10)
        Me.SchedulerControl1.Views.YearView.UseOptimizedScrolling = False
        '
        'SchedulerDataStorage1
        '
        '
        '
        '
        Me.SchedulerDataStorage1.AppointmentDependencies.AutoReload = False
        '
        '
        '
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(0, "None", "&None", System.Drawing.SystemColors.Window)
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(1, "Important", "&Important", System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(190, Byte), Integer)))
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(2, "Business", "&Business", System.Drawing.Color.FromArgb(CType(CType(168, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(255, Byte), Integer)))
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(3, "Personal", "&Personal", System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(156, Byte), Integer)))
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(4, "Vacation", "&Vacation", System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(199, Byte), Integer)))
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(5, "Must Attend", "Must &Attend", System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(147, Byte), Integer)))
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(6, "Travel Required", "&Travel Required", System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(255, Byte), Integer)))
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(7, "Needs Preparation", "&Needs Preparation", System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(152, Byte), Integer)))
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(8, "Birthday", "&Birthday", System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(233, Byte), Integer)))
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(9, "Anniversary", "&Anniversary", System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(223, Byte), Integer)))
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(10, "Phone Call", "Phone &Call", System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(165, Byte), Integer)))
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.txtUbicacionesVacias)
        Me.GroupControl1.Controls.Add(Me.lblUbicacionesVacias)
        Me.GroupControl1.Controls.Add(Me.txtUbicacionesOcupadas)
        Me.GroupControl1.Controls.Add(Me.lblUbicacionesOcupadas)
        Me.GroupControl1.Controls.Add(Me.txtCantidadPosiciones)
        Me.GroupControl1.Controls.Add(Me.lblCantidadPosiciones)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(1059, 45)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(269, 401)
        Me.GroupControl1.TabIndex = 4
        Me.GroupControl1.ToolTipController = Me.DefaultToolTipController1.DefaultController
        '
        'lblUbicacionesVacias
        '
        Me.lblUbicacionesVacias.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUbicacionesVacias.Appearance.Options.UseFont = True
        Me.lblUbicacionesVacias.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblUbicacionesVacias.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblUbicacionesVacias.Location = New System.Drawing.Point(2, 237)
        Me.lblUbicacionesVacias.Margin = New System.Windows.Forms.Padding(4)
        Me.lblUbicacionesVacias.Name = "lblUbicacionesVacias"
        Me.lblUbicacionesVacias.Size = New System.Drawing.Size(265, 57)
        Me.lblUbicacionesVacias.TabIndex = 4
        Me.lblUbicacionesVacias.Text = "Vacías"
        '
        'txtUbicacionesOcupadas
        '
        Me.txtUbicacionesOcupadas.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtUbicacionesOcupadas.Location = New System.Drawing.Point(2, 183)
        Me.txtUbicacionesOcupadas.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUbicacionesOcupadas.MenuManager = Me.RibbonControl
        Me.txtUbicacionesOcupadas.Name = "txtUbicacionesOcupadas"
        Me.txtUbicacionesOcupadas.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUbicacionesOcupadas.Properties.Appearance.Options.UseFont = True
        Me.txtUbicacionesOcupadas.Properties.Appearance.Options.UseTextOptions = True
        Me.txtUbicacionesOcupadas.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtUbicacionesOcupadas.Properties.AutoHeight = False
        Me.txtUbicacionesOcupadas.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtUbicacionesOcupadas.Properties.ReadOnly = True
        Me.txtUbicacionesOcupadas.Size = New System.Drawing.Size(265, 54)
        Me.txtUbicacionesOcupadas.TabIndex = 3
        '
        'lblUbicacionesOcupadas
        '
        Me.lblUbicacionesOcupadas.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUbicacionesOcupadas.Appearance.Options.UseFont = True
        Me.lblUbicacionesOcupadas.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblUbicacionesOcupadas.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblUbicacionesOcupadas.Location = New System.Drawing.Point(2, 126)
        Me.lblUbicacionesOcupadas.Margin = New System.Windows.Forms.Padding(4)
        Me.lblUbicacionesOcupadas.Name = "lblUbicacionesOcupadas"
        Me.lblUbicacionesOcupadas.Size = New System.Drawing.Size(265, 57)
        Me.lblUbicacionesOcupadas.TabIndex = 2
        Me.lblUbicacionesOcupadas.Text = "Ocupadas"
        '
        'txtCantidadPosiciones
        '
        Me.txtCantidadPosiciones.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtCantidadPosiciones.Location = New System.Drawing.Point(2, 80)
        Me.txtCantidadPosiciones.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCantidadPosiciones.MenuManager = Me.RibbonControl
        Me.txtCantidadPosiciones.Name = "txtCantidadPosiciones"
        Me.txtCantidadPosiciones.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidadPosiciones.Properties.Appearance.Options.UseFont = True
        Me.txtCantidadPosiciones.Properties.Appearance.Options.UseTextOptions = True
        Me.txtCantidadPosiciones.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtCantidadPosiciones.Properties.AutoHeight = False
        Me.txtCantidadPosiciones.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCantidadPosiciones.Properties.ReadOnly = True
        Me.txtCantidadPosiciones.Size = New System.Drawing.Size(265, 46)
        Me.txtCantidadPosiciones.TabIndex = 1
        '
        'lblCantidadPosiciones
        '
        Me.lblCantidadPosiciones.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCantidadPosiciones.Appearance.Options.UseFont = True
        Me.lblCantidadPosiciones.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblCantidadPosiciones.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblCantidadPosiciones.Location = New System.Drawing.Point(2, 23)
        Me.lblCantidadPosiciones.Margin = New System.Windows.Forms.Padding(4)
        Me.lblCantidadPosiciones.Name = "lblCantidadPosiciones"
        Me.lblCantidadPosiciones.Size = New System.Drawing.Size(265, 57)
        Me.lblCantidadPosiciones.TabIndex = 0
        Me.lblCantidadPosiciones.Text = "Posiciones"
        '
        'tmrTareas
        '
        Me.tmrTareas.Interval = 1000
        '
        'bgwTareas
        '
        '
        'frmPrincipal02
        '
        Me.DefaultToolTipController1.SetAllowHtmlText(Me, DevExpress.Utils.DefaultBoolean.[Default])
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1332, 665)
        Me.Controls.Add(Me.TabPane1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmPrincipal02"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = " Monitor"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgridTareas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SchedulerM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TareahhBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DSCalendarioTarea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BodegamuellesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResourcesPopupCheckedListBoxControl3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResourcesPopupCheckedListBoxControl3.ResourcesCheckedListBoxControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl7.ResumeLayout(False)
        CType(Me.SchedulerP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateNavigator4.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateNavigator4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResourcesPopupCheckedListBoxControl4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResourcesPopupCheckedListBoxControl4.ResourcesCheckedListBoxControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PropietariosBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabPane1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPane1.ResumeLayout(False)
        Me.TabTareas.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        Me.TabRellenado.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dgridRellenado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvdRellenado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgridTareasPendientesReabasto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvReabastoPendiente, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabTareasMuelle.ResumeLayout(False)
        CType(Me.PanelControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl6.ResumeLayout(False)
        CType(Me.DateNavigator3.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateNavigator3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabTareasCalendario.ResumeLayout(False)
        Me.tabOcupacion.ResumeLayout(False)
        Me.TableLayoutPanel.ResumeLayout(False)
        CType(Me.ccOcupacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cgOcupacionBodega, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleBackgroundLayerComponent1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleComponent1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleNeedleComponent1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleSpindleCapComponent1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabInidcadores.ResumeLayout(False)
        CType(Me.dgridInidicesRotacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabDashPicking.ResumeLayout(False)
        CType(Me.dvPicking, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabIndicadoresBodProp.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabNavigationPage1.ResumeLayout(False)
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.GroupControl3.PerformLayout()
        CType(Me.chkConexionInternet.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Calendario.ResumeLayout(False)
        CType(Me.SchedulerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SchedulerDataStorage1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.txtUbicacionesOcupadas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidadPosiciones.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents DSCalendarioTarea As TOMWMS.DSCalendarioTarea
    Friend WithEvents TareahhBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Tarea_hhTableAdapter As TOMWMS.DSCalendarioTareaTableAdapters.tarea_hhTableAdapter
    Friend WithEvents BodegamuellesBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Bodega_muellesTableAdapter As TOMWMS.DSCalendarioTareaTableAdapters.bodega_muellesTableAdapter
    Friend WithEvents PropietariosBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents PropietariosTableAdapter As TOMWMS.DSCalendarioTareaTableAdapters.propietariosTableAdapter
    Friend WithEvents SchedulerM As DevExpress.XtraScheduler.SchedulerControl
    Friend WithEvents ResourcesPopupCheckedListBoxControl3 As DevExpress.XtraScheduler.UI.ResourcesPopupCheckedListBoxControl
    Friend WithEvents PanelControl7 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents SchedulerP As DevExpress.XtraScheduler.SchedulerControl
    Friend WithEvents DateNavigator4 As DevExpress.XtraScheduler.DateNavigator
    Friend WithEvents ResourcesPopupCheckedListBoxControl4 As DevExpress.XtraScheduler.UI.ResourcesPopupCheckedListBoxControl
    Friend WithEvents DgridTareas As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizarFromMenu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents TabPane1 As DevExpress.XtraBars.Navigation.TabPane
    Friend WithEvents TabTareas As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents TabRellenado As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents TabTareasMuelle As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents PanelControl6 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents DateNavigator3 As DevExpress.XtraScheduler.DateNavigator
    Friend WithEvents TabTareasCalendario As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents mnuPosponerTodo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuPosponerSeleccionados As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarTarea As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents dtpFechaInicio As DateTimePicker
    Friend WithEvents dtpFechaFin As DateTimePicker
    Friend WithEvents tmrTareas As Timer
    Friend WithEvents bgwTareas As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblprg As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents BarButtonGroup1 As DevExpress.XtraBars.BarButtonGroup
    Friend WithEvents tabOcupacion As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents TableLayoutPanel As TableLayoutPanel
    Friend WithEvents GaugeControl As DevExpress.XtraGauges.Win.GaugeControl
    Friend WithEvents cgOcupacionBodega As DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge
    Private WithEvents ArcScaleBackgroundLayerComponent1 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent
    Private WithEvents ArcScaleComponent1 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent
    Private WithEvents ArcScaleNeedleComponent1 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent
    Private WithEvents ArcScaleSpindleCapComponent1 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent
    Friend WithEvents ccOcupacion As DevExpress.XtraCharts.ChartControl
    Friend WithEvents lblGauguePorcentajeOcupacion As Label
    Friend WithEvents LabelGrafica As Label
    Friend WithEvents mnuDashBoard As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents dgridRellenado As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvdRellenado As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BarSubItem1 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuReabastecimiento As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup4 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents prg As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents mnuCalcularIndicesRotacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup5 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents tabInidcadores As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents dgridInidicesRotacion As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents mnuExcluirArribaDelMinimo As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents mnuExcluirArribaDeMaximo As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents rpgReabasto As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuExcluirSinExistencia As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents mnuExcluirStockInferior As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents mnuTimerMonitor As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblCantPosiciones As Label
    Friend WithEvents lblUbicacionesVacias As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtUbicacionesOcupadas As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblUbicacionesOcupadas As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCantidadPosiciones As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblCantidadPosiciones As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TabDashPicking As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents dvPicking As DevExpress.DashboardWin.DashboardViewer
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents dgridTareasPendientesReabasto As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvReabastoPendiente As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkOcultarTareasPendientesReabasto As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents txtUbicacionesVacias As LinkLabel
    Friend WithEvents DefaultToolTipController1 As DevExpress.Utils.DefaultToolTipController
    Friend WithEvents ToolTipController1 As DevExpress.Utils.ToolTipController
    Friend WithEvents chkExcluirUbicacionDestinoLlena As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents txtprgreabasto As RichTextBox
    Friend WithEvents mnuOcultarLogReabasto As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tabIndicadoresBodProp As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtpFechaAl As DateTimePicker
    Friend WithEvents dtpFechaDel As DateTimePicker
    Friend WithEvents lblAl As Label
    Friend WithEvents lblDel As Label
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblBodega As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lblProducto As LinkLabel
    Friend WithEvents TabNavigationPage1 As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblProcessor As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblMacAddress As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblSerialNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDiskSpace As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblOSVersion As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblAppMemoryUsage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblAvailableMemory As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblUsedMemory As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblSqlServerVersion As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDotNetVersion As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkConexionInternet As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents Calendario As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents SchedulerControl1 As DevExpress.XtraScheduler.SchedulerControl
    Friend WithEvents SchedulerDataStorage1 As DevExpress.XtraScheduler.SchedulerDataStorage
End Class
