using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwRecepcionCostoArancel
    {
        public DateTime? FechaRecepcion { get; set; }
        public string Proveedor { get; set; }
        public DateTime? HoraIniPc { get; set; }
        public DateTime? HoraFinPc { get; set; }
        public string NoMarchamo { get; set; }
        public string NoDocumento { get; set; }
        public string NoPoliza { get; set; }
        public int? NoLinea { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public double? Cantidad { get; set; }
        public double? CantidadRecibida { get; set; }
        public string Presentacion { get; set; }
        public string Estado { get; set; }
        public double? Costo { get; set; }
        public string Arancel { get; set; }
        public int IdOrdenCompraEnc { get; set; }
        public int IdRecepcionEnc { get; set; }
    }
}
