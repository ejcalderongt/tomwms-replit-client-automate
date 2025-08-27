#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavEjecucionRe
    {
        public int Idejecucionres { get; set; }
        public int? Idejecucionenc { get; set; }
        public int? Idnavconfigdet { get; set; }
        public int? RegistrosWs { get; set; }
        public int? RegistrosTi { get; set; }
        public int? RegistrosWms { get; set; }
        public bool? Exitosa { get; set; }
    }
}
