using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeBodega_area : ICloneable
    {
        [Column("IdArea")]
        [DisplayName("IdArea")]
        public int IdArea { get; set; } = 0;

        [Column("IdBodega")]
        [DisplayName("IdBodega")]
        public int IdBodega { get; set; } = 0;

        [Column("Descripcion")]
        [DisplayName("Descripcion")]
        public string Descripcion { get; set; } = "";

        [Column("sistema")]
        [DisplayName("sistema")]
        public bool Sistema { get; set; } = false;

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

        [Column("Codigo")]
        [DisplayName("Codigo")]
        public string Codigo { get; set; } = "";

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("alto")]
        [DisplayName("alto")]
        public double Alto { get; set; } = 0;

        [Column("largo")]
        [DisplayName("largo")]
        public double Largo { get; set; } = 0;

        [Column("ancho")]
        [DisplayName("ancho")]
        public double Ancho { get; set; } = 0;

        [Column("margen_izquierdo")]
        [DisplayName("margen_izquierdo")]
        public double Margen_izquierdo { get; set; } = 0;

        [Column("margen_derecho")]
        [DisplayName("margen_derecho")]
        public double Margen_derecho { get; set; } = 0;

        [Column("margen_superior")]
        [DisplayName("margen_superior")]
        public double Margen_superior { get; set; } = 0;

        [Column("margen_inferior")]
        [DisplayName("margen_inferior")]
        public double Margen_inferior { get; set; } = 0;

        [Column("grupo")]
        [DisplayName("grupo")]
        public string Grupo { get; set; } = "";

        [Column("IdUbicacionRef")]
        [DisplayName("IdUbicacionRef")]
        public int IdUbicacionRef { get; set; } = 0;

        public clsBeBodega_area() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
