using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavPedCompraEnc
    {
        public string No { get; set; }
        public string BuyFromVendorNo { get; set; }
        public string BuyFromVendorName { get; set; }
        public string PostingDescription { get; set; }
        public DateTime? PostingDate { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string VendorInvoiceNo { get; set; }
        public string Status { get; set; }
        public string PaymentTermsCode { get; set; }
        public string ShipToName { get; set; }
        public string LocationCode { get; set; }
        public string ShipToContact { get; set; }
        public DateTime? ExpectedReceiptDate { get; set; }
        public bool? IsInternalTransfer { get; set; }
        public string ProductOwnerCode { get; set; }
        public string InternalTransferDocumentNo { get; set; }
        public int? DocumentType { get; set; }
    }
}
