namespace WMS.EntityCore.Dtos.Inventario
{
    public class InventarioFiltroDto
    {
        public int? IdBodega { get; set; }
        public int? IdPropietario { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public int? IdProducto { get; set; }
        public int? IdProductoBodega { get; set; }
        public int? IdUbicacion { get; set; }
        public int? IdProductoEstado { get; set; }
        public string? Codigo { get; set; }
        public string? Lote { get; set; }
        public string? Licencia { get; set; }
        public string? Ubicacion { get; set; }
        public string? Familia { get; set; }
        public string? Area { get; set; }
        public string? Clasificacion { get; set; }
        public DateTime? VenceDesde { get; set; }
        public DateTime? VenceHasta { get; set; }
        public bool SoloDisponible { get; set; } = true;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 100;
    }

    public class InventarioPagedResultDto<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public IReadOnlyList<T> Data { get; set; } = [];
    }

    public class InventarioExistenciaLoteDto
    {
        public int IdBodega { get; set; }
        public string Bodega { get; set; } = string.Empty;
        public int IdPropietario { get; set; }
        public int IdPropietarioBodega { get; set; }
        public string Propietario { get; set; } = string.Empty;
        public int IdProducto { get; set; }
        public int IdProductoBodega { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string CodigoBarra { get; set; } = string.Empty;
        public string Producto { get; set; } = string.Empty;
        public string UnidadMedida { get; set; } = string.Empty;
        public string Presentacion { get; set; } = string.Empty;
        public double Factor { get; set; }
        public int IdStock { get; set; }
        public int IdStockRes { get; set; }
        public int IdUbicacion { get; set; }
        public int IdTramo { get; set; }
        public string UbicacionTramo { get; set; } = string.Empty;
        public string UbicacionNombre { get; set; } = string.Empty;
        public string UbicacionCompleta { get; set; } = string.Empty;
        public int UbicacionNivel { get; set; }
        public string Estado { get; set; } = string.Empty;
        public bool EstadoUtilizable { get; set; }
        public bool Danado { get; set; }
        public string Lote { get; set; } = string.Empty;
        public string Licencia { get; set; } = string.Empty;
        public string Serial { get; set; } = string.Empty;
        public int IdRecepcionEnc { get; set; }
        public int IdRecepcionDet { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaVence { get; set; }
        public double CantidadUMBas { get; set; }
        public double CantidadPresentacion { get; set; }
        public double CantidadReservadaUMBas { get; set; }
        public double DisponibleUMBas { get; set; }
        public double DisponiblePresentacion { get; set; }
        public double Peso { get; set; }
        public string Familia { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string Clasificacion { get; set; } = string.Empty;
        public string CodigoPoliza { get; set; } = string.Empty;
        public string NumeroPoliza { get; set; } = string.Empty;
        public bool UbicacionPicking { get; set; }
    }

    public class InventarioResumenDto
    {
        public string Grupo { get; set; } = string.Empty;
        public int? IdBodega { get; set; }
        public string? Bodega { get; set; }
        public int? IdPropietario { get; set; }
        public string? Propietario { get; set; }
        public int? IdProducto { get; set; }
        public int? IdProductoBodega { get; set; }
        public string? Codigo { get; set; }
        public string? Producto { get; set; }
        public string? Lote { get; set; }
        public string? Licencia { get; set; }
        public int? IdUbicacion { get; set; }
        public string? Ubicacion { get; set; }
        public string? Estado { get; set; }
        public string? Familia { get; set; }
        public string? Area { get; set; }
        public string? Clasificacion { get; set; }
        public string? UnidadMedida { get; set; }
        public string? Presentacion { get; set; }
        public double CantidadUMBas { get; set; }
        public double CantidadPresentacion { get; set; }
        public double CantidadReservadaUMBas { get; set; }
        public double DisponibleUMBas { get; set; }
        public double DisponiblePresentacion { get; set; }
        public int LineasStock { get; set; }
    }
}
