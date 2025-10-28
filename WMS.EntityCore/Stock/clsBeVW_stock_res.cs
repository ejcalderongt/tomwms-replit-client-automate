using WMS.EntityCore.Producto;

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

        public string Propietario { get; set; } = string.Empty;
        public string UMBas { get; set; } = string.Empty;
        public string Nombre_Presentacion { get; set; } = string.Empty;
        public string Codigo_Producto { get; set; } = string.Empty;
        public string Nombre_Producto { get; set; } = string.Empty;
        public string Lote { get; set; } = string.Empty;

        public DateTime Fecha_ingreso { get; set; } = DateTime.Now;

        public string Serial { get; set; } = string.Empty;

        // Nota: C# permite identificadores Unicode; se preserva el nombre VB.
        public int Añada { get; set; } = 0;

        /// <summary> Cantidad en unidad de medida básica. </summary>
        public double CantidadUmBas { get; set; } = 0.0;

        public double Factor { get; set; } = 0.0;

        /// <summary> Cantidad con presentación. </summary>
        public double CantidadPresentacion { get; set; } = 0.0;

        public DateTime Fecha_Vence { get; set; } = new DateTime(1900, 1, 1);
        public string NomEstado { get; set; } = string.Empty;
        public bool EstadoUtilizable { get; set; } = false;
        public bool Dañado { get; set; } = false;
        public string Lic_plate_Anterior { get; set; } = string.Empty;
        public string Lic_plate { get; set; } = string.Empty;
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
        public string IndiceRotacion { get; set; } = string.Empty;
        public double Existencia_min_umbas { get; set; } = 0.0;
        public double Existencia_max_umbas { get; set; } = 0.0;
        public string Codigo_Barra { get; set; } = string.Empty;
        public double Costo { get; set; } = 0.0;
        public double Tolerancia { get; set; } = 0.0;
        public double Existencia_min_pres { get; set; } = 0.0;
        public double Existencia_max_pres { get; set; } = 0.0;
        public string Atributo_variante_1 { get; set; } = string.Empty;
        public int Ubicacion_Nivel { get; set; } = 0;
        public int Ubicacion_Indice_x { get; set; } = 0;
        public string Ubicacion_Nombre { get; set; } = string.Empty;
        public string Ubicacion_Tramo { get; set; } = string.Empty;
        public bool Pallet_No_Estandar { get; set; } = false;
        public string Host { get; set; } = string.Empty;
        public int no_linea { get; set; } = 0;

        public string Nombre_Clasificacion { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string Nombre_Completo { get; set; } = string.Empty;

        public int IdOperadorBodega_Asignado { get; set; } = 0;
        public int IdRecepcionDet { get; set; } = 0;
        public string Codigo_Talla { get; set; } = string.Empty;
        public string Nombre_Talla { get; set; } = string.Empty;
        public string Codigo_Color { get; set; } = string.Empty;
        public string Nombre_Color { get; set; } = string.Empty;
        public int IdProductoTallaColor { get; set; } = 0;

        public int IdPedido { get; set; } = 0;
        public int IdPedidoDet { get; set; } = 0;
        public int IdPicking { get; set; } = 0;

        public double VolumenUmBas => CantidadUmBas * AltoUMBas * LargoUmBas * AnchoUmBas;
        public double VolumenUmBasEnUbicacion => VolumenUmBas;
        public double VolumenPresEnUbicacion => VolumenPresentacion;

        public double VolumenPresentacion =>
            CantidadPresentacion * BePresentacionProductoEnStock.Alto * BePresentacionProductoEnStock.Largo * BePresentacionProductoEnStock.Ancho;

        public double TotalLinea { get; set; } = 0.0;

        public clsBeProducto_presentacion BePresentacionProductoEnStock { get; set; } = new clsBeProducto_presentacion();

        public double AltoUMBas { get; set; } = 0.0;
        public double LargoUmBas { get; set; } = 0.0;
        public double AnchoUmBas { get; set; } = 0.0;

        public bool acepto { get; set; }
        public double peso_pickeado { get; set; }
        public double peso_verificado { get; set; }
        public double Cantidad_Pickeada { get; set; }
        public double Cantidad_Verificada { get; set; }
        public double Cantidad_Despachada { get; set; }
        public bool encontrado { get; set; }
        //public clsBeBodega_ubicacion UbicacionActual { get; set; } = new clsBeBodega_ubicacion();
        public double Cantidad_Res { get; set; } = 0.0;
        public string ValorTexto { get; set; } = string.Empty;
        public double ValorNumerico { get; set; } = 0.0;
        public DateTime ValorFecha { get; set; } = default;
        public bool ValorLogico { get; set; } = false;

        public string No_Serie { get; set; } = string.Empty;
        public string No_Serie_Inicial { get; set; } = string.Empty;
        public string No_Serie_Final { get; set; } = string.Empty;

        public double CantidadReservada { get; set; } = 0.0;

        public int IdFamilia { get; set; } = 0;
        public int IdClasificacion { get; set; } = 0;
        public int IdTipoProducto { get; set; } = 0;

        public string NombreTipoProducto { get; set; } = string.Empty;
        public string Documento_Ingreso { get; set; } = string.Empty;

        public int Posiciones { get; set; } = 0;

        public string codigo_poliza { get; set; } = string.Empty;
        public string Numero_poliza { get; set; } = string.Empty;

        public bool ubicacion_picking { get; set; } = false;

        public double CamasPorTarima { get; set; } = 0.0;
        public double CajasPorCama { get; set; } = 0.0;

        public bool es_rack { get; set; } = false;

        // Si el producto está reservado en picking y se cambia de ubicación, se usa como destino temporal.
        public int IdUbicacionVirtual { get; set; } = 0;

        // Campos agregados para HH
        public DateTime Fecha_Pedido { get; set; } = default;
        public DateTime Fecha_Preparacion { get; set; } = default;

        /// <summary>
        /// Se utiliza en la HH para enviar el movimiento generado en cambio de ubicación (licencias completas).
        /// </summary>
        public clsBeTrans_movimientos Movimiento { get; set; } = new clsBeTrans_movimientos();       
        public clsBeVW_stock_res() { }

        public clsBeVW_stock_res(
            ref int IdBodega,
            int IdPropietario,
            int IdPropietarioBodega,
            int IdProducto,
            int IdProductoBodega,
            int IdStock,
            int IdUbicacionActual,
            int IdUbicacion_anterior,
            int IdUnidadMedida,
            int IdProductoEstado,
            int IdPresentacion,
            int IdRecepcionEnc,
            string Propietario,
            string UnidadMedida,
            string Presentacion,
            string codigo,
            string nombre,
            string lote,
            DateTime fecha_ingreso,
            string serial,
            int añada,
            double CantidadSF,
            double factor,
            double Cantidad,
            DateTime fecha_vence,
            string NomEstado,
            bool EstadoUtilizable,
            bool dañado,
            int IdUbicacion,
            string lic_plate,
            double peso,
            int IdIndiceRotacion,
            double alto,
            double largo,
            double ancho,
            double CantidadReservada,
            int IdTramo,
            double ancho_ubicacion,
            double largo_ubicacion,
            double alto_ubicacion,
            string IndiceRotacion,
            double existencia_min_umbas,
            double existencia_max_umbas,
            string codigo_barra,
            double costo,
            string atributo_variante_1,
            bool pallet_no_estandar)
        {
            // Por qué: mantener firma y asignaciones idénticas al VB para compatibilidad.
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

        public object Clone() => MemberwiseClone();
    }
}