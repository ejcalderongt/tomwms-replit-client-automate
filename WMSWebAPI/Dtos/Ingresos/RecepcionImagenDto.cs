namespace WMSWebAPI.Dtos.Ingresos
{
    public class RecepcionImagenDto
    {
        public int IdImagen { get; set; }
        public int IdRecepcionEnc { get; set; }

        public byte[] Imagen { get; set; } = Array.Empty<byte>();

        public string User_Agr { get; set; } = string.Empty;
        public DateTime Fec_Agr { get; set; }

        public string? Observacion { get; set; }
    }
}