using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Log
{
    public class clsBeLog_portal_ux: ICloneable
    {

        [Column("LogUxId")]
        [DisplayName("LogUxId")]
        public int LogUxId { get; set; } = 0;

        [Column("Idpropietario")]
        [DisplayName("Idpropietario")]
        public int Idpropietario { get; set; } = 0;

        [Column("Usuario")]
        [DisplayName("Usuario")]
        public string Usuario { get; set; } = "";

        [Column("Email")]
        [DisplayName("Email")]
        public string Email { get; set; } = "";

        [Column("Fecha")]
        [DisplayName("Fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Column("IPAddress")]
        [DisplayName("IPAddress")]
        public string IPAddress { get; set; } = "";

        [Column("UserAgent")]
        [DisplayName("UserAgent")]
        public string UserAgent { get; set; } = "";

        [Column("Acceso")]
        [DisplayName("Acceso")]
        public bool Acceso { get; set; } = false;

        [Column("MensajeError")]
        [DisplayName("MensajeError")]
        public string MensajeError { get; set; } = "";

        [Column("UrlAcceso")]
        [DisplayName("UrlAcceso")]
        public string UrlAcceso { get; set; } = "";


        public clsBeLog_portal_ux() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
