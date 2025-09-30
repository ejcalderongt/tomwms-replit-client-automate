using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace WMS.EntityCore.Producto
{
    public class clsBeProductoMi3 : ICloneable
    {

        [Column("IdProducto")]
        [DisplayName("IdProducto")]
        public int IdProducto { get; set; } = 0;

        [Column("IdPropietario")]
        [DisplayName("IdPropietario")]
        public int IdPropietario { get; set; } = 0;

        [Column("CodigoClasificacion")]
        [DisplayName("CodigoClasificacion")]
        public string CodigoClasificacion { get; set; } = "";

        [Column("CodigoFamilia")]
        [DisplayName("CodigoFamilia")]
        public string CodigoFamilia { get; set; } = "";

        [Column("CodigoMarca")]
        [DisplayName("CodigoMarca")]
        public string CodigoMarca { get; set; } = "";

        [Column("CodigoTipoProducto")]
        [DisplayName("CodigoTipoProducto")]
        public string CodigoTipoProducto { get; set; } = "";

        [Column("CodigoUmBas")]
        [DisplayName("CodigoUmBas")]
        public string CodigoUmBas { get; set; } = "";

        [Column("IdTipoRotacion")]
        [DisplayName("IdTipoRotacion")]
        public int IdTipoRotacion { get; set; } = 0;

        [Column("codigo")]
        [DisplayName("codigo")]
        public string codigo { get; set; } = "";

        [Column("nombre")]
        [DisplayName("nombre")]
        public string nombre { get; set; } = "";

        [Column("codigo_barra")]
        [DisplayName("codigo_barra")]
        public string codigo_barra { get; set; } = "";

        [Column("genera_lote")]
        [DisplayName("genera_lote")]
        public bool genera_lote { get; set; } = false;

        [Column("genera_lp_old")]
        [DisplayName("genera_lp_old")]
        public bool genera_lp_old { get; set; } = false;

        [Column("control_vencimiento")]
        [DisplayName("control_vencimiento")]
        public bool control_vencimiento { get; set; } = false;

        [Column("control_lote")]
        [DisplayName("control_lote")]
        public bool control_lote { get; set; } = false;

        [Column("fechamanufactura")]
        [DisplayName("fechamanufactura")]
        public bool fechamanufactura { get; set; } = false;

        [Column("control_peso")]
        [DisplayName("control_peso")]
        public bool control_peso { get; set; } = false;

        [Column("IdTipoEtiqueta")]
        [DisplayName("IdTipoEtiqueta")]
        public int IdTipoEtiqueta { get; set; } = 0;

        public clsBeProductoMi3() { }

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}
