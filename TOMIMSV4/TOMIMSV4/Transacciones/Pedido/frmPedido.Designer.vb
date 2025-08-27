<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPedido
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If pBePedidoEnc IsNot Nothing Then
                pBePedidoEnc.Dispose()
                pBePedidoEnc = Nothing
            End If
            If pBeCliente IsNot Nothing Then
                pBeCliente.Dispose()
                pBeCliente = Nothing
            End If
            If pBeProducto IsNot Nothing Then
                pBeProducto.Dispose()
                pBeProducto = Nothing
            End If
            If pBeStock IsNot Nothing Then
                pBeStock.Dispose()
                pBeStock = Nothing
            End If
            If pBeStockRes IsNot Nothing Then
                pBeStockRes.Dispose()
                pBeStockRes = Nothing
            End If
            If pBePedidoDet IsNot Nothing Then
                pBePedidoDet.Dispose()
                pBePedidoDet = Nothing
            End If
            If Propietario IsNot Nothing Then
                Propietario.Dispose()
                Propietario = Nothing
            End If
            If txtCodigoProductoGrid IsNot Nothing Then
                txtCodigoProductoGrid.Dispose()
                txtCodigoProductoGrid = Nothing
            End If
            If DgComboCliente IsNot Nothing Then
                DgComboCliente.Dispose()
                DgComboCliente = Nothing
            End If
            If DgComboEstado IsNot Nothing Then
                DgComboEstado.Dispose()
                DgComboEstado = Nothing
            End If
            If IdProductoCell IsNot Nothing Then
                IdProductoCell.Dispose()
                IdProductoCell = Nothing
            End If
            If CodProductoCell IsNot Nothing Then
                CodProductoCell.Dispose()
                CodProductoCell = Nothing
            End If
            If NomProductoCell IsNot Nothing Then
                NomProductoCell.Dispose()
                NomProductoCell = Nothing
            End If
            If CantidadCell IsNot Nothing Then
                CantidadCell.Dispose()
                CantidadCell = Nothing
            End If
            If PesoCell IsNot Nothing Then
                PesoCell.Dispose()
                PesoCell = Nothing
            End If
            If PrecioCell IsNot Nothing Then
                PrecioCell.Dispose()
                PrecioCell = Nothing
            End If
            If TotalCell IsNot Nothing Then
                TotalCell.Dispose()
                TotalCell = Nothing
            End If
            If CantDisp IsNot Nothing Then
                CantDisp.Dispose()
                CantDisp = Nothing
            End If
            If EstadoCell IsNot Nothing Then
                EstadoCell.Dispose()
                EstadoCell = Nothing
            End If
            If PresentacionCell IsNot Nothing Then
                PresentacionCell.Dispose()
                PresentacionCell = Nothing
            End If
            If UnidadMedidaCell IsNot Nothing Then
                UnidadMedidaCell.Dispose()
                UnidadMedidaCell = Nothing
            End If
            If IdPedidoDetCell IsNot Nothing Then
                IdPedidoDetCell.Dispose()
                IdPedidoDetCell = Nothing
            End If
            If NoDiasVencimientoCell IsNot Nothing Then
                NoDiasVencimientoCell.Dispose()
                NoDiasVencimientoCell = Nothing
            End If
            If SerieProductoCell IsNot Nothing Then
                SerieProductoCell.Dispose()
                SerieProductoCell = Nothing
            End If
            If NoLineaCell IsNot Nothing Then
                NoLineaCell.Dispose()
                NoLineaCell = Nothing
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
        Dim lblBodega As System.Windows.Forms.Label
        Dim lblNoDocumento As System.Windows.Forms.Label
        Dim lbCodigoPedidoEnc As System.Windows.Forms.Label
        Dim lblPropietario As System.Windows.Forms.Label
        Dim lblMuelle As System.Windows.Forms.Label
        Dim lblSEstado As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim lblDiasVencimiento As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim RoadKilometrajeLabel As System.Windows.Forms.Label
        Dim RoadTotalLabel As System.Windows.Forms.Label
        Dim RoadDesMontoLabel As System.Windows.Forms.Label
        Dim RoadImpMontoLabel As System.Windows.Forms.Label
        Dim RoadPesoLabel As System.Windows.Forms.Label
        Dim RoadBanderaLabel As System.Windows.Forms.Label
        Dim RoadStatComLabel As System.Windows.Forms.Label
        Dim RoadCalcoBJLabel As System.Windows.Forms.Label
        Dim RoadImpresLabel As System.Windows.Forms.Label
        Dim RoadADD1Label As System.Windows.Forms.Label
        Dim RoadADD2Label As System.Windows.Forms.Label
        Dim RoadADD3Label As System.Windows.Forms.Label
        Dim RoadStatProcLabel As System.Windows.Forms.Label
        Dim RoadRechazadoLabel As System.Windows.Forms.Label
        Dim RoadRazon_RechazadoLabel As System.Windows.Forms.Label
        Dim RoadInformadoLabel As System.Windows.Forms.Label
        Dim RoadSucursalLabel As System.Windows.Forms.Label
        Dim RoadIdDespachoLabel As System.Windows.Forms.Label
        Dim RoadIdFacturacionLabel As System.Windows.Forms.Label
        Dim ObservacionLabel As System.Windows.Forms.Label
        Dim lblTipoPedido As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label13 As System.Windows.Forms.Label
        Dim Label14 As System.Windows.Forms.Label
        Dim Label15 As System.Windows.Forms.Label
        Dim lblNoPedido As System.Windows.Forms.Label
        Dim lblControlUltimoLote As System.Windows.Forms.Label
        Dim Label22 As System.Windows.Forms.Label
        Dim Label21 As System.Windows.Forms.Label
        Dim Label20 As System.Windows.Forms.Label
        Dim Label19 As System.Windows.Forms.Label
        Dim Label23 As System.Windows.Forms.Label
        Dim Label26 As System.Windows.Forms.Label
        Dim Label27 As System.Windows.Forms.Label
        Dim Label28 As System.Windows.Forms.Label
        Dim Label30 As System.Windows.Forms.Label
        Dim Label32 As System.Windows.Forms.Label
        Dim Label33 As System.Windows.Forms.Label
        Dim Label34 As System.Windows.Forms.Label
        Dim Label35 As System.Windows.Forms.Label
        Dim Label37 As System.Windows.Forms.Label
        Dim Label17 As System.Windows.Forms.Label
        Dim lblCertificadoCalidad As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim lblVendedorDespacho As System.Windows.Forms.Label
        Dim lblRutaDespacho As System.Windows.Forms.Label
        Dim lblRoadVendedor As System.Windows.Forms.Label
        Dim lblRoadRuta As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim lblFechaPedido As System.Windows.Forms.Label
        Dim Label16 As System.Windows.Forms.Label
        Dim lblManufacturaLigera As System.Windows.Forms.Label
        Dim Label44 As System.Windows.Forms.Label
        Dim Label43 As System.Windows.Forms.Label
        Dim Label42 As System.Windows.Forms.Label
        Dim Label41 As System.Windows.Forms.Label
        Dim Label40 As System.Windows.Forms.Label
        Dim Label39 As System.Windows.Forms.Label
        Dim Label38 As System.Windows.Forms.Label
        Dim lblRegimen As System.Windows.Forms.Label
        Dim lblPesoNeto As System.Windows.Forms.Label
        Dim Label18 As System.Windows.Forms.Label
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPedido))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim PushTransition1 As DevExpress.Utils.Animation.PushTransition = New DevExpress.Utils.Animation.PushTransition()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.lblBodegaDestino = New System.Windows.Forms.Label()
        Me.lblBodegaOrigen = New System.Windows.Forms.Label()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
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
        Me.cmdImprimir = New DevExpress.XtraBars.BarSubItem()
        Me.cmdListaUbicacion = New DevExpress.XtraBars.BarButtonItem()
        Me.tsmiImprimirStockRes = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImprimirHojaVerificacion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImprimirGridDocumentoERP = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEstadoEnviadoAERP = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuLiberarNoPickeado = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem5 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuReservaStockManual = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPendiente = New DevExpress.XtraBars.BarButtonItem()
        Me.chkActivo = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkAnulado = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkPedidoLocal = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkSyncMI3 = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkPalletPrimero = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkControlPoliza = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkVerificar = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.BarDockingMenuItem1 = New DevExpress.XtraBars.BarDockingMenuItem()
        Me.mnuDespachado = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuLiberarStockTodoElPedido = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuLiberarStockProductoEspecifico = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminarPedidoTablaIntermedia = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminarPedido = New DevExpress.XtraBars.BarButtonItem()
        Me.chkRequiereTarimas = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.mnuCrearPicking = New DevExpress.XtraBars.BarButtonItem()
        Me.chkFotografiaVerificacion = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.mnuRevertirDespacho = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup4 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup5 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup6 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage3 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GrpProducto = New DevExpress.XtraEditors.GroupControl()
        Me.XtraScrollableControl2 = New DevExpress.XtraEditors.XtraScrollableControl()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.dtpFechaEntrega = New DevExpress.XtraEditors.DateEdit()
        Me.cmbRoadVendedorDespacho = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbRoadVendedorPedido = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbRoadRutaDespacho = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbRoadRutaPedido = New DevExpress.XtraEditors.LookUpEdit()
        Me.dtpHoraEntregaHasta = New System.Windows.Forms.DateTimePicker()
        Me.txtDireccionEntrega = New System.Windows.Forms.TextBox()
        Me.dtpHoraEntregaDesde = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtEsExportacion = New System.Windows.Forms.TextBox()
        Me.lblSociedadSAP = New System.Windows.Forms.Label()
        Me.txtSociedadSAP = New System.Windows.Forms.TextBox()
        Me.cmbMuelle = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView12 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtIdUbicacionMuelle = New System.Windows.Forms.TextBox()
        Me.cmbMotivoDevolucion = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblMotivoDevolucion = New DevExpress.XtraEditors.LabelControl()
        Me.txtReferencia2 = New DevExpress.XtraEditors.TextEdit()
        Me.txtBodegaDestino = New System.Windows.Forms.TextBox()
        Me.txtBodegaOrigen = New System.Windows.Forms.TextBox()
        Me.grpUltTareaManufactura = New DevExpress.XtraEditors.GroupControl()
        Me.txtIdManufactura = New System.Windows.Forms.LinkLabel()
        Me.cmbManufacturaLigera = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblprg = New System.Windows.Forms.RichTextBox()
        Me.lblUbicacionAbastecimiento = New System.Windows.Forms.Label()
        Me.txtIdUbicacionAbastecimiento = New System.Windows.Forms.TextBox()
        Me.dtpFechaPreparacion = New DevExpress.XtraEditors.DateEdit()
        Me.txtIdCliente = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtDiasVencimiento = New System.Windows.Forms.NumericUpDown()
        Me.txtCertificadoCalidad = New System.Windows.Forms.TextBox()
        Me.txtNoPickingERP = New DevExpress.XtraEditors.TextEdit()
        Me.txtNoDocumento = New System.Windows.Forms.TextBox()
        Me.cmbDocumentoRef = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblDocumentoRef = New DevExpress.XtraEditors.LabelControl()
        Me.lnkCliente = New System.Windows.Forms.LinkLabel()
        Me.txtReferencia = New DevExpress.XtraEditors.TextEdit()
        Me.lblEstado = New System.Windows.Forms.Label()
        Me.dtpHoraInicioPreparacion = New System.Windows.Forms.DateTimePicker()
        Me.lcmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblUbicTransito = New System.Windows.Forms.Label()
        Me.dtpHoraFinPreparacion = New System.Windows.Forms.DateTimePicker()
        Me.txtControlUltimoLote = New System.Windows.Forms.TextBox()
        Me.txtUbicacionTransito = New System.Windows.Forms.TextBox()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblIdPedidoEnc = New System.Windows.Forms.TextBox()
        Me.cmbTipoPedido = New DevExpress.XtraEditors.LookUpEdit()
        Me.dtpFechaPedido = New DevExpress.XtraEditors.DateEdit()
        Me.grpInfoPicking = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.txtObservacion = New System.Windows.Forms.TextBox()
        Me.grpScanPoliza = New DevExpress.XtraEditors.GroupControl()
        Me.txtScanPoliza = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.txtIdDespacho = New System.Windows.Forms.LinkLabel()
        Me.lblUltDespacho = New System.Windows.Forms.Label()
        Me.txtIdPicking = New System.Windows.Forms.LinkLabel()
        Me.lblNoPicking = New System.Windows.Forms.Label()
        Me.grpDetallePed = New DevExpress.XtraEditors.GroupControl()
        Me.dgrid = New System.Windows.Forms.DataGridView()
        Me.colNo_Linea = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIdProducto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIsNew = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColCodProducto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColNomProducto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUnidadMedida = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPresentacion = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.colEstadoProducto = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.colCantidadExistencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPesoExistencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColCantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColPeso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColPrecio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColIdStockRes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNoDias = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColFechaEspecifica = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colNoSerie = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPesoUnitario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CantidadPickeada = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CantidadVerificada = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Atributo_Variante_1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdStockEspecifico = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIdProductoBodega = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIdPedidoDet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdCliente = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblCantidad = New System.Windows.Forms.Label()
        Me.lblPeso = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lnkAgregarStockEspecifico = New System.Windows.Forms.LinkLabel()
        Me.lnkParametrosProducto = New System.Windows.Forms.LinkLabel()
        Me.lnkVerConfiguracionProducto = New System.Windows.Forms.LinkLabel()
        Me.lnkAgregarProducto = New System.Windows.Forms.LinkLabel()
        Me.prg = New DevExpress.XtraWaitForm.ProgressPanel()
        Me.ObservacionTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.RoadIdFacturacionSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.RoadIdDespachoSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.RoadSucursalTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.RoadInformadoCheckEdit = New DevExpress.XtraEditors.CheckEdit()
        Me.RoadRazon_RechazadoTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.RoadRechazadoCheckEdit = New DevExpress.XtraEditors.CheckEdit()
        Me.RoadStatProcTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.RoadADD3TextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.RoadADD2TextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.RoadADD1TextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.RoadImpresSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.RoadCalcoBJTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.RoadStatComTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.RoadBanderaTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.RoadPesoSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.RoadImpMontoSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.RoadDesMontoSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.RoadTotalSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.RoadKilometrajeSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.dkPedido = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.AutoHideContainer2 = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel2 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel2_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.Fec_modDateEdit1 = New DevExpress.XtraEditors.DateEdit()
        Me.Fec_agrDateEdit1 = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit1 = New System.Windows.Forms.TextBox()
        Me.User_agrTextEdit1 = New System.Windows.Forms.TextBox()
        Me.xtrPedido = New DevExpress.XtraTab.XtraTabControl()
        Me.tpDatosGenerales = New DevExpress.XtraTab.XtraTabPage()
        Me.tabPoliza = New DevExpress.XtraTab.XtraTabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.GrpPoliza = New DevExpress.XtraEditors.GroupControl()
        Me.XtraScrollableControl1 = New DevExpress.XtraEditors.XtraScrollableControl()
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
        Me.tpDetalleProducto = New DevExpress.XtraTab.XtraTabPage()
        Me.tpRoadRAW = New DevExpress.XtraTab.XtraTabPage()
        Me.grRoad = New DevExpress.XtraEditors.GroupControl()
        Me.tpStock_Reservado = New DevExpress.XtraTab.XtraTabPage()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.grdStockReservado = New DevExpress.XtraGrid.GridControl()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tpPicking = New DevExpress.XtraTab.XtraTabPage()
        Me.grpPicking = New DevExpress.XtraEditors.GroupControl()
        Me.grdPicking = New DevExpress.XtraGrid.GridControl()
        Me.EncabezadoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsPicking = New TOMWMS.DsPicking()
        Me.gvDetallePicking = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCódigo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.tpDespachos = New DevExpress.XtraTab.XtraTabPage()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.grdDespacho = New DevExpress.XtraGrid.GridControl()
        Me.EncabezadoBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsDespacho = New TOMWMS.DsDespacho()
        Me.GridView7 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colIdDespachoEnc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colFecha_Desp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colHora_ini = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colHora_fin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdPedidoEnc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.tabPedidoERP = New DevExpress.XtraTab.XtraTabPage()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.grdPedTras = New DevExpress.XtraGrid.GridControl()
        Me.GridView8 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabComposicion = New DevExpress.XtraTab.XtraTabPage()
        Me.PanelControl4 = New DevExpress.XtraEditors.PanelControl()
        Me.grdComposicion = New DevExpress.XtraGrid.GridControl()
        Me.grdVComp = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TabServiciosAsociados = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridServiciosAsociados = New DevExpress.XtraGrid.GridControl()
        Me.gvDetalleServicios = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cmbAcuerdoComercial = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblAcuerdoComercial = New DevExpress.XtraEditors.LabelControl()
        Me.tabStockLiberado = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridStockLiberado = New DevExpress.XtraGrid.GridControl()
        Me.gvLogStockLiberado = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabLogMI3 = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridLogMI3 = New DevExpress.XtraGrid.GridControl()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabHojaVerificacion = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridVerificacion = New DevExpress.XtraGrid.GridControl()
        Me.GridView9 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabImagenes = New DevExpress.XtraTab.XtraTabPage()
        Me.GrpImagen = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GrdImagen = New DevExpress.XtraGrid.GridControl()
        Me.GridViewImg = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.cmdAdd = New System.Windows.Forms.ToolStripButton()
        Me.cmdDelete = New System.Windows.Forms.ToolStripButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.PicImg = New System.Windows.Forms.PictureBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.tabLogReserva = New DevExpress.XtraTab.XtraTabPage()
        Me.dgrdLogReserva = New DevExpress.XtraGrid.GridControl()
        Me.GridView10 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabExistencias = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridExistencias = New DevExpress.XtraGrid.GridControl()
        Me.GridView11 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.DetalleBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DetalleBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.AutoHideContainer1 = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.WorkspaceManager1 = New DevExpress.Utils.WorkspaceManager(Me.components)
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        lblBodega = New System.Windows.Forms.Label()
        lblNoDocumento = New System.Windows.Forms.Label()
        lbCodigoPedidoEnc = New System.Windows.Forms.Label()
        lblPropietario = New System.Windows.Forms.Label()
        lblMuelle = New System.Windows.Forms.Label()
        lblSEstado = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        lblDiasVencimiento = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        RoadKilometrajeLabel = New System.Windows.Forms.Label()
        RoadTotalLabel = New System.Windows.Forms.Label()
        RoadDesMontoLabel = New System.Windows.Forms.Label()
        RoadImpMontoLabel = New System.Windows.Forms.Label()
        RoadPesoLabel = New System.Windows.Forms.Label()
        RoadBanderaLabel = New System.Windows.Forms.Label()
        RoadStatComLabel = New System.Windows.Forms.Label()
        RoadCalcoBJLabel = New System.Windows.Forms.Label()
        RoadImpresLabel = New System.Windows.Forms.Label()
        RoadADD1Label = New System.Windows.Forms.Label()
        RoadADD2Label = New System.Windows.Forms.Label()
        RoadADD3Label = New System.Windows.Forms.Label()
        RoadStatProcLabel = New System.Windows.Forms.Label()
        RoadRechazadoLabel = New System.Windows.Forms.Label()
        RoadRazon_RechazadoLabel = New System.Windows.Forms.Label()
        RoadInformadoLabel = New System.Windows.Forms.Label()
        RoadSucursalLabel = New System.Windows.Forms.Label()
        RoadIdDespachoLabel = New System.Windows.Forms.Label()
        RoadIdFacturacionLabel = New System.Windows.Forms.Label()
        ObservacionLabel = New System.Windows.Forms.Label()
        lblTipoPedido = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label13 = New System.Windows.Forms.Label()
        Label14 = New System.Windows.Forms.Label()
        Label15 = New System.Windows.Forms.Label()
        lblNoPedido = New System.Windows.Forms.Label()
        lblControlUltimoLote = New System.Windows.Forms.Label()
        Label22 = New System.Windows.Forms.Label()
        Label21 = New System.Windows.Forms.Label()
        Label20 = New System.Windows.Forms.Label()
        Label19 = New System.Windows.Forms.Label()
        Label23 = New System.Windows.Forms.Label()
        Label26 = New System.Windows.Forms.Label()
        Label27 = New System.Windows.Forms.Label()
        Label28 = New System.Windows.Forms.Label()
        Label30 = New System.Windows.Forms.Label()
        Label32 = New System.Windows.Forms.Label()
        Label33 = New System.Windows.Forms.Label()
        Label34 = New System.Windows.Forms.Label()
        Label35 = New System.Windows.Forms.Label()
        Label37 = New System.Windows.Forms.Label()
        Label17 = New System.Windows.Forms.Label()
        lblCertificadoCalidad = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        lblVendedorDespacho = New System.Windows.Forms.Label()
        lblRutaDespacho = New System.Windows.Forms.Label()
        lblRoadVendedor = New System.Windows.Forms.Label()
        lblRoadRuta = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        lblFechaPedido = New System.Windows.Forms.Label()
        Label16 = New System.Windows.Forms.Label()
        lblManufacturaLigera = New System.Windows.Forms.Label()
        Label44 = New System.Windows.Forms.Label()
        Label43 = New System.Windows.Forms.Label()
        Label42 = New System.Windows.Forms.Label()
        Label41 = New System.Windows.Forms.Label()
        Label40 = New System.Windows.Forms.Label()
        Label39 = New System.Windows.Forms.Label()
        Label38 = New System.Windows.Forms.Label()
        lblRegimen = New System.Windows.Forms.Label()
        lblPesoNeto = New System.Windows.Forms.Label()
        Label18 = New System.Windows.Forms.Label()
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
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpProducto.SuspendLayout()
        Me.XtraScrollableControl2.SuspendLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.dtpFechaEntrega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaEntrega.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbRoadVendedorDespacho.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbRoadVendedorPedido.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbRoadRutaDespacho.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbRoadRutaPedido.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.cmbMuelle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbMotivoDevolucion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReferencia2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpUltTareaManufactura, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpUltTareaManufactura.SuspendLayout()
        CType(Me.cmbManufacturaLigera.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaPreparacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaPreparacion.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdCliente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiasVencimiento, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoPickingERP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbDocumentoRef.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReferencia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lcmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTipoPedido.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaPedido.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaPedido.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpInfoPicking, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpInfoPicking.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.grpScanPoliza, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpScanPoliza.SuspendLayout()
        CType(Me.txtScanPoliza.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpDetallePed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDetallePed.SuspendLayout()
        CType(Me.dgrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.ObservacionTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoadIdFacturacionSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoadIdDespachoSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoadSucursalTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoadInformadoCheckEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoadRazon_RechazadoTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoadRechazadoCheckEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoadStatProcTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoadADD3TextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoadADD2TextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoadADD1TextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoadImpresSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoadCalcoBJTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoadStatComTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoadBanderaTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoadPesoSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoadImpMontoSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoadDesMontoSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoadTotalSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoadKilometrajeSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkPedido, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.AutoHideContainer2.SuspendLayout()
        Me.DockPanel2.SuspendLayout()
        Me.DockPanel2_Container.SuspendLayout()
        CType(Me.Fec_modDateEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtrPedido, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtrPedido.SuspendLayout()
        Me.tpDatosGenerales.SuspendLayout()
        Me.tabPoliza.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.GrpPoliza, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpPoliza.SuspendLayout()
        Me.XtraScrollableControl1.SuspendLayout()
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
        Me.tpDetalleProducto.SuspendLayout()
        Me.tpRoadRAW.SuspendLayout()
        CType(Me.grRoad, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grRoad.SuspendLayout()
        Me.tpStock_Reservado.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.grdStockReservado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpPicking.SuspendLayout()
        CType(Me.grpPicking, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPicking.SuspendLayout()
        CType(Me.grdPicking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EncabezadoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsPicking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetallePicking, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpDespachos.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.grdDespacho, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EncabezadoBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsDespacho, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPedidoERP.SuspendLayout()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.grdPedTras, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabComposicion.SuspendLayout()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl4.SuspendLayout()
        CType(Me.grdComposicion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdVComp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabServiciosAsociados.SuspendLayout()
        CType(Me.dgridServiciosAsociados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetalleServicios, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbAcuerdoComercial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabStockLiberado.SuspendLayout()
        CType(Me.dgridStockLiberado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvLogStockLiberado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabLogMI3.SuspendLayout()
        CType(Me.dgridLogMI3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabHojaVerificacion.SuspendLayout()
        CType(Me.dgridVerificacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabImagenes.SuspendLayout()
        CType(Me.GrpImagen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpImagen.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GrdImagen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewImg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.PicImg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabLogReserva.SuspendLayout()
        CType(Me.dgrdLogReserva, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabExistencias.SuspendLayout()
        CType(Me.dgridExistencias, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DetalleBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DetalleBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(269, 15)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(85, 13)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(269, 41)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(78, 13)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(684, 15)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(89, 13)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(684, 41)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(82, 13)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'lblBodega
        '
        lblBodega.AutoSize = True
        lblBodega.Location = New System.Drawing.Point(12, 119)
        lblBodega.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblBodega.Name = "lblBodega"
        lblBodega.Size = New System.Drawing.Size(54, 16)
        lblBodega.TabIndex = 8
        lblBodega.Text = "Bodega:"
        '
        'lblNoDocumento
        '
        lblNoDocumento.AutoSize = True
        lblNoDocumento.Location = New System.Drawing.Point(12, 272)
        lblNoDocumento.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblNoDocumento.Name = "lblNoDocumento"
        lblNoDocumento.Size = New System.Drawing.Size(99, 16)
        lblNoDocumento.TabIndex = 28
        lblNoDocumento.Text = "No. Documento:"
        '
        'lbCodigoPedidoEnc
        '
        lbCodigoPedidoEnc.AutoSize = True
        lbCodigoPedidoEnc.Location = New System.Drawing.Point(12, 23)
        lbCodigoPedidoEnc.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lbCodigoPedidoEnc.Name = "lbCodigoPedidoEnc"
        lbCodigoPedidoEnc.Size = New System.Drawing.Size(51, 16)
        lbCodigoPedidoEnc.TabIndex = 0
        lbCodigoPedidoEnc.Text = "Código:"
        '
        'lblPropietario
        '
        lblPropietario.AutoSize = True
        lblPropietario.Location = New System.Drawing.Point(12, 180)
        lblPropietario.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblPropietario.Name = "lblPropietario"
        lblPropietario.Size = New System.Drawing.Size(74, 16)
        lblPropietario.TabIndex = 15
        lblPropietario.Text = "Propietario:"
        '
        'lblMuelle
        '
        lblMuelle.AutoSize = True
        lblMuelle.Location = New System.Drawing.Point(12, 327)
        lblMuelle.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblMuelle.Name = "lblMuelle"
        lblMuelle.Size = New System.Drawing.Size(49, 16)
        lblMuelle.TabIndex = 33
        lblMuelle.Text = "Muelle:"
        '
        'lblSEstado
        '
        lblSEstado.AutoSize = True
        lblSEstado.Location = New System.Drawing.Point(12, 57)
        lblSEstado.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblSEstado.Name = "lblSEstado"
        lblSEstado.Size = New System.Drawing.Size(50, 16)
        lblSEstado.TabIndex = 5
        lblSEstado.Text = "Estado:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(12, 241)
        Label3.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(88, 16)
        Label3.TabIndex = 30
        Label3.Text = "Fecha Pedido:"
        '
        'lblDiasVencimiento
        '
        lblDiasVencimiento.AutoSize = True
        lblDiasVencimiento.Location = New System.Drawing.Point(756, 185)
        lblDiasVencimiento.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblDiasVencimiento.Name = "lblDiasVencimiento"
        lblDiasVencimiento.Size = New System.Drawing.Size(145, 16)
        lblDiasVencimiento.TabIndex = 25
        lblDiasVencimiento.Text = "Calcular vencimiento a :"
        lblDiasVencimiento.Visible = False
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(862, 208)
        Label6.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(31, 16)
        Label6.TabIndex = 27
        Label6.Text = "Días"
        Label6.Visible = False
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(671, 240)
        Label10.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(114, 16)
        Label10.TabIndex = 31
        Label10.Text = "Inicio Preparación:"
        '
        'Label11
        '
        Label11.AutoSize = True
        Label11.Location = New System.Drawing.Point(790, 239)
        Label11.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(101, 16)
        Label11.TabIndex = 32
        Label11.Text = "Fin Preparación:"
        '
        'RoadKilometrajeLabel
        '
        RoadKilometrajeLabel.AutoSize = True
        RoadKilometrajeLabel.Location = New System.Drawing.Point(24, 54)
        RoadKilometrajeLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        RoadKilometrajeLabel.Name = "RoadKilometrajeLabel"
        RoadKilometrajeLabel.Size = New System.Drawing.Size(110, 16)
        RoadKilometrajeLabel.TabIndex = 0
        RoadKilometrajeLabel.Text = "Road Kilometraje:"
        '
        'RoadTotalLabel
        '
        RoadTotalLabel.AutoSize = True
        RoadTotalLabel.Location = New System.Drawing.Point(24, 84)
        RoadTotalLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        RoadTotalLabel.Name = "RoadTotalLabel"
        RoadTotalLabel.Size = New System.Drawing.Size(74, 16)
        RoadTotalLabel.TabIndex = 4
        RoadTotalLabel.Text = "Road Total:"
        '
        'RoadDesMontoLabel
        '
        RoadDesMontoLabel.AutoSize = True
        RoadDesMontoLabel.Location = New System.Drawing.Point(24, 112)
        RoadDesMontoLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        RoadDesMontoLabel.Name = "RoadDesMontoLabel"
        RoadDesMontoLabel.Size = New System.Drawing.Size(105, 16)
        RoadDesMontoLabel.TabIndex = 8
        RoadDesMontoLabel.Text = "Road Des Monto:"
        '
        'RoadImpMontoLabel
        '
        RoadImpMontoLabel.AutoSize = True
        RoadImpMontoLabel.Location = New System.Drawing.Point(24, 143)
        RoadImpMontoLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        RoadImpMontoLabel.Name = "RoadImpMontoLabel"
        RoadImpMontoLabel.Size = New System.Drawing.Size(106, 16)
        RoadImpMontoLabel.TabIndex = 12
        RoadImpMontoLabel.Text = "Road Imp Monto:"
        '
        'RoadPesoLabel
        '
        RoadPesoLabel.AutoSize = True
        RoadPesoLabel.Location = New System.Drawing.Point(24, 175)
        RoadPesoLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        RoadPesoLabel.Name = "RoadPesoLabel"
        RoadPesoLabel.Size = New System.Drawing.Size(72, 16)
        RoadPesoLabel.TabIndex = 16
        RoadPesoLabel.Text = "Road Peso:"
        '
        'RoadBanderaLabel
        '
        RoadBanderaLabel.AutoSize = True
        RoadBanderaLabel.Location = New System.Drawing.Point(24, 206)
        RoadBanderaLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        RoadBanderaLabel.Name = "RoadBanderaLabel"
        RoadBanderaLabel.Size = New System.Drawing.Size(92, 16)
        RoadBanderaLabel.TabIndex = 20
        RoadBanderaLabel.Text = "Road Bandera:"
        '
        'RoadStatComLabel
        '
        RoadStatComLabel.AutoSize = True
        RoadStatComLabel.Location = New System.Drawing.Point(24, 236)
        RoadStatComLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        RoadStatComLabel.Name = "RoadStatComLabel"
        RoadStatComLabel.Size = New System.Drawing.Size(98, 16)
        RoadStatComLabel.TabIndex = 22
        RoadStatComLabel.Text = "Road Stat Com:"
        '
        'RoadCalcoBJLabel
        '
        RoadCalcoBJLabel.AutoSize = True
        RoadCalcoBJLabel.Location = New System.Drawing.Point(24, 266)
        RoadCalcoBJLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        RoadCalcoBJLabel.Name = "RoadCalcoBJLabel"
        RoadCalcoBJLabel.Size = New System.Drawing.Size(92, 16)
        RoadCalcoBJLabel.TabIndex = 26
        RoadCalcoBJLabel.Text = "Road Calco BJ:"
        '
        'RoadImpresLabel
        '
        RoadImpresLabel.AutoSize = True
        RoadImpresLabel.Location = New System.Drawing.Point(24, 295)
        RoadImpresLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        RoadImpresLabel.Name = "RoadImpresLabel"
        RoadImpresLabel.Size = New System.Drawing.Size(85, 16)
        RoadImpresLabel.TabIndex = 30
        RoadImpresLabel.Text = "Road Impres:"
        '
        'RoadADD1Label
        '
        RoadADD1Label.AutoSize = True
        RoadADD1Label.Location = New System.Drawing.Point(512, 206)
        RoadADD1Label.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        RoadADD1Label.Name = "RoadADD1Label"
        RoadADD1Label.Size = New System.Drawing.Size(76, 16)
        RoadADD1Label.TabIndex = 24
        RoadADD1Label.Text = "Road ADD1:"
        '
        'RoadADD2Label
        '
        RoadADD2Label.AutoSize = True
        RoadADD2Label.Location = New System.Drawing.Point(512, 231)
        RoadADD2Label.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        RoadADD2Label.Name = "RoadADD2Label"
        RoadADD2Label.Size = New System.Drawing.Size(76, 16)
        RoadADD2Label.TabIndex = 28
        RoadADD2Label.Text = "Road ADD2:"
        '
        'RoadADD3Label
        '
        RoadADD3Label.AutoSize = True
        RoadADD3Label.Location = New System.Drawing.Point(512, 260)
        RoadADD3Label.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        RoadADD3Label.Name = "RoadADD3Label"
        RoadADD3Label.Size = New System.Drawing.Size(76, 16)
        RoadADD3Label.TabIndex = 32
        RoadADD3Label.Text = "Road ADD3:"
        '
        'RoadStatProcLabel
        '
        RoadStatProcLabel.AutoSize = True
        RoadStatProcLabel.Location = New System.Drawing.Point(512, 295)
        RoadStatProcLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        RoadStatProcLabel.Name = "RoadStatProcLabel"
        RoadStatProcLabel.Size = New System.Drawing.Size(97, 16)
        RoadStatProcLabel.TabIndex = 34
        RoadStatProcLabel.Text = "Road Stat Proc:"
        '
        'RoadRechazadoLabel
        '
        RoadRechazadoLabel.AutoSize = True
        RoadRechazadoLabel.Location = New System.Drawing.Point(522, 875)
        RoadRechazadoLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        RoadRechazadoLabel.Name = "RoadRechazadoLabel"
        RoadRechazadoLabel.Size = New System.Drawing.Size(107, 16)
        RoadRechazadoLabel.TabIndex = 36
        RoadRechazadoLabel.Text = "Road Rechazado:"
        '
        'RoadRazon_RechazadoLabel
        '
        RoadRazon_RechazadoLabel.AutoSize = True
        RoadRazon_RechazadoLabel.Location = New System.Drawing.Point(522, 935)
        RoadRazon_RechazadoLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        RoadRazon_RechazadoLabel.Name = "RoadRazon_RechazadoLabel"
        RoadRazon_RechazadoLabel.Size = New System.Drawing.Size(146, 16)
        RoadRazon_RechazadoLabel.TabIndex = 38
        RoadRazon_RechazadoLabel.Text = "Road Razon Rechazado:"
        '
        'RoadInformadoLabel
        '
        RoadInformadoLabel.AutoSize = True
        RoadInformadoLabel.Location = New System.Drawing.Point(512, 146)
        RoadInformadoLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        RoadInformadoLabel.Name = "RoadInformadoLabel"
        RoadInformadoLabel.Size = New System.Drawing.Size(104, 16)
        RoadInformadoLabel.TabIndex = 15
        RoadInformadoLabel.Text = "Road Informado:"
        '
        'RoadSucursalLabel
        '
        RoadSucursalLabel.AutoSize = True
        RoadSucursalLabel.Location = New System.Drawing.Point(512, 175)
        RoadSucursalLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        RoadSucursalLabel.Name = "RoadSucursalLabel"
        RoadSucursalLabel.Size = New System.Drawing.Size(94, 16)
        RoadSucursalLabel.TabIndex = 18
        RoadSucursalLabel.Text = "Road Sucursal:"
        '
        'RoadIdDespachoLabel
        '
        RoadIdDespachoLabel.AutoSize = True
        RoadIdDespachoLabel.Location = New System.Drawing.Point(512, 116)
        RoadIdDespachoLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        RoadIdDespachoLabel.Name = "RoadIdDespachoLabel"
        RoadIdDespachoLabel.Size = New System.Drawing.Size(115, 16)
        RoadIdDespachoLabel.TabIndex = 10
        RoadIdDespachoLabel.Text = "Road Id Despacho:"
        '
        'RoadIdFacturacionLabel
        '
        RoadIdFacturacionLabel.AutoSize = True
        RoadIdFacturacionLabel.Location = New System.Drawing.Point(512, 84)
        RoadIdFacturacionLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        RoadIdFacturacionLabel.Name = "RoadIdFacturacionLabel"
        RoadIdFacturacionLabel.Size = New System.Drawing.Size(126, 16)
        RoadIdFacturacionLabel.TabIndex = 6
        RoadIdFacturacionLabel.Text = "Road Id Facturacion:"
        '
        'ObservacionLabel
        '
        ObservacionLabel.AutoSize = True
        ObservacionLabel.Location = New System.Drawing.Point(512, 54)
        ObservacionLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        ObservacionLabel.Name = "ObservacionLabel"
        ObservacionLabel.Size = New System.Drawing.Size(82, 16)
        ObservacionLabel.TabIndex = 2
        ObservacionLabel.Text = "Observacion:"
        '
        'lblTipoPedido
        '
        lblTipoPedido.AutoSize = True
        lblTipoPedido.Location = New System.Drawing.Point(12, 91)
        lblTipoPedido.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblTipoPedido.Name = "lblTipoPedido"
        lblTipoPedido.Size = New System.Drawing.Size(105, 16)
        lblTipoPedido.TabIndex = 3
        lblTipoPedido.Text = "Tipo Documento:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(165, 41)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(78, 13)
        Label5.TabIndex = 4
        Label5.Text = "Fecha Agregó:"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(580, 15)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(89, 13)
        Label7.TabIndex = 3
        Label7.Text = "Usuario Modificó:"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(165, 15)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(85, 13)
        Label8.TabIndex = 0
        Label8.Text = "Usuario Agregó:"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(580, 41)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(82, 13)
        Label9.TabIndex = 6
        Label9.Text = "Fecha Modificó:"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(188, 22)
        Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(100, 16)
        Label12.TabIndex = 0
        Label12.Text = "Usuario Agregó:"
        '
        'Label13
        '
        Label13.AutoSize = True
        Label13.Location = New System.Drawing.Point(188, 54)
        Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label13.Name = "Label13"
        Label13.Size = New System.Drawing.Size(91, 16)
        Label13.TabIndex = 4
        Label13.Text = "Fecha Agregó:"
        '
        'Label14
        '
        Label14.AutoSize = True
        Label14.Location = New System.Drawing.Point(672, 22)
        Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label14.Name = "Label14"
        Label14.Size = New System.Drawing.Size(106, 16)
        Label14.TabIndex = 2
        Label14.Text = "Usuario Modificó:"
        '
        'Label15
        '
        Label15.AutoSize = True
        Label15.Location = New System.Drawing.Point(672, 54)
        Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label15.Name = "Label15"
        Label15.Size = New System.Drawing.Size(97, 16)
        Label15.TabIndex = 6
        Label15.Text = "Fecha Modificó:"
        '
        'lblNoPedido
        '
        lblNoPedido.AutoSize = True
        lblNoPedido.Location = New System.Drawing.Point(12, 302)
        lblNoPedido.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblNoPedido.Name = "lblNoPedido"
        lblNoPedido.Size = New System.Drawing.Size(73, 16)
        lblNoPedido.TabIndex = 53
        lblNoPedido.Text = "No. Pedido:"
        '
        'lblControlUltimoLote
        '
        lblControlUltimoLote.AutoSize = True
        lblControlUltimoLote.Location = New System.Drawing.Point(512, 185)
        lblControlUltimoLote.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblControlUltimoLote.Name = "lblControlUltimoLote"
        lblControlUltimoLote.Size = New System.Drawing.Size(73, 16)
        lblControlUltimoLote.TabIndex = 58
        lblControlUltimoLote.Text = "Ultimo lote:"
        '
        'Label22
        '
        Label22.AutoSize = True
        Label22.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label22.Location = New System.Drawing.Point(594, 530)
        Label22.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label22.Name = "Label22"
        Label22.Size = New System.Drawing.Size(37, 16)
        Label22.TabIndex = 26
        Label22.Text = "CBM:"
        '
        'Label21
        '
        Label21.AutoSize = True
        Label21.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label21.Location = New System.Drawing.Point(594, 468)
        Label21.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label21.Name = "Label21"
        Label21.Size = New System.Drawing.Size(48, 16)
        Label21.TabIndex = 21
        Label21.Text = "Piezas:"
        '
        'Label20
        '
        Label20.AutoSize = True
        Label20.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label20.Location = New System.Drawing.Point(594, 405)
        Label20.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label20.Name = "Label20"
        Label20.Size = New System.Drawing.Size(77, 16)
        Label20.TabIndex = 18
        Label20.Text = "PO Number:"
        '
        'Label19
        '
        Label19.AutoSize = True
        Label19.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label19.Location = New System.Drawing.Point(594, 153)
        Label19.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label19.Name = "Label19"
        Label19.Size = New System.Drawing.Size(54, 16)
        Label19.TabIndex = 2
        Label19.Text = "Viaje #:"
        '
        'Label23
        '
        Label23.AutoSize = True
        Label23.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label23.Location = New System.Drawing.Point(594, 342)
        Label23.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label23.Name = "Label23"
        Label23.Size = New System.Drawing.Size(64, 16)
        Label23.TabIndex = 14
        Label23.Text = "Dirección:"
        '
        'Label26
        '
        Label26.AutoSize = True
        Label26.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label26.Location = New System.Drawing.Point(594, 215)
        Label26.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label26.Name = "Label26"
        Label26.Size = New System.Drawing.Size(47, 16)
        Label26.TabIndex = 6
        Label26.Text = "Buque:"
        '
        'Label27
        '
        Label27.AutoSize = True
        Label27.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label27.Location = New System.Drawing.Point(594, 278)
        Label27.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label27.Name = "Label27"
        Label27.Size = New System.Drawing.Size(80, 16)
        Label27.TabIndex = 10
        Label27.Text = "Destinatario:"
        '
        'Label28
        '
        Label28.AutoSize = True
        Label28.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label28.Location = New System.Drawing.Point(65, 528)
        Label28.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label28.Name = "Label28"
        Label28.Size = New System.Drawing.Size(96, 16)
        Label28.TabIndex = 24
        Label28.Text = "Peso Total Kgs:"
        '
        'Label30
        '
        Label30.AutoSize = True
        Label30.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label30.Location = New System.Drawing.Point(65, 210)
        Label30.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label30.Name = "Label30"
        Label30.Size = New System.Drawing.Size(106, 16)
        Label30.TabIndex = 4
        Label30.Text = "Puerta Descarga:"
        '
        'Label32
        '
        Label32.AutoSize = True
        Label32.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label32.Location = New System.Drawing.Point(65, 465)
        Label32.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label32.Name = "Label32"
        Label32.Size = New System.Drawing.Size(62, 16)
        Label32.TabIndex = 20
        Label32.Text = "Cantidad:"
        '
        'Label33
        '
        Label33.AutoSize = True
        Label33.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label33.Location = New System.Drawing.Point(65, 402)
        Label33.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label33.Name = "Label33"
        Label33.Size = New System.Drawing.Size(77, 16)
        Label33.TabIndex = 16
        Label33.Text = "Descripción:"
        '
        'Label34
        '
        Label34.AutoSize = True
        Label34.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label34.Location = New System.Drawing.Point(65, 340)
        Label34.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label34.Name = "Label34"
        Label34.Size = New System.Drawing.Size(102, 16)
        Label34.TabIndex = 12
        Label34.Text = "Fecha Abordaje:"
        '
        'Label35
        '
        Label35.AutoSize = True
        Label35.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label35.Location = New System.Drawing.Point(65, 274)
        Label35.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label35.Name = "Label35"
        Label35.Size = New System.Drawing.Size(70, 16)
        Label35.TabIndex = 8
        Label35.Text = "Remitente:"
        '
        'Label37
        '
        Label37.AutoSize = True
        Label37.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Label37.Location = New System.Drawing.Point(65, 149)
        Label37.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label37.Name = "Label37"
        Label37.Size = New System.Drawing.Size(44, 16)
        Label37.TabIndex = 0
        Label37.Text = "BL No:"
        '
        'Label17
        '
        Label17.AutoSize = True
        Label17.Location = New System.Drawing.Point(513, 326)
        Label17.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label17.Name = "Label17"
        Label17.Size = New System.Drawing.Size(100, 16)
        Label17.TabIndex = 72
        Label17.Text = "No. Picking ERP:"
        '
        'lblCertificadoCalidad
        '
        lblCertificadoCalidad.AutoSize = True
        lblCertificadoCalidad.Location = New System.Drawing.Point(626, 185)
        lblCertificadoCalidad.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblCertificadoCalidad.Name = "lblCertificadoCalidad"
        lblCertificadoCalidad.Size = New System.Drawing.Size(54, 16)
        lblCertificadoCalidad.TabIndex = 74
        lblCertificadoCalidad.Text = "Calidad:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(881, 124)
        Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(44, 16)
        Label1.TabIndex = 60
        Label1.Text = "Hasta:"
        '
        'lblVendedorDespacho
        '
        lblVendedorDespacho.AutoSize = True
        lblVendedorDespacho.Location = New System.Drawing.Point(21, 124)
        lblVendedorDespacho.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblVendedorDespacho.Name = "lblVendedorDespacho"
        lblVendedorDespacho.Size = New System.Drawing.Size(126, 16)
        lblVendedorDespacho.TabIndex = 69
        lblVendedorDespacho.Text = "Vendedor Despacho:"
        '
        'lblRutaDespacho
        '
        lblRutaDespacho.AutoSize = True
        lblRutaDespacho.Location = New System.Drawing.Point(21, 95)
        lblRutaDespacho.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblRutaDespacho.Name = "lblRutaDespacho"
        lblRutaDespacho.Size = New System.Drawing.Size(97, 16)
        lblRutaDespacho.TabIndex = 67
        lblRutaDespacho.Text = "Ruta Despacho:"
        '
        'lblRoadVendedor
        '
        lblRoadVendedor.AutoSize = True
        lblRoadVendedor.Location = New System.Drawing.Point(21, 65)
        lblRoadVendedor.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblRoadVendedor.Name = "lblRoadVendedor"
        lblRoadVendedor.Size = New System.Drawing.Size(109, 16)
        lblRoadVendedor.TabIndex = 61
        lblRoadVendedor.Text = "Vendedor Pedido:"
        '
        'lblRoadRuta
        '
        lblRoadRuta.AutoSize = True
        lblRoadRuta.Location = New System.Drawing.Point(21, 36)
        lblRoadRuta.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblRoadRuta.Name = "lblRoadRuta"
        lblRoadRuta.Size = New System.Drawing.Size(80, 16)
        lblRoadRuta.TabIndex = 56
        lblRoadRuta.Text = "Ruta Pedido:"
        '
        'Label4
        '
        Label4.Location = New System.Drawing.Point(572, 32)
        Label4.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(236, 20)
        Label4.TabIndex = 66
        Label4.Text = "Dir. Entrega:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(746, 123)
        Label2.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(47, 16)
        Label2.TabIndex = 59
        Label2.Text = "Desde:"
        '
        'lblFechaPedido
        '
        lblFechaPedido.AutoSize = True
        lblFechaPedido.Location = New System.Drawing.Point(572, 123)
        lblFechaPedido.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblFechaPedido.Name = "lblFechaPedido"
        lblFechaPedido.Size = New System.Drawing.Size(94, 16)
        lblFechaPedido.TabIndex = 58
        lblFechaPedido.Text = "Fecha Entrega:"
        '
        'Label16
        '
        Label16.AutoSize = True
        Label16.Location = New System.Drawing.Point(513, 244)
        Label16.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label16.Name = "Label16"
        Label16.Size = New System.Drawing.Size(118, 16)
        Label16.TabIndex = 81
        Label16.Text = "Fecha Preparación:"
        '
        'lblManufacturaLigera
        '
        lblManufacturaLigera.AutoSize = True
        lblManufacturaLigera.Location = New System.Drawing.Point(513, 298)
        lblManufacturaLigera.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblManufacturaLigera.Name = "lblManufacturaLigera"
        lblManufacturaLigera.Size = New System.Drawing.Size(151, 16)
        lblManufacturaLigera.TabIndex = 86
        lblManufacturaLigera.Text = "Tipo Manufactura Ligera:"
        '
        'Label44
        '
        Label44.AutoSize = True
        Label44.Location = New System.Drawing.Point(12, 385)
        Label44.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label44.Name = "Label44"
        Label44.Size = New System.Drawing.Size(84, 16)
        Label44.TabIndex = 94
        Label44.Text = "Referencia 2:"
        '
        'Label43
        '
        Label43.AutoSize = True
        Label43.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label43.Location = New System.Drawing.Point(1189, 178)
        Label43.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label43.Name = "Label43"
        Label43.Size = New System.Drawing.Size(97, 18)
        Label43.TabIndex = 158
        Label43.Text = "Total general:"
        '
        'Label42
        '
        Label42.AutoSize = True
        Label42.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label42.Location = New System.Drawing.Point(1189, 138)
        Label42.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label42.Name = "Label42"
        Label42.Size = New System.Drawing.Size(107, 18)
        Label42.TabIndex = 155
        Label42.Text = "Total a liquidar:"
        '
        'Label41
        '
        Label41.AutoSize = True
        Label41.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label41.Location = New System.Drawing.Point(1189, 102)
        Label41.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label41.Name = "Label41"
        Label41.Size = New System.Drawing.Size(127, 18)
        Label41.TabIndex = 153
        Label41.Text = "Modo Transporte:"
        '
        'Label40
        '
        Label40.AutoSize = True
        Label40.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label40.Location = New System.Drawing.Point(1189, 64)
        Label40.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label40.Name = "Label40"
        Label40.Size = New System.Drawing.Size(50, 18)
        Label40.TabIndex = 151
        Label40.Text = "Clase:"
        '
        'Label39
        '
        Label39.AutoSize = True
        Label39.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label39.Location = New System.Drawing.Point(1189, 30)
        Label39.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label39.Name = "Label39"
        Label39.Size = New System.Drawing.Size(105, 18)
        Label39.TabIndex = 150
        Label39.Text = "Nit Imp/Export:"
        '
        'Label38
        '
        Label38.AutoSize = True
        Label38.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label38.Location = New System.Drawing.Point(822, 218)
        Label38.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label38.Name = "Label38"
        Label38.Size = New System.Drawing.Size(102, 18)
        Label38.TabIndex = 139
        Label38.Text = "Clave Aduana:"
        '
        'lblRegimen
        '
        lblRegimen.AutoSize = True
        lblRegimen.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblRegimen.Location = New System.Drawing.Point(822, 64)
        lblRegimen.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblRegimen.Name = "lblRegimen"
        lblRegimen.Size = New System.Drawing.Size(71, 18)
        lblRegimen.TabIndex = 141
        lblRegimen.Text = "Régimen:"
        '
        'lblPesoNeto
        '
        lblPesoNeto.AutoSize = True
        lblPesoNeto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblPesoNeto.Location = New System.Drawing.Point(411, 222)
        lblPesoNeto.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblPesoNeto.Name = "lblPesoNeto"
        lblPesoNeto.Size = New System.Drawing.Size(146, 18)
        lblPesoNeto.TabIndex = 135
        lblPesoNeto.Text = "Total Peso Neto KG:"
        '
        'Label18
        '
        Label18.AutoSize = True
        Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label18.Location = New System.Drawing.Point(6, 290)
        Label18.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label18.Name = "Label18"
        Label18.Size = New System.Drawing.Size(108, 18)
        Label18.TabIndex = 123
        Label18.Text = "Fecha Llegada:"
        '
        'lblFechaAceptacion
        '
        lblFechaAceptacion.AutoSize = True
        lblFechaAceptacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblFechaAceptacion.Location = New System.Drawing.Point(7, 245)
        lblFechaAceptacion.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblFechaAceptacion.Name = "lblFechaAceptacion"
        lblFechaAceptacion.Size = New System.Drawing.Size(130, 18)
        lblFechaAceptacion.TabIndex = 121
        lblFechaAceptacion.Text = "Fecha Aceptación:"
        '
        'lblTotalOtros
        '
        lblTotalOtros.AutoSize = True
        lblTotalOtros.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTotalOtros.Location = New System.Drawing.Point(407, 102)
        lblTotalOtros.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblTotalOtros.Name = "lblTotalOtros"
        lblTotalOtros.Size = New System.Drawing.Size(87, 18)
        lblTotalOtros.TabIndex = 129
        lblTotalOtros.Text = "Total Otros:"
        '
        'lblNoOrden
        '
        lblNoOrden.AutoSize = True
        lblNoOrden.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblNoOrden.Location = New System.Drawing.Point(6, 124)
        lblNoOrden.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblNoOrden.Name = "lblNoOrden"
        lblNoOrden.Size = New System.Drawing.Size(107, 18)
        lblNoOrden.TabIndex = 115
        lblNoOrden.Text = "Número Orden"
        '
        'lblTicket
        '
        lblTicket.AutoSize = True
        lblTicket.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTicket.Location = New System.Drawing.Point(6, 89)
        lblTicket.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblTicket.Name = "lblTicket"
        lblTicket.Size = New System.Drawing.Size(48, 18)
        lblTicket.TabIndex = 113
        lblTicket.Text = "Ticket"
        '
        'lblCodigoPoliza
        '
        lblCodigoPoliza.AutoSize = True
        lblCodigoPoliza.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblCodigoPoliza.Location = New System.Drawing.Point(7, 54)
        lblCodigoPoliza.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblCodigoPoliza.Name = "lblCodigoPoliza"
        lblCodigoPoliza.Size = New System.Drawing.Size(101, 18)
        lblCodigoPoliza.TabIndex = 110
        lblCodigoPoliza.Text = "Código Poliza"
        '
        'lblTotalSeguroUSD
        '
        lblTotalSeguroUSD.AutoSize = True
        lblTotalSeguroUSD.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTotalSeguroUSD.Location = New System.Drawing.Point(407, 27)
        lblTotalSeguroUSD.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblTotalSeguroUSD.Name = "lblTotalSeguroUSD"
        lblTotalSeguroUSD.Size = New System.Drawing.Size(133, 18)
        lblTotalSeguroUSD.TabIndex = 125
        lblTotalSeguroUSD.Text = "Total Seguro USD:"
        '
        'Label25
        '
        Label25.AutoSize = True
        Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label25.Location = New System.Drawing.Point(822, 138)
        Label25.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label25.Name = "Label25"
        Label25.Size = New System.Drawing.Size(91, 18)
        Label25.TabIndex = 145
        Label25.Text = "Total Bultos:"
        '
        'lblNumeroDUA
        '
        lblNumeroDUA.AutoSize = True
        lblNumeroDUA.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblNumeroDUA.Location = New System.Drawing.Point(7, 162)
        lblNumeroDUA.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblNumeroDUA.Name = "lblNumeroDUA"
        lblNumeroDUA.Size = New System.Drawing.Size(101, 18)
        lblNumeroDUA.TabIndex = 117
        lblNumeroDUA.Text = "Número DUA:"
        '
        'lblTotalLineas
        '
        lblTotalLineas.AutoSize = True
        lblTotalLineas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTotalLineas.Location = New System.Drawing.Point(822, 102)
        lblTotalLineas.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblTotalLineas.Name = "lblTotalLineas"
        lblTotalLineas.Size = New System.Drawing.Size(92, 18)
        lblTotalLineas.TabIndex = 143
        lblTotalLineas.Text = "Total Líneas:"
        '
        'lblFechaDocumento
        '
        lblFechaDocumento.AutoSize = True
        lblFechaDocumento.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblFechaDocumento.Location = New System.Drawing.Point(6, 203)
        lblFechaDocumento.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblFechaDocumento.Name = "lblFechaDocumento"
        lblFechaDocumento.Size = New System.Drawing.Size(135, 18)
        lblFechaDocumento.TabIndex = 119
        lblFechaDocumento.Text = "Fecha Documento:"
        '
        'Label29
        '
        Label29.AutoSize = True
        Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label29.Location = New System.Drawing.Point(411, 258)
        Label29.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label29.Name = "Label29"
        Label29.Size = New System.Drawing.Size(97, 18)
        Label29.TabIndex = 137
        Label29.Text = "Tipo Cambio:"
        '
        'Label31
        '
        Label31.AutoSize = True
        Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label31.Location = New System.Drawing.Point(822, 28)
        Label31.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label31.Name = "Label31"
        Label31.Size = New System.Drawing.Size(128, 18)
        Label31.TabIndex = 140
        Label31.Text = "País Procedencia:"
        '
        'lblTotalFOBUSD
        '
        lblTotalFOBUSD.AutoSize = True
        lblTotalFOBUSD.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTotalFOBUSD.Location = New System.Drawing.Point(822, 178)
        lblTotalFOBUSD.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblTotalFOBUSD.Name = "lblTotalFOBUSD"
        lblTotalFOBUSD.Size = New System.Drawing.Size(116, 18)
        lblTotalFOBUSD.TabIndex = 147
        lblTotalFOBUSD.Text = "Total FOB USD:"
        '
        'lblTotalFleteUSD
        '
        lblTotalFleteUSD.AutoSize = True
        lblTotalFleteUSD.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTotalFleteUSD.Location = New System.Drawing.Point(407, 142)
        lblTotalFleteUSD.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblTotalFleteUSD.Name = "lblTotalFleteUSD"
        lblTotalFleteUSD.Size = New System.Drawing.Size(117, 18)
        lblTotalFleteUSD.TabIndex = 131
        lblTotalFleteUSD.Text = "Total Flete USD:"
        '
        'lblTotalPesoBruto
        '
        lblTotalPesoBruto.AutoSize = True
        lblTotalPesoBruto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTotalPesoBruto.Location = New System.Drawing.Point(407, 183)
        lblTotalPesoBruto.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblTotalPesoBruto.Name = "lblTotalPesoBruto"
        lblTotalPesoBruto.Size = New System.Drawing.Size(150, 18)
        lblTotalPesoBruto.TabIndex = 133
        lblTotalPesoBruto.Text = "Total Peso Bruto KG:"
        '
        'lblTotalValorAduana
        '
        lblTotalValorAduana.AutoSize = True
        lblTotalValorAduana.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTotalValorAduana.Location = New System.Drawing.Point(407, 64)
        lblTotalValorAduana.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        lblTotalValorAduana.Name = "lblTotalValorAduana"
        lblTotalValorAduana.Size = New System.Drawing.Size(136, 18)
        lblTotalValorAduana.TabIndex = 127
        lblTotalValorAduana.Text = "Total Valor Aduana:"
        '
        'Label36
        '
        Label36.AutoSize = True
        Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label36.Location = New System.Drawing.Point(7, 15)
        Label36.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Label36.Name = "Label36"
        Label36.Size = New System.Drawing.Size(57, 18)
        Label36.TabIndex = 108
        Label36.Text = "No TO:"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(906, 188)
        Me.Label45.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(95, 16)
        Me.Label45.TabIndex = 101
        Me.Label45.Text = "Es exportación:"
        '
        'lblBodegaDestino
        '
        Me.lblBodegaDestino.AutoSize = True
        Me.lblBodegaDestino.Location = New System.Drawing.Point(626, 134)
        Me.lblBodegaDestino.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblBodegaDestino.Name = "lblBodegaDestino"
        Me.lblBodegaDestino.Size = New System.Drawing.Size(99, 16)
        Me.lblBodegaDestino.TabIndex = 91
        Me.lblBodegaDestino.Text = "Bodega destino:"
        '
        'lblBodegaOrigen
        '
        Me.lblBodegaOrigen.AutoSize = True
        Me.lblBodegaOrigen.Location = New System.Drawing.Point(512, 134)
        Me.lblBodegaOrigen.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblBodegaOrigen.Name = "lblBodegaOrigen"
        Me.lblBodegaOrigen.Size = New System.Drawing.Size(94, 16)
        Me.lblBodegaOrigen.TabIndex = 89
        Me.lblBodegaOrigen.Text = "Bodega origen:"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.BarButtonItem4)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 852)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1544, 30)
        '
        'BarButtonItem4
        '
        Me.BarButtonItem4.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.BarButtonItem4.Caption = "WCF"
        Me.BarButtonItem4.Id = 14
        Me.BarButtonItem4.ImageOptions.Image = CType(resources.GetObject("BarButtonItem4.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem4.Name = "BarButtonItem4"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 19
        Me.lblRegs.Name = "lblRegs"
        '
        'RibbonControl
        '
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(57)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuAsignacion, Me.BarButtonItem1, Me.BarButtonItem2, Me.BarButtonItem3, Me.cmdCodigoBarra, Me.cmdImprimeCodigoBarra, Me.cmdImprmirCodigoBarra, Me.cmdActualizar, Me.cmdEliminar, Me.cmdUbicacion, Me.BarButtonItem4, Me.cmdImprimir, Me.cmdListaUbicacion, Me.mnuEstadoEnviadoAERP, Me.mnuLiberarNoPickeado, Me.lblRegs, Me.BarButtonItem5, Me.mnuReservaStockManual, Me.mnuPendiente, Me.chkActivo, Me.chkAnulado, Me.chkPedidoLocal, Me.chkSyncMI3, Me.chkPalletPrimero, Me.chkControlPoliza, Me.chkVerificar, Me.BarDockingMenuItem1, Me.mnuDespachado, Me.mnuLiberarStockTodoElPedido, Me.mnuLiberarStockProductoEspecifico, Me.mnuEliminarPedidoTablaIntermedia, Me.mnuEliminarPedido, Me.tsmiImprimirStockRes, Me.chkRequiereTarimas, Me.mnuImprimirHojaVerificacion, Me.mnuCrearPicking, Me.chkFotografiaVerificacion, Me.mnuRevertirDespacho, Me.mnuImprimirGridDocumentoERP})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(6, 10, 6, 10)
        Me.RibbonControl.MaxItemId = 55
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 644
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(1544, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
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
        Me.cmdImprimir.Id = 16
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdListaUbicacion), New DevExpress.XtraBars.LinkPersistInfo(Me.tsmiImprimirStockRes), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuImprimirHojaVerificacion), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuImprimirGridDocumentoERP)})
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'cmdListaUbicacion
        '
        Me.cmdListaUbicacion.Caption = "Lista de Ubicaciones"
        Me.cmdListaUbicacion.Id = 17
        Me.cmdListaUbicacion.Name = "cmdListaUbicacion"
        '
        'tsmiImprimirStockRes
        '
        Me.tsmiImprimirStockRes.Caption = "Stock reservado"
        Me.tsmiImprimirStockRes.Id = 46
        Me.tsmiImprimirStockRes.Name = "tsmiImprimirStockRes"
        '
        'mnuImprimirHojaVerificacion
        '
        Me.mnuImprimirHojaVerificacion.Caption = "Hoja de verificación"
        Me.mnuImprimirHojaVerificacion.Id = 48
        Me.mnuImprimirHojaVerificacion.Name = "mnuImprimirHojaVerificacion"
        '
        'mnuImprimirGridDocumentoERP
        '
        Me.mnuImprimirGridDocumentoERP.Caption = "Imprimir grid Documento ERP"
        Me.mnuImprimirGridDocumentoERP.Id = 54
        Me.mnuImprimirGridDocumentoERP.Name = "mnuImprimirGridDocumentoERP"
        '
        'mnuEstadoEnviadoAERP
        '
        Me.mnuEstadoEnviadoAERP.Caption = "No Enviado"
        Me.mnuEstadoEnviadoAERP.Id = 18
        Me.mnuEstadoEnviadoAERP.ImageOptions.Image = CType(resources.GetObject("mnuEstadoEnviadoAERP.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuEstadoEnviadoAERP.Name = "mnuEstadoEnviadoAERP"
        '
        'mnuLiberarNoPickeado
        '
        Me.mnuLiberarNoPickeado.Caption = "Liberar producto no pickeado"
        Me.mnuLiberarNoPickeado.Id = 21
        Me.mnuLiberarNoPickeado.ImageOptions.SvgImage = CType(resources.GetObject("mnuLiberarNoPickeado.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuLiberarNoPickeado.Name = "mnuLiberarNoPickeado"
        Me.mnuLiberarNoPickeado.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'BarButtonItem5
        '
        Me.BarButtonItem5.Id = 20
        Me.BarButtonItem5.Name = "BarButtonItem5"
        '
        'mnuReservaStockManual
        '
        Me.mnuReservaStockManual.Caption = "Reserva manual de stock "
        Me.mnuReservaStockManual.Id = 22
        Me.mnuReservaStockManual.ImageOptions.SvgImage = CType(resources.GetObject("mnuReservaStockManual.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuReservaStockManual.Name = "mnuReservaStockManual"
        '
        'mnuPendiente
        '
        Me.mnuPendiente.Caption = "Pendiente de verificación"
        Me.mnuPendiente.Id = 23
        Me.mnuPendiente.ImageOptions.SvgImage = CType(resources.GetObject("mnuPendiente.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuPendiente.Name = "mnuPendiente"
        '
        'chkActivo
        '
        Me.chkActivo.BindableChecked = True
        Me.chkActivo.Caption = "Activo"
        Me.chkActivo.Checked = True
        Me.chkActivo.Id = 24
        Me.chkActivo.Name = "chkActivo"
        '
        'chkAnulado
        '
        Me.chkAnulado.Caption = "Anulado"
        Me.chkAnulado.Id = 25
        Me.chkAnulado.Name = "chkAnulado"
        '
        'chkPedidoLocal
        '
        Me.chkPedidoLocal.BindableChecked = True
        Me.chkPedidoLocal.Caption = "Pedido Local"
        Me.chkPedidoLocal.Checked = True
        Me.chkPedidoLocal.Id = 31
        Me.chkPedidoLocal.Name = "chkPedidoLocal"
        '
        'chkSyncMI3
        '
        Me.chkSyncMI3.BindableChecked = True
        Me.chkSyncMI3.Caption = "Sync MI3"
        Me.chkSyncMI3.Checked = True
        Me.chkSyncMI3.Id = 32
        Me.chkSyncMI3.Name = "chkSyncMI3"
        '
        'chkPalletPrimero
        '
        Me.chkPalletPrimero.BindableChecked = True
        Me.chkPalletPrimero.Caption = "Pallets Primero"
        Me.chkPalletPrimero.Checked = True
        Me.chkPalletPrimero.Id = 33
        Me.chkPalletPrimero.Name = "chkPalletPrimero"
        '
        'chkControlPoliza
        '
        Me.chkControlPoliza.Caption = "Control Poliza"
        Me.chkControlPoliza.Id = 34
        Me.chkControlPoliza.Name = "chkControlPoliza"
        '
        'chkVerificar
        '
        Me.chkVerificar.Caption = "Verificación Auto"
        Me.chkVerificar.Id = 35
        Me.chkVerificar.Name = "chkVerificar"
        '
        'BarDockingMenuItem1
        '
        Me.BarDockingMenuItem1.Caption = "BarDockingMenuItem1"
        Me.BarDockingMenuItem1.Id = 36
        Me.BarDockingMenuItem1.Name = "BarDockingMenuItem1"
        '
        'mnuDespachado
        '
        Me.mnuDespachado.Caption = "Cambiar a estado Despachado"
        Me.mnuDespachado.Id = 39
        Me.mnuDespachado.ImageOptions.SvgImage = CType(resources.GetObject("mnuDespachado.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuDespachado.Name = "mnuDespachado"
        '
        'mnuLiberarStockTodoElPedido
        '
        Me.mnuLiberarStockTodoElPedido.Caption = "Liberar todo el stock reservado"
        Me.mnuLiberarStockTodoElPedido.Id = 42
        Me.mnuLiberarStockTodoElPedido.Name = "mnuLiberarStockTodoElPedido"
        '
        'mnuLiberarStockProductoEspecifico
        '
        Me.mnuLiberarStockProductoEspecifico.Caption = "Liberar stock de producto: "
        Me.mnuLiberarStockProductoEspecifico.Id = 43
        Me.mnuLiberarStockProductoEspecifico.Name = "mnuLiberarStockProductoEspecifico"
        '
        'mnuEliminarPedidoTablaIntermedia
        '
        Me.mnuEliminarPedidoTablaIntermedia.Caption = "Eliminar pedido de tabla intermedia"
        Me.mnuEliminarPedidoTablaIntermedia.Id = 44
        Me.mnuEliminarPedidoTablaIntermedia.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminarPedidoTablaIntermedia.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminarPedidoTablaIntermedia.Name = "mnuEliminarPedidoTablaIntermedia"
        '
        'mnuEliminarPedido
        '
        Me.mnuEliminarPedido.Caption = "Eliminar Documento"
        Me.mnuEliminarPedido.Id = 45
        Me.mnuEliminarPedido.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminarPedido.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminarPedido.Name = "mnuEliminarPedido"
        '
        'chkRequiereTarimas
        '
        Me.chkRequiereTarimas.Caption = "Requiere Tarimas"
        Me.chkRequiereTarimas.Id = 47
        Me.chkRequiereTarimas.Name = "chkRequiereTarimas"
        '
        'mnuCrearPicking
        '
        Me.mnuCrearPicking.Caption = "Crear Picking"
        Me.mnuCrearPicking.Id = 49
        Me.mnuCrearPicking.ImageOptions.SvgImage = CType(resources.GetObject("mnuCrearPicking.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuCrearPicking.Name = "mnuCrearPicking"
        '
        'chkFotografiaVerificacion
        '
        Me.chkFotografiaVerificacion.Caption = "Fotografia Verificación"
        Me.chkFotografiaVerificacion.Id = 50
        Me.chkFotografiaVerificacion.Name = "chkFotografiaVerificacion"
        '
        'mnuRevertirDespacho
        '
        Me.mnuRevertirDespacho.Caption = "Revertir Despacho"
        Me.mnuRevertirDespacho.Id = 52
        Me.mnuRevertirDespacho.ImageOptions.SvgImage = CType(resources.GetObject("mnuRevertirDespacho.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuRevertirDespacho.Name = "mnuRevertirDespacho"
        Me.mnuRevertirDespacho.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2, Me.RibbonPageGroup4, Me.RibbonPageGroup5, Me.RibbonPageGroup6})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menú Documento de Salida"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdEliminar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminarPedido)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEstadoEnviadoAERP)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuLiberarNoPickeado)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuReservaStockManual)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuPendiente)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuDespachado)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuEliminarPedidoTablaIntermedia)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuCrearPicking)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuRevertirDespacho)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        Me.RibbonPageGroup2.Text = "Gestión de inventario"
        '
        'RibbonPageGroup4
        '
        Me.RibbonPageGroup4.ItemLinks.Add(Me.chkActivo)
        Me.RibbonPageGroup4.ItemLinks.Add(Me.chkAnulado)
        Me.RibbonPageGroup4.Name = "RibbonPageGroup4"
        Me.RibbonPageGroup4.Text = "Estatus Pedido"
        '
        'RibbonPageGroup5
        '
        Me.RibbonPageGroup5.ItemLinks.Add(Me.chkPedidoLocal)
        Me.RibbonPageGroup5.ItemLinks.Add(Me.chkSyncMI3)
        Me.RibbonPageGroup5.ItemLinks.Add(Me.chkPalletPrimero)
        Me.RibbonPageGroup5.ItemLinks.Add(Me.chkRequiereTarimas)
        Me.RibbonPageGroup5.Name = "RibbonPageGroup5"
        Me.RibbonPageGroup5.Text = "Parámetros de pedido"
        '
        'RibbonPageGroup6
        '
        Me.RibbonPageGroup6.ItemLinks.Add(Me.chkControlPoliza)
        Me.RibbonPageGroup6.ItemLinks.Add(Me.chkVerificar)
        Me.RibbonPageGroup6.ItemLinks.Add(Me.chkFotografiaVerificacion)
        Me.RibbonPageGroup6.Name = "RibbonPageGroup6"
        Me.RibbonPageGroup6.Text = "Parámetros tipo documento"
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
        'GrpProducto
        '
        Me.GrpProducto.AllowTouchScroll = True
        Me.GrpProducto.AlwaysScrollActiveControlIntoView = False
        Me.GrpProducto.Controls.Add(Me.XtraScrollableControl2)
        Me.GrpProducto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpProducto.Location = New System.Drawing.Point(0, 0)
        Me.GrpProducto.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.GrpProducto.Name = "GrpProducto"
        Me.GrpProducto.ScrollBarSmallChange = 10
        Me.GrpProducto.Size = New System.Drawing.Size(1542, 603)
        Me.GrpProducto.TabIndex = 0
        '
        'XtraScrollableControl2
        '
        Me.XtraScrollableControl2.Controls.Add(Me.GroupControl4)
        Me.XtraScrollableControl2.Controls.Add(Me.GroupBox3)
        Me.XtraScrollableControl2.Controls.Add(Me.grpInfoPicking)
        Me.XtraScrollableControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraScrollableControl2.Location = New System.Drawing.Point(2, 28)
        Me.XtraScrollableControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.XtraScrollableControl2.Name = "XtraScrollableControl2"
        Me.XtraScrollableControl2.Size = New System.Drawing.Size(1538, 573)
        Me.XtraScrollableControl2.TabIndex = 80
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.dtpFechaEntrega)
        Me.GroupControl4.Controls.Add(Me.cmbRoadVendedorDespacho)
        Me.GroupControl4.Controls.Add(Me.cmbRoadVendedorPedido)
        Me.GroupControl4.Controls.Add(Me.cmbRoadRutaDespacho)
        Me.GroupControl4.Controls.Add(Me.cmbRoadRutaPedido)
        Me.GroupControl4.Controls.Add(lblVendedorDespacho)
        Me.GroupControl4.Controls.Add(Label1)
        Me.GroupControl4.Controls.Add(lblRutaDespacho)
        Me.GroupControl4.Controls.Add(Me.dtpHoraEntregaHasta)
        Me.GroupControl4.Controls.Add(lblRoadVendedor)
        Me.GroupControl4.Controls.Add(lblRoadRuta)
        Me.GroupControl4.Controls.Add(Label4)
        Me.GroupControl4.Controls.Add(Me.txtDireccionEntrega)
        Me.GroupControl4.Controls.Add(lblFechaPedido)
        Me.GroupControl4.Controls.Add(Me.dtpHoraEntregaDesde)
        Me.GroupControl4.Controls.Add(Label2)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl4.Location = New System.Drawing.Point(0, 451)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(1036, 189)
        Me.GroupControl4.TabIndex = 80
        Me.GroupControl4.Text = "Parámetros Road"
        '
        'dtpFechaEntrega
        '
        Me.dtpFechaEntrega.EditValue = New Date(2017, 11, 20, 9, 3, 33, 744)
        Me.dtpFechaEntrega.Location = New System.Drawing.Point(576, 146)
        Me.dtpFechaEntrega.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dtpFechaEntrega.MenuManager = Me.RibbonControl
        Me.dtpFechaEntrega.Name = "dtpFechaEntrega"
        Me.dtpFechaEntrega.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.dtpFechaEntrega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaEntrega.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaEntrega.Size = New System.Drawing.Size(159, 22)
        Me.dtpFechaEntrega.TabIndex = 62
        '
        'cmbRoadVendedorDespacho
        '
        Me.cmbRoadVendedorDespacho.Location = New System.Drawing.Point(196, 124)
        Me.cmbRoadVendedorDespacho.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.cmbRoadVendedorDespacho.MenuManager = Me.RibbonControl
        Me.cmbRoadVendedorDespacho.Name = "cmbRoadVendedorDespacho"
        Me.cmbRoadVendedorDespacho.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbRoadVendedorDespacho.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbRoadVendedorDespacho.Properties.NullText = ""
        Me.cmbRoadVendedorDespacho.Size = New System.Drawing.Size(359, 22)
        Me.cmbRoadVendedorDespacho.TabIndex = 70
        '
        'cmbRoadVendedorPedido
        '
        Me.cmbRoadVendedorPedido.Location = New System.Drawing.Point(196, 65)
        Me.cmbRoadVendedorPedido.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.cmbRoadVendedorPedido.MenuManager = Me.RibbonControl
        Me.cmbRoadVendedorPedido.Name = "cmbRoadVendedorPedido"
        Me.cmbRoadVendedorPedido.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbRoadVendedorPedido.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbRoadVendedorPedido.Properties.NullText = ""
        Me.cmbRoadVendedorPedido.Size = New System.Drawing.Size(359, 22)
        Me.cmbRoadVendedorPedido.TabIndex = 65
        '
        'cmbRoadRutaDespacho
        '
        Me.cmbRoadRutaDespacho.Location = New System.Drawing.Point(196, 95)
        Me.cmbRoadRutaDespacho.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.cmbRoadRutaDespacho.MenuManager = Me.RibbonControl
        Me.cmbRoadRutaDespacho.Name = "cmbRoadRutaDespacho"
        Me.cmbRoadRutaDespacho.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbRoadRutaDespacho.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbRoadRutaDespacho.Properties.NullText = ""
        Me.cmbRoadRutaDespacho.Size = New System.Drawing.Size(359, 22)
        Me.cmbRoadRutaDespacho.TabIndex = 68
        '
        'cmbRoadRutaPedido
        '
        Me.cmbRoadRutaPedido.Location = New System.Drawing.Point(196, 36)
        Me.cmbRoadRutaPedido.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.cmbRoadRutaPedido.MenuManager = Me.RibbonControl
        Me.cmbRoadRutaPedido.Name = "cmbRoadRutaPedido"
        Me.cmbRoadRutaPedido.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbRoadRutaPedido.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbRoadRutaPedido.Properties.NullText = ""
        Me.cmbRoadRutaPedido.Size = New System.Drawing.Size(359, 22)
        Me.cmbRoadRutaPedido.TabIndex = 57
        '
        'dtpHoraEntregaHasta
        '
        Me.dtpHoraEntregaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpHoraEntregaHasta.Location = New System.Drawing.Point(884, 146)
        Me.dtpHoraEntregaHasta.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dtpHoraEntregaHasta.Name = "dtpHoraEntregaHasta"
        Me.dtpHoraEntregaHasta.Size = New System.Drawing.Size(112, 23)
        Me.dtpHoraEntregaHasta.TabIndex = 64
        '
        'txtDireccionEntrega
        '
        Me.txtDireccionEntrega.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDireccionEntrega.Location = New System.Drawing.Point(576, 55)
        Me.txtDireccionEntrega.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtDireccionEntrega.Multiline = True
        Me.txtDireccionEntrega.Name = "txtDireccionEntrega"
        Me.txtDireccionEntrega.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDireccionEntrega.Size = New System.Drawing.Size(418, 56)
        Me.txtDireccionEntrega.TabIndex = 71
        '
        'dtpHoraEntregaDesde
        '
        Me.dtpHoraEntregaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpHoraEntregaDesde.Location = New System.Drawing.Point(748, 146)
        Me.dtpHoraEntregaDesde.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dtpHoraEntregaDesde.Name = "dtpHoraEntregaDesde"
        Me.dtpHoraEntregaDesde.Size = New System.Drawing.Size(124, 23)
        Me.dtpHoraEntregaDesde.TabIndex = 63
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtEsExportacion)
        Me.GroupBox3.Controls.Add(Me.Label45)
        Me.GroupBox3.Controls.Add(Me.lblSociedadSAP)
        Me.GroupBox3.Controls.Add(Me.txtSociedadSAP)
        Me.GroupBox3.Controls.Add(Me.cmbMuelle)
        Me.GroupBox3.Controls.Add(Me.txtIdUbicacionMuelle)
        Me.GroupBox3.Controls.Add(Me.cmbMotivoDevolucion)
        Me.GroupBox3.Controls.Add(Me.lblMotivoDevolucion)
        Me.GroupBox3.Controls.Add(Label44)
        Me.GroupBox3.Controls.Add(Me.txtReferencia2)
        Me.GroupBox3.Controls.Add(Me.txtBodegaDestino)
        Me.GroupBox3.Controls.Add(Me.lblBodegaDestino)
        Me.GroupBox3.Controls.Add(Me.txtBodegaOrigen)
        Me.GroupBox3.Controls.Add(Me.lblBodegaOrigen)
        Me.GroupBox3.Controls.Add(Me.grpUltTareaManufactura)
        Me.GroupBox3.Controls.Add(lblManufacturaLigera)
        Me.GroupBox3.Controls.Add(Me.cmbManufacturaLigera)
        Me.GroupBox3.Controls.Add(Me.lblprg)
        Me.GroupBox3.Controls.Add(Me.lblUbicacionAbastecimiento)
        Me.GroupBox3.Controls.Add(Me.txtIdUbicacionAbastecimiento)
        Me.GroupBox3.Controls.Add(Me.dtpFechaPreparacion)
        Me.GroupBox3.Controls.Add(Label16)
        Me.GroupBox3.Controls.Add(lbCodigoPedidoEnc)
        Me.GroupBox3.Controls.Add(Me.txtIdCliente)
        Me.GroupBox3.Controls.Add(Label6)
        Me.GroupBox3.Controls.Add(lblDiasVencimiento)
        Me.GroupBox3.Controls.Add(Me.txtDiasVencimiento)
        Me.GroupBox3.Controls.Add(lblBodega)
        Me.GroupBox3.Controls.Add(Me.txtCertificadoCalidad)
        Me.GroupBox3.Controls.Add(lblNoDocumento)
        Me.GroupBox3.Controls.Add(lblCertificadoCalidad)
        Me.GroupBox3.Controls.Add(lblPropietario)
        Me.GroupBox3.Controls.Add(Me.txtNoPickingERP)
        Me.GroupBox3.Controls.Add(lblMuelle)
        Me.GroupBox3.Controls.Add(Label17)
        Me.GroupBox3.Controls.Add(Me.txtNoDocumento)
        Me.GroupBox3.Controls.Add(Me.cmbDocumentoRef)
        Me.GroupBox3.Controls.Add(lblSEstado)
        Me.GroupBox3.Controls.Add(Me.lblDocumentoRef)
        Me.GroupBox3.Controls.Add(Me.lnkCliente)
        Me.GroupBox3.Controls.Add(Me.txtReferencia)
        Me.GroupBox3.Controls.Add(Label3)
        Me.GroupBox3.Controls.Add(Me.lblEstado)
        Me.GroupBox3.Controls.Add(Me.dtpHoraInicioPreparacion)
        Me.GroupBox3.Controls.Add(Me.lcmbPropietario)
        Me.GroupBox3.Controls.Add(Label10)
        Me.GroupBox3.Controls.Add(Me.lblUbicTransito)
        Me.GroupBox3.Controls.Add(Me.dtpHoraFinPreparacion)
        Me.GroupBox3.Controls.Add(Me.txtControlUltimoLote)
        Me.GroupBox3.Controls.Add(Label11)
        Me.GroupBox3.Controls.Add(lblControlUltimoLote)
        Me.GroupBox3.Controls.Add(lblTipoPedido)
        Me.GroupBox3.Controls.Add(Me.txtUbicacionTransito)
        Me.GroupBox3.Controls.Add(Me.cmbBodega)
        Me.GroupBox3.Controls.Add(Me.lblIdPedidoEnc)
        Me.GroupBox3.Controls.Add(lblNoPedido)
        Me.GroupBox3.Controls.Add(Me.cmbTipoPedido)
        Me.GroupBox3.Controls.Add(Me.dtpFechaPedido)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox3.Size = New System.Drawing.Size(1036, 451)
        Me.GroupBox3.TabIndex = 85
        Me.GroupBox3.TabStop = False
        '
        'txtEsExportacion
        '
        Me.txtEsExportacion.BackColor = System.Drawing.Color.LightPink
        Me.txtEsExportacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEsExportacion.Location = New System.Drawing.Point(905, 209)
        Me.txtEsExportacion.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtEsExportacion.Name = "txtEsExportacion"
        Me.txtEsExportacion.ReadOnly = True
        Me.txtEsExportacion.Size = New System.Drawing.Size(102, 23)
        Me.txtEsExportacion.TabIndex = 102
        Me.txtEsExportacion.Text = "No"
        Me.txtEsExportacion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblSociedadSAP
        '
        Me.lblSociedadSAP.AutoSize = True
        Me.lblSociedadSAP.Location = New System.Drawing.Point(862, 134)
        Me.lblSociedadSAP.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblSociedadSAP.Name = "lblSociedadSAP"
        Me.lblSociedadSAP.Size = New System.Drawing.Size(91, 16)
        Me.lblSociedadSAP.TabIndex = 100
        Me.lblSociedadSAP.Text = "Sociedad SAP:"
        '
        'txtSociedadSAP
        '
        Me.txtSociedadSAP.BackColor = System.Drawing.Color.Thistle
        Me.txtSociedadSAP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSociedadSAP.Location = New System.Drawing.Point(864, 155)
        Me.txtSociedadSAP.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtSociedadSAP.Name = "txtSociedadSAP"
        Me.txtSociedadSAP.ReadOnly = True
        Me.txtSociedadSAP.Size = New System.Drawing.Size(118, 23)
        Me.txtSociedadSAP.TabIndex = 99
        Me.txtSociedadSAP.Visible = False
        '
        'cmbMuelle
        '
        Me.cmbMuelle.Location = New System.Drawing.Point(183, 325)
        Me.cmbMuelle.Margin = New System.Windows.Forms.Padding(6)
        Me.cmbMuelle.MenuManager = Me.RibbonControl
        Me.cmbMuelle.Name = "cmbMuelle"
        Me.cmbMuelle.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMuelle.Properties.NullText = ""
        Me.cmbMuelle.Properties.PopupView = Me.GridView12
        Me.cmbMuelle.Size = New System.Drawing.Size(228, 22)
        Me.cmbMuelle.TabIndex = 98
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
        Me.txtIdUbicacionMuelle.BackColor = System.Drawing.Color.Thistle
        Me.txtIdUbicacionMuelle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdUbicacionMuelle.Location = New System.Drawing.Point(423, 325)
        Me.txtIdUbicacionMuelle.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtIdUbicacionMuelle.Name = "txtIdUbicacionMuelle"
        Me.txtIdUbicacionMuelle.ReadOnly = True
        Me.txtIdUbicacionMuelle.Size = New System.Drawing.Size(76, 23)
        Me.txtIdUbicacionMuelle.TabIndex = 97
        '
        'cmbMotivoDevolucion
        '
        Me.cmbMotivoDevolucion.Location = New System.Drawing.Point(668, 382)
        Me.cmbMotivoDevolucion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbMotivoDevolucion.Name = "cmbMotivoDevolucion"
        Me.cmbMotivoDevolucion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbMotivoDevolucion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMotivoDevolucion.Properties.NullText = ""
        Me.cmbMotivoDevolucion.Size = New System.Drawing.Size(326, 22)
        Me.cmbMotivoDevolucion.TabIndex = 96
        Me.cmbMotivoDevolucion.Visible = False
        '
        'lblMotivoDevolucion
        '
        Me.lblMotivoDevolucion.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblMotivoDevolucion.Appearance.Options.UseFont = True
        Me.lblMotivoDevolucion.Location = New System.Drawing.Point(517, 385)
        Me.lblMotivoDevolucion.Margin = New System.Windows.Forms.Padding(4)
        Me.lblMotivoDevolucion.Name = "lblMotivoDevolucion"
        Me.lblMotivoDevolucion.Size = New System.Drawing.Size(107, 16)
        Me.lblMotivoDevolucion.TabIndex = 95
        Me.lblMotivoDevolucion.Text = "Motivo Devolución:"
        Me.lblMotivoDevolucion.Visible = False
        '
        'txtReferencia2
        '
        Me.txtReferencia2.Location = New System.Drawing.Point(183, 382)
        Me.txtReferencia2.Margin = New System.Windows.Forms.Padding(6)
        Me.txtReferencia2.MenuManager = Me.RibbonControl
        Me.txtReferencia2.Name = "txtReferencia2"
        Me.txtReferencia2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtReferencia2.Size = New System.Drawing.Size(316, 22)
        Me.txtReferencia2.TabIndex = 93
        '
        'txtBodegaDestino
        '
        Me.txtBodegaDestino.BackColor = System.Drawing.Color.OldLace
        Me.txtBodegaDestino.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBodegaDestino.Location = New System.Drawing.Point(625, 155)
        Me.txtBodegaDestino.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtBodegaDestino.Name = "txtBodegaDestino"
        Me.txtBodegaDestino.ReadOnly = True
        Me.txtBodegaDestino.Size = New System.Drawing.Size(227, 23)
        Me.txtBodegaDestino.TabIndex = 92
        Me.txtBodegaDestino.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtBodegaOrigen
        '
        Me.txtBodegaOrigen.BackColor = System.Drawing.Color.OldLace
        Me.txtBodegaOrigen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBodegaOrigen.Location = New System.Drawing.Point(511, 155)
        Me.txtBodegaOrigen.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtBodegaOrigen.Name = "txtBodegaOrigen"
        Me.txtBodegaOrigen.ReadOnly = True
        Me.txtBodegaOrigen.Size = New System.Drawing.Size(102, 23)
        Me.txtBodegaOrigen.TabIndex = 90
        Me.txtBodegaOrigen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'grpUltTareaManufactura
        '
        Me.grpUltTareaManufactura.AppearanceCaption.BackColor = System.Drawing.Color.SteelBlue
        Me.grpUltTareaManufactura.AppearanceCaption.Options.UseBackColor = True
        Me.grpUltTareaManufactura.AppearanceCaption.Options.UseTextOptions = True
        Me.grpUltTareaManufactura.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.grpUltTareaManufactura.Controls.Add(Me.txtIdManufactura)
        Me.grpUltTareaManufactura.Location = New System.Drawing.Point(513, 25)
        Me.grpUltTareaManufactura.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.grpUltTareaManufactura.Name = "grpUltTareaManufactura"
        Me.grpUltTareaManufactura.Size = New System.Drawing.Size(170, 79)
        Me.grpUltTareaManufactura.TabIndex = 88
        Me.grpUltTareaManufactura.Text = "Ult. Manufactura #"
        Me.grpUltTareaManufactura.Visible = False
        '
        'txtIdManufactura
        '
        Me.txtIdManufactura.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdManufactura.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtIdManufactura.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdManufactura.Location = New System.Drawing.Point(2, 28)
        Me.txtIdManufactura.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.txtIdManufactura.Name = "txtIdManufactura"
        Me.txtIdManufactura.Size = New System.Drawing.Size(166, 49)
        Me.txtIdManufactura.TabIndex = 8
        Me.txtIdManufactura.TabStop = True
        Me.txtIdManufactura.Text = "0"
        Me.txtIdManufactura.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbManufacturaLigera
        '
        Me.cmbManufacturaLigera.Location = New System.Drawing.Point(668, 295)
        Me.cmbManufacturaLigera.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.cmbManufacturaLigera.MenuManager = Me.RibbonControl
        Me.cmbManufacturaLigera.Name = "cmbManufacturaLigera"
        Me.cmbManufacturaLigera.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbManufacturaLigera.Properties.NullText = ""
        Me.cmbManufacturaLigera.Size = New System.Drawing.Size(233, 22)
        Me.cmbManufacturaLigera.TabIndex = 87
        '
        'lblprg
        '
        Me.lblprg.BackColor = System.Drawing.Color.AliceBlue
        Me.lblprg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblprg.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Me.lblprg.ForeColor = System.Drawing.Color.Black
        Me.lblprg.Location = New System.Drawing.Point(4, 412)
        Me.lblprg.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.lblprg.Name = "lblprg"
        Me.lblprg.ReadOnly = True
        Me.lblprg.Size = New System.Drawing.Size(1028, 37)
        Me.lblprg.TabIndex = 85
        Me.lblprg.Text = ""
        Me.lblprg.Visible = False
        '
        'lblUbicacionAbastecimiento
        '
        Me.lblUbicacionAbastecimiento.AutoSize = True
        Me.lblUbicacionAbastecimiento.Location = New System.Drawing.Point(513, 354)
        Me.lblUbicacionAbastecimiento.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblUbicacionAbastecimiento.Name = "lblUbicacionAbastecimiento"
        Me.lblUbicacionAbastecimiento.Size = New System.Drawing.Size(131, 16)
        Me.lblUbicacionAbastecimiento.TabIndex = 84
        Me.lblUbicacionAbastecimiento.Text = "Ubic. Abastecimiento:"
        '
        'txtIdUbicacionAbastecimiento
        '
        Me.txtIdUbicacionAbastecimiento.BackColor = System.Drawing.Color.Thistle
        Me.txtIdUbicacionAbastecimiento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdUbicacionAbastecimiento.Location = New System.Drawing.Point(668, 352)
        Me.txtIdUbicacionAbastecimiento.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtIdUbicacionAbastecimiento.Name = "txtIdUbicacionAbastecimiento"
        Me.txtIdUbicacionAbastecimiento.ReadOnly = True
        Me.txtIdUbicacionAbastecimiento.Size = New System.Drawing.Size(233, 23)
        Me.txtIdUbicacionAbastecimiento.TabIndex = 83
        Me.txtIdUbicacionAbastecimiento.Visible = False
        '
        'dtpFechaPreparacion
        '
        Me.dtpFechaPreparacion.EditValue = New Date(2017, 11, 20, 9, 3, 33, 744)
        Me.dtpFechaPreparacion.Location = New System.Drawing.Point(511, 263)
        Me.dtpFechaPreparacion.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dtpFechaPreparacion.MenuManager = Me.RibbonControl
        Me.dtpFechaPreparacion.Name = "dtpFechaPreparacion"
        Me.dtpFechaPreparacion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.dtpFechaPreparacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaPreparacion.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaPreparacion.Size = New System.Drawing.Size(150, 22)
        Me.dtpFechaPreparacion.TabIndex = 82
        '
        'txtIdCliente
        '
        Me.txtIdCliente.Location = New System.Drawing.Point(183, 206)
        Me.txtIdCliente.Margin = New System.Windows.Forms.Padding(6)
        Me.txtIdCliente.MenuManager = Me.RibbonControl
        Me.txtIdCliente.Name = "txtIdCliente"
        Me.txtIdCliente.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtIdCliente.Properties.PopupView = Me.GridLookUpEdit1View
        Me.txtIdCliente.Size = New System.Drawing.Size(316, 22)
        Me.txtIdCliente.TabIndex = 79
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.DetailHeight = 682
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'txtDiasVencimiento
        '
        Me.txtDiasVencimiento.Location = New System.Drawing.Point(752, 206)
        Me.txtDiasVencimiento.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtDiasVencimiento.Name = "txtDiasVencimiento"
        Me.txtDiasVencimiento.Size = New System.Drawing.Size(100, 23)
        Me.txtDiasVencimiento.TabIndex = 26
        Me.txtDiasVencimiento.Visible = False
        '
        'txtCertificadoCalidad
        '
        Me.txtCertificadoCalidad.BackColor = System.Drawing.Color.LightPink
        Me.txtCertificadoCalidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCertificadoCalidad.Location = New System.Drawing.Point(625, 206)
        Me.txtCertificadoCalidad.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtCertificadoCalidad.Name = "txtCertificadoCalidad"
        Me.txtCertificadoCalidad.ReadOnly = True
        Me.txtCertificadoCalidad.Size = New System.Drawing.Size(102, 23)
        Me.txtCertificadoCalidad.TabIndex = 75
        Me.txtCertificadoCalidad.Text = "No"
        Me.txtCertificadoCalidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtNoPickingERP
        '
        Me.txtNoPickingERP.Location = New System.Drawing.Point(668, 324)
        Me.txtNoPickingERP.Margin = New System.Windows.Forms.Padding(6)
        Me.txtNoPickingERP.MenuManager = Me.RibbonControl
        Me.txtNoPickingERP.Name = "txtNoPickingERP"
        Me.txtNoPickingERP.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtNoPickingERP.Properties.ReadOnly = True
        Me.txtNoPickingERP.Size = New System.Drawing.Size(232, 22)
        Me.txtNoPickingERP.TabIndex = 73
        '
        'txtNoDocumento
        '
        Me.txtNoDocumento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoDocumento.Location = New System.Drawing.Point(183, 265)
        Me.txtNoDocumento.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtNoDocumento.Name = "txtNoDocumento"
        Me.txtNoDocumento.ReadOnly = True
        Me.txtNoDocumento.Size = New System.Drawing.Size(316, 23)
        Me.txtNoDocumento.TabIndex = 29
        Me.txtNoDocumento.Text = "PE000001"
        '
        'cmbDocumentoRef
        '
        Me.cmbDocumentoRef.Enabled = False
        Me.cmbDocumentoRef.Location = New System.Drawing.Point(183, 146)
        Me.cmbDocumentoRef.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.cmbDocumentoRef.Name = "cmbDocumentoRef"
        Me.cmbDocumentoRef.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbDocumentoRef.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbDocumentoRef.Properties.NullText = ""
        Me.cmbDocumentoRef.Size = New System.Drawing.Size(316, 22)
        Me.cmbDocumentoRef.TabIndex = 71
        Me.cmbDocumentoRef.Visible = False
        '
        'lblDocumentoRef
        '
        Me.lblDocumentoRef.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblDocumentoRef.Appearance.Options.UseFont = True
        Me.lblDocumentoRef.Appearance.Options.UseForeColor = True
        Me.lblDocumentoRef.Location = New System.Drawing.Point(15, 148)
        Me.lblDocumentoRef.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.lblDocumentoRef.Name = "lblDocumentoRef"
        Me.lblDocumentoRef.Size = New System.Drawing.Size(92, 16)
        Me.lblDocumentoRef.TabIndex = 70
        Me.lblDocumentoRef.Text = "Documento Ref:"
        Me.lblDocumentoRef.Visible = False
        '
        'lnkCliente
        '
        Me.lnkCliente.AutoSize = True
        Me.lnkCliente.Location = New System.Drawing.Point(12, 212)
        Me.lnkCliente.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lnkCliente.Name = "lnkCliente"
        Me.lnkCliente.Size = New System.Drawing.Size(46, 16)
        Me.lnkCliente.TabIndex = 21
        Me.lnkCliente.TabStop = True
        Me.lnkCliente.Text = "Cliente"
        '
        'txtReferencia
        '
        Me.txtReferencia.Location = New System.Drawing.Point(183, 295)
        Me.txtReferencia.Margin = New System.Windows.Forms.Padding(6)
        Me.txtReferencia.MenuManager = Me.RibbonControl
        Me.txtReferencia.Name = "txtReferencia"
        Me.txtReferencia.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtReferencia.Size = New System.Drawing.Size(316, 22)
        Me.txtReferencia.TabIndex = 69
        '
        'lblEstado
        '
        Me.lblEstado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEstado.Location = New System.Drawing.Point(183, 55)
        Me.lblEstado.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(316, 27)
        Me.lblEstado.TabIndex = 6
        Me.lblEstado.Text = "Nuevo"
        '
        'dtpHoraInicioPreparacion
        '
        Me.dtpHoraInicioPreparacion.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpHoraInicioPreparacion.Location = New System.Drawing.Point(668, 262)
        Me.dtpHoraInicioPreparacion.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dtpHoraInicioPreparacion.Name = "dtpHoraInicioPreparacion"
        Me.dtpHoraInicioPreparacion.Size = New System.Drawing.Size(110, 23)
        Me.dtpHoraInicioPreparacion.TabIndex = 36
        '
        'lcmbPropietario
        '
        Me.lcmbPropietario.Location = New System.Drawing.Point(183, 177)
        Me.lcmbPropietario.Margin = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.lcmbPropietario.Name = "lcmbPropietario"
        Me.lcmbPropietario.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lcmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lcmbPropietario.Size = New System.Drawing.Size(316, 22)
        Me.lcmbPropietario.TabIndex = 66
        '
        'lblUbicTransito
        '
        Me.lblUbicTransito.AutoSize = True
        Me.lblUbicTransito.Location = New System.Drawing.Point(12, 354)
        Me.lblUbicTransito.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblUbicTransito.Name = "lblUbicTransito"
        Me.lblUbicTransito.Size = New System.Drawing.Size(91, 16)
        Me.lblUbicTransito.TabIndex = 63
        Me.lblUbicTransito.Text = "Ubic. Tránsito:"
        '
        'dtpHoraFinPreparacion
        '
        Me.dtpHoraFinPreparacion.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpHoraFinPreparacion.Location = New System.Drawing.Point(788, 261)
        Me.dtpHoraFinPreparacion.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dtpHoraFinPreparacion.Name = "dtpHoraFinPreparacion"
        Me.dtpHoraFinPreparacion.Size = New System.Drawing.Size(111, 23)
        Me.dtpHoraFinPreparacion.TabIndex = 37
        '
        'txtControlUltimoLote
        '
        Me.txtControlUltimoLote.BackColor = System.Drawing.Color.LightPink
        Me.txtControlUltimoLote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtControlUltimoLote.Location = New System.Drawing.Point(511, 206)
        Me.txtControlUltimoLote.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtControlUltimoLote.Name = "txtControlUltimoLote"
        Me.txtControlUltimoLote.ReadOnly = True
        Me.txtControlUltimoLote.Size = New System.Drawing.Size(102, 23)
        Me.txtControlUltimoLote.TabIndex = 59
        Me.txtControlUltimoLote.Text = "No"
        Me.txtControlUltimoLote.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtUbicacionTransito
        '
        Me.txtUbicacionTransito.BackColor = System.Drawing.Color.Thistle
        Me.txtUbicacionTransito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUbicacionTransito.Location = New System.Drawing.Point(183, 354)
        Me.txtUbicacionTransito.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtUbicacionTransito.Name = "txtUbicacionTransito"
        Me.txtUbicacionTransito.ReadOnly = True
        Me.txtUbicacionTransito.Size = New System.Drawing.Size(316, 23)
        Me.txtUbicacionTransito.TabIndex = 57
        Me.txtUbicacionTransito.Visible = False
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(183, 117)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.cmbBodega.MenuManager = Me.RibbonControl
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(316, 22)
        Me.cmbBodega.TabIndex = 9
        '
        'lblIdPedidoEnc
        '
        Me.lblIdPedidoEnc.BackColor = System.Drawing.Color.Lavender
        Me.lblIdPedidoEnc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIdPedidoEnc.Location = New System.Drawing.Point(183, 25)
        Me.lblIdPedidoEnc.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.lblIdPedidoEnc.Name = "lblIdPedidoEnc"
        Me.lblIdPedidoEnc.ReadOnly = True
        Me.lblIdPedidoEnc.Size = New System.Drawing.Size(316, 23)
        Me.lblIdPedidoEnc.TabIndex = 2
        '
        'cmbTipoPedido
        '
        Me.cmbTipoPedido.Location = New System.Drawing.Point(183, 89)
        Me.cmbTipoPedido.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.cmbTipoPedido.MenuManager = Me.RibbonControl
        Me.cmbTipoPedido.Name = "cmbTipoPedido"
        Me.cmbTipoPedido.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoPedido.Properties.NullText = ""
        Me.cmbTipoPedido.Size = New System.Drawing.Size(316, 22)
        Me.cmbTipoPedido.TabIndex = 4
        '
        'dtpFechaPedido
        '
        Me.dtpFechaPedido.EditValue = New Date(2017, 11, 20, 9, 2, 58, 525)
        Me.dtpFechaPedido.Location = New System.Drawing.Point(183, 235)
        Me.dtpFechaPedido.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dtpFechaPedido.MenuManager = Me.RibbonControl
        Me.dtpFechaPedido.Name = "dtpFechaPedido"
        Me.dtpFechaPedido.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.dtpFechaPedido.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaPedido.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaPedido.Size = New System.Drawing.Size(316, 22)
        Me.dtpFechaPedido.TabIndex = 35
        '
        'grpInfoPicking
        '
        Me.grpInfoPicking.Controls.Add(Me.GroupControl3)
        Me.grpInfoPicking.Controls.Add(Me.grpScanPoliza)
        Me.grpInfoPicking.Controls.Add(Me.txtIdDespacho)
        Me.grpInfoPicking.Controls.Add(Me.lblUltDespacho)
        Me.grpInfoPicking.Controls.Add(Me.txtIdPicking)
        Me.grpInfoPicking.Controls.Add(Me.lblNoPicking)
        Me.grpInfoPicking.Dock = System.Windows.Forms.DockStyle.Right
        Me.grpInfoPicking.Location = New System.Drawing.Point(1036, 0)
        Me.grpInfoPicking.Margin = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.grpInfoPicking.Name = "grpInfoPicking"
        Me.grpInfoPicking.Size = New System.Drawing.Size(481, 640)
        Me.grpInfoPicking.TabIndex = 67
        Me.grpInfoPicking.Text = "Transacciones Asociadas"
        '
        'GroupControl3
        '
        Me.GroupControl3.CaptionImageOptions.SvgImage = CType(resources.GetObject("GroupControl3.CaptionImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.GroupControl3.Controls.Add(Me.txtObservacion)
        Me.GroupControl3.Location = New System.Drawing.Point(2, 302)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(471, 329)
        Me.GroupControl3.TabIndex = 26
        Me.GroupControl3.Text = "Observación"
        '
        'txtObservacion
        '
        Me.txtObservacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtObservacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtObservacion.Location = New System.Drawing.Point(2, 41)
        Me.txtObservacion.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtObservacion.MaxLength = 1000
        Me.txtObservacion.Multiline = True
        Me.txtObservacion.Name = "txtObservacion"
        Me.txtObservacion.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtObservacion.Size = New System.Drawing.Size(467, 286)
        Me.txtObservacion.TabIndex = 56
        '
        'grpScanPoliza
        '
        Me.grpScanPoliza.CaptionImageOptions.Image = CType(resources.GetObject("grpScanPoliza.CaptionImageOptions.Image"), System.Drawing.Image)
        Me.grpScanPoliza.Controls.Add(Me.txtScanPoliza)
        Me.grpScanPoliza.Controls.Add(Me.LabelControl2)
        Me.grpScanPoliza.Controls.Add(Me.LabelControl4)
        Me.grpScanPoliza.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpScanPoliza.Location = New System.Drawing.Point(2, 181)
        Me.grpScanPoliza.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.grpScanPoliza.Name = "grpScanPoliza"
        Me.grpScanPoliza.Size = New System.Drawing.Size(477, 635)
        Me.grpScanPoliza.TabIndex = 25
        Me.grpScanPoliza.Text = "Escaneo de Poliza"
        '
        'txtScanPoliza
        '
        Me.txtScanPoliza.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtScanPoliza.Location = New System.Drawing.Point(2, 67)
        Me.txtScanPoliza.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtScanPoliza.Name = "txtScanPoliza"
        Me.txtScanPoliza.Properties.Appearance.BackColor = System.Drawing.Color.Silver
        Me.txtScanPoliza.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScanPoliza.Properties.Appearance.Options.UseBackColor = True
        Me.txtScanPoliza.Properties.Appearance.Options.UseFont = True
        Me.txtScanPoliza.Size = New System.Drawing.Size(473, 32)
        Me.txtScanPoliza.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Appearance.Image = CType(resources.GetObject("LabelControl2.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseImage = True
        Me.LabelControl2.ImageOptions.Image = CType(resources.GetObject("LabelControl2.ImageOptions.Image"), System.Drawing.Image)
        Me.LabelControl2.Location = New System.Drawing.Point(550, 96)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(6, 10, 6, 10)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(32, 32)
        Me.LabelControl2.TabIndex = 2
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Top
        Me.LabelControl4.Location = New System.Drawing.Point(2, 33)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(473, 34)
        Me.LabelControl4.TabIndex = 0
        Me.LabelControl4.Text = "Escanee Poliza:"
        '
        'txtIdDespacho
        '
        Me.txtIdDespacho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdDespacho.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtIdDespacho.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdDespacho.LinkColor = System.Drawing.Color.Green
        Me.txtIdDespacho.Location = New System.Drawing.Point(2, 139)
        Me.txtIdDespacho.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.txtIdDespacho.Name = "txtIdDespacho"
        Me.txtIdDespacho.Size = New System.Drawing.Size(477, 42)
        Me.txtIdDespacho.TabIndex = 24
        Me.txtIdDespacho.TabStop = True
        Me.txtIdDespacho.Text = "0"
        Me.txtIdDespacho.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblUltDespacho
        '
        Me.lblUltDespacho.BackColor = System.Drawing.Color.SeaGreen
        Me.lblUltDespacho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUltDespacho.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblUltDespacho.Location = New System.Drawing.Point(2, 104)
        Me.lblUltDespacho.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblUltDespacho.Name = "lblUltDespacho"
        Me.lblUltDespacho.Size = New System.Drawing.Size(477, 35)
        Me.lblUltDespacho.TabIndex = 14
        Me.lblUltDespacho.Text = "Ult. Despacho #: "
        Me.lblUltDespacho.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtIdPicking
        '
        Me.txtIdPicking.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdPicking.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtIdPicking.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdPicking.Location = New System.Drawing.Point(2, 62)
        Me.txtIdPicking.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.txtIdPicking.Name = "txtIdPicking"
        Me.txtIdPicking.Size = New System.Drawing.Size(477, 42)
        Me.txtIdPicking.TabIndex = 7
        Me.txtIdPicking.TabStop = True
        Me.txtIdPicking.Text = "0"
        Me.txtIdPicking.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNoPicking
        '
        Me.lblNoPicking.BackColor = System.Drawing.Color.SteelBlue
        Me.lblNoPicking.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNoPicking.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblNoPicking.Location = New System.Drawing.Point(2, 28)
        Me.lblNoPicking.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblNoPicking.Name = "lblNoPicking"
        Me.lblNoPicking.Size = New System.Drawing.Size(477, 34)
        Me.lblNoPicking.TabIndex = 1
        Me.lblNoPicking.Text = "Ult. Picking #: "
        Me.lblNoPicking.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpDetallePed
        '
        Me.grpDetallePed.Controls.Add(Me.dgrid)
        Me.grpDetallePed.Controls.Add(Me.GroupBox2)
        Me.grpDetallePed.Controls.Add(Me.GroupBox1)
        Me.grpDetallePed.Controls.Add(Me.prg)
        Me.grpDetallePed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpDetallePed.Location = New System.Drawing.Point(0, 0)
        Me.grpDetallePed.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.grpDetallePed.Name = "grpDetallePed"
        Me.grpDetallePed.Size = New System.Drawing.Size(1542, 603)
        Me.grpDetallePed.TabIndex = 0
        Me.grpDetallePed.Text = "Lista de Productos"
        '
        'dgrid
        '
        Me.dgrid.AllowUserToDeleteRows = False
        Me.dgrid.AllowUserToResizeRows = False
        Me.dgrid.BackgroundColor = System.Drawing.Color.AliceBlue
        Me.dgrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgrid.ColumnHeadersHeight = 40
        Me.dgrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colNo_Linea, Me.colIdProducto, Me.colIsNew, Me.ColCodProducto, Me.ColNomProducto, Me.colUnidadMedida, Me.colPresentacion, Me.colEstadoProducto, Me.colCantidadExistencia, Me.colPesoExistencia, Me.ColCantidad, Me.ColPeso, Me.ColPrecio, Me.ColTotal, Me.ColIdStockRes, Me.colNoDias, Me.ColFechaEspecifica, Me.colNoSerie, Me.colPesoUnitario, Me.CantidadPickeada, Me.CantidadVerificada, Me.Atributo_Variante_1, Me.IdStockEspecifico, Me.colIdProductoBodega, Me.colIdPedidoDet, Me.IdCliente})
        Me.dgrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgrid.EnableHeadersVisualStyles = False
        Me.dgrid.GridColor = System.Drawing.Color.Navy
        Me.dgrid.Location = New System.Drawing.Point(2, 96)
        Me.dgrid.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dgrid.MultiSelect = False
        Me.dgrid.Name = "dgrid"
        Me.dgrid.RowHeadersVisible = False
        Me.dgrid.RowHeadersWidth = 40
        Me.dgrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgrid.Size = New System.Drawing.Size(1538, 339)
        Me.dgrid.TabIndex = 1
        '
        'colNo_Linea
        '
        DataGridViewCellStyle2.Format = "N0"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.colNo_Linea.DefaultCellStyle = DataGridViewCellStyle2
        Me.colNo_Linea.HeaderText = "No_Linea"
        Me.colNo_Linea.MinimumWidth = 6
        Me.colNo_Linea.Name = "colNo_Linea"
        Me.colNo_Linea.Width = 125
        '
        'colIdProducto
        '
        Me.colIdProducto.HeaderText = "IdProducto"
        Me.colIdProducto.MinimumWidth = 6
        Me.colIdProducto.Name = "colIdProducto"
        Me.colIdProducto.ReadOnly = True
        Me.colIdProducto.Visible = False
        Me.colIdProducto.Width = 125
        '
        'colIsNew
        '
        Me.colIsNew.HeaderText = "IsNew"
        Me.colIsNew.MinimumWidth = 6
        Me.colIsNew.Name = "colIsNew"
        Me.colIsNew.ReadOnly = True
        Me.colIsNew.Visible = False
        Me.colIsNew.Width = 125
        '
        'ColCodProducto
        '
        Me.ColCodProducto.HeaderText = "Código"
        Me.ColCodProducto.MinimumWidth = 6
        Me.ColCodProducto.Name = "ColCodProducto"
        Me.ColCodProducto.Width = 125
        '
        'ColNomProducto
        '
        Me.ColNomProducto.HeaderText = "Descripción"
        Me.ColNomProducto.MinimumWidth = 6
        Me.ColNomProducto.Name = "ColNomProducto"
        Me.ColNomProducto.ReadOnly = True
        Me.ColNomProducto.Width = 250
        '
        'colUnidadMedida
        '
        Me.colUnidadMedida.HeaderText = "U.M.Bas"
        Me.colUnidadMedida.MinimumWidth = 6
        Me.colUnidadMedida.Name = "colUnidadMedida"
        Me.colUnidadMedida.ReadOnly = True
        Me.colUnidadMedida.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colUnidadMedida.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colUnidadMedida.Width = 125
        '
        'colPresentacion
        '
        Me.colPresentacion.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.colPresentacion.HeaderText = "Presentación"
        Me.colPresentacion.MinimumWidth = 6
        Me.colPresentacion.Name = "colPresentacion"
        Me.colPresentacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colPresentacion.Width = 150
        '
        'colEstadoProducto
        '
        Me.colEstadoProducto.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.colEstadoProducto.HeaderText = "Estado"
        Me.colEstadoProducto.MinimumWidth = 6
        Me.colEstadoProducto.Name = "colEstadoProducto"
        Me.colEstadoProducto.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colEstadoProducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colEstadoProducto.Width = 125
        '
        'colCantidadExistencia
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.colCantidadExistencia.DefaultCellStyle = DataGridViewCellStyle3
        Me.colCantidadExistencia.HeaderText = "Cant. Disp"
        Me.colCantidadExistencia.MinimumWidth = 6
        Me.colCantidadExistencia.Name = "colCantidadExistencia"
        Me.colCantidadExistencia.ReadOnly = True
        Me.colCantidadExistencia.Width = 125
        '
        'colPesoExistencia
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N2"
        Me.colPesoExistencia.DefaultCellStyle = DataGridViewCellStyle4
        Me.colPesoExistencia.HeaderText = "Peso Disp."
        Me.colPesoExistencia.MinimumWidth = 6
        Me.colPesoExistencia.Name = "colPesoExistencia"
        Me.colPesoExistencia.ReadOnly = True
        Me.colPesoExistencia.Width = 125
        '
        'ColCantidad
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = "0"
        Me.ColCantidad.DefaultCellStyle = DataGridViewCellStyle5
        Me.ColCantidad.HeaderText = "Cantidad"
        Me.ColCantidad.MinimumWidth = 6
        Me.ColCantidad.Name = "ColCantidad"
        Me.ColCantidad.Width = 125
        '
        'ColPeso
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N3"
        DataGridViewCellStyle6.NullValue = "0"
        Me.ColPeso.DefaultCellStyle = DataGridViewCellStyle6
        Me.ColPeso.HeaderText = "Peso"
        Me.ColPeso.MinimumWidth = 6
        Me.ColPeso.Name = "ColPeso"
        Me.ColPeso.Width = 125
        '
        'ColPrecio
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N2"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.ColPrecio.DefaultCellStyle = DataGridViewCellStyle7
        Me.ColPrecio.HeaderText = "Precio"
        Me.ColPrecio.MinimumWidth = 6
        Me.ColPrecio.Name = "ColPrecio"
        Me.ColPrecio.Width = 125
        '
        'ColTotal
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N2"
        DataGridViewCellStyle8.NullValue = Nothing
        Me.ColTotal.DefaultCellStyle = DataGridViewCellStyle8
        Me.ColTotal.HeaderText = "Total"
        Me.ColTotal.MinimumWidth = 6
        Me.ColTotal.Name = "ColTotal"
        Me.ColTotal.ReadOnly = True
        Me.ColTotal.Width = 125
        '
        'ColIdStockRes
        '
        Me.ColIdStockRes.HeaderText = "IdStockRes"
        Me.ColIdStockRes.MinimumWidth = 6
        Me.ColIdStockRes.Name = "ColIdStockRes"
        Me.ColIdStockRes.ReadOnly = True
        Me.ColIdStockRes.Visible = False
        Me.ColIdStockRes.Width = 125
        '
        'colNoDias
        '
        Me.colNoDias.HeaderText = "NoDias"
        Me.colNoDias.MinimumWidth = 6
        Me.colNoDias.Name = "colNoDias"
        Me.colNoDias.ReadOnly = True
        Me.colNoDias.Width = 125
        '
        'ColFechaEspecifica
        '
        Me.ColFechaEspecifica.HeaderText = "FechaEspecifica"
        Me.ColFechaEspecifica.MinimumWidth = 6
        Me.ColFechaEspecifica.Name = "ColFechaEspecifica"
        Me.ColFechaEspecifica.ReadOnly = True
        Me.ColFechaEspecifica.Width = 125
        '
        'colNoSerie
        '
        Me.colNoSerie.HeaderText = "Serie"
        Me.colNoSerie.MinimumWidth = 6
        Me.colNoSerie.Name = "colNoSerie"
        Me.colNoSerie.ReadOnly = True
        Me.colNoSerie.Visible = False
        Me.colNoSerie.Width = 125
        '
        'colPesoUnitario
        '
        Me.colPesoUnitario.HeaderText = "PesoUnitario"
        Me.colPesoUnitario.MinimumWidth = 6
        Me.colPesoUnitario.Name = "colPesoUnitario"
        Me.colPesoUnitario.ReadOnly = True
        Me.colPesoUnitario.Visible = False
        Me.colPesoUnitario.Width = 125
        '
        'CantidadPickeada
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N2"
        DataGridViewCellStyle9.NullValue = "0"
        Me.CantidadPickeada.DefaultCellStyle = DataGridViewCellStyle9
        Me.CantidadPickeada.HeaderText = "Pick"
        Me.CantidadPickeada.MinimumWidth = 6
        Me.CantidadPickeada.Name = "CantidadPickeada"
        Me.CantidadPickeada.ReadOnly = True
        Me.CantidadPickeada.Width = 125
        '
        'CantidadVerificada
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "N2"
        DataGridViewCellStyle10.NullValue = "0"
        Me.CantidadVerificada.DefaultCellStyle = DataGridViewCellStyle10
        Me.CantidadVerificada.HeaderText = "Veri"
        Me.CantidadVerificada.MinimumWidth = 6
        Me.CantidadVerificada.Name = "CantidadVerificada"
        Me.CantidadVerificada.ReadOnly = True
        Me.CantidadVerificada.Width = 125
        '
        'Atributo_Variante_1
        '
        Me.Atributo_Variante_1.HeaderText = "Atributo_Variante"
        Me.Atributo_Variante_1.MinimumWidth = 6
        Me.Atributo_Variante_1.Name = "Atributo_Variante_1"
        Me.Atributo_Variante_1.ReadOnly = True
        Me.Atributo_Variante_1.Visible = False
        Me.Atributo_Variante_1.Width = 125
        '
        'IdStockEspecifico
        '
        Me.IdStockEspecifico.HeaderText = "IdStockEspecifico"
        Me.IdStockEspecifico.MinimumWidth = 6
        Me.IdStockEspecifico.Name = "IdStockEspecifico"
        Me.IdStockEspecifico.ReadOnly = True
        Me.IdStockEspecifico.Width = 125
        '
        'colIdProductoBodega
        '
        Me.colIdProductoBodega.HeaderText = "IdProductoBodega"
        Me.colIdProductoBodega.MinimumWidth = 6
        Me.colIdProductoBodega.Name = "colIdProductoBodega"
        Me.colIdProductoBodega.ReadOnly = True
        Me.colIdProductoBodega.Visible = False
        Me.colIdProductoBodega.Width = 125
        '
        'colIdPedidoDet
        '
        Me.colIdPedidoDet.HeaderText = "IdPedidoDet"
        Me.colIdPedidoDet.MinimumWidth = 6
        Me.colIdPedidoDet.Name = "colIdPedidoDet"
        Me.colIdPedidoDet.Width = 125
        '
        'IdCliente
        '
        Me.IdCliente.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.IdCliente.HeaderText = "Cliente"
        Me.IdCliente.MinimumWidth = 6
        Me.IdCliente.Name = "IdCliente"
        Me.IdCliente.ReadOnly = True
        Me.IdCliente.Width = 200
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblTotal)
        Me.GroupBox2.Controls.Add(Me.lblCantidad)
        Me.GroupBox2.Controls.Add(Me.lblPeso)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(2, 435)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.GroupBox2.Size = New System.Drawing.Size(1538, 166)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Totales"
        '
        'lblTotal
        '
        Me.lblTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTotal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(866, 80)
        Me.lblTotal.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(365, 31)
        Me.lblTotal.TabIndex = 2
        Me.lblTotal.Text = "0000000000.00"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCantidad
        '
        Me.lblCantidad.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCantidad.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblCantidad.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCantidad.Location = New System.Drawing.Point(124, 80)
        Me.lblCantidad.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.Size = New System.Drawing.Size(365, 31)
        Me.lblCantidad.TabIndex = 0
        Me.lblCantidad.Text = "0000000000.00"
        Me.lblCantidad.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPeso
        '
        Me.lblPeso.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPeso.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblPeso.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPeso.Location = New System.Drawing.Point(490, 80)
        Me.lblPeso.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblPeso.Name = "lblPeso"
        Me.lblPeso.Size = New System.Drawing.Size(365, 31)
        Me.lblPeso.TabIndex = 1
        Me.lblPeso.Text = "0000000000.00"
        Me.lblPeso.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lnkAgregarStockEspecifico)
        Me.GroupBox1.Controls.Add(Me.lnkParametrosProducto)
        Me.GroupBox1.Controls.Add(Me.lnkVerConfiguracionProducto)
        Me.GroupBox1.Controls.Add(Me.lnkAgregarProducto)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(2, 28)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.GroupBox1.Size = New System.Drawing.Size(1538, 68)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'lnkAgregarStockEspecifico
        '
        Me.lnkAgregarStockEspecifico.AutoSize = True
        Me.lnkAgregarStockEspecifico.Location = New System.Drawing.Point(172, 18)
        Me.lnkAgregarStockEspecifico.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lnkAgregarStockEspecifico.Name = "lnkAgregarStockEspecifico"
        Me.lnkAgregarStockEspecifico.Size = New System.Drawing.Size(126, 16)
        Me.lnkAgregarStockEspecifico.TabIndex = 1
        Me.lnkAgregarStockEspecifico.TabStop = True
        Me.lnkAgregarStockEspecifico.Text = "Stock Específico (F5)"
        '
        'lnkParametrosProducto
        '
        Me.lnkParametrosProducto.AutoSize = True
        Me.lnkParametrosProducto.Location = New System.Drawing.Point(506, 18)
        Me.lnkParametrosProducto.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lnkParametrosProducto.Name = "lnkParametrosProducto"
        Me.lnkParametrosProducto.Size = New System.Drawing.Size(173, 16)
        Me.lnkParametrosProducto.TabIndex = 3
        Me.lnkParametrosProducto.TabStop = True
        Me.lnkParametrosProducto.Text = "Parámetros de Producto (F4)"
        '
        'lnkVerConfiguracionProducto
        '
        Me.lnkVerConfiguracionProducto.AutoSize = True
        Me.lnkVerConfiguracionProducto.Location = New System.Drawing.Point(309, 18)
        Me.lnkVerConfiguracionProducto.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lnkVerConfiguracionProducto.Name = "lnkVerConfiguracionProducto"
        Me.lnkVerConfiguracionProducto.Size = New System.Drawing.Size(185, 16)
        Me.lnkVerConfiguracionProducto.TabIndex = 2
        Me.lnkVerConfiguracionProducto.TabStop = True
        Me.lnkVerConfiguracionProducto.Text = "Configuración de Producto (F3)"
        '
        'lnkAgregarProducto
        '
        Me.lnkAgregarProducto.AutoSize = True
        Me.lnkAgregarProducto.Location = New System.Drawing.Point(12, 18)
        Me.lnkAgregarProducto.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lnkAgregarProducto.Name = "lnkAgregarProducto"
        Me.lnkAgregarProducto.Size = New System.Drawing.Size(135, 16)
        Me.lnkAgregarProducto.TabIndex = 0
        Me.lnkAgregarProducto.TabStop = True
        Me.lnkAgregarProducto.Text = "Agregar Producto (F2)"
        '
        'prg
        '
        Me.prg.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.prg.Appearance.Options.UseBackColor = True
        Me.prg.AppearanceCaption.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.prg.AppearanceCaption.Options.UseFont = True
        Me.prg.AppearanceDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.prg.AppearanceDescription.Options.UseFont = True
        Me.prg.Caption = "Reservando existencias"
        Me.prg.Location = New System.Drawing.Point(769, 331)
        Me.prg.LookAndFeel.SkinName = "Visual Studio 2013 Dark"
        Me.prg.LookAndFeel.UseDefaultLookAndFeel = False
        Me.prg.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(612, 135)
        Me.prg.TabIndex = 2
        Me.prg.Text = "Espere por favor"
        Me.prg.Visible = False
        '
        'ObservacionTextEdit
        '
        Me.ObservacionTextEdit.Location = New System.Drawing.Point(639, 52)
        Me.ObservacionTextEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.ObservacionTextEdit.MenuManager = Me.RibbonControl
        Me.ObservacionTextEdit.Name = "ObservacionTextEdit"
        Me.ObservacionTextEdit.Size = New System.Drawing.Size(306, 22)
        Me.ObservacionTextEdit.TabIndex = 3
        '
        'RoadIdFacturacionSpinEdit
        '
        Me.RoadIdFacturacionSpinEdit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.RoadIdFacturacionSpinEdit.Location = New System.Drawing.Point(639, 80)
        Me.RoadIdFacturacionSpinEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RoadIdFacturacionSpinEdit.MenuManager = Me.RibbonControl
        Me.RoadIdFacturacionSpinEdit.Name = "RoadIdFacturacionSpinEdit"
        Me.RoadIdFacturacionSpinEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RoadIdFacturacionSpinEdit.Size = New System.Drawing.Size(306, 24)
        Me.RoadIdFacturacionSpinEdit.TabIndex = 7
        '
        'RoadIdDespachoSpinEdit
        '
        Me.RoadIdDespachoSpinEdit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.RoadIdDespachoSpinEdit.Location = New System.Drawing.Point(639, 112)
        Me.RoadIdDespachoSpinEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RoadIdDespachoSpinEdit.MenuManager = Me.RibbonControl
        Me.RoadIdDespachoSpinEdit.Name = "RoadIdDespachoSpinEdit"
        Me.RoadIdDespachoSpinEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RoadIdDespachoSpinEdit.Size = New System.Drawing.Size(306, 24)
        Me.RoadIdDespachoSpinEdit.TabIndex = 11
        '
        'RoadSucursalTextEdit
        '
        Me.RoadSucursalTextEdit.Location = New System.Drawing.Point(639, 174)
        Me.RoadSucursalTextEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RoadSucursalTextEdit.MenuManager = Me.RibbonControl
        Me.RoadSucursalTextEdit.Name = "RoadSucursalTextEdit"
        Me.RoadSucursalTextEdit.Size = New System.Drawing.Size(306, 22)
        Me.RoadSucursalTextEdit.TabIndex = 19
        '
        'RoadInformadoCheckEdit
        '
        Me.RoadInformadoCheckEdit.Location = New System.Drawing.Point(639, 143)
        Me.RoadInformadoCheckEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RoadInformadoCheckEdit.MenuManager = Me.RibbonControl
        Me.RoadInformadoCheckEdit.Name = "RoadInformadoCheckEdit"
        Me.RoadInformadoCheckEdit.Properties.Caption = ""
        Me.RoadInformadoCheckEdit.Size = New System.Drawing.Size(306, 24)
        Me.RoadInformadoCheckEdit.TabIndex = 14
        '
        'RoadRazon_RechazadoTextEdit
        '
        Me.RoadRazon_RechazadoTextEdit.Location = New System.Drawing.Point(820, 928)
        Me.RoadRazon_RechazadoTextEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RoadRazon_RechazadoTextEdit.MenuManager = Me.RibbonControl
        Me.RoadRazon_RechazadoTextEdit.Name = "RoadRazon_RechazadoTextEdit"
        Me.RoadRazon_RechazadoTextEdit.Size = New System.Drawing.Size(331, 22)
        Me.RoadRazon_RechazadoTextEdit.TabIndex = 39
        '
        'RoadRechazadoCheckEdit
        '
        Me.RoadRechazadoCheckEdit.Location = New System.Drawing.Point(820, 868)
        Me.RoadRechazadoCheckEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RoadRechazadoCheckEdit.MenuManager = Me.RibbonControl
        Me.RoadRechazadoCheckEdit.Name = "RoadRechazadoCheckEdit"
        Me.RoadRechazadoCheckEdit.Properties.Caption = ""
        Me.RoadRechazadoCheckEdit.Size = New System.Drawing.Size(275, 24)
        Me.RoadRechazadoCheckEdit.TabIndex = 37
        '
        'RoadStatProcTextEdit
        '
        Me.RoadStatProcTextEdit.Location = New System.Drawing.Point(639, 289)
        Me.RoadStatProcTextEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RoadStatProcTextEdit.MenuManager = Me.RibbonControl
        Me.RoadStatProcTextEdit.Name = "RoadStatProcTextEdit"
        Me.RoadStatProcTextEdit.Size = New System.Drawing.Size(306, 22)
        Me.RoadStatProcTextEdit.TabIndex = 35
        '
        'RoadADD3TextEdit
        '
        Me.RoadADD3TextEdit.Location = New System.Drawing.Point(639, 256)
        Me.RoadADD3TextEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RoadADD3TextEdit.MenuManager = Me.RibbonControl
        Me.RoadADD3TextEdit.Name = "RoadADD3TextEdit"
        Me.RoadADD3TextEdit.Size = New System.Drawing.Size(306, 22)
        Me.RoadADD3TextEdit.TabIndex = 33
        '
        'RoadADD2TextEdit
        '
        Me.RoadADD2TextEdit.Location = New System.Drawing.Point(639, 229)
        Me.RoadADD2TextEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RoadADD2TextEdit.MenuManager = Me.RibbonControl
        Me.RoadADD2TextEdit.Name = "RoadADD2TextEdit"
        Me.RoadADD2TextEdit.Size = New System.Drawing.Size(306, 22)
        Me.RoadADD2TextEdit.TabIndex = 29
        '
        'RoadADD1TextEdit
        '
        Me.RoadADD1TextEdit.Location = New System.Drawing.Point(639, 202)
        Me.RoadADD1TextEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RoadADD1TextEdit.MenuManager = Me.RibbonControl
        Me.RoadADD1TextEdit.Name = "RoadADD1TextEdit"
        Me.RoadADD1TextEdit.Size = New System.Drawing.Size(306, 22)
        Me.RoadADD1TextEdit.TabIndex = 25
        '
        'RoadImpresSpinEdit
        '
        Me.RoadImpresSpinEdit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.RoadImpresSpinEdit.Location = New System.Drawing.Point(146, 290)
        Me.RoadImpresSpinEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RoadImpresSpinEdit.MenuManager = Me.RibbonControl
        Me.RoadImpresSpinEdit.Name = "RoadImpresSpinEdit"
        Me.RoadImpresSpinEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RoadImpresSpinEdit.Size = New System.Drawing.Size(331, 24)
        Me.RoadImpresSpinEdit.TabIndex = 31
        '
        'RoadCalcoBJTextEdit
        '
        Me.RoadCalcoBJTextEdit.Location = New System.Drawing.Point(146, 260)
        Me.RoadCalcoBJTextEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RoadCalcoBJTextEdit.MenuManager = Me.RibbonControl
        Me.RoadCalcoBJTextEdit.Name = "RoadCalcoBJTextEdit"
        Me.RoadCalcoBJTextEdit.Size = New System.Drawing.Size(331, 22)
        Me.RoadCalcoBJTextEdit.TabIndex = 27
        '
        'RoadStatComTextEdit
        '
        Me.RoadStatComTextEdit.Location = New System.Drawing.Point(146, 230)
        Me.RoadStatComTextEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RoadStatComTextEdit.MenuManager = Me.RibbonControl
        Me.RoadStatComTextEdit.Name = "RoadStatComTextEdit"
        Me.RoadStatComTextEdit.Size = New System.Drawing.Size(331, 22)
        Me.RoadStatComTextEdit.TabIndex = 23
        '
        'RoadBanderaTextEdit
        '
        Me.RoadBanderaTextEdit.Location = New System.Drawing.Point(146, 202)
        Me.RoadBanderaTextEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RoadBanderaTextEdit.MenuManager = Me.RibbonControl
        Me.RoadBanderaTextEdit.Name = "RoadBanderaTextEdit"
        Me.RoadBanderaTextEdit.Size = New System.Drawing.Size(331, 22)
        Me.RoadBanderaTextEdit.TabIndex = 21
        '
        'RoadPesoSpinEdit
        '
        Me.RoadPesoSpinEdit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.RoadPesoSpinEdit.Location = New System.Drawing.Point(146, 171)
        Me.RoadPesoSpinEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RoadPesoSpinEdit.MenuManager = Me.RibbonControl
        Me.RoadPesoSpinEdit.Name = "RoadPesoSpinEdit"
        Me.RoadPesoSpinEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RoadPesoSpinEdit.Size = New System.Drawing.Size(331, 24)
        Me.RoadPesoSpinEdit.TabIndex = 17
        '
        'RoadImpMontoSpinEdit
        '
        Me.RoadImpMontoSpinEdit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.RoadImpMontoSpinEdit.Location = New System.Drawing.Point(146, 139)
        Me.RoadImpMontoSpinEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RoadImpMontoSpinEdit.MenuManager = Me.RibbonControl
        Me.RoadImpMontoSpinEdit.Name = "RoadImpMontoSpinEdit"
        Me.RoadImpMontoSpinEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RoadImpMontoSpinEdit.Size = New System.Drawing.Size(331, 24)
        Me.RoadImpMontoSpinEdit.TabIndex = 13
        '
        'RoadDesMontoSpinEdit
        '
        Me.RoadDesMontoSpinEdit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.RoadDesMontoSpinEdit.Location = New System.Drawing.Point(146, 108)
        Me.RoadDesMontoSpinEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RoadDesMontoSpinEdit.MenuManager = Me.RibbonControl
        Me.RoadDesMontoSpinEdit.Name = "RoadDesMontoSpinEdit"
        Me.RoadDesMontoSpinEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RoadDesMontoSpinEdit.Size = New System.Drawing.Size(331, 24)
        Me.RoadDesMontoSpinEdit.TabIndex = 9
        '
        'RoadTotalSpinEdit
        '
        Me.RoadTotalSpinEdit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.RoadTotalSpinEdit.Location = New System.Drawing.Point(146, 80)
        Me.RoadTotalSpinEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RoadTotalSpinEdit.MenuManager = Me.RibbonControl
        Me.RoadTotalSpinEdit.Name = "RoadTotalSpinEdit"
        Me.RoadTotalSpinEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RoadTotalSpinEdit.Size = New System.Drawing.Size(331, 24)
        Me.RoadTotalSpinEdit.TabIndex = 5
        '
        'RoadKilometrajeSpinEdit
        '
        Me.RoadKilometrajeSpinEdit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.RoadKilometrajeSpinEdit.Location = New System.Drawing.Point(146, 50)
        Me.RoadKilometrajeSpinEdit.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.RoadKilometrajeSpinEdit.MenuManager = Me.RibbonControl
        Me.RoadKilometrajeSpinEdit.Name = "RoadKilometrajeSpinEdit"
        Me.RoadKilometrajeSpinEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RoadKilometrajeSpinEdit.Size = New System.Drawing.Size(331, 24)
        Me.RoadKilometrajeSpinEdit.TabIndex = 1
        '
        'dkPedido
        '
        Me.dkPedido.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.AutoHideContainer2})
        Me.dkPedido.Form = Me
        Me.dkPedido.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'AutoHideContainer2
        '
        Me.AutoHideContainer2.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.AutoHideContainer2.Controls.Add(Me.DockPanel2)
        Me.AutoHideContainer2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.AutoHideContainer2.Location = New System.Drawing.Point(0, 826)
        Me.AutoHideContainer2.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.AutoHideContainer2.Name = "AutoHideContainer2"
        Me.AutoHideContainer2.Size = New System.Drawing.Size(1544, 26)
        '
        'DockPanel2
        '
        Me.DockPanel2.Controls.Add(Me.DockPanel2_Container)
        Me.DockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel2.ID = New System.Guid("9a00509c-f114-4be1-9c11-bcfc6d1a8a67")
        Me.DockPanel2.Location = New System.Drawing.Point(0, 675)
        Me.DockPanel2.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel2.Name = "DockPanel2"
        Me.DockPanel2.OriginalSize = New System.Drawing.Size(200, 121)
        Me.DockPanel2.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel2.SavedIndex = 0
        Me.DockPanel2.Size = New System.Drawing.Size(1512, 151)
        Me.DockPanel2.Text = "Bitácora"
        Me.DockPanel2.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel2_Container
        '
        Me.DockPanel2_Container.Controls.Add(Label14)
        Me.DockPanel2_Container.Controls.Add(Label15)
        Me.DockPanel2_Container.Controls.Add(Label13)
        Me.DockPanel2_Container.Controls.Add(Label12)
        Me.DockPanel2_Container.Controls.Add(Me.Fec_modDateEdit1)
        Me.DockPanel2_Container.Controls.Add(Me.Fec_agrDateEdit1)
        Me.DockPanel2_Container.Controls.Add(Me.User_modTextEdit1)
        Me.DockPanel2_Container.Controls.Add(Me.User_agrTextEdit1)
        Me.DockPanel2_Container.Location = New System.Drawing.Point(4, 34)
        Me.DockPanel2_Container.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel2_Container.Name = "DockPanel2_Container"
        Me.DockPanel2_Container.Size = New System.Drawing.Size(1504, 113)
        Me.DockPanel2_Container.TabIndex = 0
        '
        'Fec_modDateEdit1
        '
        Me.Fec_modDateEdit1.EditValue = Nothing
        Me.Fec_modDateEdit1.Enabled = False
        Me.Fec_modDateEdit1.Location = New System.Drawing.Point(783, 46)
        Me.Fec_modDateEdit1.Margin = New System.Windows.Forms.Padding(4)
        Me.Fec_modDateEdit1.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit1.Name = "Fec_modDateEdit1"
        Me.Fec_modDateEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit1.Size = New System.Drawing.Size(286, 22)
        Me.Fec_modDateEdit1.TabIndex = 7
        '
        'Fec_agrDateEdit1
        '
        Me.Fec_agrDateEdit1.EditValue = Nothing
        Me.Fec_agrDateEdit1.Enabled = False
        Me.Fec_agrDateEdit1.Location = New System.Drawing.Point(304, 46)
        Me.Fec_agrDateEdit1.Margin = New System.Windows.Forms.Padding(4)
        Me.Fec_agrDateEdit1.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit1.Name = "Fec_agrDateEdit1"
        Me.Fec_agrDateEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit1.Size = New System.Drawing.Size(286, 22)
        Me.Fec_agrDateEdit1.TabIndex = 5
        '
        'User_modTextEdit1
        '
        Me.User_modTextEdit1.Enabled = False
        Me.User_modTextEdit1.Location = New System.Drawing.Point(783, 12)
        Me.User_modTextEdit1.Margin = New System.Windows.Forms.Padding(4)
        Me.User_modTextEdit1.Name = "User_modTextEdit1"
        Me.User_modTextEdit1.ReadOnly = True
        Me.User_modTextEdit1.Size = New System.Drawing.Size(285, 23)
        Me.User_modTextEdit1.TabIndex = 3
        '
        'User_agrTextEdit1
        '
        Me.User_agrTextEdit1.Enabled = False
        Me.User_agrTextEdit1.Location = New System.Drawing.Point(304, 12)
        Me.User_agrTextEdit1.Margin = New System.Windows.Forms.Padding(4)
        Me.User_agrTextEdit1.Name = "User_agrTextEdit1"
        Me.User_agrTextEdit1.Size = New System.Drawing.Size(285, 23)
        Me.User_agrTextEdit1.TabIndex = 1
        '
        'xtrPedido
        '
        Me.xtrPedido.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtrPedido.Location = New System.Drawing.Point(0, 193)
        Me.xtrPedido.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.xtrPedido.Name = "xtrPedido"
        Me.xtrPedido.SelectedTabPage = Me.tpDatosGenerales
        Me.xtrPedido.Size = New System.Drawing.Size(1544, 633)
        Me.xtrPedido.TabIndex = 0
        Me.xtrPedido.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tpDatosGenerales, Me.tabPoliza, Me.tpDetalleProducto, Me.tpRoadRAW, Me.tpStock_Reservado, Me.tpPicking, Me.tpDespachos, Me.tabPedidoERP, Me.tabComposicion, Me.TabServiciosAsociados, Me.tabStockLiberado, Me.tabLogMI3, Me.tabHojaVerificacion, Me.tabImagenes, Me.tabLogReserva, Me.tabExistencias})
        '
        'tpDatosGenerales
        '
        Me.tpDatosGenerales.Controls.Add(Me.GrpProducto)
        Me.tpDatosGenerales.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.tpDatosGenerales.Name = "tpDatosGenerales"
        Me.tpDatosGenerales.Size = New System.Drawing.Size(1542, 603)
        Me.tpDatosGenerales.Text = "Datos Generales"
        '
        'tabPoliza
        '
        Me.tabPoliza.AccessibleDescription = ""
        Me.tabPoliza.AccessibleName = "Poliza"
        Me.tabPoliza.Controls.Add(Me.TableLayoutPanel1)
        Me.tabPoliza.Controls.Add(Me.GrpEmbarque)
        Me.tabPoliza.Margin = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.tabPoliza.Name = "tabPoliza"
        Me.tabPoliza.Size = New System.Drawing.Size(1542, 603)
        Me.tabPoliza.Text = "Poliza"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.dgridDetallePoliza, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 169.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1542, 603)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.63526!))
        Me.TableLayoutPanel2.Controls.Add(Me.GrpPoliza, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(6, 2)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1530, 430)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'GrpPoliza
        '
        Me.GrpPoliza.Controls.Add(Me.XtraScrollableControl1)
        Me.GrpPoliza.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpPoliza.Location = New System.Drawing.Point(6, 7)
        Me.GrpPoliza.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.GrpPoliza.Name = "GrpPoliza"
        Me.GrpPoliza.Size = New System.Drawing.Size(1518, 416)
        Me.GrpPoliza.TabIndex = 0
        Me.GrpPoliza.Text = "Cabecera de Poliza"
        '
        'XtraScrollableControl1
        '
        Me.XtraScrollableControl1.Controls.Add(Me.txtTotal_general)
        Me.XtraScrollableControl1.Controls.Add(Label43)
        Me.XtraScrollableControl1.Controls.Add(Me.txtTotal_liquidar)
        Me.XtraScrollableControl1.Controls.Add(Label42)
        Me.XtraScrollableControl1.Controls.Add(Me.txtMod_transporte)
        Me.XtraScrollableControl1.Controls.Add(Label41)
        Me.XtraScrollableControl1.Controls.Add(Me.txtClase)
        Me.XtraScrollableControl1.Controls.Add(Label40)
        Me.XtraScrollableControl1.Controls.Add(Me.txtNitImpExp)
        Me.XtraScrollableControl1.Controls.Add(Label39)
        Me.XtraScrollableControl1.Controls.Add(Me.txtClaveAduana)
        Me.XtraScrollableControl1.Controls.Add(Label38)
        Me.XtraScrollableControl1.Controls.Add(Me.cmbRegimen)
        Me.XtraScrollableControl1.Controls.Add(lblRegimen)
        Me.XtraScrollableControl1.Controls.Add(Me.txtTotalPesoNeto)
        Me.XtraScrollableControl1.Controls.Add(lblPesoNeto)
        Me.XtraScrollableControl1.Controls.Add(Me.dtpFechaLlegada)
        Me.XtraScrollableControl1.Controls.Add(Label18)
        Me.XtraScrollableControl1.Controls.Add(Me.dtpFechaAceptacion)
        Me.XtraScrollableControl1.Controls.Add(lblFechaAceptacion)
        Me.XtraScrollableControl1.Controls.Add(Me.txtTotalOtros)
        Me.XtraScrollableControl1.Controls.Add(lblTotalOtros)
        Me.XtraScrollableControl1.Controls.Add(lblNoOrden)
        Me.XtraScrollableControl1.Controls.Add(Me.txtNumeroOrden)
        Me.XtraScrollableControl1.Controls.Add(lblTicket)
        Me.XtraScrollableControl1.Controls.Add(Me.txtTicket)
        Me.XtraScrollableControl1.Controls.Add(lblCodigoPoliza)
        Me.XtraScrollableControl1.Controls.Add(Me.txtCodigoPoliza)
        Me.XtraScrollableControl1.Controls.Add(Me.dtFechaPoliza)
        Me.XtraScrollableControl1.Controls.Add(Me.txtValorSeguro)
        Me.XtraScrollableControl1.Controls.Add(Me.txtTotalBulto)
        Me.XtraScrollableControl1.Controls.Add(Me.txtTotalLineas)
        Me.XtraScrollableControl1.Controls.Add(Me.txtTipoCambio)
        Me.XtraScrollableControl1.Controls.Add(Me.txtTotalFOBUSD)
        Me.XtraScrollableControl1.Controls.Add(Me.txtValorFlete)
        Me.XtraScrollableControl1.Controls.Add(Me.txtTotalPesoBruto)
        Me.XtraScrollableControl1.Controls.Add(Me.txtValorAduana)
        Me.XtraScrollableControl1.Controls.Add(lblTotalSeguroUSD)
        Me.XtraScrollableControl1.Controls.Add(Label25)
        Me.XtraScrollableControl1.Controls.Add(lblNumeroDUA)
        Me.XtraScrollableControl1.Controls.Add(lblTotalLineas)
        Me.XtraScrollableControl1.Controls.Add(lblFechaDocumento)
        Me.XtraScrollableControl1.Controls.Add(Label29)
        Me.XtraScrollableControl1.Controls.Add(Me.txtNumeroDUA)
        Me.XtraScrollableControl1.Controls.Add(Label31)
        Me.XtraScrollableControl1.Controls.Add(lblTotalFOBUSD)
        Me.XtraScrollableControl1.Controls.Add(lblTotalFleteUSD)
        Me.XtraScrollableControl1.Controls.Add(lblTotalPesoBruto)
        Me.XtraScrollableControl1.Controls.Add(lblTotalValorAduana)
        Me.XtraScrollableControl1.Controls.Add(Me.txtNoPoliza)
        Me.XtraScrollableControl1.Controls.Add(Me.txtPaisProcedencia)
        Me.XtraScrollableControl1.Controls.Add(Label36)
        Me.XtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraScrollableControl1.Location = New System.Drawing.Point(2, 28)
        Me.XtraScrollableControl1.Margin = New System.Windows.Forms.Padding(6)
        Me.XtraScrollableControl1.Name = "XtraScrollableControl1"
        Me.XtraScrollableControl1.Size = New System.Drawing.Size(1514, 386)
        Me.XtraScrollableControl1.TabIndex = 56
        '
        'txtTotal_general
        '
        Me.txtTotal_general.Location = New System.Drawing.Point(1324, 174)
        Me.txtTotal_general.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtTotal_general.Name = "txtTotal_general"
        Me.txtTotal_general.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtTotal_general.Properties.Appearance.Options.UseBackColor = True
        Me.txtTotal_general.Size = New System.Drawing.Size(153, 22)
        Me.txtTotal_general.TabIndex = 159
        '
        'txtTotal_liquidar
        '
        Me.txtTotal_liquidar.Location = New System.Drawing.Point(1324, 132)
        Me.txtTotal_liquidar.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtTotal_liquidar.Name = "txtTotal_liquidar"
        Me.txtTotal_liquidar.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtTotal_liquidar.Properties.Appearance.Options.UseBackColor = True
        Me.txtTotal_liquidar.Size = New System.Drawing.Size(153, 22)
        Me.txtTotal_liquidar.TabIndex = 157
        '
        'txtMod_transporte
        '
        Me.txtMod_transporte.Location = New System.Drawing.Point(1324, 96)
        Me.txtMod_transporte.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtMod_transporte.Name = "txtMod_transporte"
        Me.txtMod_transporte.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtMod_transporte.Properties.Appearance.Options.UseBackColor = True
        Me.txtMod_transporte.Size = New System.Drawing.Size(153, 22)
        Me.txtMod_transporte.TabIndex = 156
        '
        'txtClase
        '
        Me.txtClase.Location = New System.Drawing.Point(1324, 59)
        Me.txtClase.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtClase.Name = "txtClase"
        Me.txtClase.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtClase.Properties.Appearance.Options.UseBackColor = True
        Me.txtClase.Size = New System.Drawing.Size(153, 22)
        Me.txtClase.TabIndex = 154
        '
        'txtNitImpExp
        '
        Me.txtNitImpExp.Location = New System.Drawing.Point(1324, 22)
        Me.txtNitImpExp.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtNitImpExp.Name = "txtNitImpExp"
        Me.txtNitImpExp.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtNitImpExp.Properties.Appearance.Options.UseBackColor = True
        Me.txtNitImpExp.Size = New System.Drawing.Size(153, 22)
        Me.txtNitImpExp.TabIndex = 152
        '
        'txtClaveAduana
        '
        Me.txtClaveAduana.Location = New System.Drawing.Point(960, 217)
        Me.txtClaveAduana.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtClaveAduana.Name = "txtClaveAduana"
        Me.txtClaveAduana.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtClaveAduana.Properties.Appearance.Options.UseBackColor = True
        Me.txtClaveAduana.Size = New System.Drawing.Size(169, 22)
        Me.txtClaveAduana.TabIndex = 149
        '
        'cmbRegimen
        '
        Me.cmbRegimen.Location = New System.Drawing.Point(960, 58)
        Me.cmbRegimen.Margin = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.cmbRegimen.Name = "cmbRegimen"
        Me.cmbRegimen.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.cmbRegimen.Properties.Appearance.Options.UseBackColor = True
        Me.cmbRegimen.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbRegimen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbRegimen.Properties.NullText = ""
        Me.cmbRegimen.Size = New System.Drawing.Size(169, 22)
        Me.cmbRegimen.TabIndex = 142
        '
        'txtTotalPesoNeto
        '
        Me.txtTotalPesoNeto.DecimalPlaces = 6
        Me.txtTotalPesoNeto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalPesoNeto.Location = New System.Drawing.Point(569, 217)
        Me.txtTotalPesoNeto.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtTotalPesoNeto.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTotalPesoNeto.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtTotalPesoNeto.Name = "txtTotalPesoNeto"
        Me.txtTotalPesoNeto.Size = New System.Drawing.Size(174, 24)
        Me.txtTotalPesoNeto.TabIndex = 136
        '
        'dtpFechaLlegada
        '
        Me.dtpFechaLlegada.EditValue = New Date(2017, 11, 20, 10, 36, 51, 115)
        Me.dtpFechaLlegada.Location = New System.Drawing.Point(148, 283)
        Me.dtpFechaLlegada.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
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
        Me.dtpFechaLlegada.Size = New System.Drawing.Size(200, 24)
        Me.dtpFechaLlegada.TabIndex = 124
        '
        'dtpFechaAceptacion
        '
        Me.dtpFechaAceptacion.EditValue = New Date(2017, 11, 20, 10, 36, 51, 115)
        Me.dtpFechaAceptacion.Location = New System.Drawing.Point(149, 238)
        Me.dtpFechaAceptacion.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dtpFechaAceptacion.Name = "dtpFechaAceptacion"
        Me.dtpFechaAceptacion.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.dtpFechaAceptacion.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFechaAceptacion.Properties.Appearance.Options.UseBackColor = True
        Me.dtpFechaAceptacion.Properties.Appearance.Options.UseFont = True
        Me.dtpFechaAceptacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaAceptacion.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaAceptacion.Size = New System.Drawing.Size(200, 24)
        Me.dtpFechaAceptacion.TabIndex = 122
        '
        'txtTotalOtros
        '
        Me.txtTotalOtros.BackColor = System.Drawing.Color.PowderBlue
        Me.txtTotalOtros.DecimalPlaces = 6
        Me.txtTotalOtros.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalOtros.Location = New System.Drawing.Point(569, 96)
        Me.txtTotalOtros.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtTotalOtros.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTotalOtros.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtTotalOtros.Name = "txtTotalOtros"
        Me.txtTotalOtros.Size = New System.Drawing.Size(174, 24)
        Me.txtTotalOtros.TabIndex = 130
        '
        'txtNumeroOrden
        '
        Me.txtNumeroOrden.Location = New System.Drawing.Point(148, 117)
        Me.txtNumeroOrden.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtNumeroOrden.Name = "txtNumeroOrden"
        Me.txtNumeroOrden.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtNumeroOrden.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumeroOrden.Properties.Appearance.Options.UseBackColor = True
        Me.txtNumeroOrden.Properties.Appearance.Options.UseFont = True
        Me.txtNumeroOrden.Size = New System.Drawing.Size(200, 24)
        Me.txtNumeroOrden.TabIndex = 116
        '
        'txtTicket
        '
        Me.txtTicket.Location = New System.Drawing.Point(148, 82)
        Me.txtTicket.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtTicket.Name = "txtTicket"
        Me.txtTicket.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtTicket.Properties.Appearance.Options.UseBackColor = True
        Me.txtTicket.Properties.Appearance.Options.UseFont = True
        Me.txtTicket.Size = New System.Drawing.Size(200, 22)
        Me.txtTicket.TabIndex = 114
        '
        'txtCodigoPoliza
        '
        Me.txtCodigoPoliza.Location = New System.Drawing.Point(149, 47)
        Me.txtCodigoPoliza.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtCodigoPoliza.Name = "txtCodigoPoliza"
        Me.txtCodigoPoliza.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtCodigoPoliza.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodigoPoliza.Properties.Appearance.Options.UseBackColor = True
        Me.txtCodigoPoliza.Properties.Appearance.Options.UseFont = True
        Me.txtCodigoPoliza.Size = New System.Drawing.Size(200, 24)
        Me.txtCodigoPoliza.TabIndex = 112
        '
        'dtFechaPoliza
        '
        Me.dtFechaPoliza.EditValue = New Date(2017, 11, 20, 10, 36, 51, 115)
        Me.dtFechaPoliza.Location = New System.Drawing.Point(148, 197)
        Me.dtFechaPoliza.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dtFechaPoliza.Name = "dtFechaPoliza"
        Me.dtFechaPoliza.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFechaPoliza.Properties.Appearance.Options.UseFont = True
        Me.dtFechaPoliza.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFechaPoliza.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFechaPoliza.Size = New System.Drawing.Size(200, 24)
        Me.dtFechaPoliza.TabIndex = 120
        '
        'txtValorSeguro
        '
        Me.txtValorSeguro.BackColor = System.Drawing.Color.PowderBlue
        Me.txtValorSeguro.DecimalPlaces = 6
        Me.txtValorSeguro.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorSeguro.Location = New System.Drawing.Point(569, 21)
        Me.txtValorSeguro.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtValorSeguro.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtValorSeguro.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtValorSeguro.Name = "txtValorSeguro"
        Me.txtValorSeguro.Size = New System.Drawing.Size(174, 24)
        Me.txtValorSeguro.TabIndex = 126
        '
        'txtTotalBulto
        '
        Me.txtTotalBulto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalBulto.Location = New System.Drawing.Point(960, 130)
        Me.txtTotalBulto.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtTotalBulto.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTotalBulto.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtTotalBulto.Name = "txtTotalBulto"
        Me.txtTotalBulto.Size = New System.Drawing.Size(169, 24)
        Me.txtTotalBulto.TabIndex = 146
        '
        'txtTotalLineas
        '
        Me.txtTotalLineas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalLineas.Location = New System.Drawing.Point(960, 96)
        Me.txtTotalLineas.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtTotalLineas.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTotalLineas.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtTotalLineas.Name = "txtTotalLineas"
        Me.txtTotalLineas.Size = New System.Drawing.Size(169, 24)
        Me.txtTotalLineas.TabIndex = 144
        '
        'txtTipoCambio
        '
        Me.txtTipoCambio.BackColor = System.Drawing.Color.PowderBlue
        Me.txtTipoCambio.DecimalPlaces = 6
        Me.txtTipoCambio.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTipoCambio.Location = New System.Drawing.Point(569, 256)
        Me.txtTipoCambio.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtTipoCambio.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTipoCambio.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtTipoCambio.Name = "txtTipoCambio"
        Me.txtTipoCambio.Size = New System.Drawing.Size(174, 24)
        Me.txtTipoCambio.TabIndex = 138
        '
        'txtTotalFOBUSD
        '
        Me.txtTotalFOBUSD.BackColor = System.Drawing.Color.PowderBlue
        Me.txtTotalFOBUSD.DecimalPlaces = 6
        Me.txtTotalFOBUSD.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalFOBUSD.Location = New System.Drawing.Point(960, 174)
        Me.txtTotalFOBUSD.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtTotalFOBUSD.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTotalFOBUSD.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtTotalFOBUSD.Name = "txtTotalFOBUSD"
        Me.txtTotalFOBUSD.Size = New System.Drawing.Size(169, 24)
        Me.txtTotalFOBUSD.TabIndex = 148
        '
        'txtValorFlete
        '
        Me.txtValorFlete.BackColor = System.Drawing.Color.PowderBlue
        Me.txtValorFlete.DecimalPlaces = 6
        Me.txtValorFlete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorFlete.Location = New System.Drawing.Point(569, 135)
        Me.txtValorFlete.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtValorFlete.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtValorFlete.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtValorFlete.Name = "txtValorFlete"
        Me.txtValorFlete.Size = New System.Drawing.Size(174, 24)
        Me.txtValorFlete.TabIndex = 132
        '
        'txtTotalPesoBruto
        '
        Me.txtTotalPesoBruto.BackColor = System.Drawing.Color.PowderBlue
        Me.txtTotalPesoBruto.DecimalPlaces = 6
        Me.txtTotalPesoBruto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalPesoBruto.Location = New System.Drawing.Point(569, 176)
        Me.txtTotalPesoBruto.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtTotalPesoBruto.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTotalPesoBruto.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtTotalPesoBruto.Name = "txtTotalPesoBruto"
        Me.txtTotalPesoBruto.Size = New System.Drawing.Size(174, 24)
        Me.txtTotalPesoBruto.TabIndex = 134
        '
        'txtValorAduana
        '
        Me.txtValorAduana.BackColor = System.Drawing.Color.PowderBlue
        Me.txtValorAduana.DecimalPlaces = 6
        Me.txtValorAduana.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorAduana.Location = New System.Drawing.Point(569, 57)
        Me.txtValorAduana.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtValorAduana.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtValorAduana.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtValorAduana.Name = "txtValorAduana"
        Me.txtValorAduana.Size = New System.Drawing.Size(174, 24)
        Me.txtValorAduana.TabIndex = 128
        '
        'txtNumeroDUA
        '
        Me.txtNumeroDUA.Location = New System.Drawing.Point(149, 156)
        Me.txtNumeroDUA.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtNumeroDUA.Name = "txtNumeroDUA"
        Me.txtNumeroDUA.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtNumeroDUA.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumeroDUA.Properties.Appearance.Options.UseBackColor = True
        Me.txtNumeroDUA.Properties.Appearance.Options.UseFont = True
        Me.txtNumeroDUA.Size = New System.Drawing.Size(200, 24)
        Me.txtNumeroDUA.TabIndex = 118
        '
        'txtNoPoliza
        '
        Me.txtNoPoliza.Location = New System.Drawing.Point(149, 10)
        Me.txtNoPoliza.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtNoPoliza.Name = "txtNoPoliza"
        Me.txtNoPoliza.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoPoliza.Properties.Appearance.Options.UseFont = True
        Me.txtNoPoliza.Size = New System.Drawing.Size(200, 24)
        Me.txtNoPoliza.TabIndex = 109
        '
        'txtPaisProcedencia
        '
        Me.txtPaisProcedencia.Location = New System.Drawing.Point(960, 22)
        Me.txtPaisProcedencia.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtPaisProcedencia.Name = "txtPaisProcedencia"
        Me.txtPaisProcedencia.Properties.Appearance.BackColor = System.Drawing.Color.PowderBlue
        Me.txtPaisProcedencia.Properties.Appearance.Options.UseBackColor = True
        Me.txtPaisProcedencia.Size = New System.Drawing.Size(169, 22)
        Me.txtPaisProcedencia.TabIndex = 111
        '
        'dgridDetallePoliza
        '
        Me.dgridDetallePoliza.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridDetallePoliza.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.dgridDetallePoliza.Location = New System.Drawing.Point(6, 436)
        Me.dgridDetallePoliza.MainView = Me.gvdetallepoliza
        Me.dgridDetallePoliza.Margin = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.dgridDetallePoliza.Name = "dgridDetallePoliza"
        Me.dgridDetallePoliza.Size = New System.Drawing.Size(1530, 165)
        Me.dgridDetallePoliza.TabIndex = 1
        Me.dgridDetallePoliza.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvdetallepoliza})
        '
        'gvdetallepoliza
        '
        Me.gvdetallepoliza.DetailHeight = 682
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
        Me.GrpEmbarque.Controls.Add(Label19)
        Me.GrpEmbarque.Controls.Add(Label23)
        Me.GrpEmbarque.Controls.Add(Label26)
        Me.GrpEmbarque.Controls.Add(Label27)
        Me.GrpEmbarque.Controls.Add(Me.txtDireccion)
        Me.GrpEmbarque.Controls.Add(Me.txtBuque)
        Me.GrpEmbarque.Controls.Add(Label28)
        Me.GrpEmbarque.Controls.Add(Label30)
        Me.GrpEmbarque.Controls.Add(Label32)
        Me.GrpEmbarque.Controls.Add(Label33)
        Me.GrpEmbarque.Controls.Add(Me.Descripcion)
        Me.GrpEmbarque.Controls.Add(Label34)
        Me.GrpEmbarque.Controls.Add(Label35)
        Me.GrpEmbarque.Controls.Add(Me.txtRemitente)
        Me.GrpEmbarque.Controls.Add(Me.BLNo)
        Me.GrpEmbarque.Controls.Add(Me.txtPuertaDescarga)
        Me.GrpEmbarque.Controls.Add(Label37)
        Me.GrpEmbarque.Location = New System.Drawing.Point(1910, 64)
        Me.GrpEmbarque.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.GrpEmbarque.Name = "GrpEmbarque"
        Me.GrpEmbarque.Size = New System.Drawing.Size(828, 630)
        Me.GrpEmbarque.TabIndex = 0
        Me.GrpEmbarque.Text = "Embarque"
        '
        'cmdPrepareGrid
        '
        Me.cmdPrepareGrid.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.cmdPrepareGrid.Location = New System.Drawing.Point(290, 624)
        Me.cmdPrepareGrid.Margin = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.cmdPrepareGrid.Name = "cmdPrepareGrid"
        Me.cmdPrepareGrid.Size = New System.Drawing.Size(232, 50)
        Me.cmdPrepareGrid.TabIndex = 28
        Me.cmdPrepareGrid.Text = "Preparar GRID "
        Me.cmdPrepareGrid.UseVisualStyleBackColor = True
        Me.cmdPrepareGrid.Visible = False
        '
        'dtFechaAbordaje
        '
        Me.dtFechaAbordaje.EditValue = New Date(2017, 11, 20, 10, 37, 31, 443)
        Me.dtFechaAbordaje.Location = New System.Drawing.Point(312, 331)
        Me.dtFechaAbordaje.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dtFechaAbordaje.Name = "dtFechaAbordaje"
        Me.dtFechaAbordaje.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFechaAbordaje.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFechaAbordaje.Size = New System.Drawing.Size(248, 22)
        Me.dtFechaAbordaje.TabIndex = 13
        '
        'txtPiezas
        '
        Me.txtPiezas.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Me.txtPiezas.Location = New System.Drawing.Point(822, 455)
        Me.txtPiezas.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtPiezas.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtPiezas.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtPiezas.Name = "txtPiezas"
        Me.txtPiezas.Size = New System.Drawing.Size(294, 23)
        Me.txtPiezas.TabIndex = 22
        '
        'txtCantidad
        '
        Me.txtCantidad.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Me.txtCantidad.Location = New System.Drawing.Point(312, 455)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtCantidad.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCantidad.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(248, 23)
        Me.txtCantidad.TabIndex = 23
        '
        'txtCBM
        '
        Me.txtCBM.DecimalPlaces = 6
        Me.txtCBM.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Me.txtCBM.Location = New System.Drawing.Point(822, 524)
        Me.txtCBM.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtCBM.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCBM.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtCBM.Name = "txtCBM"
        Me.txtCBM.Size = New System.Drawing.Size(294, 23)
        Me.txtCBM.TabIndex = 27
        '
        'txtPesoKgs
        '
        Me.txtPesoKgs.DecimalPlaces = 6
        Me.txtPesoKgs.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Me.txtPesoKgs.Location = New System.Drawing.Point(312, 522)
        Me.txtPesoKgs.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtPesoKgs.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtPesoKgs.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtPesoKgs.Name = "txtPesoKgs"
        Me.txtPesoKgs.Size = New System.Drawing.Size(248, 23)
        Me.txtPesoKgs.TabIndex = 25
        '
        'txtDestinatario
        '
        Me.txtDestinatario.Location = New System.Drawing.Point(822, 270)
        Me.txtDestinatario.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtDestinatario.Name = "txtDestinatario"
        Me.txtDestinatario.Size = New System.Drawing.Size(294, 22)
        Me.txtDestinatario.TabIndex = 11
        '
        'txtViaje
        '
        Me.txtViaje.Location = New System.Drawing.Point(822, 144)
        Me.txtViaje.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtViaje.Name = "txtViaje"
        Me.txtViaje.Size = New System.Drawing.Size(294, 22)
        Me.txtViaje.TabIndex = 3
        '
        'txtPONumber
        '
        Me.txtPONumber.Location = New System.Drawing.Point(822, 398)
        Me.txtPONumber.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtPONumber.Name = "txtPONumber"
        Me.txtPONumber.Size = New System.Drawing.Size(294, 22)
        Me.txtPONumber.TabIndex = 19
        '
        'txtDireccion
        '
        Me.txtDireccion.Location = New System.Drawing.Point(822, 335)
        Me.txtDireccion.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.Size = New System.Drawing.Size(294, 22)
        Me.txtDireccion.TabIndex = 15
        '
        'txtBuque
        '
        Me.txtBuque.Location = New System.Drawing.Point(822, 206)
        Me.txtBuque.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtBuque.Name = "txtBuque"
        Me.txtBuque.Size = New System.Drawing.Size(294, 22)
        Me.txtBuque.TabIndex = 7
        '
        'Descripcion
        '
        Me.Descripcion.Location = New System.Drawing.Point(312, 394)
        Me.Descripcion.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.Descripcion.Name = "Descripcion"
        Me.Descripcion.Size = New System.Drawing.Size(248, 22)
        Me.Descripcion.TabIndex = 17
        '
        'txtRemitente
        '
        Me.txtRemitente.Location = New System.Drawing.Point(312, 268)
        Me.txtRemitente.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtRemitente.Name = "txtRemitente"
        Me.txtRemitente.Size = New System.Drawing.Size(248, 22)
        Me.txtRemitente.TabIndex = 9
        '
        'BLNo
        '
        Me.BLNo.Location = New System.Drawing.Point(312, 142)
        Me.BLNo.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.BLNo.Name = "BLNo"
        Me.BLNo.Size = New System.Drawing.Size(248, 22)
        Me.BLNo.TabIndex = 1
        '
        'txtPuertaDescarga
        '
        Me.txtPuertaDescarga.Location = New System.Drawing.Point(312, 206)
        Me.txtPuertaDescarga.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtPuertaDescarga.Name = "txtPuertaDescarga"
        Me.txtPuertaDescarga.Size = New System.Drawing.Size(248, 22)
        Me.txtPuertaDescarga.TabIndex = 5
        '
        'tpDetalleProducto
        '
        Me.tpDetalleProducto.Controls.Add(Me.grpDetallePed)
        Me.tpDetalleProducto.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.tpDetalleProducto.Name = "tpDetalleProducto"
        Me.tpDetalleProducto.Size = New System.Drawing.Size(1542, 603)
        Me.tpDetalleProducto.Text = "Detalle de Productos"
        '
        'tpRoadRAW
        '
        Me.tpRoadRAW.Controls.Add(Me.grRoad)
        Me.tpRoadRAW.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.tpRoadRAW.Name = "tpRoadRAW"
        Me.tpRoadRAW.PageVisible = False
        Me.tpRoadRAW.Size = New System.Drawing.Size(1542, 603)
        Me.tpRoadRAW.Text = "Road RAW"
        '
        'grRoad
        '
        Me.grRoad.Controls.Add(ObservacionLabel)
        Me.grRoad.Controls.Add(RoadKilometrajeLabel)
        Me.grRoad.Controls.Add(Me.ObservacionTextEdit)
        Me.grRoad.Controls.Add(Me.RoadKilometrajeSpinEdit)
        Me.grRoad.Controls.Add(RoadIdFacturacionLabel)
        Me.grRoad.Controls.Add(Me.RoadTotalSpinEdit)
        Me.grRoad.Controls.Add(Me.RoadIdFacturacionSpinEdit)
        Me.grRoad.Controls.Add(RoadTotalLabel)
        Me.grRoad.Controls.Add(RoadIdDespachoLabel)
        Me.grRoad.Controls.Add(Me.RoadDesMontoSpinEdit)
        Me.grRoad.Controls.Add(Me.RoadIdDespachoSpinEdit)
        Me.grRoad.Controls.Add(RoadDesMontoLabel)
        Me.grRoad.Controls.Add(RoadSucursalLabel)
        Me.grRoad.Controls.Add(Me.RoadImpMontoSpinEdit)
        Me.grRoad.Controls.Add(Me.RoadSucursalTextEdit)
        Me.grRoad.Controls.Add(RoadImpMontoLabel)
        Me.grRoad.Controls.Add(RoadInformadoLabel)
        Me.grRoad.Controls.Add(Me.RoadPesoSpinEdit)
        Me.grRoad.Controls.Add(Me.RoadInformadoCheckEdit)
        Me.grRoad.Controls.Add(RoadPesoLabel)
        Me.grRoad.Controls.Add(RoadRazon_RechazadoLabel)
        Me.grRoad.Controls.Add(Me.RoadBanderaTextEdit)
        Me.grRoad.Controls.Add(Me.RoadRazon_RechazadoTextEdit)
        Me.grRoad.Controls.Add(RoadBanderaLabel)
        Me.grRoad.Controls.Add(RoadRechazadoLabel)
        Me.grRoad.Controls.Add(Me.RoadStatComTextEdit)
        Me.grRoad.Controls.Add(Me.RoadRechazadoCheckEdit)
        Me.grRoad.Controls.Add(RoadStatComLabel)
        Me.grRoad.Controls.Add(RoadStatProcLabel)
        Me.grRoad.Controls.Add(Me.RoadCalcoBJTextEdit)
        Me.grRoad.Controls.Add(Me.RoadStatProcTextEdit)
        Me.grRoad.Controls.Add(RoadCalcoBJLabel)
        Me.grRoad.Controls.Add(RoadADD3Label)
        Me.grRoad.Controls.Add(Me.RoadImpresSpinEdit)
        Me.grRoad.Controls.Add(Me.RoadADD3TextEdit)
        Me.grRoad.Controls.Add(RoadImpresLabel)
        Me.grRoad.Controls.Add(RoadADD2Label)
        Me.grRoad.Controls.Add(Me.RoadADD1TextEdit)
        Me.grRoad.Controls.Add(Me.RoadADD2TextEdit)
        Me.grRoad.Controls.Add(RoadADD1Label)
        Me.grRoad.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grRoad.Location = New System.Drawing.Point(0, 0)
        Me.grRoad.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.grRoad.Name = "grRoad"
        Me.grRoad.Size = New System.Drawing.Size(1542, 603)
        Me.grRoad.TabIndex = 0
        '
        'tpStock_Reservado
        '
        Me.tpStock_Reservado.Controls.Add(Me.PanelControl1)
        Me.tpStock_Reservado.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.tpStock_Reservado.Name = "tpStock_Reservado"
        Me.tpStock_Reservado.Size = New System.Drawing.Size(1542, 603)
        Me.tpStock_Reservado.Text = "Stock Reservado"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.grdStockReservado)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1542, 603)
        Me.PanelControl1.TabIndex = 0
        '
        'grdStockReservado
        '
        Me.grdStockReservado.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdStockReservado.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.grdStockReservado.Location = New System.Drawing.Point(2, 2)
        Me.grdStockReservado.MainView = Me.GridView6
        Me.grdStockReservado.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.grdStockReservado.MenuManager = Me.RibbonControl
        Me.grdStockReservado.Name = "grdStockReservado"
        Me.grdStockReservado.Size = New System.Drawing.Size(1538, 599)
        Me.grdStockReservado.TabIndex = 0
        Me.grdStockReservado.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView6})
        '
        'GridView6
        '
        Me.GridView6.DetailHeight = 682
        Me.GridView6.GridControl = Me.grdStockReservado
        Me.GridView6.Name = "GridView6"
        Me.GridView6.OptionsBehavior.Editable = False
        Me.GridView6.OptionsBehavior.ReadOnly = True
        Me.GridView6.OptionsView.ColumnAutoWidth = False
        Me.GridView6.OptionsView.ShowAutoFilterRow = True
        '
        'tpPicking
        '
        Me.tpPicking.Controls.Add(Me.grpPicking)
        Me.tpPicking.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.tpPicking.Name = "tpPicking"
        Me.tpPicking.Size = New System.Drawing.Size(1542, 603)
        Me.tpPicking.Text = "Picking"
        '
        'grpPicking
        '
        Me.grpPicking.Controls.Add(Me.grdPicking)
        Me.grpPicking.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpPicking.Location = New System.Drawing.Point(0, 0)
        Me.grpPicking.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.grpPicking.Name = "grpPicking"
        Me.grpPicking.Size = New System.Drawing.Size(1542, 603)
        Me.grpPicking.TabIndex = 0
        '
        'grdPicking
        '
        Me.grdPicking.DataSource = Me.EncabezadoBindingSource
        Me.grdPicking.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPicking.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.grdPicking.Location = New System.Drawing.Point(2, 28)
        Me.grdPicking.MainView = Me.gvDetallePicking
        Me.grdPicking.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.grdPicking.MenuManager = Me.RibbonControl
        Me.grdPicking.Name = "grdPicking"
        Me.grdPicking.Size = New System.Drawing.Size(1538, 573)
        Me.grdPicking.TabIndex = 0
        Me.grdPicking.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvDetallePicking})
        '
        'EncabezadoBindingSource
        '
        Me.EncabezadoBindingSource.DataMember = "Encabezado"
        Me.EncabezadoBindingSource.DataSource = Me.DsPicking
        '
        'DsPicking
        '
        Me.DsPicking.DataSetName = "DsPicking"
        Me.DsPicking.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'gvDetallePicking
        '
        Me.gvDetallePicking.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.colCódigo, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9})
        Me.gvDetallePicking.DetailHeight = 682
        Me.gvDetallePicking.GridControl = Me.grdPicking
        Me.gvDetallePicking.Name = "gvDetallePicking"
        Me.gvDetallePicking.OptionsBehavior.ReadOnly = True
        Me.gvDetallePicking.OptionsView.ShowAutoFilterRow = True
        Me.gvDetallePicking.OptionsView.ShowFooter = True
        '
        'GridColumn1
        '
        Me.GridColumn1.FieldName = "IdPedido"
        Me.GridColumn1.MinWidth = 38
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 159
        '
        'colCódigo
        '
        Me.colCódigo.FieldName = "Código"
        Me.colCódigo.MinWidth = 38
        Me.colCódigo.Name = "colCódigo"
        Me.colCódigo.Visible = True
        Me.colCódigo.VisibleIndex = 1
        Me.colCódigo.Width = 146
        '
        'GridColumn2
        '
        Me.GridColumn2.FieldName = "Bodega"
        Me.GridColumn2.MinWidth = 38
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        Me.GridColumn2.Width = 146
        '
        'GridColumn3
        '
        Me.GridColumn3.FieldName = "Propietario"
        Me.GridColumn3.MinWidth = 38
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 3
        Me.GridColumn3.Width = 146
        '
        'GridColumn4
        '
        Me.GridColumn4.FieldName = "Ubicación Picking"
        Me.GridColumn4.MinWidth = 38
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 4
        Me.GridColumn4.Width = 146
        '
        'GridColumn5
        '
        Me.GridColumn5.FieldName = "Estado"
        Me.GridColumn5.MinWidth = 38
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 5
        Me.GridColumn5.Width = 146
        '
        'GridColumn6
        '
        Me.GridColumn6.FieldName = "Detalle Operador"
        Me.GridColumn6.MinWidth = 38
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 6
        Me.GridColumn6.Width = 146
        '
        'GridColumn7
        '
        Me.GridColumn7.FieldName = "Hora Inicial"
        Me.GridColumn7.MinWidth = 38
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 7
        Me.GridColumn7.Width = 146
        '
        'GridColumn8
        '
        Me.GridColumn8.FieldName = "Hora Final"
        Me.GridColumn8.MinWidth = 38
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 8
        Me.GridColumn8.Width = 146
        '
        'GridColumn9
        '
        Me.GridColumn9.FieldName = "Fecha Picking"
        Me.GridColumn9.MinWidth = 38
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 9
        Me.GridColumn9.Width = 146
        '
        'tpDespachos
        '
        Me.tpDespachos.Controls.Add(Me.PanelControl2)
        Me.tpDespachos.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.tpDespachos.Name = "tpDespachos"
        Me.tpDespachos.Size = New System.Drawing.Size(1542, 603)
        Me.tpDespachos.Text = "Despacho"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.grdDespacho)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(1542, 603)
        Me.PanelControl2.TabIndex = 0
        '
        'grdDespacho
        '
        Me.grdDespacho.DataSource = Me.EncabezadoBindingSource1
        Me.grdDespacho.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDespacho.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        GridLevelNode1.RelationName = "Encabezado_Detalle"
        Me.grdDespacho.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grdDespacho.Location = New System.Drawing.Point(2, 2)
        Me.grdDespacho.MainView = Me.GridView7
        Me.grdDespacho.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.grdDespacho.MenuManager = Me.RibbonControl
        Me.grdDespacho.Name = "grdDespacho"
        Me.grdDespacho.Size = New System.Drawing.Size(1538, 599)
        Me.grdDespacho.TabIndex = 0
        Me.grdDespacho.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView7})
        '
        'EncabezadoBindingSource1
        '
        Me.EncabezadoBindingSource1.DataMember = "Encabezado"
        Me.EncabezadoBindingSource1.DataSource = Me.DsDespacho
        '
        'DsDespacho
        '
        Me.DsDespacho.DataSetName = "DsDespacho"
        Me.DsDespacho.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GridView7
        '
        Me.GridView7.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colIdDespachoEnc, Me.colFecha_Desp, Me.colHora_ini, Me.colHora_fin, Me.GridColumn10, Me.colIdPedidoEnc})
        Me.GridView7.DetailHeight = 682
        Me.GridView7.GridControl = Me.grdDespacho
        Me.GridView7.Name = "GridView7"
        Me.GridView7.OptionsBehavior.ReadOnly = True
        Me.GridView7.OptionsView.ShowAutoFilterRow = True
        '
        'colIdDespachoEnc
        '
        Me.colIdDespachoEnc.FieldName = "IdDespachoEnc"
        Me.colIdDespachoEnc.MinWidth = 38
        Me.colIdDespachoEnc.Name = "colIdDespachoEnc"
        Me.colIdDespachoEnc.Visible = True
        Me.colIdDespachoEnc.VisibleIndex = 0
        Me.colIdDespachoEnc.Width = 159
        '
        'colFecha_Desp
        '
        Me.colFecha_Desp.FieldName = "Fecha_Desp"
        Me.colFecha_Desp.MinWidth = 38
        Me.colFecha_Desp.Name = "colFecha_Desp"
        Me.colFecha_Desp.Visible = True
        Me.colFecha_Desp.VisibleIndex = 1
        Me.colFecha_Desp.Width = 146
        '
        'colHora_ini
        '
        Me.colHora_ini.FieldName = "Hora_ini"
        Me.colHora_ini.MinWidth = 38
        Me.colHora_ini.Name = "colHora_ini"
        Me.colHora_ini.Visible = True
        Me.colHora_ini.VisibleIndex = 2
        Me.colHora_ini.Width = 146
        '
        'colHora_fin
        '
        Me.colHora_fin.FieldName = "Hora_fin"
        Me.colHora_fin.MinWidth = 38
        Me.colHora_fin.Name = "colHora_fin"
        Me.colHora_fin.Visible = True
        Me.colHora_fin.VisibleIndex = 3
        Me.colHora_fin.Width = 146
        '
        'GridColumn10
        '
        Me.GridColumn10.FieldName = "Estado"
        Me.GridColumn10.MinWidth = 38
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 4
        Me.GridColumn10.Width = 146
        '
        'colIdPedidoEnc
        '
        Me.colIdPedidoEnc.FieldName = "IdPedidoEnc"
        Me.colIdPedidoEnc.MinWidth = 38
        Me.colIdPedidoEnc.Name = "colIdPedidoEnc"
        Me.colIdPedidoEnc.Visible = True
        Me.colIdPedidoEnc.VisibleIndex = 5
        Me.colIdPedidoEnc.Width = 146
        '
        'tabPedidoERP
        '
        Me.tabPedidoERP.Controls.Add(Me.PanelControl3)
        Me.tabPedidoERP.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.tabPedidoERP.Name = "tabPedidoERP"
        Me.tabPedidoERP.PageVisible = False
        Me.tabPedidoERP.Size = New System.Drawing.Size(1542, 603)
        Me.tabPedidoERP.Text = "Documento ERP"
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.grdPedTras)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl3.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl3.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(1542, 603)
        Me.PanelControl3.TabIndex = 0
        '
        'grdPedTras
        '
        Me.grdPedTras.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPedTras.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.grdPedTras.Location = New System.Drawing.Point(2, 2)
        Me.grdPedTras.MainView = Me.GridView8
        Me.grdPedTras.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.grdPedTras.MenuManager = Me.RibbonControl
        Me.grdPedTras.Name = "grdPedTras"
        Me.grdPedTras.Size = New System.Drawing.Size(1538, 599)
        Me.grdPedTras.TabIndex = 0
        Me.grdPedTras.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView8})
        '
        'GridView8
        '
        Me.GridView8.DetailHeight = 682
        Me.GridView8.GridControl = Me.grdPedTras
        Me.GridView8.Name = "GridView8"
        Me.GridView8.OptionsView.ShowAutoFilterRow = True
        '
        'tabComposicion
        '
        Me.tabComposicion.Controls.Add(Me.PanelControl4)
        Me.tabComposicion.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.tabComposicion.Name = "tabComposicion"
        Me.tabComposicion.Size = New System.Drawing.Size(1542, 603)
        Me.tabComposicion.Text = "Composición Kit"
        '
        'PanelControl4
        '
        Me.PanelControl4.Controls.Add(Me.grdComposicion)
        Me.PanelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl4.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl4.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.PanelControl4.Name = "PanelControl4"
        Me.PanelControl4.Size = New System.Drawing.Size(1542, 603)
        Me.PanelControl4.TabIndex = 0
        '
        'grdComposicion
        '
        Me.grdComposicion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdComposicion.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.grdComposicion.Location = New System.Drawing.Point(2, 2)
        Me.grdComposicion.MainView = Me.grdVComp
        Me.grdComposicion.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.grdComposicion.MenuManager = Me.RibbonControl
        Me.grdComposicion.Name = "grdComposicion"
        Me.grdComposicion.Size = New System.Drawing.Size(1538, 599)
        Me.grdComposicion.TabIndex = 0
        Me.grdComposicion.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdVComp})
        '
        'grdVComp
        '
        Me.grdVComp.DetailHeight = 682
        Me.grdVComp.GridControl = Me.grdComposicion
        Me.grdVComp.Name = "grdVComp"
        Me.grdVComp.OptionsView.ShowAutoFilterRow = True
        Me.grdVComp.OptionsView.ShowFooter = True
        '
        'TabServiciosAsociados
        '
        Me.TabServiciosAsociados.Controls.Add(Me.dgridServiciosAsociados)
        Me.TabServiciosAsociados.Controls.Add(Me.cmbAcuerdoComercial)
        Me.TabServiciosAsociados.Controls.Add(Me.lblAcuerdoComercial)
        Me.TabServiciosAsociados.Margin = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.TabServiciosAsociados.Name = "TabServiciosAsociados"
        Me.TabServiciosAsociados.Size = New System.Drawing.Size(1542, 603)
        Me.TabServiciosAsociados.Text = "Servicios asociados"
        '
        'dgridServiciosAsociados
        '
        Me.dgridServiciosAsociados.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridServiciosAsociados.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.dgridServiciosAsociados.Location = New System.Drawing.Point(0, 106)
        Me.dgridServiciosAsociados.MainView = Me.gvDetalleServicios
        Me.dgridServiciosAsociados.Margin = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.dgridServiciosAsociados.Name = "dgridServiciosAsociados"
        Me.dgridServiciosAsociados.Size = New System.Drawing.Size(1542, 497)
        Me.dgridServiciosAsociados.TabIndex = 18
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
        Me.cmbAcuerdoComercial.Location = New System.Drawing.Point(0, 78)
        Me.cmbAcuerdoComercial.Margin = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.cmbAcuerdoComercial.Name = "cmbAcuerdoComercial"
        Me.cmbAcuerdoComercial.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAcuerdoComercial.Properties.Appearance.Options.UseFont = True
        Me.cmbAcuerdoComercial.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbAcuerdoComercial.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbAcuerdoComercial.Properties.NullText = ""
        Me.cmbAcuerdoComercial.Size = New System.Drawing.Size(1542, 28)
        Me.cmbAcuerdoComercial.TabIndex = 58
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
        Me.lblAcuerdoComercial.Margin = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.lblAcuerdoComercial.Name = "lblAcuerdoComercial"
        Me.lblAcuerdoComercial.Size = New System.Drawing.Size(1542, 78)
        Me.lblAcuerdoComercial.TabIndex = 59
        Me.lblAcuerdoComercial.Text = "Acuerdo Comercial:"
        '
        'tabStockLiberado
        '
        Me.tabStockLiberado.Controls.Add(Me.dgridStockLiberado)
        Me.tabStockLiberado.Margin = New System.Windows.Forms.Padding(6)
        Me.tabStockLiberado.Name = "tabStockLiberado"
        Me.tabStockLiberado.Size = New System.Drawing.Size(1542, 603)
        Me.tabStockLiberado.Text = "Log. Stock Liberado"
        '
        'dgridStockLiberado
        '
        Me.dgridStockLiberado.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridStockLiberado.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dgridStockLiberado.Location = New System.Drawing.Point(0, 0)
        Me.dgridStockLiberado.MainView = Me.gvLogStockLiberado
        Me.dgridStockLiberado.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dgridStockLiberado.MenuManager = Me.RibbonControl
        Me.dgridStockLiberado.Name = "dgridStockLiberado"
        Me.dgridStockLiberado.Size = New System.Drawing.Size(1542, 603)
        Me.dgridStockLiberado.TabIndex = 1
        Me.dgridStockLiberado.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvLogStockLiberado})
        '
        'gvLogStockLiberado
        '
        Me.gvLogStockLiberado.DetailHeight = 682
        Me.gvLogStockLiberado.GridControl = Me.dgridStockLiberado
        Me.gvLogStockLiberado.Name = "gvLogStockLiberado"
        Me.gvLogStockLiberado.OptionsView.ColumnAutoWidth = False
        Me.gvLogStockLiberado.OptionsView.ShowAutoFilterRow = True
        Me.gvLogStockLiberado.OptionsView.ShowFooter = True
        '
        'tabLogMI3
        '
        Me.tabLogMI3.Controls.Add(Me.dgridLogMI3)
        Me.tabLogMI3.Margin = New System.Windows.Forms.Padding(6)
        Me.tabLogMI3.Name = "tabLogMI3"
        Me.tabLogMI3.Size = New System.Drawing.Size(1542, 603)
        Me.tabLogMI3.Text = "Log. MI3"
        '
        'dgridLogMI3
        '
        Me.dgridLogMI3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridLogMI3.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dgridLogMI3.Location = New System.Drawing.Point(0, 0)
        Me.dgridLogMI3.MainView = Me.GridView5
        Me.dgridLogMI3.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dgridLogMI3.MenuManager = Me.RibbonControl
        Me.dgridLogMI3.Name = "dgridLogMI3"
        Me.dgridLogMI3.Size = New System.Drawing.Size(1542, 603)
        Me.dgridLogMI3.TabIndex = 1
        Me.dgridLogMI3.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView5})
        '
        'GridView5
        '
        Me.GridView5.DetailHeight = 682
        Me.GridView5.GridControl = Me.dgridLogMI3
        Me.GridView5.Name = "GridView5"
        Me.GridView5.OptionsBehavior.Editable = False
        Me.GridView5.OptionsBehavior.ReadOnly = True
        Me.GridView5.OptionsView.ColumnAutoWidth = False
        Me.GridView5.OptionsView.ShowAutoFilterRow = True
        '
        'tabHojaVerificacion
        '
        Me.tabHojaVerificacion.Controls.Add(Me.dgridVerificacion)
        Me.tabHojaVerificacion.Margin = New System.Windows.Forms.Padding(6)
        Me.tabHojaVerificacion.Name = "tabHojaVerificacion"
        Me.tabHojaVerificacion.Size = New System.Drawing.Size(1542, 603)
        Me.tabHojaVerificacion.Text = "Hoja de verificación"
        '
        'dgridVerificacion
        '
        Me.dgridVerificacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridVerificacion.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dgridVerificacion.Location = New System.Drawing.Point(0, 0)
        Me.dgridVerificacion.MainView = Me.GridView9
        Me.dgridVerificacion.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dgridVerificacion.MenuManager = Me.RibbonControl
        Me.dgridVerificacion.Name = "dgridVerificacion"
        Me.dgridVerificacion.Size = New System.Drawing.Size(1542, 603)
        Me.dgridVerificacion.TabIndex = 1
        Me.dgridVerificacion.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView9})
        '
        'GridView9
        '
        Me.GridView9.DetailHeight = 682
        Me.GridView9.GridControl = Me.dgridVerificacion
        Me.GridView9.Name = "GridView9"
        Me.GridView9.OptionsBehavior.Editable = False
        Me.GridView9.OptionsBehavior.ReadOnly = True
        Me.GridView9.OptionsView.ColumnAutoWidth = False
        Me.GridView9.OptionsView.ShowAutoFilterRow = True
        '
        'tabImagenes
        '
        Me.tabImagenes.Controls.Add(Me.GrpImagen)
        Me.tabImagenes.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.tabImagenes.Name = "tabImagenes"
        Me.tabImagenes.Size = New System.Drawing.Size(1542, 603)
        Me.tabImagenes.Text = "Imágenes"
        '
        'GrpImagen
        '
        Me.GrpImagen.Controls.Add(Me.GroupControl1)
        Me.GrpImagen.Controls.Add(Me.Panel3)
        Me.GrpImagen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpImagen.Location = New System.Drawing.Point(0, 0)
        Me.GrpImagen.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpImagen.Name = "GrpImagen"
        Me.GrpImagen.Size = New System.Drawing.Size(1542, 603)
        Me.GrpImagen.TabIndex = 1
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.GrdImagen)
        Me.GroupControl1.Controls.Add(Me.ToolStrip)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(2, 28)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(0, 573)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Lista de Imágenes"
        '
        'GrdImagen
        '
        Me.GrdImagen.Cursor = System.Windows.Forms.Cursors.Default
        Me.GrdImagen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrdImagen.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode2.RelationName = "Level1"
        Me.GrdImagen.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.GrdImagen.Location = New System.Drawing.Point(1, 55)
        Me.GrdImagen.MainView = Me.GridViewImg
        Me.GrdImagen.Margin = New System.Windows.Forms.Padding(4)
        Me.GrdImagen.MenuManager = Me.RibbonControl
        Me.GrdImagen.Name = "GrdImagen"
        Me.GrdImagen.Size = New System.Drawing.Size(0, 516)
        Me.GrdImagen.TabIndex = 1
        Me.GrdImagen.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewImg})
        '
        'GridViewImg
        '
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
        Me.ToolStrip.Location = New System.Drawing.Point(1, 28)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip.Size = New System.Drawing.Size(-2, 27)
        Me.ToolStrip.TabIndex = 0
        Me.ToolStrip.Text = "ToolStrip4"
        '
        'cmdAdd
        '
        Me.cmdAdd.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(29, 4)
        '
        'cmdDelete
        '
        Me.cmdDelete.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(29, 24)
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.PicImg)
        Me.Panel3.Controls.Add(Me.Label24)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(-94, 28)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1634, 573)
        Me.Panel3.TabIndex = 94
        '
        'PicImg
        '
        Me.PicImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PicImg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PicImg.Location = New System.Drawing.Point(0, 27)
        Me.PicImg.Margin = New System.Windows.Forms.Padding(4)
        Me.PicImg.Name = "PicImg"
        Me.PicImg.Size = New System.Drawing.Size(1634, 546)
        Me.PicImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PicImg.TabIndex = 93
        Me.PicImg.TabStop = False
        Me.PicImg.Visible = False
        '
        'Label24
        '
        Me.Label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(0, 0)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1634, 27)
        Me.Label24.TabIndex = 1
        Me.Label24.Text = "Nombre Imagen"
        '
        'tabLogReserva
        '
        Me.tabLogReserva.Controls.Add(Me.dgrdLogReserva)
        Me.tabLogReserva.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.tabLogReserva.Name = "tabLogReserva"
        Me.tabLogReserva.Size = New System.Drawing.Size(1542, 603)
        Me.tabLogReserva.Text = "Log Reserva"
        '
        'dgrdLogReserva
        '
        Me.dgrdLogReserva.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrdLogReserva.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dgrdLogReserva.Location = New System.Drawing.Point(0, 0)
        Me.dgrdLogReserva.MainView = Me.GridView10
        Me.dgrdLogReserva.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dgrdLogReserva.MenuManager = Me.RibbonControl
        Me.dgrdLogReserva.Name = "dgrdLogReserva"
        Me.dgrdLogReserva.Size = New System.Drawing.Size(1542, 603)
        Me.dgrdLogReserva.TabIndex = 2
        Me.dgrdLogReserva.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView10})
        '
        'GridView10
        '
        Me.GridView10.DetailHeight = 682
        Me.GridView10.GridControl = Me.dgrdLogReserva
        Me.GridView10.Name = "GridView10"
        Me.GridView10.OptionsView.ColumnAutoWidth = False
        Me.GridView10.OptionsView.ShowAutoFilterRow = True
        Me.GridView10.OptionsView.ShowFooter = True
        '
        'tabExistencias
        '
        Me.tabExistencias.Controls.Add(Me.dgridExistencias)
        Me.tabExistencias.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.tabExistencias.Name = "tabExistencias"
        Me.tabExistencias.Size = New System.Drawing.Size(1542, 603)
        Me.tabExistencias.Text = "Existencias"
        '
        'dgridExistencias
        '
        Me.dgridExistencias.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridExistencias.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dgridExistencias.Location = New System.Drawing.Point(0, 0)
        Me.dgridExistencias.MainView = Me.GridView11
        Me.dgridExistencias.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.dgridExistencias.MenuManager = Me.RibbonControl
        Me.dgridExistencias.Name = "dgridExistencias"
        Me.dgridExistencias.Size = New System.Drawing.Size(1542, 603)
        Me.dgridExistencias.TabIndex = 3
        Me.dgridExistencias.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView11})
        '
        'GridView11
        '
        Me.GridView11.DetailHeight = 682
        Me.GridView11.GridControl = Me.dgridExistencias
        Me.GridView11.Name = "GridView11"
        Me.GridView11.OptionsView.ColumnAutoWidth = False
        Me.GridView11.OptionsView.ShowAutoFilterRow = True
        Me.GridView11.OptionsView.ShowFooter = True
        '
        'DetalleBindingSource1
        '
        Me.DetalleBindingSource1.DataMember = "Detalle"
        Me.DetalleBindingSource1.DataSource = Me.DsDespacho
        '
        'AutoHideContainer1
        '
        Me.AutoHideContainer1.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.AutoHideContainer1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.AutoHideContainer1.Location = New System.Drawing.Point(0, 695)
        Me.AutoHideContainer1.Name = "AutoHideContainer1"
        Me.AutoHideContainer1.Size = New System.Drawing.Size(1308, 19)
        '
        'WorkspaceManager1
        '
        Me.WorkspaceManager1.TargetControl = Me
        Me.WorkspaceManager1.TransitionType = PushTransition1
        '
        'frmPedido
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1544, 882)
        Me.Controls.Add(Me.xtrPedido)
        Me.Controls.Add(Me.AutoHideContainer2)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmPedido"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Documento de salida"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpProducto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpProducto.ResumeLayout(False)
        Me.XtraScrollableControl2.ResumeLayout(False)
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.dtpFechaEntrega.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaEntrega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbRoadVendedorDespacho.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbRoadVendedorPedido.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbRoadRutaDespacho.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbRoadRutaPedido.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.cmbMuelle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbMotivoDevolucion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReferencia2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpUltTareaManufactura, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpUltTareaManufactura.ResumeLayout(False)
        CType(Me.cmbManufacturaLigera.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaPreparacion.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaPreparacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdCliente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiasVencimiento, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoPickingERP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbDocumentoRef.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReferencia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lcmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTipoPedido.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaPedido.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaPedido.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpInfoPicking, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpInfoPicking.ResumeLayout(False)
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.GroupControl3.PerformLayout()
        CType(Me.grpScanPoliza, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpScanPoliza.ResumeLayout(False)
        Me.grpScanPoliza.PerformLayout()
        CType(Me.txtScanPoliza.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpDetallePed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDetallePed.ResumeLayout(False)
        CType(Me.dgrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.ObservacionTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoadIdFacturacionSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoadIdDespachoSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoadSucursalTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoadInformadoCheckEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoadRazon_RechazadoTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoadRechazadoCheckEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoadStatProcTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoadADD3TextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoadADD2TextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoadADD1TextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoadImpresSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoadCalcoBJTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoadStatComTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoadBanderaTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoadPesoSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoadImpMontoSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoadDesMontoSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoadTotalSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoadKilometrajeSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkPedido, System.ComponentModel.ISupportInitialize).EndInit()
        Me.AutoHideContainer2.ResumeLayout(False)
        Me.DockPanel2.ResumeLayout(False)
        Me.DockPanel2_Container.ResumeLayout(False)
        Me.DockPanel2_Container.PerformLayout()
        CType(Me.Fec_modDateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtrPedido, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtrPedido.ResumeLayout(False)
        Me.tpDatosGenerales.ResumeLayout(False)
        Me.tabPoliza.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.GrpPoliza, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpPoliza.ResumeLayout(False)
        Me.XtraScrollableControl1.ResumeLayout(False)
        Me.XtraScrollableControl1.PerformLayout()
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
        Me.tpDetalleProducto.ResumeLayout(False)
        Me.tpRoadRAW.ResumeLayout(False)
        CType(Me.grRoad, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grRoad.ResumeLayout(False)
        Me.grRoad.PerformLayout()
        Me.tpStock_Reservado.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.grdStockReservado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpPicking.ResumeLayout(False)
        CType(Me.grpPicking, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPicking.ResumeLayout(False)
        CType(Me.grdPicking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EncabezadoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsPicking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetallePicking, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpDespachos.ResumeLayout(False)
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.grdDespacho, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EncabezadoBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsDespacho, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPedidoERP.ResumeLayout(False)
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        CType(Me.grdPedTras, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabComposicion.ResumeLayout(False)
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl4.ResumeLayout(False)
        CType(Me.grdComposicion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdVComp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabServiciosAsociados.ResumeLayout(False)
        CType(Me.dgridServiciosAsociados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetalleServicios, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbAcuerdoComercial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabStockLiberado.ResumeLayout(False)
        CType(Me.dgridStockLiberado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvLogStockLiberado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabLogMI3.ResumeLayout(False)
        CType(Me.dgridLogMI3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabHojaVerificacion.ResumeLayout(False)
        CType(Me.dgridVerificacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabImagenes.ResumeLayout(False)
        CType(Me.GrpImagen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpImagen.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.GrdImagen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewImg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.PicImg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabLogReserva.ResumeLayout(False)
        CType(Me.dgrdLogReserva, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabExistencias.ResumeLayout(False)
        CType(Me.dgridExistencias, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DetalleBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DetalleBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
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
    Friend WithEvents GrpProducto As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtNoDocumento As System.Windows.Forms.TextBox
    Friend WithEvents grpDetallePed As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lnkAgregarProducto As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkCliente As System.Windows.Forms.LinkLabel
    Friend WithEvents dtpHoraFinPreparacion As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpHoraInicioPreparacion As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblEstado As System.Windows.Forms.Label
    Friend WithEvents txtDiasVencimiento As System.Windows.Forms.NumericUpDown
    Friend WithEvents ObservacionTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents RoadIdFacturacionSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents RoadIdDespachoSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents RoadSucursalTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents RoadInformadoCheckEdit As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents RoadRazon_RechazadoTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents RoadRechazadoCheckEdit As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents RoadStatProcTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents RoadADD3TextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents RoadADD2TextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents RoadADD1TextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents RoadImpresSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents RoadCalcoBJTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents RoadStatComTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents RoadBanderaTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents RoadPesoSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents RoadImpMontoSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents RoadDesMontoSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents RoadTotalSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents RoadKilometrajeSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents dgrid As System.Windows.Forms.DataGridView
    Friend WithEvents lnkParametrosProducto As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkVerConfiguracionProducto As System.Windows.Forms.LinkLabel
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lnkAgregarStockEspecifico As LinkLabel
    Friend WithEvents prg As DevExpress.XtraWaitForm.ProgressPanel
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarSubItem
    Friend WithEvents cmdListaUbicacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents dkPedido As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents xtrPedido As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tpDatosGenerales As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tpDetalleProducto As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tpRoadRAW As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grRoad As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblTotal As Label
    Friend WithEvents lblCantidad As Label
    Friend WithEvents lblPeso As Label
    Friend WithEvents tpPicking As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grpPicking As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdPicking As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDetallePicking As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colIdPedido As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPropietario As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUbicaciónPicking As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colEstado As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDetalleOperador As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colHoraInicial As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colHoraFinal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colFechaPicking As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DockPanel2 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel2_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents User_agrTextEdit1 As TextBox
    Friend WithEvents User_modTextEdit1 As TextBox
    Friend WithEvents Fec_modDateEdit1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Fec_agrDateEdit1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbTipoPedido As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents dtpFechaPedido As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblIdPedidoEnc As TextBox
    Friend WithEvents mnuEstadoEnviadoAERP As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tpStock_Reservado As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents grdStockReservado As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents EncabezadoBindingSource As BindingSource
    Friend WithEvents DsPicking As DsPicking
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCódigo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DetalleBindingSource As BindingSource
    'Friend WithEvents mnuLiberarNoPickeado As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents BarButtonItem5 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuLiberarNoPickeado As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuReservaStockManual As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblNoPicking As Label
    Friend WithEvents tpDespachos As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents grdDespacho As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView7 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents EncabezadoBindingSource1 As BindingSource
    Friend WithEvents DsDespacho As DsDespacho
    Friend WithEvents colIdDespachoEnc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colFecha_Desp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colHora_ini As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colHora_fin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdPedidoEnc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DetalleBindingSource1 As BindingSource
    Friend WithEvents txtIdPicking As LinkLabel
    Friend WithEvents mnuPendiente As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents txtIdDespacho As LinkLabel
    Friend WithEvents lblUltDespacho As Label
    Friend WithEvents AutoHideContainer1 As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents txtUbicacionTransito As TextBox
    Friend WithEvents txtControlUltimoLote As TextBox
    Friend WithEvents tabPedidoERP As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents grdPedTras As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView8 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblUbicTransito As Label
    Friend WithEvents tabComposicion As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PanelControl4 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents grdComposicion As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdVComp As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lcmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents tabPoliza As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents GrpPoliza As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrpEmbarque As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmdPrepareGrid As Button
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
    Friend WithEvents dgridDetallePoliza As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvdetallepoliza As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TabServiciosAsociados As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgridServiciosAsociados As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDetalleServicios As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbAcuerdoComercial As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblAcuerdoComercial As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkActivo As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkAnulado As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkPedidoLocal As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkSyncMI3 As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkPalletPrimero As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup4 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPageGroup5 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents chkControlPoliza As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkVerificar As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup6 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents BarDockingMenuItem1 As DevExpress.XtraBars.BarDockingMenuItem
    Friend WithEvents grpInfoPicking As DevExpress.XtraEditors.GroupControl
    Friend WithEvents WorkspaceManager1 As DevExpress.Utils.WorkspaceManager
    Friend WithEvents grpScanPoliza As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtScanPoliza As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtReferencia As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbDocumentoRef As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblDocumentoRef As DevExpress.XtraEditors.LabelControl
    Friend WithEvents XtraScrollableControl1 As DevExpress.XtraEditors.XtraScrollableControl
    Friend WithEvents mnuDespachado As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuLiberarStockTodoElPedido As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuLiberarStockProductoEspecifico As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminarPedidoTablaIntermedia As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tabStockLiberado As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgridStockLiberado As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvLogStockLiberado As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents mnuEliminarPedido As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents txtNoPickingERP As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCertificadoCalidad As TextBox
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtObservacion As TextBox
    Friend WithEvents tsmiImprimirStockRes As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tabLogMI3 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgridLogMI3 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colNo_Linea As DataGridViewTextBoxColumn
    Friend WithEvents colIdProducto As DataGridViewTextBoxColumn
    Friend WithEvents colIsNew As DataGridViewTextBoxColumn
    Friend WithEvents ColCodProducto As DataGridViewTextBoxColumn
    Friend WithEvents ColNomProducto As DataGridViewTextBoxColumn
    Friend WithEvents colUnidadMedida As DataGridViewTextBoxColumn
    Friend WithEvents colPresentacion As DataGridViewComboBoxColumn
    Friend WithEvents colEstadoProducto As DataGridViewComboBoxColumn
    Friend WithEvents colCantidadExistencia As DataGridViewTextBoxColumn
    Friend WithEvents colPesoExistencia As DataGridViewTextBoxColumn
    Friend WithEvents ColCantidad As DataGridViewTextBoxColumn
    Friend WithEvents ColPeso As DataGridViewTextBoxColumn
    Friend WithEvents ColPrecio As DataGridViewTextBoxColumn
    Friend WithEvents ColTotal As DataGridViewTextBoxColumn
    Friend WithEvents ColIdStockRes As DataGridViewTextBoxColumn
    Friend WithEvents colNoDias As DataGridViewTextBoxColumn
    Friend WithEvents ColFechaEspecifica As DataGridViewCheckBoxColumn
    Friend WithEvents colNoSerie As DataGridViewTextBoxColumn
    Friend WithEvents colPesoUnitario As DataGridViewTextBoxColumn
    Friend WithEvents CantidadPickeada As DataGridViewTextBoxColumn
    Friend WithEvents CantidadVerificada As DataGridViewTextBoxColumn
    Friend WithEvents Atributo_Variante_1 As DataGridViewTextBoxColumn
    Friend WithEvents IdStockEspecifico As DataGridViewTextBoxColumn
    Friend WithEvents colIdProductoBodega As DataGridViewTextBoxColumn
    Friend WithEvents colIdPedidoDet As DataGridViewTextBoxColumn
    Friend WithEvents IdCliente As DataGridViewComboBoxColumn
    Friend WithEvents txtIdCliente As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkRequiereTarimas As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents tabHojaVerificacion As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgridVerificacion As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView9 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents mnuImprimirHojaVerificacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCrearPicking As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents XtraScrollableControl2 As DevExpress.XtraEditors.XtraScrollableControl
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtpFechaEntrega As DevExpress.XtraEditors.DateEdit
    Friend WithEvents cmbRoadVendedorDespacho As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbRoadVendedorPedido As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbRoadRutaDespacho As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbRoadRutaPedido As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents dtpHoraEntregaHasta As DateTimePicker
    Friend WithEvents txtDireccionEntrega As TextBox
    Friend WithEvents dtpHoraEntregaDesde As DateTimePicker
    Friend WithEvents dtpFechaPreparacion As DevExpress.XtraEditors.DateEdit
    Friend WithEvents AutoHideContainer2 As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents lblUbicacionAbastecimiento As Label
    Friend WithEvents txtIdUbicacionAbastecimiento As TextBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents chkFotografiaVerificacion As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents tabImagenes As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GrpImagen As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrdImagen As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewImg As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ToolStrip As ToolStrip
    Friend WithEvents cmdAdd As ToolStripButton
    Friend WithEvents cmdDelete As ToolStripButton
    Friend WithEvents Panel3 As Panel
    Friend WithEvents PicImg As PictureBox
    Friend WithEvents Label24 As Label
    Friend WithEvents lblprg As RichTextBox
    Friend WithEvents tabLogReserva As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgrdLogReserva As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView10 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbManufacturaLigera As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents grpUltTareaManufactura As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtIdManufactura As LinkLabel
    Friend WithEvents txtBodegaDestino As TextBox
    Friend WithEvents txtBodegaOrigen As TextBox
    Friend WithEvents lblBodegaDestino As Label
    Friend WithEvents lblBodegaOrigen As Label
    Friend WithEvents txtReferencia2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbMotivoDevolucion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblMotivoDevolucion As DevExpress.XtraEditors.LabelControl
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
    Friend WithEvents tabExistencias As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgridExistencias As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView11 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents mnuRevertirDespacho As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents txtIdUbicacionMuelle As TextBox
    Friend WithEvents cmbMuelle As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView12 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents mnuImprimirGridDocumentoERP As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblSociedadSAP As Label
    Friend WithEvents txtSociedadSAP As TextBox
    Friend WithEvents txtEsExportacion As TextBox
    Friend WithEvents Label45 As Label
End Class
