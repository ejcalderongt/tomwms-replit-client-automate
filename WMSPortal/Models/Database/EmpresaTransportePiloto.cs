using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class EmpresaTransportePiloto
    {
        public EmpresaTransportePiloto()
        {
            TmsTickets = new HashSet<TmsTicket>();
            TransDespachoEncs = new HashSet<TransDespachoEnc>();
            TransTrasEncs = new HashSet<TransTrasEnc>();
        }

        public int IdPiloto { get; set; }
        public int IdEmpresaTransporte { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string NoCarnet { get; set; }
        public DateTime? FechaExpiracionCarnet { get; set; }
        public string NoDpi { get; set; }
        public string NoLicencia { get; set; }
        public DateTime? FechaExpiracionLicencia { get; set; }
        public string CodigoBarra { get; set; }
        public string Direccion { get; set; }
        public byte[] Foto { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaSalida { get; set; }
        public string IdTipoLicencia { get; set; }
        public string UserAgr { get; set; }
        public DateTime FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime FecMod { get; set; }
        public bool Activo { get; set; }

        public virtual EmpresaTransporte IdEmpresaTransporteNavigation { get; set; }
        public virtual ICollection<TmsTicket> TmsTickets { get; set; }
        public virtual ICollection<TransDespachoEnc> TransDespachoEncs { get; set; }
        public virtual ICollection<TransTrasEnc> TransTrasEncs { get; set; }
    }
}
