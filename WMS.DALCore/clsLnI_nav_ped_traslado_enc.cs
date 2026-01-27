using AppGlobal;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WMS.DALCore.Cliente;
using WMS.DALCore.I_nav_ped_traslado_det;
using WMS.DALCore.Road;
using WMS.DALCore.VW_Despacho_Rep;
using WMS.EntityCore;
using WMS.EntityCore.Cliente;
using WMS.EntityCore.Datos_Maestros;
using WMS.EntityCore.Log;
using WMS.EntityCore.Pedido;
using WMS.EntityCore.Producto;
using WMS.EntityCore.Propietario;
using WMS.EntityCore.Road;
using WMSWebAPI.Be;


namespace WMS.DALCore
{
    public static class clsLnI_nav_ped_traslado_enc
    {
        private static clsInsert Ins = new clsInsert();
        public static IConfiguration? lconfig = null;

        public static clsBeTrans_pe_enc? Importar_Pedido_Cliente_A_Tabla_Intermedia_If(clsBeI_nav_ped_traslado_enc BePedidoCliente,
                                                                                      ref string lblprg,
                                                                                      IConfiguration config)
        {
            clsBeTrans_pe_enc? result = null;

            SqlConnection? LocalConnection = null;
            SqlTransaction? LocalTransaction = null;
            int vIdBodegaOrigen = 0;
            int vIdPropietario = 0;
            int vIdPropitarioBodegaOrigen = 0;

            try
            {
                // Crear conexión local usando IConfiguration
                string? connectionString = config.GetConnectionString("CST");
                LocalConnection = new SqlConnection(connectionString);
                LocalConnection.Open();
                LocalTransaction = LocalConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

                // Obtener bodega área
                clsBeBodega_area BeBodegaArea = clsLnBodega_area.Get_Single_By_Codigo_Bodega(
                    BePedidoCliente.Transfer_from_Code, LocalConnection, LocalTransaction);

                vIdBodegaOrigen = clsLnBodega.Get_IdBodega_By_Codigo(
                    BePedidoCliente.Transfer_from_Code, LocalConnection, LocalTransaction);
                if (vIdBodegaOrigen == 0)
                {
                    if (BeBodegaArea != null)
                    {
                        vIdBodegaOrigen = BeBodegaArea.IdBodega;
                    }
                    else
                    {
                        throw new Exception($"El código de la bodega origen: {BePedidoCliente.Transfer_from_Code} no es válido");
                    }
                }

                // Obtener propietario
                vIdPropietario = clsLnPropietarios.Get_IdPropietario_By_Codigo(BePedidoCliente.Product_Owner_Code,
                                                                               LocalConnection,
                                                                               LocalTransaction);

                if (vIdPropietario == 0)
                {
                    throw new Exception($"El código de propietario: ({BePedidoCliente.Product_Owner_Code}) no es válido");
                }

                // Obtener propietario bodega origen
                vIdPropitarioBodegaOrigen = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(vIdPropietario,
                                                                                                                          vIdBodegaOrigen,
                                                                                                                          LocalConnection,
                                                                                                                          LocalTransaction);

                if (vIdPropitarioBodegaOrigen == 0)
                {
                    throw new Exception($"El código de propietario: ({BePedidoCliente.Product_Owner_Code}) de la bodega origen: ({BePedidoCliente.Transfer_from_Code}) no es válido");
                }

                // Importar traslado a tabla intermedia
                if (Importar_Traslado_A_Tabla_Intermedia(BePedidoCliente, lblprg, LocalConnection, LocalTransaction))
                {
                    // Obtener configuración directamente de la base de datos
                    clsBeI_nav_config_enc? BeConfigEnc = clsLnI_nav_config_enc.GetSingle_By_IdBodega_And_IdPropietario(
                        vIdBodegaOrigen, vIdPropietario, LocalConnection, LocalTransaction);

                    if (BeConfigEnc == null)
                    {
                        string vMsgEx = $"ERROR_202310311436: No existe la configuración asociada a la bodega: {vIdBodegaOrigen} en la tabla i_nav_config_enc configure los parámetros por defecto para la interfaz";
                        throw new Exception(vMsgEx);
                    }
                    else
                    {
                        clsBeTrans_pe_enc? BePedidoEnc = Imp_Ped_Trans_Env_Desde_Tab_Inter_A_WMS(BePedidoCliente,
                                                                                                 vIdBodegaOrigen,
                                                                                                 vIdPropietario,
                                                                                                 BeConfigEnc,
                                                                                                 LocalConnection,
                                                                                                 LocalTransaction,
                                                                                                 lblprg);

                        if (BePedidoEnc != null)
                        {
                            result = BePedidoEnc;
                        }                      
                    }
                }

                // Commit de la transacción local
                LocalTransaction.Commit();
            }
            catch (Exception)
            {
                // Rollback en caso de error
                if (LocalTransaction != null)
                {
                    LocalTransaction.Rollback();
                }
                throw;
            }
            finally
            {
                // Liberar recursos
                LocalTransaction?.Dispose();

                if (LocalConnection != null && LocalConnection.State == ConnectionState.Open)
                {
                    LocalConnection.Close();
                    LocalConnection.Dispose();
                }
            }

            return result;
        }

        public static clsBeTrans_pe_enc? Imp_Ped_Trans_Env_Desde_Tab_Inter_A_WMS(
    clsBeI_nav_ped_traslado_enc BeINavPedTrasladoEnc,
    int IdBodegaOrigen,
    int IdPropietarioBodegaOrigen,
    clsBeI_nav_config_enc BeConfigEnc,
    SqlConnection lConectionInterface,
    SqlTransaction lTransInterface,
    object lblprg)
        {
            // =========================
            // Guard clauses (evitan NRE)
            // =========================
            if (BeINavPedTrasladoEnc == null)
                throw new ArgumentNullException(nameof(BeINavPedTrasladoEnc));

            if (BeConfigEnc == null)
                throw new ArgumentNullException(nameof(BeConfigEnc));

            if (lConectionInterface == null)
                throw new ArgumentNullException(nameof(lConectionInterface));

            if (lTransInterface == null)
                throw new ArgumentNullException(nameof(lTransInterface));

            // lineas_Detalle puede venir null
            var lineasDetalle = BeINavPedTrasladoEnc.lineas_Detalle ?? new List<clsBeI_nav_ped_traslado_det>();
            
            string vCodigoCliente = "";
            clsBeTrans_pe_enc? pBePedidoEnc = null;
            var lLogErrorWMS = new List<clsBeLog_error_wms>();
            clsBeTrans_pe_enc? PedidoClienteExistente = null;
            clsBeTrans_pe_enc? PedidoClienteExistenteByCompany = null;
            clsBeCliente? BeCliente = null;

            int vContador_Lineas_Detalle_Pedido_Insertadas = 0;
            int vContador_Lineas_Detalle_Pedido_Insertadas_Tabla = 0;            

            clsBeRoad_ruta? BeRoadRuta = null;
            clsBeRoad_p_vendedor? BeRoadVendedor = null;

            DateTime vFechaInicio = DateTime.Now;
            bool vPedidoExistente = false;

            // Helpers locales
            static string SafeTrim(string? s) => (s ?? string.Empty).Trim();

            static string SafePrefix(string? companyCode)
            {
                companyCode = SafeTrim(companyCode);
                return companyCode.Length >= 1 ? companyCode.Substring(0, 1) : string.Empty;
            }

