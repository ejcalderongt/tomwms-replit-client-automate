using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class OperadorBodega
    {
        public OperadorBodega()
        {
            ProductoPallets = new HashSet<ProductoPallet>();
            TransPickingDets = new HashSet<TransPickingDet>();
            TransPickingOps = new HashSet<TransPickingOp>();
            TransReDets = new HashSet<TransReDet>();
            TransReOps = new HashSet<TransReOp>();
            TransTrasOps = new HashSet<TransTrasOp>();
            TransUbicHhDets = new HashSet<TransUbicHhDet>();
            TransUbicHhOps = new HashSet<TransUbicHhOp>();
        }

        public int IdOperadorBodega { get; set; }
        public int? IdOperador { get; set; }
        public int? IdBodega { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }

        public virtual Bodega IdBodegaNavigation { get; set; }
        public virtual Operador IdOperadorNavigation { get; set; }
        public virtual ICollection<ProductoPallet> ProductoPallets { get; set; }
        public virtual ICollection<TransPickingDet> TransPickingDets { get; set; }
        public virtual ICollection<TransPickingOp> TransPickingOps { get; set; }
        public virtual ICollection<TransReDet> TransReDets { get; set; }
        public virtual ICollection<TransReOp> TransReOps { get; set; }
        public virtual ICollection<TransTrasOp> TransTrasOps { get; set; }
        public virtual ICollection<TransUbicHhDet> TransUbicHhDets { get; set; }
        public virtual ICollection<TransUbicHhOp> TransUbicHhOps { get; set; }
    }
}
