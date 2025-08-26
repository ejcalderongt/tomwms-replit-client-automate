using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Trans_re
{
    public class clsBeTrans_re_tr : ICloneable
    {
        [Column("IdTipoTransaccion")]
        [DisplayName("IdTipoTransaccion")]
        public string IdTipoTransaccion { get; set; } = "";

        [Column("Descripcion")]
        [DisplayName("Descripcion")]
        public string Descripcion { get; set; } = "";

        [Column("Funcionalidad")]
        [DisplayName("Funcionalidad")]
        public string Funcionalidad { get; set; } = "";

        [Column("UsaHH")]
        [DisplayName("UsaHH")]
        public bool UsaHH { get; set; } = false;

        [Column("DescDev")]
        [DisplayName("DescDev")]
        public string DescDev { get; set; } = "";

        [Column("TipoTrans")]
        [DisplayName("TipoTrans")]
        public string TipoTrans { get; set; } = "";

        [Column("ConRef")]
        [DisplayName("ConRef")]
        public bool ConRef { get; set; } = false;

        [Column("Activo")]
        [DisplayName("Activo")]
        public bool Activo { get; set; } = false;

        public clsBeTrans_re_tr() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}