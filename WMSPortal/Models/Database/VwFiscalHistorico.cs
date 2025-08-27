using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwFiscalHistorico
    {
        public int? IdOrdenCompraEnc { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public int IdProveedor { get; set; }
        public int? IdProveedorBodega { get; set; }
        public int? IdTipoIngresoOc { get; set; }
        public string Regimen { get; set; }
        public string Cliente { get; set; }
        public string NumeroOrden { get; set; }
        public string CodigoPoliza { get; set; }
        public string MaterialName { get; set; }
        public DateTime? Fecha { get; set; }
        public string ShortName { get; set; }
        public string Licencia { get; set; }
        public double? Cantidad { get; set; }
        public double? Cif { get; set; }
        public double? Dai { get; set; }
        public double? Iva { get; set; }
        public double? TotalValor { get; set; }
    }
}
