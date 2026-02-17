namespace WMS.DALCore.Transacciones
{
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using WMS.EntityCore;
    using WMS.EntityCore.Producto;
    using WMS.EntityCore.Trans_oc;
    using WMS.EntityCore.Trans_re;
    using WMS.EntityCore.Transacciones;
    using WMSWebAPI.Be;

    public static class clsLnI_nav_transacciones_out
    {
        private static clsInsert Ins = new clsInsert();
        private static clsUpdate Upd = new clsUpdate();
        public static void Cargar(ref clsBeI_nav_transacciones_out oBeI_nav_transacciones_out, DataRow dr)
        {
            oBeI_nav_transacciones_out.Idtransaccion = Convert.ToInt32(dr["idtransaccion"] ?? 0);
            oBeI_nav_transacciones_out.Idempresa = Convert.ToInt32(dr["idempresa"] ?? 0);
            oBeI_nav_transacciones_out.Idbodega = Convert.ToInt32(dr["idbodega"] ?? 0);
            oBeI_nav_transacciones_out.Idpropietario = Convert.ToInt32(dr["idpropietario"] ?? 0);
            oBeI_nav_transacciones_out.Idpropietariobodega = Convert.ToInt32(dr["idpropietariobodega"] ?? 0);
            oBeI_nav_transacciones_out.Idordencompra = Convert.ToInt32(dr["idordencompra"] ?? 0);
            oBeI_nav_transacciones_out.Idrecepcionenc = Convert.ToInt32(dr["idrecepcionenc"] ?? 0);
            oBeI_nav_transacciones_out.Idpedidoenc = Convert.ToInt32(dr["idpedidoenc"] ?? 0);
            oBeI_nav_transacciones_out.Iddespachoenc = Convert.ToInt32(dr["iddespachoenc"] ?? 0);
            oBeI_nav_transacciones_out.Idproductobodega = Convert.ToInt32(dr["idproductobodega"] ?? 0);
            oBeI_nav_transacciones_out.Idproducto = Convert.ToInt32(dr["idproducto"] ?? 0);
            oBeI_nav_transacciones_out.Idunidadmedida = Convert.ToInt32(dr["idunidadmedida"] ?? 0);
            oBeI_nav_transacciones_out.Idpresentacion = Convert.ToInt32(dr["idpresentacion"] ?? 0);
            oBeI_nav_transacciones_out.Idproductoestado = Convert.ToInt32(dr["idproductoestado"] ?? 0);
            oBeI_nav_transacciones_out.Cantidad = Convert.ToDouble(dr["cantidad"] ?? 0.0);
            oBeI_nav_transacciones_out.Peso = Convert.ToDouble(dr["peso"] ?? 0.0);
            oBeI_nav_transacciones_out.Lote = dr["lote"]?.ToString() ?? "";
            oBeI_nav_transacciones_out.Fecha_vence = dr["fecha_vence"] == DBNull.Value ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dr["fecha_vence"]);
            oBeI_nav_transacciones_out.Fecha_recepcion = dr["fecha_recepcion"] == DBNull.Value ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dr["fecha_recepcion"]);
            oBeI_nav_transacciones_out.No_pedido = dr["no_pedido"]?.ToString() ?? "";
            oBeI_nav_transacciones_out.No_linea = dr["no_linea"]?.ToString() ?? "";
            oBeI_nav_transacciones_out.Codigo_producto = dr["codigo_producto"]?.ToString() ?? "";
            oBeI_nav_transacciones_out.Nombre_producto = dr["nombre_producto"]?.ToString() ?? "";
            oBeI_nav_transacciones_out.Codigo_variante = dr["codigo_variante"]?.ToString() ?? "";
            oBeI_nav_transacciones_out.Unidad_medida = dr["unidad_medida"]?.ToString() ?? "";
            oBeI_nav_transacciones_out.Tipo_transaccion = dr["tipo_transaccion"]?.ToString() ?? "";
            oBeI_nav_transacciones_out.Enviado = Convert.ToBoolean(dr["enviado"] ?? false);
            oBeI_nav_transacciones_out.Fec_agr = dr["fec_agr"] as DateTime? ?? DateTime.Now;
            oBeI_nav_transacciones_out.User_agr = dr["user_agr"]?.ToString() ?? "";
            oBeI_nav_transacciones_out.Fec_mod = dr["fec_mod"] as DateTime? ?? DateTime.Now;
            oBeI_nav_transacciones_out.User_mod = dr["user_mod"]?.ToString() ?? "";
            oBeI_nav_transacciones_out.Cantidad_Esperada = Convert.ToDouble(dr["Cantidad_Esperada"] ?? 0.0);
            oBeI_nav_transacciones_out.Lic_Plate = dr["lic_plate"]?.ToString() ?? "";
            oBeI_nav_transacciones_out.Uds_Lic_Plate = Convert.ToDouble(dr["uds_lic_plate"] ?? 0.0);
            oBeI_nav_transacciones_out.Cantidad_Presentacion = Convert.ToDouble(dr["cantidad_presentacion"] ?? 0.0);
            oBeI_nav_transacciones_out.IdTipoDocumento = Convert.ToInt32(dr["IdTipoDocumento"] ?? 0);
            oBeI_nav_transacciones_out.codigo_barra = dr["codigo_barra"]?.ToString() ?? "";
            oBeI_nav_transacciones_out.valor_aduana = Convert.ToInt32(dr["valor_aduana"] ?? 0);
            oBeI_nav_transacciones_out.valor_fob = Convert.ToInt32(dr["valor_fob"] ?? 0);
            oBeI_nav_transacciones_out.valor_iva = Convert.ToInt32(dr["valor_iva"] ?? 0);
            oBeI_nav_transacciones_out.valor_dai = Convert.ToInt32(dr["valor_dai"] ?? 0);
            oBeI_nav_transacciones_out.valor_seguro = Convert.ToInt32(dr["valor_seguro"] ?? 0);
            oBeI_nav_transacciones_out.valor_flete = Convert.ToInt32(dr["valor_flete"] ?? 0);
            oBeI_nav_transacciones_out.peso_neto = Convert.ToInt32(dr["peso_neto"] ?? 0);
            oBeI_nav_transacciones_out.peso_bruto = Convert.ToInt32(dr["peso_bruto"] ?? 0);
            oBeI_nav_transacciones_out.fecha_despacho = dr["fecha_despacho"] == DBNull.Value ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dr["fecha_despacho"]);
            oBeI_nav_transacciones_out.no_documento_salida_ref_devol = dr["no_documento_salida_ref_devol"]?.ToString() ?? "";
            oBeI_nav_transacciones_out.IdPedidoEncDevol =    dr["IdPedidoEncDevol"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdPedidoEncDevol"]);
            oBeI_nav_transacciones_out.IdDespachoDet =dr["IdDespachoDet"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdDespachoDet"]);
            oBeI_nav_transacciones_out.IdRecepcionDet =dr["IdRecepcionDet"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdRecepcionDet"]);
            oBeI_nav_transacciones_out.Cantidad_Enviada =dr["Cantidad_Enviada"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Cantidad_Enviada"]);
            oBeI_nav_transacciones_out.Cantidad_Pendiente =dr["Cantidad_Pendiente"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Cantidad_Pendiente"]);
            oBeI_nav_transacciones_out.Auditar =dr["Auditar"] == DBNull.Value ? false : Convert.ToBoolean(dr["Auditar"]);
            oBeI_nav_transacciones_out.IdProductoTallaColor =dr["IdProductoTallaColor"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdProductoTallaColor"]);
            oBeI_nav_transacciones_out.Talla=oBeI_nav_transacciones_out.Talla = dr["Talla"]?.ToString() ?? "";
            oBeI_nav_transacciones_out.Color = dr["Color"]?.ToString() ?? "";
        }

        public static int Insertar(ref clsBeI_nav_transacciones_out oBeI_nav_transacciones_out, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Ins.Init("i_nav_transacciones_out");
                Ins.Add("idtransaccion", "@idtransaccion", "F");
                Ins.Add("idempresa", "@idempresa", "F");
                Ins.Add("idbodega", "@idbodega", "F");
                Ins.Add("idpropietario", "@idpropietario", "F");
                Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
                Ins.Add("idordencompra", "@idordencompra", "F");
                Ins.Add("idrecepcionenc", "@idrecepcionenc", "F");
                Ins.Add("idpedidoenc", "@idpedidoenc", "F");
                Ins.Add("iddespachoenc", "@iddespachoenc", "F");
                Ins.Add("idproductobodega", "@idproductobodega", "F");
                Ins.Add("idproducto", "@idproducto", "F");
                Ins.Add("idunidadmedida", "@idunidadmedida", "F");
                Ins.Add("idpresentacion", "@idpresentacion", "F");
                Ins.Add("idproductoestado", "@idproductoestado", "F");
                Ins.Add("cantidad", "@cantidad", "F");
                Ins.Add("peso", "@peso", "F");
                Ins.Add("lote", "@lote", "F");
                Ins.Add("fecha_vence", "@fecha_vence", "F");
                Ins.Add("fecha_recepcion", "@fecha_recepcion", "F");
                Ins.Add("no_pedido", "@no_pedido", "F");
                Ins.Add("no_linea", "@no_linea", "F");
                Ins.Add("codigo_producto", "@codigo_producto", "F");
                Ins.Add("nombre_producto", "@nombre_producto", "F");
                Ins.Add("codigo_variante", "@codigo_variante", "F");
                Ins.Add("unidad_medida", "@unidad_medida", "F");
                Ins.Add("tipo_transaccion", "@tipo_transaccion", "F");
                Ins.Add("enviado", "@enviado", "F");
                Ins.Add("fec_agr", "@fec_agr", "F");
                Ins.Add("user_agr", "@user_agr", "F");
                Ins.Add("fec_mod", "@fec_mod", "F");
                Ins.Add("user_mod", "@user_mod", "F");
                Ins.Add("cantidad_esperada", "@cantidad_esperada", "F");
                Ins.Add("lic_plate", "@lic_plate", "F");
                Ins.Add("uds_lic_plate", "@uds_lic_plate", "F");
                Ins.Add("cantidad_presentacion", "@cantidad_presentacion", "F");
                Ins.Add("IdTipoDocumento", "@IdTipoDocumento", "F");
                Ins.Add("codigo_barra", "@codigo_barra", "F");
                Ins.Add("valor_aduana", "@valor_aduana", "F");
                Ins.Add("valor_fob", "@valor_fob", "F");
                Ins.Add("valor_iva", "@valor_iva", "F");
                Ins.Add("valor_dai", "@valor_dai", "F");
                Ins.Add("valor_seguro", "@valor_seguro", "F");
                Ins.Add("valor_flete", "@valor_flete", "F");
                Ins.Add("peso_neto", "@peso_neto", "F");
                Ins.Add("peso_bruto", "@peso_bruto", "F");
                Ins.Add("fecha_despacho", "@fecha_despacho", "F");
                Ins.Add("no_documento_salida_ref_devol", "@no_documento_salida_ref_devol", "F");
                Ins.Add("IdPedidoEncDevol", "@IdPedidoEncDevol", "F");
                Ins.Add("IdDespachoDet", "@IdDespachoDet", "F");
                Ins.Add("IdRecepcionDet", "@IdRecepcionDet", "F");
                Ins.Add("cantidad_enviada", "@cantidad_enviada", "F");
                Ins.Add("cantidad_pendiente", "@cantidad_pendiente", "F");
                Ins.Add("auditar", "@auditar", "F");
                Ins.Add("IdProductoTallaColor", "@IdProductoTallaColor", "F");
                Ins.Add("Talla", "@Talla", "F");
                Ins.Add("Color", "@Color", "F");
                string sp = Ins.SQL();

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@IDTRANSACCION", oBeI_nav_transacciones_out.Idtransaccion));
                    cmd.Parameters.Add(new SqlParameter("@IDEMPRESA", oBeI_nav_transacciones_out.Idempresa));
                    cmd.Parameters.Add(new SqlParameter("@IDBODEGA", oBeI_nav_transacciones_out.Idbodega));
                    cmd.Parameters.Add(new SqlParameter("@IDPROPIETARIO", oBeI_nav_transacciones_out.Idpropietario));
                    cmd.Parameters.Add(new SqlParameter("@IDPROPIETARIOBODEGA", oBeI_nav_transacciones_out.Idpropietariobodega));
                    cmd.Parameters.Add(new SqlParameter("@IDORDENCOMPRA", oBeI_nav_transacciones_out.Idordencompra));
                    cmd.Parameters.Add(new SqlParameter("@IDRECEPCIONENC", oBeI_nav_transacciones_out.Idrecepcionenc));
                    cmd.Parameters.Add(new SqlParameter("@IDPEDIDOENC", oBeI_nav_transacciones_out.Idpedidoenc));
                    cmd.Parameters.Add(new SqlParameter("@IDDESPACHOENC", oBeI_nav_transacciones_out.Iddespachoenc));
                    cmd.Parameters.Add(new SqlParameter("@IDPRODUCTOBODEGA", oBeI_nav_transacciones_out.Idproductobodega));
                    cmd.Parameters.Add(new SqlParameter("@IDPRODUCTO", oBeI_nav_transacciones_out.Idproducto));
                    cmd.Parameters.Add(new SqlParameter("@IDUNIDADMEDIDA", oBeI_nav_transacciones_out.Idunidadmedida));
                    cmd.Parameters.Add(new SqlParameter("@IDPRESENTACION", oBeI_nav_transacciones_out.Idpresentacion));
                    cmd.Parameters.Add(new SqlParameter("@IDPRODUCTOESTADO", oBeI_nav_transacciones_out.Idproductoestado));
                    cmd.Parameters.Add(new SqlParameter("@CANTIDAD", oBeI_nav_transacciones_out.Cantidad));
                    cmd.Parameters.Add(new SqlParameter("@PESO", oBeI_nav_transacciones_out.Peso));
                    cmd.Parameters.Add(new SqlParameter("@LOTE", oBeI_nav_transacciones_out.Lote ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_VENCE", oBeI_nav_transacciones_out.Fecha_vence == new DateTime(1900, 1, 1) ? (object)DBNull.Value : oBeI_nav_transacciones_out.Fecha_vence));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_RECEPCION", oBeI_nav_transacciones_out.Fecha_recepcion));
                    cmd.Parameters.Add(new SqlParameter("@NO_PEDIDO", oBeI_nav_transacciones_out.No_pedido ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@NO_LINEA", oBeI_nav_transacciones_out.No_linea ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@CODIGO_PRODUCTO", oBeI_nav_transacciones_out.Codigo_producto ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@NOMBRE_PRODUCTO", oBeI_nav_transacciones_out.Nombre_producto ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@CODIGO_VARIANTE", oBeI_nav_transacciones_out.Codigo_variante ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@UNIDAD_MEDIDA", oBeI_nav_transacciones_out.Unidad_medida ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@TIPO_TRANSACCION", oBeI_nav_transacciones_out.Tipo_transaccion ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@ENVIADO", oBeI_nav_transacciones_out.Enviado));
                    cmd.Parameters.Add(new SqlParameter("@FEC_AGR", oBeI_nav_transacciones_out.Fec_agr));
                    cmd.Parameters.Add(new SqlParameter("@USER_AGR", oBeI_nav_transacciones_out.User_agr));
                    cmd.Parameters.Add(new SqlParameter("@FEC_MOD", oBeI_nav_transacciones_out.Fec_mod));
                    cmd.Parameters.Add(new SqlParameter("@USER_MOD", oBeI_nav_transacciones_out.User_mod));
                    cmd.Parameters.Add(new SqlParameter("@CANTIDAD_ESPERADA", oBeI_nav_transacciones_out.Cantidad_Esperada));
                    cmd.Parameters.Add(new SqlParameter("@LIC_PLATE", oBeI_nav_transacciones_out.Lic_Plate ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@UDS_LIC_PLATE", oBeI_nav_transacciones_out.Uds_Lic_Plate));
                    cmd.Parameters.Add(new SqlParameter("@CANTIDAD_PRESENTACION", oBeI_nav_transacciones_out.Cantidad_Presentacion));
                    cmd.Parameters.Add(new SqlParameter("@IDTIPODOCUMENTO", oBeI_nav_transacciones_out.IdTipoDocumento));
                    cmd.Parameters.Add(new SqlParameter("@CODIGO_BARRA", oBeI_nav_transacciones_out.codigo_barra ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_ADUANA", oBeI_nav_transacciones_out.valor_aduana));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_FOB", oBeI_nav_transacciones_out.valor_fob));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_IVA", oBeI_nav_transacciones_out.valor_iva));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_DAI", oBeI_nav_transacciones_out.valor_dai));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_SEGURO", oBeI_nav_transacciones_out.valor_seguro));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_FLETE", oBeI_nav_transacciones_out.valor_flete));
                    cmd.Parameters.Add(new SqlParameter("@PESO_NETO", oBeI_nav_transacciones_out.peso_neto));
                    cmd.Parameters.Add(new SqlParameter("@PESO_BRUTO", oBeI_nav_transacciones_out.peso_bruto));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_DESPACHO", oBeI_nav_transacciones_out.fecha_despacho == new DateTime(1900, 1, 1) ? DBNull.Value : (object)oBeI_nav_transacciones_out.fecha_despacho));
                    cmd.Parameters.Add(new SqlParameter("@NO_DOCUMENTO_SALIDA_REF_DEVOL", oBeI_nav_transacciones_out.no_documento_salida_ref_devol ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@IDPEDIDOENCDEVOL", oBeI_nav_transacciones_out.IdPedidoEncDevol));
                    cmd.Parameters.Add(new SqlParameter("@IDDESPACHODET", oBeI_nav_transacciones_out.IdDespachoDet));
                    cmd.Parameters.Add(new SqlParameter("@IDRECEPCIONDET", oBeI_nav_transacciones_out.IdRecepcionDet));
                    cmd.Parameters.Add(new SqlParameter("@CANTIDAD_ENVIADA", oBeI_nav_transacciones_out.Cantidad_Enviada));
                    cmd.Parameters.Add(new SqlParameter("@CANTIDAD_PENDIENTE", oBeI_nav_transacciones_out.Cantidad_Pendiente));
                    cmd.Parameters.Add(new SqlParameter("@AUDITAR", oBeI_nav_transacciones_out.Auditar));
                    cmd.Parameters.Add(new SqlParameter("@IDPRODUCTOTALLACOLOR", oBeI_nav_transacciones_out.IdProductoTallaColor));
                    cmd.Parameters.Add(new SqlParameter("@TALLA", oBeI_nav_transacciones_out.Talla ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@COLOR", oBeI_nav_transacciones_out.Color ?? string.Empty));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Actualizar(ref clsBeI_nav_transacciones_out oBeI_nav_transacciones_out, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Upd.Init("i_nav_transacciones_out");
                Upd.Add("idtransaccion", "@idtransaccion", "F");
                Upd.Add("idempresa", "@idempresa", "F");
                Upd.Add("idbodega", "@idbodega", "F");
                Upd.Add("idpropietario", "@idpropietario", "F");
                Upd.Add("idpropietariobodega", "@idpropietariobodega", "F");
                Upd.Add("idordencompra", "@idordencompra", "F");
                Upd.Add("idrecepcionenc", "@idrecepcionenc", "F");
                Upd.Add("idpedidoenc", "@idpedidoenc", "F");
                Upd.Add("iddespachoenc", "@iddespachoenc", "F");
                Upd.Add("idproductobodega", "@idproductobodega", "F");
                Upd.Add("idproducto", "@idproducto", "F");
                Upd.Add("idunidadmedida", "@idunidadmedida", "F");
                Upd.Add("idpresentacion", "@idpresentacion", "F");
                Upd.Add("idproductoestado", "@idproductoestado", "F");
                Upd.Add("cantidad", "@cantidad", "F");
                Upd.Add("peso", "@peso", "F");
                Upd.Add("lote", "@lote", "F");
                Upd.Add("fecha_vence", "@fecha_vence", "F");
                Upd.Add("fecha_recepcion", "@fecha_recepcion", "F");
                Upd.Add("no_pedido", "@no_pedido", "F");
                Upd.Add("no_linea", "@no_linea", "F");
                Upd.Add("codigo_producto", "@codigo_producto", "F");
                Upd.Add("nombre_producto", "@nombre_producto", "F");
                Upd.Add("codigo_variante", "@codigo_variante", "F");
                Upd.Add("unidad_medida", "@unidad_medida", "F");
                Upd.Add("tipo_transaccion", "@tipo_transaccion", "F");
                Upd.Add("enviado", "@enviado", "F");
                Upd.Add("fec_agr", "@fec_agr", "F");
                Upd.Add("user_agr", "@user_agr", "F");
                Upd.Add("fec_mod", "@fec_mod", "F");
                Upd.Add("user_mod", "@user_mod", "F");
                Upd.Add("cantidad_esperada", "@cantidad_esperada", "F");
                Upd.Add("lic_plate", "@lic_plate", "F");
                Upd.Add("uds_lic_plate", "@uds_lic_plate", "F");
                Upd.Add("cantidad_presentacion", "@cantidad_presentacion", "F");
                Upd.Add("IdTipoDocumento", "@IdTipoDocumento", "F");
                Upd.Add("codigo_barra", "@codigo_barra", "F");
                Upd.Add("valor_aduana", "@valor_aduana", "F");
                Upd.Add("valor_fob", "@valor_fob", "F");
                Upd.Add("valor_iva", "@valor_iva", "F");
                Upd.Add("valor_dai", "@valor_dai", "F");
                Upd.Add("valor_seguro", "@valor_seguro", "F");
                Upd.Add("valor_flete", "@valor_flete", "F");
                Upd.Add("peso_neto", "@peso_neto", "F");
                Upd.Add("peso_bruto", "@peso_bruto", "F");
                Upd.Add("fecha_despacho", "@fecha_despacho", "F");
                Upd.Add("no_documento_salida_ref_devol", "@no_documento_salida_ref_devol", "F");
                Upd.Add("IdPedidoEncDevol", "@IdPedidoEncDevol", "F");
                Upd.Add("IdDespachoDet", "@IdDespachoDet", "F");
                Upd.Add("IdRecepcionDet", "@IdRecepcionDet", "F");
                Upd.Add("cantidad_enviada", "@cantidad_enviada", "F");
                Upd.Add("cantidad_pendiente", "@cantidad_pendiente", "F");
                Upd.Add("auditar", "@auditar", "F");
                Upd.Add("IdProductoTallaColor", "@IdProductoTallaColor", "F");
                Upd.Add("Talla", "@Talla", "F");
                Upd.Add("Color", "@Color", "F");
                Upd.Where("Idtransaccion = @Idtransaccion");

                string sp = Upd.SQL();

                using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@IDTRANSACCION", oBeI_nav_transacciones_out.Idtransaccion));
                    cmd.Parameters.Add(new SqlParameter("@IDEMPRESA", oBeI_nav_transacciones_out.Idempresa));
                    cmd.Parameters.Add(new SqlParameter("@IDBODEGA", oBeI_nav_transacciones_out.Idbodega));
                    cmd.Parameters.Add(new SqlParameter("@IDPROPIETARIO", oBeI_nav_transacciones_out.Idpropietario));
                    cmd.Parameters.Add(new SqlParameter("@IDPROPIETARIOBODEGA", oBeI_nav_transacciones_out.Idpropietariobodega));
                    cmd.Parameters.Add(new SqlParameter("@IDORDENCOMPRA", oBeI_nav_transacciones_out.Idordencompra));
                    cmd.Parameters.Add(new SqlParameter("@IDRECEPCIONENC", oBeI_nav_transacciones_out.Idrecepcionenc));
                    cmd.Parameters.Add(new SqlParameter("@IDPEDIDOENC", oBeI_nav_transacciones_out.Idpedidoenc));
                    cmd.Parameters.Add(new SqlParameter("@IDDESPACHOENC", oBeI_nav_transacciones_out.Iddespachoenc));
                    cmd.Parameters.Add(new SqlParameter("@IDPRODUCTOBODEGA", oBeI_nav_transacciones_out.Idproductobodega));
                    cmd.Parameters.Add(new SqlParameter("@IDPRODUCTO", oBeI_nav_transacciones_out.Idproducto));
                    cmd.Parameters.Add(new SqlParameter("@IDUNIDADMEDIDA", oBeI_nav_transacciones_out.Idunidadmedida));
                    cmd.Parameters.Add(new SqlParameter("@IDPRESENTACION", oBeI_nav_transacciones_out.Idpresentacion));
                    cmd.Parameters.Add(new SqlParameter("@IDPRODUCTOESTADO", oBeI_nav_transacciones_out.Idproductoestado));
                    cmd.Parameters.Add(new SqlParameter("@CANTIDAD", oBeI_nav_transacciones_out.Cantidad));
                    cmd.Parameters.Add(new SqlParameter("@PESO", oBeI_nav_transacciones_out.Peso));
                    cmd.Parameters.Add(new SqlParameter("@LOTE", oBeI_nav_transacciones_out.Lote ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_VENCE", oBeI_nav_transacciones_out.Fecha_vence == new DateTime(1900, 1, 1) ? (object)DBNull.Value : oBeI_nav_transacciones_out.Fecha_vence));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_RECEPCION", oBeI_nav_transacciones_out.Fecha_recepcion));
                    cmd.Parameters.Add(new SqlParameter("@NO_PEDIDO", oBeI_nav_transacciones_out.No_pedido ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@NO_LINEA", oBeI_nav_transacciones_out.No_linea ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@CODIGO_PRODUCTO", oBeI_nav_transacciones_out.Codigo_producto ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@NOMBRE_PRODUCTO", oBeI_nav_transacciones_out.Nombre_producto ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@CODIGO_VARIANTE", oBeI_nav_transacciones_out.Codigo_variante ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@UNIDAD_MEDIDA", oBeI_nav_transacciones_out.Unidad_medida ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@TIPO_TRANSACCION", oBeI_nav_transacciones_out.Tipo_transaccion ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@ENVIADO", oBeI_nav_transacciones_out.Enviado));
                    cmd.Parameters.Add(new SqlParameter("@FEC_AGR", oBeI_nav_transacciones_out.Fec_agr));
                    cmd.Parameters.Add(new SqlParameter("@USER_AGR", oBeI_nav_transacciones_out.User_agr));
                    cmd.Parameters.Add(new SqlParameter("@FEC_MOD", oBeI_nav_transacciones_out.Fec_mod));
                    cmd.Parameters.Add(new SqlParameter("@USER_MOD", oBeI_nav_transacciones_out.User_mod));
                    cmd.Parameters.Add(new SqlParameter("@CANTIDAD_ESPERADA", oBeI_nav_transacciones_out.Cantidad_Esperada));
                    cmd.Parameters.Add(new SqlParameter("@LIC_PLATE", oBeI_nav_transacciones_out.Lic_Plate ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@UDS_LIC_PLATE", oBeI_nav_transacciones_out.Uds_Lic_Plate));
                    cmd.Parameters.Add(new SqlParameter("@CANTIDAD_PRESENTACION", oBeI_nav_transacciones_out.Cantidad_Presentacion));
                    cmd.Parameters.Add(new SqlParameter("@IDTIPODOCUMENTO", oBeI_nav_transacciones_out.IdTipoDocumento));
                    cmd.Parameters.Add(new SqlParameter("@CODIGO_BARRA", oBeI_nav_transacciones_out.codigo_barra ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_ADUANA", oBeI_nav_transacciones_out.valor_aduana));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_FOB", oBeI_nav_transacciones_out.valor_fob));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_IVA", oBeI_nav_transacciones_out.valor_iva));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_DAI", oBeI_nav_transacciones_out.valor_dai));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_SEGURO", oBeI_nav_transacciones_out.valor_seguro));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_FLETE", oBeI_nav_transacciones_out.valor_flete));
                    cmd.Parameters.Add(new SqlParameter("@PESO_NETO", oBeI_nav_transacciones_out.peso_neto));
                    cmd.Parameters.Add(new SqlParameter("@PESO_BRUTO", oBeI_nav_transacciones_out.peso_bruto));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_DESPACHO", oBeI_nav_transacciones_out.fecha_despacho == new DateTime(1900, 1, 1) ? DBNull.Value : (object)oBeI_nav_transacciones_out.fecha_despacho));
                    cmd.Parameters.Add(new SqlParameter("@NO_DOCUMENTO_SALIDA_REF_DEVOL", oBeI_nav_transacciones_out.no_documento_salida_ref_devol ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@IDPEDIDOENCDEVOL", oBeI_nav_transacciones_out.IdPedidoEncDevol));
                    cmd.Parameters.Add(new SqlParameter("@IDDESPACHODET", oBeI_nav_transacciones_out.IdDespachoDet));
                    cmd.Parameters.Add(new SqlParameter("@IDRECEPCIONDET", oBeI_nav_transacciones_out.IdRecepcionDet));
                    cmd.Parameters.Add(new SqlParameter("@CANTIDAD_ENVIADA", oBeI_nav_transacciones_out.Cantidad_Enviada));
                    cmd.Parameters.Add(new SqlParameter("@CANTIDAD_PENDIENTE", oBeI_nav_transacciones_out.Cantidad_Pendiente));
                    cmd.Parameters.Add(new SqlParameter("@AUDITAR", oBeI_nav_transacciones_out.Auditar));
                    cmd.Parameters.Add(new SqlParameter("@IDPRODUCTOTALLACOLOR", oBeI_nav_transacciones_out.IdProductoTallaColor));
                    cmd.Parameters.Add(new SqlParameter("@TALLA", oBeI_nav_transacciones_out.Talla ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@COLOR", oBeI_nav_transacciones_out.Color ?? string.Empty));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool GetSingle(IConfiguration config, ref clsBeI_nav_transacciones_out pBeI_nav_transacciones_out)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;
            bool GetSingle = true;
            try
            {
                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                const string sp = "SELECT * FROM I_nav_transacciones_out" +
                                 " Where(idtransaccion = @idtransaccion)";
                SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                SqlDataAdapter dad = new SqlDataAdapter(cmd);
                dad.SelectCommand.Parameters.Add(new SqlParameter("@IDTRANSACCION", pBeI_nav_transacciones_out.Idtransaccion));
                DataTable dt = new DataTable();
                dad.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    var vrow = dt.Rows[0];
                    Cargar(ref pBeI_nav_transacciones_out, vrow);
                    GetSingle = true;
                }
                lTransaction.Commit();
            }
            catch (Exception)
            {
                if (lTransaction != null) lTransaction.Rollback();
                throw;
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                if (lTransaction !=null) lTransaction.Dispose();
                lConnection.Dispose();
            }
            return GetSingle;
        }

        public static clsBeI_nav_transacciones_out? GetSingle(IConfiguration config, int pIdTransaccion)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;
            clsBeI_nav_transacciones_out? GetSingle = null;
            try
            {
                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                const string sp = " SELECT * FROM I_nav_transacciones_out " +
                                 " Where(idtransaccion = @idtransaccion) ";
                SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                SqlDataAdapter dad = new SqlDataAdapter(cmd);
                dad.SelectCommand.Parameters.Add(new SqlParameter("@IDTRANSACCION", pIdTransaccion));
                DataTable dt = new DataTable();
                dad.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    clsBeI_nav_transacciones_out pBeI_nav_transacciones_out = new clsBeI_nav_transacciones_out();
                    var vrow = dt.Rows[0];
                    Cargar(ref pBeI_nav_transacciones_out, vrow);
                    GetSingle = pBeI_nav_transacciones_out;
                }
                lTransaction.Commit();
            }
            catch (Exception)
            {
                if (lTransaction != null) lTransaction.Rollback();
                throw;
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                if (lTransaction != null) lTransaction.Dispose();
                lConnection.Dispose();
            }
            return GetSingle;
        }

        public static int MaxID(IConfiguration config)
        {
            try
            {
                int lMax = 0;
                const string sp = "SELECT ISNULL(Max(idtransaccion),0) FROM I_nav_transacciones_out";
                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    lConnection.Open();
                    using (SqlTransaction lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
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

        public static bool Insertar_Ingreso(int pIdEmpresa,
                                           int pIdBodega,
                                           List<clsBeTrans_re_det> pListObjDetR,
                                           int pIdOrdenCompraEnc,
                                           int pIdUsuario,
                                           int pIdPropietarioBodega,
                                           SqlConnection lConnection,
                                           SqlTransaction lTransaction)
        {
            bool result = false;

            try
            {
                if (pListObjDetR != null)
                {
                    int lMaxS = MaxID(lConnection, lTransaction) + 1;
                    clsBeI_nav_transacciones_out BeTransaccionesOut;
                    clsBeTrans_oc_det? BeTransOcDet;
                    clsBeTrans_oc_ti? BeTipoDocumentoIngreso;
                    clsDataContractDI.tTipoDocumentoIngreso vIdTipoDocIngreso;
                    clsBeProducto? BeProducto;

                    foreach (clsBeTrans_re_det BeTransReDet in pListObjDetR)
                    {
                        BeTransaccionesOut = new clsBeI_nav_transacciones_out();
                        BeTransOcDet = clsLnTrans_oc_det.Get_Single_By_IdOrdenCompraEnc_And_Linea(pIdOrdenCompraEnc,
                                                                                                 BeTransReDet.No_Linea,
                                                                                                 BeTransReDet.IdProductoBodega,
                                                                                                 lConnection,
                                                                                                 lTransaction);

                        if (BeTransReDet.IdPropietarioBodega == 0)
                        {
                            BeTransReDet.IdPropietarioBodega = pIdPropietarioBodega;
                        }

                        BeTransaccionesOut.Idtransaccion = lMaxS;
                        BeTransaccionesOut.Idempresa = pIdEmpresa;
                        BeTransaccionesOut.Idbodega = pIdBodega;
                        BeTransaccionesOut.Idpropietario = clsLnPropietarios.Get_IdPropietario(pIdBodega,
                                                                                              BeTransReDet.IdPropietarioBodega,
                                                                                              lConnection,
                                                                                              lTransaction);

                        BeTransaccionesOut.Idpropietariobodega = BeTransReDet.IdPropietarioBodega;
                        BeTransaccionesOut.Idordencompra = pIdOrdenCompraEnc;
                        BeTransaccionesOut.Idrecepcionenc = BeTransReDet.IdRecepcionEnc;
                        BeTransaccionesOut.Idpedidoenc = 0;
                        BeTransaccionesOut.Iddespachoenc = 0;
                        BeTransaccionesOut.Idproductobodega = BeTransReDet.IdProductoBodega;

                        BeProducto = clsLnProducto_bodega.Get_Producto_By_IdProductoBodega(BeTransReDet.IdProductoBodega,
                                                                                          lConnection,
                                                                                          lTransaction);

                        if (BeProducto == null)
                        {
                            throw new Exception("Error_202212160939: No se obtuvo el objeto de producto para el IdProductoBodega: " + BeTransReDet.IdProductoBodega);
                        }

                        BeTransaccionesOut.Idproducto = BeProducto.IdProducto;
                        BeTransaccionesOut.Idunidadmedida = BeTransReDet.IdUnidadMedida;
                        BeTransaccionesOut.Idpresentacion = BeTransReDet.Presentacion?.IdPresentacion ?? 0;
                        BeTransaccionesOut.Idproductoestado = BeTransReDet.IdProductoEstado;
                        BeTransaccionesOut.Cantidad = BeTransReDet.Cantidad_recibida;
                        BeTransaccionesOut.Peso = BeTransReDet.Peso;
                        BeTransaccionesOut.Lote = BeTransReDet.Lote ?? "";
                        BeTransaccionesOut.Fecha_vence = BeTransReDet.Fecha_vence;
                        BeTransaccionesOut.Fecha_recepcion = BeTransReDet.Fecha_ingreso;
                        BeTransaccionesOut.No_pedido = clsLnTrans_oc_enc.Get_No_Pedido(pIdOrdenCompraEnc,
                                                                                      lConnection,
                                                                                      lTransaction);
                        BeTransaccionesOut.No_linea = BeTransReDet.No_Linea.ToString();
                        BeTransaccionesOut.Codigo_producto = BeProducto.codigo ?? "";
                        BeTransaccionesOut.codigo_barra = BeProducto.codigo_barra ?? "";
                        BeTransaccionesOut.Nombre_producto = BeProducto.nombre ?? "";
                        BeTransaccionesOut.Lic_Plate = BeTransReDet.Lic_plate ?? "";
                        BeTransaccionesOut.Codigo_variante = clsLnTrans_oc_det.Get_Cod_Variante_Nav(pIdOrdenCompraEnc,
                                                                                                   BeTransReDet.No_Linea,
                                                                                                   lConnection,
                                                                                                   lTransaction) ?? "";

                        if (BeTransaccionesOut.Idpresentacion == 0)
                        {
                            BeTransaccionesOut.Unidad_medida = (BeTransReDet.UnidadMedida?.Nombre == "" ? BeTransReDet.Nombre_unidad_medida : BeTransReDet.UnidadMedida?.Nombre) ?? "";
                        }
                        else
                        {
                            BeTransaccionesOut.Unidad_medida = (BeTransReDet.Presentacion?.Nombre == "" ? BeTransReDet.Nombre_presentacion : BeTransReDet.Presentacion?.Nombre) ?? "";
                        }

                        BeTipoDocumentoIngreso = clsLnTrans_oc_enc.Get_BeTipoDocumento_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                                                          lConnection,
                                                                                                          lTransaction);

                        if (BeTipoDocumentoIngreso != null)
                        {
                            vIdTipoDocIngreso =(clsDataContractDI.tTipoDocumentoIngreso) BeTipoDocumentoIngreso.IdTipoIngresoOC;
                        }
                        else
                        {
                            vIdTipoDocIngreso = clsDataContractDI.tTipoDocumentoIngreso.NoDefinido;
                        }

                        BeTransaccionesOut.IdTipoDocumento =(int) vIdTipoDocIngreso;
                        BeTransaccionesOut.Tipo_transaccion = "INGRESO";
                        BeTransaccionesOut.IdRecepcionDet = BeTransReDet.IdRecepcionDet;

                        if (BeTipoDocumentoIngreso != null)
                        {
                            if (BeTipoDocumentoIngreso.Marcar_registros_enviados_mi3)
                            {
                                BeTransaccionesOut.Enviado = true;
                            }
                            else
                            {
                                BeTransaccionesOut.Enviado = false;
                            }
                        }
                        else
                        {
                            BeTransaccionesOut.Enviado = false;
                        }

                        BeTransaccionesOut.Fec_mod = DateTime.Now;
                        BeTransaccionesOut.Fec_agr = DateTime.Now;
                        BeTransaccionesOut.User_agr = pIdUsuario.ToString();
                        BeTransaccionesOut.User_mod = pIdUsuario.ToString();

                        if (BeTransOcDet != null)
                        {
                            BeTransaccionesOut.codigo_barra = clsLnProducto.Get_CodigoBarra_By_IdProducto(BeTransaccionesOut.Idproducto,
                                                                                                         lConnection,
                                                                                                         lTransaction) ?? "";
                            BeTransaccionesOut.valor_aduana = BeTransOcDet.Valor_aduana;
                            BeTransaccionesOut.valor_fob = BeTransOcDet.Valor_fob;
                            BeTransaccionesOut.valor_iva = BeTransOcDet.Valor_iva;
                            BeTransaccionesOut.valor_dai = BeTransOcDet.Valor_dai;
                            BeTransaccionesOut.valor_seguro = BeTransOcDet.Valor_seguro;
                            BeTransaccionesOut.valor_flete = BeTransOcDet.Valor_flete;
                            BeTransaccionesOut.peso_neto = BeTransOcDet.Peso_neto;
                            BeTransaccionesOut.peso_bruto = BeTransOcDet.Peso_bruto;
                        }

                        BeTransaccionesOut.fecha_despacho = new DateTime(1900, 1, 1);

                        if (BeTransOcDet == null)
                        {
                            BeTransaccionesOut.Cantidad_Esperada = 0;
                        }
                        else
                        {
                            BeTransaccionesOut.Cantidad_Esperada = BeTransOcDet.Cantidad;
                        }

                        if (pIdOrdenCompraEnc == 0)
                        {
                            if (BeTipoDocumentoIngreso == null)
                            {
                                BeTipoDocumentoIngreso = new clsBeTrans_oc_ti();
                            }
                        }

                        if (vIdTipoDocIngreso == clsDataContractDI.tTipoDocumentoIngreso.Liquidacion_De_Ruta_Devolucion ||
                            (BeTipoDocumentoIngreso?.Es_devolucion ?? false))
                        {
                            if (pIdOrdenCompraEnc != 0)
                            {
                                int idPedidoEncDevol = 0;
                                string noDocumentoSalidaRefDevol = "";
                                clsLnTrans_oc_enc.Get_Parametros_Devol_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                                          ref idPedidoEncDevol,
                                                                                          ref noDocumentoSalidaRefDevol,
                                                                                          lConnection,
                                                                                          lTransaction);
                                BeTransaccionesOut.IdPedidoEncDevol = idPedidoEncDevol;
                                BeTransaccionesOut.no_documento_salida_ref_devol = noDocumentoSalidaRefDevol;
                            }
                        }

                        BeTransaccionesOut.IdProductoTallaColor = BeTransReDet.IdProductoTallaColor;

                        DataTable DtPtc = clsLnProducto_talla_color.Get_Single_Dt_By_IdProductoTallaColor(BeTransReDet.IdProductoBodega, lConnection,lTransaction);

                        if (DtPtc != null && DtPtc.Rows.Count > 0)
                        {
                            BeTransaccionesOut.Talla = DtPtc.Rows[0]["talla"]?.ToString() ?? "";
                            BeTransaccionesOut.Color = DtPtc.Rows[0]["color"]?.ToString() ?? "";
                        }

                        Insertar(ref BeTransaccionesOut, lConnection, lTransaction);
                        lMaxS++;

                        clsBeTrans_re_det_lote_num BeLoteNum = new clsBeTrans_re_det_lote_num();
                        BeLoteNum.IdLoteNum = clsLnTrans_re_det_lote_num.MaxID(lConnection, lTransaction) + 1;
                        BeLoteNum.IdProductoBodega = BeTransReDet.IdProductoBodega;
                        BeLoteNum.IdRecepcionEnc = BeTransReDet.IdRecepcionEnc;
                        BeLoteNum.Codigo = BeTransReDet.Codigo_producto ?? "";
                        BeLoteNum.Lote = BeTransReDet.Lote ?? "";
                        BeLoteNum.Lote_Numerico = BeLoteNum.IdLoteNum;
                        BeLoteNum.Cantidad = BeTransReDet.Cantidad_recibida;
                        BeLoteNum.FechaIngreso = DateTime.Now;
                        clsLnTrans_re_det_lote_num.Insertar(BeLoteNum, lConnection, lTransaction);
                    }

                    result = true;
                }
            }
            catch (Exception)
            {                
                throw;
            }

            return result;
        }

        public static int MaxID(SqlConnection pConnection, SqlTransaction pTransaction)
        {
            try
            {
                int lMax = 0;
                const string sp = "SELECT ISNULL(Max(idtransaccion),0) FROM I_nav_transacciones_out";

                using (SqlCommand lCommand = new SqlCommand(sp, pConnection, pTransaction) { CommandType = CommandType.Text })
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
        
        public static List<clsBeI_nav_transacciones_out> Get_All_Ingresos_Pendientes_De_Envio(IConfiguration configuration)
        {
            SqlConnection lConnection = new SqlConnection(configuration.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

                var lReturnList = new List<clsBeI_nav_transacciones_out>();

                string vSQL = "SELECT * FROM I_nav_transacciones_out " +
                              "WHERE tipo_transaccion = 'INGRESO' AND Enviado = 0 " +
                              "ORDER BY fec_agr";

                using var cmd = new SqlCommand(vSQL, lConnection, lTransaction)
                {
                    CommandType = CommandType.Text
                };

                using var dad = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                dad.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    var item = new clsBeI_nav_transacciones_out();
                    Cargar(ref item, dr);
                    lReturnList.Add(item);
                }

                lTransaction.Commit();
                return lReturnList;
            }
            catch
            {
                if (lTransaction != null)
                    lTransaction.Rollback();

                throw; // NO uses "throw ex;" para no perder stacktrace
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open)
                    lConnection.Close();

                lTransaction?.Dispose();
                lConnection.Dispose();
            }
        }

        public static int Marcar_Como_Enviado(IConfiguration configuration, List<int> ids)
        {
            if (ids == null || ids.Count == 0) return 0;

            ids = ids.Where(x => x > 0).Distinct().ToList();
            if (ids.Count == 0) return 0;

            using var conn = new SqlConnection(configuration.GetConnectionString("CST") ?? configuration["CST"]);
            SqlTransaction? tx = null;

            try
            {
                conn.Open();
                tx = conn.BeginTransaction(IsolationLevel.ReadCommitted);

                // 1) Validar que EXISTAN TODOS (y sean INGRESO)
                var pnames = ids.Select((_, i) => $"@p{i}").ToList();

                string sqlCount = $@"SELECT COUNT(1)
                                    FROM I_nav_transacciones_out
                                    WHERE Idtransaccion IN ({string.Join(",", pnames)})
                                      AND tipo_transaccion = 'INGRESO';";

                using (var cmdCount = new SqlCommand(sqlCount, conn, tx))
                {
                    for (int i = 0; i < ids.Count; i++)
                        cmdCount.Parameters.Add(new SqlParameter(pnames[i], SqlDbType.Int) { Value = ids[i] });

                    int encontrados = Convert.ToInt32(cmdCount.ExecuteScalar());

                    if (encontrados != ids.Count)
                        throw new Exception("Transacción abortada: uno o más Idtransaccion no existen o no son de tipo INGRESO.");
                }

                // 2) Marcar como enviado (todo dentro de la MISMA tx)                
                string sqlUpdate = $@"
                                    UPDATE I_nav_transacciones_out
                                    SET Enviado = 1,
                                        fec_mod = GETDATE()
                                    WHERE Idtransaccion IN ({string.Join(",", pnames)})                                      
                                      AND Enviado = 0;";

                int updated;
                using (var cmdUp = new SqlCommand(sqlUpdate, conn, tx))
                {
                    for (int i = 0; i < ids.Count; i++)
                        cmdUp.Parameters.Add(new SqlParameter(pnames[i], SqlDbType.Int) { Value = ids[i] });

                    updated = cmdUp.ExecuteNonQuery();
                }                

                tx.Commit();
                return updated;
            }
            catch
            {
                tx?.Rollback();
                throw;
            }
            finally
            {
                tx?.Dispose();
            }
        }

        public static List<clsBeI_nav_transacciones_out> Get_All_Salidas_Pendientes_De_Procesar(IConfiguration configuration,string? noPedido = null)
        {
            SqlConnection lConnection = new SqlConnection(configuration.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            try
            {
                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

                var lReturnList = new List<clsBeI_nav_transacciones_out>();
                
                string vSQL ="SELECT * FROM I_nav_transacciones_out " +
                             "WHERE tipo_transaccion = 'SALIDA' AND Enviado = 0 " +
                             (string.IsNullOrWhiteSpace(noPedido) ? "" : "AND no_pedido = @no_pedido ") +
                             "ORDER BY fec_agr";

                using var cmd = new SqlCommand(vSQL, lConnection, lTransaction)
                {
                    CommandType = CommandType.Text
                };                

                if (!string.IsNullOrWhiteSpace(noPedido))
                    cmd.Parameters.Add("@no_pedido", SqlDbType.VarChar, 50).Value = noPedido.Trim();

                using var dad = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                dad.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    var item = new clsBeI_nav_transacciones_out();
                    Cargar(ref item, dr);
                    lReturnList.Add(item);
                }

                lTransaction.Commit();
                return lReturnList;
            }
            catch
            {
                if (lTransaction != null)
                    lTransaction.Rollback();

                throw;
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open)
                    lConnection.Close();

                lTransaction?.Dispose();
                lConnection.Dispose();
            }
        }

    }
}
