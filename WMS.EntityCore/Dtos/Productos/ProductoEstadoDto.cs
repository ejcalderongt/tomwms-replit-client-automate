namespace WMSWebAPI.Dtos.Catalogos
{
    public class ProductoEstadoDto
    {
        public int IdEstado { get; set; }
        public int IdPropietario { get; set; }
        public string? Nombre { get; set; }
        public int? IdUbicacionDefecto { get; set; }
        public bool? Utilizable { get; set; }
        public bool? Activo { get; set; }
        public string? User_Agr { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public string? User_Mod { get; set; }
        public DateTime Fec_Mod { get; set; }
        public bool? Dañado { get; set; }
        public string? Codigo_Bodega_Erp { get; set; }
        public bool? Sistema { get; set; }
        public int Dias_Vencimiento_Clasificacion { get; set; }
        public int Tolerancia_Dias_Vencimiento { get; set; }
    }
}