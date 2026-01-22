namespace WMSWebAPI.Dtos.Ingresos
{
    public class TipoIngresoDto
    {
        public int IdTipoIngresoOC { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Es_devolucion { get; set; }
        public string User_agr { get; set; } = string.Empty;
        public DateTime Fec_agr { get; set; }
        public string User_mod { get; set; } = string.Empty;
        public DateTime Fec_mod { get; set; }
        public bool Activo { get; set; }
        public bool Control_poliza { get; set; }
        public bool Requerir_documento_ref { get; set; }
        public bool Es_poliza_consolidada { get; set; }
        public bool Genera_tarea_ingreso { get; set; }
        public bool Requerir_proveedor_es_bodega_wms { get; set; }
        public bool Requerir_documento_ref_wms { get; set; }
        public bool Requerir_ubic_rec_ingreso { get; set; }
        public bool Exigir_campo_referencia { get; set; }
        public bool Marcar_registros_enviados_mi3 { get; set; }
        public bool Preguntar_en_backorder { get; set; }
        public bool Bloquear_lotes { get; set; }
        public bool Permitir_excedente_lotes { get; set; }
        public bool Permitir_vencido_ingreso { get; set; }
        public bool Es_importacion { get; set; }
        public int IdProductoEstado { get; set; } = 0;
    }
}
