using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class LogErrorWm
    {
        public int IdError { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdBodega { get; set; }
        public DateTime? Fecha { get; set; }
        public string MensajeError { get; set; }
    }
}
