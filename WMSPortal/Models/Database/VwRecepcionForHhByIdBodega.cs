#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwRecepcionForHhByIdBodega
    {
        public int? IdBodega { get; set; }
        public int N { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int? IdPropietario { get; set; }
        public string Propietario { get; set; }
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public string NoDocumento { get; set; }
        public string MotivoDevolucion { get; set; }
        public string Tipo { get; set; }
        public string Referencia { get; set; }
        public int IdOrdenCompraEnc { get; set; }
        public string TipoTrans { get; set; }
    }
}
