using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using TOMWMS.srvProducto;
using TOMWMS;

namespace Be
{
	public class clsLnArancel
	{

		private static clsInsert Ins = new clsInsert();
		private static clsUpdate Upd = new clsUpdate();

		public static void Cargar(ref clsBeArancel oBeArancel, ref DataRow dr)
		{
			try
			{
				oBeArancel.IdArancel = dr["IdArancel"] == DBNull.Value ? 0 : int.Parse(dr["IdArancel"].ToString());
				oBeArancel.nombre = dr["nombre"] == DBNull.Value ? "" : dr["nombre"].ToString();
				oBeArancel.porcentaje = dr["porcentaje"] == DBNull.Value ? 0 : float.Parse(dr["porcentaje"].ToString());
				oBeArancel.fec_agr = dr["fec_agr"] == DBNull.Value ? DateTime.Now : DateTime.Parse(dr["fec_agr"].ToString());
				oBeArancel.user_agr = dr["user_agr"] == DBNull.Value ? "" : dr["user_agr"].ToString();
				oBeArancel.fec_mod = dr["fec_mod"] == DBNull.Value ? DateTime.Now : DateTime.Parse(dr["fec_mod"].ToString());
				oBeArancel.user_mod = dr["user_mod"] == DBNull.Value ? "" : dr["user_mod"].ToString();
				oBeArancel.activo = dr["activo"] == DBNull.Value ? false : bool.Parse(dr["activo"].ToString());
			}
			catch (Exception ex)
			{
				throw new Exception(String.Format("{0} {1}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Message));
			}
		}

		public static int Insertar(clsBeArancel oBeArancel, SqlConnection pConection, SqlTransaction pTransaction)
		{

			SqlConnection lConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST"].ConnectionString);
			SqlTransaction lTransaction = null;

			try
			{
				Ins.Init("arancel");
				Ins.Add("idarancel", "@idarancel", "F");
				Ins.Add("nombre", "@nombre", "F");
				Ins.Add("porcentaje", "@porcentaje", "F");
				Ins.Add("fec_agr", "@fec_agr", "F");
				Ins.Add("user_agr", "@user_agr", "F");
				Ins.Add("fec_mod", "@fec_mod", "F");
				Ins.Add("user_mod", "@user_mod", "F");
				Ins.Add("activo", "@activo", "F");

				string sp = Ins.SQL();

				SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

				bool Es_Transaccion_Remota = (pConection != null && pTransaction != null);

				if (Es_Transaccion_Remota)
				{
					cmd = new SqlCommand(sp, pConection, pTransaction);
				}
				else
				{
					lConnection.Open(); lTransaction = lConnection.BeginTransaction();
					cmd = new SqlCommand(sp, lConnection, lTransaction);
				}

				cmd.Parameters.Add(new SqlParameter("@IdArancel", oBeArancel.IdArancel));
				cmd.Parameters.Add(new SqlParameter("@nombre", oBeArancel.nombre));
				cmd.Parameters.Add(new SqlParameter("@porcentaje", oBeArancel.porcentaje));
				cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeArancel.fec_agr));
				cmd.Parameters.Add(new SqlParameter("@user_agr", oBeArancel.user_agr));
				cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeArancel.fec_mod));
				cmd.Parameters.Add(new SqlParameter("@user_mod", oBeArancel.user_mod));
				cmd.Parameters.Add(new SqlParameter("@activo", oBeArancel.activo));

				int rowsAffected = cmd.ExecuteNonQuery();

				if (Es_Transaccion_Remota) lTransaction.Commit();

