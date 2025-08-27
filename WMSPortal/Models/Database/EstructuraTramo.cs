using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class EstructuraTramo
    {
        public EstructuraTramo()
        {
            EstructuraGrupos = new HashSet<EstructuraGrupo>();
            EstructuraUbicacions = new HashSet<EstructuraUbicacion>();
        }

        public int IdTramo { get; set; }
        public int IdSector { get; set; }
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
        public int? IndiceX { get; set; }
        public int? Orientacion { get; set; }
        public int? IdTipoProductoDefault { get; set; }
        public int? IdFontEnc { get; set; }
        public int? Idbodega { get; set; }
        public bool? Horizontal { get; set; }
        public int? Idarea { get; set; }

        public virtual ICollection<EstructuraGrupo> EstructuraGrupos { get; set; }
        public virtual ICollection<EstructuraUbicacion> EstructuraUbicacions { get; set; }
    }
}
