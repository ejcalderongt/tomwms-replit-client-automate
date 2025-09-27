using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using WMS.EntityCore.Reset_Password;
using WMS.EntityCore.Propietario;


public class clsLnReset_portal_ux  {

		private static clsInsert Ins = new clsInsert();
		private static clsUpdate Upd = new clsUpdate();

public static void Cargar(ref clsBeReset_portal_ux oBeReset_portal_ux, DataRow dr)
{
	int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
	bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
	string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
	DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
	
	try
	{
		oBeReset_portal_ux.IdReset = GetInt("IdReset");
		oBeReset_portal_ux.IdPropietario = GetInt("IdPropietario");
		oBeReset_portal_ux.Token = GetString("Token");
		oBeReset_portal_ux.Tiempo_Expira = GetDate("Tiempo_Expira");
		oBeReset_portal_ux.Used = GetBool("Used");
		oBeReset_portal_ux.Fec_agr = GetDate("Fec_agr");
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

    public static void BindingLogParameters(SqlCommand cmd, clsBeReset_portal_ux oBeReset_portal_ux)
    {

        cmd.Parameters.Add(new SqlParameter("@IdReset", oBeReset_portal_ux.IdReset));
        cmd.Parameters.Add(new SqlParameter("@IdPropietario", oBeReset_portal_ux.IdPropietario));
        cmd.Parameters.Add(new SqlParameter("@Token", oBeReset_portal_ux.Token));
        cmd.Parameters.Add(new SqlParameter("@Tiempo_Expira", oBeReset_portal_ux.Tiempo_Expira));
        cmd.Parameters.Add(new SqlParameter("@Used", oBeReset_portal_ux.Used));
        cmd.Parameters.Add(new SqlParameter("@Fec_agr", oBeReset_portal_ux.Fec_agr));
    }

    public static int Insertar(IConfiguration config, clsBeReset_portal_ux oBeReset_portal_ux, SqlConnection? pConection =null, SqlTransaction? pTransaction =null) {

			int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

		try {
			Ins.Init("reset_portal_ux");
			Ins.Add("idreset","@idreset","F");
			Ins.Add("idpropietario","@idpropietario","F");
			Ins.Add("token","@token","F");
			Ins.Add("tiempo_expira","@tiempo_expira","F");
			Ins.Add("used","@used","F");
			Ins.Add("fec_agr","@fec_agr","F");

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

			BindingLogParameters(cmd, oBeReset_portal_ux);

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

	public static int Insertar(IConfiguration config, clsBeReset_portal_ux oBeReset_portal_ux) {

			int rowsAffected = 0;
		SqlConnection lConnection= new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

		try {
			Ins.Init("reset_portal_ux");
			Ins.Add("idreset","@idreset","F");
			Ins.Add("idpropietario","@idpropietario","F");
			Ins.Add("token","@token","F");
			Ins.Add("tiempo_expira","@tiempo_expira","F");
			Ins.Add("used","@used","F");
			Ins.Add("fec_agr","@fec_agr","F");

			string sp = Ins.SQL();

			SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

				lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
				cmd = new SqlCommand(sp, lConnection, lTransaction);

            BindingLogParameters(cmd, oBeReset_portal_ux);

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

	public static int Actualizar(IConfiguration config, clsBeReset_portal_ux oBeReset_portal_ux,SqlConnection? pConection = null, SqlTransaction? pTransaction=null) {

			int rowsAffected = 0;
		SqlConnection lConnection= new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

		try {

			Upd.Init("reset_portal_ux");
			Upd.Add("idreset","@idreset","F");
			Upd.Add("idpropietario","@idpropietario","F");
			Upd.Add("token","@token","F");
			Upd.Add("tiempo_expira","@tiempo_expira","F");
			Upd.Add("used","@used","F");
			Upd.Add("fec_agr","@fec_agr","F");
			Upd.Where("IdReset = @IdReset");

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

            BindingLogParameters(cmd, oBeReset_portal_ux);

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

	public int Eliminar(IConfiguration config, clsBeReset_portal_ux oBeReset_portal_ux,SqlConnection? pConection =null, SqlTransaction? pTransaction = null) {

		SqlConnection lConnection= new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

		try {
			const string sp = (" Delete from Reset_portal_ux" + 
			 "  Where(IdReset = @IdReset)");

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

			cmd.Parameters.Add(new SqlParameter("@IdReset", oBeReset_portal_ux.IdReset));

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
			const string sp = "Select * FROM Reset_portal_ux";
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

	public static bool GetSingle(IConfiguration config, ref clsBeReset_portal_ux pBeReset_portal_ux) { 
		
		SqlConnection lConnection= new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

		try
		{
		
			const string sp = "Select * FROM Reset_portal_ux" + 
			" Where(IdReset = @IdReset)";

				lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

			SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) {CommandType=CommandType.Text};
			SqlDataAdapter dad = new SqlDataAdapter(cmd);

			dad.SelectCommand.Parameters.Add(new SqlParameter("@IdReset", pBeReset_portal_ux.IdReset));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPropietario", pBeReset_portal_ux.IdPropietario));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@Token", pBeReset_portal_ux.Token));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@Tiempo_Expira", pBeReset_portal_ux.Tiempo_Expira));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@Used", pBeReset_portal_ux.Used));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@Fec_agr", pBeReset_portal_ux.Fec_agr));

			DataTable dt = new DataTable();
			dad.Fill(dt);

			lTransaction.Commit();

			if (dt.Rows.Count == 1) {
				DataRow r;
				r = dt.Rows[0];
				Cargar(ref pBeReset_portal_ux,r);
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

	public static List<clsBeReset_portal_ux> GetAll(IConfiguration config)
		{
		
		 SqlTransaction? lTransaction = null;
		List<clsBeReset_portal_ux> lreturnList = new List<clsBeReset_portal_ux>();
		
		try
		{
			const string sp = "Select * FROM Reset_portal_ux";

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
		
					clsBeReset_portal_ux vBeReset_portal_ux = new clsBeReset_portal_ux();

							foreach (DataRow dr in lDataTable.Rows)
						{
						vBeReset_portal_ux = new clsBeReset_portal_ux();
						Cargar(ref vBeReset_portal_ux, dr);
						lreturnList.Add(vBeReset_portal_ux);
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
		
			const string sp = "Select ISNULL(Max(IdReset),0) FROM Reset_portal_ux";

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
		
		
			const string sp = "Select ISNULL(Max(IdReset),0) FROM Reset_portal_ux";

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
                lMax = Convert.ToInt32(lreturnValue);
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

    public static bool GetSingle_By_Token(IConfiguration config, string pToken,ref clsBeReset_portal_ux pBeReset_portal_ux, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

			const string sp = "Select * FROM Reset_portal_ux Where(Token = @Token)";

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

            SqlDataAdapter dad = new SqlDataAdapter(cmd);
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Token", pToken));
            
            DataTable dt = new DataTable();
            dad.Fill(dt);

            if (!Es_Transaccion_Remota)
                if (lTransaction != null)
                    lTransaction.Commit();


			if (dt.Rows.Count == 1)
			{
				DataRow r;
				r = dt.Rows[0];
				Cargar(ref pBeReset_portal_ux, r);
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

        return false;

    }

    public static bool EsTokenValido(IConfiguration config, string Token)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Reset_portal_ux" +
            " Where(token = @token)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);
			
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Token",Token));
          
            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
				clsBeReset_portal_ux pPortalUx = new clsBeReset_portal_ux();

                Cargar(ref pPortalUx, r);

                if (pPortalUx.Token == null)
                {
                    return false;
                }

                if (pPortalUx.Used)
                {   
                    return false;
                }

                if (pPortalUx.Tiempo_Expira < DateTime.UtcNow)
                {   
                    return false;
                }

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

    public static int Cerrar_Token(IConfiguration config, clsBeReset_portal_ux oBeReset_portal_ux, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("reset_portal_ux");
            Upd.Add("used", "@used", "F");
            Upd.Where("IdReset = @IdReset and token=@token");

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

            BindingLogParameters(cmd, oBeReset_portal_ux);

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

}
