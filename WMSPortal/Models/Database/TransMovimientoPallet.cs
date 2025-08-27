using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransMovimientoPallet
    {
        public int Idmovimientopallet { get; set; }
        public int? Idbodega { get; set; }
        public string LpOrigen { get; set; }
        public string LpDestino { get; set; }
        public string Orientacion { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Idusuario { get; set; }
    }
}
