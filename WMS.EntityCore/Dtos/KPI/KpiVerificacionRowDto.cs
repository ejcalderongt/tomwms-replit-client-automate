namespace WMS.EntityCore.Dtos.KPI
{
    public class KpiVerificacionRowDto
    {
        public DateTime Fecha_Hora_Inicio { get; set; }
        public DateTime Fecha_Hora_Fin { get; set; }
        public DateTime Fecha_Por_Línea { get; set; }

        public string Tipo_Documento_Pedido { get; set; } = "";
        public string Tipo { get; set; } = "";

        public string Código_Departamento { get; set; } = "";
        public string Descripción_Departamento { get; set; } = "";
        public string Código_Categoría { get; set; } = "";
        public string Descripción_Categoría { get; set; } = "";

        public string Código_Producto { get; set; } = "";
        public string Nombre_Producto { get; set; } = "";

        public decimal Cantidad_Verificada { get; set; }
        public decimal Cantidad_Solicita_Ver { get; set; }

        public string Nombre_Producto_Estado { get; set; } = "";
        public decimal Cantidad_Merma_Ver { get; set; }

        public string Nombre_Presentación_MPQ { get; set; } = "";
        public decimal Cantidad_Verificada_Cajas { get; set; }

        public int Id_Picking { get; set; }

        public DateTime Fecha_Vence { get; set; }
        public string Lic_Plate { get; set; } = "";

        public string Código_Operador { get; set; } = "";
        public string Descripción_Operador { get; set; } = "";

        public string Código_Comprador { get; set; } = "";
        public string Nombre_Comprador { get; set; } = "";

        public string Solicitud_SAP { get; set; } = "";
    }
}