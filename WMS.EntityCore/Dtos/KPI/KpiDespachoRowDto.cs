namespace WMS.EntityCore.Dtos.KPI
{
    public class KpiDespachoRowDto
    {
        public DateTime Fecha_Hora_Inicio { get; set; }
        public DateTime Fecha_Hora_Fin { get; set; }
        public DateTime Fecha_Por_Linea { get; set; }

        public string Tipo_Documento_Pedido { get; set; } = "";

        public string Código_Proveedor { get; set; } = "";
        public string Descripción_Proveedor { get; set; } = "";

        public string No_Tienda { get; set; } = "";

        public string Tipo { get; set; } = "";
        public string Código_Departamento { get; set; } = "";
        public string Descripción_Departamento { get; set; } = "";
        public string Código_Categoría { get; set; } = "";
        public string Descripción_Categoría { get; set; } = "";

        public string Código_Producto { get; set; } = "";
        public string Nombre_Producto { get; set; } = "";

        public decimal Cantidad_Despachada { get; set; }
        public decimal Cantidad_Solicitada_Despacho { get; set; }
        public string Nombre_Producto_Estado { get; set; } = "";
        public decimal Cantidad_Merma_Despacho { get; set; }

        public decimal Cantidad_Reservada { get; set; }
        public decimal Cantidad_Dañada_Picking { get; set; }
        public decimal Cantidad_Dañada_Verificacion { get; set; }
        public decimal Cantidad_No_Encontrada { get; set; }

        public string Nombre_Presentación_MPQ { get; set; } = "";
        public decimal Cantidad_Despacho_Cajas { get; set; }

        public int Id_Despacho { get; set; }
        public string Orden_traslado { get; set; } = "";

        public DateTime Fecha_Vence { get; set; }
        public string Lic_Plate { get; set; } = "";

        public string Licencia_Despacho { get; set; } = "";

        public string Código_Usuario { get; set; } = "";
        public string Descripción_Usuario { get; set; } = "";
    }
}