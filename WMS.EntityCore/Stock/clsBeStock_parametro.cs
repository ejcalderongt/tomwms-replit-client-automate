namespace WMS.EntityCore.Stock
{
    using System;

    [Serializable]
    public class clsBeStock_parametro : ICloneable, IDisposable
    {
        public int IdStockParametro { get; set; } = 0;
        public int IdStock { get; set; } = 0;
        public int IdProductoParametro { get; set; } = 0;
        public string Valor_texto { get; set; } = "";
        public double Valor_numerico { get; set; } = 0.0;
        public DateTime Valor_fecha { get; set; } = DateTime.Now;
        public bool Valor_logico { get; set; } = false;
        public string User_agr { get; set; } = "";
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = false;
        public bool IsNew { get; set; } = false;
        public clsBeStock_parametro() { }

        public clsBeStock_parametro(int IdStockParametro, int IdStock, int IdProductoParametro, string valor_texto, double valor_numerico, DateTime valor_fecha, bool valor_logico, string user_agr, DateTime fec_agr, bool activo)
        {
            this.IdStockParametro = IdStockParametro;
            this.IdStock = IdStock;
            this.IdProductoParametro = IdProductoParametro;
            this.Valor_texto = valor_texto;
            this.Valor_numerico = valor_numerico;
            this.Valor_fecha = valor_fecha;
            this.Valor_logico = valor_logico;
            this.User_agr = user_agr;
            this.Fec_agr = fec_agr;
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
        // ~clsBeStock_parametro()
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
