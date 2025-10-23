namespace WMS.DALCore.Trans_oc
{
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using WMS.EntityCore.Trans_oc;

    public partial class clsLnTrans_oc_imagen
    {
        private static readonly clsInsert ins = new clsInsert();
        private static readonly clsUpdate upd = new clsUpdate();
        public static List<clsBeTrans_oc_imagen> GetByOrdenCompra(IConfiguration config, int pIdOrdenCompraEnc)
        {
            List<clsBeTrans_oc_imagen> lReturnList = new List<clsBeTrans_oc_imagen>();

            try
            {                
                    
                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    string vSQL = "SELECT * FROM Trans_oc_imagen WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc";

                    using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
                    {
                        lDTA.SelectCommand.CommandType = CommandType.Text;
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc);

                        DataTable lDataTable = new DataTable();
                        lDTA.Fill(lDataTable);

                        if (lDataTable != null && lDataTable.Rows.Count > 0)
                        {
                            foreach (DataRow lRow in lDataTable.Rows)
                            {
                                clsBeTrans_oc_imagen Obj = new clsBeTrans_oc_imagen();
                                Cargar(ref Obj, lRow);
                                Obj.IsNew = false;
                                lReturnList.Add(Obj);
                            }
                        }
                    }
                }

                return lReturnList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<clsBeTrans_oc_imagen> Get_Imagenes_By_IdOrdenCompraEnc(int pIdOrdenCompraEnc,
                                                                                  SqlConnection lConnection,
                                                                                  SqlTransaction lTransaction)
        {
            List<clsBeTrans_oc_imagen> lReturnList = new List<clsBeTrans_oc_imagen>();

            try
            {
                string vSQL = "SELECT * FROM Trans_oc_imagen WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc";

                using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc);
                    lDTA.SelectCommand.Transaction = lTransaction;

                    DataTable lDataTable = new DataTable();
                    lDTA.Fill(lDataTable);

                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow lRow in lDataTable.Rows)
                        {
                            clsBeTrans_oc_imagen Obj = new clsBeTrans_oc_imagen();
                            Cargar(ref Obj, lRow);
                            Obj.IsNew = false;
                            lReturnList.Add(Obj);
                        }
                    }
                }

                return lReturnList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int MaxID(IConfiguration config, int pIdOrdenCompraEnc)
        {
            try
            {
                int lMax = 0;

                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    string query = $"SELECT ISNULL(Max(IdOrdenCompraImg),0) FROM trans_oc_imagen WHERE IdOrdenCompraEnc = {pIdOrdenCompraEnc}";

                    using (SqlCommand lCommand = new SqlCommand(query, lConnection))
                    {
                        lCommand.CommandType = CommandType.Text;
                        lConnection.Open();
                        object lReturnValue = lCommand.ExecuteScalar();
                        lConnection.Close();

                        if (lReturnValue != DBNull.Value && lReturnValue != null)
                        {
                            lMax = Convert.ToInt32(lReturnValue);
                        }
                    }
                }

                return lMax;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void Delete(IConfiguration config, int pIdOrdenCompraEnc, int pIdOrdenCompraImg)
        {
            string vSQL = "DELETE FROM trans_oc_imagen WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc AND IdOrdenCompraImg = @IdOrdenCompraImg";

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            using (SqlCommand lCommand = new SqlCommand(vSQL, lConnection))
            {
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc);
                lCommand.Parameters.AddWithValue("@IdOrdenCompraImg", pIdOrdenCompraImg);

                lConnection.Open();
                lCommand.ExecuteNonQuery();
                lConnection.Close();
            }
        }
        public static void Cargar(ref clsBeTrans_oc_imagen oBeTrans_oc_imagen, DataRow dr)
        {
            try
            {
                oBeTrans_oc_imagen.IdOrdenCompraImg = dr["IdOrdenCompraImg"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdOrdenCompraImg"]);
                oBeTrans_oc_imagen.IdOrdenCompraEnc = dr["IdOrdenCompraEnc"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdOrdenCompraEnc"]);
                oBeTrans_oc_imagen.Orden = dr["Orden"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Orden"]);
                oBeTrans_oc_imagen.Imagen = dr["Imagen"] != DBNull.Value && dr["Imagen"] is byte[] bytes ? bytes : Array.Empty<byte>();
                oBeTrans_oc_imagen.Descripcion = dr["descripcion"] is DBNull ? string.Empty : dr["descripcion"]?.ToString() ?? string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }        

        public int Actualizar(ref clsBeTrans_oc_imagen oBeTrans_oc_imagen, SqlConnection pConection, SqlTransaction pTransaction)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                upd.Init("trans_oc_imagen");
                upd.Add("idordencompraimg", "@idordencompraimg", "F");
                upd.Add("idordencompraenc", "@idordencompraenc", "F");
                upd.Add("orden", "@orden", "F");
                upd.Add("imagen", "@imagen", "F");
                upd.Add("descripcion", "@descripcion", "F");
                upd.Where("IdOrdenCompraImg = @IdOrdenCompraImg AND IdOrdenCompraEnc = @IdOrdenCompraEnc");

                string sp = upd.SQL();
                cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

                cmd.Parameters.Add(new SqlParameter("@IDORDENCOMPRAIMG", oBeTrans_oc_imagen.IdOrdenCompraImg));
                cmd.Parameters.Add(new SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_imagen.IdOrdenCompraEnc));
                cmd.Parameters.Add(new SqlParameter("@ORDEN", oBeTrans_oc_imagen.Orden));

                if (oBeTrans_oc_imagen.Imagen != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@IMAGEN", oBeTrans_oc_imagen.Imagen));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@IMAGEN", SqlDbType.Image)).Value = DBNull.Value;
                }

                cmd.Parameters.Add(new SqlParameter("@DESCRIPCION", oBeTrans_oc_imagen.Descripcion ?? string.Empty));

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

        public int Eliminar(ref clsBeTrans_oc_imagen oBeTrans_oc_imagen, SqlConnection pConection, SqlTransaction pTransaction)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                string sp = "DELETE FROM Trans_oc_imagen WHERE (IdOrdenCompraImg = @IdOrdenCompraImg) AND (IdOrdenCompraEnc = @IdOrdenCompraEnc)";
                cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

                cmd.Parameters.Add(new SqlParameter("@IDORDENCOMPRAIMG", oBeTrans_oc_imagen.IdOrdenCompraImg));
                cmd.Parameters.Add(new SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_imagen.IdOrdenCompraEnc));

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
        public bool Obtener(IConfiguration config, ref clsBeTrans_oc_imagen oBeTrans_oc_imagen)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction?  lTransaction = null;
            bool result = false;

            try
            {
                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted);

                string sp = "SELECT * FROM Trans_oc_imagen WHERE (IdOrdenCompraImg = @IdOrdenCompraImg) AND (IdOrdenCompraEnc = @IdOrdenCompraEnc)";

                using (SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text })
                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@IDORDENCOMPRAIMG", oBeTrans_oc_imagen.IdOrdenCompraImg));
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_imagen.IdOrdenCompraEnc));

                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        var lrow = dt.Rows[0];
                        Cargar(ref oBeTrans_oc_imagen, lrow);
                        result = true;
                    }
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
        public int Insertar(ref clsBeTrans_oc_imagen oBeTrans_oc_imagen, SqlConnection pConection, SqlTransaction pTransaction)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                ins.Init("trans_oc_imagen");
                ins.Add("idordencompraimg", "@idordencompraimg", "F");
                ins.Add("idordencompraenc", "@idordencompraenc", "F");
                ins.Add("orden", "@orden", "F");
                ins.Add("imagen", "@imagen", "F");
                ins.Add("descripcion", "@descripcion", "F");

                string sp = ins.SQL();
                cmd.CommandType = CommandType.Text;

                cmd = new SqlCommand(sp, pConection, pTransaction);

                cmd.Parameters.Add(new SqlParameter("@IDORDENCOMPRAIMG", oBeTrans_oc_imagen.IdOrdenCompraImg));
                cmd.Parameters.Add(new SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_imagen.IdOrdenCompraEnc));
                cmd.Parameters.Add(new SqlParameter("@ORDEN", oBeTrans_oc_imagen.Orden));

                if (oBeTrans_oc_imagen.Imagen != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@IMAGEN", oBeTrans_oc_imagen.Imagen));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@IMAGEN", SqlDbType.Image)).Value = DBNull.Value;
                }

                cmd.Parameters.Add(new SqlParameter("@DESCRIPCION", oBeTrans_oc_imagen.Descripcion));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
            }
        }

    }
}