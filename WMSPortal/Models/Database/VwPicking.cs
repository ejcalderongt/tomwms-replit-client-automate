using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwPicking
    {
        public int Código { get; set; }
        public string Bodega { get; set; }
        public string Propietario { get; set; }
        public string UbicaciónPicking { get; set; }
        public string Estado { get; set; }
        public bool? DetalleOperador { get; set; }
        public string HoraInicial { get; set; }
        public string HoraFinal { get; set; }
        public string DuracionMinutos { get; set; }
        public DateTime? FechaPicking { get; set; }
        public bool? Activo { get; set; }
        public int IdBodega { get; set; }
        public bool? ProcesadoBof { get; set; }
    }
}
