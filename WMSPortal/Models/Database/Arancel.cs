using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Arancel
    {
        public Arancel()
        {
            Productos = new HashSet<Producto>();
            TransOcDets = new HashSet<TransOcDet>();
        }

        public int IdArancel { get; set; }
        public string Nombre { get; set; }
        public double? Porcentaje { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecMod { get; set; }
        public string UserMod { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
        public virtual ICollection<TransOcDet> TransOcDets { get; set; }
    }
}
