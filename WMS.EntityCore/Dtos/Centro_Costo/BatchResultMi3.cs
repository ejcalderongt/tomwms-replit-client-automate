namespace WMS.EntityCore.Dtos.Centro_Costo
{
    public class BatchResultMi3
    {
        public int Exitosos { get; set; }
        public int Fallidos { get; set; }
        public List<string> Errores { get; set; } = new();
        public List<CentroCostoDetalleResult> Detalles { get; set; } = new();
    }

    public class CentroCostoDetalleResult
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public bool Procesado { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}