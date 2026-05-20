namespace WMS.DALCore.I_nav_ped_traslado_det
{
    using AppGlobal;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Data;        
    using WMS.EntityCore.Pedido;
    using WMS.EntityCore.Producto;

    public static class clsLnI_nav_ped_traslado_det
    {
        private static clsInsert Ins = new clsInsert();
        private static clsUpdate Upd = new clsUpdate();

        private static bool TieneLlaveLinea(clsBeI_nav_ped_traslado_det det)
        {
            return det != null &&
                   !string.IsNullOrWhiteSpace(det.NoEnc) &&
                   det.Line_No != 0 &&
                   !string.IsNullOrWhiteSpace(det.Item_No);
        }

        public static void Cargar(ref clsBeI_nav_ped_traslado_det oBeI_nav_ped_traslado_det, DataRow dr)
        {
            string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
            double GetDouble(string col) { return dr[col] is DBNull ? 0.0 : Convert.ToDouble(dr[col]); }
            DateTime? GetDate(string col) { return dr[col] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr[col]); }
            bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
            int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }

            try
            {
                oBeI_nav_ped_traslado_det.NoEnc = GetString("NoEnc");
                oBeI_nav_ped_traslado_det.Line_No = GetInt("Line_No");
                oBeI_nav_ped_traslado_det.Variant_Code = GetString("Variant_Code");
                oBeI_nav_ped_traslado_det.No = GetString("No");
                oBeI_nav_ped_traslado_det.Description = GetString("Description");
                oBeI_nav_ped_traslado_det.Item_No = GetString("Item_No");
                oBeI_nav_ped_traslado_det.Qty_to_Receive = GetDouble("Qty_to_Receive");
                oBeI_nav_ped_traslado_det.Qty_to_Ship = GetDouble("Qty_to_Ship");
                oBeI_nav_ped_traslado_det.Quantity = GetDouble("Quantity");
                oBeI_nav_ped_traslado_det.Transfer_to_CodeField = GetString("transfer_to_CodeField");
                oBeI_nav_ped_traslado_det.Transfer_From_CodeField = GetString("Transfer_From_CodeField");
                oBeI_nav_ped_traslado_det.Shipment_Date = GetDate("Shipment_Date");
                oBeI_nav_ped_traslado_det.Unit_of_Measure_Code = GetString("Unit_of_Measure_Code");
                oBeI_nav_ped_traslado_det.Status = GetInt("Status");
                oBeI_nav_ped_traslado_det.Price = GetDouble("Price");
                oBeI_nav_ped_traslado_det.Source_ID = GetString("Source_ID");
                oBeI_nav_ped_traslado_det.IdPedidoDet = GetInt("IdPedidoDet");
                oBeI_nav_ped_traslado_det.Is_Partially_Processed = GetBool("Is_Partially_Processed");
                oBeI_nav_ped_traslado_det.Scan_Type = GetString("Scan_Type");
                oBeI_nav_ped_traslado_det.Color = GetString("Color");
                oBeI_nav_ped_traslado_det.Size = GetString("Size");
            }
            catch (Exception)
            {             
                throw;
            }
        }

        public static int Insertar(clsBeI_nav_ped_traslado_det oBeI_nav_ped_traslado_det, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Ins.Init("i_nav_ped_traslado_det");
                Ins.Add("noenc", "@noenc", "F");
                Ins.Add("Line_No", "@Line_No", "F");

                if (!string.IsNullOrEmpty(oBeI_nav_ped_traslado_det.Variant_Code))
                    Ins.Add("Variant_Code", "@Variant_Code", "F");

                Ins.Add("no", "@no", "F");
                Ins.Add("description", "@description", "F");
                Ins.Add("item_no", "@item_no", "F");
                Ins.Add("qty_to_receive", "@qty_to_receive", "F");
                Ins.Add("qty_to_ship", "@qty_to_ship", "F");
                Ins.Add("quantity", "@quantity", "F");
                Ins.Add("transfer_to_codefield", "@transfer_to_codefield", "F");
                Ins.Add("shipment_date", "@shipment_date", "F");
                Ins.Add("status", "@status", "F");
                Ins.Add("price", "@price", "F");
                Ins.Add("unit_of_measure_code", "@unit_of_measure_code", "F");
                Ins.Add("idpedidodet", "@idpedidodet", "F");
                Ins.Add("is_partially_processed", "@is_partially_processed", "F");
                Ins.Add("transfer_from_codefield", "@transfer_from_codefield", "F");
                Ins.Add("scan_type", "@scan_type", "F");
                Ins.Add("Color", "@Color", "F");
                Ins.Add("Size", "@Size", "F");

                if (!string.IsNullOrEmpty(oBeI_nav_ped_traslado_det.Source_ID))
                    Ins.Add("source_id", "@source_id", "F");

                string sp = Ins.SQL();

                using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction);

                cmd.Parameters.Add(new SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@LINE_NO", oBeI_nav_ped_traslado_det.Line_No == 0 ? (object)DBNull.Value : oBeI_nav_ped_traslado_det.Line_No));

                if (!string.IsNullOrEmpty(oBeI_nav_ped_traslado_det.Variant_Code))
                    cmd.Parameters.Add(new SqlParameter("@Variant_Code", oBeI_nav_ped_traslado_det.Variant_Code ?? (object)DBNull.Value));

                cmd.Parameters.Add(new SqlParameter("@NO", oBeI_nav_ped_traslado_det.No ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", clsPublic.Quitar_Caracteres_No_Permitidos(oBeI_nav_ped_traslado_det.Description) ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ITEM_NO", oBeI_nav_ped_traslado_det.Item_No ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@QTY_TO_RECEIVE", oBeI_nav_ped_traslado_det.Qty_to_Receive));
                cmd.Parameters.Add(new SqlParameter("@QTY_TO_SHIP", oBeI_nav_ped_traslado_det.Qty_to_Ship));
                cmd.Parameters.Add(new SqlParameter("@QUANTITY", oBeI_nav_ped_traslado_det.Quantity));
                cmd.Parameters.Add(new SqlParameter("@TRANSFER_TO_CODEFIELD", oBeI_nav_ped_traslado_det.Transfer_to_CodeField ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@SHIPMENT_DATE", oBeI_nav_ped_traslado_det.Shipment_Date ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@UNIT_OF_MEASURE_CODE", oBeI_nav_ped_traslado_det.Unit_of_Measure_Code ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@STATUS", oBeI_nav_ped_traslado_det.Status == 0 ? (object)DBNull.Value : oBeI_nav_ped_traslado_det.Status));
                cmd.Parameters.Add(new SqlParameter("@PRICE", oBeI_nav_ped_traslado_det.Price == 0 ? (object)DBNull.Value : oBeI_nav_ped_traslado_det.Price));

                if (!string.IsNullOrEmpty(oBeI_nav_ped_traslado_det.Source_ID))
                    cmd.Parameters.Add(new SqlParameter("@SOURCE_ID", oBeI_nav_ped_traslado_det.Source_ID ?? (object)DBNull.Value));

                cmd.Parameters.Add(new SqlParameter("@IDPEDIDODET", oBeI_nav_ped_traslado_det.IdPedidoDet));
                cmd.Parameters.Add(new SqlParameter("@IS_PARTIALLY_PROCESSED", oBeI_nav_ped_traslado_det.Is_Partially_Processed ? 1 : 0));
                cmd.Parameters.Add(new SqlParameter("@TRANSFER_FROM_CODEFIELD", oBeI_nav_ped_traslado_det.Transfer_From_CodeField ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@SCAN_TYPE", oBeI_nav_ped_traslado_det.Scan_Type ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Color", oBeI_nav_ped_traslado_det.Color ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Size", oBeI_nav_ped_traslado_det.Size ?? (object)DBNull.Value));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Actualizar(ref clsBeI_nav_ped_traslado_det oBeI_nav_ped_traslado_det, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Upd.Init("i_nav_ped_traslado_det");
                Upd.Add("noenc", "@noenc", "F");
                Upd.Add("Line_No", "@Line_No", "F");
                Upd.Add("Variant_Code", "@Variant_Code", "F");
                Upd.Add("no", "@no", "F");
                Upd.Add("description", "@description", "F");
                Upd.Add("item_no", "@item_no", "F");
                Upd.Add("qty_to_receive", "@qty_to_receive", "F");
                Upd.Add("qty_to_ship", "@qty_to_ship", "F");
                Upd.Add("quantity", "@quantity", "F");

                if (!string.IsNullOrEmpty(oBeI_nav_ped_traslado_det.Transfer_to_CodeField))
                    Upd.Add("transfer_to_codefield", "@transfer_to_codefield", "F");

                Upd.Add("shipment_date", "@shipment_date", "F");
                Upd.Add("unit_of_measure_code", "@unit_of_measure_code", "F");
                Upd.Add("status", "@status", "F");
                Upd.Add("price", "@price", "F");
                Upd.Add("source_id", "@source_id", "F");
                Upd.Add("idpedidodet", "@idpedidodet", "F");
                Upd.Add("is_partially_processed", "@is_partially_processed", "F");
                Upd.Add("transfer_from_codefield", "@transfer_from_codefield", "F");
                Upd.Add("scan_type", "@scan_type", "F");
                Upd.Add("Color", "@Color", "F");
                Upd.Add("Size", "@Size", "F");
                Upd.Where("NoEnc = @NoEnc And No = @No");

                string sp = Upd.SQL();

                using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction);

                cmd.Parameters.Add(new SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Line_No", oBeI_nav_ped_traslado_det.Line_No == 0 ? (object)DBNull.Value : oBeI_nav_ped_traslado_det.Line_No));
                cmd.Parameters.Add(new SqlParameter("@Variant_Code", oBeI_nav_ped_traslado_det.Variant_Code ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@NO", oBeI_nav_ped_traslado_det.No ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", clsPublic.Quitar_Caracteres_No_Permitidos(oBeI_nav_ped_traslado_det.Description) ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ITEM_NO", oBeI_nav_ped_traslado_det.Item_No ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@QTY_TO_RECEIVE", oBeI_nav_ped_traslado_det.Qty_to_Receive));
                cmd.Parameters.Add(new SqlParameter("@QTY_TO_SHIP", oBeI_nav_ped_traslado_det.Qty_to_Ship));
                cmd.Parameters.Add(new SqlParameter("@QUANTITY", oBeI_nav_ped_traslado_det.Quantity));
                cmd.Parameters.Add(new SqlParameter("@TRANSFER_TO_CODEFIELD", oBeI_nav_ped_traslado_det.Transfer_to_CodeField ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@SHIPMENT_DATE", oBeI_nav_ped_traslado_det.Shipment_Date ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@UNIT_OF_MEASURE_CODE", oBeI_nav_ped_traslado_det.Unit_of_Measure_Code ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@STATUS", oBeI_nav_ped_traslado_det.Status == 0 ? (object)DBNull.Value : oBeI_nav_ped_traslado_det.Status  ));
                cmd.Parameters.Add(new SqlParameter("@PRICE", oBeI_nav_ped_traslado_det.Price == 0 ? (object)DBNull.Value : oBeI_nav_ped_traslado_det.Price));
                cmd.Parameters.Add(new SqlParameter("@SOURCE_ID", oBeI_nav_ped_traslado_det.Source_ID ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@IDPEDIDODET", oBeI_nav_ped_traslado_det.IdPedidoDet));
                cmd.Parameters.Add(new SqlParameter("@IS_PARTIALLY_PROCESSED", oBeI_nav_ped_traslado_det.Is_Partially_Processed ? 1 : 0));
                cmd.Parameters.Add(new SqlParameter("@TRANSFER_FROM_CODEFIELD", oBeI_nav_ped_traslado_det.Transfer_From_CodeField ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@SCAN_TYPE", oBeI_nav_ped_traslado_det.Scan_Type ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Color", oBeI_nav_ped_traslado_det.Color ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Size", oBeI_nav_ped_traslado_det.Size ?? (object)DBNull.Value));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Actualizar_Status_Det(clsBeI_nav_ped_traslado_det oBeI_nav_ped_traslado_det, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                if (TieneLlaveLinea(oBeI_nav_ped_traslado_det))
                {
                    Upd.Init("i_nav_ped_traslado_det");
                    Upd.Add("status", "@status", "F");
                    Upd.Add("process_result", "@process_result", "F");
                    Upd.Where("NoEnc = @NoEnc AND Line_No = @Line_No AND Item_No = @Item_No");

                    string spLinea = Upd.SQL();

                    using SqlCommand cmdLinea = new SqlCommand(spLinea, pConection, pTransaction);

                    cmdLinea.Parameters.Add(new SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc ?? (object)DBNull.Value));
                    cmdLinea.Parameters.Add(new SqlParameter("@LINE_NO", oBeI_nav_ped_traslado_det.Line_No));
                    cmdLinea.Parameters.Add(new SqlParameter("@ITEM_NO", oBeI_nav_ped_traslado_det.Item_No ?? (object)DBNull.Value));
                    cmdLinea.Parameters.Add(new SqlParameter("@STATUS", oBeI_nav_ped_traslado_det.Status == 0 ? (object)DBNull.Value : oBeI_nav_ped_traslado_det.Status));
                    cmdLinea.Parameters.Add(new SqlParameter("@PROCESS_RESULT", oBeI_nav_ped_traslado_det.Process_Result ?? (object)DBNull.Value));

                    int rowsLinea = cmdLinea.ExecuteNonQuery();
                    if (rowsLinea > 0)
                        return rowsLinea;
                }

                Upd.Init("i_nav_ped_traslado_det");
                Upd.Add("status", "@status", "F");
                Upd.Add("process_result", "@process_result", "F");
                Upd.Where("NoEnc = @NoEnc And No = @No");

                string sp = Upd.SQL();

                using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction);

                cmd.Parameters.Add(new SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@NO", oBeI_nav_ped_traslado_det.No ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@STATUS", oBeI_nav_ped_traslado_det.Status == 0 ? (object)DBNull.Value : oBeI_nav_ped_traslado_det.Status  ));
                cmd.Parameters.Add(new SqlParameter("@PROCESS_RESULT", oBeI_nav_ped_traslado_det.Process_Result ?? (object)DBNull.Value));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Actualizar_Process_Result(clsBeI_nav_ped_traslado_det oBeI_nav_ped_traslado_det, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                if (TieneLlaveLinea(oBeI_nav_ped_traslado_det))
                {
                    Upd.Init("i_nav_ped_traslado_det");
                    Upd.Add("Process_Result", "@Process_Result", "F");
                    Upd.Add("Qty_to_Receive", "@Qty_to_Receive", "F");
                    Upd.Where("NoEnc = @NoEnc AND Line_No = @Line_No AND Item_No = @Item_No");

                    string spLinea = Upd.SQL();

                    using SqlCommand cmdLinea = new SqlCommand(spLinea, pConection, pTransaction);

                    cmdLinea.Parameters.Add(new SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc ?? (object)DBNull.Value));
                    cmdLinea.Parameters.Add(new SqlParameter("@QTY_TO_RECEIVE", oBeI_nav_ped_traslado_det.Qty_to_Receive));
                    cmdLinea.Parameters.Add(new SqlParameter("@PROCESS_RESULT", oBeI_nav_ped_traslado_det.Process_Result ?? (object)DBNull.Value));
                    cmdLinea.Parameters.Add(new SqlParameter("@LINE_NO", oBeI_nav_ped_traslado_det.Line_No));
                    cmdLinea.Parameters.Add(new SqlParameter("@ITEM_NO", oBeI_nav_ped_traslado_det.Item_No ?? (object)DBNull.Value));

                    int rowsLinea = cmdLinea.ExecuteNonQuery();
                    if (rowsLinea > 0)
                        return rowsLinea;
                }

                Upd.Init("i_nav_ped_traslado_det");
                Upd.Add("Process_Result", "@Process_Result", "F");
                Upd.Add("Qty_to_Receive", "@Qty_to_Receive", "F");
                Upd.Where("NoEnc = @NoEnc And No = @No And Line_No = @Line_No And Item_No = @Item_No");

                string sp = Upd.SQL();

                using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction);

                cmd.Parameters.Add(new SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@NO", oBeI_nav_ped_traslado_det.No ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@QTY_TO_RECEIVE", oBeI_nav_ped_traslado_det.Qty_to_Receive));
                cmd.Parameters.Add(new SqlParameter("@PROCESS_RESULT", oBeI_nav_ped_traslado_det.Process_Result ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@LINE_NO", oBeI_nav_ped_traslado_det.Line_No == 0 ? (object)DBNull.Value : oBeI_nav_ped_traslado_det.Line_No));
                cmd.Parameters.Add(new SqlParameter("@ITEM_NO", oBeI_nav_ped_traslado_det.Item_No ?? (object)DBNull.Value));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Eliminar(ref clsBeI_nav_ped_traslado_det oBeI_nav_ped_traslado_det, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                const string sp = "DELETE FROM I_nav_ped_traslado_det WHERE (NoEnc = @NoEnc) AND (No = @No)";

                using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.Add(new SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@NO", oBeI_nav_ped_traslado_det.No ?? (object)DBNull.Value));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Eliminar_Todos(SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                const string sp = "DELETE FROM I_nav_ped_traslado_det";

                using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction)
                {
                    CommandType = CommandType.Text
                };

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }        

        public static bool Obtener(ref clsBeI_nav_ped_traslado_det oBeI_nav_ped_traslado_det, IConfiguration config)
        {
            try
            {
                const string sp = "SELECT * FROM I_nav_ped_traslado_det WHERE (NoEnc = @NoEnc) AND (No = @No)";

                using SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
                using SqlCommand cmd = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text };
                using SqlDataAdapter dad = new SqlDataAdapter(cmd);
                {
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc ?? (object)DBNull.Value));
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@NO", oBeI_nav_ped_traslado_det.No ?? (object)DBNull.Value));

                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        Cargar(ref oBeI_nav_ped_traslado_det, dt.Rows[0]);
                        return true;
                    }
                    else
                    {
                        throw new Exception("No se pudo obtener el registro");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<clsBeI_nav_ped_traslado_det> GetAll(IConfiguration config)
        {
            try
            {
                List<clsBeI_nav_ped_traslado_det> lReturnList = new List<clsBeI_nav_ped_traslado_det>();
                const string sp = "SELECT * FROM I_nav_ped_traslado_det";

                using SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
                using SqlCommand cmd = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text };
                using SqlDataAdapter dad = new SqlDataAdapter(cmd);
                {
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        clsBeI_nav_ped_traslado_det vBeI_nav_ped_traslado_det = new clsBeI_nav_ped_traslado_det();
                        Cargar(ref vBeI_nav_ped_traslado_det, dr);
                        lReturnList.Add(vBeI_nav_ped_traslado_det);
                    }

                    return lReturnList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool GetSingle(ref clsBeI_nav_ped_traslado_det pBeI_nav_ped_traslado_det, IConfiguration config)
        {
            try
            {
                const string sp = "SELECT * FROM I_nav_ped_traslado_det WHERE (NoEnc = @NoEnc) AND (No = @No)";

                using SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
                using SqlCommand cmd = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text };
                using SqlDataAdapter dad = new SqlDataAdapter(cmd);
                {
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@NOENC", pBeI_nav_ped_traslado_det.NoEnc ?? (object)DBNull.Value));
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@NO", pBeI_nav_ped_traslado_det.No ?? (object)DBNull.Value));

                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        Cargar(ref pBeI_nav_ped_traslado_det, dt.Rows[0]);
                    }

                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Actualizar_IdPedidoDet(ref clsBeI_nav_ped_traslado_det oBeI_nav_ped_traslado_det, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Upd.Init("i_nav_ped_traslado_det");
                Upd.Add("IdPedidoDet", "@IdPedidoDet", "F");
                Upd.Where("NoEnc = @NoEnc And No = @No And Line_No = @Line_No And Item_No = @Item_No");

                string sp = Upd.SQL();

                using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction);

                cmd.Parameters.Add(new SqlParameter("@IDPEDIDODET", oBeI_nav_ped_traslado_det.IdPedidoDet));
                cmd.Parameters.Add(new SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@NO", oBeI_nav_ped_traslado_det.No ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@LINE_NO", oBeI_nav_ped_traslado_det.Line_No == 0 ? (object)DBNull.Value : oBeI_nav_ped_traslado_det.Line_No));
                cmd.Parameters.Add(new SqlParameter("@ITEM_NO", oBeI_nav_ped_traslado_det.Item_No ?? (object)DBNull.Value));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool Tiene_Clientes_Diferentes(string pNoEnc, IConfiguration config)
        {
            bool result = false;

            try
            {
                using SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
                {
                    lConnection.Open();
                    using SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                    {
                        const string sp = "SELECT DISTINCT transfer_to_codefield FROM I_nav_ped_traslado_det WHERE (NoEnc = @NoEnc)";

                        using SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                        using SqlDataAdapter dad = new SqlDataAdapter(cmd);
                        {
                            dad.SelectCommand.Parameters.Add(new SqlParameter("@NOENC", pNoEnc ?? (object)DBNull.Value));

                            DataTable dt = new DataTable();
                            dad.Fill(dt);

                            if (dt.Rows.Count > 1)
                            {
                                result = true;
                            }

                            lTransaction.Commit();
                        }
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Actualizar_Partially_Processed(ref clsBeI_nav_ped_traslado_det oBeI_nav_ped_traslado_det, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Upd.Init("i_nav_ped_traslado_det");
                Upd.Add("is_partially_processed", "@is_partially_processed", "F");
                Upd.Where("NoEnc = @NoEnc AND No = @No AND Line_No = @Line_No AND IdPedidoDet = @IdPedidoDet");

                string sp = Upd.SQL();

                using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction);

                cmd.Parameters.Add(new SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@NO", oBeI_nav_ped_traslado_det.No ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@IS_PARTIALLY_PROCESSED", oBeI_nav_ped_traslado_det.Is_Partially_Processed ? 1 : 0));
                cmd.Parameters.Add(new SqlParameter("@LINE_NO", oBeI_nav_ped_traslado_det.Line_No == 0 ? (object)DBNull.Value : oBeI_nav_ped_traslado_det.Line_No));
                cmd.Parameters.Add(new SqlParameter("@IDPEDIDODET", oBeI_nav_ped_traslado_det.IdPedidoDet));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Actualizar_Quantity_Reserved_WMS(clsBeI_nav_ped_traslado_det oBeI_nav_ped_traslado_det, 
                                                           clsBeProducto? oBeProducto, SqlConnection pConection, SqlTransaction? pTransaction) 
        {
            try
            {
                if (TieneLlaveLinea(oBeI_nav_ped_traslado_det))
                {
                    Upd.Init("i_nav_ped_traslado_det");
                    Upd.Add("Quantity_Reserved_WMS", "@Quantity_Reserved_WMS", "F");
                    Upd.Where("NoEnc = @NoEnc AND Line_No = @Line_No AND Item_No = @Item_No");

                    string spLinea = Upd.SQL();

                    using SqlCommand cmdLinea = new SqlCommand(spLinea, pConection, pTransaction);

                    cmdLinea.Parameters.Add(new SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc ?? (object)DBNull.Value));
                    cmdLinea.Parameters.Add(new SqlParameter("@QUANTITY_RESERVED_WMS", oBeI_nav_ped_traslado_det.Quantity_Reserved_WMS));
                    cmdLinea.Parameters.Add(new SqlParameter("@LINE_NO", oBeI_nav_ped_traslado_det.Line_No));
                    cmdLinea.Parameters.Add(new SqlParameter("@ITEM_NO", oBeI_nav_ped_traslado_det.Item_No ?? (object)DBNull.Value));

                    int rowsLinea = cmdLinea.ExecuteNonQuery();
                    if (rowsLinea > 0)
                        return rowsLinea;
                }

                Upd.Init("i_nav_ped_traslado_det");
                Upd.Add("Quantity_Reserved_WMS", "@Quantity_Reserved_WMS", "F");
                Upd.Where("NoEnc = @NoEnc AND No = @No AND Line_No = @Line_No");

                string sp = Upd.SQL();

                using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction);

                cmd.Parameters.Add(new SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@NO", oBeI_nav_ped_traslado_det.No ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@QUANTITY_RESERVED_WMS", oBeI_nav_ped_traslado_det.Quantity_Reserved_WMS));
                cmd.Parameters.Add(new SqlParameter("@LINE_NO", oBeI_nav_ped_traslado_det.Line_No == 0 ? (object)DBNull.Value : oBeI_nav_ped_traslado_det.Line_No));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void GetSingle(clsBeI_nav_ped_traslado_det pBeTraslado, SqlConnection lConnection, SqlTransaction lTrans)
        {
            try
            {
                const string sp = @"SELECT * FROM I_nav_ped_traslado_det 
                          WHERE NoEnc = @NoEnc AND No = @No AND Line_No = @Line_No";

                using (var cmd = new SqlCommand(sp, lConnection, lTrans) { CommandType = CommandType.Text })
                using (var dad = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Add(new SqlParameter("@NoEnc", pBeTraslado.NoEnc));
                    cmd.Parameters.Add(new SqlParameter("@No", pBeTraslado.Item_No));
                    cmd.Parameters.Add(new SqlParameter("@Line_No", pBeTraslado.Line_No));

                    var dt = new DataTable();
                    dad.Fill(dt);

                    if (dt != null && dt.Rows.Count == 1)
                    {
                        Cargar(ref pBeTraslado, dt.Rows[0]);
                    }
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public static bool Exist(clsBeI_nav_ped_traslado_det pBeI_nav_ped_traslado_det,
                                 SqlConnection pConnection,
                                 SqlTransaction pTransaction)
        {
            bool exists = false;

            try
            {                
                string vSQL = "SELECT No FROM I_nav_ped_traslado_det WHERE (NoEnc = @NoEnc AND No = @No AND Line_No = @Line_No)";

                using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, pConnection))
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    lDTA.SelectCommand.Parameters.Add(new SqlParameter("@NOENC", pBeI_nav_ped_traslado_det.NoEnc));
                    lDTA.SelectCommand.Parameters.Add(new SqlParameter("@NO", pBeI_nav_ped_traslado_det.No));
                    lDTA.SelectCommand.Parameters.Add(new SqlParameter("@LINE_NO", pBeI_nav_ped_traslado_det.Line_No));
                    lDTA.SelectCommand.Transaction = pTransaction;

                    DataTable lDT = new DataTable();
                    lDTA.Fill(lDT);

                    exists = lDT.Rows.Count > 0;
                }
            }
            catch (Exception)
            {                
                throw;
            }

            return exists;
        }

        public static int ActualizarFromIn(clsBeI_nav_ped_traslado_det oBeI_nav_ped_traslado_det,
                                          SqlConnection pConection,
                                          SqlTransaction pTransaction)
        {
            try
            {
                Upd.Init("i_nav_ped_traslado_det");
                Upd.Add("noenc", "@noenc", "F");
                Upd.Add("no", "@no", "F");
                Upd.Add("description", "@description", "F");
                Upd.Add("item_no", "@item_no", "F");
                Upd.Add("qty_to_receive", "@qty_to_receive", "F");
                Upd.Add("qty_to_ship", "@qty_to_ship", "F");
                Upd.Add("quantity", "@quantity", "F");

                if (oBeI_nav_ped_traslado_det.Transfer_to_CodeField != null)
                    Upd.Add("transfer_to_codefield", "@transfer_to_codefield", "F");

                Upd.Add("shipment_date", "@shipment_date", "F");
                Upd.Add("unit_of_measure_code", "@unit_of_measure_code", "F");
                Upd.Where("NoEnc = @NoEnc" +
                          " AND No = @No AND Line_No = @Line_No");

                string sp = Upd.SQL();

                using (SqlCommand cmd = new SqlCommand(sp, pConection))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Transaction = pTransaction;

                    cmd.Parameters.Add(new SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc));
                    cmd.Parameters.Add(new SqlParameter("@NO", oBeI_nav_ped_traslado_det.No));                    
                    cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", clsPublic.Quitar_Caracteres_No_Permitidos(oBeI_nav_ped_traslado_det.Description)));
                    cmd.Parameters.Add(new SqlParameter("@ITEM_NO", oBeI_nav_ped_traslado_det.Item_No));
                    cmd.Parameters.Add(new SqlParameter("@QTY_TO_RECEIVE", oBeI_nav_ped_traslado_det.Qty_to_Receive));
                    cmd.Parameters.Add(new SqlParameter("@QTY_TO_SHIP", oBeI_nav_ped_traslado_det.Qty_to_Ship));
                    cmd.Parameters.Add(new SqlParameter("@QUANTITY", oBeI_nav_ped_traslado_det.Quantity));
                    cmd.Parameters.Add(new SqlParameter("@TRANSFER_TO_CODEFIELD", oBeI_nav_ped_traslado_det.Transfer_to_CodeField ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@SHIPMENT_DATE", oBeI_nav_ped_traslado_det.Shipment_Date));
                    cmd.Parameters.Add(new SqlParameter("@UNIT_OF_MEASURE_CODE", oBeI_nav_ped_traslado_det.Unit_of_Measure_Code));
                    cmd.Parameters.Add(new SqlParameter("@Line_No", oBeI_nav_ped_traslado_det.Line_No));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception)
            {               
                throw;
            }
        }
    }
}
