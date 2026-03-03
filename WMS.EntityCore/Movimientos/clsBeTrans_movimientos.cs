namespace WMS.EntityCore.Movimientos
{
    public class clsBeTrans_movimientos : ICloneable, IDisposable
    {
        // Propiedades principales
        public int IdMovimiento { get; set; } = 0;
        public int IdEmpresa { get; set; } = 0;
        public int IdBodegaOrigen { get; set; } = 0;
        public int IdTransaccion { get; set; } = 0;
        public int IdPropietarioBodega { get; set; } = 0;
        public int IdProductoBodega { get; set; } = 0;
        public int IdUbicacionOrigen { get; set; } = 0;
        public int IdUbicacionDestino { get; set; } = 0;
        public int IdPresentacion { get; set; } = 0;
        public int IdEstadoOrigen { get; set; } = 0;
        public int IdEstadoDestino { get; set; } = 0;
        public int IdUnidadMedida { get; set; } = 0;
        public int IdTipoTarea { get; set; } = 0;
        public int IdBodegaDestino { get; set; } = 0;
        public int IdRecepcion { get; set; } = 0;
        public double Cantidad { get; set; } = 0.0;
        public string Serie { get; set; } = "";
        public double Peso { get; set; } = 0.0;
        public string Lote { get; set; } = "";
        public DateTime Fecha_vence { get; set; } = DateTime.Now;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Barra_pallet { get; set; } = "";
        public DateTime Hora_ini { get; set; } = DateTime.Now;
        public DateTime Hora_fin { get; set; } = DateTime.Now;
        public DateTime Fecha_agr { get; set; } = DateTime.Now;
        public string Usuario_agr { get; set; } = "";
        public double Cantidad_hist { get; set; } = 0.0;
        public double Peso_hist { get; set; } = 0.0;
        public string Lic_plate { get; set; } = "";

        /// <summary>
        /// #EJC202302211347: Asociar detalle de recepción
        /// </summary>
        public int IdRecepcionDet { get; set; } = 0;

        /// <summary>
        /// #EJC202302211347: Asociar IdPedidoEnc en despacho.
        /// </summary>
        public int IdPedidoEnc { get; set; } = 0;

        /// <summary>
        /// #EJC202302211347: Asociar IdPedidoDet en despacho.
        /// </summary>
        public int IdPedidoDet { get; set; } = 0;

        /// <summary>
        /// #EJC202302211347: Asociar IdDespachoEnc en despacho.
        /// </summary>
        public int IdDespachoEnc { get; set; } = 0;

        /// <summary>
        /// #EJC202302211347: Asociar IdDespachoDet en despacho.
        /// </summary>
        public int IdDespachoDet { get; set; } = 0;

        /// <summary>
        /// GT28082025: control de producto por talla/color
        /// </summary>
        public int IdProductoTallaColor { get; set; } = 0;
        public string Talla { get; set; } = "";
        public string Color { get; set; } = "";

        // Propiedades del Partial Class
        public bool IsNew { get; set; }
        public string TipoTarea { get; set; } = "";
        public int IdProducto { get; set; }
        public string Codigo { get; set; } = "";
        public string CodigoBarra { get; set; } = "";
        public string Producto { get; set; } = "";
        public string Propietario { get; set; } = "";
        public string Presentacion { get; set; } = "";
        public string EstadoOrigen { get; set; } = "";
        public string EstadoDestino { get; set; } = "";
        public string UMBas { get; set; } = "";
        public string UbicOrigen { get; set; } = "";
        public string UbicDestino { get; set; } = "";
        public int IdOperadorBodega { get; set; } = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="clsBeTrans_movimientos"/> class.
        /// </summary>
        public clsBeTrans_movimientos()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="clsBeTrans_movimientos"/> class.
        /// </summary>
        public clsBeTrans_movimientos(
            int IdMovimiento,
            int IdEmpresa,
            int IdBodegaOrigen,
            int IdTransaccion,
            int IdPropietarioBodega,
            int IdProductoBodega,
            int IdUbicacionOrigen,
            int IdUbicacionDestino,
            int IdPresentacion,
            int IdEstadoOrigen,
            int IdEstadoDestino,
            int IdUnidadMedida,
            int IdTipoTarea,
            int IdBodegaDestino,
            int IdRecepcion,
            double cantidad,
            string serie,
            double peso,
            string lote,
            DateTime fecha_vence,
            DateTime fecha,
            string barra_pallet,
            DateTime hora_ini,
            DateTime hora_fin,
            DateTime fecha_agr,
            string usuario_agr,
            double cantidad_hist,
            double peso_hist)
        {
            this.IdMovimiento = IdMovimiento;
            this.IdEmpresa = IdEmpresa;
            this.IdBodegaOrigen = IdBodegaOrigen;
            this.IdTransaccion = IdTransaccion;
            this.IdPropietarioBodega = IdPropietarioBodega;
            this.IdProductoBodega = IdProductoBodega;
            this.IdUbicacionOrigen = IdUbicacionOrigen;
            this.IdUbicacionDestino = IdUbicacionDestino;
            this.IdPresentacion = IdPresentacion;
            this.IdEstadoOrigen = IdEstadoOrigen;
            this.IdEstadoDestino = IdEstadoDestino;
            this.IdUnidadMedida = IdUnidadMedida;
            this.IdTipoTarea = IdTipoTarea;
            this.IdBodegaDestino = IdBodegaDestino;
            this.IdRecepcion = IdRecepcion;
            this.Cantidad = cantidad;
            this.Serie = serie ?? "";
            this.Peso = peso;
            this.Lote = lote ?? "";
            this.Fecha_vence = fecha_vence;
            this.Fecha = fecha;
            this.Barra_pallet = barra_pallet ?? "";
            this.Hora_ini = hora_ini;
            this.Hora_fin = hora_fin;
            this.Fecha_agr = fecha_agr;
            this.Usuario_agr = usuario_agr ?? "";
            this.Cantidad_hist = cantidad_hist;
            this.Peso_hist = peso_hist;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects)
                }

                // Free unmanaged resources and set large fields to null
                disposedValue = true;
            }
        }

        // Uncomment this if you have unmanaged resources
        // ~clsBeTrans_movimientos()
        // {
        //     Dispose(false);
        // }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}