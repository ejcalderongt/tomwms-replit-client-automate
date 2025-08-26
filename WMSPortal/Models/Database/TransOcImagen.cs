#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransOcImagen
    {
        public int IdOrdenCompraImg { get; set; }
        public int IdOrdenCompraEnc { get; set; }
        public int Orden { get; set; }
        public byte[] Imagen { get; set; }
        public string Descripcion { get; set; }

        public virtual TransOcEnc IdOrdenCompraEncNavigation { get; set; }
    }
}
