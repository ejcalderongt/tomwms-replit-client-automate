using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Cliente
{
    public class clsBeClientes : ICloneable
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
        public string codigo { get; set; } = string.Empty;

        [Column("nombre_comercial")]
        [DisplayName("nombre_comercial")]
        public string nombre_comercial { get; set; } = string.Empty;

        [Column("nombre_contacto")]
        [DisplayName("nombre_contacto")]
        public string nombre_contacto { get; set; } = string.Empty;

        [Column("telefono")]
        [DisplayName("telefono")]
        public string telefono { get; set; } = string.Empty;

        [Column("nit")]
        [DisplayName("nit")]
        public string nit { get; set; } = string.Empty;

        [Column("direccion")]
        [DisplayName("direccion")]
        public string direccion { get; set; } = string.Empty;

        [Column("correo_electronico")]
        [DisplayName("correo_electronico")]
        public string correo_electronico { get; set; } = string.Empty;

        [Column("activo")]
        [DisplayName("activo")]
        public bool activo { get; set; } = false;

        [Column("realiza_manufactura")]
        [DisplayName("realiza_manufactura")]
        public bool realiza_manufactura { get; set; } = false;

        [Column("user_agr")]
        [DisplayName("user_agr")]
        public string user_agr { get; set; } = string.Empty;

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime fec_agr { get; set; } = DateTime.Now;

        [Column("user_mod")]
        [DisplayName("user_mod")]
        public string user_mod { get; set; } = string.Empty;

        [Column("fec_mod")]
        [DisplayName("fec_mod")]
        public DateTime fec_mod { get; set; } = DateTime.Now;

        [Column("despachar_lotes_completos")]
        [DisplayName("despachar_lotes_completos")]
        public bool despachar_lotes_completos { get; set; } = false;

        [Column("sistema")]
        [DisplayName("sistema")]
        public string sistema { get; set; } = string.Empty;

        [Column("es_bodega_recepcion")]
        [DisplayName("es_bodega_recepcion")]
        public bool es_bodega_recepcion { get; set; } = false;

        [Column("es_bodega_traslado")]
        [DisplayName("es_bodega_traslado")]
        public bool es_bodega_traslado { get; set; } = false;

        [Column("idubicacionvirtual")]
        [DisplayName("idubicacionvirtual")]
        public int idubicacionvirtual { get; set; } = 0;

        [Column("referencia")]
        [DisplayName("referencia")]
        public string referencia { get; set; } = string.Empty;

        [Column("control_ultimo_lote")]
        [DisplayName("control_ultimo_lote")]
        public bool control_ultimo_lote { get; set; } = false;

        [Column("control_calidad")]
        [DisplayName("control_calidad")]
        public bool control_calidad { get; set; } = false;

        [Column("IdUbicacionAbastecerCon")]
        [DisplayName("IdUbicacionAbastecerCon")]
        public int IdUbicacionAbastecerCon { get; set; } = 0;

        [Column("IdBodegaAreaSAP")]
        [DisplayName("IdBodegaAreaSAP")]
        public int IdBodegaAreaSAP { get; set; } = 0;

        [Column("es_proveedor")]
        [DisplayName("es_proveedor")]
        public bool es_proveedor { get; set; } = false;

        [Column("Codigo_Empresa_ERP")]
        [DisplayName("Codigo_Empresa_ERP")]
        public string Codigo_Empresa_ERP { get; set; } = string.Empty;


        public clsBeClientes() { }

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}