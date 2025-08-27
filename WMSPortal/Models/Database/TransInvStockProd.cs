using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransInvStockProd
    {
        public int Idinventario { get; set; }
        public int Idinvstockprod { get; set; }
        public int IdProducto { get; set; }
        public int IdPresentacion { get; set; }
        public double? Cant { get; set; }
        public double? Peso { get; set; }
        public int? IdUnidadMedida { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaVence { get; set; }
        public string Codigo { get; set; }
    }
}
