namespace WMS.EntityCore.Trans_oc
{
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;    
    using System.Data;    

    public class clsLnTrans_oc_estado
    {
        private static clsInsert Ins = new clsInsert();
        private static clsUpdate Upd = new clsUpdate();
        public static void Cargar(ref clsBeTrans_oc_estado oBeTrans_oc_estado, DataRow dr)
        {
            try
            {
                oBeTrans_oc_estado.IdEstadoOC = dr["IdEstadoOC"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdEstadoOC"]);
                oBeTrans_oc_estado.Nombre = dr["Nombre"] == DBNull.Value ? "" : dr["Nombre"]?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                throw new Exception("Trans_oc_estado_Cargar: " + ex.Message);
            }
        }

        public int Insertar(IConfiguration config, ref clsBeTrans_oc_estado oBeTrans_oc_estado, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;            
            
            try
            {
                Ins.Init("trans_oc_estado");
                Ins.Add("idestadooc", "@idestadooc", "F");
                Ins.Add("nombre", "@nombre", "F");

                string sp = Ins.SQL();
                SqlCommand cmd = new SqlCommand { CommandType = CommandType.Text };

                bool Es_Transaccion_Remota = (pConection != null && pTransaction != null);

                if (Es_Transaccion_Remota)
                {
                    cmd = new SqlCommand(sp, pConection, pTransaction);
                }
                else
                {
                    lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                    cmd = new SqlCommand(sp, lConnection, lTransaction);
                }

                cmd.Parameters.Add(new SqlParameter("@IDESTADOOC", oBeTrans_oc_estado.IdEstadoOC));
                cmd.Parameters["@IDESTADOOC"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", oBeTrans_oc_estado.Nombre));

                int rowsAffected = cmd.ExecuteNonQuery();
                cmd.Dispose();

                if (!Es_Transaccion_Remota && lTransaction != null) lTransaction.Commit();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw new Exception("Trans_oc_estado_Insertar: " + ex.Message);
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                lConnection.Dispose();
            }
        }

        public int Actualizar(ref clsBeTrans_oc_estado oBeTrans_oc_estado, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Upd.Init("trans_oc_estado");
                Upd.Add("idestadooc", "@idestadooc", "F");
                Upd.Add("nombre", "@nombre", "F");
                Upd.Where("IdEstadoOC = @IdEstadoOC");

                string sp = Upd.SQL();

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@IDESTADOOC", oBeTrans_oc_estado.IdEstadoOC));
                    cmd.Parameters.Add(new SqlParameter("@NOMBRE", oBeTrans_oc_estado.Nombre ?? string.Empty));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Trans_oc_estado_Actualizar: " + ex.Message);
            }
        }

        public int Eliminar(IConfiguration config, ref clsBeTrans_oc_estado oBeTrans_oc_estado, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                const string sp = "DELETE FROM Trans_oc_estado WHERE (IdEstadoOC = @IdEstadoOC)";
                bool Es_Transaccion_Remota = (pConection != null && pTransaction != null);
                SqlCommand cmd = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text };

                if (Es_Transaccion_Remota)
                {
                    cmd = new SqlCommand(sp, pConection, pTransaction);
                }
                else
                {
                    lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                    cmd = new SqlCommand(sp, lConnection, lTransaction);
                }

                cmd.Parameters.Add(new SqlParameter("@IDESTADOOC", oBeTrans_oc_estado.IdEstadoOC));

                int rowsAffected = cmd.ExecuteNonQuery();
                cmd.Dispose();

                if (!Es_Transaccion_Remota && lTransaction != null) lTransaction.Commit();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw new Exception("Trans_oc_estado_Eliminar: " + ex.Message);
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                lConnection.Dispose();
            }
        }

        public int EliminarTodos(IConfiguration config, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                const string sp = "DELETE FROM Trans_oc_estado";
                SqlCommand cmd = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text };
                bool Es_Transaccion_Remota = (pConection != null && pTransaction != null);

                if (Es_Transaccion_Remota)
                {
                    cmd = new SqlCommand(sp, pConection);
                }
                else
                {
                    lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                    cmd = new SqlCommand(sp, lConnection, lTransaction);
                }

                int rowsAffected = cmd.ExecuteNonQuery();
                cmd.Dispose();

                if (!Es_Transaccion_Remota && lTransaction !=null) lTransaction.Commit();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw new Exception("Trans_oc_estado_Eliminar: " + ex.Message);
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                lConnection.Dispose();
            }
        }

        public static List<clsBeTrans_oc_estado> GetAll(IConfiguration config)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                List<clsBeTrans_oc_estado> lReturnList = new List<clsBeTrans_oc_estado>();
                const string sp = "SELECT * FROM Trans_oc_estado";
                SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                SqlDataAdapter dad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                dad.Fill(dt);
                clsBeTrans_oc_estado vBeTrans_oc_estado;

                foreach (DataRow dr in dt.Rows)
                {
                    vBeTrans_oc_estado = new clsBeTrans_oc_estado();                    
                    Cargar(ref vBeTrans_oc_estado, dr);
                    lReturnList.Add(vBeTrans_oc_estado);
                }

                cmd.Dispose();
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
                if (lTransaction != null) lTransaction.Dispose();
                if (lConnection != null) lConnection.Dispose();
            }
        }

        public static DataTable GetAllForCombo(IConfiguration config)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                const string sp = "SELECT IdEstadoOC as IdEstado, Nombre FROM Trans_oc_estado";
                SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                SqlDataAdapter dad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                dad.Fill(dt);
                cmd.Dispose();
                lTransaction.Commit();
                return dt;
            }
            catch (Exception)
            {
                if (lTransaction != null) lTransaction.Rollback();
                throw;
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                if (lTransaction != null) lTransaction.Dispose();
                if (lConnection != null) lConnection.Dispose();
            }
        }

        public static bool GetSingle(IConfiguration config, ref clsBeTrans_oc_estado pBeTrans_oc_estado)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;
            bool result = false;

            try
            {
                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                const string sp = "SELECT * FROM Trans_oc_estado WHERE (IdEstadoOC = @IdEstadoOC)";
                SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                SqlDataAdapter dad = new SqlDataAdapter(cmd);

                dad.SelectCommand.Parameters.Add(new SqlParameter("@IDESTADOOC", pBeTrans_oc_estado.IdEstadoOC));
                DataTable dt = new DataTable();
                dad.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    var vrow = dt.Rows[0];  
                    Cargar(ref pBeTrans_oc_estado, vrow);
                    result = true;
                }

                lTransaction.Commit();
                return result;
            }
            catch (Exception)
            {
                if (lTransaction != null) lTransaction.Rollback();
                throw;
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                if (lTransaction != null) lTransaction.Dispose();
                if (lConnection != null) lConnection.Dispose();
            }
        }

        public static clsBeTrans_oc_estado? GetSingle(clsBeTrans_oc_estado? estado,
                                                      SqlConnection lConnection,
                                                      SqlTransaction lTransaction)
        {
            try
            {
                const string sp = @"SELECT * FROM Trans_oc_estado WHERE (IdEstadoOC = @IdEstadoOC)";
                using var cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                using var dad = new SqlDataAdapter(cmd);

                cmd.Parameters.Add(new SqlParameter("@IdEstadoOC", estado?.IdEstadoOC ?? 0));

                var dt = new DataTable();
                dad.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    var vrow = dt.Rows[0];
                    var target = estado ?? new clsBeTrans_oc_estado();
                    Cargar(ref target, vrow);
                    return target;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }
    }
}