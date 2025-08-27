using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwTareasPickingHh
    {
        public int IdPickingEnc { get; set; }
        public int IdBodega { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public int? IdUbicacionPicking { get; set; }
        public DateTime? FechaPicking { get; set; }
        public DateTime? HoraIni { get; set; }
        public DateTime? HoraFin { get; set; }
        public string Estado { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? DetalleOperador { get; set; }
        public bool? Activo { get; set; }
        public string NombreBodega { get; set; }
        public string NombreComercial { get; set; }
        public string NombreUbicacion { get; set; }
        public int IdOperadorBodega { get; set; }
    }
}
