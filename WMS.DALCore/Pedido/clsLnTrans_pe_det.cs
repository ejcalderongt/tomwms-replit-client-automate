using System.Reflection;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Pedido;
using Microsoft.Extensions.Configuration;
using WMSWebAPI.Be;
using WMS.DALCore.I_nav_ped_traslado_det;
using WMS.EntityCore.Picking;
using WMS.DALCore.Picking;
using WMS.StockReservation.Compatibility;
public class clsLnTrans_pe_det
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    public static void Cargar(ref clsBeTrans_pe_det oBeTrans_pe_det, DataRow dr)
    {
        int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
        bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
        string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
        DateTime GetDate(string col) { return dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]); }
        double GetDouble(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

        try
        {
            oBeTrans_pe_det.IdPedidoDet = GetInt("IdPedidoDet");
            oBeTrans_pe_det.IdPedidoEnc = GetInt("IdPedidoEnc");
            oBeTrans_pe_det.IdProductoBodega = GetInt("IdProductoBodega");
            oBeTrans_pe_det.IdEstado = GetInt("IdEstado");
            oBeTrans_pe_det.IdPresentacion = GetInt("IdPresentacion");
            oBeTrans_pe_det.IdUnidadMedidaBasica = GetInt("IdUnidadMedidaBasica");
            oBeTrans_pe_det.Cantidad = GetDouble("Cantidad");
            oBeTrans_pe_det.Peso = GetDouble("Peso");
            oBeTrans_pe_det.Precio = GetDouble("Precio");
            oBeTrans_pe_det.No_recepcion = GetInt("no_recepcion");
            oBeTrans_pe_det.Ndias = GetInt("ndias");
            oBeTrans_pe_det.Cant_despachada = GetDouble("cant_despachada");
            oBeTrans_pe_det.Codigo_Producto = GetString("codigo_producto");
            oBeTrans_pe_det.Nombre_producto = GetString("nombre_producto");
            oBeTrans_pe_det.Nom_presentacion = GetString("nom_presentacion");
            oBeTrans_pe_det.Nom_unid_med = GetString("nom_unid_med");
            oBeTrans_pe_det.Nom_estado = GetString("nom_estado");
            oBeTrans_pe_det.User_agr = GetString("user_agr");
            oBeTrans_pe_det.Fec_agr = GetDate("fec_agr");
            oBeTrans_pe_det.Fecha_especifica = GetBool("fecha_especifica");
            oBeTrans_pe_det.RoadDes = GetDouble("RoadDes");
            oBeTrans_pe_det.RoadDesMon = GetDouble("RoadDesMon");
            oBeTrans_pe_det.RoadTotal = GetDouble("RoadTotal");
            oBeTrans_pe_det.RoadPrecioDoc = GetDouble("RoadPrecioDoc");
            oBeTrans_pe_det.RoadVAL1 = GetDouble("RoadVAL1");
            oBeTrans_pe_det.RoadVAL2 = GetString("RoadVAL2");
            oBeTrans_pe_det.RoadCantProc = GetDouble("RoadCantProc");
            oBeTrans_pe_det.Peso_despachado = GetDouble("peso_despachado");
            oBeTrans_pe_det.No_linea = GetInt("no_linea");
            oBeTrans_pe_det.Atributo_variante_1 = GetString("atributo_variante_1");
            oBeTrans_pe_det.IdStockEspecifico = GetInt("IdStockEspecifico");
            oBeTrans_pe_det.EsPadre = GetBool("EsPadre");
            oBeTrans_pe_det.IdPedidoDetPadre = GetInt("IdPedidoDetPadre");
            oBeTrans_pe_det.Peso_Bruto = GetDouble("Peso_Bruto");
            oBeTrans_pe_det.Peso_Neto = GetDouble("Peso_Neto");
            oBeTrans_pe_det.Costo = GetDouble("Costo");
            oBeTrans_pe_det.Valor_aduana = GetDouble("valor_aduana");
            oBeTrans_pe_det.Valor_fob = GetDouble("valor_fob");
            oBeTrans_pe_det.Valor_iva = GetDouble("valor_iva");
            oBeTrans_pe_det.Valor_dai = GetDouble("valor_dai");
            oBeTrans_pe_det.Valor_seguro = GetDouble("valor_seguro");
            oBeTrans_pe_det.Valor_flete = GetDouble("valor_flete");
            oBeTrans_pe_det.Total_linea = GetDouble("Total_linea");
            oBeTrans_pe_det.IdCliente = GetInt("IdCliente");
            oBeTrans_pe_det.IdProductoTallaColor= GetInt("IdProductoTallaColor");
        }
        catch (Exception ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{{0}} {{1}}", currentMethodName, ex.Message);
            
            throw new Exception(vMsgError);
        }
    }

    public static int Insertar(clsBeTrans_pe_det oBeTrans_pe_det, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

        try
        {
            Ins.Init("trans_pe_det");
            Ins.Add("idpedidodet", "@idpedidodet", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idestado", "@idestado", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", "F");
            Ins.Add("cantidad", "@cantidad", "F");
            Ins.Add("peso", "@peso", "F");
            Ins.Add("precio", "@precio", "F");
            Ins.Add("no_recepcion", "@no_recepcion", "F");
            Ins.Add("ndias", "@ndias", "F");
            Ins.Add("cant_despachada", "@cant_despachada", "F");
            Ins.Add("codigo_producto", "@codigo_producto", "F");
            Ins.Add("nombre_producto", "@nombre_producto", "F");
            Ins.Add("nom_presentacion", "@nom_presentacion", "F");
            Ins.Add("nom_unid_med", "@nom_unid_med", "F");
            Ins.Add("nom_estado", "@nom_estado", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("fecha_especifica", "@fecha_especifica", "F");
            Ins.Add("roaddes", "@roaddes", "F");
            Ins.Add("roaddesmon", "@roaddesmon", "F");
            Ins.Add("roadtotal", "@roadtotal", "F");
            Ins.Add("roadpreciodoc", "@roadpreciodoc", "F");
            Ins.Add("roadval1", "@roadval1", "F");
            Ins.Add("roadval2", "@roadval2", "F");
            Ins.Add("roadcantproc", "@roadcantproc", "F");
            Ins.Add("peso_despachado", "@peso_despachado", "F");
            Ins.Add("no_linea", "@no_linea", "F");
            Ins.Add("atributo_variante_1", "@atributo_variante_1", "F");
            Ins.Add("idstockespecifico", "@idstockespecifico", "F");
            Ins.Add("espadre", "@espadre", "F");
            Ins.Add("idpedidodetpadre", "@idpedidodetpadre", "F");
            Ins.Add("peso_bruto", "@peso_bruto", "F");
            Ins.Add("peso_neto", "@peso_neto", "F");
            Ins.Add("costo", "@costo", "F");
            Ins.Add("valor_aduana", "@valor_aduana", "F");
            Ins.Add("valor_fob", "@valor_fob", "F");
            Ins.Add("valor_iva", "@valor_iva", "F");
            Ins.Add("valor_dai", "@valor_dai", "F");
            Ins.Add("valor_seguro", "@valor_seguro", "F");
            Ins.Add("valor_flete", "@valor_flete", "F");
            Ins.Add("total_linea", "@total_linea", "F");
            Ins.Add("idcliente", "@idcliente", "F");

            string sp = Ins.SQL();

            var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            Bind(cmd, oBeTrans_pe_det);

            rowsAffected = cmd.ExecuteNonQuery();

            cmd.Dispose();
        }
        catch (SqlException)
        {
            throw;
        }

        return rowsAffected;
    }

    public static int Insertar_3pl(clsBeTrans_pe_det_3pl oBeTrans_pe_det, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

        try
        {
            Ins.Init("trans_pe_det");
            Ins.Add("idpedidodet", "@idpedidodet", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idestado", "@idestado", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", "F");
            Ins.Add("cantidad", "@cantidad", "F");
            Ins.Add("peso", "@peso", "F");
            Ins.Add("precio", "@precio", "F");
            Ins.Add("no_recepcion", "@no_recepcion", "F");
            Ins.Add("ndias", "@ndias", "F");
            Ins.Add("cant_despachada", "@cant_despachada", "F");
            Ins.Add("codigo_producto", "@codigo_producto", "F");
            Ins.Add("nombre_producto", "@nombre_producto", "F");
            Ins.Add("nom_presentacion", "@nom_presentacion", "F");
            Ins.Add("nom_unid_med", "@nom_unid_med", "F");
            Ins.Add("nom_estado", "@nom_estado", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("fecha_especifica", "@fecha_especifica", "F");
            Ins.Add("roaddes", "@roaddes", "F");
            Ins.Add("roaddesmon", "@roaddesmon", "F");
            Ins.Add("roadtotal", "@roadtotal", "F");
            Ins.Add("roadpreciodoc", "@roadpreciodoc", "F");
            Ins.Add("roadval1", "@roadval1", "F");
            Ins.Add("roadval2", "@roadval2", "F");
            Ins.Add("roadcantproc", "@roadcantproc", "F");
            Ins.Add("peso_despachado", "@peso_despachado", "F");
            Ins.Add("no_linea", "@no_linea", "F");
            Ins.Add("atributo_variante_1", "@atributo_variante_1", "F");
            Ins.Add("idstockespecifico", "@idstockespecifico", "F");
            Ins.Add("espadre", "@espadre", "F");
            Ins.Add("idpedidodetpadre", "@idpedidodetpadre", "F");
            Ins.Add("peso_bruto", "@peso_bruto", "F");
            Ins.Add("peso_neto", "@peso_neto", "F");
            Ins.Add("costo", "@costo", "F");
            Ins.Add("valor_aduana", "@valor_aduana", "F");
            Ins.Add("valor_fob", "@valor_fob", "F");
            Ins.Add("valor_iva", "@valor_iva", "F");
            Ins.Add("valor_dai", "@valor_dai", "F");
            Ins.Add("valor_seguro", "@valor_seguro", "F");
            Ins.Add("valor_flete", "@valor_flete", "F");
            Ins.Add("total_linea", "@total_linea", "F");
            Ins.Add("idcliente", "@idcliente", "F");

            string sp = Ins.SQL();

            var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            Bind_3pl(cmd, oBeTrans_pe_det);

            rowsAffected = cmd.ExecuteNonQuery();

            cmd.Dispose();
        }
        catch (SqlException)
        {
            throw;
        }

        return rowsAffected;
    }
    public static int Insertar(IConfiguration config, clsBeTrans_pe_det oBeTrans_pe_det)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("trans_pe_det");
            Ins.Add("idpedidodet", "@idpedidodet", "F");
            Ins.Add("idpedidoenc", "@idpedidoenc", "F");
            Ins.Add("idproductobodega", "@idproductobodega", "F");
            Ins.Add("idestado", "@idestado", "F");
            Ins.Add("idpresentacion", "@idpresentacion", "F");
            Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", "F");
            Ins.Add("cantidad", "@cantidad", "F");
            Ins.Add("peso", "@peso", "F");
            Ins.Add("precio", "@precio", "F");
            Ins.Add("no_recepcion", "@no_recepcion", "F");
            Ins.Add("ndias", "@ndias", "F");
            Ins.Add("cant_despachada", "@cant_despachada", "F");
            Ins.Add("codigo_producto", "@codigo_producto", "F");
            Ins.Add("nombre_producto", "@nombre_producto", "F");
            Ins.Add("nom_presentacion", "@nom_presentacion", "F");
            Ins.Add("nom_unid_med", "@nom_unid_med", "F");
            Ins.Add("nom_estado", "@nom_estado", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("fecha_especifica", "@fecha_especifica", "F");
            Ins.Add("roaddes", "@roaddes", "F");
            Ins.Add("roaddesmon", "@roaddesmon", "F");
            Ins.Add("roadtotal", "@roadtotal", "F");
            Ins.Add("roadpreciodoc", "@roadpreciodoc", "F");
            Ins.Add("roadval1", "@roadval1", "F");
            Ins.Add("roadval2", "@roadval2", "F");
            Ins.Add("roadcantproc", "@roadcantproc", "F");
            Ins.Add("peso_despachado", "@peso_despachado", "F");
            Ins.Add("no_linea", "@no_linea", "F");
            Ins.Add("atributo_variante_1", "@atributo_variante_1", "F");
            Ins.Add("idstockespecifico", "@idstockespecifico", "F");
            Ins.Add("espadre", "@espadre", "F");
            Ins.Add("idpedidodetpadre", "@idpedidodetpadre", "F");
            Ins.Add("peso_bruto", "@peso_bruto", "F");
            Ins.Add("peso_neto", "@peso_neto", "F");
            Ins.Add("costo", "@costo", "F");
            Ins.Add("valor_aduana", "@valor_aduana", "F");
            Ins.Add("valor_fob", "@valor_fob", "F");
            Ins.Add("valor_iva", "@valor_iva", "F");
            Ins.Add("valor_dai", "@valor_dai", "F");
            Ins.Add("valor_seguro", "@valor_seguro", "F");
            Ins.Add("valor_flete", "@valor_flete", "F");
            Ins.Add("total_linea", "@total_linea", "F");
            Ins.Add("idcliente", "@idcliente", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeTrans_pe_det);

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

    public static int Actualizar(clsBeTrans_pe_det oBeTrans_pe_det, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

        try
        {
            Upd.Init("trans_pe_det");
            Upd.Add("idpedidodet", "@idpedidodet", "F");
            Upd.Add("idpedidoenc", "@idpedidoenc", "F");
            Upd.Add("idproductobodega", "@idproductobodega", "F");
            Upd.Add("idestado", "@idestado", "F");
            Upd.Add("idpresentacion", "@idpresentacion", "F");
            Upd.Add("idunidadmedidabasica", "@idunidadmedidabasica", "F");
            Upd.Add("cantidad", "@cantidad", "F");
            Upd.Add("peso", "@peso", "F");
            Upd.Add("precio", "@precio", "F");
            Upd.Add("no_recepcion", "@no_recepcion", "F");
            Upd.Add("ndias", "@ndias", "F");
            Upd.Add("cant_despachada", "@cant_despachada", "F");
            Upd.Add("codigo_producto", "@codigo_producto", "F");
            Upd.Add("nombre_producto", "@nombre_producto", "F");
            Upd.Add("nom_presentacion", "@nom_presentacion", "F");
            Upd.Add("nom_unid_med", "@nom_unid_med", "F");
            Upd.Add("nom_estado", "@nom_estado", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("fecha_especifica", "@fecha_especifica", "F");
            Upd.Add("roaddes", "@roaddes", "F");
            Upd.Add("roaddesmon", "@roaddesmon", "F");
            Upd.Add("roadtotal", "@roadtotal", "F");
            Upd.Add("roadpreciodoc", "@roadpreciodoc", "F");
            Upd.Add("roadval1", "@roadval1", "F");
            Upd.Add("roadval2", "@roadval2", "F");
            Upd.Add("roadcantproc", "@roadcantproc", "F");
            Upd.Add("peso_despachado", "@peso_despachado", "F");
            Upd.Add("no_linea", "@no_linea", "F");
            Upd.Add("atributo_variante_1", "@atributo_variante_1", "F");
            Upd.Add("idstockespecifico", "@idstockespecifico", "F");
            Upd.Add("espadre", "@espadre", "F");
            Upd.Add("idpedidodetpadre", "@idpedidodetpadre", "F");
            Upd.Add("peso_bruto", "@peso_bruto", "F");
            Upd.Add("peso_neto", "@peso_neto", "F");
            Upd.Add("costo", "@costo", "F");
            Upd.Add("valor_aduana", "@valor_aduana", "F");
            Upd.Add("valor_fob", "@valor_fob", "F");
            Upd.Add("valor_iva", "@valor_iva", "F");
            Upd.Add("valor_dai", "@valor_dai", "F");
            Upd.Add("valor_seguro", "@valor_seguro", "F");
            Upd.Add("valor_flete", "@valor_flete", "F");
            Upd.Add("total_linea", "@total_linea", "F");
            Upd.Add("idcliente", "@idcliente", "F");
            Upd.Where("IdPedidoDet = @IdPedidoDet");

            string sp = Upd.SQL();

            var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            Bind(cmd, oBeTrans_pe_det);

            rowsAffected = cmd.ExecuteNonQuery();

            cmd.Dispose();
        }
        catch (SqlException)
        {          
            throw;
        }

        return rowsAffected;
    }

    public static int Actualizar_3pl(clsBeTrans_pe_det_3pl oBeTrans_pe_det, SqlConnection pConection, SqlTransaction pTransaction)
    {
        int rowsAffected = 0;

        try
        {
            Upd.Init("trans_pe_det");
            Upd.Add("idpedidodet", "@idpedidodet", "F");
            Upd.Add("idpedidoenc", "@idpedidoenc", "F");
            Upd.Add("idproductobodega", "@idproductobodega", "F");
            Upd.Add("idestado", "@idestado", "F");
            Upd.Add("idpresentacion", "@idpresentacion", "F");
            Upd.Add("idunidadmedidabasica", "@idunidadmedidabasica", "F");
            Upd.Add("cantidad", "@cantidad", "F");
            Upd.Add("peso", "@peso", "F");
            Upd.Add("precio", "@precio", "F");
            Upd.Add("no_recepcion", "@no_recepcion", "F");
            Upd.Add("ndias", "@ndias", "F");
            Upd.Add("cant_despachada", "@cant_despachada", "F");
            Upd.Add("codigo_producto", "@codigo_producto", "F");
            Upd.Add("nombre_producto", "@nombre_producto", "F");
            Upd.Add("nom_presentacion", "@nom_presentacion", "F");
            Upd.Add("nom_unid_med", "@nom_unid_med", "F");
            Upd.Add("nom_estado", "@nom_estado", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("fecha_especifica", "@fecha_especifica", "F");
            Upd.Add("roaddes", "@roaddes", "F");
            Upd.Add("roaddesmon", "@roaddesmon", "F");
            Upd.Add("roadtotal", "@roadtotal", "F");
            Upd.Add("roadpreciodoc", "@roadpreciodoc", "F");
            Upd.Add("roadval1", "@roadval1", "F");
            Upd.Add("roadval2", "@roadval2", "F");
            Upd.Add("roadcantproc", "@roadcantproc", "F");
            Upd.Add("peso_despachado", "@peso_despachado", "F");
            Upd.Add("no_linea", "@no_linea", "F");
            Upd.Add("atributo_variante_1", "@atributo_variante_1", "F");
            Upd.Add("idstockespecifico", "@idstockespecifico", "F");
            Upd.Add("espadre", "@espadre", "F");
            Upd.Add("idpedidodetpadre", "@idpedidodetpadre", "F");
            Upd.Add("peso_bruto", "@peso_bruto", "F");
            Upd.Add("peso_neto", "@peso_neto", "F");
            Upd.Add("costo", "@costo", "F");
            Upd.Add("valor_aduana", "@valor_aduana", "F");
            Upd.Add("valor_fob", "@valor_fob", "F");
            Upd.Add("valor_iva", "@valor_iva", "F");
            Upd.Add("valor_dai", "@valor_dai", "F");
            Upd.Add("valor_seguro", "@valor_seguro", "F");
            Upd.Add("valor_flete", "@valor_flete", "F");
            Upd.Add("total_linea", "@total_linea", "F");
            Upd.Add("idcliente", "@idcliente", "F");
            Upd.Where("IdPedidoDet = @IdPedidoDet");

            string sp = Upd.SQL();

            var cmd = new SqlCommand(sp, pConection, pTransaction) { CommandType = CommandType.Text };

            Bind_3pl(cmd, oBeTrans_pe_det);

            rowsAffected = cmd.ExecuteNonQuery();

            cmd.Dispose();
        }
        catch (SqlException)
        {
            throw;
        }

        return rowsAffected;
    }
    public int Eliminar(IConfiguration config, clsBeTrans_pe_det oBeTrans_pe_det, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Trans_pe_det" +
             "  Where(IdPedidoDet = @IdPedidoDet)");

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

            cmd.Parameters.Add(new SqlParameter("@IdPedidoDet", oBeTrans_pe_det.IdPedidoDet));

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
            const string sp = "Select * FROM Trans_pe_det";
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

    public static bool GetSingle(IConfiguration config, ref clsBeTrans_pe_det pBeTrans_pe_det)
    {

        SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Trans_pe_det" +
            " Where(IdPedidoDet = @IdPedidoDet)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPedidoDet", pBeTrans_pe_det.IdPedidoDet));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPedidoEnc", pBeTrans_pe_det.IdPedidoEnc));
            
            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeTrans_pe_det, r);
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

    public static List<clsBeTrans_pe_det> GetAll(IConfiguration config)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_pe_det> lreturnList = new List<clsBeTrans_pe_det>();

        try
        {
            const string sp = "Select * FROM Trans_pe_det";

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

                        clsBeTrans_pe_det vBeTrans_pe_det = new clsBeTrans_pe_det();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_pe_det = new clsBeTrans_pe_det();
                            Cargar(ref vBeTrans_pe_det, dr);
                            lreturnList.Add(vBeTrans_pe_det);
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

            const string sp = "Select ISNULL(Max(IdPedidoDet),0) FROM Trans_pe_det";

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
            const string sp = "Select ISNULL(Max(IdPedidoDet),0) FROM Trans_pe_det";

            SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction);
            cmd.CommandType = CommandType.Text;

            object lreturnValue = cmd.ExecuteScalar();

            if (lreturnValue != DBNull.Value && lreturnValue != null)
            {
                lMax = Convert.ToInt32(lreturnValue);
            }

            return lMax;
        }
        catch (SqlException ex1)
        {            

            throw new Exception(ex1.Message);
        }
    }
    public static void Bind(SqlCommand cmd, clsBeTrans_pe_det o)
    {
        cmd.Parameters.Add(new SqlParameter("@IdPedidoDet", o.IdPedidoDet != 0 ? o.IdPedidoDet : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPedidoEnc", o.IdPedidoEnc != 0 ? o.IdPedidoEnc : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", o.IdProductoBodega != 0 ? o.IdProductoBodega : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdEstado", o.IdEstado != 0 ? o.IdEstado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPresentacion", o.IdPresentacion != 0 ? o.IdPresentacion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdUnidadMedidaBasica", o.IdUnidadMedidaBasica != 0 ? o.IdUnidadMedidaBasica : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Cantidad", o.Cantidad != 0 ? o.Cantidad : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Peso", o.Peso != 0 ? o.Peso : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Precio", o.Precio != 0 ? o.Precio : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@no_recepcion", o.No_recepcion != 0 ? o.No_recepcion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@ndias", o.Ndias != 0 ? o.Ndias : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@cant_despachada", o.Cant_despachada != 0 ? o.Cant_despachada : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@codigo_producto", !string.IsNullOrWhiteSpace(o.Codigo_Producto) ? o.Codigo_Producto : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@nombre_producto", !string.IsNullOrWhiteSpace(o.Nombre_producto) ? o.Nombre_producto : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@nom_presentacion", !string.IsNullOrWhiteSpace(o.Nom_presentacion) ? o.Nom_presentacion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@nom_unid_med", !string.IsNullOrWhiteSpace(o.Nom_unid_med) ? o.Nom_unid_med : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@nom_estado", !string.IsNullOrWhiteSpace(o.Nom_estado) ? o.Nom_estado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_agr", !string.IsNullOrWhiteSpace(o.User_agr) ? o.User_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", o.Fec_agr != DateTime.MinValue ? o.Fec_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_especifica", o.Fecha_especifica)); // bool, se envía directamente
        cmd.Parameters.Add(new SqlParameter("@RoadDes", o.RoadDes != 0 ? o.RoadDes : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadDesMon", o.RoadDesMon != 0 ? o.RoadDesMon : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadTotal", o.RoadTotal != 0 ? o.RoadTotal : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadPrecioDoc", o.RoadPrecioDoc != 0 ? o.RoadPrecioDoc : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadVAL1", o.RoadVAL1 != 0 ? o.RoadVAL1 : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadVAL2", !string.IsNullOrWhiteSpace(o.RoadVAL2) ? o.RoadVAL2 : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadCantProc", o.RoadCantProc != 0 ? o.RoadCantProc : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@peso_despachado", o.Peso_despachado != 0 ? o.Peso_despachado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@no_linea", o.No_linea != 0 ? o.No_linea : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@atributo_variante_1", !string.IsNullOrWhiteSpace(o.Atributo_variante_1) ? o.Atributo_variante_1 : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdStockEspecifico", o.IdStockEspecifico != 0 ? o.IdStockEspecifico : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@EsPadre", o.EsPadre));
        cmd.Parameters.Add(new SqlParameter("@IdPedidoDetPadre", o.IdPedidoDetPadre != 0 ? o.IdPedidoDetPadre : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Peso_Bruto", o.Peso_Bruto != 0 ? o.Peso_Bruto : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Peso_Neto", o.Peso_Neto != 0 ? o.Peso_Neto : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Costo", o.Costo != 0 ? o.Costo : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@valor_aduana", o.Valor_aduana != 0 ? o.Valor_aduana : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@valor_fob", o.Valor_fob != 0 ? o.Valor_fob : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@valor_iva", o.Valor_iva != 0 ? o.Valor_iva : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@valor_dai", o.Valor_dai != 0 ? o.Valor_dai : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@valor_seguro", o.Valor_seguro != 0 ? o.Valor_seguro : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@valor_flete", o.Valor_flete != 0 ? o.Valor_flete : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Total_linea", o.Total_linea != 0 ? o.Total_linea : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdCliente", o.IdCliente != 0 ? o.IdCliente : DBNull.Value));
    }

    public static void Bind_3pl(SqlCommand cmd, clsBeTrans_pe_det_3pl o)
    {
        cmd.Parameters.Add(new SqlParameter("@IdPedidoDet", o.IdPedidoDet != 0 ? o.IdPedidoDet : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPedidoEnc", o.IdPedidoEnc != 0 ? o.IdPedidoEnc : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", o.IdProductoBodega != 0 ? o.IdProductoBodega : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdEstado", o.IdEstado != 0 ? o.IdEstado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdPresentacion", o.IdPresentacion != 0 ? o.IdPresentacion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdUnidadMedidaBasica", o.IdUnidadMedidaBasica != 0 ? o.IdUnidadMedidaBasica : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Cantidad", o.Cantidad != 0 ? o.Cantidad : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Peso", o.Peso != 0 ? o.Peso : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Precio", o.Precio != 0 ? o.Precio : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@no_recepcion", o.No_recepcion != 0 ? o.No_recepcion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@ndias", o.Ndias != 0 ? o.Ndias : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@cant_despachada", o.Cant_despachada != 0 ? o.Cant_despachada : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@codigo_producto", !string.IsNullOrWhiteSpace(o.Codigo_Producto) ? o.Codigo_Producto : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@nombre_producto", !string.IsNullOrWhiteSpace(o.Nombre_producto) ? o.Nombre_producto : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@nom_presentacion", !string.IsNullOrWhiteSpace(o.Nom_presentacion) ? o.Nom_presentacion : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@nom_unid_med", !string.IsNullOrWhiteSpace(o.Nom_unid_med) ? o.Nom_unid_med : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@nom_estado", !string.IsNullOrWhiteSpace(o.Nom_estado) ? o.Nom_estado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@user_agr", !string.IsNullOrWhiteSpace(o.User_agr) ? o.User_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fec_agr", o.Fec_agr != DateTime.MinValue ? o.Fec_agr : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@fecha_especifica", o.Fecha_especifica)); // bool, se envía directamente
        cmd.Parameters.Add(new SqlParameter("@RoadDes", o.RoadDes != 0 ? o.RoadDes : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadDesMon", o.RoadDesMon != 0 ? o.RoadDesMon : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadTotal", o.RoadTotal != 0 ? o.RoadTotal : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadPrecioDoc", o.RoadPrecioDoc != 0 ? o.RoadPrecioDoc : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadVAL1", o.RoadVAL1 != 0 ? o.RoadVAL1 : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadVAL2", !string.IsNullOrWhiteSpace(o.RoadVAL2) ? o.RoadVAL2 : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@RoadCantProc", o.RoadCantProc != 0 ? o.RoadCantProc : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@peso_despachado", o.Peso_despachado != 0 ? o.Peso_despachado : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@no_linea", o.No_linea != 0 ? o.No_linea : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@atributo_variante_1", !string.IsNullOrWhiteSpace(o.Atributo_variante_1) ? o.Atributo_variante_1 : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdStockEspecifico", o.IdStockEspecifico != 0 ? o.IdStockEspecifico : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@EsPadre", o.EsPadre));
        cmd.Parameters.Add(new SqlParameter("@IdPedidoDetPadre", o.IdPedidoDetPadre != 0 ? o.IdPedidoDetPadre : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Peso_Bruto", o.Peso_Bruto != 0 ? o.Peso_Bruto : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Peso_Neto", o.Peso_Neto != 0 ? o.Peso_Neto : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Costo", o.Costo != 0 ? o.Costo : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@valor_aduana", o.Valor_aduana != 0 ? o.Valor_aduana : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@valor_fob", o.Valor_fob != 0 ? o.Valor_fob : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@valor_iva", o.Valor_iva != 0 ? o.Valor_iva : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@valor_dai", o.Valor_dai != 0 ? o.Valor_dai : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@valor_seguro", o.Valor_seguro != 0 ? o.Valor_seguro : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@valor_flete", o.Valor_flete != 0 ? o.Valor_flete : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@Total_linea", o.Total_linea != 0 ? o.Total_linea : DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@IdCliente", o.IdCliente != 0 ? o.IdCliente : DBNull.Value));
    }
    public static int InsertOrUpdate(List<clsBeTrans_pe_det> entities, SqlConnection conn, SqlTransaction tx)
    {
        int total = 0;

        try
        {
            foreach (var entity in entities)
            {
                bool existe = Existe(entity.IdPedidoDet, entity.IdPedidoEnc, conn, tx);
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

    public static int InsertOrUpdate_3pl(List<clsBeTrans_pe_det_3pl> entities, SqlConnection conn, SqlTransaction tx)
    {
        int total = 0;

        try
        {
            foreach (var entity in entities)
            {
                bool existe = Existe(entity.IdPedidoDet, entity.IdPedidoEnc, conn, tx);
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

    public static bool Existe(int idPedidoDet, int idPedidoEnc, SqlConnection conn, SqlTransaction? tx = null)
    {
        const string sql = @"
        SELECT COUNT(1)
        FROM trans_pe_det
        WHERE IdPedidoDet = @IdPedidoDet AND IdPedidoEnc = @IdPedidoEnc";

        using SqlCommand cmd = new(sql, conn, tx);
        cmd.Parameters.AddWithValue("@IdPedidoDet", idPedidoDet);
        cmd.Parameters.AddWithValue("@IdPedidoEnc", idPedidoEnc);
        int count = Convert.ToInt32(cmd.ExecuteScalar());

        return count > 0;
    }    

    public static List<clsBeTrans_pe_det> Get_All_By_IdPedidoEnc(IConfiguration config, int IdPedidoEnc)
    {

        SqlTransaction? lTransaction = null;
        List<clsBeTrans_pe_det> lreturnList = new List<clsBeTrans_pe_det>();

        try
        {
            const string sp = "Select * FROM Trans_pe_det WHERE IdPedidoEnc = @IdPedidoEnc";

            using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {

                lConnection.Open();

                using (lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    using (SqlDataAdapter lDTA = new SqlDataAdapter(sp, lConnection))
                    {
                        lDTA.SelectCommand.CommandType = CommandType.Text;
                        lDTA.SelectCommand.Transaction = lTransaction;
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc);

                        DataTable lDataTable = new DataTable();
                        lDTA.Fill(lDataTable);

                        clsBeTrans_pe_det vBeTrans_pe_det = new clsBeTrans_pe_det();

                        foreach (DataRow dr in lDataTable.Rows)
                        {
                            vBeTrans_pe_det = new clsBeTrans_pe_det();
                            Cargar(ref vBeTrans_pe_det, dr);
                            lreturnList.Add(vBeTrans_pe_det);
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

    public static List<WMS.EntityCore.Dtos.Stock.DetalleReservaDto> Get_Detalles_Reserva_By_IdPedidoEnc(
        int idPedidoEnc,
        SqlConnection conn,
        SqlTransaction? tx)
    {
        var result = new List<WMS.EntityCore.Dtos.Stock.DetalleReservaDto>();

        const string sql = @"
            SELECT
                pd.No_Linea                                                 AS NoLinea,
                p.Codigo                                                    AS ProductCode,
                p.nombre                                                    AS ProductName,
                pd.Cantidad                                                 AS QuantityRequested,
                ISNULL(pp.Factor, 1)                                        AS Factor,
                sr.IdStockRes,
                sr.IdStock,
                s.Lote                                                      AS LotNo,
                s.Fecha_vence                                               AS ExpirationDate,
                dbo.Nombre_Completo_Ubicacion(bu.IdUbicacion, bu.IdBodega)  AS LocationCode,
                CASE WHEN bu.ubicacion_picking = 1 THEN 'Picking' ELSE 'Almacenaje' END AS Zone,
                sr.Cantidad                                                 AS ReservationQty
            FROM trans_pe_det pd
            INNER JOIN producto_bodega pb   ON pd.IdProductoBodega  = pb.IdProductoBodega
            INNER JOIN producto p           ON pb.IdProducto         = p.IdProducto
            LEFT  JOIN producto_presentacion pp ON pd.IdPresentacion = pp.IdPresentacion
            LEFT  JOIN stock_res sr ON sr.IdTransaccion    = @IdPedidoEnc
                                   AND sr.IdProductoBodega  = pd.IdProductoBodega
            LEFT  JOIN stock s          ON sr.IdStock       = s.IdStock
            LEFT  JOIN bodega_ubicacion bu ON sr.IdUbicacion = bu.IdUbicacion
                                          AND sr.IdBodega    = bu.IdBodega
            WHERE pd.IdPedidoEnc = @IdPedidoEnc
            ORDER BY pd.No_Linea, sr.IdStockRes";

        using var cmd = new SqlCommand(sql, conn, tx);
        cmd.Parameters.AddWithValue("@IdPedidoEnc", idPedidoEnc);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var row = new WMS.EntityCore.Dtos.Stock.DetalleReservaDto
            {
                NoLinea           = reader.GetInt32(reader.GetOrdinal("NoLinea")),
                ProductCode       = reader.IsDBNull(reader.GetOrdinal("ProductCode"))       ? "" : reader.GetString(reader.GetOrdinal("ProductCode")),
                ProductName       = reader.IsDBNull(reader.GetOrdinal("ProductName"))       ? "" : reader.GetString(reader.GetOrdinal("ProductName")),
                QuantityRequested = reader.IsDBNull(reader.GetOrdinal("QuantityRequested")) ? 0  : Convert.ToDouble(reader["QuantityRequested"]),
                Factor            = reader.IsDBNull(reader.GetOrdinal("Factor"))            ? 1  : Convert.ToDouble(reader["Factor"]),
                IdStockRes        = reader.IsDBNull(reader.GetOrdinal("IdStockRes"))        ? 0  : reader.GetInt32(reader.GetOrdinal("IdStockRes")),
                IdStock           = reader.IsDBNull(reader.GetOrdinal("IdStock"))           ? 0  : reader.GetInt32(reader.GetOrdinal("IdStock")),
                LotNo             = reader.IsDBNull(reader.GetOrdinal("LotNo"))             ? "" : reader.GetString(reader.GetOrdinal("LotNo")),
                ExpirationDate    = reader.IsDBNull(reader.GetOrdinal("ExpirationDate"))    ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("ExpirationDate")),
                LocationCode      = reader.IsDBNull(reader.GetOrdinal("LocationCode"))      ? "" : reader.GetString(reader.GetOrdinal("LocationCode")),
                Zone              = reader.IsDBNull(reader.GetOrdinal("Zone"))              ? "" : reader.GetString(reader.GetOrdinal("Zone")),
                ReservationQty    = reader.IsDBNull(reader.GetOrdinal("ReservationQty"))    ? 0  : Convert.ToDouble(reader["ReservationQty"])
            };
            result.Add(row);
        }

        return result;
    }

    public static List<WMS.EntityCore.Dtos.Stock.DetalleReservaDto> Get_Detalles_Reserva_By_IdPedidoEnc(
        int idPedidoEnc,
        string noEnc,
        SqlConnection conn,
        SqlTransaction? tx)
    {
        var result = new List<WMS.EntityCore.Dtos.Stock.DetalleReservaDto>();

        const string sql = @"
            SELECT
                pd.No_Linea                                                  AS NoLinea,
                p.Codigo                                                     AS ProductCode,
                p.nombre                                                     AS ProductName,
                pd.Cantidad                                                  AS QuantityRequested,
                ISNULL(pp.Factor, 1)                                         AS Factor,
                sr.IdStockRes,
                sr.IdStock,
                s.Lote                                                       AS LotNo,
                s.Fecha_vence                                                AS ExpirationDate,
                dbo.Nombre_Completo_Ubicacion(bu.IdUbicacion, bu.IdBodega)   AS LocationCode,
                CASE WHEN bu.ubicacion_picking = 1 THEN 'Picking' ELSE 'Almacenaje' END AS Zone,
                sr.Cantidad                                                  AS ReservationQty,
                ISNULL(intf.Process_Result, '')                              AS ProcessResult
            FROM trans_pe_det pd
            INNER JOIN producto_bodega pb    ON pd.IdProductoBodega  = pb.IdProductoBodega
            INNER JOIN producto p            ON pb.IdProducto         = p.IdProducto
            LEFT  JOIN producto_presentacion pp ON pd.IdPresentacion  = pp.IdPresentacion
            LEFT  JOIN stock_res sr ON sr.IdTransaccion    = @IdPedidoEnc
                                   AND sr.IdProductoBodega  = pd.IdProductoBodega
            LEFT  JOIN stock s          ON sr.IdStock       = s.IdStock
            LEFT  JOIN bodega_ubicacion bu ON sr.IdUbicacion = bu.IdUbicacion
                                          AND sr.IdBodega    = bu.IdBodega
            LEFT  JOIN I_nav_ped_traslado_det intf ON intf.NoEnc   = @NoEnc
                                                  AND intf.Line_No = pd.No_Linea
            WHERE pd.IdPedidoEnc = @IdPedidoEnc
            ORDER BY pd.No_Linea, sr.IdStockRes";

        using var cmd = new SqlCommand(sql, conn, tx);
        cmd.Parameters.AddWithValue("@IdPedidoEnc", idPedidoEnc);
        cmd.Parameters.AddWithValue("@NoEnc", string.IsNullOrEmpty(noEnc) ? (object)DBNull.Value : noEnc);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var row = new WMS.EntityCore.Dtos.Stock.DetalleReservaDto
            {
                NoLinea           = reader.GetInt32(reader.GetOrdinal("NoLinea")),
                ProductCode       = reader.IsDBNull(reader.GetOrdinal("ProductCode"))       ? "" : reader.GetString(reader.GetOrdinal("ProductCode")),
                ProductName       = reader.IsDBNull(reader.GetOrdinal("ProductName"))       ? "" : reader.GetString(reader.GetOrdinal("ProductName")),
                QuantityRequested = reader.IsDBNull(reader.GetOrdinal("QuantityRequested")) ? 0  : Convert.ToDouble(reader["QuantityRequested"]),
                Factor            = reader.IsDBNull(reader.GetOrdinal("Factor"))            ? 1  : Convert.ToDouble(reader["Factor"]),
                IdStockRes        = reader.IsDBNull(reader.GetOrdinal("IdStockRes"))        ? 0  : reader.GetInt32(reader.GetOrdinal("IdStockRes")),
                IdStock           = reader.IsDBNull(reader.GetOrdinal("IdStock"))           ? 0  : reader.GetInt32(reader.GetOrdinal("IdStock")),
                LotNo             = reader.IsDBNull(reader.GetOrdinal("LotNo"))             ? "" : reader.GetString(reader.GetOrdinal("LotNo")),
                ExpirationDate    = reader.IsDBNull(reader.GetOrdinal("ExpirationDate"))    ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("ExpirationDate")),
                LocationCode      = reader.IsDBNull(reader.GetOrdinal("LocationCode"))      ? "" : reader.GetString(reader.GetOrdinal("LocationCode")),
                Zone              = reader.IsDBNull(reader.GetOrdinal("Zone"))              ? "" : reader.GetString(reader.GetOrdinal("Zone")),
                ReservationQty    = reader.IsDBNull(reader.GetOrdinal("ReservationQty"))    ? 0  : Convert.ToDouble(reader["ReservationQty"]),
                ProcessResult     = reader.IsDBNull(reader.GetOrdinal("ProcessResult"))     ? "" : reader.GetString(reader.GetOrdinal("ProcessResult"))
            };
            result.Add(row);
        }

        return result;
    }

        public static int Eliminar_Detalle_By_IdPedidoDet(int pIdPedidoEnc,
                                                     int pIdPedidoDet,
                                                     SqlConnection pConection,
                                                     SqlTransaction pTransaction)
    {
        try
        {
            const string sp = "DELETE FROM Trans_pe_det WHERE (IdPedidoDet = @IdPedidoDet AND IdPedidoEnc = @IdPedidoEnc)";

            using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.Add(new SqlParameter("@IDPEDIDODET", pIdPedidoDet));
            cmd.Parameters.Add(new SqlParameter("@IDPEDIDOENC", pIdPedidoEnc));

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static bool Existe(int pIdPedidoEnc,
                             int pNoLinea,
                             ref clsBeTrans_pe_det pBeTrans_pe_det,
                             string CodigoProducto,
                             SqlConnection lConnection,
                             SqlTransaction lTransaction)
    {
        bool result = false;

        try
        {
            string vSQL = "SELECT * FROM trans_pe_det " +
                         "WHERE (IdPedidoEnc = @IdPedidoEnc " +
                         "AND No_Linea = @No_Linea " +
                         "AND codigo_producto = @codigo_producto)";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc);
                lDTA.SelectCommand.Parameters.AddWithValue("@No_Linea", pNoLinea);
                lDTA.SelectCommand.Parameters.AddWithValue("@codigo_producto", CodigoProducto ?? (object)DBNull.Value);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    pBeTrans_pe_det = new clsBeTrans_pe_det();

                    if (lDT.Rows.Count == 1)
                    {
                        Cargar(ref pBeTrans_pe_det, lDT.Rows[0]);
                    }

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
    public static int Get_Count_Lines_By_IdPedidoEnc(int pIdPedidoEnc,
                                                     SqlConnection lConnection,
                                                     SqlTransaction lTransaction)
    {
        try
        {
            int lCount = 0;
            const string sp = "SELECT COUNT(IdPedidoDet) AS cant FROM trans_pe_det WHERE IdPedidoEnc = @IdPedidoEnc";

            using (SqlCommand lCommand = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text })
            {
                lCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc);

                object lReturnValue = lCommand.ExecuteScalar();

                if (lReturnValue != DBNull.Value && lReturnValue != null)
                {
                    lCount = Convert.ToInt32(lReturnValue);
                }
            }

            return lCount;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static int Eliminar_Detalle_By_IdPedidoEnc(int pIdPedidoEnc,
                                                     SqlConnection pConection,
                                                     SqlTransaction pTransaction)
    {
        try
        {
            const string sp = "DELETE FROM Trans_pe_det WHERE (IdPedidoEnc = @IdPedidoEnc)";

            using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.Add(new SqlParameter("@IDPEDIDOENC", pIdPedidoEnc));

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static bool Reservar_Stock_Por_Linea_Interface(double vDiasVencimientoCliente,
                                                          ref clsBeI_nav_ped_traslado_det pBeTrasladoDet,
                                                          ref clsBeTrans_pe_det pBePedidoDet,
                                                          ref clsBeStock_res pBeStockRes,
                                                          string MaquinaQueSolicita,
                                                          clsBeI_nav_config_enc pBeConfigEnc,
                                                          int IdPropietarioBodega,
                                                          ref List<clsBeStock_res> pListStockResOUT,
                                                          ref object plblprg,
                                                          SqlConnection lConnection,
                                                          SqlTransaction lTransaction,
                                                          bool pEsManufactura = false)
    {
        bool result = false;

        lTransaction.Save("Init_Stock");

        try
        {
            int ResultadoInsert = 0;
            clsBeI_nav_ped_traslado_det pBeTrasladoTemp = new clsBeI_nav_ped_traslado_det();

            pBeTrasladoTemp.Item_No = pBeTrasladoDet.No;
            pBeTrasladoTemp.Line_No = pBeTrasladoDet.Line_No;
            pBeTrasladoTemp.NoEnc = pBeTrasladoDet.NoEnc;
            clsLnI_nav_ped_traslado_det.GetSingle(pBeTrasladoTemp,lConnection, lTransaction);

            if (pBePedidoDet.IsNew)
            {
                pBePedidoDet.IdPedidoDet = MaxID(lConnection, lTransaction) + 1;
                pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet;

                if (pBeTrasladoDet.Variant_Code != pBeTrasladoTemp.Variant_Code && pBeTrasladoTemp.Unit_of_Measure_Code != pBePedidoDet.Nom_presentacion)
                {
                    if (pBeTrasladoDet != null)
                    {
                        if (pBePedidoDet.IdPresentacion != 0)
                        {
                            pBePedidoDet.Cantidad = Math.Ceiling(Math.Round(pBeTrasladoDet.Quantity * pBePedidoDet.Factor, 2));
                            pBePedidoDet.Nom_presentacion = "";
                            pBePedidoDet.IdPresentacion = 0;
                        }
                    }
                }

                ResultadoInsert = Insertar(pBePedidoDet, lConnection, lTransaction);
            }
            else
            {
                if (!Existe(pBePedidoDet.IdPedidoDet, pBePedidoDet.IdPedidoEnc, lConnection, lTransaction))
                {
                    pBePedidoDet.IdPedidoDet = MaxID(lConnection, lTransaction) + 1;
                    pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet;
                    ResultadoInsert = Insertar(pBePedidoDet, lConnection, lTransaction);
                }
                else
                {
                    pBePedidoDet.ListaStockRes = clsLnStock_res.Get_All_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                       pBePedidoDet.IdPedidoEnc,
                                                                                       lConnection, lTransaction);
                    pBePedidoDet.ListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                                               pBePedidoDet.IdPedidoEnc,
                                                                                                               lConnection,
                                                                                                               lTransaction);
                    ResultadoInsert = Actualizar(pBePedidoDet, lConnection, lTransaction);
                }
            }
            
            if (pBePedidoDet.ListaStockRes != null && pBePedidoDet.ListaStockRes.Count > 0 && pBeTrasladoDet !=null)
            {
                double vCantidadReservada = 0;
                double vDifPedidoVrsReservado = 0;
                int vIdPickingEnc = 0;

                foreach (var Sr in pBePedidoDet.ListaStockRes)
                {
                    vCantidadReservada = clsLnStock_res.Get_Cantidad_Reservada_By_IdStock(Sr.IdStock,
                                                                                          lConnection,
                                                                                          lTransaction);
                    vDifPedidoVrsReservado = pBePedidoDet.Cantidad - vCantidadReservada;

                    switch (vDifPedidoVrsReservado)
                    {
                        case > 0:
                            pBeStockRes.Cantidad -= vCantidadReservada;

                            double Qty_received = 0;

                            if (StockReservationFacade.Reserva_Stock_From_MI3(ref pBeStockRes,
                                                                              vDiasVencimientoCliente,
                                                                              MaquinaQueSolicita,
                                                                              pBeConfigEnc,
                                                                              ref Qty_received,
                                                                              IdPropietarioBodega,
                                                                              ref pListStockResOUT,
                                                                              lConnection,
                                                                              lTransaction,
                                                                              pBeTrasladoDet.Line_No,
                                                                              false,
                                                                              pBeTrasladoDet,
                                                                              pBePedidoDet,
                                                                              pEsManufactura))
                            {
                                pBeTrasladoDet.Qty_to_Receive = Qty_received;
                                var firstPicking = pBePedidoDet.ListaPickingUbic?.FirstOrDefault();
                                if (firstPicking != null)
                                {
                                    vIdPickingEnc = firstPicking.IdPickingEnc;

                                    if (Actualiza_Picking_Existente(pBeStockRes,
                                                                   pBePedidoDet,
                                                                   vIdPickingEnc,
                                                                   lConnection,
                                                                   lTransaction))
                                    {
                                        result = true;
                                    }
                                }
                            }
                            else
                            {
                                result = false;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                double Qty_received2 = 0;
                if (pBeTrasladoDet != null)      

                if (StockReservationFacade.Reserva_Stock_From_MI3(ref pBeStockRes,
                                                                     vDiasVencimientoCliente,
                                                                     MaquinaQueSolicita,
                                                                     pBeConfigEnc,
                                                                     ref Qty_received2,
                                                                     IdPropietarioBodega,
                                                                     ref pListStockResOUT,
                                                                     lConnection,
                                                                     lTransaction,
                                                                     pBeTrasladoDet.Line_No,
                                                                     false,
                                                                     pBeTrasladoDet,
                                                                     pBePedidoDet,
                                                                     pEsManufactura))
                    {
                        result = true;
                        pBeTrasladoDet.Qty_to_Receive = Qty_received2;
                    }
                    else
                    {
                        result = false;
                    }
            }
        }
        catch (Exception)
        {
            if (lTransaction != null) lTransaction.Rollback("Init_Stock");
            throw;
        }

        return result;
    }

    public static bool Actualiza_Picking_Existente(clsBeStock_res SR,
                                                   clsBeTrans_pe_det pBePedidoDet,
                                                   int pIdPickingEnc,
                                                   SqlConnection lConnection,
                                                   SqlTransaction lTransaction)
    {
        bool result = false;

        try
        {
            bool vDetalleActualizadoCorrectamente = false;
            clsBeTrans_picking_ubic? BePickingUbicActual;
            clsBeTrans_picking_det? BePickingDetActual = new clsBeTrans_picking_det();
            int vIdPickingEnc = pIdPickingEnc;
            int vIdPickingDet;
            int vIdStock = SR.IdStock;
            int vIdPedidoDet = pBePedidoDet.IdPedidoDet;

            if (vIdPickingEnc > 0)
            {
                BePickingUbicActual = pBePedidoDet.ListaPickingUbic?
                    .Find(x => x.IdStock == vIdStock && x.IdPedidoDet == vIdPedidoDet);

                if (BePickingUbicActual != null)
                {
                    BePickingUbicActual.Cantidad_solicitada = pBePedidoDet.Cantidad;

                    if (clsLnTrans_picking_ubic.Actualizar(BePickingUbicActual, lConnection, lTransaction) > 0)
                    {
                        BePickingDetActual = clsLnTrans_picking_det.GetSingle(pBePedidoDet.IdPedidoDet, lConnection, lTransaction);

                        if (BePickingDetActual != null)
                        {
                            BePickingDetActual.Cantidad = pBePedidoDet.Cantidad;

                            if (clsLnTrans_picking_det.Actualizar(BePickingDetActual, lConnection, lTransaction) > 0)
                            {
                                vDetalleActualizadoCorrectamente = true;
                            }
                        }
                    }
                }
                else
                {
                    if (clsLnTrans_picking_det.Insertar_PickingDet(pBePedidoDet, vIdPickingEnc, out vIdPickingDet, lConnection, lTransaction))
                    {
                        if (clsLnTrans_picking_ubic.Insertar_PickingUbic(SR, vIdPickingDet, lConnection, lTransaction))
                        {
                            if (clsLnTrans_picking_det_parametros.Insertar_Parametros_Stock_Para_Picking(SR.IdStock, vIdPickingDet, lConnection, lTransaction))
                            {
                                vDetalleActualizadoCorrectamente = true;
                            }
                        }
                    }
                }
            }

            result = vDetalleActualizadoCorrectamente;
        }
        catch (Exception)
        {            
            throw;
        }

        return result;
    }

    public static bool Reservar_Stock_Por_Linea_Interface(double vDiasVencimientoCliente,
                                                          ref clsBeI_nav_ped_traslado_det pBeTrasladoDet,
                                                          ref clsBeTrans_pe_det pBePedidoDet,
                                                          ref clsBeStock_res pBeStockRes,
                                                          string MaquinaQueSolicita,
                                                          clsBeI_nav_config_enc pBeConfigEnc,
                                                          int IdPropietarioBodega,
                                                          SqlConnection lConnection,
                                                          SqlTransaction lTransaction,
                                                          bool pEsManufactura = false)
    {
        bool result = false;

        lTransaction.Save("Init_Stock");

        try
        {
            int ResultadoInsert = 0;

            if (pBePedidoDet.IsNew)
            {
                pBePedidoDet.IdPedidoDet = MaxID(lConnection, lTransaction) + 1;
                pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet;
                ResultadoInsert = Insertar(pBePedidoDet, lConnection, lTransaction);
            }
            else
            {
                if (!Existe(pBePedidoDet.IdPedidoDet, pBePedidoDet.IdPedidoEnc, lConnection, lTransaction))
                {
                    pBePedidoDet.IdPedidoDet = MaxID(lConnection, lTransaction) + 1;
                    pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet;
                    ResultadoInsert = Insertar(pBePedidoDet, lConnection, lTransaction);
                }
                else
                {
                    pBePedidoDet.ListaStockRes = clsLnStock_res.Get_All_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                       pBePedidoDet.IdPedidoEnc,
                                                                                       lConnection,
                                                                                       lTransaction);

                    pBePedidoDet.ListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                                               pBePedidoDet.IdPedidoEnc,
                                                                                                               lConnection,
                                                                                                               lTransaction);
                    ResultadoInsert = Actualizar(pBePedidoDet,
                                                 lConnection,
                                                 lTransaction);
                }
            }

            if (pBePedidoDet.ListaStockRes != null && pBePedidoDet.ListaStockRes.Count > 0)
            {
                double vCantidadReservadaEnUMDocumento = 0;
                double vDifPedidoVrsReservado = 0;
                int vIdPickingEnc = 0;
                List<clsBeStock_res> pListStockResOUT = new List<clsBeStock_res>();

                foreach (var Sr in pBePedidoDet.ListaStockRes)
                {
                    vCantidadReservadaEnUMDocumento = clsLnStock_res.Get_Cantidad_ReservadaEnUMDocumento_By_IdStock(Sr.IdStock,
                                                                                                                    lConnection,
                                                                                                                    lTransaction);

                    // El pedido, puede venir en dos variantes:
                    // 1. Variante 1: El pedido viene en UMBas.
                    // 2. Variante 2: El pedido viene en Presentación.
                    //
                    // Caso de uso #1: El pedido viene en umBas, el objeto pBeStockRes.Idpresentacion = 0.
                    // Caso de uso #2: El pedido viene en Presentación, el objeto pBeStockRes.Idpresentacion <> 0.

                    vDifPedidoVrsReservado = pBePedidoDet.Cantidad - vCantidadReservadaEnUMDocumento;

                    switch (vDifPedidoVrsReservado)
                    {
                        case > 0: // Se aumentó la cantidad en el pedido, 'Por lo tanto se debe aumentar la cantidad en picking.
                            pBeStockRes.Cantidad -= vCantidadReservadaEnUMDocumento;

                            double Qty_received = 0;

                            if (StockReservationFacade.Reserva_Stock_From_MI3(ref pBeStockRes,
                                                                             vDiasVencimientoCliente,
                                                                             MaquinaQueSolicita,
                                                                             pBeConfigEnc,
                                                                             ref Qty_received,
                                                                             IdPropietarioBodega,
                                                                             ref pListStockResOUT,
                                                                             lConnection,
                                                                             lTransaction,
                                                                             pBePedidoDet: ref pBePedidoDet,
                                                                             pEsManufactura: pEsManufactura))
                            {
                                
                                pBeTrasladoDet.Qty_to_Receive = Qty_received;

                                if (pBePedidoDet.ListaPickingUbic?.FirstOrDefault() is clsBeTrans_picking_ubic firstPickingUbic)
                                {
                                    vIdPickingEnc = firstPickingUbic.IdPickingEnc;

                                    if (Actualiza_Picking_Existente(pBeStockRes,
                                                                   pBePedidoDet,
                                                                   vIdPickingEnc,
                                                                   lConnection,
                                                                   lTransaction))
                                    {
                                        result = true;
                                    }
                                }
                            }
                            else
                            {
                                result = false;
                            }
                            break;

                        case < 0: // Se disminuyó la cantidad en el pedido
                            //foreach (var Pu in pBePedidoDet.ListaPickingUbic.Where(x => (x.Cantidad_verificada - x.Cantidad_despachada) > 0))
                            //{
                            //    Debug.Print(Pu.IdPickingUbic.ToString());
                            //}
                            break;

                        default:
                            break;
                    }
                }
            }
            else
            {
                List<clsBeStock_res> pListStockResOUT = new List<clsBeStock_res>();

                double Qty_received = 0;

                if (StockReservationFacade.Reserva_Stock_From_MI3(ref pBeStockRes,
                                                                vDiasVencimientoCliente,
                                                                MaquinaQueSolicita,
                                                                pBeConfigEnc,
                                                                ref Qty_received,
                                                                IdPropietarioBodega,
                                                                ref pListStockResOUT,
                                                                lConnection,
                                                                lTransaction,
                                                                0,
                                                                false,
                                                                pBeTrasladoDet,
                                                                pBePedidoDet,
                                                                pEsManufactura))
                {
                    result = true;
                    Qty_received = pBeTrasladoDet.Qty_to_Receive;
                }
                else
                {
                    result = false;
                }
            }
        }
        catch (Exception)
        {
            if (lTransaction != null) lTransaction.Rollback("Init_Stock");
            throw;
        }

        return result;
    }

    public static int Get_Count_Lines_By_IdPedidoEnc(int pIdPedidoEnc, IConfiguration config)
    {
        try
        {
            int lCount = 0;

            using (var lConnection = new SqlConnection(config.GetConnectionString("CST")))
            {
                lConnection.Open();

                using (var ltransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    const string sp = "SELECT count(IdPedidoDet) cant FROM trans_pe_det WHERE IdPedidoEnc=@IdPedidoEnc";

                    using (var lCommand = new SqlCommand(sp, lConnection, ltransaction) { CommandType = CommandType.Text })
                    {
                        lCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc);

                        var lReturnValue = lCommand.ExecuteScalar();

                        if (lReturnValue != DBNull.Value && lReturnValue != null)
                        {
                            lCount = Convert.ToInt32(lReturnValue);
                        }
                    }

                    ltransaction.Commit();
                }

                lConnection.Close();
            }

            return lCount;
        }       
        catch (Exception)
        {
            throw;
        }
    }
}