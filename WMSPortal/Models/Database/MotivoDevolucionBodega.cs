using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class MotivoDevolucionBodega
    {
        public int IdMotivoDevolucionBodega { get; set; }
        public int? IdMotivoDevolucion { get; set; }
        public int? IdBodega { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }

        public virtual Bodega IdBodegaNavigation { get; set; }
        public virtual MotivoDevolucion IdMotivoDevolucionNavigation { get; set; }
    }
}
