using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WMS.EntityCore.Producto;
using Microsoft.Extensions.Configuration;
public class clsLnProducto_bodega
{
    private static clsInsert Ins = new clsInsert();
    private static clsUpdate Upd = new clsUpdate();

    private static SqlCommand CrearComando(string sp, SqlConnection conn, SqlTransaction? tran, clsBeProducto_bodega p)
    {
        var cmd = new SqlCommand(sp, conn, tran) { CommandType = CommandType.Text };

        cmd.Parameters.AddRange(new[]
        {
            new SqlParameter("@IdProductoBodega", p.IdProductoBodega),
            new SqlParameter("@IdProducto", p.IdProducto),
            new SqlParameter("@IdBodega", p.IdBodega),
            new SqlParameter("@activo", p.Activo),
            new SqlParameter("@sistema", p.Sistema),
            new SqlParameter("@user_agr", p.User_agr),
            new SqlParameter("@fec_agr", p.Fec_agr),
            new SqlParameter("@user_mod", p.User_mod),
            new SqlParameter("@fec_mod", p.Fec_mod)
        });

        return cmd;
    }
    public static void Cargar(ref clsBeProducto_bodega o, DataRow dr)
    {
        try
        {
            o.IdProductoBodega = dr.Field<int>("IdProductoBodega");
            o.IdProducto = dr.Field<int>("IdProducto");
            o.IdBodega = dr.Field<int>("IdBodega");
            o.Activo = dr.Field<bool>("activo");
            o.Sistema = dr.Field<bool>("sistema");
            o.User_agr = dr.Field<string>("user_agr") ?? "";
            o.Fec_agr = dr.Field<DateTime>("fec_agr");
            o.User_mod = dr.Field<string>("user_mod") ?? "";
            o.Fec_mod = dr.Field<DateTime>("fec_mod");
        }
        catch (Exception ex)
        {
            throw new Exception($"{MethodBase.GetCurrentMethod()} {ex.Message}");
        }
    }
    public static int Insertar(clsBeProducto_bodega p, SqlConnection conn, SqlTransaction tran)
    {
        if (p == null)
            throw new ArgumentNullException(nameof(p));

        if (conn == null)
            throw new ArgumentNullException(nameof(conn));

        if (tran == null)
            throw new ArgumentNullException(nameof(tran));

        Ins.Init("producto_bodega");
        Ins.Add("idproductobodega", "@idproductobodega", "F");
        Ins.Add("idproducto", "@idproducto", "F");
        Ins.Add("idbodega", "@idbodega", "F");
        Ins.Add("activo", "@activo", "F");
        Ins.Add("sistema", "@sistema", "F");
        Ins.Add("user_agr", "@user_agr", "F");
        Ins.Add("fec_agr", "@fec_agr", "F");
        Ins.Add("user_mod", "@user_mod", "F");
        Ins.Add("fec_mod", "@fec_mod", "F");

        string sql = Ins.SQL();

        try
        {
            using var cmd = CrearComando(sql, conn, tran, p);
            return cmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            throw new Exception($"{MethodBase.GetCurrentMethod()} {ex.Message}");
        }
    }
    public static int Actualizar(clsBeProducto_bodega p, SqlConnection conn, SqlTransaction tran)
    {
        if (p == null)
            throw new ArgumentNullException(nameof(p));

        if (conn == null)
            throw new ArgumentNullException(nameof(conn));

        if (tran == null)
            throw new ArgumentNullException(nameof(tran));

        Upd.Init("producto_bodega");
        Upd.Add("idproductobodega", "@idproductobodega", "F");
        Upd.Add("idproducto", "@idproducto", "F");
        Upd.Add("idbodega", "@idbodega", "F");
        Upd.Add("activo", "@activo", "F");
        Upd.Add("sistema", "@sistema", "F");
        Upd.Add("user_agr", "@user_agr", "F");
        Upd.Add("fec_agr", "@fec_agr", "F");
        Upd.Add("user_mod", "@user_mod", "F");
        Upd.Add("fec_mod", "@fec_mod", "F");
        Upd.Where("IdProductoBodega = @IdProductoBodega");

        string sql = Upd.SQL();

        try
        {
            using var cmd = CrearComando(sql, conn, tran, p);
            return cmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            throw new Exception($"{MethodBase.GetCurrentMethod()} {ex.Message}");
        }
    }
    public static bool GetSingle(IConfiguration config, ref clsBeProducto_bodega p)
    {
        const string sql = "SELECT * FROM Producto_bodega WHERE IdProductoBodega = @IdProductoBodega";

        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();

        using var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
        using var cmd = new SqlCommand(sql, conn, tran) { CommandType = CommandType.Text };
        cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", p.IdProductoBodega));

        using var da = new SqlDataAdapter(cmd);
        var dt = new DataTable();
        da.Fill(dt);
        tran.Commit();

        if (dt.Rows.Count == 1)
        {
            Cargar(ref p, dt.Rows[0]);
            return true;
        }
        return false;
    }
    public static int MaxID(IConfiguration config)
    {
        const string sql = "SELECT ISNULL(MAX(IdProductoBodega), 0) FROM Producto_bodega";

        using var conn = new SqlConnection(config.GetConnectionString("CST"));
        conn.Open();

        using var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
        using var cmd = new SqlCommand(sql, conn, tran);
        var result = cmd.ExecuteScalar();
        tran.Commit();

        return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
    }
    public static int Eliminar(IConfiguration config, clsBeProducto_bodega p, SqlConnection? extConn = null, SqlTransaction? extTran = null)
    {
        const string sql = "DELETE FROM Producto_bodega WHERE IdProductoBodega = @IdProductoBodega";
        bool remoto = extConn != null && extTran != null;
        int rows = 0;

        using var conn = remoto ? extConn! : new SqlConnection(config.GetConnectionString("CST"));
        SqlTransaction? tran = null;

        try
        {
            if (!remoto) conn.Open();
            tran ??= remoto ? extTran! : conn.BeginTransaction(IsolationLevel.ReadUncommitted);

            using var cmd = new SqlCommand(sql, conn, tran);
            cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", p.IdProductoBodega));
            rows = cmd.ExecuteNonQuery();

            if (!remoto) tran.Commit();
        }
        catch (SqlException ex)
        {
            if (!remoto) tran?.Rollback();
            throw new Exception($"{MethodBase.GetCurrentMethod()} {ex.Message}");
        }

        return rows;
    }
    public static bool Existe(int IdProductoBodega, SqlConnection pConnection, SqlTransaction pTransaction)
    {
        try
        {
            const string query = "SELECT COUNT(1) FROM Producto_bodega WHERE IdProductoBodega = @IdProductoBodega";

            using (SqlCommand cmd = new SqlCommand(query, pConnection, pTransaction))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@IdProductoBodega", IdProductoBodega));

                object result = cmd.ExecuteScalar();
                int count = Convert.ToInt32(result);

                return count > 0;
            }
        }
        catch (SqlException ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethod = sf?.GetMethod();
            string vMsgError = string.Format("{0} {1}", currentMethod?.Name ?? "UnknownMethod", ex.Message);

            throw new Exception(vMsgError, ex);
        }
    }
    public static int InsertOrUpdate(List<clsBeProducto_bodega> entities, SqlConnection conn, SqlTransaction tx)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        if (conn == null)
            throw new ArgumentNullException(nameof(conn));

        if (tx == null)
            throw new ArgumentNullException(nameof(tx));

        if (entities.Count == 0)
            return 0;

        int affected = 0;

        try
        {
            foreach (var entity in entities)
            {
                if (entity == null)
                    continue;

                if (entity.IdProducto != 0)
                {
                    bool existe = Existe(entity.IdProductoBodega, conn, tx);

                    affected += existe
                        ? Actualizar(entity, conn, tx)
                        : Insertar(entity, conn, tx);
                }
            }

            return affected;
        }
        catch (SqlException ex)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
        }
    }
    public static int MaxID(SqlConnection conn, SqlTransaction tx)
    {
        const string sql = "SELECT ISNULL(MAX(IdProductoBodega), 0) FROM Producto_bodega";

        try
        {
            using var cmd = new SqlCommand(sql, conn, tx);
            var result = cmd.ExecuteScalar();
            return Convert.ToInt32(result);
        }
        catch
        {
            throw;
        }
    }
    public static List<clsBeProducto_bodega> GetAll(IConfiguration configuration)
    {
        const string sql = "SELECT * FROM Producto_bodega";
        var resultList = new List<clsBeProducto_bodega>();

        using var connection = new SqlConnection(configuration.GetConnectionString("CST"));
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
        using var cmd = new SqlCommand(sql, connection, transaction) { CommandType = CommandType.Text };
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var row = reader.ToDataRow();
            var item = new clsBeProducto_bodega();
            Cargar(ref item, row);
            resultList.Add(item);
        }

        transaction.Commit();
        return resultList;
    }
    public static int InsertarOActualizar(List<clsBeProducto_bodega> entities, SqlConnection conn, SqlTransaction tx)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        if (conn == null)
            throw new ArgumentNullException(nameof(conn));

        if (tx == null)
            throw new ArgumentNullException(nameof(tx));

        int total = 0;

        try
        {
            foreach (var entity in entities)
            {
                if (entity == null)
                    continue;

                bool existe = Existe(entity.IdProductoBodega, conn, tx);

                int result = existe
                    ? Actualizar(entity, conn, tx)
                    : Insertar(entity, conn, tx);

                total += result;
            }

            return total;
        }
        catch (SqlException ex)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
        }
    }

    public static clsBeProducto_bodega? Existe_Codigo_By_IdBodega(string pCodigo,
                                                                  int pIdBodega,
                                                                  SqlConnection lConnection,
                                                                  SqlTransaction lTransaction)
    {
        clsBeProducto_bodega? resultado = null;

        try
        {
            string vSQL = @"
                SELECT * 
                FROM producto_bodega pb 
                INNER JOIN producto p ON pb.IdProducto = p.IdProducto 
                WHERE p.Codigo = @Codigo AND pb.IdBodega = @IdBodega";

            using (var lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo);
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega);
                lDTA.SelectCommand.Transaction = lTransaction;

                var lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];

                    var objPB = new clsBeProducto_bodega();
                    var objP = new clsBeProducto();

                    Cargar(ref objPB, lRow);

                    objP.IdProducto = objPB.IdProducto;
                    objP = clsLnProducto.GetSingle(objP.IdProducto, lConnection, lTransaction);

                    if (objP != null) // Ensure objP is not null before assignment
                    {
                        objPB.Producto = objP;
                    }

                    resultado = objPB;
                }
            }
        }
        catch
        {
            throw;
        }

        return resultado;
    }

    public static clsBeProducto_bodega? Existe_Parte_By_IdBodega(
        string pCodigo,
        int pIdBodega,
        SqlConnection lConnection,
        SqlTransaction lTransaction)
    {
        clsBeProducto_bodega? resultado = null;

        try
        {
            string vSQL = @"
                SELECT * 
                FROM producto_bodega pb 
                INNER JOIN producto p ON pb.IdProducto = p.IdProducto 
                WHERE p.noparte = @Codigo AND pb.IdBodega = @IdBodega";

            using (var lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo);
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega);
                lDTA.SelectCommand.Transaction = lTransaction;

                var lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];
                    var objPB = new clsBeProducto_bodega();
                    var objP = new clsBeProducto();

                    Cargar(ref objPB, lRow);

                    objP.IdProducto = objPB.IdProducto;
                    objP = clsLnProducto.GetSingle(objP.IdProducto, lConnection, lTransaction);

                    if (objP != null)                    
                        objPB.Producto = objP;                    

                    resultado = objPB;
                }
            }
        }
        catch
        {
            throw;
        }

        return resultado;
    }

    public static clsBeProducto_bodega? Existe_NoSerie_By_IdBodega(
        string pCodigo,
        int pIdBodega,
        SqlConnection lConnection,
        SqlTransaction lTransaction)
    {
        clsBeProducto_bodega? resultado = null;

        try
        {
            string vSQL = @"
                SELECT * 
                FROM producto_bodega pb 
                INNER JOIN producto p ON pb.IdProducto = p.IdProducto 
                WHERE p.noserie = @Codigo AND pb.IdBodega = @IdBodega";

            using (var lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo);
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega);
                lDTA.SelectCommand.Transaction = lTransaction;

                var lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];
                    var objPB = new clsBeProducto_bodega();
                    var objP = new clsBeProducto();

                    Cargar(ref objPB, lRow);

                    objP.IdProducto = objPB.IdProducto;
                    objP = clsLnProducto.GetSingle(objP.IdProducto, lConnection, lTransaction);
                    if (objP != null)
                        objPB.Producto = objP;
                    resultado = objPB;
                }
            }
        }
        catch
        {
            throw;
        }

        return resultado;
    }

    public static clsBeProducto? Get_Producto_By_IdProductoBodega(int pIdProductoBodega,
                                                                  SqlConnection lConnection,
                                                                  SqlTransaction lTransaction)
    {
        clsBeProducto? result = null;

        try
        {
            string vSQL = "SELECT IdProducto FROM producto_bodega WHERE IdProductoBodega = @IdProductoBodega";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega);

                DataTable lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable != null && lDataTable.Rows.Count > 0)
                {
                    int idProducto = Convert.ToInt32(lDataTable.Rows[0]["IdProducto"]);
                    clsBeProducto? BeProducto = clsLnProducto.GetSingle(idProducto, lConnection, lTransaction);

                    if (BeProducto != null)
                    {
                        BeProducto.IdProductoBodega = pIdProductoBodega;
                        result = BeProducto;
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

    public static clsBeProducto_bodega? Existe(string pCodigo, int pIdBodega, SqlConnection lConnection, SqlTransaction lTransaction)
    {
        try
        {
            string vSQL = @"SELECT * from producto_bodega pb Inner Join producto p 
                       ON pb.IdProducto = p.IdProducto 
                       WHERE p.codigo = @Codigo AND pb.IdBodega = @IdBodega";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo);
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega);

                DataTable lDT = new DataTable();
                lDTA.Fill(lDT);

                if (lDT != null && lDT.Rows.Count > 0)
                {
                    DataRow lRow = lDT.Rows[0];
                    clsBeProducto_bodega BeProductoBodega = new clsBeProducto_bodega();
                    clsBeProducto? ObjP = new clsBeProducto();
                    Cargar(ref BeProductoBodega, lRow);
                    ObjP.IdProducto = BeProductoBodega.IdProducto;
                    ObjP = clsLnProducto.GetSingle(ObjP.IdProducto, lConnection, lTransaction);
                    if (ObjP !=null)
                    BeProductoBodega.Producto = ObjP;
                    return BeProductoBodega;
                }
            }

            return null;
        }
        catch
        {
            throw;
        }
    }

    public static int Get_IdBodega_By_IdProductoBodega(int pIdProductoBodega, SqlConnection pConnection, SqlTransaction pTransaction)
    {
        int result = 0;

        try
        {
            string vSQL = "SELECT IdBodega FROM producto_bodega WHERE IdProductoBodega=@IdProductoBodega";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, pConnection))
            {
                lDTA.SelectCommand.Transaction = pTransaction;
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega);

                DataTable lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable != null && lDataTable.Rows.Count > 0)
                {
                    result = Convert.ToInt32(lDataTable.Rows[0]["IdBodega"]);
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return result;
    }

    public static int Get_IdProducto_By_IdProductoBodega(int pIdProductoBodega, SqlConnection lConnection, SqlTransaction lTransaction)
    {
        int result = 0;

        try
        {
            string vSQL = "SELECT IdProducto FROM producto_bodega WHERE IdProductoBodega=@IdProductoBodega";

            using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
            {
                lDTA.SelectCommand.CommandType = CommandType.Text;
                lDTA.SelectCommand.Transaction = lTransaction;
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega);

                DataTable lDataTable = new DataTable();
                lDTA.Fill(lDataTable);

                if (lDataTable != null && lDataTable.Rows.Count > 0)
                {
                    result = Convert.ToInt32(lDataTable.Rows[0]["IdProducto"]);
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return result;
    }
}