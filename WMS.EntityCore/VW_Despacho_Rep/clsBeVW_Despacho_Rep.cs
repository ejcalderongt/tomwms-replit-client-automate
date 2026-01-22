namespace WMS.EntityCore.VW_Despacho_Rep
{
    public class clsBeVW_Despacho_Rep : ICloneable
    {
        public int IdPickingUbic { get; set; } = 0;
        public int IdStock { get; set; } = 0;
        public int IdPedidoDet { get; set; } = 0;
        public int IdPropietarioBodega { get; set; } = 0;
        public int IdProductoBodega { get; set; } = 0;
        public int IdProductoEstado { get; set; } = 0;
        public int IdPresentacion { get; set; } = 0;
        public int IdUnidadMedida { get; set; } = 0;
        public int IdRecepcion { get; set; } = 0;
        public int IdDespachoEnc { get; set; } = 0;
        public int IdDespachoDet { get; set; } = 0;
        public int IdPedidoEnc { get; set; } = 0;
        public string Propietario { get; set; } = "";
        public string Codigo_Producto { get; set; } = "";
        public string Nombre_Producto { get; set; } = "";
        public string UM { get; set; } = "";
        public string Presentacion { get; set; } = "";
        public double Factor { get; set; } = 0.0;
        public string Estado { get; set; } = "";
        public string Lote { get; set; } = "";
        public string Licencia { get; set; } = "";
        public DateTime Vence { get; set; } = DateTime.Now;
        public string Ubicacion_Origen { get; set; } = "";
        public double Cantidad_pickeada { get; set; } = 0.0;
        public double Cantidad_verificada { get; set; } = 0.0;
        public double Peso_Pickeado { get; set; } = 0.0;
        public double Peso_Verificado { get; set; } = 0.0;
        public double CantidadDespachada { get; set; } = 0.0;
        public double PesoDespachado { get; set; } = 0.0;
        public bool Encontrado { get; set; } = false;
        public bool Acepto { get; set; } = false;
        public int No_Documento_WMS { get; set; } = 0;
        public string No_Referencia { get; set; } = "";
        public string Codigo_Cliente { get; set; } = "";
        public string Nombre_Cliente { get; set; } = "";
        public int Idubicacionvirtual { get; set; } = 0;
        public bool Es_bodega_recepcion { get; set; } = false;
        public bool Es_bodega_traslado { get; set; } = false;
        public int No_pase { get; set; } = 0;
        public string Observacion { get; set; } = "";
        public int Numero { get; set; } = 0;
        public string Marchamo { get; set; } = "";
        public string Codigo_Ruta { get; set; } = "";
        public string Nombre_Ruta { get; set; } = "";
        public string Placa_Vehiculo { get; set; } = "";
        public string Placa_Comercial { get; set; } = "";
        public string Marca_Vehiculo { get; set; } = "";
        public string Modelo_Vehiculo { get; set; } = "";
        public string Nombre_Piloto { get; set; } = "";
        public string Apellido_Piloto { get; set; } = "";
        public string No_Carnet_Piloto { get; set; } = "";
        public string No_Licencia_Piloto { get; set; } = "";
        public DateTime Fecha { get; set; }
        public string codigo_poliza_pedido { get; set; } = "";
        public string clasificacion { get; set; } = "";
        public string marca { get; set; } = "";
        public string familia { get; set; } = "";
        public string parametro_a { get; set; } = "";
        public string parametro_b { get; set; } = "";
        public string numero_orden_pedido { get; set; } = "";
        public string numero_orden_ingreso { get; set; } = "";
        public string codigo_poliza_ingreso { get; set; } = "";
        public string codigo_regimen_salida { get; set; } = "";
        public string placa_contenedor_salida { get; set; } = "";
        public string Dua_salida { get; set; } = "";
        public string Talla { get; set; } = "";
        public string Color { get; set; } = "";

        public clsBeVW_Despacho_Rep()
        {
        }

        public clsBeVW_Despacho_Rep(ref int IdPickingUbic, int IdStock, int IdPedidoDet,
                            int IdPropietarioBodega, int IdProductoBodega,
                            int IdProductoEstado, int IdPresentacion,
                            int IdUnidadMedida, int IdRecepcion,
                            int IdDespachoEnc, int IdDespachoDet,
                            int IdPedidoEnc, string Propietario,
                            string Codigo_Producto, string Nombre_Producto,
                            string UM, string Presentacion, double factor,
                            string Estado, string lote, DateTime Vence,
                            string Ubicacion_Origen, double cantidad_pickeada,
                            double cantidad_verificada, double Peso_Pickeado,
                            double Peso_Verificado, double CantidadDespachada,
                            double PesoDespachado, bool Encontrado,
                            bool Acepto, int No_Documento_WMS,
                            string No_Referencia, string Codigo_Cliente,
                            string Nombre_Cliente, int idubicacionvirtual,
                            bool es_bodega_recepcion, bool es_bodega_traslado,
                            int no_pase, string observacion, int numero,
                            string marchamo, string Codigo_Ruta, string Nombre_Ruta,
                            string Placa_Vehiculo, string Placa_Comercial,
                            string Marca_Vehiculo, string Modelo_Vehiculo,
                            string Nombre_Piloto, string Apellido_Piloto,
                            string No_Carnet_Piloto, string No_Licencia_Piloto,
                            DateTime Fecha, string codigo_poliza_pedido,
                            string numero_orden_pedido,
                            string codigo_poliza_ingreso,
                            string numero_orden_ingreso)
        {
            this.IdPickingUbic = IdPickingUbic;
            this.IdStock = IdStock;
            this.IdPedidoDet = IdPedidoDet;
            this.IdPropietarioBodega = IdPropietarioBodega;
            this.IdProductoBodega = IdProductoBodega;
            this.IdProductoEstado = IdProductoEstado;
            this.IdPresentacion = IdPresentacion;
            this.IdUnidadMedida = IdUnidadMedida;
            this.IdRecepcion = IdRecepcion;
            this.IdDespachoEnc = IdDespachoEnc;
            this.IdDespachoDet = IdDespachoDet;
            this.IdPedidoEnc = IdPedidoEnc;
            this.Propietario = Propietario;
            this.Codigo_Producto = Codigo_Producto;
            this.Nombre_Producto = Nombre_Producto;
            this.UM = UM;
            this.Presentacion = Presentacion;
            this.Factor = factor;
            this.Estado = Estado;
            this.Lote = lote;
            this.Vence = Vence;
            this.Ubicacion_Origen = Ubicacion_Origen;
            this.Cantidad_pickeada = cantidad_pickeada;
            this.Cantidad_verificada = cantidad_verificada;
            this.Peso_Pickeado = Peso_Pickeado;
            this.Peso_Verificado = Peso_Verificado;
            this.CantidadDespachada = CantidadDespachada;
            this.PesoDespachado = PesoDespachado;
            this.Encontrado = Encontrado;
            this.Acepto = Acepto;
            this.No_Documento_WMS = No_Documento_WMS;
            this.No_Referencia = No_Referencia;
            this.Codigo_Cliente = Codigo_Cliente;
            this.Nombre_Cliente = Nombre_Cliente;
            this.Idubicacionvirtual = idubicacionvirtual;
            this.Es_bodega_recepcion = es_bodega_recepcion;
            this.Es_bodega_traslado = es_bodega_traslado;
            this.No_pase = no_pase;
            this.Observacion = observacion;
            this.Numero = numero;
            this.Marchamo = marchamo;
            this.Codigo_Ruta = Codigo_Ruta;
            this.Nombre_Ruta = Nombre_Ruta;
            this.Placa_Vehiculo = Placa_Vehiculo;
            this.Placa_Comercial = Placa_Comercial;
            this.Marca_Vehiculo = Marca_Vehiculo;
            this.Modelo_Vehiculo = Modelo_Vehiculo;
            this.Nombre_Piloto = Nombre_Piloto;
            this.Apellido_Piloto = Apellido_Piloto;
            this.No_Carnet_Piloto = No_Carnet_Piloto;
            this.No_Licencia_Piloto = No_Licencia_Piloto;
            this.Fecha = Fecha;
            this.codigo_poliza_pedido = codigo_poliza_pedido;
            this.numero_orden_pedido = numero_orden_pedido;
            this.codigo_poliza_ingreso = codigo_poliza_ingreso;
            this.numero_orden_ingreso = numero_orden_ingreso;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
