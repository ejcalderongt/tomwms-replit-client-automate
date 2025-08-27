using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ReglaUbicSelEnc
    {
        public ReglaUbicSelEnc()
        {
            ReglaUbicSelDets = new HashSet<ReglaUbicSelDet>();
        }

        public int IdReglaUbicacionEnc { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdBodega { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }

        public virtual ICollection<ReglaUbicSelDet> ReglaUbicSelDets { get; set; }
    }
}
