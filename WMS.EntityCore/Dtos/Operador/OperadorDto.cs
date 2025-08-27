namespace WMSWebAPI.Dtos.Operador
{
    public class OperadorDto
    {
        public int IdOperador { get; set; }
        public int IdEmpresa { get; set; }
        public int IdRolOperador { get; set; }
        public int IdJornada { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public string User_agr { get; set; } = string.Empty;
        public DateTime Fec_agr { get; set; }
        public string User_mod { get; set; } = string.Empty;
        public DateTime Fec_mod { get; set; }
        public double Costo_hora { get; set; }
        public bool Usa_hh { get; set; }
        public byte[]? Foto { get; set; } = Array.Empty<byte>();
        public bool Recibe { get; set; }
        public bool Ubica { get; set; }
        public bool Transporta { get; set; }
        public bool Pickea { get; set; }
        public bool Verifica { get; set; }
        public bool Montacarga { get; set; }
        public bool Sistema { get; set; }
    }

}
