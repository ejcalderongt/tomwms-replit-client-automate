using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavPedCompraDetLote
    {
        public string NoEnc { get; set; }
        public string SourceId { get; set; }
        public int SourceProdOrderLine { get; set; }
        public string ItemNo { get; set; }
        public string LotNo { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string EntryNo { get; set; }
        public int? SourceType { get; set; }
        public double? QuantityBase { get; set; }
        public string VariantCode { get; set; }
    }
}
