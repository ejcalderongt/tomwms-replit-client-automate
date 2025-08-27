#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavAcuerdoDet
    {
        public int IdAcuerdoDet { get; set; }
        public int IdAcuerdo { get; set; }
        public string CodigoProducto { get; set; }
        public string Servicio { get; set; }
        public int CodMoneda { get; set; }
        public string Nemonico { get; set; }
        public int? CorreDetalleacuerdo { get; set; }
        public int? CorreCatalogoproductos { get; set; }
        public int? UnidMedida { get; set; }
        public string NombreUnidad { get; set; }
        public bool ProcesadoWms { get; set; }
        public string Estado { get; set; }
    }
}
