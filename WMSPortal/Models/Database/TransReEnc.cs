using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransReEnc
    {
        public TransReEnc()
        {
            ProductoPallets = new HashSet<ProductoPallet>();
            TransReDetInfraccions = new HashSet<TransReDetInfraccion>();
            TransReDets = new HashSet<TransReDet>();
            TransReFacts = new HashSet<TransReFact>();
            TransReImgs = new HashSet<TransReImg>();
            TransReOcs = new HashSet<TransReOc>();
            TransReOps = new HashSet<TransReOp>();
        }

        public int IdRecepcionEnc { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public int? IdMuelle { get; set; }
        public int? IdUbicacionRecepcion { get; set; }
        public string IdTipoTransaccion { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public DateTime? HoraIniPc { get; set; }
        public DateTime? HoraFinPc { get; set; }
        public bool? MuestraPrecio { get; set; }
        public string Estado { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public DateTime? FechaTarea { get; set; }
        public bool? TomarFotos { get; set; }
        public bool? EscanearRecUbic { get; set; }
        public bool? ParaPorCodigo { get; set; }
        public string Observacion { get; set; }
        public byte[] FirmaPiloto { get; set; }
        public bool? Activo { get; set; }
        public string NoGuia { get; set; }
        public bool? CorreoEnviado { get; set; }
        public bool? RevisionInconsistencia { get; set; }
        public bool? Bloqueada { get; set; }
        public string BloqueadaPor { get; set; }
        public int? Idusuariobloqueo { get; set; }
        public int? Idmotivoanulacionbodega { get; set; }
        public bool HabilitarStock { get; set; }
        public int? Idvehiculo { get; set; }
        public int? Idpiloto { get; set; }
        public string NoMarchamo { get; set; }
        public bool? MostrarCantidadEsperada { get; set; }
        public int? IdBodega { get; set; }

        public virtual BodegaMuelle IdMuelleNavigation { get; set; }
        public virtual PropietarioBodega IdPropietarioBodegaNavigation { get; set; }
        public virtual TransReTr IdTipoTransaccionNavigation { get; set; }
        public virtual ICollection<ProductoPallet> ProductoPallets { get; set; }
        public virtual ICollection<TransReDetInfraccion> TransReDetInfraccions { get; set; }
        public virtual ICollection<TransReDet> TransReDets { get; set; }
        public virtual ICollection<TransReFact> TransReFacts { get; set; }
        public virtual ICollection<TransReImg> TransReImgs { get; set; }
        public virtual ICollection<TransReOc> TransReOcs { get; set; }
        public virtual ICollection<TransReOp> TransReOps { get; set; }
    }
}
