using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwStockPresentacione
    {
        public int? IdPresentacion { get; set; }
        public string Nombre { get; set; }
        public int IdProductoBodega { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int? IdPropietario { get; set; }
        public int? IdProducto { get; set; }
        public string CodigoBarra { get; set; }
        public bool? ImprimeBarra { get; set; }
        public double? Peso { get; set; }
        public double? Alto { get; set; }
        public double? Largo { get; set; }
        public double? Ancho { get; set; }
        public double Factor { get; set; }
        public int? MinimoExistencia { get; set; }
        public int? MaximoExistencia { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
    }
}
