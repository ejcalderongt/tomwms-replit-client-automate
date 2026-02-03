namespace WMS.EntityCore.Dtos.KPI
{
    public class KpiRecepcionRowDto
    {
        public DateTime Fecha_Hora_Inicio { get; set; }
        public DateTime Fecha_Hora_Fin { get; set; }
        public DateTime Fecha_por_Linea { get; set; }

        public string Tipo_Local_Importación { get; set; } = "";

        public string Código_Proveedor { get; set; } = "";
        public string Descripción_Proveedor { get; set; } = "";

        public string Tipo { get; set; } = "";

        public string Código_Departamento { get; set; } = "";
        public string Descripcion_Departamento { get; set; } = "";

        public string Código_Categoria { get; set; } = "";
        public string Descripcion_Categoría { get; set; } = "";

        public string Código_Producto { get; set; } = "";
        public string Nombre_Producto { get; set; } = "";

        public decimal Cantidad_Recibida { get; set; }
        public decimal Cantidad_Solicita_OC { get; set; }

        public string Nombre_Producto_Estado { get; set; } = "";
        public decimal Cantidad_Devolucion_OC { get; set; }

        public string Nombre_Presentacion_MPQ { get; set; } = "";
        public decimal Cantidad_Recibida_Cajas_OC { get; set; }

        public int Id_Recepcion { get; set; }
        public string Número_de_OC { get; set; } = "";
        public int IdOC { get; set; }

        public DateTime Fecha_Vence { get; set; }
        public string Lic_Plate { get; set; } = "";

        public string Código_Operador { get; set; } = "";
        public string Descripción_Operador { get; set; } = "";

        public string Código_Comprador { get; set; } = "";
        public string Descripción_Comprador { get; set; } = "";

        public string Contenedor { get; set; } = "";
    }
}