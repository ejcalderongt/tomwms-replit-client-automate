namespace WMS.EntityCore.Cambio_Ubicacion
{
    public class clsBeTrans_ubic_hh_stock : ICloneable
    {
        public int IdStockTransUbicHHDet { get; set; } = 0;
        public int IdTareaUbicacionEnc { get; set; } = 0;
        public int IdTareaUbicacionDet { get; set; } = 0;
        public int IdStock { get; set; } = 0;
        public int IdPropietarioBodega { get; set; } = 0;
        public int IdProductoBodega { get; set; } = 0;
        public int IdProductoEstado { get; set; } = 0;
        public int IdPresentacion { get; set; } = 0;
        public int IdUnidadMedida { get; set; } = 0;
        public int IdUbicacion { get; set; } = 0;
        public int IdUbicacion_anterior { get; set; } = 0;
        public int IdRecepcionEnc { get; set; } = 0;
        public int IdRecepcionDet { get; set; } = 0;
        public int IdPedidoEnc { get; set; } = 0;
        public int IdPickingEnc { get; set; } = 0;
        public int IdDespachoEnc { get; set; } = 0;
        public string Lote { get; set; } = "";
        public string Lic_plate { get; set; } = "";
        public string Serial { get; set; } = "";
        public double Cantidad { get; set; } = 0.0;
        public DateOnly Fecha_ingreso { get; set; } = new DateOnly(1900,1,1);
        public DateOnly Fecha_vence { get; set; } = new DateOnly(1900, 1, 1);
        public double Uds_lic_plate { get; set; } = 0;
        public int No_bulto { get; set; } = 0;
        public DateOnly Fecha_manufactura { get; set; } = new DateOnly(1900, 1, 1);
        public int añada { get; set; } = 0;
        public string User_agr { get; set; } = "";
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = "";
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = false;
        public double Peso { get; set; } = 0.0;
        public double Temperatura { get; set; } = 0.0;
        public DateTime Fecha_mov_hist { get; set; } = DateTime.Now;
        public string Atributo_variante_1 { get; set; } = "";
        public int IdProductoTallaColor { get; set; } = 0;
        public string Talla { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;

        public clsBeTrans_ubic_hh_stock()
        {
        }

        public clsBeTrans_ubic_hh_stock(
            int IdStockTransUbicHHDet,
            int IdTareaUbicacionEnc,
            int IdTareaUbicacionDet,
            int IdStock,
            int IdPropietarioBodega,
            int IdProductoBodega,
            int IdProductoEstado,
            int IdPresentacion,
            int IdUnidadMedida,
            int IdUbicacion,
            int IdUbicacion_anterior,
            int IdRecepcionEnc,
            int IdRecepcionDet,
            int IdPedidoEnc,
            int IdPickingEnc,
            int IdDespachoEnc,
            string lote,
            string lic_plate,
            string serial,
            double cantidad,
            DateOnly fecha_ingreso,
            DateOnly fecha_vence,
            double uds_lic_plate,
            int no_bulto,
            DateOnly fecha_manufactura,
            int añada,
            string user_agr,
            DateTime fec_agr,
            string user_mod,
            DateTime fec_mod,
            bool activo,
            double peso,
            double temperatura,
            DateTime fecha_mov_hist,
            string atributo_variante_1)
        {
            this.IdStockTransUbicHHDet = IdStockTransUbicHHDet;
            this.IdTareaUbicacionEnc = IdTareaUbicacionEnc;
            this.IdTareaUbicacionDet = IdTareaUbicacionDet;
            this.IdStock = IdStock;
            this.IdPropietarioBodega = IdPropietarioBodega;
            this.IdProductoBodega = IdProductoBodega;
            this.IdProductoEstado = IdProductoEstado;
            this.IdPresentacion = IdPresentacion;
            this.IdUnidadMedida = IdUnidadMedida;
            this.IdUbicacion = IdUbicacion;
            this.IdUbicacion_anterior = IdUbicacion_anterior;
            this.IdRecepcionEnc = IdRecepcionEnc;
            this.IdRecepcionDet = IdRecepcionDet;
            this.IdPedidoEnc = IdPedidoEnc;
            this.IdPickingEnc = IdPickingEnc;
            this.IdDespachoEnc = IdDespachoEnc;
            this.Lote = lote;
            this.Lic_plate = lic_plate;
            this.Serial = serial;
            this.Cantidad = cantidad;
            this.Fecha_ingreso = fecha_ingreso;
            this.Fecha_vence = fecha_vence;
            this.Uds_lic_plate = uds_lic_plate;
            this.No_bulto = no_bulto;
            this.Fecha_manufactura = fecha_manufactura;
            this.añada = añada;
            this.User_agr = user_agr;
            this.Fec_agr = fec_agr;
            this.User_mod = user_mod;
            this.Fec_mod = fec_mod;
            this.Activo = activo;
            this.Peso = peso;
            this.Temperatura = temperatura;
            this.Fecha_mov_hist = fecha_mov_hist;
            this.Atributo_variante_1 = atributo_variante_1;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}