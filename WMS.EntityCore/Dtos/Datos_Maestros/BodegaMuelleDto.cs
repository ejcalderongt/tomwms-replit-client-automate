namespace WMSWebAPI.Dtos.Datos_Maestros
{
    public class BodegaMuelleDto
    {
        public int IdMuelle { get; set; } = 0;
        public int IdBodega { get; set; } = 0;
        public string Codigo_Barra { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string User_Agr { get; set; } = string.Empty;
        public DateTime Fec_Agr { get; set; } = DateTime.Now;
        public string User_Mod { get; set; } = string.Empty;
        public DateTime Fec_Mod { get; set; } = DateTime.Now;
        public int Color { get; set; } = 0;
        public byte[]? Imagen { get; set; } = Array.Empty<byte>();
        public bool Activo { get; set; } = false;
        public bool Entrada { get; set; } = false;
        public bool Salida { get; set; } = false;
        public int IdUbicacionDefecto { get; set; } = 0;
    }
}