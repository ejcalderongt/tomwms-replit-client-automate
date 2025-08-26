using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TmsTicket
    {
        public int IdTicket { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdPropietario { get; set; }
        public int? IdUbicacionDestino { get; set; }
        public int? IdPiloto { get; set; }
        public int? IdVehiculo { get; set; }
        public int? IdEmpresaTransporte { get; set; }
        public string TipoOperacion { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaSalida { get; set; }
        public string Estado { get; set; }
        public string NoPoliza { get; set; }
        public string NoPlaca { get; set; }
        public string NoDocumentoPiloto { get; set; }
        public string TipoDocumentoPiloto { get; set; }
        public string NombresPiloto { get; set; }
        public string ApellidosPiloto { get; set; }
        public string NoTc { get; set; }
        public DateTime? FechaAsignado { get; set; }
        public DateTime? FechaProcesado { get; set; }
        public bool? ProcesadoStockJornada { get; set; }
        public DateTime? FechaProcesadoStockJornada { get; set; }

        public virtual EmpresaTransporte IdEmpresaTransporteNavigation { get; set; }
        public virtual EmpresaTransportePiloto IdPilotoNavigation { get; set; }
        public virtual Propietario IdPropietarioNavigation { get; set; }
        public virtual EmpresaTransporteVehiculo IdVehiculoNavigation { get; set; }
    }
}
