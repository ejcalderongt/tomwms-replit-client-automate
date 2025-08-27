using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavTransaccionesOutError
    {
        public int IdMensaje { get; set; }
        public string TipoTransaccionErp { get; set; }
        public string TipoTransaccionRoad { get; set; }
        public string ReferenciaErp { get; set; }
        public string ReferenciaRoad { get; set; }
        public string Mensaje { get; set; }
        public string NumeroError { get; set; }
        public string Observacion { get; set; }
        public DateTime? Fecha { get; set; }
        public string UsuarioErp { get; set; }
        public string UsuarioWms { get; set; }
        public bool? EsError { get; set; }
    }
}
