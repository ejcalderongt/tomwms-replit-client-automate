namespace WMSWebAPI.Dtos.Productos
{
    public class UnidadMedidaDto
    {
        public int IdUnidadMedida { get; set; } = 0;
        public int IdPropietario { get; set; } = 0;
        public string Nombre { get; set; } = string.Empty;
        public bool Activo { get; set; } = false;
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = string.Empty;
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public string User_agr { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public bool Es_um_cobro { get; set; } = false;
        public double Factor { get; set; } = 0;
    }
}