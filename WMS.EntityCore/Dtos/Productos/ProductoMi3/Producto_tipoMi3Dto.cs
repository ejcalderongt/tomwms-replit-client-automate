using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.EntityCore.Dtos.Productos
{
    public class Producto_tipoMi3Dto
    {
        public int IdPropietario { get; set; } = 0;
        public string NombreTipoProducto { get; set; } = "";
        public string Codigo { get; set; } = "";
        public bool Activo { get; set; } = false;

    }
}
