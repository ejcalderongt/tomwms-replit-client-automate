using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransDespachoEnc
    {
        public TransDespachoEnc()
        {
            TransDespachoDets = new HashSet<TransDespachoDet>();
        }

        public int IdDespachoEnc { get; set; }
        public int IdBodega { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public int? IdVehiculo { get; set; }
        public int? IdPiloto { get; set; }
        public int? IdRuta { get; set; }
        public DateTime? Fecha { get; set; }
        public int? NoPase { get; set; }
        public string Observacion { get; set; }
        public DateTime? HoraIni { get; set; }
        public DateTime? HoraFin { get; set; }
        public string Estado { get; set; }
        public int? Numero { get; set; }
        public string Marchamo { get; set; }
        public double? CantBultos { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }

        public virtual Bodega IdBodegaNavigation { get; set; }
        public virtual EmpresaTransportePiloto IdPilotoNavigation { get; set; }
        public virtual PropietarioBodega IdPropietarioBodegaNavigation { get; set; }
        public virtual RoadRutum IdRutaNavigation { get; set; }
        public virtual EmpresaTransporteVehiculo IdVehiculoNavigation { get; set; }
        public virtual ICollection<TransDespachoDet> TransDespachoDets { get; set; }
    }
}
