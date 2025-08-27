using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeUnidad_medida : ICloneable
    {
        [Column("IdUnidadMedida")]
        [DisplayName("IdUnidadMedida")]
        public int IdUnidadMedida { get; set; } = 0;

        [Column("IdPropietario")]
        [DisplayName("IdPropietario")]
        public int IdPropietario { get; set; } = 0;

        [Column("Nombre")]
        [DisplayName("Nombre")]
        public string Nombre { get; set; } = "";

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;

        [Column("user_mod")]
        [DisplayName("user_mod")]
        public string User_mod { get; set; } = "";

        [Column("fec_mod")]
        [DisplayName("fec_mod")]
        public DateTime Fec_mod { get; set; } = DateTime.Now;

        [Column("user_agr")]
        [DisplayName("user_agr")]
        public string User_agr { get; set; } = "";

        [Column("codigo")]
        [DisplayName("codigo")]
        public string Codigo { get; set; } = "";

        [Column("es_um_cobro")]
        [DisplayName("es_um_cobro")]
        public bool Es_um_cobro { get; set; } = false;

        [Column("factor")]
        [DisplayName("factor")]
        public double Factor { get; set; } = 0;

        public clsBeUnidad_medida() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}