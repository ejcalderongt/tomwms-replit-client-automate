using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using WMS.EntityCore.Operador;
using Microsoft.Extensions.Configuration;
public class clsLnOperador
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeOperador oBeOperador, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        byte[] GetBytes(string col) { return dr[col] is DBNull ? Array.Empty<byte>() : (byte[])dr[col]; }
        double getDouble(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeOperador.IdOperador = GetInt("IdOperador");
            oBeOperador.IdEmpresa = GetInt("IdEmpresa");
            oBeOperador.IdRolOperador = GetInt("IdRolOperador");
            oBeOperador.IdJornada = GetInt("IdJornada");
            oBeOperador.Nombres = GetString("nombres");
            oBeOperador.Apellidos = GetString("apellidos");
            oBeOperador.Direccion = GetString("direccion");
            oBeOperador.Telefono = GetString("telefono");
            oBeOperador.Codigo = GetString("codigo");
            oBeOperador.Clave = GetString("clave");
            oBeOperador.Activo = GetBool("activo");
            oBeOperador.User_agr = GetString("user_agr");
            oBeOperador.Fec_agr = GetDate("fec_agr");
            oBeOperador.User_mod = GetString("user_mod");
            oBeOperador.Fec_mod = GetDate("fec_mod");
            oBeOperador.Costo_hora = getDouble("costo_hora");
            oBeOperador.Usa_hh = GetBool("usa_hh");
            oBeOperador.Foto = GetBytes("foto");
            oBeOperador.Recibe = GetBool("recibe");
            oBeOperador.Ubica = GetBool("ubica");
            oBeOperador.Transporta = GetBool("transporta");
            oBeOperador.Pickea = GetBool("pickea");
            oBeOperador.Verifica = GetBool("verifica");
            oBeOperador.Montacarga = GetBool("montacarga");
            oBeOperador.Sistema = GetBool("sistema");
        }
        catch (Exception ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{{0}} {{1}}", currentMethodName, ex.Message);
            
            throw new Exception(vMsgError);
        }
    }
    public static int Insertar(clsBeOperador oBeOperador, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeOperador == null)
            throw new ArgumentNullException(nameof(oBeOperador));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

        try
        {
            Ins.Init("operador");
            Ins.Add("idoperador", "@idoperador", "F");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("idroloperador", "@idroloperador", "F");
            Ins.Add("idjornada", "@idjornada", "F");
            Ins.Add("nombres", "@nombres", "F");
            Ins.Add("apellidos", "@apellidos", "F");
            Ins.Add("direccion", "@direccion", "F");
            Ins.Add("telefono", "@telefono", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("clave", "@clave", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("costo_hora", "@costo_hora", "F");
            Ins.Add("usa_hh", "@usa_hh", "F");
            Ins.Add("foto", "@foto", "F");
            Ins.Add("recibe", "@recibe", "F");
            Ins.Add("ubica", "@ubica", "F");
            Ins.Add("transporta", "@transporta", "F");
            Ins.Add("pickea", "@pickea", "F");
            Ins.Add("verifica", "@verifica", "F");
            Ins.Add("montacarga", "@montacarga", "F");
            Ins.Add("sistema", "@sistema", "F");

            string sp = Ins.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                Bind(cmd, oBeOperador);

                rowsAffected = cmd.ExecuteNonQuery();
            }

            return rowsAffected;
        }
        catch (SqlException ex)
        {
            string errorMessage = $"Error en Insertar - {ex.Message}";
            throw new Exception(errorMessage, ex);
        }
    }
    public static int Insertar(IConfiguration config, clsBeOperador oBeOperador)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("operador");
            Ins.Add("idoperador", "@idoperador", "F");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("idroloperador", "@idroloperador", "F");
            Ins.Add("idjornada", "@idjornada", "F");
            Ins.Add("nombres", "@nombres", "F");
            Ins.Add("apellidos", "@apellidos", "F");
            Ins.Add("direccion", "@direccion", "F");
            Ins.Add("telefono", "@telefono", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("clave", "@clave", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("costo_hora", "@costo_hora", "F");
            Ins.Add("usa_hh", "@usa_hh", "F");
            Ins.Add("foto", "@foto", "F");
            Ins.Add("recibe", "@recibe", "F");
            Ins.Add("ubica", "@ubica", "F");
            Ins.Add("transporta", "@transporta", "F");
            Ins.Add("pickea", "@pickea", "F");
            Ins.Add("verifica", "@verifica", "F");
            Ins.Add("montacarga", "@montacarga", "F");
            Ins.Add("sistema", "@sistema", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeOperador);

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
    public static int Actualizar(clsBeOperador oBeOperador, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (oBeOperador == null)
            throw new ArgumentNullException(nameof(oBeOperador));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

        try
        {
            Upd.Init("operador");
            Upd.Add("idoperador", "@idoperador", "F");
            Upd.Add("idempresa", "@idempresa", "F");
            Upd.Add("idroloperador", "@idroloperador", "F");
            Upd.Add("idjornada", "@idjornada", "F");
            Upd.Add("nombres", "@nombres", "F");
            Upd.Add("apellidos", "@apellidos", "F");
            Upd.Add("direccion", "@direccion", "F");
            Upd.Add("telefono", "@telefono", "F");
            Upd.Add("codigo", "@codigo", "F");
            Upd.Add("clave", "@clave", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("costo_hora", "@costo_hora", "F");
            Upd.Add("usa_hh", "@usa_hh", "F");
            Upd.Add("foto", "@foto", "F");
            Upd.Add("recibe", "@recibe", "F");
            Upd.Add("ubica", "@ubica", "F");
            Upd.Add("transporta", "@transporta", "F");
            Upd.Add("pickea", "@pickea", "F");
            Upd.Add("verifica", "@verifica", "F");
            Upd.Add("montacarga", "@montacarga", "F");
            Upd.Add("sistema", "@sistema", "F");
            Upd.Where("IdOperador = @IdOperador");

            string sp = Upd.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                Bind(cmd, oBeOperador);

                rowsAffected = cmd.ExecuteNonQuery();
            }

            return rowsAffected;
        }
        catch (SqlException ex)
        {
            string errorMessage = $"Error en Actualizar - {ex.Message}";
            throw new Exception(errorMessage, ex);
        }
    }
    public int Eliminar(IConfiguration config, clsBeOperador oBeOperador, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Operador" +
             "  Where(IdOperador = @IdOperador)");

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

            cmd.Parameters.Add(new SqlParameter("@IdOperador", oBeOperador.IdOperador));

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
            const string sp = "Select * FROM Operador";
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
    public static bool GetSingle(IConfiguration config, ref clsBeOperador pBeOperador)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Operador" +
            " Where(IdOperador = @IdOperador)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdOperador", pBeOperador.IdOperador));
            
            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeOperador, r);
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
    public static List<clsBeOperador> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeOperador> lreturnList = new List<clsBeOperador>();

        try
        {
            const string sp = "Select * FROM Operador";

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

                        clsBeOperador vBeOperador = new clsBeOperador();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeOperador = new clsBeOperador();
                            Cargar(ref vBeOperador, dr);
                            lreturnList.Add(vBeOperador);
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

            const string sp = "Select ISNULL(Max(IdOperador),0) FROM Operador";

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


            const string sp = "Select ISNULL(Max(IdOperador),0) FROM Operador";

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

            Object lreturnValue = cmd.ExecuteScalar();

            if (lreturnValue != DBNull.Value && lreturnValue != null)
            {
                lMax = int.Parse((String)lreturnValue);
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
    public static void Bind(SqlCommand cmd, clsBeOperador oBeOperador)
    {
        cmd.Parameters.Add(new SqlParameter("@IdOperador", oBeOperador.IdOperador == 0 ? DBNull.Value : oBeOperador.IdOperador));
        cmd.Parameters.Add(new SqlParameter("@IdEmpresa", oBeOperador.IdEmpresa == 0 ? DBNull.Value : oBeOperador.IdEmpresa));
        cmd.Parameters.Add(new SqlParameter("@IdRolOperador", oBeOperador.IdRolOperador == 0 ? DBNull.Value : oBeOperador.IdRolOperador));
        cmd.Parameters.Add(new SqlParameter("@IdJornada", oBeOperador.IdJornada == 0 ? DBNull.Value : oBeOperador.IdJornada));
        cmd.Parameters.Add(new SqlParameter("@nombres", oBeOperador.Nombres));
        cmd.Parameters.Add(new SqlParameter("@apellidos", oBeOperador.Apellidos));
        cmd.Parameters.Add(new SqlParameter("@direccion", oBeOperador.Direccion));
        cmd.Parameters.Add(new SqlParameter("@telefono", oBeOperador.Telefono));
        cmd.Parameters.Add(new SqlParameter("@codigo", oBeOperador.Codigo));
        cmd.Parameters.Add(new SqlParameter("@clave", oBeOperador.Clave));
        cmd.Parameters.Add(new SqlParameter("@activo", oBeOperador.Activo));
        cmd.Parameters.Add(new SqlParameter("@user_agr", oBeOperador.User_agr));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeOperador.Fec_agr));
        cmd.Parameters.Add(new SqlParameter("@user_mod", oBeOperador.User_mod));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeOperador.Fec_mod));
        cmd.Parameters.Add(new SqlParameter("@costo_hora", oBeOperador.Costo_hora));
        cmd.Parameters.Add(new SqlParameter("@usa_hh", oBeOperador.Usa_hh));
        cmd.Parameters.Add(new SqlParameter("@foto", oBeOperador.Foto ?? Array.Empty<byte>()));
        cmd.Parameters.Add(new SqlParameter("@recibe", oBeOperador.Recibe));
        cmd.Parameters.Add(new SqlParameter("@ubica", oBeOperador.Ubica));
        cmd.Parameters.Add(new SqlParameter("@transporta", oBeOperador.Transporta));
        cmd.Parameters.Add(new SqlParameter("@pickea", oBeOperador.Pickea));
        cmd.Parameters.Add(new SqlParameter("@verifica", oBeOperador.Verifica));
        cmd.Parameters.Add(new SqlParameter("@montacarga", oBeOperador.Montacarga));
        cmd.Parameters.Add(new SqlParameter("@sistema", oBeOperador.Sistema));
    }
    public static void InsertarOActualizar(List<clsBeOperador> entities, SqlConnection conn, SqlTransaction tx)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        if (conn == null)
            throw new ArgumentNullException(nameof(conn));

        if (tx == null)
            throw new ArgumentNullException(nameof(tx));

        try
        {
            foreach (var entity in entities)
            {
                if (entity == null)
                    continue;

                if (entity.IdOperador != 0)
                {
                    bool existe = Existe(entity.IdOperador, conn, tx);

                    if (existe)
                        Actualizar(entity, conn, tx);
                    else
                        Insertar(entity, conn, tx);
                }
            }
        }
        catch (SqlException ex)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
        }
    }
    public static bool Existe(int idOperador, SqlConnection conn, SqlTransaction tx)
    {
        try
        {
            const string query = "SELECT COUNT(1) FROM operador WHERE IdOperador = @IdOperador";

            using (SqlCommand cmd = new SqlCommand(query, conn, tx))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@IdOperador", idOperador));

                object result = cmd.ExecuteScalar();
                int count = Convert.ToInt32(result);

                return count > 0;
            }
        }
        catch (SqlException ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{0} {1}", currentMethodName?.Name ?? "UnknownMethod", ex.Message);

            throw new Exception(vMsgError, ex);
        }
    }
}