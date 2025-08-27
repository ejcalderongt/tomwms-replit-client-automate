namespace WMSWebAPI.Dtos.Movimientos
{
    namespace WMSWebAPI.Dto
    {
        public class MovimientosDto
        {
            public int IdMovimiento { get; set; }
            public int IdEmpresa { get; set; }
            public int IdBodegaOrigen { get; set; }
            public int IdTransaccion { get; set; }
            public int IdPropietarioBodega { get; set; }
            public int IdProductoBodega { get; set; }
            public int IdUbicacionOrigen { get; set; }
            public int IdUbicacionDestino { get; set; }
            public int IdPresentacion { get; set; }
            public int IdEstadoOrigen { get; set; }
            public int IdEstadoDestino { get; set; }
            public int IdUnidadMedida { get; set; }
            public int IdTipoTarea { get; set; }
            public int IdBodegaDestino { get; set; }
            public int IdRecepcion { get; set; }
            public double Cantidad { get; set; }
            public string Serie { get; set; } = string.Empty;
            public double Peso { get; set; }
            public string Lote { get; set; } = string.Empty;
            public DateTime Fecha_vence { get; set; }
            public DateTime Fecha { get; set; }
            public string Barra_pallet { get; set; } = string.Empty;
            public DateTime Hora_ini { get; set; }
            public DateTime Hora_fin { get; set; }
            public DateTime Fecha_agr { get; set; }
            public string Usuario_agr { get; set; } = string.Empty;
            public double Cantidad_hist { get; set; }
            public double Peso_hist { get; set; }
            public string Lic_plate { get; set; } = string.Empty;
            public int IdOperadorBodega { get; set; }
            public int IdRecepcionDet { get; set; }
            public int IdPedidoEnc { get; set; }
            public int IdPedidoDet { get; set; }
            public int IdDespachoEnc { get; set; }
            public int IdDespachoDet { get; set; }                        
        }

        public class MovimientosRptDto
        {
            public string Propietario { get; set; } = string.Empty;
            public string Producto { get; set; } = string.Empty;
            public string Poliza { get; set; } = string.Empty;
            public string Presentacion { get; set; } = string.Empty;
            public string EstadoOrigen { get; set; } = string.Empty;
            public string EstadoDestino { get; set; } = string.Empty;
            public string UMBas { get; set; } = string.Empty;
            public float cantidad { get; set; }
            public float peso { get; set; }
            public string lote { get; set; } = string.Empty;
            public string UbicOrigen { get; set; } = string.Empty;
            public string UbicDestino { get; set; } = string.Empty;
            public string TipoTarea { get; set; } = string.Empty;
            public DateTime fecha { get; set; }
            public int IdProducto { get; set; }
            public string codigo { get; set; } = string.Empty;
            public string CodigoBarra { get; set; } = string.Empty;
            public int IdTipoTarea { get; set; }
            public bool Contabilizar { get; set; }
            public DateTime fecha_vence { get; set; }
            public int IdTipoActualizacionCosto { get; set; }
            public int IdPresentacion { get; set; }
            public int IdUnidadMedida { get; set; }
            public int IdEstadoOrigen { get; set; }
            public int IdProductoBodega { get; set; }
            public int IdPropietarioBodega { get; set; }
            public int IdBodega { get; set; }
            public string Licencia { get; set; } = string.Empty;
            public string Clasificacion { get; set; } = string.Empty;
            public string Familia { get; set; } = string.Empty;
            public int IdBodegaOrigen { get; set; }
            public int IdBodegaDestino { get; set; }
            public string Codigo_Bodega_Destino { get; set; } = string.Empty;
            public string Nombre_Bodega_Destino { get; set; } = string.Empty;
            public int IdMovimiento { get; set; }
            public string Codigo_Bodega_Origen { get; set; } = string.Empty;
            public string Nombre_Bodega_Origen { get; set; } = string.Empty;
            public string NombreArea { get; set; } = string.Empty;
            public float factor { get; set; }
            public string IdTicketTMS { get; set; } = string.Empty;
            public string Operador { get; set; } = string.Empty;
            public int IdUbicacionDestino { get; set; }
            public int IdUbicacionOrigen { get; set; }
            public int IdPropietario { get; set; }
        }
    }
}