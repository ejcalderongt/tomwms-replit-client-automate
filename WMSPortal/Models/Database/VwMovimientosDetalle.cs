using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwMovimientosDetalle
    {
        public string Propietario { get; set; }
        public string Poliza { get; set; }
        public string Producto { get; set; }
        public string Presentación { get; set; }
        public string EstadoOrigen { get; set; }
        public string EstadoDestino { get; set; }
        public string UnidadDeMedida { get; set; }
        public double? Cantidad { get; set; }
        public double? Peso { get; set; }
        public string Lote { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string TipoTarea { get; set; }
        public int? IdBodegaOrigen { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdProducto { get; set; }
        public string Codigo { get; set; }
        public string CodigoBarra { get; set; }
        public int? IdRecepcion { get; set; }
        public int? IdRecepcionOc { get; set; }
        public int? IdOrdenCompraEnc { get; set; }
    }
}
