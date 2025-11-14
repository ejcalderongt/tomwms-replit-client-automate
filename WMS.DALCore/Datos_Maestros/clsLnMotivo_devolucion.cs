using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using WMSWebAPI.Be;

public class clsLnMotivo_devolucion
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeMotivo_devolucion oBeMotivo_devolucion, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }        

        try
        {
            oBeMotivo_devolucion.IdMotivoDevolucion = GetInt("IdMotivoDevolucion");
            oBeMotivo_devolucion.IdEmpresa = GetInt("IdEmpresa");
            oBeMotivo_devolucion.IdPropietario = GetInt("IdPropietario");
            oBeMotivo_devolucion.Nombre = GetString("Nombre");
            oBeMotivo_devolucion.User_agr = GetString("user_agr");
            oBeMotivo_devolucion.Fec_agr = GetDate("fec_agr");
            oBeMotivo_devolucion.User_mod = GetString("user_mod");
            oBeMotivo_devolucion.Fec_mod = GetDate("fec_mod");
            oBeMotivo_devolucion.Activo = GetBool("activo");
            oBeMotivo_devolucion.Es_detalle = GetBool("es_detalle");
        }
        catch (Exception ex)
        {            
            string vMsgError = ex.Message;
            throw new Exception(vMsgError);
        }
    }

    public static int Insertar(clsBeMotivo_devolucion oBeMotivo_devolucion, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeMotivo_devolucion == null)
            throw new ArgumentNullException(nameof(oBeMotivo_devolucion));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

        try
        {
            Ins.Init("motivo_devolucion");
            Ins.Add("idmotivodevolucion", "@idmotivodevolucion", "F");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("es_detalle", "@es_detalle", "F");

            string sp = Ins.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter("@IdMotivoDevolucion", oBeMotivo_devolucion.IdMotivoDevolucion));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", oBeMotivo_devolucion.IdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@IdPropietario", oBeMotivo_devolucion.IdPropietario));
                cmd.Parameters.Add(new SqlParameter("@Nombre", oBeMotivo_devolucion.Nombre));
                cmd.Parameters.Add(new SqlParameter("@user_agr", oBeMotivo_devolucion.User_agr));
                cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeMotivo_devolucion.Fec_agr));
                cmd.Parameters.Add(new SqlParameter("@user_mod", oBeMotivo_devolucion.User_mod));
                cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeMotivo_devolucion.Fec_mod));
                cmd.Parameters.Add(new SqlParameter("@activo", oBeMotivo_devolucion.Activo));
                cmd.Parameters.Add(new SqlParameter("@es_detalle", oBeMotivo_devolucion.Es_detalle));

                rowsAffected = cmd.ExecuteNonQuery();
            }

            return rowsAffected;
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static int Insertar(IConfiguration config, clsBeMotivo_devolucion oBeMotivo_devolucion)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("motivo_devolucion");
            Ins.Add("idmotivodevolucion", "@idmotivodevolucion", "F");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("es_detalle", "@es_detalle", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@IdMotivoDevolucion", oBeMotivo_devolucion.IdMotivoDevolucion));
            cmd.Parameters.Add(new SqlParameter("@IdEmpresa", oBeMotivo_devolucion.IdEmpresa));
            cmd.Parameters.Add(new SqlParameter("@IdPropietario", oBeMotivo_devolucion.IdPropietario));
            cmd.Parameters.Add(new SqlParameter("@Nombre", oBeMotivo_devolucion.Nombre));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeMotivo_devolucion.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeMotivo_devolucion.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeMotivo_devolucion.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeMotivo_devolucion.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeMotivo_devolucion.Activo));
            cmd.Parameters.Add(new SqlParameter("@es_detalle", oBeMotivo_devolucion.Es_detalle));

            rowsAffected = cmd.ExecuteNonQuery();

            if (lTransaction != null)
                lTransaction.Commit();

        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            throw new Exception(ex1.Message);
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
        return rowsAffected;
    }

    public static int Actualizar(clsBeMotivo_devolucion oBeMotivo_devolucion, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeMotivo_devolucion == null)
            throw new ArgumentNullException(nameof(oBeMotivo_devolucion));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

        try
        {
            Upd.Init("motivo_devolucion");
            Upd.Add("idmotivodevolucion", "@idmotivodevolucion", "F");
            Upd.Add("idempresa", "@idempresa", "F");
            Upd.Add("idpropietario", "@idpropietario", "F");
            Upd.Add("nombre", "@nombre", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("es_detalle", "@es_detalle", "F");
            Upd.Where("IdMotivoDevolucion = @IdMotivoDevolucion");

            string sp = Upd.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter("@IdMotivoDevolucion", oBeMotivo_devolucion.IdMotivoDevolucion));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", oBeMotivo_devolucion.IdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@IdPropietario", oBeMotivo_devolucion.IdPropietario));
                cmd.Parameters.Add(new SqlParameter("@Nombre", oBeMotivo_devolucion.Nombre));
                cmd.Parameters.Add(new SqlParameter("@user_agr", oBeMotivo_devolucion.User_agr));
                cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeMotivo_devolucion.Fec_agr));
                cmd.Parameters.Add(new SqlParameter("@user_mod", oBeMotivo_devolucion.User_mod));
                cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeMotivo_devolucion.Fec_mod));
                cmd.Parameters.Add(new SqlParameter("@activo", oBeMotivo_devolucion.Activo));
                cmd.Parameters.Add(new SqlParameter("@es_detalle", oBeMotivo_devolucion.Es_detalle));

                rowsAffected = cmd.ExecuteNonQuery();
            }

            return rowsAffected;
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int Eliminar(IConfiguration config, clsBeMotivo_devolucion oBeMotivo_devolucion, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Motivo_devolucion" +
             "  Where(IdMotivoDevolucion = @IdMotivoDevolucion)");

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

            cmd.Parameters.Add(new SqlParameter("@IdMotivoDevolucion", oBeMotivo_devolucion.IdMotivoDevolucion));

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
            throw new Exception(ex1.Message);
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
    }    

    public static bool GetSingle(IConfiguration config, ref clsBeMotivo_devolucion pBeMotivo_devolucion)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Motivo_devolucion" +
            " Where(IdMotivoDevolucion = @IdMotivoDevolucion)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdMotivoDevolucion", pBeMotivo_devolucion.IdMotivoDevolucion));            

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeMotivo_devolucion, r);
                return true;
            }

        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            throw new Exception(ex1.Message);
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
        return false;

    }

    public static List<clsBeMotivo_devolucion> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeMotivo_devolucion> lreturnList = new List<clsBeMotivo_devolucion>();

        try
        {
            const string sp = "Select * FROM Motivo_devolucion";

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

                        clsBeMotivo_devolucion vBeMotivo_devolucion = new clsBeMotivo_devolucion();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeMotivo_devolucion = new clsBeMotivo_devolucion();
                            Cargar(ref vBeMotivo_devolucion, dr);
                            lreturnList.Add(vBeMotivo_devolucion);
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
            throw new Exception(ex1.Message);
        }
    }

    public static int MaxID(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;

        try
        {

            int lMax = 0;

            const string sp = "Select ISNULL(Max(IdMotivoDevolucion),0) FROM Motivo_devolucion";

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
            throw new Exception(ex1.Message);
        }
    }
    public static int MaxID(IConfiguration config, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;
        int lMax = 0;
        try
        {


            const string sp = "Select ISNULL(Max(IdMotivoDevolucion),0) FROM Motivo_devolucion";

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
            throw new Exception(ex1.Message);
        }
    }

    public static bool Obtener(clsBeMotivo_devolucion oBeMotivo_devolucion,
                              SqlConnection lConnection,
                              SqlTransaction lTransaction)
    {
        try
        {
            const string sp = @"SELECT * FROM Motivo_devolucion 
                       WHERE IdMotivoDevolucion = @IdMotivoDevolucion";

            using (SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IDMOTIVODEVOLUCION", oBeMotivo_devolucion.IdMotivoDevolucion);

                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        Cargar(ref oBeMotivo_devolucion, dt.Rows[0]);
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
}
