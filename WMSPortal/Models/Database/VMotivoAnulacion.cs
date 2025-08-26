#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VMotivoAnulacion
    {
        public int? IdBodega { get; set; }
        public string Bodega { get; set; }
        public int? IdMotivoAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }
        public int IdMotivoAnulacionBodega { get; set; }
        public bool? Activo { get; set; }
    }
}
