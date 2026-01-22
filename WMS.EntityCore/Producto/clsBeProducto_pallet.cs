using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeProducto_pallet : ICloneable
    {
        [Column("IdPallet")]
        [DisplayName("IdPallet")]
        public int IdPallet { get; set; } = 0;

        [Column("IdPropietarioBodega")]
        [DisplayName("IdPropietarioBodega")]
        public int IdPropietarioBodega { get; set; } = 0;

        [Column("IdProductoBodega")]
        [DisplayName("IdProductoBodega")]
        public int IdProductoBodega { get; set; } = 0;

        [Column("IdPresentacion")]
        [DisplayName("IdPresentacion")]
        public int IdPresentacion { get; set; } = 0;

        [Column("IdOperadorBodega")]
        [DisplayName("IdOperadorBodega")]
        public int IdOperadorBodega { get; set; } = 0;

        [Column("IdImpresora")]
        [DisplayName("IdImpresora")]
        public int IdImpresora { get; set; } = 0;

        [Column("IdRecepcionEnc")]
        [DisplayName("IdRecepcionEnc")]
        public int IdRecepcionEnc { get; set; } = 0;

        [Column("codigo_barra")]
        [DisplayName("codigo_barra")]
        public string Codigo_barra { get; set; } = "";

        [Column("cantidad")]
        [DisplayName("cantidad")]
        public double Cantidad { get; set; } = 0;

        [Column("lote")]
        [DisplayName("lote")]
        public string Lote { get; set; } = "";

        [Column("Impreso")]
        [DisplayName("Impreso")]
        public bool Impreso { get; set; } = false;

        [Column("Reimpresiones")]
        [DisplayName("Reimpresiones")]
        public int Reimpresiones { get; set; } = 0;

        [Column("fecha_vence")]
        [DisplayName("fecha_vence")]
        public DateTime Fecha_vence { get; set; } = DateTime.Now;

        [Column("fecha_ingreso")]
        [DisplayName("fecha_ingreso")]
        public DateTime Fecha_ingreso { get; set; } = DateTime.Now;

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

        [Column("IdRecepcionDet")]
        [DisplayName("IdRecepcionDet")]
        public int IdRecepcionDet { get; set; } = 0;

        [Column("codigo_producto")]
        [DisplayName("codigo_producto")]
        public string Codigo_producto { get; set; } = "";
        public bool IsNew { get; set; } = true;

        public clsBeProducto_pallet() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
