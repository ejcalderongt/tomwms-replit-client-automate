namespace WMSWebAPI.Entity.Trans_oc
{
    public class clsBeVWOrdenCompra
    {
        public string Codigo { get; set; } = "";
        public string Bodega { get; set; } = "";
        public string Propietario { get; set; } = "";
        public string Proveedor { get; set; } = "";
        public string TipoIngreso { get; set; } = "";
        public string Estado { get; set; } = "";
        public string NoDocumento { get; set; } = "";
        public string Referencia { get; set; } = "";
        public string Procedencia { get; set; } = "";
        public int IdPropietario { get; set; } = 0;
        public bool Activo { get; set; } = false;
        public int IdPropietarioBodega { get; set; } = 0;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public bool es_devolucion { get; set; } = false;
        public bool Enviado_A_ERP { get; set; } = false;
        public int IdBodega { get; set; } = 0;
        public string NoPoliza { get; set; } = "";
        public string NoOrden { get; set; } = "";
        public string No_Documento_Recepcion_ERP { get; set; } = "";
        public string No_Documento_Devolucion { get; set; } = "";
        public string No_Documento_Ubicacion_ERP { get; set; } = "";
        public string No_Ticket_TMS { get; set; } = "";
        public string No_Marchamo { get; set; } = "";
        public bool Control_Poliza { get; set; } = false;
    }
}