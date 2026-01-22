<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPicking
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If BePickingEnc IsNot Nothing Then
                BePickingEnc.Dispose()
                BePickingEnc = Nothing
            End If
            If DTOperadores IsNot Nothing Then
                DTOperadores.Dispose()
                DTOperadores = Nothing
            End If
            If DTStockRes IsNot Nothing Then
                DTStockRes.Dispose()
                DTStockRes = Nothing
            End If
            If ObjBePickingDet IsNot Nothing Then
                ObjBePickingDet.Dispose()
                ObjBePickingDet = Nothing
            End If
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
        Dim Label10 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim IdPropietarioLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label37 As System.Windows.Forms.Label
        Dim Label38 As System.Windows.Forms.Label
        Dim Label30 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim Label14 As System.Windows.Forms.Label
        Dim Label15 As System.Windows.Forms.Label
        Dim Label13 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPicking))
        Dim ArcScaleRange1 As DevExpress.XtraGauges.Core.Model.ArcScaleRange = New DevExpress.XtraGauges.Core.Model.ArcScaleRange()
        Dim ArcScaleRange2 As DevExpress.XtraGauges.Core.Model.ArcScaleRange = New DevExpress.XtraGauges.Core.Model.ArcScaleRange()
        Dim ArcScaleRange3 As DevExpress.XtraGauges.Core.Model.ArcScaleRange = New DevExpress.XtraGauges.Core.Model.ArcScaleRange()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridFormatRule1 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarSubItem()
        Me.cmdListaUbicacion = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdUbicRes = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdListaPedidos = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuProcesar = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.lblRegs1 = New DevExpress.XtraBars.BarStaticItem()
        Me.mnuPendientePicking = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegistros = New DevExpress.XtraBars.BarStaticItem()
        Me.chkActivo = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkverifica_auto = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkProcesarDesdeBOF = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkDetalleOperador = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkEmpaquePorTarima = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkEmpaqueAGranel = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.mnuPendientePacking = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizarPicking = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuDespachado = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuProcesarLinea = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdNoPickeado = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdNoVerificado = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdVerificarNuevamente = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRepartirOperadores = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuZonasPicking = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPorAtributo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuBalancear = New DevExpress.XtraBars.BarSubItem()
        Me.mnuBalanceoPorLineasPicking = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuBalanceoCantidadLineasPicking = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuInteligenciaArtificial = New DevExpress.XtraBars.BarButtonItem()
        Me.lnkAgregarPedido = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAsignacionAutomatica = New DevExpress.XtraBars.BarSubItem()
        Me.mnuEliminarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuVerificarPickeados = New DevExpress.XtraBars.BarButtonItem()
        Me.chkFotografiaVerificacion = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.lnkQuitarPedido = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuReemplazo = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup4 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup5 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.RibbonStatusBar1 = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.RibbonStatusBar2 = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.xtpDatosPicking = New DevExpress.XtraTab.XtraTabControl()
        Me.XtratabPageDato = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl10 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl8 = New DevExpress.XtraEditors.GroupControl()
        Me.Fec_modDateEdit1 = New DevExpress.XtraEditors.DateEdit()
        Me.Fec_agrDateEdit1 = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit1 = New System.Windows.Forms.TextBox()
        Me.User_agrTextEdit1 = New System.Windows.Forms.TextBox()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.txtObservacion = New System.Windows.Forms.TextBox()
        Me.groupPrioridad = New System.Windows.Forms.GroupBox()
        Me.rbAlto = New System.Windows.Forms.RadioButton()
        Me.rbBajo = New System.Windows.Forms.RadioButton()
        Me.rbMedio = New System.Windows.Forms.RadioButton()
        Me.pbAlto = New System.Windows.Forms.PictureBox()
        Me.pbMedio = New System.Windows.Forms.PictureBox()
        Me.pbBajo = New System.Windows.Forms.PictureBox()
        Me.dtmFechaPicking = New DevExpress.XtraEditors.DateEdit()
        Me.cmbBodegas = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblEstado = New System.Windows.Forms.Label()
        Me.lblC = New System.Windows.Forms.Label()
        Me.GrpAsignacionTransaccion = New DevExpress.XtraEditors.GroupControl()
        Me.cmbMuelle = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView12 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtIdUbicacionMuelle = New DevExpress.XtraEditors.TextEdit()
        Me.txtReferencia = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreUbicacion = New DevExpress.XtraEditors.TextEdit()
        Me.lnkUbicacion = New System.Windows.Forms.LinkLabel()
        Me.txtIdUbicacion = New DevExpress.XtraEditors.TextEdit()
        Me.GrpTarea = New DevExpress.XtraEditors.GroupControl()
        Me.grpGaugueProgreso = New DevExpress.XtraEditors.GroupControl()
        Me.GaugeControl1 = New DevExpress.XtraGauges.Win.GaugeControl()
        Me.cgProgresoPicking = New DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge()
        Me.ArcScaleBackgroundLayerComponent1 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent()
        Me.ArcScaleComponent1 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent()
        Me.ArcScaleNeedleComponent1 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent()
        Me.ArcScaleSpindleCapComponent1 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent()
        Me.dtmFechaTarea = New DevExpress.XtraEditors.DateEdit()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.dtmHoraFhh = New System.Windows.Forms.DateTimePicker()
        Me.dtmHoraIhh = New System.Windows.Forms.DateTimePicker()
        Me.GroupControl9 = New DevExpress.XtraEditors.GroupControl()
        Me.dtmHoraF = New System.Windows.Forms.DateTimePicker()
        Me.dtmHoraI = New System.Windows.Forms.DateTimePicker()
        Me.XtratabPagePedido = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl()
        Me.dgridPedidos = New System.Windows.Forms.DataGridView()
        Me.IdPedido = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Referencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Bodega = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cliente = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Propietario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaPedido = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EstadoP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.cmbAgrupamiento = New System.Windows.Forms.ComboBox()
        Me.dgridDetallePicking = New System.Windows.Forms.DataGridView()
        Me.IdPedidoEnc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Codigo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Producto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Presentacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UnidadMedida = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Estado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ClienteDias = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CantidadRecibida = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OperadorBodega = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.IdPedidoDet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lnkVerParametro = New System.Windows.Forms.LinkLabel()
        Me.XtraTabPageUbicacionPicking = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl7 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.grdOperadorBodega = New DevExpress.XtraGrid.GridControl()
        Me.DataBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsOrdenCompraRecepcionOperador = New TOMWMS.DsOrdenCompraRecepcionOperador()
        Me.DgridOperadorBodega = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colIdOperadorBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSelección = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdOperadorRec = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colOperador = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colcolUsaHH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ToolStripPR = New System.Windows.Forms.ToolStrip()
        Me.cmdSavePR = New System.Windows.Forms.ToolStripButton()
        Me.cmdDesactivarPresentacion = New System.Windows.Forms.ToolStripButton()
        Me.dgridPickingUbic = New DevExpress.XtraGrid.GridControl()
        Me.grdvPickingUbic = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tbDañados = New DevExpress.XtraTab.XtraTabPage()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.grdProductosDañados = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xtpImpresionPedidos = New DevExpress.XtraTab.XtraTabPage()
        Me.grdImpresionPedidos = New DevExpress.XtraGrid.GridControl()
        Me.grdViewImpresionPedidos = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.dkPicking = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.BWDatosPicking = New System.ComponentModel.BackgroundWorker()
        Me.tmrActualizarDatosPicking = New System.Windows.Forms.Timer(Me.components)
        Me.behaviorManager1 = New DevExpress.Utils.Behaviors.BehaviorManager(Me.components)
        Me.OperadorDragDropEvent = New DevExpress.Utils.DragDrop.DragDropEvents(Me.components)
        Me.pickingUbicDragDropEvent = New DevExpress.Utils.DragDrop.DragDropEvents(Me.components)
        Me.ToastNotificationsManager1 = New DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager(Me.components)
        Label10 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        IdPropietarioLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label37 = New System.Windows.Forms.Label()
        Label38 = New System.Windows.Forms.Label()
        Label30 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        Label14 = New System.Windows.Forms.Label()
        Label15 = New System.Windows.Forms.Label()
        Label13 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtpDatosPicking, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtpDatosPicking.SuspendLayout()
        Me.XtratabPageDato.SuspendLayout()
        CType(Me.GroupControl10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl10.SuspendLayout()
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl8.SuspendLayout()
        CType(Me.Fec_modDateEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        Me.groupPrioridad.SuspendLayout()
        CType(Me.pbAlto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbMedio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbBajo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaPicking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaPicking.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodegas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpAsignacionTransaccion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpAsignacionTransaccion.SuspendLayout()
        CType(Me.cmbMuelle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUbicacionMuelle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReferencia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpTarea, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTarea.SuspendLayout()
        CType(Me.grpGaugueProgreso, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGaugueProgreso.SuspendLayout()
        CType(Me.cgProgresoPicking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleBackgroundLayerComponent1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleComponent1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleNeedleComponent1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleSpindleCapComponent1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaTarea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaTarea.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl9.SuspendLayout()
        Me.XtratabPagePedido.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6.SuspendLayout()
        CType(Me.dgridPedidos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.dgridDetallePicking, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPageUbicacionPicking.SuspendLayout()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl7.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.grdOperadorBodega, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsOrdenCompraRecepcionOperador, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgridOperadorBodega, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripPR.SuspendLayout()
        CType(Me.dgridPickingUbic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvPickingUbic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbDañados.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.grdProductosDañados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtpImpresionPedidos.SuspendLayout()
        CType(Me.grdImpresionPedidos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdViewImpresionPedidos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkPicking, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        CType(Me.behaviorManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToastNotificationsManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(24, 87)
        Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(45, 16)
        Label10.TabIndex = 2
        Label10.Text = "Estado"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(24, 55)
        Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(46, 16)
        Label12.TabIndex = 0
        Label12.Text = "Código"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(24, 119)
        Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(89, 16)
        Label2.TabIndex = 4
        Label2.Text = "Fecha Picking:"
        '
        'IdPropietarioLabel
        '
        IdPropietarioLabel.AutoSize = True
        IdPropietarioLabel.Location = New System.Drawing.Point(24, 151)
        IdPropietarioLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        IdPropietarioLabel.Name = "IdPropietarioLabel"
        IdPropietarioLabel.Size = New System.Drawing.Size(54, 16)
        IdPropietarioLabel.TabIndex = 6
        IdPropietarioLabel.Text = "Bodega:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(24, 185)
        Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(74, 16)
        Label1.TabIndex = 8
        Label1.Text = "Propietario:"
        '
        'Label37
        '
        Label37.AutoSize = True
        Label37.Location = New System.Drawing.Point(13, 82)
        Label37.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label37.Name = "Label37"
        Label37.Size = New System.Drawing.Size(60, 16)
        Label37.TabIndex = 2
        Label37.Text = "Hora Fin:"
        '
        'Label38
        '
        Label38.AutoSize = True
        Label38.Location = New System.Drawing.Point(10, 49)
        Label38.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label38.Name = "Label38"
        Label38.Size = New System.Drawing.Size(73, 16)
        Label38.TabIndex = 0
        Label38.Text = "Hora Inicio:"
        '
        'Label30
        '
        Label30.AutoSize = True
        Label30.Location = New System.Drawing.Point(10, 82)
        Label30.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label30.Name = "Label30"
        Label30.Size = New System.Drawing.Size(60, 16)
        Label30.TabIndex = 2
        Label30.Text = "Hora Fin:"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(10, 49)
        Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(73, 16)
        Label7.TabIndex = 0
        Label7.Text = "Hora Inicio:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(164, 37)
        Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(58, 16)
        Label4.TabIndex = 1
        Label4.Text = "Agrupar:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(551, 50)
        Fec_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(66, 18)
        User_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(100, 16)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(551, 18)
        User_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(66, 50)
        Fec_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'Label14
        '
        Label14.AutoSize = True
        Label14.Location = New System.Drawing.Point(500, 42)
        Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label14.Name = "Label14"
        Label14.Size = New System.Drawing.Size(106, 16)
        Label14.TabIndex = 10
        Label14.Text = "Usuario Modificó:"
        '
        'Label15
        '
        Label15.AutoSize = True
        Label15.Location = New System.Drawing.Point(500, 74)
        Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label15.Name = "Label15"
        Label15.Size = New System.Drawing.Size(97, 16)
        Label15.TabIndex = 14
        Label15.Text = "Fecha Modificó:"
        '
        'Label13
        '
        Label13.AutoSize = True
        Label13.Location = New System.Drawing.Point(16, 74)
        Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label13.Name = "Label13"
        Label13.Size = New System.Drawing.Size(91, 16)
        Label13.TabIndex = 12
        Label13.Text = "Fecha Agregó:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(16, 42)
        Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(100, 16)
        Label3.TabIndex = 8
        Label3.Text = "Usuario Agregó:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(531, 46)
        Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(73, 16)
        Label5.TabIndex = 9
        Label5.Text = "Referencia:"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(22, 78)
        Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(49, 16)
        Label6.TabIndex = 18
        Label6.Text = "Muelle:"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(24, 216)
        Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(82, 16)
        Label8.TabIndex = 73
        Label8.Text = "Observación:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.cmdImprimir, Me.cmdListaUbicacion, Me.mnuProcesar, Me.lblRegs, Me.lblRegs1, Me.mnuPendientePicking, Me.lblRegistros, Me.chkActivo, Me.chkverifica_auto, Me.chkProcesarDesdeBOF, Me.chkDetalleOperador, Me.chkEmpaquePorTarima, Me.chkEmpaqueAGranel, Me.mnuPendientePacking, Me.mnuActualizarPicking, Me.mnuDespachado, Me.mnuProcesarLinea, Me.cmdUbicRes, Me.cmdNoPickeado, Me.cmdNoVerificado, Me.cmdVerificarNuevamente, Me.BarButtonItem1, Me.mnuRepartirOperadores, Me.mnuZonasPicking, Me.mnuPorAtributo, Me.mnuBalancear, Me.mnuBalanceoPorLineasPicking, Me.mnuBalanceoCantidadLineasPicking, Me.mnuInteligenciaArtificial, Me.lnkAgregarPedido, Me.mnuAsignacionAutomatica, Me.mnuEliminarLayoutGrid, Me.mnuVerificarPickeados, Me.chkFotografiaVerificacion, Me.BarButtonItem2, Me.BarButtonItem3, Me.cmdListaPedidos, Me.lnkQuitarPedido, Me.mnuReemplazo})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonControl.MaxItemId = 59
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1554, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar2
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
        Me.mnuEliminar.Caption = "Anular"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 4
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdListaUbicacion), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdUbicRes), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdListaPedidos)})
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'cmdListaUbicacion
        '
        Me.cmdListaUbicacion.Caption = "Lista de Ubicaciones"
        Me.cmdListaUbicacion.Id = 5
        Me.cmdListaUbicacion.Name = "cmdListaUbicacion"
        '
        'cmdUbicRes
        '
        Me.cmdUbicRes.Caption = "Ubicaciones resumido"
        Me.cmdUbicRes.Id = 24
        Me.cmdUbicRes.Name = "cmdUbicRes"
        '
        'cmdListaPedidos
        '
        Me.cmdListaPedidos.Caption = "Lista de pedidos"
        Me.cmdListaPedidos.Id = 56
        Me.cmdListaPedidos.Name = "cmdListaPedidos"
        '
        'mnuProcesar
        '
        Me.mnuProcesar.Caption = "Procesar picking completo"
        Me.mnuProcesar.Id = 6
        Me.mnuProcesar.ImageOptions.SvgImage = CType(resources.GetObject("mnuProcesar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuProcesar.Name = "mnuProcesar"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros:"
        Me.lblRegs.Id = 7
        Me.lblRegs.Name = "lblRegs"
        '
        'lblRegs1
        '
        Me.lblRegs1.Caption = "Registros:"
        Me.lblRegs1.Id = 8
        Me.lblRegs1.Name = "lblRegs1"
        '
        'mnuPendientePicking
        '
        Me.mnuPendientePicking.Caption = "Cambiar a estado pendiente"
        Me.mnuPendientePicking.Id = 9
        Me.mnuPendientePicking.ImageOptions.SvgImage = CType(resources.GetObject("mnuPendientePicking.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuPendientePicking.Name = "mnuPendientePicking"
        '
        'lblRegistros
        '
        Me.lblRegistros.Caption = "Registros: 0"
        Me.lblRegistros.Id = 10
        Me.lblRegistros.Name = "lblRegistros"
        '
        'chkActivo
        '
        Me.chkActivo.BindableChecked = True
        Me.chkActivo.Caption = "Activo"
        Me.chkActivo.Checked = True
        Me.chkActivo.Enabled = False
        Me.chkActivo.Id = 11
        Me.chkActivo.Name = "chkActivo"
        '
        'chkverifica_auto
        '
        Me.chkverifica_auto.Caption = "Verificación Auto."
        Me.chkverifica_auto.Id = 13
        Me.chkverifica_auto.Name = "chkverifica_auto"
        '
        'chkProcesarDesdeBOF
        '
        Me.chkProcesarDesdeBOF.Caption = "Procesamiento BOF"
        Me.chkProcesarDesdeBOF.Id = 15
        Me.chkProcesarDesdeBOF.Name = "chkProcesarDesdeBOF"
        '
        'chkDetalleOperador
        '
        Me.chkDetalleOperador.Caption = "Por línea de Pedido"
        Me.chkDetalleOperador.Id = 16
        Me.chkDetalleOperador.Name = "chkDetalleOperador"
        '
        'chkEmpaquePorTarima
        '
        Me.chkEmpaquePorTarima.Caption = "Empaque por tarima"
        Me.chkEmpaquePorTarima.Id = 17
        Me.chkEmpaquePorTarima.Name = "chkEmpaquePorTarima"
        Me.chkEmpaquePorTarima.Tag = "Tarima"
        '
        'chkEmpaqueAGranel
        '
        Me.chkEmpaqueAGranel.Caption = "Empaque a Granel"
        Me.chkEmpaqueAGranel.Id = 18
        Me.chkEmpaqueAGranel.Name = "chkEmpaqueAGranel"
        Me.chkEmpaqueAGranel.Tag = "Granel"
        '
        'mnuPendientePacking
        '
        Me.mnuPendientePacking.Caption = "Pendiente de empaque"
        Me.mnuPendientePacking.Id = 19
        Me.mnuPendientePacking.ImageOptions.SvgImage = CType(resources.GetObject("mnuPendientePacking.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuPendientePacking.Name = "mnuPendientePacking"
        '
        'mnuActualizarPicking
        '
        Me.mnuActualizarPicking.Caption = "Actualizar lista de picking"
        Me.mnuActualizarPicking.Id = 21
        Me.mnuActualizarPicking.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizarPicking.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizarPicking.Name = "mnuActualizarPicking"
        '
        'mnuDespachado
        '
        Me.mnuDespachado.Caption = "Cambiar a estado Despachado"
        Me.mnuDespachado.Id = 22
        Me.mnuDespachado.ImageOptions.SvgImage = CType(resources.GetObject("mnuDespachado.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuDespachado.Name = "mnuDespachado"
        '
        'mnuProcesarLinea
        '
        Me.mnuProcesarLinea.Caption = "Procesar Línea"
        Me.mnuProcesarLinea.Id = 23
        Me.mnuProcesarLinea.ImageOptions.SvgImage = CType(resources.GetObject("mnuProcesarLinea.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuProcesarLinea.Name = "mnuProcesarLinea"
        '
        'cmdNoPickeado
        '
        Me.cmdNoPickeado.Caption = "Línea no pickeada"
        Me.cmdNoPickeado.Id = 25
        Me.cmdNoPickeado.ImageOptions.SvgImage = CType(resources.GetObject("cmdNoPickeado.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdNoPickeado.Name = "cmdNoPickeado"
        '
        'cmdNoVerificado
        '
        Me.cmdNoVerificado.Caption = "Línea no verificada"
        Me.cmdNoVerificado.Id = 26
        Me.cmdNoVerificado.ImageOptions.SvgImage = CType(resources.GetObject("cmdNoVerificado.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdNoVerificado.Name = "cmdNoVerificado"
        '
        'cmdVerificarNuevamente
        '
        Me.cmdVerificarNuevamente.Caption = "Verificar nuevamente"
        Me.cmdVerificarNuevamente.Id = 27
        Me.cmdVerificarNuevamente.ImageOptions.SvgImage = CType(resources.GetObject("cmdVerificarNuevamente.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdVerificarNuevamente.Name = "cmdVerificarNuevamente"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "BarButtonItem1"
        Me.BarButtonItem1.Id = 30
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'mnuRepartirOperadores
        '
        Me.mnuRepartirOperadores.Caption = "Repartir"
        Me.mnuRepartirOperadores.Id = 34
        Me.mnuRepartirOperadores.ImageOptions.SvgImage = CType(resources.GetObject("mnuRepartirOperadores.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuRepartirOperadores.Name = "mnuRepartirOperadores"
        '
        'mnuZonasPicking
        '
        Me.mnuZonasPicking.Caption = "Zonas de Picking"
        Me.mnuZonasPicking.Id = 36
        Me.mnuZonasPicking.ImageOptions.SvgImage = CType(resources.GetObject("mnuZonasPicking.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuZonasPicking.Name = "mnuZonasPicking"
        '
        'mnuPorAtributo
        '
        Me.mnuPorAtributo.Caption = "Atributo de producto"
        Me.mnuPorAtributo.Enabled = False
        Me.mnuPorAtributo.Id = 37
        Me.mnuPorAtributo.ImageOptions.SvgImage = CType(resources.GetObject("mnuPorAtributo.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuPorAtributo.Name = "mnuPorAtributo"
        '
        'mnuBalancear
        '
        Me.mnuBalancear.Caption = "Balancear"
        Me.mnuBalancear.Id = 40
        Me.mnuBalancear.ImageOptions.SvgImage = CType(resources.GetObject("mnuBalancear.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuBalancear.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuBalanceoPorLineasPicking), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuBalanceoCantidadLineasPicking), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem2), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem3)})
        Me.mnuBalancear.Name = "mnuBalancear"
        '
        'mnuBalanceoPorLineasPicking
        '
        Me.mnuBalanceoPorLineasPicking.Caption = "Por líneas de picking"
        Me.mnuBalanceoPorLineasPicking.Id = 41
        Me.mnuBalanceoPorLineasPicking.Name = "mnuBalanceoPorLineasPicking"
        '
        'mnuBalanceoCantidadLineasPicking
        '
        Me.mnuBalanceoCantidadLineasPicking.Caption = "Por cantidad en líneas de picking"
        Me.mnuBalanceoCantidadLineasPicking.Id = 42
        Me.mnuBalanceoCantidadLineasPicking.Name = "mnuBalanceoCantidadLineasPicking"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Por Zona Picking"
        Me.BarButtonItem2.Id = 54
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Por olas de picking (pedidos similares)"
        Me.BarButtonItem3.Id = 55
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'mnuInteligenciaArtificial
        '
        Me.mnuInteligenciaArtificial.Caption = "IA"
        Me.mnuInteligenciaArtificial.Enabled = False
        Me.mnuInteligenciaArtificial.Hint = "Inteligencia Artificial"
        Me.mnuInteligenciaArtificial.Id = 43
        Me.mnuInteligenciaArtificial.ImageOptions.Image = CType(resources.GetObject("mnuInteligenciaArtificial.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuInteligenciaArtificial.ImageOptions.LargeImage = CType(resources.GetObject("mnuInteligenciaArtificial.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuInteligenciaArtificial.Name = "mnuInteligenciaArtificial"
        '
        'lnkAgregarPedido
        '
        Me.lnkAgregarPedido.Caption = "Agregar Pedido"
        Me.lnkAgregarPedido.Id = 44
        Me.lnkAgregarPedido.ImageOptions.SvgImage = CType(resources.GetObject("lnkAgregarPedido.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.lnkAgregarPedido.Name = "lnkAgregarPedido"
        '
        'mnuAsignacionAutomatica
        '
        Me.mnuAsignacionAutomatica.Caption = "Asignar"
        Me.mnuAsignacionAutomatica.Id = 45
        Me.mnuAsignacionAutomatica.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuRepartirOperadores), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuZonasPicking), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuPorAtributo), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuBalancear), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuInteligenciaArtificial), New DevExpress.XtraBars.LinkPersistInfo(Me.chkDetalleOperador)})
        Me.mnuAsignacionAutomatica.Name = "mnuAsignacionAutomatica"
        '
        'mnuEliminarLayoutGrid
        '
        Me.mnuEliminarLayoutGrid.Caption = "Eliminar Layout Grid"
        Me.mnuEliminarLayoutGrid.Id = 48
        Me.mnuEliminarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminarLayoutGrid.Name = "mnuEliminarLayoutGrid"
        '
        'mnuVerificarPickeados
        '
        Me.mnuVerificarPickeados.Caption = "Verificar pickeados"
        Me.mnuVerificarPickeados.Id = 50
        Me.mnuVerificarPickeados.ImageOptions.SvgImage = CType(resources.GetObject("mnuVerificarPickeados.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuVerificarPickeados.Name = "mnuVerificarPickeados"
        '
        'chkFotografiaVerificacion
        '
        Me.chkFotografiaVerificacion.Caption = "Fotografía verificación"
        Me.chkFotografiaVerificacion.Id = 53
        Me.chkFotografiaVerificacion.Name = "chkFotografiaVerificacion"
        '
        'lnkQuitarPedido
        '
        Me.lnkQuitarPedido.Caption = "Quitar pedido"
        Me.lnkQuitarPedido.Id = 57
        Me.lnkQuitarPedido.ImageOptions.SvgImage = CType(resources.GetObject("lnkQuitarPedido.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.lnkQuitarPedido.Name = "lnkQuitarPedido"
        '
        'mnuReemplazo
        '
        Me.mnuReemplazo.Caption = "Reemplazo"
        Me.mnuReemplazo.Id = 58
        Me.mnuReemplazo.ImageOptions.SvgImage = CType(resources.GetObject("mnuReemplazo.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuReemplazo.Name = "mnuReemplazo"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup4, Me.RibbonPageGroup5, Me.RibbonPageGroup3, Me.RibbonPageGroup2})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Picking"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.lnkAgregarPedido)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuPendientePicking)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuPendientePacking)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.lnkQuitarPedido)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizarPicking)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup4
        '
        Me.RibbonPageGroup4.ItemLinks.Add(Me.cmdNoPickeado)
        Me.RibbonPageGroup4.ItemLinks.Add(Me.cmdNoVerificado)
        Me.RibbonPageGroup4.ItemLinks.Add(Me.cmdVerificarNuevamente)
        Me.RibbonPageGroup4.ItemLinks.Add(Me.mnuProcesarLinea)
        Me.RibbonPageGroup4.ItemLinks.Add(Me.mnuProcesar)
        Me.RibbonPageGroup4.ItemLinks.Add(Me.mnuVerificarPickeados)
        Me.RibbonPageGroup4.ItemLinks.Add(Me.mnuDespachado)
        Me.RibbonPageGroup4.ItemLinks.Add(Me.mnuEliminarLayoutGrid)
        Me.RibbonPageGroup4.ItemLinks.Add(Me.mnuReemplazo)
        Me.RibbonPageGroup4.ItemLinks.Add(Me.chkverifica_auto)
        Me.RibbonPageGroup4.ItemLinks.Add(Me.chkProcesarDesdeBOF)
        Me.RibbonPageGroup4.ItemLinks.Add(Me.chkFotografiaVerificacion)
        Me.RibbonPageGroup4.Name = "RibbonPageGroup4"
        Me.RibbonPageGroup4.Text = "Procesamiento"
        '
        'RibbonPageGroup5
        '
        Me.RibbonPageGroup5.ItemLinks.Add(Me.chkEmpaquePorTarima)
        Me.RibbonPageGroup5.ItemLinks.Add(Me.chkEmpaqueAGranel)
        Me.RibbonPageGroup5.Name = "RibbonPageGroup5"
        Me.RibbonPageGroup5.Text = "Empaque"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.mnuAsignacionAutomatica)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        Me.RibbonPageGroup3.Text = "Asignación de Operadores"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkActivo)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        Me.RibbonPageGroup2.Text = "Estado del Registro"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegistros)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 930)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1554, 30)
        Me.RibbonStatusBar.Visible = False
        '
        'RibbonStatusBar1
        '
        Me.RibbonStatusBar1.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar1.Location = New System.Drawing.Point(2, 672)
        Me.RibbonStatusBar1.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonStatusBar1.Name = "RibbonStatusBar1"
        Me.RibbonStatusBar1.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar1.Size = New System.Drawing.Size(1548, 33)
        '
        'RibbonStatusBar2
        '
        Me.RibbonStatusBar2.ItemLinks.Add(Me.lblRegs1)
        Me.RibbonStatusBar2.Location = New System.Drawing.Point(2, 450)
        Me.RibbonStatusBar2.Name = "RibbonStatusBar2"
        Me.RibbonStatusBar2.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar2.Size = New System.Drawing.Size(1153, 33)
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegistros)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 930)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1762, 30)
        Me.RibbonStatusBar.Visible = False
        '
        'PopupMenu1
        '
        Me.PopupMenu1.Name = "PopupMenu1"
        Me.PopupMenu1.Ribbon = Me.RibbonControl
        '
        'xtpDatosPicking
        '
        Me.xtpDatosPicking.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.xtpDatosPicking.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtpDatosPicking.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.[False]
        Me.xtpDatosPicking.Location = New System.Drawing.Point(0, 193)
        Me.xtpDatosPicking.Margin = New System.Windows.Forms.Padding(12)
        Me.xtpDatosPicking.MaxTabPageWidth = 100
        Me.xtpDatosPicking.Name = "xtpDatosPicking"
        Me.xtpDatosPicking.Padding = New System.Windows.Forms.Padding(12)
        Me.xtpDatosPicking.SelectedTabPage = Me.XtratabPageDato
        Me.xtpDatosPicking.Size = New System.Drawing.Size(1554, 737)
        Me.xtpDatosPicking.TabIndex = 0
        Me.xtpDatosPicking.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtratabPageDato, Me.XtratabPagePedido, Me.XtraTabPageUbicacionPicking, Me.tbDañados, Me.xtpImpresionPedidos})
        '
        'XtratabPageDato
        '
        Me.XtratabPageDato.Appearance.PageClient.BackColor = System.Drawing.SystemColors.ControlDark
        Me.XtratabPageDato.Appearance.PageClient.Options.UseBackColor = True
        Me.XtratabPageDato.Controls.Add(Me.GroupControl10)
        Me.XtratabPageDato.Margin = New System.Windows.Forms.Padding(4)
        Me.XtratabPageDato.Name = "XtratabPageDato"
        Me.XtratabPageDato.Size = New System.Drawing.Size(1552, 707)
        Me.XtratabPageDato.Text = "Datos Picking"
        '
        'GroupControl10
        '
        Me.GroupControl10.Controls.Add(Me.GroupControl8)
        Me.GroupControl10.Controls.Add(Me.GroupControl5)
        Me.GroupControl10.Controls.Add(Me.GrpTarea)
        Me.GroupControl10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl10.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl10.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl10.Name = "GroupControl10"
        Me.GroupControl10.Size = New System.Drawing.Size(1552, 707)
        Me.GroupControl10.TabIndex = 0
        Me.GroupControl10.Text = "Datos"
        '
        'GroupControl8
        '
        Me.GroupControl8.Controls.Add(Label14)
        Me.GroupControl8.Controls.Add(Label15)
        Me.GroupControl8.Controls.Add(Label13)
        Me.GroupControl8.Controls.Add(Label3)
        Me.GroupControl8.Controls.Add(Me.Fec_modDateEdit1)
        Me.GroupControl8.Controls.Add(Me.Fec_agrDateEdit1)
        Me.GroupControl8.Controls.Add(Me.User_modTextEdit1)
        Me.GroupControl8.Controls.Add(Me.User_agrTextEdit1)
        Me.GroupControl8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl8.Location = New System.Drawing.Point(2, 605)
        Me.GroupControl8.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl8.Name = "GroupControl8"
        Me.GroupControl8.Size = New System.Drawing.Size(974, 100)
        Me.GroupControl8.TabIndex = 21
        Me.GroupControl8.Text = "Bitácora"
        '
        'Fec_modDateEdit1
        '
        Me.Fec_modDateEdit1.EditValue = Nothing
        Me.Fec_modDateEdit1.Enabled = False
        Me.Fec_modDateEdit1.Location = New System.Drawing.Point(611, 66)
        Me.Fec_modDateEdit1.Margin = New System.Windows.Forms.Padding(4)
        Me.Fec_modDateEdit1.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit1.Name = "Fec_modDateEdit1"
        Me.Fec_modDateEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit1.Size = New System.Drawing.Size(286, 22)
        Me.Fec_modDateEdit1.TabIndex = 15
        '
        'Fec_agrDateEdit1
        '
        Me.Fec_agrDateEdit1.EditValue = Nothing
        Me.Fec_agrDateEdit1.Enabled = False
        Me.Fec_agrDateEdit1.Location = New System.Drawing.Point(132, 66)
        Me.Fec_agrDateEdit1.Margin = New System.Windows.Forms.Padding(4)
        Me.Fec_agrDateEdit1.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit1.Name = "Fec_agrDateEdit1"
        Me.Fec_agrDateEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit1.Size = New System.Drawing.Size(286, 22)
        Me.Fec_agrDateEdit1.TabIndex = 13
        '
        'User_modTextEdit1
        '
        Me.User_modTextEdit1.Enabled = False
        Me.User_modTextEdit1.Location = New System.Drawing.Point(611, 32)
        Me.User_modTextEdit1.Margin = New System.Windows.Forms.Padding(4)
        Me.User_modTextEdit1.Name = "User_modTextEdit1"
        Me.User_modTextEdit1.ReadOnly = True
        Me.User_modTextEdit1.Size = New System.Drawing.Size(285, 23)
        Me.User_modTextEdit1.TabIndex = 11
        '
        'User_agrTextEdit1
        '
        Me.User_agrTextEdit1.Enabled = False
        Me.User_agrTextEdit1.Location = New System.Drawing.Point(132, 32)
        Me.User_agrTextEdit1.Margin = New System.Windows.Forms.Padding(4)
        Me.User_agrTextEdit1.Name = "User_agrTextEdit1"
        Me.User_agrTextEdit1.ReadOnly = True
        Me.User_agrTextEdit1.Size = New System.Drawing.Size(285, 23)
        Me.User_agrTextEdit1.TabIndex = 9
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Label8)
        Me.GroupControl5.Controls.Add(Me.txtObservacion)
        Me.GroupControl5.Controls.Add(Me.groupPrioridad)
        Me.GroupControl5.Controls.Add(Me.dtmFechaPicking)
        Me.GroupControl5.Controls.Add(Me.cmbBodegas)
        Me.GroupControl5.Controls.Add(Me.cmbPropietario)
        Me.GroupControl5.Controls.Add(Me.lblEstado)
        Me.GroupControl5.Controls.Add(Label10)
        Me.GroupControl5.Controls.Add(Label12)
        Me.GroupControl5.Controls.Add(Label2)
        Me.GroupControl5.Controls.Add(IdPropietarioLabel)
        Me.GroupControl5.Controls.Add(Me.lblC)
        Me.GroupControl5.Controls.Add(Label1)
        Me.GroupControl5.Controls.Add(Me.GrpAsignacionTransaccion)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl5.Location = New System.Drawing.Point(2, 28)
        Me.GroupControl5.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(974, 554)
        Me.GroupControl5.TabIndex = 20
        Me.GroupControl5.Text = "Generales"
        '
        'txtObservacion
        '
        Me.txtObservacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtObservacion.Location = New System.Drawing.Point(147, 214)
        Me.txtObservacion.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtObservacion.Multiline = True
        Me.txtObservacion.Name = "txtObservacion"
        Me.txtObservacion.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtObservacion.Size = New System.Drawing.Size(329, 80)
        Me.txtObservacion.TabIndex = 72
        '
        'groupPrioridad
        '
        Me.groupPrioridad.Controls.Add(Me.rbAlto)
        Me.groupPrioridad.Controls.Add(Me.rbBajo)
        Me.groupPrioridad.Controls.Add(Me.rbMedio)
        Me.groupPrioridad.Controls.Add(Me.pbAlto)
        Me.groupPrioridad.Controls.Add(Me.pbMedio)
        Me.groupPrioridad.Controls.Add(Me.pbBajo)
        Me.groupPrioridad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.groupPrioridad.Location = New System.Drawing.Point(547, 55)
        Me.groupPrioridad.Margin = New System.Windows.Forms.Padding(4)
        Me.groupPrioridad.Name = "groupPrioridad"
        Me.groupPrioridad.Padding = New System.Windows.Forms.Padding(4)
        Me.groupPrioridad.Size = New System.Drawing.Size(323, 130)
        Me.groupPrioridad.TabIndex = 26
        Me.groupPrioridad.TabStop = False
        Me.groupPrioridad.Text = "Prioridad"
        '
        'rbAlto
        '
        Me.rbAlto.AutoSize = True
        Me.rbAlto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbAlto.Location = New System.Drawing.Point(40, 28)
        Me.rbAlto.Margin = New System.Windows.Forms.Padding(4)
        Me.rbAlto.Name = "rbAlto"
        Me.rbAlto.Size = New System.Drawing.Size(53, 21)
        Me.rbAlto.TabIndex = 0
        Me.rbAlto.TabStop = True
        Me.rbAlto.Text = "Alta"
        Me.rbAlto.UseVisualStyleBackColor = True
        '
        'rbBajo
        '
        Me.rbBajo.AutoSize = True
        Me.rbBajo.Checked = True
        Me.rbBajo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbBajo.Location = New System.Drawing.Point(40, 98)
        Me.rbBajo.Margin = New System.Windows.Forms.Padding(4)
        Me.rbBajo.Name = "rbBajo"
        Me.rbBajo.Size = New System.Drawing.Size(57, 21)
        Me.rbBajo.TabIndex = 2
        Me.rbBajo.TabStop = True
        Me.rbBajo.Text = "Baja"
        Me.rbBajo.UseVisualStyleBackColor = True
        '
        'rbMedio
        '
        Me.rbMedio.AutoSize = True
        Me.rbMedio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbMedio.Location = New System.Drawing.Point(40, 64)
        Me.rbMedio.Margin = New System.Windows.Forms.Padding(4)
        Me.rbMedio.Name = "rbMedio"
        Me.rbMedio.Size = New System.Drawing.Size(67, 21)
        Me.rbMedio.TabIndex = 1
        Me.rbMedio.TabStop = True
        Me.rbMedio.Text = "Media"
        Me.rbMedio.UseVisualStyleBackColor = True
        '
        'pbAlto
        '
        Me.pbAlto.Image = Global.TOMWMS.My.Resources.Resources.red_ball
        Me.pbAlto.Location = New System.Drawing.Point(13, 30)
        Me.pbAlto.Margin = New System.Windows.Forms.Padding(4)
        Me.pbAlto.Name = "pbAlto"
        Me.pbAlto.Size = New System.Drawing.Size(19, 20)
        Me.pbAlto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbAlto.TabIndex = 91
        Me.pbAlto.TabStop = False
        '
        'pbMedio
        '
        Me.pbMedio.Image = Global.TOMWMS.My.Resources.Resources.yellow_ball
        Me.pbMedio.Location = New System.Drawing.Point(13, 65)
        Me.pbMedio.Margin = New System.Windows.Forms.Padding(4)
        Me.pbMedio.Name = "pbMedio"
        Me.pbMedio.Size = New System.Drawing.Size(19, 20)
        Me.pbMedio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbMedio.TabIndex = 93
        Me.pbMedio.TabStop = False
        '
        'pbBajo
        '
        Me.pbBajo.Image = Global.TOMWMS.My.Resources.Resources.green_ball
        Me.pbBajo.Location = New System.Drawing.Point(13, 100)
        Me.pbBajo.Margin = New System.Windows.Forms.Padding(4)
        Me.pbBajo.Name = "pbBajo"
        Me.pbBajo.Size = New System.Drawing.Size(19, 20)
        Me.pbBajo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbBajo.TabIndex = 95
        Me.pbBajo.TabStop = False
        '
        'dtmFechaPicking
        '
        Me.dtmFechaPicking.EditValue = New Date(2017, 11, 20, 9, 15, 49, 963)
        Me.dtmFechaPicking.Location = New System.Drawing.Point(147, 116)
        Me.dtmFechaPicking.Margin = New System.Windows.Forms.Padding(4)
        Me.dtmFechaPicking.MenuManager = Me.RibbonControl
        Me.dtmFechaPicking.Name = "dtmFechaPicking"
        Me.dtmFechaPicking.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaPicking.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaPicking.Size = New System.Drawing.Size(329, 22)
        Me.dtmFechaPicking.TabIndex = 19
        '
        'cmbBodegas
        '
        Me.cmbBodegas.Location = New System.Drawing.Point(147, 148)
        Me.cmbBodegas.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbBodegas.MenuManager = Me.RibbonControl
        Me.cmbBodegas.Name = "cmbBodegas"
        Me.cmbBodegas.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodegas.Properties.NullText = ""
        Me.cmbBodegas.Size = New System.Drawing.Size(329, 22)
        Me.cmbBodegas.TabIndex = 18
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Location = New System.Drawing.Point(147, 181)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPropietario.MenuManager = Me.RibbonControl
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Size = New System.Drawing.Size(329, 22)
        Me.cmbPropietario.TabIndex = 17
        '
        'lblEstado
        '
        Me.lblEstado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEstado.Location = New System.Drawing.Point(147, 86)
        Me.lblEstado.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(328, 20)
        Me.lblEstado.TabIndex = 3
        '
        'lblC
        '
        Me.lblC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblC.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblC.Location = New System.Drawing.Point(147, 53)
        Me.lblC.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblC.Name = "lblC"
        Me.lblC.Size = New System.Drawing.Size(328, 20)
        Me.lblC.TabIndex = 1
        '
        'GrpAsignacionTransaccion
        '
        Me.GrpAsignacionTransaccion.Controls.Add(Me.cmbMuelle)
        Me.GrpAsignacionTransaccion.Controls.Add(Me.txtIdUbicacionMuelle)
        Me.GrpAsignacionTransaccion.Controls.Add(Label6)
        Me.GrpAsignacionTransaccion.Controls.Add(Label5)
        Me.GrpAsignacionTransaccion.Controls.Add(Me.txtReferencia)
        Me.GrpAsignacionTransaccion.Controls.Add(Me.txtNombreUbicacion)
        Me.GrpAsignacionTransaccion.Controls.Add(Me.lnkUbicacion)
        Me.GrpAsignacionTransaccion.Controls.Add(Me.txtIdUbicacion)
        Me.GrpAsignacionTransaccion.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GrpAsignacionTransaccion.Location = New System.Drawing.Point(2, 426)
        Me.GrpAsignacionTransaccion.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpAsignacionTransaccion.Name = "GrpAsignacionTransaccion"
        Me.GrpAsignacionTransaccion.Size = New System.Drawing.Size(970, 126)
        Me.GrpAsignacionTransaccion.TabIndex = 14
        Me.GrpAsignacionTransaccion.Text = "Asignación de Transacción"
        '
        'cmbMuelle
        '
        Me.cmbMuelle.Location = New System.Drawing.Point(145, 75)
        Me.cmbMuelle.Margin = New System.Windows.Forms.Padding(6)
        Me.cmbMuelle.MenuManager = Me.RibbonControl
        Me.cmbMuelle.Name = "cmbMuelle"
        Me.cmbMuelle.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMuelle.Properties.NullText = ""
        Me.cmbMuelle.Properties.PopupView = Me.GridView12
        Me.cmbMuelle.Size = New System.Drawing.Size(256, 22)
        Me.cmbMuelle.TabIndex = 99
        '
        'GridView12
        '
        Me.GridView12.DetailHeight = 682
        Me.GridView12.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView12.Name = "GridView12"
        Me.GridView12.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView12.OptionsView.ShowAutoFilterRow = True
        Me.GridView12.OptionsView.ShowGroupPanel = False
        '
        'txtIdUbicacionMuelle
        '
        Me.txtIdUbicacionMuelle.Location = New System.Drawing.Point(409, 75)
        Me.txtIdUbicacionMuelle.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdUbicacionMuelle.MenuManager = Me.RibbonControl
        Me.txtIdUbicacionMuelle.Name = "txtIdUbicacionMuelle"
        Me.txtIdUbicacionMuelle.Properties.ReadOnly = True
        Me.txtIdUbicacionMuelle.Size = New System.Drawing.Size(82, 22)
        Me.txtIdUbicacionMuelle.TabIndex = 20
        '
        'txtReferencia
        '
        Me.txtReferencia.Location = New System.Drawing.Point(611, 42)
        Me.txtReferencia.Margin = New System.Windows.Forms.Padding(4)
        Me.txtReferencia.MenuManager = Me.RibbonControl
        Me.txtReferencia.Name = "txtReferencia"
        Me.txtReferencia.Properties.ReadOnly = True
        Me.txtReferencia.Size = New System.Drawing.Size(266, 22)
        Me.txtReferencia.TabIndex = 3
        '
        'txtNombreUbicacion
        '
        Me.txtNombreUbicacion.Location = New System.Drawing.Point(234, 46)
        Me.txtNombreUbicacion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNombreUbicacion.MenuManager = Me.RibbonControl
        Me.txtNombreUbicacion.Name = "txtNombreUbicacion"
        Me.txtNombreUbicacion.Properties.Mask.EditMask = "n0"
        Me.txtNombreUbicacion.Properties.ReadOnly = True
        Me.txtNombreUbicacion.Size = New System.Drawing.Size(257, 22)
        Me.txtNombreUbicacion.TabIndex = 2
        '
        'lnkUbicacion
        '
        Me.lnkUbicacion.AutoSize = True
        Me.lnkUbicacion.Location = New System.Drawing.Point(22, 48)
        Me.lnkUbicacion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkUbicacion.Name = "lnkUbicacion"
        Me.lnkUbicacion.Size = New System.Drawing.Size(104, 16)
        Me.lnkUbicacion.TabIndex = 0
        Me.lnkUbicacion.TabStop = True
        Me.lnkUbicacion.Text = "Ubicación Picking"
        '
        'txtIdUbicacion
        '
        Me.txtIdUbicacion.Location = New System.Drawing.Point(145, 46)
        Me.txtIdUbicacion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdUbicacion.MenuManager = Me.RibbonControl
        Me.txtIdUbicacion.Name = "txtIdUbicacion"
        Me.txtIdUbicacion.Size = New System.Drawing.Size(82, 22)
        Me.txtIdUbicacion.TabIndex = 1
        '
        'GrpTarea
        '
        Me.GrpTarea.Controls.Add(Me.grpGaugueProgreso)
        Me.GrpTarea.Controls.Add(Me.dtmFechaTarea)
        Me.GrpTarea.Controls.Add(Me.GroupControl4)
        Me.GrpTarea.Controls.Add(Me.GroupControl9)
        Me.GrpTarea.Dock = System.Windows.Forms.DockStyle.Right
        Me.GrpTarea.Location = New System.Drawing.Point(976, 28)
        Me.GrpTarea.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpTarea.Name = "GrpTarea"
        Me.GrpTarea.Size = New System.Drawing.Size(574, 677)
        Me.GrpTarea.TabIndex = 15
        Me.GrpTarea.Text = "Fecha Tarea"
        '
        'grpGaugueProgreso
        '
        Me.grpGaugueProgreso.Controls.Add(Me.GaugeControl1)
        Me.grpGaugueProgreso.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grpGaugueProgreso.Location = New System.Drawing.Point(2, 452)
        Me.grpGaugueProgreso.Margin = New System.Windows.Forms.Padding(4)
        Me.grpGaugueProgreso.Name = "grpGaugueProgreso"
        Me.grpGaugueProgreso.Size = New System.Drawing.Size(570, 223)
        Me.grpGaugueProgreso.TabIndex = 21
        Me.grpGaugueProgreso.Text = "Progreso"
        '
        'GaugeControl1
        '
        Me.GaugeControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GaugeControl1.Gauges.AddRange(New DevExpress.XtraGauges.Base.IGauge() {Me.cgProgresoPicking})
        Me.GaugeControl1.Location = New System.Drawing.Point(2, 28)
        Me.GaugeControl1.Name = "GaugeControl1"
        Me.GaugeControl1.Size = New System.Drawing.Size(566, 193)
        Me.GaugeControl1.TabIndex = 0
        '
        'cgProgresoPicking
        '
        Me.cgProgresoPicking.BackgroundLayers.AddRange(New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent() {Me.ArcScaleBackgroundLayerComponent1})
        Me.cgProgresoPicking.Bounds = New System.Drawing.Rectangle(6, 6, 554, 181)
        Me.cgProgresoPicking.Name = "cgProgresoPicking"
        Me.cgProgresoPicking.Needles.AddRange(New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent() {Me.ArcScaleNeedleComponent1})
        Me.cgProgresoPicking.Scales.AddRange(New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent() {Me.ArcScaleComponent1})
        Me.cgProgresoPicking.SpindleCaps.AddRange(New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent() {Me.ArcScaleSpindleCapComponent1})
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
        'dtmFechaTarea
        '
        Me.dtmFechaTarea.EditValue = New Date(2017, 11, 20, 9, 17, 59, 855)
        Me.dtmFechaTarea.Location = New System.Drawing.Point(128, 33)
        Me.dtmFechaTarea.Margin = New System.Windows.Forms.Padding(4)
        Me.dtmFechaTarea.MenuManager = Me.RibbonControl
        Me.dtmFechaTarea.Name = "dtmFechaTarea"
        Me.dtmFechaTarea.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaTarea.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaTarea.Size = New System.Drawing.Size(232, 22)
        Me.dtmFechaTarea.TabIndex = 20
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Label37)
        Me.GroupControl4.Controls.Add(Me.dtmHoraFhh)
        Me.GroupControl4.Controls.Add(Label38)
        Me.GroupControl4.Controls.Add(Me.dtmHoraIhh)
        Me.GroupControl4.Enabled = False
        Me.GroupControl4.Location = New System.Drawing.Point(247, 71)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(234, 127)
        Me.GroupControl4.TabIndex = 2
        Me.GroupControl4.Text = "Hora HandHeld"
        '
        'dtmHoraFhh
        '
        Me.dtmHoraFhh.CustomFormat = "hh:mm:ss"
        Me.dtmHoraFhh.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtmHoraFhh.Location = New System.Drawing.Point(91, 78)
        Me.dtmHoraFhh.Margin = New System.Windows.Forms.Padding(4)
        Me.dtmHoraFhh.Name = "dtmHoraFhh"
        Me.dtmHoraFhh.ShowUpDown = True
        Me.dtmHoraFhh.Size = New System.Drawing.Size(131, 23)
        Me.dtmHoraFhh.TabIndex = 3
        '
        'dtmHoraIhh
        '
        Me.dtmHoraIhh.CustomFormat = "hh:mm:ss"
        Me.dtmHoraIhh.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtmHoraIhh.Location = New System.Drawing.Point(91, 44)
        Me.dtmHoraIhh.Margin = New System.Windows.Forms.Padding(4)
        Me.dtmHoraIhh.Name = "dtmHoraIhh"
        Me.dtmHoraIhh.ShowUpDown = True
        Me.dtmHoraIhh.Size = New System.Drawing.Size(131, 23)
        Me.dtmHoraIhh.TabIndex = 1
        '
        'GroupControl9
        '
        Me.GroupControl9.Controls.Add(Label30)
        Me.GroupControl9.Controls.Add(Me.dtmHoraF)
        Me.GroupControl9.Controls.Add(Label7)
        Me.GroupControl9.Controls.Add(Me.dtmHoraI)
        Me.GroupControl9.Location = New System.Drawing.Point(7, 71)
        Me.GroupControl9.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl9.Name = "GroupControl9"
        Me.GroupControl9.Size = New System.Drawing.Size(233, 127)
        Me.GroupControl9.TabIndex = 1
        Me.GroupControl9.Text = "Hora Teórica"
        '
        'dtmHoraF
        '
        Me.dtmHoraF.CustomFormat = "hh:mm:ss"
        Me.dtmHoraF.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtmHoraF.Location = New System.Drawing.Point(90, 78)
        Me.dtmHoraF.Margin = New System.Windows.Forms.Padding(4)
        Me.dtmHoraF.Name = "dtmHoraF"
        Me.dtmHoraF.ShowUpDown = True
        Me.dtmHoraF.Size = New System.Drawing.Size(131, 23)
        Me.dtmHoraF.TabIndex = 3
        '
        'dtmHoraI
        '
        Me.dtmHoraI.CustomFormat = "hh:mm:ss"
        Me.dtmHoraI.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtmHoraI.Location = New System.Drawing.Point(90, 44)
        Me.dtmHoraI.Margin = New System.Windows.Forms.Padding(4)
        Me.dtmHoraI.Name = "dtmHoraI"
        Me.dtmHoraI.ShowUpDown = True
        Me.dtmHoraI.Size = New System.Drawing.Size(131, 23)
        Me.dtmHoraI.TabIndex = 1
        '
        'XtratabPagePedido
        '
        Me.XtratabPagePedido.Controls.Add(Me.SplitContainerControl1)
        Me.XtratabPagePedido.Margin = New System.Windows.Forms.Padding(4)
        Me.XtratabPagePedido.Name = "XtratabPagePedido"
        Me.XtratabPagePedido.Padding = New System.Windows.Forms.Padding(12)
        Me.XtratabPagePedido.Size = New System.Drawing.Size(1552, 707)
        Me.XtratabPagePedido.Text = "Pedidos"
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Horizontal = False
        Me.SplitContainerControl1.Location = New System.Drawing.Point(12, 12)
        Me.SplitContainerControl1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        '
        'SplitContainerControl1.Panel1
        '
        Me.SplitContainerControl1.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.GroupControl6)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.GroupControl1)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.ShowSplitGlyph = DevExpress.Utils.DefaultBoolean.[False]
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1528, 683)
        Me.SplitContainerControl1.SplitterPosition = 169
        Me.SplitContainerControl1.TabIndex = 1
        '
        'GroupControl6
        '
        Me.GroupControl6.Controls.Add(Me.dgridPedidos)
        Me.GroupControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl6.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl6.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl6.Name = "GroupControl6"
        Me.GroupControl6.Size = New System.Drawing.Size(1520, 165)
        Me.GroupControl6.TabIndex = 0
        Me.GroupControl6.Text = "Encabezado de Pedido"
        '
        'dgridPedidos
        '
        Me.dgridPedidos.AllowUserToResizeRows = False
        Me.dgridPedidos.BackgroundColor = System.Drawing.Color.LightSteelBlue
        Me.dgridPedidos.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgridPedidos.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgridPedidos.ColumnHeadersHeight = 40
        Me.dgridPedidos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdPedido, Me.Referencia, Me.Bodega, Me.Cliente, Me.Propietario, Me.FechaPedido, Me.EstadoP})
        Me.dgridPedidos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridPedidos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgridPedidos.EnableHeadersVisualStyles = False
        Me.dgridPedidos.GridColor = System.Drawing.Color.Navy
        Me.dgridPedidos.Location = New System.Drawing.Point(2, 28)
        Me.dgridPedidos.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridPedidos.MultiSelect = False
        Me.dgridPedidos.Name = "dgridPedidos"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgridPedidos.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgridPedidos.RowHeadersVisible = False
        Me.dgridPedidos.RowHeadersWidth = 40
        Me.dgridPedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgridPedidos.Size = New System.Drawing.Size(1516, 135)
        Me.dgridPedidos.TabIndex = 1
        '
        'IdPedido
        '
        Me.IdPedido.HeaderText = "Pedido"
        Me.IdPedido.MinimumWidth = 6
        Me.IdPedido.Name = "IdPedido"
        Me.IdPedido.ReadOnly = True
        Me.IdPedido.Width = 125
        '
        'Referencia
        '
        Me.Referencia.HeaderText = "Referencia"
        Me.Referencia.MinimumWidth = 6
        Me.Referencia.Name = "Referencia"
        Me.Referencia.Width = 125
        '
        'Bodega
        '
        Me.Bodega.HeaderText = "Bodega"
        Me.Bodega.MinimumWidth = 6
        Me.Bodega.Name = "Bodega"
        Me.Bodega.ReadOnly = True
        Me.Bodega.Width = 125
        '
        'Cliente
        '
        Me.Cliente.HeaderText = "Cliente"
        Me.Cliente.MinimumWidth = 6
        Me.Cliente.Name = "Cliente"
        Me.Cliente.ReadOnly = True
        Me.Cliente.Width = 125
        '
        'Propietario
        '
        Me.Propietario.HeaderText = "Propietario"
        Me.Propietario.MinimumWidth = 6
        Me.Propietario.Name = "Propietario"
        Me.Propietario.ReadOnly = True
        Me.Propietario.Width = 125
        '
        'FechaPedido
        '
        Me.FechaPedido.HeaderText = "Fecha Pedido"
        Me.FechaPedido.MinimumWidth = 6
        Me.FechaPedido.Name = "FechaPedido"
        Me.FechaPedido.ReadOnly = True
        Me.FechaPedido.Width = 125
        '
        'EstadoP
        '
        Me.EstadoP.HeaderText = "Estado"
        Me.EstadoP.MinimumWidth = 6
        Me.EstadoP.Name = "EstadoP"
        Me.EstadoP.ReadOnly = True
        Me.EstadoP.Width = 125
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Label4)
        Me.GroupControl1.Controls.Add(Me.cmbAgrupamiento)
        Me.GroupControl1.Controls.Add(Me.dgridDetallePicking)
        Me.GroupControl1.Controls.Add(Me.lnkVerParametro)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1520, 494)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Detalles de Pedidos"
        '
        'cmbAgrupamiento
        '
        Me.cmbAgrupamiento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgrupamiento.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAgrupamiento.ForeColor = System.Drawing.Color.Navy
        Me.cmbAgrupamiento.Items.AddRange(New Object() {"Consolidado", "Detalle"})
        Me.cmbAgrupamiento.Location = New System.Drawing.Point(230, 33)
        Me.cmbAgrupamiento.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbAgrupamiento.Name = "cmbAgrupamiento"
        Me.cmbAgrupamiento.Size = New System.Drawing.Size(171, 25)
        Me.cmbAgrupamiento.TabIndex = 2
        '
        'dgridDetallePicking
        '
        Me.dgridDetallePicking.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgridDetallePicking.BackgroundColor = System.Drawing.Color.LightSteelBlue
        Me.dgridDetallePicking.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgridDetallePicking.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgridDetallePicking.ColumnHeadersHeight = 40
        Me.dgridDetallePicking.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdPedidoEnc, Me.Codigo, Me.Producto, Me.Presentacion, Me.UnidadMedida, Me.Estado, Me.Cantidad, Me.ClienteDias, Me.CantidadRecibida, Me.OperadorBodega, Me.IdPedidoDet})
        Me.dgridDetallePicking.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgridDetallePicking.EnableHeadersVisualStyles = False
        Me.dgridDetallePicking.GridColor = System.Drawing.Color.Navy
        Me.dgridDetallePicking.Location = New System.Drawing.Point(6, 74)
        Me.dgridDetallePicking.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridDetallePicking.MultiSelect = False
        Me.dgridDetallePicking.Name = "dgridDetallePicking"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgridDetallePicking.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.dgridDetallePicking.RowHeadersVisible = False
        Me.dgridDetallePicking.RowHeadersWidth = 40
        Me.dgridDetallePicking.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgridDetallePicking.Size = New System.Drawing.Size(1508, 409)
        Me.dgridDetallePicking.TabIndex = 3
        '
        'IdPedidoEnc
        '
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.Format = "N0"
        Me.IdPedidoEnc.DefaultCellStyle = DataGridViewCellStyle4
        Me.IdPedidoEnc.HeaderText = "Pedido"
        Me.IdPedidoEnc.MinimumWidth = 6
        Me.IdPedidoEnc.Name = "IdPedidoEnc"
        Me.IdPedidoEnc.ReadOnly = True
        Me.IdPedidoEnc.Width = 125
        '
        'Codigo
        '
        Me.Codigo.HeaderText = "Código"
        Me.Codigo.MinimumWidth = 6
        Me.Codigo.Name = "Codigo"
        Me.Codigo.ReadOnly = True
        Me.Codigo.Width = 125
        '
        'Producto
        '
        Me.Producto.HeaderText = "Producto"
        Me.Producto.MinimumWidth = 6
        Me.Producto.Name = "Producto"
        Me.Producto.ReadOnly = True
        Me.Producto.Width = 125
        '
        'Presentacion
        '
        Me.Presentacion.HeaderText = "Presentación"
        Me.Presentacion.MinimumWidth = 6
        Me.Presentacion.Name = "Presentacion"
        Me.Presentacion.ReadOnly = True
        Me.Presentacion.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Presentacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Presentacion.Width = 125
        '
        'UnidadMedida
        '
        Me.UnidadMedida.HeaderText = "Unidad Medida"
        Me.UnidadMedida.MinimumWidth = 6
        Me.UnidadMedida.Name = "UnidadMedida"
        Me.UnidadMedida.ReadOnly = True
        Me.UnidadMedida.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.UnidadMedida.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.UnidadMedida.Width = 125
        '
        'Estado
        '
        Me.Estado.HeaderText = "Estado"
        Me.Estado.MinimumWidth = 6
        Me.Estado.Name = "Estado"
        Me.Estado.ReadOnly = True
        Me.Estado.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Estado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Estado.Width = 125
        '
        'Cantidad
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.Cantidad.DefaultCellStyle = DataGridViewCellStyle5
        Me.Cantidad.HeaderText = "Cantidad"
        Me.Cantidad.MinimumWidth = 6
        Me.Cantidad.Name = "Cantidad"
        Me.Cantidad.ReadOnly = True
        Me.Cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Cantidad.Width = 125
        '
        'ClienteDias
        '
        Me.ClienteDias.HeaderText = "Cliente Días"
        Me.ClienteDias.MinimumWidth = 6
        Me.ClienteDias.Name = "ClienteDias"
        Me.ClienteDias.Width = 125
        '
        'CantidadRecibida
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = "0.0"
        Me.CantidadRecibida.DefaultCellStyle = DataGridViewCellStyle6
        Me.CantidadRecibida.HeaderText = "Cantidad Recibida"
        Me.CantidadRecibida.MinimumWidth = 6
        Me.CantidadRecibida.Name = "CantidadRecibida"
        Me.CantidadRecibida.ReadOnly = True
        Me.CantidadRecibida.Width = 125
        '
        'OperadorBodega
        '
        Me.OperadorBodega.HeaderText = "Operador"
        Me.OperadorBodega.MinimumWidth = 6
        Me.OperadorBodega.Name = "OperadorBodega"
        Me.OperadorBodega.Width = 125
        '
        'IdPedidoDet
        '
        Me.IdPedidoDet.HeaderText = "IdPedidoDet"
        Me.IdPedidoDet.MinimumWidth = 6
        Me.IdPedidoDet.Name = "IdPedidoDet"
        Me.IdPedidoDet.ReadOnly = True
        Me.IdPedidoDet.Visible = False
        Me.IdPedidoDet.Width = 125
        '
        'lnkVerParametro
        '
        Me.lnkVerParametro.AutoSize = True
        Me.lnkVerParametro.Location = New System.Drawing.Point(12, 37)
        Me.lnkVerParametro.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkVerParametro.Name = "lnkVerParametro"
        Me.lnkVerParametro.Size = New System.Drawing.Size(119, 16)
        Me.lnkVerParametro.TabIndex = 0
        Me.lnkVerParametro.TabStop = True
        Me.lnkVerParametro.Text = "Ver Parámetro (F3)"
        '
        'XtraTabPageUbicacionPicking
        '
        Me.XtraTabPageUbicacionPicking.Appearance.HeaderActive.BackColor = System.Drawing.Color.PowderBlue
        Me.XtraTabPageUbicacionPicking.Appearance.HeaderActive.Options.UseBackColor = True
        Me.XtraTabPageUbicacionPicking.Controls.Add(Me.GroupControl7)
        Me.XtraTabPageUbicacionPicking.Margin = New System.Windows.Forms.Padding(4)
        Me.XtraTabPageUbicacionPicking.Name = "XtraTabPageUbicacionPicking"
        Me.XtraTabPageUbicacionPicking.Size = New System.Drawing.Size(1552, 707)
        Me.XtraTabPageUbicacionPicking.Text = "Asignación"
        '
        'GroupControl7
        '
        Me.GroupControl7.Controls.Add(Me.TableLayoutPanel1)
        Me.GroupControl7.Controls.Add(Me.RibbonStatusBar1)
        Me.GroupControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl7.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl7.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl7.Name = "GroupControl7"
        Me.GroupControl7.Size = New System.Drawing.Size(1552, 707)
        Me.GroupControl7.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.51643!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.48357!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupControl2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.dgridPickingUbic, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 28)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.26566!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1548, 644)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.GroupControl3)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(863, 4)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(681, 636)
        Me.GroupControl2.TabIndex = 0
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.grdOperadorBodega)
        Me.GroupControl3.Controls.Add(Me.ToolStripPR)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(2, 28)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(677, 606)
        Me.GroupControl3.TabIndex = 0
        Me.GroupControl3.Text = "Selección de Operadores"
        '
        'grdOperadorBodega
        '
        Me.grdOperadorBodega.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdOperadorBodega.DataSource = Me.DataBindingSource
        Me.grdOperadorBodega.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdOperadorBodega.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdOperadorBodega.Location = New System.Drawing.Point(2, 55)
        Me.grdOperadorBodega.MainView = Me.DgridOperadorBodega
        Me.grdOperadorBodega.Margin = New System.Windows.Forms.Padding(4)
        Me.grdOperadorBodega.Name = "grdOperadorBodega"
        Me.grdOperadorBodega.Size = New System.Drawing.Size(673, 549)
        Me.grdOperadorBodega.TabIndex = 0
        Me.grdOperadorBodega.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.DgridOperadorBodega})
        '
        'DataBindingSource
        '
        Me.DataBindingSource.DataMember = "Data"
        Me.DataBindingSource.DataSource = Me.DsOrdenCompraRecepcionOperador
        '
        'DsOrdenCompraRecepcionOperador
        '
        Me.DsOrdenCompraRecepcionOperador.DataSetName = "DsOrdenCompraRecepcionOperador"
        Me.DsOrdenCompraRecepcionOperador.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DgridOperadorBodega
        '
        Me.DgridOperadorBodega.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgridOperadorBodega.Appearance.HeaderPanel.Options.UseFont = True
        Me.DgridOperadorBodega.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgridOperadorBodega.Appearance.Row.Options.UseFont = True
        Me.behaviorManager1.SetBehaviors(Me.DgridOperadorBodega, New DevExpress.Utils.Behaviors.Behavior() {CType(DevExpress.Utils.DragDrop.DragDropBehavior.Create(GetType(DevExpress.XtraGrid.Extensions.ColumnViewDragDropSource), True, True, True, True, Me.OperadorDragDropEvent), DevExpress.Utils.Behaviors.Behavior)})
        Me.DgridOperadorBodega.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colIdOperadorBodega, Me.colSelección, Me.colIdOperadorRec, Me.colOperador, Me.colcolUsaHH})
        Me.DgridOperadorBodega.DetailHeight = 431
        Me.DgridOperadorBodega.GridControl = Me.grdOperadorBodega
        Me.DgridOperadorBodega.Name = "DgridOperadorBodega"
        Me.DgridOperadorBodega.OptionsFind.AlwaysVisible = True
        Me.DgridOperadorBodega.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.DgridOperadorBodega.OptionsView.ShowAutoFilterRow = True
        Me.DgridOperadorBodega.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways
        Me.DgridOperadorBodega.OptionsView.ShowFooter = True
        Me.DgridOperadorBodega.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.colSelección, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.colIdOperadorBodega, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'colIdOperadorBodega
        '
        Me.colIdOperadorBodega.Caption = "IdOperadorBodega"
        Me.colIdOperadorBodega.FieldName = "IdOperadorBodega"
        Me.colIdOperadorBodega.FieldNameSortGroup = "IdOperadorBodega"
        Me.colIdOperadorBodega.MinWidth = 23
        Me.colIdOperadorBodega.Name = "colIdOperadorBodega"
        Me.colIdOperadorBodega.OptionsColumn.ReadOnly = True
        Me.colIdOperadorBodega.Visible = True
        Me.colIdOperadorBodega.VisibleIndex = 3
        Me.colIdOperadorBodega.Width = 87
        '
        'colSelección
        '
        Me.colSelección.FieldName = "Selección"
        Me.colSelección.MinWidth = 23
        Me.colSelección.Name = "colSelección"
        Me.colSelección.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Selección", "{0}")})
        Me.colSelección.Visible = True
        Me.colSelección.VisibleIndex = 0
        Me.colSelección.Width = 87
        '
        'colIdOperadorRec
        '
        Me.colIdOperadorRec.FieldName = "IdOperadorRec"
        Me.colIdOperadorRec.MinWidth = 23
        Me.colIdOperadorRec.Name = "colIdOperadorRec"
        Me.colIdOperadorRec.OptionsColumn.ReadOnly = True
        Me.colIdOperadorRec.Width = 87
        '
        'colOperador
        '
        Me.colOperador.FieldName = "Operador"
        Me.colOperador.MinWidth = 23
        Me.colOperador.Name = "colOperador"
        Me.colOperador.OptionsColumn.ReadOnly = True
        Me.colOperador.Visible = True
        Me.colOperador.VisibleIndex = 1
        Me.colOperador.Width = 87
        '
        'colcolUsaHH
        '
        Me.colcolUsaHH.Caption = "Usa HH"
        Me.colcolUsaHH.FieldName = "colUsaHH"
        Me.colcolUsaHH.MinWidth = 23
        Me.colcolUsaHH.Name = "colcolUsaHH"
        Me.colcolUsaHH.OptionsColumn.ReadOnly = True
        Me.colcolUsaHH.Visible = True
        Me.colcolUsaHH.VisibleIndex = 2
        Me.colcolUsaHH.Width = 87
        '
        'ToolStripPR
        '
        Me.ToolStripPR.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStripPR.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdSavePR, Me.cmdDesactivarPresentacion})
        Me.ToolStripPR.Location = New System.Drawing.Point(2, 28)
        Me.ToolStripPR.Name = "ToolStripPR"
        Me.ToolStripPR.Size = New System.Drawing.Size(673, 27)
        Me.ToolStripPR.TabIndex = 2
        Me.ToolStripPR.Text = "ToolStrip2"
        '
        'cmdSavePR
        '
        Me.cmdSavePR.Image = CType(resources.GetObject("cmdSavePR.Image"), System.Drawing.Image)
        Me.cmdSavePR.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSavePR.Name = "cmdSavePR"
        Me.cmdSavePR.Size = New System.Drawing.Size(73, 24)
        Me.cmdSavePR.Text = "Todos"
        '
        'cmdDesactivarPresentacion
        '
        Me.cmdDesactivarPresentacion.Image = CType(resources.GetObject("cmdDesactivarPresentacion.Image"), System.Drawing.Image)
        Me.cmdDesactivarPresentacion.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDesactivarPresentacion.Name = "cmdDesactivarPresentacion"
        Me.cmdDesactivarPresentacion.Size = New System.Drawing.Size(90, 24)
        Me.cmdDesactivarPresentacion.Text = "Ninguno"
        '
        'dgridPickingUbic
        '
        Me.dgridPickingUbic.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgridPickingUbic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridPickingUbic.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode1.RelationName = "Level1"
        Me.dgridPickingUbic.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.dgridPickingUbic.Location = New System.Drawing.Point(4, 4)
        Me.dgridPickingUbic.MainView = Me.grdvPickingUbic
        Me.dgridPickingUbic.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridPickingUbic.MenuManager = Me.RibbonControl
        Me.dgridPickingUbic.Name = "dgridPickingUbic"
        Me.dgridPickingUbic.Size = New System.Drawing.Size(851, 636)
        Me.dgridPickingUbic.TabIndex = 1
        Me.dgridPickingUbic.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvPickingUbic})
        '
        'grdvPickingUbic
        '
        Me.grdvPickingUbic.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdvPickingUbic.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdvPickingUbic.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdvPickingUbic.Appearance.Row.Options.UseFont = True
        Me.behaviorManager1.SetBehaviors(Me.grdvPickingUbic, New DevExpress.Utils.Behaviors.Behavior() {CType(DevExpress.Utils.DragDrop.DragDropBehavior.Create(GetType(DevExpress.XtraGrid.Extensions.ColumnViewDragDropSource), True, True, True, True, Me.pickingUbicDragDropEvent), DevExpress.Utils.Behaviors.Behavior)})
        Me.grdvPickingUbic.DetailHeight = 431
        GridFormatRule1.Name = "Format0"
        GridFormatRule1.Rule = Nothing
        Me.grdvPickingUbic.FormatRules.Add(GridFormatRule1)
        Me.grdvPickingUbic.GridControl = Me.dgridPickingUbic
        Me.grdvPickingUbic.Name = "grdvPickingUbic"
        Me.grdvPickingUbic.OptionsBehavior.Editable = False
        Me.grdvPickingUbic.OptionsView.ColumnAutoWidth = False
        Me.grdvPickingUbic.OptionsView.ShowAutoFilterRow = True
        Me.grdvPickingUbic.OptionsView.ShowFooter = True
        '
        'tbDañados
        '
        Me.tbDañados.Controls.Add(Me.PanelControl1)
        Me.tbDañados.Margin = New System.Windows.Forms.Padding(4)
        Me.tbDañados.Name = "tbDañados"
        Me.tbDañados.Size = New System.Drawing.Size(1552, 707)
        Me.tbDañados.Text = "Reemplazos"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.grdProductosDañados)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1552, 707)
        Me.PanelControl1.TabIndex = 0
        '
        'grdProductosDañados
        '
        Me.grdProductosDañados.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdProductosDañados.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdProductosDañados.Location = New System.Drawing.Point(2, 2)
        Me.grdProductosDañados.MainView = Me.GridView1
        Me.grdProductosDañados.Margin = New System.Windows.Forms.Padding(4)
        Me.grdProductosDañados.MenuManager = Me.RibbonControl
        Me.grdProductosDañados.Name = "grdProductosDañados"
        Me.grdProductosDañados.Size = New System.Drawing.Size(1548, 703)
        Me.grdProductosDañados.TabIndex = 0
        Me.grdProductosDañados.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.grdProductosDañados
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowFooter = True
        '
        'xtpImpresionPedidos
        '
        Me.xtpImpresionPedidos.Controls.Add(Me.grdImpresionPedidos)
        Me.xtpImpresionPedidos.Name = "xtpImpresionPedidos"
        Me.xtpImpresionPedidos.Size = New System.Drawing.Size(1552, 707)
        Me.xtpImpresionPedidos.Text = "Impresión pedidos"
        '
        'grdImpresionPedidos
        '
        Me.grdImpresionPedidos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdImpresionPedidos.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdImpresionPedidos.Location = New System.Drawing.Point(0, 0)
        Me.grdImpresionPedidos.MainView = Me.grdViewImpresionPedidos
        Me.grdImpresionPedidos.Margin = New System.Windows.Forms.Padding(4)
        Me.grdImpresionPedidos.MenuManager = Me.RibbonControl
        Me.grdImpresionPedidos.Name = "grdImpresionPedidos"
        Me.grdImpresionPedidos.Size = New System.Drawing.Size(1552, 707)
        Me.grdImpresionPedidos.TabIndex = 1
        Me.grdImpresionPedidos.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdViewImpresionPedidos})
        '
        'grdViewImpresionPedidos
        '
        Me.grdViewImpresionPedidos.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdViewImpresionPedidos.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdViewImpresionPedidos.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdViewImpresionPedidos.Appearance.Row.Options.UseFont = True
        Me.grdViewImpresionPedidos.DetailHeight = 431
        Me.grdViewImpresionPedidos.GridControl = Me.grdImpresionPedidos
        Me.grdViewImpresionPedidos.Name = "grdViewImpresionPedidos"
        Me.grdViewImpresionPedidos.OptionsBehavior.ReadOnly = True
        Me.grdViewImpresionPedidos.OptionsFind.AlwaysVisible = True
        Me.grdViewImpresionPedidos.OptionsView.ColumnAutoWidth = False
        Me.grdViewImpresionPedidos.OptionsView.ShowAutoFilterRow = True
        Me.grdViewImpresionPedidos.OptionsView.ShowFooter = True
        '
        'dkPicking
        '
        Me.dkPicking.Form = Me
        Me.dkPicking.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("57483c3a-b463-455e-aad6-9ea419995b74")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 722)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 99)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1362, 122)
        Me.DockPanel1.Text = "Bitácora"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.Fec_agrDateEdit)
        Me.DockPanel1_Container.Controls.Add(Me.User_agrTextEdit)
        Me.DockPanel1_Container.Controls.Add(Fec_agrLabel)
        Me.DockPanel1_Container.Controls.Add(User_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_modTextEdit)
        Me.DockPanel1_Container.Controls.Add(User_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Fec_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_modDateEdit)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 31)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1354, 87)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(169, 47)
        Me.Fec_agrDateEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.Fec_agrDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_agrDateEdit.TabIndex = 5
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(169, 15)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_agrTextEdit.TabIndex = 1
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(653, 15)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 2
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(653, 47)
        Me.Fec_modDateEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.Fec_modDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_modDateEdit.TabIndex = 7
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 834)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1362, 28)
        '
        'BWDatosPicking
        '
        Me.BWDatosPicking.WorkerReportsProgress = True
        '
        'tmrActualizarDatosPicking
        '
        Me.tmrActualizarDatosPicking.Interval = 3000
        '
        'OperadorDragDropEvent
        '
        '
        'pickingUbicDragDropEvent
        '
        '
        'ToastNotificationsManager1
        '
        Me.ToastNotificationsManager1.ApplicationId = "cf3fbf03-d92f-4866-8ad6-96e1f04a2f1d"
        Me.ToastNotificationsManager1.Notifications.AddRange(New DevExpress.XtraBars.ToastNotifications.IToastNotificationProperties() {New DevExpress.XtraBars.ToastNotifications.ToastNotification("658711a9-bc62-4725-9f8a-71f0dbe14299", Nothing, "Nuevo Picking", "Realice la asignación de picking a operador", "", DevExpress.XtraBars.ToastNotifications.ToastNotificationSound.Looping_Alarm, DevExpress.XtraBars.ToastNotifications.ToastNotificationDuration.[Default], DevExpress.XtraBars.ToastNotifications.ToastNotificationTemplate.Text01)})
        '
        'frmPicking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1554, 960)
        Me.Controls.Add(Me.xtpDatosPicking)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmPicking"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Picking"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtpDatosPicking, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtpDatosPicking.ResumeLayout(False)
        Me.XtratabPageDato.ResumeLayout(False)
        CType(Me.GroupControl10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl10.ResumeLayout(False)
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl8.ResumeLayout(False)
        Me.GroupControl8.PerformLayout()
        CType(Me.Fec_modDateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        Me.GroupControl5.PerformLayout()
        Me.groupPrioridad.ResumeLayout(False)
        Me.groupPrioridad.PerformLayout()
        CType(Me.pbAlto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbMedio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbBajo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaPicking.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaPicking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodegas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpAsignacionTransaccion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpAsignacionTransaccion.ResumeLayout(False)
        Me.GrpAsignacionTransaccion.PerformLayout()
        CType(Me.cmbMuelle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdUbicacionMuelle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReferencia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpTarea, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpTarea.ResumeLayout(False)
        CType(Me.grpGaugueProgreso, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpGaugueProgreso.ResumeLayout(False)
        CType(Me.cgProgresoPicking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleBackgroundLayerComponent1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleComponent1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleNeedleComponent1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleSpindleCapComponent1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaTarea.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaTarea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl9.ResumeLayout(False)
        Me.GroupControl9.PerformLayout()
        Me.XtratabPagePedido.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6.ResumeLayout(False)
        CType(Me.dgridPedidos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.dgridDetallePicking, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPageUbicacionPicking.ResumeLayout(False)
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl7.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.GroupControl3.PerformLayout()
        CType(Me.grdOperadorBodega, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsOrdenCompraRecepcionOperador, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgridOperadorBodega, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripPR.ResumeLayout(False)
        Me.ToolStripPR.PerformLayout()
        CType(Me.dgridPickingUbic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdvPickingUbic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbDañados.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.grdProductosDañados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtpImpresionPedidos.ResumeLayout(False)
        CType(Me.grdImpresionPedidos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdViewImpresionPedidos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkPicking, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        CType(Me.behaviorManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToastNotificationsManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents xtpDatosPicking As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtratabPagePedido As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtratabPageDato As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl10 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrpAsignacionTransaccion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtNombreUbicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkUbicacion As System.Windows.Forms.LinkLabel
    Friend WithEvents txtIdUbicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblEstado As System.Windows.Forms.Label
    Friend WithEvents lblC As System.Windows.Forms.Label
    Friend WithEvents GrpTarea As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtmHoraFhh As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtmHoraIhh As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupControl9 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtmHoraF As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtmHoraI As System.Windows.Forms.DateTimePicker
    Friend WithEvents DataBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsOrdenCompraRecepcionOperador As TOMWMS.DsOrdenCompraRecepcionOperador
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarSubItem
    Friend WithEvents cmdListaUbicacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Private WithEvents grdOperadorBodega As DevExpress.XtraGrid.GridControl
    Private WithEvents DgridOperadorBodega As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colSelección As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdOperadorRec As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdOperadorBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOperador As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colcolUsaHH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dgridDetallePicking As DataGridView
    Friend WithEvents lnkVerParametro As LinkLabel
    Friend WithEvents XtraTabPageUbicacionPicking As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl6 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dgridPedidos As DataGridView
    Friend WithEvents GroupControl7 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbAgrupamiento As ComboBox
    Friend WithEvents dkPicking As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents mnuProcesar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RibbonStatusBar1 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbBodegas As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents dtmFechaPicking As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtmFechaTarea As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblRegs1 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RibbonStatusBar2 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents IdPedidoEnc As DataGridViewTextBoxColumn
    Friend WithEvents Codigo As DataGridViewTextBoxColumn
    Friend WithEvents Producto As DataGridViewTextBoxColumn
    Friend WithEvents Presentacion As DataGridViewTextBoxColumn
    Friend WithEvents UnidadMedida As DataGridViewTextBoxColumn
    Friend WithEvents Estado As DataGridViewTextBoxColumn
    Friend WithEvents Cantidad As DataGridViewTextBoxColumn
    Friend WithEvents ClienteDias As DataGridViewTextBoxColumn
    Friend WithEvents CantidadRecibida As DataGridViewTextBoxColumn
    Friend WithEvents OperadorBodega As DataGridViewComboBoxColumn
    Friend WithEvents IdPedidoDet As DataGridViewTextBoxColumn
    Friend WithEvents IdPedido As DataGridViewTextBoxColumn
    Friend WithEvents Referencia As DataGridViewTextBoxColumn
    Friend WithEvents Bodega As DataGridViewTextBoxColumn
    Friend WithEvents Cliente As DataGridViewTextBoxColumn
    Friend WithEvents Propietario As DataGridViewTextBoxColumn
    Friend WithEvents FechaPedido As DataGridViewTextBoxColumn
    Friend WithEvents EstadoP As DataGridViewTextBoxColumn
    Friend WithEvents mnuPendientePicking As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tbDañados As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents grdProductosDañados As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblRegistros As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents ToolStripPR As ToolStrip
    Friend WithEvents cmdSavePR As ToolStripButton
    Friend WithEvents cmdDesactivarPresentacion As ToolStripButton
    Friend WithEvents chkActivo As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkverifica_auto As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkProcesarDesdeBOF As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents chkDetalleOperador As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkEmpaquePorTarima As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkEmpaqueAGranel As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup5 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuPendientePacking As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BWDatosPicking As System.ComponentModel.BackgroundWorker
    Friend WithEvents tmrActualizarDatosPicking As Timer
    Friend WithEvents mnuActualizarPicking As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuDespachado As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuProcesarLinea As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdUbicRes As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdNoPickeado As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdNoVerificado As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdVerificarNuevamente As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents dgridPickingUbic As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdvPickingUbic As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents behaviorManager1 As DevExpress.Utils.Behaviors.BehaviorManager
    Friend WithEvents pickingUbicDragDropEvent As DevExpress.Utils.DragDrop.DragDropEvents
    Friend WithEvents OperadorDragDropEvent As DevExpress.Utils.DragDrop.DragDropEvents
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuRepartirOperadores As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuZonasPicking As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuPorAtributo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents mnuBalancear As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuBalanceoPorLineasPicking As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuBalanceoCantidadLineasPicking As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lnkAgregarPedido As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents RibbonPageGroup4 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuInteligenciaArtificial As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ToastNotificationsManager1 As DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager
    Friend WithEvents mnuAsignacionAutomatica As DevExpress.XtraBars.BarSubItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuEliminarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupControl8 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Fec_modDateEdit1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Fec_agrDateEdit1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit1 As TextBox
    Friend WithEvents User_agrTextEdit1 As TextBox
    Friend WithEvents txtReferencia As DevExpress.XtraEditors.TextEdit
    Friend WithEvents mnuVerificarPickeados As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkFotografiaVerificacion As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents groupPrioridad As GroupBox
    Friend WithEvents rbAlto As RadioButton
    Friend WithEvents rbBajo As RadioButton
    Friend WithEvents rbMedio As RadioButton
    Friend WithEvents pbAlto As PictureBox
    Friend WithEvents pbMedio As PictureBox
    Friend WithEvents pbBajo As PictureBox
    Friend WithEvents grpGaugueProgreso As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GaugeControl1 As DevExpress.XtraGauges.Win.GaugeControl
    Friend WithEvents cgProgresoPicking As DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge
    Private WithEvents ArcScaleBackgroundLayerComponent1 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent
    Private WithEvents ArcScaleComponent1 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent
    Private WithEvents ArcScaleNeedleComponent1 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent
    Private WithEvents ArcScaleSpindleCapComponent1 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents txtIdUbicacionMuelle As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbMuelle As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView12 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtObservacion As TextBox
    Friend WithEvents cmdListaPedidos As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents xtpImpresionPedidos As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grdImpresionPedidos As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdViewImpresionPedidos As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lnkQuitarPedido As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuReemplazo As DevExpress.XtraBars.BarButtonItem
End Class
