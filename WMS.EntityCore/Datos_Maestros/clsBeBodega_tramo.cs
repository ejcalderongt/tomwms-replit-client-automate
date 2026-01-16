using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace WMS.EntityCore.Datos_Maestros
{
    public class clsBeBodega_tramo : ICloneable
    {
        [Column("IdTramo")]
        [DisplayName("IdTramo")]
        public int IdTramo { get; set; } = 0;

        [Column("IdSector")]
        [DisplayName("IdSector")]
        public int IdSector { get; set; } = 0;

        [Column("sistema")]
        [DisplayName("sistema")]
        public bool Sistema { get; set; } = false;

        [Column("descripcion")]
        [DisplayName("descripcion")]
        public string Descripcion { get; set; } = "";

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

        [Column("Codigo")]
        [DisplayName("Codigo")]
        public string Codigo { get; set; } = "";

        [Column("Indice_x")]
        [DisplayName("Indice_x")]
        public int Indice_x { get; set; } = 0;

        [Column("Orientacion")]
        [DisplayName("Orientacion")]
        public int Orientacion { get; set; } = 0;

        [Column("IdTipoProductoDefault")]
        [DisplayName("IdTipoProductoDefault")]
        public int IdTipoProductoDefault { get; set; } = 0;

        [Column("IdFontEnc")]
        [DisplayName("IdFontEnc")]
        public int IdFontEnc { get; set; } = 0;

        [Column("IdTipoRack")]
        [DisplayName("IdTipoRack")]
        public int IdTipoRack { get; set; } = 0;

        [Column("es_rack")]
        [DisplayName("es_rack")]
        public bool Es_rack { get; set; } = false;

        [Column("Horizontal")]
        [DisplayName("Horizontal")]
        public bool Horizontal { get; set; } = false;

        [Column("IdArea")]
        [DisplayName("IdArea")]
        public int IdArea { get; set; } = 0;

        [Column("IdBodega")]
        [DisplayName("IdBodega")]
        public int IdBodega { get; set; } = 0;

        [Column("orden_descendente")]
        [DisplayName("orden_descendente")]
        public bool Orden_descendente { get; set; } = false;

        public clsBeBodega_tramo() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
