using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwTransOcDet
    {
        public int IdOrdenCompraEnc { get; set; }
        public string NoDocumento { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? NoLinea { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public double? Cantidad { get; set; }
        public double? CantidadRecibida { get; set; }
        public double? CantidadPendiente { get; set; }
        public string Um { get; set; }
    }
}
