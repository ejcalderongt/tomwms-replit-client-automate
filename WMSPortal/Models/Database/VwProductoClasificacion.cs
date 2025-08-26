using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwProductoClasificacion
    {
        public int Expr1 { get; set; }
        public string Propietario { get; set; }
        public int IdClasificacion { get; set; }
        public int IdPropietario { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public bool? Sistema { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime FecMod { get; set; }
    }
}
