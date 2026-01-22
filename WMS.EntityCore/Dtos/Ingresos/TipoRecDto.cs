namespace WMSWebAPI.Dtos.Ingresos
{
    public class TipoRecDto
    {
        public string IdTipoTransaccion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Funcionalidad { get; set; } = string.Empty;
        public bool UsaHH { get; set; } = false;
        public string DescDev { get; set; } = string.Empty;
        public string TipoTrans { get; set; } = string.Empty;
        public bool ConRef { get; set; } = false;
        public bool Activo { get; set; } = false;
    }
}
