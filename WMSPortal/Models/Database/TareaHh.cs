using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TareaHh
    {
        public int IdTareahh { get; set; }
        public int? IdPropietario { get; set; }
        public int? IdBodega { get; set; }
        public int? IdMuelle { get; set; }
        public int? IdEstado { get; set; }
        public int? IdPrioridad { get; set; }
        public int? IdTipoTarea { get; set; }
        public int? IdTransaccion { get; set; }
        public int? Tipo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? DiaCompleto { get; set; }
        public string Asunto { get; set; }
        public string Ubicacion { get; set; }
        public string Descripcion { get; set; }
        public string Recordatorio { get; set; }

        public virtual Bodega IdBodegaNavigation { get; set; }
        public virtual SisEstadoTareaHh IdEstadoNavigation { get; set; }
        public virtual BodegaMuelle IdMuelleNavigation { get; set; }
        public virtual SisPrioridadTareaHh IdPrioridadNavigation { get; set; }
        public virtual Propietario IdPropietarioNavigation { get; set; }
        public virtual SisTipoTarea IdTipoTareaNavigation { get; set; }
    }
}
