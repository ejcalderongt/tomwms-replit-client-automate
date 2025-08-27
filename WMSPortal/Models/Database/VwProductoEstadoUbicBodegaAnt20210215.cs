using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwProductoEstadoUbicBodegaAnt20210215
    {
        public int IdEstado { get; set; }
        public int IdPropietario { get; set; }
        public string Nombre { get; set; }
        public int? IdUbicacionDefecto { get; set; }
        public bool? Utilizable { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Dañado { get; set; }
        public string CodigoBodegaErp { get; set; }
        public bool? Sistema { get; set; }
        public int? IdUbicacionBodegaDefecto { get; set; }
        public int? IdBodega { get; set; }
        public string NombreUbic { get; set; }
    }
}
