using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransTrasEnc
    {
        public TransTrasEnc()
        {
            TransTrasDets = new HashSet<TransTrasDet>();
            TransTrasOps = new HashSet<TransTrasOp>();
        }

        public int IdTrasladoEnc { get; set; }
        public int? IdBodegaOrigen { get; set; }
        public int? IdBodegaDestino { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public int? IdMuelleOrigen { get; set; }
        public int? IdPiloto { get; set; }
        public int? IdVehiculo { get; set; }
        public int? IdRuta { get; set; }
        public DateTime? FechaTraslado { get; set; }
        public DateTime? HoraIni { get; set; }
        public DateTime? HoraFin { get; set; }
        public string Ubicacion { get; set; }
        public string Estado { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public long? NoDocumento { get; set; }
        public bool? Local { get; set; }
        public bool? PalletPrimero { get; set; }
        public bool? Anulado { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string Observacion { get; set; }
        public DateTime? HoraEntregaDesde { get; set; }
        public DateTime? HoraEntregaHasta { get; set; }
        public string NoGuia { get; set; }

        public virtual Bodega IdBodegaDestinoNavigation { get; set; }
        public virtual Bodega IdBodegaOrigenNavigation { get; set; }
        public virtual BodegaMuelle IdMuelleOrigenNavigation { get; set; }
        public virtual EmpresaTransportePiloto IdPilotoNavigation { get; set; }
        public virtual PropietarioBodega IdPropietarioBodegaNavigation { get; set; }
        public virtual RoadRutum IdRutaNavigation { get; set; }
        public virtual EmpresaTransporteVehiculo IdVehiculoNavigation { get; set; }
        public virtual ICollection<TransTrasDet> TransTrasDets { get; set; }
        public virtual ICollection<TransTrasOp> TransTrasOps { get; set; }
    }
}
