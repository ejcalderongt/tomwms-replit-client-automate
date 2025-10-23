using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Interface;

public class clsLnI_nav_config_enc
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeI_nav_config_enc oBeI_nav_config_enc, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }

        try
        {
            oBeI_nav_config_enc.Idnavconfigenc = GetInt("idnavconfigenc");
            oBeI_nav_config_enc.Idempresa = GetInt("idempresa");
            oBeI_nav_config_enc.Idbodega = GetInt("idbodega");
            oBeI_nav_config_enc.IdPropietario = GetInt("idPropietario");
            oBeI_nav_config_enc.IdUsuario = GetInt("idUsuario");
            oBeI_nav_config_enc.Nombre = GetString("nombre");
            oBeI_nav_config_enc.Fec_agr = GetDate("fec_agr");
            oBeI_nav_config_enc.User_agr = GetString("user_agr");
            oBeI_nav_config_enc.Fec_mod = GetDate("fec_mod");
            oBeI_nav_config_enc.User_mod = GetString("user_mod");
            oBeI_nav_config_enc.IdProductoEstado = GetInt("IdProductoEstado");
            oBeI_nav_config_enc.Rechazar_pedido_incompleto = GetInt("rechazar_pedido_incompleto");
            oBeI_nav_config_enc.Despachar_existencia_parcial = GetInt("despachar_existencia_parcial");
            oBeI_nav_config_enc.Convertir_decimales_a_umbas = GetInt("convertir_decimales_a_umbas");
            oBeI_nav_config_enc.Generar_pedido_ingreso_bodega_destino = GetBool("generar_pedido_ingreso_bodega_destino");
            oBeI_nav_config_enc.Generar_recepcion_auto_bodega_destino = GetBool("generar_recepcion_auto_bodega_destino");
            oBeI_nav_config_enc.Codigo_proveedor_produccion = GetString("codigo_proveedor_produccion");
            oBeI_nav_config_enc.IdFamilia = GetInt("idFamilia");
            oBeI_nav_config_enc.Idclasificacion = GetInt("idclasificacion");
            oBeI_nav_config_enc.IdMarca = GetInt("idMarca");
            oBeI_nav_config_enc.IdTipoProducto = GetInt("idTipoProducto");
            oBeI_nav_config_enc.Control_lote = GetBool("control_lote");
            oBeI_nav_config_enc.Control_vencimiento = GetBool("control_vencimiento");
            oBeI_nav_config_enc.Genera_lp = GetBool("genera_lp");
            oBeI_nav_config_enc.Nombre_ejecutable = GetString("nombre_ejecutable");
            oBeI_nav_config_enc.IdTipoDocumentoTransferenciasIngreso = GetInt("IdTipoDocumentoTransferenciasIngreso");
            oBeI_nav_config_enc.Crear_recepcion_de_transferencia_nav = GetBool("crear_recepcion_de_transferencia_nav");
            oBeI_nav_config_enc.Control_peso = GetBool("control_peso");
            oBeI_nav_config_enc.Crear_recepcion_de_compra_nav = GetBool("crear_recepcion_de_compra_nav");
            oBeI_nav_config_enc.IdAcuerdoEnc = GetInt("IdAcuerdoEnc");
            oBeI_nav_config_enc.IdTipoEtiqueta = GetInt("IdTipoEtiqueta");
            oBeI_nav_config_enc.Equiparar_cliente_con_propietario_en_doc_salida = GetBool("equiparar_cliente_con_propietario_en_doc_salida");
            oBeI_nav_config_enc.Push_ingreso_nav_desde_hh = GetBool("push_ingreso_nav_desde_hh");
            oBeI_nav_config_enc.Reservar_umbas_primero = GetBool("reservar_umbas_primero");
            oBeI_nav_config_enc.Implosion_automatica = GetBool("implosion_automatica");
            oBeI_nav_config_enc.Explosion_automatica = GetBool("explosion_automatica");
            oBeI_nav_config_enc.Ejecutar_En_Despacho_Automaticamente = GetBool("Ejecutar_En_Despacho_Automaticamente");
            oBeI_nav_config_enc.IdTipoRotacion = GetInt("IdTipoRotacion");
            oBeI_nav_config_enc.Explosion_automatica_desde_ubicacion_picking = GetBool("explosion_automatica_desde_ubicacion_picking");
            oBeI_nav_config_enc.Explosion_automatica_nivel_max = GetInt("explosion_automatica_nivel_max");
            oBeI_nav_config_enc.Conservar_zona_picking_clavaud = GetBool("conservar_zona_picking_clavaud");
            oBeI_nav_config_enc.Recepcion_genera_historico = GetBool("recepcion_genera_historico");
            oBeI_nav_config_enc.Excluir_ubicaciones_reabasto = GetBool("excluir_ubicaciones_reabasto");
            oBeI_nav_config_enc.Considerar_disponibilidad_ubicacion_reabasto = GetBool("considerar_disponibilidad_ubicacion_reabasto");
            oBeI_nav_config_enc.Considerar_paletizado_en_reabasto = GetBool("considerar_paletizado_en_reabasto");
            oBeI_nav_config_enc.Dias_vida_defecto_perecederos = GetInt("dias_vida_defecto_perecederos");
            oBeI_nav_config_enc.Codigo_bodega_erp_nc = GetString("codigo_bodega_erp_nc");
            oBeI_nav_config_enc.Lote_defecto_entrada_nc = GetString("lote_defecto_entrada_nc");
            oBeI_nav_config_enc.Vence_defecto_nc = GetDate("vence_defecto_nc");
            oBeI_nav_config_enc.IdProductoEstado_NC = GetInt("IdProductoEstado_NC");
            oBeI_nav_config_enc.Lote_defecto_entrada_mercancia_sap = GetString("lote_defecto_entrada_mercancia_sap");
            oBeI_nav_config_enc.Fecha_vence_defecto = GetDate("fecha_vence_defecto");
            oBeI_nav_config_enc.Interface_sap = GetBool("interface_sap");
            oBeI_nav_config_enc.Sap_control_draft_ajustes = GetBool("sap_control_draft_ajustes");
            oBeI_nav_config_enc.Sap_control_draft_traslados = GetBool("sap_control_draft_traslados");
            oBeI_nav_config_enc.Inferir_bonificacion_pedido_sap = GetBool("inferir_bonificacion_pedido_sap");
            oBeI_nav_config_enc.Rechazar_bonificacion_incompleta = GetBool("rechazar_bonificacion_incompleta");
            oBeI_nav_config_enc.IdIndiceRotacion = GetInt("IdIndiceRotacion");
            oBeI_nav_config_enc.Rango_dias_importacion = GetInt("rango_dias_importacion");
            oBeI_nav_config_enc.Equiparar_productos = GetBool("equiparar_productos");
            oBeI_nav_config_enc.Bodega_facturacion = GetString("bodega_facturacion");
            oBeI_nav_config_enc.Valida_solo_codigo = GetBool("valida_solo_codigo");
            oBeI_nav_config_enc.Excluir_recepcion_picking = GetBool("excluir_recepcion_picking");
            oBeI_nav_config_enc.Bodega_prorrateo = GetString("bodega_prorrateo");
            oBeI_nav_config_enc.Bodega_prorrateo1 = GetString("bodega_prorrateo1");
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

    public static void Binding(SqlCommand cmd, clsBeI_nav_config_enc oBe)
    {
        cmd.Parameters.Add(new SqlParameter("@idnavconfigenc", oBe.Idnavconfigenc));
        cmd.Parameters.Add(new SqlParameter("@idempresa", oBe.Idempresa));
        cmd.Parameters.Add(new SqlParameter("@idbodega", oBe.Idbodega));
        cmd.Parameters.Add(new SqlParameter("@idPropietario", oBe.IdPropietario));
        cmd.Parameters.Add(new SqlParameter("@idUsuario", oBe.IdUsuario));
        cmd.Parameters.Add(new SqlParameter("@nombre", oBe.Nombre));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", oBe.Fec_agr));
        cmd.Parameters.Add(new SqlParameter("@user_agr", oBe.User_agr));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", oBe.Fec_mod));
        cmd.Parameters.Add(new SqlParameter("@user_mod", oBe.User_mod));
        cmd.Parameters.Add(new SqlParameter("@IdProductoEstado", oBe.IdProductoEstado));
        cmd.Parameters.Add(new SqlParameter("@rechazar_pedido_incompleto", oBe.Rechazar_pedido_incompleto));
        cmd.Parameters.Add(new SqlParameter("@despachar_existencia_parcial", oBe.Despachar_existencia_parcial));
        cmd.Parameters.Add(new SqlParameter("@convertir_decimales_a_umbas", oBe.Convertir_decimales_a_umbas));
        cmd.Parameters.Add(new SqlParameter("@generar_pedido_ingreso_bodega_destino", oBe.Generar_pedido_ingreso_bodega_destino));
        cmd.Parameters.Add(new SqlParameter("@generar_recepcion_auto_bodega_destino", oBe.Generar_recepcion_auto_bodega_destino));
        cmd.Parameters.Add(new SqlParameter("@codigo_proveedor_produccion", oBe.Codigo_proveedor_produccion));
        cmd.Parameters.Add(new SqlParameter("@idFamilia", oBe.IdFamilia));
        cmd.Parameters.Add(new SqlParameter("@idclasificacion", oBe.Idclasificacion));
        cmd.Parameters.Add(new SqlParameter("@idMarca", oBe.IdMarca));
        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", oBe.IdTipoProducto));
        cmd.Parameters.Add(new SqlParameter("@control_lote", oBe.Control_lote));
        cmd.Parameters.Add(new SqlParameter("@control_vencimiento", oBe.Control_vencimiento));
        cmd.Parameters.Add(new SqlParameter("@genera_lp", oBe.Genera_lp));
        cmd.Parameters.Add(new SqlParameter("@nombre_ejecutable", oBe.Nombre_ejecutable));
        cmd.Parameters.Add(new SqlParameter("@IdTipoDocumentoTransferenciasIngreso", oBe.IdTipoDocumentoTransferenciasIngreso));
        cmd.Parameters.Add(new SqlParameter("@crear_recepcion_de_transferencia_nav", oBe.Crear_recepcion_de_transferencia_nav));
        cmd.Parameters.Add(new SqlParameter("@control_peso", oBe.Control_peso));
        cmd.Parameters.Add(new SqlParameter("@crear_recepcion_de_compra_nav", oBe.Crear_recepcion_de_compra_nav));
        cmd.Parameters.Add(new SqlParameter("@IdAcuerdoEnc", oBe.IdAcuerdoEnc));
        cmd.Parameters.Add(new SqlParameter("@IdTipoEtiqueta", oBe.IdTipoEtiqueta));
        cmd.Parameters.Add(new SqlParameter("@equiparar_cliente_con_propietario_en_doc_salida", oBe.Equiparar_cliente_con_propietario_en_doc_salida));
        cmd.Parameters.Add(new SqlParameter("@push_ingreso_nav_desde_hh", oBe.Push_ingreso_nav_desde_hh));
        cmd.Parameters.Add(new SqlParameter("@reservar_umbas_primero", oBe.Reservar_umbas_primero));
        cmd.Parameters.Add(new SqlParameter("@implosion_automatica", oBe.Implosion_automatica));
        cmd.Parameters.Add(new SqlParameter("@explosion_automatica", oBe.Explosion_automatica));
        cmd.Parameters.Add(new SqlParameter("@Ejecutar_En_Despacho_Automaticamente", oBe.Ejecutar_En_Despacho_Automaticamente));
        cmd.Parameters.Add(new SqlParameter("@IdTipoRotacion", oBe.IdTipoRotacion));
        cmd.Parameters.Add(new SqlParameter("@explosion_automatica_desde_ubicacion_picking", oBe.Explosion_automatica_desde_ubicacion_picking));
        cmd.Parameters.Add(new SqlParameter("@explosion_automatica_nivel_max", oBe.Explosion_automatica_nivel_max));
        cmd.Parameters.Add(new SqlParameter("@conservar_zona_picking_clavaud", oBe.Conservar_zona_picking_clavaud));
        cmd.Parameters.Add(new SqlParameter("@recepcion_genera_historico", oBe.Recepcion_genera_historico));
        cmd.Parameters.Add(new SqlParameter("@excluir_ubicaciones_reabasto", oBe.Excluir_ubicaciones_reabasto));
        cmd.Parameters.Add(new SqlParameter("@considerar_disponibilidad_ubicacion_reabasto", oBe.Considerar_disponibilidad_ubicacion_reabasto));
        cmd.Parameters.Add(new SqlParameter("@considerar_paletizado_en_reabasto", oBe.Considerar_paletizado_en_reabasto));
        cmd.Parameters.Add(new SqlParameter("@dias_vida_defecto_perecederos", oBe.Dias_vida_defecto_perecederos));
        cmd.Parameters.Add(new SqlParameter("@codigo_bodega_erp_nc", oBe.Codigo_bodega_erp_nc));
        cmd.Parameters.Add(new SqlParameter("@lote_defecto_entrada_nc", oBe.Lote_defecto_entrada_nc));
        cmd.Parameters.Add(new SqlParameter("@vence_defecto_nc", oBe.Vence_defecto_nc));
        cmd.Parameters.Add(new SqlParameter("@IdProductoEstado_NC", oBe.IdProductoEstado_NC));
        cmd.Parameters.Add(new SqlParameter("@lote_defecto_entrada_mercancia_sap", oBe.Lote_defecto_entrada_mercancia_sap));
        cmd.Parameters.Add(new SqlParameter("@fecha_vence_defecto", oBe.Fecha_vence_defecto));
        cmd.Parameters.Add(new SqlParameter("@interface_sap", oBe.Interface_sap));
        cmd.Parameters.Add(new SqlParameter("@sap_control_draft_ajustes", oBe.Sap_control_draft_ajustes));
        cmd.Parameters.Add(new SqlParameter("@sap_control_draft_traslados", oBe.Sap_control_draft_traslados));
        cmd.Parameters.Add(new SqlParameter("@inferir_bonificacion_pedido_sap", oBe.Inferir_bonificacion_pedido_sap));
        cmd.Parameters.Add(new SqlParameter("@rechazar_bonificacion_incompleta", oBe.Rechazar_bonificacion_incompleta));
        cmd.Parameters.Add(new SqlParameter("@IdIndiceRotacion", oBe.IdIndiceRotacion));
        cmd.Parameters.Add(new SqlParameter("@rango_dias_importacion", oBe.Rango_dias_importacion));
        cmd.Parameters.Add(new SqlParameter("@equiparar_productos", oBe.Equiparar_productos));
        cmd.Parameters.Add(new SqlParameter("@bodega_facturacion", oBe.Bodega_facturacion));
        cmd.Parameters.Add(new SqlParameter("@valida_solo_codigo", oBe.Valida_solo_codigo));
        cmd.Parameters.Add(new SqlParameter("@excluir_recepcion_picking", oBe.Excluir_recepcion_picking));
        cmd.Parameters.Add(new SqlParameter("@bodega_prorrateo", oBe.Bodega_prorrateo));
        cmd.Parameters.Add(new SqlParameter("@bodega_prorrateo1", oBe.Bodega_prorrateo1));
    }



    public static int Insertar(IConfiguration config, clsBeI_nav_config_enc oBeI_nav_config_enc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("i_nav_config_enc");
            Ins.Add("idnavconfigenc", "@idnavconfigenc", "F");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("idusuario", "@idusuario", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("idproductoestado", "@idproductoestado", "F");
            Ins.Add("rechazar_pedido_incompleto", "@rechazar_pedido_incompleto", "F");
            Ins.Add("despachar_existencia_parcial", "@despachar_existencia_parcial", "F");
            Ins.Add("convertir_decimales_a_umbas", "@convertir_decimales_a_umbas", "F");
            Ins.Add("generar_pedido_ingreso_bodega_destino", "@generar_pedido_ingreso_bodega_destino", "F");
            Ins.Add("generar_recepcion_auto_bodega_destino", "@generar_recepcion_auto_bodega_destino", "F");
            Ins.Add("codigo_proveedor_produccion", "@codigo_proveedor_produccion", "F");
            Ins.Add("idfamilia", "@idfamilia", "F");
            Ins.Add("idclasificacion", "@idclasificacion", "F");
            Ins.Add("idmarca", "@idmarca", "F");
            Ins.Add("idtipoproducto", "@idtipoproducto", "F");
            Ins.Add("control_lote", "@control_lote", "F");
            Ins.Add("control_vencimiento", "@control_vencimiento", "F");
            Ins.Add("genera_lp", "@genera_lp", "F");
            Ins.Add("nombre_ejecutable", "@nombre_ejecutable", "F");
            Ins.Add("idtipodocumentotransferenciasingreso", "@idtipodocumentotransferenciasingreso", "F");
            Ins.Add("crear_recepcion_de_transferencia_nav", "@crear_recepcion_de_transferencia_nav", "F");
            Ins.Add("control_peso", "@control_peso", "F");
            Ins.Add("crear_recepcion_de_compra_nav", "@crear_recepcion_de_compra_nav", "F");
            Ins.Add("idacuerdoenc", "@idacuerdoenc", "F");
            Ins.Add("idtipoetiqueta", "@idtipoetiqueta", "F");
            Ins.Add("equiparar_cliente_con_propietario_en_doc_salida", "@equiparar_cliente_con_propietario_en_doc_salida", "F");
            Ins.Add("push_ingreso_nav_desde_hh", "@push_ingreso_nav_desde_hh", "F");
            Ins.Add("reservar_umbas_primero", "@reservar_umbas_primero", "F");
            Ins.Add("implosion_automatica", "@implosion_automatica", "F");
            Ins.Add("explosion_automatica", "@explosion_automatica", "F");
            Ins.Add("ejecutar_en_despacho_automaticamente", "@ejecutar_en_despacho_automaticamente", "F");
            Ins.Add("idtiporotacion", "@idtiporotacion", "F");
            Ins.Add("explosion_automatica_desde_ubicacion_picking", "@explosion_automatica_desde_ubicacion_picking", "F");
            Ins.Add("explosion_automatica_nivel_max", "@explosion_automatica_nivel_max", "F");
            Ins.Add("conservar_zona_picking_clavaud", "@conservar_zona_picking_clavaud", "F");
            Ins.Add("recepcion_genera_historico", "@recepcion_genera_historico", "F");
            Ins.Add("excluir_ubicaciones_reabasto", "@excluir_ubicaciones_reabasto", "F");
            Ins.Add("considerar_disponibilidad_ubicacion_reabasto", "@considerar_disponibilidad_ubicacion_reabasto", "F");
            Ins.Add("considerar_paletizado_en_reabasto", "@considerar_paletizado_en_reabasto", "F");
            Ins.Add("dias_vida_defecto_perecederos", "@dias_vida_defecto_perecederos", "F");
            Ins.Add("codigo_bodega_erp_nc", "@codigo_bodega_erp_nc", "F");
            Ins.Add("lote_defecto_entrada_nc", "@lote_defecto_entrada_nc", "F");
            Ins.Add("vence_defecto_nc", "@vence_defecto_nc", "F");
            Ins.Add("idproductoestado_nc", "@idproductoestado_nc", "F");
            Ins.Add("lote_defecto_entrada_mercancia_sap", "@lote_defecto_entrada_mercancia_sap", "F");
            Ins.Add("fecha_vence_defecto", "@fecha_vence_defecto", "F");
            Ins.Add("interface_sap", "@interface_sap", "F");
            Ins.Add("sap_control_draft_ajustes", "@sap_control_draft_ajustes", "F");
            Ins.Add("sap_control_draft_traslados", "@sap_control_draft_traslados", "F");
            Ins.Add("inferir_bonificacion_pedido_sap", "@inferir_bonificacion_pedido_sap", "F");
            Ins.Add("rechazar_bonificacion_incompleta", "@rechazar_bonificacion_incompleta", "F");
            Ins.Add("idindicerotacion", "@idindicerotacion", "F");
            Ins.Add("rango_dias_importacion", "@rango_dias_importacion", "F");
            Ins.Add("equiparar_productos", "@equiparar_productos", "F");
            Ins.Add("bodega_facturacion", "@bodega_facturacion", "F");
            Ins.Add("valida_solo_codigo", "@valida_solo_codigo", "F");
            Ins.Add("excluir_recepcion_picking", "@excluir_recepcion_picking", "F");
            Ins.Add("bodega_prorrateo", "@bodega_prorrateo", "F");
            Ins.Add("bodega_prorrateo1", "@bodega_prorrateo1", "F");

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

            Binding(cmd, oBeI_nav_config_enc);

            rowsAffected = cmd.ExecuteNonQuery();

            cmd.Dispose();

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
            if (lConnection is not null) lConnection.Dispose();
            if (lTransaction is not null) lTransaction.Dispose();
        }
        return rowsAffected;
    }

    public static int Insertar(IConfiguration config, clsBeI_nav_config_enc oBeI_nav_config_enc)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("i_nav_config_enc");
            Ins.Add("idnavconfigenc", "@idnavconfigenc", "F");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("idusuario", "@idusuario", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("idproductoestado", "@idproductoestado", "F");
            Ins.Add("rechazar_pedido_incompleto", "@rechazar_pedido_incompleto", "F");
            Ins.Add("despachar_existencia_parcial", "@despachar_existencia_parcial", "F");
            Ins.Add("convertir_decimales_a_umbas", "@convertir_decimales_a_umbas", "F");
            Ins.Add("generar_pedido_ingreso_bodega_destino", "@generar_pedido_ingreso_bodega_destino", "F");
            Ins.Add("generar_recepcion_auto_bodega_destino", "@generar_recepcion_auto_bodega_destino", "F");
            Ins.Add("codigo_proveedor_produccion", "@codigo_proveedor_produccion", "F");
            Ins.Add("idfamilia", "@idfamilia", "F");
            Ins.Add("idclasificacion", "@idclasificacion", "F");
            Ins.Add("idmarca", "@idmarca", "F");
            Ins.Add("idtipoproducto", "@idtipoproducto", "F");
            Ins.Add("control_lote", "@control_lote", "F");
            Ins.Add("control_vencimiento", "@control_vencimiento", "F");
            Ins.Add("genera_lp", "@genera_lp", "F");
            Ins.Add("nombre_ejecutable", "@nombre_ejecutable", "F");
            Ins.Add("idtipodocumentotransferenciasingreso", "@idtipodocumentotransferenciasingreso", "F");
            Ins.Add("crear_recepcion_de_transferencia_nav", "@crear_recepcion_de_transferencia_nav", "F");
            Ins.Add("control_peso", "@control_peso", "F");
            Ins.Add("crear_recepcion_de_compra_nav", "@crear_recepcion_de_compra_nav", "F");
            Ins.Add("idacuerdoenc", "@idacuerdoenc", "F");
            Ins.Add("idtipoetiqueta", "@idtipoetiqueta", "F");
            Ins.Add("equiparar_cliente_con_propietario_en_doc_salida", "@equiparar_cliente_con_propietario_en_doc_salida", "F");
            Ins.Add("push_ingreso_nav_desde_hh", "@push_ingreso_nav_desde_hh", "F");
            Ins.Add("reservar_umbas_primero", "@reservar_umbas_primero", "F");
            Ins.Add("implosion_automatica", "@implosion_automatica", "F");
            Ins.Add("explosion_automatica", "@explosion_automatica", "F");
            Ins.Add("ejecutar_en_despacho_automaticamente", "@ejecutar_en_despacho_automaticamente", "F");
            Ins.Add("idtiporotacion", "@idtiporotacion", "F");
            Ins.Add("explosion_automatica_desde_ubicacion_picking", "@explosion_automatica_desde_ubicacion_picking", "F");
            Ins.Add("explosion_automatica_nivel_max", "@explosion_automatica_nivel_max", "F");
            Ins.Add("conservar_zona_picking_clavaud", "@conservar_zona_picking_clavaud", "F");
            Ins.Add("recepcion_genera_historico", "@recepcion_genera_historico", "F");
            Ins.Add("excluir_ubicaciones_reabasto", "@excluir_ubicaciones_reabasto", "F");
            Ins.Add("considerar_disponibilidad_ubicacion_reabasto", "@considerar_disponibilidad_ubicacion_reabasto", "F");
            Ins.Add("considerar_paletizado_en_reabasto", "@considerar_paletizado_en_reabasto", "F");
            Ins.Add("dias_vida_defecto_perecederos", "@dias_vida_defecto_perecederos", "F");
            Ins.Add("codigo_bodega_erp_nc", "@codigo_bodega_erp_nc", "F");
            Ins.Add("lote_defecto_entrada_nc", "@lote_defecto_entrada_nc", "F");
            Ins.Add("vence_defecto_nc", "@vence_defecto_nc", "F");
            Ins.Add("idproductoestado_nc", "@idproductoestado_nc", "F");
            Ins.Add("lote_defecto_entrada_mercancia_sap", "@lote_defecto_entrada_mercancia_sap", "F");
            Ins.Add("fecha_vence_defecto", "@fecha_vence_defecto", "F");
            Ins.Add("interface_sap", "@interface_sap", "F");
            Ins.Add("sap_control_draft_ajustes", "@sap_control_draft_ajustes", "F");
            Ins.Add("sap_control_draft_traslados", "@sap_control_draft_traslados", "F");
            Ins.Add("inferir_bonificacion_pedido_sap", "@inferir_bonificacion_pedido_sap", "F");
            Ins.Add("rechazar_bonificacion_incompleta", "@rechazar_bonificacion_incompleta", "F");
            Ins.Add("idindicerotacion", "@idindicerotacion", "F");
            Ins.Add("rango_dias_importacion", "@rango_dias_importacion", "F");
            Ins.Add("equiparar_productos", "@equiparar_productos", "F");
            Ins.Add("bodega_facturacion", "@bodega_facturacion", "F");
            Ins.Add("valida_solo_codigo", "@valida_solo_codigo", "F");
            Ins.Add("excluir_recepcion_picking", "@excluir_recepcion_picking", "F");
            Ins.Add("bodega_prorrateo", "@bodega_prorrateo", "F");
            Ins.Add("bodega_prorrateo1", "@bodega_prorrateo1", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Binding(cmd, oBeI_nav_config_enc);

            rowsAffected = cmd.ExecuteNonQuery();

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

    public static int Actualizar(IConfiguration config, clsBeI_nav_config_enc oBeI_nav_config_enc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("i_nav_config_enc");
            Upd.Add("idnavconfigenc", "@idnavconfigenc", "F");
            Upd.Add("idempresa", "@idempresa", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("idpropietario", "@idpropietario", "F");
            Upd.Add("idusuario", "@idusuario", "F");
            Upd.Add("nombre", "@nombre", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("idproductoestado", "@idproductoestado", "F");
            Upd.Add("rechazar_pedido_incompleto", "@rechazar_pedido_incompleto", "F");
            Upd.Add("despachar_existencia_parcial", "@despachar_existencia_parcial", "F");
            Upd.Add("convertir_decimales_a_umbas", "@convertir_decimales_a_umbas", "F");
            Upd.Add("generar_pedido_ingreso_bodega_destino", "@generar_pedido_ingreso_bodega_destino", "F");
            Upd.Add("generar_recepcion_auto_bodega_destino", "@generar_recepcion_auto_bodega_destino", "F");
            Upd.Add("codigo_proveedor_produccion", "@codigo_proveedor_produccion", "F");
            Upd.Add("idfamilia", "@idfamilia", "F");
            Upd.Add("idclasificacion", "@idclasificacion", "F");
            Upd.Add("idmarca", "@idmarca", "F");
            Upd.Add("idtipoproducto", "@idtipoproducto", "F");
            Upd.Add("control_lote", "@control_lote", "F");
            Upd.Add("control_vencimiento", "@control_vencimiento", "F");
            Upd.Add("genera_lp", "@genera_lp", "F");
            Upd.Add("nombre_ejecutable", "@nombre_ejecutable", "F");
            Upd.Add("idtipodocumentotransferenciasingreso", "@idtipodocumentotransferenciasingreso", "F");
            Upd.Add("crear_recepcion_de_transferencia_nav", "@crear_recepcion_de_transferencia_nav", "F");
            Upd.Add("control_peso", "@control_peso", "F");
            Upd.Add("crear_recepcion_de_compra_nav", "@crear_recepcion_de_compra_nav", "F");
            Upd.Add("idacuerdoenc", "@idacuerdoenc", "F");
            Upd.Add("idtipoetiqueta", "@idtipoetiqueta", "F");
            Upd.Add("equiparar_cliente_con_propietario_en_doc_salida", "@equiparar_cliente_con_propietario_en_doc_salida", "F");
            Upd.Add("push_ingreso_nav_desde_hh", "@push_ingreso_nav_desde_hh", "F");
            Upd.Add("reservar_umbas_primero", "@reservar_umbas_primero", "F");
            Upd.Add("implosion_automatica", "@implosion_automatica", "F");
            Upd.Add("explosion_automatica", "@explosion_automatica", "F");
            Upd.Add("ejecutar_en_despacho_automaticamente", "@ejecutar_en_despacho_automaticamente", "F");
            Upd.Add("idtiporotacion", "@idtiporotacion", "F");
            Upd.Add("explosion_automatica_desde_ubicacion_picking", "@explosion_automatica_desde_ubicacion_picking", "F");
            Upd.Add("explosion_automatica_nivel_max", "@explosion_automatica_nivel_max", "F");
            Upd.Add("conservar_zona_picking_clavaud", "@conservar_zona_picking_clavaud", "F");
            Upd.Add("recepcion_genera_historico", "@recepcion_genera_historico", "F");
            Upd.Add("excluir_ubicaciones_reabasto", "@excluir_ubicaciones_reabasto", "F");
            Upd.Add("considerar_disponibilidad_ubicacion_reabasto", "@considerar_disponibilidad_ubicacion_reabasto", "F");
            Upd.Add("considerar_paletizado_en_reabasto", "@considerar_paletizado_en_reabasto", "F");
            Upd.Add("dias_vida_defecto_perecederos", "@dias_vida_defecto_perecederos", "F");
            Upd.Add("codigo_bodega_erp_nc", "@codigo_bodega_erp_nc", "F");
            Upd.Add("lote_defecto_entrada_nc", "@lote_defecto_entrada_nc", "F");
            Upd.Add("vence_defecto_nc", "@vence_defecto_nc", "F");
            Upd.Add("idproductoestado_nc", "@idproductoestado_nc", "F");
            Upd.Add("lote_defecto_entrada_mercancia_sap", "@lote_defecto_entrada_mercancia_sap", "F");
            Upd.Add("fecha_vence_defecto", "@fecha_vence_defecto", "F");
            Upd.Add("interface_sap", "@interface_sap", "F");
            Upd.Add("sap_control_draft_ajustes", "@sap_control_draft_ajustes", "F");
            Upd.Add("sap_control_draft_traslados", "@sap_control_draft_traslados", "F");
            Upd.Add("inferir_bonificacion_pedido_sap", "@inferir_bonificacion_pedido_sap", "F");
            Upd.Add("rechazar_bonificacion_incompleta", "@rechazar_bonificacion_incompleta", "F");
            Upd.Add("idindicerotacion", "@idindicerotacion", "F");
            Upd.Add("rango_dias_importacion", "@rango_dias_importacion", "F");
            Upd.Add("equiparar_productos", "@equiparar_productos", "F");
            Upd.Add("bodega_facturacion", "@bodega_facturacion", "F");
            Upd.Add("valida_solo_codigo", "@valida_solo_codigo", "F");
            Upd.Add("excluir_recepcion_picking", "@excluir_recepcion_picking", "F");
            Upd.Add("bodega_prorrateo", "@bodega_prorrateo", "F");
            Upd.Add("bodega_prorrateo1", "@bodega_prorrateo1", "F");
            Upd.Where("idnavconfigenc = @idnavconfigenc");

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

            Binding(cmd, oBeI_nav_config_enc);

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

    public int Eliminar(IConfiguration config, clsBeI_nav_config_enc oBeI_nav_config_enc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from I_nav_config_enc" +
             "  Where(idnavconfigenc = @idnavconfigenc)");

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

            cmd.Parameters.Add(new SqlParameter("@idnavconfigenc", oBeI_nav_config_enc.Idnavconfigenc));

            int rowsAffected = cmd.ExecuteNonQuery();

            if (!Es_Transaccion_Remota)
                if (lTransaction != null)
                    lTransaction.Commit();

            return rowsAffected;

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
    }

    public DataTable Listar(IConfiguration config)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = "Select * FROM I_nav_config_enc";
            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            return dt;

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
    }

    public static bool GetSingle(IConfiguration config, ref clsBeI_nav_config_enc pBeI_nav_config_enc)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM I_nav_config_enc" +
            " Where(idnavconfigenc = @idnavconfigenc)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@idnavconfigenc", pBeI_nav_config_enc.Idnavconfigenc));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@idempresa", pBeI_nav_config_enc.Idempresa));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@idbodega", pBeI_nav_config_enc.Idbodega));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@idPropietario", pBeI_nav_config_enc.IdPropietario));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@idUsuario", pBeI_nav_config_enc.IdUsuario));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@nombre", pBeI_nav_config_enc.Nombre));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_agr", pBeI_nav_config_enc.Fec_agr));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@user_agr", pBeI_nav_config_enc.User_agr));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_mod", pBeI_nav_config_enc.Fec_mod));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@user_mod", pBeI_nav_config_enc.User_mod));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdProductoEstado", pBeI_nav_config_enc.IdProductoEstado));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@rechazar_pedido_incompleto", pBeI_nav_config_enc.Rechazar_pedido_incompleto));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@despachar_existencia_parcial", pBeI_nav_config_enc.Despachar_existencia_parcial));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@convertir_decimales_a_umbas", pBeI_nav_config_enc.Convertir_decimales_a_umbas));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@generar_pedido_ingreso_bodega_destino", pBeI_nav_config_enc.Generar_pedido_ingreso_bodega_destino));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@generar_recepcion_auto_bodega_destino", pBeI_nav_config_enc.Generar_recepcion_auto_bodega_destino));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@codigo_proveedor_produccion", pBeI_nav_config_enc.Codigo_proveedor_produccion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@idFamilia", pBeI_nav_config_enc.IdFamilia));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@idclasificacion", pBeI_nav_config_enc.Idclasificacion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@idMarca", pBeI_nav_config_enc.IdMarca));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@idTipoProducto", pBeI_nav_config_enc.IdTipoProducto));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@control_lote", pBeI_nav_config_enc.Control_lote));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@control_vencimiento", pBeI_nav_config_enc.Control_vencimiento));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@genera_lp", pBeI_nav_config_enc.Genera_lp));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@nombre_ejecutable", pBeI_nav_config_enc.Nombre_ejecutable));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTipoDocumentoTransferenciasIngreso", pBeI_nav_config_enc.IdTipoDocumentoTransferenciasIngreso));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@crear_recepcion_de_transferencia_nav", pBeI_nav_config_enc.Crear_recepcion_de_transferencia_nav));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@control_peso", pBeI_nav_config_enc.Control_peso));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@crear_recepcion_de_compra_nav", pBeI_nav_config_enc.Crear_recepcion_de_compra_nav));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdAcuerdoEnc", pBeI_nav_config_enc.IdAcuerdoEnc));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTipoEtiqueta", pBeI_nav_config_enc.IdTipoEtiqueta));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@equiparar_cliente_con_propietario_en_doc_salida", pBeI_nav_config_enc.Equiparar_cliente_con_propietario_en_doc_salida));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@push_ingreso_nav_desde_hh", pBeI_nav_config_enc.Push_ingreso_nav_desde_hh));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@reservar_umbas_primero", pBeI_nav_config_enc.Reservar_umbas_primero));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@implosion_automatica", pBeI_nav_config_enc.Implosion_automatica));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@explosion_automatica", pBeI_nav_config_enc.Explosion_automatica));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Ejecutar_En_Despacho_Automaticamente", pBeI_nav_config_enc.Ejecutar_En_Despacho_Automaticamente));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTipoRotacion", pBeI_nav_config_enc.IdTipoRotacion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@explosion_automatica_desde_ubicacion_picking", pBeI_nav_config_enc.Explosion_automatica_desde_ubicacion_picking));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@explosion_automatica_nivel_max", pBeI_nav_config_enc.Explosion_automatica_nivel_max));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@conservar_zona_picking_clavaud", pBeI_nav_config_enc.Conservar_zona_picking_clavaud));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@recepcion_genera_historico", pBeI_nav_config_enc.Recepcion_genera_historico));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@excluir_ubicaciones_reabasto", pBeI_nav_config_enc.Excluir_ubicaciones_reabasto));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@considerar_disponibilidad_ubicacion_reabasto", pBeI_nav_config_enc.Considerar_disponibilidad_ubicacion_reabasto));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@considerar_paletizado_en_reabasto", pBeI_nav_config_enc.Considerar_paletizado_en_reabasto));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@dias_vida_defecto_perecederos", pBeI_nav_config_enc.Dias_vida_defecto_perecederos));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@codigo_bodega_erp_nc", pBeI_nav_config_enc.Codigo_bodega_erp_nc));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@lote_defecto_entrada_nc", pBeI_nav_config_enc.Lote_defecto_entrada_nc));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@vence_defecto_nc", pBeI_nav_config_enc.Vence_defecto_nc));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdProductoEstado_NC", pBeI_nav_config_enc.IdProductoEstado_NC));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@lote_defecto_entrada_mercancia_sap", pBeI_nav_config_enc.Lote_defecto_entrada_mercancia_sap));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fecha_vence_defecto", pBeI_nav_config_enc.Fecha_vence_defecto));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@interface_sap", pBeI_nav_config_enc.Interface_sap));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@sap_control_draft_ajustes", pBeI_nav_config_enc.Sap_control_draft_ajustes));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@sap_control_draft_traslados", pBeI_nav_config_enc.Sap_control_draft_traslados));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@inferir_bonificacion_pedido_sap", pBeI_nav_config_enc.Inferir_bonificacion_pedido_sap));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@rechazar_bonificacion_incompleta", pBeI_nav_config_enc.Rechazar_bonificacion_incompleta));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdIndiceRotacion", pBeI_nav_config_enc.IdIndiceRotacion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@rango_dias_importacion", pBeI_nav_config_enc.Rango_dias_importacion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@equiparar_productos", pBeI_nav_config_enc.Equiparar_productos));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@bodega_facturacion", pBeI_nav_config_enc.Bodega_facturacion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@valida_solo_codigo", pBeI_nav_config_enc.Valida_solo_codigo));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@excluir_recepcion_picking", pBeI_nav_config_enc.Excluir_recepcion_picking));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@bodega_prorrateo", pBeI_nav_config_enc.Bodega_prorrateo));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@bodega_prorrateo1", pBeI_nav_config_enc.Bodega_prorrateo1));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeI_nav_config_enc, r);
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

    public static List<clsBeI_nav_config_enc> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeI_nav_config_enc> lreturnList = new List<clsBeI_nav_config_enc>();

        try
        {
            const string sp = "Select * FROM I_nav_config_enc";

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

                        clsBeI_nav_config_enc vBeI_nav_config_enc = new clsBeI_nav_config_enc();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeI_nav_config_enc = new clsBeI_nav_config_enc();
                            Cargar(ref vBeI_nav_config_enc, dr);
                            lreturnList.Add(vBeI_nav_config_enc);
                        }

                        lTransaction.Commit();
                    }

                    lConnection.Close();

                }

            }

            return lreturnList;

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
    }

    public static int MaxID(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;

        try
        {

            int lMax = 0;

            const string sp = "Select ISNULL(Max(idnavconfigenc),0) FROM I_nav_config_enc";

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {
                lConnection.Open();

                using (lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    using (SqlCommand lCommand = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text })
                    {
                        Object lreturnValue = lCommand.ExecuteScalar();
                        if (lreturnValue != DBNull.Value && lreturnValue != null)
                        {
                            lMax = Convert.ToInt32(lreturnValue);
                        }
                    }
                    lTransaction.Commit();
                }

                lConnection.Close();
            }

            return lMax;

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
    }
    public static int MaxID(IConfiguration config, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;
        int lMax = 0;

        try
        {


            const string sp = "Select ISNULL(Max(idnavconfigenc),0) FROM I_nav_config_enc";

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
    }


    public static clsBeI_nav_config_enc? GetSingle(clsBeI_nav_config_enc pBeI_nav_config_enc, SqlConnection pConection, SqlTransaction pTransaction)
    {
        if (pBeI_nav_config_enc == null)
            throw new ArgumentNullException(nameof(pBeI_nav_config_enc));

        if (pConection == null)
            throw new ArgumentNullException(nameof(pConection));

        if (pTransaction == null)
            throw new ArgumentNullException(nameof(pTransaction));

        try
        {
            const string sp = "SELECT TOP 1 * FROM I_nav_config_enc";

            using (var cmd = new SqlCommand(sp, pConection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;

                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        DataRow r = dt.Rows[0];
                        Cargar(ref pBeI_nav_config_enc, r);
                        return pBeI_nav_config_enc;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            string errorMessage = $"Error en GetSingle - {ex.Message}";
            throw new Exception(errorMessage, ex);
        }
    }

    public static clsBeI_nav_config_enc Get_Single_By_IdBodega_And_IdEmpresa(int pIdBodega,
                                                                            int pIdEmpresa,
                                                                            SqlConnection? pConnection,
                                                                            SqlTransaction? pTransaction)
    {
        clsBeI_nav_config_enc result = new clsBeI_nav_config_enc();

        try
        {
            string vSQL = @"SELECT * FROM i_nav_config_enc 
                            WHERE IdBodega = @IdBodega 
                            AND IdEmpresa = @IdEmpresa";

            using (var lDTA = new SqlDataAdapter(vSQL, pConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = pTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega);
                lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa);

                var lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];
                    var objConfig = new clsBeI_nav_config_enc();
                    Cargar(ref objConfig, lRow);
                    result = objConfig;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(Get_Single_By_IdBodega_And_IdEmpresa)} {ex.Message}", ex);
        }

        return result;
    }

    public static clsBeI_nav_config_enc? GetSingle(int pIdConfiguracionEncabezado,
                                                   SqlConnection pConnection,
                                                   SqlTransaction pTransaction)
    {
        try
        {
            string vSQL = "SELECT * FROM i_nav_config_enc WHERE idnavconfigenc = @pIdConfiguracionEncabezado";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, pConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = pTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@pIdConfiguracionEncabezado", pIdConfiguracionEncabezado);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];
                    clsBeI_nav_config_enc ObjUM = new clsBeI_nav_config_enc();
                    Cargar(ref ObjUM, lRow);
                    return ObjUM;
                }
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }
}