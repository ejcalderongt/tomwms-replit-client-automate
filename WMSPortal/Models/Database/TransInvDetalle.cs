using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransInvDetalle
    {
        public int Idinventariodet { get; set; }
        public int Idinventarioenc { get; set; }
        public int? Idtramo { get; set; }
        public int IdUbicacion { get; set; }
        public int? Idoperador { get; set; }
        public int? Idproducto { get; set; }
        public int? IdPresentacion { get; set; }
        public int? Idunidadmedida { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaVence { get; set; }
        public string Serie { get; set; }
        public string Idproductoestado { get; set; }
        public double? Cantidad { get; set; }
        public DateTime? FechaCaptura { get; set; }
        public string Host { get; set; }
        public string NomProducto { get; set; }
        public string NomOperador { get; set; }
        public int? Carga { get; set; }
        public double? Peso { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public string NombrePropietario { get; set; }

        public virtual TransInvEnc IdinventarioencNavigation { get; set; }
    }
}