            try
            {                

                // si Status != 0 no hace nada (tu else estaba vacío)
                if (BeINavPedTrasladoEnc.Status != 0)
                    return null;

                if (lineasDetalle.Count <= 0)
                    return null;

                // =========================
                // Encabezado base
                // =========================
                pBePedidoEnc = new clsBeTrans_pe_enc
                {
                    Referencia = BeINavPedTrasladoEnc.No,
                    IdTipoPedido = (int)BeINavPedTrasladoEnc.Document_Type,
                    Codigo_Empresa_ERP = SafeTrim(BeINavPedTrasladoEnc.Company_Code)
                };

                // =========================
                // Existe por referencia
                // =========================
                PedidoClienteExistente = clsLnTrans_pe_enc.Get_Single_By_Referencia(
                    pBePedidoEnc, lConectionInterface, lTransInterface);

                PedidoClienteExistenteByCompany = clsLnTrans_pe_enc.Get_Single_By_Referencia_And_Company(
                    ref pBePedidoEnc, lConectionInterface, lTransInterface);

                if (PedidoClienteExistente != null && PedidoClienteExistenteByCompany != null)
                    return pBePedidoEnc;

                // Si existe uno sí y el otro no, ajustas referencia (pero cuida Company_Code null/short)
                if (!(PedidoClienteExistente == null && PedidoClienteExistenteByCompany == null))
                {
                    var pref = SafePrefix(BeINavPedTrasladoEnc.Company_Code);
                    pBePedidoEnc.Referencia = pref + SafeTrim(BeINavPedTrasladoEnc.No);
                }

                // =========================
                // Código cliente
                // =========================
                var transferTo = SafeTrim(BeINavPedTrasladoEnc.Transfer_to_Code);
                if (BeConfigEnc.Interface_SAP && !string.IsNullOrEmpty(SafeTrim(BeINavPedTrasladoEnc.Company_Code)))
                {
                    vCodigoCliente = SafePrefix(BeINavPedTrasladoEnc.Company_Code) + transferTo;
                }
                else
                {
                    vCodigoCliente = transferTo;
                }

                // =========================
                // Buscar cliente
                // =========================
                BeCliente = clsLnCliente.Get_Single_By_Codigo(vCodigoCliente, lConectionInterface, lTransInterface);

                if (BeCliente == null)
                {
                    // fallback: Transfer_to_Code directo
                    BeCliente = clsLnCliente.Get_Single_By_Codigo(transferTo, lConectionInterface, lTransInterface);

                    if (BeCliente == null)
                        throw new Exception($"No existe el cliente {transferTo} en maestro para pedido de traslado");
                }

                // =========================
                // Ruta / vendedor (inputs null-safe)
                // =========================
                BeRoadRuta = clsLnRoad_ruta.Get_IdRuta_By_Codigo(
                    SafeTrim(BeINavPedTrasladoEnc.Transfer_to_CodeField),
                    lConectionInterface, lTransInterface);

                BeRoadVendedor = clsLnRoad_p_vendedor.Get_Vendedor_By_Codigo(
                    SafeTrim(BeINavPedTrasladoEnc.Transfer_from_Contact),
                    lConectionInterface, lTransInterface);

                // =========================
                // Pedido anulado?
                // =========================
                bool vPedidoAnulado = PedidoClienteExistente?.Estado == "Anulado";

                // Si ya existe y no está anulado y existe by company
                if (PedidoClienteExistente != null && !vPedidoAnulado && PedidoClienteExistenteByCompany != null)
                {
                    pBePedidoEnc.Activo = true;
                    vPedidoExistente = true;
                    return pBePedidoEnc; // ya existe, retornas
                }

                // =========================
                // Si no existe: limpiar logs por referencia
                // =========================
                clsLnLog_error_wms.Eliminar_By_Referencia_Documento(
                    SafeTrim(BeINavPedTrasladoEnc.No),
                    lConectionInterface, lTransInterface);

                // =========================
                // Construir encabezado completo
                // =========================
                DateTime fechaBase = BeINavPedTrasladoEnc.Posting_Date;
                DateTime fechaFinal = new DateTime(fechaBase.Year, fechaBase.Month, fechaBase.Day,
                    DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                pBePedidoEnc.Fecha_Pedido = fechaFinal;
                pBePedidoEnc.Referencia = SafeTrim(BeINavPedTrasladoEnc.No);
                pBePedidoEnc.IdBodega = IdBodegaOrigen;

                pBePedidoEnc.Cliente = new clsBeCliente { IdCliente = BeCliente.IdCliente };
                pBePedidoEnc.IdCliente = BeCliente.IdCliente;

                pBePedidoEnc.Control_Ultimo_Lote = BeCliente.Control_ultimo_lote;
                pBePedidoEnc.IdMuelle = 1;

                pBePedidoEnc.PropietarioBodega = new clsBePropietario_bodega { IdPropietarioBodega = IdPropietarioBodegaOrigen };
                pBePedidoEnc.IdPropietarioBodega = IdPropietarioBodegaOrigen;

                pBePedidoEnc.TipoPedido = new clsBeTrans_pe_tipo();
                pBePedidoEnc.TipoPedido.IdTipoPedido = (BeINavPedTrasladoEnc.Document_Type == 0)
                    ? (int)clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente
                    : (int)BeINavPedTrasladoEnc.Document_Type;

                pBePedidoEnc.Hora_ini = DateTime.Now;
                pBePedidoEnc.Hora_fin = DateTime.Now.AddHours(1);
                pBePedidoEnc.HoraEntregaDesde = DateTime.Now;
                pBePedidoEnc.HoraEntregaHasta = DateTime.Now.AddHours(1);

                pBePedidoEnc.Ubicacion = "1";
                pBePedidoEnc.Estado = "Nuevo";
                pBePedidoEnc.No_despacho = 0;
                pBePedidoEnc.Activo = true;

                // BeConfigEnc.IdUsuario puede ser 0 pero no null; aun así es seguro
                pBePedidoEnc.User_agr = BeConfigEnc.IdUsuario.ToString();
                pBePedidoEnc.User_mod = BeConfigEnc.IdUsuario.ToString();
                pBePedidoEnc.Fec_agr = DateTime.Now;
                pBePedidoEnc.Fec_mod = DateTime.Now;

                pBePedidoEnc.Local = true;
                pBePedidoEnc.Pallet_primero = true;
                pBePedidoEnc.Dias_cliente = 0;
                pBePedidoEnc.Anulado = false;

                pBePedidoEnc.RoadFechaEntr = BeINavPedTrasladoEnc.Shipment_Date;
                pBePedidoEnc.RoadDirEntrega = SafeTrim(BeINavPedTrasladoEnc.Address);
                pBePedidoEnc.Observacion = SafeTrim(BeINavPedTrasladoEnc.Comments);
                pBePedidoEnc.EsExportacion = BeINavPedTrasladoEnc.IsExport;

                pBePedidoEnc.Codigo_Empresa_ERP = SafeTrim(BeINavPedTrasladoEnc.Company_Code);

                // Ruta fallback: RoadCodigoRuta (null-safe)
                if (BeRoadRuta != null)
                {
                    pBePedidoEnc.RoadIdRuta = BeRoadRuta.IdRuta;
                }
                else
                {
                    var codRuta = SafeTrim(BeINavPedTrasladoEnc.RoadCodigoRuta);
                    if (!string.IsNullOrEmpty(codRuta))
                    {
                        BeRoadRuta = clsLnRoad_ruta.Get_IdRuta_By_Codigo(codRuta, lConectionInterface, lTransInterface);
                        pBePedidoEnc.RoadIdRuta = BeRoadRuta?.IdRuta ?? 0;
                    }
                    else
                    {
                        pBePedidoEnc.RoadIdRuta = 0;
                    }
                }

                // Vendedor fallback: RoadCodigoVendedor
                if (BeRoadVendedor != null)
                {
                    pBePedidoEnc.RoadIdVendedor = BeRoadVendedor.IdVendedor;
                }
                else
                {
                    var codVend = SafeTrim(BeINavPedTrasladoEnc.RoadCodigoVendedor);
                    if (!string.IsNullOrEmpty(codVend))
                    {
                        BeRoadVendedor = clsLnRoad_p_vendedor.Get_Vendedor_By_Codigo(codVend, lConectionInterface, lTransInterface);
                        pBePedidoEnc.RoadIdVendedor = BeRoadVendedor?.IdVendedor ?? 0;
                    }
                    else
                    {
                        pBePedidoEnc.RoadIdVendedor = 0;
                    }
                }

                pBePedidoEnc.Enviado_A_ERP = false;
                pBePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino = SafeTrim(BeINavPedTrasladoEnc.Receipt_Document_Reference);
                pBePedidoEnc.IdTipoManufactura = Convert.ToInt32(BeINavPedTrasladoEnc.Manufacturing_Process);
                pBePedidoEnc.Bodega_origen = SafeTrim(BeINavPedTrasladoEnc.Transfer_from_Code);
                pBePedidoEnc.Bodega_destino = SafeTrim(BeINavPedTrasladoEnc.Transfer_to_CodeField);

                // Inserta encabezado
                clsLnTrans_pe_enc.Inserta_Encabezado(ref pBePedidoEnc, lConectionInterface, lTransInterface);

                // =========================
                // Tiempos cliente
                // =========================
                var pClienteTiemposList = clsLnCliente_tiempos.Get_All_Tiempos_By_IdCliente(
                    pBePedidoEnc.IdCliente, lConectionInterface, lTransInterface) ?? new List<clsBeCliente_tiempos>();

                // =========================
                // Insert detalle
                // =========================
                clsBeI_nav_ped_traslado_det? BeINAVPedDetAnt = null;
                clsBeTrans_pe_det? refBePedidoDet = null;
                clsBeTrans_pe_det? refBePedidoDetAnt = null;

                foreach (var PDet in lineasDetalle)
                {
                    if (PDet == null) continue;

                    string vCodigoProducto = SafeTrim(PDet.Item_No);
                    if (string.IsNullOrEmpty(vCodigoProducto))
                        throw new Exception("Detalle sin Item_No (código de producto).");

                    // Producto
                    clsBeProducto? BeProducto;
                    if (BeConfigEnc.Valida_Solo_Codigo)
                    {
                        BeProducto = clsLnProducto.Get_BeProducto_By_Only_Codigo(
                            vCodigoProducto, IdBodegaOrigen, lConectionInterface, lTransInterface);
                    }
                    else
                    {
                        BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(
                            vCodigoProducto, IdBodegaOrigen, lConectionInterface, lTransInterface);
                    }

                    if (BeProducto == null)
                        throw new Exception($"El código de producto: {vCodigoProducto} no existe o no está asociado con la bodega: {IdBodegaOrigen}");

                    // Normaliza código en el detalle
                    PDet.Item_No = SafeTrim(BeProducto.codigo);

                    // U.M.
                    var umCode = SafeTrim(PDet.Unit_of_Measure_Code);
                    if (string.IsNullOrEmpty(umCode))
                        throw new Exception($"La línea {PDet.Line_No} viene sin Unit_of_Measure_Code.");

                    var BeUnidadMedida = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario(
                        umCode, BeConfigEnc.IdPropietario, lConectionInterface, lTransInterface);

                    if (BeUnidadMedida == null)
                        throw new Exception($"La U.M básica de producto: {PDet.Item_No} no existe o no está definida: {umCode}");

                    BeProducto.UnidadMedida = BeUnidadMedida;

                    // Presentación (Variant_Code)
                    clsBeProducto_presentacion? BePresentacion = null;
                    var variant = SafeTrim(PDet.Variant_Code);
                    if (!string.IsNullOrEmpty(variant))
                    {
                        BePresentacion = clsLnProducto_presentacion.Get_Presentacion_By_IdProductoBodega_And_CodPres(
                            BeProducto.IdProductoBodega, variant, lConectionInterface, lTransInterface);

                        if (BePresentacion == null)
                            throw new Exception($"La Presentación de producto: {PDet.Item_No} no existe o no está definida: {variant}");
                    }

                    // Tiempos (ojo: Clasificacion/Familia pueden ser null)
                    int vDiasVencimientoCliente = 0;
                    int? idClas = BeProducto.Clasificacion?.IdClasificacion;
                    int? idFam = BeProducto.Familia?.IdFamilia;

                    clsBeCliente_tiempos? vClienteTiempo = null;
                    if (idClas.HasValue && idFam.HasValue)
                    {
                        vClienteTiempo = pClienteTiemposList.Find(x =>
                            x != null &&
                            x.IdClasificacion == idClas.Value &&
                            x.IdFamilia == idFam.Value);
                    }

                    if (vClienteTiempo != null)
                        vDiasVencimientoCliente = vClienteTiempo.Dias_Local;
                    
                    if (pBePedidoEnc == null)
                        throw new Exception("Encabezado de pedido nulo. No se pueden insertar líneas.");

                    // Inserta línea
                    if (Inserta_Linea_Detalle_Pedido(
                        pBePedidoEnc,
                        PDet,
                        BeProducto,
                        vDiasVencimientoCliente,
                        BeUnidadMedida,
                        BePresentacion,
                        BeCliente,
                        BeConfigEnc,
                        IdBodegaOrigen,
                        IdPropietarioBodegaOrigen,
                        lblprg,
                        lConectionInterface,
                        lTransInterface,
                        ref refBePedidoDet))
                    {
                        PDet.Status = 1;
                        PDet.Process_Result = "Ok";
                        clsLnI_nav_ped_traslado_det.Actualizar_Status_Det(PDet, lConectionInterface, lTransInterface);
                        vContador_Lineas_Detalle_Pedido_Insertadas++;
                    }
                    else
                    {
                        // tu lógica de rollback de bonificación + log_error_wms va aquí tal cual,
                        // pero agregando null-checks antes de usar refBePedidoDet / refBePedidoDetAnt.
                        PDet.Status = 0;
                        PDet.Process_Result = "ERROR_202310021910A: No se pudo completar la reserva, consulte log_error_wms.";
                        clsLnI_nav_ped_traslado_det.Actualizar_Status_Det(PDet, lConectionInterface, lTransInterface);
                        clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(PDet, lConectionInterface, lTransInterface);

                        // ... inserción en Log_error_wms (igual que tu código, con null-check de pBePedidoEnc)
                        if (pBePedidoEnc != null)
                        {
                            var BeEmpresa = clsLnEmpresa.GetSingle_By_IdBodega(IdBodegaOrigen, lConectionInterface, lTransInterface);
                            if (BeEmpresa != null)
                            {
                                string? vMensajeEx = lblprg?.ToString() ?? "";
                                var BeMensajeErrorWMS = new clsBeLog_error_wms
                                {
                                    IdError = clsLnLog_error_wms.MaxID(lConectionInterface, lTransInterface) + 1,
                                    IdEmpresa = BeEmpresa.IdEmpresa,
                                    IdBodega = IdBodegaOrigen,
                                    Fecha = DateTime.Now,
                                    MensajeError = "Error_202303011638A: " + vMensajeEx,
                                    Line_No = PDet.Line_No,
                                    UmBas = umCode,
                                    Variant_Code = variant,
                                    Cantidad = PDet.Quantity,
                                    Referencia_Documento = pBePedidoEnc.Referencia,
                                    Item_No = PDet.Item_No
                                };
                                clsLnLog_error_wms.Insertar(BeMensajeErrorWMS, lConectionInterface, lTransInterface);
                            }
                        }
                    }

                    BeINAVPedDetAnt = PDet;
                    refBePedidoDetAnt = refBePedidoDet;
                }

                if (pBePedidoEnc == null)
                    throw new Exception("Encabezado de pedido nulo. No se pueden insertar líneas.");

                // =========================
                // Validación final
                // =========================
                int vCantStockRes = clsLnStock_res.Get_Count_By_IdPedidoEnc(
                    pBePedidoEnc.IdPedidoEnc, lConectionInterface, lTransInterface);

                if (vContador_Lineas_Detalle_Pedido_Insertadas == 0 && !vPedidoExistente)
                {
                    if (!clsLnTrans_pe_enc.Tiene_Detalle(pBePedidoEnc.IdPedidoEnc, lConectionInterface, lTransInterface))
                    {
                        clsLnTrans_pe_enc.Eliminar_Encabezado_Pedido(pBePedidoEnc.IdPedidoEnc, lConectionInterface, lTransInterface);
                        return null;
                    }

                    vContador_Lineas_Detalle_Pedido_Insertadas_Tabla =
                        clsLnTrans_pe_det.Get_Count_Lines_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc, lConectionInterface, lTransInterface);

                    if (vContador_Lineas_Detalle_Pedido_Insertadas_Tabla == 0 || vCantStockRes == 0)
                    {
                        clsLnTrans_pe_det.Eliminar_Detalle_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc, lConectionInterface, lTransInterface);
                        clsLnTrans_pe_enc.Eliminar_Encabezado_Pedido(pBePedidoEnc.IdPedidoEnc, lConectionInterface, lTransInterface);
                        return null;
                    }
                }

