namespace WMS.DALCore.Trans_oc
{
    using System;
    using System.Data;
    using System.Collections.Generic;
    using WMS.EntityCore.Trans_oc;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;

    public class clsLnTrans_oc_embarcador
    {
        private static clsInsert Ins = new clsInsert();
        private static clsUpdate Upd = new clsUpdate();
        public static void Cargar(ref clsBeTrans_oc_embarcador oBeTrans_oc_embarcador, DataRow dr)
        {
            try
            {
                oBeTrans_oc_embarcador.IdEmbarcador = (dr["IdEmbarcador"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IdEmbarcador"]);
                oBeTrans_oc_embarcador.Codigo = (dr["Codigo"] == DBNull.Value) ? "" : Convert.ToString(dr["Codigo"]) ?? "";
                oBeTrans_oc_embarcador.Nombre = (dr["Nombre"] == DBNull.Value) ? "" : Convert.ToString(dr["Nombre"]) ?? "";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Insertar(ref clsBeTrans_oc_embarcador oBeTrans_oc_embarcador, SqlConnection pConection, SqlTransaction pTransaction)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                Ins.Init("trans_oc_embarcador");
                Ins.Add("idembarcador", "@idembarcador", "F");
                Ins.Add("codigo", "@codigo", "F");
                Ins.Add("nombre", "@nombre", "F");

                string sp = Ins.SQL();
                cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

                cmd.Parameters.Add(new SqlParameter("@IDEMBARCADOR", oBeTrans_oc_embarcador.IdEmbarcador));
                cmd.Parameters.Add(new SqlParameter("@CODIGO", oBeTrans_oc_embarcador.Codigo));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", oBeTrans_oc_embarcador.Nombre));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmd?.Dispose();
            }
        }

        public static int Actualizar(ref clsBeTrans_oc_embarcador oBeTrans_oc_embarcador, SqlConnection pConection, SqlTransaction pTransaction)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                Upd.Init("trans_oc_embarcador");
                Upd.Add("idembarcador", "@idembarcador", "F");
                Upd.Add("codigo", "@codigo", "F");
                Upd.Add("nombre", "@nombre", "F");
                Upd.Where("IdEmbarcador = @IdEmbarcador");

                string sp = Upd.SQL();
                cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

                cmd.Parameters.Add(new SqlParameter("@IDEMBARCADOR", oBeTrans_oc_embarcador.IdEmbarcador));
                cmd.Parameters.Add(new SqlParameter("@CODIGO", oBeTrans_oc_embarcador.Codigo));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", oBeTrans_oc_embarcador.Nombre));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmd?.Dispose();
            }
        }

        public static int Eliminar(ref clsBeTrans_oc_embarcador oBeTrans_oc_embarcador, SqlConnection pConection, SqlTransaction pTransaction)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                const string sp = " Delete from Trans_oc_embarcador Where(IdEmbarcador = @IdEmbarcador)";
                cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

