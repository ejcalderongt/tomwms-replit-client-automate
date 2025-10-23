namespace WMS.DALCore.Ticket
{
    using System;
    using System.Data;
    using System.Collections.Generic;
    using WMS.EntityCore.Ticket;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;    
    using WMSWebAPI.Be;

    public class clsLnTms_ticket
    {
        private static clsInsert Ins = new clsInsert();
        private static clsUpdate Upd = new clsUpdate();
        public static void Cargar(ref clsBeTms_ticket oBeTms_ticket, DataRow dr)
        {
            try
            {
                oBeTms_ticket.IdTicket = (dr["IdTicket"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IdTicket"]);
                oBeTms_ticket.IdEmpresa = (dr["IdEmpresa"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IdEmpresa"]);
                oBeTms_ticket.IdPropietario = (dr["IdPropietario"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IdPropietario"]);
                oBeTms_ticket.IdUbicacionDestino = (dr["IdUbicacionDestino"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IdUbicacionDestino"]);
                oBeTms_ticket.IdPiloto = (dr["IdPiloto"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IdPiloto"]);
                oBeTms_ticket.IdVehiculo = (dr["IdVehiculo"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IdVehiculo"]);
                oBeTms_ticket.IdEmpresaTransporte = (dr["IdEmpresaTransporte"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IdEmpresaTransporte"]);                
                oBeTms_ticket.Fecha_Ingreso = (dr["Fecha_Ingreso"] == DBNull.Value) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dr["Fecha_Ingreso"]);
                oBeTms_ticket.Fecha_Salida = (dr["Fecha_Salida"] == DBNull.Value) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dr["Fecha_Salida"]);
                oBeTms_ticket.Fecha_Finalizado = (dr["Fecha_Finalizado"] == DBNull.Value) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dr["Fecha_Finalizado"]);
                oBeTms_ticket.Tipo_Operacion = (dr["Tipo_Operacion"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Tipo_Operacion"]) ?? string.Empty;
                oBeTms_ticket.Estado = (dr["Estado"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Estado"]) ?? string.Empty;
                oBeTms_ticket.No_Poliza = (dr["No_Poliza"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["No_Poliza"]) ?? string.Empty;
                oBeTms_ticket.No_Placa = (dr["No_Placa"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["No_Placa"]) ?? string.Empty;
                oBeTms_ticket.No_Documento_Piloto = (dr["No_Documento_Piloto"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["No_Documento_Piloto"]) ?? string.Empty;
                oBeTms_ticket.Tipo_Documento_Piloto = (dr["Tipo_Documento_Piloto"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Tipo_Documento_Piloto"]) ?? string.Empty;
                oBeTms_ticket.Nombres_Piloto = (dr["Nombres_Piloto"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Nombres_Piloto"]) ?? string.Empty;
                oBeTms_ticket.Apellidos_Piloto = (dr["Apellidos_Piloto"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Apellidos_Piloto"]) ?? string.Empty;
                oBeTms_ticket.No_TC = (dr["No_TC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["No_TC"]) ?? string.Empty;
                oBeTms_ticket.Procesado_Stock_Jornada = (dr["Procesado_Stock_Jornada"] == DBNull.Value) ? false : Convert.ToBoolean(dr["Procesado_Stock_Jornada"]);
                oBeTms_ticket.Fecha_Procesado_Stock_Jornada = (dr["fecha_procesado_stock_jornada"] == DBNull.Value) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dr["fecha_procesado_stock_jornada"]);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int Insertar(ref clsBeTms_ticket oBeTms_ticket, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Ins.Init("tms_ticket");
                Ins.Add("idticket", "@idticket", "F");
                Ins.Add("idempresa", "@idempresa", "F");
                Ins.Add("idpropietario", "@idpropietario", "F");
                Ins.Add("idubicaciondestino", "@idubicaciondestino", "F");
                Ins.Add("idpiloto", "@idpiloto", "F");
                Ins.Add("idvehiculo", "@idvehiculo", "F");
                Ins.Add("idempresatransporte", "@idempresatransporte", "F");
                Ins.Add("tipo_operacion", "@tipo_operacion", "F");
                Ins.Add("fecha_ingreso", "@fecha_ingreso", "F");
                Ins.Add("fecha_salida", "@fecha_salida", "F");
                Ins.Add("fecha_finalizado", "@fecha_finalizado", "F");
                Ins.Add("estado", "@estado", "F");
                Ins.Add("no_poliza", "@no_poliza", "F");
                Ins.Add("no_placa", "@no_placa", "F");
                Ins.Add("no_documento_piloto", "@no_documento_piloto", "F");
                Ins.Add("tipo_documento_piloto", "@tipo_documento_piloto", "F");
                Ins.Add("nombres_piloto", "@nombres_piloto", "F");
                Ins.Add("apellidos_piloto", "@apellidos_piloto", "F");
                Ins.Add("no_tc", "@no_tc", "F");
                Ins.Add("procesado_stock_jornada", "@procesado_stock_jornada", "F");
                Ins.Add("fecha_procesado_stock_jornada", "@fecha_procesado_stock_jornada", "F");

                string sp = Ins.SQL();

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@IDTICKET", oBeTms_ticket.IdTicket));
                    cmd.Parameters.Add(new SqlParameter("@IDEMPRESA", oBeTms_ticket.IdEmpresa));
                    cmd.Parameters.Add(new SqlParameter("@IDPROPIETARIO", oBeTms_ticket.IdPropietario));
                    cmd.Parameters.Add(new SqlParameter("@IDUBICACIONDESTINO", oBeTms_ticket.IdUbicacionDestino));
                    cmd.Parameters.Add(new SqlParameter("@IDPILOTO", oBeTms_ticket.IdPiloto));
                    cmd.Parameters.Add(new SqlParameter("@IDVEHICULO", oBeTms_ticket.IdVehiculo));
                    cmd.Parameters.Add(new SqlParameter("@IDEMPRESATRANSPORTE", oBeTms_ticket.IdEmpresaTransporte));
                    cmd.Parameters.Add(new SqlParameter("@TIPO_OPERACION", oBeTms_ticket.Tipo_Operacion ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_INGRESO", oBeTms_ticket.Fecha_Ingreso));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_SALIDA", oBeTms_ticket.Fecha_Salida));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_FINALIZADO", oBeTms_ticket.Fecha_Finalizado));
                    cmd.Parameters.Add(new SqlParameter("@ESTADO", oBeTms_ticket.Estado ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@NO_POLIZA", oBeTms_ticket.No_Poliza ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@NO_PLACA", oBeTms_ticket.No_Placa ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@NO_DOCUMENTO_PILOTO", oBeTms_ticket.No_Documento_Piloto ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@TIPO_DOCUMENTO_PILOTO", oBeTms_ticket.Tipo_Documento_Piloto ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@NOMBRES_PILOTO", oBeTms_ticket.Nombres_Piloto ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@APELLIDOS_PILOTO", oBeTms_ticket.Apellidos_Piloto ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@NO_TC", oBeTms_ticket.No_TC ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@PROCESADO_STOCK_JORNADA", oBeTms_ticket.Procesado_Stock_Jornada));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_PROCESADO_STOCK_JORNADA", oBeTms_ticket.Fecha_Procesado_Stock_Jornada));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int Insertar_Directo(ref clsBeTms_ticket oBeTms_ticket, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Ins.Init("tms_ticket");
                Ins.Add("idticket", "@idticket", "F");
                Ins.Add("idempresa", "@idempresa", "F");
                Ins.Add("idubicaciondestino", "@idubicaciondestino", "F");
                Ins.Add("tipo_operacion", "@tipo_operacion", "F");
                Ins.Add("fecha_ingreso", "@fecha_ingreso", "F");
                Ins.Add("fecha_salida", "@fecha_salida", "F");
                Ins.Add("fecha_finalizado", "@fecha_finalizado", "F");
                Ins.Add("estado", "@estado", "F");
                Ins.Add("no_poliza", "@no_poliza", "F");
                Ins.Add("no_placa", "@no_placa", "F");
                Ins.Add("no_documento_piloto", "@no_documento_piloto", "F");
                Ins.Add("tipo_documento_piloto", "@tipo_documento_piloto", "F");
                Ins.Add("nombres_piloto", "@nombres_piloto", "F");
                Ins.Add("apellidos_piloto", "@apellidos_piloto", "F");
                Ins.Add("no_tc", "@no_tc", "F");
                Ins.Add("procesado_stock_jornada", "@procesado_stock_jornada", "F");
                Ins.Add("fecha_procesado_stock_jornada", "@fecha_procesado_stock_jornada", "F");

                string sp = Ins.SQL();

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@IDTICKET", oBeTms_ticket.IdTicket));
                    cmd.Parameters.Add(new SqlParameter("@IDEMPRESA", oBeTms_ticket.IdEmpresa));
                    cmd.Parameters.Add(new SqlParameter("@IDUBICACIONDESTINO", oBeTms_ticket.IdUbicacionDestino));
                    cmd.Parameters.Add(new SqlParameter("@TIPO_OPERACION", oBeTms_ticket.Tipo_Operacion ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_INGRESO", oBeTms_ticket.Fecha_Ingreso));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_SALIDA", oBeTms_ticket.Fecha_Salida));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_FINALIZADO", oBeTms_ticket.Fecha_Finalizado));
                    cmd.Parameters.Add(new SqlParameter("@ESTADO", oBeTms_ticket.Estado ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@NO_POLIZA", oBeTms_ticket.No_Poliza ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@NO_PLACA", oBeTms_ticket.No_Placa ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@NO_DOCUMENTO_PILOTO", oBeTms_ticket.No_Documento_Piloto ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@TIPO_DOCUMENTO_PILOTO", oBeTms_ticket.Tipo_Documento_Piloto ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@NOMBRES_PILOTO", oBeTms_ticket.Nombres_Piloto ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@APELLIDOS_PILOTO", oBeTms_ticket.Apellidos_Piloto ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@NO_TC", oBeTms_ticket.No_TC ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@PROCESADO_STOCK_JORNADA", oBeTms_ticket.Procesado_Stock_Jornada));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_PROCESADO_STOCK_JORNADA", oBeTms_ticket.Fecha_Procesado_Stock_Jornada));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int Actualizar(ref clsBeTms_ticket oBeTms_ticket, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Upd.Init("tms_ticket");
                Upd.Add("idticket", "@idticket", "F");
                Upd.Add("idempresa", "@idempresa", "F");
                Upd.Add("idpropietario", "@idpropietario", "F");
                Upd.Add("idubicaciondestino", "@idubicaciondestino", "F");
                Upd.Add("idpiloto", "@idpiloto", "F");
                Upd.Add("idvehiculo", "@idvehiculo", "F");
                Upd.Add("idempresatransporte", "@idempresatransporte", "F");
                Upd.Add("tipo_operacion", "@tipo_operacion", "F");
                Upd.Add("fecha_ingreso", "@fecha_ingreso", "F");
                Upd.Add("fecha_salida", "@fecha_salida", "F");
                Upd.Add("fecha_finalizado", "@fecha_finalizado", "F");
                Upd.Add("estado", "@estado", "F");
                Upd.Add("no_poliza", "@no_poliza", "F");
                Upd.Add("no_placa", "@no_placa", "F");
                Upd.Add("no_documento_piloto", "@no_documento_piloto", "F");
                Upd.Add("tipo_documento_piloto", "@tipo_documento_piloto", "F");
                Upd.Add("nombres_piloto", "@nombres_piloto", "F");
                Upd.Add("apellidos_piloto", "@apellidos_piloto", "F");
                Upd.Add("no_tc", "@no_tc", "F");
                Upd.Add("procesado_stock_jornada", "@procesado_stock_jornada", "F");
                Upd.Add("fecha_procesado_stock_jornada", "@fecha_procesado_stock_jornada", "F");
                Upd.Where("IdTicket = @IdTicket");

                string sp = Upd.SQL();

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@IDTICKET", oBeTms_ticket.IdTicket));
                    cmd.Parameters.Add(new SqlParameter("@IDEMPRESA", oBeTms_ticket.IdEmpresa));
                    cmd.Parameters.Add(new SqlParameter("@IDPROPIETARIO", oBeTms_ticket.IdPropietario));
                    cmd.Parameters.Add(new SqlParameter("@IDUBICACIONDESTINO", oBeTms_ticket.IdUbicacionDestino));
                    cmd.Parameters.Add(new SqlParameter("@IDPILOTO", oBeTms_ticket.IdPiloto));
                    cmd.Parameters.Add(new SqlParameter("@IDVEHICULO", oBeTms_ticket.IdVehiculo));
                    cmd.Parameters.Add(new SqlParameter("@IDEMPRESATRANSPORTE", oBeTms_ticket.IdEmpresaTransporte));
                    cmd.Parameters.Add(new SqlParameter("@TIPO_OPERACION", oBeTms_ticket.Tipo_Operacion ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_INGRESO", oBeTms_ticket.Fecha_Ingreso));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_SALIDA", oBeTms_ticket.Fecha_Salida));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_FINALIZADO", oBeTms_ticket.Fecha_Finalizado));
                    cmd.Parameters.Add(new SqlParameter("@ESTADO", oBeTms_ticket.Estado ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@NO_POLIZA", oBeTms_ticket.No_Poliza ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@NO_PLACA", oBeTms_ticket.No_Placa ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@NO_DOCUMENTO_PILOTO", oBeTms_ticket.No_Documento_Piloto ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@TIPO_DOCUMENTO_PILOTO", oBeTms_ticket.Tipo_Documento_Piloto ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@NOMBRES_PILOTO", oBeTms_ticket.Nombres_Piloto ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@APELLIDOS_PILOTO", oBeTms_ticket.Apellidos_Piloto ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@NO_TC", oBeTms_ticket.No_TC ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@PROCESADO_STOCK_JORNADA", oBeTms_ticket.Procesado_Stock_Jornada));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_PROCESADO_STOCK_JORNADA", oBeTms_ticket.Fecha_Procesado_Stock_Jornada));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int Actualizar_Tms_Ticket(ref int BeTms_ticket, ref string Estado, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Upd.Init("tms_ticket");
                Upd.Add("estado", "@estado", "F");
                Upd.Where("IdTicket = @IdTicket");

                string sp = Upd.SQL();

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@ESTADO", Estado ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@IDTICKET", BeTms_ticket));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Actualizar_Tms_Ticket_Procesado(ref int BeTms_ticket, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Upd.Init("tms_ticket");
                Upd.Add("estado", "@estado", "F");
                Upd.Add("fecha_procesado", "@fecha_procesado", "F");
                Upd.Where("IdTicket = @IdTicket");

                string sp = Upd.SQL();

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@ESTADO", "Procesado"));
                    cmd.Parameters.Add(new SqlParameter("@IDTICKET", BeTms_ticket));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_PROCESADO", DateTime.Now));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Actualizar_Tms_Ticket_Procesado_Por_Stock_Jornada(int IdTicketTMS, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Upd.Init("tms_ticket");
                Upd.Add("procesado_stock_jornada", "@procesado_stock_jornada", "F");
                Upd.Add("fecha_procesado_stock_jornada", "@fecha_procesado_stock_jornada", "F");
                Upd.Where("IdTicket = @IdTicket");

                string sp = Upd.SQL();

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@PROCESADO_STOCK_JORNADA", 1));
                    cmd.Parameters.Add(new SqlParameter("@IDTICKET", IdTicketTMS));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_PROCESADO_STOCK_JORNADA", DateTime.Now));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Actualizar_Tms_Ticket_Asignado(ref int BeTms_ticket, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Upd.Init("tms_ticket");
                Upd.Add("estado", "@estado", "F");
                Upd.Add("fecha_asignado", "@fecha_asignado", "F");
                Upd.Where("IdTicket = @IdTicket");

                string sp = Upd.SQL();

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@ESTADO", "Asignado"));
                    cmd.Parameters.Add(new SqlParameter("@IDTICKET", BeTms_ticket));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_ASIGNADO", DateTime.Now));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Eliminar(ref clsBeTms_ticket oBeTms_ticket, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                const string sp = " Delete from Tms_ticket Where(IdTicket = @IdTicket)";

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@IDTICKET", oBeTms_ticket.IdTicket));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static DataTable Listar(IConfiguration config)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));

            try
            {
                const string sp = "SELECT * FROM Tms_ticket";
                lConnection.Open();
                SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                SqlDataAdapter dad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dad.Fill(dt);

                lTransaction.Commit();
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                lConnection?.Dispose();
            }
        }
        public static List<clsBeTms_ticket> Get_All(IConfiguration config)
        {
            List<clsBeTms_ticket> lReturnList = new List<clsBeTms_ticket>();

            try
            {
                const string sp = "SELECT * FROM Tms_ticket";

                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    lConnection.Open();

                    using (SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                    {
                        using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                        {
                            lDTA.SelectCommand.CommandType = CommandType.Text;
                            lDTA.SelectCommand.Transaction = lTransaction;
                            DataTable lDataTable = new DataTable();
                            lDTA.Fill(lDataTable);

                            clsBeTms_ticket vBeTms_ticket;

                            foreach (DataRow dr in lDataTable.Rows)
                            {
                                vBeTms_ticket = new clsBeTms_ticket();
                                var lrow = dr;
                                Cargar(ref vBeTms_ticket, lrow);
                                lReturnList.Add(vBeTms_ticket);
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
        public static bool Ticket_Procesado_Stock_Jornada(int IdTicket, SqlConnection lConnection, SqlTransaction lTransaction)
        {
            bool result = false;

            try
            {
                const string sp = "SELECT * FROM Tms_ticket Where(IdTicket = @IdTicket AND procesado_stock_jornada =1) ";

                using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    lDTA.SelectCommand.Transaction = lTransaction;
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdTicket", IdTicket);

                    DataTable lDataTable = new DataTable();
                    lDTA.Fill(lDataTable);

                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
        public static int MaxID(IConfiguration config)
        {
            try
            {
                int lMax = 0;
                const string sp = "SELECT ISNULL(Max(IdTicket),0) FROM Tms_ticket";

                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    lConnection.Open();

                    using (SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        using (SqlCommand lCommand = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text })
                        {
                            object lReturnValue = lCommand.ExecuteScalar();
                            if (lReturnValue != DBNull.Value && lReturnValue != null)
                            {
                                lMax = Convert.ToInt32(lReturnValue);
                            }
                        }

                        lTransaction.Commit();
                    }

                    lConnection.Close();
                }

                return lMax;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int MaxID(SqlConnection lConnection, SqlTransaction lTransaction)
        {
            try
            {
                int lMax = 0;
                const string sp = "SELECT ISNULL(Max(IdTicket),0) FROM Tms_ticket";

                using (SqlCommand lCommand = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text })
                {
                    object lReturnValue = lCommand.ExecuteScalar();
                    if (lReturnValue != DBNull.Value && lReturnValue != null)
                    {
                        lMax = Convert.ToInt32(lReturnValue);
                    }
                }

                return lMax;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static DataTable Get_All_For_Grid(IConfiguration config, int pIdEmpresa, DateTime pFechaDel, DateTime pFechaAl)
        {
            DataTable? result = null;

            try
            {
                string vSQL = "SELECT IdTicket, Nombre_Piloto, Apellidos_Piloto, Placa_Vehiculo, Placa_TC, Empresa_Transporte, tipo_operacion, Fecha_Ingreso, Fecha_Salida, Estado FROM VW_TMS_Tikcet WHERE IdEmpresa = @IdEmpresa ";
                vSQL += string.Format(" AND cast(Fecha_Ingreso AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel.Date), FormatoFechas.fFecha(pFechaAl.Date));
                

                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    lConnection.Open();

                    using (SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
                        {
                            lDTA.SelectCommand.CommandType = CommandType.Text;
                            lDTA.SelectCommand.Transaction = lTransaction;
                            lDTA.SelectCommand.Parameters.AddWithValue("IdEmpresa", pIdEmpresa);

                            DataTable lDataTable = new DataTable();
                            lDTA.Fill(lDataTable);
                            result = lDataTable;
                        }

                        lTransaction.Commit();
                    }

                    lConnection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
        public static int TMS_MaxID(SqlConnection lConnection, SqlTransaction lTransaction)
        {
            try
            {
                int lMax = 0;
                const string sp = "SELECT ISNULL(Max(IdOrdenTmsEnc),0) FROM Tms_ticket_pol";

                using (SqlCommand lCommand = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text })
                {
                    object lReturnValue = lCommand.ExecuteScalar();
                    if (lReturnValue != DBNull.Value && lReturnValue != null)
                    {
                        lMax = Convert.ToInt32(lReturnValue);
                    }
                }

                return lMax;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int Actualizar_Fecha_Ingreso_Tms_Ticket(ref clsBeTms_ticket BeTms_ticket, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Upd.Init("tms_ticket");
                Upd.Add("fecha_ingreso", "@fecha_ingreso", "F");
                Upd.Where("IdTicket = @IdTicket");

                string sp = Upd.SQL();

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@FECHA_INGRESO", BeTms_ticket.Fecha_Ingreso));
                    cmd.Parameters.Add(new SqlParameter("@IDTICKET", BeTms_ticket.IdTicket));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Actualizar_Tms_Ticket_Finalizado(int BeTms_ticket, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Upd.Init("tms_ticket");
                Upd.Add("estado", "@estado", "F");
                Upd.Add("fecha_finalizado", "@fecha_finalizado", "F");
                Upd.Where("IdTicket = @IdTicket");

                string sp = Upd.SQL();

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@ESTADO", "Finalizado"));
                    cmd.Parameters.Add(new SqlParameter("@IDTICKET", BeTms_ticket));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_FINALIZADO", DateTime.Now));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static clsBeTms_ticket? Get_Ultima_Visita_By_IdPiloto(IConfiguration config, int pIdPiloto)
        {
            clsBeTms_ticket? result = null;

            try
            {
                const string sp = "SELECT top(1) * FROM Tms_ticket " +
                                 " Where(IdPiloto = @IdPiloto) order by Fecha_Ingreso desc ";

                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    lConnection.Open();

                    using (SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                        {
                            lDTA.SelectCommand.CommandType = CommandType.Text;
                            lDTA.SelectCommand.Transaction = lTransaction;
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdPiloto", pIdPiloto);

                            DataTable lDataTable = new DataTable();
                            lDTA.Fill(lDataTable);

                            clsBeTms_ticket vBeTms_ticket = new clsBeTms_ticket();

                            if (lDataTable != null && lDataTable.Rows.Count > 0)
                            {
                                var lrow = lDataTable.Rows[0];
                                Cargar(ref vBeTms_ticket, lrow);
                                result = vBeTms_ticket;
                            }
                        }

                        lTransaction.Commit();
                    }

                    lConnection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public static bool Guardar_Ticket(IConfiguration config,
                                          clsBeEmpresa_transporte_pilotos Bepiloto,
                                          clsBeEmpresa_transporte_vehiculos? BeVehiculo,
                                          clsBeTms_ticket BeTicket,
                                          clsBeTms_ticket_pol BeTmsTicket)
        {
            bool result = false;
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

                if (!clsLnEmpresa_transporte_pilotos.Existe_No_Licencia(Bepiloto.No_licencia,lConnection,lTransaction))
                {
                    Bepiloto.IdPiloto = clsLnEmpresa_transporte_pilotos.MaxID(lConnection, lTransaction) + 1;
                    BeTicket.IdPiloto = Bepiloto.IdPiloto;
                    clsLnEmpresa_transporte_pilotos.Insertar(Bepiloto,lConnection,lTransaction);
                }
                else
                {
                    Bepiloto = clsLnEmpresa_transporte_pilotos.Get_By_No_Documento(Bepiloto.No_licencia, lConnection, lTransaction);
                }

                if (BeVehiculo !=null)
                if (!clsLnEmpresa_transporte_vehiculos.Existe_Placa(BeVehiculo.Placa,lConnection,lTransaction))
                {
                    BeVehiculo.IdVehiculo = clsLnEmpresa_transporte_vehiculos.MaxID(lConnection, lTransaction) + 1;
                    BeTicket.IdVehiculo = BeVehiculo.IdVehiculo;
                    clsLnEmpresa_transporte_vehiculos.Insertar(BeVehiculo,lConnection,lTransaction);
                }
                else
                {
                    BeVehiculo = clsLnEmpresa_transporte_vehiculos.Get_Single_By_No_Placa(BeVehiculo.Placa, lConnection, lTransaction);
                    if (BeVehiculo != null)
                    {
                        BeTicket.IdVehiculo = BeVehiculo.IdVehiculo;
                    }
                }

                BeTicket.IdTicket = MaxID(lConnection, lTransaction) + 1;

                if (BeTmsTicket != null)
                {
                    BeTmsTicket.IdTicket = BeTicket.IdTicket;
                    BeTmsTicket.IdOrdenTmsEnc = TMS_MaxID(lConnection, lTransaction) + 1;
                    clsLnTms_ticket_pol.Insertar(BeTmsTicket, lConnection, lTransaction);
                }

                Insertar(ref BeTicket, lConnection, lTransaction);

                lTransaction.Commit();
                result = true;
            }
            catch (Exception)
            {
                if (lTransaction != null) lTransaction.Rollback();
                throw;
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
            }

            return result;
        }

        public static bool Guardar_Ticket(IConfiguration config, clsBeTms_ticket BeTicket)
        {
            bool result = false;
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

                BeTicket.IdTicket = MaxID(lConnection, lTransaction) + 1;
                Insertar_Directo(ref BeTicket, lConnection, lTransaction);

                lTransaction.Commit();
                result = true;
            }
            catch (Exception)
            {
                if (lTransaction != null) lTransaction.Rollback();
                throw;
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
            }

            return result;
        }

        public static clsBeTms_ticket? Get_Ticket_By_Id( IConfiguration config, int pIdTicket)
        {
            clsBeTms_ticket? result = null;

            try
            {
                const string sp = "SELECT top(1) * FROM Tms_ticket " +
                                 " Where(IdTicket = @IdTicket) order by Fecha_Ingreso desc ";

                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    lConnection.Open();

                    using (SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                    {
                        using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                        {
                            lDTA.SelectCommand.CommandType = CommandType.Text;
                            lDTA.SelectCommand.Transaction = lTransaction;
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdTicket", pIdTicket);

                            DataTable lDataTable = new DataTable();
                            lDTA.Fill(lDataTable);

                            clsBeTms_ticket vBeTms_ticket = new clsBeTms_ticket();

                            if (lDataTable != null && lDataTable.Rows.Count > 0)
                            {
                                var lrow = lDataTable.Rows[0];
                                Cargar(ref vBeTms_ticket, lrow);
                                vBeTms_ticket.ObjPoliza = clsLnTms_ticket_pol.GetSingle(vBeTms_ticket.IdTicket, lConnection, lTransaction);
                                result = vBeTms_ticket;
                            }
                        }

                        lTransaction.Commit();
                    }

                    lConnection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public static clsBeTms_ticket? Get_BeTicket_By_IdTicket(int pIdTicket,
                                                               SqlConnection lConnection,
                                                               SqlTransaction lTransaction)
        {
            clsBeTms_ticket? result = null;

            try
            {
                const string sp = "SELECT top(1) * FROM Tms_ticket " +
                                 " Where(IdTicket = @IdTicket) order by Fecha_Ingreso desc ";

                using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    lDTA.SelectCommand.Transaction = lTransaction;
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdTicket", pIdTicket);

                    DataTable lDataTable = new DataTable();
                    lDTA.Fill(lDataTable);

                    clsBeTms_ticket vBeTms_ticket = new clsBeTms_ticket();

                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        var lrow = lDataTable.Rows[0];
                        Cargar(ref vBeTms_ticket, lDataTable.Rows[0]);
                        vBeTms_ticket.ObjPoliza = clsLnTms_ticket_pol.GetSingle(vBeTms_ticket.IdTicket, lConnection, lTransaction);
                        result = vBeTms_ticket;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public static clsBeVW_Fecha_Recepcion_TMS_Ticket? Get_Info_Ingreso(int pIdTicket,
                                                                          SqlConnection lConnection,
                                                                          SqlTransaction lTransaction)
        {
            clsBeVW_Fecha_Recepcion_TMS_Ticket? result = null;
            
            try
            {
                const string sp = "SELECT * FROM VW_Fecha_Recepcion_TMS_Ticket " +
                                 " Where(IdTicket = @IdTicket)  ";

                using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    lDTA.SelectCommand.Transaction = lTransaction;
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdTicket", pIdTicket);

                    DataTable lDataTable = new DataTable();
                    lDTA.Fill(lDataTable);

                    clsBeVW_Fecha_Recepcion_TMS_Ticket vBeTms_ticket = new clsBeVW_Fecha_Recepcion_TMS_Ticket();

                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        clsLnVW_Fecha_Recepcion_TMS_Ticket.Cargar(ref vBeTms_ticket, lDataTable.Rows[0]);
                        result = vBeTms_ticket;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
    }
}