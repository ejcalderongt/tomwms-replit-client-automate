using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Despacho
{
    public class clsBeTrans_despacho_det : ICloneable
    {
        [Column("IdDespachoDet")]
        [DisplayName("IdDespachoDet")]
        public int IdDespachoDet { get; set; } = 0;

        [Column("IdDespachoEnc")]
        [DisplayName("IdDespachoEnc")]
        public int IdDespachoEnc { get; set; } = 0;

        [Column("IdPickingUbic")]
        [DisplayName("IdPickingUbic")]
        public int IdPickingUbic { get; set; } = 0;

        [Column("Fecha")]
        [DisplayName("Fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;

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

        [Column("IdPedidoEnc")]
        [DisplayName("IdPedidoEnc")]
        public int IdPedidoEnc { get; set; } = 0;

        [Column("IdPedidoDet")]
        [DisplayName("IdPedidoDet")]
        public int IdPedidoDet { get; set; } = 0;

        [Column("IdProductoBodega")]
        [DisplayName("IdProductoBodega")]
        public int IdProductoBodega { get; set; } = 0;

        [Column("IdUnidadMedidaBasica")]
        [DisplayName("IdUnidadMedidaBasica")]
        public int IdUnidadMedidaBasica { get; set; } = 0;

        [Column("IdPresentacion")]
        [DisplayName("IdPresentacion")]
        public int IdPresentacion { get; set; } = 0;

        [Column("Codigo")]
        [DisplayName("Codigo")]
        public string Codigo { get; set; } = "";

        [Column("NombreProducto")]
        [DisplayName("NombreProducto")]
        public string NombreProducto { get; set; } = "";

        [Column("NombreEstado")]
        [DisplayName("NombreEstado")]
        public string NombreEstado { get; set; } = "";

        [Column("CantidadDespachada")]
        [DisplayName("CantidadDespachada")]
        public double CantidadDespachada { get; set; } = 0;

        [Column("PesoDespachado")]
        [DisplayName("PesoDespachado")]
        public double PesoDespachado { get; set; } = 0;

        [Column("IdProductoEstado")]
        [DisplayName("IdProductoEstado")]
        public int IdProductoEstado { get; set; } = 0;

        public string Lote { get; set; } = string.Empty;
        public string Lic_plate { get; set; } = string.Empty;
        public string ProductoPresentacion { get; set; } = string.Empty;

        public clsBeTrans_despacho_det() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
