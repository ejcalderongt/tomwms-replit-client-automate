#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TablasSync
    {
        public int IdTabla { get; set; }
        public string NombreTabla { get; set; }
        public bool? Sincronizar { get; set; }
        public byte[] UltimaSincronizacion { get; set; }
        public double? TimpoSyncSegundos { get; set; }
    }
}
