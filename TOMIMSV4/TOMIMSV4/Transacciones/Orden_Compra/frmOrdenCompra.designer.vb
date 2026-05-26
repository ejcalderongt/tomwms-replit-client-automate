<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmOrdenCompra
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If gBeOrdenCompra IsNot Nothing Then
                gBeOrdenCompra.Dispose()
                gBeOrdenCompra = Nothing
            End If
            If DTS IsNot Nothing Then
                DTS.Dispose()
                DTS = Nothing
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
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label16 As System.Windows.Forms.Label
        Dim Label15 As System.Windows.Forms.Label
        Dim Label14 As System.Windows.Forms.Label
        Dim Label13 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim Label18 As System.Windows.Forms.Label
        Dim Label17 As System.Windows.Forms.Label
        Dim Label19 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label20 As System.Windows.Forms.Label
        Dim Label21 As System.Windows.Forms.Label
        Dim Label22 As System.Windows.Forms.Label
        Dim lblFechaIngresoTMS As System.Windows.Forms.Label
        Dim lblPilotoTMS As System.Windows.Forms.Label
        Dim lblPlacaTMS As System.Windows.Forms.Label
        Dim Label35 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label34 As System.Windows.Forms.Label
        Dim Label33 As System.Windows.Forms.Label
        Dim Label32 As System.Windows.Forms.Label
        Dim Label28 As System.Windows.Forms.Label
        Dim Label27 As System.Windows.Forms.Label
        Dim Label26 As System.Windows.Forms.Label
        Dim lblRegimen As System.Windows.Forms.Label
        Dim lblPesoNeto As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim lblFechaAceptacion As System.Windows.Forms.Label
        Dim lblTotalOtros As System.Windows.Forms.Label
        Dim lblNoOrden As System.Windows.Forms.Label
        Dim lblTicket As System.Windows.Forms.Label
        Dim lblCodigoPoliza As System.Windows.Forms.Label
        Dim lblTotalSeguroUSD As System.Windows.Forms.Label
        Dim Label25 As System.Windows.Forms.Label
        Dim lblNumeroDUA As System.Windows.Forms.Label
        Dim lblTotalLineas As System.Windows.Forms.Label
        Dim lblFechaDocumento As System.Windows.Forms.Label
        Dim Label29 As System.Windows.Forms.Label
        Dim Label31 As System.Windows.Forms.Label
        Dim lblTotalFOBUSD As System.Windows.Forms.Label
        Dim lblTotalFleteUSD As System.Windows.Forms.Label
        Dim lblTotalPesoBruto As System.Windows.Forms.Label
        Dim lblTotalValorAduana As System.Windows.Forms.Label
        Dim Label36 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOrdenCompra))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grpEncRec = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAsignacion = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdCodigoBarra = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimeCodigoBarra = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprmirCodigoBarra = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdUbicacion = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.SubImprimir = New DevExpress.XtraBars.BarSubItem()
        Me.cmdPreIngreso = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdCostoArancel = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimeBarras = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdRecepcionesAsociadas = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdPreImpresionOC = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdPreImpresionRFID = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.mnuEstadoEnviadoAERP = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCerrarPedidoCompra = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdBackorder = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdActualizarDetalle = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem5 = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImportarExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTareaRecepcion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuExportarExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRegistrarEnNAV = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdCorreccionPoliza = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimirEtiquetasRecepcion = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdDuplicar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdEliminarDocumento = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.RepositoryItemTextEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.RibbonStatusBar1 = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage3 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GrpEnc = New DevExpress.XtraEditors.GroupControl()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.txtComentarios = New System.Windows.Forms.TextBox()
        Me.lblComentarios = New DevExpress.XtraEditors.LabelControl()
        Me.txtNomCampaña = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdCampaña = New DevExpress.XtraEditors.TextEdit()
        Me.lnkCampaña = New System.Windows.Forms.LinkLabel()
        Me.grpUltRec = New DevExpress.XtraEditors.GroupControl()
        Me.txtIdRecepcion = New System.Windows.Forms.LinkLabel()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbOperadorDefecto = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.lblC = New System.Windows.Forms.TextBox()
        Me.chkControlPoliza = New DevExpress.XtraEditors.CheckEdit()
        Me.txtNoDocumento = New System.Windows.Forms.TextBox()
        Me.txtReferencia = New System.Windows.Forms.TextBox()
        Me.txtProcedencia = New System.Windows.Forms.TextBox()
        Me.lblIdOrdenCompra = New DevExpress.XtraEditors.LabelControl()
        Me.txtObservacion = New System.Windows.Forms.TextBox()
        Me.lblFechaDoc = New DevExpress.XtraEditors.LabelControl()
        Me.txtNombreProveedor = New DevExpress.XtraEditors.TextEdit()
        Me.lblBodega = New DevExpress.XtraEditors.LabelControl()
        Me.txtIdProveedor = New DevExpress.XtraEditors.TextEdit()
        Me.lblPropietario = New DevExpress.XtraEditors.LabelControl()
        Me.lnkProveedor = New System.Windows.Forms.LinkLabel()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.lblEstado = New DevExpress.XtraEditors.LabelControl()
        Me.cmbTipoIngreso = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblNoDocumento = New DevExpress.XtraEditors.LabelControl()
        Me.cmbEstado = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblReferencia = New DevExpress.XtraEditors.LabelControl()
        Me.dtmFechaOrdenCompra = New DevExpress.XtraEditors.DateEdit()
        Me.lblProcedencia = New DevExpress.XtraEditors.LabelControl()
        Me.lcmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblObservacion = New DevExpress.XtraEditors.LabelControl()
        Me.cmbDocumentoRef = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblDocumentoRef = New DevExpress.XtraEditors.LabelControl()
        Me.grpDatosERP = New DevExpress.XtraEditors.GroupControl()
        Me.txtUsuarioERP = New System.Windows.Forms.TextBox()
        Me.lblUsuarioERP = New DevExpress.XtraEditors.LabelControl()
        Me.txtCodigoEmpresaERP = New System.Windows.Forms.TextBox()
        Me.lblDocumentoUbicacion = New DevExpress.XtraEditors.LabelControl()
        Me.txtDocumentoUbicacion = New System.Windows.Forms.TextBox()
        Me.lblNoDocumentoRecepcion = New DevExpress.XtraEditors.LabelControl()
        Me.txtNoDocumentoRecepcion = New System.Windows.Forms.TextBox()
        Me.lblSociedadERP = New DevExpress.XtraEditors.LabelControl()
        Me.grpScanPoliza = New DevExpress.XtraEditors.GroupControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.txtScanPoliza = New DevExpress.XtraEditors.TextEdit()
        Me.lbScanPoliza = New DevExpress.XtraEditors.LabelControl()
        Me.grpTMS = New DevExpress.XtraEditors.GroupControl()
        Me.txtTiempoEsperaTMS = New System.Windows.Forms.TextBox()
        Me.txtNoPlacaTMS = New System.Windows.Forms.TextBox()
        Me.lblNoTicketTMS = New DevExpress.XtraEditors.LabelControl()
        Me.txtNombresPilotoTMS = New System.Windows.Forms.TextBox()
        Me.txtFechaIngresoTMS = New System.Windows.Forms.TextBox()
        Me.txtNoTicketTMS = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.grpMotivoDevolucion = New DevExpress.XtraEditors.GroupControl()
        Me.txtNombPedido = New DevExpress.XtraEditors.TextEdit()
        Me.lnkPedido = New System.Windows.Forms.LinkLabel()
        Me.txtIdPedidoDevolucionEnc = New DevExpress.XtraEditors.TextEdit()
        Me.cmbMotivoDevolucion = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblMotivoDevolucion = New DevExpress.XtraEditors.LabelControl()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.GrpDetalle = New DevExpress.XtraEditors.GroupControl()
        Me.prgp = New DevExpress.XtraWaitForm.ProgressPanel()
        Me.DgridDetalleOC = New DevExpress.XtraGrid.GridControl()
        Me.gvDetalleDocIngreso = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.cmdAgregarProducto = New System.Windows.Forms.ToolStripButton()
        Me.cmdEliminarFila = New System.Windows.Forms.ToolStripButton()
        Me.GrpImagen = New DevExpress.XtraEditors.GroupControl()
        Me.PicImg = New System.Windows.Forms.PictureBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.GrdImagen = New DevExpress.XtraGrid.GridControl()
        Me.GridViewImg = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.cmdAdd = New System.Windows.Forms.ToolStripButton()
        Me.cmdDelete = New System.Windows.Forms.ToolStripButton()
        Me.dkOrdenCompra = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.xtraOrdenCompra = New DevExpress.XtraTab.XtraTabControl()
        Me.OrdenCompra = New DevExpress.XtraTab.XtraTabPage()
        Me.Poliza = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GrpPoliza = New DevExpress.XtraEditors.GroupControl()
        Me.cbCBM = New System.Windows.Forms.NumericUpDown()
        Me.txtTotal_general = New DevExpress.XtraEditors.TextEdit()
        Me.txtTotal_liquidar = New DevExpress.XtraEditors.TextEdit()
        Me.txtMod_transporte = New DevExpress.XtraEditors.TextEdit()
        Me.txtClase = New DevExpress.XtraEditors.TextEdit()
        Me.txtNitImpExp = New DevExpress.XtraEditors.TextEdit()
        Me.txtClaveAduana = New DevExpress.XtraEditors.TextEdit()
        Me.cmbRegimen = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtTotalPesoNeto = New System.Windows.Forms.NumericUpDown()
        Me.dtpFechaLlegada = New DevExpress.XtraEditors.DateEdit()
        Me.dtpFechaAceptacion = New DevExpress.XtraEditors.DateEdit()
        Me.txtTotalOtros = New System.Windows.Forms.NumericUpDown()
        Me.txtNumeroOrden = New DevExpress.XtraEditors.TextEdit()
        Me.txtTicket = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigoPoliza = New DevExpress.XtraEditors.TextEdit()
        Me.dtFechaPoliza = New DevExpress.XtraEditors.DateEdit()
        Me.txtValorSeguro = New System.Windows.Forms.NumericUpDown()
        Me.txtTotalBulto = New System.Windows.Forms.NumericUpDown()
        Me.txtTotalLineas = New System.Windows.Forms.NumericUpDown()
        Me.txtTipoCambio = New System.Windows.Forms.NumericUpDown()
        Me.txtTotalFOBUSD = New System.Windows.Forms.NumericUpDown()
        Me.txtValorFlete = New System.Windows.Forms.NumericUpDown()
        Me.txtTotalPesoBruto = New System.Windows.Forms.NumericUpDown()
        Me.txtValorAduana = New System.Windows.Forms.NumericUpDown()
        Me.txtNumeroDUA = New DevExpress.XtraEditors.TextEdit()
        Me.txtNoPoliza = New DevExpress.XtraEditors.TextEdit()
        Me.txtPaisProcedencia = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.dgridDetallePoliza = New DevExpress.XtraGrid.GridControl()
        Me.gvdetallepoliza = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GrpEmbarque = New DevExpress.XtraEditors.GroupControl()
        Me.cmdPrepareGrid = New System.Windows.Forms.Button()
        Me.dtFechaAbordaje = New DevExpress.XtraEditors.DateEdit()
        Me.txtPiezas = New System.Windows.Forms.NumericUpDown()
        Me.txtCantidad = New System.Windows.Forms.NumericUpDown()
        Me.txtCBM = New System.Windows.Forms.NumericUpDown()
        Me.txtPesoKgs = New System.Windows.Forms.NumericUpDown()
        Me.txtDestinatario = New DevExpress.XtraEditors.TextEdit()
        Me.txtViaje = New DevExpress.XtraEditors.TextEdit()
        Me.txtPONumber = New DevExpress.XtraEditors.TextEdit()
        Me.txtDireccion = New DevExpress.XtraEditors.TextEdit()
        Me.txtBuque = New DevExpress.XtraEditors.TextEdit()
        Me.Descripcion = New DevExpress.XtraEditors.TextEdit()
        Me.txtRemitente = New DevExpress.XtraEditors.TextEdit()
        Me.BLNo = New DevExpress.XtraEditors.TextEdit()
        Me.txtPuertaDescarga = New DevExpress.XtraEditors.TextEdit()
        Me.DetalleOC = New DevExpress.XtraTab.XtraTabPage()
        Me.tabDetalleServicios = New DevExpress.XtraTab.XtraTabPage()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.cmdEliminarServicio = New System.Windows.Forms.ToolStripButton()
        Me.dgridServiciosAsociados = New DevExpress.XtraGrid.GridControl()
        Me.gvDetalleServicios = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cmbAcuerdoComercial = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblAcuerdoComercial = New DevExpress.XtraEditors.LabelControl()
        Me.Enc_RecOC = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.grdEncRec = New DevExpress.XtraGrid.GridControl()
        Me.EncabezadoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsOC = New DsOC()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colCódigo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNoOC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNoDocumento = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colFecha = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colEstado = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTipoTransacción = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDescripción = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMuelle = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Imagenes = New DevExpress.XtraTab.XtraTabPage()
        Me.tabLotes = New DevExpress.XtraTab.XtraTabPage()
        Me.DgridLotes = New DevExpress.XtraGrid.GridControl()
        Me.gridviewLotes = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabDetERP = New DevExpress.XtraTab.XtraTabPage()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.grdDetERP = New DevExpress.XtraGrid.GridControl()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabPedidosDevolucion = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.dgridPedidos = New System.Windows.Forms.DataGridView()
        Me.IdPedido = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Referencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Bodega = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cliente = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Propietario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaPedido = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EstadoP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tabPolizaCorregida = New DevExpress.XtraTab.XtraTabPage()
        Me.DgridPolizas = New DevExpress.XtraGrid.GridControl()
        Me.gridViewPolizas = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabTallaColor = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridTallaColor = New DevExpress.XtraGrid.GridControl()
        Me.GridView7 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.DetalleBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cmdImportar = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem6 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem7 = New DevExpress.XtraBars.BarButtonItem()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label16 = New System.Windows.Forms.Label()
        Label15 = New System.Windows.Forms.Label()
        Label14 = New System.Windows.Forms.Label()
        Label13 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        Label18 = New System.Windows.Forms.Label()
        Label17 = New System.Windows.Forms.Label()
        Label19 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label20 = New System.Windows.Forms.Label()
        Label21 = New System.Windows.Forms.Label()
        Label22 = New System.Windows.Forms.Label()
        lblFechaIngresoTMS = New System.Windows.Forms.Label()
        lblPilotoTMS = New System.Windows.Forms.Label()
        lblPlacaTMS = New System.Windows.Forms.Label()
        Label35 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label34 = New System.Windows.Forms.Label()
        Label33 = New System.Windows.Forms.Label()
        Label32 = New System.Windows.Forms.Label()
        Label28 = New System.Windows.Forms.Label()
        Label27 = New System.Windows.Forms.Label()
        Label26 = New System.Windows.Forms.Label()
        lblRegimen = New System.Windows.Forms.Label()
        lblPesoNeto = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        lblFechaAceptacion = New System.Windows.Forms.Label()
        lblTotalOtros = New System.Windows.Forms.Label()
        lblNoOrden = New System.Windows.Forms.Label()
        lblTicket = New System.Windows.Forms.Label()
        lblCodigoPoliza = New System.Windows.Forms.Label()
        lblTotalSeguroUSD = New System.Windows.Forms.Label()
        Label25 = New System.Windows.Forms.Label()
        lblNumeroDUA = New System.Windows.Forms.Label()
        lblTotalLineas = New System.Windows.Forms.Label()
        lblFechaDocumento = New System.Windows.Forms.Label()
        Label29 = New System.Windows.Forms.Label()
        Label31 = New System.Windows.Forms.Label()
        lblTotalFOBUSD = New System.Windows.Forms.Label()
        lblTotalFleteUSD = New System.Windows.Forms.Label()
        lblTotalPesoBruto = New System.Windows.Forms.Label()
        lblTotalValorAduana = New System.Windows.Forms.Label()
        Label36 = New System.Windows.Forms.Label()
        CType(Me.grpEncRec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpEnc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpEnc.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.txtNomCampaña.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdCampaña.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpUltRec, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpUltRec.SuspendLayout()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbOperadorDefecto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkControlPoliza.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreProveedor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdProveedor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTipoIngreso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaOrdenCompra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaOrdenCompra.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lcmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbDocumentoRef.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpDatosERP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDatosERP.SuspendLayout()
        CType(Me.grpScanPoliza, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpScanPoliza.SuspendLayout()
        CType(Me.txtScanPoliza.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpTMS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTMS.SuspendLayout()
        CType(Me.txtNoTicketTMS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpMotivoDevolucion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMotivoDevolucion.SuspendLayout()
        CType(Me.txtNombPedido.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdPedidoDevolucionEnc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbMotivoDevolucion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpDetalle.SuspendLayout()
        CType(Me.DgridDetalleOC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetalleDocIngreso, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.GrpImagen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpImagen.SuspendLayout()
        CType(Me.PicImg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.GrdImagen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewImg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip.SuspendLayout()
        CType(Me.dkOrdenCompra, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtraOrdenCompra, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtraOrdenCompra.SuspendLayout()
        Me.OrdenCompra.SuspendLayout()
        Me.Poliza.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.GrpPoliza, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpPoliza.SuspendLayout()
        CType(Me.cbCBM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotal_general.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotal_liquidar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMod_transporte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtClase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNitImpExp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtClaveAduana.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbRegimen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalPesoNeto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaLlegada.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaLlegada.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaAceptacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaAceptacion.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalOtros, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNumeroOrden.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTicket.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoPoliza.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFechaPoliza.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFechaPoliza.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtValorSeguro, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalBulto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalLineas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTipoCambio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalFOBUSD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtValorFlete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalPesoBruto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtValorAduana, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNumeroDUA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoPoliza.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPaisProcedencia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.dgridDetallePoliza, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvdetallepoliza, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpEmbarque, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpEmbarque.SuspendLayout()
        CType(Me.dtFechaAbordaje.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFechaAbordaje.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPiezas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCBM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPesoKgs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDestinatario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtViaje.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPONumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDireccion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBuque.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Descripcion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemitente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BLNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPuertaDescarga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DetalleOC.SuspendLayout()
        Me.tabDetalleServicios.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.dgridServiciosAsociados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetalleServicios, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbAcuerdoComercial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Enc_RecOC.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.grdEncRec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EncabezadoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsOC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Imagenes.SuspendLayout()
        Me.tabLotes.SuspendLayout()
        CType(Me.DgridLotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridviewLotes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabDetERP.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.grdDetERP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPedidosDevolucion.SuspendLayout()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.dgridPedidos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPolizaCorregida.SuspendLayout()
        CType(Me.DgridPolizas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridViewPolizas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabTallaColor.SuspendLayout()
        CType(Me.dgridTallaColor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DetalleBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(192, 18)
        User_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(100, 16)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(192, 50)
        Fec_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(668, 18)
        User_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 2
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(677, 50)
        Fec_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label8.Location = New System.Drawing.Point(42, 95)
        Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(44, 16)
        Label8.TabIndex = 0
        Label8.Text = "BL No:"
        '
        'Label16
        '
        Label16.AutoSize = True
        Label16.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label16.Location = New System.Drawing.Point(42, 175)
        Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label16.Name = "Label16"
        Label16.Size = New System.Drawing.Size(70, 16)
        Label16.TabIndex = 8
        Label16.Text = "Remitente:"
        '
        'Label15
        '
        Label15.AutoSize = True
        Label15.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label15.Location = New System.Drawing.Point(42, 218)
        Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label15.Name = "Label15"
        Label15.Size = New System.Drawing.Size(102, 16)
        Label15.TabIndex = 12
        Label15.Text = "Fecha Abordaje:"
        '
        'Label14
        '
        Label14.AutoSize = True
        Label14.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label14.Location = New System.Drawing.Point(42, 258)
        Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label14.Name = "Label14"
        Label14.Size = New System.Drawing.Size(77, 16)
        Label14.TabIndex = 16
        Label14.Text = "Descripción:"
        '
        'Label13
        '
        Label13.AutoSize = True
        Label13.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label13.Location = New System.Drawing.Point(42, 298)
        Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label13.Name = "Label13"
        Label13.Size = New System.Drawing.Size(62, 16)
        Label13.TabIndex = 20
        Label13.Text = "Cantidad:"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label9.Location = New System.Drawing.Point(42, 135)
        Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(106, 16)
        Label9.TabIndex = 4
        Label9.Text = "Puerta Descarga:"
        '
        'Label11
        '
        Label11.AutoSize = True
        Label11.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label11.Location = New System.Drawing.Point(42, 338)
        Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(96, 16)
        Label11.TabIndex = 24
        Label11.Text = "Peso Total Kgs:"
        '
        'Label18
        '
        Label18.AutoSize = True
        Label18.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label18.Location = New System.Drawing.Point(380, 176)
        Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label18.Name = "Label18"
        Label18.Size = New System.Drawing.Size(80, 16)
        Label18.TabIndex = 10
        Label18.Text = "Destinatario:"
        '
        'Label17
        '
        Label17.AutoSize = True
        Label17.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label17.Location = New System.Drawing.Point(380, 135)
        Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label17.Name = "Label17"
        Label17.Size = New System.Drawing.Size(47, 16)
        Label17.TabIndex = 6
        Label17.Text = "Buque:"
        '
        'Label19
        '
        Label19.AutoSize = True
        Label19.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label19.Location = New System.Drawing.Point(380, 219)
        Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label19.Name = "Label19"
        Label19.Size = New System.Drawing.Size(64, 16)
        Label19.TabIndex = 14
        Label19.Text = "Dirección:"
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label10.Location = New System.Drawing.Point(380, 96)
        Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(54, 16)
        Label10.TabIndex = 2
        Label10.Text = "Viaje #:"
        '
        'Label20
        '
        Label20.AutoSize = True
        Label20.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label20.Location = New System.Drawing.Point(380, 258)
        Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label20.Name = "Label20"
        Label20.Size = New System.Drawing.Size(77, 16)
        Label20.TabIndex = 18
        Label20.Text = "PO Number:"
        '
        'Label21
        '
        Label21.AutoSize = True
        Label21.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label21.Location = New System.Drawing.Point(380, 299)
        Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label21.Name = "Label21"
        Label21.Size = New System.Drawing.Size(48, 16)
        Label21.TabIndex = 21
        Label21.Text = "Piezas:"
        '
        'Label22
        '
        Label22.AutoSize = True
        Label22.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label22.Location = New System.Drawing.Point(380, 338)
        Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label22.Name = "Label22"
        Label22.Size = New System.Drawing.Size(37, 16)
        Label22.TabIndex = 26
        Label22.Text = "CBM:"
        '
        'lblFechaIngresoTMS
        '
        lblFechaIngresoTMS.AutoSize = True
        lblFechaIngresoTMS.Location = New System.Drawing.Point(66, 91)
        lblFechaIngresoTMS.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblFechaIngresoTMS.Name = "lblFechaIngresoTMS"
        lblFechaIngresoTMS.Size = New System.Drawing.Size(55, 16)
        lblFechaIngresoTMS.TabIndex = 2
        lblFechaIngresoTMS.Text = "Ingresó:"
        '
        'lblPilotoTMS
        '
        lblPilotoTMS.AutoSize = True
        lblPilotoTMS.Location = New System.Drawing.Point(66, 126)
        lblPilotoTMS.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblPilotoTMS.Name = "lblPilotoTMS"
        lblPilotoTMS.Size = New System.Drawing.Size(43, 16)
        lblPilotoTMS.TabIndex = 4
        lblPilotoTMS.Text = "Piloto:"
        '
        'lblPlacaTMS
        '
        lblPlacaTMS.AutoSize = True
        lblPlacaTMS.Location = New System.Drawing.Point(66, 158)
        lblPlacaTMS.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblPlacaTMS.Name = "lblPlacaTMS"
        lblPlacaTMS.Size = New System.Drawing.Size(55, 16)
        lblPlacaTMS.TabIndex = 6
        lblPlacaTMS.Text = "Placa #:"
        '
        'Label35
        '
        Label35.AutoSize = True
        Label35.Location = New System.Drawing.Point(66, 190)
        Label35.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label35.Name = "Label35"
        Label35.Size = New System.Drawing.Size(31, 16)
        Label35.TabIndex = 8
        Label35.Text = "T.E:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(30, 258)
        Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(46, 18)
        Label1.TabIndex = 269
        Label1.Text = "CBM:"
        '
        'Label34
        '
        Label34.AutoSize = True
        Label34.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label34.Location = New System.Drawing.Point(1276, 192)
        Label34.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label34.Name = "Label34"
        Label34.Size = New System.Drawing.Size(97, 18)
        Label34.TabIndex = 267
        Label34.Text = "Total general:"
        '
        'Label33
        '
        Label33.AutoSize = True
        Label33.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label33.Location = New System.Drawing.Point(1276, 151)
        Label33.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label33.Name = "Label33"
        Label33.Size = New System.Drawing.Size(99, 18)
        Label33.TabIndex = 265
        Label33.Text = "Total_liquidar:"
        '
        'Label32
        '
        Label32.AutoSize = True
        Label32.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label32.Location = New System.Drawing.Point(1276, 117)
        Label32.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label32.Name = "Label32"
        Label32.Size = New System.Drawing.Size(113, 18)
        Label32.TabIndex = 263
        Label32.Text = "Mod transporte:"
        '
        'Label28
        '
        Label28.AutoSize = True
        Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label28.Location = New System.Drawing.Point(1276, 78)
        Label28.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label28.Name = "Label28"
        Label28.Size = New System.Drawing.Size(50, 18)
        Label28.TabIndex = 261
        Label28.Text = "Clase:"
        '
        'Label27
        '
        Label27.AutoSize = True
        Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label27.Location = New System.Drawing.Point(1276, 42)
        Label27.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label27.Name = "Label27"
        Label27.Size = New System.Drawing.Size(128, 18)
        Label27.TabIndex = 259
        Label27.Text = "NIT Import/Export:"
        '
        'Label26
        '
        Label26.AutoSize = True
        Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label26.Location = New System.Drawing.Point(846, 122)
        Label26.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label26.Name = "Label26"
        Label26.Size = New System.Drawing.Size(101, 18)
        Label26.TabIndex = 247
        Label26.Text = "Clave aduana:"
        '
        'lblRegimen
        '
        lblRegimen.AutoSize = True
        lblRegimen.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblRegimen.Location = New System.Drawing.Point(846, 212)
        lblRegimen.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblRegimen.Name = "lblRegimen"
        lblRegimen.Size = New System.Drawing.Size(71, 18)
        lblRegimen.TabIndex = 250
        lblRegimen.Text = "Régimen:"
        '
        'lblPesoNeto
        '
        lblPesoNeto.AutoSize = True
        lblPesoNeto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblPesoNeto.Location = New System.Drawing.Point(846, 41)
        lblPesoNeto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblPesoNeto.Name = "lblPesoNeto"
        lblPesoNeto.Size = New System.Drawing.Size(146, 18)
        lblPesoNeto.TabIndex = 243
        lblPesoNeto.Text = "Total Peso Neto KG:"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label12.Location = New System.Drawing.Point(433, 126)
        Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(108, 18)
        Label12.TabIndex = 231
        Label12.Text = "Fecha Llegada:"
        '
        'lblFechaAceptacion
        '
        lblFechaAceptacion.AutoSize = True
        lblFechaAceptacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblFechaAceptacion.Location = New System.Drawing.Point(433, 85)
        lblFechaAceptacion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblFechaAceptacion.Name = "lblFechaAceptacion"
        lblFechaAceptacion.Size = New System.Drawing.Size(130, 18)
        lblFechaAceptacion.TabIndex = 229
        lblFechaAceptacion.Text = "Fecha Aceptación:"
        '
        'lblTotalOtros
        '
        lblTotalOtros.AutoSize = True
        lblTotalOtros.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTotalOtros.Location = New System.Drawing.Point(433, 250)
        lblTotalOtros.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblTotalOtros.Name = "lblTotalOtros"
        lblTotalOtros.Size = New System.Drawing.Size(87, 18)
        lblTotalOtros.TabIndex = 237
        lblTotalOtros.Text = "Total Otros:"
        '
        'lblNoOrden
        '
        lblNoOrden.AutoSize = True
        lblNoOrden.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblNoOrden.Location = New System.Drawing.Point(30, 160)
        lblNoOrden.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblNoOrden.Name = "lblNoOrden"
        lblNoOrden.Size = New System.Drawing.Size(111, 18)
        lblNoOrden.TabIndex = 223
        lblNoOrden.Text = "Número Orden:"
        '
        'lblTicket
        '
        lblTicket.AutoSize = True
        lblTicket.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTicket.Location = New System.Drawing.Point(30, 124)
        lblTicket.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblTicket.Name = "lblTicket"
        lblTicket.Size = New System.Drawing.Size(52, 18)
        lblTicket.TabIndex = 221
        lblTicket.Text = "Ticket:"
        '
        'lblCodigoPoliza
        '
        lblCodigoPoliza.AutoSize = True
        lblCodigoPoliza.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblCodigoPoliza.Location = New System.Drawing.Point(30, 86)
        lblCodigoPoliza.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCodigoPoliza.Name = "lblCodigoPoliza"
        lblCodigoPoliza.Size = New System.Drawing.Size(105, 18)
        lblCodigoPoliza.TabIndex = 219
        lblCodigoPoliza.Text = "Código Poliza:"
        '
        'lblTotalSeguroUSD
        '
        lblTotalSeguroUSD.AutoSize = True
        lblTotalSeguroUSD.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTotalSeguroUSD.Location = New System.Drawing.Point(433, 166)
        lblTotalSeguroUSD.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblTotalSeguroUSD.Name = "lblTotalSeguroUSD"
        lblTotalSeguroUSD.Size = New System.Drawing.Size(133, 18)
        lblTotalSeguroUSD.TabIndex = 233
        lblTotalSeguroUSD.Text = "Total Seguro USD:"
        '
        'Label25
        '
        Label25.AutoSize = True
        Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label25.Location = New System.Drawing.Point(846, 302)
        Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label25.Name = "Label25"
        Label25.Size = New System.Drawing.Size(91, 18)
        Label25.TabIndex = 254
        Label25.Text = "Total Bultos:"
        '
        'lblNumeroDUA
        '
        lblNumeroDUA.AutoSize = True
        lblNumeroDUA.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblNumeroDUA.Location = New System.Drawing.Point(30, 207)
        lblNumeroDUA.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblNumeroDUA.Name = "lblNumeroDUA"
        lblNumeroDUA.Size = New System.Drawing.Size(101, 18)
        lblNumeroDUA.TabIndex = 225
        lblNumeroDUA.Text = "Número DUA:"
        '
        'lblTotalLineas
        '
        lblTotalLineas.AutoSize = True
        lblTotalLineas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTotalLineas.Location = New System.Drawing.Point(846, 252)
        lblTotalLineas.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblTotalLineas.Name = "lblTotalLineas"
        lblTotalLineas.Size = New System.Drawing.Size(92, 18)
        lblTotalLineas.TabIndex = 252
        lblTotalLineas.Text = "Total Líneas:"
        '
        'lblFechaDocumento
        '
        lblFechaDocumento.AutoSize = True
        lblFechaDocumento.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblFechaDocumento.Location = New System.Drawing.Point(433, 44)
        lblFechaDocumento.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblFechaDocumento.Name = "lblFechaDocumento"
        lblFechaDocumento.Size = New System.Drawing.Size(135, 18)
        lblFechaDocumento.TabIndex = 227
        lblFechaDocumento.Text = "Fecha Documento:"
        '
        'Label29
        '
        Label29.AutoSize = True
        Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label29.Location = New System.Drawing.Point(846, 81)
        Label29.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label29.Name = "Label29"
        Label29.Size = New System.Drawing.Size(97, 18)
        Label29.TabIndex = 245
        Label29.Text = "Tipo Cambio:"
        '
        'Label31
        '
        Label31.AutoSize = True
        Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label31.Location = New System.Drawing.Point(846, 166)
        Label31.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label31.Name = "Label31"
        Label31.Size = New System.Drawing.Size(128, 18)
        Label31.TabIndex = 248
        Label31.Text = "País Procedencia:"
        '
        'lblTotalFOBUSD
        '
        lblTotalFOBUSD.AutoSize = True
        lblTotalFOBUSD.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTotalFOBUSD.Location = New System.Drawing.Point(846, 345)
        lblTotalFOBUSD.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblTotalFOBUSD.Name = "lblTotalFOBUSD"
        lblTotalFOBUSD.Size = New System.Drawing.Size(116, 18)
        lblTotalFOBUSD.TabIndex = 256
        lblTotalFOBUSD.Text = "Total FOB USD:"
        '
        'lblTotalFleteUSD
        '
        lblTotalFleteUSD.AutoSize = True
        lblTotalFleteUSD.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTotalFleteUSD.Location = New System.Drawing.Point(433, 298)
        lblTotalFleteUSD.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblTotalFleteUSD.Name = "lblTotalFleteUSD"
        lblTotalFleteUSD.Size = New System.Drawing.Size(117, 18)
        lblTotalFleteUSD.TabIndex = 239
        lblTotalFleteUSD.Text = "Total Flete USD:"
        '
        'lblTotalPesoBruto
        '
        lblTotalPesoBruto.AutoSize = True
        lblTotalPesoBruto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTotalPesoBruto.Location = New System.Drawing.Point(433, 342)
        lblTotalPesoBruto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblTotalPesoBruto.Name = "lblTotalPesoBruto"
        lblTotalPesoBruto.Size = New System.Drawing.Size(150, 18)
        lblTotalPesoBruto.TabIndex = 241
        lblTotalPesoBruto.Text = "Total Peso Bruto KG:"
        '
        'lblTotalValorAduana
        '
        lblTotalValorAduana.AutoSize = True
        lblTotalValorAduana.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTotalValorAduana.Location = New System.Drawing.Point(433, 209)
        lblTotalValorAduana.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblTotalValorAduana.Name = "lblTotalValorAduana"
        lblTotalValorAduana.Size = New System.Drawing.Size(136, 18)
        lblTotalValorAduana.TabIndex = 235
        lblTotalValorAduana.Text = "Total Valor Aduana:"
        '
        'Label36
        '
        Label36.AutoSize = True
        Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label36.Location = New System.Drawing.Point(30, 53)
        Label36.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label36.Name = "Label36"
        Label36.Size = New System.Drawing.Size(65, 18)
        Label36.TabIndex = 218
        Label36.Text = "No. T.O:"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 964)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.grpEncRec
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1632, 30)
        '
        'grpEncRec
        '
        Me.grpEncRec.ExpandCollapseItem.Id = 0
        Me.grpEncRec.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.grpEncRec.ExpandCollapseItem, Me.mnuGuardar, Me.mnuAsignacion, Me.BarButtonItem1, Me.BarButtonItem2, Me.BarButtonItem3, Me.cmdCodigoBarra, Me.cmdImprimeCodigoBarra, Me.cmdImprmirCodigoBarra, Me.cmdActualizar, Me.cmdEliminar, Me.cmdUbicacion, Me.cmdImprimir, Me.SubImprimir, Me.cmdPreIngreso, Me.cmdCostoArancel, Me.cmdImprimeBarras, Me.lblRegs, Me.BarButtonItem4, Me.mnuEstadoEnviadoAERP, Me.cmdRecepcionesAsociadas, Me.mnuCerrarPedidoCompra, Me.cmdBackorder, Me.cmdActualizarDetalle, Me.BarButtonItem5, Me.cmdImportarExcel, Me.mnuTareaRecepcion, Me.mnuExportarExcel, Me.mnuEliminarLayoutGrid, Me.mnuRegistrarEnNAV, Me.cmdCorreccionPoliza, Me.cmdImprimirEtiquetasRecepcion, Me.cmdDuplicar, Me.cmdEliminarDocumento, Me.cmdPreImpresionOC, Me.cmdPreImpresionRFID})
        Me.grpEncRec.Location = New System.Drawing.Point(0, 0)
        Me.grpEncRec.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grpEncRec.MaxItemId = 42
        Me.grpEncRec.Name = "grpEncRec"
        Me.grpEncRec.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.grpEncRec.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit1, Me.RepositoryItemTextEdit2})
        Me.grpEncRec.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.grpEncRec.Size = New System.Drawing.Size(1632, 193)
        Me.grpEncRec.StatusBar = Me.RibbonStatusBar1
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
        Me.mnuGuardar.Name = "mnuGuardar"
        Me.mnuGuardar.ShortcutKeyDisplayString = "G"
        '
        'mnuAsignacion
        '
        Me.mnuAsignacion.Caption = "Asignacion"
        Me.mnuAsignacion.Id = 4
        Me.mnuAsignacion.ImageOptions.Image = CType(resources.GetObject("mnuAsignacion.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuAsignacion.ImageOptions.LargeImage = CType(resources.GetObject("mnuAsignacion.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuAsignacion.Name = "mnuAsignacion"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Estadística"
        Me.BarButtonItem1.Id = 5
        Me.BarButtonItem1.ImageOptions.Image = CType(resources.GetObject("BarButtonItem1.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem1.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Movimientos"
        Me.BarButtonItem2.Id = 6
        Me.BarButtonItem2.ImageOptions.Image = CType(resources.GetObject("BarButtonItem2.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem2.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem2.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "BarButtonItem3"
        Me.BarButtonItem3.Id = 7
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'cmdCodigoBarra
        '
        Me.cmdCodigoBarra.Caption = "Códigos de barra"
        Me.cmdCodigoBarra.Id = 8
        Me.cmdCodigoBarra.ImageOptions.Image = CType(resources.GetObject("cmdCodigoBarra.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdCodigoBarra.ImageOptions.LargeImage = CType(resources.GetObject("cmdCodigoBarra.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdCodigoBarra.Name = "cmdCodigoBarra"
        '
        'cmdImprimeCodigoBarra
        '
        Me.cmdImprimeCodigoBarra.Caption = "Imprimir Código Barra"
        Me.cmdImprimeCodigoBarra.Id = 9
        Me.cmdImprimeCodigoBarra.ImageOptions.Image = CType(resources.GetObject("cmdImprimeCodigoBarra.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdImprimeCodigoBarra.ImageOptions.LargeImage = CType(resources.GetObject("cmdImprimeCodigoBarra.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdImprimeCodigoBarra.Name = "cmdImprimeCodigoBarra"
        '
        'cmdImprmirCodigoBarra
        '
        Me.cmdImprmirCodigoBarra.Caption = "Código Barra"
        Me.cmdImprmirCodigoBarra.Id = 10
        Me.cmdImprmirCodigoBarra.ImageOptions.Image = CType(resources.GetObject("cmdImprmirCodigoBarra.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdImprmirCodigoBarra.ImageOptions.LargeImage = CType(resources.GetObject("cmdImprmirCodigoBarra.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdImprmirCodigoBarra.Name = "cmdImprmirCodigoBarra"
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Caption = "Actualizar"
        Me.cmdActualizar.Id = 11
        Me.cmdActualizar.ImageOptions.SvgImage = CType(resources.GetObject("cmdActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizar.Name = "cmdActualizar"
        '
        'cmdEliminar
        '
        Me.cmdEliminar.Caption = "Anular"
        Me.cmdEliminar.Id = 12
        Me.cmdEliminar.ImageOptions.SvgImage = CType(resources.GetObject("cmdEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdEliminar.Name = "cmdEliminar"
        '
        'cmdUbicacion
        '
        Me.cmdUbicacion.Caption = "Ubicación"
        Me.cmdUbicacion.Id = 13
        Me.cmdUbicacion.ImageOptions.Image = CType(resources.GetObject("cmdUbicacion.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdUbicacion.ImageOptions.LargeImage = CType(resources.GetObject("cmdUbicacion.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdUbicacion.Name = "cmdUbicacion"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 14
        Me.cmdImprimir.ImageOptions.Image = CType(resources.GetObject("cmdImprimir.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdImprimir.ImageOptions.LargeImage = CType(resources.GetObject("cmdImprimir.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'SubImprimir
        '
        Me.SubImprimir.Caption = "Imprimir"
        Me.SubImprimir.Id = 15
        Me.SubImprimir.ImageOptions.SvgImage = CType(resources.GetObject("SubImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.SubImprimir.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdPreIngreso), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdCostoArancel), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdImprimeBarras), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem4), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdRecepcionesAsociadas), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdPreImpresionOC), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdPreImpresionRFID)})
        Me.SubImprimir.Name = "SubImprimir"
        '
        'cmdPreIngreso
        '
        Me.cmdPreIngreso.Caption = "Orden Compra Pre Ingreso"
        Me.cmdPreIngreso.Id = 16
        Me.cmdPreIngreso.Name = "cmdPreIngreso"
        '
        'cmdCostoArancel
        '
        Me.cmdCostoArancel.Caption = "Costo y Arancel"
        Me.cmdCostoArancel.Id = 17
        Me.cmdCostoArancel.Name = "cmdCostoArancel"
        '
        'cmdImprimeBarras
        '
        Me.cmdImprimeBarras.Caption = "Códigos de Barra"
        Me.cmdImprimeBarras.Id = 18
        Me.cmdImprimeBarras.Name = "cmdImprimeBarras"
        '
        'BarButtonItem4
        '
        Me.BarButtonItem4.Caption = "Imprimir Recepción"
        Me.BarButtonItem4.Id = 20
        Me.BarButtonItem4.Name = "BarButtonItem4"
        '
        'cmdRecepcionesAsociadas
        '
        Me.cmdRecepcionesAsociadas.Caption = "Imprimir Recepciones Asociadas"
        Me.cmdRecepcionesAsociadas.Id = 22
        Me.cmdRecepcionesAsociadas.Name = "cmdRecepcionesAsociadas"
        '
        'cmdPreImpresionOC
        '
        Me.cmdPreImpresionOC.Caption = "Preimpresión etiquetas"
        Me.cmdPreImpresionOC.Id = 40
        Me.cmdPreImpresionOC.Name = "cmdPreImpresionOC"
        '
        'cmdPreImpresionRFID
        '
        Me.cmdPreImpresionRFID.Caption = "Preimpresión RFID"
        Me.cmdPreImpresionRFID.Id = 41
        Me.cmdPreImpresionRFID.Name = "cmdPreImpresionRFID"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 19
        Me.lblRegs.Name = "lblRegs"
        '
        'mnuEstadoEnviadoAERP
        '
        Me.mnuEstadoEnviadoAERP.Caption = "No enviado"
        Me.mnuEstadoEnviadoAERP.Id = 21
        Me.mnuEstadoEnviadoAERP.ImageOptions.SvgImage = CType(resources.GetObject("mnuEstadoEnviadoAERP.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEstadoEnviadoAERP.Name = "mnuEstadoEnviadoAERP"
        '
        'mnuCerrarPedidoCompra
        '
        Me.mnuCerrarPedidoCompra.Caption = "Cerrar pedido de ingreso"
        Me.mnuCerrarPedidoCompra.Id = 23
        Me.mnuCerrarPedidoCompra.ImageOptions.SvgImage = CType(resources.GetObject("mnuCerrarPedidoCompra.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuCerrarPedidoCompra.Name = "mnuCerrarPedidoCompra"
        '
        'cmdBackorder
        '
        Me.cmdBackorder.Caption = "Back-order"
        Me.cmdBackorder.Enabled = False
        Me.cmdBackorder.Id = 25
        Me.cmdBackorder.ImageOptions.SvgImage = CType(resources.GetObject("cmdBackorder.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdBackorder.Name = "cmdBackorder"
        '
        'cmdActualizarDetalle
        '
        Me.cmdActualizarDetalle.Caption = "Actualizar Detalle"
        Me.cmdActualizarDetalle.Id = 26
        Me.cmdActualizarDetalle.ImageOptions.SvgImage = CType(resources.GetObject("cmdActualizarDetalle.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizarDetalle.Name = "cmdActualizarDetalle"
        '
        'BarButtonItem5
        '
        Me.BarButtonItem5.Caption = "BarButtonItem5"
        Me.BarButtonItem5.Id = 28
        Me.BarButtonItem5.Name = "BarButtonItem5"
        '
        'cmdImportarExcel
        '
        Me.cmdImportarExcel.Caption = "Importar Excel"
        Me.cmdImportarExcel.Id = 31
        Me.cmdImportarExcel.ImageOptions.SvgImage = CType(resources.GetObject("cmdImportarExcel.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImportarExcel.Name = "cmdImportarExcel"
        '
        'mnuTareaRecepcion
        '
        Me.mnuTareaRecepcion.Caption = "Crear tarea recepción"
        Me.mnuTareaRecepcion.Id = 32
        Me.mnuTareaRecepcion.ImageOptions.SvgImage = CType(resources.GetObject("mnuTareaRecepcion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuTareaRecepcion.Name = "mnuTareaRecepcion"
        '
        'mnuExportarExcel
        '
        Me.mnuExportarExcel.Caption = "Exportar Detalle Excel"
        Me.mnuExportarExcel.Id = 33
        Me.mnuExportarExcel.ImageOptions.SvgImage = CType(resources.GetObject("mnuExportarExcel.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuExportarExcel.Name = "mnuExportarExcel"
        '
        'mnuEliminarLayoutGrid
        '
        Me.mnuEliminarLayoutGrid.Caption = "Eliminar diseño grid"
        Me.mnuEliminarLayoutGrid.Id = 34
        Me.mnuEliminarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminarLayoutGrid.Name = "mnuEliminarLayoutGrid"
        '
        'mnuRegistrarEnNAV
        '
        Me.mnuRegistrarEnNAV.Caption = "Registrar en ERP"
        Me.mnuRegistrarEnNAV.Id = 35
        Me.mnuRegistrarEnNAV.ImageOptions.SvgImage = CType(resources.GetObject("mnuRegistrarEnNAV.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuRegistrarEnNAV.Name = "mnuRegistrarEnNAV"
        '
        'cmdCorreccionPoliza
        '
        Me.cmdCorreccionPoliza.Caption = "Corrección de póliza en ingreso"
        Me.cmdCorreccionPoliza.Id = 36
        Me.cmdCorreccionPoliza.ImageOptions.SvgImage = CType(resources.GetObject("cmdCorreccionPoliza.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdCorreccionPoliza.Name = "cmdCorreccionPoliza"
        '
        'cmdImprimirEtiquetasRecepcion
        '
        Me.cmdImprimirEtiquetasRecepcion.Caption = "Imprimir etiquetas recepción"
        Me.cmdImprimirEtiquetasRecepcion.Id = 37
        Me.cmdImprimirEtiquetasRecepcion.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimirEtiquetasRecepcion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimirEtiquetasRecepcion.Name = "cmdImprimirEtiquetasRecepcion"
        Me.cmdImprimirEtiquetasRecepcion.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'cmdDuplicar
        '
        Me.cmdDuplicar.Caption = "Duplicar"
        Me.cmdDuplicar.Id = 38
        Me.cmdDuplicar.ImageOptions.SvgImage = CType(resources.GetObject("cmdDuplicar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdDuplicar.Name = "cmdDuplicar"
        '
        'cmdEliminarDocumento
        '
        Me.cmdEliminarDocumento.Caption = "Eliminar documento"
        Me.cmdEliminarDocumento.Id = 39
        Me.cmdEliminarDocumento.ImageOptions.SvgImage = CType(resources.GetObject("cmdEliminarDocumento.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdEliminarDocumento.Name = "cmdEliminarDocumento"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menú Documento de Ingreso consolidado"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdEliminar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.SubImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEstadoEnviadoAERP)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuCerrarPedidoCompra)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdBackorder)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizarDetalle)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImportarExcel)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuExportarExcel)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuTareaRecepcion)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminarLayoutGrid)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuRegistrarEnNAV)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdCorreccionPoliza, "2.1.1.2")
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimirEtiquetasRecepcion)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdDuplicar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdEliminarDocumento)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'RepositoryItemTextEdit2
        '
        Me.RepositoryItemTextEdit2.AutoHeight = False
        Me.RepositoryItemTextEdit2.Name = "RepositoryItemTextEdit2"
        '
        'RibbonStatusBar1
        '
        Me.RibbonStatusBar1.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar1.Location = New System.Drawing.Point(2, 680)
        Me.RibbonStatusBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RibbonStatusBar1.Name = "RibbonStatusBar1"
        Me.RibbonStatusBar1.Ribbon = Me.grpEncRec
        Me.RibbonStatusBar1.Size = New System.Drawing.Size(1626, 33)
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(295, 47)
        Me.Fec_agrDateEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.Fec_agrDateEdit.MenuManager = Me.grpEncRec
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_agrDateEdit.TabIndex = 5
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(295, 15)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_agrTextEdit.MenuManager = Me.grpEncRec
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_agrTextEdit.TabIndex = 1
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(779, 47)
        Me.Fec_modDateEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.Fec_modDateEdit.MenuManager = Me.grpEncRec
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_modDateEdit.TabIndex = 7
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(779, 15)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_modTextEdit.MenuManager = Me.grpEncRec
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 3
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.Image = CType(resources.GetObject("mnuEliminar.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuEliminar.ImageOptions.LargeImage = CType(resources.GetObject("mnuEliminar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuEliminar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'RibbonPage3
        '
        Me.RibbonPage3.Name = "RibbonPage3"
        Me.RibbonPage3.Text = "Opción"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'GridView1
        '
        Me.GridView1.Name = "GridView1"
        '
        'GridView2
        '
        Me.GridView2.Name = "GridView2"
        '
        'GridView3
        '
        Me.GridView3.Name = "GridView3"
        '
        'GridView4
        '
        Me.GridView4.Name = "GridView4"
        '
        'GrpEnc
        '
        Me.GrpEnc.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.GrpEnc.Appearance.Options.UseBackColor = True
        Me.GrpEnc.AppearanceCaption.BackColor = System.Drawing.Color.BlueViolet
        Me.GrpEnc.AppearanceCaption.BackColor2 = System.Drawing.Color.White
        Me.GrpEnc.AppearanceCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.GrpEnc.AppearanceCaption.Options.UseBackColor = True
        Me.GrpEnc.AppearanceCaption.Options.UseTextOptions = True
        Me.GrpEnc.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GrpEnc.Controls.Add(Me.SplitContainer2)
        Me.GrpEnc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpEnc.Location = New System.Drawing.Point(0, 0)
        Me.GrpEnc.LookAndFeel.SkinName = "Office 2019 Colorful"
        Me.GrpEnc.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003
        Me.GrpEnc.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GrpEnc.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GrpEnc.Name = "GrpEnc"
        Me.GrpEnc.ScrollBarSmallChange = 10
        Me.GrpEnc.Size = New System.Drawing.Size(1630, 715)
        Me.GrpEnc.TabIndex = 0
        Me.GrpEnc.Text = "Time: 0"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(2, 21)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtComentarios)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblComentarios)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtNomCampaña)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtIdCampaña)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lnkCampaña)
        Me.SplitContainer2.Panel1.Controls.Add(Me.grpUltRec)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmbBodega)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmbOperadorDefecto)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LabelControl6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblC)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkControlPoliza)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtNoDocumento)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtReferencia)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtProcedencia)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblIdOrdenCompra)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtObservacion)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblFechaDoc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtNombreProveedor)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBodega)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtIdProveedor)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblPropietario)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lnkProveedor)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LabelControl3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblEstado)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmbTipoIngreso)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblNoDocumento)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmbEstado)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblReferencia)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtmFechaOrdenCompra)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblProcedencia)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lcmbPropietario)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblObservacion)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmbDocumentoRef)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDocumentoRef)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.grpDatosERP)
        Me.SplitContainer2.Panel2.Controls.Add(Me.grpScanPoliza)
        Me.SplitContainer2.Panel2.Controls.Add(Me.grpTMS)
        Me.SplitContainer2.Panel2.Controls.Add(Me.grpMotivoDevolucion)
        Me.SplitContainer2.Panel2.Controls.Add(Me.chkActivo)
        Me.SplitContainer2.Size = New System.Drawing.Size(1626, 692)
        Me.SplitContainer2.SplitterDistance = 866
        Me.SplitContainer2.SplitterWidth = 5
        Me.SplitContainer2.TabIndex = 0
        '
        'txtComentarios
        '
        Me.txtComentarios.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComentarios.Location = New System.Drawing.Point(130, 503)
        Me.txtComentarios.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtComentarios.Multiline = True
        Me.txtComentarios.Name = "txtComentarios"
        Me.txtComentarios.Size = New System.Drawing.Size(374, 60)
        Me.txtComentarios.TabIndex = 42
        '
        'lblComentarios
        '
        Me.lblComentarios.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblComentarios.Appearance.Options.UseFont = True
        Me.lblComentarios.Location = New System.Drawing.Point(18, 506)
        Me.lblComentarios.Margin = New System.Windows.Forms.Padding(4)
        Me.lblComentarios.Name = "lblComentarios"
        Me.lblComentarios.Size = New System.Drawing.Size(77, 16)
        Me.lblComentarios.TabIndex = 41
        Me.lblComentarios.Text = "Comentarios:"
        '
        'txtNomCampaña
        '
        Me.txtNomCampaña.Location = New System.Drawing.Point(232, 401)
        Me.txtNomCampaña.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNomCampaña.MenuManager = Me.grpEncRec
        Me.txtNomCampaña.Name = "txtNomCampaña"
        Me.txtNomCampaña.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtNomCampaña.Properties.ReadOnly = True
        Me.txtNomCampaña.Size = New System.Drawing.Size(273, 22)
        Me.txtNomCampaña.TabIndex = 40
        '
        'txtIdCampaña
        '
        Me.txtIdCampaña.Location = New System.Drawing.Point(130, 401)
        Me.txtIdCampaña.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdCampaña.MenuManager = Me.grpEncRec
        Me.txtIdCampaña.Name = "txtIdCampaña"
        Me.txtIdCampaña.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtIdCampaña.Properties.MaskSettings.Set("mask", "n0")
        Me.txtIdCampaña.Size = New System.Drawing.Size(94, 22)
        Me.txtIdCampaña.TabIndex = 39
        '
        'lnkCampaña
        '
        Me.lnkCampaña.AutoSize = True
        Me.lnkCampaña.Location = New System.Drawing.Point(15, 403)
        Me.lnkCampaña.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkCampaña.Name = "lnkCampaña"
        Me.lnkCampaña.Size = New System.Drawing.Size(66, 16)
        Me.lnkCampaña.TabIndex = 38
        Me.lnkCampaña.TabStop = True
        Me.lnkCampaña.Text = "Campaña:"
        '
        'grpUltRec
        '
        Me.grpUltRec.AppearanceCaption.BackColor = System.Drawing.Color.SteelBlue
        Me.grpUltRec.AppearanceCaption.Options.UseBackColor = True
        Me.grpUltRec.AppearanceCaption.Options.UseTextOptions = True
        Me.grpUltRec.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.grpUltRec.Controls.Add(Me.txtIdRecepcion)
        Me.grpUltRec.Location = New System.Drawing.Point(625, 21)
        Me.grpUltRec.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.grpUltRec.Name = "grpUltRec"
        Me.grpUltRec.Size = New System.Drawing.Size(172, 81)
        Me.grpUltRec.TabIndex = 32
        Me.grpUltRec.Text = "Ult. Recepción #"
        '
        'txtIdRecepcion
        '
        Me.txtIdRecepcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdRecepcion.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdRecepcion.Location = New System.Drawing.Point(35, 33)
        Me.txtIdRecepcion.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.txtIdRecepcion.Name = "txtIdRecepcion"
        Me.txtIdRecepcion.Size = New System.Drawing.Size(95, 36)
        Me.txtIdRecepcion.TabIndex = 8
        Me.txtIdRecepcion.TabStop = True
        Me.txtIdRecepcion.Text = "0"
        Me.txtIdRecepcion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(130, 82)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbBodega.MenuManager = Me.grpEncRec
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Size = New System.Drawing.Size(374, 22)
        Me.cmbBodega.TabIndex = 30
        '
        'cmbOperadorDefecto
        '
        Me.cmbOperadorDefecto.Location = New System.Drawing.Point(131, 176)
        Me.cmbOperadorDefecto.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbOperadorDefecto.MenuManager = Me.grpEncRec
        Me.cmbOperadorDefecto.Name = "cmbOperadorDefecto"
        Me.cmbOperadorDefecto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbOperadorDefecto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOperadorDefecto.Properties.NullText = ""
        Me.cmbOperadorDefecto.Size = New System.Drawing.Size(374, 22)
        Me.cmbOperadorDefecto.TabIndex = 12
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Location = New System.Drawing.Point(18, 180)
        Me.LabelControl6.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(106, 16)
        Me.LabelControl6.TabIndex = 11
        Me.LabelControl6.Text = "Operador Defecto:"
        '
        'lblC
        '
        Me.lblC.BackColor = System.Drawing.Color.Lavender
        Me.lblC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblC.Location = New System.Drawing.Point(131, 18)
        Me.lblC.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lblC.Name = "lblC"
        Me.lblC.ReadOnly = True
        Me.lblC.Size = New System.Drawing.Size(374, 23)
        Me.lblC.TabIndex = 1
        '
        'chkControlPoliza
        '
        Me.chkControlPoliza.Enabled = False
        Me.chkControlPoliza.Location = New System.Drawing.Point(518, 209)
        Me.chkControlPoliza.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkControlPoliza.MenuManager = Me.grpEncRec
        Me.chkControlPoliza.Name = "chkControlPoliza"
        Me.chkControlPoliza.Properties.Caption = "Control Poliza"
        Me.chkControlPoliza.Size = New System.Drawing.Size(139, 24)
        Me.chkControlPoliza.TabIndex = 15
        '
        'txtNoDocumento
        '
        Me.txtNoDocumento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoDocumento.Location = New System.Drawing.Point(131, 274)
        Me.txtNoDocumento.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNoDocumento.Name = "txtNoDocumento"
        Me.txtNoDocumento.ReadOnly = True
        Me.txtNoDocumento.Size = New System.Drawing.Size(374, 23)
        Me.txtNoDocumento.TabIndex = 19
        Me.txtNoDocumento.Text = "OC"
        '
        'txtReferencia
        '
        Me.txtReferencia.AcceptsReturn = True
        Me.txtReferencia.BackColor = System.Drawing.Color.White
        Me.txtReferencia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReferencia.Location = New System.Drawing.Point(131, 306)
        Me.txtReferencia.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtReferencia.Name = "txtReferencia"
        Me.txtReferencia.Size = New System.Drawing.Size(374, 23)
        Me.txtReferencia.TabIndex = 21
        '
        'txtProcedencia
        '
        Me.txtProcedencia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProcedencia.Location = New System.Drawing.Point(131, 338)
        Me.txtProcedencia.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtProcedencia.Name = "txtProcedencia"
        Me.txtProcedencia.Size = New System.Drawing.Size(374, 23)
        Me.txtProcedencia.TabIndex = 23
        '
        'lblIdOrdenCompra
        '
        Me.lblIdOrdenCompra.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblIdOrdenCompra.Appearance.Options.UseFont = True
        Me.lblIdOrdenCompra.Location = New System.Drawing.Point(18, 20)
        Me.lblIdOrdenCompra.Margin = New System.Windows.Forms.Padding(4)
        Me.lblIdOrdenCompra.Name = "lblIdOrdenCompra"
        Me.lblIdOrdenCompra.Size = New System.Drawing.Size(44, 16)
        Me.lblIdOrdenCompra.TabIndex = 0
        Me.lblIdOrdenCompra.Text = "Código:"
        '
        'txtObservacion
        '
        Me.txtObservacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtObservacion.Location = New System.Drawing.Point(130, 433)
        Me.txtObservacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtObservacion.Multiline = True
        Me.txtObservacion.Name = "txtObservacion"
        Me.txtObservacion.Size = New System.Drawing.Size(374, 60)
        Me.txtObservacion.TabIndex = 27
        '
        'lblFechaDoc
        '
        Me.lblFechaDoc.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblFechaDoc.Appearance.Options.UseFont = True
        Me.lblFechaDoc.Location = New System.Drawing.Point(18, 55)
        Me.lblFechaDoc.Margin = New System.Windows.Forms.Padding(4)
        Me.lblFechaDoc.Name = "lblFechaDoc"
        Me.lblFechaDoc.Size = New System.Drawing.Size(39, 16)
        Me.lblFechaDoc.TabIndex = 2
        Me.lblFechaDoc.Text = "Fecha:"
        '
        'txtNombreProveedor
        '
        Me.txtNombreProveedor.Location = New System.Drawing.Point(231, 144)
        Me.txtNombreProveedor.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNombreProveedor.MenuManager = Me.grpEncRec
        Me.txtNombreProveedor.Name = "txtNombreProveedor"
        Me.txtNombreProveedor.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtNombreProveedor.Properties.ReadOnly = True
        Me.txtNombreProveedor.Size = New System.Drawing.Size(273, 22)
        Me.txtNombreProveedor.TabIndex = 10
        '
        'lblBodega
        '
        Me.lblBodega.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblBodega.Appearance.Options.UseFont = True
        Me.lblBodega.Location = New System.Drawing.Point(18, 86)
        Me.lblBodega.Margin = New System.Windows.Forms.Padding(4)
        Me.lblBodega.Name = "lblBodega"
        Me.lblBodega.Size = New System.Drawing.Size(47, 16)
        Me.lblBodega.TabIndex = 4
        Me.lblBodega.Text = "Bodega:"
        '
        'txtIdProveedor
        '
        Me.txtIdProveedor.Location = New System.Drawing.Point(131, 144)
        Me.txtIdProveedor.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdProveedor.MenuManager = Me.grpEncRec
        Me.txtIdProveedor.Name = "txtIdProveedor"
        Me.txtIdProveedor.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtIdProveedor.Properties.MaskSettings.Set("mask", "n0")
        Me.txtIdProveedor.Size = New System.Drawing.Size(94, 22)
        Me.txtIdProveedor.TabIndex = 9
        '
        'lblPropietario
        '
        Me.lblPropietario.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblPropietario.Appearance.Options.UseFont = True
        Me.lblPropietario.Location = New System.Drawing.Point(18, 117)
        Me.lblPropietario.Margin = New System.Windows.Forms.Padding(4)
        Me.lblPropietario.Name = "lblPropietario"
        Me.lblPropietario.Size = New System.Drawing.Size(67, 16)
        Me.lblPropietario.TabIndex = 6
        Me.lblPropietario.Text = "Propietario:"
        '
        'lnkProveedor
        '
        Me.lnkProveedor.AutoSize = True
        Me.lnkProveedor.Location = New System.Drawing.Point(14, 146)
        Me.lnkProveedor.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkProveedor.Name = "lnkProveedor"
        Me.lnkProveedor.Size = New System.Drawing.Size(70, 16)
        Me.lnkProveedor.TabIndex = 8
        Me.lnkProveedor.TabStop = True
        Me.lnkProveedor.Text = "Proveedor:"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(18, 212)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(98, 16)
        Me.LabelControl3.TabIndex = 13
        Me.LabelControl3.Text = "Tipo Documento:"
        '
        'lblEstado
        '
        Me.lblEstado.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblEstado.Appearance.Options.UseFont = True
        Me.lblEstado.Location = New System.Drawing.Point(18, 244)
        Me.lblEstado.Margin = New System.Windows.Forms.Padding(4)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(43, 16)
        Me.lblEstado.TabIndex = 16
        Me.lblEstado.Text = "Estado:"
        '
        'cmbTipoIngreso
        '
        Me.cmbTipoIngreso.Location = New System.Drawing.Point(131, 208)
        Me.cmbTipoIngreso.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbTipoIngreso.MenuManager = Me.grpEncRec
        Me.cmbTipoIngreso.Name = "cmbTipoIngreso"
        Me.cmbTipoIngreso.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbTipoIngreso.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoIngreso.Properties.NullText = ""
        Me.cmbTipoIngreso.Size = New System.Drawing.Size(374, 22)
        Me.cmbTipoIngreso.TabIndex = 14
        '
        'lblNoDocumento
        '
        Me.lblNoDocumento.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblNoDocumento.Appearance.Options.UseFont = True
        Me.lblNoDocumento.Location = New System.Drawing.Point(18, 277)
        Me.lblNoDocumento.Margin = New System.Windows.Forms.Padding(4)
        Me.lblNoDocumento.Name = "lblNoDocumento"
        Me.lblNoDocumento.Size = New System.Drawing.Size(92, 16)
        Me.lblNoDocumento.TabIndex = 18
        Me.lblNoDocumento.Text = "No. Documento:"
        '
        'cmbEstado
        '
        Me.cmbEstado.Enabled = False
        Me.cmbEstado.Location = New System.Drawing.Point(130, 240)
        Me.cmbEstado.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbEstado.MenuManager = Me.grpEncRec
        Me.cmbEstado.Name = "cmbEstado"
        Me.cmbEstado.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbEstado.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEstado.Properties.NullText = ""
        Me.cmbEstado.Size = New System.Drawing.Size(374, 22)
        Me.cmbEstado.TabIndex = 17
        '
        'lblReferencia
        '
        Me.lblReferencia.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblReferencia.Appearance.Options.UseFont = True
        Me.lblReferencia.Location = New System.Drawing.Point(18, 309)
        Me.lblReferencia.Margin = New System.Windows.Forms.Padding(4)
        Me.lblReferencia.Name = "lblReferencia"
        Me.lblReferencia.Size = New System.Drawing.Size(66, 16)
        Me.lblReferencia.TabIndex = 20
        Me.lblReferencia.Text = "Referencia:"
        '
        'dtmFechaOrdenCompra
        '
        Me.dtmFechaOrdenCompra.EditValue = New Date(2017, 11, 20, 10, 7, 9, 549)
        Me.dtmFechaOrdenCompra.Location = New System.Drawing.Point(131, 50)
        Me.dtmFechaOrdenCompra.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtmFechaOrdenCompra.MenuManager = Me.grpEncRec
        Me.dtmFechaOrdenCompra.Name = "dtmFechaOrdenCompra"
        Me.dtmFechaOrdenCompra.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.dtmFechaOrdenCompra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaOrdenCompra.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaOrdenCompra.Size = New System.Drawing.Size(374, 22)
        Me.dtmFechaOrdenCompra.TabIndex = 3
        '
        'lblProcedencia
        '
        Me.lblProcedencia.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblProcedencia.Appearance.Options.UseFont = True
        Me.lblProcedencia.Location = New System.Drawing.Point(18, 342)
        Me.lblProcedencia.Margin = New System.Windows.Forms.Padding(4)
        Me.lblProcedencia.Name = "lblProcedencia"
        Me.lblProcedencia.Size = New System.Drawing.Size(74, 16)
        Me.lblProcedencia.TabIndex = 22
        Me.lblProcedencia.Text = "Procedencia:"
        '
        'lcmbPropietario
        '
        Me.lcmbPropietario.Location = New System.Drawing.Point(131, 113)
        Me.lcmbPropietario.Margin = New System.Windows.Forms.Padding(4)
        Me.lcmbPropietario.MenuManager = Me.grpEncRec
        Me.lcmbPropietario.Name = "lcmbPropietario"
        Me.lcmbPropietario.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lcmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lcmbPropietario.Size = New System.Drawing.Size(374, 22)
        Me.lcmbPropietario.TabIndex = 7
        '
        'lblObservacion
        '
        Me.lblObservacion.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblObservacion.Appearance.Options.UseFont = True
        Me.lblObservacion.Location = New System.Drawing.Point(18, 436)
        Me.lblObservacion.Margin = New System.Windows.Forms.Padding(4)
        Me.lblObservacion.Name = "lblObservacion"
        Me.lblObservacion.Size = New System.Drawing.Size(75, 16)
        Me.lblObservacion.TabIndex = 26
        Me.lblObservacion.Text = "Observación:"
        '
        'cmbDocumentoRef
        '
        Me.cmbDocumentoRef.Enabled = False
        Me.cmbDocumentoRef.Location = New System.Drawing.Point(130, 370)
        Me.cmbDocumentoRef.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbDocumentoRef.MenuManager = Me.grpEncRec
        Me.cmbDocumentoRef.Name = "cmbDocumentoRef"
        Me.cmbDocumentoRef.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbDocumentoRef.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbDocumentoRef.Properties.NullText = ""
        Me.cmbDocumentoRef.Size = New System.Drawing.Size(374, 22)
        Me.cmbDocumentoRef.TabIndex = 25
        '
        'lblDocumentoRef
        '
        Me.lblDocumentoRef.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblDocumentoRef.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblDocumentoRef.Appearance.Options.UseFont = True
        Me.lblDocumentoRef.Appearance.Options.UseForeColor = True
        Me.lblDocumentoRef.Location = New System.Drawing.Point(18, 375)
        Me.lblDocumentoRef.Margin = New System.Windows.Forms.Padding(4)
        Me.lblDocumentoRef.Name = "lblDocumentoRef"
        Me.lblDocumentoRef.Size = New System.Drawing.Size(92, 16)
        Me.lblDocumentoRef.TabIndex = 24
        Me.lblDocumentoRef.Text = "Documento Ref:"
        '
        'grpDatosERP
        '
        Me.grpDatosERP.Controls.Add(Me.txtUsuarioERP)
        Me.grpDatosERP.Controls.Add(Me.lblUsuarioERP)
        Me.grpDatosERP.Controls.Add(Me.txtCodigoEmpresaERP)
        Me.grpDatosERP.Controls.Add(Me.lblDocumentoUbicacion)
        Me.grpDatosERP.Controls.Add(Me.txtDocumentoUbicacion)
        Me.grpDatosERP.Controls.Add(Me.lblNoDocumentoRecepcion)
        Me.grpDatosERP.Controls.Add(Me.txtNoDocumentoRecepcion)
        Me.grpDatosERP.Controls.Add(Me.lblSociedadERP)
        Me.grpDatosERP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpDatosERP.Location = New System.Drawing.Point(0, 493)
        Me.grpDatosERP.Name = "grpDatosERP"
        Me.grpDatosERP.Size = New System.Drawing.Size(755, 199)
        Me.grpDatosERP.TabIndex = 4
        Me.grpDatosERP.Text = "Datos ERP"
        '
        'txtUsuarioERP
        '
        Me.txtUsuarioERP.AcceptsReturn = True
        Me.txtUsuarioERP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUsuarioERP.Location = New System.Drawing.Point(20, 116)
        Me.txtUsuarioERP.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUsuarioERP.Name = "txtUsuarioERP"
        Me.txtUsuarioERP.ReadOnly = True
        Me.txtUsuarioERP.Size = New System.Drawing.Size(214, 23)
        Me.txtUsuarioERP.TabIndex = 39
        '
        'lblUsuarioERP
        '
        Me.lblUsuarioERP.Appearance.Options.UseFont = True
        Me.lblUsuarioERP.Location = New System.Drawing.Point(20, -4092)
        Me.lblUsuarioERP.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.lblUsuarioERP.Name = "lblUsuarioERP"
        Me.lblUsuarioERP.Size = New System.Drawing.Size(48, 16)
        Me.lblUsuarioERP.TabIndex = 38
        Me.lblUsuarioERP.Text = "Usuario:"
        '
        'txtCodigoEmpresaERP
        '
        Me.txtCodigoEmpresaERP.AcceptsReturn = True
        Me.txtCodigoEmpresaERP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCodigoEmpresaERP.Location = New System.Drawing.Point(20, 67)
        Me.txtCodigoEmpresaERP.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodigoEmpresaERP.Name = "txtCodigoEmpresaERP"
        Me.txtCodigoEmpresaERP.ReadOnly = True
        Me.txtCodigoEmpresaERP.Size = New System.Drawing.Size(214, 23)
        Me.txtCodigoEmpresaERP.TabIndex = 37
        '
        'lblDocumentoUbicacion
        '
        Me.lblDocumentoUbicacion.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblDocumentoUbicacion.Appearance.Options.UseFont = True
        Me.lblDocumentoUbicacion.Location = New System.Drawing.Point(253, -4140)
        Me.lblDocumentoUbicacion.Margin = New System.Windows.Forms.Padding(4)
        Me.lblDocumentoUbicacion.Name = "lblDocumentoUbicacion"
        Me.lblDocumentoUbicacion.Size = New System.Drawing.Size(145, 16)
        Me.lblDocumentoUbicacion.TabIndex = 28
        Me.lblDocumentoUbicacion.Text = "Documento de Ubicación:"
        '
        'txtDocumentoUbicacion
        '
        Me.txtDocumentoUbicacion.AcceptsReturn = True
        Me.txtDocumentoUbicacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDocumentoUbicacion.Location = New System.Drawing.Point(253, 67)
        Me.txtDocumentoUbicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDocumentoUbicacion.Name = "txtDocumentoUbicacion"
        Me.txtDocumentoUbicacion.ReadOnly = True
        Me.txtDocumentoUbicacion.Size = New System.Drawing.Size(214, 23)
        Me.txtDocumentoUbicacion.TabIndex = 29
        '
        'lblNoDocumentoRecepcion
        '
        Me.lblNoDocumentoRecepcion.Appearance.Options.UseFont = True
        Me.lblNoDocumentoRecepcion.Location = New System.Drawing.Point(486, -4140)
        Me.lblNoDocumentoRecepcion.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.lblNoDocumentoRecepcion.Name = "lblNoDocumentoRecepcion"
        Me.lblNoDocumentoRecepcion.Size = New System.Drawing.Size(149, 16)
        Me.lblNoDocumentoRecepcion.TabIndex = 34
        Me.lblNoDocumentoRecepcion.Text = "Documento de Recepción:"
        '
        'txtNoDocumentoRecepcion
        '
        Me.txtNoDocumentoRecepcion.AcceptsReturn = True
        Me.txtNoDocumentoRecepcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoDocumentoRecepcion.Location = New System.Drawing.Point(486, 67)
        Me.txtNoDocumentoRecepcion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNoDocumentoRecepcion.Name = "txtNoDocumentoRecepcion"
        Me.txtNoDocumentoRecepcion.ReadOnly = True
        Me.txtNoDocumentoRecepcion.Size = New System.Drawing.Size(214, 23)
        Me.txtNoDocumentoRecepcion.TabIndex = 35
        '
        'lblSociedadERP
        '
        Me.lblSociedadERP.Appearance.Options.UseFont = True
        Me.lblSociedadERP.Location = New System.Drawing.Point(20, -4141)
        Me.lblSociedadERP.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.lblSociedadERP.Name = "lblSociedadERP"
        Me.lblSociedadERP.Size = New System.Drawing.Size(57, 16)
        Me.lblSociedadERP.TabIndex = 36
        Me.lblSociedadERP.Text = "Sociedad:"
        '
        'grpScanPoliza
        '
        Me.grpScanPoliza.CaptionImageOptions.Image = CType(resources.GetObject("grpScanPoliza.CaptionImageOptions.Image"), System.Drawing.Image)
        Me.grpScanPoliza.Controls.Add(Me.LabelControl4)
        Me.grpScanPoliza.Controls.Add(Me.txtScanPoliza)
        Me.grpScanPoliza.Controls.Add(Me.lbScanPoliza)
        Me.grpScanPoliza.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpScanPoliza.Location = New System.Drawing.Point(0, 392)
        Me.grpScanPoliza.Margin = New System.Windows.Forms.Padding(4)
        Me.grpScanPoliza.Name = "grpScanPoliza"
        Me.grpScanPoliza.Size = New System.Drawing.Size(755, 101)
        Me.grpScanPoliza.TabIndex = 0
        Me.grpScanPoliza.Text = "Escanéo de Poliza"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Location = New System.Drawing.Point(71, 59)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(89, 16)
        Me.LabelControl4.TabIndex = 0
        Me.LabelControl4.Text = "Escanée Poliza:"
        '
        'txtScanPoliza
        '
        Me.txtScanPoliza.Location = New System.Drawing.Point(214, 54)
        Me.txtScanPoliza.Margin = New System.Windows.Forms.Padding(4)
        Me.txtScanPoliza.MenuManager = Me.grpEncRec
        Me.txtScanPoliza.Name = "txtScanPoliza"
        Me.txtScanPoliza.Properties.Appearance.BackColor = System.Drawing.Color.Silver
        Me.txtScanPoliza.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScanPoliza.Properties.Appearance.Options.UseBackColor = True
        Me.txtScanPoliza.Properties.Appearance.Options.UseFont = True
        Me.txtScanPoliza.Size = New System.Drawing.Size(354, 24)
        Me.txtScanPoliza.TabIndex = 1
        '
        'lbScanPoliza
        '
        Me.lbScanPoliza.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbScanPoliza.Appearance.Image = CType(resources.GetObject("lbScanPoliza.Appearance.Image"), System.Drawing.Image)
        Me.lbScanPoliza.Appearance.Options.UseFont = True
        Me.lbScanPoliza.Appearance.Options.UseImage = True
        Me.lbScanPoliza.ImageOptions.Image = CType(resources.GetObject("lbScanPoliza.ImageOptions.Image"), System.Drawing.Image)
        Me.lbScanPoliza.Location = New System.Drawing.Point(589, 48)
        Me.lbScanPoliza.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lbScanPoliza.Name = "lbScanPoliza"
        Me.lbScanPoliza.Size = New System.Drawing.Size(32, 32)
        Me.lbScanPoliza.TabIndex = 2
        '
        'grpTMS
        '
        Me.grpTMS.CaptionImageOptions.Image = CType(resources.GetObject("grpTMS.CaptionImageOptions.Image"), System.Drawing.Image)
        Me.grpTMS.Controls.Add(Me.txtTiempoEsperaTMS)
        Me.grpTMS.Controls.Add(Label35)
        Me.grpTMS.Controls.Add(Me.txtNoPlacaTMS)
        Me.grpTMS.Controls.Add(Me.lblNoTicketTMS)
        Me.grpTMS.Controls.Add(lblPlacaTMS)
        Me.grpTMS.Controls.Add(Me.txtNombresPilotoTMS)
        Me.grpTMS.Controls.Add(lblPilotoTMS)
        Me.grpTMS.Controls.Add(Me.txtFechaIngresoTMS)
        Me.grpTMS.Controls.Add(lblFechaIngresoTMS)
        Me.grpTMS.Controls.Add(Me.txtNoTicketTMS)
        Me.grpTMS.Controls.Add(Me.LabelControl2)
        Me.grpTMS.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpTMS.Location = New System.Drawing.Point(0, 162)
        Me.grpTMS.Margin = New System.Windows.Forms.Padding(4)
        Me.grpTMS.Name = "grpTMS"
        Me.grpTMS.Size = New System.Drawing.Size(755, 230)
        Me.grpTMS.TabIndex = 3
        Me.grpTMS.Text = "TMS Tikcet Info"
        '
        'txtTiempoEsperaTMS
        '
        Me.txtTiempoEsperaTMS.AcceptsReturn = True
        Me.txtTiempoEsperaTMS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTiempoEsperaTMS.Location = New System.Drawing.Point(211, 190)
        Me.txtTiempoEsperaTMS.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTiempoEsperaTMS.Name = "txtTiempoEsperaTMS"
        Me.txtTiempoEsperaTMS.ReadOnly = True
        Me.txtTiempoEsperaTMS.Size = New System.Drawing.Size(356, 23)
        Me.txtTiempoEsperaTMS.TabIndex = 9
        '
        'txtNoPlacaTMS
        '
        Me.txtNoPlacaTMS.AcceptsReturn = True
        Me.txtNoPlacaTMS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoPlacaTMS.Location = New System.Drawing.Point(211, 158)
        Me.txtNoPlacaTMS.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNoPlacaTMS.Name = "txtNoPlacaTMS"
        Me.txtNoPlacaTMS.ReadOnly = True
        Me.txtNoPlacaTMS.Size = New System.Drawing.Size(356, 23)
        Me.txtNoPlacaTMS.TabIndex = 7
        '
        'lblNoTicketTMS
        '
        Me.lblNoTicketTMS.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblNoTicketTMS.Appearance.Options.UseFont = True
        Me.lblNoTicketTMS.Location = New System.Drawing.Point(66, 59)
        Me.lblNoTicketTMS.Margin = New System.Windows.Forms.Padding(4)
        Me.lblNoTicketTMS.Name = "lblNoTicketTMS"
        Me.lblNoTicketTMS.Size = New System.Drawing.Size(92, 16)
        Me.lblNoTicketTMS.TabIndex = 0
        Me.lblNoTicketTMS.Text = "No. Ticket TMS:"
        '
        'txtNombresPilotoTMS
        '
        Me.txtNombresPilotoTMS.AcceptsReturn = True
        Me.txtNombresPilotoTMS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNombresPilotoTMS.Location = New System.Drawing.Point(211, 126)
        Me.txtNombresPilotoTMS.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNombresPilotoTMS.Name = "txtNombresPilotoTMS"
        Me.txtNombresPilotoTMS.ReadOnly = True
        Me.txtNombresPilotoTMS.Size = New System.Drawing.Size(356, 23)
        Me.txtNombresPilotoTMS.TabIndex = 5
        '
        'txtFechaIngresoTMS
        '
        Me.txtFechaIngresoTMS.AcceptsReturn = True
        Me.txtFechaIngresoTMS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFechaIngresoTMS.Location = New System.Drawing.Point(211, 91)
        Me.txtFechaIngresoTMS.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFechaIngresoTMS.Name = "txtFechaIngresoTMS"
        Me.txtFechaIngresoTMS.ReadOnly = True
        Me.txtFechaIngresoTMS.Size = New System.Drawing.Size(356, 23)
        Me.txtFechaIngresoTMS.TabIndex = 3
        '
        'txtNoTicketTMS
        '
        Me.txtNoTicketTMS.Location = New System.Drawing.Point(211, 59)
        Me.txtNoTicketTMS.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNoTicketTMS.MenuManager = Me.grpEncRec
        Me.txtNoTicketTMS.Name = "txtNoTicketTMS"
        Me.txtNoTicketTMS.Properties.Appearance.BackColor = System.Drawing.Color.Silver
        Me.txtNoTicketTMS.Properties.Appearance.Options.UseBackColor = True
        Me.txtNoTicketTMS.Size = New System.Drawing.Size(356, 22)
        Me.txtNoTicketTMS.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Appearance.Image = CType(resources.GetObject("LabelControl2.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseImage = True
        Me.LabelControl2.ImageOptions.Image = CType(resources.GetObject("LabelControl2.ImageOptions.Image"), System.Drawing.Image)
        Me.LabelControl2.Location = New System.Drawing.Point(594, 50)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(32, 32)
        Me.LabelControl2.TabIndex = 37
        '
        'grpMotivoDevolucion
        '
        Me.grpMotivoDevolucion.CaptionImageOptions.Image = CType(resources.GetObject("grpMotivoDevolucion.CaptionImageOptions.Image"), System.Drawing.Image)
        Me.grpMotivoDevolucion.Controls.Add(Me.txtNombPedido)
        Me.grpMotivoDevolucion.Controls.Add(Me.lnkPedido)
        Me.grpMotivoDevolucion.Controls.Add(Me.txtIdPedidoDevolucionEnc)
        Me.grpMotivoDevolucion.Controls.Add(Me.cmbMotivoDevolucion)
        Me.grpMotivoDevolucion.Controls.Add(Me.lblMotivoDevolucion)
        Me.grpMotivoDevolucion.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpMotivoDevolucion.Location = New System.Drawing.Point(0, 26)
        Me.grpMotivoDevolucion.Margin = New System.Windows.Forms.Padding(4)
        Me.grpMotivoDevolucion.Name = "grpMotivoDevolucion"
        Me.grpMotivoDevolucion.Size = New System.Drawing.Size(755, 136)
        Me.grpMotivoDevolucion.TabIndex = 1
        Me.grpMotivoDevolucion.Text = "Motivo Devolución"
        '
        'txtNombPedido
        '
        Me.txtNombPedido.Location = New System.Drawing.Point(327, 86)
        Me.txtNombPedido.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNombPedido.Name = "txtNombPedido"
        Me.txtNombPedido.Properties.ReadOnly = True
        Me.txtNombPedido.Size = New System.Drawing.Size(240, 22)
        Me.txtNombPedido.TabIndex = 18
        '
        'lnkPedido
        '
        Me.lnkPedido.AutoSize = True
        Me.lnkPedido.Location = New System.Drawing.Point(68, 87)
        Me.lnkPedido.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkPedido.Name = "lnkPedido"
        Me.lnkPedido.Size = New System.Drawing.Size(89, 16)
        Me.lnkPedido.TabIndex = 16
        Me.lnkPedido.TabStop = True
        Me.lnkPedido.Text = "Doc. de Salida"
        '
        'txtIdPedidoDevolucionEnc
        '
        Me.txtIdPedidoDevolucionEnc.Location = New System.Drawing.Point(214, 86)
        Me.txtIdPedidoDevolucionEnc.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdPedidoDevolucionEnc.Name = "txtIdPedidoDevolucionEnc"
        Me.txtIdPedidoDevolucionEnc.Properties.Mask.EditMask = "n0"
        Me.txtIdPedidoDevolucionEnc.Size = New System.Drawing.Size(108, 22)
        Me.txtIdPedidoDevolucionEnc.TabIndex = 17
        '
        'cmbMotivoDevolucion
        '
        Me.cmbMotivoDevolucion.Location = New System.Drawing.Point(214, 52)
        Me.cmbMotivoDevolucion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbMotivoDevolucion.MenuManager = Me.grpEncRec
        Me.cmbMotivoDevolucion.Name = "cmbMotivoDevolucion"
        Me.cmbMotivoDevolucion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbMotivoDevolucion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMotivoDevolucion.Properties.NullText = ""
        Me.cmbMotivoDevolucion.Size = New System.Drawing.Size(354, 22)
        Me.cmbMotivoDevolucion.TabIndex = 2
        Me.cmbMotivoDevolucion.Visible = False
        '
        'lblMotivoDevolucion
        '
        Me.lblMotivoDevolucion.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblMotivoDevolucion.Appearance.Options.UseFont = True
        Me.lblMotivoDevolucion.Location = New System.Drawing.Point(69, 57)
        Me.lblMotivoDevolucion.Margin = New System.Windows.Forms.Padding(4)
        Me.lblMotivoDevolucion.Name = "lblMotivoDevolucion"
        Me.lblMotivoDevolucion.Size = New System.Drawing.Size(107, 16)
        Me.lblMotivoDevolucion.TabIndex = 0
        Me.lblMotivoDevolucion.Text = "Motivo Devolución:"
        '
        'chkActivo
        '
        Me.chkActivo.AutoSizeInLayoutControl = True
        Me.chkActivo.Dock = System.Windows.Forms.DockStyle.Top
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(0, 0)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkActivo.MenuManager = Me.grpEncRec
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.chkActivo.Properties.Caption = "Activo"
        Me.chkActivo.Size = New System.Drawing.Size(755, 26)
        Me.chkActivo.TabIndex = 0
        '
        'GrpDetalle
        '
        Me.GrpDetalle.Controls.Add(Me.prgp)
        Me.GrpDetalle.Controls.Add(Me.DgridDetalleOC)
        Me.GrpDetalle.Controls.Add(Me.ToolStrip1)
        Me.GrpDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpDetalle.Location = New System.Drawing.Point(0, 0)
        Me.GrpDetalle.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GrpDetalle.Name = "GrpDetalle"
        Me.GrpDetalle.Size = New System.Drawing.Size(1630, 715)
        Me.GrpDetalle.TabIndex = 0
        Me.GrpDetalle.Text = "Lista de Productos"
        '
        'prgp
        '
        Me.prgp.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.prgp.Appearance.Options.UseBackColor = True
        Me.prgp.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.prgp.Caption = "Copiando a portapapeles"
        Me.prgp.Description = ""
        Me.prgp.Location = New System.Drawing.Point(676, 348)
        Me.prgp.LookAndFeel.UseDefaultLookAndFeel = False
        Me.prgp.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.prgp.Name = "prgp"
        Me.prgp.ShowDescription = False
        Me.prgp.Size = New System.Drawing.Size(359, 101)
        Me.prgp.TabIndex = 2
        Me.prgp.Visible = False
        '
        'DgridDetalleOC
        '
        Me.DgridDetalleOC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridDetalleOC.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.DgridDetalleOC.Location = New System.Drawing.Point(2, 55)
        Me.DgridDetalleOC.MainView = Me.gvDetalleDocIngreso
        Me.DgridDetalleOC.Margin = New System.Windows.Forms.Padding(4)
        Me.DgridDetalleOC.Name = "DgridDetalleOC"
        Me.DgridDetalleOC.Size = New System.Drawing.Size(1626, 658)
        Me.DgridDetalleOC.TabIndex = 19
        Me.DgridDetalleOC.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvDetalleDocIngreso})
        '
        'gvDetalleDocIngreso
        '
        Me.gvDetalleDocIngreso.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvDetalleDocIngreso.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvDetalleDocIngreso.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvDetalleDocIngreso.Appearance.Row.Options.UseFont = True
        Me.gvDetalleDocIngreso.DetailHeight = 437
        Me.gvDetalleDocIngreso.GridControl = Me.DgridDetalleOC
        Me.gvDetalleDocIngreso.Name = "gvDetalleDocIngreso"
        Me.gvDetalleDocIngreso.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvDetalleDocIngreso.OptionsView.ShowAutoFilterRow = True
        Me.gvDetalleDocIngreso.OptionsView.ShowGroupPanel = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdAgregarProducto, Me.cmdEliminarFila})
        Me.ToolStrip1.Location = New System.Drawing.Point(2, 28)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1626, 27)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip4"
        '
        'cmdAgregarProducto
        '
        Me.cmdAgregarProducto.Image = CType(resources.GetObject("cmdAgregarProducto.Image"), System.Drawing.Image)
        Me.cmdAgregarProducto.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAgregarProducto.Name = "cmdAgregarProducto"
        Me.cmdAgregarProducto.Size = New System.Drawing.Size(151, 24)
        Me.cmdAgregarProducto.Text = "Agregar Producto"
        '
        'cmdEliminarFila
        '
        Me.cmdEliminarFila.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEliminarFila.Image = CType(resources.GetObject("cmdEliminarFila.Image"), System.Drawing.Image)
        Me.cmdEliminarFila.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdEliminarFila.Name = "cmdEliminarFila"
        Me.cmdEliminarFila.Size = New System.Drawing.Size(118, 24)
        Me.cmdEliminarFila.Text = "Eliminar Fila"
        '
        'GrpImagen
        '
        Me.GrpImagen.Controls.Add(Me.PicImg)
        Me.GrpImagen.Controls.Add(Me.Label23)
        Me.GrpImagen.Controls.Add(Me.GroupControl4)
        Me.GrpImagen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpImagen.Location = New System.Drawing.Point(0, 0)
        Me.GrpImagen.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GrpImagen.Name = "GrpImagen"
        Me.GrpImagen.Size = New System.Drawing.Size(1630, 715)
        Me.GrpImagen.TabIndex = 0
        '
        'PicImg
        '
        Me.PicImg.Location = New System.Drawing.Point(568, 75)
        Me.PicImg.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PicImg.Name = "PicImg"
        Me.PicImg.Size = New System.Drawing.Size(762, 601)
        Me.PicImg.TabIndex = 93
        Me.PicImg.TabStop = False
        Me.PicImg.Visible = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(562, 42)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(0, 17)
        Me.Label23.TabIndex = 1
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.GrdImagen)
        Me.GroupControl4.Location = New System.Drawing.Point(8, 42)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(551, 635)
        Me.GroupControl4.TabIndex = 0
        Me.GroupControl4.Text = "Lista de Imágenes"
        '
        'GrdImagen
        '
        Me.GrdImagen.Cursor = System.Windows.Forms.Cursors.Default
        Me.GrdImagen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrdImagen.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GrdImagen.Location = New System.Drawing.Point(2, 28)
        Me.GrdImagen.MainView = Me.GridViewImg
        Me.GrdImagen.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GrdImagen.MenuManager = Me.grpEncRec
        Me.GrdImagen.Name = "GrdImagen"
        Me.GrdImagen.Size = New System.Drawing.Size(547, 605)
        Me.GrdImagen.TabIndex = 1
        Me.GrdImagen.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewImg})
        '
        'GridViewImg
        '
        Me.GridViewImg.DetailHeight = 437
        Me.GridViewImg.GridControl = Me.GrdImagen
        Me.GridViewImg.Name = "GridViewImg"
        Me.GridViewImg.OptionsBehavior.Editable = False
        Me.GridViewImg.OptionsFind.AlwaysVisible = True
        Me.GridViewImg.OptionsView.ShowGroupPanel = False
        '
        'ToolStrip
        '
        Me.ToolStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdAdd, Me.cmdDelete})
        Me.ToolStrip.Location = New System.Drawing.Point(2, 33)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(437, 31)
        Me.ToolStrip.TabIndex = 0
        Me.ToolStrip.Text = "ToolStrip4"
        '
        'cmdAdd
        '
        Me.cmdAdd.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(29, 28)
        '
        'cmdDelete
        '
        Me.cmdDelete.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(29, 28)
        '
        'dkOrdenCompra
        '
        Me.dkOrdenCompra.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkOrdenCompra.Form = Me
        Me.dkOrdenCompra.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 938)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1632, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("0edba7b8-5062-4fcb-b46c-8fbffdcf2fc4")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 784)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 123)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1632, 154)
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
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 34)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1624, 116)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'xtraOrdenCompra
        '
        Me.xtraOrdenCompra.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.xtraOrdenCompra.Appearance.Options.UseBackColor = True
        Me.xtraOrdenCompra.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtraOrdenCompra.Location = New System.Drawing.Point(0, 193)
        Me.xtraOrdenCompra.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.xtraOrdenCompra.Name = "xtraOrdenCompra"
        Me.xtraOrdenCompra.SelectedTabPage = Me.OrdenCompra
        Me.xtraOrdenCompra.Size = New System.Drawing.Size(1632, 745)
        Me.xtraOrdenCompra.TabIndex = 0
        Me.xtraOrdenCompra.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.OrdenCompra, Me.Poliza, Me.DetalleOC, Me.tabDetalleServicios, Me.Enc_RecOC, Me.Imagenes, Me.tabLotes, Me.tabDetERP, Me.tabPedidosDevolucion, Me.tabPolizaCorregida, Me.tabTallaColor})
        '
        'OrdenCompra
        '
        Me.OrdenCompra.Controls.Add(Me.GrpEnc)
        Me.OrdenCompra.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.OrdenCompra.Name = "OrdenCompra"
        Me.OrdenCompra.Size = New System.Drawing.Size(1630, 715)
        Me.OrdenCompra.Text = "Cabecera"
        '
        'Poliza
        '
        Me.Poliza.Controls.Add(Me.SplitContainer1)
        Me.Poliza.Controls.Add(Me.GrpEmbarque)
        Me.Poliza.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Poliza.Name = "Poliza"
        Me.Poliza.Size = New System.Drawing.Size(1630, 715)
        Me.Poliza.Text = "Poliza"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GrpPoliza)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupControl2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1630, 715)
        Me.SplitContainer1.SplitterDistance = 433
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 2
        '
        'GrpPoliza
        '
        Me.GrpPoliza.Controls.Add(Me.cbCBM)
        Me.GrpPoliza.Controls.Add(Label1)
        Me.GrpPoliza.Controls.Add(Me.txtTotal_general)
        Me.GrpPoliza.Controls.Add(Label34)
        Me.GrpPoliza.Controls.Add(Me.txtTotal_liquidar)
        Me.GrpPoliza.Controls.Add(Label33)
        Me.GrpPoliza.Controls.Add(Me.txtMod_transporte)
        Me.GrpPoliza.Controls.Add(Label32)
        Me.GrpPoliza.Controls.Add(Me.txtClase)
        Me.GrpPoliza.Controls.Add(Label28)
        Me.GrpPoliza.Controls.Add(Me.txtNitImpExp)
        Me.GrpPoliza.Controls.Add(Label27)
        Me.GrpPoliza.Controls.Add(Me.txtClaveAduana)
        Me.GrpPoliza.Controls.Add(Label26)
        Me.GrpPoliza.Controls.Add(lblRegimen)
        Me.GrpPoliza.Controls.Add(Me.cmbRegimen)
        Me.GrpPoliza.Controls.Add(Me.txtTotalPesoNeto)
        Me.GrpPoliza.Controls.Add(lblPesoNeto)
        Me.GrpPoliza.Controls.Add(Me.dtpFechaLlegada)
        Me.GrpPoliza.Controls.Add(Label12)
        Me.GrpPoliza.Controls.Add(Me.dtpFechaAceptacion)
        Me.GrpPoliza.Controls.Add(lblFechaAceptacion)
        Me.GrpPoliza.Controls.Add(Me.txtTotalOtros)
        Me.GrpPoliza.Controls.Add(lblTotalOtros)
        Me.GrpPoliza.Controls.Add(lblNoOrden)
        Me.GrpPoliza.Controls.Add(Me.txtNumeroOrden)
        Me.GrpPoliza.Controls.Add(lblTicket)
        Me.GrpPoliza.Controls.Add(Me.txtTicket)
        Me.GrpPoliza.Controls.Add(lblCodigoPoliza)
        Me.GrpPoliza.Controls.Add(Me.txtCodigoPoliza)
        Me.GrpPoliza.Controls.Add(Me.dtFechaPoliza)
        Me.GrpPoliza.Controls.Add(Me.txtValorSeguro)
        Me.GrpPoliza.Controls.Add(Me.txtTotalBulto)
        Me.GrpPoliza.Controls.Add(Me.txtTotalLineas)
        Me.GrpPoliza.Controls.Add(Me.txtTipoCambio)
        Me.GrpPoliza.Controls.Add(Me.txtTotalFOBUSD)
        Me.GrpPoliza.Controls.Add(Me.txtValorFlete)
        Me.GrpPoliza.Controls.Add(Me.txtTotalPesoBruto)
        Me.GrpPoliza.Controls.Add(Me.txtValorAduana)
        Me.GrpPoliza.Controls.Add(lblTotalSeguroUSD)
        Me.GrpPoliza.Controls.Add(Label25)
        Me.GrpPoliza.Controls.Add(lblNumeroDUA)
        Me.GrpPoliza.Controls.Add(lblTotalLineas)
        Me.GrpPoliza.Controls.Add(lblFechaDocumento)
        Me.GrpPoliza.Controls.Add(Label29)
        Me.GrpPoliza.Controls.Add(Me.txtNumeroDUA)
        Me.GrpPoliza.Controls.Add(Label31)
        Me.GrpPoliza.Controls.Add(lblTotalFOBUSD)
        Me.GrpPoliza.Controls.Add(lblTotalFleteUSD)
        Me.GrpPoliza.Controls.Add(lblTotalPesoBruto)
        Me.GrpPoliza.Controls.Add(lblTotalValorAduana)
        Me.GrpPoliza.Controls.Add(Me.txtNoPoliza)
        Me.GrpPoliza.Controls.Add(Me.txtPaisProcedencia)
        Me.GrpPoliza.Controls.Add(Label36)
        Me.GrpPoliza.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpPoliza.Location = New System.Drawing.Point(0, 0)
        Me.GrpPoliza.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GrpPoliza.Name = "GrpPoliza"
        Me.GrpPoliza.Size = New System.Drawing.Size(1630, 433)
        Me.GrpPoliza.TabIndex = 0
        Me.GrpPoliza.Text = "Cabecera de Poliza"
        '
        'cbCBM
        '
        Me.cbCBM.DecimalPlaces = 6
        Me.cbCBM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCBM.Location = New System.Drawing.Point(174, 251)
        Me.cbCBM.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.cbCBM.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.cbCBM.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.cbCBM.Name = "cbCBM"
        Me.cbCBM.Size = New System.Drawing.Size(226, 24)
        Me.cbCBM.TabIndex = 270
        '
        'txtTotal_general
        '
        Me.txtTotal_general.Location = New System.Drawing.Point(1430, 202)
        Me.txtTotal_general.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtTotal_general.MenuManager = Me.grpEncRec
        Me.txtTotal_general.Name = "txtTotal_general"
        Me.txtTotal_general.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtTotal_general.Properties.Appearance.Options.UseBackColor = True
        Me.txtTotal_general.Size = New System.Drawing.Size(156, 22)
        Me.txtTotal_general.TabIndex = 268
        '
        'txtTotal_liquidar
        '
        Me.txtTotal_liquidar.Location = New System.Drawing.Point(1430, 158)
        Me.txtTotal_liquidar.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtTotal_liquidar.MenuManager = Me.grpEncRec
        Me.txtTotal_liquidar.Name = "txtTotal_liquidar"
        Me.txtTotal_liquidar.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtTotal_liquidar.Properties.Appearance.Options.UseBackColor = True
        Me.txtTotal_liquidar.Size = New System.Drawing.Size(156, 22)
        Me.txtTotal_liquidar.TabIndex = 266
        '
        'txtMod_transporte
        '
        Me.txtMod_transporte.Location = New System.Drawing.Point(1430, 123)
        Me.txtMod_transporte.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtMod_transporte.MenuManager = Me.grpEncRec
        Me.txtMod_transporte.Name = "txtMod_transporte"
        Me.txtMod_transporte.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtMod_transporte.Properties.Appearance.Options.UseBackColor = True
        Me.txtMod_transporte.Size = New System.Drawing.Size(156, 22)
        Me.txtMod_transporte.TabIndex = 264
        '
        'txtClase
        '
        Me.txtClase.Location = New System.Drawing.Point(1430, 76)
        Me.txtClase.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtClase.MenuManager = Me.grpEncRec
        Me.txtClase.Name = "txtClase"
        Me.txtClase.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtClase.Properties.Appearance.Options.UseBackColor = True
        Me.txtClase.Size = New System.Drawing.Size(156, 22)
        Me.txtClase.TabIndex = 262
        '
        'txtNitImpExp
        '
        Me.txtNitImpExp.Location = New System.Drawing.Point(1430, 39)
        Me.txtNitImpExp.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtNitImpExp.MenuManager = Me.grpEncRec
        Me.txtNitImpExp.Name = "txtNitImpExp"
        Me.txtNitImpExp.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtNitImpExp.Properties.Appearance.Options.UseBackColor = True
        Me.txtNitImpExp.Size = New System.Drawing.Size(156, 22)
        Me.txtNitImpExp.TabIndex = 260
        '
        'txtClaveAduana
        '
        Me.txtClaveAduana.Location = New System.Drawing.Point(1034, 121)
        Me.txtClaveAduana.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtClaveAduana.MenuManager = Me.grpEncRec
        Me.txtClaveAduana.Name = "txtClaveAduana"
        Me.txtClaveAduana.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtClaveAduana.Properties.Appearance.Options.UseBackColor = True
        Me.txtClaveAduana.Size = New System.Drawing.Size(197, 22)
        Me.txtClaveAduana.TabIndex = 258
        '
        'cmbRegimen
        '
        Me.cmbRegimen.Location = New System.Drawing.Point(1034, 202)
        Me.cmbRegimen.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbRegimen.MenuManager = Me.grpEncRec
        Me.cmbRegimen.Name = "cmbRegimen"
        Me.cmbRegimen.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.cmbRegimen.Properties.Appearance.Options.UseBackColor = True
        Me.cmbRegimen.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbRegimen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbRegimen.Properties.NullText = ""
        Me.cmbRegimen.Size = New System.Drawing.Size(198, 22)
        Me.cmbRegimen.TabIndex = 251
        '
        'txtTotalPesoNeto
        '
        Me.txtTotalPesoNeto.DecimalPlaces = 6
        Me.txtTotalPesoNeto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalPesoNeto.Location = New System.Drawing.Point(1034, 41)
        Me.txtTotalPesoNeto.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtTotalPesoNeto.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTotalPesoNeto.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtTotalPesoNeto.Name = "txtTotalPesoNeto"
        Me.txtTotalPesoNeto.Size = New System.Drawing.Size(197, 24)
        Me.txtTotalPesoNeto.TabIndex = 244
        '
        'dtpFechaLlegada
        '
        Me.dtpFechaLlegada.EditValue = New Date(2017, 11, 20, 10, 36, 51, 115)
        Me.dtpFechaLlegada.Location = New System.Drawing.Point(621, 121)
        Me.dtpFechaLlegada.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.dtpFechaLlegada.MenuManager = Me.grpEncRec
        Me.dtpFechaLlegada.Name = "dtpFechaLlegada"
        Me.dtpFechaLlegada.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.dtpFechaLlegada.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFechaLlegada.Properties.Appearance.Options.UseBackColor = True
        Me.dtpFechaLlegada.Properties.Appearance.Options.UseFont = True
        Me.dtpFechaLlegada.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaLlegada.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaLlegada.Properties.DisplayFormat.FormatString = ""
        Me.dtpFechaLlegada.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtpFechaLlegada.Properties.EditFormat.FormatString = ""
        Me.dtpFechaLlegada.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtpFechaLlegada.Size = New System.Drawing.Size(197, 24)
        Me.dtpFechaLlegada.TabIndex = 232
        '
        'dtpFechaAceptacion
        '
        Me.dtpFechaAceptacion.EditValue = New Date(2017, 11, 20, 10, 36, 51, 115)
        Me.dtpFechaAceptacion.Location = New System.Drawing.Point(621, 78)
        Me.dtpFechaAceptacion.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.dtpFechaAceptacion.MenuManager = Me.grpEncRec
        Me.dtpFechaAceptacion.Name = "dtpFechaAceptacion"
        Me.dtpFechaAceptacion.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.dtpFechaAceptacion.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFechaAceptacion.Properties.Appearance.Options.UseBackColor = True
        Me.dtpFechaAceptacion.Properties.Appearance.Options.UseFont = True
        Me.dtpFechaAceptacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaAceptacion.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaAceptacion.Size = New System.Drawing.Size(197, 24)
        Me.dtpFechaAceptacion.TabIndex = 230
        '
        'txtTotalOtros
        '
        Me.txtTotalOtros.BackColor = System.Drawing.Color.PowderBlue
        Me.txtTotalOtros.DecimalPlaces = 6
        Me.txtTotalOtros.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalOtros.Location = New System.Drawing.Point(621, 250)
        Me.txtTotalOtros.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtTotalOtros.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTotalOtros.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtTotalOtros.Name = "txtTotalOtros"
        Me.txtTotalOtros.Size = New System.Drawing.Size(197, 24)
        Me.txtTotalOtros.TabIndex = 238
        '
        'txtNumeroOrden
        '
        Me.txtNumeroOrden.Location = New System.Drawing.Point(174, 160)
        Me.txtNumeroOrden.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtNumeroOrden.MenuManager = Me.grpEncRec
        Me.txtNumeroOrden.Name = "txtNumeroOrden"
        Me.txtNumeroOrden.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtNumeroOrden.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumeroOrden.Properties.Appearance.Options.UseBackColor = True
        Me.txtNumeroOrden.Properties.Appearance.Options.UseFont = True
        Me.txtNumeroOrden.Size = New System.Drawing.Size(226, 24)
        Me.txtNumeroOrden.TabIndex = 224
        '
        'txtTicket
        '
        Me.txtTicket.Location = New System.Drawing.Point(174, 127)
        Me.txtTicket.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtTicket.MenuManager = Me.grpEncRec
        Me.txtTicket.Name = "txtTicket"
        Me.txtTicket.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtTicket.Properties.Appearance.Options.UseBackColor = True
        Me.txtTicket.Properties.Appearance.Options.UseFont = True
        Me.txtTicket.Size = New System.Drawing.Size(226, 22)
        Me.txtTicket.TabIndex = 222
        '
        'txtCodigoPoliza
        '
        Me.txtCodigoPoliza.Location = New System.Drawing.Point(174, 86)
        Me.txtCodigoPoliza.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtCodigoPoliza.MenuManager = Me.grpEncRec
        Me.txtCodigoPoliza.Name = "txtCodigoPoliza"
        Me.txtCodigoPoliza.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtCodigoPoliza.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodigoPoliza.Properties.Appearance.Options.UseBackColor = True
        Me.txtCodigoPoliza.Properties.Appearance.Options.UseFont = True
        Me.txtCodigoPoliza.Size = New System.Drawing.Size(226, 24)
        Me.txtCodigoPoliza.TabIndex = 220
        '
        'dtFechaPoliza
        '
        Me.dtFechaPoliza.EditValue = New Date(2017, 11, 20, 10, 36, 51, 115)
        Me.dtFechaPoliza.Location = New System.Drawing.Point(621, 37)
        Me.dtFechaPoliza.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.dtFechaPoliza.MenuManager = Me.grpEncRec
        Me.dtFechaPoliza.Name = "dtFechaPoliza"
        Me.dtFechaPoliza.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFechaPoliza.Properties.Appearance.Options.UseFont = True
        Me.dtFechaPoliza.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFechaPoliza.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFechaPoliza.Size = New System.Drawing.Size(197, 24)
        Me.dtFechaPoliza.TabIndex = 228
        '
        'txtValorSeguro
        '
        Me.txtValorSeguro.BackColor = System.Drawing.Color.PowderBlue
        Me.txtValorSeguro.DecimalPlaces = 6
        Me.txtValorSeguro.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorSeguro.Location = New System.Drawing.Point(621, 166)
        Me.txtValorSeguro.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtValorSeguro.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtValorSeguro.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtValorSeguro.Name = "txtValorSeguro"
        Me.txtValorSeguro.Size = New System.Drawing.Size(197, 24)
        Me.txtValorSeguro.TabIndex = 234
        '
        'txtTotalBulto
        '
        Me.txtTotalBulto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalBulto.Location = New System.Drawing.Point(1034, 298)
        Me.txtTotalBulto.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtTotalBulto.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTotalBulto.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtTotalBulto.Name = "txtTotalBulto"
        Me.txtTotalBulto.Size = New System.Drawing.Size(198, 24)
        Me.txtTotalBulto.TabIndex = 255
        '
        'txtTotalLineas
        '
        Me.txtTotalLineas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalLineas.Location = New System.Drawing.Point(1034, 246)
        Me.txtTotalLineas.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtTotalLineas.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTotalLineas.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtTotalLineas.Name = "txtTotalLineas"
        Me.txtTotalLineas.Size = New System.Drawing.Size(198, 24)
        Me.txtTotalLineas.TabIndex = 253
        '
        'txtTipoCambio
        '
        Me.txtTipoCambio.BackColor = System.Drawing.Color.PowderBlue
        Me.txtTipoCambio.DecimalPlaces = 6
        Me.txtTipoCambio.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTipoCambio.Location = New System.Drawing.Point(1034, 81)
        Me.txtTipoCambio.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtTipoCambio.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTipoCambio.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtTipoCambio.Name = "txtTipoCambio"
        Me.txtTipoCambio.Size = New System.Drawing.Size(197, 24)
        Me.txtTipoCambio.TabIndex = 246
        '
        'txtTotalFOBUSD
        '
        Me.txtTotalFOBUSD.BackColor = System.Drawing.Color.PowderBlue
        Me.txtTotalFOBUSD.DecimalPlaces = 6
        Me.txtTotalFOBUSD.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalFOBUSD.Location = New System.Drawing.Point(1034, 336)
        Me.txtTotalFOBUSD.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtTotalFOBUSD.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTotalFOBUSD.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtTotalFOBUSD.Name = "txtTotalFOBUSD"
        Me.txtTotalFOBUSD.Size = New System.Drawing.Size(198, 24)
        Me.txtTotalFOBUSD.TabIndex = 257
        '
        'txtValorFlete
        '
        Me.txtValorFlete.BackColor = System.Drawing.Color.PowderBlue
        Me.txtValorFlete.DecimalPlaces = 6
        Me.txtValorFlete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorFlete.Location = New System.Drawing.Point(621, 298)
        Me.txtValorFlete.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtValorFlete.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtValorFlete.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtValorFlete.Name = "txtValorFlete"
        Me.txtValorFlete.Size = New System.Drawing.Size(197, 24)
        Me.txtValorFlete.TabIndex = 240
        '
        'txtTotalPesoBruto
        '
        Me.txtTotalPesoBruto.BackColor = System.Drawing.Color.PowderBlue
        Me.txtTotalPesoBruto.DecimalPlaces = 6
        Me.txtTotalPesoBruto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalPesoBruto.Location = New System.Drawing.Point(621, 342)
        Me.txtTotalPesoBruto.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtTotalPesoBruto.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTotalPesoBruto.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtTotalPesoBruto.Name = "txtTotalPesoBruto"
        Me.txtTotalPesoBruto.Size = New System.Drawing.Size(197, 24)
        Me.txtTotalPesoBruto.TabIndex = 242
        '
        'txtValorAduana
        '
        Me.txtValorAduana.BackColor = System.Drawing.Color.PowderBlue
        Me.txtValorAduana.DecimalPlaces = 6
        Me.txtValorAduana.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorAduana.Location = New System.Drawing.Point(621, 209)
        Me.txtValorAduana.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtValorAduana.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtValorAduana.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtValorAduana.Name = "txtValorAduana"
        Me.txtValorAduana.Size = New System.Drawing.Size(197, 24)
        Me.txtValorAduana.TabIndex = 236
        '
        'txtNumeroDUA
        '
        Me.txtNumeroDUA.Location = New System.Drawing.Point(174, 201)
        Me.txtNumeroDUA.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtNumeroDUA.MenuManager = Me.grpEncRec
        Me.txtNumeroDUA.Name = "txtNumeroDUA"
        Me.txtNumeroDUA.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtNumeroDUA.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumeroDUA.Properties.Appearance.Options.UseBackColor = True
        Me.txtNumeroDUA.Properties.Appearance.Options.UseFont = True
        Me.txtNumeroDUA.Size = New System.Drawing.Size(226, 24)
        Me.txtNumeroDUA.TabIndex = 226
        '
        'txtNoPoliza
        '
        Me.txtNoPoliza.Location = New System.Drawing.Point(174, 52)
        Me.txtNoPoliza.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtNoPoliza.MenuManager = Me.grpEncRec
        Me.txtNoPoliza.Name = "txtNoPoliza"
        Me.txtNoPoliza.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtNoPoliza.Properties.Appearance.Options.UseBackColor = True
        Me.txtNoPoliza.Size = New System.Drawing.Size(226, 22)
        Me.txtNoPoliza.TabIndex = 217
        '
        'txtPaisProcedencia
        '
        Me.txtPaisProcedencia.Location = New System.Drawing.Point(1034, 159)
        Me.txtPaisProcedencia.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtPaisProcedencia.MenuManager = Me.grpEncRec
        Me.txtPaisProcedencia.Name = "txtPaisProcedencia"
        Me.txtPaisProcedencia.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtPaisProcedencia.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaisProcedencia.Properties.Appearance.Options.UseBackColor = True
        Me.txtPaisProcedencia.Properties.Appearance.Options.UseFont = True
        Me.txtPaisProcedencia.Size = New System.Drawing.Size(198, 24)
        Me.txtPaisProcedencia.TabIndex = 249
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.dgridDetallePoliza)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1630, 277)
        Me.GroupControl2.TabIndex = 1
        Me.GroupControl2.Text = "Detalle de Póliza"
        '
        'dgridDetallePoliza
        '
        Me.dgridDetallePoliza.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridDetallePoliza.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridDetallePoliza.Location = New System.Drawing.Point(2, 28)
        Me.dgridDetallePoliza.MainView = Me.gvdetallepoliza
        Me.dgridDetallePoliza.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridDetallePoliza.MenuManager = Me.grpEncRec
        Me.dgridDetallePoliza.Name = "dgridDetallePoliza"
        Me.dgridDetallePoliza.Size = New System.Drawing.Size(1626, 247)
        Me.dgridDetallePoliza.TabIndex = 0
        Me.dgridDetallePoliza.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvdetallepoliza})
        '
        'gvdetallepoliza
        '
        Me.gvdetallepoliza.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvdetallepoliza.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvdetallepoliza.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvdetallepoliza.Appearance.Row.Options.UseFont = True
        Me.gvdetallepoliza.DetailHeight = 437
        Me.gvdetallepoliza.GridControl = Me.dgridDetallePoliza
        Me.gvdetallepoliza.Name = "gvdetallepoliza"
        Me.gvdetallepoliza.OptionsView.ColumnAutoWidth = False
        '
        'GrpEmbarque
        '
        Me.GrpEmbarque.Controls.Add(Me.cmdPrepareGrid)
        Me.GrpEmbarque.Controls.Add(Me.dtFechaAbordaje)
        Me.GrpEmbarque.Controls.Add(Me.txtPiezas)
        Me.GrpEmbarque.Controls.Add(Me.txtCantidad)
        Me.GrpEmbarque.Controls.Add(Me.txtCBM)
        Me.GrpEmbarque.Controls.Add(Me.txtPesoKgs)
        Me.GrpEmbarque.Controls.Add(Label22)
        Me.GrpEmbarque.Controls.Add(Me.txtDestinatario)
        Me.GrpEmbarque.Controls.Add(Me.txtViaje)
        Me.GrpEmbarque.Controls.Add(Label21)
        Me.GrpEmbarque.Controls.Add(Me.txtPONumber)
        Me.GrpEmbarque.Controls.Add(Label20)
        Me.GrpEmbarque.Controls.Add(Label10)
        Me.GrpEmbarque.Controls.Add(Label19)
        Me.GrpEmbarque.Controls.Add(Label17)
        Me.GrpEmbarque.Controls.Add(Label18)
        Me.GrpEmbarque.Controls.Add(Me.txtDireccion)
        Me.GrpEmbarque.Controls.Add(Me.txtBuque)
        Me.GrpEmbarque.Controls.Add(Label11)
        Me.GrpEmbarque.Controls.Add(Label9)
        Me.GrpEmbarque.Controls.Add(Label13)
        Me.GrpEmbarque.Controls.Add(Label14)
        Me.GrpEmbarque.Controls.Add(Me.Descripcion)
        Me.GrpEmbarque.Controls.Add(Label15)
        Me.GrpEmbarque.Controls.Add(Label16)
        Me.GrpEmbarque.Controls.Add(Me.txtRemitente)
        Me.GrpEmbarque.Controls.Add(Me.BLNo)
        Me.GrpEmbarque.Controls.Add(Me.txtPuertaDescarga)
        Me.GrpEmbarque.Controls.Add(Label8)
        Me.GrpEmbarque.Location = New System.Drawing.Point(3267, 159)
        Me.GrpEmbarque.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GrpEmbarque.Name = "GrpEmbarque"
        Me.GrpEmbarque.Size = New System.Drawing.Size(1442, 866)
        Me.GrpEmbarque.TabIndex = 0
        Me.GrpEmbarque.Text = "Embarque"
        '
        'cmdPrepareGrid
        '
        Me.cmdPrepareGrid.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.cmdPrepareGrid.Location = New System.Drawing.Point(186, 399)
        Me.cmdPrepareGrid.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdPrepareGrid.Name = "cmdPrepareGrid"
        Me.cmdPrepareGrid.Size = New System.Drawing.Size(149, 32)
        Me.cmdPrepareGrid.TabIndex = 28
        Me.cmdPrepareGrid.Text = "Preparar GRID "
        Me.cmdPrepareGrid.UseVisualStyleBackColor = True
        Me.cmdPrepareGrid.Visible = False
        '
        'dtFechaAbordaje
        '
        Me.dtFechaAbordaje.EditValue = New Date(2017, 11, 20, 10, 37, 31, 443)
        Me.dtFechaAbordaje.Location = New System.Drawing.Point(200, 212)
        Me.dtFechaAbordaje.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtFechaAbordaje.MenuManager = Me.grpEncRec
        Me.dtFechaAbordaje.Name = "dtFechaAbordaje"
        Me.dtFechaAbordaje.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFechaAbordaje.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFechaAbordaje.Size = New System.Drawing.Size(159, 22)
        Me.dtFechaAbordaje.TabIndex = 13
        '
        'txtPiezas
        '
        Me.txtPiezas.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Me.txtPiezas.Location = New System.Drawing.Point(526, 292)
        Me.txtPiezas.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPiezas.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtPiezas.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtPiezas.Name = "txtPiezas"
        Me.txtPiezas.Size = New System.Drawing.Size(189, 23)
        Me.txtPiezas.TabIndex = 22
        '
        'txtCantidad
        '
        Me.txtCantidad.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Me.txtCantidad.Location = New System.Drawing.Point(200, 292)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCantidad.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCantidad.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(159, 23)
        Me.txtCantidad.TabIndex = 23
        '
        'txtCBM
        '
        Me.txtCBM.DecimalPlaces = 6
        Me.txtCBM.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Me.txtCBM.Location = New System.Drawing.Point(526, 335)
        Me.txtCBM.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCBM.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCBM.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtCBM.Name = "txtCBM"
        Me.txtCBM.Size = New System.Drawing.Size(189, 23)
        Me.txtCBM.TabIndex = 27
        '
        'txtPesoKgs
        '
        Me.txtPesoKgs.DecimalPlaces = 6
        Me.txtPesoKgs.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Me.txtPesoKgs.Location = New System.Drawing.Point(200, 334)
        Me.txtPesoKgs.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPesoKgs.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtPesoKgs.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtPesoKgs.Name = "txtPesoKgs"
        Me.txtPesoKgs.Size = New System.Drawing.Size(159, 23)
        Me.txtPesoKgs.TabIndex = 25
        '
        'txtDestinatario
        '
        Me.txtDestinatario.Location = New System.Drawing.Point(526, 172)
        Me.txtDestinatario.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDestinatario.MenuManager = Me.grpEncRec
        Me.txtDestinatario.Name = "txtDestinatario"
        Me.txtDestinatario.Size = New System.Drawing.Size(189, 22)
        Me.txtDestinatario.TabIndex = 11
        '
        'txtViaje
        '
        Me.txtViaje.Location = New System.Drawing.Point(526, 92)
        Me.txtViaje.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtViaje.MenuManager = Me.grpEncRec
        Me.txtViaje.Name = "txtViaje"
        Me.txtViaje.Size = New System.Drawing.Size(189, 22)
        Me.txtViaje.TabIndex = 3
        '
        'txtPONumber
        '
        Me.txtPONumber.Location = New System.Drawing.Point(526, 254)
        Me.txtPONumber.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPONumber.MenuManager = Me.grpEncRec
        Me.txtPONumber.Name = "txtPONumber"
        Me.txtPONumber.Size = New System.Drawing.Size(189, 22)
        Me.txtPONumber.TabIndex = 19
        '
        'txtDireccion
        '
        Me.txtDireccion.Location = New System.Drawing.Point(526, 214)
        Me.txtDireccion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDireccion.MenuManager = Me.grpEncRec
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.Size = New System.Drawing.Size(189, 22)
        Me.txtDireccion.TabIndex = 15
        '
        'txtBuque
        '
        Me.txtBuque.Location = New System.Drawing.Point(526, 132)
        Me.txtBuque.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBuque.MenuManager = Me.grpEncRec
        Me.txtBuque.Name = "txtBuque"
        Me.txtBuque.Size = New System.Drawing.Size(189, 22)
        Me.txtBuque.TabIndex = 7
        '
        'Descripcion
        '
        Me.Descripcion.Location = New System.Drawing.Point(200, 252)
        Me.Descripcion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Descripcion.MenuManager = Me.grpEncRec
        Me.Descripcion.Name = "Descripcion"
        Me.Descripcion.Size = New System.Drawing.Size(159, 22)
        Me.Descripcion.TabIndex = 17
        '
        'txtRemitente
        '
        Me.txtRemitente.Location = New System.Drawing.Point(200, 171)
        Me.txtRemitente.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtRemitente.MenuManager = Me.grpEncRec
        Me.txtRemitente.Name = "txtRemitente"
        Me.txtRemitente.Size = New System.Drawing.Size(159, 22)
        Me.txtRemitente.TabIndex = 9
        '
        'BLNo
        '
        Me.BLNo.Location = New System.Drawing.Point(200, 91)
        Me.BLNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BLNo.MenuManager = Me.grpEncRec
        Me.BLNo.Name = "BLNo"
        Me.BLNo.Size = New System.Drawing.Size(159, 22)
        Me.BLNo.TabIndex = 1
        '
        'txtPuertaDescarga
        '
        Me.txtPuertaDescarga.Location = New System.Drawing.Point(200, 130)
        Me.txtPuertaDescarga.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPuertaDescarga.MenuManager = Me.grpEncRec
        Me.txtPuertaDescarga.Name = "txtPuertaDescarga"
        Me.txtPuertaDescarga.Size = New System.Drawing.Size(159, 22)
        Me.txtPuertaDescarga.TabIndex = 5
        '
        'DetalleOC
        '
        Me.DetalleOC.Controls.Add(Me.GrpDetalle)
        Me.DetalleOC.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DetalleOC.Name = "DetalleOC"
        Me.DetalleOC.Size = New System.Drawing.Size(1630, 715)
        Me.DetalleOC.Text = "Detalle"
        '
        'tabDetalleServicios
        '
        Me.tabDetalleServicios.Controls.Add(Me.ToolStrip2)
        Me.tabDetalleServicios.Controls.Add(Me.dgridServiciosAsociados)
        Me.tabDetalleServicios.Controls.Add(Me.cmbAcuerdoComercial)
        Me.tabDetalleServicios.Controls.Add(Me.lblAcuerdoComercial)
        Me.tabDetalleServicios.Margin = New System.Windows.Forms.Padding(4)
        Me.tabDetalleServicios.Name = "tabDetalleServicios"
        Me.tabDetalleServicios.Size = New System.Drawing.Size(1630, 715)
        Me.tabDetalleServicios.Text = "Servicios Asociados"
        '
        'ToolStrip2
        '
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdEliminarServicio})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 65)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(1630, 27)
        Me.ToolStrip2.TabIndex = 61
        Me.ToolStrip2.Text = "ToolStrip4"
        '
        'cmdEliminarServicio
        '
        Me.cmdEliminarServicio.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEliminarServicio.Image = CType(resources.GetObject("cmdEliminarServicio.Image"), System.Drawing.Image)
        Me.cmdEliminarServicio.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdEliminarServicio.Name = "cmdEliminarServicio"
        Me.cmdEliminarServicio.Size = New System.Drawing.Size(118, 24)
        Me.cmdEliminarServicio.Text = "Eliminar Fila"
        '
        'dgridServiciosAsociados
        '
        Me.dgridServiciosAsociados.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.dgridServiciosAsociados.Location = New System.Drawing.Point(-1, 126)
        Me.dgridServiciosAsociados.MainView = Me.gvDetalleServicios
        Me.dgridServiciosAsociados.Margin = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.dgridServiciosAsociados.Name = "dgridServiciosAsociados"
        Me.dgridServiciosAsociados.Size = New System.Drawing.Size(1535, 724)
        Me.dgridServiciosAsociados.TabIndex = 60
        Me.dgridServiciosAsociados.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvDetalleServicios})
        '
        'gvDetalleServicios
        '
        Me.gvDetalleServicios.DetailHeight = 682
        Me.gvDetalleServicios.GridControl = Me.dgridServiciosAsociados
        Me.gvDetalleServicios.Name = "gvDetalleServicios"
        '
        'cmbAcuerdoComercial
        '
        Me.cmbAcuerdoComercial.Dock = System.Windows.Forms.DockStyle.Top
        Me.cmbAcuerdoComercial.Location = New System.Drawing.Point(0, 37)
        Me.cmbAcuerdoComercial.Margin = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.cmbAcuerdoComercial.Name = "cmbAcuerdoComercial"
        Me.cmbAcuerdoComercial.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAcuerdoComercial.Properties.Appearance.Options.UseFont = True
        Me.cmbAcuerdoComercial.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbAcuerdoComercial.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbAcuerdoComercial.Properties.NullText = ""
        Me.cmbAcuerdoComercial.Size = New System.Drawing.Size(1630, 28)
        Me.cmbAcuerdoComercial.TabIndex = 59
        '
        'lblAcuerdoComercial
        '
        Me.lblAcuerdoComercial.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcuerdoComercial.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblAcuerdoComercial.Appearance.Options.UseFont = True
        Me.lblAcuerdoComercial.Appearance.Options.UseForeColor = True
        Me.lblAcuerdoComercial.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblAcuerdoComercial.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblAcuerdoComercial.Location = New System.Drawing.Point(0, 0)
        Me.lblAcuerdoComercial.Margin = New System.Windows.Forms.Padding(4)
        Me.lblAcuerdoComercial.Name = "lblAcuerdoComercial"
        Me.lblAcuerdoComercial.Size = New System.Drawing.Size(1630, 37)
        Me.lblAcuerdoComercial.TabIndex = 57
        Me.lblAcuerdoComercial.Text = "Acuerdo Comercial:"
        '
        'Enc_RecOC
        '
        Me.Enc_RecOC.Controls.Add(Me.GroupControl1)
        Me.Enc_RecOC.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Enc_RecOC.Name = "Enc_RecOC"
        Me.Enc_RecOC.Size = New System.Drawing.Size(1630, 715)
        Me.Enc_RecOC.Text = "Recepción"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.RibbonStatusBar1)
        Me.GroupControl1.Controls.Add(Me.grdEncRec)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1630, 715)
        Me.GroupControl1.TabIndex = 0
        '
        'grdEncRec
        '
        Me.grdEncRec.DataSource = Me.EncabezadoBindingSource
        Me.grdEncRec.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdEncRec.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdEncRec.Location = New System.Drawing.Point(2, 28)
        Me.grdEncRec.MainView = Me.GridView6
        Me.grdEncRec.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdEncRec.MenuManager = Me.grpEncRec
        Me.grdEncRec.Name = "grdEncRec"
        Me.grdEncRec.Size = New System.Drawing.Size(1626, 685)
        Me.grdEncRec.TabIndex = 0
        Me.grdEncRec.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView6})
        '
        'EncabezadoBindingSource
        '
        Me.EncabezadoBindingSource.DataMember = "Encabezado"
        Me.EncabezadoBindingSource.DataSource = Me.DsOC
        '
        'DsOC
        '
        Me.DsOC.DataSetName = "DsOC"
        Me.DsOC.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GridView6
        '
        Me.GridView6.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView6.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView6.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView6.Appearance.Row.Options.UseFont = True
        Me.GridView6.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colCódigo, Me.colBodega, Me.colNoOC, Me.colNoDocumento, Me.colFecha, Me.colEstado, Me.colTipoTransacción, Me.colDescripción, Me.colMuelle})
        Me.GridView6.DetailHeight = 437
        Me.GridView6.GridControl = Me.grdEncRec
        Me.GridView6.Name = "GridView6"
        Me.GridView6.OptionsBehavior.ReadOnly = True
        Me.GridView6.OptionsView.ShowFooter = True
        '
        'colCódigo
        '
        Me.colCódigo.FieldName = "Código"
        Me.colCódigo.MinWidth = 24
        Me.colCódigo.Name = "colCódigo"
        Me.colCódigo.Visible = True
        Me.colCódigo.VisibleIndex = 0
        Me.colCódigo.Width = 101
        '
        'colBodega
        '
        Me.colBodega.FieldName = "Bodega"
        Me.colBodega.MinWidth = 24
        Me.colBodega.Name = "colBodega"
        Me.colBodega.Visible = True
        Me.colBodega.VisibleIndex = 1
        Me.colBodega.Width = 94
        '
        'colNoOC
        '
        Me.colNoOC.FieldName = "NoDocIngreso"
        Me.colNoOC.MinWidth = 24
        Me.colNoOC.Name = "colNoOC"
        Me.colNoOC.Visible = True
        Me.colNoOC.VisibleIndex = 2
        Me.colNoOC.Width = 94
        '
        'colNoDocumento
        '
        Me.colNoDocumento.FieldName = "Referencia_DI"
        Me.colNoDocumento.MinWidth = 24
        Me.colNoDocumento.Name = "colNoDocumento"
        Me.colNoDocumento.Visible = True
        Me.colNoDocumento.VisibleIndex = 3
        Me.colNoDocumento.Width = 94
        '
        'colFecha
        '
        Me.colFecha.FieldName = "Fecha"
        Me.colFecha.MinWidth = 24
        Me.colFecha.Name = "colFecha"
        Me.colFecha.Visible = True
        Me.colFecha.VisibleIndex = 4
        Me.colFecha.Width = 94
        '
        'colEstado
        '
        Me.colEstado.FieldName = "Estado"
        Me.colEstado.MinWidth = 24
        Me.colEstado.Name = "colEstado"
        Me.colEstado.Visible = True
        Me.colEstado.VisibleIndex = 5
        Me.colEstado.Width = 94
        '
        'colTipoTransacción
        '
        Me.colTipoTransacción.FieldName = "Tipo_Transacción"
        Me.colTipoTransacción.MinWidth = 24
        Me.colTipoTransacción.Name = "colTipoTransacción"
        Me.colTipoTransacción.Visible = True
        Me.colTipoTransacción.VisibleIndex = 6
        Me.colTipoTransacción.Width = 94
        '
        'colDescripción
        '
        Me.colDescripción.FieldName = "Descripción"
        Me.colDescripción.MinWidth = 24
        Me.colDescripción.Name = "colDescripción"
        Me.colDescripción.Visible = True
        Me.colDescripción.VisibleIndex = 7
        Me.colDescripción.Width = 94
        '
        'colMuelle
        '
        Me.colMuelle.FieldName = "Muelle"
        Me.colMuelle.MinWidth = 24
        Me.colMuelle.Name = "colMuelle"
        Me.colMuelle.Visible = True
        Me.colMuelle.VisibleIndex = 8
        Me.colMuelle.Width = 94
        '
        'Imagenes
        '
        Me.Imagenes.Controls.Add(Me.GrpImagen)
        Me.Imagenes.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Imagenes.Name = "Imagenes"
        Me.Imagenes.Size = New System.Drawing.Size(1630, 715)
        Me.Imagenes.Text = "Imágenes"
        '
        'tabLotes
        '
        Me.tabLotes.Controls.Add(Me.DgridLotes)
        Me.tabLotes.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tabLotes.Name = "tabLotes"
        Me.tabLotes.Size = New System.Drawing.Size(1630, 715)
        Me.tabLotes.Text = "Lotes"
        '
        'DgridLotes
        '
        Me.DgridLotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridLotes.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DgridLotes.Location = New System.Drawing.Point(0, 0)
        Me.DgridLotes.MainView = Me.gridviewLotes
        Me.DgridLotes.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DgridLotes.MenuManager = Me.grpEncRec
        Me.DgridLotes.Name = "DgridLotes"
        Me.DgridLotes.Size = New System.Drawing.Size(1630, 715)
        Me.DgridLotes.TabIndex = 0
        Me.DgridLotes.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridviewLotes})
        '
        'gridviewLotes
        '
        Me.gridviewLotes.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridviewLotes.Appearance.HeaderPanel.Options.UseFont = True
        Me.gridviewLotes.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridviewLotes.Appearance.Row.Options.UseFont = True
        Me.gridviewLotes.DetailHeight = 437
        Me.gridviewLotes.GridControl = Me.DgridLotes
        Me.gridviewLotes.Name = "gridviewLotes"
        '
        'tabDetERP
        '
        Me.tabDetERP.Controls.Add(Me.PanelControl1)
        Me.tabDetERP.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tabDetERP.Name = "tabDetERP"
        Me.tabDetERP.PageVisible = False
        Me.tabDetERP.Size = New System.Drawing.Size(1630, 715)
        Me.tabDetERP.Text = "Documento ERP"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.grdDetERP)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1630, 715)
        Me.PanelControl1.TabIndex = 0
        '
        'grdDetERP
        '
        Me.grdDetERP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetERP.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdDetERP.Location = New System.Drawing.Point(2, 2)
        Me.grdDetERP.MainView = Me.GridView5
        Me.grdDetERP.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdDetERP.MenuManager = Me.grpEncRec
        Me.grdDetERP.Name = "grdDetERP"
        Me.grdDetERP.Size = New System.Drawing.Size(1626, 711)
        Me.grdDetERP.TabIndex = 0
        Me.grdDetERP.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView5})
        '
        'GridView5
        '
        Me.GridView5.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView5.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView5.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView5.Appearance.Row.Options.UseFont = True
        Me.GridView5.DetailHeight = 437
        Me.GridView5.GridControl = Me.grdDetERP
        Me.GridView5.Name = "GridView5"
        Me.GridView5.OptionsFind.AlwaysVisible = True
        '
        'tabPedidosDevolucion
        '
        Me.tabPedidosDevolucion.Controls.Add(Me.GroupControl6)
        Me.tabPedidosDevolucion.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.tabPedidosDevolucion.Name = "tabPedidosDevolucion"
        Me.tabPedidosDevolucion.Size = New System.Drawing.Size(1630, 715)
        Me.tabPedidosDevolucion.Text = "Pedidos en devolución"
        '
        'GroupControl6
        '
        Me.GroupControl6.Controls.Add(Me.SplitContainer3)
        Me.GroupControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl6.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl6.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl6.Name = "GroupControl6"
        Me.GroupControl6.Size = New System.Drawing.Size(1630, 715)
        Me.GroupControl6.TabIndex = 1
        Me.GroupControl6.Text = "Lista"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(2, 28)
        Me.SplitContainer3.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.dgridPedidos)
        Me.SplitContainer3.Size = New System.Drawing.Size(1626, 685)
        Me.SplitContainer3.SplitterDistance = 850
        Me.SplitContainer3.TabIndex = 2
        '
        'dgridPedidos
        '
        Me.dgridPedidos.AllowUserToResizeRows = False
        Me.dgridPedidos.BackgroundColor = System.Drawing.Color.PaleTurquoise
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
        Me.dgridPedidos.Location = New System.Drawing.Point(0, 0)
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
        Me.dgridPedidos.Size = New System.Drawing.Size(850, 685)
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
        'tabPolizaCorregida
        '
        Me.tabPolizaCorregida.Controls.Add(Me.DgridPolizas)
        Me.tabPolizaCorregida.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.tabPolizaCorregida.Name = "tabPolizaCorregida"
        Me.tabPolizaCorregida.Size = New System.Drawing.Size(1630, 715)
        Me.tabPolizaCorregida.Text = "Pólizas corregidas"
        '
        'DgridPolizas
        '
        Me.DgridPolizas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridPolizas.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.DgridPolizas.Location = New System.Drawing.Point(0, 0)
        Me.DgridPolizas.MainView = Me.gridViewPolizas
        Me.DgridPolizas.Margin = New System.Windows.Forms.Padding(4)
        Me.DgridPolizas.Name = "DgridPolizas"
        Me.DgridPolizas.Size = New System.Drawing.Size(1630, 715)
        Me.DgridPolizas.TabIndex = 17
        Me.DgridPolizas.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridViewPolizas})
        '
        'gridViewPolizas
        '
        Me.gridViewPolizas.DetailHeight = 431
        Me.gridViewPolizas.GridControl = Me.DgridPolizas
        Me.gridViewPolizas.Name = "gridViewPolizas"
        Me.gridViewPolizas.OptionsBehavior.ReadOnly = True
        Me.gridViewPolizas.OptionsEditForm.PopupEditFormWidth = 933
        Me.gridViewPolizas.OptionsFind.AlwaysVisible = True
        Me.gridViewPolizas.OptionsView.ColumnAutoWidth = False
        '
        'tabTallaColor
        '
        Me.tabTallaColor.Controls.Add(Me.dgridTallaColor)
        Me.tabTallaColor.Name = "tabTallaColor"
        Me.tabTallaColor.Size = New System.Drawing.Size(1630, 715)
        Me.tabTallaColor.Text = "Talla/Color"
        '
        'dgridTallaColor
        '
        Me.dgridTallaColor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridTallaColor.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridTallaColor.Location = New System.Drawing.Point(0, 0)
        Me.dgridTallaColor.MainView = Me.GridView7
        Me.dgridTallaColor.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridTallaColor.Name = "dgridTallaColor"
        Me.dgridTallaColor.Size = New System.Drawing.Size(1630, 715)
        Me.dgridTallaColor.TabIndex = 20
        Me.dgridTallaColor.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView7})
        '
        'GridView7
        '
        Me.GridView7.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView7.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView7.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView7.Appearance.Row.Options.UseFont = True
        Me.GridView7.DetailHeight = 437
        Me.GridView7.GridControl = Me.dgridTallaColor
        Me.GridView7.Name = "GridView7"
        Me.GridView7.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridView7.OptionsBehavior.Editable = False
        Me.GridView7.OptionsView.ShowAutoFilterRow = True
        Me.GridView7.OptionsView.ShowGroupPanel = False
        '
        'cmdImportar
        '
        Me.cmdImportar.Name = "cmdImportar"
        '
        'BarButtonItem6
        '
        Me.BarButtonItem6.Name = "BarButtonItem6"
        '
        'BarButtonItem7
        '
        Me.BarButtonItem7.Caption = "Importar Excel"
        Me.BarButtonItem7.Id = 7
        Me.BarButtonItem7.Name = "BarButtonItem7"
        '
        'frmOrdenCompra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1632, 994)
        Me.Controls.Add(Me.xtraOrdenCompra)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.grpEncRec)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmOrdenCompra"
        Me.Ribbon = Me.grpEncRec
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Documento de Ingreso consolidado"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grpEncRec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpEnc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpEnc.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.txtNomCampaña.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdCampaña.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpUltRec, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpUltRec.ResumeLayout(False)
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbOperadorDefecto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkControlPoliza.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreProveedor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdProveedor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTipoIngreso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaOrdenCompra.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaOrdenCompra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lcmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbDocumentoRef.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpDatosERP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDatosERP.ResumeLayout(False)
        Me.grpDatosERP.PerformLayout()
        CType(Me.grpScanPoliza, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpScanPoliza.ResumeLayout(False)
        Me.grpScanPoliza.PerformLayout()
        CType(Me.txtScanPoliza.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpTMS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTMS.ResumeLayout(False)
        Me.grpTMS.PerformLayout()
        CType(Me.txtNoTicketTMS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpMotivoDevolucion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMotivoDevolucion.ResumeLayout(False)
        Me.grpMotivoDevolucion.PerformLayout()
        CType(Me.txtNombPedido.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdPedidoDevolucionEnc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbMotivoDevolucion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpDetalle.ResumeLayout(False)
        Me.GrpDetalle.PerformLayout()
        CType(Me.DgridDetalleOC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetalleDocIngreso, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.GrpImagen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpImagen.ResumeLayout(False)
        Me.GrpImagen.PerformLayout()
        CType(Me.PicImg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        CType(Me.GrdImagen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewImg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        CType(Me.dkOrdenCompra, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.xtraOrdenCompra, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtraOrdenCompra.ResumeLayout(False)
        Me.OrdenCompra.ResumeLayout(False)
        Me.Poliza.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.GrpPoliza, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpPoliza.ResumeLayout(False)
        Me.GrpPoliza.PerformLayout()
        CType(Me.cbCBM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotal_general.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotal_liquidar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMod_transporte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtClase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNitImpExp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtClaveAduana.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbRegimen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalPesoNeto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaLlegada.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaLlegada.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaAceptacion.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaAceptacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalOtros, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNumeroOrden.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTicket.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoPoliza.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFechaPoliza.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFechaPoliza.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtValorSeguro, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalBulto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalLineas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTipoCambio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalFOBUSD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtValorFlete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalPesoBruto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtValorAduana, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNumeroDUA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoPoliza.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPaisProcedencia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.dgridDetallePoliza, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvdetallepoliza, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpEmbarque, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpEmbarque.ResumeLayout(False)
        Me.GrpEmbarque.PerformLayout()
        CType(Me.dtFechaAbordaje.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFechaAbordaje.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPiezas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCBM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPesoKgs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDestinatario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtViaje.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPONumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDireccion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBuque.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Descripcion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemitente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BLNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPuertaDescarga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DetalleOC.ResumeLayout(False)
        Me.tabDetalleServicios.ResumeLayout(False)
        Me.tabDetalleServicios.PerformLayout()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        CType(Me.dgridServiciosAsociados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetalleServicios, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbAcuerdoComercial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Enc_RecOC.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.grdEncRec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EncabezadoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsOC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Imagenes.ResumeLayout(False)
        Me.tabLotes.ResumeLayout(False)
        CType(Me.DgridLotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridviewLotes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabDetERP.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.grdDetERP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPedidosDevolucion.ResumeLayout(False)
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.dgridPedidos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPolizaCorregida.ResumeLayout(False)
        CType(Me.DgridPolizas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridViewPolizas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabTallaColor.ResumeLayout(False)
        CType(Me.dgridTallaColor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DetalleBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grpEncRec As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuAsignacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdCodigoBarra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimeCodigoBarra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprmirCodigoBarra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdUbicacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPage3 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GrpEnc As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkControlPoliza As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtObservacion As System.Windows.Forms.TextBox
    Friend WithEvents txtProcedencia As System.Windows.Forms.TextBox
    Friend WithEvents txtReferencia As System.Windows.Forms.TextBox
    Friend WithEvents txtNoDocumento As System.Windows.Forms.TextBox
    Friend WithEvents GrpDetalle As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrpImagen As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents GrdImagen As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewImg As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PicImg As System.Windows.Forms.PictureBox
    Friend WithEvents lnkProveedor As System.Windows.Forms.LinkLabel
    Friend WithEvents txtIdProveedor As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreProveedor As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents SubImprimir As DevExpress.XtraBars.BarSubItem
    Friend WithEvents cmdPreIngreso As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdCostoArancel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents cmdEliminarFila As ToolStripButton
    Friend WithEvents cmdAgregarProducto As ToolStripButton
    Friend WithEvents cmdImprimeBarras As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents dkOrdenCompra As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents xtraOrdenCompra As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents OrdenCompra As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents DetalleOC As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Poliza As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Imagenes As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Enc_RecOC As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdEncRec As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RibbonStatusBar1 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmbTipoIngreso As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbEstado As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbMotivoDevolucion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents dtmFechaOrdenCompra As DevExpress.XtraEditors.DateEdit
    Friend WithEvents prgp As DevExpress.XtraWaitForm.ProgressPanel
    Friend WithEvents lblC As TextBox
    Friend WithEvents mnuEstadoEnviadoAERP As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tabLotes As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents DgridLotes As DevExpress.XtraGrid.GridControl
    Friend WithEvents gridviewLotes As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents EncabezadoBindingSource As BindingSource
    Friend WithEvents DsOC As DsOC
    Friend WithEvents colCódigo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNoOC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNoDocumento As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colFecha As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colEstado As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTipoTransacción As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDescripción As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMuelle As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DetalleBindingSource As BindingSource
    Friend WithEvents cmdRecepcionesAsociadas As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCerrarPedidoCompra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdBackorder As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdActualizarDetalle As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tabDetERP As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents grdDetERP As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GrpEmbarque As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtFechaAbordaje As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtPiezas As NumericUpDown
    Friend WithEvents txtCantidad As NumericUpDown
    Friend WithEvents txtCBM As NumericUpDown
    Friend WithEvents txtPesoKgs As NumericUpDown
    Friend WithEvents txtDestinatario As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtViaje As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPONumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDireccion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtBuque As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Descripcion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtRemitente As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BLNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPuertaDescarga As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GrpPoliza As DevExpress.XtraEditors.GroupControl
    Friend WithEvents BarButtonItem5 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RepositoryItemTextEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents txtScanPoliza As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lbScanPoliza As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dgridDetallePoliza As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvdetallepoliza As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmdPrepareGrid As Button
    Friend WithEvents lcmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents tabDetalleServicios As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents cmdImportarExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImportar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem6 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem7 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNoTicketTMS As DevExpress.XtraEditors.TextEdit
    Friend WithEvents grpTMS As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtNoPlacaTMS As TextBox
    Friend WithEvents txtNombresPilotoTMS As TextBox
    Friend WithEvents txtFechaIngresoTMS As TextBox
    Friend WithEvents txtTiempoEsperaTMS As TextBox
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents cmbDocumentoRef As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblDocumentoRef As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblObservacion As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblIdOrdenCompra As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblFechaDoc As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblBodega As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPropietario As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblEstado As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblNoDocumento As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblReferencia As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblProcedencia As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblNoTicketTMS As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblMotivoDevolucion As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblAcuerdoComercial As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents grpMotivoDevolucion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbOperadorDefecto As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grpScanPoliza As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents DgridDetalleOC As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDetalleDocIngreso As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents mnuTareaRecepcion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tabPedidosDevolucion As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl6 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents dgridPedidos As DataGridView
    Friend WithEvents IdPedido As DataGridViewTextBoxColumn
    Friend WithEvents Referencia As DataGridViewTextBoxColumn
    Friend WithEvents Bodega As DataGridViewTextBoxColumn
    Friend WithEvents Cliente As DataGridViewTextBoxColumn
    Friend WithEvents Propietario As DataGridViewTextBoxColumn
    Friend WithEvents FechaPedido As DataGridViewTextBoxColumn
    Friend WithEvents EstadoP As DataGridViewTextBoxColumn
    Friend WithEvents txtNombPedido As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkPedido As LinkLabel
    Friend WithEvents txtIdPedidoDevolucionEnc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents mnuExportarExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents txtDocumentoUbicacion As TextBox
    Friend WithEvents lblDocumentoUbicacion As DevExpress.XtraEditors.LabelControl
    Friend WithEvents mnuRegistrarEnNAV As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tabPolizaCorregida As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents DgridPolizas As DevExpress.XtraGrid.GridControl
    Friend WithEvents gridViewPolizas As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmdCorreccionPoliza As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmdImprimirEtiquetasRecepcion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdDuplicar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grpUltRec As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtIdRecepcion As LinkLabel
    Friend WithEvents txtNoDocumentoRecepcion As TextBox
    Friend WithEvents lblNoDocumentoRecepcion As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbAcuerdoComercial As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents dgridServiciosAsociados As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDetalleServicios As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtCodigoEmpresaERP As TextBox
    Friend WithEvents lblSociedadERP As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ToolStrip2 As ToolStrip
    Friend WithEvents cmdEliminarServicio As ToolStripButton
    Friend WithEvents cbCBM As NumericUpDown
    Friend WithEvents txtTotal_general As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTotal_liquidar As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtMod_transporte As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtClase As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNitImpExp As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtClaveAduana As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbRegimen As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtTotalPesoNeto As NumericUpDown
    Friend WithEvents dtpFechaLlegada As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtpFechaAceptacion As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtTotalOtros As NumericUpDown
    Friend WithEvents txtNumeroOrden As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTicket As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodigoPoliza As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dtFechaPoliza As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtValorSeguro As NumericUpDown
    Friend WithEvents txtTotalBulto As NumericUpDown
    Friend WithEvents txtTotalLineas As NumericUpDown
    Friend WithEvents txtTipoCambio As NumericUpDown
    Friend WithEvents txtTotalFOBUSD As NumericUpDown
    Friend WithEvents txtValorFlete As NumericUpDown
    Friend WithEvents txtTotalPesoBruto As NumericUpDown
    Friend WithEvents txtValorAduana As NumericUpDown
    Friend WithEvents txtNumeroDUA As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNoPoliza As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPaisProcedencia As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tabTallaColor As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgridTallaColor As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView7 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtNomCampaña As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdCampaña As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkCampaña As LinkLabel
    Friend WithEvents txtComentarios As TextBox
    Friend WithEvents lblComentarios As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grpDatosERP As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtUsuarioERP As TextBox
    Friend WithEvents lblUsuarioERP As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmdEliminarDocumento As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdPreImpresionOC As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdPreImpresionRFID As DevExpress.XtraBars.BarButtonItem
End Class
