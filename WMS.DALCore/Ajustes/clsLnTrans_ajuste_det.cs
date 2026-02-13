using Microsoft.Data.SqlClient;
using System.Reflection;
using WMS.EntityCore.Ajustes;

namespace WMS.DALCore.Ajustes
{
    public static class clsLnTrans_ajuste_det
    {
        public static bool GetSingle(ref clsBeTrans_ajuste_det pBeTrans_ajuste_det,
                                     SqlConnection lConnection,
                                     SqlTransaction lTransaction)
        {
            var ok = false;

            try
            {
                const string sql = @"SELECT * 
                                 FROM Trans_ajuste_det
                                 WHERE idajustedet = @idajustedet;";

                using var cmd = new SqlCommand(sql, lConnection, lTransaction)
                {
                    CommandType = CommandType.Text
                };
                
                cmd.Parameters.Add(new SqlParameter("@idajustedet", SqlDbType.Int)
                {
                    Value = pBeTrans_ajuste_det.IdAjusteDet
                });

                using var dad = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                dad.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    Cargar(ref pBeTrans_ajuste_det, dt.Rows[0]);
                    ok = true;
                }

                return ok;
            }
            catch (Exception ex)
            {
                var vMsgError = $"{MethodBase.GetCurrentMethod()?.Name} {ex.Message}";                
                throw;
            }
        }

        public static void Cargar(ref clsBeTrans_ajuste_det oBeTrans_ajuste_det, DataRow dr)
        {
            try
            {
                // Helper local para DBNull
                static T Get<T>(DataRow row, string col, T def)
                {
                    var v = row[col];
                    if (v == null || v == DBNull.Value) return def;

                    // Si ya es del tipo, devuelve directo
                    if (v is T t) return t;

                    // Manejo especial bool cuando viene como 0/1 u otros
                    if (typeof(T) == typeof(bool))
                    {
                        // v podría ser byte/int/short/string
                        var b = Convert.ToBoolean(v);
                        return (T)(object)b;
                    }

                    // Convert.ChangeType cubre int/double/string/datetime, etc.
                    return (T)Convert.ChangeType(v, typeof(T));
                }

                oBeTrans_ajuste_det.IdAjusteDet = Get(dr, "idajustedet", 0);
                oBeTrans_ajuste_det.IdAjusteEnc = Get(dr, "idajusteenc", 0);
                oBeTrans_ajuste_det.IdStock = Get(dr, "IdStock", 0);
                oBeTrans_ajuste_det.IdPropietarioBodega = Get(dr, "IdPropietarioBodega", 0);
                oBeTrans_ajuste_det.IdProductoBodega = Get(dr, "IdProductoBodega", 0);
                oBeTrans_ajuste_det.IdProductoEstado = Get(dr, "IdProductoEstado", 0);
                oBeTrans_ajuste_det.IdPresentacion = Get(dr, "IdPresentacion", 0);
                oBeTrans_ajuste_det.IdUnidadMedida = Get(dr, "IdUnidadMedida", 0);
                oBeTrans_ajuste_det.IdUbicacion = Get(dr, "IdUbicacion", 0);

                oBeTrans_ajuste_det.Lote_original = Get(dr, "lote_original", "");
                oBeTrans_ajuste_det.Lote_nuevo = Get(dr, "lote_nuevo", "");

                oBeTrans_ajuste_det.Fecha_vence_original = Get(dr, "fecha_vence_original", DateTime.Now);
                oBeTrans_ajuste_det.Fecha_vence_nueva = Get(dr, "fecha_vence_nueva", DateTime.Now);

                oBeTrans_ajuste_det.Peso_original = Get(dr, "peso_original", 0.0);
                oBeTrans_ajuste_det.Peso_nuevo = Get(dr, "peso_nuevo", 0.0);

                oBeTrans_ajuste_det.Cantidad_original = Get(dr, "cantidad_original", 0.0);
                oBeTrans_ajuste_det.Cantidad_nueva = Get(dr, "cantidad_nueva", 0.0);

                oBeTrans_ajuste_det.Codigo_producto = Get(dr, "codigo_producto", "");
                oBeTrans_ajuste_det.Nombre_producto = Get(dr, "nombre_producto", "");

                oBeTrans_ajuste_det.Idtipoajuste = Get(dr, "idtipoajuste", 0);
                oBeTrans_ajuste_det.IdMotivoAjuste = Get(dr, "idmotivoajuste", 0);

                oBeTrans_ajuste_det.Observacion = Get(dr, "observacion", "");
                oBeTrans_ajuste_det.Codigo_ajuste = Get(dr, "codigo_ajuste", "");

                oBeTrans_ajuste_det.Enviado = Get(dr, "enviado", false);
                oBeTrans_ajuste_det.IdBodegaERP = Get(dr, "IdBodegaERP", 0);

                oBeTrans_ajuste_det.lic_plate = Get(dr, "lic_plate", "");
                oBeTrans_ajuste_det.referencia_ajuste_erp = Get(dr, "referencia_ajuste_erp", "");
                oBeTrans_ajuste_det.estado_ajuste_erp = Get(dr, "estado_ajuste_erp", false);

                oBeTrans_ajuste_det.IdProductoTallaColor_origen = Get(dr, "IdProductoTallaColor_origen", 0);
                oBeTrans_ajuste_det.Talla_origen = Get(dr, "Talla_origen", "");
                oBeTrans_ajuste_det.Color_origen = Get(dr, "Color_origen", "");

                oBeTrans_ajuste_det.IdProductoTallaColor_destino = Get(dr, "IdProductoTallaColor_destino", 0);
                oBeTrans_ajuste_det.Talla_destino = Get(dr, "Talla_destino", "");
                oBeTrans_ajuste_det.Color_destino = Get(dr, "Color_destino", "");
            }
            catch (Exception ex)
            {
                var vMsgError = $"{MethodBase.GetCurrentMethod()?.Name} {ex.Message}";                
                throw;
            }
        }
        public static int Actualizar_Estado_Enviado(clsBeTrans_ajuste_det oBeTrans_ajuste_det,SqlConnection lConnection, SqlTransaction lTransaction)
        {
            try
            {
                const string sql = @"UPDATE trans_ajuste_det
                                     SET enviado = @ENVIADO
                                     WHERE idajustedet = @IDAJUSTEDET;
                                     ";

                using var cmd = new SqlCommand(sql, lConnection, lTransaction)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.Add(new SqlParameter("@IDAJUSTEDET", SqlDbType.Int)
                {
                    Value = oBeTrans_ajuste_det.IdAjusteDet
                });

                cmd.Parameters.Add(new SqlParameter("@ENVIADO", SqlDbType.Bit)
                {
                    Value = oBeTrans_ajuste_det.Enviado
                });

                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var vMsgError = $"{MethodBase.GetCurrentMethod()?.Name} {ex.Message}";                
                throw; // no pierde stack
            }
        }
    }
}