#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavProducto
    {
        public string No { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
        public double? Inventory { get; set; }
        public string BaseUnitOfMeasure { get; set; }
        public double? UnitCost { get; set; }
        public string InventoryPostingGroup { get; set; }
        public string GenProdPostingGroup { get; set; }
        public string SearchDescription { get; set; }
        public string ItemCategoryCode { get; set; }
        public string ProductGroupCode { get; set; }
        public string SalesUnit { get; set; }
        public string ItemTrackingCode { get; set; }
    }
}
