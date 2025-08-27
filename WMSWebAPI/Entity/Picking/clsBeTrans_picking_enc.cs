using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeTrans_picking_enc : ICloneable
    {
        [Column("IdPickingEnc")]
        [DisplayName("IdPickingEnc")]
        public int IdPickingEnc { get; set; } = 0;

        [Column("IdBodega")]
        [DisplayName("IdBodega")]
        public int IdBodega { get; set; } = 0;

        [Column("IdPropietarioBodega")]
        [DisplayName("IdPropietarioBodega")]
        public int IdPropietarioBodega { get; set; } = 0;

        [Column("IdUbicacionPicking")]
        [DisplayName("IdUbicacionPicking")]
        public int IdUbicacionPicking { get; set; } = 0;

        [Column("fecha_picking")]
        [DisplayName("fecha_picking")]
        public DateTime Fecha_picking { get; set; } = DateTime.Now;

        [Column("hora_ini")]
        [DisplayName("hora_ini")]
        public DateTime Hora_ini { get; set; } = DateTime.Now;

        [Column("hora_fin")]
        [DisplayName("hora_fin")]
        public DateTime Hora_fin { get; set; } = DateTime.Now;

        [Column("estado")]
        [DisplayName("estado")]
        public string Estado { get; set; } = "";

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

        [Column("detalle_operador")]
        [DisplayName("detalle_operador")]
        public bool Detalle_operador { get; set; } = false;

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("verifica_auto")]
        [DisplayName("verifica_auto")]
        public bool Verifica_auto { get; set; } = false;

        [Column("procesado_bof")]
        [DisplayName("procesado_bof")]
        public bool Procesado_bof { get; set; } = false;

        [Column("requiere_preparacion")]
        [DisplayName("requiere_preparacion")]
        public bool Requiere_preparacion { get; set; } = false;

        [Column("tipo_preparacion")]
        [DisplayName("tipo_preparacion")]
        public string Tipo_preparacion { get; set; } = "";

        [Column("estado_preparacion")]
        [DisplayName("estado_preparacion")]
        public string Estado_preparacion { get; set; } = "";

        [Column("fecha_inicio_preparacion")]
        [DisplayName("fecha_inicio_preparacion")]
        public DateTime Fecha_inicio_preparacion { get; set; } = DateTime.Now;

        [Column("fecha_fin_preparacion")]
        [DisplayName("fecha_fin_preparacion")]
        public DateTime Fecha_fin_preparacion { get; set; } = DateTime.Now;

        [Column("referencia")]
        [DisplayName("referencia")]
        public string Referencia { get; set; } = "";

        [Column("fotografia_verificacion")]
        [DisplayName("fotografia_verificacion")]
        public bool Fotografia_verificacion { get; set; } = false;

        [Column("IdBodegaMuelle")]
        [DisplayName("IdBodegaMuelle")]
        public int IdBodegaMuelle { get; set; } = 0;

        [Column("IdPrioridadPicking")]
        [DisplayName("IdPrioridadPicking")]
        public int IdPrioridadPicking { get; set; } = 0;

        [Column("IdTipoPicking")]
        [DisplayName("IdTipoPicking")]
        public int IdTipoPicking { get; set; } = 0;

        public clsBeTrans_picking_enc() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
