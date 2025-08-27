using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavPedCompraDet
    {
        public string NoEnc { get; set; }
        public string No { get; set; }
        public int LineNo { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
        public string LocationCode { get; set; }
        public double? Quantity { get; set; }
        public string UnitOfMeasureCode { get; set; }
        public double? DirectUnitCost { get; set; }
        public double? LineAmount { get; set; }
        public double? QuantityReceived { get; set; }
        public DateTime? PlanedReceiptDate { get; set; }
        public string VariantCode { get; set; }
    }
}
