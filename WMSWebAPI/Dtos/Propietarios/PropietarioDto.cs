namespace WMSWebAPI.Dtos.Catalogos
{
    public class PropietarioDto
    {
        public int IdPropietario { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdTipoActualizacionCosto { get; set; }
        public string Contacto { get; set; } = string.Empty;
        public string Nombre_Comercial { get; set; } = string.Empty;
        public byte[]? Imagen { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public bool? Activo { get; set; }
        public string? User_Agr { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public string? User_Mod { get; set; }
        public DateTime? Fec_Mod { get; set; }
        public string? Email { get; set; }
        public bool? Actualiza_Costo_Oc { get; set; }
        public int? Color { get; set; }
        public string? Codigo { get; set; }
        public bool? Sistema { get; set; }
        public string? NIT { get; set; }
        public string? Codigo_Acceso { get; set; }
        public string? Clave_Acceso { get; set; }
        public int? IdBodegaAreaSAP { get; set; }
        public bool? Es_Consolidador { get; set; }
    }
}