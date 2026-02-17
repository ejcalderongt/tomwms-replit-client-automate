namespace WMS.EntityCore.Dtos.Ajustes
{
    namespace WMS.Core.Entities
    {
        /// <summary>
        /// Clase para ajustes pendientes de envío a MI3/NAV/ERP
        /// </summary>
        public class clsBeAjustesMI3
        {
            /// <summary>
            /// Número de documento generado para el ajuste
            /// </summary>
            public string NoDocumento { get; set; } = string.Empty;

            /// <summary>
            /// Código del producto
            /// </summary>
            public string Codigo_Producto { get; set; } = string.Empty;

            /// <summary>
            /// Código de bodega de WMS
            /// </summary>
            public string Codigo_Bodega { get; set; } = string.Empty;

            /// <summary>
            /// Código de bodega de ERP (Proviene de cliente)
            /// </summary>
            public string Codigo_Bodega_ERP { get; set; } = string.Empty;

            /// <summary>
            /// Unidad de medida base
            /// </summary>
            public string UMBas { get; set; } = string.Empty;

            /// <summary>
            /// Cantidad del ajuste
            /// </summary>
            public double Cantidad { get; set; }

            /// <summary>
            /// Tipo de ajuste para ERP
            /// </summary>
            public string TipoAjusteERP { get; set; } = string.Empty;

            /// <summary>
            /// Tipo de ajuste de WMS
            /// </summary>
            public string TipoAjusteWMS { get; set; } = string.Empty;

            /// <summary>
            /// Lote del producto
            /// </summary>
            public string Lote { get; set; } = string.Empty;

            /// <summary>
            /// Motivo del ajuste
            /// </summary>
            public string Motivo_Ajuste { get; set; } = string.Empty;

            /// <summary>
            /// Observación del ajuste
            /// </summary>
            public string Observacion { get; set; } = string.Empty;

            /// <summary>
            /// Sección o familia del producto
            /// </summary>
            public string Seccion { get; set; } = string.Empty;

            /// <summary>
            /// ID del encabezado de ajuste
            /// </summary>
            public int IdAjusteEnc { get; set; } = 0;

            /// <summary>
            /// ID del detalle de ajuste
            /// </summary>
            public int IdAjusteDet { get; set; } = 0;

            /// <summary>
            /// ID del centro de costo
            /// </summary>
            public int IdCentroCosto { get; set; } = 0;

            /// <summary>
            /// Código del centro de costo
            /// </summary>
            public string Codigo_Centro_Costo { get; set; } = string.Empty;

            /// <summary>
            /// Talla del producto (para productos con talla/color)
            /// </summary>
            public string Talla { get; set; } = string.Empty;

            /// <summary>
            /// Color del producto (para productos con talla/color)
            /// </summary>
            public string Color { get; set; } = string.Empty;

            /// <summary>
            /// Usuario que agregó el registro
            /// </summary>
            public string Usr_Agr { get; set; } = string.Empty;

            /// <summary>
            /// #CKFK20251030: Centro de costo ERP para integración
            /// </summary>
            public string Centro_Costo_Erp { get; set; } = string.Empty;

            /// <summary>
            /// #CKFK20251030: Dirección de centro de costo ERP para integración
            /// </summary>
            public string Centro_Costo_Dir_Erp { get; set; } = string.Empty;

            /// <summary>
            /// #CKFK20251030: Departamento de centro de costo ERP para integración
            /// </summary>
            public string Centro_Costo_Dep_Erp { get; set; } = string.Empty;

            /// <summary>
            /// Constructor por defecto
            /// </summary>
            public clsBeAjustesMI3()
            {
            }

            /// <summary>
            /// Constructor con parámetros principales
            /// </summary>
            public clsBeAjustesMI3(
                string noDocumento,
                string codigoProducto,
                string codigoBodega,
                string codigoBodegaERP,
                string umBas,
                double cantidad,
                string tipoAjusteERP,
                string tipoAjusteWMS,
                string lote,
                string motivoAjuste,
                string observacion,
                string seccion,
                int idAjusteEnc,
                int idAjusteDet)
            {
                NoDocumento = noDocumento ?? string.Empty;
                Codigo_Producto = codigoProducto ?? string.Empty;
                Codigo_Bodega = codigoBodega ?? string.Empty;
                Codigo_Bodega_ERP = codigoBodegaERP ?? string.Empty;
                UMBas = umBas ?? string.Empty;
                Cantidad = cantidad;
                TipoAjusteERP = tipoAjusteERP ?? string.Empty;
                TipoAjusteWMS = tipoAjusteWMS ?? string.Empty;
                Lote = lote ?? string.Empty;
                Motivo_Ajuste = motivoAjuste ?? string.Empty;
                Observacion = observacion ?? string.Empty;
                Seccion = seccion ?? string.Empty;
                IdAjusteEnc = idAjusteEnc;
                IdAjusteDet = idAjusteDet;
            }

            /// <summary>
            /// Devuelve una representación en string del objeto
            /// </summary>
            public override string ToString()
            {
                return $"AjusteMI3: {NoDocumento} - {Codigo_Producto} - {Cantidad} {UMBas}";
            }
        }
    }
}