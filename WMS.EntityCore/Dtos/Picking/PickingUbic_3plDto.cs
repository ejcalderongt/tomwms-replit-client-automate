using WMSWebAPI.Be;

namespace WMSWebAPI.Dtos.Picking
{
    public class PickingUbic_3plDto
    {
        public int IdPickingUbic { get; set; } = 0;
        public int IdPickingEnc { get; set; } = 0;
        public int IdPickingDet { get; set; } = 0;
        public int IdUbicacion { get; set; } = 0;

        public string Lote { get; set; } = string.Empty;

        // En JSON: "Fecha_Vence"
        public DateTime Fecha_Vence { get; set; } = DateTime.Now;
        // No existe en el JSON con este nombre (casing distinto)
        // public DateTime Fecha_vence { get; set; } = DateTime.Now;

        public DateTime Fecha_minima { get; set; } = DateTime.Now;
        public string Serial { get; set; } = string.Empty;
        public string Lic_plate { get; set; } = string.Empty;

        public bool Acepto { get; set; } = false;

        public double Peso_solicitado { get; set; } = 0;
        public double Peso_recibido { get; set; } = 0;
        public double Peso_verificado { get; set; } = 0;
        public double Peso_despachado { get; set; } = 0;

        // En JSON: "Cantidad_Solicitada"
        public double Cantidad_Solicitada { get; set; } = 0;
        // No existe en el JSON con este nombre (casing distinto)
        // public double Cantidad_solicitada { get; set; } = 0;

        // En JSON: "Cantidad_Recibida"
        public double Cantidad_Recibida { get; set; } = 0;
        // No existe en el JSON con este nombre (casing distinto)
        // public double Cantidad_recibida { get; set; } = 0;

        // En JSON: "Cantidad_Verificada"
        public double Cantidad_Verificada { get; set; } = 0;
        // No existe en el JSON con este nombre (casing distinto)
        // public double Cantidad_verificada { get; set; } = 0;

        public bool Encontrado { get; set; } = false;
        public bool Dañado_verificacion { get; set; } = false;

        public DateTime Fecha_real_vence { get; set; } = DateTime.Now;
        public DateTime Fecha_picking { get; set; } = DateTime.Now;
        public DateTime Fecha_verificado { get; set; } = DateTime.Now;
        public DateTime Fecha_despachado { get; set; } = DateTime.Now;

        public double Cantidad_despachada { get; set; } = 0;

        public string User_agr { get; set; } = string.Empty;
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = string.Empty;
        public DateTime Fec_mod { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = false;
        public bool Dañado_picking { get; set; } = false;

        public int IdProducto { get; set; } = 0;
        public int IdOperadorBodega_Pickeo { get; set; } = 0;
        public int IdOperadorBodega_Verifico { get; set; } = 0;

        public string No_packing { get; set; } = string.Empty;
        public DateTime Fecha_packing { get; set; } = DateTime.Now;

        public bool No_encontrado { get; set; } = false;
        public int IdOperadorBodega_Asignado { get; set; } = 0;

        public bool IsNew { get; set; } = true;

        public string NombreUbicacion { get; set; } = string.Empty;
        public string NombreUbicacionTemporal { get; set; } = string.Empty;

        public int IdPedidoDet { get; set; } = 0;

        public int IdStockRes { get; set; } = 0;
        public int IdStock { get; set; } = 0;

        public string CodigoProducto { get; set; } = string.Empty;
        public string NombreProducto { get; set; } = string.Empty;
        public string ProductoPresentacion { get; set; } = string.Empty;
        public string ProductoUnidadMedida { get; set; } = string.Empty;
        public string ProductoEstado { get; set; } = string.Empty;

        public int IdProductoBodega { get; set; } = 0;
        public int IdProductoEstado { get; set; } = 0;
        public int IdPresentacion { get; set; } = 0;
        public int IdUnidadMedida { get; set; } = 0;

        public int IdPedidoEnc { get; set; } = 0;

        public clsBeBodega_ubicacion Ubicacion { get; set; } = new clsBeBodega_ubicacion();

        public int IdPropietarioBodega { get; set; } = 0;
        public int IdUbicacionAnterior { get; set; } = 0;
        public int IdRecepcion { get; set; } = 0;

        public double CantidadDañada { get; set; } = 0;

        // En JSON: "Lic_plate_Reemplazo"
        public string Lic_plate_Reemplazo { get; set; } = string.Empty;
        // No existe en el JSON con este nombre (casing distinto)
        // public string Lic_plate_reemplazo { get; set; } = string.Empty;

        public int IdUbicacion_reemplazo { get; set; } = 0;
        public int IdStock_reemplazo { get; set; } = 0;

        public int IdBodega { get; set; } = 0;

        public double Tarima { get; set; } = 0;

        public string NombreArea { get; set; } = string.Empty;
        public string NombreClasificacion { get; set; } = string.Empty;

        public int IdUbicacionTemporal { get; set; } = 0;

        public string Referencia { get; set; } = string.Empty;

        // Extras (no vienen en el JSON adjunto)
        // public int IdProductoTallaColor { get; set; }
        // public string Codigo_Talla { get; set; } = string.Empty;
        // public string Nombre_Talla { get; set; } = string.Empty;
        // public string Codigo_Color { get; set; } = string.Empty;
        // public string Nombre_Color { get; set; } = string.Empty;
        // public string CodigoSKU { get; set; } = string.Empty;
        // public string No_Linea { get; set; } = string.Empty;
        // public string Producto { get; set; } = string.Empty;
        // public string Presentacion { get; set; } = string.Empty;
        // public string UnidadMedida { get; set; } = string.Empty;
    }

}