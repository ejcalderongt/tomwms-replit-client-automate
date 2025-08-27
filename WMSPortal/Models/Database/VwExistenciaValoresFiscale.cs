using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwExistenciaValoresFiscale
    {
        public int IdRecepcionEnc { get; set; }
        public string Propietario { get; set; }
        public string Proveedor { get; set; }
        public string Bodega { get; set; }
        public int IdOrdenCompraEnc { get; set; }
        public string NoDocumentoOc { get; set; }
        public string NoDocumentoRec { get; set; }
        public string ReferenciaOc { get; set; }
        public DateTime? Fecha { get; set; }
        public string Estado { get; set; }
        public string TipoTrans { get; set; }
        public string Descripcion { get; set; }
        public string Muelle { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FechaAgrego { get; set; }
        public string CodigoProd { get; set; }
        public string BarraProd { get; set; }
        public string NombreProd { get; set; }
        public double? Recibido { get; set; }
        public double ExistenciaActualUmbas { get; set; }
        public double? ExistenciaActualPres { get; set; }
        public string Um { get; set; }
        public string EstadoProd { get; set; }
        public string PresProd { get; set; }
        public string LicPlate { get; set; }
        public double? Factor { get; set; }
        public string Lote { get; set; }
        public DateTime? Vence { get; set; }
        public int IdStock { get; set; }
        public string UbicacionOrigen { get; set; }
        public string NoPoliza { get; set; }
        public double? ValorAduana { get; set; }
        public double? ValorFob { get; set; }
        public double? ValorIva { get; set; }
        public double? ValorDai { get; set; }
        public double? ValorSeguro { get; set; }
        public double? ValorFlete { get; set; }
        public double? PesoNeto { get; set; }
    }
}
