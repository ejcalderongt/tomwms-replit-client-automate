using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Tarima
    {
        public Tarima()
        {
            TransUbicTarimas = new HashSet<TransUbicTarima>();
        }

        public int IdTarima { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdTipoTarima { get; set; }
        public string Codigo { get; set; }
        public string UserAgr { get; set; }
        public DateTime FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
        public bool? Disponible { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual TipoTarima IdTipoTarimaNavigation { get; set; }
        public virtual ICollection<TransUbicTarima> TransUbicTarimas { get; set; }
    }
}
