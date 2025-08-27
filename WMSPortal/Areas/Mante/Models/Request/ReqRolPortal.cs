using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WMSPortal.Areas.Mante.Models.Request
{
    public class ReqRolPortal
    {
        [Required(ErrorMessage = "El campo nombre es oblogatorui")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
