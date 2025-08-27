using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class MontacargaServicioEnc
    {
        public int IdServicioEnc { get; set; }
        public int IdEmpresa { get; set; }
        public int IdBodega { get; set; }
        public DateTime? FechaSistema { get; set; }
        public string Descripcion { get; set; }
        public string Tecnico { get; set; }
        public string ObservacionTecnico { get; set; }
        public int? Estado { get; set; }
        public DateTime? FechaAtencion { get; set; }
        public DateTime? FechaServicio { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Solicita { get; set; }
        public bool? Garantia { get; set; }
        public double? CostoServicio { get; set; }
        public string TipoServicio { get; set; }
    }
}
