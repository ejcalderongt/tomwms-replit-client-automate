using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Producto
{
    public class clsBeProducto_parametros : ICloneable
    {
        [Column("IdProductoParametro")]
        [DisplayName("IdProductoParametro")]
        public int IdProductoParametro { get; set; } = 0;

        [Column("IdParametro")]
        [DisplayName("IdParametro")]
        public int IdParametro { get; set; } = 0;

        [Column("IdProducto")]
        [DisplayName("IdProducto")]
        public int IdProducto { get; set; } = 0;

        [Column("valor_texto")]
        [DisplayName("valor_texto")]
        public string Valor_texto { get; set; } = "";

        [Column("valor_numerico")]
        [DisplayName("valor_numerico")]
        public double Valor_numerico { get; set; } = 0;

        [Column("valor_fecha")]
        [DisplayName("valor_fecha")]
        public DateTime Valor_fecha { get; set; } = DateTime.Now;

        [Column("valor_logico")]
        [DisplayName("valor_logico")]
        public bool Valor_logico { get; set; } = false;

        [Column("capturar_siempre")]
        [DisplayName("capturar_siempre")]
        public bool Capturar_siempre { get; set; } = false;

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

        public clsBeProducto_parametros() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}