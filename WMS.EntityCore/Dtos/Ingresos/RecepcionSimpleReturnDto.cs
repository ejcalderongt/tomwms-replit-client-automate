namespace WMSWebAPI.Dtos.Ingresos
{
    public class RecepcionSimpleReturnDto
    {
        public int Idtransaccion { get; set; }
        public string No_pedido { get; set; } = "";
        public string Codigo_producto { get; set; } = "";
        public string Nombre_producto { get; set; } = "";
        public string UM { get; set; } = "";
        public int Linea { get; set; } = 0;
        public double Cantidad { get; set; } = 0;
        public string Licencia { get; set; } = "";
        public string Lote { get; set; } = "";
        public DateTime Fecha { get; set; }= DateTime.Now;
        public DateTime Vence { get; set; } = new DateTime(1900, 1, 1);
        public string Presentacion { get; set; } = "";
    }
}
