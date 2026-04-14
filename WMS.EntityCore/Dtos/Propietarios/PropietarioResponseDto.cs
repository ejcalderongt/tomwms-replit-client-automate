namespace WMSWebAPI.Dtos.Catalogos
{
    public class PropietarioResponseDto
    {
        public int IdPropietario { get; set; }
        public int? IdEmpresa { get; set; }
        public string Contacto { get; set; } = string.Empty;
        public string Nombre_Comercial { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public bool? Activo { get; set; }
        public string? Email { get; set; }
        public string? Codigo { get; set; }
        public string? NIT { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public DateTime? Fec_Mod { get; set; }
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
    }

    public class PropietarioListResponseDto
    {
        public List<PropietarioResponseDto> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
    }
}