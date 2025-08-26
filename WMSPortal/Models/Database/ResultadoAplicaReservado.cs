#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ResultadoAplicaReservado
    {
        public int IdStock { get; set; }
        public double? CantidadStock { get; set; }
        public double? CantidadRes { get; set; }
        public double? CantidadF { get; set; }
    }
}
