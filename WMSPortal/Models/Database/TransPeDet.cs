using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransPeDet
    {
        public TransPeDet()
        {
            TransPickingDets = new HashSet<TransPickingDet>();
        }

        public int IdPedidoDet { get; set; }
        public int IdPedidoEnc { get; set; }
        public int IdProductoBodega { get; set; }
        public int IdEstado { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedidaBasica { get; set; }
        public double? Cantidad { get; set; }
        public double? Peso { get; set; }
        public double? Precio { get; set; }
        public long? NoRecepcion { get; set; }
        public int? Ndias { get; set; }
        public double? CantDespachada { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string NomPresentacion { get; set; }
        public string NomUnidMed { get; set; }
        public string NomEstado { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public bool? FechaEspecifica { get; set; }
        public double? RoadDes { get; set; }
        public double? RoadDesMon { get; set; }
        public double? RoadTotal { get; set; }
        public double? RoadPrecioDoc { get; set; }
        public double? RoadVal1 { get; set; }
        public string RoadVal2 { get; set; }
        public double? RoadCantProc { get; set; }
        public double? PesoDespachado { get; set; }
        public int? NoLinea { get; set; }
        public string AtributoVariante1 { get; set; }
        public int? IdStockEspecifico { get; set; }
        public bool? EsPadre { get; set; }
        public int? IdPedidoDetPadre { get; set; }
        public double? PesoBruto { get; set; }
        public double? PesoNeto { get; set; }
        public double? Costo { get; set; }
        public double? ValorAduana { get; set; }
        public double? ValorFob { get; set; }
        public double? ValorIva { get; set; }
        public double? ValorDai { get; set; }
        public double? ValorSeguro { get; set; }
        public double? ValorFlete { get; set; }
        public double? TotalLinea { get; set; }

        public virtual TransPeEnc IdPedidoEncNavigation { get; set; }
        public virtual ProductoPresentacion IdPresentacionNavigation { get; set; }
        public virtual ProductoBodega IdProductoBodegaNavigation { get; set; }
        public virtual UnidadMedidum IdUnidadMedidaBasicaNavigation { get; set; }
        public virtual ICollection<TransPickingDet> TransPickingDets { get; set; }
    }
}
