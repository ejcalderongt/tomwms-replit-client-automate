namespace WMSWebAPI.Dtos.Catalogos
{
    public class PropietarioFilterDto
    {
        public int? IdPropietario { get; set; } = null;
        public string? Codigo { get; set; } = null;
        public string? Nombre_Comercial { get; set; } = null;
        public string? Contacto { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? NIT { get; set; } = null;
        public bool? Activo { get; set; } = null;
        public bool? Sistema { get; set; } = null;
        public int? IdEmpresa { get; set; } = null;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; } = "IdPropietario";
        public bool SortDescending { get; set; } = false;
    }
}