using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class RoadPVendedor
    {
        public int IdRuta { get; set; }
        public int IdVendedor { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string Ruta { get; set; }
        public int? Nivel { get; set; }
        public int? Nivelprecio { get; set; }
        public string Bodega { get; set; }
        public string Subbodega { get; set; }
        public string CodVehiculo { get; set; }
        public string Liquidando { get; set; }
        public DateTime UltimaFechaLiq { get; set; }
        public bool Bloqueado { get; set; }
        public int DevolucionSap { get; set; }
    }
}
