namespace WMS.DALCore.I_nav_barras_pallet
{
    using System;
    using System.Data;    
    using System.Collections.Generic;
    using WMS.EntityCore.I_nav_Ped_Compra;
    using Microsoft.Data.SqlClient;
    using WMS.EntityCore.Producto;    
    using Microsoft.Extensions.Configuration;
    using AppGlobal;

    public class clsLnI_nav_barras_pallet
    {
        private static clsInsert Ins = new clsInsert();
        private static clsUpdate Upd = new clsUpdate();
        public static void Cargar(ref clsBeI_nav_barras_pallet oBeI_nav_barras_pallet, DataRow dr)
        {
            try
            {
                oBeI_nav_barras_pallet.IdPallet = (dr["IdPallet"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IdPallet"]);                
                oBeI_nav_barras_pallet.Camas_Por_Tarima = (dr["CAMAS_POR_TARIMA"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["CAMAS_POR_TARIMA"]);
                oBeI_nav_barras_pallet.Cajas_Por_Cama = (dr["Cajas_Por_Cama"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Cajas_Por_Cama"]);
                oBeI_nav_barras_pallet.Cantidad_Presentacion = (dr["Cantidad_Presentacion"] == DBNull.Value) ? 0.0 : Convert.ToDouble(dr["Cantidad_Presentacion"]);
                oBeI_nav_barras_pallet.Cantidad_UMP = (dr["cantidad_ump"] == DBNull.Value) ? 0.0 : Convert.ToDouble(dr["cantidad_ump"]);
                oBeI_nav_barras_pallet.Lote_Numerico = (dr["Lote_Numerico"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["Lote_Numerico"]);
                oBeI_nav_barras_pallet.Fecha_Agregado = (dr["Fecha_Agregado"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["Fecha_Agregado"]);
                oBeI_nav_barras_pallet.Fecha_Ingreso = (dr["Fecha_Ingreso"] == DBNull.Value) ? new DateTime(1990, 1, 1) : Convert.ToDateTime(dr["Fecha_Ingreso"]);
                oBeI_nav_barras_pallet.Fecha_Vence = (dr["Fecha_Vence"] == DBNull.Value) ? new DateTime(1990, 1, 1) : Convert.ToDateTime(dr["Fecha_Vence"]);
                oBeI_nav_barras_pallet.Fecha_Produccion = (dr["Fecha_Produccion"] == DBNull.Value) ? new DateTime(1990, 1, 1) : Convert.ToDateTime(dr["Fecha_Produccion"]);
                oBeI_nav_barras_pallet.Activo = (dr["Activo"] == DBNull.Value) ? true : Convert.ToBoolean(dr["Activo"]);
                oBeI_nav_barras_pallet.Recibido = (dr["Recibido"] == DBNull.Value) ? false : Convert.ToBoolean(dr["Recibido"]);
                oBeI_nav_barras_pallet.IdRecepcion = (dr["IdRecepcion"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IdRecepcion"]);
                oBeI_nav_barras_pallet.Codigo = (dr["Codigo"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Codigo"]) ?? string.Empty;
                oBeI_nav_barras_pallet.Nombre = (dr["Nombre"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Nombre"]) ?? string.Empty;
                oBeI_nav_barras_pallet.UM_Producto = (dr["UM_Producto"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["UM_Producto"]) ?? string.Empty;
                oBeI_nav_barras_pallet.Lote = (dr["Lote"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Lote"]) ?? string.Empty;
                oBeI_nav_barras_pallet.Bodega_Origen = (dr["Bodega_Origen"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Bodega_Origen"]) ?? string.Empty;
                oBeI_nav_barras_pallet.Bodega_Destino = (dr["Bodega_Destino"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Bodega_Destino"]) ?? string.Empty;
                oBeI_nav_barras_pallet.Codigo_barra = (dr["codigo_barra"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["codigo_barra"]) ?? string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int Insertar(clsBeI_nav_barras_pallet oBeI_nav_barras_pallet, SqlConnection pConection, SqlTransaction pTransaction)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                Ins.Init("i_nav_barras_pallet");
                Ins.Add("idpallet", "@idpallet", "F");
                Ins.Add("codigo", "@codigo", "F");
                Ins.Add("nombre", "@nombre", "F");
                Ins.Add("CAMAS_POR_TARIMA", "@CAMAS_POR_TARIMA", "F");
                Ins.Add("cajas_por_cama", "@cajas_por_cama", "F");
                Ins.Add("cantidad_presentacion", "@cantidad_presentacion", "F");
                Ins.Add("cantidad_ump", "@cantidad_ump", "F");
                Ins.Add("um_producto", "@um_producto", "F");
                Ins.Add("lote", "@lote", "F");
                Ins.Add("lote_numerico", "@lote_numerico", "F");
                Ins.Add("fecha_agregado", "@fecha_agregado", "F");
                Ins.Add("fecha_ingreso", "@fecha_ingreso", "F");
                Ins.Add("fecha_vence", "@fecha_vence", "F");
                Ins.Add("fecha_produccion", "@fecha_produccion", "F");
                Ins.Add("activo", "@activo", "F");
                Ins.Add("recibido", "@recibido", "F");
                Ins.Add("idrecepcion", "@idrecepcion", "F");
                Ins.Add("bodega_origen", "@bodega_origen", "F");
                Ins.Add("bodega_destino", "@bodega_destino", "F");
                Ins.Add("codigo_barra", "@codigo_barra", "F");

                string sp = Ins.SQL();
                cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

                cmd.Parameters.Add(new SqlParameter("@IDPALLET", oBeI_nav_barras_pallet.IdPallet));
                cmd.Parameters.Add(new SqlParameter("@CODIGO", oBeI_nav_barras_pallet.Codigo ?? string.Empty));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeI_nav_barras_pallet.Nombre ?? string.Empty)));
                cmd.Parameters.Add(new SqlParameter("@CAMAS_POR_TARIMA", oBeI_nav_barras_pallet.Camas_Por_Tarima));
                cmd.Parameters.Add(new SqlParameter("@CAJAS_POR_CAMA", oBeI_nav_barras_pallet.Cajas_Por_Cama));
                cmd.Parameters.Add(new SqlParameter("@CANTIDAD_PRESENTACION", oBeI_nav_barras_pallet.Cantidad_Presentacion));
                cmd.Parameters.Add(new SqlParameter("@CANTIDAD_UMP", oBeI_nav_barras_pallet.Cantidad_UMP));
                cmd.Parameters.Add(new SqlParameter("@UM_PRODUCTO", oBeI_nav_barras_pallet.UM_Producto ?? string.Empty));
                cmd.Parameters.Add(new SqlParameter("@LOTE", oBeI_nav_barras_pallet.Lote ?? string.Empty));
                cmd.Parameters.Add(new SqlParameter("@LOTE_NUMERICO", oBeI_nav_barras_pallet.Lote_Numerico));
                cmd.Parameters.Add(new SqlParameter("@FECHA_AGREGADO", oBeI_nav_barras_pallet.Fecha_Agregado));
                cmd.Parameters.Add(new SqlParameter("@FECHA_INGRESO", oBeI_nav_barras_pallet.Fecha_Ingreso == new DateTime(1990, 1, 1) ? (object)DBNull.Value : oBeI_nav_barras_pallet.Fecha_Ingreso));
                cmd.Parameters.Add(new SqlParameter("@FECHA_VENCE", oBeI_nav_barras_pallet.Fecha_Vence == new DateTime(1990, 1, 1) ? (object)DBNull.Value : oBeI_nav_barras_pallet.Fecha_Vence));
                cmd.Parameters.Add(new SqlParameter("@FECHA_PRODUCCION", oBeI_nav_barras_pallet.Fecha_Produccion == new DateTime(1990, 1, 1) ? (object)DBNull.Value : oBeI_nav_barras_pallet.Fecha_Produccion));
                cmd.Parameters.Add(new SqlParameter("@ACTIVO", oBeI_nav_barras_pallet.Activo));
                cmd.Parameters.Add(new SqlParameter("@RECIBIDO", oBeI_nav_barras_pallet.Recibido));
                cmd.Parameters.Add(new SqlParameter("@IDRECEPCION", oBeI_nav_barras_pallet.IdRecepcion));
                cmd.Parameters.Add(new SqlParameter("@BODEGA_ORIGEN", oBeI_nav_barras_pallet.Bodega_Origen ?? string.Empty));
                cmd.Parameters.Add(new SqlParameter("@BODEGA_DESTINO", oBeI_nav_barras_pallet.Bodega_Destino ?? string.Empty));
                cmd.Parameters.Add(new SqlParameter("@CODIGO_BARRA", oBeI_nav_barras_pallet.Codigo_barra ?? string.Empty));

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
        public static int Actualizar(ref clsBeI_nav_barras_pallet oBeI_nav_barras_pallet, SqlConnection pConection, SqlTransaction pTransaction)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                Upd.Init("i_nav_barras_pallet");
                Upd.Add("idpallet", "@idpallet", "F");
                Upd.Add("codigo", "@codigo", "F");
                Upd.Add("nombre", "@nombre", "F");
                Upd.Add("CAMAS_POR_TARIMA", "@CAMAS_POR_TARIMA", "F");
                Upd.Add("cajas_por_cama", "@cajas_por_cama", "F");
                Upd.Add("cantidad_presentacion", "@cantidad_presentacion", "F");
                Upd.Add("cantidad_ump", "@cantidad_ump", "F");
                Upd.Add("um_producto", "@um_producto", "F");
                Upd.Add("lote", "@lote", "F");
                Upd.Add("lote_numerico", "@lote_numerico", "F");
                Upd.Add("fecha_agregado", "@fecha_agregado", "F");
                Upd.Add("fecha_ingreso", "@fecha_ingreso", "F");
                Upd.Add("fecha_vence", "@fecha_vence", "F");
                Upd.Add("fecha_produccion", "@fecha_produccion", "F");
                Upd.Add("activo", "@activo", "F");
                Upd.Add("recibido", "@recibido", "F");
                Upd.Add("idrecepcion", "@idrecepcion", "F");
                Upd.Add("bodega_origen", "@bodega_origen", "F");
                Upd.Add("bodega_destino", "@bodega_destino", "F");
                Upd.Add("codigo_barra", "@codigo_barra", "F");
                Upd.Where("IdPallet = @IdPallet");

                string sp = Upd.SQL();
                cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

                cmd.Parameters.Add(new SqlParameter("@IDPALLET", oBeI_nav_barras_pallet.IdPallet));
                cmd.Parameters.Add(new SqlParameter("@CODIGO", oBeI_nav_barras_pallet.Codigo ?? string.Empty));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeI_nav_barras_pallet.Nombre ?? string.Empty)));
                cmd.Parameters.Add(new SqlParameter("@CAMAS_POR_TARIMA", oBeI_nav_barras_pallet.Camas_Por_Tarima));
                cmd.Parameters.Add(new SqlParameter("@CAJAS_POR_CAMA", oBeI_nav_barras_pallet.Cajas_Por_Cama));
                cmd.Parameters.Add(new SqlParameter("@CANTIDAD_PRESENTACION", oBeI_nav_barras_pallet.Cantidad_Presentacion));
                cmd.Parameters.Add(new SqlParameter("@CANTIDAD_UMP", oBeI_nav_barras_pallet.Cantidad_UMP));
                cmd.Parameters.Add(new SqlParameter("@UM_PRODUCTO", oBeI_nav_barras_pallet.UM_Producto ?? string.Empty));
                cmd.Parameters.Add(new SqlParameter("@LOTE", oBeI_nav_barras_pallet.Lote ?? string.Empty));
                cmd.Parameters.Add(new SqlParameter("@LOTE_NUMERICO", oBeI_nav_barras_pallet.Lote_Numerico));
                cmd.Parameters.Add(new SqlParameter("@FECHA_AGREGADO", oBeI_nav_barras_pallet.Fecha_Agregado));
                cmd.Parameters.Add(new SqlParameter("@FECHA_INGRESO", oBeI_nav_barras_pallet.Fecha_Ingreso == new DateTime(1990, 1, 1) ? (object)DBNull.Value : oBeI_nav_barras_pallet.Fecha_Ingreso));
                cmd.Parameters.Add(new SqlParameter("@FECHA_VENCE", oBeI_nav_barras_pallet.Fecha_Vence == new DateTime(1990, 1, 1) ? (object)DBNull.Value : oBeI_nav_barras_pallet.Fecha_Vence));
                cmd.Parameters.Add(new SqlParameter("@FECHA_PRODUCCION", oBeI_nav_barras_pallet.Fecha_Produccion == new DateTime(1990, 1, 1) ? (object)DBNull.Value : oBeI_nav_barras_pallet.Fecha_Produccion));
                cmd.Parameters.Add(new SqlParameter("@ACTIVO", oBeI_nav_barras_pallet.Activo));
                cmd.Parameters.Add(new SqlParameter("@RECIBIDO", oBeI_nav_barras_pallet.Recibido));
                cmd.Parameters.Add(new SqlParameter("@IDRECEPCION", oBeI_nav_barras_pallet.IdRecepcion));
                cmd.Parameters.Add(new SqlParameter("@BODEGA_ORIGEN", oBeI_nav_barras_pallet.Bodega_Origen ?? string.Empty));
                cmd.Parameters.Add(new SqlParameter("@BODEGA_DESTINO", oBeI_nav_barras_pallet.Bodega_Destino ?? string.Empty));
                cmd.Parameters.Add(new SqlParameter("@CODIGO_BARRA", oBeI_nav_barras_pallet.Codigo_barra ?? string.Empty));

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
        public static int Eliminar(ref clsBeI_nav_barras_pallet oBeI_nav_barras_pallet, SqlConnection pConection, SqlTransaction pTransaction)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                const string sp = " Delete from I_nav_barras_pallet Where(IdPallet = @IdPallet)";
                cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

                cmd.Parameters.Add(new SqlParameter("@IDPALLET", oBeI_nav_barras_pallet.IdPallet));

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
        public static int EliminarTodos(SqlConnection pConection, SqlTransaction pTransaction)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                const string sp = " Delete from I_nav_barras_pallet";
                cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

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
        public static DataTable Listar(IConfiguration config)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));

            try
            {
                const string sp = "SELECT * FROM I_nav_barras_pallet ";
                lConnection.Open();
                SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                SqlDataAdapter dad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dad.Fill(dt);

                lTransaction.Commit();
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                lConnection?.Dispose();
            }
        }
        public static bool Obtener(ref clsBeI_nav_barras_pallet oBeI_nav_barras_pallet, SqlConnection pConection, SqlTransaction pTransaction)
        {
            bool result = false;

            try
            {
                const string sp = "SELECT * FROM I_nav_barras_pallet Where(IdPallet = @IdPallet)";

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@IDPALLET", oBeI_nav_barras_pallet.IdPallet));
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        Cargar(ref oBeI_nav_barras_pallet, dt.Rows[0]);
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
        public static List<clsBeI_nav_barras_pallet> GetAll(SqlConnection pConection, SqlTransaction pTransaction)
        {
            List<clsBeI_nav_barras_pallet> lReturnList = new List<clsBeI_nav_barras_pallet>();

            try
            {
                const string sp = "SELECT * FROM I_nav_barras_pallet ";
                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    clsBeI_nav_barras_pallet vBeI_nav_barras_pallet;

                    foreach (DataRow dr in dt.Rows)
                    {
                        vBeI_nav_barras_pallet = new clsBeI_nav_barras_pallet();
                        Cargar(ref vBeI_nav_barras_pallet, dr);
                        lReturnList.Add(vBeI_nav_barras_pallet);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lReturnList;
        }
        public static bool GetSingle(ref clsBeI_nav_barras_pallet pBeI_nav_barras_pallet, SqlConnection pConection, SqlTransaction pTransaction)
        {
            bool result = false;

            try
            {
                const string sp = "SELECT * FROM I_nav_barras_pallet Where(IdPallet = @IdPallet) ";
                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@IDPALLET", pBeI_nav_barras_pallet.IdPallet));
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        Cargar(ref pBeI_nav_barras_pallet, dt.Rows[0]);
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
        public static int MaxID(SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                int lMax = 0;
                const string sp = "SELECT ISNULL(Max(IdPallet),0) FROM I_nav_barras_pallet";

                using (SqlCommand lCommand = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
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
        public static void Guardar_Transaccion(List<clsBeI_nav_barras_pallet> pListBarras_Pallet, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                int lMax = MaxID(pConection, pTransaction);

                if (pListBarras_Pallet != null && pListBarras_Pallet.Count > 0)
                {
                    foreach (clsBeI_nav_barras_pallet Obj in pListBarras_Pallet)
                    {
                        Obj.IdPallet = lMax + 1;
                        Insertar(Obj, pConection, pTransaction);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool Existe(string pCodigoBarraPallet, SqlConnection pConection, SqlTransaction pTransaction)
        {
            bool result = false;

            try
            {
                string vSQL = "SELECT * FROM i_nav_barras_pallet WHERE Codigo_Barra=@Codigo_Barra";

                using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, pConection))
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    lDTA.SelectCommand.Transaction = pTransaction;
                    lDTA.SelectCommand.Parameters.AddWithValue("@Codigo_Barra", pCodigoBarraPallet ?? "");

                    DataTable lDT = new DataTable();
                    lDTA.Fill(lDT);

                    if (lDT != null && lDT.Rows.Count > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
        public static List<clsBeI_nav_barras_pallet> Get_All_Activos(bool pActivo, SqlConnection pConection, SqlTransaction pTransaction)
        {
            List<clsBeI_nav_barras_pallet> lReturnList = new List<clsBeI_nav_barras_pallet>();

            try
            {
                string sp = "SELECT * FROM I_nav_barras_pallet WHERE 1 >0 ";

                if (pActivo)
                {
                    sp += " AND Activo=1";
                }
                else
                {
                    sp += " AND Activo=0";
                }

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    clsBeI_nav_barras_pallet vBeI_nav_barras_pallet;

                    foreach (DataRow dr in dt.Rows)
                    {
                        vBeI_nav_barras_pallet = new clsBeI_nav_barras_pallet();
                        Cargar(ref vBeI_nav_barras_pallet, dr);
                        lReturnList.Add(vBeI_nav_barras_pallet);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lReturnList;
        }
        public static List<clsBeI_nav_barras_pallet> Get_All_Pendientes_Recepcion(bool pActivo, SqlConnection pConection, SqlTransaction pTransaction)
        {
            List<clsBeI_nav_barras_pallet> lReturnList = new List<clsBeI_nav_barras_pallet>();

            try
            {
                string sp = "SELECT * FROM I_nav_barras_pallet WHERE 1 >0 ";

                if (pActivo)
                {
                    sp += " AND Recibido=1";
                }
                else
                {
                    sp += " AND Recibido=0";
                }

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    clsBeI_nav_barras_pallet vBeI_nav_barras_pallet;

                    foreach (DataRow dr in dt.Rows)
                    {
                        vBeI_nav_barras_pallet = new clsBeI_nav_barras_pallet();
                        Cargar(ref vBeI_nav_barras_pallet, dr);
                        lReturnList.Add(vBeI_nav_barras_pallet);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lReturnList;
        }
        public static clsBeI_nav_barras_pallet? Get_Single_By_Codigo_Barra_Pallet(string pCodigoBarraPallet, int pIdBodega, ref clsBeProducto? BeProducto, SqlConnection pConection, SqlTransaction pTransaction)
        {
            clsBeI_nav_barras_pallet? result = null;

            try
            {
                const string sp = "SELECT * FROM I_nav_barras_pallet Where(codigo_barra = @codigo_barra) ";

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@CODIGO_BARRA", pCodigoBarraPallet ?? ""));
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        clsBeI_nav_barras_pallet pBeI_nav_barras_pallet = new clsBeI_nav_barras_pallet();
                        Cargar(ref pBeI_nav_barras_pallet, dt.Rows[0]);

                        BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(pBeI_nav_barras_pallet.Codigo ?? "", pIdBodega, pConection, pTransaction);

                        if (BeProducto != null)
                        {
                            result = pBeI_nav_barras_pallet;
                        }
                        else
                        {
                            string vMensaje = string.Format("Se obtuvo la información del pallet, pero no se pudo obtener la información del código de producto:{0} asociado al identificador de bodega:{1} ", pBeI_nav_barras_pallet.Codigo, pIdBodega);
                            throw new Exception(vMensaje);
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
        public static clsBeI_nav_barras_pallet? Get_Single_Pallet_Ingreso_By_Codigo_Barra_Pallet(string pCodigoBarraPallet, int pIdBodega, ref clsBeProducto? BeProducto, SqlConnection pConection, SqlTransaction pTransaction)
        {
            clsBeI_nav_barras_pallet? result = null;

            try
            {
                const string sp = "SELECT * FROM I_nav_barras_pallet Where(codigo_barra = @codigo_barra AND recibido =0) ";

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@CODIGO_BARRA", pCodigoBarraPallet ?? ""));
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        clsBeI_nav_barras_pallet pBeI_nav_barras_pallet = new clsBeI_nav_barras_pallet();
                        Cargar(ref pBeI_nav_barras_pallet, dt.Rows[0]);
                        result = pBeI_nav_barras_pallet;

                        if (!pBeI_nav_barras_pallet.Recibido)
                        {
                            BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(pBeI_nav_barras_pallet.Codigo ?? "", pIdBodega, pConection, pTransaction);

                            if (BeProducto == null)
                            {
                                string vMensaje = string.Format("Se obtuvo la información del pallet, pero no se pudo obtener la información del código de producto:{0} asociado al identificador de bodega:{1} ", pBeI_nav_barras_pallet.Codigo, pIdBodega);
                                throw new Exception(vMensaje);
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
        public static List<clsBeI_nav_barras_pallet>? Get_All_Pallet_Ingreso_By_Codigo_Barra_Pallet(string pCodigoBarraPallet, int pIdBodega, ref clsBeProducto? BeProducto, SqlConnection pConection, SqlTransaction pTransaction)
        {
            List<clsBeI_nav_barras_pallet>? result = null;

            try
            {
                const string sp = "SELECT * FROM I_nav_barras_pallet Where(codigo_barra = @codigo_barra AND Recibido =0) ";

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@CODIGO_BARRA", (pCodigoBarraPallet ?? "").Trim()));
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        List<clsBeI_nav_barras_pallet> lBeI_nav_barras_pallet = new List<clsBeI_nav_barras_pallet>();

                        foreach (DataRow dr in dt.Rows)
                        {
                            clsBeI_nav_barras_pallet vBeI_nav_barras_pallet = new clsBeI_nav_barras_pallet();
                            Cargar(ref vBeI_nav_barras_pallet, dr);

                            if (!vBeI_nav_barras_pallet.Recibido)
                            {
                                BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(vBeI_nav_barras_pallet.Codigo ?? "", pIdBodega, pConection, pTransaction);

                                if (BeProducto == null)
                                {
                                    string vMensaje = string.Format("Se obtuvo la información del pallet, pero no se pudo obtener la información del código de producto:{0} asociado al identificador de bodega:{1} ", vBeI_nav_barras_pallet.Codigo, pIdBodega);
                                    throw new Exception(vMensaje);
                                }
                            }
                            else
                            {
                                BeProducto = null;
                            }

                            lBeI_nav_barras_pallet.Add(vBeI_nav_barras_pallet);
                        }

                        result = lBeI_nav_barras_pallet;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
        public static clsBeI_nav_barras_pallet? Get_Single_Lp_Recibido_By_Codigo_Barra_And_Bodega(string pCodigoBarraPallet, SqlConnection lConnection, SqlTransaction lTransaction)
        {
            clsBeI_nav_barras_pallet? result = null;

            try
            {
                const string sp = "SELECT * FROM I_nav_barras_pallet Where(codigo_barra = @codigo_barra AND (recibido = 1 OR recibido =2) and IdRecepcion <> 0 ) ";

                using (SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text })
                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@CODIGO_BARRA", pCodigoBarraPallet ?? ""));
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        clsBeI_nav_barras_pallet pBeI_nav_barras_pallet = new clsBeI_nav_barras_pallet();
                        Cargar(ref pBeI_nav_barras_pallet, dt.Rows[0]);
                        result = pBeI_nav_barras_pallet;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
        public static clsBeProducto? Get_Single_By_Codigo_Barra_Pallet(string pCodigoBarraPallet, int pIdBodega, SqlConnection lConnection, SqlTransaction lTransaction)
        {
            clsBeProducto? result = null;

            try
            {
                const string sp = "SELECT * FROM I_nav_barras_pallet Where(codigo_barra = @codigo_barra and recibido =0) ";

                using (SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text })
                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@CODIGO_BARRA", pCodigoBarraPallet ?? ""));
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        clsBeI_nav_barras_pallet pBeI_nav_barras_pallet = new clsBeI_nav_barras_pallet();
                        Cargar(ref pBeI_nav_barras_pallet, dt.Rows[0]);

                        clsBeProducto? BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(pBeI_nav_barras_pallet.Codigo ?? "", pIdBodega, lConnection, lTransaction);

                        if (BeProducto != null)
                        {
                            result = BeProducto;
                        }
                        else
                        {
                            string vMensaje = string.Format("Se obtuvo la información del pallet, pero no se pudo obtener la información del código de producto:{0} asociado al identificador de bodega:{1} ", pBeI_nav_barras_pallet.Codigo, pIdBodega);
                            throw new Exception(vMensaje);
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