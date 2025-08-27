using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransInvEnc
    {
        public TransInvEnc()
        {
            TransInvCiclicos = new HashSet<TransInvCiclico>();
            TransInvDetalles = new HashSet<TransInvDetalle>();
            TransInvEncReconteos = new HashSet<TransInvEncReconteo>();
            TransInvOperadors = new HashSet<TransInvOperador>();
            TransInvReconteos = new HashSet<TransInvReconteo>();
            TransInvResumen = new HashSet<TransInvResuman>();
            TransInvStocks = new HashSet<TransInvStock>();
            TransInvTramos = new HashSet<TransInvTramo>();
        }

        public int Idinventarioenc { get; set; }
        public int? Idpropietario { get; set; }
        public int? Idbodega { get; set; }
        public int? Idtipoinventario { get; set; }
        public int? TipoConteoProducto { get; set; }
        public bool? DobleVerificacion { get; set; }
        public DateTime? Fecha { get; set; }
        public string Estado { get; set; }
        public bool? Inicial { get; set; }
        public bool? Activo { get; set; }
        public bool? Regularizado { get; set; }
        public DateTime? HoraIni { get; set; }
        public DateTime? HoraFin { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? EsSistema { get; set; }
        public bool? CambiaUbicacion { get; set; }
        public DateTime? FechaUltimoInventario { get; set; }
        public bool? MostrarCantidadTeoricaHh { get; set; }
        public int? IdProductoFamilia { get; set; }
        public int? IdBodegaVirtual { get; set; }
        public bool? CapturarNoExistente { get; set; }
        public bool? MultiPropietario { get; set; }

        public virtual ICollection<TransInvCiclico> TransInvCiclicos { get; set; }
        public virtual ICollection<TransInvDetalle> TransInvDetalles { get; set; }
        public virtual ICollection<TransInvEncReconteo> TransInvEncReconteos { get; set; }
        public virtual ICollection<TransInvOperador> TransInvOperadors { get; set; }
        public virtual ICollection<TransInvReconteo> TransInvReconteos { get; set; }
        public virtual ICollection<TransInvResuman> TransInvResumen { get; set; }
        public virtual ICollection<TransInvStock> TransInvStocks { get; set; }
        public virtual ICollection<TransInvTramo> TransInvTramos { get; set; }
    }
}
