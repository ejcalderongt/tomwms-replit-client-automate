using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class EmpresaTransporte
    {
        public EmpresaTransporte()
        {
            EmpresaTransporteBodegas = new HashSet<EmpresaTransporteBodega>();
            EmpresaTransportePilotos = new HashSet<EmpresaTransportePiloto>();
            EmpresaTransporteVehiculos = new HashSet<EmpresaTransporteVehiculo>();
            TmsTickets = new HashSet<TmsTicket>();
        }

        public int IdEmpresaTransporte { get; set; }
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual ICollection<EmpresaTransporteBodega> EmpresaTransporteBodegas { get; set; }
        public virtual ICollection<EmpresaTransportePiloto> EmpresaTransportePilotos { get; set; }
        public virtual ICollection<EmpresaTransporteVehiculo> EmpresaTransporteVehiculos { get; set; }
        public virtual ICollection<TmsTicket> TmsTickets { get; set; }
    }
}
