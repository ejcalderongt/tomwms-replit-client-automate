using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WMS.EntityCore.Stock;
using WMSWebAPI.Be;

public class clsLnStock_se_rec
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeStock_se_rec oBeStock_se_rec, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }

        try
        {
            oBeStock_se_rec.IdStockSeRec = GetInt("IdStockSeRec");
            oBeStock_se_rec.IdStockRec = GetInt("IdStockRec");
            oBeStock_se_rec.IdProductoBodega = GetInt("IdProductoBodega");
            oBeStock_se_rec.NoSerie = GetString("NoSerie");
            oBeStock_se_rec.NoSerieInicial = GetString("NoSerieInicial");
            oBeStock_se_rec.NoSerieFinal = GetString("NoSerieFinal");
            oBeStock_se_rec.User_agr = GetString("user_agr");
            oBeStock_se_rec.Fec_agr = GetDate("fec_agr");
            oBeStock_se_rec.User_mod = GetString("user_mod");
            oBeStock_se_rec.Fec_mod = GetDate("fec_mod");
            oBeStock_se_rec.Activo = GetBool("activo");
            oBeStock_se_rec.Regularizado = GetBool("regularizado");
            oBeStock_se_rec.Fecha_regularizacion = GetDate("fecha_regularizacion");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static int Insertar(clsBeStock_se_rec oBeStock_se_rec, SqlConnection pConection, SqlTransaction pTransaction)
    {
        Ins.Init("stock_se_rec");
        Ins.Add("idstockserec", "@idstockserec", "F");
        Ins.Add("idstockrec", "@idstockrec", "F");
        Ins.Add("idproductobodega", "@idproductobodega", "F");
        Ins.Add("noserie", "@noserie", "F");
        Ins.Add("noserieinicial", "@noserieinicial", "F");
        Ins.Add("noseriefinal", "@noseriefinal", "F");
        Ins.Add("user_agr", "@user_agr", "F");
        Ins.Add("fec_agr", "@fec_agr", "F");
        Ins.Add("user_mod", "@user_mod", "F");
        Ins.Add("fec_mod", "@fec_mod", "F");
        Ins.Add("activo", "@activo", "F");
        Ins.Add("regularizado", "@regularizado", "F");
        Ins.Add("fecha_regularizacion", "@fecha_regularizacion", "F");

        string sp = Ins.SQL();

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@IdStockSeRec", oBeStock_se_rec.IdStockSeRec);
            cmd.Parameters.AddWithValue("@IdStockRec", oBeStock_se_rec.IdStockRec);
            cmd.Parameters.AddWithValue("@IdProductoBodega", oBeStock_se_rec.IdProductoBodega);
            cmd.Parameters.AddWithValue("@NoSerie", oBeStock_se_rec.NoSerie);
            cmd.Parameters.AddWithValue("@NoSerieInicial", oBeStock_se_rec.NoSerieInicial);
            cmd.Parameters.AddWithValue("@NoSerieFinal", oBeStock_se_rec.NoSerieFinal);
            cmd.Parameters.AddWithValue("@user_agr", oBeStock_se_rec.User_agr);
            cmd.Parameters.AddWithValue("@fec_agr", oBeStock_se_rec.Fec_agr);
            cmd.Parameters.AddWithValue("@user_mod", oBeStock_se_rec.User_mod);
            cmd.Parameters.AddWithValue("@fec_mod", oBeStock_se_rec.Fec_mod);
            cmd.Parameters.AddWithValue("@activo", oBeStock_se_rec.Activo);
            cmd.Parameters.AddWithValue("@regularizado", oBeStock_se_rec.Regularizado);
            cmd.Parameters.AddWithValue("@fecha_regularizacion", oBeStock_se_rec.Fecha_regularizacion);

            return cmd.ExecuteNonQuery();
        }
    }

    public static int Insertar(IConfiguration config, clsBeStock_se_rec oBeStock_se_rec)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("stock_se_rec");
            Ins.Add("idstockserec", "@idstockserec", "F");
            Ins.Add("idstockrec", "@idstockrec", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("noserie", "@noserie", "F");
            Ins.Add("noserieinicial", "@noserieinicial", "F");
            Ins.Add("noseriefinal", "@noseriefinal", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("regularizado", "@regularizado", "F");
            Ins.Add("fecha_regularizacion", "@fecha_regularizacion", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@IdStockSeRec", oBeStock_se_rec.IdStockSeRec));
            cmd.Parameters.Add(new SqlParameter("@IdStockRec", oBeStock_se_rec.IdStockRec));
            cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", oBeStock_se_rec.IdProductoBodega));
            cmd.Parameters.Add(new SqlParameter("@NoSerie", oBeStock_se_rec.NoSerie));
            cmd.Parameters.Add(new SqlParameter("@NoSerieInicial", oBeStock_se_rec.NoSerieInicial));
            cmd.Parameters.Add(new SqlParameter("@NoSerieFinal", oBeStock_se_rec.NoSerieFinal));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeStock_se_rec.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeStock_se_rec.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeStock_se_rec.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeStock_se_rec.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeStock_se_rec.Activo));
            cmd.Parameters.Add(new SqlParameter("@regularizado", oBeStock_se_rec.Regularizado));
            cmd.Parameters.Add(new SqlParameter("@fecha_regularizacion", oBeStock_se_rec.Fecha_regularizacion));

            rowsAffected = cmd.ExecuteNonQuery();

            if (lTransaction != null)
                lTransaction.Commit();

        }
        catch (SqlException)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            throw;
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
        return rowsAffected;
    }

    public static int Actualizar(clsBeStock_se_rec oBeStock_se_rec, SqlConnection pConection, SqlTransaction pTransaction)
    {
        Upd.Init("stock_se_rec");
        Upd.Add("idstockserec", "@idstockserec", "F");
        Upd.Add("idstockrec", "@idstockrec", "F");
        Upd.Add("idproductobodega", "@idproductobodega", "F");
        Upd.Add("noserie", "@noserie", "F");
        Upd.Add("noserieinicial", "@noserieinicial", "F");
        Upd.Add("noseriefinal", "@noseriefinal", "F");
        Upd.Add("user_agr", "@user_agr", "F");
        Upd.Add("fec_agr", "@fec_agr", "F");
        Upd.Add("user_mod", "@user_mod", "F");
        Upd.Add("fec_mod", "@fec_mod", "F");
        Upd.Add("activo", "@activo", "F");
        Upd.Add("regularizado", "@regularizado", "F");
        Upd.Add("fecha_regularizacion", "@fecha_regularizacion", "F");
        Upd.Where("IdStockSeRec = @IdStockSeRec");

        string sp = Upd.SQL();

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@IdStockSeRec", oBeStock_se_rec.IdStockSeRec);
            cmd.Parameters.AddWithValue("@IdStockRec", oBeStock_se_rec.IdStockRec);
            cmd.Parameters.AddWithValue("@IdProductoBodega", oBeStock_se_rec.IdProductoBodega);
            cmd.Parameters.AddWithValue("@NoSerie", oBeStock_se_rec.NoSerie);
            cmd.Parameters.AddWithValue("@NoSerieInicial", oBeStock_se_rec.NoSerieInicial);
            cmd.Parameters.AddWithValue("@NoSerieFinal", oBeStock_se_rec.NoSerieFinal);
            cmd.Parameters.AddWithValue("@user_agr", oBeStock_se_rec.User_agr);
            cmd.Parameters.AddWithValue("@fec_agr", oBeStock_se_rec.Fec_agr);
            cmd.Parameters.AddWithValue("@user_mod", oBeStock_se_rec.User_mod);
            cmd.Parameters.AddWithValue("@fec_mod", oBeStock_se_rec.Fec_mod);
            cmd.Parameters.AddWithValue("@activo", oBeStock_se_rec.Activo);
            cmd.Parameters.AddWithValue("@regularizado", oBeStock_se_rec.Regularizado);
            cmd.Parameters.AddWithValue("@fecha_regularizacion", oBeStock_se_rec.Fecha_regularizacion);

            return cmd.ExecuteNonQuery();
        }
    }

    public int Eliminar(IConfiguration config, clsBeStock_se_rec oBeStock_se_rec, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Stock_se_rec" +
             "  Where(IdStockSeRec = @IdStockSeRec)");

            bool Es_Transaccion_Remota = (pConection != null && pTransaction != null);

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            if (Es_Transaccion_Remota)
            {
                cmd = new SqlCommand(sp, pConection, pTransaction);
            }
            else
            {
                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                cmd = new SqlCommand(sp, lConnection, lTransaction);
            }

            cmd.Parameters.Add(new SqlParameter("@IdStockSeRec", oBeStock_se_rec.IdStockSeRec));

            int rowsAffected = cmd.ExecuteNonQuery();

            if (!Es_Transaccion_Remota)
                if (lTransaction != null)
                    lTransaction.Commit();

            return rowsAffected;

        }
        catch (SqlException)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            throw;
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
    }

    public static bool GetSingle(IConfiguration config, ref clsBeStock_se_rec pBeStock_se_rec)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Stock_se_rec" +
            " Where(IdStockSeRec = @IdStockSeRec)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdStockSeRec", pBeStock_se_rec.IdStockSeRec));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeStock_se_rec, r);
                return true;
            }

        }
        catch (SqlException)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            throw;
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
        return false;

    }

    public static List<clsBeStock_se_rec> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeStock_se_rec> lreturnList = new List<clsBeStock_se_rec>();

        try
        {
            const string sp = "Select * FROM Stock_se_rec";

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {

                lConnection.Open();

                using (lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                    {
                        lDTA.SelectCommand.CommandType = CommandType.Text;
                        lDTA.SelectCommand.Transaction = lTransaction;
                        DataTable lDataTable = new DataTable();
                        lDTA.Fill(lDataTable);

                        clsBeStock_se_rec vBeStock_se_rec = new clsBeStock_se_rec();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeStock_se_rec = new clsBeStock_se_rec();
                            Cargar(ref vBeStock_se_rec, dr);
                            lreturnList.Add(vBeStock_se_rec);
                        }

                        lTransaction.Commit();
                    }

                    lConnection.Close();

                }

            }

            return lreturnList;

        }
        catch (SqlException)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            throw;
        }
    }

    public static int MaxID(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;

        try
        {

            int lMax = 0;

            const string sp = "Select ISNULL(Max(IdStockSeRec),0) FROM Stock_se_rec";

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {
                lConnection.Open();

                using (lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    using (SqlCommand lCommand = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text })
                    {
                        var lreturnValue = lCommand.ExecuteScalar();
                        if (lreturnValue != DBNull.Value && lreturnValue != null)
                        {
                            lMax = int.Parse((string)lreturnValue);
                        }
                    }
                    lTransaction.Commit();
                }

                lConnection.Close();
            }

            return lMax;

        }
        catch (SqlException)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            throw;
        }
    }
    public static int MaxID(SqlConnection pConection, SqlTransaction pTransaction)
    {
        const string sp = "Select ISNULL(Max(IdStockSeRec),0) FROM Stock_se_rec";

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;
            object lreturnValue = cmd.ExecuteScalar();

            if (lreturnValue != DBNull.Value && lreturnValue != null)
            {
                return Convert.ToInt32(lreturnValue);
            }
        }

        return 0;
    }

    public static int Guarda_Stock_Se_Rec(List<clsBeStock_se_rec>? pListStockRecSer,
                                         List<clsBeStock_rec>? pListStockRec,
                                         SqlConnection lConnection,
                                         SqlTransaction lTransaction)
    {
        int Guarda_Stock_Se_Rec = 0;
        int vRegistros = 0;

        try
        {
            if (pListStockRec != null && pListStockRec.Count > 0 && pListStockRecSer != null)
            {
                foreach (clsBeStock_rec ObjS in pListStockRec)
                {
                    foreach (clsBeStock_se_rec BeStockSeRec in pListStockRecSer.FindAll(b => b.IdStockRec == ObjS.IdStockRec))
                    {
                        if (BeStockSeRec.IsNew)
                        {
                            BeStockSeRec.Fec_agr = DateTime.Now;
                            BeStockSeRec.Fec_mod = DateTime.Now;
                            vRegistros += Insertar(BeStockSeRec, lConnection, lTransaction);
                        }
                        else
                        {
                            BeStockSeRec.Fec_mod = DateTime.Now;
                            vRegistros += Actualizar(BeStockSeRec, lConnection, lTransaction);
                        }
                    }
                }

                Guarda_Stock_Se_Rec = vRegistros;
            }
        }
        catch (Exception)
        {
            throw;
        }

        return Guarda_Stock_Se_Rec;
    }

    public static List<clsBeStock_se_rec> GetAllSerieByIdStockRec(int pIdStockRec,
                                                             SqlConnection lConnection,
                                                             SqlTransaction lTransaction)
    {
        if (lConnection == null)
            throw new ArgumentNullException(nameof(lConnection));

        if (lTransaction == null)
            throw new ArgumentNullException(nameof(lTransaction));

        var lReturnList = new List<clsBeStock_se_rec>();

        try
        {
            const string vSQL = "SELECT * FROM stock_se_rec WHERE IdStockRec = @IdStockRec";

            using (var lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdStockRec", pIdStockRec);
                lDTA.SelectCommand.Transaction = lTransaction;

                var lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable != null && lDataTable.Rows.Count > 0)
                {
                    foreach (DataRow lRow in lDataTable.Rows)
                    {
                        var Obj = new clsBeStock_se_rec();
                        Cargar(ref Obj, lRow);
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
}
