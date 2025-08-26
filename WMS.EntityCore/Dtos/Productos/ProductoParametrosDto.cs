namespace WMSWebAPI.Dtos.Catalogos
{
    public class ProductoParametrosDto
    {
        public int IdProductoParametro { get; set; }
        public int IdParametro { get; set; }
        public int IdProducto { get; set; }
        public string? Valor_Texto { get; set; }
        public double? Valor_Numerico { get; set; }
        public DateTime? Valor_Fecha { get; set; }
        public bool? Valor_Logico { get; set; }
        public bool Capturar_Siempre { get; set; }
        public string User_Agr { get; set; } = string.Empty;
        public DateTime Fec_Agr { get; set; }

        public string User_Mod { get; set; } = string.Empty;
        public DateTime Fec_Mod { get; set; }
        public bool Activo { get; set; }
    }
}
