<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmProducto
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
            If pBeProducto IsNot Nothing Then
                pBeProducto.Dispose()
                pBeProducto = Nothing
            End If
            If pObjProductoSustituto IsNot Nothing Then
                pObjProductoSustituto.Dispose()
                pObjProductoSustituto = Nothing
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
        Dim Label49 As System.Windows.Forms.Label
        Dim Label36 As System.Windows.Forms.Label
        Dim Label35 As System.Windows.Forms.Label
        Dim Label23 As System.Windows.Forms.Label
        Dim Label33 As System.Windows.Forms.Label
        Dim Label31 As System.Windows.Forms.Label
        Dim Label24 As System.Windows.Forms.Label
        Dim Label25 As System.Windows.Forms.Label
        Dim Label27 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label54 As System.Windows.Forms.Label
        Dim Label53 As System.Windows.Forms.Label
        Dim Label52 As System.Windows.Forms.Label
        Dim Label48 As System.Windows.Forms.Label
        Dim Label50 As System.Windows.Forms.Label
        Dim Label As System.Windows.Forms.Label
        Dim lblCajasPorCama As System.Windows.Forms.Label
        Dim lblPeso As System.Windows.Forms.Label
        Dim lblAlto As System.Windows.Forms.Label
        Dim lblLargo As System.Windows.Forms.Label
        Dim lblAncho As System.Windows.Forms.Label
        Dim Label18 As System.Windows.Forms.Label
        Dim lblFactor As System.Windows.Forms.Label
        Dim Label19 As System.Windows.Forms.Label
        Dim Label20 As System.Windows.Forms.Label
        Dim Label28 As System.Windows.Forms.Label
        Dim Label43 As System.Windows.Forms.Label
        Dim Label44 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim NombreLabel As System.Windows.Forms.Label
        Dim Label29 As System.Windows.Forms.Label
        Dim ProductoLabel As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label13 As System.Windows.Forms.Label
        Dim Label34 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim lblExistenciaMinima As System.Windows.Forms.Label
        Dim lblExistenciaMaxima As System.Windows.Forms.Label
        Dim lblCosto As System.Windows.Forms.Label
        Dim lblPrecio As System.Windows.Forms.Label
        Dim lblTipoRotacion As System.Windows.Forms.Label
        Dim lblCapturarPeso As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim lblCapturarTemperatura As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label17 As System.Windows.Forms.Label
        Dim lblGeneraLote As System.Windows.Forms.Label
        Dim lblEsMatPrima As System.Windows.Forms.Label
        Dim lblControlLote As System.Windows.Forms.Label
        Dim lblEsKit As System.Windows.Forms.Label
        Dim Label26 As System.Windows.Forms.Label
        Dim lblCapturaFechaManufactura As System.Windows.Forms.Label
        Dim Label47 As System.Windows.Forms.Label
        Dim lblSerializado As System.Windows.Forms.Label
        Dim Label21 As System.Windows.Forms.Label
        Dim lblArancel As System.Windows.Forms.Label
        Dim lblCapturaArancel As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label15 As System.Windows.Forms.Label
        Dim Label16 As System.Windows.Forms.Label
        Dim Label46 As System.Windows.Forms.Label
        Dim lblEsHW As System.Windows.Forms.Label
        Dim IdPropietarioLabel As System.Windows.Forms.Label
        Dim lblCodigo As System.Windows.Forms.Label
        Dim lblNombre As System.Windows.Forms.Label
        Dim lblCodigoBarra As System.Windows.Forms.Label
        Dim ImagenLabel As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim lblSimbologia As System.Windows.Forms.Label
        Dim Label37 As System.Windows.Forms.Label
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProducto))
        Dim lblPresentacionPallet As System.Windows.Forms.Label
        Dim lblPrdPd As System.Windows.Forms.Label
        Dim Label56 As System.Windows.Forms.Label
        Dim lblTipoManufactura As System.Windows.Forms.Label
        Dim lblPresentacionReabastecerCon As System.Windows.Forms.Label
        Dim Label45 As System.Windows.Forms.Label
        Dim Label39 As System.Windows.Forms.Label
        Dim lblBodegaRellenado As System.Windows.Forms.Label
        Dim Label22 As System.Windows.Forms.Label
        Dim Label42 As System.Windows.Forms.Label
        Dim Label40 As System.Windows.Forms.Label
        Dim Label38 As System.Windows.Forms.Label
        Dim Code128Generator1 As DevExpress.XtraPrinting.BarCode.Code128Generator = New DevExpress.XtraPrinting.BarCode.Code128Generator()
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode3 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode4 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode5 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode6 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode7 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode8 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode9 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode10 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode11 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.GridView10 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grdPStock = New DevExpress.XtraGrid.GridControl()
        Me.EncabezadoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsResumenStockBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsResumenStock = New TOMWMS.DsResumenStock()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colIdProducto = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPropietario = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCódigo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colProducto = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCódigo_Barra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPresentación = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colUM_Bas = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCantidadUMBas = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCantidadPresentación = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.mnuEliminarProducto = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdUbicacion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuDesactivar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPageCategory1 = New DevExpress.XtraBars.Ribbon.RibbonPageCategory()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPage2 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.DataBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsProducto = New TOMWMS.DsProducto()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage3 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.TabDatos = New DevExpress.XtraTab.XtraTabControl()
        Me.TabProducto = New DevExpress.XtraTab.XtraTabPage()
        Me.GrpProducto = New DevExpress.XtraEditors.GroupControl()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.cmbTipoManufactura = New DevExpress.XtraEditors.LookUpEdit()
        Me.lcmbParametroB = New DevExpress.XtraEditors.LookUpEdit()
        Me.lnkParametroB = New System.Windows.Forms.LinkLabel()
        Me.lcmbParametroA = New DevExpress.XtraEditors.LookUpEdit()
        Me.lnkParametroA = New System.Windows.Forms.LinkLabel()
        Me.lcmbUnidadMedidaCobro = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtIdTipoProducto = New DevExpress.XtraEditors.LookUpEdit()
        Me.lcmbMarca = New DevExpress.XtraEditors.LookUpEdit()
        Me.lcmbFamilia = New DevExpress.XtraEditors.LookUpEdit()
        Me.lcmbClasificacion = New DevExpress.XtraEditors.LookUpEdit()
        Me.lcmbUnidadMedidaBasica = New DevExpress.XtraEditors.LookUpEdit()
        Me.lnkUMCobro = New System.Windows.Forms.LinkLabel()
        Me.lnkUnidadMedida = New System.Windows.Forms.LinkLabel()
        Me.lcmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.lnkClasificacion = New System.Windows.Forms.LinkLabel()
        Me.lnkFamilia = New System.Windows.Forms.LinkLabel()
        Me.lblC = New DevExpress.XtraEditors.TextEdit()
        Me.lnkMarca = New System.Windows.Forms.LinkLabel()
        Me.lnkUMPrecio = New System.Windows.Forms.LinkLabel()
        Me.txtIdUmPrecio = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreUMPrecio = New DevExpress.XtraEditors.TextEdit()
        Me.lnkTipoProducto = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel()
        Me.TextEdit5 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit6 = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.TextEdit3 = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigoBarra = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit4 = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigo = New DevExpress.XtraEditors.TextEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.grpDimensionesUMBas = New DevExpress.XtraEditors.GroupControl()
        Me.txtMargen = New System.Windows.Forms.NumericUpDown()
        Me.lblMargen = New System.Windows.Forms.Label()
        Me.txtAnchoUB = New System.Windows.Forms.NumericUpDown()
        Me.lblAnchoPr = New System.Windows.Forms.Label()
        Me.lblAltoPr = New System.Windows.Forms.Label()
        Me.txtLargoUB = New System.Windows.Forms.NumericUpDown()
        Me.txtAltoUB = New System.Windows.Forms.NumericUpDown()
        Me.lblLargoPr = New System.Windows.Forms.Label()
        Me.grpImagenProducto = New DevExpress.XtraEditors.GroupControl()
        Me.picFoto = New System.Windows.Forms.PictureBox()
        Me.btnExaminar = New DevExpress.XtraEditors.SimpleButton()
        Me.cmbSymbology = New DevExpress.XtraEditors.LookUpEdit()
        Me.Bcc = New DevExpress.XtraEditors.BarCodeControl()
        Me.cmbEtiqueta = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblEtiqueta = New System.Windows.Forms.Label()
        Me.TabAtributo = New DevExpress.XtraTab.XtraTabPage()
        Me.GrpAtributo = New DevExpress.XtraEditors.GroupControl()
        Me.chkGeneraLicAutoP = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupControl14 = New DevExpress.XtraEditors.GroupControl()
        Me.picFormulaIndiceRotacionWMS = New System.Windows.Forms.PictureBox()
        Me.lblIndiceRotacionRef = New DevExpress.XtraEditors.LabelControl()
        Me.lblDiasInventarioPromedio = New DevExpress.XtraEditors.LabelControl()
        Me.lblIndiceRotacionWMS = New DevExpress.XtraEditors.LabelControl()
        Me.cmbIndiceRotacionWMS = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtDiasPromedioInventario = New System.Windows.Forms.NumericUpDown()
        Me.cmbIndiceRotacion = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbCamara = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbTipoRotacion = New DevExpress.XtraEditors.LookUpEdit()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.chkEsHW = New DevExpress.XtraEditors.CheckEdit()
        Me.txtNoParte = New DevExpress.XtraEditors.TextEdit()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.txtCicloVida = New System.Windows.Forms.NumericUpDown()
        Me.chkControlVencimiento = New DevExpress.XtraEditors.CheckEdit()
        Me.txtTolerancia = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmbArancel = New DevExpress.XtraEditors.LookUpEdit()
        Me.chkCapturaArancel = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmbPerfilSerializado = New DevExpress.XtraEditors.LookUpEdit()
        Me.chkSerializado = New DevExpress.XtraEditors.CheckEdit()
        Me.txtNoSerie = New DevExpress.XtraEditors.TextEdit()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkFechaManufactura = New DevExpress.XtraEditors.CheckEdit()
        Me.chkCapturarAniada = New DevExpress.XtraEditors.CheckEdit()
        Me.chkEsKit = New DevExpress.XtraEditors.CheckEdit()
        Me.chkEsMateriaPrima = New DevExpress.XtraEditors.CheckEdit()
        Me.chkControlLote = New DevExpress.XtraEditors.CheckEdit()
        Me.chkGeneraLote = New DevExpress.XtraEditors.CheckEdit()
        Me.GrpTemperatura = New System.Windows.Forms.GroupBox()
        Me.txtTemperaturaTolerancia = New System.Windows.Forms.NumericUpDown()
        Me.txtTemperaturaReferencia = New System.Windows.Forms.NumericUpDown()
        Me.chkCapturaTemperatura = New DevExpress.XtraEditors.CheckEdit()
        Me.GrpPeso = New System.Windows.Forms.GroupBox()
        Me.txtPesoTolerancia = New System.Windows.Forms.NumericUpDown()
        Me.txtPesoReferencia = New System.Windows.Forms.NumericUpDown()
        Me.chkCapturarPeso = New DevExpress.XtraEditors.CheckEdit()
        Me.txtPrecio = New System.Windows.Forms.NumericUpDown()
        Me.txtCosto = New System.Windows.Forms.NumericUpDown()
        Me.txtExistenciaMaxima = New System.Windows.Forms.NumericUpDown()
        Me.txtExitenciaMinima = New System.Windows.Forms.NumericUpDown()
        Me.TabParametros = New DevExpress.XtraTab.XtraTabPage()
        Me.GprParametro = New DevExpress.XtraEditors.GroupControl()
        Me.cmbParametro = New DevExpress.XtraEditors.LookUpEdit()
        Me.chkActivarParametro = New DevExpress.XtraEditors.CheckEdit()
        Me.ToolStripP = New System.Windows.Forms.ToolStrip()
        Me.cmdNewP = New System.Windows.Forms.ToolStripButton()
        Me.cmdSaveP = New System.Windows.Forms.ToolStripButton()
        Me.cmdDesactivarParametro = New System.Windows.Forms.ToolStripButton()
        Me.cmdNuevoParametro = New DevExpress.XtraEditors.SimpleButton()
        Me.rdCapturarSiempre = New System.Windows.Forms.RadioButton()
        Me.rdCapturarUnaVez = New System.Windows.Forms.RadioButton()
        Me.GrpParametro = New DevExpress.XtraEditors.GroupControl()
        Me.DgridParametros = New DevExpress.XtraGrid.GridControl()
        Me.GridViewP = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView9 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.chkActivoParametro = New DevExpress.XtraEditors.CheckEdit()
        Me.txtTipo = New DevExpress.XtraEditors.TextEdit()
        Me.TabCodigoBarra = New DevExpress.XtraTab.XtraTabPage()
        Me.GrpCodigoBarra = New DevExpress.XtraEditors.GroupControl()
        Me.cmbProveedor = New DevExpress.XtraEditors.LookUpEdit()
        Me.GrdCodigoBarra = New DevExpress.XtraGrid.GridControl()
        Me.GridViewCB = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView8 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.chkActivarCB = New DevExpress.XtraEditors.CheckEdit()
        Me.ToolStripC = New System.Windows.Forms.ToolStrip()
        Me.cmdNewC = New System.Windows.Forms.ToolStripButton()
        Me.cmdSaveC = New System.Windows.Forms.ToolStripButton()
        Me.cmdDesactivarCodigoBarra = New System.Windows.Forms.ToolStripButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.chkActivoCB = New DevExpress.XtraEditors.CheckEdit()
        Me.txtCodigoBarraL = New DevExpress.XtraEditors.TextEdit()
        Me.TabProductoBodega = New DevExpress.XtraTab.XtraTabPage()
        Me.GrpPB = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.dgridProductoBodega = New DevExpress.XtraGrid.GridControl()
        Me.GrdProductoBodega = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSeleccion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colIdBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IdProductoBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IdInterno = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridView7 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TabPresentacion = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GrpPresentacion = New DevExpress.XtraEditors.GroupControl()
        Me.cmbEtiquetaPresentacion = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblEtiquetaPresentacion = New System.Windows.Forms.Label()
        Me.lblSistema = New System.Windows.Forms.Label()
        Me.chkSistema = New DevExpress.XtraEditors.CheckEdit()
        Me.chkGeneraLPAuto = New DevExpress.XtraEditors.CheckEdit()
        Me.chkPermitirPaletizar = New DevExpress.XtraEditors.CheckEdit()
        Me.ChkEsPallet = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.txtMaximoPeso = New System.Windows.Forms.NumericUpDown()
        Me.txtMinimoPeso = New System.Windows.Forms.NumericUpDown()
        Me.txtMinimoExistencia = New System.Windows.Forms.NumericUpDown()
        Me.txtMaximoExistencia = New System.Windows.Forms.NumericUpDown()
        Me.grpConfigPallet = New System.Windows.Forms.GroupBox()
        Me.cmbPresentacionPallet = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtCamasPorTarima = New System.Windows.Forms.NumericUpDown()
        Me.txtCajasPorCama = New System.Windows.Forms.NumericUpDown()
        Me.lblY = New System.Windows.Forms.Label()
        Me.lblX = New System.Windows.Forms.Label()
        Me.chkActivarPR = New DevExpress.XtraEditors.CheckEdit()
        Me.ToolStripPR = New System.Windows.Forms.ToolStrip()
        Me.cmdNewPR = New System.Windows.Forms.ToolStripButton()
        Me.cmdSavePR = New System.Windows.Forms.ToolStripButton()
        Me.cmdDesactivarPresentacion = New System.Windows.Forms.ToolStripButton()
        Me.txtCodigoBarraPresentacion = New DevExpress.XtraEditors.TextEdit()
        Me.txtInfo = New System.Windows.Forms.TextBox()
        Me.txtFactor = New System.Windows.Forms.NumericUpDown()
        Me.chkImprimeBarra = New DevExpress.XtraEditors.CheckEdit()
        Me.txtAncho = New System.Windows.Forms.NumericUpDown()
        Me.txtLargo = New System.Windows.Forms.NumericUpDown()
        Me.txtNombrePresentacion = New DevExpress.XtraEditors.TextEdit()
        Me.txtAlto = New System.Windows.Forms.NumericUpDown()
        Me.txtPeso = New System.Windows.Forms.NumericUpDown()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.dGridPresentacion = New DevExpress.XtraGrid.GridControl()
        Me.GrdPresentacion = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.chkActivoPR = New DevExpress.XtraEditors.CheckEdit()
        Me.tabPresentacionTarima = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl8 = New DevExpress.XtraEditors.GroupControl()
        Me.cmbTipoTarima = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbPresentacionTarima = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtCantidadPorCama = New System.Windows.Forms.NumericUpDown()
        Me.ToolStripPT = New System.Windows.Forms.ToolStrip()
        Me.cmdNewPT = New System.Windows.Forms.ToolStripButton()
        Me.cmdSavePT = New System.Windows.Forms.ToolStripButton()
        Me.cmdDesactivarPresentacionTarima = New System.Windows.Forms.ToolStripButton()
        Me.chkActivoPT2 = New DevExpress.XtraEditors.CheckEdit()
        Me.txtCantidad = New System.Windows.Forms.NumericUpDown()
        Me.lblNombreProductoPT = New System.Windows.Forms.Label()
        Me.GroupControl9 = New DevExpress.XtraEditors.GroupControl()
        Me.GridPresentacionTarima = New DevExpress.XtraGrid.GridControl()
        Me.ViewPT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.chkActivoPT = New DevExpress.XtraEditors.CheckEdit()
        Me.TabProductoSustituto = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.cmbPresentacionR = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbProductoP = New DevExpress.XtraEditors.LookUpEdit()
        Me.chkActivarPS = New DevExpress.XtraEditors.CheckEdit()
        Me.ToolStripPS = New System.Windows.Forms.ToolStrip()
        Me.cmdNewPS = New System.Windows.Forms.ToolStripButton()
        Me.cmdSavePS = New System.Windows.Forms.ToolStripButton()
        Me.cmdDesactivarProductoSustituto = New System.Windows.Forms.ToolStripButton()
        Me.lblNombreProductoO = New System.Windows.Forms.Label()
        Me.lnkProductoR = New System.Windows.Forms.LinkLabel()
        Me.txtIdProductoR = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombrePR = New DevExpress.XtraEditors.TextEdit()
        Me.GrpProductoReeemplazo = New DevExpress.XtraEditors.GroupControl()
        Me.GrdProductoS = New DevExpress.XtraGrid.GridControl()
        Me.GridViewProductoS = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.chkActivoPS = New DevExpress.XtraEditors.CheckEdit()
        Me.TabProductoRellenado = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl()
        Me.splitContainerReabasto = New System.Windows.Forms.SplitContainer()
        Me.grpReabastecimiento = New DevExpress.XtraEditors.GroupControl()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.cmbOperadorDefecto = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.lblIdRellenado = New DevExpress.XtraEditors.LabelControl()
        Me.optNotificar = New System.Windows.Forms.RadioButton()
        Me.cmbPresentacionPR = New DevExpress.XtraEditors.LookUpEdit()
        Me.lnklblUMBasRellenado = New System.Windows.Forms.LinkLabel()
        Me.cmbProductoEstado = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtNombreUbicacion = New DevExpress.XtraEditors.TextEdit()
        Me.optGenerarAutomaticamente = New System.Windows.Forms.RadioButton()
        Me.txtIdUnidadMedidaBasicaRellenado = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdUbicacion = New DevExpress.XtraEditors.TextEdit()
        Me.lblNombreProductoPR = New System.Windows.Forms.Label()
        Me.txtNombreUMBasRellenado = New DevExpress.XtraEditors.TextEdit()
        Me.chkActivarProductoPRL = New DevExpress.XtraEditors.CheckEdit()
        Me.txtMaximoPicking = New System.Windows.Forms.NumericUpDown()
        Me.cmbBodegaRellenado = New DevExpress.XtraEditors.LookUpEdit()
        Me.lnkUbicacion = New System.Windows.Forms.LinkLabel()
        Me.txtMinimoPicking = New System.Windows.Forms.NumericUpDown()
        Me.lblReabastecerCon = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.cmbPresentacionAbastecerCon = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtNombreUMBasReabastecerCon = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdUMBasReabastecerCon = New DevExpress.XtraEditors.TextEdit()
        Me.GridProductoRellenado = New DevExpress.XtraGrid.GridControl()
        Me.ViewProductoRellenado = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.chkProductoPRL = New DevExpress.XtraEditors.CheckEdit()
        Me.ToolStripPRL = New System.Windows.Forms.ToolStrip()
        Me.cmdNewPRL = New System.Windows.Forms.ToolStripButton()
        Me.cmdSavePRL = New System.Windows.Forms.ToolStripButton()
        Me.cmdDesactivarProductoRellenado = New System.Windows.Forms.ToolStripButton()
        Me.tabProductoKit = New DevExpress.XtraTab.XtraTabPage()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl12 = New DevExpress.XtraEditors.GroupControl()
        Me.grdPrdKit = New DevExpress.XtraGrid.GridControl()
        Me.GridView11 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl13 = New DevExpress.XtraEditors.GroupControl()
        Me.txtCodPrdHijo = New DevExpress.XtraEditors.TextEdit()
        Me.txtCantPrdHijo = New System.Windows.Forms.NumericUpDown()
        Me.txtNombrePrdHijo = New DevExpress.XtraEditors.TextEdit()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.linklblProductoKit = New System.Windows.Forms.LinkLabel()
        Me.txtNomUMBHijo = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdUMBHijo = New DevExpress.XtraEditors.TextEdit()
        Me.lblProdPadre = New System.Windows.Forms.Label()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.cmdAggPrK = New System.Windows.Forms.ToolStripButton()
        Me.cmdGuardarPrk = New System.Windows.Forms.ToolStripButton()
        Me.cmdEliminarPrk = New System.Windows.Forms.ToolStripButton()
        Me.tabStock = New DevExpress.XtraTab.XtraTabPage()
        Me.Conversion = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl10 = New DevExpress.XtraEditors.GroupControl()
        Me.cmbInversa = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbOriginal = New DevExpress.XtraEditors.LookUpEdit()
        Me.CheckEdit1 = New DevExpress.XtraEditors.CheckEdit()
        Me.chkInverso = New System.Windows.Forms.CheckBox()
        Me.txtFactorConver = New System.Windows.Forms.NumericUpDown()
        Me.chkActivarConver = New DevExpress.XtraEditors.CheckEdit()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.cmdNuevoCn = New System.Windows.Forms.ToolStripButton()
        Me.cmdSaveCn = New System.Windows.Forms.ToolStripButton()
        Me.cmdDesactivarCn = New System.Windows.Forms.ToolStripButton()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.GroupControl11 = New DevExpress.XtraEditors.GroupControl()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.chkActivosCn = New DevExpress.XtraEditors.CheckEdit()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.GrpImagen = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl15 = New DevExpress.XtraEditors.GroupControl()
        Me.GrdImagen = New DevExpress.XtraGrid.GridControl()
        Me.GridViewImg = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.cmdAdd = New System.Windows.Forms.ToolStripButton()
        Me.cmdDelete = New System.Windows.Forms.ToolStripButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.PicImg = New System.Windows.Forms.PictureBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.tabTallaColor = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl7 = New DevExpress.XtraEditors.GroupControl()
        Me.dgridTallaColor = New DevExpress.XtraGrid.GridControl()
        Me.GridView12 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView13 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.dkProducto = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.DetalleBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.NumericChartRangeControlClient1 = New DevExpress.XtraEditors.NumericChartRangeControlClient()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        Label49 = New System.Windows.Forms.Label()
        Label36 = New System.Windows.Forms.Label()
        Label35 = New System.Windows.Forms.Label()
        Label23 = New System.Windows.Forms.Label()
        Label33 = New System.Windows.Forms.Label()
        Label31 = New System.Windows.Forms.Label()
        Label24 = New System.Windows.Forms.Label()
        Label25 = New System.Windows.Forms.Label()
        Label27 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label54 = New System.Windows.Forms.Label()
        Label53 = New System.Windows.Forms.Label()
        Label52 = New System.Windows.Forms.Label()
        Label48 = New System.Windows.Forms.Label()
        Label50 = New System.Windows.Forms.Label()
        Label = New System.Windows.Forms.Label()
        lblCajasPorCama = New System.Windows.Forms.Label()
        lblPeso = New System.Windows.Forms.Label()
        lblAlto = New System.Windows.Forms.Label()
        lblLargo = New System.Windows.Forms.Label()
        lblAncho = New System.Windows.Forms.Label()
        Label18 = New System.Windows.Forms.Label()
        lblFactor = New System.Windows.Forms.Label()
        Label19 = New System.Windows.Forms.Label()
        Label20 = New System.Windows.Forms.Label()
        Label28 = New System.Windows.Forms.Label()
        Label43 = New System.Windows.Forms.Label()
        Label44 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        NombreLabel = New System.Windows.Forms.Label()
        Label29 = New System.Windows.Forms.Label()
        ProductoLabel = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label13 = New System.Windows.Forms.Label()
        Label34 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        lblExistenciaMinima = New System.Windows.Forms.Label()
        lblExistenciaMaxima = New System.Windows.Forms.Label()
        lblCosto = New System.Windows.Forms.Label()
        lblPrecio = New System.Windows.Forms.Label()
        lblTipoRotacion = New System.Windows.Forms.Label()
        lblCapturarPeso = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        lblCapturarTemperatura = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label17 = New System.Windows.Forms.Label()
        lblGeneraLote = New System.Windows.Forms.Label()
        lblEsMatPrima = New System.Windows.Forms.Label()
        lblControlLote = New System.Windows.Forms.Label()
        lblEsKit = New System.Windows.Forms.Label()
        Label26 = New System.Windows.Forms.Label()
        lblCapturaFechaManufactura = New System.Windows.Forms.Label()
        Label47 = New System.Windows.Forms.Label()
        lblSerializado = New System.Windows.Forms.Label()
        Label21 = New System.Windows.Forms.Label()
        lblArancel = New System.Windows.Forms.Label()
        lblCapturaArancel = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label15 = New System.Windows.Forms.Label()
        Label16 = New System.Windows.Forms.Label()
        Label46 = New System.Windows.Forms.Label()
        lblEsHW = New System.Windows.Forms.Label()
        IdPropietarioLabel = New System.Windows.Forms.Label()
        lblCodigo = New System.Windows.Forms.Label()
        lblNombre = New System.Windows.Forms.Label()
        lblCodigoBarra = New System.Windows.Forms.Label()
        ImagenLabel = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        lblSimbologia = New System.Windows.Forms.Label()
        Label37 = New System.Windows.Forms.Label()
        lblPresentacionPallet = New System.Windows.Forms.Label()
        lblPrdPd = New System.Windows.Forms.Label()
        Label56 = New System.Windows.Forms.Label()
        lblTipoManufactura = New System.Windows.Forms.Label()
        lblPresentacionReabastecerCon = New System.Windows.Forms.Label()
        Label45 = New System.Windows.Forms.Label()
        Label39 = New System.Windows.Forms.Label()
        lblBodegaRellenado = New System.Windows.Forms.Label()
        Label22 = New System.Windows.Forms.Label()
        Label42 = New System.Windows.Forms.Label()
        Label40 = New System.Windows.Forms.Label()
        Label38 = New System.Windows.Forms.Label()
        CType(Me.GridView10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EncabezadoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsResumenStockBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsResumenStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabDatos.SuspendLayout()
        Me.TabProducto.SuspendLayout()
        CType(Me.GrpProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpProducto.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.cmbTipoManufactura.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lcmbParametroB.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lcmbParametroA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lcmbUnidadMedidaCobro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdTipoProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lcmbMarca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lcmbFamilia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lcmbClasificacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lcmbUnidadMedidaBasica.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lcmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUmPrecio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreUMPrecio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoBarra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpDimensionesUMBas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDimensionesUMBas.SuspendLayout()
        CType(Me.txtMargen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAnchoUB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLargoUB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAltoUB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpImagenProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpImagenProducto.SuspendLayout()
        CType(Me.picFoto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSymbology.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEtiqueta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabAtributo.SuspendLayout()
        CType(Me.GrpAtributo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpAtributo.SuspendLayout()
        CType(Me.chkGeneraLicAutoP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl14, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl14.SuspendLayout()
        CType(Me.picFormulaIndiceRotacionWMS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbIndiceRotacionWMS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiasPromedioInventario, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbIndiceRotacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCamara.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTipoRotacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        CType(Me.chkEsHW.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoParte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.txtCicloVida, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkControlVencimiento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTolerancia, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.cmbArancel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCapturaArancel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.cmbPerfilSerializado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSerializado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.chkFechaManufactura.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCapturarAniada.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEsKit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEsMateriaPrima.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkControlLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGeneraLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTemperatura.SuspendLayout()
        CType(Me.txtTemperaturaTolerancia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTemperaturaReferencia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCapturaTemperatura.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpPeso.SuspendLayout()
        CType(Me.txtPesoTolerancia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPesoReferencia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCapturarPeso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrecio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCosto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExistenciaMaxima, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExitenciaMinima, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabParametros.SuspendLayout()
        CType(Me.GprParametro, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GprParametro.SuspendLayout()
        CType(Me.cmbParametro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivarParametro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripP.SuspendLayout()
        CType(Me.GrpParametro, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpParametro.SuspendLayout()
        CType(Me.DgridParametros, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivoParametro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabCodigoBarra.SuspendLayout()
        CType(Me.GrpCodigoBarra, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpCodigoBarra.SuspendLayout()
        CType(Me.cmbProveedor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdCodigoBarra, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewCB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivarCB.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripC.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.chkActivoCB.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoBarraL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabProductoBodega.SuspendLayout()
        CType(Me.GrpPB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpPB.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.dgridProductoBodega, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdProductoBodega, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPresentacion.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.GrpPresentacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpPresentacion.SuspendLayout()
        CType(Me.cmbEtiquetaPresentacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSistema.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGeneraLPAuto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermitirPaletizar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkEsPallet.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox7.SuspendLayout()
        CType(Me.txtMaximoPeso, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMinimoPeso, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMinimoExistencia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMaximoExistencia, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpConfigPallet.SuspendLayout()
        CType(Me.cmbPresentacionPallet.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCamasPorTarima, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCajasPorCama, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivarPR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripPR.SuspendLayout()
        CType(Me.txtCodigoBarraPresentacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFactor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkImprimeBarra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAncho, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLargo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombrePresentacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAlto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPeso, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        CType(Me.dGridPresentacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdPresentacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivoPR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPresentacionTarima.SuspendLayout()
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl8.SuspendLayout()
        CType(Me.cmbTipoTarima.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPresentacionTarima.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidadPorCama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripPT.SuspendLayout()
        CType(Me.chkActivoPT2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl9.SuspendLayout()
        CType(Me.GridPresentacionTarima, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewPT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivoPT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabProductoSustituto.SuspendLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.cmbPresentacionR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbProductoP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivarPS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripPS.SuspendLayout()
        CType(Me.txtIdProductoR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombrePR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpProductoReeemplazo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpProductoReeemplazo.SuspendLayout()
        CType(Me.GrdProductoS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewProductoS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivoPS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabProductoRellenado.SuspendLayout()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6.SuspendLayout()
        CType(Me.splitContainerReabasto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitContainerReabasto.Panel1.SuspendLayout()
        Me.splitContainerReabasto.Panel2.SuspendLayout()
        Me.splitContainerReabasto.SuspendLayout()
        CType(Me.grpReabastecimiento, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpReabastecimiento.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.cmbOperadorDefecto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPresentacionPR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbProductoEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUnidadMedidaBasicaRellenado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreUMBasRellenado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivarProductoPRL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMaximoPicking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodegaRellenado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMinimoPicking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPresentacionAbastecerCon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreUMBasReabastecerCon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUMBasReabastecerCon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridProductoRellenado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewProductoRellenado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkProductoPRL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripPRL.SuspendLayout()
        Me.tabProductoKit.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.GroupControl12, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl12.SuspendLayout()
        CType(Me.grdPrdKit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl13.SuspendLayout()
        CType(Me.txtCodPrdHijo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantPrdHijo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombrePrdHijo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNomUMBHijo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUMBHijo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip2.SuspendLayout()
        Me.tabStock.SuspendLayout()
        Me.Conversion.SuspendLayout()
        CType(Me.GroupControl10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl10.SuspendLayout()
        CType(Me.cmbInversa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbOriginal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CheckEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFactorConver, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivarConver.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.GroupControl11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl11.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivosCn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.GrpImagen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpImagen.SuspendLayout()
        CType(Me.GroupControl15, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl15.SuspendLayout()
        CType(Me.GrdImagen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewImg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.PicImg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabTallaColor.SuspendLayout()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl7.SuspendLayout()
        CType(Me.dgridTallaColor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.DetalleBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericChartRangeControlClient1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(96, 22)
        User_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(100, 16)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(96, 54)
        Fec_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(580, 22)
        User_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(580, 54)
        Fec_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'Label49
        '
        Label49.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label49.AutoSize = True
        Label49.Location = New System.Drawing.Point(2074, 33)
        Label49.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label49.Name = "Label49"
        Label49.Size = New System.Drawing.Size(52, 16)
        Label49.TabIndex = 0
        Label49.Text = "Activos:"
        '
        'Label36
        '
        Label36.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label36.AutoSize = True
        Label36.Location = New System.Drawing.Point(40, 85)
        Label36.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label36.Name = "Label36"
        Label36.Size = New System.Drawing.Size(133, 16)
        Label36.TabIndex = 2
        Label36.Text = "Presentación Original:"
        '
        'Label35
        '
        Label35.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label35.AutoSize = True
        Label35.Location = New System.Drawing.Point(40, 116)
        Label35.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label35.Name = "Label35"
        Label35.Size = New System.Drawing.Size(131, 16)
        Label35.TabIndex = 4
        Label35.Text = "Presentación Destino:"
        '
        'Label23
        '
        Label23.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label23.AutoSize = True
        Label23.Location = New System.Drawing.Point(40, 183)
        Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label23.Name = "Label23"
        Label23.Size = New System.Drawing.Size(41, 16)
        Label23.TabIndex = 8
        Label23.Text = "Activo"
        '
        'Label33
        '
        Label33.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label33.AutoSize = True
        Label33.Location = New System.Drawing.Point(40, 149)
        Label33.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label33.Name = "Label33"
        Label33.Size = New System.Drawing.Size(52, 16)
        Label33.TabIndex = 6
        Label33.Text = "Factor: "
        '
        'Label31
        '
        Label31.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label31.AutoSize = True
        Label31.Location = New System.Drawing.Point(2326, 33)
        Label31.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label31.Name = "Label31"
        Label31.Size = New System.Drawing.Size(52, 16)
        Label31.TabIndex = 0
        Label31.Text = "Activos:"
        '
        'Label24
        '
        Label24.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label24.AutoSize = True
        Label24.Location = New System.Drawing.Point(40, 106)
        Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label24.Name = "Label24"
        Label24.Size = New System.Drawing.Size(133, 16)
        Label24.TabIndex = 3
        Label24.Text = "Presentación Original:"
        '
        'Label25
        '
        Label25.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label25.AutoSize = True
        Label25.Location = New System.Drawing.Point(40, 171)
        Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label25.Name = "Label25"
        Label25.Size = New System.Drawing.Size(152, 16)
        Label25.TabIndex = 8
        Label25.Text = "Presentación Reemplazo:"
        '
        'Label27
        '
        Label27.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label27.AutoSize = True
        Label27.Location = New System.Drawing.Point(40, 73)
        Label27.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label27.Name = "Label27"
        Label27.Size = New System.Drawing.Size(110, 16)
        Label27.TabIndex = 1
        Label27.Text = "Producto Original:"
        '
        'Label9
        '
        Label9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(40, 204)
        Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(41, 16)
        Label9.TabIndex = 10
        Label9.Text = "Activo"
        '
        'Label54
        '
        Label54.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label54.AutoSize = True
        Label54.Location = New System.Drawing.Point(2074, 33)
        Label54.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label54.Name = "Label54"
        Label54.Size = New System.Drawing.Size(52, 16)
        Label54.TabIndex = 0
        Label54.Text = "Activos:"
        '
        'Label53
        '
        Label53.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label53.AutoSize = True
        Label53.Location = New System.Drawing.Point(40, 106)
        Label53.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label53.Name = "Label53"
        Label53.Size = New System.Drawing.Size(85, 16)
        Label53.TabIndex = 3
        Label53.Text = "Presentación:"
        '
        'Label52
        '
        Label52.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label52.AutoSize = True
        Label52.Location = New System.Drawing.Point(40, 73)
        Label52.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label52.Name = "Label52"
        Label52.Size = New System.Drawing.Size(62, 16)
        Label52.TabIndex = 1
        Label52.Text = "Producto:"
        '
        'Label48
        '
        Label48.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label48.AutoSize = True
        Label48.Location = New System.Drawing.Point(40, 135)
        Label48.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label48.Name = "Label48"
        Label48.Size = New System.Drawing.Size(77, 16)
        Label48.TabIndex = 5
        Label48.Text = "Tipo Tarima"
        '
        'Label50
        '
        Label50.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label50.AutoSize = True
        Label50.Location = New System.Drawing.Point(40, 165)
        Label50.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label50.Name = "Label50"
        Label50.Size = New System.Drawing.Size(121, 16)
        Label50.TabIndex = 7
        Label50.Text = "Cantidad por tarima"
        '
        'Label
        '
        Label.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label.AutoSize = True
        Label.Location = New System.Drawing.Point(40, 226)
        Label.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label.Name = "Label"
        Label.Size = New System.Drawing.Size(41, 16)
        Label.TabIndex = 11
        Label.Text = "Activo"
        '
        'lblCajasPorCama
        '
        lblCajasPorCama.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCajasPorCama.AutoSize = True
        lblCajasPorCama.Location = New System.Drawing.Point(40, 193)
        lblCajasPorCama.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCajasPorCama.Name = "lblCajasPorCama"
        lblCajasPorCama.Size = New System.Drawing.Size(115, 16)
        lblCajasPorCama.TabIndex = 9
        lblCajasPorCama.Text = "Cantidad por cama"
        '
        'lblPeso
        '
        lblPeso.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblPeso.AutoSize = True
        lblPeso.Location = New System.Drawing.Point(40, 170)
        lblPeso.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblPeso.Name = "lblPeso"
        lblPeso.Size = New System.Drawing.Size(39, 16)
        lblPeso.TabIndex = 7
        lblPeso.Text = "Peso:"
        '
        'lblAlto
        '
        lblAlto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblAlto.AutoSize = True
        lblAlto.Location = New System.Drawing.Point(40, 203)
        lblAlto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblAlto.Name = "lblAlto"
        lblAlto.Size = New System.Drawing.Size(34, 16)
        lblAlto.TabIndex = 10
        lblAlto.Text = "Alto:"
        '
        'lblLargo
        '
        lblLargo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblLargo.AutoSize = True
        lblLargo.Location = New System.Drawing.Point(40, 236)
        lblLargo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblLargo.Name = "lblLargo"
        lblLargo.Size = New System.Drawing.Size(44, 16)
        lblLargo.TabIndex = 16
        lblLargo.Text = "Largo:"
        '
        'lblAncho
        '
        lblAncho.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblAncho.AutoSize = True
        lblAncho.Location = New System.Drawing.Point(40, 270)
        lblAncho.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblAncho.Name = "lblAncho"
        lblAncho.Size = New System.Drawing.Size(47, 16)
        lblAncho.TabIndex = 22
        lblAncho.Text = "Ancho:"
        '
        'Label18
        '
        Label18.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label18.AutoSize = True
        Label18.Location = New System.Drawing.Point(40, 73)
        Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label18.Name = "Label18"
        Label18.Size = New System.Drawing.Size(57, 16)
        Label18.TabIndex = 1
        Label18.Text = "Nombre:"
        '
        'lblFactor
        '
        lblFactor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblFactor.AutoSize = True
        lblFactor.Location = New System.Drawing.Point(40, 137)
        lblFactor.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblFactor.Name = "lblFactor"
        lblFactor.Size = New System.Drawing.Size(48, 16)
        lblFactor.TabIndex = 5
        lblFactor.Text = "Factor:"
        '
        'Label19
        '
        Label19.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label19.AutoSize = True
        Label19.Location = New System.Drawing.Point(28, 34)
        Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label19.Name = "Label19"
        Label19.Size = New System.Drawing.Size(113, 16)
        Label19.TabIndex = 12
        Label19.Text = "Mínimo Existencia:"
        '
        'Label20
        '
        Label20.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label20.AutoSize = True
        Label20.Location = New System.Drawing.Point(28, 68)
        Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label20.Name = "Label20"
        Label20.Size = New System.Drawing.Size(116, 16)
        Label20.TabIndex = 18
        Label20.Text = "Máximo Existencia:"
        '
        'Label28
        '
        Label28.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label28.AutoSize = True
        Label28.Location = New System.Drawing.Point(40, 105)
        Label28.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label28.Name = "Label28"
        Label28.Size = New System.Drawing.Size(86, 16)
        Label28.TabIndex = 3
        Label28.Text = "Código Barra:"
        '
        'Label43
        '
        Label43.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label43.AutoSize = True
        Label43.Location = New System.Drawing.Point(28, 105)
        Label43.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label43.Name = "Label43"
        Label43.Size = New System.Drawing.Size(84, 16)
        Label43.TabIndex = 14
        Label43.Text = "Mínimo Peso:"
        '
        'Label44
        '
        Label44.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label44.AutoSize = True
        Label44.Location = New System.Drawing.Point(28, 138)
        Label44.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label44.Name = "Label44"
        Label44.Size = New System.Drawing.Size(87, 16)
        Label44.TabIndex = 20
        Label44.Text = "Máximo Peso:"
        '
        'Label7
        '
        Label7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(40, 304)
        Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(41, 16)
        Label7.TabIndex = 26
        Label7.Text = "Activo"
        '
        'NombreLabel
        '
        NombreLabel.AutoSize = True
        NombreLabel.Location = New System.Drawing.Point(40, 107)
        NombreLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        NombreLabel.Name = "NombreLabel"
        NombreLabel.Size = New System.Drawing.Size(104, 16)
        NombreLabel.TabIndex = 3
        NombreLabel.Text = "Código de Barra:"
        '
        'Label29
        '
        Label29.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label29.AutoSize = True
        Label29.Location = New System.Drawing.Point(2074, 32)
        Label29.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label29.Name = "Label29"
        Label29.Size = New System.Drawing.Size(52, 16)
        Label29.TabIndex = 0
        Label29.Text = "Activos:"
        '
        'ProductoLabel
        '
        ProductoLabel.AutoSize = True
        ProductoLabel.Location = New System.Drawing.Point(40, 73)
        ProductoLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        ProductoLabel.Name = "ProductoLabel"
        ProductoLabel.Size = New System.Drawing.Size(70, 16)
        ProductoLabel.TabIndex = 1
        ProductoLabel.Text = "Proveedor:"
        '
        'Label3
        '
        Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(40, 139)
        Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(41, 16)
        Label3.TabIndex = 5
        Label3.Text = "Activo"
        '
        'Label11
        '
        Label11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label11.AutoSize = True
        Label11.Location = New System.Drawing.Point(37, 107)
        Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(37, 16)
        Label11.TabIndex = 4
        Label11.Text = "Tipo:"
        '
        'Label10
        '
        Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(37, 142)
        Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(42, 16)
        Label10.TabIndex = 6
        Label10.Text = "Valor:"
        '
        'Label13
        '
        Label13.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label13.AutoSize = True
        Label13.Location = New System.Drawing.Point(37, 73)
        Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label13.Name = "Label13"
        Label13.Size = New System.Drawing.Size(72, 16)
        Label13.TabIndex = 1
        Label13.Text = "Parámetro:"
        '
        'Label34
        '
        Label34.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label34.AutoSize = True
        Label34.Location = New System.Drawing.Point(2074, 33)
        Label34.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label34.Name = "Label34"
        Label34.Size = New System.Drawing.Size(52, 16)
        Label34.TabIndex = 0
        Label34.Text = "Activos:"
        '
        'Label1
        '
        Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(37, 207)
        Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(41, 16)
        Label1.TabIndex = 9
        Label1.Text = "Activo"
        '
        'lblExistenciaMinima
        '
        lblExistenciaMinima.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblExistenciaMinima.AutoSize = True
        lblExistenciaMinima.Location = New System.Drawing.Point(15, 85)
        lblExistenciaMinima.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblExistenciaMinima.Name = "lblExistenciaMinima"
        lblExistenciaMinima.Size = New System.Drawing.Size(113, 16)
        lblExistenciaMinima.TabIndex = 2
        lblExistenciaMinima.Text = "Existencia Mínima:"
        '
        'lblExistenciaMaxima
        '
        lblExistenciaMaxima.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblExistenciaMaxima.AutoSize = True
        lblExistenciaMaxima.Location = New System.Drawing.Point(15, 118)
        lblExistenciaMaxima.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblExistenciaMaxima.Name = "lblExistenciaMaxima"
        lblExistenciaMaxima.Size = New System.Drawing.Size(116, 16)
        lblExistenciaMaxima.TabIndex = 4
        lblExistenciaMaxima.Text = "Existencia Máxima:"
        '
        'lblCosto
        '
        lblCosto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCosto.AutoSize = True
        lblCosto.Location = New System.Drawing.Point(15, 151)
        lblCosto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCosto.Name = "lblCosto"
        lblCosto.Size = New System.Drawing.Size(44, 16)
        lblCosto.TabIndex = 6
        lblCosto.Text = "Costo:"
        '
        'lblPrecio
        '
        lblPrecio.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblPrecio.AutoSize = True
        lblPrecio.Location = New System.Drawing.Point(15, 185)
        lblPrecio.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblPrecio.Name = "lblPrecio"
        lblPrecio.Size = New System.Drawing.Size(47, 16)
        lblPrecio.TabIndex = 9
        lblPrecio.Text = "Precio:"
        '
        'lblTipoRotacion
        '
        lblTipoRotacion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblTipoRotacion.AutoSize = True
        lblTipoRotacion.Location = New System.Drawing.Point(15, 53)
        lblTipoRotacion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblTipoRotacion.Name = "lblTipoRotacion"
        lblTipoRotacion.Size = New System.Drawing.Size(90, 16)
        lblTipoRotacion.TabIndex = 0
        lblTipoRotacion.Text = "Tipo Rotación:"
        '
        'lblCapturarPeso
        '
        lblCapturarPeso.AutoSize = True
        lblCapturarPeso.Location = New System.Drawing.Point(6, 21)
        lblCapturarPeso.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCapturarPeso.Name = "lblCapturarPeso"
        lblCapturarPeso.Size = New System.Drawing.Size(88, 16)
        lblCapturarPeso.TabIndex = 0
        lblCapturarPeso.Text = "Captura Peso:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(6, 50)
        Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(91, 16)
        Label4.TabIndex = 2
        Label4.Text = "Peso Estadco.:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(6, 82)
        Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(100, 16)
        Label5.TabIndex = 4
        Label5.Text = "% Tolerancia ±:"
        '
        'lblCapturarTemperatura
        '
        lblCapturarTemperatura.AutoSize = True
        lblCapturarTemperatura.Location = New System.Drawing.Point(6, 21)
        lblCapturarTemperatura.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCapturarTemperatura.Name = "lblCapturarTemperatura"
        lblCapturarTemperatura.Size = New System.Drawing.Size(98, 16)
        lblCapturarTemperatura.TabIndex = 0
        lblCapturarTemperatura.Text = "Captura Temp.:"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(6, 50)
        Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(101, 16)
        Label8.TabIndex = 2
        Label8.Text = "Temp. Estadca.:"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(6, 81)
        Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(100, 16)
        Label6.TabIndex = 4
        Label6.Text = "% Tolerancia ±:"
        '
        'Label17
        '
        Label17.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label17.AutoSize = True
        Label17.Location = New System.Drawing.Point(15, 219)
        Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label17.Name = "Label17"
        Label17.Size = New System.Drawing.Size(57, 16)
        Label17.TabIndex = 11
        Label17.Text = "Cámara:"
        '
        'lblGeneraLote
        '
        lblGeneraLote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblGeneraLote.AutoSize = True
        lblGeneraLote.Location = New System.Drawing.Point(20, 58)
        lblGeneraLote.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblGeneraLote.Name = "lblGeneraLote"
        lblGeneraLote.Size = New System.Drawing.Size(81, 16)
        lblGeneraLote.TabIndex = 2
        lblGeneraLote.Text = "Genera Lote:"
        '
        'lblEsMatPrima
        '
        lblEsMatPrima.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblEsMatPrima.AutoSize = True
        lblEsMatPrima.Location = New System.Drawing.Point(20, 146)
        lblEsMatPrima.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblEsMatPrima.Name = "lblEsMatPrima"
        lblEsMatPrima.Size = New System.Drawing.Size(110, 16)
        lblEsMatPrima.TabIndex = 10
        lblEsMatPrima.Text = "Es materia prima:"
        '
        'lblControlLote
        '
        lblControlLote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblControlLote.AutoSize = True
        lblControlLote.Location = New System.Drawing.Point(20, 27)
        lblControlLote.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblControlLote.Name = "lblControlLote"
        lblControlLote.Size = New System.Drawing.Size(81, 16)
        lblControlLote.TabIndex = 0
        lblControlLote.Text = "Control Lote:"
        '
        'lblEsKit
        '
        lblEsKit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblEsKit.AutoSize = True
        lblEsKit.Location = New System.Drawing.Point(20, 178)
        lblEsKit.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblEsKit.Name = "lblEsKit"
        lblEsKit.Size = New System.Drawing.Size(43, 16)
        lblEsKit.TabIndex = 12
        lblEsKit.Text = "Es Kit:"
        '
        'Label26
        '
        Label26.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label26.AutoSize = True
        Label26.Location = New System.Drawing.Point(20, 87)
        Label26.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label26.Name = "Label26"
        Label26.Size = New System.Drawing.Size(102, 16)
        Label26.TabIndex = 6
        Label26.Text = "Capturar Añada:"
        '
        'lblCapturaFechaManufactura
        '
        lblCapturaFechaManufactura.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCapturaFechaManufactura.AutoSize = True
        lblCapturaFechaManufactura.Location = New System.Drawing.Point(20, 116)
        lblCapturaFechaManufactura.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCapturaFechaManufactura.Name = "lblCapturaFechaManufactura"
        lblCapturaFechaManufactura.Size = New System.Drawing.Size(121, 16)
        lblCapturaFechaManufactura.TabIndex = 8
        lblCapturaFechaManufactura.Text = "Fecha Manufactura:"
        '
        'Label47
        '
        Label47.AutoSize = True
        Label47.Location = New System.Drawing.Point(9, 84)
        Label47.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label47.Name = "Label47"
        Label47.Size = New System.Drawing.Size(61, 16)
        Label47.TabIndex = 4
        Label47.Text = "No Serie:"
        '
        'lblSerializado
        '
        lblSerializado.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblSerializado.AutoSize = True
        lblSerializado.Location = New System.Drawing.Point(9, 22)
        lblSerializado.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblSerializado.Name = "lblSerializado"
        lblSerializado.Size = New System.Drawing.Size(91, 16)
        lblSerializado.TabIndex = 0
        lblSerializado.Text = "Captura Serie:"
        '
        'Label21
        '
        Label21.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label21.AutoSize = True
        Label21.Location = New System.Drawing.Point(9, 54)
        Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label21.Name = "Label21"
        Label21.Size = New System.Drawing.Size(89, 16)
        Label21.TabIndex = 2
        Label21.Text = "Tipo de Serie:"
        '
        'lblArancel
        '
        lblArancel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblArancel.AutoSize = True
        lblArancel.Location = New System.Drawing.Point(7, 59)
        lblArancel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblArancel.Name = "lblArancel"
        lblArancel.Size = New System.Drawing.Size(55, 16)
        lblArancel.TabIndex = 2
        lblArancel.Text = "Arancel:"
        '
        'lblCapturaArancel
        '
        lblCapturaArancel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCapturaArancel.AutoSize = True
        lblCapturaArancel.Location = New System.Drawing.Point(7, 31)
        lblCapturaArancel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCapturaArancel.Name = "lblCapturaArancel"
        lblCapturaArancel.Size = New System.Drawing.Size(103, 16)
        lblCapturaArancel.TabIndex = 0
        lblCapturaArancel.Text = "Captura arancel:"
        '
        'Label2
        '
        Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(7, 57)
        Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(171, 16)
        Label2.TabIndex = 2
        Label2.Text = "Cálculo Vencimientos (Días):"
        '
        'Label15
        '
        Label15.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label15.AutoSize = True
        Label15.Location = New System.Drawing.Point(6, 90)
        Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label15.Name = "Label15"
        Label15.Size = New System.Drawing.Size(103, 16)
        Label15.TabIndex = 4
        Label15.Text = "Ciclo vida (días):"
        '
        'Label16
        '
        Label16.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label16.AutoSize = True
        Label16.Location = New System.Drawing.Point(7, 21)
        Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label16.Name = "Label16"
        Label16.Size = New System.Drawing.Size(131, 16)
        Label16.TabIndex = 0
        Label16.Text = "Captura Vencimiento:"
        '
        'Label46
        '
        Label46.AutoSize = True
        Label46.Location = New System.Drawing.Point(9, 50)
        Label46.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label46.Name = "Label46"
        Label46.Size = New System.Drawing.Size(61, 16)
        Label46.TabIndex = 2
        Label46.Text = "No Parte:"
        '
        'lblEsHW
        '
        lblEsHW.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblEsHW.AutoSize = True
        lblEsHW.Location = New System.Drawing.Point(9, 20)
        lblEsHW.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblEsHW.Name = "lblEsHW"
        lblEsHW.Size = New System.Drawing.Size(49, 16)
        lblEsHW.TabIndex = 0
        lblEsHW.Text = "Es HW:"
        '
        'IdPropietarioLabel
        '
        IdPropietarioLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        IdPropietarioLabel.AutoSize = True
        IdPropietarioLabel.ForeColor = System.Drawing.Color.Black
        IdPropietarioLabel.Location = New System.Drawing.Point(22, 15)
        IdPropietarioLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        IdPropietarioLabel.Name = "IdPropietarioLabel"
        IdPropietarioLabel.Size = New System.Drawing.Size(74, 16)
        IdPropietarioLabel.TabIndex = 0
        IdPropietarioLabel.Text = "Propietario:"
        '
        'lblCodigo
        '
        lblCodigo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCodigo.AutoSize = True
        lblCodigo.Location = New System.Drawing.Point(22, 77)
        lblCodigo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCodigo.Name = "lblCodigo"
        lblCodigo.Size = New System.Drawing.Size(105, 16)
        lblCodigo.TabIndex = 4
        lblCodigo.Text = "Código Producto:"
        '
        'lblNombre
        '
        lblNombre.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblNombre.AutoSize = True
        lblNombre.Location = New System.Drawing.Point(22, 141)
        lblNombre.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblNombre.Name = "lblNombre"
        lblNombre.Size = New System.Drawing.Size(57, 16)
        lblNombre.TabIndex = 8
        lblNombre.Text = "Nombre:"
        '
        'lblCodigoBarra
        '
        lblCodigoBarra.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCodigoBarra.AutoSize = True
        lblCodigoBarra.Location = New System.Drawing.Point(22, 109)
        lblCodigoBarra.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCodigoBarra.Name = "lblCodigoBarra"
        lblCodigoBarra.Size = New System.Drawing.Size(86, 16)
        lblCodigoBarra.TabIndex = 6
        lblCodigoBarra.Text = "Código Barra:"
        '
        'ImagenLabel
        '
        ImagenLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ImagenLabel.AutoSize = True
        ImagenLabel.Location = New System.Drawing.Point(175, 42)
        ImagenLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        ImagenLabel.Name = "ImagenLabel"
        ImagenLabel.Size = New System.Drawing.Size(55, 16)
        ImagenLabel.TabIndex = 0
        ImagenLabel.Text = "Imagen:"
        ImagenLabel.Visible = False
        '
        'Label12
        '
        Label12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(22, 45)
        Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(96, 16)
        Label12.TabIndex = 2
        Label12.Text = "Código Interno:"
        '
        'lblSimbologia
        '
        lblSimbologia.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblSimbologia.AutoSize = True
        lblSimbologia.Location = New System.Drawing.Point(210, 470)
        lblSimbologia.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblSimbologia.Name = "lblSimbologia"
        lblSimbologia.Size = New System.Drawing.Size(75, 16)
        lblSimbologia.TabIndex = 5
        lblSimbologia.Text = "Simbología:"
        '
        'Label37
        '
        Label37.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label37.AutoSize = True
        Label37.Location = New System.Drawing.Point(657, 15)
        Label37.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label37.Name = "Label37"
        Label37.Size = New System.Drawing.Size(46, 16)
        Label37.TabIndex = 34
        Label37.Text = "Activo:"
        '
        'GridView10
        '
        Me.GridView10.DetailHeight = 431
        Me.GridView10.GridControl = Me.grdPStock
        Me.GridView10.Name = "GridView10"
        Me.GridView10.OptionsBehavior.Editable = False
        Me.GridView10.OptionsBehavior.ReadOnly = True
        '
        'grdPStock
        '
        Me.grdPStock.DataSource = Me.EncabezadoBindingSource
        Me.grdPStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPStock.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode1.LevelTemplate = Me.GridView10
        GridLevelNode1.RelationName = "resumen_stock"
        Me.grdPStock.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grdPStock.Location = New System.Drawing.Point(0, 0)
        Me.grdPStock.MainView = Me.GridView2
        Me.grdPStock.Margin = New System.Windows.Forms.Padding(4)
        Me.grdPStock.MenuManager = Me.RibbonControl
        Me.grdPStock.Name = "grdPStock"
        Me.grdPStock.Size = New System.Drawing.Size(1552, 603)
        Me.grdPStock.TabIndex = 0
        Me.grdPStock.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2, Me.GridView10})
        '
        'EncabezadoBindingSource
        '
        Me.EncabezadoBindingSource.DataMember = "Encabezado"
        Me.EncabezadoBindingSource.DataSource = Me.DsResumenStockBindingSource
        '
        'DsResumenStockBindingSource
        '
        Me.DsResumenStockBindingSource.DataSource = Me.DsResumenStock
        Me.DsResumenStockBindingSource.Position = 0
        '
        'DsResumenStock
        '
        Me.DsResumenStock.DataSetName = "DsResumenStock"
        Me.DsResumenStock.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GridView2
        '
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colIdProducto, Me.colPropietario, Me.colCódigo, Me.colProducto, Me.colCódigo_Barra, Me.colPresentación, Me.colUM_Bas, Me.colCantidadUMBas, Me.colCantidadPresentación})
        Me.GridView2.DetailHeight = 431
        Me.GridView2.GridControl = Me.grdPStock
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsBehavior.ReadOnly = True
        Me.GridView2.OptionsView.ShowFooter = True
        '
        'colIdProducto
        '
        Me.colIdProducto.FieldName = "IdProducto"
        Me.colIdProducto.MinWidth = 23
        Me.colIdProducto.Name = "colIdProducto"
        Me.colIdProducto.OptionsColumn.ReadOnly = True
        Me.colIdProducto.Width = 87
        '
        'colPropietario
        '
        Me.colPropietario.FieldName = "Propietario"
        Me.colPropietario.MinWidth = 23
        Me.colPropietario.Name = "colPropietario"
        Me.colPropietario.OptionsColumn.ReadOnly = True
        Me.colPropietario.Visible = True
        Me.colPropietario.VisibleIndex = 0
        Me.colPropietario.Width = 87
        '
        'colCódigo
        '
        Me.colCódigo.FieldName = "Código"
        Me.colCódigo.MinWidth = 23
        Me.colCódigo.Name = "colCódigo"
        Me.colCódigo.OptionsColumn.ReadOnly = True
        Me.colCódigo.Visible = True
        Me.colCódigo.VisibleIndex = 1
        Me.colCódigo.Width = 87
        '
        'colProducto
        '
        Me.colProducto.FieldName = "Producto"
        Me.colProducto.MinWidth = 23
        Me.colProducto.Name = "colProducto"
        Me.colProducto.OptionsColumn.ReadOnly = True
        Me.colProducto.Visible = True
        Me.colProducto.VisibleIndex = 2
        Me.colProducto.Width = 87
        '
        'colCódigo_Barra
        '
        Me.colCódigo_Barra.FieldName = "Código_Barra"
        Me.colCódigo_Barra.MinWidth = 23
        Me.colCódigo_Barra.Name = "colCódigo_Barra"
        Me.colCódigo_Barra.OptionsColumn.ReadOnly = True
        Me.colCódigo_Barra.Visible = True
        Me.colCódigo_Barra.VisibleIndex = 3
        Me.colCódigo_Barra.Width = 87
        '
        'colPresentación
        '
        Me.colPresentación.FieldName = "Presentación"
        Me.colPresentación.MinWidth = 23
        Me.colPresentación.Name = "colPresentación"
        Me.colPresentación.OptionsColumn.ReadOnly = True
        Me.colPresentación.Visible = True
        Me.colPresentación.VisibleIndex = 4
        Me.colPresentación.Width = 87
        '
        'colUM_Bas
        '
        Me.colUM_Bas.FieldName = "UM_Bas"
        Me.colUM_Bas.MinWidth = 23
        Me.colUM_Bas.Name = "colUM_Bas"
        Me.colUM_Bas.OptionsColumn.ReadOnly = True
        Me.colUM_Bas.Visible = True
        Me.colUM_Bas.VisibleIndex = 5
        Me.colUM_Bas.Width = 87
        '
        'colCantidadUMBas
        '
        Me.colCantidadUMBas.FieldName = "CantidadUMBas"
        Me.colCantidadUMBas.MinWidth = 23
        Me.colCantidadUMBas.Name = "colCantidadUMBas"
        Me.colCantidadUMBas.OptionsColumn.ReadOnly = True
        Me.colCantidadUMBas.Visible = True
        Me.colCantidadUMBas.VisibleIndex = 6
        Me.colCantidadUMBas.Width = 87
        '
        'colCantidadPresentación
        '
        Me.colCantidadPresentación.FieldName = "CantidadPresentación"
        Me.colCantidadPresentación.MinWidth = 23
        Me.colCantidadPresentación.Name = "colCantidadPresentación"
        Me.colCantidadPresentación.OptionsColumn.ReadOnly = True
        Me.colCantidadPresentación.Visible = True
        Me.colCantidadPresentación.VisibleIndex = 7
        Me.colCantidadPresentación.Width = 87
        '
        'RibbonControl
        '
        Me.RibbonControl.ApplicationButtonImageOptions.Image = CType(resources.GetObject("RibbonControl.ApplicationButtonImageOptions.Image"), System.Drawing.Image)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuAsignacion, Me.BarButtonItem1, Me.BarButtonItem2, Me.BarButtonItem3, Me.cmdCodigoBarra, Me.cmdImprimeCodigoBarra, Me.cmdImprmirCodigoBarra, Me.cmdActualizar, Me.mnuEliminarProducto, Me.cmdUbicacion, Me.mnuDesactivar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonControl.MaxItemId = 15
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.PageCategories.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageCategory() {Me.RibbonPageCategory1})
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1, Me.RibbonPage2})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1554, 193)
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
        Me.cmdImprimeCodigoBarra.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimeCodigoBarra.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
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
        'mnuEliminarProducto
        '
        Me.mnuEliminarProducto.Caption = "Eliminar"
        Me.mnuEliminarProducto.Id = 12
        Me.mnuEliminarProducto.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminarProducto.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminarProducto.Name = "mnuEliminarProducto"
        '
        'cmdUbicacion
        '
        Me.cmdUbicacion.Caption = "Ubicación"
        Me.cmdUbicacion.Id = 13
        Me.cmdUbicacion.ImageOptions.SvgImage = CType(resources.GetObject("cmdUbicacion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdUbicacion.Name = "cmdUbicacion"
        '
        'mnuDesactivar
        '
        Me.mnuDesactivar.Caption = "Desactivar"
        Me.mnuDesactivar.Id = 14
        Me.mnuDesactivar.ImageOptions.SvgImage = CType(resources.GetObject("mnuDesactivar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuDesactivar.Name = "mnuDesactivar"
        '
        'RibbonPageCategory1
        '
        Me.RibbonPageCategory1.Name = "RibbonPageCategory1"
        Me.RibbonPageCategory1.Text = "Código Barra"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Producto"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuDesactivar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminarProducto)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimeCodigoBarra)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPage2
        '
        Me.RibbonPage2.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup2})
        Me.RibbonPage2.Name = "RibbonPage2"
        Me.RibbonPage2.Text = "Imprimir"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.BarButtonItem1)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.BarButtonItem2)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.cmdImprmirCodigoBarra)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.cmdUbicacion)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 852)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1554, 30)
        '
        'lblPresentacionPallet
        '
        lblPresentacionPallet.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblPresentacionPallet.AutoSize = True
        lblPresentacionPallet.Location = New System.Drawing.Point(8, 97)
        lblPresentacionPallet.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblPresentacionPallet.Name = "lblPresentacionPallet"
        lblPresentacionPallet.Size = New System.Drawing.Size(85, 16)
        lblPresentacionPallet.TabIndex = 37
        lblPresentacionPallet.Text = "Presentación:"
        '
        'lblPrdPd
        '
        lblPrdPd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblPrdPd.AutoSize = True
        lblPrdPd.Location = New System.Drawing.Point(10, 37)
        lblPrdPd.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblPrdPd.Name = "lblPrdPd"
        lblPrdPd.Size = New System.Drawing.Size(99, 16)
        lblPrdPd.TabIndex = 15
        lblPrdPd.Text = "Producto Padre:"
        '
        'Label56
        '
        Label56.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label56.AutoSize = True
        Label56.Location = New System.Drawing.Point(10, 106)
        Label56.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label56.Name = "Label56"
        Label56.Size = New System.Drawing.Size(58, 16)
        Label56.TabIndex = 17
        Label56.Text = "U.M.Bas:"
        '
        'lblTipoManufactura
        '
        lblTipoManufactura.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblTipoManufactura.AutoSize = True
        lblTipoManufactura.Location = New System.Drawing.Point(22, 333)
        lblTipoManufactura.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblTipoManufactura.Name = "lblTipoManufactura"
        lblTipoManufactura.Size = New System.Drawing.Size(112, 16)
        lblTipoManufactura.TabIndex = 50
        lblTipoManufactura.Text = "Tipo Manufactura:"
        '
        'lblPresentacionReabastecerCon
        '
        lblPresentacionReabastecerCon.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblPresentacionReabastecerCon.AutoSize = True
        lblPresentacionReabastecerCon.Location = New System.Drawing.Point(14, 121)
        lblPresentacionReabastecerCon.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblPresentacionReabastecerCon.Name = "lblPresentacionReabastecerCon"
        lblPresentacionReabastecerCon.Size = New System.Drawing.Size(85, 16)
        lblPresentacionReabastecerCon.TabIndex = 3
        lblPresentacionReabastecerCon.Text = "Presentación:"
        '
        'Label45
        '
        Label45.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label45.AutoSize = True
        Label45.Location = New System.Drawing.Point(23, 98)
        Label45.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label45.Name = "Label45"
        Label45.Size = New System.Drawing.Size(50, 16)
        Label45.TabIndex = 7
        Label45.Text = "Estado:"
        '
        'Label39
        '
        Label39.AutoSize = True
        Label39.Location = New System.Drawing.Point(23, 234)
        Label39.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label39.Name = "Label39"
        Label39.Size = New System.Drawing.Size(99, 16)
        Label39.TabIndex = 16
        Label39.Text = "Máximo Picking:"
        '
        'lblBodegaRellenado
        '
        lblBodegaRellenado.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblBodegaRellenado.AutoSize = True
        lblBodegaRellenado.Location = New System.Drawing.Point(23, 127)
        lblBodegaRellenado.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblBodegaRellenado.Name = "lblBodegaRellenado"
        lblBodegaRellenado.Size = New System.Drawing.Size(54, 16)
        lblBodegaRellenado.TabIndex = 9
        lblBodegaRellenado.Text = "Bodega:"
        '
        'Label22
        '
        Label22.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label22.AutoSize = True
        Label22.Location = New System.Drawing.Point(464, 78)
        Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label22.Name = "Label22"
        Label22.Size = New System.Drawing.Size(41, 16)
        Label22.TabIndex = 20
        Label22.Text = "Activo"
        '
        'Label42
        '
        Label42.AutoSize = True
        Label42.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Label42.Location = New System.Drawing.Point(23, 206)
        Label42.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label42.Name = "Label42"
        Label42.Size = New System.Drawing.Size(101, 17)
        Label42.TabIndex = 14
        Label42.Text = "Mínimo Picking:"
        '
        'Label40
        '
        Label40.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label40.AutoSize = True
        Label40.Location = New System.Drawing.Point(23, 74)
        Label40.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label40.Name = "Label40"
        Label40.Size = New System.Drawing.Size(85, 16)
        Label40.TabIndex = 5
        Label40.Text = "Presentación:"
        '
        'Label38
        '
        Label38.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label38.AutoSize = True
        Label38.Location = New System.Drawing.Point(23, 14)
        Label38.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label38.Name = "Label38"
        Label38.Size = New System.Drawing.Size(62, 16)
        Label38.TabIndex = 0
        Label38.Text = "Producto:"
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(198, 48)
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
        Me.User_agrTextEdit.Location = New System.Drawing.Point(198, 16)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_agrTextEdit.TabIndex = 1
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(682, 48)
        Me.Fec_modDateEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.Fec_modDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_modDateEdit.TabIndex = 7
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(682, 16)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 2
        '
        'DataBindingSource
        '
        Me.DataBindingSource.DataMember = "Data"
        Me.DataBindingSource.DataSource = Me.DsProducto
        '
        'DsProducto
        '
        Me.DsProducto.DataSetName = "DsProducto"
        Me.DsProducto.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar - mnuEliminar"
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
        'TabDatos
        '
        Me.TabDatos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabDatos.Location = New System.Drawing.Point(0, 193)
        Me.TabDatos.Margin = New System.Windows.Forms.Padding(4)
        Me.TabDatos.Name = "TabDatos"
        Me.TabDatos.SelectedTabPage = Me.TabProducto
        Me.TabDatos.Size = New System.Drawing.Size(1554, 633)
        Me.TabDatos.TabIndex = 0
        Me.TabDatos.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.TabProducto, Me.TabAtributo, Me.TabParametros, Me.TabCodigoBarra, Me.TabProductoBodega, Me.TabPresentacion, Me.tabPresentacionTarima, Me.TabProductoSustituto, Me.TabProductoRellenado, Me.tabProductoKit, Me.tabStock, Me.Conversion, Me.XtraTabPage1, Me.tabTallaColor})
        '
        'TabProducto
        '
        Me.TabProducto.Controls.Add(Me.GrpProducto)
        Me.TabProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.TabProducto.Name = "TabProducto"
        Me.TabProducto.Size = New System.Drawing.Size(1552, 603)
        Me.TabProducto.Text = "Datos Producto"
        '
        'GrpProducto
        '
        Me.GrpProducto.Controls.Add(Me.SplitContainer3)
        Me.GrpProducto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpProducto.Location = New System.Drawing.Point(0, 0)
        Me.GrpProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpProducto.Name = "GrpProducto"
        Me.GrpProducto.ScrollBarSmallChange = 10
        Me.GrpProducto.Size = New System.Drawing.Size(1552, 603)
        Me.GrpProducto.TabIndex = 0
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
        Me.SplitContainer3.Panel1.Controls.Add(lblTipoManufactura)
        Me.SplitContainer3.Panel1.Controls.Add(Me.cmbTipoManufactura)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lcmbParametroB)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lnkParametroB)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lcmbParametroA)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lnkParametroA)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lcmbUnidadMedidaCobro)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtIdTipoProducto)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lcmbMarca)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lcmbFamilia)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lcmbClasificacion)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lcmbUnidadMedidaBasica)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lnkUMCobro)
        Me.SplitContainer3.Panel1.Controls.Add(IdPropietarioLabel)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lnkUnidadMedida)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lcmbPropietario)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lnkClasificacion)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lnkFamilia)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblC)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lnkMarca)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lnkUMPrecio)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtIdUmPrecio)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtNombreUMPrecio)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lnkTipoProducto)
        Me.SplitContainer3.Panel1.Controls.Add(Me.LinkLabel3)
        Me.SplitContainer3.Panel1.Controls.Add(lblCodigo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.TextEdit5)
        Me.SplitContainer3.Panel1.Controls.Add(lblNombre)
        Me.SplitContainer3.Panel1.Controls.Add(Me.TextEdit6)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtNombre)
        Me.SplitContainer3.Panel1.Controls.Add(Me.LinkLabel2)
        Me.SplitContainer3.Panel1.Controls.Add(lblCodigoBarra)
        Me.SplitContainer3.Panel1.Controls.Add(Me.TextEdit3)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtCodigoBarra)
        Me.SplitContainer3.Panel1.Controls.Add(Me.TextEdit4)
        Me.SplitContainer3.Panel1.Controls.Add(Label12)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtCodigo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkActivo)
        Me.SplitContainer3.Panel1.Controls.Add(Label37)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.grpDimensionesUMBas)
        Me.SplitContainer3.Panel2.Controls.Add(Me.grpImagenProducto)
        Me.SplitContainer3.Panel2.Controls.Add(Me.cmbSymbology)
        Me.SplitContainer3.Panel2.Controls.Add(Me.Bcc)
        Me.SplitContainer3.Panel2.Controls.Add(lblSimbologia)
        Me.SplitContainer3.Panel2.Controls.Add(Me.cmbEtiqueta)
        Me.SplitContainer3.Panel2.Controls.Add(Me.lblEtiqueta)
        Me.SplitContainer3.Size = New System.Drawing.Size(1548, 573)
        Me.SplitContainer3.SplitterDistance = 809
        Me.SplitContainer3.TabIndex = 0
        '
        'cmbTipoManufactura
        '
        Me.cmbTipoManufactura.Location = New System.Drawing.Point(205, 334)
        Me.cmbTipoManufactura.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbTipoManufactura.Name = "cmbTipoManufactura"
        Me.cmbTipoManufactura.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbTipoManufactura.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoManufactura.Properties.NullText = ""
        Me.cmbTipoManufactura.Properties.PopupFormMinSize = New System.Drawing.Size(199, 199)
        Me.cmbTipoManufactura.Properties.PopupWidth = 199
        Me.cmbTipoManufactura.Size = New System.Drawing.Size(385, 22)
        Me.cmbTipoManufactura.TabIndex = 49
        '
        'lcmbParametroB
        '
        Me.lcmbParametroB.Location = New System.Drawing.Point(205, 425)
        Me.lcmbParametroB.Margin = New System.Windows.Forms.Padding(4)
        Me.lcmbParametroB.Name = "lcmbParametroB"
        Me.lcmbParametroB.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lcmbParametroB.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lcmbParametroB.Properties.NullText = ""
        Me.lcmbParametroB.Properties.PopupFormMinSize = New System.Drawing.Size(199, 199)
        Me.lcmbParametroB.Properties.PopupWidth = 199
        Me.lcmbParametroB.Size = New System.Drawing.Size(385, 22)
        Me.lcmbParametroB.TabIndex = 48
        '
        'lnkParametroB
        '
        Me.lnkParametroB.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkParametroB.AutoSize = True
        Me.lnkParametroB.Location = New System.Drawing.Point(23, 426)
        Me.lnkParametroB.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkParametroB.Name = "lnkParametroB"
        Me.lnkParametroB.Size = New System.Drawing.Size(119, 16)
        Me.lnkParametroB.TabIndex = 47
        Me.lnkParametroB.TabStop = True
        Me.lnkParametroB.Text = "Parámetro B /Lado:"
        '
        'lcmbParametroA
        '
        Me.lcmbParametroA.Location = New System.Drawing.Point(205, 393)
        Me.lcmbParametroA.Margin = New System.Windows.Forms.Padding(4)
        Me.lcmbParametroA.Name = "lcmbParametroA"
        Me.lcmbParametroA.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lcmbParametroA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lcmbParametroA.Properties.NullText = ""
        Me.lcmbParametroA.Properties.PopupFormMinSize = New System.Drawing.Size(199, 199)
        Me.lcmbParametroA.Properties.PopupWidth = 199
        Me.lcmbParametroA.Size = New System.Drawing.Size(385, 22)
        Me.lcmbParametroA.TabIndex = 46
        '
        'lnkParametroA
        '
        Me.lnkParametroA.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkParametroA.AutoSize = True
        Me.lnkParametroA.Location = New System.Drawing.Point(23, 394)
        Me.lnkParametroA.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkParametroA.Name = "lnkParametroA"
        Me.lnkParametroA.Size = New System.Drawing.Size(131, 16)
        Me.lnkParametroA.TabIndex = 45
        Me.lnkParametroA.TabStop = True
        Me.lnkParametroA.Text = "Parámetro A /Estado:"
        '
        'lcmbUnidadMedidaCobro
        '
        Me.lcmbUnidadMedidaCobro.Location = New System.Drawing.Point(205, 363)
        Me.lcmbUnidadMedidaCobro.Margin = New System.Windows.Forms.Padding(4)
        Me.lcmbUnidadMedidaCobro.Name = "lcmbUnidadMedidaCobro"
        Me.lcmbUnidadMedidaCobro.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lcmbUnidadMedidaCobro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lcmbUnidadMedidaCobro.Properties.NullText = ""
        Me.lcmbUnidadMedidaCobro.Properties.PopupFormMinSize = New System.Drawing.Size(199, 199)
        Me.lcmbUnidadMedidaCobro.Properties.PopupWidth = 199
        Me.lcmbUnidadMedidaCobro.Size = New System.Drawing.Size(385, 22)
        Me.lcmbUnidadMedidaCobro.TabIndex = 44
        '
        'txtIdTipoProducto
        '
        Me.txtIdTipoProducto.Location = New System.Drawing.Point(205, 302)
        Me.txtIdTipoProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdTipoProducto.Name = "txtIdTipoProducto"
        Me.txtIdTipoProducto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtIdTipoProducto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtIdTipoProducto.Properties.NullText = ""
        Me.txtIdTipoProducto.Properties.PopupFormMinSize = New System.Drawing.Size(199, 199)
        Me.txtIdTipoProducto.Properties.PopupWidth = 199
        Me.txtIdTipoProducto.Size = New System.Drawing.Size(385, 22)
        Me.txtIdTipoProducto.TabIndex = 43
        '
        'lcmbMarca
        '
        Me.lcmbMarca.Location = New System.Drawing.Point(205, 272)
        Me.lcmbMarca.Margin = New System.Windows.Forms.Padding(4)
        Me.lcmbMarca.Name = "lcmbMarca"
        Me.lcmbMarca.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lcmbMarca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lcmbMarca.Properties.NullText = ""
        Me.lcmbMarca.Properties.PopupFormMinSize = New System.Drawing.Size(199, 199)
        Me.lcmbMarca.Properties.PopupWidth = 199
        Me.lcmbMarca.Size = New System.Drawing.Size(385, 22)
        Me.lcmbMarca.TabIndex = 42
        '
        'lcmbFamilia
        '
        Me.lcmbFamilia.Location = New System.Drawing.Point(205, 240)
        Me.lcmbFamilia.Margin = New System.Windows.Forms.Padding(4)
        Me.lcmbFamilia.Name = "lcmbFamilia"
        Me.lcmbFamilia.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lcmbFamilia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lcmbFamilia.Properties.NullText = ""
        Me.lcmbFamilia.Properties.PopupFormMinSize = New System.Drawing.Size(199, 199)
        Me.lcmbFamilia.Properties.PopupWidth = 199
        Me.lcmbFamilia.Size = New System.Drawing.Size(385, 22)
        Me.lcmbFamilia.TabIndex = 41
        '
        'lcmbClasificacion
        '
        Me.lcmbClasificacion.Location = New System.Drawing.Point(205, 208)
        Me.lcmbClasificacion.Margin = New System.Windows.Forms.Padding(4)
        Me.lcmbClasificacion.Name = "lcmbClasificacion"
        Me.lcmbClasificacion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lcmbClasificacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lcmbClasificacion.Properties.NullText = ""
        Me.lcmbClasificacion.Properties.PopupFormMinSize = New System.Drawing.Size(199, 199)
        Me.lcmbClasificacion.Properties.PopupWidth = 199
        Me.lcmbClasificacion.Size = New System.Drawing.Size(385, 22)
        Me.lcmbClasificacion.TabIndex = 40
        '
        'lcmbUnidadMedidaBasica
        '
        Me.lcmbUnidadMedidaBasica.Location = New System.Drawing.Point(204, 176)
        Me.lcmbUnidadMedidaBasica.Margin = New System.Windows.Forms.Padding(4)
        Me.lcmbUnidadMedidaBasica.Name = "lcmbUnidadMedidaBasica"
        Me.lcmbUnidadMedidaBasica.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lcmbUnidadMedidaBasica.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lcmbUnidadMedidaBasica.Properties.NullText = ""
        Me.lcmbUnidadMedidaBasica.Properties.PopupFormMinSize = New System.Drawing.Size(199, 199)
        Me.lcmbUnidadMedidaBasica.Properties.PopupWidth = 199
        Me.lcmbUnidadMedidaBasica.Size = New System.Drawing.Size(385, 22)
        Me.lcmbUnidadMedidaBasica.TabIndex = 39
        '
        'lnkUMCobro
        '
        Me.lnkUMCobro.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkUMCobro.AutoSize = True
        Me.lnkUMCobro.Location = New System.Drawing.Point(23, 364)
        Me.lnkUMCobro.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkUMCobro.Name = "lnkUMCobro"
        Me.lnkUMCobro.Size = New System.Drawing.Size(76, 16)
        Me.lnkUMCobro.TabIndex = 36
        Me.lnkUMCobro.TabStop = True
        Me.lnkUMCobro.Text = "U.M. Cobro:"
        '
        'lnkUnidadMedida
        '
        Me.lnkUnidadMedida.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkUnidadMedida.AutoSize = True
        Me.lnkUnidadMedida.Location = New System.Drawing.Point(22, 174)
        Me.lnkUnidadMedida.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkUnidadMedida.Name = "lnkUnidadMedida"
        Me.lnkUnidadMedida.Size = New System.Drawing.Size(73, 16)
        Me.lnkUnidadMedida.TabIndex = 10
        Me.lnkUnidadMedida.TabStop = True
        Me.lnkUnidadMedida.Text = "U.M. Básica"
        '
        'lcmbPropietario
        '
        Me.lcmbPropietario.Location = New System.Drawing.Point(205, 12)
        Me.lcmbPropietario.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.lcmbPropietario.Name = "lcmbPropietario"
        Me.lcmbPropietario.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lcmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lcmbPropietario.Size = New System.Drawing.Size(384, 22)
        Me.lcmbPropietario.TabIndex = 1
        '
        'lnkClasificacion
        '
        Me.lnkClasificacion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkClasificacion.AutoSize = True
        Me.lnkClasificacion.Location = New System.Drawing.Point(22, 207)
        Me.lnkClasificacion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkClasificacion.Name = "lnkClasificacion"
        Me.lnkClasificacion.Size = New System.Drawing.Size(128, 16)
        Me.lnkClasificacion.TabIndex = 13
        Me.lnkClasificacion.TabStop = True
        Me.lnkClasificacion.Text = "Clasificación/Modelo:"
        '
        'lnkFamilia
        '
        Me.lnkFamilia.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkFamilia.AutoSize = True
        Me.lnkFamilia.Location = New System.Drawing.Point(22, 239)
        Me.lnkFamilia.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkFamilia.Name = "lnkFamilia"
        Me.lnkFamilia.Size = New System.Drawing.Size(88, 16)
        Me.lnkFamilia.TabIndex = 16
        Me.lnkFamilia.TabStop = True
        Me.lnkFamilia.Text = "Familia/Línea:"
        '
        'lblC
        '
        Me.lblC.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblC.Location = New System.Drawing.Point(205, 46)
        Me.lblC.Margin = New System.Windows.Forms.Padding(4)
        Me.lblC.Name = "lblC"
        Me.lblC.Properties.Appearance.BackColor = System.Drawing.Color.Lavender
        Me.lblC.Properties.Appearance.Options.UseBackColor = True
        Me.lblC.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.lblC.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.lblC.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblC.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.lblC.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.lblC.Size = New System.Drawing.Size(384, 22)
        Me.lblC.TabIndex = 3
        '
        'lnkMarca
        '
        Me.lnkMarca.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkMarca.AutoSize = True
        Me.lnkMarca.Location = New System.Drawing.Point(22, 271)
        Me.lnkMarca.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkMarca.Name = "lnkMarca"
        Me.lnkMarca.Size = New System.Drawing.Size(47, 16)
        Me.lnkMarca.TabIndex = 19
        Me.lnkMarca.TabStop = True
        Me.lnkMarca.Text = "Marca:"
        '
        'lnkUMPrecio
        '
        Me.lnkUMPrecio.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkUMPrecio.AutoSize = True
        Me.lnkUMPrecio.Location = New System.Drawing.Point(58, 525)
        Me.lnkUMPrecio.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkUMPrecio.Name = "lnkUMPrecio"
        Me.lnkUMPrecio.Size = New System.Drawing.Size(77, 16)
        Me.lnkUMPrecio.TabIndex = 31
        Me.lnkUMPrecio.TabStop = True
        Me.lnkUMPrecio.Text = "U.M. Precio:"
        Me.lnkUMPrecio.Visible = False
        '
        'txtIdUmPrecio
        '
        Me.txtIdUmPrecio.Location = New System.Drawing.Point(205, 519)
        Me.txtIdUmPrecio.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdUmPrecio.MenuManager = Me.RibbonControl
        Me.txtIdUmPrecio.Name = "txtIdUmPrecio"
        Me.txtIdUmPrecio.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtIdUmPrecio.Properties.Mask.EditMask = "n0"
        Me.txtIdUmPrecio.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtIdUmPrecio.Size = New System.Drawing.Size(89, 22)
        Me.txtIdUmPrecio.TabIndex = 32
        Me.txtIdUmPrecio.Visible = False
        '
        'txtNombreUMPrecio
        '
        Me.txtNombreUMPrecio.Location = New System.Drawing.Point(300, 521)
        Me.txtNombreUMPrecio.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNombreUMPrecio.MenuManager = Me.RibbonControl
        Me.txtNombreUMPrecio.Name = "txtNombreUMPrecio"
        Me.txtNombreUMPrecio.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtNombreUMPrecio.Properties.ReadOnly = True
        Me.txtNombreUMPrecio.Size = New System.Drawing.Size(290, 22)
        Me.txtNombreUMPrecio.TabIndex = 33
        Me.txtNombreUMPrecio.Visible = False
        '
        'lnkTipoProducto
        '
        Me.lnkTipoProducto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkTipoProducto.AutoSize = True
        Me.lnkTipoProducto.Location = New System.Drawing.Point(22, 303)
        Me.lnkTipoProducto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkTipoProducto.Name = "lnkTipoProducto"
        Me.lnkTipoProducto.Size = New System.Drawing.Size(37, 16)
        Me.lnkTipoProducto.TabIndex = 22
        Me.lnkTipoProducto.TabStop = True
        Me.lnkTipoProducto.Text = "Tipo:"
        '
        'LinkLabel3
        '
        Me.LinkLabel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel3.AutoSize = True
        Me.LinkLabel3.Location = New System.Drawing.Point(58, 493)
        Me.LinkLabel3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.Size = New System.Drawing.Size(80, 16)
        Me.LinkLabel3.TabIndex = 28
        Me.LinkLabel3.TabStop = True
        Me.LinkLabel3.Text = "U.M. Pedido:"
        Me.LinkLabel3.Visible = False
        '
        'TextEdit5
        '
        Me.TextEdit5.Location = New System.Drawing.Point(205, 487)
        Me.TextEdit5.Margin = New System.Windows.Forms.Padding(4)
        Me.TextEdit5.MenuManager = Me.RibbonControl
        Me.TextEdit5.Name = "TextEdit5"
        Me.TextEdit5.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.TextEdit5.Properties.Mask.EditMask = "n0"
        Me.TextEdit5.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TextEdit5.Size = New System.Drawing.Size(89, 22)
        Me.TextEdit5.TabIndex = 29
        Me.TextEdit5.Visible = False
        '
        'TextEdit6
        '
        Me.TextEdit6.Location = New System.Drawing.Point(300, 489)
        Me.TextEdit6.Margin = New System.Windows.Forms.Padding(4)
        Me.TextEdit6.MenuManager = Me.RibbonControl
        Me.TextEdit6.Name = "TextEdit6"
        Me.TextEdit6.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.TextEdit6.Properties.ReadOnly = True
        Me.TextEdit6.Size = New System.Drawing.Size(290, 22)
        Me.TextEdit6.TabIndex = 30
        Me.TextEdit6.Visible = False
        '
        'txtNombre
        '
        Me.txtNombre.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtNombre.Location = New System.Drawing.Point(205, 142)
        Me.txtNombre.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNombre.MenuManager = Me.RibbonControl
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtNombre.Size = New System.Drawing.Size(384, 22)
        Me.txtNombre.TabIndex = 9
        '
        'LinkLabel2
        '
        Me.LinkLabel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Location = New System.Drawing.Point(58, 461)
        Me.LinkLabel2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(87, 16)
        Me.LinkLabel2.TabIndex = 25
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "U.M. Compra:"
        Me.LinkLabel2.Visible = False
        '
        'TextEdit3
        '
        Me.TextEdit3.Location = New System.Drawing.Point(205, 455)
        Me.TextEdit3.Margin = New System.Windows.Forms.Padding(4)
        Me.TextEdit3.MenuManager = Me.RibbonControl
        Me.TextEdit3.Name = "TextEdit3"
        Me.TextEdit3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.TextEdit3.Properties.Mask.EditMask = "n0"
        Me.TextEdit3.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TextEdit3.Size = New System.Drawing.Size(89, 22)
        Me.TextEdit3.TabIndex = 26
        Me.TextEdit3.Visible = False
        '
        'txtCodigoBarra
        '
        Me.txtCodigoBarra.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtCodigoBarra.Location = New System.Drawing.Point(205, 110)
        Me.txtCodigoBarra.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodigoBarra.MenuManager = Me.RibbonControl
        Me.txtCodigoBarra.Name = "txtCodigoBarra"
        Me.txtCodigoBarra.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCodigoBarra.Size = New System.Drawing.Size(384, 22)
        Me.txtCodigoBarra.TabIndex = 7
        '
        'TextEdit4
        '
        Me.TextEdit4.Location = New System.Drawing.Point(300, 457)
        Me.TextEdit4.Margin = New System.Windows.Forms.Padding(4)
        Me.TextEdit4.MenuManager = Me.RibbonControl
        Me.TextEdit4.Name = "TextEdit4"
        Me.TextEdit4.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.TextEdit4.Properties.ReadOnly = True
        Me.TextEdit4.Size = New System.Drawing.Size(290, 22)
        Me.TextEdit4.TabIndex = 27
        Me.TextEdit4.Visible = False
        '
        'txtCodigo
        '
        Me.txtCodigo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtCodigo.Location = New System.Drawing.Point(205, 78)
        Me.txtCodigo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.txtCodigo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.txtCodigo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCodigo.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.txtCodigo.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtCodigo.Size = New System.Drawing.Size(384, 22)
        Me.txtCodigo.TabIndex = 5
        '
        'chkActivo
        '
        Me.chkActivo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(718, 34)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(4)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(66, 24)
        Me.chkActivo.TabIndex = 35
        '
        'grpDimensionesUMBas
        '
        Me.grpDimensionesUMBas.Controls.Add(Me.txtMargen)
        Me.grpDimensionesUMBas.Controls.Add(Me.lblMargen)
        Me.grpDimensionesUMBas.Controls.Add(Me.txtAnchoUB)
        Me.grpDimensionesUMBas.Controls.Add(Me.lblAnchoPr)
        Me.grpDimensionesUMBas.Controls.Add(Me.lblAltoPr)
        Me.grpDimensionesUMBas.Controls.Add(Me.txtLargoUB)
        Me.grpDimensionesUMBas.Controls.Add(Me.txtAltoUB)
        Me.grpDimensionesUMBas.Controls.Add(Me.lblLargoPr)
        Me.grpDimensionesUMBas.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpDimensionesUMBas.Location = New System.Drawing.Point(0, 257)
        Me.grpDimensionesUMBas.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.grpDimensionesUMBas.Name = "grpDimensionesUMBas"
        Me.grpDimensionesUMBas.Size = New System.Drawing.Size(735, 178)
        Me.grpDimensionesUMBas.TabIndex = 2
        Me.grpDimensionesUMBas.Text = "Dimensiones unidad medida básica"
        '
        'txtMargen
        '
        Me.txtMargen.DecimalPlaces = 2
        Me.txtMargen.Location = New System.Drawing.Point(317, 142)
        Me.txtMargen.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMargen.Name = "txtMargen"
        Me.txtMargen.Size = New System.Drawing.Size(190, 23)
        Me.txtMargen.TabIndex = 7
        '
        'lblMargen
        '
        Me.lblMargen.AutoSize = True
        Me.lblMargen.Location = New System.Drawing.Point(210, 142)
        Me.lblMargen.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMargen.Name = "lblMargen"
        Me.lblMargen.Size = New System.Drawing.Size(55, 16)
        Me.lblMargen.TabIndex = 6
        Me.lblMargen.Text = "Márgen:"
        '
        'txtAnchoUB
        '
        Me.txtAnchoUB.DecimalPlaces = 2
        Me.txtAnchoUB.Location = New System.Drawing.Point(317, 110)
        Me.txtAnchoUB.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAnchoUB.Name = "txtAnchoUB"
        Me.txtAnchoUB.Size = New System.Drawing.Size(190, 23)
        Me.txtAnchoUB.TabIndex = 5
        '
        'lblAnchoPr
        '
        Me.lblAnchoPr.AutoSize = True
        Me.lblAnchoPr.Location = New System.Drawing.Point(210, 111)
        Me.lblAnchoPr.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAnchoPr.Name = "lblAnchoPr"
        Me.lblAnchoPr.Size = New System.Drawing.Size(47, 16)
        Me.lblAnchoPr.TabIndex = 4
        Me.lblAnchoPr.Text = "Ancho:"
        '
        'lblAltoPr
        '
        Me.lblAltoPr.AutoSize = True
        Me.lblAltoPr.Location = New System.Drawing.Point(210, 78)
        Me.lblAltoPr.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAltoPr.Name = "lblAltoPr"
        Me.lblAltoPr.Size = New System.Drawing.Size(34, 16)
        Me.lblAltoPr.TabIndex = 2
        Me.lblAltoPr.Text = "Alto:"
        '
        'txtLargoUB
        '
        Me.txtLargoUB.DecimalPlaces = 2
        Me.txtLargoUB.Location = New System.Drawing.Point(317, 46)
        Me.txtLargoUB.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLargoUB.Name = "txtLargoUB"
        Me.txtLargoUB.Size = New System.Drawing.Size(190, 23)
        Me.txtLargoUB.TabIndex = 1
        '
        'txtAltoUB
        '
        Me.txtAltoUB.DecimalPlaces = 2
        Me.txtAltoUB.Location = New System.Drawing.Point(317, 76)
        Me.txtAltoUB.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAltoUB.Name = "txtAltoUB"
        Me.txtAltoUB.Size = New System.Drawing.Size(190, 23)
        Me.txtAltoUB.TabIndex = 3
        '
        'lblLargoPr
        '
        Me.lblLargoPr.AutoSize = True
        Me.lblLargoPr.Location = New System.Drawing.Point(210, 48)
        Me.lblLargoPr.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLargoPr.Name = "lblLargoPr"
        Me.lblLargoPr.Size = New System.Drawing.Size(44, 16)
        Me.lblLargoPr.TabIndex = 0
        Me.lblLargoPr.Text = "Largo:"
        '
        'grpImagenProducto
        '
        Me.grpImagenProducto.Controls.Add(ImagenLabel)
        Me.grpImagenProducto.Controls.Add(Me.picFoto)
        Me.grpImagenProducto.Controls.Add(Me.btnExaminar)
        Me.grpImagenProducto.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpImagenProducto.Location = New System.Drawing.Point(0, 0)
        Me.grpImagenProducto.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.grpImagenProducto.Name = "grpImagenProducto"
        Me.grpImagenProducto.Size = New System.Drawing.Size(735, 257)
        Me.grpImagenProducto.TabIndex = 1
        Me.grpImagenProducto.Text = "Imagen"
        '
        'picFoto
        '
        Me.picFoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picFoto.Location = New System.Drawing.Point(251, 38)
        Me.picFoto.Margin = New System.Windows.Forms.Padding(4)
        Me.picFoto.Name = "picFoto"
        Me.picFoto.Size = New System.Drawing.Size(231, 166)
        Me.picFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picFoto.TabIndex = 51
        Me.picFoto.TabStop = False
        '
        'btnExaminar
        '
        Me.btnExaminar.Location = New System.Drawing.Point(251, 212)
        Me.btnExaminar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExaminar.Name = "btnExaminar"
        Me.btnExaminar.Size = New System.Drawing.Size(231, 28)
        Me.btnExaminar.TabIndex = 0
        Me.btnExaminar.Text = "Examinar..."
        '
        'cmbSymbology
        '
        Me.cmbSymbology.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbSymbology.Location = New System.Drawing.Point(317, 467)
        Me.cmbSymbology.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbSymbology.MenuManager = Me.RibbonControl
        Me.cmbSymbology.Name = "cmbSymbology"
        Me.cmbSymbology.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbSymbology.Properties.NullText = ""
        Me.cmbSymbology.Size = New System.Drawing.Size(190, 22)
        Me.cmbSymbology.TabIndex = 6
        '
        'Bcc
        '
        Me.Bcc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Bcc.Location = New System.Drawing.Point(317, 493)
        Me.Bcc.Margin = New System.Windows.Forms.Padding(4)
        Me.Bcc.Name = "Bcc"
        Me.Bcc.Padding = New System.Windows.Forms.Padding(12, 2, 12, 0)
        Me.Bcc.Size = New System.Drawing.Size(190, 63)
        Me.Bcc.Symbology = Code128Generator1
        Me.Bcc.TabIndex = 0
        '
        'cmbEtiqueta
        '
        Me.cmbEtiqueta.Location = New System.Drawing.Point(317, 441)
        Me.cmbEtiqueta.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbEtiqueta.MenuManager = Me.RibbonControl
        Me.cmbEtiqueta.Name = "cmbEtiqueta"
        Me.cmbEtiqueta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEtiqueta.Properties.NullText = ""
        Me.cmbEtiqueta.Size = New System.Drawing.Size(190, 22)
        Me.cmbEtiqueta.TabIndex = 4
        '
        'lblEtiqueta
        '
        Me.lblEtiqueta.AutoSize = True
        Me.lblEtiqueta.Location = New System.Drawing.Point(210, 447)
        Me.lblEtiqueta.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEtiqueta.Name = "lblEtiqueta"
        Me.lblEtiqueta.Size = New System.Drawing.Size(53, 16)
        Me.lblEtiqueta.TabIndex = 3
        Me.lblEtiqueta.Text = "Etiqueta"
        '
        'TabAtributo
        '
        Me.TabAtributo.Controls.Add(Me.GrpAtributo)
        Me.TabAtributo.Margin = New System.Windows.Forms.Padding(4)
        Me.TabAtributo.Name = "TabAtributo"
        Me.TabAtributo.Size = New System.Drawing.Size(1552, 603)
        Me.TabAtributo.Text = "Atributos"
        '
        'GrpAtributo
        '
        Me.GrpAtributo.Controls.Add(Me.chkGeneraLicAutoP)
        Me.GrpAtributo.Controls.Add(Me.GroupControl14)
        Me.GrpAtributo.Controls.Add(Me.cmbCamara)
        Me.GrpAtributo.Controls.Add(Me.cmbTipoRotacion)
        Me.GrpAtributo.Controls.Add(Me.GroupBox5)
        Me.GrpAtributo.Controls.Add(Me.GroupBox4)
        Me.GrpAtributo.Controls.Add(Me.GroupBox3)
        Me.GrpAtributo.Controls.Add(Me.GroupBox2)
        Me.GrpAtributo.Controls.Add(Me.GroupBox1)
        Me.GrpAtributo.Controls.Add(Label17)
        Me.GrpAtributo.Controls.Add(Me.GrpTemperatura)
        Me.GrpAtributo.Controls.Add(Me.GrpPeso)
        Me.GrpAtributo.Controls.Add(lblTipoRotacion)
        Me.GrpAtributo.Controls.Add(Me.txtPrecio)
        Me.GrpAtributo.Controls.Add(lblPrecio)
        Me.GrpAtributo.Controls.Add(Me.txtCosto)
        Me.GrpAtributo.Controls.Add(lblCosto)
        Me.GrpAtributo.Controls.Add(Me.txtExistenciaMaxima)
        Me.GrpAtributo.Controls.Add(lblExistenciaMaxima)
        Me.GrpAtributo.Controls.Add(Me.txtExitenciaMinima)
        Me.GrpAtributo.Controls.Add(lblExistenciaMinima)
        Me.GrpAtributo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpAtributo.Location = New System.Drawing.Point(0, 0)
        Me.GrpAtributo.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpAtributo.Name = "GrpAtributo"
        Me.GrpAtributo.Size = New System.Drawing.Size(1552, 603)
        Me.GrpAtributo.TabIndex = 0
        Me.GrpAtributo.Tag = ""
        '
        'chkGeneraLicAutoP
        '
        Me.chkGeneraLicAutoP.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkGeneraLicAutoP.Location = New System.Drawing.Point(154, 266)
        Me.chkGeneraLicAutoP.Margin = New System.Windows.Forms.Padding(4)
        Me.chkGeneraLicAutoP.MenuManager = Me.RibbonControl
        Me.chkGeneraLicAutoP.Name = "chkGeneraLicAutoP"
        Me.chkGeneraLicAutoP.Properties.Caption = "Genera Licencia Auto"
        Me.chkGeneraLicAutoP.Size = New System.Drawing.Size(220, 24)
        Me.chkGeneraLicAutoP.TabIndex = 35
        Me.chkGeneraLicAutoP.ToolTip = "Índica si se generará un correlativo único para cada pallet o se ingresará el núm" &
    "ero de pallet."
        '
        'GroupControl14
        '
        Me.GroupControl14.Controls.Add(Me.picFormulaIndiceRotacionWMS)
        Me.GroupControl14.Controls.Add(Me.lblIndiceRotacionRef)
        Me.GroupControl14.Controls.Add(Me.lblDiasInventarioPromedio)
        Me.GroupControl14.Controls.Add(Me.lblIndiceRotacionWMS)
        Me.GroupControl14.Controls.Add(Me.cmbIndiceRotacionWMS)
        Me.GroupControl14.Controls.Add(Me.txtDiasPromedioInventario)
        Me.GroupControl14.Controls.Add(Me.cmbIndiceRotacion)
        Me.GroupControl14.Location = New System.Drawing.Point(749, 298)
        Me.GroupControl14.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupControl14.Name = "GroupControl14"
        Me.GroupControl14.Size = New System.Drawing.Size(500, 252)
        Me.GroupControl14.TabIndex = 24
        Me.GroupControl14.Text = "Parámetros de Slotting"
        '
        'picFormulaIndiceRotacionWMS
        '
        Me.picFormulaIndiceRotacionWMS.Image = Global.TOMWMS.My.Resources.Resources.IR_WMS
        Me.picFormulaIndiceRotacionWMS.Location = New System.Drawing.Point(196, 162)
        Me.picFormulaIndiceRotacionWMS.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.picFormulaIndiceRotacionWMS.Name = "picFormulaIndiceRotacionWMS"
        Me.picFormulaIndiceRotacionWMS.Size = New System.Drawing.Size(223, 65)
        Me.picFormulaIndiceRotacionWMS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picFormulaIndiceRotacionWMS.TabIndex = 25
        Me.picFormulaIndiceRotacionWMS.TabStop = False
        '
        'lblIndiceRotacionRef
        '
        Me.lblIndiceRotacionRef.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblIndiceRotacionRef.Location = New System.Drawing.Point(30, 129)
        Me.lblIndiceRotacionRef.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.lblIndiceRotacionRef.Name = "lblIndiceRotacionRef"
        Me.lblIndiceRotacionRef.Size = New System.Drawing.Size(150, 16)
        Me.lblIndiceRotacionRef.TabIndex = 30
        Me.lblIndiceRotacionRef.Text = "Días inventario promedio"
        '
        'lblDiasInventarioPromedio
        '
        Me.lblDiasInventarioPromedio.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblDiasInventarioPromedio.Location = New System.Drawing.Point(30, 94)
        Me.lblDiasInventarioPromedio.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.lblDiasInventarioPromedio.Name = "lblDiasInventarioPromedio"
        Me.lblDiasInventarioPromedio.Size = New System.Drawing.Size(150, 16)
        Me.lblDiasInventarioPromedio.TabIndex = 29
        Me.lblDiasInventarioPromedio.Text = "Días inventario promedio"
        '
        'lblIndiceRotacionWMS
        '
        Me.lblIndiceRotacionWMS.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblIndiceRotacionWMS.Location = New System.Drawing.Point(30, 59)
        Me.lblIndiceRotacionWMS.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.lblIndiceRotacionWMS.Name = "lblIndiceRotacionWMS"
        Me.lblIndiceRotacionWMS.Size = New System.Drawing.Size(150, 16)
        Me.lblIndiceRotacionWMS.TabIndex = 28
        Me.lblIndiceRotacionWMS.Text = "Índice rotación WMS"
        '
        'cmbIndiceRotacionWMS
        '
        Me.cmbIndiceRotacionWMS.Location = New System.Drawing.Point(196, 126)
        Me.cmbIndiceRotacionWMS.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbIndiceRotacionWMS.MenuManager = Me.RibbonControl
        Me.cmbIndiceRotacionWMS.Name = "cmbIndiceRotacionWMS"
        Me.cmbIndiceRotacionWMS.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbIndiceRotacionWMS.Properties.NullText = ""
        Me.cmbIndiceRotacionWMS.Size = New System.Drawing.Size(222, 22)
        Me.cmbIndiceRotacionWMS.TabIndex = 27
        '
        'txtDiasPromedioInventario
        '
        Me.txtDiasPromedioInventario.Location = New System.Drawing.Point(196, 91)
        Me.txtDiasPromedioInventario.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDiasPromedioInventario.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtDiasPromedioInventario.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtDiasPromedioInventario.Name = "txtDiasPromedioInventario"
        Me.txtDiasPromedioInventario.Size = New System.Drawing.Size(223, 23)
        Me.txtDiasPromedioInventario.TabIndex = 24
        Me.txtDiasPromedioInventario.Value = New Decimal(New Integer() {90, 0, 0, 0})
        '
        'cmbIndiceRotacion
        '
        Me.cmbIndiceRotacion.Location = New System.Drawing.Point(196, 57)
        Me.cmbIndiceRotacion.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbIndiceRotacion.MenuManager = Me.RibbonControl
        Me.cmbIndiceRotacion.Name = "cmbIndiceRotacion"
        Me.cmbIndiceRotacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbIndiceRotacion.Properties.NullText = ""
        Me.cmbIndiceRotacion.Size = New System.Drawing.Size(222, 22)
        Me.cmbIndiceRotacion.TabIndex = 23
        '
        'cmbCamara
        '
        Me.cmbCamara.Location = New System.Drawing.Point(153, 217)
        Me.cmbCamara.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbCamara.MenuManager = Me.RibbonControl
        Me.cmbCamara.Name = "cmbCamara"
        Me.cmbCamara.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCamara.Properties.NullText = ""
        Me.cmbCamara.Size = New System.Drawing.Size(222, 22)
        Me.cmbCamara.TabIndex = 22
        '
        'cmbTipoRotacion
        '
        Me.cmbTipoRotacion.Location = New System.Drawing.Point(153, 49)
        Me.cmbTipoRotacion.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbTipoRotacion.MenuManager = Me.RibbonControl
        Me.cmbTipoRotacion.Name = "cmbTipoRotacion"
        Me.cmbTipoRotacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoRotacion.Properties.NullText = ""
        Me.cmbTipoRotacion.Size = New System.Drawing.Size(222, 22)
        Me.cmbTipoRotacion.TabIndex = 21
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(lblEsHW)
        Me.GroupBox5.Controls.Add(Me.chkEsHW)
        Me.GroupBox5.Controls.Add(Me.txtNoParte)
        Me.GroupBox5.Controls.Add(Label46)
        Me.GroupBox5.Location = New System.Drawing.Point(951, 180)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox5.Size = New System.Drawing.Size(298, 108)
        Me.GroupBox5.TabIndex = 18
        Me.GroupBox5.TabStop = False
        '
        'chkEsHW
        '
        Me.chkEsHW.Location = New System.Drawing.Point(72, 16)
        Me.chkEsHW.Margin = New System.Windows.Forms.Padding(4)
        Me.chkEsHW.MenuManager = Me.RibbonControl
        Me.chkEsHW.Name = "chkEsHW"
        Me.chkEsHW.Properties.Caption = ""
        Me.chkEsHW.Size = New System.Drawing.Size(21, 24)
        Me.chkEsHW.TabIndex = 1
        '
        'txtNoParte
        '
        Me.txtNoParte.Location = New System.Drawing.Point(72, 44)
        Me.txtNoParte.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNoParte.MenuManager = Me.RibbonControl
        Me.txtNoParte.Name = "txtNoParte"
        Me.txtNoParte.Size = New System.Drawing.Size(155, 22)
        Me.txtNoParte.TabIndex = 3
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtCicloVida)
        Me.GroupBox4.Controls.Add(Label16)
        Me.GroupBox4.Controls.Add(Me.chkControlVencimiento)
        Me.GroupBox4.Controls.Add(Label15)
        Me.GroupBox4.Controls.Add(Me.txtTolerancia)
        Me.GroupBox4.Controls.Add(Label2)
        Me.GroupBox4.Location = New System.Drawing.Point(416, 298)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Size = New System.Drawing.Size(299, 138)
        Me.GroupBox4.TabIndex = 19
        Me.GroupBox4.TabStop = False
        '
        'txtCicloVida
        '
        Me.txtCicloVida.Enabled = False
        Me.txtCicloVida.Location = New System.Drawing.Point(119, 87)
        Me.txtCicloVida.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCicloVida.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCicloVida.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtCicloVida.Name = "txtCicloVida"
        Me.txtCicloVida.Size = New System.Drawing.Size(153, 23)
        Me.txtCicloVida.TabIndex = 5
        '
        'chkControlVencimiento
        '
        Me.chkControlVencimiento.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkControlVencimiento.Location = New System.Drawing.Point(188, 18)
        Me.chkControlVencimiento.Margin = New System.Windows.Forms.Padding(4)
        Me.chkControlVencimiento.MenuManager = Me.RibbonControl
        Me.chkControlVencimiento.Name = "chkControlVencimiento"
        Me.chkControlVencimiento.Properties.Caption = ""
        Me.chkControlVencimiento.Size = New System.Drawing.Size(27, 24)
        Me.chkControlVencimiento.TabIndex = 1
        '
        'txtTolerancia
        '
        Me.txtTolerancia.Location = New System.Drawing.Point(188, 54)
        Me.txtTolerancia.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTolerancia.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTolerancia.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtTolerancia.Name = "txtTolerancia"
        Me.txtTolerancia.Size = New System.Drawing.Size(84, 23)
        Me.txtTolerancia.TabIndex = 3
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmbArancel)
        Me.GroupBox3.Controls.Add(lblCapturaArancel)
        Me.GroupBox3.Controls.Add(Me.chkCapturaArancel)
        Me.GroupBox3.Controls.Add(lblArancel)
        Me.GroupBox3.Location = New System.Drawing.Point(416, 444)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(299, 106)
        Me.GroupBox3.TabIndex = 20
        Me.GroupBox3.TabStop = False
        '
        'cmbArancel
        '
        Me.cmbArancel.Location = New System.Drawing.Point(118, 60)
        Me.cmbArancel.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbArancel.MenuManager = Me.RibbonControl
        Me.cmbArancel.Name = "cmbArancel"
        Me.cmbArancel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbArancel.Properties.NullText = ""
        Me.cmbArancel.Size = New System.Drawing.Size(153, 22)
        Me.cmbArancel.TabIndex = 24
        '
        'chkCapturaArancel
        '
        Me.chkCapturaArancel.Location = New System.Drawing.Point(118, 28)
        Me.chkCapturaArancel.Margin = New System.Windows.Forms.Padding(4)
        Me.chkCapturaArancel.MenuManager = Me.RibbonControl
        Me.chkCapturaArancel.Name = "chkCapturaArancel"
        Me.chkCapturaArancel.Properties.Caption = ""
        Me.chkCapturaArancel.Size = New System.Drawing.Size(31, 24)
        Me.chkCapturaArancel.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmbPerfilSerializado)
        Me.GroupBox2.Controls.Add(Label21)
        Me.GroupBox2.Controls.Add(lblSerializado)
        Me.GroupBox2.Controls.Add(Me.chkSerializado)
        Me.GroupBox2.Controls.Add(Me.txtNoSerie)
        Me.GroupBox2.Controls.Add(Label47)
        Me.GroupBox2.Location = New System.Drawing.Point(951, 41)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(298, 132)
        Me.GroupBox2.TabIndex = 15
        Me.GroupBox2.TabStop = False
        '
        'cmbPerfilSerializado
        '
        Me.cmbPerfilSerializado.Location = New System.Drawing.Point(111, 48)
        Me.cmbPerfilSerializado.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPerfilSerializado.MenuManager = Me.RibbonControl
        Me.cmbPerfilSerializado.Name = "cmbPerfilSerializado"
        Me.cmbPerfilSerializado.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPerfilSerializado.Properties.NullText = ""
        Me.cmbPerfilSerializado.Size = New System.Drawing.Size(169, 22)
        Me.cmbPerfilSerializado.TabIndex = 24
        '
        'chkSerializado
        '
        Me.chkSerializado.Location = New System.Drawing.Point(111, 21)
        Me.chkSerializado.Margin = New System.Windows.Forms.Padding(4)
        Me.chkSerializado.MenuManager = Me.RibbonControl
        Me.chkSerializado.Name = "chkSerializado"
        Me.chkSerializado.Properties.Caption = ""
        Me.chkSerializado.Size = New System.Drawing.Size(21, 24)
        Me.chkSerializado.TabIndex = 1
        '
        'txtNoSerie
        '
        Me.txtNoSerie.Location = New System.Drawing.Point(111, 80)
        Me.txtNoSerie.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNoSerie.MenuManager = Me.RibbonControl
        Me.txtNoSerie.Name = "txtNoSerie"
        Me.txtNoSerie.Size = New System.Drawing.Size(169, 22)
        Me.txtNoSerie.TabIndex = 5
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(lblCapturaFechaManufactura)
        Me.GroupBox1.Controls.Add(Me.chkFechaManufactura)
        Me.GroupBox1.Controls.Add(Label26)
        Me.GroupBox1.Controls.Add(lblEsKit)
        Me.GroupBox1.Controls.Add(Me.chkCapturarAniada)
        Me.GroupBox1.Controls.Add(Me.chkEsKit)
        Me.GroupBox1.Controls.Add(lblControlLote)
        Me.GroupBox1.Controls.Add(Me.chkEsMateriaPrima)
        Me.GroupBox1.Controls.Add(Me.chkControlLote)
        Me.GroupBox1.Controls.Add(lblEsMatPrima)
        Me.GroupBox1.Controls.Add(lblGeneraLote)
        Me.GroupBox1.Controls.Add(Me.chkGeneraLote)
        Me.GroupBox1.Location = New System.Drawing.Point(749, 37)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(195, 251)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Parametros A"
        '
        'chkFechaManufactura
        '
        Me.chkFechaManufactura.Location = New System.Drawing.Point(156, 116)
        Me.chkFechaManufactura.Margin = New System.Windows.Forms.Padding(4)
        Me.chkFechaManufactura.MenuManager = Me.RibbonControl
        Me.chkFechaManufactura.Name = "chkFechaManufactura"
        Me.chkFechaManufactura.Properties.Caption = ""
        Me.chkFechaManufactura.Size = New System.Drawing.Size(24, 24)
        Me.chkFechaManufactura.TabIndex = 9
        '
        'chkCapturarAniada
        '
        Me.chkCapturarAniada.Location = New System.Drawing.Point(156, 84)
        Me.chkCapturarAniada.Margin = New System.Windows.Forms.Padding(4)
        Me.chkCapturarAniada.MenuManager = Me.RibbonControl
        Me.chkCapturarAniada.Name = "chkCapturarAniada"
        Me.chkCapturarAniada.Properties.Caption = ""
        Me.chkCapturarAniada.Size = New System.Drawing.Size(23, 24)
        Me.chkCapturarAniada.TabIndex = 7
        '
        'chkEsKit
        '
        Me.chkEsKit.Location = New System.Drawing.Point(156, 178)
        Me.chkEsKit.Margin = New System.Windows.Forms.Padding(4)
        Me.chkEsKit.MenuManager = Me.RibbonControl
        Me.chkEsKit.Name = "chkEsKit"
        Me.chkEsKit.Properties.Caption = ""
        Me.chkEsKit.Size = New System.Drawing.Size(24, 24)
        Me.chkEsKit.TabIndex = 13
        '
        'chkEsMateriaPrima
        '
        Me.chkEsMateriaPrima.Location = New System.Drawing.Point(156, 146)
        Me.chkEsMateriaPrima.Margin = New System.Windows.Forms.Padding(4)
        Me.chkEsMateriaPrima.MenuManager = Me.RibbonControl
        Me.chkEsMateriaPrima.Name = "chkEsMateriaPrima"
        Me.chkEsMateriaPrima.Properties.Caption = ""
        Me.chkEsMateriaPrima.Size = New System.Drawing.Size(24, 24)
        Me.chkEsMateriaPrima.TabIndex = 11
        '
        'chkControlLote
        '
        Me.chkControlLote.Location = New System.Drawing.Point(156, 23)
        Me.chkControlLote.Margin = New System.Windows.Forms.Padding(4)
        Me.chkControlLote.MenuManager = Me.RibbonControl
        Me.chkControlLote.Name = "chkControlLote"
        Me.chkControlLote.Properties.Caption = ""
        Me.chkControlLote.Size = New System.Drawing.Size(23, 24)
        Me.chkControlLote.TabIndex = 1
        '
        'chkGeneraLote
        '
        Me.chkGeneraLote.Location = New System.Drawing.Point(156, 54)
        Me.chkGeneraLote.Margin = New System.Windows.Forms.Padding(4)
        Me.chkGeneraLote.MenuManager = Me.RibbonControl
        Me.chkGeneraLote.Name = "chkGeneraLote"
        Me.chkGeneraLote.Properties.Caption = ""
        Me.chkGeneraLote.Size = New System.Drawing.Size(23, 24)
        Me.chkGeneraLote.TabIndex = 3
        '
        'GrpTemperatura
        '
        Me.GrpTemperatura.Controls.Add(Me.txtTemperaturaTolerancia)
        Me.GrpTemperatura.Controls.Add(Label6)
        Me.GrpTemperatura.Controls.Add(Me.txtTemperaturaReferencia)
        Me.GrpTemperatura.Controls.Add(Label8)
        Me.GrpTemperatura.Controls.Add(lblCapturarTemperatura)
        Me.GrpTemperatura.Controls.Add(Me.chkCapturaTemperatura)
        Me.GrpTemperatura.Location = New System.Drawing.Point(416, 161)
        Me.GrpTemperatura.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpTemperatura.Name = "GrpTemperatura"
        Me.GrpTemperatura.Padding = New System.Windows.Forms.Padding(4)
        Me.GrpTemperatura.Size = New System.Drawing.Size(299, 129)
        Me.GrpTemperatura.TabIndex = 17
        Me.GrpTemperatura.TabStop = False
        '
        'txtTemperaturaTolerancia
        '
        Me.txtTemperaturaTolerancia.DecimalPlaces = 6
        Me.txtTemperaturaTolerancia.Enabled = False
        Me.txtTemperaturaTolerancia.Location = New System.Drawing.Point(118, 79)
        Me.txtTemperaturaTolerancia.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTemperaturaTolerancia.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTemperaturaTolerancia.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtTemperaturaTolerancia.Name = "txtTemperaturaTolerancia"
        Me.txtTemperaturaTolerancia.Size = New System.Drawing.Size(155, 23)
        Me.txtTemperaturaTolerancia.TabIndex = 5
        '
        'txtTemperaturaReferencia
        '
        Me.txtTemperaturaReferencia.DecimalPlaces = 6
        Me.txtTemperaturaReferencia.Enabled = False
        Me.txtTemperaturaReferencia.Location = New System.Drawing.Point(118, 48)
        Me.txtTemperaturaReferencia.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTemperaturaReferencia.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTemperaturaReferencia.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtTemperaturaReferencia.Name = "txtTemperaturaReferencia"
        Me.txtTemperaturaReferencia.Size = New System.Drawing.Size(155, 23)
        Me.txtTemperaturaReferencia.TabIndex = 3
        '
        'chkCapturaTemperatura
        '
        Me.chkCapturaTemperatura.Location = New System.Drawing.Point(118, 17)
        Me.chkCapturaTemperatura.Margin = New System.Windows.Forms.Padding(4)
        Me.chkCapturaTemperatura.MenuManager = Me.RibbonControl
        Me.chkCapturaTemperatura.Name = "chkCapturaTemperatura"
        Me.chkCapturaTemperatura.Properties.Caption = ""
        Me.chkCapturaTemperatura.Size = New System.Drawing.Size(41, 24)
        Me.chkCapturaTemperatura.TabIndex = 1
        '
        'GrpPeso
        '
        Me.GrpPeso.Controls.Add(Me.txtPesoTolerancia)
        Me.GrpPeso.Controls.Add(Label5)
        Me.GrpPeso.Controls.Add(Me.txtPesoReferencia)
        Me.GrpPeso.Controls.Add(Label4)
        Me.GrpPeso.Controls.Add(lblCapturarPeso)
        Me.GrpPeso.Controls.Add(Me.chkCapturarPeso)
        Me.GrpPeso.Location = New System.Drawing.Point(416, 33)
        Me.GrpPeso.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpPeso.Name = "GrpPeso"
        Me.GrpPeso.Padding = New System.Windows.Forms.Padding(4)
        Me.GrpPeso.Size = New System.Drawing.Size(299, 124)
        Me.GrpPeso.TabIndex = 7
        Me.GrpPeso.TabStop = False
        '
        'txtPesoTolerancia
        '
        Me.txtPesoTolerancia.DecimalPlaces = 6
        Me.txtPesoTolerancia.Enabled = False
        Me.txtPesoTolerancia.Location = New System.Drawing.Point(117, 80)
        Me.txtPesoTolerancia.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPesoTolerancia.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtPesoTolerancia.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtPesoTolerancia.Name = "txtPesoTolerancia"
        Me.txtPesoTolerancia.Size = New System.Drawing.Size(154, 23)
        Me.txtPesoTolerancia.TabIndex = 5
        '
        'txtPesoReferencia
        '
        Me.txtPesoReferencia.DecimalPlaces = 6
        Me.txtPesoReferencia.Enabled = False
        Me.txtPesoReferencia.Location = New System.Drawing.Point(118, 48)
        Me.txtPesoReferencia.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPesoReferencia.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtPesoReferencia.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtPesoReferencia.Name = "txtPesoReferencia"
        Me.txtPesoReferencia.Size = New System.Drawing.Size(154, 23)
        Me.txtPesoReferencia.TabIndex = 3
        '
        'chkCapturarPeso
        '
        Me.chkCapturarPeso.Location = New System.Drawing.Point(117, 17)
        Me.chkCapturarPeso.Margin = New System.Windows.Forms.Padding(4)
        Me.chkCapturarPeso.MenuManager = Me.RibbonControl
        Me.chkCapturarPeso.Name = "chkCapturarPeso"
        Me.chkCapturarPeso.Properties.Caption = ""
        Me.chkCapturarPeso.Size = New System.Drawing.Size(41, 24)
        Me.chkCapturarPeso.TabIndex = 1
        '
        'txtPrecio
        '
        Me.txtPrecio.DecimalPlaces = 6
        Me.txtPrecio.Location = New System.Drawing.Point(153, 182)
        Me.txtPrecio.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPrecio.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtPrecio.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtPrecio.Name = "txtPrecio"
        Me.txtPrecio.Size = New System.Drawing.Size(223, 23)
        Me.txtPrecio.TabIndex = 10
        '
        'txtCosto
        '
        Me.txtCosto.DecimalPlaces = 6
        Me.txtCosto.Location = New System.Drawing.Point(153, 149)
        Me.txtCosto.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCosto.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCosto.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtCosto.Name = "txtCosto"
        Me.txtCosto.Size = New System.Drawing.Size(223, 23)
        Me.txtCosto.TabIndex = 8
        '
        'txtExistenciaMaxima
        '
        Me.txtExistenciaMaxima.DecimalPlaces = 6
        Me.txtExistenciaMaxima.Location = New System.Drawing.Point(153, 116)
        Me.txtExistenciaMaxima.Margin = New System.Windows.Forms.Padding(4)
        Me.txtExistenciaMaxima.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtExistenciaMaxima.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtExistenciaMaxima.Name = "txtExistenciaMaxima"
        Me.txtExistenciaMaxima.Size = New System.Drawing.Size(223, 23)
        Me.txtExistenciaMaxima.TabIndex = 5
        '
        'txtExitenciaMinima
        '
        Me.txtExitenciaMinima.DecimalPlaces = 6
        Me.txtExitenciaMinima.Location = New System.Drawing.Point(153, 82)
        Me.txtExitenciaMinima.Margin = New System.Windows.Forms.Padding(4)
        Me.txtExitenciaMinima.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtExitenciaMinima.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtExitenciaMinima.Name = "txtExitenciaMinima"
        Me.txtExitenciaMinima.Size = New System.Drawing.Size(222, 23)
        Me.txtExitenciaMinima.TabIndex = 3
        '
        'TabParametros
        '
        Me.TabParametros.Controls.Add(Me.GprParametro)
        Me.TabParametros.Margin = New System.Windows.Forms.Padding(4)
        Me.TabParametros.Name = "TabParametros"
        Me.TabParametros.Size = New System.Drawing.Size(1552, 603)
        Me.TabParametros.Text = "Parámetros"
        '
        'GprParametro
        '
        Me.GprParametro.Controls.Add(Me.cmbParametro)
        Me.GprParametro.Controls.Add(Label1)
        Me.GprParametro.Controls.Add(Me.chkActivarParametro)
        Me.GprParametro.Controls.Add(Me.ToolStripP)
        Me.GprParametro.Controls.Add(Me.cmdNuevoParametro)
        Me.GprParametro.Controls.Add(Me.rdCapturarSiempre)
        Me.GprParametro.Controls.Add(Me.rdCapturarUnaVez)
        Me.GprParametro.Controls.Add(Me.GrpParametro)
        Me.GprParametro.Controls.Add(Me.txtTipo)
        Me.GprParametro.Controls.Add(Label13)
        Me.GprParametro.Controls.Add(Label10)
        Me.GprParametro.Controls.Add(Label11)
        Me.GprParametro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GprParametro.Location = New System.Drawing.Point(0, 0)
        Me.GprParametro.Margin = New System.Windows.Forms.Padding(4)
        Me.GprParametro.Name = "GprParametro"
        Me.GprParametro.Size = New System.Drawing.Size(1552, 603)
        Me.GprParametro.TabIndex = 0
        Me.GprParametro.Tag = ""
        '
        'cmbParametro
        '
        Me.cmbParametro.Location = New System.Drawing.Point(174, 69)
        Me.cmbParametro.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbParametro.MenuManager = Me.RibbonControl
        Me.cmbParametro.Name = "cmbParametro"
        Me.cmbParametro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbParametro.Properties.NullText = ""
        Me.cmbParametro.Size = New System.Drawing.Size(276, 22)
        Me.cmbParametro.TabIndex = 12
        '
        'chkActivarParametro
        '
        Me.chkActivarParametro.EditValue = True
        Me.chkActivarParametro.Location = New System.Drawing.Point(173, 203)
        Me.chkActivarParametro.Margin = New System.Windows.Forms.Padding(4)
        Me.chkActivarParametro.MenuManager = Me.RibbonControl
        Me.chkActivarParametro.Name = "chkActivarParametro"
        Me.chkActivarParametro.Properties.Caption = ""
        Me.chkActivarParametro.Size = New System.Drawing.Size(117, 24)
        Me.chkActivarParametro.TabIndex = 10
        '
        'ToolStripP
        '
        Me.ToolStripP.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStripP.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdNewP, Me.cmdSaveP, Me.cmdDesactivarParametro})
        Me.ToolStripP.Location = New System.Drawing.Point(2, 28)
        Me.ToolStripP.Name = "ToolStripP"
        Me.ToolStripP.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStripP.Size = New System.Drawing.Size(1548, 27)
        Me.ToolStripP.TabIndex = 0
        Me.ToolStripP.Text = "ToolStrip1"
        '
        'cmdNewP
        '
        Me.cmdNewP.Image = CType(resources.GetObject("cmdNewP.Image"), System.Drawing.Image)
        Me.cmdNewP.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdNewP.Name = "cmdNewP"
        Me.cmdNewP.Size = New System.Drawing.Size(76, 24)
        Me.cmdNewP.Text = "Nuevo"
        '
        'cmdSaveP
        '
        Me.cmdSaveP.Image = CType(resources.GetObject("cmdSaveP.Image"), System.Drawing.Image)
        Me.cmdSaveP.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSaveP.Name = "cmdSaveP"
        Me.cmdSaveP.Size = New System.Drawing.Size(86, 24)
        Me.cmdSaveP.Text = "Guardar"
        '
        'cmdDesactivarParametro
        '
        Me.cmdDesactivarParametro.Image = CType(resources.GetObject("cmdDesactivarParametro.Image"), System.Drawing.Image)
        Me.cmdDesactivarParametro.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDesactivarParametro.Name = "cmdDesactivarParametro"
        Me.cmdDesactivarParametro.Size = New System.Drawing.Size(102, 24)
        Me.cmdDesactivarParametro.Text = "Desactivar"
        '
        'cmdNuevoParametro
        '
        Me.cmdNuevoParametro.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdNuevoParametro.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNuevoParametro.Appearance.Options.UseFont = True
        Me.cmdNuevoParametro.Location = New System.Drawing.Point(667, 70)
        Me.cmdNuevoParametro.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdNuevoParametro.Name = "cmdNuevoParametro"
        Me.cmdNuevoParametro.Size = New System.Drawing.Size(88, 25)
        Me.cmdNuevoParametro.TabIndex = 3
        Me.cmdNuevoParametro.Text = "Nuevo"
        '
        'rdCapturarSiempre
        '
        Me.rdCapturarSiempre.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdCapturarSiempre.AutoSize = True
        Me.rdCapturarSiempre.Location = New System.Drawing.Point(312, 175)
        Me.rdCapturarSiempre.Margin = New System.Windows.Forms.Padding(4)
        Me.rdCapturarSiempre.Name = "rdCapturarSiempre"
        Me.rdCapturarSiempre.Size = New System.Drawing.Size(130, 20)
        Me.rdCapturarSiempre.TabIndex = 8
        Me.rdCapturarSiempre.TabStop = True
        Me.rdCapturarSiempre.Text = "Capturar Siempre"
        Me.rdCapturarSiempre.UseVisualStyleBackColor = True
        '
        'rdCapturarUnaVez
        '
        Me.rdCapturarUnaVez.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdCapturarUnaVez.AutoSize = True
        Me.rdCapturarUnaVez.Checked = True
        Me.rdCapturarUnaVez.Location = New System.Drawing.Point(174, 175)
        Me.rdCapturarUnaVez.Margin = New System.Windows.Forms.Padding(4)
        Me.rdCapturarUnaVez.Name = "rdCapturarUnaVez"
        Me.rdCapturarUnaVez.Size = New System.Drawing.Size(126, 20)
        Me.rdCapturarUnaVez.TabIndex = 7
        Me.rdCapturarUnaVez.TabStop = True
        Me.rdCapturarUnaVez.Text = "Capturar una vez"
        Me.rdCapturarUnaVez.UseVisualStyleBackColor = True
        '
        'GrpParametro
        '
        Me.GrpParametro.Controls.Add(Label34)
        Me.GrpParametro.Controls.Add(Me.DgridParametros)
        Me.GrpParametro.Controls.Add(Me.chkActivoParametro)
        Me.GrpParametro.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GrpParametro.Location = New System.Drawing.Point(2, 599)
        Me.GrpParametro.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpParametro.Name = "GrpParametro"
        Me.GrpParametro.Size = New System.Drawing.Size(1548, 2)
        Me.GrpParametro.TabIndex = 11
        Me.GrpParametro.Text = "Detalle Parámetro"
        '
        'DgridParametros
        '
        Me.DgridParametros.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DgridParametros.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridParametros.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode2.RelationName = "Level1"
        Me.DgridParametros.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.DgridParametros.Location = New System.Drawing.Point(2, 58)
        Me.DgridParametros.MainView = Me.GridViewP
        Me.DgridParametros.Margin = New System.Windows.Forms.Padding(4)
        Me.DgridParametros.MenuManager = Me.RibbonControl
        Me.DgridParametros.Name = "DgridParametros"
        Me.DgridParametros.Size = New System.Drawing.Size(1542, 278)
        Me.DgridParametros.TabIndex = 2
        Me.DgridParametros.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewP, Me.GridView9})
        '
        'GridViewP
        '
        Me.GridViewP.DetailHeight = 431
        Me.GridViewP.GridControl = Me.DgridParametros
        Me.GridViewP.Name = "GridViewP"
        Me.GridViewP.OptionsBehavior.Editable = False
        Me.GridViewP.OptionsFind.AlwaysVisible = True
        Me.GridViewP.OptionsView.ShowGroupPanel = False
        '
        'GridView9
        '
        Me.GridView9.DetailHeight = 431
        Me.GridView9.GridControl = Me.DgridParametros
        Me.GridView9.Name = "GridView9"
        '
        'chkActivoParametro
        '
        Me.chkActivoParametro.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkActivoParametro.EditValue = True
        Me.chkActivoParametro.Location = New System.Drawing.Point(1512, 30)
        Me.chkActivoParametro.Margin = New System.Windows.Forms.Padding(4)
        Me.chkActivoParametro.MenuManager = Me.RibbonControl
        Me.chkActivoParametro.Name = "chkActivoParametro"
        Me.chkActivoParametro.Properties.Caption = ""
        Me.chkActivoParametro.Size = New System.Drawing.Size(21, 24)
        Me.chkActivoParametro.TabIndex = 1
        '
        'txtTipo
        '
        Me.txtTipo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTipo.Location = New System.Drawing.Point(174, 103)
        Me.txtTipo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTipo.MenuManager = Me.RibbonControl
        Me.txtTipo.Name = "txtTipo"
        Me.txtTipo.Properties.ReadOnly = True
        Me.txtTipo.Size = New System.Drawing.Size(485, 22)
        Me.txtTipo.TabIndex = 5
        '
        'TabCodigoBarra
        '
        Me.TabCodigoBarra.Controls.Add(Me.GrpCodigoBarra)
        Me.TabCodigoBarra.Margin = New System.Windows.Forms.Padding(4)
        Me.TabCodigoBarra.Name = "TabCodigoBarra"
        Me.TabCodigoBarra.Size = New System.Drawing.Size(1552, 603)
        Me.TabCodigoBarra.Text = "Código de Barra"
        '
        'GrpCodigoBarra
        '
        Me.GrpCodigoBarra.Controls.Add(Me.cmbProveedor)
        Me.GrpCodigoBarra.Controls.Add(Me.GrdCodigoBarra)
        Me.GrpCodigoBarra.Controls.Add(Label3)
        Me.GrpCodigoBarra.Controls.Add(Me.chkActivarCB)
        Me.GrpCodigoBarra.Controls.Add(Me.ToolStripC)
        Me.GrpCodigoBarra.Controls.Add(ProductoLabel)
        Me.GrpCodigoBarra.Controls.Add(Me.GroupControl1)
        Me.GrpCodigoBarra.Controls.Add(NombreLabel)
        Me.GrpCodigoBarra.Controls.Add(Me.txtCodigoBarraL)
        Me.GrpCodigoBarra.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpCodigoBarra.Location = New System.Drawing.Point(0, 0)
        Me.GrpCodigoBarra.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpCodigoBarra.Name = "GrpCodigoBarra"
        Me.GrpCodigoBarra.Size = New System.Drawing.Size(1552, 603)
        Me.GrpCodigoBarra.TabIndex = 0
        '
        'cmbProveedor
        '
        Me.cmbProveedor.Location = New System.Drawing.Point(178, 70)
        Me.cmbProveedor.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbProveedor.MenuManager = Me.RibbonControl
        Me.cmbProveedor.Name = "cmbProveedor"
        Me.cmbProveedor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbProveedor.Properties.NullText = ""
        Me.cmbProveedor.Size = New System.Drawing.Size(265, 22)
        Me.cmbProveedor.TabIndex = 8
        '
        'GrdCodigoBarra
        '
        Me.GrdCodigoBarra.Cursor = System.Windows.Forms.Cursors.Default
        Me.GrdCodigoBarra.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GrdCodigoBarra.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode3.RelationName = "Level1"
        Me.GrdCodigoBarra.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode3})
        Me.GrdCodigoBarra.Location = New System.Drawing.Point(2, 186)
        Me.GrdCodigoBarra.MainView = Me.GridViewCB
        Me.GrdCodigoBarra.Margin = New System.Windows.Forms.Padding(4)
        Me.GrdCodigoBarra.MenuManager = Me.RibbonControl
        Me.GrdCodigoBarra.Name = "GrdCodigoBarra"
        Me.GrdCodigoBarra.Size = New System.Drawing.Size(1548, 413)
        Me.GrdCodigoBarra.TabIndex = 2
        Me.GrdCodigoBarra.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewCB, Me.GridView8})
        '
        'GridViewCB
        '
        Me.GridViewCB.DetailHeight = 431
        Me.GridViewCB.GridControl = Me.GrdCodigoBarra
        Me.GridViewCB.Name = "GridViewCB"
        Me.GridViewCB.OptionsBehavior.Editable = False
        Me.GridViewCB.OptionsFind.AlwaysVisible = True
        Me.GridViewCB.OptionsView.ShowGroupPanel = False
        '
        'GridView8
        '
        Me.GridView8.DetailHeight = 431
        Me.GridView8.GridControl = Me.GrdCodigoBarra
        Me.GridView8.Name = "GridView8"
        '
        'chkActivarCB
        '
        Me.chkActivarCB.EditValue = True
        Me.chkActivarCB.Location = New System.Drawing.Point(178, 135)
        Me.chkActivarCB.Margin = New System.Windows.Forms.Padding(4)
        Me.chkActivarCB.MenuManager = Me.RibbonControl
        Me.chkActivarCB.Name = "chkActivarCB"
        Me.chkActivarCB.Properties.Caption = ""
        Me.chkActivarCB.Size = New System.Drawing.Size(43, 24)
        Me.chkActivarCB.TabIndex = 6
        '
        'ToolStripC
        '
        Me.ToolStripC.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStripC.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdNewC, Me.cmdSaveC, Me.cmdDesactivarCodigoBarra})
        Me.ToolStripC.Location = New System.Drawing.Point(2, 28)
        Me.ToolStripC.Name = "ToolStripC"
        Me.ToolStripC.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStripC.Size = New System.Drawing.Size(1548, 27)
        Me.ToolStripC.TabIndex = 0
        Me.ToolStripC.Text = "ToolStrip2"
        '
        'cmdNewC
        '
        Me.cmdNewC.Image = CType(resources.GetObject("cmdNewC.Image"), System.Drawing.Image)
        Me.cmdNewC.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdNewC.Name = "cmdNewC"
        Me.cmdNewC.Size = New System.Drawing.Size(76, 24)
        Me.cmdNewC.Text = "Nuevo"
        '
        'cmdSaveC
        '
        Me.cmdSaveC.Image = CType(resources.GetObject("cmdSaveC.Image"), System.Drawing.Image)
        Me.cmdSaveC.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSaveC.Name = "cmdSaveC"
        Me.cmdSaveC.Size = New System.Drawing.Size(86, 24)
        Me.cmdSaveC.Text = "Guardar"
        '
        'cmdDesactivarCodigoBarra
        '
        Me.cmdDesactivarCodigoBarra.Image = CType(resources.GetObject("cmdDesactivarCodigoBarra.Image"), System.Drawing.Image)
        Me.cmdDesactivarCodigoBarra.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDesactivarCodigoBarra.Name = "cmdDesactivarCodigoBarra"
        Me.cmdDesactivarCodigoBarra.Size = New System.Drawing.Size(102, 24)
        Me.cmdDesactivarCodigoBarra.Text = "Desactivar"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Label29)
        Me.GroupControl1.Controls.Add(Me.chkActivoCB)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl1.Location = New System.Drawing.Point(2, 599)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1548, 2)
        Me.GroupControl1.TabIndex = 7
        Me.GroupControl1.Text = "Detalle Código Barra"
        '
        'chkActivoCB
        '
        Me.chkActivoCB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkActivoCB.EditValue = True
        Me.chkActivoCB.Location = New System.Drawing.Point(1512, 28)
        Me.chkActivoCB.Margin = New System.Windows.Forms.Padding(4)
        Me.chkActivoCB.MenuManager = Me.RibbonControl
        Me.chkActivoCB.Name = "chkActivoCB"
        Me.chkActivoCB.Properties.Caption = ""
        Me.chkActivoCB.Size = New System.Drawing.Size(21, 24)
        Me.chkActivoCB.TabIndex = 1
        '
        'txtCodigoBarraL
        '
        Me.txtCodigoBarraL.Location = New System.Drawing.Point(178, 103)
        Me.txtCodigoBarraL.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodigoBarraL.MenuManager = Me.RibbonControl
        Me.txtCodigoBarraL.Name = "txtCodigoBarraL"
        Me.txtCodigoBarraL.Size = New System.Drawing.Size(265, 22)
        Me.txtCodigoBarraL.TabIndex = 4
        '
        'TabProductoBodega
        '
        Me.TabProductoBodega.Controls.Add(Me.GrpPB)
        Me.TabProductoBodega.Margin = New System.Windows.Forms.Padding(4)
        Me.TabProductoBodega.Name = "TabProductoBodega"
        Me.TabProductoBodega.Size = New System.Drawing.Size(1552, 603)
        Me.TabProductoBodega.Text = "Asociación Bodega"
        '
        'GrpPB
        '
        Me.GrpPB.Controls.Add(Me.GroupControl3)
        Me.GrpPB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpPB.Location = New System.Drawing.Point(0, 0)
        Me.GrpPB.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpPB.Name = "GrpPB"
        Me.GrpPB.Size = New System.Drawing.Size(1552, 603)
        Me.GrpPB.TabIndex = 0
        Me.GrpPB.Tag = ""
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.dgridProductoBodega)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(2, 28)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(1548, 573)
        Me.GroupControl3.TabIndex = 0
        Me.GroupControl3.Text = "Selección de Bodega"
        '
        'dgridProductoBodega
        '
        Me.dgridProductoBodega.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgridProductoBodega.DataSource = Me.DataBindingSource
        Me.dgridProductoBodega.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridProductoBodega.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode4.RelationName = "Level1"
        Me.dgridProductoBodega.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode4})
        Me.dgridProductoBodega.Location = New System.Drawing.Point(2, 28)
        Me.dgridProductoBodega.MainView = Me.GrdProductoBodega
        Me.dgridProductoBodega.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridProductoBodega.Name = "dgridProductoBodega"
        Me.dgridProductoBodega.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2})
        Me.dgridProductoBodega.Size = New System.Drawing.Size(1544, 543)
        Me.dgridProductoBodega.TabIndex = 0
        Me.dgridProductoBodega.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GrdProductoBodega, Me.GridView7})
        '
        'GrdProductoBodega
        '
        Me.GrdProductoBodega.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSeleccion, Me.colIdBodega, Me.IdProductoBodega, Me.colBodega, Me.IdInterno})
        Me.GrdProductoBodega.DetailHeight = 431
        Me.GrdProductoBodega.GridControl = Me.dgridProductoBodega
        Me.GrdProductoBodega.Name = "GrdProductoBodega"
        Me.GrdProductoBodega.OptionsFind.AlwaysVisible = True
        '
        'colSeleccion
        '
        Me.colSeleccion.Caption = "Asignar"
        Me.colSeleccion.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.colSeleccion.FieldName = "Selección"
        Me.colSeleccion.MinWidth = 23
        Me.colSeleccion.Name = "colSeleccion"
        Me.colSeleccion.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccion.OptionsFilter.ImmediateUpdatePopupDateFilterOnCheck = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccion.OptionsFilter.ImmediateUpdatePopupDateFilterOnDateChange = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccion.OptionsFilter.ShowBlanksFilterItems = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccion.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.colSeleccion.Visible = True
        Me.colSeleccion.VisibleIndex = 0
        Me.colSeleccion.Width = 87
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'colIdBodega
        '
        Me.colIdBodega.FieldName = "IdBodega"
        Me.colIdBodega.MinWidth = 23
        Me.colIdBodega.Name = "colIdBodega"
        Me.colIdBodega.OptionsColumn.ReadOnly = True
        Me.colIdBodega.Width = 87
        '
        'IdProductoBodega
        '
        Me.IdProductoBodega.Caption = "Asignación"
        Me.IdProductoBodega.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.IdProductoBodega.FieldName = "IdProductoBodega"
        Me.IdProductoBodega.MinWidth = 23
        Me.IdProductoBodega.Name = "IdProductoBodega"
        Me.IdProductoBodega.OptionsColumn.ReadOnly = True
        Me.IdProductoBodega.Width = 87
        '
        'colBodega
        '
        Me.colBodega.FieldName = "Bodega"
        Me.colBodega.MinWidth = 23
        Me.colBodega.Name = "colBodega"
        Me.colBodega.OptionsColumn.AllowEdit = False
        Me.colBodega.OptionsColumn.ReadOnly = True
        Me.colBodega.Visible = True
        Me.colBodega.VisibleIndex = 1
        Me.colBodega.Width = 87
        '
        'IdInterno
        '
        Me.IdInterno.Caption = "IdInterno"
        Me.IdInterno.FieldName = "IdInterno"
        Me.IdInterno.MinWidth = 23
        Me.IdInterno.Name = "IdInterno"
        Me.IdInterno.OptionsColumn.ReadOnly = True
        Me.IdInterno.Width = 87
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'GridView7
        '
        Me.GridView7.DetailHeight = 431
        Me.GridView7.GridControl = Me.dgridProductoBodega
        Me.GridView7.Name = "GridView7"
        '
        'TabPresentacion
        '
        Me.TabPresentacion.Controls.Add(Me.SplitContainer1)
        Me.TabPresentacion.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPresentacion.Name = "TabPresentacion"
        Me.TabPresentacion.Size = New System.Drawing.Size(1552, 603)
        Me.TabPresentacion.Text = "Presentaciones"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.GrpPresentacion)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupControl5)
        Me.SplitContainer1.Size = New System.Drawing.Size(1552, 603)
        Me.SplitContainer1.SplitterDistance = 372
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 0
        '
        'GrpPresentacion
        '
        Me.GrpPresentacion.Controls.Add(Me.cmbEtiquetaPresentacion)
        Me.GrpPresentacion.Controls.Add(Me.lblEtiquetaPresentacion)
        Me.GrpPresentacion.Controls.Add(Me.lblSistema)
        Me.GrpPresentacion.Controls.Add(Me.chkSistema)
        Me.GrpPresentacion.Controls.Add(Me.chkGeneraLPAuto)
        Me.GrpPresentacion.Controls.Add(Me.chkPermitirPaletizar)
        Me.GrpPresentacion.Controls.Add(Me.ChkEsPallet)
        Me.GrpPresentacion.Controls.Add(Me.GroupBox7)
        Me.GrpPresentacion.Controls.Add(Me.grpConfigPallet)
        Me.GrpPresentacion.Controls.Add(Label7)
        Me.GrpPresentacion.Controls.Add(Me.chkActivarPR)
        Me.GrpPresentacion.Controls.Add(Me.ToolStripPR)
        Me.GrpPresentacion.Controls.Add(Label28)
        Me.GrpPresentacion.Controls.Add(Me.txtCodigoBarraPresentacion)
        Me.GrpPresentacion.Controls.Add(Me.txtInfo)
        Me.GrpPresentacion.Controls.Add(Me.txtFactor)
        Me.GrpPresentacion.Controls.Add(lblFactor)
        Me.GrpPresentacion.Controls.Add(Me.chkImprimeBarra)
        Me.GrpPresentacion.Controls.Add(Me.txtAncho)
        Me.GrpPresentacion.Controls.Add(Me.txtLargo)
        Me.GrpPresentacion.Controls.Add(Me.txtNombrePresentacion)
        Me.GrpPresentacion.Controls.Add(Label18)
        Me.GrpPresentacion.Controls.Add(Me.txtAlto)
        Me.GrpPresentacion.Controls.Add(Me.txtPeso)
        Me.GrpPresentacion.Controls.Add(lblAncho)
        Me.GrpPresentacion.Controls.Add(lblLargo)
        Me.GrpPresentacion.Controls.Add(lblAlto)
        Me.GrpPresentacion.Controls.Add(lblPeso)
        Me.GrpPresentacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpPresentacion.Location = New System.Drawing.Point(0, 0)
        Me.GrpPresentacion.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpPresentacion.Name = "GrpPresentacion"
        Me.GrpPresentacion.Size = New System.Drawing.Size(1552, 372)
        Me.GrpPresentacion.TabIndex = 0
        '
        'cmbEtiquetaPresentacion
        '
        Me.cmbEtiquetaPresentacion.Location = New System.Drawing.Point(1048, 311)
        Me.cmbEtiquetaPresentacion.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbEtiquetaPresentacion.MenuManager = Me.RibbonControl
        Me.cmbEtiquetaPresentacion.Name = "cmbEtiquetaPresentacion"
        Me.cmbEtiquetaPresentacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEtiquetaPresentacion.Properties.NullText = ""
        Me.cmbEtiquetaPresentacion.Size = New System.Drawing.Size(190, 22)
        Me.cmbEtiquetaPresentacion.TabIndex = 38
        '
        'lblEtiquetaPresentacion
        '
        Me.lblEtiquetaPresentacion.AutoSize = True
        Me.lblEtiquetaPresentacion.Location = New System.Drawing.Point(941, 317)
        Me.lblEtiquetaPresentacion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEtiquetaPresentacion.Name = "lblEtiquetaPresentacion"
        Me.lblEtiquetaPresentacion.Size = New System.Drawing.Size(53, 16)
        Me.lblEtiquetaPresentacion.TabIndex = 37
        Me.lblEtiquetaPresentacion.Text = "Etiqueta"
        '
        'lblSistema
        '
        Me.lblSistema.AutoSize = True
        Me.lblSistema.Location = New System.Drawing.Point(40, 335)
        Me.lblSistema.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSistema.Name = "lblSistema"
        Me.lblSistema.Size = New System.Drawing.Size(58, 16)
        Me.lblSistema.TabIndex = 36
        Me.lblSistema.Text = "Sistema:"
        '
        'chkSistema
        '
        Me.chkSistema.Location = New System.Drawing.Point(135, 331)
        Me.chkSistema.Margin = New System.Windows.Forms.Padding(4)
        Me.chkSistema.MenuManager = Me.RibbonControl
        Me.chkSistema.Name = "chkSistema"
        Me.chkSistema.Properties.Caption = ""
        Me.chkSistema.Size = New System.Drawing.Size(37, 24)
        Me.chkSistema.TabIndex = 35
        '
        'chkGeneraLPAuto
        '
        Me.chkGeneraLPAuto.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkGeneraLPAuto.Location = New System.Drawing.Point(561, 65)
        Me.chkGeneraLPAuto.Margin = New System.Windows.Forms.Padding(4)
        Me.chkGeneraLPAuto.MenuManager = Me.RibbonControl
        Me.chkGeneraLPAuto.Name = "chkGeneraLPAuto"
        Me.chkGeneraLPAuto.Properties.Caption = "Genera Licencia Auto"
        Me.chkGeneraLPAuto.Size = New System.Drawing.Size(156, 24)
        Me.chkGeneraLPAuto.TabIndex = 34
        Me.chkGeneraLPAuto.ToolTip = "Índica si se generará un correlativo único para cada pallet o se ingresará el núm" &
    "ero de pallet."
        '
        'chkPermitirPaletizar
        '
        Me.chkPermitirPaletizar.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkPermitirPaletizar.EditValue = True
        Me.chkPermitirPaletizar.Location = New System.Drawing.Point(730, 65)
        Me.chkPermitirPaletizar.Margin = New System.Windows.Forms.Padding(4)
        Me.chkPermitirPaletizar.MenuManager = Me.RibbonControl
        Me.chkPermitirPaletizar.Name = "chkPermitirPaletizar"
        Me.chkPermitirPaletizar.Properties.Caption = "Permitir paletizar"
        Me.chkPermitirPaletizar.Size = New System.Drawing.Size(122, 24)
        Me.chkPermitirPaletizar.TabIndex = 33
        Me.chkPermitirPaletizar.ToolTip = "Índica si la presentación puede paletizarse, " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "éste parámetro es excluyente con e" &
    "l parámetro ""Es Pallet""" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'ChkEsPallet
        '
        Me.ChkEsPallet.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ChkEsPallet.Location = New System.Drawing.Point(465, 65)
        Me.ChkEsPallet.Margin = New System.Windows.Forms.Padding(4)
        Me.ChkEsPallet.MenuManager = Me.RibbonControl
        Me.ChkEsPallet.Name = "ChkEsPallet"
        Me.ChkEsPallet.Properties.Caption = "Es Pallet"
        Me.ChkEsPallet.Size = New System.Drawing.Size(77, 24)
        Me.ChkEsPallet.TabIndex = 32
        Me.ChkEsPallet.ToolTip = "Índica si la presentación tendra control como pallet"
        '
        'GroupBox7
        '
        Me.GroupBox7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox7.Controls.Add(Me.txtMaximoPeso)
        Me.GroupBox7.Controls.Add(Me.txtMinimoPeso)
        Me.GroupBox7.Controls.Add(Me.txtMinimoExistencia)
        Me.GroupBox7.Controls.Add(Me.txtMaximoExistencia)
        Me.GroupBox7.Controls.Add(Label44)
        Me.GroupBox7.Controls.Add(Label43)
        Me.GroupBox7.Controls.Add(Label20)
        Me.GroupBox7.Controls.Add(Label19)
        Me.GroupBox7.Location = New System.Drawing.Point(876, 90)
        Me.GroupBox7.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox7.Size = New System.Drawing.Size(407, 213)
        Me.GroupBox7.TabIndex = 31
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Mínimos y Máximos"
        '
        'txtMaximoPeso
        '
        Me.txtMaximoPeso.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMaximoPeso.DecimalPlaces = 6
        Me.txtMaximoPeso.Location = New System.Drawing.Point(144, 135)
        Me.txtMaximoPeso.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMaximoPeso.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtMaximoPeso.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtMaximoPeso.Name = "txtMaximoPeso"
        Me.txtMaximoPeso.Size = New System.Drawing.Size(218, 23)
        Me.txtMaximoPeso.TabIndex = 21
        '
        'txtMinimoPeso
        '
        Me.txtMinimoPeso.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMinimoPeso.DecimalPlaces = 6
        Me.txtMinimoPeso.Location = New System.Drawing.Point(144, 102)
        Me.txtMinimoPeso.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMinimoPeso.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtMinimoPeso.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtMinimoPeso.Name = "txtMinimoPeso"
        Me.txtMinimoPeso.Size = New System.Drawing.Size(218, 23)
        Me.txtMinimoPeso.TabIndex = 15
        '
        'txtMinimoExistencia
        '
        Me.txtMinimoExistencia.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMinimoExistencia.DecimalPlaces = 6
        Me.txtMinimoExistencia.Location = New System.Drawing.Point(144, 32)
        Me.txtMinimoExistencia.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMinimoExistencia.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtMinimoExistencia.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtMinimoExistencia.Name = "txtMinimoExistencia"
        Me.txtMinimoExistencia.Size = New System.Drawing.Size(218, 23)
        Me.txtMinimoExistencia.TabIndex = 13
        '
        'txtMaximoExistencia
        '
        Me.txtMaximoExistencia.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMaximoExistencia.DecimalPlaces = 6
        Me.txtMaximoExistencia.Location = New System.Drawing.Point(144, 65)
        Me.txtMaximoExistencia.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMaximoExistencia.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtMaximoExistencia.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtMaximoExistencia.Name = "txtMaximoExistencia"
        Me.txtMaximoExistencia.Size = New System.Drawing.Size(218, 23)
        Me.txtMaximoExistencia.TabIndex = 19
        '
        'grpConfigPallet
        '
        Me.grpConfigPallet.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.grpConfigPallet.Controls.Add(Me.cmbPresentacionPallet)
        Me.grpConfigPallet.Controls.Add(Me.txtCamasPorTarima)
        Me.grpConfigPallet.Controls.Add(lblPresentacionPallet)
        Me.grpConfigPallet.Controls.Add(Me.txtCajasPorCama)
        Me.grpConfigPallet.Controls.Add(Me.lblY)
        Me.grpConfigPallet.Controls.Add(Me.lblX)
        Me.grpConfigPallet.Location = New System.Drawing.Point(465, 90)
        Me.grpConfigPallet.Margin = New System.Windows.Forms.Padding(4)
        Me.grpConfigPallet.Name = "grpConfigPallet"
        Me.grpConfigPallet.Padding = New System.Windows.Forms.Padding(4)
        Me.grpConfigPallet.Size = New System.Drawing.Size(350, 137)
        Me.grpConfigPallet.TabIndex = 30
        Me.grpConfigPallet.TabStop = False
        '
        'cmbPresentacionPallet
        '
        Me.cmbPresentacionPallet.Location = New System.Drawing.Point(112, 94)
        Me.cmbPresentacionPallet.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPresentacionPallet.MenuManager = Me.RibbonControl
        Me.cmbPresentacionPallet.Name = "cmbPresentacionPallet"
        Me.cmbPresentacionPallet.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPresentacionPallet.Properties.NullText = ""
        Me.cmbPresentacionPallet.Size = New System.Drawing.Size(218, 22)
        Me.cmbPresentacionPallet.TabIndex = 38
        '
        'txtCamasPorTarima
        '
        Me.txtCamasPorTarima.Location = New System.Drawing.Point(112, 60)
        Me.txtCamasPorTarima.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCamasPorTarima.Name = "txtCamasPorTarima"
        Me.txtCamasPorTarima.Size = New System.Drawing.Size(218, 23)
        Me.txtCamasPorTarima.TabIndex = 3
        '
        'txtCajasPorCama
        '
        Me.txtCajasPorCama.Location = New System.Drawing.Point(112, 26)
        Me.txtCajasPorCama.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCajasPorCama.Name = "txtCajasPorCama"
        Me.txtCajasPorCama.Size = New System.Drawing.Size(218, 23)
        Me.txtCajasPorCama.TabIndex = 2
        '
        'lblY
        '
        Me.lblY.AutoSize = True
        Me.lblY.Location = New System.Drawing.Point(8, 60)
        Me.lblY.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblY.Name = "lblY"
        Me.lblY.Size = New System.Drawing.Size(108, 16)
        Me.lblY.TabIndex = 1
        Me.lblY.Text = "Camas X Tarima:"
        '
        'lblX
        '
        Me.lblX.AutoSize = True
        Me.lblX.Location = New System.Drawing.Point(8, 26)
        Me.lblX.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblX.Name = "lblX"
        Me.lblX.Size = New System.Drawing.Size(93, 16)
        Me.lblX.TabIndex = 0
        Me.lblX.Text = "Cajas X Cama:"
        '
        'chkActivarPR
        '
        Me.chkActivarPR.EditValue = True
        Me.chkActivarPR.Location = New System.Drawing.Point(135, 300)
        Me.chkActivarPR.Margin = New System.Windows.Forms.Padding(4)
        Me.chkActivarPR.MenuManager = Me.RibbonControl
        Me.chkActivarPR.Name = "chkActivarPR"
        Me.chkActivarPR.Properties.Caption = ""
        Me.chkActivarPR.Size = New System.Drawing.Size(43, 24)
        Me.chkActivarPR.TabIndex = 27
        '
        'ToolStripPR
        '
        Me.ToolStripPR.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStripPR.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdNewPR, Me.cmdSavePR, Me.cmdDesactivarPresentacion})
        Me.ToolStripPR.Location = New System.Drawing.Point(2, 28)
        Me.ToolStripPR.Name = "ToolStripPR"
        Me.ToolStripPR.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStripPR.Size = New System.Drawing.Size(1548, 27)
        Me.ToolStripPR.TabIndex = 0
        Me.ToolStripPR.Text = "ToolStrip2"
        '
        'cmdNewPR
        '
        Me.cmdNewPR.Image = CType(resources.GetObject("cmdNewPR.Image"), System.Drawing.Image)
        Me.cmdNewPR.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdNewPR.Name = "cmdNewPR"
        Me.cmdNewPR.Size = New System.Drawing.Size(76, 24)
        Me.cmdNewPR.Text = "Nuevo"
        '
        'cmdSavePR
        '
        Me.cmdSavePR.Image = CType(resources.GetObject("cmdSavePR.Image"), System.Drawing.Image)
        Me.cmdSavePR.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSavePR.Name = "cmdSavePR"
        Me.cmdSavePR.Size = New System.Drawing.Size(86, 24)
        Me.cmdSavePR.Text = "Guardar"
        '
        'cmdDesactivarPresentacion
        '
        Me.cmdDesactivarPresentacion.Image = CType(resources.GetObject("cmdDesactivarPresentacion.Image"), System.Drawing.Image)
        Me.cmdDesactivarPresentacion.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDesactivarPresentacion.Name = "cmdDesactivarPresentacion"
        Me.cmdDesactivarPresentacion.Size = New System.Drawing.Size(102, 24)
        Me.cmdDesactivarPresentacion.Text = "Desactivar"
        '
        'txtCodigoBarraPresentacion
        '
        Me.txtCodigoBarraPresentacion.Location = New System.Drawing.Point(135, 101)
        Me.txtCodigoBarraPresentacion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodigoBarraPresentacion.MenuManager = Me.RibbonControl
        Me.txtCodigoBarraPresentacion.Name = "txtCodigoBarraPresentacion"
        Me.txtCodigoBarraPresentacion.Size = New System.Drawing.Size(234, 22)
        Me.txtCodigoBarraPresentacion.TabIndex = 4
        '
        'txtInfo
        '
        Me.txtInfo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInfo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInfo.Location = New System.Drawing.Point(465, 236)
        Me.txtInfo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtInfo.Multiline = True
        Me.txtInfo.Name = "txtInfo"
        Me.txtInfo.ReadOnly = True
        Me.txtInfo.Size = New System.Drawing.Size(350, 104)
        Me.txtInfo.TabIndex = 9
        Me.txtInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtFactor
        '
        Me.txtFactor.DecimalPlaces = 6
        Me.txtFactor.Location = New System.Drawing.Point(135, 134)
        Me.txtFactor.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFactor.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtFactor.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtFactor.Name = "txtFactor"
        Me.txtFactor.Size = New System.Drawing.Size(234, 23)
        Me.txtFactor.TabIndex = 6
        Me.txtFactor.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'chkImprimeBarra
        '
        Me.chkImprimeBarra.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkImprimeBarra.EditValue = True
        Me.chkImprimeBarra.Location = New System.Drawing.Point(684, 367)
        Me.chkImprimeBarra.Margin = New System.Windows.Forms.Padding(4)
        Me.chkImprimeBarra.MenuManager = Me.RibbonControl
        Me.chkImprimeBarra.Name = "chkImprimeBarra"
        Me.chkImprimeBarra.Properties.Caption = "Imprime Barra"
        Me.chkImprimeBarra.Size = New System.Drawing.Size(117, 24)
        Me.chkImprimeBarra.TabIndex = 24
        '
        'txtAncho
        '
        Me.txtAncho.DecimalPlaces = 6
        Me.txtAncho.Location = New System.Drawing.Point(135, 267)
        Me.txtAncho.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAncho.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtAncho.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtAncho.Name = "txtAncho"
        Me.txtAncho.Size = New System.Drawing.Size(234, 23)
        Me.txtAncho.TabIndex = 25
        '
        'txtLargo
        '
        Me.txtLargo.DecimalPlaces = 6
        Me.txtLargo.Location = New System.Drawing.Point(135, 234)
        Me.txtLargo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLargo.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtLargo.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtLargo.Name = "txtLargo"
        Me.txtLargo.Size = New System.Drawing.Size(234, 23)
        Me.txtLargo.TabIndex = 17
        '
        'txtNombrePresentacion
        '
        Me.txtNombrePresentacion.Location = New System.Drawing.Point(135, 69)
        Me.txtNombrePresentacion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNombrePresentacion.MenuManager = Me.RibbonControl
        Me.txtNombrePresentacion.Name = "txtNombrePresentacion"
        Me.txtNombrePresentacion.Size = New System.Drawing.Size(234, 22)
        Me.txtNombrePresentacion.TabIndex = 2
        '
        'txtAlto
        '
        Me.txtAlto.DecimalPlaces = 6
        Me.txtAlto.Location = New System.Drawing.Point(135, 201)
        Me.txtAlto.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAlto.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtAlto.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtAlto.Name = "txtAlto"
        Me.txtAlto.Size = New System.Drawing.Size(234, 23)
        Me.txtAlto.TabIndex = 11
        '
        'txtPeso
        '
        Me.txtPeso.DecimalPlaces = 6
        Me.txtPeso.Location = New System.Drawing.Point(135, 167)
        Me.txtPeso.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPeso.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtPeso.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtPeso.Name = "txtPeso"
        Me.txtPeso.Size = New System.Drawing.Size(234, 23)
        Me.txtPeso.TabIndex = 8
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.dGridPresentacion)
        Me.GroupControl5.Controls.Add(Me.chkActivoPR)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl5.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl5.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(1552, 226)
        Me.GroupControl5.TabIndex = 29
        Me.GroupControl5.Text = "Detalle Presentación"
        '
        'dGridPresentacion
        '
        Me.dGridPresentacion.Cursor = System.Windows.Forms.Cursors.Default
        Me.dGridPresentacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dGridPresentacion.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode5.RelationName = "Level1"
        Me.dGridPresentacion.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode5})
        Me.dGridPresentacion.Location = New System.Drawing.Point(2, 52)
        Me.dGridPresentacion.MainView = Me.GrdPresentacion
        Me.dGridPresentacion.Margin = New System.Windows.Forms.Padding(4)
        Me.dGridPresentacion.MenuManager = Me.RibbonControl
        Me.dGridPresentacion.Name = "dGridPresentacion"
        Me.dGridPresentacion.Size = New System.Drawing.Size(1548, 172)
        Me.dGridPresentacion.TabIndex = 0
        Me.dGridPresentacion.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GrdPresentacion, Me.GridView6})
        '
        'GrdPresentacion
        '
        Me.GrdPresentacion.DetailHeight = 431
        Me.GrdPresentacion.GridControl = Me.dGridPresentacion
        Me.GrdPresentacion.Name = "GrdPresentacion"
        Me.GrdPresentacion.OptionsBehavior.Editable = False
        Me.GrdPresentacion.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GrdPresentacion.OptionsView.ShowGroupPanel = False
        '
        'GridView6
        '
        Me.GridView6.DetailHeight = 431
        Me.GridView6.GridControl = Me.dGridPresentacion
        Me.GridView6.Name = "GridView6"
        '
        'chkActivoPR
        '
        Me.chkActivoPR.Dock = System.Windows.Forms.DockStyle.Top
        Me.chkActivoPR.EditValue = True
        Me.chkActivoPR.Location = New System.Drawing.Point(2, 28)
        Me.chkActivoPR.Margin = New System.Windows.Forms.Padding(4)
        Me.chkActivoPR.MenuManager = Me.RibbonControl
        Me.chkActivoPR.Name = "chkActivoPR"
        Me.chkActivoPR.Properties.Caption = "Presentaciones Activas"
        Me.chkActivoPR.Size = New System.Drawing.Size(1548, 24)
        Me.chkActivoPR.TabIndex = 2
        '
        'tabPresentacionTarima
        '
        Me.tabPresentacionTarima.Controls.Add(Me.GroupControl8)
        Me.tabPresentacionTarima.Margin = New System.Windows.Forms.Padding(4)
        Me.tabPresentacionTarima.Name = "tabPresentacionTarima"
        Me.tabPresentacionTarima.Size = New System.Drawing.Size(1552, 603)
        Me.tabPresentacionTarima.Text = "Presentación Tarima"
        '
        'GroupControl8
        '
        Me.GroupControl8.Controls.Add(Me.cmbTipoTarima)
        Me.GroupControl8.Controls.Add(Me.cmbPresentacionTarima)
        Me.GroupControl8.Controls.Add(lblCajasPorCama)
        Me.GroupControl8.Controls.Add(Me.txtCantidadPorCama)
        Me.GroupControl8.Controls.Add(Me.ToolStripPT)
        Me.GroupControl8.Controls.Add(Label)
        Me.GroupControl8.Controls.Add(Me.chkActivoPT2)
        Me.GroupControl8.Controls.Add(Label50)
        Me.GroupControl8.Controls.Add(Me.txtCantidad)
        Me.GroupControl8.Controls.Add(Label48)
        Me.GroupControl8.Controls.Add(Me.lblNombreProductoPT)
        Me.GroupControl8.Controls.Add(Label52)
        Me.GroupControl8.Controls.Add(Label53)
        Me.GroupControl8.Controls.Add(Me.GroupControl9)
        Me.GroupControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl8.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl8.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl8.Name = "GroupControl8"
        Me.GroupControl8.Size = New System.Drawing.Size(1552, 603)
        Me.GroupControl8.TabIndex = 0
        '
        'cmbTipoTarima
        '
        Me.cmbTipoTarima.Location = New System.Drawing.Point(196, 133)
        Me.cmbTipoTarima.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbTipoTarima.MenuManager = Me.RibbonControl
        Me.cmbTipoTarima.Name = "cmbTipoTarima"
        Me.cmbTipoTarima.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoTarima.Properties.NullText = ""
        Me.cmbTipoTarima.Size = New System.Drawing.Size(223, 22)
        Me.cmbTipoTarima.TabIndex = 15
        '
        'cmbPresentacionTarima
        '
        Me.cmbPresentacionTarima.Location = New System.Drawing.Point(196, 103)
        Me.cmbPresentacionTarima.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPresentacionTarima.MenuManager = Me.RibbonControl
        Me.cmbPresentacionTarima.Name = "cmbPresentacionTarima"
        Me.cmbPresentacionTarima.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPresentacionTarima.Properties.NullText = ""
        Me.cmbPresentacionTarima.Size = New System.Drawing.Size(223, 22)
        Me.cmbPresentacionTarima.TabIndex = 14
        '
        'txtCantidadPorCama
        '
        Me.txtCantidadPorCama.Location = New System.Drawing.Point(196, 191)
        Me.txtCantidadPorCama.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCantidadPorCama.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCantidadPorCama.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtCantidadPorCama.Name = "txtCantidadPorCama"
        Me.txtCantidadPorCama.Size = New System.Drawing.Size(223, 23)
        Me.txtCantidadPorCama.TabIndex = 10
        '
        'ToolStripPT
        '
        Me.ToolStripPT.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStripPT.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdNewPT, Me.cmdSavePT, Me.cmdDesactivarPresentacionTarima})
        Me.ToolStripPT.Location = New System.Drawing.Point(2, 28)
        Me.ToolStripPT.Name = "ToolStripPT"
        Me.ToolStripPT.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStripPT.Size = New System.Drawing.Size(1548, 27)
        Me.ToolStripPT.TabIndex = 0
        Me.ToolStripPT.Text = "ToolStrip2"
        '
        'cmdNewPT
        '
        Me.cmdNewPT.Image = CType(resources.GetObject("cmdNewPT.Image"), System.Drawing.Image)
        Me.cmdNewPT.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdNewPT.Name = "cmdNewPT"
        Me.cmdNewPT.Size = New System.Drawing.Size(76, 24)
        Me.cmdNewPT.Text = "Nuevo"
        '
        'cmdSavePT
        '
        Me.cmdSavePT.Image = CType(resources.GetObject("cmdSavePT.Image"), System.Drawing.Image)
        Me.cmdSavePT.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSavePT.Name = "cmdSavePT"
        Me.cmdSavePT.Size = New System.Drawing.Size(86, 24)
        Me.cmdSavePT.Text = "Guardar"
        '
        'cmdDesactivarPresentacionTarima
        '
        Me.cmdDesactivarPresentacionTarima.Image = CType(resources.GetObject("cmdDesactivarPresentacionTarima.Image"), System.Drawing.Image)
        Me.cmdDesactivarPresentacionTarima.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDesactivarPresentacionTarima.Name = "cmdDesactivarPresentacionTarima"
        Me.cmdDesactivarPresentacionTarima.Size = New System.Drawing.Size(102, 24)
        Me.cmdDesactivarPresentacionTarima.Text = "Desactivar"
        '
        'chkActivoPT2
        '
        Me.chkActivoPT2.EditValue = True
        Me.chkActivoPT2.Location = New System.Drawing.Point(196, 224)
        Me.chkActivoPT2.Margin = New System.Windows.Forms.Padding(4)
        Me.chkActivoPT2.MenuManager = Me.RibbonControl
        Me.chkActivoPT2.Name = "chkActivoPT2"
        Me.chkActivoPT2.Properties.Caption = ""
        Me.chkActivoPT2.Size = New System.Drawing.Size(117, 24)
        Me.chkActivoPT2.TabIndex = 12
        '
        'txtCantidad
        '
        Me.txtCantidad.Location = New System.Drawing.Point(196, 162)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCantidad.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCantidad.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(223, 23)
        Me.txtCantidad.TabIndex = 8
        '
        'lblNombreProductoPT
        '
        Me.lblNombreProductoPT.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNombreProductoPT.AutoSize = True
        Me.lblNombreProductoPT.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombreProductoPT.ForeColor = System.Drawing.Color.Navy
        Me.lblNombreProductoPT.Location = New System.Drawing.Point(192, 73)
        Me.lblNombreProductoPT.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNombreProductoPT.Name = "lblNombreProductoPT"
        Me.lblNombreProductoPT.Size = New System.Drawing.Size(0, 17)
        Me.lblNombreProductoPT.TabIndex = 2
        '
        'GroupControl9
        '
        Me.GroupControl9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl9.Controls.Add(Label54)
        Me.GroupControl9.Controls.Add(Me.GridPresentacionTarima)
        Me.GroupControl9.Controls.Add(Me.chkActivoPT)
        Me.GroupControl9.Location = New System.Drawing.Point(2, 255)
        Me.GroupControl9.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl9.Name = "GroupControl9"
        Me.GroupControl9.Size = New System.Drawing.Size(1548, 274)
        Me.GroupControl9.TabIndex = 13
        Me.GroupControl9.Text = "Detalle Presentación Tarima"
        '
        'GridPresentacionTarima
        '
        Me.GridPresentacionTarima.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridPresentacionTarima.Cursor = System.Windows.Forms.Cursors.Default
        Me.GridPresentacionTarima.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode6.RelationName = "Level1"
        Me.GridPresentacionTarima.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode6})
        Me.GridPresentacionTarima.Location = New System.Drawing.Point(2, 60)
        Me.GridPresentacionTarima.MainView = Me.ViewPT
        Me.GridPresentacionTarima.Margin = New System.Windows.Forms.Padding(4)
        Me.GridPresentacionTarima.MenuManager = Me.RibbonControl
        Me.GridPresentacionTarima.Name = "GridPresentacionTarima"
        Me.GridPresentacionTarima.Size = New System.Drawing.Size(1544, 233)
        Me.GridPresentacionTarima.TabIndex = 2
        Me.GridPresentacionTarima.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewPT, Me.GridView5})
        '
        'ViewPT
        '
        Me.ViewPT.DetailHeight = 431
        Me.ViewPT.GridControl = Me.GridPresentacionTarima
        Me.ViewPT.Name = "ViewPT"
        Me.ViewPT.OptionsBehavior.Editable = False
        Me.ViewPT.OptionsFind.AlwaysVisible = True
        Me.ViewPT.OptionsView.ShowGroupPanel = False
        '
        'GridView5
        '
        Me.GridView5.DetailHeight = 431
        Me.GridView5.GridControl = Me.GridPresentacionTarima
        Me.GridView5.Name = "GridView5"
        '
        'chkActivoPT
        '
        Me.chkActivoPT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkActivoPT.EditValue = True
        Me.chkActivoPT.Location = New System.Drawing.Point(1510, 30)
        Me.chkActivoPT.Margin = New System.Windows.Forms.Padding(4)
        Me.chkActivoPT.MenuManager = Me.RibbonControl
        Me.chkActivoPT.Name = "chkActivoPT"
        Me.chkActivoPT.Properties.Caption = ""
        Me.chkActivoPT.Size = New System.Drawing.Size(21, 24)
        Me.chkActivoPT.TabIndex = 1
        '
        'TabProductoSustituto
        '
        Me.TabProductoSustituto.Controls.Add(Me.GroupControl4)
        Me.TabProductoSustituto.Margin = New System.Windows.Forms.Padding(4)
        Me.TabProductoSustituto.Name = "TabProductoSustituto"
        Me.TabProductoSustituto.Size = New System.Drawing.Size(1552, 603)
        Me.TabProductoSustituto.Text = "Sustitución"
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.cmbPresentacionR)
        Me.GroupControl4.Controls.Add(Me.cmbProductoP)
        Me.GroupControl4.Controls.Add(Label9)
        Me.GroupControl4.Controls.Add(Me.chkActivarPS)
        Me.GroupControl4.Controls.Add(Me.ToolStripPS)
        Me.GroupControl4.Controls.Add(Me.lblNombreProductoO)
        Me.GroupControl4.Controls.Add(Label27)
        Me.GroupControl4.Controls.Add(Label25)
        Me.GroupControl4.Controls.Add(Me.lnkProductoR)
        Me.GroupControl4.Controls.Add(Me.txtIdProductoR)
        Me.GroupControl4.Controls.Add(Me.txtNombrePR)
        Me.GroupControl4.Controls.Add(Label24)
        Me.GroupControl4.Controls.Add(Me.GrpProductoReeemplazo)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl4.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(1552, 603)
        Me.GroupControl4.TabIndex = 0
        '
        'cmbPresentacionR
        '
        Me.cmbPresentacionR.Location = New System.Drawing.Point(196, 169)
        Me.cmbPresentacionR.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPresentacionR.MenuManager = Me.RibbonControl
        Me.cmbPresentacionR.Name = "cmbPresentacionR"
        Me.cmbPresentacionR.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPresentacionR.Properties.NullText = ""
        Me.cmbPresentacionR.Size = New System.Drawing.Size(234, 22)
        Me.cmbPresentacionR.TabIndex = 14
        '
        'cmbProductoP
        '
        Me.cmbProductoP.Location = New System.Drawing.Point(196, 103)
        Me.cmbProductoP.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbProductoP.MenuManager = Me.RibbonControl
        Me.cmbProductoP.Name = "cmbProductoP"
        Me.cmbProductoP.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbProductoP.Properties.NullText = ""
        Me.cmbProductoP.Size = New System.Drawing.Size(234, 22)
        Me.cmbProductoP.TabIndex = 13
        '
        'chkActivarPS
        '
        Me.chkActivarPS.EditValue = True
        Me.chkActivarPS.Location = New System.Drawing.Point(196, 201)
        Me.chkActivarPS.Margin = New System.Windows.Forms.Padding(4)
        Me.chkActivarPS.MenuManager = Me.RibbonControl
        Me.chkActivarPS.Name = "chkActivarPS"
        Me.chkActivarPS.Properties.Caption = ""
        Me.chkActivarPS.Size = New System.Drawing.Size(43, 24)
        Me.chkActivarPS.TabIndex = 11
        '
        'ToolStripPS
        '
        Me.ToolStripPS.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStripPS.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdNewPS, Me.cmdSavePS, Me.cmdDesactivarProductoSustituto})
        Me.ToolStripPS.Location = New System.Drawing.Point(2, 28)
        Me.ToolStripPS.Name = "ToolStripPS"
        Me.ToolStripPS.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStripPS.Size = New System.Drawing.Size(1548, 27)
        Me.ToolStripPS.TabIndex = 0
        Me.ToolStripPS.Text = "ToolStrip2"
        '
        'cmdNewPS
        '
        Me.cmdNewPS.Image = CType(resources.GetObject("cmdNewPS.Image"), System.Drawing.Image)
        Me.cmdNewPS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdNewPS.Name = "cmdNewPS"
        Me.cmdNewPS.Size = New System.Drawing.Size(76, 24)
        Me.cmdNewPS.Text = "Nuevo"
        '
        'cmdSavePS
        '
        Me.cmdSavePS.Image = CType(resources.GetObject("cmdSavePS.Image"), System.Drawing.Image)
        Me.cmdSavePS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSavePS.Name = "cmdSavePS"
        Me.cmdSavePS.Size = New System.Drawing.Size(86, 24)
        Me.cmdSavePS.Text = "Guardar"
        '
        'cmdDesactivarProductoSustituto
        '
        Me.cmdDesactivarProductoSustituto.Image = CType(resources.GetObject("cmdDesactivarProductoSustituto.Image"), System.Drawing.Image)
        Me.cmdDesactivarProductoSustituto.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDesactivarProductoSustituto.Name = "cmdDesactivarProductoSustituto"
        Me.cmdDesactivarProductoSustituto.Size = New System.Drawing.Size(102, 24)
        Me.cmdDesactivarProductoSustituto.Text = "Desactivar"
        '
        'lblNombreProductoO
        '
        Me.lblNombreProductoO.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNombreProductoO.AutoSize = True
        Me.lblNombreProductoO.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombreProductoO.ForeColor = System.Drawing.Color.Navy
        Me.lblNombreProductoO.Location = New System.Drawing.Point(192, 73)
        Me.lblNombreProductoO.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNombreProductoO.Name = "lblNombreProductoO"
        Me.lblNombreProductoO.Size = New System.Drawing.Size(0, 17)
        Me.lblNombreProductoO.TabIndex = 2
        '
        'lnkProductoR
        '
        Me.lnkProductoR.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkProductoR.AutoSize = True
        Me.lnkProductoR.Location = New System.Drawing.Point(40, 139)
        Me.lnkProductoR.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkProductoR.Name = "lnkProductoR"
        Me.lnkProductoR.Size = New System.Drawing.Size(124, 16)
        Me.lnkProductoR.TabIndex = 5
        Me.lnkProductoR.TabStop = True
        Me.lnkProductoR.Text = "Producto Reemplazo"
        '
        'txtIdProductoR
        '
        Me.txtIdProductoR.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtIdProductoR.Location = New System.Drawing.Point(196, 135)
        Me.txtIdProductoR.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdProductoR.MenuManager = Me.RibbonControl
        Me.txtIdProductoR.Name = "txtIdProductoR"
        Me.txtIdProductoR.Properties.Mask.EditMask = "n0"
        Me.txtIdProductoR.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtIdProductoR.Size = New System.Drawing.Size(443, 22)
        Me.txtIdProductoR.TabIndex = 6
        '
        'txtNombrePR
        '
        Me.txtNombrePR.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNombrePR.Location = New System.Drawing.Point(648, 135)
        Me.txtNombrePR.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNombrePR.MenuManager = Me.RibbonControl
        Me.txtNombrePR.Name = "txtNombrePR"
        Me.txtNombrePR.Properties.ReadOnly = True
        Me.txtNombrePR.Size = New System.Drawing.Size(276, 22)
        Me.txtNombrePR.TabIndex = 7
        '
        'GrpProductoReeemplazo
        '
        Me.GrpProductoReeemplazo.Controls.Add(Label31)
        Me.GrpProductoReeemplazo.Controls.Add(Me.GrdProductoS)
        Me.GrpProductoReeemplazo.Controls.Add(Me.chkActivoPS)
        Me.GrpProductoReeemplazo.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GrpProductoReeemplazo.Location = New System.Drawing.Point(2, 599)
        Me.GrpProductoReeemplazo.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpProductoReeemplazo.Name = "GrpProductoReeemplazo"
        Me.GrpProductoReeemplazo.Size = New System.Drawing.Size(1548, 2)
        Me.GrpProductoReeemplazo.TabIndex = 12
        Me.GrpProductoReeemplazo.Text = "Detalle Producto Sustituto"
        '
        'GrdProductoS
        '
        Me.GrdProductoS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GrdProductoS.Cursor = System.Windows.Forms.Cursors.Default
        Me.GrdProductoS.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode7.RelationName = "Level1"
        Me.GrdProductoS.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode7})
        Me.GrdProductoS.Location = New System.Drawing.Point(2, 60)
        Me.GrdProductoS.MainView = Me.GridViewProductoS
        Me.GrdProductoS.Margin = New System.Windows.Forms.Padding(4)
        Me.GrdProductoS.MenuManager = Me.RibbonControl
        Me.GrdProductoS.Name = "GrdProductoS"
        Me.GrdProductoS.Size = New System.Drawing.Size(1542, 284)
        Me.GrdProductoS.TabIndex = 2
        Me.GrdProductoS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewProductoS, Me.GridView4})
        '
        'GridViewProductoS
        '
        Me.GridViewProductoS.DetailHeight = 431
        Me.GridViewProductoS.GridControl = Me.GrdProductoS
        Me.GridViewProductoS.Name = "GridViewProductoS"
        Me.GridViewProductoS.OptionsBehavior.Editable = False
        Me.GridViewProductoS.OptionsFind.AlwaysVisible = True
        Me.GridViewProductoS.OptionsView.ShowGroupPanel = False
        '
        'GridView4
        '
        Me.GridView4.DetailHeight = 431
        Me.GridView4.GridControl = Me.GrdProductoS
        Me.GridView4.Name = "GridView4"
        '
        'chkActivoPS
        '
        Me.chkActivoPS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkActivoPS.EditValue = True
        Me.chkActivoPS.Location = New System.Drawing.Point(1512, 30)
        Me.chkActivoPS.Margin = New System.Windows.Forms.Padding(4)
        Me.chkActivoPS.MenuManager = Me.RibbonControl
        Me.chkActivoPS.Name = "chkActivoPS"
        Me.chkActivoPS.Properties.Caption = ""
        Me.chkActivoPS.Size = New System.Drawing.Size(21, 24)
        Me.chkActivoPS.TabIndex = 1
        '
        'TabProductoRellenado
        '
        Me.TabProductoRellenado.Controls.Add(Me.GroupControl6)
        Me.TabProductoRellenado.Margin = New System.Windows.Forms.Padding(4)
        Me.TabProductoRellenado.Name = "TabProductoRellenado"
        Me.TabProductoRellenado.Size = New System.Drawing.Size(1552, 603)
        Me.TabProductoRellenado.Text = "Reabastecimiento"
        '
        'GroupControl6
        '
        Me.GroupControl6.Controls.Add(Me.splitContainerReabasto)
        Me.GroupControl6.Controls.Add(Me.ToolStripPRL)
        Me.GroupControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl6.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl6.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl6.Name = "GroupControl6"
        Me.GroupControl6.Size = New System.Drawing.Size(1552, 603)
        Me.GroupControl6.TabIndex = 0
        '
        'splitContainerReabasto
        '
        Me.splitContainerReabasto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitContainerReabasto.Location = New System.Drawing.Point(2, 55)
        Me.splitContainerReabasto.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.splitContainerReabasto.Name = "splitContainerReabasto"
        Me.splitContainerReabasto.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitContainerReabasto.Panel1
        '
        Me.splitContainerReabasto.Panel1.Controls.Add(Me.grpReabastecimiento)
        '
        'splitContainerReabasto.Panel2
        '
        Me.splitContainerReabasto.Panel2.Controls.Add(Me.GridProductoRellenado)
        Me.splitContainerReabasto.Panel2.Controls.Add(Me.chkProductoPRL)
        Me.splitContainerReabasto.Size = New System.Drawing.Size(1548, 546)
        Me.splitContainerReabasto.SplitterDistance = 263
        Me.splitContainerReabasto.TabIndex = 1
        '
        'grpReabastecimiento
        '
        Me.grpReabastecimiento.Controls.Add(Me.SplitContainer2)
        Me.grpReabastecimiento.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpReabastecimiento.Location = New System.Drawing.Point(0, 0)
        Me.grpReabastecimiento.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.grpReabastecimiento.Name = "grpReabastecimiento"
        Me.grpReabastecimiento.Size = New System.Drawing.Size(1548, 263)
        Me.grpReabastecimiento.TabIndex = 0
        Me.grpReabastecimiento.Text = "Regla de reabastecimiento"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(2, 28)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmbOperadorDefecto)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LabelControl6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblIdRellenado)
        Me.SplitContainer2.Panel1.Controls.Add(Label38)
        Me.SplitContainer2.Panel1.Controls.Add(Label40)
        Me.SplitContainer2.Panel1.Controls.Add(Me.optNotificar)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmbPresentacionPR)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lnklblUMBasRellenado)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmbProductoEstado)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtNombreUbicacion)
        Me.SplitContainer2.Panel1.Controls.Add(Label42)
        Me.SplitContainer2.Panel1.Controls.Add(Me.optGenerarAutomaticamente)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtIdUnidadMedidaBasicaRellenado)
        Me.SplitContainer2.Panel1.Controls.Add(Label22)
        Me.SplitContainer2.Panel1.Controls.Add(lblBodegaRellenado)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtIdUbicacion)
        Me.SplitContainer2.Panel1.Controls.Add(Label39)
        Me.SplitContainer2.Panel1.Controls.Add(Label45)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblNombreProductoPR)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtNombreUMBasRellenado)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkActivarProductoPRL)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtMaximoPicking)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmbBodegaRellenado)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lnkUbicacion)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtMinimoPicking)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.lblReabastecerCon)
        Me.SplitContainer2.Panel2.Controls.Add(Me.LinkLabel1)
        Me.SplitContainer2.Panel2.Controls.Add(lblPresentacionReabastecerCon)
        Me.SplitContainer2.Panel2.Controls.Add(Me.cmbPresentacionAbastecerCon)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtNombreUMBasReabastecerCon)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtIdUMBasReabastecerCon)
        Me.SplitContainer2.Size = New System.Drawing.Size(1544, 233)
        Me.SplitContainer2.SplitterDistance = 824
        Me.SplitContainer2.TabIndex = 7
        '
        'cmbOperadorDefecto
        '
        Me.cmbOperadorDefecto.Location = New System.Drawing.Point(178, 174)
        Me.cmbOperadorDefecto.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbOperadorDefecto.Name = "cmbOperadorDefecto"
        Me.cmbOperadorDefecto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbOperadorDefecto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOperadorDefecto.Properties.NullText = ""
        Me.cmbOperadorDefecto.Size = New System.Drawing.Size(200, 22)
        Me.cmbOperadorDefecto.TabIndex = 39
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Location = New System.Drawing.Point(26, 178)
        Me.LabelControl6.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(106, 16)
        Me.LabelControl6.TabIndex = 38
        Me.LabelControl6.Text = "Operador Defecto:"
        '
        'lblIdRellenado
        '
        Me.lblIdRellenado.Appearance.Font = New System.Drawing.Font("Tahoma", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIdRellenado.Appearance.ForeColor = System.Drawing.Color.Firebrick
        Me.lblIdRellenado.Appearance.Options.UseFont = True
        Me.lblIdRellenado.Appearance.Options.UseForeColor = True
        Me.lblIdRellenado.Appearance.Options.UseTextOptions = True
        Me.lblIdRellenado.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblIdRellenado.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblIdRellenado.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblIdRellenado.Location = New System.Drawing.Point(384, 71)
        Me.lblIdRellenado.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.lblIdRellenado.Name = "lblIdRellenado"
        Me.lblIdRellenado.Size = New System.Drawing.Size(74, 68)
        Me.lblIdRellenado.TabIndex = 37
        Me.lblIdRellenado.Text = "1"
        Me.lblIdRellenado.Visible = False
        '
        'optNotificar
        '
        Me.optNotificar.AutoSize = True
        Me.optNotificar.Checked = True
        Me.optNotificar.Location = New System.Drawing.Point(392, 202)
        Me.optNotificar.Margin = New System.Windows.Forms.Padding(4)
        Me.optNotificar.Name = "optNotificar"
        Me.optNotificar.Size = New System.Drawing.Size(75, 20)
        Me.optNotificar.TabIndex = 18
        Me.optNotificar.TabStop = True
        Me.optNotificar.Text = "Notificar"
        Me.optNotificar.UseVisualStyleBackColor = True
        '
        'cmbPresentacionPR
        '
        Me.cmbPresentacionPR.Location = New System.Drawing.Point(178, 71)
        Me.cmbPresentacionPR.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPresentacionPR.MenuManager = Me.RibbonControl
        Me.cmbPresentacionPR.Name = "cmbPresentacionPR"
        Me.cmbPresentacionPR.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPresentacionPR.Properties.NullText = ""
        Me.cmbPresentacionPR.Size = New System.Drawing.Size(200, 22)
        Me.cmbPresentacionPR.TabIndex = 6
        '
        'lnklblUMBasRellenado
        '
        Me.lnklblUMBasRellenado.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnklblUMBasRellenado.AutoSize = True
        Me.lnklblUMBasRellenado.Location = New System.Drawing.Point(23, 46)
        Me.lnklblUMBasRellenado.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnklblUMBasRellenado.Name = "lnklblUMBasRellenado"
        Me.lnklblUMBasRellenado.Size = New System.Drawing.Size(73, 16)
        Me.lnklblUMBasRellenado.TabIndex = 2
        Me.lnklblUMBasRellenado.TabStop = True
        Me.lnklblUMBasRellenado.Text = "U.M. Básica"
        '
        'cmbProductoEstado
        '
        Me.cmbProductoEstado.Location = New System.Drawing.Point(178, 97)
        Me.cmbProductoEstado.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbProductoEstado.MenuManager = Me.RibbonControl
        Me.cmbProductoEstado.Name = "cmbProductoEstado"
        Me.cmbProductoEstado.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbProductoEstado.Properties.NullText = ""
        Me.cmbProductoEstado.Size = New System.Drawing.Size(200, 22)
        Me.cmbProductoEstado.TabIndex = 8
        '
        'txtNombreUbicacion
        '
        Me.txtNombreUbicacion.Location = New System.Drawing.Point(382, 149)
        Me.txtNombreUbicacion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNombreUbicacion.MenuManager = Me.RibbonControl
        Me.txtNombreUbicacion.Name = "txtNombreUbicacion"
        Me.txtNombreUbicacion.Properties.ReadOnly = True
        Me.txtNombreUbicacion.Size = New System.Drawing.Size(208, 22)
        Me.txtNombreUbicacion.TabIndex = 13
        '
        'optGenerarAutomaticamente
        '
        Me.optGenerarAutomaticamente.AutoSize = True
        Me.optGenerarAutomaticamente.Location = New System.Drawing.Point(392, 230)
        Me.optGenerarAutomaticamente.Margin = New System.Windows.Forms.Padding(4)
        Me.optGenerarAutomaticamente.Name = "optGenerarAutomaticamente"
        Me.optGenerarAutomaticamente.Size = New System.Drawing.Size(178, 20)
        Me.optGenerarAutomaticamente.TabIndex = 19
        Me.optGenerarAutomaticamente.TabStop = True
        Me.optGenerarAutomaticamente.Text = "Generar Automaticamente"
        Me.optGenerarAutomaticamente.UseVisualStyleBackColor = True
        '
        'txtIdUnidadMedidaBasicaRellenado
        '
        Me.txtIdUnidadMedidaBasicaRellenado.Location = New System.Drawing.Point(178, 41)
        Me.txtIdUnidadMedidaBasicaRellenado.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdUnidadMedidaBasicaRellenado.MenuManager = Me.RibbonControl
        Me.txtIdUnidadMedidaBasicaRellenado.Name = "txtIdUnidadMedidaBasicaRellenado"
        Me.txtIdUnidadMedidaBasicaRellenado.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtIdUnidadMedidaBasicaRellenado.Properties.MaskSettings.Set("mask", "n0")
        Me.txtIdUnidadMedidaBasicaRellenado.Properties.ReadOnly = True
        Me.txtIdUnidadMedidaBasicaRellenado.Size = New System.Drawing.Size(102, 22)
        Me.txtIdUnidadMedidaBasicaRellenado.TabIndex = 3
        '
        'txtIdUbicacion
        '
        Me.txtIdUbicacion.Location = New System.Drawing.Point(178, 149)
        Me.txtIdUbicacion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdUbicacion.MenuManager = Me.RibbonControl
        Me.txtIdUbicacion.Name = "txtIdUbicacion"
        Me.txtIdUbicacion.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtIdUbicacion.Properties.MaskSettings.Set("mask", "n0")
        Me.txtIdUbicacion.Size = New System.Drawing.Size(200, 22)
        Me.txtIdUbicacion.TabIndex = 12
        '
        'lblNombreProductoPR
        '
        Me.lblNombreProductoPR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNombreProductoPR.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombreProductoPR.ForeColor = System.Drawing.Color.Navy
        Me.lblNombreProductoPR.Location = New System.Drawing.Point(178, 14)
        Me.lblNombreProductoPR.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNombreProductoPR.Name = "lblNombreProductoPR"
        Me.lblNombreProductoPR.Size = New System.Drawing.Size(412, 22)
        Me.lblNombreProductoPR.TabIndex = 1
        '
        'txtNombreUMBasRellenado
        '
        Me.txtNombreUMBasRellenado.Location = New System.Drawing.Point(286, 41)
        Me.txtNombreUMBasRellenado.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNombreUMBasRellenado.MenuManager = Me.RibbonControl
        Me.txtNombreUMBasRellenado.Name = "txtNombreUMBasRellenado"
        Me.txtNombreUMBasRellenado.Properties.ReadOnly = True
        Me.txtNombreUMBasRellenado.Size = New System.Drawing.Size(303, 22)
        Me.txtNombreUMBasRellenado.TabIndex = 4
        '
        'chkActivarProductoPRL
        '
        Me.chkActivarProductoPRL.EditValue = True
        Me.chkActivarProductoPRL.Location = New System.Drawing.Point(510, 73)
        Me.chkActivarProductoPRL.Margin = New System.Windows.Forms.Padding(4)
        Me.chkActivarProductoPRL.MenuManager = Me.RibbonControl
        Me.chkActivarProductoPRL.Name = "chkActivarProductoPRL"
        Me.chkActivarProductoPRL.Properties.Caption = ""
        Me.chkActivarProductoPRL.Size = New System.Drawing.Size(40, 24)
        Me.chkActivarProductoPRL.TabIndex = 21
        '
        'txtMaximoPicking
        '
        Me.txtMaximoPicking.Location = New System.Drawing.Point(178, 231)
        Me.txtMaximoPicking.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMaximoPicking.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtMaximoPicking.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtMaximoPicking.Name = "txtMaximoPicking"
        Me.txtMaximoPicking.Size = New System.Drawing.Size(200, 23)
        Me.txtMaximoPicking.TabIndex = 17
        '
        'cmbBodegaRellenado
        '
        Me.cmbBodegaRellenado.Location = New System.Drawing.Point(178, 124)
        Me.cmbBodegaRellenado.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbBodegaRellenado.MenuManager = Me.RibbonControl
        Me.cmbBodegaRellenado.Name = "cmbBodegaRellenado"
        Me.cmbBodegaRellenado.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodegaRellenado.Properties.NullText = ""
        Me.cmbBodegaRellenado.Size = New System.Drawing.Size(200, 22)
        Me.cmbBodegaRellenado.TabIndex = 10
        '
        'lnkUbicacion
        '
        Me.lnkUbicacion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkUbicacion.AutoSize = True
        Me.lnkUbicacion.Location = New System.Drawing.Point(23, 153)
        Me.lnkUbicacion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkUbicacion.Name = "lnkUbicacion"
        Me.lnkUbicacion.Size = New System.Drawing.Size(61, 16)
        Me.lnkUbicacion.TabIndex = 11
        Me.lnkUbicacion.TabStop = True
        Me.lnkUbicacion.Text = "Ubicación"
        '
        'txtMinimoPicking
        '
        Me.txtMinimoPicking.Location = New System.Drawing.Point(178, 203)
        Me.txtMinimoPicking.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMinimoPicking.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtMinimoPicking.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtMinimoPicking.Name = "txtMinimoPicking"
        Me.txtMinimoPicking.Size = New System.Drawing.Size(200, 23)
        Me.txtMinimoPicking.TabIndex = 15
        '
        'lblReabastecerCon
        '
        Me.lblReabastecerCon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReabastecerCon.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblReabastecerCon.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.lblReabastecerCon.ForeColor = System.Drawing.Color.Black
        Me.lblReabastecerCon.Location = New System.Drawing.Point(0, 0)
        Me.lblReabastecerCon.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblReabastecerCon.Name = "lblReabastecerCon"
        Me.lblReabastecerCon.Size = New System.Drawing.Size(714, 30)
        Me.lblReabastecerCon.TabIndex = 7
        Me.lblReabastecerCon.Text = "Reabastecer con:"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(14, 90)
        Me.LinkLabel1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(73, 16)
        Me.LinkLabel1.TabIndex = 0
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "U.M. Básica"
        Me.LinkLabel1.Visible = False
        '
        'cmbPresentacionAbastecerCon
        '
        Me.cmbPresentacionAbastecerCon.Location = New System.Drawing.Point(126, 117)
        Me.cmbPresentacionAbastecerCon.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPresentacionAbastecerCon.MenuManager = Me.RibbonControl
        Me.cmbPresentacionAbastecerCon.Name = "cmbPresentacionAbastecerCon"
        Me.cmbPresentacionAbastecerCon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPresentacionAbastecerCon.Properties.NullText = ""
        Me.cmbPresentacionAbastecerCon.Size = New System.Drawing.Size(248, 22)
        Me.cmbPresentacionAbastecerCon.TabIndex = 4
        '
        'txtNombreUMBasReabastecerCon
        '
        Me.txtNombreUMBasReabastecerCon.Location = New System.Drawing.Point(223, 89)
        Me.txtNombreUMBasReabastecerCon.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNombreUMBasReabastecerCon.MenuManager = Me.RibbonControl
        Me.txtNombreUMBasReabastecerCon.Name = "txtNombreUMBasReabastecerCon"
        Me.txtNombreUMBasReabastecerCon.Properties.ReadOnly = True
        Me.txtNombreUMBasReabastecerCon.Size = New System.Drawing.Size(150, 22)
        Me.txtNombreUMBasReabastecerCon.TabIndex = 2
        Me.txtNombreUMBasReabastecerCon.Visible = False
        '
        'txtIdUMBasReabastecerCon
        '
        Me.txtIdUMBasReabastecerCon.Location = New System.Drawing.Point(126, 89)
        Me.txtIdUMBasReabastecerCon.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdUMBasReabastecerCon.MenuManager = Me.RibbonControl
        Me.txtIdUMBasReabastecerCon.Name = "txtIdUMBasReabastecerCon"
        Me.txtIdUMBasReabastecerCon.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtIdUMBasReabastecerCon.Properties.MaskSettings.Set("mask", "n0")
        Me.txtIdUMBasReabastecerCon.Properties.ReadOnly = True
        Me.txtIdUMBasReabastecerCon.Size = New System.Drawing.Size(91, 22)
        Me.txtIdUMBasReabastecerCon.TabIndex = 1
        Me.txtIdUMBasReabastecerCon.Visible = False
        '
        'GridProductoRellenado
        '
        Me.GridProductoRellenado.Cursor = System.Windows.Forms.Cursors.Default
        Me.GridProductoRellenado.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridProductoRellenado.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode8.RelationName = "Level1"
        Me.GridProductoRellenado.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode8})
        Me.GridProductoRellenado.Location = New System.Drawing.Point(0, 24)
        Me.GridProductoRellenado.MainView = Me.ViewProductoRellenado
        Me.GridProductoRellenado.Margin = New System.Windows.Forms.Padding(4)
        Me.GridProductoRellenado.MenuManager = Me.RibbonControl
        Me.GridProductoRellenado.Name = "GridProductoRellenado"
        Me.GridProductoRellenado.Size = New System.Drawing.Size(1548, 255)
        Me.GridProductoRellenado.TabIndex = 2
        Me.GridProductoRellenado.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewProductoRellenado, Me.GridView3})
        '
        'ViewProductoRellenado
        '
        Me.ViewProductoRellenado.DetailHeight = 431
        Me.ViewProductoRellenado.GridControl = Me.GridProductoRellenado
        Me.ViewProductoRellenado.Name = "ViewProductoRellenado"
        Me.ViewProductoRellenado.OptionsBehavior.Editable = False
        Me.ViewProductoRellenado.OptionsFind.AllowFindPanel = False
        Me.ViewProductoRellenado.OptionsView.ShowAutoFilterRow = True
        Me.ViewProductoRellenado.OptionsView.ShowFooter = True
        Me.ViewProductoRellenado.OptionsView.ShowGroupPanel = False
        '
        'GridView3
        '
        Me.GridView3.DetailHeight = 431
        Me.GridView3.GridControl = Me.GridProductoRellenado
        Me.GridView3.Name = "GridView3"
        '
        'chkProductoPRL
        '
        Me.chkProductoPRL.Dock = System.Windows.Forms.DockStyle.Top
        Me.chkProductoPRL.EditValue = True
        Me.chkProductoPRL.Location = New System.Drawing.Point(0, 0)
        Me.chkProductoPRL.Margin = New System.Windows.Forms.Padding(4)
        Me.chkProductoPRL.MenuManager = Me.RibbonControl
        Me.chkProductoPRL.Name = "chkProductoPRL"
        Me.chkProductoPRL.Properties.Caption = "Activo"
        Me.chkProductoPRL.Size = New System.Drawing.Size(1548, 24)
        Me.chkProductoPRL.TabIndex = 0
        '
        'ToolStripPRL
        '
        Me.ToolStripPRL.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStripPRL.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdNewPRL, Me.cmdSavePRL, Me.cmdDesactivarProductoRellenado})
        Me.ToolStripPRL.Location = New System.Drawing.Point(2, 28)
        Me.ToolStripPRL.Name = "ToolStripPRL"
        Me.ToolStripPRL.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStripPRL.Size = New System.Drawing.Size(1548, 27)
        Me.ToolStripPRL.TabIndex = 0
        Me.ToolStripPRL.Text = "ToolStrip2"
        '
        'cmdNewPRL
        '
        Me.cmdNewPRL.Image = CType(resources.GetObject("cmdNewPRL.Image"), System.Drawing.Image)
        Me.cmdNewPRL.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdNewPRL.Name = "cmdNewPRL"
        Me.cmdNewPRL.Size = New System.Drawing.Size(76, 24)
        Me.cmdNewPRL.Text = "Nuevo"
        '
        'cmdSavePRL
        '
        Me.cmdSavePRL.Image = CType(resources.GetObject("cmdSavePRL.Image"), System.Drawing.Image)
        Me.cmdSavePRL.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSavePRL.Name = "cmdSavePRL"
        Me.cmdSavePRL.Size = New System.Drawing.Size(86, 24)
        Me.cmdSavePRL.Text = "Guardar"
        '
        'cmdDesactivarProductoRellenado
        '
        Me.cmdDesactivarProductoRellenado.Image = CType(resources.GetObject("cmdDesactivarProductoRellenado.Image"), System.Drawing.Image)
        Me.cmdDesactivarProductoRellenado.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDesactivarProductoRellenado.Name = "cmdDesactivarProductoRellenado"
        Me.cmdDesactivarProductoRellenado.Size = New System.Drawing.Size(102, 24)
        Me.cmdDesactivarProductoRellenado.Text = "Desactivar"
        '
        'tabProductoKit
        '
        Me.tabProductoKit.Controls.Add(Me.PanelControl1)
        Me.tabProductoKit.Margin = New System.Windows.Forms.Padding(4)
        Me.tabProductoKit.Name = "tabProductoKit"
        Me.tabProductoKit.PageVisible = False
        Me.tabProductoKit.Size = New System.Drawing.Size(1552, 603)
        Me.tabProductoKit.Text = "Kit"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.GroupControl2)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1552, 603)
        Me.PanelControl1.TabIndex = 0
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.GroupControl12)
        Me.GroupControl2.Controls.Add(Me.GroupControl13)
        Me.GroupControl2.Controls.Add(Me.ToolStrip2)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(2, 2)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1548, 599)
        Me.GroupControl2.TabIndex = 0
        '
        'GroupControl12
        '
        Me.GroupControl12.Controls.Add(Me.grdPrdKit)
        Me.GroupControl12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl12.Location = New System.Drawing.Point(2, 291)
        Me.GroupControl12.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl12.Name = "GroupControl12"
        Me.GroupControl12.Size = New System.Drawing.Size(1544, 306)
        Me.GroupControl12.TabIndex = 28
        Me.GroupControl12.Text = "Composición de Kit"
        '
        'grdPrdKit
        '
        Me.grdPrdKit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdPrdKit.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdPrdKit.Location = New System.Drawing.Point(2, 25)
        Me.grdPrdKit.MainView = Me.GridView11
        Me.grdPrdKit.Margin = New System.Windows.Forms.Padding(4)
        Me.grdPrdKit.MenuManager = Me.RibbonControl
        Me.grdPrdKit.Name = "grdPrdKit"
        Me.grdPrdKit.Size = New System.Drawing.Size(1543, 316)
        Me.grdPrdKit.TabIndex = 0
        Me.grdPrdKit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView11})
        '
        'GridView11
        '
        Me.GridView11.DetailHeight = 431
        Me.GridView11.GridControl = Me.grdPrdKit
        Me.GridView11.Name = "GridView11"
        Me.GridView11.OptionsBehavior.ReadOnly = True
        Me.GridView11.OptionsFind.AlwaysVisible = True
        Me.GridView11.OptionsView.ColumnAutoWidth = False
        Me.GridView11.OptionsView.ShowFooter = True
        '
        'GroupControl13
        '
        Me.GroupControl13.AutoSize = True
        Me.GroupControl13.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupControl13.Controls.Add(Me.txtCodPrdHijo)
        Me.GroupControl13.Controls.Add(Label56)
        Me.GroupControl13.Controls.Add(Me.txtCantPrdHijo)
        Me.GroupControl13.Controls.Add(Me.txtNombrePrdHijo)
        Me.GroupControl13.Controls.Add(Me.Label30)
        Me.GroupControl13.Controls.Add(Me.linklblProductoKit)
        Me.GroupControl13.Controls.Add(Me.txtNomUMBHijo)
        Me.GroupControl13.Controls.Add(lblPrdPd)
        Me.GroupControl13.Controls.Add(Me.txtIdUMBHijo)
        Me.GroupControl13.Controls.Add(Me.lblProdPadre)
        Me.GroupControl13.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl13.Location = New System.Drawing.Point(2, 55)
        Me.GroupControl13.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl13.Name = "GroupControl13"
        Me.GroupControl13.Size = New System.Drawing.Size(1544, 162)
        Me.GroupControl13.TabIndex = 29
        '
        'txtCodPrdHijo
        '
        Me.txtCodPrdHijo.Location = New System.Drawing.Point(167, 71)
        Me.txtCodPrdHijo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodPrdHijo.MenuManager = Me.RibbonControl
        Me.txtCodPrdHijo.Name = "txtCodPrdHijo"
        Me.txtCodPrdHijo.Size = New System.Drawing.Size(136, 22)
        Me.txtCodPrdHijo.TabIndex = 19
        '
        'txtCantPrdHijo
        '
        Me.txtCantPrdHijo.Location = New System.Drawing.Point(167, 135)
        Me.txtCantPrdHijo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCantPrdHijo.Maximum = New Decimal(New Integer() {1569325056, 23283064, 0, 0})
        Me.txtCantPrdHijo.Name = "txtCantPrdHijo"
        Me.txtCantPrdHijo.Size = New System.Drawing.Size(140, 23)
        Me.txtCantPrdHijo.TabIndex = 27
        '
        'txtNombrePrdHijo
        '
        Me.txtNombrePrdHijo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNombrePrdHijo.Location = New System.Drawing.Point(310, 73)
        Me.txtNombrePrdHijo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNombrePrdHijo.MenuManager = Me.RibbonControl
        Me.txtNombrePrdHijo.Name = "txtNombrePrdHijo"
        Me.txtNombrePrdHijo.Properties.ReadOnly = True
        Me.txtNombrePrdHijo.Size = New System.Drawing.Size(557, 22)
        Me.txtNombrePrdHijo.TabIndex = 20
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(10, 135)
        Me.Label30.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(62, 16)
        Me.Label30.TabIndex = 26
        Me.Label30.Text = "Cantidad:"
        '
        'linklblProductoKit
        '
        Me.linklblProductoKit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.linklblProductoKit.AutoSize = True
        Me.linklblProductoKit.Location = New System.Drawing.Point(10, 75)
        Me.linklblProductoKit.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.linklblProductoKit.Name = "linklblProductoKit"
        Me.linklblProductoKit.Size = New System.Drawing.Size(88, 16)
        Me.linklblProductoKit.TabIndex = 18
        Me.linklblProductoKit.TabStop = True
        Me.linklblProductoKit.Text = "Producto Hijo:"
        '
        'txtNomUMBHijo
        '
        Me.txtNomUMBHijo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNomUMBHijo.Location = New System.Drawing.Point(310, 103)
        Me.txtNomUMBHijo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNomUMBHijo.MenuManager = Me.RibbonControl
        Me.txtNomUMBHijo.Name = "txtNomUMBHijo"
        Me.txtNomUMBHijo.Properties.ReadOnly = True
        Me.txtNomUMBHijo.Size = New System.Drawing.Size(557, 22)
        Me.txtNomUMBHijo.TabIndex = 25
        '
        'txtIdUMBHijo
        '
        Me.txtIdUMBHijo.Location = New System.Drawing.Point(167, 103)
        Me.txtIdUMBHijo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdUMBHijo.MenuManager = Me.RibbonControl
        Me.txtIdUMBHijo.Name = "txtIdUMBHijo"
        Me.txtIdUMBHijo.Properties.ReadOnly = True
        Me.txtIdUMBHijo.Size = New System.Drawing.Size(136, 22)
        Me.txtIdUMBHijo.TabIndex = 24
        '
        'lblProdPadre
        '
        Me.lblProdPadre.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblProdPadre.AutoSize = True
        Me.lblProdPadre.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProdPadre.ForeColor = System.Drawing.Color.Navy
        Me.lblProdPadre.Location = New System.Drawing.Point(163, 37)
        Me.lblProdPadre.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProdPadre.Name = "lblProdPadre"
        Me.lblProdPadre.Size = New System.Drawing.Size(26, 17)
        Me.lblProdPadre.TabIndex = 16
        Me.lblProdPadre.Text = "---"
        '
        'ToolStrip2
        '
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdAggPrK, Me.cmdGuardarPrk, Me.cmdEliminarPrk})
        Me.ToolStrip2.Location = New System.Drawing.Point(2, 28)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip2.Size = New System.Drawing.Size(1544, 27)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'cmdAggPrK
        '
        Me.cmdAggPrK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAggPrK.Name = "cmdAggPrK"
        Me.cmdAggPrK.Size = New System.Drawing.Size(56, 24)
        Me.cmdAggPrK.Text = "Nuevo"
        '
        'cmdGuardarPrk
        '
        Me.cmdGuardarPrk.Image = Global.TOMWMS.My.Resources.Resources.greencheck
        Me.cmdGuardarPrk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdGuardarPrk.Name = "cmdGuardarPrk"
        Me.cmdGuardarPrk.Size = New System.Drawing.Size(86, 24)
        Me.cmdGuardarPrk.Text = "Guardar"
        '
        'cmdEliminarPrk
        '
        Me.cmdEliminarPrk.Image = Global.TOMWMS.My.Resources.Resources.desactivar
        Me.cmdEliminarPrk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdEliminarPrk.Name = "cmdEliminarPrk"
        Me.cmdEliminarPrk.Size = New System.Drawing.Size(87, 24)
        Me.cmdEliminarPrk.Text = "Eliminar"
        '
        'tabStock
        '
        Me.tabStock.Controls.Add(Me.grdPStock)
        Me.tabStock.Margin = New System.Windows.Forms.Padding(4)
        Me.tabStock.Name = "tabStock"
        Me.tabStock.Size = New System.Drawing.Size(1552, 603)
        Me.tabStock.Text = "Stock"
        '
        'Conversion
        '
        Me.Conversion.Controls.Add(Me.GroupControl10)
        Me.Conversion.Margin = New System.Windows.Forms.Padding(4)
        Me.Conversion.Name = "Conversion"
        Me.Conversion.Size = New System.Drawing.Size(1552, 603)
        Me.Conversion.Text = "Conversión"
        '
        'GroupControl10
        '
        Me.GroupControl10.Controls.Add(Me.cmbInversa)
        Me.GroupControl10.Controls.Add(Me.cmbOriginal)
        Me.GroupControl10.Controls.Add(Me.CheckEdit1)
        Me.GroupControl10.Controls.Add(Me.chkInverso)
        Me.GroupControl10.Controls.Add(Me.txtFactorConver)
        Me.GroupControl10.Controls.Add(Label33)
        Me.GroupControl10.Controls.Add(Label23)
        Me.GroupControl10.Controls.Add(Me.chkActivarConver)
        Me.GroupControl10.Controls.Add(Me.ToolStrip1)
        Me.GroupControl10.Controls.Add(Me.Label32)
        Me.GroupControl10.Controls.Add(Label35)
        Me.GroupControl10.Controls.Add(Label36)
        Me.GroupControl10.Controls.Add(Me.GroupControl11)
        Me.GroupControl10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl10.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl10.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl10.Name = "GroupControl10"
        Me.GroupControl10.Size = New System.Drawing.Size(1552, 603)
        Me.GroupControl10.TabIndex = 0
        '
        'cmbInversa
        '
        Me.cmbInversa.Location = New System.Drawing.Point(196, 113)
        Me.cmbInversa.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbInversa.MenuManager = Me.RibbonControl
        Me.cmbInversa.Name = "cmbInversa"
        Me.cmbInversa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbInversa.Properties.NullText = ""
        Me.cmbInversa.Size = New System.Drawing.Size(234, 22)
        Me.cmbInversa.TabIndex = 14
        '
        'cmbOriginal
        '
        Me.cmbOriginal.Location = New System.Drawing.Point(196, 82)
        Me.cmbOriginal.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbOriginal.MenuManager = Me.RibbonControl
        Me.cmbOriginal.Name = "cmbOriginal"
        Me.cmbOriginal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOriginal.Properties.NullText = ""
        Me.cmbOriginal.Size = New System.Drawing.Size(234, 22)
        Me.cmbOriginal.TabIndex = 13
        '
        'CheckEdit1
        '
        Me.CheckEdit1.EditValue = True
        Me.CheckEdit1.Location = New System.Drawing.Point(376, 207)
        Me.CheckEdit1.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckEdit1.MenuManager = Me.RibbonControl
        Me.CheckEdit1.Name = "CheckEdit1"
        Me.CheckEdit1.Properties.Caption = ""
        Me.CheckEdit1.Size = New System.Drawing.Size(43, 24)
        Me.CheckEdit1.TabIndex = 12
        '
        'chkInverso
        '
        Me.chkInverso.AutoSize = True
        Me.chkInverso.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkInverso.Checked = True
        Me.chkInverso.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkInverso.Location = New System.Drawing.Point(345, 178)
        Me.chkInverso.Margin = New System.Windows.Forms.Padding(4)
        Me.chkInverso.Name = "chkInverso"
        Me.chkInverso.Size = New System.Drawing.Size(71, 20)
        Me.chkInverso.TabIndex = 9
        Me.chkInverso.Text = "Inverso"
        Me.chkInverso.UseVisualStyleBackColor = True
        '
        'txtFactorConver
        '
        Me.txtFactorConver.DecimalPlaces = 2
        Me.txtFactorConver.Location = New System.Drawing.Point(196, 145)
        Me.txtFactorConver.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFactorConver.Name = "txtFactorConver"
        Me.txtFactorConver.Size = New System.Drawing.Size(234, 23)
        Me.txtFactorConver.TabIndex = 7
        '
        'chkActivarConver
        '
        Me.chkActivarConver.EditValue = True
        Me.chkActivarConver.Location = New System.Drawing.Point(196, 180)
        Me.chkActivarConver.Margin = New System.Windows.Forms.Padding(4)
        Me.chkActivarConver.MenuManager = Me.RibbonControl
        Me.chkActivarConver.Name = "chkActivarConver"
        Me.chkActivarConver.Properties.Caption = ""
        Me.chkActivarConver.Size = New System.Drawing.Size(43, 24)
        Me.chkActivarConver.TabIndex = 10
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdNuevoCn, Me.cmdSaveCn, Me.cmdDesactivarCn})
        Me.ToolStrip1.Location = New System.Drawing.Point(2, 28)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1548, 27)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip2"
        '
        'cmdNuevoCn
        '
        Me.cmdNuevoCn.Image = CType(resources.GetObject("cmdNuevoCn.Image"), System.Drawing.Image)
        Me.cmdNuevoCn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdNuevoCn.Name = "cmdNuevoCn"
        Me.cmdNuevoCn.Size = New System.Drawing.Size(76, 24)
        Me.cmdNuevoCn.Text = "Nuevo"
        '
        'cmdSaveCn
        '
        Me.cmdSaveCn.Image = CType(resources.GetObject("cmdSaveCn.Image"), System.Drawing.Image)
        Me.cmdSaveCn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSaveCn.Name = "cmdSaveCn"
        Me.cmdSaveCn.Size = New System.Drawing.Size(86, 24)
        Me.cmdSaveCn.Text = "Guardar"
        '
        'cmdDesactivarCn
        '
        Me.cmdDesactivarCn.Image = CType(resources.GetObject("cmdDesactivarCn.Image"), System.Drawing.Image)
        Me.cmdDesactivarCn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDesactivarCn.Name = "cmdDesactivarCn"
        Me.cmdDesactivarCn.Size = New System.Drawing.Size(102, 24)
        Me.cmdDesactivarCn.Text = "Desactivar"
        '
        'Label32
        '
        Me.Label32.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.Navy
        Me.Label32.Location = New System.Drawing.Point(192, 73)
        Me.Label32.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(0, 17)
        Me.Label32.TabIndex = 1
        '
        'GroupControl11
        '
        Me.GroupControl11.Controls.Add(Label49)
        Me.GroupControl11.Controls.Add(Me.GridControl1)
        Me.GroupControl11.Controls.Add(Me.chkActivosCn)
        Me.GroupControl11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl11.Location = New System.Drawing.Point(2, 599)
        Me.GroupControl11.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl11.Name = "GroupControl11"
        Me.GroupControl11.Size = New System.Drawing.Size(1548, 2)
        Me.GroupControl11.TabIndex = 11
        Me.GroupControl11.Text = "Detalle Conversiones "
        '
        'GridControl1
        '
        Me.GridControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridControl1.Cursor = System.Windows.Forms.Cursors.Default
        Me.GridControl1.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode9.RelationName = "Level1"
        Me.GridControl1.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode9})
        Me.GridControl1.Location = New System.Drawing.Point(2, 60)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.GridControl1.MenuManager = Me.RibbonControl
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(1542, 284)
        Me.GridControl1.TabIndex = 2
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'chkActivosCn
        '
        Me.chkActivosCn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkActivosCn.EditValue = True
        Me.chkActivosCn.Location = New System.Drawing.Point(1512, 30)
        Me.chkActivosCn.Margin = New System.Windows.Forms.Padding(4)
        Me.chkActivosCn.MenuManager = Me.RibbonControl
        Me.chkActivosCn.Name = "chkActivosCn"
        Me.chkActivosCn.Properties.Caption = ""
        Me.chkActivosCn.Size = New System.Drawing.Size(21, 24)
        Me.chkActivosCn.TabIndex = 1
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.GrpImagen)
        Me.XtraTabPage1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1552, 603)
        Me.XtraTabPage1.Text = "Imágenes"
        '
        'GrpImagen
        '
        Me.GrpImagen.Controls.Add(Me.GroupControl15)
        Me.GrpImagen.Controls.Add(Me.Panel3)
        Me.GrpImagen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpImagen.Location = New System.Drawing.Point(0, 0)
        Me.GrpImagen.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpImagen.Name = "GrpImagen"
        Me.GrpImagen.Size = New System.Drawing.Size(1552, 603)
        Me.GrpImagen.TabIndex = 4
        '
        'GroupControl15
        '
        Me.GroupControl15.Controls.Add(Me.GrdImagen)
        Me.GroupControl15.Controls.Add(Me.ToolStrip)
        Me.GroupControl15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl15.Location = New System.Drawing.Point(2, 28)
        Me.GroupControl15.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl15.Name = "GroupControl15"
        Me.GroupControl15.Size = New System.Drawing.Size(1120, 573)
        Me.GroupControl15.TabIndex = 0
        Me.GroupControl15.Text = "Lista de Imágenes"
        '
        'GrdImagen
        '
        Me.GrdImagen.Cursor = System.Windows.Forms.Cursors.Default
        Me.GrdImagen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrdImagen.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode10.RelationName = "Level1"
        Me.GrdImagen.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode10})
        Me.GrdImagen.Location = New System.Drawing.Point(2, 55)
        Me.GrdImagen.MainView = Me.GridViewImg
        Me.GrdImagen.Margin = New System.Windows.Forms.Padding(4)
        Me.GrdImagen.MenuManager = Me.RibbonControl
        Me.GrdImagen.Name = "GrdImagen"
        Me.GrdImagen.Size = New System.Drawing.Size(1116, 516)
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
        Me.ToolStrip.Location = New System.Drawing.Point(2, 28)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip.Size = New System.Drawing.Size(1116, 27)
        Me.ToolStrip.TabIndex = 0
        Me.ToolStrip.Text = "ToolStrip4"
        '
        'cmdAdd
        '
        Me.cmdAdd.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.Image = Global.TOMWMS.My.Resources.Resources.add
        Me.cmdAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(29, 24)
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
        Me.Panel3.Controls.Add(Me.Label14)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(1122, 28)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(428, 573)
        Me.Panel3.TabIndex = 94
        '
        'PicImg
        '
        Me.PicImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PicImg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PicImg.Location = New System.Drawing.Point(0, 27)
        Me.PicImg.Margin = New System.Windows.Forms.Padding(4)
        Me.PicImg.Name = "PicImg"
        Me.PicImg.Size = New System.Drawing.Size(428, 546)
        Me.PicImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PicImg.TabIndex = 93
        Me.PicImg.TabStop = False
        Me.PicImg.Visible = False
        '
        'Label14
        '
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(428, 27)
        Me.Label14.TabIndex = 1
        Me.Label14.Text = "Nombre Imagen"
        '
        'tabTallaColor
        '
        Me.tabTallaColor.Controls.Add(Me.GroupControl7)
        Me.tabTallaColor.Name = "tabTallaColor"
        Me.tabTallaColor.Size = New System.Drawing.Size(1552, 603)
        Me.tabTallaColor.Text = "Talla/Color"
        '
        'GroupControl7
        '
        Me.GroupControl7.Controls.Add(Me.dgridTallaColor)
        Me.GroupControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl7.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl7.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl7.Name = "GroupControl7"
        Me.GroupControl7.Size = New System.Drawing.Size(1552, 603)
        Me.GroupControl7.TabIndex = 30
        Me.GroupControl7.Text = "Detalle Tallas y Colores"
        '
        'dgridTallaColor
        '
        Me.dgridTallaColor.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgridTallaColor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridTallaColor.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode11.RelationName = "Level1"
        Me.dgridTallaColor.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode11})
        Me.dgridTallaColor.Location = New System.Drawing.Point(2, 28)
        Me.dgridTallaColor.MainView = Me.GridView12
        Me.dgridTallaColor.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridTallaColor.MenuManager = Me.RibbonControl
        Me.dgridTallaColor.Name = "dgridTallaColor"
        Me.dgridTallaColor.Size = New System.Drawing.Size(1548, 573)
        Me.dgridTallaColor.TabIndex = 0
        Me.dgridTallaColor.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView12, Me.GridView13})
        '
        'GridView12
        '
        Me.GridView12.DetailHeight = 431
        Me.GridView12.GridControl = Me.dgridTallaColor
        Me.GridView12.Name = "GridView12"
        Me.GridView12.OptionsBehavior.Editable = False
        Me.GridView12.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GridView12.OptionsView.ShowGroupPanel = False
        '
        'GridView13
        '
        Me.GridView13.DetailHeight = 431
        Me.GridView13.GridControl = Me.dgridTallaColor
        Me.GridView13.Name = "GridView13"
        '
        'dkProducto
        '
        Me.dkProducto.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkProducto.Form = Me
        Me.dkProducto.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 826)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1554, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("a4314c96-a0a8-4769-9746-301a723d3b29")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 933)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 105)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1645, 105)
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
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1637, 66)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'DetalleBindingSource
        '
        Me.DetalleBindingSource.DataMember = "Detalle"
        Me.DetalleBindingSource.DataSource = Me.DsResumenStockBindingSource
        '
        'frmProducto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1554, 882)
        Me.Controls.Add(Me.TabDatos)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmProducto"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Mantenimiento de Producto"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GridView10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EncabezadoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsResumenStockBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsResumenStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsProducto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabDatos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabDatos.ResumeLayout(False)
        Me.TabProducto.ResumeLayout(False)
        CType(Me.GrpProducto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpProducto.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.Panel2.PerformLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.cmbTipoManufactura.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lcmbParametroB.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lcmbParametroA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lcmbUnidadMedidaCobro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdTipoProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lcmbMarca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lcmbFamilia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lcmbClasificacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lcmbUnidadMedidaBasica.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lcmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdUmPrecio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreUMPrecio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoBarra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpDimensionesUMBas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDimensionesUMBas.ResumeLayout(False)
        Me.grpDimensionesUMBas.PerformLayout()
        CType(Me.txtMargen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAnchoUB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLargoUB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAltoUB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpImagenProducto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpImagenProducto.ResumeLayout(False)
        Me.grpImagenProducto.PerformLayout()
        CType(Me.picFoto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSymbology.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEtiqueta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabAtributo.ResumeLayout(False)
        CType(Me.GrpAtributo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpAtributo.ResumeLayout(False)
        Me.GrpAtributo.PerformLayout()
        CType(Me.chkGeneraLicAutoP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl14, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl14.ResumeLayout(False)
        CType(Me.picFormulaIndiceRotacionWMS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbIndiceRotacionWMS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiasPromedioInventario, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbIndiceRotacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCamara.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTipoRotacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.chkEsHW.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoParte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.txtCicloVida, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkControlVencimiento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTolerancia, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.cmbArancel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCapturaArancel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.cmbPerfilSerializado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSerializado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.chkFechaManufactura.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCapturarAniada.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEsKit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEsMateriaPrima.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkControlLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGeneraLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpTemperatura.ResumeLayout(False)
        Me.GrpTemperatura.PerformLayout()
        CType(Me.txtTemperaturaTolerancia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTemperaturaReferencia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCapturaTemperatura.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpPeso.ResumeLayout(False)
        Me.GrpPeso.PerformLayout()
        CType(Me.txtPesoTolerancia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPesoReferencia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCapturarPeso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrecio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCosto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExistenciaMaxima, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExitenciaMinima, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabParametros.ResumeLayout(False)
        CType(Me.GprParametro, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GprParametro.ResumeLayout(False)
        Me.GprParametro.PerformLayout()
        CType(Me.cmbParametro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivarParametro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripP.ResumeLayout(False)
        Me.ToolStripP.PerformLayout()
        CType(Me.GrpParametro, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpParametro.ResumeLayout(False)
        Me.GrpParametro.PerformLayout()
        CType(Me.DgridParametros, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivoParametro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabCodigoBarra.ResumeLayout(False)
        CType(Me.GrpCodigoBarra, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpCodigoBarra.ResumeLayout(False)
        Me.GrpCodigoBarra.PerformLayout()
        CType(Me.cmbProveedor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrdCodigoBarra, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewCB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivarCB.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripC.ResumeLayout(False)
        Me.ToolStripC.PerformLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.chkActivoCB.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoBarraL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabProductoBodega.ResumeLayout(False)
        CType(Me.GrpPB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpPB.ResumeLayout(False)
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.dgridProductoBodega, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrdProductoBodega, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPresentacion.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.GrpPresentacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpPresentacion.ResumeLayout(False)
        Me.GrpPresentacion.PerformLayout()
        CType(Me.cmbEtiquetaPresentacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSistema.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGeneraLPAuto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermitirPaletizar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkEsPallet.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        CType(Me.txtMaximoPeso, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMinimoPeso, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMinimoExistencia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMaximoExistencia, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpConfigPallet.ResumeLayout(False)
        Me.grpConfigPallet.PerformLayout()
        CType(Me.cmbPresentacionPallet.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCamasPorTarima, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCajasPorCama, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivarPR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripPR.ResumeLayout(False)
        Me.ToolStripPR.PerformLayout()
        CType(Me.txtCodigoBarraPresentacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFactor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkImprimeBarra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAncho, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLargo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombrePresentacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAlto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPeso, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        CType(Me.dGridPresentacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrdPresentacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivoPR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPresentacionTarima.ResumeLayout(False)
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl8.ResumeLayout(False)
        Me.GroupControl8.PerformLayout()
        CType(Me.cmbTipoTarima.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPresentacionTarima.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidadPorCama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripPT.ResumeLayout(False)
        Me.ToolStripPT.PerformLayout()
        CType(Me.chkActivoPT2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl9.ResumeLayout(False)
        Me.GroupControl9.PerformLayout()
        CType(Me.GridPresentacionTarima, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewPT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivoPT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabProductoSustituto.ResumeLayout(False)
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.cmbPresentacionR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbProductoP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivarPS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripPS.ResumeLayout(False)
        Me.ToolStripPS.PerformLayout()
        CType(Me.txtIdProductoR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombrePR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpProductoReeemplazo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpProductoReeemplazo.ResumeLayout(False)
        Me.GrpProductoReeemplazo.PerformLayout()
        CType(Me.GrdProductoS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewProductoS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivoPS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabProductoRellenado.ResumeLayout(False)
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6.ResumeLayout(False)
        Me.GroupControl6.PerformLayout()
        Me.splitContainerReabasto.Panel1.ResumeLayout(False)
        Me.splitContainerReabasto.Panel2.ResumeLayout(False)
        CType(Me.splitContainerReabasto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitContainerReabasto.ResumeLayout(False)
        CType(Me.grpReabastecimiento, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpReabastecimiento.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.cmbOperadorDefecto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPresentacionPR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbProductoEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdUnidadMedidaBasicaRellenado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreUMBasRellenado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivarProductoPRL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMaximoPicking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodegaRellenado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMinimoPicking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPresentacionAbastecerCon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreUMBasReabastecerCon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdUMBasReabastecerCon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridProductoRellenado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewProductoRellenado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkProductoPRL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripPRL.ResumeLayout(False)
        Me.ToolStripPRL.PerformLayout()
        Me.tabProductoKit.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.GroupControl12, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl12.ResumeLayout(False)
        CType(Me.grdPrdKit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl13.ResumeLayout(False)
        Me.GroupControl13.PerformLayout()
        CType(Me.txtCodPrdHijo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantPrdHijo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombrePrdHijo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNomUMBHijo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdUMBHijo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.tabStock.ResumeLayout(False)
        Me.Conversion.ResumeLayout(False)
        CType(Me.GroupControl10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl10.ResumeLayout(False)
        Me.GroupControl10.PerformLayout()
        CType(Me.cmbInversa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbOriginal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CheckEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFactorConver, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivarConver.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.GroupControl11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl11.ResumeLayout(False)
        Me.GroupControl11.PerformLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivosCn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.GrpImagen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpImagen.ResumeLayout(False)
        CType(Me.GroupControl15, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl15.ResumeLayout(False)
        Me.GroupControl15.PerformLayout()
        CType(Me.GrdImagen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewImg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.PicImg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabTallaColor.ResumeLayout(False)
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl7.ResumeLayout(False)
        CType(Me.dgridTallaColor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkProducto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.DetalleBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericChartRangeControlClient1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents DataBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsProducto As TOMWMS.DsProducto
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
    Friend WithEvents mnuEliminarProducto As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdUbicacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageCategory1 As DevExpress.XtraBars.Ribbon.RibbonPageCategory
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPage2 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPage3 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents TabDatos As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents TabProducto As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GrpProducto As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblC As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkUMPrecio As LinkLabel
    Friend WithEvents txtIdUmPrecio As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreUMPrecio As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LinkLabel3 As LinkLabel
    Friend WithEvents TextEdit5 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit6 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LinkLabel2 As LinkLabel
    Friend WithEvents TextEdit3 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit4 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodigo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Bcc As DevExpress.XtraEditors.BarCodeControl
    Friend WithEvents btnExaminar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents picFoto As PictureBox
    Friend WithEvents txtCodigoBarra As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkTipoProducto As LinkLabel
    Friend WithEvents lnkMarca As LinkLabel
    Friend WithEvents lnkFamilia As LinkLabel
    Friend WithEvents lnkClasificacion As LinkLabel
    Friend WithEvents TabAtributo As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GrpAtributo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents chkEsHW As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtNoParte As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents txtCicloVida As NumericUpDown
    Friend WithEvents chkControlVencimiento As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtTolerancia As NumericUpDown
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents chkCapturaArancel As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents chkSerializado As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtNoSerie As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents chkFechaManufactura As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkCapturarAniada As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkEsKit As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkEsMateriaPrima As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkControlLote As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkGeneraLote As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GrpTemperatura As GroupBox
    Friend WithEvents txtTemperaturaTolerancia As NumericUpDown
    Friend WithEvents txtTemperaturaReferencia As NumericUpDown
    Friend WithEvents chkCapturaTemperatura As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GrpPeso As GroupBox
    Friend WithEvents txtPesoTolerancia As NumericUpDown
    Friend WithEvents txtPesoReferencia As NumericUpDown
    Friend WithEvents chkCapturarPeso As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtPrecio As NumericUpDown
    Friend WithEvents txtCosto As NumericUpDown
    Friend WithEvents txtExistenciaMaxima As NumericUpDown
    Friend WithEvents txtExitenciaMinima As NumericUpDown
    Friend WithEvents TabParametros As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GprParametro As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkActivarParametro As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ToolStripP As ToolStrip
    Friend WithEvents cmdNewP As ToolStripButton
    Friend WithEvents cmdSaveP As ToolStripButton
    Friend WithEvents cmdDesactivarParametro As ToolStripButton
    Friend WithEvents cmdNuevoParametro As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents rdCapturarSiempre As RadioButton
    Friend WithEvents rdCapturarUnaVez As RadioButton
    Friend WithEvents GrpParametro As DevExpress.XtraEditors.GroupControl
    Friend WithEvents DgridParametros As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewP As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkActivoParametro As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtTipo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TabCodigoBarra As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GrpCodigoBarra As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkActivarCB As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ToolStripC As ToolStrip
    Friend WithEvents cmdNewC As ToolStripButton
    Friend WithEvents cmdSaveC As ToolStripButton
    Friend WithEvents cmdDesactivarCodigoBarra As ToolStripButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrdCodigoBarra As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewCB As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkActivoCB As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtCodigoBarraL As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TabProductoBodega As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GrpPB As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Private WithEvents dgridProductoBodega As DevExpress.XtraGrid.GridControl
    Private WithEvents GrdProductoBodega As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colSeleccion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents colIdBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IdProductoBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IdInterno As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents TabPresentacion As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GrpPresentacion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkActivarPR As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ToolStripPR As ToolStrip
    Friend WithEvents cmdNewPR As ToolStripButton
    Friend WithEvents cmdSavePR As ToolStripButton
    Friend WithEvents cmdDesactivarPresentacion As ToolStripButton
    Friend WithEvents txtMaximoPeso As NumericUpDown
    Friend WithEvents txtMinimoPeso As NumericUpDown
    Friend WithEvents txtMinimoExistencia As NumericUpDown
    Friend WithEvents txtMaximoExistencia As NumericUpDown
    Friend WithEvents txtCodigoBarraPresentacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtInfo As TextBox
    Friend WithEvents txtFactor As NumericUpDown
    Friend WithEvents chkImprimeBarra As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtAncho As NumericUpDown
    Friend WithEvents txtLargo As NumericUpDown
    Friend WithEvents txtNombrePresentacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtAlto As NumericUpDown
    Friend WithEvents txtPeso As NumericUpDown
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dGridPresentacion As DevExpress.XtraGrid.GridControl
    Friend WithEvents GrdPresentacion As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkActivoPR As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents tabPresentacionTarima As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl8 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtCantidadPorCama As NumericUpDown
    Friend WithEvents ToolStripPT As ToolStrip
    Friend WithEvents cmdNewPT As ToolStripButton
    Friend WithEvents cmdSavePT As ToolStripButton
    Friend WithEvents cmdDesactivarPresentacionTarima As ToolStripButton
    Friend WithEvents chkActivoPT2 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtCantidad As NumericUpDown
    Friend WithEvents lblNombreProductoPT As Label
    Friend WithEvents GroupControl9 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridPresentacionTarima As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewPT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkActivoPT As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents TabProductoSustituto As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkActivarPS As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ToolStripPS As ToolStrip
    Friend WithEvents cmdNewPS As ToolStripButton
    Friend WithEvents cmdSavePS As ToolStripButton
    Friend WithEvents cmdDesactivarProductoSustituto As ToolStripButton
    Friend WithEvents lblNombreProductoO As Label
    Friend WithEvents lnkProductoR As LinkLabel
    Friend WithEvents txtIdProductoR As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombrePR As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GrpProductoReeemplazo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrdProductoS As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewProductoS As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkActivoPS As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents tabStock As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Conversion As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl10 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkInverso As CheckBox
    Friend WithEvents txtFactorConver As NumericUpDown
    Friend WithEvents chkActivarConver As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents cmdNuevoCn As ToolStripButton
    Friend WithEvents cmdSaveCn As ToolStripButton
    Friend WithEvents cmdDesactivarCn As ToolStripButton
    Friend WithEvents Label32 As Label
    Friend WithEvents GroupControl11 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkActivosCn As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView7 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView8 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView9 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblEtiqueta As Label
    Friend WithEvents txtAnchoUB As NumericUpDown
    Friend WithEvents lblAnchoPr As Label
    Friend WithEvents lblAltoPr As Label
    Friend WithEvents txtLargoUB As NumericUpDown
    Friend WithEvents txtAltoUB As NumericUpDown
    Friend WithEvents lblLargoPr As Label
    Friend WithEvents lnkUnidadMedida As LinkLabel
    Friend WithEvents grpConfigPallet As GroupBox
    Friend WithEvents lblY As Label
    Friend WithEvents lblX As Label
    Friend WithEvents txtCamasPorTarima As NumericUpDown
    Friend WithEvents txtCajasPorCama As NumericUpDown
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents ChkEsPallet As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents CheckEdit1 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents dkProducto As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents chkPermitirPaletizar As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkGeneraLPAuto As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents grdPStock As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colIdProducto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPropietario As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCódigo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colProducto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCódigo_Barra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPresentación As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUM_Bas As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCantidadUMBas As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCantidadPresentación As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridView10 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbSymbology As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbEtiqueta As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbTipoRotacion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbCamara As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbIndiceRotacion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbPerfilSerializado As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbParametro As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbProveedor As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbPresentacionTarima As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbTipoTarima As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbProductoP As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbPresentacionR As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbOriginal As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbInversa As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents EncabezadoBindingSource As BindingSource
    Friend WithEvents DsResumenStockBindingSource As BindingSource
    Friend WithEvents DsResumenStock As DsResumenStock
    Friend WithEvents DetalleBindingSource As BindingSource
    Friend WithEvents cmbArancel As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblSistema As Label
    Friend WithEvents chkSistema As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents cmbPresentacionPallet As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents tabProductoKit As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ToolStrip2 As ToolStrip
    Friend WithEvents cmdAggPrK As ToolStripButton
    Friend WithEvents cmdGuardarPrk As ToolStripButton
    Friend WithEvents cmdEliminarPrk As ToolStripButton
    Friend WithEvents lblProdPadre As Label
    Friend WithEvents linklblProductoKit As LinkLabel
    Friend WithEvents txtCodPrdHijo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombrePrdHijo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNomUMBHijo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdUMBHijo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCantPrdHijo As NumericUpDown
    Friend WithEvents Label30 As Label
    Friend WithEvents NumericChartRangeControlClient1 As DevExpress.XtraEditors.NumericChartRangeControlClient
    Friend WithEvents GroupControl12 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdPrdKit As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView11 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl13 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lcmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents grpDimensionesUMBas As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grpImagenProducto As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lnkUMCobro As LinkLabel
    Friend WithEvents mnuDesactivar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupControl14 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbIndiceRotacionWMS As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtDiasPromedioInventario As NumericUpDown
    Friend WithEvents picFormulaIndiceRotacionWMS As PictureBox
    Friend WithEvents lblIndiceRotacionRef As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDiasInventarioPromedio As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblIndiceRotacionWMS As DevExpress.XtraEditors.LabelControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GrpImagen As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl15 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrdImagen As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewImg As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ToolStrip As ToolStrip
    Friend WithEvents cmdAdd As ToolStripButton
    Friend WithEvents cmdDelete As ToolStripButton
    Friend WithEvents Panel3 As Panel
    Friend WithEvents PicImg As PictureBox
    Friend WithEvents Label14 As Label
    Friend WithEvents lcmbUnidadMedidaBasica As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lcmbClasificacion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lcmbFamilia As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lcmbMarca As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtIdTipoProducto As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lcmbUnidadMedidaCobro As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lcmbParametroB As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lnkParametroB As LinkLabel
    Friend WithEvents lcmbParametroA As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lnkParametroA As LinkLabel
    Friend WithEvents chkGeneraLicAutoP As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cmbTipoManufactura As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents TabProductoRellenado As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl6 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents splitContainerReabasto As SplitContainer
    Friend WithEvents grpReabastecimiento As DevExpress.XtraEditors.GroupControl
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents cmbOperadorDefecto As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblIdRellenado As DevExpress.XtraEditors.LabelControl
    Friend WithEvents optNotificar As RadioButton
    Friend WithEvents cmbPresentacionPR As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lnklblUMBasRellenado As LinkLabel
    Friend WithEvents cmbProductoEstado As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtNombreUbicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents optGenerarAutomaticamente As RadioButton
    Friend WithEvents txtIdUnidadMedidaBasicaRellenado As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdUbicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblNombreProductoPR As Label
    Friend WithEvents txtNombreUMBasRellenado As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkActivarProductoPRL As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtMaximoPicking As NumericUpDown
    Friend WithEvents cmbBodegaRellenado As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lnkUbicacion As LinkLabel
    Friend WithEvents txtMinimoPicking As NumericUpDown
    Friend WithEvents lblReabastecerCon As Label
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents cmbPresentacionAbastecerCon As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtNombreUMBasReabastecerCon As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdUMBasReabastecerCon As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GridProductoRellenado As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewProductoRellenado As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkProductoPRL As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ToolStripPRL As ToolStrip
    Friend WithEvents cmdNewPRL As ToolStripButton
    Friend WithEvents cmdSavePRL As ToolStripButton
    Friend WithEvents cmdDesactivarProductoRellenado As ToolStripButton
    Friend WithEvents lblMargen As Label
    Friend WithEvents txtMargen As NumericUpDown
    Friend WithEvents tabTallaColor As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl7 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dgridTallaColor As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView12 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView13 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbEtiquetaPresentacion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblEtiquetaPresentacion As Label
End Class
