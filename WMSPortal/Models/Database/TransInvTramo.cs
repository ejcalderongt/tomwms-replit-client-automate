using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransInvTramo
    {
        public int Idinventario { get; set; }
        public int Idtramo { get; set; }
        public int? DetIdoperador { get; set; }
        public string DetEstado { get; set; }
        public DateTime? DetInicio { get; set; }
        public DateTime? DetFin { get; set; }
        public int? ResIdoperador { get; set; }
        public string ResEstado { get; set; }
        public DateTime? ResInicio { get; set; }
        public DateTime? ResFin { get; set; }
        public bool? Aplicado { get; set; }
        public int? IdBodega { get; set; }

        public virtual TransInvEnc IdinventarioNavigation { get; set; }
    }
}
