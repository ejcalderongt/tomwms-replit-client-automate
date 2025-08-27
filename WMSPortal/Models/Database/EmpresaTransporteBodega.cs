using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class EmpresaTransporteBodega
    {
        public int IdAsignacion { get; set; }
        public int? IdEmpresaTransporte { get; set; }
        public int? IdBodega { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }

        public virtual Bodega IdBodegaNavigation { get; set; }
        public virtual EmpresaTransporte IdEmpresaTransporteNavigation { get; set; }
    }
}
