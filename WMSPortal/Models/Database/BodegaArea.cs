using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class BodegaArea
    {
        public BodegaArea()
        {
            BodegaSectors = new HashSet<BodegaSector>();
        }

        public int IdArea { get; set; }
        public int IdBodega { get; set; }
        public string Descripcion { get; set; }
        public bool? Sistema { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public string Codigo { get; set; }
        public bool? Activo { get; set; }
        public double? Alto { get; set; }
        public double? Largo { get; set; }
        public double? Ancho { get; set; }
        public double? MargenIzquierdo { get; set; }
        public double? MargenDerecho { get; set; }
        public double? MargenSuperior { get; set; }
        public double? MargenInferior { get; set; }

        public virtual Bodega IdBodegaNavigation { get; set; }
        public virtual ICollection<BodegaSector> BodegaSectors { get; set; }
    }
}
