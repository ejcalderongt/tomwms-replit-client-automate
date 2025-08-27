using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransPackingEnc
    {
        public int Idpackingenc { get; set; }
        public int Idbodega { get; set; }
        public int Idpickingenc { get; set; }
        public int? Iddespachoenc { get; set; }
        public int Idproductobodega { get; set; }
        public int Idproductoestado { get; set; }
        public int? Idpresentacion { get; set; }
        public int Idunidadmedida { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaVence { get; set; }
        public string LicPlate { get; set; }
        public int NoLinea { get; set; }
        public double CantidadBultosPacking { get; set; }
        public double CantidadCamasPacking { get; set; }
        public int Idoperadorbodega { get; set; }
        public int? Idempresaservicio { get; set; }
        public string Referencia { get; set; }
        public DateTime FechaPacking { get; set; }
    }
}
