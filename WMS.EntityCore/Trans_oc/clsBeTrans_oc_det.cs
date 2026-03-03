using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.EntityCore.Producto;
using WMSWebAPI.Be;

namespace WMS.EntityCore.Trans_oc
{
    public class clsBeTrans_oc_det : ICloneable
    {
        [Column("IdOrdenCompraEnc")]
        [DisplayName("IdOrdenCompraEnc")]
        public int IdOrdenCompraEnc { get; set; } = 0;

        [Column("IdOrdenCompraDet")]
        [DisplayName("IdOrdenCompraDet")]
        public int IdOrdenCompraDet { get; set; } = 0;

        [Column("IdProductoBodega")]
        [DisplayName("IdProductoBodega")]
        public int IdProductoBodega { get; set; } = 0;

        [Column("IdArancel")]
        [DisplayName("IdArancel")]
        public int IdArancel { get; set; } = 0;

        [Column("IdPresentacion")]
        [DisplayName("IdPresentacion")]
        public int IdPresentacion { get; set; } = 0;

        [Column("IdUnidadMedidaBasica")]
        [DisplayName("IdUnidadMedidaBasica")]
        public int IdUnidadMedidaBasica { get; set; } = 0;

        [Column("IdMotivoDevolucion")]
        [DisplayName("IdMotivoDevolucion")]
        public int IdMotivoDevolucion { get; set; } = 0;

        [Column("No_Linea")]
        [DisplayName("No_Linea")]
        public int No_Linea { get; set; } = 0;

        [Column("nombre_producto")]
        [DisplayName("nombre_producto")]
        public string Nombre_producto { get; set; } = "";

        [Column("nombre_presentacion")]
        [DisplayName("nombre_presentacion")]
        public string Nombre_presentacion { get; set; } = "";

        [Column("nombre_arancel")]
        [DisplayName("nombre_arancel")]
        public string Nombre_arancel { get; set; } = "";

        [Column("porcentaje_arancel")]
        [DisplayName("porcentaje_arancel")]
        public double Porcentaje_arancel { get; set; } = 0;

        [Column("nombre_unidad_medida_basica")]
        [DisplayName("nombre_unidad_medida_basica")]
        public string Nombre_unidad_medida_basica { get; set; } = "";

        [Column("cantidad")]
        [DisplayName("cantidad")]
        public double Cantidad { get; set; } = 0;

        [Column("cantidad_recibida")]
        [DisplayName("cantidad_recibida")]
        public double Cantidad_recibida { get; set; } = 0;

        [Column("costo")]
        [DisplayName("costo")]
        public double Costo { get; set; } = 0;

        [Column("total_linea")]
        [DisplayName("total_linea")]
        public double Total_linea { get; set; } = 0;

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

        [Column("peso")]
        [DisplayName("peso")]
        public double Peso { get; set; } = 0;

        [Column("peso_recibido")]
        [DisplayName("peso_recibido")]
        public double Peso_recibido { get; set; } = 0;

        [Column("atributo_variante_1")]
        [DisplayName("atributo_variante_1")]
        public string Atributo_variante_1 { get; set; } = "";

        [Column("codigo_producto")]
        [DisplayName("codigo_producto")]
        public string Codigo_producto { get; set; } = "";

        [Column("valor_aduana")]
        [DisplayName("valor_aduana")]
        public double Valor_aduana { get; set; } = 0;

        [Column("valor_fob")]
        [DisplayName("valor_fob")]
        public double Valor_fob { get; set; } = 0;

        [Column("valor_iva")]
        [DisplayName("valor_iva")]
        public double Valor_iva { get; set; } = 0;

        [Column("valor_dai")]
        [DisplayName("valor_dai")]
        public double Valor_dai { get; set; } = 0;

        [Column("valor_seguro")]
        [DisplayName("valor_seguro")]
        public double Valor_seguro { get; set; } = 0;

        [Column("valor_flete")]
        [DisplayName("valor_flete")]
        public double Valor_flete { get; set; } = 0;

        [Column("peso_neto")]
        [DisplayName("peso_neto")]
        public double Peso_neto { get; set; } = 0;

        [Column("peso_bruto")]
        [DisplayName("peso_bruto")]
        public double Peso_bruto { get; set; } = 0;

        [Column("IdPropietarioBodega")]
        [DisplayName("IdPropietarioBodega")]
        public int IdPropietarioBodega { get; set; } = 0;

        [Column("nombre_propietario")]
        [DisplayName("nombre_propietario")]
        public string Nombre_propietario { get; set; } = "";

        [Column("IdOrdenCompraDetPadre")]
        [DisplayName("IdOrdenCompraDetPadre")]
        public int IdOrdenCompraDetPadre { get; set; } = 0;

        [Column("IdEmbarcador")]
        [DisplayName("IdEmbarcador")]
        public int IdEmbarcador { get; set; } = 0;
        public clsBeProducto_presentacion Presentacion { get; set; } = new clsBeProducto_presentacion();
        public bool IsNew { get; set; }
        public bool ExisteEnRecepcion { get; set; }
        public decimal FactorPresentacion { get; set; }
        public clsBeArancel Arancel { get; set; } = new clsBeArancel();
        public clsBeProducto Producto { get; set; } = new clsBeProducto();
        public clsBeUnidad_medida UnidadMedida { get; set; } = new clsBeUnidad_medida();
        public int RowIndex { get; set; } = 0;
        public List<clsBeTrans_oc_det> lProductosHijosKit { get; set; } = new List<clsBeTrans_oc_det>();
        public string Nombre_Propietario { get; set; } = "";
        public clsBeTalla Talla { get; set; } = new clsBeTalla();
        public clsBeColor Color { get; set; } = new clsBeColor();
        public int IdProductoTallaColor { get; set; } = 0;
        public string Nombre_Embarcador { get; set; } = "";
        public string Nombre_Clasificacion { get; set; } = "";
        public int Camas_Tarima { get; set; } = 0;
        public int Cajas_Cama { get; set; } = 0;
        public clsBeTrans_oc_det() { }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}