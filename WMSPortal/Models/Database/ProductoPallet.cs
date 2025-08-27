using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ProductoPallet
    {
        public int IdPallet { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int IdProductoBodega { get; set; }
        public int IdPresentacion { get; set; }
        public int? IdOperadorBodega { get; set; }
        public int? IdImpresora { get; set; }
        public int IdRecepcionEnc { get; set; }
        public string CodigoBarra { get; set; }
        public double? Cantidad { get; set; }
        public string Lote { get; set; }
        public bool? Impreso { get; set; }
        public int? Reimpresiones { get; set; }
        public DateTime? FechaVence { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
        public int IdRecepcionDet { get; set; }
        public string CodigoProducto { get; set; }

        public virtual Impresora IdImpresoraNavigation { get; set; }
        public virtual OperadorBodega IdOperadorBodegaNavigation { get; set; }
        public virtual ProductoPresentacion IdPresentacionNavigation { get; set; }
        public virtual ProductoBodega IdProductoBodegaNavigation { get; set; }
        public virtual PropietarioBodega IdPropietarioBodegaNavigation { get; set; }
        public virtual TransReDet IdRecepcion { get; set; }
        public virtual TransReEnc IdRecepcionEncNavigation { get; set; }
    }
}
