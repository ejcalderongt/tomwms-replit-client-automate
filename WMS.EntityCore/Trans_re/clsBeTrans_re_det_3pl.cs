using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.EntityCore.Producto;
using WMSWebAPI.Be;

namespace WMS.EntityCore.Trans_re
{
    public class clsBeTrans_re_det_3pl : ICloneable
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

        [Column("Nombre_producto")]
        [DisplayName("Nombre_producto")]
        public string Nombre_producto { get; set; } = "";

        [Column("Nombre_presentacion")]
        [DisplayName("Nombre_presentacion")]
        public string Nombre_presentacion { get; set; } = "";

        [Column("Nombre_unidad_medida")]
        [DisplayName("Nombre_unidad_medida")]
        public string Nombre_unidad_medida { get; set; } = "";

        [Column("Nombre_producto_estado")]
        [DisplayName("Nombre_producto_estado")]
        public string Nombre_producto_estado { get; set; } = "";

        [Column("Lote")]
        [DisplayName("Lote")]
        public string Lote { get; set; } = "";

        [Column("Fecha_vence")]
        [DisplayName("Fecha_vence")]
        public DateTime Fecha_vence { get; set; } = DateTime.Now;

        [Column("Fecha_ingreso")]
        [DisplayName("Fecha_ingreso")]
        public DateTime Fecha_ingreso { get; set; } = DateTime.Now;

        [Column("Peso")]
        [DisplayName("Peso")]
        public double Peso { get; set; } = 0;

        [Column("Peso_Estadistico")]
        [DisplayName("Peso_Estadistico")]
        public double Peso_Estadistico { get; set; } = 0;

        [Column("Peso_Minimo")]
        [DisplayName("Peso_Minimo")]
        public double Peso_Minimo { get; set; } = 0;

        [Column("Peso_Maximo")]
        [DisplayName("Peso_Maximo")]
        public double Peso_Maximo { get; set; } = 0;

        [Column("peso_unitario")]
        [DisplayName("peso_unitario")]
        public double peso_unitario { get; set; } = 0;

        [Column("User_agr")]
        [DisplayName("User_agr")]
        public string User_agr { get; set; } = "";

        [Column("Fec_agr")]
        [DisplayName("Fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;

        [Column("Observacion")]
        [DisplayName("Observacion")]
        public string Observacion { get; set; } = "";

        [Column("Añada")]
        [DisplayName("Añada")]
        public int Añada { get; set; } = 0;

        // No coincide con el JSON proporcionado (usa "Aniada")
        // [Column("añada")]
        // [DisplayName("añada")]
        // public int Añada { get; set; } = 0;

        [Column("Costo")]
        [DisplayName("Costo")]
        public double Costo { get; set; } = 0;

        [Column("Costo_Oc")]
        [DisplayName("Costo_Oc")]
        public double Costo_Oc { get; set; } = 0;

        [Column("Costo_Estadistico")]
        [DisplayName("Costo_Estadistico")]
        public double Costo_Estadistico { get; set; } = 0;

        [Column("Atributo_Variante_1")]
        [DisplayName("Atributo_Variante_1")]
        public string Atributo_Variante_1 { get; set; } = "";

        [Column("Codigo_Producto")]
        [DisplayName("Codigo_Producto")]
        public string Codigo_Producto { get; set; } = "";

        [Column("Lic_plate")]
        [DisplayName("Lic_plate")]
        public string Lic_plate { get; set; } = "";

        [Column("Pallet_No_Estandar")]
        [DisplayName("Pallet_No_Estandar")]
        public bool Pallet_No_Estandar { get; set; } = false;

        [Column("Posiciones")]
        [DisplayName("Posiciones")]
        public int Posiciones { get; set; } = 0;

        [Column("IdOrdenCompraEnc")]
        [DisplayName("IdOrdenCompraEnc")]
        public int IdOrdenCompraEnc { get; set; } = 0;

        [Column("IdOrdenCompraDet")]
        [DisplayName("IdOrdenCompraDet")]
        public int IdOrdenCompraDet { get; set; } = 0;

        [Column("IdJornadaSistema")]
        [DisplayName("IdJornadaSistema")]
        public int IdJornadaSistema { get; set; } = 0;

        public clsBeProducto Producto { get; set; } = new clsBeProducto();
        public clsBeProducto_presentacion Presentacion { get; set; } = new clsBeProducto_presentacion();
        public clsBeProducto_estado ProductoEstado { get; set; } = new clsBeProducto_estado();
        public clsBeUnidad_medida UnidadMedida { get; set; } = new clsBeUnidad_medida();
        public clsBeMotivo_devolucion MotivoDevolucion { get; set; } = new clsBeMotivo_devolucion();

        public bool IsNew { get; set; } = true;
        public bool Control_Peso { get; set; }

        public int IdPropietarioBodega { get; set; }
        public int IdUbicacion { get; set; }
        public int IdUbicacionAnterior { get; set; }

        public DateTime Fecha_Rec { get; set; }
        public DateTime Fecha_tarea { get; set; }
        public DateTime Hora_ini { get; set; }
        public DateTime Hora_Fin { get; set; }

        public string Estado_Rec { get; set; } = "";
        public string UbicacionCompleta { get; set; } = "";

        public double Uds_lic_plate { get; set; } = 0;

        public string Host { get; set; } = "";

        // No existen en el JSON proporcionado (detalle[0])
        // public clsBeTalla Talla { get; set; } = new clsBeTalla();
        // public clsBeColor Color { get; set; } = new clsBeColor();
        // public int IdProductoTallaColor { get; set; } = 0;

        public clsBeTrans_re_det_3pl() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}