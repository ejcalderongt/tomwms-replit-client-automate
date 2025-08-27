using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Entity.Propietario
{
    public class clsBePropietarios : ICloneable
    {
        [Column("IdPropietario"), DisplayName("IdPropietario")]
        public int IdPropietario { get; set; } = 0;

        [Column("IdEmpresa"), DisplayName("IdEmpresa")]
        public int IdEmpresa { get; set; } = 0;

        [Column("IdTipoActualizacionCosto"), DisplayName("IdTipoActualizacionCosto")]
        public int IdTipoActualizacionCosto { get; set; } = 0;

        [Column("contacto"), DisplayName("Contacto")]
        public string Contacto { get; set; } = string.Empty;

        [Column("nombre_comercial"), DisplayName("Nombre Comercial")]
        public string Nombre_Comercial { get; set; } = string.Empty;

        [Column("imagen"), DisplayName("Imagen")]
        public byte[] Imagen { get; set; } = Array.Empty<byte>();

        [Column("telefono"), DisplayName("Teléfono")]
        public string Telefono { get; set; } = string.Empty;

        [Column("direccion"), DisplayName("Dirección")]
        public string Direccion { get; set; } = string.Empty;

        [Column("activo"), DisplayName("Activo")]
        public bool Activo { get; set; } = false;

        [Column("user_agr"), DisplayName("Usuario Agregado")]
        public string user_agr { get; set; } = string.Empty;

        [Column("fec_agr"), DisplayName("Fecha Agregado")]
        public DateTime fec_agr { get; set; } = DateTime.Now;

        [Column("user_mod"), DisplayName("Usuario Modificado")]
        public string user_mod { get; set; } = string.Empty;

        [Column("fec_mod"), DisplayName("Fecha Modificado")]
        public DateTime fec_mod { get; set; } = DateTime.Now;

        [Column("email"), DisplayName("Email")]
        public string email { get; set; } = string.Empty;

        [Column("actualiza_costo_oc"), DisplayName("Actualiza Costo OC")]
        public bool actualiza_costo_oc { get; set; } = false;

        [Column("color"), DisplayName("Color")]
        public int color { get; set; } = 0;

        [Column("codigo"), DisplayName("Código")]
        public string codigo { get; set; } = string.Empty;

        [Column("sistema"), DisplayName("Sistema")]
        public bool sistema { get; set; } = false;

        [Column("NIT"), DisplayName("NIT")]
        public string NIT { get; set; } = string.Empty;

        [Column("codigo_acceso"), DisplayName("Código Acceso")]
        public string codigo_acceso { get; set; } = string.Empty;

        [Column("clave_acceso"), DisplayName("Clave Acceso")]
        public string clave_acceso { get; set; } = string.Empty;

        [Column("IdBodegaAreaSAP"), DisplayName("Id Bodega Area SAP")]
        public int IdBodegaAreaSAP { get; set; } = 0;

        [Column("es_consolidador"), DisplayName("Es Consolidador")]
        public bool es_consolidador { get; set; } = false;

        public clsBePropietarios() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}