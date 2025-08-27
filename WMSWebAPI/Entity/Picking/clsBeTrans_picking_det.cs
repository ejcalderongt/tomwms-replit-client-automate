using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeTrans_picking_det : ICloneable
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

        [Column("nombre")]
        [DisplayName("nombre")]
        public string Nombre { get; set; } = "";

        public clsBeTrans_picking_det() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