				return rowsAffected;

			}
			catch (Exception ex)
			{
				throw new Exception(String.Format("{0} {1}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Message));
			}
			finally
			{
				if (lConnection.State == ConnectionState.Open) lConnection.Close();
				if (lConnection != null) lConnection.Dispose();
				if (lTransaction != null) lTransaction.Dispose();
			}
		}

		public static int Insertar(clsBeArancel oBeArancel)
		{

			SqlConnection lConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST"].ConnectionString);
			SqlTransaction lTransaction = null;

			try
			{
				Ins.Init("arancel");
				Ins.Add("idarancel", "@idarancel", "F");
				Ins.Add("nombre", "@nombre", "F");
				Ins.Add("porcentaje", "@porcentaje", "F");
				Ins.Add("fec_agr", "@fec_agr", "F");
				Ins.Add("user_agr", "@user_agr", "F");
				Ins.Add("fec_mod", "@fec_mod", "F");
				Ins.Add("user_mod", "@user_mod", "F");
				Ins.Add("activo", "@activo", "F");

				string sp = Ins.SQL();

				SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

				lConnection.Open(); lTransaction = lConnection.BeginTransaction();
				cmd = new SqlCommand(sp, lConnection, lTransaction);

				cmd.Parameters.Add(new SqlParameter("@IdArancel", oBeArancel.IdArancel));
				cmd.Parameters.Add(new SqlParameter("@nombre", oBeArancel.nombre));
				cmd.Parameters.Add(new SqlParameter("@porcentaje", oBeArancel.porcentaje));
				cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeArancel.fec_agr));
				cmd.Parameters.Add(new SqlParameter("@user_agr", oBeArancel.user_agr));
				cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeArancel.fec_mod));
				cmd.Parameters.Add(new SqlParameter("@user_mod", oBeArancel.user_mod));
				cmd.Parameters.Add(new SqlParameter("@activo", oBeArancel.activo));

				int rowsAffected = cmd.ExecuteNonQuery();

				lTransaction.Commit();

				return rowsAffected;

			}
			catch (Exception ex)
			{
				throw new Exception(String.Format("{0} {1}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Message));
			}
			finally
			{
				if (lConnection.State == ConnectionState.Open) lConnection.Close();
				if (lConnection != null) lConnection.Dispose();
				if (lTransaction != null) lTransaction.Dispose();
			}
		}

		public static int Actualizar(clsBeArancel oBeArancel, SqlConnection pConection, SqlTransaction pTransaction)
		{

			SqlConnection lConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST"].ConnectionString);
			SqlTransaction lTransaction = null;

			try
			{

				Upd.Init("arancel");
				Upd.Add("idarancel", "@idarancel", "F");
				Upd.Add("nombre", "@nombre", "F");
				Upd.Add("porcentaje", "@porcentaje", "F");
				Upd.Add("fec_agr", "@fec_agr", "F");
				Upd.Add("user_agr", "@user_agr", "F");
				Upd.Add("fec_mod", "@fec_mod", "F");
				Upd.Add("user_mod", "@user_mod", "F");
				Upd.Add("activo", "@activo", "F");
				Upd.Where("IdArancel = @IdArancel");

				string sp = Upd.SQL();

				SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

				bool Es_Transaccion_Remota = (pConection != null && pTransaction != null);

				if (Es_Transaccion_Remota)
				{
					cmd = new SqlCommand(sp, pConection, pTransaction);
				}
				else
				{
					lConnection.Open(); lTransaction = lConnection.BeginTransaction();
					cmd = new SqlCommand(sp, lConnection, lTransaction);
				}

				cmd.Parameters.Add(new SqlParameter("@IdArancel", oBeArancel.IdArancel));
				cmd.Parameters.Add(new SqlParameter("@nombre", oBeArancel.nombre));
				cmd.Parameters.Add(new SqlParameter("@porcentaje", oBeArancel.porcentaje));
				cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeArancel.fec_agr));
				cmd.Parameters.Add(new SqlParameter("@user_agr", oBeArancel.user_agr));
				cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeArancel.fec_mod));
				cmd.Parameters.Add(new SqlParameter("@user_mod", oBeArancel.user_mod));
				cmd.Parameters.Add(new SqlParameter("@activo", oBeArancel.activo));

				int rowsAffected = cmd.ExecuteNonQuery();

				if (Es_Transaccion_Remota) lTransaction.Commit();

				return rowsAffected;

			}
			catch (Exception ex)
			{
				throw new Exception(String.Format("{0} {1}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Message));
			}
			finally
			{
				if (lConnection.State == ConnectionState.Open) lConnection.Close();
				if (lConnection != null) lConnection.Dispose();
				if (lTransaction != null) lTransaction.Dispose();
			}
		}

		public int Eliminar(clsBeArancel oBeArancel, SqlConnection pConection, SqlTransaction pTransaction)
		{

			SqlConnection lConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST"].ConnectionString);
			SqlTransaction lTransaction = null;

			try
			{
				const String sp = (" Delete from Arancel" +
				 "  Where(IdArancel = @IdArancel)");

				bool Es_Transaccion_Remota = (pConection != null && pTransaction != null);

				SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

				if (Es_Transaccion_Remota)
				{
					cmd = new SqlCommand(sp, pConection, pTransaction);
				}
				else
				{
					lConnection.Open(); lTransaction = lConnection.BeginTransaction();
					cmd = new SqlCommand(sp, lConnection, lTransaction);
				}

				cmd.Parameters.Add(new SqlParameter("@IdArancel", oBeArancel.IdArancel));

				int rowsAffected = cmd.ExecuteNonQuery();

				if (Es_Transaccion_Remota) lTransaction.Commit();

				return rowsAffected;

			}
			catch (Exception ex)
			{
				throw new Exception(String.Format("{0} {1}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Message));
			}
			finally
			{
				if (lConnection.State == ConnectionState.Open) lConnection.Close();
				if (lConnection != null) lConnection.Dispose();
				if (lTransaction != null) lTransaction.Dispose();
			}
		}

		public DataTable Listar()
		{

			SqlConnection lConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST"].ConnectionString);
			SqlTransaction lTransaction = null;

			try
			{
				const String sp = "SELECT * FROM Arancel";
				lConnection.Open(); lTransaction = lConnection.BeginTransaction();
				SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
				SqlDataAdapter dad = new SqlDataAdapter(cmd);
				DataTable dt = new DataTable();
				dad.Fill(dt);

				lTransaction.Commit();

				return dt;

			}
			catch (Exception ex)
			{
				throw new Exception(String.Format("{0} {1}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Message));
			}
			finally
			{
				if (lConnection.State == ConnectionState.Open) lConnection.Close();
				if (lConnection != null) lConnection.Dispose();
				if (lTransaction != null) lTransaction.Dispose();
			}
		}

		public static bool GetSingle(ref clsBeArancel pBeArancel)
		{

			SqlConnection lConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST"].ConnectionString);
			SqlTransaction lTransaction = null;

			try
			{

				const String sp = "SELECT * FROM Arancel" +
				" Where(IdArancel = @IdArancel)";

				lConnection.Open(); lTransaction = lConnection.BeginTransaction();

				SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
				SqlDataAdapter dad = new SqlDataAdapter(cmd);

				dad.SelectCommand.Parameters.Add(new SqlParameter("@IdArancel", pBeArancel.IdArancel));
				dad.SelectCommand.Parameters.Add(new SqlParameter("@nombre", pBeArancel.nombre));
				dad.SelectCommand.Parameters.Add(new SqlParameter("@porcentaje", pBeArancel.porcentaje));
				dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_agr", pBeArancel.fec_agr));
				dad.SelectCommand.Parameters.Add(new SqlParameter("@user_agr", pBeArancel.user_agr));
				dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_mod", pBeArancel.fec_mod));
				dad.SelectCommand.Parameters.Add(new SqlParameter("@user_mod", pBeArancel.user_mod));
				dad.SelectCommand.Parameters.Add(new SqlParameter("@activo", pBeArancel.activo));

				DataTable dt = new DataTable();
				dad.Fill(dt);

				lTransaction.Commit();

				if (dt.Rows.Count == 1)
				{
					DataRow r;
					r = dt.Rows[0];
					Cargar(ref pBeArancel, ref r);
					return true;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(String.Format("{0} {1}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Message));
			}
			finally
			{
				if (lConnection.State == ConnectionState.Open) lConnection.Close();
				if (lConnection != null) lConnection.Dispose();
				if (lTransaction != null) lTransaction.Dispose();
			}
			return false;

		}

		public static List<clsBeArancel> GetAll()
		{
			List<clsBeArancel> lReturnList = new List<clsBeArancel>();

			try
			{
				const string sp = "SELECT * FROM Arancel";

				using (SqlConnection lConnection = new SqlConnection(connectionString:= ConfigurationManager.ConnectionStrings["CST"].ConnectionString))
				{

					lConnection.Open();

					using (SqlTransaction lTransaction = lConnection.BeginTransaction())
					{
						using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
						{
							lDTA.SelectCommand.CommandType = CommandType.Text;
							lDTA.SelectCommand.Transaction = lTransaction;
							DataTable lDataTable = new DataTable();
							lDTA.Fill(lDataTable);

							clsBeArancel vBeArancel = new clsBeArancel();

							foreach (DataRow dr in lDataTable.Rows)
							{
								vBeArancel = new clsBeArancel();
								Cargar(vBeArancel, dr);
								lReturnList.Add(vBeArancel);
							}

							lTransaction.Commit();
						}

						lConnection.Close();

					}

				}

				return lReturnList;

			}
			catch (Exception ex)
			{
				throw new Exception(String.Format("{0} {1}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Message));
			}
		}

		public static int MaxID()
		{

			try
			{

				int lMax = 0;

				const String sp = "SELECT ISNULL(Max(IdArancel),0) FROM Arancel";

				using (SqlConnection lConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST"].ConnectionString))
				{
					lConnection.Open();

					using (SqlTransaction lTransaction = lConnection.BeginTransaction())
					{
						using (SqlCommand lCommand = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text })
						{
							Object lReturnValue = lCommand.ExecuteScalar();
							if (lReturnValue != DBNull.Value && lReturnValue != null)
							{
								lMax = int.Parse(lReturnValue.ToString());
							}
						}
						lTransaction.Commit();
					}

					lConnection.Close();
				}

				return lMax;

			}
			catch (Exception ex)
			{
				throw new Exception(String.Format("{0} {1}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Message));
			}
		}
	}

}