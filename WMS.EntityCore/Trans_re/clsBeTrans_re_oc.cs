using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.EntityCore.Trans_oc;

namespace WMS.EntityCore.Trans_re
{
    public class clsBeTrans_re_oc : ICloneable
    {
        [Column("IdRecepcionOc")]
        [DisplayName("IdRecepcionOc")]
        public int IdRecepcionOc { get; set; } = 0;
        [Column("IdRecepcionEnc")]
        [DisplayName("IdRecepcionEnc")]
        public int IdRecepcionEnc { get; set; } = 0;
        [Column("IdOrdenCompraEnc")]
        [DisplayName("IdOrdenCompraEnc")]
        public int IdOrdenCompraEnc { get; set; } = 0;
        [Column("recepcion_ciega")]
        [DisplayName("recepcion_ciega")]
        public bool Recepcion_ciega { get; set; } = false;
        [Column("recepcion_manual")]
        [DisplayName("recepcion_manual")]
        public bool Recepcion_manual { get; set; } = false;
        [Column("no_docto")]
        [DisplayName("no_docto")]
        public string No_docto { get; set; } = "";
        [Column("hora_ini_hh")]
        [DisplayName("hora_ini_hh")]
        public DateTime Hora_ini_hh { get; set; } = DateTime.Now;
        [Column("hora_fin_hh")]
        [DisplayName("hora_fin_hh")]
        public DateTime Hora_fin_hh { get; set; } = DateTime.Now;
        [Column("user_agr")]
        [DisplayName("user_agr")]
        public string User_agr { get; set; } = "";
        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        [Column("firma_operador")]
        [DisplayName("firma_operador")]
        public byte[] Firma_operador { get; set; } = Array.Empty<byte>();
        public bool IsNew { get; set; } = false;
        public clsBeTrans_oc_enc OC { get; set; } = new clsBeTrans_oc_enc();
        public clsBeTrans_re_oc() { }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
