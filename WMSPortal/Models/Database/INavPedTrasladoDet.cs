using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavPedTrasladoDet
    {
        public string NoEnc { get; set; }
        public string No { get; set; }
        public string Description { get; set; }
        public string ItemNo { get; set; }
        public double? QtyToReceive { get; set; }
        public double? QtyToShip { get; set; }
        public double? Quantity { get; set; }
        public string TransferToCodeField { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public string UnitOfMeasureCode { get; set; }
        public string LineNo { get; set; }
        public string VariantCode { get; set; }
        public int? Status { get; set; }
        public string ProcessResult { get; set; }
        public double? Price { get; set; }
        public string SourceId { get; set; }
    }
}
