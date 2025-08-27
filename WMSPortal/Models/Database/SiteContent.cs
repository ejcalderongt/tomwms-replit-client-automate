using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class SiteContent
    {
        public Guid SiteContentId { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
        public int? Length { get; set; }
        public byte[] Data { get; set; }
        public string Text { get; set; }
        public string Roles { get; set; }
        public string RoleExceptions { get; set; }
        public string Users { get; set; }
        public string UserExceptions { get; set; }
        public string Schedule { get; set; }
        public string ScheduleExceptions { get; set; }
        public string CacheProfile { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
