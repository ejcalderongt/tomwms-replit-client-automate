#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwExistenciasProductoCategorium
    {
        public string Tipo { get; set; }
        public string Clasificacion { get; set; }
        public int IdProducto { get; set; }
        public string Codigo { get; set; }
        public string Producto { get; set; }
        public double? ExistenciaMin { get; set; }
        public double? ExistenciaMax { get; set; }
        public double Existencia { get; set; }
    }
}
