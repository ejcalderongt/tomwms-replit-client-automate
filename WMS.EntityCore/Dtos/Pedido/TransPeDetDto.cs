using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Picking;
using WMSWebAPI.Dtos.Productos;
using WMSWebAPI.Dtos.Stock;

namespace WMSWebAPI.Dtos.Pedido
{
    public class TransPeDetDto
    {
        public int IdPedidoDet { get; set; } = 0;
        public int IdPedidoEnc { get; set; } = 0;
        public int IdProductoBodega { get; set; } = 0;
        public int IdEstado { get; set; } = 0;
        public int IdPresentacion { get; set; } = 0;
        public int IdUnidadMedidaBasica { get; set; } = 0;
        public double Cantidad { get; set; } = 0;
        public double Peso { get; set; } = 0;
        public double Precio { get; set; } = 0;
        public int No_recepcion { get; set; } = 0;
        public int Ndias { get; set; } = 0;
        public double Cant_despachada { get; set; } = 0;
        public string Codigo_producto { get; set; } = string.Empty;
        public string Nombre_producto { get; set; } = string.Empty;
        public string Nom_presentacion { get; set; } = string.Empty;
        public string Nom_unid_med { get; set; } = string.Empty;
        public string Nom_estado { get; set; } = string.Empty;
        public string User_agr { get; set; } = string.Empty;
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public bool Fecha_especifica { get; set; } = false;
        public double RoadDes { get; set; } = 0;
        public double RoadDesMon { get; set; } = 0;
        public double RoadTotal { get; set; } = 0;
        public double RoadPrecioDoc { get; set; } = 0;
        public double RoadVAL1 { get; set; } = 0;
        public string RoadVAL2 { get; set; } = string.Empty;
        public double RoadCantProc { get; set; } = 0;
        public double Peso_despachado { get; set; } = 0;
        public int No_linea { get; set; } = 0;
        public string Atributo_variante_1 { get; set; } = string.Empty;
        public int IdStockEspecifico { get; set; } = 0;
        public bool EsPadre { get; set; } = false;
        public int IdPedidoDetPadre { get; set; } = 0;
        public double Peso_Bruto { get; set; } = 0;
        public double Peso_Neto { get; set; } = 0;
        public double Costo { get; set; } = 0;
        public double Valor_aduana { get; set; } = 0;
        public double Valor_fob { get; set; } = 0;
        public double Valor_iva { get; set; } = 0;
        public double Valor_dai { get; set; } = 0;
        public double Valor_seguro { get; set; } = 0;
        public double Valor_flete { get; set; } = 0;
        public double Total_linea { get; set; } = 0;
        public int IdCliente { get; set; } = 0;
        public bool IsNew { get; set; } = true;
        public ProductoDto Producto { get; set; } = new ProductoDto();
        public ProductoPresentacionDto Presentacion { get; set; } = new ProductoPresentacionDto();
        public UnidadMedidaDto UnidadMedida { get; set; } = new UnidadMedidaDto();
        public List<StockResDto> ListaStockRes { get; set; } = new List<StockResDto>();
        public List<PickingUbicDto> ListaPickingUbic { get; set; } = new List<PickingUbicDto>();
        public string NombreProducto { get; set; } = string.Empty;
        public string ProductoPresentacion { get; set; } = string.Empty;
        public string ProductoUnidadMedida { get; set; } = string.Empty;
        public string ProductoEstado { get; set; } = string.Empty;
        public string BodegaUbicacion { get; set; } = string.Empty;
        public decimal CantidadFisica { get; set; } = 0;
        public decimal Factor { get; set; } = 0;
        public decimal CantidadReservada { get; set; } = 0;
        public decimal PesoReservado { get; set; } = 0;
        public DateTime FechaIngreso { get; set; } = new DateTime(1900, 1, 1);
        public DateTime FechaVence { get; set; } = new DateTime(1900, 1, 1);
    }
}