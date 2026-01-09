namespace WMSWebAPI.Dtos.Stock
{
    public class StockRes_3plDto
    {
        public int IdStockRes { get; set; } = 0;

        public int IdTransaccion { get; set; } = 0;

        public string Indicador { get; set; } = "";

        public int IdPedidoDet { get; set; } = 0;

        public int IdStock { get; set; } = 0;

        public int IdPropietarioBodega { get; set; } = 0;

        public int IdProductoBodega { get; set; } = 0;

        public int IdProductoEstado { get; set; } = 0;

        public int IdPresentacion { get; set; } = 0;

        public int IdUnidadMedida { get; set; } = 0;

        public int IdUbicacion { get; set; } = 0;

        public string Ubicacion_ant { get; set; } = "";

        public int IdRecepcion { get; set; } = 0;

        public string Lote { get; set; } = "";

        public string Lic_plate { get; set; } = "";

        public string Serial { get; set; } = "";

        public double Cantidad { get; set; } = 0;

        public double Peso { get; set; } = 0;

        public string Estado { get; set; } = "";

        public DateTime Fecha_ingreso { get; set; } = DateTime.Now;

        public DateTime Fecha_vence { get; set; } = DateTime.Now;

        public double Uds_lic_plate { get; set; } = 0;

        public int No_bulto { get; set; } = 0;

        public int IdPicking { get; set; } = 0;

        public int IdPedido { get; set; } = 0;

        public int IdDespacho { get; set; } = 0;

        public string User_agr { get; set; } = "";

        public DateTime Fec_agr { get; set; } = DateTime.Now;

        public string User_mod { get; set; } = "";

        public DateTime Fec_mod { get; set; } = DateTime.Now;

        public string Host { get; set; } = "";

        public int Añada { get; set; } = 0;

        public DateTime Fecha_manufactura { get; set; } = DateTime.Now;

        public int IdBodega { get; set; } = 0;

        public bool Pallet_no_estandar { get; set; } = false;

        public string Atributo_Variante_1 { get; set; } = "";

        public bool Control_Ultimo_Lote { get; set; } = false;

        public string? Ultimo_Lote { get; set; } = "";

        public string Codigo_Producto { get; set; } = "";

        public string No_Pedido { get; set; } = "";

        public int IdUbicacionAbastecerCon { get; set; } = 0;
    }


}
