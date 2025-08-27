using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Windows.Forms;
using System.Xml.Serialization;
using TOMWMS.srvBarrasPallet;
using TOMWMS.srvPedidoCliente;
using TOMWMS.srvPedidoCompra;
using TOMWMS.srvProducto;
using TOMWMS.srvPropietario;
using TOMWMS.srvProveedorBodega;
using TOMWMS.srvReportes;
using TOMWMS.srvTransaccionesOut;
using TOMWMS.srvUsuario;
using TOMWMS.svBodega;
using TOMWMS.WSHH;

namespace TestMI3
{
    public partial class frmInicio : Form
    {
        public frmInicio()
        {
            InitializeComponent();
        }

        private void bodegaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceBodegaClient sv = new ServiceBodegaClient();
                dgrid.DataSource = sv.Get_All();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} {1}", MethodBase.GetCurrentMethod(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }
        }

        private void getByCodigoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceProductoClient sv = new ServiceProductoClient();
                int IdProducto = sv.Get_IdProducto_By_Codigo("500204");
                MessageBox.Show(IdProducto.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} {1}", MethodBase.GetCurrentMethod(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }
        }

        Uri baseAddress = new Uri("http://192.168.43.77/MI3/");

        private void pedidoCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {


                // string address = "http://192.168.0.18/MI3/Transacciones/PedidoCompra.svc";
                // EndpointAddress address = new EndpointAddress("http://192.168.0.4/mi3/Transacciones/PedidoCliente.svc");
                // PedidoCompraClient sv = new PedidoCompraClient();
                //sv.Endpoint.Address = new EndpointAddress(address);

                BasicHttpBinding binding = new BasicHttpBinding();
                EndpointAddress address = new EndpointAddress("http://192.168.0.4/mi3/Transacciones/PedidoCompra.svc");
                //EndpointAddress address = new EndpointAddress("http://192.168.126.79/MI3/Transacciones/PedidoCliente.svc");
                PedidoCompraClient sv = new PedidoCompraClient(binding, address);
                clsBeI_nav_ped_compra_enc BePedCompra = new clsBeI_nav_ped_compra_enc();

                sv.Open();

                BePedCompra.No = "5323971";
                BePedCompra.Buy_From_Vendor_No = "ET"; //Código de Proveedor.
                BePedCompra.Buy_From_Vendor_Name = "BODEGA DE RETENCION DE P.T. BOCA COSTA";
                BePedCompra.Posting_Description = "Traslado Inventario Salida a Transito ";
                BePedCompra.Posting_Date = DateTime.Parse("26/05/2025");
                BePedCompra.Document_Date = DateTime.Parse("26/05/2025");
                BePedCompra.Vendor_Invoice_No = "9884";
                BePedCompra.Status = "1";
                BePedCompra.Payment_Terms_Code = "N/A";
                BePedCompra.Ship_To_Name = "jadelacruz";
                BePedCompra.Location_Code = "EN"; //Código de cliente o código de bodega de destino
                BePedCompra.Ship_To_Contact = "jadelacruz";
                BePedCompra.Expected_Receipt_Date = DateTime.Parse("31/07/2024");
                BePedCompra.Is_Internal_Transfer = false;
                BePedCompra.Product_Owner_Code = "01";

                BePedCompra.Lineas_Detalle = new List<clsBeI_nav_ped_compra_det>();

                clsBeI_nav_ped_compra_det BeCompraDet = new clsBeI_nav_ped_compra_det();
                BeCompraDet.NoEnc = BePedCompra.No;
                BeCompraDet.No = "42487"; //Código de producto
                BeCompraDet.Line_No = 1;
                BeCompraDet.Type = "2";
                BeCompraDet.Description = "VITINA PAY Y GALLETAS"; //Descripción de producto
                BeCompraDet.Description2 = "";
                BeCompraDet.Location_Code = "EN"; //Código de bodega de destino
                BeCompraDet.Quantity = 1;
                //Descripción de la unidad de medida tal como está en la tabla unidad_medida
                BeCompraDet.Unit_of_Measure_Code = "UN";
                BeCompraDet.Direct_Unit_Cost = 0;
                BeCompraDet.Line_Amount = 0;
                BeCompraDet.Quantity_Received = 0;
                BeCompraDet.Planed_Receipt_Date = DateTime.Parse("26/05/2025");
                BeCompraDet.Variant_Code = "42487";
                BePedCompra.Lineas_Detalle.Add(BeCompraDet);

                string resultado;

                resultado = "";

                //BePedCompra.Lineas_Detalle_Lotes = new List<clsBeI_nav_ped_compra_det_lote>();

                //clsBeI_nav_ped_compra_det_lote BeCompraDetLote = new clsBeI_nav_ped_compra_det_lote();
                //BeCompraDetLote.NoEnc = BePedCompra.No;
                ////Si es transferencia interna el código de documento de despacho de la bodegea origen
                ////Si No es T.I. el número de ornde de compra.
                //BeCompraDetLote.source_ID = BePedCompra.No;
                //BeCompraDetLote.Item_No = "40470"; //Código de producto
                //BeCompraDetLote.Lot_No = "L2019-1"; //# Lote
                //BeCompraDetLote.Entry_No = "1"; //# Valor por defecto (incrementa si existieran mas lotes para una misma línea)
                //BeCompraDetLote.Source_Prod_Order_Line = 10000; //# Línea
                //BeCompraDetLote.Expiration_Date = DateTime.Parse("01/01/2020"); //# Línea
                //BePedCompra.Lineas_Detalle_Lotes.Add(BeCompraDetLote);

                sv.Insert(ref BePedCompra, ref resultado);

                txtResult.AppendText(resultado);

            }

            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} {1}", MethodBase.GetCurrentMethod(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }
        }

        private void barrasPalletToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("¿Insertar 10,000 barras de pallet?", "BP", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                try
                {

                    Barras_PalletClient BpSrv = new Barras_PalletClient();
                    TOMWMS.srvBarrasPallet.clsBeI_nav_barras_pallet bp = new TOMWMS.srvBarrasPallet.clsBeI_nav_barras_pallet();

                    BpSrv.Open();

                    int MaxIdPallet = BpSrv.Max_IdPallet() + 1;

                    for (int i = 0; i <= 10000; i++)
                    {

                        bp.IdPallet = MaxIdPallet;
                        bp.Codigo = "40029";
                        bp.Nombre = "CAJA ACEITE IDEAL 12U. 1800 Ml";
                        bp.Camas_Por_Tarima = 5;
                        bp.Cajas_Por_Cama = 6;
                        bp.Cantidad_Presentacion = 12;
                        bp.UM_Producto = "Caja12;";
                        bp.Lote = "A20181122";
                        bp.Lote_Numerico = int.Parse("20181122") + i;
                        bp.Fecha_Agregado = DateTime.Now;
                        bp.Fecha_Ingreso = DateTime.Parse("01/01/1990");
                        bp.Fecha_Vence = DateTime.Parse("01/01/2019");
                        bp.Fecha_Produccion = DateTime.Now;
                        bp.Activo = true;
                        bp.Recibido = false;
                        bp.IdRecepcion = 0;
                        bp.Bodega_Origen = "BC";
                        bp.Bodega_Destino = "100031";
                        bp.Codigo_barra = bp.Lote + bp.IdPallet;
                        BpSrv.Insert_Single(bp);

                        MaxIdPallet++;

                        txtResult.AppendText("Se insertó el pallet: " + bp.Codigo_barra + "\n");
                        txtResult.Refresh();
                        txtResult.SelectionStart = txtResult.TextLength;
                        txtResult.ScrollToCaret();

                        Application.DoEvents();

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("{0} {1}", MethodBase.GetCurrentMethod(), ex.Message),
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                }

            }
        }

        private int NoPedido = 5187000;
        private decimal CantidadPedido = 36;
        private void pedidoClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("¿Insertar pedido de cliente?", "BP", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                try
                {

                    BasicHttpBinding binding = new BasicHttpBinding();
                    EndpointAddress address = new EndpointAddress("http://192.168.0.6/mi3/Transacciones/PedidoCliente.svc");
                    //EndpointAddress address = new EndpointAddress("http://192.168.126.79/MI3/Transacciones/PedidoCliente.svc");
                    PedidoClienteClient srvPedCliente = new PedidoClienteClient(binding, address);
                    clsBeI_nav_ped_traslado_enc BePedidoEnc = new clsBeI_nav_ped_traslado_enc();

                    NoPedido += 1;

                    #region "Ped8 40421-11"
                    //No      Posting_Date    Receipt_Date    Shipment_Date   Status Transfer_from_Code  Transfer_from_Contact Transfer_from_Name                     Transfer_to_Code Transfer_to_Contact Transfer_to_Name           transfer_to_CodeField   Product_Owner_Code Receipt_Document_Reference  Document_Type External_Document_No    fec_agr RoadCodigoRuta  RoadCodigoVendedor Manufacturing_Process   Address Comments    Company_Code
                    //5186854 2024 - 11 - 18  2024 - 11 - 18  2024 - 11 - 18  1      ET                   NULL                 BODEGA DE RETENCION DE P.T.BOCA COSTA  EO                NULL                BOD.PROD.TERMINADO IODESA EO                       01                0                           4             NULL                     2024 - 11 - 18 09:20:59.790 0   0   0

                    BePedidoEnc.No = (NoPedido).ToString();
                    BePedidoEnc.Posting_Date = DateTime.Now;
                    BePedidoEnc.Receipt_Date = DateTime.Now;
                    BePedidoEnc.Shipment_Date = DateTime.Now;
                    BePedidoEnc.Status = 1;
                    BePedidoEnc.Transfer_from_Code = "ET"; //Código Bodega Origen
                    BePedidoEnc.Transfer_from_Contact = "Interface"; //Nombre Responsable (O usuario que ejecuta) Bodega Origen
                    BePedidoEnc.Transfer_from_Name = "BODEGA DE RETENCION DE P.T. BOCA COSTA"; //Nombre Bodega Origen
                    BePedidoEnc.Transfer_to_Code = "EO"; //Código de Cliente o Código de bodega Destino.
                    BePedidoEnc.Transfer_to_CodeField1 = BePedidoEnc.Transfer_to_Code; //Código de Cliente o Código de bodega Destino.
                    BePedidoEnc.Transfer_to_Name = "BOD.PROD.TERMINADO IODESA"; //Nombre de Cliente o Nombre de bodega Destino.
                    BePedidoEnc.Transfer_to_Contact = null;
                    BePedidoEnc.Is_Internal_Transfer = true;
                    BePedidoEnc.Document_Type = (IdTipoDocumentoSalida)TOMWMS.clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente;
                    BePedidoEnc.Product_Owner_Code = "01";
                    BePedidoEnc.External_Document_No = "";
                    BePedidoEnc.Receipt_Document_Reference = "";
                    BePedidoEnc.Lineas_Detalle = new List<clsBeI_nav_ped_traslado_det>();

                    clsBeI_nav_ped_traslado_det BeTrasDet = new clsBeI_nav_ped_traslado_det();

                    BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    BeTrasDet.No = "43033"; //código de producto
                    BeTrasDet.Description = "400 G MIRASOL REGULAR 24 U DE 5 BARRAS L16"; //código de producto
                    BeTrasDet.Item_No = BeTrasDet.No;
                    BeTrasDet.Qty_to_Receive = 0;
                    BeTrasDet.Qty_to_Ship = 0;
                    BeTrasDet.Quantity = 23;
                    BeTrasDet.Transfer_to_CodeField = "EO";
                    BeTrasDet.Shipment_Date = DateTime.Now;
                    BeTrasDet.Unit_of_Measure_Code = "UN";
                    BeTrasDet.Line_No = 1;
                    BeTrasDet.Variant_Code = "43033";
                    BeTrasDet.Status = 1;
                    BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "21204HB01"; //código de producto
                    //BeTrasDet.Description = "400 G.DETERGENTE EN POLVO ULTRAKLIN FUERZA VITAL "; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 15;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 2;
                    //BeTrasDet.Variant_Code = null;
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    #endregion


                    //#region "Ped1 212040M31-10CJ"

                    //BePedidoEnc.No = (NoPedido).ToString();
                    //BePedidoEnc.Posting_Date = DateTime.Now;
                    //BePedidoEnc.Receipt_Date = DateTime.Now;
                    //BePedidoEnc.Shipment_Date = DateTime.Now;
                    //BePedidoEnc.Status = 1;
                    //BePedidoEnc.Transfer_from_Code = "04"; //Código Bodega Origen
                    //BePedidoEnc.Transfer_from_Contact = "Interface"; //Nombre Responsable (O usuario que ejecuta) Bodega Origen
                    //BePedidoEnc.Transfer_from_Name = "04- BODEGA MERCOPAN"; //Nombre Bodega Origen
                    //BePedidoEnc.Transfer_to_Code = "115685"; //Código de Cliente o Código de bodega Destino.
                    //BePedidoEnc.Transfer_to_CodeField1 = BePedidoEnc.Transfer_to_Code; //Código de Cliente o Código de bodega Destino.
                    //BePedidoEnc.Transfer_to_Name = "DESPACHOS WMS"; //Nombre de Cliente o Nombre de bodega Destino.
                    //BePedidoEnc.Transfer_to_Contact = null;
                    //BePedidoEnc.Is_Internal_Transfer = true;
                    //BePedidoEnc.Document_Type = (IdTipoDocumentoSalida)TOMWMS.clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente;
                    //BePedidoEnc.Product_Owner_Code = "01";
                    //BePedidoEnc.External_Document_No = "";
                    //BePedidoEnc.Receipt_Document_Reference = "";
                    //BePedidoEnc.Lineas_Detalle = new List<clsBeI_nav_ped_traslado_det>();

                    //clsBeI_nav_ped_traslado_det BeTrasDet = new clsBeI_nav_ped_traslado_det();

                    ////BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "212040M31"; //código de producto
                    //BeTrasDet.Description = "5 KGDETERPOLVO ULTRAKLIN FUERZA NATURAL F2"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 51;
                    //BeTrasDet.Transfer_to_CodeField = "115685";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 1;
                    //BeTrasDet.Variant_Code = "212040M31";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //#endregion

                    #region "Ped5 40117-4"

                    //BePedidoEnc.No = (NoPedido).ToString();
                    //BePedidoEnc.Posting_Date = DateTime.Now;
                    //BePedidoEnc.Receipt_Date = DateTime.Now;
                    //BePedidoEnc.Shipment_Date = DateTime.Now;
                    //BePedidoEnc.Status = 1;
                    //BePedidoEnc.Transfer_from_Code = "BV01"; //Código Bodega Origen
                    //BePedidoEnc.Transfer_from_Contact = "Interface"; //Nombre Responsable (O usuario que ejecuta) Bodega Origen
                    //BePedidoEnc.Transfer_from_Name = "PRODUCTO TERMINADO SPS"; //Nombre Bodega Origen
                    //BePedidoEnc.Transfer_to_Code = "002196"; //Código de Cliente o Código de bodega Destino.
                    //BePedidoEnc.Transfer_to_CodeField1 = BePedidoEnc.Transfer_to_Code; //Código de Cliente o Código de bodega Destino.
                    //BePedidoEnc.Transfer_to_Name = "OPERADORA DEL ORIENTE S.A. DE C.V."; //Nombre de Cliente o Nombre de bodega Destino.
                    //BePedidoEnc.Transfer_to_Contact = null;
                    //BePedidoEnc.Is_Internal_Transfer = true;
                    //BePedidoEnc.Document_Type = (IdTipoDocumentoSalida)TOMWMS.clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente;
                    //BePedidoEnc.Product_Owner_Code = "01";
                    //BePedidoEnc.External_Document_No = "";
                    //BePedidoEnc.Receipt_Document_Reference = "";
                    //BePedidoEnc.Lineas_Detalle = new List<clsBeI_nav_ped_traslado_det>();

                    //clsBeI_nav_ped_traslado_det BeTrasDet = new clsBeI_nav_ped_traslado_det();

                    ////BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "40117"; //código de producto
                    //BeTrasDet.Description = "1500ML. ACEITE EL DORADO"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 4;
                    //BeTrasDet.Transfer_to_CodeField = "002196";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 1;
                    //BeTrasDet.Variant_Code = null;
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    #endregion

                    #region "Ped6 40117-3"

                    // BePedidoEnc.No = (NoPedido).ToString();
                    // BePedidoEnc.Posting_Date = DateTime.Now;
                    // BePedidoEnc.Receipt_Date = DateTime.Now;
                    // BePedidoEnc.Shipment_Date = DateTime.Now;
                    // BePedidoEnc.Status = 1;
                    // BePedidoEnc.Transfer_from_Code = "BV01"; //Código Bodega Origen
                    // BePedidoEnc.Transfer_from_Contact = "Interface"; //Nombre Responsable (O usuario que ejecuta) Bodega Origen
                    // BePedidoEnc.Transfer_from_Name = "PRODUCTO TERMINADO SPS"; //Nombre Bodega Origen
                    // BePedidoEnc.Transfer_to_Code = "002196"; //Código de Cliente o Código de bodega Destino.
                    // BePedidoEnc.Transfer_to_CodeField1 = BePedidoEnc.Transfer_to_Code; //Código de Cliente o Código de bodega Destino.
                    // BePedidoEnc.Transfer_to_Name = "OPERADORA DEL ORIENTE S.A. DE C.V."; //Nombre de Cliente o Nombre de bodega Destino.
                    // BePedidoEnc.Transfer_to_Contact = null;
                    // BePedidoEnc.Is_Internal_Transfer = true;
                    // BePedidoEnc.Document_Type = (IdTipoDocumentoSalida)TOMWMS.clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente;
                    // BePedidoEnc.Product_Owner_Code = "01";
                    // BePedidoEnc.External_Document_No = "";
                    // BePedidoEnc.Receipt_Document_Reference = "";
                    // BePedidoEnc.Lineas_Detalle = new List<clsBeI_nav_ped_traslado_det>();

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();

                    // //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    // BeTrasDet.No = "40117"; //código de producto
                    // BeTrasDet.Description = "1500ML. ACEITE EL DORADO"; //código de producto
                    // BeTrasDet.Item_No = BeTrasDet.No;
                    // BeTrasDet.Qty_to_Receive = 0;
                    // BeTrasDet.Qty_to_Ship = 0;
                    // BeTrasDet.Quantity = 3;
                    // BeTrasDet.Transfer_to_CodeField = "002196";
                    // BeTrasDet.Shipment_Date = DateTime.Now;
                    // BeTrasDet.Unit_of_Measure_Code = "UN";
                    // BeTrasDet.Line_No = 1;
                    // BeTrasDet.Variant_Code = null;
                    // BeTrasDet.Status = 1;
                    // BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    #endregion

                    #region "Ped2 40262-12"

                    //BePedidoEnc.No = (NoPedido).ToString();
                    //BePedidoEnc.Posting_Date = DateTime.Now;
                    //BePedidoEnc.Receipt_Date = DateTime.Now;
                    //BePedidoEnc.Shipment_Date = DateTime.Now;
                    //BePedidoEnc.Status = 1;
                    //BePedidoEnc.Transfer_from_Code = "BV01"; //Código Bodega Origen
                    //BePedidoEnc.Transfer_from_Contact = "Interface"; //Nombre Responsable (O usuario que ejecuta) Bodega Origen
                    //BePedidoEnc.Transfer_from_Name = "PRODUCTO TERMINADO SPS"; //Nombre Bodega Origen
                    //BePedidoEnc.Transfer_to_Code = "002196"; //Código de Cliente o Código de bodega Destino.
                    //BePedidoEnc.Transfer_to_CodeField1 = BePedidoEnc.Transfer_to_Code; //Código de Cliente o Código de bodega Destino.
                    //BePedidoEnc.Transfer_to_Name = "OPERADORA DEL ORIENTE S.A. DE C.V."; //Nombre de Cliente o Nombre de bodega Destino.
                    //BePedidoEnc.Transfer_to_Contact = null;
                    //BePedidoEnc.Is_Internal_Transfer = true;
                    //BePedidoEnc.Document_Type = (IdTipoDocumentoSalida)TOMWMS.clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente;
                    //BePedidoEnc.Product_Owner_Code = "01";
                    //BePedidoEnc.External_Document_No = "";
                    //BePedidoEnc.Receipt_Document_Reference = "";
                    //BePedidoEnc.Lineas_Detalle = new List<clsBeI_nav_ped_traslado_det>();

                    //clsBeI_nav_ped_traslado_det BeTrasDet = new clsBeI_nav_ped_traslado_det();

                    ////BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "40262"; //código de producto
                    //BeTrasDet.Description = "1500ML. ACEITE EL DORADO"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 12;
                    //BeTrasDet.Transfer_to_CodeField = "002196";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 1;
                    //BeTrasDet.Variant_Code = null;
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    #endregion

                    #region "Ped2 40262-11"

                    // BePedidoEnc.No = (NoPedido).ToString();
                    // BePedidoEnc.Posting_Date = DateTime.Now;
                    // BePedidoEnc.Receipt_Date = DateTime.Now;
                    // BePedidoEnc.Shipment_Date = DateTime.Now;
                    // BePedidoEnc.Status = 1;
                    // BePedidoEnc.Transfer_from_Code = "BV01"; //Código Bodega Origen
                    // BePedidoEnc.Transfer_from_Contact = "Interface"; //Nombre Responsable (O usuario que ejecuta) Bodega Origen
                    // BePedidoEnc.Transfer_from_Name = "PRODUCTO TERMINADO SPS"; //Nombre Bodega Origen
                    // BePedidoEnc.Transfer_to_Code = "002196"; //Código de Cliente o Código de bodega Destino.
                    // BePedidoEnc.Transfer_to_CodeField1 = BePedidoEnc.Transfer_to_Code; //Código de Cliente o Código de bodega Destino.
                    // BePedidoEnc.Transfer_to_Name = "OPERADORA DEL ORIENTE S.A. DE C.V."; //Nombre de Cliente o Nombre de bodega Destino.
                    // BePedidoEnc.Transfer_to_Contact = null;
                    // BePedidoEnc.Is_Internal_Transfer = true;
                    // BePedidoEnc.Document_Type = (IdTipoDocumentoSalida)TOMWMS.clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente;
                    // BePedidoEnc.Product_Owner_Code = "01";
                    // BePedidoEnc.External_Document_No = "";
                    // BePedidoEnc.Receipt_Document_Reference = "";
                    // BePedidoEnc.Lineas_Detalle = new List<clsBeI_nav_ped_traslado_det>();

                    // BeTrasDet = new clsBeI_nav_ped_traslado_det();

                    // //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    // BeTrasDet.No = "40262"; //código de producto
                    // BeTrasDet.Description = "1500ML. ACEITE EL DORADO"; //código de producto
                    // BeTrasDet.Item_No = BeTrasDet.No;
                    // BeTrasDet.Qty_to_Receive = 0;
                    // BeTrasDet.Qty_to_Ship = 0;
                    // BeTrasDet.Quantity = 11;
                    // BeTrasDet.Transfer_to_CodeField = "002196";
                    // BeTrasDet.Shipment_Date = DateTime.Now;
                    // BeTrasDet.Unit_of_Measure_Code = "UN";
                    // BeTrasDet.Line_No = 1;
                    // BeTrasDet.Variant_Code = null;
                    // BeTrasDet.Status = 1;
                    // BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    #endregion

                    #region "Ped7 2030X1181-44"

                    //BePedidoEnc.No = (NoPedido).ToString();
                    //BePedidoEnc.Posting_Date = DateTime.Now;
                    //BePedidoEnc.Receipt_Date = DateTime.Now;
                    //BePedidoEnc.Shipment_Date = DateTime.Now;
                    //BePedidoEnc.Status = 1;
                    //BePedidoEnc.Transfer_from_Code = "BT1"; //Código Bodega Origen
                    //BePedidoEnc.Transfer_from_Contact = "Interface"; //Nombre Responsable (O usuario que ejecuta) Bodega Origen
                    //BePedidoEnc.Transfer_from_Name = "PRODUCTO TERMINADO SPS"; //Nombre Bodega Origen
                    //BePedidoEnc.Transfer_to_Code = "320209"; //Código de Cliente o Código de bodega Destino.
                    //BePedidoEnc.Transfer_to_CodeField1 = BePedidoEnc.Transfer_to_Code; //Código de Cliente o Código de bodega Destino.
                    //BePedidoEnc.Transfer_to_Name = "OPERADORA DEL ORIENTE S.A. DE C.V."; //Nombre de Cliente o Nombre de bodega Destino.
                    //BePedidoEnc.Transfer_to_Contact = null;
                    //BePedidoEnc.Is_Internal_Transfer = true;
                    //BePedidoEnc.Document_Type = (IdTipoDocumentoSalida)TOMWMS.clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente;
                    //BePedidoEnc.Product_Owner_Code = "01";
                    //BePedidoEnc.External_Document_No = "";
                    //BePedidoEnc.Receipt_Document_Reference = "";
                    //BePedidoEnc.Lineas_Detalle = new List<clsBeI_nav_ped_traslado_det>();

                    //clsBeI_nav_ped_traslado_det BeTrasDet = new clsBeI_nav_ped_traslado_det();

                    ////BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "203011X81"; //código de producto
                    //BeTrasDet.Description = "1500ML. ACEITE EL DORADO"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity =44;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 1;
                    //BeTrasDet.Variant_Code = null;
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    #endregion

                    #region "Ped3 223144-25"

                    //BePedidoEnc.No = (NoPedido).ToString();
                    //BePedidoEnc.Posting_Date = DateTime.Now;
                    //BePedidoEnc.Receipt_Date = DateTime.Now;
                    //BePedidoEnc.Shipment_Date = DateTime.Now;
                    //BePedidoEnc.Status = 1;
                    //BePedidoEnc.Transfer_from_Code = "BV01"; //Código Bodega Origen
                    //BePedidoEnc.Transfer_from_Contact = "Interface"; //Nombre Responsable (O usuario que ejecuta) Bodega Origen
                    //BePedidoEnc.Transfer_from_Name = "PRODUCTO TERMINADO SPS"; //Nombre Bodega Origen
                    //BePedidoEnc.Transfer_to_Code = "002196"; //Código de Cliente o Código de bodega Destino.
                    //BePedidoEnc.Transfer_to_CodeField1 = BePedidoEnc.Transfer_to_Code; //Código de Cliente o Código de bodega Destino.
                    //BePedidoEnc.Transfer_to_Name = "OPERADORA DEL ORIENTE S.A. DE C.V."; //Nombre de Cliente o Nombre de bodega Destino.
                    //BePedidoEnc.Transfer_to_Contact = null;
                    //BePedidoEnc.Is_Internal_Transfer = true;
                    //BePedidoEnc.Document_Type = (IdTipoDocumentoSalida)TOMWMS.clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente;
                    //BePedidoEnc.Product_Owner_Code = "01";
                    //BePedidoEnc.External_Document_No = "";
                    //BePedidoEnc.Receipt_Document_Reference = "";
                    //BePedidoEnc.Lineas_Detalle = new List<clsBeI_nav_ped_traslado_det>();

                    //clsBeI_nav_ped_traslado_det BeTrasDet = new clsBeI_nav_ped_traslado_det();

                    ////BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "223144"; //código de producto
                    //BeTrasDet.Description = "1500ML. ACEITE EL DORADO"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 25;
                    //BeTrasDet.Transfer_to_CodeField = "002196";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 1;
                    //BeTrasDet.Variant_Code = null;
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    #endregion

                    #region "Ped3 223144-21"

                    // BePedidoEnc.No = (NoPedido).ToString();
                    // BePedidoEnc.Posting_Date = DateTime.Now;
                    // BePedidoEnc.Receipt_Date = DateTime.Now;
                    // BePedidoEnc.Shipment_Date = DateTime.Now;
                    // BePedidoEnc.Status = 1;
                    // BePedidoEnc.Transfer_from_Code = "BV01"; //Código Bodega Origen
                    // BePedidoEnc.Transfer_from_Contact = "Interface"; //Nombre Responsable (O usuario que ejecuta) Bodega Origen
                    // BePedidoEnc.Transfer_from_Name = "PRODUCTO TERMINADO SPS"; //Nombre Bodega Origen
                    // BePedidoEnc.Transfer_to_Code = "002196"; //Código de Cliente o Código de bodega Destino.
                    // BePedidoEnc.Transfer_to_CodeField1 = BePedidoEnc.Transfer_to_Code; //Código de Cliente o Código de bodega Destino.
                    // BePedidoEnc.Transfer_to_Name = "OPERADORA DEL ORIENTE S.A. DE C.V."; //Nombre de Cliente o Nombre de bodega Destino.
                    // BePedidoEnc.Transfer_to_Contact = null;
                    // BePedidoEnc.Is_Internal_Transfer = true;
                    // BePedidoEnc.Document_Type = (IdTipoDocumentoSalida)TOMWMS.clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente;
                    // BePedidoEnc.Product_Owner_Code = "01";
                    // BePedidoEnc.External_Document_No = "";
                    // BePedidoEnc.Receipt_Document_Reference = "";
                    // BePedidoEnc.Lineas_Detalle = new List<clsBeI_nav_ped_traslado_det>();

                    // BeTrasDet = new clsBeI_nav_ped_traslado_det();

                    // //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    // BeTrasDet.No = "223144"; //código de producto
                    // BeTrasDet.Description = "1500ML. ACEITE EL DORADO"; //código de producto
                    // BeTrasDet.Item_No = BeTrasDet.No;
                    // BeTrasDet.Qty_to_Receive = 0;
                    // BeTrasDet.Qty_to_Ship = 0;
                    // BeTrasDet.Quantity = 21;
                    // BeTrasDet.Transfer_to_CodeField = "002196";
                    // BeTrasDet.Shipment_Date = DateTime.Now;
                    // BeTrasDet.Unit_of_Measure_Code = "UN";
                    // BeTrasDet.Line_No = 1;
                    // BeTrasDet.Variant_Code = null;
                    // BeTrasDet.Status = 1;
                    // BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    #endregion

                    #region "Ped9 223144-5"

                    // BePedidoEnc.No = (NoPedido).ToString();
                    // BePedidoEnc.Posting_Date = DateTime.Now;
                    // BePedidoEnc.Receipt_Date = DateTime.Now;
                    // BePedidoEnc.Shipment_Date = DateTime.Now;
                    // BePedidoEnc.Status = 1;
                    // BePedidoEnc.Transfer_from_Code = "BV01"; //Código Bodega Origen
                    // BePedidoEnc.Transfer_from_Contact = "Interface"; //Nombre Responsable (O usuario que ejecuta) Bodega Origen
                    // BePedidoEnc.Transfer_from_Name = "PRODUCTO TERMINADO SPS"; //Nombre Bodega Origen
                    // BePedidoEnc.Transfer_to_Code = "002196"; //Código de Cliente o Código de bodega Destino.
                    // BePedidoEnc.Transfer_to_CodeField1 = BePedidoEnc.Transfer_to_Code; //Código de Cliente o Código de bodega Destino.
                    // BePedidoEnc.Transfer_to_Name = "OPERADORA DEL ORIENTE S.A. DE C.V."; //Nombre de Cliente o Nombre de bodega Destino.
                    // BePedidoEnc.Transfer_to_Contact = null;
                    // BePedidoEnc.Is_Internal_Transfer = true;
                    // BePedidoEnc.Document_Type = (IdTipoDocumentoSalida)TOMWMS.clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente;
                    // BePedidoEnc.Product_Owner_Code = "01";
                    // BePedidoEnc.External_Document_No = "";
                    // BePedidoEnc.Receipt_Document_Reference = "";
                    // BePedidoEnc.Lineas_Detalle = new List<clsBeI_nav_ped_traslado_det>();

                    // BeTrasDet = new clsBeI_nav_ped_traslado_det();

                    // //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    // BeTrasDet.No = "223144"; //código de producto
                    // BeTrasDet.Description = "1500ML. ACEITE EL DORADO"; //código de producto
                    // BeTrasDet.Item_No = BeTrasDet.No;
                    // BeTrasDet.Qty_to_Receive = 0;
                    // BeTrasDet.Qty_to_Ship = 0;
                    // BeTrasDet.Quantity = 5;
                    // BeTrasDet.Transfer_to_CodeField = "002196";
                    // BeTrasDet.Shipment_Date = DateTime.Now;
                    // BeTrasDet.Unit_of_Measure_Code = "UN";
                    // BeTrasDet.Line_No = 1;
                    // BeTrasDet.Variant_Code = null;
                    // BeTrasDet.Status = 1;
                    // BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    #endregion

                    #region "Ped10 223144-11"

                    // BePedidoEnc.No = (NoPedido).ToString();
                    // BePedidoEnc.Posting_Date = DateTime.Now;
                    // BePedidoEnc.Receipt_Date = DateTime.Now;
                    // BePedidoEnc.Shipment_Date = DateTime.Now;
                    // BePedidoEnc.Status = 1;
                    // BePedidoEnc.Transfer_from_Code = "BV01"; //Código Bodega Origen
                    // BePedidoEnc.Transfer_from_Contact = "Interface"; //Nombre Responsable (O usuario que ejecuta) Bodega Origen
                    // BePedidoEnc.Transfer_from_Name = "PRODUCTO TERMINADO SPS"; //Nombre Bodega Origen
                    // BePedidoEnc.Transfer_to_Code = "002196"; //Código de Cliente o Código de bodega Destino.
                    // BePedidoEnc.Transfer_to_CodeField1 = BePedidoEnc.Transfer_to_Code; //Código de Cliente o Código de bodega Destino.
                    // BePedidoEnc.Transfer_to_Name = "OPERADORA DEL ORIENTE S.A. DE C.V."; //Nombre de Cliente o Nombre de bodega Destino.
                    // BePedidoEnc.Transfer_to_Contact = null;
                    // BePedidoEnc.Is_Internal_Transfer = true;
                    // BePedidoEnc.Document_Type = (IdTipoDocumentoSalida)TOMWMS.clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente;
                    // BePedidoEnc.Product_Owner_Code = "01";
                    // BePedidoEnc.External_Document_No = "";
                    // BePedidoEnc.Receipt_Document_Reference = "";
                    // BePedidoEnc.Lineas_Detalle = new List<clsBeI_nav_ped_traslado_det>();

                    // BeTrasDet = new clsBeI_nav_ped_traslado_det();

                    // //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    // BeTrasDet.No = "223144"; //código de producto
                    // BeTrasDet.Description = "1500ML. ACEITE EL DORADO"; //código de producto
                    // BeTrasDet.Item_No = BeTrasDet.No;
                    // BeTrasDet.Qty_to_Receive = 0;
                    // BeTrasDet.Qty_to_Ship = 0;
                    // BeTrasDet.Quantity = 11;
                    // BeTrasDet.Transfer_to_CodeField = "002196";
                    // BeTrasDet.Shipment_Date = DateTime.Now;
                    // BeTrasDet.Unit_of_Measure_Code = "UN";
                    // BeTrasDet.Line_No = 1;
                    // BeTrasDet.Variant_Code = null;
                    // BeTrasDet.Status = 1;
                    // BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    #endregion

                    #region "Ped4 203258L31-45"

                    //BePedidoEnc.No = (NoPedido).ToString();
                    //BePedidoEnc.Posting_Date = DateTime.Now;
                    //BePedidoEnc.Receipt_Date = DateTime.Now;
                    //BePedidoEnc.Shipment_Date = DateTime.Now;
                    //BePedidoEnc.Status = 1;
                    //BePedidoEnc.Transfer_from_Code = "BV01"; //Código Bodega Origen
                    //BePedidoEnc.Transfer_from_Contact = "Interface"; //Nombre Responsable (O usuario que ejecuta) Bodega Origen
                    //BePedidoEnc.Transfer_from_Name = "PRODUCTO TERMINADO SPS"; //Nombre Bodega Origen
                    //BePedidoEnc.Transfer_to_Code = "002196"; //Código de Cliente o Código de bodega Destino.
                    //BePedidoEnc.Transfer_to_CodeField1 = BePedidoEnc.Transfer_to_Code; //Código de Cliente o Código de bodega Destino.
                    //BePedidoEnc.Transfer_to_Name = "OPERADORA DEL ORIENTE S.A. DE C.V."; //Nombre de Cliente o Nombre de bodega Destino.
                    //BePedidoEnc.Transfer_to_Contact = null;
                    //BePedidoEnc.Is_Internal_Transfer = true;
                    //BePedidoEnc.Document_Type = (IdTipoDocumentoSalida)TOMWMS.clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente;
                    //BePedidoEnc.Product_Owner_Code = "01";
                    //BePedidoEnc.External_Document_No = "";
                    //BePedidoEnc.Receipt_Document_Reference = "";
                    //BePedidoEnc.Lineas_Detalle = new List<clsBeI_nav_ped_traslado_det>();

                    //clsBeI_nav_ped_traslado_det BeTrasDet = new clsBeI_nav_ped_traslado_det();

                    ////BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "203258L31"; //código de producto
                    //BeTrasDet.Description = "1500ML. ACEITE EL DORADO"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 45;
                    //BeTrasDet.Transfer_to_CodeField = "002196";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 1;
                    //BeTrasDet.Variant_Code = null;
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    #endregion

                    #region "Ped11 203258L31-46"

                    //BePedidoEnc.No = (NoPedido).ToString();
                    //BePedidoEnc.Posting_Date = DateTime.Now;
                    //BePedidoEnc.Receipt_Date = DateTime.Now;
                    //BePedidoEnc.Shipment_Date = DateTime.Now;
                    //BePedidoEnc.Status = 1;
                    //BePedidoEnc.Transfer_from_Code = "BV01"; //Código Bodega Origen
                    //BePedidoEnc.Transfer_from_Contact = "Interface"; //Nombre Responsable (O usuario que ejecuta) Bodega Origen
                    //BePedidoEnc.Transfer_from_Name = "PRODUCTO TERMINADO SPS"; //Nombre Bodega Origen
                    //BePedidoEnc.Transfer_to_Code = "002196"; //Código de Cliente o Código de bodega Destino.
                    //BePedidoEnc.Transfer_to_CodeField1 = BePedidoEnc.Transfer_to_Code; //Código de Cliente o Código de bodega Destino.
                    //BePedidoEnc.Transfer_to_Name = "OPERADORA DEL ORIENTE S.A. DE C.V."; //Nombre de Cliente o Nombre de bodega Destino.
                    //BePedidoEnc.Transfer_to_Contact = null;
                    //BePedidoEnc.Is_Internal_Transfer = true;
                    //BePedidoEnc.Document_Type = (IdTipoDocumentoSalida)TOMWMS.clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente;
                    //BePedidoEnc.Product_Owner_Code = "01";
                    //BePedidoEnc.External_Document_No = "";
                    //BePedidoEnc.Receipt_Document_Reference = "";
                    //BePedidoEnc.Lineas_Detalle = new List<clsBeI_nav_ped_traslado_det>();

                    //clsBeI_nav_ped_traslado_det BeTrasDet = new clsBeI_nav_ped_traslado_det();

                    ////BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "203258L31"; //código de producto
                    //BeTrasDet.Description = "1500ML. ACEITE EL DORADO"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 46;
                    //BeTrasDet.Transfer_to_CodeField = "002196";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 1;
                    //BeTrasDet.Variant_Code = null;
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    #endregion

                    #region "Ped8 40421-11"

                    // BePedidoEnc.No = (NoPedido).ToString();
                    // BePedidoEnc.Posting_Date = DateTime.Now;
                    // BePedidoEnc.Receipt_Date = DateTime.Now;
                    // BePedidoEnc.Shipment_Date = DateTime.Now;
                    // BePedidoEnc.Status = 1;
                    // BePedidoEnc.Transfer_from_Code = "BV01"; //Código Bodega Origen
                    // BePedidoEnc.Transfer_from_Contact = "Interface"; //Nombre Responsable (O usuario que ejecuta) Bodega Origen
                    // BePedidoEnc.Transfer_from_Name = "PRODUCTO TERMINADO SPS"; //Nombre Bodega Origen
                    // BePedidoEnc.Transfer_to_Code = "002196"; //Código de Cliente o Código de bodega Destino.
                    // BePedidoEnc.Transfer_to_CodeField1 = BePedidoEnc.Transfer_to_Code; //Código de Cliente o Código de bodega Destino.
                    // BePedidoEnc.Transfer_to_Name = "OPERADORA DEL ORIENTE S.A. DE C.V."; //Nombre de Cliente o Nombre de bodega Destino.
                    // BePedidoEnc.Transfer_to_Contact = null;
                    // BePedidoEnc.Is_Internal_Transfer = true;
                    // BePedidoEnc.Document_Type = (IdTipoDocumentoSalida)TOMWMS.clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente;
                    // BePedidoEnc.Product_Owner_Code = "01";
                    // BePedidoEnc.External_Document_No = "";
                    // BePedidoEnc.Receipt_Document_Reference = "";
                    // BePedidoEnc.Lineas_Detalle = new List<clsBeI_nav_ped_traslado_det>();

                    // BeTrasDet = new clsBeI_nav_ped_traslado_det();

                    // //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    // BeTrasDet.No = "40421"; //código de producto
                    // BeTrasDet.Description = "1500ML. ACEITE EL DORADO"; //código de producto
                    // BeTrasDet.Item_No = BeTrasDet.No;
                    // BeTrasDet.Qty_to_Receive = 0;
                    // BeTrasDet.Qty_to_Ship = 0;
                    // BeTrasDet.Quantity = 11;
                    // BeTrasDet.Transfer_to_CodeField = "002196";
                    // BeTrasDet.Shipment_Date = DateTime.Now;
                    // BeTrasDet.Unit_of_Measure_Code = "UN";
                    // BeTrasDet.Line_No = 1;
                    // BeTrasDet.Variant_Code = null;
                    // BeTrasDet.Status = 1;
                    // BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    #endregion

                    #region "Productos"

                    //BeTrasDet.NoEnc = BePedidoEnc.No;
                    //BeTrasDet.No = "203422Z51"; //código de producto
                    //BeTrasDet.Description = "5 KG.DET.ULTRAKLIN FUERZA NATURAL F-2 74061710368"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 24;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 1;
                    //BeTrasDet.Variant_Code = null;
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "212040M50"; //código de producto
                    //BeTrasDet.Description =""; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 13;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 2;
                    //BeTrasDet.Variant_Code = "212040M50";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "212040ML1"; //código de producto
                    //BeTrasDet.Description = "400G.DETER.ULTRA KLIN FUERZA NATURAL F - 20"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 92;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 3;
                    //BeTrasDet.Variant_Code = "212040ML1";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "212040OH1"; //código de producto
                    //BeTrasDet.Description = "400G.DETER.ULTRA KLIN FUERZA EXTREMA F - 20"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 13;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 4;
                    //BeTrasDet.Variant_Code = "212040OH1";
                    //BeTrasDet.Status = 92;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "212047T01"; //código de producto
                    //BeTrasDet.Description = "400G.DETER.ULTRA KLIN ANTIBACTERIAL F-20"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 102;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 5;
                    //BeTrasDet.Variant_Code = "212047T01";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "212047Y51"; //código de producto
                    //BeTrasDet.Description = "5 KG.DET.EN POLVO ULTRA KLIN FUERZA RADIANTE CJ - 2"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 5;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 6;
                    //BeTrasDet.Variant_Code = "212047Y51";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "212049691"; //código de producto
                    //BeTrasDet.Description = "400G.DETERGENTE ULTRA KLIN FUERZA SOLAR F-20"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 102;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 7;
                    //BeTrasDet.Variant_Code = "212049691";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "2120497B1"; //código de producto
                    //BeTrasDet.Description = "400G.DETER.ULTRA KLIN FUERZA INTENSA F - 20"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 82;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 8;
                    //BeTrasDet.Variant_Code = "2120497B1";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "2120498C1"; //código de producto
                    //BeTrasDet.Description = "400G.DETERGENTE ULTRA KLIN FUERZA TOTAL F-20"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 82;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 9;
                    //BeTrasDet.Variant_Code = "2120498C1";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "213A67Q21"; //código de producto
                    //BeTrasDet.Description = "850G.LAV.ZAGAZ CARE ALMENDRACREMA DE ARROZ CJ-12"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 4;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 10;
                    //BeTrasDet.Variant_Code = "213A67Q21";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "215301K61"; //código de producto
                    //BeTrasDet.Description = "6 4PACK DE 1000G.BARRA ESPUMIL CJ - 6"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 120;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No =11;
                    //BeTrasDet.Variant_Code = "215301K61";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "222142"; //código de producto
                    //BeTrasDet.Description = "DETERGENTE ULTRAKLIN 2 KGS X 5 UNIDAD 74061710101"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 5;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 12;
                    //BeTrasDet.Variant_Code = "222142";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "222143"; //código de producto
                    //BeTrasDet.Description = "DETERGENTE ULTRAKLIN 5 KGS X 2 UNIDAD 74061710101"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 5;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 13;
                    //BeTrasDet.Variant_Code = "222143";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "222148"; //código de producto
                    //BeTrasDet.Description = "2KG.DETER.ULTRAKLIN FUERZA SOLAR F-5 7406171010"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 5;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 14;
                    //BeTrasDet.Variant_Code = "222148";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "222150"; //código de producto
                    //BeTrasDet.Description = "ULTRAKLIN FUERZA SOLAR 5 KGS 7406171010733"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 5;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 15;
                    //BeTrasDet.Variant_Code = "222150";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "222183"; //código de producto
                    //BeTrasDet.Description = "DET ULTRAKLIN FUERZA INTENSA 2KG X 5 740617101172"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 5;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 16;
                    //BeTrasDet.Variant_Code = "222183";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "223027"; //código de producto
                    //BeTrasDet.Description = "2K.DETER.ULTRAKLIN FUERZA EXTREMA F-5 740617101"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 5;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 17;
                    //BeTrasDet.Variant_Code = "223027";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "223096"; //código de producto
                    //BeTrasDet.Description = "1.1 K.DETPOLVO ULTRAKLIN FS F-10 7406171021234"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 73;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 18;
                    //BeTrasDet.Variant_Code = "223096";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "223097"; //código de producto
                    //BeTrasDet.Description = "1.1 K.DETPOLVO ULTRAKLIN FT F-10 7406171021241"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 99;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 2;
                    //BeTrasDet.Variant_Code = "223097";
                    //BeTrasDet.Status = 19;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "223099"; //código de producto
                    //BeTrasDet.Description = "1.1 K.DETPOLVO ULTRAKLIN FE F-10 7406171021388"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 80;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 20;
                    //BeTrasDet.Variant_Code = "223099";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "223344"; //código de producto
                    //BeTrasDet.Description = "5KG.DETER.EN POLVO ULTRAKLIN FE F - 2 740617102684"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 5;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 21;
                    //BeTrasDet.Variant_Code = "223344";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "223345"; //código de producto
                    //BeTrasDet.Description = "9 KG.DET.ULTRA.FUERZA EXTREMA 7406171026949"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 13;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 22;
                    //BeTrasDet.Variant_Code = "223345";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet.No = "2234312"; //código de producto
                    //BeTrasDet.Description =""; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 26;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 23;
                    //BeTrasDet.Variant_Code = "2234312";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "2234452"; //código de producto
                    //BeTrasDet.Description = "DET.EN POLVO ULTRAKLIN FI 115G.FDX35"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 26;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 24;
                    //BeTrasDet.Variant_Code = "2234452";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "226318501"; //código de producto
                    //BeTrasDet.Description = "1600G.AMBEX MULTILIMPIEZA CJ - 5"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 25;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 25;
                    //BeTrasDet.Variant_Code = "226318501";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "2631BL01"; //código de producto
                    //BeTrasDet.Description = "1600G.4PK J.AMBEX TOQUE DE SUAVI EXPLOSION FRAGAN"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 30;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 26;
                    //BeTrasDet.Variant_Code = "2631BL01";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "264072"; //código de producto
                    //BeTrasDet.Description = "850G.LAVAPLATOS ZAGAZ ANTIBACTERIAL CITRUS CJ - 12"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 4;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 27;
                    //BeTrasDet.Variant_Code = "264072";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "264085"; //código de producto
                    //BeTrasDet.Description = "850G.LAVA.ZAGAZ FRUTOS ROJOS ANTI BACTERIAL CJ-12"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 4;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 28;
                    //BeTrasDet.Variant_Code = "264085";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "265829"; //código de producto
                    //BeTrasDet.Description = "LAV.ZAGAZ PEACH BELLINI 850G12U"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 4;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 29;
                    //BeTrasDet.Variant_Code = "265829";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "273038"; //código de producto
                    //BeTrasDet.Description = "JABON MAYA HOTEL 48 UDS 752121267342"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 6;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 30;
                    //BeTrasDet.Variant_Code = "273038";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "40006"; //código de producto
                    //BeTrasDet.Description = "3.750 L.ACEITE IDEAL EXP. 7401001501993"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 3;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 31;
                    //BeTrasDet.Variant_Code = "40006";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "40026"; //código de producto
                    //BeTrasDet.Description = " 3.000 L.ACEITE IDEAL 7401001500675"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 3;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 32;
                    //BeTrasDet.Variant_Code = "40026";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);


                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "40034"; //código de producto
                    //BeTrasDet.Description = "1.500 L ACEITE IDEAL 7401001500651"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 22;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 33;
                    //BeTrasDet.Variant_Code = "40034";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);

                    //BeTrasDet = new clsBeI_nav_ped_traslado_det();
                    //BeTrasDet.No = "43243"; //código de producto
                    //BeTrasDet.Description = "MARGARINA FRANCESSA 400 GRS 400 GRS. 24 UNIDADES"; //código de producto
                    //BeTrasDet.Item_No = BeTrasDet.No;
                    //BeTrasDet.Qty_to_Receive = 0;
                    //BeTrasDet.Qty_to_Ship = 0;
                    //BeTrasDet.Quantity = 37;
                    //BeTrasDet.Transfer_to_CodeField = "320209";
                    //BeTrasDet.Shipment_Date = DateTime.Now;
                    //BeTrasDet.Unit_of_Measure_Code = "UN";
                    //BeTrasDet.Line_No = 34;
                    //BeTrasDet.Variant_Code = "43243";
                    //BeTrasDet.Status = 1;
                    //BePedidoEnc.Lineas_Detalle.Add(BeTrasDet);


                    #endregion

                    string resultado;

                    resultado = "";

                    Int32 filasinsertadas = srvPedCliente.Insert(ref BePedidoEnc, ref resultado);
                    //BePedidoEnc.es

                    Int32 lineasinsertadas = (from line in BePedidoEnc.Lineas_Detalle.Where(x => x.Process_Result == "Ok") select line).ToList().Count();

                    txtResult.AppendText(resultado);
                    txtResult.Refresh();
                    txtResult.SelectionStart = txtResult.TextLength;
                    txtResult.ScrollToCaret();

                    NoPedido++;
                    CantidadPedido++;
                }

                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("{0} {1}", MethodBase.GetCurrentMethod(), ex.Message),
                   Text,
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation);
                }

            }

        }

        private void proveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {



        }

        private void transaccionesOutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                BasicHttpBinding binding = new BasicHttpBinding();
                binding.MaxBufferPoolSize = 2147483647;
                binding.MaxReceivedMessageSize = 2147483647;
                //Specify the address to be used for the client.
                EndpointAddress address = new EndpointAddress("http://ec2-52-41-114-122.us-west-2.compute.amazonaws.com/MI3/Transacciones/TransaccionesOut.svc");
                TransaccionesOutClient TransaccionesOut = new TransaccionesOutClient(binding, address);

                List<clsBeAjustesMI3> ListOut = new List<clsBeAjustesMI3>();

                List<clsBeI_nav_transacciones_out> ListOutDespachos = new List<clsBeI_nav_transacciones_out>();

                ListOutDespachos.AddRange(TransaccionesOut.Get_Despachos_Pendientes_De_Procesar());

                string Resss = null;
                ListOut.AddRange(TransaccionesOut.Get_Ajustes_Pendientes_Envio(ref Resss));

                foreach (clsBeAjustesMI3 aj in ListOut)
                {
                    Console.WriteLine(aj.Codigo_Producto + " " + aj.TipoAjusteWMS + " " + aj.Cantidad + " " + aj.Codigo_Bodega_ERP);
                }

                Debug.Print(Resss);


            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} {1}", MethodBase.GetCurrentMethod(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }
        }

        private void proveedorBodegaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                ServiceProveedorBodegaClient srvProveedorBodega = new ServiceProveedorBodegaClient();
                TOMWMS.srvProveedorBodega.clsBeProveedor_bodega BeProveedorBodega = new TOMWMS.srvProveedorBodega.clsBeProveedor_bodega();

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} {1}", MethodBase.GetCurrentMethod(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }
        }

        private void getDespachosPendientesDeProcesarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                BasicHttpBinding binding = new BasicHttpBinding();
                //Specify the address to be used for the client.
                EndpointAddress address = new EndpointAddress("http://192.168.126.79/MI3/Transacciones/TransaccionesOut.svc");
                TransaccionesOutClient TransaccionesOut = new TransaccionesOutClient(binding, address);

                List<clsBeI_nav_transacciones_out> ListOut = new List<clsBeI_nav_transacciones_out>();

                ListOut.AddRange(TransaccionesOut.Get_Despachos_Pendientes_De_Procesar());

                foreach (clsBeI_nav_transacciones_out Tout in ListOut)
                {
                    Debug.Print(Tout.Cantidad.ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} {1}", MethodBase.GetCurrentMethod(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }

        }

        private void ajustesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                BasicHttpBinding binding = new BasicHttpBinding();
                //Specify the address to be used for the client.
                EndpointAddress address = new EndpointAddress("http://192.168.0.10/MI3/Transacciones/TransaccionesOut.svc");
                TransaccionesOutClient TransaccionesOut = new TransaccionesOutClient(binding, address);

                List<clsBeAjustesMI3> ListOut = new List<clsBeAjustesMI3>();

                string res = null;

                ListOut.AddRange(TransaccionesOut.Get_Ajustes_Pendientes_Envio(ref res));

                foreach (clsBeAjustesMI3 Tout in ListOut)
                {
                    Debug.Print(Tout.Cantidad.ToString());
                    TransaccionesOut.Actualizar_Ajuste_Procesado(Tout.IdAjusteEnc, true, "SAPREF");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} {1}", MethodBase.GetCurrentMethod(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }
        }

        private void usuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                BasicHttpBinding binding = new BasicHttpBinding();
                //Specify the address to be used for the client.
                EndpointAddress address = new EndpointAddress("http://52.41.114.122/MI3/Usuario/srvUsuario.svc");
                IsrvUsuarioClient Usuario = new IsrvUsuarioClient(binding, address);
                TOMWMS.srvUsuario.clsBeUsuario claseUsuario = new TOMWMS.srvUsuario.clsBeUsuario();

                claseUsuario.Codigo = "dts";
                claseUsuario.Clave = "dts1965!";
                claseUsuario.IdEmpresa = 1;

                if (Usuario.Usuario_Valido_By_Obj(ref claseUsuario))
                {
                    MessageBox.Show("Correcto", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Incorrecto", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} {1}", MethodBase.GetCurrentMethod(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }
        }

        private void tsmiReportes_Click(object sender, EventArgs e)
        {
            try
            {

                BasicHttpBinding binding = new BasicHttpBinding();
                //Specify the address to be used for the client.
                EndpointAddress address = new EndpointAddress("http://52.41.114.122/MI3/Reportes/srvReportes.svc");
                IsrvReportesClient Reporte = new IsrvReportesClient(binding, address);

                List<TOMWMS.srvReportes.clsBeVW_Movimientos> movimientos = new List<TOMWMS.srvReportes.clsBeVW_Movimientos>();

                int Cant = 0;

                DateTime fechaini = new DateTime(2021, 1, 1);
                DateTime fechafin = new DateTime(2021, 2, 12);

                Cant = Reporte.Get_Movimientos_Kardex_By_IdEmpresa(1, fechaini, fechafin).Length;

                if (Cant > 0)
                {
                    MessageBox.Show("Tiene Datos", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("No tiene datos", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                foreach (TOMWMS.srvReportes.clsBeVW_Movimientos r in Reporte.Get_Movimientos_Kardex_By_IdEmpresa(1, fechaini, fechafin))
                {
                    movimientos.Add(r);
                }

                if (movimientos.Count > 0)
                {
                    MessageBox.Show("Tiene Datos", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("No tiene datos", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }



                /*System.Data.DataTable tabla = new System.Data.DataTable("Datos");

                tabla = Reporte.Get_Movimientos_Kardex_Con_Docs(1, fechaini, fechafin);
                tabla = Reporte.Get_Stock_By_IdEmpresa(1);

               if (tabla.Rows.Count>0) {
                    MessageBox.Show("Tiene Datos", Text, MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("No tiene datos", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }*/



            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} {1}", MethodBase.GetCurrentMethod(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }
        }

        private void propietarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://192.168.0.79/MI3/Propietario/ServicePropietario.svc");
            ServicePropietarioClient srvProp = new ServicePropietarioClient(binding, address);
            TOMWMS.srvPropietario.clsBePropietarios prop = new TOMWMS.srvPropietario.clsBePropietarios();

            try
            {

                prop = srvProp.Get_Single_By_IdPropietario(1);


                MessageBox.Show("Tiene Datos", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} {1}", MethodBase.GetCurrentMethod(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }
        }

        private void testRecepcionHHToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                TOMHHWSSoapClient wshh = new TOMHHWSSoapClient();
                clsArchHeader h = new clsArchHeader();
                h.Tipo = "Andr";

                string path = string.Empty;
                string xmlInputData = string.Empty;
                string xmlOutputData = string.Empty;

                path = Directory.GetCurrentDirectory() + @"\pRec.xml";
                xmlInputData = File.ReadAllText(path);

                TOMWMS.WSHH.clsBeTrans_re_enc pRecEnc = Deserialize<TOMWMS.WSHH.clsBeTrans_re_enc>(xmlInputData);

                wshh.Guardar_Recepcion(h,
                                       pRecEnc,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       1,
                                       1,
                                       1,
                                       1);

                //MessageBox.Show("Tiene Datos", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} {1}", MethodBase.GetCurrentMethod(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }
        }

        public T Deserialize<T>(string input) where T : class
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public string Serialize<T>(T ObjectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                return textWriter.ToString();
            }
        }
        private void getAlmacenajeHistoricoByIdPropietarioAndRangoFechasToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                BasicHttpBinding binding = new BasicHttpBinding();
                binding.MaxBufferPoolSize = 2147483647;
                binding.MaxReceivedMessageSize = 2147483647;
                //Specify the address to be used for the client.
                EndpointAddress address = new EndpointAddress("http://192.168.0.98/MI3/Transacciones/TransaccionesOut.svc");
                TransaccionesOutClient TransaccionesOut = new TransaccionesOutClient(binding, address);

                List<clsBeStock_jornada_logistico> ListOut = new List<clsBeStock_jornada_logistico>();

                //Unidad de medida basica
                //ListOut.AddRange(TransaccionesOut.Get_Almacenaje_Historico_By_IdPropietario_And_Rango_Fechas(11,
                //                                                                                            0,
                //                                                                                            "3471705354",
                //                                                                                            new DateTime(2021, 9, 1),
                //                                                                                            new DateTime(2021, 9, 23),
                //                                                                                            clsDataContractDI.tTipoAlmacen.General,
                //                                                                                            "",
                //                                                                                            clsDataContractDI.tRubroERP.CantidadUMBas,
                //                                                                                            ""));

                //Por posicion
                //ListOut.AddRange(TransaccionesOut.Get_Almacenaje_Historico_By_IdPropietario_And_Rango_Fechas(11,
                //                                                                                             0,
                //                                                                                             "3471705354",
                //                                                                                             new DateTime(2021, 9, 1),
                //                                                                                             new DateTime(2021, 9, 23),
                //                                                                                             clsDataContractDI.tTipoAlmacen.General,
                //                                                                                             "",
                //                                                                                             clsDataContractDI.tRubroERP.Posicion,
                //                                                                                             "Motores Electrícos"));

                //Por presentación
                //ListOut.AddRange(TransaccionesOut.Get_Almacenaje_Historico_By_IdPropietario_And_Rango_Fechas(1711,
                //                                                                                             0,
                //                                                                                             "0000080",
                //                                                                                             new DateTime(2021, 9, 1),
                //                                                                                             new DateTime(2021, 9, 23),
                //                                                                                             clsDataContractDI.tTipoAlmacen.General,
                //                                                                                             "",
                //                                                                                             clsDataContractDI.tRubroERP.Presentacion,
                //                                                                                             "YUPS004"));

                //Por tarimas
                //ListOut.AddRange(TransaccionesOut.Get_Almacenaje_Historico_By_IdPropietario_And_Rango_Fechas(5,
                //                                                                                             0,
                //                                                                                             "0000005",
                //                                                                                             new DateTime(2021, 9, 1),
                //                                                                                             new DateTime(2021, 9, 23),
                //                                                                                             clsDataContractDI.tTipoAlmacen.General,
                //                                                                                             "",
                //                                                                                             clsDataContractDI.tRubroERP.Tarimas,
                //                                                                                             "TEJAS MARSELLESA"));

                //Por CIF_DAI_IVA
                //ListOut.AddRange(TransaccionesOut.Get_Almacenaje_Historico_By_IdPropietario_And_Rango_Fechas(1,
                //                                                                                             0,
                //                                                                                             "3401703044",
                //                                                                                             new DateTime(2021, 9, 1),
                //                                                                                             new DateTime(2021, 9, 23),
                //                                                                                             clsDataContractDI.tTipoAlmacen.Fiscal,
                //                                                                                             "",
                //                                                                                             clsDataContractDI.tRubroERP.CIF_DAI_IVA,
                //                                                                                             "ALG987"));

                ////Por Clasificacion
                ListOut.AddRange(TransaccionesOut.Get_Almacenaje_Historico_By_IdPropietario_And_Rango_Fechas(40,
                                                                                                             0,
                                                                                                             "",
                                                                                                             new DateTime(2021, 9, 1),
                                                                                                             new DateTime(2021, 9, 23),
                                                                                                             (IdTipoAlmacen)TOMWMS.clsDataContractDI.tTipoAlmacen.General,
                                                                                                             "5009",
                                                                                                             (IdTipoRubroERP)TOMWMS.clsDataContractDI.tRubroERP.Clasificacion,
                                                                                                             ""));

                dgrid.DataSource = ListOut;

                foreach (clsBeStock_jornada_logistico sj in ListOut)
                {
                    Console.WriteLine(sj._Codigo_Producto + " " + " " + sj._Nombre_Producto + " " + sj._Numero_Orden);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} {1}", MethodBase.GetCurrentMethod(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }
        }

        private void getServiciosHistoricoByIdPropietarioAndRangoFechasToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                BasicHttpBinding binding = new BasicHttpBinding();
                binding.MaxBufferPoolSize = 2147483647;
                binding.MaxReceivedMessageSize = 2147483647;
                //Specify the address to be used for the client.
                EndpointAddress address = new EndpointAddress("http://192.168.0.6/MI3/Transacciones/TransaccionesOut.svc");
                TransaccionesOutClient TransaccionesOut = new TransaccionesOutClient(binding, address);

                List<clsBe_Servicio_Logistico> ListOut = new List<clsBe_Servicio_Logistico>();

                DateTime fecha_ini = new DateTime(2021, 5, 25);
                DateTime fecha_fin = new DateTime(2021, 5, 25);

                ListOut.AddRange(TransaccionesOut.Get_Servicios_By_IdPropietario_And_Rango_Fechas(735, 735, "43325235", fecha_ini, fecha_fin, (IdTipoAlmacen)TOMWMS.clsDataContractDI.tTipoAlmacen.General, "2.2"));

                dgrid.DataSource = ListOut;

                foreach (clsBe_Servicio_Logistico sj in ListOut)
                {
                    Console.WriteLine(sj._Codigo_producto + " " + " " + sj._Fecha_Servicio + " " + sj._No_orden);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} {1}", MethodBase.GetCurrentMethod(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }
        }

        private void getSingleMI3ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                BasicHttpBinding binding = new BasicHttpBinding();
                EndpointAddress address = new EndpointAddress("http://192.168.126.79/MI3/Transacciones/TransaccionesOut.svc");
                TransaccionesOutClient TransaccionesOut = new TransaccionesOutClient(binding, address);

                int resultado = TransaccionesOut.Actualizar_Registro_Procesado_By_IdDespacho_And_Codigo_Producto("A", 0, "");

                Debug.WriteLine("Regulstado " + resultado);

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} {1}", MethodBase.GetCurrentMethod(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }

        }

        private void ubicacionesDeLosLotesDeLasOPToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                PedidoCompraClient sv = new PedidoCompraClient();
                BasicHttpBinding binding = new BasicHttpBinding();
                EndpointAddress address = new EndpointAddress("http://192.168.0.5/MI3/Transacciones/PedidoCompra.svc");
                PedidoCompraClient pedidoCompra = new PedidoCompraClient(binding, address);

                string pNoDocumentoIngreso;
                int pNoLinea;
                string pCodigoProducto;
                double pCantidad;
                System.DateTime pFechaVence;
                string pLote;
                string pLicencia;
                string pUbicacionNAV;

                pNoDocumentoIngreso = "OP-00038571";
                pNoLinea = 10000;
                pCodigoProducto = "00710002";
                pCantidad = 1250;
                pLote = "L.01.02.22";
                pFechaVence = new DateTime(2022, 8, 1);
                pLicencia = "BA000200710002000523";
                pUbicacionNAV = "UA - 133016";

                //10000   00710002    450 0   L.01.02.22  2022 - 08 - 01  BA000200710002000525 UA-133017
                //10000   00710002    750 0   L.01.02.22  2022 - 08 - 01  BA000200710002000526 UA-133018
                //10000   00710002    1000    0   L.01.02.22  2022 - 08 - 01  BA000200710002000527 UA-133019
                //10000   00025002    150 0   L.01.02.22  2023 - 08 - 01  BA000200025002000528 UA-133020
                //10000   00025002    650 0   L.01.02.22  2023 - 08 - 01  BA000200025002000529 UA-133021
                //10000   00710002    1300    0   L.01.02.22  2022 - 08 - 01  BA000200710002000524 UA-133022


                Boolean resultado = pedidoCompra.Registrar_Lote_Documento_Ingreso(pNoDocumentoIngreso,
                                                                                  pNoLinea,
                                                                                  pCodigoProducto,
                                                                                  pCantidad,
                                                                                  pFechaVence,
                                                                                  pLote,
                                                                                  pLicencia,
                                                                                  pUbicacionNAV);

                Debug.WriteLine("Resultado " + resultado);

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} {1}", MethodBase.GetCurrentMethod(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }

        }

        private void frmInicio_Load(object sender, EventArgs e)
        {

        }

        private void reservaCaso1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void productoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            BasicHttpBinding binding = new BasicHttpBinding();
            //Specify the address to be used for the client.
            EndpointAddress address = new EndpointAddress("http://10.10.1.19/MI3/Producto/ServiceProducto.svc");

            ServiceProductoClient Producto = new ServiceProductoClient(binding, address);

            int IdProducto = 0;

            IdProducto = Producto.Get_IdProducto_By_Codigo("40002");

            Debug.Print(IdProducto.ToString());

            TOMWMS.srvProducto.clsBeProducto beProd = new TOMWMS.srvProducto.clsBeProducto();

            beProd.IdProducto = 7506;
            beProd.Nombre = "Probando";
            beProd.IdPropietario = 1;
            beProd.IdClasificacion = 13;
            beProd.IdFamilia = 86;
            beProd.IdMarca = 1;
            beProd.IdTipoProducto = 514;
            beProd.IdUnidadMedidaBasica = 96;
            beProd.IdTipoRotacion = 3;
            beProd.IdIndiceRotacion = 3;
            beProd.Codigo = "7506";
            beProd.Codigo_barra = "7506";
            beProd.Precio = 0;
            beProd.Activo = true;

            Producto.Insert_Single(beProd);

            txtResult.Text = "Se insertó el producto " + beProd.Codigo.ToString();

            //foreach (clsBeI_nav_transacciones_out Tout in ListOut)
            //{
            //    Debug.Print(Tout.Cantidad.ToString());
            //}

        }

        //private void pedidoClienteToolStripMenuItem_Click(object sender, EventArgs e)
        //{

        //}
    }
}
