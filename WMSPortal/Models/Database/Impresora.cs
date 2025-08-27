using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Impresora
    {
        public Impresora()
        {
            ProductoPallets = new HashSet<ProductoPallet>();
        }

        public int IdImpresora { get; set; }
        public int? IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public string DireccionIp { get; set; }
        public string UserAgr { get; set; }
        public DateTime FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime FecMod { get; set; }
        public bool Activo { get; set; }
        public string MacAdress { get; set; }
        public int? IdBodega { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual ICollection<ProductoPallet> ProductoPallets { get; set; }
    }
}
