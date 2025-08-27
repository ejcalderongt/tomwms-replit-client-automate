using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Cbcatalogoproducto
    {
        public byte CodEmpresa { get; set; }
        public string Codigoproducto { get; set; }
        public string Descripcion { get; set; }
        public string Nemonico { get; set; }
        public int CodigoRubro { get; set; }
        public bool Movimiento { get; set; }
        public string Estado { get; set; }
        public string CodCentro { get; set; }
        public string CodCuentaxcobrar { get; set; }
        public string CodCuentaproducto { get; set; }
        public string Usuario { get; set; }
        public DateTime Fechamov { get; set; }
        public int Control { get; set; }
        public int Correlativo { get; set; }
        public string CodCuentapasivodiferido { get; set; }
        public string CodCuentaDifCxc { get; set; }
        public string CodCuentaDifPasdif { get; set; }
        public string CodCuentaxcobrarMe { get; set; }
        public string CodCuentapasivodiferidoMe { get; set; }
        public int? CorreCbcesantes { get; set; }
        public decimal? Montominimo { get; set; }
    }
}
