namespace WMSWebAPI.Dtos.Picking
{
    public class PickingOpDto
    {
        public int IdOperadorPicking { get; set; } = 0;
        public int IdPickingEnc { get; set; } = 0;
        public int IdOperadorBodega { get; set; } = 0;
        public string User_agr { get; set; } = string.Empty;
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = string.Empty;
        public DateTime Fec_mod { get; set; } = DateTime.Now;
    }
}