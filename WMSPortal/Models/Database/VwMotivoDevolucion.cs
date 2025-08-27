using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwMotivoDevolucion
    {
        public string Empresa { get; set; }
        public string Propietario { get; set; }
        public int IdMotivoDevolucion { get; set; }
        public int IdEmpresa { get; set; }
        public int? IdPropietario { get; set; }
        public string Nombre { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool Activo { get; set; }
        public bool EsDetalle { get; set; }
    }
}
