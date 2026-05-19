using System.Data;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Data.SqlClient;
using WMSWebAPI.Be;
using Microsoft.Extensions.Configuration;
using System.Reflection;

public class clsLnTarea_hh
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTarea_hh oBeTarea_hh, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }        

        try
        {
            oBeTarea_hh.IdTareahh = GetInt("IdTareahh");
            oBeTarea_hh.IdPropietario = GetInt("IdPropietario");
            oBeTarea_hh.IdBodega = GetInt("IdBodega");
            oBeTarea_hh.IdMuelle = GetInt("IdMuelle");
            oBeTarea_hh.IdEstado = GetInt("IdEstado");
            oBeTarea_hh.IdPrioridad = GetInt("IdPrioridad");
            oBeTarea_hh.IdTipoTarea = GetInt("IdTipoTarea");
            oBeTarea_hh.IdTransaccion = GetInt("IdTransaccion");
            oBeTarea_hh.Tipo = GetInt("Tipo");
            oBeTarea_hh.FechaInicio = GetDate("FechaInicio");
            oBeTarea_hh.FechaFin = GetDate("FechaFin");
            oBeTarea_hh.DiaCompleto = GetBool("DiaCompleto");
            oBeTarea_hh.Asunto = GetString("Asunto");
            oBeTarea_hh.Ubicacion = GetString("Ubicacion");
            oBeTarea_hh.Descripcion = GetString("Descripcion");
            oBeTarea_hh.Recordatorio = GetString("Recordatorio");
            oBeTarea_hh.IdOperadorBodega_Cerro = GetInt("IdOperadorBodega_Cerro");
            oBeTarea_hh.Host_Cerro = GetString("Host_Cerro");
        }
        catch (Exception ex)
        {                        
            throw new Exception(ex.Message);
        }
    }

    public static int Insertar(clsBeTarea_hh oBeTarea_hh, SqlConnection pConection, SqlTransaction pTransaction)
    {
        Ins.Init("tarea_hh");
        Ins.Add("idtareahh", "@idtareahh", "F");
        Ins.Add("idpropietario", "@idpropietario", "F");
        Ins.Add("idbodega", "@idbodega", "F");
        Ins.Add("idmuelle", "@idmuelle", "F");
        Ins.Add("idestado", "@idestado", "F");
        Ins.Add("idprioridad", "@idprioridad", "F");
        Ins.Add("idtipotarea", "@idtipotarea", "F");
        Ins.Add("idtransaccion", "@idtransaccion", "F");
        Ins.Add("tipo", "@tipo", "F");
        Ins.Add("fechainicio", "@fechainicio", "F");
        Ins.Add("fechafin", "@fechafin", "F");
        Ins.Add("diacompleto", "@diacompleto", "F");
        Ins.Add("asunto", "@asunto", "F");
        Ins.Add("ubicacion", "@ubicacion", "F");
        Ins.Add("descripcion", "@descripcion", "F");
        Ins.Add("recordatorio", "@recordatorio", "F");
        Ins.Add("idoperadorbodega_cerro", "@idoperadorbodega_cerro", "F");
        Ins.Add("host_cerro", "@host_cerro", "F");

        string sp = Ins.SQL();

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@IdTareahh", oBeTarea_hh.IdTareahh);
            cmd.Parameters.AddWithValue("@IdPropietario", oBeTarea_hh.IdPropietario);
            cmd.Parameters.AddWithValue("@IdBodega", oBeTarea_hh.IdBodega);
            cmd.Parameters.AddWithValue("@IdMuelle", oBeTarea_hh.IdMuelle);
            cmd.Parameters.AddWithValue("@IdEstado", oBeTarea_hh.IdEstado);
            cmd.Parameters.AddWithValue("@IdPrioridad", oBeTarea_hh.IdPrioridad);
            cmd.Parameters.AddWithValue("@IdTipoTarea", oBeTarea_hh.IdTipoTarea);
            cmd.Parameters.AddWithValue("@IdTransaccion", oBeTarea_hh.IdTransaccion);
            cmd.Parameters.AddWithValue("@Tipo", oBeTarea_hh.Tipo);
            cmd.Parameters.AddWithValue("@FechaInicio", oBeTarea_hh.FechaInicio);
            cmd.Parameters.AddWithValue("@FechaFin", oBeTarea_hh.FechaFin);
            cmd.Parameters.AddWithValue("@DiaCompleto", oBeTarea_hh.DiaCompleto);
            cmd.Parameters.AddWithValue("@Asunto", oBeTarea_hh.Asunto);
            cmd.Parameters.AddWithValue("@Ubicacion", oBeTarea_hh.Ubicacion);
            cmd.Parameters.AddWithValue("@Descripcion", oBeTarea_hh.Descripcion);
            cmd.Parameters.AddWithValue("@Recordatorio", oBeTarea_hh.Recordatorio);
            cmd.Parameters.AddWithValue("@IdOperadorBodega_Cerro", oBeTarea_hh.IdOperadorBodega_Cerro);
            cmd.Parameters.AddWithValue("@Host_Cerro", oBeTarea_hh.Host_Cerro);

            return cmd.ExecuteNonQuery();
        }
    }

    public static int Insertar(IConfiguration config, clsBeTarea_hh oBeTarea_hh)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("tarea_hh");
            Ins.Add("idtareahh", "@idtareahh", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idmuelle", "@idmuelle", "F");
            Ins.Add("idestado", "@idestado", "F");
            Ins.Add("idprioridad", "@idprioridad", "F");
            Ins.Add("idtipotarea", "@idtipotarea", "F");
            Ins.Add("idtransaccion", "@idtransaccion", "F");
            Ins.Add("tipo", "@tipo", "F");
            Ins.Add("fechainicio", "@fechainicio", "F");
            Ins.Add("fechafin", "@fechafin", "F");
            Ins.Add("diacompleto", "@diacompleto", "F");
            Ins.Add("asunto", "@asunto", "F");
            Ins.Add("ubicacion", "@ubicacion", "F");
            Ins.Add("descripcion", "@descripcion", "F");
            Ins.Add("recordatorio", "@recordatorio", "F");
            Ins.Add("idoperadorbodega_cerro", "@idoperadorbodega_cerro", "F");
            Ins.Add("host_cerro", "@host_cerro", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@IdTareahh", oBeTarea_hh.IdTareahh));
            cmd.Parameters.Add(new SqlParameter("@IdPropietario", oBeTarea_hh.IdPropietario));
            cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeTarea_hh.IdBodega));
            cmd.Parameters.Add(new SqlParameter("@IdMuelle", oBeTarea_hh.IdMuelle));
            cmd.Parameters.Add(new SqlParameter("@IdEstado", oBeTarea_hh.IdEstado));
            cmd.Parameters.Add(new SqlParameter("@IdPrioridad", oBeTarea_hh.IdPrioridad));
            cmd.Parameters.Add(new SqlParameter("@IdTipoTarea", oBeTarea_hh.IdTipoTarea));
            cmd.Parameters.Add(new SqlParameter("@IdTransaccion", oBeTarea_hh.IdTransaccion));
            cmd.Parameters.Add(new SqlParameter("@Tipo", oBeTarea_hh.Tipo));
            cmd.Parameters.Add(new SqlParameter("@FechaInicio", oBeTarea_hh.FechaInicio));
            cmd.Parameters.Add(new SqlParameter("@FechaFin", oBeTarea_hh.FechaFin));
            cmd.Parameters.Add(new SqlParameter("@DiaCompleto", oBeTarea_hh.DiaCompleto));
            cmd.Parameters.Add(new SqlParameter("@Asunto", oBeTarea_hh.Asunto));
            cmd.Parameters.Add(new SqlParameter("@Ubicacion", oBeTarea_hh.Ubicacion));
            cmd.Parameters.Add(new SqlParameter("@Descripcion", oBeTarea_hh.Descripcion));
            cmd.Parameters.Add(new SqlParameter("@Recordatorio", oBeTarea_hh.Recordatorio));
            cmd.Parameters.Add(new SqlParameter("@IdOperadorBodega_Cerro", oBeTarea_hh.IdOperadorBodega_Cerro));
            cmd.Parameters.Add(new SqlParameter("@Host_Cerro", oBeTarea_hh.Host_Cerro));

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

    public static int Actualizar(clsBeTarea_hh oBeTarea_hh, SqlConnection pConection, SqlTransaction pTransaction)
    {
        Upd.Init("tarea_hh");
        Upd.Add("idtareahh", "@idtareahh", "F");
        Upd.Add("idpropietario", "@idpropietario", "F");
        Upd.Add("idbodega", "@idbodega", "F");
        Upd.Add("idmuelle", "@idmuelle", "F");
        Upd.Add("idestado", "@idestado", "F");
        Upd.Add("idprioridad", "@idprioridad", "F");
        Upd.Add("idtipotarea", "@idtipotarea", "F");
        Upd.Add("idtransaccion", "@idtransaccion", "F");
        Upd.Add("tipo", "@tipo", "F");
        Upd.Add("fechainicio", "@fechainicio", "F");
        Upd.Add("fechafin", "@fechafin", "F");
        Upd.Add("diacompleto", "@diacompleto", "F");
        Upd.Add("asunto", "@asunto", "F");
        Upd.Add("ubicacion", "@ubicacion", "F");
        Upd.Add("descripcion", "@descripcion", "F");
        Upd.Add("recordatorio", "@recordatorio", "F");
        Upd.Add("idoperadorbodega_cerro", "@idoperadorbodega_cerro", "F");
        Upd.Add("host_cerro", "@host_cerro", "F");
        Upd.Where("IdTareahh = @IdTareahh");

        string sp = Upd.SQL();

        using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
        {
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@IdTareahh", oBeTarea_hh.IdTareahh);
            cmd.Parameters.AddWithValue("@IdPropietario", oBeTarea_hh.IdPropietario);
            cmd.Parameters.AddWithValue("@IdBodega", oBeTarea_hh.IdBodega);
            cmd.Parameters.AddWithValue("@IdMuelle", oBeTarea_hh.IdMuelle);
            cmd.Parameters.AddWithValue("@IdEstado", oBeTarea_hh.IdEstado);
            cmd.Parameters.AddWithValue("@IdPrioridad", oBeTarea_hh.IdPrioridad);
            cmd.Parameters.AddWithValue("@IdTipoTarea", oBeTarea_hh.IdTipoTarea);
            cmd.Parameters.AddWithValue("@IdTransaccion", oBeTarea_hh.IdTransaccion);
            cmd.Parameters.AddWithValue("@Tipo", oBeTarea_hh.Tipo);
            cmd.Parameters.AddWithValue("@FechaInicio", oBeTarea_hh.FechaInicio);
            cmd.Parameters.AddWithValue("@FechaFin", oBeTarea_hh.FechaFin);
            cmd.Parameters.AddWithValue("@DiaCompleto", oBeTarea_hh.DiaCompleto);
            cmd.Parameters.AddWithValue("@Asunto", oBeTarea_hh.Asunto);
            cmd.Parameters.AddWithValue("@Ubicacion", oBeTarea_hh.Ubicacion);
            cmd.Parameters.AddWithValue("@Descripcion", oBeTarea_hh.Descripcion);
            cmd.Parameters.AddWithValue("@Recordatorio", oBeTarea_hh.Recordatorio);
            cmd.Parameters.AddWithValue("@IdOperadorBodega_Cerro", oBeTarea_hh.IdOperadorBodega_Cerro);
            cmd.Parameters.AddWithValue("@Host_Cerro", oBeTarea_hh.Host_Cerro);

            return cmd.ExecuteNonQuery();
        }
    }

    public int Eliminar(IConfiguration config, clsBeTarea_hh oBeTarea_hh, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Tarea_hh" +
             "  Where(IdTareahh = @IdTareahh)");

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

            cmd.Parameters.Add(new SqlParameter("@IdTareahh", oBeTarea_hh.IdTareahh));

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

    public static bool GetSingle(IConfiguration config, ref clsBeTarea_hh pBeTarea_hh)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Tarea_hh" +
            " Where(IdTareahh = @IdTareahh)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTareahh", pBeTarea_hh.IdTareahh));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPropietario", pBeTarea_hh.IdPropietario));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdBodega", pBeTarea_hh.IdBodega));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdMuelle", pBeTarea_hh.IdMuelle));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdEstado", pBeTarea_hh.IdEstado));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPrioridad", pBeTarea_hh.IdPrioridad));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTipoTarea", pBeTarea_hh.IdTipoTarea));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTransaccion", pBeTarea_hh.IdTransaccion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Tipo", pBeTarea_hh.Tipo));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@FechaInicio", pBeTarea_hh.FechaInicio));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@FechaFin", pBeTarea_hh.FechaFin));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@DiaCompleto", pBeTarea_hh.DiaCompleto));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Asunto", pBeTarea_hh.Asunto));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Ubicacion", pBeTarea_hh.Ubicacion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Descripcion", pBeTarea_hh.Descripcion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Recordatorio", pBeTarea_hh.Recordatorio));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdOperadorBodega_Cerro", pBeTarea_hh.IdOperadorBodega_Cerro));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Host_Cerro", pBeTarea_hh.Host_Cerro));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTarea_hh, r);
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

    public static List<clsBeTarea_hh> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTarea_hh> lreturnList = new List<clsBeTarea_hh>();

        try
        {
            const string sp = "Select * FROM Tarea_hh";

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

                        clsBeTarea_hh vBeTarea_hh = new clsBeTarea_hh();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTarea_hh = new clsBeTarea_hh();
                            Cargar(ref vBeTarea_hh, dr);
                            lreturnList.Add(vBeTarea_hh);
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

            const string sp = "Select ISNULL(Max(IdTareahh),0) FROM Tarea_hh";

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
    public static int MaxID(SqlConnection pConection, SqlTransaction pTransaction)
    {
        const string sp = "Select ISNULL(Max(IdTareahh),0) FROM Tarea_hh";

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

    public static int Get_Tiempo_Medio_Tarea_Ingreso_Minutos(SqlConnection lConnection, SqlTransaction lTransaction)
    {        
        int tiempoMedio = 60;

        try
        {
            string sp = "SELECT AVG(DATEDIFF(MINUTE, fechainicio, fechafin)) as minutos FROM tarea_hh WHERE IdTipoTarea = 1";
            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction)
            {
                CommandType = CommandType.Text
            };

            SqlDataAdapter dad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dad.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["minutos"] == DBNull.Value ? 60 : Convert.ToInt32(dt.Rows[0]["minutos"]);
            }

            dt.Dispose();

            return tiempoMedio;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static void Guardar_Tarea_Recepcion_HH(clsBeTarea_hh pObjTareaHH,
                                             SqlConnection lConnection,
                                             SqlTransaction lTransaction)
    {
        try
        {
            if (pObjTareaHH != null)
            {
                if (pObjTareaHH.IsNew && pObjTareaHH.CreaTarea)
                {
                    pObjTareaHH.IdTareahh = MaxID(lConnection, lTransaction)+1;
                    Insertar(pObjTareaHH, lConnection, lTransaction);
                }
                else
                {
                    Actualizar(pObjTareaHH, lConnection, lTransaction);
                }
            }
        }
        catch (Exception)
        {         
            throw;
        }
    }

    public static clsBeTarea_hh? GetSingle(int pIdTipoTarea,
                                           int pIdTransaccion,
                                           SqlTransaction lTransaction,
                                           SqlConnection lConnection)
        {
        try
        {
            string vSQL = @"SELECT * FROM tarea_hh 
                       WHERE IdTipoTarea = @IdTipoTarea 
                       AND IdTransaccion = @IdTransaccion";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoTarea", pIdTipoTarea);
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTransaccion", pIdTransaccion);

                DataTable lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable?.Rows.Count > 0)
                {
                    clsBeTarea_hh Obj = new clsBeTarea_hh();
                    Cargar(ref Obj, lDataTable.Rows[0]);
                    Obj.IsNew = false;
                    return Obj;
                }
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }    

    public static clsBeTarea_hh? Get_Tarea_Recepcion_By_IdRecepcionEnc(IConfiguration config,
                                                                       int pIdRecepcionEnc)
    {
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            lConnection.Open();
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            clsBeTarea_hh? tarea = GetSingle(1, pIdRecepcionEnc, lTransaction, lConnection);

            lTransaction.Commit();
            return tarea;
        }
        catch (Exception)
        {
            if (lTransaction != null) lTransaction.Rollback();
            throw;
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lTransaction != null) lTransaction.Dispose();
            lConnection.Dispose();
        }
    }

    public static clsBeTarea_hh GetSingle(int IdTipoTarea,
                                          int IdTransaccion,
                                          int IdPropietario,
                                          SqlConnection lConnection,
                                          SqlTransaction lTransaction)
    {
        try
        {
            const string sp = "SELECT * FROM Tarea_hh " +
                             " Where(IdTipoTarea = @IdTipoTarea " +
                             " And IdTransaccion = @IdTransaccion " +
                             " And IdPropietario = @IdPropietario ) ";

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);
            clsBeTarea_hh pBeTarea_hh = new clsBeTarea_hh();

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTipoTarea", IdTipoTarea));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTransaccion", IdTransaccion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPropietario", IdPropietario));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                Cargar(ref pBeTarea_hh, dt.Rows[0]);
            }

            return pBeTarea_hh;
        }
        catch (Exception)
        {            
            throw;
        }
    }

    public static int Finalizar_Tarea_Recepcion(int pIdRecepcionEnc,
                                                SqlConnection pConection,
                                                SqlTransaction pTransaction)
    {
        int result = 0;

        try
        {
            string vSQL = "UPDATE tarea_hh " +
                         "SET IdEstado = @IdEstado, " +
                         "FechaFin = GetDate() " +
                         "WHERE IdTransaccion = @IdTransaccion " +
                         "AND IdTipoTarea = @IdTipoTarea ";

            SqlCommand cmd = new SqlCommand(vSQL, pConection, pTransaction) { CommandType = CommandType.Text };

            cmd.Parameters.Add(new SqlParameter("@IdTransaccion", pIdRecepcionEnc));
            cmd.Parameters.Add(new SqlParameter("@IdTipoTarea", 1));
            cmd.Parameters.Add(new SqlParameter("@IdEstado", 4));

            result = cmd.ExecuteNonQuery();

            cmd.Dispose();

            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
