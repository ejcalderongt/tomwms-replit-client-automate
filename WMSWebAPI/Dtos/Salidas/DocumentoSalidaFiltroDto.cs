namespace WMSWebAPI.Dtos.Salidas
{
    public class DocumentoSalidaFiltroDto
    {
        public DateTime FechaInicio { get; set; } = DateTime.Now;
        public DateTime FechaFin { get; set; } = DateTime.Now;
        public int IdBodega { get; set; } = 0;
        public int IdPropietario { get; set; } = 0;
    }
}