namespace WMSWebAPI.Dtos.Catalogos
{
    public class PropietarioBodegaDto
    {
        public int IdPropietarioBodega { get; set; }
        public int? IdPropietario { get; set; }
        public int? IdBodega { get; set; }
        public string? User_Agr { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public string? User_Mod { get; set; }
        public DateTime? Fec_Mod { get; set; }

        public bool? Activo { get; set; }
    }
}