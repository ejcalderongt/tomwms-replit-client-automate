
namespace WMSWebAPI.Dtos.Log_portal_ux
{
    public class LogPortalUxDto
    {

        public int LogUxId { get; set; } = 0;
        public int Idpropietario { get; set; } = 0;
        public string Usuario { get; set; } = string.Empty;
        public string Email { get; set; } = String.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string IpAddress { get; set; } = string.Empty ;
        public string UserAgent { get; set; } = string.Empty;
        public bool Acceso { get; set; } = false;
        public string MensajeError { get; set; } = string.Empty;
        public string UrlAcceso { get; set; } = string.Empty;


    }
}
