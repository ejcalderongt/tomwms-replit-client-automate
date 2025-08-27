namespace WMSWebAPI.Dtos.Picking
{
    public class PickingDto
    {
        public PickingEncDto? Encabezado { get; set; }
        public List<PickingDetDto> Detalle { get; set; } = new();
        public List<PickingUbicDto> PickingUbic { get; set; } = new();
        public List<PickingUbicStockDto> PickingUbicStock { get; set; } = new();
        public List<PickingImgDto> PickingImg { get; set; } = new();
        public List<PickingOpDto> PickingOperadores { get; set; } = new();
        public PickingPrioridadDto? Prioridad { get; set; }
    }
}