namespace WMSWebAPI.Dtos.Picking
{
    public class PickingEncDto
    {
        public int IdPickingEnc { get; set; } = 0;
        public int IdBodega { get; set; } = 0;
        public int IdPropietarioBodega { get; set; } = 0;
        public int IdUbicacionPicking { get; set; } = 0;
        public DateTime Fecha_picking { get; set; } = DateTime.Now;
        public DateTime Hora_ini { get; set; } = DateTime.Now;
        public DateTime Hora_fin { get; set; } = DateTime.Now;
        public string Estado { get; set; } = string.Empty;
        public string User_agr { get; set; } = string.Empty;
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = string.Empty;
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public bool Detalle_operador { get; set; } = false;
        public bool Activo { get; set; } = false;
        public bool Verifica_auto { get; set; } = false;
        public bool Procesado_bof { get; set; } = false;
        public bool Requiere_preparacion { get; set; } = false;
        public string Tipo_preparacion { get; set; } = string.Empty;
        public string Estado_preparacion { get; set; } = string.Empty;
        public DateTime Fecha_inicio_preparacion { get; set; } = DateTime.Now;
        public DateTime Fecha_fin_preparacion { get; set; } = DateTime.Now;
        public string Referencia { get; set; } = string.Empty;
        public bool Fotografia_verificacion { get; set; } = false;
        public int IdBodegaMuelle { get; set; } = 0;
        public int IdPrioridadPicking { get; set; } = 0;
        public int IdTipoPicking { get; set; } = 0;
    }
}