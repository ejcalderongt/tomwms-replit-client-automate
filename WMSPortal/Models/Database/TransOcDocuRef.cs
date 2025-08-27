using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransOcDocuRef
    {
        public int IdDocumentoRef { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaDocumento { get; set; }
        public DateTime? FechaAsignacion { get; set; }
        public DateTime? FechaAgregado { get; set; }
        public bool? Asignado { get; set; }
        public bool? Activo { get; set; }
    }
}
