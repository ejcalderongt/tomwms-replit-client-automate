using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Entity.Propietario
{
    public class clsBePropietario_destinatario : ICloneable
    {
        [Column("IdDestinatarioPropietario")]
        [DisplayName("IdDestinatarioPropietario")]
        public int IdDestinatarioPropietario { get; set; } = 0;

        [Column("IdPropietario")]
        [DisplayName("IdPropietario")]
        public int IdPropietario { get; set; } = 0;

        [Column("nombre")]
        [DisplayName("nombre")]
        public string Nombre { get; set; } = "";

        [Column("apellido")]
        [DisplayName("apellido")]
        public string Apellido { get; set; } = "";

        [Column("correo_electronico")]
        [DisplayName("correo_electronico")]
        public string Correo_electronico { get; set; } = "";

        [Column("telefono")]
        [DisplayName("telefono")]
        public string Telefono { get; set; } = "";

        [Column("telefono1")]
        [DisplayName("telefono1")]
        public string Telefono1 { get; set; } = "";

        [Column("cargo")]
        [DisplayName("cargo")]
        public string Cargo { get; set; } = "";

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        public clsBePropietario_destinatario() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}