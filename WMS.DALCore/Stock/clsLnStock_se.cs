using AppGlobal;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WMS.EntityCore.Stock
{
    public static class clsLnStock_se
    {
        private static clsInsert Ins = new clsInsert();
        private static clsUpdate Upd = new clsUpdate();
        public static void Cargar(ref clsBeStock_se oBeStock_se, DataRow dr)
        {
            if (oBeStock_se == null)
                throw new ArgumentNullException(nameof(oBeStock_se));

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

                oBeStock_se.IdStockSe = GetSafeValue("IdStockSe", 0, Convert.ToInt32);
                oBeStock_se.IdStock = GetSafeValue("IdStock", 0, Convert.ToInt32);
                oBeStock_se.IdProductoBodega = GetSafeValue("IdProductoBodega", 0, Convert.ToInt32);
                oBeStock_se.NoSerie = GetSafeValue("NoSerie", "", Convert.ToString) ?? "";
                oBeStock_se.NoSerieInicial = GetSafeValue("NoSerieInicial", "", Convert.ToString) ?? "";
                oBeStock_se.NoSerieFinal = GetSafeValue("NoSerieFinal", "", Convert.ToString) ?? "";
                oBeStock_se.User_agr = GetSafeValue("user_agr", "", Convert.ToString) ?? "";
                oBeStock_se.Fec_agr = GetSafeValue("fec_agr", DateTime.Now, Convert.ToDateTime);
                oBeStock_se.User_mod = GetSafeValue("user_mod", "", Convert.ToString) ?? "";
                oBeStock_se.Fec_mod = GetSafeValue("fec_mod", DateTime.Now, Convert.ToDateTime);
                oBeStock_se.Activo = GetSafeValue("activo", false, Convert.ToBoolean);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Insertar(clsBeStock_se oBeStock_se, SqlConnection pConection, SqlTransaction pTransaction)
        {
            if (oBeStock_se == null)
                throw new ArgumentNullException(nameof(oBeStock_se));

            if (pConection == null)
                throw new ArgumentNullException(nameof(pConection));

            if (pTransaction == null)
                throw new ArgumentNullException(nameof(pTransaction));

            try
            {
                Ins.Init("stock_se");
                Ins.Add("idstockse", "@idstockse", "F");
                Ins.Add("idstock", "@idstock", "F");
                Ins.Add("idproductobodega", "@idproductobodega", "F");
                Ins.Add("noserie", "@noserie", "F");
                Ins.Add("noserieinicial", "@noserieinicial", "F");
                Ins.Add("noseriefinal", "@noseriefinal", "F");
                Ins.Add("user_agr", "@user_agr", "F");
                Ins.Add("fec_agr", "@fec_agr", "F");
                Ins.Add("user_mod", "@user_mod", "F");
                Ins.Add("fec_mod", "@fec_mod", "F");
                Ins.Add("activo", "@activo", "F");

                string sp = Ins.SQL();

                using (var cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add(new SqlParameter("@IDSTOCKSE", oBeStock_se.IdStockSe));
                    cmd.Parameters.Add(new SqlParameter("@IDSTOCK", oBeStock_se.IdStock));
                    cmd.Parameters.Add(new SqlParameter("@IDPRODUCTOBODEGA", oBeStock_se.IdProductoBodega));
                    cmd.Parameters.Add(new SqlParameter("@NOSERIE", string.IsNullOrEmpty(oBeStock_se.NoSerie) ? DBNull.Value : oBeStock_se.NoSerie));
                    cmd.Parameters.Add(new SqlParameter("@NOSERIEINICIAL", string.IsNullOrEmpty(oBeStock_se.NoSerieInicial) ? DBNull.Value : oBeStock_se.NoSerieInicial));
                    cmd.Parameters.Add(new SqlParameter("@NOSERIEFINAL", string.IsNullOrEmpty(oBeStock_se.NoSerieFinal) ? DBNull.Value : oBeStock_se.NoSerieFinal));
                    cmd.Parameters.Add(new SqlParameter("@USER_AGR", oBeStock_se.User_agr));
                    cmd.Parameters.Add(new SqlParameter("@FEC_AGR", oBeStock_se.Fec_agr));
                    cmd.Parameters.Add(new SqlParameter("@USER_MOD", oBeStock_se.User_mod));
                    cmd.Parameters.Add(new SqlParameter("@FEC_MOD", oBeStock_se.Fec_mod));
                    cmd.Parameters.Add(new SqlParameter("@ACTIVO", oBeStock_se.Activo));

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Actualizar(clsBeStock_se oBeStock_se, SqlConnection pConection, SqlTransaction pTransaction)
        {
            if (oBeStock_se == null)
                throw new ArgumentNullException(nameof(oBeStock_se));

            if (pConection == null)
                throw new ArgumentNullException(nameof(pConection));

            if (pTransaction == null)
                throw new ArgumentNullException(nameof(pTransaction));

            try
            {
                Upd.Init("stock_se");
                Upd.Add("idstockse", "@idstockse", "F");
                Upd.Add("idstock", "@idstock", "F");
                Upd.Add("idproductobodega", "@idproductobodega", "F");
                Upd.Add("noserie", "@noserie", "F");
                Upd.Add("noserieinicial", "@noserieinicial", "F");
                Upd.Add("noseriefinal", "@noseriefinal", "F");
                Upd.Add("user_agr", "@user_agr", "F");
                Upd.Add("fec_agr", "@fec_agr", "F");
                Upd.Add("user_mod", "@user_mod", "F");
                Upd.Add("fec_mod", "@fec_mod", "F");
                Upd.Add("activo", "@activo", "F");
                Upd.Where("IdStockSe = @IdStockSe");

                string sp = Upd.SQL();

                using (var cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add(new SqlParameter("@IDSTOCKSE", oBeStock_se.IdStockSe));
                    cmd.Parameters.Add(new SqlParameter("@IDSTOCK", oBeStock_se.IdStock));
                    cmd.Parameters.Add(new SqlParameter("@IDPRODUCTOBODEGA", oBeStock_se.IdProductoBodega));
                    cmd.Parameters.Add(new SqlParameter("@NOSERIE", string.IsNullOrEmpty(oBeStock_se.NoSerie) ? DBNull.Value : oBeStock_se.NoSerie));
                    cmd.Parameters.Add(new SqlParameter("@NOSERIEINICIAL", string.IsNullOrEmpty(oBeStock_se.NoSerieInicial) ? DBNull.Value : oBeStock_se.NoSerieInicial));
                    cmd.Parameters.Add(new SqlParameter("@NOSERIEFINAL", string.IsNullOrEmpty(oBeStock_se.NoSerieFinal) ? DBNull.Value : oBeStock_se.NoSerieFinal));
                    cmd.Parameters.Add(new SqlParameter("@USER_AGR", oBeStock_se.User_agr));
                    cmd.Parameters.Add(new SqlParameter("@FEC_AGR", oBeStock_se.Fec_agr));
                    cmd.Parameters.Add(new SqlParameter("@USER_MOD", oBeStock_se.User_mod));
                    cmd.Parameters.Add(new SqlParameter("@FEC_MOD", oBeStock_se.Fec_mod));
                    cmd.Parameters.Add(new SqlParameter("@ACTIVO", oBeStock_se.Activo));

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Eliminar(clsBeStock_se oBeStock_se, SqlConnection pConection, SqlTransaction pTransaction)
        {
            if (oBeStock_se == null)
                throw new ArgumentNullException(nameof(oBeStock_se));

            if (pConection == null)
                throw new ArgumentNullException(nameof(pConection));

            if (pTransaction == null)
                throw new ArgumentNullException(nameof(pTransaction));

            try
            {
                const string sp = @"DELETE FROM Stock_se 
                               WHERE IdStockSe = @IdStockSe";

                using (var cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@IDSTOCKSE", oBeStock_se.IdStockSe));
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Eliminar_By_IdStock(int pIdStock, SqlConnection pConection, SqlTransaction pTransaction)
        {
            if (pConection == null)
                throw new ArgumentNullException(nameof(pConection));

            if (pTransaction == null)
                throw new ArgumentNullException(nameof(pTransaction));

            try
            {
                const string sp = @"DELETE FROM Stock_se 
                               WHERE IdStock = @IdStock";

                using (var cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@IdStock", pIdStock));
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool Obtener(clsBeStock_se oBeStock_se, SqlConnection pConection, SqlTransaction pTransaction)
        {
            if (oBeStock_se == null)
                throw new ArgumentNullException(nameof(oBeStock_se));

            if (pConection == null)
                throw new ArgumentNullException(nameof(pConection));

            if (pTransaction == null)
                throw new ArgumentNullException(nameof(pTransaction));

            try
            {
                const string sp = @"SELECT * FROM Stock_se 
                               WHERE IdStockSe = @IdStockSe";

                using (var cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@IDSTOCKSE", oBeStock_se.IdStockSe));

                    using (var dad = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        dad.Fill(dt);

                        if (dt.Rows.Count == 1)
                        {
                            Cargar(ref oBeStock_se, dt.Rows[0]);
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int Insertar_Stock_Serializado_Recepcion(clsBeStock_rec BeStockRec,
                                                               int IdStock,
                                                               SqlConnection lConnection,
                                                               SqlTransaction lTransaction)
        {
            if (BeStockRec == null)
                throw new ArgumentNullException(nameof(BeStockRec));

            if (lConnection == null)
                throw new ArgumentNullException(nameof(lConnection));

            if (lTransaction == null)
                throw new ArgumentNullException(nameof(lTransaction));

            int vRegistros = 0;

            try
            {
                int lMaxSE = MaxID(lConnection, lTransaction);
                var lStockSerializadoRec = clsLnStock_se_rec.GetAllSerieByIdStockRec(BeStockRec.IdStockRec,
                                                                                     lConnection,
                                                                                     lTransaction);

                if (lStockSerializadoRec != null && lStockSerializadoRec.Count > 0)
                {
                    foreach (var Stock_Se_Rec in lStockSerializadoRec)
                    {
                        if (Stock_Se_Rec == null)
                            continue;

                        var ObjS = new clsBeStock_se();
                        clsPublic.CopyObject(Stock_Se_Rec, ref ObjS);

                        lMaxSE += 1;

                        ObjS.IdStock = IdStock;
                        ObjS.IdStockSe = lMaxSE;

                        Stock_Se_Rec.Regularizado = true;
                        Stock_Se_Rec.Fecha_regularizacion = DateTime.Now;

                        vRegistros += Insertar(ObjS, lConnection, lTransaction);
                    }
                }

                return vRegistros;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public static int MaxID(SqlConnection pConnection, SqlTransaction pTransaction)
        {
            if (pConnection == null)
                throw new ArgumentNullException(nameof(pConnection));

            if (pTransaction == null)
                throw new ArgumentNullException(nameof(pTransaction));

            try
            {
                const string vSQL = "SELECT ISNULL(MAX(IdStockse), 0) FROM stock_se";

                using (var lCommand = new SqlCommand(vSQL, pConnection, pTransaction))
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
    }
}