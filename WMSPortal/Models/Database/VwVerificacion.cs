using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwVerificacion
    {
        public int IdPedidoEnc { get; set; }
        public int IdPedidoDet { get; set; }
        public int? IdProductoBodega { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaVence { get; set; }
        public string LicPlate { get; set; }
        public string NomUnidMed { get; set; }
        public string NombreProducto { get; set; }
        public string NomEstado { get; set; }
        public double? CantidadSolicitada { get; set; }
        public double? CantidadRecibida { get; set; }
        public double? CantidadVerificada { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedidaBasica { get; set; }
        public string Codigo { get; set; }
        public int? Ndias { get; set; }
        public double? Diferencia { get; set; }
        public int? IdPresentacionPicking { get; set; }
        public string NomPresentacion { get; set; }
        public int? IdProductoEstado { get; set; }
    }
}
