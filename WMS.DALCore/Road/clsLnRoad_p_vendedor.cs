namespace WMS.DALCore.Road
{
    using System;
    using System.Data;    
    using Microsoft.Extensions.Configuration;
    using System.Diagnostics;
    using System.Reflection;
    using WMS.EntityCore.Road;
    using Microsoft.Data.SqlClient;

    public static class clsLnRoad_p_vendedor
    {
        private static clsInsert Ins = new clsInsert();
        private static clsUpdate Upd = new clsUpdate();
        public static void Cargar(ref clsBeRoad_p_vendedor oBeRoad_p_vendedor, DataRow dr)
        {
            int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
            bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
            string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
            DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }

            try
            {
                oBeRoad_p_vendedor.IdRuta = GetInt("IdRuta");
                oBeRoad_p_vendedor.IdVendedor = GetInt("IdVendedor");
                oBeRoad_p_vendedor.Codigo = GetString("codigo");
                oBeRoad_p_vendedor.Nombre = GetString("nombre");
                oBeRoad_p_vendedor.Clave = GetString("clave");
                oBeRoad_p_vendedor.Ruta = GetString("ruta");
                oBeRoad_p_vendedor.Nivel = GetInt("nivel");
                oBeRoad_p_vendedor.Nivelprecio = GetInt("nivelprecio");
                oBeRoad_p_vendedor.Bodega = GetString("bodega");
                oBeRoad_p_vendedor.Subbodega = GetString("subbodega");
                oBeRoad_p_vendedor.Cod_vehiculo = GetString("cod_vehiculo");
                oBeRoad_p_vendedor.Liquidando = GetString("liquidando");
                oBeRoad_p_vendedor.Ultima_fecha_liq = GetDate("ultima_fecha_liq");
                oBeRoad_p_vendedor.Bloqueado = GetBool("bloqueado");
                oBeRoad_p_vendedor.Devolucion_sap = GetInt("devolucion_sap");
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

        public static int Insertar(ref clsBeRoad_p_vendedor oBeRoad_p_vendedor, IConfiguration config, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Ins.Init("road_p_vendedor");
                Ins.Add("idvendedor", "@idvendedor", "F");
                Ins.Add("codigo", "@codigo", "F");
                Ins.Add("nombre", "@nombre", "F");
                Ins.Add("clave", "@clave", "F");
                Ins.Add("ruta", "@ruta", "F");
                Ins.Add("nivel", "@nivel", "F");
                Ins.Add("nivelprecio", "@nivelprecio", "F");
                Ins.Add("bodega", "@bodega", "F");
                Ins.Add("subbodega", "@subbodega", "F");
                Ins.Add("cod_vehiculo", "@cod_vehiculo", "F");
                Ins.Add("liquidando", "@liquidando", "F");
                Ins.Add("ultima_fecha_liq", "@ultima_fecha_liq", "F");
                Ins.Add("bloqueado", "@bloqueado", "F");
                Ins.Add("devolucion_sap", "@devolucion_sap", "F");
                Ins.Add("idruta", "@idruta", "F");

                string sp = Ins.SQL();

                using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction);

                cmd.Parameters.Add(new SqlParameter("@IDVENDEDOR", oBeRoad_p_vendedor.IdVendedor));
                cmd.Parameters.Add(new SqlParameter("@CODIGO", oBeRoad_p_vendedor.Codigo ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", oBeRoad_p_vendedor.Nombre ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@CLAVE", oBeRoad_p_vendedor.Clave ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@RUTA", oBeRoad_p_vendedor.Ruta ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@NIVEL", oBeRoad_p_vendedor.Nivel));
                cmd.Parameters.Add(new SqlParameter("@NIVELPRECIO", oBeRoad_p_vendedor.Nivelprecio));
                cmd.Parameters.Add(new SqlParameter("@BODEGA", oBeRoad_p_vendedor.Bodega ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@SUBBODEGA", oBeRoad_p_vendedor.Subbodega ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@COD_VEHICULO", oBeRoad_p_vendedor.Cod_vehiculo ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@LIQUIDANDO", oBeRoad_p_vendedor.Liquidando ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ULTIMA_FECHA_LIQ", oBeRoad_p_vendedor.Ultima_fecha_liq));
                cmd.Parameters.Add(new SqlParameter("@BLOQUEADO", oBeRoad_p_vendedor.Bloqueado));
                cmd.Parameters.Add(new SqlParameter("@DEVOLUCION_SAP", oBeRoad_p_vendedor.Devolucion_sap));
                cmd.Parameters.Add(new SqlParameter("@IDRUTA", oBeRoad_p_vendedor.IdRuta));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Actualizar(ref clsBeRoad_p_vendedor oBeRoad_p_vendedor, IConfiguration config, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Upd.Init("road_p_vendedor");
                Upd.Add("idvendedor", "@idvendedor", "F");
                Upd.Add("codigo", "@codigo", "F");
                Upd.Add("nombre", "@nombre", "F");
                Upd.Add("clave", "@clave", "F");
                Upd.Add("ruta", "@ruta", "F");
                Upd.Add("nivel", "@nivel", "F");
                Upd.Add("nivelprecio", "@nivelprecio", "F");
                Upd.Add("bodega", "@bodega", "F");
                Upd.Add("subbodega", "@subbodega", "F");
                Upd.Add("cod_vehiculo", "@cod_vehiculo", "F");
                Upd.Add("liquidando", "@liquidando", "F");
                Upd.Add("ultima_fecha_liq", "@ultima_fecha_liq", "F");
                Upd.Add("bloqueado", "@bloqueado", "F");
                Upd.Add("devolucion_sap", "@devolucion_sap", "F");
                Upd.Add("idruta", "@idruta", "F");
                Upd.Where("IdVendedor = @IdVendedor AND codigo = @codigo");

                string sp = Upd.SQL();

                using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction);

                cmd.Parameters.Add(new SqlParameter("@IDVENDEDOR", oBeRoad_p_vendedor.IdVendedor));
                cmd.Parameters.Add(new SqlParameter("@CODIGO", oBeRoad_p_vendedor.Codigo ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", oBeRoad_p_vendedor.Nombre ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@CLAVE", oBeRoad_p_vendedor.Clave ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@RUTA", oBeRoad_p_vendedor.Ruta ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@NIVEL", oBeRoad_p_vendedor.Nivel));
                cmd.Parameters.Add(new SqlParameter("@NIVELPRECIO", oBeRoad_p_vendedor.Nivelprecio));
                cmd.Parameters.Add(new SqlParameter("@BODEGA", oBeRoad_p_vendedor.Bodega ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@SUBBODEGA", oBeRoad_p_vendedor.Subbodega ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@COD_VEHICULO", oBeRoad_p_vendedor.Cod_vehiculo ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@LIQUIDANDO", oBeRoad_p_vendedor.Liquidando ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ULTIMA_FECHA_LIQ", oBeRoad_p_vendedor.Ultima_fecha_liq));
                cmd.Parameters.Add(new SqlParameter("@BLOQUEADO", oBeRoad_p_vendedor.Bloqueado));
                cmd.Parameters.Add(new SqlParameter("@DEVOLUCION_SAP", oBeRoad_p_vendedor.Devolucion_sap));
                cmd.Parameters.Add(new SqlParameter("@IDRUTA", oBeRoad_p_vendedor.IdRuta));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Eliminar(ref clsBeRoad_p_vendedor oBeRoad_p_vendedor, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                string sp = "DELETE FROM Road_p_vendedor WHERE (IdVendedor = @IdVendedor) AND (codigo = @codigo)";

                using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.Add(new SqlParameter("@IDVENDEDOR", oBeRoad_p_vendedor.IdVendedor));
                cmd.Parameters.Add(new SqlParameter("@CODIGO", oBeRoad_p_vendedor.Codigo ?? (object)DBNull.Value));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool Obtener(ref clsBeRoad_p_vendedor oBeRoad_p_vendedor, IConfiguration config)
        {
            bool result = false;

            try
            {
                using SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
                {
                    lConnection.Open();

                    string sp = "SELECT * FROM Road_p_vendedor WHERE (IdVendedor = @IdVendedor) AND (codigo = @codigo)";

                    using SqlCommand cmd = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text };
                    using SqlDataAdapter dad = new SqlDataAdapter(cmd);
                    {
                        dad.SelectCommand.Parameters.Add(new SqlParameter("@IDVENDEDOR", oBeRoad_p_vendedor.IdVendedor));
                        dad.SelectCommand.Parameters.Add(new SqlParameter("@CODIGO", oBeRoad_p_vendedor.Codigo ?? (object)DBNull.Value));

                        DataTable dt = new DataTable();
                        dad.Fill(dt);

                        if (dt.Rows.Count == 1)
                        {
                            Cargar(ref oBeRoad_p_vendedor, dt.Rows[0]);
                            result = true;
                        }
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static clsBeRoad_p_vendedor? Get_Vendedor_By_Codigo(string pCodigoVendedor,
                                                                   SqlConnection lConnection,
                                                                   SqlTransaction lTransaction)
        {
            try
            {
                string vSQL = "SELECT TOP 1 * FROM road_p_vendedor WHERE codigo = @codigo";

                using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    lDTA.SelectCommand.Transaction = lTransaction;
                    lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodigoVendedor ?? (object)DBNull.Value);

                    DataTable lDT = new DataTable();
                    lDTA.Fill(lDT);

                    if (lDT != null && lDT.Rows.Count > 0)
                    {
                        DataRow lRow = lDT.Rows[0];
                        clsBeRoad_p_vendedor Obj = new clsBeRoad_p_vendedor();
                        Cargar(ref Obj, lRow);
                        return Obj;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
