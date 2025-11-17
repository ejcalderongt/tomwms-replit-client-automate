namespace WMS.DALCore.Datos_Maestros
{
    using System;

    public class clsBeTipo_rotacion : ICloneable, IDisposable
    {
        public int IdTipoRotacion { get; set; } = 0;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; } = false;
        public int IdProductoBodega { get; set; } = 0;

        public clsBeTipo_rotacion()
        {
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null.
                disposedValue = true;
            }
        }

        // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~clsBeTipo_rotacion()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
