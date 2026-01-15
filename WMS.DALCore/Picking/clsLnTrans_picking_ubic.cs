using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using WMS.EntityCore.Picking;
using Microsoft.Extensions.Configuration;
using WMSWebAPI.Be;
using AppGlobal;
public class clsLnTrans_picking_ubic
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_picking_ubic oBeTrans_picking_ubic, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }        
        double GetDecimal(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeTrans_picking_ubic.IdPickingUbic = GetInt("IdPickingUbic");
            oBeTrans_picking_ubic.IdPickingEnc = GetInt("IdPickingEnc");
            oBeTrans_picking_ubic.IdPickingDet = GetInt("IdPickingDet");
            oBeTrans_picking_ubic.IdUbicacion = GetInt("IdUbicacion");
            oBeTrans_picking_ubic.IdStock = GetInt("IdStock");
            oBeTrans_picking_ubic.IdPropietarioBodega = GetInt("IdPropietarioBodega");
            oBeTrans_picking_ubic.IdProductoBodega = GetInt("IdProductoBodega");
            oBeTrans_picking_ubic.IdProductoEstado = GetInt("IdProductoEstado");
            oBeTrans_picking_ubic.IdPresentacion = GetInt("IdPresentacion");
            oBeTrans_picking_ubic.IdUnidadMedida = GetInt("IdUnidadMedida");
            oBeTrans_picking_ubic.IdUbicacionAnterior = GetInt("IdUbicacionAnterior");
            oBeTrans_picking_ubic.IdRecepcion = GetInt("IdRecepcion");
            oBeTrans_picking_ubic.Lote = GetString("lote");
            oBeTrans_picking_ubic.Fecha_vence = GetDate("fecha_vence");
            oBeTrans_picking_ubic.Fecha_minima = GetDate("fecha_minima");
            oBeTrans_picking_ubic.Serial = GetString("serial");
            oBeTrans_picking_ubic.Lic_plate = GetString("lic_plate");
            oBeTrans_picking_ubic.Acepto = GetBool("acepto");
            oBeTrans_picking_ubic.Peso_solicitado = GetDecimal("peso_solicitado");
            oBeTrans_picking_ubic.Peso_recibido = GetDecimal("peso_recibido");
            oBeTrans_picking_ubic.Peso_verificado = GetDecimal("peso_verificado");
            oBeTrans_picking_ubic.Peso_despachado = GetDecimal("peso_despachado");
            oBeTrans_picking_ubic.Cantidad_solicitada = GetDecimal("cantidad_solicitada");
            oBeTrans_picking_ubic.Cantidad_recibida = GetDecimal("cantidad_recibida");
            oBeTrans_picking_ubic.Cantidad_verificada = GetDecimal("cantidad_verificada");
            oBeTrans_picking_ubic.Encontrado = GetBool("encontrado");
            oBeTrans_picking_ubic.Dañado_verificacion = GetBool("dañado_verificacion");
            oBeTrans_picking_ubic.Fecha_real_vence = GetDate("fecha_real_vence");
            oBeTrans_picking_ubic.No_packing = GetString("no_packing");
            oBeTrans_picking_ubic.Fecha_picking = GetDate("fecha_picking");
            oBeTrans_picking_ubic.Fecha_verificado = GetDate("fecha_verificado");
            oBeTrans_picking_ubic.Fecha_packing = GetDate("fecha_packing");
            oBeTrans_picking_ubic.Fecha_despachado = GetDate("fecha_despachado");
            oBeTrans_picking_ubic.Cantidad_despachada = GetDecimal("cantidad_despachada");
            oBeTrans_picking_ubic.User_agr = GetString("user_agr");
            oBeTrans_picking_ubic.Fec_agr = GetDate("fec_agr");
            oBeTrans_picking_ubic.User_mod = GetString("user_mod");
            oBeTrans_picking_ubic.Fec_mod = GetDate("fec_mod");
            oBeTrans_picking_ubic.Activo = GetBool("activo");
            oBeTrans_picking_ubic.IdPedidoDet = GetInt("IdPedidoDet");
            oBeTrans_picking_ubic.Dañado_picking = GetBool("dañado_picking");
            oBeTrans_picking_ubic.IdStockRes = GetInt("IdStockRes");
            oBeTrans_picking_ubic.Lic_plate_reemplazo = GetString("lic_plate_reemplazo");
            oBeTrans_picking_ubic.IdUbicacion_reemplazo = GetInt("IdUbicacion_reemplazo");
            oBeTrans_picking_ubic.IdStock_reemplazo = GetInt("IdStock_reemplazo");
            oBeTrans_picking_ubic.IdBodega = GetInt("IdBodega");
            oBeTrans_picking_ubic.IdOperadorBodega_Pickeo = GetInt("IdOperadorBodega_Pickeo");
            oBeTrans_picking_ubic.IdOperadorBodega_Verifico = GetInt("IdOperadorBodega_Verifico");
            oBeTrans_picking_ubic.IdPedidoEnc = GetInt("IdPedidoEnc");
            oBeTrans_picking_ubic.No_encontrado = GetBool("no_encontrado");
            oBeTrans_picking_ubic.IdUbicacionTemporal = GetInt("IdUbicacionTemporal");
            oBeTrans_picking_ubic.IdOperadorBodega_Asignado = GetInt("IdOperadorBodega_Asignado");
        }
        catch (Exception)
        {            
            throw;
        }
    }

    public static int Insertar(clsBeTrans_picking_ubic oBeTrans_picking_ubic, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

        try
        {
            Ins.Init("trans_picking_ubic");
            Ins.Add("idpickingubic", "@idpickingubic", "F");
            Ins.Add("idpickingenc", "@idpickingenc", "F");
            Ins.Add("idpickingdet", "@idpickingdet", "F");
            Ins.Add("idubicacion", "@idubicacion", "F");
            Ins.Add("idstock", "@idstock", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idproductoestado", "@idproductoestado", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idunidadmedida", "@idunidadmedida", "F");
            Ins.Add("idubicacionanterior", "@idubicacionanterior", "F");
            Ins.Add("idrecepcion", "@idrecepcion", "F");
            Ins.Add("lote", "@lote", "F");
            Ins.Add("fecha_vence", "@fecha_vence", "F");
            Ins.Add("fecha_minima", "@fecha_minima", "F");
            Ins.Add("serial", "@serial", "F");
            Ins.Add("lic_plate", "@lic_plate", "F");
            Ins.Add("acepto", "@acepto", "F");
            Ins.Add("peso_solicitado", "@peso_solicitado", "F");
            Ins.Add("peso_recibido", "@peso_recibido", "F");
            Ins.Add("peso_verificado", "@peso_verificado", "F");
            Ins.Add("peso_despachado", "@peso_despachado", "F");
            Ins.Add("cantidad_solicitada", "@cantidad_solicitada", "F");
            Ins.Add("cantidad_recibida", "@cantidad_recibida", "F");
            Ins.Add("cantidad_verificada", "@cantidad_verificada", "F");
            Ins.Add("encontrado", "@encontrado", "F");
            Ins.Add("dañado_verificacion", "@dañado_verificacion", "F");
            Ins.Add("fecha_real_vence", "@fecha_real_vence", "F");
            Ins.Add("no_packing", "@no_packing", "F");
            Ins.Add("fecha_picking", "@fecha_picking", "F");
            Ins.Add("fecha_verificado", "@fecha_verificado", "F");
            Ins.Add("fecha_packing", "@fecha_packing", "F");
            Ins.Add("fecha_despachado", "@fecha_despachado", "F");
            Ins.Add("cantidad_despachada", "@cantidad_despachada", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("idpedidodet", "@idpedidodet", "F");
            Ins.Add("dañado_picking", "@dañado_picking", "F");
            Ins.Add("idstockres", "@idstockres", "F");
            Ins.Add("lic_plate_reemplazo", "@lic_plate_reemplazo", "F");
            Ins.Add("idubicacion_reemplazo", "@idubicacion_reemplazo", "F");
            Ins.Add("idstock_reemplazo", "@idstock_reemplazo", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idoperadorbodega_pickeo", "@idoperadorbodega_pickeo", "F");
            Ins.Add("idoperadorbodega_verifico", "@idoperadorbodega_verifico", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("no_encontrado", "@no_encontrado", "F");
            Ins.Add("idubicaciontemporal", "@idubicaciontemporal", "F");
            Ins.Add("idoperadorbodega_asignado", "@idoperadorbodega_asignado", "F");

            string sp = Ins.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
            {
                Bind(cmd, oBeTrans_picking_ubic);
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }
        catch (SqlException)
        {
            throw;
        }

        return rowsAffected;
    }

    public static int Insertar_3pl(clsBeTrans_picking_ubic_3pl oBeTrans_picking_ubic, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

        try
        {
            Ins.Init("trans_picking_ubic");
            Ins.Add("idpickingubic", "@idpickingubic", "F");
            Ins.Add("idpickingenc", "@idpickingenc", "F");
            Ins.Add("idpickingdet", "@idpickingdet", "F");
            Ins.Add("idubicacion", "@idubicacion", "F");
            Ins.Add("idstock", "@idstock", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idproductoestado", "@idproductoestado", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idunidadmedida", "@idunidadmedida", "F");
            Ins.Add("idubicacionanterior", "@idubicacionanterior", "F");
            Ins.Add("idrecepcion", "@idrecepcion", "F");
            Ins.Add("lote", "@lote", "F");
            Ins.Add("fecha_vence", "@fecha_vence", "F");
            Ins.Add("fecha_minima", "@fecha_minima", "F");
            Ins.Add("serial", "@serial", "F");
            Ins.Add("lic_plate", "@lic_plate", "F");
            Ins.Add("acepto", "@acepto", "F");
            Ins.Add("peso_solicitado", "@peso_solicitado", "F");
            Ins.Add("peso_recibido", "@peso_recibido", "F");
            Ins.Add("peso_verificado", "@peso_verificado", "F");
            Ins.Add("peso_despachado", "@peso_despachado", "F");
            Ins.Add("cantidad_solicitada", "@cantidad_solicitada", "F");
            Ins.Add("cantidad_recibida", "@cantidad_recibida", "F");
            Ins.Add("cantidad_verificada", "@cantidad_verificada", "F");
            Ins.Add("encontrado", "@encontrado", "F");
            Ins.Add("dañado_verificacion", "@dañado_verificacion", "F");
            Ins.Add("fecha_real_vence", "@fecha_real_vence", "F");
            Ins.Add("no_packing", "@no_packing", "F");
            Ins.Add("fecha_picking", "@fecha_picking", "F");
            Ins.Add("fecha_verificado", "@fecha_verificado", "F");
            Ins.Add("fecha_packing", "@fecha_packing", "F");
            Ins.Add("fecha_despachado", "@fecha_despachado", "F");
            Ins.Add("cantidad_despachada", "@cantidad_despachada", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("idpedidodet", "@idpedidodet", "F");
            Ins.Add("dañado_picking", "@dañado_picking", "F");
            Ins.Add("idstockres", "@idstockres", "F");
            Ins.Add("lic_plate_reemplazo", "@lic_plate_reemplazo", "F");
            Ins.Add("idubicacion_reemplazo", "@idubicacion_reemplazo", "F");
            Ins.Add("idstock_reemplazo", "@idstock_reemplazo", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idoperadorbodega_pickeo", "@idoperadorbodega_pickeo", "F");
            Ins.Add("idoperadorbodega_verifico", "@idoperadorbodega_verifico", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("no_encontrado", "@no_encontrado", "F");
            Ins.Add("idubicaciontemporal", "@idubicaciontemporal", "F");
            Ins.Add("idoperadorbodega_asignado", "@idoperadorbodega_asignado", "F");

            string sp = Ins.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
            {
                Bind_3pl(cmd, oBeTrans_picking_ubic);
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }
        catch (SqlException)
        {
            throw;
        }

        return rowsAffected;
    }
    public static int Insertar(IConfiguration config, clsBeTrans_picking_ubic oBeTrans_picking_ubic)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_picking_ubic");
            Ins.Add("idpickingubic", "@idpickingubic", "F");
            Ins.Add("idpickingenc", "@idpickingenc", "F");
            Ins.Add("idpickingdet", "@idpickingdet", "F");
            Ins.Add("idubicacion", "@idubicacion", "F");
            Ins.Add("idstock", "@idstock", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idproductoestado", "@idproductoestado", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idunidadmedida", "@idunidadmedida", "F");
            Ins.Add("idubicacionanterior", "@idubicacionanterior", "F");
            Ins.Add("idrecepcion", "@idrecepcion", "F");
            Ins.Add("lote", "@lote", "F");
            Ins.Add("fecha_vence", "@fecha_vence", "F");
            Ins.Add("fecha_minima", "@fecha_minima", "F");
            Ins.Add("serial", "@serial", "F");
            Ins.Add("lic_plate", "@lic_plate", "F");
            Ins.Add("acepto", "@acepto", "F");
            Ins.Add("peso_solicitado", "@peso_solicitado", "F");
            Ins.Add("peso_recibido", "@peso_recibido", "F");
            Ins.Add("peso_verificado", "@peso_verificado", "F");
            Ins.Add("peso_despachado", "@peso_despachado", "F");
            Ins.Add("cantidad_solicitada", "@cantidad_solicitada", "F");
            Ins.Add("cantidad_recibida", "@cantidad_recibida", "F");
            Ins.Add("cantidad_verificada", "@cantidad_verificada", "F");
            Ins.Add("encontrado", "@encontrado", "F");
            Ins.Add("dañado_verificacion", "@dañado_verificacion", "F");
            Ins.Add("fecha_real_vence", "@fecha_real_vence", "F");
            Ins.Add("no_packing", "@no_packing", "F");
            Ins.Add("fecha_picking", "@fecha_picking", "F");
            Ins.Add("fecha_verificado", "@fecha_verificado", "F");
            Ins.Add("fecha_packing", "@fecha_packing", "F");
            Ins.Add("fecha_despachado", "@fecha_despachado", "F");
            Ins.Add("cantidad_despachada", "@cantidad_despachada", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("idpedidodet", "@idpedidodet", "F");
            Ins.Add("dañado_picking", "@dañado_picking", "F");
            Ins.Add("idstockres", "@idstockres", "F");
            Ins.Add("lic_plate_reemplazo", "@lic_plate_reemplazo", "F");
            Ins.Add("idubicacion_reemplazo", "@idubicacion_reemplazo", "F");
            Ins.Add("idstock_reemplazo", "@idstock_reemplazo", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("idoperadorbodega_pickeo", "@idoperadorbodega_pickeo", "F");
            Ins.Add("idoperadorbodega_verifico", "@idoperadorbodega_verifico", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("no_encontrado", "@no_encontrado", "F");
            Ins.Add("idubicaciontemporal", "@idubicaciontemporal", "F");
            Ins.Add("idoperadorbodega_asignado", "@idoperadorbodega_asignado", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeTrans_picking_ubic);

            rowsAffected = cmd.ExecuteNonQuery();

            if (lTransaction != null)
                lTransaction.Commit();

        }
        catch (SqlException)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();            
            throw;
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
        return rowsAffected;
    }

    public static int Actualizar(clsBeTrans_picking_ubic oBeTrans_picking_ubic, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

        try
        {
            Upd.Init("trans_picking_ubic");
            Upd.Add("idpickingubic", "@idpickingubic", "F");
            Upd.Add("idpickingenc", "@idpickingenc", "F");
            Upd.Add("idpickingdet", "@idpickingdet", "F");
            Upd.Add("idubicacion", "@idubicacion", "F");
            Upd.Add("idstock", "@idstock", "F");
            Upd.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Upd.Add("idproductobodega", "@idproductobodega", "F");
            Upd.Add("idproductoestado", "@idproductoestado", "F");
            Upd.Add("idpresentacion", "@idpresentacion", "F");
            Upd.Add("idunidadmedida", "@idunidadmedida", "F");
            Upd.Add("idubicacionanterior", "@idubicacionanterior", "F");
            Upd.Add("idrecepcion", "@idrecepcion", "F");
            Upd.Add("lote", "@lote", "F");
            Upd.Add("fecha_vence", "@fecha_vence", "F");
            Upd.Add("fecha_minima", "@fecha_minima", "F");
            Upd.Add("serial", "@serial", "F");
            Upd.Add("lic_plate", "@lic_plate", "F");
            Upd.Add("acepto", "@acepto", "F");
            Upd.Add("peso_solicitado", "@peso_solicitado", "F");
            Upd.Add("peso_recibido", "@peso_recibido", "F");
            Upd.Add("peso_verificado", "@peso_verificado", "F");
            Upd.Add("peso_despachado", "@peso_despachado", "F");
            Upd.Add("cantidad_solicitada", "@cantidad_solicitada", "F");
            Upd.Add("cantidad_recibida", "@cantidad_recibida", "F");
            Upd.Add("cantidad_verificada", "@cantidad_verificada", "F");
            Upd.Add("encontrado", "@encontrado", "F");
            Upd.Add("dañado_verificacion", "@dañado_verificacion", "F");
            Upd.Add("fecha_real_vence", "@fecha_real_vence", "F");
            Upd.Add("no_packing", "@no_packing", "F");
            Upd.Add("fecha_picking", "@fecha_picking", "F");
            Upd.Add("fecha_verificado", "@fecha_verificado", "F");
            Upd.Add("fecha_packing", "@fecha_packing", "F");
            Upd.Add("fecha_despachado", "@fecha_despachado", "F");
            Upd.Add("cantidad_despachada", "@cantidad_despachada", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("idpedidodet", "@idpedidodet", "F");
            Upd.Add("dañado_picking", "@dañado_picking", "F");
            Upd.Add("idstockres", "@idstockres", "F");
            Upd.Add("lic_plate_reemplazo", "@lic_plate_reemplazo", "F");
            Upd.Add("idubicacion_reemplazo", "@idubicacion_reemplazo", "F");
            Upd.Add("idstock_reemplazo", "@idstock_reemplazo", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("idoperadorbodega_pickeo", "@idoperadorbodega_pickeo", "F");
            Upd.Add("idoperadorbodega_verifico", "@idoperadorbodega_verifico", "F");
            Upd.Add("idpedidoenc", "@idpedidoenc", "F");
            Upd.Add("no_encontrado", "@no_encontrado", "F");
            Upd.Add("idubicaciontemporal", "@idubicaciontemporal", "F");
            Upd.Add("idoperadorbodega_asignado", "@idoperadorbodega_asignado", "F");
            Upd.Where("IdPickingUbic = @IdPickingUbic");

            string sp = Upd.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
            {
                Bind(cmd, oBeTrans_picking_ubic);
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }
        catch (SqlException)
        {
            throw;
        }

        return rowsAffected;
    }

    public static int Actualizar_3pl(clsBeTrans_picking_ubic_3pl oBeTrans_picking_ubic, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

        try
        {
            Upd.Init("trans_picking_ubic");
            Upd.Add("idpickingubic", "@idpickingubic", "F");
            Upd.Add("idpickingenc", "@idpickingenc", "F");
            Upd.Add("idpickingdet", "@idpickingdet", "F");
            Upd.Add("idubicacion", "@idubicacion", "F");
            Upd.Add("idstock", "@idstock", "F");
            Upd.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Upd.Add("idproductobodega", "@idproductobodega", "F");
            Upd.Add("idproductoestado", "@idproductoestado", "F");
            Upd.Add("idpresentacion", "@idpresentacion", "F");
            Upd.Add("idunidadmedida", "@idunidadmedida", "F");
            Upd.Add("idubicacionanterior", "@idubicacionanterior", "F");
            Upd.Add("idrecepcion", "@idrecepcion", "F");
            Upd.Add("lote", "@lote", "F");
            Upd.Add("fecha_vence", "@fecha_vence", "F");
            Upd.Add("fecha_minima", "@fecha_minima", "F");
            Upd.Add("serial", "@serial", "F");
            Upd.Add("lic_plate", "@lic_plate", "F");
            Upd.Add("acepto", "@acepto", "F");
            Upd.Add("peso_solicitado", "@peso_solicitado", "F");
            Upd.Add("peso_recibido", "@peso_recibido", "F");
            Upd.Add("peso_verificado", "@peso_verificado", "F");
            Upd.Add("peso_despachado", "@peso_despachado", "F");
            Upd.Add("cantidad_solicitada", "@cantidad_solicitada", "F");
            Upd.Add("cantidad_recibida", "@cantidad_recibida", "F");
            Upd.Add("cantidad_verificada", "@cantidad_verificada", "F");
            Upd.Add("encontrado", "@encontrado", "F");
            Upd.Add("dañado_verificacion", "@dañado_verificacion", "F");
            Upd.Add("fecha_real_vence", "@fecha_real_vence", "F");
            Upd.Add("no_packing", "@no_packing", "F");
            Upd.Add("fecha_picking", "@fecha_picking", "F");
            Upd.Add("fecha_verificado", "@fecha_verificado", "F");
            Upd.Add("fecha_packing", "@fecha_packing", "F");
            Upd.Add("fecha_despachado", "@fecha_despachado", "F");
            Upd.Add("cantidad_despachada", "@cantidad_despachada", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("idpedidodet", "@idpedidodet", "F");
            Upd.Add("dañado_picking", "@dañado_picking", "F");
            Upd.Add("idstockres", "@idstockres", "F");
            Upd.Add("lic_plate_reemplazo", "@lic_plate_reemplazo", "F");
            Upd.Add("idubicacion_reemplazo", "@idubicacion_reemplazo", "F");
            Upd.Add("idstock_reemplazo", "@idstock_reemplazo", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("idoperadorbodega_pickeo", "@idoperadorbodega_pickeo", "F");
            Upd.Add("idoperadorbodega_verifico", "@idoperadorbodega_verifico", "F");
            Upd.Add("idpedidoenc", "@idpedidoenc", "F");
            Upd.Add("no_encontrado", "@no_encontrado", "F");
            Upd.Add("idubicaciontemporal", "@idubicaciontemporal", "F");
            Upd.Add("idoperadorbodega_asignado", "@idoperadorbodega_asignado", "F");
            Upd.Where("IdPickingUbic = @IdPickingUbic");

            string sp = Upd.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
            {
                Bind_3pl(cmd, oBeTrans_picking_ubic);
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }
        catch (SqlException)
        {
            throw;
        }

        return rowsAffected;
    }
    public int Eliminar(IConfiguration config, clsBeTrans_picking_ubic oBeTrans_picking_ubic, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_picking_ubic" +
             "  Where(IdPickingUbic = @IdPickingUbic)");

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

            cmd.Parameters.Add(new SqlParameter("@IdPickingUbic", oBeTrans_picking_ubic.IdPickingUbic));

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
            const string sp = "Select * FROM Trans_picking_ubic";
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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_picking_ubic pBeTrans_picking_ubic)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_picking_ubic" +
            " Where(IdPickingUbic = @IdPickingUbic)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            Bind(cmd, pBeTrans_picking_ubic);

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_picking_ubic, r);
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

    public static List<clsBeTrans_picking_ubic> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_picking_ubic> lreturnList = new List<clsBeTrans_picking_ubic>();

        try
        {
            const string sp = "Select * FROM Trans_picking_ubic";

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

                        clsBeTrans_picking_ubic vBeTrans_picking_ubic = new clsBeTrans_picking_ubic();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_picking_ubic = new clsBeTrans_picking_ubic();
                            Cargar(ref vBeTrans_picking_ubic, dr);
                            lreturnList.Add(vBeTrans_picking_ubic);
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

            const string sp = "Select ISNULL(Max(IdPickingUbic),0) FROM Trans_picking_ubic";

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
            const string sp = "Select ISNULL(Max(IdPickingUbic),0) FROM Trans_picking_ubic";

            using (var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
            {
                var lreturnValue = cmd.ExecuteScalar();

                if (lreturnValue != DBNull.Value && lreturnValue != null)
                {
                    lMax = Convert.ToInt32(lreturnValue);
                }
            }

            return lMax;
        }
        catch (SqlException)
        {    
            throw;
        }
    }
    public static void Bind(SqlCommand cmd, clsBeTrans_picking_ubic o)
    {
        cmd.Parameters.Add(new SqlParameter("@IdPickingUbic", o.IdPickingUbic != 0 ? o.IdPickingUbic : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPickingEnc", o.IdPickingEnc != 0 ? o.IdPickingEnc : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPickingDet", o.IdPickingDet != 0 ? o.IdPickingDet : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdUbicacion", o.IdUbicacion != 0 ? o.IdUbicacion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdStock", o.IdStock != 0 ? o.IdStock : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPropietarioBodega", o.IdPropietarioBodega != 0 ? o.IdPropietarioBodega : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", o.IdProductoBodega != 0 ? o.IdProductoBodega : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdProductoEstado", o.IdProductoEstado != 0 ? o.IdProductoEstado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPresentacion", o.IdPresentacion != 0 ? o.IdPresentacion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdUnidadMedida", o.IdUnidadMedida != 0 ? o.IdUnidadMedida : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdUbicacionAnterior", o.IdUbicacionAnterior != 0 ? o.IdUbicacionAnterior : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdRecepcion", o.IdRecepcion != 0 ? o.IdRecepcion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@lote", !string.IsNullOrWhiteSpace(o.Lote) ? o.Lote : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_vence", o.Fecha_vence != DateTime.MinValue ? o.Fecha_vence : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_minima", o.Fecha_minima != DateTime.MinValue ? o.Fecha_minima : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@serial", !string.IsNullOrWhiteSpace(o.Serial) ? o.Serial : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@lic_plate", !string.IsNullOrWhiteSpace(o.Lic_plate) ? o.Lic_plate : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@acepto", o.Acepto));
        cmd.Parameters.Add(new SqlParameter("@peso_solicitado", o.Peso_solicitado != 0 ? o.Peso_solicitado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@peso_recibido", o.Peso_recibido != 0 ? o.Peso_recibido : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@peso_verificado", o.Peso_verificado != 0 ? o.Peso_verificado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@peso_despachado", o.Peso_despachado != 0 ? o.Peso_despachado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@cantidad_solicitada", o.Cantidad_solicitada != 0 ? o.Cantidad_solicitada : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@cantidad_recibida", o.Cantidad_recibida != 0 ? o.Cantidad_recibida : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@cantidad_verificada", o.Cantidad_verificada != 0 ? o.Cantidad_verificada : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@encontrado", o.Encontrado));
        cmd.Parameters.Add(new SqlParameter("@dañado_verificacion", o.Dañado_verificacion));
        cmd.Parameters.Add(new SqlParameter("@fecha_real_vence", o.Fecha_real_vence != DateTime.MinValue ? o.Fecha_real_vence : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@no_packing", !string.IsNullOrWhiteSpace(o.No_packing) ? o.No_packing : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_picking", o.Fecha_picking != DateTime.MinValue ? o.Fecha_picking : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_verificado", o.Fecha_verificado != DateTime.MinValue ? o.Fecha_verificado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_packing", o.Fecha_packing != DateTime.MinValue ? o.Fecha_packing : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_despachado", o.Fecha_despachado != DateTime.MinValue ? o.Fecha_despachado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@cantidad_despachada", o.Cantidad_despachada != 0 ? o.Cantidad_despachada : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_agr", !string.IsNullOrWhiteSpace(o.User_agr) ? o.User_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", o.Fec_agr != DateTime.MinValue ? o.Fec_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_mod", !string.IsNullOrWhiteSpace(o.User_mod) ? o.User_mod : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", o.Fec_mod != DateTime.MinValue ? o.Fec_mod : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@activo", o.Activo));
        cmd.Parameters.Add(new SqlParameter("@IdPedidoDet", o.IdPedidoDet != 0 ? o.IdPedidoDet : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@dañado_picking", o.Dañado_picking));
        cmd.Parameters.Add(new SqlParameter("@IdStockRes", o.IdStockRes != 0 ? o.IdStockRes : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@lic_plate_reemplazo", !string.IsNullOrWhiteSpace(o.Lic_plate_reemplazo) ? o.Lic_plate_reemplazo : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdUbicacion_reemplazo", o.IdUbicacion_reemplazo != 0 ? o.IdUbicacion_reemplazo : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdStock_reemplazo", o.IdStock_reemplazo != 0 ? o.IdStock_reemplazo : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdBodega", o.IdBodega != 0 ? o.IdBodega : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdOperadorBodega_Pickeo", o.IdOperadorBodega_Pickeo != 0 ? o.IdOperadorBodega_Pickeo : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdOperadorBodega_Verifico", o.IdOperadorBodega_Verifico != 0 ? o.IdOperadorBodega_Verifico : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPedidoEnc", o.IdPedidoEnc != 0 ? o.IdPedidoEnc : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@no_encontrado", o.No_encontrado));
        cmd.Parameters.Add(new SqlParameter("@IdUbicacionTemporal", o.IdUbicacionTemporal != 0 ? o.IdUbicacionTemporal : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdOperadorBodega_Asignado", o.IdOperadorBodega_Asignado != 0 ? o.IdOperadorBodega_Asignado : DBNull.Value));
    }

    public static void Bind_3pl(SqlCommand cmd, clsBeTrans_picking_ubic_3pl o)
    {
        cmd.Parameters.Add(new SqlParameter("@IdPickingUbic", o.IdPickingUbic != 0 ? o.IdPickingUbic : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPickingEnc", o.IdPickingEnc != 0 ? o.IdPickingEnc : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPickingDet", o.IdPickingDet != 0 ? o.IdPickingDet : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdUbicacion", o.IdUbicacion != 0 ? o.IdUbicacion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdStock", o.IdStock != 0 ? o.IdStock : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPropietarioBodega", o.IdPropietarioBodega != 0 ? o.IdPropietarioBodega : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", o.IdProductoBodega != 0 ? o.IdProductoBodega : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdProductoEstado", o.IdProductoEstado != 0 ? o.IdProductoEstado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPresentacion", o.IdPresentacion != 0 ? o.IdPresentacion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdUnidadMedida", o.IdUnidadMedida != 0 ? o.IdUnidadMedida : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdUbicacionAnterior", o.IdUbicacionAnterior != 0 ? o.IdUbicacionAnterior : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdRecepcion", o.IdRecepcion != 0 ? o.IdRecepcion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@lote", !string.IsNullOrWhiteSpace(o.Lote) ? o.Lote : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_vence", o.Fecha_Vence != DateTime.MinValue ? o.Fecha_Vence : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_minima", o.Fecha_minima != DateTime.MinValue ? o.Fecha_minima : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@serial", !string.IsNullOrWhiteSpace(o.Serial) ? o.Serial : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@lic_plate", !string.IsNullOrWhiteSpace(o.Lic_plate) ? o.Lic_plate : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@acepto", o.Acepto));
        cmd.Parameters.Add(new SqlParameter("@peso_solicitado", o.Peso_solicitado != 0 ? o.Peso_solicitado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@peso_recibido", o.Peso_recibido != 0 ? o.Peso_recibido : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@peso_verificado", o.Peso_verificado != 0 ? o.Peso_verificado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@peso_despachado", o.Peso_despachado != 0 ? o.Peso_despachado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@cantidad_solicitada", o.Cantidad_Solicitada != 0 ? o.Cantidad_Solicitada : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@cantidad_recibida", o.Cantidad_Recibida != 0 ? o.Cantidad_Recibida : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@cantidad_verificada", o.Cantidad_Verificada != 0 ? o.Cantidad_Verificada : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@encontrado", o.Encontrado));
        cmd.Parameters.Add(new SqlParameter("@dañado_verificacion", o.Dañado_verificacion));
        cmd.Parameters.Add(new SqlParameter("@fecha_real_vence", o.Fecha_real_vence != DateTime.MinValue ? o.Fecha_real_vence : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@no_packing", !string.IsNullOrWhiteSpace(o.No_packing) ? o.No_packing : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_picking", o.Fecha_picking != DateTime.MinValue ? o.Fecha_picking : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_verificado", o.Fecha_verificado != DateTime.MinValue ? o.Fecha_verificado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_packing", o.Fecha_packing != DateTime.MinValue ? o.Fecha_packing : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_despachado", o.Fecha_despachado != DateTime.MinValue ? o.Fecha_despachado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@cantidad_despachada", o.Cantidad_despachada != 0 ? o.Cantidad_despachada : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_agr", !string.IsNullOrWhiteSpace(o.User_agr) ? o.User_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", o.Fec_agr != DateTime.MinValue ? o.Fec_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_mod", !string.IsNullOrWhiteSpace(o.User_mod) ? o.User_mod : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_mod", o.Fec_mod != DateTime.MinValue ? o.Fec_mod : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@activo", o.Activo));
        cmd.Parameters.Add(new SqlParameter("@IdPedidoDet", o.IdPedidoDet != 0 ? o.IdPedidoDet : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@dañado_picking", o.Dañado_picking));
        cmd.Parameters.Add(new SqlParameter("@IdStockRes", o.IdStockRes != 0 ? o.IdStockRes : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@lic_plate_reemplazo", !string.IsNullOrWhiteSpace(o.Lic_plate_Reemplazo) ? o.Lic_plate_Reemplazo : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdUbicacion_reemplazo", o.IdUbicacion_reemplazo != 0 ? o.IdUbicacion_reemplazo : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdStock_reemplazo", o.IdStock_reemplazo != 0 ? o.IdStock_reemplazo : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdBodega", o.IdBodega != 0 ? o.IdBodega : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdOperadorBodega_Pickeo", o.IdOperadorBodega_Pickeo != 0 ? o.IdOperadorBodega_Pickeo : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdOperadorBodega_Verifico", o.IdOperadorBodega_Verifico != 0 ? o.IdOperadorBodega_Verifico : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPedidoEnc", o.IdPedidoEnc != 0 ? o.IdPedidoEnc : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@no_encontrado", o.No_encontrado));
        cmd.Parameters.Add(new SqlParameter("@IdUbicacionTemporal", o.IdUbicacionTemporal != 0 ? o.IdUbicacionTemporal : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdOperadorBodega_Asignado", o.IdOperadorBodega_Asignado != 0 ? o.IdOperadorBodega_Asignado : DBNull.Value));
    }
    public static int InsertOrUpdate(List<clsBeTrans_picking_ubic> entities, SqlConnection conn, SqlTransaction tx)
    {
        int total = 0;

        try
        {
            foreach (var entity in entities)
            {
                bool existe = Existe(entity.IdPickingUbic, entity.IdPickingEnc, conn, tx);

                int resultado = existe
                    ? Actualizar(entity, conn, tx)
                    : Insertar(entity, conn, tx);

                total += resultado;
            }

            return total;
        }
        catch
        {
            throw;
        }
    }

    public static int InsertOrUpdate_3pl(List<clsBeTrans_picking_ubic_3pl> entities, SqlConnection conn, SqlTransaction tx)
    {
        int total = 0;

        try
        {
            foreach (var entity in entities)
            {
                bool existe = Existe(entity.IdPickingUbic, entity.IdPickingEnc, conn, tx);

                int resultado = existe
                    ? Actualizar_3pl(entity, conn, tx)
                    : Insertar_3pl(entity, conn, tx);

                total += resultado;
            }

            return total;
        }
        catch
        {
            throw;
        }
    }
    public static bool Existe(int idPickingUbic, int idPickingEnc, SqlConnection conn, SqlTransaction? tx = null)
    {
        const string sql = @"
        SELECT COUNT(1)
        FROM trans_picking_ubic
        WHERE IdPickingUbic = @IdPickingUbic AND IdPickingEnc = @IdPickingEnc";

        using SqlCommand cmd = new(sql, conn, tx);
        cmd.Parameters.AddWithValue("@IdPickingUbic", idPickingUbic);
        cmd.Parameters.AddWithValue("@IdPickingEnc", idPickingEnc);
        int count = Convert.ToInt32(cmd.ExecuteScalar());

        return count > 0;
    }

    public static List<clsBeTrans_picking_ubic>? Get_All_PickingUbic_Despachado_By_IdDespachoEnc(int pIdDespachoEnc,
                                                                                                 SqlConnection lConnection,
                                                                                                 SqlTransaction lTransaction)
    {
        List<clsBeTrans_picking_ubic>? lReturnList = null;

        try
        {
            string vSQL = @"SELECT * FROM VW_PickingUbic_Desp_By_IdDespachoEnc
                        WHERE IdDespachoEnc = @IdDespachoEnc";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.Add(new SqlParameter("@IdDespachoEnc", pIdDespachoEnc));

                DataTable lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable != null && lDataTable.Rows.Count > 0)
                {
                    lReturnList = new List<clsBeTrans_picking_ubic>();

                    foreach (DataRow lRow in lDataTable.Rows)
                    {
                        clsBeTrans_picking_ubic Obj = new clsBeTrans_picking_ubic();

                        Cargar_For_Despacho(ref Obj, lRow);

                        Obj.Ubicacion.IdUbicacion = lRow["IdUbicacion"] == DBNull.Value ? 0 : Convert.ToInt32(lRow["IdUbicacion"]);                        
                        Obj.IdPedidoDet = lRow["IdPedidoDet"] == DBNull.Value ? 0 : Convert.ToInt32(lRow["IdPedidoDet"]);
                        Obj.NombreUbicacion = lRow["Nombre_Ubicacion"] == DBNull.Value ? "" : Convert.ToString(lRow["Nombre_Ubicacion"]) ?? "";
                        Obj.CodigoProducto = lRow["codigo"] == DBNull.Value ? "" : Convert.ToString(lRow["codigo"]) ?? "";
                        Obj.NombreProducto = lRow["nombre"] == DBNull.Value ? "" : Convert.ToString(lRow["nombre"]) ?? "";

                        if (lDataTable.Columns.Contains("Presentacion"))
                        {
                            Obj.ProductoPresentacion = lRow["Presentacion"] == DBNull.Value ? "" : Convert.ToString(lRow["Presentacion"]) ?? "";
                        }

                        if (lDataTable.Columns.Contains("UnidadMedida"))
                        {
                            Obj.ProductoUnidadMedida = lRow["UnidadMedida"] == DBNull.Value ? "" : Convert.ToString(lRow["UnidadMedida"]) ?? "";
                        }

                        if (lDataTable.Columns.Contains("NomEstado"))
                        {
                            Obj.ProductoEstado = lRow["NomEstado"] == DBNull.Value ? "" : Convert.ToString(lRow["NomEstado"]) ?? "";
                        }

                        Obj.IdProductoBodega = lRow["IdProductoBodega"] == DBNull.Value ? 0 : Convert.ToInt32(lRow["IdProductoBodega"]);
                        Obj.IdProductoEstado = lRow["IdProductoEstado"] == DBNull.Value ? 0 : Convert.ToInt32(lRow["IdProductoEstado"]);
                        Obj.IdPresentacion = lRow["IdPresentacion"] == DBNull.Value ? 0 : Convert.ToInt32(lRow["IdPresentacion"]);
                        Obj.IdUnidadMedida = lRow["IdUnidadMedida"] == DBNull.Value ? 0 : Convert.ToInt32(lRow["IdUnidadMedida"]);
                        Obj.IdStock = lRow["IdStock"] == DBNull.Value ? 0 : Convert.ToInt32(lRow["IdStock"]);
                        Obj.IdPedidoEnc = lRow["IdPedidoEnc"] == DBNull.Value ? 0 : Convert.ToInt32(lRow["IdPedidoEnc"]);
                        Obj.Codigo_Talla = lRow["Talla"] == DBNull.Value ? "" : Convert.ToString(lRow["Talla"]) ?? "";
                        Obj.Codigo_Color = lRow["Color"] == DBNull.Value ? "" : Convert.ToString(lRow["Color"]) ?? "";
                        Obj.No_Linea = lRow["No_Linea"] == DBNull.Value ? 0 : Convert.ToInt32(lRow["No_Linea"]);
                        Obj.IsNew = false;

                        lReturnList.Add(Obj);
                    }
                }
            }

            return lReturnList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static void Cargar_For_Despacho(ref clsBeTrans_picking_ubic oBeTrans_picking_ubic, DataRow dr)
    {
        try
        {
            oBeTrans_picking_ubic.IdPickingEnc = dr["IdPickingEnc"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdPickingEnc"]);
            oBeTrans_picking_ubic.IdPickingUbic = dr["IdPickingUbic"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdPickingUbic"]);
            oBeTrans_picking_ubic.IdPickingDet = dr["IdPickingDet"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdPickingDet"]);
            oBeTrans_picking_ubic.IdPedidoDet = dr["IdPedidoDet"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdPedidoDet"]); // #CKFK 20180331 Agregué el IdPedidoDet en el cargar porque se estaba quedando con valor 0
            oBeTrans_picking_ubic.IdUbicacion = dr["IdUbicacion"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdUbicacion"]);
            oBeTrans_picking_ubic.IdStock = dr["IdStock"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdStock"]);
            oBeTrans_picking_ubic.IdPropietarioBodega = dr["IdPropietarioBodega"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdPropietarioBodega"]);
            oBeTrans_picking_ubic.IdProductoBodega = dr["IdProductoBodega"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdProductoBodega"]);
            oBeTrans_picking_ubic.IdProductoEstado = dr["IdProductoEstado"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdProductoEstado"]);
            oBeTrans_picking_ubic.IdPresentacion = dr["IdPresentacion"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdPresentacion"]);
            oBeTrans_picking_ubic.IdUnidadMedida = dr["IdUnidadMedida"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdUnidadMedida"]);
            oBeTrans_picking_ubic.IdUbicacionAnterior = dr["IdUbicacionAnterior"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdUbicacionAnterior"]);
            oBeTrans_picking_ubic.IdRecepcion = dr["IdRecepcion"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdRecepcion"]);            
            oBeTrans_picking_ubic.Fecha_vence = dr["fecha_vence"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fecha_vence"]);
            oBeTrans_picking_ubic.Fecha_minima = dr["fecha_minima"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fecha_minima"]);
            oBeTrans_picking_ubic.Lote = dr["lote"] == DBNull.Value ? "" : Convert.ToString(dr["lote"]) ?? "";
            oBeTrans_picking_ubic.Serial = dr["serial"] == DBNull.Value ? "" : Convert.ToString(dr["serial"]) ?? "";
            oBeTrans_picking_ubic.Lic_plate = dr["lic_plate"] == DBNull.Value ? "" : Convert.ToString(dr["lic_plate"]) ?? "";
            oBeTrans_picking_ubic.Acepto = dr["acepto"] != DBNull.Value && Convert.ToBoolean(dr["acepto"]);
            oBeTrans_picking_ubic.Peso_solicitado = dr["peso_solicitado"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["peso_solicitado"]);
            oBeTrans_picking_ubic.Peso_recibido = dr["peso_recibido"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["peso_recibido"]);
            oBeTrans_picking_ubic.Peso_verificado = dr["peso_verificado"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["peso_verificado"]);
            oBeTrans_picking_ubic.Peso_despachado = dr["peso_despachado"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["peso_despachado"]);
            oBeTrans_picking_ubic.Cantidad_solicitada = dr["cantidad_solicitada"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["cantidad_solicitada"]);
            oBeTrans_picking_ubic.Cantidad_recibida = dr["cantidad_recibida"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["cantidad_recibida"]);
            oBeTrans_picking_ubic.Cantidad_verificada = dr["cantidad_verificada"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["cantidad_verificada"]);
            oBeTrans_picking_ubic.Encontrado = dr["encontrado"] != DBNull.Value && Convert.ToBoolean(dr["encontrado"]);
            oBeTrans_picking_ubic.Dañado_verificacion = dr["dañado_verificacion"] != DBNull.Value && Convert.ToBoolean(dr["dañado_verificacion"]);
            oBeTrans_picking_ubic.Fecha_real_vence = dr["fecha_real_vence"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fecha_real_vence"]);            
            oBeTrans_picking_ubic.Fecha_picking = dr["fecha_picking"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fecha_picking"]);
            oBeTrans_picking_ubic.Fecha_verificado = dr["fecha_verificado"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fecha_verificado"]);
            oBeTrans_picking_ubic.Fecha_packing = dr["fecha_packing"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fecha_packing"]);
            oBeTrans_picking_ubic.Fecha_despachado = dr["fecha_despachado"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fecha_despachado"]);
            oBeTrans_picking_ubic.Cantidad_despachada = dr["cantidad_despachada"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["cantidad_despachada"]);            
            oBeTrans_picking_ubic.Fec_agr = dr["fec_agr"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fec_agr"]);            
            oBeTrans_picking_ubic.Fec_mod = dr["fec_mod"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fec_mod"]);
            oBeTrans_picking_ubic.Activo = dr["activo"] != DBNull.Value && Convert.ToBoolean(dr["activo"]);
            oBeTrans_picking_ubic.Dañado_picking = dr["dañado_picking"] != DBNull.Value && Convert.ToBoolean(dr["dañado_picking"]);
            oBeTrans_picking_ubic.IdUbicacionTemporal = dr["IdUbicacionTemporal"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdUbicacionTemporal"]);
            oBeTrans_picking_ubic.IdProductoTallaColor = dr["IdProductoTallaColor"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdProductoTallaColor"]);
            oBeTrans_picking_ubic.No_packing = dr["no_packing"] == DBNull.Value ? "" : Convert.ToString(dr["no_packing"]) ?? "";
            oBeTrans_picking_ubic.User_agr = dr["user_agr"] == DBNull.Value ? "" : Convert.ToString(dr["user_agr"]) ?? "";
            oBeTrans_picking_ubic.User_mod = dr["user_mod"] == DBNull.Value ? "" : Convert.ToString(dr["user_mod"]) ?? "";
        }        
        catch (Exception)
        {         
            throw;
        }
    }

    public static List<clsBeTrans_picking_ubic>? Get_All_PickingUbic_By_IdPedidoDet(int pIdPedidoDet, int pIdPedidoEnc, SqlConnection pConnection, SqlTransaction pTransaction)
    {
        List<clsBeTrans_picking_ubic>? lReturnList = null;

        try
        {
            string vSQL = " SELECT * FROM VW_PickingUbic_By_IdPedidoDet WHERE IdPedidoDet = @IdPedidoDet AND IdPedidoEnc = @IdPedidoEnc ";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, pConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = pTransaction;
                lDTA.SelectCommand.CommandTimeout = 100;
                lDTA.SelectCommand.Parameters.Add(new SqlParameter("@IdPedidoDet", pIdPedidoDet));
                lDTA.SelectCommand.Parameters.Add(new SqlParameter("@IdPedidoEnc", pIdPedidoEnc));

                DataTable lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable != null && lDataTable.Rows.Count > 0)
                {
                    lReturnList = new List<clsBeTrans_picking_ubic>();

                    foreach (DataRow lRow in lDataTable.Rows)
                    {
                        clsBeTrans_picking_ubic Obj = new clsBeTrans_picking_ubic();
                        Cargar(ref Obj, lRow);

                        Obj.Ubicacion.IdUbicacion = lRow.Field<int?>("IdUbicacion") ?? 0;
                        Obj.NombreUbicacion = lRow.Field<string>("Nombre_Ubicacion") ?? "";
                        Obj.IdPickingEnc = lRow.Field<int?>("IdPickingEnc") ?? 0;
                        Obj.IdPedidoDet = lRow.Field<int?>("IdPedidoDet") ?? 0;
                        Obj.CodigoProducto = lRow.Field<string>("codigo_producto") ?? "";
                        Obj.NombreProducto = lRow.Field<string>("nombre_producto") ?? "";
                        Obj.ProductoPresentacion = lRow.Field<string>("nom_presentacion") ?? "";
                        Obj.ProductoUnidadMedida = lRow.Field<string>("nom_unid_med") ?? "";
                        Obj.ProductoEstado = lRow.Field<string>("nom_estado") ?? "";
                        Obj.IdProductoBodega = lRow.Field<int?>("IdProductoBodega") ?? 0;
                        Obj.IdProductoEstado = lRow.Field<int?>("IdProductoEstado") ?? 0;
                        Obj.IdPresentacion = lRow.Field<int?>("IdPresentacion") ?? 0;
                        Obj.IdUnidadMedida = lRow.Field<int?>("IdUnidadMedidaBasica") ?? 0;
                        Obj.IdStockRes = lRow.Field<int?>("IdStockRes") ?? 0;
                        Obj.IdStock = lRow.Field<int?>("IdStock") ?? 0;
                        Obj.IdPedidoEnc = lRow.Field<int?>("IdPedidoEnc") ?? 0;
                        Obj.IsNew = false;

                        lReturnList.Add(Obj);
                    }
                }
            }

            return lReturnList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static bool Insertar_PickingUbic(clsBeStock_res pStockRes,
                                            int pIdPickingDet,
                                            SqlConnection lConnection,
                                            SqlTransaction lTransaction)
    {
        bool result = false;

        try
        {
            clsBeTrans_picking_ubic BePickingUbic = new clsBeTrans_picking_ubic();

            BePickingUbic.IdPickingUbic = MaxID(lConnection, lTransaction) + 1;
            BePickingUbic.IdPedidoDet = pStockRes.IdPedidoDet;
            BePickingUbic.IdStockRes = pStockRes.IdStockRes;
            BePickingUbic.IdPickingDet = pIdPickingDet;
            BePickingUbic.IdPickingEnc = pStockRes.IdPicking;
            BePickingUbic.IdPedidoEnc = pStockRes.IdPedido;
            BePickingUbic.IdStock = pStockRes.IdStock;
            BePickingUbic.IdPropietarioBodega = pStockRes.IdPropietarioBodega;
            BePickingUbic.IdProductoBodega = pStockRes.IdProductoBodega;
            BePickingUbic.IdProductoEstado = pStockRes.IdProductoEstado;
            BePickingUbic.IdPresentacion = pStockRes.IdPresentacion;
            BePickingUbic.IdUnidadMedida = pStockRes.IdUnidadMedida;
            BePickingUbic.IdUbicacionAnterior = Convert.ToInt32(pStockRes.Ubicacion_ant);
            BePickingUbic.IdRecepcion = pStockRes.IdRecepcion;
            BePickingUbic.IdUbicacion = pStockRes.IdUbicacion;
            BePickingUbic.Lote = pStockRes.Lote;
            BePickingUbic.Fecha_vence = pStockRes.Fecha_vence;
            BePickingUbic.Serial = pStockRes.Serial;
            BePickingUbic.Lic_plate = pStockRes.Lic_plate;
            BePickingUbic.Peso_solicitado = pStockRes.Peso;
            BePickingUbic.Cantidad_solicitada = pStockRes.Cantidad;
            BePickingUbic.Cantidad_recibida = 0.0;
            BePickingUbic.Fecha_real_vence = pStockRes.Fecha_vence;
            BePickingUbic.User_agr = pStockRes.User_agr;
            BePickingUbic.Fec_agr = DateTime.Now;
            BePickingUbic.User_mod = pStockRes.User_mod;
            BePickingUbic.Fec_mod = DateTime.Now;
            BePickingUbic.Activo = true;
            BePickingUbic.IsNew = true;
            BePickingUbic.IdBodega = clsLnProducto_bodega.Get_IdBodega_By_IdProductoBodega(pStockRes.IdProductoBodega, lConnection, lTransaction);
            BePickingUbic.IdProducto = clsLnProducto_bodega.Get_IdProducto_By_IdProductoBodega(pStockRes.IdProductoBodega, lConnection, lTransaction);

            double CantidadStockDestino = BePickingUbic.Cantidad_solicitada;

            bool vPermitirDecimales = clsLnBodega.Get_Permitir_Decimales(BePickingUbic.IdBodega, lConnection, lTransaction);
            clsPublic.Abs(CantidadStockDestino - Math.Floor(CantidadStockDestino), vPermitirDecimales);

            Insertar(BePickingUbic, lConnection, lTransaction);

            pStockRes.IdPicking = pStockRes.IdPicking;
            pStockRes.User_agr = pStockRes.User_agr;
            clsLnStock_res.Actualizar_IdPickingEnc(pStockRes, lConnection, lTransaction);

            result = true;
        }
        catch (Exception)
        {            
            throw;
        }

        return result;
    }
}