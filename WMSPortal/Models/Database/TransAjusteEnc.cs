using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class TransAjusteEnc
    {
        public TransAjusteEnc()
        {
            TransAjusteDetDocs = new HashSet<TransAjusteDetDoc>();
            TransAjusteDets = new HashSet<TransAjusteDet>();
        }

        public int Idajusteenc { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Idusuario { get; set; }
        public string Referencia { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecMod { get; set; }
        public string UserMod { get; set; }
        public int? Idbodega { get; set; }
        public bool? EnviadoAErp { get; set; }
        public int? IdProductoFamilia { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public bool? AjustePorInventario { get; set; }

        public virtual ICollection<TransAjusteDetDoc> TransAjusteDetDocs { get; set; }
        public virtual ICollection<TransAjusteDet> TransAjusteDets { get; set; }
    }
}
