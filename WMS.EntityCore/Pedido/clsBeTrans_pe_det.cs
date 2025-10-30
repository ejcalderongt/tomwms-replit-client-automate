using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.EntityCore.Picking;
using WMS.EntityCore.Producto;
using WMSWebAPI.Be;

namespace WMS.EntityCore.Pedido
{
    public class clsBeTrans_pe_det : ICloneable
    {
        [Column("IdPedidoDet")]
        [DisplayName("IdPedidoDet")]
        public int IdPedidoDet { get; set; } = 0;

        [Column("IdPedidoEnc")]
        [DisplayName("IdPedidoEnc")]
        public int IdPedidoEnc { get; set; } = 0;

        [Column("IdProductoBodega")]
        [DisplayName("IdProductoBodega")]
        public int IdProductoBodega { get; set; } = 0;

        [Column("IdEstado")]
        [DisplayName("IdEstado")]
        public int IdEstado { get; set; } = 0;

        [Column("IdPresentacion")]
        [DisplayName("IdPresentacion")]
        public int IdPresentacion { get; set; } = 0;

        [Column("IdUnidadMedidaBasica")]
        [DisplayName("IdUnidadMedidaBasica")]
        public int IdUnidadMedidaBasica { get; set; } = 0;

        [Column("Cantidad")]
        [DisplayName("Cantidad")]
        public double Cantidad { get; set; } = 0;

        [Column("Peso")]
        [DisplayName("Peso")]
        public double Peso { get; set; } = 0;

        [Column("Precio")]
        [DisplayName("Precio")]
        public double Precio { get; set; } = 0;

        [Column("no_recepcion")]
        [DisplayName("no_recepcion")]
        public int No_recepcion { get; set; } = 0;

        [Column("ndias")]
        [DisplayName("ndias")]
        public int Ndias { get; set; } = 0;

        [Column("cant_despachada")]
        [DisplayName("cant_despachada")]
        public double Cant_despachada { get; set; } = 0;

        [Column("codigo_producto")]
        [DisplayName("codigo_producto")]
        public string Codigo_producto { get; set; } = "";

        [Column("nombre_producto")]
        [DisplayName("nombre_producto")]
        public string Nombre_producto { get; set; } = "";

        [Column("nom_presentacion")]
        [DisplayName("nom_presentacion")]
        public string Nom_presentacion { get; set; } = "";

        [Column("nom_unid_med")]
        [DisplayName("nom_unid_med")]
        public string Nom_unid_med { get; set; } = "";

        [Column("nom_estado")]
        [DisplayName("nom_estado")]
        public string Nom_estado { get; set; } = "";

        [Column("user_agr")]
        [DisplayName("user_agr")]
        public string User_agr { get; set; } = "";

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;

        [Column("fecha_especifica")]
        [DisplayName("fecha_especifica")]
        public bool Fecha_especifica { get; set; } = false;

        [Column("RoadDes")]
        [DisplayName("RoadDes")]
        public double RoadDes { get; set; } = 0;

        [Column("RoadDesMon")]
        [DisplayName("RoadDesMon")]
        public double RoadDesMon { get; set; } = 0;

        [Column("RoadTotal")]
        [DisplayName("RoadTotal")]
        public double RoadTotal { get; set; } = 0;

        [Column("RoadPrecioDoc")]
        [DisplayName("RoadPrecioDoc")]
        public double RoadPrecioDoc { get; set; } = 0;

        [Column("RoadVAL1")]
        [DisplayName("RoadVAL1")]
        public double RoadVAL1 { get; set; } = 0;

        [Column("RoadVAL2")]
        [DisplayName("RoadVAL2")]
        public string RoadVAL2 { get; set; } = "";

        [Column("RoadCantProc")]
        [DisplayName("RoadCantProc")]
        public double RoadCantProc { get; set; } = 0;

        [Column("peso_despachado")]
        [DisplayName("peso_despachado")]
        public double Peso_despachado { get; set; } = 0;

        [Column("no_linea")]
        [DisplayName("no_linea")]
        public int No_linea { get; set; } = 0;

        [Column("atributo_variante_1")]
        [DisplayName("atributo_variante_1")]
        public string Atributo_variante_1 { get; set; } = "";

        [Column("IdStockEspecifico")]
        [DisplayName("IdStockEspecifico")]
        public int IdStockEspecifico { get; set; } = 0;

        [Column("EsPadre")]
        [DisplayName("EsPadre")]
        public bool EsPadre { get; set; } = false;

        [Column("IdPedidoDetPadre")]
        [DisplayName("IdPedidoDetPadre")]
        public int IdPedidoDetPadre { get; set; } = 0;

        [Column("Peso_Bruto")]
        [DisplayName("Peso_Bruto")]
        public double Peso_Bruto { get; set; } = 0;

        [Column("Peso_Neto")]
        [DisplayName("Peso_Neto")]
        public double Peso_Neto { get; set; } = 0;

        [Column("Costo")]
        [DisplayName("Costo")]
        public double Costo { get; set; } = 0;

        [Column("valor_aduana")]
        [DisplayName("valor_aduana")]
        public double Valor_aduana { get; set; } = 0;

        [Column("valor_fob")]
        [DisplayName("valor_fob")]
        public double Valor_fob { get; set; } = 0;

        [Column("valor_iva")]
        [DisplayName("valor_iva")]
        public double Valor_iva { get; set; } = 0;

        [Column("valor_dai")]
        [DisplayName("valor_dai")]
        public double Valor_dai { get; set; } = 0;

        [Column("valor_seguro")]
        [DisplayName("valor_seguro")]
        public double Valor_seguro { get; set; } = 0;

        [Column("valor_flete")]
        [DisplayName("valor_flete")]
        public double Valor_flete { get; set; } = 0;

        [Column("Total_linea")]
        [DisplayName("Total_linea")]
        public double Total_linea { get; set; } = 0;

        [Column("IdCliente")]
        [DisplayName("IdCliente")]
        public int IdCliente { get; set; } = 0;
        public bool IsNew { get; set; } = false;
        public clsBeProducto Producto { get; set; } = new clsBeProducto();
        public clsBeProducto_presentacion Presentacion { get; set; } = new clsBeProducto_presentacion();
        public clsBeUnidad_medida UnidadMedida { get; set; } = new clsBeUnidad_medida();
        public List<clsBeStock_res> ListaStockRes { get; set; } = new List<clsBeStock_res>();
        public List<clsBeTrans_picking_ubic> ListaPickingUbic { get; set; } = new List<clsBeTrans_picking_ubic>();
        public string Codigo_Producto { get; set; } = string.Empty;
        public string NombreProducto { get; set; } = string.Empty;
        public string ProductoPresentacion { get; set; } = string.Empty;
        public string ProductoUnidadMedida { get; set; } = string.Empty;
        public string ProductoEstado { get; set; } = string.Empty;
        public string BodegaUbicacion { get; set; } = string.Empty;
        public double CantidadFisica { get; set; } = 0.0;
        public double Factor { get; set; } = 0.0;
        public double CantidadReservada { get; set; } = 0.0;
        public double PesoReservado { get; set; } = 0.0;
        public DateTime FechaIngreso { get; set; } = DateTime.MinValue;
        public DateTime FechaVence { get; set; } = DateTime.MinValue;
        public clsBeTrans_pe_det() { }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}