using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ProductoKitComposicion
    {
        public int IdProductoKitComposicion { get; set; }
        public int IdProductoPadre { get; set; }
        public int IdProductoHijo { get; set; }
        public int? IdUnidadMedidaBasicaPadre { get; set; }
        public int? IdUnidadMedidaBasicaHijo { get; set; }
        public double? Cantidad { get; set; }
        public DateTime? FechaAgr { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FechaMod { get; set; }
        public string UserMod { get; set; }
        public int? IdBodega { get; set; }
    }
}
