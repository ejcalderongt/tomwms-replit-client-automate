using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Stock
{
    public class clsBeTrans_movimientos : ICloneable
    {
        [Column("IdMovimiento")]
        [DisplayName("IdMovimiento")]
        public int IdMovimiento { get; set; } = 0;

        [Column("IdEmpresa")]
        [DisplayName("IdEmpresa")]
        public int IdEmpresa { get; set; } = 0;

        [Column("IdBodegaOrigen")]
        [DisplayName("IdBodegaOrigen")]
        public int IdBodegaOrigen { get; set; } = 0;

        [Column("IdTransaccion")]
        [DisplayName("IdTransaccion")]
        public int IdTransaccion { get; set; } = 0;

        [Column("IdPropietarioBodega")]
        [DisplayName("IdPropietarioBodega")]
        public int IdPropietarioBodega { get; set; } = 0;

        [Column("IdProductoBodega")]
        [DisplayName("IdProductoBodega")]
        public int IdProductoBodega { get; set; } = 0;

        [Column("IdUbicacionOrigen")]
        [DisplayName("IdUbicacionOrigen")]
        public int IdUbicacionOrigen { get; set; } = 0;

        [Column("IdUbicacionDestino")]
        [DisplayName("IdUbicacionDestino")]
        public int IdUbicacionDestino { get; set; } = 0;

        [Column("IdPresentacion")]
        [DisplayName("IdPresentacion")]
        public int IdPresentacion { get; set; } = 0;

        [Column("IdEstadoOrigen")]
        [DisplayName("IdEstadoOrigen")]
        public int IdEstadoOrigen { get; set; } = 0;

        [Column("IdEstadoDestino")]
        [DisplayName("IdEstadoDestino")]
        public int IdEstadoDestino { get; set; } = 0;

        [Column("IdUnidadMedida")]
        [DisplayName("IdUnidadMedida")]
        public int IdUnidadMedida { get; set; } = 0;

        [Column("IdTipoTarea")]
        [DisplayName("IdTipoTarea")]
        public int IdTipoTarea { get; set; } = 0;

        [Column("IdBodegaDestino")]
        [DisplayName("IdBodegaDestino")]
        public int IdBodegaDestino { get; set; } = 0;

        [Column("IdRecepcion")]
        [DisplayName("IdRecepcion")]
        public int IdRecepcion { get; set; } = 0;

        [Column("cantidad")]
        [DisplayName("cantidad")]
        public double Cantidad { get; set; } = 0;

        [Column("serie")]
        [DisplayName("serie")]
        public string Serie { get; set; } = "";

        [Column("peso")]
        [DisplayName("peso")]
        public double Peso { get; set; } = 0;

        [Column("lote")]
        [DisplayName("lote")]
        public string Lote { get; set; } = "";

        [Column("fecha_vence")]
        [DisplayName("fecha_vence")]
        public DateTime Fecha_vence { get; set; } = DateTime.Now;

        [Column("fecha")]
        [DisplayName("fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Column("barra_pallet")]
        [DisplayName("barra_pallet")]
        public string Barra_pallet { get; set; } = "";

        [Column("hora_ini")]
        [DisplayName("hora_ini")]
        public DateTime Hora_ini { get; set; } = DateTime.Now;

        [Column("hora_fin")]
        [DisplayName("hora_fin")]
        public DateTime Hora_fin { get; set; } = DateTime.Now;

        [Column("fecha_agr")]
        [DisplayName("fecha_agr")]
        public DateTime Fecha_agr { get; set; } = DateTime.Now;

        [Column("usuario_agr")]
        [DisplayName("usuario_agr")]
        public string Usuario_agr { get; set; } = "";

        [Column("cantidad_hist")]
        [DisplayName("cantidad_hist")]
        public double Cantidad_hist { get; set; } = 0;

        [Column("peso_hist")]
        [DisplayName("peso_hist")]
        public double Peso_hist { get; set; } = 0;

        [Column("lic_plate")]
        [DisplayName("lic_plate")]
        public string Lic_plate { get; set; } = "";

        [Column("IdOperadorBodega")]
        [DisplayName("IdOperadorBodega")]
        public int IdOperadorBodega { get; set; } = 0;

        [Column("IdRecepcionDet")]
        [DisplayName("IdRecepcionDet")]
        public int IdRecepcionDet { get; set; } = 0;

        [Column("IdPedidoEnc")]
        [DisplayName("IdPedidoEnc")]
        public int IdPedidoEnc { get; set; } = 0;

        [Column("IdPedidoDet")]
        [DisplayName("IdPedidoDet")]
        public int IdPedidoDet { get; set; } = 0;

        [Column("IdDespachoEnc")]
        [DisplayName("IdDespachoEnc")]
        public int IdDespachoEnc { get; set; } = 0;

        [Column("IdDespachoDet")]
        [DisplayName("IdDespachoDet")]
        public int IdDespachoDet { get; set; } = 0;
        public int IdProductoTallaColor { get; set; } = 0;
        public string? Talla { get; set; } = "";
        public string? Color { get; set; } = "";

        public clsBeTrans_movimientos() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}