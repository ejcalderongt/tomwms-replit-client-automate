namespace WMSWebAPI.Dtos.Catalogos
{
    public class ProveedorBodegaDto
    {
        public int IdAsignacion { get; set; } = 0;
        public int? IdProveedor { get; set; } = 0;
        public int? IdBodega { get; set; }= 0;
        public bool? Activo { get; set; } = true;
        public string? User_Agr { get; set; } = "";
        public DateTime? Fec_Agr { get; set; } = new DateTime(1900, 1, 1);
        public string? User_Mod { get; set; } = "";
        public DateTime? Fec_Mod { get; set; }= new DateTime(1900, 1, 1);
        public int? IdAreaOrigen { get; set; }= 0;
        public ProveedorDto Proveedor { get; set; } = new ProveedorDto();
    }
}