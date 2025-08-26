using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class BodegaUbicacion
    {
        public BodegaUbicacion()
        {
            StockRecs = new HashSet<StockRec>();
            Stocks = new HashSet<Stock>();
            TransUbicHhDetIdNavigations = new HashSet<TransUbicHhDet>();
            TransUbicHhDetIds = new HashSet<TransUbicHhDet>();
        }

        public int IdUbicacion { get; set; }
        public int IdTramo { get; set; }
        public string Descripcion { get; set; }
        public double? Ancho { get; set; }
        public double? Largo { get; set; }
        public double? Alto { get; set; }
        public int? Nivel { get; set; }
        public int? IndiceX { get; set; }
        public int? IdIndiceRotacion { get; set; }
        public int? IdTipoRotacion { get; set; }
        public bool? Sistema { get; set; }
        public string CodigoBarra { get; set; }
        public string CodigoBarra2 { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Dañado { get; set; }
        public bool? Activo { get; set; }
        public bool? Bloqueada { get; set; }
        public bool? AceptaPallet { get; set; }
        public bool? UbicacionPicking { get; set; }
        public bool? UbicacionRecepcion { get; set; }
        public bool? UbicacionDespacho { get; set; }
        public bool? UbicacionMerma { get; set; }
        public double? MargenIzquierdo { get; set; }
        public double? MargenDerecho { get; set; }
        public double? MargenSuperior { get; set; }
        public double? MargenInferior { get; set; }
        public string OrientacionPos { get; set; }
        public bool? UbicacionVirtual { get; set; }
        public bool? UbicacionNe { get; set; }
        public int IdBodega { get; set; }
        public int IdArea { get; set; }
        public int IdSector { get; set; }

        public virtual BodegaTramo Id { get; set; }
        public virtual TipoRotacion IdTipoRotacionNavigation { get; set; }
        public virtual ICollection<StockRec> StockRecs { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<TransUbicHhDet> TransUbicHhDetIdNavigations { get; set; }
        public virtual ICollection<TransUbicHhDet> TransUbicHhDetIds { get; set; }
    }
}
