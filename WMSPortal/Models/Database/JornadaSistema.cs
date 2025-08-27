using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class JornadaSistema
    {
        public int IdJornada { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdBodega { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaAgregado { get; set; }
    }
}
