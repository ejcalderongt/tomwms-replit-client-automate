using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Consolidador
    {
        public int Idconsolidador { get; set; }
        public string Codigo { get; set; }
        public int IdEmpresa { get; set; }
        public string NomComercial { get; set; }
        public string RazonSocial { get; set; }
        public string Nit { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
    }
}
