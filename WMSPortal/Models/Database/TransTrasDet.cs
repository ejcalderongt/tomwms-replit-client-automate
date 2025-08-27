using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransTrasDet
    {
        public long IdTrasladoDet { get; set; }
        public int IdTrasladoEnc { get; set; }
        public int IdProducto { get; set; }
        public int IdEstado { get; set; }
        public int IdPresentacion { get; set; }
        public int? IdUnidadMedidaBasica { get; set; }
        public double? Cantidad { get; set; }
        public double? Peso { get; set; }
        public double? Precio { get; set; }
        public double? CantDespachada { get; set; }
        public string NombreProducto { get; set; }
        public string NomPresentacion { get; set; }
        public string NomUnidMed { get; set; }
        public string NomEstado { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public bool? FechaEspecifica { get; set; }

        public virtual ProductoEstado IdEstadoNavigation { get; set; }
        public virtual ProductoPresentacion IdPresentacionNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
        public virtual TransTrasEnc IdTrasladoEncNavigation { get; set; }
    }
}
