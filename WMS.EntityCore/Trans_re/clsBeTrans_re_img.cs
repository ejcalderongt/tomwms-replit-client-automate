using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Trans_re
{
    public class clsBeTrans_re_img : ICloneable
    {
        [Column("IdImagen")]
        [DisplayName("IdImagen")]
        public int IdImagen { get; set; } = 0;

        [Column("IdRecepcionEnc")]
        [DisplayName("IdRecepcionEnc")]
        public int IdRecepcionEnc { get; set; } = 0;

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
        public bool IsNew { get; set; } = true;

        public clsBeTrans_re_img() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}