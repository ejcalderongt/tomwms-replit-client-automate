using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwParametro
    {
        public int IdParametro { get; set; }
        public string Tipo { get; set; }
        public string Descripción { get; set; }
        public string ValorTexto { get; set; }
        public double? ValorNúmerico { get; set; }
        public DateTime? ValorFecha { get; set; }
        public bool? ValorLógico { get; set; }
        public bool? Activo { get; set; }
    }
}
