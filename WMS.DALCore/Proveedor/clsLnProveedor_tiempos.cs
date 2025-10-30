namespace WMS.DALCore.Proveedor
{
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Data;   
    using WMS.EntityCore.Producto;
    using WMS.EntityCore.Proveedor;

    public class clsLnProveedor_tiempos
    {
        private static readonly clsInsert ins = new clsInsert();
        private static readonly clsUpdate upd = new clsUpdate();
        public static void Cargar(ref clsBeProveedor_tiempos oBeProveedor_tiempos, DataRow dr)
        {
            try
            {
                oBeProveedor_tiempos.IdTiempoProveedor = dr["IdTiempoproveedor"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdTiempoproveedor"]);
                oBeProveedor_tiempos.IdProveedor = dr["Idproveedor"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Idproveedor"]);
                oBeProveedor_tiempos.IdFamilia = dr["IdFamilia"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdFamilia"]);
                oBeProveedor_tiempos.IdClasificacion = dr["IdClasificacion"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdClasificacion"]);
                oBeProveedor_tiempos.Dias_Local = dr["Dias_Local"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Dias_Local"]);
                oBeProveedor_tiempos.Dias_Exterior = dr["Dias_Exterior"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Dias_Exterior"]);                
                oBeProveedor_tiempos.Fec_agr = dr["fec_agr"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fec_agr"]);
                oBeProveedor_tiempos.User_agr = dr["user_agr"] is not DBNull ? dr["user_agr"]?.ToString() ?? string.Empty : string.Empty;
                oBeProveedor_tiempos.User_mod = dr["user_mod"] is not DBNull ? dr["user_mod"]?.ToString() ?? string.Empty : string.Empty;
                oBeProveedor_tiempos.Fec_mod = dr["fec_mod"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fec_mod"]);
                oBeProveedor_tiempos.Activo = dr["activo"] != DBNull.Value && Convert.ToBoolean(dr["activo"]);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Insertar(ref clsBeProveedor_tiempos oBeProveedor_tiempos, SqlConnection pConection, SqlTransaction pTransaction)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                ins.Init("proveedor_tiempos");
                ins.Add("idtiempoproveedor", "@idtiempoproveedor", "F");
                ins.Add("idproveedor", "@idproveedor", "F");
                if (oBeProveedor_tiempos.IdFamilia != 0) ins.Add("idfamilia", "@idfamilia", "F");
                if (oBeProveedor_tiempos.IdClasificacion != 0) ins.Add("idclasificacion", "@idclasificacion", "F");
                ins.Add("dias_local", "@dias_local", "F");
                ins.Add("dias_exterior", "@dias_exterior", "F");
                ins.Add("user_agr", "@user_agr", "F");
                ins.Add("fec_agr", "@fec_agr", "F");
                ins.Add("user_mod", "@user_mod", "F");
                ins.Add("fec_mod", "@fec_mod", "F");
                ins.Add("activo", "@activo", "F");

                string sp = ins.SQL();
                cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

                cmd.Parameters.Add(new SqlParameter("@IDTIEMPOPROVEEDOR", oBeProveedor_tiempos.IdTiempoProveedor));
                cmd.Parameters.Add(new SqlParameter("@IDPROVEEDOR", oBeProveedor_tiempos.IdProveedor));
                if (oBeProveedor_tiempos.IdClasificacion != 0) cmd.Parameters.Add(new SqlParameter("@IDCLASIFICACION", oBeProveedor_tiempos.IdClasificacion));
                if (oBeProveedor_tiempos.IdFamilia != 0) cmd.Parameters.Add(new SqlParameter("@IDFAMILIA", oBeProveedor_tiempos.IdFamilia));
                cmd.Parameters.Add(new SqlParameter("@DIAS_LOCAL", oBeProveedor_tiempos.Dias_Local));
                cmd.Parameters.Add(new SqlParameter("@DIAS_EXTERIOR", oBeProveedor_tiempos.Dias_Exterior));
                cmd.Parameters.Add(new SqlParameter("@USER_AGR", oBeProveedor_tiempos.User_agr));
                cmd.Parameters.Add(new SqlParameter("@FEC_AGR", oBeProveedor_tiempos.Fec_agr));
                cmd.Parameters.Add(new SqlParameter("@USER_MOD", oBeProveedor_tiempos.User_mod));
                cmd.Parameters.Add(new SqlParameter("@FEC_MOD", oBeProveedor_tiempos.Fec_mod));
                cmd.Parameters.Add(new SqlParameter("@ACTIVO", oBeProveedor_tiempos.Activo));

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

        public static int Actualizar(ref clsBeProveedor_tiempos oBeProveedor_tiempos, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                upd.Init("proveedor_tiempos");
                upd.Add("idtiempoproveedor", "@idtiempoproveedor", "F");
                upd.Add("idproveedor", "@idproveedor", "F");
                upd.Add("idfamilia", "@idfamilia", "F");
                upd.Add("idclasificacion", "@idclasificacion", "F");
                upd.Add("dias_local", "@dias_local", "F");
                upd.Add("dias_exterior", "@dias_exterior", "F");
                upd.Add("user_agr", "@user_agr", "F");
                upd.Add("fec_agr", "@fec_agr", "F");
                upd.Add("user_mod", "@user_mod", "F");
                upd.Add("fec_mod", "@fec_mod", "F");
                upd.Add("activo", "@activo", "F");
                upd.Where("IdTiempoproveedor = @IdTiempoproveedor");

                string sp = upd.SQL();

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@IDTIEMPOPROVEEDOR", oBeProveedor_tiempos.IdTiempoProveedor));
                    cmd.Parameters.Add(new SqlParameter("@IDPROVEEDOR", oBeProveedor_tiempos.IdProveedor));
                    cmd.Parameters.Add(new SqlParameter("@IDFAMILIA", oBeProveedor_tiempos.IdFamilia));
                    cmd.Parameters.Add(new SqlParameter("@IDCLASIFICACION", oBeProveedor_tiempos.IdClasificacion));
                    cmd.Parameters.Add(new SqlParameter("@DIAS_LOCAL", oBeProveedor_tiempos.Dias_Local));
                    cmd.Parameters.Add(new SqlParameter("@DIAS_EXTERIOR", oBeProveedor_tiempos.Dias_Exterior));
                    cmd.Parameters.Add(new SqlParameter("@USER_AGR", oBeProveedor_tiempos.User_agr));
                    cmd.Parameters.Add(new SqlParameter("@FEC_AGR", oBeProveedor_tiempos.Fec_agr));
                    cmd.Parameters.Add(new SqlParameter("@USER_MOD", oBeProveedor_tiempos.User_mod));
                    cmd.Parameters.Add(new SqlParameter("@FEC_MOD", oBeProveedor_tiempos.Fec_mod));
                    cmd.Parameters.Add(new SqlParameter("@ACTIVO", oBeProveedor_tiempos.Activo));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int MaxID(ref SqlConnection lConnection, ref SqlTransaction lTransaction)
        {
            try
            {
                int lMax = 0;
                const string sp = "SELECT ISNULL(Max(IdTiempoproveedor),0) FROM Proveedor_tiempos";

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
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public static List<clsBeProveedor_tiempos> Get_All_Tiempos_By_IdCliente(IConfiguration config, int pIdProveedor)
        {
            List<clsBeProveedor_tiempos> lReturnList = new List<clsBeProveedor_tiempos>();
            string vSQL = "SELECT * FROM VW_TiempoProveedor WHERE activo=1 AND IdProveedor=@pIdProveedor";

            try
            {
                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    lDTA.SelectCommand.Parameters.AddWithValue("@pIdProveedor", pIdProveedor);

                    DataTable lDT = new DataTable();
                    lDTA.Fill(lDT);

                    if (lDT != null && lDT.Rows.Count > 0)
                    {
                        foreach (DataRow lRow in lDT.Rows)
                        {
                            clsBeProveedor_tiempos Obj = new clsBeProveedor_tiempos();
                            Obj.Familia = new clsBeProducto_familia();
                            Obj.Clasificacion = new clsBeProducto_clasificacion();
                            var lrowRef = lRow;
                            Cargar(ref Obj, lrowRef);

                            if (lRow["Familia"] != DBNull.Value && lRow["Familia"] != null)
                            {
                                Obj.Familia.Nombre = lRow["Familia"] is DBNull ? string.Empty : lRow["Familia"]?.ToString() ?? string.Empty;
                            }

                            if (lRow["Clasificación"] != DBNull.Value && lRow["Clasificación"] != null)
                            {
                                Obj.Clasificacion.Nombre = lRow["Clasificación"] is DBNull ? string.Empty : lRow["Clasificación"]?.ToString() ?? string.Empty;
                            }

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

        public static List<clsBeProveedor_tiempos>? Get_All_Tiempos_By_IdProveedor(int pIdProveedor,
                                                                                  SqlConnection lConnection,
                                                                                  SqlTransaction lTransaction)
        {
            List<clsBeProveedor_tiempos>? lReturnList = null;
            string vSQL = "SELECT * FROM VW_TiempoProveedor WHERE activo=1 AND IdProveedor=@pIdProveedor";

            try
            {
                using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    lDTA.SelectCommand.Transaction = lTransaction;
                    lDTA.SelectCommand.Parameters.AddWithValue("@pIdProveedor", pIdProveedor);

                    DataTable lDT = new DataTable();
                    lDTA.Fill(lDT);

                    if (lDT != null && lDT.Rows.Count > 0)
                    {
                        lReturnList = new List<clsBeProveedor_tiempos>();

                        foreach (DataRow lRow in lDT.Rows)
                        {
                            clsBeProveedor_tiempos BeProveedorTiempos = new clsBeProveedor_tiempos();
                            BeProveedorTiempos.Familia = new clsBeProducto_familia();
                            BeProveedorTiempos.Clasificacion = new clsBeProducto_clasificacion();
                            Cargar(ref BeProveedorTiempos, lRow);

                            if (lRow["Familia"] != DBNull.Value && lRow["Familia"] != null)
                            {                                
                                BeProveedorTiempos.Familia.Nombre = lRow["Familia"] is DBNull ? string.Empty : lRow["Familia"]?.ToString() ?? string.Empty;

                            }

                            if (lRow["Clasificación"] != DBNull.Value && lRow["Clasificación"] != null)
                            {                                
                                BeProveedorTiempos.Clasificacion.Nombre = lRow["Clasificación"] is DBNull ? string.Empty : lRow["Clasificación"]?.ToString() ?? string.Empty;
                            }

                            lReturnList.Add(BeProveedorTiempos);
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

        public static bool Obtener(ref clsBeProveedor_tiempos oBeProveedor_tiempos,
                                   SqlConnection lConnection,
                                   SqlTransaction lTransaction)
        {
            bool result = false;

            try
            {
                string sp = "SELECT * FROM Proveedor_tiempos WHERE (IdProveedor = @IdProveedor)";

                using (SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text })
                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@IDPROVEEDOR", oBeProveedor_tiempos.IdProveedor));
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        Cargar(ref oBeProveedor_tiempos, dt.Rows[0]);
                        result = true;
                    }
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}