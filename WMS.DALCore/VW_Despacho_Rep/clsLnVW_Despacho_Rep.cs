using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using WMS.EntityCore.Datos_Maestros;
using WMS.EntityCore.VW_Despacho_Rep;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WMS.DALCore.VW_Despacho_Rep
{
    public class clsLnVW_Despacho_Rep
    {
        public static void Cargar(ref clsBeVW_Despacho_Rep oBeTmp_VW_Despacho_Rep, DataRow dr)
        {
            oBeTmp_VW_Despacho_Rep.IdPickingUbic = dr.Field<int?>("IdPickingUbic") ?? 0;
            oBeTmp_VW_Despacho_Rep.IdStock = dr.Field<int?>("IdStock") ?? 0;
            oBeTmp_VW_Despacho_Rep.IdPedidoDet = dr.Field<int?>("IdPedidoDet") ?? 0;
            oBeTmp_VW_Despacho_Rep.IdPropietarioBodega = dr.Field<int?>("IdPropietarioBodega") ?? 0;
            oBeTmp_VW_Despacho_Rep.IdProductoBodega = dr.Field<int?>("IdProductoBodega") ?? 0;
            oBeTmp_VW_Despacho_Rep.IdProductoEstado = dr.Field<int?>("IdProductoEstado") ?? 0;
            oBeTmp_VW_Despacho_Rep.IdPresentacion = dr.Field<int?>("IdPresentacion") ?? 0;
            oBeTmp_VW_Despacho_Rep.IdUnidadMedida = dr.Field<int?>("IdUnidadMedida") ?? 0;
            oBeTmp_VW_Despacho_Rep.IdRecepcion = dr.Field<int?>("IdRecepcion") ?? 0;
            oBeTmp_VW_Despacho_Rep.IdDespachoEnc = dr.Field<int?>("IdDespachoEnc") ?? 0;
            oBeTmp_VW_Despacho_Rep.IdDespachoDet = dr.Field<int?>("IdDespachoDet") ?? 0;
            oBeTmp_VW_Despacho_Rep.IdPedidoEnc = dr.Field<int?>("IdPedidoEnc") ?? 0;
            oBeTmp_VW_Despacho_Rep.Propietario = dr.Field<string>("Propietario") ?? "";
            oBeTmp_VW_Despacho_Rep.Codigo_Producto = dr.Field<string>("Codigo_Producto") ?? "";
            oBeTmp_VW_Despacho_Rep.Nombre_Producto = dr.Field<string>("Nombre_Producto") ?? "";
            oBeTmp_VW_Despacho_Rep.UM = dr.Field<string>("UM") ?? "";
            oBeTmp_VW_Despacho_Rep.Presentacion = dr.Field<string>("Presentacion") ?? "";
            oBeTmp_VW_Despacho_Rep.Factor = dr.Field<double?>("factor") ?? 0.0;
            oBeTmp_VW_Despacho_Rep.Estado = dr.Field<string>("Estado") ?? "";
            oBeTmp_VW_Despacho_Rep.Lote = dr.Field<string>("lote") ?? "";
            oBeTmp_VW_Despacho_Rep.Licencia = dr.Field<string>("licencia") ?? "";
            oBeTmp_VW_Despacho_Rep.Vence = dr.Field<DateTime?>("Vence") ?? DateTime.Now;
            oBeTmp_VW_Despacho_Rep.Ubicacion_Origen = dr.Field<string>("Ubicacion_Origen") ?? "";
            oBeTmp_VW_Despacho_Rep.Cantidad_pickeada = dr.Field<double?>("cantidad_pickeada") ?? 0.0;
            oBeTmp_VW_Despacho_Rep.Cantidad_verificada = dr.Field<double?>("cantidad_verificada") ?? 0.0;
            oBeTmp_VW_Despacho_Rep.Peso_Pickeado = dr.Field<double?>("Peso_Pickeado") ?? 0.0;
            oBeTmp_VW_Despacho_Rep.Peso_Verificado = dr.Field<double?>("Peso_Verificado") ?? 0.0;
            oBeTmp_VW_Despacho_Rep.CantidadDespachada = dr.Field<double?>("CantidadDespachada") ?? 0.0;
            oBeTmp_VW_Despacho_Rep.PesoDespachado = dr.Field<double?>("PesoDespachado") ?? 0.0;
            oBeTmp_VW_Despacho_Rep.Encontrado = dr.Field<bool?>("Encontrado") ?? false;
            oBeTmp_VW_Despacho_Rep.Acepto = dr.Field<bool?>("Acepto") ?? false;
            oBeTmp_VW_Despacho_Rep.No_Documento_WMS = dr.Field<int?>("No_Documento_WMS") ?? 0;
            oBeTmp_VW_Despacho_Rep.No_Referencia = dr.Field<string>("No_Referencia") ?? "";
            oBeTmp_VW_Despacho_Rep.Codigo_Cliente = dr.Field<string>("Codigo_Cliente") ?? "";
            oBeTmp_VW_Despacho_Rep.Nombre_Cliente = dr.Field<string>("Nombre_Cliente") ?? "";
            oBeTmp_VW_Despacho_Rep.Idubicacionvirtual = dr.Field<int?>("idubicacionvirtual") ?? 0;
            oBeTmp_VW_Despacho_Rep.Es_bodega_recepcion = dr.Field<bool?>("es_bodega_recepcion") ?? false;
            oBeTmp_VW_Despacho_Rep.Es_bodega_traslado = dr.Field<bool?>("es_bodega_traslado") ?? false;
            oBeTmp_VW_Despacho_Rep.No_pase = dr.Field<int?>("no_pase") ?? 0;
            oBeTmp_VW_Despacho_Rep.Observacion = dr.Field<string>("observacion") ?? "";
            oBeTmp_VW_Despacho_Rep.Numero = dr.Field<int?>("numero") ?? 0;
            oBeTmp_VW_Despacho_Rep.Marchamo = dr.Field<string>("marchamo") ?? "";
            oBeTmp_VW_Despacho_Rep.Codigo_Ruta = dr.Field<string>("Codigo_Ruta") ?? "";
            oBeTmp_VW_Despacho_Rep.Nombre_Ruta = dr.Field<string>("Nombre_Ruta") ?? "";
            oBeTmp_VW_Despacho_Rep.Placa_Vehiculo = dr.Field<string>("Placa_Vehiculo") ?? "";
            oBeTmp_VW_Despacho_Rep.Placa_Comercial = dr.Field<string>("Placa_Comercial") ?? "";
            oBeTmp_VW_Despacho_Rep.Marca_Vehiculo = dr.Field<string>("Marca_Vehiculo") ?? "";
            oBeTmp_VW_Despacho_Rep.Modelo_Vehiculo = dr.Field<string>("Modelo_Vehiculo") ?? "";
            oBeTmp_VW_Despacho_Rep.Nombre_Piloto = dr.Field<string>("Nombre_Piloto") ?? "";
            oBeTmp_VW_Despacho_Rep.Apellido_Piloto = dr.Field<string>("Apellido_Piloto") ?? "";
            oBeTmp_VW_Despacho_Rep.No_Carnet_Piloto = dr.Field<string>("No_Carnet_Piloto") ?? "";
            oBeTmp_VW_Despacho_Rep.No_Licencia_Piloto = dr.Field<string>("No_Licencia_Piloto") ?? "";
            oBeTmp_VW_Despacho_Rep.Fecha = dr.Field<DateTime?>("fecha") ?? DateTime.Now;
            oBeTmp_VW_Despacho_Rep.clasificacion = dr.Field<string>("clasificacion") ?? "";
            oBeTmp_VW_Despacho_Rep.marca = dr.Field<string>("marca") ?? "";
            oBeTmp_VW_Despacho_Rep.familia = dr.Field<string>("familia") ?? "";
            oBeTmp_VW_Despacho_Rep.parametro_a = dr.Field<string>("parametro_a") ?? "";
            oBeTmp_VW_Despacho_Rep.parametro_b = dr.Field<string>("parametro_b") ?? "";
            oBeTmp_VW_Despacho_Rep.numero_orden_pedido = dr.Field<string>("numero_orden_pedido") ?? "";
            oBeTmp_VW_Despacho_Rep.codigo_poliza_pedido = dr.Field<string>("codigo_poliza_pedido") ?? "";
            oBeTmp_VW_Despacho_Rep.numero_orden_ingreso = dr.Field<string>("numero_orden_ingreso") ?? "";
            oBeTmp_VW_Despacho_Rep.codigo_poliza_ingreso = dr.Field<string>("codigo_poliza_ingreso") ?? "";
            oBeTmp_VW_Despacho_Rep.codigo_regimen_salida = dr.Field<string>("codigo_regimen_salida") ?? "";
            oBeTmp_VW_Despacho_Rep.placa_contenedor_salida = dr.Field<string>("placa_contenedor_salida") ?? "";
            oBeTmp_VW_Despacho_Rep.Dua_salida = dr.Field<string>("dua_salida") ?? "";
            oBeTmp_VW_Despacho_Rep.Talla = dr.Field<string>("Talla") ?? "";
            oBeTmp_VW_Despacho_Rep.Color = dr.Field<string>("Color") ?? "";
        }

        public static List<clsBeVW_Despacho_Rep> Get_All_By_Rango_Fechas(IConfiguration config, DateTime pFechaDel, DateTime pFechaAl, clsBeBodega pBodega)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                List<clsBeVW_Despacho_Rep> lReturnList = new List<clsBeVW_Despacho_Rep>();

                string vSQL = string.Format("SELECT * FROM VW_Despacho_Rep_Det_I      " +
                             " WHERE IdBodega = @pIdBodega AND CAST(Fecha AS DATE) BETWEEN '{0}' AND '{1}'",  
                             FormatoFechas.fFecha(pFechaDel),
                             FormatoFechas.fFecha(pFechaAl));

                if (pBodega.Es_bodega_fiscal)
                {
                    vSQL += " And poliza_salida_activa=1";
                }

                vSQL += " Order By fecha desc";

                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

                using (SqlCommand cmd = new SqlCommand(vSQL, lConnection, lTransaction) { CommandType = CommandType.Text })
                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    dad.SelectCommand.Parameters.AddWithValue("@pIdBodega", pBodega.IdBodega);
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        clsBeVW_Despacho_Rep vBeTmp_VW_Despacho_Rep = new clsBeVW_Despacho_Rep();

                        foreach (DataRow dr in dt.Rows)
                        {
                            vBeTmp_VW_Despacho_Rep = new clsBeVW_Despacho_Rep();
                            Cargar(ref vBeTmp_VW_Despacho_Rep, dr);
                            lReturnList.Add(vBeTmp_VW_Despacho_Rep);
                        }
                    }
                }

                lTransaction.Commit();
                return lReturnList;
            }
            catch (Exception)
            {
                if (lTransaction != null) lTransaction.Rollback();
                throw;
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
            }
        }

        public static string? Get_Ultimo_Lote_By_IdCliente(IConfiguration config, int pIdCliente, int pIdProductoBodega)
        {
            SqlConnection lConection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;
            string? result = "";

            try
            {
                string vSQL = @"SELECT TOP(1) lote FROM VW_Lotes_Despacho
                          WHERE IdCliente = @pIdCliente 
                          AND IdProductoBodega = @pIdProductoBodega";
                vSQL += " order by fecha_despacho desc";

                lConection.Open(); lTransaction = lConection.BeginTransaction(IsolationLevel.ReadUncommitted);

                using (SqlCommand cmd = new SqlCommand(vSQL, lConection, lTransaction) { CommandType = CommandType.Text })
                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    dad.SelectCommand.Parameters.AddWithValue("@pIdCliente", pIdCliente);
                    dad.SelectCommand.Parameters.AddWithValue("@pIdProductoBodega", pIdProductoBodega);
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        string? vLoteAlfanumerico = (dt.Rows[0]["lote"] == DBNull.Value || dt.Rows[0]["lote"] == null) ? "0" : Convert.ToString(dt.Rows[0]["lote"]);

                        string vSQL1 = @"SELECT TOP(1) lote_numerico 
                                   FROM trans_re_det_lote_num 
                                   WHERE lote = @LoteAlfanumerico
                                   AND IdProductoBodega = @pIdProductoBodega";
                        vSQL1 += " order by fechaingreso desc";

                        using (SqlCommand cmd1 = new SqlCommand(vSQL1, lConection, lTransaction) { CommandType = CommandType.Text })
                        using (SqlDataAdapter dad1 = new SqlDataAdapter(cmd1))
                        {
                            dad1.SelectCommand.Parameters.AddWithValue("@LoteAlfanumerico", vLoteAlfanumerico);
                            dad1.SelectCommand.Parameters.AddWithValue("@pIdProductoBodega", pIdProductoBodega);
                            DataTable dt1 = new DataTable();
                            dad1.Fill(dt1);

                            if (dt1.Rows.Count > 0)
                            {
                                result = (dt1.Rows[0]["lote_numerico"] == DBNull.Value) ? "0" : Convert.ToString(dt1.Rows[0]["lote_numerico"]);
                            }
                        }
                    }
                }

                lTransaction.Commit();
            }
            catch (Exception)
            {
                if (lTransaction != null) lTransaction.Rollback();
                throw;
            }
            finally
            {
                if (lConection != null && lConection.State == ConnectionState.Open) lConection.Close();
            }

            return result;
        }

        public static string? Get_Ultimo_Lote_By_IdCliente(int pIdCliente, int pIdProductoBodega, SqlConnection lConnection, SqlTransaction lTransaction)
        {
            string? result = "";

            try
            {
                string vSQL = @"SELECT TOP(1) lote FROM VW_Lotes_Despacho
                          WHERE IdCliente = @pIdCliente 
                          AND IdProductoBodega = @pIdProductoBodega";
                vSQL += " order by fecha_despacho desc";

                using (SqlCommand cmd = new SqlCommand(vSQL, lConnection, lTransaction) { CommandType = CommandType.Text })
                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    dad.SelectCommand.Parameters.AddWithValue("@pIdCliente", pIdCliente);
                    dad.SelectCommand.Parameters.AddWithValue("@pIdProductoBodega", pIdProductoBodega);
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        string? vLoteAlfanumerico = (dt.Rows[0]["lote"] == DBNull.Value) ? "0" : Convert.ToString(dt.Rows[0]["lote"]);

                        string vSQL1 = @"SELECT TOP(1) lote_numerico 
                                   FROM trans_re_det_lote_num 
                                   WHERE lote = @LoteAlfanumerico
                                   AND IdProductoBodega = @pIdProductoBodega";
                        vSQL1 += " order by fechaingreso desc";

                        using (SqlCommand cmd1 = new SqlCommand(vSQL1, lConnection, lTransaction) { CommandType = CommandType.Text })
                        using (SqlDataAdapter dad1 = new SqlDataAdapter(cmd1))
                        {
                            dad1.SelectCommand.Parameters.AddWithValue("@LoteAlfanumerico", vLoteAlfanumerico);
                            dad1.SelectCommand.Parameters.AddWithValue("@pIdProductoBodega", pIdProductoBodega);
                            DataTable dt1 = new DataTable();
                            dad1.Fill(dt1);

                            if (dt1.Rows.Count > 0)
                            {
                                result = (dt1.Rows[0]["lote_numerico"] == DBNull.Value) ? "0" : Convert.ToString(dt1.Rows[0]["lote_numerico"]);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }        
    }

}
