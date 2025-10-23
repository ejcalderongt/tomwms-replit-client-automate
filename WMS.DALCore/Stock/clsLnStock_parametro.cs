namespace WMS.DALCore.Stock
{
    using AppGlobal;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Data;  
    using WMS.EntityCore.Stock;

    public class clsLnStock_parametro
    {
        private static clsInsert Ins = new clsInsert();
        private static clsUpdate Upd = new clsUpdate();
        public static void Cargar(ref clsBeStock_parametro oBeStock_parametro, DataRow dr)
        {
            if (oBeStock_parametro == null)
                throw new ArgumentNullException(nameof(oBeStock_parametro));

            if (dr == null)
                throw new ArgumentNullException(nameof(dr));

            try
            {
                // Función local para manejar valores de forma segura
                T GetSafeValue<T>(string columnName, T defaultValue, Func<object, T> converter)
                {
                    if (dr.Table.Columns.Contains(columnName) && !Convert.IsDBNull(dr[columnName]))
                    {
                        var value = dr[columnName];
                        if (value != null)
                            return converter(value);
                    }
                    return defaultValue;
                }

                oBeStock_parametro.IdStockParametro = GetSafeValue("IdStockParametro", 0, Convert.ToInt32);
                oBeStock_parametro.IdStock = GetSafeValue("IdStock", 0, Convert.ToInt32);
                oBeStock_parametro.IdProductoParametro = GetSafeValue("IdProductoParametro", 0, Convert.ToInt32);
                oBeStock_parametro.Valor_texto = GetSafeValue("valor_texto", "", Convert.ToString) ?? "";
                oBeStock_parametro.Valor_numerico = GetSafeValue("valor_numerico", 0.0, Convert.ToDouble);
                oBeStock_parametro.Valor_fecha = GetSafeValue("valor_fecha", DateTime.Now, Convert.ToDateTime);
                oBeStock_parametro.Valor_logico = GetSafeValue("valor_logico", false, Convert.ToBoolean);
                oBeStock_parametro.User_agr = GetSafeValue("user_agr", "", Convert.ToString) ?? "";
                oBeStock_parametro.Fec_agr = GetSafeValue("fec_agr", DateTime.Now, Convert.ToDateTime);
                oBeStock_parametro.Activo = GetSafeValue("activo", false, Convert.ToBoolean);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Insertar(clsBeStock_parametro oBeStock_parametro, SqlConnection pConection, SqlTransaction pTransaction)
        {
            if (oBeStock_parametro == null)
                throw new ArgumentNullException(nameof(oBeStock_parametro));

            if (pConection == null)
                throw new ArgumentNullException(nameof(pConection));

            if (pTransaction == null)
                throw new ArgumentNullException(nameof(pTransaction));

            try
            {
                Ins.Init("stock_parametro");
                Ins.Add("idstockparametro", "@idstockparametro", "F");
                Ins.Add("idstock", "@idstock", "F");
                Ins.Add("idproductoparametro", "@idproductoparametro", "F");
                Ins.Add("valor_texto", "@valor_texto", "F");
                Ins.Add("valor_numerico", "@valor_numerico", "F");
                Ins.Add("valor_fecha", "@valor_fecha", "F");
                Ins.Add("valor_logico", "@valor_logico", "F");
                Ins.Add("user_agr", "@user_agr", "F");
                Ins.Add("fec_agr", "@fec_agr", "F");
                Ins.Add("activo", "@activo", "F");

                string sp = Ins.SQL();

                using (var cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add(new SqlParameter("@IDSTOCKPARAMETRO", oBeStock_parametro.IdStockParametro));
                    cmd.Parameters.Add(new SqlParameter("@IDSTOCK", oBeStock_parametro.IdStock));
                    cmd.Parameters.Add(new SqlParameter("@IDPRODUCTOPARAMETRO", oBeStock_parametro.IdProductoParametro));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_TEXTO", oBeStock_parametro.Valor_texto));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_NUMERICO", oBeStock_parametro.Valor_numerico));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_FECHA", oBeStock_parametro.Valor_fecha));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_LOGICO", oBeStock_parametro.Valor_logico));
                    cmd.Parameters.Add(new SqlParameter("@USER_AGR", oBeStock_parametro.User_agr));
                    cmd.Parameters.Add(new SqlParameter("@FEC_AGR", oBeStock_parametro.Fec_agr));
                    cmd.Parameters.Add(new SqlParameter("@ACTIVO", oBeStock_parametro.Activo));

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public static int Actualizar(clsBeStock_parametro oBeStock_parametro, SqlConnection pConection, SqlTransaction pTransaction)
        {
            if (oBeStock_parametro == null)
                throw new ArgumentNullException(nameof(oBeStock_parametro));

            if (pConection == null)
                throw new ArgumentNullException(nameof(pConection));

            if (pTransaction == null)
                throw new ArgumentNullException(nameof(pTransaction));

            try
            {
                Upd.Init("stock_parametro");
                Upd.Add("idstockparametro", "@idstockparametro", "F");
                Upd.Add("idstock", "@idstock", "F");
                Upd.Add("idproductoparametro", "@idproductoparametro", "F");
                Upd.Add("valor_texto", "@valor_texto", "F");
                Upd.Add("valor_numerico", "@valor_numerico", "F");
                Upd.Add("valor_fecha", "@valor_fecha", "F");
                Upd.Add("valor_logico", "@valor_logico", "F");
                Upd.Add("user_agr", "@user_agr", "F");
                Upd.Add("fec_agr", "@fec_agr", "F");
                Upd.Add("activo", "@activo", "F");
                Upd.Where("IdStockParametro = @IdStockParametro");

                string sp = Upd.SQL();

                using (var cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add(new SqlParameter("@IDSTOCKPARAMETRO", oBeStock_parametro.IdStockParametro));
                    cmd.Parameters.Add(new SqlParameter("@IDSTOCK", oBeStock_parametro.IdStock));
                    cmd.Parameters.Add(new SqlParameter("@IDPRODUCTOPARAMETRO", oBeStock_parametro.IdProductoParametro));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_TEXTO", oBeStock_parametro.Valor_texto));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_NUMERICO", oBeStock_parametro.Valor_numerico));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_FECHA", oBeStock_parametro.Valor_fecha));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_LOGICO", oBeStock_parametro.Valor_logico));
                    cmd.Parameters.Add(new SqlParameter("@USER_AGR", oBeStock_parametro.User_agr));
                    cmd.Parameters.Add(new SqlParameter("@FEC_AGR", oBeStock_parametro.Fec_agr));
                    cmd.Parameters.Add(new SqlParameter("@ACTIVO", oBeStock_parametro.Activo));

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public static int Eliminar(clsBeStock_parametro oBeStock_parametro, SqlConnection pConection, SqlTransaction pTransaction)
        {
            if (oBeStock_parametro == null)
                throw new ArgumentNullException(nameof(oBeStock_parametro));

            if (pConection == null)
                throw new ArgumentNullException(nameof(pConection));

            if (pTransaction == null)
                throw new ArgumentNullException(nameof(pTransaction));

            try
            {
                const string sp = @"DELETE FROM Stock_parametro 
                               WHERE IdStockParametro = @IdStockParametro";

                using (var cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@IDSTOCKPARAMETRO", oBeStock_parametro.IdStockParametro));
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public static int EliminarTodos(SqlConnection pConection, SqlTransaction pTransaction)
        {
            if (pConection == null)
                throw new ArgumentNullException(nameof(pConection));

            if (pTransaction == null)
                throw new ArgumentNullException(nameof(pTransaction));

            try
            {
                const string sp = "DELETE FROM Stock_parametro";

                using (var cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public static int MaxID(IConfiguration config)
        {
            try
            {
                const string sp = "SELECT ISNULL(MAX(IdStockParametro), 0) FROM Stock_parametro";

                using (var lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    lConnection.Open();

                    using (var lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        using (var lCommand = new SqlCommand(sp, lConnection, lTransaction))
                        {
                            lCommand.CommandType = CommandType.Text;
                            object lReturnValue = lCommand.ExecuteScalar();

                            if (lReturnValue != DBNull.Value && lReturnValue != null)
                            {
                                return Convert.ToInt32(lReturnValue);
                            }
                            return 0;
                        }
                    }
                }
            }            
            catch (Exception)
            {                
                throw;
            }
        }
        public static int MaxID(SqlConnection lConnection, SqlTransaction lTransaction)
        {
            if (lConnection == null)
                throw new ArgumentNullException(nameof(lConnection));

            if (lTransaction == null)
                throw new ArgumentNullException(nameof(lTransaction));

            try
            {
                const string sp = "SELECT ISNULL(MAX(IdStockParametro), 0) FROM Stock_parametro";

                using (var lCommand = new SqlCommand(sp, lConnection, lTransaction))
                {
                    lCommand.CommandType = CommandType.Text;
                    object lReturnValue = lCommand.ExecuteScalar();

                    if (lReturnValue != DBNull.Value && lReturnValue != null)
                    {
                        return Convert.ToInt32(lReturnValue);
                    }
                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Insertar_Stock_Parametro_Recepcion(clsBeStock_rec bo,
                                                             int IdStock,
                                                             SqlConnection lConnection,
                                                             SqlTransaction lTransaction)
        {
            if (bo == null)
                throw new ArgumentNullException(nameof(bo));

            if (lConnection == null)
                throw new ArgumentNullException(nameof(lConnection));

            if (lTransaction == null)
                throw new ArgumentNullException(nameof(lTransaction));

            int vRegistros = 0;

            try
            {
                int lMaxSP = MaxID(lConnection, lTransaction);

                var listadetp = clsLnTrans_re_det_parametros.Get_All_By_IdRecepcionEnc_And_IdRecepcionDet(bo.IdRecepcionEnc,
                                                                                                          bo.IdRecepcionDet,
                                                                                                          lConnection,
                                                                                                          lTransaction);

                if (listadetp != null && listadetp.Count > 0)
                {
                    foreach (var dp in listadetp)
                    {
                        if (dp == null)
                            continue;

                        var BeTransReDetParam = new clsBeStock_parametro();
                        clsPublic.CopyObject(dp, ref BeTransReDetParam);
                        lMaxSP += 1;
                        BeTransReDetParam.IdStockParametro = lMaxSP;
                        BeTransReDetParam.IdStock = IdStock;
                        vRegistros += Insertar(BeTransReDetParam, lConnection, lTransaction);
                    }
                }

                return vRegistros;
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
