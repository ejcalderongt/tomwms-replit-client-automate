namespace WMS.EntityCore.Dtos.KPI
{
    public class KpiStockResRowDto
    {
        public int IDBODEGA { get; set; }
        public string BODEGA { get; set; } = "";
        public string PROPIETARIO { get; set; } = "";

        public string CODIGO { get; set; } = "";
        public string NOMBRE { get; set; } = "";

        public string UnidadMedida { get; set; } = "";
        public string PRESENTACION { get; set; } = "";

        public string LOTE { get; set; } = "";
        public DateTime? fecha_ingreso { get; set; }
        public DateTime? fecha_vence { get; set; }

        public decimal Disponible_UMBas { get; set; }
        public decimal Disponible_Presentacion { get; set; }

        public string ESTADO { get; set; } = "";
        public string LICENCIA { get; set; } = "";

        public string FAMILIA { get; set; } = "";
        public string AREA { get; set; } = "";
        public string CLASIFICACION { get; set; } = "";

        public string UBICACION { get; set; } = "";
    }
}