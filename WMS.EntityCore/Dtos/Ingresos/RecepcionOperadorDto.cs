namespace WMSWebAPI.Dtos.Ingresos
{
    public class RecepcionOperadorDto
    {
        public int IdOperadorRec { get; set; }
        public int IdRecepcionEnc { get; set; }
        public int? IdOperadorBodega { get; set; }
        public string? User_Agr { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public string? User_Mod { get; set; }
        public DateTime? Fec_Mod { get; set; }
        public bool IsNew { get; set; } = true;
        public bool UsaHH { get; set; }
    }
}