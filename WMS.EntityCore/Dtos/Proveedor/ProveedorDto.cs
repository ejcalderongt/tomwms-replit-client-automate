using WMS.EntityCore.Proveedor;

namespace WMSWebAPI.Dtos.Catalogos
{
    public class ProveedorDto
    {
        public int IdEmpresa { get; set; }
        public int IdPropietario { get; set; }
        public int IdProveedor { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Nit { get; set; }
        public string? Direccion { get; set; }
        public string? Email { get; set; }
        public string? Contacto { get; set; }

        public bool? Activo { get; set; }
        public bool? Muestra_Precio { get; set; }
        public string? User_Agr { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public string? User_Mod { get; set; }
        public DateTime? Fec_Mod { get; set; }
        public bool? Actualiza_Costo_OC { get; set; }
        public int? IdUbicacionVirtual { get; set; }
        public bool? Es_Bodega_Recepcion { get; set; }
        public bool? Es_Bodega_Traslado { get; set; }
        public string? Referencia { get; set; }
        public bool? Sistema { get; set; }
        public int? IdConfiguracionBarraPallet { get; set; }
        public bool? Es_Proveedor_Servicio { get; set; }
        public int? IdBodegaAreaSAP { get; set; }
        public int? IdPais { get; set; }
        public string? Codigo_Empresa_ERP { get; set; }
        public bool IsNew { get; set; } = true;
        public List<clsBeProveedor_tiempos> TiemposProveedor { get; set; } = new List<clsBeProveedor_tiempos>();

    }
}