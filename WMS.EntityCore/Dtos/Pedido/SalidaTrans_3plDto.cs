using WMSWebAPI.Dtos.Datos_Maestros;
using WMSWebAPI.Dtos.Operador;
using WMSWebAPI.Dtos.Picking;

namespace WMSWebAPI.Dtos.Pedido
{
    public class SalidaTrans_3plDto
    {
        public TransPeTipoDto? TipoPedido { get; set; }
        public TransPeEncDto? Encabezado { get; set; }
        public List<TransPeDet_3plDto>? Detalle { get; set; }
        public List<TransPePolDto>? Poliza { get; set; }
        public Picking_3plDto? Picking { get; set; }
        public BodegaMuelleDto? BodegaMuelle { get; set; }
        public List<ClienteDto>? Cliente { get; set; }
        public List<OperadorDto>? Operadores { get; set; }
        public List<OperadorBodegaDto>? OperadorBodega { get; set; }
       
    }
}