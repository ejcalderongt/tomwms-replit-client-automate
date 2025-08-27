using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransInvResuman
    {
        public int Idinventariores { get; set; }
        public int Idinventarioenct { get; set; }
        public int Idtramo { get; set; }
        public int Idproducto { get; set; }
        public int? Idoperador { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? Idpresentacion { get; set; }
        public int? Idproductoestado { get; set; }
        public double? Cantidad { get; set; }
        public DateTime? FechaCaptura { get; set; }
        public string Host { get; set; }
        public string NomProducto { get; set; }
        public string NomOperador { get; set; }

        public virtual TransInvEnc IdinventarioenctNavigation { get; set; }
    }
}
