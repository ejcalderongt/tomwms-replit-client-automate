using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.EntityCore.Trans_re;
using WMSWebAPI.Be;

namespace WMS.EntityCore.Propietario
{
    public class clsBePropietario_reglas_enc : ICloneable
    {
        [Column("IdReglaPropietarioEnc")]
        [DisplayName("IdReglaPropietarioEnc")]
        public int IdReglaPropietarioEnc { get; set; } = 0;

        [Column("IdReglaRecepcion")]
        [DisplayName("IdReglaRecepcion")]
        public int IdReglaRecepcion { get; set; } = 0;

        [Column("IdPropietario")]
        [DisplayName("IdPropietario")]
        public int IdPropietario { get; set; } = 0;

        [Column("IdMensajeRegla")]
        [DisplayName("IdMensajeRegla")]
        public int IdMensajeRegla { get; set; } = 0;

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
        public bool IsNew { get; set; }
        public clsBeReglas_RecepcionRes Regla { get; set; } = new clsBeReglas_RecepcionRes();
        public string Propietario { get; set; } = "";
        public clsBeMensaje_regla Mensaje { get; set; } = new clsBeMensaje_regla();
        public List<clsBePropietario_reglas_det> ReglasDet { get; set; } = new List<clsBePropietario_reglas_det>();
        public clsBePropietario_reglas_enc() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}