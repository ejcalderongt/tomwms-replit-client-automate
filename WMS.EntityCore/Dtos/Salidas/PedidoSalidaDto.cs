namespace WMSWebAPI.Dtos.Salidas
{
    public class PedidoSalidaDto
    {
        public int Correlativo { get; set; } = 0;
        public int IdBodega { get; set; } = 0;
        public long NoDocumento { get; set; } = 0;
        public string Referencia { get; set; } = string.Empty;
        public string Referencia2 { get; set; } = string.Empty;
        public DateTime FechaPedido { get; set; } = DateTime.MinValue;
        public string Cliente { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Bodega { get; set; } = string.Empty;
        public string Muelle { get; set; } = string.Empty;
        public string Propietario { get; set; } = string.Empty;
        public string RoadVendedor { get; set; } = string.Empty;
        public string RoadRuta { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.MinValue;
        public bool Anulado { get; set; } = false;
        public bool Activo { get; set; } = true;
        public bool EnviadoAErp { get; set; } = false;
        public DateTime FecAgr { get; set; } = DateTime.MinValue;
        public int IdPickingEnc { get; set; } = 0;
        public string TipoDocumento { get; set; } = string.Empty;
        public long IdDespachoEnc { get; set; } = 0;
        public string Observacion { get; set; } = string.Empty;
        public string RutaDespacho { get; set; } = string.Empty;
        public string NoPickingErp { get; set; } = string.Empty;
        public string NoDocumentoExterno { get; set; } = string.Empty;
        public string BodegaOrigen { get; set; } = string.Empty;
        public string BodegaDestino { get; set; } = string.Empty;
        public int IdPrioridadPicking { get; set; } = 0;
        public string Codigo_Empresa_ERP { get; set; } = "";
        public bool EsExportacion { get; set; } = false;
    }
}