using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using WMSWebAPI.Be;
public class clsLnBodega_area
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeBodega_area oBeBodega_area, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }        
        double GetDouble(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeBodega_area.IdArea = GetInt("IdArea");
            oBeBodega_area.IdBodega = GetInt("IdBodega");
            oBeBodega_area.Descripcion = GetString("Descripcion");
            oBeBodega_area.Sistema = GetBool("sistema");
            oBeBodega_area.User_agr = GetString("user_agr");
            oBeBodega_area.Fec_agr = GetDate("fec_agr");
            oBeBodega_area.User_mod = GetString("user_mod");
            oBeBodega_area.Fec_mod = GetDate("fec_mod");
            oBeBodega_area.Codigo = GetString("Codigo");
            oBeBodega_area.Activo = GetBool("activo");
            oBeBodega_area.Alto = GetDouble("alto");
            oBeBodega_area.Largo = GetDouble("largo");
            oBeBodega_area.Ancho = GetDouble("ancho");
            oBeBodega_area.Margen_izquierdo = GetDouble("margen_izquierdo");
            oBeBodega_area.Margen_derecho = GetDouble("margen_derecho");
            oBeBodega_area.Margen_superior = GetDouble("margen_superior");
            oBeBodega_area.Margen_inferior = GetDouble("margen_inferior");
            oBeBodega_area.Grupo = GetString("grupo");
            oBeBodega_area.IdUbicacionRef = GetInt("IdUbicacionRef");
        }
        catch (Exception ex)
        {
            var st = new System.Diagnostics.StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{{0}} {{1}}", currentMethodName, ex.Message);            
            throw new Exception(vMsgError);
        }
    }

    public static int Insertar(IConfiguration config, clsBeBodega_area oBeBodega_area, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("bodega_area");
            Ins.Add("idarea", "@idarea", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("descripcion", "@descripcion", "F");
            Ins.Add("sistema", "@sistema", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("alto", "@alto", "F");
            Ins.Add("largo", "@largo", "F");
            Ins.Add("ancho", "@ancho", "F");
            Ins.Add("margen_izquierdo", "@margen_izquierdo", "F");
            Ins.Add("margen_derecho", "@margen_derecho", "F");
            Ins.Add("margen_superior", "@margen_superior", "F");
            Ins.Add("margen_inferior", "@margen_inferior", "F");
            Ins.Add("grupo", "@grupo", "F");
            Ins.Add("idubicacionref", "@idubicacionref", "F");

            string sp = Ins.SQL();

            var cmd = new SqlCommand(sp, lConnection) { CommandType = (CommandType)Conversions.ToInteger(CommandType.Text) };

            bool Es_Transaccion_Remota = (pConection != null && pTransaction != null);

            if (Es_Transaccion_Remota)
            {
                cmd = new SqlCommand(sp, pConection, pTransaction);
            }
            else
            {
                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                cmd = new SqlCommand(sp, lConnection, lTransaction);
            }

            cmd.Parameters.Add(new SqlParameter("@IdArea", oBeBodega_area.IdArea));
            cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeBodega_area.IdBodega));
            cmd.Parameters.Add(new SqlParameter("@Descripcion", oBeBodega_area.Descripcion));
            cmd.Parameters.Add(new SqlParameter("@sistema", oBeBodega_area.Sistema));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeBodega_area.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeBodega_area.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeBodega_area.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeBodega_area.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@Codigo", oBeBodega_area.Codigo));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeBodega_area.Activo));
            cmd.Parameters.Add(new SqlParameter("@alto", oBeBodega_area.Alto));
            cmd.Parameters.Add(new SqlParameter("@largo", oBeBodega_area.Largo));
            cmd.Parameters.Add(new SqlParameter("@ancho", oBeBodega_area.Ancho));
            cmd.Parameters.Add(new SqlParameter("@margen_izquierdo", oBeBodega_area.Margen_izquierdo));
            cmd.Parameters.Add(new SqlParameter("@margen_derecho", oBeBodega_area.Margen_derecho));
            cmd.Parameters.Add(new SqlParameter("@margen_superior", oBeBodega_area.Margen_superior));
            cmd.Parameters.Add(new SqlParameter("@margen_inferior", oBeBodega_area.Margen_inferior));
            cmd.Parameters.Add(new SqlParameter("@grupo", oBeBodega_area.Grupo));
            cmd.Parameters.Add(new SqlParameter("@IdUbicacionRef", oBeBodega_area.IdUbicacionRef));

            rowsAffected = cmd.ExecuteNonQuery();

            cmd.Dispose();

            if (!Es_Transaccion_Remota)
                if (lTransaction != null)
                    lTransaction.Commit();


        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
            
            throw new Exception(vMsgError);
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection is not null) lConnection.Dispose();
            if (lTransaction is not null) lTransaction.Dispose();
        }
        return rowsAffected;
    }

    public static int Insertar(IConfiguration config, clsBeBodega_area oBeBodega_area)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("bodega_area");
            Ins.Add("idarea", "@idarea", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("descripcion", "@descripcion", "F");
            Ins.Add("sistema", "@sistema", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("alto", "@alto", "F");
            Ins.Add("largo", "@largo", "F");
            Ins.Add("ancho", "@ancho", "F");
            Ins.Add("margen_izquierdo", "@margen_izquierdo", "F");
            Ins.Add("margen_derecho", "@margen_derecho", "F");
            Ins.Add("margen_superior", "@margen_superior", "F");
            Ins.Add("margen_inferior", "@margen_inferior", "F");
            Ins.Add("grupo", "@grupo", "F");
            Ins.Add("idubicacionref", "@idubicacionref", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@IdArea", oBeBodega_area.IdArea));
            cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeBodega_area.IdBodega));
            cmd.Parameters.Add(new SqlParameter("@Descripcion", oBeBodega_area.Descripcion));
            cmd.Parameters.Add(new SqlParameter("@sistema", oBeBodega_area.Sistema));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeBodega_area.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeBodega_area.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeBodega_area.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeBodega_area.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@Codigo", oBeBodega_area.Codigo));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeBodega_area.Activo));
            cmd.Parameters.Add(new SqlParameter("@alto", oBeBodega_area.Alto));
            cmd.Parameters.Add(new SqlParameter("@largo", oBeBodega_area.Largo));
            cmd.Parameters.Add(new SqlParameter("@ancho", oBeBodega_area.Ancho));
            cmd.Parameters.Add(new SqlParameter("@margen_izquierdo", oBeBodega_area.Margen_izquierdo));
            cmd.Parameters.Add(new SqlParameter("@margen_derecho", oBeBodega_area.Margen_derecho));
            cmd.Parameters.Add(new SqlParameter("@margen_superior", oBeBodega_area.Margen_superior));
            cmd.Parameters.Add(new SqlParameter("@margen_inferior", oBeBodega_area.Margen_inferior));
            cmd.Parameters.Add(new SqlParameter("@grupo", oBeBodega_area.Grupo));
            cmd.Parameters.Add(new SqlParameter("@IdUbicacionRef", oBeBodega_area.IdUbicacionRef));

            rowsAffected = cmd.ExecuteNonQuery();

            if (lTransaction != null)
                lTransaction.Commit();

        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
            
            throw new Exception(vMsgError);
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
        return rowsAffected;
    }

    public static int Actualizar(IConfiguration config, clsBeBodega_area oBeBodega_area, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("bodega_area");
            Upd.Add("idarea", "@idarea", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("descripcion", "@descripcion", "F");
            Upd.Add("sistema", "@sistema", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("codigo", "@codigo", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("alto", "@alto", "F");
            Upd.Add("largo", "@largo", "F");
            Upd.Add("ancho", "@ancho", "F");
            Upd.Add("margen_izquierdo", "@margen_izquierdo", "F");
            Upd.Add("margen_derecho", "@margen_derecho", "F");
            Upd.Add("margen_superior", "@margen_superior", "F");
            Upd.Add("margen_inferior", "@margen_inferior", "F");
            Upd.Add("grupo", "@grupo", "F");
            Upd.Add("idubicacionref", "@idubicacionref", "F");
            Upd.Where("IdArea = @IdArea" +
                " AND IdBodega = @IdBodega");

            string sp = Upd.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            bool Es_Transaccion_Remota = (pConection != null && pTransaction != null);

            if (Es_Transaccion_Remota)
            {
                cmd = new SqlCommand(sp, pConection, pTransaction);
            }
            else
            {
                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                cmd = new SqlCommand(sp, lConnection, lTransaction);
            }

            cmd.Parameters.Add(new SqlParameter("@IdArea", oBeBodega_area.IdArea));
            cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeBodega_area.IdBodega));
            cmd.Parameters.Add(new SqlParameter("@Descripcion", oBeBodega_area.Descripcion));
            cmd.Parameters.Add(new SqlParameter("@sistema", oBeBodega_area.Sistema));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeBodega_area.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeBodega_area.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeBodega_area.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeBodega_area.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@Codigo", oBeBodega_area.Codigo));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeBodega_area.Activo));
            cmd.Parameters.Add(new SqlParameter("@alto", oBeBodega_area.Alto));
            cmd.Parameters.Add(new SqlParameter("@largo", oBeBodega_area.Largo));
            cmd.Parameters.Add(new SqlParameter("@ancho", oBeBodega_area.Ancho));
            cmd.Parameters.Add(new SqlParameter("@margen_izquierdo", oBeBodega_area.Margen_izquierdo));
            cmd.Parameters.Add(new SqlParameter("@margen_derecho", oBeBodega_area.Margen_derecho));
            cmd.Parameters.Add(new SqlParameter("@margen_superior", oBeBodega_area.Margen_superior));
            cmd.Parameters.Add(new SqlParameter("@margen_inferior", oBeBodega_area.Margen_inferior));
            cmd.Parameters.Add(new SqlParameter("@grupo", oBeBodega_area.Grupo));
            cmd.Parameters.Add(new SqlParameter("@IdUbicacionRef", oBeBodega_area.IdUbicacionRef));

            rowsAffected = cmd.ExecuteNonQuery();

            if (!Es_Transaccion_Remota)
                if (lTransaction != null)
                    lTransaction.Commit();


        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
            
            throw new Exception(vMsgError);
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
        return rowsAffected;
    }

    public int Eliminar(IConfiguration config, clsBeBodega_area oBeBodega_area, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Bodega_area" +
             "  Where(IdArea = @IdArea)" +
             "  And (IdBodega = @IdBodega)");

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

            cmd.Parameters.Add(new SqlParameter("@IdArea", oBeBodega_area.IdArea));

            int rowsAffected = cmd.ExecuteNonQuery();

            if (!Es_Transaccion_Remota)
                if (lTransaction != null)
                    lTransaction.Commit();

            return rowsAffected;

        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
            
            throw new Exception(vMsgError);
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
    }

    public DataTable Listar(IConfiguration config)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = "Select * FROM Bodega_area";
            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            return dt;

        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
            
            throw new Exception(vMsgError);
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
    }

    public static bool GetSingle(IConfiguration config, ref clsBeBodega_area pBeBodega_area)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Bodega_area" +
            " Where(IdArea = @IdArea) " +
            " And (IdBodega = @IdBodega) ";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdArea", pBeBodega_area.IdArea));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdBodega", pBeBodega_area.IdBodega));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Descripcion", pBeBodega_area.Descripcion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@sistema", pBeBodega_area.Sistema));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@user_agr", pBeBodega_area.User_agr));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_agr", pBeBodega_area.Fec_agr));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@user_mod", pBeBodega_area.User_mod));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_mod", pBeBodega_area.Fec_mod));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Codigo", pBeBodega_area.Codigo));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@activo", pBeBodega_area.Activo));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@alto", pBeBodega_area.Alto));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@largo", pBeBodega_area.Largo));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@ancho", pBeBodega_area.Ancho));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@margen_izquierdo", pBeBodega_area.Margen_izquierdo));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@margen_derecho", pBeBodega_area.Margen_derecho));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@margen_superior", pBeBodega_area.Margen_superior));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@margen_inferior", pBeBodega_area.Margen_inferior));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@grupo", pBeBodega_area.Grupo));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdUbicacionRef", pBeBodega_area.IdUbicacionRef));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeBodega_area, r);
                return true;
            }

        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
            
            throw new Exception(vMsgError);
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
        return false;

    }

    public static List<clsBeBodega_area> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeBodega_area> lreturnList = new List<clsBeBodega_area>();

        try
        {
            const string sp = "Select * FROM Bodega_area";

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

                        clsBeBodega_area vBeBodega_area = new clsBeBodega_area();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeBodega_area = new clsBeBodega_area();
                            Cargar(ref vBeBodega_area, dr);
                            lreturnList.Add(vBeBodega_area);
                        }

                        lTransaction.Commit();
                    }

                    lConnection.Close();

                }

            }

            return lreturnList;

        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
            
            throw new Exception(vMsgError);
        }
    }

    public static int MaxID(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;

        try
        {

            int lMax = 0;

            const string sp = "Select ISNULL(Max(IdArea),0) FROM Bodega_area";

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
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
            
            throw new Exception(vMsgError);
        }
    }
    public static int MaxID(IConfiguration config, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;
        int lMax = 0;
        try
        {


            const string sp = "Select ISNULL(Max(IdArea),0) FROM Bodega_area";

            bool Es_Transaccion_Remota = pConection is not null && pTransaction is not null;
            var cmd = new SqlCommand(sp, lConnection) { CommandType = (CommandType)Conversions.ToInteger(CommandType.Text) };
            if (Es_Transaccion_Remota)
            {
                cmd = new SqlCommand(sp, pConection, pTransaction);
            }
            else
            {
                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                cmd = new SqlCommand(sp, lConnection, lTransaction);
            }

            var lreturnValue = cmd.ExecuteScalar();

            if (lreturnValue != DBNull.Value && lreturnValue != null)
            {
                lMax = int.Parse((string)lreturnValue);
            }

            if (!Es_Transaccion_Remota)
                if (lTransaction != null)
                    lTransaction.Commit();

            return lMax;

        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);            
            throw new Exception(vMsgError);
        }
    }
    public static int Get_IdUbicacionVirtual_By_Codigo(string Codigo_Bodega,
                                                       SqlConnection lConnection,
                                                       SqlTransaction lTransaction)
    {
        int IdUbicacionVirtual = 0;

        try
        {
            const string sp = @"SELECT IdUbicacionVirtual FROM Cliente 
                                WHERE (Codigo = @Codigo)";

            using (var cmd = new SqlCommand(sp, lConnection, lTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@Codigo", Codigo_Bodega));

                using (var dad = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        IdUbicacionVirtual = dt.Rows[0]["IdUbicacionVirtual"] == DBNull.Value
                            ? 0
                            : Convert.ToInt32(dt.Rows[0]["IdUbicacionVirtual"]);
                    }
                }
            }
        }        
        catch (Exception ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex.Message);
        }

        return IdUbicacionVirtual;
    }

    public static clsBeBodega_area Get_Single_By_Codigo_Bodega(string CodigoBodega,
                                                                SqlConnection lConnection,
                                                                SqlTransaction lTransaction)
    {
        clsBeBodega_area resultado = new clsBeBodega_area();

        try
        {
            const string sp = @"SELECT * FROM Bodega_area 
                                WHERE Codigo = @Codigo_Bodega";

            using (var cmd = new SqlCommand(sp, lConnection, lTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@Codigo_Bodega", CodigoBodega));

                using (var dad = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        var pBeBodega = new clsBeBodega_area();
                        Cargar(ref pBeBodega, dt.Rows[0]);
                        resultado = pBeBodega;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex.Message);
        }

        return resultado;
    }
}