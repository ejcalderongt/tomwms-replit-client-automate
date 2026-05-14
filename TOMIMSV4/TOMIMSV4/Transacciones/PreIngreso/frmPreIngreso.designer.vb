<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPreIngreso
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If DT IsNot Nothing Then
                DT.Dispose()
                DT = Nothing
            End If
            If txtCodigoProductoGrid IsNot Nothing Then
                txtCodigoProductoGrid.Dispose()
                txtCodigoProductoGrid = Nothing
            End If
            If gBeRecepcion IsNot Nothing Then
                gBeRecepcion.Dispose()
                gBeRecepcion = Nothing
            End If
            If gBeOrdenCompra IsNot Nothing Then
                gBeOrdenCompra.Dispose()
                gBeOrdenCompra = Nothing
            End If
            If pObjOC IsNot Nothing Then
                pObjOC.Dispose()
                pObjOC = Nothing
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
            If cPresentacion IsNot Nothing Then
                cPresentacion.Dispose()
                cPresentacion = Nothing
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
        Dim SplashScreenManager1 As DevExpress.XtraSplashScreen.SplashScreenManager = New DevExpress.XtraSplashScreen.SplashScreenManager(Me, Nothing, True, True)
        Dim Label37 As System.Windows.Forms.Label
        Dim Label38 As System.Windows.Forms.Label
        Dim Label30 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim IdPropietarioLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPreIngreso))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle25 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle24 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
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
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.BarSubItem1 = New DevExpress.XtraBars.BarSubItem()
        Me.mnuDocumentoPreIngreso = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImprimirBarras = New DevExpress.XtraBars.BarButtonItem()
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
        Me.GrpProducto = New DevExpress.XtraEditors.GroupControl()
        Me.GrpTIpoTransaccion = New DevExpress.XtraEditors.GroupControl()
        Me.txtDescripcionTR = New DevExpress.XtraEditors.TextEdit()
        Me.lnkTipoT = New System.Windows.Forms.LinkLabel()
        Me.txtIdTipoTR = New DevExpress.XtraEditors.TextEdit()
        Me.GrpTarea = New DevExpress.XtraEditors.GroupControl()
        Me.dtmFechaTarea = New DevExpress.XtraEditors.DateEdit()
        Me.GroupControl10 = New DevExpress.XtraEditors.GroupControl()
        Me.dtmHoraFhh = New System.Windows.Forms.DateTimePicker()
        Me.dtmHoraIhh = New System.Windows.Forms.DateTimePicker()
        Me.GroupControl9 = New DevExpress.XtraEditors.GroupControl()
        Me.dtmHoraF = New System.Windows.Forms.DateTimePicker()
        Me.dtmHoraI = New System.Windows.Forms.DateTimePicker()
        Me.GrpObservacion = New DevExpress.XtraEditors.GroupControl()
        Me.txtObservacion = New System.Windows.Forms.TextBox()
        Me.GrpParametrosIngreso = New DevExpress.XtraEditors.GroupControl()
        Me.lblParaPorCodigo = New System.Windows.Forms.Label()
        Me.lblMuestraCosto = New System.Windows.Forms.Label()
        Me.lblRecManual = New System.Windows.Forms.Label()
        Me.lblTomarFotos = New System.Windows.Forms.Label()
        Me.lblSEscanearUbicRec = New System.Windows.Forms.Label()
        Me.txtNoDocumento = New DevExpress.XtraEditors.TextEdit()
        Me.chkParaPorCodigo = New DevExpress.XtraEditors.CheckEdit()
        Me.chkMuestraCosto = New DevExpress.XtraEditors.CheckEdit()
        Me.chkEscanear = New DevExpress.XtraEditors.CheckEdit()
        Me.chkRecepcionManual = New DevExpress.XtraEditors.CheckEdit()
        Me.chkTomarFoto = New DevExpress.XtraEditors.CheckEdit()
        Me.GrpAsignacionTransaccion = New DevExpress.XtraEditors.GroupControl()
        Me.cmbMuelle = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtNombreUbicacion = New DevExpress.XtraEditors.TextEdit()
        Me.lnkUbicacion = New System.Windows.Forms.LinkLabel()
        Me.txtIdUbicacion = New DevExpress.XtraEditors.TextEdit()
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblPesoOC = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblCantidad = New System.Windows.Forms.Label()
        Me.lblCosto = New System.Windows.Forms.Label()
        Me.DgridOC = New System.Windows.Forms.DataGridView()
        Me.NoLinea = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CodigoProducto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Producto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Presentacion = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.UnidadMedida = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Arancel = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PesoOC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Costo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdProducto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IsNew = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.IdOrdenCompraEnc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdOrdenCompraDet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Key = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IsNewRecepcion = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Factor1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AtributoVariante1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.cmdAgregarProducto = New System.Windows.Forms.ToolStripButton()
        Me.cmdVerParametros = New System.Windows.Forms.ToolStripButton()
        Me.cmdEliminarFila = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblPesoR = New System.Windows.Forms.Label()
        Me.lblTotalR = New System.Windows.Forms.Label()
        Me.lblCantidadR = New System.Windows.Forms.Label()
        Me.lblCostoR = New System.Windows.Forms.Label()
        Me.GrpImagen = New DevExpress.XtraEditors.GroupControl()
        Me.PicImg = New System.Windows.Forms.PictureBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.GrdImagen = New DevExpress.XtraGrid.GridControl()
        Me.GridViewImg = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.cmdAdd = New System.Windows.Forms.ToolStripButton()
        Me.cmdDelete = New System.Windows.Forms.ToolStripButton()
        Me.GrpOperadorBodega = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl13 = New DevExpress.XtraEditors.GroupControl()
        Me.Grid = New DevExpress.XtraGrid.GridControl()
        Me.DataBindingSource = New System.Windows.Forms.BindingSource()
        Me.DsOrdenCompraRecepcionOperador = New DsOrdenCompraRecepcionOperador()
        Me.GrdOperadorBobega = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSelección = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colIdOperadorRec = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdOperadorBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colOperador = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colcolUsaHH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.dkRecepcion = New DevExpress.XtraBars.Docking.DockManager()
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.xtrRecepcion = New DevExpress.XtraTab.XtraTabControl()
        Me.DatosRec = New DevExpress.XtraTab.XtraTabPage()
        Me.DetalleOC = New DevExpress.XtraTab.XtraTabPage()
        Me.DetRec = New DevExpress.XtraTab.XtraTabPage()
        Me.Imagenes = New DevExpress.XtraTab.XtraTabPage()
        Me.DetOp = New DevExpress.XtraTab.XtraTabPage()
        Me.BarrasRec = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.cmdMarcarTodos = New System.Windows.Forms.ToolStripButton()
        Me.cmdDesmarcarTodos = New System.Windows.Forms.ToolStripButton()
        Me.dgridBarrasRec = New System.Windows.Forms.DataGridView()
        Me.ColIdStockRecBarras = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColImprimir = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colLicPlate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewComboBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewComboBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCantidadBarras = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewComboBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFechaProduccion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Label37 = New System.Windows.Forms.Label()
        Label38 = New System.Windows.Forms.Label()
        Label30 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        IdPropietarioLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpProducto.SuspendLayout()
        CType(Me.GrpTIpoTransaccion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTIpoTransaccion.SuspendLayout()
        CType(Me.txtDescripcionTR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdTipoTR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpTarea, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTarea.SuspendLayout()
        CType(Me.dtmFechaTarea.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaTarea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl10.SuspendLayout()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl9.SuspendLayout()
        CType(Me.GrpObservacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpObservacion.SuspendLayout()
        CType(Me.GrpParametrosIngreso, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpParametrosIngreso.SuspendLayout()
        CType(Me.txtNoDocumento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkParaPorCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMuestraCosto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEscanear.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRecepcionManual.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTomarFoto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpAsignacionTransaccion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpAsignacionTransaccion.SuspendLayout()
        CType(Me.cmbMuelle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpTransaccion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTransaccion.SuspendLayout()
        CType(Me.dtmFechaRecepcion.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaRecepcion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdOrdenCompra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpFactura, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpFactura.SuspendLayout()
        CType(Me.grdListaFactura, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpDetalle.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.DgridOC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpDetalleRecepcion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpDetalleRecepcion.SuspendLayout()
        CType(Me.DgridDetalleRec, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.GrpImagen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpImagen.SuspendLayout()
        CType(Me.PicImg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.GrdImagen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewImg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip.SuspendLayout()
        CType(Me.GrpOperadorBodega, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpOperadorBodega.SuspendLayout()
        CType(Me.GroupControl13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl13.SuspendLayout()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsOrdenCompraRecepcionOperador, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdOperadorBobega, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkRecepcion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtrRecepcion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtrRecepcion.SuspendLayout()
        Me.DatosRec.SuspendLayout()
        Me.DetalleOC.SuspendLayout()
        Me.DetRec.SuspendLayout()
        Me.Imagenes.SuspendLayout()
        Me.DetOp.SuspendLayout()
        Me.BarrasRec.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.dgridBarrasRec, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplashScreenManager1
        '
        SplashScreenManager1.ClosingDelay = 500
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
        'Label30
        '
        Label30.AutoSize = True
        Label30.Location = New System.Drawing.Point(9, 59)
        Label30.Name = "Label30"
        Label30.Size = New System.Drawing.Size(51, 13)
        Label30.TabIndex = 2
        Label30.Text = "Hora Fin:"
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
        'Label8
        '
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(18, 30)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(85, 13)
        Label8.TabIndex = 0
        Label8.Text = "No. Documento:"
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
        'Label10
        '
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(15, 65)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(40, 13)
        Label10.TabIndex = 2
        Label10.Text = "Estado"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(15, 39)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(40, 13)
        Label12.TabIndex = 0
        Label12.Text = "Código"
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
        'IdPropietarioLabel
        '
        IdPropietarioLabel.AutoSize = True
        IdPropietarioLabel.Location = New System.Drawing.Point(15, 118)
        IdPropietarioLabel.Name = "IdPropietarioLabel"
        IdPropietarioLabel.Size = New System.Drawing.Size(47, 13)
        IdPropietarioLabel.TabIndex = 9
        IdPropietarioLabel.Text = "Bodega:"
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
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(256, 14)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(85, 13)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
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
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 716)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1360, 31)
        '
        'RibbonControl
        '
        Me.RibbonControl.ApplicationIcon = Global.TOMWMS.My.Resources.Resources.inventory_maintenance_icon
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdGuardar, Me.mnuAsignacion, Me.BarButtonItem1, Me.BarButtonItem2, Me.BarButtonItem3, Me.cmdCodigoBarra, Me.cmdImprimeCodigoBarra, Me.cmdImprmirCodigoBarra, Me.cmdActualizar, Me.cmdEliminar, Me.cmdUbicacion, Me.cmdImprimir, Me.SubImprimir, Me.cmdPreIngreso, Me.cmdCostoArancel, Me.cmdFinalizar, Me.lblRegs, Me.BarSubItem1, Me.mnuDocumentoPreIngreso, Me.mnuImprimirBarras})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 24
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1360, 143)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar1
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Caption = "Guardar"
        Me.cmdGuardar.Id = 1
        Me.cmdGuardar.ImageOptions.Image = CType(resources.GetObject("cmdGuardar.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdGuardar.ImageOptions.LargeImage = CType(resources.GetObject("cmdGuardar.ImageOptions.LargeImage"), System.Drawing.Image)
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
        Me.cmdActualizar.ImageOptions.Image = CType(resources.GetObject("cmdActualizar.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdActualizar.ImageOptions.LargeImage = CType(resources.GetObject("cmdActualizar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdActualizar.Name = "cmdActualizar"
        '
        'cmdEliminar
        '
        Me.cmdEliminar.Caption = "Anular"
        Me.cmdEliminar.Id = 12
        Me.cmdEliminar.ImageOptions.Image = CType(resources.GetObject("cmdEliminar.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdEliminar.ImageOptions.LargeImage = CType(resources.GetObject("cmdEliminar.ImageOptions.LargeImage"), System.Drawing.Image)
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
        Me.cmdFinalizar.ImageOptions.Image = CType(resources.GetObject("cmdFinalizar.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdFinalizar.ImageOptions.LargeImage = CType(resources.GetObject("cmdFinalizar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdFinalizar.Name = "cmdFinalizar"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros:"
        Me.lblRegs.Id = 20
        Me.lblRegs.Name = "lblRegs"
        '
        'BarSubItem1
        '
        Me.BarSubItem1.Caption = "Imprimir"
        Me.BarSubItem1.Id = 21
        Me.BarSubItem1.ImageOptions.Image = CType(resources.GetObject("BarSubItem1.ImageOptions.Image"), System.Drawing.Image)
        Me.BarSubItem1.ImageOptions.LargeImage = CType(resources.GetObject("BarSubItem1.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuDocumentoPreIngreso), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuImprimirBarras)})
        Me.BarSubItem1.Name = "BarSubItem1"
        '
        'mnuDocumentoPreIngreso
        '
        Me.mnuDocumentoPreIngreso.Caption = "Documento de pre-ingreso"
        Me.mnuDocumentoPreIngreso.Id = 22
        Me.mnuDocumentoPreIngreso.Name = "mnuDocumentoPreIngreso"
        '
        'mnuImprimirBarras
        '
        Me.mnuImprimirBarras.Caption = "Barras de producto"
        Me.mnuImprimirBarras.Id = 23
        Me.mnuImprimirBarras.Name = "mnuImprimirBarras"
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
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.BarSubItem1)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonStatusBar1
        '
        Me.RibbonStatusBar1.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar1.Location = New System.Drawing.Point(2, 475)
        Me.RibbonStatusBar1.Name = "RibbonStatusBar1"
        Me.RibbonStatusBar1.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar1.Size = New System.Drawing.Size(1346, 27)
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
        'GrpProducto
        '
        Me.GrpProducto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GrpProducto.Controls.Add(Me.GrpTIpoTransaccion)
        Me.GrpProducto.Controls.Add(Me.GrpTarea)
        Me.GrpProducto.Controls.Add(Me.GrpObservacion)
        Me.GrpProducto.Controls.Add(Me.GrpParametrosIngreso)
        Me.GrpProducto.Controls.Add(Me.GrpAsignacionTransaccion)
        Me.GrpProducto.Controls.Add(Me.GrpTransaccion)
        Me.GrpProducto.Location = New System.Drawing.Point(10, 26)
        Me.GrpProducto.Name = "GrpProducto"
        Me.GrpProducto.ScrollBarSmallChange = 10
        Me.GrpProducto.Size = New System.Drawing.Size(876, 494)
        Me.GrpProducto.TabIndex = 0
        '
        'GrpTIpoTransaccion
        '
        Me.GrpTIpoTransaccion.Controls.Add(Me.txtDescripcionTR)
        Me.GrpTIpoTransaccion.Controls.Add(Me.lnkTipoT)
        Me.GrpTIpoTransaccion.Controls.Add(Me.txtIdTipoTR)
        Me.GrpTIpoTransaccion.Location = New System.Drawing.Point(446, 29)
        Me.GrpTIpoTransaccion.Name = "GrpTIpoTransaccion"
        Me.GrpTIpoTransaccion.Size = New System.Drawing.Size(420, 52)
        Me.GrpTIpoTransaccion.TabIndex = 1
        Me.GrpTIpoTransaccion.Text = "Tipo Transacción"
        '
        'txtDescripcionTR
        '
        Me.txtDescripcionTR.Location = New System.Drawing.Point(184, 23)
        Me.txtDescripcionTR.MenuManager = Me.RibbonControl
        Me.txtDescripcionTR.Name = "txtDescripcionTR"
        Me.txtDescripcionTR.Properties.ReadOnly = True
        Me.txtDescripcionTR.Size = New System.Drawing.Size(219, 20)
        Me.txtDescripcionTR.TabIndex = 2
        '
        'lnkTipoT
        '
        Me.lnkTipoT.AutoSize = True
        Me.lnkTipoT.Location = New System.Drawing.Point(15, 26)
        Me.lnkTipoT.Name = "lnkTipoT"
        Me.lnkTipoT.Size = New System.Drawing.Size(87, 13)
        Me.lnkTipoT.TabIndex = 0
        Me.lnkTipoT.TabStop = True
        Me.lnkTipoT.Text = "Tipo Transacción"
        '
        'txtIdTipoTR
        '
        Me.txtIdTipoTR.Location = New System.Drawing.Point(108, 23)
        Me.txtIdTipoTR.MenuManager = Me.RibbonControl
        Me.txtIdTipoTR.Name = "txtIdTipoTR"
        Me.txtIdTipoTR.Size = New System.Drawing.Size(70, 20)
        Me.txtIdTipoTR.TabIndex = 1
        '
        'GrpTarea
        '
        Me.GrpTarea.Controls.Add(Me.dtmFechaTarea)
        Me.GrpTarea.Controls.Add(Me.GroupControl10)
        Me.GrpTarea.Controls.Add(Me.GroupControl9)
        Me.GrpTarea.Location = New System.Drawing.Point(446, 271)
        Me.GrpTarea.Name = "GrpTarea"
        Me.GrpTarea.Size = New System.Drawing.Size(420, 150)
        Me.GrpTarea.TabIndex = 4
        Me.GrpTarea.Text = "Fecha Tarea"
        '
        'dtmFechaTarea
        '
        Me.dtmFechaTarea.Dock = System.Windows.Forms.DockStyle.Top
        Me.dtmFechaTarea.EditValue = New Date(2017, 11, 19, 11, 13, 59, 0)
        Me.dtmFechaTarea.Location = New System.Drawing.Point(2, 20)
        Me.dtmFechaTarea.MenuManager = Me.RibbonControl
        Me.dtmFechaTarea.Name = "dtmFechaTarea"
        Me.dtmFechaTarea.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaTarea.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaTarea.Size = New System.Drawing.Size(416, 20)
        Me.dtmFechaTarea.TabIndex = 0
        '
        'GroupControl10
        '
        Me.GroupControl10.Controls.Add(Label37)
        Me.GroupControl10.Controls.Add(Me.dtmHoraFhh)
        Me.GroupControl10.Controls.Add(Label38)
        Me.GroupControl10.Controls.Add(Me.dtmHoraIhh)
        Me.GroupControl10.Enabled = False
        Me.GroupControl10.Location = New System.Drawing.Point(212, 53)
        Me.GroupControl10.Name = "GroupControl10"
        Me.GroupControl10.Size = New System.Drawing.Size(201, 86)
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
        Me.GroupControl9.Location = New System.Drawing.Point(6, 53)
        Me.GroupControl9.Name = "GroupControl9"
        Me.GroupControl9.Size = New System.Drawing.Size(200, 86)
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
        'GrpObservacion
        '
        Me.GrpObservacion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GrpObservacion.Controls.Add(Me.txtObservacion)
        Me.GrpObservacion.Location = New System.Drawing.Point(20, 334)
        Me.GrpObservacion.Name = "GrpObservacion"
        Me.GrpObservacion.Size = New System.Drawing.Size(420, 155)
        Me.GrpObservacion.TabIndex = 5
        Me.GrpObservacion.Text = "Observación"
        '
        'txtObservacion
        '
        Me.txtObservacion.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtObservacion.Location = New System.Drawing.Point(21, 28)
        Me.txtObservacion.Multiline = True
        Me.txtObservacion.Name = "txtObservacion"
        Me.txtObservacion.Size = New System.Drawing.Size(383, 112)
        Me.txtObservacion.TabIndex = 0
        '
        'GrpParametrosIngreso
        '
        Me.GrpParametrosIngreso.Controls.Add(Me.lblParaPorCodigo)
        Me.GrpParametrosIngreso.Controls.Add(Me.lblMuestraCosto)
        Me.GrpParametrosIngreso.Controls.Add(Me.lblRecManual)
        Me.GrpParametrosIngreso.Controls.Add(Me.lblTomarFotos)
        Me.GrpParametrosIngreso.Controls.Add(Me.lblSEscanearUbicRec)
        Me.GrpParametrosIngreso.Controls.Add(Me.txtNoDocumento)
        Me.GrpParametrosIngreso.Controls.Add(Me.chkParaPorCodigo)
        Me.GrpParametrosIngreso.Controls.Add(Me.chkMuestraCosto)
        Me.GrpParametrosIngreso.Controls.Add(Me.chkEscanear)
        Me.GrpParametrosIngreso.Controls.Add(Me.chkRecepcionManual)
        Me.GrpParametrosIngreso.Controls.Add(Me.chkTomarFoto)
        Me.GrpParametrosIngreso.Controls.Add(Label8)
        Me.GrpParametrosIngreso.Location = New System.Drawing.Point(446, 84)
        Me.GrpParametrosIngreso.Name = "GrpParametrosIngreso"
        Me.GrpParametrosIngreso.Size = New System.Drawing.Size(420, 179)
        Me.GrpParametrosIngreso.TabIndex = 2
        Me.GrpParametrosIngreso.Text = "Parametros de Ingreso"
        '
        'lblParaPorCodigo
        '
        Me.lblParaPorCodigo.AutoSize = True
        Me.lblParaPorCodigo.Location = New System.Drawing.Point(18, 156)
        Me.lblParaPorCodigo.Name = "lblParaPorCodigo"
        Me.lblParaPorCodigo.Size = New System.Drawing.Size(88, 13)
        Me.lblParaPorCodigo.TabIndex = 10
        Me.lblParaPorCodigo.Text = "Para por Código:"
        '
        'lblMuestraCosto
        '
        Me.lblMuestraCosto.AutoSize = True
        Me.lblMuestraCosto.Location = New System.Drawing.Point(18, 131)
        Me.lblMuestraCosto.Name = "lblMuestraCosto"
        Me.lblMuestraCosto.Size = New System.Drawing.Size(81, 13)
        Me.lblMuestraCosto.TabIndex = 8
        Me.lblMuestraCosto.Text = "Muestra Costo:"
        '
        'lblRecManual
        '
        Me.lblRecManual.AutoSize = True
        Me.lblRecManual.Location = New System.Drawing.Point(18, 106)
        Me.lblRecManual.Name = "lblRecManual"
        Me.lblRecManual.Size = New System.Drawing.Size(97, 13)
        Me.lblRecManual.TabIndex = 6
        Me.lblRecManual.Text = "Recepción Manual:"
        '
        'lblTomarFotos
        '
        Me.lblTomarFotos.AutoSize = True
        Me.lblTomarFotos.Location = New System.Drawing.Point(18, 81)
        Me.lblTomarFotos.Name = "lblTomarFotos"
        Me.lblTomarFotos.Size = New System.Drawing.Size(69, 13)
        Me.lblTomarFotos.TabIndex = 4
        Me.lblTomarFotos.Text = "Tomar fotos:"
        '
        'lblSEscanearUbicRec
        '
        Me.lblSEscanearUbicRec.AutoSize = True
        Me.lblSEscanearUbicRec.Location = New System.Drawing.Point(18, 56)
        Me.lblSEscanearUbicRec.Name = "lblSEscanearUbicRec"
        Me.lblSEscanearUbicRec.Size = New System.Drawing.Size(166, 13)
        Me.lblSEscanearUbicRec.TabIndex = 2
        Me.lblSEscanearUbicRec.Text = "Escanear ubicación en recepción:"
        '
        'txtNoDocumento
        '
        Me.txtNoDocumento.Location = New System.Drawing.Point(108, 27)
        Me.txtNoDocumento.MenuManager = Me.RibbonControl
        Me.txtNoDocumento.Name = "txtNoDocumento"
        Me.txtNoDocumento.Properties.Mask.EditMask = "n0"
        Me.txtNoDocumento.Size = New System.Drawing.Size(295, 20)
        Me.txtNoDocumento.TabIndex = 1
        '
        'chkParaPorCodigo
        '
        Me.chkParaPorCodigo.Location = New System.Drawing.Point(213, 153)
        Me.chkParaPorCodigo.MenuManager = Me.RibbonControl
        Me.chkParaPorCodigo.Name = "chkParaPorCodigo"
        Me.chkParaPorCodigo.Properties.Caption = ""
        Me.chkParaPorCodigo.Size = New System.Drawing.Size(35, 19)
        Me.chkParaPorCodigo.TabIndex = 11
        '
        'chkMuestraCosto
        '
        Me.chkMuestraCosto.Location = New System.Drawing.Point(213, 128)
        Me.chkMuestraCosto.MenuManager = Me.RibbonControl
        Me.chkMuestraCosto.Name = "chkMuestraCosto"
        Me.chkMuestraCosto.Properties.Caption = ""
        Me.chkMuestraCosto.Size = New System.Drawing.Size(35, 19)
        Me.chkMuestraCosto.TabIndex = 9
        '
        'chkEscanear
        '
        Me.chkEscanear.Location = New System.Drawing.Point(213, 53)
        Me.chkEscanear.MenuManager = Me.RibbonControl
        Me.chkEscanear.Name = "chkEscanear"
        Me.chkEscanear.Properties.Caption = ""
        Me.chkEscanear.Size = New System.Drawing.Size(35, 19)
        Me.chkEscanear.TabIndex = 3
        '
        'chkRecepcionManual
        '
        Me.chkRecepcionManual.Location = New System.Drawing.Point(213, 103)
        Me.chkRecepcionManual.MenuManager = Me.RibbonControl
        Me.chkRecepcionManual.Name = "chkRecepcionManual"
        Me.chkRecepcionManual.Properties.Caption = ""
        Me.chkRecepcionManual.Size = New System.Drawing.Size(35, 19)
        Me.chkRecepcionManual.TabIndex = 7
        '
        'chkTomarFoto
        '
        Me.chkTomarFoto.Location = New System.Drawing.Point(213, 78)
        Me.chkTomarFoto.MenuManager = Me.RibbonControl
        Me.chkTomarFoto.Name = "chkTomarFoto"
        Me.chkTomarFoto.Properties.Caption = ""
        Me.chkTomarFoto.Size = New System.Drawing.Size(35, 19)
        Me.chkTomarFoto.TabIndex = 5
        '
        'GrpAsignacionTransaccion
        '
        Me.GrpAsignacionTransaccion.Controls.Add(Me.cmbMuelle)
        Me.GrpAsignacionTransaccion.Controls.Add(Label5)
        Me.GrpAsignacionTransaccion.Controls.Add(Me.txtNombreUbicacion)
        Me.GrpAsignacionTransaccion.Controls.Add(Me.lnkUbicacion)
        Me.GrpAsignacionTransaccion.Controls.Add(Me.txtIdUbicacion)
        Me.GrpAsignacionTransaccion.Location = New System.Drawing.Point(20, 234)
        Me.GrpAsignacionTransaccion.Name = "GrpAsignacionTransaccion"
        Me.GrpAsignacionTransaccion.Size = New System.Drawing.Size(420, 95)
        Me.GrpAsignacionTransaccion.TabIndex = 3
        Me.GrpAsignacionTransaccion.Text = "Asignación de Transacción"
        '
        'cmbMuelle
        '
        Me.cmbMuelle.Location = New System.Drawing.Point(121, 34)
        Me.cmbMuelle.MenuManager = Me.RibbonControl
        Me.cmbMuelle.Name = "cmbMuelle"
        Me.cmbMuelle.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMuelle.Properties.NullText = ""
        Me.cmbMuelle.Size = New System.Drawing.Size(282, 20)
        Me.cmbMuelle.TabIndex = 1
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
        Me.lnkUbicacion.Size = New System.Drawing.Size(83, 13)
        Me.lnkUbicacion.TabIndex = 2
        Me.lnkUbicacion.TabStop = True
        Me.lnkUbicacion.Text = "Ubic. Recepción"
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
        Me.GrpTransaccion.Location = New System.Drawing.Point(20, 29)
        Me.GrpTransaccion.Name = "GrpTransaccion"
        Me.GrpTransaccion.Size = New System.Drawing.Size(420, 202)
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
        Me.lblDiagonal.AutoSize = True
        Me.lblDiagonal.Location = New System.Drawing.Point(168, 65)
        Me.lblDiagonal.Name = "lblDiagonal"
        Me.lblDiagonal.Size = New System.Drawing.Size(11, 13)
        Me.lblDiagonal.TabIndex = 4
        Me.lblDiagonal.Text = "/"
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
        Me.txtOC.Location = New System.Drawing.Point(197, 169)
        Me.txtOC.MenuManager = Me.RibbonControl
        Me.txtOC.Name = "txtOC"
        Me.txtOC.Properties.ReadOnly = True
        Me.txtOC.Size = New System.Drawing.Size(206, 20)
        Me.txtOC.TabIndex = 15
        '
        'lblC
        '
        Me.lblC.AutoSize = True
        Me.lblC.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblC.Location = New System.Drawing.Point(123, 39)
        Me.lblC.Name = "lblC"
        Me.lblC.Size = New System.Drawing.Size(0, 13)
        Me.lblC.TabIndex = 1
        '
        'lnk
        '
        Me.lnk.AutoSize = True
        Me.lnk.Location = New System.Drawing.Point(15, 172)
        Me.lnk.Name = "lnk"
        Me.lnk.Size = New System.Drawing.Size(92, 13)
        Me.lnk.TabIndex = 13
        Me.lnk.TabStop = True
        Me.lnk.Text = "Orden de Compra"
        '
        'txtIdOrdenCompra
        '
        Me.txtIdOrdenCompra.Location = New System.Drawing.Point(121, 169)
        Me.txtIdOrdenCompra.MenuManager = Me.RibbonControl
        Me.txtIdOrdenCompra.Name = "txtIdOrdenCompra"
        Me.txtIdOrdenCompra.Properties.Mask.EditMask = "n0"
        Me.txtIdOrdenCompra.Size = New System.Drawing.Size(70, 20)
        Me.txtIdOrdenCompra.TabIndex = 14
        '
        'GrpFactura
        '
        Me.GrpFactura.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GrpFactura.Controls.Add(Me.cmdAbajo)
        Me.GrpFactura.Controls.Add(Me.cmdArriba)
        Me.GrpFactura.Controls.Add(Me.cmdEliminarFactura)
        Me.GrpFactura.Controls.Add(Me.cmdAgregar)
        Me.GrpFactura.Controls.Add(Me.grdListaFactura)
        Me.GrpFactura.Location = New System.Drawing.Point(892, 26)
        Me.GrpFactura.Name = "GrpFactura"
        Me.GrpFactura.Size = New System.Drawing.Size(455, 494)
        Me.GrpFactura.TabIndex = 1
        Me.GrpFactura.Text = "Facturas asociadas"
        '
        'cmdAbajo
        '
        Me.cmdAbajo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAbajo.ImageOptions.Image = CType(resources.GetObject("cmdAbajo.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdAbajo.Location = New System.Drawing.Point(409, 165)
        Me.cmdAbajo.Name = "cmdAbajo"
        Me.cmdAbajo.Size = New System.Drawing.Size(41, 39)
        Me.cmdAbajo.TabIndex = 4
        '
        'cmdArriba
        '
        Me.cmdArriba.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdArriba.ImageOptions.Image = CType(resources.GetObject("cmdArriba.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdArriba.Location = New System.Drawing.Point(409, 119)
        Me.cmdArriba.Name = "cmdArriba"
        Me.cmdArriba.Size = New System.Drawing.Size(41, 39)
        Me.cmdArriba.TabIndex = 3
        '
        'cmdEliminarFactura
        '
        Me.cmdEliminarFactura.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdEliminarFactura.ImageOptions.Image = CType(resources.GetObject("cmdEliminarFactura.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdEliminarFactura.Location = New System.Drawing.Point(409, 74)
        Me.cmdEliminarFactura.Name = "cmdEliminarFactura"
        Me.cmdEliminarFactura.Size = New System.Drawing.Size(41, 39)
        Me.cmdEliminarFactura.TabIndex = 2
        '
        'cmdAgregar
        '
        Me.cmdAgregar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAgregar.ImageOptions.Image = CType(resources.GetObject("cmdAgregar.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdAgregar.Location = New System.Drawing.Point(409, 29)
        Me.cmdAgregar.Name = "cmdAgregar"
        Me.cmdAgregar.Size = New System.Drawing.Size(41, 39)
        Me.cmdAgregar.TabIndex = 1
        '
        'grdListaFactura
        '
        Me.grdListaFactura.AllowUserToAddRows = False
        Me.grdListaFactura.AllowUserToDeleteRows = False
        Me.grdListaFactura.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SteelBlue
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdListaFactura.DefaultCellStyle = DataGridViewCellStyle2
        Me.grdListaFactura.GridColor = System.Drawing.SystemColors.ActiveBorder
        Me.grdListaFactura.Location = New System.Drawing.Point(2, 20)
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
        Me.grdListaFactura.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdListaFactura.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdListaFactura.ShowEditingIcon = False
        Me.grdListaFactura.ShowRowErrors = False
        Me.grdListaFactura.Size = New System.Drawing.Size(453, 469)
        Me.grdListaFactura.TabIndex = 0
        '
        'IdFacturaRecepcion
        '
        Me.IdFacturaRecepcion.HeaderText = "IdFacturaRecepcion"
        Me.IdFacturaRecepcion.Name = "IdFacturaRecepcion"
        Me.IdFacturaRecepcion.ReadOnly = True
        Me.IdFacturaRecepcion.Visible = False
        '
        'IdRecepcion
        '
        Me.IdRecepcion.HeaderText = "IdRecepcion"
        Me.IdRecepcion.Name = "IdRecepcion"
        Me.IdRecepcion.ReadOnly = True
        Me.IdRecepcion.Visible = False
        '
        'Orden
        '
        Me.Orden.HeaderText = "Orden"
        Me.Orden.Name = "Orden"
        Me.Orden.ReadOnly = True
        '
        'NoFactura
        '
        Me.NoFactura.HeaderText = "No. Factura"
        Me.NoFactura.Name = "NoFactura"
        Me.NoFactura.ReadOnly = True
        '
        'Obs
        '
        Me.Obs.HeaderText = "Observación"
        Me.Obs.Name = "Obs"
        Me.Obs.ReadOnly = True
        Me.Obs.Width = 175
        '
        'Completa
        '
        Me.Completa.HeaderText = "Completa"
        Me.Completa.Name = "Completa"
        Me.Completa.ReadOnly = True
        Me.Completa.Width = 75
        '
        'GrpDetalle
        '
        Me.GrpDetalle.Controls.Add(Me.Panel2)
        Me.GrpDetalle.Controls.Add(Me.DgridOC)
        Me.GrpDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpDetalle.Location = New System.Drawing.Point(0, 0)
        Me.GrpDetalle.Name = "GrpDetalle"
        Me.GrpDetalle.Size = New System.Drawing.Size(1354, 526)
        Me.GrpDetalle.TabIndex = 0
        Me.GrpDetalle.Text = "Lista de Productos"
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.lblPesoOC)
        Me.Panel2.Controls.Add(Me.lblTotal)
        Me.Panel2.Controls.Add(Me.lblCantidad)
        Me.Panel2.Controls.Add(Me.lblCosto)
        Me.Panel2.Location = New System.Drawing.Point(5, 494)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1344, 27)
        Me.Panel2.TabIndex = 1
        '
        'lblPesoOC
        '
        Me.lblPesoOC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPesoOC.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblPesoOC.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPesoOC.Location = New System.Drawing.Point(846, 7)
        Me.lblPesoOC.Name = "lblPesoOC"
        Me.lblPesoOC.Size = New System.Drawing.Size(160, 13)
        Me.lblPesoOC.TabIndex = 1
        Me.lblPesoOC.Text = "0000000000.00"
        Me.lblPesoOC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotal
        '
        Me.lblTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTotal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(1181, 7)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(160, 13)
        Me.lblTotal.TabIndex = 3
        Me.lblTotal.Text = "0000000000.00"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCantidad
        '
        Me.lblCantidad.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCantidad.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblCantidad.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCantidad.Location = New System.Drawing.Point(680, 7)
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.Size = New System.Drawing.Size(160, 13)
        Me.lblCantidad.TabIndex = 0
        Me.lblCantidad.Text = "0000000000.00"
        Me.lblCantidad.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCosto
        '
        Me.lblCosto.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCosto.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblCosto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCosto.Location = New System.Drawing.Point(1015, 7)
        Me.lblCosto.Name = "lblCosto"
        Me.lblCosto.Size = New System.Drawing.Size(160, 13)
        Me.lblCosto.TabIndex = 2
        Me.lblCosto.Text = "0000000000.00"
        Me.lblCosto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DgridOC
        '
        Me.DgridOC.AllowUserToAddRows = False
        Me.DgridOC.AllowUserToDeleteRows = False
        Me.DgridOC.AllowUserToResizeRows = False
        Me.DgridOC.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DgridOC.BackgroundColor = System.Drawing.Color.PaleTurquoise
        Me.DgridOC.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SteelBlue
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgridOC.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DgridOC.ColumnHeadersHeight = 40
        Me.DgridOC.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NoLinea, Me.CodigoProducto, Me.Producto, Me.Presentacion, Me.UnidadMedida, Me.Arancel, Me.Cantidad, Me.PesoOC, Me.Costo, Me.Total, Me.IdProducto, Me.IsNew, Me.IdOrdenCompraEnc, Me.IdOrdenCompraDet, Me.Key, Me.IsNewRecepcion, Me.Factor1, Me.AtributoVariante1})
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Tahoma", 8.25!)
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.SteelBlue
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgridOC.DefaultCellStyle = DataGridViewCellStyle9
        Me.DgridOC.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.DgridOC.Enabled = False
        Me.DgridOC.GridColor = System.Drawing.SystemColors.ActiveBorder
        Me.DgridOC.Location = New System.Drawing.Point(5, 34)
        Me.DgridOC.MultiSelect = False
        Me.DgridOC.Name = "DgridOC"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Tahoma", 8.25!)
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.SteelBlue
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgridOC.RowHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.DgridOC.RowHeadersVisible = False
        Me.DgridOC.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DgridOC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgridOC.ShowEditingIcon = False
        Me.DgridOC.ShowRowErrors = False
        Me.DgridOC.Size = New System.Drawing.Size(1344, 454)
        Me.DgridOC.TabIndex = 0
        '
        'NoLinea
        '
        Me.NoLinea.HeaderText = "No. Linea"
        Me.NoLinea.Name = "NoLinea"
        '
        'CodigoProducto
        '
        Me.CodigoProducto.HeaderText = "Código"
        Me.CodigoProducto.Name = "CodigoProducto"
        Me.CodigoProducto.ReadOnly = True
        '
        'Producto
        '
        Me.Producto.HeaderText = "Producto"
        Me.Producto.Name = "Producto"
        Me.Producto.ReadOnly = True
        '
        'Presentacion
        '
        Me.Presentacion.HeaderText = "Presentación"
        Me.Presentacion.Name = "Presentacion"
        Me.Presentacion.ReadOnly = True
        Me.Presentacion.Width = 125
        '
        'UnidadMedida
        '
        Me.UnidadMedida.HeaderText = "Unidad Medida"
        Me.UnidadMedida.Name = "UnidadMedida"
        Me.UnidadMedida.ReadOnly = True
        Me.UnidadMedida.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.UnidadMedida.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.UnidadMedida.Width = 125
        '
        'Arancel
        '
        Me.Arancel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Arancel.HeaderText = "Arancel"
        Me.Arancel.Name = "Arancel"
        Me.Arancel.ReadOnly = True
        Me.Arancel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Arancel.Width = 68
        '
        'Cantidad
        '
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.Cantidad.DefaultCellStyle = DataGridViewCellStyle5
        Me.Cantidad.HeaderText = "Cantidad"
        Me.Cantidad.Name = "Cantidad"
        Me.Cantidad.ReadOnly = True
        '
        'PesoOC
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N6"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.PesoOC.DefaultCellStyle = DataGridViewCellStyle6
        Me.PesoOC.HeaderText = "Peso"
        Me.PesoOC.Name = "PesoOC"
        Me.PesoOC.ReadOnly = True
        '
        'Costo
        '
        DataGridViewCellStyle7.Format = "N6"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.Costo.DefaultCellStyle = DataGridViewCellStyle7
        Me.Costo.HeaderText = "Costo"
        Me.Costo.Name = "Costo"
        Me.Costo.ReadOnly = True
        '
        'Total
        '
        DataGridViewCellStyle8.Format = "N6"
        DataGridViewCellStyle8.NullValue = Nothing
        Me.Total.DefaultCellStyle = DataGridViewCellStyle8
        Me.Total.HeaderText = "Total"
        Me.Total.Name = "Total"
        Me.Total.ReadOnly = True
        '
        'IdProducto
        '
        Me.IdProducto.HeaderText = "IdProducto"
        Me.IdProducto.Name = "IdProducto"
        Me.IdProducto.ReadOnly = True
        Me.IdProducto.Visible = False
        '
        'IsNew
        '
        Me.IsNew.HeaderText = "IsNew"
        Me.IsNew.Name = "IsNew"
        Me.IsNew.ReadOnly = True
        Me.IsNew.Visible = False
        '
        'IdOrdenCompraEnc
        '
        Me.IdOrdenCompraEnc.HeaderText = "IdOrdenCompraEnc"
        Me.IdOrdenCompraEnc.Name = "IdOrdenCompraEnc"
        Me.IdOrdenCompraEnc.ReadOnly = True
        Me.IdOrdenCompraEnc.Visible = False
        '
        'IdOrdenCompraDet
        '
        Me.IdOrdenCompraDet.HeaderText = "IdOrdenCompraDet"
        Me.IdOrdenCompraDet.Name = "IdOrdenCompraDet"
        Me.IdOrdenCompraDet.ReadOnly = True
        Me.IdOrdenCompraDet.Visible = False
        '
        'Key
        '
        Me.Key.HeaderText = "Key"
        Me.Key.Name = "Key"
        Me.Key.ReadOnly = True
        Me.Key.Visible = False
        '
        'IsNewRecepcion
        '
        Me.IsNewRecepcion.HeaderText = "IsNewRecepcion"
        Me.IsNewRecepcion.Name = "IsNewRecepcion"
        Me.IsNewRecepcion.ReadOnly = True
        Me.IsNewRecepcion.Visible = False
        '
        'Factor1
        '
        Me.Factor1.HeaderText = "Factor1"
        Me.Factor1.Name = "Factor1"
        Me.Factor1.ReadOnly = True
        Me.Factor1.Visible = False
        '
        'AtributoVariante1
        '
        Me.AtributoVariante1.HeaderText = "Variant_Code"
        Me.AtributoVariante1.Name = "AtributoVariante1"
        Me.AtributoVariante1.ReadOnly = True
        '
        'GrpDetalleRecepcion
        '
        Me.GrpDetalleRecepcion.Controls.Add(Me.DgridDetalleRec)
        Me.GrpDetalleRecepcion.Controls.Add(Me.ToolStrip1)
        Me.GrpDetalleRecepcion.Controls.Add(Me.Panel1)
        Me.GrpDetalleRecepcion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpDetalleRecepcion.Location = New System.Drawing.Point(0, 0)
        Me.GrpDetalleRecepcion.Name = "GrpDetalleRecepcion"
        Me.GrpDetalleRecepcion.Size = New System.Drawing.Size(1354, 526)
        Me.GrpDetalleRecepcion.TabIndex = 0
        Me.GrpDetalleRecepcion.Text = "Lista de Productos"
        '
        'DgridDetalleRec
        '
        Me.DgridDetalleRec.AllowUserToDeleteRows = False
        Me.DgridDetalleRec.AllowUserToResizeRows = False
        Me.DgridDetalleRec.BackgroundColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Tahoma", 8.25!)
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgridDetalleRec.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.DgridDetalleRec.ColumnHeadersHeight = 40
        Me.DgridDetalleRec.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No_Linea, Me.CodigoP, Me.ProductoP, Me.PresentacionP, Me.UnidadMedidaP, Me.CantidadP, Me.Peso, Me.CostoOC, Me.CostoP, Me.TotalP, Me.IdProductoP, Me.FechaVencimiento, Me.Estado, Me.Lote, Me.MotivoDevolucion, Me.IsNewR, Me.IdRecepcionEnc, Me.IdRecepcionDet, Me.Observacion, Me.IdUbicacionDefecto, Me.ControlVencimiento, Me.KeyP, Me.PesoPresentacion, Me.ControlPeso, Me.Factor2, Me.PesoUnitario, Me.Atributo_Variante_1})
        Me.DgridDetalleRec.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridDetalleRec.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.DgridDetalleRec.EnableHeadersVisualStyles = False
        Me.DgridDetalleRec.GridColor = System.Drawing.Color.Navy
        Me.DgridDetalleRec.Location = New System.Drawing.Point(2, 45)
        Me.DgridDetalleRec.MultiSelect = False
        Me.DgridDetalleRec.Name = "DgridDetalleRec"
        DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle21.Font = New System.Drawing.Font("Tahoma", 8.25!)
        DataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgridDetalleRec.RowHeadersDefaultCellStyle = DataGridViewCellStyle21
        Me.DgridDetalleRec.RowHeadersVisible = False
        Me.DgridDetalleRec.RowHeadersWidth = 60
        Me.DgridDetalleRec.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgridDetalleRec.Size = New System.Drawing.Size(1350, 440)
        Me.DgridDetalleRec.TabIndex = 1
        '
        'No_Linea
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.Format = "N0"
        Me.No_Linea.DefaultCellStyle = DataGridViewCellStyle12
        Me.No_Linea.HeaderText = "No. Linea"
        Me.No_Linea.Name = "No_Linea"
        Me.No_Linea.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.No_Linea.Width = 97
        '
        'CodigoP
        '
        Me.CodigoP.HeaderText = "Código"
        Me.CodigoP.Name = "CodigoP"
        Me.CodigoP.Width = 97
        '
        'ProductoP
        '
        Me.ProductoP.HeaderText = "Producto"
        Me.ProductoP.Name = "ProductoP"
        Me.ProductoP.ReadOnly = True
        Me.ProductoP.Width = 96
        '
        'PresentacionP
        '
        Me.PresentacionP.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.PresentacionP.HeaderText = "Presentación"
        Me.PresentacionP.Name = "PresentacionP"
        Me.PresentacionP.Width = 97
        '
        'UnidadMedidaP
        '
        Me.UnidadMedidaP.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.UnidadMedidaP.HeaderText = "U.M. Bas"
        Me.UnidadMedidaP.Name = "UnidadMedidaP"
        Me.UnidadMedidaP.Width = 97
        '
        'CantidadP
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.Format = "N2"
        DataGridViewCellStyle13.NullValue = "0"
        Me.CantidadP.DefaultCellStyle = DataGridViewCellStyle13
        Me.CantidadP.HeaderText = "Cantidad"
        Me.CantidadP.Name = "CantidadP"
        Me.CantidadP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CantidadP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CantidadP.Width = 97
        '
        'Peso
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle14.Format = "N2"
        DataGridViewCellStyle14.NullValue = "0"
        Me.Peso.DefaultCellStyle = DataGridViewCellStyle14
        Me.Peso.HeaderText = "Peso"
        Me.Peso.Name = "Peso"
        Me.Peso.ReadOnly = True
        Me.Peso.Width = 96
        '
        'CostoOC
        '
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle15.Format = "N6"
        DataGridViewCellStyle15.NullValue = Nothing
        Me.CostoOC.DefaultCellStyle = DataGridViewCellStyle15
        Me.CostoOC.HeaderText = "Costo OC"
        Me.CostoOC.Name = "CostoOC"
        Me.CostoOC.ReadOnly = True
        Me.CostoOC.Width = 97
        '
        'CostoP
        '
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle16.Format = "N6"
        DataGridViewCellStyle16.NullValue = Nothing
        Me.CostoP.DefaultCellStyle = DataGridViewCellStyle16
        Me.CostoP.HeaderText = "Costo Real"
        Me.CostoP.Name = "CostoP"
        Me.CostoP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CostoP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CostoP.Width = 97
        '
        'TotalP
        '
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle17.Format = "N2"
        DataGridViewCellStyle17.NullValue = Nothing
        Me.TotalP.DefaultCellStyle = DataGridViewCellStyle17
        Me.TotalP.HeaderText = "Total"
        Me.TotalP.Name = "TotalP"
        Me.TotalP.ReadOnly = True
        Me.TotalP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TotalP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TotalP.Width = 97
        '
        'IdProductoP
        '
        Me.IdProductoP.HeaderText = "IdProducto"
        Me.IdProductoP.Name = "IdProductoP"
        Me.IdProductoP.ReadOnly = True
        Me.IdProductoP.Visible = False
        '
        'FechaVencimiento
        '
        Me.FechaVencimiento.HeaderText = "Fecha Vencimiento"
        Me.FechaVencimiento.Name = "FechaVencimiento"
        Me.FechaVencimiento.Width = 97
        '
        'Estado
        '
        Me.Estado.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.Estado.HeaderText = "Estado"
        Me.Estado.Name = "Estado"
        Me.Estado.Width = 96
        '
        'Lote
        '
        Me.Lote.HeaderText = "Lote"
        Me.Lote.Name = "Lote"
        Me.Lote.Width = 97
        '
        'MotivoDevolucion
        '
        Me.MotivoDevolucion.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.MotivoDevolucion.HeaderText = "MotivoDevolucion"
        Me.MotivoDevolucion.Name = "MotivoDevolucion"
        Me.MotivoDevolucion.Visible = False
        '
        'IsNewR
        '
        Me.IsNewR.HeaderText = "IsNewR"
        Me.IsNewR.Name = "IsNewR"
        Me.IsNewR.ReadOnly = True
        Me.IsNewR.Visible = False
        '
        'IdRecepcionEnc
        '
        Me.IdRecepcionEnc.HeaderText = "IdRecepcionEnc"
        Me.IdRecepcionEnc.Name = "IdRecepcionEnc"
        Me.IdRecepcionEnc.ReadOnly = True
        Me.IdRecepcionEnc.Visible = False
        '
        'IdRecepcionDet
        '
        Me.IdRecepcionDet.HeaderText = "IdRecepcionDet"
        Me.IdRecepcionDet.Name = "IdRecepcionDet"
        Me.IdRecepcionDet.ReadOnly = True
        Me.IdRecepcionDet.Visible = False
        '
        'Observacion
        '
        Me.Observacion.HeaderText = "Observación"
        Me.Observacion.Name = "Observacion"
        Me.Observacion.Width = 97
        '
        'IdUbicacionDefecto
        '
        Me.IdUbicacionDefecto.HeaderText = "IdUbicacionDefecto"
        Me.IdUbicacionDefecto.Name = "IdUbicacionDefecto"
        Me.IdUbicacionDefecto.ReadOnly = True
        Me.IdUbicacionDefecto.Visible = False
        '
        'ControlVencimiento
        '
        Me.ControlVencimiento.HeaderText = "ControlVencimiento"
        Me.ControlVencimiento.Name = "ControlVencimiento"
        Me.ControlVencimiento.ReadOnly = True
        Me.ControlVencimiento.Visible = False
        '
        'KeyP
        '
        Me.KeyP.HeaderText = "Key"
        Me.KeyP.Name = "KeyP"
        Me.KeyP.ReadOnly = True
        Me.KeyP.Visible = False
        '
        'PesoPresentacion
        '
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle18.Format = "N2"
        DataGridViewCellStyle18.NullValue = Nothing
        Me.PesoPresentacion.DefaultCellStyle = DataGridViewCellStyle18
        Me.PesoPresentacion.HeaderText = "PesoPresentacion"
        Me.PesoPresentacion.Name = "PesoPresentacion"
        Me.PesoPresentacion.ReadOnly = True
        Me.PesoPresentacion.Visible = False
        '
        'ControlPeso
        '
        Me.ControlPeso.HeaderText = "ControlPeso"
        Me.ControlPeso.Name = "ControlPeso"
        Me.ControlPeso.ReadOnly = True
        Me.ControlPeso.Visible = False
        '
        'Factor2
        '
        Me.Factor2.HeaderText = "Factor2"
        Me.Factor2.Name = "Factor2"
        Me.Factor2.ReadOnly = True
        Me.Factor2.Visible = False
        '
        'PesoUnitario
        '
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle19.Format = "N2"
        DataGridViewCellStyle19.NullValue = Nothing
        Me.PesoUnitario.DefaultCellStyle = DataGridViewCellStyle19
        Me.PesoUnitario.HeaderText = "PesoUnitario"
        Me.PesoUnitario.Name = "PesoUnitario"
        Me.PesoUnitario.ReadOnly = True
        Me.PesoUnitario.Visible = False
        '
        'Atributo_Variante_1
        '
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Atributo_Variante_1.DefaultCellStyle = DataGridViewCellStyle20
        Me.Atributo_Variante_1.HeaderText = "Atributo_Variante_1"
        Me.Atributo_Variante_1.Name = "Atributo_Variante_1"
        Me.Atributo_Variante_1.ReadOnly = True
        Me.Atributo_Variante_1.Visible = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdAgregarProducto, Me.cmdVerParametros, Me.cmdEliminarFila})
        Me.ToolStrip1.Location = New System.Drawing.Point(2, 20)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1350, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip4"
        '
        'cmdAgregarProducto
        '
        Me.cmdAgregarProducto.Image = CType(resources.GetObject("cmdAgregarProducto.Image"), System.Drawing.Image)
        Me.cmdAgregarProducto.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAgregarProducto.Name = "cmdAgregarProducto"
        Me.cmdAgregarProducto.Size = New System.Drawing.Size(121, 22)
        Me.cmdAgregarProducto.Text = "Agregar Producto"
        '
        'cmdVerParametros
        '
        Me.cmdVerParametros.Image = CType(resources.GetObject("cmdVerParametros.Image"), System.Drawing.Image)
        Me.cmdVerParametros.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdVerParametros.Name = "cmdVerParametros"
        Me.cmdVerParametros.Size = New System.Drawing.Size(107, 22)
        Me.cmdVerParametros.Text = "Ver parámetros"
        '
        'cmdEliminarFila
        '
        Me.cmdEliminarFila.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEliminarFila.Image = CType(resources.GetObject("cmdEliminarFila.Image"), System.Drawing.Image)
        Me.cmdEliminarFila.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdEliminarFila.Name = "cmdEliminarFila"
        Me.cmdEliminarFila.Size = New System.Drawing.Size(92, 22)
        Me.cmdEliminarFila.Text = "Eliminar Fila"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblPesoR)
        Me.Panel1.Controls.Add(Me.lblTotalR)
        Me.Panel1.Controls.Add(Me.lblCantidadR)
        Me.Panel1.Controls.Add(Me.lblCostoR)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(2, 485)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1350, 39)
        Me.Panel1.TabIndex = 2
        '
        'lblPesoR
        '
        Me.lblPesoR.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPesoR.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblPesoR.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPesoR.Location = New System.Drawing.Point(855, 7)
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
        Me.lblTotalR.Location = New System.Drawing.Point(1187, 7)
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
        Me.lblCantidadR.Location = New System.Drawing.Point(676, 7)
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
        Me.lblCostoR.Location = New System.Drawing.Point(1021, 7)
        Me.lblCostoR.Name = "lblCostoR"
        Me.lblCostoR.Size = New System.Drawing.Size(160, 13)
        Me.lblCostoR.TabIndex = 2
        Me.lblCostoR.Text = "0000000000.00"
        Me.lblCostoR.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GrpImagen
        '
        Me.GrpImagen.Controls.Add(Me.PicImg)
        Me.GrpImagen.Controls.Add(Me.Label23)
        Me.GrpImagen.Controls.Add(Me.GroupControl4)
        Me.GrpImagen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpImagen.Location = New System.Drawing.Point(0, 0)
        Me.GrpImagen.Name = "GrpImagen"
        Me.GrpImagen.Size = New System.Drawing.Size(1354, 526)
        Me.GrpImagen.TabIndex = 0
        '
        'PicImg
        '
        Me.PicImg.Location = New System.Drawing.Point(389, 49)
        Me.PicImg.Name = "PicImg"
        Me.PicImg.Size = New System.Drawing.Size(523, 376)
        Me.PicImg.TabIndex = 93
        Me.PicImg.TabStop = False
        Me.PicImg.Visible = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(386, 28)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(98, 13)
        Me.Label23.TabIndex = 1
        Me.Label23.Text = "Nombre Imagen"
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.GrdImagen)
        Me.GroupControl4.Controls.Add(Me.ToolStrip)
        Me.GroupControl4.Location = New System.Drawing.Point(5, 28)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(378, 399)
        Me.GroupControl4.TabIndex = 0
        Me.GroupControl4.Text = "Lista de Imágenes"
        '
        'GrdImagen
        '
        Me.GrdImagen.Cursor = System.Windows.Forms.Cursors.Default
        Me.GrdImagen.Dock = System.Windows.Forms.DockStyle.Fill
        GridLevelNode1.RelationName = "Level1"
        Me.GrdImagen.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.GrdImagen.Location = New System.Drawing.Point(2, 45)
        Me.GrdImagen.MainView = Me.GridViewImg
        Me.GrdImagen.MenuManager = Me.RibbonControl
        Me.GrdImagen.Name = "GrdImagen"
        Me.GrdImagen.Size = New System.Drawing.Size(374, 352)
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
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdAdd, Me.cmdDelete})
        Me.ToolStrip.Location = New System.Drawing.Point(2, 20)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(374, 25)
        Me.ToolStrip.TabIndex = 0
        Me.ToolStrip.Text = "ToolStrip4"
        '
        'cmdAdd
        '
        Me.cmdAdd.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Me.cmdAdd.Image = Global.TOMWMS.My.Resources.Resources.add
        Me.cmdAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(23, 22)
        '
        'cmdDelete
        '
        Me.cmdDelete.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(23, 22)
        '
        'GrpOperadorBodega
        '
        Me.GrpOperadorBodega.Controls.Add(Me.GroupControl13)
        Me.GrpOperadorBodega.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpOperadorBodega.Location = New System.Drawing.Point(0, 0)
        Me.GrpOperadorBodega.Name = "GrpOperadorBodega"
        Me.GrpOperadorBodega.Size = New System.Drawing.Size(1354, 526)
        Me.GrpOperadorBodega.TabIndex = 0
        Me.GrpOperadorBodega.Tag = ""
        '
        'GroupControl13
        '
        Me.GroupControl13.Controls.Add(Me.RibbonStatusBar1)
        Me.GroupControl13.Controls.Add(Me.Grid)
        Me.GroupControl13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl13.Location = New System.Drawing.Point(2, 20)
        Me.GroupControl13.Name = "GroupControl13"
        Me.GroupControl13.Size = New System.Drawing.Size(1350, 504)
        Me.GroupControl13.TabIndex = 0
        Me.GroupControl13.Text = "Selección de Operador"
        '
        'Grid
        '
        Me.Grid.Cursor = System.Windows.Forms.Cursors.Default
        Me.Grid.DataSource = Me.DataBindingSource
        Me.Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid.Location = New System.Drawing.Point(2, 20)
        Me.Grid.MainView = Me.GrdOperadorBobega
        Me.Grid.Name = "Grid"
        Me.Grid.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2})
        Me.Grid.Size = New System.Drawing.Size(1346, 482)
        Me.Grid.TabIndex = 0
        Me.Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GrdOperadorBobega})
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
        Me.GrdOperadorBobega.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSelección, Me.colIdOperadorRec, Me.colIdOperadorBodega, Me.colOperador, Me.colcolUsaHH})
        Me.GrdOperadorBobega.GridControl = Me.Grid
        Me.GrdOperadorBobega.Name = "GrdOperadorBobega"
        Me.GrdOperadorBobega.OptionsFind.AlwaysVisible = True
        '
        'colSelección
        '
        Me.colSelección.Caption = "Asignar"
        Me.colSelección.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.colSelección.FieldName = "Selección"
        Me.colSelección.Name = "colSelección"
        Me.colSelección.Visible = True
        Me.colSelección.VisibleIndex = 0
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'colIdOperadorRec
        '
        Me.colIdOperadorRec.FieldName = "IdOperadorRec"
        Me.colIdOperadorRec.Name = "colIdOperadorRec"
        Me.colIdOperadorRec.OptionsColumn.ReadOnly = True
        '
        'colIdOperadorBodega
        '
        Me.colIdOperadorBodega.FieldName = "IdOperadorBodega"
        Me.colIdOperadorBodega.Name = "colIdOperadorBodega"
        Me.colIdOperadorBodega.OptionsColumn.ReadOnly = True
        '
        'colOperador
        '
        Me.colOperador.Caption = "Operador"
        Me.colOperador.FieldName = "Operador"
        Me.colOperador.Name = "colOperador"
        Me.colOperador.Visible = True
        Me.colOperador.VisibleIndex = 1
        '
        'colcolUsaHH
        '
        Me.colcolUsaHH.Caption = "Usa HH"
        Me.colcolUsaHH.FieldName = "colUsaHH"
        Me.colcolUsaHH.Name = "colcolUsaHH"
        Me.colcolUsaHH.Visible = True
        Me.colcolUsaHH.VisibleIndex = 2
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
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
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 697)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1360, 19)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("3bb47686-189c-4fb8-a839-198f9210017f")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 598)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 99)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1360, 99)
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
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 24)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1352, 71)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'xtrRecepcion
        '
        Me.xtrRecepcion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtrRecepcion.Location = New System.Drawing.Point(0, 143)
        Me.xtrRecepcion.Name = "xtrRecepcion"
        Me.xtrRecepcion.SelectedTabPage = Me.DatosRec
        Me.xtrRecepcion.Size = New System.Drawing.Size(1360, 554)
        Me.xtrRecepcion.TabIndex = 0
        Me.xtrRecepcion.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.DatosRec, Me.DetalleOC, Me.DetRec, Me.Imagenes, Me.DetOp, Me.BarrasRec})
        '
        'DatosRec
        '
        Me.DatosRec.Controls.Add(Me.GrpFactura)
        Me.DatosRec.Controls.Add(Me.GrpProducto)
        Me.DatosRec.Name = "DatosRec"
        Me.DatosRec.Size = New System.Drawing.Size(1354, 526)
        Me.DatosRec.Text = "Datos Recepción"
        '
        'DetalleOC
        '
        Me.DetalleOC.Controls.Add(Me.GrpDetalle)
        Me.DetalleOC.Name = "DetalleOC"
        Me.DetalleOC.Size = New System.Drawing.Size(1354, 526)
        Me.DetalleOC.Text = "Detalle"
        '
        'DetRec
        '
        Me.DetRec.Controls.Add(Me.GrpDetalleRecepcion)
        Me.DetRec.Name = "DetRec"
        Me.DetRec.Size = New System.Drawing.Size(1354, 526)
        Me.DetRec.Text = "Detalle Recepción"
        '
        'Imagenes
        '
        Me.Imagenes.Controls.Add(Me.GrpImagen)
        Me.Imagenes.Name = "Imagenes"
        Me.Imagenes.Size = New System.Drawing.Size(1354, 526)
        Me.Imagenes.Text = "Imagenes"
        '
        'DetOp
        '
        Me.DetOp.Controls.Add(Me.GrpOperadorBodega)
        Me.DetOp.Name = "DetOp"
        Me.DetOp.Size = New System.Drawing.Size(1354, 526)
        Me.DetOp.Text = "Detalle de Operadores"
        '
        'BarrasRec
        '
        Me.BarrasRec.Controls.Add(Me.GroupControl1)
        Me.BarrasRec.Name = "BarrasRec"
        Me.BarrasRec.Size = New System.Drawing.Size(1354, 526)
        Me.BarrasRec.Text = "Barras recepción"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.ToolStrip2)
        Me.GroupControl1.Controls.Add(Me.dgridBarrasRec)
        Me.GroupControl1.Controls.Add(Me.Panel3)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1354, 526)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Lista de barras a imprimir"
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdMarcarTodos, Me.cmdDesmarcarTodos})
        Me.ToolStrip2.Location = New System.Drawing.Point(2, 20)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(1350, 25)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip4"
        '
        'cmdMarcarTodos
        '
        Me.cmdMarcarTodos.Image = Global.TOMWMS.My.Resources.Resources.cheked
        Me.cmdMarcarTodos.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdMarcarTodos.Name = "cmdMarcarTodos"
        Me.cmdMarcarTodos.Size = New System.Drawing.Size(97, 22)
        Me.cmdMarcarTodos.Text = "Marcar todos"
        '
        'cmdDesmarcarTodos
        '
        Me.cmdDesmarcarTodos.Image = Global.TOMWMS.My.Resources.Resources.unchecked
        Me.cmdDesmarcarTodos.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDesmarcarTodos.Name = "cmdDesmarcarTodos"
        Me.cmdDesmarcarTodos.Size = New System.Drawing.Size(116, 22)
        Me.cmdDesmarcarTodos.Text = "Desmarcar todos"
        '
        'dgridBarrasRec
        '
        Me.dgridBarrasRec.AllowUserToDeleteRows = false
        Me.dgridBarrasRec.AllowUserToResizeRows = false
        Me.dgridBarrasRec.BackgroundColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle22.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle22.Font = New System.Drawing.Font("Tahoma", 8.25!)
        DataGridViewCellStyle22.ForeColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgridBarrasRec.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle22
        Me.dgridBarrasRec.ColumnHeadersHeight = 40
        Me.dgridBarrasRec.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColIdStockRecBarras, Me.ColImprimir, Me.colLicPlate, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewComboBoxColumn1, Me.DataGridViewComboBoxColumn2, Me.colCantidadBarras, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn10, Me.DataGridViewComboBoxColumn3, Me.DataGridViewTextBoxColumn11, Me.colFechaProduccion})
        Me.dgridBarrasRec.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgridBarrasRec.EnableHeadersVisualStyles = false
        Me.dgridBarrasRec.GridColor = System.Drawing.Color.Navy
        Me.dgridBarrasRec.Location = New System.Drawing.Point(2, 61)
        Me.dgridBarrasRec.MultiSelect = false
        Me.dgridBarrasRec.Name = "dgridBarrasRec"
        DataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle25.Font = New System.Drawing.Font("Tahoma", 8.25!)
        DataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgridBarrasRec.RowHeadersDefaultCellStyle = DataGridViewCellStyle25
        Me.dgridBarrasRec.RowHeadersVisible = false
        Me.dgridBarrasRec.RowHeadersWidth = 60
        Me.dgridBarrasRec.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgridBarrasRec.Size = New System.Drawing.Size(1350, 430)
        Me.dgridBarrasRec.TabIndex = 1
        '
        'ColIdStockRecBarras
        '
        Me.ColIdStockRecBarras.HeaderText = "IdStockRec"
        Me.ColIdStockRecBarras.Name = "ColIdStockRecBarras"
        Me.ColIdStockRecBarras.ReadOnly = true
        Me.ColIdStockRecBarras.Visible = false
        '
        'ColImprimir
        '
        Me.ColImprimir.HeaderText = "Imprimir"
        Me.ColImprimir.Name = "ColImprimir"
        '
        'colLicPlate
        '
        Me.colLicPlate.HeaderText = "Pallet_ID"
        Me.colLicPlate.Name = "colLicPlate"
        Me.colLicPlate.ReadOnly = true
        Me.colLicPlate.Width = 150
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Código"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 97
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Producto"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = true
        Me.DataGridViewTextBoxColumn3.Width = 250
        '
        'DataGridViewComboBoxColumn1
        '
        Me.DataGridViewComboBoxColumn1.HeaderText = "Presentación"
        Me.DataGridViewComboBoxColumn1.Name = "DataGridViewComboBoxColumn1"
        Me.DataGridViewComboBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewComboBoxColumn1.Width = 150
        '
        'DataGridViewComboBoxColumn2
        '
        Me.DataGridViewComboBoxColumn2.HeaderText = "U.M. Bas"
        Me.DataGridViewComboBoxColumn2.Name = "DataGridViewComboBoxColumn2"
        Me.DataGridViewComboBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewComboBoxColumn2.Width = 97
        '
        'colCantidadBarras
        '
        DataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle23.Format = "N2"
        DataGridViewCellStyle23.NullValue = "0"
        Me.colCantidadBarras.DefaultCellStyle = DataGridViewCellStyle23
        Me.colCantidadBarras.HeaderText = "Cantidad"
        Me.colCantidadBarras.Name = "colCantidadBarras"
        Me.colCantidadBarras.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colCantidadBarras.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colCantidadBarras.Width = 97
        '
        'DataGridViewTextBoxColumn5
        '
        DataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle24.Format = "N2"
        DataGridViewCellStyle24.NullValue = "0"
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle24
        Me.DataGridViewTextBoxColumn5.HeaderText = "Peso"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = true
        Me.DataGridViewTextBoxColumn5.Width = 96
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "Fecha Vencimiento"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.Width = 97
        '
        'DataGridViewComboBoxColumn3
        '
        Me.DataGridViewComboBoxColumn3.HeaderText = "Estado"
        Me.DataGridViewComboBoxColumn3.Name = "DataGridViewComboBoxColumn3"
        Me.DataGridViewComboBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewComboBoxColumn3.Width = 96
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "Lote"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.Width = 97
        '
        'colFechaProduccion
        '
        Me.colFechaProduccion.HeaderText = "Fecha Producción"
        Me.colFechaProduccion.Name = "colFechaProduccion"
        Me.colFechaProduccion.ReadOnly = true
        Me.colFechaProduccion.Width = 97
        '
        'Panel3
        '
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(2, 491)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1350, 33)
        Me.Panel3.TabIndex = 2
        '
        'frmPreIngreso
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1360, 747)
        Me.Controls.Add(Me.xtrRecepcion)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = true
        Me.Name = "frmPreIngreso"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Pre-ingreso"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_agrDateEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.User_agrTextEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_modDateEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.User_modTextEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView4,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GrpProducto,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpProducto.ResumeLayout(false)
        CType(Me.GrpTIpoTransaccion,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpTIpoTransaccion.ResumeLayout(false)
        Me.GrpTIpoTransaccion.PerformLayout
        CType(Me.txtDescripcionTR.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtIdTipoTR.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GrpTarea,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpTarea.ResumeLayout(false)
        CType(Me.dtmFechaTarea.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dtmFechaTarea.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GroupControl10,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupControl10.ResumeLayout(false)
        Me.GroupControl10.PerformLayout
        CType(Me.GroupControl9,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupControl9.ResumeLayout(false)
        Me.GroupControl9.PerformLayout
        CType(Me.GrpObservacion,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpObservacion.ResumeLayout(false)
        Me.GrpObservacion.PerformLayout
        CType(Me.GrpParametrosIngreso,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpParametrosIngreso.ResumeLayout(false)
        Me.GrpParametrosIngreso.PerformLayout
        CType(Me.txtNoDocumento.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkParaPorCodigo.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkMuestraCosto.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkEscanear.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkRecepcionManual.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkTomarFoto.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GrpAsignacionTransaccion,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpAsignacionTransaccion.ResumeLayout(false)
        Me.GrpAsignacionTransaccion.PerformLayout
        CType(Me.cmbMuelle.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtNombreUbicacion.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtIdUbicacion.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GrpTransaccion,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpTransaccion.ResumeLayout(false)
        Me.GrpTransaccion.PerformLayout
        CType(Me.dtmFechaRecepcion.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dtmFechaRecepcion.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cmbPropietario.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cmbBodega.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtOC.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtIdOrdenCompra.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GrpFactura,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpFactura.ResumeLayout(false)
        CType(Me.grdListaFactura,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GrpDetalle,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpDetalle.ResumeLayout(false)
        Me.Panel2.ResumeLayout(false)
        CType(Me.DgridOC,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GrpDetalleRecepcion,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpDetalleRecepcion.ResumeLayout(false)
        Me.GrpDetalleRecepcion.PerformLayout
        CType(Me.DgridDetalleRec,System.ComponentModel.ISupportInitialize).EndInit
        Me.ToolStrip1.ResumeLayout(false)
        Me.ToolStrip1.PerformLayout
        Me.Panel1.ResumeLayout(false)
        CType(Me.GrpImagen,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpImagen.ResumeLayout(false)
        Me.GrpImagen.PerformLayout
        CType(Me.PicImg,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GroupControl4,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupControl4.ResumeLayout(false)
        Me.GroupControl4.PerformLayout
        CType(Me.GrdImagen,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridViewImg,System.ComponentModel.ISupportInitialize).EndInit
        Me.ToolStrip.ResumeLayout(false)
        Me.ToolStrip.PerformLayout
        CType(Me.GrpOperadorBodega,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpOperadorBodega.ResumeLayout(false)
        CType(Me.GroupControl13,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupControl13.ResumeLayout(false)
        CType(Me.Grid,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.DataBindingSource,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.DsOrdenCompraRecepcionOperador,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GrdOperadorBobega,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dkRecepcion,System.ComponentModel.ISupportInitialize).EndInit
        Me.hideContainerBottom.ResumeLayout(false)
        Me.DockPanel1.ResumeLayout(false)
        Me.DockPanel1_Container.ResumeLayout(false)
        Me.DockPanel1_Container.PerformLayout
        CType(Me.xtrRecepcion,System.ComponentModel.ISupportInitialize).EndInit
        Me.xtrRecepcion.ResumeLayout(false)
        Me.DatosRec.ResumeLayout(false)
        Me.DetalleOC.ResumeLayout(false)
        Me.DetRec.ResumeLayout(false)
        Me.Imagenes.ResumeLayout(false)
        Me.DetOp.ResumeLayout(false)
        Me.BarrasRec.ResumeLayout(false)
        CType(Me.GroupControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupControl1.ResumeLayout(false)
        Me.GroupControl1.PerformLayout
        Me.ToolStrip2.ResumeLayout(false)
        Me.ToolStrip2.PerformLayout
        CType(Me.dgridBarrasRec,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

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
    Friend WithEvents GrpProducto As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblC As System.Windows.Forms.Label
    Friend WithEvents chkEscanear As DevExpress.XtraEditors.CheckEdit
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
    Friend WithEvents chkMuestraCosto As DevExpress.XtraEditors.CheckEdit
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
    Friend WithEvents DgridOC As System.Windows.Forms.DataGridView
    Friend WithEvents GrpDetalleRecepcion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblTotalR As System.Windows.Forms.Label
    Friend WithEvents lblCantidadR As System.Windows.Forms.Label
    Friend WithEvents lblCostoR As System.Windows.Forms.Label
    Friend WithEvents GrpTIpoTransaccion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtDescripcionTR As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkTipoT As System.Windows.Forms.LinkLabel
    Friend WithEvents txtIdTipoTR As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GrpOperadorBodega As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl13 As DevExpress.XtraEditors.GroupControl
    Private WithEvents Grid As DevExpress.XtraGrid.GridControl
    Private WithEvents GrdOperadorBobega As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cmdFinalizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblEstado As System.Windows.Forms.Label
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
    Friend WithEvents DgridDetalleRec As System.Windows.Forms.DataGridView
    Friend WithEvents DataBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsOrdenCompraRecepcionOperador As DsOrdenCompraRecepcionOperador
    Friend WithEvents colSelección As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdOperadorRec As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdOperadorBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOperador As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colcolUsaHH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lblTotal As Label
    Friend WithEvents lblCantidad As Label
    Friend WithEvents lblCosto As Label
    Friend WithEvents cmdAgregarProducto As ToolStripButton
    Friend WithEvents cmdVerParametros As ToolStripButton
    Friend WithEvents lblPesoR As Label
    Friend WithEvents lblPesoOC As Label
    Friend WithEvents dkRecepcion As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents xtrRecepcion As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents DatosRec As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents DetalleOC As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents DetRec As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Imagenes As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents DetOp As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbMuelle As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblMotivoAnulacion As Label
    Friend WithEvents lblDiagonal As Label
    Friend WithEvents lblId As Label
    Friend WithEvents dtmFechaRecepcion As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtmFechaTarea As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RibbonStatusBar1 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents NoLinea As DataGridViewTextBoxColumn
    Friend WithEvents CodigoProducto As DataGridViewTextBoxColumn
    Friend WithEvents Producto As DataGridViewTextBoxColumn
    Friend WithEvents Presentacion As DataGridViewComboBoxColumn
    Friend WithEvents UnidadMedida As DataGridViewComboBoxColumn
    Friend WithEvents Arancel As DataGridViewComboBoxColumn
    Friend WithEvents Cantidad As DataGridViewTextBoxColumn
    Friend WithEvents PesoOC As DataGridViewTextBoxColumn
    Friend WithEvents Costo As DataGridViewTextBoxColumn
    Friend WithEvents Total As DataGridViewTextBoxColumn
    Friend WithEvents IdProducto As DataGridViewTextBoxColumn
    Friend WithEvents IsNew As DataGridViewCheckBoxColumn
    Friend WithEvents IdOrdenCompraEnc As DataGridViewTextBoxColumn
    Friend WithEvents IdOrdenCompraDet As DataGridViewTextBoxColumn
    Friend WithEvents Key As DataGridViewTextBoxColumn
    Friend WithEvents IsNewRecepcion As DataGridViewCheckBoxColumn
    Friend WithEvents Factor1 As DataGridViewTextBoxColumn
    Friend WithEvents AtributoVariante1 As DataGridViewTextBoxColumn
    Friend WithEvents lblSEscanearUbicRec As Label
    Friend WithEvents lblTomarFotos As Label
    Friend WithEvents lblRecManual As Label
    Friend WithEvents lblParaPorCodigo As Label
    Friend WithEvents lblMuestraCosto As Label
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
    Friend WithEvents BarSubItem1 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuDocumentoPreIngreso As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuImprimirBarras As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarrasRec As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dgridBarrasRec As DataGridView
    Friend WithEvents Panel3 As Panel
    Friend WithEvents ColIdStockRecBarras As DataGridViewTextBoxColumn
    Friend WithEvents ColImprimir As DataGridViewCheckBoxColumn
    Friend WithEvents colLicPlate As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents colCantidadBarras As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As DataGridViewTextBoxColumn
    Friend WithEvents colFechaProduccion As DataGridViewTextBoxColumn
    Friend WithEvents ToolStrip2 As ToolStrip
    Friend WithEvents cmdMarcarTodos As ToolStripButton
    Friend WithEvents cmdDesmarcarTodos As ToolStripButton
End Class
