namespace WMSWebAPI.Dtos.Stock
{
    public class StockDto
    {
        public int IdBodega { get; set; }
        public int IdStock { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int IdProductoBodega { get; set; }
        public int IdProductoEstado { get; set; }
        public int IdPresentacion { get; set; }
        public int IdUnidadMedida { get; set; }
        public int IdUbicacion { get; set; }
        public int IdUbicacion_anterior { get; set; }
        public int IdRecepcionEnc { get; set; }
        public int IdRecepcionDet { get; set; }
        public int IdPedidoEnc { get; set; }
        public int IdPickingEnc { get; set; }
        public int IdDespachoEnc { get; set; }
        public string Lote { get; set; } = string.Empty;
        public string Lic_plate { get; set; } = string.Empty;
        public string Serial { get; set; } = string.Empty;
        public double Cantidad { get; set; }
        public DateTime Fecha_ingreso { get; set; }
        public DateTime Fecha_vence { get; set; }
        public double Uds_lic_plate { get; set; }
        public int No_bulto { get; set; }
        public DateTime Fecha_manufactura { get; set; }
        public int Añada { get; set; }
        public string User_agr { get; set; } = string.Empty;
        public DateTime Fec_agr { get; set; }
        public string User_mod { get; set; } = string.Empty;
        public DateTime Fec_mod { get; set; }
        public bool Activo { get; set; }
        public double Peso { get; set; }
        public double Temperatura { get; set; }
        public string Atributo_variante_1 { get; set; } = string.Empty;
        public bool Pallet_no_estandar { get; set; }
        public int IdPickingUbicStock { get; set; }
        public int IdPickingUbic { get; set; }
        public int IdPedidoDet { get; set; }
    }
}
