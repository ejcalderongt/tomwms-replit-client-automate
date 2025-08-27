namespace WMSWebAPI.Dtos.Catalogos
{
    public class ProductoPresentacionDto
    {
        public int IdPresentacion { get; set; }
        public int IdProducto { get; set; }
        public string? Codigo_Barra { get; set; }
        public string? Nombre { get; set; }
        public bool? Imprime_Barra { get; set; }
        public float? Peso { get; set; }
        public float? Alto { get; set; }
        public float? Largo { get; set; }
        public float? Ancho { get; set; }
        public float Factor { get; set; }
        public float? MinimoExistencia { get; set; }
        public float? MaximoExistencia { get; set; }
        public string? User_Agr { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public string? User_Mod { get; set; }
        public DateTime? Fec_Mod { get; set; }
        public bool? Activo { get; set; }
        public bool? EsPallet { get; set; }
        public float? Precio { get; set; }
        public float? MinimoPeso { get; set; }
        public float? MaximoPeso { get; set; }
        public float? Costo { get; set; }
        public float? CamasPorTarima { get; set; }
        public float? CajasPorCama { get; set; }
        public bool? Genera_Lp_Auto { get; set; }
        public bool? Permitir_Paletizar { get; set; }
        public bool? Sistema { get; set; }
        public int? IdPresentacionPallet { get; set; }
        public string? Codigo { get; set; }
    }
}