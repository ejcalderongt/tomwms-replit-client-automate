namespace WMSWebAPI.Dtos.Operador
{
    public class OperadorBodegaDto
    {
        public int IdOperadorBodega { get; set; }
        public int IdOperador { get; set; }
        public int IdBodega { get; set; }
        public bool Activo { get; set; }
        public string User_agr { get; set; } = string.Empty;
        public DateTime Fec_agr { get; set; }
        public string User_mod { get; set; } = string.Empty;
        public DateTime Fec_mod { get; set; }
    }
}
