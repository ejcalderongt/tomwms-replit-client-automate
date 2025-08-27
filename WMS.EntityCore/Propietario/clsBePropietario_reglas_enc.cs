using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

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

        public clsBePropietario_reglas_enc() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}