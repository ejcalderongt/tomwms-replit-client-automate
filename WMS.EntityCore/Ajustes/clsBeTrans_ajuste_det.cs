using WMS.EntityCore.Producto;

namespace WMS.EntityCore.Ajustes
{    
    public partial class clsBeTrans_ajuste_det : ICloneable
    {
        // Propiedades existentes del partial
        public int idstockres { get; set; }
        public int idstocklink { get; set; } = 0;
        public int esnuevolink { get; set; } = 0;
        public string UmBas { get; set; } = "";
        public double CantReservada { get; set; }
        public double Factor { get; set; } = 0;
        public string Nombre_Presentacion { get; set; } = "";

        /// <summary>
        /// Gets or sets the idajustedet.
        /// </summary>
        /// <value>The idajustedet.</value>
        public int IdAjusteDet { get; set; } = 0;

        /// <summary>
        /// Gets or sets the idajusteenc.
        /// </summary>
        /// <value>The idajusteenc.</value>
        public int IdAjusteEnc { get; set; } = 0;

        /// <summary>
        /// Gets or sets the identifier stock.
        /// </summary>
        /// <value>The identifier stock.</value>
        public int IdStock { get; set; } = 0;

        /// <summary>
        /// Gets or sets the identifier propietario bodega.
        /// </summary>
        /// <value>The identifier propietario bodega.</value>
        public int IdPropietarioBodega { get; set; } = 0;

        /// <summary>
        /// Gets or sets the identifier producto bodega.
        /// </summary>
        /// <value>The identifier producto bodega.</value>
        public int IdProductoBodega { get; set; } = 0;

        /// <summary>
        /// Gets or sets the identifier producto estado.
        /// </summary>
        /// <value>The identifier producto estado.</value>
        public int IdProductoEstado { get; set; } = 0;

        /// <summary>
        /// Gets or sets the identifier presentacion.
        /// </summary>
        /// <value>The identifier presentacion.</value>
        public int IdPresentacion { get; set; } = 0;

        /// <summary>
        /// Gets or sets the identifier unidad medida.
        /// </summary>
        /// <value>The identifier unidad medida.</value>
        public int IdUnidadMedida { get; set; } = 0;

        /// <summary>
        /// Gets or sets the identifier ubicacion.
        /// </summary>
        /// <value>The identifier ubicacion.</value>
        public int IdUbicacion { get; set; } = 0;

        /// <summary>
        /// Gets or sets the lote_original.
        /// </summary>
        /// <value>The lote_original.</value>
        public string Lote_original { get; set; } = "";

        /// <summary>
        /// Gets or sets the lote_nuevo.
        /// </summary>
        /// <value>The lote_nuevo.</value>
        public string Lote_nuevo { get; set; } = "";

