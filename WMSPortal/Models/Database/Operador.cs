using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Operador
    {
        public Operador()
        {
            OperadorBodegas = new HashSet<OperadorBodega>();
        }

        public int IdOperador { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdRolOperador { get; set; }
        public int? IdJornada { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Codigo { get; set; }
        public string Clave { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public double? CostoHora { get; set; }
        public bool? UsaHh { get; set; }
        public byte[] Foto { get; set; }
        public bool? Recibe { get; set; }
        public bool? Ubica { get; set; }
        public bool? Transporta { get; set; }
        public bool? Pickea { get; set; }
        public bool? Verifica { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual JornadaLaboral IdJornadaNavigation { get; set; }
        public virtual ICollection<OperadorBodega> OperadorBodegas { get; set; }
    }
}
