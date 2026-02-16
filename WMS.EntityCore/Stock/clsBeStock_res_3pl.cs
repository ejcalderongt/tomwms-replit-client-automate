using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeStock_res_3pl : ICloneable
    {
        [Column("IdStockRes")]
        [DisplayName("IdStockRes")]
        public int IdStockRes { get; set; } = 0;

        [Column("IdTransaccion")]
        [DisplayName("IdTransaccion")]
        public int IdTransaccion { get; set; } = 0;

        [Column("Indicador")]
        [DisplayName("Indicador")]
        public string Indicador { get; set; } = "";

        [Column("IdPedidoDet")]
        [DisplayName("IdPedidoDet")]
        public int IdPedidoDet { get; set; } = 0;

        [Column("IdStock")]
        [DisplayName("IdStock")]
        public int IdStock { get; set; } = 0;

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

        [Column("ubicacion_ant")]
        [DisplayName("ubicacion_ant")]
        public string Ubicacion_ant { get; set; } = "";

        [Column("IdRecepcion")]
        [DisplayName("IdRecepcion")]
        public int IdRecepcion { get; set; } = 0;

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

        [Column("peso")]
        [DisplayName("peso")]
        public double Peso { get; set; } = 0;

        [Column("estado")]
        [DisplayName("estado")]
        public string Estado { get; set; } = "";

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

        [Column("IdPicking")]
        [DisplayName("IdPicking")]
        public int IdPicking { get; set; } = 0;

        [Column("IdPedido")]
        [DisplayName("IdPedido")]
        public int IdPedido { get; set; } = 0;

        [Column("IdDespacho")]
        [DisplayName("IdDespacho")]
        public int IdDespacho { get; set; } = 0;

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

        [Column("host")]
        [DisplayName("host")]
        public string Host { get; set; } = "";

        [Column("añada")]
        [DisplayName("añada")]
        public int Añada { get; set; } = 0;

        [Column("fecha_manufactura")]
        [DisplayName("fecha_manufactura")]
        public DateTime Fecha_manufactura { get; set; } = DateTime.Now;

        [Column("IdBodega")]
        [DisplayName("IdBodega")]
        public int IdBodega { get; set; } = 0;

        [Column("pallet_no_estandar")]
        [DisplayName("pallet_no_estandar")]
        public bool Pallet_no_estandar { get; set; } = false;
        public string Atributo_Variante_1 { get; set; } = "";
        public bool Control_Ultimo_Lote { get; set; } = false;
        public string? Ultimo_Lote { get; set; } = "";
        public string Codigo_Producto { get; set; } = "";
        public string No_Pedido { get; set; } = "";
        public int IdUbicacionAbastecerCon { get; set; } = 0;
      
        public clsBeStock_res_3pl() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}