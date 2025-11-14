using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeEmpresa_transporte_vehiculos : ICloneable
    {
        [Column("IdVehiculo")]
        [DisplayName("IdVehiculo")]
        public int IdVehiculo { get; set; } = 0;

        [Column("IdEmpresaTransporte")]
        [DisplayName("IdEmpresaTransporte")]
        public int IdEmpresaTransporte { get; set; } = 0;

        [Column("IdTipoContenedor")]
        [DisplayName("IdTipoContenedor")]
        public int IdTipoContenedor { get; set; } = 0;

        [Column("placa")]
        [DisplayName("placa")]
        public string Placa { get; set; } = "";

        [Column("marca")]
        [DisplayName("marca")]
        public string Marca { get; set; } = "";

        [Column("modelo")]
        [DisplayName("modelo")]
        public string Modelo { get; set; } = "";

        [Column("peso")]
        [DisplayName("peso")]
        public double Peso { get; set; } = 0;

        [Column("volumen")]
        [DisplayName("volumen")]
        public double Volumen { get; set; } = 0;

        [Column("activo")]
        [DisplayName("activo")]
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

        [Column("tipo")]
        [DisplayName("tipo")]
        public string Tipo { get; set; } = "";

        [Column("alto")]
        [DisplayName("alto")]
        public double Alto { get; set; } = 0;

        [Column("largo")]
        [DisplayName("largo")]
        public double Largo { get; set; } = 0;

        [Column("ancho")]
        [DisplayName("ancho")]
        public double Ancho { get; set; } = 0;

        [Column("placa_comercial")]
        [DisplayName("placa_comercial")]
        public string Placa_comercial { get; set; } = "";

        [Column("es_contedor")]
        [DisplayName("es_contedor")]
        public int Es_contedor { get; set; } = 0;

        public clsBeEmpresa_transporte_vehiculos() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
