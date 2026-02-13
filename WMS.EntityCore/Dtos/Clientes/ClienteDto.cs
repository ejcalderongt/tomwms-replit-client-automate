namespace WMSWebAPI.Dtos.Datos_Maestros
{
    public class ClienteDto
    {
        public int IdCliente { get; set; } = 0;
        public int IdEmpresa { get; set; } = 0;
        public int IdPropietario { get; set; } = 0;
        public int IdTipoCliente { get; set; } = 0;
        public int IdUbicacionManufactura { get; set; } = 0;
        public string codigo { get; set; } = string.Empty;
        public string nombre_comercial { get; set; } = string.Empty;
        public string nombre_contacto { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public string nit { get; set; } = string.Empty;
        public string direccion { get; set; } = string.Empty;
        public string correo_electronico { get; set; } = string.Empty;
        public bool activo { get; set; } = false;
        public bool realiza_manufactura { get; set; } = false;
        public string user_agr { get; set; } = string.Empty;
        public DateTime fec_agr { get; set; } = DateTime.Now;
        public string user_mod { get; set; } = string.Empty;
        public DateTime fec_mod { get; set; } = DateTime.Now;
        public bool despachar_lotes_completos { get; set; } = false;
        public bool sistema { get; set; } = false;
        public bool es_bodega_recepcion { get; set; } = false;
        public bool es_bodega_traslado { get; set; } = false;
        public int idubicacionvirtual { get; set; } = 0;
        public string referencia { get; set; } = string.Empty;
        public bool control_ultimo_lote { get; set; } = false;
        public bool control_calidad { get; set; } = false;
        public int IdUbicacionAbastecerCon { get; set; } = 0;
        public int IdBodegaAreaSAP { get; set; } = 0;
        public bool es_proveedor { get; set; } = false;
        public string Codigo_Empresa_ERP { get; set; } = string.Empty;
        
        
    }
}