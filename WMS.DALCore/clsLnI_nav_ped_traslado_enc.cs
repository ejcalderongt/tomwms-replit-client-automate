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
                                // Procesar pedido
                                clsBeTrans_pe_enc BePedidoEnc = Imp_Ped_Trans_Env_Desde_Tab_Inter_A_WMS(BePedidoCliente, 
                                                                                                        vIdBodegaOrigen, 
                                                                                                        vIdPropitarioBodegaOrigen,
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
        public static clsBeTrans_pe_enc Imp_Ped_Trans_Env_Desde_Tab_Inter_A_WMS(clsBeI_nav_ped_traslado_enc BeINavPedTrasladoEnc,
                                                                               int IdBodegaOrigen,
                                                                               int IdPropietarioBodegaOrigen,
                                                                               clsBeI_nav_config_enc BeConfigEnc,
                                                                               SqlConnection lConectionInterface,
                                                                               SqlTransaction lTransInterface,
                                                                               object lblprg)
        {
            clsBeTrans_pe_enc? result = null;
            string vCodigoCliente = "";
            clsBeTrans_pe_enc? pBePedidoEnc = null;
            List<clsBeLog_error_wms> lLogErrorWMS = new List<clsBeLog_error_wms>();
            clsBeTrans_pe_enc? PedidoClienteExistente = null;
            clsBeCliente? BeCliente = new clsBeCliente();
            int vContador = 0;
            List<clsBeCliente_tiempos> pClienteTiemposList = new List<clsBeCliente_tiempos>();
            clsBeProducto? BeProducto = new clsBeProducto();
            clsBeProducto_bodega BeProductoBodega = new clsBeProducto_bodega();
            clsBeTrans_pe_det pBePedidoDet = new clsBeTrans_pe_det();
            clsBeCliente_tiempos? vClienteTiempo = new clsBeCliente_tiempos();
            int vDiasVencimientoCliente = 0;
            clsBeUnidad_medida? BeUnidadMedida = new clsBeUnidad_medida();
            clsBeProducto_presentacion? BePresentacion = new clsBeProducto_presentacion();
            int vContador_Lineas_Detalle_Pedido_Insertadas = 0;
            int vContador_Lineas_Detalle_Pedido_Insertadas_Tabla = 0;
            int VContadorBitacoraTOMWMS = 0;
            string vCodigoProducto = "";
            clsBeRoad_ruta? BeRoadRuta = new clsBeRoad_ruta();
            clsBeRoad_p_vendedor? BeRoadVendedor = new clsBeRoad_p_vendedor();
            string vMsgEx3 = "";
            DateTime vFechaInicio = DateTime.Now;
            bool vPedidoExistente = false;
            clsBeTrans_pe_enc? PedidoClienteExistenteByCompany = new clsBeTrans_pe_enc();

            try
            {
                VContadorBitacoraTOMWMS = 0;

                if (BeINavPedTrasladoEnc.Status == 0)
                {
                    if (BeINavPedTrasladoEnc.lineas_Detalle.Count > 0)
                    {
                        pBePedidoEnc = new clsBeTrans_pe_enc()
                        {
                            Referencia = BeINavPedTrasladoEnc.No,
                            IdTipoPedido = (int)BeINavPedTrasladoEnc.Document_Type,
                            Codigo_Empresa_ERP = BeINavPedTrasladoEnc.Company_Code.ToString()
                        };

                        PedidoClienteExistente = clsLnTrans_pe_enc.Get_Single_By_Referencia(pBePedidoEnc,
                                                                                            lConectionInterface,
                                                                                            lTransInterface);

                        PedidoClienteExistenteByCompany = clsLnTrans_pe_enc.Get_Single_By_Referencia_And_Company(ref pBePedidoEnc,
                                                                                                                 lConectionInterface,
                                                                                                                 lTransInterface);

                        if (PedidoClienteExistente != null && PedidoClienteExistenteByCompany != null)
                        {
                            result = pBePedidoEnc;
                            return result;
                        }
                        else
                        {
                            if (!(PedidoClienteExistente == null && PedidoClienteExistenteByCompany == null))
                            {
                                pBePedidoEnc.Referencia = BeINavPedTrasladoEnc.Company_Code.Substring(0, 1) + BeINavPedTrasladoEnc.No;
                            }
                        }

                        if (BeConfigEnc.Interface_SAP && !string.IsNullOrEmpty(BeINavPedTrasladoEnc.Company_Code))
                        {
                            vCodigoCliente = BeINavPedTrasladoEnc.Company_Code.Substring(0, 1) + BeINavPedTrasladoEnc.Transfer_to_Code;
                        }
                        else
                        {
                            vCodigoCliente = BeINavPedTrasladoEnc.Transfer_to_Code;
                        }

                        BeCliente = new clsBeCliente();
                        BeCliente = clsLnCliente.Get_Single_By_Codigo(vCodigoCliente,
                                                                      lConectionInterface,
                                                                      lTransInterface);

                        if (BeCliente == null)
                        {
                            var tocode = BeINavPedTrasladoEnc.Transfer_to_Code;

                            BeCliente = clsLnCliente.Get_Single_By_Codigo(tocode,
                                                                          lConectionInterface,
                                                                          lTransInterface);

                            if (BeCliente == null)
                            {
                                throw new Exception(string.Format("No existe el cliente {0} en maestro para pedido de traslado ", BeINavPedTrasladoEnc.Transfer_to_Code));
                            }
                        }

                        BeRoadRuta = clsLnRoad_ruta.Get_IdRuta_By_Codigo(BeINavPedTrasladoEnc.Transfer_to_CodeField,
                                                                         lConectionInterface,
                                                                         lTransInterface);

                        BeRoadVendedor = clsLnRoad_p_vendedor.Get_Vendedor_By_Codigo(BeINavPedTrasladoEnc.Transfer_from_Contact,
                                                                                     lConectionInterface,
                                                                                     lTransInterface);

                        bool vPedidoAnulado = false;

                        if (PedidoClienteExistente != null)
                        {
                            if (PedidoClienteExistente.Estado == "Anulado")
                            {
                                vPedidoAnulado = true;
                            }
                        }

                        if (PedidoClienteExistente != null && !vPedidoAnulado && PedidoClienteExistenteByCompany != null)
                        {
                            pBePedidoEnc.Activo = true;
                            result = pBePedidoEnc;
                            vPedidoExistente = true;
                        }
                        else
                        {
                            clsLnLog_error_wms.Eliminar_By_Referencia_Documento(BeINavPedTrasladoEnc.No,
                                                                                lConectionInterface,
                                                                                lTransInterface);

                            DateTime fechaBase = BeINavPedTrasladoEnc.Posting_Date;
                            DateTime fechaFinal = new DateTime(fechaBase.Year, fechaBase.Month, fechaBase.Day,
                                          DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                            pBePedidoEnc.Fecha_Pedido = fechaFinal;
                            pBePedidoEnc.Referencia = BeINavPedTrasladoEnc.No;
                            pBePedidoEnc.IdBodega = IdBodegaOrigen;
                            pBePedidoEnc.Cliente = new clsBeCliente();
                            pBePedidoEnc.Cliente.IdCliente = BeCliente.IdCliente;
                            pBePedidoEnc.IdCliente = BeCliente.IdCliente;
                            pBePedidoEnc.Control_Ultimo_Lote = BeCliente.Control_ultimo_lote;
                            pBePedidoEnc.IdMuelle = 1;
                            pBePedidoEnc.PropietarioBodega = new clsBePropietario_bodega();
                            pBePedidoEnc.PropietarioBodega.IdPropietarioBodega = IdPropietarioBodegaOrigen;
                            pBePedidoEnc.IdPropietarioBodega = IdPropietarioBodegaOrigen;
                            pBePedidoEnc.TipoPedido = new clsBeTrans_pe_tipo();

                            if (BeINavPedTrasladoEnc.Document_Type == 0)
                            {
                                pBePedidoEnc.TipoPedido.IdTipoPedido = (int)clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente;
                            }
                            else
                            {
                                pBePedidoEnc.TipoPedido.IdTipoPedido = (int)BeINavPedTrasladoEnc.Document_Type;
                            }

                            pBePedidoEnc.Hora_ini = DateTime.Now;
                            pBePedidoEnc.Hora_fin = DateTime.Now.AddHours(1);
                            pBePedidoEnc.HoraEntregaDesde = DateTime.Now;
                            pBePedidoEnc.HoraEntregaHasta = DateTime.Now.AddHours(1);
                            pBePedidoEnc.Ubicacion = "1";
                            pBePedidoEnc.Estado = "Nuevo";
                            pBePedidoEnc.No_despacho = 0;
                            pBePedidoEnc.Activo = true;
                            pBePedidoEnc.User_agr = BeConfigEnc.IdUsuario.ToString();
                            pBePedidoEnc.Fec_agr = DateTime.Now;
                            pBePedidoEnc.User_mod = BeConfigEnc.IdUsuario.ToString();
                            pBePedidoEnc.Fec_mod = DateTime.Now;
                            pBePedidoEnc.Local = true;
                            pBePedidoEnc.Pallet_primero = true;
                            pBePedidoEnc.Dias_cliente = 0;
                            pBePedidoEnc.Anulado = false;
                            pBePedidoEnc.IdPickingEnc = 0;
                            pBePedidoEnc.RoadKilometraje = 0;
                            pBePedidoEnc.RoadFechaEntr = BeINavPedTrasladoEnc.Shipment_Date;
                            pBePedidoEnc.RoadDirEntrega = "";
                            pBePedidoEnc.RoadTotal = 0;
                            pBePedidoEnc.RoadDesMonto = 0;
                            pBePedidoEnc.RoadImpMonto = 0;
                            pBePedidoEnc.RoadPeso = 0;
                            pBePedidoEnc.RoadBandera = "0";
                            pBePedidoEnc.RoadStatCom = "";
                            pBePedidoEnc.RoadCalcoBJ = "0";
                            pBePedidoEnc.RoadImpres = 0;
                            pBePedidoEnc.RoadADD1 = "";
                            pBePedidoEnc.RoadADD2 = "";
                            pBePedidoEnc.RoadADD3 = "";
                            pBePedidoEnc.RoadStatProc = "0";
                            pBePedidoEnc.RoadRechazado = false;
                            pBePedidoEnc.RoadRazon_Rechazado = "0";
                            pBePedidoEnc.RoadInformado = false;
                            pBePedidoEnc.RoadSucursal = "";
                            pBePedidoEnc.RoadIdDespacho = 0;
                            pBePedidoEnc.RoadIdFacturacion = 0;
                            pBePedidoEnc.Codigo_Empresa_ERP = BeINavPedTrasladoEnc.Company_Code;

                            if (BeRoadRuta != null)
                            {
                                pBePedidoEnc.RoadIdRuta = BeRoadRuta.IdRuta;
                            }
                            else
                            {
                                if (BeINavPedTrasladoEnc.RoadCodigoRuta != null)
                                {
                                    if (!string.IsNullOrEmpty(BeINavPedTrasladoEnc.RoadCodigoRuta.Trim()))
                                    {
                                        BeRoadRuta = clsLnRoad_ruta.Get_IdRuta_By_Codigo(BeINavPedTrasladoEnc.RoadCodigoRuta,
                                                                                        lConectionInterface,
                                                                                        lTransInterface);
                                    }

                                    if (BeRoadRuta != null)
                                    {
                                        pBePedidoEnc.RoadIdRuta = BeRoadRuta.IdRuta;
                                    }
                                    else
                                    {
                                        pBePedidoEnc.RoadIdRuta = 0;
                                    }
                                }
                            }

                            if (BeRoadVendedor != null)
                            {
                                pBePedidoEnc.RoadIdVendedor = BeRoadVendedor.IdVendedor;
                            }
                            else
                            {
                                if (BeINavPedTrasladoEnc.RoadCodigoVendedor != null)
                                {
                                    if (!string.IsNullOrEmpty(BeINavPedTrasladoEnc.RoadCodigoVendedor.Trim()))
                                    {
                                        BeRoadVendedor = clsLnRoad_p_vendedor.Get_Vendedor_By_Codigo(BeINavPedTrasladoEnc.RoadCodigoVendedor,
                                                                                                     lConectionInterface,
                                                                                                     lTransInterface);
                                    }

                                    if (BeRoadVendedor != null)
                                    {
                                        pBePedidoEnc.RoadIdVendedor = BeRoadVendedor.IdVendedor;
                                    }
                                    else
                                    {
                                        pBePedidoEnc.RoadIdVendedor = 0;
                                    }
                                }
                            }

                            pBePedidoEnc.RoadIdRutaDespacho = 0;
                            pBePedidoEnc.RoadIdVendedorDespacho = 0;
                            pBePedidoEnc.Enviado_A_ERP = false;
                            pBePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino = BeINavPedTrasladoEnc.Receipt_Document_Reference;
                            pBePedidoEnc.IdTipoManufactura = Convert.ToInt32(BeINavPedTrasladoEnc.Manufacturing_Process);
                            pBePedidoEnc.Bodega_origen = BeINavPedTrasladoEnc.Transfer_from_Code;
                            pBePedidoEnc.Bodega_destino = BeINavPedTrasladoEnc.Transfer_to_CodeField;
                            pBePedidoEnc.RoadDirEntrega = BeINavPedTrasladoEnc.Address;
                            pBePedidoEnc.Observacion = BeINavPedTrasladoEnc.Comments;
                            pBePedidoEnc.EsExportacion = BeINavPedTrasladoEnc.IsExport;

                            clsLnTrans_pe_enc.Inserta_Encabezado(ref pBePedidoEnc,
                                                                 lConectionInterface,
                                                                 lTransInterface);

                            pClienteTiemposList = clsLnCliente_tiempos.Get_All_Tiempos_By_IdCliente(pBePedidoEnc.IdCliente,
                                                                                                    lConectionInterface,
                                                                                                    lTransInterface);

                            clsBeI_nav_ped_traslado_det BeINAVPedDetAnt = new clsBeI_nav_ped_traslado_det();
                            clsBeTrans_pe_det? refBePedidoDet = new clsBeTrans_pe_det();
                            clsBeTrans_pe_det? refBePedidoDetAnt = new clsBeTrans_pe_det();

                            foreach (clsBeI_nav_ped_traslado_det PDet in BeINavPedTrasladoEnc.lineas_Detalle)
                            {
                                vCodigoProducto = PDet.Item_No;
                                BeProducto = new clsBeProducto();

                                if (BeConfigEnc.Valida_Solo_Codigo)
                                {
                                    BeProducto = clsLnProducto.Get_BeProducto_By_Only_Codigo(vCodigoProducto,
                                                                                             IdBodegaOrigen,
                                                                                             lConectionInterface,
                                                                                             lTransInterface);
                                }
                                else
                                {
                                    BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(vCodigoProducto,
                                                                                        IdBodegaOrigen,
                                                                                        lConectionInterface,
                                                                                        lTransInterface);
                                }

                                if (BeProducto == null)
                                {
                                    string vMsgEx1 = "El código de producto: " + PDet.Item_No + " no existe o no está asociado con el código de bodega: " + IdBodegaOrigen;
                                    throw new Exception(vMsgEx1);
                                }

                                PDet.Item_No = BeProducto.codigo;

                                BeUnidadMedida = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario(PDet.Unit_of_Measure_Code,
                                                                                                       BeConfigEnc.IdPropietario,
                                                                                                       lConectionInterface,
                                                                                                       lTransInterface);

                                if (BeUnidadMedida == null)
                                {
                                    string vMsgEx2 = "La U.M básica de producto: " + PDet.Item_No + " no existe o no está definida: " + PDet.Unit_of_Measure_Code;
                                    throw new Exception(vMsgEx2);
                                }
                                else
                                {
                                    BeProducto.UnidadMedida = BeUnidadMedida;
                                }

                                if (PDet.Variant_Code != null)
                                {
                                    BePresentacion = new clsBeProducto_presentacion();
                                    BePresentacion = clsLnProducto_presentacion.Get_Presentacion_By_IdProductoBodega_And_CodPres(BeProducto.IdProductoBodega,
                                                                                                                                 PDet.Variant_Code,
                                                                                                                                 lConectionInterface,
                                                                                                                                 lTransInterface);

                                    if (BePresentacion == null)
                                    {
                                        vMsgEx3 = "La Presentacion de producto: " + PDet.Item_No + " no existe o no está definida: " + PDet.Variant_Code + " Código Killios " + BeProducto.noparte + " Código Garesa: " + BeProducto.noserie;
                                        throw new Exception(vMsgEx3);
                                    }
                                }
                                else
                                {
                                    BePresentacion = null;
                                }

                                vClienteTiempo = pClienteTiemposList.Find(x =>
                                                                          x.IdClasificacion == BeProducto.Clasificacion.IdClasificacion &&
                                                                          x.IdFamilia == BeProducto.Familia.IdFamilia);

                                vMsgEx3 = " El IdClasificación de producto es: " + BeProducto.Clasificacion.IdClasificacion +
                                                        " El IdFamilia es: " + BeProducto.Familia.IdFamilia;

                                if (vClienteTiempo != null)
                                {
                                    vDiasVencimientoCliente = vClienteTiempo.Dias_Local;
                                }

                                if (PedidoClienteExistente == null)
                                {
                                    try
                                    {
                                        if (PDet.Item_No == "AG00023260" && PDet.Line_No == 21)
                                        {
                                            Debug.WriteLine("espera");
                                        }

                                        if (Inserta_Linea_Detalle_Pedido(pBePedidoEnc,
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

                                            clsBeI_nav_ped_traslado_det vPedidoDet = PDet;
                                            clsLnI_nav_ped_traslado_det.Actualizar_Status_Det(vPedidoDet,
                                                                                              lConectionInterface,
                                                                                              lTransInterface);
                                            vContador_Lineas_Detalle_Pedido_Insertadas += 1;
                                        }
                                        else
                                        {
                                            if (BeConfigEnc.Inferir_Bonificacion_Pedido_SAP)
                                            {
                                                if (BeConfigEnc.Rechazar_Bonificacion_Incompleta)
                                                {
                                                    if (BeINAVPedDetAnt != null && refBePedidoDetAnt != null && refBePedidoDet != null)
                                                    {
                                                        if (BeINAVPedDetAnt.Item_No == PDet.Item_No)
                                                        {
                                                            clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet(refBePedidoDetAnt.IdPedidoDet,
                                                                                                                   lConectionInterface,
                                                                                                                   lTransInterface);

                                                            clsLnTrans_pe_det.Eliminar_Detalle_By_IdPedidoDet(pBePedidoEnc.IdPedidoEnc,
                                                                                                              refBePedidoDetAnt.IdPedidoDet,
                                                                                                              lConectionInterface,
                                                                                                              lTransInterface);

                                                            clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet(refBePedidoDet.IdPedidoDet,
                                                                                                                   lConectionInterface,
                                                                                                                   lTransInterface);

                                                            clsLnTrans_pe_det.Eliminar_Detalle_By_IdPedidoDet(pBePedidoEnc.IdPedidoEnc,
                                                                                                              refBePedidoDet.IdPedidoDet,
                                                                                                              lConectionInterface,
                                                                                                              lTransInterface);
                                                        }
                                                    }
                                                }
                                            }

                                            PDet.Status = 0;

                                            clsBeI_nav_ped_traslado_det vPedidoDet = PDet;
                                            clsLnI_nav_ped_traslado_det.Actualizar_Status_Det(vPedidoDet,
                                                                                              lConectionInterface,
                                                                                              lTransInterface);

                                            string? vMensajeEx = "";
                                            if (lblprg != null)
                                            {
                                                vMensajeEx = lblprg.ToString();
                                            }

                                            vPedidoDet.Process_Result = "ERROR_202310021910A: No se pudo completar la reserva, consulte log_error_wms.";
                                            clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(vPedidoDet,
                                                                                                  lConectionInterface,
                                                                                                  lTransInterface);

                                            clsBeEmpresa? BeEmpresa = new clsBeEmpresa();
                                            BeEmpresa = clsLnEmpresa.GetSingle_By_IdBodega(IdBodegaOrigen,
                                                                                           lConectionInterface,
                                                                                           lTransInterface);

                                            if (BeEmpresa != null)
                                            {
                                                clsBeLog_error_wms BeMensajeErrorWMS = new clsBeLog_error_wms();
                                                BeMensajeErrorWMS.IdError = clsLnLog_error_wms.MaxID(lConectionInterface, lTransInterface) + 1;
                                                BeMensajeErrorWMS.IdEmpresa = BeEmpresa.IdEmpresa;
                                                BeMensajeErrorWMS.IdBodega = IdBodegaOrigen;
                                                BeMensajeErrorWMS.Fecha = DateTime.Now;
                                                BeMensajeErrorWMS.MensajeError = "Error_202303011638A: " + vMensajeEx;
                                                BeMensajeErrorWMS.Line_No = PDet.Line_No;
                                                BeMensajeErrorWMS.UmBas = PDet.Unit_of_Measure_Code;
                                                BeMensajeErrorWMS.Variant_Code = PDet.Variant_Code;
                                                BeMensajeErrorWMS.Cantidad = PDet.Quantity;
                                                BeMensajeErrorWMS.Referencia_Documento = pBePedidoEnc.Referencia;
                                                BeMensajeErrorWMS.Item_No = PDet.Item_No;
                                                clsLnLog_error_wms.Insertar(BeMensajeErrorWMS, lConectionInterface, lTransInterface);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        PDet.Status = 0;
                                        PDet.Process_Result = ex.Message;
                                        clsLnI_nav_ped_traslado_det.Actualizar_Status_Det(PDet, lConectionInterface, lTransInterface);
                                        throw;
                                    }
                                }
                                else
                                {
                                    if (!clsLnTrans_pe_det.Existe(PedidoClienteExistente.IdPedidoEnc,
                                                                  PDet.Line_No,
                                                                  ref pBePedidoDet,
                                                                  PDet.No,
                                                                  lConectionInterface,
                                                                  lTransInterface))
                                    {
                                        try
                                        {
                                            if (Inserta_Linea_Detalle_Pedido(pBePedidoEnc,
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
                                                clsLnI_nav_ped_traslado_det.Actualizar_Status_Det(PDet,
                                                                                                  lConectionInterface,
                                                                                                  lTransInterface);

                                                vContador_Lineas_Detalle_Pedido_Insertadas += 1;
                                            }
                                            else
                                            {
                                                if (BeConfigEnc.Inferir_Bonificacion_Pedido_SAP)
                                                {
                                                    if (BeConfigEnc.Rechazar_Bonificacion_Incompleta)
                                                    {
                                                        if (BeINAVPedDetAnt != null && refBePedidoDet != null && refBePedidoDetAnt != null)
                                                        {
                                                            if (BeINAVPedDetAnt.Item_No == PDet.Item_No)
                                                            {
                                                                clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet(refBePedidoDetAnt.IdPedidoDet,
                                                                                                                       lConectionInterface,
                                                                                                                       lTransInterface);

                                                                clsLnTrans_pe_det.Eliminar_Detalle_By_IdPedidoDet(pBePedidoEnc.IdPedidoEnc,
                                                                                                                  refBePedidoDetAnt.IdPedidoDet,
                                                                                                                  lConectionInterface,
                                                                                                                  lTransInterface);

                                                                clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet(refBePedidoDet.IdPedidoDet,
                                                                                                                       lConectionInterface,
                                                                                                                       lTransInterface);

                                                                clsLnTrans_pe_det.Eliminar_Detalle_By_IdPedidoDet(pBePedidoEnc.IdPedidoEnc,
                                                                                                                  refBePedidoDet.IdPedidoDet,
                                                                                                                  lConectionInterface,
                                                                                                                  lTransInterface);
                                                            }
                                                        }
                                                    }
                                                }

                                                string vMensajeEx = string.Format(Environment.NewLine + "ERROR_202310021911: Al reservar stock para el pedido: {0} Línea: {1} Código_Producto: {3} U.M.: {4} V.C.: {5} Descripción del error: {2} Cantidad: {6}", PDet.NoEnc,
                                                                                        PDet.Line_No,
                                                                                        "No se pudo completar la reserva",
                                                                                        PDet.Item_No,
                                                                                        PDet.Unit_of_Measure_Code,
                                                                                        PDet.Variant_Code,
                                                                                        PDet.Quantity);

                                                PDet.Process_Result = "ERROR_202310021911A: No se pudo completar la reserva, consulte log_error_wms.";
                                                clsBeI_nav_ped_traslado_det vPedidoDet = PDet;
                                                clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(vPedidoDet,
                                                                                                      lConectionInterface,
                                                                                                      lTransInterface);

                                                clsBeEmpresa? BeEmpresa = new clsBeEmpresa();
                                                BeEmpresa = clsLnEmpresa.GetSingle_By_IdBodega(IdBodegaOrigen,
                                                                                               lConectionInterface,
                                                                                               lTransInterface);

                                                if (BeEmpresa != null)
                                                {
                                                    clsBeLog_error_wms BeMensajeErrorWMS = new clsBeLog_error_wms();
                                                    BeMensajeErrorWMS.IdError = clsLnLog_error_wms.MaxID(lConectionInterface, lTransInterface) + 1;
                                                    BeMensajeErrorWMS.IdEmpresa = BeEmpresa.IdEmpresa;
                                                    BeMensajeErrorWMS.IdBodega = pBePedidoEnc.IdBodega;
                                                    BeMensajeErrorWMS.Fecha = DateTime.Now;
                                                    BeMensajeErrorWMS.MensajeError = "Error_202303011638: " + vMensajeEx;
                                                    BeMensajeErrorWMS.Line_No = PDet.Line_No;
                                                    BeMensajeErrorWMS.UmBas = PDet.Unit_of_Measure_Code;
                                                    BeMensajeErrorWMS.Variant_Code = PDet.Variant_Code;
                                                    BeMensajeErrorWMS.Cantidad = PDet.Quantity;
                                                    BeMensajeErrorWMS.Referencia_Documento = pBePedidoEnc.Referencia;
                                                    BeMensajeErrorWMS.Item_No = PDet.Item_No;
                                                    clsLnLog_error_wms.Insertar(BeMensajeErrorWMS, lConectionInterface, lTransInterface);
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            PDet.Status = 0;
                                            PDet.Process_Result = ex.Message;
                                            clsLnI_nav_ped_traslado_det.Actualizar_Status_Det(PDet, lConectionInterface, lTransInterface);
                                            throw new Exception(string.Format("#ERROR_202112271234: {0} ", ex.Message));
                                        }
                                    }
                                }

                                BeINAVPedDetAnt = PDet;
                                refBePedidoDetAnt = refBePedidoDet;
                            }

                            try
                            {
                                int vCantStockRes = clsLnStock_res.Get_Count_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc,
                                                                                           lConectionInterface,
                                                                                           lTransInterface);

                                if (vContador_Lineas_Detalle_Pedido_Insertadas == 0 && !vPedidoExistente)
                                {
                                    try
                                    {
                                        if (!clsLnTrans_pe_enc.Tiene_Detalle(pBePedidoEnc.IdPedidoEnc,
                                                                           lConectionInterface,
                                                                           lTransInterface))
                                        {
                                            clsLnTrans_pe_enc.Eliminar_Encabezado_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                                         lConectionInterface,
                                                                                         lTransInterface);
                                        }
                                        else
                                        {
                                            vContador_Lineas_Detalle_Pedido_Insertadas_Tabla = clsLnTrans_pe_det.Get_Count_Lines_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc,
                                                                                                                                                lConectionInterface,
                                                                                                                                                lTransInterface);

                                            if (vContador_Lineas_Detalle_Pedido_Insertadas_Tabla == 0 || vCantStockRes == 0)
                                            {
                                                clsLnTrans_pe_det.Eliminar_Detalle_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc,
                                                                                                  lConectionInterface,
                                                                                                  lTransInterface);

                                                clsLnTrans_pe_enc.Eliminar_Encabezado_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                                             lConectionInterface,
                                                                                             lTransInterface);
                                            }
                                            else if (vCantStockRes > 0)
                                            {
                                                result = pBePedidoEnc;
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        throw;
                                    }
                                }
                                else if (vContador_Lineas_Detalle_Pedido_Insertadas == BeINavPedTrasladoEnc.lineas_Detalle.Count || vCantStockRes > 0)
                                {
                                    result = pBePedidoEnc;
                                }
                                else
                                {
                                    if ((int)BeConfigEnc.Rechazar_pedido_incompleto==1)
                                    {
                                        string vMensajeError20230301 = string.Format("vContador_Lineas_Detalle_Pedido_Insertadas: " + vContador_Lineas_Detalle_Pedido_Insertadas + " BeINavPedTrasladoEnc.Lineas_Detalle.Count: " + BeINavPedTrasladoEnc.lineas_Detalle.Count);
                                        throw new Exception(vMensajeError20230301);
                                    }
                                    else
                                    {
                                        result = pBePedidoEnc;
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }

                        if (!vPedidoExistente)
                        {
                            if (!clsLnTrans_pe_enc.Tiene_Detalle(pBePedidoEnc.IdPedidoEnc,
                                                               lConectionInterface,
                                                               lTransInterface))
                            {
                                clsLnTrans_pe_enc.Eliminar_Encabezado_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                             lConectionInterface,
                                                                             lTransInterface);
                            }
                        }
                    }
                }
                else
                {
                }

                vContador += 1;

                VContadorBitacoraTOMWMS = vContador;

                if (pBePedidoEnc != null)
                {
                    if (pBePedidoEnc.IdPedidoEnc > 0)
                    {
                    }
                }

                double difSegundos = (DateTime.Now - vFechaInicio).TotalSeconds;

                if (!BeConfigEnc.Interface_SAP)
                {
                }
            }
            catch (Exception ex)
            {
                string vListaMensajeError = "";

                if (lconfig != null) {
                    if (pBePedidoEnc != null)
                    {
                        lLogErrorWMS = clsLnLog_error_wms.Get_All_By_Referencia_Documento(lconfig, pBePedidoEnc.Referencia);
                    }
                    else
                    {
                        lLogErrorWMS = clsLnLog_error_wms.Get_All_By_Referencia_Documento(lconfig, BeINavPedTrasladoEnc.No);
                    }
                }                

                if (lLogErrorWMS != null)
                {
                    if (lLogErrorWMS.Count > 0)
                    {
                        foreach (clsBeLog_error_wms Lwms in lLogErrorWMS)
                        {
                            vListaMensajeError += Environment.NewLine + Lwms.MensajeError;
                        }
                    }
                }

                string vMensajeError = ex.Message;

                if (string.IsNullOrEmpty(vListaMensajeError))
                {
                    throw;
                }
                else
                {
                    throw new Exception(vMensajeError + Environment.NewLine + vListaMensajeError);
                }
            }

            return result;
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

                if (BeBodega !=null)
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

                    if ((int)pBeConfigEnc.Rechazar_pedido_incompleto ==1)
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
