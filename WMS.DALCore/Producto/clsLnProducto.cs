using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Producto;
using Microsoft.Extensions.Configuration;
public class clsLnProducto
{

    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();
    public static void Cargar(ref clsBeProducto oBeProducto, DataRow dr)
    {
        try
        {
            int GetInt(string col) => dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]);
            bool GetBool(string col) => dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]);
            string GetString(string col) => dr[col] is DBNull ? "" : Convert.ToString(dr[col]) ?? "";
            DateTime GetDate(string col) => dr[col] is DBNull ? DateTime.Now : Convert.ToDateTime(dr[col]);
            byte[] GetBytes(string col) => dr[col] is DBNull ? Array.Empty<byte>() : (byte[])dr[col];
            decimal GetDecimal(string col) => dr[col] is DBNull ? 0 : Convert.ToDecimal(dr[col]);

            oBeProducto.IdProducto = GetInt("IdProducto");
            oBeProducto.IdPropietario = GetInt("IdPropietario");
            oBeProducto.IdClasificacion = GetInt("IdClasificacion");
            oBeProducto.IdFamilia = GetInt("IdFamilia");
            oBeProducto.IdMarca = GetInt("IdMarca");
            oBeProducto.IdTipoProducto = GetInt("IdTipoProducto");
            oBeProducto.IdUnidadMedidaBasica = GetInt("IdUnidadMedidaBasica");
            oBeProducto.IdCamara = GetInt("IdCamara");
            oBeProducto.IdTipoRotacion = GetInt("IdTipoRotacion");
            oBeProducto.IdPerfilSerializado = GetInt("IdPerfilSerializado");
            oBeProducto.IdIndiceRotacion = GetInt("IdIndiceRotacion");
            oBeProducto.IdSimbologia = GetInt("IdSimbologia");
            oBeProducto.IdArancel = GetInt("IdArancel");

            oBeProducto.codigo = GetString("codigo");
            oBeProducto.nombre = GetString("nombre");
            oBeProducto.codigo_barra = GetString("codigo_barra");

            oBeProducto.precio = GetDecimal("precio");
            oBeProducto.existencia_min = GetDecimal("existencia_min");
            oBeProducto.existencia_max = GetDecimal("existencia_max");
            oBeProducto.costo = GetDecimal("costo");
            oBeProducto.peso_referencia = GetDecimal("peso_referencia");
            oBeProducto.peso_tolerancia = GetDecimal("peso_tolerancia");
            oBeProducto.temperatura_referencia = GetDecimal("temperatura_referencia");
            oBeProducto.temperatura_tolerancia = GetDecimal("temperatura_tolerancia");

            oBeProducto.activo = GetBool("activo");
            oBeProducto.serializado = GetBool("serializado");
            oBeProducto.genera_lote = GetBool("genera_lote");
            oBeProducto.genera_lp_old = GetBool("genera_lp_old");
            oBeProducto.control_vencimiento = GetBool("control_vencimiento");
            oBeProducto.control_lote = GetBool("control_lote");
            oBeProducto.peso_recepcion = GetBool("peso_recepcion");
            oBeProducto.peso_despacho = GetBool("peso_despacho");
            oBeProducto.temperatura_recepcion = GetBool("temperatura_recepcion");
            oBeProducto.temperatura_despacho = GetBool("temperatura_despacho");
            oBeProducto.materia_prima = GetBool("materia_prima");
            oBeProducto.kit = GetBool("kit");

            oBeProducto.tolerancia = GetInt("tolerancia");
            oBeProducto.ciclo_vida = GetInt("ciclo_vida");

            oBeProducto.user_agr = GetString("user_agr");
            oBeProducto.fec_agr = GetDate("fec_agr");
            oBeProducto.user_mod = GetString("user_mod");
            oBeProducto.fec_mod = GetDate("fec_mod");

            oBeProducto.imagen = GetBytes("imagen");
            oBeProducto.noserie = GetString("noserie");
            oBeProducto.noparte = GetString("noparte");

            oBeProducto.fechamanufactura = GetBool("fechamanufactura");
            oBeProducto.capturar_aniada = GetBool("capturar_aniada");
            oBeProducto.control_peso = GetBool("control_peso");
            oBeProducto.captura_arancel = GetBool("captura_arancel");
            oBeProducto.es_hardware = GetBool("es_hardware");

            oBeProducto.largo = GetDecimal("largo");
            oBeProducto.alto = GetDecimal("alto");
            oBeProducto.ancho = GetDecimal("ancho");

            oBeProducto.IdUnidadMedidaCobro = GetInt("IdUnidadMedidaCobro");
            oBeProducto.IdTipoEtiqueta = GetInt("IdTipoEtiqueta");
            oBeProducto.dias_inventario_promedio = GetInt("dias_inventario_promedio");
            oBeProducto.IDPRODUCTOPARAMETROA = GetInt("IDPRODUCTOPARAMETROA");
            oBeProducto.IDPRODUCTOPARAMETROB = GetInt("IDPRODUCTOPARAMETROB");
            oBeProducto.IdTipoManufactura = GetInt("IdTipoManufactura");
        }
        catch (Exception ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethod = sf?.GetMethod();

            string msg = $"{currentMethod?.Name ?? "Cargar"}: {ex.Message}";
            throw new Exception(msg, ex);
        }
    }
    public static int Insertar(IConfiguration configuration, clsBeProducto oBeProducto, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(configuration.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("producto");
            Ins.Add("idproducto", "@idproducto", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("idclasificacion", "@idclasificacion", "F");
            Ins.Add("idfamilia", "@idfamilia", "F");
            Ins.Add("idmarca", "@idmarca", "F");
            if (oBeProducto.IdTipoProducto > 0) {
                
            }
            Ins.Add("idtipoproducto", "@idtipoproducto", "F");
            Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", "F");
            Ins.Add("idcamara", "@idcamara", "F");
            Ins.Add("idtiporotacion", "@idtiporotacion", "F");
            Ins.Add("idperfilserializado", "@idperfilserializado", "F");
            Ins.Add("idindicerotacion", "@idindicerotacion", "F");
            Ins.Add("idsimbologia", "@idsimbologia", "F");
            Ins.Add("idarancel", "@idarancel", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("codigo_barra", "@codigo_barra", "F");
            Ins.Add("precio", "@precio", "F");
            Ins.Add("existencia_min", "@existencia_min", "F");
            Ins.Add("existencia_max", "@existencia_max", "F");
            Ins.Add("costo", "@costo", "F");
            Ins.Add("peso_referencia", "@peso_referencia", "F");
            Ins.Add("peso_tolerancia", "@peso_tolerancia", "F");
            Ins.Add("temperatura_referencia", "@temperatura_referencia", "F");
            Ins.Add("temperatura_tolerancia", "@temperatura_tolerancia", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("serializado", "@serializado", "F");
            Ins.Add("genera_lote", "@genera_lote", "F");
            Ins.Add("genera_lp_old", "@genera_lp_old", "F");
            Ins.Add("control_vencimiento", "@control_vencimiento", "F");
            Ins.Add("control_lote", "@control_lote", "F");
            Ins.Add("peso_recepcion", "@peso_recepcion", "F");
            Ins.Add("peso_despacho", "@peso_despacho", "F");
            Ins.Add("temperatura_recepcion", "@temperatura_recepcion", "F");
            Ins.Add("temperatura_despacho", "@temperatura_despacho", "F");
            Ins.Add("materia_prima", "@materia_prima", "F");
            Ins.Add("kit", "@kit", "F");
            Ins.Add("tolerancia", "@tolerancia", "F");
            Ins.Add("ciclo_vida", "@ciclo_vida", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("imagen", "@imagen", "F");
            Ins.Add("noserie", "@noserie", "F");
            Ins.Add("noparte", "@noparte", "F");
            Ins.Add("fechamanufactura", "@fechamanufactura", "F");
            Ins.Add("capturar_aniada", "@capturar_aniada", "F");
            Ins.Add("control_peso", "@control_peso", "F");
            Ins.Add("captura_arancel", "@captura_arancel", "F");
            Ins.Add("es_hardware", "@es_hardware", "F");
            Ins.Add("largo", "@largo", "F");
            Ins.Add("alto", "@alto", "F");
            Ins.Add("ancho", "@ancho", "F");
            Ins.Add("idunidadmedidacobro", "@idunidadmedidacobro", "F");
            Ins.Add("idtipoetiqueta", "@idtipoetiqueta", "F");
            Ins.Add("dias_inventario_promedio", "@dias_inventario_promedio", "F");
            Ins.Add("idproductoparametroa", "@idproductoparametroa", "F");
            Ins.Add("idproductoparametrob", "@idproductoparametrob", "F");
            Ins.Add("idtipomanufactura", "@idtipomanufactura", "F");

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

            Bind(cmd, oBeProducto);

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
    public static int Insertar(IConfiguration configuration, clsBeProducto oBeProducto)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(configuration.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            Ins.Init("producto");
            Ins.Add("idproducto", "@idproducto", "F");
            Ins.Add("idpropietario", "@idpropietario", "F");
            Ins.Add("idclasificacion", "@idclasificacion", "F");
            Ins.Add("idfamilia", "@idfamilia", "F");
            Ins.Add("idmarca", "@idmarca", "F");
            Ins.Add("idtipoproducto", "@idtipoproducto", "F");
            Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", "F");
            Ins.Add("idcamara", "@idcamara", "F");
            Ins.Add("idtiporotacion", "@idtiporotacion", "F");
            Ins.Add("idperfilserializado", "@idperfilserializado", "F");
            Ins.Add("idindicerotacion", "@idindicerotacion", "F");
            Ins.Add("idsimbologia", "@idsimbologia", "F");
            Ins.Add("idarancel", "@idarancel", "F");
            Ins.Add("codigo", "@codigo", "F");
            Ins.Add("nombre", "@nombre", "F");
            Ins.Add("codigo_barra", "@codigo_barra", "F");
            Ins.Add("precio", "@precio", "F");
            Ins.Add("existencia_min", "@existencia_min", "F");
            Ins.Add("existencia_max", "@existencia_max", "F");
            Ins.Add("costo", "@costo", "F");
            Ins.Add("peso_referencia", "@peso_referencia", "F");
            Ins.Add("peso_tolerancia", "@peso_tolerancia", "F");
            Ins.Add("temperatura_referencia", "@temperatura_referencia", "F");
            Ins.Add("temperatura_tolerancia", "@temperatura_tolerancia", "F");
            Ins.Add("activo", "@activo", "F");
            Ins.Add("serializado", "@serializado", "F");
            Ins.Add("genera_lote", "@genera_lote", "F");
            Ins.Add("genera_lp_old", "@genera_lp_old", "F");
            Ins.Add("control_vencimiento", "@control_vencimiento", "F");
            Ins.Add("control_lote", "@control_lote", "F");
            Ins.Add("peso_recepcion", "@peso_recepcion", "F");
            Ins.Add("peso_despacho", "@peso_despacho", "F");
            Ins.Add("temperatura_recepcion", "@temperatura_recepcion", "F");
            Ins.Add("temperatura_despacho", "@temperatura_despacho", "F");
            Ins.Add("materia_prima", "@materia_prima", "F");
            Ins.Add("kit", "@kit", "F");
            Ins.Add("tolerancia", "@tolerancia", "F");
            Ins.Add("ciclo_vida", "@ciclo_vida", "F");
            Ins.Add("user_agr", "@user_agr", "F");
            Ins.Add("fec_agr", "@fec_agr", "F");
            Ins.Add("user_mod", "@user_mod", "F");
            Ins.Add("fec_mod", "@fec_mod", "F");
            Ins.Add("imagen", "@imagen", "F");
            Ins.Add("noserie", "@noserie", "F");
            Ins.Add("noparte", "@noparte", "F");
            Ins.Add("fechamanufactura", "@fechamanufactura", "F");
            Ins.Add("capturar_aniada", "@capturar_aniada", "F");
            Ins.Add("control_peso", "@control_peso", "F");
            Ins.Add("captura_arancel", "@captura_arancel", "F");
            Ins.Add("es_hardware", "@es_hardware", "F");
            Ins.Add("largo", "@largo", "F");
            Ins.Add("alto", "@alto", "F");
            Ins.Add("ancho", "@ancho", "F");
            Ins.Add("idunidadmedidacobro", "@idunidadmedidacobro", "F");
            Ins.Add("idtipoetiqueta", "@idtipoetiqueta", "F");
            Ins.Add("dias_inventario_promedio", "@dias_inventario_promedio", "F");
            Ins.Add("idproductoparametroa", "@idproductoparametroa", "F");
            Ins.Add("idproductoparametrob", "@idproductoparametrob", "F");
            Ins.Add("idtipomanufactura", "@idtipomanufactura", "F");

            string sp = Ins.SQL();

            SqlCommand cmd = new SqlCommand() { CommandType = CommandType.Text };

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            cmd = new SqlCommand(sp, lConnection, lTransaction);

            Bind(cmd, oBeProducto);

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
    public static int Actualizar(IConfiguration configuration, clsBeProducto oBeProducto, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        int rowsAffected = 0;
        SqlConnection lConnection = new SqlConnection(configuration.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            Upd.Init("producto");
            Upd.Add("idproducto", "@idproducto", "F");
            Upd.Add("idpropietario", "@idpropietario", "F");
            Upd.Add("idclasificacion", "@idclasificacion", "F");
            Upd.Add("idfamilia", "@idfamilia", "F");
            Upd.Add("idmarca", "@idmarca", "F");
            Upd.Add("idtipoproducto", "@idtipoproducto", "F");
            Upd.Add("idunidadmedidabasica", "@idunidadmedidabasica", "F");
            Upd.Add("idcamara", "@idcamara", "F");
            Upd.Add("idtiporotacion", "@idtiporotacion", "F");
            Upd.Add("idperfilserializado", "@idperfilserializado", "F");
            Upd.Add("idindicerotacion", "@idindicerotacion", "F");
            Upd.Add("idsimbologia", "@idsimbologia", "F");
            Upd.Add("idarancel", "@idarancel", "F");
            Upd.Add("codigo", "@codigo", "F");
            Upd.Add("nombre", "@nombre", "F");
            Upd.Add("codigo_barra", "@codigo_barra", "F");
            Upd.Add("precio", "@precio", "F");
            Upd.Add("existencia_min", "@existencia_min", "F");
            Upd.Add("existencia_max", "@existencia_max", "F");
            Upd.Add("costo", "@costo", "F");
            Upd.Add("peso_referencia", "@peso_referencia", "F");
            Upd.Add("peso_tolerancia", "@peso_tolerancia", "F");
            Upd.Add("temperatura_referencia", "@temperatura_referencia", "F");
            Upd.Add("temperatura_tolerancia", "@temperatura_tolerancia", "F");
            Upd.Add("activo", "@activo", "F");
            Upd.Add("serializado", "@serializado", "F");
            Upd.Add("genera_lote", "@genera_lote", "F");
            Upd.Add("genera_lp_old", "@genera_lp_old", "F");
            Upd.Add("control_vencimiento", "@control_vencimiento", "F");
            Upd.Add("control_lote", "@control_lote", "F");
            Upd.Add("peso_recepcion", "@peso_recepcion", "F");
            Upd.Add("peso_despacho", "@peso_despacho", "F");
            Upd.Add("temperatura_recepcion", "@temperatura_recepcion", "F");
            Upd.Add("temperatura_despacho", "@temperatura_despacho", "F");
            Upd.Add("materia_prima", "@materia_prima", "F");
            Upd.Add("kit", "@kit", "F");
            Upd.Add("tolerancia", "@tolerancia", "F");
            Upd.Add("ciclo_vida", "@ciclo_vida", "F");
            Upd.Add("user_agr", "@user_agr", "F");
            Upd.Add("fec_agr", "@fec_agr", "F");
            Upd.Add("user_mod", "@user_mod", "F");
            Upd.Add("fec_mod", "@fec_mod", "F");
            Upd.Add("imagen", "@imagen", "F");
            Upd.Add("noserie", "@noserie", "F");
            Upd.Add("noparte", "@noparte", "F");
            Upd.Add("fechamanufactura", "@fechamanufactura", "F");
            Upd.Add("capturar_aniada", "@capturar_aniada", "F");
            Upd.Add("control_peso", "@control_peso", "F");
            Upd.Add("captura_arancel", "@captura_arancel", "F");
            Upd.Add("es_hardware", "@es_hardware", "F");
            Upd.Add("largo", "@largo", "F");
            Upd.Add("alto", "@alto", "F");
            Upd.Add("ancho", "@ancho", "F");
            Upd.Add("idunidadmedidacobro", "@idunidadmedidacobro", "F");
            Upd.Add("idtipoetiqueta", "@idtipoetiqueta", "F");
            Upd.Add("dias_inventario_promedio", "@dias_inventario_promedio", "F");
            Upd.Add("idproductoparametroa", "@idproductoparametroa", "F");
            Upd.Add("idproductoparametrob", "@idproductoparametrob", "F");
            Upd.Add("idtipomanufactura", "@idtipomanufactura", "F");
            Upd.Where("IdProducto = @IdProducto");

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

            Bind(cmd, oBeProducto);

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
    public int Eliminar(clsBeProducto oBeProducto, IConfiguration configuration, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    {

        SqlConnection lConnection = new SqlConnection(configuration.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = (" Delete from Producto" +
             "  Where(IdProducto = @IdProducto)");

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

            cmd.Parameters.Add(new SqlParameter("@IdProducto", oBeProducto.IdProducto));

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
    public DataTable Listar(IConfiguration configuration)
    {

        SqlConnection lConnection = new SqlConnection(configuration.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {
            const string sp = "Select * FROM Producto";
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
    public static bool GetSingle(ref clsBeProducto pBeProducto, IConfiguration configuration)
    {

        SqlConnection lConnection = new SqlConnection(configuration.GetConnectionString("CST"));
        SqlTransaction? lTransaction = null;

        try
        {

            const string sp = "Select * FROM Producto" +
            " Where(IdProducto = @IdProducto)";

            lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

            SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
            SqlDataAdapter dad = new SqlDataAdapter(cmd);

            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdProducto", pBeProducto.IdProducto));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPropietario", pBeProducto.IdPropietario));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdClasificacion", pBeProducto.IdClasificacion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdFamilia", pBeProducto.IdFamilia));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdMarca", pBeProducto.IdMarca));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTipoProducto", pBeProducto.IdTipoProducto));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdUnidadMedidaBasica", pBeProducto.IdUnidadMedidaBasica));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdCamara", pBeProducto.IdCamara));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTipoRotacion", pBeProducto.IdTipoRotacion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdPerfilSerializado", pBeProducto.IdPerfilSerializado));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdIndiceRotacion", pBeProducto.IdIndiceRotacion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdSimbologia", pBeProducto.IdSimbologia));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdArancel", pBeProducto.IdArancel));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@codigo", pBeProducto.codigo));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@nombre", pBeProducto.nombre));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@codigo_barra", pBeProducto.codigo_barra));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@precio", pBeProducto.precio));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@existencia_min", pBeProducto.existencia_min));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@existencia_max", pBeProducto.existencia_max));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@costo", pBeProducto.costo));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@peso_referencia", pBeProducto.peso_referencia));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@peso_tolerancia", pBeProducto.peso_tolerancia));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@temperatura_referencia", pBeProducto.temperatura_referencia));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@temperatura_tolerancia", pBeProducto.temperatura_tolerancia));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@activo", pBeProducto.activo));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@serializado", pBeProducto.serializado));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@genera_lote", pBeProducto.genera_lote));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@genera_lp_old", pBeProducto.genera_lp_old));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@control_vencimiento", pBeProducto.control_vencimiento));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@control_lote", pBeProducto.control_lote));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@peso_recepcion", pBeProducto.peso_recepcion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@peso_despacho", pBeProducto.peso_despacho));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@temperatura_recepcion", pBeProducto.temperatura_recepcion));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@temperatura_despacho", pBeProducto.temperatura_despacho));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@materia_prima", pBeProducto.materia_prima));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@kit", pBeProducto.kit));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@tolerancia", pBeProducto.tolerancia));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@ciclo_vida", pBeProducto.ciclo_vida));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@user_agr", pBeProducto.user_agr));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_agr", pBeProducto.fec_agr));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@user_mod", pBeProducto.user_mod));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fec_mod", pBeProducto.fec_mod));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@imagen", pBeProducto.imagen));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@noserie", pBeProducto.noserie));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@noparte", pBeProducto.noparte));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@fechamanufactura", pBeProducto.fechamanufactura));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@capturar_aniada", pBeProducto.capturar_aniada));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@control_peso", pBeProducto.control_peso));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@captura_arancel", pBeProducto.captura_arancel));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@es_hardware", pBeProducto.es_hardware));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@largo", pBeProducto.largo));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@alto", pBeProducto.alto));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@ancho", pBeProducto.ancho));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdUnidadMedidaCobro", pBeProducto.IdUnidadMedidaCobro));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTipoEtiqueta", pBeProducto.IdTipoEtiqueta));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@dias_inventario_promedio", pBeProducto.dias_inventario_promedio));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IDPRODUCTOPARAMETROA", pBeProducto.IDPRODUCTOPARAMETROA));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IDPRODUCTOPARAMETROB", pBeProducto.IDPRODUCTOPARAMETROB));
            dad.SelectCommand.Parameters.Add(new SqlParameter("@IdTipoManufactura", pBeProducto.IdTipoManufactura));

            DataTable dt = new DataTable();
            dad.Fill(dt);

            lTransaction.Commit();

            if (dt.Rows.Count == 1)
            {
                DataRow r;
                r = dt.Rows[0];
                Cargar(ref pBeProducto, r);
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
    public static List<clsBeProducto> GetAll(IConfiguration configuration)
    {
        const string sp = "SELECT * FROM Producto";
        var lreturnList = new List<clsBeProducto>();

        var lConnection = new SqlConnection(configuration.GetConnectionString("CST"));
        lConnection.Open();
        using var lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
        using var cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var row = reader.ToDataRow();
            var producto = new clsBeProducto();
            Cargar(ref producto, row);
            lreturnList.Add(producto);
        }

        lTransaction.Commit();
        return lreturnList;
    }
    public static int MaxID(IConfiguration configuration)
    {
        SqlTransaction? lTransaction = null;

        try
        {
            int lMax = 0;
            const string sp = "SELECT ISNULL(MAX(IdProducto), 0) FROM Producto";

            using (SqlConnection lConnection = new SqlConnection(configuration.GetConnectionString("CST")))
            {
                lConnection.Open();
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

                using (SqlCommand lCommand = new SqlCommand(sp, lConnection, lTransaction))
                {
                    object lreturnValue = lCommand.ExecuteScalar();

                    if (lreturnValue != DBNull.Value && lreturnValue != null)
                    {
                        lMax = Convert.ToInt32(lreturnValue);
                    }
                }

                lTransaction.Commit();
            }

            return lMax;
        }
        catch (SqlException ex1)
        {
            if (lTransaction != null)
            {
                try { lTransaction.Rollback(); } catch { /* opcional: log de rollback fallido */ }
            }

            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{0} {1}", currentMethodName?.Name ?? "UnknownMethod", ex1.Message);

            throw new Exception(vMsgError, ex1);
        }
    }
    //public static int MaxID(IConfiguration configuration, SqlConnection? pConection = null, SqlTransaction? pTransaction = null)
    //{
    //    SqlConnection? lConnection = null;
    //    SqlTransaction? lTransaction = null;
    //    int lMax = 0;

    //    try
    //    {
    //        const string sp = "SELECT ISNULL(MAX(IdProducto), 0) FROM Producto";
    //        bool esTransaccionRemota = pConection is not null && pTransaction is not null;

    //        SqlCommand cmd;

    //        if (esTransaccionRemota)
    //        {
    //            cmd = new SqlCommand(sp, pConection, pTransaction);
    //        }
    //        else
    //        {
    //            lConnection = new SqlConnection(configuration.GetConnectionString("CST"));
    //            lConnection.Open();
    //            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
    //            cmd = new SqlCommand(sp, lConnection, lTransaction);
    //        }

    //        object lreturnValue = cmd.ExecuteScalar();

    //        if (lreturnValue != DBNull.Value && lreturnValue != null)
    //        {
    //            lMax = Convert.ToInt32(lreturnValue);
    //        }

    //        if (!esTransaccionRemota && lTransaction is not null)
    //        {
    //            lTransaction.Commit();
    //        }

    //        return lMax;
    //    }
    //    catch (SqlException ex1)
    //    {
    //        if (lTransaction is not null)
    //        {
    //            try { lTransaction.Rollback(); } catch { /* Opcional: manejo de rollback fallido */ }
    //        }

    //        var st = new StackTrace();
    //        var sf = st.GetFrame(0);
    //        MethodBase? currentMethodName = sf?.GetMethod();
    //        string vMsgError = string.Format("{0} {1}", currentMethodName?.Name ?? "UnknownMethod", ex1.Message);

    //        throw new Exception(vMsgError, ex1);
    //    }
    //    finally
    //    {
    //        if (lConnection is not null && lConnection.State == ConnectionState.Open)
    //        {
    //            lConnection.Close();
    //        }
    //    }
    //}


    public static int MaxID(IConfiguration config, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        const string sql = "SELECT ISNULL(MAX(IdProducto), 0) FROM Producto";
        bool externa = conn != null && tx != null;

        var lConn = externa ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? lTx = null;
        if (!externa) { lConn.Open(); lTx = lConn.BeginTransaction(); }

        try
        {
            using var cmd = new SqlCommand(sql, lConn, externa ? tx! : lTx!);
            var result = cmd.ExecuteScalar();
            if (!externa) lTx?.Commit();
            return Convert.ToInt32(result);
        }
        catch
        {
            if (!externa) lTx?.Rollback();
            throw;
        }
        finally
        {
            if (!externa) lConn.Close();
        }
    }

    //public static bool Existe(int IdProducto, SqlConnection pConnection, SqlTransaction pTransaction)
    //{
    //    try
    //    {
    //        const string query = "SELECT COUNT(1) FROM Producto WHERE IdProducto = @IdProducto";

    //        using (SqlCommand cmd = new SqlCommand(query, pConnection, pTransaction))
    //        {
    //            cmd.CommandType = CommandType.Text;
    //            cmd.Parameters.Add(new SqlParameter("@IdProducto", IdProducto));

    //            object result = cmd.ExecuteScalar();
    //            int count = Convert.ToInt32(result);

    //            return count > 0;
    //        }
    //    }
    //    catch (SqlException ex)
    //    {
    //        var st = new StackTrace();
    //        var sf = st.GetFrame(0);
    //        MethodBase? currentMethodName = sf?.GetMethod();
    //        string vMsgError = string.Format("{0} {1}", currentMethodName?.Name ?? "UnknownMethod", ex.Message);

    //        throw new Exception(vMsgError, ex);
    //    }
    //}
    public static int InsertOrUpdate(IConfiguration config, clsBeProducto entity, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool isExternalTx = conn != null && tx != null;

        var connection = isExternalTx ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;

        try
        {
            if (!isExternalTx)
            {
                connection.Open();
                localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            bool existe = Existe(entity.IdProducto, connection, isExternalTx ? tx! : localTx!);

            return existe
                ? Actualizar(config, entity, connection, isExternalTx ? tx : localTx)
                : Insertar(config, entity, connection, isExternalTx ? tx : localTx);
        }
        catch (SqlException ex)
        {
            if (!isExternalTx && localTx is not null)
                localTx.Rollback();

            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
        }
        finally
        {
            if (!isExternalTx)
            {
                connection.Close();
                connection.Dispose();
                localTx?.Dispose();
            }
        }
    }
    public static void Bind(SqlCommand cmd, clsBeProducto o)
    {
        cmd.Parameters.AddWithValue("@IdProducto", o.IdProducto);
        cmd.Parameters.AddWithValue("@IdPropietario", o.IdPropietario == 0 ? DBNull.Value : o.IdPropietario);
        cmd.Parameters.AddWithValue("@IdClasificacion", o.IdClasificacion == 0 ? DBNull.Value : o.IdClasificacion);
        cmd.Parameters.AddWithValue("@IdFamilia", o.IdFamilia == 0 ? DBNull.Value : o.IdFamilia);
        cmd.Parameters.AddWithValue("@IdMarca", o.IdMarca == 0 ? DBNull.Value : o.IdMarca);
        cmd.Parameters.AddWithValue("@IdTipoProducto", o.IdTipoProducto == 0 ? DBNull.Value : o.IdTipoProducto);
        cmd.Parameters.AddWithValue("@IdUnidadMedidaBasica", o.IdUnidadMedidaBasica == 0 ? DBNull.Value : o.IdUnidadMedidaBasica);
        cmd.Parameters.AddWithValue("@IdCamara", o.IdCamara == 0 ? DBNull.Value : o.IdCamara);
        cmd.Parameters.AddWithValue("@IdTipoRotacion", o.IdTipoRotacion == 0 ? DBNull.Value : o.IdTipoRotacion);
        cmd.Parameters.AddWithValue("@IdPerfilSerializado", o.IdPerfilSerializado == 0 ? DBNull.Value : o.IdPerfilSerializado);
        cmd.Parameters.AddWithValue("@IdIndiceRotacion", o.IdIndiceRotacion == 0 ? DBNull.Value : o.IdIndiceRotacion);
        cmd.Parameters.AddWithValue("@IdSimbologia", o.IdSimbologia == 0 ? DBNull.Value : o.IdSimbologia);
        cmd.Parameters.AddWithValue("@IdArancel", o.IdArancel == 0 ? DBNull.Value : o.IdArancel);
        cmd.Parameters.AddWithValue("@codigo", o.codigo ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@nombre", o.nombre ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@codigo_barra", o.codigo_barra ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@precio", o.precio);
        cmd.Parameters.AddWithValue("@existencia_min", o.existencia_min);
        cmd.Parameters.AddWithValue("@existencia_max", o.existencia_max);
        cmd.Parameters.AddWithValue("@costo", o.costo);
        cmd.Parameters.AddWithValue("@peso_referencia", o.peso_referencia);
        cmd.Parameters.AddWithValue("@peso_tolerancia", o.peso_tolerancia);
        cmd.Parameters.AddWithValue("@temperatura_referencia", o.temperatura_referencia);
        cmd.Parameters.AddWithValue("@temperatura_tolerancia", o.temperatura_tolerancia);
        cmd.Parameters.AddWithValue("@activo", o.activo);
        cmd.Parameters.AddWithValue("@serializado", o.serializado);
        cmd.Parameters.AddWithValue("@genera_lote", o.genera_lote);
        cmd.Parameters.AddWithValue("@genera_lp_old", o.genera_lp_old);
        cmd.Parameters.AddWithValue("@control_vencimiento", o.control_vencimiento);
        cmd.Parameters.AddWithValue("@control_lote", o.control_lote);
        cmd.Parameters.AddWithValue("@peso_recepcion", o.peso_recepcion);
        cmd.Parameters.AddWithValue("@peso_despacho", o.peso_despacho);
        cmd.Parameters.AddWithValue("@temperatura_recepcion", o.temperatura_recepcion);
        cmd.Parameters.AddWithValue("@temperatura_despacho", o.temperatura_despacho);
        cmd.Parameters.AddWithValue("@materia_prima", o.materia_prima);
        cmd.Parameters.AddWithValue("@kit", o.kit);
        cmd.Parameters.AddWithValue("@tolerancia", o.tolerancia);
        cmd.Parameters.AddWithValue("@ciclo_vida", o.ciclo_vida);
        cmd.Parameters.AddWithValue("@user_agr", o.user_agr ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@fec_agr", o.fec_agr);
        cmd.Parameters.AddWithValue("@user_mod", o.user_mod ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@fec_mod", o.fec_mod);
        cmd.Parameters.Add("@imagen", SqlDbType.Image).Value = o.imagen ?? (object)DBNull.Value;
        cmd.Parameters.AddWithValue("@noserie", o.noserie ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@noparte", o.noparte ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@fechamanufactura", o.fechamanufactura);
        cmd.Parameters.AddWithValue("@capturar_aniada", o.capturar_aniada);
        cmd.Parameters.AddWithValue("@control_peso", o.control_peso);
        cmd.Parameters.AddWithValue("@captura_arancel", o.captura_arancel);
        cmd.Parameters.AddWithValue("@es_hardware", o.es_hardware);
        cmd.Parameters.AddWithValue("@largo", o.largo);
        cmd.Parameters.AddWithValue("@alto", o.alto);
        cmd.Parameters.AddWithValue("@ancho", o.ancho);
        cmd.Parameters.AddWithValue("@IdUnidadMedidaCobro", o.IdUnidadMedidaCobro == 0 ? DBNull.Value : o.IdUnidadMedidaCobro);
        cmd.Parameters.AddWithValue("@IdTipoEtiqueta", o.IdTipoEtiqueta == 0 ? DBNull.Value : o.IdTipoEtiqueta);
        cmd.Parameters.AddWithValue("@dias_inventario_promedio", o.dias_inventario_promedio);
        cmd.Parameters.AddWithValue("@IDPRODUCTOPARAMETROA", o.IDPRODUCTOPARAMETROA == 0 ? DBNull.Value : o.IDPRODUCTOPARAMETROA);
        cmd.Parameters.AddWithValue("@IDPRODUCTOPARAMETROB", o.IDPRODUCTOPARAMETROB == 0 ? DBNull.Value : o.IDPRODUCTOPARAMETROB);
        cmd.Parameters.AddWithValue("@IdTipoManufactura", o.IdTipoManufactura == 0 ? DBNull.Value : o.IdTipoManufactura);
    }
    public static bool Existe(int IdProducto, SqlConnection pConnection, SqlTransaction pTransaction)
    {
        try
        {
            const string query = "SELECT COUNT(1) FROM producto WHERE IdProducto= @IdProducto";

            using (SqlCommand cmd = new SqlCommand(query, pConnection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@IdProducto", IdProducto));

                object result = cmd.ExecuteScalar();
                int count = Convert.ToInt32(result);

                return count > 0;
            }
        }
        catch (SqlException ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{0} {1}", currentMethodName?.Name ?? "UnknownMethod", ex.Message);

            throw new Exception(vMsgError, ex);
        }
    }


    public static bool Existe_By_Codigo(string pCodigo, SqlConnection pConnection, SqlTransaction pTransaction)
    {
        try
        {
            const string query = "SELECT COUNT(1) FROM producto WHERE codigo= @codigo";

            using (SqlCommand cmd = new SqlCommand(query, pConnection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@codigo", pCodigo));

                object result = cmd.ExecuteScalar();
                int count = Convert.ToInt32(result);

                return count > 0;
            }
        }
        catch (SqlException ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            string vMsgError = string.Format("{0} {1}", currentMethodName?.Name ?? "UnknownMethod", ex.Message);

            throw new Exception(vMsgError, ex);
        }
    }

    public static void Valida_Atributos(IConfiguration config, clsBeProductoMhs entity, SqlConnection? conn = null, SqlTransaction? tx = null)
    {
        bool isExternalTx = conn != null && tx != null;
        var connection = isExternalTx ? conn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? localTx = null;

        try
        {
            if (!isExternalTx)
            {
                connection.Open();
                localTx = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }

            bool existe = Existe_By_Codigo(entity.codigo, connection, isExternalTx ? tx! : localTx!);

            if (!existe)
            {

                var Clasificacion = new clsBeProducto_clasificacion();
                var Familia = new clsBeProducto_familia();
                var Marca = new clsBeProducto_marca();

                if (!string.IsNullOrEmpty(entity.CodigoClasificacion)) 
                {   
                    Clasificacion.IdClasificacion = clsLnProducto_clasificacion.MaxID(config, connection, isExternalTx ? tx : localTx) + 1;
                    Clasificacion.Codigo = entity.CodigoClasificacion;
                    Clasificacion.Nombre = entity.nombre;
                    Clasificacion.User_agr = "1";
                    Clasificacion.Fec_agr = DateTime.Now;
                    Clasificacion.Fec_mod = DateTime.Now;
                    Clasificacion.Activo = true;
                    Clasificacion.IdPropietario = entity.IdPropietario;
                    clsLnProducto_clasificacion.Insertar(config, Clasificacion, connection, isExternalTx ? tx : localTx);
                }


                if (!string.IsNullOrEmpty(entity.CodigoFamilia)) 
                {
                    Familia.IdFamilia = clsLnProducto_familia.MaxID(config, connection, isExternalTx ? tx : localTx)+1;
                    Familia.Codigo = entity.CodigoFamilia;
                    Familia.Nombre = entity.nombre;
                    Familia.User_agr = "1";
                    Familia.Fec_agr = DateTime.Now;
                    Familia.Fec_agr = DateTime.Now;
                    Familia.Activo = true;
                    Familia.IdPropietario = entity.IdPropietario;
                    clsLnProducto_familia.Insertar(config, Familia, connection, isExternalTx ? tx : localTx);
                }

                if (!string.IsNullOrEmpty(entity.CodigoMarca)) 
                {
                    Marca.IdMarca = clsLnProductoMarca.MaxId(config, connection, isExternalTx ? tx : localTx) + 1;
                    Marca.Codigo = entity.CodigoMarca;
                    Marca.Nombre = entity.nombre;
                    Marca.User_agr = "1";
                    Marca.Fec_agr = DateTime.Now;
                    Marca.Fec_mod = DateTime.Now;
                    Marca.Activo = true;
                    Marca.IdPropietario = entity.IdPropietario; //los propietarios deben ser sincronizados previamente.
                    clsLnProductoMarca.Insert(config, Marca, connection, isExternalTx ? tx : localTx);
                }

               
                var pProducto = new clsBeProducto();
                pProducto.IdProducto = MaxID(config, connection, isExternalTx ? tx : localTx)+1;
                pProducto.nombre = entity.nombre;
                pProducto.IdPropietario = entity.IdPropietario;
                pProducto.IdClasificacion = Clasificacion.IdClasificacion;
                pProducto.IdFamilia = Familia.IdFamilia;
                pProducto.IdMarca = Marca.IdMarca;
                pProducto.IdTipoProducto = entity.IdTipoProducto;
                pProducto.IdUnidadMedidaBasica = entity.IdUnidadMedidaBasica;
                pProducto.IdSimbologia = entity.IdSimbologia;
                pProducto.codigo = entity.codigo;
                pProducto.nombre = entity.nombre;
                pProducto.codigo_barra = entity.codigo_barra;
                pProducto.activo = true;
                //
                pProducto.genera_lp_old = entity.genera_lp_old;
                pProducto.control_lote = entity.control_lote;
                pProducto.control_peso = entity.control_peso;
                pProducto.control_vencimiento= entity.control_vencimiento;
                pProducto.IdTipoRotacion = entity.IdTipoRotacion;
                pProducto.user_agr = "1";
                pProducto.user_mod = "1";
                pProducto.fec_agr = DateTime.Now;
                pProducto.fec_mod= DateTime.Now;  


                Insertar(config, pProducto, connection, isExternalTx ? tx : localTx);
            }

        }
        catch (SqlException ex)
        {
            if (!isExternalTx && localTx is not null)
                localTx.Rollback();

            var method = new StackTrace().GetFrame(0)?.GetMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
        }
        finally
        {
            if (!isExternalTx)
            {
                connection.Close();
                connection.Dispose();
                localTx?.Dispose();
            }
        }
    }

}