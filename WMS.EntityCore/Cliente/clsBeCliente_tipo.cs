using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeCliente_tipo : ICloneable
    {
        [Column("IdTipoCliente")]
        [DisplayName("IdTipoCliente")]
        public int IdTipoCliente { get; set; } = 0;

        [Column("IdPropietario")]
        [DisplayName("IdPropietario")]
        public int IdPropietario { get; set; } = 0;

        [Column("NombreTipoCliente")]
        [DisplayName("NombreTipoCliente")]
        public string NombreTipoCliente { get; set; } = "";

        [Column("Activo")]
        [DisplayName("Activo")]
        public bool Activo { get; set; } = false;

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

        public clsBeCliente_tipo() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
