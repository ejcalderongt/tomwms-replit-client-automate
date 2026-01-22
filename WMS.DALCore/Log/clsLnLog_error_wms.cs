using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Log;
using Microsoft.Extensions.Configuration;
public class clsLnLog_error_wms
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeLog_error_wms oBeLog_error_wms, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        double getDouble(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeLog_error_wms.IdError = GetInt("IdError");
            oBeLog_error_wms.IdEmpresa = GetInt("IdEmpresa");
            oBeLog_error_wms.IdBodega = GetInt("IdBodega");
            oBeLog_error_wms.Fecha = GetDate("Fecha");
            oBeLog_error_wms.MensajeError = GetString("MensajeError");
            oBeLog_error_wms.IdPedidoEnc = GetInt("IdPedidoEnc");
            oBeLog_error_wms.IdPickingEnc = GetInt("IdPickingEnc");
            oBeLog_error_wms.IdRecepcionEnc = GetInt("IdRecepcionEnc");
            oBeLog_error_wms.IdUsuarioAgr = GetInt("IdUsuarioAgr");
            oBeLog_error_wms.Line_No = GetInt("Line_No");
            oBeLog_error_wms.Item_No = GetString("Item_No");
            oBeLog_error_wms.UmBas = GetString("UmBas");
            oBeLog_error_wms.Variant_Code = GetString("Variant_Code");
            oBeLog_error_wms.Cantidad = getDouble("Cantidad");
            oBeLog_error_wms.Referencia_Documento = GetString("Referencia_Documento");
        }
        catch (Exception)
        {        
            throw;
        }
    }

    public static int Insertar(clsBeLog_error_wms oBeLog_error_wms, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

        try
        {
            Ins.Init("log_error_wms");
            Ins.Add("iderror", "@iderror", "F");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("fecha", "@fecha", "F");
            Ins.Add("mensajeerror", "@mensajeerror", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("idpickingenc", "@idpickingenc", "F");
            Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Ins.Add("idusuarioagr", "@idusuarioagr", "F");
            Ins.Add("line_no", "@line_no", "F");
            Ins.Add("item_no", "@item_no", "F");
            Ins.Add("umbas", "@umbas", "F");
            Ins.Add("variant_code", "@variant_code", "F");
            Ins.Add("cantidad", "@cantidad", "F");
            Ins.Add("referencia_documento", "@referencia_documento", "F");

            string sp = Ins.SQL();

            using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction)
            {
                CommandType = CommandType.Text
            };

            BindLogParameters(cmd, oBeLog_error_wms);

            rowsAffected = cmd.ExecuteNonQuery();
        }
        catch (Exception ex1)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
            throw new Exception(vMsgError);
        }

        return rowsAffected;
    }

    public static void BindLogParameters(SqlCommand cmd, clsBeLog_error_wms log)
    {
        cmd.Parameters.Add(new SqlParameter("@IdError", log.IdError));
        cmd.Parameters.Add(new SqlParameter("@IdEmpresa", log.IdEmpresa));
        cmd.Parameters.Add(new SqlParameter("@IdBodega", log.IdBodega));
        cmd.Parameters.Add(new SqlParameter("@Fecha", log.Fecha));
        cmd.Parameters.Add(new SqlParameter("@MensajeError", log.MensajeError));
        cmd.Parameters.Add(new SqlParameter("@IdPedidoEnc", log.IdPedidoEnc));
        cmd.Parameters.Add(new SqlParameter("@IdPickingEnc", log.IdPickingEnc));
        cmd.Parameters.Add(new SqlParameter("@IdRecepcionEnc", log.IdRecepcionEnc));
        cmd.Parameters.Add(new SqlParameter("@IdUsuarioAgr", log.IdUsuarioAgr));
        cmd.Parameters.Add(new SqlParameter("@Line_No", log.Line_No));
        cmd.Parameters.Add(new SqlParameter("@Item_No", log.Item_No));
        cmd.Parameters.Add(new SqlParameter("@UmBas", log.UmBas));
        cmd.Parameters.Add(new SqlParameter("@Variant_Code", log.Variant_Code));
        cmd.Parameters.Add(new SqlParameter("@Cantidad", log.Cantidad));
        cmd.Parameters.Add(new SqlParameter("@Referencia_Documento", log.Referencia_Documento));
    }


    public static int Insertar(IConfiguration config, clsBeLog_error_wms oBeLog_error_wms)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("log_error_wms");
            Ins.Add("iderror", "@iderror", "F");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("fecha", "@fecha", "F");
            Ins.Add("mensajeerror", "@mensajeerror", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("idpickingenc", "@idpickingenc", "F");
            Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Ins.Add("idusuarioagr", "@idusuarioagr", "F");
            Ins.Add("line_no", "@line_no", "F");
            Ins.Add("item_no", "@item_no", "F");
            Ins.Add("umbas", "@umbas", "F");
            Ins.Add("variant_code", "@variant_code", "F");
            Ins.Add("cantidad", "@cantidad", "F");
            Ins.Add("referencia_documento", "@referencia_documento", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            BindLogParameters(cmd, oBeLog_error_wms);

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

    public static int Actualizar(IConfiguration config, clsBeLog_error_wms oBeLog_error_wms, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("log_error_wms");
            Upd.Add("iderror", "@iderror", "F");
            Upd.Add("idempresa", "@idempresa", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("fecha", "@fecha", "F");
            Upd.Add("mensajeerror", "@mensajeerror", "F");
            Upd.Add("idpedidoenc", "@idpedidoenc", "F");
            Upd.Add("idpickingenc", "@idpickingenc", "F");
            Upd.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Upd.Add("idusuarioagr", "@idusuarioagr", "F");
            Upd.Add("line_no", "@line_no", "F");
            Upd.Add("item_no", "@item_no", "F");
            Upd.Add("umbas", "@umbas", "F");
            Upd.Add("variant_code", "@variant_code", "F");
            Upd.Add("cantidad", "@cantidad", "F");
            Upd.Add("referencia_documento", "@referencia_documento", "F");
            Upd.Where("IdError = @IdError");

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

            BindLogParameters(cmd, oBeLog_error_wms);

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

    public int Eliminar(IConfiguration config, clsBeLog_error_wms oBeLog_error_wms, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Log_error_wms" +
             "  Where(IdError = @IdError)");

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

            cmd.Parameters.Add(new SqlParameter("@IdError", oBeLog_error_wms.IdError));

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
            const string sp = "Select * FROM Log_error_wms";
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

    public static bool GetSingle(IConfiguration config, ref clsBeLog_error_wms pBeLog_error_wms)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Log_error_wms" +
            " Where(IdError = @IdError)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdError", pBeLog_error_wms.IdError));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdEmpresa", pBeLog_error_wms.IdEmpresa));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdBodega", pBeLog_error_wms.IdBodega));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Fecha", pBeLog_error_wms.Fecha));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@MensajeError", pBeLog_error_wms.MensajeError));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPedidoEnc", pBeLog_error_wms.IdPedidoEnc));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPickingEnc", pBeLog_error_wms.IdPickingEnc));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdRecepcionEnc", pBeLog_error_wms.IdRecepcionEnc));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdUsuarioAgr", pBeLog_error_wms.IdUsuarioAgr));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Line_No", pBeLog_error_wms.Line_No));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Item_No", pBeLog_error_wms.Item_No));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@UmBas", pBeLog_error_wms.UmBas));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Variant_Code", pBeLog_error_wms.Variant_Code));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Cantidad", pBeLog_error_wms.Cantidad));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Referencia_Documento", pBeLog_error_wms.Referencia_Documento));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeLog_error_wms, r);
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

    public static List<clsBeLog_error_wms> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeLog_error_wms> lreturnList = new List<clsBeLog_error_wms>();

        try
        {
            const string sp = "Select * FROM Log_error_wms";

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

                        clsBeLog_error_wms vBeLog_error_wms = new clsBeLog_error_wms();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeLog_error_wms = new clsBeLog_error_wms();
                            Cargar(ref vBeLog_error_wms, dr);
                            lreturnList.Add(vBeLog_error_wms);
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

            const string sp = "Select ISNULL(Max(IdError),0) FROM Log_error_wms";

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
    public static int MaxID(SqlConnection pConection, SqlTransaction pTransaction)
    {
        int lMax = 0;

        try
        {
            const string sp = "SELECT ISNULL(Max(IdError),0) FROM Log_error_wms";

            using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction)
            {
                CommandType = CommandType.Text
            };

            var lreturnValue = cmd.ExecuteScalar();

            if (lreturnValue != DBNull.Value && lreturnValue != null)
            {
                lMax = (int)lreturnValue;
            }

            return lMax;
        }
        catch (Exception ex1)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);
            throw new Exception(vMsgError);
        }
    }

    public static void Agregar_Error(IConfiguration config, string pMensajeExcepcion)
    {
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            lConnection.Open();
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            if (lConnection.State == ConnectionState.Open)
            {
                var oBeLog_error_wms = new clsBeLog_error_wms();
                oBeLog_error_wms.IdError = MaxID(lConnection, lTransaction) + 1;

                if (!string.IsNullOrEmpty(pMensajeExcepcion) && pMensajeExcepcion.Length > 2000)
                {
                    pMensajeExcepcion = pMensajeExcepcion.Substring(0, 2000);
                }

                oBeLog_error_wms.MensajeError = pMensajeExcepcion;
                oBeLog_error_wms.Fecha = DateTime.Now;
                oBeLog_error_wms.IdEmpresa = 0;
                oBeLog_error_wms.IdBodega = 0;

                Insertar(oBeLog_error_wms, lConnection, lTransaction);
            }

            if (lTransaction?.Connection != null)
            {
                lTransaction.Commit();
            }
        }
        catch (Exception ex)
        {
            if (lTransaction != null)
            {
                lTransaction.Rollback();
            }

            if (ex.Message.StartsWith("Insertar String or binary data would be truncated"))
            {

                throw new Exception(pMensajeExcepcion);
            }
            else
            {
                throw;
            }
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open)
                lConnection.Close();

            lConnection?.Dispose();
            lTransaction?.Dispose();
        }
    }

    public static int Eliminar_By_Referencia_Documento(string pReferenciaDocumento,
                                                      SqlConnection pConection,
                                                      SqlTransaction pTransaction)
    {
        try
        {
            const string sp = "DELETE FROM Log_error_wms WHERE (Referencia_Documento = @Referencia_Documento)";

            using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.Add(new SqlParameter("@REFERENCIA_DOCUMENTO", pReferenciaDocumento ?? (object)DBNull.Value));

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static List<clsBeLog_error_wms> Get_All_By_Referencia_Documento(IConfiguration config, string pReferenciaDocumento)
    {
        List<clsBeLog_error_wms> lReturnList = new List<clsBeLog_error_wms>();        
        
        try
        {
            const string sp = "SELECT * FROM LOG_ERROR_WMS WHERE REFERENCIA_DOCUMENTO = @REFERENCIA_DOCUMENTO";

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {
                lConnection.Open();

                using (SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                    {
                        lDTA.SelectCommand.CommandType = CommandType.Text;
                        lDTA.SelectCommand.Transaction = lTransaction;
                        lDTA.SelectCommand.Parameters.AddWithValue("@REFERENCIA_DOCUMENTO", pReferenciaDocumento);
                        DataTable lDataTable = new DataTable();
                        lDTA.Fill(lDataTable);

                        clsBeLog_error_wms vBeLog_error_wms = new clsBeLog_error_wms();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeLog_error_wms = new clsBeLog_error_wms();
                            Cargar(ref vBeLog_error_wms, dr);
                            lReturnList.Add(vBeLog_error_wms);
                        }
                    }

                    lTransaction.Commit();
                }

                lConnection.Close();
            }

            return lReturnList;
        }
        catch (Exception)
        {            
            throw;
        }
    }
}
