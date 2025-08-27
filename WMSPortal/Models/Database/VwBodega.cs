#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwBodega
    {
        public int Correlativo { get; set; }
        public string Pais { get; set; }
        public string Empresa { get; set; }
        public string Código { get; set; }
        public string Nombre { get; set; }
        public string NombreComercial { get; set; }
        public string Responsable { get; set; }
        public bool? Activo { get; set; }
    }
}
