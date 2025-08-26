using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransInvReconteo
    {
        public int Idinvreconteo { get; set; }
        public int Idreconteo { get; set; }
        public int Idinvciclico { get; set; }
        public int Idinventarioenc { get; set; }
        public int IdStock { get; set; }
        public int IdProductoBodega { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUbicacionAnterior { get; set; }
        public int IdUbicacion { get; set; }
        public bool? EsNuevo { get; set; }
        public DateTime? FechaVence { get; set; }
        public string Lote { get; set; }
        public double? CantidadAnterior { get; set; }
        public double Cantidad { get; set; }
        public double? PesoAnterior { get; set; }
        public double? Peso { get; set; }
        public string UserAgr { get; set; }
        public DateTime FecAgr { get; set; }
        public int? IdOperador { get; set; }
        public bool? EsPallet { get; set; }
        public string LicPlate { get; set; }

        public virtual ProductoBodega IdProductoBodegaNavigation { get; set; }
        public virtual TransInvCiclico IdinvciclicoNavigation { get; set; }
        public virtual TransInvEnc IdinventarioencNavigation { get; set; }
    }
}
