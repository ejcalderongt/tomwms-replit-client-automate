using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransReDetInfraccion
    {
        public int IdRecepcionDetInfraccion { get; set; }
        public int? IdReglaPropietarioEnc { get; set; }
        public int? IdOrdenCompraEnc { get; set; }
        public int? IdRecepcionEnc { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdProductoBodega { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }

        public virtual TransOcEnc IdOrdenCompraEncNavigation { get; set; }
        public virtual ProductoPresentacion IdPresentacionNavigation { get; set; }
        public virtual ProductoBodega IdProductoBodegaNavigation { get; set; }
        public virtual TransReEnc IdRecepcionEncNavigation { get; set; }
        public virtual PropietarioReglasEnc IdReglaPropietarioEncNavigation { get; set; }
    }
}
