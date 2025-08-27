using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwServicio
    {
        public int IdBodega { get; set; }
        public string Almacen { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public int IdPropietarioEnc { get; set; }
        public string NoOrden { get; set; }
        public string TipoTransaccion { get; set; }
        public int? NoLinea { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public int? Cantidad { get; set; }
        public DateTime? FechaServicio { get; set; }
    }
}
