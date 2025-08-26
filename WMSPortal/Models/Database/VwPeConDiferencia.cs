using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwPeConDiferencia
    {
        public long? Ordenpedido { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public double? Cantidad { get; set; }
        public double? CantDespachada { get; set; }
        public string Presentacion { get; set; }
        public double? Diferencia { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public string Bodega { get; set; }
        public string Propietario { get; set; }
        public int? IdTipoPedido { get; set; }
        public string NombrePedido { get; set; }
        public int IdProductoBodega { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedidaBasica { get; set; }
        public string Umbas { get; set; }
        public string Estado { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FechaPedido { get; set; }
        public int IdPropietario { get; set; }
        public int? IdBodega { get; set; }
    }
}
