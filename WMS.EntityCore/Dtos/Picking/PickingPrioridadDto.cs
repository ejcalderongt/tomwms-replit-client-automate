namespace WMSWebAPI.Dtos.Picking
{
    public class PickingPrioridadDto
    {
        public int IdPrioridadPicking { get; set; } = 0;
        public int Codigo { get; set; } = 0;
        public string Nombre { get; set; } = string.Empty;
        public string User_agr { get; set; } = string.Empty;
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = string.Empty;
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = false;
    }
}