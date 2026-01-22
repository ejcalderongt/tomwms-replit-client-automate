using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Datos_Maestros
{
    public class clsBeIndice_rotacion : ICloneable
    {
        [Column("IdIndiceRotacion")]
        [DisplayName("IdIndiceRotacion")]
        public int IdIndiceRotacion { get; set; } = 0;

        [Column("Descripcion")]
        [DisplayName("Descripcion")]
        public string Descripcion { get; set; } = "";

        [Column("Activo")]
        [DisplayName("Activo")]
        public bool Activo { get; set; } = false;

        [Column("IndicePrioridad")]
        [DisplayName("IndicePrioridad")]
        public int IndicePrioridad { get; set; } = 0;

        [Column("Grupo")]
        [DisplayName("Grupo")]
        public int Grupo { get; set; } = 0;

        public clsBeIndice_rotacion() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}