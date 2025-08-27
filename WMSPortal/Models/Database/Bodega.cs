using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Bodega
    {
        public Bodega()
        {
            BodegaAreas = new HashSet<BodegaArea>();
            BodegaMonitorParametros = new HashSet<BodegaMonitorParametro>();
            BodegaMuelles = new HashSet<BodegaMuelle>();
            ClienteBodegas = new HashSet<ClienteBodega>();
            EmpresaTransporteBodegas = new HashSet<EmpresaTransporteBodega>();
            HorarioLaboralEncs = new HashSet<HorarioLaboralEnc>();
            INavConfigEncs = new HashSet<INavConfigEnc>();
            JornadaLaborals = new HashSet<JornadaLaboral>();
            MotivoAnulacionBodegas = new HashSet<MotivoAnulacionBodega>();
            MotivoDevolucionBodegas = new HashSet<MotivoDevolucionBodega>();
            OperadorBodegas = new HashSet<OperadorBodega>();
            ProductoBodegas = new HashSet<ProductoBodega>();
            PropietarioBodegas = new HashSet<PropietarioBodega>();
            ProveedorBodegas = new HashSet<ProveedorBodega>();
            RolBodegas = new HashSet<RolBodega>();
            TareaHhs = new HashSet<TareaHh>();
            TransDespachoEncs = new HashSet<TransDespachoEnc>();
            TransMovimientos = new HashSet<TransMovimiento>();
            TransPeEncs = new HashSet<TransPeEnc>();
            TransPickingEncs = new HashSet<TransPickingEnc>();
            TransServicioEncs = new HashSet<TransServicioEnc>();
            TransTrasEncIdBodegaDestinoNavigations = new HashSet<TransTrasEnc>();
            TransTrasEncIdBodegaOrigenNavigations = new HashSet<TransTrasEnc>();
            Turnos = new HashSet<Turno>();
            UsuarioBodegas = new HashSet<UsuarioBodega>();
        }

        public int IdBodega { get; set; }
        public int? IdPais { get; set; }
        public int IdEmpresa { get; set; }
        public string Codigo { get; set; }
        public string CodigoBarra { get; set; }
        public string Nombre { get; set; }
        public string NombreComercial { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Encargado { get; set; }
        public string UbicRecepcion { get; set; }
        public string UbicPicking { get; set; }
        public string UbicDespacho { get; set; }
        public string UbicMerma { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
        public string CoordenadaX { get; set; }
        public string CoordenadaY { get; set; }
        public double? Largo { get; set; }
        public double? Ancho { get; set; }
        public double? Alto { get; set; }
        public bool? ReservarStocksPorLinea { get; set; }
        public bool? RechazarPedidoPorStock { get; set; }
        public string IdTipoTransaccion { get; set; }
        public double? Zoom { get; set; }
        public int? IdMotivoUbicacionDañadoPicking { get; set; }
        public bool? CambioUbicacionAuto { get; set; }
        public string CodigoBodegaErp { get; set; }
        public int? UbicProductoNe { get; set; }
        public int? IdProductoEstadoNe { get; set; }
        public string CuentaIngresoMercancias { get; set; }
        public string CuentaEgresoMercancias { get; set; }
        public bool NotificacionVoz { get; set; }
        public bool ControlTarifaServicios { get; set; }
        public int? IdMotivoUbicReabasto { get; set; }
        public bool? OperadorDefectoEnDocumentoIngreso { get; set; }
        public bool? EsBodegaFiscal { get; set; }
        public bool? HabilitarIngresoConsolidado { get; set; }
        public bool? BloquearLpHh { get; set; }
        public bool CapturaEstibaIngreso { get; set; }
        public bool CapturaPalletNoEstandar { get; set; }
        public double? ValorPorcentajeIva { get; set; }
        public bool? PermitirVerificacionConsolidada { get; set; }
        public bool? ControlBanderasCliente { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual Paise IdPaisNavigation { get; set; }
        public virtual ICollection<BodegaArea> BodegaAreas { get; set; }
        public virtual ICollection<BodegaMonitorParametro> BodegaMonitorParametros { get; set; }
        public virtual ICollection<BodegaMuelle> BodegaMuelles { get; set; }
        public virtual ICollection<ClienteBodega> ClienteBodegas { get; set; }
        public virtual ICollection<EmpresaTransporteBodega> EmpresaTransporteBodegas { get; set; }
        public virtual ICollection<HorarioLaboralEnc> HorarioLaboralEncs { get; set; }
        public virtual ICollection<INavConfigEnc> INavConfigEncs { get; set; }
        public virtual ICollection<JornadaLaboral> JornadaLaborals { get; set; }
        public virtual ICollection<MotivoAnulacionBodega> MotivoAnulacionBodegas { get; set; }
        public virtual ICollection<MotivoDevolucionBodega> MotivoDevolucionBodegas { get; set; }
        public virtual ICollection<OperadorBodega> OperadorBodegas { get; set; }
        public virtual ICollection<ProductoBodega> ProductoBodegas { get; set; }
        public virtual ICollection<PropietarioBodega> PropietarioBodegas { get; set; }
        public virtual ICollection<ProveedorBodega> ProveedorBodegas { get; set; }
        public virtual ICollection<RolBodega> RolBodegas { get; set; }
        public virtual ICollection<TareaHh> TareaHhs { get; set; }
        public virtual ICollection<TransDespachoEnc> TransDespachoEncs { get; set; }
        public virtual ICollection<TransMovimiento> TransMovimientos { get; set; }
        public virtual ICollection<TransPeEnc> TransPeEncs { get; set; }
        public virtual ICollection<TransPickingEnc> TransPickingEncs { get; set; }
        public virtual ICollection<TransServicioEnc> TransServicioEncs { get; set; }
        public virtual ICollection<TransTrasEnc> TransTrasEncIdBodegaDestinoNavigations { get; set; }
        public virtual ICollection<TransTrasEnc> TransTrasEncIdBodegaOrigenNavigations { get; set; }
        public virtual ICollection<Turno> Turnos { get; set; }
        public virtual ICollection<UsuarioBodega> UsuarioBodegas { get; set; }
    }
}
