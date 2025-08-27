using System.ComponentModel.DataAnnotations;

namespace WMSPortal.Models
{
    public class ClsUsuario
    {
        [Required(ErrorMessage = "El campo empresa es requerido")]
        public int idEmpresa { get; set; }

        [Required(ErrorMessage = "El campo usuario es requerido")]
        public string usuario { get; set; }

        [Required(ErrorMessage = "El campo usuario es requerido")]
        public string contrasena { get; set; }

        public bool errores { get; set; } = false;

        public string mensaje { get; set; }
    }
}
