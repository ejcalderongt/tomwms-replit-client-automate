namespace WMS.EntityCore.Dtos.Ingresos
{
    public class DocumentoIngresoEstatusResponseDto
    {
        public string Referencia { get; set; } = "";
        public int IdOrdenCompraEnc { get; set; }
        public DocumentoIngresoEstadoOCDto EstadoOC { get; set; } = new();
        public DocumentoIngresoEstatusResumenDto Resumen { get; set; } = new();
        public List<DocumentoIngresoEstatusProductoDto> Productos { get; set; } = new();
    }

    public class DocumentoIngresoEstadoOCDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
    }

    public class DocumentoIngresoEstatusResumenDto
    {
        public int TotalLineas { get; set; }
        public int LineasCompletas { get; set; }
        public int LineasPendientes { get; set; }
        public bool RecepcionCompleta { get; set; }
        public bool TareasFinalizadas { get; set; }
    }

    public class DocumentoIngresoEstatusProductoDto
    {
        public int NoLinea { get; set; }
        public int IdOrdenCompraDet { get; set; }
        public int IdProductoBodega { get; set; }
        public string CodigoProducto { get; set; } = "";
        public string NombreProducto { get; set; } = "";
        public string Presentacion { get; set; } = "";
        public string UnidadMedida { get; set; } = "";
        public double CantidadSolicitada { get; set; }
        public double CantidadRecibida { get; set; }
        public double CantidadPendiente { get; set; }
        public bool RecepcionCompleta { get; set; }
        public DocumentoIngresoEstatusTareaResumenDto Tarea { get; set; } = new();
    }

    public class DocumentoIngresoEstatusTareaResumenDto
    {
        public int Total { get; set; }
        public int Finalizadas { get; set; }
        public int Pendientes { get; set; }
        public bool Finalizada { get; set; }
        public List<DocumentoIngresoEstatusTareaEstadoDto> Estados { get; set; } = new();
    }

    public class DocumentoIngresoEstatusTareaEstadoDto
    {
        public int IdEstado { get; set; }
        public string Estado { get; set; } = "";
        public int Cantidad { get; set; }
    }
}
