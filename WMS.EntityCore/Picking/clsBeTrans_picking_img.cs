using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Picking
{
    public class clsBeTrans_picking_img : ICloneable
    {
        [Column("IdImagen")]
        [DisplayName("IdImagen")]
        public int IdImagen { get; set; } = 0;

        [Column("IdPickingEnc")]
        [DisplayName("IdPickingEnc")]
        public int IdPickingEnc { get; set; } = 0;

        [Column("IdPickingDet")]
        [DisplayName("IdPickingDet")]
        public int IdPickingDet { get; set; } = 0;

        [Column("IdPedidoEnc")]
        [DisplayName("IdPedidoEnc")]
        public int IdPedidoEnc { get; set; } = 0;

        [Column("IdPedidoDet")]
        [DisplayName("IdPedidoDet")]
        public int IdPedidoDet { get; set; } = 0;

        [Column("Imagen")]
        [DisplayName("Imagen")]
        public byte[] Imagen { get; set; } = Array.Empty<byte>();

        [Column("user_agr")]
        [DisplayName("user_agr")]
        public string User_agr { get; set; } = "";

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;

        [Column("observacion")]
        [DisplayName("observacion")]
        public string Observacion { get; set; } = "";

        public clsBeTrans_picking_img() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}