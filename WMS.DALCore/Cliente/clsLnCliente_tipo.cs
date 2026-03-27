using System.Reflection;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WMSWebAPI.Be;

public class clsLnCliente_tipo  {

		private static clsInsert Ins = new clsInsert();
		private static clsUpdate Upd = new clsUpdate();


    public static void BindingTipoCliente(SqlCommand cmd, clsBeCliente_tipo oBeCliente_tipo)
    {
        cmd.Parameters.Add(new SqlParameter("@IdTipoCliente", oBeCliente_tipo.IdTipoCliente));
        cmd.Parameters.Add(new SqlParameter("@IdPropietario", oBeCliente_tipo.IdPropietario));
        cmd.Parameters.Add(new SqlParameter("@NombreTipoCliente", oBeCliente_tipo.NombreTipoCliente));
        cmd.Parameters.Add(new SqlParameter("@Activo", oBeCliente_tipo.Activo));
        cmd.Parameters.Add(new SqlParameter("@user_agr", oBeCliente_tipo.User_agr));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeCliente_tipo.Fec_agr));
        cmd.Parameters.Add(new SqlParameter("@user_mod", oBeCliente_tipo.User_mod));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeCliente_tipo.Fec_mod));
    }


    public static void InsertarOActualizar(List<clsBeCliente_tipo> entities, SqlConnection conn, SqlTransaction tx)
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

                bool existe = Existe(entity.IdTipoCliente, conn, tx);

                if (existe)
                    Actualizar(entity, conn, tx);
                else
                    Insertar(entity, conn, tx);
            }
        }
        catch (SqlException ex)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
        }
    }


    public static bool Existe(int IdTipoCliente, SqlConnection conn, SqlTransaction? tx = null)
    {
        const string sql = "SELECT COUNT(1) FROM cliente_tipo WHERE IdTipoCliente = @IdTipoCliente";

        using SqlCommand cmd = new(sql, conn, tx);
        cmd.Parameters.AddWithValue("@IdTipoCliente", IdTipoCliente);
        int count = Convert.ToInt32(cmd.ExecuteScalar());

        return count > 0;
    }



    public static int Actualizar(clsBeCliente_tipo oBeCliente_tipo, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        if (oBeCliente_tipo == null)
            throw new ArgumentNullException(nameof(oBeCliente_tipo));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

        try
        {

            Upd.Init("cliente_tipo");
            Upd.Add("idtipocliente", "@idtipocliente", "F");
            Upd.Add("idpropietario", "@idpropietario", "F");
            Upd.Add("nombretipocliente", "@nombretipocliente", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Where("IdTipoCliente = @IdTipoCliente");

            string sp = Upd.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                BindingTipoCliente(cmd, oBeCliente_tipo);

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



    public static void Cargar(ref clsBeCliente_tipo oBeCliente_tipo, DataRow dr)
{
	int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
	bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
	string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
	DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
	
	try
	{
		oBeCliente_tipo.IdTipoCliente = GetInt("IdTipoCliente");
		oBeCliente_tipo.IdPropietario = GetInt("IdPropietario");
		oBeCliente_tipo.NombreTipoCliente = GetString("NombreTipoCliente");
		oBeCliente_tipo.Activo = GetBool("Activo");
		oBeCliente_tipo.User_agr = GetString("user_agr");
		oBeCliente_tipo.Fec_agr = GetDate("fec_agr");
		oBeCliente_tipo.User_mod = GetString("user_mod");
		oBeCliente_tipo.Fec_mod = GetDate("fec_mod");
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

	public static int Insertar(clsBeCliente_tipo oBeCliente_tipo, SqlConnection? pConection =null, SqlTransaction? pTransaction =null) {

        if (oBeCliente_tipo == null)
            throw new ArgumentNullException(nameof(oBeCliente_tipo));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        int rowsAffected = 0;

        try {
			Ins.Init("cliente_tipo");
			Ins.Add("idtipocliente","@idtipocliente","F");
			Ins.Add("idpropietario","@idpropietario","F");
			Ins.Add("nombretipocliente","@nombretipocliente","F");
			Ins.Add("activo","@activo","F");
			Ins.Add("user_agr","@user_agr","F");
			Ins.Add("fec_agr","@fec_agr","F");
			Ins.Add("user_mod","@user_mod","F");
			Ins.Add("fec_mod","@fec_mod","F");

            string sp = Ins.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                BindingTipoCliente(cmd, oBeCliente_tipo);

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

	public static int Insertar(IConfiguration config, clsBeCliente_tipo oBeCliente_tipo) {

			int rowsAffected = 0;
		SqlConnection lConnection= new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

		try {
			Ins.Init("cliente_tipo");
			Ins.Add("idtipocliente","@idtipocliente","F");
			Ins.Add("idpropietario","@idpropietario","F");
			Ins.Add("nombretipocliente","@nombretipocliente","F");
			Ins.Add("activo","@activo","F");
			Ins.Add("user_agr","@user_agr","F");
			Ins.Add("fec_agr","@fec_agr","F");
			Ins.Add("user_mod","@user_mod","F");
			Ins.Add("fec_mod","@fec_mod","F");

			string sp = Ins.SQL();

			SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

				lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
				cmd = new SqlCommand(sp, lConnection, lTransaction);

            BindingTipoCliente(cmd, oBeCliente_tipo);

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

	public static int Actualizar(IConfiguration config, clsBeCliente_tipo oBeCliente_tipo,SqlConnection? pConection = null, SqlTransaction? pTransaction=null) {

			int rowsAffected = 0;
		SqlConnection lConnection= new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

		try {

			Upd.Init("cliente_tipo");
			Upd.Add("idtipocliente","@idtipocliente","F");
			Upd.Add("idpropietario","@idpropietario","F");
			Upd.Add("nombretipocliente","@nombretipocliente","F");
			Upd.Add("activo","@activo","F");
			Upd.Add("user_agr","@user_agr","F");
			Upd.Add("fec_agr","@fec_agr","F");
			Upd.Add("user_mod","@user_mod","F");
			Upd.Add("fec_mod","@fec_mod","F");
			Upd.Where("IdTipoCliente = @IdTipoCliente");

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

            BindingTipoCliente(cmd, oBeCliente_tipo);

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

	public int Eliminar(IConfiguration config, clsBeCliente_tipo oBeCliente_tipo,SqlConnection? pConection =null, SqlTransaction? pTransaction = null) {

		SqlConnection lConnection= new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

		try {
			const string sp = (" Delete from Cliente_tipo" + 
			 "  Where(IdTipoCliente = @IdTipoCliente)");

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

			cmd.Parameters.Add(new SqlParameter("@IdTipoCliente", oBeCliente_tipo.IdTipoCliente));

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
			const string sp = "Select * FROM Cliente_tipo";
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

	public static bool GetSingle(IConfiguration config, ref clsBeCliente_tipo pBeCliente_tipo) { 
		
		SqlConnection lConnection= new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

		try
		{
		
			const string sp = "Select * FROM Cliente_tipo" + 
			" Where(IdTipoCliente = @IdTipoCliente)";

				lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

			SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) {CommandType=CommandType.Text};
			SqlDataAdapter dad = new SqlDataAdapter(cmd);

			dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTipoCliente", pBeCliente_tipo.IdTipoCliente));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPropietario", pBeCliente_tipo.IdPropietario));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@NombreTipoCliente", pBeCliente_tipo.NombreTipoCliente));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@Activo", pBeCliente_tipo.Activo));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@user_agr", pBeCliente_tipo.User_agr));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_agr", pBeCliente_tipo.Fec_agr));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@user_mod", pBeCliente_tipo.User_mod));
			dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_mod", pBeCliente_tipo.Fec_mod));

			DataTable dt = new DataTable();
			dad.Fill(dt);

			lTransaction.Commit();

			if (dt.Rows.Count == 1) {
				DataRow r;
				r = dt.Rows[0];
				Cargar(ref pBeCliente_tipo,r);
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

	public static List<clsBeCliente_tipo> GetAll(IConfiguration config)
		{
		
		 SqlTransaction? lTransaction = null;
		List<clsBeCliente_tipo> lreturnList = new List<clsBeCliente_tipo>();
		
		try
		{
			const string sp = "Select * FROM Cliente_tipo";

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

						clsBeCliente_tipo vBeCliente_tipo = new clsBeCliente_tipo();

						foreach (DataRow dr in lDataTable.Rows)
						{
							vBeCliente_tipo = new clsBeCliente_tipo();
							Cargar(ref vBeCliente_tipo, dr);
							lreturnList.Add(vBeCliente_tipo);
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
		
			const string sp = "Select ISNULL(Max(IdTipoCliente),0) FROM Cliente_tipo";

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
		
		
			const string sp = "Select ISNULL(Max(IdTipoCliente),0) FROM Cliente_tipo";

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
