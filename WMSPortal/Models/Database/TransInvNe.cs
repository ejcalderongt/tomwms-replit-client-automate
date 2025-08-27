using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransInvNe
    {
        public int Idinventarione { get; set; }
        public int Idinventarioenc { get; set; }
        public int? Idproducto { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public double? Cantidad { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UsrAgr { get; set; }
    }
}
