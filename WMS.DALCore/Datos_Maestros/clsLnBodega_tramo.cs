using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using System.Reflection;
using WMS.EntityCore.Datos_Maestros;

public class clsLnBodega_tramo  {

		private static clsInsert Ins = new clsInsert();
		private static clsUpdate Upd = new clsUpdate();


    public static void BindingParameters(SqlCommand cmd, clsBeBodega_tramo oBeBodega_tramo)
    {
        if (cmd == null) throw new ArgumentNullException(nameof(cmd));
        if (oBeBodega_tramo == null) throw new ArgumentNullException(nameof(oBeBodega_tramo));

        // Si el SqlCommand se reutiliza, evitar acumulación de parámetros.
        cmd.Parameters.Clear();

        cmd.Parameters.Add(new SqlParameter("@IdTramo", oBeBodega_tramo.IdTramo));
        cmd.Parameters.Add(new SqlParameter("@IdSector", oBeBodega_tramo.IdSector));
        cmd.Parameters.Add(new SqlParameter("@sistema", oBeBodega_tramo.Sistema));
        cmd.Parameters.Add(new SqlParameter("@descripcion", oBeBodega_tramo.Descripcion));
        cmd.Parameters.Add(new SqlParameter("@user_agr", oBeBodega_tramo.User_agr));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeBodega_tramo.Fec_agr));
        cmd.Parameters.Add(new SqlParameter("@user_mod", oBeBodega_tramo.User_mod));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeBodega_tramo.Fec_mod));
        cmd.Parameters.Add(new SqlParameter("@activo", oBeBodega_tramo.Activo));
        cmd.Parameters.Add(new SqlParameter("@alto", oBeBodega_tramo.Alto));
        cmd.Parameters.Add(new SqlParameter("@largo", oBeBodega_tramo.Largo));
        cmd.Parameters.Add(new SqlParameter("@ancho", oBeBodega_tramo.Ancho));
        cmd.Parameters.Add(new SqlParameter("@margen_izquierdo", oBeBodega_tramo.Margen_izquierdo));
        cmd.Parameters.Add(new SqlParameter("@margen_derecho", oBeBodega_tramo.Margen_derecho));
        cmd.Parameters.Add(new SqlParameter("@margen_superior", oBeBodega_tramo.Margen_superior));
        cmd.Parameters.Add(new SqlParameter("@margen_inferior", oBeBodega_tramo.Margen_inferior));
        cmd.Parameters.Add(new SqlParameter("@Codigo", oBeBodega_tramo.Codigo));
        cmd.Parameters.Add(new SqlParameter("@Indice_x", oBeBodega_tramo.Indice_x));
        cmd.Parameters.Add(new SqlParameter("@Orientacion", oBeBodega_tramo.Orientacion));
        cmd.Parameters.Add(new SqlParameter("@IdTipoProductoDefault", oBeBodega_tramo.IdTipoProductoDefault));
        cmd.Parameters.Add(new SqlParameter("@IdFontEnc", oBeBodega_tramo.IdFontEnc));
        cmd.Parameters.Add(new SqlParameter("@IdTipoRack", oBeBodega_tramo.IdTipoRack));
        cmd.Parameters.Add(new SqlParameter("@es_rack", oBeBodega_tramo.Es_rack));
        cmd.Parameters.Add(new SqlParameter("@Horizontal", oBeBodega_tramo.Horizontal));
        cmd.Parameters.Add(new SqlParameter("@IdArea", oBeBodega_tramo.IdArea));
        cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeBodega_tramo.IdBodega));
        cmd.Parameters.Add(new SqlParameter("@orden_descendente", oBeBodega_tramo.Orden_descendente));
    }

    public static void Cargar(ref clsBeBodega_tramo oBeBodega_tramo, DataRow dr)
{
	int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
	bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
	string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
	DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
    double GetDouble(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
	{
		oBeBodega_tramo.IdTramo = GetInt("IdTramo");
		oBeBodega_tramo.IdSector = GetInt("IdSector");
		oBeBodega_tramo.Sistema = GetBool("sistema");
		oBeBodega_tramo.Descripcion = GetString("descripcion");
		oBeBodega_tramo.User_agr = GetString("user_agr");
		oBeBodega_tramo.Fec_agr = GetDate("fec_agr");
		oBeBodega_tramo.User_mod = GetString("user_mod");
		oBeBodega_tramo.Fec_mod = GetDate("fec_mod");
		oBeBodega_tramo.Activo = GetBool("activo");
		oBeBodega_tramo.Alto = GetDouble("alto");
		oBeBodega_tramo.Largo = GetDouble("largo");
		oBeBodega_tramo.Ancho = GetDouble("ancho");
		oBeBodega_tramo.Margen_izquierdo = GetDouble("margen_izquierdo");
		oBeBodega_tramo.Margen_derecho = GetDouble("margen_derecho");
		oBeBodega_tramo.Margen_superior = GetDouble("margen_superior");
		oBeBodega_tramo.Margen_inferior = GetDouble("margen_inferior");
		oBeBodega_tramo.Codigo = GetString("Codigo");
		oBeBodega_tramo.Indice_x = GetInt("Indice_x");
		oBeBodega_tramo.Orientacion = GetInt("Orientacion");
		oBeBodega_tramo.IdTipoProductoDefault = GetInt("IdTipoProductoDefault");
		oBeBodega_tramo.IdFontEnc = GetInt("IdFontEnc");
		oBeBodega_tramo.IdTipoRack = GetInt("IdTipoRack");
		oBeBodega_tramo.Es_rack = GetBool("es_rack");
		oBeBodega_tramo.Horizontal = GetBool("Horizontal");
		oBeBodega_tramo.IdArea = GetInt("IdArea");
		oBeBodega_tramo.IdBodega = GetInt("IdBodega");
		oBeBodega_tramo.Orden_descendente = GetBool("orden_descendente");
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


    public static bool Existe(int IdTramo, int IdBodega, SqlConnection conn, SqlTransaction tx)
    {
        using var cmd = new SqlCommand("SELECT COUNT(1) FROM bodega_tramo WHERE (IdTramo = @IdTramo and IdBodega=@IdBodega) ", conn, tx);

        cmd.Parameters.AddWithValue("@IdTramo", IdTramo);
        cmd.Parameters.AddWithValue("@IdBodega", IdBodega);

        var count = Convert.ToInt32(cmd.ExecuteScalar());
        return count > 0;
    }

    public static void InsertarOActualizar(List<clsBeBodega_tramo> entities, SqlConnection conn, SqlTransaction tx)
    {
        foreach (var entity in entities)
        {
            if (entity.IdSector != 0)
            {
                bool existe = Existe(entity.IdTramo, entity.IdBodega, conn, tx);

                if (existe)
                    Actualizar(entity, conn, tx);
                else
                    Insertar(entity, conn, tx);
            }
        }
    }

    public static int Insertar(clsBeBodega_tramo oBeBodega_tramo, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        if (oBeBodega_tramo == null)
            throw new ArgumentNullException(nameof(oBeBodega_tramo));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

        try
        {
            Ins.Init("bodega_tramo");
            Ins.Add("idtramo", "@idtramo", "F");
            Ins.Add("idsector", "@idsector", "F");
            Ins.Add("sistema", "@sistema", "F");
            Ins.Add("descripcion", "@descripcion", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("alto", "@alto", "F");
            Ins.Add("largo", "@largo", "F");
            Ins.Add("ancho", "@ancho", "F");
            Ins.Add("margen_izquierdo", "@margen_izquierdo", "F");
            Ins.Add("margen_derecho", "@margen_derecho", "F");
            Ins.Add("margen_superior", "@margen_superior", "F");
            Ins.Add("margen_inferior", "@margen_inferior", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("indice_x", "@indice_x", "F");
            Ins.Add("orientacion", "@orientacion", "F");
            Ins.Add("idtipoproductodefault", "@idtipoproductodefault", "F");
            Ins.Add("idfontenc", "@idfontenc", "F");
            Ins.Add("idtiporack", "@idtiporack", "F");
            Ins.Add("es_rack", "@es_rack", "F");
            Ins.Add("horizontal", "@horizontal", "F");
            Ins.Add("idarea", "@idarea", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("orden_descendente", "@orden_descendente", "F");

            string sp = Ins.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                BindingParameters(cmd, oBeBodega_tramo);

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
    public static int Insertar(IConfiguration config, clsBeBodega_tramo oBeBodega_tramo, SqlConnection? pConection =null, SqlTransaction? pTransaction =null) 
	{

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;
        int rowsAffected = 0;
	
		try {
			Ins.Init("bodega_tramo");
			Ins.Add("idtramo","@idtramo","F");
			Ins.Add("idsector","@idsector","F");
			Ins.Add("sistema","@sistema","F");
			Ins.Add("descripcion","@descripcion","F");
			Ins.Add("user_agr","@user_agr","F");
			Ins.Add("fec_agr","@fec_agr","F");
			Ins.Add("user_mod","@user_mod","F");
			Ins.Add("fec_mod","@fec_mod","F");
			Ins.Add("activo","@activo","F");
			Ins.Add("alto","@alto","F");
			Ins.Add("largo","@largo","F");
			Ins.Add("ancho","@ancho","F");
			Ins.Add("margen_izquierdo","@margen_izquierdo","F");
			Ins.Add("margen_derecho","@margen_derecho","F");
			Ins.Add("margen_superior","@margen_superior","F");
			Ins.Add("margen_inferior","@margen_inferior","F");
			Ins.Add("codigo","@codigo","F");
			Ins.Add("indice_x","@indice_x","F");
			Ins.Add("orientacion","@orientacion","F");
			Ins.Add("idtipoproductodefault","@idtipoproductodefault","F");
			Ins.Add("idfontenc","@idfontenc","F");
			Ins.Add("idtiporack","@idtiporack","F");
			Ins.Add("es_rack","@es_rack","F");
			Ins.Add("horizontal","@horizontal","F");
			Ins.Add("idarea","@idarea","F");
			Ins.Add("idbodega","@idbodega","F");
			Ins.Add("orden_descendente","@orden_descendente","F");

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

			BindingParameters(cmd, oBeBodega_tramo);

            rowsAffected = cmd.ExecuteNonQuery();

			cmd.Dispose();

			if (!Es_Transaccion_Remota)
				if (lTransaction != null)
					lTransaction.Commit();


		}
		catch (SqlException ex1) {
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

	public static int Insertar(IConfiguration config, clsBeBodega_tramo oBeBodega_tramo) {

			int rowsAffected = 0;
		SqlConnection lConnection= new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

		try {
			Ins.Init("bodega_tramo");
			Ins.Add("idtramo","@idtramo","F");
			Ins.Add("idsector","@idsector","F");
			Ins.Add("sistema","@sistema","F");
			Ins.Add("descripcion","@descripcion","F");
			Ins.Add("user_agr","@user_agr","F");
			Ins.Add("fec_agr","@fec_agr","F");
			Ins.Add("user_mod","@user_mod","F");
			Ins.Add("fec_mod","@fec_mod","F");
			Ins.Add("activo","@activo","F");
			Ins.Add("alto","@alto","F");
			Ins.Add("largo","@largo","F");
			Ins.Add("ancho","@ancho","F");
			Ins.Add("margen_izquierdo","@margen_izquierdo","F");
			Ins.Add("margen_derecho","@margen_derecho","F");
			Ins.Add("margen_superior","@margen_superior","F");
			Ins.Add("margen_inferior","@margen_inferior","F");
			Ins.Add("codigo","@codigo","F");
			Ins.Add("indice_x","@indice_x","F");
			Ins.Add("orientacion","@orientacion","F");
			Ins.Add("idtipoproductodefault","@idtipoproductodefault","F");
			Ins.Add("idfontenc","@idfontenc","F");
			Ins.Add("idtiporack","@idtiporack","F");
			Ins.Add("es_rack","@es_rack","F");
			Ins.Add("horizontal","@horizontal","F");
			Ins.Add("idarea","@idarea","F");
			Ins.Add("idbodega","@idbodega","F");
			Ins.Add("orden_descendente","@orden_descendente","F");

			string sp = Ins.SQL();

			SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

				lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
				cmd = new SqlCommand(sp, lConnection, lTransaction);

            BindingParameters(cmd, oBeBodega_tramo);

            rowsAffected = cmd.ExecuteNonQuery();

			if (lTransaction != null)
				lTransaction.Commit();

		}
		catch (SqlException ex1) {
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

    public static int Actualizar(clsBeBodega_tramo oBeBodega_tramo, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {
        if (oBeBodega_tramo == null)
            throw new ArgumentNullException(nameof(oBeBodega_tramo));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;
        
        try
        {

            Upd.Init("bodega_tramo");
            Upd.Add("idtramo", "@idtramo", "F");
            Upd.Add("idsector", "@idsector", "F");
            Upd.Add("sistema", "@sistema", "F");
            Upd.Add("descripcion", "@descripcion", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("alto", "@alto", "F");
            Upd.Add("largo", "@largo", "F");
            Upd.Add("ancho", "@ancho", "F");
            Upd.Add("margen_izquierdo", "@margen_izquierdo", "F");
            Upd.Add("margen_derecho", "@margen_derecho", "F");
            Upd.Add("margen_superior", "@margen_superior", "F");
            Upd.Add("margen_inferior", "@margen_inferior", "F");
            Upd.Add("codigo", "@codigo", "F");
            Upd.Add("indice_x", "@indice_x", "F");
            Upd.Add("orientacion", "@orientacion", "F");
            Upd.Add("idtipoproductodefault", "@idtipoproductodefault", "F");
            Upd.Add("idfontenc", "@idfontenc", "F");
            Upd.Add("idtiporack", "@idtiporack", "F");
            Upd.Add("es_rack", "@es_rack", "F");
            Upd.Add("horizontal", "@horizontal", "F");
            Upd.Add("idarea", "@idarea", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("orden_descendente", "@orden_descendente", "F");
            Upd.Where("IdTramo = @IdTramo AND IdBodega = @IdBodega ");

            string sp = Upd.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                BindingParameters(cmd, oBeBodega_tramo);

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
    public static int Actualizar(IConfiguration config, clsBeBodega_tramo oBeBodega_tramo,SqlConnection? pConection = null, SqlTransaction? pTransaction=null) {

			int rowsAffected = 0;
		SqlConnection lConnection= new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

		try {

			Upd.Init("bodega_tramo");
			Upd.Add("idtramo","@idtramo","F");
			Upd.Add("idsector","@idsector","F");
			Upd.Add("sistema","@sistema","F");
			Upd.Add("descripcion","@descripcion","F");
			Upd.Add("user_agr","@user_agr","F");
			Upd.Add("fec_agr","@fec_agr","F");
			Upd.Add("user_mod","@user_mod","F");
			Upd.Add("fec_mod","@fec_mod","F");
			Upd.Add("activo","@activo","F");
			Upd.Add("alto","@alto","F");
			Upd.Add("largo","@largo","F");
			Upd.Add("ancho","@ancho","F");
			Upd.Add("margen_izquierdo","@margen_izquierdo","F");
			Upd.Add("margen_derecho","@margen_derecho","F");
			Upd.Add("margen_superior","@margen_superior","F");
			Upd.Add("margen_inferior","@margen_inferior","F");
			Upd.Add("codigo","@codigo","F");
			Upd.Add("indice_x","@indice_x","F");
			Upd.Add("orientacion","@orientacion","F");
			Upd.Add("idtipoproductodefault","@idtipoproductodefault","F");
			Upd.Add("idfontenc","@idfontenc","F");
			Upd.Add("idtiporack","@idtiporack","F");
			Upd.Add("es_rack","@es_rack","F");
			Upd.Add("horizontal","@horizontal","F");
			Upd.Add("idarea","@idarea","F");
			Upd.Add("idbodega","@idbodega","F");
			Upd.Add("orden_descendente","@orden_descendente","F");
			Upd.Where("IdTramo = @IdTramo AND IdBodega = @IdBodega ");

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

            BindingParameters(cmd, oBeBodega_tramo);

            rowsAffected = cmd.ExecuteNonQuery();

			if (!Es_Transaccion_Remota)
				if (lTransaction != null)
					lTransaction.Commit();


		}
		catch (SqlException ex1) {
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
			if(lConnection.State == ConnectionState.Open) lConnection.Close();
			if (lConnection != null) lConnection.Dispose();
			if (lTransaction != null) lTransaction.Dispose();
		}
			return rowsAffected;
	}

	public int Eliminar(IConfiguration config, clsBeBodega_tramo oBeBodega_tramo,SqlConnection? pConection =null, SqlTransaction? pTransaction = null) {

		SqlConnection lConnection= new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

		try {
			const string sp = (" Delete from Bodega_tramo" + 
			 "  Where(IdTramo = @IdTramo)" + 
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

			cmd.Parameters.Add(new SqlParameter("@IdTramo", oBeBodega_tramo.IdTramo));

			int rowsAffected = cmd.ExecuteNonQuery();

			if (!Es_Transaccion_Remota)
				if (lTransaction != null)
					lTransaction.Commit();

			return rowsAffected;

		}
		catch (SqlException ex1) {
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
			if(lConnection.State == ConnectionState.Open) lConnection.Close();
			if (lConnection != null) lConnection.Dispose();
			if (lTransaction != null) lTransaction.Dispose();
		}
	}

	public DataTable Listar(IConfiguration config) {

		SqlConnection lConnection= new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

		try {
			const string sp = "Select * FROM Bodega_tramo";
			lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
			SqlCommand cmd = new SqlCommand(sp,lConnection,lTransaction) { CommandType = CommandType.Text };
			SqlDataAdapter dad = new SqlDataAdapter(cmd);
			DataTable dt = new DataTable();
			dad.Fill(dt);

			lTransaction.Commit();

			return dt;

		}
		catch (SqlException ex1) {
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
			if(lConnection.State == ConnectionState.Open) lConnection.Close();
			if (lConnection != null) lConnection.Dispose();
			if (lTransaction != null) lTransaction.Dispose();
		}
	}

	public static bool GetSingle(IConfiguration config, ref clsBeBodega_tramo pBeBodega_tramo) { 
		
		SqlConnection lConnection= new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

		try
		{
		
			const string sp = "Select * FROM Bodega_tramo" + 
			" Where(IdTramo = @IdTramo)" +  
			" And (IdBodega = @IdBodega)";

				lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

			SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) {CommandType=CommandType.Text};
			SqlDataAdapter dad = new SqlDataAdapter(cmd);

			dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTramo", pBeBodega_tramo.IdTramo));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@IdSector", pBeBodega_tramo.IdSector));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@sistema", pBeBodega_tramo.Sistema));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@descripcion", pBeBodega_tramo.Descripcion));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@user_agr", pBeBodega_tramo.User_agr));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_agr", pBeBodega_tramo.Fec_agr));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@user_mod", pBeBodega_tramo.User_mod));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_mod", pBeBodega_tramo.Fec_mod));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@activo", pBeBodega_tramo.Activo));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@alto", pBeBodega_tramo.Alto));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@largo", pBeBodega_tramo.Largo));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@ancho", pBeBodega_tramo.Ancho));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@margen_izquierdo", pBeBodega_tramo.Margen_izquierdo));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@margen_derecho", pBeBodega_tramo.Margen_derecho));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@margen_superior", pBeBodega_tramo.Margen_superior));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@margen_inferior", pBeBodega_tramo.Margen_inferior));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@Codigo", pBeBodega_tramo.Codigo));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@Indice_x", pBeBodega_tramo.Indice_x));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@Orientacion", pBeBodega_tramo.Orientacion));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTipoProductoDefault", pBeBodega_tramo.IdTipoProductoDefault));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@IdFontEnc", pBeBodega_tramo.IdFontEnc));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTipoRack", pBeBodega_tramo.IdTipoRack));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@es_rack", pBeBodega_tramo.Es_rack));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@Horizontal", pBeBodega_tramo.Horizontal));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@IdArea", pBeBodega_tramo.IdArea));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@IdBodega", pBeBodega_tramo.IdBodega));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@orden_descendente", pBeBodega_tramo.Orden_descendente));

			DataTable dt = new DataTable();
			dad.Fill(dt);

			lTransaction.Commit();

			if (dt.Rows.Count == 1) {
				DataRow r;
				r = dt.Rows[0];
				Cargar(ref pBeBodega_tramo,r);
			return true;
			}

		}
		catch (SqlException ex1) {
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
			if(lConnection.State == ConnectionState.Open) lConnection.Close();
			if (lConnection != null) lConnection.Dispose();
			if (lTransaction != null) lTransaction.Dispose();
		}
		return false;

	}

	public static List<clsBeBodega_tramo> GetAll(IConfiguration config)
		{
		
		 SqlTransaction? lTransaction = null;
		List<clsBeBodega_tramo> lreturnList = new List<clsBeBodega_tramo>();
		
		try
		{
			const string sp = "Select * FROM Bodega_tramo";

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

						clsBeBodega_tramo vBeBodega_tramo = new clsBeBodega_tramo();

						foreach (DataRow dr in lDataTable.Rows)
						{
							vBeBodega_tramo = new clsBeBodega_tramo();
							Cargar(ref vBeBodega_tramo, dr);
							lreturnList.Add(vBeBodega_tramo);
						}

						lTransaction.Commit();
					}

					lConnection.Close();

				}

			}

			return lreturnList;

			}
			catch (SqlException ex1) {
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

	public static int MaxID(IConfiguration config) {
		
		SqlTransaction? lTransaction = null;
		
		try{
		
			int lMax = 0;
		
			const string sp = "Select ISNULL(Max(IdTramo),0) FROM Bodega_tramo";

			using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
			{
				lConnection.Open();

				using (lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)) 
				{
					using (SqlCommand lCommand = new SqlCommand(sp, lConnection) {CommandType=CommandType.Text})
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
		catch (SqlException ex1) {
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
	public static int MaxID(IConfiguration config, SqlConnection? pConection = null, SqlTransaction? pTransaction = null) {
		
		SqlConnection lConnection= new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;
			int lMax = 0;
		try{
		
		
			const string sp = "Select ISNULL(Max(IdTramo),0) FROM Bodega_tramo";

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
		catch (SqlException ex1) {
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

}
