using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WMSWebAPI.Be;

public class clsLnProducto_pallet
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeProducto_pallet oBeProducto_pallet, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }        
        double GetDecimal(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeProducto_pallet.IdPallet = GetInt("IdPallet");
            oBeProducto_pallet.IdPropietarioBodega = GetInt("IdPropietarioBodega");
            oBeProducto_pallet.IdProductoBodega = GetInt("IdProductoBodega");
            oBeProducto_pallet.IdPresentacion = GetInt("IdPresentacion");
            oBeProducto_pallet.IdOperadorBodega = GetInt("IdOperadorBodega");
            oBeProducto_pallet.IdImpresora = GetInt("IdImpresora");
            oBeProducto_pallet.IdRecepcionEnc = GetInt("IdRecepcionEnc");
            oBeProducto_pallet.Codigo_barra = GetString("codigo_barra");
            oBeProducto_pallet.Cantidad = GetDecimal("cantidad");
            oBeProducto_pallet.Lote = GetString("lote");
            oBeProducto_pallet.Impreso = GetBool("Impreso");
            oBeProducto_pallet.Reimpresiones = GetInt("Reimpresiones");
            oBeProducto_pallet.Fecha_vence = GetDate("fecha_vence");
            oBeProducto_pallet.Fecha_ingreso = GetDate("fecha_ingreso");
            oBeProducto_pallet.User_agr = GetString("user_agr");
            oBeProducto_pallet.Fec_agr = GetDate("fec_agr");
            oBeProducto_pallet.User_mod = GetString("user_mod");
            oBeProducto_pallet.Fec_mod = GetDate("fec_mod");
            oBeProducto_pallet.Activo = GetBool("activo");
            oBeProducto_pallet.IdRecepcionDet = GetInt("IdRecepcionDet");
            oBeProducto_pallet.Codigo_producto = GetString("codigo_producto");
        }
        catch (Exception)
        {            
            throw;
        }
    }

    public static int Insertar(clsBeProducto_pallet oBeProducto_pallet, SqlConnection pConection, SqlTransaction pTransaction)
    {
        Ins.Init("producto_pallet");
        Ins.Add("idpallet", "@idpallet", "F");
        Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
        Ins.Add("idproductobodega", "@idproductobodega", "F");
        Ins.Add("idpresentacion", "@idpresentacion", "F");
        Ins.Add("idoperadorbodega", "@idoperadorbodega", "F");
        Ins.Add("idimpresora", "@idimpresora", "F");
        Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
        Ins.Add("codigo_barra", "@codigo_barra", "F");
        Ins.Add("cantidad", "@cantidad", "F");
        Ins.Add("lote", "@lote", "F");
        Ins.Add("impreso", "@impreso", "F");
        Ins.Add("reimpresiones", "@reimpresiones", "F");
        Ins.Add("fecha_vence", "@fecha_vence", "F");
        Ins.Add("fecha_ingreso", "@fecha_ingreso", "F");
        Ins.Add("user_agr", "@user_agr", "F");
        Ins.Add("fec_agr", "@fec_agr", "F");
        Ins.Add("user_mod", "@user_mod", "F");
        Ins.Add("fec_mod", "@fec_mod", "F");
        Ins.Add("activo", "@activo", "F");
        Ins.Add("idrecepciondet", "@idrecepciondet", "F");
        Ins.Add("codigo_producto", "@codigo_producto", "F");

        string sp = Ins.SQL();

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@IdPallet", oBeProducto_pallet.IdPallet);
            cmd.Parameters.AddWithValue("@IdPropietarioBodega", oBeProducto_pallet.IdPropietarioBodega);
            cmd.Parameters.AddWithValue("@IdProductoBodega", oBeProducto_pallet.IdProductoBodega);
            cmd.Parameters.AddWithValue("@IdPresentacion", oBeProducto_pallet.IdPresentacion);
            cmd.Parameters.AddWithValue("@IdOperadorBodega", oBeProducto_pallet.IdOperadorBodega);
            cmd.Parameters.AddWithValue("@IdImpresora", oBeProducto_pallet.IdImpresora);
            cmd.Parameters.AddWithValue("@IdRecepcionEnc", oBeProducto_pallet.IdRecepcionEnc);
            cmd.Parameters.AddWithValue("@codigo_barra", oBeProducto_pallet.Codigo_barra);
            cmd.Parameters.AddWithValue("@cantidad", oBeProducto_pallet.Cantidad);
            cmd.Parameters.AddWithValue("@lote", oBeProducto_pallet.Lote);
            cmd.Parameters.AddWithValue("@Impreso", oBeProducto_pallet.Impreso);
            cmd.Parameters.AddWithValue("@Reimpresiones", oBeProducto_pallet.Reimpresiones);
            cmd.Parameters.AddWithValue("@fecha_vence", oBeProducto_pallet.Fecha_vence);
            cmd.Parameters.AddWithValue("@fecha_ingreso", oBeProducto_pallet.Fecha_ingreso);
            cmd.Parameters.AddWithValue("@user_agr", oBeProducto_pallet.User_agr);
            cmd.Parameters.AddWithValue("@fec_agr", oBeProducto_pallet.Fec_agr);
            cmd.Parameters.AddWithValue("@user_mod", oBeProducto_pallet.User_mod);
            cmd.Parameters.AddWithValue("@fec_mod", oBeProducto_pallet.Fec_mod);
            cmd.Parameters.AddWithValue("@activo", oBeProducto_pallet.Activo);
            cmd.Parameters.AddWithValue("@IdRecepcionDet", oBeProducto_pallet.IdRecepcionDet);
            cmd.Parameters.AddWithValue("@codigo_producto", oBeProducto_pallet.Codigo_producto);

            return cmd.ExecuteNonQuery();
        }
    }

    public static int Insertar(IConfiguration config, clsBeProducto_pallet oBeProducto_pallet)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("producto_pallet");
            Ins.Add("idpallet", "@idpallet", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idoperadorbodega", "@idoperadorbodega", "F");
            Ins.Add("idimpresora", "@idimpresora", "F");
            Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Ins.Add("codigo_barra", "@codigo_barra", "F");
            Ins.Add("cantidad", "@cantidad", "F");
            Ins.Add("lote", "@lote", "F");
            Ins.Add("impreso", "@impreso", "F");
            Ins.Add("reimpresiones", "@reimpresiones", "F");
            Ins.Add("fecha_vence", "@fecha_vence", "F");
            Ins.Add("fecha_ingreso", "@fecha_ingreso", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("idrecepciondet", "@idrecepciondet", "F");
            Ins.Add("codigo_producto", "@codigo_producto", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@IdPallet", oBeProducto_pallet.IdPallet));
            cmd.Parameters.Add(new SqlParameter("@IdPropietarioBodega", oBeProducto_pallet.IdPropietarioBodega));
            cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", oBeProducto_pallet.IdProductoBodega));
            cmd.Parameters.Add(new SqlParameter("@IdPresentacion", oBeProducto_pallet.IdPresentacion));
            cmd.Parameters.Add(new SqlParameter("@IdOperadorBodega", oBeProducto_pallet.IdOperadorBodega));
            cmd.Parameters.Add(new SqlParameter("@IdImpresora", oBeProducto_pallet.IdImpresora));
            cmd.Parameters.Add(new SqlParameter("@IdRecepcionEnc", oBeProducto_pallet.IdRecepcionEnc));
            cmd.Parameters.Add(new SqlParameter("@codigo_barra", oBeProducto_pallet.Codigo_barra));
            cmd.Parameters.Add(new SqlParameter("@cantidad", oBeProducto_pallet.Cantidad));
            cmd.Parameters.Add(new SqlParameter("@lote", oBeProducto_pallet.Lote));
            cmd.Parameters.Add(new SqlParameter("@Impreso", oBeProducto_pallet.Impreso));
            cmd.Parameters.Add(new SqlParameter("@Reimpresiones", oBeProducto_pallet.Reimpresiones));
            cmd.Parameters.Add(new SqlParameter("@fecha_vence", oBeProducto_pallet.Fecha_vence));
            cmd.Parameters.Add(new SqlParameter("@fecha_ingreso", oBeProducto_pallet.Fecha_ingreso));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeProducto_pallet.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeProducto_pallet.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeProducto_pallet.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeProducto_pallet.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeProducto_pallet.Activo));
            cmd.Parameters.Add(new SqlParameter("@IdRecepcionDet", oBeProducto_pallet.IdRecepcionDet));
            cmd.Parameters.Add(new SqlParameter("@codigo_producto", oBeProducto_pallet.Codigo_producto));

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

    public static int Actualizar(clsBeProducto_pallet oBeProducto_pallet, SqlConnection pConection, SqlTransaction pTransaction)
    {
        Upd.Init("producto_pallet");
        Upd.Add("idpallet", "@idpallet", "F");
        Upd.Add("idpropietariobodega", "@idpropietariobodega", "F");
        Upd.Add("idproductobodega", "@idproductobodega", "F");
        Upd.Add("idpresentacion", "@idpresentacion", "F");
        Upd.Add("idoperadorbodega", "@idoperadorbodega", "F");
        Upd.Add("idimpresora", "@idimpresora", "F");
        Upd.Add("idrecepcionenc", "@idrecepcionenc", "F");
        Upd.Add("codigo_barra", "@codigo_barra", "F");
        Upd.Add("cantidad", "@cantidad", "F");
        Upd.Add("lote", "@lote", "F");
        Upd.Add("impreso", "@impreso", "F");
        Upd.Add("reimpresiones", "@reimpresiones", "F");
        Upd.Add("fecha_vence", "@fecha_vence", "F");
        Upd.Add("fecha_ingreso", "@fecha_ingreso", "F");
        Upd.Add("user_agr", "@user_agr", "F");
        Upd.Add("fec_agr", "@fec_agr", "F");
        Upd.Add("user_mod", "@user_mod", "F");
        Upd.Add("fec_mod", "@fec_mod", "F");
        Upd.Add("activo", "@activo", "F");
        Upd.Add("idrecepciondet", "@idrecepciondet", "F");
        Upd.Add("codigo_producto", "@codigo_producto", "F");
        Upd.Where("IdPallet = @IdPallet");

        string sp = Upd.SQL();

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@IdPallet", oBeProducto_pallet.IdPallet);
            cmd.Parameters.AddWithValue("@IdPropietarioBodega", oBeProducto_pallet.IdPropietarioBodega);
            cmd.Parameters.AddWithValue("@IdProductoBodega", oBeProducto_pallet.IdProductoBodega);
            cmd.Parameters.AddWithValue("@IdPresentacion", oBeProducto_pallet.IdPresentacion);
            cmd.Parameters.AddWithValue("@IdOperadorBodega", oBeProducto_pallet.IdOperadorBodega);
            cmd.Parameters.AddWithValue("@IdImpresora", oBeProducto_pallet.IdImpresora);
            cmd.Parameters.AddWithValue("@IdRecepcionEnc", oBeProducto_pallet.IdRecepcionEnc);
            cmd.Parameters.AddWithValue("@codigo_barra", oBeProducto_pallet.Codigo_barra);
            cmd.Parameters.AddWithValue("@cantidad", oBeProducto_pallet.Cantidad);
            cmd.Parameters.AddWithValue("@lote", oBeProducto_pallet.Lote);
            cmd.Parameters.AddWithValue("@Impreso", oBeProducto_pallet.Impreso);
            cmd.Parameters.AddWithValue("@Reimpresiones", oBeProducto_pallet.Reimpresiones);
            cmd.Parameters.AddWithValue("@fecha_vence", oBeProducto_pallet.Fecha_vence);
            cmd.Parameters.AddWithValue("@fecha_ingreso", oBeProducto_pallet.Fecha_ingreso);
            cmd.Parameters.AddWithValue("@user_agr", oBeProducto_pallet.User_agr);
            cmd.Parameters.AddWithValue("@fec_agr", oBeProducto_pallet.Fec_agr);
            cmd.Parameters.AddWithValue("@user_mod", oBeProducto_pallet.User_mod);
            cmd.Parameters.AddWithValue("@fec_mod", oBeProducto_pallet.Fec_mod);
            cmd.Parameters.AddWithValue("@activo", oBeProducto_pallet.Activo);
            cmd.Parameters.AddWithValue("@IdRecepcionDet", oBeProducto_pallet.IdRecepcionDet);
            cmd.Parameters.AddWithValue("@codigo_producto", oBeProducto_pallet.Codigo_producto);

            return cmd.ExecuteNonQuery();
        }
    }

    public int Eliminar(IConfiguration config, clsBeProducto_pallet oBeProducto_pallet, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Producto_pallet" +
             "  Where(IdPallet = @IdPallet)");

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

            cmd.Parameters.Add(new SqlParameter("@IdPallet", oBeProducto_pallet.IdPallet));

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

    public static bool GetSingle(IConfiguration config, ref clsBeProducto_pallet pBeProducto_pallet)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Producto_pallet" +
            " Where(IdPallet = @IdPallet)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPallet", pBeProducto_pallet.IdPallet));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPropietarioBodega", pBeProducto_pallet.IdPropietarioBodega));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdProductoBodega", pBeProducto_pallet.IdProductoBodega));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPresentacion", pBeProducto_pallet.IdPresentacion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdOperadorBodega", pBeProducto_pallet.IdOperadorBodega));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdImpresora", pBeProducto_pallet.IdImpresora));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdRecepcionEnc", pBeProducto_pallet.IdRecepcionEnc));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@codigo_barra", pBeProducto_pallet.Codigo_barra));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@cantidad", pBeProducto_pallet.Cantidad));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@lote", pBeProducto_pallet.Lote));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Impreso", pBeProducto_pallet.Impreso));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Reimpresiones", pBeProducto_pallet.Reimpresiones));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fecha_vence", pBeProducto_pallet.Fecha_vence));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fecha_ingreso", pBeProducto_pallet.Fecha_ingreso));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@user_agr", pBeProducto_pallet.User_agr));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_agr", pBeProducto_pallet.Fec_agr));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@user_mod", pBeProducto_pallet.User_mod));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_mod", pBeProducto_pallet.Fec_mod));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@activo", pBeProducto_pallet.Activo));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdRecepcionDet", pBeProducto_pallet.IdRecepcionDet));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@codigo_producto", pBeProducto_pallet.Codigo_producto));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeProducto_pallet, r);
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

    public static List<clsBeProducto_pallet> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeProducto_pallet> lreturnList = new List<clsBeProducto_pallet>();

        try
        {
            const string sp = "Select * FROM Producto_pallet";

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

                        clsBeProducto_pallet vBeProducto_pallet = new clsBeProducto_pallet();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeProducto_pallet = new clsBeProducto_pallet();
                            Cargar(ref vBeProducto_pallet, dr);
                            lreturnList.Add(vBeProducto_pallet);
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

            const string sp = "Select ISNULL(Max(IdPallet),0) FROM Producto_pallet";

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
        const string sp = "Select ISNULL(Max(IdPallet),0) FROM Producto_pallet";

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

    public static int Guarda_Producto_Pallet(int IdRecepcionEnc,
                                            List<clsBeProducto_pallet> ?pListProductoPallet,
                                            SqlConnection lConnection,
                                            SqlTransaction lTransaction)
    {
        int Guarda_Producto_Pallet = 0;
        int vRegistros = 0;

        try
        {
            if (pListProductoPallet != null && pListProductoPallet.Count > 0)
            {
                int lMaxIdProdPallet = MaxID(lConnection, lTransaction) + 1;

                foreach (clsBeProducto_pallet BeProdPallet in pListProductoPallet)
                {
                    if (BeProdPallet.IsNew)
                    {
                        BeProdPallet.IdPallet = lMaxIdProdPallet;
                        BeProdPallet.IdRecepcionEnc = IdRecepcionEnc;
                        BeProdPallet.Fecha_ingreso = DateTime.Now;
                        BeProdPallet.Fec_agr = DateTime.Now;
                        BeProdPallet.Fec_mod = DateTime.Now;
                        vRegistros += Insertar(BeProdPallet, lConnection, lTransaction);
                        lMaxIdProdPallet += 1;
                    }
                    else
                    {
                        BeProdPallet.Fec_mod = DateTime.Now;
                        vRegistros += Actualizar(BeProdPallet, lConnection, lTransaction);
                    }
                }
            }

            Guarda_Producto_Pallet = vRegistros;
        }
        catch (Exception)
        {
            throw;
        }

        return Guarda_Producto_Pallet;
    }
}
