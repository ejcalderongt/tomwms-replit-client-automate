namespace WMSWebAPI.Dtos.Catalogos
{
    public class ProductoTipoDto
    {
        public int IdTipoProducto { get; set; }
        public int IdPropietario { get; set; }
        public string? NombreTipoProducto { get; set; }
        public bool? Activo { get; set; }
        public string? User_Agr { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public string? User_Mod { get; set; }
        public DateTime? Fec_Mod { get; set; }
        public string? Codigo { get; set; }
        public PropietarioDto Propietario { get; set; } = new();
    }
}