using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.EntityCore.Dtos.Catalogos
{
    public class ProductoMarcaSimpleDto
    {
        public int IdPropietario { get; set; }
        public string? Nombre { get; set; }
        public string? Codigo { get; set; }
        public bool? Activo { get; set; }
    }
}
