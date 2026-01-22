using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeTrans_re_det_lote_num : ICloneable
    {
        [Column("IdLoteNum")]
        [DisplayName("IdLoteNum")]
        public int IdLoteNum { get; set; } = 0;

        [Column("IdRecepcionEnc")]
        [DisplayName("IdRecepcionEnc")]
        public int IdRecepcionEnc { get; set; } = 0;

        [Column("IdProductoBodega")]
        [DisplayName("IdProductoBodega")]
        public int IdProductoBodega { get; set; } = 0;

        [Column("Codigo")]
        [DisplayName("Codigo")]
        public string Codigo { get; set; } = "";

        [Column("Lote")]
        [DisplayName("Lote")]
        public string Lote { get; set; } = "";

        [Column("Lote_Numerico")]
        [DisplayName("Lote_Numerico")]
        public int Lote_Numerico { get; set; } = 0;

        [Column("FechaIngreso")]
        [DisplayName("FechaIngreso")]
        public DateTime FechaIngreso { get; set; } = DateTime.Now;

        [Column("Cantidad")]
        [DisplayName("Cantidad")]
        public double Cantidad { get; set; } = 0;

        public clsBeTrans_re_det_lote_num() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}