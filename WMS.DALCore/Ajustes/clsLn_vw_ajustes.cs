using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using WMS.EntityCore.Dtos.Ajustes;

namespace WMS.DALCore.Ajustes
{
    namespace WMS.Infrastructure.Repositories
    {
        public class clsLn_vw_ajustes
        {
            private readonly IConfiguration _configuration;
            private readonly string _connectionString;

            public clsLn_vw_ajustes(IConfiguration configuration)
            {
                _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
                _connectionString = _configuration.GetConnectionString("CST")
                    ?? _configuration["AppSettings:CST"]
                    ?? throw new InvalidOperationException("Connection string 'CST' not found.");
            }

            public async Task<List<clsBe_vw_ajustes>> Get_All_Pendientes_EnvioAsync(
                int IdAjusteEnc,
                SqlConnection lConnection,
                SqlTransaction lTransaction)
            {
                try
                {
                    var lReturnList = new List<clsBe_vw_ajustes>();

                    const string sp = @"SELECT * FROM vw_ajustes 
                                   WHERE IdAjusteEnc = @IdAjusteEnc
                                   AND Enviado = 0";

                    await using var cmd = new SqlCommand(sp, lConnection, lTransaction)
                    {
                        CommandType = CommandType.Text
                    };

                    cmd.Parameters.AddWithValue("@IdAjusteEnc", IdAjusteEnc);

                    using var dad = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    dad.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        var vBeT_vw_ajustes = new clsBe_vw_ajustes();
                        Cargar(vBeT_vw_ajustes, dr);
                        lReturnList.Add(vBeT_vw_ajustes);
                    }

                    return lReturnList;
                }
                catch (Exception ex)
                {
                    var vMsgError = $"{MethodBase.GetCurrentMethod()?.Name} {ex.Message}";                    
                    throw;
                }
            }

            private static void Cargar(clsBe_vw_ajustes entity, DataRow dr)
            {
                if (entity == null || dr == null) return;

                if (dr.Table.Columns.Contains("IdAjusteEnc") && dr["IdAjusteEnc"] != DBNull.Value)
                    entity.IdAjusteEnc = Convert.ToInt32(dr["IdAjusteEnc"]);

                if (dr.Table.Columns.Contains("IdAjusteDet") && dr["IdAjusteDet"] != DBNull.Value)
                    entity.IdAjusteDet = Convert.ToInt32(dr["IdAjusteDet"]);

                if (dr.Table.Columns.Contains("Codigo_Producto") && dr["Codigo_Producto"] != DBNull.Value)
                    entity.Codigo_Producto = dr["Codigo_Producto"].ToString() ?? string.Empty;

                if (dr.Table.Columns.Contains("Nombre_Producto") && dr["Nombre_Producto"] != DBNull.Value)
                    entity.Nombre_Producto = dr["Nombre_Producto"].ToString() ?? string.Empty;

                if (dr.Table.Columns.Contains("Codigo_Bodega") && dr["Codigo_Bodega"] != DBNull.Value)
                    entity.Codigo_Bodega = dr["Codigo_Bodega"].ToString() ?? string.Empty;

                if (dr.Table.Columns.Contains("IdBodegaERP") && dr["IdBodegaERP"] != DBNull.Value)
                    entity.IdBodegaERP = Convert.ToInt32(dr["IdBodegaERP"]);

                if (dr.Table.Columns.Contains("Tipo_Ajuste") && dr["Tipo_Ajuste"] != DBNull.Value)
                    entity.Tipo_Ajuste = dr["Tipo_Ajuste"].ToString() ?? string.Empty;

                if (dr.Table.Columns.Contains("UMBas") && dr["UMBas"] != DBNull.Value)
                    entity.UMBas = dr["UMBas"].ToString() ?? string.Empty;

                if (dr.Table.Columns.Contains("Cantidad_original") && dr["Cantidad_original"] != DBNull.Value)
                    entity.Cantidad_original = Convert.ToDouble(dr["Cantidad_original"]);

                if (dr.Table.Columns.Contains("Cantidad_nueva") && dr["Cantidad_nueva"] != DBNull.Value)
                    entity.Cantidad_nueva = Convert.ToDouble(dr["Cantidad_nueva"]);

                if (dr.Table.Columns.Contains("Lote_Nuevo") && dr["Lote_Nuevo"] != DBNull.Value)
                    entity.Lote_Nuevo = dr["Lote_Nuevo"].ToString() ?? string.Empty;

                if (dr.Table.Columns.Contains("Motivo_Ajuste") && dr["Motivo_Ajuste"] != DBNull.Value)
                    entity.Motivo_Ajuste = dr["Motivo_Ajuste"].ToString() ?? string.Empty;

                if (dr.Table.Columns.Contains("Observacion") && dr["Observacion"] != DBNull.Value)
                    entity.Observacion = dr["Observacion"].ToString() ?? string.Empty;

                if (dr.Table.Columns.Contains("Seccion") && dr["Seccion"] != DBNull.Value)
                    entity.Seccion = dr["Seccion"].ToString() ?? string.Empty;

                if (dr.Table.Columns.Contains("Enviado") && dr["Enviado"] != DBNull.Value)
                    entity.Enviado = Convert.ToBoolean(dr["Enviado"]);

                if (dr.Table.Columns.Contains("Modifica_Cantidad") && dr["Modifica_Cantidad"] != DBNull.Value)
                    entity.Modifica_Cantidad = Convert.ToBoolean(dr["Modifica_Cantidad"]);

                if (dr.Table.Columns.Contains("Talla") && dr["Talla"] != DBNull.Value)
                    entity.Talla = dr["Talla"].ToString() ?? string.Empty;

                if (dr.Table.Columns.Contains("Color") && dr["Color"] != DBNull.Value)
                    entity.Color = dr["Color"].ToString() ?? string.Empty;
            }

            public static List<clsBe_vw_ajustes> Get_All_Pendientes_Envio(
             int IdAjusteEnc,
             SqlConnection lConnection,
             SqlTransaction lTransaction)
            {
                try
                {
                    var lReturnList = new List<clsBe_vw_ajustes>();

                    const string sp = @"SELECT * FROM vw_ajustes 
                                   WHERE IdAjusteEnc = @IdAjusteEnc
                                   AND Enviado = 0";

                    using var cmd = new SqlCommand(sp, lConnection, lTransaction)
                    {
                        CommandType = CommandType.Text
                    };

                    cmd.Parameters.AddWithValue("@IdAjusteEnc", IdAjusteEnc);

                    using var dad = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    dad.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        var vBeT_vw_ajustes = new clsBe_vw_ajustes();
                        Cargar(vBeT_vw_ajustes, dr);
                        lReturnList.Add(vBeT_vw_ajustes);
                    }

                    return lReturnList;
                }
                catch (Exception ex)
                {
                    var vMsgError = $"{MethodBase.GetCurrentMethod()?.Name} {ex.Message}";                    
                    throw;
                }
            }
        }
    }
}