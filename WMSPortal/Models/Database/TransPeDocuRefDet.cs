#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransPeDocuRefDet
    {
        public int IdDocumentoRef { get; set; }
        public int IdDocumentoRefDet { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public double? Cantidad { get; set; }
        public double? Peso { get; set; }
        public string Umbas { get; set; }
        public string Presentaacion { get; set; }
    }
}