                // Si insertaste todo o hay stock reservado, ok
                if (vContador_Lineas_Detalle_Pedido_Insertadas == lineasDetalle.Count || vCantStockRes > 0)
                    return pBePedidoEnc;

                // Pedido incompleto
                if (Convert.ToInt32(BeConfigEnc.Rechazar_pedido_incompleto) == 1)
                {
                    throw new Exception($"Pedido incompleto: insertadas={vContador_Lineas_Detalle_Pedido_Insertadas}, total={lineasDetalle.Count}");
                }

                return pBePedidoEnc;
            }
            catch (Exception ex)
            {
                // =========================
                // Construcción de mensaje (evita NRE)
                // =========================
                string vListaMensajeError = "";

                try
                {
                    // NOTA: aquí antes usabas lconfig (no existe en firma). Sin config no llames ese overload.
                    // Si tienes overload que recibe conexión/tx, úsalo.
                    string referencia = pBePedidoEnc?.Referencia ?? SafeTrim(BeINavPedTrasladoEnc.No);

                    // Si tienes este método con conn/tx:

                    if (lconfig != null)
                    {
                        if (pBePedidoEnc != null)
                        {
                            lLogErrorWMS = clsLnLog_error_wms.Get_All_By_Referencia_Documento(lconfig, referencia);
                        }
                        else
                        {
                            lLogErrorWMS = clsLnLog_error_wms.Get_All_By_Referencia_Documento(lconfig, referencia);
                        }
                    }

                    foreach (var Lwms in lLogErrorWMS)
                        if (Lwms != null && !string.IsNullOrEmpty(Lwms.MensajeError))
                            vListaMensajeError += Environment.NewLine + Lwms.MensajeError;
                }
                catch
                {
                    // Si falló el log, no tapes el error original
                }

                if (string.IsNullOrEmpty(vListaMensajeError))
                    throw;

                throw new Exception(ex.Message + Environment.NewLine + vListaMensajeError);
            }
        }

