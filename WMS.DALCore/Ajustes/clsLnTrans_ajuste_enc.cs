using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using WMS.EntityCore.Ajustes;

namespace WMS.DALCore.Ajustes
{
    public class clsLnTrans_ajuste_enc
    {        

        public static async Task<List<clsBeTrans_ajuste_enc>> GetAllAsync(IConfiguration config, DateTime pFechaDel, DateTime pFechaAl, int pIdBodega)
        {
            try
            {
                var lReturnList = new List<clsBeTrans_ajuste_enc>();
                var sp = "SELECT * FROM Trans_ajuste_enc WHERE IdBodega = @IdBodega AND CAST(Trans_ajuste_enc.fec_agr AS DATE) BETWEEN @FechaDel AND @FechaAl";

                await using var lConnection = new SqlConnection(config.GetConnectionString("CST"));
                await using var cmd = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text };

                cmd.Parameters.AddWithValue("@IdBodega", pIdBodega);
                cmd.Parameters.AddWithValue("@FechaDel", pFechaDel.Date);
                cmd.Parameters.AddWithValue("@FechaAl", pFechaAl.Date);

                await lConnection.OpenAsync();

                using var dad = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                dad.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    var vBeTrans_ajuste_enc = new clsBeTrans_ajuste_enc();
                    Cargar(vBeTrans_ajuste_enc, dr);
                    lReturnList.Add(vBeTrans_ajuste_enc);
                }

