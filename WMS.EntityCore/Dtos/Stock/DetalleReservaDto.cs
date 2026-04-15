namespace WMS.EntityCore.Dtos.Stock
{
    public class DetalleReservaDto
    {
        public int NoLinea { get; set; }
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public double QuantityRequested { get; set; }
        public double Factor { get; set; } = 1;
        public int IdStockRes { get; set; }
        public int IdStock { get; set; }
        public string LotNo { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public string LocationCode { get; set; } = string.Empty;
        public string Zone { get; set; } = string.Empty;
        public double ReservationQty { get; set; }
    }
}