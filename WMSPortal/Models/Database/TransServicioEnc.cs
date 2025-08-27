using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransServicioEnc
    {
        public TransServicioEnc()
        {
            TransServicioDets = new HashSet<TransServicioDet>();
        }

        public int IdServicioEnc { get; set; }
        public int? IdOrdenCompraEnc { get; set; }
        public int IdEmpresa { get; set; }
        public int IdBodega { get; set; }
        public string NoPoliza { get; set; }
        public string NoOrden { get; set; }
        public DateTime? FechaDocIngreso { get; set; }
        public DateTime? FechaServicio { get; set; }
        public bool EnviadoAErp { get; set; }
        public bool? Activo { get; set; }
        public int IdPropietario { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public string Estado { get; set; }
        public int? IdPedidoEnc { get; set; }
        public bool? EsIngreso { get; set; }

        public virtual Bodega IdBodegaNavigation { get; set; }
        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual TransOcEnc IdOrdenCompraEncNavigation { get; set; }
        public virtual Propietario IdPropietarioNavigation { get; set; }
        public virtual ICollection<TransServicioDet> TransServicioDets { get; set; }
    }
}
