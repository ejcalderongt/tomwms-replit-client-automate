using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransInventarioEnc
    {
        public TransInventarioEnc()
        {
            TransInventarioDets = new HashSet<TransInventarioDet>();
        }

        public long IdInventarioEnc { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public int IdBodega { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? HoraIni { get; set; }
        public DateTime? HoraFin { get; set; }
        public string Estado { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public string TipoConteo { get; set; }
        public bool? ActualizaVal { get; set; }
        public int? TipoInv { get; set; }

        public virtual PropietarioBodega IdPropietarioBodegaNavigation { get; set; }
        public virtual ICollection<TransInventarioDet> TransInventarioDets { get; set; }
    }
}
