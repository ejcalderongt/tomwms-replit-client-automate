namespace WMSWebAPI.Dtos.Catalogos
{
    public class ProveedorBodegaDto
    {
        public int IdAsignacion { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdBodega { get; set; }
        public bool? Activo { get; set; }

        public string? User_Agr { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public string? User_Mod { get; set; }
        public DateTime? Fec_Mod { get; set; }

        public int? IdAreaOrigen { get; set; }
    }
}