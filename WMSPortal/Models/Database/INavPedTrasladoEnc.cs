using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavPedTrasladoEnc
    {
        public string No { get; set; }
        public DateTime? PostingDate { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public int? Status { get; set; }
        public string TransferFromCode { get; set; }
        public string TransferFromContact { get; set; }
        public string TransferFromName { get; set; }
        public string TransferToCode { get; set; }
        public string TransferToContact { get; set; }
        public string TransferToName { get; set; }
        public string TransferToCodeField { get; set; }
        public string ProductOwnerCode { get; set; }
        public string ReceiptDocumentReference { get; set; }
    }
}
