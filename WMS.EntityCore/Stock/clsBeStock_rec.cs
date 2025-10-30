using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.EntityCore.Producto;

namespace WMS.EntityCore.Stock
{
    public class clsBeStock_rec : ICloneable
    {
        [Column("IdStockRec")]
        [DisplayName("IdStockRec")]
        public int IdStockRec { get; set; } = 0;

        [Column("IdPropietarioBodega")]
        [DisplayName("IdPropietarioBodega")]
        public int IdPropietarioBodega { get; set; } = 0;

        [Column("IdProductoBodega")]
        [DisplayName("IdProductoBodega")]
        public int IdProductoBodega { get; set; } = 0;

        [Column("IdProductoEstado")]
        [DisplayName("IdProductoEstado")]
        public int IdProductoEstado { get; set; } = 0;

        [Column("IdPresentacion")]
        [DisplayName("IdPresentacion")]
        public int IdPresentacion { get; set; } = 0;

        [Column("IdUnidadMedida")]
        [DisplayName("IdUnidadMedida")]
        public int IdUnidadMedida { get; set; } = 0;

        [Column("IdUbicacion")]
        [DisplayName("IdUbicacion")]
        public int IdUbicacion { get; set; } = 0;

        [Column("IdUbicacion_anterior")]
        [DisplayName("IdUbicacion_anterior")]
        public int IdUbicacion_anterior { get; set; } = 0;

        [Column("IdRecepcionEnc")]
        [DisplayName("IdRecepcionEnc")]
        public int IdRecepcionEnc { get; set; } = 0;

        [Column("IdRecepcionDet")]
        [DisplayName("IdRecepcionDet")]
        public int IdRecepcionDet { get; set; } = 0;

        [Column("IdPedidoEnc")]
        [DisplayName("IdPedidoEnc")]
        public int IdPedidoEnc { get; set; } = 0;

        [Column("IdPickingEnc")]
        [DisplayName("IdPickingEnc")]
        public int IdPickingEnc { get; set; } = 0;

        [Column("IdDespachoEnc")]
        [DisplayName("IdDespachoEnc")]
        public int IdDespachoEnc { get; set; } = 0;

        [Column("lote")]
        [DisplayName("lote")]
        public string Lote { get; set; } = "";

        [Column("lic_plate")]
        [DisplayName("lic_plate")]
        public string Lic_plate { get; set; } = "";

        [Column("serial")]
        [DisplayName("serial")]
        public string Serial { get; set; } = "";

        [Column("cantidad")]
        [DisplayName("cantidad")]
        public double Cantidad { get; set; } = 0;

        [Column("fecha_ingreso")]
        [DisplayName("fecha_ingreso")]
        public DateTime Fecha_ingreso { get; set; } = DateTime.Now;

        [Column("fecha_vence")]
        [DisplayName("fecha_vence")]
        public DateTime Fecha_vence { get; set; } = DateTime.Now;

        [Column("uds_lic_plate")]
        [DisplayName("uds_lic_plate")]
        public double Uds_lic_plate { get; set; } = 0;

        [Column("no_bulto")]
        [DisplayName("no_bulto")]
        public int No_bulto { get; set; } = 0;

        [Column("fecha_manufactura")]
        [DisplayName("fecha_manufactura")]
        public DateTime Fecha_manufactura { get; set; } = DateTime.Now;

        [Column("añada")]
        [DisplayName("añada")]
        public int Añada { get; set; } = 0;

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

        [Column("temperatura")]
        [DisplayName("temperatura")]
        public double Temperatura { get; set; } = 0;

        [Column("regularizado")]
        [DisplayName("regularizado")]
        public bool Regularizado { get; set; } = false;

        [Column("fecha_regularizacion")]
        [DisplayName("fecha_regularizacion")]
        public DateTime Fecha_regularizacion { get; set; } = DateTime.Now;

        [Column("no_linea")]
        [DisplayName("no_linea")]
        public int No_linea { get; set; } = 0;

        [Column("atributo_variante_1")]
        [DisplayName("atributo_variante_1")]
        public string Atributo_variante_1 { get; set; } = "";

        [Column("impreso")]
        [DisplayName("impreso")]
        public bool Impreso { get; set; } = false;

        [Column("IdBodega")]
        [DisplayName("IdBodega")]
        public int IdBodega { get; set; } = 0;

        [Column("pallet_no_estandar")]
        [DisplayName("pallet_no_estandar")]
        public bool Pallet_no_estandar { get; set; } = false;
        public bool IsNew { get; set; } = true;
        public bool ProductoValidado { get; set; }
        public clsBeProducto_presentacion Presentacion { get; set; } = new clsBeProducto_presentacion();
        public clsBeProducto_estado ProductoEstado { get; set; } = new clsBeProducto_estado();
        public double CantidadEnStock { get; set; } = 0;
        public double PesoEnStock { get; set; } = 0;
        public double Cantidad_Nav { get; set; } = 0;
        public int IdProductoTallaColor { get; set; } = 0;
        public string? Talla { get; set; }
        public string? Color { get; set; }

        public clsBeStock_rec() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}