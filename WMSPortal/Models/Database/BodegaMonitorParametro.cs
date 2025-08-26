#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class BodegaMonitorParametro
    {
        public int IdMonitor { get; set; }
        public int? IdBodega { get; set; }
        public string Nombre { get; set; }
        public int? TiempoActualizacion { get; set; }

        public virtual Bodega IdBodegaNavigation { get; set; }
    }
}
