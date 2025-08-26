using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwStockReservadoByIdPedidoEnc
    {
        public long? IdPedido { get; set; }
        public int IdStockRes { get; set; }
        public string Codigo { get; set; }
        public string Producto { get; set; }
        public string Estado { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaVence { get; set; }
        public double? Cantidad { get; set; }
        public double? CantidadPresentacion { get; set; }
        public double? Peso { get; set; }
        public int IdUbicacion { get; set; }
        public string NombreCompleto { get; set; }
        public string Umbas { get; set; }
        public string Presentacion { get; set; }
        public string LicPlate { get; set; }
    }
}
