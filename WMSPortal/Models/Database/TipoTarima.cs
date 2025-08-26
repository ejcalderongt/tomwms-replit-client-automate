using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TipoTarima
    {
        public TipoTarima()
        {
            ProductoPresentacionTarimas = new HashSet<ProductoPresentacionTarima>();
            Tarimas = new HashSet<Tarima>();
        }

        public int IdTipoTarima { get; set; }
        public string Nombre { get; set; }
        public double? Alto { get; set; }
        public double? Largo { get; set; }
        public double? Ancho { get; set; }
        public double? CargaDinamica { get; set; }
        public double? CargaEstatica { get; set; }
        public double? CargaEstanterias { get; set; }
        public double? EntradasTransPaleta { get; set; }
        public double? PesoPromedio { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
        public double? Tara { get; set; }

        public virtual ICollection<ProductoPresentacionTarima> ProductoPresentacionTarimas { get; set; }
        public virtual ICollection<Tarima> Tarimas { get; set; }
    }
}
