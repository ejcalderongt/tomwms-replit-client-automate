using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.EntityCore.Cliente
{
    public class clsBeClientesMi3
    {
        
        [Column("IdPropietario")]
        [DisplayName("IdPropietario")]
        public int IdPropietario { get; set; } = 0;

        [Column("IdTipoCliente")]
        [DisplayName("IdTipoCliente")]
        public int IdTipoCliente { get; set; } = 0;

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

        [Column("es_proveedor")]
        [DisplayName("es_proveedor")]
        public bool es_proveedor { get; set; } = false;

        public clsBeClientesMi3() { }

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}
