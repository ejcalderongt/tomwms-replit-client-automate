namespace WMSWebAPI.Dtos.Stock
{
    public class StockResDto
    {
        public int IdBodega { get; set; }
        public string Bodega { get; set; } = string.Empty;
        public int IdPropietario { get; set; }
        public int IdPropietarioBodega { get; set; }
        public string Propietario { get; set; } = string.Empty;
        public int IdProducto { get; set; }
        public int IdProductoBodega { get; set; }
        public int IdStock { get; set; }
        public int IdUbicacion_anterior { get; set; }
        public int IdUnidadMedida { get; set; }
        public int IdProductoEstado { get; set; }
        public int IdPresentacion { get; set; }
        public int IdRecepcionEnc { get; set; }
        public string codigo { get; set; } = string.Empty;
        public string codigo_barra { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public string UnidadMedida { get; set; } = string.Empty;
        public string Presentacion { get; set; } = string.Empty;
        public string lote { get; set; } = string.Empty;
        public DateTime fecha_ingreso { get; set; }
        public DateTime fecha_vence { get; set; }
        public float Cantidad_UMBas { get; set; }
        public float CantidadSF { get; set; }
        public float Cantidad_Presentacion { get; set; }
        public float factor { get; set; }
        public float CantidadReservadaUmBas { get; set; }
        public float Cantidad_Reservada_Pres { get; set; }
        public float Disponible_UMBas { get; set; }
        public float Disponible_Presentacion { get; set; }
        public float peso { get; set; }
        public string NomEstado { get; set; } = string.Empty;
        public bool dañado { get; set; }
        public bool EstadoUtilizable { get; set; }
        public int IdUbicacion { get; set; }
        public string lic_plate { get; set; } = string.Empty;
        public string serial { get; set; } = string.Empty;
        public int añada { get; set; }
        public int IdIndiceRotacion { get; set; }
        public float alto { get; set; }
        public float largo { get; set; }
        public float ancho { get; set; }
        public int IdTramo { get; set; }
        public float Ancho_ubicacion { get; set; }
        public float Largo_ubicacion { get; set; }
        public float Alto_ubicacion { get; set; }
        public string IndiceRotacion { get; set; } = string.Empty;
        public float Existencia_min_umbas { get; set; }
        public float Existencia_max_umbas { get; set; }
        public float costo { get; set; }
        public float Existencia_min_pres { get; set; }
        public float Existencia_max_pres { get; set; }
        public int IdUbicacionActual { get; set; }
        public int Ubicacion_Nivel { get; set; }
        public int Ubicacion_Indice_X { get; set; }
        public string Ubicacion_Nombre { get; set; } = string.Empty;
        public string Ubicacion_Tramo { get; set; } = string.Empty;
        public bool activo { get; set; }
        public bool bloqueada { get; set; }
        public bool ubicacion_merma { get; set; }
        public string MotivoDevolucion { get; set; } = string.Empty;
        public string Codigo_Poliza { get; set; } = string.Empty;
        public string Numero_poliza { get; set; } = string.Empty;
        public string numero_orden { get; set; } = string.Empty;
        public string Familia { get; set; } = string.Empty;
        public string NoTO { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string Clasificacion { get; set; } = string.Empty;
        public int IdProductoParametroA { get; set; }
        public int IdProductoParametroB { get; set; }
        public string atributo_variante_1 { get; set; } = string.Empty;
        public string parametro_a { get; set; } = string.Empty;
        public string parametro_b { get; set; } = string.Empty;
        public string parametro_personalizado { get; set; } = string.Empty;
        public string parametro_valor { get; set; } = string.Empty;
        public int IdTipoProducto { get; set; }
        public string tipo { get; set; } = string.Empty;
        public int IdMarca { get; set; }
        public string marca { get; set; } = string.Empty;
        public float cantidad { get; set; }
        public int doc_ingreso { get; set; }
        public int posiciones { get; set; }
        public float valor_unitario { get; set; }
        public float valor_total { get; set; }
        public int IdEmbarcador { get; set; }
        public string Shipper { get; set; } = string.Empty;
        public string Regimen { get; set; } = string.Empty;
        public string ingreso_ticket { get; set; } = string.Empty;
        public int IdEmpresa { get; set; }
        public int IdTipoEtiqueta { get; set; }
        public bool ubicacion_picking { get; set; }
        public float CamasPorTarima { get; set; }
        public float CajasPorCama { get; set; }
        public long IdPedido { get; set; }
        public bool pallet_no_estandar { get; set; }
        public string Embarcador { get; set; } = string.Empty;
        public int Documento_Ingreso { get; set; }
        public float CantidadReservada { get; set; }
        public string Nombre_Completo { get; set; } = string.Empty;
        public bool es_rack { get; set; }
        public string ReferenciaOCEnc { get; set; } = string.Empty;
        public float precio_producto { get; set; }
        public float costo_producto { get; set; }
        public float costo_ingreso { get; set; }
        public int IdRecepcionDet { get; set; }
        public string Ingreso { get; set; } = string.Empty;
        public bool es_devolucion { get; set; }
        public int No_Linea { get; set; }
        public int IdArea { get; set; }
        public string Referencia { get; set; } = string.Empty;
        public string No_Docto { get; set; } = string.Empty;
        public string No_Contenedor { get; set; } = string.Empty;
        public int IdTipoRotacion { get; set; }
    }
}
