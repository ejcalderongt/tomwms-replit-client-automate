using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeEmpresa_transporte_pilotos : ICloneable
    {
        [Column("IdPiloto")]
        [DisplayName("IdPiloto")]
        public int IdPiloto { get; set; } = 0;

        [Column("IdEmpresaTransporte")]
        [DisplayName("IdEmpresaTransporte")]
        public int IdEmpresaTransporte { get; set; } = 0;

        [Column("nombres")]
        [DisplayName("nombres")]
        public string Nombres { get; set; } = "";

        [Column("apellidos")]
        [DisplayName("apellidos")]
        public string Apellidos { get; set; } = "";

        [Column("telefono")]
        [DisplayName("telefono")]
        public string Telefono { get; set; } = "";

        [Column("correo_electronico")]
        [DisplayName("correo_electronico")]
        public string Correo_electronico { get; set; } = "";

        [Column("no_carnet")]
        [DisplayName("no_carnet")]
        public string No_carnet { get; set; } = "";

        [Column("fecha_expiracion_carnet")]
        [DisplayName("fecha_expiracion_carnet")]
        public DateTime Fecha_expiracion_carnet { get; set; } = DateTime.Now;

        [Column("no_dpi")]
        [DisplayName("no_dpi")]
        public string No_dpi { get; set; } = "";

        [Column("no_licencia")]
        [DisplayName("no_licencia")]
        public string No_licencia { get; set; } = "";

        [Column("fecha_expiracion_licencia")]
        [DisplayName("fecha_expiracion_licencia")]
        public DateTime Fecha_expiracion_licencia { get; set; } = DateTime.Now;

        [Column("codigo_barra")]
        [DisplayName("codigo_barra")]
        public string Codigo_barra { get; set; } = "";

        [Column("direccion")]
        [DisplayName("direccion")]
        public string Direccion { get; set; } = "";

        [Column("foto")]
        [DisplayName("foto")]
        public byte[] Foto { get; set; }= Array.Empty<byte>();

        [Column("fecha_nacimiento")]
        [DisplayName("fecha_nacimiento")]
        public DateTime Fecha_nacimiento { get; set; } = DateTime.Now;

        [Column("fecha_ingreso")]
        [DisplayName("fecha_ingreso")]
        public DateTime Fecha_ingreso { get; set; } = DateTime.Now;

        [Column("fecha_salida")]
        [DisplayName("fecha_salida")]
        public DateTime Fecha_salida { get; set; } = DateTime.Now;

        [Column("IdTipoLicencia")]
        [DisplayName("IdTipoLicencia")]
        public string IdTipoLicencia { get; set; } = "";

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
        public bool IsNew { get; set; } = true;

        public clsBeEmpresa_transporte_pilotos() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
