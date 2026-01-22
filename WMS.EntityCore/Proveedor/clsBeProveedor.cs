using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Proveedor
{
    public class clsBeProveedor : ICloneable
    {
        [Column("IdEmpresa")]
        [DisplayName("IdEmpresa")]
        public int IdEmpresa { get; set; } = 0;

        [Column("IdPropietario")]
        [DisplayName("IdPropietario")]
        public int IdPropietario { get; set; } = 0;

        [Column("IdProveedor")]
        [DisplayName("IdProveedor")]
        public int IdProveedor { get; set; } = 0;

        [Column("codigo")]
        [DisplayName("codigo")]
        public string Codigo { get; set; } = "";

        [Column("nombre")]
        [DisplayName("nombre")]
        public string Nombre { get; set; } = "";

        [Column("telefono")]
        [DisplayName("telefono")]
        public string Telefono { get; set; } = "";

        [Column("nit")]
        [DisplayName("nit")]
        public string Nit { get; set; } = "";

        [Column("direccion")]
        [DisplayName("direccion")]
        public string Direccion { get; set; } = "";

        [Column("email")]
        [DisplayName("email")]
        public string Email { get; set; } = "";

        [Column("contacto")]
        [DisplayName("contacto")]
        public string Contacto { get; set; } = "";

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("muestra_precio")]
        [DisplayName("muestra_precio")]
        public bool Muestra_precio { get; set; } = false;

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

        [Column("actualiza_costo_oc")]
        [DisplayName("actualiza_costo_oc")]
        public bool Actualiza_costo_oc { get; set; } = false;

        [Column("idubicacionvirtual")]
        [DisplayName("idubicacionvirtual")]
        public int Idubicacionvirtual { get; set; } = 0;

        [Column("es_bodega_recepcion")]
        [DisplayName("es_bodega_recepcion")]
        public bool Es_bodega_recepcion { get; set; } = false;

        [Column("es_bodega_traslado")]
        [DisplayName("es_bodega_traslado")]
        public bool Es_bodega_traslado { get; set; } = false;

        [Column("referencia")]
        [DisplayName("referencia")]
        public string Referencia { get; set; } = "";

        [Column("sistema")]
        [DisplayName("sistema")]
        public bool Sistema { get; set; } = false;

        [Column("IdConfiguracionBarraPallet")]
        [DisplayName("IdConfiguracionBarraPallet")]
        public int IdConfiguracionBarraPallet { get; set; } = 0;

        [Column("es_proveedor_servicio")]
        [DisplayName("es_proveedor_servicio")]
        public bool Es_proveedor_servicio { get; set; } = false;

        [Column("IdBodegaAreaSAP")]
        [DisplayName("IdBodegaAreaSAP")]
        public int IdBodegaAreaSAP { get; set; } = 0;

        [Column("IdPais")]
        [DisplayName("IdPais")]
        public int IdPais { get; set; } = 0;

        [Column("Codigo_Empresa_ERP")]
        [DisplayName("Codigo_Empresa_ERP")]
        public string Codigo_Empresa_ERP { get; set; } = "";
        public bool IsNew { get; set; } = true;
        public List<clsBeProveedor_tiempos>? TiemposProveedor { get; set; } = new List<clsBeProveedor_tiempos>();
        public clsBeProveedor() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
