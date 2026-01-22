using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Cliente
{
    public class clsBeCliente_bodega : ICloneable
    {
        [Column("IdClienteBodega")]
        [DisplayName("IdClienteBodega")]
        public int IdClienteBodega { get; set; } = 0;

        [Column("IdBodega")]
        [DisplayName("IdBodega")]
        public int IdBodega { get; set; } = 0;

        [Column("IdCliente")]
        [DisplayName("IdCliente")]
        public int IdCliente { get; set; } = 0;

        [Column("user_agr")]
        [DisplayName("user_agr")]
        public string User_agr { get; set; } = "";

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;

        [Column("user_mod")]
        [DisplayName("user_mod")]
        public string User_mod { get; set; } = "";

        [Column("fec_mod")]
        [DisplayName("fec_mod")]
        public DateTime Fec_mod { get; set; } = DateTime.Now;

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("IdAreaDestino")]
        [DisplayName("IdAreaDestino")]
        public int IdAreaDestino { get; set; } = 0;

        public clsBeCliente_bodega() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
