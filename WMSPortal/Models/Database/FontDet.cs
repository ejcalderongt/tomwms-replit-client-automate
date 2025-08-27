#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class FontDet
    {
        public int IdFontDet { get; set; }
        public int? IdFontEnc { get; set; }
        public string Letra { get; set; }
        public double? Tamaño { get; set; }
        public bool? Negrita { get; set; }
        public bool? Cursiva { get; set; }
        public bool? Subrayado { get; set; }
        public string ColorFont { get; set; }
        public string ColorFondo { get; set; }
    }
}
