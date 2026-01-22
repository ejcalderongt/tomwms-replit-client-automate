using WMS.EntityCore.Picking;
using WMS.EntityCore.Producto;

namespace WMSWebAPI.Dtos.Picking
{
    public class PickingDetDto
    {
        public int IdPickingDet { get; set; } = 0;
        public int IdPickingEnc { get; set; } = 0;
        public int IdPedidoEnc { get; set; } = 0;
        public int IdPedidoDet { get; set; } = 0;
        public int IdOperadorBodega { get; set; } = 0;
        public double Cantidad { get; set; } = 0;
        public int Cliente_dias { get; set; } = 0;
        public double Cantidad_recibida { get; set; } = 0;
        public string User_agr { get; set; } = string.Empty;
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = string.Empty;
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = false;
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public bool IsNew { get; set; }
        public string Bodega { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public string Propietario { get; set; } = string.Empty;
        public DateTime FechaPedido { get; set; }
        public string No_Documento { get; set; } = string.Empty;
        public int IdMuelle { get; set; }
        public DateTime Hora_Inicio { get; set; }
        public DateTime Hora_fin { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string UbicacionPicking { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
        public string NombreProducto { get; set; } = string.Empty;
        public DateTime Fecha_Ingreso { get; set; }
        public DateTime Fecha_Vence { get; set; }
        public string Presetacion { get; set; } = string.Empty;
        public decimal Factorx { get; set; }
        public decimal CantidadReservada { get; set; }
        public decimal Cantidad_Pickeada { get; set; }
        public decimal Cantidad_Verificada { get; set; }
        public decimal Cantidad_Stock { get; set; }
        public int IdUbicacion { get; set; }
        public string UMBas { get; set; } = string.Empty;
        public string Lic_Plate { get; set; } = string.Empty;
        public string Lote { get; set; } = string.Empty;        
        public List<clsBeTrans_picking_det_parametros> ListaDetalleParametro { get; set; } = new List<clsBeTrans_picking_det_parametros>();
        public string Bono { get; set; } = string.Empty;
        public clsBeProducto Producto { get; set; } = new clsBeProducto();
        public clsBeProducto_presentacion Presentacion { get; set; } = new clsBeProducto_presentacion();
        public clsBeProducto_estado ProductoEstado { get; set; } = new clsBeProducto_estado();
        public clsBeUnidad_medida UnidadMedida { get; set; } = new clsBeUnidad_medida();

    }
}
