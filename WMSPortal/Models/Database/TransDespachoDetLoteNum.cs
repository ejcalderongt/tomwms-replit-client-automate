#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransDespachoDetLoteNum
    {
        public int IdDespachoDetLote { get; set; }
        public int IdDespachoEnc { get; set; }
        public int? IdPedidoEnc { get; set; }
        public int? IdProductoBodega { get; set; }
        public string Lote { get; set; }
        public int? LoteNum { get; set; }
        public double? CantidadDespachada { get; set; }
        public double? PesoDespachado { get; set; }
        public int? IdProductoEstado { get; set; }
    }
}
