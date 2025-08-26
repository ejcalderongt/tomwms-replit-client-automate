using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwDespacho
    {
        public int Código { get; set; }
        public string Bodega { get; set; }
        public string Propietario { get; set; }
        public string Piloto { get; set; }
        public string Vehiculo { get; set; }
        public string Ruta { get; set; }
        public bool? Activo { get; set; }
        public DateTime? Fecha { get; set; }
        public int IdBodega { get; set; }
    }
}
