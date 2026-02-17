using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WMSWebAPI.Be;

namespace WMS.EntityCore.Cliente
{

    public class clsBeCliente_3pl : ICloneable
    {
        [Column("IdCliente")]
        [DisplayName("IdCliente")]
        public int IdCliente { get; set; } = 0;

        [Column("IdEmpresa")]
        [DisplayName("IdEmpresa")]
        public int IdEmpresa { get; set; } = 0;

        [Column("IdPropietario")]
        [DisplayName("IdPropietario")]
        public int IdPropietario { get; set; } = 0;

        [Column("IdTipoCliente")]
        [DisplayName("IdTipoCliente")]
        public int IdTipoCliente { get; set; } = 0;

        [Column("IdUbicacionManufactura")]
        [DisplayName("IdUbicacionManufactura")]
        public int IdUbicacionManufactura { get; set; } = 0;

        [Column("codigo")]
        [DisplayName("codigo")]
        public string Codigo { get; set; } = string.Empty;

        [Column("nombre_comercial")]
        [DisplayName("nombre_comercial")]
        public string Nombre_comercial { get; set; } = string.Empty;

        [Column("nombre_contacto")]
        [DisplayName("nombre_contacto")]
        public string Nombre_contacto { get; set; } = string.Empty;

        [Column("telefono")]
        [DisplayName("telefono")]
        public string Telefono { get; set; } = string.Empty;

        [Column("nit")]
        [DisplayName("nit")]
        public string Nit { get; set; } = string.Empty;

        [Column("direccion")]
        [DisplayName("direccion")]
        public string Direccion { get; set; } = string.Empty;

        [Column("correo_electronico")]
        [DisplayName("correo_electronico")]
        public string Correo_electronico { get; set; } = string.Empty;

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("realiza_manufactura")]
        [DisplayName("realiza_manufactura")]
        public bool Realiza_manufactura { get; set; } = false;

        [Column("user_agr")]
        [DisplayName("user_agr")]
        public string User_agr { get; set; } = string.Empty;

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;

        [Column("user_mod")]
        [DisplayName("user_mod")]
        public string User_mod { get; set; } = string.Empty;

        [Column("fec_mod")]
        [DisplayName("fec_mod")]
        public DateTime Fec_mod { get; set; } = DateTime.Now;

        [Column("despachar_lotes_completos")]
        [DisplayName("despachar_lotes_completos")]
        public bool Despachar_lotes_completos { get; set; } = false;

        [Column("sistema")]
        [DisplayName("sistema")]
        public bool Sistema { get; set; } = false;

        [Column("es_bodega_recepcion")]
        [DisplayName("es_bodega_recepcion")]
        public bool Es_bodega_recepcion { get; set; } = false;

        [Column("es_bodega_traslado")]
        [DisplayName("es_bodega_traslado")]
        public bool Es_bodega_traslado { get; set; } = false;

        [Column("idubicacionvirtual")]
        [DisplayName("idubicacionvirtual")]
        public int Idubicacionvirtual { get; set; } = 0;

        [Column("referencia")]
        [DisplayName("referencia")]
        public string Referencia { get; set; } = string.Empty;

        [Column("control_ultimo_lote")]
        [DisplayName("control_ultimo_lote")]
        public bool Control_ultimo_lote { get; set; } = false;

        [Column("control_calidad")]
        [DisplayName("control_calidad")]
        public bool Control_calidad { get; set; } = false;

        [Column("IdUbicacionAbastecerCon")]
        [DisplayName("IdUbicacionAbastecerCon")]
        public int IdUbicacionAbastecerCon { get; set; } = 0;

        [Column("IdBodegaAreaSAP")]
        [DisplayName("IdBodegaAreaSAP")]
        public int IdBodegaAreaSAP { get; set; } = 0;

        [Column("es_proveedor")]
        [DisplayName("es_proveedor")]
        public bool Es_proveedor { get; set; } = false;

        // Esta propiedad NO existe en clsBeCliente (extra del DTO). La dejo tal cual.
        [Column("Codigo_Empresa_ERP")]
        [DisplayName("Codigo_Empresa_ERP")]
        public string Codigo_Empresa_ERP { get; set; } = string.Empty;

        // Propiedad nueva (tipo clase). Si tu mapper/ORM intenta mapear todo a columnas, márcala como NotMapped/Ignore.
        public clsBeCliente_tipo TipoCliente { get; set; } = new clsBeCliente_tipo();

        public clsBeCliente_3pl() { }

        public object Clone() => MemberwiseClone();
    }


}