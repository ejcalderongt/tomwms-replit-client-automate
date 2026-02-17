using Microsoft.Data.SqlClient;
using WMS.DALCore.Stock;
using WMS.EntityCore.Picking;
using WMS.EntityCore.Stock;

namespace WMS.DALCore.Picking
{
    public class clsLnTrans_picking_det_parametros
    {
        private static clsInsert Ins = new clsInsert();
        private static clsUpdate Upd = new clsUpdate();
        public static void Cargar(ref clsBeTrans_picking_det_parametros oBeTrans_picking_det_parametros, ref DataRow dr)
        {
            try
            {
                oBeTrans_picking_det_parametros.IdParametroPicking = (dr["IdParametroPicking"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IdParametroPicking"]);
                oBeTrans_picking_det_parametros.IdPickingDet = (dr["IdPickingDet"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IdPickingDet"]);
                oBeTrans_picking_det_parametros.IdProductoParametro = (dr["IdProductoParametro"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IdProductoParametro"]);
                oBeTrans_picking_det_parametros.Valor_texto =dr["valor_texto"] as string ?? string.Empty;
                oBeTrans_picking_det_parametros.Valor_numerico = (dr["valor_numerico"] == DBNull.Value) ? 0.0 : Convert.ToDouble(dr["valor_numerico"]);
                oBeTrans_picking_det_parametros.Valor_fecha = (dr["valor_fecha"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["valor_fecha"]);
                oBeTrans_picking_det_parametros.Valor_logico = (dr["valor_logico"] == DBNull.Value) ? false : Convert.ToBoolean(dr["valor_logico"]);
                oBeTrans_picking_det_parametros.User_agr = dr["user_agr"] as string ?? string.Empty;
                oBeTrans_picking_det_parametros.Fec_agr = (dr["fec_agr"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["fec_agr"]);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public static int Insertar(clsBeTrans_picking_det_parametros oBeTrans_picking_det_parametros, SqlConnection pConection, SqlTransaction pTransaction)
        {
            int rowsAffected = 0;

            try
            {
                Ins.Init("trans_picking_det_parametros");
                Ins.Add("idparametropicking", "@idparametropicking", "F");
                Ins.Add("idpickingdet", "@idpickingdet", "F");
                Ins.Add("idproductoparametro", "@idproductoparametro", "F");
                Ins.Add("valor_texto", "@valor_texto", "F");
                Ins.Add("valor_numerico", "@valor_numerico", "F");
                Ins.Add("valor_fecha", "@valor_fecha", "F");
                Ins.Add("valor_logico", "@valor_logico", "F");
                Ins.Add("user_agr", "@user_agr", "F");
                Ins.Add("fec_agr", "@fec_agr", "F");

                string sp = Ins.SQL();

                using (var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
                {
                    cmd.Parameters.Add(new SqlParameter("@IDPARAMETROPICKING", oBeTrans_picking_det_parametros.IdParametroPicking));
                    cmd.Parameters.Add(new SqlParameter("@IDPICKINGDET", oBeTrans_picking_det_parametros.IdPickingDet));
                    cmd.Parameters.Add(new SqlParameter("@IDPRODUCTOPARAMETRO", oBeTrans_picking_det_parametros.IdProductoParametro));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_TEXTO", oBeTrans_picking_det_parametros.Valor_texto ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_NUMERICO", oBeTrans_picking_det_parametros.Valor_numerico ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_FECHA", oBeTrans_picking_det_parametros.Valor_fecha ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_LOGICO", oBeTrans_picking_det_parametros.Valor_logico ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@USER_AGR", oBeTrans_picking_det_parametros.User_agr));
                    cmd.Parameters.Add(new SqlParameter("@FEC_AGR", oBeTrans_picking_det_parametros.Fec_agr));

                    rowsAffected = cmd.ExecuteNonQuery();
                }

                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Actualizar(clsBeTrans_picking_det_parametros oBeTrans_picking_det_parametros, SqlConnection pConection, SqlTransaction pTransaction)
        {
            int rowsAffected = 0;

            try
            {
                Upd.Init("trans_picking_det_parametros");
                Upd.Add("idparametropicking", "@idparametropicking", "F");
                Upd.Add("idpickingdet", "@idpickingdet", "F");
                Upd.Add("idproductoparametro", "@idproductoparametro", "F");
                Upd.Add("valor_texto", "@valor_texto", "F");
                Upd.Add("valor_numerico", "@valor_numerico", "F");
                Upd.Add("valor_fecha", "@valor_fecha", "F");
                Upd.Add("valor_logico", "@valor_logico", "F");
                Upd.Add("user_agr", "@user_agr", "F");
                Upd.Add("fec_agr", "@fec_agr", "F");
                Upd.Where("IdParametroPicking = @IdParametroPicking");

                string sp = Upd.SQL();

                using (var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
                {
                    cmd.Parameters.Add(new SqlParameter("@IDPARAMETROPICKING", oBeTrans_picking_det_parametros.IdParametroPicking));
                    cmd.Parameters.Add(new SqlParameter("@IDPICKINGDET", oBeTrans_picking_det_parametros.IdPickingDet));
                    cmd.Parameters.Add(new SqlParameter("@IDPRODUCTOPARAMETRO", oBeTrans_picking_det_parametros.IdProductoParametro));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_TEXTO", oBeTrans_picking_det_parametros.Valor_texto ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_NUMERICO", oBeTrans_picking_det_parametros.Valor_numerico ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_FECHA", oBeTrans_picking_det_parametros.Valor_fecha ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_LOGICO", oBeTrans_picking_det_parametros.Valor_logico ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@USER_AGR", oBeTrans_picking_det_parametros.User_agr));
                    cmd.Parameters.Add(new SqlParameter("@FEC_AGR", oBeTrans_picking_det_parametros.Fec_agr));

                    rowsAffected = cmd.ExecuteNonQuery();
                }

                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Eliminar(clsBeTrans_picking_det_parametros oBeTrans_picking_det_parametros, SqlConnection pConection, SqlTransaction pTransaction)
        {
            int rowsAffected = 0;

            try
            {
                string sp = @"Delete from Trans_picking_det_parametros
                        Where(IdParametroPicking = @IdParametroPicking)";

                using (var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
                {
                    cmd.Parameters.Add(new SqlParameter("@IDPARAMETROPICKING", oBeTrans_picking_det_parametros.IdParametroPicking));
                    rowsAffected = cmd.ExecuteNonQuery();
                }

                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool Insertar_Parametros_Stock_Para_Picking(int pIdStock,
                                                                  int pIdPickingDet,
                                                                  SqlConnection lConnection,
                                                                  SqlTransaction lTransaction)
        {
            bool result = false;

            try
            {
                clsBeTrans_picking_det_parametros? BeParametros;
                List<clsBeStock_parametro>? lStockP = clsLnStock_parametro.Get_All_By_IdStock(pIdStock, lConnection, lTransaction);

                if (lStockP != null)
                {
                    int MaxIDS = MaxID(lConnection, lTransaction) + 1;

                    foreach (clsBeStock_parametro Obj in lStockP)
                    {
                        BeParametros = new clsBeTrans_picking_det_parametros();
                        BeParametros.IdParametroPicking = MaxIDS;
                        BeParametros.IdPickingDet = pIdPickingDet;
                        BeParametros.IdProductoParametro = Obj.IdProductoParametro;
                        BeParametros.Valor_texto = Obj.Valor_texto;
                        BeParametros.Valor_numerico = Obj.Valor_numerico;
                        BeParametros.Valor_logico = Obj.Valor_logico;
                        BeParametros.Valor_fecha = Obj.Valor_fecha;
                        BeParametros.Fec_agr = DateTime.Now;
                        BeParametros.User_agr = Obj.User_agr;
                        BeParametros.IsNew = true;

                        Insertar(BeParametros, lConnection, lTransaction);

                        MaxIDS++;
                    }
                }

                result = true;
            }
            catch (Exception)
            {                
                throw;
            }

            return result;
        }

        public static int MaxID(SqlConnection pConnection, SqlTransaction pTransaction)
        {
            try
            {
                int lMax = 0;

                string vSQL = "SELECT ISNULL(Max(IdParametroPicking),0) FROM trans_picking_det_parametros";

                using (SqlCommand lCommand = new SqlCommand(vSQL, pConnection, pTransaction))
                {
                    lCommand.CommandType = CommandType.Text;

                    object lReturnValue = lCommand.ExecuteScalar();

                    if (lReturnValue != DBNull.Value && lReturnValue != null)
                    {
                        lMax = Convert.ToInt32(lReturnValue);
                    }
                }

                return lMax;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
