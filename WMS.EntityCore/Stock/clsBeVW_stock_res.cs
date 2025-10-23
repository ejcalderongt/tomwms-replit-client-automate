namespace WMS.EntityCore.Stock
{
    using System;
    using WMS.EntityCore.Producto;
    using WMSWebAPI.Be;

    [Serializable]
    public class clsBeVW_stock_res : ICloneable
    {
        public int IdBodega { get; set; } = 0;
        public int IdPropietario { get; set; } = 0;
        public int IdPropietarioBodega { get; set; } = 0;
        public int IdProducto { get; set; } = 0;
        public int IdProductoBodega { get; set; } = 0;
        public int IdStock { get; set; } = 0;
        public int IdStockRes { get; set; } = 0;
        public int IdUbicacion { get; set; } = 0;
        public int IdUbicacion_Anterior { get; set; } = 0;
        public int IdUnidadMedida { get; set; } = 0;
        public int IdProductoEstado { get; set; } = 0;
        public int IdPresentacion_Anterior { get; set; } = 0;
        public int IdPresentacion { get; set; } = 0;
        public int IdRecepcionEnc { get; set; } = 0;
        public string Propietario { get; set; } = "";
        public string UMBas { get; set; } = "";
        public string Nombre_Presentacion { get; set; } = "";
        public string Codigo_Producto { get; set; } = "";
        public string Nombre_Producto { get; set; } = "";
        public string Lote { get; set; } = "";
        public DateTime Fecha_ingreso { get; set; } = DateTime.Now;
        public string Serial { get; set; } = "";
        public int Añada { get; set; } = 0;
        /// <summary>
        /// Cantidad en unidad de medida básica.
        /// </summary>
        public double CantidadUmBas { get; set; } = 0.0;
        public double Factor { get; set; } = 0.0;

        /// <summary>
        /// Cantidad con presentación
        /// </summary>
        public double CantidadPresentacion { get; set; } = 0;
        public DateTime Fecha_Vence { get; set; } = new DateTime(1900, 1, 1);
        public string NomEstado { get; set; } = "";
        public bool EstadoUtilizable { get; set; } = false;
        public bool Dañado { get; set; } = false;
        public string Lic_plate_Anterior { get; set; } = "";
        public string Lic_plate { get; set; } = "";
        public double Peso { get; set; } = 0.0;
        public int IdIndiceRotacion { get; set; } = 0;
        public double AltoUbicacion { get; set; } = 0.0;
        public double LargoUbicacion { get; set; } = 0.0;
        public double AnchoUbicacion { get; set; } = 0.0;
        public double CantidadReservadaUMBas { get; set; } = 0.0;
        public int IdTramo { get; set; } = 0;
        public double Ancho_ubicacion { get; set; } = 0.0;
        public double Largo_ubicacion { get; set; } = 0.0;
        public double Alto_ubicacion { get; set; } = 0.0;
        public string IndiceRotacion { get; set; } = "";
        public double Existencia_min_umbas { get; set; } = 0.0;
        public double Existencia_max_umbas { get; set; } = 0.0;
        public string Codigo_Barra { get; set; } = "";
        public double Costo { get; set; } = 0.0;
        public double Tolerancia { get; set; } = 0;
        public double Existencia_min_pres { get; set; } = 0;
        public double Existencia_max_pres { get; set; } = 0;
        public string Atributo_variante_1 { get; set; } = "";
        public int Ubicacion_Nivel { get; set; } = 0;
        public int Ubicacion_Indice_x { get; set; } = 0;
        public string Ubicacion_Nombre { get; set; } = "";
        public string Ubicacion_Tramo { get; set; } = "";
        public bool Pallet_No_Estandar { get; set; } = false;
        public string Host { get; set; } = "";
        public int no_linea { get; set; } = 0;
        public string Nombre_Clasificacion { get; set; } = "";
        public string Area { get; set; } = "";
        public string Nombre_Completo { get; set; } = "";
        public int IdOperadorBodega_Asignado { get; set; } = 0;
        public int IdRecepcionDet { get; set; } = 0;
        public string Codigo_Talla { get; set; } = "";
        public string Nombre_Talla { get; set; } = "";
        public string Codigo_Color { get; set; } = "";
        public string Nombre_Color { get; set; } = "";
        public int IdProductoTallaColor { get; set; } = 0;        

        public int IdPedido { get; set; } = 0;
        public int IdPedidoDet { get; set; } = 0;
        public int IdPicking { get; set; } = 0;

        public double VolumenUmBas
        {
            get { return CantidadUmBas * AltoUMBas * LargoUmBas * AnchoUmBas; }
        }

        public double VolumenUmBasEnUbicacion
        {
            get { return VolumenUmBas; }
        }

        public double VolumenPresEnUbicacion
        {
            get { return VolumenPresentacion; }
        }

        public double VolumenPresentacion
        {
            get { return CantidadPresentacion * BePresentacionProductoEnStock.Alto * BePresentacionProductoEnStock.Largo * BePresentacionProductoEnStock.Ancho; }
        }

        public double TotalLinea { get; set; } = 0;

        public clsBeProducto_presentacion BePresentacionProductoEnStock { get; set; } = new clsBeProducto_presentacion();

        public double AltoUMBas { get; set; } = 0;
        public double LargoUmBas { get; set; } = 0;
        public double AnchoUmBas { get; set; } = 0;
        public bool acepto { get; set; } = false;
        public double peso_pickeado { get; set; }
        public double peso_verificado { get; set; }
        public double Cantidad_Pickeada { get; set; }
        public double Cantidad_Verificada { get; set; }
        public double Cantidad_Despachada { get; set; }
        public bool encontrado { get; set; } = false;
        public clsBeBodega_ubicacion UbicacionActual { get; set; } = new clsBeBodega_ubicacion();
        public double Cantidad_Res { get; set; } = 0;
        public string ValorTexto { get; set; } = string.Empty;
        public double ValorNumerico { get; set; } = 0;
        public DateTime ValorFecha { get; set; } = DateTime.MinValue;
        public bool ValorLogico { get; set; } = false;
        public string No_Serie { get; set; } = string.Empty;
        public string No_Serie_Inicial { get; set; } = string.Empty;
        public string No_Serie_Final { get; set; } = string.Empty;
        public double CantidadReservada { get; set; } = 0;
        public int IdFamilia { get; set; } = 0;
        public int IdClasificacion { get; set; } = 0;
        public int IdTipoProducto { get; set; } = 0;
        public string NombreTipoProducto { get; set; } = "";
        public string Documento_Ingreso { get; set; } = "";
        public int Posiciones { get; set; } = 0;
        public string codigo_poliza { get; set; } = "";
        public string Numero_poliza { get; set; } = "";
        public bool ubicacion_picking { get; set; } = false;
        public double CamasPorTarima { get; set; } = 0;
        public double CajasPorCama { get; set; } = 0;
        public bool es_rack { get; set; } = false;

        // #EJC20220330: Si el producto está reservado en picking y se quiere cambiar de ubicación, aquí se colocará la ubicación temporal de destino.
        public int IdUbicacionVirtual { get; set; } = 0;

        // #CKKF20220627 Campos agregados en la vista para poder ver estos datos en la HH
        public DateTime Fecha_Pedido { get; set; } = DateTime.MinValue;
        public DateTime Fecha_Preparacion { get; set; }= DateTime.MinValue;
        public clsBeVW_stock_res() { }
        public clsBeVW_stock_res(int IdBodega, int IdPropietario, int IdPropietarioBodega, int IdProducto, int IdProductoBodega, int IdStock,
                                 int IdUbicacionActual, int IdUbicacion_anterior, int IdUnidadMedida, int IdProductoEstado, int IdPresentacion,
                                 int IdRecepcionEnc, string Propietario, string UnidadMedida, string Presentacion, string codigo, string nombre,
                                 string lote, DateTime fecha_ingreso, string serial, int añada, double CantidadSF, double factor, double Cantidad,
                                 DateTime fecha_vence, string NomEstado, bool EstadoUtilizable, bool dañado, int IdUbicacion, string lic_plate,
                                 double peso, int IdIndiceRotacion, double alto, double largo, double ancho, double CantidadReservada, int IdTramo,
                                 double ancho_ubicacion, double largo_ubicacion, double alto_ubicacion, string IndiceRotacion, double existencia_min_umbas,
                                 double existencia_max_umbas, string codigo_barra, double costo, string atributo_variante_1, bool pallet_no_estandar)
        {
            this.IdBodega = IdBodega;
            this.IdPropietario = IdPropietario;
            this.IdPropietarioBodega = IdPropietarioBodega;
            this.IdProducto = IdProducto;
            this.IdProductoBodega = IdProductoBodega;
            this.IdStock = IdStock;
            this.IdUbicacion = IdUbicacionActual;
            this.IdUbicacion_Anterior = IdUbicacion_anterior;
            this.IdUnidadMedida = IdUnidadMedida;
            this.IdProductoEstado = IdProductoEstado;
            this.IdPresentacion = IdPresentacion;
            this.IdRecepcionEnc = IdRecepcionEnc;
            this.Propietario = Propietario;
            this.UMBas = UnidadMedida;
            this.Nombre_Presentacion = Presentacion;
            this.Codigo_Producto = codigo;
            this.Nombre_Producto = nombre;
            this.Lote = lote;
            this.Fecha_ingreso = fecha_ingreso;
            this.Serial = serial;
            this.Añada = añada;
            this.CantidadUmBas = CantidadSF;
            this.Factor = factor;
            this.CantidadPresentacion = Cantidad;
            this.Fecha_Vence = fecha_vence;
            this.NomEstado = NomEstado;
            this.EstadoUtilizable = EstadoUtilizable;
            this.Dañado = dañado;
            this.IdUbicacion = IdUbicacion;
            this.Lic_plate = lic_plate;
            this.Peso = peso;
            this.IdIndiceRotacion = IdIndiceRotacion;
            this.AltoUbicacion = alto;
            this.LargoUbicacion = largo;
            this.AnchoUbicacion = ancho;
            this.CantidadReservadaUMBas = CantidadReservada;
            this.IdTramo = IdTramo;
            this.Ancho_ubicacion = ancho_ubicacion;
            this.Largo_ubicacion = largo_ubicacion;
            this.Alto_ubicacion = alto_ubicacion;
            this.IndiceRotacion = IndiceRotacion;
            this.Existencia_min_umbas = existencia_min_umbas;
            this.Existencia_max_umbas = existencia_max_umbas;
            this.Codigo_Barra = codigo_barra;
            this.Costo = costo;
            this.Atributo_variante_1 = atributo_variante_1;
            this.Pallet_No_Estandar = pallet_no_estandar;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }    
}