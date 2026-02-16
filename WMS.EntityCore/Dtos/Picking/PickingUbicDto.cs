using WMSWebAPI.Be;

namespace WMSWebAPI.Dtos.Picking
{
    public class PickingUbicDto
    {
        public int IdPickingUbic { get; set; } = 0;
        public int IdPickingEnc { get; set; } = 0;
        public int IdPickingDet { get; set; } = 0;
        public int IdUbicacion { get; set; } = 0;
        public int IdStock { get; set; } = 0;
        public int IdPropietarioBodega { get; set; } = 0;
        public int IdProductoBodega { get; set; } = 0;
        public int IdProductoEstado { get; set; } = 0;
        public int IdPresentacion { get; set; } = 0;
        public int IdUnidadMedida { get; set; } = 0;
        public int IdUbicacionAnterior { get; set; } = 0;
        public int IdRecepcion { get; set; } = 0;
        public string Lote { get; set; } = string.Empty;
        public DateTime Fecha_vence { get; set; } = DateTime.Now;
        public DateTime Fecha_minima { get; set; } = DateTime.Now;
        public string Serial { get; set; } = string.Empty;
        public string Lic_plate { get; set; } = string.Empty;
        public bool Acepto { get; set; } = false;
        public double Peso_solicitado { get; set; } = 0;
        public double Peso_recibido { get; set; } = 0;
        public double Peso_verificado { get; set; } = 0;
        public double Peso_despachado { get; set; } = 0;
        public double Cantidad_solicitada { get; set; } = 0;
        public double Cantidad_recibida { get; set; } = 0;
        public double Cantidad_verificada { get; set; } = 0;
        public bool Encontrado { get; set; } = false;
        public bool Dañado_verificacion { get; set; } = false;
        public DateTime Fecha_real_vence { get; set; } = DateTime.Now;
        public string No_packing { get; set; } = string.Empty;
        public DateTime Fecha_picking { get; set; } = DateTime.Now;
        public DateTime Fecha_verificado { get; set; } = DateTime.Now;
        public DateTime Fecha_packing { get; set; } = DateTime.Now;
        public DateTime Fecha_despachado { get; set; } = DateTime.Now;
        public double Cantidad_despachada { get; set; } = 0;
        public string User_agr { get; set; } = string.Empty;
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = string.Empty;
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = false;
        public int IdPedidoDet { get; set; } = 0;
        public bool Dañado_picking { get; set; } = false;
        public int IdStockRes { get; set; } = 0;
        public string Lic_plate_reemplazo { get; set; } = string.Empty;
        public int IdUbicacion_reemplazo { get; set; } = 0;
        public int IdStock_reemplazo { get; set; } = 0;
        public int IdBodega { get; set; } = 0;
        public int IdOperadorBodega_Pickeo { get; set; } = 0;
        public int IdOperadorBodega_Verifico { get; set; } = 0;
        public int IdPedidoEnc { get; set; } = 0;
        public bool No_encontrado { get; set; } = false;
        public int IdUbicacionTemporal { get; set; } = 0;
        public int IdOperadorBodega_Asignado { get; set; } = 0;
        public int IdProductoTallaColor { get; set; }
        public bool IsNew { get; set; } = true;
        public string NombreUbicacion { get; set; } = string.Empty;
        public string NombreUbicacionTemporal { get; set; } = string.Empty;
        public string CodigoProducto { get; set; } = string.Empty;
        public string NombreProducto { get; set; } = string.Empty;
        public string ProductoPresentacion { get; set; } = string.Empty;
        public string ProductoUnidadMedida { get; set; } = string.Empty;
        public string ProductoEstado { get; set; } = string.Empty;
        public clsBeBodega_ubicacion Ubicacion { get; set; } = new clsBeBodega_ubicacion();
        public decimal CantidadDañada { get; set; }
        //public string Tarima { get; set; } = string.Empty;
        public double Tarima { get; set; } = 0;
        public string NombreArea { get; set; } = string.Empty;
        public string NombreClasificacion { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
        public string Codigo_Talla { get; set; } = string.Empty;
        public string Nombre_Talla { get; set; } = string.Empty;
        public string Codigo_Color { get; set; } = string.Empty;
        public string Nombre_Color { get; set; } = string.Empty;
        public string CodigoSKU { get; set; } = string.Empty;
        public string No_Linea { get; set; } = string.Empty;
        public int IdProducto { get; set; } = 0;
        public string Producto { get; set; } = string.Empty;
        public string Presentacion { get; set; } = string.Empty;
        public string UnidadMedida { get; set; } = string.Empty;
    }
}