using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwFiscalCtasOrden
    {
        public int? IdTipoDocumento { get; set; }
        public string TipoTransaccion { get; set; }
        public int? Idordencompra { get; set; }
        public int? Idempresa { get; set; }
        public int? Idpropietario { get; set; }
        public string NombreComercial { get; set; }
        public int? Idpropietariobodega { get; set; }
        public string UnidadMedida { get; set; }
        public double? CantidadPresentacion { get; set; }
        public double? Cantidad { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public string CodigoProducto { get; set; }
        public string CodigoPoliza { get; set; }
        public string CodigoBarra { get; set; }
        public int ValorDai { get; set; }
        public int ValorIva { get; set; }
        public int ValorAduana { get; set; }
    }
}
