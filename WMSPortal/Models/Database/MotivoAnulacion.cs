using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class MotivoAnulacion
    {
        public MotivoAnulacion()
        {
            MotivoAnulacionBodegas = new HashSet<MotivoAnulacionBodega>();
        }

        public int IdMotivoAnulacion { get; set; }
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual ICollection<MotivoAnulacionBodega> MotivoAnulacionBodegas { get; set; }
    }
}
