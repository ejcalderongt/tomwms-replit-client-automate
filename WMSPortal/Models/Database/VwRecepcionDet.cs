using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwRecepcionDet
    {
        public int? IdBodega { get; set; }
        public int IdRecepcionEnc { get; set; }
        public string Propietario { get; set; }
        public string Poliza { get; set; }
        public string CodigoBodega { get; set; }
        public int IdOrdenCompraEnc { get; set; }
        public string CodigoProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string NoDocumentoOc { get; set; }
        public string ReferenciaOc { get; set; }
        public DateTime? Fecha { get; set; }
        public string Estado { get; set; }
        public string TipoTrans { get; set; }
        public string Descripcion { get; set; }
        public bool? Activo { get; set; }
        public string UsuarioAgrego { get; set; }
        public string OperadorHh { get; set; }
        public DateTime? FechaAgrego { get; set; }
        public string CodigoProd { get; set; }
        public string BarraProd { get; set; }
        public string NombreProd { get; set; }
        public double? Recibido { get; set; }
        public string Um { get; set; }
        public string EstadoProd { get; set; }
        public string PresProd { get; set; }
        public string LicPlate { get; set; }
    }
}
