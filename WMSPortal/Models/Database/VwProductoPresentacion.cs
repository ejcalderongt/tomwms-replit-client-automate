using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwProductoPresentacion
    {
        public string Producto { get; set; }
        public int IdPresentacion { get; set; }
        public int IdProducto { get; set; }
        public string CodigoBarra { get; set; }
        public string Nombre { get; set; }
        public bool? ImprimeBarra { get; set; }
        public double? Peso { get; set; }
        public double? Alto { get; set; }
        public double? Largo { get; set; }
        public double? Ancho { get; set; }
        public double Factor { get; set; }
        public double? MinimoExistencia { get; set; }
        public double? MaximoExistencia { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
        public bool? EsPallet { get; set; }
        public double? Precio { get; set; }
        public double? MinimoPeso { get; set; }
        public double? MaximoPeso { get; set; }
        public double? Costo { get; set; }
        public double? CamasPorTarima { get; set; }
        public double? CajasPorCama { get; set; }
        public bool? GeneraLpAuto { get; set; }
        public bool? PermitirPaletizar { get; set; }
        public bool? Sistema { get; set; }
        public int? IdPresentacionPallet { get; set; }
        public int IdProductoBodega { get; set; }
        public int? IdBodega { get; set; }
    }
}
