using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Entity.Propietario
{
    public class clsBePropietarios : ICloneable
    {
        [Column("IdPropietario")]
        [DisplayName("IdPropietario")]
        public int IdPropietario { get; set; } = 0;

        [Column("IdEmpresa")]
        [DisplayName("IdEmpresa")]
        public int IdEmpresa { get; set; } = 0;

        [Column("IdTipoActualizacionCosto")]
        [DisplayName("IdTipoActualizacionCosto")]
        public int IdTipoActualizacionCosto { get; set; } = 0;

        [Column("contacto")]
        [DisplayName("contacto")]
        public string Contacto { get; set; } = "";

        [Column("nombre_comercial")]
        [DisplayName("nombre_comercial")]
        public string Nombre_comercial { get; set; } = "";

        [Column("imagen")]
        [DisplayName("imagen")]
        public byte[] Imagen { get; set; }= Array.Empty<byte>();

        [Column("telefono")]
        [DisplayName("telefono")]
        public string Telefono { get; set; } = "";

        [Column("direccion")]
        [DisplayName("direccion")]
        public string Direccion { get; set; } = "";

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

        [Column("email")]
        [DisplayName("email")]
        public string Email { get; set; } = "";

        [Column("actualiza_costo_oc")]
        [DisplayName("actualiza_costo_oc")]
        public bool Actualiza_costo_oc { get; set; } = false;

        [Column("color")]
        [DisplayName("color")]
        public int Color { get; set; } = 0;

        [Column("codigo")]
        [DisplayName("codigo")]
        public string Codigo { get; set; } = "";

        [Column("sistema")]
        [DisplayName("sistema")]
        public bool Sistema { get; set; } = false;

        [Column("NIT")]
        [DisplayName("NIT")]
        public string NIT { get; set; } = "";

        [Column("codigo_acceso")]
        [DisplayName("codigo_acceso")]
        public string Codigo_acceso { get; set; } = "";

        [Column("clave_acceso")]
        [DisplayName("clave_acceso")]
        public string Clave_acceso { get; set; } = "";

        [Column("IdBodegaAreaSAP")]
        [DisplayName("IdBodegaAreaSAP")]
        public int IdBodegaAreaSAP { get; set; } = 0;

        [Column("es_consolidador")]
        [DisplayName("es_consolidador")]
        public bool Es_consolidador { get; set; } = false;

        public clsBePropietarios() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}