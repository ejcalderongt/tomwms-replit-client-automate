Imports DevExpress.XtraBars
Imports DevExpress.XtraSplashScreen

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMenu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMenu))
        Dim GalleryItemGroup1 As DevExpress.XtraBars.Ribbon.GalleryItemGroup = New DevExpress.XtraBars.Ribbon.GalleryItemGroup()
        Dim GalleryItem1 As DevExpress.XtraBars.Ribbon.GalleryItem = New DevExpress.XtraBars.Ribbon.GalleryItem()
        Me.rbMain = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuBodega = New DevExpress.XtraBars.BarSubItem()
        Me.mnuMantBodega = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMuelles = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMantOperadores = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMantTurnoLab = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMantJornadaLab = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMantHorarioLab = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMotivoUbic = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTipoTarima = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTipoConte = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTarima = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuIndiceRot = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuReglasList = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuReglaUbicPrio = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdLetra = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImpEtiqueta = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEstructuraInicial = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSerieDocs = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdPrint = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTipoCuadrilla = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCuadrilla = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRegimenFiscal = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTiemposTareas = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuZonaPicking = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuUbicacion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuProductos = New DevExpress.XtraBars.BarSubItem()
        Me.mnuMantClas = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuFamilia = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMarca = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMantEstados = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMantTipo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMantUnidMed = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdUnidadMedidaconversion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMantProducto = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdCodBarras = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuParametros = New DevExpress.XtraBars.BarSubItem()
        Me.cmdParametroA = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdParametroB = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTalla = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuColor = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCampaña = New DevExpress.XtraBars.BarButtonItem()
        Me.SkinRibbonGalleryBarItem1 = New DevExpress.XtraBars.SkinRibbonGalleryBarItem()
        Me.RibbonGalleryBarItem1 = New DevExpress.XtraBars.RibbonGalleryBarItem()
        Me.SkinRibbonGalleryBarItem2 = New DevExpress.XtraBars.SkinRibbonGalleryBarItem()
        Me.mnuMantPropietarios = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEmpresa = New DevExpress.XtraBars.BarSubItem()
        Me.mnuMantEmpresa = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMantMotivoAnul = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMantMotivoDevol = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMantCentroCostos = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdArancel = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuGeograficos = New DevExpress.XtraBars.BarSubItem()
        Me.mnuMantPais = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuDepartamento = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMunicpio = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRegion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTiposDocumentoIngreso = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTiposDocumentoSalida = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuClientes = New DevExpress.XtraBars.BarSubItem()
        Me.mnuMantCliente = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTipoCliente = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMantConsolidador = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTiemposAceptacion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuProveedores = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSeguridad = New DevExpress.XtraBars.BarSubItem()
        Me.mnuMantUsuarios = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRolesUsuario = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRolesOperador = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCambiarContraseña = New DevExpress.XtraBars.BarButtonItem()
        Me.lblNombrePCCliente = New DevExpress.XtraBars.BarStaticItem()
        Me.lblVersion = New DevExpress.XtraBars.BarStaticItem()
        Me.mnuOrdenesCompra = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRecepcion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRepIngresoRapido = New DevExpress.XtraBars.BarSubItem()
        Me.cmdIngresos = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdExistenciaspordocumento = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdDocConDiferencias = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuLogistica = New DevExpress.XtraBars.BarSubItem()
        Me.mnuEmpTrans = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuVehiculos = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPiloto = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuControlMontacargas = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuFallaMontacarga = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuVendedorRoad = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuVendedorRuta = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuUbicSug = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdTipoEtiqueta = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuServicios = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTarifas = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCambioUbicacion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCambioEstado = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCotizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPedidoVenta = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPicking = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuDespachos = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem38 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem39 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem40 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRepSalidasRapido = New DevExpress.XtraBars.BarButtonItem()
        Me.PopupMenu2 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.mnuInventarios = New DevExpress.XtraBars.BarButtonItem()
        Me.lblUsuario = New DevExpress.XtraBars.BarStaticItem()
        Me.mnuPropietarioBodega = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonGroup1 = New DevExpress.XtraBars.BarButtonGroup()
        Me.BarButtonGroup2 = New DevExpress.XtraBars.BarButtonGroup()
        Me.BarButtonGroup3 = New DevExpress.XtraBars.BarButtonGroup()
        Me.BarButtonGroup4 = New DevExpress.XtraBars.BarButtonGroup()
        Me.BarMdiChildrenListItem1 = New DevExpress.XtraBars.BarMdiChildrenListItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarSubProveedor = New DevExpress.XtraBars.BarSubItem()
        Me.cmdProveedor = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdProveedores = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonGroup5 = New DevExpress.XtraBars.BarButtonGroup()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdCalendario = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTraslados = New DevExpress.XtraBars.BarButtonItem()
        Me.BarSubItem1 = New DevExpress.XtraBars.BarSubItem()
        Me.BarLinkContainerItem1 = New DevExpress.XtraBars.BarLinkContainerItem()
        Me.BarMdiChildrenListItem2 = New DevExpress.XtraBars.BarMdiChildrenListItem()
        Me.BarButtonItem5 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem6 = New DevExpress.XtraBars.BarButtonItem()
        Me.lblHoraActual = New DevExpress.XtraBars.BarHeaderItem()
        Me.mnuExistencias = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem7 = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdDetalleExistencia = New DevExpress.XtraBars.BarSubItem()
        Me.cmdResumenExistencia = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdResumenExistenciasUMBas = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdExistenciasProductos = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdExistenciasEstado = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdValorizacion = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdDetalleParametro = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdDetalleSerie = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdExistenciasUbic = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdExistenciasPorLote = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdDetalleLotePorUbi = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdExistenciaPorTipoProd = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdDetalleInventario = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdRpExitLP = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdExistCnRec = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdExistFiscal = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdExistConsolidador = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdExistPorClasif = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdExistenciasPropietario = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdExistenciasPorLote_Posicion = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdStockJornadaSistema = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdValorizacionOC = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdStockEnLinea = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdStockJornada = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdLicenciasPorUbicacion = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem9 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem13 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem14 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPropietarios = New DevExpress.XtraBars.BarSubItem()
        Me.mnuMantPropietario = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMantReglaMsj = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMantReglaRc = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem18 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMonitor = New DevExpress.XtraBars.BarSubItem()
        Me.mnuMostrarMonitor = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuConfigurarMonitor = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImpresora = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdConexionBD = New DevExpress.XtraBars.BarButtonItem()
        Me.lblServerAPP = New DevExpress.XtraBars.BarStaticItem()
        Me.lblBDAPP = New DevExpress.XtraBars.BarStaticItem()
        Me.cmdUbicacionPicking = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdTipoIngreso = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuOrdenCompra = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuReglaUbicacion = New DevExpress.XtraBars.BarButtonItem()
        Me.BarEditItem1 = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemBreadCrumbEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemBreadCrumbEdit()
        Me.BarButtonItem24 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem25 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem27 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuConversion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuInterfaceNav = New DevExpress.XtraBars.BarButtonItem()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.lblEmpresa = New DevExpress.XtraBars.BarMdiChildrenListItem()
        Me.lblBodega = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdMovimientos = New DevExpress.XtraBars.BarSubItem()
        Me.cmdMovimiento = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdMovimientosCardex = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdMovimientosUbic = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdMovimientosDet = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdTrazaLote = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdMovCardexConDocs = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuKardexLote = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdMovporLote = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdMovimientosPoliza = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdMovimientosDoc = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRptAjustesInventario = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdMovimientosEstado = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuLicencia = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuLicencias = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem8 = New DevExpress.XtraBars.BarButtonItem()
        Me.GalleryDropDown1 = New DevExpress.XtraBars.Ribbon.GalleryDropDown(Me.components)
        Me.mnuAjusteStock = New DevExpress.XtraBars.BarSubItem()
        Me.cmdMotivoAjuste = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdTipoAjuste = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdAjusteInventario = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem10 = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdControl = New DevExpress.XtraBars.BarSubItem()
        Me.cmdProximosVencer = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdMinMax = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdPendientesReq = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdStockTrans = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdRotacionProd = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdStockEnFecha = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuReporteDistribucionPorTramo = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdEstacionalidadProducto = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdAuditoriaPicking = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdReglasVencimiento = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTiemposRecepcion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTiemposDespacho = New DevExpress.XtraBars.BarButtonItem()
        Me.mnurptTransaccionesOP = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuResetMenuLayOut = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTareasPreIngreso = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdStockRes = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEstadoEnviosNAV = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuBackup = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCambioDeUsuario = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdTransOut = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdBarrasPlt = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdMi3 = New DevExpress.XtraBars.BarSubItem()
        Me.cmdTransaccionesOut = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTransaccionesPendientesReenvio = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdBarrasPallet = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdLogInterface = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdPrintSvr = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuDashBoardDesigner = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuConfiguracionInt = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRepSalidaRapido = New DevExpress.XtraBars.BarSubItem()
        Me.mnuRepSalidasRapidoD = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdDocPeConDiferencias = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSalidasDiasPiso = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdPackingDespachados = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRepFiscales = New DevExpress.XtraBars.BarSubItem()
        Me.cmdHistResGeneral = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdResumenCliente = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdCtas = New DevExpress.XtraBars.BarSubItem()
        Me.cmdCtasOrden = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdCtaOrdenPoliza = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdMovimiento_Reporte = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdResValorizacion = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdResValorizacionMerca = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdAuditoriaRetroactivo = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdIngresoPoliza = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdMercaVencida = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdCtaOrden = New DevExpress.XtraBars.BarButtonItem()
        Me.BarSubItem2 = New DevExpress.XtraBars.BarSubItem()
        Me.cmdHistResFiscal = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRepFiscal = New DevExpress.XtraBars.BarSubItem()
        Me.bbiCambiaBodega = New DevExpress.XtraBars.BarButtonItem()
        Me.pmBodegas = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.sddiSkinWMS = New DevExpress.XtraBars.SkinDropDownButtonItem()
        Me.cmdServicios = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuReporteServicios = New DevExpress.XtraBars.BarSubItem()
        Me.btnServicios = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuReportesGallery = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuIndicadores = New DevExpress.XtraBars.BarSubItem()
        Me.mnuAnalitica1 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAnalitica2 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAnalitica3 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuKPIResumen = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRegistroServicios = New DevExpress.XtraBars.BarSubItem()
        Me.mnuServiciosIngreso = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuServiciosSalidas = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdStockResJornada = New DevExpress.XtraBars.BarButtonItem()
        Me.btInvInicial = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem15 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuLogErrorWMS = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAnalitica = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuQAEscenariosReserva = New DevExpress.XtraBars.BarSubItem()
        Me.mnuCaso1ReservaIdealsa = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuQAReservas = New DevExpress.XtraBars.BarButtonItem()
        Me.lblModoDebug = New DevExpress.XtraBars.BarStaticItem()
        Me.mnuReportesSAT = New DevExpress.XtraBars.BarSubItem()
        Me.mnuRptIngresosSAT = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRptSalidasSAT = New DevExpress.XtraBars.BarButtonItem()
        Me.mnurptExistenciasSAT = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizarBD = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizarIndices = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuGestionInventarioCalidad = New DevExpress.XtraBars.BarSubItem()
        Me.mnuHabilitacionLotes = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuReportesControlCalidad = New DevExpress.XtraBars.BarSubItem()
        Me.cmdMovimientosControlCalidad = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTamañoTablas = New DevExpress.XtraBars.BarButtonItem()
        Me.lblWSHHURL = New DevExpress.XtraBars.BarStaticItem()
        Me.mnuManufactura = New DevExpress.XtraBars.BarSubItem()
        Me.cmdTransaccionesManufactura = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdTipo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPreFacturacion = New DevExpress.XtraBars.BarSubItem()
        Me.cmdPreFacturar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdAcuerdosyServicios = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuVisualizarTableroWMS = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuProductividad = New DevExpress.XtraBars.BarSubItem()
        Me.mnuProductividadPicking = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuInterfaceDMS = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuVerificacionBOF = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdOcupacionArea = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdIA = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuStockTag = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonMiniToolbar1 = New DevExpress.XtraBars.Ribbon.RibbonMiniToolbar(Me.components)
        Me.rpCatalogos = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.pgEmpresa = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgBodega = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgProductos = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgLogistica = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgClientes = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.PgPropietario2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgProveedores = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgSeguridad = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RpgCalendario = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rbPageMonitor = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rbPageQA = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpIngresos = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.rpgOdenCompra = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgTareaIngreso = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgTareasPreIngreso = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpSalidas = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.pgPedidoVenta = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgPicking = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgDespacho = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.grpStockRes = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgCotizar = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgVerificacionBOF = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpServicios = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.pgRegistroServicios = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgReporteServicios = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgPreFacturacion = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpInventarios = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.pgInventarios = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpMovimientos = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.pgCambioUbicacion = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgCambioEstado = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgTraslados = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgAjusteInventario = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpReportes = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.pgReportes = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgReporteFiscal = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgReportesIngresoRapido = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgSalidasRep = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.grpRepTablero = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgReportesGallery = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgIndicadores = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgLogWMS = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgSat = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgProductividad = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpAdministrador = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.pgParametrosCn = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgInterface = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgLicencia = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgResetMenu = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgReporteTransacciones = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.pgBackup = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgCambioDeUsuario = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgServicioImpresiones = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgActualizacionesBD = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpControlCalidad = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.rpGestionInventario = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpManufacturaLigera = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.rpMLTareas = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpRFID = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.rpgRFID = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.mnuImpresionBarraPallet = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuListaIngresoTag = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuListaSalidaTag = New DevExpress.XtraBars.BarButtonItem()
        Me.RepositoryItemButtonEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.RepositoryItemSearchLookUpEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.RepositoryItemSearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemZoomTrackBar1 = New DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.PopupControlContainer1 = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.lblprg = New System.Windows.Forms.RichTextBox()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.xtMdi = New DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(Me.components)
        Me.RibbonPageGroup4 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup7 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup14 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.BarSubItem5 = New DevExpress.XtraBars.BarSubItem()
        Me.BarStaticItem1 = New DevExpress.XtraBars.BarStaticItem()
        Me.BarStaticItem2 = New DevExpress.XtraBars.BarStaticItem()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.colSeleccionar = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colProducto = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarButtonItem11 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem12 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuReporteTransNav = New DevExpress.XtraBars.BarSubItem()
        Me.PopupMenu3 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.BarButtonItem16 = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.rbMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemBreadCrumbEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GalleryDropDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pmBodegas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupControlContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PopupControlContainer1.SuspendLayout()
        CType(Me.xtMdi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rbMain
        '
        Me.rbMain.AutoSaveLayoutToXml = True
        Me.rbMain.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(174)
        Me.rbMain.ExpandCollapseItem.Id = 0
        Me.rbMain.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.rbMain.ExpandCollapseItem, Me.mnuBodega, Me.mnuUbicacion, Me.mnuProductos, Me.mnuMantClas, Me.mnuFamilia, Me.mnuMarca, Me.mnuMantEstados, Me.mnuMantTipo, Me.mnuMantUnidMed, Me.mnuMantProducto, Me.mnuMantOperadores, Me.SkinRibbonGalleryBarItem1, Me.RibbonGalleryBarItem1, Me.SkinRibbonGalleryBarItem2, Me.mnuMantPropietarios, Me.mnuEmpresa, Me.mnuMuelles, Me.mnuMantMotivoAnul, Me.mnuMantMotivoDevol, Me.mnuMantEmpresa, Me.mnuClientes, Me.mnuMantCliente, Me.mnuTipoCliente, Me.mnuTiemposAceptacion, Me.mnuProveedores, Me.mnuSeguridad, Me.mnuMantUsuarios, Me.mnuRolesUsuario, Me.lblNombrePCCliente, Me.lblVersion, Me.mnuOrdenesCompra, Me.mnuRecepcion, Me.mnuRepIngresoRapido, Me.mnuLogistica, Me.mnuEmpTrans, Me.mnuVehiculos, Me.mnuCambioUbicacion, Me.mnuCambioEstado, Me.mnuCotizar, Me.mnuPedidoVenta, Me.mnuPicking, Me.mnuDespachos, Me.BarButtonItem38, Me.BarButtonItem39, Me.BarButtonItem40, Me.mnuRepSalidasRapido, Me.mnuInventarios, Me.mnuMantJornadaLab, Me.mnuControlMontacargas, Me.lblUsuario, Me.mnuMantPais, Me.mnuMantHorarioLab, Me.mnuMantTurnoLab, Me.mnuPiloto, Me.mnuPropietarioBodega, Me.BarButtonGroup1, Me.BarButtonGroup2, Me.BarButtonGroup3, Me.BarButtonGroup4, Me.BarMdiChildrenListItem1, Me.mnuMantBodega, Me.BarButtonItem1, Me.BarSubProveedor, Me.cmdProveedor, Me.cmdProveedores, Me.cmdArancel, Me.BarButtonItem3, Me.BarButtonGroup5, Me.BarButtonItem4, Me.mnuVendedorRoad, Me.mnuVendedorRuta, Me.mnuFallaMontacarga, Me.mnuRolesOperador, Me.cmdCalendario, Me.mnuTraslados, Me.BarSubItem1, Me.BarLinkContainerItem1, Me.BarMdiChildrenListItem2, Me.BarButtonItem5, Me.BarButtonItem6, Me.mnuGeograficos, Me.mnuDepartamento, Me.mnuMunicpio, Me.mnuRegion, Me.mnuMotivoUbic, Me.lblHoraActual, Me.mnuExistencias, Me.BarButtonItem7, Me.cmdDetalleExistencia, Me.cmdDetalleSerie, Me.cmdDetalleParametro, Me.BarButtonItem9, Me.mnuTipoTarima, Me.mnuTipoConte, Me.mnuTarima, Me.BarButtonItem13, Me.BarButtonItem14, Me.mnuPropietarios, Me.mnuMantPropietario, Me.mnuMantReglaMsj, Me.mnuMantReglaRc, Me.BarButtonItem18, Me.mnuMonitor, Me.mnuConfigurarMonitor, Me.mnuMostrarMonitor, Me.cmdImpresora, Me.cmdConexionBD, Me.lblServerAPP, Me.lblBDAPP, Me.cmdUbicacionPicking, Me.cmdTipoIngreso, Me.mnuOrdenCompra, Me.mnuUbicSug, Me.mnuIndiceRot, Me.mnuReglaUbicacion, Me.mnuReglasList, Me.BarEditItem1, Me.BarButtonItem24, Me.cmdIngresos, Me.BarButtonItem25, Me.mnuReglaUbicPrio, Me.BarButtonItem27, Me.mnuConversion, Me.cmdLetra, Me.cmdTipoEtiqueta, Me.mnuImpEtiqueta, Me.mnuInterfaceNav, Me.lblEmpresa, Me.lblBodega, Me.cmdExistenciasProductos, Me.cmdValorizacion, Me.cmdMovimientos, Me.cmdMovimiento, Me.cmdMovimientosDet, Me.mnuLicencia, Me.mnuLicencias, Me.cmdUnidadMedidaconversion, Me.cmdResumenExistencia, Me.cmdResumenExistenciasUMBas, Me.mnuEstructuraInicial, Me.cmdMovimientosCardex, Me.BarButtonItem8, Me.mnuAjusteStock, Me.cmdMotivoAjuste, Me.cmdTipoAjuste, Me.cmdAjusteInventario, Me.BarButtonItem10, Me.cmdExistenciasEstado, Me.cmdMovimientosUbic, Me.cmdControl, Me.cmdProximosVencer, Me.cmdMinMax, Me.cmdPendientesReq, Me.cmdStockTrans, Me.cmdRotacionProd, Me.cmdTrazaLote, Me.mnuResetMenuLayOut, Me.cmdStockEnFecha, Me.mnuTareasPreIngreso, Me.cmdExistenciasUbic, Me.cmdExistenciaspordocumento, Me.mnuCambiarContraseña, Me.cmdExistenciasPorLote, Me.cmdDetalleLotePorUbi, Me.cmdMovCardexConDocs, Me.cmdStockRes, Me.mnuEstadoEnviosNAV, Me.cmdExistenciaPorTipoProd, Me.mnuBackup, Me.mnuCambioDeUsuario, Me.cmdDetalleInventario, Me.cmdSerieDocs, Me.cmdRpExitLP, Me.cmdExistCnRec, Me.cmdTransOut, Me.cmdBarrasPlt, Me.cmdMi3, Me.cmdTransaccionesOut, Me.cmdBarrasPallet, Me.cmdCodBarras, Me.cmdPrintSvr, Me.cmdLogInterface, Me.cmdPrint, Me.mnuDashBoardDesigner, Me.mnuTipoCuadrilla, Me.mnuCuadrilla, Me.mnuReporteDistribucionPorTramo, Me.mnuServicios, Me.mnuTarifas, Me.mnuRegimenFiscal, Me.mnuConfiguracionInt, Me.cmdDocConDiferencias, Me.mnuRepSalidaRapido, Me.cmdDocPeConDiferencias, Me.mnuRepSalidasRapidoD, Me.cmdExistFiscal, Me.mnuTiemposTareas, Me.mnuRepFiscales, Me.cmdCtaOrden, Me.BarSubItem2, Me.cmdHistResGeneral, Me.cmdHistResFiscal, Me.cmdResumenCliente, Me.mnuRepFiscal, Me.mnuMantConsolidador, Me.cmdCtas, Me.cmdCtasOrden, Me.cmdCtaOrdenPoliza, Me.cmdMovimiento_Reporte, Me.bbiCambiaBodega, Me.sddiSkinWMS, Me.cmdExistConsolidador, Me.cmdExistPorClasif, Me.cmdExistenciasPropietario, Me.cmdExistenciasPorLote_Posicion, Me.cmdStockJornadaSistema, Me.cmdServicios, Me.mnuReporteServicios, Me.btnServicios, Me.mnuReportesGallery, Me.mnuIndicadores, Me.mnuAnalitica1, Me.mnuRegistroServicios, Me.mnuServiciosIngreso, Me.mnuServiciosSalidas, Me.mnuTiposDocumentoIngreso, Me.mnuTiposDocumentoSalida, Me.mnuKardexLote, Me.cmdValorizacionOC, Me.cmdStockResJornada, Me.cmdResValorizacion, Me.cmdMovporLote, Me.cmdResValorizacionMerca, Me.btInvInicial, Me.mnuAnalitica2, Me.mnuAnalitica3, Me.cmdEstacionalidadProducto, Me.cmdStockEnLinea, Me.cmdMovimientosPoliza, Me.mnuMantCentroCostos, Me.cmdAuditoriaPicking, Me.mnuZonaPicking, Me.cmdStockJornada, Me.BarButtonItem15, Me.mnuParametros, Me.cmdParametroA, Me.cmdParametroB, Me.cmdMovimientosDoc, Me.cmdSalidasDiasPiso, Me.mnuLogErrorWMS, Me.cmdAuditoriaRetroactivo, Me.cmdLicenciasPorUbicacion, Me.mnuAnalitica, Me.cmdIngresoPoliza, Me.mnuQAEscenariosReserva, Me.mnuCaso1ReservaIdealsa, Me.mnuQAReservas, Me.lblModoDebug, Me.mnuReportesSAT, Me.mnuRptIngresosSAT, Me.mnuRptSalidasSAT, Me.mnurptExistenciasSAT, Me.mnuActualizarBD, Me.mnuActualizarIndices, Me.mnuGestionInventarioCalidad, Me.mnuHabilitacionLotes, Me.mnuReportesControlCalidad, Me.cmdMovimientosControlCalidad, Me.mnuTamañoTablas, Me.lblWSHHURL, Me.cmdReglasVencimiento, Me.mnuManufactura, Me.cmdTransaccionesManufactura, Me.cmdTipo, Me.mnuPreFacturacion, Me.cmdPreFacturar, Me.cmdAcuerdosyServicios, Me.cmdMercaVencida, Me.mnuTiemposRecepcion, Me.mnuRptAjustesInventario, Me.mnuTiemposDespacho, Me.mnuKPIResumen, Me.mnuVisualizarTableroWMS, Me.mnuTransaccionesPendientesReenvio, Me.cmdMovimientosEstado, Me.cmdPackingDespachados, Me.mnuTalla, Me.mnuColor, Me.mnuProductividad, Me.mnuProductividadPicking, Me.mnurptTransaccionesOP, Me.mnuCampaña, Me.mnuInterfaceDMS, Me.mnuVerificacionBOF, Me.cmdOcupacionArea, Me.cmdIA, Me.mnuStockTag, Me.BarButtonItem16})
        Me.rbMain.Location = New System.Drawing.Point(0, 0)
        Me.rbMain.Margin = New System.Windows.Forms.Padding(38, 48, 38, 48)
        Me.rbMain.MaxItemId = 382
        Me.rbMain.MiniToolbars.Add(Me.RibbonMiniToolbar1)
        Me.rbMain.Name = "rbMain"
        Me.rbMain.OptionsMenuMinWidth = 1962
        Me.rbMain.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.rpCatalogos, Me.rpIngresos, Me.rpSalidas, Me.rpServicios, Me.rpInventarios, Me.rpMovimientos, Me.rpReportes, Me.rpAdministrador, Me.rpControlCalidad, Me.rpManufacturaLigera, Me.rpRFID})
        Me.rbMain.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemButtonEdit1, Me.RepositoryItemBreadCrumbEdit1, Me.RepositoryItemSearchLookUpEdit1, Me.RepositoryItemZoomTrackBar1})
        Me.rbMain.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.rbMain.Size = New System.Drawing.Size(1714, 193)
        Me.rbMain.StatusBar = Me.RibbonStatusBar
        '
        'mnuBodega
        '
        Me.mnuBodega.Caption = "Bodega"
        Me.mnuBodega.Id = 3
        Me.mnuBodega.ImageOptions.SvgImage = CType(resources.GetObject("mnuBodega.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuBodega.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantBodega), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMuelles), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantOperadores), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantTurnoLab), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantJornadaLab), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantHorarioLab), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMotivoUbic), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuTipoTarima), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuTipoConte), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuTarima), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuIndiceRot), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuReglasList), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuReglaUbicPrio), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdLetra), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuImpEtiqueta), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuEstructuraInicial), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdSerieDocs), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdPrint), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuTipoCuadrilla), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCuadrilla), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuRegimenFiscal), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuTiemposTareas), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuZonaPicking)})
        Me.mnuBodega.Name = "mnuBodega"
        '
        'mnuMantBodega
        '
        Me.mnuMantBodega.Caption = "Bodega"
        Me.mnuMantBodega.Id = 75
        Me.mnuMantBodega.Name = "mnuMantBodega"
        '
        'mnuMuelles
        '
        Me.mnuMuelles.Caption = "Muelles"
        Me.mnuMuelles.Id = 9
        Me.mnuMuelles.Name = "mnuMuelles"
        '
        'mnuMantOperadores
        '
        Me.mnuMantOperadores.Caption = "Operadores"
        Me.mnuMantOperadores.Id = 19
        Me.mnuMantOperadores.Name = "mnuMantOperadores"
        '
        'mnuMantTurnoLab
        '
        Me.mnuMantTurnoLab.Caption = "Turno"
        Me.mnuMantTurnoLab.Id = 67
        Me.mnuMantTurnoLab.Name = "mnuMantTurnoLab"
        '
        'mnuMantJornadaLab
        '
        Me.mnuMantJornadaLab.Caption = "Jornada Laboral"
        Me.mnuMantJornadaLab.Id = 59
        Me.mnuMantJornadaLab.Name = "mnuMantJornadaLab"
        '
        'mnuMantHorarioLab
        '
        Me.mnuMantHorarioLab.Caption = "Horario Laboral"
        Me.mnuMantHorarioLab.Id = 66
        Me.mnuMantHorarioLab.Name = "mnuMantHorarioLab"
        '
        'mnuMotivoUbic
        '
        Me.mnuMotivoUbic.Caption = "Motivo Ubicación"
        Me.mnuMotivoUbic.Id = 107
        Me.mnuMotivoUbic.Name = "mnuMotivoUbic"
        '
        'mnuTipoTarima
        '
        Me.mnuTipoTarima.Caption = "Tipo Tarima"
        Me.mnuTipoTarima.Id = 120
        Me.mnuTipoTarima.Name = "mnuTipoTarima"
        '
        'mnuTipoConte
        '
        Me.mnuTipoConte.Caption = "Tipo Contenedor"
        Me.mnuTipoConte.Id = 121
        Me.mnuTipoConte.Name = "mnuTipoConte"
        '
        'mnuTarima
        '
        Me.mnuTarima.Caption = "Tarima"
        Me.mnuTarima.Id = 122
        Me.mnuTarima.Name = "mnuTarima"
        '
        'mnuIndiceRot
        '
        Me.mnuIndiceRot.Caption = "Indice Rotación"
        Me.mnuIndiceRot.Id = 147
        Me.mnuIndiceRot.Name = "mnuIndiceRot"
        '
        'mnuReglasList
        '
        Me.mnuReglasList.Caption = "Reglas de ubicación"
        Me.mnuReglasList.Id = 150
        Me.mnuReglasList.Name = "mnuReglasList"
        '
        'mnuReglaUbicPrio
        '
        Me.mnuReglaUbicPrio.Caption = "Reglas de ubicación - Prioridades"
        Me.mnuReglaUbicPrio.Id = 155
        Me.mnuReglaUbicPrio.Name = "mnuReglaUbicPrio"
        '
        'cmdLetra
        '
        Me.cmdLetra.Caption = "Letra de Tramo"
        Me.cmdLetra.Id = 158
        Me.cmdLetra.Name = "cmdLetra"
        '
        'mnuImpEtiqueta
        '
        Me.mnuImpEtiqueta.Caption = "Imprimir Etiquetas "
        Me.mnuImpEtiqueta.Id = 160
        Me.mnuImpEtiqueta.Name = "mnuImpEtiqueta"
        '
        'mnuEstructuraInicial
        '
        Me.mnuEstructuraInicial.Caption = "Estructura Inicial"
        Me.mnuEstructuraInicial.Id = 187
        Me.mnuEstructuraInicial.Name = "mnuEstructuraInicial"
        '
        'cmdSerieDocs
        '
        Me.cmdSerieDocs.Caption = "Serie de Documentos"
        Me.cmdSerieDocs.Id = 224
        Me.cmdSerieDocs.Name = "cmdSerieDocs"
        '
        'cmdPrint
        '
        Me.cmdPrint.Caption = "Impresora"
        Me.cmdPrint.Id = 236
        Me.cmdPrint.Name = "cmdPrint"
        '
        'mnuTipoCuadrilla
        '
        Me.mnuTipoCuadrilla.Caption = "Tipo Cuadrilla"
        Me.mnuTipoCuadrilla.Id = 238
        Me.mnuTipoCuadrilla.Name = "mnuTipoCuadrilla"
        '
        'mnuCuadrilla
        '
        Me.mnuCuadrilla.Caption = "Cuadrilla"
        Me.mnuCuadrilla.Id = 239
        Me.mnuCuadrilla.Name = "mnuCuadrilla"
        '
        'mnuRegimenFiscal
        '
        Me.mnuRegimenFiscal.Caption = "Régimen Fiscal"
        Me.mnuRegimenFiscal.Id = 243
        Me.mnuRegimenFiscal.Name = "mnuRegimenFiscal"
        '
        'mnuTiemposTareas
        '
        Me.mnuTiemposTareas.Caption = "Tiempos Tareas"
        Me.mnuTiemposTareas.Id = 259
        Me.mnuTiemposTareas.Name = "mnuTiemposTareas"
        '
        'mnuZonaPicking
        '
        Me.mnuZonaPicking.Caption = "Zona de Picking"
        Me.mnuZonaPicking.Id = 314
        Me.mnuZonaPicking.Name = "mnuZonaPicking"
        '
        'mnuUbicacion
        '
        Me.mnuUbicacion.Caption = "Ubicaciones"
        Me.mnuUbicacion.Id = 8
        Me.mnuUbicacion.Name = "mnuUbicacion"
        '
        'mnuProductos
        '
        Me.mnuProductos.Caption = "Productos"
        Me.mnuProductos.Id = 9
        Me.mnuProductos.ImageOptions.SvgImage = CType(resources.GetObject("mnuProductos.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuProductos.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantClas), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuFamilia), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMarca), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantEstados), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantTipo), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantUnidMed), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdUnidadMedidaconversion), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantProducto), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdCodBarras), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuParametros), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuTalla), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuColor), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCampaña)})
        Me.mnuProductos.Name = "mnuProductos"
        '
        'mnuMantClas
        '
        Me.mnuMantClas.Caption = "Clasificación"
        Me.mnuMantClas.Id = 10
        Me.mnuMantClas.Name = "mnuMantClas"
        '
        'mnuFamilia
        '
        Me.mnuFamilia.Caption = "Familia"
        Me.mnuFamilia.Id = 11
        Me.mnuFamilia.Name = "mnuFamilia"
        '
        'mnuMarca
        '
        Me.mnuMarca.Caption = "Marca"
        Me.mnuMarca.Id = 12
        Me.mnuMarca.Name = "mnuMarca"
        '
        'mnuMantEstados
        '
        Me.mnuMantEstados.Caption = "Estado"
        Me.mnuMantEstados.Id = 13
        Me.mnuMantEstados.Name = "mnuMantEstados"
        '
        'mnuMantTipo
        '
        Me.mnuMantTipo.Caption = "Tipo"
        Me.mnuMantTipo.Id = 14
        Me.mnuMantTipo.Name = "mnuMantTipo"
        '
        'mnuMantUnidMed
        '
        Me.mnuMantUnidMed.Caption = "Unidad de medida"
        Me.mnuMantUnidMed.Id = 16
        Me.mnuMantUnidMed.Name = "mnuMantUnidMed"
        '
        'cmdUnidadMedidaconversion
        '
        Me.cmdUnidadMedidaconversion.Caption = "Unidad Medida Conversión"
        Me.cmdUnidadMedidaconversion.Id = 184
        Me.cmdUnidadMedidaconversion.Name = "cmdUnidadMedidaconversion"
        '
        'mnuMantProducto
        '
        Me.mnuMantProducto.Caption = "Producto"
        Me.mnuMantProducto.Id = 18
        Me.mnuMantProducto.Name = "mnuMantProducto"
        '
        'cmdCodBarras
        '
        Me.cmdCodBarras.Caption = "Impresión de etiquetas"
        Me.cmdCodBarras.Id = 233
        Me.cmdCodBarras.Name = "cmdCodBarras"
        '
        'mnuParametros
        '
        Me.mnuParametros.Caption = "Parámetros"
        Me.mnuParametros.Id = 319
        Me.mnuParametros.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdParametroA), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdParametroB)})
        Me.mnuParametros.Name = "mnuParametros"
        '
        'cmdParametroA
        '
        Me.cmdParametroA.Caption = "Parámetros A"
        Me.cmdParametroA.Id = 320
        Me.cmdParametroA.Name = "cmdParametroA"
        '
        'cmdParametroB
        '
        Me.cmdParametroB.Caption = "Parámetros B"
        Me.cmdParametroB.Id = 321
        Me.cmdParametroB.Name = "cmdParametroB"
        '
        'mnuTalla
        '
        Me.mnuTalla.Caption = "Talla"
        Me.mnuTalla.Id = 364
        Me.mnuTalla.Name = "mnuTalla"
        '
        'mnuColor
        '
        Me.mnuColor.Caption = "Color"
        Me.mnuColor.Id = 365
        Me.mnuColor.Name = "mnuColor"
        '
        'mnuCampaña
        '
        Me.mnuCampaña.Caption = "Campaña"
        Me.mnuCampaña.Id = 370
        Me.mnuCampaña.Name = "mnuCampaña"
        '
        'SkinRibbonGalleryBarItem1
        '
        Me.SkinRibbonGalleryBarItem1.Caption = "SkinRibbonGalleryBarItem1"
        Me.SkinRibbonGalleryBarItem1.Id = 24
        Me.SkinRibbonGalleryBarItem1.Name = "SkinRibbonGalleryBarItem1"
        '
        'RibbonGalleryBarItem1
        '
        Me.RibbonGalleryBarItem1.Caption = "RibbonGalleryBarItem1"
        '
        '
        '
        GalleryItemGroup1.Caption = "Group4"
        GalleryItem1.Caption = "Item4"
        GalleryItemGroup1.Items.AddRange(New DevExpress.XtraBars.Ribbon.GalleryItem() {GalleryItem1})
        Me.RibbonGalleryBarItem1.Gallery.Groups.AddRange(New DevExpress.XtraBars.Ribbon.GalleryItemGroup() {GalleryItemGroup1})
        Me.RibbonGalleryBarItem1.Id = 25
        Me.RibbonGalleryBarItem1.Name = "RibbonGalleryBarItem1"
        '
        'SkinRibbonGalleryBarItem2
        '
        Me.SkinRibbonGalleryBarItem2.Caption = "SkinRibbonGalleryBarItem2"
        Me.SkinRibbonGalleryBarItem2.Id = 26
        Me.SkinRibbonGalleryBarItem2.Name = "SkinRibbonGalleryBarItem2"
        '
        'mnuMantPropietarios
        '
        Me.mnuMantPropietarios.Caption = "Propietarios"
        Me.mnuMantPropietarios.Id = 1
        Me.mnuMantPropietarios.Name = "mnuMantPropietarios"
        '
        'mnuEmpresa
        '
        Me.mnuEmpresa.Caption = "Empresa"
        Me.mnuEmpresa.Id = 7
        Me.mnuEmpresa.ImageOptions.SvgImage = CType(resources.GetObject("mnuEmpresa.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEmpresa.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantEmpresa), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantMotivoAnul), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantMotivoDevol), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantCentroCostos), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdArancel), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuGeograficos, True), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuTiposDocumentoIngreso), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuTiposDocumentoSalida)})
        Me.mnuEmpresa.Name = "mnuEmpresa"
        '
        'mnuMantEmpresa
        '
        Me.mnuMantEmpresa.Caption = "Empresa"
        Me.mnuMantEmpresa.Id = 12
        Me.mnuMantEmpresa.Name = "mnuMantEmpresa"
        '
        'mnuMantMotivoAnul
        '
        Me.mnuMantMotivoAnul.Caption = "Motivos Anulación"
        Me.mnuMantMotivoAnul.Id = 10
        Me.mnuMantMotivoAnul.Name = "mnuMantMotivoAnul"
        '
        'mnuMantMotivoDevol
        '
        Me.mnuMantMotivoDevol.Caption = "Motivos Devolución"
        Me.mnuMantMotivoDevol.Id = 11
        Me.mnuMantMotivoDevol.Name = "mnuMantMotivoDevol"
        '
        'mnuMantCentroCostos
        '
        Me.mnuMantCentroCostos.Caption = "Centro de Costos"
        Me.mnuMantCentroCostos.Id = 312
        Me.mnuMantCentroCostos.Name = "mnuMantCentroCostos"
        '
        'cmdArancel
        '
        Me.cmdArancel.Caption = "Arancel"
        Me.cmdArancel.Id = 80
        Me.cmdArancel.Name = "cmdArancel"
        '
        'mnuGeograficos
        '
        Me.mnuGeograficos.Caption = "Geográficos"
        Me.mnuGeograficos.Id = 103
        Me.mnuGeograficos.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantPais), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuDepartamento), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMunicpio), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuRegion)})
        Me.mnuGeograficos.Name = "mnuGeograficos"
        '
        'mnuMantPais
        '
        Me.mnuMantPais.Caption = "País"
        Me.mnuMantPais.Id = 63
        Me.mnuMantPais.Name = "mnuMantPais"
        '
        'mnuDepartamento
        '
        Me.mnuDepartamento.Caption = "Departamento"
        Me.mnuDepartamento.Id = 104
        Me.mnuDepartamento.Name = "mnuDepartamento"
        '
        'mnuMunicpio
        '
        Me.mnuMunicpio.Caption = "Municipio"
        Me.mnuMunicpio.Id = 105
        Me.mnuMunicpio.Name = "mnuMunicpio"
        '
        'mnuRegion
        '
        Me.mnuRegion.Caption = "Región"
        Me.mnuRegion.Id = 106
        Me.mnuRegion.Name = "mnuRegion"
        '
        'mnuTiposDocumentoIngreso
        '
        Me.mnuTiposDocumentoIngreso.Caption = "Tipos de documento ingreso"
        Me.mnuTiposDocumentoIngreso.Id = 297
        Me.mnuTiposDocumentoIngreso.Name = "mnuTiposDocumentoIngreso"
        '
        'mnuTiposDocumentoSalida
        '
        Me.mnuTiposDocumentoSalida.Caption = "Tipos de documento salida"
        Me.mnuTiposDocumentoSalida.Id = 298
        Me.mnuTiposDocumentoSalida.Name = "mnuTiposDocumentoSalida"
        '
        'mnuClientes
        '
        Me.mnuClientes.Caption = "Clientes"
        Me.mnuClientes.Id = 15
        Me.mnuClientes.ImageOptions.SvgImage = CType(resources.GetObject("mnuClientes.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuClientes.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantCliente, True), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuTipoCliente), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantConsolidador)})
        Me.mnuClientes.Name = "mnuClientes"
        '
        'mnuMantCliente
        '
        Me.mnuMantCliente.Caption = "Cliente"
        Me.mnuMantCliente.Id = 16
        Me.mnuMantCliente.Name = "mnuMantCliente"
        '
        'mnuTipoCliente
        '
        Me.mnuTipoCliente.Caption = "Tipos de cliente"
        Me.mnuTipoCliente.Id = 17
        Me.mnuTipoCliente.Name = "mnuTipoCliente"
        '
        'mnuMantConsolidador
        '
        Me.mnuMantConsolidador.Caption = "Consolidador"
        Me.mnuMantConsolidador.Id = 268
        Me.mnuMantConsolidador.Name = "mnuMantConsolidador"
        '
        'mnuTiemposAceptacion
        '
        Me.mnuTiemposAceptacion.Caption = "Tiempos de aceptación"
        Me.mnuTiemposAceptacion.Id = 18
        Me.mnuTiemposAceptacion.Name = "mnuTiemposAceptacion"
        '
        'mnuProveedores
        '
        Me.mnuProveedores.Caption = "Proveedores"
        Me.mnuProveedores.Id = 19
        Me.mnuProveedores.ImageOptions.Image = CType(resources.GetObject("mnuProveedores.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuProveedores.ImageOptions.LargeImage = CType(resources.GetObject("mnuProveedores.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuProveedores.Name = "mnuProveedores"
        Me.mnuProveedores.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'mnuSeguridad
        '
        Me.mnuSeguridad.Caption = "Seguridad"
        Me.mnuSeguridad.Id = 20
        Me.mnuSeguridad.ImageOptions.SvgImage = CType(resources.GetObject("mnuSeguridad.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuSeguridad.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantUsuarios), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuRolesUsuario), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuRolesOperador), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuCambiarContraseña)})
        Me.mnuSeguridad.Name = "mnuSeguridad"
        '
        'mnuMantUsuarios
        '
        Me.mnuMantUsuarios.Caption = "Usuarios"
        Me.mnuMantUsuarios.Id = 21
        Me.mnuMantUsuarios.Name = "mnuMantUsuarios"
        '
        'mnuRolesUsuario
        '
        Me.mnuRolesUsuario.Caption = "Roles de Usuario"
        Me.mnuRolesUsuario.Id = 22
        Me.mnuRolesUsuario.Name = "mnuRolesUsuario"
        '
        'mnuRolesOperador
        '
        Me.mnuRolesOperador.Caption = "Roles de Operador"
        Me.mnuRolesOperador.Id = 92
        Me.mnuRolesOperador.Name = "mnuRolesOperador"
        '
        'mnuCambiarContraseña
        '
        Me.mnuCambiarContraseña.Caption = "Cambiar Contraseña"
        Me.mnuCambiarContraseña.Id = 211
        Me.mnuCambiarContraseña.Name = "mnuCambiarContraseña"
        '
        'lblNombrePCCliente
        '
        Me.lblNombrePCCliente.Caption = "PROGRAN"
        Me.lblNombrePCCliente.Id = 23
        Me.lblNombrePCCliente.ImageOptions.Image = CType(resources.GetObject("lblNombrePCCliente.ImageOptions.Image"), System.Drawing.Image)
        Me.lblNombrePCCliente.Name = "lblNombrePCCliente"
        Me.lblNombrePCCliente.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'lblVersion
        '
        Me.lblVersion.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.lblVersion.Id = 25
        Me.lblVersion.ItemAppearance.Normal.BackColor = System.Drawing.Color.AntiqueWhite
        Me.lblVersion.ItemAppearance.Normal.ForeColor = System.Drawing.Color.Black
        Me.lblVersion.ItemAppearance.Normal.Options.UseBackColor = True
        Me.lblVersion.ItemAppearance.Normal.Options.UseForeColor = True
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Tag = "Versión 4.0 FP: 20191218"
        '
        'mnuOrdenesCompra
        '
        Me.mnuOrdenesCompra.Caption = "Ordenes de compra"
        Me.mnuOrdenesCompra.Id = 31
        Me.mnuOrdenesCompra.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.[False]
        Me.mnuOrdenesCompra.ImageOptions.Image = CType(resources.GetObject("mnuOrdenesCompra.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuOrdenesCompra.ImageOptions.LargeImage = CType(resources.GetObject("mnuOrdenesCompra.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuOrdenesCompra.Name = "mnuOrdenesCompra"
        '
        'mnuRecepcion
        '
        Me.mnuRecepcion.Caption = "Tareas de ingreso"
        Me.mnuRecepcion.Id = 32
        Me.mnuRecepcion.ImageOptions.SvgImage = CType(resources.GetObject("mnuRecepcion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuRecepcion.Name = "mnuRecepcion"
        '
        'mnuRepIngresoRapido
        '
        Me.mnuRepIngresoRapido.Caption = "Reporte de ingresos"
        Me.mnuRepIngresoRapido.Id = 34
        Me.mnuRepIngresoRapido.ImageOptions.SvgImage = CType(resources.GetObject("mnuRepIngresoRapido.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuRepIngresoRapido.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdIngresos), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdExistenciaspordocumento), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdDocConDiferencias)})
        Me.mnuRepIngresoRapido.Name = "mnuRepIngresoRapido"
        '
        'cmdIngresos
        '
        Me.cmdIngresos.Caption = "Ingresos"
        Me.cmdIngresos.Id = 153
        Me.cmdIngresos.Name = "cmdIngresos"
        '
        'cmdExistenciaspordocumento
        '
        Me.cmdExistenciaspordocumento.Caption = "Existencias por número de documento"
        Me.cmdExistenciaspordocumento.Id = 210
        Me.cmdExistenciaspordocumento.Name = "cmdExistenciaspordocumento"
        '
        'cmdDocConDiferencias
        '
        Me.cmdDocConDiferencias.Caption = "Documentos con diferencias"
        Me.cmdDocConDiferencias.Id = 245
        Me.cmdDocConDiferencias.Name = "cmdDocConDiferencias"
        '
        'mnuLogistica
        '
        Me.mnuLogistica.Caption = "Logística"
        Me.mnuLogistica.Id = 35
        Me.mnuLogistica.ImageOptions.SvgImage = CType(resources.GetObject("mnuLogistica.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuLogistica.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuEmpTrans), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuVehiculos), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuPiloto), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuControlMontacargas), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuFallaMontacarga), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuVendedorRoad), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuVendedorRuta), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuUbicSug), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdTipoEtiqueta), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuServicios), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuTarifas)})
        Me.mnuLogistica.Name = "mnuLogistica"
        '
        'mnuEmpTrans
        '
        Me.mnuEmpTrans.Caption = "Empresas de transporte"
        Me.mnuEmpTrans.Id = 36
        Me.mnuEmpTrans.Name = "mnuEmpTrans"
        '
        'mnuVehiculos
        '
        Me.mnuVehiculos.Caption = "Vehiculos"
        Me.mnuVehiculos.Id = 37
        Me.mnuVehiculos.Name = "mnuVehiculos"
        '
        'mnuPiloto
        '
        Me.mnuPiloto.Caption = "Pilotos"
        Me.mnuPiloto.Id = 68
        Me.mnuPiloto.Name = "mnuPiloto"
        '
        'mnuControlMontacargas
        '
        Me.mnuControlMontacargas.Caption = "Control de Montacarga"
        Me.mnuControlMontacargas.Id = 60
        Me.mnuControlMontacargas.Name = "mnuControlMontacargas"
        '
        'mnuFallaMontacarga
        '
        Me.mnuFallaMontacarga.Caption = "Falla de Montacarga"
        Me.mnuFallaMontacarga.Id = 91
        Me.mnuFallaMontacarga.Name = "mnuFallaMontacarga"
        '
        'mnuVendedorRoad
        '
        Me.mnuVendedorRoad.Caption = "Road Vendedor"
        Me.mnuVendedorRoad.Id = 89
        Me.mnuVendedorRoad.Name = "mnuVendedorRoad"
        '
        'mnuVendedorRuta
        '
        Me.mnuVendedorRuta.Caption = "Road Ruta"
        Me.mnuVendedorRuta.Id = 90
        Me.mnuVendedorRuta.Name = "mnuVendedorRuta"
        '
        'mnuUbicSug
        '
        Me.mnuUbicSug.Caption = "Ubic sugerida"
        Me.mnuUbicSug.Id = 146
        Me.mnuUbicSug.Name = "mnuUbicSug"
        '
        'cmdTipoEtiqueta
        '
        Me.cmdTipoEtiqueta.Caption = "Tipo Etiqueta"
        Me.cmdTipoEtiqueta.Id = 159
        Me.cmdTipoEtiqueta.Name = "cmdTipoEtiqueta"
        '
        'mnuServicios
        '
        Me.mnuServicios.Caption = "Servicios"
        Me.mnuServicios.Id = 241
        Me.mnuServicios.Name = "mnuServicios"
        '
        'mnuTarifas
        '
        Me.mnuTarifas.Caption = "Tarifas"
        Me.mnuTarifas.Id = 242
        Me.mnuTarifas.Name = "mnuTarifas"
        '
        'mnuCambioUbicacion
        '
        Me.mnuCambioUbicacion.Caption = "Cambio de ubicación"
        Me.mnuCambioUbicacion.Id = 40
        Me.mnuCambioUbicacion.ImageOptions.SvgImage = CType(resources.GetObject("mnuCambioUbicacion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuCambioUbicacion.Name = "mnuCambioUbicacion"
        '
        'mnuCambioEstado
        '
        Me.mnuCambioEstado.Caption = "Cambio de estado"
        Me.mnuCambioEstado.Id = 41
        Me.mnuCambioEstado.ImageOptions.SvgImage = CType(resources.GetObject("mnuCambioEstado.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuCambioEstado.Name = "mnuCambioEstado"
        '
        'mnuCotizar
        '
        Me.mnuCotizar.Caption = "Cotizar"
        Me.mnuCotizar.Id = 42
        Me.mnuCotizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuCotizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuCotizar.Name = "mnuCotizar"
        '
        'mnuPedidoVenta
        '
        Me.mnuPedidoVenta.Caption = "Documento de Salida"
        Me.mnuPedidoVenta.Id = 43
        Me.mnuPedidoVenta.ImageOptions.SvgImage = CType(resources.GetObject("mnuPedidoVenta.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuPedidoVenta.Name = "mnuPedidoVenta"
        '
        'mnuPicking
        '
        Me.mnuPicking.Caption = "Picking"
        Me.mnuPicking.Id = 46
        Me.mnuPicking.ImageOptions.SvgImage = CType(resources.GetObject("mnuPicking.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuPicking.Name = "mnuPicking"
        '
        'mnuDespachos
        '
        Me.mnuDespachos.Caption = "Despacho"
        Me.mnuDespachos.Id = 48
        Me.mnuDespachos.ImageOptions.SvgImage = CType(resources.GetObject("mnuDespachos.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuDespachos.Name = "mnuDespachos"
        '
        'BarButtonItem38
        '
        Me.BarButtonItem38.Caption = "BarButtonItem38"
        Me.BarButtonItem38.Id = 52
        Me.BarButtonItem38.Name = "BarButtonItem38"
        '
        'BarButtonItem39
        '
        Me.BarButtonItem39.Caption = "BarButtonItem39"
        Me.BarButtonItem39.Id = 53
        Me.BarButtonItem39.Name = "BarButtonItem39"
        '
        'BarButtonItem40
        '
        Me.BarButtonItem40.Caption = "BarButtonItem40"
        Me.BarButtonItem40.Id = 54
        Me.BarButtonItem40.Name = "BarButtonItem40"
        '
        'mnuRepSalidasRapido
        '
        Me.mnuRepSalidasRapido.ActAsDropDown = True
        Me.mnuRepSalidasRapido.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.mnuRepSalidasRapido.Caption = "Reporte de salidas"
        Me.mnuRepSalidasRapido.DropDownControl = Me.PopupMenu2
        Me.mnuRepSalidasRapido.Id = 56
        Me.mnuRepSalidasRapido.ImageOptions.Image = CType(resources.GetObject("mnuRepSalidasRapido.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuRepSalidasRapido.ImageOptions.LargeImage = CType(resources.GetObject("mnuRepSalidasRapido.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuRepSalidasRapido.Name = "mnuRepSalidasRapido"
        '
        'PopupMenu2
        '
        Me.PopupMenu2.Name = "PopupMenu2"
        Me.PopupMenu2.Ribbon = Me.rbMain
        '
        'mnuInventarios
        '
        Me.mnuInventarios.Caption = "Inventarios"
        Me.mnuInventarios.Id = 58
        Me.mnuInventarios.ImageOptions.SvgImage = CType(resources.GetObject("mnuInventarios.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuInventarios.Name = "mnuInventarios"
        '
        'lblUsuario
        '
        Me.lblUsuario.Caption = "Usuario:"
        Me.lblUsuario.Id = 62
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'mnuPropietarioBodega
        '
        Me.mnuPropietarioBodega.Caption = "Propietario Bodega"
        Me.mnuPropietarioBodega.Id = 69
        Me.mnuPropietarioBodega.Name = "mnuPropietarioBodega"
        '
        'BarButtonGroup1
        '
        Me.BarButtonGroup1.Caption = "BarButtonGroup1"
        Me.BarButtonGroup1.CategoryGuid = New System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537")
        Me.BarButtonGroup1.Id = 70
        Me.BarButtonGroup1.Name = "BarButtonGroup1"
        '
        'BarButtonGroup2
        '
        Me.BarButtonGroup2.Caption = "BarButtonGroup2"
        Me.BarButtonGroup2.CategoryGuid = New System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537")
        Me.BarButtonGroup2.Id = 71
        Me.BarButtonGroup2.Name = "BarButtonGroup2"
        '
        'BarButtonGroup3
        '
        Me.BarButtonGroup3.Caption = "BarButtonGroup3"
        Me.BarButtonGroup3.CategoryGuid = New System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537")
        Me.BarButtonGroup3.Id = 72
        Me.BarButtonGroup3.Name = "BarButtonGroup3"
        '
        'BarButtonGroup4
        '
        Me.BarButtonGroup4.Caption = "BarButtonGroup4"
        Me.BarButtonGroup4.CategoryGuid = New System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537")
        Me.BarButtonGroup4.Id = 73
        Me.BarButtonGroup4.Name = "BarButtonGroup4"
        '
        'BarMdiChildrenListItem1
        '
        Me.BarMdiChildrenListItem1.Caption = "BarMdiChildrenListItem1"
        Me.BarMdiChildrenListItem1.Id = 74
        Me.BarMdiChildrenListItem1.Name = "BarMdiChildrenListItem1"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "BarButtonItem1"
        Me.BarButtonItem1.Id = 76
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarSubProveedor
        '
        Me.BarSubProveedor.Caption = "Proveedores"
        Me.BarSubProveedor.Id = 77
        Me.BarSubProveedor.ImageOptions.Image = CType(resources.GetObject("BarSubProveedor.ImageOptions.Image"), System.Drawing.Image)
        Me.BarSubProveedor.ImageOptions.LargeImage = CType(resources.GetObject("BarSubProveedor.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarSubProveedor.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdProveedor)})
        Me.BarSubProveedor.Name = "BarSubProveedor"
        '
        'cmdProveedor
        '
        Me.cmdProveedor.Caption = "Proveedor"
        Me.cmdProveedor.Id = 78
        Me.cmdProveedor.Name = "cmdProveedor"
        '
        'cmdProveedores
        '
        Me.cmdProveedores.Caption = "Proveedores"
        Me.cmdProveedores.Id = 79
        Me.cmdProveedores.ImageOptions.SvgImage = CType(resources.GetObject("cmdProveedores.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdProveedores.Name = "cmdProveedores"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "BarButtonItem3"
        Me.BarButtonItem3.Id = 82
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'BarButtonGroup5
        '
        Me.BarButtonGroup5.Caption = "BarButtonGroup5"
        Me.BarButtonGroup5.Id = 83
        Me.BarButtonGroup5.Name = "BarButtonGroup5"
        '
        'BarButtonItem4
        '
        Me.BarButtonItem4.Caption = "BarButtonItem4"
        Me.BarButtonItem4.Id = 85
        Me.BarButtonItem4.Name = "BarButtonItem4"
        '
        'cmdCalendario
        '
        Me.cmdCalendario.Caption = "Calendario"
        Me.cmdCalendario.Id = 93
        Me.cmdCalendario.ImageOptions.Image = CType(resources.GetObject("cmdCalendario.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdCalendario.ImageOptions.LargeImage = CType(resources.GetObject("cmdCalendario.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdCalendario.Name = "cmdCalendario"
        '
        'mnuTraslados
        '
        Me.mnuTraslados.Caption = "Traslados"
        Me.mnuTraslados.Id = 94
        Me.mnuTraslados.ImageOptions.Image = CType(resources.GetObject("mnuTraslados.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuTraslados.ImageOptions.LargeImage = CType(resources.GetObject("mnuTraslados.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuTraslados.Name = "mnuTraslados"
        Me.mnuTraslados.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'BarSubItem1
        '
        Me.BarSubItem1.Caption = "BarSubItem1"
        Me.BarSubItem1.Id = 96
        Me.BarSubItem1.Name = "BarSubItem1"
        '
        'BarLinkContainerItem1
        '
        Me.BarLinkContainerItem1.Caption = "BarLinkContainerItem1"
        Me.BarLinkContainerItem1.Id = 97
        Me.BarLinkContainerItem1.Name = "BarLinkContainerItem1"
        '
        'BarMdiChildrenListItem2
        '
        Me.BarMdiChildrenListItem2.Caption = "BarMdiChildrenListItem2"
        Me.BarMdiChildrenListItem2.Id = 98
        Me.BarMdiChildrenListItem2.Name = "BarMdiChildrenListItem2"
        '
        'BarButtonItem5
        '
        Me.BarButtonItem5.Caption = "BarButtonItem5"
        Me.BarButtonItem5.Id = 99
        Me.BarButtonItem5.Name = "BarButtonItem5"
        '
        'BarButtonItem6
        '
        Me.BarButtonItem6.Caption = "Bodega Tree"
        Me.BarButtonItem6.Id = 100
        Me.BarButtonItem6.Name = "BarButtonItem6"
        '
        'lblHoraActual
        '
        Me.lblHoraActual.Caption = "00:00:00"
        Me.lblHoraActual.Id = 108
        Me.lblHoraActual.Name = "lblHoraActual"
        '
        'mnuExistencias
        '
        Me.mnuExistencias.Caption = "Detalle de existencias"
        Me.mnuExistencias.Id = 109
        Me.mnuExistencias.ImageOptions.Image = CType(resources.GetObject("mnuExistencias.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuExistencias.ImageOptions.LargeImage = CType(resources.GetObject("mnuExistencias.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuExistencias.Name = "mnuExistencias"
        '
        'BarButtonItem7
        '
        Me.BarButtonItem7.Caption = "Detalle de Existencias"
        Me.BarButtonItem7.Id = 113
        Me.BarButtonItem7.ImageOptions.Image = CType(resources.GetObject("BarButtonItem7.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem7.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem7.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonItem7.Name = "BarButtonItem7"
        '
        'cmdDetalleExistencia
        '
        Me.cmdDetalleExistencia.Caption = "Detalle de Existencias"
        Me.cmdDetalleExistencia.Id = 114
        Me.cmdDetalleExistencia.ImageOptions.SvgImage = CType(resources.GetObject("cmdDetalleExistencia.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdDetalleExistencia.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdResumenExistencia), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdResumenExistenciasUMBas), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdExistenciasProductos), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdExistenciasEstado), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdValorizacion), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdDetalleParametro), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdDetalleSerie), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdExistenciasUbic), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdExistenciasPorLote), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdDetalleLotePorUbi), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdExistenciaPorTipoProd), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdDetalleInventario), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdRpExitLP), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdExistCnRec), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdExistFiscal), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdExistConsolidador), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdExistPorClasif), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdExistenciasPropietario), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdExistenciasPorLote_Posicion), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdStockJornadaSistema), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdValorizacionOC), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdStockEnLinea), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdStockJornada), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdLicenciasPorUbicacion)})
        Me.cmdDetalleExistencia.Name = "cmdDetalleExistencia"
        '
        'cmdResumenExistencia
        '
        Me.cmdResumenExistencia.Caption = "1. Resumen existencias "
        Me.cmdResumenExistencia.Id = 185
        Me.cmdResumenExistencia.Name = "cmdResumenExistencia"
        '
        'cmdResumenExistenciasUMBas
        '
        Me.cmdResumenExistenciasUMBas.Caption = "2. Resumen existencias en U.M. Bas."
        Me.cmdResumenExistenciasUMBas.Id = 186
        Me.cmdResumenExistenciasUMBas.Name = "cmdResumenExistenciasUMBas"
        '
        'cmdExistenciasProductos
        '
        Me.cmdExistenciasProductos.Caption = "3. Detalle de existencias"
        Me.cmdExistenciasProductos.Id = 172
        Me.cmdExistenciasProductos.Name = "cmdExistenciasProductos"
        '
        'cmdExistenciasEstado
        '
        Me.cmdExistenciasEstado.Caption = "4. Resumen de existencias por estado"
        Me.cmdExistenciasEstado.Id = 196
        Me.cmdExistenciasEstado.Name = "cmdExistenciasEstado"
        '
        'cmdValorizacion
        '
        Me.cmdValorizacion.Caption = "5. Valorización de inventario"
        Me.cmdValorizacion.Id = 175
        Me.cmdValorizacion.Name = "cmdValorizacion"
        '
        'cmdDetalleParametro
        '
        Me.cmdDetalleParametro.Caption = "6. Existencias con parámetros"
        Me.cmdDetalleParametro.Id = 116
        Me.cmdDetalleParametro.Name = "cmdDetalleParametro"
        '
        'cmdDetalleSerie
        '
        Me.cmdDetalleSerie.Caption = "7. Exitencias con series"
        Me.cmdDetalleSerie.Id = 115
        Me.cmdDetalleSerie.Name = "cmdDetalleSerie"
        '
        'cmdExistenciasUbic
        '
        Me.cmdExistenciasUbic.Caption = "8. Existencias por ubicación"
        Me.cmdExistenciasUbic.Id = 209
        Me.cmdExistenciasUbic.Name = "cmdExistenciasUbic"
        '
        'cmdExistenciasPorLote
        '
        Me.cmdExistenciasPorLote.Caption = "9. Existencias por lote"
        Me.cmdExistenciasPorLote.Id = 212
        Me.cmdExistenciasPorLote.Name = "cmdExistenciasPorLote"
        '
        'cmdDetalleLotePorUbi
        '
        Me.cmdDetalleLotePorUbi.Caption = "10. Existencias de lotes por ubicación"
        Me.cmdDetalleLotePorUbi.Id = 214
        Me.cmdDetalleLotePorUbi.Name = "cmdDetalleLotePorUbi"
        '
        'cmdExistenciaPorTipoProd
        '
        Me.cmdExistenciaPorTipoProd.Caption = "11. Existencias por tipo de producto"
        Me.cmdExistenciaPorTipoProd.Id = 219
        Me.cmdExistenciaPorTipoProd.Name = "cmdExistenciaPorTipoProd"
        '
        'cmdDetalleInventario
        '
        Me.cmdDetalleInventario.Caption = "12. Detalle de inventario"
        Me.cmdDetalleInventario.Id = 223
        Me.cmdDetalleInventario.Name = "cmdDetalleInventario"
        '
        'cmdRpExitLP
        '
        Me.cmdRpExitLP.Caption = "13. Existencias por licencia"
        Me.cmdRpExitLP.Id = 225
        Me.cmdRpExitLP.Name = "cmdRpExitLP"
        '
        'cmdExistCnRec
        '
        Me.cmdExistCnRec.Caption = "14. Existencias por recepción"
        Me.cmdExistCnRec.Id = 226
        Me.cmdExistCnRec.Name = "cmdExistCnRec"
        '
        'cmdExistFiscal
        '
        Me.cmdExistFiscal.Caption = "15. Existencia con valor Fiscal"
        Me.cmdExistFiscal.Id = 258
        Me.cmdExistFiscal.Name = "cmdExistFiscal"
        '
        'cmdExistConsolidador
        '
        Me.cmdExistConsolidador.Caption = "16. Existencias por Consolidador"
        Me.cmdExistConsolidador.Id = 274
        Me.cmdExistConsolidador.Name = "cmdExistConsolidador"
        '
        'cmdExistPorClasif
        '
        Me.cmdExistPorClasif.Caption = "17. Existencias por Clasificación"
        Me.cmdExistPorClasif.Id = 276
        Me.cmdExistPorClasif.Name = "cmdExistPorClasif"
        '
        'cmdExistenciasPropietario
        '
        Me.cmdExistenciasPropietario.Caption = "18. Existencia por Propietario"
        Me.cmdExistenciasPropietario.Id = 277
        Me.cmdExistenciasPropietario.Name = "cmdExistenciasPropietario"
        '
        'cmdExistenciasPorLote_Posicion
        '
        Me.cmdExistenciasPorLote_Posicion.Caption = "19. Existencia por Lote y Posición"
        Me.cmdExistenciasPorLote_Posicion.Id = 278
        Me.cmdExistenciasPorLote_Posicion.Name = "cmdExistenciasPorLote_Posicion"
        '
        'cmdStockJornadaSistema
        '
        Me.cmdStockJornadaSistema.Caption = "20. Stock Jornada Sistema"
        Me.cmdStockJornadaSistema.Id = 281
        Me.cmdStockJornadaSistema.Name = "cmdStockJornadaSistema"
        '
        'cmdValorizacionOC
        '
        Me.cmdValorizacionOC.Caption = "21. Valorización por Doc. Ingreso"
        Me.cmdValorizacionOC.Id = 300
        Me.cmdValorizacionOC.Name = "cmdValorizacionOC"
        '
        'cmdStockEnLinea
        '
        Me.cmdStockEnLinea.Caption = "22. Inventarío en Línea"
        Me.cmdStockEnLinea.Id = 310
        Me.cmdStockEnLinea.Name = "cmdStockEnLinea"
        '
        'cmdStockJornada
        '
        Me.cmdStockJornada.Caption = "23. Stock jornada"
        Me.cmdStockJornada.Id = 315
        Me.cmdStockJornada.Name = "cmdStockJornada"
        '
        'cmdLicenciasPorUbicacion
        '
        Me.cmdLicenciasPorUbicacion.Caption = "24. Licencias por ubicación"
        Me.cmdLicenciasPorUbicacion.Id = 326
        Me.cmdLicenciasPorUbicacion.Name = "cmdLicenciasPorUbicacion"
        '
        'BarButtonItem9
        '
        Me.BarButtonItem9.Caption = "Tipo Tarima"
        Me.BarButtonItem9.Id = 119
        Me.BarButtonItem9.Name = "BarButtonItem9"
        '
        'BarButtonItem13
        '
        Me.BarButtonItem13.Caption = "Mensaje Regla"
        Me.BarButtonItem13.Id = 123
        Me.BarButtonItem13.Name = "BarButtonItem13"
        '
        'BarButtonItem14
        '
        Me.BarButtonItem14.Caption = "Reglas Recepcion"
        Me.BarButtonItem14.Id = 124
        Me.BarButtonItem14.Name = "BarButtonItem14"
        '
        'mnuPropietarios
        '
        Me.mnuPropietarios.Caption = "Propietarios"
        Me.mnuPropietarios.Id = 125
        Me.mnuPropietarios.ImageOptions.SvgImage = CType(resources.GetObject("mnuPropietarios.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuPropietarios.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantPropietario), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantReglaMsj), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMantReglaRc)})
        Me.mnuPropietarios.Name = "mnuPropietarios"
        '
        'mnuMantPropietario
        '
        Me.mnuMantPropietario.Caption = "Propietario"
        Me.mnuMantPropietario.Id = 126
        Me.mnuMantPropietario.Name = "mnuMantPropietario"
        '
        'mnuMantReglaMsj
        '
        Me.mnuMantReglaMsj.Caption = "Regla de Mensaje"
        Me.mnuMantReglaMsj.Id = 127
        Me.mnuMantReglaMsj.Name = "mnuMantReglaMsj"
        '
        'mnuMantReglaRc
        '
        Me.mnuMantReglaRc.Caption = "Regla de Recepción"
        Me.mnuMantReglaRc.Id = 128
        Me.mnuMantReglaRc.Name = "mnuMantReglaRc"
        '
        'BarButtonItem18
        '
        Me.BarButtonItem18.Caption = "Regla por Propietario"
        Me.BarButtonItem18.Id = 129
        Me.BarButtonItem18.Name = "BarButtonItem18"
        '
        'mnuMonitor
        '
        Me.mnuMonitor.Caption = "Monitor"
        Me.mnuMonitor.Id = 134
        Me.mnuMonitor.ImageOptions.Image = CType(resources.GetObject("mnuMonitor.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuMonitor.ImageOptions.LargeImage = CType(resources.GetObject("mnuMonitor.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuMonitor.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuMostrarMonitor), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuConfigurarMonitor)})
        Me.mnuMonitor.Name = "mnuMonitor"
        '
        'mnuMostrarMonitor
        '
        Me.mnuMostrarMonitor.Caption = "Mostrar"
        Me.mnuMostrarMonitor.Id = 136
        Me.mnuMostrarMonitor.Name = "mnuMostrarMonitor"
        '
        'mnuConfigurarMonitor
        '
        Me.mnuConfigurarMonitor.Caption = "Configurar"
        Me.mnuConfigurarMonitor.Id = 135
        Me.mnuConfigurarMonitor.Name = "mnuConfigurarMonitor"
        '
        'cmdImpresora
        '
        Me.cmdImpresora.Caption = "Impresora"
        Me.cmdImpresora.Id = 137
        Me.cmdImpresora.ImageOptions.Image = CType(resources.GetObject("cmdImpresora.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdImpresora.ImageOptions.LargeImage = CType(resources.GetObject("cmdImpresora.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdImpresora.Name = "cmdImpresora"
        '
        'cmdConexionBD
        '
        Me.cmdConexionBD.Caption = "Parámetros de Conexión"
        Me.cmdConexionBD.Id = 138
        Me.cmdConexionBD.ImageOptions.SvgImage = CType(resources.GetObject("cmdConexionBD.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdConexionBD.Name = "cmdConexionBD"
        '
        'lblServerAPP
        '
        Me.lblServerAPP.Caption = "Server:"
        Me.lblServerAPP.Id = 139
        Me.lblServerAPP.ImageOptions.Image = CType(resources.GetObject("lblServerAPP.ImageOptions.Image"), System.Drawing.Image)
        Me.lblServerAPP.Name = "lblServerAPP"
        Me.lblServerAPP.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'lblBDAPP
        '
        Me.lblBDAPP.Caption = "BD:"
        Me.lblBDAPP.Id = 140
        Me.lblBDAPP.ImageOptions.Image = CType(resources.GetObject("lblBDAPP.ImageOptions.Image"), System.Drawing.Image)
        Me.lblBDAPP.Name = "lblBDAPP"
        Me.lblBDAPP.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'cmdUbicacionPicking
        '
        Me.cmdUbicacionPicking.Caption = "Ubicación Picking"
        Me.cmdUbicacionPicking.Enabled = False
        Me.cmdUbicacionPicking.Id = 143
        Me.cmdUbicacionPicking.ImageOptions.Image = CType(resources.GetObject("cmdUbicacionPicking.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdUbicacionPicking.ImageOptions.LargeImage = CType(resources.GetObject("cmdUbicacionPicking.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdUbicacionPicking.Name = "cmdUbicacionPicking"
        Me.cmdUbicacionPicking.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'cmdTipoIngreso
        '
        Me.cmdTipoIngreso.Caption = "Tipo Ingreso"
        Me.cmdTipoIngreso.Id = 144
        Me.cmdTipoIngreso.Name = "cmdTipoIngreso"
        '
        'mnuOrdenCompra
        '
        Me.mnuOrdenCompra.Caption = "Documento de Ingreso"
        Me.mnuOrdenCompra.Id = 145
        Me.mnuOrdenCompra.ImageOptions.SvgImage = CType(resources.GetObject("mnuOrdenCompra.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuOrdenCompra.Name = "mnuOrdenCompra"
        '
        'mnuReglaUbicacion
        '
        Me.mnuReglaUbicacion.Caption = "Regla Ubicacion Filtro"
        Me.mnuReglaUbicacion.Id = 148
        Me.mnuReglaUbicacion.Name = "mnuReglaUbicacion"
        '
        'BarEditItem1
        '
        Me.BarEditItem1.Caption = "Ingresos"
        Me.BarEditItem1.Edit = Me.RepositoryItemBreadCrumbEdit1
        Me.BarEditItem1.Id = 151
        Me.BarEditItem1.Name = "BarEditItem1"
        '
        'RepositoryItemBreadCrumbEdit1
        '
        Me.RepositoryItemBreadCrumbEdit1.AutoHeight = False
        Me.RepositoryItemBreadCrumbEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemBreadCrumbEdit1.Name = "RepositoryItemBreadCrumbEdit1"
        '
        'BarButtonItem24
        '
        Me.BarButtonItem24.Caption = "BarButtonItem24"
        Me.BarButtonItem24.Id = 152
        Me.BarButtonItem24.Name = "BarButtonItem24"
        '
        'BarButtonItem25
        '
        Me.BarButtonItem25.Caption = "Regla Ubicacion Seleccion"
        Me.BarButtonItem25.Id = 154
        Me.BarButtonItem25.Name = "BarButtonItem25"
        '
        'BarButtonItem27
        '
        Me.BarButtonItem27.Caption = "BarButtonItem27"
        Me.BarButtonItem27.Id = 156
        Me.BarButtonItem27.Name = "BarButtonItem27"
        '
        'mnuConversion
        '
        Me.mnuConversion.Caption = "Conversión"
        Me.mnuConversion.Id = 157
        Me.mnuConversion.Name = "mnuConversion"
        '
        'mnuInterfaceNav
        '
        Me.mnuInterfaceNav.ActAsDropDown = True
        Me.mnuInterfaceNav.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.mnuInterfaceNav.Caption = "MI3 Sync"
        Me.mnuInterfaceNav.DropDownControl = Me.PopupMenu1
        Me.mnuInterfaceNav.Id = 161
        Me.mnuInterfaceNav.ImageOptions.SvgImage = CType(resources.GetObject("mnuInterfaceNav.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuInterfaceNav.Name = "mnuInterfaceNav"
        '
        'PopupMenu1
        '
        Me.PopupMenu1.Name = "PopupMenu1"
        Me.PopupMenu1.Ribbon = Me.rbMain
        '
        'lblEmpresa
        '
        Me.lblEmpresa.Caption = "Empresa:"
        Me.lblEmpresa.Id = 162
        Me.lblEmpresa.ImageOptions.Image = CType(resources.GetObject("lblEmpresa.ImageOptions.Image"), System.Drawing.Image)
        Me.lblEmpresa.ImageOptions.LargeImage = CType(resources.GetObject("lblEmpresa.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.lblEmpresa.Name = "lblEmpresa"
        '
        'lblBodega
        '
        Me.lblBodega.Caption = "Bodega:"
        Me.lblBodega.Id = 171
        Me.lblBodega.ImageOptions.Image = CType(resources.GetObject("lblBodega.ImageOptions.Image"), System.Drawing.Image)
        Me.lblBodega.ImageOptions.LargeImage = CType(resources.GetObject("lblBodega.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.lblBodega.Name = "lblBodega"
        Me.lblBodega.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'cmdMovimientos
        '
        Me.cmdMovimientos.Caption = "Movimientos"
        Me.cmdMovimientos.Id = 176
        Me.cmdMovimientos.ImageOptions.SvgImage = CType(resources.GetObject("cmdMovimientos.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdMovimientos.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdMovimiento), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdMovimientosCardex), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdMovimientosUbic), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdMovimientosDet), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdTrazaLote), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdMovCardexConDocs), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuKardexLote), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdMovporLote), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdMovimientosPoliza), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdMovimientosDoc), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuRptAjustesInventario), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdMovimientosEstado)})
        Me.cmdMovimientos.Name = "cmdMovimientos"
        '
        'cmdMovimiento
        '
        Me.cmdMovimiento.Caption = "1. Detalle de movimientos"
        Me.cmdMovimiento.Id = 177
        Me.cmdMovimiento.Name = "cmdMovimiento"
        '
        'cmdMovimientosCardex
        '
        Me.cmdMovimientosCardex.Caption = "2. Movimientos kárdex"
        Me.cmdMovimientosCardex.Id = 188
        Me.cmdMovimientosCardex.Name = "cmdMovimientosCardex"
        '
        'cmdMovimientosUbic
        '
        Me.cmdMovimientosUbic.Caption = "3. Cambios de ubicación"
        Me.cmdMovimientosUbic.Id = 197
        Me.cmdMovimientosUbic.Name = "cmdMovimientosUbic"
        '
        'cmdMovimientosDet
        '
        Me.cmdMovimientosDet.Caption = "4. Traza por documento de ingreso"
        Me.cmdMovimientosDet.Id = 178
        Me.cmdMovimientosDet.Name = "cmdMovimientosDet"
        '
        'cmdTrazaLote
        '
        Me.cmdTrazaLote.Caption = "5. Traza por lote"
        Me.cmdTrazaLote.Id = 204
        Me.cmdTrazaLote.Name = "cmdTrazaLote"
        '
        'cmdMovCardexConDocs
        '
        Me.cmdMovCardexConDocs.Caption = "6. Movimientos kárdex con referencia"
        Me.cmdMovCardexConDocs.Id = 215
        Me.cmdMovCardexConDocs.Name = "cmdMovCardexConDocs"
        '
        'mnuKardexLote
        '
        Me.mnuKardexLote.Caption = "7. Movimientos kárdex por lote"
        Me.mnuKardexLote.Id = 299
        Me.mnuKardexLote.Name = "mnuKardexLote"
        '
        'cmdMovporLote
        '
        Me.cmdMovporLote.Caption = "8. Movimientos por lote"
        Me.cmdMovporLote.Id = 303
        Me.cmdMovporLote.Name = "cmdMovporLote"
        '
        'cmdMovimientosPoliza
        '
        Me.cmdMovimientosPoliza.Caption = "9. Movimientos por Póliza"
        Me.cmdMovimientosPoliza.Id = 311
        Me.cmdMovimientosPoliza.Name = "cmdMovimientosPoliza"
        '
        'cmdMovimientosDoc
        '
        Me.cmdMovimientosDoc.Caption = "10. Movimientos por Documento"
        Me.cmdMovimientosDoc.Id = 322
        Me.cmdMovimientosDoc.Name = "cmdMovimientosDoc"
        '
        'mnuRptAjustesInventario
        '
        Me.mnuRptAjustesInventario.Caption = "11. Ajustes de Stock"
        Me.mnuRptAjustesInventario.Id = 357
        Me.mnuRptAjustesInventario.Name = "mnuRptAjustesInventario"
        '
        'cmdMovimientosEstado
        '
        Me.cmdMovimientosEstado.Caption = "12. Cambios de Estado"
        Me.cmdMovimientosEstado.Id = 362
        Me.cmdMovimientosEstado.Name = "cmdMovimientosEstado"
        '
        'mnuLicencia
        '
        Me.mnuLicencia.Caption = "Licencia"
        Me.mnuLicencia.Description = "Lic"
        Me.mnuLicencia.Id = 181
        Me.mnuLicencia.ImageOptions.Image = CType(resources.GetObject("mnuLicencia.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuLicencia.ImageOptions.LargeImage = CType(resources.GetObject("mnuLicencia.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuLicencia.Name = "mnuLicencia"
        Me.mnuLicencia.Tag = ""
        '
        'mnuLicencias
        '
        Me.mnuLicencias.Caption = "Licencia"
        Me.mnuLicencias.Id = 183
        Me.mnuLicencias.ImageOptions.SvgImage = CType(resources.GetObject("mnuLicencias.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuLicencias.Name = "mnuLicencias"
        '
        'BarButtonItem8
        '
        Me.BarButtonItem8.ActAsDropDown = True
        Me.BarButtonItem8.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.BarButtonItem8.Caption = "Ajuste de Inventario"
        Me.BarButtonItem8.DropDownControl = Me.GalleryDropDown1
        Me.BarButtonItem8.Id = 189
        Me.BarButtonItem8.ImageOptions.Image = CType(resources.GetObject("BarButtonItem8.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem8.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem8.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonItem8.Name = "BarButtonItem8"
        '
        'GalleryDropDown1
        '
        Me.GalleryDropDown1.Name = "GalleryDropDown1"
        Me.GalleryDropDown1.Ribbon = Me.rbMain
        '
        'mnuAjusteStock
        '
        Me.mnuAjusteStock.Caption = "Ajuste de Inventario"
        Me.mnuAjusteStock.Id = 191
        Me.mnuAjusteStock.ImageOptions.SvgImage = CType(resources.GetObject("mnuAjusteStock.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuAjusteStock.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdMotivoAjuste), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdTipoAjuste), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdAjusteInventario)})
        Me.mnuAjusteStock.Name = "mnuAjusteStock"
        '
        'cmdMotivoAjuste
        '
        Me.cmdMotivoAjuste.Caption = "Motivo Ajuste"
        Me.cmdMotivoAjuste.Id = 192
        Me.cmdMotivoAjuste.Name = "cmdMotivoAjuste"
        '
        'cmdTipoAjuste
        '
        Me.cmdTipoAjuste.Caption = "Tipo Ajuste"
        Me.cmdTipoAjuste.Id = 193
        Me.cmdTipoAjuste.Name = "cmdTipoAjuste"
        '
        'cmdAjusteInventario
        '
        Me.cmdAjusteInventario.Caption = "Ajuste Inventario"
        Me.cmdAjusteInventario.Id = 194
        Me.cmdAjusteInventario.Name = "cmdAjusteInventario"
        '
        'BarButtonItem10
        '
        Me.BarButtonItem10.Caption = "PRUEBA"
        Me.BarButtonItem10.Id = 195
        Me.BarButtonItem10.Name = "BarButtonItem10"
        '
        'cmdControl
        '
        Me.cmdControl.Caption = "Control"
        Me.cmdControl.Id = 198
        Me.cmdControl.ImageOptions.SvgImage = CType(resources.GetObject("cmdControl.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdControl.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdProximosVencer), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdMinMax), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdPendientesReq), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdStockTrans), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdRotacionProd), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdStockEnFecha), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuReporteDistribucionPorTramo), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdEstacionalidadProducto), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdAuditoriaPicking), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdReglasVencimiento), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuTiemposRecepcion), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuTiemposDespacho), New DevExpress.XtraBars.LinkPersistInfo(Me.mnurptTransaccionesOP)})
        Me.cmdControl.Name = "cmdControl"
        '
        'cmdProximosVencer
        '
        Me.cmdProximosVencer.Caption = "1. Productos proximos a vencer"
        Me.cmdProximosVencer.Id = 199
        Me.cmdProximosVencer.Name = "cmdProximosVencer"
        '
        'cmdMinMax
        '
        Me.cmdMinMax.Caption = "2. Mínimos y máximos"
        Me.cmdMinMax.Id = 200
        Me.cmdMinMax.Name = "cmdMinMax"
        '
        'cmdPendientesReq
        '
        Me.cmdPendientesReq.Caption = "3. Pendientes de requisición"
        Me.cmdPendientesReq.Id = 201
        Me.cmdPendientesReq.Name = "cmdPendientesReq"
        '
        'cmdStockTrans
        '
        Me.cmdStockTrans.Caption = "4. Stock en tránsito"
        Me.cmdStockTrans.Id = 202
        Me.cmdStockTrans.Name = "cmdStockTrans"
        '
        'cmdRotacionProd
        '
        Me.cmdRotacionProd.Caption = "5. Rotación de productos"
        Me.cmdRotacionProd.Id = 203
        Me.cmdRotacionProd.Name = "cmdRotacionProd"
        '
        'cmdStockEnFecha
        '
        Me.cmdStockEnFecha.Caption = "6. Stock en un Fecha"
        Me.cmdStockEnFecha.Id = 207
        Me.cmdStockEnFecha.Name = "cmdStockEnFecha"
        '
        'mnuReporteDistribucionPorTramo
        '
        Me.mnuReporteDistribucionPorTramo.Caption = "7. Distribución por tramo"
        Me.mnuReporteDistribucionPorTramo.Id = 240
        Me.mnuReporteDistribucionPorTramo.Name = "mnuReporteDistribucionPorTramo"
        '
        'cmdEstacionalidadProducto
        '
        Me.cmdEstacionalidadProducto.Caption = "8. Estacionalidad producto"
        Me.cmdEstacionalidadProducto.Id = 309
        Me.cmdEstacionalidadProducto.Name = "cmdEstacionalidadProducto"
        '
        'cmdAuditoriaPicking
        '
        Me.cmdAuditoriaPicking.Caption = "9. Auditoría de picking"
        Me.cmdAuditoriaPicking.Id = 313
        Me.cmdAuditoriaPicking.Name = "cmdAuditoriaPicking"
        '
        'cmdReglasVencimiento
        '
        Me.cmdReglasVencimiento.Caption = "10. Reglas de Vencimiento"
        Me.cmdReglasVencimiento.Id = 346
        Me.cmdReglasVencimiento.Name = "cmdReglasVencimiento"
        '
        'mnuTiemposRecepcion
        '
        Me.mnuTiemposRecepcion.Caption = "11. KPI Tiempos de Recepción"
        Me.mnuTiemposRecepcion.Id = 356
        Me.mnuTiemposRecepcion.Name = "mnuTiemposRecepcion"
        '
        'mnuTiemposDespacho
        '
        Me.mnuTiemposDespacho.Caption = "12. KPI Tiempos de Despacho"
        Me.mnuTiemposDespacho.Id = 358
        Me.mnuTiemposDespacho.Name = "mnuTiemposDespacho"
        '
        'mnurptTransaccionesOP
        '
        Me.mnurptTransaccionesOP.Caption = "13. Transacciones por Operador"
        Me.mnurptTransaccionesOP.Id = 369
        Me.mnurptTransaccionesOP.Name = "mnurptTransaccionesOP"
        '
        'mnuResetMenuLayOut
        '
        Me.mnuResetMenuLayOut.Caption = "Reset menú layout"
        Me.mnuResetMenuLayOut.Id = 206
        Me.mnuResetMenuLayOut.ImageOptions.SvgImage = CType(resources.GetObject("mnuResetMenuLayOut.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuResetMenuLayOut.Name = "mnuResetMenuLayOut"
        '
        'mnuTareasPreIngreso
        '
        Me.mnuTareasPreIngreso.Caption = "Tareas de pre-ingreso"
        Me.mnuTareasPreIngreso.Id = 208
        Me.mnuTareasPreIngreso.ImageOptions.Image = CType(resources.GetObject("mnuTareasPreIngreso.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuTareasPreIngreso.ImageOptions.LargeImage = CType(resources.GetObject("mnuTareasPreIngreso.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuTareasPreIngreso.Name = "mnuTareasPreIngreso"
        '
        'cmdStockRes
        '
        Me.cmdStockRes.Caption = "Stock reservado"
        Me.cmdStockRes.Id = 216
        Me.cmdStockRes.ImageOptions.SvgImage = CType(resources.GetObject("cmdStockRes.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdStockRes.Name = "cmdStockRes"
        '
        'mnuEstadoEnviosNAV
        '
        Me.mnuEstadoEnviosNAV.Caption = "Estado Envíos a ERP"
        Me.mnuEstadoEnviosNAV.Id = 218
        Me.mnuEstadoEnviosNAV.ImageOptions.SvgImage = CType(resources.GetObject("mnuEstadoEnviosNAV.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEstadoEnviosNAV.Name = "mnuEstadoEnviosNAV"
        '
        'mnuBackup
        '
        Me.mnuBackup.Caption = "Backup"
        Me.mnuBackup.Id = 220
        Me.mnuBackup.ImageOptions.SvgImage = CType(resources.GetObject("mnuBackup.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuBackup.Name = "mnuBackup"
        '
        'mnuCambioDeUsuario
        '
        Me.mnuCambioDeUsuario.Caption = "Cambio de Usuario"
        Me.mnuCambioDeUsuario.Id = 222
        Me.mnuCambioDeUsuario.ImageOptions.SvgImage = CType(resources.GetObject("mnuCambioDeUsuario.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuCambioDeUsuario.Name = "mnuCambioDeUsuario"
        '
        'cmdTransOut
        '
        Me.cmdTransOut.Caption = "Transacciones"
        Me.cmdTransOut.Id = 228
        Me.cmdTransOut.Name = "cmdTransOut"
        '
        'cmdBarrasPlt
        '
        Me.cmdBarrasPlt.Caption = "Barras Pallet"
        Me.cmdBarrasPlt.Id = 229
        Me.cmdBarrasPlt.Name = "cmdBarrasPlt"
        '
        'cmdMi3
        '
        Me.cmdMi3.Caption = "Datos MI3"
        Me.cmdMi3.Id = 230
        Me.cmdMi3.ImageOptions.SvgImage = CType(resources.GetObject("cmdMi3.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdMi3.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdTransaccionesOut), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuTransaccionesPendientesReenvio), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdBarrasPallet), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdLogInterface)})
        Me.cmdMi3.Name = "cmdMi3"
        '
        'cmdTransaccionesOut
        '
        Me.cmdTransaccionesOut.Caption = "Transacciones"
        Me.cmdTransaccionesOut.Id = 231
        Me.cmdTransaccionesOut.Name = "cmdTransaccionesOut"
        '
        'mnuTransaccionesPendientesReenvio
        '
        Me.mnuTransaccionesPendientesReenvio.Caption = "Transacciones pendientes reenvío"
        Me.mnuTransaccionesPendientesReenvio.Id = 361
        Me.mnuTransaccionesPendientesReenvio.Name = "mnuTransaccionesPendientesReenvio"
        '
        'cmdBarrasPallet
        '
        Me.cmdBarrasPallet.Caption = "Barras pallet"
        Me.cmdBarrasPallet.Id = 232
        Me.cmdBarrasPallet.Name = "cmdBarrasPallet"
        '
        'cmdLogInterface
        '
        Me.cmdLogInterface.Caption = "Log interface"
        Me.cmdLogInterface.Id = 235
        Me.cmdLogInterface.Name = "cmdLogInterface"
        '
        'cmdPrintSvr
        '
        Me.cmdPrintSvr.Caption = "Servicio impresión de barras"
        Me.cmdPrintSvr.Id = 234
        Me.cmdPrintSvr.ImageOptions.SvgImage = CType(resources.GetObject("cmdPrintSvr.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdPrintSvr.Name = "cmdPrintSvr"
        '
        'mnuDashBoardDesigner
        '
        Me.mnuDashBoardDesigner.Caption = "Diseñador de tableros"
        Me.mnuDashBoardDesigner.Id = 237
        Me.mnuDashBoardDesigner.ImageOptions.SvgImage = CType(resources.GetObject("mnuDashBoardDesigner.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuDashBoardDesigner.Name = "mnuDashBoardDesigner"
        '
        'mnuConfiguracionInt
        '
        Me.mnuConfiguracionInt.Caption = "Configuración Interface"
        Me.mnuConfiguracionInt.Id = 244
        Me.mnuConfiguracionInt.ImageOptions.SvgImage = CType(resources.GetObject("mnuConfiguracionInt.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuConfiguracionInt.Name = "mnuConfiguracionInt"
        '
        'mnuRepSalidaRapido
        '
        Me.mnuRepSalidaRapido.Caption = "Reporte de salidas"
        Me.mnuRepSalidaRapido.Id = 255
        Me.mnuRepSalidaRapido.ImageOptions.SvgImage = CType(resources.GetObject("mnuRepSalidaRapido.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuRepSalidaRapido.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuRepSalidasRapidoD), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdDocPeConDiferencias), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdSalidasDiasPiso), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdPackingDespachados)})
        Me.mnuRepSalidaRapido.Name = "mnuRepSalidaRapido"
        '
        'mnuRepSalidasRapidoD
        '
        Me.mnuRepSalidasRapidoD.Caption = "Salidas"
        Me.mnuRepSalidasRapidoD.Id = 257
        Me.mnuRepSalidasRapidoD.Name = "mnuRepSalidasRapidoD"
        '
        'cmdDocPeConDiferencias
        '
        Me.cmdDocPeConDiferencias.Caption = "Documentos con diferencias"
        Me.cmdDocPeConDiferencias.Id = 256
        Me.cmdDocPeConDiferencias.Name = "cmdDocPeConDiferencias"
        '
        'cmdSalidasDiasPiso
        '
        Me.cmdSalidasDiasPiso.Caption = "Salidas con días piso"
        Me.cmdSalidasDiasPiso.Id = 323
        Me.cmdSalidasDiasPiso.Name = "cmdSalidasDiasPiso"
        '
        'cmdPackingDespachados
        '
        Me.cmdPackingDespachados.Caption = "Packing Despachados"
        Me.cmdPackingDespachados.Id = 363
        Me.cmdPackingDespachados.Name = "cmdPackingDespachados"
        '
        'mnuRepFiscales
        '
        Me.mnuRepFiscales.Caption = "Reportes Fiscales"
        Me.mnuRepFiscales.Id = 260
        Me.mnuRepFiscales.ImageOptions.Image = CType(resources.GetObject("mnuRepFiscales.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuRepFiscales.ImageOptions.LargeImage = CType(resources.GetObject("mnuRepFiscales.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuRepFiscales.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdHistResGeneral), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdResumenCliente), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdCtas), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdMovimiento_Reporte), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdResValorizacion), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdResValorizacionMerca), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdAuditoriaRetroactivo), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdIngresoPoliza), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdMercaVencida)})
        Me.mnuRepFiscales.Name = "mnuRepFiscales"
        '
        'cmdHistResGeneral
        '
        Me.cmdHistResGeneral.Caption = "Historico resumen"
        Me.cmdHistResGeneral.Id = 263
        Me.cmdHistResGeneral.Name = "cmdHistResGeneral"
        '
        'cmdResumenCliente
        '
        Me.cmdResumenCliente.Caption = "Resumen por cliente"
        Me.cmdResumenCliente.Id = 266
        Me.cmdResumenCliente.Name = "cmdResumenCliente"
        '
        'cmdCtas
        '
        Me.cmdCtas.Caption = "Cuentas de Orden"
        Me.cmdCtas.Id = 269
        Me.cmdCtas.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdCtasOrden), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdCtaOrdenPoliza)})
        Me.cmdCtas.Name = "cmdCtas"
        '
        'cmdCtasOrden
        '
        Me.cmdCtasOrden.Caption = "Cuentas de Orden Detalle"
        Me.cmdCtasOrden.Id = 270
        Me.cmdCtasOrden.Name = "cmdCtasOrden"
        '
        'cmdCtaOrdenPoliza
        '
        Me.cmdCtaOrdenPoliza.Caption = "Cuentas de Orden Poliza"
        Me.cmdCtaOrdenPoliza.Id = 271
        Me.cmdCtaOrdenPoliza.Name = "cmdCtaOrdenPoliza"
        '
        'cmdMovimiento_Reporte
        '
        Me.cmdMovimiento_Reporte.Caption = "Inventario para Retroactivo"
        Me.cmdMovimiento_Reporte.Id = 273
        Me.cmdMovimiento_Reporte.Name = "cmdMovimiento_Reporte"
        '
        'cmdResValorizacion
        '
        Me.cmdResValorizacion.Caption = "Valorizacion resumen"
        Me.cmdResValorizacion.Id = 302
        Me.cmdResValorizacion.Name = "cmdResValorizacion"
        '
        'cmdResValorizacionMerca
        '
        Me.cmdResValorizacionMerca.Caption = "Valorizacion por mercaderia"
        Me.cmdResValorizacionMerca.Id = 304
        Me.cmdResValorizacionMerca.Name = "cmdResValorizacionMerca"
        '
        'cmdAuditoriaRetroactivo
        '
        Me.cmdAuditoriaRetroactivo.Caption = "Movimientos de Retroactivo"
        Me.cmdAuditoriaRetroactivo.Id = 325
        Me.cmdAuditoriaRetroactivo.Name = "cmdAuditoriaRetroactivo"
        '
        'cmdIngresoPoliza
        '
        Me.cmdIngresoPoliza.Caption = "Ingresos con póliza"
        Me.cmdIngresoPoliza.Id = 328
        Me.cmdIngresoPoliza.Name = "cmdIngresoPoliza"
        '
        'cmdMercaVencida
        '
        Me.cmdMercaVencida.Caption = "Mercaderia vencida"
        Me.cmdMercaVencida.Id = 355
        Me.cmdMercaVencida.Name = "cmdMercaVencida"
        '
        'cmdCtaOrden
        '
        Me.cmdCtaOrden.Caption = "Cuentas de orden"
        Me.cmdCtaOrden.Id = 261
        Me.cmdCtaOrden.Name = "cmdCtaOrden"
        '
        'BarSubItem2
        '
        Me.BarSubItem2.Caption = "Fiscales"
        Me.BarSubItem2.Id = 262
        Me.BarSubItem2.Name = "BarSubItem2"
        '
        'cmdHistResFiscal
        '
        Me.cmdHistResFiscal.Caption = "Historico resumen fiscal"
        Me.cmdHistResFiscal.Id = 264
        Me.cmdHistResFiscal.Name = "cmdHistResFiscal"
        '
        'mnuRepFiscal
        '
        Me.mnuRepFiscal.Caption = "Fiscales"
        Me.mnuRepFiscal.Id = 267
        Me.mnuRepFiscal.ImageOptions.Image = CType(resources.GetObject("mnuRepFiscal.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuRepFiscal.ImageOptions.LargeImage = CType(resources.GetObject("mnuRepFiscal.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuRepFiscal.Name = "mnuRepFiscal"
        '
        'bbiCambiaBodega
        '
        Me.bbiCambiaBodega.ActAsDropDown = True
        Me.bbiCambiaBodega.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.bbiCambiaBodega.Caption = "Cambia Bodega"
        Me.bbiCambiaBodega.DropDownControl = Me.pmBodegas
        Me.bbiCambiaBodega.Hint = "Cambia Bodega"
        Me.bbiCambiaBodega.Id = 274
        Me.bbiCambiaBodega.ImageOptions.SvgImage = CType(resources.GetObject("bbiCambiaBodega.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.bbiCambiaBodega.Name = "bbiCambiaBodega"
        '
        'pmBodegas
        '
        Me.pmBodegas.Name = "pmBodegas"
        Me.pmBodegas.Ribbon = Me.rbMain
        '
        'sddiSkinWMS
        '
        Me.sddiSkinWMS.Id = 275
        Me.sddiSkinWMS.Name = "sddiSkinWMS"
        '
        'cmdServicios
        '
        Me.cmdServicios.Caption = "Servicios"
        Me.cmdServicios.Id = 284
        Me.cmdServicios.Name = "cmdServicios"
        '
        'mnuReporteServicios
        '
        Me.mnuReporteServicios.Caption = "Reportes de Servicios"
        Me.mnuReporteServicios.Id = 285
        Me.mnuReporteServicios.ImageOptions.SvgImage = CType(resources.GetObject("mnuReporteServicios.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuReporteServicios.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnServicios)})
        Me.mnuReporteServicios.Name = "mnuReporteServicios"
        '
        'btnServicios
        '
        Me.btnServicios.Caption = "Servicios"
        Me.btnServicios.Id = 286
        Me.btnServicios.Name = "btnServicios"
        '
        'mnuReportesGallery
        '
        Me.mnuReportesGallery.Caption = "Galería de reportes"
        Me.mnuReportesGallery.Id = 287
        Me.mnuReportesGallery.ImageOptions.SvgImage = CType(resources.GetObject("mnuReportesGallery.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuReportesGallery.Name = "mnuReportesGallery"
        '
        'mnuIndicadores
        '
        Me.mnuIndicadores.Caption = "Indicadores"
        Me.mnuIndicadores.Id = 290
        Me.mnuIndicadores.ImageOptions.SvgImage = CType(resources.GetObject("mnuIndicadores.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuIndicadores.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuAnalitica1), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuAnalitica2), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuAnalitica3), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuKPIResumen)})
        Me.mnuIndicadores.Name = "mnuIndicadores"
        '
        'mnuAnalitica1
        '
        Me.mnuAnalitica1.Caption = "Salidas"
        Me.mnuAnalitica1.Id = 291
        Me.mnuAnalitica1.Name = "mnuAnalitica1"
        '
        'mnuAnalitica2
        '
        Me.mnuAnalitica2.Caption = "Ingresos"
        Me.mnuAnalitica2.Id = 306
        Me.mnuAnalitica2.Name = "mnuAnalitica2"
        '
        'mnuAnalitica3
        '
        Me.mnuAnalitica3.Caption = "Productos"
        Me.mnuAnalitica3.Id = 307
        Me.mnuAnalitica3.Name = "mnuAnalitica3"
        '
        'mnuKPIResumen
        '
        Me.mnuKPIResumen.Caption = "Resumen"
        Me.mnuKPIResumen.Id = 359
        Me.mnuKPIResumen.Name = "mnuKPIResumen"
        '
        'mnuRegistroServicios
        '
        Me.mnuRegistroServicios.Caption = "Registro de servicios"
        Me.mnuRegistroServicios.Id = 292
        Me.mnuRegistroServicios.ImageOptions.SvgImage = CType(resources.GetObject("mnuRegistroServicios.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuRegistroServicios.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuServiciosIngreso), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuServiciosSalidas)})
        Me.mnuRegistroServicios.Name = "mnuRegistroServicios"
        '
        'mnuServiciosIngreso
        '
        Me.mnuServiciosIngreso.Caption = "Ingresos"
        Me.mnuServiciosIngreso.Id = 293
        Me.mnuServiciosIngreso.Name = "mnuServiciosIngreso"
        '
        'mnuServiciosSalidas
        '
        Me.mnuServiciosSalidas.Caption = "Salidas"
        Me.mnuServiciosSalidas.Id = 294
        Me.mnuServiciosSalidas.Name = "mnuServiciosSalidas"
        '
        'cmdStockResJornada
        '
        Me.cmdStockResJornada.Caption = "Resumen Valorizacion"
        Me.cmdStockResJornada.Id = 301
        Me.cmdStockResJornada.Name = "cmdStockResJornada"
        '
        'btInvInicial
        '
        Me.btInvInicial.Caption = "Importar inv Inicial"
        Me.btInvInicial.Id = 305
        Me.btInvInicial.ImageOptions.SvgImage = CType(resources.GetObject("btInvInicial.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btInvInicial.Name = "btInvInicial"
        '
        'BarButtonItem15
        '
        Me.BarButtonItem15.Caption = "Parámetros"
        Me.BarButtonItem15.Id = 318
        Me.BarButtonItem15.Name = "BarButtonItem15"
        '
        'mnuLogErrorWMS
        '
        Me.mnuLogErrorWMS.Caption = "Log WMS"
        Me.mnuLogErrorWMS.Id = 324
        Me.mnuLogErrorWMS.ImageOptions.SvgImage = CType(resources.GetObject("mnuLogErrorWMS.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuLogErrorWMS.Name = "mnuLogErrorWMS"
        '
        'mnuAnalitica
        '
        Me.mnuAnalitica.Caption = "Análitica"
        Me.mnuAnalitica.Id = 327
        Me.mnuAnalitica.ImageOptions.SvgImage = CType(resources.GetObject("mnuAnalitica.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuAnalitica.Name = "mnuAnalitica"
        '
        'mnuQAEscenariosReserva
        '
        Me.mnuQAEscenariosReserva.Caption = "Reservas"
        Me.mnuQAEscenariosReserva.Id = 329
        Me.mnuQAEscenariosReserva.ImageOptions.SvgImage = CType(resources.GetObject("mnuQAEscenariosReserva.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuQAEscenariosReserva.Name = "mnuQAEscenariosReserva"
        '
        'mnuCaso1ReservaIdealsa
        '
        Me.mnuCaso1ReservaIdealsa.Caption = "CASO#1 - IDEAL_20231002011101:"
        Me.mnuCaso1ReservaIdealsa.Id = 330
        Me.mnuCaso1ReservaIdealsa.Name = "mnuCaso1ReservaIdealsa"
        '
        'mnuQAReservas
        '
        Me.mnuQAReservas.Caption = "QA"
        Me.mnuQAReservas.Id = 331
        Me.mnuQAReservas.ImageOptions.SvgImage = CType(resources.GetObject("mnuQAReservas.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuQAReservas.Name = "mnuQAReservas"
        '
        'lblModoDebug
        '
        Me.lblModoDebug.Caption = "Modo Debug = ON"
        Me.lblModoDebug.Id = 332
        Me.lblModoDebug.ImageOptions.SvgImage = CType(resources.GetObject("lblModoDebug.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.lblModoDebug.Name = "lblModoDebug"
        Me.lblModoDebug.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'mnuReportesSAT
        '
        Me.mnuReportesSAT.Caption = "SAT"
        Me.mnuReportesSAT.Id = 333
        Me.mnuReportesSAT.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.[False]
        Me.mnuReportesSAT.ImageOptions.AllowStubGlyph = DevExpress.Utils.DefaultBoolean.[False]
        Me.mnuReportesSAT.ImageOptions.SvgImage = CType(resources.GetObject("mnuReportesSAT.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuReportesSAT.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuRptIngresosSAT), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuRptSalidasSAT), New DevExpress.XtraBars.LinkPersistInfo(Me.mnurptExistenciasSAT)})
        Me.mnuReportesSAT.Name = "mnuReportesSAT"
        '
        'mnuRptIngresosSAT
        '
        Me.mnuRptIngresosSAT.Caption = "Ingresos"
        Me.mnuRptIngresosSAT.Id = 334
        Me.mnuRptIngresosSAT.Name = "mnuRptIngresosSAT"
        '
        'mnuRptSalidasSAT
        '
        Me.mnuRptSalidasSAT.Caption = "Egresos"
        Me.mnuRptSalidasSAT.Id = 335
        Me.mnuRptSalidasSAT.Name = "mnuRptSalidasSAT"
        '
        'mnurptExistenciasSAT
        '
        Me.mnurptExistenciasSAT.Caption = "Existencias"
        Me.mnurptExistenciasSAT.Id = 336
        Me.mnurptExistenciasSAT.Name = "mnurptExistenciasSAT"
        '
        'mnuActualizarBD
        '
        Me.mnuActualizarBD.Caption = "Actualizar versión"
        Me.mnuActualizarBD.Id = 337
        Me.mnuActualizarBD.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizarBD.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizarBD.Name = "mnuActualizarBD"
        '
        'mnuActualizarIndices
        '
        Me.mnuActualizarIndices.Caption = "Regenerar Índices"
        Me.mnuActualizarIndices.Id = 338
        Me.mnuActualizarIndices.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizarIndices.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizarIndices.Name = "mnuActualizarIndices"
        '
        'mnuGestionInventarioCalidad
        '
        Me.mnuGestionInventarioCalidad.Caption = "Gestión de inventario"
        Me.mnuGestionInventarioCalidad.Id = 340
        Me.mnuGestionInventarioCalidad.ImageOptions.SvgImage = CType(resources.GetObject("mnuGestionInventarioCalidad.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGestionInventarioCalidad.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuHabilitacionLotes)})
        Me.mnuGestionInventarioCalidad.Name = "mnuGestionInventarioCalidad"
        '
        'mnuHabilitacionLotes
        '
        Me.mnuHabilitacionLotes.Caption = "Lotes"
        Me.mnuHabilitacionLotes.Id = 341
        Me.mnuHabilitacionLotes.Name = "mnuHabilitacionLotes"
        '
        'mnuReportesControlCalidad
        '
        Me.mnuReportesControlCalidad.Caption = "Reportes"
        Me.mnuReportesControlCalidad.Id = 342
        Me.mnuReportesControlCalidad.ImageOptions.SvgImage = CType(resources.GetObject("mnuReportesControlCalidad.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuReportesControlCalidad.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdMovimientosControlCalidad)})
        Me.mnuReportesControlCalidad.Name = "mnuReportesControlCalidad"
        '
        'cmdMovimientosControlCalidad
        '
        Me.cmdMovimientosControlCalidad.Caption = "Movimientos"
        Me.cmdMovimientosControlCalidad.Id = 343
        Me.cmdMovimientosControlCalidad.Name = "cmdMovimientosControlCalidad"
        '
        'mnuTamañoTablas
        '
        Me.mnuTamañoTablas.Caption = "Salud"
        Me.mnuTamañoTablas.Id = 344
        Me.mnuTamañoTablas.ImageOptions.SvgImage = CType(resources.GetObject("mnuTamañoTablas.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuTamañoTablas.Name = "mnuTamañoTablas"
        '
        'lblWSHHURL
        '
        Me.lblWSHHURL.Caption = "wsHH"
        Me.lblWSHHURL.Id = 345
        Me.lblWSHHURL.ImageOptions.SvgImage = CType(resources.GetObject("lblWSHHURL.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.lblWSHHURL.Name = "lblWSHHURL"
        Me.lblWSHHURL.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'mnuManufactura
        '
        Me.mnuManufactura.Caption = "Manufactura"
        Me.mnuManufactura.Id = 348
        Me.mnuManufactura.ImageOptions.SvgImage = CType(resources.GetObject("mnuManufactura.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuManufactura.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdTransaccionesManufactura), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdTipo)})
        Me.mnuManufactura.Name = "mnuManufactura"
        '
        'cmdTransaccionesManufactura
        '
        Me.cmdTransaccionesManufactura.Caption = "Transacciones Manufactura"
        Me.cmdTransaccionesManufactura.Id = 349
        Me.cmdTransaccionesManufactura.Name = "cmdTransaccionesManufactura"
        '
        'cmdTipo
        '
        Me.cmdTipo.Caption = "Tipo Manufactura"
        Me.cmdTipo.Id = 350
        Me.cmdTipo.Name = "cmdTipo"
        '
        'mnuPreFacturacion
        '
        Me.mnuPreFacturacion.Caption = "PreFacturacion"
        Me.mnuPreFacturacion.Id = 352
        Me.mnuPreFacturacion.ImageOptions.SvgImage = CType(resources.GetObject("mnuPreFacturacion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuPreFacturacion.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdPreFacturar), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdAcuerdosyServicios)})
        Me.mnuPreFacturacion.Name = "mnuPreFacturacion"
        '
        'cmdPreFacturar
        '
        Me.cmdPreFacturar.Caption = "PreFacturas"
        Me.cmdPreFacturar.Id = 353
        Me.cmdPreFacturar.Name = "cmdPreFacturar"
        '
        'cmdAcuerdosyServicios
        '
        Me.cmdAcuerdosyServicios.Caption = "Acuerdos y Servicios"
        Me.cmdAcuerdosyServicios.Id = 354
        Me.cmdAcuerdosyServicios.Name = "cmdAcuerdosyServicios"
        '
        'mnuVisualizarTableroWMS
        '
        Me.mnuVisualizarTableroWMS.Caption = "Visualizador de tableros"
        Me.mnuVisualizarTableroWMS.Id = 360
        Me.mnuVisualizarTableroWMS.Name = "mnuVisualizarTableroWMS"
        '
        'mnuProductividad
        '
        Me.mnuProductividad.Caption = "Productividad"
        Me.mnuProductividad.Id = 367
        Me.mnuProductividad.ImageOptions.SvgImage = CType(resources.GetObject("mnuProductividad.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuProductividad.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuProductividadPicking)})
        Me.mnuProductividad.Name = "mnuProductividad"
        '
        'mnuProductividadPicking
        '
        Me.mnuProductividadPicking.Caption = "Picking"
        Me.mnuProductividadPicking.Id = 368
        Me.mnuProductividadPicking.Name = "mnuProductividadPicking"
        '
        'mnuInterfaceDMS
        '
        Me.mnuInterfaceDMS.Caption = "DMS"
        Me.mnuInterfaceDMS.Id = 371
        Me.mnuInterfaceDMS.ImageOptions.SvgImage = CType(resources.GetObject("mnuInterfaceDMS.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuInterfaceDMS.Name = "mnuInterfaceDMS"
        '
        'mnuVerificacionBOF
        '
        Me.mnuVerificacionBOF.Caption = "Verificacion BOF"
        Me.mnuVerificacionBOF.Hint = "Verificación por BOF"
        Me.mnuVerificacionBOF.Id = 372
        Me.mnuVerificacionBOF.ImageOptions.SvgImage = CType(resources.GetObject("mnuVerificacionBOF.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuVerificacionBOF.Name = "mnuVerificacionBOF"
        '
        'cmdOcupacionArea
        '
        Me.cmdOcupacionArea.Caption = "25. Ocupación por área"
        Me.cmdOcupacionArea.Id = 373
        Me.cmdOcupacionArea.Name = "cmdOcupacionArea"
        '
        'cmdIA
        '
        Me.cmdIA.Id = 379
        Me.cmdIA.Name = "cmdIA"
        '
        'mnuStockTag
        '
        Me.mnuStockTag.Caption = "Existencias"
        Me.mnuStockTag.Id = 380
        Me.mnuStockTag.ImageOptions.SvgImage = CType(resources.GetObject("mnuStockTag.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuStockTag.Name = "mnuStockTag"
        '
        'rpCatalogos
        '
        Me.rpCatalogos.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.pgEmpresa, Me.pgBodega, Me.pgProductos, Me.pgLogistica, Me.pgClientes, Me.PgPropietario2, Me.pgProveedores, Me.pgSeguridad, Me.RpgCalendario, Me.rbPageMonitor, Me.rbPageQA})
        Me.rpCatalogos.Name = "rpCatalogos"
        Me.rpCatalogos.Text = "Catálogos"
        '
        'pgEmpresa
        '
        Me.pgEmpresa.ItemLinks.Add(Me.mnuEmpresa)
        Me.pgEmpresa.Name = "pgEmpresa"
        '
        'pgBodega
        '
        Me.pgBodega.ItemLinks.Add(Me.mnuBodega)
        Me.pgBodega.Name = "pgBodega"
        '
        'pgProductos
        '
        Me.pgProductos.ItemLinks.Add(Me.mnuProductos)
        Me.pgProductos.Name = "pgProductos"
        '
        'pgLogistica
        '
        Me.pgLogistica.ItemLinks.Add(Me.mnuLogistica)
        Me.pgLogistica.Name = "pgLogistica"
        '
        'pgClientes
        '
        Me.pgClientes.ItemLinks.Add(Me.mnuClientes)
        Me.pgClientes.Name = "pgClientes"
        '
        'PgPropietario2
        '
        Me.PgPropietario2.ItemLinks.Add(Me.mnuPropietarios)
        Me.PgPropietario2.Name = "PgPropietario2"
        '
        'pgProveedores
        '
        Me.pgProveedores.ItemLinks.Add(Me.cmdProveedores)
        Me.pgProveedores.Name = "pgProveedores"
        '
        'pgSeguridad
        '
        Me.pgSeguridad.ItemLinks.Add(Me.mnuSeguridad)
        Me.pgSeguridad.Name = "pgSeguridad"
        '
        'RpgCalendario
        '
        Me.RpgCalendario.ItemLinks.Add(Me.cmdCalendario)
        Me.RpgCalendario.Name = "RpgCalendario"
        '
        'rbPageMonitor
        '
        Me.rbPageMonitor.ItemLinks.Add(Me.mnuMonitor)
        Me.rbPageMonitor.Name = "rbPageMonitor"
        '
        'rbPageQA
        '
        Me.rbPageQA.ItemLinks.Add(Me.mnuQAReservas)
        Me.rbPageQA.Name = "rbPageQA"
        '
        'rpIngresos
        '
        Me.rpIngresos.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.rpgOdenCompra, Me.pgTareaIngreso, Me.pgTareasPreIngreso})
        Me.rpIngresos.Name = "rpIngresos"
        Me.rpIngresos.Text = "Ingresos"
        '
        'rpgOdenCompra
        '
        Me.rpgOdenCompra.ItemLinks.Add(Me.mnuOrdenCompra)
        Me.rpgOdenCompra.Name = "rpgOdenCompra"
        '
        'pgTareaIngreso
        '
        Me.pgTareaIngreso.ItemLinks.Add(Me.mnuRecepcion)
        Me.pgTareaIngreso.Name = "pgTareaIngreso"
        '
        'pgTareasPreIngreso
        '
        Me.pgTareasPreIngreso.ItemLinks.Add(Me.mnuTareasPreIngreso)
        Me.pgTareasPreIngreso.Name = "pgTareasPreIngreso"
        '
        'rpSalidas
        '
        Me.rpSalidas.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.pgPedidoVenta, Me.pgPicking, Me.pgDespacho, Me.grpStockRes, Me.pgCotizar, Me.pgVerificacionBOF})
        Me.rpSalidas.Name = "rpSalidas"
        Me.rpSalidas.Text = "Salidas"
        Me.rpSalidas.Visible = False
        '
        'pgPedidoVenta
        '
        Me.pgPedidoVenta.ItemLinks.Add(Me.mnuPedidoVenta)
        Me.pgPedidoVenta.Name = "pgPedidoVenta"
        '
        'pgPicking
        '
        Me.pgPicking.ItemLinks.Add(Me.mnuPicking)
        Me.pgPicking.Name = "pgPicking"
        '
        'pgDespacho
        '
        Me.pgDespacho.ItemLinks.Add(Me.mnuDespachos)
        Me.pgDespacho.Name = "pgDespacho"
        '
        'grpStockRes
        '
        Me.grpStockRes.ItemLinks.Add(Me.cmdStockRes)
        Me.grpStockRes.Name = "grpStockRes"
        '
        'pgCotizar
        '
        Me.pgCotizar.ItemLinks.Add(Me.mnuCotizar)
        Me.pgCotizar.Name = "pgCotizar"
        Me.pgCotizar.Visible = False
        '
        'pgVerificacionBOF
        '
        Me.pgVerificacionBOF.ItemLinks.Add(Me.mnuVerificacionBOF)
        Me.pgVerificacionBOF.Name = "pgVerificacionBOF"
        Me.pgVerificacionBOF.Visible = False
        '
        'rpServicios
        '
        Me.rpServicios.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.pgRegistroServicios, Me.pgReporteServicios, Me.pgPreFacturacion})
        Me.rpServicios.Name = "rpServicios"
        Me.rpServicios.Text = "Servicios"
        '
        'pgRegistroServicios
        '
        Me.pgRegistroServicios.ItemLinks.Add(Me.mnuRegistroServicios)
        Me.pgRegistroServicios.Name = "pgRegistroServicios"
        '
        'pgReporteServicios
        '
        Me.pgReporteServicios.ItemLinks.Add(Me.mnuReporteServicios)
        Me.pgReporteServicios.Name = "pgReporteServicios"
        '
        'pgPreFacturacion
        '
        Me.pgPreFacturacion.ItemLinks.Add(Me.mnuPreFacturacion)
        Me.pgPreFacturacion.Name = "pgPreFacturacion"
        '
        'rpInventarios
        '
        Me.rpInventarios.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.pgInventarios})
        Me.rpInventarios.Name = "rpInventarios"
        Me.rpInventarios.Text = "Inventarios"
        '
        'pgInventarios
        '
        Me.pgInventarios.ItemLinks.Add(Me.mnuInventarios)
        Me.pgInventarios.ItemLinks.Add(Me.btInvInicial)
        Me.pgInventarios.Name = "pgInventarios"
        '
        'rpMovimientos
        '
        Me.rpMovimientos.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.pgCambioUbicacion, Me.pgCambioEstado, Me.pgTraslados, Me.pgAjusteInventario})
        Me.rpMovimientos.Name = "rpMovimientos"
        Me.rpMovimientos.Text = "Movimientos"
        '
        'pgCambioUbicacion
        '
        Me.pgCambioUbicacion.ItemLinks.Add(Me.mnuCambioUbicacion)
        Me.pgCambioUbicacion.Name = "pgCambioUbicacion"
        '
        'pgCambioEstado
        '
        Me.pgCambioEstado.ItemLinks.Add(Me.mnuCambioEstado)
        Me.pgCambioEstado.Name = "pgCambioEstado"
        '
        'pgTraslados
        '
        Me.pgTraslados.ItemLinks.Add(Me.mnuTraslados)
        Me.pgTraslados.Name = "pgTraslados"
        Me.pgTraslados.Visible = False
        '
        'pgAjusteInventario
        '
        Me.pgAjusteInventario.ItemLinks.Add(Me.mnuAjusteStock)
        Me.pgAjusteInventario.Name = "pgAjusteInventario"
        '
        'rpReportes
        '
        Me.rpReportes.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.pgReportes, Me.pgReporteFiscal, Me.pgReportesIngresoRapido, Me.pgSalidasRep, Me.grpRepTablero, Me.pgReportesGallery, Me.pgIndicadores, Me.pgLogWMS, Me.rpgSat, Me.pgProductividad})
        Me.rpReportes.Name = "rpReportes"
        Me.rpReportes.Text = "Reportes"
        '
        'pgReportes
        '
        Me.pgReportes.ItemLinks.Add(Me.cmdDetalleExistencia)
        Me.pgReportes.ItemLinks.Add(Me.cmdControl)
        Me.pgReportes.ItemLinks.Add(Me.cmdMovimientos)
        Me.pgReportes.ItemLinks.Add(Me.cmdUbicacionPicking)
        Me.pgReportes.ItemLinks.Add(Me.mnuEstadoEnviosNAV)
        Me.pgReportes.Name = "pgReportes"
        Me.pgReportes.Text = "Existencias"
        '
        'pgReporteFiscal
        '
        Me.pgReporteFiscal.ItemLinks.Add(Me.mnuRepFiscales)
        Me.pgReporteFiscal.Name = "pgReporteFiscal"
        '
        'pgReportesIngresoRapido
        '
        Me.pgReportesIngresoRapido.ItemLinks.Add(Me.mnuRepIngresoRapido)
        Me.pgReportesIngresoRapido.Name = "pgReportesIngresoRapido"
        '
        'pgSalidasRep
        '
        Me.pgSalidasRep.ItemLinks.Add(Me.mnuRepSalidaRapido)
        Me.pgSalidasRep.Name = "pgSalidasRep"
        '
        'grpRepTablero
        '
        Me.grpRepTablero.ItemLinks.Add(Me.mnuDashBoardDesigner)
        Me.grpRepTablero.ItemLinks.Add(Me.mnuVisualizarTableroWMS)
        Me.grpRepTablero.Name = "grpRepTablero"
        '
        'pgReportesGallery
        '
        Me.pgReportesGallery.ItemLinks.Add(Me.mnuReportesGallery)
        Me.pgReportesGallery.Name = "pgReportesGallery"
        '
        'pgIndicadores
        '
        Me.pgIndicadores.ItemLinks.Add(Me.mnuIndicadores)
        Me.pgIndicadores.Name = "pgIndicadores"
        '
        'pgLogWMS
        '
        Me.pgLogWMS.ItemLinks.Add(Me.mnuAnalitica)
        Me.pgLogWMS.ItemLinks.Add(Me.mnuLogErrorWMS)
        Me.pgLogWMS.Name = "pgLogWMS"
        '
        'rpgSat
        '
        Me.rpgSat.ItemLinks.Add(Me.mnuReportesSAT)
        Me.rpgSat.Name = "rpgSat"
        '
        'pgProductividad
        '
        Me.pgProductividad.ItemLinks.Add(Me.mnuProductividad)
        Me.pgProductividad.Name = "pgProductividad"
        '
        'rpAdministrador
        '
        Me.rpAdministrador.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.pgParametrosCn, Me.rpgInterface, Me.rpgLicencia, Me.pgResetMenu, Me.pgReporteTransacciones, Me.pgBackup, Me.rpgCambioDeUsuario, Me.rpgServicioImpresiones, Me.rpgActualizacionesBD})
        Me.rpAdministrador.Name = "rpAdministrador"
        Me.rpAdministrador.Text = "Administrador"
        '
        'pgParametrosCn
        '
        Me.pgParametrosCn.ItemLinks.Add(Me.cmdConexionBD)
        Me.pgParametrosCn.Name = "pgParametrosCn"
        '
        'rpgInterface
        '
        Me.rpgInterface.ItemLinks.Add(Me.mnuInterfaceNav)
        Me.rpgInterface.ItemLinks.Add(Me.mnuConfiguracionInt)
        Me.rpgInterface.ItemLinks.Add(Me.mnuInterfaceDMS)
        Me.rpgInterface.Name = "rpgInterface"
        '
        'rpgLicencia
        '
        Me.rpgLicencia.ItemLinks.Add(Me.mnuLicencias)
        Me.rpgLicencia.Name = "rpgLicencia"
        '
        'pgResetMenu
        '
        Me.pgResetMenu.ItemLinks.Add(Me.mnuResetMenuLayOut)
        Me.pgResetMenu.Name = "pgResetMenu"
        '
        'pgReporteTransacciones
        '
        Me.pgReporteTransacciones.ItemLinks.Add(Me.cmdMi3)
        Me.pgReporteTransacciones.Name = "pgReporteTransacciones"
        '
        'pgBackup
        '
        Me.pgBackup.ItemLinks.Add(Me.mnuBackup)
        Me.pgBackup.ItemLinks.Add(Me.cmdIA)
        Me.pgBackup.Name = "pgBackup"
        '
        'rpgCambioDeUsuario
        '
        Me.rpgCambioDeUsuario.ItemLinks.Add(Me.mnuCambioDeUsuario)
        Me.rpgCambioDeUsuario.Name = "rpgCambioDeUsuario"
        '
        'rpgServicioImpresiones
        '
        Me.rpgServicioImpresiones.ItemLinks.Add(Me.cmdPrintSvr)
        Me.rpgServicioImpresiones.Name = "rpgServicioImpresiones"
        '
        'rpgActualizacionesBD
        '
        Me.rpgActualizacionesBD.ItemLinks.Add(Me.mnuActualizarBD)
        Me.rpgActualizacionesBD.ItemLinks.Add(Me.mnuActualizarIndices)
        Me.rpgActualizacionesBD.ItemLinks.Add(Me.mnuTamañoTablas)
        Me.rpgActualizacionesBD.Name = "rpgActualizacionesBD"
        Me.rpgActualizacionesBD.Text = "Mantenimiento base de datos"
        '
        'rpControlCalidad
        '
        Me.rpControlCalidad.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.rpGestionInventario})
        Me.rpControlCalidad.Name = "rpControlCalidad"
        Me.rpControlCalidad.Text = "Control de calidad"
        '
        'rpGestionInventario
        '
        Me.rpGestionInventario.ItemLinks.Add(Me.mnuGestionInventarioCalidad)
        Me.rpGestionInventario.ItemLinks.Add(Me.mnuReportesControlCalidad)
        Me.rpGestionInventario.Name = "rpGestionInventario"
        '
        'rpManufacturaLigera
        '
        Me.rpManufacturaLigera.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.rpMLTareas})
        Me.rpManufacturaLigera.Name = "rpManufacturaLigera"
        Me.rpManufacturaLigera.Text = "Manufactura Ligera"
        '
        'rpMLTareas
        '
        Me.rpMLTareas.ItemLinks.Add(Me.mnuManufactura)
        Me.rpMLTareas.Name = "rpMLTareas"
        '
        'rpRFID
        '
        Me.rpRFID.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.rpgRFID})
        Me.rpRFID.Name = "rpRFID"
        Me.rpRFID.Text = "RFID"
        '
        'rpgRFID
        '
        Me.rpgRFID.ItemLinks.Add(Me.mnuImpresionBarraPallet)
        Me.rpgRFID.ItemLinks.Add(Me.mnuListaIngresoTag)
        Me.rpgRFID.ItemLinks.Add(Me.mnuListaSalidaTag)
        Me.rpgRFID.ItemLinks.Add(Me.mnuStockTag)
        Me.rpgRFID.ItemLinks.Add(Me.BarButtonItem16)
        Me.rpgRFID.Name = "rpgRFID"
        '
        'mnuImpresionBarraPallet
        '
        Me.mnuImpresionBarraPallet.Caption = "Impresión de Tags"
        Me.mnuImpresionBarraPallet.ImageOptions.SvgImage = CType(resources.GetObject("mnuImpresionBarraPallet.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuImpresionBarraPallet.Name = "mnuImpresionBarraPallet"
        '
        'mnuListaIngresoTag
        '
        Me.mnuListaIngresoTag.Caption = "Ingreso con Tags"
        Me.mnuListaIngresoTag.ImageOptions.SvgImage = CType(resources.GetObject("mnuListaIngresoTag.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuListaIngresoTag.Name = "mnuListaIngresoTag"
        '
        'mnuListaSalidaTag
        '
        Me.mnuListaSalidaTag.Caption = "Salida con Tags"
        Me.mnuListaSalidaTag.ImageOptions.SvgImage = CType(resources.GetObject("mnuListaSalidaTag.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuListaSalidaTag.Name = "mnuListaSalidaTag"
        '
        'RepositoryItemButtonEdit1
        '
        Me.RepositoryItemButtonEdit1.AutoHeight = False
        Me.RepositoryItemButtonEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.RepositoryItemButtonEdit1.Name = "RepositoryItemButtonEdit1"
        '
        'RepositoryItemSearchLookUpEdit1
        '
        Me.RepositoryItemSearchLookUpEdit1.AutoHeight = False
        Me.RepositoryItemSearchLookUpEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemSearchLookUpEdit1.Name = "RepositoryItemSearchLookUpEdit1"
        Me.RepositoryItemSearchLookUpEdit1.PopupView = Me.RepositoryItemSearchLookUpEdit1View
        '
        'RepositoryItemSearchLookUpEdit1View
        '
        Me.RepositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemSearchLookUpEdit1View.Name = "RepositoryItemSearchLookUpEdit1View"
        Me.RepositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'RepositoryItemZoomTrackBar1
        '
        Me.RepositoryItemZoomTrackBar1.Middle = 5
        Me.RepositoryItemZoomTrackBar1.Name = "RepositoryItemZoomTrackBar1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblNombrePCCliente)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblVersion)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblUsuario)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblServerAPP)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblBDAPP)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblEmpresa)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblBodega)
        Me.RibbonStatusBar.ItemLinks.Add(Me.bbiCambiaBodega)
        Me.RibbonStatusBar.ItemLinks.Add(Me.sddiSkinWMS)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblModoDebug)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblWSHHURL)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 676)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(38, 48, 38, 48)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.rbMain
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1714, 30)
        '
        'PopupControlContainer1
        '
        Me.PopupControlContainer1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PopupControlContainer1.Controls.Add(Me.lblprg)
        Me.PopupControlContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PopupControlContainer1.Location = New System.Drawing.Point(0, 193)
        Me.PopupControlContainer1.Margin = New System.Windows.Forms.Padding(24, 25, 24, 25)
        Me.PopupControlContainer1.Name = "PopupControlContainer1"
        Me.PopupControlContainer1.Ribbon = Me.rbMain
        Me.PopupControlContainer1.Size = New System.Drawing.Size(1714, 513)
        Me.PopupControlContainer1.TabIndex = 3
        Me.PopupControlContainer1.Visible = False
        '
        'lblprg
        '
        Me.lblprg.BackColor = System.Drawing.Color.OldLace
        Me.lblprg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblprg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblprg.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.lblprg.Location = New System.Drawing.Point(0, 390)
        Me.lblprg.Margin = New System.Windows.Forms.Padding(4)
        Me.lblprg.Name = "lblprg"
        Me.lblprg.Size = New System.Drawing.Size(1714, 123)
        Me.lblprg.TabIndex = 35
        Me.lblprg.Text = ""
        Me.lblprg.Visible = False
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Clientes"
        Me.BarButtonItem2.Id = 1
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'xtMdi
        '
        Me.xtMdi.MdiParent = Me
        '
        'RibbonPageGroup4
        '
        Me.RibbonPageGroup4.ItemLinks.Add(Me.mnuProductos)
        Me.RibbonPageGroup4.Name = "RibbonPageGroup4"
        '
        'RibbonPageGroup7
        '
        Me.RibbonPageGroup7.ItemLinks.Add(Me.mnuPicking)
        Me.RibbonPageGroup7.Name = "RibbonPageGroup7"
        '
        'RibbonPageGroup14
        '
        Me.RibbonPageGroup14.ItemLinks.Add(Me.mnuRepIngresoRapido)
        Me.RibbonPageGroup14.Name = "RibbonPageGroup14"
        '
        'BarSubItem5
        '
        Me.BarSubItem5.Caption = "Reportes de ingreso"
        Me.BarSubItem5.Id = 34
        Me.BarSubItem5.ImageOptions.Image = CType(resources.GetObject("BarSubItem5.ImageOptions.Image"), System.Drawing.Image)
        Me.BarSubItem5.ImageOptions.LargeImage = CType(resources.GetObject("BarSubItem5.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarSubItem5.Name = "BarSubItem5"
        '
        'BarStaticItem1
        '
        Me.BarStaticItem1.Caption = "PROGRAN"
        Me.BarStaticItem1.Id = 23
        Me.BarStaticItem1.ImageOptions.Image = CType(resources.GetObject("BarStaticItem1.ImageOptions.Image"), System.Drawing.Image)
        Me.BarStaticItem1.Name = "BarStaticItem1"
        Me.BarStaticItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'BarStaticItem2
        '
        Me.BarStaticItem2.Caption = "PROGRAN"
        Me.BarStaticItem2.Id = 23
        Me.BarStaticItem2.ImageOptions.Image = CType(resources.GetObject("BarStaticItem2.ImageOptions.Image"), System.Drawing.Image)
        Me.BarStaticItem2.Name = "BarStaticItem2"
        Me.BarStaticItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuProveedores)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'colSeleccionar
        '
        Me.colSeleccionar.FieldName = "Seleccionar"
        Me.colSeleccionar.Name = "colSeleccionar"
        '
        'colProducto
        '
        Me.colProducto.FieldName = "Producto"
        Me.colProducto.Name = "colProducto"
        Me.colProducto.OptionsColumn.ReadOnly = True
        '
        'BarButtonItem11
        '
        Me.BarButtonItem11.Caption = "Roles de Operador"
        Me.BarButtonItem11.Id = 92
        Me.BarButtonItem11.Name = "BarButtonItem11"
        '
        'BarButtonItem12
        '
        Me.BarButtonItem12.Caption = "Roles de Operador"
        Me.BarButtonItem12.Id = 92
        Me.BarButtonItem12.Name = "BarButtonItem12"
        '
        'mnuReporteTransNav
        '
        Me.mnuReporteTransNav.Caption = "Datos MI3"
        Me.mnuReporteTransNav.Id = 227
        Me.mnuReporteTransNav.ImageOptions.Image = CType(resources.GetObject("mnuReporteTransNav.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuReporteTransNav.ImageOptions.LargeImage = CType(resources.GetObject("mnuReporteTransNav.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuReporteTransNav.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdTransOut), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdBarrasPlt)})
        Me.mnuReporteTransNav.Name = "mnuReporteTransNav"
        '
        'PopupMenu3
        '
        Me.PopupMenu3.Name = "PopupMenu3"
        Me.PopupMenu3.Ribbon = Me.rbMain
        '
        'BarButtonItem16
        '
        Me.BarButtonItem16.Caption = "Inventario"
        Me.BarButtonItem16.Id = 381
        Me.BarButtonItem16.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem16.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem16.Name = "BarButtonItem16"
        '
        'frmMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1714, 706)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.PopupControlContainer1)
        Me.Controls.Add(Me.rbMain)
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmMenu"
        Me.Ribbon = Me.rbMain
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "TOMWMS Menú Principal"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.rbMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemBreadCrumbEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GalleryDropDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pmBodegas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupControlContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PopupControlContainer1.ResumeLayout(False)
        CType(Me.xtMdi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private Sub BarButtonItem25_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdIngresos.ItemClick

        If Not permiteMenu(e.Link) Then Return

        With frmIngreso_List
            .Modo = frmIngreso_List.pModo.Lista
            .MdiParent = Me
            .Show()
            .Focus()
        End With

        SplashScreenManager.CloseForm(False)

    End Sub

    Friend WithEvents rbMain As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents rpCatalogos As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents pgEmpresa As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents xtMdi As DevExpress.XtraTabbedMdi.XtraTabbedMdiManager
    Friend WithEvents mnuBodega As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuUbicacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuProductos As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuMantClas As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuFamilia As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMarca As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMantEstados As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMantTipo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMantUnidMed As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMantProducto As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pgBodega As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents pgProductos As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuMantOperadores As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup4 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuMantPropietarios As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents SkinRibbonGalleryBarItem1 As DevExpress.XtraBars.SkinRibbonGalleryBarItem
    Friend WithEvents RibbonGalleryBarItem1 As DevExpress.XtraBars.RibbonGalleryBarItem
    Friend WithEvents SkinRibbonGalleryBarItem2 As DevExpress.XtraBars.SkinRibbonGalleryBarItem
    Friend WithEvents RepositoryItemButtonEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents mnuMuelles As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEmpresa As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuMantEmpresa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMantMotivoAnul As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMantMotivoDevol As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pgLogistica As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuClientes As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuMantCliente As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuTipoCliente As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuTiemposAceptacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuProveedores As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pgClientes As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuSeguridad As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuMantUsuarios As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuRolesUsuario As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pgProveedores As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents lblNombrePCCliente As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblVersion As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents rpIngresos As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents mnuOrdenesCompra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuRecepcion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuRepIngresoRapido As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuLogistica As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuEmpTrans As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuVehiculos As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pgSeguridad As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents pgTareaIngreso As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents pgReportesIngresoRapido As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents rpSalidas As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents rpInventarios As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents mnuCambioUbicacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rpMovimientos As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents pgCambioUbicacion As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuCambioEstado As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCotizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pgCotizar As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents pgCambioEstado As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuPedidoVenta As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pgPedidoVenta As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuPicking As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pgPicking As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPageGroup7 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuDespachos As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pgDespacho As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents pgInventarios As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents pgSalidasRep As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPageGroup14 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents BarSubItem5 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarButtonItem38 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem39 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem40 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuRepSalidasRapido As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuInventarios As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMantJornadaLab As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuControlMontacargas As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblUsuario As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents BarStaticItem1 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents BarStaticItem2 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents mnuMantPais As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuMantHorarioLab As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMantTurnoLab As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuPiloto As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuPropietarioBodega As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonGroup1 As DevExpress.XtraBars.BarButtonGroup
    Friend WithEvents BarButtonGroup2 As DevExpress.XtraBars.BarButtonGroup
    Friend WithEvents BarButtonGroup3 As DevExpress.XtraBars.BarButtonGroup
    Friend WithEvents BarButtonGroup4 As DevExpress.XtraBars.BarButtonGroup
    Friend WithEvents BarMdiChildrenListItem1 As DevExpress.XtraBars.BarMdiChildrenListItem
    Friend WithEvents mnuMantBodega As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarSubProveedor As DevExpress.XtraBars.BarSubItem
    Friend WithEvents cmdProveedor As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdProveedores As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdArancel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonGroup5 As DevExpress.XtraBars.BarButtonGroup
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rpgOdenCompra As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuVendedorRoad As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuVendedorRuta As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuFallaMontacarga As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuRolesOperador As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdCalendario As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RpgCalendario As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuTraslados As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pgTraslados As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents BarMdiChildrenListItem2 As DevExpress.XtraBars.BarMdiChildrenListItem
    Friend WithEvents BarSubItem1 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarLinkContainerItem1 As DevExpress.XtraBars.BarLinkContainerItem
    Friend WithEvents BarButtonItem5 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem6 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuGeograficos As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuDepartamento As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMunicpio As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuRegion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMotivoUbic As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblHoraActual As DevExpress.XtraBars.BarHeaderItem
    Friend WithEvents rpReportes As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents pgReportes As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuExistencias As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem7 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdDetalleExistencia As DevExpress.XtraBars.BarSubItem
    Friend WithEvents cmdDetalleSerie As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdDetalleParametro As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rbPageMonitor As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents colSeleccionar As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colProducto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarButtonItem9 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuTipoTarima As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuTipoConte As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuTarima As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem13 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem14 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuPropietarios As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuMantPropietario As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMantReglaMsj As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMantReglaRc As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem18 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMonitor As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuConfigurarMonitor As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMostrarMonitor As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImpresora As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdConexionBD As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rpAdministrador As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents pgParametrosCn As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents lblServerAPP As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblBDAPP As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents cmdUbicacionPicking As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdTipoIngreso As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuOrdenCompra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuUbicSug As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuIndiceRot As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuReglaUbicacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuReglasList As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarEditItem1 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemBreadCrumbEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemBreadCrumbEdit
    Friend WithEvents BarButtonItem24 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdIngresos As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem25 As BarButtonItem
    Friend WithEvents mnuReglaUbicPrio As BarButtonItem
    Friend WithEvents BarButtonItem27 As BarButtonItem
    Friend WithEvents mnuConversion As BarButtonItem
    Friend WithEvents cmdLetra As BarButtonItem
    Friend WithEvents cmdTipoEtiqueta As BarButtonItem
    Friend WithEvents mnuImpEtiqueta As BarButtonItem
    Friend WithEvents mnuInterfaceNav As BarButtonItem
    Friend WithEvents rpgInterface As Ribbon.RibbonPageGroup
    Friend WithEvents PgPropietario2 As Ribbon.RibbonPageGroup
    Friend WithEvents lblEmpresa As BarMdiChildrenListItem
    Friend WithEvents RepositoryItemSearchLookUpEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents RepositoryItemSearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblBodega As BarButtonItem
    Friend WithEvents RepositoryItemZoomTrackBar1 As DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar
    Friend WithEvents cmdExistenciasProductos As BarButtonItem
    Friend WithEvents cmdValorizacion As BarButtonItem
    Friend WithEvents cmdMovimientos As BarSubItem
    Friend WithEvents cmdMovimiento As BarButtonItem
    Friend WithEvents cmdMovimientosDet As BarButtonItem
    Friend WithEvents PopupMenu1 As PopupMenu
    Friend WithEvents mnuLicencia As BarButtonItem
    Friend WithEvents mnuLicencias As BarButtonItem
    Friend WithEvents RibbonMiniToolbar1 As Ribbon.RibbonMiniToolbar
    Friend WithEvents rpgLicencia As Ribbon.RibbonPageGroup
    Friend WithEvents cmdUnidadMedidaconversion As BarButtonItem
    Friend WithEvents grpRepTablero As Ribbon.RibbonPageGroup
    Friend WithEvents cmdResumenExistencia As BarButtonItem
    Friend WithEvents cmdResumenExistenciasUMBas As BarButtonItem
    Friend WithEvents mnuEstructuraInicial As BarButtonItem
    Friend WithEvents cmdMovimientosCardex As BarButtonItem
    Friend WithEvents BarButtonItem8 As BarButtonItem
    Friend WithEvents GalleryDropDown1 As Ribbon.GalleryDropDown
    Friend WithEvents mnuAjusteStock As BarSubItem
    Friend WithEvents pgAjusteInventario As Ribbon.RibbonPageGroup
    Friend WithEvents cmdMotivoAjuste As BarButtonItem
    Friend WithEvents cmdTipoAjuste As BarButtonItem
    Friend WithEvents cmdAjusteInventario As BarButtonItem
    Friend WithEvents BarButtonItem10 As BarButtonItem
    Friend WithEvents cmdExistenciasEstado As BarButtonItem
    Friend WithEvents cmdMovimientosUbic As BarButtonItem
    Friend WithEvents cmdControl As BarSubItem
    Friend WithEvents cmdProximosVencer As BarButtonItem
    Friend WithEvents cmdMinMax As BarButtonItem
    Friend WithEvents cmdPendientesReq As BarButtonItem
    Friend WithEvents cmdStockTrans As BarButtonItem
    Friend WithEvents cmdRotacionProd As BarButtonItem
    Friend WithEvents cmdTrazaLote As BarButtonItem
    Friend WithEvents mnuResetMenuLayOut As BarButtonItem
    Friend WithEvents pgResetMenu As Ribbon.RibbonPageGroup
    Friend WithEvents cmdStockEnFecha As BarButtonItem
    Friend WithEvents mnuTareasPreIngreso As BarButtonItem
    Friend WithEvents pgTareasPreIngreso As Ribbon.RibbonPageGroup
    Friend WithEvents cmdExistenciasUbic As BarButtonItem
    Friend WithEvents cmdExistenciaspordocumento As BarButtonItem
    Friend WithEvents mnuCambiarContraseña As BarButtonItem
    Friend WithEvents BarButtonItem11 As BarButtonItem
    Friend WithEvents BarButtonItem12 As BarButtonItem
    Friend WithEvents cmdExistenciasPorLote As BarButtonItem
    Friend WithEvents pgReporteTransacciones As Ribbon.RibbonPageGroup
    Friend WithEvents cmdDetalleLotePorUbi As BarButtonItem
    Friend WithEvents cmdMovCardexConDocs As BarButtonItem
    Friend WithEvents cmdStockRes As BarButtonItem
    Friend WithEvents grpStockRes As Ribbon.RibbonPageGroup
    Friend WithEvents mnuEstadoEnviosNAV As BarButtonItem
    Friend WithEvents cmdExistenciaPorTipoProd As BarButtonItem
    Friend WithEvents pgBackup As Ribbon.RibbonPageGroup
    Friend WithEvents mnuBackup As BarButtonItem
    Friend WithEvents CodBarras1 As CodBarras
    Friend WithEvents mnuCambioDeUsuario As BarButtonItem
    Friend WithEvents rpgCambioDeUsuario As Ribbon.RibbonPageGroup
    Friend WithEvents cmdDetalleInventario As BarButtonItem
    Friend WithEvents cmdSerieDocs As BarButtonItem
    Friend WithEvents cmdRpExitLP As BarButtonItem
    Friend WithEvents cmdExistCnRec As BarButtonItem
    Friend WithEvents cmdTransOut As BarButtonItem
    Friend WithEvents cmdBarrasPlt As BarButtonItem
    Friend WithEvents mnuReporteTransNav As BarSubItem
    Friend WithEvents cmdMi3 As BarSubItem
    Friend WithEvents cmdTransaccionesOut As BarButtonItem
    Friend WithEvents cmdBarrasPallet As BarButtonItem
    Friend WithEvents cmdCodBarras As BarButtonItem
    Friend WithEvents cmdPrintSvr As BarButtonItem
    Friend WithEvents rpgServicioImpresiones As Ribbon.RibbonPageGroup
    Friend WithEvents cmdLogInterface As BarButtonItem
    Friend WithEvents cmdPrint As BarButtonItem
    Friend WithEvents mnuDashBoardDesigner As BarButtonItem
    Friend WithEvents mnuTipoCuadrilla As BarButtonItem
    Friend WithEvents mnuCuadrilla As BarButtonItem
    Friend WithEvents mnuReporteDistribucionPorTramo As BarButtonItem
    Friend WithEvents mnuServicios As BarButtonItem
    Friend WithEvents mnuTarifas As BarButtonItem
    Friend WithEvents mnuRegimenFiscal As BarButtonItem
    Friend WithEvents mnuConfiguracionInt As BarButtonItem
    Friend WithEvents cmdDocConDiferencias As BarButtonItem
    Friend WithEvents PopupMenu2 As PopupMenu
    Friend WithEvents mnuRepSalidaRapido As BarSubItem
    Friend WithEvents cmdDocPeConDiferencias As BarButtonItem
    Friend WithEvents mnuRepSalidasRapidoD As BarButtonItem
    Friend WithEvents cmdExistFiscal As BarButtonItem
    Friend WithEvents mnuTiemposTareas As BarButtonItem
    Friend WithEvents mnuRepFiscales As BarSubItem
    Friend WithEvents pgReporteFiscal As Ribbon.RibbonPageGroup
    Friend WithEvents cmdCtaOrden As BarButtonItem
    Friend WithEvents BarSubItem2 As BarSubItem
    Friend WithEvents cmdHistResGeneral As BarButtonItem
    Friend WithEvents cmdHistResFiscal As BarButtonItem
    Friend WithEvents cmdResumenCliente As BarButtonItem
    Friend WithEvents mnuRepFiscal As BarSubItem
    Friend WithEvents mnuMantConsolidador As BarButtonItem
    Friend WithEvents cmdCtas As BarSubItem
    Friend WithEvents cmdCtasOrden As BarButtonItem
    Friend WithEvents cmdCtaOrdenPoliza As BarButtonItem
    Friend WithEvents cmdMovimiento_Reporte As BarButtonItem
    Friend WithEvents cmdExistConsolidador As BarButtonItem
    Friend WithEvents bbiCambiaBodega As BarButtonItem
    Friend WithEvents PopupControlContainer1 As PopupControlContainer
    Friend WithEvents sddiSkinWMS As SkinDropDownButtonItem
    Friend WithEvents pmBodegas As PopupMenu
    Friend WithEvents cmdExistPorClasif As BarButtonItem
    Friend WithEvents cmdExistenciasPropietario As BarButtonItem
    Friend WithEvents cmdExistenciasPorLote_Posicion As BarButtonItem
    Friend WithEvents pgRegistroServicios As Ribbon.RibbonPageGroup
    Friend WithEvents cmdStockJornadaSistema As BarButtonItem
    Friend WithEvents cmdServicios As BarButtonItem
    Friend WithEvents mnuReporteServicios As BarSubItem
    Friend WithEvents btnServicios As BarButtonItem
    Friend WithEvents pgReporteServicios As Ribbon.RibbonPageGroup
    Friend WithEvents mnuReportesGallery As BarButtonItem
    Friend WithEvents pgReportesGallery As Ribbon.RibbonPageGroup
    Friend WithEvents pgIndicadores As Ribbon.RibbonPageGroup
    Friend WithEvents mnuIndicadores As BarSubItem
    Friend WithEvents mnuAnalitica1 As BarButtonItem
    Friend WithEvents mnuRegistroServicios As BarSubItem
    Friend WithEvents mnuServiciosIngreso As BarButtonItem
    Friend WithEvents mnuServiciosSalidas As BarButtonItem
    Friend WithEvents rpServicios As Ribbon.RibbonPage
    Friend WithEvents mnuTiposDocumentoIngreso As BarButtonItem
    Friend WithEvents mnuTiposDocumentoSalida As BarButtonItem
    Friend WithEvents mnuKardexLote As BarButtonItem
    Friend WithEvents cmdValorizacionOC As BarButtonItem
    Friend WithEvents cmdStockResJornada As BarButtonItem
    Friend WithEvents cmdResValorizacion As BarButtonItem
    Friend WithEvents cmdMovporLote As BarButtonItem
    Friend WithEvents cmdResValorizacionMerca As BarButtonItem
    Friend WithEvents btInvInicial As BarButtonItem
    Friend WithEvents mnuAnalitica2 As BarButtonItem
    Friend WithEvents mnuAnalitica3 As BarButtonItem
    Friend WithEvents cmdEstacionalidadProducto As BarButtonItem
    Friend WithEvents cmdStockEnLinea As BarButtonItem
    Friend WithEvents cmdMovimientosPoliza As BarButtonItem
    Friend WithEvents mnuMantCentroCostos As BarButtonItem
    Friend WithEvents cmdAuditoriaPicking As BarButtonItem
    Friend WithEvents mnuZonaPicking As BarButtonItem
    Friend WithEvents cmdStockJornada As BarButtonItem
    Friend WithEvents mnuParametros As BarSubItem
    Friend WithEvents cmdParametroA As BarButtonItem
    Friend WithEvents cmdParametroB As BarButtonItem
    Friend WithEvents BarButtonItem15 As BarButtonItem
    Friend WithEvents cmdMovimientosDoc As BarButtonItem
    Friend WithEvents cmdSalidasDiasPiso As BarButtonItem
    Friend WithEvents mnuLogErrorWMS As BarButtonItem
    Friend WithEvents pgLogWMS As Ribbon.RibbonPageGroup
    Friend WithEvents cmdAuditoriaRetroactivo As BarButtonItem
    Friend WithEvents cmdLicenciasPorUbicacion As BarButtonItem
    Friend WithEvents mnuAnalitica As BarButtonItem
    Friend WithEvents cmdIngresoPoliza As BarButtonItem
    Friend WithEvents mnuQAEscenariosReserva As BarSubItem
    Friend WithEvents rbPageQA As Ribbon.RibbonPageGroup
    Friend WithEvents mnuCaso1ReservaIdealsa As BarButtonItem
    Friend WithEvents mnuQAReservas As BarButtonItem
    Friend WithEvents lblModoDebug As BarStaticItem
    Friend WithEvents rpgSat As Ribbon.RibbonPageGroup
    Friend WithEvents mnuReportesSAT As BarSubItem
    Friend WithEvents mnuRptIngresosSAT As BarButtonItem
    Friend WithEvents mnuRptSalidasSAT As BarButtonItem
    Friend WithEvents mnurptExistenciasSAT As BarButtonItem
    Friend WithEvents mnuActualizarBD As BarButtonItem
    Friend WithEvents rpgActualizacionesBD As Ribbon.RibbonPageGroup
    Friend WithEvents lblprg As RichTextBox
    Friend WithEvents mnuActualizarIndices As BarButtonItem
    Friend WithEvents rpControlCalidad As Ribbon.RibbonPage
    Friend WithEvents rpGestionInventario As Ribbon.RibbonPageGroup
    Friend WithEvents mnuGestionInventarioCalidad As BarSubItem
    Friend WithEvents mnuHabilitacionLotes As BarButtonItem
    Friend WithEvents rpManufacturaLigera As Ribbon.RibbonPage
    Friend WithEvents rpMLTareas As Ribbon.RibbonPageGroup
    Friend WithEvents mnuReportesControlCalidad As BarSubItem
    Friend WithEvents cmdMovimientosControlCalidad As BarButtonItem
    Friend WithEvents mnuTamañoTablas As BarButtonItem
    Friend WithEvents lblWSHHURL As BarStaticItem
    Friend WithEvents cmdReglasVencimiento As BarButtonItem
    Friend WithEvents PopupMenu3 As PopupMenu
    Friend WithEvents mnuManufactura As BarSubItem
    Friend WithEvents cmdTransaccionesManufactura As BarButtonItem
    Friend WithEvents cmdTipo As BarButtonItem
    Friend WithEvents pgPreFacturacion As Ribbon.RibbonPageGroup
    Friend WithEvents mnuPreFacturacion As BarSubItem
    Friend WithEvents cmdPreFacturar As BarButtonItem
    Friend WithEvents cmdAcuerdosyServicios As BarButtonItem
    Friend WithEvents cmdMercaVencida As BarButtonItem
    Friend WithEvents mnuTiemposRecepcion As BarButtonItem
    Friend WithEvents mnuRptAjustesInventario As BarButtonItem
    Friend WithEvents mnuTiemposDespacho As BarButtonItem
    Friend WithEvents mnuKPIResumen As BarButtonItem
    Friend WithEvents mnuVisualizarTableroWMS As BarButtonItem
    Friend WithEvents mnuTransaccionesPendientesReenvio As BarButtonItem
    Friend WithEvents cmdMovimientosEstado As BarButtonItem
    Friend WithEvents cmdPackingDespachados As BarButtonItem
    Friend WithEvents mnuTalla As BarButtonItem
    Friend WithEvents mnuColor As BarButtonItem
    Friend WithEvents mnuProductividad As BarSubItem
    Friend WithEvents mnuProductividadPicking As BarButtonItem
    Friend WithEvents pgProductividad As Ribbon.RibbonPageGroup
    Friend WithEvents mnurptTransaccionesOP As BarButtonItem
    Friend WithEvents mnuCampaña As BarButtonItem
    Friend WithEvents mnuInterfaceDMS As BarButtonItem
    Friend WithEvents mnuVerificacionBOF As BarButtonItem
    Friend WithEvents pgVerificacionBOF As Ribbon.RibbonPageGroup
    Friend WithEvents cmdOcupacionArea As BarButtonItem
    Friend WithEvents cmdIA As BarButtonItem
    Friend WithEvents mnuImpresionBarraPallet As BarButtonItem
    Friend WithEvents rpRFID As Ribbon.RibbonPage
    Friend WithEvents rpgRFID As Ribbon.RibbonPageGroup
    Friend WithEvents mnuListaIngresoTag As BarButtonItem
    Friend WithEvents mnuListaSalidaTag As BarButtonItem
    Friend WithEvents mnuStockTag As BarButtonItem
    Friend WithEvents BarButtonItem16 As BarButtonItem
    'Friend WithEvents Picking_Service1 As wWSPicking.Picking_Service
End Class
