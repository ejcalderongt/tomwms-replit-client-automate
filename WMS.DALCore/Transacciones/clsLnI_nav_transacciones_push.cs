namespace WMS.DALCore.Transacciones
{
    using System;
    using System.Data;
    using System.Collections.Generic;
    using WMS.EntityCore.Transacciones;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using WMS.EntityCore.Trans_re;

    public class clsLnI_nav_transacciones_push
    {
        private static clsInsert Ins = new clsInsert();
        private static clsUpdate Upd = new clsUpdate();
        public static void Cargar(clsBeI_nav_transacciones_push oBeI_nav_transacciones_push, DataRow dr)
        {
            try
            {
                oBeI_nav_transacciones_push.IdTransaccionPush = dr["IdTransaccionPush"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdTransaccionPush"]);
                oBeI_nav_transacciones_push.IdEmpresa = dr["IdEmpresa"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdEmpresa"]);
                oBeI_nav_transacciones_push.IdBodega = dr["IdBodega"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdBodega"]);
                oBeI_nav_transacciones_push.IdPropietariobodega = dr["IdPropietariobodega"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdPropietariobodega"]);
                oBeI_nav_transacciones_push.IdOrdenCompra = dr["IdOrdenCompra"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdOrdenCompra"]);
                oBeI_nav_transacciones_push.IdRecepcionEnc = dr["IdRecepcionEnc"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdRecepcionEnc"]);
                oBeI_nav_transacciones_push.IdRecepcionDet = dr["IdRecepcionDet"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdRecepcionDet"]);
                oBeI_nav_transacciones_push.Idproductobodega = dr["Idproductobodega"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Idproductobodega"]);
                oBeI_nav_transacciones_push.Idproducto = dr["Idproducto"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Idproducto"]);
                oBeI_nav_transacciones_push.Idunidadmedida = dr["Idunidadmedida"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Idunidadmedida"]);
                oBeI_nav_transacciones_push.Idpresentacion = dr["Idpresentacion"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Idpresentacion"]);
                oBeI_nav_transacciones_push.Idproductoestado = dr["Idproductoestado"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Idproductoestado"]);
                oBeI_nav_transacciones_push.Cantidad = dr["cantidad"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["cantidad"]);
                oBeI_nav_transacciones_push.Peso = dr["peso"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["peso"]);
                oBeI_nav_transacciones_push.Lote = dr["lote"] == DBNull.Value ? string.Empty : Convert.ToString(dr["lote"]) ?? string.Empty;
                oBeI_nav_transacciones_push.Fecha_vence = dr["fecha_vence"] == DBNull.Value ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dr["fecha_vence"]);
                oBeI_nav_transacciones_push.No_linea = dr["no_linea"] == DBNull.Value ? string.Empty : Convert.ToString(dr["no_linea"]) ?? string.Empty;
                oBeI_nav_transacciones_push.Codigo_variante = dr["codigo_variante"] == DBNull.Value ? string.Empty : Convert.ToString(dr["codigo_variante"]) ?? string.Empty;
                oBeI_nav_transacciones_push.Nom_unidad_medida = dr["nom_unidad_medida"] == DBNull.Value ? string.Empty : Convert.ToString(dr["nom_unidad_medida"]) ?? string.Empty;
                oBeI_nav_transacciones_push.Tipo_transaccion = dr["tipo_transaccion"] == DBNull.Value ? string.Empty : Convert.ToString(dr["tipo_transaccion"]) ?? string.Empty;
                oBeI_nav_transacciones_push.IdTipoDocumento = dr["IdTipoDocumento"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdTipoDocumento"]);
                oBeI_nav_transacciones_push.Tipo_push = dr["tipo_push"] == DBNull.Value ? string.Empty : Convert.ToString(dr["tipo_push"]) ?? string.Empty;
                oBeI_nav_transacciones_push.No_recepcion_almacen = dr["no_recepcion_almacen"] == DBNull.Value ? string.Empty : Convert.ToString(dr["no_recepcion_almacen"]) ?? string.Empty;
                oBeI_nav_transacciones_push.Documento_ubicacion = dr["documento_ubicacion"] == DBNull.Value ? string.Empty : Convert.ToString(dr["documento_ubicacion"]) ?? string.Empty;
                oBeI_nav_transacciones_push.Documento_ingreso = dr["documento_ingreso"] == DBNull.Value ? string.Empty : Convert.ToString(dr["documento_ingreso"]) ?? string.Empty;
                oBeI_nav_transacciones_push.Documento_recepcion = dr["documento_recepcion"] == DBNull.Value ? string.Empty : Convert.ToString(dr["documento_recepcion"]) ?? string.Empty;
                oBeI_nav_transacciones_push.Location_code = dr["location_code"] == DBNull.Value ? string.Empty : Convert.ToString(dr["location_code"]) ?? string.Empty;
                oBeI_nav_transacciones_push.Zone_code = dr["zone_code"] == DBNull.Value ? string.Empty : Convert.ToString(dr["zone_code"]) ?? string.Empty;
                oBeI_nav_transacciones_push.Bin_code = dr["bin_code"] == DBNull.Value ? string.Empty : Convert.ToString(dr["bin_code"]) ?? string.Empty;
                oBeI_nav_transacciones_push.Assigne_user_id = dr["assigne_user_id"] == DBNull.Value ? string.Empty : Convert.ToString(dr["assigne_user_id"]) ?? string.Empty;
                oBeI_nav_transacciones_push.Item_no = dr["item_no"] == DBNull.Value ? string.Empty : Convert.ToString(dr["item_no"]) ?? string.Empty;
                oBeI_nav_transacciones_push.No_orden_prod = dr["no_orden_prod"] == DBNull.Value ? string.Empty : Convert.ToString(dr["no_orden_prod"]) ?? string.Empty;
                oBeI_nav_transacciones_push.Respuesta_push = dr["respuesta_push"] == DBNull.Value ? string.Empty : Convert.ToString(dr["respuesta_push"]) ?? string.Empty;
                oBeI_nav_transacciones_push.Enviado_A_ERP = dr["Enviado_A_ERP"] != DBNull.Value && Convert.ToBoolean(dr["Enviado_A_ERP"]);
                oBeI_nav_transacciones_push.Fec_agr = dr["fec_agr"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fec_agr"]);
                oBeI_nav_transacciones_push.User_agr = dr["user_agr"] == DBNull.Value ? string.Empty : Convert.ToString(dr["user_agr"]) ?? string.Empty;
                oBeI_nav_transacciones_push.Fec_mod = dr["fec_mod"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fec_mod"]);
                oBeI_nav_transacciones_push.User_mod = dr["user_mod"] == DBNull.Value ? string.Empty : Convert.ToString(dr["user_mod"]) ?? string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Insertar(clsBeI_nav_transacciones_push oBeI_nav_transacciones_push, SqlConnection pConection, SqlTransaction pTransaction)
        {
            Ins.Init("i_nav_transacciones_push");
            Ins.Add("idtransaccionpush", "@idtransaccionpush", "F");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idordencompra", "@idordencompra", "F");
            Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Ins.Add("idrecepciondet", "@idrecepciondet", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idproducto", "@idproducto", "F");
            Ins.Add("idunidadmedida", "@idunidadmedida", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idproductoestado", "@idproductoestado", "F");
            Ins.Add("cantidad", "@cantidad", "F");
            Ins.Add("peso", "@peso", "F");
            Ins.Add("lote", "@lote", "F");
            Ins.Add("fecha_vence", "@fecha_vence", "F");
            Ins.Add("no_linea", "@no_linea", "F");
            Ins.Add("codigo_variante", "@codigo_variante", "F");
            Ins.Add("nom_unidad_medida", "@nom_unidad_medida", "F");
            Ins.Add("tipo_transaccion", "@tipo_transaccion", "F");
            Ins.Add("idtipodocumento", "@idtipodocumento", "F");
            Ins.Add("tipo_push", "@tipo_push", "F");
            Ins.Add("no_recepcion_almacen", "@no_recepcion_almacen", "F");
            Ins.Add("documento_ubicacion", "@documento_ubicacion", "F");
            Ins.Add("documento_ingreso", "@documento_ingreso", "F");
            Ins.Add("documento_recepcion", "@documento_recepcion", "F");
            Ins.Add("location_code", "@location_code", "F");
            Ins.Add("zone_code", "@zone_code", "F");
            Ins.Add("bin_code", "@bin_code", "F");
            Ins.Add("assigne_user_id", "@assigne_user_id", "F");
            Ins.Add("item_no", "@item_no", "F");
            Ins.Add("no_orden_prod", "@no_orden_prod", "F");
            Ins.Add("respuesta_push", "@respuesta_push", "F");
            Ins.Add("enviado_a_erp", "@enviado_a_erp", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("user_mod", "@user_mod", "F");

            string sp = Ins.SQL();

            using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@IDTRANSACCIONPUSH", oBeI_nav_transacciones_push.IdTransaccionPush);
                cmd.Parameters.AddWithValue("@IDEMPRESA", oBeI_nav_transacciones_push.IdEmpresa);
                cmd.Parameters.AddWithValue("@IDBODEGA", oBeI_nav_transacciones_push.IdBodega);
                cmd.Parameters.AddWithValue("@IDPROPIETARIOBODEGA", oBeI_nav_transacciones_push.IdPropietariobodega);
                cmd.Parameters.AddWithValue("@IDORDENCOMPRA", oBeI_nav_transacciones_push.IdOrdenCompra);
                cmd.Parameters.AddWithValue("@IDRECEPCIONENC", oBeI_nav_transacciones_push.IdRecepcionEnc);
                cmd.Parameters.AddWithValue("@IDRECEPCIONDET", oBeI_nav_transacciones_push.IdRecepcionDet);
                cmd.Parameters.AddWithValue("@IDPRODUCTOBODEGA", oBeI_nav_transacciones_push.Idproductobodega);
                cmd.Parameters.AddWithValue("@IDPRODUCTO", oBeI_nav_transacciones_push.Idproducto);
                cmd.Parameters.AddWithValue("@IDUNIDADMEDIDA", oBeI_nav_transacciones_push.Idunidadmedida);
                cmd.Parameters.AddWithValue("@IDPRESENTACION", oBeI_nav_transacciones_push.Idpresentacion);
                cmd.Parameters.AddWithValue("@IDPRODUCTOESTADO", oBeI_nav_transacciones_push.Idproductoestado);
                cmd.Parameters.AddWithValue("@CANTIDAD", oBeI_nav_transacciones_push.Cantidad);
                cmd.Parameters.AddWithValue("@PESO", oBeI_nav_transacciones_push.Peso);
                cmd.Parameters.AddWithValue("@LOTE", oBeI_nav_transacciones_push.Lote);
                cmd.Parameters.AddWithValue("@FECHA_VENCE", oBeI_nav_transacciones_push.Fecha_vence);
                cmd.Parameters.AddWithValue("@NO_LINEA", oBeI_nav_transacciones_push.No_linea);
                cmd.Parameters.AddWithValue("@CODIGO_VARIANTE", oBeI_nav_transacciones_push.Codigo_variante);
                cmd.Parameters.AddWithValue("@NOM_UNIDAD_MEDIDA", oBeI_nav_transacciones_push.Nom_unidad_medida);
                cmd.Parameters.AddWithValue("@TIPO_TRANSACCION", oBeI_nav_transacciones_push.Tipo_transaccion);
                cmd.Parameters.AddWithValue("@IDTIPODOCUMENTO", oBeI_nav_transacciones_push.IdTipoDocumento);
                cmd.Parameters.AddWithValue("@TIPO_PUSH", oBeI_nav_transacciones_push.Tipo_push);
                cmd.Parameters.AddWithValue("@NO_RECEPCION_ALMACEN", oBeI_nav_transacciones_push.No_recepcion_almacen);
                cmd.Parameters.AddWithValue("@DOCUMENTO_UBICACION", oBeI_nav_transacciones_push.Documento_ubicacion);
                cmd.Parameters.AddWithValue("@DOCUMENTO_INGRESO", oBeI_nav_transacciones_push.Documento_ingreso);
                cmd.Parameters.AddWithValue("@DOCUMENTO_RECEPCION", oBeI_nav_transacciones_push.Documento_recepcion);
                cmd.Parameters.AddWithValue("@LOCATION_CODE", oBeI_nav_transacciones_push.Location_code);
                cmd.Parameters.AddWithValue("@ZONE_CODE", oBeI_nav_transacciones_push.Zone_code);
                cmd.Parameters.AddWithValue("@BIN_CODE", oBeI_nav_transacciones_push.Bin_code);
                cmd.Parameters.AddWithValue("@ASSIGNE_USER_ID", oBeI_nav_transacciones_push.Assigne_user_id);
                cmd.Parameters.AddWithValue("@ITEM_NO", oBeI_nav_transacciones_push.Item_no);
                cmd.Parameters.AddWithValue("@NO_ORDEN_PROD", oBeI_nav_transacciones_push.No_orden_prod);
                cmd.Parameters.AddWithValue("@RESPUESTA_PUSH", oBeI_nav_transacciones_push.Respuesta_push);
                cmd.Parameters.AddWithValue("@ENVIADO_A_ERP", oBeI_nav_transacciones_push.Enviado_A_ERP);
                cmd.Parameters.AddWithValue("@FEC_AGR", oBeI_nav_transacciones_push.Fec_agr);
                cmd.Parameters.AddWithValue("@USER_AGR", oBeI_nav_transacciones_push.User_agr);
                cmd.Parameters.AddWithValue("@FEC_MOD", oBeI_nav_transacciones_push.Fec_mod);
                cmd.Parameters.AddWithValue("@USER_MOD", oBeI_nav_transacciones_push.User_mod);

                return cmd.ExecuteNonQuery();
            }
        }

        public static int Actualizar(clsBeI_nav_transacciones_push oBeI_nav_transacciones_push, SqlConnection pConection, SqlTransaction pTransaction)
        {
            Upd.Init("i_nav_transacciones_push");
            Upd.Add("idtransaccionpush", "@idtransaccionpush", "F");
            Upd.Add("idempresa", "@idempresa", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Upd.Add("idordencompra", "@idordencompra", "F");
            Upd.Add("idrecepcionenc", "@idrecepcionenc", "F");
            Upd.Add("idrecepciondet", "@idrecepciondet", "F");
            Upd.Add("idproductobodega", "@idproductobodega", "F");
            Upd.Add("idproducto", "@idproducto", "F");
            Upd.Add("idunidadmedida", "@idunidadmedida", "F");
            Upd.Add("idpresentacion", "@idpresentacion", "F");
            Upd.Add("idproductoestado", "@idproductoestado", "F");
            Upd.Add("cantidad", "@cantidad", "F");
            Upd.Add("peso", "@peso", "F");
            Upd.Add("lote", "@lote", "F");
            Upd.Add("fecha_vence", "@fecha_vence", "F");
            Upd.Add("no_linea", "@no_linea", "F");
            Upd.Add("codigo_variante", "@codigo_variante", "F");
            Upd.Add("nom_unidad_medida", "@nom_unidad_medida", "F");
            Upd.Add("tipo_transaccion", "@tipo_transaccion", "F");
            Upd.Add("idtipodocumento", "@idtipodocumento", "F");
            Upd.Add("tipo_push", "@tipo_push", "F");
            Upd.Add("no_recepcion_almacen", "@no_recepcion_almacen", "F");
            Upd.Add("documento_ubicacion", "@documento_ubicacion", "F");
            Upd.Add("documento_ingreso", "@documento_ingreso", "F");
            Upd.Add("documento_recepcion", "@documento_recepcion", "F");
            Upd.Add("location_code", "@location_code", "F");
            Upd.Add("zone_code", "@zone_code", "F");
            Upd.Add("bin_code", "@bin_code", "F");
            Upd.Add("assigne_user_id", "@assigne_user_id", "F");
            Upd.Add("item_no", "@item_no", "F");
            Upd.Add("no_orden_prod", "@no_orden_prod", "F");
            Upd.Add("respuesta_push", "@respuesta_push", "F");
            Upd.Add("enviado_a_erp", "@enviado_a_erp", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Where("IdTransaccionPush = @IdTransaccionPush");

            string sp = Upd.SQL();

            using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@IDTRANSACCIONPUSH", oBeI_nav_transacciones_push.IdTransaccionPush);
                cmd.Parameters.AddWithValue("@IDEMPRESA", oBeI_nav_transacciones_push.IdEmpresa);
                cmd.Parameters.AddWithValue("@IDBODEGA", oBeI_nav_transacciones_push.IdBodega);
                cmd.Parameters.AddWithValue("@IDPROPIETARIOBODEGA", oBeI_nav_transacciones_push.IdPropietariobodega);
                cmd.Parameters.AddWithValue("@IDORDENCOMPRA", oBeI_nav_transacciones_push.IdOrdenCompra);
                cmd.Parameters.AddWithValue("@IDRECEPCIONENC", oBeI_nav_transacciones_push.IdRecepcionEnc);
                cmd.Parameters.AddWithValue("@IDRECEPCIONDET", oBeI_nav_transacciones_push.IdRecepcionDet);
                cmd.Parameters.AddWithValue("@IDPRODUCTOBODEGA", oBeI_nav_transacciones_push.Idproductobodega);
                cmd.Parameters.AddWithValue("@IDPRODUCTO", oBeI_nav_transacciones_push.Idproducto);
                cmd.Parameters.AddWithValue("@IDUNIDADMEDIDA", oBeI_nav_transacciones_push.Idunidadmedida);
                cmd.Parameters.AddWithValue("@IDPRESENTACION", oBeI_nav_transacciones_push.Idpresentacion);
                cmd.Parameters.AddWithValue("@IDPRODUCTOESTADO", oBeI_nav_transacciones_push.Idproductoestado);
                cmd.Parameters.AddWithValue("@CANTIDAD", oBeI_nav_transacciones_push.Cantidad);
                cmd.Parameters.AddWithValue("@PESO", oBeI_nav_transacciones_push.Peso);
                cmd.Parameters.AddWithValue("@LOTE", oBeI_nav_transacciones_push.Lote);
                cmd.Parameters.AddWithValue("@FECHA_VENCE", oBeI_nav_transacciones_push.Fecha_vence);
                cmd.Parameters.AddWithValue("@NO_LINEA", oBeI_nav_transacciones_push.No_linea);
                cmd.Parameters.AddWithValue("@CODIGO_VARIANTE", oBeI_nav_transacciones_push.Codigo_variante);
                cmd.Parameters.AddWithValue("@NOM_UNIDAD_MEDIDA", oBeI_nav_transacciones_push.Nom_unidad_medida);
                cmd.Parameters.AddWithValue("@TIPO_TRANSACCION", oBeI_nav_transacciones_push.Tipo_transaccion);
                cmd.Parameters.AddWithValue("@IDTIPODOCUMENTO", oBeI_nav_transacciones_push.IdTipoDocumento);
                cmd.Parameters.AddWithValue("@TIPO_PUSH", oBeI_nav_transacciones_push.Tipo_push);
                cmd.Parameters.AddWithValue("@NO_RECEPCION_ALMACEN", oBeI_nav_transacciones_push.No_recepcion_almacen);
                cmd.Parameters.AddWithValue("@DOCUMENTO_UBICACION", oBeI_nav_transacciones_push.Documento_ubicacion);
                cmd.Parameters.AddWithValue("@DOCUMENTO_INGRESO", oBeI_nav_transacciones_push.Documento_ingreso);
                cmd.Parameters.AddWithValue("@DOCUMENTO_RECEPCION", oBeI_nav_transacciones_push.Documento_recepcion);
                cmd.Parameters.AddWithValue("@LOCATION_CODE", oBeI_nav_transacciones_push.Location_code);
                cmd.Parameters.AddWithValue("@ZONE_CODE", oBeI_nav_transacciones_push.Zone_code);
                cmd.Parameters.AddWithValue("@BIN_CODE", oBeI_nav_transacciones_push.Bin_code);
                cmd.Parameters.AddWithValue("@ASSIGNE_USER_ID", oBeI_nav_transacciones_push.Assigne_user_id);
                cmd.Parameters.AddWithValue("@ITEM_NO", oBeI_nav_transacciones_push.Item_no);
                cmd.Parameters.AddWithValue("@NO_ORDEN_PROD", oBeI_nav_transacciones_push.No_orden_prod);
                cmd.Parameters.AddWithValue("@RESPUESTA_PUSH", oBeI_nav_transacciones_push.Respuesta_push);
                cmd.Parameters.AddWithValue("@ENVIADO_A_ERP", oBeI_nav_transacciones_push.Enviado_A_ERP);
                cmd.Parameters.AddWithValue("@FEC_AGR", oBeI_nav_transacciones_push.Fec_agr);
                cmd.Parameters.AddWithValue("@USER_AGR", oBeI_nav_transacciones_push.User_agr);
                cmd.Parameters.AddWithValue("@FEC_MOD", oBeI_nav_transacciones_push.Fec_mod);
                cmd.Parameters.AddWithValue("@USER_MOD", oBeI_nav_transacciones_push.User_mod);

                return cmd.ExecuteNonQuery();
            }
        }

        public static int Eliminar(clsBeI_nav_transacciones_push oBeI_nav_transacciones_push, SqlConnection pConection, SqlTransaction pTransaction)
        {
            const string sp = @"DELETE FROM I_nav_transacciones_push 
                           WHERE IdTransaccionPush = @IdTransaccionPush";

            using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IDTRANSACCIONPUSH", oBeI_nav_transacciones_push.IdTransaccionPush);
                return cmd.ExecuteNonQuery();
            }
        }

        public static List<clsBeI_nav_transacciones_push> Get_All(IConfiguration config)
        {
            List<clsBeI_nav_transacciones_push> lReturnList = new List<clsBeI_nav_transacciones_push>();
            const string sp = "SELECT * FROM I_nav_transacciones_push";

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {
                lConnection.Open();
                using (SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                    {
                        lDTA.SelectCommand.CommandType = CommandType.Text;
                        lDTA.SelectCommand.Transaction = lTransaction;
                        DataTable lDataTable = new DataTable();
                        lDTA.Fill(lDataTable);

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            clsBeI_nav_transacciones_push vBeI_nav_transacciones_push = new clsBeI_nav_transacciones_push();
                            Cargar(vBeI_nav_transacciones_push, dr);
                            lReturnList.Add(vBeI_nav_transacciones_push);
                        }

                        lTransaction.Commit();
                    }
                }
            }

            return lReturnList;
        }

        public static void GetSingle(IConfiguration config, clsBeI_nav_transacciones_push pBeI_nav_transacciones_push)
        {
            const string sp = @"SELECT * FROM I_nav_transacciones_push 
                           WHERE IdTransaccionPush = @IdTransaccionPush";

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {
                lConnection.Open();
                using (SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                    {
                        lDTA.SelectCommand.CommandType = CommandType.Text;
                        lDTA.SelectCommand.Transaction = lTransaction;
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTransaccionPush", pBeI_nav_transacciones_push.IdTransaccionPush);

                        DataTable lDataTable = new DataTable();
                        lDTA.Fill(lDataTable);

                        if (lDataTable != null && lDataTable.Rows.Count > 0)
                        {
                            Cargar(pBeI_nav_transacciones_push, lDataTable.Rows[0]);
                        }

                        lTransaction.Commit();
                    }
                }
            }
        }

        public static int MaxID(IConfiguration config)
        {
            const string sp = "SELECT ISNULL(Max(IdTransaccionPush),0) FROM I_nav_transacciones_push";

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {
                lConnection.Open();
                using (SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    using (SqlCommand lCommand = new SqlCommand(sp, lConnection, lTransaction))
                    {
                        lCommand.CommandType = CommandType.Text;
                        object lReturnValue = lCommand.ExecuteScalar();

                        if (lReturnValue != DBNull.Value && lReturnValue != null)
                        {
                            return Convert.ToInt32(lReturnValue);
                        }

                        lTransaction.Commit();
                        return 0;
                    }
                }
            }
        }

        public static int Guardar_Transaccion_Existente(string DocumentoUbicacion,
                                                       string Recepcion_Almacen,
                                                       string Tipo_Push,
                                                       string Error_Push,
                                                       int IdRecepcionEnc,
                                                       int IdRecepcionDet,
                                                       int IdUsuario,
                                                       SqlConnection lConnection,
                                                       SqlTransaction lTransaction)
        {
            try
            {
                clsBeI_nav_transacciones_push? oBeI_nav_transacciones_push = new clsBeI_nav_transacciones_push();
                clsBeTrans_re_det? beTransReDet = new clsBeTrans_re_det();
                clsBeTrans_re_enc? beTransReEnc = new clsBeTrans_re_enc();
                int pIdEmpresa;
                int rowsAffected = 0;

                int vMax = MaxID(lConnection, lTransaction);

                beTransReEnc = clsLnTrans_re_enc.GetSingle(IdRecepcionEnc, lConnection, lTransaction);

                if (beTransReEnc?.OrdenCompraRec?.OC == null)
                    throw new Exception("No se pudo obtener la información de la orden de compra");

                pIdEmpresa = clsLnPropietario_bodega.GetIdEmpresa_By_IdPropietarioBodega(beTransReEnc.OrdenCompraRec.OC.IdPropietarioBodega,
                                                                                         lConnection, lTransaction);

                beTransReDet = beTransReEnc.Detalle?.Find(x => x.IdRecepcionDet == IdRecepcionDet);

                if (beTransReDet == null)
                    throw new Exception($"No se encontró el detalle de recepción con IdRecepcionDet: {IdRecepcionDet}");

                oBeI_nav_transacciones_push.IdRecepcionEnc = beTransReDet.IdRecepcionEnc;
                oBeI_nav_transacciones_push.IdRecepcionDet = beTransReDet.IdRecepcionDet;
                GetSingle_By_RecepcionDet(oBeI_nav_transacciones_push, lConnection, lTransaction);

                if (oBeI_nav_transacciones_push?.IdTransaccionPush == 0)
                {
                    oBeI_nav_transacciones_push = new clsBeI_nav_transacciones_push();

                    oBeI_nav_transacciones_push.IdTransaccionPush = vMax + 1;
                    oBeI_nav_transacciones_push.IdEmpresa = pIdEmpresa;
                    oBeI_nav_transacciones_push.IdBodega = beTransReEnc.IdBodega;
                    oBeI_nav_transacciones_push.IdPropietariobodega = beTransReEnc.OrdenCompraRec.OC.IdPropietarioBodega;
                    oBeI_nav_transacciones_push.IdOrdenCompra = beTransReEnc.OrdenCompraRec.IdOrdenCompraEnc;
                    oBeI_nav_transacciones_push.IdRecepcionEnc = beTransReDet.IdRecepcionEnc;
                    oBeI_nav_transacciones_push.IdRecepcionDet = beTransReDet.IdRecepcionDet;
                    oBeI_nav_transacciones_push.Idproductobodega = beTransReDet.IdProductoBodega;
                    oBeI_nav_transacciones_push.Idproducto = beTransReDet.Producto?.IdProducto ?? 0;
                    oBeI_nav_transacciones_push.Idunidadmedida = beTransReDet.IdUnidadMedida;
                    oBeI_nav_transacciones_push.Idpresentacion = beTransReDet.IdPresentacion;
                    oBeI_nav_transacciones_push.Idproductoestado = beTransReDet.IdProductoEstado;

                    if (Tipo_Push == "Push_Recepcion_Devolucion_Venta_To_NAV_For_BYB")
                    {
                        oBeI_nav_transacciones_push.Cantidad = beTransReDet.Cantidad_recibida;
                    }
                    else
                    {
                        if (beTransReDet.IdPresentacion != 0 && beTransReDet.Presentacion?.Factor > 0)
                        {
                            oBeI_nav_transacciones_push.Cantidad = beTransReDet.Cantidad_recibida * beTransReDet.Presentacion.Factor;
                        }
                        else
                        {
                            oBeI_nav_transacciones_push.Cantidad = beTransReDet.Cantidad_recibida;
                        }
                    }

                    oBeI_nav_transacciones_push.Peso = beTransReDet.Peso;
                    oBeI_nav_transacciones_push.Lote = beTransReDet.Lote ?? string.Empty;
                    oBeI_nav_transacciones_push.Fecha_vence = beTransReDet.Fecha_vence;
                    oBeI_nav_transacciones_push.No_linea = beTransReDet.No_Linea.ToString();
                    oBeI_nav_transacciones_push.Codigo_variante = beTransReDet.Atributo_variante_1 ?? string.Empty;
                    oBeI_nav_transacciones_push.Nom_unidad_medida = beTransReDet.Nombre_unidad_medida ?? string.Empty;
                    oBeI_nav_transacciones_push.Tipo_transaccion = "INGRESO";
                    oBeI_nav_transacciones_push.IdTipoDocumento = beTransReEnc.OrdenCompraRec.OC.IdTipoIngresoOC;
                    oBeI_nav_transacciones_push.Tipo_push = Tipo_Push;
                    oBeI_nav_transacciones_push.No_recepcion_almacen = Recepcion_Almacen ?? string.Empty;
                    oBeI_nav_transacciones_push.Documento_ubicacion = DocumentoUbicacion ?? string.Empty;
                    oBeI_nav_transacciones_push.Documento_ingreso = beTransReEnc.OrdenCompraRec.OC.Referencia ?? string.Empty;
                    oBeI_nav_transacciones_push.Documento_recepcion = beTransReEnc.OrdenCompraRec.OC.No_Documento_Recepcion_ERP ?? string.Empty;
                    oBeI_nav_transacciones_push.Location_code = string.Empty;
                    oBeI_nav_transacciones_push.Zone_code = string.Empty;
                    oBeI_nav_transacciones_push.Bin_code = string.Empty;
                    oBeI_nav_transacciones_push.Assigne_user_id = string.Empty;
                    oBeI_nav_transacciones_push.Item_no = string.Empty;
                    oBeI_nav_transacciones_push.No_orden_prod = string.Empty;
                    oBeI_nav_transacciones_push.Respuesta_push = Error_Push ?? string.Empty;
                    oBeI_nav_transacciones_push.Enviado_A_ERP = false;
                    oBeI_nav_transacciones_push.Fec_agr = DateTime.Now;
                    oBeI_nav_transacciones_push.User_agr = IdUsuario.ToString();
                    oBeI_nav_transacciones_push.Fec_mod = DateTime.Now;
                    oBeI_nav_transacciones_push.User_mod = IdUsuario.ToString();

                    rowsAffected = Insertar(oBeI_nav_transacciones_push, lConnection, lTransaction);
                }
                else
                {
                    if (oBeI_nav_transacciones_push != null)
                    {
                        oBeI_nav_transacciones_push.Tipo_push = Tipo_Push;
                        oBeI_nav_transacciones_push.Respuesta_push = Error_Push ?? string.Empty;
                        oBeI_nav_transacciones_push.Enviado_A_ERP = false;
                        oBeI_nav_transacciones_push.Fec_mod = DateTime.Now;
                        oBeI_nav_transacciones_push.User_mod = IdUsuario.ToString();
                        oBeI_nav_transacciones_push.No_recepcion_almacen = Recepcion_Almacen ?? string.Empty;
                        oBeI_nav_transacciones_push.Documento_ubicacion = DocumentoUbicacion ?? string.Empty;

                        rowsAffected = Actualizar(oBeI_nav_transacciones_push, lConnection, lTransaction);
                    }
                }

                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void GetSingle_By_RecepcionDet(clsBeI_nav_transacciones_push pBeI_nav_transacciones_push,
                                                    SqlConnection pConnection,
                                                    SqlTransaction pTransaction)
        {
            const string sp = @"SELECT * FROM I_nav_transacciones_push 
                       WHERE (IdRecepcionEnc = @IdRecepcionEnc) 
                       AND (IdRecepcionDet = @IdRecepcionDet)";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, pConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = pTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pBeI_nav_transacciones_push.IdRecepcionEnc);
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionDet", pBeI_nav_transacciones_push.IdRecepcionDet);

                DataTable lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable != null && lDataTable.Rows.Count > 0)
                {
                    Cargar(pBeI_nav_transacciones_push, lDataTable.Rows[0]);
                }
                else
                {
                    pBeI_nav_transacciones_push.IdTransaccionPush = 0;
                }
            }
        }
        public static int MaxID(SqlConnection pConnection, SqlTransaction pTransaction)
        {
            const string sp = "SELECT ISNULL(Max(IdTransaccionPush),0) FROM I_nav_transacciones_push";

            using (SqlCommand lCommand = new SqlCommand(sp, pConnection, pTransaction))
            {
                lCommand.CommandType = CommandType.Text;
                object lReturnValue = lCommand.ExecuteScalar();

                if (lReturnValue != DBNull.Value && lReturnValue != null)
                {
                    return Convert.ToInt32(lReturnValue);
                }

                return 0;
            }
        }
    }
}