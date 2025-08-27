using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ProductoPresentacionTarima
    {
        public int IdPresentacionTarima { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdTipoTarima { get; set; }
        public double? Cantidad { get; set; }
        public double? CantidadPorCama { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }

        public virtual ProductoPresentacion IdPresentacionNavigation { get; set; }
        public virtual TipoTarima IdTipoTarimaNavigation { get; set; }
    }
}
