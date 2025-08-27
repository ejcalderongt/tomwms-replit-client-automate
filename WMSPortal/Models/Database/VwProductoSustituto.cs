#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwProductoSustituto
    {
        public int Código { get; set; }
        public int IdProductoOriginal { get; set; }
        public int? IdProductoPresentacionOriginal { get; set; }
        public int IdProductoReemplazo { get; set; }
        public int? IdProductoPresentacionReemplazo { get; set; }
        public string PresentaciónOriginal { get; set; }
        public string ProductoReemplazo { get; set; }
        public string PresentaciónReemplazo { get; set; }
        public bool? Activo { get; set; }
    }
}
