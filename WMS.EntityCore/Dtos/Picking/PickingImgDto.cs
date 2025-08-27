namespace WMSWebAPI.Dtos.Picking
{
    public class PickingImgDto
    {
        public int IdImagen { get; set; } = 0;
        public int IdPickingEnc { get; set; } = 0;
        public int IdPickingDet { get; set; } = 0;
        public int IdPedidoEnc { get; set; } = 0;
        public int IdPedidoDet { get; set; } = 0;
        public byte[]? Imagen { get; set; } = Array.Empty<byte>();
        public string User_agr { get; set; } = string.Empty;
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string Observacion { get; set; } = string.Empty;
    }
}