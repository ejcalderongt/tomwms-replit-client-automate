using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransPePol
    {
        public int IdOrdenPedidoPol { get; set; }
        public int IdOrdenPedidoEnc { get; set; }
        public string BlNo { get; set; }
        public string NoPoliza { get; set; }
        public string PtoDescarga { get; set; }
        public string ViajeNo { get; set; }
        public string BuqueNo { get; set; }
        public string Remitente { get; set; }
        public DateTime? FechaAbordaje { get; set; }
        public string Destino { get; set; }
        public string DirDestino { get; set; }
        public string Descripcion { get; set; }
        public string PoNumber { get; set; }
        public int? Cantidad { get; set; }
        public int? Piezas { get; set; }
        public double? TotalKgs { get; set; }
        public double? Cbm { get; set; }
        public string Dua { get; set; }
        public DateTime? FechaPoliza { get; set; }
        public string PaisProcede { get; set; }
        public double? TipoCambio { get; set; }
        public double? TotalValoraduana { get; set; }
        public int? TotalLineas { get; set; }
        public int? TotalBultos { get; set; }
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
        public string CodigoPoliza { get; set; }
        public string Ticket { get; set; }
        public string NumeroOrden { get; set; }
        public DateTime? FechaAceptacion { get; set; }
        public DateTime? FechaLlegada { get; set; }
        public double? TotalOtros { get; set; }
        public int? IdRegimen { get; set; }
    }
}
