using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Entity.Producto
{
    public class clsBeProducto : ICloneable
    {
        [Column("IdProducto")]
        [DisplayName("IdProducto")]
        public int IdProducto { get; set; } = 0;

        [Column("IdPropietario")]
        [DisplayName("IdPropietario")]
        public int IdPropietario { get; set; } = 0;

        [Column("IdClasificacion")]
        [DisplayName("IdClasificacion")]
        public int IdClasificacion { get; set; } = 0;

        [Column("IdFamilia")]
        [DisplayName("IdFamilia")]
        public int IdFamilia { get; set; } = 0;

        [Column("IdMarca")]
        [DisplayName("IdMarca")]
        public int IdMarca { get; set; } = 0;

        [Column("IdTipoProducto")]
        [DisplayName("IdTipoProducto")]
        public int IdTipoProducto { get; set; } = 0;

        [Column("IdUnidadMedidaBasica")]
        [DisplayName("IdUnidadMedidaBasica")]
        public int IdUnidadMedidaBasica { get; set; } = 0;

        [Column("IdCamara")]
        [DisplayName("IdCamara")]
        public int IdCamara { get; set; } = 0;

        [Column("IdTipoRotacion")]
        [DisplayName("IdTipoRotacion")]
        public int IdTipoRotacion { get; set; } = 0;

        [Column("IdPerfilSerializado")]
        [DisplayName("IdPerfilSerializado")]
        public int IdPerfilSerializado { get; set; } = 0;

        [Column("IdIndiceRotacion")]
        [DisplayName("IdIndiceRotacion")]
        public int IdIndiceRotacion { get; set; } = 0;

        [Column("IdSimbologia")]
        [DisplayName("IdSimbologia")]
        public int IdSimbologia { get; set; } = 0;

        [Column("IdArancel")]
        [DisplayName("IdArancel")]
        public int IdArancel { get; set; } = 0;

        [Column("codigo")]
        [DisplayName("codigo")]
        public string codigo { get; set; } = "";

        [Column("nombre")]
        [DisplayName("nombre")]
        public string nombre { get; set; } = "";

        [Column("codigo_barra")]
        [DisplayName("codigo_barra")]
        public string codigo_barra { get; set; } = "";

        [Column("precio")]
        [DisplayName("precio")]
        public decimal precio { get; set; } = 0;

        [Column("existencia_min")]
        [DisplayName("existencia_min")]
        public decimal existencia_min { get; set; } = 0;

        [Column("existencia_max")]
        [DisplayName("existencia_max")]
        public decimal existencia_max { get; set; } = 0;

        [Column("costo")]
        [DisplayName("costo")]
        public decimal costo { get; set; } = 0;

        [Column("peso_referencia")]
        [DisplayName("peso_referencia")]
        public decimal peso_referencia { get; set; } = 0;

        [Column("peso_tolerancia")]
        [DisplayName("peso_tolerancia")]
        public decimal peso_tolerancia { get; set; } = 0;

        [Column("temperatura_referencia")]
        [DisplayName("temperatura_referencia")]
        public decimal temperatura_referencia { get; set; } = 0;

        [Column("temperatura_tolerancia")]
        [DisplayName("temperatura_tolerancia")]
        public decimal temperatura_tolerancia { get; set; } = 0;

        [Column("activo")]
        [DisplayName("activo")]
        public bool activo { get; set; } = false;

        [Column("serializado")]
        [DisplayName("serializado")]
        public bool serializado { get; set; } = false;

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

        [Column("peso_recepcion")]
        [DisplayName("peso_recepcion")]
        public bool peso_recepcion { get; set; } = false;

        [Column("peso_despacho")]
        [DisplayName("peso_despacho")]
        public bool peso_despacho { get; set; } = false;

        [Column("temperatura_recepcion")]
        [DisplayName("temperatura_recepcion")]
        public bool temperatura_recepcion { get; set; } = false;

        [Column("temperatura_despacho")]
        [DisplayName("temperatura_despacho")]
        public bool temperatura_despacho { get; set; } = false;

        [Column("materia_prima")]
        [DisplayName("materia_prima")]
        public bool materia_prima { get; set; } = false;

        [Column("kit")]
        [DisplayName("kit")]
        public bool kit { get; set; } = false;

        [Column("tolerancia")]
        [DisplayName("tolerancia")]
        public int tolerancia { get; set; } = 0;

        [Column("ciclo_vida")]
        [DisplayName("ciclo_vida")]
        public int ciclo_vida { get; set; } = 0;

        [Column("user_agr")]
        [DisplayName("user_agr")]
        public string user_agr { get; set; } = "";

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime fec_agr { get; set; } = DateTime.Now;

        [Column("user_mod")]
        [DisplayName("user_mod")]
        public string user_mod { get; set; } = "";

        [Column("fec_mod")]
        [DisplayName("fec_mod")]
        public DateTime fec_mod { get; set; } = DateTime.Now;

        [Column("imagen")]
        [DisplayName("imagen")]
        public byte[]? imagen { get; set; } = null;

        [Column("noserie")]
        [DisplayName("noserie")]
        public string noserie { get; set; } = "";

        [Column("noparte")]
        [DisplayName("noparte")]
        public string noparte { get; set; } = "";

        [Column("fechamanufactura")]
        [DisplayName("fechamanufactura")]
        public bool fechamanufactura { get; set; } = false;

        [Column("capturar_aniada")]
        [DisplayName("capturar_aniada")]
        public bool capturar_aniada { get; set; } = false;

        [Column("control_peso")]
        [DisplayName("control_peso")]
        public bool control_peso { get; set; } = false;

        [Column("captura_arancel")]
        [DisplayName("captura_arancel")]
        public bool captura_arancel { get; set; } = false;

        [Column("es_hardware")]
        [DisplayName("es_hardware")]
        public bool es_hardware { get; set; } = false;

        [Column("largo")]
        [DisplayName("largo")]
        public decimal largo { get; set; } = 0;

        [Column("alto")]
        [DisplayName("alto")]
        public decimal alto { get; set; } = 0;

        [Column("ancho")]
        [DisplayName("ancho")]
        public decimal ancho { get; set; } = 0;

        [Column("IdUnidadMedidaCobro")]
        [DisplayName("IdUnidadMedidaCobro")]
        public int IdUnidadMedidaCobro { get; set; } = 0;

        [Column("IdTipoEtiqueta")]
        [DisplayName("IdTipoEtiqueta")]
        public int IdTipoEtiqueta { get; set; } = 0;

        [Column("dias_inventario_promedio")]
        [DisplayName("dias_inventario_promedio")]
        public int dias_inventario_promedio { get; set; } = 0;

        [Column("IDPRODUCTOPARAMETROA")]
        [DisplayName("IDPRODUCTOPARAMETROA")]
        public int IDPRODUCTOPARAMETROA { get; set; } = 0;

        [Column("IDPRODUCTOPARAMETROB")]
        [DisplayName("IDPRODUCTOPARAMETROB")]
        public int IDPRODUCTOPARAMETROB { get; set; } = 0;

        [Column("IdTipoManufactura")]
        [DisplayName("IdTipoManufactura")]
        public int IdTipoManufactura { get; set; } = 0;

        public clsBeProducto() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}