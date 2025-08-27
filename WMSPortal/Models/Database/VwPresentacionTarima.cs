using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwPresentacionTarima
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string TipoTarima { get; set; }
        public int IdPresentacionTarima { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdTipoTarima { get; set; }
        public double? Cantidad { get; set; }
        public double? CantidadPorCama { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
    }
}
