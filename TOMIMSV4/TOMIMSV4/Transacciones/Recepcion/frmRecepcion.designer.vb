<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRecepcion
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            'If DTOperadores IsNot Nothing Then
            '    DTOperadores.Dispose()
            '    DTOperadores = Nothing
            'End If
            If txtCodigoProductoGrid IsNot Nothing Then
                txtCodigoProductoGrid.Dispose()
                txtCodigoProductoGrid = Nothing
            End If
            If gBeRecepcionEnc IsNot Nothing Then
                gBeRecepcionEnc.Dispose()
                gBeRecepcionEnc = Nothing
            End If
            If gBeOrdenCompra IsNot Nothing Then
                gBeOrdenCompra.Dispose()
                gBeOrdenCompra = Nothing
            End If
            If BeTransOcEnc IsNot Nothing Then
                BeTransOcEnc.Dispose()
                BeTransOcEnc = Nothing
            End If
            If DgComboPresentacion IsNot Nothing Then
                DgComboPresentacion.Dispose()
                DgComboPresentacion = Nothing
            End If
            If DgComboEstado IsNot Nothing Then
                DgComboEstado.Dispose()
                DgComboEstado = Nothing
            End If
            If DgComboUnidadMedida IsNot Nothing Then
                DgComboUnidadMedida.Dispose()
                DgComboUnidadMedida = Nothing
            End If
            If DgComboArancel IsNot Nothing Then
                DgComboArancel.Dispose()
                DgComboArancel = Nothing
            End If
            If BeTareaHH IsNot Nothing Then
                BeTareaHH.Dispose()
                BeTareaHH = Nothing
            End If
            If BeProducto IsNot Nothing Then
                BeProducto.Dispose()
                BeProducto = Nothing
            End If
            If FechaVencimientoCell IsNot Nothing Then
                FechaVencimientoCell.Dispose()
                FechaVencimientoCell = Nothing
            End If
            If LoteCell IsNot Nothing Then
                LoteCell.Dispose()
                LoteCell = Nothing
            End If
            If pBeTR IsNot Nothing Then
                pBeTR.Dispose()
                pBeTR = Nothing
            End If
            If txtNoLinea IsNot Nothing Then
                txtNoLinea.Dispose()
                txtNoLinea = Nothing
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
        Dim IdPropietarioLabel As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim lblControlLote As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label30 As System.Windows.Forms.Label
        Dim Label37 As System.Windows.Forms.Label
        Dim Label38 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim SplashScreenManager1 As DevExpress.XtraSplashScreen.SplashScreenManager = New DevExpress.XtraSplashScreen.SplashScreenManager(Me, Nothing, True, True)
        Dim lblNoGuia As System.Windows.Forms.Label
        Dim lblCartaCupo As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRecepcion))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdGuardar = New DevExpress.XtraBars.BarButtonItem()
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
        Me.cmdFinalizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdPrint = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.cmdActualizarDetalle = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdPrintLabels = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
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
        Me.GrpTarea = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl10 = New DevExpress.XtraEditors.GroupControl()
        Me.dtmHoraFhh = New System.Windows.Forms.DateTimePicker()
        Me.dtmHoraIhh = New System.Windows.Forms.DateTimePicker()
        Me.GroupControl9 = New DevExpress.XtraEditors.GroupControl()
        Me.dtmHoraF = New System.Windows.Forms.DateTimePicker()
        Me.dtmHoraI = New System.Windows.Forms.DateTimePicker()
        Me.dtmFechaTarea = New DevExpress.XtraEditors.DateEdit()
        Me.GrpTransaccion = New DevExpress.XtraEditors.GroupControl()
        Me.dtmFechaRecepcion = New DevExpress.XtraEditors.DateEdit()
        Me.lblId = New System.Windows.Forms.Label()
        Me.lblMotivoAnulacion = New System.Windows.Forms.Label()
        Me.lblDiagonal = New System.Windows.Forms.Label()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblEstado = New System.Windows.Forms.Label()
        Me.txtOC = New DevExpress.XtraEditors.TextEdit()
        Me.lblC = New System.Windows.Forms.Label()
        Me.lnk = New System.Windows.Forms.LinkLabel()
        Me.txtIdOrdenCompra = New DevExpress.XtraEditors.TextEdit()
        Me.GrpObservacion = New DevExpress.XtraEditors.GroupControl()
        Me.txtObservacion = New System.Windows.Forms.TextBox()
        Me.GrpAsignacionTransaccion = New DevExpress.XtraEditors.GroupControl()
        Me.cmbMuelle = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView12 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtNombreEstado = New DevExpress.XtraEditors.TextEdit()
        Me.lnkEstadoPorDefecto = New System.Windows.Forms.LinkLabel()
        Me.txtIdEstadoDefectoRecepcion = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreUbicacion = New DevExpress.XtraEditors.TextEdit()
        Me.lnkUbicacion = New System.Windows.Forms.LinkLabel()
        Me.txtIdUbicacion = New DevExpress.XtraEditors.TextEdit()
        Me.GrpParametrosIngreso = New DevExpress.XtraEditors.GroupControl()
        Me.chkDI2REC = New DevExpress.XtraEditors.CheckEdit()
        Me.lblDIToRec = New System.Windows.Forms.Label()
        Me.chkMostrarCantidadPI = New DevExpress.XtraEditors.CheckEdit()
        Me.lblMostrarCantidadPI = New System.Windows.Forms.Label()
        Me.txtNoMarchamo = New DevExpress.XtraEditors.TextEdit()
        Me.lblMarchamo = New System.Windows.Forms.Label()
        Me.chkHabilitaStock = New DevExpress.XtraEditors.CheckEdit()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtNoDocumento = New DevExpress.XtraEditors.TextEdit()
        Me.chkParaPorCodigo = New DevExpress.XtraEditors.CheckEdit()
        Me.chkMuestraPrecio = New DevExpress.XtraEditors.CheckEdit()
        Me.chkEscanearUbicacionRec = New DevExpress.XtraEditors.CheckEdit()
        Me.chkRecepcionManual = New DevExpress.XtraEditors.CheckEdit()
        Me.chkTomarFoto = New DevExpress.XtraEditors.CheckEdit()
        Me.GrpFactura = New DevExpress.XtraEditors.GroupControl()
        Me.cmdAbajo = New DevExpress.XtraEditors.SimpleButton()
        Me.cmdArriba = New DevExpress.XtraEditors.SimpleButton()
        Me.cmdEliminarFactura = New DevExpress.XtraEditors.SimpleButton()
        Me.cmdAgregar = New DevExpress.XtraEditors.SimpleButton()
        Me.grdListaFactura = New System.Windows.Forms.DataGridView()
        Me.IdFacturaRecepcion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdRecepcion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Orden = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NoFactura = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Obs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Completa = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GrpDetalle = New DevExpress.XtraEditors.GroupControl()
        Me.DgridDetalleOC = New DevExpress.XtraGrid.GridControl()
        Me.gvDetalleDocIngreso = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GrpDetalleRecepcion = New DevExpress.XtraEditors.GroupControl()
        Me.DgridDetalleRec = New System.Windows.Forms.DataGridView()
        Me.No_Linea = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CodigoP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProductoP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PresentacionP = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.UnidadMedidaP = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.CantidadP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Peso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CostoOC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CostoP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdProductoP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaVencimiento = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Estado = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Lote = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MotivoDevolucion = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.IsNewR = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.IdRecepcionEnc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdRecepcionDet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Observacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdUbicacionDefecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ControlVencimiento = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.KeyP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PesoPresentacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ControlPeso = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Factor2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PesoUnitario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Atributo_Variante_1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lic_plate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdOrdenCompraEnc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdOrdenCompraDet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.cmdAgregarProducto = New System.Windows.Forms.ToolStripButton()
        Me.cmdVerParametros = New System.Windows.Forms.ToolStripButton()
        Me.cmdEliminarFila = New System.Windows.Forms.ToolStripButton()
        Me.lblStatus = New System.Windows.Forms.ToolStripLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblPesoR = New System.Windows.Forms.Label()
        Me.lblTotalR = New System.Windows.Forms.Label()
        Me.lblCantidadR = New System.Windows.Forms.Label()
        Me.lblCostoR = New System.Windows.Forms.Label()
        Me.GrpImagen = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.GrdImagen = New DevExpress.XtraGrid.GridControl()
        Me.GridViewImg = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.cmdAdd = New System.Windows.Forms.ToolStripButton()
        Me.cmdDelete = New System.Windows.Forms.ToolStripButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.PicImg = New System.Windows.Forms.PictureBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.GrpOperadorBodega = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl13 = New DevExpress.XtraEditors.GroupControl()
        Me.DGridOperadores = New DevExpress.XtraGrid.GridControl()
        Me.DataBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsOrdenCompraRecepcionOperador = New TOMWMS.DsOrdenCompraRecepcionOperador()
        Me.GrdOperadorBobega = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSelección = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdOperadorRec = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdOperadorBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colOperador = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colcolUsaHH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colFoto = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ToolStripPR = New System.Windows.Forms.ToolStrip()
        Me.cmdMarcarTodosOperador = New System.Windows.Forms.ToolStripButton()
        Me.cmdDesmarcarTodosOperador = New System.Windows.Forms.ToolStripButton()
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.dkRecepcion = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.xtrRecepcion = New DevExpress.XtraTab.XtraTabControl()
        Me.tabDatosRec = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.GrpTIpoTransaccion = New DevExpress.XtraEditors.GroupControl()
        Me.txtDescripcionTR = New DevExpress.XtraEditors.TextEdit()
        Me.lnkTipoT = New System.Windows.Forms.LinkLabel()
        Me.txtIdTipoTR = New DevExpress.XtraEditors.TextEdit()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.grpDatosFiscalSAT = New System.Windows.Forms.Panel()
        Me.txtNoContenedor = New DevExpress.XtraEditors.TextEdit()
        Me.txtCartaCupo = New DevExpress.XtraEditors.TextEdit()
        Me.GrpPiloto = New DevExpress.XtraEditors.GroupControl()
        Me.lnkPiloto = New System.Windows.Forms.LinkLabel()
        Me.txtNombrePiloto = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdPiloto = New DevExpress.XtraEditors.TextEdit()
        Me.GrpVehiculo = New DevExpress.XtraEditors.GroupControl()
        Me.txtNombreVehiculo = New DevExpress.XtraEditors.TextEdit()
        Me.lnkVehiculo = New System.Windows.Forms.LinkLabel()
        Me.txtIdVehiculo = New DevExpress.XtraEditors.TextEdit()
        Me.tabDetalleOC = New DevExpress.XtraTab.XtraTabPage()
        Me.tabDetRec = New DevExpress.XtraTab.XtraTabPage()
        Me.tabImagenes = New DevExpress.XtraTab.XtraTabPage()
        Me.tabDetOp = New DevExpress.XtraTab.XtraTabPage()
        Me.tabDetalleRecepcion2 = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.DgridDetalleRec2 = New DevExpress.XtraGrid.GridControl()
        Me.gvDetalleRec2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolEliminarFila = New System.Windows.Forms.ToolStripButton()
        Me.tmrActualizarDatosRecepcion = New System.Windows.Forms.Timer(Me.components)
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        IdPropietarioLabel = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        lblControlLote = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label30 = New System.Windows.Forms.Label()
        Label37 = New System.Windows.Forms.Label()
        Label38 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        lblNoGuia = New System.Windows.Forms.Label()
        lblCartaCupo = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.GrpTarea, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTarea.SuspendLayout()
        CType(Me.GroupControl10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl10.SuspendLayout()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl9.SuspendLayout()
        CType(Me.dtmFechaTarea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaTarea.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpTransaccion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTransaccion.SuspendLayout()
        CType(Me.dtmFechaRecepcion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaRecepcion.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdOrdenCompra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpObservacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpObservacion.SuspendLayout()
        CType(Me.GrpAsignacionTransaccion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpAsignacionTransaccion.SuspendLayout()
        CType(Me.cmbMuelle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdEstadoDefectoRecepcion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpParametrosIngreso, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpParametrosIngreso.SuspendLayout()
        CType(Me.chkDI2REC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMostrarCantidadPI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoMarchamo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkHabilitaStock.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoDocumento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkParaPorCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMuestraPrecio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEscanearUbicacionRec.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRecepcionManual.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTomarFoto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpFactura, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpFactura.SuspendLayout()
        CType(Me.grdListaFactura, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpDetalle.SuspendLayout()
        CType(Me.DgridDetalleOC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetalleDocIngreso, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpDetalleRecepcion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpDetalleRecepcion.SuspendLayout()
        CType(Me.DgridDetalleRec, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.GrpImagen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpImagen.SuspendLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.GrdImagen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewImg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.PicImg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpOperadorBodega, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpOperadorBodega.SuspendLayout()
        CType(Me.GroupControl13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl13.SuspendLayout()
        CType(Me.DGridOperadores, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsOrdenCompraRecepcionOperador, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdOperadorBobega, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripPR.SuspendLayout()
        CType(Me.dkRecepcion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtrRecepcion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtrRecepcion.SuspendLayout()
        Me.tabDatosRec.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.GrpTIpoTransaccion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTIpoTransaccion.SuspendLayout()
        CType(Me.txtDescripcionTR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdTipoTR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        Me.grpDatosFiscalSAT.SuspendLayout()
        CType(Me.txtNoContenedor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCartaCupo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpPiloto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpPiloto.SuspendLayout()
        CType(Me.txtNombrePiloto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdPiloto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpVehiculo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpVehiculo.SuspendLayout()
        CType(Me.txtNombreVehiculo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdVehiculo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabDetalleOC.SuspendLayout()
        Me.tabDetRec.SuspendLayout()
        Me.tabImagenes.SuspendLayout()
        Me.tabDetOp.SuspendLayout()
        Me.tabDetalleRecepcion2.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.DgridDetalleRec2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetalleRec2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(256, 14)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(85, 13)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(256, 40)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(78, 13)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(671, 14)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(89, 13)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(671, 40)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(82, 13)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'IdPropietarioLabel
        '
        IdPropietarioLabel.AutoSize = True
        IdPropietarioLabel.Location = New System.Drawing.Point(15, 118)
        IdPropietarioLabel.Name = "IdPropietarioLabel"
        IdPropietarioLabel.Size = New System.Drawing.Size(47, 13)
        IdPropietarioLabel.TabIndex = 9
        IdPropietarioLabel.Text = "Bodega:"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(15, 33)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(40, 13)
        Label12.TabIndex = 0
        Label12.Text = "Código"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(15, 145)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(63, 13)
        Label1.TabIndex = 11
        Label1.Text = "Propietario:"
        '
        'lblControlLote
        '
        lblControlLote.AutoSize = True
        lblControlLote.Location = New System.Drawing.Point(14, 91)
        lblControlLote.Name = "lblControlLote"
        lblControlLote.Size = New System.Drawing.Size(166, 13)
        lblControlLote.TabIndex = 4
        lblControlLote.Text = "Escanear ubicación en recepción:"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(264, 114)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(71, 13)
        Label6.TabIndex = 6
        Label6.Text = "Tomar Fotos:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(14, 92)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(92, 13)
        Label2.TabIndex = 7
        Label2.Text = "Fecha Recepción:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(15, 36)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(41, 13)
        Label5.TabIndex = 0
        Label5.Text = "Muelle:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(14, 114)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(97, 13)
        Label3.TabIndex = 8
        Label3.Text = "Recepción Manual:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(264, 140)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(81, 13)
        Label4.TabIndex = 10
        Label4.Text = "Muestra Costo:"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(9, 32)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(62, 13)
        Label7.TabIndex = 0
        Label7.Text = "Hora Inicio:"
        '
        'Label30
        '
        Label30.AutoSize = True
        Label30.Location = New System.Drawing.Point(9, 59)
        Label30.Name = "Label30"
        Label30.Size = New System.Drawing.Size(51, 13)
        Label30.TabIndex = 2
        Label30.Text = "Hora Fin:"
        '
        'Label37
        '
        Label37.AutoSize = True
        Label37.Location = New System.Drawing.Point(11, 59)
        Label37.Name = "Label37"
        Label37.Size = New System.Drawing.Size(51, 13)
        Label37.TabIndex = 2
        Label37.Text = "Hora Fin:"
        '
        'Label38
        '
        Label38.AutoSize = True
        Label38.Location = New System.Drawing.Point(9, 32)
        Label38.Name = "Label38"
        Label38.Size = New System.Drawing.Size(62, 13)
        Label38.TabIndex = 0
        Label38.Text = "Hora Inicio:"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(13, 37)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(85, 13)
        Label8.TabIndex = 0
        Label8.Text = "No. Documento:"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(14, 136)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(88, 13)
        Label9.TabIndex = 12
        Label9.Text = "Para por Código:"
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(15, 63)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(40, 13)
        Label10.TabIndex = 2
        Label10.Text = "Estado"
        '
        'SplashScreenManager1
        '
        SplashScreenManager1.ClosingDelay = 500
        '
        'lblNoGuia
        '
        lblNoGuia.AutoSize = True
        lblNoGuia.Location = New System.Drawing.Point(191, 15)
        lblNoGuia.Name = "lblNoGuia"
        lblNoGuia.Size = New System.Drawing.Size(88, 13)
        lblNoGuia.TabIndex = 2
        lblNoGuia.Text = "No. Contenedor:"
        '
        'lblCartaCupo
        '
        lblCartaCupo.AutoSize = True
        lblCartaCupo.Location = New System.Drawing.Point(34, 15)
        lblCartaCupo.Name = "lblCartaCupo"
        lblCartaCupo.Size = New System.Drawing.Size(81, 13)
        lblCartaCupo.TabIndex = 5
        lblCartaCupo.Text = "Carta de Cupo:"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 693)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1332, 24)
        '
        'RibbonControl
        '
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(26, 24, 26, 24)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdGuardar, Me.mnuAsignacion, Me.BarButtonItem1, Me.BarButtonItem2, Me.BarButtonItem3, Me.cmdCodigoBarra, Me.cmdImprimeCodigoBarra, Me.cmdImprmirCodigoBarra, Me.cmdActualizar, Me.cmdEliminar, Me.cmdUbicacion, Me.cmdImprimir, Me.SubImprimir, Me.cmdPreIngreso, Me.cmdCostoArancel, Me.cmdFinalizar, Me.cmdPrint, Me.lblRegs, Me.cmdActualizarDetalle, Me.cmdPrintLabels, Me.BarButtonItem4})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 24
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 283
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1332, 158)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Caption = "Guardar"
        Me.cmdGuardar.Id = 1
        Me.cmdGuardar.ImageOptions.SvgImage = CType(resources.GetObject("cmdGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdGuardar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
        Me.cmdGuardar.Name = "cmdGuardar"
        Me.cmdGuardar.ShortcutKeyDisplayString = "G"
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
        Me.SubImprimir.ImageOptions.Image = CType(resources.GetObject("SubImprimir.ImageOptions.Image"), System.Drawing.Image)
        Me.SubImprimir.ImageOptions.LargeImage = CType(resources.GetObject("SubImprimir.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.SubImprimir.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdPreIngreso), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdCostoArancel)})
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
        'cmdFinalizar
        '
        Me.cmdFinalizar.Caption = "Finalizar"
        Me.cmdFinalizar.Id = 18
        Me.cmdFinalizar.ImageOptions.SvgImage = CType(resources.GetObject("cmdFinalizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdFinalizar.Name = "cmdFinalizar"
        '
        'cmdPrint
        '
        Me.cmdPrint.Caption = "Imprimir"
        Me.cmdPrint.Id = 19
        Me.cmdPrint.ImageOptions.SvgImage = CType(resources.GetObject("cmdPrint.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdPrint.Name = "cmdPrint"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros:"
        Me.lblRegs.Id = 20
        Me.lblRegs.Name = "lblRegs"
        '
        'cmdActualizarDetalle
        '
        Me.cmdActualizarDetalle.Caption = "Actualizar detalle de HH"
        Me.cmdActualizarDetalle.Id = 21
        Me.cmdActualizarDetalle.ImageOptions.SvgImage = CType(resources.GetObject("cmdActualizarDetalle.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizarDetalle.Name = "cmdActualizarDetalle"
        '
        'cmdPrintLabels
        '
        Me.cmdPrintLabels.Caption = "Reimpresion Etiquetas"
        Me.cmdPrintLabels.Id = 22
        Me.cmdPrintLabels.ImageOptions.SvgImage = CType(resources.GetObject("cmdPrintLabels.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdPrintLabels.Name = "cmdPrintLabels"
        '
        'BarButtonItem4
        '
        Me.BarButtonItem4.Caption = "Impresión Sojet"
        Me.BarButtonItem4.Id = 23
        Me.BarButtonItem4.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem4.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem4.Name = "BarButtonItem4"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menu"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdEliminar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdFinalizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizarDetalle)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.cmdPrint)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.cmdPrintLabels)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.BarButtonItem4)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonStatusBar1
        '
        Me.RibbonStatusBar1.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar1.Location = New System.Drawing.Point(2, 467)
        Me.RibbonStatusBar1.Name = "RibbonStatusBar1"
        Me.RibbonStatusBar1.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar1.Size = New System.Drawing.Size(1400, 27)
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(344, 37)
        Me.Fec_agrDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(224, 20)
        Me.Fec_agrDateEdit.TabIndex = 5
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(344, 11)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(224, 20)
        Me.User_agrTextEdit.TabIndex = 1
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(759, 37)
        Me.Fec_modDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(224, 20)
        Me.Fec_modDateEdit.TabIndex = 7
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(759, 11)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(224, 20)
        Me.User_modTextEdit.TabIndex = 2
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
        'GrpTarea
        '
        Me.GrpTarea.Controls.Add(Me.GroupControl10)
        Me.GrpTarea.Controls.Add(Me.GroupControl9)
        Me.GrpTarea.Controls.Add(Me.dtmFechaTarea)
        Me.GrpTarea.Dock = System.Windows.Forms.DockStyle.Top
        Me.GrpTarea.Location = New System.Drawing.Point(0, 257)
        Me.GrpTarea.Name = "GrpTarea"
        Me.GrpTarea.Size = New System.Drawing.Size(509, 132)
        Me.GrpTarea.TabIndex = 5
        Me.GrpTarea.Text = "Fecha Tarea"
        '
        'GroupControl10
        '
        Me.GroupControl10.Controls.Add(Label37)
        Me.GroupControl10.Controls.Add(Me.dtmHoraFhh)
        Me.GroupControl10.Controls.Add(Label38)
        Me.GroupControl10.Controls.Add(Me.dtmHoraIhh)
        Me.GroupControl10.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupControl10.Enabled = False
        Me.GroupControl10.Location = New System.Drawing.Point(306, 47)
        Me.GroupControl10.Name = "GroupControl10"
        Me.GroupControl10.Size = New System.Drawing.Size(201, 83)
        Me.GroupControl10.TabIndex = 2
        Me.GroupControl10.Text = "Hora HandHeld"
        '
        'dtmHoraFhh
        '
        Me.dtmHoraFhh.CustomFormat = "hh:mm:ss"
        Me.dtmHoraFhh.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtmHoraFhh.Location = New System.Drawing.Point(78, 55)
        Me.dtmHoraFhh.Name = "dtmHoraFhh"
        Me.dtmHoraFhh.ShowUpDown = True
        Me.dtmHoraFhh.Size = New System.Drawing.Size(113, 21)
        Me.dtmHoraFhh.TabIndex = 3
        '
        'dtmHoraIhh
        '
        Me.dtmHoraIhh.CustomFormat = "hh:mm:ss"
        Me.dtmHoraIhh.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtmHoraIhh.Location = New System.Drawing.Point(78, 28)
        Me.dtmHoraIhh.Name = "dtmHoraIhh"
        Me.dtmHoraIhh.ShowUpDown = True
        Me.dtmHoraIhh.Size = New System.Drawing.Size(113, 21)
        Me.dtmHoraIhh.TabIndex = 1
        '
        'GroupControl9
        '
        Me.GroupControl9.Controls.Add(Label30)
        Me.GroupControl9.Controls.Add(Me.dtmHoraF)
        Me.GroupControl9.Controls.Add(Label7)
        Me.GroupControl9.Controls.Add(Me.dtmHoraI)
        Me.GroupControl9.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupControl9.Location = New System.Drawing.Point(2, 47)
        Me.GroupControl9.Name = "GroupControl9"
        Me.GroupControl9.Size = New System.Drawing.Size(200, 83)
        Me.GroupControl9.TabIndex = 1
        Me.GroupControl9.Text = "Hora Teórica"
        '
        'dtmHoraF
        '
        Me.dtmHoraF.CustomFormat = "hh:mm:ss"
        Me.dtmHoraF.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtmHoraF.Location = New System.Drawing.Point(77, 55)
        Me.dtmHoraF.Name = "dtmHoraF"
        Me.dtmHoraF.ShowUpDown = True
        Me.dtmHoraF.Size = New System.Drawing.Size(113, 21)
        Me.dtmHoraF.TabIndex = 3
        '
        'dtmHoraI
        '
        Me.dtmHoraI.CustomFormat = "hh:mm:ss"
        Me.dtmHoraI.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtmHoraI.Location = New System.Drawing.Point(77, 28)
        Me.dtmHoraI.Name = "dtmHoraI"
        Me.dtmHoraI.ShowUpDown = True
        Me.dtmHoraI.Size = New System.Drawing.Size(113, 21)
        Me.dtmHoraI.TabIndex = 1
        '
        'dtmFechaTarea
        '
        Me.dtmFechaTarea.Dock = System.Windows.Forms.DockStyle.Top
        Me.dtmFechaTarea.EditValue = New Date(2017, 11, 19, 11, 13, 59, 0)
        Me.dtmFechaTarea.Location = New System.Drawing.Point(2, 23)
        Me.dtmFechaTarea.MenuManager = Me.RibbonControl
        Me.dtmFechaTarea.Name = "dtmFechaTarea"
        Me.dtmFechaTarea.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtmFechaTarea.Properties.Appearance.Options.UseFont = True
        Me.dtmFechaTarea.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaTarea.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaTarea.Size = New System.Drawing.Size(505, 24)
        Me.dtmFechaTarea.TabIndex = 0
        '
        'GrpTransaccion
        '
        Me.GrpTransaccion.Controls.Add(Me.dtmFechaRecepcion)
        Me.GrpTransaccion.Controls.Add(Me.lblId)
        Me.GrpTransaccion.Controls.Add(Me.lblMotivoAnulacion)
        Me.GrpTransaccion.Controls.Add(Me.lblDiagonal)
        Me.GrpTransaccion.Controls.Add(Me.cmbPropietario)
        Me.GrpTransaccion.Controls.Add(Me.cmbBodega)
        Me.GrpTransaccion.Controls.Add(Me.lblEstado)
        Me.GrpTransaccion.Controls.Add(Label10)
        Me.GrpTransaccion.Controls.Add(Label12)
        Me.GrpTransaccion.Controls.Add(Label2)
        Me.GrpTransaccion.Controls.Add(IdPropietarioLabel)
        Me.GrpTransaccion.Controls.Add(Me.txtOC)
        Me.GrpTransaccion.Controls.Add(Me.lblC)
        Me.GrpTransaccion.Controls.Add(Me.lnk)
        Me.GrpTransaccion.Controls.Add(Label1)
        Me.GrpTransaccion.Controls.Add(Me.txtIdOrdenCompra)
        Me.GrpTransaccion.Dock = System.Windows.Forms.DockStyle.Top
        Me.GrpTransaccion.Location = New System.Drawing.Point(0, 0)
        Me.GrpTransaccion.Name = "GrpTransaccion"
        Me.GrpTransaccion.Size = New System.Drawing.Size(397, 228)
        Me.GrpTransaccion.TabIndex = 0
        Me.GrpTransaccion.Text = "Transacción"
        '
        'dtmFechaRecepcion
        '
        Me.dtmFechaRecepcion.EditValue = New Date(2017, 11, 19, 11, 13, 59, 0)
        Me.dtmFechaRecepcion.Location = New System.Drawing.Point(121, 90)
        Me.dtmFechaRecepcion.MenuManager = Me.RibbonControl
        Me.dtmFechaRecepcion.Name = "dtmFechaRecepcion"
        Me.dtmFechaRecepcion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaRecepcion.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaRecepcion.Size = New System.Drawing.Size(282, 20)
        Me.dtmFechaRecepcion.TabIndex = 8
        '
        'lblId
        '
        Me.lblId.AutoSize = True
        Me.lblId.Location = New System.Drawing.Point(180, 65)
        Me.lblId.Name = "lblId"
        Me.lblId.Size = New System.Drawing.Size(0, 13)
        Me.lblId.TabIndex = 5
        Me.lblId.Visible = False
        '
        'lblMotivoAnulacion
        '
        Me.lblMotivoAnulacion.AutoSize = True
        Me.lblMotivoAnulacion.Location = New System.Drawing.Point(194, 65)
        Me.lblMotivoAnulacion.Name = "lblMotivoAnulacion"
        Me.lblMotivoAnulacion.Size = New System.Drawing.Size(0, 13)
        Me.lblMotivoAnulacion.TabIndex = 6
        Me.lblMotivoAnulacion.Visible = False
        '
        'lblDiagonal
        '
        Me.lblDiagonal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDiagonal.Location = New System.Drawing.Point(123, 63)
        Me.lblDiagonal.Name = "lblDiagonal"
        Me.lblDiagonal.Size = New System.Drawing.Size(281, 19)
        Me.lblDiagonal.TabIndex = 4
        Me.lblDiagonal.Visible = False
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Location = New System.Drawing.Point(121, 143)
        Me.cmbPropietario.MenuManager = Me.RibbonControl
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Size = New System.Drawing.Size(282, 20)
        Me.cmbPropietario.TabIndex = 12
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(121, 116)
        Me.cmbBodega.MenuManager = Me.RibbonControl
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(282, 20)
        Me.cmbBodega.TabIndex = 10
        '
        'lblEstado
        '
        Me.lblEstado.AutoSize = True
        Me.lblEstado.Location = New System.Drawing.Point(123, 65)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(0, 13)
        Me.lblEstado.TabIndex = 3
        '
        'txtOC
        '
        Me.txtOC.Location = New System.Drawing.Point(197, 173)
        Me.txtOC.MenuManager = Me.RibbonControl
        Me.txtOC.Name = "txtOC"
        Me.txtOC.Properties.ReadOnly = True
        Me.txtOC.Size = New System.Drawing.Size(206, 20)
        Me.txtOC.TabIndex = 15
        '
        'lblC
        '
        Me.lblC.BackColor = System.Drawing.Color.Bisque
        Me.lblC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblC.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblC.Location = New System.Drawing.Point(123, 32)
        Me.lblC.Name = "lblC"
        Me.lblC.Size = New System.Drawing.Size(281, 22)
        Me.lblC.TabIndex = 1
        '
        'lnk
        '
        Me.lnk.Location = New System.Drawing.Point(15, 164)
        Me.lnk.Name = "lnk"
        Me.lnk.Size = New System.Drawing.Size(91, 35)
        Me.lnk.TabIndex = 13
        Me.lnk.TabStop = True
        Me.lnk.Text = "Documento"
        Me.lnk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtIdOrdenCompra
        '
        Me.txtIdOrdenCompra.Location = New System.Drawing.Point(121, 173)
        Me.txtIdOrdenCompra.MenuManager = Me.RibbonControl
        Me.txtIdOrdenCompra.Name = "txtIdOrdenCompra"
        Me.txtIdOrdenCompra.Properties.Mask.EditMask = "n0"
        Me.txtIdOrdenCompra.Size = New System.Drawing.Size(70, 20)
        Me.txtIdOrdenCompra.TabIndex = 14
        '
        'GrpObservacion
        '
        Me.GrpObservacion.Controls.Add(Me.txtObservacion)
        Me.GrpObservacion.Dock = System.Windows.Forms.DockStyle.Top
        Me.GrpObservacion.Location = New System.Drawing.Point(0, 345)
        Me.GrpObservacion.Name = "GrpObservacion"
        Me.GrpObservacion.Size = New System.Drawing.Size(397, 207)
        Me.GrpObservacion.TabIndex = 4
        Me.GrpObservacion.Text = "Observación"
        '
        'txtObservacion
        '
        Me.txtObservacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtObservacion.Location = New System.Drawing.Point(2, 23)
        Me.txtObservacion.MaxLength = 100
        Me.txtObservacion.Multiline = True
        Me.txtObservacion.Name = "txtObservacion"
        Me.txtObservacion.Size = New System.Drawing.Size(393, 182)
        Me.txtObservacion.TabIndex = 0
        '
        'GrpAsignacionTransaccion
        '
        Me.GrpAsignacionTransaccion.Controls.Add(Me.cmbMuelle)
        Me.GrpAsignacionTransaccion.Controls.Add(Me.txtNombreEstado)
        Me.GrpAsignacionTransaccion.Controls.Add(Me.lnkEstadoPorDefecto)
        Me.GrpAsignacionTransaccion.Controls.Add(Me.txtIdEstadoDefectoRecepcion)
        Me.GrpAsignacionTransaccion.Controls.Add(Label5)
        Me.GrpAsignacionTransaccion.Controls.Add(Me.txtNombreUbicacion)
        Me.GrpAsignacionTransaccion.Controls.Add(Me.lnkUbicacion)
        Me.GrpAsignacionTransaccion.Controls.Add(Me.txtIdUbicacion)
        Me.GrpAsignacionTransaccion.Dock = System.Windows.Forms.DockStyle.Top
        Me.GrpAsignacionTransaccion.Location = New System.Drawing.Point(0, 228)
        Me.GrpAsignacionTransaccion.Name = "GrpAsignacionTransaccion"
        Me.GrpAsignacionTransaccion.Size = New System.Drawing.Size(397, 117)
        Me.GrpAsignacionTransaccion.TabIndex = 3
        Me.GrpAsignacionTransaccion.Text = "Asignación de Transacción"
        '
        'cmbMuelle
        '
        Me.cmbMuelle.Location = New System.Drawing.Point(121, 31)
        Me.cmbMuelle.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmbMuelle.MenuManager = Me.RibbonControl
        Me.cmbMuelle.Name = "cmbMuelle"
        Me.cmbMuelle.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMuelle.Properties.NullText = ""
        Me.cmbMuelle.Properties.PopupView = Me.GridView12
        Me.cmbMuelle.Size = New System.Drawing.Size(282, 20)
        Me.cmbMuelle.TabIndex = 100
        '
        'GridView12
        '
        Me.GridView12.DetailHeight = 554
        Me.GridView12.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView12.Name = "GridView12"
        Me.GridView12.OptionsEditForm.PopupEditFormWidth = 686
        Me.GridView12.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView12.OptionsView.ShowAutoFilterRow = True
        Me.GridView12.OptionsView.ShowGroupPanel = False
        '
        'txtNombreEstado
        '
        Me.txtNombreEstado.Location = New System.Drawing.Point(197, 86)
        Me.txtNombreEstado.MenuManager = Me.RibbonControl
        Me.txtNombreEstado.Name = "txtNombreEstado"
        Me.txtNombreEstado.Properties.MaskSettings.Set("mask", "n0")
        Me.txtNombreEstado.Properties.ReadOnly = True
        Me.txtNombreEstado.Size = New System.Drawing.Size(206, 20)
        Me.txtNombreEstado.TabIndex = 7
        '
        'lnkEstadoPorDefecto
        '
        Me.lnkEstadoPorDefecto.AutoSize = True
        Me.lnkEstadoPorDefecto.Location = New System.Drawing.Point(15, 89)
        Me.lnkEstadoPorDefecto.Name = "lnkEstadoPorDefecto"
        Me.lnkEstadoPorDefecto.Size = New System.Drawing.Size(40, 13)
        Me.lnkEstadoPorDefecto.TabIndex = 5
        Me.lnkEstadoPorDefecto.TabStop = True
        Me.lnkEstadoPorDefecto.Text = "Estado"
        '
        'txtIdEstadoDefectoRecepcion
        '
        Me.txtIdEstadoDefectoRecepcion.Location = New System.Drawing.Point(121, 86)
        Me.txtIdEstadoDefectoRecepcion.MenuManager = Me.RibbonControl
        Me.txtIdEstadoDefectoRecepcion.Name = "txtIdEstadoDefectoRecepcion"
        Me.txtIdEstadoDefectoRecepcion.Properties.MaskSettings.Set("mask", "n0")
        Me.txtIdEstadoDefectoRecepcion.Size = New System.Drawing.Size(70, 20)
        Me.txtIdEstadoDefectoRecepcion.TabIndex = 6
        '
        'txtNombreUbicacion
        '
        Me.txtNombreUbicacion.Location = New System.Drawing.Point(197, 60)
        Me.txtNombreUbicacion.MenuManager = Me.RibbonControl
        Me.txtNombreUbicacion.Name = "txtNombreUbicacion"
        Me.txtNombreUbicacion.Properties.Mask.EditMask = "n0"
        Me.txtNombreUbicacion.Properties.ReadOnly = True
        Me.txtNombreUbicacion.Size = New System.Drawing.Size(206, 20)
        Me.txtNombreUbicacion.TabIndex = 4
        '
        'lnkUbicacion
        '
        Me.lnkUbicacion.AutoSize = True
        Me.lnkUbicacion.Location = New System.Drawing.Point(15, 63)
        Me.lnkUbicacion.Name = "lnkUbicacion"
        Me.lnkUbicacion.Size = New System.Drawing.Size(52, 13)
        Me.lnkUbicacion.TabIndex = 2
        Me.lnkUbicacion.TabStop = True
        Me.lnkUbicacion.Text = "Ubicación"
        '
        'txtIdUbicacion
        '
        Me.txtIdUbicacion.Location = New System.Drawing.Point(121, 60)
        Me.txtIdUbicacion.MenuManager = Me.RibbonControl
        Me.txtIdUbicacion.Name = "txtIdUbicacion"
        Me.txtIdUbicacion.Properties.Mask.EditMask = "n0"
        Me.txtIdUbicacion.Size = New System.Drawing.Size(70, 20)
        Me.txtIdUbicacion.TabIndex = 3
        '
        'GrpParametrosIngreso
        '
        Me.GrpParametrosIngreso.Controls.Add(Me.chkDI2REC)
        Me.GrpParametrosIngreso.Controls.Add(Me.lblDIToRec)
        Me.GrpParametrosIngreso.Controls.Add(Me.chkMostrarCantidadPI)
        Me.GrpParametrosIngreso.Controls.Add(Me.lblMostrarCantidadPI)
        Me.GrpParametrosIngreso.Controls.Add(Me.txtNoMarchamo)
        Me.GrpParametrosIngreso.Controls.Add(Me.lblMarchamo)
        Me.GrpParametrosIngreso.Controls.Add(Me.chkHabilitaStock)
        Me.GrpParametrosIngreso.Controls.Add(Me.Label11)
        Me.GrpParametrosIngreso.Controls.Add(Label9)
        Me.GrpParametrosIngreso.Controls.Add(Me.txtNoDocumento)
        Me.GrpParametrosIngreso.Controls.Add(Me.chkParaPorCodigo)
        Me.GrpParametrosIngreso.Controls.Add(Label4)
        Me.GrpParametrosIngreso.Controls.Add(Me.chkMuestraPrecio)
        Me.GrpParametrosIngreso.Controls.Add(Me.chkEscanearUbicacionRec)
        Me.GrpParametrosIngreso.Controls.Add(Me.chkRecepcionManual)
        Me.GrpParametrosIngreso.Controls.Add(lblControlLote)
        Me.GrpParametrosIngreso.Controls.Add(Label3)
        Me.GrpParametrosIngreso.Controls.Add(Me.chkTomarFoto)
        Me.GrpParametrosIngreso.Controls.Add(Label8)
        Me.GrpParametrosIngreso.Controls.Add(Label6)
        Me.GrpParametrosIngreso.Dock = System.Windows.Forms.DockStyle.Top
        Me.GrpParametrosIngreso.Location = New System.Drawing.Point(0, 62)
        Me.GrpParametrosIngreso.Name = "GrpParametrosIngreso"
        Me.GrpParametrosIngreso.Size = New System.Drawing.Size(509, 195)
        Me.GrpParametrosIngreso.TabIndex = 2
        Me.GrpParametrosIngreso.Text = "Parametros de Ingreso"
        '
        'chkDI2REC
        '
        Me.chkDI2REC.Enabled = False
        Me.chkDI2REC.Location = New System.Drawing.Point(386, 162)
        Me.chkDI2REC.MenuManager = Me.RibbonControl
        Me.chkDI2REC.Name = "chkDI2REC"
        Me.chkDI2REC.Properties.Caption = ""
        Me.chkDI2REC.Size = New System.Drawing.Size(35, 20)
        Me.chkDI2REC.TabIndex = 19
        '
        'lblDIToRec
        '
        Me.lblDIToRec.AutoSize = True
        Me.lblDIToRec.Location = New System.Drawing.Point(265, 162)
        Me.lblDIToRec.Name = "lblDIToRec"
        Me.lblDIToRec.Size = New System.Drawing.Size(44, 13)
        Me.lblDIToRec.TabIndex = 18
        Me.lblDIToRec.Text = "DI2REC"
        '
        'chkMostrarCantidadPI
        '
        Me.chkMostrarCantidadPI.EditValue = True
        Me.chkMostrarCantidadPI.Location = New System.Drawing.Point(386, 89)
        Me.chkMostrarCantidadPI.MenuManager = Me.RibbonControl
        Me.chkMostrarCantidadPI.Name = "chkMostrarCantidadPI"
        Me.chkMostrarCantidadPI.Properties.Caption = ""
        Me.chkMostrarCantidadPI.Size = New System.Drawing.Size(24, 20)
        Me.chkMostrarCantidadPI.TabIndex = 17
        '
        'lblMostrarCantidadPI
        '
        Me.lblMostrarCantidadPI.AutoSize = True
        Me.lblMostrarCantidadPI.Location = New System.Drawing.Point(264, 91)
        Me.lblMostrarCantidadPI.Name = "lblMostrarCantidadPI"
        Me.lblMostrarCantidadPI.Size = New System.Drawing.Size(112, 13)
        Me.lblMostrarCantidadPI.TabIndex = 16
        Me.lblMostrarCantidadPI.Text = "Mostrar Cantidad D.I."
        '
        'txtNoMarchamo
        '
        Me.txtNoMarchamo.Location = New System.Drawing.Point(108, 60)
        Me.txtNoMarchamo.MenuManager = Me.RibbonControl
        Me.txtNoMarchamo.Name = "txtNoMarchamo"
        Me.txtNoMarchamo.Properties.Mask.EditMask = "n0"
        Me.txtNoMarchamo.Size = New System.Drawing.Size(295, 20)
        Me.txtNoMarchamo.TabIndex = 3
        '
        'lblMarchamo
        '
        Me.lblMarchamo.AutoSize = True
        Me.lblMarchamo.Location = New System.Drawing.Point(13, 63)
        Me.lblMarchamo.Name = "lblMarchamo"
        Me.lblMarchamo.Size = New System.Drawing.Size(80, 13)
        Me.lblMarchamo.TabIndex = 2
        Me.lblMarchamo.Text = "No. Marchamo:"
        '
        'chkHabilitaStock
        '
        Me.chkHabilitaStock.Location = New System.Drawing.Point(213, 160)
        Me.chkHabilitaStock.MenuManager = Me.RibbonControl
        Me.chkHabilitaStock.Name = "chkHabilitaStock"
        Me.chkHabilitaStock.Properties.Caption = ""
        Me.chkHabilitaStock.Size = New System.Drawing.Size(35, 20)
        Me.chkHabilitaStock.TabIndex = 15
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(14, 162)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(79, 13)
        Me.Label11.TabIndex = 14
        Me.Label11.Text = "Habilitar Stock:"
        '
        'txtNoDocumento
        '
        Me.txtNoDocumento.Location = New System.Drawing.Point(108, 34)
        Me.txtNoDocumento.MenuManager = Me.RibbonControl
        Me.txtNoDocumento.Name = "txtNoDocumento"
        Me.txtNoDocumento.Properties.Mask.EditMask = "n0"
        Me.txtNoDocumento.Size = New System.Drawing.Size(295, 20)
        Me.txtNoDocumento.TabIndex = 1
        '
        'chkParaPorCodigo
        '
        Me.chkParaPorCodigo.Location = New System.Drawing.Point(213, 134)
        Me.chkParaPorCodigo.MenuManager = Me.RibbonControl
        Me.chkParaPorCodigo.Name = "chkParaPorCodigo"
        Me.chkParaPorCodigo.Properties.Caption = ""
        Me.chkParaPorCodigo.Size = New System.Drawing.Size(35, 20)
        Me.chkParaPorCodigo.TabIndex = 13
        '
        'chkMuestraPrecio
        '
        Me.chkMuestraPrecio.Location = New System.Drawing.Point(386, 136)
        Me.chkMuestraPrecio.MenuManager = Me.RibbonControl
        Me.chkMuestraPrecio.Name = "chkMuestraPrecio"
        Me.chkMuestraPrecio.Properties.Caption = ""
        Me.chkMuestraPrecio.Size = New System.Drawing.Size(35, 20)
        Me.chkMuestraPrecio.TabIndex = 11
        '
        'chkEscanearUbicacionRec
        '
        Me.chkEscanearUbicacionRec.Location = New System.Drawing.Point(213, 89)
        Me.chkEscanearUbicacionRec.MenuManager = Me.RibbonControl
        Me.chkEscanearUbicacionRec.Name = "chkEscanearUbicacionRec"
        Me.chkEscanearUbicacionRec.Properties.Caption = ""
        Me.chkEscanearUbicacionRec.Size = New System.Drawing.Size(35, 20)
        Me.chkEscanearUbicacionRec.TabIndex = 5
        '
        'chkRecepcionManual
        '
        Me.chkRecepcionManual.Location = New System.Drawing.Point(213, 110)
        Me.chkRecepcionManual.MenuManager = Me.RibbonControl
        Me.chkRecepcionManual.Name = "chkRecepcionManual"
        Me.chkRecepcionManual.Properties.Caption = ""
        Me.chkRecepcionManual.Size = New System.Drawing.Size(35, 20)
        Me.chkRecepcionManual.TabIndex = 9
        '
        'chkTomarFoto
        '
        Me.chkTomarFoto.Location = New System.Drawing.Point(386, 110)
        Me.chkTomarFoto.MenuManager = Me.RibbonControl
        Me.chkTomarFoto.Name = "chkTomarFoto"
        Me.chkTomarFoto.Properties.Caption = ""
        Me.chkTomarFoto.Size = New System.Drawing.Size(35, 20)
        Me.chkTomarFoto.TabIndex = 7
        '
        'GrpFactura
        '
        Me.GrpFactura.Controls.Add(Me.cmdAbajo)
        Me.GrpFactura.Controls.Add(Me.cmdArriba)
        Me.GrpFactura.Controls.Add(Me.cmdEliminarFactura)
        Me.GrpFactura.Controls.Add(Me.cmdAgregar)
        Me.GrpFactura.Controls.Add(Me.grdListaFactura)
        Me.GrpFactura.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpFactura.Location = New System.Drawing.Point(0, 0)
        Me.GrpFactura.Name = "GrpFactura"
        Me.GrpFactura.Size = New System.Drawing.Size(421, 253)
        Me.GrpFactura.TabIndex = 1
        Me.GrpFactura.Text = "Facturas asociadas"
        '
        'cmdAbajo
        '
        Me.cmdAbajo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAbajo.ImageOptions.Image = CType(resources.GetObject("cmdAbajo.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdAbajo.Location = New System.Drawing.Point(372, 192)
        Me.cmdAbajo.Name = "cmdAbajo"
        Me.cmdAbajo.Size = New System.Drawing.Size(41, 39)
        Me.cmdAbajo.TabIndex = 4
        '
        'cmdArriba
        '
        Me.cmdArriba.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdArriba.ImageOptions.Image = CType(resources.GetObject("cmdArriba.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdArriba.Location = New System.Drawing.Point(372, 145)
        Me.cmdArriba.Name = "cmdArriba"
        Me.cmdArriba.Size = New System.Drawing.Size(41, 39)
        Me.cmdArriba.TabIndex = 3
        '
        'cmdEliminarFactura
        '
        Me.cmdEliminarFactura.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdEliminarFactura.ImageOptions.Image = CType(resources.GetObject("cmdEliminarFactura.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdEliminarFactura.Location = New System.Drawing.Point(372, 101)
        Me.cmdEliminarFactura.Name = "cmdEliminarFactura"
        Me.cmdEliminarFactura.Size = New System.Drawing.Size(41, 39)
        Me.cmdEliminarFactura.TabIndex = 2
        '
        'cmdAgregar
        '
        Me.cmdAgregar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAgregar.ImageOptions.Image = CType(resources.GetObject("cmdAgregar.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdAgregar.Location = New System.Drawing.Point(372, 56)
        Me.cmdAgregar.Name = "cmdAgregar"
        Me.cmdAgregar.Size = New System.Drawing.Size(41, 39)
        Me.cmdAgregar.TabIndex = 1
        '
        'grdListaFactura
        '
        Me.grdListaFactura.AllowUserToAddRows = False
        Me.grdListaFactura.AllowUserToDeleteRows = False
        Me.grdListaFactura.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.grdListaFactura.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SteelBlue
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdListaFactura.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdListaFactura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdListaFactura.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdFacturaRecepcion, Me.IdRecepcion, Me.Orden, Me.NoFactura, Me.Obs, Me.Completa})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SteelBlue
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdListaFactura.DefaultCellStyle = DataGridViewCellStyle2
        Me.grdListaFactura.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdListaFactura.GridColor = System.Drawing.SystemColors.ActiveBorder
        Me.grdListaFactura.Location = New System.Drawing.Point(2, 23)
        Me.grdListaFactura.MultiSelect = False
        Me.grdListaFactura.Name = "grdListaFactura"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdListaFactura.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdListaFactura.RowHeadersVisible = False
        Me.grdListaFactura.RowHeadersWidth = 51
        Me.grdListaFactura.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdListaFactura.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdListaFactura.ShowEditingIcon = False
        Me.grdListaFactura.ShowRowErrors = False
        Me.grdListaFactura.Size = New System.Drawing.Size(417, 228)
        Me.grdListaFactura.TabIndex = 0
        '
        'IdFacturaRecepcion
        '
        Me.IdFacturaRecepcion.HeaderText = "IdFacturaRecepcion"
        Me.IdFacturaRecepcion.MinimumWidth = 6
        Me.IdFacturaRecepcion.Name = "IdFacturaRecepcion"
        Me.IdFacturaRecepcion.ReadOnly = True
        Me.IdFacturaRecepcion.Visible = False
        Me.IdFacturaRecepcion.Width = 125
        '
        'IdRecepcion
        '
        Me.IdRecepcion.HeaderText = "IdRecepcion"
        Me.IdRecepcion.MinimumWidth = 6
        Me.IdRecepcion.Name = "IdRecepcion"
        Me.IdRecepcion.ReadOnly = True
        Me.IdRecepcion.Visible = False
        Me.IdRecepcion.Width = 125
        '
        'Orden
        '
        Me.Orden.HeaderText = "Orden"
        Me.Orden.MinimumWidth = 6
        Me.Orden.Name = "Orden"
        Me.Orden.ReadOnly = True
        Me.Orden.Width = 125
        '
        'NoFactura
        '
        Me.NoFactura.HeaderText = "No. Factura"
        Me.NoFactura.MinimumWidth = 6
        Me.NoFactura.Name = "NoFactura"
        Me.NoFactura.ReadOnly = True
        Me.NoFactura.Width = 125
        '
        'Obs
        '
        Me.Obs.HeaderText = "Observación"
        Me.Obs.MinimumWidth = 6
        Me.Obs.Name = "Obs"
        Me.Obs.ReadOnly = True
        Me.Obs.Width = 175
        '
        'Completa
        '
        Me.Completa.HeaderText = "Completa"
        Me.Completa.MinimumWidth = 6
        Me.Completa.Name = "Completa"
        Me.Completa.ReadOnly = True
        Me.Completa.Width = 75
        '
        'GrpDetalle
        '
        Me.GrpDetalle.Controls.Add(Me.DgridDetalleOC)
        Me.GrpDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpDetalle.Location = New System.Drawing.Point(0, 0)
        Me.GrpDetalle.Name = "GrpDetalle"
        Me.GrpDetalle.Size = New System.Drawing.Size(1408, 521)
        Me.GrpDetalle.TabIndex = 0
        Me.GrpDetalle.Text = "Lista de Productos"
        '
        'DgridDetalleOC
        '
        Me.DgridDetalleOC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridDetalleOC.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DgridDetalleOC.Location = New System.Drawing.Point(2, 23)
        Me.DgridDetalleOC.MainView = Me.gvDetalleDocIngreso
        Me.DgridDetalleOC.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DgridDetalleOC.Name = "DgridDetalleOC"
        Me.DgridDetalleOC.Size = New System.Drawing.Size(1404, 496)
        Me.DgridDetalleOC.TabIndex = 20
        Me.DgridDetalleOC.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvDetalleDocIngreso})
        '
        'gvDetalleDocIngreso
        '
        Me.gvDetalleDocIngreso.DetailHeight = 284
        Me.gvDetalleDocIngreso.GridControl = Me.DgridDetalleOC
        Me.gvDetalleDocIngreso.Name = "gvDetalleDocIngreso"
        Me.gvDetalleDocIngreso.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvDetalleDocIngreso.OptionsEditForm.PopupEditFormWidth = 686
        Me.gvDetalleDocIngreso.OptionsView.ShowAutoFilterRow = True
        Me.gvDetalleDocIngreso.OptionsView.ShowGroupPanel = False
        '
        'GrpDetalleRecepcion
        '
        Me.GrpDetalleRecepcion.Controls.Add(Me.DgridDetalleRec)
        Me.GrpDetalleRecepcion.Controls.Add(Me.ToolStrip1)
        Me.GrpDetalleRecepcion.Controls.Add(Me.Panel1)
        Me.GrpDetalleRecepcion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpDetalleRecepcion.Location = New System.Drawing.Point(0, 0)
        Me.GrpDetalleRecepcion.Name = "GrpDetalleRecepcion"
        Me.GrpDetalleRecepcion.Size = New System.Drawing.Size(1408, 521)
        Me.GrpDetalleRecepcion.TabIndex = 0
        Me.GrpDetalleRecepcion.Text = "Lista de Productos"
        '
        'DgridDetalleRec
        '
        Me.DgridDetalleRec.AllowUserToDeleteRows = False
        Me.DgridDetalleRec.AllowUserToResizeRows = False
        Me.DgridDetalleRec.BackgroundColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgridDetalleRec.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DgridDetalleRec.ColumnHeadersHeight = 40
        Me.DgridDetalleRec.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No_Linea, Me.CodigoP, Me.ProductoP, Me.PresentacionP, Me.UnidadMedidaP, Me.CantidadP, Me.Peso, Me.CostoOC, Me.CostoP, Me.TotalP, Me.IdProductoP, Me.FechaVencimiento, Me.Estado, Me.Lote, Me.MotivoDevolucion, Me.IsNewR, Me.IdRecepcionEnc, Me.IdRecepcionDet, Me.Observacion, Me.IdUbicacionDefecto, Me.ControlVencimiento, Me.KeyP, Me.PesoPresentacion, Me.ControlPeso, Me.Factor2, Me.PesoUnitario, Me.Atributo_Variante_1, Me.lic_plate, Me.IdOrdenCompraEnc, Me.IdOrdenCompraDet})
        Me.DgridDetalleRec.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridDetalleRec.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.DgridDetalleRec.EnableHeadersVisualStyles = False
        Me.DgridDetalleRec.GridColor = System.Drawing.Color.Navy
        Me.DgridDetalleRec.Location = New System.Drawing.Point(2, 50)
        Me.DgridDetalleRec.MultiSelect = False
        Me.DgridDetalleRec.Name = "DgridDetalleRec"
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Tahoma", 8.25!)
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgridDetalleRec.RowHeadersDefaultCellStyle = DataGridViewCellStyle14
        Me.DgridDetalleRec.RowHeadersVisible = False
        Me.DgridDetalleRec.RowHeadersWidth = 60
        Me.DgridDetalleRec.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgridDetalleRec.Size = New System.Drawing.Size(1404, 420)
        Me.DgridDetalleRec.TabIndex = 1
        '
        'No_Linea
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.Format = "N0"
        Me.No_Linea.DefaultCellStyle = DataGridViewCellStyle5
        Me.No_Linea.HeaderText = "No. Linea"
        Me.No_Linea.MinimumWidth = 6
        Me.No_Linea.Name = "No_Linea"
        Me.No_Linea.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.No_Linea.Width = 97
        '
        'CodigoP
        '
        Me.CodigoP.HeaderText = "Código"
        Me.CodigoP.MinimumWidth = 6
        Me.CodigoP.Name = "CodigoP"
        Me.CodigoP.Width = 97
        '
        'ProductoP
        '
        Me.ProductoP.HeaderText = "Producto"
        Me.ProductoP.MinimumWidth = 6
        Me.ProductoP.Name = "ProductoP"
        Me.ProductoP.ReadOnly = True
        Me.ProductoP.Width = 96
        '
        'PresentacionP
        '
        Me.PresentacionP.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.PresentacionP.HeaderText = "Presentación"
        Me.PresentacionP.MinimumWidth = 6
        Me.PresentacionP.Name = "PresentacionP"
        Me.PresentacionP.Width = 97
        '
        'UnidadMedidaP
        '
        Me.UnidadMedidaP.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.UnidadMedidaP.HeaderText = "U.M. Bas"
        Me.UnidadMedidaP.MinimumWidth = 6
        Me.UnidadMedidaP.Name = "UnidadMedidaP"
        Me.UnidadMedidaP.Width = 97
        '
        'CantidadP
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.CantidadP.DefaultCellStyle = DataGridViewCellStyle6
        Me.CantidadP.HeaderText = "Cantidad"
        Me.CantidadP.MinimumWidth = 6
        Me.CantidadP.Name = "CantidadP"
        Me.CantidadP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CantidadP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CantidadP.Width = 97
        '
        'Peso
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N2"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.Peso.DefaultCellStyle = DataGridViewCellStyle7
        Me.Peso.HeaderText = "Peso"
        Me.Peso.MinimumWidth = 6
        Me.Peso.Name = "Peso"
        Me.Peso.ReadOnly = True
        Me.Peso.Width = 96
        '
        'CostoOC
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N6"
        DataGridViewCellStyle8.NullValue = Nothing
        Me.CostoOC.DefaultCellStyle = DataGridViewCellStyle8
        Me.CostoOC.HeaderText = "Costo OC"
        Me.CostoOC.MinimumWidth = 6
        Me.CostoOC.Name = "CostoOC"
        Me.CostoOC.ReadOnly = True
        Me.CostoOC.Width = 97
        '
        'CostoP
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N6"
        DataGridViewCellStyle9.NullValue = Nothing
        Me.CostoP.DefaultCellStyle = DataGridViewCellStyle9
        Me.CostoP.HeaderText = "Costo Real"
        Me.CostoP.MinimumWidth = 6
        Me.CostoP.Name = "CostoP"
        Me.CostoP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CostoP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CostoP.Width = 97
        '
        'TotalP
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "N2"
        DataGridViewCellStyle10.NullValue = Nothing
        Me.TotalP.DefaultCellStyle = DataGridViewCellStyle10
        Me.TotalP.HeaderText = "Total"
        Me.TotalP.MinimumWidth = 6
        Me.TotalP.Name = "TotalP"
        Me.TotalP.ReadOnly = True
        Me.TotalP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TotalP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TotalP.Width = 97
        '
        'IdProductoP
        '
        Me.IdProductoP.HeaderText = "IdProducto"
        Me.IdProductoP.MinimumWidth = 6
        Me.IdProductoP.Name = "IdProductoP"
        Me.IdProductoP.ReadOnly = True
        Me.IdProductoP.Visible = False
        Me.IdProductoP.Width = 125
        '
        'FechaVencimiento
        '
        Me.FechaVencimiento.HeaderText = "Fecha Vencimiento"
        Me.FechaVencimiento.MinimumWidth = 6
        Me.FechaVencimiento.Name = "FechaVencimiento"
        Me.FechaVencimiento.Width = 97
        '
        'Estado
        '
        Me.Estado.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.Estado.HeaderText = "Estado"
        Me.Estado.MinimumWidth = 6
        Me.Estado.Name = "Estado"
        Me.Estado.Width = 96
        '
        'Lote
        '
        Me.Lote.HeaderText = "Lote"
        Me.Lote.MinimumWidth = 6
        Me.Lote.Name = "Lote"
        Me.Lote.Width = 97
        '
        'MotivoDevolucion
        '
        Me.MotivoDevolucion.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.MotivoDevolucion.HeaderText = "MotivoDevolucion"
        Me.MotivoDevolucion.MinimumWidth = 6
        Me.MotivoDevolucion.Name = "MotivoDevolucion"
        Me.MotivoDevolucion.Visible = False
        Me.MotivoDevolucion.Width = 125
        '
        'IsNewR
        '
        Me.IsNewR.HeaderText = "IsNewR"
        Me.IsNewR.MinimumWidth = 6
        Me.IsNewR.Name = "IsNewR"
        Me.IsNewR.ReadOnly = True
        Me.IsNewR.Visible = False
        Me.IsNewR.Width = 125
        '
        'IdRecepcionEnc
        '
        Me.IdRecepcionEnc.HeaderText = "IdRecepcionEnc"
        Me.IdRecepcionEnc.MinimumWidth = 6
        Me.IdRecepcionEnc.Name = "IdRecepcionEnc"
        Me.IdRecepcionEnc.ReadOnly = True
        Me.IdRecepcionEnc.Visible = False
        Me.IdRecepcionEnc.Width = 125
        '
        'IdRecepcionDet
        '
        Me.IdRecepcionDet.HeaderText = "IdRecepcionDet"
        Me.IdRecepcionDet.MinimumWidth = 6
        Me.IdRecepcionDet.Name = "IdRecepcionDet"
        Me.IdRecepcionDet.ReadOnly = True
        Me.IdRecepcionDet.Visible = False
        Me.IdRecepcionDet.Width = 125
        '
        'Observacion
        '
        Me.Observacion.HeaderText = "Observación"
        Me.Observacion.MinimumWidth = 6
        Me.Observacion.Name = "Observacion"
        Me.Observacion.Width = 97
        '
        'IdUbicacionDefecto
        '
        Me.IdUbicacionDefecto.HeaderText = "IdUbicacionDefecto"
        Me.IdUbicacionDefecto.MinimumWidth = 6
        Me.IdUbicacionDefecto.Name = "IdUbicacionDefecto"
        Me.IdUbicacionDefecto.ReadOnly = True
        Me.IdUbicacionDefecto.Visible = False
        Me.IdUbicacionDefecto.Width = 125
        '
        'ControlVencimiento
        '
        Me.ControlVencimiento.HeaderText = "ControlVencimiento"
        Me.ControlVencimiento.MinimumWidth = 6
        Me.ControlVencimiento.Name = "ControlVencimiento"
        Me.ControlVencimiento.ReadOnly = True
        Me.ControlVencimiento.Visible = False
        Me.ControlVencimiento.Width = 125
        '
        'KeyP
        '
        Me.KeyP.HeaderText = "Key"
        Me.KeyP.MinimumWidth = 6
        Me.KeyP.Name = "KeyP"
        Me.KeyP.ReadOnly = True
        Me.KeyP.Visible = False
        Me.KeyP.Width = 125
        '
        'PesoPresentacion
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "N2"
        DataGridViewCellStyle11.NullValue = Nothing
        Me.PesoPresentacion.DefaultCellStyle = DataGridViewCellStyle11
        Me.PesoPresentacion.HeaderText = "PesoPresentacion"
        Me.PesoPresentacion.MinimumWidth = 6
        Me.PesoPresentacion.Name = "PesoPresentacion"
        Me.PesoPresentacion.ReadOnly = True
        Me.PesoPresentacion.Visible = False
        Me.PesoPresentacion.Width = 125
        '
        'ControlPeso
        '
        Me.ControlPeso.HeaderText = "ControlPeso"
        Me.ControlPeso.MinimumWidth = 6
        Me.ControlPeso.Name = "ControlPeso"
        Me.ControlPeso.ReadOnly = True
        Me.ControlPeso.Visible = False
        Me.ControlPeso.Width = 125
        '
        'Factor2
        '
        Me.Factor2.HeaderText = "Factor2"
        Me.Factor2.MinimumWidth = 6
        Me.Factor2.Name = "Factor2"
        Me.Factor2.ReadOnly = True
        Me.Factor2.Visible = False
        Me.Factor2.Width = 125
        '
        'PesoUnitario
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.Format = "N2"
        DataGridViewCellStyle12.NullValue = Nothing
        Me.PesoUnitario.DefaultCellStyle = DataGridViewCellStyle12
        Me.PesoUnitario.HeaderText = "PesoUnitario"
        Me.PesoUnitario.MinimumWidth = 6
        Me.PesoUnitario.Name = "PesoUnitario"
        Me.PesoUnitario.ReadOnly = True
        Me.PesoUnitario.Visible = False
        Me.PesoUnitario.Width = 125
        '
        'Atributo_Variante_1
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Atributo_Variante_1.DefaultCellStyle = DataGridViewCellStyle13
        Me.Atributo_Variante_1.HeaderText = "Atributo_Variante_1"
        Me.Atributo_Variante_1.MinimumWidth = 6
        Me.Atributo_Variante_1.Name = "Atributo_Variante_1"
        Me.Atributo_Variante_1.ReadOnly = True
        Me.Atributo_Variante_1.Visible = False
        Me.Atributo_Variante_1.Width = 125
        '
        'lic_plate
        '
        Me.lic_plate.HeaderText = "Lic_Plate"
        Me.lic_plate.MinimumWidth = 6
        Me.lic_plate.Name = "lic_plate"
        Me.lic_plate.ReadOnly = True
        Me.lic_plate.Width = 150
        '
        'IdOrdenCompraEnc
        '
        Me.IdOrdenCompraEnc.HeaderText = "IdOrdenCompraEnc"
        Me.IdOrdenCompraEnc.MinimumWidth = 6
        Me.IdOrdenCompraEnc.Name = "IdOrdenCompraEnc"
        Me.IdOrdenCompraEnc.ReadOnly = True
        Me.IdOrdenCompraEnc.Visible = False
        Me.IdOrdenCompraEnc.Width = 125
        '
        'IdOrdenCompraDet
        '
        Me.IdOrdenCompraDet.HeaderText = "IdOrdenCompraDet"
        Me.IdOrdenCompraDet.MinimumWidth = 6
        Me.IdOrdenCompraDet.Name = "IdOrdenCompraDet"
        Me.IdOrdenCompraDet.ReadOnly = True
        Me.IdOrdenCompraDet.Visible = False
        Me.IdOrdenCompraDet.Width = 125
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdAgregarProducto, Me.cmdVerParametros, Me.cmdEliminarFila, Me.lblStatus})
        Me.ToolStrip1.Location = New System.Drawing.Point(2, 23)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1404, 27)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip4"
        '
        'cmdAgregarProducto
        '
        Me.cmdAgregarProducto.Image = CType(resources.GetObject("cmdAgregarProducto.Image"), System.Drawing.Image)
        Me.cmdAgregarProducto.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAgregarProducto.Name = "cmdAgregarProducto"
        Me.cmdAgregarProducto.Size = New System.Drawing.Size(125, 24)
        Me.cmdAgregarProducto.Text = "Agregar Producto"
        '
        'cmdVerParametros
        '
        Me.cmdVerParametros.Image = CType(resources.GetObject("cmdVerParametros.Image"), System.Drawing.Image)
        Me.cmdVerParametros.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdVerParametros.Name = "cmdVerParametros"
        Me.cmdVerParametros.Size = New System.Drawing.Size(110, 24)
        Me.cmdVerParametros.Text = "Ver parámetros"
        '
        'cmdEliminarFila
        '
        Me.cmdEliminarFila.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEliminarFila.Image = CType(resources.GetObject("cmdEliminarFila.Image"), System.Drawing.Image)
        Me.cmdEliminarFila.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdEliminarFila.Name = "cmdEliminarFila"
        Me.cmdEliminarFila.Size = New System.Drawing.Size(96, 24)
        Me.cmdEliminarFila.Text = "Eliminar Fila"
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.Firebrick
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(12, 24)
        Me.lblStatus.Text = "-"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblPesoR)
        Me.Panel1.Controls.Add(Me.lblTotalR)
        Me.Panel1.Controls.Add(Me.lblCantidadR)
        Me.Panel1.Controls.Add(Me.lblCostoR)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(2, 470)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1404, 49)
        Me.Panel1.TabIndex = 2
        '
        'lblPesoR
        '
        Me.lblPesoR.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPesoR.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblPesoR.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPesoR.Location = New System.Drawing.Point(907, 7)
        Me.lblPesoR.Name = "lblPesoR"
        Me.lblPesoR.Size = New System.Drawing.Size(160, 13)
        Me.lblPesoR.TabIndex = 1
        Me.lblPesoR.Text = "0000000000.00"
        Me.lblPesoR.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalR
        '
        Me.lblTotalR.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalR.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTotalR.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalR.Location = New System.Drawing.Point(1241, 7)
        Me.lblTotalR.Name = "lblTotalR"
        Me.lblTotalR.Size = New System.Drawing.Size(160, 13)
        Me.lblTotalR.TabIndex = 3
        Me.lblTotalR.Text = "0000000000.00"
        Me.lblTotalR.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCantidadR
        '
        Me.lblCantidadR.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCantidadR.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblCantidadR.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCantidadR.Location = New System.Drawing.Point(728, 7)
        Me.lblCantidadR.Name = "lblCantidadR"
        Me.lblCantidadR.Size = New System.Drawing.Size(160, 13)
        Me.lblCantidadR.TabIndex = 0
        Me.lblCantidadR.Text = "0000000000.00"
        Me.lblCantidadR.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCostoR
        '
        Me.lblCostoR.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCostoR.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblCostoR.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostoR.Location = New System.Drawing.Point(1074, 7)
        Me.lblCostoR.Name = "lblCostoR"
        Me.lblCostoR.Size = New System.Drawing.Size(160, 13)
        Me.lblCostoR.TabIndex = 2
        Me.lblCostoR.Text = "0000000000.00"
        Me.lblCostoR.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GrpImagen
        '
        Me.GrpImagen.Controls.Add(Me.GroupControl4)
        Me.GrpImagen.Controls.Add(Me.Panel3)
        Me.GrpImagen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpImagen.Location = New System.Drawing.Point(0, 0)
        Me.GrpImagen.Name = "GrpImagen"
        Me.GrpImagen.Size = New System.Drawing.Size(1408, 521)
        Me.GrpImagen.TabIndex = 0
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.GrdImagen)
        Me.GroupControl4.Controls.Add(Me.ToolStrip)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl4.Location = New System.Drawing.Point(2, 23)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(507, 496)
        Me.GroupControl4.TabIndex = 0
        Me.GroupControl4.Text = "Lista de Imágenes"
        '
        'GrdImagen
        '
        Me.GrdImagen.Dock = System.Windows.Forms.DockStyle.Fill
        GridLevelNode1.RelationName = "Level1"
        Me.GrdImagen.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.GrdImagen.Location = New System.Drawing.Point(2, 50)
        Me.GrdImagen.MainView = Me.GridViewImg
        Me.GrdImagen.MenuManager = Me.RibbonControl
        Me.GrdImagen.Name = "GrdImagen"
        Me.GrdImagen.Size = New System.Drawing.Size(503, 444)
        Me.GrdImagen.TabIndex = 1
        Me.GrdImagen.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewImg})
        '
        'GridViewImg
        '
        Me.GridViewImg.DetailHeight = 284
        Me.GridViewImg.GridControl = Me.GrdImagen
        Me.GridViewImg.Name = "GridViewImg"
        Me.GridViewImg.OptionsBehavior.Editable = False
        Me.GridViewImg.OptionsEditForm.PopupEditFormWidth = 686
        Me.GridViewImg.OptionsFind.AlwaysVisible = True
        Me.GridViewImg.OptionsView.ShowGroupPanel = False
        '
        'ToolStrip
        '
        Me.ToolStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdAdd, Me.cmdDelete})
        Me.ToolStrip.Location = New System.Drawing.Point(2, 23)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip.Size = New System.Drawing.Size(503, 27)
        Me.ToolStrip.TabIndex = 0
        Me.ToolStrip.Text = "ToolStrip4"
        '
        'cmdAdd
        '
        Me.cmdAdd.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(23, 24)
        '
        'cmdDelete
        '
        Me.cmdDelete.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(24, 24)
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.PicImg)
        Me.Panel3.Controls.Add(Me.Label23)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(509, 23)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(897, 496)
        Me.Panel3.TabIndex = 94
        '
        'PicImg
        '
        Me.PicImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PicImg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PicImg.Location = New System.Drawing.Point(0, 22)
        Me.PicImg.Name = "PicImg"
        Me.PicImg.Size = New System.Drawing.Size(897, 474)
        Me.PicImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PicImg.TabIndex = 93
        Me.PicImg.TabStop = False
        Me.PicImg.Visible = False
        '
        'Label23
        '
        Me.Label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(897, 22)
        Me.Label23.TabIndex = 1
        Me.Label23.Text = "Nombre Imagen"
        '
        'GrpOperadorBodega
        '
        Me.GrpOperadorBodega.Controls.Add(Me.GroupControl13)
        Me.GrpOperadorBodega.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpOperadorBodega.Location = New System.Drawing.Point(0, 0)
        Me.GrpOperadorBodega.Name = "GrpOperadorBodega"
        Me.GrpOperadorBodega.Size = New System.Drawing.Size(1408, 521)
        Me.GrpOperadorBodega.TabIndex = 0
        Me.GrpOperadorBodega.Tag = ""
        '
        'GroupControl13
        '
        Me.GroupControl13.Controls.Add(Me.DGridOperadores)
        Me.GroupControl13.Controls.Add(Me.ToolStripPR)
        Me.GroupControl13.Controls.Add(Me.RibbonStatusBar1)
        Me.GroupControl13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl13.Location = New System.Drawing.Point(2, 23)
        Me.GroupControl13.Name = "GroupControl13"
        Me.GroupControl13.Size = New System.Drawing.Size(1404, 496)
        Me.GroupControl13.TabIndex = 0
        Me.GroupControl13.Text = "Selección de Operador"
        '
        'DGridOperadores
        '
        Me.DGridOperadores.DataSource = Me.DataBindingSource
        Me.DGridOperadores.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGridOperadores.Location = New System.Drawing.Point(2, 50)
        Me.DGridOperadores.MainView = Me.GrdOperadorBobega
        Me.DGridOperadores.Name = "DGridOperadores"
        Me.DGridOperadores.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2})
        Me.DGridOperadores.Size = New System.Drawing.Size(1400, 417)
        Me.DGridOperadores.TabIndex = 0
        Me.DGridOperadores.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GrdOperadorBobega})
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
        'GrdOperadorBobega
        '
        Me.GrdOperadorBobega.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.GrdOperadorBobega.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSelección, Me.colIdOperadorRec, Me.colIdOperadorBodega, Me.colOperador, Me.colcolUsaHH, Me.colFoto})
        Me.GrdOperadorBobega.DetailHeight = 284
        Me.GrdOperadorBobega.GridControl = Me.DGridOperadores
        Me.GrdOperadorBobega.Name = "GrdOperadorBobega"
        Me.GrdOperadorBobega.OptionsEditForm.PopupEditFormWidth = 686
        Me.GrdOperadorBobega.OptionsFind.AlwaysVisible = True
        Me.GrdOperadorBobega.OptionsView.ShowAutoFilterRow = True
        '
        'colSelección
        '
        Me.colSelección.FieldName = "Selección"
        Me.colSelección.MinWidth = 21
        Me.colSelección.Name = "colSelección"
        Me.colSelección.Visible = True
        Me.colSelección.VisibleIndex = 0
        Me.colSelección.Width = 81
        '
        'colIdOperadorRec
        '
        Me.colIdOperadorRec.FieldName = "IdOperadorRec"
        Me.colIdOperadorRec.MinWidth = 21
        Me.colIdOperadorRec.Name = "colIdOperadorRec"
        Me.colIdOperadorRec.OptionsColumn.ReadOnly = True
        Me.colIdOperadorRec.Width = 81
        '
        'colIdOperadorBodega
        '
        Me.colIdOperadorBodega.FieldName = "IdOperadorBodega"
        Me.colIdOperadorBodega.MinWidth = 21
        Me.colIdOperadorBodega.Name = "colIdOperadorBodega"
        Me.colIdOperadorBodega.OptionsColumn.ReadOnly = True
        Me.colIdOperadorBodega.Width = 81
        '
        'colOperador
        '
        Me.colOperador.FieldName = "Operador"
        Me.colOperador.MinWidth = 21
        Me.colOperador.Name = "colOperador"
        Me.colOperador.OptionsColumn.ReadOnly = True
        Me.colOperador.Visible = True
        Me.colOperador.VisibleIndex = 1
        Me.colOperador.Width = 81
        '
        'colcolUsaHH
        '
        Me.colcolUsaHH.CustomizationCaption = "Usa HH"
        Me.colcolUsaHH.FieldName = "colUsaHH"
        Me.colcolUsaHH.MinWidth = 21
        Me.colcolUsaHH.Name = "colcolUsaHH"
        Me.colcolUsaHH.OptionsColumn.ReadOnly = True
        Me.colcolUsaHH.Visible = True
        Me.colcolUsaHH.VisibleIndex = 2
        Me.colcolUsaHH.Width = 81
        '
        'colFoto
        '
        Me.colFoto.FieldName = "Foto"
        Me.colFoto.MinWidth = 21
        Me.colFoto.Name = "colFoto"
        Me.colFoto.OptionsColumn.ReadOnly = True
        Me.colFoto.Visible = True
        Me.colFoto.VisibleIndex = 3
        Me.colFoto.Width = 81
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'ToolStripPR
        '
        Me.ToolStripPR.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStripPR.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdMarcarTodosOperador, Me.cmdDesmarcarTodosOperador})
        Me.ToolStripPR.Location = New System.Drawing.Point(2, 23)
        Me.ToolStripPR.Name = "ToolStripPR"
        Me.ToolStripPR.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStripPR.Size = New System.Drawing.Size(1400, 27)
        Me.ToolStripPR.TabIndex = 3
        Me.ToolStripPR.Text = "ToolStrip2"
        '
        'cmdMarcarTodosOperador
        '
        Me.cmdMarcarTodosOperador.Image = CType(resources.GetObject("cmdMarcarTodosOperador.Image"), System.Drawing.Image)
        Me.cmdMarcarTodosOperador.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdMarcarTodosOperador.Name = "cmdMarcarTodosOperador"
        Me.cmdMarcarTodosOperador.Size = New System.Drawing.Size(63, 24)
        Me.cmdMarcarTodosOperador.Text = "Todos"
        '
        'cmdDesmarcarTodosOperador
        '
        Me.cmdDesmarcarTodosOperador.Image = CType(resources.GetObject("cmdDesmarcarTodosOperador.Image"), System.Drawing.Image)
        Me.cmdDesmarcarTodosOperador.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDesmarcarTodosOperador.Name = "cmdDesmarcarTodosOperador"
        Me.cmdDesmarcarTodosOperador.Size = New System.Drawing.Size(78, 24)
        Me.cmdDesmarcarTodosOperador.Text = "Ninguno"
        '
        'Bar2
        '
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'dkRecepcion
        '
        Me.dkRecepcion.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkRecepcion.Form = Me
        Me.dkRecepcion.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 679)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1332, 14)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("3bb47686-189c-4fb8-a839-198f9210017f")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 602)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 99)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1410, 101)
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
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 28)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1403, 70)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'xtrRecepcion
        '
        Me.xtrRecepcion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtrRecepcion.Location = New System.Drawing.Point(0, 158)
        Me.xtrRecepcion.Name = "xtrRecepcion"
        Me.xtrRecepcion.SelectedTabPage = Me.tabDatosRec
        Me.xtrRecepcion.Size = New System.Drawing.Size(1332, 521)
        Me.xtrRecepcion.TabIndex = 0
        Me.xtrRecepcion.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabDatosRec, Me.tabDetalleOC, Me.tabDetRec, Me.tabImagenes, Me.tabDetOp, Me.tabDetalleRecepcion2})
        '
        'tabDatosRec
        '
        Me.tabDatosRec.Controls.Add(Me.SplitContainer2)
        Me.tabDatosRec.Controls.Add(Me.SplitContainer1)
        Me.tabDatosRec.Name = "tabDatosRec"
        Me.tabDatosRec.Size = New System.Drawing.Size(1330, 496)
        Me.tabDatosRec.Text = "Datos Recepción"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.GrpObservacion)
        Me.SplitContainer2.Panel1.Controls.Add(Me.GrpAsignacionTransaccion)
        Me.SplitContainer2.Panel1.Controls.Add(Me.GrpTransaccion)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GrpTarea)
        Me.SplitContainer2.Panel2.Controls.Add(Me.GrpParametrosIngreso)
        Me.SplitContainer2.Panel2.Controls.Add(Me.GrpTIpoTransaccion)
        Me.SplitContainer2.Size = New System.Drawing.Size(727, 397)
        Me.SplitContainer2.SplitterDistance = 318
        Me.SplitContainer2.SplitterWidth = 3
        Me.SplitContainer2.TabIndex = 6
        '
        'GrpTIpoTransaccion
        '
        Me.GrpTIpoTransaccion.Appearance.Options.UseBackColor = True
        Me.GrpTIpoTransaccion.Appearance.Options.UseBorderColor = True
        Me.GrpTIpoTransaccion.AppearanceCaption.BackColor = System.Drawing.Color.Brown
        Me.GrpTIpoTransaccion.AppearanceCaption.Options.UseBackColor = True
        Me.GrpTIpoTransaccion.AppearanceCaption.Options.UseBorderColor = True
        Me.GrpTIpoTransaccion.Controls.Add(Me.txtDescripcionTR)
        Me.GrpTIpoTransaccion.Controls.Add(Me.lnkTipoT)
        Me.GrpTIpoTransaccion.Controls.Add(Me.txtIdTipoTR)
        Me.GrpTIpoTransaccion.Dock = System.Windows.Forms.DockStyle.Top
        Me.GrpTIpoTransaccion.Location = New System.Drawing.Point(0, 0)
        Me.GrpTIpoTransaccion.Name = "GrpTIpoTransaccion"
        Me.GrpTIpoTransaccion.Size = New System.Drawing.Size(406, 62)
        Me.GrpTIpoTransaccion.TabIndex = 1
        Me.GrpTIpoTransaccion.Text = "Tipo Transacción"
        '
        'txtDescripcionTR
        '
        Me.txtDescripcionTR.Location = New System.Drawing.Point(184, 34)
        Me.txtDescripcionTR.MenuManager = Me.RibbonControl
        Me.txtDescripcionTR.Name = "txtDescripcionTR"
        Me.txtDescripcionTR.Properties.ReadOnly = True
        Me.txtDescripcionTR.Size = New System.Drawing.Size(219, 20)
        Me.txtDescripcionTR.TabIndex = 2
        '
        'lnkTipoT
        '
        Me.lnkTipoT.AutoSize = True
        Me.lnkTipoT.Location = New System.Drawing.Point(9, 37)
        Me.lnkTipoT.Name = "lnkTipoT"
        Me.lnkTipoT.Size = New System.Drawing.Size(87, 13)
        Me.lnkTipoT.TabIndex = 0
        Me.lnkTipoT.TabStop = True
        Me.lnkTipoT.Text = "Tipo Transacción"
        '
        'txtIdTipoTR
        '
        Me.txtIdTipoTR.Location = New System.Drawing.Point(108, 34)
        Me.txtIdTipoTR.MenuManager = Me.RibbonControl
        Me.txtIdTipoTR.Name = "txtIdTipoTR"
        Me.txtIdTipoTR.Size = New System.Drawing.Size(70, 20)
        Me.txtIdTipoTR.TabIndex = 1
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Right
        Me.SplitContainer1.Location = New System.Drawing.Point(643, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupControl3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GrpFactura)
        Me.SplitContainer1.Size = New System.Drawing.Size(421, 397)
        Me.SplitContainer1.SplitterDistance = 192
        Me.SplitContainer1.SplitterWidth = 3
        Me.SplitContainer1.TabIndex = 7
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.grpDatosFiscalSAT)
        Me.GroupControl3.Controls.Add(Me.GrpPiloto)
        Me.GroupControl3.Controls.Add(Me.GrpVehiculo)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(421, 192)
        Me.GroupControl3.TabIndex = 6
        Me.GroupControl3.Text = "Datos de Transporte"
        '
        'grpDatosFiscalSAT
        '
        Me.grpDatosFiscalSAT.Controls.Add(Me.txtNoContenedor)
        Me.grpDatosFiscalSAT.Controls.Add(lblNoGuia)
        Me.grpDatosFiscalSAT.Controls.Add(Me.txtCartaCupo)
        Me.grpDatosFiscalSAT.Controls.Add(lblCartaCupo)
        Me.grpDatosFiscalSAT.Location = New System.Drawing.Point(0, 186)
        Me.grpDatosFiscalSAT.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.grpDatosFiscalSAT.Name = "grpDatosFiscalSAT"
        Me.grpDatosFiscalSAT.Size = New System.Drawing.Size(421, 71)
        Me.grpDatosFiscalSAT.TabIndex = 6
        '
        'txtNoContenedor
        '
        Me.txtNoContenedor.Location = New System.Drawing.Point(189, 34)
        Me.txtNoContenedor.MenuManager = Me.RibbonControl
        Me.txtNoContenedor.Name = "txtNoContenedor"
        Me.txtNoContenedor.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.txtNoContenedor.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoContenedor.Properties.Appearance.Options.UseBackColor = True
        Me.txtNoContenedor.Properties.Appearance.Options.UseFont = True
        Me.txtNoContenedor.Properties.MaskSettings.Set("mask", "n0")
        Me.txtNoContenedor.Size = New System.Drawing.Size(160, 18)
        Me.txtNoContenedor.TabIndex = 3
        '
        'txtCartaCupo
        '
        Me.txtCartaCupo.Location = New System.Drawing.Point(33, 34)
        Me.txtCartaCupo.MenuManager = Me.RibbonControl
        Me.txtCartaCupo.Name = "txtCartaCupo"
        Me.txtCartaCupo.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.txtCartaCupo.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCartaCupo.Properties.Appearance.Options.UseBackColor = True
        Me.txtCartaCupo.Properties.Appearance.Options.UseFont = True
        Me.txtCartaCupo.Properties.MaskSettings.Set("mask", "n0")
        Me.txtCartaCupo.Size = New System.Drawing.Size(148, 18)
        Me.txtCartaCupo.TabIndex = 4
        '
        'GrpPiloto
        '
        Me.GrpPiloto.Controls.Add(Me.lnkPiloto)
        Me.GrpPiloto.Controls.Add(Me.txtNombrePiloto)
        Me.GrpPiloto.Controls.Add(Me.txtIdPiloto)
        Me.GrpPiloto.Location = New System.Drawing.Point(21, 39)
        Me.GrpPiloto.Name = "GrpPiloto"
        Me.GrpPiloto.Size = New System.Drawing.Size(386, 61)
        Me.GrpPiloto.TabIndex = 0
        Me.GrpPiloto.Text = "Piloto"
        '
        'lnkPiloto
        '
        Me.lnkPiloto.AutoSize = True
        Me.lnkPiloto.Location = New System.Drawing.Point(9, 31)
        Me.lnkPiloto.Name = "lnkPiloto"
        Me.lnkPiloto.Size = New System.Drawing.Size(33, 13)
        Me.lnkPiloto.TabIndex = 0
        Me.lnkPiloto.TabStop = True
        Me.lnkPiloto.Text = "Piloto"
        '
        'txtNombrePiloto
        '
        Me.txtNombrePiloto.Location = New System.Drawing.Point(157, 28)
        Me.txtNombrePiloto.MenuManager = Me.RibbonControl
        Me.txtNombrePiloto.Name = "txtNombrePiloto"
        Me.txtNombrePiloto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtNombrePiloto.Properties.Mask.EditMask = "n0"
        Me.txtNombrePiloto.Properties.ReadOnly = True
        Me.txtNombrePiloto.Size = New System.Drawing.Size(220, 20)
        Me.txtNombrePiloto.TabIndex = 2
        '
        'txtIdPiloto
        '
        Me.txtIdPiloto.Location = New System.Drawing.Point(81, 28)
        Me.txtIdPiloto.MenuManager = Me.RibbonControl
        Me.txtIdPiloto.Name = "txtIdPiloto"
        Me.txtIdPiloto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtIdPiloto.Size = New System.Drawing.Size(70, 20)
        Me.txtIdPiloto.TabIndex = 1
        '
        'GrpVehiculo
        '
        Me.GrpVehiculo.Controls.Add(Me.txtNombreVehiculo)
        Me.GrpVehiculo.Controls.Add(Me.lnkVehiculo)
        Me.GrpVehiculo.Controls.Add(Me.txtIdVehiculo)
        Me.GrpVehiculo.Location = New System.Drawing.Point(21, 105)
        Me.GrpVehiculo.Name = "GrpVehiculo"
        Me.GrpVehiculo.Size = New System.Drawing.Size(386, 56)
        Me.GrpVehiculo.TabIndex = 1
        Me.GrpVehiculo.Text = "Vehículo"
        '
        'txtNombreVehiculo
        '
        Me.txtNombreVehiculo.Location = New System.Drawing.Point(157, 28)
        Me.txtNombreVehiculo.MenuManager = Me.RibbonControl
        Me.txtNombreVehiculo.Name = "txtNombreVehiculo"
        Me.txtNombreVehiculo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtNombreVehiculo.Properties.Mask.EditMask = "n0"
        Me.txtNombreVehiculo.Properties.ReadOnly = True
        Me.txtNombreVehiculo.Size = New System.Drawing.Size(220, 20)
        Me.txtNombreVehiculo.TabIndex = 2
        '
        'lnkVehiculo
        '
        Me.lnkVehiculo.AutoSize = True
        Me.lnkVehiculo.Location = New System.Drawing.Point(9, 31)
        Me.lnkVehiculo.Name = "lnkVehiculo"
        Me.lnkVehiculo.Size = New System.Drawing.Size(46, 13)
        Me.lnkVehiculo.TabIndex = 0
        Me.lnkVehiculo.TabStop = True
        Me.lnkVehiculo.Text = "Vehículo"
        '
        'txtIdVehiculo
        '
        Me.txtIdVehiculo.Location = New System.Drawing.Point(81, 28)
        Me.txtIdVehiculo.MenuManager = Me.RibbonControl
        Me.txtIdVehiculo.Name = "txtIdVehiculo"
        Me.txtIdVehiculo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtIdVehiculo.Size = New System.Drawing.Size(70, 20)
        Me.txtIdVehiculo.TabIndex = 1
        '
        'tabDetalleOC
        '
        Me.tabDetalleOC.Controls.Add(Me.GrpDetalle)
        Me.tabDetalleOC.Name = "tabDetalleOC"
        Me.tabDetalleOC.Size = New System.Drawing.Size(1408, 521)
        Me.tabDetalleOC.Text = "Detalle documento de Ingreso"
        '
        'tabDetRec
        '
        Me.tabDetRec.Controls.Add(Me.GrpDetalleRecepcion)
        Me.tabDetRec.Name = "tabDetRec"
        Me.tabDetRec.Size = New System.Drawing.Size(1408, 521)
        Me.tabDetRec.Text = "Detalle Recepción"
        '
        'tabImagenes
        '
        Me.tabImagenes.Controls.Add(Me.GrpImagen)
        Me.tabImagenes.Name = "tabImagenes"
        Me.tabImagenes.Size = New System.Drawing.Size(1408, 521)
        Me.tabImagenes.Text = "Imagenes"
        '
        'tabDetOp
        '
        Me.tabDetOp.Controls.Add(Me.GrpOperadorBodega)
        Me.tabDetOp.Name = "tabDetOp"
        Me.tabDetOp.Size = New System.Drawing.Size(1408, 521)
        Me.tabDetOp.Text = "Detalle de Operadores"
        '
        'tabDetalleRecepcion2
        '
        Me.tabDetalleRecepcion2.Controls.Add(Me.GroupControl1)
        Me.tabDetalleRecepcion2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tabDetalleRecepcion2.Name = "tabDetalleRecepcion2"
        Me.tabDetalleRecepcion2.Size = New System.Drawing.Size(1408, 521)
        Me.tabDetalleRecepcion2.Text = "Detalle de Recepción"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.DgridDetalleRec2)
        Me.GroupControl1.Controls.Add(Me.ToolStrip2)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1126, 417)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "Lista de Productos"
        '
        'DgridDetalleRec2
        '
        Me.DgridDetalleRec2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridDetalleRec2.Location = New System.Drawing.Point(2, 50)
        Me.DgridDetalleRec2.MainView = Me.gvDetalleRec2
        Me.DgridDetalleRec2.Name = "DgridDetalleRec2"
        Me.DgridDetalleRec2.Size = New System.Drawing.Size(1122, 365)
        Me.DgridDetalleRec2.TabIndex = 19
        Me.DgridDetalleRec2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvDetalleRec2})
        '
        'gvDetalleRec2
        '
        Me.gvDetalleRec2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvDetalleRec2.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvDetalleRec2.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvDetalleRec2.Appearance.Row.Options.UseFont = True
        Me.gvDetalleRec2.DetailHeight = 355
        Me.gvDetalleRec2.GridControl = Me.DgridDetalleRec2
        Me.gvDetalleRec2.Name = "gvDetalleRec2"
        Me.gvDetalleRec2.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvDetalleRec2.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace
        Me.gvDetalleRec2.OptionsCustomization.AllowSort = False
        Me.gvDetalleRec2.OptionsEditForm.ActionOnModifiedRowChange = DevExpress.XtraGrid.Views.Grid.EditFormModifiedAction.Save
        Me.gvDetalleRec2.OptionsEditForm.PopupEditFormWidth = 686
        Me.gvDetalleRec2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.gvDetalleRec2.OptionsView.ShowGroupPanel = False
        '
        'ToolStrip2
        '
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolEliminarFila})
        Me.ToolStrip2.Location = New System.Drawing.Point(2, 23)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip2.Size = New System.Drawing.Size(1122, 27)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip4"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(105, 24)
        Me.ToolStripButton1.Text = "Agregar Producto"
        '
        'ToolEliminarFila
        '
        Me.ToolEliminarFila.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolEliminarFila.Image = Global.TOMWMS.My.Resources.Resources.Delete_16
        Me.ToolEliminarFila.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolEliminarFila.Name = "ToolEliminarFila"
        Me.ToolEliminarFila.Size = New System.Drawing.Size(96, 24)
        Me.ToolEliminarFila.Text = "Eliminar Fila"
        '
        'tmrActualizarDatosRecepcion
        '
        Me.tmrActualizarDatosRecepcion.Interval = 3000
        '
        'frmRecepcion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1332, 717)
        Me.Controls.Add(Me.xtrRecepcion)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Name = "frmRecepcion"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Recepción"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.GrpTarea, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpTarea.ResumeLayout(False)
        CType(Me.GroupControl10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl10.ResumeLayout(False)
        Me.GroupControl10.PerformLayout()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl9.ResumeLayout(False)
        Me.GroupControl9.PerformLayout()
        CType(Me.dtmFechaTarea.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaTarea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpTransaccion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpTransaccion.ResumeLayout(False)
        Me.GrpTransaccion.PerformLayout()
        CType(Me.dtmFechaRecepcion.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaRecepcion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdOrdenCompra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpObservacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpObservacion.ResumeLayout(False)
        Me.GrpObservacion.PerformLayout()
        CType(Me.GrpAsignacionTransaccion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpAsignacionTransaccion.ResumeLayout(False)
        Me.GrpAsignacionTransaccion.PerformLayout()
        CType(Me.cmbMuelle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdEstadoDefectoRecepcion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpParametrosIngreso, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpParametrosIngreso.ResumeLayout(False)
        Me.GrpParametrosIngreso.PerformLayout()
        CType(Me.chkDI2REC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMostrarCantidadPI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoMarchamo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkHabilitaStock.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoDocumento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkParaPorCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMuestraPrecio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEscanearUbicacionRec.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRecepcionManual.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTomarFoto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpFactura, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpFactura.ResumeLayout(False)
        CType(Me.grdListaFactura, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpDetalle.ResumeLayout(False)
        CType(Me.DgridDetalleOC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetalleDocIngreso, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpDetalleRecepcion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpDetalleRecepcion.ResumeLayout(False)
        Me.GrpDetalleRecepcion.PerformLayout()
        CType(Me.DgridDetalleRec, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.GrpImagen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpImagen.ResumeLayout(False)
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.GrdImagen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewImg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.PicImg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpOperadorBodega, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpOperadorBodega.ResumeLayout(False)
        CType(Me.GroupControl13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl13.ResumeLayout(False)
        Me.GroupControl13.PerformLayout()
        CType(Me.DGridOperadores, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsOrdenCompraRecepcionOperador, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrdOperadorBobega, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripPR.ResumeLayout(False)
        Me.ToolStripPR.PerformLayout()
        CType(Me.dkRecepcion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.xtrRecepcion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtrRecepcion.ResumeLayout(False)
        Me.tabDatosRec.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.GrpTIpoTransaccion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpTIpoTransaccion.ResumeLayout(False)
        Me.GrpTIpoTransaccion.PerformLayout()
        CType(Me.txtDescripcionTR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdTipoTR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.grpDatosFiscalSAT.ResumeLayout(False)
        Me.grpDatosFiscalSAT.PerformLayout()
        CType(Me.txtNoContenedor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCartaCupo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpPiloto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpPiloto.ResumeLayout(False)
        Me.GrpPiloto.PerformLayout()
        CType(Me.txtNombrePiloto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdPiloto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpVehiculo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpVehiculo.ResumeLayout(False)
        Me.GrpVehiculo.PerformLayout()
        CType(Me.txtNombreVehiculo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdVehiculo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabDetalleOC.ResumeLayout(False)
        Me.tabDetRec.ResumeLayout(False)
        Me.tabImagenes.ResumeLayout(False)
        Me.tabDetOp.ResumeLayout(False)
        Me.tabDetalleRecepcion2.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.DgridDetalleRec2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetalleRec2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
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
    Friend WithEvents cmdGuardar As DevExpress.XtraBars.BarButtonItem
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
    Friend WithEvents lblC As System.Windows.Forms.Label
    Friend WithEvents chkEscanearUbicacionRec As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkTomarFoto As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lnk As System.Windows.Forms.LinkLabel
    Friend WithEvents txtIdOrdenCompra As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents SubImprimir As DevExpress.XtraBars.BarSubItem
    Friend WithEvents cmdPreIngreso As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdCostoArancel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents txtOC As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GrpParametrosIngreso As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrpAsignacionTransaccion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtNombreUbicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkUbicacion As System.Windows.Forms.LinkLabel
    Friend WithEvents txtIdUbicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GrpTransaccion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrpObservacion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtObservacion As System.Windows.Forms.TextBox
    Friend WithEvents chkMuestraPrecio As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkRecepcionManual As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GrpTarea As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl9 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtmHoraI As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupControl10 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtmHoraFhh As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtmHoraIhh As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtmHoraF As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtNoDocumento As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkParaPorCodigo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GrpImagen As DevExpress.XtraEditors.GroupControl
    Friend WithEvents PicImg As System.Windows.Forms.PictureBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrdImagen As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewImg As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents GrpDetalle As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrpDetalleRecepcion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblTotalR As System.Windows.Forms.Label
    Friend WithEvents lblCantidadR As System.Windows.Forms.Label
    Friend WithEvents lblCostoR As System.Windows.Forms.Label
    Friend WithEvents GrpOperadorBodega As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl13 As DevExpress.XtraEditors.GroupControl
    Private WithEvents DGridOperadores As DevExpress.XtraGrid.GridControl
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cmdFinalizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblEstado As System.Windows.Forms.Label
    Friend WithEvents cmdPrint As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdEliminarFila As System.Windows.Forms.ToolStripButton
    Friend WithEvents GrpFactura As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmdAgregar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmdEliminarFactura As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmdAbajo As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmdArriba As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grdListaFactura As DataGridView
    Friend WithEvents IdFacturaRecepcion As DataGridViewTextBoxColumn
    Friend WithEvents IdRecepcion As DataGridViewTextBoxColumn
    Friend WithEvents Orden As DataGridViewTextBoxColumn
    Friend WithEvents NoFactura As DataGridViewTextBoxColumn
    Friend WithEvents Obs As DataGridViewTextBoxColumn
    Friend WithEvents Completa As DataGridViewCheckBoxColumn
    Friend WithEvents DataBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsOrdenCompraRecepcionOperador As TOMWMS.DsOrdenCompraRecepcionOperador
    Friend WithEvents cmdAgregarProducto As ToolStripButton
    Friend WithEvents cmdVerParametros As ToolStripButton
    Friend WithEvents lblPesoR As Label
    Friend WithEvents dkRecepcion As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents xtrRecepcion As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tabDatosRec As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabDetalleOC As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabDetRec As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabImagenes As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabDetOp As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblMotivoAnulacion As Label
    Friend WithEvents lblDiagonal As Label
    Friend WithEvents lblId As Label
    Friend WithEvents dtmFechaRecepcion As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtmFechaTarea As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RibbonStatusBar1 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents GrpPiloto As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lnkPiloto As LinkLabel
    Friend WithEvents txtNombrePiloto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdPiloto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GrpVehiculo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtNombreVehiculo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkVehiculo As LinkLabel
    Friend WithEvents txtIdVehiculo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkHabilitaStock As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Label11 As Label
    Friend WithEvents txtNoMarchamo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblMarchamo As Label
    Friend WithEvents chkMostrarCantidadPI As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblMostrarCantidadPI As Label
    Friend WithEvents cmdActualizarDetalle As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ToolStripPR As ToolStrip
    Friend WithEvents cmdMarcarTodosOperador As ToolStripButton
    Friend WithEvents cmdDesmarcarTodosOperador As ToolStripButton
    Friend WithEvents Panel3 As Panel
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents GrpTIpoTransaccion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtDescripcionTR As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkTipoT As LinkLabel
    Friend WithEvents txtIdTipoTR As DevExpress.XtraEditors.TextEdit
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents DgridDetalleOC As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDetalleDocIngreso As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkDI2REC As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblDIToRec As Label
    Friend WithEvents lblStatus As ToolStripLabel
    Friend WithEvents txtNoContenedor As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tmrActualizarDatosRecepcion As Timer
    Friend WithEvents GrdOperadorBobega As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colSelección As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdOperadorRec As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdOperadorBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOperador As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colcolUsaHH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colFoto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtCartaCupo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents grpDatosFiscalSAT As Panel
    Friend WithEvents txtNombreEstado As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkEstadoPorDefecto As LinkLabel
    Friend WithEvents txtIdEstadoDefectoRecepcion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tabDetalleRecepcion2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents DgridDetalleRec2 As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDetalleRec2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ToolStrip2 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents ToolEliminarFila As ToolStripButton
    Friend WithEvents DgridDetalleRec As DataGridView
    Friend WithEvents cmdPrintLabels As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents No_Linea As DataGridViewTextBoxColumn
    Friend WithEvents CodigoP As DataGridViewTextBoxColumn
    Friend WithEvents ProductoP As DataGridViewTextBoxColumn
    Friend WithEvents PresentacionP As DataGridViewComboBoxColumn
    Friend WithEvents UnidadMedidaP As DataGridViewComboBoxColumn
    Friend WithEvents CantidadP As DataGridViewTextBoxColumn
    Friend WithEvents Peso As DataGridViewTextBoxColumn
    Friend WithEvents CostoOC As DataGridViewTextBoxColumn
    Friend WithEvents CostoP As DataGridViewTextBoxColumn
    Friend WithEvents TotalP As DataGridViewTextBoxColumn
    Friend WithEvents IdProductoP As DataGridViewTextBoxColumn
    Friend WithEvents FechaVencimiento As DataGridViewTextBoxColumn
    Friend WithEvents Estado As DataGridViewComboBoxColumn
    Friend WithEvents Lote As DataGridViewTextBoxColumn
    Friend WithEvents MotivoDevolucion As DataGridViewComboBoxColumn
    Friend WithEvents IsNewR As DataGridViewCheckBoxColumn
    Friend WithEvents IdRecepcionEnc As DataGridViewTextBoxColumn
    Friend WithEvents IdRecepcionDet As DataGridViewTextBoxColumn
    Friend WithEvents Observacion As DataGridViewTextBoxColumn
    Friend WithEvents IdUbicacionDefecto As DataGridViewTextBoxColumn
    Friend WithEvents ControlVencimiento As DataGridViewCheckBoxColumn
    Friend WithEvents KeyP As DataGridViewTextBoxColumn
    Friend WithEvents PesoPresentacion As DataGridViewTextBoxColumn
    Friend WithEvents ControlPeso As DataGridViewCheckBoxColumn
    Friend WithEvents Factor2 As DataGridViewTextBoxColumn
    Friend WithEvents PesoUnitario As DataGridViewTextBoxColumn
    Friend WithEvents Atributo_Variante_1 As DataGridViewTextBoxColumn
    Friend WithEvents lic_plate As DataGridViewTextBoxColumn
    Friend WithEvents IdOrdenCompraEnc As DataGridViewTextBoxColumn
    Friend WithEvents IdOrdenCompraDet As DataGridViewTextBoxColumn
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmbMuelle As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView12 As DevExpress.XtraGrid.Views.Grid.GridView
End Class
