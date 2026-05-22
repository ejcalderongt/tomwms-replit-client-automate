using Microsoft.EntityFrameworkCore;
using WMSPortal.Models.Database;
#nullable disable

namespace WMSPortal.Library.Database
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AjusteMotivo> AjusteMotivos { get; set; }
        public virtual DbSet<AjusteTipo> AjusteTipos { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Arancel> Arancels { get; set; }
        public virtual DbSet<Bodega> Bodegas { get; set; }
        public virtual DbSet<BodegaArea> BodegaAreas { get; set; }
        public virtual DbSet<BodegaMonitorParametro> BodegaMonitorParametros { get; set; }
        public virtual DbSet<BodegaMuelle> BodegaMuelles { get; set; }
        public virtual DbSet<BodegaOrientacionPo> BodegaOrientacionPos { get; set; }
        public virtual DbSet<BodegaParametro> BodegaParametros { get; set; }
        public virtual DbSet<BodegaSector> BodegaSectors { get; set; }
        public virtual DbSet<BodegaTramo> BodegaTramos { get; set; }
        public virtual DbSet<BodegaUbicacion> BodegaUbicacions { get; set; }
        public virtual DbSet<Camara> Camaras { get; set; }
        public virtual DbSet<Cbcatalogoproducto> Cbcatalogoproductos { get; set; }
        public virtual DbSet<CealsaVwacuerdocomercialdet> CealsaVwacuerdocomercialdets { get; set; }
        public virtual DbSet<CealsaVwacuerdocomercialenc> CealsaVwacuerdocomercialencs { get; set; }
        public virtual DbSet<CealsaVwcliente> CealsaVwclientes { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<ClienteBodega> ClienteBodegas { get; set; }
        public virtual DbSet<ClienteDireccion> ClienteDireccions { get; set; }
        public virtual DbSet<ClienteTiempo> ClienteTiempos { get; set; }
        public virtual DbSet<ClienteTipo> ClienteTipos { get; set; }
        public virtual DbSet<CodBarraClc> CodBarraClcs { get; set; }
        public virtual DbSet<ConfiguracionBarraPallet> ConfiguracionBarraPallets { get; set; }
        public virtual DbSet<Consolidador> Consolidadors { get; set; }
        public virtual DbSet<Contenedor> Contenedors { get; set; }
        public virtual DbSet<CuadrillaDetMontacarga> CuadrillaDetMontacargas { get; set; }
        public virtual DbSet<CuadrillaDetOperador> CuadrillaDetOperadors { get; set; }
        public virtual DbSet<CuadrillaEnc> CuadrillaEncs { get; set; }
        public virtual DbSet<CuadrillaTipo> CuadrillaTipos { get; set; }
        public virtual DbSet<DhOcupacionBodega> DhOcupacionBodegas { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<EmpresaTransporte> EmpresaTransportes { get; set; }
        public virtual DbSet<EmpresaTransporteBodega> EmpresaTransporteBodegas { get; set; }
        public virtual DbSet<EmpresaTransportePiloto> EmpresaTransportePilotos { get; set; }
        public virtual DbSet<EmpresaTransporteVehiculo> EmpresaTransporteVehiculos { get; set; }
        public virtual DbSet<EstructuraGrupo> EstructuraGrupos { get; set; }
        public virtual DbSet<EstructuraTramo> EstructuraTramos { get; set; }
        public virtual DbSet<EstructuraUbicacion> EstructuraUbicacions { get; set; }
        public virtual DbSet<FontDet> FontDets { get; set; }
        public virtual DbSet<FontEnc> FontEncs { get; set; }
        public virtual DbSet<HorarioLaboralDet> HorarioLaboralDets { get; set; }
        public virtual DbSet<HorarioLaboralEnc> HorarioLaboralEncs { get; set; }
        public virtual DbSet<INavAcuerdo> INavAcuerdos { get; set; }
        public virtual DbSet<INavAcuerdoDet> INavAcuerdoDets { get; set; }
        public virtual DbSet<INavAcuerdoEnc> INavAcuerdoEncs { get; set; }
        public virtual DbSet<INavAcuerdoProducto> INavAcuerdoProductos { get; set; }
        public virtual DbSet<INavBarrasPallet> INavBarrasPallets { get; set; }
        public virtual DbSet<INavBodega> INavBodegas { get; set; }
        public virtual DbSet<INavCliente> INavClientes { get; set; }
        public virtual DbSet<INavConfigDet> INavConfigDets { get; set; }
        public virtual DbSet<INavConfigEnc> INavConfigEncs { get; set; }
        public virtual DbSet<INavConfigEnt> INavConfigEnts { get; set; }
        public virtual DbSet<INavEjecucionDetError> INavEjecucionDetErrors { get; set; }
        public virtual DbSet<INavEjecucionEnc> INavEjecucionEncs { get; set; }
        public virtual DbSet<INavEjecucionRe> INavEjecucionRes { get; set; }
        public virtual DbSet<INavEnt> INavEnts { get; set; }
        public virtual DbSet<INavEntFiltro> INavEntFiltros { get; set; }
        public virtual DbSet<INavPedCompraDet> INavPedCompraDets { get; set; }
        public virtual DbSet<INavPedCompraDetLote> INavPedCompraDetLotes { get; set; }
        public virtual DbSet<INavPedCompraEnc> INavPedCompraEncs { get; set; }
        public virtual DbSet<INavPedTrasladoDet> INavPedTrasladoDets { get; set; }
        public virtual DbSet<INavPedTrasladoEnc> INavPedTrasladoEncs { get; set; }
        public virtual DbSet<INavProducto> INavProductos { get; set; }
        public virtual DbSet<INavProveedor> INavProveedors { get; set; }
        public virtual DbSet<INavServicio> INavServicios { get; set; }
        public virtual DbSet<INavTransaccionesOut> INavTransaccionesOuts { get; set; }
        public virtual DbSet<INavTransaccionesOutError> INavTransaccionesOutErrors { get; set; }
        public virtual DbSet<INavUnidadMedidum> INavUnidadMedida { get; set; }
        public virtual DbSet<ImpresionProductosBarra> ImpresionProductosBarras { get; set; }
        public virtual DbSet<Impresora> Impresoras { get; set; }
        public virtual DbSet<IndiceRotacion> IndiceRotacions { get; set; }
        public virtual DbSet<InterfaceEnc> InterfaceEncs { get; set; }
        public virtual DbSet<JornadaLaboral> JornadaLaborals { get; set; }
        public virtual DbSet<JornadaSistema> JornadaSistemas { get; set; }
        public virtual DbSet<LicenciaItem> LicenciaItems { get; set; }
        public virtual DbSet<LicenciaLlave> LicenciaLlaves { get; set; }
        public virtual DbSet<LicenciaLogin> LicenciaLogins { get; set; }
        public virtual DbSet<LicenciaSolic> LicenciaSolics { get; set; }
        public virtual DbSet<LogErrorWm> LogErrorWms { get; set; }
        public virtual DbSet<LogImportacionExcel> LogImportacionExcels { get; set; }
        public virtual DbSet<Marcaje> Marcajes { get; set; }
        public virtual DbSet<MensajeRegla> MensajeReglas { get; set; }
        public virtual DbSet<MenuRol> MenuRols { get; set; }
        public virtual DbSet<MenuRolOp> MenuRolOps { get; set; }
        public virtual DbSet<MenuSistema> MenuSistemas { get; set; }
        public virtual DbSet<MenuSistemaOp> MenuSistemaOps { get; set; }
        public virtual DbSet<Montacarga> Montacargas { get; set; }
        public virtual DbSet<MontacargaBodega> MontacargaBodegas { get; set; }
        public virtual DbSet<MontacargaServicioEnc> MontacargaServicioEncs { get; set; }
        public virtual DbSet<MontacargaTipoFalla> MontacargaTipoFallas { get; set; }
        public virtual DbSet<MotivoAnulacion> MotivoAnulacions { get; set; }
        public virtual DbSet<MotivoAnulacionBodega> MotivoAnulacionBodegas { get; set; }
        public virtual DbSet<MotivoDevolucion> MotivoDevolucions { get; set; }
        public virtual DbSet<MotivoDevolucionBodega> MotivoDevolucionBodegas { get; set; }
        public virtual DbSet<MotivoUbicacion> MotivoUbicacions { get; set; }
        public virtual DbSet<Operador> Operadors { get; set; }
        public virtual DbSet<OperadorBodega> OperadorBodegas { get; set; }
        public virtual DbSet<PParametro> PParametros { get; set; }
        public virtual DbSet<PaisDepartamento> PaisDepartamentos { get; set; }
        public virtual DbSet<PaisMunicipio> PaisMunicipios { get; set; }
        public virtual DbSet<PaisRegion> PaisRegions { get; set; }
        public virtual DbSet<Paise> Paises { get; set; }
        public virtual DbSet<PerfilSerializado> PerfilSerializados { get; set; }
        public virtual DbSet<PortalMenu> PortalMenus { get; set; }
        public virtual DbSet<PortalMenuRol> PortalMenuRols { get; set; }
        public virtual DbSet<PortalRol> PortalRols { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<ProductoBodega> ProductoBodegas { get; set; }
        public virtual DbSet<ProductoClasificacion> ProductoClasificacions { get; set; }
        public virtual DbSet<ProductoCodigosBarra> ProductoCodigosBarras { get; set; }
        public virtual DbSet<ProductoEstado> ProductoEstados { get; set; }
        public virtual DbSet<ProductoEstadoUbic> ProductoEstadoUbics { get; set; }
        public virtual DbSet<ProductoFamilium> ProductoFamilia { get; set; }
        public virtual DbSet<ProductoKitComposicion> ProductoKitComposicions { get; set; }
        public virtual DbSet<ProductoMarca> ProductoMarcas { get; set; }
        public virtual DbSet<ProductoPallet> ProductoPallets { get; set; }
        public virtual DbSet<ProductoPalletRec> ProductoPalletRecs { get; set; }
        public virtual DbSet<ProductoParametro> ProductoParametros { get; set; }
        public virtual DbSet<ProductoPresentacion> ProductoPresentacions { get; set; }
        public virtual DbSet<ProductoPresentacionTarima> ProductoPresentacionTarimas { get; set; }
        public virtual DbSet<ProductoPresentacionesConversione> ProductoPresentacionesConversiones { get; set; }
        public virtual DbSet<ProductoRellenado> ProductoRellenados { get; set; }
        public virtual DbSet<ProductoSustituto> ProductoSustitutos { get; set; }
        public virtual DbSet<ProductoTipo> ProductoTipos { get; set; }
        public virtual DbSet<Propietario> Propietarios { get; set; }
        public virtual DbSet<PropietarioBodega> PropietarioBodegas { get; set; }
        public virtual DbSet<PropietarioDestinatario> PropietarioDestinatarios { get; set; }
        public virtual DbSet<PropietarioReglasDet> PropietarioReglasDets { get; set; }
        public virtual DbSet<PropietarioReglasEnc> PropietarioReglasEncs { get; set; }
        public virtual DbSet<Proveedor> Proveedors { get; set; }
        public virtual DbSet<ProveedorBodega> ProveedorBodegas { get; set; }
        public virtual DbSet<RegimenFiscal> RegimenFiscals { get; set; }
        public virtual DbSet<ReglaUbicDetIr> ReglaUbicDetIrs { get; set; }
        public virtual DbSet<ReglaUbicDetPe> ReglaUbicDetPes { get; set; }
        public virtual DbSet<ReglaUbicDetPp> ReglaUbicDetPps { get; set; }
        public virtual DbSet<ReglaUbicDetProp> ReglaUbicDetProps { get; set; }
        public virtual DbSet<ReglaUbicDetTp> ReglaUbicDetTps { get; set; }
        public virtual DbSet<ReglaUbicDetTr> ReglaUbicDetTrs { get; set; }
        public virtual DbSet<ReglaUbicEnc> ReglaUbicEncs { get; set; }
        public virtual DbSet<ReglaUbicPrioDet> ReglaUbicPrioDets { get; set; }
        public virtual DbSet<ReglaUbicPrioEnc> ReglaUbicPrioEncs { get; set; }
        public virtual DbSet<ReglaUbicPrioParam> ReglaUbicPrioParams { get; set; }
        public virtual DbSet<ReglaUbicPrioProducto> ReglaUbicPrioProductos { get; set; }
        public virtual DbSet<ReglaUbicSel> ReglaUbicSels { get; set; }
        public virtual DbSet<ReglaUbicSelDet> ReglaUbicSelDets { get; set; }
        public virtual DbSet<ReglaUbicSelEnc> ReglaUbicSelEncs { get; set; }
        public virtual DbSet<ReglaUbicSelItem> ReglaUbicSelItems { get; set; }
        public virtual DbSet<ReglaUbicacion> ReglaUbicacions { get; set; }
        public virtual DbSet<ReglasRecepcion> ReglasRecepcions { get; set; }
        public virtual DbSet<ResolucionLpOperador> ResolucionLpOperadors { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<ResultadoAplicaReservado> ResultadoAplicaReservados { get; set; }
        public virtual DbSet<RoadPVendedor> RoadPVendedors { get; set; }
        public virtual DbSet<RoadRutum> RoadRuta { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<RolBodega> RolBodegas { get; set; }
        public virtual DbSet<RolMenu> RolMenus { get; set; }
        public virtual DbSet<RolOperador> RolOperadors { get; set; }
        public virtual DbSet<SimbologiasCodigoBarra> SimbologiasCodigoBarras { get; set; }
        public virtual DbSet<SisEstadoTareaHh> SisEstadoTareaHhs { get; set; }
        public virtual DbSet<SisObsLog> SisObsLogs { get; set; }
        public virtual DbSet<SisPrioridadTareaHh> SisPrioridadTareaHhs { get; set; }
        public virtual DbSet<SisTipoAccion> SisTipoAccions { get; set; }
        public virtual DbSet<SisTipoTarea> SisTipoTareas { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<StockDet> StockDets { get; set; }
        public virtual DbSet<StockHist> StockHists { get; set; }
        public virtual DbSet<StockJornadum> StockJornada { get; set; }
        public virtual DbSet<StockParametro> StockParametros { get; set; }
        public virtual DbSet<StockRe> StockRes { get; set; }
        public virtual DbSet<StockRec> StockRecs { get; set; }
        public virtual DbSet<StockResSe> StockResSes { get; set; }
        public virtual DbSet<StockSe> StockSes { get; set; }
        public virtual DbSet<StockSeRec> StockSeRecs { get; set; }
        public virtual DbSet<StockTransito> StockTransitos { get; set; }
        public virtual DbSet<TProductoBodega> TProductoBodegas { get; set; }
        public virtual DbSet<TablasSync> TablasSyncs { get; set; }
        public virtual DbSet<TareaHh> TareaHhs { get; set; }
        public virtual DbSet<TarifaTipoTransaccion> TarifaTipoTransaccions { get; set; }
        public virtual DbSet<TarifaTipoTransaccionDet> TarifaTipoTransaccionDets { get; set; }
        public virtual DbSet<Tarima> Tarimas { get; set; }
        public virtual DbSet<TempComparacionInventario> TempComparacionInventarios { get; set; }
        public virtual DbSet<TempComparativoInventario> TempComparativoInventarios { get; set; }
        public virtual DbSet<TempLicenciaLlave> TempLicenciaLlaves { get; set; }
        public virtual DbSet<TipoActualizacionCosto> TipoActualizacionCostos { get; set; }
        public virtual DbSet<TipoContenedor> TipoContenedors { get; set; }
        public virtual DbSet<TipoConteo> TipoConteos { get; set; }
        public virtual DbSet<TipoEtiquetum> TipoEtiqueta { get; set; }
        public virtual DbSet<TipoInventario> TipoInventarios { get; set; }
        public virtual DbSet<TipoRack> TipoRacks { get; set; }
        public virtual DbSet<TipoRotacion> TipoRotacions { get; set; }
        public virtual DbSet<TipoTareaTiempo> TipoTareaTiempos { get; set; }
        public virtual DbSet<TipoTarima> TipoTarimas { get; set; }
        public virtual DbSet<TmpBodegaUbicacion> TmpBodegaUbicacions { get; set; }
        public virtual DbSet<TmpEstructuraUbicacion> TmpEstructuraUbicacions { get; set; }
        public virtual DbSet<TmpHouseDatum> TmpHouseData { get; set; }
        public virtual DbSet<TmpINavTransaccionesOut> TmpINavTransaccionesOuts { get; set; }
        public virtual DbSet<TmpLicenciaItem> TmpLicenciaItems { get; set; }
        public virtual DbSet<TmpStock13540> TmpStock13540s { get; set; }
        public virtual DbSet<TmpStock18782> TmpStock18782s { get; set; }
        public virtual DbSet<TmpStockRe> TmpStockRes { get; set; }
        public virtual DbSet<TmsTicket> TmsTickets { get; set; }
        public virtual DbSet<TmsTicketPol> TmsTicketPols { get; set; }
        public virtual DbSet<TransAjusteDet> TransAjusteDets { get; set; }
        public virtual DbSet<TransAjusteDetDoc> TransAjusteDetDocs { get; set; }
        public virtual DbSet<TransAjusteEnc> TransAjusteEncs { get; set; }
        public virtual DbSet<TransDespachoDet> TransDespachoDets { get; set; }
        public virtual DbSet<TransDespachoDetLoteNum> TransDespachoDetLoteNums { get; set; }
        public virtual DbSet<TransDespachoEnc> TransDespachoEncs { get; set; }
        public virtual DbSet<TransDiEnc> TransDiEncs { get; set; }
        public virtual DbSet<TransInvCiclico> TransInvCiclicos { get; set; }
        public virtual DbSet<TransInvCiclicoUbic> TransInvCiclicoUbics { get; set; }
        public virtual DbSet<TransInvDetalle> TransInvDetalles { get; set; }
        public virtual DbSet<TransInvEnc> TransInvEncs { get; set; }
        public virtual DbSet<TransInvEncReconteo> TransInvEncReconteos { get; set; }
        public virtual DbSet<TransInvNe> TransInvNes { get; set; }
        public virtual DbSet<TransInvOperador> TransInvOperadors { get; set; }
        public virtual DbSet<TransInvReconteo> TransInvReconteos { get; set; }
        public virtual DbSet<TransInvResuman> TransInvResumen { get; set; }
        public virtual DbSet<TransInvStock> TransInvStocks { get; set; }
        public virtual DbSet<TransInvStockProd> TransInvStockProds { get; set; }
        public virtual DbSet<TransInvTramo> TransInvTramos { get; set; }
        public virtual DbSet<TransInventarioDet> TransInventarioDets { get; set; }
        public virtual DbSet<TransInventarioEnc> TransInventarioEncs { get; set; }
        public virtual DbSet<TransMovimiento> TransMovimientos { get; set; }
        public virtual DbSet<TransMovimientoPallet> TransMovimientoPallets { get; set; }
        public virtual DbSet<TransOcDet> TransOcDets { get; set; }
        public virtual DbSet<TransOcDetLote> TransOcDetLotes { get; set; }
        public virtual DbSet<TransOcDocuRef> TransOcDocuRefs { get; set; }
        public virtual DbSet<TransOcEnc> TransOcEncs { get; set; }
        public virtual DbSet<TransOcEstado> TransOcEstados { get; set; }
        public virtual DbSet<TransOcImagen> TransOcImagens { get; set; }
        public virtual DbSet<TransOcPol> TransOcPols { get; set; }
        public virtual DbSet<TransOcServicio> TransOcServicios { get; set; }
        public virtual DbSet<TransOcTi> TransOcTis { get; set; }
        public virtual DbSet<TransPackingEnc> TransPackingEncs { get; set; }
        public virtual DbSet<TransPeDet> TransPeDets { get; set; }
        public virtual DbSet<TransPeDocuRef> TransPeDocuRefs { get; set; }
        public virtual DbSet<TransPeDocuRefDet> TransPeDocuRefDets { get; set; }
        public virtual DbSet<TransPeEnc> TransPeEncs { get; set; }
        public virtual DbSet<TransPePol> TransPePols { get; set; }
        public virtual DbSet<TransPeServicio> TransPeServicios { get; set; }
        public virtual DbSet<TransPeTipo> TransPeTipos { get; set; }
        public virtual DbSet<TransPickingDet> TransPickingDets { get; set; }
        public virtual DbSet<TransPickingDetParametro> TransPickingDetParametros { get; set; }
        public virtual DbSet<TransPickingEnc> TransPickingEncs { get; set; }
        public virtual DbSet<TransPickingOp> TransPickingOps { get; set; }
        public virtual DbSet<TransPickingUbic> TransPickingUbics { get; set; }
        public virtual DbSet<TransReDet> TransReDets { get; set; }
        public virtual DbSet<TransReDetInfraccion> TransReDetInfraccions { get; set; }
        public virtual DbSet<TransReDetLoteNum> TransReDetLoteNums { get; set; }
        public virtual DbSet<TransReDetParametro> TransReDetParametros { get; set; }
        public virtual DbSet<TransReEnc> TransReEncs { get; set; }
        public virtual DbSet<TransReFact> TransReFacts { get; set; }
        public virtual DbSet<TransReImg> TransReImgs { get; set; }
        public virtual DbSet<TransReOc> TransReOcs { get; set; }
        public virtual DbSet<TransReOp> TransReOps { get; set; }
        public virtual DbSet<TransReTr> TransReTrs { get; set; }
        public virtual DbSet<TransReabastecimientoLog> TransReabastecimientoLogs { get; set; }
        public virtual DbSet<TransServicioDet> TransServicioDets { get; set; }
        public virtual DbSet<TransServicioEnc> TransServicioEncs { get; set; }
        public virtual DbSet<TransTrasDet> TransTrasDets { get; set; }
        public virtual DbSet<TransTrasEnc> TransTrasEncs { get; set; }
        public virtual DbSet<TransTrasOp> TransTrasOps { get; set; }
        public virtual DbSet<TransUbicHhDet> TransUbicHhDets { get; set; }
        public virtual DbSet<TransUbicHhEnc> TransUbicHhEncs { get; set; }
        public virtual DbSet<TransUbicHhOp> TransUbicHhOps { get; set; }
        public virtual DbSet<TransUbicHhSe> TransUbicHhSes { get; set; }
        public virtual DbSet<TransUbicHhStock> TransUbicHhStocks { get; set; }
        public virtual DbSet<TransUbicTarima> TransUbicTarimas { get; set; }
        public virtual DbSet<TransaccionesLog> TransaccionesLogs { get; set; }
        public virtual DbSet<Turno> Turnos { get; set; }
        public virtual DbSet<UbicacionesPorRegla> UbicacionesPorReglas { get; set; }
        public virtual DbSet<UnidadMedidaConversion> UnidadMedidaConversions { get; set; }
        public virtual DbSet<UnidadMedidum> UnidadMedida { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<UsuarioBodega> UsuarioBodegas { get; set; }
        public virtual DbSet<VMotivoAnulacion> VMotivoAnulacions { get; set; }
        public virtual DbSet<VTransPedido> VTransPedidos { get; set; }
        public virtual DbSet<VersionWmsHh> VersionWmsHhs { get; set; }
        public virtual DbSet<VwAjuste> VwAjustes { get; set; }
        public virtual DbSet<VwBodega> VwBodegas { get; set; }
        public virtual DbSet<VwBodegaArea> VwBodegaAreas { get; set; }
        public virtual DbSet<VwBodegaMuelle> VwBodegaMuelles { get; set; }
        public virtual DbSet<VwBodegaSector> VwBodegaSectors { get; set; }
        public virtual DbSet<VwBodegaUbicacion> VwBodegaUbicacions { get; set; }
        public virtual DbSet<VwCalculoVencimiento> VwCalculoVencimientos { get; set; }
        public virtual DbSet<VwCambiosUbicacion> VwCambiosUbicacions { get; set; }
        public virtual DbSet<VwCliente> VwClientes { get; set; }
        public virtual DbSet<VwClienteDireccion> VwClienteDireccions { get; set; }
        public virtual DbSet<VwClienteTipo> VwClienteTipos { get; set; }
        public virtual DbSet<VwCodigoBarraOc> VwCodigoBarraOcs { get; set; }
        public virtual DbSet<VwComparativoNavWmsConCosto> VwComparativoNavWmsConCostos { get; set; }
        public virtual DbSet<VwConfiguracioninv> VwConfiguracioninvs { get; set; }
        public virtual DbSet<VwCuadrilla> VwCuadrillas { get; set; }
        public virtual DbSet<VwDespacho> VwDespachos { get; set; }
        public virtual DbSet<VwDespachoDetalle> VwDespachoDetalles { get; set; }
        public virtual DbSet<VwDespachoRep> VwDespachoReps { get; set; }
        public virtual DbSet<VwDespachoRepDet> VwDespachoRepDets { get; set; }
        public virtual DbSet<VwDespachoRepDetI> VwDespachoRepDetIs { get; set; }
        public virtual DbSet<VwDespachoRepRe> VwDespachoRepRes { get; set; }
        public virtual DbSet<VwDocConDiferencia> VwDocConDiferencias { get; set; }
        public virtual DbSet<VwEstadoEnviosNav> VwEstadoEnviosNavs { get; set; }
        public virtual DbSet<VwExistenciaValoresFiscale> VwExistenciaValoresFiscales { get; set; }
        public virtual DbSet<VwExistenciasPorNoDocumento> VwExistenciasPorNoDocumentos { get; set; }
        public virtual DbSet<VwExistenciasProductoCategorium> VwExistenciasProductoCategoria { get; set; }
        public virtual DbSet<VwFiscalCtasOrden> VwFiscalCtasOrdens { get; set; }
        public virtual DbSet<VwFiscalHistorico> VwFiscalHistoricos { get; set; }
        public virtual DbSet<VwFiscalMercaVencidum> VwFiscalMercaVencida { get; set; }
        public virtual DbSet<VwGetAllStockDetalleResuman> VwGetAllStockDetalleResumen { get; set; }
        public virtual DbSet<VwGetDetalleByIdRecepcionEnc> VwGetDetalleByIdRecepcionEncs { get; set; }
        public virtual DbSet<VwGetSinglePedido> VwGetSinglePedidos { get; set; }
        public virtual DbSet<VwHorarioLaboral> VwHorarioLaborals { get; set; }
        public virtual DbSet<VwHorarioLaboralDet> VwHorarioLaboralDets { get; set; }
        public virtual DbSet<VwHorarioLaboralEnc> VwHorarioLaboralEncs { get; set; }
        public virtual DbSet<VwINavAcuerdo> VwINavAcuerdos { get; set; }
        public virtual DbSet<VwImpresionPallet> VwImpresionPallets { get; set; }
        public virtual DbSet<VwImpresionPalletRec> VwImpresionPalletRecs { get; set; }
        public virtual DbSet<VwInvConteoOperador> VwInvConteoOperadors { get; set; }
        public virtual DbSet<VwInventarioPrgPorTipo> VwInventarioPrgPorTipos { get; set; }
        public virtual DbSet<VwMinimosMaximosPorPresentacion> VwMinimosMaximosPorPresentacions { get; set; }
        public virtual DbSet<VwMotivoAnulacion> VwMotivoAnulacions { get; set; }
        public virtual DbSet<VwMotivoAnulacionBodega> VwMotivoAnulacionBodegas { get; set; }
        public virtual DbSet<VwMotivoDevolucion> VwMotivoDevolucions { get; set; }
        public virtual DbSet<VwMovimiento> VwMovimientos { get; set; }
        public virtual DbSet<VwMovimientosDetalle> VwMovimientosDetalles { get; set; }
        public virtual DbSet<VwMovimientosN> VwMovimientosNs { get; set; }
        public virtual DbSet<VwMovimientosN1> VwMovimientosN1s { get; set; }
        public virtual DbSet<VwNavdetalleconfiguracion> VwNavdetalleconfiguracions { get; set; }
        public virtual DbSet<VwOcupacionBodega> VwOcupacionBodegas { get; set; }
        public virtual DbSet<VwOcupacionBodegaTramo> VwOcupacionBodegaTramos { get; set; }
        public virtual DbSet<VwOperador> VwOperadors { get; set; }
        public virtual DbSet<VwOperadorHorario> VwOperadorHorarios { get; set; }
        public virtual DbSet<VwOrdenCompra> VwOrdenCompras { get; set; }
        public virtual DbSet<VwOrdenCompraPreIngreso> VwOrdenCompraPreIngresos { get; set; }
        public virtual DbSet<VwPai> VwPais { get; set; }
        public virtual DbSet<VwPaisDepartamento> VwPaisDepartamentos { get; set; }
        public virtual DbSet<VwPaisRegion> VwPaisRegions { get; set; }
        public virtual DbSet<VwParametro> VwParametros { get; set; }
        public virtual DbSet<VwPeConDiferencia> VwPeConDiferencias { get; set; }
        public virtual DbSet<VwPedido> VwPedidos { get; set; }
        public virtual DbSet<VwPedidosDm> VwPedidosDms { get; set; }
        public virtual DbSet<VwPedidosList> VwPedidosLists { get; set; }
        public virtual DbSet<VwPicking> VwPickings { get; set; }
        public virtual DbSet<VwPickingDetByIdPickingEnc> VwPickingDetByIdPickingEncs { get; set; }
        public virtual DbSet<VwPickingUbicByIdPedidoDet> VwPickingUbicByIdPedidoDets { get; set; }
        public virtual DbSet<VwPickingUbicByIdPickingDet> VwPickingUbicByIdPickingDets { get; set; }
        public virtual DbSet<VwPickingUbicByIdPickingEnc> VwPickingUbicByIdPickingEncs { get; set; }
        public virtual DbSet<VwPickingUbicDespachadoByIdPedidoDet> VwPickingUbicDespachadoByIdPedidoDets { get; set; }
        public virtual DbSet<VwPickingUbicacion> VwPickingUbicacions { get; set; }
        public virtual DbSet<VwPresentacionTarima> VwPresentacionTarimas { get; set; }
        public virtual DbSet<VwProducto> VwProductos { get; set; }
        public virtual DbSet<VwProductoBodegaParametro> VwProductoBodegaParametros { get; set; }
        public virtual DbSet<VwProductoClasificacion> VwProductoClasificacions { get; set; }
        public virtual DbSet<VwProductoDimension> VwProductoDimensions { get; set; }
        public virtual DbSet<VwProductoEstado> VwProductoEstados { get; set; }
        public virtual DbSet<VwProductoEstadoUbic> VwProductoEstadoUbics { get; set; }
        public virtual DbSet<VwProductoEstadoUbicBodega> VwProductoEstadoUbicBodegas { get; set; }
        public virtual DbSet<VwProductoEstadoUbicBodegaAnt20210215> VwProductoEstadoUbicBodegaAnt20210215s { get; set; }
        public virtual DbSet<VwProductoEstadoUbicBodegaHh> VwProductoEstadoUbicBodegaHhs { get; set; }
        public virtual DbSet<VwProductoEstadoUbicDefecto> VwProductoEstadoUbicDefectos { get; set; }
        public virtual DbSet<VwProductoEstadoUbicacion> VwProductoEstadoUbicacions { get; set; }
        public virtual DbSet<VwProductoFamilium> VwProductoFamilia { get; set; }
        public virtual DbSet<VwProductoMarca> VwProductoMarcas { get; set; }
        public virtual DbSet<VwProductoOc> VwProductoOcs { get; set; }
        public virtual DbSet<VwProductoParametro> VwProductoParametros { get; set; }
        public virtual DbSet<VwProductoPresentacion> VwProductoPresentacions { get; set; }
        public virtual DbSet<VwProductoRellenado> VwProductoRellenados { get; set; }
        public virtual DbSet<VwProductoSi> VwProductoSis { get; set; }
        public virtual DbSet<VwProductoSustituto> VwProductoSustitutos { get; set; }
        public virtual DbSet<VwProductoTipo> VwProductoTipos { get; set; }
        public virtual DbSet<VwPropietario> VwPropietarios { get; set; }
        public virtual DbSet<VwPropietarioReglaRecepcion> VwPropietarioReglaRecepcions { get; set; }
        public virtual DbSet<VwProveedor> VwProveedors { get; set; }
        public virtual DbSet<VwProveedorBodega> VwProveedorBodegas { get; set; }
        public virtual DbSet<VwProximosVencimiento> VwProximosVencimientos { get; set; }
        public virtual DbSet<VwRecConOc> VwRecConOcs { get; set; }
        public virtual DbSet<VwRecConocFin> VwRecConocFins { get; set; }
        public virtual DbSet<VwRecOcMix> VwRecOcMixes { get; set; }
        public virtual DbSet<VwRecSinOc> VwRecSinOcs { get; set; }
        public virtual DbSet<VwRecepcion> VwRecepcions { get; set; }
        public virtual DbSet<VwRecepcionConOc> VwRecepcionConOcs { get; set; }
        public virtual DbSet<VwRecepcionCostoArancel> VwRecepcionCostoArancels { get; set; }
        public virtual DbSet<VwRecepcionDet> VwRecepcionDets { get; set; }
        public virtual DbSet<VwRecepcionForHhByIdBodega> VwRecepcionForHhByIdBodegas { get; set; }
        public virtual DbSet<VwRecepcionForHhByIdBodegaByOperador> VwRecepcionForHhByIdBodegaByOperadors { get; set; }
        public virtual DbSet<VwRecepcionSinOc> VwRecepcionSinOcs { get; set; }
        public virtual DbSet<VwRecepcionesEncOc> VwRecepcionesEncOcs { get; set; }
        public virtual DbSet<VwReporteDetalleStockDataSet> VwReporteDetalleStockDataSets { get; set; }
        public virtual DbSet<VwReporteRecepcion20190726> VwReporteRecepcion20190726s { get; set; }
        public virtual DbSet<VwReporteRecepcion20190727> VwReporteRecepcion20190727s { get; set; }
        public virtual DbSet<VwRevisionProducto> VwRevisionProductos { get; set; }
        public virtual DbSet<VwRevisionProducto1> VwRevisionProductos1 { get; set; }
        public virtual DbSet<VwRptMinimosMaximo> VwRptMinimosMaximos { get; set; }
        public virtual DbSet<VwRptMinimosMaximosV2> VwRptMinimosMaximosV2s { get; set; }
        public virtual DbSet<VwRptProductosProximosVencimiento> VwRptProductosProximosVencimientos { get; set; }
        public virtual DbSet<VwRptStock> VwRptStocks { get; set; }
        public virtual DbSet<VwServicio> VwServicios { get; set; }
        public virtual DbSet<VwStock> VwStocks { get; set; }
        public virtual DbSet<VwStockEnc> VwStockEncs { get; set; }
        public virtual DbSet<VwStockEspecifico> VwStockEspecificos { get; set; }
        public virtual DbSet<VwStockEstadosProducto> VwStockEstadosProductos { get; set; }
        public virtual DbSet<VwStockJornadum> VwStockJornada { get; set; }
        public virtual DbSet<VwStockPorProductoUbicacionCi> VwStockPorProductoUbicacionCis { get; set; }
        public virtual DbSet<VwStockPresentacione> VwStockPresentaciones { get; set; }
        //public virtual DbSet<VwStockRe> VwStockRes { get; set; }
        public virtual DbSet<VwStockRecep> VwStockReceps { get; set; }
        public virtual DbSet<VwStockResConsolidador> VwStockResConsolidadors { get; set; }
        public virtual DbSet<VwStockResPedido> VwStockResPedidos { get; set; }
        public virtual DbSet<VwStockResTipoProducto> VwStockResTipoProductos { get; set; }
        public virtual DbSet<VwStockResV1> VwStockResV1s { get; set; }
        public virtual DbSet<VwStockReservadoByIdPedidoEnc> VwStockReservadoByIdPedidoEncs { get; set; }
        public virtual DbSet<VwStockResuman> VwStockResumen { get; set; }
        public virtual DbSet<VwStockSerieParametro> VwStockSerieParametros { get; set; }
        public virtual DbSet<VwStockSp> VwStockSps { get; set; }
        public virtual DbSet<VwStockTransito> VwStockTransitos { get; set; }
        public virtual DbSet<VwTareasActivasHh> VwTareasActivasHhs { get; set; }
        public virtual DbSet<VwTareasHh> VwTareasHhs { get; set; }
        public virtual DbSet<VwTareasPickingHh> VwTareasPickingHhs { get; set; }
        public virtual DbSet<VwTarima> VwTarimas { get; set; }
        public virtual DbSet<VwTarimasUsadasEnTransaccion> VwTarimasUsadasEnTransaccions { get; set; }
        public virtual DbSet<VwTiempoCliente> VwTiempoClientes { get; set; }
        public virtual DbSet<VwTmsTikcet> VwTmsTikcets { get; set; }
        public virtual DbSet<VwTransInvConteo> VwTransInvConteos { get; set; }
        public virtual DbSet<VwTransInvStock> VwTransInvStocks { get; set; }
        public virtual DbSet<VwTransOcDet> VwTransOcDets { get; set; }
        public virtual DbSet<VwTransServicio> VwTransServicios { get; set; }
        public virtual DbSet<VwTransUbicHhDet> VwTransUbicHhDets { get; set; }
        public virtual DbSet<VwTransUbicacionHhEnc> VwTransUbicacionHhEncs { get; set; }
        public virtual DbSet<VwUbicacionPicking> VwUbicacionPickings { get; set; }
        public virtual DbSet<VwUbicacionesPicking> VwUbicacionesPickings { get; set; }
        public virtual DbSet<VwUbicacionesPorRegla> VwUbicacionesPorReglas { get; set; }
        public virtual DbSet<VwUbicacionesTramoDisponible> VwUbicacionesTramoDisponibles { get; set; }
        public virtual DbSet<VwUnidadMedidum> VwUnidadMedida { get; set; }
        public virtual DbSet<VwVerificacion> VwVerificacions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<AjusteMotivo>(entity =>
            {
                entity.HasKey(e => e.Idmotivoajuste)
                    .HasName("PK_ajuste_momtivo");

                entity.ToTable("ajuste_motivo");

                entity.Property(e => e.Idmotivoajuste)
                    .ValueGeneratedNever()
                    .HasColumnName("idmotivoajuste");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.Sistema)
                    .HasColumnName("sistema")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<AjusteTipo>(entity =>
            {
                entity.HasKey(e => e.Idtipoajuste)
                    .HasName("PK_tipo_ajuste");

                entity.ToTable("ajuste_tipo");

                entity.Property(e => e.Idtipoajuste)
                    .ValueGeneratedNever()
                    .HasColumnName("idtipoajuste");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.ModificaCantidad).HasColumnName("modifica_cantidad");

                entity.Property(e => e.ModificaLote).HasColumnName("modifica_lote");

                entity.Property(e => e.ModificaPeso).HasColumnName("modifica_peso");

                entity.Property(e => e.MomdificaVencimiento).HasColumnName("momdifica_vencimiento");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.UniqueId);

                entity.Property(e => e.UniqueId).HasColumnName("UniqueID");

                entity.Property(e => e.EndDate).HasColumnType("smalldatetime");

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.ResourceId).HasColumnName("ResourceID");

                entity.Property(e => e.ResourceIds).HasColumnName("ResourceIDs");

                entity.Property(e => e.StartDate).HasColumnType("smalldatetime");

                entity.Property(e => e.Subject).HasMaxLength(50);
            });

            modelBuilder.Entity<Arancel>(entity =>
            {
                entity.HasKey(e => e.IdArancel)
                    .HasName("PK_Arancel");

                entity.ToTable("arancel");

                entity.Property(e => e.IdArancel).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(150)
                    .HasColumnName("nombre");

                entity.Property(e => e.Porcentaje).HasColumnName("porcentaje");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<Bodega>(entity =>
            {
                entity.HasKey(e => e.IdBodega)
                    .HasName("PK_bodega_1");

                entity.ToTable("bodega");

                entity.Property(e => e.IdBodega).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.BloquearLpHh)
                    .HasColumnName("bloquear_lp_hh")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CambioUbicacionAuto).HasColumnName("cambio_ubicacion_auto");

                entity.Property(e => e.CapturaEstibaIngreso).HasColumnName("captura_estiba_ingreso");

                entity.Property(e => e.CapturaPalletNoEstandar).HasColumnName("captura_pallet_no_estandar");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(150)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.CodigoBodegaErp)
                    .HasMaxLength(25)
                    .HasColumnName("codigo_bodega_erp");

                entity.Property(e => e.ControlBanderasCliente)
                    .HasColumnName("control_banderas_cliente")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ControlTarifaServicios).HasColumnName("control_tarifa_servicios");

                entity.Property(e => e.CoordenadaX)
                    .HasMaxLength(50)
                    .HasColumnName("coordenada_x");

                entity.Property(e => e.CoordenadaY)
                    .HasMaxLength(50)
                    .HasColumnName("coordenada_y");

                entity.Property(e => e.CuentaEgresoMercancias)
                    .HasMaxLength(50)
                    .HasColumnName("Cuenta_Egreso_Mercancias")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CuentaIngresoMercancias)
                    .HasMaxLength(50)
                    .HasColumnName("Cuenta_Ingreso_Mercancias")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .HasColumnName("direccion");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Encargado)
                    .HasMaxLength(50)
                    .HasColumnName("encargado");

                entity.Property(e => e.EsBodegaFiscal)
                    .HasColumnName("es_bodega_fiscal")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.HabilitarIngresoConsolidado)
                    .HasColumnName("habilitar_ingreso_consolidado")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IdMotivoUbicReabasto).HasColumnName("Id_Motivo_Ubic_Reabasto");

                entity.Property(e => e.IdProductoEstadoNe).HasColumnName("IdProductoEstadoNE");

                entity.Property(e => e.IdTipoTransaccion).HasMaxLength(50);

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreComercial)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_comercial");

                entity.Property(e => e.NotificacionVoz).HasColumnName("notificacion_voz");

                entity.Property(e => e.OperadorDefectoEnDocumentoIngreso)
                    .HasColumnName("operador_defecto_en_documento_ingreso")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PermitirVerificacionConsolidada)
                    .HasColumnName("Permitir_Verificacion_Consolidada")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RechazarPedidoPorStock).HasColumnName("rechazar_pedido_por_stock");

                entity.Property(e => e.ReservarStocksPorLinea).HasColumnName("reservar_stocks_por_linea");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("telefono");

                entity.Property(e => e.UbicDespacho)
                    .HasMaxLength(25)
                    .HasColumnName("ubic_despacho");

                entity.Property(e => e.UbicMerma)
                    .HasMaxLength(50)
                    .HasColumnName("ubic_merma");

                entity.Property(e => e.UbicPicking)
                    .HasMaxLength(25)
                    .HasColumnName("ubic_picking");

                entity.Property(e => e.UbicProductoNe).HasColumnName("ubic_producto_ne");

                entity.Property(e => e.UbicRecepcion)
                    .HasMaxLength(25)
                    .HasColumnName("ubic_recepcion");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");

                entity.Property(e => e.ValorPorcentajeIva)
                    .HasColumnName("valor_porcentaje_iva")
                    .HasDefaultValueSql("((12))");

                entity.Property(e => e.Zoom)
                    .HasColumnName("zoom")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Bodegas)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK_bodega_empresa");

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.Bodegas)
                    .HasForeignKey(d => d.IdPais)
                    .HasConstraintName("FK_bodega_paises");
            });

            modelBuilder.Entity<BodegaArea>(entity =>
            {
                entity.HasKey(e => new { e.IdArea, e.IdBodega })
                    .HasName("PK_BodegaAreaId");

                entity.ToTable("bodega_area");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.MargenDerecho).HasColumnName("margen_derecho");

                entity.Property(e => e.MargenInferior).HasColumnName("margen_inferior");

                entity.Property(e => e.MargenIzquierdo).HasColumnName("margen_izquierdo");

                entity.Property(e => e.MargenSuperior).HasColumnName("margen_superior");

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.BodegaAreas)
                    .HasForeignKey(d => d.IdBodega)
                    .HasConstraintName("FK_bodega_area_bodega");
            });

            modelBuilder.Entity<BodegaMonitorParametro>(entity =>
            {
                entity.HasKey(e => e.IdMonitor);

                entity.ToTable("bodega_monitor_parametro");

                entity.Property(e => e.IdMonitor).ValueGeneratedNever();

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.TiempoActualizacion)
                    .HasDefaultValueSql("((0))")
                    .HasComment("tiempo en segundos");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.BodegaMonitorParametros)
                    .HasForeignKey(d => d.IdBodega)
                    .HasConstraintName("FK_bodega_monitor_parametro_bodega");
            });

            modelBuilder.Entity<BodegaMuelle>(entity =>
            {
                entity.HasKey(e => e.IdMuelle)
                    .HasName("PK_bodega_muelles_1");

                entity.ToTable("bodega_muelles");

                entity.Property(e => e.IdMuelle).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_barra")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Color).HasColumnName("color");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Imagen)
                    .HasColumnType("image")
                    .HasColumnName("imagen");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.BodegaMuelles)
                    .HasForeignKey(d => d.IdBodega)
                    .HasConstraintName("FK_bodega_muelles_bodega");
            });

            modelBuilder.Entity<BodegaOrientacionPo>(entity =>
            {
                entity.HasKey(e => e.IdOrientacionPos);

                entity.ToTable("bodega_orientacion_pos");

                entity.Property(e => e.IdOrientacionPos).ValueGeneratedNever();

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<BodegaParametro>(entity =>
            {
                entity.HasKey(e => e.IdParametroBodega);

                entity.ToTable("bodega_parametros");

                entity.Property(e => e.IdParametroBodega).ValueGeneratedNever();

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<BodegaSector>(entity =>
            {
                entity.HasKey(e => new { e.IdSector, e.IdBodega })
                    .HasName("PK_BodegaSectorId");

                entity.ToTable("bodega_sector");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Horizontal)
                    .HasColumnName("horizontal")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.MargenDerecho).HasColumnName("margen_derecho");

                entity.Property(e => e.MargenInferior).HasColumnName("margen_inferior");

                entity.Property(e => e.MargenIzquierdo).HasColumnName("margen_izquierdo");

                entity.Property(e => e.MargenSuperior).HasColumnName("margen_superior");

                entity.Property(e => e.PosX)
                    .HasColumnName("pos_x")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PosY)
                    .HasColumnName("pos_y")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.BodegaSectors)
                    .HasForeignKey(d => new { d.IdArea, d.IdBodega })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bodega_sector_bodega_area");
            });

            modelBuilder.Entity<BodegaTramo>(entity =>
            {
                entity.HasKey(e => new { e.IdTramo, e.IdBodega })
                    .HasName("PK_BodegaTramoId");

                entity.ToTable("bodega_tramo");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.EsRack)
                    .HasColumnName("es_rack")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Horizontal).HasDefaultValueSql("((0))");

                entity.Property(e => e.IdTipoProductoDefault)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Indica el tipo de producto que el tramo puede tener por default.");

                entity.Property(e => e.IndiceX).HasColumnName("Indice_x");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.MargenDerecho).HasColumnName("margen_derecho");

                entity.Property(e => e.MargenInferior).HasColumnName("margen_inferior");

                entity.Property(e => e.MargenIzquierdo).HasColumnName("margen_izquierdo");

                entity.Property(e => e.MargenSuperior).HasColumnName("margen_superior");

                entity.Property(e => e.Orientacion).HasComment("Indica si está a la izquierda 0 o a la derecha 1");

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.BodegaTramos)
                    .HasForeignKey(d => new { d.IdSector, d.IdBodega })
                    .HasConstraintName("FK_bodega_tramo_bodega_sector");
            });

            modelBuilder.Entity<BodegaUbicacion>(entity =>
            {
                entity.HasKey(e => new { e.IdUbicacion, e.IdBodega })
                    .HasName("PK_BodegaUbicacionId");

                entity.ToTable("bodega_ubicacion");

                entity.HasIndex(e => e.IdUbicacion, "IX_bodega_ubicacion");

                entity.HasIndex(e => new { e.IdBodega, e.IdSector }, "NCLI_Bodega_Ubicacion_20191210_EJC");

                entity.HasIndex(e => new { e.IdTramo, e.IdBodega, e.IdArea, e.IdSector }, "NCLI_Bodega_Ubicacion_20200204_EJC_A");

                entity.HasIndex(e => new { e.IdTramo, e.IdBodega, e.IdSector }, "NCLI_bodega_ubicacion_20200204_EJC");

                entity.Property(e => e.AceptaPallet)
                    .HasColumnName("acepta_pallet")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.Bloqueada)
                    .HasColumnName("bloqueada")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.CodigoBarra2)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_barra2");

                entity.Property(e => e.Dañado)
                    .HasColumnName("dañado")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IdIndiceRotacion).HasDefaultValueSql("((0))");

                entity.Property(e => e.IdTipoRotacion).HasDefaultValueSql("((1))");

                entity.Property(e => e.IndiceX)
                    .HasColumnName("indice_x")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.MargenDerecho)
                    .HasColumnName("margen_derecho")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MargenInferior)
                    .HasColumnName("margen_inferior")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MargenIzquierdo)
                    .HasColumnName("margen_izquierdo")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MargenSuperior)
                    .HasColumnName("margen_superior")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Nivel)
                    .HasColumnName("nivel")
                    .HasComment("Coordenada Y");

                entity.Property(e => e.OrientacionPos)
                    .HasMaxLength(50)
                    .HasColumnName("orientacion_pos")
                    .HasComment("Indica la orientación de la posición dentro del rack, puede ser enfrente izquierda, enfrente derecha (FD), atras izquierda (BI) siglas en ingles");

                entity.Property(e => e.Sistema)
                    .HasColumnName("sistema")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UbicacionDespacho)
                    .HasColumnName("ubicacion_despacho")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UbicacionMerma)
                    .HasColumnName("ubicacion_merma")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UbicacionNe)
                    .HasColumnName("ubicacion_ne")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UbicacionPicking)
                    .HasColumnName("ubicacion_picking")
                    .HasDefaultValueSql("((0))")
                    .HasComment("Indica si la ubicación puede ser utilizada como zona de picking");

                entity.Property(e => e.UbicacionRecepcion)
                    .HasColumnName("ubicacion_recepcion")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UbicacionVirtual)
                    .HasColumnName("ubicacion_virtual")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdTipoRotacionNavigation)
                    .WithMany(p => p.BodegaUbicacions)
                    .HasForeignKey(d => d.IdTipoRotacion)
                    .HasConstraintName("FK_bodega_ubicacion_tipo_rotacion");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.BodegaUbicacions)
                    .HasForeignKey(d => new { d.IdTramo, d.IdBodega })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bodega_ubicacion_bodega_tramo");
            });

            modelBuilder.Entity<Camara>(entity =>
            {
                entity.HasKey(e => e.IdCamara);

                entity.ToTable("camara");

                entity.Property(e => e.IdCamara).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Ip)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Modelo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("modelo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Serie)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("serie");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<Cbcatalogoproducto>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("cbcatalogoproductos");

                entity.Property(e => e.CodCentro)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("cod_centro");

                entity.Property(e => e.CodCuentaDifCxc)
                    .HasMaxLength(23)
                    .IsUnicode(false)
                    .HasColumnName("cod_cuenta_dif_cxc");

                entity.Property(e => e.CodCuentaDifPasdif)
                    .HasMaxLength(23)
                    .IsUnicode(false)
                    .HasColumnName("cod_cuenta_dif_pasdif");

                entity.Property(e => e.CodCuentapasivodiferido)
                    .IsRequired()
                    .HasMaxLength(23)
                    .IsUnicode(false)
                    .HasColumnName("cod_cuentapasivodiferido");

                entity.Property(e => e.CodCuentapasivodiferidoMe)
                    .HasMaxLength(23)
                    .IsUnicode(false)
                    .HasColumnName("cod_cuentapasivodiferido_me");

                entity.Property(e => e.CodCuentaproducto)
                    .IsRequired()
                    .HasMaxLength(23)
                    .IsUnicode(false)
                    .HasColumnName("cod_cuentaproducto");

                entity.Property(e => e.CodCuentaxcobrar)
                    .IsRequired()
                    .HasMaxLength(23)
                    .IsUnicode(false)
                    .HasColumnName("cod_cuentaxcobrar");

                entity.Property(e => e.CodCuentaxcobrarMe)
                    .HasMaxLength(23)
                    .IsUnicode(false)
                    .HasColumnName("cod_cuentaxcobrar_me");

                entity.Property(e => e.CodEmpresa).HasColumnName("cod_empresa");

                entity.Property(e => e.CodigoRubro).HasColumnName("codigo_rubro");

                entity.Property(e => e.Codigoproducto)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigoproducto");

                entity.Property(e => e.Control).HasColumnName("control");

                entity.Property(e => e.CorreCbcesantes).HasColumnName("corre_cbcesantes");

                entity.Property(e => e.Correlativo).HasColumnName("correlativo");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength(true);

                entity.Property(e => e.Fechamov)
                    .HasColumnType("datetime")
                    .HasColumnName("fechamov");

                entity.Property(e => e.Montominimo)
                    .HasColumnType("money")
                    .HasColumnName("montominimo");

                entity.Property(e => e.Movimiento).HasColumnName("movimiento");

                entity.Property(e => e.Nemonico)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("nemonico");

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("usuario");
            });

            modelBuilder.Entity<CealsaVwacuerdocomercialdet>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("cealsa_vwacuerdocomercialdet");

                entity.Property(e => e.CodigoCliente).HasColumnName("codigo_cliente");

                entity.Property(e => e.CodigoProducto)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.CorreCbcatalogoproductos).HasColumnName("corre_cbcatalogoproductos");

                entity.Property(e => e.CorreCbmaeacuerdosservicios).HasColumnName("corre_cbmaeacuerdosservicios");

                entity.Property(e => e.Correlativo).HasColumnName("correlativo");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.DiasEventos).HasColumnName("dias_eventos");

                entity.Property(e => e.Nemonico)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("nemonico");

                entity.Property(e => e.NumeroUnidades)
                    .HasColumnType("numeric(15, 5)")
                    .HasColumnName("numero_unidades");

                entity.Property(e => e.Servicio)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("servicio");
            });

            modelBuilder.Entity<CealsaVwacuerdocomercialenc>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("cealsa_vwacuerdocomercialenc");

                entity.Property(e => e.Codacuerdo).HasColumnName("codacuerdo");

                entity.Property(e => e.Codcliente).HasColumnName("codcliente");

                entity.Property(e => e.Codmoneda).HasColumnName("codmoneda");

                entity.Property(e => e.Descrip)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descrip");

                entity.Property(e => e.Moneda)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("moneda");

                entity.Property(e => e.Tipocobro)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("tipocobro")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<CealsaVwcliente>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("cealsa_vwclientes");

                entity.Property(e => e.CodigoCliente).HasColumnName("codigo_cliente");

                entity.Property(e => e.NombreCliente)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("nombre_cliente");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.ToTable("cliente");

                entity.HasIndex(e => e.Codigo, "IX_CodigoCliente")
                    .IsUnique();

                entity.Property(e => e.IdCliente).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(150)
                    .HasColumnName("codigo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ControlUltimoLote)
                    .HasColumnName("control_ultimo_lote")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(150)
                    .HasColumnName("correo_electronico")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.DespacharLotesCompletos)
                    .HasColumnName("despachar_lotes_completos")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .HasColumnName("direccion")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EsBodegaRecepcion)
                    .HasColumnName("es_bodega_recepcion")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EsBodegaTraslado)
                    .HasColumnName("es_bodega_traslado")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Idubicacionvirtual).HasColumnName("idubicacionvirtual");

                entity.Property(e => e.Nit)
                    .HasMaxLength(125)
                    .HasColumnName("nit")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreComercial)
                    .HasMaxLength(150)
                    .HasColumnName("nombre_comercial")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreContacto)
                    .HasMaxLength(150)
                    .HasColumnName("nombre_contacto")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.RealizaManufactura)
                    .HasColumnName("realiza_manufactura")
                    .HasComment("Este campo servirá para filtrar los clientes a los que se les puede hacer despacho en el módulo de manufactura.");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(25)
                    .HasColumnName("referencia");

                entity.Property(e => e.Sistema)
                    .HasColumnName("sistema")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(125)
                    .HasColumnName("telefono")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_empresa");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdPropietario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_propietarios");

                entity.HasOne(d => d.IdTipoClienteNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdTipoCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_cliente_tipo");
            });

            modelBuilder.Entity<ClienteBodega>(entity =>
            {
                entity.HasKey(e => e.IdClienteBodega);

                entity.ToTable("cliente_bodega");

                entity.Property(e => e.IdClienteBodega).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.ClienteBodegas)
                    .HasForeignKey(d => d.IdBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_bodega_bodega");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.ClienteBodegas)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_bodega_cliente");
            });

            modelBuilder.Entity<ClienteDireccion>(entity =>
            {
                entity.HasKey(e => new { e.IdDireccion, e.IdCliente });

                entity.ToTable("cliente_direccion");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Avenida).HasMaxLength(50);

                entity.Property(e => e.Calle).HasMaxLength(50);

                entity.Property(e => e.CoordenadaX)
                    .HasMaxLength(50)
                    .HasColumnName("coordenada_x");

                entity.Property(e => e.CoordenadaY)
                    .HasMaxLength(50)
                    .HasColumnName("coordenada_y");

                entity.Property(e => e.Direccion).HasMaxLength(250);

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Local).HasDefaultValueSql("((1))");

                entity.Property(e => e.NoCasa)
                    .HasMaxLength(50)
                    .HasColumnName("No_Casa");

                entity.Property(e => e.Referencia).HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Zona).HasMaxLength(50);

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.ClienteDireccions)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("FK_cliente_direccion_pais_municipio");

                entity.HasOne(d => d.IdRegionNavigation)
                    .WithMany(p => p.ClienteDireccions)
                    .HasForeignKey(d => d.IdRegion)
                    .HasConstraintName("FK_cliente_direccion_pais_region");
            });

            modelBuilder.Entity<ClienteTiempo>(entity =>
            {
                entity.HasKey(e => e.IdTiempoCliente)
                    .HasName("PK_cliente_tiempos_1");

                entity.ToTable("cliente_tiempos");

                entity.Property(e => e.IdTiempoCliente).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.DiasExterior).HasColumnName("Dias_Exterior");

                entity.Property(e => e.DiasLocal).HasColumnName("Dias_Local");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdClasificacionNavigation)
                    .WithMany(p => p.ClienteTiempos)
                    .HasForeignKey(d => d.IdClasificacion)
                    .HasConstraintName("FK_cliente_tiempos_producto_clasificacion");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.ClienteTiempos)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_tiempos_cliente");

                entity.HasOne(d => d.IdFamiliaNavigation)
                    .WithMany(p => p.ClienteTiempos)
                    .HasForeignKey(d => d.IdFamilia)
                    .HasConstraintName("FK_cliente_tiempos_producto_familia");
            });

            modelBuilder.Entity<ClienteTipo>(entity =>
            {
                entity.HasKey(e => e.IdTipoCliente);

                entity.ToTable("cliente_tipo");

                entity.Property(e => e.IdTipoCliente).ValueGeneratedNever();

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.NombreTipoCliente)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.ClienteTipos)
                    .HasForeignKey(d => d.IdPropietario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_tipo_propietarios");
            });

            modelBuilder.Entity<CodBarraClc>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("cod_barra_clc");

                entity.Property(e => e.CodBarra2)
                    .HasMaxLength(109)
                    .HasColumnName("cod_barra2");

                entity.Property(e => e.Idubicacion).HasColumnName("idubicacion");
            });

            modelBuilder.Entity<ConfiguracionBarraPallet>(entity =>
            {
                entity.HasKey(e => e.IdConfiguracionPallet);

                entity.ToTable("configuracion_barra_pallet");

                entity.Property(e => e.IdConfiguracionPallet).ValueGeneratedNever();

                entity.Property(e => e.CodigoNumerico).HasComment("Si verdadero, entonces se hace un val del código en la HH de lo contrario se tomarán los ceros a la izquierda como parte del código");

                entity.Property(e => e.IdentificadorInicio)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.LongLp).HasColumnName("LongLP");
            });

            modelBuilder.Entity<Consolidador>(entity =>
            {
                entity.HasKey(e => e.Idconsolidador);

                entity.ToTable("consolidador");

                entity.Property(e => e.Idconsolidador).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .HasColumnName("direccion");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nit)
                    .HasMaxLength(50)
                    .HasColumnName("nit");

                entity.Property(e => e.NomComercial)
                    .HasMaxLength(100)
                    .HasColumnName("nom_comercial");

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(50)
                    .HasColumnName("razon_social");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("telefono");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<Contenedor>(entity =>
            {
                entity.HasKey(e => e.IdContenedor);

                entity.ToTable("contenedor");

                entity.Property(e => e.IdContenedor).ValueGeneratedNever();

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<CuadrillaDetMontacarga>(entity =>
            {
                entity.HasKey(e => e.IdCuadrillaDetMontaCarga);

                entity.ToTable("cuadrilla_det_montacarga");

                entity.Property(e => e.IdCuadrillaDetMontaCarga).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<CuadrillaDetOperador>(entity =>
            {
                entity.HasKey(e => e.IdCuadrillaDet)
                    .HasName("PK_cuadrilla_det");

                entity.ToTable("cuadrilla_det_operador");

                entity.Property(e => e.IdCuadrillaDet).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<CuadrillaEnc>(entity =>
            {
                entity.HasKey(e => e.IdCuadrillaEnc);

                entity.ToTable("cuadrilla_enc");

                entity.Property(e => e.IdCuadrillaEnc).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<CuadrillaTipo>(entity =>
            {
                entity.HasKey(e => e.IdTipoCuadrilla);

                entity.ToTable("cuadrilla_tipo");

                entity.Property(e => e.IdTipoCuadrilla).ValueGeneratedNever();

                entity.Property(e => e.EsInventario).HasColumnName("es_inventario");

                entity.Property(e => e.EsPicking).HasColumnName("es_picking");

                entity.Property(e => e.EsRecepcion).HasColumnName("es_recepcion");

                entity.Property(e => e.EsTransito).HasColumnName("es_transito");

                entity.Property(e => e.EsUbicacion).HasColumnName("es_ubicacion");

                entity.Property(e => e.EsVerificacion).HasColumnName("es_verificacion");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<DhOcupacionBodega>(entity =>
            {
                entity.HasKey(e => e.IdOcupacionBodega)
                    .HasName("PK_ocupacion_bodega_hist");

                entity.ToTable("dh_ocupacion_bodega");

                entity.Property(e => e.IdOcupacionBodega).ValueGeneratedNever();

                entity.Property(e => e.CantUbicacionesOcupadas).HasColumnName("cant_ubicaciones_ocupadas");

                entity.Property(e => e.CantUbicacionesVacias).HasColumnName("cant_ubicaciones_vacias");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa);

                entity.ToTable("empresa");

                entity.Property(e => e.IdEmpresa).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AnulacionesPorSupervisor)
                    .HasColumnName("anulaciones_por_supervisor")
                    .HasComment("Determina si para cualquier anulación en el sistema, se debe ingresar el código de un usuario administrador");

                entity.Property(e => e.CantidadDecimalesCalculo)
                    .HasColumnName("cantidad_decimales_calculo")
                    .HasDefaultValueSql("((6))");

                entity.Property(e => e.CantidadDecimalesDespliegue)
                    .HasColumnName("cantidad_decimales_despliegue")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.Clave)
                    .HasMaxLength(50)
                    .HasColumnName("clave");

                entity.Property(e => e.ClienteRapido)
                    .HasColumnName("clienteRapido")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoAutomatico).HasColumnName("codigo_automatico");

                entity.Property(e => e.ControlPresentaciones)
                    .HasColumnName("control_presentaciones")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CorrCodBarra).HasColumnName("corr_cod_barra");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .HasColumnName("direccion")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Duracionclave).HasColumnName("duracionclave");

                entity.Property(e => e.Duracionclavetemporal).HasColumnName("duracionclavetemporal");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.GenerarStockJornada)
                    .IsRequired()
                    .HasColumnName("generar_stock_jornada")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.HoraCorteJornadaSistema)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_corte_jornada_sistema")
                    .HasDefaultValueSql("('23:59:59')");

                entity.Property(e => e.Imagen)
                    .HasColumnType("image")
                    .HasColumnName("imagen");

                entity.Property(e => e.Intento).HasColumnName("intento");

                entity.Property(e => e.MinutosTimerJornadaSistema)
                    .HasColumnName("minutos_timer_jornada_sistema")
                    .HasDefaultValueSql("((30))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.OperadorLogistico).HasColumnName("operador_logistico");

                entity.Property(e => e.PathPrinter)
                    .HasMaxLength(500)
                    .HasColumnName("path_printer")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.PoliticaContraseñas)
                    .HasColumnName("politica_contraseñas")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PuertoEscaner).HasColumnName("puerto_escaner");

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(50)
                    .HasColumnName("razon_social")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Representante)
                    .HasMaxLength(50)
                    .HasColumnName("representante")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("telefono")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<EmpresaTransporte>(entity =>
            {
                entity.HasKey(e => e.IdEmpresaTransporte)
                    .HasName("PK_empresa_transporte_1");

                entity.ToTable("empresa_transporte");

                entity.Property(e => e.IdEmpresaTransporte).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.EmpresaTransportes)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_empresa_transporte_empresa");
            });

            modelBuilder.Entity<EmpresaTransporteBodega>(entity =>
            {
                entity.HasKey(e => e.IdAsignacion);

                entity.ToTable("empresa_transporte_bodega");

                entity.Property(e => e.IdAsignacion).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.EmpresaTransporteBodegas)
                    .HasForeignKey(d => d.IdBodega)
                    .HasConstraintName("FK_empresa_transporte_bodega_bodega");

                entity.HasOne(d => d.IdEmpresaTransporteNavigation)
                    .WithMany(p => p.EmpresaTransporteBodegas)
                    .HasForeignKey(d => d.IdEmpresaTransporte)
                    .HasConstraintName("FK_empresa_transporte_bodega_empresa_transporte");
            });

            modelBuilder.Entity<EmpresaTransportePiloto>(entity =>
            {
                entity.HasKey(e => e.IdPiloto)
                    .HasName("PK_empresa_transporte_pilotos_1");

                entity.ToTable("empresa_transporte_pilotos");

                entity.Property(e => e.IdPiloto).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(150)
                    .HasColumnName("apellidos");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(150)
                    .HasColumnName("correo_electronico");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .HasColumnName("direccion");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaExpiracionCarnet)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_expiracion_carnet");

                entity.Property(e => e.FechaExpiracionLicencia)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_expiracion_licencia");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_nacimiento");

                entity.Property(e => e.FechaSalida)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_salida");

                entity.Property(e => e.Foto)
                    .HasColumnType("image")
                    .HasColumnName("foto");

                entity.Property(e => e.IdTipoLicencia).HasMaxLength(50);

                entity.Property(e => e.NoCarnet)
                    .HasMaxLength(50)
                    .HasColumnName("no_carnet");

                entity.Property(e => e.NoDpi)
                    .HasMaxLength(50)
                    .HasColumnName("no_dpi");

                entity.Property(e => e.NoLicencia)
                    .HasMaxLength(50)
                    .HasColumnName("no_licencia");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(150)
                    .HasColumnName("nombres");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("telefono");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdEmpresaTransporteNavigation)
                    .WithMany(p => p.EmpresaTransportePilotos)
                    .HasForeignKey(d => d.IdEmpresaTransporte)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_empresa_transporte_pilotos_empresa_transporte");
            });

            modelBuilder.Entity<EmpresaTransporteVehiculo>(entity =>
            {
                entity.HasKey(e => e.IdVehiculo)
                    .HasName("PK_empresa_transporte_vehiculos_1");

                entity.ToTable("empresa_transporte_vehiculos");

                entity.Property(e => e.IdVehiculo).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.EsContedor).HasColumnName("es_contedor");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.Marca)
                    .HasMaxLength(50)
                    .HasColumnName("marca")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Modelo)
                    .HasMaxLength(50)
                    .HasColumnName("modelo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Placa)
                    .HasMaxLength(20)
                    .HasColumnName("placa")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.PlacaComercial)
                    .HasMaxLength(50)
                    .HasColumnName("placa_comercial");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .HasColumnName("tipo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Volumen).HasColumnName("volumen");

                entity.HasOne(d => d.IdEmpresaTransporteNavigation)
                    .WithMany(p => p.EmpresaTransporteVehiculos)
                    .HasForeignKey(d => d.IdEmpresaTransporte)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_empresa_transporte_vehiculos_empresa_transporte");
            });

            modelBuilder.Entity<EstructuraGrupo>(entity =>
            {
                entity.HasKey(e => e.IdGrupo)
                    .HasName("PK_estructura_grupos");

                entity.ToTable("estructura_grupo");

                entity.Property(e => e.IdGrupo).ValueGeneratedNever();

                entity.Property(e => e.Agrupacion).HasColumnName("agrupacion");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.Cant).HasColumnName("cant");

                entity.Property(e => e.Largo)
                    .HasColumnName("largo")
                    .HasComment("Coordenada Y");

                entity.Property(e => e.Offset).HasColumnName("offset");

                entity.Property(e => e.Orient)
                    .HasColumnName("orient")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Palet).HasColumnName("palet");

                entity.Property(e => e.Pos).HasColumnName("pos");

                entity.Property(e => e.Tamano).HasColumnName("tamano");

                entity.HasOne(d => d.IdTramoNavigation)
                    .WithMany(p => p.EstructuraGrupos)
                    .HasForeignKey(d => d.IdTramo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_estructura_grupo_estructura_tramo");
            });

            modelBuilder.Entity<EstructuraTramo>(entity =>
            {
                entity.HasKey(e => e.IdTramo);

                entity.ToTable("estructura_tramo");

                entity.Property(e => e.IdTramo).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Horizontal).HasDefaultValueSql("((0))");

                entity.Property(e => e.IdTipoProductoDefault)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Indica el tipo de producto que el tramo puede tener por default.");

                entity.Property(e => e.Idarea).HasColumnName("idarea");

                entity.Property(e => e.Idbodega).HasColumnName("idbodega");

                entity.Property(e => e.IndiceX).HasColumnName("Indice_x");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.MargenDerecho).HasColumnName("margen_derecho");

                entity.Property(e => e.MargenInferior).HasColumnName("margen_inferior");

                entity.Property(e => e.MargenIzquierdo).HasColumnName("margen_izquierdo");

                entity.Property(e => e.MargenSuperior).HasColumnName("margen_superior");

                entity.Property(e => e.Orientacion).HasComment("Indica si está a la izquierda 0 o a la derecha 1");

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<EstructuraUbicacion>(entity =>
            {
                entity.HasKey(e => e.IdUbicacion);

                entity.ToTable("estructura_ubicacion");

                entity.Property(e => e.IdUbicacion).ValueGeneratedNever();

                entity.Property(e => e.AceptaPallet)
                    .HasColumnName("acepta_pallet")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.Bloqueada)
                    .HasColumnName("bloqueada")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(25)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.CodigoBarra2)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_barra2");

                entity.Property(e => e.Dañado)
                    .HasColumnName("dañado")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IdIndiceRotacion).HasDefaultValueSql("((0))");

                entity.Property(e => e.IdTipoRotacion).HasDefaultValueSql("((1))");

                entity.Property(e => e.Idarea).HasColumnName("idarea");

                entity.Property(e => e.Idbodega).HasColumnName("idbodega");

                entity.Property(e => e.Idsector).HasColumnName("idsector");

                entity.Property(e => e.IndiceX)
                    .HasColumnName("indice_x")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.MargenDerecho)
                    .HasColumnName("margen_derecho")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MargenInferior)
                    .HasColumnName("margen_inferior")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MargenIzquierdo)
                    .HasColumnName("margen_izquierdo")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MargenSuperior)
                    .HasColumnName("margen_superior")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Nivel)
                    .HasColumnName("nivel")
                    .HasComment("Coordenada Y");

                entity.Property(e => e.OrientacionPos)
                    .HasMaxLength(50)
                    .HasColumnName("orientacion_pos")
                    .HasComment("Indica la orientación de la posición dentro del rack, puede ser enfrente izquierda, enfrente derecha (FD), atras izquierda (BI) siglas en ingles");

                entity.Property(e => e.Sistema)
                    .HasColumnName("sistema")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UbicacionDespacho)
                    .HasColumnName("ubicacion_despacho")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UbicacionMerma)
                    .HasColumnName("ubicacion_merma")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UbicacionPicking)
                    .HasColumnName("ubicacion_picking")
                    .HasDefaultValueSql("((0))")
                    .HasComment("Indica si la ubicación puede ser utilizada como zona de picking");

                entity.Property(e => e.UbicacionRecepcion)
                    .HasColumnName("ubicacion_recepcion")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdTramoNavigation)
                    .WithMany(p => p.EstructuraUbicacions)
                    .HasForeignKey(d => d.IdTramo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_estructura_ubicacion_estructura_tramo");
            });

            modelBuilder.Entity<FontDet>(entity =>
            {
                entity.HasKey(e => e.IdFontDet);

                entity.ToTable("font_det");

                entity.Property(e => e.IdFontDet).ValueGeneratedNever();

                entity.Property(e => e.ColorFondo).HasMaxLength(50);

                entity.Property(e => e.ColorFont).HasMaxLength(50);

                entity.Property(e => e.Letra).HasMaxLength(50);
            });

            modelBuilder.Entity<FontEnc>(entity =>
            {
                entity.HasKey(e => e.IdFontEnc);

                entity.ToTable("font_enc");

                entity.Property(e => e.IdFontEnc).ValueGeneratedNever();

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<HorarioLaboralDet>(entity =>
            {
                entity.HasKey(e => e.IdHorarioLaboralDet);

                entity.ToTable("horario_laboral_det");

                entity.Property(e => e.IdHorarioLaboralDet).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Dia).HasColumnName("dia");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaBaja)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_baja");

                entity.Property(e => e.HoraFin)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin");

                entity.Property(e => e.HoraInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_inicio");

                entity.Property(e => e.HorasExtras).HasColumnName("horas_extras");

                entity.Property(e => e.MaximoMinHoraIngreso).HasColumnName("maximo_min_hora_ingreso");

                entity.Property(e => e.MaximoMinHoraSalida).HasColumnName("maximo_min_hora_salida");

                entity.Property(e => e.MinimoMinHoraIngreso).HasColumnName("minimo_min_hora_ingreso");

                entity.Property(e => e.MinimoMinHoraSalida).HasColumnName("minimo_min_hora_salida");

                entity.Property(e => e.TiempoRetrasoPermitido).HasColumnName("tiempo_retraso_permitido");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdHorarioLaboralEncNavigation)
                    .WithMany(p => p.HorarioLaboralDets)
                    .HasForeignKey(d => d.IdHorarioLaboralEnc)
                    .HasConstraintName("FK_horario_laboral_det_horario_laboral_enc");
            });

            modelBuilder.Entity<HorarioLaboralEnc>(entity =>
            {
                entity.HasKey(e => e.IdHorarioLaboralEnc);

                entity.ToTable("horario_laboral_enc");

                entity.Property(e => e.IdHorarioLaboralEnc).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(128)
                    .HasColumnName("nombre");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.HorarioLaboralEncs)
                    .HasForeignKey(d => d.IdBodega)
                    .HasConstraintName("FK_horario_laboral_enc_bodega");

                entity.HasOne(d => d.IdJornadaNavigation)
                    .WithMany(p => p.HorarioLaboralEncs)
                    .HasForeignKey(d => d.IdJornada)
                    .HasConstraintName("FK_horario_laboral_enc_jornada_laboral");

                entity.HasOne(d => d.IdTurnoNavigation)
                    .WithMany(p => p.HorarioLaboralEncs)
                    .HasForeignKey(d => d.IdTurno)
                    .HasConstraintName("FK_horario_laboral_enc_turno");
            });

            modelBuilder.Entity<INavAcuerdo>(entity =>
            {
                entity.HasKey(e => e.IdAcuerdo);

                entity.ToTable("i_nav_acuerdo");

                entity.Property(e => e.IdAcuerdo).ValueGeneratedNever();

                entity.Property(e => e.CodMoneda).HasColumnName("cod_moneda");

                entity.Property(e => e.CodigoAcuerdo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codigo_acuerdo");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("descripcion");

                entity.Property(e => e.NomMoneda)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("nom_moneda");

                entity.Property(e => e.TipoCobro)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("tipo_cobro");
            });

            modelBuilder.Entity<INavAcuerdoDet>(entity =>
            {
                entity.HasKey(e => e.IdAcuerdoDet)
                    .HasName("PK_i_nav_acuerdoDet");

                entity.ToTable("i_nav_acuerdo_det");

                entity.Property(e => e.IdAcuerdoDet).ValueGeneratedNever();

                entity.Property(e => e.CodMoneda).HasColumnName("cod_moneda");

                entity.Property(e => e.CodigoProducto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.CorreCatalogoproductos).HasColumnName("corre_catalogoproductos");

                entity.Property(e => e.CorreDetalleacuerdo).HasColumnName("corre_detalleacuerdo");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("estado");

                entity.Property(e => e.Nemonico)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("nemonico");

                entity.Property(e => e.NombreUnidad)
                    .HasMaxLength(100)
                    .HasColumnName("nombre_unidad");

                entity.Property(e => e.ProcesadoWms).HasColumnName("procesado_wms");

                entity.Property(e => e.Servicio)
                    .HasMaxLength(500)
                    .HasColumnName("servicio");

                entity.Property(e => e.UnidMedida).HasColumnName("unid_medida");
            });

            modelBuilder.Entity<INavAcuerdoEnc>(entity =>
            {
                entity.HasKey(e => e.IdAcuerdo);

                entity.ToTable("i_nav_acuerdo_enc");

                entity.Property(e => e.IdAcuerdo).ValueGeneratedNever();

                entity.Property(e => e.CodMoneda).HasColumnName("cod_moneda");

                entity.Property(e => e.CodigoAcuerdo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codigo_acuerdo");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("descripcion");

                entity.Property(e => e.NomMoneda)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("nom_moneda");

                entity.Property(e => e.ProcesadoWms).HasColumnName("procesado_wms");

                entity.Property(e => e.TipoCobro)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("tipo_cobro");
            });

            modelBuilder.Entity<INavAcuerdoProducto>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("i_nav_acuerdo_productos");

                entity.Property(e => e.CodCentro)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("cod_centro")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodCuentaDifCxc)
                    .HasMaxLength(23)
                    .IsUnicode(false)
                    .HasColumnName("cod_cuenta_dif_cxc")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodCuentaDifPasdif)
                    .HasMaxLength(23)
                    .IsUnicode(false)
                    .HasColumnName("cod_cuenta_dif_pasdif")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodCuentapasivodiferido)
                    .IsRequired()
                    .HasMaxLength(23)
                    .IsUnicode(false)
                    .HasColumnName("cod_cuentapasivodiferido")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodCuentapasivodiferidoMe)
                    .HasMaxLength(23)
                    .IsUnicode(false)
                    .HasColumnName("cod_cuentapasivodiferido_me")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodCuentaproducto)
                    .IsRequired()
                    .HasMaxLength(23)
                    .IsUnicode(false)
                    .HasColumnName("cod_cuentaproducto")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodCuentaxcobrar)
                    .IsRequired()
                    .HasMaxLength(23)
                    .IsUnicode(false)
                    .HasColumnName("cod_cuentaxcobrar")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodCuentaxcobrarMe)
                    .HasMaxLength(23)
                    .IsUnicode(false)
                    .HasColumnName("cod_cuentaxcobrar_me")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodEmpresa).HasColumnName("cod_empresa");

                entity.Property(e => e.CodigoRubro).HasColumnName("codigo_rubro");

                entity.Property(e => e.Codigoproducto)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigoproducto")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Control).HasColumnName("control");

                entity.Property(e => e.CorreCbcesantes).HasColumnName("corre_cbcesantes");

                entity.Property(e => e.Correlativo).HasColumnName("correlativo");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Fechamov)
                    .HasColumnType("datetime")
                    .HasColumnName("fechamov");

                entity.Property(e => e.Montominimo)
                    .HasColumnType("money")
                    .HasColumnName("montominimo");

                entity.Property(e => e.Movimiento).HasColumnName("movimiento");

                entity.Property(e => e.Nemonico)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("nemonico")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("usuario")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<INavBarrasPallet>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("i_nav_barras_pallet");

                entity.Property(e => e.BodegaDestino)
                    .HasMaxLength(50)
                    .HasColumnName("Bodega_Destino");

                entity.Property(e => e.BodegaOrigen)
                    .HasMaxLength(50)
                    .HasColumnName("Bodega_Origen");

                entity.Property(e => e.CajasPorCama).HasColumnName("Cajas_Por_Cama");

                entity.Property(e => e.CamasPorTarima).HasColumnName("Camas_Por_Tarima");

                entity.Property(e => e.CantidadPresentacion)
                    .HasColumnName("Cantidad_Presentacion")
                    .HasComment("Cantidad de unidades básicas contenidas dentro de la presentación");

                entity.Property(e => e.CantidadUmp).HasColumnName("Cantidad_UMP");

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(50)
                    .HasColumnName("Codigo_Barra");

                entity.Property(e => e.FechaAgregado)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Agregado");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Ingreso");

                entity.Property(e => e.FechaProduccion)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Produccion");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Vence");

                entity.Property(e => e.Lote).HasMaxLength(50);

                entity.Property(e => e.LoteNumerico).HasColumnName("Lote_Numerico");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.UmProducto)
                    .HasMaxLength(50)
                    .HasColumnName("UM_Producto");
            });

            modelBuilder.Entity<INavBodega>(entity =>
            {
                entity.HasKey(e => e.BodegaCode);

                entity.ToTable("i_nav_bodega");

                entity.Property(e => e.BodegaCode)
                    .HasMaxLength(50)
                    .HasColumnName("bodega_code");

                entity.Property(e => e.BodegaName)
                    .HasMaxLength(150)
                    .HasColumnName("bodega_name");
            });

            modelBuilder.Entity<INavCliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.ToTable("i_nav_cliente");

                entity.Property(e => e.IdCliente).ValueGeneratedNever();

                entity.Property(e => e.Adress).HasMaxLength(120);

                entity.Property(e => e.City).HasMaxLength(120);

                entity.Property(e => e.CodigoCliente)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codigo_cliente");

                entity.Property(e => e.ContactName).HasMaxLength(120);

                entity.Property(e => e.Country).HasMaxLength(120);

                entity.Property(e => e.LocationCode)
                    .HasMaxLength(120)
                    .HasColumnName("Location_Code");

                entity.Property(e => e.Name).HasMaxLength(120);

                entity.Property(e => e.Nit)
                    .HasMaxLength(20)
                    .HasColumnName("nit");

                entity.Property(e => e.No).HasMaxLength(50);

                entity.Property(e => e.NombreCliente)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasColumnName("nombre_cliente");

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(120)
                    .HasColumnName("Phone_No");

                entity.Property(e => e.ProcesadoWms).HasColumnName("procesado_wms");

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(120)
                    .HasColumnName("razon_social");

                entity.Property(e => e.SearchName)
                    .HasMaxLength(120)
                    .HasColumnName("Search_Name");

                entity.Property(e => e.VatRegistratrionNo)
                    .HasMaxLength(120)
                    .HasColumnName("VAT_Registratrion_No");
            });

            modelBuilder.Entity<INavConfigDet>(entity =>
            {
                entity.HasKey(e => e.Idnavconfigdet);

                entity.ToTable("i_nav_config_det");

                entity.Property(e => e.Idnavconfigdet)
                    .ValueGeneratedNever()
                    .HasColumnName("idnavconfigdet");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Dia).HasColumnName("dia");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Frecuencia).HasColumnName("frecuencia");

                entity.Property(e => e.Horafin)
                    .HasColumnType("datetime")
                    .HasColumnName("horafin");

                entity.Property(e => e.Horainicio)
                    .HasColumnType("datetime")
                    .HasColumnName("horainicio");

                entity.Property(e => e.Idnavconfigenc).HasColumnName("idnavconfigenc");

                entity.Property(e => e.Idnavent).HasColumnName("idnavent");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdnavconfigencNavigation)
                    .WithMany(p => p.INavConfigDets)
                    .HasForeignKey(d => d.Idnavconfigenc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_i_nav_config_det_i_nav_config_enc");

                entity.HasOne(d => d.IdnaventNavigation)
                    .WithMany(p => p.INavConfigDets)
                    .HasForeignKey(d => d.Idnavent)
                    .HasConstraintName("FK_i_nav_config_det_i_nav_ent");
            });

            modelBuilder.Entity<INavConfigEnc>(entity =>
            {
                entity.HasKey(e => e.Idnavconfigenc)
                    .HasName("PK_i_nav_config_enc_1");

                entity.ToTable("i_nav_config_enc");

                entity.Property(e => e.Idnavconfigenc)
                    .ValueGeneratedNever()
                    .HasColumnName("idnavconfigenc");

                entity.Property(e => e.CodigoProveedorProduccion)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_proveedor_produccion");

                entity.Property(e => e.ControlLote).HasColumnName("control_lote");

                entity.Property(e => e.ControlPeso)
                    .HasColumnName("control_peso")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ControlVencimiento).HasColumnName("control_vencimiento");

                entity.Property(e => e.ConvertirDecimalesAUmbas)
                    .HasColumnName("convertir_decimales_a_umbas")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CrearRecepcionDeCompraNav)
                    .HasColumnName("crear_recepcion_de_compra_nav")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CrearRecepcionDeTransferenciaNav)
                    .HasColumnName("crear_recepcion_de_transferencia_nav")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DespacharExistenciaParcial)
                    .HasColumnName("despachar_existencia_parcial")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.EquipararClienteConPropietarioEnDocSalida).HasColumnName("equiparar_cliente_con_propietario_en_doc_salida");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.GeneraLp).HasColumnName("genera_lp");

                entity.Property(e => e.GenerarPedidoIngresoBodegaDestino)
                    .HasColumnName("generar_pedido_ingreso_bodega_destino")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.GenerarRecepcionAutoBodegaDestino)
                    .HasColumnName("generar_recepcion_auto_bodega_destino")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IdAcuerdoEnc).HasDefaultValueSql("((0))");

                entity.Property(e => e.IdFamilia).HasColumnName("idFamilia");

                entity.Property(e => e.IdMarca).HasColumnName("idMarca");

                entity.Property(e => e.IdPropietario).HasColumnName("idPropietario");

                entity.Property(e => e.IdTipoDocumentoTransferenciasIngreso).HasDefaultValueSql("((3))");

                entity.Property(e => e.IdTipoEtiqueta).HasDefaultValueSql("((0))");

                entity.Property(e => e.IdTipoProducto).HasColumnName("idTipoProducto");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Idbodega).HasColumnName("idbodega");

                entity.Property(e => e.Idclasificacion).HasColumnName("idclasificacion");

                entity.Property(e => e.Idempresa).HasColumnName("idempresa");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreEjecutable)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_ejecutable");

                entity.Property(e => e.RechazarPedidoIncompleto)
                    .HasColumnName("rechazar_pedido_incompleto")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.INavConfigEncs)
                    .HasForeignKey(d => d.IdPropietario)
                    .HasConstraintName("FK_i_nav_config_enc_propietarios");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.INavConfigEncs)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_i_nav_config_enc_usuario");

                entity.HasOne(d => d.IdbodegaNavigation)
                    .WithMany(p => p.INavConfigEncs)
                    .HasForeignKey(d => d.Idbodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_i_nav_config_enc_bodega");

                entity.HasOne(d => d.IdempresaNavigation)
                    .WithMany(p => p.INavConfigEncs)
                    .HasForeignKey(d => d.Idempresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_i_nav_config_enc_empresa");
            });

            modelBuilder.Entity<INavConfigEnt>(entity =>
            {
                entity.HasKey(e => e.Idnavconfigent);

                entity.ToTable("i_nav_config_ent");

                entity.Property(e => e.Idnavconfigent)
                    .ValueGeneratedNever()
                    .HasColumnName("idnavconfigent");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Endpoint)
                    .HasMaxLength(256)
                    .HasColumnName("endpoint");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Idnavent).HasColumnName("idnavent");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod).HasColumnName("user_mod");

                entity.HasOne(d => d.IdnaventNavigation)
                    .WithMany(p => p.INavConfigEnts)
                    .HasForeignKey(d => d.Idnavent)
                    .HasConstraintName("FK_i_nav_config_ent_i_nav_ent");
            });

            modelBuilder.Entity<INavEjecucionDetError>(entity =>
            {
                entity.HasKey(e => e.Idejecuciondet);

                entity.ToTable("i_nav_ejecucion_det_error");

                entity.Property(e => e.Idejecuciondet)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idejecuciondet");

                entity.Property(e => e.Error)
                    .HasMaxLength(1000)
                    .HasColumnName("error");

                entity.Property(e => e.EsBodegaRecepcion)
                    .HasColumnName("es_bodega_recepcion")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Idejecucionenc).HasColumnName("idejecucionenc");

                entity.Property(e => e.Idnavconfigdet).HasColumnName("idnavconfigdet");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(1000)
                    .HasColumnName("referencia");
            });

            modelBuilder.Entity<INavEjecucionEnc>(entity =>
            {
                entity.HasKey(e => e.Idejecucionenc);

                entity.ToTable("i_nav_ejecucion_enc");

                entity.Property(e => e.Idejecucionenc)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idejecucionenc");

                entity.Property(e => e.Exitosa).HasColumnName("exitosa");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Idnavconfigenc).HasColumnName("idnavconfigenc");
            });

            modelBuilder.Entity<INavEjecucionRe>(entity =>
            {
                entity.HasKey(e => e.Idejecucionres);

                entity.ToTable("i_nav_ejecucion_res");

                entity.Property(e => e.Idejecucionres)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idejecucionres");

                entity.Property(e => e.Exitosa).HasColumnName("exitosa");

                entity.Property(e => e.Idejecucionenc).HasColumnName("idejecucionenc");

                entity.Property(e => e.Idnavconfigdet).HasColumnName("idnavconfigdet");

                entity.Property(e => e.RegistrosTi).HasColumnName("registros_ti");

                entity.Property(e => e.RegistrosWms).HasColumnName("registros_wms");

                entity.Property(e => e.RegistrosWs).HasColumnName("registros_ws");
            });

            modelBuilder.Entity<INavEnt>(entity =>
            {
                entity.HasKey(e => e.Idnavent);

                entity.ToTable("i_nav_ent");

                entity.Property(e => e.Idnavent)
                    .ValueGeneratedNever()
                    .HasColumnName("idnavent");

                entity.Property(e => e.Endpoint)
                    .HasMaxLength(512)
                    .IsUnicode(false)
                    .HasColumnName("endpoint");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<INavEntFiltro>(entity =>
            {
                entity.HasKey(e => e.Idnaventfiltro)
                    .HasName("PK_i_nav_env_filtros");

                entity.ToTable("i_nav_ent_filtros");

                entity.Property(e => e.Idnaventfiltro)
                    .ValueGeneratedNever()
                    .HasColumnName("idnaventfiltro");

                entity.Property(e => e.Idnavent).HasColumnName("idnavent");

                entity.Property(e => e.Valor)
                    .HasMaxLength(50)
                    .HasColumnName("valor");
            });

            modelBuilder.Entity<INavPedCompraDet>(entity =>
            {
                entity.HasKey(e => new { e.NoEnc, e.No, e.LineNo })
                    .HasName("PK__i_nav_pe__0D8B837B72B8B3D5");

                entity.ToTable("i_nav_ped_compra_det");

                entity.Property(e => e.NoEnc)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.No)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.LineNo).HasColumnName("Line_No");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Description2).HasMaxLength(50);

                entity.Property(e => e.DirectUnitCost).HasColumnName("Direct_Unit_Cost");

                entity.Property(e => e.LineAmount).HasColumnName("Line_Amount");

                entity.Property(e => e.LocationCode)
                    .HasMaxLength(50)
                    .HasColumnName("Location_Code");

                entity.Property(e => e.PlanedReceiptDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Planed_Receipt_Date");

                entity.Property(e => e.QuantityReceived).HasColumnName("Quantity_Received");

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.UnitOfMeasureCode)
                    .HasMaxLength(50)
                    .HasColumnName("Unit_Of_Measure_Code");

                entity.Property(e => e.VariantCode)
                    .HasMaxLength(25)
                    .HasColumnName("Variant_Code");
            });

            modelBuilder.Entity<INavPedCompraDetLote>(entity =>
            {
                entity.HasKey(e => new { e.NoEnc, e.ItemNo, e.SourceProdOrderLine })
                    .HasName("PK__i_nav_pe__6EC3B191C3EE4BF8");

                entity.ToTable("i_nav_ped_compra_det_lote");

                entity.Property(e => e.NoEnc).HasMaxLength(50);

                entity.Property(e => e.ItemNo)
                    .HasMaxLength(50)
                    .HasColumnName("Item_No");

                entity.Property(e => e.SourceProdOrderLine).HasColumnName("Source_Prod_Order_Line");

                entity.Property(e => e.EntryNo)
                    .HasMaxLength(50)
                    .HasColumnName("Entry_No");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Expiration_Date");

                entity.Property(e => e.LotNo)
                    .HasMaxLength(100)
                    .HasColumnName("Lot_No");

                entity.Property(e => e.QuantityBase).HasColumnName("Quantity_Base");

                entity.Property(e => e.SourceId)
                    .HasMaxLength(50)
                    .HasColumnName("source_ID");

                entity.Property(e => e.SourceType).HasColumnName("Source_Type");

                entity.Property(e => e.VariantCode)
                    .HasMaxLength(25)
                    .HasColumnName("Variant_Code");
            });

            modelBuilder.Entity<INavPedCompraEnc>(entity =>
            {
                entity.HasKey(e => e.No);

                entity.ToTable("i_nav_ped_compra_enc");

                entity.Property(e => e.No)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.BuyFromVendorName)
                    .HasMaxLength(250)
                    .HasColumnName("Buy_From_Vendor_Name");

                entity.Property(e => e.BuyFromVendorNo)
                    .HasMaxLength(150)
                    .HasColumnName("Buy_From_Vendor_No");

                entity.Property(e => e.DocumentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Document_Date");

                entity.Property(e => e.DocumentType)
                    .HasColumnName("Document_Type")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ExpectedReceiptDate)
                    .HasColumnType("date")
                    .HasColumnName("Expected_Receipt_Date");

                entity.Property(e => e.InternalTransferDocumentNo)
                    .HasMaxLength(50)
                    .HasColumnName("Internal_Transfer_Document_No");

                entity.Property(e => e.IsInternalTransfer)
                    .HasColumnName("Is_Internal_Transfer")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LocationCode)
                    .HasMaxLength(150)
                    .HasColumnName("Location_Code");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Order_Date");

                entity.Property(e => e.PaymentTermsCode)
                    .HasMaxLength(150)
                    .HasColumnName("Payment_Terms_Code");

                entity.Property(e => e.PostingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Posting_Date");

                entity.Property(e => e.PostingDescription)
                    .HasMaxLength(150)
                    .HasColumnName("Posting_Description");

                entity.Property(e => e.ProductOwnerCode)
                    .HasMaxLength(25)
                    .HasColumnName("Product_Owner_Code");

                entity.Property(e => e.ShipToContact)
                    .HasMaxLength(150)
                    .HasColumnName("Ship_To_Contact");

                entity.Property(e => e.ShipToName)
                    .HasMaxLength(250)
                    .HasColumnName("Ship_To_Name");

                entity.Property(e => e.Status).HasMaxLength(150);

                entity.Property(e => e.VendorInvoiceNo)
                    .HasMaxLength(150)
                    .HasColumnName("Vendor_Invoice_No");
            });

            modelBuilder.Entity<INavPedTrasladoDet>(entity =>
            {
                entity.HasKey(e => new { e.NoEnc, e.No, e.LineNo });

                entity.ToTable("i_nav_ped_traslado_det");

                entity.Property(e => e.NoEnc).HasMaxLength(50);

                entity.Property(e => e.No).HasMaxLength(50);

                entity.Property(e => e.LineNo)
                    .HasMaxLength(25)
                    .HasColumnName("Line_No");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.ItemNo)
                    .HasMaxLength(50)
                    .HasColumnName("Item_No");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ProcessResult)
                    .HasMaxLength(1000)
                    .HasColumnName("Process_Result");

                entity.Property(e => e.QtyToReceive).HasColumnName("Qty_to_Receive");

                entity.Property(e => e.QtyToShip).HasColumnName("Qty_to_Ship");

                entity.Property(e => e.ShipmentDate)
                    .HasColumnType("date")
                    .HasColumnName("Shipment_Date");

                entity.Property(e => e.SourceId)
                    .HasMaxLength(50)
                    .HasColumnName("source_id")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TransferToCodeField)
                    .HasMaxLength(50)
                    .HasColumnName("transfer_to_CodeField");

                entity.Property(e => e.UnitOfMeasureCode)
                    .HasMaxLength(50)
                    .HasColumnName("Unit_of_Measure_Code");

                entity.Property(e => e.VariantCode)
                    .HasMaxLength(25)
                    .HasColumnName("Variant_Code");
            });

            modelBuilder.Entity<INavPedTrasladoEnc>(entity =>
            {
                entity.HasKey(e => e.No);

                entity.ToTable("i_nav_ped_traslado_enc");

                entity.Property(e => e.No).HasMaxLength(50);

                entity.Property(e => e.PostingDate)
                    .HasColumnType("date")
                    .HasColumnName("Posting_Date");

                entity.Property(e => e.ProductOwnerCode)
                    .HasMaxLength(25)
                    .HasColumnName("Product_Owner_Code");

                entity.Property(e => e.ReceiptDate)
                    .HasColumnType("date")
                    .HasColumnName("Receipt_Date");

                entity.Property(e => e.ReceiptDocumentReference)
                    .HasMaxLength(50)
                    .HasColumnName("Receipt_Document_Reference");

                entity.Property(e => e.ShipmentDate)
                    .HasColumnType("date")
                    .HasColumnName("Shipment_Date");

                entity.Property(e => e.TransferFromCode)
                    .HasMaxLength(50)
                    .HasColumnName("Transfer_from_Code");

                entity.Property(e => e.TransferFromContact)
                    .HasMaxLength(50)
                    .HasColumnName("Transfer_from_Contact");

                entity.Property(e => e.TransferFromName)
                    .HasMaxLength(50)
                    .HasColumnName("Transfer_from_Name");

                entity.Property(e => e.TransferToCode)
                    .HasMaxLength(50)
                    .HasColumnName("Transfer_to_Code");

                entity.Property(e => e.TransferToCodeField)
                    .HasMaxLength(50)
                    .HasColumnName("transfer_to_CodeField");

                entity.Property(e => e.TransferToContact)
                    .HasMaxLength(50)
                    .HasColumnName("Transfer_to_Contact");

                entity.Property(e => e.TransferToName)
                    .HasMaxLength(50)
                    .HasColumnName("Transfer_to_Name");
            });

            modelBuilder.Entity<INavProducto>(entity =>
            {
                entity.HasKey(e => e.No);

                entity.ToTable("i_nav_producto");

                entity.Property(e => e.No).HasMaxLength(50);

                entity.Property(e => e.BaseUnitOfMeasure)
                    .HasMaxLength(50)
                    .HasColumnName("Base_Unit_Of_Measure");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Description2)
                    .HasMaxLength(50)
                    .HasColumnName("Description_2");

                entity.Property(e => e.GenProdPostingGroup)
                    .HasMaxLength(50)
                    .HasColumnName("Gen_Prod_Posting_Group");

                entity.Property(e => e.InventoryPostingGroup)
                    .HasMaxLength(50)
                    .HasColumnName("Inventory_Posting_Group");

                entity.Property(e => e.ItemCategoryCode)
                    .HasMaxLength(50)
                    .HasColumnName("Item_Category_Code");

                entity.Property(e => e.ItemTrackingCode)
                    .HasMaxLength(50)
                    .HasColumnName("Item_Tracking_Code");

                entity.Property(e => e.ProductGroupCode)
                    .HasMaxLength(50)
                    .HasColumnName("Product_Group_Code");

                entity.Property(e => e.SalesUnit)
                    .HasMaxLength(50)
                    .HasColumnName("Sales_Unit");

                entity.Property(e => e.SearchDescription)
                    .HasMaxLength(50)
                    .HasColumnName("Search_Description");

                entity.Property(e => e.UnitCost).HasColumnName("Unit_Cost");
            });

            modelBuilder.Entity<INavProveedor>(entity =>
            {
                entity.HasKey(e => e.No);

                entity.ToTable("i_nav_proveedor");

                entity.Property(e => e.No).HasMaxLength(50);

                entity.Property(e => e.Adress).HasMaxLength(50);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Contact).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.LocationCode)
                    .HasMaxLength(50)
                    .HasColumnName("Location_Code");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(50)
                    .HasColumnName("Phone_No");

                entity.Property(e => e.SearchName)
                    .HasMaxLength(50)
                    .HasColumnName("Search_Name");

                entity.Property(e => e.VatRegistratrionNo)
                    .HasMaxLength(50)
                    .HasColumnName("VAT_Registratrion_No");
            });

            modelBuilder.Entity<INavServicio>(entity =>
            {
                entity.HasKey(e => e.IdServicio);

                entity.ToTable("i_nav_servicio");

                entity.Property(e => e.IdServicio).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CodigoRubro).HasColumnName("codigo_rubro");

                entity.Property(e => e.CodigoServicio)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codigo_servicio")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nemonico)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("nemonico");

                entity.Property(e => e.ProcesadoWms).HasColumnName("procesado_wms");
            });

            modelBuilder.Entity<INavTransaccionesOut>(entity =>
            {
                entity.HasKey(e => e.Idtransaccion);

                entity.ToTable("i_nav_transacciones_out");

                entity.Property(e => e.Idtransaccion)
                    .ValueGeneratedNever()
                    .HasColumnName("idtransaccion");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CantidadEsperada).HasColumnName("Cantidad_Esperada");

                entity.Property(e => e.CantidadPresentacion).HasColumnName("cantidad_presentacion");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.CodigoProducto)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.CodigoVariante)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_variante");

                entity.Property(e => e.Enviado).HasColumnName("enviado");

                entity.Property(e => e.EsServicio)
                    .HasColumnName("es_servicio")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaDespacho)
                    .HasColumnType("date")
                    .HasColumnName("fecha_despacho");

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_recepcion");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("date")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.IdPedidoEncDevol).HasDefaultValueSql("((0))");

                entity.Property(e => e.IdTipoDocumento).HasDefaultValueSql("((0))");

                entity.Property(e => e.Idbodega).HasColumnName("idbodega");

                entity.Property(e => e.Iddespachoenc).HasColumnName("iddespachoenc");

                entity.Property(e => e.Idempresa).HasColumnName("idempresa");

                entity.Property(e => e.Idordencompra).HasColumnName("idordencompra");

                entity.Property(e => e.Idpedidoenc).HasColumnName("idpedidoenc");

                entity.Property(e => e.Idpresentacion).HasColumnName("idpresentacion");

                entity.Property(e => e.Idproducto).HasColumnName("idproducto");

                entity.Property(e => e.Idproductobodega).HasColumnName("idproductobodega");

                entity.Property(e => e.Idproductoestado).HasColumnName("idproductoestado");

                entity.Property(e => e.Idpropietario).HasColumnName("idpropietario");

                entity.Property(e => e.Idpropietariobodega).HasColumnName("idpropietariobodega");

                entity.Property(e => e.Idrecepcionenc).HasColumnName("idrecepcionenc");

                entity.Property(e => e.Idunidadmedida).HasColumnName("idunidadmedida");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoDocumentoSalidaRefDevol)
                    .HasMaxLength(50)
                    .HasColumnName("no_documento_salida_ref_devol")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.NoLinea)
                    .HasMaxLength(50)
                    .HasColumnName("no_linea");

                entity.Property(e => e.NoPedido)
                    .HasMaxLength(50)
                    .HasColumnName("no_pedido");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(150)
                    .HasColumnName("nombre_producto");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.PesoBruto).HasColumnName("peso_bruto");

                entity.Property(e => e.PesoNeto).HasColumnName("peso_neto");

                entity.Property(e => e.TipoTransaccion)
                    .HasMaxLength(50)
                    .HasColumnName("tipo_transaccion");

                entity.Property(e => e.UdsLicPlate).HasColumnName("uds_lic_plate");

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .HasColumnName("unidad_medida");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.Property(e => e.ValorAduana).HasColumnName("valor_aduana");

                entity.Property(e => e.ValorDai).HasColumnName("valor_dai");

                entity.Property(e => e.ValorFlete).HasColumnName("valor_flete");

                entity.Property(e => e.ValorFob).HasColumnName("valor_fob");

                entity.Property(e => e.ValorIva).HasColumnName("valor_iva");

                entity.Property(e => e.ValorSeguro).HasColumnName("valor_seguro");
            });

            modelBuilder.Entity<INavTransaccionesOutError>(entity =>
            {
                entity.HasKey(e => e.IdMensaje);

                entity.ToTable("i_nav_transacciones_out_error");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Mensaje).HasMaxLength(500);

                entity.Property(e => e.NumeroError).HasMaxLength(50);

                entity.Property(e => e.Observacion).HasMaxLength(150);

                entity.Property(e => e.ReferenciaErp)
                    .HasMaxLength(50)
                    .HasColumnName("ReferenciaERP");

                entity.Property(e => e.ReferenciaRoad).HasMaxLength(50);

                entity.Property(e => e.TipoTransaccionErp)
                    .HasMaxLength(50)
                    .HasColumnName("TipoTransaccionERP");

                entity.Property(e => e.TipoTransaccionRoad).HasMaxLength(50);

                entity.Property(e => e.UsuarioErp)
                    .HasMaxLength(50)
                    .HasColumnName("UsuarioERP");

                entity.Property(e => e.UsuarioWms)
                    .HasMaxLength(50)
                    .HasColumnName("UsuarioWMS")
                    .HasDefaultValueSql("('MI3')");
            });

            modelBuilder.Entity<INavUnidadMedidum>(entity =>
            {
                entity.HasKey(e => e.IdUnidadMedida);

                entity.ToTable("i_nav_unidad_medida");

                entity.Property(e => e.CodigoUnidadMedida)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("codigo_unidad_medida");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasColumnName("descripcion");

                entity.Property(e => e.ProcesadoWms).HasColumnName("procesado_wms");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("tipo");

                entity.Property(e => e.TipoRubro)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("tipo_rubro");
            });

            modelBuilder.Entity<ImpresionProductosBarra>(entity =>
            {
                entity.HasKey(e => e.IdProductoBarra)
                    .HasName("PK_Impresion_productos_barras");

                entity.ToTable("impresion_productos_barras");

                entity.Property(e => e.IdProductoBarra).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CajasPorCama).HasColumnName("Cajas_Por_Cama");

                entity.Property(e => e.CamasPorTarima).HasColumnName("Camas_Por_Tarima");

                entity.Property(e => e.CantidadImpresiones).HasColumnName("cantidad_impresiones");

                entity.Property(e => e.CantidadPresentacion).HasColumnName("Cantidad_Presentacion");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(100)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.FechaAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_agr");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Ingreso");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Vence");

                entity.Property(e => e.Impreso).HasColumnName("impreso");

                entity.Property(e => e.Lote).HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.UnidadMedida).HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");
            });

            modelBuilder.Entity<Impresora>(entity =>
            {
                entity.HasKey(e => e.IdImpresora)
                    .HasName("PK_Impresora");

                entity.ToTable("impresora");

                entity.Property(e => e.IdImpresora).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.DireccionIp)
                    .HasMaxLength(50)
                    .HasColumnName("direccion_Ip");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.MacAdress)
                    .HasMaxLength(25)
                    .HasColumnName("mac_adress");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Impresoras)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK_impresora_empresa");
            });

            modelBuilder.Entity<IndiceRotacion>(entity =>
            {
                entity.HasKey(e => e.IdIndiceRotacion);

                entity.ToTable("indice_rotacion");

                entity.Property(e => e.IdIndiceRotacion).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<InterfaceEnc>(entity =>
            {
                entity.HasKey(e => e.IdInterface);

                entity.ToTable("interface_enc");

                entity.Property(e => e.IdInterface).ValueGeneratedNever();

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<JornadaLaboral>(entity =>
            {
                entity.HasKey(e => e.IdJornada);

                entity.ToTable("jornada_laboral");

                entity.Property(e => e.IdJornada).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaBaja)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_baja");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_fin");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_inicio");

                entity.Property(e => e.HorasTrabajadas).HasColumnName("horas_trabajadas");

                entity.Property(e => e.NombreJornada)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_jornada");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.JornadaLaborals)
                    .HasForeignKey(d => d.IdBodega)
                    .HasConstraintName("FK_jornada_laboral_bodega");
            });

            modelBuilder.Entity<JornadaSistema>(entity =>
            {
                entity.HasKey(e => e.IdJornada);

                entity.ToTable("jornada_sistema");

                entity.Property(e => e.IdJornada).ValueGeneratedNever();

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.FechaAgregado).HasColumnType("datetime");
            });

            modelBuilder.Entity<LicenciaItem>(entity =>
            {
                entity.HasKey(e => e.IdDisp);

                entity.ToTable("licencia_item");

                entity.Property(e => e.IdDisp)
                    .HasMaxLength(200)
                    .HasColumnName("idDisp");

                entity.Property(e => e.Bandera)
                    .HasColumnName("bandera")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Estado)
                    .HasMaxLength(200)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaSistema)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_sistema")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(200)
                    .HasColumnName("identificacion");

                entity.Property(e => e.Tipo).HasColumnName("tipo");
            });

            modelBuilder.Entity<LicenciaLlave>(entity =>
            {
                entity.HasKey(e => e.IdLlave);

                entity.ToTable("licencia_llave");

                entity.Property(e => e.IdLlave)
                    .ValueGeneratedNever()
                    .HasColumnName("idLlave");

                entity.Property(e => e.Llave)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<LicenciaLogin>(entity =>
            {
                entity.HasKey(e => e.IdDisp);

                entity.ToTable("licencia_login");

                entity.Property(e => e.IdDisp)
                    .HasMaxLength(200)
                    .HasColumnName("idDisp");

                entity.Property(e => e.Valor)
                    .HasMaxLength(200)
                    .HasColumnName("valor");
            });

            modelBuilder.Entity<LicenciaSolic>(entity =>
            {
                entity.HasKey(e => e.IdDisp);

                entity.ToTable("licencia_solic");

                entity.Property(e => e.IdDisp)
                    .HasMaxLength(200)
                    .HasColumnName("idDisp");

                entity.Property(e => e.Estado)
                    .HasMaxLength(200)
                    .HasColumnName("estado");

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(200)
                    .HasColumnName("identificacion");

                entity.Property(e => e.Tipo).HasColumnName("tipo");
            });

            modelBuilder.Entity<LogErrorWm>(entity =>
            {
                entity.HasKey(e => e.IdError);

                entity.ToTable("log_error_wms");

                entity.Property(e => e.IdError).ValueGeneratedNever();

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MensajeError).HasMaxLength(500);
            });

            modelBuilder.Entity<LogImportacionExcel>(entity =>
            {
                entity.HasKey(e => e.IdImportacion);

                entity.ToTable("log_importacion_excel");

                entity.Property(e => e.IdImportacion).ValueGeneratedNever();

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.HashArchivo)
                    .HasMaxLength(150)
                    .HasColumnName("hash_archivo");
            });

            modelBuilder.Entity<Marcaje>(entity =>
            {
                entity.HasKey(e => e.IdMarcaje)
                    .HasName("PK_marcaje_1");

                entity.ToTable("marcaje");

                entity.Property(e => e.IdMarcaje).ValueGeneratedNever();

                entity.Property(e => e.EsBitacora).HasColumnName("Es_bitacora");

                entity.Property(e => e.FecLectura)
                    .HasColumnType("date")
                    .HasColumnName("Fec_lectura");

                entity.Property(e => e.HoraEntro)
                    .HasColumnType("datetime")
                    .HasColumnName("Hora_entro");

                entity.Property(e => e.HoraFinHorario)
                    .HasColumnType("datetime")
                    .HasColumnName("Hora_fin_horario");

                entity.Property(e => e.HoraInicioHorario)
                    .HasColumnType("datetime")
                    .HasColumnName("Hora_inicio_horario");

                entity.Property(e => e.HoraLectura)
                    .HasColumnType("datetime")
                    .HasColumnName("Hora_lectura");

                entity.Property(e => e.HoraSalio)
                    .HasColumnType("datetime")
                    .HasColumnName("Hora_salio");

                entity.Property(e => e.IdDispositivo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IngresoAnticipado).HasColumnName("Ingreso_anticipado");

                entity.Property(e => e.IngresoTardio).HasColumnName("Ingreso_tardio");

                entity.Property(e => e.MarcajeAproximado).HasColumnName("Marcaje_aproximado");

                entity.Property(e => e.MarcajeContabilizado).HasColumnName("Marcaje_contabilizado");

                entity.Property(e => e.MarcajeFueraDeSucursal).HasColumnName("Marcaje_fuera_de_sucursal");

                entity.Property(e => e.MarcajeManual)
                    .HasColumnName("Marcaje_manual")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PrimerMarcaje).HasColumnName("Primer_marcaje");

                entity.Property(e => e.SalidaAnticipada).HasColumnName("Salida_anticipada");

                entity.Property(e => e.SalidaTardia).HasColumnName("Salida_tardia");
            });

            modelBuilder.Entity<MensajeRegla>(entity =>
            {
                entity.HasKey(e => e.IdMensajeRegla);

                entity.ToTable("mensaje_regla");

                entity.Property(e => e.IdMensajeRegla).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<MenuRol>(entity =>
            {
                entity.HasKey(e => e.IdMenuRol);

                entity.ToTable("menu_rol");

                entity.Property(e => e.IdMenuRol).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.IdMenu)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.Property(e => e.Visible).HasColumnName("visible");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.MenuRols)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_menu_rol_rol");
            });

            modelBuilder.Entity<MenuRolOp>(entity =>
            {
                entity.HasKey(e => new { e.IdMenuSistemaOp, e.IdRolOperador });

                entity.ToTable("menu_rol_op");

                entity.Property(e => e.IdMenuSistemaOp)
                    .HasMaxLength(50)
                    .HasColumnName("IdMenuSistemaOP");

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasComment("indica si el registro fue eliminado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.Property(e => e.Visible)
                    .HasColumnName("visible")
                    .HasComment("Indica si la opción del menú está o no habilitada para el operador");

                entity.HasOne(d => d.IdMenuSistemaOpNavigation)
                    .WithMany(p => p.MenuRolOps)
                    .HasForeignKey(d => d.IdMenuSistemaOp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_menu_rol_op_menu_sistema_op");

                entity.HasOne(d => d.IdRolOperadorNavigation)
                    .WithMany(p => p.MenuRolOps)
                    .HasForeignKey(d => d.IdRolOperador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_menu_rol_op_rol_operador");
            });

            modelBuilder.Entity<MenuSistema>(entity =>
            {
                entity.HasKey(e => e.IdMenu);

                entity.ToTable("menu_sistema");

                entity.Property(e => e.IdMenu).HasMaxLength(50);

                entity.Property(e => e.Nivel).HasColumnName("nivel");

                entity.Property(e => e.NombreLgco)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_lgco")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Padre)
                    .HasMaxLength(50)
                    .HasColumnName("padre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.SolicitarClaveAutorizacion)
                    .HasColumnName("solicitar_clave_autorizacion")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(50)
                    .HasColumnName("titulo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<MenuSistemaOp>(entity =>
            {
                entity.HasKey(e => e.IdMenuSistemaOp)
                    .HasName("PK_menu_operador");

                entity.ToTable("menu_sistema_op");

                entity.Property(e => e.IdMenuSistemaOp)
                    .HasMaxLength(50)
                    .HasColumnName("IdMenuSistemaOP");

                entity.Property(e => e.IdTipoTarea).HasComment("valor sale de sis_tipo_tarea");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Padre).HasMaxLength(50);

                entity.Property(e => e.Posicion).HasComment("indica la posición en la que se desplegará en la HH");

                entity.HasOne(d => d.IdTipoTareaNavigation)
                    .WithMany(p => p.MenuSistemaOps)
                    .HasForeignKey(d => d.IdTipoTarea)
                    .HasConstraintName("FK_menu_sistema_op_sis_tipo_tarea");
            });

            modelBuilder.Entity<Montacarga>(entity =>
            {
                entity.HasKey(e => e.IdMontacarga);

                entity.ToTable("montacarga");

                entity.Property(e => e.IdMontacarga).ValueGeneratedNever();

                entity.Property(e => e.CapacidadBasica)
                    .HasColumnName("capacidad_basica")
                    .HasComment("toneladas métricas o libras máximas soportadas");

                entity.Property(e => e.CostoHora).HasColumnName("costo_hora");

                entity.Property(e => e.DesplazamientoMotor)
                    .HasColumnName("desplazamiento_motor")
                    .HasComment("En litros ?");

                entity.Property(e => e.FechaCompra)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_compra");

                entity.Property(e => e.FechaInicioOperaciones)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_inicio_operaciones");

                entity.Property(e => e.Modelo).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.ProximoMantenimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("proximo_mantenimiento");

                entity.Property(e => e.Serie).HasMaxLength(50);

                entity.Property(e => e.TipoCombustible)
                    .HasMaxLength(50)
                    .HasColumnName("tipo_combustible")
                    .HasComment("Diesel, gasolina, Gas ");

                entity.Property(e => e.TipoMontacarga)
                    .HasMaxLength(50)
                    .HasColumnName("tipo_montacarga")
                    .HasComment("Eléctrico o combustión ?");
            });

            modelBuilder.Entity<MontacargaBodega>(entity =>
            {
                entity.HasKey(e => e.IdMontacargaBodega);

                entity.ToTable("montacarga_bodega");

                entity.Property(e => e.IdMontacargaBodega).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<MontacargaServicioEnc>(entity =>
            {
                entity.HasKey(e => e.IdServicioEnc);

                entity.ToTable("montacarga_servicio_enc");

                entity.Property(e => e.IdServicioEnc).ValueGeneratedNever();

                entity.Property(e => e.Descripcion).HasMaxLength(100);

                entity.Property(e => e.FechaAtencion)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Atencion");

                entity.Property(e => e.FechaFin).HasColumnType("datetime");

                entity.Property(e => e.FechaInicio).HasColumnType("datetime");

                entity.Property(e => e.FechaServicio)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Servicio");

                entity.Property(e => e.FechaSistema)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Sistema")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ObservacionTecnico).HasMaxLength(250);

                entity.Property(e => e.Solicita).HasMaxLength(50);

                entity.Property(e => e.Tecnico).HasMaxLength(50);

                entity.Property(e => e.TipoServicio)
                    .HasMaxLength(50)
                    .HasComment("Preventivo o Correctivo");
            });

            modelBuilder.Entity<MontacargaTipoFalla>(entity =>
            {
                entity.HasKey(e => e.IdTipoFalla)
                    .HasName("PK_TipoFalla");

                entity.ToTable("montacarga_tipoFalla");

                entity.Property(e => e.IdTipoFalla).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<MotivoAnulacion>(entity =>
            {
                entity.HasKey(e => e.IdMotivoAnulacion);

                entity.ToTable("motivo_anulacion");

                entity.Property(e => e.IdMotivoAnulacion).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.MotivoAnulacions)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_motivo_anulacion_empresa");
            });

            modelBuilder.Entity<MotivoAnulacionBodega>(entity =>
            {
                entity.HasKey(e => e.IdMotivoAnulacionBodega);

                entity.ToTable("motivo_anulacion_bodega");

                entity.Property(e => e.IdMotivoAnulacionBodega).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.MotivoAnulacionBodegas)
                    .HasForeignKey(d => d.IdBodega)
                    .HasConstraintName("FK_motivo_anulacion_bodega_bodega");

                entity.HasOne(d => d.IdMotivoAnulacionNavigation)
                    .WithMany(p => p.MotivoAnulacionBodegas)
                    .HasForeignKey(d => d.IdMotivoAnulacion)
                    .HasConstraintName("FK_motivo_anulacion_bodega_motivo_anulacion");
            });

            modelBuilder.Entity<MotivoDevolucion>(entity =>
            {
                entity.HasKey(e => e.IdMotivoDevolucion);

                entity.ToTable("motivo_devolucion");

                entity.Property(e => e.IdMotivoDevolucion).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.EsDetalle).HasColumnName("es_detalle");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.MotivoDevolucions)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_motivo_devolucion_empresa");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.MotivoDevolucions)
                    .HasForeignKey(d => d.IdPropietario)
                    .HasConstraintName("FK_motivo_devolucion_propietarios");
            });

            modelBuilder.Entity<MotivoDevolucionBodega>(entity =>
            {
                entity.HasKey(e => e.IdMotivoDevolucionBodega);

                entity.ToTable("motivo_devolucion_bodega");

                entity.Property(e => e.IdMotivoDevolucionBodega).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.MotivoDevolucionBodegas)
                    .HasForeignKey(d => d.IdBodega)
                    .HasConstraintName("FK_motivo_devolucion_bodega_bodega");

                entity.HasOne(d => d.IdMotivoDevolucionNavigation)
                    .WithMany(p => p.MotivoDevolucionBodegas)
                    .HasForeignKey(d => d.IdMotivoDevolucion)
                    .HasConstraintName("FK_motivo_devolucion_bodega_motivo_devolucion");
            });

            modelBuilder.Entity<MotivoUbicacion>(entity =>
            {
                entity.HasKey(e => e.IdMotivoUbicacion);

                entity.ToTable("motivo_ubicacion");

                entity.Property(e => e.IdMotivoUbicacion).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.MotivoUbicacions)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_motivo_ubicacion_empresa");
            });

            modelBuilder.Entity<Operador>(entity =>
            {
                entity.HasKey(e => e.IdOperador)
                    .HasName("PK_operador_1");

                entity.ToTable("operador");

                entity.Property(e => e.IdOperador).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(100)
                    .HasColumnName("apellidos")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Clave)
                    .HasMaxLength(25)
                    .HasColumnName("clave")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(25)
                    .HasColumnName("codigo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CostoHora).HasColumnName("costo_hora");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .HasColumnName("direccion")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Foto)
                    .HasColumnType("image")
                    .HasColumnName("foto");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(100)
                    .HasColumnName("nombres")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Pickea)
                    .HasColumnName("pickea")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Recibe)
                    .HasColumnName("recibe")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("telefono")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Transporta)
                    .HasColumnName("transporta")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ubica)
                    .HasColumnName("ubica")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UsaHh).HasColumnName("usa_hh");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Verifica)
                    .HasColumnName("verifica")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Operadors)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK_operador_empresa");

                entity.HasOne(d => d.IdJornadaNavigation)
                    .WithMany(p => p.Operadors)
                    .HasForeignKey(d => d.IdJornada)
                    .HasConstraintName("FK_operador_jornada_laboral");
            });

            modelBuilder.Entity<OperadorBodega>(entity =>
            {
                entity.HasKey(e => e.IdOperadorBodega);

                entity.ToTable("operador_bodega");

                entity.Property(e => e.IdOperadorBodega).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.OperadorBodegas)
                    .HasForeignKey(d => d.IdBodega)
                    .HasConstraintName("FK_operador_bodega_bodega");

                entity.HasOne(d => d.IdOperadorNavigation)
                    .WithMany(p => p.OperadorBodegas)
                    .HasForeignKey(d => d.IdOperador)
                    .HasConstraintName("FK_operador_bodega_operador");
            });

            modelBuilder.Entity<PParametro>(entity =>
            {
                entity.HasKey(e => e.IdParametro)
                    .HasName("PK_producto_parametros_1");

                entity.ToTable("p_parametro");

                entity.Property(e => e.IdParametro).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .HasColumnName("tipo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.Property(e => e.ValorFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("valor_fecha");

                entity.Property(e => e.ValorLogico).HasColumnName("valor_logico");

                entity.Property(e => e.ValorNumerico).HasColumnName("valor_numerico");

                entity.Property(e => e.ValorTexto)
                    .HasMaxLength(50)
                    .HasColumnName("valor_texto")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<PaisDepartamento>(entity =>
            {
                entity.HasKey(e => e.IdDepartamento)
                    .HasName("PK__pais_dep__787A433D5CFF1199");

                entity.ToTable("pais_departamento");

                entity.Property(e => e.IdDepartamento).ValueGeneratedNever();

                entity.Property(e => e.FecAgr).HasColumnName("fec_agr");

                entity.Property(e => e.FecMod).HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UserAgr).HasColumnName("user_agr");

                entity.Property(e => e.UserMod).HasColumnName("user_mod");

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.PaisDepartamentos)
                    .HasForeignKey(d => d.IdPais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pais_departamento_paises");
            });

            modelBuilder.Entity<PaisMunicipio>(entity =>
            {
                entity.HasKey(e => e.IdMunicipio);

                entity.ToTable("pais_municipio");

                entity.Property(e => e.IdMunicipio).ValueGeneratedNever();

                entity.Property(e => e.FecAgr).HasColumnName("fec_agr");

                entity.Property(e => e.FecMod).HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserAgr).HasColumnName("user_agr");

                entity.Property(e => e.UserMod).HasColumnName("user_mod");

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.PaisMunicipios)
                    .HasForeignKey(d => d.IdDepartamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pais_municipio_pais_departamento");
            });

            modelBuilder.Entity<PaisRegion>(entity =>
            {
                entity.HasKey(e => e.IdRegion)
                    .HasName("PK__pais_reg__8CBC09EBCD0136C9");

                entity.ToTable("pais_region");

                entity.Property(e => e.IdRegion).ValueGeneratedNever();

                entity.Property(e => e.FecAgr).HasColumnName("fec_agr");

                entity.Property(e => e.FecMod).HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UserAgr).HasColumnName("user_agr");

                entity.Property(e => e.UserMod).HasColumnName("user_mod");

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.PaisRegions)
                    .HasForeignKey(d => d.IdPais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pais_region_paises");
            });

            modelBuilder.Entity<Paise>(entity =>
            {
                entity.HasKey(e => e.IdPais);

                entity.ToTable("paises");

                entity.Property(e => e.IdPais).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Iso2)
                    .HasMaxLength(50)
                    .HasColumnName("ISO2")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Iso3)
                    .HasMaxLength(50)
                    .HasColumnName("ISO3")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Isonum).HasColumnName("ISONUM");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("NOMBRE")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<PerfilSerializado>(entity =>
            {
                entity.HasKey(e => e.IdPerfilSerializado);

                entity.ToTable("perfil_serializado");

                entity.Property(e => e.IdPerfilSerializado).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<PortalMenu>(entity =>
            {
                entity.HasKey(e => e.IdPortalMenu)
                    .HasName("PK_menu_opcion");

                entity.ToTable("portal_menu");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Enlace)
                    .HasMaxLength(250)
                    .HasColumnName("enlace")
                    .IsFixedLength(true);

                entity.Property(e => e.Icono)
                    .HasMaxLength(150)
                    .HasColumnName("icono")
                    .IsFixedLength(true);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("nombre")
                    .IsFixedLength(true);

                entity.Property(e => e.Padre).HasColumnName("padre");

                entity.HasOne(d => d.PadreNavigation)
                    .WithMany(p => p.InversePadreNavigation)
                    .HasForeignKey(d => d.Padre)
                    .HasConstraintName("FK_menu_opcion_menu_opcion1");
            });

            modelBuilder.Entity<PortalMenuRol>(entity =>
            {
                entity.HasKey(e => e.IdPortalMenuRol)
                    .HasName("PK_rol_menu_opcion");

                entity.ToTable("portal_menu_rol");

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdPortalMenuNavigation)
                    .WithMany(p => p.PortalMenuRols)
                    .HasForeignKey(d => d.IdPortalMenu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rol_menu_opcion_menu_opcion");

                entity.HasOne(d => d.IdPortalRolNavigation)
                    .WithMany(p => p.PortalMenuRols)
                    .HasForeignKey(d => d.IdPortalRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rol_menu_opcion_rol_portal");
            });

            modelBuilder.Entity<PortalRol>(entity =>
            {
                entity.HasKey(e => e.IdPortalRol);

                entity.ToTable("portal_rol");

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK_producto_1");

                entity.ToTable("producto");

                entity.HasIndex(e => e.Codigo, "IX_producto")
                    .IsUnique();

                entity.HasIndex(e => e.IdPropietario, "NCLI_Producto_20191210A_EJC");

                entity.HasIndex(e => e.IdClasificacion, "NCLI_Producto_20191210_EJC");

                entity.HasIndex(e => e.IdPropietario, "NCL_Producto_20191122_EJC");

                entity.HasIndex(e => e.IdUnidadMedidaBasica, "NCL_Producto_20200115_ejc");

                entity.Property(e => e.IdProducto).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.CapturaArancel)
                    .HasColumnName("captura_arancel")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CapturarAniada)
                    .HasColumnName("capturar_aniada")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CicloVida)
                    .HasColumnName("ciclo_vida")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.ControlLote)
                    .HasColumnName("control_lote")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ControlPeso)
                    .HasColumnName("control_peso")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ControlVencimiento)
                    .HasColumnName("control_vencimiento")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Costo)
                    .HasColumnName("costo")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EsHardware)
                    .HasColumnName("es_hardware")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ExistenciaMax)
                    .HasColumnName("existencia_max")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ExistenciaMin)
                    .HasColumnName("existencia_min")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Fechamanufactura)
                    .HasColumnName("fechamanufactura")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.GeneraLote)
                    .HasColumnName("genera_lote")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.GeneraLpOld)
                    .HasColumnName("genera_lp_old")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Imagen)
                    .HasColumnType("image")
                    .HasColumnName("imagen");

                entity.Property(e => e.Kit)
                    .HasColumnName("kit")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.MateriaPrima)
                    .HasColumnName("materia_prima")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Noparte)
                    .HasMaxLength(50)
                    .HasColumnName("noparte");

                entity.Property(e => e.Noserie)
                    .HasMaxLength(50)
                    .HasColumnName("noserie");

                entity.Property(e => e.PesoDespacho).HasColumnName("peso_despacho");

                entity.Property(e => e.PesoRecepcion)
                    .HasColumnName("peso_recepcion")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PesoReferencia).HasColumnName("peso_referencia");

                entity.Property(e => e.PesoTolerancia).HasColumnName("peso_tolerancia");

                entity.Property(e => e.Precio)
                    .HasColumnName("precio")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Serializado)
                    .HasColumnName("serializado")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TemperaturaDespacho)
                    .HasColumnName("temperatura_despacho")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TemperaturaRecepcion)
                    .HasColumnName("temperatura_recepcion")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TemperaturaReferencia).HasColumnName("temperatura_referencia");

                entity.Property(e => e.TemperaturaTolerancia).HasColumnName("temperatura_tolerancia");

                entity.Property(e => e.Tolerancia)
                    .HasColumnName("tolerancia")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdArancelNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdArancel)
                    .HasConstraintName("FK_producto_Arancel");

                entity.HasOne(d => d.IdCamaraNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCamara)
                    .HasConstraintName("FK_producto_camara");

                entity.HasOne(d => d.IdClasificacionNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdClasificacion)
                    .HasConstraintName("FK_producto_producto_clasificacion");

                entity.HasOne(d => d.IdFamiliaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdFamilia)
                    .HasConstraintName("FK_producto_producto_familia");

                entity.HasOne(d => d.IdIndiceRotacionNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdIndiceRotacion)
                    .HasConstraintName("FK_producto_indice_rotacion");

                entity.HasOne(d => d.IdMarcaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdMarca)
                    .HasConstraintName("FK_producto_producto_marca");

                entity.HasOne(d => d.IdPerfilSerializadoNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdPerfilSerializado)
                    .HasConstraintName("FK_producto_perfil_serializado");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdPropietario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_propietarios");

                entity.HasOne(d => d.IdSimbologiaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdSimbologia)
                    .HasConstraintName("FK_producto_simbologias_codigo_barra");

                entity.HasOne(d => d.IdTipoProductoNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdTipoProducto)
                    .HasConstraintName("FK_producto_producto_tipo");

                entity.HasOne(d => d.IdTipoRotacionNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdTipoRotacion)
                    .HasConstraintName("FK_producto_tipo_rotacion");

                entity.HasOne(d => d.IdUnidadMedidaBasicaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdUnidadMedidaBasica)
                    .HasConstraintName("FK_producto_unidad_medida");
            });

            modelBuilder.Entity<ProductoBodega>(entity =>
            {
                entity.HasKey(e => e.IdProductoBodega);

                entity.ToTable("producto_bodega");

                entity.HasIndex(e => e.IdBodega, "NCLI_PRODUCTO_BODEGA_20191118_EJC");

                entity.HasIndex(e => e.IdProducto, "NCLI_Producto_Bodega_20191210_EJC");

                entity.Property(e => e.IdProductoBodega).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.ProductoBodegas)
                    .HasForeignKey(d => d.IdBodega)
                    .HasConstraintName("FK_producto_bodega_bodega");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.ProductoBodegas)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_producto_bodega_producto");
            });

            modelBuilder.Entity<ProductoClasificacion>(entity =>
            {
                entity.HasKey(e => e.IdClasificacion);

                entity.ToTable("producto_clasificacion");

                entity.Property(e => e.IdClasificacion).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.ProductoClasificacions)
                    .HasForeignKey(d => d.IdPropietario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_clasificacion_propietarios");
            });

            modelBuilder.Entity<ProductoCodigosBarra>(entity =>
            {
                entity.HasKey(e => e.IdProductoCodigoBarra);

                entity.ToTable("producto_codigos_barra");

                entity.Property(e => e.IdProductoCodigoBarra).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CodigoBarra)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<ProductoEstado>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.ToTable("producto_estado");

                entity.Property(e => e.IdEstado).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CodigoBodegaErp)
                    .HasMaxLength(25)
                    .HasColumnName("codigo_bodega_erp");

                entity.Property(e => e.Dañado)
                    .HasColumnName("dañado")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.Property(e => e.Utilizable)
                    .HasColumnName("utilizable")
                    .HasComment("Define si el producto en éste estado puede ser utilizado para pedidos");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.ProductoEstados)
                    .HasForeignKey(d => d.IdPropietario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_estado_propietarios");
            });

            modelBuilder.Entity<ProductoEstadoUbic>(entity =>
            {
                entity.HasKey(e => e.IdProductoEstadUbic)
                    .HasName("PK_producto_estado_ubic_1");

                entity.ToTable("producto_estado_ubic");

                entity.Property(e => e.IdProductoEstadUbic).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.ProductoEstadoUbics)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_estado_ubic_producto_estado");
            });

            modelBuilder.Entity<ProductoFamilium>(entity =>
            {
                entity.HasKey(e => e.IdFamilia)
                    .HasName("PK_producto_familia_1");

                entity.ToTable("producto_familia");

                entity.Property(e => e.IdFamilia).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.ProductoFamilia)
                    .HasForeignKey(d => d.IdPropietario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_familia_propietarios");
            });

            modelBuilder.Entity<ProductoKitComposicion>(entity =>
            {
                entity.HasKey(e => e.IdProductoKitComposicion);

                entity.ToTable("producto_kit_composicion");

                entity.Property(e => e.IdProductoKitComposicion).ValueGeneratedNever();

                entity.Property(e => e.FechaAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_agr");

                entity.Property(e => e.FechaMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<ProductoMarca>(entity =>
            {
                entity.HasKey(e => e.IdMarca);

                entity.ToTable("producto_marca");

                entity.Property(e => e.IdMarca).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.ProductoMarcas)
                    .HasForeignKey(d => d.IdPropietario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_marca_propietarios");
            });

            modelBuilder.Entity<ProductoPallet>(entity =>
            {
                entity.HasKey(e => e.IdPallet);

                entity.ToTable("producto_pallet");

                entity.Property(e => e.IdPallet).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CodigoBarra)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.Lote)
                    .HasMaxLength(25)
                    .HasColumnName("lote");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdImpresoraNavigation)
                    .WithMany(p => p.ProductoPallets)
                    .HasForeignKey(d => d.IdImpresora)
                    .HasConstraintName("FK_producto_pallet_Impresora");

                entity.HasOne(d => d.IdOperadorBodegaNavigation)
                    .WithMany(p => p.ProductoPallets)
                    .HasForeignKey(d => d.IdOperadorBodega)
                    .HasConstraintName("FK_producto_pallet_operador_bodega");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.ProductoPallets)
                    .HasForeignKey(d => d.IdPresentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_pallet_producto_presentacion");

                entity.HasOne(d => d.IdProductoBodegaNavigation)
                    .WithMany(p => p.ProductoPallets)
                    .HasForeignKey(d => d.IdProductoBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_pallet_producto_bodega");

                entity.HasOne(d => d.IdPropietarioBodegaNavigation)
                    .WithMany(p => p.ProductoPallets)
                    .HasForeignKey(d => d.IdPropietarioBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_pallet_propietario_bodega");

                entity.HasOne(d => d.IdRecepcionEncNavigation)
                    .WithMany(p => p.ProductoPallets)
                    .HasForeignKey(d => d.IdRecepcionEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_pallet_trans_re_enc");

                entity.HasOne(d => d.IdRecepcion)
                    .WithMany(p => p.ProductoPallets)
                    .HasForeignKey(d => new { d.IdRecepcionDet, d.IdRecepcionEnc })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_pallet_trans_re_det");
            });

            modelBuilder.Entity<ProductoPalletRec>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("producto_pallet_rec");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CodigoBarra)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.Lote)
                    .HasMaxLength(25)
                    .HasColumnName("lote");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<ProductoParametro>(entity =>
            {
                entity.HasKey(e => e.IdProductoParametro)
                    .HasName("PK_producto_parametros_2");

                entity.ToTable("producto_parametros");

                entity.Property(e => e.IdProductoParametro).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CapturarSiempre).HasColumnName("capturar_siempre");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.Property(e => e.ValorFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("valor_fecha");

                entity.Property(e => e.ValorLogico).HasColumnName("valor_logico");

                entity.Property(e => e.ValorNumerico).HasColumnName("valor_numerico");

                entity.Property(e => e.ValorTexto)
                    .HasMaxLength(50)
                    .HasColumnName("valor_texto");

                entity.HasOne(d => d.IdParametroNavigation)
                    .WithMany(p => p.ProductoParametros)
                    .HasForeignKey(d => d.IdParametro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_parametros_p_parametro");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.ProductoParametros)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_parametros_producto");
            });

            modelBuilder.Entity<ProductoPresentacion>(entity =>
            {
                entity.HasKey(e => e.IdPresentacion)
                    .HasName("PK_producto_presentacion_1");

                entity.ToTable("producto_presentacion");

                entity.HasIndex(e => e.IdProducto, "NCLI_Producto_Presentacion_20210825_EJC");

                entity.Property(e => e.IdPresentacion).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.GeneraLpAuto)
                    .HasColumnName("genera_lp_auto")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ImprimeBarra).HasColumnName("imprime_barra");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.PermitirPaletizar)
                    .HasColumnName("permitir_paletizar")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Índica si la presentación puede paletizarse.");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Sistema)
                    .HasColumnName("sistema")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdPresentacionPalletNavigation)
                    .WithMany(p => p.InverseIdPresentacionPalletNavigation)
                    .HasForeignKey(d => d.IdPresentacionPallet)
                    .HasConstraintName("FK__producto___IdPre__60B4AB0C");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.ProductoPresentacions)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_presentacion_producto");
            });

            modelBuilder.Entity<ProductoPresentacionTarima>(entity =>
            {
                entity.HasKey(e => e.IdPresentacionTarima);

                entity.ToTable("producto_presentacion_tarima");

                entity.Property(e => e.IdPresentacionTarima).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Cantidad)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Cantidad de cajas por tarima");

                entity.Property(e => e.CantidadPorCama)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Cantidad de cajas por fila o cama");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.IdPresentacion).HasComment("Id de la presentación asociada al producto");

                entity.Property(e => e.IdTipoTarima).HasComment("Id del tipo de tarima");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.ProductoPresentacionTarimas)
                    .HasForeignKey(d => d.IdPresentacion)
                    .HasConstraintName("FK_producto_presentacion_tarima_producto_presentacion");

                entity.HasOne(d => d.IdTipoTarimaNavigation)
                    .WithMany(p => p.ProductoPresentacionTarimas)
                    .HasForeignKey(d => d.IdTipoTarima)
                    .HasConstraintName("FK_producto_presentacion_tarima_tipo_tarima");
            });

            modelBuilder.Entity<ProductoPresentacionesConversione>(entity =>
            {
                entity.HasKey(e => e.IdConversion);

                entity.ToTable("producto_presentaciones_conversiones");

                entity.Property(e => e.IdConversion).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Inverso).HasColumnName("inverso");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdPresentacionOrigenNavigation)
                    .WithMany(p => p.ProductoPresentacionesConversiones)
                    .HasForeignKey(d => d.IdPresentacionOrigen)
                    .HasConstraintName("FK_producto_presentaciones_conversiones_producto_presentacion");
            });

            modelBuilder.Entity<ProductoRellenado>(entity =>
            {
                entity.HasKey(e => e.IdRellenado);

                entity.ToTable("producto_rellenado");

                entity.Property(e => e.IdRellenado).ValueGeneratedNever();

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.NomPresentacionRellenarCon).HasMaxLength(100);

                entity.Property(e => e.NomUmBasAbastecerCon).HasMaxLength(100);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(30)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.ProductoRellenados)
                    .HasForeignKey(d => d.IdPresentacion)
                    .HasConstraintName("FK_producto_rellenado_producto_presentacion");

                entity.HasOne(d => d.IdProductoEstadoNavigation)
                    .WithMany(p => p.ProductoRellenados)
                    .HasForeignKey(d => d.IdProductoEstado)
                    .HasConstraintName("FK_producto_rellenado_producto_estado");

                entity.HasOne(d => d.IdTipoAccionNavigation)
                    .WithMany(p => p.ProductoRellenados)
                    .HasForeignKey(d => d.IdTipoAccion)
                    .HasConstraintName("FK_producto_rellenado_sis_tipo_accion");
            });

            modelBuilder.Entity<ProductoSustituto>(entity =>
            {
                entity.HasKey(e => e.IdProductoSustituto);

                entity.ToTable("producto_sustituto");

                entity.Property(e => e.IdProductoSustituto).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdProductoOriginalNavigation)
                    .WithMany(p => p.ProductoSustitutoIdProductoOriginalNavigations)
                    .HasForeignKey(d => d.IdProductoOriginal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_sustituto_producto");

                entity.HasOne(d => d.IdProductoPresentacionOriginalNavigation)
                    .WithMany(p => p.ProductoSustitutos)
                    .HasForeignKey(d => d.IdProductoPresentacionOriginal)
                    .HasConstraintName("FK_producto_sustituto_producto_presentacion");

                entity.HasOne(d => d.IdProductoReemplazoNavigation)
                    .WithMany(p => p.ProductoSustitutoIdProductoReemplazoNavigations)
                    .HasForeignKey(d => d.IdProductoReemplazo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_sustituto_producto1");
            });

            modelBuilder.Entity<ProductoTipo>(entity =>
            {
                entity.HasKey(e => e.IdTipoProducto);

                entity.ToTable("producto_tipo");

                entity.Property(e => e.IdTipoProducto).ValueGeneratedNever();

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.NombreTipoProducto)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.ProductoTipos)
                    .HasForeignKey(d => d.IdPropietario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_tipo_propietarios");
            });

            modelBuilder.Entity<Propietario>(entity =>
            {
                entity.HasKey(e => e.IdPropietario)
                    .HasName("PK_propietarios_1");

                entity.ToTable("propietarios");

                entity.Property(e => e.IdPropietario).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.ActualizaCostoOc).HasColumnName("actualiza_costo_oc");

                entity.Property(e => e.ClaveAcceso)
                    .HasMaxLength(50)
                    .HasColumnName("clave_acceso");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(25)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoAcceso)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_acceso");

                entity.Property(e => e.Color).HasColumnName("color");

                entity.Property(e => e.Contacto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("contacto")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .HasColumnName("direccion");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Imagen)
                    .HasColumnType("image")
                    .HasColumnName("imagen");

                entity.Property(e => e.Nit)
                    .HasMaxLength(50)
                    .HasColumnName("NIT");

                entity.Property(e => e.NombreComercial)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombre_comercial")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Sistema)
                    .HasColumnName("sistema")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("telefono")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Propietarios)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK_propietarios_empresa");

                entity.HasOne(d => d.IdTipoActualizacionCostoNavigation)
                    .WithMany(p => p.Propietarios)
                    .HasForeignKey(d => d.IdTipoActualizacionCosto)
                    .HasConstraintName("FK_propietarios_tipo_actualizacion_costo");
            });

            modelBuilder.Entity<PropietarioBodega>(entity =>
            {
                entity.HasKey(e => e.IdPropietarioBodega);

                entity.ToTable("propietario_bodega");

                entity.Property(e => e.IdPropietarioBodega).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(30)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.PropietarioBodegas)
                    .HasForeignKey(d => d.IdBodega)
                    .HasConstraintName("FK_propietario_bodega_bodega");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.PropietarioBodegas)
                    .HasForeignKey(d => d.IdPropietario)
                    .HasConstraintName("FK_propietario_bodega_propietarios");
            });

            modelBuilder.Entity<PropietarioDestinatario>(entity =>
            {
                entity.HasKey(e => e.IdDestinatarioPropietario);

                entity.ToTable("propietario_destinatario");

                entity.Property(e => e.IdDestinatarioPropietario).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .HasColumnName("apellido");

                entity.Property(e => e.Cargo)
                    .HasMaxLength(50)
                    .HasColumnName("cargo");

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(50)
                    .HasColumnName("correo_electronico");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("telefono");

                entity.Property(e => e.Telefono1)
                    .HasMaxLength(50)
                    .HasColumnName("telefono1");
            });

            modelBuilder.Entity<PropietarioReglasDet>(entity =>
            {
                entity.HasKey(e => e.IdReglaPropietarioDet);

                entity.ToTable("propietario_reglas_det");

                entity.Property(e => e.IdReglaPropietarioDet).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdDestinatarioPropietarioNavigation)
                    .WithMany(p => p.PropietarioReglasDets)
                    .HasForeignKey(d => d.IdDestinatarioPropietario)
                    .HasConstraintName("FK_propietario_reglas_det_propietario_destinatario");

                entity.HasOne(d => d.IdReglaPropietarioEncNavigation)
                    .WithMany(p => p.PropietarioReglasDets)
                    .HasForeignKey(d => d.IdReglaPropietarioEnc)
                    .HasConstraintName("FK_propietario_reglas_det_propietario_reglas_enc");
            });

            modelBuilder.Entity<PropietarioReglasEnc>(entity =>
            {
                entity.HasKey(e => e.IdReglaPropietarioEnc)
                    .HasName("PK_propietario_reglas");

                entity.ToTable("propietario_reglas_enc");

                entity.Property(e => e.IdReglaPropietarioEnc).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdMensajeReglaNavigation)
                    .WithMany(p => p.PropietarioReglasEncs)
                    .HasForeignKey(d => d.IdMensajeRegla)
                    .HasConstraintName("FK_propietario_reglas_enc_mensaje_regla");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.PropietarioReglasEncs)
                    .HasForeignKey(d => d.IdPropietario)
                    .HasConstraintName("FK_propietario_reglas_enc_propietarios");

                entity.HasOne(d => d.IdReglaRecepcionNavigation)
                    .WithMany(p => p.PropietarioReglasEncs)
                    .HasForeignKey(d => d.IdReglaRecepcion)
                    .HasConstraintName("FK_propietario_reglas_enc_reglas_recepcion");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor)
                    .HasName("PK_proveedor_1");

                entity.ToTable("proveedor");

                entity.HasIndex(e => e.IdPropietario, "NCLI_Proveedor_20200120_EJC");

                entity.Property(e => e.IdProveedor).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ActualizaCostoOc).HasColumnName("actualiza_costo_oc");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.Contacto)
                    .HasMaxLength(100)
                    .HasColumnName("contacto");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .HasColumnName("direccion")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EsBodegaRecepcion).HasColumnName("es_bodega_recepcion");

                entity.Property(e => e.EsBodegaTraslado).HasColumnName("es_bodega_traslado");

                entity.Property(e => e.EsProveedorServicio)
                    .HasColumnName("es_proveedor_servicio")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Idubicacionvirtual).HasColumnName("idubicacionvirtual");

                entity.Property(e => e.MuestraPrecio).HasColumnName("muestra_precio");

                entity.Property(e => e.Nit)
                    .HasMaxLength(25)
                    .HasColumnName("nit")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(25)
                    .HasColumnName("referencia");

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("telefono");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Proveedors)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_proveedor_empresa");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.Proveedors)
                    .HasForeignKey(d => d.IdPropietario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_proveedor_propietarios");
            });

            modelBuilder.Entity<ProveedorBodega>(entity =>
            {
                entity.HasKey(e => e.IdAsignacion);

                entity.ToTable("proveedor_bodega");

                entity.HasIndex(e => e.IdBodega, "NCL_Proveedor_Bodega_20200115_EJC");

                entity.Property(e => e.IdAsignacion).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.ProveedorBodegas)
                    .HasForeignKey(d => d.IdBodega)
                    .HasConstraintName("FK_proveedor_bodega_bodega");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.ProveedorBodegas)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("FK_proveedor_bodega_proveedor");
            });

            modelBuilder.Entity<RegimenFiscal>(entity =>
            {
                entity.HasKey(e => e.IdRegimen)
                    .HasName("PK_regimenes");

                entity.ToTable("regimen_fiscal");

                entity.Property(e => e.IdRegimen).ValueGeneratedNever();

                entity.Property(e => e.CodigoRegimen)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("codigo_regimen");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .HasColumnName("descripcion");

                entity.Property(e => e.DiasVencimiento).HasColumnName("dias_vencimiento");
            });

            modelBuilder.Entity<ReglaUbicDetIr>(entity =>
            {
                entity.HasKey(e => e.IdReglaUbicacionDetIr);

                entity.ToTable("regla_ubic_det_ir");

                entity.Property(e => e.IdReglaUbicacionDetIr).ValueGeneratedNever();

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdIndiceRotacionNavigation)
                    .WithMany(p => p.ReglaUbicDetIrs)
                    .HasForeignKey(d => d.IdIndiceRotacion)
                    .HasConstraintName("FK_regla_ubic_det_ir_indice_rotacion");

                entity.HasOne(d => d.IdReglaUbicacionEncNavigation)
                    .WithMany(p => p.ReglaUbicDetIrs)
                    .HasForeignKey(d => d.IdReglaUbicacionEnc)
                    .HasConstraintName("FK_regla_ubic_det_ir_regla_ubic_enc");
            });

            modelBuilder.Entity<ReglaUbicDetPe>(entity =>
            {
                entity.HasKey(e => e.IdReglaUbicacionDetPe);

                entity.ToTable("regla_ubic_det_pe");

                entity.Property(e => e.IdReglaUbicacionDetPe).ValueGeneratedNever();

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.ReglaUbicDetPes)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("FK_regla_ubic_det_pe_producto_estado");

                entity.HasOne(d => d.IdReglaUbicacionEncNavigation)
                    .WithMany(p => p.ReglaUbicDetPes)
                    .HasForeignKey(d => d.IdReglaUbicacionEnc)
                    .HasConstraintName("FK_regla_ubic_det_pe_regla_ubic_enc");
            });

            modelBuilder.Entity<ReglaUbicDetPp>(entity =>
            {
                entity.HasKey(e => e.IdReglaUbicacionDetPp);

                entity.ToTable("regla_ubic_det_pp");

                entity.Property(e => e.IdReglaUbicacionDetPp)
                    .ValueGeneratedNever()
                    .HasColumnName("IdReglaUbicacionDetPP");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.ReglaUbicDetPps)
                    .HasForeignKey(d => d.IdPresentacion)
                    .HasConstraintName("FK_regla_ubic_det_pp_producto_presentacion");

                entity.HasOne(d => d.IdReglaUbicacionEncNavigation)
                    .WithMany(p => p.ReglaUbicDetPps)
                    .HasForeignKey(d => d.IdReglaUbicacionEnc)
                    .HasConstraintName("FK_regla_ubic_det_pp_regla_ubic_enc");
            });

            modelBuilder.Entity<ReglaUbicDetProp>(entity =>
            {
                entity.HasKey(e => e.IdReglaUbicacionDetProp);

                entity.ToTable("regla_ubic_det_prop");

                entity.Property(e => e.IdReglaUbicacionDetProp).ValueGeneratedNever();

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdPropietarioBodegaNavigation)
                    .WithMany(p => p.ReglaUbicDetProps)
                    .HasForeignKey(d => d.IdPropietarioBodega)
                    .HasConstraintName("FK_regla_ubic_det_prop_propietario_bodega");

                entity.HasOne(d => d.IdReglaUbicacionEncNavigation)
                    .WithMany(p => p.ReglaUbicDetProps)
                    .HasForeignKey(d => d.IdReglaUbicacionEnc)
                    .HasConstraintName("FK_regla_ubic_det_prop_regla_ubic_enc");
            });

            modelBuilder.Entity<ReglaUbicDetTp>(entity =>
            {
                entity.HasKey(e => e.IdReglaUbicacoinTp);

                entity.ToTable("regla_ubic_det_tp");

                entity.Property(e => e.IdReglaUbicacoinTp)
                    .ValueGeneratedNever()
                    .HasColumnName("IdReglaUbicacoinTP");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdReglaUbicacionEncNavigation)
                    .WithMany(p => p.ReglaUbicDetTps)
                    .HasForeignKey(d => d.IdReglaUbicacionEnc)
                    .HasConstraintName("FK_regla_ubic_det_tp_regla_ubic_enc");

                entity.HasOne(d => d.IdTipoProductoNavigation)
                    .WithMany(p => p.ReglaUbicDetTps)
                    .HasForeignKey(d => d.IdTipoProducto)
                    .HasConstraintName("FK_regla_ubic_det_tp_producto_tipo");
            });

            modelBuilder.Entity<ReglaUbicDetTr>(entity =>
            {
                entity.HasKey(e => e.IdReglaUbicacionDetTr);

                entity.ToTable("regla_ubic_det_tr");

                entity.Property(e => e.IdReglaUbicacionDetTr)
                    .ValueGeneratedNever()
                    .HasColumnName("IdREglaUbicacionDetTr");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdReglaUbicacionEncNavigation)
                    .WithMany(p => p.ReglaUbicDetTrs)
                    .HasForeignKey(d => d.IdReglaUbicacionEnc)
                    .HasConstraintName("FK_regla_ubic_det_tr_regla_ubic_enc");

                entity.HasOne(d => d.IdTipoRotacionNavigation)
                    .WithMany(p => p.ReglaUbicDetTrs)
                    .HasForeignKey(d => d.IdTipoRotacion)
                    .HasConstraintName("FK_regla_ubic_det_tr_tipo_rotacion");
            });

            modelBuilder.Entity<ReglaUbicEnc>(entity =>
            {
                entity.HasKey(e => e.IdReglaUbicacionEnc);

                entity.ToTable("regla_ubic_enc");

                entity.Property(e => e.IdReglaUbicacionEnc).ValueGeneratedNever();

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<ReglaUbicPrioDet>(entity =>
            {
                entity.HasKey(e => e.IdReglaUbicPrioDet)
                    .HasName("PK_regla_ubic_prio_enc¿");

                entity.ToTable("regla_ubic_prio_det");

                entity.Property(e => e.IdReglaUbicPrioDet).ValueGeneratedNever();
            });

            modelBuilder.Entity<ReglaUbicPrioEnc>(entity =>
            {
                entity.HasKey(e => e.IdReglaUbicPrioEnc);

                entity.ToTable("regla_ubic_prio_enc");

                entity.Property(e => e.IdReglaUbicPrioEnc).ValueGeneratedNever();

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<ReglaUbicPrioParam>(entity =>
            {
                entity.HasKey(e => e.IdReglaUbicPrioParam);

                entity.ToTable("regla_ubic_prio_param");

                entity.Property(e => e.IdReglaUbicPrioParam).ValueGeneratedNever();

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Orden).HasDefaultValueSql("((0))");

                entity.Property(e => e.Tipo).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ReglaUbicPrioProducto>(entity =>
            {
                entity.HasKey(e => e.IdReglaUbicPrioProd)
                    .HasName("PK_regla_ubic_prio_ubic");

                entity.ToTable("regla_ubic_prio_producto");

                entity.Property(e => e.IdReglaUbicPrioProd).ValueGeneratedNever();
            });

            modelBuilder.Entity<ReglaUbicSel>(entity =>
            {
                entity.HasKey(e => new { e.IdUbicacion, e.IdReglaUbicacionEnc })
                    .HasName("PK_regla_ubic_orden_1");

                entity.ToTable("regla_ubic_sel");
            });

            modelBuilder.Entity<ReglaUbicSelDet>(entity =>
            {
                entity.HasKey(e => new { e.IdReglaUbicOrd, e.IdRegla })
                    .HasName("PK_regla_ubic_orden_det");

                entity.ToTable("regla_ubic_sel_det");

                entity.Property(e => e.IdReglaUbicOrd).HasColumnName("idReglaUbicOrd");

                entity.Property(e => e.IdRegla).HasColumnName("idRegla");

                entity.HasOne(d => d.IdReglaUbicOrdNavigation)
                    .WithMany(p => p.ReglaUbicSelDets)
                    .HasForeignKey(d => d.IdReglaUbicOrd)
                    .HasConstraintName("FK_regla_ubic_sel_det_regla_ubic_sel_enc");
            });

            modelBuilder.Entity<ReglaUbicSelEnc>(entity =>
            {
                entity.HasKey(e => e.IdReglaUbicacionEnc)
                    .HasName("PK_regla_ubic_orden");

                entity.ToTable("regla_ubic_sel_enc");

                entity.Property(e => e.IdReglaUbicacionEnc).ValueGeneratedNever();

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<ReglaUbicSelItem>(entity =>
            {
                entity.HasKey(e => e.IdRegla)
                    .HasName("PK_regla_ubic_orden_item");

                entity.ToTable("regla_ubic_sel_item");

                entity.Property(e => e.IdRegla)
                    .ValueGeneratedNever()
                    .HasColumnName("idRegla");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Orden).HasDefaultValueSql("((0))");

                entity.Property(e => e.Tipo).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ReglaUbicacion>(entity =>
            {
                entity.HasKey(e => new { e.IdUbicacion, e.IdReglaUbicacionEnc });

                entity.ToTable("regla_ubicacion");

                entity.Property(e => e.IdBodega).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdReglaUbicacionEncNavigation)
                    .WithMany(p => p.ReglaUbicacions)
                    .HasForeignKey(d => d.IdReglaUbicacionEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_regla_ubicacion_regla_ubic_enc");
            });

            modelBuilder.Entity<ReglasRecepcion>(entity =>
            {
                entity.HasKey(e => e.IdReglaRecepcion);

                entity.ToTable("reglas_recepcion");

                entity.Property(e => e.IdReglaRecepcion).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.Rechazar).HasComment("Indica si la recepción se rechaza en su totalidad por este motivo o regla");

                entity.Property(e => e.StockNoDisponible).HasComment("Si True, el stock no se inserta en stock, se queda en las tablas de recepción hasta que sea auditada la recepción y colocada como disponible.");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<ResolucionLpOperador>(entity =>
            {
                entity.HasKey(e => e.Idresolucionlp);

                entity.ToTable("resolucion_lp_operador");

                entity.Property(e => e.Idresolucionlp)
                    .ValueGeneratedNever()
                    .HasColumnName("idresolucionlp");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CorrelativoActual).HasColumnName("correlativo_actual");

                entity.Property(e => e.CorrelativoFinal).HasColumnName("correlativo_final");

                entity.Property(e => e.CorrelativoInicial).HasColumnName("correlativo_inicial");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Idbodega).HasColumnName("idbodega");

                entity.Property(e => e.Idoperador).HasColumnName("idoperador");

                entity.Property(e => e.Serie)
                    .HasMaxLength(50)
                    .HasColumnName("serie");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasKey(e => e.UniqueId);

                entity.Property(e => e.UniqueId).HasColumnName("UniqueID");

                entity.Property(e => e.Image).HasColumnType("image");

                entity.Property(e => e.ResourceId).HasColumnName("ResourceID");

                entity.Property(e => e.ResourceName).HasMaxLength(50);
            });

            modelBuilder.Entity<ResultadoAplicaReservado>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("resultado_aplica_reservado");

                entity.Property(e => e.CantidadF).HasColumnName("Cantidad_f");

                entity.Property(e => e.CantidadRes).HasColumnName("Cantidad_res");

                entity.Property(e => e.CantidadStock).HasColumnName("cantidad_stock");
            });

            modelBuilder.Entity<RoadPVendedor>(entity =>
            {
                entity.HasKey(e => new { e.IdRuta, e.IdVendedor, e.Codigo })
                    .HasName("PK_P_VENDEDOR");

                entity.ToTable("road_p_vendedor");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(8)
                    .HasColumnName("codigo");

                entity.Property(e => e.Bloqueado).HasColumnName("bloqueado");

                entity.Property(e => e.Bodega)
                    .HasMaxLength(15)
                    .HasColumnName("bodega");

                entity.Property(e => e.Clave)
                    .HasMaxLength(15)
                    .HasColumnName("clave");

                entity.Property(e => e.CodVehiculo)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("cod_vehiculo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DevolucionSap).HasColumnName("devolucion_sap");

                entity.Property(e => e.Liquidando)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("liquidando")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Nivel).HasColumnName("nivel");

                entity.Property(e => e.Nivelprecio).HasColumnName("nivelprecio");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.Ruta)
                    .HasMaxLength(15)
                    .HasColumnName("ruta");

                entity.Property(e => e.Subbodega)
                    .HasMaxLength(15)
                    .HasColumnName("subbodega");

                entity.Property(e => e.UltimaFechaLiq)
                    .HasColumnType("datetime")
                    .HasColumnName("ultima_fecha_liq")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<RoadRutum>(entity =>
            {
                entity.HasKey(e => e.IdRuta)
                    .HasName("PK_ruta");

                entity.ToTable("road_ruta");

                entity.Property(e => e.IdRuta).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("ACTIVO")
                    .HasDefaultValueSql("(N'S')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.AplicacionUsa).HasColumnName("APLICACION_USA");

                entity.Property(e => e.Bodega)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("BODEGA")
                    .HasDefaultValueSql("((1))")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Bonif)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("BONIF")
                    .HasDefaultValueSql("(N'S')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("CELULAR")
                    .HasDefaultValueSql("(N'N')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("CODIGO")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Comstat)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("COMSTAT")
                    .HasDefaultValueSql("('')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Descuento)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("DESCUENTO")
                    .HasDefaultValueSql("(N'S')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.DiluirBon).HasColumnName("DILUIR_BON");

                entity.Property(e => e.Editdesc)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("EDITDESC")
                    .HasDefaultValueSql("(N'N')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Editdevprec)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("EDITDEVPREC")
                    .HasDefaultValueSql("(N'N')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("EMAIL")
                    .HasDefaultValueSql("('')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EsRutaOficina).HasColumnName("ES_RUTA_OFICINA");

                entity.Property(e => e.Expstat)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("EXPSTAT")
                    .HasDefaultValueSql("('')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Forania)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("FORANIA")
                    .HasDefaultValueSql("('N')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Ftpfold)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("FTPFOLD")
                    .HasDefaultValueSql("('')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.HoraFin).HasColumnName("HORA_FIN");

                entity.Property(e => e.HoraIni).HasColumnName("HORA_INI");

                entity.Property(e => e.IdUbicacionTransito).HasComment("Ubicación en la que se despacha el producto entregado a la ruta");

                entity.Property(e => e.Impresion)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("IMPRESION")
                    .HasDefaultValueSql("(N'S')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Impstat)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("IMPSTAT")
                    .HasDefaultValueSql("('')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.IntentosLect).HasColumnName("INTENTOS_LECT");

                entity.Property(e => e.IntervaloMax).HasColumnName("INTERVALO_MAX");

                entity.Property(e => e.Kilometraje)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("KILOMETRAJE")
                    .HasDefaultValueSql("(N'N')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Lastcom).HasColumnName("LASTCOM");

                entity.Property(e => e.Lastexp).HasColumnName("LASTEXP");

                entity.Property(e => e.Lastimp).HasColumnName("LASTIMP");

                entity.Property(e => e.LecturasValid).HasColumnName("LECTURAS_VALID");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("NOMBRE")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Objano).HasColumnName("OBJANO");

                entity.Property(e => e.Objmes).HasColumnName("OBJMES");

                entity.Property(e => e.Oferta)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("OFERTA")
                    .HasDefaultValueSql("(N'N')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Param1)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("PARAM1")
                    .HasDefaultValueSql("('')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Param2)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("PARAM2")
                    .HasDefaultValueSql("('')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Params)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("PARAMS")
                    .HasDefaultValueSql("('')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Pasarcredito)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("PASARCREDITO")
                    .HasDefaultValueSql("(N'N')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Percrent).HasColumnName("PERCRENT");

                entity.Property(e => e.Pesolim).HasColumnName("PESOLIM");

                entity.Property(e => e.PreimpresionFactura).HasColumnName("PREIMPRESION_FACTURA");

                entity.Property(e => e.PuertoGps).HasColumnName("PUERTO_GPS");

                entity.Property(e => e.Recibopropio)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("RECIBOPROPIO")
                    .HasDefaultValueSql("(N'N')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Rentabil)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("RENTABIL")
                    .HasDefaultValueSql("(N'N')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Semana).HasColumnName("SEMANA");

                entity.Property(e => e.Subbodega)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("SUBBODEGA")
                    .HasDefaultValueSql("((1))")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Subtipo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("SUBTIPO")
                    .HasDefaultValueSql("(N'POLLOS')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Sucursal)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("SUCURSAL")
                    .HasDefaultValueSql("((3900))")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Syncfold)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("SYNCFOLD")
                    .HasDefaultValueSql("('')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Teclado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("TECLADO")
                    .HasDefaultValueSql("(N'S')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("TIPO")
                    .HasDefaultValueSql("(N'POLLOS')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Vendedor)
                    .IsRequired()
                    .HasMaxLength(8)
                    .HasColumnName("VENDEDOR")
                    .HasDefaultValueSql("('9999')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Venta)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("VENTA")
                    .HasDefaultValueSql("('V')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Wlfold)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("WLFOLD")
                    .HasDefaultValueSql("('')")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK_rol_1");

                entity.ToTable("rol");

                entity.Property(e => e.IdRol).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.RegistrarClaveAutorizacion)
                    .HasColumnName("registrar_clave_autorizacion")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(30)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Rols)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rol_empresa");
            });

            modelBuilder.Entity<RolBodega>(entity =>
            {
                entity.HasKey(e => e.IdRolBodega);

                entity.ToTable("rol_bodega");

                entity.Property(e => e.IdRolBodega).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.RolBodegas)
                    .HasForeignKey(d => d.IdBodega)
                    .HasConstraintName("FK_rol_bodega_bodega");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolBodegas)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_rol_bodega_rol");
            });

            modelBuilder.Entity<RolMenu>(entity =>
            {
                entity.HasKey(e => e.IdMenuRol)
                    .HasName("PK_menu_rol_1");

                entity.ToTable("rol_menu");

                entity.Property(e => e.IdMenuRol).ValueGeneratedNever();

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.IdMenu)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UsuarioAgr)
                    .HasMaxLength(50)
                    .HasColumnName("usuario_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Visible).HasColumnName("visible");

                entity.HasOne(d => d.IdMenuNavigation)
                    .WithMany(p => p.RolMenus)
                    .HasForeignKey(d => d.IdMenu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rol_menu_menu_sistema");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolMenus)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rol_menu_rol");
            });

            modelBuilder.Entity<RolOperador>(entity =>
            {
                entity.HasKey(e => e.IdRolOperador);

                entity.ToTable("rol_operador");

                entity.Property(e => e.IdRolOperador).ValueGeneratedNever();

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<SimbologiasCodigoBarra>(entity =>
            {
                entity.HasKey(e => e.IdSimbologia);

                entity.ToTable("simbologias_codigo_barra");

                entity.Property(e => e.IdSimbologia).ValueGeneratedNever();

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<SisEstadoTareaHh>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.ToTable("sis_estado_tarea_hh");

                entity.Property(e => e.IdEstado).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<SisObsLog>(entity =>
            {
                entity.HasKey(e => e.IdObservacion);

                entity.ToTable("sis_obs_log");

                entity.Property(e => e.IdObservacion).ValueGeneratedNever();

                entity.Property(e => e.Descripcion).IsRequired();
            });

            modelBuilder.Entity<SisPrioridadTareaHh>(entity =>
            {
                entity.HasKey(e => e.IdPrioridad);

                entity.ToTable("sis_prioridad_tarea_hh");

                entity.Property(e => e.IdPrioridad).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<SisTipoAccion>(entity =>
            {
                entity.HasKey(e => e.IdTipoAccion);

                entity.ToTable("sis_tipo_accion");

                entity.Property(e => e.IdTipoAccion).ValueGeneratedNever();

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<SisTipoTarea>(entity =>
            {
                entity.HasKey(e => e.IdTipoTarea)
                    .HasName("PK_sis_tipo_tarea_hh");

                entity.ToTable("sis_tipo_tarea");

                entity.Property(e => e.IdTipoTarea).ValueGeneratedNever();

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => e.IdStock);

                entity.ToTable("stock");

                entity.HasIndex(e => e.IdPropietarioBodega, "NCLI_STOCK_20191205_EJC");

                entity.HasIndex(e => e.IdUbicacion, "NCLI_Stock_20191210_EJC");

                entity.HasIndex(e => e.IdPropietarioBodega, "NCLI_Stock_20200112_EJC");

                entity.HasIndex(e => e.IdProductoBodega, "NCLI_Stock_20200115_EJC");

                entity.Property(e => e.IdStock).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.AtributoVariante1)
                    .HasMaxLength(25)
                    .HasColumnName("atributo_variante_1");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaManufactura)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_manufactura");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lote")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.NoBulto).HasColumnName("no_bulto");

                entity.Property(e => e.PalletNoEstandar)
                    .HasColumnName("pallet_no_estandar")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Peso)
                    .HasColumnName("peso")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.Temperatura)
                    .HasColumnName("temperatura")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UdsLicPlate).HasColumnName("uds_lic_plate");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdPedidoEncNavigation)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.IdPedidoEnc)
                    .HasConstraintName("FK_stock_trans_pe_enc");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.IdPresentacion)
                    .HasConstraintName("FK_stock_producto_presentacion");

                entity.HasOne(d => d.IdProductoBodegaNavigation)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.IdProductoBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stock_producto_bodega");

                entity.HasOne(d => d.IdProductoEstadoNavigation)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.IdProductoEstado)
                    .HasConstraintName("FK_stock_producto_estado");

                entity.HasOne(d => d.IdPropietarioBodegaNavigation)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.IdPropietarioBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stock_propietario_bodega");

                entity.HasOne(d => d.IdUnidadMedidaNavigation)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.IdUnidadMedida)
                    .HasConstraintName("FK_stock_unidad_medida");

                entity.HasOne(d => d.IdRecepcion)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => new { d.IdRecepcionDet, d.IdRecepcionEnc })
                    .HasConstraintName("FK_stock_trans_re_det");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => new { d.IdUbicacion, d.IdBodega })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stock_bodega_ubicacion");
            });

            modelBuilder.Entity<StockDet>(entity =>
            {
                entity.HasKey(e => e.IdStock);

                entity.ToTable("stock_det");

                entity.Property(e => e.IdStock).ValueGeneratedNever();

                entity.Property(e => e.Posiciones).HasColumnName("posiciones");
            });

            modelBuilder.Entity<StockHist>(entity =>
            {
                entity.HasKey(e => e.IdStockHist);

                entity.ToTable("stock_hist");

                entity.Property(e => e.IdStockHist).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Cantidad)
                    .HasColumnName("cantidad")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaManufactura)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_manufactura");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoBulto)
                    .HasMaxLength(20)
                    .HasColumnName("no_bulto");

                entity.Property(e => e.Peso)
                    .HasColumnName("peso")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Posiciones)
                    .HasColumnName("posiciones")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.Temperatura)
                    .HasColumnName("temperatura")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UdsLicPlate).HasColumnName("uds_lic_plate");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdPedidoEncNavigation)
                    .WithMany(p => p.StockHists)
                    .HasForeignKey(d => d.IdPedidoEnc)
                    .HasConstraintName("FK_stock_hist_trans_pe_enc");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.StockHists)
                    .HasForeignKey(d => d.IdPresentacion)
                    .HasConstraintName("FK_stock_hist_producto_presentacion");

                entity.HasOne(d => d.IdProductoBodegaNavigation)
                    .WithMany(p => p.StockHists)
                    .HasForeignKey(d => d.IdProductoBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stock_hist_producto_bodega");

                entity.HasOne(d => d.IdProductoEstadoNavigation)
                    .WithMany(p => p.StockHists)
                    .HasForeignKey(d => d.IdProductoEstado)
                    .HasConstraintName("FK_stock_hist_producto_estado");

                entity.HasOne(d => d.IdPropietarioBodegaNavigation)
                    .WithMany(p => p.StockHists)
                    .HasForeignKey(d => d.IdPropietarioBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stock_hist_propietario_bodega");

                entity.HasOne(d => d.IdUnidadMedidaNavigation)
                    .WithMany(p => p.StockHists)
                    .HasForeignKey(d => d.IdUnidadMedida)
                    .HasConstraintName("FK_stock_hist_unidad_medida");

                entity.HasOne(d => d.IdRecepcion)
                    .WithMany(p => p.StockHists)
                    .HasForeignKey(d => new { d.IdRecepcionDet, d.IdRecepcionEnc })
                    .HasConstraintName("FK_stock_hist_trans_re_det");
            });

            modelBuilder.Entity<StockJornadum>(entity =>
            {
                entity.HasKey(e => e.IdStockJornada);

                entity.ToTable("stock_jornada");

                entity.Property(e => e.IdStockJornada).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.AtributoVariante1)
                    .HasMaxLength(25)
                    .HasColumnName("atributo_variante_1");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Bodega).HasMaxLength(50);

                entity.Property(e => e.CajasPorCama).HasDefaultValueSql("((0))");

                entity.Property(e => e.CamasPorTarima).HasDefaultValueSql("((0))");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CantidadIngresoAfectaASalida)
                    .HasColumnName("Cantidad_Ingreso_Afecta_A_Salida")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Clasificacion).HasMaxLength(100);

                entity.Property(e => e.CodigoBarraProducto)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra_producto");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.CodigoRegimen)
                    .HasMaxLength(20)
                    .HasColumnName("codigo_regimen");

                entity.Property(e => e.CostoUnitario)
                    .HasColumnName("costo_unitario")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DiasVencimientoRegimen).HasColumnName("dias_vencimiento_regimen");

                entity.Property(e => e.EsRetroactivo)
                    .HasColumnName("es_retroactivo")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Existencia).HasColumnName("existencia");

                entity.Property(e => e.Factor)
                    .HasColumnName("factor")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.FechaAgrego)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Agrego");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaIngresoTicketTms)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso_ticket_tms")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaManufactura)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_manufactura");

                entity.Property(e => e.FechaProcesadoStockJornada)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_procesado_stock_jornada");

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Recepcion");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.IdTicketTms)
                    .HasColumnName("IdTicketTMS")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoBulto).HasColumnName("no_bulto");

                entity.Property(e => e.NoDocumentoOc)
                    .HasMaxLength(30)
                    .HasColumnName("No_DocumentoOC");

                entity.Property(e => e.NoDocumentoRec)
                    .HasMaxLength(50)
                    .HasColumnName("No_DocumentoRec");

                entity.Property(e => e.NoPoliza)
                    .HasMaxLength(50)
                    .HasColumnName("no_poliza");

                entity.Property(e => e.NomEstadoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("nom_estado_producto");

                entity.Property(e => e.NomPresentacionProducto)
                    .HasMaxLength(50)
                    .HasColumnName("nom_presentacion_producto");

                entity.Property(e => e.NomUmBas)
                    .HasMaxLength(50)
                    .HasColumnName("nom_umBas");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .HasColumnName("nombre_producto");

                entity.Property(e => e.NombreRegimen)
                    .HasMaxLength(500)
                    .HasColumnName("nombre_regimen");

                entity.Property(e => e.NumeroOrden)
                    .HasMaxLength(50)
                    .HasColumnName("numero_orden");

                entity.Property(e => e.PalletNoEstandar).HasColumnName("pallet_no_estandar");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.PesoNeto).HasColumnName("peso_neto");

                entity.Property(e => e.Posiciones).HasColumnName("posiciones");

                entity.Property(e => e.Propietario).HasMaxLength(100);

                entity.Property(e => e.Proveedor).HasMaxLength(100);

                entity.Property(e => e.ReferenciaOc)
                    .HasMaxLength(100)
                    .HasColumnName("ReferenciaOC");

                entity.Property(e => e.Regimen).HasMaxLength(20);

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.StockJornadaHash)
                    .HasMaxLength(150)
                    .HasColumnName("Stock_Jornada_Hash")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Temperatura).HasColumnName("temperatura");

                entity.Property(e => e.TipoTrans).HasMaxLength(25);

                entity.Property(e => e.UbicacionOrigen)
                    .HasMaxLength(200)
                    .HasColumnName("ubicacion_origen");

                entity.Property(e => e.UdsLicPlate).HasColumnName("uds_lic_plate");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.Property(e => e.ValorAduana).HasColumnName("valor_aduana");

                entity.Property(e => e.ValorDai).HasColumnName("valor_dai");

                entity.Property(e => e.ValorFlete).HasColumnName("valor_flete");

                entity.Property(e => e.ValorFob).HasColumnName("valor_fob");

                entity.Property(e => e.ValorIva).HasColumnName("valor_iva");

                entity.Property(e => e.ValorSeguro).HasColumnName("valor_seguro");
            });

            modelBuilder.Entity<StockParametro>(entity =>
            {
                entity.HasKey(e => e.IdStockParametro);

                entity.ToTable("stock_parametro");

                entity.Property(e => e.IdStockParametro).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.ValorFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("valor_fecha");

                entity.Property(e => e.ValorLogico).HasColumnName("valor_logico");

                entity.Property(e => e.ValorNumerico).HasColumnName("valor_numerico");

                entity.Property(e => e.ValorTexto)
                    .HasMaxLength(50)
                    .HasColumnName("valor_texto")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdProductoParametroNavigation)
                    .WithMany(p => p.StockParametros)
                    .HasForeignKey(d => d.IdProductoParametro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stock_parametro_producto_parametros");

                entity.HasOne(d => d.IdStockNavigation)
                    .WithMany(p => p.StockParametros)
                    .HasForeignKey(d => d.IdStock)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stock_parametro_stock");
            });

            modelBuilder.Entity<StockRe>(entity =>
            {
                entity.HasKey(e => e.IdStockRes)
                    .HasName("PK_stock_pe");

                entity.ToTable("stock_res");

                entity.Property(e => e.IdStockRes).ValueGeneratedNever();

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaManufactura)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_manufactura");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.Host)
                    .HasMaxLength(50)
                    .HasColumnName("host");

                entity.Property(e => e.Indicador)
                    .HasMaxLength(50)
                    .HasComment("Debe indicar el tipo de transacción que reserva el stock PE,UB,CE,TR");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NoBulto).HasColumnName("no_bulto");

                entity.Property(e => e.PalletNoEstandar).HasColumnName("pallet_no_estandar");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.UbicacionAnt)
                    .HasMaxLength(25)
                    .HasColumnName("ubicacion_ant")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UdsLicPlate).HasColumnName("uds_lic_plate");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<StockRec>(entity =>
            {
                entity.HasKey(e => e.IdStockRec);

                entity.ToTable("stock_rec");

                entity.Property(e => e.IdStockRec).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.AtributoVariante1)
                    .HasMaxLength(25)
                    .HasColumnName("atributo_variante_1");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaManufactura)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_manufactura");

                entity.Property(e => e.FechaRegularizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_regularizacion");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.Impreso)
                    .HasColumnName("impreso")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LicPlate)
                    .IsUnicode(false)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lote")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.NoBulto).HasColumnName("no_bulto");

                entity.Property(e => e.NoLinea).HasColumnName("no_linea");

                entity.Property(e => e.PalletNoEstandar)
                    .HasColumnName("pallet_no_estandar")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Peso)
                    .HasColumnName("peso")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Regularizado)
                    .HasColumnName("regularizado")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.Temperatura)
                    .HasColumnName("temperatura")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UdsLicPlate).HasColumnName("uds_lic_plate");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.StockRecs)
                    .HasForeignKey(d => d.IdPresentacion)
                    .HasConstraintName("FK_stock_rec_producto_presentacion");

                entity.HasOne(d => d.IdProductoBodegaNavigation)
                    .WithMany(p => p.StockRecs)
                    .HasForeignKey(d => d.IdProductoBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stock_rec_producto_bodega");

                entity.HasOne(d => d.IdProductoEstadoNavigation)
                    .WithMany(p => p.StockRecs)
                    .HasForeignKey(d => d.IdProductoEstado)
                    .HasConstraintName("FK_stock_rec_producto_estado");

                entity.HasOne(d => d.IdPropietarioBodegaNavigation)
                    .WithMany(p => p.StockRecs)
                    .HasForeignKey(d => d.IdPropietarioBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stock_rec_propietario_bodega");

                entity.HasOne(d => d.IdUnidadMedidaNavigation)
                    .WithMany(p => p.StockRecs)
                    .HasForeignKey(d => d.IdUnidadMedida)
                    .HasConstraintName("FK_stock_rec_unidad_medida");

                entity.HasOne(d => d.IdRecepcion)
                    .WithMany(p => p.StockRecs)
                    .HasForeignKey(d => new { d.IdRecepcionDet, d.IdRecepcionEnc })
                    .HasConstraintName("FK_stock_rec_trans_re_det");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.StockRecs)
                    .HasForeignKey(d => new { d.IdUbicacion, d.IdBodega })
                    .HasConstraintName("FK_stock_rec_bodega_ubicacion");
            });

            modelBuilder.Entity<StockResSe>(entity =>
            {
                entity.HasKey(e => e.IdStockResSe);

                entity.ToTable("stock_res_se");

                entity.Property(e => e.IdStockResSe).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Indicador).HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<StockSe>(entity =>
            {
                entity.HasKey(e => e.IdStockSe);

                entity.ToTable("stock_se");

                entity.Property(e => e.IdStockSe).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.NoSerie).HasMaxLength(50);

                entity.Property(e => e.NoSerieFinal).HasMaxLength(50);

                entity.Property(e => e.NoSerieInicial).HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdProductoBodegaNavigation)
                    .WithMany(p => p.StockSes)
                    .HasForeignKey(d => d.IdProductoBodega)
                    .HasConstraintName("FK_stock_se_producto_bodega");

                entity.HasOne(d => d.IdStockNavigation)
                    .WithMany(p => p.StockSes)
                    .HasForeignKey(d => d.IdStock)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stock_se_stock");
            });

            modelBuilder.Entity<StockSeRec>(entity =>
            {
                entity.HasKey(e => e.IdStockSeRec);

                entity.ToTable("stock_se_rec");

                entity.Property(e => e.IdStockSeRec).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaRegularizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_regularizacion");

                entity.Property(e => e.NoSerie).HasMaxLength(50);

                entity.Property(e => e.NoSerieFinal).HasMaxLength(50);

                entity.Property(e => e.NoSerieInicial).HasMaxLength(50);

                entity.Property(e => e.Regularizado).HasColumnName("regularizado");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdProductoBodegaNavigation)
                    .WithMany(p => p.StockSeRecs)
                    .HasForeignKey(d => d.IdProductoBodega)
                    .HasConstraintName("FK_stock_se_rec_producto_bodega");

                entity.HasOne(d => d.IdStockRecNavigation)
                    .WithMany(p => p.StockSeRecs)
                    .HasForeignKey(d => d.IdStockRec)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stock_se_rec_stock_rec");
            });

            modelBuilder.Entity<StockTransito>(entity =>
            {
                entity.HasKey(e => e.IdStockTransito);

                entity.ToTable("stock_transito");

                entity.Property(e => e.IdStockTransito).ValueGeneratedNever();

                entity.Property(e => e.CantidadRecibida).HasColumnName("Cantidad_Recibida");

                entity.Property(e => e.FechaAgregado)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Agregado")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Ingreso");

                entity.Property(e => e.FechaManufactura)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Manufactura");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Vence");

                entity.Property(e => e.IdOrdenCompraEncBodDest).HasColumnName("IdOrdenCompraEnc_BodDest");

                entity.Property(e => e.IdRecepcionEncBodDest).HasColumnName("IdRecepcionEnc_BodDest");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("Lic_Plate");

                entity.Property(e => e.Lote).HasMaxLength(50);
            });

            modelBuilder.Entity<TProductoBodega>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("t_producto_bodega");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<TablasSync>(entity =>
            {
                entity.HasKey(e => e.IdTabla);

                entity.ToTable("tablas_sync");

                entity.Property(e => e.IdTabla).ValueGeneratedNever();

                entity.Property(e => e.NombreTabla).HasMaxLength(50);

                entity.Property(e => e.Sincronizar).HasDefaultValueSql("((1))");

                entity.Property(e => e.UltimaSincronizacion)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<TareaHh>(entity =>
            {
                entity.HasKey(e => e.IdTareahh);

                entity.ToTable("tarea_hh");

                entity.Property(e => e.IdTareahh).ValueGeneratedNever();

                entity.Property(e => e.Asunto).HasMaxLength(50);

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.FechaFin).HasColumnType("datetime");

                entity.Property(e => e.FechaInicio).HasColumnType("datetime");

                entity.Property(e => e.IdEstado).HasComment("Este campo va ir cambiando dependiendo en donde vaya la transaccion, nuevo, pendiente, finalizado. - definir tabla");

                entity.Property(e => e.IdMuelle).HasComment("este campo tendra referencia hacia la tabla bodega_muelles - IdMuelle");

                entity.Property(e => e.IdPrioridad).HasComment("Este campo se convertira en la prioridad de la tarea - definir tabla");

                entity.Property(e => e.Ubicacion).HasMaxLength(50);

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.TareaHhs)
                    .HasForeignKey(d => d.IdBodega)
                    .HasConstraintName("FK_tarea_hh_bodega");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TareaHhs)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("FK_tarea_hh_sis_estado_tarea_hh");

                entity.HasOne(d => d.IdMuelleNavigation)
                    .WithMany(p => p.TareaHhs)
                    .HasForeignKey(d => d.IdMuelle)
                    .HasConstraintName("FK_tarea_hh_bodega_muelles");

                entity.HasOne(d => d.IdPrioridadNavigation)
                    .WithMany(p => p.TareaHhs)
                    .HasForeignKey(d => d.IdPrioridad)
                    .HasConstraintName("FK_tarea_hh_sis_prioridad_tarea_hh");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.TareaHhs)
                    .HasForeignKey(d => d.IdPropietario)
                    .HasConstraintName("FK_tarea_hh_propietarios");

                entity.HasOne(d => d.IdTipoTareaNavigation)
                    .WithMany(p => p.TareaHhs)
                    .HasForeignKey(d => d.IdTipoTarea)
                    .HasConstraintName("FK_tarea_hh_sis_tipo_tarea");
            });

            modelBuilder.Entity<TarifaTipoTransaccion>(entity =>
            {
                entity.HasKey(e => e.IdTipoTransaccion)
                    .HasName("PK_transaccion_tipo");

                entity.ToTable("tarifa_tipo_transaccion");

                entity.Property(e => e.IdTipoTransaccion).ValueGeneratedNever();

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TarifaTipoTransaccionDet>(entity =>
            {
                entity.HasKey(e => new { e.IdTipoTransaccion, e.IdServicio })
                    .HasName("PK_transaccion_servicio_det");

                entity.ToTable("tarifa_tipo_transaccion_det");
            });

            modelBuilder.Entity<Tarima>(entity =>
            {
                entity.HasKey(e => e.IdTarima);

                entity.ToTable("tarimas");

                entity.Property(e => e.IdTarima).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.Disponible).HasColumnName("disponible");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Tarimas)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK_tarimas_empresa");

                entity.HasOne(d => d.IdTipoTarimaNavigation)
                    .WithMany(p => p.Tarimas)
                    .HasForeignKey(d => d.IdTipoTarima)
                    .HasConstraintName("FK_tarimas_tipo_tarima");
            });

            modelBuilder.Entity<TempComparacionInventario>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TempComparacionInventario");

                entity.Property(e => e.CantidadStock).HasColumnName("Cantidad_Stock");

                entity.Property(e => e.Codigo).HasMaxLength(150);

                entity.Property(e => e.EntradasSalidas).HasColumnName("Entradas_Salidas");

                entity.Property(e => e.FechaVence).HasColumnType("date");

                entity.Property(e => e.Lote).HasMaxLength(50);

                entity.Property(e => e.PesoStock).HasColumnName("Peso_Stock");

                entity.Property(e => e.Producto).HasMaxLength(250);
            });

            modelBuilder.Entity<TempComparativoInventario>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TempComparativoInventario");

                entity.Property(e => e.CantidadStock).HasColumnName("Cantidad_Stock");

                entity.Property(e => e.Codigo).HasMaxLength(150);

                entity.Property(e => e.EntradasSalidas).HasColumnName("Entradas_Salidas");

                entity.Property(e => e.FechaVence).HasColumnType("date");

                entity.Property(e => e.Lote).HasMaxLength(200);

                entity.Property(e => e.PesoStock).HasColumnName("Peso_Stock");

                entity.Property(e => e.Producto).HasMaxLength(250);
            });

            modelBuilder.Entity<TempLicenciaLlave>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("temp_licencia_llave");

                entity.Property(e => e.IdLlave).HasColumnName("idLlave");

                entity.Property(e => e.Llave)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<TipoActualizacionCosto>(entity =>
            {
                entity.HasKey(e => e.IdTipoActualizacionCosto);

                entity.ToTable("tipo_actualizacion_costo");

                entity.Property(e => e.IdTipoActualizacionCosto).ValueGeneratedNever();

                entity.Property(e => e.NombreActualizacionCosto)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TipoContenedor>(entity =>
            {
                entity.HasKey(e => e.IdTipoContenedor);

                entity.ToTable("tipo_contenedor");

                entity.Property(e => e.IdTipoContenedor).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Alto).HasComment("Metros");

                entity.Property(e => e.Ancho).HasComment("Metros");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Largo).HasComment("Metros");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Pies).HasComment("Pies");

                entity.Property(e => e.Tara).HasComment("Peso del contenedor sin carga");

                entity.Property(e => e.Tonealadas).HasComment("Toneladas");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.Property(e => e.VolumenUtil).HasComment("metros cúbicos");
            });

            modelBuilder.Entity<TipoConteo>(entity =>
            {
                entity.HasKey(e => e.IdTipoConteo)
                    .HasName("PK__TipoCont__75071D5AEF19E1ED");

                entity.ToTable("TipoConteo");

                entity.Property(e => e.IdTipoConteo).ValueGeneratedNever();

                entity.Property(e => e.Descripcion).HasMaxLength(100);
            });

            modelBuilder.Entity<TipoEtiquetum>(entity =>
            {
                entity.HasKey(e => e.IdTipoEtiqueta);

                entity.ToTable("tipo_etiqueta");

                entity.Property(e => e.IdTipoEtiqueta).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("date")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("date")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<TipoInventario>(entity =>
            {
                entity.HasKey(e => e.IdTipoInv)
                    .HasName("PK__TipoInve__3C69A8E3BC78C291");

                entity.ToTable("TipoInventario");

                entity.Property(e => e.IdTipoInv).ValueGeneratedNever();

                entity.Property(e => e.Descripcion).HasMaxLength(100);
            });

            modelBuilder.Entity<TipoRack>(entity =>
            {
                entity.HasKey(e => e.IdTipoRack);

                entity.ToTable("tipo_rack");

                entity.Property(e => e.IdTipoRack).ValueGeneratedNever();

                entity.Property(e => e.CantidadPosicionesAncho).HasColumnName("cantidad_posiciones_ancho");

                entity.Property(e => e.CantidadPosicionesProfundo).HasColumnName("cantidad_posiciones_profundo");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoRotacion>(entity =>
            {
                entity.HasKey(e => e.IdTipoRotacion);

                entity.ToTable("tipo_rotacion");

                entity.Property(e => e.IdTipoRotacion).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TipoTareaTiempo>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdBodega, e.IdTipoTarea });

                entity.ToTable("tipo_tarea_tiempos");
            });

            modelBuilder.Entity<TipoTarima>(entity =>
            {
                entity.HasKey(e => e.IdTipoTarima);

                entity.ToTable("tipo_tarima");

                entity.Property(e => e.IdTipoTarima).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.EntradasTransPaleta).HasComment("Cuantas entradas tiene para ser tomado por el montacargas o carretilla");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("date")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("date")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.PesoPromedio).HasComment("Cuanto pesa la tarima");

                entity.Property(e => e.Tara).HasComment(" peso del contenedor ó empaque (tara)");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<TmpBodegaUbicacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tmp_bodega_ubicacion");

                entity.Property(e => e.AceptaPallet).HasColumnName("acepta_pallet");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.Bloqueada).HasColumnName("bloqueada");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.CodigoBarra2)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_barra2");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.IndiceX).HasColumnName("indice_x");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.MargenDerecho).HasColumnName("margen_derecho");

                entity.Property(e => e.MargenInferior).HasColumnName("margen_inferior");

                entity.Property(e => e.MargenIzquierdo).HasColumnName("margen_izquierdo");

                entity.Property(e => e.MargenSuperior).HasColumnName("margen_superior");

                entity.Property(e => e.Nivel).HasColumnName("nivel");

                entity.Property(e => e.OrientacionPos)
                    .HasMaxLength(50)
                    .HasColumnName("orientacion_pos");

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.UbicacionDespacho).HasColumnName("ubicacion_despacho");

                entity.Property(e => e.UbicacionMerma).HasColumnName("ubicacion_merma");

                entity.Property(e => e.UbicacionNe).HasColumnName("ubicacion_ne");

                entity.Property(e => e.UbicacionPicking).HasColumnName("ubicacion_picking");

                entity.Property(e => e.UbicacionRecepcion).HasColumnName("ubicacion_recepcion");

                entity.Property(e => e.UbicacionVirtual).HasColumnName("ubicacion_virtual");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<TmpEstructuraUbicacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tmp_estructura_ubicacion");

                entity.Property(e => e.AceptaPallet).HasColumnName("acepta_pallet");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.Bloqueada).HasColumnName("bloqueada");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(25)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.CodigoBarra2)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_barra2");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.IndiceX).HasColumnName("indice_x");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.MargenDerecho).HasColumnName("margen_derecho");

                entity.Property(e => e.MargenInferior).HasColumnName("margen_inferior");

                entity.Property(e => e.MargenIzquierdo).HasColumnName("margen_izquierdo");

                entity.Property(e => e.MargenSuperior).HasColumnName("margen_superior");

                entity.Property(e => e.Nivel).HasColumnName("nivel");

                entity.Property(e => e.OrientacionPos)
                    .HasMaxLength(50)
                    .HasColumnName("orientacion_pos");

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.UbicacionDespacho).HasColumnName("ubicacion_despacho");

                entity.Property(e => e.UbicacionMerma).HasColumnName("ubicacion_merma");

                entity.Property(e => e.UbicacionPicking).HasColumnName("ubicacion_picking");

                entity.Property(e => e.UbicacionRecepcion).HasColumnName("ubicacion_recepcion");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<TmpHouseDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tmp_house_data");

                entity.Property(e => e.CodigoCliente)
                    .HasMaxLength(150)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(150)
                    .HasColumnName("Codigo_Producto")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FechaPedido).HasColumnType("date");
            });

            modelBuilder.Entity<TmpINavTransaccionesOut>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tmp_i_nav_transacciones_out");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CantidadEsperada).HasColumnName("Cantidad_Esperada");

                entity.Property(e => e.CantidadPresentacion).HasColumnName("cantidad_presentacion");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.CodigoVariante)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_variante");

                entity.Property(e => e.Enviado).HasColumnName("enviado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_recepcion");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("date")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.Idbodega).HasColumnName("idbodega");

                entity.Property(e => e.Iddespachoenc).HasColumnName("iddespachoenc");

                entity.Property(e => e.Idempresa).HasColumnName("idempresa");

                entity.Property(e => e.Idordencompra).HasColumnName("idordencompra");

                entity.Property(e => e.Idpedidoenc).HasColumnName("idpedidoenc");

                entity.Property(e => e.Idpresentacion).HasColumnName("idpresentacion");

                entity.Property(e => e.Idproducto).HasColumnName("idproducto");

                entity.Property(e => e.Idproductobodega).HasColumnName("idproductobodega");

                entity.Property(e => e.Idproductoestado).HasColumnName("idproductoestado");

                entity.Property(e => e.Idpropietario).HasColumnName("idpropietario");

                entity.Property(e => e.Idpropietariobodega).HasColumnName("idpropietariobodega");

                entity.Property(e => e.Idrecepcionenc).HasColumnName("idrecepcionenc");

                entity.Property(e => e.Idtransaccion).HasColumnName("idtransaccion");

                entity.Property(e => e.Idunidadmedida).HasColumnName("idunidadmedida");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoLinea)
                    .HasMaxLength(50)
                    .HasColumnName("no_linea");

                entity.Property(e => e.NoPedido)
                    .HasMaxLength(50)
                    .HasColumnName("no_pedido");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(150)
                    .HasColumnName("nombre_producto");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.TipoTransaccion)
                    .HasMaxLength(50)
                    .HasColumnName("tipo_transaccion");

                entity.Property(e => e.UdsLicPlate).HasColumnName("uds_lic_plate");

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .HasColumnName("unidad_medida");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<TmpLicenciaItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tmp_licencia_item");

                entity.Property(e => e.Bandera).HasColumnName("bandera");

                entity.Property(e => e.Estado)
                    .HasMaxLength(200)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaSistema)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_sistema");

                entity.Property(e => e.IdDisp)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("idDisp");

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(200)
                    .HasColumnName("identificacion");

                entity.Property(e => e.Tipo).HasColumnName("tipo");
            });

            modelBuilder.Entity<TmpStock13540>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tmp_stock_13540");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.AtributoVariante1)
                    .HasMaxLength(25)
                    .HasColumnName("atributo_variante_1");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaManufactura)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_manufactura");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoBulto).HasColumnName("no_bulto");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.Temperatura).HasColumnName("temperatura");

                entity.Property(e => e.UdsLicPlate).HasColumnName("uds_lic_plate");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<TmpStock18782>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tmp_stock_18782");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.AtributoVariante1)
                    .HasMaxLength(25)
                    .HasColumnName("atributo_variante_1");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaManufactura)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_manufactura");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoBulto).HasColumnName("no_bulto");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.Temperatura).HasColumnName("temperatura");

                entity.Property(e => e.UdsLicPlate).HasColumnName("uds_lic_plate");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<TmpStockRe>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tmp_stock_res");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaManufactura)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_manufactura");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.Host)
                    .HasMaxLength(50)
                    .HasColumnName("host");

                entity.Property(e => e.Indicador).HasMaxLength(50);

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NoBulto).HasColumnName("no_bulto");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.UbicacionAnt)
                    .HasMaxLength(25)
                    .HasColumnName("ubicacion_ant")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UdsLicPlate).HasColumnName("uds_lic_plate");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<TmsTicket>(entity =>
            {
                entity.HasKey(e => e.IdTicket);

                entity.ToTable("tms_ticket");

                entity.Property(e => e.IdTicket).ValueGeneratedNever();

                entity.Property(e => e.ApellidosPiloto)
                    .HasMaxLength(150)
                    .HasColumnName("Apellidos_Piloto");

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.FechaAsignado)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_asignado");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Ingreso");

                entity.Property(e => e.FechaProcesado)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_procesado");

                entity.Property(e => e.FechaProcesadoStockJornada)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_procesado_stock_jornada");

                entity.Property(e => e.FechaSalida)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Salida");

                entity.Property(e => e.NoDocumentoPiloto)
                    .HasMaxLength(150)
                    .HasColumnName("No_Documento_Piloto");

                entity.Property(e => e.NoPlaca)
                    .HasMaxLength(50)
                    .HasColumnName("No_Placa");

                entity.Property(e => e.NoPoliza)
                    .HasMaxLength(50)
                    .HasColumnName("No_Poliza");

                entity.Property(e => e.NoTc)
                    .HasMaxLength(50)
                    .HasColumnName("No_TC");

                entity.Property(e => e.NombresPiloto)
                    .HasMaxLength(150)
                    .HasColumnName("Nombres_Piloto");

                entity.Property(e => e.ProcesadoStockJornada)
                    .HasColumnName("procesado_stock_jornada")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TipoDocumentoPiloto)
                    .HasMaxLength(150)
                    .HasColumnName("Tipo_Documento_Piloto");

                entity.Property(e => e.TipoOperacion)
                    .HasMaxLength(50)
                    .HasColumnName("Tipo_Operacion");

                entity.HasOne(d => d.IdEmpresaTransporteNavigation)
                    .WithMany(p => p.TmsTickets)
                    .HasForeignKey(d => d.IdEmpresaTransporte)
                    .HasConstraintName("FK_tms_ticket_empresa_transporte");

                entity.HasOne(d => d.IdPilotoNavigation)
                    .WithMany(p => p.TmsTickets)
                    .HasForeignKey(d => d.IdPiloto)
                    .HasConstraintName("FK_tms_ticket_empresa_transporte_pilotos");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.TmsTickets)
                    .HasForeignKey(d => d.IdPropietario)
                    .HasConstraintName("FK_tms_ticket_propietarios");

                entity.HasOne(d => d.IdVehiculoNavigation)
                    .WithMany(p => p.TmsTickets)
                    .HasForeignKey(d => d.IdVehiculo)
                    .HasConstraintName("FK_tms_ticket_empresa_transporte_vehiculos");
            });

            modelBuilder.Entity<TmsTicketPol>(entity =>
            {
                entity.HasKey(e => new { e.IdTicket, e.IdOrdenTmsEnc });

                entity.ToTable("tms_ticket_pol");

                entity.Property(e => e.Clase)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clase");

                entity.Property(e => e.ClaveAduana)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clave_aduana");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(1000)
                    .HasColumnName("Codigo_Barra");

                entity.Property(e => e.Dua)
                    .HasMaxLength(50)
                    .HasColumnName("dua");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaPoliza)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_poliza");

                entity.Property(e => e.ModTransporte)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("mod_transporte");

                entity.Property(e => e.NitImpExp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nit_imp_exp");

                entity.Property(e => e.NoPoliza).HasMaxLength(50);

                entity.Property(e => e.PaisProcede)
                    .HasMaxLength(50)
                    .HasColumnName("pais_procede");

                entity.Property(e => e.TipoCambio).HasColumnName("tipo_cambio");

                entity.Property(e => e.TotalBultosPeso).HasColumnName("total_bultos_peso");

                entity.Property(e => e.TotalFlete).HasColumnName("total_flete");

                entity.Property(e => e.TotalGeneral).HasColumnName("total_general");

                entity.Property(e => e.TotalLiquidar).HasColumnName("total_liquidar");

                entity.Property(e => e.TotalSeguro).HasColumnName("total_seguro");

                entity.Property(e => e.TotalUsd).HasColumnName("total_usd");

                entity.Property(e => e.TotalValoraduana).HasColumnName("total_valoraduana");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<TransAjusteDet>(entity =>
            {
                entity.HasKey(e => e.Idajustedet);

                entity.ToTable("trans_ajuste_det");

                entity.HasIndex(e => e.Idtipoajuste, "NCLI_trans_ajuste_det_20200728");

                entity.Property(e => e.Idajustedet)
                    .ValueGeneratedNever()
                    .HasColumnName("idajustedet");

                entity.Property(e => e.CantidadNueva)
                    .HasColumnName("cantidad_nueva")
                    .HasDefaultValueSql("((0))")
                    .HasComment("cantidad nueva despues de ajuste.");

                entity.Property(e => e.CantidadOriginal)
                    .HasColumnName("cantidad_original")
                    .HasComment("cantidad original, antes de ajuste.");

                entity.Property(e => e.CodigoAjuste)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_ajuste")
                    .HasComment("el código de ajuste puede ser utilizado para reportar en el ERP el código de ajuste aplicado si se lee desde el Webservice.");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.Enviado)
                    .HasColumnName("enviado")
                    .HasDefaultValueSql("((0))")
                    .HasComment(" Éste campo permitirá al webservice listar los ajustes que no han sido procesados por el ERP (0) y podrá ser actualizada a través del webservices para indicar que el ajuste ya fue procesado (1)");

                entity.Property(e => e.FechaVenceNueva)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence_nueva")
                    .HasComment("fecha de vencimiento después del ajuste.");

                entity.Property(e => e.FechaVenceOriginal)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence_original")
                    .HasComment("fecha de vencimiento original del producto antes de ajuste.");

                entity.Property(e => e.IdBodegaErp).HasColumnName("IdBodegaERP");

                entity.Property(e => e.Idajusteenc).HasColumnName("idajusteenc");

                entity.Property(e => e.Idmotivoajuste)
                    .HasColumnName("idmotivoajuste")
                    .HasComment("indica porque razón se realizó el ajuste, pej. error en digitación.");

                entity.Property(e => e.Idtipoajuste)
                    .HasColumnName("idtipoajuste")
                    .HasComment("indica el tipo de ajuste que se le aplicó, pej. cambio_lote, cambio_vencimiento, cambio_peso, cambio_cantidad, etc.");

                entity.Property(e => e.LoteNuevo)
                    .HasMaxLength(50)
                    .HasColumnName("lote_nuevo")
                    .HasComment("Nuevo lote, despues de ajuste.");

                entity.Property(e => e.LoteOriginal)
                    .HasMaxLength(50)
                    .HasColumnName("lote_original")
                    .HasComment("Lote original del producto antes del ajuste.");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(200)
                    .HasColumnName("nombre_producto");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(50)
                    .HasColumnName("observacion")
                    .HasComment("observación que se desee adicionar al ajuste.");

                entity.Property(e => e.PesoNuevo)
                    .HasColumnName("peso_nuevo")
                    .HasComment("Nuevo peso después de ajuste.");

                entity.Property(e => e.PesoOriginal)
                    .HasColumnName("peso_original")
                    .HasComment("Peso original antes de ajuste.");

                entity.HasOne(d => d.IdajusteencNavigation)
                    .WithMany(p => p.TransAjusteDets)
                    .HasForeignKey(d => d.Idajusteenc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_ajuste_enc_trans_ajuste_det");
            });

            modelBuilder.Entity<TransAjusteDetDoc>(entity =>
            {
                entity.HasKey(e => e.Idajustedoc)
                    .HasName("PK_trans_ajuste_doc");

                entity.ToTable("trans_ajuste_det_doc");

                entity.Property(e => e.Idajustedoc)
                    .ValueGeneratedNever()
                    .HasColumnName("idajustedoc");

                entity.Property(e => e.Documento)
                    .HasMaxLength(50)
                    .HasColumnName("documento");

                entity.Property(e => e.Idajusteenc).HasColumnName("idajusteenc");

                entity.HasOne(d => d.IdajusteencNavigation)
                    .WithMany(p => p.TransAjusteDetDocs)
                    .HasForeignKey(d => d.Idajusteenc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_ajuste_enc_trans_ajuste_det_doc");
            });

            modelBuilder.Entity<TransAjusteEnc>(entity =>
            {
                entity.HasKey(e => e.Idajusteenc);

                entity.ToTable("trans_ajuste_enc");

                entity.Property(e => e.Idajusteenc)
                    .ValueGeneratedNever()
                    .HasColumnName("idajusteenc");

                entity.Property(e => e.AjustePorInventario)
                    .HasColumnName("ajuste_por_inventario")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EnviadoAErp)
                    .HasColumnName("Enviado_A_ERP")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Idbodega).HasColumnName("idbodega");

                entity.Property(e => e.Idusuario).HasColumnName("idusuario");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(50)
                    .HasColumnName("referencia");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<TransDespachoDet>(entity =>
            {
                entity.HasKey(e => e.IdDespachoDet);

                entity.ToTable("trans_despacho_det");

                entity.HasIndex(e => new { e.IdPickingUbic, e.IdProductoBodega, e.IdUnidadMedidaBasica }, "NCLI_trans_despacho_det_20200721_EJC");

                entity.HasIndex(e => new { e.IdPickingUbic, e.IdProductoBodega, e.IdUnidadMedidaBasica }, "NCLI_trans_despacho_det_20210908_EJC");

                entity.Property(e => e.IdDespachoDet).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NombreEstado).HasMaxLength(50);

                entity.Property(e => e.NombreProducto).HasMaxLength(250);

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdDespachoEncNavigation)
                    .WithMany(p => p.TransDespachoDets)
                    .HasForeignKey(d => d.IdDespachoEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_despacho_det_trans_despacho_enc");

                entity.HasOne(d => d.IdPickingUbicNavigation)
                    .WithMany(p => p.TransDespachoDets)
                    .HasForeignKey(d => d.IdPickingUbic)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_despacho_det_trans_picking_ubic");
            });

            modelBuilder.Entity<TransDespachoDetLoteNum>(entity =>
            {
                entity.HasKey(e => e.IdDespachoDetLote)
                    .HasName("PK_trans_despacho_det_lote");

                entity.ToTable("trans_despacho_det_lote_num");

                entity.Property(e => e.IdDespachoDetLote).ValueGeneratedNever();

                entity.Property(e => e.Lote).HasMaxLength(250);
            });

            modelBuilder.Entity<TransDespachoEnc>(entity =>
            {
                entity.HasKey(e => e.IdDespachoEnc);

                entity.ToTable("trans_despacho_enc");

                entity.Property(e => e.IdDespachoEnc).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CantBultos).HasColumnName("cant_bultos");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.HoraFin)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin");

                entity.Property(e => e.HoraIni)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini");

                entity.Property(e => e.Marchamo)
                    .HasMaxLength(50)
                    .HasColumnName("marchamo");

                entity.Property(e => e.NoPase).HasColumnName("no_pase");

                entity.Property(e => e.Numero)
                    .HasColumnName("numero")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(500)
                    .HasColumnName("observacion");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(30)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.TransDespachoEncs)
                    .HasForeignKey(d => d.IdBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_despacho_enc_bodega");

                entity.HasOne(d => d.IdPilotoNavigation)
                    .WithMany(p => p.TransDespachoEncs)
                    .HasForeignKey(d => d.IdPiloto)
                    .HasConstraintName("FK_trans_despacho_enc_empresa_transporte_pilotos");

                entity.HasOne(d => d.IdPropietarioBodegaNavigation)
                    .WithMany(p => p.TransDespachoEncs)
                    .HasForeignKey(d => d.IdPropietarioBodega)
                    .HasConstraintName("FK_trans_despacho_enc_propietario_bodega");

                entity.HasOne(d => d.IdRutaNavigation)
                    .WithMany(p => p.TransDespachoEncs)
                    .HasForeignKey(d => d.IdRuta)
                    .HasConstraintName("FK_trans_despacho_enc_road_ruta");

                entity.HasOne(d => d.IdVehiculoNavigation)
                    .WithMany(p => p.TransDespachoEncs)
                    .HasForeignKey(d => d.IdVehiculo)
                    .HasConstraintName("FK_trans_despacho_enc_empresa_transporte_vehiculos");
            });

            modelBuilder.Entity<TransDiEnc>(entity =>
            {
                entity.HasKey(e => e.IdTransDienc);

                entity.ToTable("trans_di_enc");

                entity.Property(e => e.IdTransDienc)
                    .ValueGeneratedNever()
                    .HasColumnName("IdTransDIEnc");

                entity.Property(e => e.ControlPoliza).HasColumnName("Control_Poliza");

                entity.Property(e => e.Correlativo).HasColumnName("correlativo");

                entity.Property(e => e.EnviadoAErp)
                    .HasColumnName("Enviado_A_ERP")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("Fec_Agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("Fec_Mod");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Creacion");

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Recepcion");

                entity.Property(e => e.HoraCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("Hora_Creacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HoraFinRecepcion)
                    .HasColumnType("datetime")
                    .HasColumnName("Hora_Fin_Recepcion");

                entity.Property(e => e.HoraInicioRecepcion)
                    .HasColumnType("datetime")
                    .HasColumnName("Hora_Inicio_Recepcion");

                entity.Property(e => e.IdEstadoOc).HasColumnName("IdEstadoOC");

                entity.Property(e => e.IdMotivoAnulacionBodega).HasDefaultValueSql("((0))");

                entity.Property(e => e.IdTipoIngresoOc)
                    .HasColumnName("IdTipoIngresoOC")
                    .HasComment("Marítimo, Terrestre, Aéreo");

                entity.Property(e => e.NoDocumento)
                    .HasMaxLength(30)
                    .HasColumnName("No_Documento");

                entity.Property(e => e.NoMarchamo)
                    .HasMaxLength(50)
                    .HasColumnName("No_Marchamo");

                entity.Property(e => e.NoTicketTms)
                    .HasMaxLength(50)
                    .HasColumnName("No_Ticket_TMS");

                entity.Property(e => e.Observacion).HasColumnType("text");

                entity.Property(e => e.Procedencia).HasMaxLength(150);

                entity.Property(e => e.ProgramarRecepcion).HasColumnName("Programar_Recepcion");

                entity.Property(e => e.Referencia).HasMaxLength(100);

                entity.Property(e => e.Serie)
                    .HasMaxLength(25)
                    .HasColumnName("serie");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("User_Agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("User_Mod");

                entity.HasOne(d => d.IdEstadoOcNavigation)
                    .WithMany(p => p.TransDiEncs)
                    .HasForeignKey(d => d.IdEstadoOc)
                    .HasConstraintName("FK_trans_di_enc_trans_oc_estado");

                entity.HasOne(d => d.IdTipoIngresoOcNavigation)
                    .WithMany(p => p.TransDiEncs)
                    .HasForeignKey(d => d.IdTipoIngresoOc)
                    .HasConstraintName("FK_trans_di_enc_trans_oc_ti");
            });

            modelBuilder.Entity<TransInvCiclico>(entity =>
            {
                entity.HasKey(e => e.Idinvciclico);

                entity.ToTable("trans_inv_ciclico");

                entity.Property(e => e.Idinvciclico)
                    .ValueGeneratedNever()
                    .HasColumnName("idinvciclico");

                entity.Property(e => e.CantReconteo).HasColumnName("cant_reconteo");

                entity.Property(e => e.CantStock).HasColumnName("cant_stock");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.EsNuevo).HasDefaultValueSql("((0))");

                entity.Property(e => e.EsPallet).HasDefaultValueSql("((0))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaVenceStock)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence_stock");

                entity.Property(e => e.IdPresentacionNuevo).HasColumnName("IdPresentacion_nuevo");

                entity.Property(e => e.IdProductoEstNuevo).HasColumnName("IdProductoEst_nuevo");

                entity.Property(e => e.IdUbicacionNuevo).HasColumnName("IdUbicacion_nuevo");

                entity.Property(e => e.Idinventarioenc).HasColumnName("idinventarioenc");

                entity.Property(e => e.Idoperador).HasColumnName("idoperador");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(100)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.LoteStock)
                    .HasMaxLength(50)
                    .HasColumnName("lote_stock");

                entity.Property(e => e.Peso)
                    .HasColumnName("peso")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PesoReconteo).HasColumnName("peso_reconteo");

                entity.Property(e => e.PesoStock).HasColumnName("peso_stock");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.HasOne(d => d.IdProductoBodegaNavigation)
                    .WithMany(p => p.TransInvCiclicos)
                    .HasForeignKey(d => d.IdProductoBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_inv_ciclico_producto_bodega");

                entity.HasOne(d => d.IdinventarioencNavigation)
                    .WithMany(p => p.TransInvCiclicos)
                    .HasForeignKey(d => d.Idinventarioenc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_inv_ciclico_trans_inv_enc");
            });

            modelBuilder.Entity<TransInvCiclicoUbic>(entity =>
            {
                entity.HasKey(e => new { e.Idinventarioenc, e.Idubicacion });

                entity.ToTable("trans_inv_ciclico_ubic");

                entity.Property(e => e.Idinventarioenc).HasColumnName("idinventarioenc");

                entity.Property(e => e.Idubicacion).HasColumnName("idubicacion");
            });

            modelBuilder.Entity<TransInvDetalle>(entity =>
            {
                entity.HasKey(e => e.Idinventariodet)
                    .HasName("PK_inv_ini_detalle_1");

                entity.ToTable("trans_inv_detalle");

                entity.Property(e => e.Idinventariodet).HasColumnName("idinventariodet");

                entity.Property(e => e.Cantidad)
                    .HasColumnName("cantidad")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Carga).HasColumnName("carga");

                entity.Property(e => e.FechaCaptura)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_captura")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("date")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.Host)
                    .HasMaxLength(50)
                    .HasColumnName("host");

                entity.Property(e => e.Idinventarioenc).HasColumnName("idinventarioenc");

                entity.Property(e => e.Idoperador).HasColumnName("idoperador");

                entity.Property(e => e.Idproducto).HasColumnName("idproducto");

                entity.Property(e => e.Idproductoestado)
                    .HasMaxLength(10)
                    .HasColumnName("idproductoestado")
                    .IsFixedLength(true);

                entity.Property(e => e.Idtramo).HasColumnName("idtramo");

                entity.Property(e => e.Idunidadmedida).HasColumnName("idunidadmedida");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NomOperador)
                    .HasMaxLength(50)
                    .HasColumnName("nom_operador");

                entity.Property(e => e.NomProducto)
                    .HasMaxLength(250)
                    .HasColumnName("nom_producto");

                entity.Property(e => e.NombrePropietario)
                    .HasMaxLength(150)
                    .HasColumnName("nombre_propietario");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Serie)
                    .HasMaxLength(50)
                    .HasColumnName("serie");

                entity.HasOne(d => d.IdinventarioencNavigation)
                    .WithMany(p => p.TransInvDetalles)
                    .HasForeignKey(d => d.Idinventarioenc)
                    .HasConstraintName("FK_inv_ini_detalle_inv_enc");
            });

            modelBuilder.Entity<TransInvEnc>(entity =>
            {
                entity.HasKey(e => e.Idinventarioenc)
                    .HasName("PK_inv_enc");

                entity.ToTable("trans_inv_enc");

                entity.Property(e => e.Idinventarioenc)
                    .ValueGeneratedNever()
                    .HasColumnName("idinventarioenc");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CambiaUbicacion).HasColumnName("cambia_ubicacion");

                entity.Property(e => e.CapturarNoExistente)
                    .HasColumnName("capturar_no_existente")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DobleVerificacion)
                    .HasColumnName("doble_verificacion")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EsSistema).HasDefaultValueSql("((0))");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.FechaUltimoInventario)
                    .HasColumnType("date")
                    .HasColumnName("fecha_ultimo_inventario");

                entity.Property(e => e.HoraFin)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin");

                entity.Property(e => e.HoraIni)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini");

                entity.Property(e => e.Idbodega).HasColumnName("idbodega");

                entity.Property(e => e.Idpropietario).HasColumnName("idpropietario");

                entity.Property(e => e.Idtipoinventario)
                    .HasColumnName("idtipoinventario")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Inicial).HasColumnName("inicial");

                entity.Property(e => e.MostrarCantidadTeoricaHh)
                    .HasColumnName("mostrar_cantidad_teorica_hh")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MultiPropietario).HasColumnName("multi_propietario");

                entity.Property(e => e.Regularizado).HasColumnName("regularizado");

                entity.Property(e => e.TipoConteoProducto)
                    .HasColumnName("tipo_conteo_producto")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(30)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<TransInvEncReconteo>(entity =>
            {
                entity.HasKey(e => e.Idinvencreconteo);

                entity.ToTable("trans_inv_enc_reconteo");

                entity.Property(e => e.Idinvencreconteo)
                    .ValueGeneratedNever()
                    .HasColumnName("idinvencreconteo");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.HoraFin)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin");

                entity.Property(e => e.HoraIni)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini");

                entity.Property(e => e.Idinventarioenc).HasColumnName("idinventarioenc");

                entity.Property(e => e.Reconteo)
                    .HasColumnName("reconteo")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(30)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdinventarioencNavigation)
                    .WithMany(p => p.TransInvEncReconteos)
                    .HasForeignKey(d => d.Idinventarioenc)
                    .HasConstraintName("FK_trans_inv_enc_reconteo_trans_inv_enc");
            });

            modelBuilder.Entity<TransInvNe>(entity =>
            {
                entity.HasKey(e => e.Idinventarione);

                entity.ToTable("trans_inv_ne");

                entity.Property(e => e.Idinventarione)
                    .ValueGeneratedNever()
                    .HasColumnName("idinventarione");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.Idinventarioenc).HasColumnName("idinventarioenc");

                entity.Property(e => e.Idproducto).HasColumnName("idproducto");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.UsrAgr)
                    .HasMaxLength(50)
                    .HasColumnName("usr_agr");
            });

            modelBuilder.Entity<TransInvOperador>(entity =>
            {
                entity.HasKey(e => e.Idinvoperador)
                    .HasName("PK_trans_inv_oper_1");

                entity.ToTable("trans_inv_operador");

                entity.Property(e => e.Idinvoperador)
                    .ValueGeneratedNever()
                    .HasColumnName("idinvoperador");

                entity.Property(e => e.Idinvencreconteo).HasColumnName("idinvencreconteo");

                entity.Property(e => e.Idinventarioenc).HasColumnName("idinventarioenc");

                entity.Property(e => e.Idoperador).HasColumnName("idoperador");

                entity.Property(e => e.Idubic).HasColumnName("idubic");

                entity.HasOne(d => d.IdinventarioencNavigation)
                    .WithMany(p => p.TransInvOperadors)
                    .HasForeignKey(d => d.Idinventarioenc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_inv_oper_trans_inv_enc");
            });

            modelBuilder.Entity<TransInvReconteo>(entity =>
            {
                entity.HasKey(e => e.Idinvreconteo);

                entity.ToTable("trans_inv_reconteo");

                entity.Property(e => e.Idinvreconteo)
                    .ValueGeneratedNever()
                    .HasColumnName("idinvreconteo");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CantidadAnterior).HasColumnName("cantidadAnterior");

                entity.Property(e => e.EsNuevo).HasDefaultValueSql("((0))");

                entity.Property(e => e.EsPallet).HasDefaultValueSql("((0))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("idUbicacionAnterior");

                entity.Property(e => e.Idinvciclico).HasColumnName("idinvciclico");

                entity.Property(e => e.Idinventarioenc).HasColumnName("idinventarioenc");

                entity.Property(e => e.Idreconteo).HasColumnName("idreconteo");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(100)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.Peso)
                    .HasColumnName("peso")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PesoAnterior).HasColumnName("pesoAnterior");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.HasOne(d => d.IdProductoBodegaNavigation)
                    .WithMany(p => p.TransInvReconteos)
                    .HasForeignKey(d => d.IdProductoBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_inv_reconteo_producto_bodega");

                entity.HasOne(d => d.IdinvciclicoNavigation)
                    .WithMany(p => p.TransInvReconteos)
                    .HasForeignKey(d => d.Idinvciclico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_inv_reconteo_trans_inv_ciclico");

                entity.HasOne(d => d.IdinventarioencNavigation)
                    .WithMany(p => p.TransInvReconteos)
                    .HasForeignKey(d => d.Idinventarioenc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_inv_reconteo_trans_inv_enc");
            });

            modelBuilder.Entity<TransInvResuman>(entity =>
            {
                entity.HasKey(e => e.Idinventariores)
                    .HasName("PK_trans_inv_ini_resumen");

                entity.ToTable("trans_inv_resumen");

                entity.Property(e => e.Idinventariores)
                    .ValueGeneratedNever()
                    .HasColumnName("idinventariores");

                entity.Property(e => e.Cantidad)
                    .HasColumnName("cantidad")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FechaCaptura)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_captura");

                entity.Property(e => e.Host)
                    .HasMaxLength(50)
                    .HasColumnName("host");

                entity.Property(e => e.Idinventarioenct).HasColumnName("idinventarioenct");

                entity.Property(e => e.Idoperador).HasColumnName("idoperador");

                entity.Property(e => e.Idpresentacion).HasColumnName("idpresentacion");

                entity.Property(e => e.Idproducto).HasColumnName("idproducto");

                entity.Property(e => e.Idproductoestado).HasColumnName("idproductoestado");

                entity.Property(e => e.Idtramo).HasColumnName("idtramo");

                entity.Property(e => e.NomOperador)
                    .HasMaxLength(50)
                    .HasColumnName("nom_operador");

                entity.Property(e => e.NomProducto)
                    .HasMaxLength(250)
                    .HasColumnName("nom_producto");

                entity.HasOne(d => d.IdinventarioenctNavigation)
                    .WithMany(p => p.TransInvResumen)
                    .HasForeignKey(d => d.Idinventarioenct)
                    .HasConstraintName("FK_inv_ini_resumen_inv_enc");
            });

            modelBuilder.Entity<TransInvStock>(entity =>
            {
                entity.HasKey(e => new { e.Idinventario, e.IdStock })
                    .HasName("PK_inv_stock");

                entity.ToTable("trans_inv_stock");

                entity.Property(e => e.Idinventario).HasColumnName("idinventario");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Cantidad)
                    .HasColumnName("cantidad")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaCopia)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_copia");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaManufactura)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_manufactura");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoBulto)
                    .HasMaxLength(20)
                    .HasColumnName("no_bulto");

                entity.Property(e => e.Peso)
                    .HasColumnName("peso")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.Temperatura)
                    .HasColumnName("temperatura")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UdsLicPlate).HasColumnName("uds_lic_plate");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdinventarioNavigation)
                    .WithMany(p => p.TransInvStocks)
                    .HasForeignKey(d => d.Idinventario)
                    .HasConstraintName("FK_inv_ini_stock_inv_enc");
            });

            modelBuilder.Entity<TransInvStockProd>(entity =>
            {
                entity.HasKey(e => new { e.Idinventario, e.Idinvstockprod })
                    .HasName("PK_trans_inv_stock_prod_1");

                entity.ToTable("trans_inv_stock_prod");

                entity.Property(e => e.Idinventario).HasColumnName("idinventario");

                entity.Property(e => e.Idinvstockprod).HasColumnName("idinvstockprod");

                entity.Property(e => e.Cant).HasColumnName("cant");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.IdPresentacion).HasColumnName("idPresentacion");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.IdUnidadMedida).HasColumnName("idUnidadMedida");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.Peso)
                    .HasColumnName("peso")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TransInvTramo>(entity =>
            {
                entity.HasKey(e => new { e.Idinventario, e.Idtramo });

                entity.ToTable("trans_inv_tramo");

                entity.Property(e => e.Idinventario).HasColumnName("idinventario");

                entity.Property(e => e.Idtramo).HasColumnName("idtramo");

                entity.Property(e => e.Aplicado)
                    .HasColumnName("aplicado")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DetEstado)
                    .HasMaxLength(20)
                    .HasColumnName("det_estado");

                entity.Property(e => e.DetFin)
                    .HasColumnType("datetime")
                    .HasColumnName("det_fin");

                entity.Property(e => e.DetIdoperador).HasColumnName("det_idoperador");

                entity.Property(e => e.DetInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("det_inicio");

                entity.Property(e => e.ResEstado)
                    .HasMaxLength(20)
                    .HasColumnName("res_estado");

                entity.Property(e => e.ResFin)
                    .HasColumnType("datetime")
                    .HasColumnName("res_fin");

                entity.Property(e => e.ResIdoperador).HasColumnName("res_idoperador");

                entity.Property(e => e.ResInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("res_inicio");

                entity.HasOne(d => d.IdinventarioNavigation)
                    .WithMany(p => p.TransInvTramos)
                    .HasForeignKey(d => d.Idinventario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_inv_ini_tramo_inv_enc");
            });

            modelBuilder.Entity<TransInventarioDet>(entity =>
            {
                entity.HasKey(e => e.IdInventarioDet);

                entity.ToTable("trans_inventario_det");

                entity.Property(e => e.IdInventarioDet).ValueGeneratedNever();

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Conteo).HasColumnName("conteo");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.Inicial)
                    .HasColumnName("inicial")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LicPlate).HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lote")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NoBulto)
                    .HasMaxLength(20)
                    .HasColumnName("no_bulto")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Recuento).HasColumnName("recuento");

                entity.Property(e => e.Serial)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.UbicacionAnt)
                    .HasMaxLength(25)
                    .HasColumnName("ubicacion_ant")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdInventarioEncNavigation)
                    .WithMany(p => p.TransInventarioDets)
                    .HasForeignKey(d => d.IdInventarioEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_inventario_det_trans_inventario_enc");

                entity.HasOne(d => d.IdStockNavigation)
                    .WithMany(p => p.TransInventarioDets)
                    .HasForeignKey(d => d.IdStock)
                    .HasConstraintName("FK_trans_inventario_det_stock");
            });

            modelBuilder.Entity<TransInventarioEnc>(entity =>
            {
                entity.HasKey(e => e.IdInventarioEnc);

                entity.ToTable("trans_inventario_enc");

                entity.Property(e => e.IdInventarioEnc).ValueGeneratedNever();

                entity.Property(e => e.ActualizaVal)
                    .HasColumnName("actualiza_val")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.HoraFin)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin");

                entity.Property(e => e.HoraIni)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini");

                entity.Property(e => e.TipoConteo)
                    .HasMaxLength(10)
                    .HasColumnName("tipo_conteo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TipoInv).HasColumnName("tipo_inv");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(30)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdPropietarioBodegaNavigation)
                    .WithMany(p => p.TransInventarioEncs)
                    .HasForeignKey(d => d.IdPropietarioBodega)
                    .HasConstraintName("FK_trans_inventario_enc_propietario_bodega");
            });

            modelBuilder.Entity<TransMovimiento>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdBodegaOrigen, e.IdTransaccion, e.IdMovimiento })
                    .HasName("PK_movimientos");

                entity.ToTable("trans_movimientos");

                entity.HasIndex(e => new { e.IdProductoBodega, e.IdBodegaDestino }, "NCLI_TransMovimientos_20191211_EJC");

                entity.HasIndex(e => e.IdUnidadMedida, "NCLI_trans_movimientos_IdUnidadMedida_20200729");

                entity.Property(e => e.BarraPallet)
                    .HasMaxLength(50)
                    .HasColumnName("barra_pallet")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CantidadHist).HasColumnName("cantidad_hist");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.FechaAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_agr");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.HoraFin)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin");

                entity.Property(e => e.HoraIni)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.PesoHist).HasColumnName("peso_hist");

                entity.Property(e => e.Serie)
                    .HasMaxLength(50)
                    .HasColumnName("serie");

                entity.Property(e => e.UsuarioAgr)
                    .HasMaxLength(25)
                    .HasColumnName("usuario_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.HasOne(d => d.IdBodegaDestinoNavigation)
                    .WithMany(p => p.TransMovimientos)
                    .HasForeignKey(d => d.IdBodegaDestino)
                    .HasConstraintName("FK_trans_movimientos_bodega");

                entity.HasOne(d => d.IdEstadoOrigenNavigation)
                    .WithMany(p => p.TransMovimientos)
                    .HasForeignKey(d => d.IdEstadoOrigen)
                    .HasConstraintName("FK_trans_movimientos_producto_estado");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.TransMovimientos)
                    .HasForeignKey(d => d.IdPresentacion)
                    .HasConstraintName("FK_trans_movimientos_producto_presentacion");

                entity.HasOne(d => d.IdProductoBodegaNavigation)
                    .WithMany(p => p.TransMovimientos)
                    .HasForeignKey(d => d.IdProductoBodega)
                    .HasConstraintName("FK_trans_movimientos_producto_bodega");

                entity.HasOne(d => d.IdPropietarioBodegaNavigation)
                    .WithMany(p => p.TransMovimientos)
                    .HasForeignKey(d => d.IdPropietarioBodega)
                    .HasConstraintName("FK_trans_movimientos_propietario_bodega");

                entity.HasOne(d => d.IdTipoTareaNavigation)
                    .WithMany(p => p.TransMovimientos)
                    .HasForeignKey(d => d.IdTipoTarea)
                    .HasConstraintName("FK_trans_movimientos_sis_tipo_tarea_hh");

                entity.HasOne(d => d.IdUnidadMedidaNavigation)
                    .WithMany(p => p.TransMovimientos)
                    .HasForeignKey(d => d.IdUnidadMedida)
                    .HasConstraintName("FK_trans_movimientos_unidad_medida");
            });

            modelBuilder.Entity<TransMovimientoPallet>(entity =>
            {
                entity.HasKey(e => e.Idmovimientopallet);

                entity.ToTable("trans_movimiento_pallet");

                entity.Property(e => e.Idmovimientopallet)
                    .ValueGeneratedNever()
                    .HasColumnName("idmovimientopallet");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Idbodega).HasColumnName("idbodega");

                entity.Property(e => e.Idusuario).HasColumnName("idusuario");

                entity.Property(e => e.LpDestino)
                    .HasMaxLength(50)
                    .HasColumnName("lp_destino");

                entity.Property(e => e.LpOrigen)
                    .HasMaxLength(50)
                    .HasColumnName("lp_origen");

                entity.Property(e => e.Orientacion)
                    .HasMaxLength(50)
                    .HasColumnName("orientacion");
            });

            modelBuilder.Entity<TransOcDet>(entity =>
            {
                entity.HasKey(e => new { e.IdOrdenCompraDet, e.IdOrdenCompraEnc })
                    .HasName("PK_trans_orden_compra_det");

                entity.ToTable("trans_oc_det");

                entity.HasIndex(e => e.IdOrdenCompraEnc, "NCLI_TRANS_OC_DET_20210623_EJC");

                entity.HasIndex(e => e.IdProductoBodega, "NCLI_Trans_Oc_Det_IdProductoBodega_20210601EJC");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.AtributoVariante1)
                    .HasMaxLength(50)
                    .HasColumnName("atributo_variante_1");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CantidadRecibida).HasColumnName("cantidad_recibida");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.NoLinea).HasColumnName("No_Linea");

                entity.Property(e => e.NombreArancel)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_arancel");

                entity.Property(e => e.NombrePresentacion)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_presentacion");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .HasColumnName("nombre_producto");

                entity.Property(e => e.NombrePropietario)
                    .HasMaxLength(150)
                    .HasColumnName("nombre_propietario");

                entity.Property(e => e.NombreUnidadMedidaBasica)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_unidad_medida_basica");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.PesoBruto).HasColumnName("peso_bruto");

                entity.Property(e => e.PesoNeto)
                    .HasColumnName("peso_neto")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PesoRecibido).HasColumnName("peso_recibido");

                entity.Property(e => e.PorcentajeArancel).HasColumnName("porcentaje_arancel");

                entity.Property(e => e.TotalLinea).HasColumnName("total_linea");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.Property(e => e.ValorAduana)
                    .HasColumnName("valor_aduana")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ValorDai)
                    .HasColumnName("valor_dai")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ValorFlete)
                    .HasColumnName("valor_flete")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ValorFob)
                    .HasColumnName("valor_fob")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ValorIva)
                    .HasColumnName("valor_iva")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ValorSeguro)
                    .HasColumnName("valor_seguro")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdArancelNavigation)
                    .WithMany(p => p.TransOcDets)
                    .HasForeignKey(d => d.IdArancel)
                    .HasConstraintName("FK_trans_orden_compra_det_Arancel");

                entity.HasOne(d => d.IdMotivoDevolucionNavigation)
                    .WithMany(p => p.TransOcDets)
                    .HasForeignKey(d => d.IdMotivoDevolucion)
                    .HasConstraintName("FK_trans_oc_det_motivo_devolucion");

                entity.HasOne(d => d.IdOrdenCompraEncNavigation)
                    .WithMany(p => p.TransOcDets)
                    .HasForeignKey(d => d.IdOrdenCompraEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_orden_compra_det_trans_orden_compra_enc");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.TransOcDets)
                    .HasForeignKey(d => d.IdPresentacion)
                    .HasConstraintName("FK_trans_orden_compra_det_producto_presentacion");

                entity.HasOne(d => d.IdProductoBodegaNavigation)
                    .WithMany(p => p.TransOcDets)
                    .HasForeignKey(d => d.IdProductoBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_oc_det_producto_bodega");

                entity.HasOne(d => d.IdUnidadMedidaBasicaNavigation)
                    .WithMany(p => p.TransOcDets)
                    .HasForeignKey(d => d.IdUnidadMedidaBasica)
                    .HasConstraintName("FK_trans_orden_compra_det_unidad_medida");
            });

            modelBuilder.Entity<TransOcDetLote>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("trans_oc_det_lote");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CantidadRecibida).HasColumnName("cantidad_recibida");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("date")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoLinea).HasColumnName("no_linea");

                entity.Property(e => e.Ubicacion).HasMaxLength(50);
            });

            modelBuilder.Entity<TransOcDocuRef>(entity =>
            {
                entity.HasKey(e => e.IdDocumentoRef);

                entity.ToTable("trans_oc_docu_ref");

                entity.Property(e => e.IdDocumentoRef).ValueGeneratedNever();

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.Descripcion).HasMaxLength(150);

                entity.Property(e => e.FechaAgregado).HasColumnType("datetime");

                entity.Property(e => e.FechaAsignacion).HasColumnType("datetime");

                entity.Property(e => e.FechaDocumento).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TransOcEnc>(entity =>
            {
                entity.HasKey(e => e.IdOrdenCompraEnc)
                    .HasName("PK_trans_orden_compra_enc");

                entity.ToTable("trans_oc_enc");

                entity.HasIndex(e => e.IdTipoIngresoOc, "NCLI_TRANS_OC_ENC_20210623_EJC");

                entity.Property(e => e.IdOrdenCompraEnc).ValueGeneratedNever();

                entity.Property(e => e.ControlPoliza).HasColumnName("Control_Poliza");

                entity.Property(e => e.Correlativo).HasColumnName("correlativo");

                entity.Property(e => e.EnviadoAErp)
                    .HasColumnName("Enviado_A_ERP")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("Fec_Agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("Fec_Mod");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Creacion");

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Recepcion");

                entity.Property(e => e.HoraCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("Hora_Creacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HoraFinRecepcion)
                    .HasColumnType("datetime")
                    .HasColumnName("Hora_Fin_Recepcion");

                entity.Property(e => e.HoraInicioRecepcion)
                    .HasColumnType("datetime")
                    .HasColumnName("Hora_Inicio_Recepcion");

                entity.Property(e => e.IdEstadoOc).HasColumnName("IdEstadoOC");

                entity.Property(e => e.IdMotivoAnulacionBodega).HasDefaultValueSql("((0))");

                entity.Property(e => e.IdPedidoEncDevolucion).HasDefaultValueSql("((0))");

                entity.Property(e => e.IdTipoIngresoOc)
                    .HasColumnName("IdTipoIngresoOC")
                    .HasComment("Marítimo, Terrestre, Aéreo");

                entity.Property(e => e.Idacuerdocomercial).HasColumnName("idacuerdocomercial");

                entity.Property(e => e.NoDocumento)
                    .HasMaxLength(30)
                    .HasColumnName("No_Documento");

                entity.Property(e => e.NoDocumentoDevolucion)
                    .HasMaxLength(50)
                    .HasColumnName("no_documento_devolucion")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.NoDocumentoRecepcionErp)
                    .HasMaxLength(50)
                    .HasColumnName("no_documento_recepcion_erp");

                entity.Property(e => e.NoMarchamo)
                    .HasMaxLength(50)
                    .HasColumnName("No_Marchamo");

                entity.Property(e => e.NoTicketTms)
                    .HasMaxLength(50)
                    .HasColumnName("no_ticket_tms");

                entity.Property(e => e.Observacion).HasColumnType("text");

                entity.Property(e => e.Procedencia).HasMaxLength(150);

                entity.Property(e => e.ProgramarRecepcion).HasColumnName("Programar_Recepcion");

                entity.Property(e => e.Referencia).HasMaxLength(100);

                entity.Property(e => e.Serie)
                    .HasMaxLength(25)
                    .HasColumnName("serie");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("User_Agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("User_Mod");

                entity.HasOne(d => d.IdEstadoOcNavigation)
                    .WithMany(p => p.TransOcEncs)
                    .HasForeignKey(d => d.IdEstadoOc)
                    .HasConstraintName("FK_trans_oc_enc_trans_oc_estado");

                entity.HasOne(d => d.IdMotivoDevolucionNavigation)
                    .WithMany(p => p.TransOcEncs)
                    .HasForeignKey(d => d.IdMotivoDevolucion)
                    .HasConstraintName("FK_trans_oc_enc_motivo_devolucion");

                entity.HasOne(d => d.IdPropietarioBodegaNavigation)
                    .WithMany(p => p.TransOcEncs)
                    .HasForeignKey(d => d.IdPropietarioBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_oc_enc_propietario_bodega");

                entity.HasOne(d => d.IdProveedorBodegaNavigation)
                    .WithMany(p => p.TransOcEncs)
                    .HasForeignKey(d => d.IdProveedorBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_oc_enc_proveedor");

                entity.HasOne(d => d.IdTipoIngresoOcNavigation)
                    .WithMany(p => p.TransOcEncs)
                    .HasForeignKey(d => d.IdTipoIngresoOc)
                    .HasConstraintName("FK_trans_oc_enc_trans_oc_ti");
            });

            modelBuilder.Entity<TransOcEstado>(entity =>
            {
                entity.HasKey(e => e.IdEstadoOc)
                    .HasName("PK_trans_orden_compra_estado");

                entity.ToTable("trans_oc_estado");

                entity.Property(e => e.IdEstadoOc)
                    .ValueGeneratedNever()
                    .HasColumnName("IdEstadoOC");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TransOcImagen>(entity =>
            {
                entity.HasKey(e => new { e.IdOrdenCompraImg, e.IdOrdenCompraEnc })
                    .HasName("PK_trans_orden_compra_imagen");

                entity.ToTable("trans_oc_imagen");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Imagen).HasColumnType("image");

                entity.HasOne(d => d.IdOrdenCompraEncNavigation)
                    .WithMany(p => p.TransOcImagens)
                    .HasForeignKey(d => d.IdOrdenCompraEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_orden_compra_imagen_trans_orden_compra_enc");
            });

            modelBuilder.Entity<TransOcPol>(entity =>
            {
                entity.HasKey(e => new { e.IdOrdenCompraPol, e.IdOrdenCompraEnc });

                entity.ToTable("trans_oc_pol");

                entity.Property(e => e.BlNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("bl_no");

                entity.Property(e => e.BuqueNo)
                    .HasMaxLength(50)
                    .HasColumnName("buque_no");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Cbm).HasColumnName("cbm");

                entity.Property(e => e.Clase)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clase");

                entity.Property(e => e.ClaveAduana)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clave_aduana");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(1000)
                    .HasColumnName("Codigo_Barra");

                entity.Property(e => e.CodigoPoliza)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_poliza");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Destino)
                    .HasMaxLength(50)
                    .HasColumnName("destino");

                entity.Property(e => e.DirDestino)
                    .HasMaxLength(50)
                    .HasColumnName("dir_destino");

                entity.Property(e => e.Dua)
                    .HasMaxLength(50)
                    .HasColumnName("dua");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaAbordaje)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_abordaje");

                entity.Property(e => e.FechaAceptacion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_aceptacion");

                entity.Property(e => e.FechaLlegada)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_llegada");

                entity.Property(e => e.FechaPoliza)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_poliza");

                entity.Property(e => e.ModTransporte)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("mod_transporte");

                entity.Property(e => e.NitImpExp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nit_imp_exp");

                entity.Property(e => e.NoPoliza).HasMaxLength(50);

                entity.Property(e => e.NumeroOrden)
                    .HasMaxLength(50)
                    .HasColumnName("numero_orden");

                entity.Property(e => e.PaisProcede)
                    .HasMaxLength(50)
                    .HasColumnName("pais_procede");

                entity.Property(e => e.Piezas).HasColumnName("piezas");

                entity.Property(e => e.PoNumber)
                    .HasMaxLength(50)
                    .HasColumnName("po_number");

                entity.Property(e => e.PtoDescarga)
                    .HasMaxLength(50)
                    .HasColumnName("pto_descarga");

                entity.Property(e => e.Remitente)
                    .HasMaxLength(50)
                    .HasColumnName("remitente");

                entity.Property(e => e.Ticket)
                    .HasMaxLength(50)
                    .HasColumnName("ticket");

                entity.Property(e => e.TipoCambio).HasColumnName("tipo_cambio");

                entity.Property(e => e.TotalBultos).HasColumnName("total_bultos");

                entity.Property(e => e.TotalBultosPeso).HasColumnName("total_bultos_peso");

                entity.Property(e => e.TotalBultosPesoNeto)
                    .HasColumnName("total_bultos_peso_neto")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalFlete).HasColumnName("total_flete");

                entity.Property(e => e.TotalGeneral).HasColumnName("total_general");

                entity.Property(e => e.TotalKgs).HasColumnName("total_kgs");

                entity.Property(e => e.TotalLineas).HasColumnName("total_lineas");

                entity.Property(e => e.TotalLiquidar).HasColumnName("total_liquidar");

                entity.Property(e => e.TotalOtros).HasColumnName("total_otros");

                entity.Property(e => e.TotalSeguro).HasColumnName("total_seguro");

                entity.Property(e => e.TotalUsd).HasColumnName("total_usd");

                entity.Property(e => e.TotalValoraduana).HasColumnName("total_valoraduana");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.Property(e => e.ViajeNo)
                    .HasMaxLength(50)
                    .HasColumnName("viaje_no");
            });

            modelBuilder.Entity<TransOcServicio>(entity =>
            {
                entity.HasKey(e => e.IdOrdenCompraServicio)
                    .HasName("PK_trans_oc_Serv");

                entity.ToTable("trans_oc_servicios");

                entity.Property(e => e.IdOrdenCompraServicio).ValueGeneratedNever();

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.CorreCatalogoproductos).HasColumnName("corre_catalogoproductos");

                entity.Property(e => e.CorreDetalleacuerdo).HasColumnName("corre_detalleacuerdo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.NombreServicio)
                    .HasMaxLength(150)
                    .HasColumnName("nombre_servicio");

                entity.Property(e => e.NombreUnidad)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_unidad");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(150)
                    .HasColumnName("observacion");

                entity.Property(e => e.UnidMedida).HasColumnName("unid_medida");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<TransOcTi>(entity =>
            {
                entity.HasKey(e => e.IdTipoIngresoOc)
                    .HasName("PK_trans_orden_compra_tipo_ingreso");

                entity.ToTable("trans_oc_ti");

                entity.Property(e => e.IdTipoIngresoOc)
                    .ValueGeneratedNever()
                    .HasColumnName("IdTipoIngresoOC");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.ControlPoliza)
                    .HasColumnName("control_poliza")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EsDevolucion).HasColumnName("es_devolucion");

                entity.Property(e => e.EsPolizaConsolidada)
                    .HasColumnName("es_poliza_consolidada")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.GeneraTareaIngreso)
                    .HasColumnName("genera_tarea_ingreso")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.RequerirDocumentoRef)
                    .HasColumnName("requerir_documento_ref")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RequerirDocumentoRefWms)
                    .HasColumnName("requerir_documento_ref_wms")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RequerirProveedorEsBodegaWms)
                    .HasColumnName("requerir_proveedor_es_bodega_wms")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<TransPackingEnc>(entity =>
            {
                entity.HasKey(e => e.Idpackingenc);

                entity.ToTable("trans_packing_enc");

                entity.Property(e => e.Idpackingenc)
                    .ValueGeneratedNever()
                    .HasColumnName("idpackingenc");

                entity.Property(e => e.CantidadBultosPacking).HasColumnName("cantidad_bultos_packing");

                entity.Property(e => e.CantidadCamasPacking).HasColumnName("cantidad_camas_packing");

                entity.Property(e => e.FechaPacking)
                    .HasColumnType("date")
                    .HasColumnName("fecha_packing")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("date")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.Idbodega).HasColumnName("idbodega");

                entity.Property(e => e.Iddespachoenc).HasColumnName("iddespachoenc");

                entity.Property(e => e.Idempresaservicio).HasColumnName("idempresaservicio");

                entity.Property(e => e.Idoperadorbodega).HasColumnName("idoperadorbodega");

                entity.Property(e => e.Idpickingenc).HasColumnName("idpickingenc");

                entity.Property(e => e.Idpresentacion).HasColumnName("idpresentacion");

                entity.Property(e => e.Idproductobodega).HasColumnName("idproductobodega");

                entity.Property(e => e.Idproductoestado).HasColumnName("idproductoestado");

                entity.Property(e => e.Idunidadmedida).HasColumnName("idunidadmedida");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoLinea).HasColumnName("no_linea");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(50)
                    .HasColumnName("referencia");
            });

            modelBuilder.Entity<TransPeDet>(entity =>
            {
                entity.HasKey(e => e.IdPedidoDet)
                    .HasName("PK_trans_pedido_det");

                entity.ToTable("trans_pe_det");

                entity.HasIndex(e => e.IdPedidoEnc, "NCLI_trans_pe_det_20200720_EJC");

                entity.HasIndex(e => new { e.IdPedidoDet, e.IdPedidoEnc, e.IdProductoBodega, e.IdEstado, e.IdPresentacion, e.IdUnidadMedidaBasica, e.Cantidad }, "NonClusteredIndex-20190903-100646");

                entity.Property(e => e.IdPedidoDet).ValueGeneratedNever();

                entity.Property(e => e.AtributoVariante1)
                    .HasMaxLength(25)
                    .HasColumnName("atributo_variante_1");

                entity.Property(e => e.CantDespachada).HasColumnName("cant_despachada");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.Costo).HasDefaultValueSql("((0))");

                entity.Property(e => e.EsPadre).HasDefaultValueSql("((0))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FechaEspecifica).HasColumnName("fecha_especifica");

                entity.Property(e => e.Ndias).HasColumnName("ndias");

                entity.Property(e => e.NoLinea).HasColumnName("no_linea");

                entity.Property(e => e.NoRecepcion).HasColumnName("no_recepcion");

                entity.Property(e => e.NomEstado)
                    .HasMaxLength(50)
                    .HasColumnName("nom_estado");

                entity.Property(e => e.NomPresentacion)
                    .HasMaxLength(50)
                    .HasColumnName("nom_presentacion");

                entity.Property(e => e.NomUnidMed)
                    .HasMaxLength(50)
                    .HasColumnName("nom_unid_med");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .HasColumnName("nombre_producto");

                entity.Property(e => e.PesoBruto)
                    .HasColumnName("Peso_Bruto")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PesoDespachado).HasColumnName("peso_despachado");

                entity.Property(e => e.PesoNeto)
                    .HasColumnName("Peso_Neto")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RoadVal1).HasColumnName("RoadVAL1");

                entity.Property(e => e.RoadVal2)
                    .HasMaxLength(50)
                    .HasColumnName("RoadVAL2");

                entity.Property(e => e.TotalLinea)
                    .HasColumnName("Total_linea")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.ValorAduana)
                    .HasColumnName("valor_aduana")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ValorDai)
                    .HasColumnName("valor_dai")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ValorFlete)
                    .HasColumnName("valor_flete")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ValorFob)
                    .HasColumnName("valor_fob")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ValorIva)
                    .HasColumnName("valor_iva")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ValorSeguro)
                    .HasColumnName("valor_seguro")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdPedidoEncNavigation)
                    .WithMany(p => p.TransPeDets)
                    .HasForeignKey(d => d.IdPedidoEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_pedido_det_trans_pedido_enc");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.TransPeDets)
                    .HasForeignKey(d => d.IdPresentacion)
                    .HasConstraintName("FK_trans_pedido_det_producto_presentacion");

                entity.HasOne(d => d.IdProductoBodegaNavigation)
                    .WithMany(p => p.TransPeDets)
                    .HasForeignKey(d => d.IdProductoBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_pedido_det_producto_bodega");

                entity.HasOne(d => d.IdUnidadMedidaBasicaNavigation)
                    .WithMany(p => p.TransPeDets)
                    .HasForeignKey(d => d.IdUnidadMedidaBasica)
                    .HasConstraintName("FK_trans_pedido_det_unidad_medida");
            });

            modelBuilder.Entity<TransPeDocuRef>(entity =>
            {
                entity.HasKey(e => e.IdDocumentoRef);

                entity.ToTable("trans_pe_docu_ref");

                entity.Property(e => e.IdDocumentoRef).ValueGeneratedNever();

                entity.Property(e => e.Bodega).HasMaxLength(50);

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.CodigoCliente)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_cliente");

                entity.Property(e => e.Descripcion).HasMaxLength(150);

                entity.Property(e => e.Empresa).HasMaxLength(50);

                entity.Property(e => e.FechaAgregado).HasColumnType("datetime");

                entity.Property(e => e.FechaAsignacion).HasColumnType("datetime");

                entity.Property(e => e.FechaDocumento).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Referencia)
                    .HasMaxLength(50)
                    .HasColumnName("referencia");
            });

            modelBuilder.Entity<TransPeDocuRefDet>(entity =>
            {
                entity.HasKey(e => new { e.IdDocumentoRef, e.IdDocumentoRefDet });

                entity.ToTable("trans_pe_docu_ref_det");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(10)
                    .HasColumnName("nombre_producto")
                    .IsFixedLength(true);

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Presentaacion)
                    .HasMaxLength(50)
                    .HasColumnName("presentaacion");

                entity.Property(e => e.Umbas)
                    .HasMaxLength(50)
                    .HasColumnName("umbas");
            });

            modelBuilder.Entity<TransPeEnc>(entity =>
            {
                entity.HasKey(e => e.IdPedidoEnc)
                    .HasName("PK_trans_pedido_enc");

                entity.ToTable("trans_pe_enc");

                entity.Property(e => e.IdPedidoEnc).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Anulado).HasColumnName("anulado");

                entity.Property(e => e.ControlUltimoLote)
                    .HasColumnName("control_ultimo_lote")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Correlativo).HasColumnName("correlativo");

                entity.Property(e => e.DiasCliente).HasColumnName("dias_cliente");

                entity.Property(e => e.EnviadoAErp)
                    .HasColumnName("Enviado_A_ERP")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaPedido)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Pedido");

                entity.Property(e => e.HoraEntregaDesde).HasColumnType("datetime");

                entity.Property(e => e.HoraEntregaHasta).HasColumnType("datetime");

                entity.Property(e => e.HoraFin)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin");

                entity.Property(e => e.HoraIni)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Local).HasColumnName("local");

                entity.Property(e => e.NoDespacho).HasColumnName("no_despacho");

                entity.Property(e => e.NoDocumento).HasColumnName("no_documento");

                entity.Property(e => e.PalletPrimero)
                    .HasColumnName("pallet_primero")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(25)
                    .HasColumnName("referencia");

                entity.Property(e => e.ReferenciaDocumentoIngresoBodegaDestino)
                    .HasMaxLength(50)
                    .HasColumnName("Referencia_Documento_Ingreso_Bodega_Destino");

                entity.Property(e => e.RoadAdd1)
                    .HasMaxLength(5)
                    .HasColumnName("RoadADD1");

                entity.Property(e => e.RoadAdd2)
                    .HasMaxLength(5)
                    .HasColumnName("RoadADD2");

                entity.Property(e => e.RoadAdd3)
                    .HasMaxLength(35)
                    .HasColumnName("RoadADD3");

                entity.Property(e => e.RoadBandera).HasMaxLength(5);

                entity.Property(e => e.RoadCalcoBj)
                    .HasMaxLength(1)
                    .HasColumnName("RoadCalcoBJ");

                entity.Property(e => e.RoadDirEntrega).HasMaxLength(255);

                entity.Property(e => e.RoadFechaEntr).HasColumnType("datetime");

                entity.Property(e => e.RoadIdDespacho).HasDefaultValueSql("((0))");

                entity.Property(e => e.RoadIdFacturacion).HasDefaultValueSql("((0))");

                entity.Property(e => e.RoadIdRuta).HasComment("Id de Ruta en TOMIMS (Ruta de pedido)");

                entity.Property(e => e.RoadIdVendedor).HasComment("Id de Vendedor en TOMIMS (Vendedor de pedido)");

                entity.Property(e => e.RoadInformado).HasDefaultValueSql("((0))");

                entity.Property(e => e.RoadRazonRechazado)
                    .HasMaxLength(50)
                    .HasColumnName("RoadRazon_Rechazado")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RoadRechazado).HasDefaultValueSql("((0))");

                entity.Property(e => e.RoadStatCom).HasMaxLength(1);

                entity.Property(e => e.RoadStatProc)
                    .HasMaxLength(3)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RoadSucursal)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('01')");

                entity.Property(e => e.Serie)
                    .HasMaxLength(25)
                    .HasColumnName("serie");

                entity.Property(e => e.SyncMi3)
                    .HasColumnName("sync_mi3")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Ubicacion)
                    .HasMaxLength(35)
                    .HasColumnName("ubicacion");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(30)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.TransPeEncs)
                    .HasForeignKey(d => d.IdBodega)
                    .HasConstraintName("FK_trans_pedido_enc_bodega");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.TransPeEncs)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK_trans_pedido_enc_cliente");

                entity.HasOne(d => d.IdMuelleNavigation)
                    .WithMany(p => p.TransPeEncs)
                    .HasForeignKey(d => d.IdMuelle)
                    .HasConstraintName("FK_trans_pedido_enc_bodega_muelles");

                entity.HasOne(d => d.IdPropietarioBodegaNavigation)
                    .WithMany(p => p.TransPeEncs)
                    .HasForeignKey(d => d.IdPropietarioBodega)
                    .HasConstraintName("FK_trans_pedido_enc_propietario_bodega");
            });

            modelBuilder.Entity<TransPePol>(entity =>
            {
                entity.HasKey(e => new { e.IdOrdenPedidoPol, e.IdOrdenPedidoEnc });

                entity.ToTable("trans_pe_pol");

                entity.Property(e => e.BlNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("bl_no");

                entity.Property(e => e.BuqueNo)
                    .HasMaxLength(50)
                    .HasColumnName("buque_no");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Cbm).HasColumnName("cbm");

                entity.Property(e => e.Clase)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clase");

                entity.Property(e => e.ClaveAduana)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clave_aduana");

                entity.Property(e => e.CodigoPoliza)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_poliza");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Destino)
                    .HasMaxLength(50)
                    .HasColumnName("destino");

                entity.Property(e => e.DirDestino)
                    .HasMaxLength(50)
                    .HasColumnName("dir_destino");

                entity.Property(e => e.Dua)
                    .HasMaxLength(50)
                    .HasColumnName("dua");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaAbordaje)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_abordaje");

                entity.Property(e => e.FechaAceptacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_aceptacion");

                entity.Property(e => e.FechaLlegada)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_llegada");

                entity.Property(e => e.FechaPoliza)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_poliza");

                entity.Property(e => e.ModTransporte)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("mod_transporte");

                entity.Property(e => e.NitImpExp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nit_imp_exp");

                entity.Property(e => e.NoPoliza).HasMaxLength(50);

                entity.Property(e => e.NumeroOrden)
                    .HasMaxLength(50)
                    .HasColumnName("numero_orden");

                entity.Property(e => e.PaisProcede)
                    .HasMaxLength(50)
                    .HasColumnName("pais_procede");

                entity.Property(e => e.Piezas).HasColumnName("piezas");

                entity.Property(e => e.PoNumber)
                    .HasMaxLength(50)
                    .HasColumnName("po_number");

                entity.Property(e => e.PtoDescarga)
                    .HasMaxLength(50)
                    .HasColumnName("pto_descarga");

                entity.Property(e => e.Remitente)
                    .HasMaxLength(50)
                    .HasColumnName("remitente");

                entity.Property(e => e.Ticket)
                    .HasMaxLength(50)
                    .HasColumnName("ticket");

                entity.Property(e => e.TipoCambio).HasColumnName("tipo_cambio");

                entity.Property(e => e.TotalBultos).HasColumnName("total_bultos");

                entity.Property(e => e.TotalBultosPeso).HasColumnName("total_bultos_peso");

                entity.Property(e => e.TotalFlete).HasColumnName("total_flete");

                entity.Property(e => e.TotalGeneral).HasColumnName("total_general");

                entity.Property(e => e.TotalKgs).HasColumnName("total_kgs");

                entity.Property(e => e.TotalLineas).HasColumnName("total_lineas");

                entity.Property(e => e.TotalLiquidar).HasColumnName("total_liquidar");

                entity.Property(e => e.TotalOtros).HasColumnName("total_otros");

                entity.Property(e => e.TotalSeguro).HasColumnName("total_seguro");

                entity.Property(e => e.TotalUsd).HasColumnName("total_usd");

                entity.Property(e => e.TotalValoraduana).HasColumnName("total_valoraduana");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.Property(e => e.ViajeNo)
                    .HasMaxLength(50)
                    .HasColumnName("viaje_no");
            });

            modelBuilder.Entity<TransPeServicio>(entity =>
            {
                entity.HasKey(e => e.IdOrdenPedidoServicio)
                    .HasName("PK_trans_pe_Serv");

                entity.ToTable("trans_pe_servicios");

                entity.Property(e => e.IdOrdenPedidoServicio).ValueGeneratedNever();

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(150)
                    .HasColumnName("observacion");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<TransPeTipo>(entity =>
            {
                entity.HasKey(e => e.IdTipoPedido);

                entity.ToTable("trans_pe_tipo");

                entity.Property(e => e.IdTipoPedido).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ControlPoliza)
                    .HasColumnName("control_poliza")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Descripcion).HasMaxLength(250);

                entity.Property(e => e.GenerarPedidoIngresoBodegaDestino)
                    .HasColumnName("Generar_pedido_ingreso_bodega_destino")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IdTipoIngresoOc)
                    .HasColumnName("IdTipoIngresoOC")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.RequerirClienteEsBodegaWms)
                    .HasColumnName("requerir_cliente_es_bodega_wms")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RequerirDocumentoRef)
                    .HasColumnName("requerir_documento_ref")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TrasladarLotesDocIngreso)
                    .HasColumnName("trasladar_lotes_doc_ingreso")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TransPickingDet>(entity =>
            {
                entity.HasKey(e => e.IdPickingDet);

                entity.ToTable("trans_picking_det");

                entity.Property(e => e.IdPickingDet).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CantidadRecibida).HasColumnName("cantidad_recibida");

                entity.Property(e => e.ClienteDias).HasColumnName("cliente_dias");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(150)
                    .HasColumnName("nombre");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdOperadorBodegaNavigation)
                    .WithMany(p => p.TransPickingDets)
                    .HasForeignKey(d => d.IdOperadorBodega)
                    .HasConstraintName("FK_trans_picking_det_operador_bodega");

                entity.HasOne(d => d.IdPedidoDetNavigation)
                    .WithMany(p => p.TransPickingDets)
                    .HasForeignKey(d => d.IdPedidoDet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_picking_det_trans_pedido_det");

                entity.HasOne(d => d.IdPickingEncNavigation)
                    .WithMany(p => p.TransPickingDets)
                    .HasForeignKey(d => d.IdPickingEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_picking_det_trans_picking_enc");
            });

            modelBuilder.Entity<TransPickingDetParametro>(entity =>
            {
                entity.HasKey(e => e.IdParametroPicking);

                entity.ToTable("trans_picking_det_parametros");

                entity.Property(e => e.IdParametroPicking).ValueGeneratedNever();

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.ValorFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("valor_fecha");

                entity.Property(e => e.ValorLogico).HasColumnName("valor_logico");

                entity.Property(e => e.ValorNumerico).HasColumnName("valor_numerico");

                entity.Property(e => e.ValorTexto)
                    .HasMaxLength(50)
                    .HasColumnName("valor_texto");

                entity.HasOne(d => d.IdPickingDetNavigation)
                    .WithMany(p => p.TransPickingDetParametros)
                    .HasForeignKey(d => d.IdPickingDet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_picking_det_parametros_trans_picking_det");

                entity.HasOne(d => d.IdProductoParametroNavigation)
                    .WithMany(p => p.TransPickingDetParametros)
                    .HasForeignKey(d => d.IdProductoParametro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_picking_det_parametros_producto_parametros");
            });

            modelBuilder.Entity<TransPickingEnc>(entity =>
            {
                entity.HasKey(e => e.IdPickingEnc);

                entity.ToTable("trans_picking_enc");

                entity.Property(e => e.IdPickingEnc).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.DetalleOperador)
                    .HasColumnName("detalle_operador")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaPicking)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_picking");

                entity.Property(e => e.HoraFin)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin");

                entity.Property(e => e.HoraIni)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini");

                entity.Property(e => e.ProcesadoBof)
                    .HasColumnName("procesado_bof")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RequierePreparacion)
                    .HasColumnName("requiere_preparacion")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TipoPreparacion)
                    .HasMaxLength(50)
                    .HasColumnName("tipo_preparacion")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(30)
                    .HasColumnName("user_mod");

                entity.Property(e => e.VerificaAuto)
                    .HasColumnName("verifica_auto")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.TransPickingEncs)
                    .HasForeignKey(d => d.IdBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_picking_enc_bodega");

                entity.HasOne(d => d.IdPropietarioBodegaNavigation)
                    .WithMany(p => p.TransPickingEncs)
                    .HasForeignKey(d => d.IdPropietarioBodega)
                    .HasConstraintName("FK_trans_picking_enc_propietario_bodega");
            });

            modelBuilder.Entity<TransPickingOp>(entity =>
            {
                entity.HasKey(e => e.IdOperadorPicking);

                entity.ToTable("trans_picking_op");

                entity.Property(e => e.IdOperadorPicking).ValueGeneratedNever();

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdOperadorBodegaNavigation)
                    .WithMany(p => p.TransPickingOps)
                    .HasForeignKey(d => d.IdOperadorBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_picking_op_operador_bodega");

                entity.HasOne(d => d.IdPickingEncNavigation)
                    .WithMany(p => p.TransPickingOps)
                    .HasForeignKey(d => d.IdPickingEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_picking_op_trans_picking_enc");
            });

            modelBuilder.Entity<TransPickingUbic>(entity =>
            {
                entity.HasKey(e => e.IdPickingUbic);

                entity.ToTable("trans_picking_ubic");

                entity.HasIndex(e => e.IdPickingDet, "NCLI_Trans_Picking_Ubic_IdPickingDet_EJC");

                entity.HasIndex(e => new { e.DañadoVerificacion, e.DañadoPicking }, "NCLI_trans_picking_ubic_20200720_EJC");

                entity.HasIndex(e => e.IdProductoEstado, "NCLI_trans_picking_ubic_20200721_EJC");

                entity.HasIndex(e => new { e.IdProductoBodega, e.IdProductoEstado }, "NCLI_trans_picking_ubic_20210908_EJC");

                entity.Property(e => e.IdPickingUbic).ValueGeneratedNever();

                entity.Property(e => e.Acepto)
                    .HasColumnName("acepto")
                    .HasComment("Indica si el operador escaneó o no esta linea, se utiliza para saber si una línea del picking fue reemplazada por otra");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CantidadDespachada)
                    .HasColumnName("cantidad_despachada")
                    .HasComment("cantidad despachada");

                entity.Property(e => e.CantidadRecibida)
                    .HasColumnName("cantidad_recibida")
                    .HasComment("cantidad recibida en línea de picking en ubicación de bodega");

                entity.Property(e => e.CantidadSolicitada)
                    .HasColumnName("cantidad_solicitada")
                    .HasComment("cantidad solicitada en pedido");

                entity.Property(e => e.CantidadVerificada)
                    .HasColumnName("cantidad_verificada")
                    .HasDefaultValueSql("((0))")
                    .HasComment("cantidad verificada");

                entity.Property(e => e.DañadoPicking).HasColumnName("dañado_picking");

                entity.Property(e => e.DañadoVerificacion)
                    .HasColumnName("dañado_verificacion")
                    .HasDefaultValueSql("((0))")
                    .HasComment("si el producto en la línea de picking estaba dañado, se marca como dañado con este campo");

                entity.Property(e => e.Encontrado)
                    .HasColumnName("encontrado")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Si el producto fue encontrado en el picking ");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaDespachado)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_despachado")
                    .HasComment("fecha en la que se realizó el despacho del producto");

                entity.Property(e => e.FechaMinima)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_minima")
                    .HasComment("fecha mínima de vencimiento que puede ser despachada (sirve para reemplazar el producto por otra fecha de vencimiento)");

                entity.Property(e => e.FechaPacking)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_packing")
                    .HasComment("fecha y hora en la que se registró el packing de la línea del picking");

                entity.Property(e => e.FechaPicking)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_picking")
                    .HasComment("fecha y hora en la que se proceso la línea de picking");

                entity.Property(e => e.FechaRealVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_real_vence");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence")
                    .HasComment("fecha de vencimiento del producto");

                entity.Property(e => e.FechaVerificado)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_verificado")
                    .HasComment("fecha y hora en la que se proceso la verificación de la línea de picking");

                entity.Property(e => e.IdOperadorBodegaPickeo).HasColumnName("IdOperadorBodega_Pickeo");

                entity.Property(e => e.IdOperadorBodegaVerifico).HasColumnName("IdOperadorBodega_Verifico");

                entity.Property(e => e.IdStockReemplazo).HasColumnName("IdStock_reemplazo");

                entity.Property(e => e.IdUbicacion).HasComment("Ubicación en la bodega de donde será tomado el producto al momento del picking");

                entity.Property(e => e.IdUbicacionReemplazo).HasColumnName("IdUbicacion_reemplazo");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(25)
                    .HasColumnName("lic_plate")
                    .HasComment("código único de pallet");

                entity.Property(e => e.LicPlateReemplazo)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate_reemplazo");

                entity.Property(e => e.Lote)
                    .HasMaxLength(35)
                    .HasColumnName("lote")
                    .HasComment("Lote de producto a pickear");

                entity.Property(e => e.NoPacking)
                    .HasMaxLength(50)
                    .HasColumnName("no_packing")
                    .HasComment("Código del paquete en el que se almaceno x producto de la línea del picking");

                entity.Property(e => e.PesoDespachado)
                    .HasColumnName("peso_despachado")
                    .HasComment("Peso despachado");

                entity.Property(e => e.PesoRecibido)
                    .HasColumnName("peso_recibido")
                    .HasComment("Peso recibido en el picking");

                entity.Property(e => e.PesoSolicitado)
                    .HasColumnName("peso_solicitado")
                    .HasComment("Peso solicitado en el pedido");

                entity.Property(e => e.PesoVerificado)
                    .HasColumnName("peso_verificado")
                    .HasComment("Peso verificado en el picking");

                entity.Property(e => e.Serial)
                    .HasMaxLength(35)
                    .HasColumnName("serial")
                    .HasComment("número de serie, aplica para equipos con serie, no serializados ej: linea blanca, motores, etc.");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdPickingDetNavigation)
                    .WithMany(p => p.TransPickingUbics)
                    .HasForeignKey(d => d.IdPickingDet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_picking_ubic_trans_picking_det");
            });

            modelBuilder.Entity<TransReDet>(entity =>
            {
                entity.HasKey(e => new { e.IdRecepcionDet, e.IdRecepcionEnc })
                    .HasName("PK_trans_recepcion_det");

                entity.ToTable("trans_re_det");

                entity.HasIndex(e => e.IdProductoBodega, "NCL_Trans_re_Det_20200115_EJC");

                entity.HasIndex(e => e.IdRecepcionEnc, "NCL_trans_re_det_rep_20200115_ejc");

                entity.Property(e => e.AtributoVariante1)
                    .HasMaxLength(25)
                    .HasColumnName("atributo_variante_1");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.CantidadRecibida).HasColumnName("cantidad_recibida");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.CostoEstadistico).HasColumnName("costo_estadistico");

                entity.Property(e => e.CostoOc).HasColumnName("costo_oc");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoLinea).HasColumnName("No_Linea");

                entity.Property(e => e.NombrePresentacion)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_presentacion");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .HasColumnName("nombre_producto");

                entity.Property(e => e.NombreProductoEstado)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_producto_estado");

                entity.Property(e => e.NombreUnidadMedida)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_unidad_medida");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(150)
                    .HasColumnName("observacion");

                entity.Property(e => e.PalletNoEstandar)
                    .HasColumnName("pallet_no_estandar")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.PesoEstadistico).HasColumnName("peso_estadistico");

                entity.Property(e => e.PesoMaximo).HasColumnName("peso_maximo");

                entity.Property(e => e.PesoMinimo).HasColumnName("peso_minimo");

                entity.Property(e => e.PesoUnitario)
                    .HasColumnName("peso_unitario")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.HasOne(d => d.IdMotivoDevolucionNavigation)
                    .WithMany(p => p.TransReDets)
                    .HasForeignKey(d => d.IdMotivoDevolucion)
                    .HasConstraintName("FK_trans_re_det_motivo_devolucion");

                entity.HasOne(d => d.IdOperadorBodegaNavigation)
                    .WithMany(p => p.TransReDets)
                    .HasForeignKey(d => d.IdOperadorBodega)
                    .HasConstraintName("FK_trans_re_det_operador_bodega");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.TransReDets)
                    .HasForeignKey(d => d.IdPresentacion)
                    .HasConstraintName("FK_trans_recepcion_det_producto_presentacion");

                entity.HasOne(d => d.IdProductoBodegaNavigation)
                    .WithMany(p => p.TransReDets)
                    .HasForeignKey(d => d.IdProductoBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_re_det_producto_bodega");

                entity.HasOne(d => d.IdProductoEstadoNavigation)
                    .WithMany(p => p.TransReDets)
                    .HasForeignKey(d => d.IdProductoEstado)
                    .HasConstraintName("FK_trans_recepcion_det_producto_estado");

                entity.HasOne(d => d.IdRecepcionEncNavigation)
                    .WithMany(p => p.TransReDets)
                    .HasForeignKey(d => d.IdRecepcionEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_recepcion_det_trans_recepcion_enc");

                entity.HasOne(d => d.IdUnidadMedidaNavigation)
                    .WithMany(p => p.TransReDets)
                    .HasForeignKey(d => d.IdUnidadMedida)
                    .HasConstraintName("FK_trans_re_det_unidad_medida");
            });

            modelBuilder.Entity<TransReDetInfraccion>(entity =>
            {
                entity.HasKey(e => e.IdRecepcionDetInfraccion);

                entity.ToTable("trans_re_det_infraccion");

                entity.Property(e => e.IdRecepcionDetInfraccion).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdOrdenCompraEncNavigation)
                    .WithMany(p => p.TransReDetInfraccions)
                    .HasForeignKey(d => d.IdOrdenCompraEnc)
                    .HasConstraintName("FK_trans_re_det_infraccion_trans_oc_enc");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.TransReDetInfraccions)
                    .HasForeignKey(d => d.IdPresentacion)
                    .HasConstraintName("FK_trans_re_det_infraccion_producto_presentacion");

                entity.HasOne(d => d.IdProductoBodegaNavigation)
                    .WithMany(p => p.TransReDetInfraccions)
                    .HasForeignKey(d => d.IdProductoBodega)
                    .HasConstraintName("FK_trans_re_det_infraccion_producto_bodega");

                entity.HasOne(d => d.IdRecepcionEncNavigation)
                    .WithMany(p => p.TransReDetInfraccions)
                    .HasForeignKey(d => d.IdRecepcionEnc)
                    .HasConstraintName("FK_trans_re_det_infraccion_trans_re_enc");

                entity.HasOne(d => d.IdReglaPropietarioEncNavigation)
                    .WithMany(p => p.TransReDetInfraccions)
                    .HasForeignKey(d => d.IdReglaPropietarioEnc)
                    .HasConstraintName("FK_trans_re_det_infraccion_propietario_reglas_enc");
            });

            modelBuilder.Entity<TransReDetLoteNum>(entity =>
            {
                entity.HasKey(e => e.IdLoteNum)
                    .HasName("PK_lote_num");

                entity.ToTable("trans_re_det_lote_num");

                entity.Property(e => e.IdLoteNum).ValueGeneratedNever();

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.FechaIngreso).HasColumnType("date");

                entity.Property(e => e.Lote).HasMaxLength(50);

                entity.Property(e => e.LoteNumerico).HasColumnName("Lote_Numerico");
            });

            modelBuilder.Entity<TransReDetParametro>(entity =>
            {
                entity.HasKey(e => new { e.IdParametroDet, e.IdRecepcionDet, e.IdRecepcionEnc })
                    .HasName("PK_trans_recepcion_det_parametros");

                entity.ToTable("trans_re_det_parametros");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.ValorFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("valor_fecha");

                entity.Property(e => e.ValorLogico).HasColumnName("valor_logico");

                entity.Property(e => e.ValorNumerico).HasColumnName("valor_numerico");

                entity.Property(e => e.ValorTexto)
                    .HasMaxLength(50)
                    .HasColumnName("valor_texto");

                entity.HasOne(d => d.IdProductoParametroNavigation)
                    .WithMany(p => p.TransReDetParametros)
                    .HasForeignKey(d => d.IdProductoParametro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_re_det_parametros_producto_parametros");
            });

            modelBuilder.Entity<TransReEnc>(entity =>
            {
                entity.HasKey(e => e.IdRecepcionEnc)
                    .HasName("PK_trans_recepcion_enc");

                entity.ToTable("trans_re_enc");

                entity.Property(e => e.IdRecepcionEnc).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Bloqueada)
                    .HasColumnName("bloqueada")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BloqueadaPor)
                    .HasMaxLength(2)
                    .HasColumnName("bloqueada_por");

                entity.Property(e => e.CorreoEnviado).HasDefaultValueSql("((0))");

                entity.Property(e => e.EscanearRecUbic).HasColumnName("escanear_rec_ubic");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_recepcion");

                entity.Property(e => e.FechaTarea)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_tarea");

                entity.Property(e => e.FirmaPiloto)
                    .HasColumnType("image")
                    .HasColumnName("firma_piloto");

                entity.Property(e => e.HabilitarStock).HasColumnName("Habilitar_Stock");

                entity.Property(e => e.HoraFinPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin_pc");

                entity.Property(e => e.HoraIniPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini_pc");

                entity.Property(e => e.IdTipoTransaccion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Idmotivoanulacionbodega).HasColumnName("idmotivoanulacionbodega");

                entity.Property(e => e.Idpiloto).HasColumnName("idpiloto");

                entity.Property(e => e.Idusuariobloqueo).HasColumnName("idusuariobloqueo");

                entity.Property(e => e.Idvehiculo).HasColumnName("idvehiculo");

                entity.Property(e => e.MostrarCantidadEsperada)
                    .HasColumnName("mostrar_cantidad_esperada")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MuestraPrecio).HasColumnName("muestra_precio");

                entity.Property(e => e.NoGuia).HasMaxLength(50);

                entity.Property(e => e.NoMarchamo)
                    .HasMaxLength(50)
                    .HasColumnName("No_Marchamo");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(100)
                    .HasColumnName("observacion");

                entity.Property(e => e.ParaPorCodigo)
                    .HasColumnName("para_por_codigo")
                    .HasComment("verdadero si se capturaran una vez únicamente los parametros por producto o se capturan cada vez que se ingrese el código");

                entity.Property(e => e.RevisionInconsistencia).HasColumnName("Revision_Inconsistencia");

                entity.Property(e => e.TomarFotos).HasColumnName("tomar_fotos");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(30)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdMuelleNavigation)
                    .WithMany(p => p.TransReEncs)
                    .HasForeignKey(d => d.IdMuelle)
                    .HasConstraintName("FK_trans_recepcion_enc_bodega_muelles");

                entity.HasOne(d => d.IdPropietarioBodegaNavigation)
                    .WithMany(p => p.TransReEncs)
                    .HasForeignKey(d => d.IdPropietarioBodega)
                    .HasConstraintName("FK_trans_recepcion_enc_propietario_bodega");

                entity.HasOne(d => d.IdTipoTransaccionNavigation)
                    .WithMany(p => p.TransReEncs)
                    .HasForeignKey(d => d.IdTipoTransaccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_re_enc_trans_re_tr");
            });

            modelBuilder.Entity<TransReFact>(entity =>
            {
                entity.HasKey(e => e.IdFacturaRecepcion);

                entity.ToTable("trans_re_fact");

                entity.HasIndex(e => e.NoFactura, "numerofactura")
                    .IsUnique();

                entity.Property(e => e.IdFacturaRecepcion).ValueGeneratedNever();

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.NoFactura).HasMaxLength(50);

                entity.Property(e => e.Observacion).HasMaxLength(250);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdRecepcionEncNavigation)
                    .WithMany(p => p.TransReFacts)
                    .HasForeignKey(d => d.IdRecepcionEnc)
                    .HasConstraintName("FK_trans_re_fact_trans_re_enc");
            });

            modelBuilder.Entity<TransReImg>(entity =>
            {
                entity.HasKey(e => new { e.IdImagen, e.IdRecepcionEnc });

                entity.ToTable("trans_re_img");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.Imagen)
                    .IsRequired()
                    .HasColumnType("image");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(150)
                    .HasColumnName("observacion");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.HasOne(d => d.IdRecepcionEncNavigation)
                    .WithMany(p => p.TransReImgs)
                    .HasForeignKey(d => d.IdRecepcionEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_recepcion_img_trans_recepcion_enc");
            });

            modelBuilder.Entity<TransReOc>(entity =>
            {
                entity.HasKey(e => new { e.IdRecepcionOc, e.IdRecepcionEnc })
                    .HasName("PK_trans_recepcion_oc");

                entity.ToTable("trans_re_oc");

                entity.HasIndex(e => e.IdRecepcionEnc, "NCLI_trans_re_oc_IdRecepcionEnc_20200728");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FirmaOperador)
                    .HasColumnType("image")
                    .HasColumnName("firma_operador");

                entity.Property(e => e.HoraFinHh)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin_hh");

                entity.Property(e => e.HoraIniHh)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini_hh");

                entity.Property(e => e.NoDocto)
                    .HasMaxLength(50)
                    .HasColumnName("no_docto");

                entity.Property(e => e.RecepcionCiega).HasColumnName("recepcion_ciega");

                entity.Property(e => e.RecepcionManual).HasColumnName("recepcion_manual");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.HasOne(d => d.IdOrdenCompraEncNavigation)
                    .WithMany(p => p.TransReOcs)
                    .HasForeignKey(d => d.IdOrdenCompraEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_recepcion_oc_trans_orden_compra_enc");

                entity.HasOne(d => d.IdRecepcionEncNavigation)
                    .WithMany(p => p.TransReOcs)
                    .HasForeignKey(d => d.IdRecepcionEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_recepcion_oc_trans_recepcion_enc");
            });

            modelBuilder.Entity<TransReOp>(entity =>
            {
                entity.HasKey(e => new { e.IdOperadorRec, e.IdRecepcionEnc })
                    .HasName("PK_trans_recepcion_operadores");

                entity.ToTable("trans_re_op");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdOperadorBodegaNavigation)
                    .WithMany(p => p.TransReOps)
                    .HasForeignKey(d => d.IdOperadorBodega)
                    .HasConstraintName("FK_trans_re_op_operador_bodega");

                entity.HasOne(d => d.IdRecepcionEncNavigation)
                    .WithMany(p => p.TransReOps)
                    .HasForeignKey(d => d.IdRecepcionEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_recepcion_operadores_trans_recepcion_enc");
            });

            modelBuilder.Entity<TransReTr>(entity =>
            {
                entity.HasKey(e => e.IdTipoTransaccion);

                entity.ToTable("trans_re_tr");

                entity.Property(e => e.IdTipoTransaccion).HasMaxLength(50);

                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.Property(e => e.ConRef).HasDefaultValueSql("((0))");

                entity.Property(e => e.DescDev).HasColumnType("text");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Funcionalidad).HasColumnType("text");

                entity.Property(e => e.TipoTrans).HasMaxLength(25);

                entity.Property(e => e.UsaHh)
                    .HasColumnName("UsaHH")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TransReabastecimientoLog>(entity =>
            {
                entity.HasKey(e => e.IdReabastecimientoLog);

                entity.ToTable("trans_reabastecimiento_log");

                entity.Property(e => e.IdReabastecimientoLog).ValueGeneratedNever();

                entity.Property(e => e.Cancelado).HasDefaultValueSql("((0))");

                entity.Property(e => e.CantidadAUbicar).HasColumnName("Cantidad_A_Ubicar");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("Codigo_Producto");

                entity.Property(e => e.DisponibleUmbas).HasColumnName("DisponibleUMBas");

                entity.Property(e => e.Enviado).HasDefaultValueSql("((0))");

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaGeneracionInexistencia)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Generacion_Inexistencia")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaProcesamientoBof)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Procesamiento_BOF");

                entity.Property(e => e.FechaProcesamientoHh)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Procesamiento_HH");

                entity.Property(e => e.HoraGeneracionInexistencia)
                    .HasColumnType("datetime")
                    .HasColumnName("Hora_Generacion_Inexistencia");

                entity.Property(e => e.HoraProcesamientoBof)
                    .HasColumnType("date")
                    .HasColumnName("Hora_Procesamiento_BOF");

                entity.Property(e => e.NombrePresentacionAbastecerCon).HasMaxLength(50);

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .HasColumnName("Nombre_Producto");

                entity.Property(e => e.NombrePropietario)
                    .HasMaxLength(100)
                    .HasColumnName("Nombre_Propietario");

                entity.Property(e => e.NombreUmBas).HasMaxLength(50);

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.ProcesadoHh)
                    .HasColumnName("Procesado_HH")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.StockDisponible).HasColumnName("Stock_Disponible");

                entity.Property(e => e.StockInferior).HasColumnName("Stock_Inferior");

                entity.Property(e => e.StockUbicacion).HasColumnName("Stock_Ubicacion");

                entity.Property(e => e.StockUmbas).HasColumnName("StockUMBas");

                entity.Property(e => e.Ubicacion).HasMaxLength(200);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(30)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<TransServicioDet>(entity =>
            {
                entity.HasKey(e => e.IdServicioDet)
                    .HasName("PK__trans_se__454F2235F49DD03F");

                entity.ToTable("trans_servicio_det");

                entity.Property(e => e.IdServicioDet).ValueGeneratedNever();

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.CorreCatalogoproductos).HasColumnName("corre_catalogoproductos");

                entity.Property(e => e.CorreDetalleacuerdo).HasColumnName("corre_detalleacuerdo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.NombreServicio)
                    .HasMaxLength(150)
                    .HasColumnName("nombre_servicio");

                entity.Property(e => e.NombreUnidad)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_unidad");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(150)
                    .HasColumnName("observacion");

                entity.Property(e => e.UnidMedida).HasColumnName("unid_medida");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.TransServicioDets)
                    .HasForeignKey(d => d.IdPropietario)
                    .HasConstraintName("FK_trans_servicio_det_propietarios");

                entity.HasOne(d => d.IdServicioEncNavigation)
                    .WithMany(p => p.TransServicioDets)
                    .HasForeignKey(d => d.IdServicioEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_servicio_det_trans_servicio_det");
            });

            modelBuilder.Entity<TransServicioEnc>(entity =>
            {
                entity.HasKey(e => e.IdServicioEnc)
                    .HasName("PK_servicio_enc");

                entity.ToTable("trans_servicio_enc");

                entity.Property(e => e.IdServicioEnc).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.EnviadoAErp).HasColumnName("enviado_a_erp");

                entity.Property(e => e.EsIngreso)
                    .HasColumnName("es_ingreso")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('Abierto')");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaDocIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_doc_ingreso")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaServicio)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_servicio")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NoOrden)
                    .HasMaxLength(50)
                    .HasColumnName("no_orden");

                entity.Property(e => e.NoPoliza)
                    .HasMaxLength(50)
                    .HasColumnName("no_poliza");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.TransServicioEncs)
                    .HasForeignKey(d => d.IdBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_servicio_enc_bodega");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.TransServicioEncs)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_servicio_enc_empresa");

                entity.HasOne(d => d.IdOrdenCompraEncNavigation)
                    .WithMany(p => p.TransServicioEncs)
                    .HasForeignKey(d => d.IdOrdenCompraEnc)
                    .HasConstraintName("FK_servicio_enc_trans_oc_enc");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.TransServicioEncs)
                    .HasForeignKey(d => d.IdPropietario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_servicio_enc_propietarios");
            });

            modelBuilder.Entity<TransTrasDet>(entity =>
            {
                entity.HasKey(e => e.IdTrasladoDet);

                entity.ToTable("trans_tras_det");

                entity.Property(e => e.IdTrasladoDet).ValueGeneratedNever();

                entity.Property(e => e.CantDespachada).HasColumnName("cant_despachada");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FechaEspecifica).HasColumnName("fecha_especifica");

                entity.Property(e => e.NomEstado)
                    .HasMaxLength(50)
                    .HasColumnName("nom_estado");

                entity.Property(e => e.NomPresentacion)
                    .HasMaxLength(50)
                    .HasColumnName("nom_presentacion");

                entity.Property(e => e.NomUnidMed)
                    .HasMaxLength(50)
                    .HasColumnName("nom_unid_med");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_producto");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TransTrasDets)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_tras_det_producto_estado");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.TransTrasDets)
                    .HasForeignKey(d => d.IdPresentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_tras_det_producto_presentacion");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.TransTrasDets)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_tras_det_producto");

                entity.HasOne(d => d.IdTrasladoEncNavigation)
                    .WithMany(p => p.TransTrasDets)
                    .HasForeignKey(d => d.IdTrasladoEnc)
                    .HasConstraintName("FK_trans_tras_det_trans_tras_enc");
            });

            modelBuilder.Entity<TransTrasEnc>(entity =>
            {
                entity.HasKey(e => e.IdTrasladoEnc);

                entity.ToTable("trans_tras_enc");

                entity.Property(e => e.IdTrasladoEnc).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Anulado).HasColumnName("anulado");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaEntrega).HasColumnType("datetime");

                entity.Property(e => e.FechaTraslado).HasColumnType("datetime");

                entity.Property(e => e.HoraEntregaDesde).HasColumnType("datetime");

                entity.Property(e => e.HoraEntregaHasta).HasColumnType("datetime");

                entity.Property(e => e.HoraFin)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin");

                entity.Property(e => e.HoraIni)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Local).HasColumnName("local");

                entity.Property(e => e.NoDocumento).HasColumnName("no_documento");

                entity.Property(e => e.NoGuia).HasMaxLength(50);

                entity.Property(e => e.PalletPrimero)
                    .HasColumnName("pallet_primero")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ubicacion)
                    .HasMaxLength(35)
                    .HasColumnName("ubicacion");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(30)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdBodegaDestinoNavigation)
                    .WithMany(p => p.TransTrasEncIdBodegaDestinoNavigations)
                    .HasForeignKey(d => d.IdBodegaDestino)
                    .HasConstraintName("FK_trans_tras_enc_bodega1");

                entity.HasOne(d => d.IdBodegaOrigenNavigation)
                    .WithMany(p => p.TransTrasEncIdBodegaOrigenNavigations)
                    .HasForeignKey(d => d.IdBodegaOrigen)
                    .HasConstraintName("FK_trans_tras_enc_bodega");

                entity.HasOne(d => d.IdMuelleOrigenNavigation)
                    .WithMany(p => p.TransTrasEncs)
                    .HasForeignKey(d => d.IdMuelleOrigen)
                    .HasConstraintName("FK_trans_tras_enc_bodega_muelles");

                entity.HasOne(d => d.IdPilotoNavigation)
                    .WithMany(p => p.TransTrasEncs)
                    .HasForeignKey(d => d.IdPiloto)
                    .HasConstraintName("FK_trans_tras_enc_empresa_transporte_pilotos");

                entity.HasOne(d => d.IdPropietarioBodegaNavigation)
                    .WithMany(p => p.TransTrasEncs)
                    .HasForeignKey(d => d.IdPropietarioBodega)
                    .HasConstraintName("FK_trans_tras_enc_propietario_bodega");

                entity.HasOne(d => d.IdRutaNavigation)
                    .WithMany(p => p.TransTrasEncs)
                    .HasForeignKey(d => d.IdRuta)
                    .HasConstraintName("FK_trans_tras_enc_road_ruta");

                entity.HasOne(d => d.IdVehiculoNavigation)
                    .WithMany(p => p.TransTrasEncs)
                    .HasForeignKey(d => d.IdVehiculo)
                    .HasConstraintName("FK_trans_tras_enc_empresa_transporte_vehiculos");
            });

            modelBuilder.Entity<TransTrasOp>(entity =>
            {
                entity.HasKey(e => e.IdOperadorTras);

                entity.ToTable("trans_tras_op");

                entity.Property(e => e.IdOperadorTras).ValueGeneratedNever();

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdOperadorBodegaNavigation)
                    .WithMany(p => p.TransTrasOps)
                    .HasForeignKey(d => d.IdOperadorBodega)
                    .HasConstraintName("FK_trans_tras_op_operador_bodega");

                entity.HasOne(d => d.IdTrasladoEncNavigation)
                    .WithMany(p => p.TransTrasOps)
                    .HasForeignKey(d => d.IdTrasladoEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_tras_op_trans_tras_enc");
            });

            modelBuilder.Entity<TransUbicHhDet>(entity =>
            {
                entity.HasKey(e => new { e.IdTareaUbicacionEnc, e.IdTareaUbicacionDet });

                entity.ToTable("trans_ubic_hh_det");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.AtributoVariante1)
                    .HasMaxLength(25)
                    .HasColumnName("atributo_variante_1");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Estado)
                    .HasMaxLength(25)
                    .HasColumnName("estado")
                    .HasDefaultValueSql("('Pendiente')");

                entity.Property(e => e.HoraFin).HasColumnType("datetime");

                entity.Property(e => e.HoraInicio).HasColumnType("datetime");

                entity.Property(e => e.Recibido)
                    .HasColumnName("recibido")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdEstadoDestinoNavigation)
                    .WithMany(p => p.TransUbicHhDetIdEstadoDestinoNavigations)
                    .HasForeignKey(d => d.IdEstadoDestino)
                    .HasConstraintName("FK_trans_ubic_hh_det_producto_estado_destino");

                entity.HasOne(d => d.IdEstadoOrigenNavigation)
                    .WithMany(p => p.TransUbicHhDetIdEstadoOrigenNavigations)
                    .HasForeignKey(d => d.IdEstadoOrigen)
                    .HasConstraintName("FK_trans_ubic_hh_det_producto_estado_orig");

                entity.HasOne(d => d.IdOperadorBodegaNavigation)
                    .WithMany(p => p.TransUbicHhDets)
                    .HasForeignKey(d => d.IdOperadorBodega)
                    .HasConstraintName("FK_trans_ubic_hh_det_operador_bodega");

                entity.HasOne(d => d.IdTareaUbicacionEncNavigation)
                    .WithMany(p => p.TransUbicHhDets)
                    .HasForeignKey(d => d.IdTareaUbicacionEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_ubic_hh_det_trans_ubic_hh_enc");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.TransUbicHhDetIds)
                    .HasForeignKey(d => new { d.IdUbicacionDestino, d.IdBodega })
                    .HasConstraintName("FK_trans_ubic_hh_det_bodega_ubic_dest");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.TransUbicHhDetIdNavigations)
                    .HasForeignKey(d => new { d.IdUbicacionOrigen, d.IdBodega })
                    .HasConstraintName("FK_trans_ubic_hh_det_bodega_ubic_orig");
            });

            modelBuilder.Entity<TransUbicHhEnc>(entity =>
            {
                entity.HasKey(e => e.IdTareaUbicacionEnc)
                    .HasName("PK_trans_ubic_hh_enc_1");

                entity.ToTable("trans_ubic_hh_enc");

                entity.Property(e => e.IdTareaUbicacionEnc).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CambioEstado)
                    .HasColumnName("cambio_estado")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .HasColumnName("estado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaFin).HasColumnType("date");

                entity.Property(e => e.FechaInicio).HasColumnType("date");

                entity.Property(e => e.HoraFin).HasColumnType("datetime");

                entity.Property(e => e.HoraInicio).HasColumnType("datetime");

                entity.Property(e => e.Observacion).HasMaxLength(150);

                entity.Property(e => e.OperadorPorLinea).HasColumnName("operador_por_linea");

                entity.Property(e => e.UbicacionConHh)
                    .HasColumnName("ubicacion_con_hh")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdPropietarioBodegaNavigation)
                    .WithMany(p => p.TransUbicHhEncs)
                    .HasForeignKey(d => d.IdPropietarioBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_ubic_hh_enc_propietario_bodega");
            });

            modelBuilder.Entity<TransUbicHhOp>(entity =>
            {
                entity.HasKey(e => new { e.IdTransUbicHhOp, e.IdTareaUbicacionEnc });

                entity.ToTable("trans_ubic_hh_op");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdOperadorBodegaNavigation)
                    .WithMany(p => p.TransUbicHhOps)
                    .HasForeignKey(d => d.IdOperadorBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trans_ubic_hh_op_operador_bodega");
            });

            modelBuilder.Entity<TransUbicHhSe>(entity =>
            {
                entity.HasKey(e => e.IdTareaUbicacionDetSe);

                entity.ToTable("trans_ubic_hh_se");

                entity.Property(e => e.IdTareaUbicacionDetSe).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.HoraFin).HasColumnType("datetime");

                entity.Property(e => e.HoraInicio).HasColumnType("datetime");
            });

            modelBuilder.Entity<TransUbicHhStock>(entity =>
            {
                entity.HasKey(e => e.IdStockTransUbicHhdet);

                entity.ToTable("trans_ubic_hh_stock");

                entity.Property(e => e.IdStockTransUbicHhdet)
                    .ValueGeneratedNever()
                    .HasColumnName("IdStockTransUbicHHDet");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.AtributoVariante1)
                    .HasMaxLength(25)
                    .HasColumnName("atributo_variante_1");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Cantidad)
                    .HasColumnName("cantidad")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaManufactura)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_manufactura");

                entity.Property(e => e.FechaMovHist)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_mov_hist")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.LicPlate)
                    .IsUnicode(false)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoBulto).HasColumnName("no_bulto");

                entity.Property(e => e.Peso)
                    .HasColumnName("peso")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.Temperatura)
                    .HasColumnName("temperatura")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UdsLicPlate)
                    .IsUnicode(false)
                    .HasColumnName("uds_lic_plate");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdPedidoEncNavigation)
                    .WithMany(p => p.TransUbicHhStocks)
                    .HasForeignKey(d => d.IdPedidoEnc)
                    .HasConstraintName("FK_stock_ubic_hh_trans_pe_enc");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.TransUbicHhStocks)
                    .HasForeignKey(d => d.IdPresentacion)
                    .HasConstraintName("FK_stock_ubic_hh_producto_presentacion");

                entity.HasOne(d => d.IdProductoBodegaNavigation)
                    .WithMany(p => p.TransUbicHhStocks)
                    .HasForeignKey(d => d.IdProductoBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stock_ubic_hh_producto_bodega");

                entity.HasOne(d => d.IdProductoEstadoNavigation)
                    .WithMany(p => p.TransUbicHhStocks)
                    .HasForeignKey(d => d.IdProductoEstado)
                    .HasConstraintName("FK_stock_ubic_hh_producto_estado");

                entity.HasOne(d => d.IdPropietarioBodegaNavigation)
                    .WithMany(p => p.TransUbicHhStocks)
                    .HasForeignKey(d => d.IdPropietarioBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stock_ubic_hh_propietario_bodega");

                entity.HasOne(d => d.IdUnidadMedidaNavigation)
                    .WithMany(p => p.TransUbicHhStocks)
                    .HasForeignKey(d => d.IdUnidadMedida)
                    .HasConstraintName("FK_stock_ubic_hh_unidad_medida");

                entity.HasOne(d => d.IdRecepcion)
                    .WithMany(p => p.TransUbicHhStocks)
                    .HasForeignKey(d => new { d.IdRecepcionDet, d.IdRecepcionEnc })
                    .HasConstraintName("FK_stock_ubic_hh_trans_re_det");
            });

            modelBuilder.Entity<TransUbicTarima>(entity =>
            {
                entity.HasKey(e => e.IdTarimaTareaUbic);

                entity.ToTable("trans_ubic_tarima");

                entity.Property(e => e.IdTarimaTareaUbic).ValueGeneratedNever();

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaUtilizacion).HasColumnType("datetime");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdTareaUbicacionEncNavigation)
                    .WithMany(p => p.TransUbicTarimas)
                    .HasForeignKey(d => d.IdTareaUbicacionEnc)
                    .HasConstraintName("FK_trans_ubic_tarima_trans_ubic_hh_enc");

                entity.HasOne(d => d.IdTarimaNavigation)
                    .WithMany(p => p.TransUbicTarimas)
                    .HasForeignKey(d => d.IdTarima)
                    .HasConstraintName("FK_trans_ubic_tarima_tarimas");
            });

            modelBuilder.Entity<TransaccionesLog>(entity =>
            {
                entity.HasKey(e => new { e.IdTransaccionLog, e.IdEmpresa, e.IdPropietarioBodega, e.IdObservacion });

                entity.ToTable("transacciones_log");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CantidadReabasto).HasColumnName("cantidad_reabasto");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.TransaccionesLogs)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_transacciones_log_empresa");

                entity.HasOne(d => d.IdObservacionNavigation)
                    .WithMany(p => p.TransaccionesLogs)
                    .HasForeignKey(d => d.IdObservacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_transacciones_log_sis_obs_log");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.TransaccionesLogs)
                    .HasForeignKey(d => d.IdPresentacion)
                    .HasConstraintName("FK_transacciones_log_producto_presentacion");

                entity.HasOne(d => d.IdProductoBodegaNavigation)
                    .WithMany(p => p.TransaccionesLogs)
                    .HasForeignKey(d => d.IdProductoBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_transacciones_log_producto_bodega");

                entity.HasOne(d => d.IdProductoEstadoNavigation)
                    .WithMany(p => p.TransaccionesLogs)
                    .HasForeignKey(d => d.IdProductoEstado)
                    .HasConstraintName("FK_transacciones_log_producto_estado");

                entity.HasOne(d => d.IdPropietarioBodegaNavigation)
                    .WithMany(p => p.TransaccionesLogs)
                    .HasForeignKey(d => d.IdPropietarioBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_transacciones_log_propietario_bodega");

                entity.HasOne(d => d.IdUnidadMedidaNavigation)
                    .WithMany(p => p.TransaccionesLogs)
                    .HasForeignKey(d => d.IdUnidadMedida)
                    .HasConstraintName("FK_transacciones_log_unidad_medida");
            });

            modelBuilder.Entity<Turno>(entity =>
            {
                entity.HasKey(e => e.IdTurno);

                entity.ToTable("turno");

                entity.Property(e => e.IdTurno).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.Turnos)
                    .HasForeignKey(d => d.IdBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_turno_bodega");
            });

            modelBuilder.Entity<UbicacionesPorRegla>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ubicaciones_por_regla");

                entity.Property(e => e.AceptaPallet).HasColumnName("acepta_pallet");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.Bloqueada).HasColumnName("bloqueada");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IndiceX).HasColumnName("indice_x");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.Nivel).HasColumnName("nivel");

                entity.Property(e => e.ReglaUbicDetPeActivo).HasColumnName("regla_ubic_det_pe_Activo");

                entity.Property(e => e.ReglaUbicDetPropActivo).HasColumnName("regla_ubic_det_prop_Activo");

                entity.Property(e => e.ReglaUbicDetTpActivo).HasColumnName("regla_ubic_det_tp_Activo");
            });

            modelBuilder.Entity<UnidadMedidaConversion>(entity =>
            {
                entity.HasKey(e => e.IdConversion)
                    .HasName("PK_conversion_unidad_medida");

                entity.ToTable("unidad_medida_conversion");

                entity.Property(e => e.IdConversion).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");
            });

            modelBuilder.Entity<UnidadMedidum>(entity =>
            {
                entity.HasKey(e => e.IdUnidadMedida)
                    .HasName("PK_producto_unidad_medida");

                entity.ToTable("unidad_medida");

                entity.Property(e => e.IdUnidadMedida).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(25)
                    .HasColumnName("codigo");

                entity.Property(e => e.EsUmCobro)
                    .HasColumnName("es_um_cobro")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Factor)
                    .HasColumnName("factor")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.UnidadMedida)
                    .HasForeignKey(d => d.IdPropietario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_unidad_medida_propietarios");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK_usuario_1");

                entity.ToTable("usuario");

                entity.Property(e => e.IdUsuario).ValueGeneratedNever();

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(100)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(25)
                    .HasColumnName("cedula");

                entity.Property(e => e.Clave)
                    .HasMaxLength(50)
                    .HasColumnName("clave");

                entity.Property(e => e.ClaveAutorizacion)
                    .HasMaxLength(50)
                    .HasColumnName("clave_autorizacion");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .HasColumnName("direccion");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Foto)
                    .HasColumnType("image")
                    .HasColumnName("foto");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(100)
                    .HasColumnName("nombres");

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(25)
                    .HasColumnName("telefono");

                entity.Property(e => e.UltimoLogin)
                    .HasColumnType("datetime")
                    .HasColumnName("ultimo_login");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(30)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK_usuario_empresa");
            });

            modelBuilder.Entity<UsuarioBodega>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioBodega)
                    .HasName("PK_usuarios_empresa");

                entity.ToTable("usuario_bodega");

                entity.Property(e => e.IdUsuarioBodega).ValueGeneratedNever();

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(30)
                    .HasColumnName("user_mod");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.UsuarioBodegas)
                    .HasForeignKey(d => d.IdBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_usuarios_empresa_bodega");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioBodegaIdUsuarioNavigations)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_usuarios_empresa_usuario");

                entity.HasOne(d => d.IdUsuarioSuperiorNavigation)
                    .WithMany(p => p.UsuarioBodegaIdUsuarioSuperiorNavigations)
                    .HasForeignKey(d => d.IdUsuarioSuperior)
                    .HasConstraintName("FK_usuario_bodega_usuario");
            });

            modelBuilder.Entity<VMotivoAnulacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_motivo_anulacion");

                entity.Property(e => e.Bodega)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.MotivoAnulacion)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VTransPedido>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_trans_pedido");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Bodega)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(103)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Estado).HasMaxLength(20);

                entity.Property(e => e.FechaPedido)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Pedido");

                entity.Property(e => e.Muelle)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NoDocumento).HasColumnName("no_documento");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.RoadRuta)
                    .IsRequired()
                    .HasMaxLength(69)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.RoadVendedor).HasMaxLength(61);
            });

            modelBuilder.Entity<VersionWmsHh>(entity =>
            {
                entity.HasKey(e => e.IdEmpresaVersion)
                    .HasName("PK_P_EMPRESA_VERSION");

                entity.ToTable("version_wms_hh");

                entity.Property(e => e.IdEmpresaVersion).ValueGeneratedNever();

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Notas)
                    .HasMaxLength(150)
                    .HasColumnName("notas");

                entity.Property(e => e.Version)
                    .HasMaxLength(50)
                    .HasColumnName("version");
            });

            modelBuilder.Entity<VwAjuste>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Ajustes");

                entity.Property(e => e.CantidadNueva).HasColumnName("cantidad_nueva");

                entity.Property(e => e.CantidadOriginal).HasColumnName("cantidad_original");

                entity.Property(e => e.CodigoAjuste)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_ajuste");

                entity.Property(e => e.CodigoBodega)
                    .HasMaxLength(150)
                    .HasColumnName("Codigo_Bodega")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.Enviado).HasColumnName("enviado");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.FechaVenceNueva)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence_nueva");

                entity.Property(e => e.FechaVenceOriginal)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence_original");

                entity.Property(e => e.IdBodegaErp).HasColumnName("IdBodegaERP");

                entity.Property(e => e.Idajustedet).HasColumnName("idajustedet");

                entity.Property(e => e.Idajusteenc).HasColumnName("idajusteenc");

                entity.Property(e => e.LoteNuevo)
                    .HasMaxLength(50)
                    .HasColumnName("lote_nuevo");

                entity.Property(e => e.LoteOriginal)
                    .HasMaxLength(50)
                    .HasColumnName("lote_original");

                entity.Property(e => e.ModificaCantidad).HasColumnName("modifica_cantidad");

                entity.Property(e => e.MotivoAjuste)
                    .HasMaxLength(50)
                    .HasColumnName("Motivo_Ajuste");

                entity.Property(e => e.NombreBodega)
                    .HasMaxLength(150)
                    .HasColumnName("Nombre_Bodega")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(200)
                    .HasColumnName("nombre_producto");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(50)
                    .HasColumnName("observacion");

                entity.Property(e => e.PesoNuevo).HasColumnName("peso_nuevo");

                entity.Property(e => e.PesoOriginal).HasColumnName("peso_original");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(50)
                    .HasColumnName("referencia");

                entity.Property(e => e.TipoAjuste)
                    .HasMaxLength(50)
                    .HasColumnName("Tipo_Ajuste");

                entity.Property(e => e.Umbas)
                    .HasMaxLength(50)
                    .HasColumnName("UMBas");
            });

            modelBuilder.Entity<VwBodega>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Bodega");

                entity.Property(e => e.Código).HasMaxLength(50);

                entity.Property(e => e.Empresa)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.NombreComercial).HasMaxLength(50);

                entity.Property(e => e.Pais)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Responsable).HasMaxLength(50);
            });

            modelBuilder.Entity<VwBodegaArea>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_BodegaArea");

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwBodegaMuelle>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_BodegaMuelle");

                entity.Property(e => e.Bodega)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Muelle)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwBodegaSector>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_BodegaSector");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Expr17).HasMaxLength(50);

                entity.Property(e => e.Expr4)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Expr5)
                    .HasMaxLength(25)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Expr6).HasColumnType("datetime");

                entity.Property(e => e.Expr7)
                    .HasMaxLength(25)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Expr8).HasColumnType("datetime");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.MargenDerecho).HasColumnName("margen_derecho");

                entity.Property(e => e.MargenInferior).HasColumnName("margen_inferior");

                entity.Property(e => e.MargenIzquierdo).HasColumnName("margen_izquierdo");

                entity.Property(e => e.MargenSuperior).HasColumnName("margen_superior");

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwBodegaUbicacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_BodegaUbicacion");
            });

            modelBuilder.Entity<VwCalculoVencimiento>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_CalculoVencimientos");

                entity.Property(e => e.Barra).HasMaxLength(35);

                entity.Property(e => e.CalculoVencimientoDías).HasColumnName("CalculoVencimiento(Días)");

                entity.Property(e => e.CantidadSf).HasColumnName("CantidadSF");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaProyectada).HasColumnType("datetime");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NomEstado).HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UbicacionCompleta).HasMaxLength(90);

                entity.Property(e => e.UnidadMedida).HasMaxLength(50);
            });

            modelBuilder.Entity<VwCambiosUbicacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Cambios_Ubicacion");

                entity.Property(e => e.Código).HasMaxLength(50);

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Lp)
                    .HasMaxLength(50)
                    .HasColumnName("LP")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Motivo).HasMaxLength(50);

                entity.Property(e => e.Poliza).HasMaxLength(50);

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TipoTarea).HasMaxLength(50);

                entity.Property(e => e.UbicacionDestino)
                    .HasMaxLength(90)
                    .HasColumnName("Ubicacion_Destino");

                entity.Property(e => e.UbicacionOrigen)
                    .HasMaxLength(90)
                    .HasColumnName("Ubicacion_Origen");

                entity.Property(e => e.Umbas)
                    .HasMaxLength(50)
                    .HasColumnName("UMBas");

                entity.Property(e => e.Vence).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwCliente>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Cliente");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.ActivoBodega).HasColumnName("activo_bodega");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(150)
                    .HasColumnName("codigo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(150)
                    .HasColumnName("correo_electronico")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.DespacharLotesCompletos).HasColumnName("despachar_lotes_completos");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .HasColumnName("direccion")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Empresa)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EsBodegaRecepcion).HasColumnName("es_bodega_recepcion");

                entity.Property(e => e.EsBodegaTraslado).HasColumnName("es_bodega_traslado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Idubicacionvirtual).HasColumnName("idubicacionvirtual");

                entity.Property(e => e.Nit)
                    .HasMaxLength(125)
                    .HasColumnName("nit")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreComercial)
                    .HasMaxLength(150)
                    .HasColumnName("nombre_comercial")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreContacto)
                    .HasMaxLength(150)
                    .HasColumnName("nombre_contacto")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Propietario)
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.RealizaManufactura).HasColumnName("realiza_manufactura");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(25)
                    .HasColumnName("referencia");

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(125)
                    .HasColumnName("telefono")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TipoCliente)
                    .HasMaxLength(50)
                    .HasColumnName("Tipo Cliente")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwClienteDireccion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ClienteDireccion");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Avenida).HasMaxLength(50);

                entity.Property(e => e.Calle).HasMaxLength(50);

                entity.Property(e => e.CoordenadaX)
                    .HasMaxLength(50)
                    .HasColumnName("coordenada_x");

                entity.Property(e => e.CoordenadaY)
                    .HasMaxLength(50)
                    .HasColumnName("coordenada_y");

                entity.Property(e => e.Direccion).HasMaxLength(50);

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.NoCasa)
                    .HasMaxLength(50)
                    .HasColumnName("No_Casa");

                entity.Property(e => e.NombreMunicipio)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NombreRegion)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Referencia).HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Zona).HasMaxLength(50);
            });

            modelBuilder.Entity<VwClienteTipo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ClienteTipo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.NombreTipoCliente)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwCodigoBarraOc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_CodigoBarra_OC");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("Codigo_barra")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwComparativoNavWmsConCosto>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Comparativo_NAV_WMS_ConCostos");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.CostoConteo).HasColumnName("Costo_Conteo");

                entity.Property(e => e.CostoNav).HasColumnName("Costo_Nav");

                entity.Property(e => e.DifConteo).HasColumnName("Dif_Conteo");

                entity.Property(e => e.DifCosto).HasColumnName("Dif_Costo");

                entity.Property(e => e.DifErp).HasColumnName("Dif_ERP");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Vence");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.StockWms).HasColumnName("Stock_WMS");

                entity.Property(e => e.TeoricoErp).HasColumnName("Teorico_ERP");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Umbas)
                    .HasMaxLength(50)
                    .HasColumnName("UMBas");
            });

            modelBuilder.Entity<VwConfiguracioninv>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Configuracioninv");

                entity.Property(e => e.Bodega).HasMaxLength(50);

                entity.Property(e => e.Empresa)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.IdPropietario).HasColumnName("idPropietario");

                entity.Property(e => e.Idbodega).HasColumnName("idbodega");

                entity.Property(e => e.Idempresa).HasColumnName("idempresa");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Propietario)
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwCuadrilla>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Cuadrilla");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Bodega).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Tipo).HasMaxLength(50);
            });

            modelBuilder.Entity<VwDespacho>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Despacho");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Bodega).HasMaxLength(50);

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Piloto)
                    .IsRequired()
                    .HasMaxLength(301);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Ruta)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Vehiculo)
                    .IsRequired()
                    .HasMaxLength(156)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwDespachoDetalle>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Despacho_Detalle");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Presentacion)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Producto)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Ubicacion)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwDespachoRep>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Despacho_Rep");

                entity.Property(e => e.ApellidoPiloto)
                    .HasMaxLength(150)
                    .HasColumnName("Apellido_Piloto");

                entity.Property(e => e.CantidadPickeada).HasColumnName("cantidad_pickeada");

                entity.Property(e => e.CantidadVerificada).HasColumnName("cantidad_verificada");

                entity.Property(e => e.CodigoCliente)
                    .HasMaxLength(150)
                    .HasColumnName("Codigo_Cliente")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("Codigo_Producto");

                entity.Property(e => e.CodigoRuta)
                    .HasMaxLength(15)
                    .HasColumnName("Codigo_Ruta")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EsBodegaRecepcion).HasColumnName("es_bodega_recepcion");

                entity.Property(e => e.EsBodegaTraslado).HasColumnName("es_bodega_traslado");

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Idubicacionvirtual).HasColumnName("idubicacionvirtual");

                entity.Property(e => e.Lote)
                    .HasMaxLength(35)
                    .HasColumnName("lote");

                entity.Property(e => e.MarcaVehiculo)
                    .HasMaxLength(50)
                    .HasColumnName("Marca_Vehiculo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Marchamo)
                    .HasMaxLength(50)
                    .HasColumnName("marchamo");

                entity.Property(e => e.ModeloVehiculo)
                    .HasMaxLength(50)
                    .HasColumnName("Modelo_Vehiculo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NoCarnetPiloto)
                    .HasMaxLength(50)
                    .HasColumnName("No_Carnet_Piloto");

                entity.Property(e => e.NoDocumentoWms).HasColumnName("No_Documento_WMS");

                entity.Property(e => e.NoLicenciaPiloto)
                    .HasMaxLength(50)
                    .HasColumnName("No_Licencia_Piloto");

                entity.Property(e => e.NoPase).HasColumnName("no_pase");

                entity.Property(e => e.NoReferencia)
                    .HasMaxLength(25)
                    .HasColumnName("No_Referencia");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(150)
                    .HasColumnName("Nombre_Cliente")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombrePiloto)
                    .HasMaxLength(150)
                    .HasColumnName("Nombre_Piloto");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .HasColumnName("Nombre_Producto");

                entity.Property(e => e.NombreRuta)
                    .HasMaxLength(50)
                    .HasColumnName("Nombre_Ruta")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(500)
                    .HasColumnName("observacion");

                entity.Property(e => e.PesoPickeado).HasColumnName("Peso_Pickeado");

                entity.Property(e => e.PesoVerificado).HasColumnName("Peso_Verificado");

                entity.Property(e => e.PlacaComercial)
                    .HasMaxLength(50)
                    .HasColumnName("placa_comercial");

                entity.Property(e => e.PlacaVehiculo)
                    .HasMaxLength(20)
                    .HasColumnName("Placa_Vehiculo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UbicacionOrigen)
                    .HasMaxLength(50)
                    .HasColumnName("Ubicacion_Origen");

                entity.Property(e => e.Um)
                    .HasMaxLength(50)
                    .HasColumnName("UM");

                entity.Property(e => e.Vence).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwDespachoRepDet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Despacho_Rep_Det");

                entity.Property(e => e.ApellidoPiloto)
                    .HasMaxLength(150)
                    .HasColumnName("Apellido_Piloto");

                entity.Property(e => e.CantidadPickeada).HasColumnName("cantidad_pickeada");

                entity.Property(e => e.CantidadVerificada).HasColumnName("cantidad_verificada");

                entity.Property(e => e.CodigoCliente)
                    .HasMaxLength(150)
                    .HasColumnName("Codigo_Cliente")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("Codigo_Producto");

                entity.Property(e => e.CodigoRuta)
                    .HasMaxLength(15)
                    .HasColumnName("Codigo_Ruta")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EsBodegaRecepcion).HasColumnName("es_bodega_recepcion");

                entity.Property(e => e.EsBodegaTraslado).HasColumnName("es_bodega_traslado");

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.FechaDespacho)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Despacho");

                entity.Property(e => e.FechaPedido)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Pedido");

                entity.Property(e => e.Idubicacionvirtual).HasColumnName("idubicacionvirtual");

                entity.Property(e => e.Lote)
                    .HasMaxLength(35)
                    .HasColumnName("lote");

                entity.Property(e => e.MarcaVehiculo)
                    .HasMaxLength(50)
                    .HasColumnName("Marca_Vehiculo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Marchamo)
                    .HasMaxLength(50)
                    .HasColumnName("marchamo");

                entity.Property(e => e.ModeloVehiculo)
                    .HasMaxLength(50)
                    .HasColumnName("Modelo_Vehiculo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NoCarnetPiloto)
                    .HasMaxLength(50)
                    .HasColumnName("No_Carnet_Piloto");

                entity.Property(e => e.NoDocumentoWms).HasColumnName("No_Documento_WMS");

                entity.Property(e => e.NoLicenciaPiloto)
                    .HasMaxLength(50)
                    .HasColumnName("No_Licencia_Piloto");

                entity.Property(e => e.NoPase).HasColumnName("no_pase");

                entity.Property(e => e.NoReferencia)
                    .HasMaxLength(25)
                    .HasColumnName("No_Referencia");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(150)
                    .HasColumnName("Nombre_Cliente")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombrePiloto)
                    .HasMaxLength(150)
                    .HasColumnName("Nombre_Piloto");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .HasColumnName("Nombre_Producto");

                entity.Property(e => e.NombreRuta)
                    .HasMaxLength(50)
                    .HasColumnName("Nombre_Ruta")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(500)
                    .HasColumnName("observacion");

                entity.Property(e => e.PesoPickeado).HasColumnName("Peso_Pickeado");

                entity.Property(e => e.PesoVerificado).HasColumnName("Peso_Verificado");

                entity.Property(e => e.PlacaComercial)
                    .HasMaxLength(50)
                    .HasColumnName("placa_comercial");

                entity.Property(e => e.PlacaVehiculo)
                    .HasMaxLength(20)
                    .HasColumnName("Placa_Vehiculo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UbicacionOrigen)
                    .HasMaxLength(50)
                    .HasColumnName("Ubicacion_Origen");

                entity.Property(e => e.Um)
                    .HasMaxLength(50)
                    .HasColumnName("UM");

                entity.Property(e => e.Vence).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwDespachoRepDetI>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Despacho_Rep_Det_I");

                entity.Property(e => e.ApellidoPiloto)
                    .HasMaxLength(150)
                    .HasColumnName("Apellido_Piloto");

                entity.Property(e => e.CantidadPickeada).HasColumnName("cantidad_pickeada");

                entity.Property(e => e.CantidadVerificada).HasColumnName("cantidad_verificada");

                entity.Property(e => e.CodigoCliente)
                    .HasMaxLength(150)
                    .HasColumnName("Codigo_Cliente")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodigoPoliza)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_poliza");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("Codigo_Producto");

                entity.Property(e => e.CodigoRuta)
                    .HasMaxLength(15)
                    .HasColumnName("Codigo_Ruta")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EsBodegaRecepcion).HasColumnName("es_bodega_recepcion");

                entity.Property(e => e.EsBodegaTraslado).HasColumnName("es_bodega_traslado");

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Idubicacionvirtual).HasColumnName("idubicacionvirtual");

                entity.Property(e => e.Lote)
                    .HasMaxLength(35)
                    .HasColumnName("lote");

                entity.Property(e => e.MarcaVehiculo)
                    .HasMaxLength(50)
                    .HasColumnName("Marca_Vehiculo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Marchamo)
                    .HasMaxLength(50)
                    .HasColumnName("marchamo");

                entity.Property(e => e.ModeloVehiculo)
                    .HasMaxLength(50)
                    .HasColumnName("Modelo_Vehiculo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NoCarnetPiloto)
                    .HasMaxLength(50)
                    .HasColumnName("No_Carnet_Piloto");

                entity.Property(e => e.NoDocumentoWms).HasColumnName("No_Documento_WMS");

                entity.Property(e => e.NoLicenciaPiloto)
                    .HasMaxLength(50)
                    .HasColumnName("No_Licencia_Piloto");

                entity.Property(e => e.NoPase).HasColumnName("no_pase");

                entity.Property(e => e.NoReferencia)
                    .HasMaxLength(25)
                    .HasColumnName("No_Referencia");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(150)
                    .HasColumnName("Nombre_Cliente")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreEstado).HasMaxLength(50);

                entity.Property(e => e.NombrePiloto)
                    .HasMaxLength(150)
                    .HasColumnName("Nombre_Piloto");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .HasColumnName("Nombre_Producto");

                entity.Property(e => e.NombreRuta)
                    .HasMaxLength(50)
                    .HasColumnName("Nombre_Ruta")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(500)
                    .HasColumnName("observacion");

                entity.Property(e => e.PesoPickeado).HasColumnName("Peso_Pickeado");

                entity.Property(e => e.PesoVerificado).HasColumnName("Peso_Verificado");

                entity.Property(e => e.PlacaComercial)
                    .HasMaxLength(50)
                    .HasColumnName("placa_comercial");

                entity.Property(e => e.PlacaVehiculo)
                    .HasMaxLength(20)
                    .HasColumnName("Placa_Vehiculo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UbicacionOrigen).HasColumnName("Ubicacion_Origen");

                entity.Property(e => e.Um)
                    .HasMaxLength(50)
                    .HasColumnName("UM");

                entity.Property(e => e.Vence).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwDespachoRepRe>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Despacho_Rep_Res");

                entity.Property(e => e.ApellidoPiloto)
                    .HasMaxLength(150)
                    .HasColumnName("Apellido_Piloto");

                entity.Property(e => e.CantidadPickeada).HasColumnName("cantidad_pickeada");

                entity.Property(e => e.CantidadVerificada).HasColumnName("cantidad_verificada");

                entity.Property(e => e.CodigoCliente)
                    .HasMaxLength(150)
                    .HasColumnName("Codigo_Cliente")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("Codigo_Producto");

                entity.Property(e => e.CodigoRuta)
                    .HasMaxLength(15)
                    .HasColumnName("Codigo_Ruta")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EsBodegaRecepcion).HasColumnName("es_bodega_recepcion");

                entity.Property(e => e.EsBodegaTraslado).HasColumnName("es_bodega_traslado");

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.FechaDespacho)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Despacho");

                entity.Property(e => e.Idubicacionvirtual).HasColumnName("idubicacionvirtual");

                entity.Property(e => e.Lote)
                    .HasMaxLength(35)
                    .HasColumnName("lote");

                entity.Property(e => e.MarcaVehiculo)
                    .HasMaxLength(50)
                    .HasColumnName("Marca_Vehiculo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Marchamo)
                    .HasMaxLength(50)
                    .HasColumnName("marchamo");

                entity.Property(e => e.ModeloVehiculo)
                    .HasMaxLength(50)
                    .HasColumnName("Modelo_Vehiculo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NoCarnetPiloto)
                    .HasMaxLength(50)
                    .HasColumnName("No_Carnet_Piloto");

                entity.Property(e => e.NoDocumentoWms).HasColumnName("No_Documento_WMS");

                entity.Property(e => e.NoLicenciaPiloto)
                    .HasMaxLength(50)
                    .HasColumnName("No_Licencia_Piloto");

                entity.Property(e => e.NoPase).HasColumnName("no_pase");

                entity.Property(e => e.NoReferencia)
                    .HasMaxLength(25)
                    .HasColumnName("No_Referencia");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(150)
                    .HasColumnName("Nombre_Cliente")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombrePiloto)
                    .HasMaxLength(150)
                    .HasColumnName("Nombre_Piloto");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .HasColumnName("Nombre_Producto");

                entity.Property(e => e.NombreRuta)
                    .HasMaxLength(50)
                    .HasColumnName("Nombre_Ruta")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(500)
                    .HasColumnName("observacion");

                entity.Property(e => e.PesoPickeado).HasColumnName("Peso_Pickeado");

                entity.Property(e => e.PesoVerificado).HasColumnName("Peso_Verificado");

                entity.Property(e => e.PlacaComercial)
                    .HasMaxLength(50)
                    .HasColumnName("placa_comercial");

                entity.Property(e => e.PlacaVehiculo)
                    .HasMaxLength(20)
                    .HasColumnName("Placa_Vehiculo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Um)
                    .HasMaxLength(50)
                    .HasColumnName("UM");

                entity.Property(e => e.Vence).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwDocConDiferencia>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Doc_Con_Diferencias");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Bodega)
                    .HasMaxLength(50)
                    .HasColumnName("BODEGA");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CantidadRecibida).HasColumnName("cantidad_recibida");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.Diferencia).HasColumnName("DIFERENCIA");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Creacion");

                entity.Property(e => e.IdTipoIngresoOc).HasColumnName("IdTipoIngresoOC");

                entity.Property(e => e.NoDocumento)
                    .HasMaxLength(30)
                    .HasColumnName("No_Documento");

                entity.Property(e => e.NombreIngresooc)
                    .HasMaxLength(50)
                    .HasColumnName("NOMBRE_INGRESOOC");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .HasColumnName("nombre_producto");

                entity.Property(e => e.Ordencompra)
                    .HasMaxLength(30)
                    .HasColumnName("ORDENCOMPRA");

                entity.Property(e => e.Poliza).HasMaxLength(50);

                entity.Property(e => e.Presentacion)
                    .HasMaxLength(50)
                    .HasColumnName("PRESENTACION");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("PROPIETARIO")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Umbas)
                    .HasMaxLength(50)
                    .HasColumnName("UMBas");
            });

            modelBuilder.Entity<VwEstadoEnviosNav>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Estado_Envios_Nav");

                entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("FECHA");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("TIPO");
            });

            modelBuilder.Entity<VwExistenciaValoresFiscale>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Existencia_Valores_Fiscales");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.BarraProd).HasMaxLength(35);

                entity.Property(e => e.Bodega).HasMaxLength(50);

                entity.Property(e => e.CodigoProd).HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado");

                entity.Property(e => e.EstadoProd).HasMaxLength(50);

                entity.Property(e => e.ExistenciaActualPres).HasColumnName("Existencia_Actual_Pres");

                entity.Property(e => e.ExistenciaActualUmbas).HasColumnName("Existencia_Actual_UMBas");

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.FechaAgrego)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Agrego");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.Muelle)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NoDocumentoOc)
                    .HasMaxLength(30)
                    .HasColumnName("No_DocumentoOC");

                entity.Property(e => e.NoDocumentoRec)
                    .HasMaxLength(50)
                    .HasColumnName("No_DocumentoRec");

                entity.Property(e => e.NoPoliza).HasMaxLength(50);

                entity.Property(e => e.NombreProd).HasMaxLength(100);

                entity.Property(e => e.PesoNeto).HasColumnName("peso_neto");

                entity.Property(e => e.PresProd).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Proveedor)
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ReferenciaOc)
                    .HasMaxLength(100)
                    .HasColumnName("ReferenciaOC");

                entity.Property(e => e.TipoTrans).HasMaxLength(25);

                entity.Property(e => e.UbicacionOrigen)
                    .HasMaxLength(200)
                    .HasColumnName("Ubicacion_Origen");

                entity.Property(e => e.Um)
                    .HasMaxLength(50)
                    .HasColumnName("UM");

                entity.Property(e => e.ValorAduana).HasColumnName("valor_aduana");

                entity.Property(e => e.ValorDai).HasColumnName("valor_dai");

                entity.Property(e => e.ValorFlete).HasColumnName("valor_flete");

                entity.Property(e => e.ValorFob).HasColumnName("valor_fob");

                entity.Property(e => e.ValorIva).HasColumnName("valor_iva");

                entity.Property(e => e.ValorSeguro).HasColumnName("valor_seguro");

                entity.Property(e => e.Vence).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwExistenciasPorNoDocumento>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ExistenciasPorNoDocumento");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.BarraProd).HasMaxLength(35);

                entity.Property(e => e.Bodega).HasMaxLength(50);

                entity.Property(e => e.CodigoProd).HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado");

                entity.Property(e => e.EstadoProd).HasMaxLength(50);

                entity.Property(e => e.ExistenciaActualPres).HasColumnName("Existencia_Actual_Pres");

                entity.Property(e => e.ExistenciaActualUmbas).HasColumnName("Existencia_Actual_UMBas");

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.FechaAgrego)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Agrego");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.Muelle)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NoDocumentoOc)
                    .HasMaxLength(30)
                    .HasColumnName("No_DocumentoOC");

                entity.Property(e => e.NoDocumentoRec)
                    .HasMaxLength(50)
                    .HasColumnName("No_DocumentoRec");

                entity.Property(e => e.NombreProd).HasMaxLength(100);

                entity.Property(e => e.Poliza).HasMaxLength(50);

                entity.Property(e => e.PresProd).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Proveedor)
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ReferenciaOc)
                    .HasMaxLength(100)
                    .HasColumnName("ReferenciaOC");

                entity.Property(e => e.TipoTrans).HasMaxLength(25);

                entity.Property(e => e.UbicacionOrigen)
                    .HasMaxLength(200)
                    .HasColumnName("Ubicacion_Origen");

                entity.Property(e => e.Um)
                    .HasMaxLength(50)
                    .HasColumnName("UM");

                entity.Property(e => e.UsuarioAgrego)
                    .HasMaxLength(100)
                    .HasColumnName("Usuario_Agrego");

                entity.Property(e => e.Vence).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwExistenciasProductoCategorium>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_existencias_producto_categoria");

                entity.Property(e => e.Clasificacion)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ExistenciaMax).HasColumnName("existencia_max");

                entity.Property(e => e.ExistenciaMin).HasColumnName("existencia_min");

                entity.Property(e => e.Producto)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwFiscalCtasOrden>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Fiscal_CtasOrden");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CantidadPresentacion).HasColumnName("cantidad_presentacion");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.CodigoPoliza)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("codigo_poliza");

                entity.Property(e => e.CodigoProducto)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_recepcion");

                entity.Property(e => e.Idempresa).HasColumnName("idempresa");

                entity.Property(e => e.Idordencompra).HasColumnName("idordencompra");

                entity.Property(e => e.Idpropietario).HasColumnName("idpropietario");

                entity.Property(e => e.Idpropietariobodega).HasColumnName("idpropietariobodega");

                entity.Property(e => e.NombreComercial)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombre_comercial")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TipoTransaccion)
                    .HasMaxLength(50)
                    .HasColumnName("tipo_transaccion");

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .HasColumnName("unidad_medida");

                entity.Property(e => e.ValorAduana).HasColumnName("valor_aduana");

                entity.Property(e => e.ValorDai).HasColumnName("valor_dai");

                entity.Property(e => e.ValorIva).HasColumnName("valor_iva");
            });

            modelBuilder.Entity<VwFiscalHistorico>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Fiscal_historico");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Cif).HasColumnName("CIF");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(100)
                    .HasColumnName("CLIENTE")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodigoPoliza)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CODIGO_POLIZA");

                entity.Property(e => e.Dai).HasColumnName("DAI");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("FECHA");

                entity.Property(e => e.IdTipoIngresoOc).HasColumnName("IdTipoIngresoOC");

                entity.Property(e => e.Iva).HasColumnName("IVA");

                entity.Property(e => e.Licencia)
                    .HasMaxLength(50)
                    .HasColumnName("LICENCIA");

                entity.Property(e => e.MaterialName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("MATERIAL_NAME")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NumeroOrden)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("NUMERO_ORDEN");

                entity.Property(e => e.Regimen)
                    .HasMaxLength(50)
                    .HasColumnName("REGIMEN");

                entity.Property(e => e.ShortName)
                    .HasMaxLength(100)
                    .HasColumnName("SHORT_NAME");

                entity.Property(e => e.TotalValor).HasColumnName("TOTAL_VALOR");
            });

            modelBuilder.Entity<VwFiscalMercaVencidum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Fiscal_Merca_Vencida");

                entity.Property(e => e.CantidadPresentacion).HasColumnName("cantidad_presentacion");

                entity.Property(e => e.CantidadReservada).HasColumnName("cantidad_reservada");

                entity.Property(e => e.CantidadUmbas).HasColumnName("cantidad_umbas");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.CodigoRegimen)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("codigo_regimen");

                entity.Property(e => e.DiasRegimen).HasColumnName("dias_regimen");

                entity.Property(e => e.DiasVida).HasColumnName("dias_vida");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Creacion");

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Recepcion");

                entity.Property(e => e.FechaVencimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vencimiento");

                entity.Property(e => e.Material)
                    .HasMaxLength(100)
                    .HasColumnName("material");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.NombreCliente)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombre_cliente")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NumeroOrden)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("numero_orden");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Poliza)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Presentacion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UnidadPeso)
                    .HasMaxLength(50)
                    .HasColumnName("unidad_peso");
            });

            modelBuilder.Entity<VwGetAllStockDetalleResuman>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Get_All_Stock_Detalle_Resumen");

                entity.Property(e => e.Barra).HasMaxLength(35);

                entity.Property(e => e.CantPresentacion).HasColumnName("Cant_Presentacion");

                entity.Property(e => e.CantUmbas).HasColumnName("Cant_UMBas");

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Ingreso");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Vence");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.Presentación).HasMaxLength(50);

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.Tramo).HasMaxLength(50);

                entity.Property(e => e.Ubicacion).HasMaxLength(50);

                entity.Property(e => e.Umbas)
                    .HasMaxLength(50)
                    .HasColumnName("UMBas");
            });

            modelBuilder.Entity<VwGetDetalleByIdRecepcionEnc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Get_Detalle_By_IdRecepcionEnc");

                entity.Property(e => e.AtributoVariante1)
                    .HasMaxLength(25)
                    .HasColumnName("atributo_variante_1");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.CantidadRecibida).HasColumnName("cantidad_recibida");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.ControlPeso).HasColumnName("control_peso");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.CostoEstadistico).HasColumnName("costo_estadistico");

                entity.Property(e => e.CostoOc).HasColumnName("costo_oc");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoLinea).HasColumnName("No_Linea");

                entity.Property(e => e.NombrePresentacion)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_presentacion");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .HasColumnName("nombre_producto");

                entity.Property(e => e.NombreProductoEstado)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_producto_estado");

                entity.Property(e => e.NombreUnidadMedida)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_unidad_medida");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(150)
                    .HasColumnName("observacion");

                entity.Property(e => e.PalletNoEstandar).HasColumnName("pallet_no_estandar");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.PesoEstadistico).HasColumnName("peso_estadistico");

                entity.Property(e => e.PesoMaximo).HasColumnName("peso_maximo");

                entity.Property(e => e.PesoMinimo).HasColumnName("peso_minimo");

                entity.Property(e => e.PesoUnitario).HasColumnName("peso_unitario");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");
            });

            modelBuilder.Entity<VwGetSinglePedido>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Get_Single_Pedido");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Anulado).HasColumnName("anulado");

                entity.Property(e => e.CodigoCliente)
                    .HasMaxLength(150)
                    .HasColumnName("Codigo_Cliente")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ControlUltimoLote).HasColumnName("control_ultimo_lote");

                entity.Property(e => e.ControlUltimoLoteCliente).HasColumnName("control_ultimo_lote_cliente");

                entity.Property(e => e.Correlativo).HasColumnName("correlativo");

                entity.Property(e => e.DiasCliente).HasColumnName("dias_cliente");

                entity.Property(e => e.EnviadoAErp).HasColumnName("Enviado_A_ERP");

                entity.Property(e => e.EsBodegaRecepcion).HasColumnName("es_bodega_recepcion");

                entity.Property(e => e.EsBodegaTraslado).HasColumnName("es_bodega_traslado");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaPedido)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Pedido");

                entity.Property(e => e.HoraEntregaDesde).HasColumnType("datetime");

                entity.Property(e => e.HoraEntregaHasta).HasColumnType("datetime");

                entity.Property(e => e.HoraFin)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin");

                entity.Property(e => e.HoraIni)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini");

                entity.Property(e => e.Idubicacionvirtual).HasColumnName("idubicacionvirtual");

                entity.Property(e => e.Local).HasColumnName("local");

                entity.Property(e => e.NoDespacho).HasColumnName("no_despacho");

                entity.Property(e => e.NoDocumento).HasColumnName("no_documento");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(150)
                    .HasColumnName("Nombre_Cliente")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombrePropietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Nombre_Propietario")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreTipoPedido)
                    .HasMaxLength(50)
                    .HasColumnName("Nombre_Tipo_Pedido");

                entity.Property(e => e.PalletPrimero).HasColumnName("pallet_primero");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(25)
                    .HasColumnName("referencia");

                entity.Property(e => e.ReferenciaDocumentoIngresoBodegaDestino)
                    .HasMaxLength(50)
                    .HasColumnName("Referencia_Documento_Ingreso_Bodega_Destino");

                entity.Property(e => e.RoadAdd1)
                    .HasMaxLength(5)
                    .HasColumnName("RoadADD1");

                entity.Property(e => e.RoadAdd2)
                    .HasMaxLength(5)
                    .HasColumnName("RoadADD2");

                entity.Property(e => e.RoadAdd3)
                    .HasMaxLength(35)
                    .HasColumnName("RoadADD3");

                entity.Property(e => e.RoadBandera).HasMaxLength(5);

                entity.Property(e => e.RoadCalcoBj)
                    .HasMaxLength(1)
                    .HasColumnName("RoadCalcoBJ");

                entity.Property(e => e.RoadDirEntrega).HasMaxLength(255);

                entity.Property(e => e.RoadFechaEntr).HasColumnType("datetime");

                entity.Property(e => e.RoadRazonRechazado)
                    .HasMaxLength(50)
                    .HasColumnName("RoadRazon_Rechazado");

                entity.Property(e => e.RoadStatCom).HasMaxLength(1);

                entity.Property(e => e.RoadStatProc).HasMaxLength(3);

                entity.Property(e => e.RoadSucursal).HasMaxLength(10);

                entity.Property(e => e.Serie)
                    .HasMaxLength(25)
                    .HasColumnName("serie");

                entity.Property(e => e.SyncMi3).HasColumnName("sync_mi3");

                entity.Property(e => e.Ubicacion)
                    .HasMaxLength(35)
                    .HasColumnName("ubicacion");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(30)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwHorarioLaboral>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_HorarioLaboral");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Dia).HasColumnName("dia");

                entity.Property(e => e.Día)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.HoraFin)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("Hora Fin");

                entity.Property(e => e.HoraInicio)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("Hora Inicio");
            });

            modelBuilder.Entity<VwHorarioLaboralDet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_HorarioLaboralDet");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Dia).HasColumnName("dia");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaBaja)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_baja");

                entity.Property(e => e.HoraFin)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin");

                entity.Property(e => e.HoraInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_inicio");

                entity.Property(e => e.HorasExtras).HasColumnName("horas_extras");

                entity.Property(e => e.MaximoMinHoraIngreso).HasColumnName("maximo_min_hora_ingreso");

                entity.Property(e => e.MaximoMinHoraSalida).HasColumnName("maximo_min_hora_salida");

                entity.Property(e => e.MinimoMinHoraIngreso).HasColumnName("minimo_min_hora_ingreso");

                entity.Property(e => e.MinimoMinHoraSalida).HasColumnName("minimo_min_hora_salida");

                entity.Property(e => e.NhoraFin)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasColumnName("Nhora_fin");

                entity.Property(e => e.NhoraInicio)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasColumnName("Nhora_inicio");

                entity.Property(e => e.NombreDia)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("nombreDia");

                entity.Property(e => e.TiempoRetrasoPermitido).HasColumnName("tiempo_retraso_permitido");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwHorarioLaboralEnc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_HorarioLaboralEnc");

                entity.Property(e => e.Jornada).HasMaxLength(50);

                entity.Property(e => e.Turno).HasMaxLength(50);
            });

            modelBuilder.Entity<VwINavAcuerdo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_i_nav_acuerdos");

                entity.Property(e => e.CodigoAcuerdo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codigo_acuerdo");

                entity.Property(e => e.CodigoCliente)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codigo_cliente");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("descripcion");

                entity.Property(e => e.NombreCliente)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasColumnName("nombre_cliente");
            });

            modelBuilder.Entity<VwImpresionPallet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Impresion_Pallet");

                entity.Property(e => e.Bodega)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Empresa)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FechaPc)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_PC");

                entity.Property(e => e.Imprimio).HasMaxLength(201);

                entity.Property(e => e.Lp)
                    .HasMaxLength(35)
                    .HasColumnName("LP");

                entity.Property(e => e.Observacion).HasColumnType("text");

                entity.Property(e => e.Pc)
                    .HasMaxLength(25)
                    .HasColumnName("PC");

                entity.Property(e => e.ProductoCantidad).HasColumnName("Producto_Cantidad");

                entity.Property(e => e.ProductoCodigo)
                    .HasMaxLength(50)
                    .HasColumnName("Producto_Codigo");

                entity.Property(e => e.ProductoEstado)
                    .HasMaxLength(50)
                    .HasColumnName("Producto_Estado");

                entity.Property(e => e.ProductoLote)
                    .HasMaxLength(50)
                    .HasColumnName("Producto_Lote");

                entity.Property(e => e.ProductoNombreLargo)
                    .HasMaxLength(100)
                    .HasColumnName("Producto_Nombre_Largo");

                entity.Property(e => e.ProductoPresentacion)
                    .HasMaxLength(50)
                    .HasColumnName("Producto_Presentacion");

                entity.Property(e => e.ProductoUm)
                    .HasMaxLength(50)
                    .HasColumnName("Producto_UM");

                entity.Property(e => e.ProductoVence)
                    .HasColumnType("datetime")
                    .HasColumnName("Producto_Vence");

                entity.Property(e => e.PropietarioNombre)
                    .HasMaxLength(100)
                    .HasColumnName("Propietario_Nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ProveedorCodigo)
                    .HasMaxLength(50)
                    .HasColumnName("Proveedor_Codigo");

                entity.Property(e => e.ProveedorDir)
                    .HasMaxLength(250)
                    .HasColumnName("Proveedor_Dir")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ProveedorNombre)
                    .HasMaxLength(50)
                    .HasColumnName("Proveedor_Nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ProveedorTel)
                    .HasMaxLength(50)
                    .HasColumnName("Proveedor_Tel");

                entity.Property(e => e.RecNo).HasColumnName("Rec_No");

                entity.Property(e => e.RefPc)
                    .HasMaxLength(50)
                    .HasColumnName("Ref_PC");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");
            });

            modelBuilder.Entity<VwImpresionPalletRec>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Impresion_Pallet_Rec");

                entity.Property(e => e.Bodega)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Empresa)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FechaManufactura)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_manufactura");

                entity.Property(e => e.FechaPc)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_PC");

                entity.Property(e => e.Impreso).HasColumnName("impreso");

                entity.Property(e => e.Imprimio).HasMaxLength(201);

                entity.Property(e => e.LicPlate)
                    .IsUnicode(false)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lp)
                    .IsUnicode(false)
                    .HasColumnName("LP");

                entity.Property(e => e.Observacion).HasColumnType("text");

                entity.Property(e => e.Pc)
                    .HasMaxLength(25)
                    .HasColumnName("PC");

                entity.Property(e => e.ProductoCantidad).HasColumnName("Producto_Cantidad");

                entity.Property(e => e.ProductoCodigo)
                    .HasMaxLength(50)
                    .HasColumnName("Producto_Codigo");

                entity.Property(e => e.ProductoEstado)
                    .HasMaxLength(50)
                    .HasColumnName("Producto_Estado");

                entity.Property(e => e.ProductoLote)
                    .HasMaxLength(50)
                    .HasColumnName("Producto_Lote");

                entity.Property(e => e.ProductoNombreLargo)
                    .HasMaxLength(100)
                    .HasColumnName("Producto_Nombre_Largo");

                entity.Property(e => e.ProductoPeso).HasColumnName("Producto_Peso");

                entity.Property(e => e.ProductoPresentacion)
                    .HasMaxLength(50)
                    .HasColumnName("Producto_Presentacion");

                entity.Property(e => e.ProductoUm)
                    .HasMaxLength(50)
                    .HasColumnName("Producto_UM");

                entity.Property(e => e.ProductoVence)
                    .HasColumnType("datetime")
                    .HasColumnName("Producto_Vence");

                entity.Property(e => e.PropietarioNombre)
                    .HasMaxLength(100)
                    .HasColumnName("Propietario_Nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ProveedorCodigo)
                    .HasMaxLength(50)
                    .HasColumnName("Proveedor_Codigo");

                entity.Property(e => e.ProveedorDir)
                    .HasMaxLength(250)
                    .HasColumnName("Proveedor_Dir")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ProveedorNombre)
                    .HasMaxLength(50)
                    .HasColumnName("Proveedor_Nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ProveedorTel)
                    .HasMaxLength(50)
                    .HasColumnName("Proveedor_Tel");

                entity.Property(e => e.RecNo).HasColumnName("Rec_No");

                entity.Property(e => e.RefPc)
                    .HasMaxLength(50)
                    .HasColumnName("Ref_PC");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");
            });

            modelBuilder.Entity<VwInvConteoOperador>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Inv_Conteo_Operador");

                entity.Property(e => e.Bodega).HasMaxLength(50);

                entity.Property(e => e.CantidadConteo).HasColumnName("Cantidad_Conteo");

                entity.Property(e => e.CantidadStock).HasColumnName("Cantidad_Stock");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.EstadoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("Estado_Producto");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Ingreso");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.Idinventarioenc).HasColumnName("idinventarioenc");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.Operador)
                    .HasMaxLength(201)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.PesoConteo).HasColumnName("Peso_Conteo");

                entity.Property(e => e.PesoStock).HasColumnName("peso_stock");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Umbas)
                    .HasMaxLength(50)
                    .HasColumnName("UMBas");
            });

            modelBuilder.Entity<VwInventarioPrgPorTipo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Inventario_prg_por_tipo");

                entity.Property(e => e.Idinventarioenc).HasColumnName("idinventarioenc");

                entity.Property(e => e.NombreTipoProducto)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwMinimosMaximosPorPresentacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_MinimosMaximosPorPresentacion");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(50)
                    .HasColumnName("nombreProducto")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreProductoPresentacion)
                    .HasMaxLength(50)
                    .HasColumnName("nombreProductoPresentacion")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwMotivoAnulacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_MotivoAnulacion");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Empresa)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwMotivoAnulacionBodega>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_MotivoAnulacionBodega");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Empresa)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwMotivoDevolucion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_MotivoDevolucion");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Empresa)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EsDetalle).HasColumnName("es_detalle");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwMovimiento>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Movimientos");

                entity.Property(e => e.BarraPallet)
                    .HasMaxLength(50)
                    .HasColumnName("barra_pallet")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CantidadPresentacion).HasColumnName("Cantidad_Presentacion");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.Destino).HasMaxLength(50);

                entity.Property(e => e.EstadoDestino)
                    .HasMaxLength(50)
                    .HasColumnName("Estado Destino");

                entity.Property(e => e.EstadoOrigen)
                    .HasMaxLength(50)
                    .HasColumnName("Estado Origen");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Origen).HasMaxLength(50);

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Poliza).HasMaxLength(50);

                entity.Property(e => e.Presentación).HasMaxLength(50);

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TipoTarea)
                    .HasMaxLength(50)
                    .HasColumnName("Tipo Tarea");

                entity.Property(e => e.UnidadDeMedida)
                    .HasMaxLength(50)
                    .HasColumnName("Unidad de Medida");
            });

            modelBuilder.Entity<VwMovimientosDetalle>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_MovimientosDetalle");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.Destino).HasMaxLength(50);

                entity.Property(e => e.EstadoDestino)
                    .HasMaxLength(50)
                    .HasColumnName("Estado Destino");

                entity.Property(e => e.EstadoOrigen)
                    .HasMaxLength(50)
                    .HasColumnName("Estado Origen");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Origen).HasMaxLength(50);

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Poliza).HasMaxLength(50);

                entity.Property(e => e.Presentación).HasMaxLength(50);

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Propietario)
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TipoTarea)
                    .HasMaxLength(50)
                    .HasColumnName("Tipo Tarea");

                entity.Property(e => e.UnidadDeMedida)
                    .HasMaxLength(50)
                    .HasColumnName("Unidad de Medida");
            });

            modelBuilder.Entity<VwMovimientosN>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Movimientos_N");

                entity.Property(e => e.BarraPallet)
                    .HasMaxLength(50)
                    .HasColumnName("barra_pallet")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Clasificacion).HasMaxLength(50);

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra).HasMaxLength(35);

                entity.Property(e => e.CodigoBodegaDestino)
                    .HasMaxLength(50)
                    .HasColumnName("Codigo_Bodega_Destino");

                entity.Property(e => e.CodigoBodegaOrigen)
                    .HasMaxLength(50)
                    .HasColumnName("Codigo_Bodega_Origen");

                entity.Property(e => e.EstadoDestino).HasMaxLength(50);

                entity.Property(e => e.EstadoOrigen).HasMaxLength(50);

                entity.Property(e => e.Familia).HasMaxLength(50);

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreBodegaDestino)
                    .HasMaxLength(50)
                    .HasColumnName("Nombre_Bodega_Destino");

                entity.Property(e => e.NombreBodegaOrigen)
                    .HasMaxLength(50)
                    .HasColumnName("Nombre_Bodega_Origen");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Poliza).HasMaxLength(50);

                entity.Property(e => e.Presentación).HasMaxLength(50);

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TipoTarea).HasMaxLength(50);

                entity.Property(e => e.UbicDestino).HasMaxLength(50);

                entity.Property(e => e.UbicOrigen).HasMaxLength(50);

                entity.Property(e => e.Umbas)
                    .HasMaxLength(50)
                    .HasColumnName("UMBas");
            });

            modelBuilder.Entity<VwMovimientosN1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Movimientos_N1");

                entity.Property(e => e.BarraPallet)
                    .HasMaxLength(50)
                    .HasColumnName("barra_pallet")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra).HasMaxLength(35);

                entity.Property(e => e.CodigoBodega).HasMaxLength(50);

                entity.Property(e => e.EstadoDestino).HasMaxLength(50);

                entity.Property(e => e.EstadoOrigen).HasMaxLength(50);

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.Idmovimiento).HasColumnName("idmovimiento");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NoDocIngreso)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("No_Doc_Ingreso");

                entity.Property(e => e.NoDocSalida)
                    .HasMaxLength(50)
                    .HasColumnName("No_Doc_Salida");

                entity.Property(e => e.NoRefIngreso)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("No_Ref_Ingreso");

                entity.Property(e => e.NoRefSalida)
                    .HasMaxLength(50)
                    .HasColumnName("No_Ref_Salida");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Presentación).HasMaxLength(50);

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TipoTarea).HasMaxLength(50);

                entity.Property(e => e.UbicDestino).HasMaxLength(50);

                entity.Property(e => e.UbicOrigen).HasMaxLength(50);

                entity.Property(e => e.Umbas)
                    .HasMaxLength(50)
                    .HasColumnName("UMBas");
            });

            modelBuilder.Entity<VwNavdetalleconfiguracion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_navdetalleconfiguracion");

                entity.Property(e => e.Dia).HasColumnName("dia");

                entity.Property(e => e.Entidad)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Frecuencia).HasColumnName("frecuencia");

                entity.Property(e => e.Horafin)
                    .HasColumnType("datetime")
                    .HasColumnName("horafin");

                entity.Property(e => e.Horainicio)
                    .HasColumnType("datetime")
                    .HasColumnName("horainicio");

                entity.Property(e => e.Idnavconfigenc).HasColumnName("idnavconfigenc");

                entity.Property(e => e.NombreDia)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("nombreDia");
            });

            modelBuilder.Entity<VwOcupacionBodega>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_OcupacionBodega");
            });

            modelBuilder.Entity<VwOcupacionBodegaTramo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_OcupacionBodegaTramo");
            });

            modelBuilder.Entity<VwOperador>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Operador");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(100)
                    .HasColumnName("apellidos")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CostoHora).HasColumnName("costo_hora");

                entity.Property(e => e.Dirección)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Empresa)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Foto)
                    .HasColumnType("image")
                    .HasColumnName("foto");

                entity.Property(e => e.Horario).HasMaxLength(128);

                entity.Property(e => e.HorasJornada).HasColumnName("Horas_Jornada");

                entity.Property(e => e.NombreJornada)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_jornada");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(100)
                    .HasColumnName("nombres")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Pickea).HasColumnName("pickea");

                entity.Property(e => e.Recibe).HasColumnName("recibe");

                entity.Property(e => e.Teléfono)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Transporta).HasColumnName("transporta");

                entity.Property(e => e.Turno)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ubica).HasColumnName("ubica");

                entity.Property(e => e.UsaHh).HasColumnName("usa_hh");

                entity.Property(e => e.Verifica).HasColumnName("verifica");
            });

            modelBuilder.Entity<VwOperadorHorario>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Operador_Horario");

                entity.Property(e => e.Dia).HasColumnName("dia");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_fin");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_inicio");

                entity.Property(e => e.HoraFin)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin");

                entity.Property(e => e.HoraInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_inicio");

                entity.Property(e => e.HorasExtras).HasColumnName("horas_extras");

                entity.Property(e => e.MaximoMinHoraIngreso).HasColumnName("maximo_min_hora_ingreso");

                entity.Property(e => e.MaximoMinHoraSalida).HasColumnName("maximo_min_hora_salida");

                entity.Property(e => e.MinimoMinHoraIngreso).HasColumnName("minimo_min_hora_ingreso");

                entity.Property(e => e.MinimoMinHoraSalida).HasColumnName("minimo_min_hora_salida");

                entity.Property(e => e.TiempoRetrasoPermitido).HasColumnName("tiempo_retraso_permitido");
            });

            modelBuilder.Entity<VwOrdenCompra>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_OrdenCompra");

                entity.Property(e => e.Bodega).HasMaxLength(50);

                entity.Property(e => e.EnviadoAErp).HasColumnName("Enviado_A_ERP");

                entity.Property(e => e.EsDevolucion).HasColumnName("es_devolucion");

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.NoDocumento)
                    .HasMaxLength(30)
                    .HasColumnName("No. Documento");

                entity.Property(e => e.Procedencia).HasMaxLength(150);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Proveedor)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Referencia).HasMaxLength(100);

                entity.Property(e => e.TipoIngreso)
                    .HasMaxLength(50)
                    .HasColumnName("Tipo Ingreso");
            });

            modelBuilder.Entity<VwOrdenCompraPreIngreso>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_OrdenCompraPreIngreso");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Creacion");

                entity.Property(e => e.MotivoDevolucion).HasMaxLength(50);

                entity.Property(e => e.NoDocumento)
                    .HasMaxLength(30)
                    .HasColumnName("No_Documento");

                entity.Property(e => e.NoLinea).HasColumnName("No_Linea");

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Proveedor)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwPai>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Pais");

                entity.Property(e => e.NombreDepartamento)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.NombreMunicipio)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NombrePais)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreRegion)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwPaisDepartamento>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Pais_Departamento");

                entity.Property(e => e.NombreDepartamento)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.NombrePais)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwPaisRegion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Pais_Region");

                entity.Property(e => e.NombrePais)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreRegion)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwParametro>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Parametro");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Descripción)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ValorFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("Valor Fecha");

                entity.Property(e => e.ValorLógico).HasColumnName("Valor Lógico");

                entity.Property(e => e.ValorNúmerico).HasColumnName("Valor Númerico");

                entity.Property(e => e.ValorTexto)
                    .HasMaxLength(50)
                    .HasColumnName("Valor Texto")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwPeConDiferencia>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Pe_Con_Diferencias");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Bodega)
                    .HasMaxLength(50)
                    .HasColumnName("BODEGA");

                entity.Property(e => e.CantDespachada).HasColumnName("cant_despachada");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.Diferencia).HasColumnName("DIFERENCIA");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaPedido)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Pedido");

                entity.Property(e => e.NombrePedido)
                    .HasMaxLength(250)
                    .HasColumnName("NOMBRE_PEDIDO");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .HasColumnName("nombre_producto");

                entity.Property(e => e.Ordenpedido).HasColumnName("ORDENPEDIDO");

                entity.Property(e => e.Presentacion)
                    .HasMaxLength(50)
                    .HasColumnName("PRESENTACION");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("PROPIETARIO")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Umbas)
                    .HasMaxLength(50)
                    .HasColumnName("UMBas");
            });

            modelBuilder.Entity<VwPedido>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Pedido");

                entity.Property(e => e.CantidadDespachada).HasColumnName("Cantidad_Despachada");

                entity.Property(e => e.CantidadPickeada).HasColumnName("Cantidad_Pickeada");

                entity.Property(e => e.CantidadVerificada).HasColumnName("Cantidad_Verificada");

                entity.Property(e => e.Código).HasMaxLength(50);

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.EstadoPedido).HasMaxLength(20);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Referencia)
                    .HasMaxLength(25)
                    .HasColumnName("referencia");

                entity.Property(e => e.UnidadMedida).HasMaxLength(50);
            });

            modelBuilder.Entity<VwPedidosDm>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_PEDIDOS_DM");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(150)
                    .HasColumnName("codigo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.FechaPedido)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Pedido");
            });

            modelBuilder.Entity<VwPedidosList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Pedidos_List");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Bodega).HasMaxLength(50);

                entity.Property(e => e.Cliente)
                    .HasMaxLength(303)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EnviadoAErp).HasColumnName("Enviado_A_ERP");

                entity.Property(e => e.Estado).HasMaxLength(20);

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.FechaPedido)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Pedido");

                entity.Property(e => e.Muelle)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NoDocumento).HasColumnName("no_documento");

                entity.Property(e => e.Propietario)
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Referencia).HasMaxLength(25);

                entity.Property(e => e.RoadRuta)
                    .HasMaxLength(69)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.RoadVendedor).HasMaxLength(61);
            });

            modelBuilder.Entity<VwPicking>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Picking");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Bodega).HasMaxLength(50);

                entity.Property(e => e.DetalleOperador).HasColumnName("Detalle Operador");

                entity.Property(e => e.DuracionMinutos)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Duracion_Minutos");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaPicking)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha Picking");

                entity.Property(e => e.HoraFinal)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Hora Final")
                    .IsFixedLength(true);

                entity.Property(e => e.HoraInicial)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Hora Inicial")
                    .IsFixedLength(true);

                entity.Property(e => e.ProcesadoBof).HasColumnName("Procesado_Bof");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UbicaciónPicking)
                    .HasMaxLength(50)
                    .HasColumnName("Ubicación Picking");
            });

            modelBuilder.Entity<VwPickingDetByIdPickingEnc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Picking_Det_By_IdPickingEnc");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Bodega).HasMaxLength(50);

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CantidadRecibida).HasColumnName("cantidad_recibida");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(150)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ClienteDias).HasColumnName("cliente_dias");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaPedido)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Pedido");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreEstado)
                    .HasMaxLength(50)
                    .HasColumnName("Nombre_Estado");

                entity.Property(e => e.NombrePresentacion)
                    .HasMaxLength(50)
                    .HasColumnName("Nombre_Presentacion");

                entity.Property(e => e.NombreUnidadMedida)
                    .HasMaxLength(50)
                    .HasColumnName("Nombre_Unidad_Medida");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwPickingUbicByIdPedidoDet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_PickingUbic_By_IdPedidoDet");

                entity.Property(e => e.Acepto).HasColumnName("acepto");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CantidadDespachada).HasColumnName("cantidad_despachada");

                entity.Property(e => e.CantidadRecibida).HasColumnName("cantidad_recibida");

                entity.Property(e => e.CantidadSolicitada).HasColumnName("cantidad_solicitada");

                entity.Property(e => e.CantidadVerificada).HasColumnName("cantidad_verificada");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.DañadoPicking).HasColumnName("dañado_picking");

                entity.Property(e => e.DañadoVerificacion).HasColumnName("dañado_verificacion");

                entity.Property(e => e.Encontrado).HasColumnName("encontrado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaDespachado)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_despachado");

                entity.Property(e => e.FechaMinima)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_minima");

                entity.Property(e => e.FechaPacking)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_packing");

                entity.Property(e => e.FechaPicking)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_picking");

                entity.Property(e => e.FechaRealVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_real_vence");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.FechaVerificado)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_verificado");

                entity.Property(e => e.IdOperadorBodegaPickeo).HasColumnName("IdOperadorBodega_Pickeo");

                entity.Property(e => e.IdOperadorBodegaVerifico).HasColumnName("IdOperadorBodega_Verifico");

                entity.Property(e => e.IdUbicacionReemplazo).HasColumnName("IdUbicacion_reemplazo");

                entity.Property(e => e.IdstockReemplazo).HasColumnName("idstock_reemplazo");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(25)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.LicPlateReemplazo)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate_reemplazo");

                entity.Property(e => e.Lote)
                    .HasMaxLength(35)
                    .HasColumnName("lote");

                entity.Property(e => e.NoPacking)
                    .HasMaxLength(50)
                    .HasColumnName("no_packing");

                entity.Property(e => e.NomEstado)
                    .HasMaxLength(50)
                    .HasColumnName("nom_estado");

                entity.Property(e => e.NomPresentacion)
                    .HasMaxLength(50)
                    .HasColumnName("nom_presentacion");

                entity.Property(e => e.NomUnidMed)
                    .HasMaxLength(50)
                    .HasColumnName("nom_unid_med");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .HasColumnName("nombre_producto");

                entity.Property(e => e.NombreUbicacion)
                    .HasMaxLength(90)
                    .HasColumnName("Nombre_Ubicacion");

                entity.Property(e => e.PdetIdPresentacion).HasColumnName("PDetIdPresentacion");

                entity.Property(e => e.PdetIdProductoBodega).HasColumnName("PDetIdProductoBodega");

                entity.Property(e => e.PesoDespachado).HasColumnName("peso_despachado");

                entity.Property(e => e.PesoRecibido).HasColumnName("peso_recibido");

                entity.Property(e => e.PesoSolicitado).HasColumnName("peso_solicitado");

                entity.Property(e => e.PesoVerificado).HasColumnName("peso_verificado");

                entity.Property(e => e.Serial)
                    .HasMaxLength(35)
                    .HasColumnName("serial");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwPickingUbicByIdPickingDet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_PickingUbic_By_IdPickingDet");

                entity.Property(e => e.Acepto).HasColumnName("acepto");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CantidadDespachada).HasColumnName("cantidad_despachada");

                entity.Property(e => e.CantidadRecibida).HasColumnName("cantidad_recibida");

                entity.Property(e => e.CantidadSolicitada).HasColumnName("cantidad_solicitada");

                entity.Property(e => e.CantidadVerificada).HasColumnName("cantidad_verificada");

                entity.Property(e => e.DañadoPicking).HasColumnName("dañado_picking");

                entity.Property(e => e.DañadoVerificacion).HasColumnName("dañado_verificacion");

                entity.Property(e => e.Encontrado).HasColumnName("encontrado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaDespachado)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_despachado");

                entity.Property(e => e.FechaMinima)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_minima");

                entity.Property(e => e.FechaPacking)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_packing");

                entity.Property(e => e.FechaPicking)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_picking");

                entity.Property(e => e.FechaRealVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_real_vence");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.FechaVerificado)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_verificado");

                entity.Property(e => e.IdOperadorBodegaPickeo).HasColumnName("IdOperadorBodega_Pickeo");

                entity.Property(e => e.IdOperadorBodegaVerifico).HasColumnName("IdOperadorBodega_Verifico");

                entity.Property(e => e.IdStockReemplazo).HasColumnName("IdStock_reemplazo");

                entity.Property(e => e.IdUbicacionReemplazo).HasColumnName("IdUbicacion_reemplazo");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(25)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.LicPlateReemplazo)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate_reemplazo");

                entity.Property(e => e.Lote)
                    .HasMaxLength(35)
                    .HasColumnName("lote");

                entity.Property(e => e.NoPacking)
                    .HasMaxLength(50)
                    .HasColumnName("no_packing");

                entity.Property(e => e.NombreOperadorPickeo)
                    .HasMaxLength(100)
                    .HasColumnName("Nombre_Operador_Pickeo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreOperadorVerifico)
                    .HasMaxLength(100)
                    .HasColumnName("Nombre_Operador_Verifico")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.PesoDespachado).HasColumnName("peso_despachado");

                entity.Property(e => e.PesoRecibido).HasColumnName("peso_recibido");

                entity.Property(e => e.PesoSolicitado).HasColumnName("peso_solicitado");

                entity.Property(e => e.PesoVerificado).HasColumnName("peso_verificado");

                entity.Property(e => e.Serial)
                    .HasMaxLength(35)
                    .HasColumnName("serial");

                entity.Property(e => e.Ubicacion).HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwPickingUbicByIdPickingEnc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_PickingUbic_By_IdPickingEnc");

                entity.Property(e => e.Acepto).HasColumnName("acepto");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CantidadDespachada).HasColumnName("cantidad_despachada");

                entity.Property(e => e.CantidadRecibida).HasColumnName("cantidad_recibida");

                entity.Property(e => e.CantidadSolicitada).HasColumnName("cantidad_solicitada");

                entity.Property(e => e.CantidadVerificada).HasColumnName("cantidad_verificada");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.DañadoPicking).HasColumnName("dañado_picking");

                entity.Property(e => e.DañadoVerificacion).HasColumnName("dañado_verificacion");

                entity.Property(e => e.Encontrado).HasColumnName("encontrado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaDespachado)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_despachado");

                entity.Property(e => e.FechaMinima)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_minima");

                entity.Property(e => e.FechaPacking)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_packing");

                entity.Property(e => e.FechaPicking)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_picking");

                entity.Property(e => e.FechaRealVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_real_vence");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.FechaVerificado)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_verificado");

                entity.Property(e => e.IdOperadorBodegaPickeo).HasColumnName("IdOperadorBodega_Pickeo");

                entity.Property(e => e.IdOperadorBodegaVerifico).HasColumnName("IdOperadorBodega_Verifico");

                entity.Property(e => e.IdStockReemplazo).HasColumnName("IdStock_reemplazo");

                entity.Property(e => e.IdUbicacionReemplazo).HasColumnName("IdUbicacion_reemplazo");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(25)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.LicPlateReemplazo)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate_reemplazo");

                entity.Property(e => e.Lote)
                    .HasMaxLength(35)
                    .HasColumnName("lote");

                entity.Property(e => e.NoPacking)
                    .HasMaxLength(50)
                    .HasColumnName("no_packing");

                entity.Property(e => e.NomEstado)
                    .HasMaxLength(50)
                    .HasColumnName("nom_estado");

                entity.Property(e => e.NomPresentacion)
                    .HasMaxLength(50)
                    .HasColumnName("nom_presentacion");

                entity.Property(e => e.NomUnidMed)
                    .HasMaxLength(50)
                    .HasColumnName("nom_unid_med");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .HasColumnName("nombre_producto");

                entity.Property(e => e.NombreUbicacion).HasMaxLength(200);

                entity.Property(e => e.PesoDespachado).HasColumnName("peso_despachado");

                entity.Property(e => e.PesoRecibido).HasColumnName("peso_recibido");

                entity.Property(e => e.PesoSolicitado).HasColumnName("peso_solicitado");

                entity.Property(e => e.PesoVerificado).HasColumnName("peso_verificado");

                entity.Property(e => e.Serial)
                    .HasMaxLength(35)
                    .HasColumnName("serial");

                entity.Property(e => e.SrIdStock).HasColumnName("Sr_IdStock");

                entity.Property(e => e.SrIdStockRes).HasColumnName("Sr_IdStockRes");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwPickingUbicDespachadoByIdPedidoDet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_PickingUbic_Despachado_By_IdPedidoDet");

                entity.Property(e => e.Acepto).HasColumnName("acepto");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CantidadDespachada).HasColumnName("cantidad_despachada");

                entity.Property(e => e.CantidadRecibida).HasColumnName("cantidad_recibida");

                entity.Property(e => e.CantidadSolicitada).HasColumnName("cantidad_solicitada");

                entity.Property(e => e.CantidadVerificada).HasColumnName("cantidad_verificada");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.DañadoPicking).HasColumnName("dañado_picking");

                entity.Property(e => e.DañadoVerificacion).HasColumnName("dañado_verificacion");

                entity.Property(e => e.Encontrado).HasColumnName("encontrado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaDespachado)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_despachado");

                entity.Property(e => e.FechaMinima)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_minima");

                entity.Property(e => e.FechaPacking)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_packing");

                entity.Property(e => e.FechaPicking)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_picking");

                entity.Property(e => e.FechaRealVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_real_vence");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.FechaVerificado)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_verificado");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(25)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(35)
                    .HasColumnName("lote");

                entity.Property(e => e.NoPacking)
                    .HasMaxLength(50)
                    .HasColumnName("no_packing");

                entity.Property(e => e.NomEstado).HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreUbicacion)
                    .HasMaxLength(90)
                    .HasColumnName("Nombre_Ubicacion");

                entity.Property(e => e.PesoDespachado).HasColumnName("peso_despachado");

                entity.Property(e => e.PesoRecibido).HasColumnName("peso_recibido");

                entity.Property(e => e.PesoSolicitado).HasColumnName("peso_solicitado");

                entity.Property(e => e.PesoVerificado).HasColumnName("peso_verificado");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Serial)
                    .HasMaxLength(35)
                    .HasColumnName("serial");

                entity.Property(e => e.UnidadMedida).HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwPickingUbicacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_PickingUbicacion");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Código).HasMaxLength(50);

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.FechaPedido)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha Pedido");

                entity.Property(e => e.FechaPicking)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha Picking");

                entity.Property(e => e.IdPicking).HasColumnName("ID Picking");

                entity.Property(e => e.Presentación).HasMaxLength(50);

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Ubicación).HasMaxLength(50);

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .HasColumnName("Unidad Medida");

                entity.Property(e => e.Vence).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwPresentacionTarima>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Presentacion_Tarima");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TipoTarima).HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwProducto>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Producto");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CapturaArancel).HasColumnName("captura_arancel");

                entity.Property(e => e.CapturarAniada).HasColumnName("capturar_aniada");

                entity.Property(e => e.CicloVida).HasColumnName("ciclo_vida");

                entity.Property(e => e.Clasificación).HasMaxLength(50);

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.ControlLote).HasColumnName("control_lote");

                entity.Property(e => e.ControlPeso).HasColumnName("control_peso");

                entity.Property(e => e.ControlVencimiento).HasColumnName("control_vencimiento");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.Código).HasMaxLength(50);

                entity.Property(e => e.CódigoDeBarra)
                    .HasMaxLength(35)
                    .HasColumnName("Código de Barra");

                entity.Property(e => e.EsHardware).HasColumnName("es_hardware");

                entity.Property(e => e.ExistenciaMax).HasColumnName("existencia_max");

                entity.Property(e => e.ExistenciaMin).HasColumnName("existencia_min");

                entity.Property(e => e.ExistenciaMáxima).HasColumnName("Existencia Máxima");

                entity.Property(e => e.ExistenciaMínima).HasColumnName("Existencia Mínima");

                entity.Property(e => e.Familia).HasMaxLength(50);

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Fechamanufactura).HasColumnName("fechamanufactura");

                entity.Property(e => e.GeneraLote).HasColumnName("genera_lote");

                entity.Property(e => e.Imagen)
                    .HasColumnType("image")
                    .HasColumnName("imagen");

                entity.Property(e => e.Kit).HasColumnName("kit");

                entity.Property(e => e.Marca).HasMaxLength(50);

                entity.Property(e => e.MateriaPrima).HasColumnName("materia_prima");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Noparte)
                    .HasMaxLength(50)
                    .HasColumnName("noparte");

                entity.Property(e => e.Noserie)
                    .HasMaxLength(50)
                    .HasColumnName("noserie");

                entity.Property(e => e.PesoDespacho).HasColumnName("peso_despacho");

                entity.Property(e => e.PesoRecepcion).HasColumnName("peso_recepcion");

                entity.Property(e => e.PesoReferencia).HasColumnName("peso_referencia");

                entity.Property(e => e.PesoTolerancia).HasColumnName("peso_tolerancia");

                entity.Property(e => e.Precio).HasColumnName("precio");

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Propietario)
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Serializado).HasColumnName("serializado");

                entity.Property(e => e.TemperaturaDespacho).HasColumnName("temperatura_despacho");

                entity.Property(e => e.TemperaturaRecepcion).HasColumnName("temperatura_recepcion");

                entity.Property(e => e.TemperaturaReferencia).HasColumnName("temperatura_referencia");

                entity.Property(e => e.TemperaturaTolerancia).HasColumnName("temperatura_tolerancia");

                entity.Property(e => e.TipoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("Tipo Producto")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tolerancia).HasColumnName("tolerancia");

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .HasColumnName("Unidad Medida");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwProductoBodegaParametro>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ProductoBodegaParametro");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CapturarSiempre).HasColumnName("capturar_siempre");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .HasColumnName("tipo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.Property(e => e.ValorFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("valor_fecha");

                entity.Property(e => e.ValorLogico).HasColumnName("valor_logico");

                entity.Property(e => e.ValorNumerico).HasColumnName("valor_numerico");

                entity.Property(e => e.ValorTexto)
                    .HasMaxLength(50)
                    .HasColumnName("valor_texto");
            });

            modelBuilder.Entity<VwProductoClasificacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ProductoClasificacion");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwProductoDimension>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ProductoDimension");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Dimension).HasColumnName("dimension");

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Presentacion)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Umbas)
                    .HasMaxLength(50)
                    .HasColumnName("UMBas")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwProductoEstado>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ProductoEstado");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Utilizable).HasColumnName("utilizable");
            });

            modelBuilder.Entity<VwProductoEstadoUbic>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ProductoEstadoUbic");

                entity.Property(e => e.NombreUbic).HasMaxLength(200);
            });

            modelBuilder.Entity<VwProductoEstadoUbicBodega>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Producto_Estado_Ubic_Bodega");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CodigoBodegaErp)
                    .HasMaxLength(25)
                    .HasColumnName("codigo_bodega_erp");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreUbic).HasMaxLength(200);

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.Property(e => e.Utilizable).HasColumnName("utilizable");
            });

            modelBuilder.Entity<VwProductoEstadoUbicBodegaAnt20210215>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Producto_Estado_Ubic_Bodega_Ant_20210215");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CodigoBodegaErp)
                    .HasMaxLength(25)
                    .HasColumnName("codigo_bodega_erp");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreUbic).HasMaxLength(200);

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.Property(e => e.Utilizable).HasColumnName("utilizable");
            });

            modelBuilder.Entity<VwProductoEstadoUbicBodegaHh>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Producto_Estado_Ubic_Bodega_HH");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CodigoBodegaErp)
                    .HasMaxLength(25)
                    .HasColumnName("codigo_bodega_erp");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreUbic).HasMaxLength(200);

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.Property(e => e.Utilizable).HasColumnName("utilizable");
            });

            modelBuilder.Entity<VwProductoEstadoUbicDefecto>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ProductoEstadoUbicDefecto");

                entity.Property(e => e.NombreUbic).HasMaxLength(90);
            });

            modelBuilder.Entity<VwProductoEstadoUbicacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ProductoEstadoUbicacion");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Bodega).HasMaxLength(50);

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Ubicacion).HasMaxLength(156);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwProductoFamilium>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ProductoFamilia");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwProductoMarca>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ProductoMarca");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwProductoOc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ProductoOC");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Activoproductobodega).HasColumnName("activoproductobodega");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.CapturaArancel).HasColumnName("captura_arancel");

                entity.Property(e => e.CapturarAniada).HasColumnName("capturar_aniada");

                entity.Property(e => e.CicloVida).HasColumnName("ciclo_vida");

                entity.Property(e => e.Clasificación).HasMaxLength(50);

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.ControlLote).HasColumnName("control_lote");

                entity.Property(e => e.ControlPeso).HasColumnName("control_peso");

                entity.Property(e => e.ControlVencimiento).HasColumnName("control_vencimiento");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.EsHardware).HasColumnName("es_hardware");

                entity.Property(e => e.ExistenciaMax).HasColumnName("existencia_max");

                entity.Property(e => e.ExistenciaMin).HasColumnName("existencia_min");

                entity.Property(e => e.Familia).HasMaxLength(50);

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Fechamanufactura).HasColumnName("fechamanufactura");

                entity.Property(e => e.GeneraLote).HasColumnName("genera_lote");

                entity.Property(e => e.GeneraLpOld).HasColumnName("genera_lp_old");

                entity.Property(e => e.Kit).HasColumnName("kit");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.Marca).HasMaxLength(50);

                entity.Property(e => e.MateriaPrima).HasColumnName("materia_prima");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Noparte)
                    .HasMaxLength(50)
                    .HasColumnName("noparte");

                entity.Property(e => e.Noserie)
                    .HasMaxLength(50)
                    .HasColumnName("noserie");

                entity.Property(e => e.PesoDespacho).HasColumnName("peso_despacho");

                entity.Property(e => e.PesoRecepcion).HasColumnName("peso_recepcion");

                entity.Property(e => e.PesoReferencia).HasColumnName("peso_referencia");

                entity.Property(e => e.PesoTolerancia).HasColumnName("peso_tolerancia");

                entity.Property(e => e.Precio).HasColumnName("precio");

                entity.Property(e => e.Propietario)
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Serializado).HasColumnName("serializado");

                entity.Property(e => e.TemperaturaDespacho).HasColumnName("temperatura_despacho");

                entity.Property(e => e.TemperaturaRecepcion).HasColumnName("temperatura_recepcion");

                entity.Property(e => e.TemperaturaReferencia).HasColumnName("temperatura_referencia");

                entity.Property(e => e.TemperaturaTolerancia).HasColumnName("temperatura_tolerancia");

                entity.Property(e => e.TipoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("Tipo Producto")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tolerancia).HasColumnName("tolerancia");

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .HasColumnName("Unidad Medida");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwProductoParametro>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ProductoParametro");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CapturarSiempre).HasColumnName("capturar_siempre");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .HasColumnName("tipo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.Property(e => e.ValorFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("valor_fecha");

                entity.Property(e => e.ValorLogico).HasColumnName("valor_logico");

                entity.Property(e => e.ValorNumerico).HasColumnName("valor_numerico");

                entity.Property(e => e.ValorTexto)
                    .HasMaxLength(50)
                    .HasColumnName("valor_texto");
            });

            modelBuilder.Entity<VwProductoPresentacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ProductoPresentacion");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.GeneraLpAuto).HasColumnName("genera_lp_auto");

                entity.Property(e => e.ImprimeBarra).HasColumnName("imprime_barra");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.PermitirPaletizar).HasColumnName("permitir_paletizar");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwProductoRellenado>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ProductoRellenado");

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.NomPresentacionRellenarCon).HasMaxLength(50);

                entity.Property(e => e.NomUmBasAbastecerCon).HasMaxLength(50);

                entity.Property(e => e.Presentación).HasMaxLength(50);

                entity.Property(e => e.Ubicación).HasMaxLength(50);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(30)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwProductoSi>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ProductoSI");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Activopp).HasColumnName("activopp");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.Arancel).HasMaxLength(150);

                entity.Property(e => e.CapturaArancel).HasColumnName("captura_arancel");

                entity.Property(e => e.CapturarAniada).HasColumnName("capturar_aniada");

                entity.Property(e => e.CicloVida).HasColumnName("ciclo_vida");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.CodigoBarraPcb)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra_pcb");

                entity.Property(e => e.CodigoBarraPresentacion)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_barra_presentacion");

                entity.Property(e => e.ControlLote).HasColumnName("control_lote");

                entity.Property(e => e.ControlPeso).HasColumnName("control_peso");

                entity.Property(e => e.ControlVencimiento).HasColumnName("control_vencimiento");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.EsHardware).HasColumnName("es_hardware");

                entity.Property(e => e.ExistenciaMax).HasColumnName("existencia_max");

                entity.Property(e => e.ExistenciaMin).HasColumnName("existencia_min");

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Fechamanufactura).HasColumnName("fechamanufactura");

                entity.Property(e => e.GeneraLote).HasColumnName("genera_lote");

                entity.Property(e => e.GeneraLpOld).HasColumnName("genera_lp_old");

                entity.Property(e => e.Imagen).HasColumnName("imagen");

                entity.Property(e => e.Kit).HasColumnName("kit");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.MateriaPrima).HasColumnName("materia_prima");

                entity.Property(e => e.NomPresentacion).HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Noparte)
                    .HasMaxLength(50)
                    .HasColumnName("noparte");

                entity.Property(e => e.Noserie)
                    .HasMaxLength(50)
                    .HasColumnName("noserie");

                entity.Property(e => e.PesoDespacho).HasColumnName("peso_despacho");

                entity.Property(e => e.PesoRecepcion).HasColumnName("peso_recepcion");

                entity.Property(e => e.PesoReferencia).HasColumnName("peso_referencia");

                entity.Property(e => e.PesoTolerancia).HasColumnName("peso_tolerancia");

                entity.Property(e => e.Precio).HasColumnName("precio");

                entity.Property(e => e.Serializado).HasColumnName("serializado");

                entity.Property(e => e.TemperaturaDespacho).HasColumnName("temperatura_despacho");

                entity.Property(e => e.TemperaturaRecepcion).HasColumnName("temperatura_recepcion");

                entity.Property(e => e.TemperaturaReferencia).HasColumnName("temperatura_referencia");

                entity.Property(e => e.TemperaturaTolerancia).HasColumnName("temperatura_tolerancia");

                entity.Property(e => e.Tolerancia).HasColumnName("tolerancia");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwProductoSustituto>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ProductoSustituto");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.PresentaciónOriginal)
                    .HasMaxLength(50)
                    .HasColumnName("Presentación Original")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.PresentaciónReemplazo)
                    .HasMaxLength(50)
                    .HasColumnName("Presentación Reemplazo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ProductoReemplazo)
                    .HasMaxLength(50)
                    .HasColumnName("Producto Reemplazo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwProductoTipo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ProductoTipo");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.NombreTipoProducto)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwPropietario>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Propietario");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.ActualizaCostoOc).HasColumnName("actualiza_costo_oc");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(25)
                    .HasColumnName("codigo");

                entity.Property(e => e.Color).HasColumnName("color");

                entity.Property(e => e.Contacto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("contacto")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .HasColumnName("direccion")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Empresa)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Imagen)
                    .HasColumnType("image")
                    .HasColumnName("imagen");

                entity.Property(e => e.Nit)
                    .HasMaxLength(50)
                    .HasColumnName("nit");

                entity.Property(e => e.NombreComercial)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombre_comercial")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("telefono")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwPropietarioReglaRecepcion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Propietario_Regla_Recepcion");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Mensaje).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Regla).HasMaxLength(50);
            });

            modelBuilder.Entity<VwProveedor>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Proveedor");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.ActivoProveedorBodega).HasColumnName("activo_proveedor_bodega");

                entity.Property(e => e.ActualizaCostoOc).HasColumnName("actualiza_costo_oc");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.Contacto)
                    .HasMaxLength(100)
                    .HasColumnName("contacto");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .HasColumnName("direccion")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Empresa)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EsBodegaRecepcion).HasColumnName("es_bodega_recepcion");

                entity.Property(e => e.EsBodegaTraslado).HasColumnName("es_bodega_traslado");

                entity.Property(e => e.EsProveedorServicio).HasColumnName("es_proveedor_servicio");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Idubicacionvirtual).HasColumnName("idubicacionvirtual");

                entity.Property(e => e.MuestraPrecio).HasColumnName("muestra_precio");

                entity.Property(e => e.Nit)
                    .HasMaxLength(25)
                    .HasColumnName("nit")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(25)
                    .HasColumnName("referencia");

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("telefono");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwProveedorBodega>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ProveedorBodega");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.ActivoProveedorBodega).HasColumnName("activo_proveedor_bodega");

                entity.Property(e => e.ActualizaCostoOc).HasColumnName("actualiza_costo_oc");

                entity.Property(e => e.Contacto)
                    .HasMaxLength(100)
                    .HasColumnName("contacto");

                entity.Property(e => e.Código).HasMaxLength(50);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .HasColumnName("direccion")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Empresa)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EsBodegaRecepcion).HasColumnName("es_bodega_recepcion");

                entity.Property(e => e.EsBodegaTraslado).HasColumnName("es_bodega_traslado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Idubicacionvirtual).HasColumnName("idubicacionvirtual");

                entity.Property(e => e.MuestraPrecio).HasColumnName("muestra_precio");

                entity.Property(e => e.Nit)
                    .HasMaxLength(25)
                    .HasColumnName("nit")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("telefono");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwProximosVencimiento>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ProximosVencimiento");

                entity.Property(e => e.Barra).HasMaxLength(35);

                entity.Property(e => e.CantidadSf).HasColumnName("CantidadSF");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaProyectada).HasColumnType("datetime");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NomEstado).HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ToleranciaDias).HasColumnName("Tolerancia_Dias");

                entity.Property(e => e.UbicacionCompleta).HasMaxLength(90);

                entity.Property(e => e.UnidadMedida).HasMaxLength(50);
            });

            modelBuilder.Entity<VwRecConOc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_REC_CON_OC");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.EstadoProducto).HasMaxLength(50);

                entity.Property(e => e.EstadoRec).HasMaxLength(20);

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_recepcion");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.FirmaPiloto)
                    .HasColumnType("image")
                    .HasColumnName("firma_piloto");

                entity.Property(e => e.HoraFinPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin_pc");

                entity.Property(e => e.HoraIniPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini_pc");

                entity.Property(e => e.IdProveedor).HasColumnName("Id_Proveedor");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.Marca)
                    .HasMaxLength(50)
                    .HasColumnName("marca")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NoDocto)
                    .HasMaxLength(50)
                    .HasColumnName("no_docto");

                entity.Property(e => e.NoLinea).HasColumnName("No_Linea");

                entity.Property(e => e.NoMarchamo)
                    .HasMaxLength(50)
                    .HasColumnName("No_Marchamo");

                entity.Property(e => e.NombrePiloto).HasMaxLength(301);

                entity.Property(e => e.Operador)
                    .HasMaxLength(201)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Placa)
                    .HasMaxLength(20)
                    .HasColumnName("placa")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Proveedor)
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Referencia).HasMaxLength(100);

                entity.Property(e => e.TipoTrans).HasMaxLength(25);

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .HasColumnName("Unidad_Medida");
            });

            modelBuilder.Entity<VwRecConocFin>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_REC_CONOC_FIN");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EstadoProducto)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EstadoRec).HasMaxLength(20);

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_recepcion");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.HoraFinPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin_pc");

                entity.Property(e => e.HoraIniPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini_pc");

                entity.Property(e => e.IdProveedor).HasColumnName("Id_Proveedor");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoDocumento)
                    .HasMaxLength(50)
                    .HasColumnName("No_Documento");

                entity.Property(e => e.NoLinea).HasColumnName("No_Linea");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Presentacion)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Producto)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Proveedor)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TipoTrans).HasMaxLength(25);

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .HasColumnName("Unidad_Medida")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwRecOcMix>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_RecOC_MIX");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EstadoProducto)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EstadoRec).HasMaxLength(20);

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_recepcion");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.HoraFinPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin_pc");

                entity.Property(e => e.HoraIniPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini_pc");

                entity.Property(e => e.IdProveedor).HasColumnName("Id_Proveedor");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoDocumento)
                    .HasMaxLength(50)
                    .HasColumnName("No_Documento");

                entity.Property(e => e.NoLinea).HasColumnName("No_Linea");

                entity.Property(e => e.Presentacion)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Producto)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Proveedor)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TipoTrans).HasMaxLength(25);

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .HasColumnName("Unidad_Medida")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwRecSinOc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_REC_SIN_OC");

                entity.Property(e => e.AtributoVariante)
                    .HasMaxLength(25)
                    .HasColumnName("Atributo_Variante");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.EstadoProducto).HasMaxLength(50);

                entity.Property(e => e.EstadoRec).HasMaxLength(20);

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_recepcion");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.HoraFinPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin_pc");

                entity.Property(e => e.HoraIniPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini_pc");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoLinea).HasColumnName("No_Linea");

                entity.Property(e => e.NoMarchamo)
                    .HasMaxLength(50)
                    .HasColumnName("No_Marchamo");

                entity.Property(e => e.NombrePiloto).HasMaxLength(301);

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Placa)
                    .HasMaxLength(20)
                    .HasColumnName("placa")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TipoTrans).HasMaxLength(25);

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .HasColumnName("Unidad_Medida");
            });

            modelBuilder.Entity<VwRecepcion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Recepcion");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Bodega).HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.FechaAgrego)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Agrego");

                entity.Property(e => e.IdTipoTransaccion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Muelle)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NoDocumentoOc)
                    .HasMaxLength(131)
                    .HasColumnName("No_DocumentoOC");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Proveedor)
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TipoTrans).HasMaxLength(25);

                entity.Property(e => e.UsuarioAgrego)
                    .HasMaxLength(100)
                    .HasColumnName("Usuario_Agrego");
            });

            modelBuilder.Entity<VwRecepcionConOc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_RecepcionConOC");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EstadoProducto)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EstadoRec).HasMaxLength(20);

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_recepcion");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.HoraFinPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin_pc");

                entity.Property(e => e.HoraIniPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini_pc");

                entity.Property(e => e.IdEstadoOc).HasColumnName("IdEstadoOC");

                entity.Property(e => e.IdProveedor).HasColumnName("Id_Proveedor");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoDocumento)
                    .HasMaxLength(50)
                    .HasColumnName("No_Documento");

                entity.Property(e => e.NoLinea).HasColumnName("No_Linea");

                entity.Property(e => e.Presentacion)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Producto)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Proveedor)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TipoTrans).HasMaxLength(25);

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .HasColumnName("Unidad_Medida")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwRecepcionCostoArancel>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_RecepcionCostoArancel");

                entity.Property(e => e.Arancel).HasMaxLength(150);

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CantidadRecibida).HasColumnName("cantidad_recibida");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.Estado).HasMaxLength(20);

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_recepcion");

                entity.Property(e => e.HoraFinPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin_pc");

                entity.Property(e => e.HoraIniPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini_pc");

                entity.Property(e => e.NoDocumento)
                    .HasMaxLength(25)
                    .HasColumnName("No_Documento")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NoLinea).HasColumnName("No_Linea");

                entity.Property(e => e.NoMarchamo)
                    .HasMaxLength(50)
                    .HasColumnName("No_Marchamo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NoPoliza).HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Presentacion)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Proveedor)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwRecepcionDet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Recepcion_Det");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.BarraProd).HasMaxLength(35);

                entity.Property(e => e.CodigoBodega)
                    .HasMaxLength(50)
                    .HasColumnName("Codigo_Bodega");

                entity.Property(e => e.CodigoProd).HasMaxLength(50);

                entity.Property(e => e.CodigoProveedor)
                    .HasMaxLength(50)
                    .HasColumnName("Codigo_Proveedor");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado");

                entity.Property(e => e.EstadoProd).HasMaxLength(50);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.FechaAgrego)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Agrego");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.NoDocumentoOc)
                    .HasMaxLength(30)
                    .HasColumnName("No_DocumentoOC");

                entity.Property(e => e.NombreProd).HasMaxLength(100);

                entity.Property(e => e.NombreProveedor)
                    .HasMaxLength(100)
                    .HasColumnName("Nombre_Proveedor")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.OperadorHh)
                    .HasMaxLength(100)
                    .HasColumnName("Operador_HH")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Poliza).HasMaxLength(50);

                entity.Property(e => e.PresProd).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ReferenciaOc)
                    .HasMaxLength(100)
                    .HasColumnName("ReferenciaOC");

                entity.Property(e => e.TipoTrans).HasMaxLength(25);

                entity.Property(e => e.Um)
                    .HasMaxLength(50)
                    .HasColumnName("UM");

                entity.Property(e => e.UsuarioAgrego)
                    .HasMaxLength(100)
                    .HasColumnName("Usuario_Agrego");
            });

            modelBuilder.Entity<VwRecepcionForHhByIdBodega>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Recepcion_For_HH_By_IdBodega");

                entity.Property(e => e.MotivoDevolucion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NoDocumento)
                    .HasMaxLength(30)
                    .HasColumnName("No_Documento");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Proveedor)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Referencia).HasMaxLength(100);

                entity.Property(e => e.Tipo).HasMaxLength(50);

                entity.Property(e => e.TipoTrans).HasMaxLength(25);
            });

            modelBuilder.Entity<VwRecepcionForHhByIdBodegaByOperador>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Recepcion_For_HH_By_IdBodega_By_Operador");

                entity.Property(e => e.MotivoDevolucion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NoDocumento)
                    .HasMaxLength(30)
                    .HasColumnName("No_Documento");

                entity.Property(e => e.NumOrden).HasMaxLength(50);

                entity.Property(e => e.NumPoliza).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Proveedor)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Referencia).HasMaxLength(100);

                entity.Property(e => e.Tipo).HasMaxLength(50);

                entity.Property(e => e.TipoTrans).HasMaxLength(25);
            });

            modelBuilder.Entity<VwRecepcionSinOc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_RecepcionSinOC");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EstadoProducto)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EstadoRec).HasMaxLength(20);

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_recepcion");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.HoraFinPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin_pc");

                entity.Property(e => e.HoraIniPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini_pc");

                entity.Property(e => e.IdProveedor).HasColumnName("Id_Proveedor");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoDocumento)
                    .HasMaxLength(50)
                    .HasColumnName("No_Documento");

                entity.Property(e => e.NoLinea).HasColumnName("No_Linea");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Presentacion)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Producto)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Propietario)
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Proveedor)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TipoTrans).HasMaxLength(25);

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .HasColumnName("Unidad_Medida")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwRecepcionesEncOc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_RecepcionesEncOC");

                entity.Property(e => e.Estado).HasMaxLength(20);
            });

            modelBuilder.Entity<VwReporteDetalleStockDataSet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Reporte_Detalle_Stock_DataSet");

                entity.Property(e => e.Barra)
                    .HasMaxLength(35)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CantPresentacion).HasColumnName("Cant_Presentacion");

                entity.Property(e => e.CantUmbas).HasColumnName("Cant_UMBas");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Presentación)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Producto)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.Tramo)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Ubicacion)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Umbas)
                    .HasMaxLength(50)
                    .HasColumnName("UMBas")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwReporteRecepcion20190726>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Reporte_Recepcion_20190726");

                entity.Property(e => e.AtributoVariante)
                    .HasMaxLength(25)
                    .HasColumnName("Atributo_Variante");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.EstadoProducto).HasMaxLength(50);

                entity.Property(e => e.EstadoRec).HasMaxLength(20);

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_recepcion");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.HoraFinPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin_pc");

                entity.Property(e => e.HoraIniPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini_pc");

                entity.Property(e => e.IdProveedor).HasColumnName("Id_Proveedor");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoDocumento)
                    .HasMaxLength(30)
                    .HasColumnName("No_Documento");

                entity.Property(e => e.NoLinea).HasColumnName("No_Linea");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Proveedor)
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TipoTrans).HasMaxLength(25);

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .HasColumnName("Unidad_Medida");
            });

            modelBuilder.Entity<VwReporteRecepcion20190727>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Reporte_Recepcion_20190727");

                entity.Property(e => e.AtributoVariante)
                    .HasMaxLength(25)
                    .HasColumnName("Atributo_Variante");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.EstadoProducto).HasMaxLength(50);

                entity.Property(e => e.EstadoRec).HasMaxLength(20);

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_recepcion");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.HoraFinPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin_pc");

                entity.Property(e => e.HoraIniPc)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini_pc");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoLinea).HasColumnName("No_Linea");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TipoTrans).HasMaxLength(25);

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .HasColumnName("Unidad_Medida");
            });

            modelBuilder.Entity<VwRevisionProducto>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Revision_Producto");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("Codigo_Producto");

                entity.Property(e => e.DisponibleUmbas).HasColumnName("DisponibleUMBas");

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.NombrePresentacionAbastecerCon).HasMaxLength(50);

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .HasColumnName("Nombre_Producto");

                entity.Property(e => e.NombrePropietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Nombre_Propietario")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreUmBas).HasMaxLength(50);

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.StockUmbas).HasColumnName("StockUMBas");

                entity.Property(e => e.Ubicacion).HasMaxLength(200);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(30)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwRevisionProducto1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_RevisionProducto");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.Presentación)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Producto)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Ubicación)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwRptMinimosMaximo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_rptMinimosMaximos");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.IndiceRotacion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LicPlate)
                    .IsUnicode(false)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NomEstado)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Presentacion)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.TotalLinea).HasColumnName("totalLinea");

                entity.Property(e => e.UbicacionAnterior)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwRptMinimosMaximosV2>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_rptMinimosMaximos_v2");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.CantidadSf).HasColumnName("CantidadSF");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.ExistenciaMaxUmbas).HasColumnName("Existencia_max_umbas");

                entity.Property(e => e.ExistenciaMinUmbas).HasColumnName("Existencia_min_umbas");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.IndiceRotacion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NomEstado).HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.TotalLinea).HasColumnName("totalLinea");

                entity.Property(e => e.UbicacionAnterior).HasMaxLength(50);

                entity.Property(e => e.UnidadMedida).HasMaxLength(50);
            });

            modelBuilder.Entity<VwRptProductosProximosVencimiento>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_rptProductosProximosVencimiento");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ControlVencimiento).HasColumnName("control_vencimiento");

                entity.Property(e => e.ExistenciaMax).HasColumnName("existencia_max");

                entity.Property(e => e.ExistenciaMin).HasColumnName("existencia_min");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaProyectada).HasColumnType("datetime");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NomEstado)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Presentacion)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwRptStock>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_RptStock");

                entity.Property(e => e.CantPresentación).HasColumnName("Cant Presentación");

                entity.Property(e => e.CantUMBas).HasColumnName("Cant U.M Bas");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FechaManufactura)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_manufactura");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NoSerie).HasMaxLength(50);

                entity.Property(e => e.NoSerieFinal).HasMaxLength(50);

                entity.Property(e => e.NoSerieInicial).HasMaxLength(50);

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Presentacion)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Producto)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwServicio>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Servicio");

                entity.Property(e => e.Almacen)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.FechaServicio)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Servicio");

                entity.Property(e => e.IdPropietarioEnc).HasColumnName("IdPropietario_Enc");

                entity.Property(e => e.NoLinea).HasColumnName("No_Linea");

                entity.Property(e => e.NoOrden)
                    .HasMaxLength(50)
                    .HasColumnName("no_orden");

                entity.Property(e => e.NombreCliente)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Nombre_Cliente")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(150)
                    .HasColumnName("Nombre_Producto");

                entity.Property(e => e.TipoTransaccion)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("Tipo_Transaccion");
            });

            modelBuilder.Entity<VwStock>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Stock");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodigoBarraPresentacion)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreEstadoProducto)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombrePresentacion)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombrePropietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreUnidadMedida)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Precio).HasColumnName("precio");
            });

            modelBuilder.Entity<VwStockEnc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Stock_Enc");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.CantidadSf).HasColumnName("CantidadSF");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.ExistenciaMaxPres).HasColumnName("existencia_max_pres");

                entity.Property(e => e.ExistenciaMaxUmbas).HasColumnName("existencia_max_umbas");

                entity.Property(e => e.ExistenciaMinPres).HasColumnName("existencia_min_pres");

                entity.Property(e => e.ExistenciaMinUmbas).HasColumnName("existencia_min_umbas");

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.IndiceRotacion).HasMaxLength(50);

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.LicPlate)
                    .IsUnicode(false)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NomEstado)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Presentacion)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwStockEspecifico>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Stock_Especifico");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.AltoUbicacion).HasColumnName("Alto_ubicacion");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.AnchoUbicacion).HasColumnName("Ancho_ubicacion");

                entity.Property(e => e.Aplica)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.AtributoVariante1)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("atributo_variante_1");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Barra)
                    .IsRequired()
                    .HasMaxLength(35);

                entity.Property(e => e.CantidadPresentacion).HasColumnName("Cantidad_Presentacion");

                entity.Property(e => e.CantidadUmbas).HasColumnName("Cantidad_UMBas");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.DiasExterior).HasColumnName("Dias_Exterior");

                entity.Property(e => e.DiasLocal).HasColumnName("Dias_Local");

                entity.Property(e => e.DisponibleUmbas).HasColumnName("Disponible_UMBas");

                entity.Property(e => e.ExistenciaMaxPres).HasColumnName("Existencia_max_pres");

                entity.Property(e => e.ExistenciaMaxUmbas).HasColumnName("Existencia_max_umbas");

                entity.Property(e => e.ExistenciaMinPres).HasColumnName("Existencia_min_pres");

                entity.Property(e => e.ExistenciaMinUmbas).HasColumnName("Existencia_min_umbas");

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.IndiceRotacion).HasMaxLength(50);

                entity.Property(e => e.Ingreso).HasColumnType("datetime");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.LargoUbicacion).HasColumnName("Largo_ubicacion");

                entity.Property(e => e.LicPlate)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.MotivoDevolucion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NoPoliza)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NomEstado).HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Presentacion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Serial)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.UbicacionCompleta).HasMaxLength(90);

                entity.Property(e => e.UbicacionIndiceX).HasColumnName("Ubicacion_Indice_X");

                entity.Property(e => e.UbicacionNivel).HasColumnName("Ubicacion_Nivel");

                entity.Property(e => e.UbicacionNombre)
                    .HasMaxLength(50)
                    .HasColumnName("Ubicacion_Nombre");

                entity.Property(e => e.UbicacionTramo)
                    .HasMaxLength(50)
                    .HasColumnName("Ubicacion_Tramo");

                entity.Property(e => e.UnidadMedida)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Vence).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwStockEstadosProducto>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_StockEstadosProducto");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Utilizable).HasColumnName("utilizable");
            });

            modelBuilder.Entity<VwStockJornadum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Stock_Jornada");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.AtributoVariante1)
                    .HasMaxLength(25)
                    .HasColumnName("atributo_variante_1");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Bodega).HasMaxLength(50);

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Clasificacion).HasMaxLength(100);

                entity.Property(e => e.CodigoBarraProducto)
                    .HasMaxLength(35)
                    .HasColumnName("Codigo_Barra_Producto");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("Codigo_Producto");

                entity.Property(e => e.CodigoRegimen)
                    .HasMaxLength(20)
                    .HasColumnName("codigo_regimen");

                entity.Property(e => e.DiasVencimientoRegimen).HasColumnName("dias_vencimiento_regimen");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaAgrego)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Agrego");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaIngresoTicketTms)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Ingreso_Ticket_TMS");

                entity.Property(e => e.FechaManufactura)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_manufactura");

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_recepcion");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.IdTicketTms)
                    .HasMaxLength(50)
                    .HasColumnName("IdTicketTMS");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NoBulto).HasColumnName("no_bulto");

                entity.Property(e => e.NoDocumentoOc)
                    .HasMaxLength(30)
                    .HasColumnName("No_DocumentoOC");

                entity.Property(e => e.NoDocumentoRec)
                    .HasMaxLength(100)
                    .HasColumnName("No_DocumentoRec");

                entity.Property(e => e.NoPoliza)
                    .HasMaxLength(50)
                    .HasColumnName("No_Poliza");

                entity.Property(e => e.NomEstadoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("nom_estado_producto");

                entity.Property(e => e.NomUmBas)
                    .HasMaxLength(50)
                    .HasColumnName("Nom_umBas");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .HasColumnName("Nombre_Producto");

                entity.Property(e => e.NombreRegimen)
                    .HasMaxLength(500)
                    .HasColumnName("nombre_regimen");

                entity.Property(e => e.NumeroOrden)
                    .HasMaxLength(50)
                    .HasColumnName("numero_orden");

                entity.Property(e => e.PalletNoEstandar).HasColumnName("pallet_no_estandar");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.PesoBruto).HasColumnName("peso_bruto");

                entity.Property(e => e.PesoNeto).HasColumnName("peso_neto");

                entity.Property(e => e.PresentacionProducto)
                    .HasMaxLength(50)
                    .HasColumnName("Presentacion_Producto");

                entity.Property(e => e.Propietario)
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Proveedor)
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ReferenciaOc)
                    .HasMaxLength(50)
                    .HasColumnName("ReferenciaOC");

                entity.Property(e => e.Regimen)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.Temperatura).HasColumnName("temperatura");

                entity.Property(e => e.TipoTrans).HasMaxLength(25);

                entity.Property(e => e.UbicacionOrigen)
                    .HasMaxLength(200)
                    .HasColumnName("Ubicacion_Origen");

                entity.Property(e => e.UdsLicPlate).HasColumnName("uds_lic_plate");

                entity.Property(e => e.UserAgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");

                entity.Property(e => e.ValorAduana).HasColumnName("valor_aduana");

                entity.Property(e => e.ValorDai).HasColumnName("valor_dai");

                entity.Property(e => e.ValorFlete).HasColumnName("valor_flete");

                entity.Property(e => e.ValorFob).HasColumnName("valor_fob");

                entity.Property(e => e.ValorIva).HasColumnName("valor_iva");

                entity.Property(e => e.ValorSeguro).HasColumnName("valor_seguro");
            });

            modelBuilder.Entity<VwStockPorProductoUbicacionCi>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Stock_Por_Producto_Ubicacion_CI");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.AltoUbicacion).HasColumnName("Alto_ubicacion");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.AnchoUbicacion).HasColumnName("Ancho_ubicacion");

                entity.Property(e => e.Aplica)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.AtributoVariante1)
                    .HasMaxLength(25)
                    .HasColumnName("atributo_variante_1");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Barra).HasMaxLength(35);

                entity.Property(e => e.CantidadUmbas).HasColumnName("Cantidad_UMBas");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.DiasExterior).HasColumnName("Dias_Exterior");

                entity.Property(e => e.DiasLocal).HasColumnName("Dias_Local");

                entity.Property(e => e.DisponibleUmbas).HasColumnName("Disponible_UMBas");

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.ExistenciaMaxPres).HasColumnName("Existencia_max_pres");

                entity.Property(e => e.ExistenciaMaxUmbas).HasColumnName("Existencia_max_umbas");

                entity.Property(e => e.ExistenciaMinPres).HasColumnName("Existencia_min_pres");

                entity.Property(e => e.ExistenciaMinUmbas).HasColumnName("Existencia_min_umbas");

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.IndiceRotacion).HasMaxLength(50);

                entity.Property(e => e.Ingreso).HasColumnType("datetime");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.LargoUbicacion).HasColumnName("Largo_ubicacion");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.MotivoDevolucion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NoPoliza)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.UbicacionCompleta).HasMaxLength(200);

                entity.Property(e => e.UbicacionIndiceX).HasColumnName("Ubicacion_Indice_X");

                entity.Property(e => e.UbicacionNivel).HasColumnName("Ubicacion_Nivel");

                entity.Property(e => e.UbicacionNombre)
                    .HasMaxLength(50)
                    .HasColumnName("Ubicacion_Nombre");

                entity.Property(e => e.UbicacionTramo)
                    .HasMaxLength(50)
                    .HasColumnName("Ubicacion_Tramo");

                entity.Property(e => e.UnidadMedida).HasMaxLength(50);

                entity.Property(e => e.Vence).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwStockPresentacione>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_StockPresentaciones");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_barra")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.ImprimeBarra).HasColumnName("imprime_barra");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwStockRecep>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Stock_Recep");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.LicPlate)
                    .IsUnicode(false)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreEstado)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_estado")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombrePresentacion)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_presentacion")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombrePropietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombre_propietario")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.Sumcant).HasColumnName("sumcant");

                entity.Property(e => e.Sumpeso).HasColumnName("sumpeso");

                entity.Property(e => e.Temperatura).HasColumnName("temperatura");
            });

            modelBuilder.Entity<VwStockResConsolidador>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Stock_Res_Consolidador");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.AltoUbicacion).HasColumnName("alto_ubicacion");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.AnchoUbicacion).HasColumnName("ancho_ubicacion");

                entity.Property(e => e.AtributoVariante1)
                    .HasMaxLength(25)
                    .HasColumnName("atributo_variante_1");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Bodega).HasMaxLength(50);

                entity.Property(e => e.CantidadSf).HasColumnName("CantidadSF");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.Consolidador).HasColumnName("consolidador");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.ExistenciaMaxPres).HasColumnName("existencia_max_pres");

                entity.Property(e => e.ExistenciaMaxUmbas).HasColumnName("existencia_max_umbas");

                entity.Property(e => e.ExistenciaMinPres).HasColumnName("existencia_min_pres");

                entity.Property(e => e.ExistenciaMinUmbas).HasColumnName("existencia_min_umbas");

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.IndiceRotacion).HasMaxLength(50);

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.LargoUbicacion).HasColumnName("largo_ubicacion");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NomEstado).HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(200)
                    .HasColumnName("Nombre_Completo");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.UbicacionIndiceX).HasColumnName("Ubicacion_Indice_X");

                entity.Property(e => e.UbicacionNivel).HasColumnName("Ubicacion_Nivel");

                entity.Property(e => e.UbicacionNombre)
                    .HasMaxLength(50)
                    .HasColumnName("Ubicacion_Nombre");

                entity.Property(e => e.UbicacionTramo)
                    .HasMaxLength(50)
                    .HasColumnName("Ubicacion_Tramo");

                entity.Property(e => e.UnidadMedida).HasMaxLength(50);
            });

            modelBuilder.Entity<VwStockResPedido>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Stock_Res_Pedido");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Bodegaubicacion)
                    .HasMaxLength(50)
                    .HasColumnName("bodegaubicacion");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Cantidadfisica).HasColumnName("cantidadfisica");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaManufactura)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_manufactura");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.Host)
                    .HasMaxLength(50)
                    .HasColumnName("host");

                entity.Property(e => e.Indicador).HasMaxLength(50);

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NoBulto).HasColumnName("no_bulto");

                entity.Property(e => e.NomEstado).HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Presentacion)
                    .HasMaxLength(50)
                    .HasColumnName("presentacion");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("propietario")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(25)
                    .HasColumnName("referencia");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.UbicacionAnt)
                    .HasMaxLength(25)
                    .HasColumnName("ubicacion_ant")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UdsLicPlate).HasColumnName("uds_lic_plate");

                entity.Property(e => e.Unidadmedida)
                    .HasMaxLength(50)
                    .HasColumnName("unidadmedida");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwStockResTipoProducto>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Stock_Res_Tipo_Producto");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.AltoUbicacion).HasColumnName("alto_ubicacion");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.AnchoUbicacion).HasColumnName("ancho_ubicacion");

                entity.Property(e => e.AtributoVariante1)
                    .HasMaxLength(25)
                    .HasColumnName("atributo_variante_1");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.CantidadSf).HasColumnName("CantidadSF");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.ExistenciaMaxPres).HasColumnName("existencia_max_pres");

                entity.Property(e => e.ExistenciaMaxUmbas).HasColumnName("existencia_max_umbas");

                entity.Property(e => e.ExistenciaMinPres).HasColumnName("existencia_min_pres");

                entity.Property(e => e.ExistenciaMinUmbas).HasColumnName("existencia_min_umbas");

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.IndiceRotacion).HasMaxLength(50);

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.LargoUbicacion).HasColumnName("largo_ubicacion");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NomEstado).HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreTipoProducto)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.UbicacionIndiceX).HasColumnName("Ubicacion_Indice_X");

                entity.Property(e => e.UbicacionNivel).HasColumnName("Ubicacion_Nivel");

                entity.Property(e => e.UbicacionNombre)
                    .HasMaxLength(50)
                    .HasColumnName("Ubicacion_Nombre");

                entity.Property(e => e.UbicacionTramo)
                    .HasMaxLength(50)
                    .HasColumnName("Ubicacion_Tramo");

                entity.Property(e => e.UnidadMedida).HasMaxLength(50);
            });

            modelBuilder.Entity<VwStockResV1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Stock_Res_V1");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.AtributoVariante1)
                    .HasMaxLength(25)
                    .HasColumnName("atributo_variante_1");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.CantidadSf).HasColumnName("CantidadSF");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.ExistenciaMaxPres).HasColumnName("existencia_max_pres");

                entity.Property(e => e.ExistenciaMaxUmbas).HasColumnName("existencia_max_umbas");

                entity.Property(e => e.ExistenciaMinPres).HasColumnName("existencia_min_pres");

                entity.Property(e => e.ExistenciaMinUmbas).HasColumnName("existencia_min_umbas");

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NomEstado).HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.UnidadMedida).HasMaxLength(50);
            });

            modelBuilder.Entity<VwStockReservadoByIdPedidoEnc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Stock_Reservado_By_IdPedidoEnc");

                entity.Property(e => e.CantidadPresentacion).HasColumnName("Cantidad_Presentacion");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Vence");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(90)
                    .HasColumnName("Nombre_Completo");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Umbas)
                    .HasMaxLength(50)
                    .HasColumnName("UMBas");
            });

            modelBuilder.Entity<VwStockResuman>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Stock_Res");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.AltoUbicacion).HasColumnName("Alto_ubicacion");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.AnchoUbicacion).HasColumnName("Ancho_ubicacion");

                entity.Property(e => e.AtributoVariante1)
                    .HasMaxLength(25)
                    .HasColumnName("atributo_variante_1");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Barra).HasMaxLength(35);

                entity.Property(e => e.CantidadPresentacion).HasColumnName("Cantidad_Presentacion");

                entity.Property(e => e.CantidadUmbas).HasColumnName("Cantidad_UMBas");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoPoliza)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Codigo_Poliza");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.DisponibleUmbas).HasColumnName("Disponible_UMBas");

                entity.Property(e => e.ExistenciaMaxPres).HasColumnName("Existencia_max_pres");

                entity.Property(e => e.ExistenciaMaxUmbas).HasColumnName("Existencia_max_umbas");

                entity.Property(e => e.ExistenciaMinPres).HasColumnName("Existencia_min_pres");

                entity.Property(e => e.ExistenciaMinUmbas).HasColumnName("Existencia_min_umbas");

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.Familia).HasMaxLength(50);

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.IndiceRotacion).HasMaxLength(50);

                entity.Property(e => e.Ingreso).HasColumnType("datetime");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.LargoUbicacion).HasColumnName("Largo_ubicacion");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.MotivoDevolucion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NomEstado).HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.NumeroPoliza)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Numero_poliza");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.UbicacionCompleta).HasMaxLength(200);

                entity.Property(e => e.UbicacionIndiceX).HasColumnName("Ubicacion_Indice_X");

                entity.Property(e => e.UbicacionNivel).HasColumnName("Ubicacion_Nivel");

                entity.Property(e => e.UbicacionNombre)
                    .HasMaxLength(50)
                    .HasColumnName("Ubicacion_Nombre");

                entity.Property(e => e.UbicacionTramo)
                    .HasMaxLength(50)
                    .HasColumnName("Ubicacion_Tramo");

                entity.Property(e => e.UnidadMedida).HasMaxLength(50);

                entity.Property(e => e.Vence).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwStockSerieParametro>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Stock_Serie_Parametro");

                entity.Property(e => e.CódigoDeBarra)
                    .HasMaxLength(35)
                    .HasColumnName("Código de Barra")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CódigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("Código Producto")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha Ingreso");

                entity.Property(e => e.FechaVencimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha Vencimiento");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Presentación)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Producto)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UMBas)
                    .HasMaxLength(50)
                    .HasColumnName("U.M. Bas")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwStockSp>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Stock_SP");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.NomEstado).HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.UbicacionAnterior).HasMaxLength(50);

                entity.Property(e => e.UnidadMedida).HasMaxLength(50);
            });

            modelBuilder.Entity<VwStockTransito>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Stock_Transito");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CantidadPendiente).HasColumnName("Cantidad Pendiente");

                entity.Property(e => e.CantidadRecibida).HasColumnName("Cantidad Recibida");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.OrdenDeCompra).HasColumnName("Orden de Compra");

                entity.Property(e => e.Presentaciòn).HasMaxLength(50);

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Proveedor)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwTareasActivasHh>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Tareas_Activas_HH");

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.Inicio).HasColumnType("datetime");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tarea)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Ttm).HasColumnName("TTM");

                entity.Property(e => e.UltRevision)
                    .HasColumnType("datetime")
                    .HasColumnName("Ult_Revision");
            });

            modelBuilder.Entity<VwTareasHh>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_tareas_hh");

                entity.Property(e => e.Bodega)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.FechaFin).HasColumnType("datetime");

                entity.Property(e => e.FechaInicio).HasColumnType("datetime");

                entity.Property(e => e.Muelle)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Prioridad).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TipoTarea).HasMaxLength(50);
            });

            modelBuilder.Entity<VwTareasPickingHh>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Tareas_Picking_HH");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.DetalleOperador).HasColumnName("detalle_operador");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaPicking)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_picking");

                entity.Property(e => e.HoraFin)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_fin");

                entity.Property(e => e.HoraIni)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_ini");

                entity.Property(e => e.NombreBodega).HasMaxLength(50);

                entity.Property(e => e.NombreComercial)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombre_comercial")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreUbicacion).HasMaxLength(90);

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(30)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(30)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwTarima>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Tarimas");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.Disponible).HasColumnName("disponible");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.NombreTipoTarima)
                    .HasMaxLength(50)
                    .HasColumnName("nombreTipoTarima");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwTarimasUsadasEnTransaccion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_TarimasUsadasEnTransaccion");

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.CodigoTarima)
                    .HasMaxLength(50)
                    .HasColumnName("codigoTarima");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaUtilizacion).HasColumnType("datetime");

                entity.Property(e => e.NombreTipoTarima)
                    .HasMaxLength(50)
                    .HasColumnName("nombreTipoTarima");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwTiempoCliente>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_TiempoCliente");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Clasificación)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.DiasExterior).HasColumnName("Dias_Exterior");

                entity.Property(e => e.DiasLocal).HasColumnName("Dias_Local");

                entity.Property(e => e.Familia)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwTmsTikcet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_TMS_Tikcet");

                entity.Property(e => e.ApellidosPiloto)
                    .HasMaxLength(150)
                    .HasColumnName("Apellidos_Piloto");

                entity.Property(e => e.EmpresaTransporte)
                    .HasMaxLength(100)
                    .HasColumnName("Empresa_Transporte")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Ingreso");

                entity.Property(e => e.FechaSalida)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Salida");

                entity.Property(e => e.NoPoliza)
                    .HasMaxLength(50)
                    .HasColumnName("No_Poliza");

                entity.Property(e => e.NombrePiloto)
                    .HasMaxLength(150)
                    .HasColumnName("Nombre_Piloto");

                entity.Property(e => e.PlacaTc)
                    .HasMaxLength(50)
                    .HasColumnName("Placa_TC");

                entity.Property(e => e.PlacaVehiculo)
                    .HasMaxLength(20)
                    .HasColumnName("Placa_Vehiculo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TipoOperacion)
                    .HasMaxLength(50)
                    .HasColumnName("Tipo_Operacion");
            });

            modelBuilder.Entity<VwTransInvConteo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Trans_Inv_Conteo");

                entity.Property(e => e.Bodega)
                    .HasMaxLength(50)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CantidadConteo).HasColumnName("Cantidad_Conteo");

                entity.Property(e => e.CantidadStock).HasColumnName("Cantidad_Stock");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.EstadoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("Estado_Producto");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Ingreso");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.Idinventarioenc).HasColumnName("idinventarioenc");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.PesoConteo).HasColumnName("Peso_Conteo");

                entity.Property(e => e.PesoStock).HasColumnName("Peso_Stock");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Propietario)
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwTransInvStock>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Trans_Inv_Stock");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.AltoUbicacion).HasColumnName("alto_ubicacion");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.AnchoUbicacion).HasColumnName("ancho_ubicacion");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.CantidadUm).HasColumnName("CantidadUM");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(35)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.EstadoProducto).HasMaxLength(50);

                entity.Property(e => e.ExistenciaMax).HasColumnName("existencia_max");

                entity.Property(e => e.ExistenciaMin).HasColumnName("existencia_min");

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.IdUbicacionAnterior).HasColumnName("IdUbicacion_anterior");

                entity.Property(e => e.IndiceX).HasColumnName("indice_x");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.LargoUbicacion).HasColumnName("largo_ubicacion");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(50)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.Nivel).HasColumnName("nivel");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Producto).HasMaxLength(100);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.UbicacionNombre)
                    .HasMaxLength(50)
                    .HasColumnName("Ubicacion_Nombre")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UbicacionTramo)
                    .HasMaxLength(50)
                    .HasColumnName("Ubicacion_Tramo")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UnidadMedida).HasMaxLength(50);

                entity.Property(e => e.Utilizable).HasColumnName("utilizable");
            });

            modelBuilder.Entity<VwTransOcDet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_TRANS_OC_DET");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CantidadPendiente).HasColumnName("Cantidad_Pendiente");

                entity.Property(e => e.CantidadRecibida).HasColumnName("cantidad_recibida");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Creacion");

                entity.Property(e => e.NoDocumento)
                    .HasMaxLength(30)
                    .HasColumnName("No_Documento");

                entity.Property(e => e.NoLinea).HasColumnName("No_Linea");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_producto");

                entity.Property(e => e.Um)
                    .HasMaxLength(50)
                    .HasColumnName("UM");
            });

            modelBuilder.Entity<VwTransServicio>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Trans_Servicios");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Bodega).HasMaxLength(50);

                entity.Property(e => e.CodigoBodega).HasMaxLength(50);

                entity.Property(e => e.DocumentoIngreso)
                    .HasMaxLength(30)
                    .HasColumnName("Documento_Ingreso");

                entity.Property(e => e.DocumentoSalida).HasColumnName("Documento_Salida");

                entity.Property(e => e.EsIngreso).HasColumnName("es_ingreso");

                entity.Property(e => e.EstadoServicio)
                    .HasMaxLength(50)
                    .HasColumnName("Estado_Servicio");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Mi3Estatus).HasColumnName("MI3_Estatus");

                entity.Property(e => e.NoOrden)
                    .HasMaxLength(50)
                    .HasColumnName("no_orden");

                entity.Property(e => e.NoPoliza)
                    .HasMaxLength(50)
                    .HasColumnName("no_poliza");

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.RefDocSalida).HasMaxLength(25);
            });

            modelBuilder.Entity<VwTransUbicHhDet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_TransUbicHhDet");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Añada).HasColumnName("añada");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .HasMaxLength(25)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaIngreso)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("fecha_ingreso")
                    .IsFixedLength(true);

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.HoraFin).HasColumnType("datetime");

                entity.Property(e => e.HoraInicio).HasColumnType("datetime");

                entity.Property(e => e.IndiceX).HasColumnName("indice_x");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.Nivel).HasColumnName("nivel");

                entity.Property(e => e.NomEstado).HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreComercial)
                    .HasMaxLength(100)
                    .HasColumnName("nombre_comercial")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(100)
                    .HasColumnName("nombres")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Presentacion).HasMaxLength(50);

                entity.Property(e => e.Recibido).HasColumnName("recibido");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .HasColumnName("serial");

                entity.Property(e => e.Serializado).HasColumnName("serializado");

                entity.Property(e => e.Tramo).HasMaxLength(50);

                entity.Property(e => e.UnidadMedida).HasMaxLength(50);
            });

            modelBuilder.Entity<VwTransUbicacionHhEnc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_TransUbicacionHhEnc");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CambioEstado).HasColumnName("cambio_estado");

                entity.Property(e => e.DescripcionMotivo).HasMaxLength(50);

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .HasColumnName("estado");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.FechaFin).HasColumnType("date");

                entity.Property(e => e.FechaInicio).HasColumnType("date");

                entity.Property(e => e.HoraFin).HasColumnType("datetime");

                entity.Property(e => e.HoraInicio).HasColumnType("datetime");

                entity.Property(e => e.Observacion).HasMaxLength(150);

                entity.Property(e => e.OperadorPorLinea).HasColumnName("operador_por_linea");

                entity.Property(e => e.UbicacionConHh).HasColumnName("ubicacion_con_hh");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwUbicacionPicking>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_UbicacionPicking");

                entity.Property(e => e.CodigoProducto).HasMaxLength(50);

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.FechaPedido).HasColumnType("datetime");

                entity.Property(e => e.FechaPicking).HasColumnType("datetime");

                entity.Property(e => e.Lote)
                    .HasMaxLength(35)
                    .HasColumnName("lote");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(150)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NombreProducto).HasMaxLength(100);

                entity.Property(e => e.Operador)
                    .IsRequired()
                    .HasMaxLength(201)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Presentación).HasMaxLength(50);

                entity.Property(e => e.Ubicacion).HasMaxLength(50);

                entity.Property(e => e.Vence).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwUbicacionesPicking>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Ubicaciones_Picking");

                entity.Property(e => e.AceptaPallet).HasColumnName("Acepta_Pallet");

                entity.Property(e => e.Area).HasMaxLength(50);

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(50)
                    .HasColumnName("Codigo_Barra");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IndiceX).HasColumnName("Indice_X");

                entity.Property(e => e.Sector).HasMaxLength(50);

                entity.Property(e => e.Tramo).HasMaxLength(50);
            });

            modelBuilder.Entity<VwUbicacionesPorRegla>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_ubicaciones_por_regla");

                entity.Property(e => e.AceptaPallet).HasColumnName("acepta_pallet");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.Bloqueada).HasColumnName("bloqueada");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IndiceX).HasColumnName("indice_x");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.Nivel).HasColumnName("nivel");

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(200)
                    .HasColumnName("Nombre_Completo");

                entity.Property(e => e.ReglaUbicDetPeActivo).HasColumnName("regla_ubic_det_pe_Activo");

                entity.Property(e => e.ReglaUbicDetPropActivo).HasColumnName("regla_ubic_det_prop_Activo");

                entity.Property(e => e.ReglaUbicDetTpActivo).HasColumnName("regla_ubic_det_tp_Activo");
            });

            modelBuilder.Entity<VwUbicacionesTramoDisponible>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Ubicaciones_Tramo_Disponibles");

                entity.Property(e => e.AceptaPallet).HasColumnName("acepta_pallet");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Alto).HasColumnName("alto");

                entity.Property(e => e.Ancho).HasColumnName("ancho");

                entity.Property(e => e.Bloqueada).HasColumnName("bloqueada");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_barra");

                entity.Property(e => e.CodigoBarra2)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_barra2");

                entity.Property(e => e.Dañado).HasColumnName("dañado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.IndiceX).HasColumnName("indice_x");

                entity.Property(e => e.Largo).HasColumnName("largo");

                entity.Property(e => e.MargenDerecho).HasColumnName("margen_derecho");

                entity.Property(e => e.MargenInferior).HasColumnName("margen_inferior");

                entity.Property(e => e.MargenIzquierdo).HasColumnName("margen_izquierdo");

                entity.Property(e => e.MargenSuperior).HasColumnName("margen_superior");

                entity.Property(e => e.Nivel).HasColumnName("nivel");

                entity.Property(e => e.OrientacionPos)
                    .HasMaxLength(50)
                    .HasColumnName("orientacion_pos");

                entity.Property(e => e.Sistema).HasColumnName("sistema");

                entity.Property(e => e.UbicacionDespacho).HasColumnName("ubicacion_despacho");

                entity.Property(e => e.UbicacionMerma).HasColumnName("ubicacion_merma");

                entity.Property(e => e.UbicacionNe).HasColumnName("ubicacion_ne");

                entity.Property(e => e.UbicacionPicking).HasColumnName("ubicacion_picking");

                entity.Property(e => e.UbicacionRecepcion).HasColumnName("ubicacion_recepcion");

                entity.Property(e => e.UbicacionVirtual).HasColumnName("ubicacion_virtual");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(25)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(25)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwUnidadMedidum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_UnidadMedida");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(25)
                    .HasColumnName("codigo");

                entity.Property(e => e.EsUmCobro).HasColumnName("es_um_cobro");

                entity.Property(e => e.Factor).HasColumnName("factor");

                entity.Property(e => e.FecAgr)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_agr");

                entity.Property(e => e.FecMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_mod");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Propietario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UserAgr)
                    .HasMaxLength(50)
                    .HasColumnName("user_agr");

                entity.Property(e => e.UserMod)
                    .HasMaxLength(50)
                    .HasColumnName("user_mod");
            });

            modelBuilder.Entity<VwVerificacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Verificacion");

                entity.Property(e => e.CantidadRecibida).HasColumnName("cantidad_recibida");

                entity.Property(e => e.CantidadSolicitada).HasColumnName("cantidad_solicitada");

                entity.Property(e => e.CantidadVerificada).HasColumnName("cantidad_verificada");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.Diferencia).HasColumnName("diferencia");

                entity.Property(e => e.FechaVence)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vence");

                entity.Property(e => e.LicPlate)
                    .HasMaxLength(25)
                    .HasColumnName("lic_plate");

                entity.Property(e => e.Lote)
                    .HasMaxLength(35)
                    .HasColumnName("lote");

                entity.Property(e => e.Ndias).HasColumnName("ndias");

                entity.Property(e => e.NomEstado)
                    .HasMaxLength(50)
                    .HasColumnName("nom_estado");

                entity.Property(e => e.NomPresentacion)
                    .HasMaxLength(50)
                    .HasColumnName("nom_presentacion");

                entity.Property(e => e.NomUnidMed)
                    .HasMaxLength(50)
                    .HasColumnName("nom_unid_med");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .HasColumnName("nombre_producto");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
