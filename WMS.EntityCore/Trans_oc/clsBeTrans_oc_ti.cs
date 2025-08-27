using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Trans_oc
{
    public class clsBeTrans_oc_ti : ICloneable
    {
        [Column("IdTipoIngresoOC")]
        [DisplayName("IdTipoIngresoOC")]
        public int IdTipoIngresoOC { get; set; } = 0;

        [Column("Nombre")]
        [DisplayName("Nombre")]
        public string Nombre { get; set; } = "";

        [Column("es_devolucion")]
        [DisplayName("es_devolucion")]
        public bool Es_devolucion { get; set; } = false;

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

        [Column("control_poliza")]
        [DisplayName("control_poliza")]
        public bool Control_poliza { get; set; } = false;

        [Column("requerir_documento_ref")]
        [DisplayName("requerir_documento_ref")]
        public bool Requerir_documento_ref { get; set; } = false;

        [Column("es_poliza_consolidada")]
        [DisplayName("es_poliza_consolidada")]
        public bool Es_poliza_consolidada { get; set; } = false;

        [Column("genera_tarea_ingreso")]
        [DisplayName("genera_tarea_ingreso")]
        public bool Genera_tarea_ingreso { get; set; } = false;

        [Column("requerir_proveedor_es_bodega_wms")]
        [DisplayName("requerir_proveedor_es_bodega_wms")]
        public bool Requerir_proveedor_es_bodega_wms { get; set; } = false;

        [Column("requerir_documento_ref_wms")]
        [DisplayName("requerir_documento_ref_wms")]
        public bool Requerir_documento_ref_wms { get; set; } = false;

        [Column("requerir_ubic_rec_ingreso")]
        [DisplayName("requerir_ubic_rec_ingreso")]
        public bool Requerir_ubic_rec_ingreso { get; set; } = false;

        [Column("exigir_campo_referencia")]
        [DisplayName("exigir_campo_referencia")]
        public bool Exigir_campo_referencia { get; set; } = false;

        [Column("marcar_registros_enviados_mi3")]
        [DisplayName("marcar_registros_enviados_mi3")]
        public bool Marcar_registros_enviados_mi3 { get; set; } = false;

        [Column("preguntar_en_backorder")]
        [DisplayName("preguntar_en_backorder")]
        public bool Preguntar_en_backorder { get; set; } = false;

        [Column("bloquear_lotes")]
        [DisplayName("bloquear_lotes")]
        public bool Bloquear_lotes { get; set; } = false;

        [Column("permitir_excedente_lotes")]
        [DisplayName("permitir_excedente_lotes")]
        public bool Permitir_excedente_lotes { get; set; } = false;

        [Column("permitir_vencido_ingreso")]
        [DisplayName("permitir_vencido_ingreso")]
        public bool Permitir_vencido_ingreso { get; set; } = false;

        [Column("es_importacion")]
        [DisplayName("es_importacion")]
        public bool Es_importacion { get; set; } = false;

        public clsBeTrans_oc_ti() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}