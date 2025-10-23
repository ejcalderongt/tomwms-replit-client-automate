namespace WMS.DALCore.Ticket
{
    using System;
    using System.Data;
    using System.Collections.Generic;
    using WMS.EntityCore.Ticket;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;

    public class clsLnVW_Fecha_Recepcion_TMS_Ticket
    {
        public static void Cargar(ref clsBeVW_Fecha_Recepcion_TMS_Ticket oBeTMP_VW_Fecha_Recepcion_TMS_Ticket, DataRow dr)
        {
            try
            {
                oBeTMP_VW_Fecha_Recepcion_TMS_Ticket.IdTicket = (dr["IdTicket"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IdTicket"]);
                oBeTMP_VW_Fecha_Recepcion_TMS_Ticket.IdOrdenCompraEnc = (dr["IdOrdenCompraEnc"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IdOrdenCompraEnc"]);
                oBeTMP_VW_Fecha_Recepcion_TMS_Ticket.IdRecepcionEnc = (dr["IdRecepcionEnc"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IdRecepcionEnc"]);
                oBeTMP_VW_Fecha_Recepcion_TMS_Ticket.Fecha_Ingreso = (dr["Fecha_Ingreso"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["Fecha_Ingreso"]);
                oBeTMP_VW_Fecha_Recepcion_TMS_Ticket.Fecha_Creacion = (dr["Fecha_Creacion"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["Fecha_Creacion"]);
                oBeTMP_VW_Fecha_Recepcion_TMS_Ticket.Fecha_Recepcion = (dr["Fecha_Recepcion"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["Fecha_Recepcion"]);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<clsBeVW_Fecha_Recepcion_TMS_Ticket> Get_All(IConfiguration config)
        {
            List<clsBeVW_Fecha_Recepcion_TMS_Ticket> lReturnList = new List<clsBeVW_Fecha_Recepcion_TMS_Ticket>();

            try
            {
                const string sp = "SELECT * FROM TMP_VW_Fecha_Recepcion_TMS_Ticket ";

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

                            clsBeVW_Fecha_Recepcion_TMS_Ticket vBeTMP_VW_Fecha_Recepcion_TMS_Ticket;

                            foreach (DataRow dr in lDataTable.Rows)
                            {
                                vBeTMP_VW_Fecha_Recepcion_TMS_Ticket = new clsBeVW_Fecha_Recepcion_TMS_Ticket();
                                Cargar(ref vBeTMP_VW_Fecha_Recepcion_TMS_Ticket, dr);
                                lReturnList.Add(vBeTMP_VW_Fecha_Recepcion_TMS_Ticket);
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

        public static void GetSingle(IConfiguration config, ref clsBeVW_Fecha_Recepcion_TMS_Ticket pBeTMP_VW_Fecha_Recepcion_TMS_Ticket)
        {
            try
            {
                const string sp = "SELECT * FROM TMP_VW_Fecha_Recepcion_TMS_Ticket";

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

                            clsBeVW_Fecha_Recepcion_TMS_Ticket vBeTMP_VW_Fecha_Recepcion_TMS_Ticket = new clsBeVW_Fecha_Recepcion_TMS_Ticket();

                            if (lDataTable != null && lDataTable.Rows.Count > 0)
                            {
                                var lrow = lDataTable.Rows[0];
                                Cargar(ref vBeTMP_VW_Fecha_Recepcion_TMS_Ticket, lrow);
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
        }
    }
}