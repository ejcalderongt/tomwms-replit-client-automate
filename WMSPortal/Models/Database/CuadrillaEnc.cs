using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class CuadrillaEnc
    {
        public int IdCuadrillaEnc { get; set; }
        public int? IdTipoCuadrilla { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdBodega { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool Activo { get; set; }
    }
}