        private static bool Inserta_Linea_Detalle_Pedido(clsBeTrans_pe_enc BePedidoEnc,
                                                        clsBeI_nav_ped_traslado_det pBeTrasladoDet,
                                                        clsBeProducto pBePoducto,
                                                        int pDiasVencimientoCliente,
                                                        clsBeUnidad_medida pBeUnidadMedida,
                                                        clsBeProducto_presentacion? pBePresentacion,
                                                        clsBeCliente pBeCliente,
                                                        clsBeI_nav_config_enc pBeConfigEnc,
                                                        int pIdBodegaOrigen,
                                                        int pIdPropietarioBodega,
                                                        object? plblprg,
                                                        SqlConnection lConectionInterface,
                                                        SqlTransaction lTransactionInterface,
                                                        ref clsBeTrans_pe_det? BePedidoDet)
        {
            bool result = false;

            clsBeTrans_pe_det pBePedidoDet = new clsBeTrans_pe_det();
            clsBeStock_res pBeStockRes = new clsBeStock_res();

            BePedidoDet = null;

            try
            {
                clsBeBodega? BeBodega = new clsBeBodega();
                BeBodega = clsLnBodega.GetSingle_By_Idbodega(pBeConfigEnc.Idbodega,
                                                             lConectionInterface,
                                                             lTransactionInterface);

                pBePedidoDet = new clsBeTrans_pe_det();
                pBePedidoDet.IdPedidoDet = clsLnTrans_pe_det.MaxID(lConectionInterface, lTransactionInterface) + 1;
                pBePedidoDet.No_linea = pBeTrasladoDet.Line_No;
                pBePedidoDet.Atributo_variante_1 = pBeTrasladoDet.Variant_Code;
                pBePedidoDet.IdPedidoEnc = BePedidoEnc.IdPedidoEnc;
                pBePedidoDet.Producto = new clsBeProducto();
                pBePedidoDet.Producto.IdProducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(pBePoducto.IdProductoBodega,
                                                                                                     lConectionInterface,
                                                                                                     lTransactionInterface);
                pBePedidoDet.Producto.IdProductoBodega = pBePoducto.IdProductoBodega;
                pBePedidoDet.IdProductoBodega = pBePoducto.IdProductoBodega;
                pBePedidoDet.Codigo_Producto = pBeTrasladoDet.Item_No;
                pBePedidoDet.Producto.codigo = pBeTrasladoDet.Item_No;
                pBePedidoDet.Producto.nombre = clsPublic.Quitar_Caracteres_No_Permitidos(pBeTrasladoDet.Description);
                pBePedidoDet.Nombre_producto = clsPublic.Quitar_Caracteres_No_Permitidos(pBeTrasladoDet.Description);
                pBePedidoDet.IdUnidadMedidaBasica = pBeUnidadMedida.IdUnidadMedida;
                pBePedidoDet.Cantidad = pBeTrasladoDet.Quantity;
                pBePedidoDet.Peso = 0;
                pBePedidoDet.Precio = pBeTrasladoDet.Price;
                pBePedidoDet.No_recepcion = 0;
                pBePedidoDet.Cant_despachada = 0;
                pBePedidoDet.IdEstado = pBeConfigEnc.IdProductoEstado;
                pBePedidoDet.Ndias = pDiasVencimientoCliente;
                pBePedidoDet.Nom_estado = "Buen Estado";
                pBePedidoDet.IsNew = true;
                pBePedidoDet.Fec_agr = DateTime.Now;
                pBePedidoDet.User_agr = pBeConfigEnc.IdUsuario.ToString();
                pBePedidoDet.RoadDes = 0;
                pBePedidoDet.RoadDesMon = 0;
                pBePedidoDet.RoadPrecioDoc = pBeTrasladoDet.Price;
                pBePedidoDet.RoadTotal = Math.Round(pBeTrasladoDet.Price * pBeTrasladoDet.Quantity, 6);
                pBePedidoDet.RoadVAL1 = 0;
                pBePedidoDet.RoadVAL2 = "0";
                pBePedidoDet.Talla = pBeTrasladoDet.Size;
                pBePedidoDet.Color = pBeTrasladoDet.Color;

                clsBeProducto_talla_color? BeProductoTallaColor = null;

                if (BeBodega != null)
                    if (BeBodega.Control_Talla_Color)
                    {
                        BeProductoTallaColor = clsLnProducto_talla_color.Get_Single_By_IdProducto(pBePedidoDet.Producto.IdProducto,
                                                                                                  pBeTrasladoDet.Size,
                                                                                                  pBeTrasladoDet.Color,
                                                                                                  lConectionInterface,
                                                                                                  lTransactionInterface);

                        if (BeProductoTallaColor != null)
                        {
                            pBePedidoDet.IdProductoTallaColor = BeProductoTallaColor.IdProductoTallaColor;
                        }
                    }

                if (pBePresentacion != null)
                {
                    if (pBePresentacion.IdPresentacion != 0)
                    {
                        pBePedidoDet.Nom_presentacion = pBePresentacion.Nombre;
                        pBePedidoDet.IdPresentacion = pBePresentacion.IdPresentacion;
                    }
                    else
                    {
                        pBePedidoDet.Nom_presentacion = "";
                    }
                }
                else
                {
                    pBePedidoDet.Nom_presentacion = "";
                }

                pBePedidoDet.Nom_unid_med = pBeTrasladoDet.Unit_of_Measure_Code;
                pBePedidoDet.Nom_estado = "Buen Estado";
                pBeStockRes.IdStockRes = 0;
                pBeStockRes.IdTransaccion = BePedidoEnc.IdPedidoEnc;
                pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet;
                pBeStockRes.Indicador = "PED";
                pBeStockRes.Añada = 0;
                pBeStockRes.Cantidad = pBeTrasladoDet.Quantity;
                pBeStockRes.Estado = "PPC";
                pBePedidoDet.Ndias = pDiasVencimientoCliente;
                pBeStockRes.User_agr = pBeConfigEnc.IdUsuario.ToString();
                pBeStockRes.Fec_agr = DateTime.Now;
                pBeStockRes.User_mod = pBeConfigEnc.IdUsuario.ToString();
                pBeStockRes.Fec_mod = DateTime.Now;
                pBeStockRes.Host = "Interface";
                pBeStockRes.Talla = pBeTrasladoDet.Size;
                pBeStockRes.Color = pBeTrasladoDet.Color;

                clsBeProducto_talla_color? BePtc = clsLnProducto_talla_color.Get_Single_By_IdProductoBodega(pBePoducto.IdProductoBodega,
                                                                                                            pBeStockRes.Talla,
                                                                                                            pBeStockRes.Color,
                                                                                                            lConectionInterface,
                                                                                                            lTransactionInterface);
                if (BePtc != null)
                {
                    if (BePtc.IdProductoTallaColor != pBePedidoDet.IdProductoTallaColor)
                    {
                        throw new Exception("Discrepancia entre la asignación de talla y color");
                    }
                }
                if (BeProductoTallaColor != null) pBeStockRes.IdProductoTallaColor = pBePedidoDet.IdProductoTallaColor;

                double vCantidadEnteraPres = 0;
                double vCantidadDecimalUMBas = 0;
                double vCantidadSolicitadaPedido = 0;

                #region Procesar cantidades en fracción
                if (pBeConfigEnc.Convertir_decimales_a_umbas == 1 && pBeConfigEnc.Interface_SAP)
                {
                    if (pBePresentacion != null)
                    {
                        if (pBePresentacion.Factor > 0)
                        {
                            decimal cantidadDecimal;
                            if (decimal.TryParse(pBeTrasladoDet.Quantity.ToString(), out cantidadDecimal))
                            {
                                clsPublic.Split_Decimal(pBeTrasladoDet.Quantity, ref vCantidadEnteraPres, ref vCantidadDecimalUMBas);

                                vCantidadDecimalUMBas = Math.Round(vCantidadDecimalUMBas * pBePresentacion.Factor);
                                vCantidadEnteraPres = vCantidadEnteraPres * pBePresentacion.Factor;

                                if (vCantidadEnteraPres > 0)
                                {
                                    vCantidadSolicitadaPedido = vCantidadEnteraPres;
                                }
                                else
                                {
                                    vCantidadSolicitadaPedido = vCantidadDecimalUMBas;
                                    pBeStockRes.Atributo_Variante_1 = "";
                                    pBeStockRes.IdPresentacion = 0;
                                }
                            }
                            else
                            {
                                vCantidadSolicitadaPedido = pBeTrasladoDet.Quantity;
                            }
                        }
                        else
                        {
                            throw new Exception("ERROR_202210251745: El factor es 0 para la presentación NO se puede inferir la conversión.");
                        }
                    }
                    else
                    {
                        vCantidadSolicitadaPedido = pBeTrasladoDet.Quantity;
                    }
                }
                else
                {
                    vCantidadSolicitadaPedido = pBeTrasladoDet.Quantity;
                }
                #endregion

                List<clsBeProducto_estado> BeProductoEstadoList = new List<clsBeProducto_estado>();
                int vIdPropietario = clsLnPropietario_bodega.Get_IdPropietario_By_IdBodega_IdPropietarioBodega(pIdBodegaOrigen,
                                                                                                              pIdPropietarioBodega,
                                                                                                              lConectionInterface,
                                                                                                              lTransactionInterface);
                try
                {
                    if (BeBodega != null)
                    {
                        if (BeBodega.Interface_SAP && BeBodega.Restringir_areas_sap)
                        {
                            pBeStockRes.IdProductoEstado = clsLnProducto_estado.Get_IdEstado_By_Codigo_Area(BePedidoEnc.Bodega_origen,
                                                                                                            lConectionInterface,
                                                                                                            lTransactionInterface);
                        }
                        else
                        {
                            int vIdEstadoProductoInterface = pBeConfigEnc.IdProductoEstado;

                            BeProductoEstadoList = clsLnProducto_estado.Existe_IdEstado_By_IdPropietario(vIdPropietario,
                                                                                                         vIdEstadoProductoInterface,
                                                                                                         lConectionInterface,
                                                                                                         lTransactionInterface);

                            if (BeProductoEstadoList != null)
                            {
                                var firstItem = BeProductoEstadoList.FirstOrDefault();
                                if (firstItem != null)
                                {
                                    pBeStockRes.IdProductoEstado = firstItem.IdEstado;
                                }
                                else
                                {
                                    throw new Exception("ERR_202205121200A: Error al obtener el estado de producto por defecto para los parámetros IdPropietario: " + pIdPropietarioBodega + " and IdBodega: " + pIdBodegaOrigen);
                                }
                            }
                            else
                            {
                                throw new Exception("ERR_202205121200B: Error al obtener el estado de producto por defecto para los parámetros IdPropietario: " + pIdPropietarioBodega + " and IdBodega: " + pIdBodegaOrigen);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("ERES_TU: " + ex.Message);
                }

                pBeStockRes.IdPedido = BePedidoEnc.IdPedidoEnc;
                pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet;
                pBeStockRes.IdProductoBodega = pBePoducto.IdProductoBodega;
                pBeStockRes.IdPropietarioBodega = pIdPropietarioBodega;
                pBeStockRes.IdBodega = pIdBodegaOrigen;
                pBeStockRes.IdUnidadMedida = clsLnProducto.Get_Id_Unidad_Medida_By_Codigo(pBePedidoDet.Producto.codigo,
                                                                                          lConectionInterface,
                                                                                          lTransactionInterface);
                pBeStockRes.Atributo_Variante_1 = pBePedidoDet.Atributo_variante_1;
                pBeStockRes.Control_Ultimo_Lote = pBeCliente.Control_ultimo_lote;

                clsBeProducto_presentacion? BePresentacion2 = new clsBeProducto_presentacion();

                if (pBePedidoDet.IdPresentacion != 0)
                {
                    if (pBePedidoDet.Atributo_variante_1 != null)
                    {
                        BePresentacion2 = new clsBeProducto_presentacion();
                        BePresentacion2 = clsLnProducto_presentacion.Existe_Presentacion_By_Codigo(pBePedidoDet.Producto.IdProducto,
                                                                                                  pBePedidoDet.Atributo_variante_1,
                                                                                                  lConectionInterface,
                                                                                                  lTransactionInterface);

                        if (BePresentacion2 != null)
                        {
                            pBeStockRes.IdPresentacion = BePresentacion2.IdPresentacion;
                        }
                        else
                        {
                            pBeStockRes.IdPresentacion = 0;
                        }
                    }
                    else
                    {
                        pBeStockRes.IdPresentacion = 0;
                    }
                }

                if (vCantidadDecimalUMBas > 0)
                {
                    pBeStockRes.Cantidad = vCantidadSolicitadaPedido;
                    pBeStockRes.IdPresentacion = 0;
                }

                if (pBeStockRes.Control_Ultimo_Lote)
                {
                    pBeStockRes.Ultimo_Lote = clsLnVW_Despacho_Rep.Get_Ultimo_Lote_By_IdCliente(pBeCliente.IdCliente,
                                                                                                pBePedidoDet.Producto.IdProducto,
                                                                                                lConectionInterface,
                                                                                                lTransactionInterface);
                }

                if (Convert.ToInt32(pBeCliente.IdUbicacionAbastecerCon) != 0)
                {
                    pBeStockRes.IdUbicacionAbastecerCon = pBeCliente.IdUbicacionAbastecerCon;
                }
                else
                {
                    pBeStockRes.IdUbicacionAbastecerCon = 0;
                }

                try
                {
                    if (clsLnTrans_pe_det.Reservar_Stock_Por_Linea_Interface(pDiasVencimientoCliente,
                                                                            ref pBeTrasladoDet,
                                                                            ref pBePedidoDet,
                                                                            ref pBeStockRes,
                                                                            "Interface",
                                                                            pBeConfigEnc,
                                                                            pIdPropietarioBodega,
                                                                            lConectionInterface,
                                                                            lTransactionInterface))
                    {
                        result = true;

                        pBeTrasladoDet.Process_Result = "Ok";
                        clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(pBeTrasladoDet,
                                                                              lConectionInterface,
                                                                              lTransactionInterface);
                    }
                    else
                    {
                        string vMensajeEx = "";

                        bool tieneTallaOColor = !string.IsNullOrWhiteSpace(pBeTrasladoDet.Size) ||
                                                 !string.IsNullOrWhiteSpace(pBeTrasladoDet.Color);

                        if (pBeStockRes.IdUbicacionAbastecerCon == 0)
                        {
                            if (tieneTallaOColor)
                            {
                                vMensajeEx = string.Format(Environment.NewLine +
                                                        "Reserva fallida. Pedido {0}, línea {1}: {2} (T: {3}, C: {4} IdTc:{5} ) sin stock. Cant: {6}",
                                                        pBeTrasladoDet.NoEnc,
                                                        pBeTrasladoDet.Line_No,
                                                        pBeTrasladoDet.Item_No,
                                                        pBeTrasladoDet.Size,
                                                        pBeTrasladoDet.Color,
                                                        pBeStockRes.IdProductoTallaColor,
                                                        pBeTrasladoDet.Quantity);
                            }
                            else
                            {
                                vMensajeEx = string.Format(Environment.NewLine +
                                                            "Reserva fallida. Pedido {0}, línea {1}: {2} sin stock. Cant: {3}",
                                                            pBeTrasladoDet.NoEnc,
                                                            pBeTrasladoDet.Line_No,
                                                            pBeTrasladoDet.Item_No,
                                                            pBeTrasladoDet.Quantity);
                            }
                        }
                        else
                        {
                            if (tieneTallaOColor)
                            {
                                vMensajeEx = string.Format(Environment.NewLine +
                                                            "Reserva fallida. Pedido {0}, línea {1}: {2} (T: {3}, C: {4}) sin stock en ubicación {5}. Cant: {6}",
                                                            pBeTrasladoDet.NoEnc,
                                                            pBeTrasladoDet.Line_No,
                                                            pBeTrasladoDet.Item_No,
                                                            pBeTrasladoDet.Size,
                                                            pBeTrasladoDet.Color,
                                                            pBeStockRes.IdUbicacionAbastecerCon,
                                                            pBeTrasladoDet.Quantity);
                            }
                            else
                            {
                                vMensajeEx = string.Format(Environment.NewLine +
                                                            "Reserva fallida. Pedido {0}, línea {1}: {2} sin stock en ubicación {3}. Cant: {4}",
                                                            pBeTrasladoDet.NoEnc,
                                                            pBeTrasladoDet.Line_No,
                                                            pBeTrasladoDet.Item_No,
                                                            pBeStockRes.IdUbicacionAbastecerCon,
                                                            pBeTrasladoDet.Quantity);
                            }
                        }

                        pBeTrasladoDet.Process_Result = vMensajeEx;


                        clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(pBeTrasladoDet,
                                                                              lConectionInterface,
                                                                              lTransactionInterface);
                    }
                }
                catch (Exception ex)
                {
                    string vMensajeEx = string.Format(Environment.NewLine + "{0}{1}{2}{2}{2}{2} Documento:{7} línea:{3} U.M: {5} V.C: {6}",
                                                             ex.Message,
                                                             Environment.NewLine,
                                                             "\t",
                                                             pBeTrasladoDet.Line_No,
                                                             pBeTrasladoDet.Item_No,
                                                             pBeTrasladoDet.Unit_of_Measure_Code,
                                                             pBeTrasladoDet.Variant_Code,
                                                             BePedidoEnc.Referencia);

                    pBeTrasladoDet.Process_Result = vMensajeEx;

                    clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(pBeTrasladoDet,
                                                                          lConectionInterface,
                                                                          lTransactionInterface);

                    if ((int)pBeConfigEnc.Rechazar_pedido_incompleto == 1)
                    {
                        throw new Exception(vMensajeEx);
                    }
                }

                BePedidoDet = pBePedidoDet;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public static bool Importar_Traslado_A_Tabla_Intermedia(clsBeI_nav_ped_traslado_enc BePedidoCliente,
                                                              object lblprg,
                                                              SqlConnection lConnection,
                                                              SqlTransaction lTransaction)
        {
            int vContadorLineas = 0;
            clsBeI_nav_config_enc? BeConfingEnc = new clsBeI_nav_config_enc();
            bool result = false;

            try
            {
                clsBeProducto_bodega? BeProductoBodega = new clsBeProducto_bodega();
                clsBeBodega? BeBodega = new clsBeBodega();
                int vContador = 0;

                try
                {
                    if (!string.IsNullOrEmpty(BePedidoCliente.Company_Code))
                    {
                        if (!Exist_By_No_And_Company(BePedidoCliente.No, BePedidoCliente.Company_Code, BePedidoCliente.Document_Type, lConnection, lTransaction))
                        {
                            if (BePedidoCliente.Company_Code.Length > 1)
                            {
                                BePedidoCliente.No = BePedidoCliente.Company_Code.Substring(0, 1) + BePedidoCliente.No;
                            }
                            Insertar(BePedidoCliente, lConnection, lTransaction);
                        }
                    }
                    else if (!Exist(BePedidoCliente.No, BePedidoCliente.Document_Type, lConnection, lTransaction))
                    {
                        Insertar(BePedidoCliente, lConnection, lTransaction);
                    }

                    vContador += 1;
                    lTransaction.Save("Encabezado");

                    if (BePedidoCliente.lineas_Detalle != null)
                    {
                        foreach (clsBeI_nav_ped_traslado_det BeI_Nav_PedidoTrasladoDet in BePedidoCliente.lineas_Detalle)
                        {
                            try
                            {
                                BeI_Nav_PedidoTrasladoDet.NoEnc = BePedidoCliente.No;
                                if (BeI_Nav_PedidoTrasladoDet.Item_No != null)
                                    BeI_Nav_PedidoTrasladoDet.No = BeI_Nav_PedidoTrasladoDet.Item_No;
                                BeI_Nav_PedidoTrasladoDet.Variant_Code = BeI_Nav_PedidoTrasladoDet.Variant_Code;

                                if (BeI_Nav_PedidoTrasladoDet.Item_No != null)
                                {
                                    clsBeBodega_area BeBodegaArea = clsLnBodega_area.Get_Single_By_Codigo_Bodega(BePedidoCliente.Transfer_from_Code,
                                                                                                                lConnection,
                                                                                                                lTransaction);

                                    BeBodega = clsLnBodega.GetSingle_By_Codigo(BePedidoCliente.Transfer_from_Code,
                                                                               lConnection,
                                                                               lTransaction);

                                    if (BeBodega == null)
                                    {
                                        if (BeBodegaArea != null)
                                        {
                                            BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeBodegaArea.IdBodega, lConnection, lTransaction);

                                            if (BeBodega == null)
                                            {
                                                throw new Exception("ERROR_20231031A: La bodega: " + BePedidoCliente.Transfer_from_Code + " no existe.");
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("ERROR_20231031: La bodega: " + BePedidoCliente.Transfer_from_Code + " no existe.");
                                        }
                                    }

                                    BeConfingEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega(BeBodega.IdBodega, lConnection, lTransaction);

                                    // Siempre obtener de la base de datos - eliminado el manejo en memoria
                                    BeProductoBodega = clsLnProducto_bodega.Existe(BeI_Nav_PedidoTrasladoDet.Item_No,
                                                                                   BeBodega.IdBodega,
                                                                                   lConnection,
                                                                                   lTransaction);

                                    if (BeProductoBodega == null && BeConfingEnc != null)
                                    {
                                        if (BeConfingEnc.Equiparar_Productos)
                                        {
                                            BeProductoBodega = clsLnProducto_bodega.Existe_Parte_By_IdBodega(BeI_Nav_PedidoTrasladoDet.Item_No,
                                                                                                             BeBodega.IdBodega,
                                                                                                             lConnection,
                                                                                                             lTransaction);
                                            if (BeProductoBodega == null)
                                            {
                                                BeProductoBodega = clsLnProducto_bodega.Existe_NoSerie_By_IdBodega(BeI_Nav_PedidoTrasladoDet.Item_No,
                                                                                                                   BeBodega.IdBodega,
                                                                                                                   lConnection,
                                                                                                                   lTransaction);
                                            }
                                        }
                                    }

                                    if (BeProductoBodega == null)
                                    {
                                        throw new Exception("El producto: " + BeI_Nav_PedidoTrasladoDet.Item_No + " No está asociado a la bodega: " + BePedidoCliente.Transfer_from_Code + " o no existe en el maestro de materiales.");
                                    }

                                    if (BeI_Nav_PedidoTrasladoDet.Qty_to_Receive == 0)
                                    {
                                        if (clsLnI_nav_ped_traslado_det.Exist(BeI_Nav_PedidoTrasladoDet, lConnection, lTransaction))
                                        {
                                            clsLnI_nav_ped_traslado_det.ActualizarFromIn(BeI_Nav_PedidoTrasladoDet, lConnection, lTransaction);
                                        }
                                        else
                                        {
                                            clsLnI_nav_ped_traslado_det.Insertar(BeI_Nav_PedidoTrasladoDet, lConnection, lTransaction);
                                        }

                                        vContadorLineas += 1;
                                    }
                                }
                                else
                                {
                                    Debug.Print("_: " + BeI_Nav_PedidoTrasladoDet.Description);
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Pedido de compra sin lineas de detalle?");
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                result = (vContadorLineas > 0);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public static int Insertar(clsBeI_nav_ped_traslado_enc oBeI_nav_ped_traslado_enc, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Ins.Init("i_nav_ped_traslado_enc");
                Ins.Add("no", "@no", "F");
                Ins.Add("posting_date", "@posting_date", "F");
                Ins.Add("receipt_date", "@receipt_date", "F");
                Ins.Add("shipment_date", "@shipment_date", "F");
                Ins.Add("status", "@status", "F");
                Ins.Add("transfer_from_code", "@transfer_from_code", "F");
                Ins.Add("transfer_from_name", "@transfer_from_name", "F");
                Ins.Add("transfer_to_code", "@transfer_to_code", "F");
                Ins.Add("transfer_to_name", "@transfer_to_name", "F");
                Ins.Add("transfer_to_codefield", "@transfer_to_codefield", "F");
                Ins.Add("product_owner_code", "@product_owner_code", "F");
                Ins.Add("receipt_document_reference", "@receipt_document_reference", "F");
                Ins.Add("document_type", "@document_type", "F");
                Ins.Add("Manufacturing_Process", "@Manufacturing_Process", "F");
                Ins.Add("Address", "@Address", "F");
                Ins.Add("Comments", "@Comments", "F");
                Ins.Add("Company_Code", "@Company_Code", "F");
                Ins.Add("IsExport", "@IsExport", "F");

                if (oBeI_nav_ped_traslado_enc.External_Document_No != null)
                {
                    if (!string.IsNullOrEmpty(oBeI_nav_ped_traslado_enc.External_Document_No.Trim()))
                        Ins.Add("external_document_no", "@external_document_no", "F");
                }

                if (oBeI_nav_ped_traslado_enc.RoadCodigoRuta != null)
                {
                    if (!string.IsNullOrEmpty(oBeI_nav_ped_traslado_enc.RoadCodigoRuta.Trim()))
                        Ins.Add("RoadCodigoRuta", "@RoadCodigoRuta", "F");
                }

                if (oBeI_nav_ped_traslado_enc.RoadCodigoVendedor != null)
                {
                    if (!string.IsNullOrEmpty(oBeI_nav_ped_traslado_enc.RoadCodigoVendedor.Trim()))
                        Ins.Add("RoadCodigoVendedor", "@RoadCodigoVendedor", "F");
                }

                string sp = Ins.SQL();
                SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction);

                cmd.Parameters.Add(new SqlParameter("@NO", oBeI_nav_ped_traslado_enc.No));
                cmd.Parameters.Add(new SqlParameter("@POSTING_DATE", oBeI_nav_ped_traslado_enc.Posting_Date));
                cmd.Parameters.Add(new SqlParameter("@RECEIPT_DATE", oBeI_nav_ped_traslado_enc.Receipt_Date));
                cmd.Parameters.Add(new SqlParameter("@SHIPMENT_DATE", oBeI_nav_ped_traslado_enc.Shipment_Date));
                cmd.Parameters.Add(new SqlParameter("@STATUS", oBeI_nav_ped_traslado_enc.Status));
                cmd.Parameters.Add(new SqlParameter("@TRANSFER_FROM_CODE", oBeI_nav_ped_traslado_enc.Transfer_from_Code));
                cmd.Parameters.Add(new SqlParameter("@TRANSFER_FROM_NAME", oBeI_nav_ped_traslado_enc.Transfer_from_Name));
                cmd.Parameters.Add(new SqlParameter("@TRANSFER_TO_CODE", oBeI_nav_ped_traslado_enc.Transfer_to_Code));
                cmd.Parameters.Add(new SqlParameter("@TRANSFER_TO_NAME", oBeI_nav_ped_traslado_enc.Transfer_to_Name));
                cmd.Parameters.Add(new SqlParameter("@TRANSFER_TO_CODEFIELD", oBeI_nav_ped_traslado_enc.Transfer_to_CodeField));
                cmd.Parameters.Add(new SqlParameter("@PRODUCT_OWNER_CODE", oBeI_nav_ped_traslado_enc.Product_Owner_Code));
                cmd.Parameters.Add(new SqlParameter("@RECEIPT_DOCUMENT_REFERENCE", oBeI_nav_ped_traslado_enc.Receipt_Document_Reference));
                cmd.Parameters.Add(new SqlParameter("@DOCUMENT_TYPE", oBeI_nav_ped_traslado_enc.Document_Type));
                cmd.Parameters.Add(new SqlParameter("@MANUFACTURING_PROCESS", oBeI_nav_ped_traslado_enc.Manufacturing_Process));
                cmd.Parameters.Add(new SqlParameter("@ADDRESS", oBeI_nav_ped_traslado_enc.Address));
                cmd.Parameters.Add(new SqlParameter("@COMMENTS", oBeI_nav_ped_traslado_enc.Comments));
                cmd.Parameters.Add(new SqlParameter("@COMPANY_CODE", oBeI_nav_ped_traslado_enc.Company_Code));

                if (oBeI_nav_ped_traslado_enc.External_Document_No != null)
                {
                    if (!string.IsNullOrEmpty(oBeI_nav_ped_traslado_enc.External_Document_No.Trim()))
                        cmd.Parameters.Add(new SqlParameter("@EXTERNAL_DOCUMENT_NO", oBeI_nav_ped_traslado_enc.External_Document_No));
                }

                if (oBeI_nav_ped_traslado_enc.RoadCodigoRuta != null)
                {
                    if (!string.IsNullOrEmpty(oBeI_nav_ped_traslado_enc.RoadCodigoRuta.Trim()))
                        cmd.Parameters.Add(new SqlParameter("@RoadCodigoRuta", oBeI_nav_ped_traslado_enc.RoadCodigoRuta));
                }

                if (oBeI_nav_ped_traslado_enc.RoadCodigoVendedor != null)
                {
                    if (!string.IsNullOrEmpty(oBeI_nav_ped_traslado_enc.RoadCodigoVendedor.Trim()))
                        cmd.Parameters.Add(new SqlParameter("@RoadCodigoVendedor", oBeI_nav_ped_traslado_enc.RoadCodigoVendedor));
                }

                cmd.Parameters.Add(new SqlParameter("@ISEXPORT", oBeI_nav_ped_traslado_enc.IsExport));

                int rowsAffected = cmd.ExecuteNonQuery();

                cmd.Dispose();

                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool Exist_By_No_And_Company(string pNo, string pCompany, clsDataContractDI.tTipoDocumentoSalida pTipoDocumento, SqlConnection lConnection, SqlTransaction lTransaction)
        {
            try
            {
                const string sp = @"SELECT No FROM i_nav_ped_traslado_enc
                           Where(No = @No AND Document_Type = @Document_Type AND Company_Code = @Company_Code)";

                SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                SqlDataAdapter dad = new SqlDataAdapter(cmd);
                dad.SelectCommand.Parameters.Add(new SqlParameter("@NO", pNo));
                dad.SelectCommand.Parameters.Add(new SqlParameter("@Document_Type", pTipoDocumento));
                dad.SelectCommand.Parameters.Add(new SqlParameter("@Company_Code", pCompany));

                DataTable dt = new DataTable();
                dad.Fill(dt);

                return dt.Rows.Count > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool Exist(string pNo, clsDataContractDI.tTipoDocumentoSalida pTipoDocumento, SqlConnection lConnection, SqlTransaction lTransaction)
        {
            try
            {
                const string sp = @"SELECT No FROM i_nav_ped_traslado_enc Where(No = @No AND Document_Type = @Document_Type)";

                SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text };
                SqlDataAdapter dad = new SqlDataAdapter(cmd);
                dad.SelectCommand.Parameters.Add(new SqlParameter("@NO", pNo));
                dad.SelectCommand.Parameters.Add(new SqlParameter("@Document_Type", pTipoDocumento));

                DataTable dt = new DataTable();
                dad.Fill(dt);

                return dt.Rows.Count > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool Exist(IConfiguration config, string pNo)
        {
            bool Exist = false;

            try
            {
                const string sp = "SELECT No FROM I_nav_ped_traslado_enc Where(No = @No)";

                using (var lConnection = new SqlConnection(config.GetConnectionString("CST")))
                using (var cmd = new SqlCommand(sp, lConnection) { CommandType = CommandType.Text })
                using (var dad = new SqlDataAdapter(cmd))
                {
                    dad.SelectCommand.Parameters.Add(new SqlParameter("@NO", pNo));

                    var dt = new DataTable();
                    dad.Fill(dt);

                    Exist = dt.Rows.Count > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Exist;
        }
    }

}
