namespace WMSWebAPI.Dtos.Catalogos
{
    public class TipoIngresoOcDto
    {
        public int IdTipoIngresoOC { get; set; }
        public string? Nombre { get; set; }
        public bool? Es_Devolucion { get; set; }
        public string? User_Agr { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public string? User_Mod { get; set; }
        public DateTime? Fec_Mod { get; set; }
        public bool? Activo { get; set; }
        public bool? Control_Poliza { get; set; }
        public bool? Requerir_Documento_Ref { get; set; }
        public bool? Es_Poliza_Consolidada { get; set; }
        public bool? Genera_Tarea_Ingreso { get; set; }
        public bool? Requerir_Proveedor_Es_Bodega_Wms { get; set; }
        public bool? Requerir_Documento_Ref_Wms { get; set; }
        public bool? Requerir_Ubic_Rec_Ingreso { get; set; }
        public bool Exigir_Campo_Referencia { get; set; }
        public bool Marcar_Registros_Enviados_Mi3 { get; set; }
        public bool Preguntar_En_Backorder { get; set; }
        public bool Bloquear_Lotes { get; set; }
        public bool Permitir_Excedente_Lotes { get; set; }
        public bool Permitir_Vencido_Ingreso { get; set; }
        public bool Es_Importacion { get; set; }
    }
}
