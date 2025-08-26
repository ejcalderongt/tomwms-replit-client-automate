using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TmsTicketPol
    {
        public int IdTicket { get; set; }
        public int IdOrdenTmsEnc { get; set; }
        public string NoPoliza { get; set; }
        public string Dua { get; set; }
        public DateTime? FechaPoliza { get; set; }
        public string PaisProcede { get; set; }
        public double? TipoCambio { get; set; }
        public double? TotalValoraduana { get; set; }
        public double? TotalBultosPeso { get; set; }
        public double? TotalUsd { get; set; }
        public double? TotalFlete { get; set; }
        public double? TotalSeguro { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public string ClaveAduana { get; set; }
        public string NitImpExp { get; set; }
        public string Clase { get; set; }
        public string ModTransporte { get; set; }
        public double? TotalLiquidar { get; set; }
        public double? TotalGeneral { get; set; }
        public int? IdRegimen { get; set; }
        public string CodigoBarra { get; set; }
    }
}
