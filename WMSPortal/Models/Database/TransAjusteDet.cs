using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransAjusteDet
    {
        public int Idajustedet { get; set; }
        public int Idajusteenc { get; set; }
        public int? IdStock { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public int? IdProductoBodega { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? IdUbicacion { get; set; }
        public string LoteOriginal { get; set; }
        public string LoteNuevo { get; set; }
        public DateTime? FechaVenceOriginal { get; set; }
        public DateTime? FechaVenceNueva { get; set; }
        public double? PesoOriginal { get; set; }
        public double? PesoNuevo { get; set; }
        public double? CantidadOriginal { get; set; }
        public double? CantidadNueva { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public int? Idtipoajuste { get; set; }
        public int? Idmotivoajuste { get; set; }
        public string Observacion { get; set; }
        public string CodigoAjuste { get; set; }
        public bool? Enviado { get; set; }
        public int? IdBodegaErp { get; set; }

        public virtual TransAjusteEnc IdajusteencNavigation { get; set; }
    }
}
