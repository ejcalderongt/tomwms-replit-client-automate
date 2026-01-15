using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.EntityCore.Producto;

namespace WMS.EntityCore.Picking
{
    public class clsBeTrans_picking_det_3pl : ICloneable
    {
        [Column("IdPickingDet")]
        [DisplayName("IdPickingDet")]
        public int IdPickingDet { get; set; } = 0;

        [Column("IdPickingEnc")]
        [DisplayName("IdPickingEnc")]
        public int IdPickingEnc { get; set; } = 0;

        [Column("IdPedidoEnc")]
        [DisplayName("IdPedidoEnc")]
        public int IdPedidoEnc { get; set; } = 0;

        [Column("IdPedidoDet")]
        [DisplayName("IdPedidoDet")]
        public int IdPedidoDet { get; set; } = 0;

        [Column("IdOperadorBodega")]
        [DisplayName("IdOperadorBodega")]
        public int IdOperadorBodega { get; set; } = 0;

        [Column("cantidad")]
        [DisplayName("cantidad")]
        public double Cantidad { get; set; } = 0;

        [Column("cliente_dias")]
        [DisplayName("cliente_dias")]
        public int Cliente_dias { get; set; } = 0;

        [Column("cantidad_recibida")]
        [DisplayName("cantidad_recibida")]
        public double Cantidad_recibida { get; set; } = 0;

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

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("codigo")]
        [DisplayName("codigo")]
        public string Codigo { get; set; } = "";

        // No se usa en el DTO actual
        // [Column("nombre")]
        // [DisplayName("nombre")]
        // public string Nombre { get; set; } = "";

        public bool IsNew { get; set; } = false;
        public string Bodega { get; set; } = "";
        public string Cliente { get; set; } = "";
        public string Propietario { get; set; } = "";
        public DateTime FechaPedido { get; set; }

        public int No_Documento { get; set; }
        public int IdMuelle { get; set; } = 0;
        public DateTime Hora_Inicio { get; set; }
        public DateTime Hora_fin { get; set; }

        public string? Estado { get; set; } = null;
        public string? UbicacionPicking { get; set; } = null;
        public string? Referencia { get; set; } = null;

        public string NombreProducto { get; set; } = "";
        public DateTime Fecha_Ingreso { get; set; }
        public DateTime Fecha_Vence { get; set; }

        public string? Presetacion { get; set; } = null;

        public decimal Factorx { get; set; } = 0m;
        public decimal CantidadReservada { get; set; } = 0m;
        public decimal Cantidad_Pickeada { get; set; } = 0m;
        public decimal Cantidad_Verificada { get; set; } = 0m;
        public decimal Cantidad_Stock { get; set; } = 0m;

        public int IdUbicacion { get; set; }

        public string? UMBas { get; set; } = null;

        public string Lic_Plate { get; set; } = "";
        public string Lote { get; set; } = "";

        public clsBeProducto Producto { get; set; } = new clsBeProducto();
        public clsBeProducto_presentacion Presentacion { get; set; } = new clsBeProducto_presentacion();
        public clsBeProducto_estado ProductoEstado { get; set; } = new clsBeProducto_estado();
        public clsBeUnidad_medida UnidadMedida { get; set; } = new clsBeUnidad_medida();

        public List<clsBeTrans_picking_det_parametros> ListaDetalleParametro { get; set; } = new List<clsBeTrans_picking_det_parametros>();
        public string Bono { get; set; } = "";

        public clsBeTrans_picking_det_3pl() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

}
