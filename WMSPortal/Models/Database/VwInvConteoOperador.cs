using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwInvConteoOperador
    {
        public int Idinventarioenc { get; set; }
        public string Codigo { get; set; }
        public string Producto { get; set; }
        public string Bodega { get; set; }
        public string Propietario { get; set; }
        public string EstadoProducto { get; set; }
        public string Presentacion { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaVence { get; set; }
        public double CantidadConteo { get; set; }
        public double? PesoConteo { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string Operador { get; set; }
        public int IdStock { get; set; }
        public double? CantidadStock { get; set; }
        public double? PesoStock { get; set; }
        public string Umbas { get; set; }
    }
}
