using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class PropietarioReglasDet
    {
        public int IdReglaPropietarioDet { get; set; }
        public int? IdReglaPropietarioEnc { get; set; }
        public int? IdDestinatarioPropietario { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }

        public virtual PropietarioDestinatario IdDestinatarioPropietarioNavigation { get; set; }
        public virtual PropietarioReglasEnc IdReglaPropietarioEncNavigation { get; set; }
    }
}
