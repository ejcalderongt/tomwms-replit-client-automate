using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwTransServicio
    {
        public int IdServicioEnc { get; set; }
        public int IdBodega { get; set; }
        public int IdPropietario { get; set; }
        public string CodigoBodega { get; set; }
        public string Bodega { get; set; }
        public string Propietario { get; set; }
        public int IdDocumentoIngreso { get; set; }
        public string DocumentoIngreso { get; set; }
        public int? IdDocumentoSalida { get; set; }
        public string RefDocSalida { get; set; }
        public long? DocumentoSalida { get; set; }
        public string NoOrden { get; set; }
        public string NoPoliza { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? Activo { get; set; }
        public string EstadoServicio { get; set; }
        public bool Mi3Estatus { get; set; }
        public bool? EsIngreso { get; set; }
    }
}
