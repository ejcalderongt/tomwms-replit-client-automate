using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransOcEnc
    {
        public TransOcEnc()
        {
            TransOcDets = new HashSet<TransOcDet>();
            TransOcImagens = new HashSet<TransOcImagen>();
            TransReDetInfraccions = new HashSet<TransReDetInfraccion>();
            TransReOcs = new HashSet<TransReOc>();
            TransServicioEncs = new HashSet<TransServicioEnc>();
        }

        public int IdOrdenCompraEnc { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int IdProveedorBodega { get; set; }
        public int? IdTipoIngresoOc { get; set; }
        public int? IdEstadoOc { get; set; }
        public int? IdMotivoDevolucion { get; set; }
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
        public int? Idacuerdocomercial { get; set; }
        public int? IdOperadorBodegaDefecto { get; set; }
        public int? IdBodega { get; set; }
        public string NoDocumentoRecepcionErp { get; set; }
        public string NoDocumentoDevolucion { get; set; }
        public int? IdPedidoEncDevolucion { get; set; }

        public virtual TransOcEstado IdEstadoOcNavigation { get; set; }
        public virtual MotivoDevolucion IdMotivoDevolucionNavigation { get; set; }
        public virtual PropietarioBodega IdPropietarioBodegaNavigation { get; set; }
        public virtual ProveedorBodega IdProveedorBodegaNavigation { get; set; }
        public virtual TransOcTi IdTipoIngresoOcNavigation { get; set; }
        public virtual ICollection<TransOcDet> TransOcDets { get; set; }
        public virtual ICollection<TransOcImagen> TransOcImagens { get; set; }
        public virtual ICollection<TransReDetInfraccion> TransReDetInfraccions { get; set; }
        public virtual ICollection<TransReOc> TransReOcs { get; set; }
        public virtual ICollection<TransServicioEnc> TransServicioEncs { get; set; }
    }
}
