namespace WMSWebAPI.Dtos.Ingresos
{
    public class DocumentoIngresoFiltroDto
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdBodega { get; set; }
        public int IdPropietario { get; set; }
    }
}
