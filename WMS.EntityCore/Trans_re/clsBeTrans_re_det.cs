using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Trans_re
{
    public class clsBeTrans_re_det : ICloneable
    {
        [Column("IdRecepcionDet")]
        [DisplayName("IdRecepcionDet")]
        public int IdRecepcionDet { get; set; } = 0;

        [Column("IdRecepcionEnc")]
        [DisplayName("IdRecepcionEnc")]
        public int IdRecepcionEnc { get; set; } = 0;

        [Column("IdProductoBodega")]
        [DisplayName("IdProductoBodega")]
        public int IdProductoBodega { get; set; } = 0;

        [Column("IdPresentacion")]
        [DisplayName("IdPresentacion")]
        public int IdPresentacion { get; set; } = 0;

        [Column("IdUnidadMedida")]
        [DisplayName("IdUnidadMedida")]
        public int IdUnidadMedida { get; set; } = 0;

        [Column("IdProductoEstado")]
        [DisplayName("IdProductoEstado")]
        public int IdProductoEstado { get; set; } = 0;

        [Column("IdOperadorBodega")]
        [DisplayName("IdOperadorBodega")]
        public int IdOperadorBodega { get; set; } = 0;

        [Column("IdMotivoDevolucion")]
        [DisplayName("IdMotivoDevolucion")]
        public int IdMotivoDevolucion { get; set; } = 0;

        [Column("No_Linea")]
        [DisplayName("No_Linea")]
        public int No_Linea { get; set; } = 0;

        [Column("cantidad_recibida")]
        [DisplayName("cantidad_recibida")]
        public double Cantidad_recibida { get; set; } = 0;

        [Column("nombre_producto")]
        [DisplayName("nombre_producto")]
        public string Nombre_producto { get; set; } = "";

        [Column("nombre_presentacion")]
        [DisplayName("nombre_presentacion")]
        public string Nombre_presentacion { get; set; } = "";

        [Column("nombre_unidad_medida")]
        [DisplayName("nombre_unidad_medida")]
        public string Nombre_unidad_medida { get; set; } = "";

        [Column("nombre_producto_estado")]
        [DisplayName("nombre_producto_estado")]
        public string Nombre_producto_estado { get; set; } = "";

        [Column("lote")]
        [DisplayName("lote")]
        public string Lote { get; set; } = "";

        [Column("fecha_vence")]
        [DisplayName("fecha_vence")]
        public DateTime Fecha_vence { get; set; } = DateTime.Now;

        [Column("fecha_ingreso")]
        [DisplayName("fecha_ingreso")]
        public DateTime Fecha_ingreso { get; set; } = DateTime.Now;

        [Column("peso")]
        [DisplayName("peso")]
        public double Peso { get; set; } = 0;

        [Column("peso_estadistico")]
        [DisplayName("peso_estadistico")]
        public double Peso_estadistico { get; set; } = 0;

        [Column("peso_minimo")]
        [DisplayName("peso_minimo")]
        public double Peso_minimo { get; set; } = 0;

        [Column("peso_maximo")]
        [DisplayName("peso_maximo")]
        public double Peso_maximo { get; set; } = 0;

        [Column("peso_unitario")]
        [DisplayName("peso_unitario")]
        public double Peso_unitario { get; set; } = 0;

        [Column("user_agr")]
        [DisplayName("user_agr")]
        public string User_agr { get; set; } = "";

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;

        [Column("observacion")]
        [DisplayName("observacion")]
        public string Observacion { get; set; } = "";

        [Column("añada")]
        [DisplayName("añada")]
        public int Añada { get; set; } = 0;

        [Column("costo")]
        [DisplayName("costo")]
        public double Costo { get; set; } = 0;

        [Column("costo_oc")]
        [DisplayName("costo_oc")]
        public double Costo_oc { get; set; } = 0;

        [Column("costo_estadistico")]
        [DisplayName("costo_estadistico")]
        public double Costo_estadistico { get; set; } = 0;

        [Column("atributo_variante_1")]
        [DisplayName("atributo_variante_1")]
        public string Atributo_variante_1 { get; set; } = "";

        [Column("codigo_producto")]
        [DisplayName("codigo_producto")]
        public string Codigo_producto { get; set; } = "";

        [Column("lic_plate")]
        [DisplayName("lic_plate")]
        public string Lic_plate { get; set; } = "";

        [Column("pallet_no_estandar")]
        [DisplayName("pallet_no_estandar")]
        public bool Pallet_no_estandar { get; set; } = false;

        [Column("IdOrdenCompraEnc")]
        [DisplayName("IdOrdenCompraEnc")]
        public int IdOrdenCompraEnc { get; set; } = 0;

        [Column("IdOrdenCompraDet")]
        [DisplayName("IdOrdenCompraDet")]
        public int IdOrdenCompraDet { get; set; } = 0;

        [Column("IdJornadaSistema")]
        [DisplayName("IdJornadaSistema")]
        public int IdJornadaSistema { get; set; } = 0;
        public clsBeTrans_re_det() { }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}