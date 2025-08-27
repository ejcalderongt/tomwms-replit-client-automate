using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class PropietarioBodega
    {
        public PropietarioBodega()
        {
            ProductoPallets = new HashSet<ProductoPallet>();
            ReglaUbicDetProps = new HashSet<ReglaUbicDetProp>();
            StockHists = new HashSet<StockHist>();
            StockRecs = new HashSet<StockRec>();
            Stocks = new HashSet<Stock>();
            TransDespachoEncs = new HashSet<TransDespachoEnc>();
            TransInventarioEncs = new HashSet<TransInventarioEnc>();
            TransMovimientos = new HashSet<TransMovimiento>();
            TransOcEncs = new HashSet<TransOcEnc>();
            TransPeEncs = new HashSet<TransPeEnc>();
            TransPickingEncs = new HashSet<TransPickingEnc>();
            TransReEncs = new HashSet<TransReEnc>();
            TransTrasEncs = new HashSet<TransTrasEnc>();
            TransUbicHhEncs = new HashSet<TransUbicHhEnc>();
            TransUbicHhStocks = new HashSet<TransUbicHhStock>();
            TransaccionesLogs = new HashSet<TransaccionesLog>();
        }

        public int IdPropietarioBodega { get; set; }
        public int? IdPropietario { get; set; }
        public int? IdBodega { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }

        public virtual Bodega IdBodegaNavigation { get; set; }
        public virtual Propietario IdPropietarioNavigation { get; set; }
        public virtual ICollection<ProductoPallet> ProductoPallets { get; set; }
        public virtual ICollection<ReglaUbicDetProp> ReglaUbicDetProps { get; set; }
        public virtual ICollection<StockHist> StockHists { get; set; }
        public virtual ICollection<StockRec> StockRecs { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<TransDespachoEnc> TransDespachoEncs { get; set; }
        public virtual ICollection<TransInventarioEnc> TransInventarioEncs { get; set; }
        public virtual ICollection<TransMovimiento> TransMovimientos { get; set; }
        public virtual ICollection<TransOcEnc> TransOcEncs { get; set; }
        public virtual ICollection<TransPeEnc> TransPeEncs { get; set; }
        public virtual ICollection<TransPickingEnc> TransPickingEncs { get; set; }
        public virtual ICollection<TransReEnc> TransReEncs { get; set; }
        public virtual ICollection<TransTrasEnc> TransTrasEncs { get; set; }
        public virtual ICollection<TransUbicHhEnc> TransUbicHhEncs { get; set; }
        public virtual ICollection<TransUbicHhStock> TransUbicHhStocks { get; set; }
        public virtual ICollection<TransaccionesLog> TransaccionesLogs { get; set; }
    }
}