                return lReturnList;
            }
            catch (Exception ex)
            {
                var vMsgError = $"{MethodBase.GetCurrentMethod()?.Name} {ex.Message}";
                
                throw;
            }
        }             

        public static async Task<List<clsBeTrans_ajuste_enc>> GetAll_Pendientes_EnvioAsync(IConfiguration config)
        {
            try
            {
                var lReturnList = new List<clsBeTrans_ajuste_enc>();

                const string sp = @"SELECT DISTINCT trans_ajuste_enc.*
                                   FROM trans_ajuste_det 
                                   INNER JOIN trans_ajuste_enc 
                                   ON trans_ajuste_det.idajusteenc = trans_ajuste_enc.idajusteenc 
                                   WHERE trans_ajuste_det.enviado = 0";

                await using var lConnection = new SqlConnection(config.GetConnectionString("CST"));
                await using var cmd = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text };

                await lConnection.OpenAsync();

                using var dad = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                dad.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    var vBeTrans_ajuste_enc = new clsBeTrans_ajuste_enc();
                    Cargar(vBeTrans_ajuste_enc, dr);
                    lReturnList.Add(vBeTrans_ajuste_enc);
                }

                return lReturnList;
            }
            catch (Exception ex)
            {
                var vMsgError = $"{MethodBase.GetCurrentMethod()?.Name} {ex.Message}";
                
                throw;
            }
        }

        public static async Task<List<clsBeTrans_ajuste_enc>> Get_All_Pendientes_Envio(SqlConnection lConnection,SqlTransaction lTransaction)
        {
            if (lConnection == null) throw new ArgumentNullException(nameof(lConnection));
            if (lTransaction == null) throw new ArgumentNullException(nameof(lTransaction));

            try
            {
                var lReturnList = new List<clsBeTrans_ajuste_enc>();

                const string sp = @"SELECT DISTINCT trans_ajuste_enc.*
                                   FROM trans_ajuste_det 
                                   INNER JOIN trans_ajuste_enc 
                                   ON trans_ajuste_det.idajusteenc = trans_ajuste_enc.idajusteenc 
                                   WHERE trans_ajuste_det.enviado = 0 AND trans_ajuste_enc.auditado = 1";

                await using var cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };

                using var dad = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                dad.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    var vBeTrans_ajuste_enc = new clsBeTrans_ajuste_enc();
                    Cargar(vBeTrans_ajuste_enc, dr);
                    lReturnList.Add(vBeTrans_ajuste_enc);
                }

                return lReturnList;
            }
            catch (Exception ex)
            {
                var vMsgError = $"{MethodBase.GetCurrentMethod()?.Name} {ex.Message}";
                
                throw;
            }
        }        

        public static async Task<List<clsBeTrans_ajuste_enc>> Get_All_Auditados_Pendientes_EnvioAsync(
            SqlConnection lConnection,
            SqlTransaction lTransaction)
        {
            if (lConnection == null) throw new ArgumentNullException(nameof(lConnection));
            if (lTransaction == null) throw new ArgumentNullException(nameof(lTransaction));

            try
            {
                var lReturnList = new List<clsBeTrans_ajuste_enc>();

                const string sp = @"SELECT DISTINCT trans_ajuste_enc.*
                                   FROM trans_ajuste_det 
                                   INNER JOIN trans_ajuste_enc 
                                   ON trans_ajuste_det.idajusteenc = trans_ajuste_enc.idajusteenc 
                                   WHERE trans_ajuste_det.enviado = 0 
                                     AND trans_ajuste_enc.auditado = 1 
                                     AND ajuste_por_inventario = 0";

                await using var cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };

                using var dad = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                dad.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    var vBeTrans_ajuste_enc = new clsBeTrans_ajuste_enc();
                    Cargar(vBeTrans_ajuste_enc, dr);
                    lReturnList.Add(vBeTrans_ajuste_enc);
                }

                return lReturnList;
            }
            catch (Exception ex)
            {
                var vMsgError = $"{MethodBase.GetCurrentMethod()?.Name} {ex.Message}";
                
                throw;
            }
        }

        public static async Task<List<clsBeTrans_ajuste_enc>> Get_All_Auditados_Pendientes_Envio_By_IdInventarioEncAsync(
            SqlConnection lConnection,
            SqlTransaction lTransaction)
        {
            if (lConnection == null) throw new ArgumentNullException(nameof(lConnection));
            if (lTransaction == null) throw new ArgumentNullException(nameof(lTransaction));

            try
            {
                var lReturnList = new List<clsBeTrans_ajuste_enc>();

                const string sp = @"SELECT DISTINCT trans_ajuste_enc.*
                                   FROM trans_ajuste_det 
                                   INNER JOIN trans_ajuste_enc 
                                   ON trans_ajuste_det.idajusteenc = trans_ajuste_enc.idajusteenc
                                   WHERE trans_ajuste_det.enviado = 0 
                                     AND trans_ajuste_enc.auditado = 1 
                                     AND ajuste_por_inventario > 0";

                await using var cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };

                using var dad = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                dad.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    var vBeTrans_ajuste_enc = new clsBeTrans_ajuste_enc();
                    Cargar(vBeTrans_ajuste_enc, dr);
                    lReturnList.Add(vBeTrans_ajuste_enc);
                }

                return lReturnList;
            }
            catch (Exception ex)
            {
                var vMsgError = $"{MethodBase.GetCurrentMethod()?.Name} {ex.Message}";
                
                throw;
            }
        }

        public static async Task<List<int>> Get_All_By_IdInventarioEncAsync(IConfiguration config, int pIdInventarioEnc)
        {
            try
            {
                var lReturnList = new List<int>();

                const string sp = @"SELECT IdAjusteEnc 
                                   FROM trans_ajuste_enc 
                                   WHERE ajuste_por_inventario = @ajuste_por_inventario";

                await using SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
                await lConnection.OpenAsync();

                await using SqlTransaction lTransaction = (SqlTransaction) await lConnection.BeginTransactionAsync(IsolationLevel.ReadUncommitted);

                try
                {
                    await using var cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                    cmd.Parameters.AddWithValue("@ajuste_por_inventario", pIdInventarioEnc);

                    using var dad = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    dad.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr[0] != DBNull.Value)
                        {
                            lReturnList.Add(Convert.ToInt32(dr[0]));
                        }
                    }

                    await lTransaction.CommitAsync();
                    return lReturnList;
                }
                catch
                {
                    await lTransaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                var vMsgError = $"{MethodBase.GetCurrentMethod()?.Name} {ex.Message}";
                
                throw;
            }
        }

        public static async Task<int> Actualizar_Estado_Enviado_A_ERP_AllAsync(
            List<int> lAjustes,
            SqlConnection pConnection,
            SqlTransaction pTransaction)
        {
            if (lAjustes == null) throw new ArgumentNullException(nameof(lAjustes));
            if (pConnection == null) throw new ArgumentNullException(nameof(pConnection));
            if (pTransaction == null) throw new ArgumentNullException(nameof(pTransaction));

            var result = 0;

            try
            {
                if (!lAjustes.Any()) return result;

                var vSQL = @"UPDATE trans_ajuste_enc SET Enviado_A_ERP = 1
                            WHERE Enviado_A_ERP = 0 
                              AND idajusteenc IN (SELECT DISTINCT idajusteenc FROM trans_ajuste_det WHERE enviado = 1)
                              AND idajusteenc NOT IN (SELECT DISTINCT idajusteenc FROM trans_ajuste_det WHERE enviado = 0)
                              AND idajusteenc IN ({0})";

                var parameters = lAjustes.Select((id, index) => $"@Id{index}");
                var inClause = string.Join(",", parameters);
                vSQL = string.Format(vSQL, inClause);

                await using var lCommandEnc = new SqlCommand(vSQL, pConnection, pTransaction) { CommandType = CommandType.Text };

                for (int i = 0; i < lAjustes.Count; i++)
                {
                    lCommandEnc.Parameters.AddWithValue($"@Id{i}", lAjustes[i]);
                }

                result = await lCommandEnc.ExecuteNonQueryAsync();

                return result;
            }
            catch (Exception ex)
            {
                var vMsgError = $"{MethodBase.GetCurrentMethod()?.Name} {ex.Message}";
                
                throw;
            }
        }

        public static async Task<DataTable> ObtenerAjustesInventarioAsync(IConfiguration config, DateTime fechaDesde, DateTime fechaHasta)
        {
            var query = @"WITH AjustesUnicos AS (
                            SELECT 
                                ajuste_por_inventario AS Correlativo, 
                                CONCAT('Inventario No. ', ajuste_por_inventario) AS NoInventario,
                                Fecha,
                                referencia,
                                ROW_NUMBER() OVER (PARTITION BY ajuste_por_inventario ORDER BY Fecha DESC) AS rn
                            FROM trans_ajuste_enc 
                            WHERE Fecha BETWEEN @FechaDesde AND @FechaHasta 
                              AND ajuste_por_inventario > 0
                        )
                        SELECT 
                            Correlativo, 
                            NoInventario, 
                            Fecha, 
                            referencia
                        FROM AjustesUnicos
                        WHERE rn = 1";

            var dtResultado = new DataTable();

            await using var conn = new SqlConnection(config.GetConnectionString("CST"));
            await conn.OpenAsync();

            var tran = await conn.BeginTransactionAsync(IsolationLevel.ReadUncommitted);

            try
            {
                await using (var cmd = new SqlCommand(query, conn, (SqlTransaction)tran))
                {
                    cmd.Parameters.AddWithValue("@FechaDesde", fechaDesde.Date);
                    cmd.Parameters.AddWithValue("@FechaHasta", fechaHasta.Date);

                    using var da = new SqlDataAdapter(cmd);
                    da.Fill(dtResultado);
                }

                await tran.CommitAsync();
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                throw new ApplicationException($"Error al obtener ajustes de inventario: {ex.Message}", ex);
            }

            return dtResultado;
        }        

        public static async Task<DataTable> Get_All_VWAsync(IConfiguration config, DateTime pFechaDel, DateTime pFechaAl, int pIdBodega)
        {
            try
            {
                var sp = "SELECT * FROM VW_Ajustes_List WHERE IdBodega = @IdBodega AND CAST(fecha AS DATE) BETWEEN @FechaDel AND @FechaAl";

                await using var lConnection = new SqlConnection(config.GetConnectionString("CST"));
                await using var cmd = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text };

                cmd.Parameters.AddWithValue("@IdBodega", pIdBodega);
                cmd.Parameters.AddWithValue("@FechaDel", pFechaDel.Date);
                cmd.Parameters.AddWithValue("@FechaAl", pFechaAl.Date);

                await lConnection.OpenAsync();

                using var dad = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                dad.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                var vMsgError = $"{MethodBase.GetCurrentMethod()?.Name} {ex.Message}";
                
                throw;
            }
        }

        private static void Cargar(clsBeTrans_ajuste_enc entity, DataRow dr)
        {
            // Implementación del método Cargar para mapear DataRow a la entidad
            if (entity == null || dr == null) return;

            if (dr.Table.Columns.Contains("Idajusteenc") && dr["Idajusteenc"] != DBNull.Value)
                entity.Idajusteenc = Convert.ToInt32(dr["Idajusteenc"]);

            if (dr.Table.Columns.Contains("Fecha") && dr["Fecha"] != DBNull.Value)
                entity.Fecha = Convert.ToDateTime(dr["Fecha"]);

            if (dr.Table.Columns.Contains("Idusuario") && dr["Idusuario"] != DBNull.Value)
                entity.Idusuario = Convert.ToInt32(dr["Idusuario"]);

            if (dr.Table.Columns.Contains("Referencia") && dr["Referencia"] != DBNull.Value)
                entity.Referencia = dr["Referencia"].ToString() ?? "";

            if (dr.Table.Columns.Contains("Fec_agr") && dr["Fec_agr"] != DBNull.Value)
                entity.Fec_agr = Convert.ToDateTime(dr["Fec_agr"]);

            if (dr.Table.Columns.Contains("User_agr") && dr["User_agr"] != DBNull.Value)
                entity.User_agr = dr["User_agr"].ToString() ?? "";

            if (dr.Table.Columns.Contains("Fec_mod") && dr["Fec_mod"] != DBNull.Value)
                entity.Fec_mod = Convert.ToDateTime(dr["Fec_mod"]);

            if (dr.Table.Columns.Contains("User_mod") && dr["User_mod"] != DBNull.Value)
                entity.User_mod = dr["User_mod"].ToString() ?? "";

            if (dr.Table.Columns.Contains("IdBodega") && dr["IdBodega"] != DBNull.Value)
                entity.IdBodega = Convert.ToInt32(dr["IdBodega"]);

            if (dr.Table.Columns.Contains("IdProductoFamilia") && dr["IdProductoFamilia"] != DBNull.Value)
                entity.IdProductoFamilia = Convert.ToInt32(dr["IdProductoFamilia"]);

            if (dr.Table.Columns.Contains("Enviado_A_ERP") && dr["Enviado_A_ERP"] != DBNull.Value)
                entity.Enviado_A_ERP = Convert.ToBoolean(dr["Enviado_A_ERP"]);

            if (dr.Table.Columns.Contains("IdPropietarioBodega") && dr["IdPropietarioBodega"] != DBNull.Value)
                entity.IdPropietarioBodega = Convert.ToInt32(dr["IdPropietarioBodega"]);

            if (dr.Table.Columns.Contains("Ajuste_Por_Inventario") && dr["Ajuste_Por_Inventario"] != DBNull.Value)
                entity.Ajuste_Por_Inventario = Convert.ToInt32(dr["Ajuste_Por_Inventario"]);

            if (dr.Table.Columns.Contains("IdCentroCosto") && dr["IdCentroCosto"] != DBNull.Value)
                entity.IdCentroCosto = Convert.ToInt32(dr["IdCentroCosto"]);

            if (dr.Table.Columns.Contains("Auditado") && dr["Auditado"] != DBNull.Value)
                entity.Auditado = Convert.ToBoolean(dr["Auditado"]);

            if (dr.Table.Columns.Contains("Centro_Costo_Erp") && dr["Centro_Costo_Erp"] != DBNull.Value)
                entity.Centro_Costo_Erp = dr["Centro_Costo_Erp"].ToString() ?? "";

            if (dr.Table.Columns.Contains("Centro_Costo_Dir_Erp") && dr["Centro_Costo_Dir_Erp"] != DBNull.Value)
                entity.Centro_Costo_Dir_Erp = dr["Centro_Costo_Dir_Erp"].ToString() ?? "";

            if (dr.Table.Columns.Contains("Centro_Costo_Dep_Erp") && dr["Centro_Costo_Dep_Erp"] != DBNull.Value)
                entity.Centro_Costo_Dep_Erp = dr["Centro_Costo_Dep_Erp"].ToString() ?? "";
        }      
    }
}