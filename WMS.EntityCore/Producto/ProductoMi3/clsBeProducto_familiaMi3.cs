using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.EntityCore.Producto.ProductoSimple
{
    public class clsBeProducto_familiaSimple : ICloneable
    {
        [Column("IdPropietario")]
        [DisplayName("IdPropietario")]
        public int IdPropietario { get; set; } = 0;

        [Column("codigo")]
        [DisplayName("codigo")]
        public string Codigo { get; set; } = "";

        [Column("nombre")]
        [DisplayName("nombre")]
        public string Nombre { get; set; } = "";

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        public clsBeProducto_familiaSimple() { }

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}
