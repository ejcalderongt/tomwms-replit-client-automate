using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Pedido;
using Microsoft.Extensions.Configuration;
using WMSWebAPI.Dtos.Salidas;
public class clsLnTrans_pe_enc
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_pe_enc oBeTrans_pe_enc, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }        
        double GetDecimal(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeTrans_pe_enc.IdPedidoEnc = GetInt("IdPedidoEnc");
            oBeTrans_pe_enc.IdBodega = GetInt("IdBodega");
            oBeTrans_pe_enc.IdCliente = GetInt("IdCliente");
            oBeTrans_pe_enc.IdMuelle = GetInt("IdMuelle");
            oBeTrans_pe_enc.IdPropietarioBodega = GetInt("IdPropietarioBodega");
            oBeTrans_pe_enc.IdTipoPedido = GetInt("IdTipoPedido");
            oBeTrans_pe_enc.IdPickingEnc = GetInt("IdPickingEnc");
            oBeTrans_pe_enc.Fecha_Pedido = GetDate("Fecha_Pedido");
            oBeTrans_pe_enc.Hora_ini = GetDate("hora_ini");
            oBeTrans_pe_enc.Hora_fin = GetDate("hora_fin");
            oBeTrans_pe_enc.Ubicacion = GetString("ubicacion");
            oBeTrans_pe_enc.Estado = GetString("estado");
            oBeTrans_pe_enc.No_despacho = GetInt("no_despacho");
            oBeTrans_pe_enc.Activo = GetBool("activo");
            oBeTrans_pe_enc.User_agr = GetString("user_agr");
            oBeTrans_pe_enc.Fec_agr = GetDate("fec_agr");
            oBeTrans_pe_enc.User_mod = GetString("user_mod");
            oBeTrans_pe_enc.Fec_mod = GetDate("fec_mod");
            oBeTrans_pe_enc.No_documento = GetInt("no_documento");
            oBeTrans_pe_enc.Local = GetBool("local");
            oBeTrans_pe_enc.Pallet_primero = GetBool("pallet_primero");
            oBeTrans_pe_enc.Dias_cliente = GetDecimal("dias_cliente");
            oBeTrans_pe_enc.Anulado = GetBool("anulado");
            oBeTrans_pe_enc.RoadKilometraje = GetDecimal("RoadKilometraje");
            oBeTrans_pe_enc.RoadFechaEntr = GetDate("RoadFechaEntr");
            oBeTrans_pe_enc.RoadDirEntrega = GetString("RoadDirEntrega");
            oBeTrans_pe_enc.RoadTotal = GetDecimal("RoadTotal");
            oBeTrans_pe_enc.RoadDesMonto = GetDecimal("RoadDesMonto");
            oBeTrans_pe_enc.RoadImpMonto = GetDecimal("RoadImpMonto");
            oBeTrans_pe_enc.RoadPeso = GetDecimal("RoadPeso");
            oBeTrans_pe_enc.RoadBandera = GetString("RoadBandera");
            oBeTrans_pe_enc.RoadStatCom = GetString("RoadStatCom");
            oBeTrans_pe_enc.RoadCalcoBJ = GetString("RoadCalcoBJ");
            oBeTrans_pe_enc.RoadImpres = GetInt("RoadImpres");
            oBeTrans_pe_enc.RoadADD1 = GetString("RoadADD1");
            oBeTrans_pe_enc.RoadADD2 = GetString("RoadADD2");
            oBeTrans_pe_enc.RoadADD3 = GetString("RoadADD3");
            oBeTrans_pe_enc.RoadStatProc = GetString("RoadStatProc");
            oBeTrans_pe_enc.RoadRechazado = GetBool("RoadRechazado");
            oBeTrans_pe_enc.RoadRazon_Rechazado = GetString("RoadRazon_Rechazado");
            oBeTrans_pe_enc.RoadInformado = GetBool("RoadInformado");
            oBeTrans_pe_enc.RoadSucursal = GetString("RoadSucursal");
            oBeTrans_pe_enc.RoadIdDespacho = GetInt("RoadIdDespacho");
            oBeTrans_pe_enc.RoadIdFacturacion = GetInt("RoadIdFacturacion");
            oBeTrans_pe_enc.RoadIdRuta = GetInt("RoadIdRuta");
            oBeTrans_pe_enc.RoadIdVendedor = GetInt("RoadIdVendedor");
            oBeTrans_pe_enc.RoadIdRutaDespacho = GetInt("RoadIdRutaDespacho");
            oBeTrans_pe_enc.RoadIdVendedorDespacho = GetInt("RoadIdVendedorDespacho");
            oBeTrans_pe_enc.Observacion = GetString("Observacion");
            oBeTrans_pe_enc.PedidoRoad = GetBool("PedidoRoad");
            oBeTrans_pe_enc.HoraEntregaDesde = GetDate("HoraEntregaDesde");
            oBeTrans_pe_enc.HoraEntregaHasta = GetDate("HoraEntregaHasta");
            oBeTrans_pe_enc.Referencia = GetString("referencia");
            oBeTrans_pe_enc.IdMotivoAnulacionBodega = GetInt("IdMotivoAnulacionBodega");
            oBeTrans_pe_enc.Enviado_A_ERP = GetBool("Enviado_A_ERP");
            oBeTrans_pe_enc.Control_ultimo_lote = GetBool("control_ultimo_lote");
            oBeTrans_pe_enc.Serie = GetString("serie");
            oBeTrans_pe_enc.Correlativo = GetInt("correlativo");
            oBeTrans_pe_enc.Referencia_Documento_Ingreso_Bodega_Destino = GetString("Referencia_Documento_Ingreso_Bodega_Destino");
            oBeTrans_pe_enc.Sync_mi3 = GetBool("sync_mi3");
            oBeTrans_pe_enc.No_Picking_ERP = GetString("No_Picking_ERP");
            oBeTrans_pe_enc.No_documento_externo = GetString("no_documento_externo");
            oBeTrans_pe_enc.Requiere_tarimas = GetBool("requiere_tarimas");
            oBeTrans_pe_enc.Fecha_preparacion = GetDate("fecha_preparacion");
            oBeTrans_pe_enc.IdTipoManufactura = GetInt("IdTipoManufactura");
            oBeTrans_pe_enc.Bodega_origen = GetString("bodega_origen");
            oBeTrans_pe_enc.Bodega_destino = GetString("bodega_destino");
            oBeTrans_pe_enc.IdMotivoDevolucion = GetInt("IdMotivoDevolucion");
            oBeTrans_pe_enc.EsExportacion = GetBool("EsExportacion");
        }
        catch (Exception)
        {                      
            throw;
        }
    }

    public static int Insertar(clsBeTrans_pe_enc oBeTrans_pe_enc, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

        try
        {
            Ins.Init("trans_pe_enc");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idcliente", "@idcliente", "F");
            Ins.Add("idmuelle", "@idmuelle", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idtipopedido", "@idtipopedido", "F");
            Ins.Add("idpickingenc", "@idpickingenc", "F");
            Ins.Add("fecha_pedido", "@fecha_pedido", "F");
            Ins.Add("hora_ini", "@hora_ini", "F");
            Ins.Add("hora_fin", "@hora_fin", "F");
            Ins.Add("ubicacion", "@ubicacion", "F");
            Ins.Add("estado", "@estado", "F");
            Ins.Add("no_despacho", "@no_despacho", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("no_documento", "@no_documento", "F");
            Ins.Add("local", "@local", "F");
            Ins.Add("pallet_primero", "@pallet_primero", "F");
            Ins.Add("dias_cliente", "@dias_cliente", "F");
            Ins.Add("anulado", "@anulado", "F");
            Ins.Add("roadkilometraje", "@roadkilometraje", "F");
            Ins.Add("roadfechaentr", "@roadfechaentr", "F");
            Ins.Add("roaddirentrega", "@roaddirentrega", "F");
            Ins.Add("roadtotal", "@roadtotal", "F");
            Ins.Add("roaddesmonto", "@roaddesmonto", "F");
            Ins.Add("roadimpmonto", "@roadimpmonto", "F");
            Ins.Add("roadpeso", "@roadpeso", "F");
            Ins.Add("roadbandera", "@roadbandera", "F");
            Ins.Add("roadstatcom", "@roadstatcom", "F");
            Ins.Add("roadcalcobj", "@roadcalcobj", "F");
            Ins.Add("roadimpres", "@roadimpres", "F");
            Ins.Add("roadadd1", "@roadadd1", "F");
            Ins.Add("roadadd2", "@roadadd2", "F");
            Ins.Add("roadadd3", "@roadadd3", "F");
            Ins.Add("roadstatproc", "@roadstatproc", "F");
            Ins.Add("roadrechazado", "@roadrechazado", "F");
            Ins.Add("roadrazon_rechazado", "@roadrazon_rechazado", "F");
            Ins.Add("roadinformado", "@roadinformado", "F");
            Ins.Add("roadsucursal", "@roadsucursal", "F");
            Ins.Add("roadiddespacho", "@roadiddespacho", "F");
            Ins.Add("roadidfacturacion", "@roadidfacturacion", "F");
            Ins.Add("roadidruta", "@roadidruta", "F");
            Ins.Add("roadidvendedor", "@roadidvendedor", "F");
            Ins.Add("roadidrutadespacho", "@roadidrutadespacho", "F");
            Ins.Add("roadidvendedordespacho", "@roadidvendedordespacho", "F");
            Ins.Add("observacion", "@observacion", "F");
            Ins.Add("pedidoroad", "@pedidoroad", "F");
            Ins.Add("horaentregadesde", "@horaentregadesde", "F");
            Ins.Add("horaentregahasta", "@horaentregahasta", "F");
            Ins.Add("referencia", "@referencia", "F");
            Ins.Add("idmotivoanulacionbodega", "@idmotivoanulacionbodega", "F");
            Ins.Add("enviado_a_erp", "@enviado_a_erp", "F");
            Ins.Add("control_ultimo_lote", "@control_ultimo_lote", "F");
            Ins.Add("serie", "@serie", "F");
            Ins.Add("correlativo", "@correlativo", "F");
            Ins.Add("referencia_documento_ingreso_bodega_destino", "@referencia_documento_ingreso_bodega_destino", "F");
            Ins.Add("sync_mi3", "@sync_mi3", "F");
            Ins.Add("no_picking_erp", "@no_picking_erp", "F");
            Ins.Add("no_documento_externo", "@no_documento_externo", "F");
            Ins.Add("requiere_tarimas", "@requiere_tarimas", "F");
            Ins.Add("fecha_preparacion", "@fecha_preparacion", "F");
            Ins.Add("idtipomanufactura", "@idtipomanufactura", "F");
            Ins.Add("bodega_origen", "@bodega_origen", "F");
            Ins.Add("bodega_destino", "@bodega_destino", "F");
            Ins.Add("idmotivodevolucion", "@idmotivodevolucion", "F");

            string sp = Ins.SQL();

            using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.Add(new SqlParameter("@IdPedidoEnc", oBeTrans_pe_enc.IdPedidoEnc));
            cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeTrans_pe_enc.IdBodega));
            cmd.Parameters.Add(new SqlParameter("@IdCliente", oBeTrans_pe_enc.IdCliente));
            cmd.Parameters.Add(new SqlParameter("@IdMuelle", oBeTrans_pe_enc.IdMuelle == 0 ? (object)DBNull.Value : oBeTrans_pe_enc.IdMuelle));
            cmd.Parameters.Add(new SqlParameter("@IdPropietarioBodega", oBeTrans_pe_enc.IdPropietarioBodega));
            cmd.Parameters.Add(new SqlParameter("@IdTipoPedido", oBeTrans_pe_enc.IdTipoPedido));
            cmd.Parameters.Add(new SqlParameter("@IdPickingEnc", oBeTrans_pe_enc.IdPickingEnc));
            cmd.Parameters.Add(new SqlParameter("@Fecha_Pedido", oBeTrans_pe_enc.Fecha_Pedido));
            cmd.Parameters.Add(new SqlParameter("@hora_ini", oBeTrans_pe_enc.Hora_ini));
            cmd.Parameters.Add(new SqlParameter("@hora_fin", oBeTrans_pe_enc.Hora_fin));
            cmd.Parameters.Add(new SqlParameter("@ubicacion", oBeTrans_pe_enc.Ubicacion ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@estado", oBeTrans_pe_enc.Estado ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@no_despacho", oBeTrans_pe_enc.No_despacho == 0 ? (object)DBNull.Value : oBeTrans_pe_enc.No_despacho));
            cmd.Parameters.Add(new SqlParameter("@activo", oBeTrans_pe_enc.Activo));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeTrans_pe_enc.User_agr ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeTrans_pe_enc.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeTrans_pe_enc.User_mod ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeTrans_pe_enc.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@no_documento", oBeTrans_pe_enc.No_documento == 0 ? (object)DBNull.Value : oBeTrans_pe_enc.No_documento));
            cmd.Parameters.Add(new SqlParameter("@local", oBeTrans_pe_enc.Local ? 1 : 0));
            cmd.Parameters.Add(new SqlParameter("@pallet_primero", oBeTrans_pe_enc.Pallet_primero));
            cmd.Parameters.Add(new SqlParameter("@dias_cliente", oBeTrans_pe_enc.Dias_cliente));
            cmd.Parameters.Add(new SqlParameter("@anulado", oBeTrans_pe_enc.Anulado));
            cmd.Parameters.Add(new SqlParameter("@RoadKilometraje", oBeTrans_pe_enc.RoadKilometraje));
            cmd.Parameters.Add(new SqlParameter("@RoadFechaEntr", oBeTrans_pe_enc.RoadFechaEntr));
            cmd.Parameters.Add(new SqlParameter("@RoadDirEntrega", oBeTrans_pe_enc.RoadDirEntrega ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@RoadTotal", oBeTrans_pe_enc.RoadTotal));
            cmd.Parameters.Add(new SqlParameter("@RoadDesMonto", oBeTrans_pe_enc.RoadDesMonto));
            cmd.Parameters.Add(new SqlParameter("@RoadImpMonto", oBeTrans_pe_enc.RoadImpMonto));
            cmd.Parameters.Add(new SqlParameter("@RoadPeso", oBeTrans_pe_enc.RoadPeso));
            cmd.Parameters.Add(new SqlParameter("@RoadBandera", oBeTrans_pe_enc.RoadBandera ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@RoadStatCom", oBeTrans_pe_enc.RoadStatCom ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@RoadCalcoBJ", oBeTrans_pe_enc.RoadCalcoBJ ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@RoadImpres", oBeTrans_pe_enc.RoadImpres == 0 ? (object)DBNull.Value : oBeTrans_pe_enc.RoadImpres));
            cmd.Parameters.Add(new SqlParameter("@RoadADD1", oBeTrans_pe_enc.RoadADD1 ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@RoadADD2", oBeTrans_pe_enc.RoadADD2 ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@RoadADD3", oBeTrans_pe_enc.RoadADD3 ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@RoadStatProc", oBeTrans_pe_enc.RoadStatProc ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@RoadRechazado", oBeTrans_pe_enc.RoadRechazado));
            cmd.Parameters.Add(new SqlParameter("@RoadRazon_Rechazado", oBeTrans_pe_enc.RoadRazon_Rechazado ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@RoadInformado", oBeTrans_pe_enc.RoadInformado));
            cmd.Parameters.Add(new SqlParameter("@RoadSucursal", oBeTrans_pe_enc.RoadSucursal ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@RoadIdDespacho", oBeTrans_pe_enc.RoadIdDespacho));
            cmd.Parameters.Add(new SqlParameter("@RoadIdFacturacion", oBeTrans_pe_enc.RoadIdFacturacion));
            cmd.Parameters.Add(new SqlParameter("@RoadIdRuta", oBeTrans_pe_enc.RoadIdRuta));
            cmd.Parameters.Add(new SqlParameter("@RoadIdVendedor", oBeTrans_pe_enc.RoadIdVendedor));
            cmd.Parameters.Add(new SqlParameter("@RoadIdRutaDespacho", oBeTrans_pe_enc.RoadIdRutaDespacho));
            cmd.Parameters.Add(new SqlParameter("@RoadIdVendedorDespacho", oBeTrans_pe_enc.RoadIdVendedorDespacho));
            cmd.Parameters.Add(new SqlParameter("@Observacion", oBeTrans_pe_enc.Observacion ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@PedidoRoad", oBeTrans_pe_enc.PedidoRoad ? 1 : 0));
            cmd.Parameters.Add(new SqlParameter("@HoraEntregaDesde", oBeTrans_pe_enc.HoraEntregaDesde));
            cmd.Parameters.Add(new SqlParameter("@HoraEntregaHasta", oBeTrans_pe_enc.HoraEntregaHasta));
            cmd.Parameters.Add(new SqlParameter("@referencia", oBeTrans_pe_enc.Referencia ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@IdMotivoAnulacionBodega", oBeTrans_pe_enc.IdMotivoAnulacionBodega));
            cmd.Parameters.Add(new SqlParameter("@Enviado_A_ERP", oBeTrans_pe_enc.Enviado_A_ERP));
            cmd.Parameters.Add(new SqlParameter("@control_ultimo_lote", oBeTrans_pe_enc.Control_ultimo_lote));
            cmd.Parameters.Add(new SqlParameter("@serie", oBeTrans_pe_enc.Serie ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@correlativo", oBeTrans_pe_enc.Correlativo == 0 ? (object)DBNull.Value : oBeTrans_pe_enc.Correlativo));
            cmd.Parameters.Add(new SqlParameter("@Referencia_Documento_Ingreso_Bodega_Destino", oBeTrans_pe_enc.Referencia_Documento_Ingreso_Bodega_Destino ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@sync_mi3", oBeTrans_pe_enc.Sync_mi3));
            cmd.Parameters.Add(new SqlParameter("@No_Picking_ERP", oBeTrans_pe_enc.No_Picking_ERP ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@no_documento_externo", oBeTrans_pe_enc.No_documento_externo ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@requiere_tarimas", oBeTrans_pe_enc.Requiere_tarimas));
            cmd.Parameters.Add(new SqlParameter("@fecha_preparacion", oBeTrans_pe_enc.Fecha_preparacion));
            cmd.Parameters.Add(new SqlParameter("@IdTipoManufactura", oBeTrans_pe_enc.IdTipoManufactura));
            cmd.Parameters.Add(new SqlParameter("@bodega_origen", oBeTrans_pe_enc.Bodega_origen ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@bodega_destino", oBeTrans_pe_enc.Bodega_destino ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@IdMotivoDevolucion", oBeTrans_pe_enc.IdMotivoDevolucion));

            rowsAffected = cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {            
            throw;
        }

        return rowsAffected;
    }

    public static int Insertar(IConfiguration config, clsBeTrans_pe_enc oBeTrans_pe_enc)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_pe_enc");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idcliente", "@idcliente", "F");
            Ins.Add("idmuelle", "@idmuelle", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idtipopedido", "@idtipopedido", "F");
            Ins.Add("idpickingenc", "@idpickingenc", "F");
            Ins.Add("fecha_pedido", "@fecha_pedido", "F");
            Ins.Add("hora_ini", "@hora_ini", "F");
            Ins.Add("hora_fin", "@hora_fin", "F");
            Ins.Add("ubicacion", "@ubicacion", "F");
            Ins.Add("estado", "@estado", "F");
            Ins.Add("no_despacho", "@no_despacho", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("no_documento", "@no_documento", "F");
            Ins.Add("local", "@local", "F");
            Ins.Add("pallet_primero", "@pallet_primero", "F");
            Ins.Add("dias_cliente", "@dias_cliente", "F");
            Ins.Add("anulado", "@anulado", "F");
            Ins.Add("roadkilometraje", "@roadkilometraje", "F");
            Ins.Add("roadfechaentr", "@roadfechaentr", "F");
            Ins.Add("roaddirentrega", "@roaddirentrega", "F");
            Ins.Add("roadtotal", "@roadtotal", "F");
            Ins.Add("roaddesmonto", "@roaddesmonto", "F");
            Ins.Add("roadimpmonto", "@roadimpmonto", "F");
            Ins.Add("roadpeso", "@roadpeso", "F");
            Ins.Add("roadbandera", "@roadbandera", "F");
            Ins.Add("roadstatcom", "@roadstatcom", "F");
            Ins.Add("roadcalcobj", "@roadcalcobj", "F");
            Ins.Add("roadimpres", "@roadimpres", "F");
            Ins.Add("roadadd1", "@roadadd1", "F");
            Ins.Add("roadadd2", "@roadadd2", "F");
            Ins.Add("roadadd3", "@roadadd3", "F");
            Ins.Add("roadstatproc", "@roadstatproc", "F");
            Ins.Add("roadrechazado", "@roadrechazado", "F");
            Ins.Add("roadrazon_rechazado", "@roadrazon_rechazado", "F");
            Ins.Add("roadinformado", "@roadinformado", "F");
            Ins.Add("roadsucursal", "@roadsucursal", "F");
            Ins.Add("roadiddespacho", "@roadiddespacho", "F");
            Ins.Add("roadidfacturacion", "@roadidfacturacion", "F");
            Ins.Add("roadidruta", "@roadidruta", "F");
            Ins.Add("roadidvendedor", "@roadidvendedor", "F");
            Ins.Add("roadidrutadespacho", "@roadidrutadespacho", "F");
            Ins.Add("roadidvendedordespacho", "@roadidvendedordespacho", "F");
            Ins.Add("observacion", "@observacion", "F");
            Ins.Add("pedidoroad", "@pedidoroad", "F");
            Ins.Add("horaentregadesde", "@horaentregadesde", "F");
            Ins.Add("horaentregahasta", "@horaentregahasta", "F");
            Ins.Add("referencia", "@referencia", "F");
            Ins.Add("idmotivoanulacionbodega", "@idmotivoanulacionbodega", "F");
            Ins.Add("enviado_a_erp", "@enviado_a_erp", "F");
            Ins.Add("control_ultimo_lote", "@control_ultimo_lote", "F");
            Ins.Add("serie", "@serie", "F");
            Ins.Add("correlativo", "@correlativo", "F");
            Ins.Add("referencia_documento_ingreso_bodega_destino", "@referencia_documento_ingreso_bodega_destino", "F");
            Ins.Add("sync_mi3", "@sync_mi3", "F");
            Ins.Add("no_picking_erp", "@no_picking_erp", "F");
            Ins.Add("no_documento_externo", "@no_documento_externo", "F");
            Ins.Add("requiere_tarimas", "@requiere_tarimas", "F");
            Ins.Add("fecha_preparacion", "@fecha_preparacion", "F");
            Ins.Add("idtipomanufactura", "@idtipomanufactura", "F");
            Ins.Add("bodega_origen", "@bodega_origen", "F");
            Ins.Add("bodega_destino", "@bodega_destino", "F");
            Ins.Add("idmotivodevolucion", "@idmotivodevolucion", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeTrans_pe_enc);

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

    public static int Actualizar(clsBeTrans_pe_enc oBeTrans_pe_enc, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

        try
        {
            Upd.Init("trans_pe_enc");
            Upd.Add("idpedidoenc", "@idpedidoenc", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("idcliente", "@idcliente", "F");
            Upd.Add("idmuelle", "@idmuelle", "F");
            Upd.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Upd.Add("idtipopedido", "@idtipopedido", "F");
            Upd.Add("idpickingenc", "@idpickingenc", "F");
            Upd.Add("fecha_pedido", "@fecha_pedido", "F");
            Upd.Add("hora_ini", "@hora_ini", "F");
            Upd.Add("hora_fin", "@hora_fin", "F");
            Upd.Add("ubicacion", "@ubicacion", "F");
            Upd.Add("estado", "@estado", "F");
            Upd.Add("no_despacho", "@no_despacho", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("no_documento", "@no_documento", "F");
            Upd.Add("local", "@local", "F");
            Upd.Add("pallet_primero", "@pallet_primero", "F");
            Upd.Add("dias_cliente", "@dias_cliente", "F");
            Upd.Add("anulado", "@anulado", "F");
            Upd.Add("roadkilometraje", "@roadkilometraje", "F");
            Upd.Add("roadfechaentr", "@roadfechaentr", "F");
            Upd.Add("roaddirentrega", "@roaddirentrega", "F");
            Upd.Add("roadtotal", "@roadtotal", "F");
            Upd.Add("roaddesmonto", "@roaddesmonto", "F");
            Upd.Add("roadimpmonto", "@roadimpmonto", "F");
            Upd.Add("roadpeso", "@roadpeso", "F");
            Upd.Add("roadbandera", "@roadbandera", "F");
            Upd.Add("roadstatcom", "@roadstatcom", "F");
            Upd.Add("roadcalcobj", "@roadcalcobj", "F");
            Upd.Add("roadimpres", "@roadimpres", "F");
            Upd.Add("roadadd1", "@roadadd1", "F");
            Upd.Add("roadadd2", "@roadadd2", "F");
            Upd.Add("roadadd3", "@roadadd3", "F");
            Upd.Add("roadstatproc", "@roadstatproc", "F");
            Upd.Add("roadrechazado", "@roadrechazado", "F");
            Upd.Add("roadrazon_rechazado", "@roadrazon_rechazado", "F");
            Upd.Add("roadinformado", "@roadinformado", "F");
            Upd.Add("roadsucursal", "@roadsucursal", "F");
            Upd.Add("roadiddespacho", "@roadiddespacho", "F");
            Upd.Add("roadidfacturacion", "@roadidfacturacion", "F");
            Upd.Add("roadidruta", "@roadidruta", "F");
            Upd.Add("roadidvendedor", "@roadidvendedor", "F");
            Upd.Add("roadidrutadespacho", "@roadidrutadespacho", "F");
            Upd.Add("roadidvendedordespacho", "@roadidvendedordespacho", "F");
            Upd.Add("observacion", "@observacion", "F");
            Upd.Add("pedidoroad", "@pedidoroad", "F");
            Upd.Add("horaentregadesde", "@horaentregadesde", "F");
            Upd.Add("horaentregahasta", "@horaentregahasta", "F");
            Upd.Add("referencia", "@referencia", "F");
            Upd.Add("idmotivoanulacionbodega", "@idmotivoanulacionbodega", "F");
            Upd.Add("enviado_a_erp", "@enviado_a_erp", "F");
            Upd.Add("control_ultimo_lote", "@control_ultimo_lote", "F");
            Upd.Add("serie", "@serie", "F");
            Upd.Add("correlativo", "@correlativo", "F");
            Upd.Add("referencia_documento_ingreso_bodega_destino", "@referencia_documento_ingreso_bodega_destino", "F");
            Upd.Add("sync_mi3", "@sync_mi3", "F");
            Upd.Add("no_picking_erp", "@no_picking_erp", "F");
            Upd.Add("no_documento_externo", "@no_documento_externo", "F");
            Upd.Add("requiere_tarimas", "@requiere_tarimas", "F");
            Upd.Add("fecha_preparacion", "@fecha_preparacion", "F");
            Upd.Add("idtipomanufactura", "@idtipomanufactura", "F");
            Upd.Add("bodega_origen", "@bodega_origen", "F");
            Upd.Add("bodega_destino", "@bodega_destino", "F");
            Upd.Add("idmotivodevolucion", "@idmotivodevolucion", "F");
            Upd.Where("IdPedidoEnc = @IdPedidoEnc");

            string sp = Upd.SQL();

            using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction)
            {
                CommandType = CommandType.Text
            };

            Bind(cmd, oBeTrans_pe_enc);

            rowsAffected = cmd.ExecuteNonQuery();
        }
        catch (Exception ex1)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{0} {1}", currentMethodName, ex1.Message);

            throw new Exception(vMsgError);
        }

        return rowsAffected;
    }

    public int Eliminar(IConfiguration config, clsBeTrans_pe_enc oBeTrans_pe_enc, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_pe_enc" +
             "  Where(IdPedidoEnc = @IdPedidoEnc)");

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

            cmd.Parameters.Add(new SqlParameter("@IdPedidoEnc", oBeTrans_pe_enc.IdPedidoEnc));

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
            const string sp = "Select * FROM Trans_pe_enc";
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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_pe_enc pBeTrans_pe_enc)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_pe_enc" +
            " Where(IdPedidoEnc = @IdPedidoEnc)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPedidoEnc", pBeTrans_pe_enc.IdPedidoEnc));
            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_pe_enc, r);
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

    public static List<clsBeTrans_pe_enc> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_pe_enc> lreturnList = new List<clsBeTrans_pe_enc>();

        try
        {
            const string sp = "Select * FROM Trans_pe_enc";

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

                        clsBeTrans_pe_enc vBeTrans_pe_enc = new clsBeTrans_pe_enc();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_pe_enc = new clsBeTrans_pe_enc();
                            Cargar(ref vBeTrans_pe_enc, dr);
                            lreturnList.Add(vBeTrans_pe_enc);
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

            const string sp = "Select ISNULL(Max(IdPedidoEnc),0) FROM Trans_pe_enc";

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
    public static int MaxID(SqlConnection pConection, SqlTransaction pTransaction)
    {
        int lMax = 0;

        try
        {
            const string sp = "SELECT ISNULL(Max(IdPedidoEnc),0) FROM Trans_pe_enc";

            using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction)
            {
                CommandType = CommandType.Text
            };

            object lreturnValue = cmd.ExecuteScalar();

            if (lreturnValue != DBNull.Value && lreturnValue != null)
            {
                lMax = Convert.ToInt32(lreturnValue);
            }

            return lMax;
        }
        catch (Exception)
        {           
            throw;
        }
    }
    public static void Bind(SqlCommand cmd, clsBeTrans_pe_enc o)
    {
        cmd.Parameters.Add(new SqlParameter("@IdPedidoEnc", o.IdPedidoEnc != 0 ? o.IdPedidoEnc : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdBodega", o.IdBodega != 0 ? o.IdBodega : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdCliente", o.IdCliente != 0 ? o.IdCliente : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdMuelle", o.IdMuelle != 0 ? o.IdMuelle : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPropietarioBodega", o.IdPropietarioBodega != 0 ? o.IdPropietarioBodega : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdTipoPedido", o.IdTipoPedido != 0 ? o.IdTipoPedido : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPickingEnc", o.IdPickingEnc != 0 ? o.IdPickingEnc : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Fecha_Pedido", o.Fecha_Pedido != DateTime.MinValue ? o.Fecha_Pedido : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@hora_ini", o.Hora_ini != DateTime.MinValue ? o.Hora_ini : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@hora_fin", o.Hora_fin != DateTime.MinValue ? o.Hora_fin : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@ubicacion", !string.IsNullOrWhiteSpace(o.Ubicacion) ? o.Ubicacion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@estado", !string.IsNullOrWhiteSpace(o.Estado) ? o.Estado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@no_despacho", o.No_despacho != 0 ? o.No_despacho : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@activo", o.Activo));
        cmd.Parameters.Add(new SqlParameter("@user_agr", !string.IsNullOrWhiteSpace(o.User_agr) ? o.User_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", o.Fec_agr != DateTime.MinValue ? o.Fec_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_mod", !string.IsNullOrWhiteSpace(o.User_mod) ? o.User_mod : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", o.Fec_mod != DateTime.MinValue ? o.Fec_mod : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@no_documento", o.No_documento != 0 ? o.No_documento : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@local", o.Local));
        cmd.Parameters.Add(new SqlParameter("@pallet_primero", o.Pallet_primero));
        cmd.Parameters.Add(new SqlParameter("@dias_cliente", o.Dias_cliente != 0 ? o.Dias_cliente : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@anulado", o.Anulado));
        cmd.Parameters.Add(new SqlParameter("@RoadKilometraje", o.RoadKilometraje != 0 ? o.RoadKilometraje : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadFechaEntr", o.RoadFechaEntr != DateTime.MinValue ? o.RoadFechaEntr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadDirEntrega", !string.IsNullOrWhiteSpace(o.RoadDirEntrega) ? o.RoadDirEntrega : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadTotal", o.RoadTotal != 0 ? o.RoadTotal : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadDesMonto", o.RoadDesMonto != 0 ? o.RoadDesMonto : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadImpMonto", o.RoadImpMonto != 0 ? o.RoadImpMonto : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadPeso", o.RoadPeso != 0 ? o.RoadPeso : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadBandera", o.RoadBandera));
        cmd.Parameters.Add(new SqlParameter("@RoadStatCom", o.RoadStatCom));
        cmd.Parameters.Add(new SqlParameter("@RoadCalcoBJ", o.RoadCalcoBJ));
        cmd.Parameters.Add(new SqlParameter("@RoadImpres", o.RoadImpres));
        cmd.Parameters.Add(new SqlParameter("@RoadADD1", !string.IsNullOrWhiteSpace(o.RoadADD1) ? o.RoadADD1 : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadADD2", !string.IsNullOrWhiteSpace(o.RoadADD2) ? o.RoadADD2 : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadADD3", !string.IsNullOrWhiteSpace(o.RoadADD3) ? o.RoadADD3 : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadStatProc", o.RoadStatProc));
        cmd.Parameters.Add(new SqlParameter("@RoadRechazado", o.RoadRechazado));
        cmd.Parameters.Add(new SqlParameter("@RoadRazon_Rechazado", !string.IsNullOrWhiteSpace(o.RoadRazon_Rechazado) ? o.RoadRazon_Rechazado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadInformado", o.RoadInformado));
        cmd.Parameters.Add(new SqlParameter("@RoadSucursal", !string.IsNullOrWhiteSpace(o.RoadSucursal) ? o.RoadSucursal : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadIdDespacho", o.RoadIdDespacho != 0 ? o.RoadIdDespacho : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadIdFacturacion", o.RoadIdFacturacion != 0 ? o.RoadIdFacturacion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadIdRuta", o.RoadIdRuta != 0 ? o.RoadIdRuta : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadIdVendedor", o.RoadIdVendedor != 0 ? o.RoadIdVendedor : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadIdRutaDespacho", o.RoadIdRutaDespacho != 0 ? o.RoadIdRutaDespacho : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadIdVendedorDespacho", o.RoadIdVendedorDespacho != 0 ? o.RoadIdVendedorDespacho : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Observacion", !string.IsNullOrWhiteSpace(o.Observacion) ? o.Observacion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@PedidoRoad", o.PedidoRoad));
        cmd.Parameters.Add(new SqlParameter("@HoraEntregaDesde", o.HoraEntregaDesde != DateTime.MinValue ? o.HoraEntregaDesde : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@HoraEntregaHasta", o.HoraEntregaHasta != DateTime.MinValue ? o.HoraEntregaHasta : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@referencia", !string.IsNullOrWhiteSpace(o.Referencia) ? o.Referencia : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdMotivoAnulacionBodega", o.IdMotivoAnulacionBodega != 0 ? o.IdMotivoAnulacionBodega : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Enviado_A_ERP", o.Enviado_A_ERP));
        cmd.Parameters.Add(new SqlParameter("@control_ultimo_lote", o.Control_ultimo_lote));
        cmd.Parameters.Add(new SqlParameter("@serie", !string.IsNullOrWhiteSpace(o.Serie) ? o.Serie : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@correlativo", o.Correlativo != 0 ? o.Correlativo : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Referencia_Documento_Ingreso_Bodega_Destino", !string.IsNullOrWhiteSpace(o.Referencia_Documento_Ingreso_Bodega_Destino) ? o.Referencia_Documento_Ingreso_Bodega_Destino : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@sync_mi3", o.Sync_mi3));
        cmd.Parameters.Add(new SqlParameter("@No_Picking_ERP", !string.IsNullOrWhiteSpace(o.No_Picking_ERP) ? o.No_Picking_ERP : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@no_documento_externo", !string.IsNullOrWhiteSpace(o.No_documento_externo) ? o.No_documento_externo : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@requiere_tarimas", o.Requiere_tarimas));
        cmd.Parameters.Add(new SqlParameter("@fecha_preparacion", o.Fecha_preparacion != DateTime.MinValue ? o.Fecha_preparacion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdTipoManufactura", o.IdTipoManufactura != 0 ? o.IdTipoManufactura : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@bodega_origen", !string.IsNullOrWhiteSpace(o.Bodega_origen) ? o.Bodega_origen : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@bodega_destino", !string.IsNullOrWhiteSpace(o.Bodega_destino) ? o.Bodega_destino : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdMotivoDevolucion", o.IdMotivoDevolucion != 0 ? o.IdMotivoDevolucion : DBNull.Value));
    }

    public static int InsertOrUpdate(clsBeTrans_pe_enc entity, SqlConnection conn, SqlTransaction tx)
    {
        try
        {
            if (Existe(entity.IdPedidoEnc, conn, tx))
                return Actualizar(entity, conn, tx);
            else
                return Insertar(entity, conn, tx);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public static bool Existe(int idPedidoEnc, SqlConnection conn, SqlTransaction? tx = null)
    {
        const string sql = "SELECT COUNT(1) FROM trans_pe_enc WHERE IdPedidoEnc = @IdPedidoEnc";

        using SqlCommand cmd = new(sql, conn, tx);
        cmd.Parameters.AddWithValue("@IdPedidoEnc", idPedidoEnc);
        int count = Convert.ToInt32(cmd.ExecuteScalar());

        return count > 0;
    }

    public static List<PedidoSalidaDto> GetAllPedidosSalida(IConfiguration config, bool activo, DateTime fechaDel, DateTime fechaAl, int idBodega, int idPropietario)
    {
        var lista = new List<PedidoSalidaDto>();

        try
        {
            var query = new StringBuilder("SELECT * FROM VW_PEDIDOS_LIST WHERE 1=1");

            query.Append(activo ? " AND Activo = 1" : " AND Activo = 0");
            query.Append(" AND CAST(Fecha_Pedido AS DATE) BETWEEN @FechaDel AND @FechaAl");

            if (idBodega != 0)
                query.Append(" AND IdBodega = @IdBodega");

            if (idPropietario != 0)
                query.Append(" AND IdPropietario = @IdPropietario");

            using var conn = new SqlConnection(config.GetConnectionString("CST"));
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.Transaction = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query.ToString();

            cmd.Parameters.AddWithValue("@FechaDel", fechaDel.Date);
            cmd.Parameters.AddWithValue("@FechaAl", fechaAl.Date);
            if (idBodega != 0)
                cmd.Parameters.AddWithValue("@IdBodega", idBodega);
            if (idPropietario != 0)
                cmd.Parameters.AddWithValue("@IdPropietario", idPropietario);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var dto = new PedidoSalidaDto
                {
                    Correlativo = reader["Correlativo"] as int? ?? 0,
                    IdBodega = reader["IdBodega"] as int? ?? 0,
                    NoDocumento = reader["no_documento"] as long? ?? 0,
                    Referencia = reader["referencia"]?.ToString() ?? string.Empty,
                    Referencia2 = reader["Referencia2"]?.ToString() ?? string.Empty,
                    FechaPedido = reader["Fecha_Pedido"] as DateTime? ?? DateTime.MinValue,
                    Cliente = reader["Cliente"]?.ToString() ?? string.Empty,
                    Estado = reader["Estado"]?.ToString() ?? string.Empty,
                    Bodega = reader["Bodega"]?.ToString() ?? string.Empty,
                    Muelle = reader["Muelle"]?.ToString() ?? string.Empty,
                    Propietario = reader["Propietario"]?.ToString() ?? string.Empty,
                    RoadVendedor = reader["RoadVendedor"]?.ToString() ?? string.Empty,
                    RoadRuta = reader["RoadRuta"]?.ToString() ?? string.Empty,
                    Fecha = reader["Fecha"] as DateTime? ?? DateTime.MinValue,
                    Anulado = reader["anulado"] as bool? ?? false,
                    Activo = reader["activo"] as bool? ?? false,
                    EnviadoAErp = reader["Enviado_A_ERP"] as bool? ?? false,
                    FecAgr = reader["fec_agr"] as DateTime? ?? DateTime.MinValue,
                    IdPickingEnc = reader["IdPickingEnc"] as int? ?? 0,
                    TipoDocumento = reader["TipoDocumento"]?.ToString() ?? string.Empty,
                    IdDespachoEnc = reader["IdDespachoEnc"] as long? ?? 0,
                    Observacion = reader["Observacion"]?.ToString() ?? string.Empty,
                    RutaDespacho = reader["RutaDespacho"]?.ToString() ?? string.Empty,
                    NoPickingErp = reader["No_Picking_ERP"]?.ToString() ?? string.Empty,
                    NoDocumentoExterno = reader["no_documento_externo"]?.ToString() ?? string.Empty,
                    BodegaOrigen = reader["bodega_origen"]?.ToString() ?? string.Empty,
                    BodegaDestino = reader["bodega_destino"]?.ToString() ?? string.Empty,
                    IdPrioridadPicking = reader["IdPrioridadPicking"] as int? ?? 0
                };

                lista.Add(dto);
            }

            reader.Close();
            cmd.Transaction.Commit();
            return lista;
        }
        catch (Exception ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{0} {1}", currentMethodName?.Name ?? "UnknownMethod", ex.Message);
            throw new Exception(vMsgError, ex);
        }
    }

    public static clsBeTrans_pe_enc? Get_Single_By_Referencia(clsBeTrans_pe_enc pBeTrans_pe_enc,
                                                             SqlConnection pConection,
                                                             SqlTransaction pTransaction)
    {
        try
        {
            const string sp = @"SELECT * FROM Trans_pe_enc 
                           Where(Referencia = @Referencia AND IdTipoPedido = @IdTipoPedido)";

            SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@Referencia", pBeTrans_pe_enc.Referencia));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTipoPedido", pBeTrans_pe_enc.IdTipoPedido));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            if (dt.Rows.Count >= 1)
            {
                clsBeTrans_pe_enc ObjUM = new clsBeTrans_pe_enc();
                Cargar(ref ObjUM, dt.Rows[0]);
                return ObjUM;
            }

            return null;
        }
        catch (Exception)
        {         
            throw;
        }
    }

    public static clsBeTrans_pe_enc? Get_Single_By_Referencia_And_Company(ref clsBeTrans_pe_enc pBeTrans_pe_enc,
                                                                        SqlConnection pConection,
                                                                        SqlTransaction pTransaction)
    {
        clsBeTrans_pe_enc? result = null;

        try
        {
            const string sp = "SELECT * FROM Trans_pe_enc " +
                             " WHERE (Referencia = @Referencia AND IdTipoPedido = @IdTipoPedido AND Codigo_Empresa_ERP = @Codigo_Empresa_ERP) ";

            using (SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
            {
                using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                {
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@Referencia", pBeTrans_pe_enc.Referencia));
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTipoPedido", pBeTrans_pe_enc.IdTipoPedido));
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@Codigo_Empresa_ERP", pBeTrans_pe_enc.Codigo_Empresa_ERP));

                    DataTable dt = new DataTable();
                    dad.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        clsBeTrans_pe_enc ObjUM = new clsBeTrans_pe_enc();
                        Cargar(ref ObjUM, dt.Rows[0]);
                        return ObjUM;
                    }
                }
            }
        }
        catch (Exception)
        {            
            throw;
        }

        return result;
    }

    public static bool Inserta_Encabezado(ref clsBeTrans_pe_enc pPedido,
                                          SqlConnection lConnection,
                                          SqlTransaction lTransaction)
    {
        bool result = false;

        try
        {
            pPedido.IdPedidoEnc = MaxID(lConnection, lTransaction)+1;
            string correlativo = pPedido.IdPedidoEnc.ToString().PadLeft(7, '0');
            pPedido.No_documento = int.Parse(correlativo);
            int ResultadoInsert = Insertar(pPedido, lConnection, lTransaction);
            result = ResultadoInsert > 0;
        }
        catch (Exception)
        {
            throw;
        }

        return result;
    }

    public static bool Tiene_Detalle(int IdPedidoEnc, SqlConnection lConnection, SqlTransaction lTransaction)
    {
        try
        {
            string vSQL = "SELECT TOP(1) IdPedidoDet FROM trans_pe_det WHERE IdPedidoEnc = @IdPedidoEnc";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                return lDT.Rows.Count > 0;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static int Eliminar_Encabezado_Pedido(int IdPedidoEnc,
                                                SqlConnection lConnection,
                                                SqlTransaction lTransaction)
    {
        try
        {
            string sp = "DELETE FROM Trans_pe_enc WHERE (IdPedidoEnc = @IdPedidoEnc)";

            using SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.Add(new SqlParameter("@IDPEDIDOENC", IdPedidoEnc));

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static Dictionary<int, string> Get_Codigos_Cliente_By_IdsPedidoEnc(
    IConfiguration configuration,
    List<int> idsPedidoEnc)
    {
        var result = new Dictionary<int, string>();

        if (idsPedidoEnc == null || idsPedidoEnc.Count == 0)
            return result;

        var ids = idsPedidoEnc.Where(x => x > 0)
                              .Distinct()
                              .ToList();

        if (ids.Count == 0)
            return result;

        using var lConnection = new SqlConnection(configuration.GetConnectionString("CST"));
        lConnection.Open();

        using var cmd = lConnection.CreateCommand();
        cmd.CommandType = CommandType.Text;

        var paramNames = new List<string>();
        for (int i = 0; i < ids.Count; i++)
        {
            var paramName = $"@id{i}";
            paramNames.Add(paramName);
            cmd.Parameters.Add(paramName, SqlDbType.Int).Value = ids[i];
        }

        cmd.CommandText = $@"
        SELECT pe.IdPedidoEnc, c.Codigo
        FROM trans_pe_enc pe
        INNER JOIN cliente c
            ON c.IdCliente = pe.IdCliente
        WHERE pe.IdPedidoEnc IN ({string.Join(",", paramNames)})";

        using var dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            var idPedidoEnc = dr["IdPedidoEnc"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdPedidoEnc"]);
            var codigo = dr["Codigo"] == DBNull.Value ? "" : dr["Codigo"].ToString() ?? "";

            if (idPedidoEnc > 0 && !result.ContainsKey(idPedidoEnc))
                result.Add(idPedidoEnc, codigo);
        }

        return result;
    }
}