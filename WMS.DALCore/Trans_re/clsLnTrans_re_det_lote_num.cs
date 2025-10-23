using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using WMSWebAPI.Be;

public class clsLnTrans_re_det_lote_num
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_re_det_lote_num oBeTrans_re_det_lote_num, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }        
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }        
        double GetDecimal(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeTrans_re_det_lote_num.IdLoteNum = GetInt("IdLoteNum");
            oBeTrans_re_det_lote_num.IdRecepcionEnc = GetInt("IdRecepcionEnc");
            oBeTrans_re_det_lote_num.IdProductoBodega = GetInt("IdProductoBodega");
            oBeTrans_re_det_lote_num.Codigo = GetString("Codigo");
            oBeTrans_re_det_lote_num.Lote = GetString("Lote");
            oBeTrans_re_det_lote_num.Lote_Numerico = GetInt("Lote_Numerico");
            oBeTrans_re_det_lote_num.FechaIngreso = GetDate("FechaIngreso");
            oBeTrans_re_det_lote_num.Cantidad = GetDecimal("Cantidad");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static int Insertar(clsBeTrans_re_det_lote_num oBeTrans_re_det_lote_num, SqlConnection pConection, SqlTransaction pTransaction)
    {
        SqlCommand cmd = new SqlCommand();

        try
        {
            Ins.Init("trans_re_det_lote_num");
            Ins.Add("idlotenum", "@idlotenum", "F");
            Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("lote", "@lote", "F");
            Ins.Add("lote_numerico", "@lote_numerico", "F");
            Ins.Add("fechaingreso", "@fechaingreso", "F");
            Ins.Add("cantidad", "@cantidad", "F");

            string sp = Ins.SQL();
            cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            cmd.Parameters.Add(new SqlParameter("@IdLoteNum", oBeTrans_re_det_lote_num.IdLoteNum));
            cmd.Parameters.Add(new SqlParameter("@IdRecepcionEnc", oBeTrans_re_det_lote_num.IdRecepcionEnc));
            cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", oBeTrans_re_det_lote_num.IdProductoBodega));
            cmd.Parameters.Add(new SqlParameter("@Codigo", oBeTrans_re_det_lote_num.Codigo));
            cmd.Parameters.Add(new SqlParameter("@Lote", oBeTrans_re_det_lote_num.Lote));
            cmd.Parameters.Add(new SqlParameter("@Lote_Numerico", oBeTrans_re_det_lote_num.Lote_Numerico));
            cmd.Parameters.Add(new SqlParameter("@FechaIngreso", oBeTrans_re_det_lote_num.FechaIngreso));
            cmd.Parameters.Add(new SqlParameter("@Cantidad", oBeTrans_re_det_lote_num.Cantidad));

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

    public static int Insertar(IConfiguration config, clsBeTrans_re_det_lote_num oBeTrans_re_det_lote_num)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_re_det_lote_num");
            Ins.Add("idlotenum", "@idlotenum", "F");
            Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("lote", "@lote", "F");
            Ins.Add("lote_numerico", "@lote_numerico", "F");
            Ins.Add("fechaingreso", "@fechaingreso", "F");
            Ins.Add("cantidad", "@cantidad", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@IdLoteNum", oBeTrans_re_det_lote_num.IdLoteNum));
            cmd.Parameters.Add(new SqlParameter("@IdRecepcionEnc", oBeTrans_re_det_lote_num.IdRecepcionEnc));
            cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", oBeTrans_re_det_lote_num.IdProductoBodega));
            cmd.Parameters.Add(new SqlParameter("@Codigo", oBeTrans_re_det_lote_num.Codigo));
            cmd.Parameters.Add(new SqlParameter("@Lote", oBeTrans_re_det_lote_num.Lote));
            cmd.Parameters.Add(new SqlParameter("@Lote_Numerico", oBeTrans_re_det_lote_num.Lote_Numerico));
            cmd.Parameters.Add(new SqlParameter("@FechaIngreso", oBeTrans_re_det_lote_num.FechaIngreso));
            cmd.Parameters.Add(new SqlParameter("@Cantidad", oBeTrans_re_det_lote_num.Cantidad));

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

    public static int Actualizar(clsBeTrans_re_det_lote_num oBeTrans_re_det_lote_num, SqlConnection pConection, SqlTransaction pTransaction)
    {
        SqlCommand cmd = new SqlCommand();

        try
        {
            Upd.Init("trans_re_det_lote_num");
            Upd.Add("idlotenum", "@idlotenum", "F");
            Upd.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Upd.Add("idproductobodega", "@idproductobodega", "F");
            Upd.Add("codigo", "@codigo", "F");
            Upd.Add("lote", "@lote", "F");
            Upd.Add("lote_numerico", "@lote_numerico", "F");
            Upd.Add("fechaingreso", "@fechaingreso", "F");
            Upd.Add("cantidad", "@cantidad", "F");
            Upd.Where("IdLoteNum = @IdLoteNum");

            string sp = Upd.SQL();
            cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            cmd.Parameters.Add(new SqlParameter("@IdLoteNum", oBeTrans_re_det_lote_num.IdLoteNum));
            cmd.Parameters.Add(new SqlParameter("@IdRecepcionEnc", oBeTrans_re_det_lote_num.IdRecepcionEnc));
            cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", oBeTrans_re_det_lote_num.IdProductoBodega));
            cmd.Parameters.Add(new SqlParameter("@Codigo", oBeTrans_re_det_lote_num.Codigo));
            cmd.Parameters.Add(new SqlParameter("@Lote", oBeTrans_re_det_lote_num.Lote));
            cmd.Parameters.Add(new SqlParameter("@Lote_Numerico", oBeTrans_re_det_lote_num.Lote_Numerico));
            cmd.Parameters.Add(new SqlParameter("@FechaIngreso", oBeTrans_re_det_lote_num.FechaIngreso));
            cmd.Parameters.Add(new SqlParameter("@Cantidad", oBeTrans_re_det_lote_num.Cantidad));

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

    public int Eliminar(IConfiguration config, clsBeTrans_re_det_lote_num oBeTrans_re_det_lote_num, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_re_det_lote_num" +
             "  Where(IdLoteNum = @IdLoteNum)");

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

            cmd.Parameters.Add(new SqlParameter("@IdLoteNum", oBeTrans_re_det_lote_num.IdLoteNum));

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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_re_det_lote_num pBeTrans_re_det_lote_num)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_re_det_lote_num" +
            " Where(IdLoteNum = @IdLoteNum)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdLoteNum", pBeTrans_re_det_lote_num.IdLoteNum));            

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_re_det_lote_num, r);
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

    public static List<clsBeTrans_re_det_lote_num> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_re_det_lote_num> lreturnList = new List<clsBeTrans_re_det_lote_num>();

        try
        {
            const string sp = "Select * FROM Trans_re_det_lote_num";

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

                        clsBeTrans_re_det_lote_num vBeTrans_re_det_lote_num = new clsBeTrans_re_det_lote_num();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_re_det_lote_num = new clsBeTrans_re_det_lote_num();
                            Cargar(ref vBeTrans_re_det_lote_num, dr);
                            lreturnList.Add(vBeTrans_re_det_lote_num);
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

            const string sp = "Select ISNULL(Max(IdLoteNum),0) FROM Trans_re_det_lote_num";

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
        try
        {
            const string sp = "Select ISNULL(Max(IdLoteNum),0) FROM Trans_re_det_lote_num";

            using (var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
            {
                object lreturnValue = cmd.ExecuteScalar();

                if (lreturnValue != DBNull.Value && lreturnValue != null)
                {
                    return Convert.ToInt32(lreturnValue);
                }
            }

            return 0;
        }
        catch (Exception)
        {
            throw;
        }
    }

}