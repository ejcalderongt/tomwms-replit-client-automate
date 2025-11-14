using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.EntityCore.Producto;

namespace WMSWebAPI.Be
{
    public class clsBeTrans_oc_det_lote : ICloneable
    {
        [Column("IdOrdenCompraEnc")]
        [DisplayName("IdOrdenCompraEnc")]
        public int IdOrdenCompraEnc { get; set; } = 0;

        [Column("IdOrdenCompraDet")]
        [DisplayName("IdOrdenCompraDet")]
        public int IdOrdenCompraDet { get; set; } = 0;

        [Column("IdOrdenCompraDetLote")]
        [DisplayName("IdOrdenCompraDetLote")]
        public int IdOrdenCompraDetLote { get; set; } = 0;

        [Column("IdProductoBodega")]
        [DisplayName("IdProductoBodega")]
        public int IdProductoBodega { get; set; } = 0;

        [Column("no_linea")]
        [DisplayName("no_linea")]
        public int No_linea { get; set; } = 0;

        [Column("codigo_producto")]
        [DisplayName("codigo_producto")]
        public string Codigo_producto { get; set; } = "";

        [Column("cantidad")]
        [DisplayName("cantidad")]
        public double Cantidad { get; set; } = 0;

        [Column("cantidad_recibida")]
        [DisplayName("cantidad_recibida")]
        public double Cantidad_recibida { get; set; } = 0;

        [Column("lote")]
        [DisplayName("lote")]
        public string Lote { get; set; } = "";

        [Column("fecha_vence")]
        [DisplayName("fecha_vence")]
        public DateTime Fecha_vence { get; set; } = DateTime.Now;

        [Column("lic_plate")]
        [DisplayName("lic_plate")]
        public string Lic_plate { get; set; } = "";

        [Column("Ubicacion")]
        [DisplayName("Ubicacion")]
        public string Ubicacion { get; set; } = "";

        [Column("IdPresentacion")]
        [DisplayName("IdPresentacion")]
        public int IdPresentacion { get; set; } = 0;

        [Column("IdUnidadMedidaBasica")]
        [DisplayName("IdUnidadMedidaBasica")]
        public int IdUnidadMedidaBasica { get; set; } = 0;

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

        [Column("reclasificar")]
        [DisplayName("reclasificar")]
        public bool Reclasificar { get; set; } = false;

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("no_documento")]
        [DisplayName("no_documento")]
        public string No_documento { get; set; } = "";
        public clsBeProducto_presentacion Presentacion { get; set; } = new clsBeProducto_presentacion();
        public clsBeUnidad_medida UnidadMedida { get; set; } = new clsBeUnidad_medida();
        public int IdProductoTallaColor { get; set; } = 0;
        public string Talla { get; set; } = "";
        public string Color { get; set; } = "";

        public clsBeTrans_oc_det_lote() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
