using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwClienteDireccion
    {
        public int IdCliente { get; set; }
        public int IdDireccion { get; set; }
        public int? IdRegion { get; set; }
        public string NombreRegion { get; set; }
        public int? IdMunicipio { get; set; }
        public string NombreMunicipio { get; set; }
        public string Avenida { get; set; }
        public string Calle { get; set; }
        public string NoCasa { get; set; }
        public string Zona { get; set; }
        public string Direccion { get; set; }
        public string Referencia { get; set; }
        public string CoordenadaY { get; set; }
        public string CoordenadaX { get; set; }
        public bool? Local { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool Activo { get; set; }
    }
}
