using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Reset_Password
{
    public class clsBeReset_portal_ux : ICloneable
    {
        [Column("IdReset")]
        [DisplayName("IdReset")]
        public int IdReset { get; set; } = 0;

        [Column("IdPropietario")]
        [DisplayName("IdPropietario")]
        public int IdPropietario { get; set; } = 0;

        [Column("Token")]
        [DisplayName("Token")]
        public string Token { get; set; } = "";

        [Column("Tiempo_Expira")]
        [DisplayName("Tiempo_Expira")]
        public DateTime Tiempo_Expira { get; set; } = DateTime.Now;

        [Column("Used")]
        [DisplayName("Used")]
        public bool Used { get; set; } = false;

        [Column("Fec_agr")]
        [DisplayName("Fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;

        public clsBeReset_portal_ux() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
