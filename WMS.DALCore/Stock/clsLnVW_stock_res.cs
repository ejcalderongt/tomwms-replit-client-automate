using System.Data;
using WMS.EntityCore.Stock;

namespace WMS.DALCore.Stock
{
    internal class clsLnVW_stock_res
    {
        public static void Cargar(ref clsBeVW_stock_res oBeT_vw_stock_res,
                                 DataRow dr)
        {
            if (oBeT_vw_stock_res == null)
                throw new ArgumentNullException(nameof(oBeT_vw_stock_res));

            if (dr == null)
                throw new ArgumentNullException(nameof(dr));            

            try
            {
                // Función auxiliar para simplificar la asignación
                T GetSafeValue<T>(string columnName, T defaultValue)
                {
                    if (dr.Table.Columns.Contains(columnName) && !Convert.IsDBNull(dr[columnName]))
                        return (T)Convert.ChangeType(dr[columnName], typeof(T));
                    return defaultValue;
                }

                // Asignaciones usando la función auxiliar
                oBeT_vw_stock_res.IdBodega = GetSafeValue("IdBodega", 0);
                oBeT_vw_stock_res.IdPropietario = GetSafeValue("IdPropietario", 0);
                oBeT_vw_stock_res.IdPropietarioBodega = GetSafeValue("IdPropietarioBodega", 0);
                oBeT_vw_stock_res.IdProducto = GetSafeValue("IdProducto", 0);
                oBeT_vw_stock_res.IdProductoBodega = GetSafeValue("IdProductoBodega", 0);
                oBeT_vw_stock_res.IdStock = GetSafeValue("IdStock", 0);

                // Manejo especial para IdUbicacion (puede venir de diferentes columnas)
                if (dr.Table.Columns.Contains("IdUbicacionActual") && !Convert.IsDBNull(dr["IdUbicacionActual"]))
                    oBeT_vw_stock_res.IdUbicacion = Convert.ToInt32(dr["IdUbicacionActual"]);
                else if (dr.Table.Columns.Contains("IdUbicacion") && !Convert.IsDBNull(dr["IdUbicacion"]))
                    oBeT_vw_stock_res.IdUbicacion = Convert.ToInt32(dr["IdUbicacion"]);

                oBeT_vw_stock_res.IdUbicacion_Anterior = GetSafeValue("IdUbicacion_anterior", 0);
                oBeT_vw_stock_res.IdUnidadMedida = GetSafeValue("IdUnidadMedida", 0);
                oBeT_vw_stock_res.IdProductoEstado = GetSafeValue("IdProductoEstado", 0);
                oBeT_vw_stock_res.IdPresentacion = GetSafeValue("IdPresentacion", 0);
                oBeT_vw_stock_res.IdRecepcionEnc = GetSafeValue("IdRecepcionEnc", 0);
                oBeT_vw_stock_res.IdRecepcionDet = GetSafeValue("IdRecepcionDet", 0);
                oBeT_vw_stock_res.Propietario = GetSafeValue("Propietario", "");
                oBeT_vw_stock_res.UMBas = GetSafeValue("UnidadMedida", "");
                oBeT_vw_stock_res.Nombre_Presentacion = GetSafeValue("Presentacion", "");
                oBeT_vw_stock_res.Codigo_Producto = GetSafeValue("codigo", "");
                oBeT_vw_stock_res.Nombre_Producto = GetSafeValue("nombre", "");
                oBeT_vw_stock_res.Lote = GetSafeValue("lote", "");
                oBeT_vw_stock_res.Fecha_ingreso = GetSafeValue("fecha_ingreso", DateTime.Now);
                oBeT_vw_stock_res.Serial = GetSafeValue("serial", "");
                oBeT_vw_stock_res.Añada = GetSafeValue("añada", 0);
                oBeT_vw_stock_res.CantidadUmBas = GetSafeValue("CantidadSF", 0.0);
                oBeT_vw_stock_res.Factor = GetSafeValue("factor", 0.0);
                oBeT_vw_stock_res.CantidadPresentacion = GetSafeValue("Cantidad", 0.0);
                oBeT_vw_stock_res.Fecha_Vence = GetSafeValue("fecha_vence", new DateTime(1990, 1, 1));
                oBeT_vw_stock_res.NomEstado = GetSafeValue("NomEstado", "");
                oBeT_vw_stock_res.EstadoUtilizable = GetSafeValue("EstadoUtilizable", false);
                oBeT_vw_stock_res.Dañado = GetSafeValue("dañado", false);
                oBeT_vw_stock_res.Lic_plate = GetSafeValue("lic_plate", "");
                oBeT_vw_stock_res.Peso = GetSafeValue("peso", 0.0);
                oBeT_vw_stock_res.IdIndiceRotacion = GetSafeValue("IdIndiceRotacion", 0);
                oBeT_vw_stock_res.AltoUbicacion = GetSafeValue("alto", 0.0);
                oBeT_vw_stock_res.LargoUbicacion = GetSafeValue("largo", 0.0);
                oBeT_vw_stock_res.AnchoUbicacion = GetSafeValue("ancho", 0.0);
                oBeT_vw_stock_res.CantidadReservadaUMBas = GetSafeValue("CantidadReservada", 0.0);
                oBeT_vw_stock_res.IdTramo = GetSafeValue("IdTramo", 0);
                oBeT_vw_stock_res.Ancho_ubicacion = GetSafeValue("ancho_ubicacion", 0.0);
                oBeT_vw_stock_res.Largo_ubicacion = GetSafeValue("largo_ubicacion", 0.0);
                oBeT_vw_stock_res.Alto_ubicacion = GetSafeValue("alto_ubicacion", 0.0);
                oBeT_vw_stock_res.IndiceRotacion = GetSafeValue("IndiceRotacion", "");
                oBeT_vw_stock_res.Existencia_min_umbas = GetSafeValue("existencia_min_umbas", 0.0);
                oBeT_vw_stock_res.Existencia_max_umbas = GetSafeValue("existencia_max_umbas", 0.0);
                oBeT_vw_stock_res.Existencia_min_pres = GetSafeValue("Existencia_min_pres", 0.0);
                oBeT_vw_stock_res.Existencia_max_pres = GetSafeValue("Existencia_max_pres", 0.0);
                oBeT_vw_stock_res.Codigo_Barra = GetSafeValue("codigo_barra", "");
                oBeT_vw_stock_res.Costo = GetSafeValue("costo", 0.0);
                oBeT_vw_stock_res.IdPedido = GetSafeValue("IdPedido", 0);
                oBeT_vw_stock_res.IdPedidoDet = GetSafeValue("IdPedidoDet", 0);
                oBeT_vw_stock_res.IdPicking = GetSafeValue("IdPicking", 0);
                oBeT_vw_stock_res.Tolerancia = GetSafeValue("Tolerancia", 0.0);
                oBeT_vw_stock_res.Atributo_variante_1 = GetSafeValue("atributo_variante_1", "");
                oBeT_vw_stock_res.Ubicacion_Nivel = GetSafeValue("ubicacion_nivel", 0);
                oBeT_vw_stock_res.Ubicacion_Nombre = GetSafeValue("ubicacion_nombre", "");
                oBeT_vw_stock_res.Ubicacion_Indice_x = GetSafeValue("ubicacion_indice_x", 0);
                oBeT_vw_stock_res.Ubicacion_Tramo = GetSafeValue("ubicacion_tramo", "");
                oBeT_vw_stock_res.IdTipoProducto = GetSafeValue("IdTipoProducto", 0);
                oBeT_vw_stock_res.NombreTipoProducto = GetSafeValue("NombreTipoProducto", "");
                oBeT_vw_stock_res.Pallet_No_Estandar = GetSafeValue("Pallet_No_Estandar", false);
                oBeT_vw_stock_res.Posiciones = GetSafeValue("Posiciones", 0);
                oBeT_vw_stock_res.codigo_poliza = GetSafeValue("codigo_poliza", "");
                oBeT_vw_stock_res.Numero_poliza = GetSafeValue("Numero_poliza", "");
                oBeT_vw_stock_res.ubicacion_picking = GetSafeValue("ubicacion_picking", false);
                oBeT_vw_stock_res.CajasPorCama = GetSafeValue("CajasPorCama", 0.0);
                oBeT_vw_stock_res.CamasPorTarima = GetSafeValue("CamasPorTarima", 0.0);
                oBeT_vw_stock_res.es_rack = GetSafeValue("es_rack", false);
                oBeT_vw_stock_res.IdStockRes = GetSafeValue("IdStockRes", 0);
                oBeT_vw_stock_res.Area = GetSafeValue("Area", "");
                oBeT_vw_stock_res.Nombre_Clasificacion = GetSafeValue("Clasificacion", "");
                oBeT_vw_stock_res.Nombre_Completo = GetSafeValue("Nombre_Completo", "");
                oBeT_vw_stock_res.Fecha_Preparacion = GetSafeValue("Fecha_Preparacion", new DateTime(1900, 1, 1));
                oBeT_vw_stock_res.Fecha_Pedido = GetSafeValue("Fecha_Pedido", new DateTime(1900, 1, 1));
                oBeT_vw_stock_res.Codigo_Talla = GetSafeValue("Codigo_Talla", "");
                oBeT_vw_stock_res.Nombre_Talla = GetSafeValue("Nombre_Talla", "");
                oBeT_vw_stock_res.Codigo_Color = GetSafeValue("Codigo_Color", "");
                oBeT_vw_stock_res.Nombre_Color = GetSafeValue("Nombre_Color", "");
                oBeT_vw_stock_res.IdProductoTallaColor = GetSafeValue("IdProductoTallaColor", 0);

                // Campos que pueden venir de diferentes columnas
                if (string.IsNullOrEmpty(oBeT_vw_stock_res.Codigo_Talla))
                    oBeT_vw_stock_res.Codigo_Talla = GetSafeValue("Talla", "");

                if (string.IsNullOrEmpty(oBeT_vw_stock_res.Codigo_Color))
                    oBeT_vw_stock_res.Codigo_Color = GetSafeValue("Color", "");
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
