using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransImploEnc
    {
        public int IdPalletEnc { get; set; }
        public string LicPlate { get; set; }
        public DateTime? FecAgr { get; set; }
        public int? Idtipopallet { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string UserAgr { get; set; }
        public string UserMod { get; set; }
    }
}
