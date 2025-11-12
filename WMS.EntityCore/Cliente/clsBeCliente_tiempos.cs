using WMS.EntityCore.Producto;

namespace WMS.EntityCore.Cliente
{
    public class clsBeCliente_tiempos : ICloneable, IDisposable
    {
        public int IdTiempoCliente { get; set; } = 0;
        public int IdCliente { get; set; } = 0;
        public int IdFamilia { get; set; } = 0;
        public int IdClasificacion { get; set; } = 0;
        public clsBeProducto_familia Familia { get; set; }= new clsBeProducto_familia();
        public clsBeProducto_clasificacion Clasificacion { get; set; } = new clsBeProducto_clasificacion();
        public int Dias_Local { get; set; } = 0;
        public int Dias_Exterior { get; set; } = 0;
        public string User_agr { get; set; } = "";
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = "";
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = false;

        public clsBeCliente_tiempos()
        {
        }

        public clsBeCliente_tiempos(int IdTiempoCliente, int IdCliente, int IdFamilia, int IdClasificacion,
                                   int Dias_Local, int Dias_Exterior, string user_agr, DateTime fec_agr,
                                   string user_mod, DateTime fec_mod, bool activo)
        {
            this.IdTiempoCliente = IdTiempoCliente;
            this.IdCliente = IdCliente;
            this.IdFamilia = IdFamilia;
            this.IdClasificacion = IdClasificacion;
            this.Dias_Local = Dias_Local;
            this.Dias_Exterior = Dias_Exterior;
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
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~clsBeCliente_tiempos()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);           
        }
        #endregion
    }
}
