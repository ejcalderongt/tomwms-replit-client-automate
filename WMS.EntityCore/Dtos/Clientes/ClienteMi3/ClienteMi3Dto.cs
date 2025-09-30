using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.EntityCore.Dtos.Clientes
{
    public class ClienteMi3Dto
    {
        public int IdPropietario { get; set; } = 0;
        public int IdTipoCliente { get; set; } = 0;
        public string codigo { get; set; } = string.Empty;
        public string nombre_comercial { get; set; } = string.Empty;
        public string nombre_contacto { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public string nit { get; set; } = string.Empty;
        public string direccion { get; set; } = string.Empty;
        public string correo_electronico { get; set; } = string.Empty;
        public bool activo { get; set; } = false;
        public bool es_bodega_recepcion { get; set; } = false;
        public bool es_bodega_traslado { get; set; } = false;
        public int idubicacionvirtual { get; set; } = 0;
        public string referencia { get; set; } = string.Empty;
        public bool control_ultimo_lote { get; set; } = false;
        public bool control_calidad { get; set; } = false;
        public int IdUbicacionAbastecerCon { get; set; } = 0;
        public bool es_proveedor { get; set; } = false;

    }
}
