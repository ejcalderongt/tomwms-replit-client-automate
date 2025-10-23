using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WMSWebAPI.Be;

public class clsLnProducto_talla_color
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeProducto_talla_color oBeProducto_talla_color, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }        
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        
        try
        {
            oBeProducto_talla_color.IdProductoTallaColor = GetInt("IdProductoTallaColor");
            oBeProducto_talla_color.IdProducto = GetInt("IdProducto");
            oBeProducto_talla_color.IdTalla = GetInt("IdTalla");
            oBeProducto_talla_color.IdColor = GetInt("IdColor");
            oBeProducto_talla_color.IdCampaña = GetInt("IdCampaña");
            oBeProducto_talla_color.CodigoSKU = GetString("CodigoSKU");
            oBeProducto_talla_color.Fec_agr = GetDate("fec_agr");
            oBeProducto_talla_color.User_agr = GetString("user_agr");
            oBeProducto_talla_color.Fec_mod = GetDate("fec_mod");
            oBeProducto_talla_color.User_mod = GetString("user_mod");
        }
        catch (Exception)
        {            
            throw;
        }
    }

    public static int Insertar(clsBeProducto_talla_color oBeProducto_talla_color,
                          SqlConnection pConection,
                          SqlTransaction pTransaction)
    {
        Ins.Init("producto_talla_color");
        Ins.Add("idproductotallacolor", "@idproductotallacolor", "F");
        Ins.Add("idproducto", "@idproducto", "F");
        Ins.Add("idtalla", "@idtalla", "F");
        Ins.Add("idcolor", "@idcolor", "F");
        Ins.Add("idcampaña", "@idcampaña", "F");
        Ins.Add("codigosku", "@codigosku", "F");
        Ins.Add("fec_agr", "@fec_agr", "F");
        Ins.Add("user_agr", "@user_agr", "F");
        Ins.Add("fec_mod", "@fec_mod", "F");
        Ins.Add("user_mod", "@user_mod", "F");

        string sp = Ins.SQL();

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@IdProductoTallaColor", oBeProducto_talla_color.IdProductoTallaColor);
            cmd.Parameters.AddWithValue("@IdProducto", oBeProducto_talla_color.IdProducto);
            cmd.Parameters.AddWithValue("@IdTalla", oBeProducto_talla_color.IdTalla);
            cmd.Parameters.AddWithValue("@IdColor", oBeProducto_talla_color.IdColor);
            cmd.Parameters.AddWithValue("@IdCampaña", oBeProducto_talla_color.IdCampaña);
            cmd.Parameters.AddWithValue("@CodigoSKU", oBeProducto_talla_color.CodigoSKU ?? string.Empty);
            cmd.Parameters.AddWithValue("@fec_agr", oBeProducto_talla_color.Fec_agr);
            cmd.Parameters.AddWithValue("@user_agr", oBeProducto_talla_color.User_agr ?? string.Empty);
            cmd.Parameters.AddWithValue("@fec_mod", oBeProducto_talla_color.Fec_mod);
            cmd.Parameters.AddWithValue("@user_mod", oBeProducto_talla_color.User_mod ?? string.Empty);

            return cmd.ExecuteNonQuery();
        }
    }

    public static int Insertar(IConfiguration config, clsBeProducto_talla_color oBeProducto_talla_color)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("producto_talla_color");
            Ins.Add("idproductotallacolor", "@idproductotallacolor", "F");
            Ins.Add("idproducto", "@idproducto", "F");
            Ins.Add("idtalla", "@idtalla", "F");
            Ins.Add("idcolor", "@idcolor", "F");
            Ins.Add("idcampaña", "@idcampaña", "F");
            Ins.Add("codigosku", "@codigosku", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("user_mod", "@user_mod", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@IdProductoTallaColor", oBeProducto_talla_color.IdProductoTallaColor));
            cmd.Parameters.Add(new SqlParameter("@IdProducto", oBeProducto_talla_color.IdProducto));
            cmd.Parameters.Add(new SqlParameter("@IdTalla", oBeProducto_talla_color.IdTalla));
            cmd.Parameters.Add(new SqlParameter("@IdColor", oBeProducto_talla_color.IdColor));
            cmd.Parameters.Add(new SqlParameter("@IdCampaña", oBeProducto_talla_color.IdCampaña));
            cmd.Parameters.Add(new SqlParameter("@CodigoSKU", oBeProducto_talla_color.CodigoSKU));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeProducto_talla_color.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeProducto_talla_color.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeProducto_talla_color.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeProducto_talla_color.User_mod));

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

    public static int Actualizar(clsBeProducto_talla_color oBeProducto_talla_color,
                              SqlConnection pConection,
                              SqlTransaction pTransaction)
    {
        Upd.Init("producto_talla_color");
        Upd.Add("idproductotallacolor", "@idproductotallacolor", "F");
        Upd.Add("idproducto", "@idproducto", "F");
        Upd.Add("idtalla", "@idtalla", "F");
        Upd.Add("idcolor", "@idcolor", "F");
        Upd.Add("idcampaña", "@idcampaña", "F");
        Upd.Add("codigosku", "@codigosku", "F");
        Upd.Add("fec_agr", "@fec_agr", "F");
        Upd.Add("user_agr", "@user_agr", "F");
        Upd.Add("fec_mod", "@fec_mod", "F");
        Upd.Add("user_mod", "@user_mod", "F");
        Upd.Where("IdProductoTallaColor = @IdProductoTallaColor");

        string sp = Upd.SQL();

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@IdProductoTallaColor", oBeProducto_talla_color.IdProductoTallaColor);
            cmd.Parameters.AddWithValue("@IdProducto", oBeProducto_talla_color.IdProducto);
            cmd.Parameters.AddWithValue("@IdTalla", oBeProducto_talla_color.IdTalla);
            cmd.Parameters.AddWithValue("@IdColor", oBeProducto_talla_color.IdColor);
            cmd.Parameters.AddWithValue("@IdCampaña", oBeProducto_talla_color.IdCampaña);
            cmd.Parameters.AddWithValue("@CodigoSKU", oBeProducto_talla_color.CodigoSKU ?? string.Empty);
            cmd.Parameters.AddWithValue("@fec_agr", oBeProducto_talla_color.Fec_agr);
            cmd.Parameters.AddWithValue("@user_agr", oBeProducto_talla_color.User_agr ?? string.Empty);
            cmd.Parameters.AddWithValue("@fec_mod", oBeProducto_talla_color.Fec_mod);
            cmd.Parameters.AddWithValue("@user_mod", oBeProducto_talla_color.User_mod ?? string.Empty);

            return cmd.ExecuteNonQuery();
        }
    }

    public int Eliminar(IConfiguration config, clsBeProducto_talla_color oBeProducto_talla_color, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Producto_talla_color" +
             "  Where(IdProductoTallaColor = @IdProductoTallaColor)");

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

            cmd.Parameters.Add(new SqlParameter("@IdProductoTallaColor", oBeProducto_talla_color.IdProductoTallaColor));

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

    public static bool GetSingle(IConfiguration config, ref clsBeProducto_talla_color pBeProducto_talla_color)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Producto_talla_color" +
            " Where(IdProductoTallaColor = @IdProductoTallaColor)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdProductoTallaColor", pBeProducto_talla_color.IdProductoTallaColor));            

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeProducto_talla_color, r);
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

    public static List<clsBeProducto_talla_color> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeProducto_talla_color> lreturnList = new List<clsBeProducto_talla_color>();

        try
        {
            const string sp = "Select * FROM Producto_talla_color";

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

                        clsBeProducto_talla_color vBeProducto_talla_color = new clsBeProducto_talla_color();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeProducto_talla_color = new clsBeProducto_talla_color();
                            Cargar(ref vBeProducto_talla_color, dr);
                            lreturnList.Add(vBeProducto_talla_color);
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

            const string sp = "Select ISNULL(Max(IdProductoTallaColor),0) FROM Producto_talla_color";

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {
                lConnection.Open();

                using (lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    using (SqlCommand lCommand = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text })
                    {
                        Object lreturnValue = lCommand.ExecuteScalar();
                        if (lreturnValue != DBNull.Value && lreturnValue != null)
                        {
                            lMax = int.Parse((String)lreturnValue);
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
        const string sp = "Select ISNULL(Max(IdProductoTallaColor),0) FROM Producto_talla_color";

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

    public static clsBeProducto_talla_color? GetSingle(int IdProductoTallaColor,
                                                      SqlConnection lConnection,
                                                      SqlTransaction lTransaction)
    {
        try
        {
            const string sp = @"SELECT * FROM Producto_talla_color 
                           WHERE IdProductoTallaColor = @IdProductoTallaColor";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoTallaColor", IdProductoTallaColor);

                DataTable lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable?.Rows.Count > 0)
                {
                    clsBeProducto_talla_color vBeProducto_talla_color = new clsBeProducto_talla_color();
                    Cargar(ref vBeProducto_talla_color, lDataTable.Rows[0]);
                    return vBeProducto_talla_color;
                }
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static clsBeProducto_talla_color? Get_Single_By_Params(int IdProducto, string Talla, string Color, SqlConnection lConnection, SqlTransaction lTransaction)
    {
        clsBeProducto_talla_color? result = null;

        try
        {
            const string sp = @"select * from producto_talla_color p
                           join talla t on p.IdTalla = t.IdTalla
                           join color c on p.IdColor = c.IdColor
                           Where(p.IdProducto = @IdProducto AND t.Codigo = @CodigoTalla AND c.Codigo = @CodigoColor)";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", IdProducto);
                lDTA.SelectCommand.Parameters.AddWithValue("@CodigoTalla", Talla);
                lDTA.SelectCommand.Parameters.AddWithValue("@CodigoColor", Color);

                DataTable lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                clsBeProducto_talla_color vBeProducto_talla_color = new clsBeProducto_talla_color();

                if (lDataTable != null && lDataTable.Rows.Count > 0)
                {
                    Cargar(ref vBeProducto_talla_color, lDataTable.Rows[0]);
                    result = vBeProducto_talla_color;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return result;
    }    

    public static DataTable Get_Single_Dt_By_IdProductoTallaColor(int IdProductoTallaColor, SqlConnection lConnection, SqlTransaction lTransaction)
    {
        try
        {
            const string sql = @"
                    SELECT 
                        ptc.IdProductoTallaColor AS Codigo, 
                        t.Codigo AS Talla,
                        c.Codigo AS Color,									
                        ptc.CodigoSKU AS SKU
                    FROM producto_talla_color AS ptc
                    INNER JOIN talla  AS t ON ptc.IdTalla  = t.IdTalla
                    INNER JOIN color  AS c ON ptc.IdColor  = c.IdColor
                    INNER JOIN [campaña] AS ca ON ptc.IdCampaña = ca.IdCampaña
                    WHERE ptc.IdProductoTallaColor = @IdProductoTallaColor;";

            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand(sql, lConnection, lTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@IdProductoTallaColor", SqlDbType.Int).Value = IdProductoTallaColor;

                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    dad.Fill(dt);
                }
            }

            return dt;
        }
        catch (Exception)
        {
            throw;
        }
    }
}