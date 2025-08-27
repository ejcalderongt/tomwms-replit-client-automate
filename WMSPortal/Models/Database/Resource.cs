#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Resource
    {
        public int UniqueId { get; set; }
        public int ResourceId { get; set; }
        public string ResourceName { get; set; }
        public int? Color { get; set; }
        public byte[] Image { get; set; }
        public string CustomField1 { get; set; }
    }
}
