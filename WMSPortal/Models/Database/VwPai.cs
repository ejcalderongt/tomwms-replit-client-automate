#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwPai
    {
        public int IdPais { get; set; }
        public string NombrePais { get; set; }
        public int? IdRegion { get; set; }
        public string NombreRegion { get; set; }
        public int? IdDepartamento { get; set; }
        public string NombreDepartamento { get; set; }
        public int? IdMunicipio { get; set; }
        public string NombreMunicipio { get; set; }
    }
}
