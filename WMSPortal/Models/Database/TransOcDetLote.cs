using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransOcDetLote
    {
        public int IdOrdenCompraEnc { get; set; }
        public int IdOrdenCompraDet { get; set; }
        public int? IdOrdenCompraDetLote { get; set; }
        public int IdProductoBodega { get; set; }
        public int? NoLinea { get; set; }
        public string CodigoProducto { get; set; }
        public double? Cantidad { get; set; }
        public double? CantidadRecibida { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaVence { get; set; }
        public string LicPlate { get; set; }
        public string Ubicacion { get; set; }
    }
}
