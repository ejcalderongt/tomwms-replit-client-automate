using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransInvCiclico
    {
        public TransInvCiclico()
        {
            TransInvReconteos = new HashSet<TransInvReconteo>();
        }

        public int Idinvciclico { get; set; }
        public int Idinventarioenc { get; set; }
        public int IdStock { get; set; }
        public int IdProductoBodega { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdPresentacion { get; set; }
        public int IdUbicacion { get; set; }
        public bool? EsNuevo { get; set; }
        public string LoteStock { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaVenceStock { get; set; }
        public DateTime? FechaVence { get; set; }
        public double? CantStock { get; set; }
        public double Cantidad { get; set; }
        public double? CantReconteo { get; set; }
        public double? PesoStock { get; set; }
        public double? Peso { get; set; }
        public double? PesoReconteo { get; set; }
        public int? Idoperador { get; set; }
        public string UserAgr { get; set; }
        public DateTime FecAgr { get; set; }
        public int? IdProductoEstNuevo { get; set; }
        public int? IdPresentacionNuevo { get; set; }
        public int? IdUbicacionNuevo { get; set; }
        public bool? EsPallet { get; set; }
        public string LicPlate { get; set; }
        public int? IdUnidadMedida { get; set; }

        public virtual ProductoBodega IdProductoBodegaNavigation { get; set; }
        public virtual TransInvEnc IdinventarioencNavigation { get; set; }
        public virtual ICollection<TransInvReconteo> TransInvReconteos { get; set; }
    }
}
