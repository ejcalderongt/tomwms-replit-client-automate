#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavUnidadMedidum
    {
        public int IdUnidadMedida { get; set; }
        public string CodigoUnidadMedida { get; set; }
        public string Descripcion { get; set; }
        public string TipoRubro { get; set; }
        public string Tipo { get; set; }
        public bool ProcesadoWms { get; set; }
    }
}
