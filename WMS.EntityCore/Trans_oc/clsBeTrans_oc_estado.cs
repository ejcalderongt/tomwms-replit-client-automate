namespace WMS.EntityCore.Trans_oc
{
    using System;
    public class clsBeTrans_oc_estado : ICloneable, IDisposable
    {
        public int IdEstadoOC { get; set; } = 0;
        public string Nombre { get; set; } = string.Empty;

        public clsBeTrans_oc_estado() { }

        public clsBeTrans_oc_estado(int idEstadoOC, string nombre)
        {
            IdEstadoOC = idEstadoOC;
            Nombre = nombre;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Liberar recursos administrados si existieran
                }

                // Liberar recursos no administrados si existieran
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
