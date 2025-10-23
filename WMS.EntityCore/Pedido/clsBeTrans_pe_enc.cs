using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.EntityCore.Cliente;
using WMS.EntityCore.Picking;
using WMS.EntityCore.Propietario;

namespace WMS.EntityCore.Pedido
{
    public class clsBeTrans_pe_enc : ICloneable
    {
        [Column("IdPedidoEnc")]
        [DisplayName("IdPedidoEnc")]
        public int IdPedidoEnc { get; set; } = 0;

        [Column("IdBodega")]
        [DisplayName("IdBodega")]
        public int IdBodega { get; set; } = 0;

        [Column("IdCliente")]
        [DisplayName("IdCliente")]
        public int IdCliente { get; set; } = 0;

        [Column("IdMuelle")]
        [DisplayName("IdMuelle")]
        public int IdMuelle { get; set; } = 0;

        [Column("IdPropietarioBodega")]
        [DisplayName("IdPropietarioBodega")]
        public int IdPropietarioBodega { get; set; } = 0;

        [Column("IdTipoPedido")]
        [DisplayName("IdTipoPedido")]
        public int IdTipoPedido { get; set; } = 0;

        [Column("IdPickingEnc")]
        [DisplayName("IdPickingEnc")]
        public int IdPickingEnc { get; set; } = 0;

        [Column("Fecha_Pedido")]
        [DisplayName("Fecha_Pedido")]
        public DateTime Fecha_Pedido { get; set; } = DateTime.Now;

        [Column("hora_ini")]
        [DisplayName("hora_ini")]
        public DateTime Hora_ini { get; set; } = DateTime.Now;

        [Column("hora_fin")]
        [DisplayName("hora_fin")]
        public DateTime Hora_fin { get; set; } = DateTime.Now;

        [Column("ubicacion")]
        [DisplayName("ubicacion")]
        public string Ubicacion { get; set; } = "";

        [Column("estado")]
        [DisplayName("estado")]
        public string Estado { get; set; } = "";

        [Column("no_despacho")]
        [DisplayName("no_despacho")]
        public int No_despacho { get; set; } = 0;

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("user_agr")]
        [DisplayName("user_agr")]
        public string User_agr { get; set; } = "";

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;

        [Column("user_mod")]
        [DisplayName("user_mod")]
        public string User_mod { get; set; } = "";

        [Column("fec_mod")]
        [DisplayName("fec_mod")]
        public DateTime Fec_mod { get; set; } = DateTime.Now;

        [Column("no_documento")]
        [DisplayName("no_documento")]
        public int No_documento { get; set; } = 0;

        [Column("local")]
        [DisplayName("local")]
        public bool Local { get; set; } = false;

        [Column("pallet_primero")]
        [DisplayName("pallet_primero")]
        public bool Pallet_primero { get; set; } = false;

        [Column("dias_cliente")]
        [DisplayName("dias_cliente")]
        public double Dias_cliente { get; set; } = 0;

        [Column("anulado")]
        [DisplayName("anulado")]
        public bool Anulado { get; set; } = false;

        [Column("RoadKilometraje")]
        [DisplayName("RoadKilometraje")]
        public double RoadKilometraje { get; set; } = 0;

        [Column("RoadFechaEntr")]
        [DisplayName("RoadFechaEntr")]
        public DateTime RoadFechaEntr { get; set; } = DateTime.Now;

        [Column("RoadDirEntrega")]
        [DisplayName("RoadDirEntrega")]
        public string RoadDirEntrega { get; set; } = "";

        [Column("RoadTotal")]
        [DisplayName("RoadTotal")]
        public double RoadTotal { get; set; } = 0;

        [Column("RoadDesMonto")]
        [DisplayName("RoadDesMonto")]
        public double RoadDesMonto { get; set; } = 0;

        [Column("RoadImpMonto")]
        [DisplayName("RoadImpMonto")]
        public double RoadImpMonto { get; set; } = 0;

        [Column("RoadPeso")]
        [DisplayName("RoadPeso")]
        public double RoadPeso { get; set; } = 0;

        [Column("RoadBandera")]
        [DisplayName("RoadBandera")]
        public string RoadBandera { get; set; } = "";

        [Column("RoadStatCom")]
        [DisplayName("RoadStatCom")]
        public string RoadStatCom { get; set; } = "";

        [Column("RoadCalcoBJ")]
        [DisplayName("RoadCalcoBJ")]
        public string RoadCalcoBJ { get; set; } = "";

        [Column("RoadImpres")]
        [DisplayName("RoadImpres")]
        public int RoadImpres { get; set; } = 0;

        [Column("RoadADD1")]
        [DisplayName("RoadADD1")]
        public string RoadADD1 { get; set; } = "";

        [Column("RoadADD2")]
        [DisplayName("RoadADD2")]
        public string RoadADD2 { get; set; } = "";

        [Column("RoadADD3")]
        [DisplayName("RoadADD3")]
        public string RoadADD3 { get; set; } = "";

        [Column("RoadStatProc")]
        [DisplayName("RoadStatProc")]
        public string RoadStatProc { get; set; } = "";

        [Column("RoadRechazado")]
        [DisplayName("RoadRechazado")]
        public bool RoadRechazado { get; set; } = false;

