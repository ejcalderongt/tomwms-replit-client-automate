using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Cliente
    {
        public Cliente()
        {
            ClienteBodegas = new HashSet<ClienteBodega>();
            ClienteTiempos = new HashSet<ClienteTiempo>();
            TransPeEncs = new HashSet<TransPeEnc>();
        }

        public int IdCliente { get; set; }
        public int IdEmpresa { get; set; }
        public int IdPropietario { get; set; }
        public int IdTipoCliente { get; set; }
        public int? IdUbicacionManufactura { get; set; }
        public string Codigo { get; set; }
        public string NombreComercial { get; set; }
        public string NombreContacto { get; set; }
        public string Telefono { get; set; }
        public string Nit { get; set; }
        public string Direccion { get; set; }
        public string CorreoElectronico { get; set; }
        public bool? Activo { get; set; }
        public bool? RealizaManufactura { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? DespacharLotesCompletos { get; set; }
        public bool? Sistema { get; set; }
        public bool? EsBodegaRecepcion { get; set; }
        public bool? EsBodegaTraslado { get; set; }
        public int? Idubicacionvirtual { get; set; }
        public string Referencia { get; set; }
        public bool? ControlUltimoLote { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual Propietario IdPropietarioNavigation { get; set; }
        public virtual ClienteTipo IdTipoClienteNavigation { get; set; }
        public virtual ICollection<ClienteBodega> ClienteBodegas { get; set; }
        public virtual ICollection<ClienteTiempo> ClienteTiempos { get; set; }
        public virtual ICollection<TransPeEnc> TransPeEncs { get; set; }
    }
}
