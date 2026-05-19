<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBodega
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If DTArea IsNot Nothing Then
                DTArea.Dispose()
                DTArea = Nothing
            End If
            If DTSector IsNot Nothing Then
                DTSector.Dispose()
                DTSector = Nothing
            End If
            If DTTramo IsNot Nothing Then
                DTTramo.Dispose()
                DTTramo = Nothing
            End If
            If DTUbiacion IsNot Nothing Then
                DTUbiacion.Dispose()
                DTUbiacion = Nothing
            End If
            If pBeBodega IsNot Nothing Then
                pBeBodega.Dispose()
                pBeBodega = Nothing
            End If
            If pObjBAB IsNot Nothing Then
                pObjBAB.Dispose()
            End If
            If pObjBS IsNot Nothing Then
                pObjBS.Dispose()
            End If
            If ObjBP IsNot Nothing Then
                ObjBP.Dispose()
            End If
            If clsLnBodegaP IsNot Nothing Then
                clsLnBodegaP.Dispose()
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
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim Label15 As System.Windows.Forms.Label
        Dim Label16 As System.Windows.Forms.Label
        Dim Label17 As System.Windows.Forms.Label
        Dim Label18 As System.Windows.Forms.Label
        Dim Label20 As System.Windows.Forms.Label
        Dim Label21 As System.Windows.Forms.Label
        Dim lblCodigoSectorReferencia As System.Windows.Forms.Label
        Dim Label26 As System.Windows.Forms.Label
        Dim Label37 As System.Windows.Forms.Label
        Dim Label38 As System.Windows.Forms.Label
        Dim Label39 As System.Windows.Forms.Label
        Dim Label40 As System.Windows.Forms.Label
        Dim Label41 As System.Windows.Forms.Label
        Dim Label42 As System.Windows.Forms.Label
        Dim Label43 As System.Windows.Forms.Label
        Dim Label45 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim lblAnchoSector As System.Windows.Forms.Label
        Dim LlblLargoSector As System.Windows.Forms.Label
        Dim lblAltoSector As System.Windows.Forms.Label
        Dim lblDescripcionSector As System.Windows.Forms.Label
        Dim lblAreaSector As System.Windows.Forms.Label
        Dim lblCodigoSector As System.Windows.Forms.Label
        Dim lblMargenIzquierdoSector As System.Windows.Forms.Label
        Dim lblMargenDerechoSector As System.Windows.Forms.Label
        Dim lblMargenInferiorSector As System.Windows.Forms.Label
        Dim lblMargenSuperiorSector As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim lblDescripcionAreaBodega As System.Windows.Forms.Label
        Dim lblCodigoArea As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim IdEmpresaLabel As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim IdBodegaLabel As System.Windows.Forms.Label
        Dim NombreLabel As System.Windows.Forms.Label
        Dim DireccionLabel As System.Windows.Forms.Label
        Dim TelefonoLabel As System.Windows.Forms.Label
        Dim EmailLabel As System.Windows.Forms.Label
        Dim EncargadoLabel As System.Windows.Forms.Label
        Dim ActivoLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label30 As System.Windows.Forms.Label
        Dim Label28 As System.Windows.Forms.Label
        Dim lblCodigoUbicacionVirtual As System.Windows.Forms.Label
        Dim Label24 As System.Windows.Forms.Label
        Dim Label44 As System.Windows.Forms.Label
        Dim Label46 As System.Windows.Forms.Label
        Dim Label50 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label13 As System.Windows.Forms.Label
        Dim lblAlto As System.Windows.Forms.Label
        Dim lblLargo As System.Windows.Forms.Label
        Dim lblAncho As System.Windows.Forms.Label
        Dim lblLatitud As System.Windows.Forms.Label
        Dim lblLongitud As System.Windows.Forms.Label
        Dim Label31 As System.Windows.Forms.Label
        Dim Label22 As System.Windows.Forms.Label
        Dim Label19 As System.Windows.Forms.Label
        Dim lblCodigoBodegaERP As System.Windows.Forms.Label
        Dim lblUbicCodigoBodegaERP As System.Windows.Forms.Label
        Dim Label48 As System.Windows.Forms.Label
        Dim Label49 As System.Windows.Forms.Label
        Dim lblEsBodegaFiscal As System.Windows.Forms.Label
        Dim lblHabilitarIngresoConsolidado As System.Windows.Forms.Label
        Dim Label51 As System.Windows.Forms.Label
        Dim lblCapturaPalletNoEstandar As System.Windows.Forms.Label
        Dim lblCapturaEstibaIngreso As System.Windows.Forms.Label
        Dim Label52 As System.Windows.Forms.Label
        Dim lblControlBanderasClientePedido As System.Windows.Forms.Label
        Dim lblGrupo As System.Windows.Forms.Label
        Dim lblIdArea As System.Windows.Forms.Label
        Dim Label53 As System.Windows.Forms.Label
        Dim lblIdTramo As System.Windows.Forms.Label
        Dim lblPriorizar As System.Windows.Forms.Label
        Dim Label54 As System.Windows.Forms.Label
        Dim lblPermitirEliminarDocumentoSalida As System.Windows.Forms.Label
        Dim lblMostrarZonaEnHH As System.Windows.Forms.Label
        Dim lblControlOperadorPorUbicacion As System.Windows.Forms.Label
        Dim lblEscanearCodigoProductoEnPicking As System.Windows.Forms.Label
        Dim lblinferir_origen_en_cambio_ubic As System.Windows.Forms.Label
        Dim lblEliminarDocumentosSalida As System.Windows.Forms.Label
        Dim lblPickeadorVerifica As System.Windows.Forms.Label
        Dim lblPermitirCambioDeUbicacionEnPicking As System.Windows.Forms.Label
        Dim Label58 As System.Windows.Forms.Label
        Dim Label59 As System.Windows.Forms.Label
        Dim lblrestringir_vencimiento_en_reemplazo As System.Windows.Forms.Label
        Dim lblrestringir_lote_en_reemplazo As System.Windows.Forms.Label
        Dim Label64 As System.Windows.Forms.Label
        Dim lblPermitirRepeticionesEnIngreso As System.Windows.Forms.Label
        Dim Label66 As System.Windows.Forms.Label
        Dim Label67 As System.Windows.Forms.Label
        Dim lblcalcular_ubicacion_sugerida_ml As System.Windows.Forms.Label
        Dim Label68 As System.Windows.Forms.Label
        Dim Label69 As System.Windows.Forms.Label
        Dim Label70 As System.Windows.Forms.Label
        Dim Label71 As System.Windows.Forms.Label
        Dim Label72 As System.Windows.Forms.Label
        Dim lblPermitirReemplazoPickingMismaLIcencia As System.Windows.Forms.Label
        Dim Label75 As System.Windows.Forms.Label
        Dim Label76 As System.Windows.Forms.Label
        Dim lblSimbologia As System.Windows.Forms.Label
        Dim Label77 As System.Windows.Forms.Label
        Dim Label78 As System.Windows.Forms.Label
        Dim Label79 As System.Windows.Forms.Label
        Dim lbldespachoauto As System.Windows.Forms.Label
        Dim lblLimpiarCampos As System.Windows.Forms.Label
        Dim lblControlTallaColor As System.Windows.Forms.Label
        Dim Label83 As System.Windows.Forms.Label
        Dim Label84 As System.Windows.Forms.Label
        Dim Label85 As System.Windows.Forms.Label
        Dim Label88 As System.Windows.Forms.Label
        Dim Label89 As System.Windows.Forms.Label
        Dim Label90 As System.Windows.Forms.Label
        Dim Label91 As System.Windows.Forms.Label
        Dim Label92 As System.Windows.Forms.Label
        Dim Label93 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBodega))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim ButtonImageOptions1 As DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions = New DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions()
        Dim Code128Generator1 As DevExpress.XtraPrinting.BarCode.Code128Generator = New DevExpress.XtraPrinting.BarCode.Code128Generator()
        Me.lblControlGondola = New System.Windows.Forms.Label()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.lblRutaCDN = New System.Windows.Forms.Label()
        Me.lblHomologarLoteConFechaVence = New System.Windows.Forms.Label()
        Me.lblEscanearLicenciaPicking = New System.Windows.Forms.Label()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl12 = New DevExpress.XtraEditors.GroupControl()
        Me.txtIdTramo = New DevExpress.XtraEditors.TextEdit()
        Me.txtTipoRack = New System.Windows.Forms.NumericUpDown()
        Me.chkOrient = New DevExpress.XtraEditors.CheckEdit()
        Me.mnu = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuDiseñoGrafico = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEstructuraInicial = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdRefrescar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuParametrosInterface = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEditarConnIni = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuUnificarBodegas = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdHabilitarReemplazo = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdDeshabilitarReemplazo = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizarIndicesRotacion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPlantillaIndicesRotacion = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage2 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup4 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup5 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.txtIndice = New DevExpress.XtraEditors.TextEdit()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.chkEsRack = New DevExpress.XtraEditors.CheckEdit()
        Me.cmbFont = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbSector = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbAreasR = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.chkActivoTramo = New DevExpress.XtraEditors.CheckEdit()
        Me.txtCodigoTramo = New DevExpress.XtraEditors.TextEdit()
        Me.chkSistemaTramo = New DevExpress.XtraEditors.CheckEdit()
        Me.txtDescripcionTramo = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl11 = New DevExpress.XtraEditors.GroupControl()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.chkOrientacion = New System.Windows.Forms.CheckBox()
        Me.nUpdMargenInferiorTramo = New System.Windows.Forms.NumericUpDown()
        Me.nUpdMargenSuperiorTramo = New System.Windows.Forms.NumericUpDown()
        Me.nUpdMargenDerechoTramo = New System.Windows.Forms.NumericUpDown()
        Me.nUpdMargenIzquierdoTramo = New System.Windows.Forms.NumericUpDown()
        Me.nUpdAnchoTramo = New System.Windows.Forms.NumericUpDown()
        Me.nUpdLargoTramo = New System.Windows.Forms.NumericUpDown()
        Me.nUpdAltoTramo = New System.Windows.Forms.NumericUpDown()
        Me.GroupControl10 = New DevExpress.XtraEditors.GroupControl()
        Me.grdTramo = New DevExpress.XtraGrid.GridControl()
        Me.GridViewTramo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.chkTramosActivos = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupControl14 = New DevExpress.XtraEditors.GroupControl()
        Me.chkUbicacionMuelle = New DevExpress.XtraEditors.CheckEdit()
        Me.chkUbicPrdNE = New DevExpress.XtraEditors.CheckEdit()
        Me.chkUbicacionesActivas = New DevExpress.XtraEditors.CheckEdit()
        Me.cmbOrientacion = New DevExpress.XtraEditors.LookUpEdit()
        Me.chkEsBodegaVirtual = New DevExpress.XtraEditors.CheckEdit()
        Me.nUpdMargenInferiorUbicacion = New System.Windows.Forms.NumericUpDown()
        Me.nUpdMargenSuperiorUbicacion = New System.Windows.Forms.NumericUpDown()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.nUpdMargenDerechoUbicacion = New System.Windows.Forms.NumericUpDown()
        Me.chkMerma = New DevExpress.XtraEditors.CheckEdit()
        Me.nUpdMargenIzquierdoUbicacion = New System.Windows.Forms.NumericUpDown()
        Me.nUpdAnchoUbicacion = New System.Windows.Forms.NumericUpDown()
        Me.nUpdLargoUbicacion = New System.Windows.Forms.NumericUpDown()
        Me.nUpdAltoUbicacion = New System.Windows.Forms.NumericUpDown()
        Me.chkDespacho = New DevExpress.XtraEditors.CheckEdit()
        Me.chkDañadoUbicacion = New DevExpress.XtraEditors.CheckEdit()
        Me.chkUbicacionPicking = New DevExpress.XtraEditors.CheckEdit()
        Me.chkActivoUbicacion = New DevExpress.XtraEditors.CheckEdit()
        Me.chkAceptaPalletUbicacion = New DevExpress.XtraEditors.CheckEdit()
        Me.chkRecepcion = New DevExpress.XtraEditors.CheckEdit()
        Me.chkBloqueadaUbicacion = New DevExpress.XtraEditors.CheckEdit()
        Me.chkSistemaUbicacion = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupControl15 = New DevExpress.XtraEditors.GroupControl()
        Me.grdUbicacion = New DevExpress.XtraGrid.GridControl()
        Me.GridViewUbi = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl16 = New DevExpress.XtraEditors.GroupControl()
        Me.tlUbicaciones = New DevExpress.XtraTreeList.TreeList()
        Me.Dgrid = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtTiempoActualizacionP = New System.Windows.Forms.NumericUpDown()
        Me.txtNombreTarea = New DevExpress.XtraEditors.TextEdit()
        Me.lnkTareas = New System.Windows.Forms.LinkLabel()
        Me.txtIdTarea = New DevExpress.XtraEditors.TextEdit()
        Me.DsBodega = New TOMWMS.DsBodega()
        Me.BodegaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ControlPanelBodega = New DevExpress.XtraTab.XtraTabControl()
        Me.tabDatos = New DevExpress.XtraTab.XtraTabPage()
        Me.grpDatosGen = New DevExpress.XtraEditors.GroupControl()
        Me.XtraScrollableControl = New DevExpress.XtraEditors.XtraScrollableControl()
        Me.gpSmtp = New DevExpress.XtraEditors.GroupControl()
        Me.txtPassword = New DevExpress.XtraEditors.TextEdit()
        Me.txtUsuario = New DevExpress.XtraEditors.TextEdit()
        Me.txtPuerto = New DevExpress.XtraEditors.TextEdit()
        Me.txtServidor = New DevExpress.XtraEditors.TextEdit()
        Me.chkSsl = New DevExpress.XtraEditors.CheckEdit()
        Me.gcCentroCosto = New DevExpress.XtraEditors.GroupControl()
        Me.cmbCentroCostoDepERP = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbCentroCostoDirERP = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbCentroCostoERP = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmdRutaCDN = New DevExpress.XtraEditors.SimpleButton()
        Me.txtRutaCDN = New DevExpress.XtraEditors.TextEdit()
        Me.GrpTIpoTransaccion = New DevExpress.XtraEditors.GroupControl()
        Me.txtNombreDocumentoSalida = New DevExpress.XtraEditors.TextEdit()
        Me.lnkTipoSalida = New System.Windows.Forms.LinkLabel()
        Me.txtIdTipoDocumentoSalida = New DevExpress.XtraEditors.TextEdit()
        Me.txtDescripcionTR = New DevExpress.XtraEditors.TextEdit()
        Me.lnkTipoT = New System.Windows.Forms.LinkLabel()
        Me.txtIdTipoTR = New DevExpress.XtraEditors.TextEdit()
        Me.cmbTamañoEtiquetaUbicacionDefecto = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblTamañoEtiquetaUbicacionDefecto = New System.Windows.Forms.Label()
        Me.txtCodigoBodegaERP = New DevExpress.XtraEditors.TextEdit()
        Me.lblMensajeUbicacionesDef = New System.Windows.Forms.Label()
        Me.cmbEmpresa = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbPais = New DevExpress.XtraEditors.LookUpEdit()
        Me.EncargadoTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.EmailTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.TelefonoTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.DireccionTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreComercial = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigoBarra = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigo = New DevExpress.XtraEditors.TextEdit()
        Me.tabDimensionesBod = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl18 = New DevExpress.XtraEditors.GroupControl()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtZoom = New System.Windows.Forms.NumericUpDown()
        Me.txtAlto = New System.Windows.Forms.NumericUpDown()
        Me.txtCoordenadaY = New DevExpress.XtraEditors.TextEdit()
        Me.txtLargo = New System.Windows.Forms.NumericUpDown()
        Me.txtCoordenadaX = New DevExpress.XtraEditors.TextEdit()
        Me.txtAncho = New System.Windows.Forms.NumericUpDown()
        Me.tabArea = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.txtNombreUbicacionRecepcionArea = New DevExpress.XtraEditors.TextEdit()
        Me.lnkUbicacionRecepcionArea = New System.Windows.Forms.LinkLabel()
        Me.txtUbicacionRecepcionArea = New DevExpress.XtraEditors.TextEdit()
        Me.txtGrupoArea = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdArea = New DevExpress.XtraEditors.TextEdit()
        Me.chkActivoAreaBodega = New DevExpress.XtraEditors.CheckEdit()
        Me.txtCodigoAreaBodega = New DevExpress.XtraEditors.TextEdit()
        Me.chkSistemaAreaBodega = New DevExpress.XtraEditors.CheckEdit()
        Me.txtDescripcionAreaBodega = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl()
        Me.nUpdMargenInferior = New System.Windows.Forms.NumericUpDown()
        Me.nUpdMargenSuperior = New System.Windows.Forms.NumericUpDown()
        Me.nUpdMargenDerecho = New System.Windows.Forms.NumericUpDown()
        Me.nUpdMargenIzquierdo = New System.Windows.Forms.NumericUpDown()
        Me.nUpdAlto = New System.Windows.Forms.NumericUpDown()
        Me.nUpdAncho = New System.Windows.Forms.NumericUpDown()
        Me.nUpdLargo = New System.Windows.Forms.NumericUpDown()
        Me.ToolStripPR = New System.Windows.Forms.ToolStrip()
        Me.cmdNuevaArea = New System.Windows.Forms.ToolStripButton()
        Me.cmdGuardarArea = New System.Windows.Forms.ToolStripButton()
        Me.GroupControl17 = New DevExpress.XtraEditors.GroupControl()
        Me.grdAreaBodega = New DevExpress.XtraGrid.GridControl()
        Me.GridViewArea = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.chkAreasBodegaActivos = New DevExpress.XtraEditors.CheckEdit()
        Me.tabSector = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.GroupControl8 = New DevExpress.XtraEditors.GroupControl()
        Me.txtIdSector = New DevExpress.XtraEditors.TextEdit()
        Me.cmbArea = New DevExpress.XtraEditors.LookUpEdit()
        Me.chkActivoSector = New DevExpress.XtraEditors.CheckEdit()
        Me.txtCodigoSector = New DevExpress.XtraEditors.TextEdit()
        Me.nUpdAnchoSector = New System.Windows.Forms.NumericUpDown()
        Me.chkSistemaSector = New DevExpress.XtraEditors.CheckEdit()
        Me.nUpdLargoSector = New System.Windows.Forms.NumericUpDown()
        Me.txtDescripcionSector = New DevExpress.XtraEditors.TextEdit()
        Me.nUpdAltoSector = New System.Windows.Forms.NumericUpDown()
        Me.GroupControl7 = New DevExpress.XtraEditors.GroupControl()
        Me.txtPosY = New System.Windows.Forms.NumericUpDown()
        Me.txtPosX = New System.Windows.Forms.NumericUpDown()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.chkHorizontal = New System.Windows.Forms.CheckBox()
        Me.nUpdMargenInferiorSector = New System.Windows.Forms.NumericUpDown()
        Me.nUpdMargenSuperiorSector = New System.Windows.Forms.NumericUpDown()
        Me.nUpdMargenDerechoSector = New System.Windows.Forms.NumericUpDown()
        Me.nUpdMargenIzquierdoSector = New System.Windows.Forms.NumericUpDown()
        Me.GroupControl9 = New DevExpress.XtraEditors.GroupControl()
        Me.grdSectorArea = New DevExpress.XtraGrid.GridControl()
        Me.GridViewSec = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.chkSectoresActivos = New DevExpress.XtraEditors.CheckEdit()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsmnuNuevoSector = New System.Windows.Forms.ToolStripButton()
        Me.tsmnuGuardarSector = New System.Windows.Forms.ToolStripButton()
        Me.tabTramo = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.tsmnuNuevoTramo = New System.Windows.Forms.ToolStripButton()
        Me.tsmnuGuardarTramo = New System.Windows.Forms.ToolStripButton()
        Me.TabUbicacion = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.GroupControl13 = New DevExpress.XtraEditors.GroupControl()
        Me.txtUbicCodigoBodegaERP = New DevExpress.XtraEditors.TextEdit()
        Me.cmbTramo = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbSectorR = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbAreaUbic = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbIndiceRotacion = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbTipoRotacion = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtIndiceX = New System.Windows.Forms.NumericUpDown()
        Me.nUpdNivelUbicacion = New System.Windows.Forms.NumericUpDown()
        Me.txtCodigoBarra2ubicacion = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigoBarraUbicacion = New DevExpress.XtraEditors.TextEdit()
        Me.txtDescripcionUbicacion = New DevExpress.XtraEditors.TextEdit()
        Me.ToolStrip3 = New System.Windows.Forms.ToolStrip()
        Me.tsmnuNuevaUbicacion = New System.Windows.Forms.ToolStripButton()
        Me.tsmnuGuardarUbicacion = New System.Windows.Forms.ToolStripButton()
        Me.tabReferencia = New DevExpress.XtraTab.XtraTabPage()
        Me.tabParametros = New DevExpress.XtraTab.XtraTabPage()
        Me.tabUbicacionesDefecto = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.chkControlGondola = New DevExpress.XtraEditors.CheckEdit()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.nudRangoDiasDocumentos = New System.Windows.Forms.NumericUpDown()
        Me.chkControlTallaColor = New DevExpress.XtraEditors.CheckEdit()
        Me.chkrestringir_vencimiento_en_reemplazo = New DevExpress.XtraEditors.CheckEdit()
        Me.chkrestringir_lote_en_reemplazo = New DevExpress.XtraEditors.CheckEdit()
        Me.chkLberarStockDepachosParciales = New DevExpress.XtraEditors.CheckEdit()
        Me.chkHomologarLoteConFechaVence = New DevExpress.XtraEditors.CheckEdit()
        Me.chkValidarExistenciasEnCargaInventarioInicial = New DevExpress.XtraEditors.CheckEdit()
        Me.chkControlPalletsMixtos = New DevExpress.XtraEditors.CheckEdit()
        Me.chkControlOperadorUbicacion = New DevExpress.XtraEditors.CheckEdit()
        Me.chkinferir_origen_en_cambio_ubic = New DevExpress.XtraEditors.CheckEdit()
        Me.chkValidarDisponibilidadEnUbicacionDestino = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.chkBodegaClienteAjusteByB = New DevExpress.XtraEditors.CheckEdit()
        Me.lblBodegaClienteAjusteByB = New System.Windows.Forms.Label()
        Me.chkreemplazoOpcional = New DevExpress.XtraEditors.CheckEdit()
        Me.chkImprimir_Verificacion = New DevExpress.XtraEditors.CheckEdit()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.chkAdvertirMpqUmbas = New DevExpress.XtraEditors.CheckEdit()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.chkAgrupar_sin_lic_veri_no_cons = New DevExpress.XtraEditors.CheckEdit()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.chkVerificacion_Consolidada = New DevExpress.XtraEditors.CheckEdit()
        Me.chkControlBanderasCliente = New DevExpress.XtraEditors.CheckEdit()
        Me.chkDespacharProductoVencido = New DevExpress.XtraEditors.CheckEdit()
        Me.chkPermitirReemplazoVerificacion = New DevExpress.XtraEditors.CheckEdit()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.chkPermitir_Verificacion_Consolidada = New DevExpress.XtraEditors.CheckEdit()
        Me.txtDiasMaximoVencimientoReemplazo = New System.Windows.Forms.NumericUpDown()
        Me.chkdespachoautohh = New DevExpress.XtraEditors.CheckEdit()
        Me.chkPermitirEliminarDocumentosSalida = New DevExpress.XtraEditors.CheckEdit()
        Me.chkEliminarDocumentosSalida = New DevExpress.XtraEditors.CheckEdit()
        Me.chkFiltrarPedidosUsuario = New DevExpress.XtraEditors.CheckEdit()
        Me.lblCambioUbicacionRestrictivo = New System.Windows.Forms.Label()
        Me.chkCambioUbicacionRestrictivo = New DevExpress.XtraEditors.CheckEdit()
        Me.lblPermitirCambioUbicIndiceMenor = New System.Windows.Forms.Label()
        Me.chkPermitirCambioUbicIndiceMenor = New DevExpress.XtraEditors.CheckEdit()
        Me.lblRequerirMismoProductoPosiciones = New System.Windows.Forms.Label()
        Me.chkRequerirMismoProductoPosiciones = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.chkCambioUbiAuto = New DevExpress.XtraEditors.CheckEdit()
        Me.chkControlTarifaServ = New DevExpress.XtraEditors.CheckEdit()
        Me.chkEsBodegaFiscal = New DevExpress.XtraEditors.CheckEdit()
        Me.chkLimpiarCamposHH = New DevExpress.XtraEditors.CheckEdit()
        Me.chkNotificacionVoz = New DevExpress.XtraEditors.CheckEdit()
        Me.chkEsMotriz = New DevExpress.XtraEditors.CheckEdit()
        Me.chkRestringirAreasSAP = New DevExpress.XtraEditors.CheckEdit()
        Me.chkMostrarAreaEnHH = New DevExpress.XtraEditors.CheckEdit()
        Me.chkInterface_SAP = New DevExpress.XtraEditors.CheckEdit()
        Me.chkcalcular_ubicacion_sugerida_ml = New DevExpress.XtraEditors.CheckEdit()
        Me.chkPermitirDecimales = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.chkControlGuia = New DevExpress.XtraEditors.CheckEdit()
        Me.chkOperadorPickingVerifica = New DevExpress.XtraEditors.CheckEdit()
        Me.chkPermitirNoEncontradoPicking = New DevExpress.XtraEditors.CheckEdit()
        Me.chkPermitirReemplazoPickingMismaLIcencia = New DevExpress.XtraEditors.CheckEdit()
        Me.chkEscanearLicenciaPicking = New DevExpress.XtraEditors.CheckEdit()
        Me.chkPermitirReemplazoPicking = New DevExpress.XtraEditors.CheckEdit()
        Me.chkpermitir_buen_estado_en_reemplazo = New DevExpress.XtraEditors.CheckEdit()
        Me.chkEscanearCodigoProductoEnPicking = New DevExpress.XtraEditors.CheckEdit()
        Me.chkPermitirCambioUbicacionPicking = New DevExpress.XtraEditors.CheckEdit()
        Me.chkOrdenarPickingDescendente = New DevExpress.XtraEditors.CheckEdit()
        Me.chkOrdenarNombreCompleto = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbEstadoDefectoRack = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.chkPermitirCambioUbicacionRecepcion = New DevExpress.XtraEditors.CheckEdit()
        Me.chkBloquearLpHH = New DevExpress.XtraEditors.CheckEdit()
        Me.chkIngresoConsolidado = New DevExpress.XtraEditors.CheckEdit()
        Me.chkPriorizar_UbicRec_Sobre_UbicEst = New DevExpress.XtraEditors.CheckEdit()
        Me.chkCapturaPalletNoEstandar = New DevExpress.XtraEditors.CheckEdit()
        Me.chkCapturaEstibaIngreso = New DevExpress.XtraEditors.CheckEdit()
        Me.chkPermitirRepeticionesEnIngreso = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.cmbEtiquetaVerificacion = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label99 = New System.Windows.Forms.Label()
        Me.Bcc = New DevExpress.XtraEditors.BarCodeControl()
        Me.cmbSymbology = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbEtiqueta = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblEtiqueta = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.dtHorarioEjecucionHistorico = New DevExpress.XtraEditors.DateTimeOffsetEdit()
        Me.txtIdDiasLimiteRetroactivo = New System.Windows.Forms.NumericUpDown()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.nudTopReabastecimientoManual = New System.Windows.Forms.NumericUpDown()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.txtIdConfiguracionPantallaRecepcion = New System.Windows.Forms.NumericUpDown()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.txtIdConfiguracionPantallaVerificacion = New System.Windows.Forms.NumericUpDown()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.txtIdConfiguracionPantallaPicking = New System.Windows.Forms.NumericUpDown()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.txtValorIVA = New System.Windows.Forms.NumericUpDown()
        Me.lblValorIVA = New System.Windows.Forms.LinkLabel()
        Me.txtIdMotivoUbicReabasto = New DevExpress.XtraEditors.TextEdit()
        Me.txtMotivoUbicReabasto = New DevExpress.XtraEditors.TextEdit()
        Me.lnkReabasto = New System.Windows.Forms.LinkLabel()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.cmbEstadoNe = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtIdUbicacionPrdNE = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreUbicNE = New DevExpress.XtraEditors.TextEdit()
        Me.lnkUbicPrdNE = New System.Windows.Forms.LinkLabel()
        Me.txtidmotivoubicaciondañadopicking = New DevExpress.XtraEditors.TextEdit()
        Me.txtMotivoUbicacionDañadoPicking = New DevExpress.XtraEditors.TextEdit()
        Me.lblDañadoPicking = New System.Windows.Forms.LinkLabel()
        Me.txtNombreUbicacionMerma = New DevExpress.XtraEditors.TextEdit()
        Me.lnkUbicacionMerma = New System.Windows.Forms.LinkLabel()
        Me.txtIdUbicacionMerma = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreUbicacionDespacho = New DevExpress.XtraEditors.TextEdit()
        Me.lnkUbicacionDespacho = New System.Windows.Forms.LinkLabel()
        Me.txtIdUbicacionDespacho = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreUbicacionPicking = New DevExpress.XtraEditors.TextEdit()
        Me.lnkUbicacionPicking = New System.Windows.Forms.LinkLabel()
        Me.txtIdUbicacionPicking = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreUbicacionRecepcion = New DevExpress.XtraEditors.TextEdit()
        Me.lnkUbicacionRecepcion = New System.Windows.Forms.LinkLabel()
        Me.txtIdUbicacionRecepcion = New DevExpress.XtraEditors.TextEdit()
        Me.tabListaUbicaciones = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.dgridUbicaciones = New DevExpress.XtraGrid.GridControl()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.dkBodega = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.chkUbicImplosionAuto = New DevExpress.XtraEditors.CheckEdit()
        User_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        Label15 = New System.Windows.Forms.Label()
        Label16 = New System.Windows.Forms.Label()
        Label17 = New System.Windows.Forms.Label()
        Label18 = New System.Windows.Forms.Label()
        Label20 = New System.Windows.Forms.Label()
        Label21 = New System.Windows.Forms.Label()
        lblCodigoSectorReferencia = New System.Windows.Forms.Label()
        Label26 = New System.Windows.Forms.Label()
        Label37 = New System.Windows.Forms.Label()
        Label38 = New System.Windows.Forms.Label()
        Label39 = New System.Windows.Forms.Label()
        Label40 = New System.Windows.Forms.Label()
        Label41 = New System.Windows.Forms.Label()
        Label42 = New System.Windows.Forms.Label()
        Label43 = New System.Windows.Forms.Label()
        Label45 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        lblAnchoSector = New System.Windows.Forms.Label()
        LlblLargoSector = New System.Windows.Forms.Label()
        lblAltoSector = New System.Windows.Forms.Label()
        lblDescripcionSector = New System.Windows.Forms.Label()
        lblAreaSector = New System.Windows.Forms.Label()
        lblCodigoSector = New System.Windows.Forms.Label()
        lblMargenIzquierdoSector = New System.Windows.Forms.Label()
        lblMargenDerechoSector = New System.Windows.Forms.Label()
        lblMargenInferiorSector = New System.Windows.Forms.Label()
        lblMargenSuperiorSector = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        lblDescripcionAreaBodega = New System.Windows.Forms.Label()
        lblCodigoArea = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        IdEmpresaLabel = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        IdBodegaLabel = New System.Windows.Forms.Label()
        NombreLabel = New System.Windows.Forms.Label()
        DireccionLabel = New System.Windows.Forms.Label()
        TelefonoLabel = New System.Windows.Forms.Label()
        EmailLabel = New System.Windows.Forms.Label()
        EncargadoLabel = New System.Windows.Forms.Label()
        ActivoLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label30 = New System.Windows.Forms.Label()
        Label28 = New System.Windows.Forms.Label()
        lblCodigoUbicacionVirtual = New System.Windows.Forms.Label()
        Label24 = New System.Windows.Forms.Label()
        Label44 = New System.Windows.Forms.Label()
        Label46 = New System.Windows.Forms.Label()
        Label50 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label13 = New System.Windows.Forms.Label()
        lblAlto = New System.Windows.Forms.Label()
        lblLargo = New System.Windows.Forms.Label()
        lblAncho = New System.Windows.Forms.Label()
        lblLatitud = New System.Windows.Forms.Label()
        lblLongitud = New System.Windows.Forms.Label()
        Label31 = New System.Windows.Forms.Label()
        Label22 = New System.Windows.Forms.Label()
        Label19 = New System.Windows.Forms.Label()
        lblCodigoBodegaERP = New System.Windows.Forms.Label()
        lblUbicCodigoBodegaERP = New System.Windows.Forms.Label()
        Label48 = New System.Windows.Forms.Label()
        Label49 = New System.Windows.Forms.Label()
        lblEsBodegaFiscal = New System.Windows.Forms.Label()
        lblHabilitarIngresoConsolidado = New System.Windows.Forms.Label()
        Label51 = New System.Windows.Forms.Label()
        lblCapturaPalletNoEstandar = New System.Windows.Forms.Label()
        lblCapturaEstibaIngreso = New System.Windows.Forms.Label()
        Label52 = New System.Windows.Forms.Label()
        lblControlBanderasClientePedido = New System.Windows.Forms.Label()
        lblGrupo = New System.Windows.Forms.Label()
        lblIdArea = New System.Windows.Forms.Label()
        Label53 = New System.Windows.Forms.Label()
        lblIdTramo = New System.Windows.Forms.Label()
        lblPriorizar = New System.Windows.Forms.Label()
        Label54 = New System.Windows.Forms.Label()
        lblPermitirEliminarDocumentoSalida = New System.Windows.Forms.Label()
        lblMostrarZonaEnHH = New System.Windows.Forms.Label()
        lblControlOperadorPorUbicacion = New System.Windows.Forms.Label()
        lblEscanearCodigoProductoEnPicking = New System.Windows.Forms.Label()
        lblinferir_origen_en_cambio_ubic = New System.Windows.Forms.Label()
        lblEliminarDocumentosSalida = New System.Windows.Forms.Label()
        lblPickeadorVerifica = New System.Windows.Forms.Label()
        lblPermitirCambioDeUbicacionEnPicking = New System.Windows.Forms.Label()
        Label58 = New System.Windows.Forms.Label()
        Label59 = New System.Windows.Forms.Label()
        lblrestringir_vencimiento_en_reemplazo = New System.Windows.Forms.Label()
        lblrestringir_lote_en_reemplazo = New System.Windows.Forms.Label()
        Label64 = New System.Windows.Forms.Label()
        lblPermitirRepeticionesEnIngreso = New System.Windows.Forms.Label()
        Label66 = New System.Windows.Forms.Label()
        Label67 = New System.Windows.Forms.Label()
        lblcalcular_ubicacion_sugerida_ml = New System.Windows.Forms.Label()
        Label68 = New System.Windows.Forms.Label()
        Label69 = New System.Windows.Forms.Label()
        Label70 = New System.Windows.Forms.Label()
        Label71 = New System.Windows.Forms.Label()
        Label72 = New System.Windows.Forms.Label()
        lblPermitirReemplazoPickingMismaLIcencia = New System.Windows.Forms.Label()
        Label75 = New System.Windows.Forms.Label()
        Label76 = New System.Windows.Forms.Label()
        lblSimbologia = New System.Windows.Forms.Label()
        Label77 = New System.Windows.Forms.Label()
        Label78 = New System.Windows.Forms.Label()
        Label79 = New System.Windows.Forms.Label()
        lbldespachoauto = New System.Windows.Forms.Label()
        lblLimpiarCampos = New System.Windows.Forms.Label()
        lblControlTallaColor = New System.Windows.Forms.Label()
        Label83 = New System.Windows.Forms.Label()
        Label84 = New System.Windows.Forms.Label()
        Label85 = New System.Windows.Forms.Label()
        Label88 = New System.Windows.Forms.Label()
        Label89 = New System.Windows.Forms.Label()
        Label90 = New System.Windows.Forms.Label()
        Label91 = New System.Windows.Forms.Label()
        Label92 = New System.Windows.Forms.Label()
        Label93 = New System.Windows.Forms.Label()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl12, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl12.SuspendLayout()
        CType(Me.txtIdTramo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTipoRack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOrient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mnu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIndice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEsRack.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbFont.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSector.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbAreasR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivoTramo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoTramo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSistemaTramo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescripcionTramo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl11.SuspendLayout()
        CType(Me.nUpdMargenInferiorTramo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdMargenSuperiorTramo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdMargenDerechoTramo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdMargenIzquierdoTramo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdAnchoTramo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdLargoTramo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdAltoTramo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl10.SuspendLayout()
        CType(Me.grdTramo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewTramo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTramosActivos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl14, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl14.SuspendLayout()
        CType(Me.chkUbicacionMuelle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkUbicPrdNE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkUbicacionesActivas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbOrientacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEsBodegaVirtual.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdMargenInferiorUbicacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdMargenSuperiorUbicacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdMargenDerechoUbicacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMerma.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdMargenIzquierdoUbicacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdAnchoUbicacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdLargoUbicacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdAltoUbicacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDespacho.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDañadoUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkUbicacionPicking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivoUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAceptaPalletUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRecepcion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBloqueadaUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSistemaUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl15, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl15.SuspendLayout()
        CType(Me.grdUbicacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewUbi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl16, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl16.SuspendLayout()
        CType(Me.tlUbicaciones, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Dgrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtTiempoActualizacionP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreTarea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdTarea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsBodega, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BodegaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ControlPanelBodega, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ControlPanelBodega.SuspendLayout()
        Me.tabDatos.SuspendLayout()
        CType(Me.grpDatosGen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDatosGen.SuspendLayout()
        Me.XtraScrollableControl.SuspendLayout()
        CType(Me.gpSmtp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpSmtp.SuspendLayout()
        CType(Me.txtPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUsuario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPuerto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtServidor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSsl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcCentroCosto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcCentroCosto.SuspendLayout()
        CType(Me.cmbCentroCostoDepERP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCentroCostoDirERP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCentroCostoERP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRutaCDN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpTIpoTransaccion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTIpoTransaccion.SuspendLayout()
        CType(Me.txtNombreDocumentoSalida.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdTipoDocumentoSalida.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescripcionTR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdTipoTR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTamañoEtiquetaUbicacionDefecto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoBodegaERP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPais.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EncargadoTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmailTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TelefonoTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DireccionTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreComercial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoBarra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabDimensionesBod.SuspendLayout()
        CType(Me.GroupControl18, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl18.SuspendLayout()
        CType(Me.txtZoom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAlto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCoordenadaY.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLargo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCoordenadaX.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAncho, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabArea.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.txtNombreUbicacionRecepcionArea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUbicacionRecepcionArea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGrupoArea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdArea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivoAreaBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoAreaBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSistemaAreaBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescripcionAreaBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6.SuspendLayout()
        CType(Me.nUpdMargenInferior, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdMargenSuperior, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdMargenDerecho, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdMargenIzquierdo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdAlto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdAncho, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdLargo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripPR.SuspendLayout()
        CType(Me.GroupControl17, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl17.SuspendLayout()
        CType(Me.grdAreaBodega, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAreasBodegaActivos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabSector.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl8.SuspendLayout()
        CType(Me.txtIdSector.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbArea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivoSector.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoSector.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdAnchoSector, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSistemaSector.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdLargoSector, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescripcionSector.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdAltoSector, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl7.SuspendLayout()
        CType(Me.txtPosY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPosX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdMargenInferiorSector, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdMargenSuperiorSector, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdMargenDerechoSector, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdMargenIzquierdoSector, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl9.SuspendLayout()
        CType(Me.grdSectorArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewSec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSectoresActivos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.tabTramo.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        Me.TabUbicacion.SuspendLayout()
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.GroupControl13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl13.SuspendLayout()
        CType(Me.txtUbicCodigoBodegaERP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTramo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSectorR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbAreaUbic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbIndiceRotacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTipoRotacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIndiceX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUpdNivelUbicacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoBarra2ubicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoBarraUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescripcionUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip3.SuspendLayout()
        Me.tabReferencia.SuspendLayout()
        Me.tabParametros.SuspendLayout()
        Me.tabUbicacionesDefecto.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.chkControlGondola.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudRangoDiasDocumentos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkControlTallaColor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkrestringir_vencimiento_en_reemplazo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkrestringir_lote_en_reemplazo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLberarStockDepachosParciales.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkHomologarLoteConFechaVence.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkValidarExistenciasEnCargaInventarioInicial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkControlPalletsMixtos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkControlOperadorUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkinferir_origen_en_cambio_ubic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkValidarDisponibilidadEnUbicacionDestino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.chkBodegaClienteAjusteByB.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkreemplazoOpcional.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkImprimir_Verificacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAdvertirMpqUmbas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAgrupar_sin_lic_veri_no_cons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVerificacion_Consolidada.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkControlBanderasCliente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDespacharProductoVencido.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermitirReemplazoVerificacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermitir_Verificacion_Consolidada.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiasMaximoVencimientoReemplazo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkdespachoautohh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermitirEliminarDocumentosSalida.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEliminarDocumentosSalida.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFiltrarPedidosUsuario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCambioUbicacionRestrictivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermitirCambioUbicIndiceMenor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRequerirMismoProductoPosiciones.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.chkCambioUbiAuto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkControlTarifaServ.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEsBodegaFiscal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLimpiarCamposHH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkNotificacionVoz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEsMotriz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRestringirAreasSAP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMostrarAreaEnHH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInterface_SAP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkcalcular_ubicacion_sugerida_ml.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermitirDecimales.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.chkControlGuia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOperadorPickingVerifica.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermitirNoEncontradoPicking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermitirReemplazoPickingMismaLIcencia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEscanearLicenciaPicking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermitirReemplazoPicking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkpermitir_buen_estado_en_reemplazo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEscanearCodigoProductoEnPicking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermitirCambioUbicacionPicking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOrdenarPickingDescendente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOrdenarNombreCompleto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbEstadoDefectoRack.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermitirCambioUbicacionRecepcion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBloquearLpHH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIngresoConsolidado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPriorizar_UbicRec_Sobre_UbicEst.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCapturaPalletNoEstandar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCapturaEstibaIngreso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermitirRepeticionesEnIngreso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.cmbEtiquetaVerificacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSymbology.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEtiqueta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtHorarioEjecucionHistorico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdDiasLimiteRetroactivo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudTopReabastecimientoManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdConfiguracionPantallaRecepcion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdConfiguracionPantallaVerificacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdConfiguracionPantallaPicking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtValorIVA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdMotivoUbicReabasto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMotivoUbicReabasto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEstadoNe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUbicacionPrdNE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreUbicNE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtidmotivoubicaciondañadopicking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMotivoUbicacionDañadoPicking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreUbicacionMerma.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUbicacionMerma.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreUbicacionDespacho.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUbicacionDespacho.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreUbicacionPicking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUbicacionPicking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreUbicacionRecepcion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUbicacionRecepcion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabListaUbicaciones.SuspendLayout()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        CType(Me.dgridUbicaciones, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkBodega, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkUbicImplosionAuto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(97, 18)
        User_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(100, 16)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(580, 20)
        User_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 2
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(314, 18)
        Fec_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 1
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(797, 20)
        Fec_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 3
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'Label15
        '
        Label15.AutoSize = True
        Label15.Location = New System.Drawing.Point(31, 118)
        Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label15.Name = "Label15"
        Label15.Size = New System.Drawing.Size(49, 16)
        Label15.TabIndex = 3
        Label15.Text = "Sector:"
        '
        'Label16
        '
        Label16.AutoSize = True
        Label16.Location = New System.Drawing.Point(385, 135)
        Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label16.Name = "Label16"
        Label16.Size = New System.Drawing.Size(108, 16)
        Label16.TabIndex = 8
        Label16.Text = "Margen Superior:"
        '
        'Label17
        '
        Label17.AutoSize = True
        Label17.Location = New System.Drawing.Point(385, 175)
        Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label17.Name = "Label17"
        Label17.Size = New System.Drawing.Size(101, 16)
        Label17.TabIndex = 12
        Label17.Text = "Margen Inferior:"
        '
        'Label18
        '
        Label18.AutoSize = True
        Label18.Location = New System.Drawing.Point(385, 92)
        Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label18.Name = "Label18"
        Label18.Size = New System.Drawing.Size(106, 16)
        Label18.TabIndex = 4
        Label18.Text = "Margen Derecho:"
        '
        'Label20
        '
        Label20.AutoSize = True
        Label20.Location = New System.Drawing.Point(31, 142)
        Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label20.Name = "Label20"
        Label20.Size = New System.Drawing.Size(47, 16)
        Label20.TabIndex = 9
        Label20.Text = "Ancho:"
        '
        'Label21
        '
        Label21.AutoSize = True
        Label21.Location = New System.Drawing.Point(31, 100)
        Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label21.Name = "Label21"
        Label21.Size = New System.Drawing.Size(44, 16)
        Label21.TabIndex = 5
        Label21.Text = "Largo:"
        '
        'lblCodigoSectorReferencia
        '
        lblCodigoSectorReferencia.AutoSize = True
        lblCodigoSectorReferencia.Location = New System.Drawing.Point(31, 153)
        lblCodigoSectorReferencia.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCodigoSectorReferencia.Name = "lblCodigoSectorReferencia"
        lblCodigoSectorReferencia.Size = New System.Drawing.Size(51, 16)
        lblCodigoSectorReferencia.TabIndex = 6
        lblCodigoSectorReferencia.Text = "Código:"
        '
        'Label26
        '
        Label26.AutoSize = True
        Label26.Location = New System.Drawing.Point(31, 185)
        Label26.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label26.Name = "Label26"
        Label26.Size = New System.Drawing.Size(77, 16)
        Label26.TabIndex = 9
        Label26.Text = "Descripción:"
        '
        'Label37
        '
        Label37.AutoSize = True
        Label37.Location = New System.Drawing.Point(345, 107)
        Label37.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label37.Name = "Label37"
        Label37.Size = New System.Drawing.Size(108, 16)
        Label37.TabIndex = 8
        Label37.Text = "Margen Superior:"
        '
        'Label38
        '
        Label38.AutoSize = True
        Label38.Location = New System.Drawing.Point(345, 135)
        Label38.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label38.Name = "Label38"
        Label38.Size = New System.Drawing.Size(101, 16)
        Label38.TabIndex = 12
        Label38.Text = "Margen Inferior:"
        '
        'Label39
        '
        Label39.AutoSize = True
        Label39.Location = New System.Drawing.Point(345, 77)
        Label39.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label39.Name = "Label39"
        Label39.Size = New System.Drawing.Size(106, 16)
        Label39.TabIndex = 4
        Label39.Text = "Margen Derecho:"
        '
        'Label40
        '
        Label40.AutoSize = True
        Label40.Location = New System.Drawing.Point(345, 48)
        Label40.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label40.Name = "Label40"
        Label40.Size = New System.Drawing.Size(112, 16)
        Label40.TabIndex = 0
        Label40.Text = "Margen Izquierdo:"
        '
        'Label41
        '
        Label41.AutoSize = True
        Label41.Location = New System.Drawing.Point(25, 117)
        Label41.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label41.Name = "Label41"
        Label41.Size = New System.Drawing.Size(47, 16)
        Label41.TabIndex = 9
        Label41.Text = "Ancho:"
        '
        'Label42
        '
        Label42.AutoSize = True
        Label42.Location = New System.Drawing.Point(25, 85)
        Label42.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label42.Name = "Label42"
        Label42.Size = New System.Drawing.Size(44, 16)
        Label42.TabIndex = 5
        Label42.Text = "Largo:"
        '
        'Label43
        '
        Label43.AutoSize = True
        Label43.Location = New System.Drawing.Point(25, 53)
        Label43.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label43.Name = "Label43"
        Label43.Size = New System.Drawing.Size(34, 16)
        Label43.TabIndex = 1
        Label43.Text = "Alto:"
        '
        'Label45
        '
        Label45.AutoSize = True
        Label45.Location = New System.Drawing.Point(31, 86)
        Label45.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label45.Name = "Label45"
        Label45.Size = New System.Drawing.Size(39, 16)
        Label45.TabIndex = 0
        Label45.Text = "Area:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(31, 102)
        Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(120, 16)
        Label5.TabIndex = 3
        Label5.Text = "Actualizacion (seg):"
        '
        'lblAnchoSector
        '
        lblAnchoSector.AutoSize = True
        lblAnchoSector.Location = New System.Drawing.Point(474, 144)
        lblAnchoSector.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblAnchoSector.Name = "lblAnchoSector"
        lblAnchoSector.Size = New System.Drawing.Size(63, 16)
        lblAnchoSector.TabIndex = 10
        lblAnchoSector.Text = "Profundo:"
        '
        'LlblLargoSector
        '
        LlblLargoSector.AutoSize = True
        LlblLargoSector.Location = New System.Drawing.Point(474, 112)
        LlblLargoSector.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        LlblLargoSector.Name = "LlblLargoSector"
        LlblLargoSector.Size = New System.Drawing.Size(44, 16)
        LlblLargoSector.TabIndex = 8
        LlblLargoSector.Text = "Largo:"
        '
        'lblAltoSector
        '
        lblAltoSector.AutoSize = True
        lblAltoSector.Location = New System.Drawing.Point(474, 78)
        lblAltoSector.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblAltoSector.Name = "lblAltoSector"
        lblAltoSector.Size = New System.Drawing.Size(34, 16)
        lblAltoSector.TabIndex = 6
        lblAltoSector.Text = "Alto:"
        '
        'lblDescripcionSector
        '
        lblDescripcionSector.AutoSize = True
        lblDescripcionSector.Location = New System.Drawing.Point(34, 144)
        lblDescripcionSector.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblDescripcionSector.Name = "lblDescripcionSector"
        lblDescripcionSector.Size = New System.Drawing.Size(77, 16)
        lblDescripcionSector.TabIndex = 4
        lblDescripcionSector.Text = "Descripción:"
        '
        'lblAreaSector
        '
        lblAreaSector.AutoSize = True
        lblAreaSector.Location = New System.Drawing.Point(34, 78)
        lblAreaSector.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblAreaSector.Name = "lblAreaSector"
        lblAreaSector.Size = New System.Drawing.Size(39, 16)
        lblAreaSector.TabIndex = 0
        lblAreaSector.Text = "Area:"
        '
        'lblCodigoSector
        '
        lblCodigoSector.AutoSize = True
        lblCodigoSector.Location = New System.Drawing.Point(34, 112)
        lblCodigoSector.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCodigoSector.Name = "lblCodigoSector"
        lblCodigoSector.Size = New System.Drawing.Size(51, 16)
        lblCodigoSector.TabIndex = 2
        lblCodigoSector.Text = "Código:"
        '
        'lblMargenIzquierdoSector
        '
        lblMargenIzquierdoSector.AutoSize = True
        lblMargenIzquierdoSector.Location = New System.Drawing.Point(407, 77)
        lblMargenIzquierdoSector.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblMargenIzquierdoSector.Name = "lblMargenIzquierdoSector"
        lblMargenIzquierdoSector.Size = New System.Drawing.Size(112, 16)
        lblMargenIzquierdoSector.TabIndex = 3
        lblMargenIzquierdoSector.Text = "Margen Izquierdo:"
        '
        'lblMargenDerechoSector
        '
        lblMargenDerechoSector.AutoSize = True
        lblMargenDerechoSector.Location = New System.Drawing.Point(407, 110)
        lblMargenDerechoSector.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblMargenDerechoSector.Name = "lblMargenDerechoSector"
        lblMargenDerechoSector.Size = New System.Drawing.Size(106, 16)
        lblMargenDerechoSector.TabIndex = 7
        lblMargenDerechoSector.Text = "Margen Derecho:"
        '
        'lblMargenInferiorSector
        '
        lblMargenInferiorSector.AutoSize = True
        lblMargenInferiorSector.Location = New System.Drawing.Point(407, 173)
        lblMargenInferiorSector.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblMargenInferiorSector.Name = "lblMargenInferiorSector"
        lblMargenInferiorSector.Size = New System.Drawing.Size(101, 16)
        lblMargenInferiorSector.TabIndex = 12
        lblMargenInferiorSector.Text = "Margen Inferior:"
        '
        'lblMargenSuperiorSector
        '
        lblMargenSuperiorSector.AutoSize = True
        lblMargenSuperiorSector.Location = New System.Drawing.Point(407, 143)
        lblMargenSuperiorSector.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblMargenSuperiorSector.Name = "lblMargenSuperiorSector"
        lblMargenSuperiorSector.Size = New System.Drawing.Size(108, 16)
        lblMargenSuperiorSector.TabIndex = 8
        lblMargenSuperiorSector.Text = "Margen Superior:"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(12, 136)
        Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(47, 16)
        Label8.TabIndex = 8
        Label8.Text = "Ancho:"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(12, 106)
        Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(44, 16)
        Label7.TabIndex = 6
        Label7.Text = "Largo:"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(12, 78)
        Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(34, 16)
        Label6.TabIndex = 4
        Label6.Text = "Alto:"
        '
        'lblDescripcionAreaBodega
        '
        lblDescripcionAreaBodega.AutoSize = True
        lblDescripcionAreaBodega.Location = New System.Drawing.Point(31, 116)
        lblDescripcionAreaBodega.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblDescripcionAreaBodega.Name = "lblDescripcionAreaBodega"
        lblDescripcionAreaBodega.Size = New System.Drawing.Size(77, 16)
        lblDescripcionAreaBodega.TabIndex = 2
        lblDescripcionAreaBodega.Text = "Descripción:"
        '
        'lblCodigoArea
        '
        lblCodigoArea.AutoSize = True
        lblCodigoArea.Location = New System.Drawing.Point(31, 83)
        lblCodigoArea.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCodigoArea.Name = "lblCodigoArea"
        lblCodigoArea.Size = New System.Drawing.Size(51, 16)
        lblCodigoArea.TabIndex = 0
        lblCodigoArea.Text = "Código:"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(365, 73)
        Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(112, 16)
        Label9.TabIndex = 0
        Label9.Text = "Margen Izquierdo:"
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(365, 108)
        Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(106, 16)
        Label10.TabIndex = 2
        Label10.Text = "Margen Derecho:"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(365, 172)
        Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(101, 16)
        Label12.TabIndex = 6
        Label12.Text = "Margen Inferior:"
        '
        'Label11
        '
        Label11.AutoSize = True
        Label11.Location = New System.Drawing.Point(365, 139)
        Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(108, 16)
        Label11.TabIndex = 4
        Label11.Text = "Margen Superior:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(15, 128)
        Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(109, 16)
        Label2.TabIndex = 6
        Label2.Text = "Código de Barrra:"
        '
        'IdEmpresaLabel
        '
        IdEmpresaLabel.AutoSize = True
        IdEmpresaLabel.Location = New System.Drawing.Point(15, 64)
        IdEmpresaLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        IdEmpresaLabel.Name = "IdEmpresaLabel"
        IdEmpresaLabel.Size = New System.Drawing.Size(62, 16)
        IdEmpresaLabel.TabIndex = 2
        IdEmpresaLabel.Text = "Empresa:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(15, 224)
        Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(118, 16)
        Label3.TabIndex = 10
        Label3.Text = "Nombre Comercial:"
        '
        'IdBodegaLabel
        '
        IdBodegaLabel.AutoSize = True
        IdBodegaLabel.Location = New System.Drawing.Point(15, 96)
        IdBodegaLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        IdBodegaLabel.Name = "IdBodegaLabel"
        IdBodegaLabel.Size = New System.Drawing.Size(115, 16)
        IdBodegaLabel.TabIndex = 4
        IdBodegaLabel.Text = "Código de Bodega:"
        '
        'NombreLabel
        '
        NombreLabel.AutoSize = True
        NombreLabel.Location = New System.Drawing.Point(15, 192)
        NombreLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        NombreLabel.Name = "NombreLabel"
        NombreLabel.Size = New System.Drawing.Size(57, 16)
        NombreLabel.TabIndex = 8
        NombreLabel.Text = "Nombre:"
        '
        'DireccionLabel
        '
        DireccionLabel.AutoSize = True
        DireccionLabel.Location = New System.Drawing.Point(15, 256)
        DireccionLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        DireccionLabel.Name = "DireccionLabel"
        DireccionLabel.Size = New System.Drawing.Size(64, 16)
        DireccionLabel.TabIndex = 12
        DireccionLabel.Text = "Dirección:"
        '
        'TelefonoLabel
        '
        TelefonoLabel.AutoSize = True
        TelefonoLabel.Location = New System.Drawing.Point(15, 288)
        TelefonoLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        TelefonoLabel.Name = "TelefonoLabel"
        TelefonoLabel.Size = New System.Drawing.Size(62, 16)
        TelefonoLabel.TabIndex = 14
        TelefonoLabel.Text = "Teléfono:"
        '
        'EmailLabel
        '
        EmailLabel.AutoSize = True
        EmailLabel.Location = New System.Drawing.Point(15, 320)
        EmailLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        EmailLabel.Name = "EmailLabel"
        EmailLabel.Size = New System.Drawing.Size(43, 16)
        EmailLabel.TabIndex = 16
        EmailLabel.Text = "Email:"
        '
        'EncargadoLabel
        '
        EncargadoLabel.AutoSize = True
        EncargadoLabel.Location = New System.Drawing.Point(15, 352)
        EncargadoLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        EncargadoLabel.Name = "EncargadoLabel"
        EncargadoLabel.Size = New System.Drawing.Size(72, 16)
        EncargadoLabel.TabIndex = 18
        EncargadoLabel.Text = "Encargado:"
        '
        'ActivoLabel
        '
        ActivoLabel.AutoSize = True
        ActivoLabel.Location = New System.Drawing.Point(15, 418)
        ActivoLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        ActivoLabel.Name = "ActivoLabel"
        ActivoLabel.Size = New System.Drawing.Size(46, 16)
        ActivoLabel.TabIndex = 20
        ActivoLabel.Text = "Activo:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(15, 31)
        Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(35, 16)
        Label1.TabIndex = 0
        Label1.Text = "Pais:"
        '
        'Label30
        '
        Label30.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label30.AutoSize = True
        Label30.Location = New System.Drawing.Point(9, 245)
        Label30.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label30.Name = "Label30"
        Label30.Size = New System.Drawing.Size(77, 16)
        Label30.TabIndex = 14
        Label30.Text = "Descripción:"
        '
        'Label28
        '
        Label28.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label28.AutoSize = True
        Label28.Location = New System.Drawing.Point(9, 187)
        Label28.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label28.Name = "Label28"
        Label28.Size = New System.Drawing.Size(86, 16)
        Label28.TabIndex = 10
        Label28.Text = "Código Barra:"
        '
        'lblCodigoUbicacionVirtual
        '
        lblCodigoUbicacionVirtual.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCodigoUbicacionVirtual.AutoSize = True
        lblCodigoUbicacionVirtual.Location = New System.Drawing.Point(9, 216)
        lblCodigoUbicacionVirtual.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCodigoUbicacionVirtual.Name = "lblCodigoUbicacionVirtual"
        lblCodigoUbicacionVirtual.Size = New System.Drawing.Size(107, 16)
        lblCodigoUbicacionVirtual.TabIndex = 12
        lblCodigoUbicacionVirtual.Text = "Código Ubic. Virt:"
        '
        'Label24
        '
        Label24.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label24.AutoSize = True
        Label24.Location = New System.Drawing.Point(9, 100)
        Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label24.Name = "Label24"
        Label24.Size = New System.Drawing.Size(50, 16)
        Label24.TabIndex = 4
        Label24.Text = "Tramo:"
        '
        'Label44
        '
        Label44.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label44.AutoSize = True
        Label44.Location = New System.Drawing.Point(9, 274)
        Label44.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label44.Name = "Label44"
        Label44.Size = New System.Drawing.Size(60, 16)
        Label44.TabIndex = 16
        Label44.Text = "Nivel (Y):"
        '
        'Label46
        '
        Label46.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label46.AutoSize = True
        Label46.Location = New System.Drawing.Point(9, 71)
        Label46.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label46.Name = "Label46"
        Label46.Size = New System.Drawing.Size(49, 16)
        Label46.TabIndex = 2
        Label46.Text = "Sector:"
        '
        'Label50
        '
        Label50.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label50.AutoSize = True
        Label50.Location = New System.Drawing.Point(9, 42)
        Label50.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label50.Name = "Label50"
        Label50.Size = New System.Drawing.Size(39, 16)
        Label50.TabIndex = 0
        Label50.Text = "Area:"
        '
        'Label4
        '
        Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(9, 129)
        Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(90, 16)
        Label4.TabIndex = 6
        Label4.Text = "Tipo Rotación:"
        '
        'Label13
        '
        Label13.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label13.Location = New System.Drawing.Point(9, 303)
        Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label13.Name = "Label13"
        Label13.Size = New System.Drawing.Size(136, 20)
        Label13.TabIndex = 18
        Label13.Text = "Columna (X):"
        '
        'lblAlto
        '
        lblAlto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblAlto.AutoSize = True
        lblAlto.Location = New System.Drawing.Point(41, 161)
        lblAlto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblAlto.Name = "lblAlto"
        lblAlto.Size = New System.Drawing.Size(34, 16)
        lblAlto.TabIndex = 6
        lblAlto.Text = "Alto:"
        '
        'lblLargo
        '
        lblLargo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblLargo.AutoSize = True
        lblLargo.Location = New System.Drawing.Point(41, 62)
        lblLargo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblLargo.Name = "lblLargo"
        lblLargo.Size = New System.Drawing.Size(44, 16)
        lblLargo.TabIndex = 0
        lblLargo.Text = "Largo:"
        '
        'lblAncho
        '
        lblAncho.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblAncho.AutoSize = True
        lblAncho.Location = New System.Drawing.Point(41, 95)
        lblAncho.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblAncho.Name = "lblAncho"
        lblAncho.Size = New System.Drawing.Size(47, 16)
        lblAncho.TabIndex = 2
        lblAncho.Text = "Ancho:"
        '
        'lblLatitud
        '
        lblLatitud.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblLatitud.AutoSize = True
        lblLatitud.Location = New System.Drawing.Point(41, 196)
        lblLatitud.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblLatitud.Name = "lblLatitud"
        lblLatitud.Size = New System.Drawing.Size(45, 16)
        lblLatitud.TabIndex = 8
        lblLatitud.Text = "Latitud"
        '
        'lblLongitud
        '
        lblLongitud.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblLongitud.AutoSize = True
        lblLongitud.Location = New System.Drawing.Point(41, 228)
        lblLongitud.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblLongitud.Name = "lblLongitud"
        lblLongitud.Size = New System.Drawing.Size(55, 16)
        lblLongitud.TabIndex = 10
        lblLongitud.Text = "Longitud"
        '
        'Label31
        '
        Label31.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label31.AutoSize = True
        Label31.Location = New System.Drawing.Point(7, 31)
        Label31.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label31.Name = "Label31"
        Label31.Size = New System.Drawing.Size(179, 16)
        Label31.TabIndex = 22
        Label31.Text = "Cambio ubicación automático:"
        '
        'Label22
        '
        Label22.AutoSize = True
        Label22.Location = New System.Drawing.Point(31, 55)
        Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label22.Name = "Label22"
        Label22.Size = New System.Drawing.Size(34, 16)
        Label22.TabIndex = 3
        Label22.Text = "Alto:"
        '
        'Label19
        '
        Label19.AutoSize = True
        Label19.Location = New System.Drawing.Point(385, 49)
        Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label19.Name = "Label19"
        Label19.Size = New System.Drawing.Size(112, 16)
        Label19.TabIndex = 2
        Label19.Text = "Margen Izquierdo:"
        '
        'lblCodigoBodegaERP
        '
        lblCodigoBodegaERP.AutoSize = True
        lblCodigoBodegaERP.Location = New System.Drawing.Point(15, 160)
        lblCodigoBodegaERP.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCodigoBodegaERP.Name = "lblCodigoBodegaERP"
        lblCodigoBodegaERP.Size = New System.Drawing.Size(123, 16)
        lblCodigoBodegaERP.TabIndex = 26
        lblCodigoBodegaERP.Text = "Código bodega ERP:"
        '
        'lblUbicCodigoBodegaERP
        '
        lblUbicCodigoBodegaERP.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblUbicCodigoBodegaERP.AutoSize = True
        lblUbicCodigoBodegaERP.Location = New System.Drawing.Point(9, 336)
        lblUbicCodigoBodegaERP.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblUbicCodigoBodegaERP.Name = "lblUbicCodigoBodegaERP"
        lblUbicCodigoBodegaERP.Size = New System.Drawing.Size(80, 16)
        lblUbicCodigoBodegaERP.TabIndex = 20
        lblUbicCodigoBodegaERP.Text = "Bodega ERP:"
        '
        'Label48
        '
        Label48.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label48.AutoSize = True
        Label48.Location = New System.Drawing.Point(10, 62)
        Label48.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label48.Name = "Label48"
        Label48.Size = New System.Drawing.Size(93, 16)
        Label48.TabIndex = 28
        Label48.Text = "Noficación voz:"
        '
        'Label49
        '
        Label49.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label49.AutoSize = True
        Label49.Location = New System.Drawing.Point(7, 90)
        Label49.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label49.Name = "Label49"
        Label49.Size = New System.Drawing.Size(140, 16)
        Label49.TabIndex = 30
        Label49.Text = "Control tarifa servicios:"
        '
        'lblEsBodegaFiscal
        '
        lblEsBodegaFiscal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblEsBodegaFiscal.AutoSize = True
        lblEsBodegaFiscal.Location = New System.Drawing.Point(7, 119)
        lblEsBodegaFiscal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblEsBodegaFiscal.Name = "lblEsBodegaFiscal"
        lblEsBodegaFiscal.Size = New System.Drawing.Size(104, 16)
        lblEsBodegaFiscal.TabIndex = 32
        lblEsBodegaFiscal.Text = "Es bodega fiscal:"
        '
        'lblHabilitarIngresoConsolidado
        '
        lblHabilitarIngresoConsolidado.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblHabilitarIngresoConsolidado.AutoSize = True
        lblHabilitarIngresoConsolidado.Location = New System.Drawing.Point(10, 62)
        lblHabilitarIngresoConsolidado.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblHabilitarIngresoConsolidado.Name = "lblHabilitarIngresoConsolidado"
        lblHabilitarIngresoConsolidado.Size = New System.Drawing.Size(176, 16)
        lblHabilitarIngresoConsolidado.TabIndex = 34
        lblHabilitarIngresoConsolidado.Text = "Habilitar ingreso consolidado:"
        '
        'Label51
        '
        Label51.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label51.AutoSize = True
        Label51.Location = New System.Drawing.Point(10, 32)
        Label51.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label51.Name = "Label51"
        Label51.Size = New System.Drawing.Size(117, 16)
        Label51.TabIndex = 36
        Label51.Text = "Bloquear LP en HH:"
        '
        'lblCapturaPalletNoEstandar
        '
        lblCapturaPalletNoEstandar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCapturaPalletNoEstandar.AutoSize = True
        lblCapturaPalletNoEstandar.Location = New System.Drawing.Point(10, 94)
        lblCapturaPalletNoEstandar.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCapturaPalletNoEstandar.Name = "lblCapturaPalletNoEstandar"
        lblCapturaPalletNoEstandar.Size = New System.Drawing.Size(164, 16)
        lblCapturaPalletNoEstandar.TabIndex = 38
        lblCapturaPalletNoEstandar.Text = "Captura pallet no estándar:"
        '
        'lblCapturaEstibaIngreso
        '
        lblCapturaEstibaIngreso.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCapturaEstibaIngreso.AutoSize = True
        lblCapturaEstibaIngreso.Location = New System.Drawing.Point(10, 124)
        lblCapturaEstibaIngreso.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCapturaEstibaIngreso.Name = "lblCapturaEstibaIngreso"
        lblCapturaEstibaIngreso.Size = New System.Drawing.Size(141, 16)
        lblCapturaEstibaIngreso.TabIndex = 39
        lblCapturaEstibaIngreso.Text = "Captura estiba ingreso:"
        '
        'Label52
        '
        Label52.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label52.AutoSize = True
        Label52.Location = New System.Drawing.Point(381, 22)
        Label52.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label52.Name = "Label52"
        Label52.Size = New System.Drawing.Size(200, 16)
        Label52.TabIndex = 42
        Label52.Text = "Permitir Verificacion Consolidada:"
        '
        'lblControlBanderasClientePedido
        '
        lblControlBanderasClientePedido.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblControlBanderasClientePedido.AutoSize = True
        lblControlBanderasClientePedido.Location = New System.Drawing.Point(10, 26)
        lblControlBanderasClientePedido.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblControlBanderasClientePedido.Name = "lblControlBanderasClientePedido"
        lblControlBanderasClientePedido.Size = New System.Drawing.Size(198, 16)
        lblControlBanderasClientePedido.TabIndex = 44
        lblControlBanderasClientePedido.Text = "Control banderas cliente (Pedido)"
        '
        'lblGrupo
        '
        lblGrupo.AutoSize = True
        lblGrupo.Location = New System.Drawing.Point(31, 149)
        lblGrupo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblGrupo.Name = "lblGrupo"
        lblGrupo.Size = New System.Drawing.Size(46, 16)
        lblGrupo.TabIndex = 12
        lblGrupo.Text = "Grupo:"
        '
        'lblIdArea
        '
        lblIdArea.AutoSize = True
        lblIdArea.Location = New System.Drawing.Point(31, 50)
        lblIdArea.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblIdArea.Name = "lblIdArea"
        lblIdArea.Size = New System.Drawing.Size(50, 16)
        lblIdArea.TabIndex = 14
        lblIdArea.Text = "IdArea:"
        '
        'Label53
        '
        Label53.AutoSize = True
        Label53.Location = New System.Drawing.Point(34, 48)
        Label53.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label53.Name = "Label53"
        Label53.Size = New System.Drawing.Size(60, 16)
        Label53.TabIndex = 16
        Label53.Text = "IdSector:"
        '
        'lblIdTramo
        '
        lblIdTramo.AutoSize = True
        lblIdTramo.Location = New System.Drawing.Point(31, 54)
        lblIdTramo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblIdTramo.Name = "lblIdTramo"
        lblIdTramo.Size = New System.Drawing.Size(61, 16)
        lblIdTramo.TabIndex = 19
        lblIdTramo.Text = "IdTramo:"
        '
        'lblPriorizar
        '
        lblPriorizar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblPriorizar.AutoSize = True
        lblPriorizar.Location = New System.Drawing.Point(10, 190)
        lblPriorizar.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblPriorizar.Name = "lblPriorizar"
        lblPriorizar.Size = New System.Drawing.Size(360, 16)
        lblPriorizar.TabIndex = 48
        lblPriorizar.Text = "Priorizar ubicación recepción sobre ubicación estado producto"
        '
        'Label54
        '
        Label54.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label54.AutoSize = True
        Label54.Location = New System.Drawing.Point(7, 190)
        Label54.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label54.Name = "Label54"
        Label54.Size = New System.Drawing.Size(252, 16)
        Label54.TabIndex = 50
        Label54.Text = "Validar disponibilidad en ubicación destino "
        '
        'lblPermitirEliminarDocumentoSalida
        '
        lblPermitirEliminarDocumentoSalida.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblPermitirEliminarDocumentoSalida.AutoSize = True
        lblPermitirEliminarDocumentoSalida.Location = New System.Drawing.Point(10, 113)
        lblPermitirEliminarDocumentoSalida.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblPermitirEliminarDocumentoSalida.Name = "lblPermitirEliminarDocumentoSalida"
        lblPermitirEliminarDocumentoSalida.Size = New System.Drawing.Size(230, 16)
        lblPermitirEliminarDocumentoSalida.TabIndex = 52
        lblPermitirEliminarDocumentoSalida.Text = "Permitir eliminar documentos de salida"
        '
        'lblMostrarZonaEnHH
        '
        lblMostrarZonaEnHH.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblMostrarZonaEnHH.AutoSize = True
        lblMostrarZonaEnHH.Location = New System.Drawing.Point(8, 176)
        lblMostrarZonaEnHH.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblMostrarZonaEnHH.Name = "lblMostrarZonaEnHH"
        lblMostrarZonaEnHH.Size = New System.Drawing.Size(120, 16)
        lblMostrarZonaEnHH.TabIndex = 54
        lblMostrarZonaEnHH.Text = "Mostrar Área en HH"
        '
        'lblControlOperadorPorUbicacion
        '
        lblControlOperadorPorUbicacion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblControlOperadorPorUbicacion.AutoSize = True
        lblControlOperadorPorUbicacion.Location = New System.Drawing.Point(7, 162)
        lblControlOperadorPorUbicacion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblControlOperadorPorUbicacion.Name = "lblControlOperadorPorUbicacion"
        lblControlOperadorPorUbicacion.Size = New System.Drawing.Size(202, 16)
        lblControlOperadorPorUbicacion.TabIndex = 56
        lblControlOperadorPorUbicacion.Text = "Control de operador por ubicación"
        '
        'lblEscanearCodigoProductoEnPicking
        '
        lblEscanearCodigoProductoEnPicking.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblEscanearCodigoProductoEnPicking.AutoSize = True
        lblEscanearCodigoProductoEnPicking.Location = New System.Drawing.Point(12, 198)
        lblEscanearCodigoProductoEnPicking.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblEscanearCodigoProductoEnPicking.Name = "lblEscanearCodigoProductoEnPicking"
        lblEscanearCodigoProductoEnPicking.Size = New System.Drawing.Size(235, 16)
        lblEscanearCodigoProductoEnPicking.TabIndex = 58
        lblEscanearCodigoProductoEnPicking.Text = "Escanear Código de Producto en Picking"
        '
        'lblinferir_origen_en_cambio_ubic
        '
        lblinferir_origen_en_cambio_ubic.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblinferir_origen_en_cambio_ubic.AutoSize = True
        lblinferir_origen_en_cambio_ubic.Location = New System.Drawing.Point(7, 215)
        lblinferir_origen_en_cambio_ubic.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblinferir_origen_en_cambio_ubic.Name = "lblinferir_origen_en_cambio_ubic"
        lblinferir_origen_en_cambio_ubic.Size = New System.Drawing.Size(277, 16)
        lblinferir_origen_en_cambio_ubic.TabIndex = 60
        lblinferir_origen_en_cambio_ubic.Text = "Inferir ubicación origen en cambio de ubicación"
        '
        'lblEliminarDocumentosSalida
        '
        lblEliminarDocumentosSalida.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblEliminarDocumentosSalida.AutoSize = True
        lblEliminarDocumentosSalida.Location = New System.Drawing.Point(10, 142)
        lblEliminarDocumentosSalida.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblEliminarDocumentosSalida.Name = "lblEliminarDocumentosSalida"
        lblEliminarDocumentosSalida.Size = New System.Drawing.Size(181, 16)
        lblEliminarDocumentosSalida.TabIndex = 62
        lblEliminarDocumentosSalida.Text = "Eliminar documentos de salida"
        '
        'lblPickeadorVerifica
        '
        lblPickeadorVerifica.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblPickeadorVerifica.AutoSize = True
        lblPickeadorVerifica.Location = New System.Drawing.Point(12, 25)
        lblPickeadorVerifica.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblPickeadorVerifica.Name = "lblPickeadorVerifica"
        lblPickeadorVerifica.Size = New System.Drawing.Size(236, 16)
        lblPickeadorVerifica.TabIndex = 64
        lblPickeadorVerifica.Text = "Operador de picking realiza verificación."
        '
        'lblPermitirCambioDeUbicacionEnPicking
        '
        lblPermitirCambioDeUbicacionEnPicking.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblPermitirCambioDeUbicacionEnPicking.AutoSize = True
        lblPermitirCambioDeUbicacionEnPicking.Location = New System.Drawing.Point(12, 227)
        lblPermitirCambioDeUbicacionEnPicking.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblPermitirCambioDeUbicacionEnPicking.Name = "lblPermitirCambioDeUbicacionEnPicking"
        lblPermitirCambioDeUbicacionEnPicking.Size = New System.Drawing.Size(318, 16)
        lblPermitirCambioDeUbicacionEnPicking.TabIndex = 66
        lblPermitirCambioDeUbicacionEnPicking.Text = "Permitir cambio de ubicación en picking (staging area)"
        '
        'Label58
        '
        Label58.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label58.AutoSize = True
        Label58.Location = New System.Drawing.Point(12, 171)
        Label58.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label58.Name = "Label58"
        Label58.Size = New System.Drawing.Size(291, 16)
        Label58.TabIndex = 70
        Label58.Text = "Permitir ""Buen Estado"" en reemplazo Picking (HH)"
        '
        'Label59
        '
        Label59.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label59.AutoSize = True
        Label59.Location = New System.Drawing.Point(8, 149)
        Label59.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label59.Name = "Label59"
        Label59.Size = New System.Drawing.Size(113, 16)
        Label59.TabIndex = 72
        Label59.Text = "Es Industria Motriz"
        '
        'lblrestringir_vencimiento_en_reemplazo
        '
        lblrestringir_vencimiento_en_reemplazo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblrestringir_vencimiento_en_reemplazo.AutoSize = True
        lblrestringir_vencimiento_en_reemplazo.ForeColor = System.Drawing.Color.Firebrick
        lblrestringir_vencimiento_en_reemplazo.Location = New System.Drawing.Point(7, 21)
        lblrestringir_vencimiento_en_reemplazo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblrestringir_vencimiento_en_reemplazo.Name = "lblrestringir_vencimiento_en_reemplazo"
        lblrestringir_vencimiento_en_reemplazo.Size = New System.Drawing.Size(216, 16)
        lblrestringir_vencimiento_en_reemplazo.TabIndex = 74
        lblrestringir_vencimiento_en_reemplazo.Text = "Restringir vencimiento en reemplazo"
        '
        'lblrestringir_lote_en_reemplazo
        '
        lblrestringir_lote_en_reemplazo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblrestringir_lote_en_reemplazo.AutoSize = True
        lblrestringir_lote_en_reemplazo.ForeColor = System.Drawing.Color.Firebrick
        lblrestringir_lote_en_reemplazo.Location = New System.Drawing.Point(7, 49)
        lblrestringir_lote_en_reemplazo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblrestringir_lote_en_reemplazo.Name = "lblrestringir_lote_en_reemplazo"
        lblrestringir_lote_en_reemplazo.Size = New System.Drawing.Size(175, 16)
        lblrestringir_lote_en_reemplazo.TabIndex = 76
        lblrestringir_lote_en_reemplazo.Text = "Restringir lotes en reemplazo"
        '
        'Label64
        '
        Label64.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label64.AutoSize = True
        Label64.Location = New System.Drawing.Point(8, 236)
        Label64.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label64.Name = "Label64"
        Label64.Size = New System.Drawing.Size(113, 16)
        Label64.TabIndex = 78
        Label64.Text = "Permitir decimales"
        '
        'lblPermitirRepeticionesEnIngreso
        '
        lblPermitirRepeticionesEnIngreso.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblPermitirRepeticionesEnIngreso.AutoSize = True
        lblPermitirRepeticionesEnIngreso.Location = New System.Drawing.Point(10, 55)
        lblPermitirRepeticionesEnIngreso.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblPermitirRepeticionesEnIngreso.Name = "lblPermitirRepeticionesEnIngreso"
        lblPermitirRepeticionesEnIngreso.Size = New System.Drawing.Size(168, 16)
        lblPermitirRepeticionesEnIngreso.TabIndex = 82
        lblPermitirRepeticionesEnIngreso.Text = "Despachar producto vencido"
        '
        'Label66
        '
        Label66.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label66.AutoSize = True
        Label66.Location = New System.Drawing.Point(7, 135)
        Label66.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label66.Name = "Label66"
        Label66.Size = New System.Drawing.Size(281, 16)
        Label66.TabIndex = 84
        Label66.Text = "Validar existencias en carga de inventario inicial"
        '
        'Label67
        '
        Label67.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label67.AutoSize = True
        Label67.Location = New System.Drawing.Point(10, 158)
        Label67.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label67.Name = "Label67"
        Label67.Size = New System.Drawing.Size(189, 16)
        Label67.TabIndex = 86
        Label67.Text = "Permitir repeticiones en ingreso"
        '
        'lblcalcular_ubicacion_sugerida_ml
        '
        lblcalcular_ubicacion_sugerida_ml.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblcalcular_ubicacion_sugerida_ml.AutoSize = True
        lblcalcular_ubicacion_sugerida_ml.Location = New System.Drawing.Point(8, 206)
        lblcalcular_ubicacion_sugerida_ml.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblcalcular_ubicacion_sugerida_ml.Name = "lblcalcular_ubicacion_sugerida_ml"
        lblcalcular_ubicacion_sugerida_ml.Size = New System.Drawing.Size(196, 16)
        lblcalcular_ubicacion_sugerida_ml.TabIndex = 87
        lblcalcular_ubicacion_sugerida_ml.Text = "Calcular Ubicacion Sugerida (HH)"
        '
        'Label68
        '
        Label68.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label68.AutoSize = True
        Label68.Location = New System.Drawing.Point(12, 255)
        Label68.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label68.Name = "Label68"
        Label68.Size = New System.Drawing.Size(173, 16)
        Label68.TabIndex = 91
        Label68.Text = "Ordenar picking descendente"
        '
        'Label69
        '
        Label69.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label69.AutoSize = True
        Label69.Location = New System.Drawing.Point(12, 282)
        Label69.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label69.Name = "Label69"
        Label69.Size = New System.Drawing.Size(181, 16)
        Label69.TabIndex = 93
        Label69.Text = "Ordenar por nombre completo"
        '
        'Label70
        '
        Label70.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label70.AutoSize = True
        Label70.Location = New System.Drawing.Point(12, 143)
        Label70.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label70.Name = "Label70"
        Label70.Size = New System.Drawing.Size(177, 16)
        Label70.TabIndex = 95
        Label70.Text = "Permitir reemplazo en picking"
        '
        'Label71
        '
        Label71.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label71.AutoSize = True
        Label71.Location = New System.Drawing.Point(10, 229)
        Label71.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label71.Name = "Label71"
        Label71.Size = New System.Drawing.Size(202, 16)
        Label71.TabIndex = 96
        Label71.Text = "Permitir reemplazo en verificación"
        '
        'Label72
        '
        Label72.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label72.AutoSize = True
        Label72.Location = New System.Drawing.Point(12, 54)
        Label72.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label72.Name = "Label72"
        Label72.Size = New System.Drawing.Size(199, 16)
        Label72.TabIndex = 97
        Label72.Text = "Permitir no encontrado en Picking"
        '
        'lblPermitirReemplazoPickingMismaLIcencia
        '
        lblPermitirReemplazoPickingMismaLIcencia.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblPermitirReemplazoPickingMismaLIcencia.AutoSize = True
        lblPermitirReemplazoPickingMismaLIcencia.Location = New System.Drawing.Point(12, 83)
        lblPermitirReemplazoPickingMismaLIcencia.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblPermitirReemplazoPickingMismaLIcencia.Name = "lblPermitirReemplazoPickingMismaLIcencia"
        lblPermitirReemplazoPickingMismaLIcencia.Size = New System.Drawing.Size(305, 16)
        lblPermitirReemplazoPickingMismaLIcencia.TabIndex = 101
        lblPermitirReemplazoPickingMismaLIcencia.Text = "Permitir reemplazo en picking por la misma Licencia"
        '
        'Label75
        '
        Label75.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label75.AutoSize = True
        Label75.Location = New System.Drawing.Point(10, 171)
        Label75.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label75.Name = "Label75"
        Label75.Size = New System.Drawing.Size(158, 16)
        Label75.TabIndex = 103
        Label75.Text = "Flitrar pedidos por usuario"
        '
        'Label76
        '
        Label76.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label76.AutoSize = True
        Label76.Location = New System.Drawing.Point(7, 82)
        Label76.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label76.Name = "Label76"
        Label76.Size = New System.Drawing.Size(199, 16)
        Label76.TabIndex = 105
        Label76.Text = "Liberar stock despachos parciales"
        '
        'lblSimbologia
        '
        lblSimbologia.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblSimbologia.AutoSize = True
        lblSimbologia.Location = New System.Drawing.Point(-197, 640)
        lblSimbologia.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblSimbologia.Name = "lblSimbologia"
        lblSimbologia.Size = New System.Drawing.Size(121, 16)
        lblSimbologia.TabIndex = 43
        lblSimbologia.Text = "Simbología licencia:"
        '
        'Label77
        '
        Label77.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label77.AutoSize = True
        Label77.Location = New System.Drawing.Point(9, 266)
        Label77.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label77.Name = "Label77"
        Label77.Size = New System.Drawing.Size(85, 16)
        Label77.TabIndex = 111
        Label77.Text = "Interface SAP"
        '
        'Label78
        '
        Label78.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label78.AutoSize = True
        Label78.Location = New System.Drawing.Point(8, 294)
        Label78.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label78.Name = "Label78"
        Label78.Size = New System.Drawing.Size(125, 16)
        Label78.TabIndex = 113
        Label78.Text = "Restringir áreas SAP"
        '
        'Label79
        '
        Label79.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label79.AutoSize = True
        Label79.Location = New System.Drawing.Point(7, 242)
        Label79.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label79.Name = "Label79"
        Label79.Size = New System.Drawing.Size(118, 16)
        Label79.TabIndex = 115
        Label79.Text = "Control pallet mixto"
        '
        'lbldespachoauto
        '
        lbldespachoauto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lbldespachoauto.AutoSize = True
        lbldespachoauto.Location = New System.Drawing.Point(10, 200)
        lbldespachoauto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lbldespachoauto.Name = "lbldespachoauto"
        lbldespachoauto.Size = New System.Drawing.Size(149, 16)
        lbldespachoauto.TabIndex = 117
        lbldespachoauto.Text = "Despacho automático HH"
        '
        'lblLimpiarCampos
        '
        lblLimpiarCampos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblLimpiarCampos.AutoSize = True
        lblLimpiarCampos.Location = New System.Drawing.Point(8, 322)
        lblLimpiarCampos.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblLimpiarCampos.Name = "lblLimpiarCampos"
        lblLimpiarCampos.Size = New System.Drawing.Size(117, 16)
        lblLimpiarCampos.TabIndex = 119
        lblLimpiarCampos.Text = "Limpiar campos HH"
        '
        'lblControlTallaColor
        '
        lblControlTallaColor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblControlTallaColor.AutoSize = True
        lblControlTallaColor.Location = New System.Drawing.Point(8, 270)
        lblControlTallaColor.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblControlTallaColor.Name = "lblControlTallaColor"
        lblControlTallaColor.Size = New System.Drawing.Size(115, 16)
        lblControlTallaColor.TabIndex = 117
        lblControlTallaColor.Text = "Control Talla/Color"
        '
        'Label83
        '
        Label83.AutoSize = True
        Label83.Location = New System.Drawing.Point(7, 80)
        Label83.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label83.Name = "Label83"
        Label83.Size = New System.Drawing.Size(64, 16)
        Label83.TabIndex = 47
        Label83.Text = "Dirección:"
        '
        'Label84
        '
        Label84.AutoSize = True
        Label84.Location = New System.Drawing.Point(7, 46)
        Label84.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label84.Name = "Label84"
        Label84.Size = New System.Drawing.Size(86, 16)
        Label84.TabIndex = 49
        Label84.Text = "Centro Costo:"
        '
        'Label85
        '
        Label85.AutoSize = True
        Label85.Location = New System.Drawing.Point(7, 114)
        Label85.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label85.Name = "Label85"
        Label85.Size = New System.Drawing.Size(93, 16)
        Label85.TabIndex = 51
        Label85.Text = "Departamento:"
        '
        'Label88
        '
        Label88.AutoSize = True
        Label88.Location = New System.Drawing.Point(7, 80)
        Label88.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label88.Name = "Label88"
        Label88.Size = New System.Drawing.Size(49, 16)
        Label88.TabIndex = 47
        Label88.Text = "Puerto:"
        '
        'Label89
        '
        Label89.AutoSize = True
        Label89.Location = New System.Drawing.Point(7, 46)
        Label89.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label89.Name = "Label89"
        Label89.Size = New System.Drawing.Size(92, 16)
        Label89.TabIndex = 49
        Label89.Text = "Servidor smtp:"
        '
        'Label90
        '
        Label90.AutoSize = True
        Label90.Location = New System.Drawing.Point(8, 150)
        Label90.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label90.Name = "Label90"
        Label90.Size = New System.Drawing.Size(55, 16)
        Label90.TabIndex = 51
        Label90.Text = "Usuario:"
        '
        'Label91
        '
        Label91.AutoSize = True
        Label91.Location = New System.Drawing.Point(7, 115)
        Label91.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label91.Name = "Label91"
        Label91.Size = New System.Drawing.Size(74, 16)
        Label91.TabIndex = 56
        Label91.Text = "Cifrado Ssl:"
        '
        'Label92
        '
        Label92.AutoSize = True
        Label92.Location = New System.Drawing.Point(11, 182)
        Label92.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label92.Name = "Label92"
        Label92.Size = New System.Drawing.Size(67, 16)
        Label92.TabIndex = 60
        Label92.Text = "Password:"
        '
        'Label93
        '
        Label93.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label93.AutoSize = True
        Label93.Location = New System.Drawing.Point(10, 258)
        Label93.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label93.Name = "Label93"
        Label93.Size = New System.Drawing.Size(123, 16)
        Label93.TabIndex = 128
        Label93.Text = "Reemplazo Opcional"
        '
        'lblControlGondola
        '
        Me.lblControlGondola.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblControlGondola.AutoSize = True
        Me.lblControlGondola.Location = New System.Drawing.Point(8, 322)
        Me.lblControlGondola.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblControlGondola.Name = "lblControlGondola"
        Me.lblControlGondola.Size = New System.Drawing.Size(97, 16)
        Me.lblControlGondola.TabIndex = 125
        Me.lblControlGondola.Text = "Control gondola"
        '
        'Label80
        '
        Me.Label80.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label80.AutoSize = True
        Me.Label80.Location = New System.Drawing.Point(381, 50)
        Me.Label80.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(151, 16)
        Me.Label80.TabIndex = 118
        Me.Label80.Text = "Verificacion Consolidada:"
        '
        'Label81
        '
        Me.Label81.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label81.AutoSize = True
        Me.Label81.Location = New System.Drawing.Point(10, 220)
        Me.Label81.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(263, 16)
        Me.Label81.TabIndex = 88
        Me.Label81.Text = "Permitir cambio de ubicación en la recepción"
        '
        'lblRutaCDN
        '
        Me.lblRutaCDN.AutoSize = True
        Me.lblRutaCDN.Location = New System.Drawing.Point(684, 28)
        Me.lblRutaCDN.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRutaCDN.Name = "lblRutaCDN"
        Me.lblRutaCDN.Size = New System.Drawing.Size(66, 16)
        Me.lblRutaCDN.TabIndex = 48
        Me.lblRutaCDN.Text = "Ruta CDN:"
        '
        'lblHomologarLoteConFechaVence
        '
        Me.lblHomologarLoteConFechaVence.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblHomologarLoteConFechaVence.AutoSize = True
        Me.lblHomologarLoteConFechaVence.Location = New System.Drawing.Point(7, 108)
        Me.lblHomologarLoteConFechaVence.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHomologarLoteConFechaVence.Name = "lblHomologarLoteConFechaVence"
        Me.lblHomologarLoteConFechaVence.Size = New System.Drawing.Size(243, 16)
        Me.lblHomologarLoteConFechaVence.TabIndex = 107
        Me.lblHomologarLoteConFechaVence.Text = "Homologar lote con fecha de vencimiento"
        '
        'lblEscanearLicenciaPicking
        '
        Me.lblEscanearLicenciaPicking.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEscanearLicenciaPicking.AutoSize = True
        Me.lblEscanearLicenciaPicking.Location = New System.Drawing.Point(12, 113)
        Me.lblEscanearLicenciaPicking.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEscanearLicenciaPicking.Name = "lblEscanearLicenciaPicking"
        Me.lblEscanearLicenciaPicking.Size = New System.Drawing.Size(166, 16)
        Me.lblEscanearLicenciaPicking.TabIndex = 109
        Me.lblEscanearLicenciaPicking.Text = "Escanear licencia en picking"
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(100, 38)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(205, 22)
        Me.User_agrTextEdit.TabIndex = 4
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(583, 38)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(205, 22)
        Me.User_modTextEdit.TabIndex = 6
        '
        'Fec_agrTextEdit
        '
        Me.Fec_agrTextEdit.Enabled = False
        Me.Fec_agrTextEdit.Location = New System.Drawing.Point(314, 38)
        Me.Fec_agrTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.Fec_agrTextEdit.Name = "Fec_agrTextEdit"
        Me.Fec_agrTextEdit.Size = New System.Drawing.Size(205, 22)
        Me.Fec_agrTextEdit.TabIndex = 5
        '
        'Fec_modTextEdit
        '
        Me.Fec_modTextEdit.Enabled = False
        Me.Fec_modTextEdit.Location = New System.Drawing.Point(796, 38)
        Me.Fec_modTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.Fec_modTextEdit.Name = "Fec_modTextEdit"
        Me.Fec_modTextEdit.Size = New System.Drawing.Size(205, 22)
        Me.Fec_modTextEdit.TabIndex = 7
        '
        'GroupControl12
        '
        Me.GroupControl12.AlwaysScrollActiveControlIntoView = False
        Me.GroupControl12.Controls.Add(lblIdTramo)
        Me.GroupControl12.Controls.Add(Me.txtIdTramo)
        Me.GroupControl12.Controls.Add(Me.txtTipoRack)
        Me.GroupControl12.Controls.Add(Me.chkOrient)
        Me.GroupControl12.Controls.Add(Me.Label47)
        Me.GroupControl12.Controls.Add(Me.txtIndice)
        Me.GroupControl12.Controls.Add(Me.Label36)
        Me.GroupControl12.Controls.Add(Me.chkEsRack)
        Me.GroupControl12.Controls.Add(Me.cmbFont)
        Me.GroupControl12.Controls.Add(Me.cmbSector)
        Me.GroupControl12.Controls.Add(Me.cmbAreasR)
        Me.GroupControl12.Controls.Add(Me.Label33)
        Me.GroupControl12.Controls.Add(Label45)
        Me.GroupControl12.Controls.Add(Me.chkActivoTramo)
        Me.GroupControl12.Controls.Add(lblCodigoSectorReferencia)
        Me.GroupControl12.Controls.Add(Label15)
        Me.GroupControl12.Controls.Add(Me.txtCodigoTramo)
        Me.GroupControl12.Controls.Add(Me.chkSistemaTramo)
        Me.GroupControl12.Controls.Add(Label26)
        Me.GroupControl12.Controls.Add(Me.txtDescripcionTramo)
        Me.GroupControl12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl12.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl12.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl12.Name = "GroupControl12"
        Me.GroupControl12.Size = New System.Drawing.Size(718, 305)
        Me.GroupControl12.TabIndex = 0
        Me.GroupControl12.Text = "Datos de Tramo"
        '
        'txtIdTramo
        '
        Me.txtIdTramo.Location = New System.Drawing.Point(139, 52)
        Me.txtIdTramo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdTramo.Name = "txtIdTramo"
        Me.txtIdTramo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtIdTramo.Properties.MaxLength = 50
        Me.txtIdTramo.Properties.ReadOnly = True
        Me.txtIdTramo.Size = New System.Drawing.Size(411, 22)
        Me.txtIdTramo.TabIndex = 20
        '
        'txtTipoRack
        '
        Me.txtTipoRack.Location = New System.Drawing.Point(139, 210)
        Me.txtTipoRack.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTipoRack.Maximum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.txtTipoRack.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtTipoRack.Name = "txtTipoRack"
        Me.txtTipoRack.Size = New System.Drawing.Size(411, 23)
        Me.txtTipoRack.TabIndex = 18
        Me.txtTipoRack.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'chkOrient
        '
        Me.chkOrient.Location = New System.Drawing.Point(579, 220)
        Me.chkOrient.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkOrient.MenuManager = Me.mnu
        Me.chkOrient.Name = "chkOrient"
        Me.chkOrient.Properties.Caption = "Orientación Izq."
        Me.chkOrient.Size = New System.Drawing.Size(195, 24)
        Me.chkOrient.TabIndex = 17
        '
        'mnu
        '
        Me.mnu.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(37)
        Me.mnu.ExpandCollapseItem.Id = 0
        Me.mnu.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.mnu.ExpandCollapseItem, Me.BarButtonItem2, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.mnuDiseñoGrafico, Me.mnuEstructuraInicial, Me.cmdRefrescar, Me.mnuParametrosInterface, Me.mnuEditarConnIni, Me.mnuUnificarBodegas, Me.cmdImprimir, Me.cmdHabilitarReemplazo, Me.cmdDeshabilitarReemplazo, Me.BarButtonItem1, Me.mnuActualizarIndicesRotacion, Me.mnuPlantillaIndicesRotacion})
        Me.mnu.Location = New System.Drawing.Point(0, 0)
        Me.mnu.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.mnu.MaxItemId = 21
        Me.mnu.Name = "mnu"
        Me.mnu.OptionsMenuMinWidth = 412
        Me.mnu.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage2})
        Me.mnu.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.mnu.Size = New System.Drawing.Size(1786, 193)
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Id = 1
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 2
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 3
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 4
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'mnuDiseñoGrafico
        '
        Me.mnuDiseñoGrafico.Caption = "Diseño"
        Me.mnuDiseñoGrafico.Id = 6
        Me.mnuDiseñoGrafico.ImageOptions.SvgImage = CType(resources.GetObject("mnuDiseñoGrafico.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuDiseñoGrafico.Name = "mnuDiseñoGrafico"
        '
        'mnuEstructuraInicial
        '
        Me.mnuEstructuraInicial.Caption = "Estructura Inicial"
        Me.mnuEstructuraInicial.Id = 7
        Me.mnuEstructuraInicial.ImageOptions.SvgImage = CType(resources.GetObject("mnuEstructuraInicial.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEstructuraInicial.Name = "mnuEstructuraInicial"
        '
        'cmdRefrescar
        '
        Me.cmdRefrescar.Caption = "Recargar"
        Me.cmdRefrescar.Id = 8
        Me.cmdRefrescar.ImageOptions.SvgImage = CType(resources.GetObject("cmdRefrescar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdRefrescar.Name = "cmdRefrescar"
        '
        'mnuParametrosInterface
        '
        Me.mnuParametrosInterface.Caption = "Parámetros Interface"
        Me.mnuParametrosInterface.Id = 9
        Me.mnuParametrosInterface.ImageOptions.SvgImage = CType(resources.GetObject("mnuParametrosInterface.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuParametrosInterface.Name = "mnuParametrosInterface"
        '
        'mnuEditarConnIni
        '
        Me.mnuEditarConnIni.Caption = "Editar Conn.ini"
        Me.mnuEditarConnIni.Id = 10
        Me.mnuEditarConnIni.ImageOptions.SvgImage = CType(resources.GetObject("mnuEditarConnIni.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEditarConnIni.Name = "mnuEditarConnIni"
        Me.mnuEditarConnIni.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuUnificarBodegas
        '
        Me.mnuUnificarBodegas.Caption = "Unificar Bodegas"
        Me.mnuUnificarBodegas.Id = 11
        Me.mnuUnificarBodegas.ImageOptions.SvgImage = CType(resources.GetObject("mnuUnificarBodegas.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuUnificarBodegas.Name = "mnuUnificarBodegas"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir lista de ubicaciones"
        Me.cmdImprimir.Id = 12
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'cmdHabilitarReemplazo
        '
        Me.cmdHabilitarReemplazo.Caption = "Habilitar reemplazo"
        Me.cmdHabilitarReemplazo.Id = 15
        Me.cmdHabilitarReemplazo.ImageOptions.Image = CType(resources.GetObject("cmdHabilitarReemplazo.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdHabilitarReemplazo.ImageOptions.LargeImage = CType(resources.GetObject("cmdHabilitarReemplazo.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdHabilitarReemplazo.Name = "cmdHabilitarReemplazo"
        '
        'cmdDeshabilitarReemplazo
        '
        Me.cmdDeshabilitarReemplazo.Caption = "Deshabilitar reemplazo"
        Me.cmdDeshabilitarReemplazo.Id = 16
        Me.cmdDeshabilitarReemplazo.ImageOptions.Image = CType(resources.GetObject("cmdDeshabilitarReemplazo.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdDeshabilitarReemplazo.ImageOptions.LargeImage = CType(resources.GetObject("cmdDeshabilitarReemplazo.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdDeshabilitarReemplazo.Name = "cmdDeshabilitarReemplazo"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Reglas de vencimiento"
        Me.BarButtonItem1.Id = 17
        Me.BarButtonItem1.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'mnuActualizarIndicesRotacion
        '
        Me.mnuActualizarIndicesRotacion.Caption = "Actualizar índices de rotación"
        Me.mnuActualizarIndicesRotacion.Id = 19
        Me.mnuActualizarIndicesRotacion.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizarIndicesRotacion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizarIndicesRotacion.Name = "mnuActualizarIndicesRotacion"
        '
        'mnuPlantillaIndicesRotacion
        '
        Me.mnuPlantillaIndicesRotacion.Caption = "Plantilla índices de rotación"
        Me.mnuPlantillaIndicesRotacion.Id = 20
        Me.mnuPlantillaIndicesRotacion.ImageOptions.SvgImage = CType(resources.GetObject("mnuPlantillaIndicesRotacion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuPlantillaIndicesRotacion.Name = "mnuPlantillaIndicesRotacion"
        '
        'RibbonPage2
        '
        Me.RibbonPage2.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup2, Me.RibbonPageGroup1, Me.RibbonPageGroup3, Me.RibbonPageGroup4, Me.RibbonPageGroup5})
        Me.RibbonPage2.Name = "RibbonPage2"
        Me.RibbonPage2.Text = "Mantenimiento de bodega"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuDiseñoGrafico)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuEstructuraInicial)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.cmdRefrescar)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuParametrosInterface)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuEditarConnIni)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.cmdHabilitarReemplazo)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.cmdDeshabilitarReemplazo)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuUnificarBodegas)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'RibbonPageGroup4
        '
        Me.RibbonPageGroup4.ItemLinks.Add(Me.BarButtonItem1)
        Me.RibbonPageGroup4.Name = "RibbonPageGroup4"
        '
        'RibbonPageGroup5
        '
        Me.RibbonPageGroup5.ItemLinks.Add(Me.mnuPlantillaIndicesRotacion)
        Me.RibbonPageGroup5.ItemLinks.Add(Me.mnuActualizarIndicesRotacion)
        Me.RibbonPageGroup5.Name = "RibbonPageGroup5"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(31, 249)
        Me.Label47.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(58, 16)
        Me.Label47.TabIndex = 16
        Me.Label47.Text = "Indice X:"
        '
        'txtIndice
        '
        Me.txtIndice.Location = New System.Drawing.Point(139, 242)
        Me.txtIndice.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIndice.MenuManager = Me.mnu
        Me.txtIndice.Name = "txtIndice"
        Me.txtIndice.Size = New System.Drawing.Size(411, 22)
        Me.txtIndice.TabIndex = 15
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(31, 213)
        Me.Label36.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(68, 16)
        Me.Label36.TabIndex = 14
        Me.Label36.Text = "Tipo Rack:"
        '
        'chkEsRack
        '
        Me.chkEsRack.Location = New System.Drawing.Point(579, 178)
        Me.chkEsRack.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkEsRack.MenuManager = Me.mnu
        Me.chkEsRack.Name = "chkEsRack"
        Me.chkEsRack.Properties.Caption = "Es Rack"
        Me.chkEsRack.Size = New System.Drawing.Size(89, 24)
        Me.chkEsRack.TabIndex = 8
        '
        'cmbFont
        '
        Me.cmbFont.Location = New System.Drawing.Point(139, 274)
        Me.cmbFont.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbFont.MenuManager = Me.mnu
        Me.cmbFont.Name = "cmbFont"
        Me.cmbFont.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbFont.Properties.NullText = ""
        Me.cmbFont.Size = New System.Drawing.Size(411, 22)
        Me.cmbFont.TabIndex = 12
        '
        'cmbSector
        '
        Me.cmbSector.Location = New System.Drawing.Point(139, 116)
        Me.cmbSector.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbSector.MenuManager = Me.mnu
        Me.cmbSector.Name = "cmbSector"
        Me.cmbSector.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbSector.Properties.NullText = ""
        Me.cmbSector.Size = New System.Drawing.Size(411, 22)
        Me.cmbSector.TabIndex = 4
        '
        'cmbAreasR
        '
        Me.cmbAreasR.Location = New System.Drawing.Point(139, 84)
        Me.cmbAreasR.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbAreasR.MenuManager = Me.mnu
        Me.cmbAreasR.Name = "cmbAreasR"
        Me.cmbAreasR.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbAreasR.Properties.NullText = ""
        Me.cmbAreasR.Size = New System.Drawing.Size(411, 22)
        Me.cmbAreasR.TabIndex = 2
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(31, 278)
        Me.Label33.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(37, 16)
        Me.Label33.TabIndex = 11
        Me.Label33.Text = "Font:"
        '
        'chkActivoTramo
        '
        Me.chkActivoTramo.EditValue = True
        Me.chkActivoTramo.Location = New System.Drawing.Point(579, 89)
        Me.chkActivoTramo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkActivoTramo.Name = "chkActivoTramo"
        Me.chkActivoTramo.Properties.Caption = "Activo"
        Me.chkActivoTramo.Size = New System.Drawing.Size(89, 24)
        Me.chkActivoTramo.TabIndex = 1
        '
        'txtCodigoTramo
        '
        Me.txtCodigoTramo.Location = New System.Drawing.Point(139, 148)
        Me.txtCodigoTramo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCodigoTramo.Name = "txtCodigoTramo"
        Me.txtCodigoTramo.Properties.MaxLength = 50
        Me.txtCodigoTramo.Size = New System.Drawing.Size(411, 22)
        Me.txtCodigoTramo.TabIndex = 7
        '
        'chkSistemaTramo
        '
        Me.chkSistemaTramo.Location = New System.Drawing.Point(579, 135)
        Me.chkSistemaTramo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkSistemaTramo.Name = "chkSistemaTramo"
        Me.chkSistemaTramo.Properties.Caption = "Sistema"
        Me.chkSistemaTramo.Size = New System.Drawing.Size(89, 24)
        Me.chkSistemaTramo.TabIndex = 5
        '
        'txtDescripcionTramo
        '
        Me.txtDescripcionTramo.Location = New System.Drawing.Point(139, 180)
        Me.txtDescripcionTramo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDescripcionTramo.Name = "txtDescripcionTramo"
        Me.txtDescripcionTramo.Properties.MaxLength = 50
        Me.txtDescripcionTramo.Size = New System.Drawing.Size(411, 22)
        Me.txtDescripcionTramo.TabIndex = 10
        '
        'GroupControl11
        '
        Me.GroupControl11.Controls.Add(Me.Label34)
        Me.GroupControl11.Controls.Add(Me.chkOrientacion)
        Me.GroupControl11.Controls.Add(Me.nUpdMargenInferiorTramo)
        Me.GroupControl11.Controls.Add(Me.nUpdMargenSuperiorTramo)
        Me.GroupControl11.Controls.Add(Me.nUpdMargenDerechoTramo)
        Me.GroupControl11.Controls.Add(Me.nUpdMargenIzquierdoTramo)
        Me.GroupControl11.Controls.Add(Me.nUpdAnchoTramo)
        Me.GroupControl11.Controls.Add(Me.nUpdLargoTramo)
        Me.GroupControl11.Controls.Add(Me.nUpdAltoTramo)
        Me.GroupControl11.Controls.Add(Label16)
        Me.GroupControl11.Controls.Add(Label17)
        Me.GroupControl11.Controls.Add(Label18)
        Me.GroupControl11.Controls.Add(Label19)
        Me.GroupControl11.Controls.Add(Label20)
        Me.GroupControl11.Controls.Add(Label21)
        Me.GroupControl11.Controls.Add(Label22)
        Me.GroupControl11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl11.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl11.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl11.Name = "GroupControl11"
        Me.GroupControl11.Size = New System.Drawing.Size(1060, 305)
        Me.GroupControl11.TabIndex = 0
        Me.GroupControl11.Text = "Dimensiones"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(31, 185)
        Me.Label34.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(69, 16)
        Me.Label34.TabIndex = 14
        Me.Label34.Text = "Horizontal:"
        '
        'chkOrientacion
        '
        Me.chkOrientacion.AutoSize = True
        Me.chkOrientacion.Location = New System.Drawing.Point(130, 185)
        Me.chkOrientacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkOrientacion.Name = "chkOrientacion"
        Me.chkOrientacion.Size = New System.Drawing.Size(18, 17)
        Me.chkOrientacion.TabIndex = 15
        Me.chkOrientacion.UseVisualStyleBackColor = True
        '
        'nUpdMargenInferiorTramo
        '
        Me.nUpdMargenInferiorTramo.DecimalPlaces = 6
        Me.nUpdMargenInferiorTramo.Location = New System.Drawing.Point(540, 172)
        Me.nUpdMargenInferiorTramo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdMargenInferiorTramo.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdMargenInferiorTramo.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdMargenInferiorTramo.Name = "nUpdMargenInferiorTramo"
        Me.nUpdMargenInferiorTramo.Size = New System.Drawing.Size(170, 23)
        Me.nUpdMargenInferiorTramo.TabIndex = 13
        '
        'nUpdMargenSuperiorTramo
        '
        Me.nUpdMargenSuperiorTramo.DecimalPlaces = 6
        Me.nUpdMargenSuperiorTramo.Location = New System.Drawing.Point(540, 132)
        Me.nUpdMargenSuperiorTramo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdMargenSuperiorTramo.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdMargenSuperiorTramo.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdMargenSuperiorTramo.Name = "nUpdMargenSuperiorTramo"
        Me.nUpdMargenSuperiorTramo.Size = New System.Drawing.Size(170, 23)
        Me.nUpdMargenSuperiorTramo.TabIndex = 10
        '
        'nUpdMargenDerechoTramo
        '
        Me.nUpdMargenDerechoTramo.DecimalPlaces = 6
        Me.nUpdMargenDerechoTramo.Location = New System.Drawing.Point(540, 89)
        Me.nUpdMargenDerechoTramo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdMargenDerechoTramo.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdMargenDerechoTramo.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdMargenDerechoTramo.Name = "nUpdMargenDerechoTramo"
        Me.nUpdMargenDerechoTramo.Size = New System.Drawing.Size(170, 23)
        Me.nUpdMargenDerechoTramo.TabIndex = 6
        '
        'nUpdMargenIzquierdoTramo
        '
        Me.nUpdMargenIzquierdoTramo.DecimalPlaces = 6
        Me.nUpdMargenIzquierdoTramo.Location = New System.Drawing.Point(540, 46)
        Me.nUpdMargenIzquierdoTramo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdMargenIzquierdoTramo.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdMargenIzquierdoTramo.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdMargenIzquierdoTramo.Name = "nUpdMargenIzquierdoTramo"
        Me.nUpdMargenIzquierdoTramo.Size = New System.Drawing.Size(170, 23)
        Me.nUpdMargenIzquierdoTramo.TabIndex = 0
        '
        'nUpdAnchoTramo
        '
        Me.nUpdAnchoTramo.DecimalPlaces = 6
        Me.nUpdAnchoTramo.Location = New System.Drawing.Point(130, 140)
        Me.nUpdAnchoTramo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdAnchoTramo.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdAnchoTramo.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdAnchoTramo.Name = "nUpdAnchoTramo"
        Me.nUpdAnchoTramo.Size = New System.Drawing.Size(170, 23)
        Me.nUpdAnchoTramo.TabIndex = 11
        '
        'nUpdLargoTramo
        '
        Me.nUpdLargoTramo.DecimalPlaces = 6
        Me.nUpdLargoTramo.Location = New System.Drawing.Point(130, 95)
        Me.nUpdLargoTramo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdLargoTramo.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdLargoTramo.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdLargoTramo.Name = "nUpdLargoTramo"
        Me.nUpdLargoTramo.Size = New System.Drawing.Size(170, 23)
        Me.nUpdLargoTramo.TabIndex = 7
        '
        'nUpdAltoTramo
        '
        Me.nUpdAltoTramo.DecimalPlaces = 6
        Me.nUpdAltoTramo.Location = New System.Drawing.Point(130, 50)
        Me.nUpdAltoTramo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdAltoTramo.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdAltoTramo.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdAltoTramo.Name = "nUpdAltoTramo"
        Me.nUpdAltoTramo.Size = New System.Drawing.Size(170, 23)
        Me.nUpdAltoTramo.TabIndex = 1
        '
        'GroupControl10
        '
        Me.GroupControl10.Controls.Add(Me.grdTramo)
        Me.GroupControl10.Controls.Add(Me.chkTramosActivos)
        Me.GroupControl10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl10.Location = New System.Drawing.Point(0, 332)
        Me.GroupControl10.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl10.Name = "GroupControl10"
        Me.GroupControl10.Size = New System.Drawing.Size(1784, 379)
        Me.GroupControl10.TabIndex = 2
        Me.GroupControl10.Text = "Detalle Tramos"
        '
        'grdTramo
        '
        Me.grdTramo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdTramo.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdTramo.Location = New System.Drawing.Point(2, 28)
        Me.grdTramo.MainView = Me.GridViewTramo
        Me.grdTramo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdTramo.Name = "grdTramo"
        Me.grdTramo.Size = New System.Drawing.Size(1780, 325)
        Me.grdTramo.TabIndex = 0
        Me.grdTramo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewTramo})
        '
        'GridViewTramo
        '
        Me.GridViewTramo.DetailHeight = 437
        Me.GridViewTramo.GridControl = Me.grdTramo
        Me.GridViewTramo.Name = "GridViewTramo"
        Me.GridViewTramo.OptionsBehavior.Editable = False
        Me.GridViewTramo.OptionsEditForm.PopupEditFormWidth = 1000
        Me.GridViewTramo.OptionsView.ShowFooter = True
        Me.GridViewTramo.OptionsView.ShowGroupPanel = False
        '
        'chkTramosActivos
        '
        Me.chkTramosActivos.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.chkTramosActivos.EditValue = True
        Me.chkTramosActivos.Location = New System.Drawing.Point(2, 353)
        Me.chkTramosActivos.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkTramosActivos.Name = "chkTramosActivos"
        Me.chkTramosActivos.Properties.Caption = "Activos"
        Me.chkTramosActivos.Size = New System.Drawing.Size(1780, 24)
        Me.chkTramosActivos.TabIndex = 1
        '
        'GroupControl14
        '
        Me.GroupControl14.Controls.Add(Me.chkUbicacionMuelle)
        Me.GroupControl14.Controls.Add(Me.chkUbicPrdNE)
        Me.GroupControl14.Controls.Add(Me.chkUbicacionesActivas)
        Me.GroupControl14.Controls.Add(Me.cmbOrientacion)
        Me.GroupControl14.Controls.Add(Me.chkEsBodegaVirtual)
        Me.GroupControl14.Controls.Add(Me.nUpdMargenInferiorUbicacion)
        Me.GroupControl14.Controls.Add(Me.nUpdMargenSuperiorUbicacion)
        Me.GroupControl14.Controls.Add(Me.Label29)
        Me.GroupControl14.Controls.Add(Me.nUpdMargenDerechoUbicacion)
        Me.GroupControl14.Controls.Add(Me.chkMerma)
        Me.GroupControl14.Controls.Add(Me.nUpdMargenIzquierdoUbicacion)
        Me.GroupControl14.Controls.Add(Me.nUpdAnchoUbicacion)
        Me.GroupControl14.Controls.Add(Me.nUpdLargoUbicacion)
        Me.GroupControl14.Controls.Add(Me.nUpdAltoUbicacion)
        Me.GroupControl14.Controls.Add(Label37)
        Me.GroupControl14.Controls.Add(Me.chkDespacho)
        Me.GroupControl14.Controls.Add(Label38)
        Me.GroupControl14.Controls.Add(Me.chkDañadoUbicacion)
        Me.GroupControl14.Controls.Add(Label39)
        Me.GroupControl14.Controls.Add(Label40)
        Me.GroupControl14.Controls.Add(Label41)
        Me.GroupControl14.Controls.Add(Me.chkUbicacionPicking)
        Me.GroupControl14.Controls.Add(Label42)
        Me.GroupControl14.Controls.Add(Me.chkActivoUbicacion)
        Me.GroupControl14.Controls.Add(Label43)
        Me.GroupControl14.Controls.Add(Me.chkAceptaPalletUbicacion)
        Me.GroupControl14.Controls.Add(Me.chkRecepcion)
        Me.GroupControl14.Controls.Add(Me.chkBloqueadaUbicacion)
        Me.GroupControl14.Controls.Add(Me.chkSistemaUbicacion)
        Me.GroupControl14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl14.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl14.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl14.Name = "GroupControl14"
        Me.GroupControl14.Size = New System.Drawing.Size(1075, 380)
        Me.GroupControl14.TabIndex = 0
        Me.GroupControl14.Text = "Dimensiones"
        '
        'chkUbicacionMuelle
        '
        Me.chkUbicacionMuelle.Location = New System.Drawing.Point(473, 273)
        Me.chkUbicacionMuelle.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkUbicacionMuelle.Name = "chkUbicacionMuelle"
        Me.chkUbicacionMuelle.Properties.Caption = "Ubicación Muelle"
        Me.chkUbicacionMuelle.Size = New System.Drawing.Size(218, 24)
        Me.chkUbicacionMuelle.TabIndex = 27
        '
        'chkUbicPrdNE
        '
        Me.chkUbicPrdNE.Location = New System.Drawing.Point(219, 319)
        Me.chkUbicPrdNE.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkUbicPrdNE.Name = "chkUbicPrdNE"
        Me.chkUbicPrdNE.Properties.Caption = "Ubicación Producto NE"
        Me.chkUbicPrdNE.Size = New System.Drawing.Size(200, 24)
        Me.chkUbicPrdNE.TabIndex = 26
        '
        'chkUbicacionesActivas
        '
        Me.chkUbicacionesActivas.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.chkUbicacionesActivas.EditValue = True
        Me.chkUbicacionesActivas.Location = New System.Drawing.Point(2, 354)
        Me.chkUbicacionesActivas.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkUbicacionesActivas.Name = "chkUbicacionesActivas"
        Me.chkUbicacionesActivas.Properties.Caption = "Listar ubicaciones activas"
        Me.chkUbicacionesActivas.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.chkUbicacionesActivas.Size = New System.Drawing.Size(1071, 24)
        Me.chkUbicacionesActivas.TabIndex = 1
        '
        'cmbOrientacion
        '
        Me.cmbOrientacion.Location = New System.Drawing.Point(465, 165)
        Me.cmbOrientacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbOrientacion.MenuManager = Me.mnu
        Me.cmbOrientacion.Name = "cmbOrientacion"
        Me.cmbOrientacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOrientacion.Properties.NullText = ""
        Me.cmbOrientacion.Size = New System.Drawing.Size(218, 22)
        Me.cmbOrientacion.TabIndex = 15
        '
        'chkEsBodegaVirtual
        '
        Me.chkEsBodegaVirtual.Location = New System.Drawing.Point(227, 276)
        Me.chkEsBodegaVirtual.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkEsBodegaVirtual.Name = "chkEsBodegaVirtual"
        Me.chkEsBodegaVirtual.Properties.Caption = "Bodega Virtual"
        Me.chkEsBodegaVirtual.Size = New System.Drawing.Size(200, 24)
        Me.chkEsBodegaVirtual.TabIndex = 25
        '
        'nUpdMargenInferiorUbicacion
        '
        Me.nUpdMargenInferiorUbicacion.DecimalPlaces = 6
        Me.nUpdMargenInferiorUbicacion.Location = New System.Drawing.Point(465, 135)
        Me.nUpdMargenInferiorUbicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdMargenInferiorUbicacion.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdMargenInferiorUbicacion.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdMargenInferiorUbicacion.Name = "nUpdMargenInferiorUbicacion"
        Me.nUpdMargenInferiorUbicacion.Size = New System.Drawing.Size(218, 23)
        Me.nUpdMargenInferiorUbicacion.TabIndex = 13
        '
        'nUpdMargenSuperiorUbicacion
        '
        Me.nUpdMargenSuperiorUbicacion.DecimalPlaces = 6
        Me.nUpdMargenSuperiorUbicacion.Location = New System.Drawing.Point(465, 105)
        Me.nUpdMargenSuperiorUbicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdMargenSuperiorUbicacion.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdMargenSuperiorUbicacion.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdMargenSuperiorUbicacion.Name = "nUpdMargenSuperiorUbicacion"
        Me.nUpdMargenSuperiorUbicacion.Size = New System.Drawing.Size(218, 23)
        Me.nUpdMargenSuperiorUbicacion.TabIndex = 10
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(345, 168)
        Me.Label29.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(101, 16)
        Me.Label29.TabIndex = 14
        Me.Label29.Text = "Orientación Pos:"
        '
        'nUpdMargenDerechoUbicacion
        '
        Me.nUpdMargenDerechoUbicacion.DecimalPlaces = 6
        Me.nUpdMargenDerechoUbicacion.Location = New System.Drawing.Point(465, 75)
        Me.nUpdMargenDerechoUbicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdMargenDerechoUbicacion.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdMargenDerechoUbicacion.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdMargenDerechoUbicacion.Name = "nUpdMargenDerechoUbicacion"
        Me.nUpdMargenDerechoUbicacion.Size = New System.Drawing.Size(218, 23)
        Me.nUpdMargenDerechoUbicacion.TabIndex = 6
        '
        'chkMerma
        '
        Me.chkMerma.Location = New System.Drawing.Point(227, 198)
        Me.chkMerma.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkMerma.Name = "chkMerma"
        Me.chkMerma.Properties.Caption = "Ubicación de merma"
        Me.chkMerma.Size = New System.Drawing.Size(200, 24)
        Me.chkMerma.TabIndex = 20
        '
        'nUpdMargenIzquierdoUbicacion
        '
        Me.nUpdMargenIzquierdoUbicacion.DecimalPlaces = 6
        Me.nUpdMargenIzquierdoUbicacion.Location = New System.Drawing.Point(465, 46)
        Me.nUpdMargenIzquierdoUbicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdMargenIzquierdoUbicacion.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdMargenIzquierdoUbicacion.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdMargenIzquierdoUbicacion.Name = "nUpdMargenIzquierdoUbicacion"
        Me.nUpdMargenIzquierdoUbicacion.Size = New System.Drawing.Size(218, 23)
        Me.nUpdMargenIzquierdoUbicacion.TabIndex = 2
        '
        'nUpdAnchoUbicacion
        '
        Me.nUpdAnchoUbicacion.DecimalPlaces = 6
        Me.nUpdAnchoUbicacion.Location = New System.Drawing.Point(80, 110)
        Me.nUpdAnchoUbicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdAnchoUbicacion.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdAnchoUbicacion.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdAnchoUbicacion.Name = "nUpdAnchoUbicacion"
        Me.nUpdAnchoUbicacion.Size = New System.Drawing.Size(166, 23)
        Me.nUpdAnchoUbicacion.TabIndex = 11
        '
        'nUpdLargoUbicacion
        '
        Me.nUpdLargoUbicacion.DecimalPlaces = 6
        Me.nUpdLargoUbicacion.Location = New System.Drawing.Point(80, 78)
        Me.nUpdLargoUbicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdLargoUbicacion.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdLargoUbicacion.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdLargoUbicacion.Name = "nUpdLargoUbicacion"
        Me.nUpdLargoUbicacion.Size = New System.Drawing.Size(166, 23)
        Me.nUpdLargoUbicacion.TabIndex = 7
        '
        'nUpdAltoUbicacion
        '
        Me.nUpdAltoUbicacion.DecimalPlaces = 6
        Me.nUpdAltoUbicacion.Location = New System.Drawing.Point(80, 46)
        Me.nUpdAltoUbicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdAltoUbicacion.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdAltoUbicacion.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdAltoUbicacion.Name = "nUpdAltoUbicacion"
        Me.nUpdAltoUbicacion.Size = New System.Drawing.Size(166, 23)
        Me.nUpdAltoUbicacion.TabIndex = 3
        '
        'chkDespacho
        '
        Me.chkDespacho.Location = New System.Drawing.Point(227, 236)
        Me.chkDespacho.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkDespacho.Name = "chkDespacho"
        Me.chkDespacho.Properties.Caption = "Ubicación Tránsito"
        Me.chkDespacho.Size = New System.Drawing.Size(200, 24)
        Me.chkDespacho.TabIndex = 23
        '
        'chkDañadoUbicacion
        '
        Me.chkDañadoUbicacion.Location = New System.Drawing.Point(31, 313)
        Me.chkDañadoUbicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkDañadoUbicacion.Name = "chkDañadoUbicacion"
        Me.chkDañadoUbicacion.Properties.Caption = "Dañado"
        Me.chkDañadoUbicacion.Size = New System.Drawing.Size(163, 24)
        Me.chkDañadoUbicacion.TabIndex = 21
        '
        'chkUbicacionPicking
        '
        Me.chkUbicacionPicking.Location = New System.Drawing.Point(473, 237)
        Me.chkUbicacionPicking.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkUbicacionPicking.Name = "chkUbicacionPicking"
        Me.chkUbicacionPicking.Properties.Caption = "Ubicación Picking"
        Me.chkUbicacionPicking.Size = New System.Drawing.Size(218, 24)
        Me.chkUbicacionPicking.TabIndex = 24
        '
        'chkActivoUbicacion
        '
        Me.chkActivoUbicacion.EditValue = True
        Me.chkActivoUbicacion.Location = New System.Drawing.Point(29, 149)
        Me.chkActivoUbicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkActivoUbicacion.Name = "chkActivoUbicacion"
        Me.chkActivoUbicacion.Properties.Caption = "Activo"
        Me.chkActivoUbicacion.Size = New System.Drawing.Size(186, 24)
        Me.chkActivoUbicacion.TabIndex = 16
        '
        'chkAceptaPalletUbicacion
        '
        Me.chkAceptaPalletUbicacion.Location = New System.Drawing.Point(473, 198)
        Me.chkAceptaPalletUbicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkAceptaPalletUbicacion.Name = "chkAceptaPalletUbicacion"
        Me.chkAceptaPalletUbicacion.Properties.Caption = "Acepta Pallet"
        Me.chkAceptaPalletUbicacion.Size = New System.Drawing.Size(218, 24)
        Me.chkAceptaPalletUbicacion.TabIndex = 18
        '
        'chkRecepcion
        '
        Me.chkRecepcion.Location = New System.Drawing.Point(31, 237)
        Me.chkRecepcion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkRecepcion.Name = "chkRecepcion"
        Me.chkRecepcion.Properties.Caption = "Ubicación Recepción"
        Me.chkRecepcion.Size = New System.Drawing.Size(163, 24)
        Me.chkRecepcion.TabIndex = 22
        '
        'chkBloqueadaUbicacion
        '
        Me.chkBloqueadaUbicacion.Location = New System.Drawing.Point(31, 198)
        Me.chkBloqueadaUbicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkBloqueadaUbicacion.Name = "chkBloqueadaUbicacion"
        Me.chkBloqueadaUbicacion.Properties.Caption = "Bloqueada"
        Me.chkBloqueadaUbicacion.Size = New System.Drawing.Size(163, 24)
        Me.chkBloqueadaUbicacion.TabIndex = 19
        '
        'chkSistemaUbicacion
        '
        Me.chkSistemaUbicacion.Location = New System.Drawing.Point(31, 277)
        Me.chkSistemaUbicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkSistemaUbicacion.Name = "chkSistemaUbicacion"
        Me.chkSistemaUbicacion.Properties.Caption = "Sistema"
        Me.chkSistemaUbicacion.Properties.ReadOnly = True
        Me.chkSistemaUbicacion.Size = New System.Drawing.Size(163, 24)
        Me.chkSistemaUbicacion.TabIndex = 17
        '
        'GroupControl15
        '
        Me.GroupControl15.Controls.Add(Me.grdUbicacion)
        Me.GroupControl15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl15.Location = New System.Drawing.Point(0, 407)
        Me.GroupControl15.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl15.Name = "GroupControl15"
        Me.GroupControl15.Size = New System.Drawing.Size(1784, 304)
        Me.GroupControl15.TabIndex = 2
        Me.GroupControl15.Text = "Detalle Ubicaciones"
        '
        'grdUbicacion
        '
        Me.grdUbicacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdUbicacion.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdUbicacion.Location = New System.Drawing.Point(2, 28)
        Me.grdUbicacion.MainView = Me.GridViewUbi
        Me.grdUbicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdUbicacion.Name = "grdUbicacion"
        Me.grdUbicacion.Size = New System.Drawing.Size(1780, 274)
        Me.grdUbicacion.TabIndex = 0
        Me.grdUbicacion.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewUbi})
        '
        'GridViewUbi
        '
        Me.GridViewUbi.DetailHeight = 437
        Me.GridViewUbi.GridControl = Me.grdUbicacion
        Me.GridViewUbi.Name = "GridViewUbi"
        Me.GridViewUbi.OptionsBehavior.Editable = False
        Me.GridViewUbi.OptionsEditForm.PopupEditFormWidth = 1000
        Me.GridViewUbi.OptionsView.ColumnAutoWidth = False
        Me.GridViewUbi.OptionsView.ShowAutoFilterRow = True
        Me.GridViewUbi.OptionsView.ShowFooter = True
        Me.GridViewUbi.OptionsView.ShowGroupPanel = False
        '
        'GroupControl16
        '
        Me.GroupControl16.Controls.Add(Me.tlUbicaciones)
        Me.GroupControl16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl16.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl16.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl16.Name = "GroupControl16"
        Me.GroupControl16.Size = New System.Drawing.Size(1784, 711)
        Me.GroupControl16.TabIndex = 0
        Me.GroupControl16.Text = "Bodega"
        '
        'tlUbicaciones
        '
        Me.tlUbicaciones.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlUbicaciones.Location = New System.Drawing.Point(2, 28)
        Me.tlUbicaciones.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tlUbicaciones.MinWidth = 24
        Me.tlUbicaciones.Name = "tlUbicaciones"
        Me.tlUbicaciones.OptionsBehavior.Editable = False
        Me.tlUbicaciones.OptionsBehavior.ReadOnly = True
        Me.tlUbicaciones.Size = New System.Drawing.Size(1780, 681)
        Me.tlUbicaciones.TabIndex = 0
        Me.tlUbicaciones.TreeLevelWidth = 22
        '
        'Dgrid
        '
        Me.Dgrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Dgrid.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        GridLevelNode1.RelationName = "Level1"
        Me.Dgrid.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.Dgrid.Location = New System.Drawing.Point(0, 180)
        Me.Dgrid.MainView = Me.GridView1
        Me.Dgrid.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Dgrid.Name = "Dgrid"
        Me.Dgrid.Size = New System.Drawing.Size(1784, 531)
        Me.Dgrid.TabIndex = 1
        Me.Dgrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 437
        Me.GridView1.GridControl = Me.Dgrid
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsEditForm.PopupEditFormWidth = 1000
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.Button1)
        Me.GroupControl1.Controls.Add(Me.txtTiempoActualizacionP)
        Me.GroupControl1.Controls.Add(Label5)
        Me.GroupControl1.Controls.Add(Me.txtNombreTarea)
        Me.GroupControl1.Controls.Add(Me.lnkTareas)
        Me.GroupControl1.Controls.Add(Me.txtIdTarea)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1784, 180)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Dimensiones"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(755, 124)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(170, 34)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Guardar "
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtTiempoActualizacionP
        '
        Me.txtTiempoActualizacionP.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTiempoActualizacionP.Location = New System.Drawing.Point(205, 91)
        Me.txtTiempoActualizacionP.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTiempoActualizacionP.Maximum = New Decimal(New Integer() {50000, 0, 0, 0})
        Me.txtTiempoActualizacionP.Name = "txtTiempoActualizacionP"
        Me.txtTiempoActualizacionP.Size = New System.Drawing.Size(1334, 23)
        Me.txtTiempoActualizacionP.TabIndex = 4
        Me.txtTiempoActualizacionP.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtNombreTarea
        '
        Me.txtNombreTarea.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNombreTarea.CausesValidation = False
        Me.txtNombreTarea.Location = New System.Drawing.Point(316, 50)
        Me.txtNombreTarea.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNombreTarea.Name = "txtNombreTarea"
        Me.txtNombreTarea.Properties.ReadOnly = True
        Me.txtNombreTarea.Size = New System.Drawing.Size(620, 22)
        Me.txtNombreTarea.TabIndex = 2
        '
        'lnkTareas
        '
        Me.lnkTareas.AutoSize = True
        Me.lnkTareas.Location = New System.Drawing.Point(31, 55)
        Me.lnkTareas.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkTareas.Name = "lnkTareas"
        Me.lnkTareas.Size = New System.Drawing.Size(41, 16)
        Me.lnkTareas.TabIndex = 0
        Me.lnkTareas.TabStop = True
        Me.lnkTareas.Text = "Tarea"
        '
        'txtIdTarea
        '
        Me.txtIdTarea.Location = New System.Drawing.Point(205, 50)
        Me.txtIdTarea.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdTarea.Name = "txtIdTarea"
        Me.txtIdTarea.Properties.Mask.EditMask = "n0"
        Me.txtIdTarea.Size = New System.Drawing.Size(102, 22)
        Me.txtIdTarea.TabIndex = 1
        '
        'DsBodega
        '
        Me.DsBodega.DataSetName = "DsBodega"
        Me.DsBodega.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'BodegaBindingSource
        '
        Me.BodegaBindingSource.DataMember = "Bodega"
        Me.BodegaBindingSource.DataSource = Me.DsBodega
        '
        'ControlPanelBodega
        '
        Me.ControlPanelBodega.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ControlPanelBodega.Location = New System.Drawing.Point(0, 193)
        Me.ControlPanelBodega.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ControlPanelBodega.Name = "ControlPanelBodega"
        Me.ControlPanelBodega.SelectedTabPage = Me.tabDatos
        Me.ControlPanelBodega.Size = New System.Drawing.Size(1786, 741)
        Me.ControlPanelBodega.TabIndex = 0
        Me.ControlPanelBodega.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabDatos, Me.tabDimensionesBod, Me.tabArea, Me.tabSector, Me.tabTramo, Me.TabUbicacion, Me.tabReferencia, Me.tabParametros, Me.tabUbicacionesDefecto, Me.tabListaUbicaciones})
        '
        'tabDatos
        '
        Me.tabDatos.Controls.Add(Me.grpDatosGen)
        Me.tabDatos.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tabDatos.Name = "tabDatos"
        Me.tabDatos.Size = New System.Drawing.Size(1784, 711)
        Me.tabDatos.Text = "Bodega"
        '
        'grpDatosGen
        '
        Me.grpDatosGen.Controls.Add(Me.XtraScrollableControl)
        Me.grpDatosGen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpDatosGen.Location = New System.Drawing.Point(0, 0)
        Me.grpDatosGen.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grpDatosGen.Name = "grpDatosGen"
        Me.grpDatosGen.Size = New System.Drawing.Size(1784, 711)
        Me.grpDatosGen.TabIndex = 0
        Me.grpDatosGen.Text = "Datos de Bodega"
        '
        'XtraScrollableControl
        '
        Me.XtraScrollableControl.Controls.Add(Me.gpSmtp)
        Me.XtraScrollableControl.Controls.Add(Me.gcCentroCosto)
        Me.XtraScrollableControl.Controls.Add(Me.cmdRutaCDN)
        Me.XtraScrollableControl.Controls.Add(Me.txtRutaCDN)
        Me.XtraScrollableControl.Controls.Add(Me.lblRutaCDN)
        Me.XtraScrollableControl.Controls.Add(Me.GrpTIpoTransaccion)
        Me.XtraScrollableControl.Controls.Add(Me.cmbTamañoEtiquetaUbicacionDefecto)
        Me.XtraScrollableControl.Controls.Add(Me.lblTamañoEtiquetaUbicacionDefecto)
        Me.XtraScrollableControl.Controls.Add(lblCodigoBodegaERP)
        Me.XtraScrollableControl.Controls.Add(Me.txtCodigoBodegaERP)
        Me.XtraScrollableControl.Controls.Add(Me.lblMensajeUbicacionesDef)
        Me.XtraScrollableControl.Controls.Add(Me.cmbEmpresa)
        Me.XtraScrollableControl.Controls.Add(Me.cmbPais)
        Me.XtraScrollableControl.Controls.Add(Label1)
        Me.XtraScrollableControl.Controls.Add(Me.EncargadoTextEdit)
        Me.XtraScrollableControl.Controls.Add(Me.chkActivo)
        Me.XtraScrollableControl.Controls.Add(ActivoLabel)
        Me.XtraScrollableControl.Controls.Add(EncargadoLabel)
        Me.XtraScrollableControl.Controls.Add(Me.EmailTextEdit)
        Me.XtraScrollableControl.Controls.Add(EmailLabel)
        Me.XtraScrollableControl.Controls.Add(Me.TelefonoTextEdit)
        Me.XtraScrollableControl.Controls.Add(TelefonoLabel)
        Me.XtraScrollableControl.Controls.Add(Me.DireccionTextEdit)
        Me.XtraScrollableControl.Controls.Add(DireccionLabel)
        Me.XtraScrollableControl.Controls.Add(Me.txtNombre)
        Me.XtraScrollableControl.Controls.Add(NombreLabel)
        Me.XtraScrollableControl.Controls.Add(IdBodegaLabel)
        Me.XtraScrollableControl.Controls.Add(Label3)
        Me.XtraScrollableControl.Controls.Add(IdEmpresaLabel)
        Me.XtraScrollableControl.Controls.Add(Me.txtNombreComercial)
        Me.XtraScrollableControl.Controls.Add(Me.txtCodigoBarra)
        Me.XtraScrollableControl.Controls.Add(Me.txtCodigo)
        Me.XtraScrollableControl.Controls.Add(Label2)
        Me.XtraScrollableControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraScrollableControl.Location = New System.Drawing.Point(2, 28)
        Me.XtraScrollableControl.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.XtraScrollableControl.Name = "XtraScrollableControl"
        Me.XtraScrollableControl.Size = New System.Drawing.Size(1780, 681)
        Me.XtraScrollableControl.TabIndex = 0
        '
        'gpSmtp
        '
        Me.gpSmtp.Controls.Add(Me.txtPassword)
        Me.gpSmtp.Controls.Add(Label92)
        Me.gpSmtp.Controls.Add(Me.txtUsuario)
        Me.gpSmtp.Controls.Add(Me.txtPuerto)
        Me.gpSmtp.Controls.Add(Me.txtServidor)
        Me.gpSmtp.Controls.Add(Label91)
        Me.gpSmtp.Controls.Add(Me.chkSsl)
        Me.gpSmtp.Controls.Add(Label88)
        Me.gpSmtp.Controls.Add(Label89)
        Me.gpSmtp.Controls.Add(Label90)
        Me.gpSmtp.CustomHeaderButtons.AddRange(New DevExpress.XtraEditors.ButtonPanel.IBaseButton() {New DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Limpiar", True, ButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, True, Nothing, True, False, True, Nothing, -1)})
        Me.gpSmtp.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText
        Me.gpSmtp.Location = New System.Drawing.Point(687, 320)
        Me.gpSmtp.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.gpSmtp.Name = "gpSmtp"
        Me.gpSmtp.Size = New System.Drawing.Size(566, 225)
        Me.gpSmtp.TabIndex = 57
        Me.gpSmtp.Text = "Configuración smtp"
        Me.gpSmtp.Visible = False
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(107, 179)
        Me.txtPassword.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(436, 22)
        Me.txtPassword.TabIndex = 61
        '
        'txtUsuario
        '
        Me.txtUsuario.Location = New System.Drawing.Point(107, 147)
        Me.txtUsuario.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.Size = New System.Drawing.Size(436, 22)
        Me.txtUsuario.TabIndex = 59
        '
        'txtPuerto
        '
        Me.txtPuerto.EditValue = CType(0, Short)
        Me.txtPuerto.Location = New System.Drawing.Point(107, 77)
        Me.txtPuerto.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPuerto.Name = "txtPuerto"
        Me.txtPuerto.Size = New System.Drawing.Size(436, 22)
        Me.txtPuerto.TabIndex = 58
        '
        'txtServidor
        '
        Me.txtServidor.Location = New System.Drawing.Point(107, 43)
        Me.txtServidor.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtServidor.Name = "txtServidor"
        Me.txtServidor.Size = New System.Drawing.Size(436, 22)
        Me.txtServidor.TabIndex = 57
        '
        'chkSsl
        '
        Me.chkSsl.EditValue = True
        Me.chkSsl.Location = New System.Drawing.Point(107, 109)
        Me.chkSsl.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkSsl.Name = "chkSsl"
        Me.chkSsl.Properties.Caption = ""
        Me.chkSsl.Size = New System.Drawing.Size(264, 24)
        Me.chkSsl.TabIndex = 55
        '
        'gcCentroCosto
        '
        Me.gcCentroCosto.Controls.Add(Me.cmbCentroCostoDepERP)
        Me.gcCentroCosto.Controls.Add(Me.cmbCentroCostoDirERP)
        Me.gcCentroCosto.Controls.Add(Me.cmbCentroCostoERP)
        Me.gcCentroCosto.Controls.Add(Label83)
        Me.gcCentroCosto.Controls.Add(Label84)
        Me.gcCentroCosto.Controls.Add(Label85)
        Me.gcCentroCosto.Location = New System.Drawing.Point(687, 170)
        Me.gcCentroCosto.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.gcCentroCosto.Name = "gcCentroCosto"
        Me.gcCentroCosto.Size = New System.Drawing.Size(566, 144)
        Me.gcCentroCosto.TabIndex = 56
        Me.gcCentroCosto.Text = "Centro Costo"
        Me.gcCentroCosto.Visible = False
        '
        'cmbCentroCostoDepERP
        '
        Me.cmbCentroCostoDepERP.Location = New System.Drawing.Point(102, 110)
        Me.cmbCentroCostoDepERP.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCentroCostoDepERP.Name = "cmbCentroCostoDepERP"
        Me.cmbCentroCostoDepERP.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCentroCostoDepERP.Properties.NullText = ""
        Me.cmbCentroCostoDepERP.Size = New System.Drawing.Size(458, 22)
        Me.cmbCentroCostoDepERP.TabIndex = 54
        '
        'cmbCentroCostoDirERP
        '
        Me.cmbCentroCostoDirERP.Location = New System.Drawing.Point(102, 74)
        Me.cmbCentroCostoDirERP.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCentroCostoDirERP.Name = "cmbCentroCostoDirERP"
        Me.cmbCentroCostoDirERP.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCentroCostoDirERP.Properties.NullText = ""
        Me.cmbCentroCostoDirERP.Size = New System.Drawing.Size(458, 22)
        Me.cmbCentroCostoDirERP.TabIndex = 53
        '
        'cmbCentroCostoERP
        '
        Me.cmbCentroCostoERP.Location = New System.Drawing.Point(102, 42)
        Me.cmbCentroCostoERP.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCentroCostoERP.Name = "cmbCentroCostoERP"
        Me.cmbCentroCostoERP.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCentroCostoERP.Properties.NullText = ""
        Me.cmbCentroCostoERP.Size = New System.Drawing.Size(458, 22)
        Me.cmbCentroCostoERP.TabIndex = 52
        '
        'cmdRutaCDN
        '
        Me.cmdRutaCDN.AutoSize = True
        Me.cmdRutaCDN.ImageOptions.SvgImageSize = New System.Drawing.Size(15, 15)
        Me.cmdRutaCDN.Location = New System.Drawing.Point(1234, 22)
        Me.cmdRutaCDN.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.cmdRutaCDN.Name = "cmdRutaCDN"
        Me.cmdRutaCDN.Size = New System.Drawing.Size(19, 27)
        Me.cmdRutaCDN.TabIndex = 50
        Me.cmdRutaCDN.Text = "..."
        '
        'txtRutaCDN
        '
        Me.txtRutaCDN.Location = New System.Drawing.Point(758, 25)
        Me.txtRutaCDN.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtRutaCDN.Name = "txtRutaCDN"
        Me.txtRutaCDN.Size = New System.Drawing.Size(472, 22)
        Me.txtRutaCDN.TabIndex = 49
        '
        'GrpTIpoTransaccion
        '
        Me.GrpTIpoTransaccion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GrpTIpoTransaccion.Controls.Add(Me.txtNombreDocumentoSalida)
        Me.GrpTIpoTransaccion.Controls.Add(Me.lnkTipoSalida)
        Me.GrpTIpoTransaccion.Controls.Add(Me.txtIdTipoDocumentoSalida)
        Me.GrpTIpoTransaccion.Controls.Add(Me.txtDescripcionTR)
        Me.GrpTIpoTransaccion.Controls.Add(Me.lnkTipoT)
        Me.GrpTIpoTransaccion.Controls.Add(Me.txtIdTipoTR)
        Me.GrpTIpoTransaccion.Location = New System.Drawing.Point(687, 64)
        Me.GrpTIpoTransaccion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GrpTIpoTransaccion.Name = "GrpTIpoTransaccion"
        Me.GrpTIpoTransaccion.Size = New System.Drawing.Size(566, 98)
        Me.GrpTIpoTransaccion.TabIndex = 24
        Me.GrpTIpoTransaccion.Text = "Tipo Ingreso por Defecto"
        '
        'txtNombreDocumentoSalida
        '
        Me.txtNombreDocumentoSalida.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNombreDocumentoSalida.Location = New System.Drawing.Point(236, 71)
        Me.txtNombreDocumentoSalida.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNombreDocumentoSalida.Name = "txtNombreDocumentoSalida"
        Me.txtNombreDocumentoSalida.Properties.ReadOnly = True
        Me.txtNombreDocumentoSalida.Size = New System.Drawing.Size(324, 22)
        Me.txtNombreDocumentoSalida.TabIndex = 5
        '
        'lnkTipoSalida
        '
        Me.lnkTipoSalida.AutoSize = True
        Me.lnkTipoSalida.Location = New System.Drawing.Point(7, 73)
        Me.lnkTipoSalida.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkTipoSalida.Name = "lnkTipoSalida"
        Me.lnkTipoSalida.Size = New System.Drawing.Size(71, 16)
        Me.lnkTipoSalida.TabIndex = 3
        Me.lnkTipoSalida.TabStop = True
        Me.lnkTipoSalida.Text = "Tipo Salida"
        '
        'txtIdTipoDocumentoSalida
        '
        Me.txtIdTipoDocumentoSalida.Location = New System.Drawing.Point(92, 71)
        Me.txtIdTipoDocumentoSalida.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdTipoDocumentoSalida.Name = "txtIdTipoDocumentoSalida"
        Me.txtIdTipoDocumentoSalida.Size = New System.Drawing.Size(135, 22)
        Me.txtIdTipoDocumentoSalida.TabIndex = 4
        '
        'txtDescripcionTR
        '
        Me.txtDescripcionTR.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescripcionTR.Location = New System.Drawing.Point(236, 33)
        Me.txtDescripcionTR.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDescripcionTR.Name = "txtDescripcionTR"
        Me.txtDescripcionTR.Properties.ReadOnly = True
        Me.txtDescripcionTR.Size = New System.Drawing.Size(324, 22)
        Me.txtDescripcionTR.TabIndex = 2
        '
        'lnkTipoT
        '
        Me.lnkTipoT.AutoSize = True
        Me.lnkTipoT.Location = New System.Drawing.Point(7, 36)
        Me.lnkTipoT.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkTipoT.Name = "lnkTipoT"
        Me.lnkTipoT.Size = New System.Drawing.Size(79, 16)
        Me.lnkTipoT.TabIndex = 0
        Me.lnkTipoT.TabStop = True
        Me.lnkTipoT.Text = "Tipo Ingreso"
        '
        'txtIdTipoTR
        '
        Me.txtIdTipoTR.Location = New System.Drawing.Point(92, 33)
        Me.txtIdTipoTR.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdTipoTR.Name = "txtIdTipoTR"
        Me.txtIdTipoTR.Size = New System.Drawing.Size(135, 22)
        Me.txtIdTipoTR.TabIndex = 1
        '
        'cmbTamañoEtiquetaUbicacionDefecto
        '
        Me.cmbTamañoEtiquetaUbicacionDefecto.Location = New System.Drawing.Point(200, 378)
        Me.cmbTamañoEtiquetaUbicacionDefecto.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbTamañoEtiquetaUbicacionDefecto.Name = "cmbTamañoEtiquetaUbicacionDefecto"
        Me.cmbTamañoEtiquetaUbicacionDefecto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTamañoEtiquetaUbicacionDefecto.Properties.NullText = ""
        Me.cmbTamañoEtiquetaUbicacionDefecto.Size = New System.Drawing.Size(456, 22)
        Me.cmbTamañoEtiquetaUbicacionDefecto.TabIndex = 47
        '
        'lblTamañoEtiquetaUbicacionDefecto
        '
        Me.lblTamañoEtiquetaUbicacionDefecto.AutoSize = True
        Me.lblTamañoEtiquetaUbicacionDefecto.Location = New System.Drawing.Point(15, 384)
        Me.lblTamañoEtiquetaUbicacionDefecto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTamañoEtiquetaUbicacionDefecto.Name = "lblTamañoEtiquetaUbicacionDefecto"
        Me.lblTamañoEtiquetaUbicacionDefecto.Size = New System.Drawing.Size(179, 16)
        Me.lblTamañoEtiquetaUbicacionDefecto.TabIndex = 46
        Me.lblTamañoEtiquetaUbicacionDefecto.Text = "Tamaño etiqueta de ubicación"
        '
        'txtCodigoBodegaERP
        '
        Me.txtCodigoBodegaERP.Location = New System.Drawing.Point(200, 154)
        Me.txtCodigoBodegaERP.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCodigoBodegaERP.Name = "txtCodigoBodegaERP"
        Me.txtCodigoBodegaERP.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.txtCodigoBodegaERP.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.txtCodigoBodegaERP.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.txtCodigoBodegaERP.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtCodigoBodegaERP.Size = New System.Drawing.Size(456, 22)
        Me.txtCodigoBodegaERP.TabIndex = 27
        '
        'lblMensajeUbicacionesDef
        '
        Me.lblMensajeUbicacionesDef.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblMensajeUbicacionesDef.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMensajeUbicacionesDef.ForeColor = System.Drawing.Color.Red
        Me.lblMensajeUbicacionesDef.Location = New System.Drawing.Point(0, 633)
        Me.lblMensajeUbicacionesDef.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMensajeUbicacionesDef.Name = "lblMensajeUbicacionesDef"
        Me.lblMensajeUbicacionesDef.Size = New System.Drawing.Size(1780, 48)
        Me.lblMensajeUbicacionesDef.TabIndex = 25
        Me.lblMensajeUbicacionesDef.Text = "-"
        '
        'cmbEmpresa
        '
        Me.cmbEmpresa.Location = New System.Drawing.Point(200, 58)
        Me.cmbEmpresa.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbEmpresa.MenuManager = Me.mnu
        Me.cmbEmpresa.Name = "cmbEmpresa"
        Me.cmbEmpresa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEmpresa.Properties.NullText = ""
        Me.cmbEmpresa.Size = New System.Drawing.Size(456, 22)
        Me.cmbEmpresa.TabIndex = 3
        '
        'cmbPais
        '
        Me.cmbPais.Location = New System.Drawing.Point(200, 25)
        Me.cmbPais.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPais.MenuManager = Me.mnu
        Me.cmbPais.Name = "cmbPais"
        Me.cmbPais.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPais.Properties.NullText = ""
        Me.cmbPais.Size = New System.Drawing.Size(456, 22)
        Me.cmbPais.TabIndex = 1
        '
        'EncargadoTextEdit
        '
        Me.EncargadoTextEdit.Location = New System.Drawing.Point(200, 346)
        Me.EncargadoTextEdit.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.EncargadoTextEdit.Name = "EncargadoTextEdit"
        Me.EncargadoTextEdit.Size = New System.Drawing.Size(456, 22)
        Me.EncargadoTextEdit.TabIndex = 19
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(200, 410)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(264, 24)
        Me.chkActivo.TabIndex = 21
        '
        'EmailTextEdit
        '
        Me.EmailTextEdit.Location = New System.Drawing.Point(200, 314)
        Me.EmailTextEdit.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.EmailTextEdit.Name = "EmailTextEdit"
        Me.EmailTextEdit.Size = New System.Drawing.Size(456, 22)
        Me.EmailTextEdit.TabIndex = 17
        '
        'TelefonoTextEdit
        '
        Me.TelefonoTextEdit.Location = New System.Drawing.Point(200, 282)
        Me.TelefonoTextEdit.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TelefonoTextEdit.Name = "TelefonoTextEdit"
        Me.TelefonoTextEdit.Size = New System.Drawing.Size(456, 22)
        Me.TelefonoTextEdit.TabIndex = 15
        '
        'DireccionTextEdit
        '
        Me.DireccionTextEdit.Location = New System.Drawing.Point(200, 250)
        Me.DireccionTextEdit.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DireccionTextEdit.Name = "DireccionTextEdit"
        Me.DireccionTextEdit.Size = New System.Drawing.Size(456, 22)
        Me.DireccionTextEdit.TabIndex = 13
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(200, 186)
        Me.txtNombre.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(456, 22)
        Me.txtNombre.TabIndex = 9
        '
        'txtNombreComercial
        '
        Me.txtNombreComercial.Location = New System.Drawing.Point(200, 218)
        Me.txtNombreComercial.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNombreComercial.Name = "txtNombreComercial"
        Me.txtNombreComercial.Size = New System.Drawing.Size(456, 22)
        Me.txtNombreComercial.TabIndex = 11
        '
        'txtCodigoBarra
        '
        Me.txtCodigoBarra.Location = New System.Drawing.Point(200, 122)
        Me.txtCodigoBarra.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCodigoBarra.Name = "txtCodigoBarra"
        Me.txtCodigoBarra.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.txtCodigoBarra.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.txtCodigoBarra.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.txtCodigoBarra.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtCodigoBarra.Size = New System.Drawing.Size(456, 22)
        Me.txtCodigoBarra.TabIndex = 7
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(200, 90)
        Me.txtCodigo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.txtCodigo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.txtCodigo.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.txtCodigo.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtCodigo.Size = New System.Drawing.Size(456, 22)
        Me.txtCodigo.TabIndex = 5
        '
        'tabDimensionesBod
        '
        Me.tabDimensionesBod.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.tabDimensionesBod.Appearance.PageClient.Options.UseBackColor = True
        Me.tabDimensionesBod.Controls.Add(Me.GroupControl18)
        Me.tabDimensionesBod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tabDimensionesBod.Name = "tabDimensionesBod"
        Me.tabDimensionesBod.Size = New System.Drawing.Size(1784, 711)
        Me.tabDimensionesBod.Text = "Dimensiones de bodega"
        '
        'GroupControl18
        '
        Me.GroupControl18.Controls.Add(Me.Label27)
        Me.GroupControl18.Controls.Add(Me.txtZoom)
        Me.GroupControl18.Controls.Add(lblAlto)
        Me.GroupControl18.Controls.Add(lblLongitud)
        Me.GroupControl18.Controls.Add(Me.txtAlto)
        Me.GroupControl18.Controls.Add(Me.txtCoordenadaY)
        Me.GroupControl18.Controls.Add(Me.txtLargo)
        Me.GroupControl18.Controls.Add(lblLatitud)
        Me.GroupControl18.Controls.Add(Me.txtCoordenadaX)
        Me.GroupControl18.Controls.Add(lblLargo)
        Me.GroupControl18.Controls.Add(lblAncho)
        Me.GroupControl18.Controls.Add(Me.txtAncho)
        Me.GroupControl18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl18.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl18.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl18.Name = "GroupControl18"
        Me.GroupControl18.Size = New System.Drawing.Size(1784, 711)
        Me.GroupControl18.TabIndex = 0
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(41, 133)
        Me.Label27.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(44, 16)
        Me.Label27.TabIndex = 4
        Me.Label27.Text = "Zoom:"
        '
        'txtZoom
        '
        Me.txtZoom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtZoom.BackColor = System.Drawing.Color.MistyRose
        Me.txtZoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtZoom.DecimalPlaces = 2
        Me.txtZoom.Location = New System.Drawing.Point(121, 128)
        Me.txtZoom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtZoom.Name = "txtZoom"
        Me.txtZoom.Size = New System.Drawing.Size(338, 23)
        Me.txtZoom.TabIndex = 5
        Me.txtZoom.Value = New Decimal(New Integer() {20, 0, 0, 0})
        '
        'txtAlto
        '
        Me.txtAlto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAlto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAlto.DecimalPlaces = 6
        Me.txtAlto.Location = New System.Drawing.Point(121, 161)
        Me.txtAlto.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAlto.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtAlto.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtAlto.Name = "txtAlto"
        Me.txtAlto.Size = New System.Drawing.Size(338, 23)
        Me.txtAlto.TabIndex = 7
        '
        'txtCoordenadaY
        '
        Me.txtCoordenadaY.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCoordenadaY.Location = New System.Drawing.Point(121, 226)
        Me.txtCoordenadaY.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCoordenadaY.Name = "txtCoordenadaY"
        Me.txtCoordenadaY.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.txtCoordenadaY.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.txtCoordenadaY.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.txtCoordenadaY.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtCoordenadaY.Size = New System.Drawing.Size(338, 22)
        Me.txtCoordenadaY.TabIndex = 11
        '
        'txtLargo
        '
        Me.txtLargo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLargo.BackColor = System.Drawing.Color.MistyRose
        Me.txtLargo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLargo.DecimalPlaces = 6
        Me.txtLargo.Location = New System.Drawing.Point(121, 62)
        Me.txtLargo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtLargo.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtLargo.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtLargo.Name = "txtLargo"
        Me.txtLargo.Size = New System.Drawing.Size(338, 23)
        Me.txtLargo.TabIndex = 1
        '
        'txtCoordenadaX
        '
        Me.txtCoordenadaX.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCoordenadaX.Location = New System.Drawing.Point(121, 194)
        Me.txtCoordenadaX.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCoordenadaX.Name = "txtCoordenadaX"
        Me.txtCoordenadaX.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.txtCoordenadaX.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.txtCoordenadaX.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.txtCoordenadaX.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtCoordenadaX.Size = New System.Drawing.Size(338, 22)
        Me.txtCoordenadaX.TabIndex = 9
        '
        'txtAncho
        '
        Me.txtAncho.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAncho.BackColor = System.Drawing.Color.MistyRose
        Me.txtAncho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAncho.DecimalPlaces = 6
        Me.txtAncho.Location = New System.Drawing.Point(121, 95)
        Me.txtAncho.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAncho.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtAncho.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtAncho.Name = "txtAncho"
        Me.txtAncho.Size = New System.Drawing.Size(338, 23)
        Me.txtAncho.TabIndex = 3
        '
        'tabArea
        '
        Me.tabArea.Controls.Add(Me.SplitContainer1)
        Me.tabArea.Controls.Add(Me.ToolStripPR)
        Me.tabArea.Controls.Add(Me.GroupControl17)
        Me.tabArea.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tabArea.Name = "tabArea"
        Me.tabArea.Size = New System.Drawing.Size(1784, 711)
        Me.tabArea.Text = "Areas"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 27)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupControl4)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupControl6)
        Me.SplitContainer1.Size = New System.Drawing.Size(1784, 244)
        Me.SplitContainer1.SplitterDistance = 716
        Me.SplitContainer1.SplitterWidth = 6
        Me.SplitContainer1.TabIndex = 1
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.txtNombreUbicacionRecepcionArea)
        Me.GroupControl4.Controls.Add(Me.lnkUbicacionRecepcionArea)
        Me.GroupControl4.Controls.Add(Me.txtUbicacionRecepcionArea)
        Me.GroupControl4.Controls.Add(lblIdArea)
        Me.GroupControl4.Controls.Add(lblGrupo)
        Me.GroupControl4.Controls.Add(Me.txtGrupoArea)
        Me.GroupControl4.Controls.Add(Me.txtIdArea)
        Me.GroupControl4.Controls.Add(Me.chkActivoAreaBodega)
        Me.GroupControl4.Controls.Add(lblCodigoArea)
        Me.GroupControl4.Controls.Add(Me.txtCodigoAreaBodega)
        Me.GroupControl4.Controls.Add(Me.chkSistemaAreaBodega)
        Me.GroupControl4.Controls.Add(lblDescripcionAreaBodega)
        Me.GroupControl4.Controls.Add(Me.txtDescripcionAreaBodega)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl4.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(716, 244)
        Me.GroupControl4.TabIndex = 0
        Me.GroupControl4.Text = "Datos de Area"
        '
        'txtNombreUbicacionRecepcionArea
        '
        Me.txtNombreUbicacionRecepcionArea.Location = New System.Drawing.Point(359, 178)
        Me.txtNombreUbicacionRecepcionArea.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNombreUbicacionRecepcionArea.Name = "txtNombreUbicacionRecepcionArea"
        Me.txtNombreUbicacionRecepcionArea.Properties.ReadOnly = True
        Me.txtNombreUbicacionRecepcionArea.Size = New System.Drawing.Size(214, 22)
        Me.txtNombreUbicacionRecepcionArea.TabIndex = 18
        '
        'lnkUbicacionRecepcionArea
        '
        Me.lnkUbicacionRecepcionArea.AutoSize = True
        Me.lnkUbicacionRecepcionArea.Location = New System.Drawing.Point(31, 182)
        Me.lnkUbicacionRecepcionArea.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkUbicacionRecepcionArea.Name = "lnkUbicacionRecepcionArea"
        Me.lnkUbicacionRecepcionArea.Size = New System.Drawing.Size(141, 16)
        Me.lnkUbicacionRecepcionArea.TabIndex = 17
        Me.lnkUbicacionRecepcionArea.TabStop = True
        Me.lnkUbicacionRecepcionArea.Text = "Ubicación de Recepción"
        '
        'txtUbicacionRecepcionArea
        '
        Me.txtUbicacionRecepcionArea.Location = New System.Drawing.Point(185, 178)
        Me.txtUbicacionRecepcionArea.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtUbicacionRecepcionArea.Name = "txtUbicacionRecepcionArea"
        Me.txtUbicacionRecepcionArea.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtUbicacionRecepcionArea.Properties.MaxLength = 50
        Me.txtUbicacionRecepcionArea.Size = New System.Drawing.Size(167, 22)
        Me.txtUbicacionRecepcionArea.TabIndex = 16
        '
        'txtGrupoArea
        '
        Me.txtGrupoArea.Location = New System.Drawing.Point(185, 145)
        Me.txtGrupoArea.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtGrupoArea.Name = "txtGrupoArea"
        Me.txtGrupoArea.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtGrupoArea.Properties.MaxLength = 50
        Me.txtGrupoArea.Size = New System.Drawing.Size(388, 22)
        Me.txtGrupoArea.TabIndex = 13
        '
        'txtIdArea
        '
        Me.txtIdArea.Location = New System.Drawing.Point(185, 46)
        Me.txtIdArea.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdArea.Name = "txtIdArea"
        Me.txtIdArea.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtIdArea.Properties.MaxLength = 50
        Me.txtIdArea.Properties.ReadOnly = True
        Me.txtIdArea.Size = New System.Drawing.Size(388, 22)
        Me.txtIdArea.TabIndex = 15
        '
        'chkActivoAreaBodega
        '
        Me.chkActivoAreaBodega.EditValue = True
        Me.chkActivoAreaBodega.Location = New System.Drawing.Point(359, 210)
        Me.chkActivoAreaBodega.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkActivoAreaBodega.Name = "chkActivoAreaBodega"
        Me.chkActivoAreaBodega.Properties.Caption = "Activo"
        Me.chkActivoAreaBodega.Size = New System.Drawing.Size(98, 24)
        Me.chkActivoAreaBodega.TabIndex = 10
        '
        'txtCodigoAreaBodega
        '
        Me.txtCodigoAreaBodega.Location = New System.Drawing.Point(185, 79)
        Me.txtCodigoAreaBodega.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCodigoAreaBodega.Name = "txtCodigoAreaBodega"
        Me.txtCodigoAreaBodega.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCodigoAreaBodega.Properties.MaxLength = 50
        Me.txtCodigoAreaBodega.Size = New System.Drawing.Size(388, 22)
        Me.txtCodigoAreaBodega.TabIndex = 1
        '
        'chkSistemaAreaBodega
        '
        Me.chkSistemaAreaBodega.Location = New System.Drawing.Point(185, 210)
        Me.chkSistemaAreaBodega.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkSistemaAreaBodega.Name = "chkSistemaAreaBodega"
        Me.chkSistemaAreaBodega.Properties.Caption = "Sistema"
        Me.chkSistemaAreaBodega.Size = New System.Drawing.Size(98, 24)
        Me.chkSistemaAreaBodega.TabIndex = 11
        '
        'txtDescripcionAreaBodega
        '
        Me.txtDescripcionAreaBodega.Location = New System.Drawing.Point(185, 112)
        Me.txtDescripcionAreaBodega.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDescripcionAreaBodega.Name = "txtDescripcionAreaBodega"
        Me.txtDescripcionAreaBodega.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtDescripcionAreaBodega.Properties.MaxLength = 50
        Me.txtDescripcionAreaBodega.Size = New System.Drawing.Size(388, 22)
        Me.txtDescripcionAreaBodega.TabIndex = 3
        '
        'GroupControl6
        '
        Me.GroupControl6.Controls.Add(Me.nUpdMargenInferior)
        Me.GroupControl6.Controls.Add(Me.nUpdMargenSuperior)
        Me.GroupControl6.Controls.Add(Me.nUpdMargenDerecho)
        Me.GroupControl6.Controls.Add(Me.nUpdMargenIzquierdo)
        Me.GroupControl6.Controls.Add(Label11)
        Me.GroupControl6.Controls.Add(Label12)
        Me.GroupControl6.Controls.Add(Label10)
        Me.GroupControl6.Controls.Add(Label9)
        Me.GroupControl6.Controls.Add(Me.nUpdAlto)
        Me.GroupControl6.Controls.Add(Label8)
        Me.GroupControl6.Controls.Add(Me.nUpdAncho)
        Me.GroupControl6.Controls.Add(Label7)
        Me.GroupControl6.Controls.Add(Me.nUpdLargo)
        Me.GroupControl6.Controls.Add(Label6)
        Me.GroupControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl6.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl6.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl6.Name = "GroupControl6"
        Me.GroupControl6.Size = New System.Drawing.Size(1062, 244)
        Me.GroupControl6.TabIndex = 0
        Me.GroupControl6.Text = "Dimensiones"
        '
        'nUpdMargenInferior
        '
        Me.nUpdMargenInferior.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nUpdMargenInferior.DecimalPlaces = 6
        Me.nUpdMargenInferior.Location = New System.Drawing.Point(511, 169)
        Me.nUpdMargenInferior.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdMargenInferior.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdMargenInferior.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdMargenInferior.Name = "nUpdMargenInferior"
        Me.nUpdMargenInferior.Size = New System.Drawing.Size(233, 23)
        Me.nUpdMargenInferior.TabIndex = 7
        '
        'nUpdMargenSuperior
        '
        Me.nUpdMargenSuperior.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nUpdMargenSuperior.DecimalPlaces = 6
        Me.nUpdMargenSuperior.Location = New System.Drawing.Point(511, 137)
        Me.nUpdMargenSuperior.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdMargenSuperior.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdMargenSuperior.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdMargenSuperior.Name = "nUpdMargenSuperior"
        Me.nUpdMargenSuperior.Size = New System.Drawing.Size(233, 23)
        Me.nUpdMargenSuperior.TabIndex = 5
        '
        'nUpdMargenDerecho
        '
        Me.nUpdMargenDerecho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nUpdMargenDerecho.DecimalPlaces = 6
        Me.nUpdMargenDerecho.Location = New System.Drawing.Point(511, 105)
        Me.nUpdMargenDerecho.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdMargenDerecho.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdMargenDerecho.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdMargenDerecho.Name = "nUpdMargenDerecho"
        Me.nUpdMargenDerecho.Size = New System.Drawing.Size(233, 23)
        Me.nUpdMargenDerecho.TabIndex = 3
        '
        'nUpdMargenIzquierdo
        '
        Me.nUpdMargenIzquierdo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nUpdMargenIzquierdo.DecimalPlaces = 6
        Me.nUpdMargenIzquierdo.Location = New System.Drawing.Point(511, 69)
        Me.nUpdMargenIzquierdo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdMargenIzquierdo.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdMargenIzquierdo.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdMargenIzquierdo.Name = "nUpdMargenIzquierdo"
        Me.nUpdMargenIzquierdo.Size = New System.Drawing.Size(233, 23)
        Me.nUpdMargenIzquierdo.TabIndex = 1
        '
        'nUpdAlto
        '
        Me.nUpdAlto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nUpdAlto.DecimalPlaces = 6
        Me.nUpdAlto.Location = New System.Drawing.Point(88, 76)
        Me.nUpdAlto.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdAlto.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdAlto.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdAlto.Name = "nUpdAlto"
        Me.nUpdAlto.Size = New System.Drawing.Size(224, 23)
        Me.nUpdAlto.TabIndex = 5
        '
        'nUpdAncho
        '
        Me.nUpdAncho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nUpdAncho.DecimalPlaces = 6
        Me.nUpdAncho.Location = New System.Drawing.Point(88, 133)
        Me.nUpdAncho.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdAncho.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdAncho.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdAncho.Name = "nUpdAncho"
        Me.nUpdAncho.Size = New System.Drawing.Size(224, 23)
        Me.nUpdAncho.TabIndex = 9
        '
        'nUpdLargo
        '
        Me.nUpdLargo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nUpdLargo.DecimalPlaces = 6
        Me.nUpdLargo.Location = New System.Drawing.Point(88, 105)
        Me.nUpdLargo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdLargo.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdLargo.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdLargo.Name = "nUpdLargo"
        Me.nUpdLargo.Size = New System.Drawing.Size(224, 23)
        Me.nUpdLargo.TabIndex = 7
        '
        'ToolStripPR
        '
        Me.ToolStripPR.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStripPR.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdNuevaArea, Me.cmdGuardarArea})
        Me.ToolStripPR.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripPR.Name = "ToolStripPR"
        Me.ToolStripPR.Size = New System.Drawing.Size(1784, 27)
        Me.ToolStripPR.TabIndex = 0
        Me.ToolStripPR.Text = "ToolStrip2"
        '
        'cmdNuevaArea
        '
        Me.cmdNuevaArea.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdNuevaArea.Name = "cmdNuevaArea"
        Me.cmdNuevaArea.Size = New System.Drawing.Size(56, 24)
        Me.cmdNuevaArea.Text = "Nuevo"
        '
        'cmdGuardarArea
        '
        Me.cmdGuardarArea.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdGuardarArea.Name = "cmdGuardarArea"
        Me.cmdGuardarArea.Size = New System.Drawing.Size(66, 24)
        Me.cmdGuardarArea.Text = "Guardar"
        '
        'GroupControl17
        '
        Me.GroupControl17.Controls.Add(Me.grdAreaBodega)
        Me.GroupControl17.Controls.Add(Me.chkAreasBodegaActivos)
        Me.GroupControl17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl17.Location = New System.Drawing.Point(0, 271)
        Me.GroupControl17.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl17.Name = "GroupControl17"
        Me.GroupControl17.Size = New System.Drawing.Size(1784, 440)
        Me.GroupControl17.TabIndex = 2
        Me.GroupControl17.Text = "Detalle Areas"
        '
        'grdAreaBodega
        '
        Me.grdAreaBodega.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdAreaBodega.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdAreaBodega.Location = New System.Drawing.Point(2, 28)
        Me.grdAreaBodega.MainView = Me.GridViewArea
        Me.grdAreaBodega.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdAreaBodega.Name = "grdAreaBodega"
        Me.grdAreaBodega.Size = New System.Drawing.Size(1780, 386)
        Me.grdAreaBodega.TabIndex = 0
        Me.grdAreaBodega.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewArea, Me.GridView3})
        '
        'GridViewArea
        '
        Me.GridViewArea.DetailHeight = 437
        Me.GridViewArea.GridControl = Me.grdAreaBodega
        Me.GridViewArea.Name = "GridViewArea"
        Me.GridViewArea.OptionsBehavior.Editable = False
        Me.GridViewArea.OptionsEditForm.PopupEditFormWidth = 1000
        Me.GridViewArea.OptionsView.ShowFooter = True
        Me.GridViewArea.OptionsView.ShowGroupPanel = False
        '
        'GridView3
        '
        Me.GridView3.DetailHeight = 437
        Me.GridView3.GridControl = Me.grdAreaBodega
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsEditForm.PopupEditFormWidth = 1000
        '
        'chkAreasBodegaActivos
        '
        Me.chkAreasBodegaActivos.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.chkAreasBodegaActivos.EditValue = True
        Me.chkAreasBodegaActivos.Location = New System.Drawing.Point(2, 414)
        Me.chkAreasBodegaActivos.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkAreasBodegaActivos.Name = "chkAreasBodegaActivos"
        Me.chkAreasBodegaActivos.Properties.Caption = "Activos"
        Me.chkAreasBodegaActivos.Size = New System.Drawing.Size(1780, 24)
        Me.chkAreasBodegaActivos.TabIndex = 1
        '
        'tabSector
        '
        Me.tabSector.Controls.Add(Me.SplitContainer2)
        Me.tabSector.Controls.Add(Me.GroupControl9)
        Me.tabSector.Controls.Add(Me.ToolStrip1)
        Me.tabSector.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tabSector.Name = "tabSector"
        Me.tabSector.Size = New System.Drawing.Size(1784, 711)
        Me.tabSector.Text = "Sector"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 27)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupControl8)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GroupControl7)
        Me.SplitContainer2.Size = New System.Drawing.Size(1784, 217)
        Me.SplitContainer2.SplitterDistance = 838
        Me.SplitContainer2.SplitterWidth = 6
        Me.SplitContainer2.TabIndex = 1
        '
        'GroupControl8
        '
        Me.GroupControl8.Controls.Add(Label53)
        Me.GroupControl8.Controls.Add(Me.txtIdSector)
        Me.GroupControl8.Controls.Add(Me.cmbArea)
        Me.GroupControl8.Controls.Add(Me.chkActivoSector)
        Me.GroupControl8.Controls.Add(lblCodigoSector)
        Me.GroupControl8.Controls.Add(lblAreaSector)
        Me.GroupControl8.Controls.Add(Me.txtCodigoSector)
        Me.GroupControl8.Controls.Add(Me.nUpdAnchoSector)
        Me.GroupControl8.Controls.Add(lblDescripcionSector)
        Me.GroupControl8.Controls.Add(Me.chkSistemaSector)
        Me.GroupControl8.Controls.Add(Me.nUpdLargoSector)
        Me.GroupControl8.Controls.Add(Me.txtDescripcionSector)
        Me.GroupControl8.Controls.Add(Me.nUpdAltoSector)
        Me.GroupControl8.Controls.Add(lblAltoSector)
        Me.GroupControl8.Controls.Add(LlblLargoSector)
        Me.GroupControl8.Controls.Add(lblAnchoSector)
        Me.GroupControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl8.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl8.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl8.Name = "GroupControl8"
        Me.GroupControl8.Size = New System.Drawing.Size(838, 217)
        Me.GroupControl8.TabIndex = 0
        Me.GroupControl8.Text = "Datos de Sector"
        '
        'txtIdSector
        '
        Me.txtIdSector.Location = New System.Drawing.Point(140, 46)
        Me.txtIdSector.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdSector.Name = "txtIdSector"
        Me.txtIdSector.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtIdSector.Properties.MaxLength = 50
        Me.txtIdSector.Properties.ReadOnly = True
        Me.txtIdSector.Size = New System.Drawing.Size(294, 22)
        Me.txtIdSector.TabIndex = 17
        '
        'cmbArea
        '
        Me.cmbArea.Location = New System.Drawing.Point(140, 75)
        Me.cmbArea.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbArea.MenuManager = Me.mnu
        Me.cmbArea.Name = "cmbArea"
        Me.cmbArea.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbArea.Properties.NullText = ""
        Me.cmbArea.Size = New System.Drawing.Size(294, 22)
        Me.cmbArea.TabIndex = 1
        '
        'chkActivoSector
        '
        Me.chkActivoSector.EditValue = True
        Me.chkActivoSector.Location = New System.Drawing.Point(272, 173)
        Me.chkActivoSector.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkActivoSector.Name = "chkActivoSector"
        Me.chkActivoSector.Properties.Caption = "Activo"
        Me.chkActivoSector.Size = New System.Drawing.Size(94, 24)
        Me.chkActivoSector.TabIndex = 12
        '
        'txtCodigoSector
        '
        Me.txtCodigoSector.Location = New System.Drawing.Point(140, 109)
        Me.txtCodigoSector.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCodigoSector.Name = "txtCodigoSector"
        Me.txtCodigoSector.Properties.MaxLength = 50
        Me.txtCodigoSector.Size = New System.Drawing.Size(294, 22)
        Me.txtCodigoSector.TabIndex = 3
        '
        'nUpdAnchoSector
        '
        Me.nUpdAnchoSector.BackColor = System.Drawing.Color.MistyRose
        Me.nUpdAnchoSector.DecimalPlaces = 6
        Me.nUpdAnchoSector.Location = New System.Drawing.Point(543, 141)
        Me.nUpdAnchoSector.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdAnchoSector.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdAnchoSector.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdAnchoSector.Name = "nUpdAnchoSector"
        Me.nUpdAnchoSector.Size = New System.Drawing.Size(249, 23)
        Me.nUpdAnchoSector.TabIndex = 11
        '
        'chkSistemaSector
        '
        Me.chkSistemaSector.Location = New System.Drawing.Point(140, 173)
        Me.chkSistemaSector.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkSistemaSector.Name = "chkSistemaSector"
        Me.chkSistemaSector.Properties.Caption = "Sistema"
        Me.chkSistemaSector.Size = New System.Drawing.Size(102, 24)
        Me.chkSistemaSector.TabIndex = 13
        '
        'nUpdLargoSector
        '
        Me.nUpdLargoSector.BackColor = System.Drawing.Color.MistyRose
        Me.nUpdLargoSector.DecimalPlaces = 6
        Me.nUpdLargoSector.Location = New System.Drawing.Point(543, 109)
        Me.nUpdLargoSector.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdLargoSector.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdLargoSector.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdLargoSector.Name = "nUpdLargoSector"
        Me.nUpdLargoSector.Size = New System.Drawing.Size(249, 23)
        Me.nUpdLargoSector.TabIndex = 9
        '
        'txtDescripcionSector
        '
        Me.txtDescripcionSector.Location = New System.Drawing.Point(140, 141)
        Me.txtDescripcionSector.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDescripcionSector.Name = "txtDescripcionSector"
        Me.txtDescripcionSector.Properties.MaxLength = 50
        Me.txtDescripcionSector.Size = New System.Drawing.Size(294, 22)
        Me.txtDescripcionSector.TabIndex = 5
        '
        'nUpdAltoSector
        '
        Me.nUpdAltoSector.BackColor = System.Drawing.Color.MistyRose
        Me.nUpdAltoSector.DecimalPlaces = 6
        Me.nUpdAltoSector.Location = New System.Drawing.Point(543, 75)
        Me.nUpdAltoSector.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdAltoSector.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdAltoSector.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdAltoSector.Name = "nUpdAltoSector"
        Me.nUpdAltoSector.Size = New System.Drawing.Size(249, 23)
        Me.nUpdAltoSector.TabIndex = 7
        '
        'GroupControl7
        '
        Me.GroupControl7.CausesValidation = False
        Me.GroupControl7.Controls.Add(Me.txtPosY)
        Me.GroupControl7.Controls.Add(Me.txtPosX)
        Me.GroupControl7.Controls.Add(Me.Label25)
        Me.GroupControl7.Controls.Add(Me.Label23)
        Me.GroupControl7.Controls.Add(Me.Label14)
        Me.GroupControl7.Controls.Add(Me.chkHorizontal)
        Me.GroupControl7.Controls.Add(Me.nUpdMargenInferiorSector)
        Me.GroupControl7.Controls.Add(Me.nUpdMargenSuperiorSector)
        Me.GroupControl7.Controls.Add(Me.nUpdMargenDerechoSector)
        Me.GroupControl7.Controls.Add(Me.nUpdMargenIzquierdoSector)
        Me.GroupControl7.Controls.Add(lblMargenSuperiorSector)
        Me.GroupControl7.Controls.Add(lblMargenInferiorSector)
        Me.GroupControl7.Controls.Add(lblMargenDerechoSector)
        Me.GroupControl7.Controls.Add(lblMargenIzquierdoSector)
        Me.GroupControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl7.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl7.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl7.Name = "GroupControl7"
        Me.GroupControl7.Size = New System.Drawing.Size(940, 217)
        Me.GroupControl7.TabIndex = 0
        Me.GroupControl7.Text = "Dimensiones"
        '
        'txtPosY
        '
        Me.txtPosY.BackColor = System.Drawing.Color.MistyRose
        Me.txtPosY.DecimalPlaces = 6
        Me.txtPosY.Location = New System.Drawing.Point(128, 107)
        Me.txtPosY.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPosY.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.txtPosY.Name = "txtPosY"
        Me.txtPosY.Size = New System.Drawing.Size(170, 23)
        Me.txtPosY.TabIndex = 6
        '
        'txtPosX
        '
        Me.txtPosX.BackColor = System.Drawing.Color.MistyRose
        Me.txtPosX.DecimalPlaces = 6
        Me.txtPosX.Location = New System.Drawing.Point(128, 74)
        Me.txtPosX.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPosX.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.txtPosX.Name = "txtPosX"
        Me.txtPosX.Size = New System.Drawing.Size(170, 23)
        Me.txtPosX.TabIndex = 2
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(15, 110)
        Me.Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(69, 16)
        Me.Label25.TabIndex = 4
        Me.Label25.Text = "Posición Y:"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(14, 77)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(70, 16)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "Posición X:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(15, 143)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(55, 16)
        Me.Label14.TabIndex = 9
        Me.Label14.Text = "Vertical:"
        '
        'chkHorizontal
        '
        Me.chkHorizontal.AutoSize = True
        Me.chkHorizontal.Location = New System.Drawing.Point(128, 143)
        Me.chkHorizontal.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkHorizontal.Name = "chkHorizontal"
        Me.chkHorizontal.Size = New System.Drawing.Size(18, 17)
        Me.chkHorizontal.TabIndex = 10
        Me.chkHorizontal.UseVisualStyleBackColor = True
        '
        'nUpdMargenInferiorSector
        '
        Me.nUpdMargenInferiorSector.DecimalPlaces = 6
        Me.nUpdMargenInferiorSector.Location = New System.Drawing.Point(522, 171)
        Me.nUpdMargenInferiorSector.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdMargenInferiorSector.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdMargenInferiorSector.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdMargenInferiorSector.Name = "nUpdMargenInferiorSector"
        Me.nUpdMargenInferiorSector.Size = New System.Drawing.Size(170, 23)
        Me.nUpdMargenInferiorSector.TabIndex = 13
        '
        'nUpdMargenSuperiorSector
        '
        Me.nUpdMargenSuperiorSector.BackColor = System.Drawing.Color.MistyRose
        Me.nUpdMargenSuperiorSector.DecimalPlaces = 6
        Me.nUpdMargenSuperiorSector.Location = New System.Drawing.Point(522, 140)
        Me.nUpdMargenSuperiorSector.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdMargenSuperiorSector.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdMargenSuperiorSector.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdMargenSuperiorSector.Name = "nUpdMargenSuperiorSector"
        Me.nUpdMargenSuperiorSector.Size = New System.Drawing.Size(170, 23)
        Me.nUpdMargenSuperiorSector.TabIndex = 11
        '
        'nUpdMargenDerechoSector
        '
        Me.nUpdMargenDerechoSector.DecimalPlaces = 6
        Me.nUpdMargenDerechoSector.Location = New System.Drawing.Point(522, 107)
        Me.nUpdMargenDerechoSector.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdMargenDerechoSector.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdMargenDerechoSector.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdMargenDerechoSector.Name = "nUpdMargenDerechoSector"
        Me.nUpdMargenDerechoSector.Size = New System.Drawing.Size(170, 23)
        Me.nUpdMargenDerechoSector.TabIndex = 5
        '
        'nUpdMargenIzquierdoSector
        '
        Me.nUpdMargenIzquierdoSector.DecimalPlaces = 6
        Me.nUpdMargenIzquierdoSector.Location = New System.Drawing.Point(522, 74)
        Me.nUpdMargenIzquierdoSector.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdMargenIzquierdoSector.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdMargenIzquierdoSector.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdMargenIzquierdoSector.Name = "nUpdMargenIzquierdoSector"
        Me.nUpdMargenIzquierdoSector.Size = New System.Drawing.Size(170, 23)
        Me.nUpdMargenIzquierdoSector.TabIndex = 1
        '
        'GroupControl9
        '
        Me.GroupControl9.Controls.Add(Me.grdSectorArea)
        Me.GroupControl9.Controls.Add(Me.chkSectoresActivos)
        Me.GroupControl9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl9.Location = New System.Drawing.Point(0, 244)
        Me.GroupControl9.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl9.Name = "GroupControl9"
        Me.GroupControl9.Size = New System.Drawing.Size(1784, 467)
        Me.GroupControl9.TabIndex = 2
        Me.GroupControl9.Text = "Detalle Sectores"
        '
        'grdSectorArea
        '
        Me.grdSectorArea.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSectorArea.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdSectorArea.Location = New System.Drawing.Point(2, 28)
        Me.grdSectorArea.MainView = Me.GridViewSec
        Me.grdSectorArea.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdSectorArea.Name = "grdSectorArea"
        Me.grdSectorArea.Size = New System.Drawing.Size(1780, 413)
        Me.grdSectorArea.TabIndex = 0
        Me.grdSectorArea.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewSec, Me.GridView2})
        '
        'GridViewSec
        '
        Me.GridViewSec.DetailHeight = 437
        Me.GridViewSec.GridControl = Me.grdSectorArea
        Me.GridViewSec.Name = "GridViewSec"
        Me.GridViewSec.OptionsBehavior.Editable = False
        Me.GridViewSec.OptionsEditForm.PopupEditFormWidth = 1000
        Me.GridViewSec.OptionsView.ColumnAutoWidth = False
        Me.GridViewSec.OptionsView.ShowFooter = True
        Me.GridViewSec.OptionsView.ShowGroupPanel = False
        '
        'GridView2
        '
        Me.GridView2.DetailHeight = 437
        Me.GridView2.GridControl = Me.grdSectorArea
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsEditForm.PopupEditFormWidth = 1000
        '
        'chkSectoresActivos
        '
        Me.chkSectoresActivos.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.chkSectoresActivos.EditValue = True
        Me.chkSectoresActivos.Location = New System.Drawing.Point(2, 441)
        Me.chkSectoresActivos.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkSectoresActivos.Name = "chkSectoresActivos"
        Me.chkSectoresActivos.Properties.Caption = "Activos"
        Me.chkSectoresActivos.Size = New System.Drawing.Size(1780, 24)
        Me.chkSectoresActivos.TabIndex = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmnuNuevoSector, Me.tsmnuGuardarSector})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1784, 27)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip2"
        '
        'tsmnuNuevoSector
        '
        Me.tsmnuNuevoSector.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsmnuNuevoSector.Name = "tsmnuNuevoSector"
        Me.tsmnuNuevoSector.Size = New System.Drawing.Size(56, 24)
        Me.tsmnuNuevoSector.Text = "Nuevo"
        '
        'tsmnuGuardarSector
        '
        Me.tsmnuGuardarSector.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsmnuGuardarSector.Name = "tsmnuGuardarSector"
        Me.tsmnuGuardarSector.Size = New System.Drawing.Size(66, 24)
        Me.tsmnuGuardarSector.Text = "Guardar"
        '
        'tabTramo
        '
        Me.tabTramo.Controls.Add(Me.SplitContainer3)
        Me.tabTramo.Controls.Add(Me.GroupControl10)
        Me.tabTramo.Controls.Add(Me.ToolStrip2)
        Me.tabTramo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tabTramo.Name = "tabTramo"
        Me.tabTramo.Size = New System.Drawing.Size(1784, 711)
        Me.tabTramo.Text = "Tramo"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 27)
        Me.SplitContainer3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.GroupControl12)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.GroupControl11)
        Me.SplitContainer3.Size = New System.Drawing.Size(1784, 305)
        Me.SplitContainer3.SplitterDistance = 718
        Me.SplitContainer3.SplitterWidth = 6
        Me.SplitContainer3.TabIndex = 1
        '
        'ToolStrip2
        '
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmnuNuevoTramo, Me.tsmnuGuardarTramo})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(1784, 27)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'tsmnuNuevoTramo
        '
        Me.tsmnuNuevoTramo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsmnuNuevoTramo.Image = CType(resources.GetObject("tsmnuNuevoTramo.Image"), System.Drawing.Image)
        Me.tsmnuNuevoTramo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsmnuNuevoTramo.Name = "tsmnuNuevoTramo"
        Me.tsmnuNuevoTramo.Size = New System.Drawing.Size(56, 24)
        Me.tsmnuNuevoTramo.Text = "Nuevo"
        '
        'tsmnuGuardarTramo
        '
        Me.tsmnuGuardarTramo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsmnuGuardarTramo.Image = CType(resources.GetObject("tsmnuGuardarTramo.Image"), System.Drawing.Image)
        Me.tsmnuGuardarTramo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsmnuGuardarTramo.Name = "tsmnuGuardarTramo"
        Me.tsmnuGuardarTramo.Size = New System.Drawing.Size(66, 24)
        Me.tsmnuGuardarTramo.Text = "Guardar"
        '
        'TabUbicacion
        '
        Me.TabUbicacion.Controls.Add(Me.SplitContainer4)
        Me.TabUbicacion.Controls.Add(Me.GroupControl15)
        Me.TabUbicacion.Controls.Add(Me.ToolStrip3)
        Me.TabUbicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabUbicacion.Name = "TabUbicacion"
        Me.TabUbicacion.Size = New System.Drawing.Size(1784, 711)
        Me.TabUbicacion.Text = "Ubicación"
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 27)
        Me.SplitContainer4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SplitContainer4.Name = "SplitContainer4"
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.GroupControl13)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.GroupControl14)
        Me.SplitContainer4.Size = New System.Drawing.Size(1784, 380)
        Me.SplitContainer4.SplitterDistance = 703
        Me.SplitContainer4.SplitterWidth = 6
        Me.SplitContainer4.TabIndex = 1
        '
        'GroupControl13
        '
        Me.GroupControl13.Controls.Add(lblUbicCodigoBodegaERP)
        Me.GroupControl13.Controls.Add(Me.txtUbicCodigoBodegaERP)
        Me.GroupControl13.Controls.Add(Me.cmbTramo)
        Me.GroupControl13.Controls.Add(Me.cmbSectorR)
        Me.GroupControl13.Controls.Add(Me.cmbAreaUbic)
        Me.GroupControl13.Controls.Add(Me.cmbIndiceRotacion)
        Me.GroupControl13.Controls.Add(Me.cmbTipoRotacion)
        Me.GroupControl13.Controls.Add(Me.Label32)
        Me.GroupControl13.Controls.Add(Me.txtIndiceX)
        Me.GroupControl13.Controls.Add(Label13)
        Me.GroupControl13.Controls.Add(Label4)
        Me.GroupControl13.Controls.Add(Label50)
        Me.GroupControl13.Controls.Add(Label46)
        Me.GroupControl13.Controls.Add(Me.nUpdNivelUbicacion)
        Me.GroupControl13.Controls.Add(Label44)
        Me.GroupControl13.Controls.Add(Label24)
        Me.GroupControl13.Controls.Add(lblCodigoUbicacionVirtual)
        Me.GroupControl13.Controls.Add(Me.txtCodigoBarra2ubicacion)
        Me.GroupControl13.Controls.Add(Label28)
        Me.GroupControl13.Controls.Add(Me.txtCodigoBarraUbicacion)
        Me.GroupControl13.Controls.Add(Label30)
        Me.GroupControl13.Controls.Add(Me.txtDescripcionUbicacion)
        Me.GroupControl13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl13.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl13.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl13.Name = "GroupControl13"
        Me.GroupControl13.Size = New System.Drawing.Size(703, 380)
        Me.GroupControl13.TabIndex = 0
        Me.GroupControl13.Text = "Datos de Ubicación"
        '
        'txtUbicCodigoBodegaERP
        '
        Me.txtUbicCodigoBodegaERP.Location = New System.Drawing.Point(161, 334)
        Me.txtUbicCodigoBodegaERP.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtUbicCodigoBodegaERP.Name = "txtUbicCodigoBodegaERP"
        Me.txtUbicCodigoBodegaERP.Properties.MaxLength = 50
        Me.txtUbicCodigoBodegaERP.Properties.NullValuePrompt = "SSI Bodega_Virtual = True"
        Me.txtUbicCodigoBodegaERP.Properties.ReadOnly = True
        Me.txtUbicCodigoBodegaERP.Size = New System.Drawing.Size(514, 22)
        Me.txtUbicCodigoBodegaERP.TabIndex = 21
        '
        'cmbTramo
        '
        Me.cmbTramo.Location = New System.Drawing.Point(161, 100)
        Me.cmbTramo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbTramo.MenuManager = Me.mnu
        Me.cmbTramo.Name = "cmbTramo"
        Me.cmbTramo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTramo.Properties.NullText = ""
        Me.cmbTramo.Size = New System.Drawing.Size(514, 22)
        Me.cmbTramo.TabIndex = 5
        '
        'cmbSectorR
        '
        Me.cmbSectorR.Location = New System.Drawing.Point(161, 71)
        Me.cmbSectorR.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbSectorR.MenuManager = Me.mnu
        Me.cmbSectorR.Name = "cmbSectorR"
        Me.cmbSectorR.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbSectorR.Properties.NullText = ""
        Me.cmbSectorR.Size = New System.Drawing.Size(514, 22)
        Me.cmbSectorR.TabIndex = 3
        '
        'cmbAreaUbic
        '
        Me.cmbAreaUbic.Location = New System.Drawing.Point(161, 42)
        Me.cmbAreaUbic.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbAreaUbic.MenuManager = Me.mnu
        Me.cmbAreaUbic.Name = "cmbAreaUbic"
        Me.cmbAreaUbic.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbAreaUbic.Properties.NullText = ""
        Me.cmbAreaUbic.Size = New System.Drawing.Size(514, 22)
        Me.cmbAreaUbic.TabIndex = 1
        '
        'cmbIndiceRotacion
        '
        Me.cmbIndiceRotacion.Location = New System.Drawing.Point(161, 158)
        Me.cmbIndiceRotacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbIndiceRotacion.MenuManager = Me.mnu
        Me.cmbIndiceRotacion.Name = "cmbIndiceRotacion"
        Me.cmbIndiceRotacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbIndiceRotacion.Properties.NullText = ""
        Me.cmbIndiceRotacion.Size = New System.Drawing.Size(514, 22)
        Me.cmbIndiceRotacion.TabIndex = 9
        '
        'cmbTipoRotacion
        '
        Me.cmbTipoRotacion.Location = New System.Drawing.Point(161, 129)
        Me.cmbTipoRotacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbTipoRotacion.MenuManager = Me.mnu
        Me.cmbTipoRotacion.Name = "cmbTipoRotacion"
        Me.cmbTipoRotacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoRotacion.Properties.NullText = ""
        Me.cmbTipoRotacion.Size = New System.Drawing.Size(514, 22)
        Me.cmbTipoRotacion.TabIndex = 7
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(9, 158)
        Me.Label32.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(99, 16)
        Me.Label32.TabIndex = 8
        Me.Label32.Text = "Indice Rotación:"
        '
        'txtIndiceX
        '
        Me.txtIndiceX.Location = New System.Drawing.Point(161, 304)
        Me.txtIndiceX.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIndiceX.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtIndiceX.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtIndiceX.Name = "txtIndiceX"
        Me.txtIndiceX.Size = New System.Drawing.Size(514, 23)
        Me.txtIndiceX.TabIndex = 19
        '
        'nUpdNivelUbicacion
        '
        Me.nUpdNivelUbicacion.Location = New System.Drawing.Point(161, 274)
        Me.nUpdNivelUbicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nUpdNivelUbicacion.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nUpdNivelUbicacion.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nUpdNivelUbicacion.Name = "nUpdNivelUbicacion"
        Me.nUpdNivelUbicacion.Size = New System.Drawing.Size(514, 23)
        Me.nUpdNivelUbicacion.TabIndex = 17
        '
        'txtCodigoBarra2ubicacion
        '
        Me.txtCodigoBarra2ubicacion.Location = New System.Drawing.Point(161, 216)
        Me.txtCodigoBarra2ubicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCodigoBarra2ubicacion.Name = "txtCodigoBarra2ubicacion"
        Me.txtCodigoBarra2ubicacion.Properties.MaxLength = 50
        Me.txtCodigoBarra2ubicacion.Properties.NullValuePrompt = "SSI Bodega_Virtual = True"
        Me.txtCodigoBarra2ubicacion.Properties.ReadOnly = True
        Me.txtCodigoBarra2ubicacion.Size = New System.Drawing.Size(514, 22)
        Me.txtCodigoBarra2ubicacion.TabIndex = 13
        '
        'txtCodigoBarraUbicacion
        '
        Me.txtCodigoBarraUbicacion.Location = New System.Drawing.Point(161, 187)
        Me.txtCodigoBarraUbicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCodigoBarraUbicacion.Name = "txtCodigoBarraUbicacion"
        Me.txtCodigoBarraUbicacion.Properties.MaxLength = 25
        Me.txtCodigoBarraUbicacion.Size = New System.Drawing.Size(514, 22)
        Me.txtCodigoBarraUbicacion.TabIndex = 11
        '
        'txtDescripcionUbicacion
        '
        Me.txtDescripcionUbicacion.Location = New System.Drawing.Point(161, 245)
        Me.txtDescripcionUbicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDescripcionUbicacion.Name = "txtDescripcionUbicacion"
        Me.txtDescripcionUbicacion.Properties.MaxLength = 50
        Me.txtDescripcionUbicacion.Size = New System.Drawing.Size(514, 22)
        Me.txtDescripcionUbicacion.TabIndex = 15
        '
        'ToolStrip3
        '
        Me.ToolStrip3.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmnuNuevaUbicacion, Me.tsmnuGuardarUbicacion})
        Me.ToolStrip3.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip3.Name = "ToolStrip3"
        Me.ToolStrip3.Size = New System.Drawing.Size(1784, 27)
        Me.ToolStrip3.TabIndex = 0
        Me.ToolStrip3.Text = "ToolStrip3"
        '
        'tsmnuNuevaUbicacion
        '
        Me.tsmnuNuevaUbicacion.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsmnuNuevaUbicacion.Name = "tsmnuNuevaUbicacion"
        Me.tsmnuNuevaUbicacion.Size = New System.Drawing.Size(56, 24)
        Me.tsmnuNuevaUbicacion.Text = "Nuevo"
        '
        'tsmnuGuardarUbicacion
        '
        Me.tsmnuGuardarUbicacion.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsmnuGuardarUbicacion.Name = "tsmnuGuardarUbicacion"
        Me.tsmnuGuardarUbicacion.Size = New System.Drawing.Size(66, 24)
        Me.tsmnuGuardarUbicacion.Text = "Guardar"
        '
        'tabReferencia
        '
        Me.tabReferencia.Controls.Add(Me.GroupControl16)
        Me.tabReferencia.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tabReferencia.Name = "tabReferencia"
        Me.tabReferencia.Size = New System.Drawing.Size(1784, 711)
        Me.tabReferencia.Text = "Árbol de ubicaciones"
        '
        'tabParametros
        '
        Me.tabParametros.Controls.Add(Me.Dgrid)
        Me.tabParametros.Controls.Add(Me.GroupControl1)
        Me.tabParametros.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tabParametros.Name = "tabParametros"
        Me.tabParametros.Size = New System.Drawing.Size(1784, 711)
        Me.tabParametros.Text = "Parametros Monitor"
        '
        'tabUbicacionesDefecto
        '
        Me.tabUbicacionesDefecto.Controls.Add(Me.GroupControl3)
        Me.tabUbicacionesDefecto.Controls.Add(Me.GroupControl2)
        Me.tabUbicacionesDefecto.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tabUbicacionesDefecto.Name = "tabUbicacionesDefecto"
        Me.tabUbicacionesDefecto.Size = New System.Drawing.Size(1784, 711)
        Me.tabUbicacionesDefecto.Text = "Valores por defecto"
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.GroupBox5)
        Me.GroupControl3.Controls.Add(Me.GroupBox4)
        Me.GroupControl3.Controls.Add(Me.GroupBox3)
        Me.GroupControl3.Controls.Add(Me.GroupBox2)
        Me.GroupControl3.Controls.Add(Me.GroupBox1)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(612, 0)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(1172, 711)
        Me.GroupControl3.TabIndex = 61
        Me.GroupControl3.Text = "Parámetros"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.lblControlGondola)
        Me.GroupBox5.Controls.Add(Me.chkControlGondola)
        Me.GroupBox5.Controls.Add(Me.Label82)
        Me.GroupBox5.Controls.Add(Me.nudRangoDiasDocumentos)
        Me.GroupBox5.Controls.Add(lblControlTallaColor)
        Me.GroupBox5.Controls.Add(Me.chkControlTallaColor)
        Me.GroupBox5.Controls.Add(lblrestringir_vencimiento_en_reemplazo)
        Me.GroupBox5.Controls.Add(Me.chkrestringir_vencimiento_en_reemplazo)
        Me.GroupBox5.Controls.Add(lblrestringir_lote_en_reemplazo)
        Me.GroupBox5.Controls.Add(Me.chkrestringir_lote_en_reemplazo)
        Me.GroupBox5.Controls.Add(Me.chkLberarStockDepachosParciales)
        Me.GroupBox5.Controls.Add(Label76)
        Me.GroupBox5.Controls.Add(Me.lblHomologarLoteConFechaVence)
        Me.GroupBox5.Controls.Add(Me.chkHomologarLoteConFechaVence)
        Me.GroupBox5.Controls.Add(Label66)
        Me.GroupBox5.Controls.Add(Me.chkValidarExistenciasEnCargaInventarioInicial)
        Me.GroupBox5.Controls.Add(lblControlOperadorPorUbicacion)
        Me.GroupBox5.Controls.Add(Label79)
        Me.GroupBox5.Controls.Add(Me.chkControlPalletsMixtos)
        Me.GroupBox5.Controls.Add(Me.chkControlOperadorUbicacion)
        Me.GroupBox5.Controls.Add(Label54)
        Me.GroupBox5.Controls.Add(Me.chkinferir_origen_en_cambio_ubic)
        Me.GroupBox5.Controls.Add(Me.chkValidarDisponibilidadEnUbicacionDestino)
        Me.GroupBox5.Controls.Add(lblinferir_origen_en_cambio_ubic)
        Me.GroupBox5.Location = New System.Drawing.Point(806, 37)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox5.Size = New System.Drawing.Size(354, 347)
        Me.GroupBox5.TabIndex = 124
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Operacion Mixta"
        '
        'chkControlGondola
        '
        Me.chkControlGondola.Location = New System.Drawing.Point(318, 315)
        Me.chkControlGondola.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkControlGondola.Name = "chkControlGondola"
        Me.chkControlGondola.Properties.Caption = ""
        Me.chkControlGondola.Size = New System.Drawing.Size(28, 24)
        Me.chkControlGondola.TabIndex = 124
        Me.chkControlGondola.ToolTip = "Restringir áeras de SAP"
        '
        'Label82
        '
        Me.Label82.AutoSize = True
        Me.Label82.Location = New System.Drawing.Point(9, 297)
        Me.Label82.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(143, 16)
        Me.Label82.TabIndex = 122
        Me.Label82.Text = "Rango días documentos"
        '
        'nudRangoDiasDocumentos
        '
        Me.nudRangoDiasDocumentos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nudRangoDiasDocumentos.Location = New System.Drawing.Point(290, 290)
        Me.nudRangoDiasDocumentos.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nudRangoDiasDocumentos.Maximum = New Decimal(New Integer() {365, 0, 0, 0})
        Me.nudRangoDiasDocumentos.Name = "nudRangoDiasDocumentos"
        Me.nudRangoDiasDocumentos.Size = New System.Drawing.Size(49, 23)
        Me.nudRangoDiasDocumentos.TabIndex = 123
        '
        'chkControlTallaColor
        '
        Me.chkControlTallaColor.Location = New System.Drawing.Point(317, 262)
        Me.chkControlTallaColor.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkControlTallaColor.Name = "chkControlTallaColor"
        Me.chkControlTallaColor.Properties.Caption = ""
        Me.chkControlTallaColor.Size = New System.Drawing.Size(28, 24)
        Me.chkControlTallaColor.TabIndex = 116
        Me.chkControlTallaColor.ToolTip = "Restringir áeras de SAP"
        '
        'chkrestringir_vencimiento_en_reemplazo
        '
        Me.chkrestringir_vencimiento_en_reemplazo.Location = New System.Drawing.Point(315, 21)
        Me.chkrestringir_vencimiento_en_reemplazo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkrestringir_vencimiento_en_reemplazo.Name = "chkrestringir_vencimiento_en_reemplazo"
        Me.chkrestringir_vencimiento_en_reemplazo.Properties.Caption = ""
        Me.chkrestringir_vencimiento_en_reemplazo.Size = New System.Drawing.Size(28, 24)
        Me.chkrestringir_vencimiento_en_reemplazo.TabIndex = 75
        Me.chkrestringir_vencimiento_en_reemplazo.ToolTip = " If Requerir_Cliente_Es_Bodega_WMS Then"
        '
        'chkrestringir_lote_en_reemplazo
        '
        Me.chkrestringir_lote_en_reemplazo.Location = New System.Drawing.Point(315, 48)
        Me.chkrestringir_lote_en_reemplazo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkrestringir_lote_en_reemplazo.Name = "chkrestringir_lote_en_reemplazo"
        Me.chkrestringir_lote_en_reemplazo.Properties.Caption = ""
        Me.chkrestringir_lote_en_reemplazo.Size = New System.Drawing.Size(28, 24)
        Me.chkrestringir_lote_en_reemplazo.TabIndex = 77
        Me.chkrestringir_lote_en_reemplazo.ToolTip = " If Requerir_Cliente_Es_Bodega_WMS Then"
        '
        'chkLberarStockDepachosParciales
        '
        Me.chkLberarStockDepachosParciales.Location = New System.Drawing.Point(316, 78)
        Me.chkLberarStockDepachosParciales.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.chkLberarStockDepachosParciales.Name = "chkLberarStockDepachosParciales"
        Me.chkLberarStockDepachosParciales.Properties.Caption = ""
        Me.chkLberarStockDepachosParciales.Size = New System.Drawing.Size(28, 24)
        Me.chkLberarStockDepachosParciales.TabIndex = 104
        '
        'chkHomologarLoteConFechaVence
        '
        Me.chkHomologarLoteConFechaVence.Location = New System.Drawing.Point(316, 105)
        Me.chkHomologarLoteConFechaVence.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkHomologarLoteConFechaVence.Name = "chkHomologarLoteConFechaVence"
        Me.chkHomologarLoteConFechaVence.Properties.Caption = ""
        Me.chkHomologarLoteConFechaVence.Size = New System.Drawing.Size(28, 24)
        Me.chkHomologarLoteConFechaVence.TabIndex = 106
        '
        'chkValidarExistenciasEnCargaInventarioInicial
        '
        Me.chkValidarExistenciasEnCargaInventarioInicial.Location = New System.Drawing.Point(316, 130)
        Me.chkValidarExistenciasEnCargaInventarioInicial.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkValidarExistenciasEnCargaInventarioInicial.Name = "chkValidarExistenciasEnCargaInventarioInicial"
        Me.chkValidarExistenciasEnCargaInventarioInicial.Properties.Caption = ""
        Me.chkValidarExistenciasEnCargaInventarioInicial.Size = New System.Drawing.Size(28, 24)
        Me.chkValidarExistenciasEnCargaInventarioInicial.TabIndex = 85
        Me.chkValidarExistenciasEnCargaInventarioInicial.ToolTip = "#EJC20220912: Si true, se permite recibir X cantidad de copias de un producto en " &
    "recepción que cumpla con los mismos parámetros si el usuario tiene resolución de" &
    " licencias definidas."
        '
        'chkControlPalletsMixtos
        '
        Me.chkControlPalletsMixtos.Location = New System.Drawing.Point(317, 235)
        Me.chkControlPalletsMixtos.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkControlPalletsMixtos.Name = "chkControlPalletsMixtos"
        Me.chkControlPalletsMixtos.Properties.Caption = ""
        Me.chkControlPalletsMixtos.Size = New System.Drawing.Size(28, 24)
        Me.chkControlPalletsMixtos.TabIndex = 114
        Me.chkControlPalletsMixtos.ToolTip = "Restringir áeras de SAP"
        '
        'chkControlOperadorUbicacion
        '
        Me.chkControlOperadorUbicacion.Location = New System.Drawing.Point(317, 156)
        Me.chkControlOperadorUbicacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkControlOperadorUbicacion.Name = "chkControlOperadorUbicacion"
        Me.chkControlOperadorUbicacion.Properties.Caption = ""
        Me.chkControlOperadorUbicacion.Size = New System.Drawing.Size(28, 24)
        Me.chkControlOperadorUbicacion.TabIndex = 57
        Me.chkControlOperadorUbicacion.ToolTip = "  '#EJC20220129: Validar si la ubicación destino tiene producto o está ""libre"" an" &
    "tes de colocar producto allí"
        '
        'chkinferir_origen_en_cambio_ubic
        '
        Me.chkinferir_origen_en_cambio_ubic.Location = New System.Drawing.Point(317, 209)
        Me.chkinferir_origen_en_cambio_ubic.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkinferir_origen_en_cambio_ubic.Name = "chkinferir_origen_en_cambio_ubic"
        Me.chkinferir_origen_en_cambio_ubic.Properties.Caption = ""
        Me.chkinferir_origen_en_cambio_ubic.Size = New System.Drawing.Size(28, 24)
        Me.chkinferir_origen_en_cambio_ubic.TabIndex = 61
        Me.chkinferir_origen_en_cambio_ubic.ToolTip = "#EJC20220314: si true, entonces en el cambio de ubicación, al escanear únicamente" &
    " licencia, se coloca automáticamente la ubicación de origen."
        '
        'chkValidarDisponibilidadEnUbicacionDestino
        '
        Me.chkValidarDisponibilidadEnUbicacionDestino.Location = New System.Drawing.Point(317, 183)
        Me.chkValidarDisponibilidadEnUbicacionDestino.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkValidarDisponibilidadEnUbicacionDestino.Name = "chkValidarDisponibilidadEnUbicacionDestino"
        Me.chkValidarDisponibilidadEnUbicacionDestino.Properties.Caption = ""
        Me.chkValidarDisponibilidadEnUbicacionDestino.Size = New System.Drawing.Size(28, 24)
        Me.chkValidarDisponibilidadEnUbicacionDestino.TabIndex = 51
        Me.chkValidarDisponibilidadEnUbicacionDestino.ToolTip = "  '#EJC20220129: Validar si la ubicación destino tiene producto o está ""libre"" an" &
    "tes de colocar producto allí"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label96)
        Me.GroupBox4.Controls.Add(Me.chkUbicImplosionAuto)
        Me.GroupBox4.Controls.Add(Me.chkBodegaClienteAjusteByB)
        Me.GroupBox4.Controls.Add(Me.lblBodegaClienteAjusteByB)
        Me.GroupBox4.Controls.Add(Me.chkreemplazoOpcional)
        Me.GroupBox4.Controls.Add(Label93)
        Me.GroupBox4.Controls.Add(Me.chkImprimir_Verificacion)
        Me.GroupBox4.Controls.Add(Me.Label100)
        Me.GroupBox4.Controls.Add(Me.chkAdvertirMpqUmbas)
        Me.GroupBox4.Controls.Add(Me.Label87)
        Me.GroupBox4.Controls.Add(Me.chkAgrupar_sin_lic_veri_no_cons)
        Me.GroupBox4.Controls.Add(Me.Label86)
        Me.GroupBox4.Controls.Add(Me.chkVerificacion_Consolidada)
        Me.GroupBox4.Controls.Add(Me.Label80)
        Me.GroupBox4.Controls.Add(lblControlBanderasClientePedido)
        Me.GroupBox4.Controls.Add(Me.chkControlBanderasCliente)
        Me.GroupBox4.Controls.Add(lblPermitirRepeticionesEnIngreso)
        Me.GroupBox4.Controls.Add(Me.chkDespacharProductoVencido)
        Me.GroupBox4.Controls.Add(Me.chkPermitirReemplazoVerificacion)
        Me.GroupBox4.Controls.Add(Label71)
        Me.GroupBox4.Controls.Add(Me.Label65)
        Me.GroupBox4.Controls.Add(Me.chkPermitir_Verificacion_Consolidada)
        Me.GroupBox4.Controls.Add(Me.txtDiasMaximoVencimientoReemplazo)
        Me.GroupBox4.Controls.Add(Label52)
        Me.GroupBox4.Controls.Add(lblPermitirEliminarDocumentoSalida)
        Me.GroupBox4.Controls.Add(lbldespachoauto)
        Me.GroupBox4.Controls.Add(Me.chkdespachoautohh)
        Me.GroupBox4.Controls.Add(Me.chkPermitirEliminarDocumentosSalida)
        Me.GroupBox4.Controls.Add(lblEliminarDocumentosSalida)
        Me.GroupBox4.Controls.Add(Me.chkEliminarDocumentosSalida)
        Me.GroupBox4.Controls.Add(Label75)
        Me.GroupBox4.Controls.Add(Me.chkFiltrarPedidosUsuario)
        Me.GroupBox4.Controls.Add(Me.lblCambioUbicacionRestrictivo)
        Me.GroupBox4.Controls.Add(Me.chkCambioUbicacionRestrictivo)
        Me.GroupBox4.Controls.Add(Me.lblPermitirCambioUbicIndiceMenor)
        Me.GroupBox4.Controls.Add(Me.chkPermitirCambioUbicIndiceMenor)
        Me.GroupBox4.Controls.Add(Me.lblRequerirMismoProductoPosiciones)
        Me.GroupBox4.Controls.Add(Me.chkRequerirMismoProductoPosiciones)
        Me.GroupBox4.Location = New System.Drawing.Point(435, 390)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox4.Size = New System.Drawing.Size(724, 316)
        Me.GroupBox4.TabIndex = 123
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Salidas"
        '
        'chkBodegaClienteAjusteByB
        '
        Me.chkBodegaClienteAjusteByB.Location = New System.Drawing.Point(686, 159)
        Me.chkBodegaClienteAjusteByB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkBodegaClienteAjusteByB.Name = "chkBodegaClienteAjusteByB"
        Me.chkBodegaClienteAjusteByB.Properties.Caption = ""
        Me.chkBodegaClienteAjusteByB.Size = New System.Drawing.Size(23, 24)
        Me.chkBodegaClienteAjusteByB.TabIndex = 131
        '
        'lblBodegaClienteAjusteByB
        '
        Me.lblBodegaClienteAjusteByB.AutoSize = True
        Me.lblBodegaClienteAjusteByB.Location = New System.Drawing.Point(381, 162)
        Me.lblBodegaClienteAjusteByB.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBodegaClienteAjusteByB.Name = "lblBodegaClienteAjusteByB"
        Me.lblBodegaClienteAjusteByB.Size = New System.Drawing.Size(153, 16)
        Me.lblBodegaClienteAjusteByB.TabIndex = 130
        Me.lblBodegaClienteAjusteByB.Text = "Bodega cliente ajuste ByB"
        '
        'chkreemplazoOpcional
        '
        Me.chkreemplazoOpcional.Location = New System.Drawing.Point(336, 257)
        Me.chkreemplazoOpcional.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkreemplazoOpcional.Name = "chkreemplazoOpcional"
        Me.chkreemplazoOpcional.Properties.Caption = ""
        Me.chkreemplazoOpcional.Size = New System.Drawing.Size(28, 24)
        Me.chkreemplazoOpcional.TabIndex = 129
        '
        'chkImprimir_Verificacion
        '
        Me.chkImprimir_Verificacion.Location = New System.Drawing.Point(686, 130)
        Me.chkImprimir_Verificacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkImprimir_Verificacion.Name = "chkImprimir_Verificacion"
        Me.chkImprimir_Verificacion.Properties.Caption = ""
        Me.chkImprimir_Verificacion.Size = New System.Drawing.Size(23, 24)
        Me.chkImprimir_Verificacion.TabIndex = 127
        '
        'Label100
        '
        Me.Label100.AutoSize = True
        Me.Label100.Location = New System.Drawing.Point(381, 134)
        Me.Label100.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(124, 16)
        Me.Label100.TabIndex = 126
        Me.Label100.Text = "Imprimir verificacion"
        '
        'chkAdvertirMpqUmbas
        '
        Me.chkAdvertirMpqUmbas.Location = New System.Drawing.Point(686, 101)
        Me.chkAdvertirMpqUmbas.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkAdvertirMpqUmbas.Name = "chkAdvertirMpqUmbas"
        Me.chkAdvertirMpqUmbas.Properties.Caption = ""
        Me.chkAdvertirMpqUmbas.Size = New System.Drawing.Size(23, 24)
        Me.chkAdvertirMpqUmbas.TabIndex = 125
        '
        'Label87
        '
        Me.Label87.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label87.AutoSize = True
        Me.Label87.Location = New System.Drawing.Point(381, 106)
        Me.Label87.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(144, 16)
        Me.Label87.TabIndex = 124
        Me.Label87.Text = "Advertir MPQ UM Básica"
        '
        'chkAgrupar_sin_lic_veri_no_cons
        '
        Me.chkAgrupar_sin_lic_veri_no_cons.Location = New System.Drawing.Point(686, 72)
        Me.chkAgrupar_sin_lic_veri_no_cons.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkAgrupar_sin_lic_veri_no_cons.Name = "chkAgrupar_sin_lic_veri_no_cons"
        Me.chkAgrupar_sin_lic_veri_no_cons.Properties.Caption = ""
        Me.chkAgrupar_sin_lic_veri_no_cons.Size = New System.Drawing.Size(23, 24)
        Me.chkAgrupar_sin_lic_veri_no_cons.TabIndex = 123
        '
        'Label86
        '
        Me.Label86.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label86.AutoSize = True
        Me.Label86.Location = New System.Drawing.Point(381, 78)
        Me.Label86.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(281, 16)
        Me.Label86.TabIndex = 122
        Me.Label86.Text = "Agrupar sin licencia en verificación consolidada:"
        '
        'chkVerificacion_Consolidada
        '
        Me.chkVerificacion_Consolidada.Location = New System.Drawing.Point(686, 43)
        Me.chkVerificacion_Consolidada.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkVerificacion_Consolidada.Name = "chkVerificacion_Consolidada"
        Me.chkVerificacion_Consolidada.Properties.Caption = ""
        Me.chkVerificacion_Consolidada.Size = New System.Drawing.Size(23, 24)
        Me.chkVerificacion_Consolidada.TabIndex = 119
        '
        'chkControlBanderasCliente
        '
        Me.chkControlBanderasCliente.Location = New System.Drawing.Point(336, 18)
        Me.chkControlBanderasCliente.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkControlBanderasCliente.Name = "chkControlBanderasCliente"
        Me.chkControlBanderasCliente.Properties.Caption = ""
        Me.chkControlBanderasCliente.Size = New System.Drawing.Size(28, 24)
        Me.chkControlBanderasCliente.TabIndex = 45
        Me.chkControlBanderasCliente.ToolTip = " If Requerir_Cliente_Es_Bodega_WMS Then"
        '
        'chkDespacharProductoVencido
        '
        Me.chkDespacharProductoVencido.Location = New System.Drawing.Point(336, 48)
        Me.chkDespacharProductoVencido.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkDespacharProductoVencido.Name = "chkDespacharProductoVencido"
        Me.chkDespacharProductoVencido.Properties.Caption = ""
        Me.chkDespacharProductoVencido.Size = New System.Drawing.Size(28, 24)
        Me.chkDespacharProductoVencido.TabIndex = 69
        Me.chkDespacharProductoVencido.ToolTip = "#EJC20220330: Si true, se permite realizar el cambio de ubicación de producto que" &
    " está reservado en picking pero se actualiza el IdUbicacionTemporal"
        '
        'chkPermitirReemplazoVerificacion
        '
        Me.chkPermitirReemplazoVerificacion.Location = New System.Drawing.Point(336, 227)
        Me.chkPermitirReemplazoVerificacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkPermitirReemplazoVerificacion.Name = "chkPermitirReemplazoVerificacion"
        Me.chkPermitirReemplazoVerificacion.Properties.Caption = ""
        Me.chkPermitirReemplazoVerificacion.Size = New System.Drawing.Size(28, 24)
        Me.chkPermitirReemplazoVerificacion.TabIndex = 98
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(10, 84)
        Me.Label65.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(235, 16)
        Me.Label65.TabIndex = 80
        Me.Label65.Text = "Días Máximo Vencimiento en reemplazo"
        '
        'chkPermitir_Verificacion_Consolidada
        '
        Me.chkPermitir_Verificacion_Consolidada.Location = New System.Drawing.Point(686, 14)
        Me.chkPermitir_Verificacion_Consolidada.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkPermitir_Verificacion_Consolidada.Name = "chkPermitir_Verificacion_Consolidada"
        Me.chkPermitir_Verificacion_Consolidada.Properties.Caption = ""
        Me.chkPermitir_Verificacion_Consolidada.Size = New System.Drawing.Size(23, 24)
        Me.chkPermitir_Verificacion_Consolidada.TabIndex = 43
        '
        'txtDiasMaximoVencimientoReemplazo
        '
        Me.txtDiasMaximoVencimientoReemplazo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiasMaximoVencimientoReemplazo.Location = New System.Drawing.Point(304, 78)
        Me.txtDiasMaximoVencimientoReemplazo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDiasMaximoVencimientoReemplazo.Maximum = New Decimal(New Integer() {365, 0, 0, 0})
        Me.txtDiasMaximoVencimientoReemplazo.Name = "txtDiasMaximoVencimientoReemplazo"
        Me.txtDiasMaximoVencimientoReemplazo.Size = New System.Drawing.Size(49, 23)
        Me.txtDiasMaximoVencimientoReemplazo.TabIndex = 81
        '
        'chkdespachoautohh
        '
        Me.chkdespachoautohh.Location = New System.Drawing.Point(336, 197)
        Me.chkdespachoautohh.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkdespachoautohh.Name = "chkdespachoautohh"
        Me.chkdespachoautohh.Properties.Caption = ""
        Me.chkdespachoautohh.Size = New System.Drawing.Size(28, 24)
        Me.chkdespachoautohh.TabIndex = 116
        Me.chkdespachoautohh.ToolTip = "Restringir áeras de SAP"
        '
        'chkPermitirEliminarDocumentosSalida
        '
        Me.chkPermitirEliminarDocumentosSalida.Location = New System.Drawing.Point(336, 107)
        Me.chkPermitirEliminarDocumentosSalida.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkPermitirEliminarDocumentosSalida.Name = "chkPermitirEliminarDocumentosSalida"
        Me.chkPermitirEliminarDocumentosSalida.Properties.Caption = ""
        Me.chkPermitirEliminarDocumentosSalida.Size = New System.Drawing.Size(28, 24)
        Me.chkPermitirEliminarDocumentosSalida.TabIndex = 53
        Me.chkPermitirEliminarDocumentosSalida.ToolTip = "  '#EJC20220129: Validar si la ubicación destino tiene producto o está ""libre"" an" &
    "tes de colocar producto allí"
        '
        'chkEliminarDocumentosSalida
        '
        Me.chkEliminarDocumentosSalida.Location = New System.Drawing.Point(336, 137)
        Me.chkEliminarDocumentosSalida.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkEliminarDocumentosSalida.Name = "chkEliminarDocumentosSalida"
        Me.chkEliminarDocumentosSalida.Properties.Caption = ""
        Me.chkEliminarDocumentosSalida.Size = New System.Drawing.Size(28, 24)
        Me.chkEliminarDocumentosSalida.TabIndex = 63
        Me.chkEliminarDocumentosSalida.ToolTip = "  '#EJC20220129: Validar si la ubicación destino tiene producto o está ""libre"" an" &
    "tes de colocar producto allí"
        '
        'chkFiltrarPedidosUsuario
        '
        Me.chkFiltrarPedidosUsuario.Location = New System.Drawing.Point(336, 167)
        Me.chkFiltrarPedidosUsuario.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkFiltrarPedidosUsuario.Name = "chkFiltrarPedidosUsuario"
        Me.chkFiltrarPedidosUsuario.Properties.Caption = ""
        Me.chkFiltrarPedidosUsuario.Size = New System.Drawing.Size(28, 24)
        Me.chkFiltrarPedidosUsuario.TabIndex = 102
        '
        'lblCambioUbicacionRestrictivo
        '
        Me.lblCambioUbicacionRestrictivo.AutoSize = True
        Me.lblCambioUbicacionRestrictivo.Location = New System.Drawing.Point(381, 190)
        Me.lblCambioUbicacionRestrictivo.Name = "lblCambioUbicacionRestrictivo"
        Me.lblCambioUbicacionRestrictivo.Size = New System.Drawing.Size(167, 16)
        Me.lblCambioUbicacionRestrictivo.TabIndex = 132
        Me.lblCambioUbicacionRestrictivo.Text = "Cambio ubicación restrictivo"
        '
        'chkCambioUbicacionRestrictivo
        '
        Me.chkCambioUbicacionRestrictivo.Location = New System.Drawing.Point(686, 188)
        Me.chkCambioUbicacionRestrictivo.Name = "chkCambioUbicacionRestrictivo"
        Me.chkCambioUbicacionRestrictivo.Properties.Caption = ""
        Me.chkCambioUbicacionRestrictivo.Size = New System.Drawing.Size(23, 24)
        Me.chkCambioUbicacionRestrictivo.TabIndex = 128
        '
        'lblPermitirCambioUbicIndiceMenor
        '
        Me.lblPermitirCambioUbicIndiceMenor.AutoSize = True
        Me.lblPermitirCambioUbicIndiceMenor.Location = New System.Drawing.Point(381, 218)
        Me.lblPermitirCambioUbicIndiceMenor.Name = "lblPermitirCambioUbicIndiceMenor"
        Me.lblPermitirCambioUbicIndiceMenor.Size = New System.Drawing.Size(202, 16)
        Me.lblPermitirCambioUbicIndiceMenor.TabIndex = 133
        Me.lblPermitirCambioUbicIndiceMenor.Text = "Permitir cambio ubic índice menor"
        '
        'chkPermitirCambioUbicIndiceMenor
        '
        Me.chkPermitirCambioUbicIndiceMenor.Location = New System.Drawing.Point(686, 217)
        Me.chkPermitirCambioUbicIndiceMenor.Name = "chkPermitirCambioUbicIndiceMenor"
        Me.chkPermitirCambioUbicIndiceMenor.Properties.Caption = ""
        Me.chkPermitirCambioUbicIndiceMenor.Size = New System.Drawing.Size(23, 24)
        Me.chkPermitirCambioUbicIndiceMenor.TabIndex = 129
        '
        'lblRequerirMismoProductoPosiciones
        '
        Me.lblRequerirMismoProductoPosiciones.AutoSize = True
        Me.lblRequerirMismoProductoPosiciones.Location = New System.Drawing.Point(381, 246)
        Me.lblRequerirMismoProductoPosiciones.Name = "lblRequerirMismoProductoPosiciones"
        Me.lblRequerirMismoProductoPosiciones.Size = New System.Drawing.Size(233, 16)
        Me.lblRequerirMismoProductoPosiciones.TabIndex = 134
        Me.lblRequerirMismoProductoPosiciones.Text = "Requerir mismo producto en posiciones"
        '
        'chkRequerirMismoProductoPosiciones
        '
        Me.chkRequerirMismoProductoPosiciones.Location = New System.Drawing.Point(686, 246)
        Me.chkRequerirMismoProductoPosiciones.Name = "chkRequerirMismoProductoPosiciones"
        Me.chkRequerirMismoProductoPosiciones.Properties.Caption = ""
        Me.chkRequerirMismoProductoPosiciones.Size = New System.Drawing.Size(23, 24)
        Me.chkRequerirMismoProductoPosiciones.TabIndex = 130
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Label31)
        Me.GroupBox3.Controls.Add(Me.chkCambioUbiAuto)
        Me.GroupBox3.Controls.Add(Label48)
        Me.GroupBox3.Controls.Add(Label49)
        Me.GroupBox3.Controls.Add(Me.chkControlTarifaServ)
        Me.GroupBox3.Controls.Add(lblEsBodegaFiscal)
        Me.GroupBox3.Controls.Add(Me.chkEsBodegaFiscal)
        Me.GroupBox3.Controls.Add(lblLimpiarCampos)
        Me.GroupBox3.Controls.Add(Me.chkLimpiarCamposHH)
        Me.GroupBox3.Controls.Add(Me.chkNotificacionVoz)
        Me.GroupBox3.Controls.Add(Label59)
        Me.GroupBox3.Controls.Add(Me.chkEsMotriz)
        Me.GroupBox3.Controls.Add(Me.chkRestringirAreasSAP)
        Me.GroupBox3.Controls.Add(Label78)
        Me.GroupBox3.Controls.Add(lblMostrarZonaEnHH)
        Me.GroupBox3.Controls.Add(Me.chkMostrarAreaEnHH)
        Me.GroupBox3.Controls.Add(Me.chkInterface_SAP)
        Me.GroupBox3.Controls.Add(Label77)
        Me.GroupBox3.Controls.Add(lblcalcular_ubicacion_sugerida_ml)
        Me.GroupBox3.Controls.Add(Me.chkcalcular_ubicacion_sugerida_ml)
        Me.GroupBox3.Controls.Add(Label64)
        Me.GroupBox3.Controls.Add(Me.chkPermitirDecimales)
        Me.GroupBox3.Location = New System.Drawing.Point(10, 37)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox3.Size = New System.Drawing.Size(410, 347)
        Me.GroupBox3.TabIndex = 122
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Generales"
        '
        'chkCambioUbiAuto
        '
        Me.chkCambioUbiAuto.EditValue = True
        Me.chkCambioUbiAuto.Location = New System.Drawing.Point(382, 32)
        Me.chkCambioUbiAuto.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkCambioUbiAuto.Name = "chkCambioUbiAuto"
        Me.chkCambioUbiAuto.Properties.Caption = ""
        Me.chkCambioUbiAuto.Size = New System.Drawing.Size(28, 24)
        Me.chkCambioUbiAuto.TabIndex = 23
        '
        'chkControlTarifaServ
        '
        Me.chkControlTarifaServ.Location = New System.Drawing.Point(382, 90)
        Me.chkControlTarifaServ.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkControlTarifaServ.Name = "chkControlTarifaServ"
        Me.chkControlTarifaServ.Properties.Caption = ""
        Me.chkControlTarifaServ.Size = New System.Drawing.Size(28, 24)
        Me.chkControlTarifaServ.TabIndex = 31
        '
        'chkEsBodegaFiscal
        '
        Me.chkEsBodegaFiscal.Location = New System.Drawing.Point(382, 118)
        Me.chkEsBodegaFiscal.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkEsBodegaFiscal.Name = "chkEsBodegaFiscal"
        Me.chkEsBodegaFiscal.Properties.Caption = ""
        Me.chkEsBodegaFiscal.Size = New System.Drawing.Size(28, 24)
        Me.chkEsBodegaFiscal.TabIndex = 33
        '
        'chkLimpiarCamposHH
        '
        Me.chkLimpiarCamposHH.Location = New System.Drawing.Point(382, 316)
        Me.chkLimpiarCamposHH.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkLimpiarCamposHH.Name = "chkLimpiarCamposHH"
        Me.chkLimpiarCamposHH.Properties.Caption = ""
        Me.chkLimpiarCamposHH.Size = New System.Drawing.Size(28, 24)
        Me.chkLimpiarCamposHH.TabIndex = 118
        Me.chkLimpiarCamposHH.ToolTip = "Restringir áeras de SAP"
        '
        'chkNotificacionVoz
        '
        Me.chkNotificacionVoz.EditValue = True
        Me.chkNotificacionVoz.Location = New System.Drawing.Point(382, 60)
        Me.chkNotificacionVoz.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkNotificacionVoz.Name = "chkNotificacionVoz"
        Me.chkNotificacionVoz.Properties.Caption = ""
        Me.chkNotificacionVoz.Size = New System.Drawing.Size(28, 24)
        Me.chkNotificacionVoz.TabIndex = 29
        '
        'chkEsMotriz
        '
        Me.chkEsMotriz.Location = New System.Drawing.Point(382, 144)
        Me.chkEsMotriz.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkEsMotriz.Name = "chkEsMotriz"
        Me.chkEsMotriz.Properties.Caption = ""
        Me.chkEsMotriz.Size = New System.Drawing.Size(28, 24)
        Me.chkEsMotriz.TabIndex = 73
        '
        'chkRestringirAreasSAP
        '
        Me.chkRestringirAreasSAP.Location = New System.Drawing.Point(382, 286)
        Me.chkRestringirAreasSAP.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkRestringirAreasSAP.Name = "chkRestringirAreasSAP"
        Me.chkRestringirAreasSAP.Properties.Caption = ""
        Me.chkRestringirAreasSAP.Size = New System.Drawing.Size(28, 24)
        Me.chkRestringirAreasSAP.TabIndex = 112
        Me.chkRestringirAreasSAP.ToolTip = "Restringir áeras de SAP"
        '
        'chkMostrarAreaEnHH
        '
        Me.chkMostrarAreaEnHH.Location = New System.Drawing.Point(382, 174)
        Me.chkMostrarAreaEnHH.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkMostrarAreaEnHH.Name = "chkMostrarAreaEnHH"
        Me.chkMostrarAreaEnHH.Properties.Caption = ""
        Me.chkMostrarAreaEnHH.Size = New System.Drawing.Size(28, 24)
        Me.chkMostrarAreaEnHH.TabIndex = 55
        Me.chkMostrarAreaEnHH.ToolTip = "  '#EJC20220129: Validar si la ubicación destino tiene producto o está ""libre"" an" &
    "tes de colocar producto allí"
        '
        'chkInterface_SAP
        '
        Me.chkInterface_SAP.Location = New System.Drawing.Point(382, 260)
        Me.chkInterface_SAP.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkInterface_SAP.Name = "chkInterface_SAP"
        Me.chkInterface_SAP.Properties.Caption = ""
        Me.chkInterface_SAP.Size = New System.Drawing.Size(28, 24)
        Me.chkInterface_SAP.TabIndex = 110
        '
        'chkcalcular_ubicacion_sugerida_ml
        '
        Me.chkcalcular_ubicacion_sugerida_ml.Location = New System.Drawing.Point(382, 203)
        Me.chkcalcular_ubicacion_sugerida_ml.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkcalcular_ubicacion_sugerida_ml.Name = "chkcalcular_ubicacion_sugerida_ml"
        Me.chkcalcular_ubicacion_sugerida_ml.Properties.Caption = ""
        Me.chkcalcular_ubicacion_sugerida_ml.Size = New System.Drawing.Size(28, 24)
        Me.chkcalcular_ubicacion_sugerida_ml.TabIndex = 88
        Me.chkcalcular_ubicacion_sugerida_ml.ToolTip = " If Permitir_Decimales entonces en la HH en los procesos de recepción, cambios de" &
    " ubicación y estado lo va a permitir"
        '
        'chkPermitirDecimales
        '
        Me.chkPermitirDecimales.Location = New System.Drawing.Point(382, 231)
        Me.chkPermitirDecimales.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkPermitirDecimales.Name = "chkPermitirDecimales"
        Me.chkPermitirDecimales.Properties.Caption = ""
        Me.chkPermitirDecimales.Size = New System.Drawing.Size(28, 24)
        Me.chkPermitirDecimales.TabIndex = 79
        Me.chkPermitirDecimales.ToolTip = " If Permitir_Decimales entonces en la HH en los procesos de recepción, cambios de" &
    " ubicación y estado lo va a permitir"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label95)
        Me.GroupBox2.Controls.Add(Me.chkControlGuia)
        Me.GroupBox2.Controls.Add(lblPickeadorVerifica)
        Me.GroupBox2.Controls.Add(Me.chkOperadorPickingVerifica)
        Me.GroupBox2.Controls.Add(Label72)
        Me.GroupBox2.Controls.Add(Me.chkPermitirNoEncontradoPicking)
        Me.GroupBox2.Controls.Add(lblPermitirReemplazoPickingMismaLIcencia)
        Me.GroupBox2.Controls.Add(Me.chkPermitirReemplazoPickingMismaLIcencia)
        Me.GroupBox2.Controls.Add(Me.lblEscanearLicenciaPicking)
        Me.GroupBox2.Controls.Add(Me.chkEscanearLicenciaPicking)
        Me.GroupBox2.Controls.Add(Label70)
        Me.GroupBox2.Controls.Add(Me.chkPermitirReemplazoPicking)
        Me.GroupBox2.Controls.Add(Label58)
        Me.GroupBox2.Controls.Add(Me.chkpermitir_buen_estado_en_reemplazo)
        Me.GroupBox2.Controls.Add(lblEscanearCodigoProductoEnPicking)
        Me.GroupBox2.Controls.Add(Me.chkEscanearCodigoProductoEnPicking)
        Me.GroupBox2.Controls.Add(lblPermitirCambioDeUbicacionEnPicking)
        Me.GroupBox2.Controls.Add(Me.chkPermitirCambioUbicacionPicking)
        Me.GroupBox2.Controls.Add(Label68)
        Me.GroupBox2.Controls.Add(Me.chkOrdenarPickingDescendente)
        Me.GroupBox2.Controls.Add(Label69)
        Me.GroupBox2.Controls.Add(Me.chkOrdenarNombreCompleto)
        Me.GroupBox2.Location = New System.Drawing.Point(429, 37)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(369, 347)
        Me.GroupBox2.TabIndex = 121
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Picking"
        '
        'Label95
        '
        Me.Label95.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label95.AutoSize = True
        Me.Label95.Location = New System.Drawing.Point(12, 305)
        Me.Label95.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(76, 16)
        Me.Label95.TabIndex = 127
        Me.Label95.Text = "Control guía"
        '
        'chkControlGuia
        '
        Me.chkControlGuia.Location = New System.Drawing.Point(344, 301)
        Me.chkControlGuia.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkControlGuia.Name = "chkControlGuia"
        Me.chkControlGuia.Properties.Caption = ""
        Me.chkControlGuia.Size = New System.Drawing.Size(28, 24)
        Me.chkControlGuia.TabIndex = 126
        Me.chkControlGuia.ToolTip = "Restringir áeras de SAP"
        '
        'chkOperadorPickingVerifica
        '
        Me.chkOperadorPickingVerifica.Location = New System.Drawing.Point(341, 21)
        Me.chkOperadorPickingVerifica.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkOperadorPickingVerifica.Name = "chkOperadorPickingVerifica"
        Me.chkOperadorPickingVerifica.Properties.Caption = ""
        Me.chkOperadorPickingVerifica.Size = New System.Drawing.Size(29, 24)
        Me.chkOperadorPickingVerifica.TabIndex = 65
        Me.chkOperadorPickingVerifica.ToolTip = "  '#EJC20220330: Si se habilita, entonces el operador que realizó el picking, ser" &
    "á el único operador que podrá realizar la tarea de verificación."
        '
        'chkPermitirNoEncontradoPicking
        '
        Me.chkPermitirNoEncontradoPicking.Location = New System.Drawing.Point(341, 49)
        Me.chkPermitirNoEncontradoPicking.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkPermitirNoEncontradoPicking.Name = "chkPermitirNoEncontradoPicking"
        Me.chkPermitirNoEncontradoPicking.Properties.Caption = ""
        Me.chkPermitirNoEncontradoPicking.Size = New System.Drawing.Size(29, 24)
        Me.chkPermitirNoEncontradoPicking.TabIndex = 99
        '
        'chkPermitirReemplazoPickingMismaLIcencia
        '
        Me.chkPermitirReemplazoPickingMismaLIcencia.Location = New System.Drawing.Point(341, 79)
        Me.chkPermitirReemplazoPickingMismaLIcencia.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkPermitirReemplazoPickingMismaLIcencia.Name = "chkPermitirReemplazoPickingMismaLIcencia"
        Me.chkPermitirReemplazoPickingMismaLIcencia.Properties.Caption = ""
        Me.chkPermitirReemplazoPickingMismaLIcencia.Size = New System.Drawing.Size(29, 24)
        Me.chkPermitirReemplazoPickingMismaLIcencia.TabIndex = 100
        '
        'chkEscanearLicenciaPicking
        '
        Me.chkEscanearLicenciaPicking.Location = New System.Drawing.Point(341, 110)
        Me.chkEscanearLicenciaPicking.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkEscanearLicenciaPicking.Name = "chkEscanearLicenciaPicking"
        Me.chkEscanearLicenciaPicking.Properties.Caption = ""
        Me.chkEscanearLicenciaPicking.Size = New System.Drawing.Size(29, 24)
        Me.chkEscanearLicenciaPicking.TabIndex = 108
        '
        'chkPermitirReemplazoPicking
        '
        Me.chkPermitirReemplazoPicking.Location = New System.Drawing.Point(341, 137)
        Me.chkPermitirReemplazoPicking.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkPermitirReemplazoPicking.Name = "chkPermitirReemplazoPicking"
        Me.chkPermitirReemplazoPicking.Properties.Caption = ""
        Me.chkPermitirReemplazoPicking.Size = New System.Drawing.Size(29, 24)
        Me.chkPermitirReemplazoPicking.TabIndex = 94
        '
        'chkpermitir_buen_estado_en_reemplazo
        '
        Me.chkpermitir_buen_estado_en_reemplazo.Location = New System.Drawing.Point(341, 165)
        Me.chkpermitir_buen_estado_en_reemplazo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkpermitir_buen_estado_en_reemplazo.Name = "chkpermitir_buen_estado_en_reemplazo"
        Me.chkpermitir_buen_estado_en_reemplazo.Properties.Caption = ""
        Me.chkpermitir_buen_estado_en_reemplazo.Size = New System.Drawing.Size(29, 24)
        Me.chkpermitir_buen_estado_en_reemplazo.TabIndex = 71
        Me.chkpermitir_buen_estado_en_reemplazo.ToolTip = " If Requerir_Cliente_Es_Bodega_WMS Then"
        '
        'chkEscanearCodigoProductoEnPicking
        '
        Me.chkEscanearCodigoProductoEnPicking.Location = New System.Drawing.Point(341, 195)
        Me.chkEscanearCodigoProductoEnPicking.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkEscanearCodigoProductoEnPicking.Name = "chkEscanearCodigoProductoEnPicking"
        Me.chkEscanearCodigoProductoEnPicking.Properties.Caption = ""
        Me.chkEscanearCodigoProductoEnPicking.Size = New System.Drawing.Size(29, 24)
        Me.chkEscanearCodigoProductoEnPicking.TabIndex = 59
        Me.chkEscanearCodigoProductoEnPicking.ToolTip = "  '#EJC20220129: Validar si la ubicación destino tiene producto o está ""libre"" an" &
    "tes de colocar producto allí"
        '
        'chkPermitirCambioUbicacionPicking
        '
        Me.chkPermitirCambioUbicacionPicking.Location = New System.Drawing.Point(341, 220)
        Me.chkPermitirCambioUbicacionPicking.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkPermitirCambioUbicacionPicking.Name = "chkPermitirCambioUbicacionPicking"
        Me.chkPermitirCambioUbicacionPicking.Properties.Caption = ""
        Me.chkPermitirCambioUbicacionPicking.Size = New System.Drawing.Size(29, 24)
        Me.chkPermitirCambioUbicacionPicking.TabIndex = 67
        Me.chkPermitirCambioUbicacionPicking.ToolTip = "#EJC20220330: Si true, se permite realizar el cambio de ubicación de producto que" &
    " está reservado en picking pero se actualiza el IdUbicacionTemporal"
        '
        'chkOrdenarPickingDescendente
        '
        Me.chkOrdenarPickingDescendente.Location = New System.Drawing.Point(341, 247)
        Me.chkOrdenarPickingDescendente.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkOrdenarPickingDescendente.Name = "chkOrdenarPickingDescendente"
        Me.chkOrdenarPickingDescendente.Properties.Caption = ""
        Me.chkOrdenarPickingDescendente.Size = New System.Drawing.Size(29, 24)
        Me.chkOrdenarPickingDescendente.TabIndex = 90
        Me.chkOrdenarPickingDescendente.ToolTip = "#EJC20220912: Si true, se permite recibir X cantidad de copias de un producto en " &
    "recepción que cumpla con los mismos parámetros si el usuario tiene resolución de" &
    " licencias definidas."
        '
        'chkOrdenarNombreCompleto
        '
        Me.chkOrdenarNombreCompleto.Location = New System.Drawing.Point(341, 275)
        Me.chkOrdenarNombreCompleto.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkOrdenarNombreCompleto.Name = "chkOrdenarNombreCompleto"
        Me.chkOrdenarNombreCompleto.Properties.Caption = ""
        Me.chkOrdenarNombreCompleto.Size = New System.Drawing.Size(29, 24)
        Me.chkOrdenarNombreCompleto.TabIndex = 92
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbEstadoDefectoRack)
        Me.GroupBox1.Controls.Add(Me.Label81)
        Me.GroupBox1.Controls.Add(Me.Label94)
        Me.GroupBox1.Controls.Add(Me.chkPermitirCambioUbicacionRecepcion)
        Me.GroupBox1.Controls.Add(Me.chkBloquearLpHH)
        Me.GroupBox1.Controls.Add(Label51)
        Me.GroupBox1.Controls.Add(lblHabilitarIngresoConsolidado)
        Me.GroupBox1.Controls.Add(Me.chkIngresoConsolidado)
        Me.GroupBox1.Controls.Add(lblCapturaPalletNoEstandar)
        Me.GroupBox1.Controls.Add(Me.chkPriorizar_UbicRec_Sobre_UbicEst)
        Me.GroupBox1.Controls.Add(Me.chkCapturaPalletNoEstandar)
        Me.GroupBox1.Controls.Add(lblPriorizar)
        Me.GroupBox1.Controls.Add(lblCapturaEstibaIngreso)
        Me.GroupBox1.Controls.Add(Me.chkCapturaEstibaIngreso)
        Me.GroupBox1.Controls.Add(Label67)
        Me.GroupBox1.Controls.Add(Me.chkPermitirRepeticionesEnIngreso)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 390)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(412, 316)
        Me.GroupBox1.TabIndex = 120
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Recepcion"
        '
        'cmbEstadoDefectoRack
        '
        Me.cmbEstadoDefectoRack.Location = New System.Drawing.Point(283, 249)
        Me.cmbEstadoDefectoRack.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbEstadoDefectoRack.Name = "cmbEstadoDefectoRack"
        Me.cmbEstadoDefectoRack.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEstadoDefectoRack.Properties.NullText = ""
        Me.cmbEstadoDefectoRack.Size = New System.Drawing.Size(124, 22)
        Me.cmbEstadoDefectoRack.TabIndex = 132
        '
        'Label94
        '
        Me.Label94.AutoSize = True
        Me.Label94.Location = New System.Drawing.Point(10, 252)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(119, 16)
        Me.Label94.TabIndex = 130
        Me.Label94.Text = "Estado defecto rack"
        '
        'chkPermitirCambioUbicacionRecepcion
        '
        Me.chkPermitirCambioUbicacionRecepcion.Location = New System.Drawing.Point(384, 219)
        Me.chkPermitirCambioUbicacionRecepcion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkPermitirCambioUbicacionRecepcion.Name = "chkPermitirCambioUbicacionRecepcion"
        Me.chkPermitirCambioUbicacionRecepcion.Properties.Caption = ""
        Me.chkPermitirCambioUbicacionRecepcion.Size = New System.Drawing.Size(24, 24)
        Me.chkPermitirCambioUbicacionRecepcion.TabIndex = 87
        Me.chkPermitirCambioUbicacionRecepcion.ToolTip = "#EJC20220912: Si true, se permite recibir X cantidad de copias de un producto en " &
    "recepción que cumpla con los mismos parámetros si el usuario tiene resolución de" &
    " licencias definidas."
        '
        'chkBloquearLpHH
        '
        Me.chkBloquearLpHH.Location = New System.Drawing.Point(384, 28)
        Me.chkBloquearLpHH.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkBloquearLpHH.Name = "chkBloquearLpHH"
        Me.chkBloquearLpHH.Properties.Caption = ""
        Me.chkBloquearLpHH.Size = New System.Drawing.Size(24, 24)
        Me.chkBloquearLpHH.TabIndex = 37
        '
        'chkIngresoConsolidado
        '
        Me.chkIngresoConsolidado.Location = New System.Drawing.Point(384, 59)
        Me.chkIngresoConsolidado.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkIngresoConsolidado.Name = "chkIngresoConsolidado"
        Me.chkIngresoConsolidado.Properties.Caption = ""
        Me.chkIngresoConsolidado.Size = New System.Drawing.Size(24, 24)
        Me.chkIngresoConsolidado.TabIndex = 35
        '
        'chkPriorizar_UbicRec_Sobre_UbicEst
        '
        Me.chkPriorizar_UbicRec_Sobre_UbicEst.Location = New System.Drawing.Point(384, 185)
        Me.chkPriorizar_UbicRec_Sobre_UbicEst.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkPriorizar_UbicRec_Sobre_UbicEst.Name = "chkPriorizar_UbicRec_Sobre_UbicEst"
        Me.chkPriorizar_UbicRec_Sobre_UbicEst.Properties.Caption = ""
        Me.chkPriorizar_UbicRec_Sobre_UbicEst.Size = New System.Drawing.Size(24, 24)
        Me.chkPriorizar_UbicRec_Sobre_UbicEst.TabIndex = 49
        Me.chkPriorizar_UbicRec_Sobre_UbicEst.ToolTip = " If Requerir_Cliente_Es_Bodega_WMS Then"
        '
        'chkCapturaPalletNoEstandar
        '
        Me.chkCapturaPalletNoEstandar.Location = New System.Drawing.Point(384, 91)
        Me.chkCapturaPalletNoEstandar.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkCapturaPalletNoEstandar.Name = "chkCapturaPalletNoEstandar"
        Me.chkCapturaPalletNoEstandar.Properties.Caption = ""
        Me.chkCapturaPalletNoEstandar.Size = New System.Drawing.Size(24, 24)
        Me.chkCapturaPalletNoEstandar.TabIndex = 40
        '
        'chkCapturaEstibaIngreso
        '
        Me.chkCapturaEstibaIngreso.Location = New System.Drawing.Point(384, 124)
        Me.chkCapturaEstibaIngreso.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkCapturaEstibaIngreso.Name = "chkCapturaEstibaIngreso"
        Me.chkCapturaEstibaIngreso.Properties.Caption = ""
        Me.chkCapturaEstibaIngreso.Size = New System.Drawing.Size(24, 24)
        Me.chkCapturaEstibaIngreso.TabIndex = 41
        '
        'chkPermitirRepeticionesEnIngreso
        '
        Me.chkPermitirRepeticionesEnIngreso.Location = New System.Drawing.Point(384, 156)
        Me.chkPermitirRepeticionesEnIngreso.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkPermitirRepeticionesEnIngreso.Name = "chkPermitirRepeticionesEnIngreso"
        Me.chkPermitirRepeticionesEnIngreso.Properties.Caption = ""
        Me.chkPermitirRepeticionesEnIngreso.Size = New System.Drawing.Size(24, 24)
        Me.chkPermitirRepeticionesEnIngreso.TabIndex = 83
        Me.chkPermitirRepeticionesEnIngreso.ToolTip = "#EJC20220912: Si true, se permite recibir X cantidad de copias de un producto en " &
    "recepción que cumpla con los mismos parámetros si el usuario tiene resolución de" &
    " licencias definidas."
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.cmbEtiquetaVerificacion)
        Me.GroupControl2.Controls.Add(Me.Label99)
        Me.GroupControl2.Controls.Add(Me.Bcc)
        Me.GroupControl2.Controls.Add(Me.cmbSymbology)
        Me.GroupControl2.Controls.Add(lblSimbologia)
        Me.GroupControl2.Controls.Add(Me.cmbEtiqueta)
        Me.GroupControl2.Controls.Add(Me.lblEtiqueta)
        Me.GroupControl2.Controls.Add(Me.Label74)
        Me.GroupControl2.Controls.Add(Me.dtHorarioEjecucionHistorico)
        Me.GroupControl2.Controls.Add(Me.txtIdDiasLimiteRetroactivo)
        Me.GroupControl2.Controls.Add(Me.Label73)
        Me.GroupControl2.Controls.Add(Me.Label63)
        Me.GroupControl2.Controls.Add(Me.Label62)
        Me.GroupControl2.Controls.Add(Me.Label61)
        Me.GroupControl2.Controls.Add(Me.nudTopReabastecimientoManual)
        Me.GroupControl2.Controls.Add(Me.Label60)
        Me.GroupControl2.Controls.Add(Me.txtIdConfiguracionPantallaRecepcion)
        Me.GroupControl2.Controls.Add(Me.Label57)
        Me.GroupControl2.Controls.Add(Me.txtIdConfiguracionPantallaVerificacion)
        Me.GroupControl2.Controls.Add(Me.Label56)
        Me.GroupControl2.Controls.Add(Me.txtIdConfiguracionPantallaPicking)
        Me.GroupControl2.Controls.Add(Me.Label55)
        Me.GroupControl2.Controls.Add(Me.txtValorIVA)
        Me.GroupControl2.Controls.Add(Me.lblValorIVA)
        Me.GroupControl2.Controls.Add(Me.txtIdMotivoUbicReabasto)
        Me.GroupControl2.Controls.Add(Me.txtMotivoUbicReabasto)
        Me.GroupControl2.Controls.Add(Me.lnkReabasto)
        Me.GroupControl2.Controls.Add(Me.Label35)
        Me.GroupControl2.Controls.Add(Me.cmbEstadoNe)
        Me.GroupControl2.Controls.Add(Me.txtIdUbicacionPrdNE)
        Me.GroupControl2.Controls.Add(Me.txtNombreUbicNE)
        Me.GroupControl2.Controls.Add(Me.lnkUbicPrdNE)
        Me.GroupControl2.Controls.Add(Me.txtidmotivoubicaciondañadopicking)
        Me.GroupControl2.Controls.Add(Me.txtMotivoUbicacionDañadoPicking)
        Me.GroupControl2.Controls.Add(Me.lblDañadoPicking)
        Me.GroupControl2.Controls.Add(Me.txtNombreUbicacionMerma)
        Me.GroupControl2.Controls.Add(Me.lnkUbicacionMerma)
        Me.GroupControl2.Controls.Add(Me.txtIdUbicacionMerma)
        Me.GroupControl2.Controls.Add(Me.txtNombreUbicacionDespacho)
        Me.GroupControl2.Controls.Add(Me.lnkUbicacionDespacho)
        Me.GroupControl2.Controls.Add(Me.txtIdUbicacionDespacho)
        Me.GroupControl2.Controls.Add(Me.txtNombreUbicacionPicking)
        Me.GroupControl2.Controls.Add(Me.lnkUbicacionPicking)
        Me.GroupControl2.Controls.Add(Me.txtIdUbicacionPicking)
        Me.GroupControl2.Controls.Add(Me.txtNombreUbicacionRecepcion)
        Me.GroupControl2.Controls.Add(Me.lnkUbicacionRecepcion)
        Me.GroupControl2.Controls.Add(Me.txtIdUbicacionRecepcion)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(612, 711)
        Me.GroupControl2.TabIndex = 0
        Me.GroupControl2.Text = "Ubicaciones por defecto"
        '
        'cmbEtiquetaVerificacion
        '
        Me.cmbEtiquetaVerificacion.Location = New System.Drawing.Point(258, 670)
        Me.cmbEtiquetaVerificacion.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbEtiquetaVerificacion.Name = "cmbEtiquetaVerificacion"
        Me.cmbEtiquetaVerificacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEtiquetaVerificacion.Properties.NullText = ""
        Me.cmbEtiquetaVerificacion.Size = New System.Drawing.Size(124, 22)
        Me.cmbEtiquetaVerificacion.TabIndex = 49
        '
        'Label99
        '
        Me.Label99.AutoSize = True
        Me.Label99.Location = New System.Drawing.Point(21, 670)
        Me.Label99.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(123, 16)
        Me.Label99.TabIndex = 48
        Me.Label99.Text = "Etiqueta Verificación"
        '
        'Bcc
        '
        Me.Bcc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Bcc.Location = New System.Drawing.Point(396, 608)
        Me.Bcc.Margin = New System.Windows.Forms.Padding(4)
        Me.Bcc.Name = "Bcc"
        Me.Bcc.Padding = New System.Windows.Forms.Padding(12, 2, 12, 0)
        Me.Bcc.Size = New System.Drawing.Size(195, 53)
        Me.Bcc.Symbology = Code128Generator1
        Me.Bcc.TabIndex = 45
        '
        'cmbSymbology
        '
        Me.cmbSymbology.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbSymbology.Location = New System.Drawing.Point(261, 640)
        Me.cmbSymbology.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbSymbology.Name = "cmbSymbology"
        Me.cmbSymbology.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbSymbology.Properties.NullText = ""
        Me.cmbSymbology.Size = New System.Drawing.Size(122, 22)
        Me.cmbSymbology.TabIndex = 44
        '
        'cmbEtiqueta
        '
        Me.cmbEtiqueta.Location = New System.Drawing.Point(260, 606)
        Me.cmbEtiqueta.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbEtiqueta.Name = "cmbEtiqueta"
        Me.cmbEtiqueta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEtiqueta.Properties.NullText = ""
        Me.cmbEtiqueta.Size = New System.Drawing.Size(124, 22)
        Me.cmbEtiqueta.TabIndex = 42
        '
        'lblEtiqueta
        '
        Me.lblEtiqueta.AutoSize = True
        Me.lblEtiqueta.Location = New System.Drawing.Point(26, 610)
        Me.lblEtiqueta.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEtiqueta.Name = "lblEtiqueta"
        Me.lblEtiqueta.Size = New System.Drawing.Size(99, 16)
        Me.lblEtiqueta.TabIndex = 41
        Me.lblEtiqueta.Text = "Etiqueta licencia"
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.Location = New System.Drawing.Point(26, 580)
        Me.Label74.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(177, 16)
        Me.Label74.TabIndex = 39
        Me.Label74.Text = "Horario ejecución de histórico"
        '
        'dtHorarioEjecucionHistorico
        '
        Me.dtHorarioEjecucionHistorico.EditValue = Nothing
        Me.dtHorarioEjecucionHistorico.Location = New System.Drawing.Point(260, 574)
        Me.dtHorarioEjecucionHistorico.Margin = New System.Windows.Forms.Padding(4)
        Me.dtHorarioEjecucionHistorico.MenuManager = Me.mnu
        Me.dtHorarioEjecucionHistorico.Name = "dtHorarioEjecucionHistorico"
        Me.dtHorarioEjecucionHistorico.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtHorarioEjecucionHistorico.Properties.MaskSettings.Set("mask", "T")
        Me.dtHorarioEjecucionHistorico.Size = New System.Drawing.Size(124, 22)
        Me.dtHorarioEjecucionHistorico.TabIndex = 38
        '
        'txtIdDiasLimiteRetroactivo
        '
        Me.txtIdDiasLimiteRetroactivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdDiasLimiteRetroactivo.Location = New System.Drawing.Point(260, 533)
        Me.txtIdDiasLimiteRetroactivo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdDiasLimiteRetroactivo.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.txtIdDiasLimiteRetroactivo.Name = "txtIdDiasLimiteRetroactivo"
        Me.txtIdDiasLimiteRetroactivo.Size = New System.Drawing.Size(122, 23)
        Me.txtIdDiasLimiteRetroactivo.TabIndex = 37
        Me.txtIdDiasLimiteRetroactivo.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label73
        '
        Me.Label73.AutoSize = True
        Me.Label73.Location = New System.Drawing.Point(24, 535)
        Me.Label73.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(232, 16)
        Me.Label73.TabIndex = 36
        Me.Label73.Text = "Días limite Retroactivo para Ticket TMS"
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(391, 457)
        Me.Label63.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(154, 16)
        Me.Label63.TabIndex = 35
        Me.Label63.Text = "(1-Horizontal / 3-Vertical)"
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(391, 415)
        Me.Label62.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(154, 16)
        Me.Label62.TabIndex = 34
        Me.Label62.Text = "(1-Horizontal / 3-Vertical)"
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(391, 375)
        Me.Label61.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(154, 16)
        Me.Label61.TabIndex = 33
        Me.Label61.Text = "(1-Horizontal / 3-Vertical)"
        '
        'nudTopReabastecimientoManual
        '
        Me.nudTopReabastecimientoManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nudTopReabastecimientoManual.Location = New System.Drawing.Point(261, 491)
        Me.nudTopReabastecimientoManual.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nudTopReabastecimientoManual.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudTopReabastecimientoManual.Name = "nudTopReabastecimientoManual"
        Me.nudTopReabastecimientoManual.Size = New System.Drawing.Size(122, 23)
        Me.nudTopReabastecimientoManual.TabIndex = 32
        Me.nudTopReabastecimientoManual.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Location = New System.Drawing.Point(26, 494)
        Me.Label60.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(197, 16)
        Me.Label60.TabIndex = 31
        Me.Label60.Text = "Top reabastecimiento manual HH"
        '
        'txtIdConfiguracionPantallaRecepcion
        '
        Me.txtIdConfiguracionPantallaRecepcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdConfiguracionPantallaRecepcion.Location = New System.Drawing.Point(261, 453)
        Me.txtIdConfiguracionPantallaRecepcion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdConfiguracionPantallaRecepcion.Maximum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.txtIdConfiguracionPantallaRecepcion.Name = "txtIdConfiguracionPantallaRecepcion"
        Me.txtIdConfiguracionPantallaRecepcion.Size = New System.Drawing.Size(122, 23)
        Me.txtIdConfiguracionPantallaRecepcion.TabIndex = 30
        Me.txtIdConfiguracionPantallaRecepcion.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(26, 455)
        Me.Label57.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(196, 16)
        Me.Label57.TabIndex = 29
        Me.Label57.Text = "Tipo pantalla de recepción en HH"
        '
        'txtIdConfiguracionPantallaVerificacion
        '
        Me.txtIdConfiguracionPantallaVerificacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdConfiguracionPantallaVerificacion.Location = New System.Drawing.Point(261, 414)
        Me.txtIdConfiguracionPantallaVerificacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdConfiguracionPantallaVerificacion.Maximum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.txtIdConfiguracionPantallaVerificacion.Name = "txtIdConfiguracionPantallaVerificacion"
        Me.txtIdConfiguracionPantallaVerificacion.Size = New System.Drawing.Size(122, 23)
        Me.txtIdConfiguracionPantallaVerificacion.TabIndex = 28
        Me.txtIdConfiguracionPantallaVerificacion.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(26, 417)
        Me.Label56.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(205, 16)
        Me.Label56.TabIndex = 27
        Me.Label56.Text = "Tipo pantalla de verificación en HH"
        '
        'txtIdConfiguracionPantallaPicking
        '
        Me.txtIdConfiguracionPantallaPicking.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdConfiguracionPantallaPicking.Location = New System.Drawing.Point(261, 375)
        Me.txtIdConfiguracionPantallaPicking.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdConfiguracionPantallaPicking.Maximum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.txtIdConfiguracionPantallaPicking.Name = "txtIdConfiguracionPantallaPicking"
        Me.txtIdConfiguracionPantallaPicking.Size = New System.Drawing.Size(122, 23)
        Me.txtIdConfiguracionPantallaPicking.TabIndex = 26
        Me.txtIdConfiguracionPantallaPicking.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(26, 378)
        Me.Label55.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(180, 16)
        Me.Label55.TabIndex = 25
        Me.Label55.Text = "Tipo pantalla de picking en HH"
        '
        'txtValorIVA
        '
        Me.txtValorIVA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtValorIVA.DecimalPlaces = 2
        Me.txtValorIVA.Location = New System.Drawing.Point(261, 335)
        Me.txtValorIVA.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtValorIVA.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtValorIVA.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtValorIVA.Name = "txtValorIVA"
        Me.txtValorIVA.Size = New System.Drawing.Size(122, 23)
        Me.txtValorIVA.TabIndex = 24
        '
        'lblValorIVA
        '
        Me.lblValorIVA.AutoSize = True
        Me.lblValorIVA.Location = New System.Drawing.Point(26, 335)
        Me.lblValorIVA.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblValorIVA.Name = "lblValorIVA"
        Me.lblValorIVA.Size = New System.Drawing.Size(61, 16)
        Me.lblValorIVA.TabIndex = 23
        Me.lblValorIVA.TabStop = True
        Me.lblValorIVA.Text = "Valor IVA"
        '
        'txtIdMotivoUbicReabasto
        '
        Me.txtIdMotivoUbicReabasto.EditValue = ""
        Me.txtIdMotivoUbicReabasto.Location = New System.Drawing.Point(260, 228)
        Me.txtIdMotivoUbicReabasto.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdMotivoUbicReabasto.MenuManager = Me.mnu
        Me.txtIdMotivoUbicReabasto.Name = "txtIdMotivoUbicReabasto"
        Me.txtIdMotivoUbicReabasto.Size = New System.Drawing.Size(122, 22)
        Me.txtIdMotivoUbicReabasto.TabIndex = 21
        '
        'txtMotivoUbicReabasto
        '
        Me.txtMotivoUbicReabasto.Location = New System.Drawing.Point(396, 228)
        Me.txtMotivoUbicReabasto.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtMotivoUbicReabasto.MenuManager = Me.mnu
        Me.txtMotivoUbicReabasto.Name = "txtMotivoUbicReabasto"
        Me.txtMotivoUbicReabasto.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtMotivoUbicReabasto.Properties.Appearance.Options.UseBackColor = True
        Me.txtMotivoUbicReabasto.Size = New System.Drawing.Size(195, 22)
        Me.txtMotivoUbicReabasto.TabIndex = 22
        '
        'lnkReabasto
        '
        Me.lnkReabasto.AutoSize = True
        Me.lnkReabasto.Location = New System.Drawing.Point(26, 228)
        Me.lnkReabasto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkReabasto.Name = "lnkReabasto"
        Me.lnkReabasto.Size = New System.Drawing.Size(159, 16)
        Me.lnkReabasto.TabIndex = 20
        Me.lnkReabasto.TabStop = True
        Me.lnkReabasto.Text = "Motivo Ubicación Reabasto"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(26, 302)
        Me.Label35.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(118, 16)
        Me.Label35.TabIndex = 19
        Me.Label35.Text = "Producto Estado NE"
        '
        'cmbEstadoNe
        '
        Me.cmbEstadoNe.Location = New System.Drawing.Point(260, 298)
        Me.cmbEstadoNe.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.cmbEstadoNe.MenuManager = Me.mnu
        Me.cmbEstadoNe.Name = "cmbEstadoNe"
        Me.cmbEstadoNe.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEstadoNe.Properties.NullText = ""
        Me.cmbEstadoNe.Size = New System.Drawing.Size(331, 22)
        Me.cmbEstadoNe.TabIndex = 18
        '
        'txtIdUbicacionPrdNE
        '
        Me.txtIdUbicacionPrdNE.Location = New System.Drawing.Point(260, 263)
        Me.txtIdUbicacionPrdNE.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.txtIdUbicacionPrdNE.MenuManager = Me.mnu
        Me.txtIdUbicacionPrdNE.Name = "txtIdUbicacionPrdNE"
        Me.txtIdUbicacionPrdNE.Size = New System.Drawing.Size(122, 22)
        Me.txtIdUbicacionPrdNE.TabIndex = 16
        '
        'txtNombreUbicNE
        '
        Me.txtNombreUbicNE.Location = New System.Drawing.Point(396, 263)
        Me.txtNombreUbicNE.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.txtNombreUbicNE.MenuManager = Me.mnu
        Me.txtNombreUbicNE.Name = "txtNombreUbicNE"
        Me.txtNombreUbicNE.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtNombreUbicNE.Properties.Appearance.Options.UseBackColor = True
        Me.txtNombreUbicNE.Size = New System.Drawing.Size(195, 22)
        Me.txtNombreUbicNE.TabIndex = 17
        '
        'lnkUbicPrdNE
        '
        Me.lnkUbicPrdNE.AutoSize = True
        Me.lnkUbicPrdNE.Location = New System.Drawing.Point(26, 263)
        Me.lnkUbicPrdNE.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkUbicPrdNE.Name = "lnkUbicPrdNE"
        Me.lnkUbicPrdNE.Size = New System.Drawing.Size(152, 16)
        Me.lnkUbicPrdNE.TabIndex = 15
        Me.lnkUbicPrdNE.TabStop = True
        Me.lnkUbicPrdNE.Text = "Ubicación de Producto NE"
        '
        'txtidmotivoubicaciondañadopicking
        '
        Me.txtidmotivoubicaciondañadopicking.Location = New System.Drawing.Point(260, 193)
        Me.txtidmotivoubicaciondañadopicking.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtidmotivoubicaciondañadopicking.MenuManager = Me.mnu
        Me.txtidmotivoubicaciondañadopicking.Name = "txtidmotivoubicaciondañadopicking"
        Me.txtidmotivoubicaciondañadopicking.Size = New System.Drawing.Size(122, 22)
        Me.txtidmotivoubicaciondañadopicking.TabIndex = 13
        '
        'txtMotivoUbicacionDañadoPicking
        '
        Me.txtMotivoUbicacionDañadoPicking.Location = New System.Drawing.Point(396, 193)
        Me.txtMotivoUbicacionDañadoPicking.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtMotivoUbicacionDañadoPicking.MenuManager = Me.mnu
        Me.txtMotivoUbicacionDañadoPicking.Name = "txtMotivoUbicacionDañadoPicking"
        Me.txtMotivoUbicacionDañadoPicking.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtMotivoUbicacionDañadoPicking.Properties.Appearance.Options.UseBackColor = True
        Me.txtMotivoUbicacionDañadoPicking.Size = New System.Drawing.Size(195, 22)
        Me.txtMotivoUbicacionDañadoPicking.TabIndex = 14
        '
        'lblDañadoPicking
        '
        Me.lblDañadoPicking.AutoSize = True
        Me.lblDañadoPicking.Location = New System.Drawing.Point(26, 193)
        Me.lblDañadoPicking.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDañadoPicking.Name = "lblDañadoPicking"
        Me.lblDañadoPicking.Size = New System.Drawing.Size(192, 16)
        Me.lblDañadoPicking.TabIndex = 12
        Me.lblDañadoPicking.TabStop = True
        Me.lblDañadoPicking.Text = "Motivo Ubicación Dañado Picking"
        '
        'txtNombreUbicacionMerma
        '
        Me.txtNombreUbicacionMerma.Location = New System.Drawing.Point(396, 155)
        Me.txtNombreUbicacionMerma.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNombreUbicacionMerma.Name = "txtNombreUbicacionMerma"
        Me.txtNombreUbicacionMerma.Properties.ReadOnly = True
        Me.txtNombreUbicacionMerma.Size = New System.Drawing.Size(195, 22)
        Me.txtNombreUbicacionMerma.TabIndex = 11
        '
        'lnkUbicacionMerma
        '
        Me.lnkUbicacionMerma.AutoSize = True
        Me.lnkUbicacionMerma.Location = New System.Drawing.Point(26, 155)
        Me.lnkUbicacionMerma.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkUbicacionMerma.Name = "lnkUbicacionMerma"
        Me.lnkUbicacionMerma.Size = New System.Drawing.Size(123, 16)
        Me.lnkUbicacionMerma.TabIndex = 9
        Me.lnkUbicacionMerma.TabStop = True
        Me.lnkUbicacionMerma.Text = "Ubicación de Merma"
        '
        'txtIdUbicacionMerma
        '
        Me.txtIdUbicacionMerma.Location = New System.Drawing.Point(260, 155)
        Me.txtIdUbicacionMerma.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdUbicacionMerma.Name = "txtIdUbicacionMerma"
        Me.txtIdUbicacionMerma.Properties.MaskSettings.Set("mask", "n0")
        Me.txtIdUbicacionMerma.Size = New System.Drawing.Size(122, 22)
        Me.txtIdUbicacionMerma.TabIndex = 10
        '
        'txtNombreUbicacionDespacho
        '
        Me.txtNombreUbicacionDespacho.Location = New System.Drawing.Point(396, 121)
        Me.txtNombreUbicacionDespacho.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNombreUbicacionDespacho.Name = "txtNombreUbicacionDespacho"
        Me.txtNombreUbicacionDespacho.Properties.ReadOnly = True
        Me.txtNombreUbicacionDespacho.Size = New System.Drawing.Size(195, 22)
        Me.txtNombreUbicacionDespacho.TabIndex = 8
        '
        'lnkUbicacionDespacho
        '
        Me.lnkUbicacionDespacho.AutoSize = True
        Me.lnkUbicacionDespacho.Location = New System.Drawing.Point(26, 121)
        Me.lnkUbicacionDespacho.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkUbicacionDespacho.Name = "lnkUbicacionDespacho"
        Me.lnkUbicacionDespacho.Size = New System.Drawing.Size(130, 16)
        Me.lnkUbicacionDespacho.TabIndex = 6
        Me.lnkUbicacionDespacho.TabStop = True
        Me.lnkUbicacionDespacho.Text = "Ubicación de Tránsito"
        '
        'txtIdUbicacionDespacho
        '
        Me.txtIdUbicacionDespacho.Location = New System.Drawing.Point(260, 121)
        Me.txtIdUbicacionDespacho.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdUbicacionDespacho.Name = "txtIdUbicacionDespacho"
        Me.txtIdUbicacionDespacho.Properties.MaskSettings.Set("mask", "n0")
        Me.txtIdUbicacionDespacho.Size = New System.Drawing.Size(122, 22)
        Me.txtIdUbicacionDespacho.TabIndex = 7
        '
        'txtNombreUbicacionPicking
        '
        Me.txtNombreUbicacionPicking.Location = New System.Drawing.Point(396, 89)
        Me.txtNombreUbicacionPicking.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNombreUbicacionPicking.Name = "txtNombreUbicacionPicking"
        Me.txtNombreUbicacionPicking.Properties.ReadOnly = True
        Me.txtNombreUbicacionPicking.Size = New System.Drawing.Size(195, 22)
        Me.txtNombreUbicacionPicking.TabIndex = 5
        '
        'lnkUbicacionPicking
        '
        Me.lnkUbicacionPicking.AutoSize = True
        Me.lnkUbicacionPicking.Location = New System.Drawing.Point(26, 89)
        Me.lnkUbicacionPicking.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkUbicacionPicking.Name = "lnkUbicacionPicking"
        Me.lnkUbicacionPicking.Size = New System.Drawing.Size(122, 16)
        Me.lnkUbicacionPicking.TabIndex = 3
        Me.lnkUbicacionPicking.TabStop = True
        Me.lnkUbicacionPicking.Text = "Ubicación de Picking"
        '
        'txtIdUbicacionPicking
        '
        Me.txtIdUbicacionPicking.Location = New System.Drawing.Point(260, 89)
        Me.txtIdUbicacionPicking.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdUbicacionPicking.Name = "txtIdUbicacionPicking"
        Me.txtIdUbicacionPicking.Properties.MaskSettings.Set("mask", "n0")
        Me.txtIdUbicacionPicking.Size = New System.Drawing.Size(122, 22)
        Me.txtIdUbicacionPicking.TabIndex = 4
        '
        'txtNombreUbicacionRecepcion
        '
        Me.txtNombreUbicacionRecepcion.Location = New System.Drawing.Point(396, 54)
        Me.txtNombreUbicacionRecepcion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNombreUbicacionRecepcion.Name = "txtNombreUbicacionRecepcion"
        Me.txtNombreUbicacionRecepcion.Properties.ReadOnly = True
        Me.txtNombreUbicacionRecepcion.Size = New System.Drawing.Size(195, 22)
        Me.txtNombreUbicacionRecepcion.TabIndex = 2
        '
        'lnkUbicacionRecepcion
        '
        Me.lnkUbicacionRecepcion.AutoSize = True
        Me.lnkUbicacionRecepcion.Location = New System.Drawing.Point(26, 54)
        Me.lnkUbicacionRecepcion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkUbicacionRecepcion.Name = "lnkUbicacionRecepcion"
        Me.lnkUbicacionRecepcion.Size = New System.Drawing.Size(141, 16)
        Me.lnkUbicacionRecepcion.TabIndex = 0
        Me.lnkUbicacionRecepcion.TabStop = True
        Me.lnkUbicacionRecepcion.Text = "Ubicación de Recepción"
        '
        'txtIdUbicacionRecepcion
        '
        Me.txtIdUbicacionRecepcion.Location = New System.Drawing.Point(260, 54)
        Me.txtIdUbicacionRecepcion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdUbicacionRecepcion.Name = "txtIdUbicacionRecepcion"
        Me.txtIdUbicacionRecepcion.Properties.MaskSettings.Set("mask", "n0")
        Me.txtIdUbicacionRecepcion.Size = New System.Drawing.Size(122, 22)
        Me.txtIdUbicacionRecepcion.TabIndex = 1
        '
        'tabListaUbicaciones
        '
        Me.tabListaUbicaciones.Controls.Add(Me.GroupControl5)
        Me.tabListaUbicaciones.Margin = New System.Windows.Forms.Padding(4)
        Me.tabListaUbicaciones.Name = "tabListaUbicaciones"
        Me.tabListaUbicaciones.Size = New System.Drawing.Size(1784, 711)
        Me.tabListaUbicaciones.Text = "Lista de ubicaciones"
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.dgridUbicaciones)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl5.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl5.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(1784, 711)
        Me.GroupControl5.TabIndex = 3
        Me.GroupControl5.Text = "Detalle Ubicaciones"
        '
        'dgridUbicaciones
        '
        Me.dgridUbicaciones.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridUbicaciones.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgridUbicaciones.Location = New System.Drawing.Point(2, 28)
        Me.dgridUbicaciones.MainView = Me.GridView4
        Me.dgridUbicaciones.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgridUbicaciones.Name = "dgridUbicaciones"
        Me.dgridUbicaciones.Size = New System.Drawing.Size(1780, 681)
        Me.dgridUbicaciones.TabIndex = 0
        Me.dgridUbicaciones.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView4})
        '
        'GridView4
        '
        Me.GridView4.DetailHeight = 437
        Me.GridView4.GridControl = Me.dgridUbicaciones
        Me.GridView4.Name = "GridView4"
        Me.GridView4.OptionsBehavior.Editable = False
        Me.GridView4.OptionsEditForm.PopupEditFormWidth = 1000
        Me.GridView4.OptionsView.ColumnAutoWidth = False
        Me.GridView4.OptionsView.ShowAutoFilterRow = True
        Me.GridView4.OptionsView.ShowFooter = True
        Me.GridView4.OptionsView.ShowGroupPanel = False
        '
        'dkBodega
        '
        Me.dkBodega.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkBodega.Form = Me
        Me.dkBodega.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 934)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1786, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("7c19cadc-1583-472b-b377-1a97687f8de8")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 126)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1230, 126)
        Me.DockPanel1.Text = "Bitácora"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.User_agrTextEdit)
        Me.DockPanel1_Container.Controls.Add(User_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_modTextEdit)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_agrTextEdit)
        Me.DockPanel1_Container.Controls.Add(User_modLabel)
        Me.DockPanel1_Container.Controls.Add(Fec_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Fec_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_modTextEdit)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 34)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1222, 89)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumericUpDown1.Location = New System.Drawing.Point(278, 364)
        Me.NumericUpDown1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(98, 22)
        Me.NumericUpDown1.TabIndex = 26
        Me.NumericUpDown1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label96
        '
        Me.Label96.AutoSize = True
        Me.Label96.Location = New System.Drawing.Point(381, 280)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(148, 16)
        Me.Label96.TabIndex = 136
        Me.Label96.Text = "Ubicación implosión auto"
        '
        'chkUbicImplosionAuto
        '
        Me.chkUbicImplosionAuto.Location = New System.Drawing.Point(686, 276)
        Me.chkUbicImplosionAuto.Name = "chkUbicImplosionAuto"
        Me.chkUbicImplosionAuto.Properties.Caption = ""
        Me.chkUbicImplosionAuto.Size = New System.Drawing.Size(23, 24)
        Me.chkUbicImplosionAuto.TabIndex = 135
        '
        'frmBodega
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1786, 960)
        Me.Controls.Add(Me.ControlPanelBodega)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.mnu)
        Me.IconOptions.Icon = CType(resources.GetObject("frmBodega.IconOptions.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmBodega"
        Me.Ribbon = Me.mnu
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mantenimiento de bodega"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl12, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl12.ResumeLayout(False)
        Me.GroupControl12.PerformLayout()
        CType(Me.txtIdTramo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTipoRack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOrient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mnu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIndice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEsRack.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbFont.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSector.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbAreasR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivoTramo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoTramo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSistemaTramo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescripcionTramo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl11.ResumeLayout(False)
        Me.GroupControl11.PerformLayout()
        CType(Me.nUpdMargenInferiorTramo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdMargenSuperiorTramo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdMargenDerechoTramo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdMargenIzquierdoTramo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdAnchoTramo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdLargoTramo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdAltoTramo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl10.ResumeLayout(False)
        CType(Me.grdTramo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewTramo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTramosActivos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl14, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl14.ResumeLayout(False)
        Me.GroupControl14.PerformLayout()
        CType(Me.chkUbicacionMuelle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkUbicPrdNE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkUbicacionesActivas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbOrientacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEsBodegaVirtual.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdMargenInferiorUbicacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdMargenSuperiorUbicacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdMargenDerechoUbicacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMerma.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdMargenIzquierdoUbicacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdAnchoUbicacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdLargoUbicacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdAltoUbicacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDespacho.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDañadoUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkUbicacionPicking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivoUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAceptaPalletUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRecepcion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBloqueadaUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSistemaUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl15, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl15.ResumeLayout(False)
        CType(Me.grdUbicacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewUbi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl16, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl16.ResumeLayout(False)
        CType(Me.tlUbicaciones, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Dgrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtTiempoActualizacionP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreTarea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdTarea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsBodega, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BodegaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ControlPanelBodega, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ControlPanelBodega.ResumeLayout(False)
        Me.tabDatos.ResumeLayout(False)
        CType(Me.grpDatosGen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDatosGen.ResumeLayout(False)
        Me.XtraScrollableControl.ResumeLayout(False)
        Me.XtraScrollableControl.PerformLayout()
        CType(Me.gpSmtp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpSmtp.ResumeLayout(False)
        Me.gpSmtp.PerformLayout()
        CType(Me.txtPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUsuario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPuerto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtServidor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSsl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcCentroCosto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcCentroCosto.ResumeLayout(False)
        Me.gcCentroCosto.PerformLayout()
        CType(Me.cmbCentroCostoDepERP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCentroCostoDirERP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCentroCostoERP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRutaCDN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpTIpoTransaccion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpTIpoTransaccion.ResumeLayout(False)
        Me.GrpTIpoTransaccion.PerformLayout()
        CType(Me.txtNombreDocumentoSalida.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdTipoDocumentoSalida.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescripcionTR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdTipoTR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTamañoEtiquetaUbicacionDefecto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoBodegaERP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPais.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EncargadoTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmailTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TelefonoTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DireccionTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreComercial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoBarra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabDimensionesBod.ResumeLayout(False)
        CType(Me.GroupControl18, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl18.ResumeLayout(False)
        Me.GroupControl18.PerformLayout()
        CType(Me.txtZoom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAlto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCoordenadaY.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLargo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCoordenadaX.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAncho, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabArea.ResumeLayout(False)
        Me.tabArea.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.txtNombreUbicacionRecepcionArea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUbicacionRecepcionArea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGrupoArea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdArea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivoAreaBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoAreaBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSistemaAreaBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescripcionAreaBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6.ResumeLayout(False)
        Me.GroupControl6.PerformLayout()
        CType(Me.nUpdMargenInferior, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdMargenSuperior, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdMargenDerecho, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdMargenIzquierdo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdAlto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdAncho, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdLargo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripPR.ResumeLayout(False)
        Me.ToolStripPR.PerformLayout()
        CType(Me.GroupControl17, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl17.ResumeLayout(False)
        CType(Me.grdAreaBodega, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAreasBodegaActivos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabSector.ResumeLayout(False)
        Me.tabSector.PerformLayout()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl8.ResumeLayout(False)
        Me.GroupControl8.PerformLayout()
        CType(Me.txtIdSector.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbArea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivoSector.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoSector.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdAnchoSector, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSistemaSector.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdLargoSector, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescripcionSector.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdAltoSector, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl7.ResumeLayout(False)
        Me.GroupControl7.PerformLayout()
        CType(Me.txtPosY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPosX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdMargenInferiorSector, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdMargenSuperiorSector, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdMargenDerechoSector, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdMargenIzquierdoSector, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl9.ResumeLayout(False)
        CType(Me.grdSectorArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewSec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSectoresActivos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.tabTramo.ResumeLayout(False)
        Me.tabTramo.PerformLayout()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.TabUbicacion.ResumeLayout(False)
        Me.TabUbicacion.PerformLayout()
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.GroupControl13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl13.ResumeLayout(False)
        Me.GroupControl13.PerformLayout()
        CType(Me.txtUbicCodigoBodegaERP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTramo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSectorR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbAreaUbic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbIndiceRotacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTipoRotacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIndiceX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUpdNivelUbicacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoBarra2ubicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoBarraUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescripcionUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip3.ResumeLayout(False)
        Me.ToolStrip3.PerformLayout()
        Me.tabReferencia.ResumeLayout(False)
        Me.tabParametros.ResumeLayout(False)
        Me.tabUbicacionesDefecto.ResumeLayout(False)
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.chkControlGondola.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudRangoDiasDocumentos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkControlTallaColor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkrestringir_vencimiento_en_reemplazo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkrestringir_lote_en_reemplazo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLberarStockDepachosParciales.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkHomologarLoteConFechaVence.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkValidarExistenciasEnCargaInventarioInicial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkControlPalletsMixtos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkControlOperadorUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkinferir_origen_en_cambio_ubic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkValidarDisponibilidadEnUbicacionDestino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.chkBodegaClienteAjusteByB.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkreemplazoOpcional.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkImprimir_Verificacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAdvertirMpqUmbas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAgrupar_sin_lic_veri_no_cons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVerificacion_Consolidada.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkControlBanderasCliente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDespacharProductoVencido.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermitirReemplazoVerificacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermitir_Verificacion_Consolidada.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiasMaximoVencimientoReemplazo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkdespachoautohh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermitirEliminarDocumentosSalida.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEliminarDocumentosSalida.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFiltrarPedidosUsuario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCambioUbicacionRestrictivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermitirCambioUbicIndiceMenor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRequerirMismoProductoPosiciones.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.chkCambioUbiAuto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkControlTarifaServ.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEsBodegaFiscal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLimpiarCamposHH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkNotificacionVoz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEsMotriz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRestringirAreasSAP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMostrarAreaEnHH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInterface_SAP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkcalcular_ubicacion_sugerida_ml.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermitirDecimales.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.chkControlGuia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOperadorPickingVerifica.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermitirNoEncontradoPicking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermitirReemplazoPickingMismaLIcencia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEscanearLicenciaPicking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermitirReemplazoPicking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkpermitir_buen_estado_en_reemplazo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEscanearCodigoProductoEnPicking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermitirCambioUbicacionPicking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOrdenarPickingDescendente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOrdenarNombreCompleto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbEstadoDefectoRack.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermitirCambioUbicacionRecepcion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBloquearLpHH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIngresoConsolidado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPriorizar_UbicRec_Sobre_UbicEst.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCapturaPalletNoEstandar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCapturaEstibaIngreso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermitirRepeticionesEnIngreso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.cmbEtiquetaVerificacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSymbology.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEtiqueta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtHorarioEjecucionHistorico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdDiasLimiteRetroactivo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudTopReabastecimientoManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdConfiguracionPantallaRecepcion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdConfiguracionPantallaVerificacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdConfiguracionPantallaPicking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtValorIVA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdMotivoUbicReabasto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMotivoUbicReabasto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEstadoNe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdUbicacionPrdNE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreUbicNE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtidmotivoubicaciondañadopicking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMotivoUbicacionDañadoPicking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreUbicacionMerma.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdUbicacionMerma.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreUbicacionDespacho.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdUbicacionDespacho.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreUbicacionPicking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdUbicacionPicking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreUbicacionRecepcion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdUbicacionRecepcion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabListaUbicaciones.ResumeLayout(False)
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        CType(Me.dgridUbicaciones, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkBodega, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkUbicImplosionAuto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkTramosActivos As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl10 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdTramo As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewTramo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl11 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents nUpdMargenInferiorTramo As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUpdMargenSuperiorTramo As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUpdMargenDerechoTramo As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUpdAnchoTramo As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUpdLargoTramo As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupControl12 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkActivoTramo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtCodigoTramo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkSistemaTramo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtDescripcionTramo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkActivoUbicacion As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkSistemaUbicacion As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkUbicacionPicking As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkAceptaPalletUbicacion As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkBloqueadaUbicacion As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkDañadoUbicacion As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl14 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents nUpdMargenInferiorUbicacion As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUpdMargenSuperiorUbicacion As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUpdMargenDerechoUbicacion As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUpdMargenIzquierdoUbicacion As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUpdAnchoUbicacion As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUpdLargoUbicacion As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUpdAltoUbicacion As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupControl15 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdUbicacion As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewUbi As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkUbicacionesActivas As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkMerma As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkDespacho As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkRecepcion As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents mnu As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage2 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents GroupControl16 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tlUbicaciones As DevExpress.XtraTreeList.TreeList
    Friend WithEvents DsBodega As DsBodega
    Friend WithEvents BodegaBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtNombreTarea As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkTareas As LinkLabel
    Friend WithEvents txtIdTarea As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTiempoActualizacionP As NumericUpDown
    Friend WithEvents Dgrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Button1 As Button
    Friend WithEvents ControlPanelBodega As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tabArea As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl17 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdAreaBodega As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewArea As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkAreasBodegaActivos As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl6 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents nUpdMargenInferior As NumericUpDown
    Friend WithEvents nUpdMargenSuperior As NumericUpDown
    Friend WithEvents nUpdMargenDerecho As NumericUpDown
    Friend WithEvents nUpdMargenIzquierdo As NumericUpDown
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkActivoAreaBodega As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtCodigoAreaBodega As DevExpress.XtraEditors.TextEdit
    Friend WithEvents nUpdAncho As NumericUpDown
    Friend WithEvents nUpdLargo As NumericUpDown
    Friend WithEvents chkSistemaAreaBodega As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents nUpdAlto As NumericUpDown
    Friend WithEvents txtDescripcionAreaBodega As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ToolStripPR As ToolStrip
    Friend WithEvents cmdNuevaArea As ToolStripButton
    Friend WithEvents cmdGuardarArea As ToolStripButton
    Friend WithEvents tabDatos As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtNombreUbicacionMerma As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkUbicacionMerma As LinkLabel
    Friend WithEvents txtIdUbicacionMerma As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreUbicacionDespacho As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkUbicacionDespacho As LinkLabel
    Friend WithEvents txtIdUbicacionDespacho As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreUbicacionPicking As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkUbicacionPicking As LinkLabel
    Friend WithEvents txtIdUbicacionPicking As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreUbicacionRecepcion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkUbicacionRecepcion As LinkLabel
    Friend WithEvents txtIdUbicacionRecepcion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents grpDatosGen As DevExpress.XtraEditors.GroupControl
    Friend WithEvents XtraScrollableControl As DevExpress.XtraEditors.XtraScrollableControl
    Friend WithEvents GrpTIpoTransaccion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtDescripcionTR As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkTipoT As LinkLabel
    Friend WithEvents txtIdTipoTR As DevExpress.XtraEditors.TextEdit
    Friend WithEvents EncargadoTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents EmailTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TelefonoTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents DireccionTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreComercial As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodigoBarra As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodigo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tabSector As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl7 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtPosY As NumericUpDown
    Friend WithEvents txtPosX As NumericUpDown
    Friend WithEvents Label25 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents chkHorizontal As CheckBox
    Friend WithEvents nUpdMargenInferiorSector As NumericUpDown
    Friend WithEvents nUpdMargenSuperiorSector As NumericUpDown
    Friend WithEvents nUpdMargenDerechoSector As NumericUpDown
    Friend WithEvents nUpdMargenIzquierdoSector As NumericUpDown
    Friend WithEvents GroupControl8 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkActivoSector As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl9 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkSectoresActivos As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents grdSectorArea As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewSec As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtCodigoSector As DevExpress.XtraEditors.TextEdit
    Friend WithEvents nUpdAnchoSector As NumericUpDown
    Friend WithEvents chkSistemaSector As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents nUpdLargoSector As NumericUpDown
    Friend WithEvents txtDescripcionSector As DevExpress.XtraEditors.TextEdit
    Friend WithEvents nUpdAltoSector As NumericUpDown
    Friend WithEvents tabTramo As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TabUbicacion As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabReferencia As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabParametros As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl13 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtIndiceX As NumericUpDown
    Friend WithEvents nUpdNivelUbicacion As NumericUpDown
    Friend WithEvents txtCodigoBarra2ubicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodigoBarraUbicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDescripcionUbicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents SplitContainer4 As SplitContainer
    Friend WithEvents tabUbicacionesDefecto As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabDimensionesBod As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Label27 As Label
    Friend WithEvents txtAlto As NumericUpDown
    Friend WithEvents txtLargo As NumericUpDown
    Friend WithEvents txtZoom As NumericUpDown
    Friend WithEvents txtAncho As NumericUpDown
    Friend WithEvents txtCoordenadaX As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCoordenadaY As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl18 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Label29 As Label
    Friend WithEvents Label32 As Label
    Friend WithEvents Label33 As Label
    Friend WithEvents mnuDiseñoGrafico As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents dkBodega As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents cmbPais As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbEmpresa As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbTipoRotacion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbIndiceRotacion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbArea As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbAreasR As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbAreaUbic As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbSector As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbSectorR As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbFont As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbTramo As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents chkEsBodegaVirtual As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents tsmnuNuevoSector As ToolStripButton
    Friend WithEvents tsmnuGuardarSector As ToolStripButton
    Friend WithEvents ToolStrip2 As ToolStrip
    Friend WithEvents ToolStrip3 As ToolStrip
    Friend WithEvents tsmnuNuevaUbicacion As ToolStripButton
    Friend WithEvents tsmnuGuardarUbicacion As ToolStripButton
    Friend WithEvents lblDañadoPicking As LinkLabel
    Friend WithEvents txtMotivoUbicacionDañadoPicking As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtidmotivoubicaciondañadopicking As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbOrientacion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents chkCambioUbiAuto As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkEsRack As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblMensajeUbicacionesDef As Label
    Friend WithEvents nUpdMargenIzquierdoTramo As NumericUpDown
    Friend WithEvents nUpdAltoTramo As NumericUpDown
    Friend WithEvents txtCodigoBodegaERP As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtUbicCodigoBodegaERP As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label34 As Label
    Friend WithEvents chkOrientacion As CheckBox
    Friend WithEvents txtIdUbicacionPrdNE As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreUbicNE As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkUbicPrdNE As LinkLabel
    Friend WithEvents Label35 As Label
    Friend WithEvents cmbEstadoNe As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents chkUbicPrdNE As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Label36 As Label
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents Label47 As Label
    Friend WithEvents txtIndice As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkOrient As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtTipoRack As NumericUpDown
    Friend WithEvents mnuEstructuraInicial As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdRefrescar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkNotificacionVoz As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkControlTarifaServ As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtIdMotivoUbicReabasto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtMotivoUbicReabasto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkReabasto As LinkLabel
    Friend WithEvents chkEsBodegaFiscal As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkIngresoConsolidado As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkBloquearLpHH As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkCapturaEstibaIngreso As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkCapturaPalletNoEstandar As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents mnuParametrosInterface As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEditarConnIni As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblValorIVA As LinkLabel
    Friend WithEvents txtValorIVA As NumericUpDown
    Friend WithEvents chkPermitir_Verificacion_Consolidada As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkControlBanderasCliente As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtGrupoArea As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdArea As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdSector As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdTramo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents mnuUnificarBodegas As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmbTamañoEtiquetaUbicacionDefecto As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblTamañoEtiquetaUbicacionDefecto As Label
    Friend WithEvents chkPriorizar_UbicRec_Sobre_UbicEst As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkValidarDisponibilidadEnUbicacionDestino As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtNombreDocumentoSalida As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkTipoSalida As LinkLabel
    Friend WithEvents txtIdTipoDocumentoSalida As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkPermitirEliminarDocumentosSalida As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkMostrarAreaEnHH As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkEscanearCodigoProductoEnPicking As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkControlOperadorUbicacion As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkinferir_origen_en_cambio_ubic As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkEliminarDocumentosSalida As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkOperadorPickingVerifica As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkPermitirCambioUbicacionPicking As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents tabListaUbicaciones As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dgridUbicaciones As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents chkDespacharProductoVencido As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtIdConfiguracionPantallaPicking As NumericUpDown
    Friend WithEvents Label55 As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents txtIdConfiguracionPantallaRecepcion As NumericUpDown
    Friend WithEvents Label57 As Label
    Friend WithEvents txtIdConfiguracionPantallaVerificacion As NumericUpDown
    Friend WithEvents Label56 As Label
    Friend WithEvents chkpermitir_buen_estado_en_reemplazo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkEsMotriz As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkrestringir_lote_en_reemplazo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkrestringir_vencimiento_en_reemplazo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Label63 As Label
    Friend WithEvents Label62 As Label
    Friend WithEvents Label61 As Label
    Friend WithEvents nudTopReabastecimientoManual As NumericUpDown
    Friend WithEvents Label60 As Label
    Friend WithEvents chkPermitirDecimales As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtDiasMaximoVencimientoReemplazo As NumericUpDown
    Friend WithEvents Label65 As Label
    Friend WithEvents chkPermitirRepeticionesEnIngreso As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkValidarExistenciasEnCargaInventarioInicial As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkcalcular_ubicacion_sugerida_ml As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkOrdenarNombreCompleto As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkOrdenarPickingDescendente As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkPermitirNoEncontradoPicking As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkPermitirReemplazoVerificacion As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkPermitirReemplazoPicking As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkPermitirReemplazoPickingMismaLIcencia As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtIdDiasLimiteRetroactivo As NumericUpDown
    Friend WithEvents Label73 As Label
    Friend WithEvents Label74 As Label
    Friend WithEvents dtHorarioEjecucionHistorico As DevExpress.XtraEditors.DateTimeOffsetEdit
    Friend WithEvents cmdHabilitarReemplazo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdDeshabilitarReemplazo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkFiltrarPedidosUsuario As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkLberarStockDepachosParciales As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkHomologarLoteConFechaVence As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup4 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents chkEscanearLicenciaPicking As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lnkUbicacionRecepcionArea As LinkLabel
    Friend WithEvents txtUbicacionRecepcionArea As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreUbicacionRecepcionArea As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblHomologarLoteConFechaVence As Label
    Friend WithEvents lblEscanearLicenciaPicking As Label
    Friend WithEvents cmbSymbology As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbEtiqueta As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblEtiqueta As Label
    Friend WithEvents Bcc As DevExpress.XtraEditors.BarCodeControl
    Friend WithEvents chkInterface_SAP As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkUbicacionMuelle As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkRestringirAreasSAP As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents tsmnuNuevoTramo As ToolStripButton
    Friend WithEvents tsmnuGuardarTramo As ToolStripButton
    Friend WithEvents chkControlPalletsMixtos As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents mnuActualizarIndicesRotacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup5 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuPlantillaIndicesRotacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkdespachoautohh As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkLimpiarCamposHH As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents chkVerificacion_Consolidada As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkPermitirCambioUbicacionRecepcion As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cmdRutaCDN As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtRutaCDN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents Label80 As Label
    Friend WithEvents Label81 As Label
    Friend WithEvents lblRutaCDN As Label
    Friend WithEvents chkControlTallaColor As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Label82 As Label
    Friend WithEvents nudRangoDiasDocumentos As NumericUpDown
    Friend WithEvents gcCentroCosto As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbCentroCostoDepERP As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbCentroCostoDirERP As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbCentroCostoERP As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents chkAgrupar_sin_lic_veri_no_cons As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Label86 As Label
    Friend WithEvents chkAdvertirMpqUmbas As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Label87 As Label
    Friend WithEvents chkControlGondola As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblControlGondola As Label
    Friend WithEvents gpSmtp As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkSsl As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtPassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtUsuario As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPuerto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtServidor As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkImprimir_Verificacion As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Label100 As Label
    Friend WithEvents cmbEtiquetaVerificacion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label99 As Label
    Friend WithEvents chkreemplazoOpcional As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Label94 As Label
    Friend WithEvents cmbEstadoDefectoRack As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents chkCambioUbicacionRestrictivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkPermitirCambioUbicIndiceMenor As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkRequerirMismoProductoPosiciones As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblCambioUbicacionRestrictivo As System.Windows.Forms.Label
    Friend WithEvents lblPermitirCambioUbicIndiceMenor As System.Windows.Forms.Label
    Friend WithEvents lblRequerirMismoProductoPosiciones As System.Windows.Forms.Label

    Friend WithEvents chkBodegaClienteAjusteByB As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblBodegaClienteAjusteByB As Label
    Friend WithEvents Label95 As Label
    Friend WithEvents chkControlGuia As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Label96 As Label
    Friend WithEvents chkUbicImplosionAuto As DevExpress.XtraEditors.CheckEdit
End Class
