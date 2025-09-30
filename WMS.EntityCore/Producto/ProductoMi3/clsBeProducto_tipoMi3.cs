using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.EntityCore.Producto
{
    public class clsBeProducto_tipoMi3
    {

        [Column("IdPropietario")]
        [DisplayName("IdPropietario")]
        public int IdPropietario { get; set; } = 0;

        [Column("NombreTipoProducto")]
        [DisplayName("NombreTipoProducto")]
        public string NombreTipoProducto { get; set; } = "";

        [Column("codigo")]
        [DisplayName("codigo")]
        public string Codigo { get; set; } = "";

        [Column("Activo")]
        [DisplayName("Activo")]
        public bool Activo { get; set; } = false;

        public clsBeProducto_tipoMi3() { }

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}
