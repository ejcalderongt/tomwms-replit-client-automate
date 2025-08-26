using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class CuadrillaTipo
    {
        public int IdTipoCuadrilla { get; set; }
        public string Nombre { get; set; }
        public bool? EsRecepcion { get; set; }
        public bool? EsPicking { get; set; }
        public bool? EsVerificacion { get; set; }
        public bool? EsTransito { get; set; }
        public bool? EsInventario { get; set; }
        public bool? EsUbicacion { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecMod { get; set; }
        public string UserMod { get; set; }
    }
}
