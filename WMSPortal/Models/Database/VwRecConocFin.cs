using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwRecConocFin
    {
        public int IdOrdenCompraEnc { get; set; }
        public int IdProductoBodega { get; set; }
        public string Codigo { get; set; }
        public string Producto { get; set; }
        public double? Cantidad { get; set; }
        public double? CantidadRecibida { get; set; }
        public string UnidadMedida { get; set; }
        public string Presentacion { get; set; }
        public int? IdRecepcionEnc { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public DateTime? HoraIniPc { get; set; }
        public DateTime? HoraFinPc { get; set; }
        public string EstadoRec { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaVence { get; set; }
        public double? Peso { get; set; }
        public string Proveedor { get; set; }
        public int IdProveedorBodega { get; set; }
        public int IdProveedor { get; set; }
        public string TipoTrans { get; set; }
        public string NoDocumento { get; set; }
        public int? NoLinea { get; set; }
        public string EstadoProducto { get; set; }
    }
}
