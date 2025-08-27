using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TipoActualizacionCosto
    {
        public TipoActualizacionCosto()
        {
            Propietarios = new HashSet<Propietario>();
        }

        public int IdTipoActualizacionCosto { get; set; }
        public string NombreActualizacionCosto { get; set; }

        public virtual ICollection<Propietario> Propietarios { get; set; }
    }
}
