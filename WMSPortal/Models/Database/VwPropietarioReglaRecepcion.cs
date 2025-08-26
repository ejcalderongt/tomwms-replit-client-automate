#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwPropietarioReglaRecepcion
    {
        public int Código { get; set; }
        public string Regla { get; set; }
        public string Propietario { get; set; }
        public string Mensaje { get; set; }
        public bool? Activo { get; set; }
        public int? IdPropietario { get; set; }
        public int? IdReglaRecepcion { get; set; }
        public bool? Rechazar { get; set; }
        public bool? StockNoDisponible { get; set; }
        public string Descripcion { get; set; }
    }
}
