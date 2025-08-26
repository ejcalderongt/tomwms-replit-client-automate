using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class EmpresaTransporteVehiculo
    {
        public EmpresaTransporteVehiculo()
        {
            TmsTickets = new HashSet<TmsTicket>();
            TransDespachoEncs = new HashSet<TransDespachoEnc>();
            TransTrasEncs = new HashSet<TransTrasEnc>();
        }

        public int IdVehiculo { get; set; }
        public int IdEmpresaTransporte { get; set; }
        public int? IdTipoContenedor { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public double? Peso { get; set; }
        public double? Volumen { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public string Tipo { get; set; }
        public double? Alto { get; set; }
        public double? Largo { get; set; }
        public double? Ancho { get; set; }
        public string PlacaComercial { get; set; }
        public int? EsContedor { get; set; }

        public virtual EmpresaTransporte IdEmpresaTransporteNavigation { get; set; }
        public virtual ICollection<TmsTicket> TmsTickets { get; set; }
        public virtual ICollection<TransDespachoEnc> TransDespachoEncs { get; set; }
        public virtual ICollection<TransTrasEnc> TransTrasEncs { get; set; }
    }
}
