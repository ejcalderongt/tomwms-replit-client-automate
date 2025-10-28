using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using WMS.EntityCore.Datos_Maestros;

public class clsLnBodega
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeBodega oBeBodega, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }        
        double GetDecimal(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeBodega.IdBodega = GetInt("IdBodega");
            oBeBodega.IdPais = GetInt("IdPais");
            oBeBodega.IdEmpresa = GetInt("IdEmpresa");
            oBeBodega.Codigo = GetString("codigo");
            oBeBodega.Codigo_barra = GetString("codigo_barra");
            oBeBodega.Nombre = GetString("nombre");
            oBeBodega.Nombre_comercial = GetString("nombre_comercial");
            oBeBodega.Direccion = GetString("direccion");
            oBeBodega.Telefono = GetString("telefono");
            oBeBodega.Email = GetString("email");
            oBeBodega.Encargado = GetString("encargado");
            oBeBodega.Ubic_recepcion = GetString("ubic_recepcion");
            oBeBodega.Ubic_picking = GetString("ubic_picking");
            oBeBodega.Ubic_despacho = GetString("ubic_despacho");
            oBeBodega.Ubic_merma = GetString("ubic_merma");
            oBeBodega.User_agr = GetString("user_agr");
            oBeBodega.Fec_agr = GetDate("fec_agr");
            oBeBodega.User_mod = GetString("user_mod");
            oBeBodega.Fec_mod = GetDate("fec_mod");
            oBeBodega.Activo = GetBool("activo");
            oBeBodega.Coordenada_x = GetString("coordenada_x");
            oBeBodega.Coordenada_y = GetString("coordenada_y");
            oBeBodega.Largo = GetDecimal("largo");
            oBeBodega.Ancho = GetDecimal("ancho");
            oBeBodega.Alto = GetDecimal("alto");
            oBeBodega.Reservar_stocks_por_linea = GetBool("reservar_stocks_por_linea");
            oBeBodega.Rechazar_pedido_por_stock = GetBool("rechazar_pedido_por_stock");
            oBeBodega.IdTipoTransaccion = GetString("IdTipoTransaccion");
            oBeBodega.Zoom = GetDecimal("zoom");
            oBeBodega.IdMotivoUbicacionDañadoPicking = GetInt("IdMotivoUbicacionDañadoPicking");
            oBeBodega.Cambio_ubicacion_auto = GetBool("cambio_ubicacion_auto");
            oBeBodega.Codigo_bodega_erp = GetString("codigo_bodega_erp");
            oBeBodega.Ubic_producto_ne = GetInt("ubic_producto_ne");
            oBeBodega.IdProductoEstadoNE = GetInt("IdProductoEstadoNE");
            oBeBodega.Cuenta_Ingreso_Mercancias = GetString("Cuenta_Ingreso_Mercancias");
            oBeBodega.Cuenta_Egreso_Mercancias = GetString("Cuenta_Egreso_Mercancias");
            oBeBodega.Notificacion_voz = GetBool("notificacion_voz");
            oBeBodega.Control_tarifa_servicios = GetBool("control_tarifa_servicios");
            oBeBodega.Id_Motivo_Ubic_Reabasto = GetInt("Id_Motivo_Ubic_Reabasto");
            oBeBodega.Operador_defecto_en_documento_ingreso = GetBool("operador_defecto_en_documento_ingreso");
            oBeBodega.Es_bodega_fiscal = GetBool("es_bodega_fiscal");
            oBeBodega.Habilitar_ingreso_consolidado = GetBool("habilitar_ingreso_consolidado");
            oBeBodega.Bloquear_lp_hh = GetBool("bloquear_lp_hh");
            oBeBodega.Captura_estiba_ingreso = GetBool("captura_estiba_ingreso");
            oBeBodega.Captura_pallet_no_estandar = GetBool("captura_pallet_no_estandar");
            oBeBodega.Valor_porcentaje_iva = GetDecimal("valor_porcentaje_iva");
            oBeBodega.Permitir_Verificacion_Consolidada = GetBool("Permitir_Verificacion_Consolidada");
            oBeBodega.Control_banderas_cliente = GetBool("control_banderas_cliente");
            oBeBodega.IdTamañoEtiquetaUbicacionDefecto = GetInt("IdTamañoEtiquetaUbicacionDefecto");
            oBeBodega.Priorizar_ubicrec_sobre_ubicest = GetBool("priorizar_ubicrec_sobre_ubicest");
            oBeBodega.Ubicar_tarimas_completas_reabasto = GetBool("ubicar_tarimas_completas_reabasto");
            oBeBodega.Validar_disponibilidad_ubicaicon_destino = GetBool("validar_disponibilidad_ubicaicon_destino");
            oBeBodega.IdTipoTransaccionSalida = GetInt("IdTipoTransaccionSalida");
            oBeBodega.Permitir_eliminar_documento_salida = GetBool("permitir_eliminar_documento_salida");
            oBeBodega.Mostrar_area_en_hh = GetBool("mostrar_area_en_hh");
            oBeBodega.Confirmar_codigo_en_picking = GetBool("confirmar_codigo_en_picking");
            oBeBodega.Control_operador_ubicacion = GetBool("control_operador_ubicacion");
            oBeBodega.Inferir_origen_en_cambio_ubic = GetBool("inferir_origen_en_cambio_ubic");
            oBeBodega.Eliminar_documento_salida = GetBool("eliminar_documento_salida");
            oBeBodega.Operador_picking_realiza_verificacion = GetBool("operador_picking_realiza_verificacion");
            oBeBodega.Permitir_cambio_ubic_producto_picking = GetBool("permitir_cambio_ubic_producto_picking");
            oBeBodega.Despachar_producto_vencido = GetBool("despachar_producto_vencido");
            oBeBodega.TIPO_PANTALLA_PICKING = GetInt("TIPO_PANTALLA_PICKING");
            oBeBodega.Verificacion_consolidada = GetBool("verificacion_consolidada");
            oBeBodega.Tipo_pantalla_recepcion = GetInt("tipo_pantalla_recepcion");
            oBeBodega.Tipo_pantalla_verificacion = GetInt("tipo_pantalla_verificacion");
            oBeBodega.PERMITIR_BUEN_ESTADO_EN_REEMPLAZO = GetBool("PERMITIR_BUEN_ESTADO_EN_REEMPLAZO");
            oBeBodega.Restringir_vencimiento_en_reemplazo = GetBool("restringir_vencimiento_en_reemplazo");
            oBeBodega.Restringir_lote_en_reemplazo = GetBool("restringir_lote_en_reemplazo");
            oBeBodega.Industria_motriz = GetBool("industria_motriz");
            oBeBodega.Top_reabastecimiento_manual = GetInt("top_reabastecimiento_manual");
            oBeBodega.Permitir_decimales = GetBool("permitir_decimales");
            oBeBodega.Permitir_repeticiones_en_ingreso = GetBool("permitir_repeticiones_en_ingreso");
            oBeBodega.Dias_maximo_vencimiento_reemplazo = GetInt("dias_maximo_vencimiento_reemplazo");
            oBeBodega.Validar_existencias_inv_ini = GetBool("validar_existencias_inv_ini");
            oBeBodega.Calcular_ubicacion_sugerida_ml = GetBool("calcular_ubicacion_sugerida_ml");
            oBeBodega.Permitir_reemplazo_picking = GetBool("permitir_reemplazo_picking");
            oBeBodega.Permitir_no_encontrado_picking = GetBool("permitir_no_encontrado_picking");
            oBeBodega.Permitir_reemplazo_verificacion = GetBool("permitir_reemplazo_verificacion");
            oBeBodega.Ordenar_por_nombre_completo = GetBool("ordenar_por_nombre_completo");
            oBeBodega.Ordenar_picking_descendente = GetBool("ordenar_picking_descendente");
            oBeBodega.Permitir_Reemplazo_Picking_Misma_Licencia = GetBool("Permitir_Reemplazo_Picking_Misma_Licencia");
            oBeBodega.Dias_limite_retroactivo = GetInt("dias_limite_retroactivo");            
            oBeBodega.Filtrar_pedidos_usuario = GetBool("filtrar_pedidos_usuario");            
            oBeBodega.Liberar_stock_despachos_parciales = GetBool("liberar_stock_despachos_parciales");
            oBeBodega.Homologar_lote_vencimiento = GetBool("homologar_lote_vencimiento");
            oBeBodega.Escanear_licencia_picking = GetBool("escanear_licencia_picking");
            oBeBodega.Interface_SAP = GetBool("interface_SAP");
            oBeBodega.IdTipoEtiquetaLicencia = GetInt("IdTipoEtiquetaLicencia");
            oBeBodega.IdSimbologiaLicencia = GetInt("IdSimbologiaLicencia");
            oBeBodega.Restringir_areas_sap = GetBool("restringir_areas_sap");
            oBeBodega.Control_pallet_mixto = GetBool("control_pallet_mixto");
            oBeBodega.Despacho_automatico_hh = GetBool("despacho_automatico_hh");
        }
        catch (Exception ex)
        {            
            throw new Exception(ex.Message);
        }
    }

    public static int Insertar(IConfiguration config, clsBeBodega oBeBodega, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("bodega");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idpais", "@idpais", "F");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("codigo_barra", "@codigo_barra", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("nombre_comercial", "@nombre_comercial", "F");
            Ins.Add("direccion", "@direccion", "F");
            Ins.Add("telefono", "@telefono", "F");
            Ins.Add("email", "@email", "F");
            Ins.Add("encargado", "@encargado", "F");
            Ins.Add("ubic_recepcion", "@ubic_recepcion", "F");
            Ins.Add("ubic_picking", "@ubic_picking", "F");
            Ins.Add("ubic_despacho", "@ubic_despacho", "F");
            Ins.Add("ubic_merma", "@ubic_merma", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("coordenada_x", "@coordenada_x", "F");
            Ins.Add("coordenada_y", "@coordenada_y", "F");
            Ins.Add("largo", "@largo", "F");
            Ins.Add("ancho", "@ancho", "F");
            Ins.Add("alto", "@alto", "F");
            Ins.Add("reservar_stocks_por_linea", "@reservar_stocks_por_linea", "F");
            Ins.Add("rechazar_pedido_por_stock", "@rechazar_pedido_por_stock", "F");
            Ins.Add("idtipotransaccion", "@idtipotransaccion", "F");
            Ins.Add("zoom", "@zoom", "F");
            Ins.Add("idmotivoubicaciondañadopicking", "@idmotivoubicaciondañadopicking", "F");
            Ins.Add("cambio_ubicacion_auto", "@cambio_ubicacion_auto", "F");
            Ins.Add("codigo_bodega_erp", "@codigo_bodega_erp", "F");
            Ins.Add("ubic_producto_ne", "@ubic_producto_ne", "F");
            Ins.Add("idproductoestadone", "@idproductoestadone", "F");
            Ins.Add("cuenta_ingreso_mercancias", "@cuenta_ingreso_mercancias", "F");
            Ins.Add("cuenta_egreso_mercancias", "@cuenta_egreso_mercancias", "F");
            Ins.Add("notificacion_voz", "@notificacion_voz", "F");
            Ins.Add("control_tarifa_servicios", "@control_tarifa_servicios", "F");
            Ins.Add("id_motivo_ubic_reabasto", "@id_motivo_ubic_reabasto", "F");
            Ins.Add("operador_defecto_en_documento_ingreso", "@operador_defecto_en_documento_ingreso", "F");
            Ins.Add("es_bodega_fiscal", "@es_bodega_fiscal", "F");
            Ins.Add("habilitar_ingreso_consolidado", "@habilitar_ingreso_consolidado", "F");
            Ins.Add("bloquear_lp_hh", "@bloquear_lp_hh", "F");
            Ins.Add("captura_estiba_ingreso", "@captura_estiba_ingreso", "F");
            Ins.Add("captura_pallet_no_estandar", "@captura_pallet_no_estandar", "F");
            Ins.Add("valor_porcentaje_iva", "@valor_porcentaje_iva", "F");
            Ins.Add("permitir_verificacion_consolidada", "@permitir_verificacion_consolidada", "F");
            Ins.Add("control_banderas_cliente", "@control_banderas_cliente", "F");
            Ins.Add("idtamañoetiquetaubicaciondefecto", "@idtamañoetiquetaubicaciondefecto", "F");
            Ins.Add("priorizar_ubicrec_sobre_ubicest", "@priorizar_ubicrec_sobre_ubicest", "F");
            Ins.Add("ubicar_tarimas_completas_reabasto", "@ubicar_tarimas_completas_reabasto", "F");
            Ins.Add("validar_disponibilidad_ubicaicon_destino", "@validar_disponibilidad_ubicaicon_destino", "F");
            Ins.Add("idtipotransaccionsalida", "@idtipotransaccionsalida", "F");
            Ins.Add("permitir_eliminar_documento_salida", "@permitir_eliminar_documento_salida", "F");
            Ins.Add("mostrar_area_en_hh", "@mostrar_area_en_hh", "F");
            Ins.Add("confirmar_codigo_en_picking", "@confirmar_codigo_en_picking", "F");
            Ins.Add("control_operador_ubicacion", "@control_operador_ubicacion", "F");
            Ins.Add("inferir_origen_en_cambio_ubic", "@inferir_origen_en_cambio_ubic", "F");
            Ins.Add("eliminar_documento_salida", "@eliminar_documento_salida", "F");
            Ins.Add("operador_picking_realiza_verificacion", "@operador_picking_realiza_verificacion", "F");
            Ins.Add("permitir_cambio_ubic_producto_picking", "@permitir_cambio_ubic_producto_picking", "F");
            Ins.Add("despachar_producto_vencido", "@despachar_producto_vencido", "F");
            Ins.Add("tipo_pantalla_picking", "@tipo_pantalla_picking", "F");
            Ins.Add("verificacion_consolidada", "@verificacion_consolidada", "F");
            Ins.Add("tipo_pantalla_recepcion", "@tipo_pantalla_recepcion", "F");
            Ins.Add("tipo_pantalla_verificacion", "@tipo_pantalla_verificacion", "F");
            Ins.Add("permitir_buen_estado_en_reemplazo", "@permitir_buen_estado_en_reemplazo", "F");
            Ins.Add("restringir_vencimiento_en_reemplazo", "@restringir_vencimiento_en_reemplazo", "F");
            Ins.Add("restringir_lote_en_reemplazo", "@restringir_lote_en_reemplazo", "F");
            Ins.Add("industria_motriz", "@industria_motriz", "F");
            Ins.Add("top_reabastecimiento_manual", "@top_reabastecimiento_manual", "F");
            Ins.Add("permitir_decimales", "@permitir_decimales", "F");
            Ins.Add("permitir_repeticiones_en_ingreso", "@permitir_repeticiones_en_ingreso", "F");
            Ins.Add("dias_maximo_vencimiento_reemplazo", "@dias_maximo_vencimiento_reemplazo", "F");
            Ins.Add("validar_existencias_inv_ini", "@validar_existencias_inv_ini", "F");
            Ins.Add("calcular_ubicacion_sugerida_ml", "@calcular_ubicacion_sugerida_ml", "F");
            Ins.Add("permitir_reemplazo_picking", "@permitir_reemplazo_picking", "F");
            Ins.Add("permitir_no_encontrado_picking", "@permitir_no_encontrado_picking", "F");
            Ins.Add("permitir_reemplazo_verificacion", "@permitir_reemplazo_verificacion", "F");
            Ins.Add("ordenar_por_nombre_completo", "@ordenar_por_nombre_completo", "F");
            Ins.Add("ordenar_picking_descendente", "@ordenar_picking_descendente", "F");
            Ins.Add("permitir_reemplazo_picking_misma_licencia", "@permitir_reemplazo_picking_misma_licencia", "F");
            Ins.Add("dias_limite_retroactivo", "@dias_limite_retroactivo", "F");
            Ins.Add("horario_ejecucion_historico", "@horario_ejecucion_historico", "F");
            Ins.Add("filtrar_pedidos_usuario", "@filtrar_pedidos_usuario", "F");
            Ins.Add("liberar_stock_depachos_parciales", "@liberar_stock_depachos_parciales", "F");
            Ins.Add("liberar_stock_despachos_parciales", "@liberar_stock_despachos_parciales", "F");
            Ins.Add("homologar_lote_vencimiento", "@homologar_lote_vencimiento", "F");
            Ins.Add("escanear_licencia_picking", "@escanear_licencia_picking", "F");
            Ins.Add("interface_sap", "@interface_sap", "F");
            Ins.Add("idtipoetiquetalicencia", "@idtipoetiquetalicencia", "F");
            Ins.Add("idsimbologialicencia", "@idsimbologialicencia", "F");
            Ins.Add("restringir_areas_sap", "@restringir_areas_sap", "F");
            Ins.Add("control_pallet_mixto", "@control_pallet_mixto", "F");
            Ins.Add("despacho_automatico_hh", "@despacho_automatico_hh", "F");

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

            Bind(cmd, oBeBodega);

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

    public static int Insertar(IConfiguration config, clsBeBodega oBeBodega)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("bodega");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idpais", "@idpais", "F");
            Ins.Add("idempresa", "@idempresa", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("codigo_barra", "@codigo_barra", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("nombre_comercial", "@nombre_comercial", "F");
            Ins.Add("direccion", "@direccion", "F");
            Ins.Add("telefono", "@telefono", "F");
            Ins.Add("email", "@email", "F");
            Ins.Add("encargado", "@encargado", "F");
            Ins.Add("ubic_recepcion", "@ubic_recepcion", "F");
            Ins.Add("ubic_picking", "@ubic_picking", "F");
            Ins.Add("ubic_despacho", "@ubic_despacho", "F");
            Ins.Add("ubic_merma", "@ubic_merma", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("coordenada_x", "@coordenada_x", "F");
            Ins.Add("coordenada_y", "@coordenada_y", "F");
            Ins.Add("largo", "@largo", "F");
            Ins.Add("ancho", "@ancho", "F");
            Ins.Add("alto", "@alto", "F");
            Ins.Add("reservar_stocks_por_linea", "@reservar_stocks_por_linea", "F");
            Ins.Add("rechazar_pedido_por_stock", "@rechazar_pedido_por_stock", "F");
            Ins.Add("idtipotransaccion", "@idtipotransaccion", "F");
            Ins.Add("zoom", "@zoom", "F");
            Ins.Add("idmotivoubicaciondañadopicking", "@idmotivoubicaciondañadopicking", "F");
            Ins.Add("cambio_ubicacion_auto", "@cambio_ubicacion_auto", "F");
            Ins.Add("codigo_bodega_erp", "@codigo_bodega_erp", "F");
            Ins.Add("ubic_producto_ne", "@ubic_producto_ne", "F");
            Ins.Add("idproductoestadone", "@idproductoestadone", "F");
            Ins.Add("cuenta_ingreso_mercancias", "@cuenta_ingreso_mercancias", "F");
            Ins.Add("cuenta_egreso_mercancias", "@cuenta_egreso_mercancias", "F");
            Ins.Add("notificacion_voz", "@notificacion_voz", "F");
            Ins.Add("control_tarifa_servicios", "@control_tarifa_servicios", "F");
            Ins.Add("id_motivo_ubic_reabasto", "@id_motivo_ubic_reabasto", "F");
            Ins.Add("operador_defecto_en_documento_ingreso", "@operador_defecto_en_documento_ingreso", "F");
            Ins.Add("es_bodega_fiscal", "@es_bodega_fiscal", "F");
            Ins.Add("habilitar_ingreso_consolidado", "@habilitar_ingreso_consolidado", "F");
            Ins.Add("bloquear_lp_hh", "@bloquear_lp_hh", "F");
            Ins.Add("captura_estiba_ingreso", "@captura_estiba_ingreso", "F");
            Ins.Add("captura_pallet_no_estandar", "@captura_pallet_no_estandar", "F");
            Ins.Add("valor_porcentaje_iva", "@valor_porcentaje_iva", "F");
            Ins.Add("permitir_verificacion_consolidada", "@permitir_verificacion_consolidada", "F");
            Ins.Add("control_banderas_cliente", "@control_banderas_cliente", "F");
            Ins.Add("idtamañoetiquetaubicaciondefecto", "@idtamañoetiquetaubicaciondefecto", "F");
            Ins.Add("priorizar_ubicrec_sobre_ubicest", "@priorizar_ubicrec_sobre_ubicest", "F");
            Ins.Add("ubicar_tarimas_completas_reabasto", "@ubicar_tarimas_completas_reabasto", "F");
            Ins.Add("validar_disponibilidad_ubicaicon_destino", "@validar_disponibilidad_ubicaicon_destino", "F");
            Ins.Add("idtipotransaccionsalida", "@idtipotransaccionsalida", "F");
            Ins.Add("permitir_eliminar_documento_salida", "@permitir_eliminar_documento_salida", "F");
            Ins.Add("mostrar_area_en_hh", "@mostrar_area_en_hh", "F");
            Ins.Add("confirmar_codigo_en_picking", "@confirmar_codigo_en_picking", "F");
            Ins.Add("control_operador_ubicacion", "@control_operador_ubicacion", "F");
            Ins.Add("inferir_origen_en_cambio_ubic", "@inferir_origen_en_cambio_ubic", "F");
            Ins.Add("eliminar_documento_salida", "@eliminar_documento_salida", "F");
            Ins.Add("operador_picking_realiza_verificacion", "@operador_picking_realiza_verificacion", "F");
            Ins.Add("permitir_cambio_ubic_producto_picking", "@permitir_cambio_ubic_producto_picking", "F");
            Ins.Add("despachar_producto_vencido", "@despachar_producto_vencido", "F");
            Ins.Add("tipo_pantalla_picking", "@tipo_pantalla_picking", "F");
            Ins.Add("verificacion_consolidada", "@verificacion_consolidada", "F");
            Ins.Add("tipo_pantalla_recepcion", "@tipo_pantalla_recepcion", "F");
            Ins.Add("tipo_pantalla_verificacion", "@tipo_pantalla_verificacion", "F");
            Ins.Add("permitir_buen_estado_en_reemplazo", "@permitir_buen_estado_en_reemplazo", "F");
            Ins.Add("restringir_vencimiento_en_reemplazo", "@restringir_vencimiento_en_reemplazo", "F");
            Ins.Add("restringir_lote_en_reemplazo", "@restringir_lote_en_reemplazo", "F");
            Ins.Add("industria_motriz", "@industria_motriz", "F");
            Ins.Add("top_reabastecimiento_manual", "@top_reabastecimiento_manual", "F");
            Ins.Add("permitir_decimales", "@permitir_decimales", "F");
            Ins.Add("permitir_repeticiones_en_ingreso", "@permitir_repeticiones_en_ingreso", "F");
            Ins.Add("dias_maximo_vencimiento_reemplazo", "@dias_maximo_vencimiento_reemplazo", "F");
            Ins.Add("validar_existencias_inv_ini", "@validar_existencias_inv_ini", "F");
            Ins.Add("calcular_ubicacion_sugerida_ml", "@calcular_ubicacion_sugerida_ml", "F");
            Ins.Add("permitir_reemplazo_picking", "@permitir_reemplazo_picking", "F");
            Ins.Add("permitir_no_encontrado_picking", "@permitir_no_encontrado_picking", "F");
            Ins.Add("permitir_reemplazo_verificacion", "@permitir_reemplazo_verificacion", "F");
            Ins.Add("ordenar_por_nombre_completo", "@ordenar_por_nombre_completo", "F");
            Ins.Add("ordenar_picking_descendente", "@ordenar_picking_descendente", "F");
            Ins.Add("permitir_reemplazo_picking_misma_licencia", "@permitir_reemplazo_picking_misma_licencia", "F");
            Ins.Add("dias_limite_retroactivo", "@dias_limite_retroactivo", "F");
            Ins.Add("horario_ejecucion_historico", "@horario_ejecucion_historico", "F");
            Ins.Add("filtrar_pedidos_usuario", "@filtrar_pedidos_usuario", "F");
            Ins.Add("liberar_stock_depachos_parciales", "@liberar_stock_depachos_parciales", "F");
            Ins.Add("liberar_stock_despachos_parciales", "@liberar_stock_despachos_parciales", "F");
            Ins.Add("homologar_lote_vencimiento", "@homologar_lote_vencimiento", "F");
            Ins.Add("escanear_licencia_picking", "@escanear_licencia_picking", "F");
            Ins.Add("interface_sap", "@interface_sap", "F");
            Ins.Add("idtipoetiquetalicencia", "@idtipoetiquetalicencia", "F");
            Ins.Add("idsimbologialicencia", "@idsimbologialicencia", "F");
            Ins.Add("restringir_areas_sap", "@restringir_areas_sap", "F");
            Ins.Add("control_pallet_mixto", "@control_pallet_mixto", "F");
            Ins.Add("despacho_automatico_hh", "@despacho_automatico_hh", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeBodega);

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

    public static int Actualizar(IConfiguration config, clsBeBodega oBeBodega, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("bodega");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("idpais", "@idpais", "F");
            Upd.Add("idempresa", "@idempresa", "F");
            Upd.Add("codigo", "@codigo", "F");
            Upd.Add("codigo_barra", "@codigo_barra", "F");
            Upd.Add("nombre", "@nombre", "F");
            Upd.Add("nombre_comercial", "@nombre_comercial", "F");
            Upd.Add("direccion", "@direccion", "F");
            Upd.Add("telefono", "@telefono", "F");
            Upd.Add("email", "@email", "F");
            Upd.Add("encargado", "@encargado", "F");
            Upd.Add("ubic_recepcion", "@ubic_recepcion", "F");
            Upd.Add("ubic_picking", "@ubic_picking", "F");
            Upd.Add("ubic_despacho", "@ubic_despacho", "F");
            Upd.Add("ubic_merma", "@ubic_merma", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("coordenada_x", "@coordenada_x", "F");
            Upd.Add("coordenada_y", "@coordenada_y", "F");
            Upd.Add("largo", "@largo", "F");
            Upd.Add("ancho", "@ancho", "F");
            Upd.Add("alto", "@alto", "F");
            Upd.Add("reservar_stocks_por_linea", "@reservar_stocks_por_linea", "F");
            Upd.Add("rechazar_pedido_por_stock", "@rechazar_pedido_por_stock", "F");
            Upd.Add("idtipotransaccion", "@idtipotransaccion", "F");
            Upd.Add("zoom", "@zoom", "F");
            Upd.Add("idmotivoubicaciondañadopicking", "@idmotivoubicaciondañadopicking", "F");
            Upd.Add("cambio_ubicacion_auto", "@cambio_ubicacion_auto", "F");
            Upd.Add("codigo_bodega_erp", "@codigo_bodega_erp", "F");
            Upd.Add("ubic_producto_ne", "@ubic_producto_ne", "F");
            Upd.Add("idproductoestadone", "@idproductoestadone", "F");
            Upd.Add("cuenta_ingreso_mercancias", "@cuenta_ingreso_mercancias", "F");
            Upd.Add("cuenta_egreso_mercancias", "@cuenta_egreso_mercancias", "F");
            Upd.Add("notificacion_voz", "@notificacion_voz", "F");
            Upd.Add("control_tarifa_servicios", "@control_tarifa_servicios", "F");
            Upd.Add("id_motivo_ubic_reabasto", "@id_motivo_ubic_reabasto", "F");
            Upd.Add("operador_defecto_en_documento_ingreso", "@operador_defecto_en_documento_ingreso", "F");
            Upd.Add("es_bodega_fiscal", "@es_bodega_fiscal", "F");
            Upd.Add("habilitar_ingreso_consolidado", "@habilitar_ingreso_consolidado", "F");
            Upd.Add("bloquear_lp_hh", "@bloquear_lp_hh", "F");
            Upd.Add("captura_estiba_ingreso", "@captura_estiba_ingreso", "F");
            Upd.Add("captura_pallet_no_estandar", "@captura_pallet_no_estandar", "F");
            Upd.Add("valor_porcentaje_iva", "@valor_porcentaje_iva", "F");
            Upd.Add("permitir_verificacion_consolidada", "@permitir_verificacion_consolidada", "F");
            Upd.Add("control_banderas_cliente", "@control_banderas_cliente", "F");
            Upd.Add("idtamañoetiquetaubicaciondefecto", "@idtamañoetiquetaubicaciondefecto", "F");
            Upd.Add("priorizar_ubicrec_sobre_ubicest", "@priorizar_ubicrec_sobre_ubicest", "F");
            Upd.Add("ubicar_tarimas_completas_reabasto", "@ubicar_tarimas_completas_reabasto", "F");
            Upd.Add("validar_disponibilidad_ubicaicon_destino", "@validar_disponibilidad_ubicaicon_destino", "F");
            Upd.Add("idtipotransaccionsalida", "@idtipotransaccionsalida", "F");
            Upd.Add("permitir_eliminar_documento_salida", "@permitir_eliminar_documento_salida", "F");
            Upd.Add("mostrar_area_en_hh", "@mostrar_area_en_hh", "F");
            Upd.Add("confirmar_codigo_en_picking", "@confirmar_codigo_en_picking", "F");
            Upd.Add("control_operador_ubicacion", "@control_operador_ubicacion", "F");
            Upd.Add("inferir_origen_en_cambio_ubic", "@inferir_origen_en_cambio_ubic", "F");
            Upd.Add("eliminar_documento_salida", "@eliminar_documento_salida", "F");
            Upd.Add("operador_picking_realiza_verificacion", "@operador_picking_realiza_verificacion", "F");
            Upd.Add("permitir_cambio_ubic_producto_picking", "@permitir_cambio_ubic_producto_picking", "F");
            Upd.Add("despachar_producto_vencido", "@despachar_producto_vencido", "F");
            Upd.Add("tipo_pantalla_picking", "@tipo_pantalla_picking", "F");
            Upd.Add("verificacion_consolidada", "@verificacion_consolidada", "F");
            Upd.Add("tipo_pantalla_recepcion", "@tipo_pantalla_recepcion", "F");
            Upd.Add("tipo_pantalla_verificacion", "@tipo_pantalla_verificacion", "F");
            Upd.Add("permitir_buen_estado_en_reemplazo", "@permitir_buen_estado_en_reemplazo", "F");
            Upd.Add("restringir_vencimiento_en_reemplazo", "@restringir_vencimiento_en_reemplazo", "F");
            Upd.Add("restringir_lote_en_reemplazo", "@restringir_lote_en_reemplazo", "F");
            Upd.Add("industria_motriz", "@industria_motriz", "F");
            Upd.Add("top_reabastecimiento_manual", "@top_reabastecimiento_manual", "F");
            Upd.Add("permitir_decimales", "@permitir_decimales", "F");
            Upd.Add("permitir_repeticiones_en_ingreso", "@permitir_repeticiones_en_ingreso", "F");
            Upd.Add("dias_maximo_vencimiento_reemplazo", "@dias_maximo_vencimiento_reemplazo", "F");
            Upd.Add("validar_existencias_inv_ini", "@validar_existencias_inv_ini", "F");
            Upd.Add("calcular_ubicacion_sugerida_ml", "@calcular_ubicacion_sugerida_ml", "F");
            Upd.Add("permitir_reemplazo_picking", "@permitir_reemplazo_picking", "F");
            Upd.Add("permitir_no_encontrado_picking", "@permitir_no_encontrado_picking", "F");
            Upd.Add("permitir_reemplazo_verificacion", "@permitir_reemplazo_verificacion", "F");
            Upd.Add("ordenar_por_nombre_completo", "@ordenar_por_nombre_completo", "F");
            Upd.Add("ordenar_picking_descendente", "@ordenar_picking_descendente", "F");
            Upd.Add("permitir_reemplazo_picking_misma_licencia", "@permitir_reemplazo_picking_misma_licencia", "F");
            Upd.Add("dias_limite_retroactivo", "@dias_limite_retroactivo", "F");
            Upd.Add("horario_ejecucion_historico", "@horario_ejecucion_historico", "F");
            Upd.Add("filtrar_pedidos_usuario", "@filtrar_pedidos_usuario", "F");
            Upd.Add("liberar_stock_depachos_parciales", "@liberar_stock_depachos_parciales", "F");
            Upd.Add("liberar_stock_despachos_parciales", "@liberar_stock_despachos_parciales", "F");
            Upd.Add("homologar_lote_vencimiento", "@homologar_lote_vencimiento", "F");
            Upd.Add("escanear_licencia_picking", "@escanear_licencia_picking", "F");
            Upd.Add("interface_sap", "@interface_sap", "F");
            Upd.Add("idtipoetiquetalicencia", "@idtipoetiquetalicencia", "F");
            Upd.Add("idsimbologialicencia", "@idsimbologialicencia", "F");
            Upd.Add("restringir_areas_sap", "@restringir_areas_sap", "F");
            Upd.Add("control_pallet_mixto", "@control_pallet_mixto", "F");
            Upd.Add("despacho_automatico_hh", "@despacho_automatico_hh", "F");
            Upd.Where("IdBodega = @IdBodega");

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

            Bind(cmd, oBeBodega);

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

    public int Eliminar(IConfiguration config, clsBeBodega oBeBodega, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Bodega" +
             "  Where(IdBodega = @IdBodega)");

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

            cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeBodega.IdBodega));

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
            const string sp = "Select * FROM Bodega";
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

    public static bool GetSingle(IConfiguration config, ref clsBeBodega pBeBodega)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Bodega" +
            " Where(IdBodega = @IdBodega)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdBodega", pBeBodega.IdBodega));
            
            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeBodega, r);
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

    public static List<clsBeBodega> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeBodega> lreturnList = new List<clsBeBodega>();

        try
        {
            const string sp = "Select * FROM Bodega";

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

                        clsBeBodega vBeBodega = new clsBeBodega();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeBodega = new clsBeBodega();
                            Cargar(ref vBeBodega, dr);
                            lreturnList.Add(vBeBodega);
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

            const string sp = "Select ISNULL(Max(IdBodega),0) FROM Bodega";

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
                            lMax = int.Parse((String)lreturnValue);
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


            const string sp = "Select ISNULL(Max(IdBodega),0) FROM Bodega";

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
    public static void Bind(SqlCommand cmd, clsBeBodega oBeBodega)
    {
        cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeBodega.IdBodega));
        cmd.Parameters.Add(new SqlParameter("@IdPais", oBeBodega.IdPais));
        cmd.Parameters.Add(new SqlParameter("@IdEmpresa", oBeBodega.IdEmpresa));
        cmd.Parameters.Add(new SqlParameter("@codigo", (object?)oBeBodega.Codigo ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@codigo_barra", (object?)oBeBodega.Codigo_barra ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@nombre", (object?)oBeBodega.Nombre ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@nombre_comercial", (object?)oBeBodega.Nombre_comercial ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@direccion", (object?)oBeBodega.Direccion ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@telefono", (object?)oBeBodega.Telefono ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@email", (object?)oBeBodega.Email ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@encargado", (object?)oBeBodega.Encargado ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@ubic_recepcion", (object?)oBeBodega.Ubic_recepcion ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@ubic_picking", (object?)oBeBodega.Ubic_picking ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@ubic_despacho", (object?)oBeBodega.Ubic_despacho ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@ubic_merma", (object?)oBeBodega.Ubic_merma ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_agr", (object?)oBeBodega.User_agr ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", (object?)oBeBodega.Fec_agr ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_mod", (object?)oBeBodega.User_mod ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", (object?)oBeBodega.Fec_mod ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@activo", oBeBodega.Activo ? (object)oBeBodega.Activo : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@coordenada_x", (object?)oBeBodega.Coordenada_x ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@coordenada_y", (object?)oBeBodega.Coordenada_y ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@largo", oBeBodega.Largo != 0 ? (object)oBeBodega.Largo : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@ancho", oBeBodega.Ancho != 0 ? (object)oBeBodega.Ancho : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@alto", oBeBodega.Alto != 0 ? (object)oBeBodega.Alto : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@reservar_stocks_por_linea", oBeBodega.Reservar_stocks_por_linea ? (object)oBeBodega.Reservar_stocks_por_linea : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@rechazar_pedido_por_stock", oBeBodega.Rechazar_pedido_por_stock ? (object)oBeBodega.Rechazar_pedido_por_stock: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdTipoTransaccion", (object?)oBeBodega.IdTipoTransaccion ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@zoom", oBeBodega.Zoom != 0 ? (object)oBeBodega.Zoom : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdMotivoUbicacionDañadoPicking", oBeBodega.IdMotivoUbicacionDañadoPicking != 0 ? (object)oBeBodega.IdMotivoUbicacionDañadoPicking : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@cambio_ubicacion_auto", oBeBodega.Cambio_ubicacion_auto ? (object)oBeBodega.Cambio_ubicacion_auto : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@codigo_bodega_erp", (object?)oBeBodega.Codigo_bodega_erp ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@ubic_producto_ne", oBeBodega.Ubic_producto_ne != 0 ? (object)oBeBodega.Ubic_producto_ne : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdProductoEstadoNE", oBeBodega.IdProductoEstadoNE != 0 ? (object)oBeBodega.IdProductoEstadoNE : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Cuenta_Ingreso_Mercancias", (object?)oBeBodega.Cuenta_Ingreso_Mercancias ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Cuenta_Egreso_Mercancias", (object?)oBeBodega.Cuenta_Egreso_Mercancias ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@notificacion_voz", oBeBodega.Notificacion_voz ? (object)oBeBodega.Notificacion_voz : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@control_tarifa_servicios", oBeBodega.Control_tarifa_servicios? (object)oBeBodega.Control_tarifa_servicios : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Id_Motivo_Ubic_Reabasto", oBeBodega.Id_Motivo_Ubic_Reabasto!=0? (object)oBeBodega.Id_Motivo_Ubic_Reabasto: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@operador_defecto_en_documento_ingreso", oBeBodega.Operador_defecto_en_documento_ingreso? (object)oBeBodega.Operador_defecto_en_documento_ingreso : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@es_bodega_fiscal", oBeBodega.Es_bodega_fiscal? (object)oBeBodega.Es_bodega_fiscal : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@habilitar_ingreso_consolidado", oBeBodega.Habilitar_ingreso_consolidado? (object)oBeBodega.Habilitar_ingreso_consolidado: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@bloquear_lp_hh", oBeBodega.Bloquear_lp_hh? (object)oBeBodega.Bloquear_lp_hh: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@captura_estiba_ingreso", oBeBodega.Captura_estiba_ingreso ? (object)oBeBodega.Captura_estiba_ingreso : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@captura_pallet_no_estandar", oBeBodega.Captura_pallet_no_estandar? (object)oBeBodega.Captura_pallet_no_estandar: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@valor_porcentaje_iva", oBeBodega.Valor_porcentaje_iva != 0 ? (object)oBeBodega.Valor_porcentaje_iva: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Permitir_Verificacion_Consolidada", oBeBodega.Permitir_Verificacion_Consolidada? (object)oBeBodega.Permitir_Verificacion_Consolidada: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@control_banderas_cliente", oBeBodega.Control_banderas_cliente? (object)oBeBodega.Control_banderas_cliente: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdTamañoEtiquetaUbicacionDefecto", oBeBodega.IdTamañoEtiquetaUbicacionDefecto != 0 ? (object)oBeBodega.IdTamañoEtiquetaUbicacionDefecto: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@priorizar_ubicrec_sobre_ubicest", oBeBodega.Priorizar_ubicrec_sobre_ubicest? (object)oBeBodega.Priorizar_ubicrec_sobre_ubicest: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@ubicar_tarimas_completas_reabasto", oBeBodega.Ubicar_tarimas_completas_reabasto? (object)oBeBodega.Ubicar_tarimas_completas_reabasto: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@validar_disponibilidad_ubicaicon_destino", oBeBodega.Validar_disponibilidad_ubicaicon_destino? (object)oBeBodega.Validar_disponibilidad_ubicaicon_destino: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdTipoTransaccionSalida", (object?)oBeBodega.IdTipoTransaccionSalida ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@permitir_eliminar_documento_salida", oBeBodega.Permitir_eliminar_documento_salida? (object)oBeBodega.Permitir_eliminar_documento_salida: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@mostrar_area_en_hh", oBeBodega.Mostrar_area_en_hh? (object)oBeBodega.Mostrar_area_en_hh: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@confirmar_codigo_en_picking", oBeBodega.Confirmar_codigo_en_picking? (object)oBeBodega.Confirmar_codigo_en_picking: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@control_operador_ubicacion", oBeBodega.Control_operador_ubicacion? (object)oBeBodega.Control_operador_ubicacion: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@inferir_origen_en_cambio_ubic", oBeBodega.Inferir_origen_en_cambio_ubic? (object)oBeBodega.Inferir_origen_en_cambio_ubic: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@eliminar_documento_salida", oBeBodega.Eliminar_documento_salida? (object)oBeBodega.Eliminar_documento_salida: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@operador_picking_realiza_verificacion", oBeBodega.Operador_picking_realiza_verificacion? (object)oBeBodega.Operador_picking_realiza_verificacion: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@permitir_cambio_ubic_producto_picking", oBeBodega.Permitir_cambio_ubic_producto_picking? (object)oBeBodega.Permitir_cambio_ubic_producto_picking: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@despachar_producto_vencido", oBeBodega.Despachar_producto_vencido? (object)oBeBodega.Despachar_producto_vencido: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@TIPO_PANTALLA_PICKING", oBeBodega.TIPO_PANTALLA_PICKING != 0 ? (object)oBeBodega.TIPO_PANTALLA_PICKING: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@verificacion_consolidada", oBeBodega.Verificacion_consolidada? (object)oBeBodega.Verificacion_consolidada: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@tipo_pantalla_recepcion", oBeBodega.Tipo_pantalla_recepcion != 0 ? (object)oBeBodega.Tipo_pantalla_recepcion: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@tipo_pantalla_verificacion", oBeBodega.Tipo_pantalla_verificacion != 0 ? (object)oBeBodega.Tipo_pantalla_verificacion: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@PERMITIR_BUEN_ESTADO_EN_REEMPLAZO", oBeBodega.PERMITIR_BUEN_ESTADO_EN_REEMPLAZO? (object)oBeBodega.PERMITIR_BUEN_ESTADO_EN_REEMPLAZO: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@restringir_vencimiento_en_reemplazo", oBeBodega.Restringir_vencimiento_en_reemplazo? (object)oBeBodega.Restringir_vencimiento_en_reemplazo: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@restringir_lote_en_reemplazo", oBeBodega.Restringir_lote_en_reemplazo? (object)oBeBodega.Restringir_lote_en_reemplazo: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@industria_motriz", oBeBodega.Industria_motriz? (object)oBeBodega.Industria_motriz: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@top_reabastecimiento_manual", oBeBodega.Top_reabastecimiento_manual != 0 ? (object)oBeBodega.Top_reabastecimiento_manual: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@permitir_decimales", oBeBodega.Permitir_decimales? (object)oBeBodega.Permitir_decimales: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@permitir_repeticiones_en_ingreso", oBeBodega.Permitir_repeticiones_en_ingreso? (object)oBeBodega.Permitir_repeticiones_en_ingreso: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@dias_maximo_vencimiento_reemplazo", oBeBodega.Dias_maximo_vencimiento_reemplazo != 0 ? (object)oBeBodega.Dias_maximo_vencimiento_reemplazo: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@validar_existencias_inv_ini", oBeBodega.Validar_existencias_inv_ini? (object)oBeBodega.Validar_existencias_inv_ini: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@calcular_ubicacion_sugerida_ml", oBeBodega.Calcular_ubicacion_sugerida_ml? (object)oBeBodega.Calcular_ubicacion_sugerida_ml: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@permitir_reemplazo_picking", oBeBodega.Permitir_reemplazo_picking? (object)oBeBodega.Permitir_reemplazo_picking: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@permitir_no_encontrado_picking", oBeBodega.Permitir_no_encontrado_picking? (object)oBeBodega.Permitir_no_encontrado_picking: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@permitir_reemplazo_verificacion", oBeBodega.Permitir_reemplazo_verificacion? (object)oBeBodega.Permitir_reemplazo_verificacion: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@ordenar_por_nombre_completo", oBeBodega.Ordenar_por_nombre_completo? (object)oBeBodega.Ordenar_por_nombre_completo: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@ordenar_picking_descendente", oBeBodega.Ordenar_picking_descendente? (object)oBeBodega.Ordenar_picking_descendente: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Permitir_Reemplazo_Picking_Misma_Licencia", oBeBodega.Permitir_Reemplazo_Picking_Misma_Licencia? (object)oBeBodega.Permitir_Reemplazo_Picking_Misma_Licencia: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@dias_limite_retroactivo", oBeBodega.Dias_limite_retroactivo != 0 ? (object)oBeBodega.Dias_limite_retroactivo: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@horario_ejecucion_historico", (object?)oBeBodega.Horario_ejecucion_historico ?? DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@filtrar_pedidos_usuario", oBeBodega.Filtrar_pedidos_usuario? (object)oBeBodega.Filtrar_pedidos_usuario: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@liberar_stock_depachos_parciales", oBeBodega.Liberar_stock_depachos_parciales? (object)oBeBodega.Liberar_stock_depachos_parciales: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@liberar_stock_despachos_parciales", oBeBodega.Liberar_stock_despachos_parciales? (object)oBeBodega.Liberar_stock_despachos_parciales: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@homologar_lote_vencimiento", oBeBodega.Homologar_lote_vencimiento? (object)oBeBodega.Homologar_lote_vencimiento: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@escanear_licencia_picking", oBeBodega.Escanear_licencia_picking? (object)oBeBodega.Escanear_licencia_picking: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@interface_SAP", oBeBodega.Interface_SAP? (object)oBeBodega.Interface_SAP: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdTipoEtiquetaLicencia", oBeBodega.IdTipoEtiquetaLicencia != 0 ? (object)oBeBodega.IdTipoEtiquetaLicencia: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdSimbologiaLicencia", oBeBodega.IdSimbologiaLicencia != 0 ? (object)oBeBodega.IdSimbologiaLicencia: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@restringir_areas_sap", oBeBodega.Restringir_areas_sap? (object)oBeBodega.Restringir_areas_sap: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@control_pallet_mixto", oBeBodega.Control_pallet_mixto? (object)oBeBodega.Control_pallet_mixto: DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@despacho_automatico_hh", oBeBodega.Despacho_automatico_hh? (object)oBeBodega.Despacho_automatico_hh: DBNull.Value));
        
    }
    public static int InsertOrUpdate(IConfiguration config, clsBeBodega oBeBodega, SqlConnection? conn = null, SqlTransaction? tran = null)
    {
        bool isExternalTx = conn != null && tran != null;
        var connection = isExternalTx ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTran = null;

        if (!isExternalTx)
        {
            connection.Open();
            localTran = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        try
        {
            if (Existe(oBeBodega.IdBodega, connection, isExternalTx ? tran! : localTran))
                return Actualizar(config, oBeBodega, connection, isExternalTx ? tran : localTran);
            else
                return Insertar(config, oBeBodega, connection, isExternalTx ? tran : localTran);
        }
        catch
        {
            if (!isExternalTx) localTran?.Rollback();
            throw;
        }
        finally
        {
            if (!isExternalTx)
            {
                localTran?.Commit();
                connection.Close();
            }
        }
    }
    public static bool Existe(int idBodega, SqlConnection conn, SqlTransaction? tran = null)
    {
        using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM bodega WHERE IdBodega = @IdBodega", conn, tran))
        {
            cmd.Parameters.Add(new SqlParameter("@IdBodega", idBodega));
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }
    }
    public static bool Existe(int idBodega, IConfiguration config)
    {
        using (var conn = new SqlConnection(config.GetConnectionString("CST")))
        {
            conn.Open();

            using (var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted))
            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM bodega WHERE IdBodega = @IdBodega", conn, tran))
            {
                cmd.Parameters.Add(new SqlParameter("@IdBodega", idBodega));
                int count = (int)cmd.ExecuteScalar();
                tran.Commit();

                return count > 0;
            }
        }
    }

    public static List<clsBeBodega> GetAll( SqlConnection cn, SqlTransaction? tx = null)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeBodega> lreturnList = new List<clsBeBodega>();

        try
        {


            const string sql = @"SELECT  * FROM Bodega WHERE activo=1 ";

            using var cmd = new SqlCommand(sql, cn, tx);
            using var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count >0)
            {
                clsBeBodega vBeBodega = new clsBeBodega();

                foreach (DataRow dr in dt.Rows)
                {
                    vBeBodega = new clsBeBodega();
                    Cargar(ref vBeBodega, dr);
                    lreturnList.Add(vBeBodega);
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

    public static bool Exists_By_Codigo(string pCodigo, SqlConnection lConnection, SqlTransaction lTransaction)
    {
        try
        {
            bool lExists = false;
            string vSQL = "SELECT COUNT(1) FROM bodega WHERE Codigo = @Codigo";

            using (var lCommand = new SqlCommand(vSQL, lConnection, lTransaction))
            {
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@Codigo", pCodigo);

                object lReturnValue = lCommand.ExecuteScalar();

                if (lReturnValue != DBNull.Value && lReturnValue != null)
                {
                    lExists = Convert.ToInt32(lReturnValue) > 0;
                }
            }

            return lExists;
        }
        catch (Exception ex)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex.Message);
            throw new Exception(vMsgError);
        }
    }

    public static int Get_IdBodega_By_Codigo(string pCodigo, SqlConnection lConnection, SqlTransaction lTransaction)
    {
        int idBodega = 0;

        try
        {
            string vSQL = "SELECT IdBodega FROM bodega WHERE Codigo = @Codigo";

            using (var lCommand = new SqlCommand(vSQL, lConnection, lTransaction))
            {
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@Codigo", pCodigo);

                object lReturnValue = lCommand.ExecuteScalar();

                if (lReturnValue != DBNull.Value && lReturnValue != null)
                {
                    idBodega = Convert.ToInt32(lReturnValue);
                }
            }
        }
        catch (Exception ex)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex.Message);
            throw new Exception(vMsgError);
        }

        return idBodega;
    }

    public static bool Exists_By_IdBodega(int pIdBodega, SqlConnection lConnection, SqlTransaction lTransaction)
    {
        try
        {
            bool lExists = false;

            string vSQL = "SELECT COUNT(1) FROM bodega WHERE IdBodega = @IdBodega";

            using (var lCommand = new SqlCommand(vSQL, lConnection, lTransaction))
            {
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega);

                object lReturnValue = lCommand.ExecuteScalar();

                if (lReturnValue != DBNull.Value && lReturnValue != null)
                {
                    lExists = Convert.ToInt32(lReturnValue) > 0;
                }
            }

            return lExists;
        }
        catch (Exception ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex.Message);
            throw new Exception(vMsgError);
        }
    }

    public static int Get_IdEmpresa_By_IdBodega(int pIdBodega, SqlConnection lConnection, SqlTransaction lTransaction)
    {
        int result = 0;

        try
        {
            string vSQL = "SELECT IdEmpresa FROM bodega WHERE IdBodega = @IdBodega";

            using (var lCommand = new SqlCommand(vSQL, lConnection, lTransaction))
            {
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega);

                object lReturnValue = lCommand.ExecuteScalar();

                if (lReturnValue != DBNull.Value && lReturnValue != null)
                {
                    result = Convert.ToInt32(lReturnValue);
                }
            }
        }
        catch (Exception ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = null;
            if (sf != null) { currentMethodName = sf.GetMethod(); }
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex.Message);
            throw new Exception(vMsgError);
        }

        return result;
    }

    public static int Get_IdUbicacion_Recepcion_By_IdBodega(int pIdBodega,
                                                            SqlConnection lConnection,
                                                            SqlTransaction lTransaction)
    {
        try
        {
            string vSQL = "SELECT ubic_recepcion FROM bodega WHERE IdBodega = @IdBodega";

            using (SqlCommand lCommand = new SqlCommand(vSQL, lConnection, lTransaction)
            {
                CommandType = CommandType.Text
            })
            {
                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega);

                object lReturnValue = lCommand.ExecuteScalar();
                
                if (lReturnValue != DBNull.Value && lReturnValue != null)
                {
                    if (int.TryParse(lReturnValue.ToString(), out int result))
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception($"La ubicación por defecto para recepción no está definida para la bodega código: {pIdBodega}");
                    }
                }

                return 0;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}