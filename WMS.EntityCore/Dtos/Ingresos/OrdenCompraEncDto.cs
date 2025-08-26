namespace WMSWebAPI.Dtos.Ingresos
{
    public class OrdenCompraEncDto
    {
        public int IdOrdenCompraEnc { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int IdProveedorBodega { get; set; }
        public int? IdTipoIngresoOC { get; set; }
        public int? IdEstadoOC { get; set; }
        public int? IdMotivoDevolucion { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public DateTime? Hora_Creacion { get; set; }
        public string? No_Documento { get; set; }
        public string? User_Agr { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public string? User_Mod { get; set; }
        public DateTime? Fec_Mod { get; set; }
        public string? Procedencia { get; set; }
        public string? No_Marchamo { get; set; }
        public string? Referencia { get; set; }
        public string? Observacion { get; set; }
        public bool? Control_Poliza { get; set; }
        public bool? Activo { get; set; }
        public DateTime? Fecha_Recepcion { get; set; }
        public DateTime? Hora_Inicio_Recepcion { get; set; }
        public DateTime? Hora_Fin_Recepcion { get; set; }
        public int? IdMuelleRecepcion { get; set; }
        public bool? Programar_Recepcion { get; set; }
        public int? IdMotivoAnulacionBodega { get; set; }
        public bool? Enviado_A_ERP { get; set; }
        public string? Serie { get; set; }
        public int? Correlativo { get; set; }
        public int? IdDespachoEnc { get; set; }
        public string? No_Ticket_Tms { get; set; }
        public int? IdNoDocumentoRef { get; set; }
        public int? IdAcuerdoComercial { get; set; }
        public int? IdOperadorBodegaDefecto { get; set; }
        public int? IdBodega { get; set; }
        public string? No_Documento_Recepcion_Erp { get; set; }
        public string? No_Documento_Devolucion { get; set; }
        public int? IdPedidoEncDevolucion { get; set; }
        public bool? Push_To_Nav { get; set; }
        public string? No_Documento_Ubicacion_Erp { get; set; }
        public bool? PutAway_Registrado { get; set; }
        public string? Codigo_Empresa_ERP { get; set; }
    }
}