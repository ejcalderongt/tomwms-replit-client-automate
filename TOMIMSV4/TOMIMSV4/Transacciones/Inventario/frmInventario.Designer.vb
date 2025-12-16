<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmInventario
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
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInventario))
        Dim PieSeriesView1 As DevExpress.XtraCharts.PieSeriesView = New DevExpress.XtraCharts.PieSeriesView()
        Dim ArcScaleRange1 As DevExpress.XtraGauges.Core.Model.ArcScaleRange = New DevExpress.XtraGauges.Core.Model.ArcScaleRange()
        Dim ArcScaleRange2 As DevExpress.XtraGauges.Core.Model.ArcScaleRange = New DevExpress.XtraGauges.Core.Model.ArcScaleRange()
        Dim ArcScaleRange3 As DevExpress.XtraGauges.Core.Model.ArcScaleRange = New DevExpress.XtraGauges.Core.Model.ArcScaleRange()
        Dim PieSeriesView2 As DevExpress.XtraCharts.PieSeriesView = New DevExpress.XtraCharts.PieSeriesView()
        Dim PushTransition1 As DevExpress.Utils.Animation.PushTransition = New DevExpress.Utils.Animation.PushTransition()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdActualizarInvInicial = New DevExpress.XtraBars.BarButtonItem()
        Me.BarCheckItem1 = New DevExpress.XtraBars.BarCheckItem()
        Me.cmbSector = New DevExpress.XtraBars.BarEditItem()
        Me.cmbSector1 = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.rgrp = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdCompracionStock = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdConvertir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdReconteo = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.lblReg = New DevExpress.XtraBars.BarStaticItem()
        Me.lblRe = New DevExpress.XtraBars.BarStaticItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarSubItem()
        Me.cmdImprimirGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimirdetalle = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimirporoperador = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimirReconteo = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegsRec = New DevExpress.XtraBars.BarStaticItem()
        Me.cmdActualizarInventario = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImprimirInicial = New DevExpress.XtraBars.BarSubItem()
        Me.cmdImprimirConteo = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimirVerifi = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimirComparacion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImprimirComparacionERP = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuExportarExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdAplicarAjustesFecha = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.chkComparativoConUbicacion = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.mnuImportarTeoricoERP = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rgprImportar = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpActualizar = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgComparacion = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgReconteo = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgImprimir = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.grpImprimirInicial = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgExportar = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.grpRegularizarInvStock = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rgrpRegularizar = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgAplicarAjustesFecha = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgKPI = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar1 = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.tabDetalle = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.grpConteo = New System.Windows.Forms.GroupBox()
        Me.grdConteo = New DevExpress.XtraGrid.GridControl()
        Me.gviewConteo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grpVerificac = New System.Windows.Forms.GroupBox()
        Me.grdVerifica = New DevExpress.XtraGrid.GridControl()
        Me.gviewVerifica = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grpComparacion = New System.Windows.Forms.GroupBox()
        Me.dgridComparativoInvInicial = New DevExpress.XtraGrid.GridControl()
        Me.gviewComparativo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grpFiltros = New DevExpress.XtraEditors.GroupControl()
        Me.lnkUbicacionInvInicial = New System.Windows.Forms.LinkLabel()
        Me.txtIdUbicacionInvInicial = New DevExpress.XtraEditors.TextEdit()
        Me.chkUbicCompleta = New DevExpress.XtraEditors.CheckEdit()
        Me.btnLimpiar = New DevExpress.XtraEditors.SimpleButton()
        Me.txtPropietarioId = New System.Windows.Forms.TextBox()
        Me.txtProductoId = New System.Windows.Forms.TextBox()
        Me.txtTramoId = New System.Windows.Forms.TextBox()
        Me.linkPropietario = New System.Windows.Forms.LinkLabel()
        Me.linkProducto = New System.Windows.Forms.LinkLabel()
        Me.linkTramo = New System.Windows.Forms.LinkLabel()
        Me.txtPropietarioNombre = New System.Windows.Forms.TextBox()
        Me.txtTramoNombre = New System.Windows.Forms.TextBox()
        Me.txtProductoNombre = New System.Windows.Forms.TextBox()
        Me.Datos = New DevExpress.XtraTab.XtraTabPage()
        Me.grpInven = New DevExpress.XtraEditors.GroupControl()
        Me.chkCapturarNoAsignado = New DevExpress.XtraEditors.CheckEdit()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbCentroCosto = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblCentroCosto = New System.Windows.Forms.Label()
        Me.chkMultiPropietario = New DevExpress.XtraEditors.CheckEdit()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkCaptNtExist = New DevExpress.XtraEditors.CheckEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbCliente = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.cmbProductoFamilia = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblSeccionAjuste = New System.Windows.Forms.Label()
        Me.lblMostrarCantidad = New System.Windows.Forms.Label()
        Me.chkMostrarCantidad = New DevExpress.XtraEditors.CheckEdit()
        Me.chkCambiaUbicacion = New DevExpress.XtraEditors.CheckEdit()
        Me.lblCambiaUbicacion = New System.Windows.Forms.Label()
        Me.lblUltInv = New System.Windows.Forms.Label()
        Me.dtpUltimoInv = New DevExpress.XtraEditors.DateEdit()
        Me.lblPrg = New System.Windows.Forms.Label()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.lblEsSistema = New System.Windows.Forms.Label()
        Me.chkSistema = New DevExpress.XtraEditors.CheckEdit()
        Me.dtpHoraFin = New DevExpress.XtraEditors.DateEdit()
        Me.dtpHoraInicio = New DevExpress.XtraEditors.DateEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.cmbTipoConteo = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbTipoInventario = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblFin = New System.Windows.Forms.Label()
        Me.lblInicio = New System.Windows.Forms.Label()
        Me.lblActivo = New System.Windows.Forms.Label()
        Me.chkDobleVerifica = New DevExpress.XtraEditors.CheckEdit()
        Me.lblDobleVerif = New System.Windows.Forms.Label()
        Me.lblTipoConteo = New System.Windows.Forms.Label()
        Me.lblTipoInventario = New System.Windows.Forms.Label()
        Me.lblFecha = New System.Windows.Forms.Label()
        Me.Fecha = New DevExpress.XtraEditors.DateEdit()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.Estado = New System.Windows.Forms.Label()
        Me.lblCod = New System.Windows.Forms.Label()
        Me.lblPropietario = New System.Windows.Forms.Label()
        Me.lblBodega = New System.Windows.Forms.Label()
        Me.lblEstado = New System.Windows.Forms.Label()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.xtraTabInv = New DevExpress.XtraTab.XtraTabControl()
        Me.tabAsignacionProductos = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.lblUbicacionesUnicas = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblProductosUnicos = New System.Windows.Forms.TextBox()
        Me.lblRegistros = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dgridAsignacionProductos = New DevExpress.XtraTreeList.TreeList()
        Me.grpProductos = New DevExpress.XtraEditors.GroupControl()
        Me.cmdQuitaOpProd = New DevExpress.XtraEditors.SimpleButton()
        Me.cmdAsignaOpProd = New DevExpress.XtraEditors.SimpleButton()
        Me.twTodos = New DevExpress.XtraEditors.ToggleSwitch()
        Me.cmbOperadorProd = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdEliminarOpProd = New DevExpress.XtraEditors.SimpleButton()
        Me.cmdAsignarOp = New DevExpress.XtraEditors.SimpleButton()
        Me.cmdQuitarProducto = New DevExpress.XtraEditors.SimpleButton()
        Me.cmdAgregarProducto = New DevExpress.XtraEditors.SimpleButton()
        Me.Tramos = New DevExpress.XtraTab.XtraTabPage()
        Me.grpTramos = New DevExpress.XtraEditors.GroupControl()
        Me.chkSeleccionarTodos = New System.Windows.Forms.CheckBox()
        Me.chkTramosAsig = New System.Windows.Forms.CheckBox()
        Me.dgridAsignacionTramos = New DevExpress.XtraGrid.GridControl()
        Me.GridViewTramos = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSeleccionar = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colIdTramos = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.coldIdSector = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.coldTramo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabAsignacionOperadores = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridAsignacionOperadores = New DevExpress.XtraTreeList.TreeList()
        Me.grpOperadores = New DevExpress.XtraEditors.GroupControl()
        Me.cmdQuitar = New DevExpress.XtraEditors.SimpleButton()
        Me.cmdQuitarOperador = New DevExpress.XtraEditors.SimpleButton()
        Me.cmdAgregar = New DevExpress.XtraEditors.SimpleButton()
        Me.cmdAsignarOperador = New DevExpress.XtraEditors.SimpleButton()
        Me.cmbOperador = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblOperador = New System.Windows.Forms.Label()
        Me.tabConteo = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridInventarioCiclico = New DevExpress.XtraGrid.GridControl()
        Me.gdviewTeorico = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grpConteoCi = New DevExpress.XtraEditors.GroupControl()
        Me.prgPanInvConteo = New System.Windows.Forms.ProgressBar()
        Me.LinklblUbicacion = New System.Windows.Forms.LinkLabel()
        Me.txtNombreUbicacion = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdUbicacion = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreProducto = New DevExpress.XtraEditors.TextEdit()
        Me.LinklblProducto = New System.Windows.Forms.LinkLabel()
        Me.txtIdProducto = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreOperador = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdOperador = New DevExpress.XtraEditors.TextEdit()
        Me.LinklblOperador = New System.Windows.Forms.LinkLabel()
        Me.txtNombreTramo = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdTramo = New DevExpress.XtraEditors.TextEdit()
        Me.txtClasificacionNombre = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdClasificacion = New DevExpress.XtraEditors.TextEdit()
        Me.txtFamiliaNombre = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdFamilia = New DevExpress.XtraEditors.TextEdit()
        Me.linklblTramo = New System.Windows.Forms.LinkLabel()
        Me.linklblClasificacion = New System.Windows.Forms.LinkLabel()
        Me.linklblFamilia = New System.Windows.Forms.LinkLabel()
        Me.tabDiferenciasInventario = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridDiferenciasCiclico = New DevExpress.XtraGrid.GridControl()
        Me.gvDiferenciasCiclico = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabConteoOperador = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridConteoOperador = New DevExpress.XtraGrid.GridControl()
        Me.gvConteoOperador = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabReconteo = New DevExpress.XtraTab.XtraTabPage()
        Me.grpReconteo = New DevExpress.XtraEditors.GroupControl()
        Me.grdReconteo = New DevExpress.XtraGrid.GridControl()
        Me.EncabezadoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DSReconteoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DSReconteo = New TOMWMS.DSReconteo()
        Me.GridView8 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colCorrelativo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdReconteoEnc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colEstado = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colHora_Inicio = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colHora_Fin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdInventarioEnc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.tabInvTeorico = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridInvTeorico = New DevExpress.XtraGrid.GridControl()
        Me.gvInvTeoricoWMS = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabComparativoERPWMS = New DevExpress.XtraTab.XtraTabPage()
        Me.chkLoteVence = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkConUbicacion = New DevExpress.XtraEditors.ToggleSwitch()
        Me.dgridcomparativoerpwms = New DevExpress.XtraGrid.GridControl()
        Me.gvInvTeoricoERP = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tbne = New DevExpress.XtraTab.XtraTabPage()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.grdNE = New DevExpress.XtraGrid.GridControl()
        Me.GridView10 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabInvCongelado = New DevExpress.XtraTab.XtraTabPage()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.dgridCongelado = New DevExpress.XtraGrid.GridControl()
        Me.GridView9 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TabInventarioCostos = New DevExpress.XtraTab.XtraTabPage()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.grdCostos = New DevExpress.XtraGrid.GridControl()
        Me.GridView7 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabKPI = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.charcUniverso = New DevExpress.XtraCharts.ChartControl()
        Me.SplitContainerControl2 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblRegsCont = New DevExpress.XtraEditors.LabelControl()
        Me.gcRegistros = New DevExpress.XtraGauges.Win.GaugeControl()
        Me.circularGauge1 = New DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge()
        Me.ArcScaleBackgroundLayerComponent1 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent()
        Me.ArcScaleComponent1 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent()
        Me.ArcScaleNeedleComponent1 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent()
        Me.ArcScaleSpindleCapComponent1 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent()
        Me.lblRegistrosContados = New DevExpress.XtraEditors.LabelControl()
        Me.chartcEstratoTipo = New DevExpress.XtraCharts.ChartControl()
        Me.tabAsignacionUbicaciones = New DevExpress.XtraTab.XtraTabPage()
        Me.grpUbicaciones = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.dgridAsignacionUbicaciones = New DevExpress.XtraTreeList.TreeList()
        Me.btnFiltLimpia = New DevExpress.XtraEditors.SimpleButton()
        Me.txtFiltroUbic = New DevExpress.XtraEditors.TextEdit()
        Me.grdUbicaciones = New DevExpress.XtraGrid.GridControl()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grp = New DevExpress.XtraEditors.GroupControl()
        Me.xtpRegularizacion = New DevExpress.XtraTab.XtraTabPage()
        Me.grdRegularizar = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DetalleBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.bwKPI = New System.ComponentModel.BackgroundWorker()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.WorkspaceManager1 = New DevExpress.Utils.WorkspaceManager(Me.components)
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSector1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabDetalle.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.grpConteo.SuspendLayout()
        CType(Me.grdConteo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gviewConteo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpVerificac.SuspendLayout()
        CType(Me.grdVerifica, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gviewVerifica, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpComparacion.SuspendLayout()
        CType(Me.dgridComparativoInvInicial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gviewComparativo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpFiltros, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFiltros.SuspendLayout()
        CType(Me.txtIdUbicacionInvInicial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkUbicCompleta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Datos.SuspendLayout()
        CType(Me.grpInven, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpInven.SuspendLayout()
        CType(Me.chkCapturarNoAsignado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCentroCosto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMultiPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCaptNtExist.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCliente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbProductoFamilia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMostrarCantidad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCambiaUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpUltimoInv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpUltimoInv.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSistema.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpHoraFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpHoraFin.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpHoraInicio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpHoraInicio.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTipoConteo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTipoInventario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDobleVerifica.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fecha.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fecha.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtraTabInv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtraTabInv.SuspendLayout()
        Me.tabAsignacionProductos.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.dgridAsignacionProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpProductos.SuspendLayout()
        CType(Me.twTodos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbOperadorProd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Tramos.SuspendLayout()
        CType(Me.grpTramos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTramos.SuspendLayout()
        CType(Me.dgridAsignacionTramos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewTramos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabAsignacionOperadores.SuspendLayout()
        CType(Me.dgridAsignacionOperadores, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpOperadores, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpOperadores.SuspendLayout()
        CType(Me.cmbOperador.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabConteo.SuspendLayout()
        CType(Me.dgridInventarioCiclico, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdviewTeorico, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpConteoCi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpConteoCi.SuspendLayout()
        CType(Me.txtNombreUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreOperador.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdOperador.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreTramo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdTramo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtClasificacionNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdClasificacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFamiliaNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdFamilia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabDiferenciasInventario.SuspendLayout()
        CType(Me.dgridDiferenciasCiclico, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDiferenciasCiclico, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabConteoOperador.SuspendLayout()
        CType(Me.dgridConteoOperador, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvConteoOperador, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabReconteo.SuspendLayout()
        CType(Me.grpReconteo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpReconteo.SuspendLayout()
        CType(Me.grdReconteo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EncabezadoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DSReconteoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DSReconteo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabInvTeorico.SuspendLayout()
        CType(Me.dgridInvTeorico, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvInvTeoricoWMS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabComparativoERPWMS.SuspendLayout()
        CType(Me.chkLoteVence.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkConUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgridcomparativoerpwms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvInvTeoricoERP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbne.SuspendLayout()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.grdNE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabInvCongelado.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.dgridCongelado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabInventarioCostos.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.grdCostos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabKPI.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.charcUniverso, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(PieSeriesView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl2.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl2.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.Panel2.SuspendLayout()
        Me.SplitContainerControl2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.circularGauge1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleBackgroundLayerComponent1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleComponent1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleNeedleComponent1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleSpindleCapComponent1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartcEstratoTipo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(PieSeriesView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabAsignacionUbicaciones.SuspendLayout()
        CType(Me.grpUbicaciones, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpUbicaciones.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.dgridAsignacionUbicaciones, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFiltroUbic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdUbicaciones, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtpRegularizacion.SuspendLayout()
        CType(Me.grdRegularizar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DetalleBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(56, 62)
        Fec_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(404, 21)
        User_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 2
        User_modLabel.Text = "Usuario Modificó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(56, 21)
        User_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(100, 16)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(412, 59)
        Fec_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'RibbonControl
        '
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(37)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.cmdActualizarInvInicial, Me.BarCheckItem1, Me.cmbSector, Me.rgrp, Me.cmdCompracionStock, Me.cmdConvertir, Me.cmdReconteo, Me.lblRegs, Me.lblReg, Me.lblRe, Me.cmdImprimir, Me.cmdImprimirGrid, Me.cmdImprimirdetalle, Me.cmdImprimirporoperador, Me.lblRegsRec, Me.cmdImprimirReconteo, Me.cmdActualizarInventario, Me.mnuImprimirInicial, Me.cmdImprimirConteo, Me.cmdImprimirVerifi, Me.cmdImprimirComparacion, Me.mnuExportarExcel, Me.cmdAplicarAjustesFecha, Me.BarButtonItem1, Me.BarButtonItem2, Me.chkComparativoConUbicacion, Me.mnuImportarTeoricoERP, Me.mnuImprimirComparacionERP})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.RibbonControl.MaxItemId = 50
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 412
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.cmbSector1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1938, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar1
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'cmdActualizarInvInicial
        '
        Me.cmdActualizarInvInicial.Caption = "Actualizar Detalle"
        Me.cmdActualizarInvInicial.Id = 6
        Me.cmdActualizarInvInicial.ImageOptions.SvgImage = CType(resources.GetObject("cmdActualizarInvInicial.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizarInvInicial.Name = "cmdActualizarInvInicial"
        '
        'BarCheckItem1
        '
        Me.BarCheckItem1.Caption = "Todos"
        Me.BarCheckItem1.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.AfterText
        Me.BarCheckItem1.Id = 9
        Me.BarCheckItem1.Name = "BarCheckItem1"
        '
        'cmbSector
        '
        Me.cmbSector.Caption = "Sector"
        Me.cmbSector.Edit = Me.cmbSector1
        Me.cmbSector.Id = 10
        Me.cmbSector.Name = "cmbSector"
        '
        'cmbSector1
        '
        Me.cmbSector1.AutoHeight = False
        Me.cmbSector1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbSector1.Name = "cmbSector1"
        Me.cmbSector1.NullText = ""
        '
        'rgrp
        '
        Me.rgrp.Caption = "Importar Inv. teórico WMS"
        Me.rgrp.Id = 13
        Me.rgrp.ImageOptions.SvgImage = CType(resources.GetObject("rgrp.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.rgrp.Name = "rgrp"
        '
        'cmdCompracionStock
        '
        Me.cmdCompracionStock.Caption = "Comparación de Productos en Stock"
        Me.cmdCompracionStock.Id = 14
        Me.cmdCompracionStock.ImageOptions.SvgImage = CType(resources.GetObject("cmdCompracionStock.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdCompracionStock.Name = "cmdCompracionStock"
        '
        'cmdConvertir
        '
        Me.cmdConvertir.Caption = "Regularizar inventario inicial"
        Me.cmdConvertir.Id = 15
        Me.cmdConvertir.ImageOptions.SvgImage = CType(resources.GetObject("cmdConvertir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdConvertir.Name = "cmdConvertir"
        '
        'cmdReconteo
        '
        Me.cmdReconteo.Caption = "Generar Reconteo"
        Me.cmdReconteo.Id = 16
        Me.cmdReconteo.ImageOptions.SvgImage = CType(resources.GetObject("cmdReconteo.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdReconteo.Name = "cmdReconteo"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 17
        Me.lblRegs.Name = "lblRegs"
        '
        'lblReg
        '
        Me.lblReg.Caption = "Registros: 0"
        Me.lblReg.Id = 19
        Me.lblReg.Name = "lblReg"
        '
        'lblRe
        '
        Me.lblRe.Caption = "Registros: 0"
        Me.lblRe.Id = 22
        Me.lblRe.Name = "lblRe"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 24
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdImprimirGrid), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdImprimirdetalle), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdImprimirporoperador), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdImprimirReconteo), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem1)})
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'cmdImprimirGrid
        '
        Me.cmdImprimirGrid.Caption = "Imprimir Grid Detalle Inventario Cíclico"
        Me.cmdImprimirGrid.Id = 25
        Me.cmdImprimirGrid.Name = "cmdImprimirGrid"
        '
        'cmdImprimirdetalle
        '
        Me.cmdImprimirdetalle.Caption = "Imprimir Detalle Inventario Cíclico"
        Me.cmdImprimirdetalle.Id = 26
        Me.cmdImprimirdetalle.Name = "cmdImprimirdetalle"
        '
        'cmdImprimirporoperador
        '
        Me.cmdImprimirporoperador.Caption = "Imprimir Detalle Inventario Cíclico Por Operador"
        Me.cmdImprimirporoperador.Id = 27
        Me.cmdImprimirporoperador.Name = "cmdImprimirporoperador"
        '
        'cmdImprimirReconteo
        '
        Me.cmdImprimirReconteo.Caption = "Imprimir Detalle Reconteo"
        Me.cmdImprimirReconteo.Id = 30
        Me.cmdImprimirReconteo.Name = "cmdImprimirReconteo"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Imprimir Comparativo por valorización"
        Me.BarButtonItem1.Id = 41
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'lblRegsRec
        '
        Me.lblRegsRec.Caption = "Registros: 0"
        Me.lblRegsRec.Id = 29
        Me.lblRegsRec.Name = "lblRegsRec"
        '
        'cmdActualizarInventario
        '
        Me.cmdActualizarInventario.Caption = "Regularizar inventario"
        Me.cmdActualizarInventario.Id = 31
        Me.cmdActualizarInventario.ImageOptions.SvgImage = CType(resources.GetObject("cmdActualizarInventario.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizarInventario.Name = "cmdActualizarInventario"
        '
        'mnuImprimirInicial
        '
        Me.mnuImprimirInicial.Caption = "Imprimir"
        Me.mnuImprimirInicial.Id = 33
        Me.mnuImprimirInicial.ImageOptions.SvgImage = CType(resources.GetObject("mnuImprimirInicial.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuImprimirInicial.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdImprimirConteo), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdImprimirVerifi), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdImprimirComparacion), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuImprimirComparacionERP)})
        Me.mnuImprimirInicial.Name = "mnuImprimirInicial"
        '
        'cmdImprimirConteo
        '
        Me.cmdImprimirConteo.Caption = "Imprimir Grid de Conteo"
        Me.cmdImprimirConteo.Id = 34
        Me.cmdImprimirConteo.Name = "cmdImprimirConteo"
        '
        'cmdImprimirVerifi
        '
        Me.cmdImprimirVerifi.Caption = "Imprimir Grid de Verificación"
        Me.cmdImprimirVerifi.Id = 35
        Me.cmdImprimirVerifi.Name = "cmdImprimirVerifi"
        '
        'cmdImprimirComparacion
        '
        Me.cmdImprimirComparacion.Caption = "Imprimir Grid de Comparación"
        Me.cmdImprimirComparacion.Id = 36
        Me.cmdImprimirComparacion.Name = "cmdImprimirComparacion"
        '
        'mnuImprimirComparacionERP
        '
        Me.mnuImprimirComparacionERP.Caption = "Imprimir Grid Comparación ERP"
        Me.mnuImprimirComparacionERP.Id = 47
        Me.mnuImprimirComparacionERP.Name = "mnuImprimirComparacionERP"
        '
        'mnuExportarExcel
        '
        Me.mnuExportarExcel.Caption = "Exportar a excel"
        Me.mnuExportarExcel.Id = 38
        Me.mnuExportarExcel.ImageOptions.SvgImage = CType(resources.GetObject("mnuExportarExcel.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuExportarExcel.Name = "mnuExportarExcel"
        '
        'cmdAplicarAjustesFecha
        '
        Me.cmdAplicarAjustesFecha.Caption = "Aplicar ajustes fecha"
        Me.cmdAplicarAjustesFecha.Id = 39
        Me.cmdAplicarAjustesFecha.ImageOptions.SvgImage = CType(resources.GetObject("cmdAplicarAjustesFecha.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdAplicarAjustesFecha.Name = "cmdAplicarAjustesFecha"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Actualizar KPI"
        Me.BarButtonItem2.Id = 42
        Me.BarButtonItem2.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem2.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'chkComparativoConUbicacion
        '
        Me.chkComparativoConUbicacion.Caption = "Comparativo con ubicación"
        Me.chkComparativoConUbicacion.Id = 43
        Me.chkComparativoConUbicacion.Name = "chkComparativoConUbicacion"
        '
        'mnuImportarTeoricoERP
        '
        Me.mnuImportarTeoricoERP.Caption = "Importar Inv. teórico ERP"
        Me.mnuImportarTeoricoERP.Id = 45
        Me.mnuImportarTeoricoERP.ImageOptions.SvgImage = CType(resources.GetObject("mnuImportarTeoricoERP.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuImportarTeoricoERP.Name = "mnuImportarTeoricoERP"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.rgprImportar, Me.rpActualizar, Me.rpgComparacion, Me.rpgReconteo, Me.pgImprimir, Me.grpImprimirInicial, Me.pgExportar, Me.grpRegularizarInvStock, Me.rgrpRegularizar, Me.rpgAplicarAjustesFecha, Me.rpgKPI})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Inventario"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'rgprImportar
        '
        Me.rgprImportar.ItemLinks.Add(Me.rgrp)
        Me.rgprImportar.ItemLinks.Add(Me.mnuImportarTeoricoERP)
        Me.rgprImportar.Name = "rgprImportar"
        '
        'rpActualizar
        '
        Me.rpActualizar.ItemLinks.Add(Me.cmdActualizarInvInicial)
        Me.rpActualizar.Name = "rpActualizar"
        '
        'rpgComparacion
        '
        Me.rpgComparacion.ItemLinks.Add(Me.cmdCompracionStock)
        Me.rpgComparacion.Name = "rpgComparacion"
        '
        'rpgReconteo
        '
        Me.rpgReconteo.ItemLinks.Add(Me.cmdReconteo)
        Me.rpgReconteo.Name = "rpgReconteo"
        '
        'pgImprimir
        '
        Me.pgImprimir.ItemLinks.Add(Me.cmdImprimir)
        Me.pgImprimir.Name = "pgImprimir"
        '
        'grpImprimirInicial
        '
        Me.grpImprimirInicial.ItemLinks.Add(Me.mnuImprimirInicial)
        Me.grpImprimirInicial.Name = "grpImprimirInicial"
        '
        'pgExportar
        '
        Me.pgExportar.ItemLinks.Add(Me.mnuExportarExcel)
        Me.pgExportar.Name = "pgExportar"
        '
        'grpRegularizarInvStock
        '
        Me.grpRegularizarInvStock.ItemLinks.Add(Me.cmdActualizarInventario)
        Me.grpRegularizarInvStock.Name = "grpRegularizarInvStock"
        '
        'rgrpRegularizar
        '
        Me.rgrpRegularizar.ItemLinks.Add(Me.cmdConvertir)
        Me.rgrpRegularizar.Name = "rgrpRegularizar"
        '
        'rpgAplicarAjustesFecha
        '
        Me.rpgAplicarAjustesFecha.ItemLinks.Add(Me.cmdAplicarAjustesFecha)
        Me.rpgAplicarAjustesFecha.Name = "rpgAplicarAjustesFecha"
        Me.rpgAplicarAjustesFecha.Visible = False
        '
        'rpgKPI
        '
        Me.rpgKPI.ItemLinks.Add(Me.BarButtonItem2)
        Me.rpgKPI.ItemLinks.Add(Me.chkComparativoConUbicacion)
        Me.rpgKPI.Name = "rpgKPI"
        Me.rpgKPI.Visible = False
        '
        'RibbonStatusBar1
        '
        Me.RibbonStatusBar1.ItemLinks.Add(Me.lblRegsRec)
        Me.RibbonStatusBar1.Location = New System.Drawing.Point(2, 597)
        Me.RibbonStatusBar1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.RibbonStatusBar1.Name = "RibbonStatusBar1"
        Me.RibbonStatusBar1.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar1.Size = New System.Drawing.Size(1932, 33)
        '
        'DockManager1
        '
        Me.DockManager1.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.DockManager1.Form = Me
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 856)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1938, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("27b08e8a-c032-417e-bde3-aefadf1a50ca")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 817)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 111)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1499, 111)
        Me.DockPanel1.Text = "Bitácora"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.Fec_agrDateEdit)
        Me.DockPanel1_Container.Controls.Add(Me.User_modTextEdit)
        Me.DockPanel1_Container.Controls.Add(Fec_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Fec_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_modDateEdit)
        Me.DockPanel1_Container.Controls.Add(User_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_agrTextEdit)
        Me.DockPanel1_Container.Controls.Add(User_modLabel)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 34)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1491, 73)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(159, 58)
        Me.Fec_agrDateEdit.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Fec_agrDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(175, 22)
        Me.Fec_agrDateEdit.TabIndex = 5
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(514, 17)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(175, 22)
        Me.User_modTextEdit.TabIndex = 3
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(514, 55)
        Me.Fec_modDateEdit.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Fec_modDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(175, 22)
        Me.Fec_modDateEdit.TabIndex = 7
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(159, 17)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(175, 22)
        Me.User_agrTextEdit.TabIndex = 1
        '
        'tabDetalle
        '
        Me.tabDetalle.Controls.Add(Me.SplitContainer1)
        Me.tabDetalle.Controls.Add(Me.grpFiltros)
        Me.tabDetalle.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.tabDetalle.Name = "tabDetalle"
        Me.tabDetalle.Size = New System.Drawing.Size(1936, 633)
        Me.tabDetalle.Text = "Detalle de Inventario Inicial"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 58)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.grpComparacion)
        Me.SplitContainer1.Size = New System.Drawing.Size(1936, 575)
        Me.SplitContainer1.SplitterDistance = 907
        Me.SplitContainer1.SplitterWidth = 6
        Me.SplitContainer1.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.grpConteo)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.grpVerificac)
        Me.SplitContainer2.Size = New System.Drawing.Size(907, 575)
        Me.SplitContainer2.SplitterDistance = 231
        Me.SplitContainer2.SplitterWidth = 6
        Me.SplitContainer2.TabIndex = 0
        '
        'grpConteo
        '
        Me.grpConteo.Controls.Add(Me.grdConteo)
        Me.grpConteo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpConteo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpConteo.Location = New System.Drawing.Point(0, 0)
        Me.grpConteo.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grpConteo.Name = "grpConteo"
        Me.grpConteo.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grpConteo.Size = New System.Drawing.Size(907, 231)
        Me.grpConteo.TabIndex = 0
        Me.grpConteo.TabStop = False
        Me.grpConteo.Text = "Conteo"
        '
        'grdConteo
        '
        Me.grdConteo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdConteo.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grdConteo.Location = New System.Drawing.Point(5, 22)
        Me.grdConteo.MainView = Me.gviewConteo
        Me.grdConteo.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grdConteo.MenuManager = Me.RibbonControl
        Me.grdConteo.Name = "grdConteo"
        Me.grdConteo.Size = New System.Drawing.Size(897, 204)
        Me.grdConteo.TabIndex = 0
        Me.grdConteo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gviewConteo, Me.GridView4})
        '
        'gviewConteo
        '
        Me.gviewConteo.DetailHeight = 437
        Me.gviewConteo.GridControl = Me.grdConteo
        Me.gviewConteo.Name = "gviewConteo"
        Me.gviewConteo.OptionsBehavior.ReadOnly = True
        Me.gviewConteo.OptionsEditForm.PopupEditFormWidth = 1000
        Me.gviewConteo.OptionsSelection.MultiSelect = True
        Me.gviewConteo.OptionsView.ColumnAutoWidth = False
        Me.gviewConteo.OptionsView.ShowAutoFilterRow = True
        Me.gviewConteo.OptionsView.ShowFooter = True
        Me.gviewConteo.OptionsView.ShowGroupPanel = False
        '
        'GridView4
        '
        Me.GridView4.DetailHeight = 437
        Me.GridView4.GridControl = Me.grdConteo
        Me.GridView4.Name = "GridView4"
        Me.GridView4.OptionsEditForm.PopupEditFormWidth = 1000
        Me.GridView4.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways
        Me.GridView4.OptionsView.ShowFooter = True
        '
        'grpVerificac
        '
        Me.grpVerificac.Controls.Add(Me.grdVerifica)
        Me.grpVerificac.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpVerificac.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpVerificac.Location = New System.Drawing.Point(0, 0)
        Me.grpVerificac.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grpVerificac.Name = "grpVerificac"
        Me.grpVerificac.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grpVerificac.Size = New System.Drawing.Size(907, 338)
        Me.grpVerificac.TabIndex = 0
        Me.grpVerificac.TabStop = False
        Me.grpVerificac.Text = "Verificación"
        '
        'grdVerifica
        '
        Me.grdVerifica.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdVerifica.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grdVerifica.Location = New System.Drawing.Point(5, 22)
        Me.grdVerifica.MainView = Me.gviewVerifica
        Me.grdVerifica.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grdVerifica.MenuManager = Me.RibbonControl
        Me.grdVerifica.Name = "grdVerifica"
        Me.grdVerifica.Size = New System.Drawing.Size(897, 311)
        Me.grdVerifica.TabIndex = 0
        Me.grdVerifica.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gviewVerifica})
        '
        'gviewVerifica
        '
        Me.gviewVerifica.DetailHeight = 437
        Me.gviewVerifica.GridControl = Me.grdVerifica
        Me.gviewVerifica.Name = "gviewVerifica"
        Me.gviewVerifica.OptionsBehavior.ReadOnly = True
        Me.gviewVerifica.OptionsEditForm.PopupEditFormWidth = 1000
        Me.gviewVerifica.OptionsSelection.MultiSelect = True
        Me.gviewVerifica.OptionsView.ColumnAutoWidth = False
        Me.gviewVerifica.OptionsView.ShowAutoFilterRow = True
        Me.gviewVerifica.OptionsView.ShowFooter = True
        Me.gviewVerifica.OptionsView.ShowGroupPanel = False
        '
        'grpComparacion
        '
        Me.grpComparacion.Controls.Add(Me.dgridComparativoInvInicial)
        Me.grpComparacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpComparacion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpComparacion.Location = New System.Drawing.Point(0, 0)
        Me.grpComparacion.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grpComparacion.Name = "grpComparacion"
        Me.grpComparacion.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grpComparacion.Size = New System.Drawing.Size(1023, 575)
        Me.grpComparacion.TabIndex = 0
        Me.grpComparacion.TabStop = False
        Me.grpComparacion.Text = "Comparación"
        '
        'dgridComparativoInvInicial
        '
        Me.dgridComparativoInvInicial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridComparativoInvInicial.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dgridComparativoInvInicial.Location = New System.Drawing.Point(5, 22)
        Me.dgridComparativoInvInicial.MainView = Me.gviewComparativo
        Me.dgridComparativoInvInicial.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dgridComparativoInvInicial.MenuManager = Me.RibbonControl
        Me.dgridComparativoInvInicial.Name = "dgridComparativoInvInicial"
        Me.dgridComparativoInvInicial.Size = New System.Drawing.Size(1013, 548)
        Me.dgridComparativoInvInicial.TabIndex = 0
        Me.dgridComparativoInvInicial.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gviewComparativo})
        '
        'gviewComparativo
        '
        Me.gviewComparativo.DetailHeight = 437
        Me.gviewComparativo.GridControl = Me.dgridComparativoInvInicial
        Me.gviewComparativo.Name = "gviewComparativo"
        Me.gviewComparativo.OptionsBehavior.ReadOnly = True
        Me.gviewComparativo.OptionsEditForm.PopupEditFormWidth = 1000
        Me.gviewComparativo.OptionsSelection.MultiSelect = True
        Me.gviewComparativo.OptionsView.ColumnAutoWidth = False
        Me.gviewComparativo.OptionsView.ShowAutoFilterRow = True
        Me.gviewComparativo.OptionsView.ShowFooter = True
        Me.gviewComparativo.OptionsView.ShowGroupPanel = False
        '
        'grpFiltros
        '
        Me.grpFiltros.Controls.Add(Me.lnkUbicacionInvInicial)
        Me.grpFiltros.Controls.Add(Me.txtIdUbicacionInvInicial)
        Me.grpFiltros.Controls.Add(Me.chkUbicCompleta)
        Me.grpFiltros.Controls.Add(Me.btnLimpiar)
        Me.grpFiltros.Controls.Add(Me.txtPropietarioId)
        Me.grpFiltros.Controls.Add(Me.txtProductoId)
        Me.grpFiltros.Controls.Add(Me.txtTramoId)
        Me.grpFiltros.Controls.Add(Me.linkPropietario)
        Me.grpFiltros.Controls.Add(Me.linkProducto)
        Me.grpFiltros.Controls.Add(Me.linkTramo)
        Me.grpFiltros.Controls.Add(Me.txtPropietarioNombre)
        Me.grpFiltros.Controls.Add(Me.txtTramoNombre)
        Me.grpFiltros.Controls.Add(Me.txtProductoNombre)
        Me.grpFiltros.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpFiltros.Location = New System.Drawing.Point(0, 0)
        Me.grpFiltros.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grpFiltros.Name = "grpFiltros"
        Me.grpFiltros.ShowCaption = False
        Me.grpFiltros.Size = New System.Drawing.Size(1936, 58)
        Me.grpFiltros.TabIndex = 0
        Me.grpFiltros.Text = "Filtros"
        '
        'lnkUbicacionInvInicial
        '
        Me.lnkUbicacionInvInicial.AutoSize = True
        Me.lnkUbicacionInvInicial.Location = New System.Drawing.Point(5, 17)
        Me.lnkUbicacionInvInicial.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lnkUbicacionInvInicial.Name = "lnkUbicacionInvInicial"
        Me.lnkUbicacionInvInicial.Size = New System.Drawing.Size(61, 16)
        Me.lnkUbicacionInvInicial.TabIndex = 11
        Me.lnkUbicacionInvInicial.TabStop = True
        Me.lnkUbicacionInvInicial.Text = "Ubicación"
        '
        'txtIdUbicacionInvInicial
        '
        Me.txtIdUbicacionInvInicial.Location = New System.Drawing.Point(75, 14)
        Me.txtIdUbicacionInvInicial.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtIdUbicacionInvInicial.MenuManager = Me.RibbonControl
        Me.txtIdUbicacionInvInicial.Name = "txtIdUbicacionInvInicial"
        Me.txtIdUbicacionInvInicial.Size = New System.Drawing.Size(160, 22)
        Me.txtIdUbicacionInvInicial.TabIndex = 12
        '
        'chkUbicCompleta
        '
        Me.chkUbicCompleta.Location = New System.Drawing.Point(1765, 42)
        Me.chkUbicCompleta.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.chkUbicCompleta.MenuManager = Me.RibbonControl
        Me.chkUbicCompleta.Name = "chkUbicCompleta"
        Me.chkUbicCompleta.Properties.Caption = "Ubic_Completa"
        Me.chkUbicCompleta.Size = New System.Drawing.Size(156, 24)
        Me.chkUbicCompleta.TabIndex = 10
        Me.chkUbicCompleta.Visible = False
        '
        'btnLimpiar
        '
        Me.btnLimpiar.Location = New System.Drawing.Point(1416, 7)
        Me.btnLimpiar.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.btnLimpiar.Name = "btnLimpiar"
        Me.btnLimpiar.Size = New System.Drawing.Size(110, 34)
        Me.btnLimpiar.TabIndex = 9
        Me.btnLimpiar.Text = "Limpiar"
        '
        'txtPropietarioId
        '
        Me.txtPropietarioId.Location = New System.Drawing.Point(719, 15)
        Me.txtPropietarioId.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtPropietarioId.Name = "txtPropietarioId"
        Me.txtPropietarioId.Size = New System.Drawing.Size(93, 23)
        Me.txtPropietarioId.TabIndex = 7
        '
        'txtProductoId
        '
        Me.txtProductoId.Location = New System.Drawing.Point(309, 14)
        Me.txtProductoId.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtProductoId.Name = "txtProductoId"
        Me.txtProductoId.Size = New System.Drawing.Size(93, 23)
        Me.txtProductoId.TabIndex = 4
        '
        'txtTramoId
        '
        Me.txtTramoId.Location = New System.Drawing.Point(1092, 14)
        Me.txtTramoId.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtTramoId.Name = "txtTramoId"
        Me.txtTramoId.Size = New System.Drawing.Size(93, 23)
        Me.txtTramoId.TabIndex = 1
        '
        'linkPropietario
        '
        Me.linkPropietario.AutoSize = True
        Me.linkPropietario.Location = New System.Drawing.Point(635, 17)
        Me.linkPropietario.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.linkPropietario.Name = "linkPropietario"
        Me.linkPropietario.Size = New System.Drawing.Size(74, 16)
        Me.linkPropietario.TabIndex = 6
        Me.linkPropietario.TabStop = True
        Me.linkPropietario.Text = "Propietario:"
        '
        'linkProducto
        '
        Me.linkProducto.AutoSize = True
        Me.linkProducto.Location = New System.Drawing.Point(241, 20)
        Me.linkProducto.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.linkProducto.Name = "linkProducto"
        Me.linkProducto.Size = New System.Drawing.Size(62, 16)
        Me.linkProducto.TabIndex = 3
        Me.linkProducto.TabStop = True
        Me.linkProducto.Text = "Producto:"
        '
        'linkTramo
        '
        Me.linkTramo.AutoSize = True
        Me.linkTramo.Location = New System.Drawing.Point(1040, 17)
        Me.linkTramo.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.linkTramo.Name = "linkTramo"
        Me.linkTramo.Size = New System.Drawing.Size(50, 16)
        Me.linkTramo.TabIndex = 0
        Me.linkTramo.TabStop = True
        Me.linkTramo.Text = "Tramo:"
        '
        'txtPropietarioNombre
        '
        Me.txtPropietarioNombre.Location = New System.Drawing.Point(821, 15)
        Me.txtPropietarioNombre.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtPropietarioNombre.Name = "txtPropietarioNombre"
        Me.txtPropietarioNombre.Size = New System.Drawing.Size(213, 23)
        Me.txtPropietarioNombre.TabIndex = 8
        '
        'txtTramoNombre
        '
        Me.txtTramoNombre.Location = New System.Drawing.Point(1194, 14)
        Me.txtTramoNombre.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtTramoNombre.Name = "txtTramoNombre"
        Me.txtTramoNombre.Size = New System.Drawing.Size(213, 23)
        Me.txtTramoNombre.TabIndex = 2
        '
        'txtProductoNombre
        '
        Me.txtProductoNombre.Location = New System.Drawing.Point(412, 14)
        Me.txtProductoNombre.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtProductoNombre.Name = "txtProductoNombre"
        Me.txtProductoNombre.Size = New System.Drawing.Size(213, 23)
        Me.txtProductoNombre.TabIndex = 5
        '
        'Datos
        '
        Me.Datos.Controls.Add(Me.grpInven)
        Me.Datos.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.Datos.Name = "Datos"
        Me.Datos.Size = New System.Drawing.Size(1936, 633)
        Me.Datos.Text = "Datos de Inventario"
        '
        'grpInven
        '
        Me.grpInven.Controls.Add(Me.chkCapturarNoAsignado)
        Me.grpInven.Controls.Add(Me.Label4)
        Me.grpInven.Controls.Add(Me.cmbCentroCosto)
        Me.grpInven.Controls.Add(Me.lblCentroCosto)
        Me.grpInven.Controls.Add(Me.chkMultiPropietario)
        Me.grpInven.Controls.Add(Me.Label3)
        Me.grpInven.Controls.Add(Me.chkCaptNtExist)
        Me.grpInven.Controls.Add(Me.Label2)
        Me.grpInven.Controls.Add(Me.cmbCliente)
        Me.grpInven.Controls.Add(Me.lblCliente)
        Me.grpInven.Controls.Add(Me.cmbProductoFamilia)
        Me.grpInven.Controls.Add(Me.lblSeccionAjuste)
        Me.grpInven.Controls.Add(Me.lblMostrarCantidad)
        Me.grpInven.Controls.Add(Me.chkMostrarCantidad)
        Me.grpInven.Controls.Add(Me.chkCambiaUbicacion)
        Me.grpInven.Controls.Add(Me.lblCambiaUbicacion)
        Me.grpInven.Controls.Add(Me.lblUltInv)
        Me.grpInven.Controls.Add(Me.dtpUltimoInv)
        Me.grpInven.Controls.Add(Me.lblPrg)
        Me.grpInven.Controls.Add(Me.prg)
        Me.grpInven.Controls.Add(Me.lblEsSistema)
        Me.grpInven.Controls.Add(Me.chkSistema)
        Me.grpInven.Controls.Add(Me.dtpHoraFin)
        Me.grpInven.Controls.Add(Me.dtpHoraInicio)
        Me.grpInven.Controls.Add(Me.chkActivo)
        Me.grpInven.Controls.Add(Me.cmbTipoConteo)
        Me.grpInven.Controls.Add(Me.cmbTipoInventario)
        Me.grpInven.Controls.Add(Me.lblFin)
        Me.grpInven.Controls.Add(Me.lblInicio)
        Me.grpInven.Controls.Add(Me.lblActivo)
        Me.grpInven.Controls.Add(Me.chkDobleVerifica)
        Me.grpInven.Controls.Add(Me.lblDobleVerif)
        Me.grpInven.Controls.Add(Me.lblTipoConteo)
        Me.grpInven.Controls.Add(Me.lblTipoInventario)
        Me.grpInven.Controls.Add(Me.lblFecha)
        Me.grpInven.Controls.Add(Me.Fecha)
        Me.grpInven.Controls.Add(Me.cmbPropietario)
        Me.grpInven.Controls.Add(Me.cmbBodega)
        Me.grpInven.Controls.Add(Me.Estado)
        Me.grpInven.Controls.Add(Me.lblCod)
        Me.grpInven.Controls.Add(Me.lblPropietario)
        Me.grpInven.Controls.Add(Me.lblBodega)
        Me.grpInven.Controls.Add(Me.lblEstado)
        Me.grpInven.Controls.Add(Me.lblCodigo)
        Me.grpInven.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpInven.Location = New System.Drawing.Point(0, 0)
        Me.grpInven.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grpInven.Name = "grpInven"
        Me.grpInven.Size = New System.Drawing.Size(1936, 633)
        Me.grpInven.TabIndex = 0
        '
        'chkCapturarNoAsignado
        '
        Me.chkCapturarNoAsignado.Location = New System.Drawing.Point(835, 267)
        Me.chkCapturarNoAsignado.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.chkCapturarNoAsignado.MenuManager = Me.RibbonControl
        Me.chkCapturarNoAsignado.Name = "chkCapturarNoAsignado"
        Me.chkCapturarNoAsignado.Properties.Caption = ""
        Me.chkCapturarNoAsignado.Size = New System.Drawing.Size(24, 24)
        Me.chkCapturarNoAsignado.TabIndex = 43
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(635, 267)
        Me.Label4.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(135, 16)
        Me.Label4.TabIndex = 42
        Me.Label4.Text = "Capturar no asignado:"
        '
        'cmbCentroCosto
        '
        Me.cmbCentroCosto.Location = New System.Drawing.Point(835, 54)
        Me.cmbCentroCosto.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmbCentroCosto.MenuManager = Me.RibbonControl
        Me.cmbCentroCosto.Name = "cmbCentroCosto"
        Me.cmbCentroCosto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCentroCosto.Properties.NullText = ""
        Me.cmbCentroCosto.Size = New System.Drawing.Size(314, 22)
        Me.cmbCentroCosto.TabIndex = 41
        '
        'lblCentroCosto
        '
        Me.lblCentroCosto.AutoSize = True
        Me.lblCentroCosto.Location = New System.Drawing.Point(635, 54)
        Me.lblCentroCosto.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblCentroCosto.Name = "lblCentroCosto"
        Me.lblCentroCosto.Size = New System.Drawing.Size(159, 16)
        Me.lblCentroCosto.TabIndex = 40
        Me.lblCentroCosto.Text = "Centro de costo / Remark:"
        '
        'chkMultiPropietario
        '
        Me.chkMultiPropietario.Location = New System.Drawing.Point(835, 206)
        Me.chkMultiPropietario.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.chkMultiPropietario.MenuManager = Me.RibbonControl
        Me.chkMultiPropietario.Name = "chkMultiPropietario"
        Me.chkMultiPropietario.Properties.Caption = ""
        Me.chkMultiPropietario.Size = New System.Drawing.Size(24, 24)
        Me.chkMultiPropietario.TabIndex = 39
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(635, 206)
        Me.Label3.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 16)
        Me.Label3.TabIndex = 38
        Me.Label3.Text = "Multi-Propietario:"
        '
        'chkCaptNtExist
        '
        Me.chkCaptNtExist.Location = New System.Drawing.Point(835, 325)
        Me.chkCaptNtExist.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.chkCaptNtExist.MenuManager = Me.RibbonControl
        Me.chkCaptNtExist.Name = "chkCaptNtExist"
        Me.chkCaptNtExist.Properties.Caption = ""
        Me.chkCaptNtExist.Size = New System.Drawing.Size(24, 24)
        Me.chkCaptNtExist.TabIndex = 37
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(635, 325)
        Me.Label2.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(135, 16)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "Capturar no existente:"
        '
        'cmbCliente
        '
        Me.cmbCliente.Location = New System.Drawing.Point(835, 82)
        Me.cmbCliente.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmbCliente.MenuManager = Me.RibbonControl
        Me.cmbCliente.Name = "cmbCliente"
        Me.cmbCliente.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCliente.Properties.NullText = ""
        Me.cmbCliente.Size = New System.Drawing.Size(314, 22)
        Me.cmbCliente.TabIndex = 13
        '
        'lblCliente
        '
        Me.lblCliente.AutoSize = True
        Me.lblCliente.Location = New System.Drawing.Point(635, 89)
        Me.lblCliente.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(93, 16)
        Me.lblCliente.TabIndex = 12
        Me.lblCliente.Text = "Bodega virtual:"
        '
        'cmbProductoFamilia
        '
        Me.cmbProductoFamilia.Location = New System.Drawing.Point(835, 112)
        Me.cmbProductoFamilia.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmbProductoFamilia.MenuManager = Me.RibbonControl
        Me.cmbProductoFamilia.Name = "cmbProductoFamilia"
        Me.cmbProductoFamilia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbProductoFamilia.Properties.NullText = ""
        Me.cmbProductoFamilia.Size = New System.Drawing.Size(314, 22)
        Me.cmbProductoFamilia.TabIndex = 17
        '
        'lblSeccionAjuste
        '
        Me.lblSeccionAjuste.AutoSize = True
        Me.lblSeccionAjuste.Location = New System.Drawing.Point(635, 118)
        Me.lblSeccionAjuste.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblSeccionAjuste.Name = "lblSeccionAjuste"
        Me.lblSeccionAjuste.Size = New System.Drawing.Size(113, 16)
        Me.lblSeccionAjuste.TabIndex = 16
        Me.lblSeccionAjuste.Text = "Sección de ajuste:"
        '
        'lblMostrarCantidad
        '
        Me.lblMostrarCantidad.AutoSize = True
        Me.lblMostrarCantidad.Location = New System.Drawing.Point(635, 238)
        Me.lblMostrarCantidad.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblMostrarCantidad.Name = "lblMostrarCantidad"
        Me.lblMostrarCantidad.Size = New System.Drawing.Size(108, 16)
        Me.lblMostrarCantidad.TabIndex = 32
        Me.lblMostrarCantidad.Text = "Mostrar cantidad:"
        Me.lblMostrarCantidad.Visible = False
        '
        'chkMostrarCantidad
        '
        Me.chkMostrarCantidad.Location = New System.Drawing.Point(835, 238)
        Me.chkMostrarCantidad.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.chkMostrarCantidad.MenuManager = Me.RibbonControl
        Me.chkMostrarCantidad.Name = "chkMostrarCantidad"
        Me.chkMostrarCantidad.Properties.Caption = ""
        Me.chkMostrarCantidad.Size = New System.Drawing.Size(24, 24)
        Me.chkMostrarCantidad.TabIndex = 33
        Me.chkMostrarCantidad.Visible = False
        '
        'chkCambiaUbicacion
        '
        Me.chkCambiaUbicacion.Location = New System.Drawing.Point(835, 295)
        Me.chkCambiaUbicacion.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.chkCambiaUbicacion.MenuManager = Me.RibbonControl
        Me.chkCambiaUbicacion.Name = "chkCambiaUbicacion"
        Me.chkCambiaUbicacion.Properties.Caption = ""
        Me.chkCambiaUbicacion.Size = New System.Drawing.Size(24, 24)
        Me.chkCambiaUbicacion.TabIndex = 29
        Me.chkCambiaUbicacion.Visible = False
        '
        'lblCambiaUbicacion
        '
        Me.lblCambiaUbicacion.AutoSize = True
        Me.lblCambiaUbicacion.Location = New System.Drawing.Point(635, 295)
        Me.lblCambiaUbicacion.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblCambiaUbicacion.Name = "lblCambiaUbicacion"
        Me.lblCambiaUbicacion.Size = New System.Drawing.Size(112, 16)
        Me.lblCambiaUbicacion.TabIndex = 28
        Me.lblCambiaUbicacion.Text = "Cambia ubicación:"
        '
        'lblUltInv
        '
        Me.lblUltInv.AutoSize = True
        Me.lblUltInv.Location = New System.Drawing.Point(40, 295)
        Me.lblUltInv.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblUltInv.Name = "lblUltInv"
        Me.lblUltInv.Size = New System.Drawing.Size(51, 16)
        Me.lblUltInv.TabIndex = 25
        Me.lblUltInv.Text = "Ult. Inv."
        '
        'dtpUltimoInv
        '
        Me.dtpUltimoInv.EditValue = New Date(2017, 12, 4, 13, 30, 51, 367)
        Me.dtpUltimoInv.Location = New System.Drawing.Point(236, 295)
        Me.dtpUltimoInv.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dtpUltimoInv.MenuManager = Me.RibbonControl
        Me.dtpUltimoInv.Name = "dtpUltimoInv"
        Me.dtpUltimoInv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpUltimoInv.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpUltimoInv.Size = New System.Drawing.Size(314, 22)
        Me.dtpUltimoInv.TabIndex = 24
        '
        'lblPrg
        '
        Me.lblPrg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPrg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblPrg.Location = New System.Drawing.Point(2, 494)
        Me.lblPrg.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblPrg.Name = "lblPrg"
        Me.lblPrg.Size = New System.Drawing.Size(1932, 105)
        Me.lblPrg.TabIndex = 34
        Me.lblPrg.Text = "..."
        Me.lblPrg.Visible = False
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.prg.Location = New System.Drawing.Point(2, 599)
        Me.prg.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(1932, 32)
        Me.prg.TabIndex = 35
        Me.prg.Visible = False
        '
        'lblEsSistema
        '
        Me.lblEsSistema.AutoSize = True
        Me.lblEsSistema.Location = New System.Drawing.Point(40, 354)
        Me.lblEsSistema.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblEsSistema.Name = "lblEsSistema"
        Me.lblEsSistema.Size = New System.Drawing.Size(75, 16)
        Me.lblEsSistema.TabIndex = 30
        Me.lblEsSistema.Text = "Es Sistema:"
        Me.lblEsSistema.Visible = False
        '
        'chkSistema
        '
        Me.chkSistema.Location = New System.Drawing.Point(236, 354)
        Me.chkSistema.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.chkSistema.MenuManager = Me.RibbonControl
        Me.chkSistema.Name = "chkSistema"
        Me.chkSistema.Properties.Caption = ""
        Me.chkSistema.Size = New System.Drawing.Size(24, 24)
        Me.chkSistema.TabIndex = 31
        Me.chkSistema.Visible = False
        '
        'dtpHoraFin
        '
        Me.dtpHoraFin.EditValue = New Date(2017, 12, 4, 13, 30, 51, 367)
        Me.dtpHoraFin.Location = New System.Drawing.Point(236, 267)
        Me.dtpHoraFin.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dtpHoraFin.MenuManager = Me.RibbonControl
        Me.dtpHoraFin.Name = "dtpHoraFin"
        Me.dtpHoraFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpHoraFin.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpHoraFin.Properties.DisplayFormat.FormatString = "t"
        Me.dtpHoraFin.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtpHoraFin.Properties.EditFormat.FormatString = "t"
        Me.dtpHoraFin.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtpHoraFin.Properties.MaskSettings.Set("mask", "t")
        Me.dtpHoraFin.Size = New System.Drawing.Size(314, 22)
        Me.dtpHoraFin.TabIndex = 23
        '
        'dtpHoraInicio
        '
        Me.dtpHoraInicio.EditValue = New Date(2017, 12, 4, 13, 30, 51, 367)
        Me.dtpHoraInicio.Location = New System.Drawing.Point(236, 238)
        Me.dtpHoraInicio.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dtpHoraInicio.MenuManager = Me.RibbonControl
        Me.dtpHoraInicio.Name = "dtpHoraInicio"
        Me.dtpHoraInicio.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpHoraInicio.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpHoraInicio.Properties.DisplayFormat.FormatString = "t"
        Me.dtpHoraInicio.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtpHoraInicio.Properties.EditFormat.FormatString = "t"
        Me.dtpHoraInicio.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtpHoraInicio.Properties.MaskSettings.Set("mask", "t")
        Me.dtpHoraInicio.Size = New System.Drawing.Size(314, 22)
        Me.dtpHoraInicio.TabIndex = 19
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(236, 325)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(24, 24)
        Me.chkActivo.TabIndex = 27
        '
        'cmbTipoConteo
        '
        Me.cmbTipoConteo.Location = New System.Drawing.Point(835, 144)
        Me.cmbTipoConteo.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmbTipoConteo.MenuManager = Me.RibbonControl
        Me.cmbTipoConteo.Name = "cmbTipoConteo"
        Me.cmbTipoConteo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoConteo.Properties.NullText = ""
        Me.cmbTipoConteo.Size = New System.Drawing.Size(314, 22)
        Me.cmbTipoConteo.TabIndex = 21
        Me.cmbTipoConteo.Visible = False
        '
        'cmbTipoInventario
        '
        Me.cmbTipoInventario.Location = New System.Drawing.Point(236, 176)
        Me.cmbTipoInventario.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmbTipoInventario.MenuManager = Me.RibbonControl
        Me.cmbTipoInventario.Name = "cmbTipoInventario"
        Me.cmbTipoInventario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoInventario.Properties.NullText = ""
        Me.cmbTipoInventario.Size = New System.Drawing.Size(314, 22)
        Me.cmbTipoInventario.TabIndex = 9
        '
        'lblFin
        '
        Me.lblFin.AutoSize = True
        Me.lblFin.Location = New System.Drawing.Point(40, 267)
        Me.lblFin.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblFin.Name = "lblFin"
        Me.lblFin.Size = New System.Drawing.Size(60, 16)
        Me.lblFin.TabIndex = 22
        Me.lblFin.Text = "Hora Fin:"
        '
        'lblInicio
        '
        Me.lblInicio.AutoSize = True
        Me.lblInicio.Location = New System.Drawing.Point(40, 238)
        Me.lblInicio.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblInicio.Name = "lblInicio"
        Me.lblInicio.Size = New System.Drawing.Size(73, 16)
        Me.lblInicio.TabIndex = 18
        Me.lblInicio.Text = "Hora Inicio:"
        '
        'lblActivo
        '
        Me.lblActivo.AutoSize = True
        Me.lblActivo.Location = New System.Drawing.Point(40, 325)
        Me.lblActivo.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblActivo.Name = "lblActivo"
        Me.lblActivo.Size = New System.Drawing.Size(46, 16)
        Me.lblActivo.TabIndex = 26
        Me.lblActivo.Text = "Activo:"
        '
        'chkDobleVerifica
        '
        Me.chkDobleVerifica.Location = New System.Drawing.Point(835, 176)
        Me.chkDobleVerifica.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.chkDobleVerifica.MenuManager = Me.RibbonControl
        Me.chkDobleVerifica.Name = "chkDobleVerifica"
        Me.chkDobleVerifica.Properties.Caption = ""
        Me.chkDobleVerifica.Size = New System.Drawing.Size(24, 24)
        Me.chkDobleVerifica.TabIndex = 10
        '
        'lblDobleVerif
        '
        Me.lblDobleVerif.AutoSize = True
        Me.lblDobleVerif.Location = New System.Drawing.Point(635, 176)
        Me.lblDobleVerif.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblDobleVerif.Name = "lblDobleVerif"
        Me.lblDobleVerif.Size = New System.Drawing.Size(114, 16)
        Me.lblDobleVerif.TabIndex = 11
        Me.lblDobleVerif.Text = "Doble Verificación:"
        '
        'lblTipoConteo
        '
        Me.lblTipoConteo.AutoSize = True
        Me.lblTipoConteo.Location = New System.Drawing.Point(635, 144)
        Me.lblTipoConteo.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblTipoConteo.Name = "lblTipoConteo"
        Me.lblTipoConteo.Size = New System.Drawing.Size(81, 16)
        Me.lblTipoConteo.TabIndex = 20
        Me.lblTipoConteo.Text = "Tipo Conteo:"
        Me.lblTipoConteo.Visible = False
        '
        'lblTipoInventario
        '
        Me.lblTipoInventario.AutoSize = True
        Me.lblTipoInventario.Location = New System.Drawing.Point(40, 176)
        Me.lblTipoInventario.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblTipoInventario.Name = "lblTipoInventario"
        Me.lblTipoInventario.Size = New System.Drawing.Size(98, 16)
        Me.lblTipoInventario.TabIndex = 8
        Me.lblTipoInventario.Text = "Tipo Inventario:"
        '
        'lblFecha
        '
        Me.lblFecha.AutoSize = True
        Me.lblFecha.Location = New System.Drawing.Point(40, 206)
        Me.lblFecha.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(46, 16)
        Me.lblFecha.TabIndex = 14
        Me.lblFecha.Text = "Fecha:"
        '
        'Fecha
        '
        Me.Fecha.EditValue = New Date(2017, 12, 4, 13, 30, 51, 367)
        Me.Fecha.Location = New System.Drawing.Point(236, 206)
        Me.Fecha.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.Fecha.MenuManager = Me.RibbonControl
        Me.Fecha.Name = "Fecha"
        Me.Fecha.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fecha.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fecha.Size = New System.Drawing.Size(314, 22)
        Me.Fecha.TabIndex = 15
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Location = New System.Drawing.Point(236, 144)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmbPropietario.MenuManager = Me.RibbonControl
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Size = New System.Drawing.Size(314, 22)
        Me.cmbPropietario.TabIndex = 6
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(236, 112)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmbBodega.MenuManager = Me.RibbonControl
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Properties.ReadOnly = True
        Me.cmbBodega.Size = New System.Drawing.Size(314, 22)
        Me.cmbBodega.TabIndex = 4
        '
        'Estado
        '
        Me.Estado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Estado.Location = New System.Drawing.Point(236, 82)
        Me.Estado.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Estado.Name = "Estado"
        Me.Estado.Size = New System.Drawing.Size(317, 22)
        Me.Estado.TabIndex = 3
        Me.Estado.Text = "Estado"
        '
        'lblCod
        '
        Me.lblCod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCod.Location = New System.Drawing.Point(236, 54)
        Me.lblCod.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblCod.Name = "lblCod"
        Me.lblCod.Size = New System.Drawing.Size(318, 24)
        Me.lblCod.TabIndex = 1
        Me.lblCod.Text = "000000001"
        '
        'lblPropietario
        '
        Me.lblPropietario.AutoSize = True
        Me.lblPropietario.Location = New System.Drawing.Point(40, 144)
        Me.lblPropietario.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblPropietario.Name = "lblPropietario"
        Me.lblPropietario.Size = New System.Drawing.Size(74, 16)
        Me.lblPropietario.TabIndex = 7
        Me.lblPropietario.Text = "Propietario:"
        '
        'lblBodega
        '
        Me.lblBodega.AutoSize = True
        Me.lblBodega.Location = New System.Drawing.Point(40, 118)
        Me.lblBodega.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblBodega.Name = "lblBodega"
        Me.lblBodega.Size = New System.Drawing.Size(54, 16)
        Me.lblBodega.TabIndex = 5
        Me.lblBodega.Text = "Bodega:"
        '
        'lblEstado
        '
        Me.lblEstado.AutoSize = True
        Me.lblEstado.Location = New System.Drawing.Point(40, 89)
        Me.lblEstado.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(50, 16)
        Me.lblEstado.TabIndex = 2
        Me.lblEstado.Text = "Estado:"
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Location = New System.Drawing.Point(40, 54)
        Me.lblCodigo.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(51, 16)
        Me.lblCodigo.TabIndex = 0
        Me.lblCodigo.Text = "Código:"
        '
        'xtraTabInv
        '
        Me.xtraTabInv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtraTabInv.Location = New System.Drawing.Point(0, 193)
        Me.xtraTabInv.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.xtraTabInv.Name = "xtraTabInv"
        Me.xtraTabInv.SelectedTabPage = Me.Datos
        Me.xtraTabInv.Size = New System.Drawing.Size(1938, 663)
        Me.xtraTabInv.TabIndex = 0
        Me.xtraTabInv.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.Datos, Me.tabAsignacionProductos, Me.Tramos, Me.tabAsignacionOperadores, Me.tabDetalle, Me.tabConteo, Me.tabDiferenciasInventario, Me.tabConteoOperador, Me.tabReconteo, Me.tabInvTeorico, Me.tabComparativoERPWMS, Me.tbne, Me.tabInvCongelado, Me.TabInventarioCostos, Me.tabKPI, Me.tabAsignacionUbicaciones, Me.xtpRegularizacion})
        '
        'tabAsignacionProductos
        '
        Me.tabAsignacionProductos.Controls.Add(Me.GroupControl2)
        Me.tabAsignacionProductos.Controls.Add(Me.dgridAsignacionProductos)
        Me.tabAsignacionProductos.Controls.Add(Me.grpProductos)
        Me.tabAsignacionProductos.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.tabAsignacionProductos.Name = "tabAsignacionProductos"
        Me.tabAsignacionProductos.Size = New System.Drawing.Size(1936, 632)
        Me.tabAsignacionProductos.Text = "Asignación de Productos"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.lblUbicacionesUnicas)
        Me.GroupControl2.Controls.Add(Me.Label13)
        Me.GroupControl2.Controls.Add(Me.lblProductosUnicos)
        Me.GroupControl2.Controls.Add(Me.lblRegistros)
        Me.GroupControl2.Controls.Add(Me.Label5)
        Me.GroupControl2.Controls.Add(Me.Label6)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl2.Location = New System.Drawing.Point(0, 553)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1936, 79)
        Me.GroupControl2.TabIndex = 43
        '
        'lblUbicacionesUnicas
        '
        Me.lblUbicacionesUnicas.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblUbicacionesUnicas.Location = New System.Drawing.Point(646, 37)
        Me.lblUbicacionesUnicas.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lblUbicacionesUnicas.Name = "lblUbicacionesUnicas"
        Me.lblUbicacionesUnicas.ReadOnly = True
        Me.lblUbicacionesUnicas.Size = New System.Drawing.Size(54, 26)
        Me.lblUbicacionesUnicas.TabIndex = 17
        Me.lblUbicacionesUnicas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(490, 39)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(156, 21)
        Me.Label13.TabIndex = 16
        Me.Label13.Text = "Ubicaciones únicas:"
        '
        'lblProductosUnicos
        '
        Me.lblProductosUnicos.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblProductosUnicos.Location = New System.Drawing.Point(368, 37)
        Me.lblProductosUnicos.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lblProductosUnicos.Name = "lblProductosUnicos"
        Me.lblProductosUnicos.ReadOnly = True
        Me.lblProductosUnicos.Size = New System.Drawing.Size(76, 26)
        Me.lblProductosUnicos.TabIndex = 15
        Me.lblProductosUnicos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblRegistros
        '
        Me.lblRegistros.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblRegistros.Location = New System.Drawing.Point(108, 38)
        Me.lblRegistros.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lblRegistros.Name = "lblRegistros"
        Me.lblRegistros.ReadOnly = True
        Me.lblRegistros.Size = New System.Drawing.Size(65, 26)
        Me.lblRegistros.TabIndex = 14
        Me.lblRegistros.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(226, 39)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(141, 21)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Productos unicos:"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(23, 39)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 22)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Registros:"
        '
        'dgridAsignacionProductos
        '
        Me.dgridAsignacionProductos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridAsignacionProductos.Location = New System.Drawing.Point(0, 114)
        Me.dgridAsignacionProductos.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dgridAsignacionProductos.Name = "dgridAsignacionProductos"
        Me.dgridAsignacionProductos.OptionsBehavior.AllowBoundCheckBoxesInVirtualMode = True
        Me.dgridAsignacionProductos.OptionsBehavior.ReadOnly = True
        Me.dgridAsignacionProductos.OptionsFind.AlwaysVisible = True
        Me.dgridAsignacionProductos.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check
        Me.dgridAsignacionProductos.OptionsView.ShowAutoFilterRow = True
        Me.dgridAsignacionProductos.OptionsView.ShowSummaryFooter = True
        Me.dgridAsignacionProductos.RowHeight = 30
        Me.dgridAsignacionProductos.Size = New System.Drawing.Size(1936, 518)
        Me.dgridAsignacionProductos.TabIndex = 1
        Me.dgridAsignacionProductos.TreeLevelWidth = 17
        '
        'grpProductos
        '
        Me.grpProductos.Controls.Add(Me.cmdQuitaOpProd)
        Me.grpProductos.Controls.Add(Me.cmdAsignaOpProd)
        Me.grpProductos.Controls.Add(Me.twTodos)
        Me.grpProductos.Controls.Add(Me.cmbOperadorProd)
        Me.grpProductos.Controls.Add(Me.Label1)
        Me.grpProductos.Controls.Add(Me.cmdEliminarOpProd)
        Me.grpProductos.Controls.Add(Me.cmdAsignarOp)
        Me.grpProductos.Controls.Add(Me.cmdQuitarProducto)
        Me.grpProductos.Controls.Add(Me.cmdAgregarProducto)
        Me.grpProductos.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpProductos.Location = New System.Drawing.Point(0, 0)
        Me.grpProductos.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grpProductos.Name = "grpProductos"
        Me.grpProductos.ShowCaption = False
        Me.grpProductos.Size = New System.Drawing.Size(1936, 114)
        Me.grpProductos.TabIndex = 0
        '
        'cmdQuitaOpProd
        '
        Me.cmdQuitaOpProd.ImageOptions.Image = CType(resources.GetObject("cmdQuitaOpProd.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdQuitaOpProd.Location = New System.Drawing.Point(902, 15)
        Me.cmdQuitaOpProd.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmdQuitaOpProd.Name = "cmdQuitaOpProd"
        Me.cmdQuitaOpProd.Size = New System.Drawing.Size(147, 44)
        Me.cmdQuitaOpProd.TabIndex = 8
        Me.cmdQuitaOpProd.Text = "Quitar operador"
        '
        'cmdAsignaOpProd
        '
        Me.cmdAsignaOpProd.ImageOptions.Image = CType(resources.GetObject("cmdAsignaOpProd.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdAsignaOpProd.Location = New System.Drawing.Point(744, 15)
        Me.cmdAsignaOpProd.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmdAsignaOpProd.Name = "cmdAsignaOpProd"
        Me.cmdAsignaOpProd.Size = New System.Drawing.Size(147, 44)
        Me.cmdAsignaOpProd.TabIndex = 7
        Me.cmdAsignaOpProd.Text = "Asignar operador"
        '
        'twTodos
        '
        Me.twTodos.Location = New System.Drawing.Point(438, 58)
        Me.twTodos.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.twTodos.MenuManager = Me.RibbonControl
        Me.twTodos.Name = "twTodos"
        Me.twTodos.Properties.OffText = "Ningún producto"
        Me.twTodos.Properties.OnText = "Todos los productos"
        Me.twTodos.Size = New System.Drawing.Size(280, 24)
        Me.twTodos.TabIndex = 6
        '
        'cmbOperadorProd
        '
        Me.cmbOperadorProd.Location = New System.Drawing.Point(438, 26)
        Me.cmbOperadorProd.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmbOperadorProd.MenuManager = Me.RibbonControl
        Me.cmbOperadorProd.Name = "cmbOperadorProd"
        Me.cmbOperadorProd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOperadorProd.Properties.NullText = ""
        Me.cmbOperadorProd.Size = New System.Drawing.Size(281, 22)
        Me.cmbOperadorProd.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(348, 26)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Operadores:"
        '
        'cmdEliminarOpProd
        '
        Me.cmdEliminarOpProd.ImageOptions.SvgImage = CType(resources.GetObject("cmdEliminarOpProd.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdEliminarOpProd.Location = New System.Drawing.Point(1208, 15)
        Me.cmdEliminarOpProd.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmdEliminarOpProd.Name = "cmdEliminarOpProd"
        Me.cmdEliminarOpProd.Size = New System.Drawing.Size(140, 44)
        Me.cmdEliminarOpProd.TabIndex = 5
        Me.cmdEliminarOpProd.Text = "Quitar todos" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "los operadores"
        '
        'cmdAsignarOp
        '
        Me.cmdAsignarOp.ImageOptions.SvgImage = CType(resources.GetObject("cmdAsignarOp.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdAsignarOp.Location = New System.Drawing.Point(1059, 15)
        Me.cmdAsignarOp.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmdAsignarOp.Name = "cmdAsignarOp"
        Me.cmdAsignarOp.Size = New System.Drawing.Size(140, 44)
        Me.cmdAsignarOp.TabIndex = 4
        Me.cmdAsignarOp.Text = "Asignar todos " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "los operadores"
        '
        'cmdQuitarProducto
        '
        Me.cmdQuitarProducto.ImageOptions.Image = CType(resources.GetObject("cmdQuitarProducto.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdQuitarProducto.Location = New System.Drawing.Point(172, 18)
        Me.cmdQuitarProducto.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmdQuitarProducto.Name = "cmdQuitarProducto"
        Me.cmdQuitarProducto.Size = New System.Drawing.Size(149, 57)
        Me.cmdQuitarProducto.TabIndex = 3
        Me.cmdQuitarProducto.Text = "Quitar" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Productos"
        '
        'cmdAgregarProducto
        '
        Me.cmdAgregarProducto.ImageOptions.Image = CType(resources.GetObject("cmdAgregarProducto.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdAgregarProducto.Location = New System.Drawing.Point(13, 18)
        Me.cmdAgregarProducto.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmdAgregarProducto.Name = "cmdAgregarProducto"
        Me.cmdAgregarProducto.Size = New System.Drawing.Size(149, 57)
        Me.cmdAgregarProducto.TabIndex = 2
        Me.cmdAgregarProducto.Text = "Agregar" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Productos"
        '
        'Tramos
        '
        Me.Tramos.Controls.Add(Me.grpTramos)
        Me.Tramos.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.Tramos.Name = "Tramos"
        Me.Tramos.Size = New System.Drawing.Size(1936, 632)
        Me.Tramos.Text = "Asignación Tramos"
        '
        'grpTramos
        '
        Me.grpTramos.Controls.Add(Me.chkSeleccionarTodos)
        Me.grpTramos.Controls.Add(Me.chkTramosAsig)
        Me.grpTramos.Controls.Add(Me.dgridAsignacionTramos)
        Me.grpTramos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpTramos.Location = New System.Drawing.Point(0, 0)
        Me.grpTramos.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grpTramos.Name = "grpTramos"
        Me.grpTramos.ShowCaption = False
        Me.grpTramos.Size = New System.Drawing.Size(1936, 632)
        Me.grpTramos.TabIndex = 0
        '
        'chkSeleccionarTodos
        '
        Me.chkSeleccionarTodos.AutoSize = True
        Me.chkSeleccionarTodos.Location = New System.Drawing.Point(901, 30)
        Me.chkSeleccionarTodos.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.chkSeleccionarTodos.Name = "chkSeleccionarTodos"
        Me.chkSeleccionarTodos.Size = New System.Drawing.Size(134, 20)
        Me.chkSeleccionarTodos.TabIndex = 2
        Me.chkSeleccionarTodos.Text = "Seleccionar Todos"
        Me.chkSeleccionarTodos.UseVisualStyleBackColor = True
        '
        'chkTramosAsig
        '
        Me.chkTramosAsig.AutoSize = True
        Me.chkTramosAsig.Location = New System.Drawing.Point(758, 30)
        Me.chkTramosAsig.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.chkTramosAsig.Name = "chkTramosAsig"
        Me.chkTramosAsig.Size = New System.Drawing.Size(87, 20)
        Me.chkTramosAsig.TabIndex = 1
        Me.chkTramosAsig.Text = "Asignados"
        Me.chkTramosAsig.UseVisualStyleBackColor = True
        '
        'dgridAsignacionTramos
        '
        Me.dgridAsignacionTramos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridAsignacionTramos.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dgridAsignacionTramos.Location = New System.Drawing.Point(2, 2)
        Me.dgridAsignacionTramos.MainView = Me.GridViewTramos
        Me.dgridAsignacionTramos.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dgridAsignacionTramos.Name = "dgridAsignacionTramos"
        Me.dgridAsignacionTramos.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2})
        Me.dgridAsignacionTramos.Size = New System.Drawing.Size(1932, 628)
        Me.dgridAsignacionTramos.TabIndex = 0
        Me.dgridAsignacionTramos.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewTramos, Me.GridView5})
        '
        'GridViewTramos
        '
        Me.GridViewTramos.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSeleccionar, Me.colIdTramos, Me.coldIdSector, Me.coldTramo})
        Me.GridViewTramos.DetailHeight = 437
        Me.GridViewTramos.GridControl = Me.dgridAsignacionTramos
        Me.GridViewTramos.Name = "GridViewTramos"
        Me.GridViewTramos.OptionsEditForm.PopupEditFormWidth = 1000
        Me.GridViewTramos.OptionsFind.AlwaysVisible = True
        Me.GridViewTramos.OptionsView.ShowAutoFilterRow = True
        Me.GridViewTramos.OptionsView.ShowFooter = True
        Me.GridViewTramos.OptionsView.ShowGroupPanel = False
        '
        'colSeleccionar
        '
        Me.colSeleccionar.ColumnEdit = Me.RepositoryItemCheckEdit2
        Me.colSeleccionar.FieldName = "Seleccionar"
        Me.colSeleccionar.MinWidth = 24
        Me.colSeleccionar.Name = "colSeleccionar"
        Me.colSeleccionar.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Seleccionar", "{0}")})
        Me.colSeleccionar.Visible = True
        Me.colSeleccionar.VisibleIndex = 0
        Me.colSeleccionar.Width = 94
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'colIdTramos
        '
        Me.colIdTramos.Caption = "Código"
        Me.colIdTramos.FieldName = "IdTramo"
        Me.colIdTramos.MinWidth = 24
        Me.colIdTramos.Name = "colIdTramos"
        Me.colIdTramos.OptionsColumn.AllowEdit = False
        Me.colIdTramos.Visible = True
        Me.colIdTramos.VisibleIndex = 1
        Me.colIdTramos.Width = 94
        '
        'coldIdSector
        '
        Me.coldIdSector.Caption = "IdSector"
        Me.coldIdSector.FieldName = "IdSector"
        Me.coldIdSector.MinWidth = 24
        Me.coldIdSector.Name = "coldIdSector"
        Me.coldIdSector.Visible = True
        Me.coldIdSector.VisibleIndex = 2
        Me.coldIdSector.Width = 94
        '
        'coldTramo
        '
        Me.coldTramo.Caption = "Tramo"
        Me.coldTramo.FieldName = "Tramo"
        Me.coldTramo.MinWidth = 24
        Me.coldTramo.Name = "coldTramo"
        Me.coldTramo.Visible = True
        Me.coldTramo.VisibleIndex = 3
        Me.coldTramo.Width = 94
        '
        'GridView5
        '
        Me.GridView5.DetailHeight = 437
        Me.GridView5.GridControl = Me.dgridAsignacionTramos
        Me.GridView5.Name = "GridView5"
        Me.GridView5.OptionsEditForm.PopupEditFormWidth = 1000
        '
        'tabAsignacionOperadores
        '
        Me.tabAsignacionOperadores.Controls.Add(Me.dgridAsignacionOperadores)
        Me.tabAsignacionOperadores.Controls.Add(Me.grpOperadores)
        Me.tabAsignacionOperadores.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.tabAsignacionOperadores.Name = "tabAsignacionOperadores"
        Me.tabAsignacionOperadores.Size = New System.Drawing.Size(1936, 632)
        Me.tabAsignacionOperadores.Text = "Asignación de Ubicaciones"
        '
        'dgridAsignacionOperadores
        '
        Me.dgridAsignacionOperadores.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridAsignacionOperadores.Location = New System.Drawing.Point(0, 86)
        Me.dgridAsignacionOperadores.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dgridAsignacionOperadores.Name = "dgridAsignacionOperadores"
        Me.dgridAsignacionOperadores.OptionsBehavior.AllowBoundCheckBoxesInVirtualMode = True
        Me.dgridAsignacionOperadores.OptionsBehavior.Editable = False
        Me.dgridAsignacionOperadores.OptionsBehavior.ReadOnly = True
        Me.dgridAsignacionOperadores.OptionsFind.AlwaysVisible = True
        Me.dgridAsignacionOperadores.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.dgridAsignacionOperadores.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check
        Me.dgridAsignacionOperadores.RowHeight = 30
        Me.dgridAsignacionOperadores.Size = New System.Drawing.Size(1936, 546)
        Me.dgridAsignacionOperadores.TabIndex = 1
        Me.dgridAsignacionOperadores.TreeLevelWidth = 17
        '
        'grpOperadores
        '
        Me.grpOperadores.Controls.Add(Me.cmdQuitar)
        Me.grpOperadores.Controls.Add(Me.cmdQuitarOperador)
        Me.grpOperadores.Controls.Add(Me.cmdAgregar)
        Me.grpOperadores.Controls.Add(Me.cmdAsignarOperador)
        Me.grpOperadores.Controls.Add(Me.cmbOperador)
        Me.grpOperadores.Controls.Add(Me.lblOperador)
        Me.grpOperadores.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpOperadores.Location = New System.Drawing.Point(0, 0)
        Me.grpOperadores.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grpOperadores.Name = "grpOperadores"
        Me.grpOperadores.ShowCaption = False
        Me.grpOperadores.Size = New System.Drawing.Size(1936, 86)
        Me.grpOperadores.TabIndex = 0
        '
        'cmdQuitar
        '
        Me.cmdQuitar.ImageOptions.Image = CType(resources.GetObject("cmdQuitar.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdQuitar.Location = New System.Drawing.Point(189, 9)
        Me.cmdQuitar.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmdQuitar.Name = "cmdQuitar"
        Me.cmdQuitar.Size = New System.Drawing.Size(152, 52)
        Me.cmdQuitar.TabIndex = 1
        Me.cmdQuitar.Text = "Quitar" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ubicaciones"
        '
        'cmdQuitarOperador
        '
        Me.cmdQuitarOperador.ImageOptions.Image = CType(resources.GetObject("cmdQuitarOperador.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdQuitarOperador.Location = New System.Drawing.Point(892, 14)
        Me.cmdQuitarOperador.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmdQuitarOperador.Name = "cmdQuitarOperador"
        Me.cmdQuitarOperador.Size = New System.Drawing.Size(113, 48)
        Me.cmdQuitarOperador.TabIndex = 3
        Me.cmdQuitarOperador.Text = "Quitar"
        '
        'cmdAgregar
        '
        Me.cmdAgregar.ImageOptions.Image = CType(resources.GetObject("cmdAgregar.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdAgregar.Location = New System.Drawing.Point(27, 10)
        Me.cmdAgregar.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmdAgregar.Name = "cmdAgregar"
        Me.cmdAgregar.Size = New System.Drawing.Size(152, 52)
        Me.cmdAgregar.TabIndex = 0
        Me.cmdAgregar.Text = "Agregar " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ubicaciones"
        '
        'cmdAsignarOperador
        '
        Me.cmdAsignarOperador.ImageOptions.Image = CType(resources.GetObject("cmdAsignarOperador.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdAsignarOperador.Location = New System.Drawing.Point(770, 14)
        Me.cmdAsignarOperador.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmdAsignarOperador.Name = "cmdAsignarOperador"
        Me.cmdAsignarOperador.Size = New System.Drawing.Size(113, 48)
        Me.cmdAsignarOperador.TabIndex = 2
        Me.cmdAsignarOperador.Text = "Asignar"
        '
        'cmbOperador
        '
        Me.cmbOperador.Location = New System.Drawing.Point(479, 25)
        Me.cmbOperador.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmbOperador.MenuManager = Me.RibbonControl
        Me.cmbOperador.Name = "cmbOperador"
        Me.cmbOperador.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOperador.Properties.NullText = ""
        Me.cmbOperador.Size = New System.Drawing.Size(281, 22)
        Me.cmbOperador.TabIndex = 1
        '
        'lblOperador
        '
        Me.lblOperador.AutoSize = True
        Me.lblOperador.Location = New System.Drawing.Point(380, 27)
        Me.lblOperador.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblOperador.Name = "lblOperador"
        Me.lblOperador.Size = New System.Drawing.Size(79, 16)
        Me.lblOperador.TabIndex = 0
        Me.lblOperador.Text = "Operadores:"
        '
        'tabConteo
        '
        Me.tabConteo.Controls.Add(Me.dgridInventarioCiclico)
        Me.tabConteo.Controls.Add(Me.grpConteoCi)
        Me.tabConteo.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.tabConteo.Name = "tabConteo"
        Me.tabConteo.Size = New System.Drawing.Size(1936, 633)
        Me.tabConteo.Text = "Detalle de Inventario Ciclico"
        '
        'dgridInventarioCiclico
        '
        Me.dgridInventarioCiclico.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridInventarioCiclico.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dgridInventarioCiclico.Location = New System.Drawing.Point(0, 170)
        Me.dgridInventarioCiclico.MainView = Me.gdviewTeorico
        Me.dgridInventarioCiclico.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dgridInventarioCiclico.MenuManager = Me.RibbonControl
        Me.dgridInventarioCiclico.Name = "dgridInventarioCiclico"
        Me.dgridInventarioCiclico.Size = New System.Drawing.Size(1936, 463)
        Me.dgridInventarioCiclico.TabIndex = 1
        Me.dgridInventarioCiclico.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdviewTeorico})
        '
        'gdviewTeorico
        '
        Me.gdviewTeorico.DetailHeight = 437
        Me.gdviewTeorico.GridControl = Me.dgridInventarioCiclico
        Me.gdviewTeorico.Name = "gdviewTeorico"
        Me.gdviewTeorico.OptionsBehavior.ReadOnly = True
        Me.gdviewTeorico.OptionsEditForm.PopupEditFormWidth = 1000
        Me.gdviewTeorico.OptionsView.ColumnAutoWidth = False
        Me.gdviewTeorico.OptionsView.ShowAutoFilterRow = True
        '
        'grpConteoCi
        '
        Me.grpConteoCi.Controls.Add(Me.prgPanInvConteo)
        Me.grpConteoCi.Controls.Add(Me.LinklblUbicacion)
        Me.grpConteoCi.Controls.Add(Me.txtNombreUbicacion)
        Me.grpConteoCi.Controls.Add(Me.txtIdUbicacion)
        Me.grpConteoCi.Controls.Add(Me.txtNombreProducto)
        Me.grpConteoCi.Controls.Add(Me.LinklblProducto)
        Me.grpConteoCi.Controls.Add(Me.txtIdProducto)
        Me.grpConteoCi.Controls.Add(Me.txtNombreOperador)
        Me.grpConteoCi.Controls.Add(Me.txtIdOperador)
        Me.grpConteoCi.Controls.Add(Me.LinklblOperador)
        Me.grpConteoCi.Controls.Add(Me.txtNombreTramo)
        Me.grpConteoCi.Controls.Add(Me.txtIdTramo)
        Me.grpConteoCi.Controls.Add(Me.txtClasificacionNombre)
        Me.grpConteoCi.Controls.Add(Me.txtIdClasificacion)
        Me.grpConteoCi.Controls.Add(Me.txtFamiliaNombre)
        Me.grpConteoCi.Controls.Add(Me.txtIdFamilia)
        Me.grpConteoCi.Controls.Add(Me.linklblTramo)
        Me.grpConteoCi.Controls.Add(Me.linklblClasificacion)
        Me.grpConteoCi.Controls.Add(Me.linklblFamilia)
        Me.grpConteoCi.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpConteoCi.Location = New System.Drawing.Point(0, 0)
        Me.grpConteoCi.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grpConteoCi.Name = "grpConteoCi"
        Me.grpConteoCi.Size = New System.Drawing.Size(1936, 170)
        Me.grpConteoCi.TabIndex = 0
        Me.grpConteoCi.Text = "Filtros"
        '
        'prgPanInvConteo
        '
        Me.prgPanInvConteo.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.prgPanInvConteo.Location = New System.Drawing.Point(2, 67)
        Me.prgPanInvConteo.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.prgPanInvConteo.Name = "prgPanInvConteo"
        Me.prgPanInvConteo.Size = New System.Drawing.Size(1932, 101)
        Me.prgPanInvConteo.TabIndex = 18
        Me.prgPanInvConteo.Visible = False
        '
        'LinklblUbicacion
        '
        Me.LinklblUbicacion.AutoSize = True
        Me.LinklblUbicacion.Location = New System.Drawing.Point(1078, 42)
        Me.LinklblUbicacion.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LinklblUbicacion.Name = "LinklblUbicacion"
        Me.LinklblUbicacion.Size = New System.Drawing.Size(61, 16)
        Me.LinklblUbicacion.TabIndex = 6
        Me.LinklblUbicacion.TabStop = True
        Me.LinklblUbicacion.Text = "Ubicación"
        '
        'txtNombreUbicacion
        '
        Me.txtNombreUbicacion.Location = New System.Drawing.Point(1268, 34)
        Me.txtNombreUbicacion.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtNombreUbicacion.MenuManager = Me.RibbonControl
        Me.txtNombreUbicacion.Name = "txtNombreUbicacion"
        Me.txtNombreUbicacion.Size = New System.Drawing.Size(286, 22)
        Me.txtNombreUbicacion.TabIndex = 8
        '
        'txtIdUbicacion
        '
        Me.txtIdUbicacion.Location = New System.Drawing.Point(1162, 34)
        Me.txtIdUbicacion.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtIdUbicacion.MenuManager = Me.RibbonControl
        Me.txtIdUbicacion.Name = "txtIdUbicacion"
        Me.txtIdUbicacion.Size = New System.Drawing.Size(96, 22)
        Me.txtIdUbicacion.TabIndex = 7
        '
        'txtNombreProducto
        '
        Me.txtNombreProducto.Location = New System.Drawing.Point(754, 71)
        Me.txtNombreProducto.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtNombreProducto.MenuManager = Me.RibbonControl
        Me.txtNombreProducto.Name = "txtNombreProducto"
        Me.txtNombreProducto.Size = New System.Drawing.Size(286, 22)
        Me.txtNombreProducto.TabIndex = 14
        '
        'LinklblProducto
        '
        Me.LinklblProducto.AutoSize = True
        Me.LinklblProducto.Location = New System.Drawing.Point(568, 79)
        Me.LinklblProducto.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LinklblProducto.Name = "LinklblProducto"
        Me.LinklblProducto.Size = New System.Drawing.Size(57, 16)
        Me.LinklblProducto.TabIndex = 12
        Me.LinklblProducto.TabStop = True
        Me.LinklblProducto.Text = "Producto"
        '
        'txtIdProducto
        '
        Me.txtIdProducto.Location = New System.Drawing.Point(649, 71)
        Me.txtIdProducto.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtIdProducto.MenuManager = Me.RibbonControl
        Me.txtIdProducto.Name = "txtIdProducto"
        Me.txtIdProducto.Size = New System.Drawing.Size(96, 22)
        Me.txtIdProducto.TabIndex = 13
        '
        'txtNombreOperador
        '
        Me.txtNombreOperador.Location = New System.Drawing.Point(754, 34)
        Me.txtNombreOperador.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtNombreOperador.MenuManager = Me.RibbonControl
        Me.txtNombreOperador.Name = "txtNombreOperador"
        Me.txtNombreOperador.Size = New System.Drawing.Size(286, 22)
        Me.txtNombreOperador.TabIndex = 5
        '
        'txtIdOperador
        '
        Me.txtIdOperador.Location = New System.Drawing.Point(649, 34)
        Me.txtIdOperador.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtIdOperador.MenuManager = Me.RibbonControl
        Me.txtIdOperador.Name = "txtIdOperador"
        Me.txtIdOperador.Size = New System.Drawing.Size(96, 22)
        Me.txtIdOperador.TabIndex = 4
        '
        'LinklblOperador
        '
        Me.LinklblOperador.AutoSize = True
        Me.LinklblOperador.Location = New System.Drawing.Point(562, 38)
        Me.LinklblOperador.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LinklblOperador.Name = "LinklblOperador"
        Me.LinklblOperador.Size = New System.Drawing.Size(61, 16)
        Me.LinklblOperador.TabIndex = 0
        Me.LinklblOperador.TabStop = True
        Me.LinklblOperador.Text = "Operador"
        '
        'txtNombreTramo
        '
        Me.txtNombreTramo.Location = New System.Drawing.Point(1268, 71)
        Me.txtNombreTramo.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtNombreTramo.MenuManager = Me.RibbonControl
        Me.txtNombreTramo.Name = "txtNombreTramo"
        Me.txtNombreTramo.Size = New System.Drawing.Size(286, 22)
        Me.txtNombreTramo.TabIndex = 17
        '
        'txtIdTramo
        '
        Me.txtIdTramo.Location = New System.Drawing.Point(1162, 71)
        Me.txtIdTramo.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtIdTramo.MenuManager = Me.RibbonControl
        Me.txtIdTramo.Name = "txtIdTramo"
        Me.txtIdTramo.Size = New System.Drawing.Size(96, 22)
        Me.txtIdTramo.TabIndex = 16
        '
        'txtClasificacionNombre
        '
        Me.txtClasificacionNombre.Location = New System.Drawing.Point(232, 71)
        Me.txtClasificacionNombre.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtClasificacionNombre.MenuManager = Me.RibbonControl
        Me.txtClasificacionNombre.Name = "txtClasificacionNombre"
        Me.txtClasificacionNombre.Size = New System.Drawing.Size(286, 22)
        Me.txtClasificacionNombre.TabIndex = 11
        '
        'txtIdClasificacion
        '
        Me.txtIdClasificacion.Location = New System.Drawing.Point(128, 71)
        Me.txtIdClasificacion.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtIdClasificacion.MenuManager = Me.RibbonControl
        Me.txtIdClasificacion.Name = "txtIdClasificacion"
        Me.txtIdClasificacion.Size = New System.Drawing.Size(96, 22)
        Me.txtIdClasificacion.TabIndex = 10
        '
        'txtFamiliaNombre
        '
        Me.txtFamiliaNombre.Location = New System.Drawing.Point(232, 34)
        Me.txtFamiliaNombre.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtFamiliaNombre.MenuManager = Me.RibbonControl
        Me.txtFamiliaNombre.Name = "txtFamiliaNombre"
        Me.txtFamiliaNombre.Size = New System.Drawing.Size(286, 22)
        Me.txtFamiliaNombre.TabIndex = 3
        '
        'txtIdFamilia
        '
        Me.txtIdFamilia.Location = New System.Drawing.Point(128, 34)
        Me.txtIdFamilia.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtIdFamilia.MenuManager = Me.RibbonControl
        Me.txtIdFamilia.Name = "txtIdFamilia"
        Me.txtIdFamilia.Size = New System.Drawing.Size(96, 22)
        Me.txtIdFamilia.TabIndex = 2
        '
        'linklblTramo
        '
        Me.linklblTramo.AutoSize = True
        Me.linklblTramo.Location = New System.Drawing.Point(1100, 79)
        Me.linklblTramo.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.linklblTramo.Name = "linklblTramo"
        Me.linklblTramo.Size = New System.Drawing.Size(45, 16)
        Me.linklblTramo.TabIndex = 15
        Me.linklblTramo.TabStop = True
        Me.linklblTramo.Text = "Tramo"
        '
        'linklblClasificacion
        '
        Me.linklblClasificacion.AutoSize = True
        Me.linklblClasificacion.Location = New System.Drawing.Point(16, 79)
        Me.linklblClasificacion.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.linklblClasificacion.Name = "linklblClasificacion"
        Me.linklblClasificacion.Size = New System.Drawing.Size(77, 16)
        Me.linklblClasificacion.TabIndex = 9
        Me.linklblClasificacion.TabStop = True
        Me.linklblClasificacion.Text = "Clasificación"
        '
        'linklblFamilia
        '
        Me.linklblFamilia.AutoSize = True
        Me.linklblFamilia.Location = New System.Drawing.Point(54, 42)
        Me.linklblFamilia.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.linklblFamilia.Name = "linklblFamilia"
        Me.linklblFamilia.Size = New System.Drawing.Size(48, 16)
        Me.linklblFamilia.TabIndex = 1
        Me.linklblFamilia.TabStop = True
        Me.linklblFamilia.Text = "Familia"
        '
        'tabDiferenciasInventario
        '
        Me.tabDiferenciasInventario.Controls.Add(Me.dgridDiferenciasCiclico)
        Me.tabDiferenciasInventario.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.tabDiferenciasInventario.Name = "tabDiferenciasInventario"
        Me.tabDiferenciasInventario.Size = New System.Drawing.Size(1936, 632)
        Me.tabDiferenciasInventario.Text = "Diferencias Inventario"
        '
        'dgridDiferenciasCiclico
        '
        Me.dgridDiferenciasCiclico.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridDiferenciasCiclico.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dgridDiferenciasCiclico.Location = New System.Drawing.Point(0, 0)
        Me.dgridDiferenciasCiclico.MainView = Me.gvDiferenciasCiclico
        Me.dgridDiferenciasCiclico.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dgridDiferenciasCiclico.MenuManager = Me.RibbonControl
        Me.dgridDiferenciasCiclico.Name = "dgridDiferenciasCiclico"
        Me.dgridDiferenciasCiclico.Size = New System.Drawing.Size(1936, 632)
        Me.dgridDiferenciasCiclico.TabIndex = 2
        Me.dgridDiferenciasCiclico.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvDiferenciasCiclico})
        '
        'gvDiferenciasCiclico
        '
        Me.gvDiferenciasCiclico.DetailHeight = 437
        Me.gvDiferenciasCiclico.GridControl = Me.dgridDiferenciasCiclico
        Me.gvDiferenciasCiclico.Name = "gvDiferenciasCiclico"
        Me.gvDiferenciasCiclico.OptionsBehavior.ReadOnly = True
        Me.gvDiferenciasCiclico.OptionsEditForm.PopupEditFormWidth = 1000
        Me.gvDiferenciasCiclico.OptionsView.ColumnAutoWidth = False
        Me.gvDiferenciasCiclico.OptionsView.ShowAutoFilterRow = True
        '
        'tabConteoOperador
        '
        Me.tabConteoOperador.Controls.Add(Me.dgridConteoOperador)
        Me.tabConteoOperador.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.tabConteoOperador.Name = "tabConteoOperador"
        Me.tabConteoOperador.Size = New System.Drawing.Size(1936, 632)
        Me.tabConteoOperador.Text = "Conteo por Operador"
        '
        'dgridConteoOperador
        '
        Me.dgridConteoOperador.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridConteoOperador.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dgridConteoOperador.Location = New System.Drawing.Point(0, 0)
        Me.dgridConteoOperador.MainView = Me.gvConteoOperador
        Me.dgridConteoOperador.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dgridConteoOperador.MenuManager = Me.RibbonControl
        Me.dgridConteoOperador.Name = "dgridConteoOperador"
        Me.dgridConteoOperador.Size = New System.Drawing.Size(1936, 632)
        Me.dgridConteoOperador.TabIndex = 1
        Me.dgridConteoOperador.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvConteoOperador})
        '
        'gvConteoOperador
        '
        Me.gvConteoOperador.DetailHeight = 437
        Me.gvConteoOperador.GridControl = Me.dgridConteoOperador
        Me.gvConteoOperador.Name = "gvConteoOperador"
        Me.gvConteoOperador.OptionsBehavior.Editable = False
        Me.gvConteoOperador.OptionsBehavior.ReadOnly = True
        Me.gvConteoOperador.OptionsEditForm.PopupEditFormWidth = 1000
        Me.gvConteoOperador.OptionsFind.AlwaysVisible = True
        Me.gvConteoOperador.OptionsView.ShowAutoFilterRow = True
        '
        'tabReconteo
        '
        Me.tabReconteo.Controls.Add(Me.grpReconteo)
        Me.tabReconteo.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.tabReconteo.Name = "tabReconteo"
        Me.tabReconteo.Size = New System.Drawing.Size(1936, 632)
        Me.tabReconteo.Text = "Detalle Reconteo"
        '
        'grpReconteo
        '
        Me.grpReconteo.Controls.Add(Me.grdReconteo)
        Me.grpReconteo.Controls.Add(Me.RibbonStatusBar1)
        Me.grpReconteo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpReconteo.Location = New System.Drawing.Point(0, 0)
        Me.grpReconteo.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grpReconteo.Name = "grpReconteo"
        Me.grpReconteo.ShowCaption = False
        Me.grpReconteo.Size = New System.Drawing.Size(1936, 632)
        Me.grpReconteo.TabIndex = 0
        '
        'grdReconteo
        '
        Me.grdReconteo.DataSource = Me.EncabezadoBindingSource
        Me.grdReconteo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdReconteo.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grdReconteo.Location = New System.Drawing.Point(2, 2)
        Me.grdReconteo.MainView = Me.GridView8
        Me.grdReconteo.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grdReconteo.MenuManager = Me.RibbonControl
        Me.grdReconteo.Name = "grdReconteo"
        Me.grdReconteo.Size = New System.Drawing.Size(1932, 595)
        Me.grdReconteo.TabIndex = 0
        Me.grdReconteo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView8})
        '
        'EncabezadoBindingSource
        '
        Me.EncabezadoBindingSource.DataMember = "Encabezado"
        Me.EncabezadoBindingSource.DataSource = Me.DSReconteoBindingSource
        '
        'DSReconteoBindingSource
        '
        Me.DSReconteoBindingSource.DataSource = Me.DSReconteo
        Me.DSReconteoBindingSource.Position = 0
        '
        'DSReconteo
        '
        Me.DSReconteo.DataSetName = "DSReconteo"
        Me.DSReconteo.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GridView8
        '
        Me.GridView8.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colCorrelativo, Me.colIdReconteoEnc, Me.colEstado, Me.colHora_Inicio, Me.colHora_Fin, Me.colIdInventarioEnc})
        Me.GridView8.DetailHeight = 437
        Me.GridView8.GridControl = Me.grdReconteo
        Me.GridView8.Name = "GridView8"
        Me.GridView8.OptionsBehavior.ReadOnly = True
        Me.GridView8.OptionsEditForm.PopupEditFormWidth = 1000
        Me.GridView8.OptionsFind.AlwaysVisible = True
        Me.GridView8.OptionsView.ColumnAutoWidth = False
        '
        'colCorrelativo
        '
        Me.colCorrelativo.FieldName = "Correlativo"
        Me.colCorrelativo.MinWidth = 24
        Me.colCorrelativo.Name = "colCorrelativo"
        Me.colCorrelativo.Visible = True
        Me.colCorrelativo.VisibleIndex = 0
        Me.colCorrelativo.Width = 101
        '
        'colIdReconteoEnc
        '
        Me.colIdReconteoEnc.FieldName = "IdReconteoEnc"
        Me.colIdReconteoEnc.MinWidth = 24
        Me.colIdReconteoEnc.Name = "colIdReconteoEnc"
        Me.colIdReconteoEnc.Visible = True
        Me.colIdReconteoEnc.VisibleIndex = 1
        Me.colIdReconteoEnc.Width = 94
        '
        'colEstado
        '
        Me.colEstado.FieldName = "Estado"
        Me.colEstado.MinWidth = 24
        Me.colEstado.Name = "colEstado"
        Me.colEstado.Visible = True
        Me.colEstado.VisibleIndex = 2
        Me.colEstado.Width = 94
        '
        'colHora_Inicio
        '
        Me.colHora_Inicio.FieldName = "Hora_Inicio"
        Me.colHora_Inicio.MinWidth = 24
        Me.colHora_Inicio.Name = "colHora_Inicio"
        Me.colHora_Inicio.Visible = True
        Me.colHora_Inicio.VisibleIndex = 3
        Me.colHora_Inicio.Width = 94
        '
        'colHora_Fin
        '
        Me.colHora_Fin.FieldName = "Hora_Fin"
        Me.colHora_Fin.MinWidth = 24
        Me.colHora_Fin.Name = "colHora_Fin"
        Me.colHora_Fin.Visible = True
        Me.colHora_Fin.VisibleIndex = 4
        Me.colHora_Fin.Width = 94
        '
        'colIdInventarioEnc
        '
        Me.colIdInventarioEnc.FieldName = "IdInventarioEnc"
        Me.colIdInventarioEnc.MinWidth = 24
        Me.colIdInventarioEnc.Name = "colIdInventarioEnc"
        Me.colIdInventarioEnc.Visible = True
        Me.colIdInventarioEnc.VisibleIndex = 5
        Me.colIdInventarioEnc.Width = 94
        '
        'tabInvTeorico
        '
        Me.tabInvTeorico.Controls.Add(Me.dgridInvTeorico)
        Me.tabInvTeorico.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.tabInvTeorico.Name = "tabInvTeorico"
        Me.tabInvTeorico.Size = New System.Drawing.Size(1936, 632)
        Me.tabInvTeorico.Text = "Comparativo Teórico WMS Vrs Físico"
        '
        'dgridInvTeorico
        '
        Me.dgridInvTeorico.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridInvTeorico.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dgridInvTeorico.Location = New System.Drawing.Point(0, 0)
        Me.dgridInvTeorico.MainView = Me.gvInvTeoricoWMS
        Me.dgridInvTeorico.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dgridInvTeorico.MenuManager = Me.RibbonControl
        Me.dgridInvTeorico.Name = "dgridInvTeorico"
        Me.dgridInvTeorico.Size = New System.Drawing.Size(1936, 632)
        Me.dgridInvTeorico.TabIndex = 0
        Me.dgridInvTeorico.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvInvTeoricoWMS})
        '
        'gvInvTeoricoWMS
        '
        Me.gvInvTeoricoWMS.DetailHeight = 437
        Me.gvInvTeoricoWMS.GridControl = Me.dgridInvTeorico
        Me.gvInvTeoricoWMS.Name = "gvInvTeoricoWMS"
        Me.gvInvTeoricoWMS.OptionsEditForm.PopupEditFormWidth = 1000
        Me.gvInvTeoricoWMS.OptionsSelection.MultiSelect = True
        Me.gvInvTeoricoWMS.OptionsView.ColumnAutoWidth = False
        Me.gvInvTeoricoWMS.OptionsView.ShowAutoFilterRow = True
        Me.gvInvTeoricoWMS.OptionsView.ShowFooter = True
        '
        'tabComparativoERPWMS
        '
        Me.tabComparativoERPWMS.Controls.Add(Me.chkLoteVence)
        Me.tabComparativoERPWMS.Controls.Add(Me.chkConUbicacion)
        Me.tabComparativoERPWMS.Controls.Add(Me.dgridcomparativoerpwms)
        Me.tabComparativoERPWMS.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tabComparativoERPWMS.Name = "tabComparativoERPWMS"
        Me.tabComparativoERPWMS.Size = New System.Drawing.Size(1936, 632)
        Me.tabComparativoERPWMS.Text = "Comparativo ERP vrs WMS"
        '
        'chkLoteVence
        '
        Me.chkLoteVence.Location = New System.Drawing.Point(853, 12)
        Me.chkLoteVence.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkLoteVence.MenuManager = Me.RibbonControl
        Me.chkLoteVence.Name = "chkLoteVence"
        Me.chkLoteVence.Properties.OffText = "Sin lotes y vencimientos"
        Me.chkLoteVence.Properties.OnText = "Con lotes y vencimientos"
        Me.chkLoteVence.Size = New System.Drawing.Size(222, 24)
        Me.chkLoteVence.TabIndex = 3
        '
        'chkConUbicacion
        '
        Me.chkConUbicacion.Location = New System.Drawing.Point(657, 12)
        Me.chkConUbicacion.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkConUbicacion.MenuManager = Me.RibbonControl
        Me.chkConUbicacion.Name = "chkConUbicacion"
        Me.chkConUbicacion.Properties.OffText = "Sin ubicación"
        Me.chkConUbicacion.Properties.OnText = "Con ubicación"
        Me.chkConUbicacion.Size = New System.Drawing.Size(164, 24)
        Me.chkConUbicacion.TabIndex = 2
        '
        'dgridcomparativoerpwms
        '
        Me.dgridcomparativoerpwms.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridcomparativoerpwms.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dgridcomparativoerpwms.Location = New System.Drawing.Point(0, 0)
        Me.dgridcomparativoerpwms.MainView = Me.gvInvTeoricoERP
        Me.dgridcomparativoerpwms.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dgridcomparativoerpwms.MenuManager = Me.RibbonControl
        Me.dgridcomparativoerpwms.Name = "dgridcomparativoerpwms"
        Me.dgridcomparativoerpwms.Size = New System.Drawing.Size(1936, 632)
        Me.dgridcomparativoerpwms.TabIndex = 1
        Me.dgridcomparativoerpwms.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvInvTeoricoERP})
        '
        'gvInvTeoricoERP
        '
        Me.gvInvTeoricoERP.DetailHeight = 437
        Me.gvInvTeoricoERP.GridControl = Me.dgridcomparativoerpwms
        Me.gvInvTeoricoERP.Name = "gvInvTeoricoERP"
        Me.gvInvTeoricoERP.OptionsBehavior.Editable = False
        Me.gvInvTeoricoERP.OptionsEditForm.PopupEditFormWidth = 1000
        Me.gvInvTeoricoERP.OptionsFind.AlwaysVisible = True
        Me.gvInvTeoricoERP.OptionsSelection.MultiSelect = True
        Me.gvInvTeoricoERP.OptionsView.ColumnAutoWidth = False
        Me.gvInvTeoricoERP.OptionsView.HeaderFilterButtonShowMode = DevExpress.XtraEditors.Controls.FilterButtonShowMode.Button
        Me.gvInvTeoricoERP.OptionsView.ShowAutoFilterRow = True
        Me.gvInvTeoricoERP.OptionsView.ShowFooter = True
        '
        'tbne
        '
        Me.tbne.Controls.Add(Me.PanelControl3)
        Me.tbne.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.tbne.Name = "tbne"
        Me.tbne.Size = New System.Drawing.Size(1936, 632)
        Me.tbne.Text = "Detalle de no existentes"
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.grdNE)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl3.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl3.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(1936, 632)
        Me.PanelControl3.TabIndex = 0
        '
        'grdNE
        '
        Me.grdNE.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdNE.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grdNE.Location = New System.Drawing.Point(2, 2)
        Me.grdNE.MainView = Me.GridView10
        Me.grdNE.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grdNE.MenuManager = Me.RibbonControl
        Me.grdNE.Name = "grdNE"
        Me.grdNE.Size = New System.Drawing.Size(1932, 628)
        Me.grdNE.TabIndex = 0
        Me.grdNE.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView10})
        '
        'GridView10
        '
        Me.GridView10.DetailHeight = 437
        Me.GridView10.GridControl = Me.grdNE
        Me.GridView10.Name = "GridView10"
        Me.GridView10.OptionsBehavior.ReadOnly = True
        Me.GridView10.OptionsEditForm.PopupEditFormWidth = 1000
        Me.GridView10.OptionsFind.AlwaysVisible = True
        Me.GridView10.OptionsView.ColumnAutoWidth = False
        Me.GridView10.OptionsView.ShowAutoFilterRow = True
        Me.GridView10.OptionsView.ShowFooter = True
        '
        'tabInvCongelado
        '
        Me.tabInvCongelado.Controls.Add(Me.PanelControl1)
        Me.tabInvCongelado.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.tabInvCongelado.Name = "tabInvCongelado"
        Me.tabInvCongelado.Size = New System.Drawing.Size(1936, 632)
        Me.tabInvCongelado.Text = "Inv. Congelado"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.dgridCongelado)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1936, 632)
        Me.PanelControl1.TabIndex = 0
        '
        'dgridCongelado
        '
        Me.dgridCongelado.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridCongelado.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dgridCongelado.Location = New System.Drawing.Point(2, 2)
        Me.dgridCongelado.MainView = Me.GridView9
        Me.dgridCongelado.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dgridCongelado.MenuManager = Me.RibbonControl
        Me.dgridCongelado.Name = "dgridCongelado"
        Me.dgridCongelado.Size = New System.Drawing.Size(1932, 628)
        Me.dgridCongelado.TabIndex = 0
        Me.dgridCongelado.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView9})
        '
        'GridView9
        '
        Me.GridView9.DetailHeight = 437
        Me.GridView9.GridControl = Me.dgridCongelado
        Me.GridView9.Name = "GridView9"
        Me.GridView9.OptionsBehavior.ReadOnly = True
        Me.GridView9.OptionsEditForm.PopupEditFormWidth = 1000
        Me.GridView9.OptionsFind.AlwaysVisible = True
        Me.GridView9.OptionsView.ColumnAutoWidth = False
        Me.GridView9.OptionsView.ShowAutoFilterRow = True
        Me.GridView9.OptionsView.ShowFooter = True
        '
        'TabInventarioCostos
        '
        Me.TabInventarioCostos.Controls.Add(Me.PanelControl2)
        Me.TabInventarioCostos.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.TabInventarioCostos.Name = "TabInventarioCostos"
        Me.TabInventarioCostos.Size = New System.Drawing.Size(1936, 632)
        Me.TabInventarioCostos.Text = "Comparativo por valorización"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.grdCostos)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(1936, 632)
        Me.PanelControl2.TabIndex = 0
        '
        'grdCostos
        '
        Me.grdCostos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdCostos.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grdCostos.Location = New System.Drawing.Point(2, 2)
        Me.grdCostos.MainView = Me.GridView7
        Me.grdCostos.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grdCostos.MenuManager = Me.RibbonControl
        Me.grdCostos.Name = "grdCostos"
        Me.grdCostos.Size = New System.Drawing.Size(1932, 628)
        Me.grdCostos.TabIndex = 0
        Me.grdCostos.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView7})
        '
        'GridView7
        '
        Me.GridView7.DetailHeight = 437
        Me.GridView7.GridControl = Me.grdCostos
        Me.GridView7.Name = "GridView7"
        Me.GridView7.OptionsBehavior.Editable = False
        Me.GridView7.OptionsBehavior.ReadOnly = True
        Me.GridView7.OptionsEditForm.PopupEditFormWidth = 1000
        Me.GridView7.OptionsFind.AlwaysVisible = True
        Me.GridView7.OptionsView.ShowAutoFilterRow = True
        '
        'tabKPI
        '
        Me.tabKPI.Controls.Add(Me.SplitContainerControl1)
        Me.tabKPI.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.tabKPI.Name = "tabKPI"
        Me.tabKPI.Size = New System.Drawing.Size(1936, 632)
        Me.tabKPI.Text = "KPI"
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        '
        'SplitContainerControl1.Panel1
        '
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.charcUniverso)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.SplitContainerControl2)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1936, 632)
        Me.SplitContainerControl1.SplitterPosition = 569
        Me.SplitContainerControl1.TabIndex = 4
        '
        'charcUniverso
        '
        Me.charcUniverso.Dock = System.Windows.Forms.DockStyle.Fill
        Me.charcUniverso.Legend.Name = "Default Legend"
        Me.charcUniverso.Location = New System.Drawing.Point(0, 0)
        Me.charcUniverso.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.charcUniverso.Name = "charcUniverso"
        Me.charcUniverso.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.charcUniverso.SeriesTemplate.View = PieSeriesView1
        Me.charcUniverso.Size = New System.Drawing.Size(569, 632)
        Me.charcUniverso.TabIndex = 1
        '
        'SplitContainerControl2
        '
        Me.SplitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl2.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.SplitContainerControl2.Name = "SplitContainerControl2"
        '
        'SplitContainerControl2.Panel1
        '
        Me.SplitContainerControl2.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainerControl2.Panel1.Text = "Panel1"
        '
        'SplitContainerControl2.Panel2
        '
        Me.SplitContainerControl2.Panel2.Controls.Add(Me.chartcEstratoTipo)
        Me.SplitContainerControl2.Panel2.Text = "Panel2"
        Me.SplitContainerControl2.Size = New System.Drawing.Size(1355, 632)
        Me.SplitContainerControl2.SplitterPosition = 618
        Me.SplitContainerControl2.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblRegsCont)
        Me.GroupBox1.Controls.Add(Me.gcRegistros)
        Me.GroupBox1.Controls.Add(Me.lblRegistrosContados)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(618, 632)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'lblRegsCont
        '
        Me.lblRegsCont.Appearance.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegsCont.Appearance.Options.UseFont = True
        Me.lblRegsCont.Appearance.Options.UseTextOptions = True
        Me.lblRegsCont.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblRegsCont.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblRegsCont.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.lblRegsCont.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblRegsCont.Location = New System.Drawing.Point(5, 604)
        Me.lblRegsCont.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.lblRegsCont.Name = "lblRegsCont"
        Me.lblRegsCont.Size = New System.Drawing.Size(608, 438)
        Me.lblRegsCont.TabIndex = 2
        Me.lblRegsCont.Text = "0/0"
        '
        'gcRegistros
        '
        Me.gcRegistros.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.gcRegistros.Dock = System.Windows.Forms.DockStyle.Top
        Me.gcRegistros.Gauges.AddRange(New DevExpress.XtraGauges.Base.IGauge() {Me.circularGauge1})
        Me.gcRegistros.LayoutInterval = 7
        Me.gcRegistros.LayoutPadding = New DevExpress.XtraGauges.Core.Base.Thickness(7)
        Me.gcRegistros.Location = New System.Drawing.Point(5, 291)
        Me.gcRegistros.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.gcRegistros.Name = "gcRegistros"
        Me.gcRegistros.Size = New System.Drawing.Size(608, 313)
        Me.gcRegistros.TabIndex = 1
        '
        'circularGauge1
        '
        Me.circularGauge1.BackgroundLayers.AddRange(New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent() {Me.ArcScaleBackgroundLayerComponent1})
        Me.circularGauge1.Bounds = New System.Drawing.Rectangle(7, 7, 594, 299)
        Me.circularGauge1.Name = "circularGauge1"
        Me.circularGauge1.Needles.AddRange(New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent() {Me.ArcScaleNeedleComponent1})
        Me.circularGauge1.Scales.AddRange(New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent() {Me.ArcScaleComponent1})
        Me.circularGauge1.SpindleCaps.AddRange(New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent() {Me.ArcScaleSpindleCapComponent1})
        '
        'ArcScaleBackgroundLayerComponent1
        '
        Me.ArcScaleBackgroundLayerComponent1.ArcScale = Me.ArcScaleComponent1
        Me.ArcScaleBackgroundLayerComponent1.Name = "bg"
        Me.ArcScaleBackgroundLayerComponent1.ScaleCenterPos = New DevExpress.XtraGauges.Core.Base.PointF2D(0.5!, 0.695!)
        Me.ArcScaleBackgroundLayerComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.CircularHalf_Style16
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
        Me.ArcScaleComponent1.AppearanceTickmarkText.TextBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#484E5A")
        Me.ArcScaleComponent1.Center = New DevExpress.XtraGauges.Core.Base.PointF2D(125.0!, 165.0!)
        Me.ArcScaleComponent1.EndAngle = 0!
        Me.ArcScaleComponent1.MajorTickCount = 6
        Me.ArcScaleComponent1.MajorTickmark.FormatString = "{0:F0}"
        Me.ArcScaleComponent1.MajorTickmark.ShapeOffset = -13.0!
        Me.ArcScaleComponent1.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_1
        Me.ArcScaleComponent1.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight
        Me.ArcScaleComponent1.MaxValue = 100.0!
        Me.ArcScaleComponent1.MinorTickCount = 4
        Me.ArcScaleComponent1.MinorTickmark.ShapeOffset = -9.0!
        Me.ArcScaleComponent1.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_2
        Me.ArcScaleComponent1.Name = "scale1"
        Me.ArcScaleComponent1.RadiusX = 98.0!
        Me.ArcScaleComponent1.RadiusY = 98.0!
        ArcScaleRange1.AppearanceRange.ContentBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#9EC968")
        ArcScaleRange1.EndThickness = 14.0!
        ArcScaleRange1.EndValue = 33.0!
        ArcScaleRange1.Name = "Range0"
        ArcScaleRange1.ShapeOffset = 0!
        ArcScaleRange1.StartThickness = 14.0!
        ArcScaleRange2.AppearanceRange.ContentBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#FED96D")
        ArcScaleRange2.EndThickness = 14.0!
        ArcScaleRange2.EndValue = 66.0!
        ArcScaleRange2.Name = "Range1"
        ArcScaleRange2.ShapeOffset = 0!
        ArcScaleRange2.StartThickness = 14.0!
        ArcScaleRange2.StartValue = 33.0!
        ArcScaleRange3.AppearanceRange.ContentBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#EF8C75")
        ArcScaleRange3.EndThickness = 14.0!
        ArcScaleRange3.EndValue = 100.0!
        ArcScaleRange3.Name = "Range2"
        ArcScaleRange3.ShapeOffset = 0!
        ArcScaleRange3.StartThickness = 14.0!
        ArcScaleRange3.StartValue = 66.0!
        Me.ArcScaleComponent1.Ranges.AddRange(New DevExpress.XtraGauges.Core.Model.IRange() {ArcScaleRange1, ArcScaleRange2, ArcScaleRange3})
        Me.ArcScaleComponent1.StartAngle = -180.0!
        Me.ArcScaleComponent1.Value = 22.0!
        '
        'ArcScaleNeedleComponent1
        '
        Me.ArcScaleNeedleComponent1.ArcScale = Me.ArcScaleComponent1
        Me.ArcScaleNeedleComponent1.EndOffset = 3.0!
        Me.ArcScaleNeedleComponent1.Name = "needle"
        Me.ArcScaleNeedleComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.NeedleShapeType.CircularFull_Style16
        Me.ArcScaleNeedleComponent1.ZOrder = -50
        '
        'ArcScaleSpindleCapComponent1
        '
        Me.ArcScaleSpindleCapComponent1.ArcScale = Me.ArcScaleComponent1
        Me.ArcScaleSpindleCapComponent1.Name = "circularGauge1_SpindleCap1"
        Me.ArcScaleSpindleCapComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.SpindleCapShapeType.CircularFull_Style16
        Me.ArcScaleSpindleCapComponent1.Size = New System.Drawing.SizeF(25.0!, 25.0!)
        Me.ArcScaleSpindleCapComponent1.ZOrder = -100
        '
        'lblRegistrosContados
        '
        Me.lblRegistrosContados.Appearance.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegistrosContados.Appearance.Options.UseFont = True
        Me.lblRegistrosContados.Appearance.Options.UseTextOptions = True
        Me.lblRegistrosContados.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblRegistrosContados.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblRegistrosContados.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblRegistrosContados.Location = New System.Drawing.Point(5, 21)
        Me.lblRegistrosContados.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.lblRegistrosContados.Name = "lblRegistrosContados"
        Me.lblRegistrosContados.Size = New System.Drawing.Size(608, 270)
        Me.lblRegistrosContados.TabIndex = 0
        Me.lblRegistrosContados.Text = "% Registros contados"
        '
        'chartcEstratoTipo
        '
        Me.chartcEstratoTipo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartcEstratoTipo.Legend.Name = "Default Legend"
        Me.chartcEstratoTipo.Location = New System.Drawing.Point(0, 0)
        Me.chartcEstratoTipo.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.chartcEstratoTipo.Name = "chartcEstratoTipo"
        Me.chartcEstratoTipo.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.chartcEstratoTipo.SeriesTemplate.View = PieSeriesView2
        Me.chartcEstratoTipo.Size = New System.Drawing.Size(725, 632)
        Me.chartcEstratoTipo.TabIndex = 4
        '
        'tabAsignacionUbicaciones
        '
        Me.tabAsignacionUbicaciones.Controls.Add(Me.grpUbicaciones)
        Me.tabAsignacionUbicaciones.Controls.Add(Me.grp)
        Me.tabAsignacionUbicaciones.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.tabAsignacionUbicaciones.Name = "tabAsignacionUbicaciones"
        Me.tabAsignacionUbicaciones.PageVisible = False
        Me.tabAsignacionUbicaciones.Size = New System.Drawing.Size(1936, 632)
        Me.tabAsignacionUbicaciones.Text = "Asignación de Ubicaciones"
        '
        'grpUbicaciones
        '
        Me.grpUbicaciones.Controls.Add(Me.GroupControl1)
        Me.grpUbicaciones.Controls.Add(Me.grdUbicaciones)
        Me.grpUbicaciones.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpUbicaciones.Location = New System.Drawing.Point(0, 94)
        Me.grpUbicaciones.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grpUbicaciones.Name = "grpUbicaciones"
        Me.grpUbicaciones.ShowCaption = False
        Me.grpUbicaciones.Size = New System.Drawing.Size(1936, 538)
        Me.grpUbicaciones.TabIndex = 1
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.dgridAsignacionUbicaciones)
        Me.GroupControl1.Controls.Add(Me.btnFiltLimpia)
        Me.GroupControl1.Controls.Add(Me.txtFiltroUbic)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(2, 2)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1932, 534)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Bodega -> Area -> Sector -> Tramo -> Ubicación"
        '
        'dgridAsignacionUbicaciones
        '
        Me.dgridAsignacionUbicaciones.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridAsignacionUbicaciones.Location = New System.Drawing.Point(2, 58)
        Me.dgridAsignacionUbicaciones.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dgridAsignacionUbicaciones.Name = "dgridAsignacionUbicaciones"
        Me.dgridAsignacionUbicaciones.OptionsBehavior.AllowBoundCheckBoxesInVirtualMode = True
        Me.dgridAsignacionUbicaciones.OptionsBehavior.Editable = False
        Me.dgridAsignacionUbicaciones.OptionsBehavior.ReadOnly = True
        Me.dgridAsignacionUbicaciones.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.dgridAsignacionUbicaciones.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check
        Me.dgridAsignacionUbicaciones.OptionsView.ShowAutoFilterRow = True
        Me.dgridAsignacionUbicaciones.OptionsView.ShowFilterPanelMode = DevExpress.XtraTreeList.ShowFilterPanelMode.ShowAlways
        Me.dgridAsignacionUbicaciones.RowHeight = 30
        Me.dgridAsignacionUbicaciones.Size = New System.Drawing.Size(1928, 474)
        Me.dgridAsignacionUbicaciones.TabIndex = 2
        Me.dgridAsignacionUbicaciones.TreeLevelWidth = 17
        '
        'btnFiltLimpia
        '
        Me.btnFiltLimpia.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFiltLimpia.ImageOptions.Image = CType(resources.GetObject("btnFiltLimpia.ImageOptions.Image"), System.Drawing.Image)
        Me.btnFiltLimpia.Location = New System.Drawing.Point(1887, 34)
        Me.btnFiltLimpia.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.btnFiltLimpia.Name = "btnFiltLimpia"
        Me.btnFiltLimpia.Size = New System.Drawing.Size(36, 30)
        Me.btnFiltLimpia.TabIndex = 1
        Me.btnFiltLimpia.Visible = False
        '
        'txtFiltroUbic
        '
        Me.txtFiltroUbic.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.[True]
        Me.txtFiltroUbic.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtFiltroUbic.Location = New System.Drawing.Point(2, 28)
        Me.txtFiltroUbic.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtFiltroUbic.MenuManager = Me.RibbonControl
        Me.txtFiltroUbic.Name = "txtFiltroUbic"
        Me.txtFiltroUbic.Properties.Appearance.BackColor = System.Drawing.Color.AliceBlue
        Me.txtFiltroUbic.Properties.Appearance.Font = New System.Drawing.Font("Consolas", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFiltroUbic.Properties.Appearance.Options.UseBackColor = True
        Me.txtFiltroUbic.Properties.Appearance.Options.UseFont = True
        Me.txtFiltroUbic.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat
        Me.txtFiltroUbic.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFiltroUbic.Size = New System.Drawing.Size(1928, 30)
        Me.txtFiltroUbic.TabIndex = 0
        Me.txtFiltroUbic.ToolTip = resources.GetString("txtFiltroUbic.ToolTip")
        Me.txtFiltroUbic.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        Me.txtFiltroUbic.ToolTipTitle = "Filtro de búsqueda"
        Me.txtFiltroUbic.Visible = False
        '
        'grdUbicaciones
        '
        Me.grdUbicaciones.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grdUbicaciones.Location = New System.Drawing.Point(968, 48)
        Me.grdUbicaciones.MainView = Me.GridView6
        Me.grdUbicaciones.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grdUbicaciones.MenuManager = Me.RibbonControl
        Me.grdUbicaciones.Name = "grdUbicaciones"
        Me.grdUbicaciones.Size = New System.Drawing.Size(396, 375)
        Me.grdUbicaciones.TabIndex = 1
        Me.grdUbicaciones.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView6})
        '
        'GridView6
        '
        Me.GridView6.DetailHeight = 437
        Me.GridView6.GridControl = Me.grdUbicaciones
        Me.GridView6.Name = "GridView6"
        Me.GridView6.OptionsEditForm.PopupEditFormWidth = 1000
        Me.GridView6.OptionsFind.AlwaysVisible = True
        Me.GridView6.OptionsView.ShowGroupPanel = False
        '
        'grp
        '
        Me.grp.Dock = System.Windows.Forms.DockStyle.Top
        Me.grp.Location = New System.Drawing.Point(0, 0)
        Me.grp.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.grp.Name = "grp"
        Me.grp.ShowCaption = False
        Me.grp.Size = New System.Drawing.Size(1936, 94)
        Me.grp.TabIndex = 0
        '
        'xtpRegularizacion
        '
        Me.xtpRegularizacion.Controls.Add(Me.grdRegularizar)
        Me.xtpRegularizacion.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.xtpRegularizacion.Name = "xtpRegularizacion"
        Me.xtpRegularizacion.Size = New System.Drawing.Size(1936, 632)
        Me.xtpRegularizacion.Text = "Regularización"
        '
        'grdRegularizar
        '
        Me.grdRegularizar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdRegularizar.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grdRegularizar.Location = New System.Drawing.Point(0, 0)
        Me.grdRegularizar.MainView = Me.GridView1
        Me.grdRegularizar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grdRegularizar.MenuManager = Me.RibbonControl
        Me.grdRegularizar.Name = "grdRegularizar"
        Me.grdRegularizar.Size = New System.Drawing.Size(1936, 632)
        Me.grdRegularizar.TabIndex = 3
        Me.grdRegularizar.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.grdRegularizar
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'DetalleBindingSource
        '
        Me.DetalleBindingSource.DataMember = "Detalle"
        Me.DetalleBindingSource.DataSource = Me.DSReconteoBindingSource
        '
        'bwKPI
        '
        '
        'Timer1
        '
        Me.Timer1.Interval = 10000
        '
        'WorkspaceManager1
        '
        Me.WorkspaceManager1.TargetControl = Me
        Me.WorkspaceManager1.TransitionType = PushTransition1
        '
        'frmInventario
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1938, 882)
        Me.Controls.Add(Me.xtraTabInv)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmInventario"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar1
        Me.Text = "Inventario"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSector1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabDetalle.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.grpConteo.ResumeLayout(False)
        CType(Me.grdConteo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gviewConteo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpVerificac.ResumeLayout(False)
        CType(Me.grdVerifica, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gviewVerifica, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpComparacion.ResumeLayout(False)
        CType(Me.dgridComparativoInvInicial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gviewComparativo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpFiltros, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFiltros.ResumeLayout(False)
        Me.grpFiltros.PerformLayout()
        CType(Me.txtIdUbicacionInvInicial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkUbicCompleta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Datos.ResumeLayout(False)
        CType(Me.grpInven, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpInven.ResumeLayout(False)
        Me.grpInven.PerformLayout()
        CType(Me.chkCapturarNoAsignado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCentroCosto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMultiPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCaptNtExist.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCliente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbProductoFamilia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMostrarCantidad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCambiaUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpUltimoInv.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpUltimoInv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSistema.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpHoraFin.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpHoraFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpHoraInicio.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpHoraInicio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTipoConteo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTipoInventario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDobleVerifica.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fecha.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fecha.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtraTabInv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtraTabInv.ResumeLayout(False)
        Me.tabAsignacionProductos.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.dgridAsignacionProductos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpProductos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpProductos.ResumeLayout(False)
        Me.grpProductos.PerformLayout()
        CType(Me.twTodos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbOperadorProd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Tramos.ResumeLayout(False)
        CType(Me.grpTramos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTramos.ResumeLayout(False)
        Me.grpTramos.PerformLayout()
        CType(Me.dgridAsignacionTramos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewTramos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabAsignacionOperadores.ResumeLayout(False)
        CType(Me.dgridAsignacionOperadores, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpOperadores, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpOperadores.ResumeLayout(False)
        Me.grpOperadores.PerformLayout()
        CType(Me.cmbOperador.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabConteo.ResumeLayout(False)
        CType(Me.dgridInventarioCiclico, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdviewTeorico, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpConteoCi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpConteoCi.ResumeLayout(False)
        Me.grpConteoCi.PerformLayout()
        CType(Me.txtNombreUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreOperador.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdOperador.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreTramo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdTramo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtClasificacionNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdClasificacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFamiliaNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdFamilia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabDiferenciasInventario.ResumeLayout(False)
        CType(Me.dgridDiferenciasCiclico, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDiferenciasCiclico, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabConteoOperador.ResumeLayout(False)
        CType(Me.dgridConteoOperador, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvConteoOperador, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabReconteo.ResumeLayout(False)
        CType(Me.grpReconteo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpReconteo.ResumeLayout(False)
        CType(Me.grdReconteo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EncabezadoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DSReconteoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DSReconteo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabInvTeorico.ResumeLayout(False)
        CType(Me.dgridInvTeorico, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvInvTeoricoWMS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabComparativoERPWMS.ResumeLayout(False)
        CType(Me.chkLoteVence.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkConUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgridcomparativoerpwms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvInvTeoricoERP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbne.ResumeLayout(False)
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        CType(Me.grdNE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabInvCongelado.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.dgridCongelado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabInventarioCostos.ResumeLayout(False)
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.grdCostos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabKPI.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(PieSeriesView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.charcUniverso, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitContainerControl2.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl2.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.circularGauge1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleBackgroundLayerComponent1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleComponent1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleNeedleComponent1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleSpindleCapComponent1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(PieSeriesView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartcEstratoTipo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabAsignacionUbicaciones.ResumeLayout(False)
        CType(Me.grpUbicaciones, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpUbicaciones.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.dgridAsignacionUbicaciones, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFiltroUbic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdUbicaciones, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtpRegularizacion.ResumeLayout(False)
        CType(Me.grdRegularizar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DetalleBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents cmdActualizarInvInicial As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents xtraTabInv As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents Datos As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grpInven As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbTipoConteo As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbTipoInventario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblFin As Label
    Friend WithEvents lblInicio As Label
    Friend WithEvents lblActivo As Label
    Friend WithEvents chkDobleVerifica As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblDobleVerif As Label
    Friend WithEvents lblTipoConteo As Label
    Friend WithEvents lblTipoInventario As Label
    Friend WithEvents lblFecha As Label
    Friend WithEvents Fecha As DevExpress.XtraEditors.DateEdit
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Estado As Label
    Friend WithEvents lblCod As Label
    Friend WithEvents lblPropietario As Label
    Friend WithEvents lblBodega As Label
    Friend WithEvents lblEstado As Label
    Friend WithEvents lblCodigo As Label
    Friend WithEvents tabDetalle As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents grdConteo As DevExpress.XtraGrid.GridControl
    Friend WithEvents gviewConteo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdVerifica As DevExpress.XtraGrid.GridControl
    Friend WithEvents gviewVerifica As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents dgridComparativoInvInicial As DevExpress.XtraGrid.GridControl
    Friend WithEvents gviewComparativo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BarCheckItem1 As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents cmbSector As DevExpress.XtraBars.BarEditItem
    Friend WithEvents cmbSector1 As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents txtTramoNombre As TextBox
    Friend WithEvents txtProductoNombre As TextBox
    Friend WithEvents grpFiltros As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtPropietarioNombre As TextBox
    Friend WithEvents linkTramo As LinkLabel
    Friend WithEvents linkPropietario As LinkLabel
    Friend WithEvents linkProducto As LinkLabel
    Friend WithEvents grpConteo As GroupBox
    Friend WithEvents grpVerificac As GroupBox
    Friend WithEvents grpComparacion As GroupBox
    Friend WithEvents txtTramoId As TextBox
    Friend WithEvents txtProductoId As TextBox
    Friend WithEvents txtPropietarioId As TextBox
    Friend WithEvents btnLimpiar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Tramos As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grpTramos As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dgridAsignacionTramos As DevExpress.XtraGrid.GridControl
    Private WithEvents GridViewTramos As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colSeleccionar As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents colIdTramos As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents coldIdSector As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents coldTramo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents chkTramosAsig As CheckBox
    Friend WithEvents chkSeleccionarTodos As CheckBox
    Friend WithEvents rgrp As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rgprImportar As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmdCompracionStock As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rpgComparacion As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmdConvertir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rgrpRegularizar As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents dtpHoraFin As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtpHoraInicio As DevExpress.XtraEditors.DateEdit
    Friend WithEvents tabAsignacionUbicaciones As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grpUbicaciones As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grp As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmdQuitar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmdAgregar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grdUbicaciones As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtFiltroUbic As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnFiltLimpia As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tabAsignacionOperadores As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grpOperadores As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dgridAsignacionOperadores As DevExpress.XtraTreeList.TreeList
    Friend WithEvents cmbOperador As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblOperador As Label
    Friend WithEvents cmdQuitarOperador As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmdAsignarOperador As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tabAsignacionProductos As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grpProductos As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmdAgregarProducto As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmdQuitarProducto As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents dgridAsignacionProductos As DevExpress.XtraTreeList.TreeList
    Friend WithEvents cmdAsignarOp As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmdEliminarOpProd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tabConteo As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grpConteoCi As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dgridInventarioCiclico As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdviewTeorico As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtNombreTramo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdTramo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtClasificacionNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdClasificacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtFamiliaNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdFamilia As DevExpress.XtraEditors.TextEdit
    Friend WithEvents linklblTramo As LinkLabel
    Friend WithEvents linklblClasificacion As LinkLabel
    Friend WithEvents linklblFamilia As LinkLabel
    Friend WithEvents txtNombreOperador As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdOperador As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LinklblOperador As LinkLabel
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents txtNombreProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LinklblProducto As LinkLabel
    Friend WithEvents txtIdProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdUbicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LinklblUbicacion As LinkLabel
    Friend WithEvents txtNombreUbicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents rpgReconteo As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmdReconteo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblReg As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents pgImprimir As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents lblRe As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents dgridAsignacionUbicaciones As DevExpress.XtraTreeList.TreeList
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarSubItem
    Friend WithEvents cmdImprimirGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimirdetalle As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimirporoperador As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblEsSistema As Label
    Friend WithEvents chkSistema As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents tabReconteo As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grdReconteo As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView8 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grpReconteo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblRegsRec As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RibbonStatusBar1 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdImprimirReconteo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdActualizarInventario As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grpRegularizarInvStock As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents EncabezadoBindingSource As BindingSource
    Friend WithEvents DSReconteoBindingSource As BindingSource
    Friend WithEvents DSReconteo As DSReconteo
    Friend WithEvents colIdReconteoEnc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colEstado As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colHora_Inicio As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colHora_Fin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdInventarioEnc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCorrelativo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DetalleBindingSource As BindingSource
    Friend WithEvents mnuImprimirInicial As DevExpress.XtraBars.BarSubItem
    Friend WithEvents grpImprimirInicial As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmdImprimirConteo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimirVerifi As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimirComparacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tabInvTeorico As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgridInvTeorico As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvInvTeoricoWMS As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkUbicCompleta As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents tabInvCongelado As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents dgridCongelado As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView9 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents mnuExportarExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pgExportar As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents lblPrg As Label
    Friend WithEvents prg As ProgressBar
    Friend WithEvents prgPanInvConteo As ProgressBar
    Friend WithEvents cmdAplicarAjustesFecha As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rpgAplicarAjustesFecha As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmbOperadorProd As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label1 As Label
    Friend WithEvents twTodos As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents lblUltInv As Label
    Friend WithEvents dtpUltimoInv As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblMostrarCantidad As Label
    Friend WithEvents chkMostrarCantidad As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkCambiaUbicacion As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblCambiaUbicacion As Label
    Friend WithEvents cmbProductoFamilia As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblSeccionAjuste As Label
    Friend WithEvents TabInventarioCostos As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents grdCostos As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView7 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmbCliente As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblCliente As Label
    Friend WithEvents tabKPI As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents bwKPI As System.ComponentModel.BackgroundWorker
    Friend WithEvents Timer1 As Timer
    Friend WithEvents chartcEstratoContadoPorTipo As DevExpress.XtraCharts.ChartControl
    Friend WithEvents VW_Inventario_prg_por_tipoTableAdapter1 As DSInventarioTableAdapters.VW_Inventario_prg_por_tipoTableAdapter
    Friend WithEvents DsInventario1 As DSInventario
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rpgKPI As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents chkCaptNtExist As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents tbne As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents grdNE As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView10 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkMultiPropietario As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Label3 As Label
    Friend WithEvents chkComparativoConUbicacion As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents cmbCentroCosto As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblCentroCosto As Label
    Friend WithEvents mnuImportarTeoricoERP As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tabComparativoERPWMS As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgridcomparativoerpwms As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvInvTeoricoERP As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmdAsignaOpProd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmdQuitaOpProd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents mnuImprimirComparacionERP As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lnkUbicacionInvInicial As LinkLabel
    Friend WithEvents txtIdUbicacionInvInicial As DevExpress.XtraEditors.TextEdit
    Friend WithEvents rpActualizar As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents WorkspaceManager1 As DevExpress.Utils.WorkspaceManager
    Friend WithEvents chkCapturarNoAsignado As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Label4 As Label
    Friend WithEvents tabConteoOperador As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgridConteoOperador As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvConteoOperador As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tabDiferenciasInventario As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgridDiferenciasCiclico As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDiferenciasCiclico As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblUbicacionesUnicas As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents lblProductosUnicos As TextBox
    Friend WithEvents lblRegistros As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents chkConUbicacion As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkLoteVence As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents SplitContainerControl2 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents charcUniverso As DevExpress.XtraCharts.ChartControl
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblRegsCont As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gcRegistros As DevExpress.XtraGauges.Win.GaugeControl
    Friend WithEvents circularGauge1 As DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge
    Private WithEvents ArcScaleBackgroundLayerComponent1 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent
    Private WithEvents ArcScaleComponent1 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent
    Private WithEvents ArcScaleNeedleComponent1 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent
    Private WithEvents ArcScaleSpindleCapComponent1 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent
    Friend WithEvents lblRegistrosContados As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chartcEstratoTipo As DevExpress.XtraCharts.ChartControl
    Friend WithEvents xtpRegularizacion As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grdRegularizar As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
End Class