        [Column("RoadRazon_Rechazado")]
        [DisplayName("RoadRazon_Rechazado")]
        public string RoadRazon_Rechazado { get; set; } = "";

        [Column("RoadInformado")]
        [DisplayName("RoadInformado")]
        public bool RoadInformado { get; set; } = false;

        [Column("RoadSucursal")]
        [DisplayName("RoadSucursal")]
        public string RoadSucursal { get; set; } = "";

        [Column("RoadIdDespacho")]
        [DisplayName("RoadIdDespacho")]
        public int RoadIdDespacho { get; set; } = 0;

        [Column("RoadIdFacturacion")]
        [DisplayName("RoadIdFacturacion")]
        public int RoadIdFacturacion { get; set; } = 0;

        [Column("RoadIdRuta")]
        [DisplayName("RoadIdRuta")]
        public int RoadIdRuta { get; set; } = 0;

        [Column("RoadIdVendedor")]
        [DisplayName("RoadIdVendedor")]
        public int RoadIdVendedor { get; set; } = 0;

        [Column("RoadIdRutaDespacho")]
        [DisplayName("RoadIdRutaDespacho")]
        public int RoadIdRutaDespacho { get; set; } = 0;

        [Column("RoadIdVendedorDespacho")]
        [DisplayName("RoadIdVendedorDespacho")]
        public int RoadIdVendedorDespacho { get; set; } = 0;

        [Column("Observacion")]
        [DisplayName("Observacion")]
        public string Observacion { get; set; } = "";

        [Column("PedidoRoad")]
        [DisplayName("PedidoRoad")]
        public bool PedidoRoad { get; set; } = false;

        [Column("HoraEntregaDesde")]
        [DisplayName("HoraEntregaDesde")]
        public DateTime HoraEntregaDesde { get; set; } = DateTime.Now;

        [Column("HoraEntregaHasta")]
        [DisplayName("HoraEntregaHasta")]
        public DateTime HoraEntregaHasta { get; set; } = DateTime.Now;

        [Column("referencia")]
        [DisplayName("referencia")]
        public string Referencia { get; set; } = "";

        [Column("IdMotivoAnulacionBodega")]
        [DisplayName("IdMotivoAnulacionBodega")]
        public int IdMotivoAnulacionBodega { get; set; } = 0;

        [Column("Enviado_A_ERP")]
        [DisplayName("Enviado_A_ERP")]
        public bool Enviado_A_ERP { get; set; } = false;

        [Column("control_ultimo_lote")]
        [DisplayName("control_ultimo_lote")]
        public bool Control_ultimo_lote { get; set; } = false;

        [Column("serie")]
        [DisplayName("serie")]
        public string Serie { get; set; } = "";

        [Column("correlativo")]
        [DisplayName("correlativo")]
        public int Correlativo { get; set; } = 0;

        [Column("Referencia_Documento_Ingreso_Bodega_Destino")]
        [DisplayName("Referencia_Documento_Ingreso_Bodega_Destino")]
        public string Referencia_Documento_Ingreso_Bodega_Destino { get; set; } = "";

        [Column("sync_mi3")]
        [DisplayName("sync_mi3")]
        public bool Sync_mi3 { get; set; } = false;

        [Column("No_Picking_ERP")]
        [DisplayName("No_Picking_ERP")]
        public string No_Picking_ERP { get; set; } = "";

        [Column("no_documento_externo")]
        [DisplayName("no_documento_externo")]
        public string No_documento_externo { get; set; } = "";

        [Column("requiere_tarimas")]
        [DisplayName("requiere_tarimas")]
        public bool Requiere_tarimas { get; set; } = false;

        [Column("fecha_preparacion")]
        [DisplayName("fecha_preparacion")]
        public DateTime Fecha_preparacion { get; set; } = DateTime.Now;

        [Column("IdTipoManufactura")]
        [DisplayName("IdTipoManufactura")]
        public int IdTipoManufactura { get; set; } = 0;

        [Column("bodega_origen")]
        [DisplayName("bodega_origen")]
        public string Bodega_origen { get; set; } = "";

        [Column("bodega_destino")]
        [DisplayName("bodega_destino")]
        public string Bodega_destino { get; set; } = "";

        [Column("IdMotivoDevolucion")]
        [DisplayName("IdMotivoDevolucion")]
        public int IdMotivoDevolucion { get; set; } = 0;
        public bool IsNew { get; set; } = false;
        public List<clsBeTrans_pe_det> Detalle { get; set; } = new List<clsBeTrans_pe_det>();
        public clsBeTrans_picking_enc Picking { get; set; } = new clsBeTrans_picking_enc();
        public clsBePropietario_bodega PropietarioBodega { get; set; } = new clsBePropietario_bodega();
        public clsBeCliente Cliente { get; set; } = new clsBeCliente();
        public clsBeTrans_pe_tipo TipoPedido { get; set; } = new clsBeTrans_pe_tipo();
        public bool Control_Ultimo_Lote { get; set; } = false;        
        public clsBeTrans_pe_pol ObjPoliza { get; set; } = new clsBeTrans_pe_pol();        
        public clsBeTrans_pe_enc() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}