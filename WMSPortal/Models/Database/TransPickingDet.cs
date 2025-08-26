using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransPickingDet
    {
        public TransPickingDet()
        {
            TransPickingDetParametros = new HashSet<TransPickingDetParametro>();
            TransPickingUbics = new HashSet<TransPickingUbic>();
        }

        public int IdPickingDet { get; set; }
        public int IdPickingEnc { get; set; }
        public int? IdPedidoEnc { get; set; }
        public int IdPedidoDet { get; set; }
        public int? IdOperadorBodega { get; set; }
        public double? Cantidad { get; set; }
        public int? ClienteDias { get; set; }
        public double? CantidadRecibida { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        public virtual OperadorBodega IdOperadorBodegaNavigation { get; set; }
        public virtual TransPeDet IdPedidoDetNavigation { get; set; }
        public virtual TransPickingEnc IdPickingEncNavigation { get; set; }
        public virtual ICollection<TransPickingDetParametro> TransPickingDetParametros { get; set; }
        public virtual ICollection<TransPickingUbic> TransPickingUbics { get; set; }
    }
}
