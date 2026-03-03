using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeCentro_costo : ICloneable
    {
        [Column("IdCentroCosto")]
        [DisplayName("IdCentroCosto")]
        public int IdCentroCosto { get; set; } = 0;

        [Column("IdEmpresa")]
        [DisplayName("IdEmpresa")]
        public int IdEmpresa { get; set; } = 0;

        [Column("Codigo")]
        [DisplayName("Codigo")]
        public string Codigo { get; set; } = "";

        [Column("Nombre")]
        [DisplayName("Nombre")]
        public string Nombre { get; set; } = "";

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

        [Column("referencia")]
        [DisplayName("referencia")]
        public string Referencia { get; set; } = "";

        [Column("control_inventario")]
        [DisplayName("control_inventario")]
        public bool Control_inventario { get; set; } = false;

        public clsBeCentro_costo() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}