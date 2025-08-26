namespace WMSWebAPI.Dtos.Catalogos
{
    public class ProductoParametroADto
    {
        public int IdProductoParametroA { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public string? User_Agr { get; set; }
        public DateTime? Fec_Mod { get; set; }
        public string? User_Mod { get; set; }
        public bool? Activo { get; set; }
    }
}