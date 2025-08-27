using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwOrdenCompraPreIngreso
    {
        public DateTime? FechaCreacion { get; set; }
        public string Proveedor { get; set; }
        public string NoDocumento { get; set; }
        public int? NoLinea { get; set; }
        public string Codigo { get; set; }
        public string Producto { get; set; }
        public double? Cantidad { get; set; }
        public string CodigoBarra { get; set; }
        public int? IdSimbologia { get; set; }
        public int IdOrdenCompraEnc { get; set; }
        public int IdOrdenCompraDet { get; set; }
        public int IdProveedor { get; set; }
        public string MotivoDevolucion { get; set; }
    }
}
