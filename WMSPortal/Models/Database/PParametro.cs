using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class PParametro
    {
        public PParametro()
        {
            ProductoParametros = new HashSet<ProductoParametro>();
        }

        public int IdParametro { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string ValorTexto { get; set; }
        public double? ValorNumerico { get; set; }
        public DateTime? ValorFecha { get; set; }
        public bool? ValorLogico { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }

        public virtual ICollection<ProductoParametro> ProductoParametros { get; set; }
    }
}
