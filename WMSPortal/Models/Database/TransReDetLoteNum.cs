using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransReDetLoteNum
    {
        public int IdLoteNum { get; set; }
        public int? IdRecepcionEnc { get; set; }
        public int? IdProductoBodega { get; set; }
        public string Codigo { get; set; }
        public string Lote { get; set; }
        public int? LoteNumerico { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public double? Cantidad { get; set; }
    }
}
