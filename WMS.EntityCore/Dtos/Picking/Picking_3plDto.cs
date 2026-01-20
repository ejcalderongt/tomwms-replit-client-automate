namespace WMSWebAPI.Dtos.Picking
{
    public class Picking_3plDto
    {
        public PickingEncDto? Encabezado { get; set; }
        public List<PickingDet_3plDto> Detalle { get; set; } = new();
        public List<PickingUbic_3plDto> PickingUbic { get; set; } = new();
        public List<PickingUbicStockDto> PickingUbicStock { get; set; } = new();
        public List<PickingImgDto> PickingImg { get; set; } = new();
        public List<PickingOpDto> PickingOperadores { get; set; } = new();
        public PickingPrioridadDto? Prioridad { get; set; }
    }
}