using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Entity.Propietario
{
    public class clsBePropietario_reglas_det : ICloneable
    {
        [Column("IdReglaPropietarioDet")]
        [DisplayName("IdReglaPropietarioDet")]
        public int IdReglaPropietarioDet { get; set; } = 0;

        [Column("IdReglaPropietarioEnc")]
        [DisplayName("IdReglaPropietarioEnc")]
        public int IdReglaPropietarioEnc { get; set; } = 0;

        [Column("IdDestinatarioPropietario")]
        [DisplayName("IdDestinatarioPropietario")]
        public int IdDestinatarioPropietario { get; set; } = 0;

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

        public clsBePropietario_reglas_det() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}