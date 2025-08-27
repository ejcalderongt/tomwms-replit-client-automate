using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavBarrasPallet
    {
        public int IdPallet { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int? CamasPorTarima { get; set; }
        public int? CajasPorCama { get; set; }
        public double? CantidadPresentacion { get; set; }
        public string UmProducto { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaAgregado { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaVence { get; set; }
        public DateTime? FechaProduccion { get; set; }
        public bool? Activo { get; set; }
        public bool? Recibido { get; set; }
        public int? IdRecepcion { get; set; }
        public string BodegaOrigen { get; set; }
        public string BodegaDestino { get; set; }
        public string CodigoBarra { get; set; }
        public double? CantidadUmp { get; set; }
        public double? LoteNumerico { get; set; }
    }
}
