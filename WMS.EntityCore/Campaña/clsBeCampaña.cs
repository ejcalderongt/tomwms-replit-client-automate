using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeCampaña : ICloneable
    {
        [Column("IdCampaña")]
        [DisplayName("IdCampaña")]
        public int IdCampaña { get; set; } = 0;

        [Column("Nombre")]
        [DisplayName("Nombre")]
        public string Nombre { get; set; } = "";

        [Column("FechaInicio")]
        [DisplayName("FechaInicio")]
        public DateTime FechaInicio { get; set; } = DateTime.Now;

        [Column("FechaFin")]
        [DisplayName("FechaFin")]
        public DateTime FechaFin { get; set; } = DateTime.Now;

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;

        [Column("user_agr")]
        [DisplayName("user_agr")]
        public string User_agr { get; set; } = "";

        [Column("fec_mod")]
        [DisplayName("fec_mod")]
        public DateTime Fec_mod { get; set; } = DateTime.Now;

        [Column("user_mod")]
        [DisplayName("user_mod")]
        public string User_mod { get; set; } = "";

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("codigo")]
        [DisplayName("codigo")]
        public string Codigo { get; set; } = "";

        public clsBeCampaña() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
