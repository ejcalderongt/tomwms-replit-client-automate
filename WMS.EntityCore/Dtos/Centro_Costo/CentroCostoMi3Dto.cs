using System.ComponentModel.DataAnnotations;

namespace WMS.EntityCore.Dtos.Centro_Costo
{
    public class CentroCostoMi3Dto
    {
        [Required(ErrorMessage = "El código es requerido")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;

        public string? Referencia { get; set; }

        public bool ControlInventario { get; set; }

        // Campos que podrían venir de tu interfaz MI3
        public string? IdPropietario { get; set; }
    }
}
