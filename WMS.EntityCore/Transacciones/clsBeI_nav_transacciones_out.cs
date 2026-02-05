namespace WMS.EntityCore.Transacciones
{
    public class clsBeI_nav_transacciones_out : ICloneable
    {
        public int Idtransaccion { get; set; } = 0;
        public int Idempresa { get; set; } = 0;
        public int Idbodega { get; set; } = 0;
        public int Idpropietario { get; set; } = 0;
        public int Idpropietariobodega { get; set; } = 0;
        public int Idordencompra { get; set; } = 0;
        public int Idrecepcionenc { get; set; } = 0;
        public int Idpedidoenc { get; set; } = 0;
        public int Iddespachoenc { get; set; } = 0;
        public int Idproductobodega { get; set; } = 0;
        public int Idproducto { get; set; } = 0;
        public int Idunidadmedida { get; set; } = 0;
        public int Idpresentacion { get; set; } = 0;
        public int Idproductoestado { get; set; } = 0;
        public double Cantidad { get; set; } = 0.0;
        public double Cantidad_Esperada { get; set; } = 0;
        public double Peso { get; set; } = 0.0;
        public string Lote { get; set; } = "";
        public DateTime Fecha_vence { get; set; } = new DateTime(1900, 1, 1);
        public DateTime Fecha_recepcion { get; set; } = new DateTime(1900, 1, 1);
        public string No_pedido { get; set; } = "";
        public string No_linea { get; set; } = "";
        public string Codigo_producto { get; set; } = "";
        public string Nombre_producto { get; set; } = "";
        public string Codigo_variante { get; set; } = "";
        public string Unidad_medida { get; set; } = "";
        public string Tipo_transaccion { get; set; } = "";
        public bool Enviado { get; set; } = false;
        public bool Auditar { get; set; } = false;
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_agr { get; set; } = "";
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = "";
        public string Lic_Plate { get; set; } = "";
        public double Uds_Lic_Plate { get; set; } = 0.0;
        public double Cantidad_Presentacion { get; set; } = 0.0;
        public int IdTipoDocumento { get; set; } = 0;
        public string Observacion { get; set; } = "";
        public string Empresa_Transporte { get; set; } = "";
        public string Piloto_Transporte { get; set; } = "";
        public string Contacto_Recibe { get; set; } = "";
        public string Contacto_Entrega { get; set; } = "";
        public string Placa_Transporte { get; set; } = "";
        public string TCN_Transporte { get; set; } = "";
        public string Marchamo_No { get; set; } = "";
        public string Codigo_Bodega_Origen { get; set; } = "";
        public string Codigo_Bodega_Destino { get; set; } = "";
        public string Codigo_Cliente { get; set; } = "";
        public string codigo_barra { get; set; } = "";
        public double valor_aduana { get; set; } = 0.0;
        public double valor_fob { get; set; } = 0.0;
        public double valor_iva { get; set; } = 0.0;
        public double valor_dai { get; set; } = 0.0;
        public double valor_seguro { get; set; } = 0.0;
        public double valor_flete { get; set; } = 0.0;
        public double peso_neto { get; set; } = 0.0;
        public double peso_bruto { get; set; } = 0.0;
        public DateTime fecha_despacho { get; set; } = new DateTime(1900, 1, 1);
        public string no_documento_salida_ref_devol { get; set; } = "";
        public int IdPedidoEncDevol { get; set; } = 0;
        public int IdDespachoDet { get; set; } = 0;
        public int IdRecepcionDet { get; set; } = 0;
        public double Cantidad_Enviada { get; set; } = 0;
        public double Cantidad_Pendiente { get; set; } = 0;
        public int IdProductoTallaColor { get; set; } = 0;
        public string Talla { get; set; } = "";
        public string Color { get; set; } = "";

        public clsBeI_nav_transacciones_out()
        {
        }

        public clsBeI_nav_transacciones_out(ref int idtransaccion, int idempresa, int idbodega, int idpropietario,
            int idpropietariobodega, int idordencompra, int idrecepcionenc,
            int idpedidoenc, int iddespachoenc, int idproductobodega, int idproducto,
            int idunidadmedida, int idpresentacion, int idproductoestado, double cantidad,
            double peso, string lote, string fecha_vence, string fecha_recepcion, string no_pedido,
            string no_linea, string codigo_producto, string nombre_producto, string codigo_variante,
            string unidad_medida, string tipo_transaccion, bool enviado, DateTime fec_agr,
            string user_agr, DateTime fec_mod, string user_mod, string lic_plate, double uds_lic_plate,
            double cantidad_presentacion,
            string codigo_barra, double valor_aduana, double valor_fob, double valor_iva, double valor_dai,
            double valor_seguro, double valor_flete, double peso_neto, double peso_bruto, DateTime fecha_despacho)
        {
            this.Idtransaccion = idtransaccion;
            this.Idempresa = idempresa;
            this.Idbodega = idbodega;
            this.Idpropietario = idpropietario;
            this.Idpropietariobodega = idpropietariobodega;
            this.Idordencompra = idordencompra;
            this.Idrecepcionenc = idrecepcionenc;
            this.Idpedidoenc = idpedidoenc;
            this.Iddespachoenc = iddespachoenc;
            this.Idproductobodega = idproductobodega;
            this.Idproducto = idproducto;
            this.Idunidadmedida = idunidadmedida;
            this.Idpresentacion = idpresentacion;
            this.Idproductoestado = idproductoestado;
            this.Cantidad = cantidad;
            this.Peso = peso;
            this.Lote = lote;
            this.Fecha_vence = DateTime.Parse(fecha_vence);
            this.Fecha_recepcion = DateTime.Parse(fecha_recepcion);
            this.No_pedido = no_pedido;
            this.No_linea = no_linea;
            this.Codigo_producto = codigo_producto;
            this.Nombre_producto = nombre_producto;
            this.Codigo_variante = codigo_variante;
            this.Unidad_medida = unidad_medida;
            this.Tipo_transaccion = tipo_transaccion;
            this.Enviado = enviado;
            this.Fec_agr = fec_agr;
            this.User_agr = user_agr;
            this.Fec_mod = fec_mod;
            this.User_mod = user_mod;
            this.Lic_Plate = lic_plate;
            this.Uds_Lic_Plate = uds_lic_plate;
            this.Cantidad_Presentacion = cantidad_presentacion;
            this.codigo_barra = codigo_barra;
            this.valor_aduana = valor_dai;
            this.valor_fob = valor_fob;
            this.valor_iva = valor_iva;
            this.valor_dai = valor_dai;
            this.valor_seguro = valor_seguro;
            this.valor_flete = valor_flete;
            this.peso_neto = peso_neto;
            this.peso_bruto = peso_bruto;
            this.fecha_despacho = fecha_despacho;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


    }
}