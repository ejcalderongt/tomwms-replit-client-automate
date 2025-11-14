using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeUnidad_medida_conversion : ICloneable
    {
        [Column("IdConversion")]
        [DisplayName("IdConversion")]
        public int IdConversion { get; set; } = 0;

        [Column("IdUnidadMedidaOrigen")]
        [DisplayName("IdUnidadMedidaOrigen")]
        public int IdUnidadMedidaOrigen { get; set; } = 0;

        [Column("IdUnidadMedidaDestino")]
        [DisplayName("IdUnidadMedidaDestino")]
        public int IdUnidadMedidaDestino { get; set; } = 0;

        [Column("Factor")]
        [DisplayName("Factor")]
        public double Factor { get; set; } = 0;

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        public clsBeUnidad_medida_conversion() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
