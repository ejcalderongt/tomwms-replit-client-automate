using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeBodega_muelles : ICloneable
    {
        [Column("IdMuelle")]
        [DisplayName("IdMuelle")]
        public int IdMuelle { get; set; } = 0;

        [Column("IdBodega")]
        [DisplayName("IdBodega")]
        public int IdBodega { get; set; } = 0;

        [Column("codigo_barra")]
        [DisplayName("codigo_barra")]
        public string Codigo_barra { get; set; } = "";

        [Column("nombre")]
        [DisplayName("nombre")]
        public string Nombre { get; set; } = "";

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

        [Column("color")]
        [DisplayName("color")]
        public int Color { get; set; } = 0;

        [Column("imagen")]
        [DisplayName("imagen")]
        public byte[] Imagen { get; set; } = Array.Empty<byte>();

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("Entrada")]
        [DisplayName("Entrada")]
        public bool Entrada { get; set; } = false;

        [Column("Salida")]
        [DisplayName("Salida")]
        public bool Salida { get; set; } = false;

        [Column("IdUbicacionDefecto")]
        [DisplayName("IdUbicacionDefecto")]
        public int IdUbicacionDefecto { get; set; } = 0;

        public clsBeBodega_muelles() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
