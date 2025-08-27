using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwPedido
    {
        public int Pedido { get; set; }
        public string Referencia { get; set; }
        public int IdPedidoDet { get; set; }
        public string Código { get; set; }
        public string Producto { get; set; }
        public string Presentacion { get; set; }
        public string UnidadMedida { get; set; }
        public string Estado { get; set; }
        public double? Cantidad { get; set; }
        public DateTime? Fecha { get; set; }
        public int IdProducto { get; set; }
        public int? IdPickingEnc { get; set; }
        public string EstadoPedido { get; set; }
        public int? IdBodega { get; set; }
        public double? CantidadPickeada { get; set; }
        public double? CantidadVerificada { get; set; }
        public double? CantidadDespachada { get; set; }
        public int? IdPropietarioBodega { get; set; }
    }
}
