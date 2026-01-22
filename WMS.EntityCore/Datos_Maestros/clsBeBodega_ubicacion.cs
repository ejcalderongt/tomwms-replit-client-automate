using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeBodega_ubicacion : ICloneable
    {
        [Column("IdUbicacion")]
        [DisplayName("IdUbicacion")]
        public int IdUbicacion { get; set; } = 0;

        [Column("IdTramo")]
        [DisplayName("IdTramo")]
        public int IdTramo { get; set; } = 0;

        [Column("descripcion")]
        [DisplayName("descripcion")]
        public string Descripcion { get; set; } = "";

        [Column("ancho")]
        [DisplayName("ancho")]
        public double Ancho { get; set; } = 0;

        [Column("largo")]
        [DisplayName("largo")]
        public double Largo { get; set; } = 0;

        [Column("alto")]
        [DisplayName("alto")]
        public double Alto { get; set; } = 0;

        [Column("nivel")]
        [DisplayName("nivel")]
        public int Nivel { get; set; } = 0;

        [Column("indice_x")]
        [DisplayName("indice_x")]
        public int Indice_x { get; set; } = 0;

        [Column("IdIndiceRotacion")]
        [DisplayName("IdIndiceRotacion")]
        public int IdIndiceRotacion { get; set; } = 0;

        [Column("IdTipoRotacion")]
        [DisplayName("IdTipoRotacion")]
        public int IdTipoRotacion { get; set; } = 0;

        [Column("sistema")]
        [DisplayName("sistema")]
        public bool Sistema { get; set; } = false;

        [Column("codigo_barra")]
        [DisplayName("codigo_barra")]
        public string Codigo_barra { get; set; } = "";

        [Column("codigo_barra2")]
        [DisplayName("codigo_barra2")]
        public string Codigo_barra2 { get; set; } = "";

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

        [Column("dañado")]
        [DisplayName("dañado")]
        public bool Dañado { get; set; } = false;

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("bloqueada")]
        [DisplayName("bloqueada")]
        public bool Bloqueada { get; set; } = false;

        [Column("acepta_pallet")]
        [DisplayName("acepta_pallet")]
        public bool Acepta_pallet { get; set; } = false;

        [Column("ubicacion_picking")]
        [DisplayName("ubicacion_picking")]
        public bool Ubicacion_picking { get; set; } = false;

        [Column("ubicacion_recepcion")]
        [DisplayName("ubicacion_recepcion")]
        public bool Ubicacion_recepcion { get; set; } = false;

        [Column("ubicacion_despacho")]
        [DisplayName("ubicacion_despacho")]
        public bool Ubicacion_despacho { get; set; } = false;

        [Column("ubicacion_merma")]
        [DisplayName("ubicacion_merma")]
        public bool Ubicacion_merma { get; set; } = false;

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

        [Column("orientacion_pos")]
        [DisplayName("orientacion_pos")]
        public string Orientacion_pos { get; set; } = "";

        [Column("ubicacion_virtual")]
        [DisplayName("ubicacion_virtual")]
        public bool Ubicacion_virtual { get; set; } = false;

        [Column("ubicacion_ne")]
        [DisplayName("ubicacion_ne")]
        public bool Ubicacion_ne { get; set; } = false;

        [Column("IdBodega")]
        [DisplayName("IdBodega")]
        public int IdBodega { get; set; } = 0;

        [Column("IdArea")]
        [DisplayName("IdArea")]
        public int IdArea { get; set; } = 0;

        [Column("IdSector")]
        [DisplayName("IdSector")]
        public int IdSector { get; set; } = 0;

        [Column("posicion_x")]
        [DisplayName("posicion_x")]
        public double Posicion_x { get; set; } = 0;

        [Column("posicion_y")]
        [DisplayName("posicion_y")]
        public double Posicion_y { get; set; } = 0;

        [Column("ubicacion_muelle")]
        [DisplayName("ubicacion_muelle")]
        public bool Ubicacion_muelle { get; set; } = false;

        public clsBeBodega_ubicacion() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
