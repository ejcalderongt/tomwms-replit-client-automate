namespace WMSWebAPI.Dtos.Catalogos
{
    public class ProductoBodegaDto
    {
        public int IdProductoBodega { get; set; }
        public int? IdProducto { get; set; }
        public int? IdBodega { get; set; }
        public bool? Activo { get; set; }
        public bool? Sistema { get; set; }
        public string? User_Agr { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public string? User_Mod { get; set; }
        public DateTime Fec_Mod { get; set; }
    }
}