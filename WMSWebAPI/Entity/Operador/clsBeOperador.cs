using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeOperador : ICloneable
    {
        [Column("IdOperador")]
        [DisplayName("IdOperador")]
        public int IdOperador { get; set; } = 0;

        [Column("IdEmpresa")]
        [DisplayName("IdEmpresa")]
        public int IdEmpresa { get; set; } = 0;

        [Column("IdRolOperador")]
        [DisplayName("IdRolOperador")]
        public int IdRolOperador { get; set; } = 0;

        [Column("IdJornada")]
        [DisplayName("IdJornada")]
        public int IdJornada { get; set; } = 0;

        [Column("nombres")]
        [DisplayName("nombres")]
        public string Nombres { get; set; } = "";

        [Column("apellidos")]
        [DisplayName("apellidos")]
        public string Apellidos { get; set; } = "";

        [Column("direccion")]
        [DisplayName("direccion")]
        public string Direccion { get; set; } = "";

        [Column("telefono")]
        [DisplayName("telefono")]
        public string Telefono { get; set; } = "";

        [Column("codigo")]
        [DisplayName("codigo")]
        public string Codigo { get; set; } = "";

        [Column("clave")]
        [DisplayName("clave")]
        public string Clave { get; set; } = "";

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

        [Column("costo_hora")]
        [DisplayName("costo_hora")]
        public double Costo_hora { get; set; } = 0;

        [Column("usa_hh")]
        [DisplayName("usa_hh")]
        public bool Usa_hh { get; set; } = false;

        [Column("foto")]
        [DisplayName("foto")]        
        public byte[] Foto { get; set; } = Array.Empty<byte>();

        [Column("recibe")]
        [DisplayName("recibe")]
        public bool Recibe { get; set; } = false;

        [Column("ubica")]
        [DisplayName("ubica")]
        public bool Ubica { get; set; } = false;

        [Column("transporta")]
        [DisplayName("transporta")]
        public bool Transporta { get; set; } = false;

        [Column("pickea")]
        [DisplayName("pickea")]
        public bool Pickea { get; set; } = false;

        [Column("verifica")]
        [DisplayName("verifica")]
        public bool Verifica { get; set; } = false;

        [Column("montacarga")]
        [DisplayName("montacarga")]
        public bool Montacarga { get; set; } = false;

        [Column("sistema")]
        [DisplayName("sistema")]
        public bool Sistema { get; set; } = false;

        public clsBeOperador() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
