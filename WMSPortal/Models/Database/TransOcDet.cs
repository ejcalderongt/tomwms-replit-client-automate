using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransOcDet
    {
        public int IdOrdenCompraEnc { get; set; }
        public int IdOrdenCompraDet { get; set; }
        public int IdProductoBodega { get; set; }
        public int? IdArancel { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedidaBasica { get; set; }
        public int? IdMotivoDevolucion { get; set; }
        public int? NoLinea { get; set; }
        public string NombreProducto { get; set; }
        public string NombrePresentacion { get; set; }
        public string NombreArancel { get; set; }
        public double? PorcentajeArancel { get; set; }
        public string NombreUnidadMedidaBasica { get; set; }
        public double? Cantidad { get; set; }
        public double? CantidadRecibida { get; set; }
        public double? Costo { get; set; }
        public double? TotalLinea { get; set; }
        public string UserAgr { get; set; }
        public DateTime FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime FecMod { get; set; }
        public bool Activo { get; set; }
        public double? Peso { get; set; }
        public double? PesoRecibido { get; set; }
        public string AtributoVariante1 { get; set; }
        public string CodigoProducto { get; set; }
        public double? ValorAduana { get; set; }
        public double? ValorFob { get; set; }
        public double? ValorIva { get; set; }
        public double? ValorDai { get; set; }
        public double? ValorSeguro { get; set; }
        public double? ValorFlete { get; set; }
        public double? PesoNeto { get; set; }
        public double? PesoBruto { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public string NombrePropietario { get; set; }
        public int? IdOrdenCompraDetPadre { get; set; }

        public virtual Arancel IdArancelNavigation { get; set; }
        public virtual MotivoDevolucion IdMotivoDevolucionNavigation { get; set; }
        public virtual TransOcEnc IdOrdenCompraEncNavigation { get; set; }
        public virtual ProductoPresentacion IdPresentacionNavigation { get; set; }
        public virtual ProductoBodega IdProductoBodegaNavigation { get; set; }
        public virtual UnidadMedidum IdUnidadMedidaBasicaNavigation { get; set; }
    }
}
