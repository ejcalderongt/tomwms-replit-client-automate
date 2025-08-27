using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransInventarioDet
    {
        public int IdInventarioDet { get; set; }
        public long IdInventarioEnc { get; set; }
        public int? IdStock { get; set; }
        public int IdProducto { get; set; }
        public int IdUbicacion { get; set; }
        public int? IdEstado { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedida { get; set; }
        public string Lote { get; set; }
        public int LicPlate { get; set; }
        public string Serial { get; set; }
        public double? Cantidad { get; set; }
        public double? Peso { get; set; }
        public double? Conteo { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaVence { get; set; }
        public string UbicacionAnt { get; set; }
        public string NoBulto { get; set; }
        public double? Recuento { get; set; }
        public bool? Inicial { get; set; }

        public virtual TransInventarioEnc IdInventarioEncNavigation { get; set; }
        public virtual Stock IdStockNavigation { get; set; }
    }
}
