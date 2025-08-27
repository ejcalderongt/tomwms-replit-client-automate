using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class BodegaMuelle
    {
        public BodegaMuelle()
        {
            TareaHhs = new HashSet<TareaHh>();
            TransPeEncs = new HashSet<TransPeEnc>();
            TransReEncs = new HashSet<TransReEnc>();
            TransTrasEncs = new HashSet<TransTrasEnc>();
        }

        public int IdMuelle { get; set; }
        public int IdBodega { get; set; }
        public string CodigoBarra { get; set; }
        public string Nombre { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public int? Color { get; set; }
        public byte[] Imagen { get; set; }
        public bool? Activo { get; set; }
        public bool? Entrada { get; set; }
        public bool? Salida { get; set; }

        public virtual Bodega IdBodegaNavigation { get; set; }
        public virtual ICollection<TareaHh> TareaHhs { get; set; }
        public virtual ICollection<TransPeEnc> TransPeEncs { get; set; }
        public virtual ICollection<TransReEnc> TransReEncs { get; set; }
        public virtual ICollection<TransTrasEnc> TransTrasEncs { get; set; }
    }
}
