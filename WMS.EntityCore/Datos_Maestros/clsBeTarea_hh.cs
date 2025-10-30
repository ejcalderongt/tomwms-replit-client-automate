using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeTarea_hh : ICloneable
    {
        [Column("IdTareahh")]
        [DisplayName("IdTareahh")]
        public int IdTareahh { get; set; } = 0;

        [Column("IdPropietario")]
        [DisplayName("IdPropietario")]
        public int IdPropietario { get; set; } = 0;

        [Column("IdBodega")]
        [DisplayName("IdBodega")]
        public int IdBodega { get; set; } = 0;

        [Column("IdMuelle")]
        [DisplayName("IdMuelle")]
        public int IdMuelle { get; set; } = 0;

        [Column("IdEstado")]
        [DisplayName("IdEstado")]
        public int IdEstado { get; set; } = 0;

        [Column("IdPrioridad")]
        [DisplayName("IdPrioridad")]
        public int IdPrioridad { get; set; } = 0;

        [Column("IdTipoTarea")]
        [DisplayName("IdTipoTarea")]
        public int IdTipoTarea { get; set; } = 0;

        [Column("IdTransaccion")]
        [DisplayName("IdTransaccion")]
        public int IdTransaccion { get; set; } = 0;

        [Column("Tipo")]
        [DisplayName("Tipo")]
        public int Tipo { get; set; } = 0;

        [Column("FechaInicio")]
        [DisplayName("FechaInicio")]
        public DateTime FechaInicio { get; set; } = DateTime.Now;

        [Column("FechaFin")]
        [DisplayName("FechaFin")]
        public DateTime FechaFin { get; set; } = DateTime.Now;

        [Column("DiaCompleto")]
        [DisplayName("DiaCompleto")]
        public bool DiaCompleto { get; set; } = false;

        [Column("Asunto")]
        [DisplayName("Asunto")]
        public string Asunto { get; set; } = "";

        [Column("Ubicacion")]
        [DisplayName("Ubicacion")]
        public string Ubicacion { get; set; } = "";

        [Column("Descripcion")]
        [DisplayName("Descripcion")]
        public string Descripcion { get; set; } = "";

        [Column("Recordatorio")]
        [DisplayName("Recordatorio")]
        public string Recordatorio { get; set; } = "";

        [Column("IdOperadorBodega_Cerro")]
        [DisplayName("IdOperadorBodega_Cerro")]
        public int IdOperadorBodega_Cerro { get; set; } = 0;

        [Column("Host_Cerro")]
        [DisplayName("Host_Cerro")]
        public string Host_Cerro { get; set; } = "";
        public bool CreaTarea { get; set; }=false;        
        public bool IsNew { get; set; } = false;
        public clsBeTarea_hh() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
