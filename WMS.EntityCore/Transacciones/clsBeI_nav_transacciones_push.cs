namespace WMS.EntityCore.Transacciones
{
    public class clsBeI_nav_transacciones_push : ICloneable
    {
        public int IdTransaccionPush { get; set; } = 0;
        public int IdEmpresa { get; set; } = 0;
        public int IdBodega { get; set; } = 0;
        public int IdPropietariobodega { get; set; } = 0;
        public int IdOrdenCompra { get; set; } = 0;
        public int IdRecepcionEnc { get; set; } = 0;
        public int IdRecepcionDet { get; set; } = 0;
        public int Idproductobodega { get; set; } = 0;
        public int Idproducto { get; set; } = 0;
        public int Idunidadmedida { get; set; } = 0;
        public int Idpresentacion { get; set; } = 0;
        public int Idproductoestado { get; set; } = 0;
        public double Cantidad { get; set; } = 0.0;
        public double Peso { get; set; } = 0.0;
        public string Lote { get; set; } = "";
        public DateTime Fecha_vence { get; set; } = new DateTime(1900, 1, 1);
        public string No_linea { get; set; } = "";
        public string Codigo_variante { get; set; } = "";
        public string Nom_unidad_medida { get; set; } = "";
        public string Tipo_transaccion { get; set; } = "";
        public int IdTipoDocumento { get; set; } = 0;
        public string Tipo_push { get; set; } = "";
        public string No_recepcion_almacen { get; set; } = "";
        public string Documento_ubicacion { get; set; } = "";
        public string Documento_ingreso { get; set; } = "";
        public string Documento_recepcion { get; set; } = "";
        public string Location_code { get; set; } = "";
        public string Zone_code { get; set; } = "";
        public string Bin_code { get; set; } = "";
        public string Assigne_user_id { get; set; } = "";
        public string Item_no { get; set; } = "";
        public string No_orden_prod { get; set; } = "";
        public string Respuesta_push { get; set; } = "";
        public bool Enviado_A_ERP { get; set; } = false;
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_agr { get; set; } = "";
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = "";

        public clsBeI_nav_transacciones_push()
        {
            // Constructor
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}