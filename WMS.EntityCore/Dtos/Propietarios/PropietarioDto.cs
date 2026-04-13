namespace WMSWebAPI.Dtos.Catalogos
{
    public class PropietarioDto
    {
        private byte[]? _imagen;

        public int IdPropietario { get; set; } = 0;
        public int? IdEmpresa { get; set; } = null;
        public int? IdTipoActualizacionCosto { get; set; } = null;
        public string Contacto { get; set; } = string.Empty;
        public string Nombre_Comercial { get; set; } = string.Empty;

        public byte[]? Imagen
        {
            get => _imagen;
            set => _imagen = value;
        }

        public string? Telefono { get; set; } = string.Empty;
        public string? Direccion { get; set; } = string.Empty;
        public bool? Activo { get; set; } = true;
        public string? User_Agr { get; set; } = string.Empty;
        public DateTime? Fec_Agr { get; set; } = DateTime.Now;
        public string? User_Mod { get; set; } = string.Empty;
        public DateTime? Fec_Mod { get; set; } = null;
        public string? Email { get; set; } = string.Empty;
        public bool? Actualiza_Costo_Oc { get; set; } = false;
        public int? Color { get; set; } = null;
        public string? Codigo { get; set; } = string.Empty;
        public bool? Sistema { get; set; } = false;
        public string? NIT { get; set; } = string.Empty;
        public string? Codigo_Acceso { get; set; } = string.Empty;
        public string? Clave_Acceso { get; set; } = string.Empty;
        public int? IdBodegaAreaSAP { get; set; } = null;
        public bool? Es_Consolidador { get; set; } = false;

        public PropietarioDto()
        {
        }

        public PropietarioDto(int idPropietario, string contacto, string nombreComercial, string codigo)
        {
            IdPropietario = idPropietario;
            Contacto = contacto ?? string.Empty;
            Nombre_Comercial = nombreComercial ?? string.Empty;
            Codigo = codigo ?? string.Empty;
        }
    }
}