using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwOrdenCompra
    {
        public int Código { get; set; }
        public string Bodega { get; set; }
        public string Propietario { get; set; }
        public string Proveedor { get; set; }
        public string TipoIngreso { get; set; }
        public string Estado { get; set; }
        public string NoDocumento { get; set; }
        public string Referencia { get; set; }
        public string Procedencia { get; set; }
        public int? IdBodega { get; set; }
        public int? IdPropietario { get; set; }
        public bool? Activo { get; set; }
        public int IdPropietarioBodega { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? EsDevolucion { get; set; }
        public bool? EnviadoAErp { get; set; }
    }
}
