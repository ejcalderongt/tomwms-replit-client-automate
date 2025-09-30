using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.EntityCore.Dtos.Productos
{
    public class UnidadMedidaMi3Dto
    {

        public int IdPropietario { get; set; } = 0;
        public string Nombre { get; set; } = "";
        public string Codigo { get; set; } = "";
        public bool Activo { get; set; } = false;

    }
}