        /// <summary>
        /// Gets or sets the fecha_vence_original.
        /// </summary>
        /// <value>The fecha_vence_original.</value>
        public DateTime Fecha_vence_original { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the fecha_vence_nueva.
        /// </summary>
        /// <value>The fecha_vence_nueva.</value>
        public DateTime Fecha_vence_nueva { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the peso_original.
        /// </summary>
        /// <value>The peso_original.</value>
        public double Peso_original { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets the peso_nuevo.
        /// </summary>
        /// <value>The peso_nuevo.</value>
        public double Peso_nuevo { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets the cantidad_original.
        /// </summary>
        /// <value>The cantidad_original.</value>
        public double Cantidad_original { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets the cantidad_nueva.
        /// </summary>
        /// <value>The cantidad_nueva.</value>
        public double Cantidad_nueva { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets the codigo_producto.
        /// </summary>
        /// <value>The codigo_producto.</value>
        public string Codigo_producto { get; set; } = "";

        /// <summary>
        /// Gets or sets the nombre_producto.
        /// </summary>
        /// <value>The nombre_producto.</value>
        public string Nombre_producto { get; set; } = "";

        /// <summary>
        /// Gets or sets the idtipoajuste.
        /// </summary>
        /// <value>The idtipoajuste.</value>
        public int Idtipoajuste { get; set; } = 0;

        /// <summary>
        /// Gets or sets the idmotivoajuste.
        /// </summary>
        /// <value>The idmotivoajuste.</value>
        public int IdMotivoAjuste { get; set; } = 0;

        /// <summary>
        /// Gets or sets the observacion.
        /// </summary>
        /// <value>The observacion.</value>
        public string Observacion { get; set; } = "";

        /// <summary>
        /// Gets or sets the codigo_ajuste.
        /// </summary>
        /// <value>The codigo_ajuste.</value>
        public string Codigo_ajuste { get; set; } = "";

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="clsBeTrans_ajuste_det"/> is enviado.
        /// </summary>
        /// <value><c>true</c> if enviado; otherwise, <c>false</c>.</value>
        public bool Enviado { get; set; } = false;

        /// <summary>
        /// Gets or sets the presentacion.
        /// </summary>
        public clsBeProducto_presentacion Presentacion { get; set; } = new clsBeProducto_presentacion();

        /// <summary>
        /// Gets or sets the identifier bodega erp.
        /// </summary>
        public int IdBodegaERP { get; set; } = 0;

        /// <summary>
        /// CKFK20220208: Agregamos el campo LicPlate
        /// </summary>
        public string lic_plate { get; set; } = "";

        /// <summary>
        /// CKFK20220208: Agregamos estos campos para control del envío de los ajustes a NAV
        /// </summary>
        public string referencia_ajuste_erp { get; set; } = "";

        /// <summary>
        /// Gets or sets a value indicating whether estado_ajuste_erp.
        /// </summary>
        public bool estado_ajuste_erp { get; set; } = false;

        /// <summary>
        /// GT28082025: control de producto por talla/color
        /// </summary>
        public int IdProductoTallaColor_origen { get; set; } = 0;

        /// <summary>
        /// Gets or sets the talla_origen.
        /// </summary>
        public string Talla_origen { get; set; } = "";

        /// <summary>
        /// Gets or sets the color_origen.
        /// </summary>
        public string Color_origen { get; set; } = "";

        /// <summary>
        /// GT17122025: se agregan propiedades para poder dar traza si una talla cambio a otra, igual con color
        /// </summary>
        public string Talla_destino { get; set; } = "";

        /// <summary>
        /// Gets or sets the color_destino.
        /// </summary>
        public string Color_destino { get; set; } = "";

        /// <summary>
        /// Gets or sets the identifier producto talla color destino.
        /// </summary>
        public int IdProductoTallaColor_destino { get; set; } = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="clsBeTrans_ajuste_det"/> class.
        /// </summary>
        public clsBeTrans_ajuste_det()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="clsBeTrans_ajuste_det"/> class.
        /// </summary>
        /// <param name="idajustedet">The idajustedet.</param>
        /// <param name="idajusteenc">The idajusteenc.</param>
        /// <param name="IdStock">The identifier stock.</param>
        /// <param name="IdPropietarioBodega">The identifier propietario bodega.</param>
        /// <param name="IdProductoBodega">The identifier producto bodega.</param>
        /// <param name="IdProductoEstado">The identifier producto estado.</param>
        /// <param name="IdPresentacion">The identifier presentacion.</param>
        /// <param name="IdUnidadMedida">The identifier unidad medida.</param>
        /// <param name="IdUbicacion">The identifier ubicacion.</param>
        /// <param name="lote_original">The lote_original.</param>
        /// <param name="lote_nuevo">The lote_nuevo.</param>
        /// <param name="fecha_vence_original">The fecha_vence_original.</param>
        /// <param name="fecha_vence_nueva">The fecha_vence_nueva.</param>
        /// <param name="peso_original">The peso_original.</param>
        /// <param name="peso_nuevo">The peso_nuevo.</param>
        /// <param name="cantidad_original">The cantidad_original.</param>
        /// <param name="cantidad_nueva">The cantidad_nueva.</param>
        /// <param name="codigo_producto">The codigo_producto.</param>
        /// <param name="nombre_producto">The nombre_producto.</param>
        /// <param name="idtipoajuste">The idtipoajuste.</param>
        /// <param name="idmotivoajuste">The idmotivoajuste.</param>
        /// <param name="observacion">The observacion.</param>
        /// <param name="codigo_ajuste">The codigo_ajuste.</param>
        /// <param name="enviado">if set to <c>true</c> [enviado].</param>
        public clsBeTrans_ajuste_det(
            int idajustedet,
            int idajusteenc,
            int IdStock,
            int IdPropietarioBodega,
            int IdProductoBodega,
            int IdProductoEstado,
            int IdPresentacion,
            int IdUnidadMedida,
            int IdUbicacion,
            string lote_original,
            string lote_nuevo,
            DateTime fecha_vence_original,
            DateTime fecha_vence_nueva,
            double peso_original,
            double peso_nuevo,
            double cantidad_original,
            double cantidad_nueva,
            string codigo_producto,
            string nombre_producto,
            int idtipoajuste,
            int idmotivoajuste,
            string observacion,
            string codigo_ajuste,
            bool enviado)
        {
            IdAjusteDet = idajustedet;
            IdAjusteEnc = idajusteenc;
            this.IdStock = IdStock;
            this.IdPropietarioBodega = IdPropietarioBodega;
            this.IdProductoBodega = IdProductoBodega;
            this.IdProductoEstado = IdProductoEstado;
            this.IdPresentacion = IdPresentacion;
            this.IdUnidadMedida = IdUnidadMedida;
            this.IdUbicacion = IdUbicacion;
            Lote_original = lote_original ?? "";
            Lote_nuevo = lote_nuevo ?? "";
            Fecha_vence_original = fecha_vence_original;
            Fecha_vence_nueva = fecha_vence_nueva;
            Peso_original = peso_original;
            Peso_nuevo = peso_nuevo;
            Cantidad_original = cantidad_original;
            Cantidad_nueva = cantidad_nueva;
            Codigo_producto = codigo_producto ?? "";
            Nombre_producto = nombre_producto ?? "";
            Idtipoajuste = idtipoajuste;
            IdMotivoAjuste = idmotivoajuste;
            Observacion = observacion ?? "";
            Codigo_ajuste = codigo_ajuste ?? "";
            Enviado = enviado;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}