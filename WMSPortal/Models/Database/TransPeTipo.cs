#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransPeTipo
    {
        public int IdTipoPedido { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Preparar { get; set; }
        public bool? Verificar { get; set; }
        public bool? ReservaStock { get; set; }
        public bool? ImprimeBarrasPicking { get; set; }
        public bool? ImprimeBarrasPacking { get; set; }
        public bool? ControlPoliza { get; set; }
        public bool? GenerarPedidoIngresoBodegaDestino { get; set; }
        public int? IdTipoIngresoOc { get; set; }
        public bool? RequerirDocumentoRef { get; set; }
        public bool? TrasladarLotesDocIngreso { get; set; }
        public bool? Activo { get; set; }
        public bool? RequerirClienteEsBodegaWms { get; set; }
    }
}