                cmd.Parameters.Add(new SqlParameter("@IDEMBARCADOR", oBeTrans_oc_embarcador.IdEmbarcador));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmd?.Dispose();
            }
        }    

        public static List<clsBeTrans_oc_embarcador> Get_All(IConfiguration config)
        {
            List<clsBeTrans_oc_embarcador> lReturnList = new List<clsBeTrans_oc_embarcador>();

            try
            {
                const string sp = "SELECT * FROM Trans_oc_embarcador";

                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    lConnection.Open();

                    using (SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                    {
                        using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                        {
                            lDTA.SelectCommand.CommandType = CommandType.Text;
                            lDTA.SelectCommand.Transaction = lTransaction;
                            DataTable lDataTable = new DataTable();
                            lDTA.Fill(lDataTable);

                            clsBeTrans_oc_embarcador vBeTrans_oc_embarcador;

                            foreach (DataRow dr in lDataTable.Rows)
                            {
                                vBeTrans_oc_embarcador = new clsBeTrans_oc_embarcador();
                                Cargar(ref vBeTrans_oc_embarcador, dr);
                                lReturnList.Add(vBeTrans_oc_embarcador);
                            }
                        }

                        lTransaction.Commit();
                    }

                    lConnection.Close();
                }

                return lReturnList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void GetSingle(IConfiguration config, ref clsBeTrans_oc_embarcador pBeTrans_oc_embarcador)
        {
            try
            {
                const string sp = "SELECT * FROM Trans_oc_embarcador Where(IdEmbarcador = @IdEmbarcador)";

                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    lConnection.Open();

                    using (SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                    {
                        using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                        {
                            lDTA.SelectCommand.CommandType = CommandType.Text;
                            lDTA.SelectCommand.Transaction = lTransaction;
                            DataTable lDataTable = new DataTable();
                            lDTA.Fill(lDataTable);

                            clsBeTrans_oc_embarcador vBeTrans_oc_embarcador = new clsBeTrans_oc_embarcador();

                            if (lDataTable != null && lDataTable.Rows.Count > 0)
                            {
                                Cargar(ref vBeTrans_oc_embarcador, lDataTable.Rows[0]);
                            }
                        }

                        lTransaction.Commit();
                    }

                    lConnection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int MaxID(IConfiguration config)
        {
            try
            {
                int lMax = 0;
                const string sp = "SELECT ISNULL(Max(IdEmbarcador),0) FROM Trans_oc_embarcador";

                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    lConnection.Open();

                    using (SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                    {
                        using (SqlCommand lCommand = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text })
                        {
                            object lReturnValue = lCommand.ExecuteScalar();
                            if (lReturnValue != DBNull.Value && lReturnValue != null)
                            {
                                lMax = Convert.ToInt32(lReturnValue);
                            }
                        }

                        lTransaction.Commit();
                    }

                    lConnection.Close();
                }

                return lMax;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static clsBeTrans_oc_embarcador? Get_Single_By_Nombre(IConfiguration config, string pNombre)
        {
            clsBeTrans_oc_embarcador? result = null;

            try
            {
                const string sp = "SELECT * FROM Trans_oc_embarcador Where(Nombre = @Nombre) ";

                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    lConnection.Open();

                    using (SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                    {
                        using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                        {
                            lDTA.SelectCommand.CommandType = CommandType.Text;
                            lDTA.SelectCommand.Transaction = lTransaction;
                            lDTA.SelectCommand.Parameters.AddWithValue("@Nombre", pNombre.Trim());

                            DataTable lDataTable = new DataTable();
                            lDTA.Fill(lDataTable);

                            clsBeTrans_oc_embarcador vBeTrans_oc_embarcador = new clsBeTrans_oc_embarcador();

                            if (lDataTable != null && lDataTable.Rows.Count > 0)
                            {
                                Cargar(ref vBeTrans_oc_embarcador, lDataTable.Rows[0]);
                                result = vBeTrans_oc_embarcador;
                            }
                        }

                        lTransaction.Commit();
                    }

                    lConnection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public static clsBeTrans_oc_embarcador? Get_Single_By_Nombre(string pNombre, SqlConnection lConnection, SqlTransaction lTransaction)
        {
            clsBeTrans_oc_embarcador? result = null;

            try
            {
                const string sp = "SELECT * FROM Trans_oc_embarcador Where(Nombre = @Nombre) ";

                using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    lDTA.SelectCommand.Transaction = lTransaction;
                    lDTA.SelectCommand.Parameters.AddWithValue("@Nombre", pNombre.Trim());

                    DataTable lDataTable = new DataTable();
                    lDTA.Fill(lDataTable);

                    clsBeTrans_oc_embarcador vBeTrans_oc_embarcador = new clsBeTrans_oc_embarcador();

                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        Cargar(ref vBeTrans_oc_embarcador, lDataTable.Rows[0]);
                        result = vBeTrans_oc_embarcador;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public static int MaxID(SqlConnection lConnection, SqlTransaction lTransaction)
        {
            try
            {
                int lMax = 0;
                const string sp = "SELECT ISNULL(Max(IdEmbarcador),0) FROM Trans_oc_embarcador ";

                using (SqlCommand lCommand = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text })
                {
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

        public static clsBeTrans_oc_embarcador? Get_Single_By_IdEmbarcador(IConfiguration config, int pIdEmbarcador)
        {
            clsBeTrans_oc_embarcador? result = null;

            try
            {
                const string sp = "SELECT * FROM Trans_oc_embarcador Where(IdEmbarcador = @IdEmbarcador)";

                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    lConnection.Open();

                    using (SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                    {
                        using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                        {
                            lDTA.SelectCommand.CommandType = CommandType.Text;
                            lDTA.SelectCommand.Transaction = lTransaction;
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdEmbarcador", pIdEmbarcador);

                            DataTable lDataTable = new DataTable();
                            lDTA.Fill(lDataTable);

                            clsBeTrans_oc_embarcador vBeTrans_oc_embarcador = new clsBeTrans_oc_embarcador();

                            if (lDataTable != null && lDataTable.Rows.Count > 0)
                            {
                                Cargar(ref vBeTrans_oc_embarcador, lDataTable.Rows[0]);
                                result = vBeTrans_oc_embarcador;
                            }
                        }

                        lTransaction.Commit();
                    }

                    lConnection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public static clsBeTrans_oc_embarcador? Get_Single_By_IdEmbarcador(int pIdEmbarcador, SqlConnection lConnection, SqlTransaction lTransaction)
        {
            clsBeTrans_oc_embarcador? result = null;

            try
            {
                const string sp = "SELECT * FROM Trans_oc_embarcador Where(IdEmbarcador = @IdEmbarcador) ";

                using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    lDTA.SelectCommand.Transaction = lTransaction;
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdEmbarcador", pIdEmbarcador);

                    DataTable lDataTable = new DataTable();
                    lDTA.Fill(lDataTable);

                    clsBeTrans_oc_embarcador vBeTrans_oc_embarcador = new clsBeTrans_oc_embarcador();

                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        Cargar(ref vBeTrans_oc_embarcador, lDataTable.Rows[0]);
                        result = vBeTrans_oc_embarcador;
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
