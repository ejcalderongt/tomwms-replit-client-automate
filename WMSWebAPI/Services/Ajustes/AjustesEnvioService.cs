using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using WMS.DALCore.Ajustes;
using WMS.DALCore.Ajustes.WMS.Infrastructure.Repositories;
using WMS.EntityCore.Ajustes;
using WMS.EntityCore.Cliente;
using WMS.EntityCore.Datos_Maestros;
using WMS.EntityCore.Dtos.Ajustes;
using WMS.EntityCore.Dtos.Ajustes.WMS.Core.Entities;
using WMS.EntityCore.Producto;
using WMSWebAPI.Ln;

public interface IAjustesEnvioService
{
    Task<AjustesPendientesEnvioResponse> GetAjustesPendientesEnvioAsync(CancellationToken ct = default);
}

public sealed class AjustesEnvioService : IAjustesEnvioService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AjustesEnvioService> _logger;

    public AjustesEnvioService(IConfiguration configuration, ILogger<AjustesEnvioService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<AjustesPendientesEnvioResponse> GetAjustesPendientesEnvioAsync(CancellationToken ct = default)
    {
        var sb = new StringBuilder();
        var resp = new AjustesPendientesEnvioResponse();

        string vNDoc = "";

        try
        {
            var cs = _configuration.GetConnectionString("CST");
            if (string.IsNullOrWhiteSpace(cs))
                throw new InvalidOperationException("No existe ConnectionString 'CST'.");

            await using var conn = new SqlConnection(cs);
            await conn.OpenAsync(ct);
            await using var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

            // === VB: clsLnTrans_ajuste_enc.Get_All_Pendientes_Envio ===
            List<clsBeTrans_ajuste_enc> ajustesPendEnvio = await clsLnTrans_ajuste_enc.Get_All_Pendientes_Envio(conn, tran);

            if (ajustesPendEnvio == null || ajustesPendEnvio.Count == 0)
            {
                Append(sb, "No hay ajustes pendientes de envío.");
                Append(sb, "Fin de sincronización de ajustes.");

                resp.Resultado = sb.ToString();
                resp.Ajustes = new List<clsBeAjustesMI3>();

                tran.Commit();
                return resp;
            }      

            foreach (var ajEnc in ajustesPendEnvio)
            {
                // VB: vNoDocumento = "WMS" + Right("000000" & AjEnc.Idajusteenc, 6)
                var vNoDocumento = $"WMS{ajEnc.Idajusteenc.ToString().PadLeft(6, '0')}";
                vNDoc = vNoDocumento;

                var vistaDetalles = clsLn_vw_ajustes.Get_All_Pendientes_Envio(ajEnc.Idajusteenc, conn, tran) ?? new List<clsBe_vw_ajustes>();

                int vCorrelativoActual = 0;
                int detallesEnviados = 0;

                // VB: familia
                clsBeProducto_familia? beFamilia = null;
                if (ajEnc.IdProductoFamilia > 0)
                    beFamilia = clsLnProducto_familia.GetSingle(ajEnc.IdProductoFamilia, conn, tran);

                if (vistaDetalles.Count == 0)
                {                   
                    Append(sb, $"No hay detalle de ajustes válidos para el Id de ajuste #: {ajEnc.Idajusteenc}");
                    continue;
                }

                Append(sb, $"Detalle de ajustes para transacción: {ajEnc.Idajusteenc}");

                // VB: Bodega
                var beBodega = new clsBeBodega { IdBodega = ajEnc.IdBodega };
                clsLnBodega.GetSingle_By_Idbodega(ajEnc.IdBodega, conn, tran);

                var codigoBodegaWms = beBodega.Codigo;

                // En tu VB vNomenclaturaBase existe pero no se asigna; lo dejo igual
                string vNomenclaturaBase = "";

                foreach (var ajDet in vistaDetalles)
                {
                    // VB: cliente por AjDet.IdBodegaERP
                    var beCliente = clsLnCliente.Get_Single_By_Codigo(ajDet.IdBodegaERP.ToString(), conn, tran);

                    string vSerieBodega = "";
                    string codigoBodegaERP = "";

                    if (beCliente == null)
                    {
                        Append(sb, $"#EJC20200219_2214: No se encontró cliente/Serie para IdBodega: {ajEnc.IdBodega}");

                        if (beBodega.Interface_SAP)
                            codigoBodegaERP = beBodega.Codigo;
                    }
                    else
                    {
                        vSerieBodega = beCliente.Referencia;
                        codigoBodegaERP = beCliente.Codigo;
                    }

                    // VB: correlativo
                    if (vCorrelativoActual == 0)
                    {
                        var numericPart = ExtractNumericPartSafe(vNoDocumento);
                        vCorrelativoActual = numericPart + 1;
                    }
                    else if (vCorrelativoActual == 999999)
                    {
                        vCorrelativoActual = 1;
                    }
                    else
                    {
                        vCorrelativoActual += 1;
                    }

                    vNoDocumento = $"{vNomenclaturaBase}{vCorrelativoActual.ToString().PadLeft(6, '0')}";
                    Append(sb, $"Procesando ajuste número de documento: {vNoDocumento}");

                    // VB: GetSingle del detalle real
                    var beAjusteDet = new clsBeTrans_ajuste_det
                    {
                        IdAjusteDet = ajDet.IdAjusteDet,
                        IdAjusteEnc = ajEnc.Idajusteenc
                    };

                    clsLnTrans_ajuste_det.GetSingle(ref beAjusteDet, conn, tran);

                    if (beFamilia != null)
                        ajDet.Seccion = beFamilia.Nombre;
                   

                    if (ajDet.Modifica_Cantidad)
                    {
                        // Negativo
                        if (ajDet.Cantidad_original > ajDet.Cantidad_nueva)
                        {
                            var dif = Math.Round(ajDet.Cantidad_original - ajDet.Cantidad_nueva, 6);

                            try
                            {
                                var beAjusteMi3 = BuildAjusteMI3(ajEnc, 
                                                                 ajDet, 
                                                                 codigoBodegaWms, 
                                                                 codigoBodegaERP,
                                                                 vNoDocumento, 
                                                                 dif, 
                                                                 beCliente, 
                                                                 conn, 
                                                                 tran);

                                resp.Ajustes.Add(beAjusteMi3);

                                Append(sb, $"Procesando ajuste negativo para: {ajDet.Codigo_Producto} {ajDet.Nombre_Producto}");
                                detallesEnviados++;
                            }
                            catch (Exception)
                            {            
                                Append(sb, $"Error al procesar el ajuste #: {ajEnc.Idajusteenc}");
                                vCorrelativoActual -= 1;
                                throw;
                            }
                        }
                        // Positivo
                        else if (ajDet.Cantidad_original < ajDet.Cantidad_nueva)
                        {
                            var dif = Math.Round(ajDet.Cantidad_nueva - ajDet.Cantidad_original, 6);

                            try
                            {
                                var beAjusteMi3 = BuildAjusteMI3(ajEnc, ajDet, codigoBodegaWms, codigoBodegaERP,
                                    vNoDocumento, dif, beCliente, conn, tran);

                                resp.Ajustes.Add(beAjusteMi3);

                                Append(sb, $"Procesando ajuste positivo para: {ajDet.Codigo_Producto} {ajDet.Nombre_Producto}");

                                beAjusteDet.Enviado = true;

                                if (string.IsNullOrWhiteSpace(ajEnc.Referencia))
                                    ajEnc.Referencia = vNoDocumento;

                                detallesEnviados++;
                            }
                            catch (Exception)
                            {                 
                                Append(sb, $"Error al procesar el ajuste #: {ajEnc.Idajusteenc}");
                                vCorrelativoActual -= 1;
                                throw;
                            }
                        }
                    }
                    else
                    {
                        // VB: tipos que no se envían
                        beAjusteDet.Enviado = true;
                        clsLnTrans_ajuste_det.Actualizar_Estado_Enviado(beAjusteDet, conn, tran);
                        detallesEnviados++;
                    }
                 
                }

                resp.Resultado = sb.ToString();
            }

            Append(sb, "Fin de sincronización de ajustes.");
            resp.Resultado = sb.ToString();

            tran.Commit();
            return resp;
        }
        catch (Exception ex)
        {            
            Append(sb, $"Error al enviar ajustes a NAV: {Environment.NewLine}{ex.Message}-{vNDoc}");
            resp.Resultado = sb.ToString();

            throw; // mantiene stack
        }
    }

    private static clsBeAjustesMI3 BuildAjusteMI3(clsBeTrans_ajuste_enc ajEnc,
                                                  clsBe_vw_ajustes ajDet,
                                                  string codigoBodegaWms,
                                                  string codigoBodegaERP,
                                                  string noDocumento,
                                                  double cantidad,
                                                  clsBeCliente? beCliente,
                                                  SqlConnection conn,
                                                  SqlTransaction tran)
    {
        return new clsBeAjustesMI3
        { 
            IdAjusteEnc = ajDet.IdAjusteEnc,
            IdAjusteDet = ajDet.IdAjusteDet,
            Codigo_Bodega = codigoBodegaWms,
            Codigo_Bodega_ERP = codigoBodegaERP,
            NoDocumento = noDocumento,
            Codigo_Producto = ajDet.Codigo_Producto,
            TipoAjusteERP = (ajEnc.Ajuste_Por_Inventario > 0)
                ? ajDet.Codigo_Bodega
                : (beCliente?.Codigo ?? ""),
            TipoAjusteWMS = ajDet.Tipo_Ajuste,
            UMBas = ajDet.UMBas,
            Cantidad = cantidad,
            Lote = ajDet.Lote_Nuevo,
            Motivo_Ajuste = ajDet.Motivo_Ajuste,
            Observacion = ajDet.Observacion,
            Seccion = ajDet.Seccion,
            IdCentroCosto = ajEnc.IdCentroCosto,
            Codigo_Centro_Costo = clsLnCentro_costo.Get_Codigo_By_IdCentroCosto(ajEnc.IdCentroCosto, conn, tran),
            Talla = ajDet.Talla,
            Color = ajDet.Color,
            Centro_Costo_Erp = ajEnc.Centro_Costo_Erp,
            Centro_Costo_Dep_Erp = ajEnc.Centro_Costo_Dep_Erp,
            Centro_Costo_Dir_Erp = ajEnc.Centro_Costo_Dir_Erp
        };
    }

    private static void Append(StringBuilder sb, string msg) => sb.AppendLine(msg);

    private static int ExtractNumericPartSafe(string doc)
    {
        var digits = new string(doc.Where(char.IsDigit).ToArray());
        return int.TryParse(digits, out var n) ? n : 0;
    }
}