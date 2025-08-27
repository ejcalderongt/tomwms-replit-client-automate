using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwAjuste
    {
        public int Idajusteenc { get; set; }
        public int Idajustedet { get; set; }
        public DateTime? Fecha { get; set; }
        public string Referencia { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public int? IdPresentacion { get; set; }
        public string Umbas { get; set; }
        public int? IdBodegaErp { get; set; }
        public string CodigoBodega { get; set; }
        public string NombreBodega { get; set; }
        public double? CantidadOriginal { get; set; }
        public double? CantidadNueva { get; set; }
        public double? PesoNuevo { get; set; }
        public double? PesoOriginal { get; set; }
        public DateTime? FechaVenceNueva { get; set; }
        public DateTime? FechaVenceOriginal { get; set; }
        public string LoteOriginal { get; set; }
        public string LoteNuevo { get; set; }
        public string TipoAjuste { get; set; }
        public bool? ModificaCantidad { get; set; }
        public bool? Enviado { get; set; }
        public string MotivoAjuste { get; set; }
        public string Observacion { get; set; }
        public string CodigoAjuste { get; set; }
        public int? IdProductoFamilia { get; set; }
    }
}
