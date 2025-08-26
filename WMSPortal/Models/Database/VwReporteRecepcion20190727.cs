using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwReporteRecepcion20190727
    {
        public int IdRecepcionEnc { get; set; }
        public int IdRecepcionDet { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public string Propietario { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public DateTime? HoraIniPc { get; set; }
        public DateTime? HoraFinPc { get; set; }
        public string TipoTrans { get; set; }
        public int? NoLinea { get; set; }
        public string Codigo { get; set; }
        public string CodigoBarra { get; set; }
        public string Producto { get; set; }
        public double? CantidadRecibida { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaVence { get; set; }
        public string EstadoProducto { get; set; }
        public string Presentacion { get; set; }
        public string EstadoRec { get; set; }
        public string UnidadMedida { get; set; }
        public double? Peso { get; set; }
        public string AtributoVariante { get; set; }
    }
}
