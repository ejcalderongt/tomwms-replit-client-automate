using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Usuario
    {
        public Usuario()
        {
            INavConfigEncs = new HashSet<INavConfigEnc>();
            UsuarioBodegaIdUsuarioNavigations = new HashSet<UsuarioBodega>();
            UsuarioBodegaIdUsuarioSuperiorNavigations = new HashSet<UsuarioBodega>();
        }

        public int IdUsuario { get; set; }
        public int? IdEmpresa { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Codigo { get; set; }
        public string Clave { get; set; }
        public DateTime? UltimoLogin { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public byte[] Foto { get; set; }
        public bool? Sistema { get; set; }
        public string ClaveAutorizacion { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual ICollection<INavConfigEnc> INavConfigEncs { get; set; }
        public virtual ICollection<UsuarioBodega> UsuarioBodegaIdUsuarioNavigations { get; set; }
        public virtual ICollection<UsuarioBodega> UsuarioBodegaIdUsuarioSuperiorNavigations { get; set; }
    }
}
