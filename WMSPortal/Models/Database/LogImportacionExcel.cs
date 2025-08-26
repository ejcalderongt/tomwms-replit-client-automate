using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class LogImportacionExcel
    {
        public int IdImportacion { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdBodega { get; set; }
        public int? IdUsuario { get; set; }
        public string HashArchivo { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
