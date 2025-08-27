using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using WMS.EntityCore.Despacho;
using Microsoft.Extensions.Configuration;
public class clsLnTrans_despacho_det
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_despacho_det oBeTrans_despacho_det, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        double GetDouble(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeTrans_despacho_det.IdDespachoDet = GetInt("IdDespachoDet");
            oBeTrans_despacho_det.IdDespachoEnc = GetInt("IdDespachoEnc");
            oBeTrans_despacho_det.IdPickingUbic = GetInt("IdPickingUbic");
            oBeTrans_despacho_det.Fecha = GetDate("Fecha");
            oBeTrans_despacho_det.User_agr = GetString("user_agr");
            oBeTrans_despacho_det.Fec_agr = GetDate("fec_agr");
            oBeTrans_despacho_det.User_mod = GetString("user_mod");
            oBeTrans_despacho_det.Fec_mod = GetDate("fec_mod");
            oBeTrans_despacho_det.Activo = GetBool("activo");
            oBeTrans_despacho_det.IdPedidoEnc = GetInt("IdPedidoEnc");
            oBeTrans_despacho_det.IdPedidoDet = GetInt("IdPedidoDet");
            oBeTrans_despacho_det.IdProductoBodega = GetInt("IdProductoBodega");
            oBeTrans_despacho_det.IdUnidadMedidaBasica = GetInt("IdUnidadMedidaBasica");
            oBeTrans_despacho_det.IdPresentacion = GetInt("IdPresentacion");
            oBeTrans_despacho_det.Codigo = GetString("Codigo");
            oBeTrans_despacho_det.NombreProducto = GetString("NombreProducto");
            oBeTrans_despacho_det.NombreEstado = GetString("NombreEstado");
            oBeTrans_despacho_det.CantidadDespachada = GetDouble("CantidadDespachada");
            oBeTrans_despacho_det.PesoDespachado = GetDouble("PesoDespachado");
            oBeTrans_despacho_det.IdProductoEstado = GetInt("IdProductoEstado");
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

    public static int Insertar(IConfiguration config, clsBeTrans_despacho_det oBeTrans_despacho_det, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_despacho_det");
            Ins.Add("iddespachodet", "@iddespachodet", "F");
            Ins.Add("iddespachoenc", "@iddespachoenc", "F");
            Ins.Add("idpickingubic", "@idpickingubic", "F");
            Ins.Add("fecha", "@fecha", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("idpedidodet", "@idpedidodet", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("nombreproducto", "@nombreproducto", "F");
            Ins.Add("nombreestado", "@nombreestado", "F");
            Ins.Add("cantidaddespachada", "@cantidaddespachada", "F");
            Ins.Add("pesodespachado", "@pesodespachado", "F");
            Ins.Add("idproductoestado", "@idproductoestado", "F");

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

            cmd.Parameters.Add(new SqlParameter("@IdDespachoDet", oBeTrans_despacho_det.IdDespachoDet));
            cmd.Parameters.Add(new SqlParameter("@IdDespachoEnc", oBeTrans_despacho_det.IdDespachoEnc));
            cmd.Parameters.Add(new SqlParameter("@IdPickingUbic", oBeTrans_despacho_det.IdPickingUbic));
            cmd.Parameters.Add(new SqlParameter("@Fecha", oBeTrans_despacho_det.Fecha));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTrans_despacho_det.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTrans_despacho_det.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeTrans_despacho_det.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTrans_despacho_det.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeTrans_despacho_det.Activo));
            cmd.Parameters.Add(new SqlParameter("@IdPedidoEnc", oBeTrans_despacho_det.IdPedidoEnc));
            cmd.Parameters.Add(new SqlParameter("@IdPedidoDet", oBeTrans_despacho_det.IdPedidoDet));
            cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", oBeTrans_despacho_det.IdProductoBodega));
            cmd.Parameters.Add(new SqlParameter("@IdUnidadMedidaBasica", oBeTrans_despacho_det.IdUnidadMedidaBasica));
            cmd.Parameters.Add(new SqlParameter("@IdPresentacion", oBeTrans_despacho_det.IdPresentacion));
            cmd.Parameters.Add(new SqlParameter("@Codigo", oBeTrans_despacho_det.Codigo));
            cmd.Parameters.Add(new SqlParameter("@NombreProducto", oBeTrans_despacho_det.NombreProducto));
            cmd.Parameters.Add(new SqlParameter("@NombreEstado", oBeTrans_despacho_det.NombreEstado));
            cmd.Parameters.Add(new SqlParameter("@CantidadDespachada", oBeTrans_despacho_det.CantidadDespachada));
            cmd.Parameters.Add(new SqlParameter("@PesoDespachado", oBeTrans_despacho_det.PesoDespachado));
            cmd.Parameters.Add(new SqlParameter("@IdProductoEstado", oBeTrans_despacho_det.IdProductoEstado));

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

    public static int Insertar(IConfiguration config, clsBeTrans_despacho_det oBeTrans_despacho_det)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_despacho_det");
            Ins.Add("iddespachodet", "@iddespachodet", "F");
            Ins.Add("iddespachoenc", "@iddespachoenc", "F");
            Ins.Add("idpickingubic", "@idpickingubic", "F");
            Ins.Add("fecha", "@fecha", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("idpedidodet", "@idpedidodet", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("nombreproducto", "@nombreproducto", "F");
            Ins.Add("nombreestado", "@nombreestado", "F");
            Ins.Add("cantidaddespachada", "@cantidaddespachada", "F");
            Ins.Add("pesodespachado", "@pesodespachado", "F");
            Ins.Add("idproductoestado", "@idproductoestado", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@IdDespachoDet", oBeTrans_despacho_det.IdDespachoDet));
            cmd.Parameters.Add(new SqlParameter("@IdDespachoEnc", oBeTrans_despacho_det.IdDespachoEnc));
            cmd.Parameters.Add(new SqlParameter("@IdPickingUbic", oBeTrans_despacho_det.IdPickingUbic));
            cmd.Parameters.Add(new SqlParameter("@Fecha", oBeTrans_despacho_det.Fecha));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTrans_despacho_det.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTrans_despacho_det.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeTrans_despacho_det.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTrans_despacho_det.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeTrans_despacho_det.Activo));
            cmd.Parameters.Add(new SqlParameter("@IdPedidoEnc", oBeTrans_despacho_det.IdPedidoEnc));
            cmd.Parameters.Add(new SqlParameter("@IdPedidoDet", oBeTrans_despacho_det.IdPedidoDet));
            cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", oBeTrans_despacho_det.IdProductoBodega));
            cmd.Parameters.Add(new SqlParameter("@IdUnidadMedidaBasica", oBeTrans_despacho_det.IdUnidadMedidaBasica));
            cmd.Parameters.Add(new SqlParameter("@IdPresentacion", oBeTrans_despacho_det.IdPresentacion));
            cmd.Parameters.Add(new SqlParameter("@Codigo", oBeTrans_despacho_det.Codigo));
            cmd.Parameters.Add(new SqlParameter("@NombreProducto", oBeTrans_despacho_det.NombreProducto));
            cmd.Parameters.Add(new SqlParameter("@NombreEstado", oBeTrans_despacho_det.NombreEstado));
            cmd.Parameters.Add(new SqlParameter("@CantidadDespachada", oBeTrans_despacho_det.CantidadDespachada));
            cmd.Parameters.Add(new SqlParameter("@PesoDespachado", oBeTrans_despacho_det.PesoDespachado));
            cmd.Parameters.Add(new SqlParameter("@IdProductoEstado", oBeTrans_despacho_det.IdProductoEstado));

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

    public static int Actualizar(IConfiguration config, clsBeTrans_despacho_det oBeTrans_despacho_det, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("trans_despacho_det");
            Upd.Add("iddespachodet", "@iddespachodet", "F");
            Upd.Add("iddespachoenc", "@iddespachoenc", "F");
            Upd.Add("idpickingubic", "@idpickingubic", "F");
            Upd.Add("fecha", "@fecha", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("idpedidoenc", "@idpedidoenc", "F");
            Upd.Add("idpedidodet", "@idpedidodet", "F");
            Upd.Add("idproductobodega", "@idproductobodega", "F");
            Upd.Add("idunidadmedidabasica", "@idunidadmedidabasica", "F");
            Upd.Add("idpresentacion", "@idpresentacion", "F");
            Upd.Add("codigo", "@codigo", "F");
            Upd.Add("nombreproducto", "@nombreproducto", "F");
            Upd.Add("nombreestado", "@nombreestado", "F");
            Upd.Add("cantidaddespachada", "@cantidaddespachada", "F");
            Upd.Add("pesodespachado", "@pesodespachado", "F");
            Upd.Add("idproductoestado", "@idproductoestado", "F");
            Upd.Where("IdDespachoDet = @IdDespachoDet");

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

            cmd.Parameters.Add(new SqlParameter("@IdDespachoDet", oBeTrans_despacho_det.IdDespachoDet));
            cmd.Parameters.Add(new SqlParameter("@IdDespachoEnc", oBeTrans_despacho_det.IdDespachoEnc));
            cmd.Parameters.Add(new SqlParameter("@IdPickingUbic", oBeTrans_despacho_det.IdPickingUbic));
            cmd.Parameters.Add(new SqlParameter("@Fecha", oBeTrans_despacho_det.Fecha));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTrans_despacho_det.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTrans_despacho_det.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeTrans_despacho_det.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTrans_despacho_det.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeTrans_despacho_det.Activo));
            cmd.Parameters.Add(new SqlParameter("@IdPedidoEnc", oBeTrans_despacho_det.IdPedidoEnc));
            cmd.Parameters.Add(new SqlParameter("@IdPedidoDet", oBeTrans_despacho_det.IdPedidoDet));
            cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", oBeTrans_despacho_det.IdProductoBodega));
            cmd.Parameters.Add(new SqlParameter("@IdUnidadMedidaBasica", oBeTrans_despacho_det.IdUnidadMedidaBasica));
            cmd.Parameters.Add(new SqlParameter("@IdPresentacion", oBeTrans_despacho_det.IdPresentacion));
            cmd.Parameters.Add(new SqlParameter("@Codigo", oBeTrans_despacho_det.Codigo));
            cmd.Parameters.Add(new SqlParameter("@NombreProducto", oBeTrans_despacho_det.NombreProducto));
            cmd.Parameters.Add(new SqlParameter("@NombreEstado", oBeTrans_despacho_det.NombreEstado));
            cmd.Parameters.Add(new SqlParameter("@CantidadDespachada", oBeTrans_despacho_det.CantidadDespachada));
            cmd.Parameters.Add(new SqlParameter("@PesoDespachado", oBeTrans_despacho_det.PesoDespachado));
            cmd.Parameters.Add(new SqlParameter("@IdProductoEstado", oBeTrans_despacho_det.IdProductoEstado));

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

    public int Eliminar(IConfiguration config, clsBeTrans_despacho_det oBeTrans_despacho_det, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_despacho_det" +
             "  Where(IdDespachoDet = @IdDespachoDet)");

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

            cmd.Parameters.Add(new SqlParameter("@IdDespachoDet", oBeTrans_despacho_det.IdDespachoDet));

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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_despacho_det pBeTrans_despacho_det)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_despacho_det" +
            " Where(IdDespachoDet = @IdDespachoDet)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdDespachoDet", pBeTrans_despacho_det.IdDespachoEnc));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_despacho_det, r);
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

    public static List<clsBeTrans_despacho_det> GetAll(IConfiguration config, int IdPedidoEnc)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_despacho_det> lreturnList = new List<clsBeTrans_despacho_det>();

        try
        {
            const string sp = "Select * FROM Trans_despacho_det WHERE IdPedidoEnc = @IdPedidoEnc";

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {

                lConnection.Open();

                using (lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                    {
                        lDTA.SelectCommand.CommandType = CommandType.Text;
                        lDTA.SelectCommand.Transaction = lTransaction;
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc);
                        DataTable lDataTable = new DataTable();
                        lDTA.Fill(lDataTable);

                        clsBeTrans_despacho_det vBeTrans_despacho_det = new clsBeTrans_despacho_det();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_despacho_det = new clsBeTrans_despacho_det();
                            Cargar(ref vBeTrans_despacho_det, dr);
                            lreturnList.Add(vBeTrans_despacho_det);
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

            const string sp = "Select ISNULL(Max(IdDespachoDet),0) FROM Trans_despacho_det";

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


            const string sp = "Select ISNULL(Max(IdDespachoDet),0) FROM Trans_despacho_det";

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

    public static List<clsBeTrans_despacho_det> Get_All_By_IdPedidoEnc(IConfiguration config, int idPedidoEnc, int idDespachoEnc, SqlConnection? pConnection = null, SqlTransaction? pTransaction = null)
    {
        var resultList = new List<clsBeTrans_despacho_det>();
        SqlConnection? lConnection = null;
        SqlTransaction? lTransaction = null;

        try
        {
            string query = @"
            SELECT 
                det.IdDespachoDet, det.IdDespachoEnc, det.IdPickingUbic, det.Fecha,
                det.user_agr, det.fec_agr, det.user_mod, det.fec_mod, det.activo,
                det.IdPedidoEnc, det.IdPedidoDet, det.IdProductoBodega,
                det.IdUnidadMedidaBasica, det.IdPresentacion, det.Codigo,
                det.NombreProducto, det.NombreEstado, det.CantidadDespachada,
                det.PesoDespachado, det.IdProductoEstado,
                pu.lote, pu.lic_plate, pp.Nombre AS Presentacion
            FROM trans_pe_det pe
            INNER JOIN trans_pe_enc enc ON pe.IdPedidoEnc = enc.IdPedidoEnc
            INNER JOIN trans_despacho_det det ON pe.IdPedidoDet = det.IdPedidoDet
            INNER JOIN trans_despacho_enc de ON det.IdDespachoEnc = de.IdDespachoEnc
            INNER JOIN trans_picking_ubic pu ON det.IdPickingUbic = pu.IdPickingUbic
            LEFT JOIN producto_presentacion pp ON pu.IdPresentacion = pp.IdPresentacion
            WHERE pe.IdPedidoEnc = @IdPedidoEnc
              AND det.IdDespachoEnc = @IdDespachoEnc
              AND det.CantidadDespachada > 0";

            bool externalTx = pConnection != null && pTransaction != null;

            if (externalTx)
            {
                lConnection = pConnection;
                lTransaction = pTransaction;
            }
            else
            {
                lConnection = new SqlConnection(config.GetConnectionString("CST"));
                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            using var adapter = new SqlDataAdapter(query, lConnection);
            adapter.SelectCommand.Transaction = lTransaction;
            adapter.SelectCommand.CommandType = CommandType.Text;
            adapter.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", idPedidoEnc);
            adapter.SelectCommand.Parameters.AddWithValue("@IdDespachoEnc", idDespachoEnc);

            var dt = new DataTable();
            adapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var item = new clsBeTrans_despacho_det();
                Cargar(ref item, row);

                item.Lote = row["lote"] != DBNull.Value ? row["lote"].ToString()! : "";
                item.Lic_plate = row["lic_plate"] != DBNull.Value ? row["lic_plate"].ToString()! : "";
                item.ProductoPresentacion = row["Presentacion"] != DBNull.Value ? row["Presentacion"].ToString()! : "";

                resultList.Add(item);
            }

            if (!externalTx && lTransaction != null)
                lTransaction.Commit();

            if (!externalTx && lConnection != null)
                lConnection.Close();

            return resultList;
        }
        catch (Exception ex)
        {
            if (lTransaction is not null && pTransaction is null)
                lTransaction.Rollback();

            throw new Exception($"Get_All_By_IdPedidoEnc → {ex.Message}", ex);
        }
    }
}