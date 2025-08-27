using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class BodegaSector
    {
        public BodegaSector()
        {
            BodegaTramos = new HashSet<BodegaTramo>();
        }

        public int IdBodega { get; set; }
        public int IdSector { get; set; }
        public int IdArea { get; set; }
        public bool? Sistema { get; set; }
        public string Descripcion { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
        public double? Alto { get; set; }
        public double? Largo { get; set; }
        public double? Ancho { get; set; }
        public double? MargenIzquierdo { get; set; }
        public double? MargenDerecho { get; set; }
        public double? MargenSuperior { get; set; }
        public double? MargenInferior { get; set; }
        public string Codigo { get; set; }
        public int? IdSectorIzquierda { get; set; }
        public int? IdSectorDerecha { get; set; }
        public bool? Horizontal { get; set; }
        public double? PosX { get; set; }
        public double? PosY { get; set; }

        public virtual BodegaArea Id { get; set; }
        public virtual ICollection<BodegaTramo> BodegaTramos { get; set; }
    }
}
