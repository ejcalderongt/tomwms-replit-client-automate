#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TipoTareaTiempo
    {
        public int IdEmpresa { get; set; }
        public int IdBodega { get; set; }
        public int IdTipoTarea { get; set; }
        public double? TiempoMedioMinutos { get; set; }
    }
}
