using System.Data;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using WMS.EntityCore.Producto;
using WMS.EntityCore.Stock;
using WMSWebAPI.Be;

public class clsLnStock_res
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeStock_res oBeStock_res, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }        
        double GetDecimal(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeStock_res.IdStockRes = GetInt("IdStockRes");
            oBeStock_res.IdTransaccion = GetInt("IdTransaccion");
            oBeStock_res.Indicador = GetString("Indicador");
            oBeStock_res.IdPedidoDet = GetInt("IdPedidoDet");
            oBeStock_res.IdStock = GetInt("IdStock");
            oBeStock_res.IdPropietarioBodega = GetInt("IdPropietarioBodega");
            oBeStock_res.IdProductoBodega = GetInt("IdProductoBodega");
            oBeStock_res.IdProductoEstado = GetInt("IdProductoEstado");
            oBeStock_res.IdPresentacion = GetInt("IdPresentacion");
            oBeStock_res.IdUnidadMedida = GetInt("IdUnidadMedida");
            oBeStock_res.IdUbicacion = GetInt("IdUbicacion");
            oBeStock_res.Ubicacion_ant = GetString("ubicacion_ant");
            oBeStock_res.IdRecepcion = GetInt("IdRecepcion");
            oBeStock_res.Lote = GetString("lote");
            oBeStock_res.Lic_plate = GetString("lic_plate");
            oBeStock_res.Serial = GetString("serial");
            oBeStock_res.Cantidad = GetDecimal("cantidad");
            oBeStock_res.Peso = GetDecimal("peso");
            oBeStock_res.Estado = GetString("estado");
            oBeStock_res.Fecha_ingreso = GetDate("fecha_ingreso");
            oBeStock_res.Fecha_vence = GetDate("fecha_vence");
            oBeStock_res.Uds_lic_plate = GetDecimal("uds_lic_plate");
            oBeStock_res.No_bulto = GetInt("no_bulto");
            oBeStock_res.IdPicking = GetInt("IdPicking");
            oBeStock_res.IdPedido = GetInt("IdPedido");
            oBeStock_res.IdDespacho = GetInt("IdDespacho");
            oBeStock_res.User_agr = GetString("user_agr");
            oBeStock_res.Fec_agr = GetDate("fec_agr");
            oBeStock_res.User_mod = GetString("user_mod");
            oBeStock_res.Fec_mod = GetDate("fec_mod");
            oBeStock_res.Host = GetString("host");
            oBeStock_res.Añada = GetInt("añada");
            oBeStock_res.Fecha_manufactura = GetDate("fecha_manufactura");
            oBeStock_res.IdBodega = GetInt("IdBodega");
            oBeStock_res.Pallet_no_estandar = GetBool("pallet_no_estandar");
        }
        catch (Exception ex)
        {            
            throw new Exception(ex.Message);
        }
    }

    public static int Insertar(IConfiguration config, clsBeStock_res oBeStock_res, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("stock_res");
            Ins.Add("idstockres", "@idstockres", "F");
            Ins.Add("idtransaccion", "@idtransaccion", "F");
            Ins.Add("indicador", "@indicador", "F");
            Ins.Add("idpedidodet", "@idpedidodet", "F");
            Ins.Add("idstock", "@idstock", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idproductoestado", "@idproductoestado", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idunidadmedida", "@idunidadmedida", "F");
            Ins.Add("idubicacion", "@idubicacion", "F");
            Ins.Add("ubicacion_ant", "@ubicacion_ant", "F");
            Ins.Add("idrecepcion", "@idrecepcion", "F");
            Ins.Add("lote", "@lote", "F");
            Ins.Add("lic_plate", "@lic_plate", "F");
            Ins.Add("serial", "@serial", "F");
            Ins.Add("cantidad", "@cantidad", "F");
            Ins.Add("peso", "@peso", "F");
            Ins.Add("estado", "@estado", "F");
            Ins.Add("fecha_ingreso", "@fecha_ingreso", "F");
            Ins.Add("fecha_vence", "@fecha_vence", "F");
            Ins.Add("uds_lic_plate", "@uds_lic_plate", "F");
            Ins.Add("no_bulto", "@no_bulto", "F");
            Ins.Add("idpicking", "@idpicking", "F");
            Ins.Add("idpedido", "@idpedido", "F");
            Ins.Add("iddespacho", "@iddespacho", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("host", "@host", "F");
            Ins.Add("añada", "@añada", "F");
            Ins.Add("fecha_manufactura", "@fecha_manufactura", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("pallet_no_estandar", "@pallet_no_estandar", "F");

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

            cmd.Parameters.Add(new SqlParameter("@IdStockRes", oBeStock_res.IdStockRes));
            cmd.Parameters.Add(new SqlParameter("@IdTransaccion", oBeStock_res.IdTransaccion));
            cmd.Parameters.Add(new SqlParameter("@Indicador", oBeStock_res.Indicador));
            cmd.Parameters.Add(new SqlParameter("@IdPedidoDet", oBeStock_res.IdPedidoDet));
            cmd.Parameters.Add(new SqlParameter("@IdStock", oBeStock_res.IdStock));
            cmd.Parameters.Add(new SqlParameter("@IdPropietarioBodega", oBeStock_res.IdPropietarioBodega));
            cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", oBeStock_res.IdProductoBodega));
            cmd.Parameters.Add(new SqlParameter("@IdProductoEstado", oBeStock_res.IdProductoEstado));
            cmd.Parameters.Add(new SqlParameter("@IdPresentacion", oBeStock_res.IdPresentacion));
            cmd.Parameters.Add(new SqlParameter("@IdUnidadMedida", oBeStock_res.IdUnidadMedida));
            cmd.Parameters.Add(new SqlParameter("@IdUbicacion", oBeStock_res.IdUbicacion));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_ant", oBeStock_res.Ubicacion_ant));
            cmd.Parameters.Add(new SqlParameter("@IdRecepcion", oBeStock_res.IdRecepcion));
            cmd.Parameters.Add(new SqlParameter("@lote", oBeStock_res.Lote));
            cmd.Parameters.Add(new SqlParameter("@lic_plate", oBeStock_res.Lic_plate));
            cmd.Parameters.Add(new SqlParameter("@serial", oBeStock_res.Serial));
            cmd.Parameters.Add(new SqlParameter("@cantidad", oBeStock_res.Cantidad));
            cmd.Parameters.Add(new SqlParameter("@peso", oBeStock_res.Peso));
            cmd.Parameters.Add(new SqlParameter("@estado", oBeStock_res.Estado));
            cmd.Parameters.Add(new SqlParameter("@fecha_ingreso", oBeStock_res.Fecha_ingreso));
            cmd.Parameters.Add(new SqlParameter("@fecha_vence", oBeStock_res.Fecha_vence));
            cmd.Parameters.Add(new SqlParameter("@uds_lic_plate", oBeStock_res.Uds_lic_plate));
            cmd.Parameters.Add(new SqlParameter("@no_bulto", oBeStock_res.No_bulto));
            cmd.Parameters.Add(new SqlParameter("@IdPicking", oBeStock_res.IdPicking));
            cmd.Parameters.Add(new SqlParameter("@IdPedido", oBeStock_res.IdPedido));
            cmd.Parameters.Add(new SqlParameter("@IdDespacho", oBeStock_res.IdDespacho));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeStock_res.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeStock_res.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeStock_res.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeStock_res.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@host", oBeStock_res.Host));
            cmd.Parameters.Add(new SqlParameter("@añada", oBeStock_res.Añada));
            cmd.Parameters.Add(new SqlParameter("@fecha_manufactura", oBeStock_res.Fecha_manufactura));
            cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeStock_res.IdBodega));
            cmd.Parameters.Add(new SqlParameter("@pallet_no_estandar", oBeStock_res.Pallet_no_estandar));

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
            throw new Exception(ex1.Message);
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection is not null) lConnection.Dispose();
            if (lTransaction is not null) lTransaction.Dispose();
        }
        return rowsAffected;
    }

    public static int Insertar(IConfiguration config, clsBeStock_res oBeStock_res)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("stock_res");
            Ins.Add("idstockres", "@idstockres", "F");
            Ins.Add("idtransaccion", "@idtransaccion", "F");
            Ins.Add("indicador", "@indicador", "F");
            Ins.Add("idpedidodet", "@idpedidodet", "F");
            Ins.Add("idstock", "@idstock", "F");
            Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idproductoestado", "@idproductoestado", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idunidadmedida", "@idunidadmedida", "F");
            Ins.Add("idubicacion", "@idubicacion", "F");
            Ins.Add("ubicacion_ant", "@ubicacion_ant", "F");
            Ins.Add("idrecepcion", "@idrecepcion", "F");
            Ins.Add("lote", "@lote", "F");
            Ins.Add("lic_plate", "@lic_plate", "F");
            Ins.Add("serial", "@serial", "F");
            Ins.Add("cantidad", "@cantidad", "F");
            Ins.Add("peso", "@peso", "F");
            Ins.Add("estado", "@estado", "F");
            Ins.Add("fecha_ingreso", "@fecha_ingreso", "F");
            Ins.Add("fecha_vence", "@fecha_vence", "F");
            Ins.Add("uds_lic_plate", "@uds_lic_plate", "F");
            Ins.Add("no_bulto", "@no_bulto", "F");
            Ins.Add("idpicking", "@idpicking", "F");
            Ins.Add("idpedido", "@idpedido", "F");
            Ins.Add("iddespacho", "@iddespacho", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("host", "@host", "F");
            Ins.Add("añada", "@añada", "F");
            Ins.Add("fecha_manufactura", "@fecha_manufactura", "F");
            Ins.Add("idbodega", "@idbodega", "F");
            Ins.Add("pallet_no_estandar", "@pallet_no_estandar", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            cmd.Parameters.Add(new SqlParameter("@IdStockRes", oBeStock_res.IdStockRes));
            cmd.Parameters.Add(new SqlParameter("@IdTransaccion", oBeStock_res.IdTransaccion));
            cmd.Parameters.Add(new SqlParameter("@Indicador", oBeStock_res.Indicador));
            cmd.Parameters.Add(new SqlParameter("@IdPedidoDet", oBeStock_res.IdPedidoDet));
            cmd.Parameters.Add(new SqlParameter("@IdStock", oBeStock_res.IdStock));
            cmd.Parameters.Add(new SqlParameter("@IdPropietarioBodega", oBeStock_res.IdPropietarioBodega));
            cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", oBeStock_res.IdProductoBodega));
            cmd.Parameters.Add(new SqlParameter("@IdProductoEstado", oBeStock_res.IdProductoEstado));
            cmd.Parameters.Add(new SqlParameter("@IdPresentacion", oBeStock_res.IdPresentacion));
            cmd.Parameters.Add(new SqlParameter("@IdUnidadMedida", oBeStock_res.IdUnidadMedida));
            cmd.Parameters.Add(new SqlParameter("@IdUbicacion", oBeStock_res.IdUbicacion));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_ant", oBeStock_res.Ubicacion_ant));
            cmd.Parameters.Add(new SqlParameter("@IdRecepcion", oBeStock_res.IdRecepcion));
            cmd.Parameters.Add(new SqlParameter("@lote", oBeStock_res.Lote));
            cmd.Parameters.Add(new SqlParameter("@lic_plate", oBeStock_res.Lic_plate));
            cmd.Parameters.Add(new SqlParameter("@serial", oBeStock_res.Serial));
            cmd.Parameters.Add(new SqlParameter("@cantidad", oBeStock_res.Cantidad));
            cmd.Parameters.Add(new SqlParameter("@peso", oBeStock_res.Peso));
            cmd.Parameters.Add(new SqlParameter("@estado", oBeStock_res.Estado));
            cmd.Parameters.Add(new SqlParameter("@fecha_ingreso", oBeStock_res.Fecha_ingreso));
            cmd.Parameters.Add(new SqlParameter("@fecha_vence", oBeStock_res.Fecha_vence));
            cmd.Parameters.Add(new SqlParameter("@uds_lic_plate", oBeStock_res.Uds_lic_plate));
            cmd.Parameters.Add(new SqlParameter("@no_bulto", oBeStock_res.No_bulto));
            cmd.Parameters.Add(new SqlParameter("@IdPicking", oBeStock_res.IdPicking));
            cmd.Parameters.Add(new SqlParameter("@IdPedido", oBeStock_res.IdPedido));
            cmd.Parameters.Add(new SqlParameter("@IdDespacho", oBeStock_res.IdDespacho));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeStock_res.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeStock_res.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeStock_res.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeStock_res.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@host", oBeStock_res.Host));
            cmd.Parameters.Add(new SqlParameter("@añada", oBeStock_res.Añada));
            cmd.Parameters.Add(new SqlParameter("@fecha_manufactura", oBeStock_res.Fecha_manufactura));
            cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeStock_res.IdBodega));
            cmd.Parameters.Add(new SqlParameter("@pallet_no_estandar", oBeStock_res.Pallet_no_estandar));

            rowsAffected = cmd.ExecuteNonQuery();

            if (lTransaction != null)
                lTransaction.Commit();

        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();            
            throw new Exception(ex1.Message);
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
        return rowsAffected;
    }

    public static int Actualizar(IConfiguration config, clsBeStock_res oBeStock_res, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("stock_res");
            Upd.Add("idstockres", "@idstockres", "F");
            Upd.Add("idtransaccion", "@idtransaccion", "F");
            Upd.Add("indicador", "@indicador", "F");
            Upd.Add("idpedidodet", "@idpedidodet", "F");
            Upd.Add("idstock", "@idstock", "F");
            Upd.Add("idpropietariobodega", "@idpropietariobodega", "F");
            Upd.Add("idproductobodega", "@idproductobodega", "F");
            Upd.Add("idproductoestado", "@idproductoestado", "F");
            Upd.Add("idpresentacion", "@idpresentacion", "F");
            Upd.Add("idunidadmedida", "@idunidadmedida", "F");
            Upd.Add("idubicacion", "@idubicacion", "F");
            Upd.Add("ubicacion_ant", "@ubicacion_ant", "F");
            Upd.Add("idrecepcion", "@idrecepcion", "F");
            Upd.Add("lote", "@lote", "F");
            Upd.Add("lic_plate", "@lic_plate", "F");
            Upd.Add("serial", "@serial", "F");
            Upd.Add("cantidad", "@cantidad", "F");
            Upd.Add("peso", "@peso", "F");
            Upd.Add("estado", "@estado", "F");
            Upd.Add("fecha_ingreso", "@fecha_ingreso", "F");
            Upd.Add("fecha_vence", "@fecha_vence", "F");
            Upd.Add("uds_lic_plate", "@uds_lic_plate", "F");
            Upd.Add("no_bulto", "@no_bulto", "F");
            Upd.Add("idpicking", "@idpicking", "F");
            Upd.Add("idpedido", "@idpedido", "F");
            Upd.Add("iddespacho", "@iddespacho", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("host", "@host", "F");
            Upd.Add("añada", "@añada", "F");
            Upd.Add("fecha_manufactura", "@fecha_manufactura", "F");
            Upd.Add("idbodega", "@idbodega", "F");
            Upd.Add("pallet_no_estandar", "@pallet_no_estandar", "F");
            Upd.Where("IdStockRes = @IdStockRes");

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

            cmd.Parameters.Add(new SqlParameter("@IdStockRes", oBeStock_res.IdStockRes));
            cmd.Parameters.Add(new SqlParameter("@IdTransaccion", oBeStock_res.IdTransaccion));
            cmd.Parameters.Add(new SqlParameter("@Indicador", oBeStock_res.Indicador));
            cmd.Parameters.Add(new SqlParameter("@IdPedidoDet", oBeStock_res.IdPedidoDet));
            cmd.Parameters.Add(new SqlParameter("@IdStock", oBeStock_res.IdStock));
            cmd.Parameters.Add(new SqlParameter("@IdPropietarioBodega", oBeStock_res.IdPropietarioBodega));
            cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", oBeStock_res.IdProductoBodega));
            cmd.Parameters.Add(new SqlParameter("@IdProductoEstado", oBeStock_res.IdProductoEstado));
            cmd.Parameters.Add(new SqlParameter("@IdPresentacion", oBeStock_res.IdPresentacion));
            cmd.Parameters.Add(new SqlParameter("@IdUnidadMedida", oBeStock_res.IdUnidadMedida));
            cmd.Parameters.Add(new SqlParameter("@IdUbicacion", oBeStock_res.IdUbicacion));
            cmd.Parameters.Add(new SqlParameter("@ubicacion_ant", oBeStock_res.Ubicacion_ant));
            cmd.Parameters.Add(new SqlParameter("@IdRecepcion", oBeStock_res.IdRecepcion));
            cmd.Parameters.Add(new SqlParameter("@lote", oBeStock_res.Lote));
            cmd.Parameters.Add(new SqlParameter("@lic_plate", oBeStock_res.Lic_plate));
            cmd.Parameters.Add(new SqlParameter("@serial", oBeStock_res.Serial));
            cmd.Parameters.Add(new SqlParameter("@cantidad", oBeStock_res.Cantidad));
            cmd.Parameters.Add(new SqlParameter("@peso", oBeStock_res.Peso));
            cmd.Parameters.Add(new SqlParameter("@estado", oBeStock_res.Estado));
            cmd.Parameters.Add(new SqlParameter("@fecha_ingreso", oBeStock_res.Fecha_ingreso));
            cmd.Parameters.Add(new SqlParameter("@fecha_vence", oBeStock_res.Fecha_vence));
            cmd.Parameters.Add(new SqlParameter("@uds_lic_plate", oBeStock_res.Uds_lic_plate));
            cmd.Parameters.Add(new SqlParameter("@no_bulto", oBeStock_res.No_bulto));
            cmd.Parameters.Add(new SqlParameter("@IdPicking", oBeStock_res.IdPicking));
            cmd.Parameters.Add(new SqlParameter("@IdPedido", oBeStock_res.IdPedido));
            cmd.Parameters.Add(new SqlParameter("@IdDespacho", oBeStock_res.IdDespacho));
            cmd.Parameters.Add(new SqlParameter("@user_agr", oBeStock_res.User_agr));
            cmd.Parameters.Add(new SqlParameter("@fec_agr", oBeStock_res.Fec_agr));
            cmd.Parameters.Add(new SqlParameter("@user_mod", oBeStock_res.User_mod));
            cmd.Parameters.Add(new SqlParameter("@fec_mod", oBeStock_res.Fec_mod));
            cmd.Parameters.Add(new SqlParameter("@host", oBeStock_res.Host));
            cmd.Parameters.Add(new SqlParameter("@añada", oBeStock_res.Añada));
            cmd.Parameters.Add(new SqlParameter("@fecha_manufactura", oBeStock_res.Fecha_manufactura));
            cmd.Parameters.Add(new SqlParameter("@IdBodega", oBeStock_res.IdBodega));
            cmd.Parameters.Add(new SqlParameter("@pallet_no_estandar", oBeStock_res.Pallet_no_estandar));

            rowsAffected = cmd.ExecuteNonQuery();

            if (!Es_Transaccion_Remota)
                if (lTransaction != null)
                    lTransaction.Commit();


        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();            
            throw new Exception(ex1.Message);
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
        return rowsAffected;
    }

    public int Eliminar(IConfiguration config, clsBeStock_res oBeStock_res, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Stock_res" +
             "  Where(IdStockRes = @IdStockRes)");

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

            cmd.Parameters.Add(new SqlParameter("@IdStockRes", oBeStock_res.IdStockRes));

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
            throw new Exception(ex1.Message);
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
    }   
    public static bool GetSingle(IConfiguration config, ref clsBeStock_res pBeStock_res)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Stock_res" +
            " Where(IdStockRes = @IdStockRes)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdStockRes", pBeStock_res.IdStockRes));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTransaccion", pBeStock_res.IdTransaccion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@Indicador", pBeStock_res.Indicador));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPedidoDet", pBeStock_res.IdPedidoDet));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdStock", pBeStock_res.IdStock));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPropietarioBodega", pBeStock_res.IdPropietarioBodega));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdProductoBodega", pBeStock_res.IdProductoBodega));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdProductoEstado", pBeStock_res.IdProductoEstado));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPresentacion", pBeStock_res.IdPresentacion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdUnidadMedida", pBeStock_res.IdUnidadMedida));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdUbicacion", pBeStock_res.IdUbicacion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@ubicacion_ant", pBeStock_res.Ubicacion_ant));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdRecepcion", pBeStock_res.IdRecepcion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@lote", pBeStock_res.Lote));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@lic_plate", pBeStock_res.Lic_plate));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@serial", pBeStock_res.Serial));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@cantidad", pBeStock_res.Cantidad));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@peso", pBeStock_res.Peso));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@estado", pBeStock_res.Estado));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fecha_ingreso", pBeStock_res.Fecha_ingreso));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fecha_vence", pBeStock_res.Fecha_vence));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@uds_lic_plate", pBeStock_res.Uds_lic_plate));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@no_bulto", pBeStock_res.No_bulto));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPicking", pBeStock_res.IdPicking));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPedido", pBeStock_res.IdPedido));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdDespacho", pBeStock_res.IdDespacho));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@user_agr", pBeStock_res.User_agr));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_agr", pBeStock_res.Fec_agr));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@user_mod", pBeStock_res.User_mod));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_mod", pBeStock_res.Fec_mod));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@host", pBeStock_res.Host));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@añada", pBeStock_res.Añada));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fecha_manufactura", pBeStock_res.Fecha_manufactura));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdBodega", pBeStock_res.IdBodega));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@pallet_no_estandar", pBeStock_res.Pallet_no_estandar));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeStock_res, r);
                return true;
            }

        }
        catch (SqlException ex1)
        {
            if (lTransaction is not null)
                lTransaction.Rollback();            
            throw new Exception(ex1.Message);
        }
        finally
        {
            if (lConnection.State == ConnectionState.Open) lConnection.Close();
            if (lConnection != null) lConnection.Dispose();
            if (lTransaction != null) lTransaction.Dispose();
        }
        return false;

    }

    public static List<clsBeStock_res> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeStock_res> lreturnList = new List<clsBeStock_res>();

        try
        {
            const string sp = "Select * FROM Stock_res";

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

                        clsBeStock_res vBeStock_res = new clsBeStock_res();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeStock_res = new clsBeStock_res();
                            Cargar(ref vBeStock_res, dr);
                            lreturnList.Add(vBeStock_res);
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
            throw new Exception(ex1.Message);
        }
    }

    public static int MaxID(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;

        try
        {

            int lMax = 0;

            const string sp = "Select ISNULL(Max(IdStockRes),0) FROM Stock_res";

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {
                lConnection.Open();

                using (lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    using (SqlCommand lCommand = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text })
                    {
                        var lreturnValue = lCommand.ExecuteScalar();
                        if (lreturnValue != DBNull.Value && lreturnValue != null)
                        {
                            lMax = int.Parse((string)lreturnValue);
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
            throw new Exception(ex1.Message);
        }
    }
    public static int MaxID(SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {
        int lMax = 0;

        try
        {
            const string sp = "SELECT ISNULL(MAX(IdStockRes), 0) FROM Stock_res";
            
            if (pConection == null || pTransaction == null)
                throw new ArgumentException("Se requiere conexión y transacción para ejecutar esta operación");

            var cmd = new SqlCommand(sp, pConection, pTransaction)
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
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static bool Eliminar_Stock_Reservado_By_IdPedidoDet(int IdPedidoDet,
                                                              SqlConnection lConnection,
                                                              SqlTransaction ltransaction)
    {
        try
        {
            string sp = "DELETE FROM stock_res WHERE IdPedidoDet = @IdPedidoDet";

            int rowsAffected = 0;

            using (SqlCommand cmd = new SqlCommand(sp, lConnection, ltransaction) { CommandType = CommandType.Text })
            {
                cmd.Parameters.Add(new SqlParameter("@IDPEDIDODET", IdPedidoDet));
                rowsAffected = cmd.ExecuteNonQuery();
            }

            return rowsAffected > 0;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static int Get_Count_By_IdPedidoEnc(int IdPedidoEnc, SqlConnection lConnection, SqlTransaction lTransaction)
    {
        int result = 0;

        try
        {
            string vSQL = "SELECT COUNT(1) AS Registros " +
                         "FROM VW_Stock_Reservado_By_IdPedidoEnc " +
                         "WHERE IdPedido = @IdPedidoEnc";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc);

                DataTable lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable != null && lDataTable.Rows.Count > 0)
                {
                    result = lDataTable.Rows[0]["Registros"] is DBNull ? 0 : Convert.ToInt32(lDataTable.Rows[0]["Registros"]);
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return result;
    }

    public static List<clsBeStock_res> Get_All_By_IdPedidoDet(int pIdPedidoDet, int pIdPedidoEnc, SqlConnection pConnection, SqlTransaction pTransaction)
    {
        List<clsBeStock_res> lReturnList = new List<clsBeStock_res>();

        try
        {
            string vSQL = "SELECT * FROM stock_res WHERE IdPedidoDet = @IdPedidoDet AND IdPedido = @IdPedidoEnc";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, pConnection))
            {
                lDTA.SelectCommand.Transaction = pTransaction;
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoDet", pIdPedidoDet);
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc);

                DataTable lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable != null && lDataTable.Rows.Count > 0)
                {
                    foreach (DataRow lRow in lDataTable.Rows)
                    {
                        clsBeStock_res Obj = new clsBeStock_res();
                        Cargar(ref Obj, lRow);
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

    public static double Get_Cantidad_Reservada_By_IdStock(int pIdStock, SqlConnection lConnection, SqlTransaction lTransaction)
    {
        try
        {
            List<clsBeStock_res> lStockRes = Get_All_By_IdStock(pIdStock, lConnection, lTransaction);

            double vTotalReservado = 0;

            foreach (clsBeStock_res StockRes in lStockRes)
            {
                if (StockRes.IdPresentacion != 0)
                {
                    clsBeProducto_presentacion BePres = new clsBeProducto_presentacion();
                    BePres.IdPresentacion = StockRes.IdPresentacion;
                    clsLnProducto_presentacion.GetSingle(ref BePres, lConnection, lTransaction);

                    if (BePres.EsPallet)
                    {
                        vTotalReservado += StockRes.Cantidad / (BePres.Factor * BePres.CajasPorCama * BePres.CamasPorTarima);
                    }
                    else
                    {
                        vTotalReservado += (StockRes.Cantidad / BePres.Factor);
                    }
                }
                else
                {
                    vTotalReservado += StockRes.Cantidad;
                }
            }

            return vTotalReservado;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static List<clsBeStock_res> Get_All_By_IdStock(int pIdStock, SqlConnection lConnection, SqlTransaction? lTransaction)
    {
        try
        {
            List<clsBeStock_res> lReturnList = new List<clsBeStock_res>();
            const string sp = "SELECT * FROM Stock_res WHERE IdSTock = @IdStock ";

            using (SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text })
            using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
            {
                dad.SelectCommand.Parameters.AddWithValue("@IdStock", pIdStock);
                DataTable dt = new DataTable();
                dad.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    clsBeStock_res vBeStock_res = new clsBeStock_res();
                    Cargar(ref vBeStock_res, dr);
                    lReturnList.Add(vBeStock_res);
                }
            }

            return lReturnList;
        }        
        catch (Exception)
        {
            throw;
        }
    }

    public static int Actualizar_IdPickingEnc(clsBeStock_res oBeStock_res, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

        try
        {
            Upd.Init("stock_res");
            Upd.Add("idpicking", "@idpicking", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Where("IdStockRes = @IdStockRes AND IdPedido=@IdPedido");

            string sp = Upd.SQL();

            using (var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text })
            {
                cmd.Parameters.Add(new SqlParameter("@IDSTOCKRES", oBeStock_res.IdStockRes));
                cmd.Parameters.Add(new SqlParameter("@IDPICKING", oBeStock_res.IdPicking));
                cmd.Parameters.Add(new SqlParameter("@IDPEDIDO", oBeStock_res.IdPedido));
                cmd.Parameters.Add(new SqlParameter("@USER_AGR", oBeStock_res.User_agr));
                cmd.Parameters.Add(new SqlParameter("@FEC_AGR", oBeStock_res.Fec_agr));

                rowsAffected = cmd.ExecuteNonQuery();
            }

            return rowsAffected;
        }     
        catch (Exception)
        {
            throw;
        }
    }

    public static double Get_Cantidad_ReservadaEnUMDocumento_By_IdStock(int pIdStock, SqlConnection lConnection, SqlTransaction lTransaction)
    {

        try
        {
            List<clsBeStock_res> lStockRes = Get_All_By_IdStock(pIdStock, lConnection, lTransaction);

            double vTotalReservado = 0;

            foreach (clsBeStock_res StockRes in lStockRes)
            {
                if (StockRes.IdPresentacion != 0)
                {
                    clsBeProducto_presentacion BePres = new clsBeProducto_presentacion();
                    BePres.IdPresentacion = StockRes.IdPresentacion;
                    clsLnProducto_presentacion.GetSingle(ref BePres, lConnection, lTransaction);

                    if (BePres.EsPallet)
                    {
                        vTotalReservado += StockRes.Cantidad / (BePres.Factor * BePres.CajasPorCama * BePres.CamasPorTarima);
                    }
                    else
                    {
                        vTotalReservado += (StockRes.Cantidad / BePres.Factor);
                    }
                }
                else
                {
                    vTotalReservado += StockRes.Cantidad;
                }
            }

            return vTotalReservado;
        }
        catch (Exception)
        {            
            throw;
        }
    }    

    public static bool Get_Cantidad_Y_Peso_ReservadaUMBas_By_IdStock(int pIdStock,
                                                                bool ConvertirPresentacion,
                                                                ref double Cantidad_Reservada,
                                                                ref double Peso_Reservado,
                                                                SqlConnection lConnection,
                                                                SqlTransaction? lTransaction)
    {
        bool result = false;

        try
        {
            List<clsBeStock_res> lStockRes = new List<clsBeStock_res>();
            lStockRes = Get_All_By_IdStock(pIdStock, lConnection, lTransaction);

            double vTotalCantidadReservada = 0;
            double vTotalPesoReservado = 0;

            clsBeProducto_presentacion BePres = new clsBeProducto_presentacion();

            foreach (clsBeStock_res StockRes in lStockRes)
            {
                if (ConvertirPresentacion && (StockRes.IdPresentacion != 0))
                {
                    if (StockRes.IdPresentacion != 0)
                    {
                        BePres = new clsBeProducto_presentacion();
                        BePres.IdPresentacion = StockRes.IdPresentacion;
                        clsLnProducto_presentacion.GetSingle(ref BePres, lConnection, lTransaction);

                        if (BePres.EsPallet)
                        {
                            vTotalCantidadReservada += StockRes.Cantidad / (BePres.Factor * BePres.CajasPorCama * BePres.CamasPorTarima);
                            vTotalPesoReservado += StockRes.Peso / (BePres.Factor * BePres.CajasPorCama * BePres.CamasPorTarima);
                        }
                        else
                        {
                            vTotalCantidadReservada += (StockRes.Cantidad / BePres.Factor);
                            vTotalPesoReservado += (StockRes.Peso / BePres.Factor);
                        }
                    }
                    else
                    {
                        vTotalCantidadReservada += StockRes.Cantidad;
                        vTotalPesoReservado += StockRes.Peso;
                    }
                }
                else
                {
                    vTotalCantidadReservada += StockRes.Cantidad;
                    vTotalPesoReservado += StockRes.Peso;
                }
            }

            Cantidad_Reservada = vTotalCantidadReservada;
            Peso_Reservado = vTotalPesoReservado;

            result = true;
        }
        catch (Exception)
        {            
            throw;
        }

        return result;
    }

    public static List<int> Get_All_By_IdBodega(int pIdBodega, SqlConnection lConnection, SqlTransaction? lTransaction)
    {
        List<int> lReturnValue = new List<int>();

        try
        {
            const string sp = "SELECT IdStock FROM Stock_res WHERE IdBodega = @IdBodega";

            using (SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdBodega", pIdBodega);

                using (SqlDataReader lDataReader = cmd.ExecuteReader())
                {
                    while (lDataReader.Read())
                    {
                        lReturnValue.Add(lDataReader.GetInt32(0));
                    }
                }
            }
        }      
        catch (Exception)
        {
            throw;
        }

        return lReturnValue;
    }

    public static bool Restar_Stock_Reservado(List<clsBeStock> lStock,
                                              clsBeI_nav_config_enc pBeConfigEnc,
                                              SqlConnection lConnection,
                                              SqlTransaction? lTransaction)
    {
        double vCantidadReservadaRef = 0;
        double vPesoReservadoRef = 0;
        double vCantidadEnStockEnPresentacionClavaud = 0;
        double vCantidadProductoPorTarima = 0;
        double vCantidadTarimasCompletasAPickearClavaud = 0;
        double vCantidadEnteraTarimasCompletasClavaud = 0;
        double vCantidadDecimalTarimasCompletasClavaud = 0;

        bool result = false;

        try
        {
            clsBeProducto_presentacion BePresentacionStock = new clsBeProducto_presentacion();

            if (lStock == null) return true;

            List<int> lIdStocksReservados = new List<int>();
            lIdStocksReservados = Get_All_By_IdBodega(pBeConfigEnc.Idbodega, lConnection, lTransaction);

            foreach (clsBeStock BeStock in lStock)
            {
                if (lIdStocksReservados.Contains(BeStock.IdStock))
                {
                    BePresentacionStock = BeStock.Presentacion;

                    if (Get_Cantidad_Y_Peso_ReservadaUMBas_By_IdStock(BeStock.IdStock,
                                                                    false,
                                                                    ref vCantidadReservadaRef,
                                                                    ref vPesoReservadoRef,
                                                                    lConnection,
                                                                    lTransaction))
                    {
                        if (vCantidadReservadaRef != 0)
                        {
                            if ((BeStock.Cantidad_Reservada != 0 && BeStock.Cantidad_Reservada != vCantidadReservadaRef)
                                || BeStock.Cantidad_Reservada == 0)
                            {
                                BeStock.Cantidad_Reservada = vCantidadReservadaRef;
                                BeStock.Pallet_Completo = false;

                                if (BeStock.Cantidad > 0)
                                {
                                    if (vCantidadReservadaRef > BeStock.Cantidad)
                                    {
                                        BeStock.Cantidad = 0;
                                    }
                                    else
                                    {
                                        BeStock.Cantidad -= vCantidadReservadaRef;
                                        BeStock.Peso -= vPesoReservadoRef;
                                    }
                                }
                            }
                        }
                        else
                        {
                            BeStock.Pallet_Completo = true;
                        }

                        if (pBeConfigEnc.considerar_paletizado_en_reabasto)
                        {
                            if (BePresentacionStock != null)
                            {
                                if (BeStock.IdPresentacion == 0)
                                {
                                    BePresentacionStock = new clsBeProducto_presentacion();
                                    BePresentacionStock.IdPresentacion = BeStock.IdPresentacion;
                                    clsLnProducto_presentacion.GetSingle(ref BePresentacionStock, lConnection, lTransaction);
                                }
                                else
                                {
                                    BePresentacionStock = new clsBeProducto_presentacion();
                                    BePresentacionStock.IdPresentacion = BeStock.IdPresentacion;
                                    clsLnProducto_presentacion.GetSingle(ref BePresentacionStock, lConnection, lTransaction);

                                    if (BePresentacionStock != null)
                                    {
                                        if (BePresentacionStock.Factor > 0)
                                        {
                                            if ((BePresentacionStock.CajasPorCama > 0) && (BePresentacionStock.CamasPorTarima > 0))
                                            {
                                                vCantidadProductoPorTarima = Math.Round(BePresentacionStock.CajasPorCama * BePresentacionStock.CamasPorTarima, 2);
                                                vCantidadTarimasCompletasAPickearClavaud = Math.Round(BeStock.Cantidad / vCantidadProductoPorTarima, 2);

                                                Split_Decimal(vCantidadTarimasCompletasAPickearClavaud,
                                                              ref vCantidadEnteraTarimasCompletasClavaud,
                                                              ref vCantidadDecimalTarimasCompletasClavaud);
                                            }
                                        }
                                    }
                                }

                                if(BePresentacionStock !=null)

                                if (BePresentacionStock.Factor > 0)
                                {
                                    if ((BePresentacionStock.CajasPorCama > 0) && (BePresentacionStock.CamasPorTarima > 0))
                                    {
                                        vCantidadProductoPorTarima = Math.Round(BePresentacionStock.CajasPorCama * BePresentacionStock.CamasPorTarima, 2);
                                        vCantidadTarimasCompletasAPickearClavaud = Math.Round(BeStock.Cantidad / vCantidadProductoPorTarima, 2);

                                        Split_Decimal(vCantidadTarimasCompletasAPickearClavaud,
                                                      ref vCantidadEnteraTarimasCompletasClavaud,
                                                      ref vCantidadDecimalTarimasCompletasClavaud);

                                        if ((BePresentacionStock.CajasPorCama > 0) && (BePresentacionStock.CamasPorTarima > 0))
                                        {
                                            vCantidadEnStockEnPresentacionClavaud = Math.Round(BeStock.Cantidad / BePresentacionStock.Factor, 6);

                                            BeStock.Pallet_Completo = (vCantidadEnStockEnPresentacionClavaud == vCantidadProductoPorTarima);

                                            Debug.WriteLine($"El pallet está {(BeStock.Pallet_Completo ? "in" : "")}completo para el el IdStock: {BeStock.IdStock} - {BeStock.Pallet_Completo}");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            result = true;
        }
        catch (Exception)
        {
            throw;
        }

        return result;
    }
    private static void Split_Decimal(double number,
                                      ref double wholePart,
                                      ref double fractionalPart)
    {
        try
        {
            wholePart = Math.Truncate(number);
            fractionalPart = number - wholePart;
        }
        catch (Exception)
        {
            throw;
        }
    } 
}

