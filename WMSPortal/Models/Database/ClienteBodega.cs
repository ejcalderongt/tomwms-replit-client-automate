using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class ClienteBodega
    {
        public int IdClienteBodega { get; set; }
        public int IdBodega { get; set; }
        public int IdCliente { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool Activo { get; set; }

        public virtual Bodega IdBodegaNavigation { get; set; }
        public virtual Cliente IdClienteNavigation { get; set; }
    }
}
