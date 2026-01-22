namespace WMS.EntityCore.Stock
{
    using System;

    [Serializable]
    public class clsBeStock_se : ICloneable, IDisposable
    {
        public int IdStockSe { get; set; } = 0;
        public int IdStock { get; set; } = 0;
        public int IdProductoBodega { get; set; } = 0;
        public string NoSerie { get; set; } = "";
        public string NoSerieInicial { get; set; } = "";
        public string NoSerieFinal { get; set; } = "";
        public string User_agr { get; set; } = "";
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = "";
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = false;

        public clsBeStock_se() { }

        public clsBeStock_se(int IdStockSe, int IdStock, int IdProductoBodega, string NoSerie, string NoSerieInicial, string NoSerieFinal, string user_agr, DateTime fec_agr, string user_mod, DateTime fec_mod, bool activo)
        {
            this.IdStockSe = IdStockSe;
            this.IdStock = IdStock;
            this.IdProductoBodega = IdProductoBodega;
            this.NoSerie = NoSerie;
            this.NoSerieInicial = NoSerieInicial;
            this.NoSerieFinal = NoSerieFinal;
            this.User_agr = user_agr;
            this.Fec_agr = fec_agr;
            this.User_mod = user_mod;
            this.Fec_mod = fec_mod;
            this.Activo = activo;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~clsBeStock_se()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
