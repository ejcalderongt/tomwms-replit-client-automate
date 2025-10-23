using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using WMSWebAPI.Be;

public class clsLnBodega_ubicacion
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeBodega_ubicacion oBeBodega_ubicacion, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }        
        double GetDecimal(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeBodega_ubicacion.IdUbicacion = GetInt("IdUbicacion");
            oBeBodega_ubicacion.IdTramo = GetInt("IdTramo");
            oBeBodega_ubicacion.Descripcion = GetString("descripcion");
            oBeBodega_ubicacion.Ancho = GetDecimal("ancho");
            oBeBodega_ubicacion.Largo = GetDecimal("largo");
            oBeBodega_ubicacion.Alto = GetDecimal("alto");
            oBeBodega_ubicacion.Nivel = GetInt("nivel");
            oBeBodega_ubicacion.Indice_x = GetInt("indice_x");
            oBeBodega_ubicacion.IdIndiceRotacion = GetInt("IdIndiceRotacion");
            oBeBodega_ubicacion.IdTipoRotacion = GetInt("IdTipoRotacion");
            oBeBodega_ubicacion.Sistema = GetBool("sistema");
            oBeBodega_ubicacion.Codigo_barra = GetString("codigo_barra");
            oBeBodega_ubicacion.Codigo_barra2 = GetString("codigo_barra2");
            oBeBodega_ubicacion.User_agr = GetString("user_agr");
            oBeBodega_ubicacion.Fec_agr = GetDate("fec_agr");
            oBeBodega_ubicacion.User_mod = GetString("user_mod");
            oBeBodega_ubicacion.Fec_mod = GetDate("fec_mod");
            oBeBodega_ubicacion.Dañado = GetBool("dañado");
            oBeBodega_ubicacion.Activo = GetBool("activo");
            oBeBodega_ubicacion.Bloqueada = GetBool("bloqueada");
            oBeBodega_ubicacion.Acepta_pallet = GetBool("acepta_pallet");
            oBeBodega_ubicacion.Ubicacion_picking = GetBool("ubicacion_picking");
            oBeBodega_ubicacion.Ubicacion_recepcion = GetBool("ubicacion_recepcion");
            oBeBodega_ubicacion.Ubicacion_despacho = GetBool("ubicacion_despacho");
            oBeBodega_ubicacion.Ubicacion_merma = GetBool("ubicacion_merma");
            oBeBodega_ubicacion.Margen_izquierdo = GetDecimal("margen_izquierdo");
            oBeBodega_ubicacion.Margen_derecho = GetDecimal("margen_derecho");
            oBeBodega_ubicacion.Margen_superior = GetDecimal("margen_superior");
            oBeBodega_ubicacion.Margen_inferior = GetDecimal("margen_inferior");
            oBeBodega_ubicacion.Orientacion_pos = GetString("orientacion_pos");
            oBeBodega_ubicacion.Ubicacion_virtual = GetBool("ubicacion_virtual");
            oBeBodega_ubicacion.Ubicacion_ne = GetBool("ubicacion_ne");
            oBeBodega_ubicacion.IdBodega = GetInt("IdBodega");
            oBeBodega_ubicacion.IdArea = GetInt("IdArea");
            oBeBodega_ubicacion.IdSector = GetInt("IdSector");
            oBeBodega_ubicacion.Posicion_x = GetDecimal("posicion_x");
            oBeBodega_ubicacion.Posicion_y = GetDecimal("posicion_y");
            oBeBodega_ubicacion.Ubicacion_muelle = GetBool("ubicacion_muelle");
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

    public static int Insertar(IConfiguration config, clsBeBodega_ubicacion oBeBodega_ubicacion, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("bodega_ubicacion");
            Ins.Add("idubicacion", "@idubicacion", "F");
            Ins.Add("idtramo", "@idtramo", "F");
            Ins.Add("descripcion", "@descripcion", "F");
            Ins.Add("ancho", "@ancho", "F");
            Ins.Add("largo", "@largo", "F");
            Ins.Add("alto", "@alto", "F");
            Ins.Add("nivel", "@nivel", "F");
            Ins.Add("indice_x", "@indice_x", "F");
            Ins.Add("idindicerotacion", "@idindicerotacion", "F");
            Ins.Add("idtiporotacion", "@idtiporotacion", "F");
            Ins.Add("sistema", "@sistema", "F");
            Ins.Add("codigo_barra", "@codigo_barra", "F");
            Ins.Add("codigo_barra2", "@codigo_barra2", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("dañado", "@dañado", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("bloqueada", "@bloqueada", "F");
            Ins.Add("acepta_pallet", "@acepta_pallet", "F");
            Ins.Add("ubicacion_picking", "@ubicacion_picking", "F");
            Ins.Add("ubicacion_recepcion", "@ubicacion_recepcion", "F");
            Ins.Add("ubicacion_despacho", "@ubicacion_despacho", "F");
            Ins.Add("ubicacion_merma", "@ubicacion_merma", "F");
            Ins.Add("margen_izquierdo", "@margen_izquierdo", "F");
            Ins.Add("margen_derecho", "@margen_derecho", "F");
            Ins.Add("margen_superior", "@margen_superior", "F");
            Ins.Add("margen_inferior", "@margen_inferior", "F");
            Ins.Add("orientacion_pos", "@orientacion_pos", "F");
            Ins.Add("ubicacion_virtual", "@ubicacion_virtual", "F");
            Ins.Add("ubicacion_ne", "@ubicacion_ne", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idarea", "@idarea", "F");
            Ins.Add("idsector", "@idsector", "F");
            Ins.Add("posicion_x", "@posicion_x", "F");
            Ins.Add("posicion_y", "@posicion_y", "F");
            Ins.Add("ubicacion_muelle", "@ubicacion_muelle", "F");

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

            cmd.Parameters.Add(new SqlParameter("@IdUbicacion", oBeBodega_ubicacion.IdUbicacion));
            cmd.Parameters.Add(new SqlParameter("@IdTramo", oBeBodega_ubicacion.IdTramo));
            cmd.Parameters.Add(new SqlParameter("@descripcion", oBeBodega_ubicacion.Descripcion));
            cmd.Parameters.Add(new SqlParameter("@ancho", oBeBodega_ubicacion.Ancho));
            cmd.Parameters.Add(new SqlParameter("@largo", oBeBodega_ubicacion.Largo));
            cmd.Parameters.Add(new SqlParameter("@alto", oBeBodega_ubicacion.Alto));
            cmd.Parameters.Add(new SqlParameter("@nivel", oBeBodega_ubicacion.Nivel));
            cmd.Parameters.Add(new SqlParameter("@indice_x", oBeBodega_ubicacion.Indice_x));
            cmd.Parameters.Add(new SqlParameter("@IdIndiceRotacion", oBeBodega_ubicacion.IdIndiceRotacion));
            cmd.Parameters.Add(new SqlParameter("@IdTipoRotacion", oBeBodega_ubicacion.IdTipoRotacion));
            cmd.Parameters.Add(new SqlParameter("@sistema", oBeBodega_ubicacion.Sistema));
            cmd.Parameters.Add(new SqlParameter("@codigo_barra", oBeBodega_ubicacion.Codigo_barra));
            cmd.Parameters.Add(new SqlParameter("@codigo_barra2", oBeBodega_ubicacion.Codigo_barra2));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeBodega_ubicacion.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeBodega_ubicacion.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeBodega_ubicacion.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeBodega_ubicacion.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@dañado", oBeBodega_ubicacion.Dañado));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeBodega_ubicacion.Activo));
            cmd.Parameters.Add(new SqlParameter("@bloqueada", oBeBodega_ubicacion.Bloqueada));
            cmd.Parameters.Add(new SqlParameter("@acepta_pallet", oBeBodega_ubicacion.Acepta_pallet));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_picking", oBeBodega_ubicacion.Ubicacion_picking));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_recepcion", oBeBodega_ubicacion.Ubicacion_recepcion));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_despacho", oBeBodega_ubicacion.Ubicacion_despacho));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_merma", oBeBodega_ubicacion.Ubicacion_merma));
            cmd.Parameters.Add(new SqlParameter("@margen_izquierdo", oBeBodega_ubicacion.Margen_izquierdo));
            cmd.Parameters.Add(new SqlParameter("@margen_derecho", oBeBodega_ubicacion.Margen_derecho));
            cmd.Parameters.Add(new SqlParameter("@margen_superior", oBeBodega_ubicacion.Margen_superior));
            cmd.Parameters.Add(new SqlParameter("@margen_inferior", oBeBodega_ubicacion.Margen_inferior));
            cmd.Parameters.Add(new SqlParameter("@orientacion_pos", oBeBodega_ubicacion.Orientacion_pos));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_virtual", oBeBodega_ubicacion.Ubicacion_virtual));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_ne", oBeBodega_ubicacion.Ubicacion_ne));
            cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeBodega_ubicacion.IdBodega));
            cmd.Parameters.Add(new SqlParameter("@IdArea", oBeBodega_ubicacion.IdArea));
            cmd.Parameters.Add(new SqlParameter("@IdSector", oBeBodega_ubicacion.IdSector));
            cmd.Parameters.Add(new SqlParameter("@posicion_x", oBeBodega_ubicacion.Posicion_x));
            cmd.Parameters.Add(new SqlParameter("@posicion_y", oBeBodega_ubicacion.Posicion_y));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_muelle", oBeBodega_ubicacion.Ubicacion_muelle));

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

    public static int Insertar(IConfiguration config, clsBeBodega_ubicacion oBeBodega_ubicacion)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("bodega_ubicacion");
            Ins.Add("idubicacion", "@idubicacion", "F");
            Ins.Add("idtramo", "@idtramo", "F");
            Ins.Add("descripcion", "@descripcion", "F");
            Ins.Add("ancho", "@ancho", "F");
            Ins.Add("largo", "@largo", "F");
            Ins.Add("alto", "@alto", "F");
            Ins.Add("nivel", "@nivel", "F");
            Ins.Add("indice_x", "@indice_x", "F");
            Ins.Add("idindicerotacion", "@idindicerotacion", "F");
            Ins.Add("idtiporotacion", "@idtiporotacion", "F");
            Ins.Add("sistema", "@sistema", "F");
            Ins.Add("codigo_barra", "@codigo_barra", "F");
            Ins.Add("codigo_barra2", "@codigo_barra2", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("dañado", "@dañado", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("bloqueada", "@bloqueada", "F");
            Ins.Add("acepta_pallet", "@acepta_pallet", "F");
            Ins.Add("ubicacion_picking", "@ubicacion_picking", "F");
            Ins.Add("ubicacion_recepcion", "@ubicacion_recepcion", "F");
            Ins.Add("ubicacion_despacho", "@ubicacion_despacho", "F");
            Ins.Add("ubicacion_merma", "@ubicacion_merma", "F");
            Ins.Add("margen_izquierdo", "@margen_izquierdo", "F");
            Ins.Add("margen_derecho", "@margen_derecho", "F");
            Ins.Add("margen_superior", "@margen_superior", "F");
            Ins.Add("margen_inferior", "@margen_inferior", "F");
            Ins.Add("orientacion_pos", "@orientacion_pos", "F");
            Ins.Add("ubicacion_virtual", "@ubicacion_virtual", "F");
            Ins.Add("ubicacion_ne", "@ubicacion_ne", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idarea", "@idarea", "F");
            Ins.Add("idsector", "@idsector", "F");
            Ins.Add("posicion_x", "@posicion_x", "F");
            Ins.Add("posicion_y", "@posicion_y", "F");
            Ins.Add("ubicacion_muelle", "@ubicacion_muelle", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@IdUbicacion", oBeBodega_ubicacion.IdUbicacion));
            cmd.Parameters.Add(new SqlParameter("@IdTramo", oBeBodega_ubicacion.IdTramo));
            cmd.Parameters.Add(new SqlParameter("@descripcion", oBeBodega_ubicacion.Descripcion));
            cmd.Parameters.Add(new SqlParameter("@ancho", oBeBodega_ubicacion.Ancho));
            cmd.Parameters.Add(new SqlParameter("@largo", oBeBodega_ubicacion.Largo));
            cmd.Parameters.Add(new SqlParameter("@alto", oBeBodega_ubicacion.Alto));
            cmd.Parameters.Add(new SqlParameter("@nivel", oBeBodega_ubicacion.Nivel));
            cmd.Parameters.Add(new SqlParameter("@indice_x", oBeBodega_ubicacion.Indice_x));
            cmd.Parameters.Add(new SqlParameter("@IdIndiceRotacion", oBeBodega_ubicacion.IdIndiceRotacion));
            cmd.Parameters.Add(new SqlParameter("@IdTipoRotacion", oBeBodega_ubicacion.IdTipoRotacion));
            cmd.Parameters.Add(new SqlParameter("@sistema", oBeBodega_ubicacion.Sistema));
            cmd.Parameters.Add(new SqlParameter("@codigo_barra", oBeBodega_ubicacion.Codigo_barra));
            cmd.Parameters.Add(new SqlParameter("@codigo_barra2", oBeBodega_ubicacion.Codigo_barra2));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeBodega_ubicacion.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeBodega_ubicacion.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeBodega_ubicacion.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeBodega_ubicacion.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@dañado", oBeBodega_ubicacion.Dañado));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeBodega_ubicacion.Activo));
            cmd.Parameters.Add(new SqlParameter("@bloqueada", oBeBodega_ubicacion.Bloqueada));
            cmd.Parameters.Add(new SqlParameter("@acepta_pallet", oBeBodega_ubicacion.Acepta_pallet));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_picking", oBeBodega_ubicacion.Ubicacion_picking));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_recepcion", oBeBodega_ubicacion.Ubicacion_recepcion));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_despacho", oBeBodega_ubicacion.Ubicacion_despacho));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_merma", oBeBodega_ubicacion.Ubicacion_merma));
            cmd.Parameters.Add(new SqlParameter("@margen_izquierdo", oBeBodega_ubicacion.Margen_izquierdo));
            cmd.Parameters.Add(new SqlParameter("@margen_derecho", oBeBodega_ubicacion.Margen_derecho));
            cmd.Parameters.Add(new SqlParameter("@margen_superior", oBeBodega_ubicacion.Margen_superior));
            cmd.Parameters.Add(new SqlParameter("@margen_inferior", oBeBodega_ubicacion.Margen_inferior));
            cmd.Parameters.Add(new SqlParameter("@orientacion_pos", oBeBodega_ubicacion.Orientacion_pos));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_virtual", oBeBodega_ubicacion.Ubicacion_virtual));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_ne", oBeBodega_ubicacion.Ubicacion_ne));
            cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeBodega_ubicacion.IdBodega));
            cmd.Parameters.Add(new SqlParameter("@IdArea", oBeBodega_ubicacion.IdArea));
            cmd.Parameters.Add(new SqlParameter("@IdSector", oBeBodega_ubicacion.IdSector));
            cmd.Parameters.Add(new SqlParameter("@posicion_x", oBeBodega_ubicacion.Posicion_x));
            cmd.Parameters.Add(new SqlParameter("@posicion_y", oBeBodega_ubicacion.Posicion_y));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_muelle", oBeBodega_ubicacion.Ubicacion_muelle));

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

    public static int Actualizar(IConfiguration config, clsBeBodega_ubicacion oBeBodega_ubicacion, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("bodega_ubicacion");
            Upd.Add("idubicacion", "@idubicacion", "F");
            Upd.Add("idtramo", "@idtramo", "F");
            Upd.Add("descripcion", "@descripcion", "F");
            Upd.Add("ancho", "@ancho", "F");
            Upd.Add("largo", "@largo", "F");
            Upd.Add("alto", "@alto", "F");
            Upd.Add("nivel", "@nivel", "F");
            Upd.Add("indice_x", "@indice_x", "F");
            Upd.Add("idindicerotacion", "@idindicerotacion", "F");
            Upd.Add("idtiporotacion", "@idtiporotacion", "F");
            Upd.Add("sistema", "@sistema", "F");
            Upd.Add("codigo_barra", "@codigo_barra", "F");
            Upd.Add("codigo_barra2", "@codigo_barra2", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("dañado", "@dañado", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("bloqueada", "@bloqueada", "F");
            Upd.Add("acepta_pallet", "@acepta_pallet", "F");
            Upd.Add("ubicacion_picking", "@ubicacion_picking", "F");
            Upd.Add("ubicacion_recepcion", "@ubicacion_recepcion", "F");
            Upd.Add("ubicacion_despacho", "@ubicacion_despacho", "F");
            Upd.Add("ubicacion_merma", "@ubicacion_merma", "F");
            Upd.Add("margen_izquierdo", "@margen_izquierdo", "F");
            Upd.Add("margen_derecho", "@margen_derecho", "F");
            Upd.Add("margen_superior", "@margen_superior", "F");
            Upd.Add("margen_inferior", "@margen_inferior", "F");
            Upd.Add("orientacion_pos", "@orientacion_pos", "F");
            Upd.Add("ubicacion_virtual", "@ubicacion_virtual", "F");
            Upd.Add("ubicacion_ne", "@ubicacion_ne", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("idarea", "@idarea", "F");
            Upd.Add("idsector", "@idsector", "F");
            Upd.Add("posicion_x", "@posicion_x", "F");
            Upd.Add("posicion_y", "@posicion_y", "F");
            Upd.Add("ubicacion_muelle", "@ubicacion_muelle", "F");
            Upd.Where("IdUbicacion = @IdUbicacion" +
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

            cmd.Parameters.Add(new SqlParameter("@IdUbicacion", oBeBodega_ubicacion.IdUbicacion));
            cmd.Parameters.Add(new SqlParameter("@IdTramo", oBeBodega_ubicacion.IdTramo));
            cmd.Parameters.Add(new SqlParameter("@descripcion", oBeBodega_ubicacion.Descripcion));
            cmd.Parameters.Add(new SqlParameter("@ancho", oBeBodega_ubicacion.Ancho));
            cmd.Parameters.Add(new SqlParameter("@largo", oBeBodega_ubicacion.Largo));
            cmd.Parameters.Add(new SqlParameter("@alto", oBeBodega_ubicacion.Alto));
            cmd.Parameters.Add(new SqlParameter("@nivel", oBeBodega_ubicacion.Nivel));
            cmd.Parameters.Add(new SqlParameter("@indice_x", oBeBodega_ubicacion.Indice_x));
            cmd.Parameters.Add(new SqlParameter("@IdIndiceRotacion", oBeBodega_ubicacion.IdIndiceRotacion));
            cmd.Parameters.Add(new SqlParameter("@IdTipoRotacion", oBeBodega_ubicacion.IdTipoRotacion));
            cmd.Parameters.Add(new SqlParameter("@sistema", oBeBodega_ubicacion.Sistema));
            cmd.Parameters.Add(new SqlParameter("@codigo_barra", oBeBodega_ubicacion.Codigo_barra));
            cmd.Parameters.Add(new SqlParameter("@codigo_barra2", oBeBodega_ubicacion.Codigo_barra2));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeBodega_ubicacion.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeBodega_ubicacion.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeBodega_ubicacion.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeBodega_ubicacion.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@dañado", oBeBodega_ubicacion.Dañado));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeBodega_ubicacion.Activo));
            cmd.Parameters.Add(new SqlParameter("@bloqueada", oBeBodega_ubicacion.Bloqueada));
            cmd.Parameters.Add(new SqlParameter("@acepta_pallet", oBeBodega_ubicacion.Acepta_pallet));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_picking", oBeBodega_ubicacion.Ubicacion_picking));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_recepcion", oBeBodega_ubicacion.Ubicacion_recepcion));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_despacho", oBeBodega_ubicacion.Ubicacion_despacho));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_merma", oBeBodega_ubicacion.Ubicacion_merma));
            cmd.Parameters.Add(new SqlParameter("@margen_izquierdo", oBeBodega_ubicacion.Margen_izquierdo));
            cmd.Parameters.Add(new SqlParameter("@margen_derecho", oBeBodega_ubicacion.Margen_derecho));
            cmd.Parameters.Add(new SqlParameter("@margen_superior", oBeBodega_ubicacion.Margen_superior));
            cmd.Parameters.Add(new SqlParameter("@margen_inferior", oBeBodega_ubicacion.Margen_inferior));
            cmd.Parameters.Add(new SqlParameter("@orientacion_pos", oBeBodega_ubicacion.Orientacion_pos));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_virtual", oBeBodega_ubicacion.Ubicacion_virtual));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_ne", oBeBodega_ubicacion.Ubicacion_ne));
            cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeBodega_ubicacion.IdBodega));
            cmd.Parameters.Add(new SqlParameter("@IdArea", oBeBodega_ubicacion.IdArea));
            cmd.Parameters.Add(new SqlParameter("@IdSector", oBeBodega_ubicacion.IdSector));
            cmd.Parameters.Add(new SqlParameter("@posicion_x", oBeBodega_ubicacion.Posicion_x));
            cmd.Parameters.Add(new SqlParameter("@posicion_y", oBeBodega_ubicacion.Posicion_y));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_muelle", oBeBodega_ubicacion.Ubicacion_muelle));


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

    public int Eliminar(IConfiguration config, clsBeBodega_ubicacion oBeBodega_ubicacion, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Bodega_ubicacion" +
             "  Where(IdUbicacion = @IdUbicacion)" +
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

            cmd.Parameters.Add(new SqlParameter("@IdUbicacion", oBeBodega_ubicacion.IdUbicacion));

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

    public static bool GetSingle(IConfiguration config, ref clsBeBodega_ubicacion pBeBodega_ubicacion)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Bodega_ubicacion" +
            " Where(IdUbicacion = @IdUbicacion)" +
            " And (IdBodega = @IdBodega)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdUbicacion", pBeBodega_ubicacion.IdUbicacion));            
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdBodega", pBeBodega_ubicacion.IdBodega));            

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeBodega_ubicacion, r);
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

    public static List<clsBeBodega_ubicacion> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeBodega_ubicacion> lreturnList = new List<clsBeBodega_ubicacion>();

        try
        {
            const string sp = "Select * FROM Bodega_ubicacion";

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

                        clsBeBodega_ubicacion vBeBodega_ubicacion = new clsBeBodega_ubicacion();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeBodega_ubicacion = new clsBeBodega_ubicacion();
                            Cargar(ref vBeBodega_ubicacion, dr);
                            lreturnList.Add(vBeBodega_ubicacion);
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

            const string sp = "Select ISNULL(Max(IdUbicacion),0) FROM Bodega_ubicacion";

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
    public static int MaxID(IConfiguration config,   SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;
        int lMax = 0;
        try
        {


            const string sp = "Select ISNULL(Max(IdUbicacion),0) FROM Bodega_ubicacion";

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

    public static int Exists(string ubicacion,
                             int IdBodega,
                             SqlConnection lConnection,
                             SqlTransaction lTransaction)
    {
        try
        {
            int lMax = 0;
            string sp = @"select IdUbicacion from bodega_ubicacion 
                     where IdBodega=@IdBodega and ";

            if (int.TryParse(ubicacion, out _))
            {
                sp += "(IdUbicacion=@ubicacion or descripcion=@ubicacion or codigo_barra=@ubicacion)";
            }
            else
            {
                sp += "(descripcion=@ubicacion Or codigo_barra=@ubicacion)";
            }

            using (SqlCommand lCommand = new SqlCommand(sp, lConnection, lTransaction))
            {
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@IdBodega", IdBodega);
                lCommand.Parameters.AddWithValue("@ubicacion", ubicacion);

                object lReturnValue = lCommand.ExecuteScalar();

                if (lReturnValue != DBNull.Value && lReturnValue != null)
                {
                    lMax = Convert.ToInt32(lReturnValue);
                }
            }

            return lMax;
        }
        catch (SqlException)
        {
            throw;
        }
    }
}
