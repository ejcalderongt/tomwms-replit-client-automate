namespace WMSWebAPI.Dtos.Picking
{
    public class PickingDetDto
    {
        public int IdPickingDet { get; set; } = 0;
        public int IdPickingEnc { get; set; } = 0;
        public int IdPedidoEnc { get; set; } = 0;
        public int IdPedidoDet { get; set; } = 0;
        public int IdOperadorBodega { get; set; } = 0;
        public double Cantidad { get; set; } = 0;
        public int Cliente_dias { get; set; } = 0;
        public double Cantidad_recibida { get; set; } = 0;
        public string User_agr { get; set; } = string.Empty;
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = string.Empty;
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = false;
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
    }
}
