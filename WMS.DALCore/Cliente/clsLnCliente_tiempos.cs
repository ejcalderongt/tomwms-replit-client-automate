namespace WMS.DALCore.Cliente
{
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Data;    
    using System.Diagnostics;
    using System.Reflection;
    using WMS.EntityCore.Cliente;
    using WMS.EntityCore.Producto;

    public static class clsLnCliente_tiempos
    {
        private static clsInsert Ins = new clsInsert();
        private static clsUpdate Upd = new clsUpdate();
        public static void Cargar(ref clsBeCliente_tiempos oBeCliente_tiempos, DataRow dr)
        {
            int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
            bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
            string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
            DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }

            try
            {
                oBeCliente_tiempos.IdTiempoCliente = GetInt("IdTiempoCliente");
                oBeCliente_tiempos.IdCliente = GetInt("IdCliente");
                oBeCliente_tiempos.IdFamilia = GetInt("IdFamilia");
                oBeCliente_tiempos.IdClasificacion = GetInt("IdClasificacion");
                oBeCliente_tiempos.Dias_Local = GetInt("Dias_Local");
                oBeCliente_tiempos.Dias_Exterior = GetInt("Dias_Exterior");
                oBeCliente_tiempos.User_agr = GetString("user_agr");
                oBeCliente_tiempos.Fec_agr = GetDate("fec_agr");
                oBeCliente_tiempos.User_mod = GetString("user_mod");
                oBeCliente_tiempos.Fec_mod = GetDate("fec_mod");
                oBeCliente_tiempos.Activo = GetBool("activo");
                oBeCliente_tiempos.Es_Manufactura = GetBool("es_manufactura");
            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                MethodBase? currentMethodName = sf?.GetMethod();
                string vMsgError = string.Format("{0} {1}", currentMethodName, ex.Message);

                throw new Exception(vMsgError);
            }
        }

        public static int Insertar(ref clsBeCliente_tiempos oBeCliente_tiempos, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Ins.Init("cliente_tiempos");
                Ins.Add("idtiempocliente", "@idtiempocliente", "F");
                Ins.Add("idcliente", "@idcliente", "F");

                if (oBeCliente_tiempos.IdFamilia != 0)
                    Ins.Add("idfamilia", "@idfamilia", "F");

                if (oBeCliente_tiempos.IdClasificacion != 0)
                    Ins.Add("idclasificacion", "@idclasificacion", "F");

                Ins.Add("dias_local", "@dias_local", "F");
                Ins.Add("dias_exterior", "@dias_exterior", "F");
                Ins.Add("user_agr", "@user_agr", "F");
                Ins.Add("fec_agr", "@fec_agr", "F");
                Ins.Add("user_mod", "@user_mod", "F");
                Ins.Add("fec_mod", "@fec_mod", "F");
                Ins.Add("activo", "@activo", "F");

                string sp = Ins.SQL();

                using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction);

                cmd.Parameters.Add(new SqlParameter("@IDTIEMPOCLIENTE", oBeCliente_tiempos.IdTiempoCliente));
                cmd.Parameters.Add(new SqlParameter("@IDCLIENTE", oBeCliente_tiempos.IdCliente));

                if (oBeCliente_tiempos.IdClasificacion != 0)
                    cmd.Parameters.Add(new SqlParameter("@IDCLASIFICACION", oBeCliente_tiempos.IdClasificacion));

                if (oBeCliente_tiempos.IdFamilia != 0)
                    cmd.Parameters.Add(new SqlParameter("@IDFAMILIA", oBeCliente_tiempos.IdFamilia));

                cmd.Parameters.Add(new SqlParameter("@DIAS_LOCAL", oBeCliente_tiempos.Dias_Local));
                cmd.Parameters.Add(new SqlParameter("@DIAS_EXTERIOR", oBeCliente_tiempos.Dias_Exterior));
                cmd.Parameters.Add(new SqlParameter("@USER_AGR", oBeCliente_tiempos.User_agr ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@FEC_AGR", oBeCliente_tiempos.Fec_agr));
                cmd.Parameters.Add(new SqlParameter("@USER_MOD", oBeCliente_tiempos.User_mod ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@FEC_MOD", oBeCliente_tiempos.Fec_mod));
                cmd.Parameters.Add(new SqlParameter("@ACTIVO", oBeCliente_tiempos.Activo ? 1 : 0));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Actualizar(ref clsBeCliente_tiempos oBeCliente_tiempos, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Upd.Init("cliente_tiempos");
                Upd.Add("idtiempocliente", "@idtiempocliente", "F");
                Upd.Add("idcliente", "@idcliente", "F");
                Upd.Add("idfamilia", "@idfamilia", "F");
                Upd.Add("idclasificacion", "@idclasificacion", "F");
                Upd.Add("dias_local", "@dias_local", "F");
                Upd.Add("dias_exterior", "@dias_exterior", "F");
                Upd.Add("user_agr", "@user_agr", "F");
                Upd.Add("fec_agr", "@fec_agr", "F");
                Upd.Add("user_mod", "@user_mod", "F");
                Upd.Add("fec_mod", "@fec_mod", "F");
                Upd.Add("activo", "@activo", "F");
                Upd.Where("IdTiempoCliente = @IdTiempoCliente");

                string sp = Upd.SQL();

                using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction);

                cmd.Parameters.Add(new SqlParameter("@IDTIEMPOCLIENTE", oBeCliente_tiempos.IdTiempoCliente));
                cmd.Parameters.Add(new SqlParameter("@IDCLIENTE", oBeCliente_tiempos.IdCliente));
                cmd.Parameters.Add(new SqlParameter("@IDFAMILIA", oBeCliente_tiempos.IdFamilia));
                cmd.Parameters.Add(new SqlParameter("@IDCLASIFICACION", oBeCliente_tiempos.IdClasificacion));
                cmd.Parameters.Add(new SqlParameter("@DIAS_LOCAL", oBeCliente_tiempos.Dias_Local));
                cmd.Parameters.Add(new SqlParameter("@DIAS_EXTERIOR", oBeCliente_tiempos.Dias_Exterior));
                cmd.Parameters.Add(new SqlParameter("@USER_AGR", oBeCliente_tiempos.User_agr ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@FEC_AGR", oBeCliente_tiempos.Fec_agr));
                cmd.Parameters.Add(new SqlParameter("@USER_MOD", oBeCliente_tiempos.User_mod ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@FEC_MOD", oBeCliente_tiempos.Fec_mod));
                cmd.Parameters.Add(new SqlParameter("@ACTIVO", oBeCliente_tiempos.Activo ? 1 : 0));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Eliminar(ref clsBeCliente_tiempos oBeCliente_tiempos, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                const string sp = "DELETE FROM Cliente_tiempos WHERE (IdTiempoCliente = @IdTiempoCliente)";

                using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.Add(new SqlParameter("@IDTIEMPOCLIENTE", oBeCliente_tiempos.IdTiempoCliente));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int EliminarTodos(SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                const string sp = "DELETE FROM Cliente_tiempos";

                using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction)
                {
                    CommandType = CommandType.Text
                };

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable Listar(IConfiguration config)
        {
            try
            {
                const string sp = "SELECT * FROM Cliente_tiempos";

                using SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
                using SqlCommand cmd = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text };
                using SqlDataAdapter dad = new SqlDataAdapter(cmd);
                {
                    DataTable dt = new DataTable();
                    dad.Fill(dt);
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool Obtener(ref clsBeCliente_tiempos oBeCliente_tiempos, IConfiguration config)
        {
            try
            {
                const string sp = "SELECT * FROM Cliente_tiempos WHERE (IdTiempoCliente = @IdTiempoCliente)";

                using SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
                using SqlCommand cmd = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text };
                using SqlDataAdapter dad = new SqlDataAdapter(cmd);
                {
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@IDTIEMPOCLIENTE", oBeCliente_tiempos.IdTiempoCliente));

                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        Cargar(ref oBeCliente_tiempos, dt.Rows[0]);
                        return true;
                    }
                    else
                    {
                        throw new Exception("No se pudo obtener el registro");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<clsBeCliente_tiempos> GetAll(IConfiguration config)
        {
            try
            {
                List<clsBeCliente_tiempos> lReturnList = new List<clsBeCliente_tiempos>();
                const string sp = "SELECT * FROM Cliente_tiempos";

                using SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
                using SqlCommand cmd = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text };
                using SqlDataAdapter dad = new SqlDataAdapter(cmd);
                {
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        clsBeCliente_tiempos vBeCliente_tiempos = new clsBeCliente_tiempos();
                        Cargar(ref vBeCliente_tiempos, dr);
                        lReturnList.Add(vBeCliente_tiempos);
                    }

                    return lReturnList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool GetSingle(ref clsBeCliente_tiempos pBeCliente_tiempos, IConfiguration config)
        {
            try
            {
                const string sp = "SELECT * FROM Cliente_tiempos WHERE (IdTiempoCliente = @IdTiempoCliente)";

                using SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
                using SqlCommand cmd = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text };
                using SqlDataAdapter dad = new SqlDataAdapter(cmd);
                {
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@IDTIEMPOCLIENTE", pBeCliente_tiempos.IdTiempoCliente));

                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        Cargar(ref pBeCliente_tiempos, dt.Rows[0]);
                    }

                    return true;
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
                const string sp = "SELECT ISNULL(Max(IdTiempoCliente),0) FROM Cliente_tiempos";

                using SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
                {
                    lConnection.Open();
                    using SqlCommand lCommand = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text };
                    {
                        object lReturnValue = lCommand.ExecuteScalar();
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

        public static int MaxID(SqlConnection lConnection, SqlTransaction lTransaction)
        {
            try
            {
                int lMax = 0;
                const string sp = "SELECT ISNULL(Max(IdTiempoCliente),0) FROM Cliente_tiempos";

                using SqlCommand lCommand = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
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

        public static clsBeCliente_tiempos? GetSingle(int IdTiempoCliente, IConfiguration config)
        {
            try
            {
                const string sp = "SELECT * FROM Cliente_tiempos WHERE (IdTiempoCliente = @IdTiempoCliente)";

                using SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
                using SqlCommand cmd = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text };
                using SqlDataAdapter dad = new SqlDataAdapter(cmd);
                {
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@IDTIEMPOCLIENTE", IdTiempoCliente));

                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        clsBeCliente_tiempos pBeCliente_tiempos = new clsBeCliente_tiempos();
                        Cargar(ref pBeCliente_tiempos, dt.Rows[0]);
                        return pBeCliente_tiempos;
                    }

                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool Cliente_Tiene_Tiempos(int IdCliente, SqlConnection lConnection, SqlTransaction lTransaction)
        {
            try
            {
                const string sp = "SELECT COUNT(*) FROM Cliente_tiempos WHERE IdCliente = @IdCliente";

                using SqlCommand lCommand = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                {
                    lCommand.Parameters.AddWithValue("@IdCliente", IdCliente);
                    int count = Convert.ToInt32(lCommand.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<clsBeCliente_tiempos> Get_All_Tiempos_By_IdCliente(int pIdCliente, IConfiguration config)
        {
            List<clsBeCliente_tiempos>? lReturnList = null;
            string vSQL = "SELECT * FROM VW_TiempoCliente WHERE activo=1 AND IdCliente=@IdCliente";

            try
            {
                using SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
                using SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection);
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente);

                    DataTable lDT = new DataTable();
                    lDTA.Fill(lDT);

                    if (lDT != null && lDT.Rows.Count > 0)
                    {
                        lReturnList = new List<clsBeCliente_tiempos>();

                        foreach (DataRow lRow in lDT.Rows)
                        {
                            clsBeCliente_tiempos Obj = new clsBeCliente_tiempos();
                            Obj.Familia = new clsBeProducto_familia();
                            Obj.Clasificacion = new clsBeProducto_clasificacion();
                            Cargar(ref Obj, lRow);

                            if (lRow["Familia"] != DBNull.Value && lRow["Familia"] != null)
                            {
                                Obj.Familia.Nombre = Convert.ToString(lRow["Familia"]) ?? "";
                            }

                            if (lRow["Clasificación"] != DBNull.Value && lRow["Clasificación"] != null)
                            {
                                Obj.Clasificacion.Nombre = Convert.ToString(lRow["Clasificación"]) ?? "";
                            }

                            lReturnList.Add(Obj);
                        }
                    }
                }

                return lReturnList ?? new List<clsBeCliente_tiempos>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<clsBeCliente_tiempos> Get_All_Tiempos_By_IdCliente(int pIdCliente,
                                                                              SqlConnection lConnection,
                                                                              SqlTransaction lTransaction)
        {
            List<clsBeCliente_tiempos> lReturnList = new List<clsBeCliente_tiempos>();
            string vSQL = "SELECT * FROM VW_TiempoCliente WHERE activo=1 AND IdCliente=@IdCliente";

            try
            {
                using SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection);
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    lDTA.SelectCommand.Transaction = lTransaction;
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente);

                    DataTable lDT = new DataTable();
                    lDTA.Fill(lDT);

                    if (lDT != null && lDT.Rows.Count > 0)
                    {
                        foreach (DataRow lRow in lDT.Rows)
                        {
                            clsBeCliente_tiempos BeClienteTiempos = new clsBeCliente_tiempos();
                            BeClienteTiempos.Familia = new clsBeProducto_familia();
                            BeClienteTiempos.Clasificacion = new clsBeProducto_clasificacion();
                            Cargar(ref BeClienteTiempos, lRow);

                            if (lRow["Familia"] != DBNull.Value && lRow["Familia"] != null)
                            {
                                BeClienteTiempos.Familia.Nombre = Convert.ToString(lRow["Familia"]) ?? "";
                            }

                            if (lRow["Clasificación"] != DBNull.Value && lRow["Clasificación"] != null)
                            {
                                BeClienteTiempos.Clasificacion.Nombre = Convert.ToString(lRow["Clasificación"]) ?? "";
                            }

                            lReturnList.Add(BeClienteTiempos);
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

        public static clsBeCliente_tiempos? GetSingle(int IdCliente, int IdFamilia, int IdClasificacion, IConfiguration config)
        {
            try
            {
                const string sp = "SELECT * FROM Cliente_tiempos WHERE (IdCliente = @IdCliente) AND IdFamilia = @IdFamilia AND IdClasificacion = @IdClasificacion";

                using SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
                using SqlCommand cmd = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text };
                using SqlDataAdapter dad = new SqlDataAdapter(cmd);
                {
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@IdCliente", IdCliente));
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@IdFamilia", IdFamilia));
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@IdClasificacion", IdClasificacion));

                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        clsBeCliente_tiempos pBeCliente_tiempos = new clsBeCliente_tiempos();
                        Cargar(ref pBeCliente_tiempos, dt.Rows[0]);
                        return pBeCliente_tiempos;
                    }

                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable Get_All_Tiempos_Clas_And_Fam(bool ConTiempos, IConfiguration config)
        {
            DataTable lDatatable = new DataTable();
            string vSQL = "SELECT * FROM VW_Clientes_Tiempos ";

            if (ConTiempos)
            {
                vSQL += "WHERE cant_familias>0 And cant_clasificacion>0";
            }
            else
            {
                vSQL += "WHERE cant_familias=0 And cant_clasificacion=0";
            }

            try
            {
                using SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
                {
                    lConnection.Open();
                    using SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                    using SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection);
                    {
                        lDTA.SelectCommand.CommandType = CommandType.Text;
                        lDTA.SelectCommand.Transaction = lTransaction;

                        DataTable lDT = new DataTable();
                        lDTA.Fill(lDT);

                        if (lDT != null && lDT.Rows.Count > 0)
                        {
                            lDatatable = lDT;
                        }

                        lTransaction.Commit();
                    }
                    lConnection.Close();
                }

                return lDatatable;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable Get_All_Tiempos_Det(IConfiguration config)
        {
            DataTable lDatatable = new DataTable();
            string vSQL = "SELECT Cliente, Familia, Clasificación, Dias_Local, Dias_Exterior FROM VW_TiempoCliente";

            try
            {
                using SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
                {
                    lConnection.Open();
                    using SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                    using SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection);
                    {
                        lDTA.SelectCommand.CommandType = CommandType.Text;
                        lDTA.SelectCommand.Transaction = lTransaction;
                        lDTA.SelectCommand.CommandTimeout = 0;

                        DataTable lDT = new DataTable();
                        lDTA.Fill(lDT);

                        if (lDT != null && lDT.Rows.Count > 0)
                        {
                            lDatatable = lDT;
                        }

                        lTransaction.Commit();
                    }
                    lConnection.Close();
                }

                return lDatatable;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static clsBeCliente_tiempos? GetSingle(int IdCliente, int IdFamilia, int IdClasificacion,
                                                     SqlConnection pConnection, SqlTransaction pTransaction)
        {
            try
            {
                const string sp = "SELECT * FROM Cliente_tiempos WHERE (IdCliente = @IdCliente) AND IdFamilia = @IdFamilia AND IdClasificacion = @IdClasificacion";

                using SqlCommand cmd = new SqlCommand(sp, pConnection, pTransaction) { CommandType = CommandType.Text };
                using SqlDataAdapter dad = new SqlDataAdapter(cmd);
                {
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@IdCliente", IdCliente));
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@IdFamilia", IdFamilia));
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@IdClasificacion", IdClasificacion));

                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        clsBeCliente_tiempos pBeCliente_tiempos = new clsBeCliente_tiempos();
                        Cargar(ref pBeCliente_tiempos, dt.Rows[0]);
                        return pBeCliente_tiempos;
                    }

                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable Get_All_Tiempos_Det_By_IdCliente(string pCodCliente, IConfiguration config)
        {
            DataTable lDatatable = new DataTable();
            string vSQL = "SELECT Cliente, Familia, Clasificación, Dias_Local, Dias_Exterior " +
                          "FROM VW_TiempoCliente " +
                          "WHERE IdCliente IN (SELECT IdCliente FROM cliente WHERE codigo = @CodCliente)";

            try
            {
                using SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
                {
                    lConnection.Open();
                    using SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                    using SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection);
                    {
                        lDTA.SelectCommand.CommandType = CommandType.Text;
                        lDTA.SelectCommand.Transaction = lTransaction;
                        lDTA.SelectCommand.CommandTimeout = 0;
                        lDTA.SelectCommand.Parameters.AddWithValue("@CodCliente", pCodCliente ?? (object)DBNull.Value);

                        DataTable lDT = new DataTable();
                        lDTA.Fill(lDT);

                        if (lDT != null && lDT.Rows.Count > 0)
                        {
                            lDatatable = lDT;
                        }

                        lTransaction.Commit();
                    }
                    lConnection.Close();
                }

                return lDatatable;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}