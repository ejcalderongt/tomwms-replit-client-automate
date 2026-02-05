using System.ComponentModel.DataAnnotations;

namespace WMSWebAPI.Models
{
    public class TransaccionProcesada
    {
        [Key]
        public Guid IdTransaccion { get; set; }
        public DateTime FechaProcesado { get; set; } = DateTime.Now;

        [MaxLength(20)]
        public string Estado { get; set; } = "Procesado";

        [MaxLength(50)]
        public string Fuente { get; set; } = "Desconocido";
    }

}
