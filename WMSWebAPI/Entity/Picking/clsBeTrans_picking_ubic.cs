using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeTrans_picking_ubic : ICloneable
    {
        [Column("IdPickingUbic")]
        [DisplayName("IdPickingUbic")]
        public int IdPickingUbic { get; set; } = 0;

        [Column("IdPickingEnc")]
        [DisplayName("IdPickingEnc")]
        public int IdPickingEnc { get; set; } = 0;

        [Column("IdPickingDet")]
        [DisplayName("IdPickingDet")]
        public int IdPickingDet { get; set; } = 0;

        [Column("IdUbicacion")]
        [DisplayName("IdUbicacion")]
        public int IdUbicacion { get; set; } = 0;

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

        [Column("IdUbicacionAnterior")]
        [DisplayName("IdUbicacionAnterior")]
        public int IdUbicacionAnterior { get; set; } = 0;

        [Column("IdRecepcion")]
        [DisplayName("IdRecepcion")]
        public int IdRecepcion { get; set; } = 0;

        [Column("lote")]
        [DisplayName("lote")]
        public string Lote { get; set; } = "";

        [Column("fecha_vence")]
        [DisplayName("fecha_vence")]
        public DateTime Fecha_vence { get; set; } = DateTime.Now;

        [Column("fecha_minima")]
        [DisplayName("fecha_minima")]
        public DateTime Fecha_minima { get; set; } = DateTime.Now;

        [Column("serial")]
        [DisplayName("serial")]
        public string Serial { get; set; } = "";

        [Column("lic_plate")]
        [DisplayName("lic_plate")]
        public string Lic_plate { get; set; } = "";

        [Column("acepto")]
        [DisplayName("acepto")]
        public bool Acepto { get; set; } = false;

        [Column("peso_solicitado")]
        [DisplayName("peso_solicitado")]
        public double Peso_solicitado { get; set; } = 0;

        [Column("peso_recibido")]
        [DisplayName("peso_recibido")]
        public double Peso_recibido { get; set; } = 0;

        [Column("peso_verificado")]
        [DisplayName("peso_verificado")]
        public double Peso_verificado { get; set; } = 0;

        [Column("peso_despachado")]
        [DisplayName("peso_despachado")]
        public double Peso_despachado { get; set; } = 0;

        [Column("cantidad_solicitada")]
        [DisplayName("cantidad_solicitada")]
        public double Cantidad_solicitada { get; set; } = 0;

        [Column("cantidad_recibida")]
        [DisplayName("cantidad_recibida")]
        public double Cantidad_recibida { get; set; } = 0;

        [Column("cantidad_verificada")]
        [DisplayName("cantidad_verificada")]
        public double Cantidad_verificada { get; set; } = 0;

        [Column("encontrado")]
        [DisplayName("encontrado")]
        public bool Encontrado { get; set; } = false;

        [Column("dañado_verificacion")]
        [DisplayName("dañado_verificacion")]
        public bool Dañado_verificacion { get; set; } = false;

        [Column("fecha_real_vence")]
        [DisplayName("fecha_real_vence")]
        public DateTime Fecha_real_vence { get; set; } = DateTime.Now;

        [Column("no_packing")]
        [DisplayName("no_packing")]
        public string No_packing { get; set; } = "";

        [Column("fecha_picking")]
        [DisplayName("fecha_picking")]
        public DateTime Fecha_picking { get; set; } = DateTime.Now;

        [Column("fecha_verificado")]
        [DisplayName("fecha_verificado")]
        public DateTime Fecha_verificado { get; set; } = DateTime.Now;

        [Column("fecha_packing")]
        [DisplayName("fecha_packing")]
        public DateTime Fecha_packing { get; set; } = DateTime.Now;

        [Column("fecha_despachado")]
        [DisplayName("fecha_despachado")]
        public DateTime Fecha_despachado { get; set; } = DateTime.Now;

        [Column("cantidad_despachada")]
        [DisplayName("cantidad_despachada")]
        public double Cantidad_despachada { get; set; } = 0;

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

        [Column("IdPedidoDet")]
        [DisplayName("IdPedidoDet")]
        public int IdPedidoDet { get; set; } = 0;

        [Column("dañado_picking")]
        [DisplayName("dañado_picking")]
        public bool Dañado_picking { get; set; } = false;

        [Column("IdStockRes")]
        [DisplayName("IdStockRes")]
        public int IdStockRes { get; set; } = 0;

        [Column("lic_plate_reemplazo")]
        [DisplayName("lic_plate_reemplazo")]
        public string Lic_plate_reemplazo { get; set; } = "";

        [Column("IdUbicacion_reemplazo")]
        [DisplayName("IdUbicacion_reemplazo")]
        public int IdUbicacion_reemplazo { get; set; } = 0;

        [Column("IdStock_reemplazo")]
        [DisplayName("IdStock_reemplazo")]
        public int IdStock_reemplazo { get; set; } = 0;

        [Column("IdBodega")]
        [DisplayName("IdBodega")]
        public int IdBodega { get; set; } = 0;

        [Column("IdOperadorBodega_Pickeo")]
        [DisplayName("IdOperadorBodega_Pickeo")]
        public int IdOperadorBodega_Pickeo { get; set; } = 0;

        [Column("IdOperadorBodega_Verifico")]
        [DisplayName("IdOperadorBodega_Verifico")]
        public int IdOperadorBodega_Verifico { get; set; } = 0;

        [Column("IdPedidoEnc")]
        [DisplayName("IdPedidoEnc")]
        public int IdPedidoEnc { get; set; } = 0;

        [Column("no_encontrado")]
        [DisplayName("no_encontrado")]
        public bool No_encontrado { get; set; } = false;

        [Column("IdUbicacionTemporal")]
        [DisplayName("IdUbicacionTemporal")]
        public int IdUbicacionTemporal { get; set; } = 0;

        [Column("IdOperadorBodega_Asignado")]
        [DisplayName("IdOperadorBodega_Asignado")]
        public int IdOperadorBodega_Asignado { get; set; } = 0;

        public clsBeTrans_picking_ubic() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}