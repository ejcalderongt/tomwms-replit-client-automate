using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwProductoBodegaParametro
    {
        public int IdProductoParametro { get; set; }
        public int IdParametro { get; set; }
        public int IdProducto { get; set; }
        public string ValorTexto { get; set; }
        public double? ValorNumerico { get; set; }
        public DateTime? ValorFecha { get; set; }
        public bool? ValorLogico { get; set; }
        public bool Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime FecMod { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public int Producto { get; set; }
        public int ProductoParametro { get; set; }
        public int IdProductoBodega { get; set; }
        public bool CapturarSiempre { get; set; }
    }
}
