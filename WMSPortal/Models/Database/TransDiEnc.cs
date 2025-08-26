using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransDiEnc
    {
        public int IdTransDienc { get; set; }
        public int? IdTipoIngresoOc { get; set; }
        public int? IdEstadoOc { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? HoraCreacion { get; set; }
        public string NoDocumento { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public string Procedencia { get; set; }
        public string NoMarchamo { get; set; }
        public string Referencia { get; set; }
        public string Observacion { get; set; }
        public bool? ControlPoliza { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public DateTime? HoraInicioRecepcion { get; set; }
        public DateTime? HoraFinRecepcion { get; set; }
        public int? IdMuelleRecepcion { get; set; }
        public bool? ProgramarRecepcion { get; set; }
        public int? IdMotivoAnulacionBodega { get; set; }
        public bool? EnviadoAErp { get; set; }
        public string Serie { get; set; }
        public int? Correlativo { get; set; }
        public int? IdDespachoEnc { get; set; }
        public string NoTicketTms { get; set; }
        public int? IdNoDocumentoRef { get; set; }

        public virtual TransOcEstado IdEstadoOcNavigation { get; set; }
        public virtual TransOcTi IdTipoIngresoOcNavigation { get; set; }
    }
}
