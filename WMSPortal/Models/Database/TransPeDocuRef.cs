using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransPeDocuRef
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
        public string Empresa { get; set; }
        public string Bodega { get; set; }
        public string Referencia { get; set; }
        public string CodigoCliente { get; set; }
    }
}
