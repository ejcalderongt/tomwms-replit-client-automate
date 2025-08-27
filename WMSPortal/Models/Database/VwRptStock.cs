using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwRptStock
    {
        public int IdStock { get; set; }
        public string Propietario { get; set; }
        public string Producto { get; set; }
        public string Estado { get; set; }
        public string Presentacion { get; set; }
        public string UnidadMedida { get; set; }
        public string Lote { get; set; }
        public string Serial { get; set; }
        public double? CantPresentación { get; set; }
        public double? CantUMBas { get; set; }
        public double? Peso { get; set; }
        public DateTime? FechaManufactura { get; set; }
        public DateTime? FechaVence { get; set; }
        public string NoSerie { get; set; }
        public string NoSerieInicial { get; set; }
        public string NoSerieFinal { get; set; }
    }
}
