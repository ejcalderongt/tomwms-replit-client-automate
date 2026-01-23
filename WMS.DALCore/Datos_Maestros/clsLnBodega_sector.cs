using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using System.Reflection;
using WMS.EntityCore.Datos_Maestros;


public class clsLnBodega_sector  {

		private static clsInsert Ins = new clsInsert();
		private static clsUpdate Upd = new clsUpdate();


    public static void BindingParameters(SqlCommand cmd, clsBeBodega_sector oBeBodega_sector)
    {
        if (cmd == null) throw new ArgumentNullException(nameof(cmd));
        if (oBeBodega_sector == null) throw new ArgumentNullException(nameof(oBeBodega_sector));

        // Si el SqlCommand se reutiliza, evitar acumulación de parámetros.
        cmd.Parameters.Clear();

        cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeBodega_sector.IdBodega));
        cmd.Parameters.Add(new SqlParameter("@IdSector", oBeBodega_sector.IdSector));
        cmd.Parameters.Add(new SqlParameter("@IdArea", oBeBodega_sector.IdArea));
        cmd.Parameters.Add(new SqlParameter("@sistema", oBeBodega_sector.Sistema));
        cmd.Parameters.Add(new SqlParameter("@descripcion", oBeBodega_sector.Descripcion));
        cmd.Parameters.Add(new SqlParameter("@user_agr", oBeBodega_sector.User_agr));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeBodega_sector.Fec_agr));
        cmd.Parameters.Add(new SqlParameter("@user_mod", oBeBodega_sector.User_mod));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeBodega_sector.Fec_mod));
        cmd.Parameters.Add(new SqlParameter("@activo", oBeBodega_sector.Activo));
        cmd.Parameters.Add(new SqlParameter("@alto", oBeBodega_sector.Alto));
        cmd.Parameters.Add(new SqlParameter("@largo", oBeBodega_sector.Largo));
        cmd.Parameters.Add(new SqlParameter("@ancho", oBeBodega_sector.Ancho));
        cmd.Parameters.Add(new SqlParameter("@margen_izquierdo", oBeBodega_sector.Margen_izquierdo));
        cmd.Parameters.Add(new SqlParameter("@margen_derecho", oBeBodega_sector.Margen_derecho));
        cmd.Parameters.Add(new SqlParameter("@margen_superior", oBeBodega_sector.Margen_superior));
        cmd.Parameters.Add(new SqlParameter("@margen_inferior", oBeBodega_sector.Margen_inferior));
        cmd.Parameters.Add(new SqlParameter("@Codigo", oBeBodega_sector.Codigo));
        cmd.Parameters.Add(new SqlParameter("@IdSectorIzquierda", oBeBodega_sector.IdSectorIzquierda));
        cmd.Parameters.Add(new SqlParameter("@IdSectorDerecha", oBeBodega_sector.IdSectorDerecha));
        cmd.Parameters.Add(new SqlParameter("@horizontal", oBeBodega_sector.Horizontal));
        cmd.Parameters.Add(new SqlParameter("@pos_x", oBeBodega_sector.Pos_x));
        cmd.Parameters.Add(new SqlParameter("@pos_y", oBeBodega_sector.Pos_y));
    }

    public static void Cargar(ref clsBeBodega_sector oBeBodega_sector, DataRow dr)
{
	int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
	bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
	string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
	DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
    double GetDouble(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
	{
		oBeBodega_sector.IdBodega = GetInt("IdBodega");
		oBeBodega_sector.IdSector = GetInt("IdSector");
		oBeBodega_sector.IdArea = GetInt("IdArea");
		oBeBodega_sector.Sistema = GetBool("sistema");
		oBeBodega_sector.Descripcion = GetString("descripcion");
		oBeBodega_sector.User_agr = GetString("user_agr");
		oBeBodega_sector.Fec_agr = GetDate("fec_agr");
		oBeBodega_sector.User_mod = GetString("user_mod");
		oBeBodega_sector.Fec_mod = GetDate("fec_mod");
		oBeBodega_sector.Activo = GetBool("activo");
		oBeBodega_sector.Alto = GetDouble("alto");
		oBeBodega_sector.Largo = GetDouble("largo");
		oBeBodega_sector.Ancho = GetDouble("ancho");
		oBeBodega_sector.Margen_izquierdo = GetDouble("margen_izquierdo");
		oBeBodega_sector.Margen_derecho = GetDouble("margen_derecho");
		oBeBodega_sector.Margen_superior = GetDouble("margen_superior");
		oBeBodega_sector.Margen_inferior = GetDouble("margen_inferior");
		oBeBodega_sector.Codigo = GetString("Codigo");
		oBeBodega_sector.IdSectorIzquierda = GetInt("IdSectorIzquierda");
		oBeBodega_sector.IdSectorDerecha = GetInt("IdSectorDerecha");
		oBeBodega_sector.Horizontal = GetBool("horizontal");
		oBeBodega_sector.Pos_x = GetDouble("pos_x");
		oBeBodega_sector.Pos_y = GetDouble("pos_y");
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

    public static bool Existe(int IdSector, int IdArea, int IdBodega, SqlConnection conn, SqlTransaction tx)
    {
        using var cmd = new SqlCommand("SELECT COUNT(1) FROM bodega_sector WHERE (IdSector = @IdSector and IdArea=@IdArea and IdBodega=@IdBodega) ", conn, tx);

        cmd.Parameters.AddWithValue("@IdSector", IdSector);
        cmd.Parameters.AddWithValue("@IdArea", IdArea);
        cmd.Parameters.AddWithValue("@IdBodega", IdBodega);

        var count = Convert.ToInt32(cmd.ExecuteScalar());
        return count > 0;
    }

    public static void InsertarOActualizar(List<clsBeBodega_sector> entities, SqlConnection conn, SqlTransaction tx)
    {
        foreach (var entity in entities)
        {
            if (entity.IdSector != 0)
            {
                bool existe = Existe(entity.IdSector,entity.IdArea, entity.IdBodega ,conn, tx);

                if (existe)
                    Actualizar(entity, conn, tx);
                else
                    Insertar(entity, conn, tx);
            }
        }
    }

    public static int Insertar(IConfiguration config, clsBeBodega_sector oBeBodega_sector, SqlConnection? pConection =null, SqlTransaction? pTransaction =null) {

			int rowsAffected = 0;
		SqlConnection lConnection= new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

		try {
			Ins.Init("bodega_sector");
			Ins.Add("idbodega","@idbodega","F");
			Ins.Add("idsector","@idsector","F");
			Ins.Add("idarea","@idarea","F");
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
			Ins.Add("idsectorizquierda","@idsectorizquierda","F");
			Ins.Add("idsectorderecha","@idsectorderecha","F");
			Ins.Add("horizontal","@horizontal","F");
			Ins.Add("pos_x","@pos_x","F");
			Ins.Add("pos_y","@pos_y","F");

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

			BindingParameters(cmd, oBeBodega_sector);

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

	public static int Insertar(clsBeBodega_sector oBeBodega_sector, SqlConnection? pConection = null, SqlTransaction? pTransaction = null) {

        if (oBeBodega_sector == null)
            throw new ArgumentNullException(nameof(oBeBodega_sector));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));
        int rowsAffected = 0;
	

		try {
			Ins.Init("bodega_sector");
			Ins.Add("idbodega","@idbodega","F");
			Ins.Add("idsector","@idsector","F");
			Ins.Add("idarea","@idarea","F");
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
			Ins.Add("idsectorizquierda","@idsectorizquierda","F");
			Ins.Add("idsectorderecha","@idsectorderecha","F");
			Ins.Add("horizontal","@horizontal","F");
			Ins.Add("pos_x","@pos_x","F");
			Ins.Add("pos_y","@pos_y","F");

            string sp = Ins.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                BindingParameters(cmd, oBeBodega_sector);

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

    public static int Actualizar(clsBeBodega_sector oBeBodega_sector, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {
        if (oBeBodega_sector == null)
            throw new ArgumentNullException(nameof(oBeBodega_sector));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));
        int rowsAffected = 0;
        
        try
        {

            Upd.Init("bodega_sector");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("idsector", "@idsector", "F");
            Upd.Add("idarea", "@idarea", "F");
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
            Upd.Add("idsectorizquierda", "@idsectorizquierda", "F");
            Upd.Add("idsectorderecha", "@idsectorderecha", "F");
            Upd.Add("horizontal", "@horizontal", "F");
            Upd.Add("pos_x", "@pos_x", "F");
            Upd.Add("pos_y", "@pos_y", "F");
            Upd.Where("IdBodega = @IdBodega AND IdSector = @IdSector ");

            string sp = Upd.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                BindingParameters(cmd, oBeBodega_sector);

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
    public static int Actualizar(IConfiguration config, clsBeBodega_sector oBeBodega_sector,SqlConnection? pConection = null, SqlTransaction? pTransaction=null) {

			int rowsAffected = 0;
		SqlConnection lConnection= new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

		try {

			Upd.Init("bodega_sector");
			Upd.Add("idbodega","@idbodega","F");
			Upd.Add("idsector","@idsector","F");
			Upd.Add("idarea","@idarea","F");
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
			Upd.Add("idsectorizquierda","@idsectorizquierda","F");
			Upd.Add("idsectorderecha","@idsectorderecha","F");
			Upd.Add("horizontal","@horizontal","F");
			Upd.Add("pos_x","@pos_x","F");
			Upd.Add("pos_y","@pos_y","F");
			Upd.Where("IdBodega = @IdBodega AND IdSector = @IdSector ");

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

            BindingParameters(cmd, oBeBodega_sector);

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

	public int Eliminar(IConfiguration config, clsBeBodega_sector oBeBodega_sector,SqlConnection? pConection =null, SqlTransaction? pTransaction = null) {

		SqlConnection lConnection= new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

		try {
			const string sp = (" Delete from Bodega_sector" + 
			 "  Where(IdBodega = @IdBodega)" + 
			 "  And (IdSector = @IdSector)");

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

			cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeBodega_sector.IdBodega));

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
			const string sp = "Select * FROM Bodega_sector";
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

	public static bool GetSingle(IConfiguration config, ref clsBeBodega_sector pBeBodega_sector) { 
		
		SqlConnection lConnection= new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

		try
		{
		
			const string sp = "Select * FROM Bodega_sector" + 
			" Where(IdBodega = @IdBodega)" +  
			" And (IdSector = @IdSector)";

				lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

			SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) {CommandType=CommandType.Text};
			SqlDataAdapter dad = new SqlDataAdapter(cmd);

			dad.SelectCommand.Parameters.Add(new SqlParameter("@IdBodega", pBeBodega_sector.IdBodega));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@IdSector", pBeBodega_sector.IdSector));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@IdArea", pBeBodega_sector.IdArea));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@sistema", pBeBodega_sector.Sistema));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@descripcion", pBeBodega_sector.Descripcion));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@user_agr", pBeBodega_sector.User_agr));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_agr", pBeBodega_sector.Fec_agr));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@user_mod", pBeBodega_sector.User_mod));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_mod", pBeBodega_sector.Fec_mod));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@activo", pBeBodega_sector.Activo));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@alto", pBeBodega_sector.Alto));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@largo", pBeBodega_sector.Largo));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@ancho", pBeBodega_sector.Ancho));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@margen_izquierdo", pBeBodega_sector.Margen_izquierdo));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@margen_derecho", pBeBodega_sector.Margen_derecho));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@margen_superior", pBeBodega_sector.Margen_superior));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@margen_inferior", pBeBodega_sector.Margen_inferior));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@Codigo", pBeBodega_sector.Codigo));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@IdSectorIzquierda", pBeBodega_sector.IdSectorIzquierda));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@IdSectorDerecha", pBeBodega_sector.IdSectorDerecha));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@horizontal", pBeBodega_sector.Horizontal));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@pos_x", pBeBodega_sector.Pos_x));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@pos_y", pBeBodega_sector.Pos_y));

			DataTable dt = new DataTable();
			dad.Fill(dt);

			lTransaction.Commit();

			if (dt.Rows.Count == 1) {
				DataRow r;
				r = dt.Rows[0];
				Cargar(ref pBeBodega_sector,r);
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

	public static List<clsBeBodega_sector> GetAll(IConfiguration config)
		{
		
		 SqlTransaction? lTransaction = null;
		List<clsBeBodega_sector> lreturnList = new List<clsBeBodega_sector>();
		
		try
		{
			const string sp = "Select * FROM Bodega_sector";

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
		
					clsBeBodega_sector vBeBodega_sector = new clsBeBodega_sector();

							foreach (DataRow dr in lDataTable.Rows)
						{
						vBeBodega_sector = new clsBeBodega_sector();
						Cargar(ref vBeBodega_sector, dr);
						lreturnList.Add(vBeBodega_sector);
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
		
			const string sp = "Select ISNULL(Max(IdBodega),0) FROM Bodega_sector";

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
		
		
			const string sp = "Select ISNULL(Max(IdBodega),0) FROM Bodega_sector";

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
