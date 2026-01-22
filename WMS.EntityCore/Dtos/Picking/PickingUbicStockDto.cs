namespace WMSWebAPI.Dtos.Picking
{
    public class PickingUbicStockDto
    {
        public int IdPickingUbicStock { get; set; } = 0;
        public int IdBodega { get; set; } = 0;
        public int IdPickingUbic { get; set; } = 0;
        public int IdPickingDet { get; set; } = 0;
        public int IdUbicacion { get; set; } = 0;
        public int IdStock { get; set; } = 0;
        public int IdStockRes { get; set; } = 0;
        public int IdPropietarioBodega { get; set; } = 0;
        public int IdProductoBodega { get; set; } = 0;
        public int IdProductoEstado { get; set; } = 0;
        public int IdPresentacion { get; set; } = 0;
        public int IdUnidadMedida { get; set; } = 0;
        public int IdUbicacionAnterior { get; set; } = 0;
        public int IdRecepcion { get; set; } = 0;
        public int IdPedidoEnc { get; set; } = 0;
        public int IdPedidoDet { get; set; } = 0;
        public int Idpickingenc { get; set; } = 0;
        public int IdOperadorBodega { get; set; } = 0;
        public int IdOperadorBodega_Pickeo { get; set; } = 0;
        public int IdOperadorBodega_Verifico { get; set; } = 0;

        public string Lote { get; set; } = string.Empty;
        public DateTime Fecha_vence { get; set; } = DateTime.Now;
        public DateTime Fecha_minima { get; set; } = DateTime.Now;
        public string Serial { get; set; } = string.Empty;
        public string Licencia { get; set; } = string.Empty;
        public double Cantidad_recibida { get; set; } = 0;
        public double Cantidad_verificada { get; set; } = 0;
        public DateTime Fecha_picking { get; set; } = DateTime.Now;
        public DateTime Fecha_verificado { get; set; } = DateTime.Now;
        public DateTime Fecha_despachado { get; set; } = DateTime.Now;
        public double Cantidad_despachada { get; set; } = 0;
        public string User_agr { get; set; } = string.Empty;
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = string.Empty;
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = false;
        public int IdUbicacionTemporal { get; set; } = 0;
        public int IdOperadorBodega_Asignado { get; set; } = 0;
        public double Cantidad_pickeada { get; set; } = 0;
        public string Host { get; set; } = string.Empty;
        public int IdMovimiento { get; set; } = 0;
    }
}