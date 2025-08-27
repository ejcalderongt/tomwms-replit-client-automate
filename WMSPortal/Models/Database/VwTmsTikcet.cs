using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwTmsTikcet
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
        public string NombrePiloto { get; set; }
        public string ApellidosPiloto { get; set; }
        public string PlacaVehiculo { get; set; }
        public string PlacaTc { get; set; }
        public string EmpresaTransporte { get; set; }
    }
}
