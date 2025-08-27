using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ReglaUbicEnc
    {
        public ReglaUbicEnc()
        {
            ReglaUbicDetIrs = new HashSet<ReglaUbicDetIr>();
            ReglaUbicDetPes = new HashSet<ReglaUbicDetPe>();
            ReglaUbicDetPps = new HashSet<ReglaUbicDetPp>();
            ReglaUbicDetProps = new HashSet<ReglaUbicDetProp>();
            ReglaUbicDetTps = new HashSet<ReglaUbicDetTp>();
            ReglaUbicDetTrs = new HashSet<ReglaUbicDetTr>();
            ReglaUbicacions = new HashSet<ReglaUbicacion>();
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

        public virtual ICollection<ReglaUbicDetIr> ReglaUbicDetIrs { get; set; }
        public virtual ICollection<ReglaUbicDetPe> ReglaUbicDetPes { get; set; }
        public virtual ICollection<ReglaUbicDetPp> ReglaUbicDetPps { get; set; }
        public virtual ICollection<ReglaUbicDetProp> ReglaUbicDetProps { get; set; }
        public virtual ICollection<ReglaUbicDetTp> ReglaUbicDetTps { get; set; }
        public virtual ICollection<ReglaUbicDetTr> ReglaUbicDetTrs { get; set; }
        public virtual ICollection<ReglaUbicacion> ReglaUbicacions { get; set; }
    }
}
