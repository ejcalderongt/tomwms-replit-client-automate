using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class CuadrillaDetOperador
    {
        public int IdCuadrillaDet { get; set; }
        public int? IdCuadrillaEnc { get; set; }
        public int? IdOperadorBodega { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool Activo { get; set; }
    }
}
