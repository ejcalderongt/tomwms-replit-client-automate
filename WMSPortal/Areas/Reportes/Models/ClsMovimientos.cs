using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WMSPortal.Areas.Reportes.Models
{
    public class ClsMovimientos
    {
        [Required(ErrorMessage = "El campo fecha del es requerido")]
        public DateTime pFechaDel { get; set; }

        [Required(ErrorMessage = "El campo fecha al es requerido")]
        public DateTime pFechaAl { get; set; }
    }
}
