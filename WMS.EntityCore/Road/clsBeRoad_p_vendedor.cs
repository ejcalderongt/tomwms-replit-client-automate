namespace WMS.EntityCore.Road
{
    using System;

    public class clsBeRoad_p_vendedor : ICloneable, IDisposable
    {
        public int IdVendedor { get; set; } = 0;
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Clave { get; set; } = "";
        public string Ruta { get; set; } = "";
        public int Nivel { get; set; } = 0;
        public int Nivelprecio { get; set; } = 0;
        public string Bodega { get; set; } = "";
        public string Subbodega { get; set; } = "";
        public string Cod_vehiculo { get; set; } = "";
        public string Liquidando { get; set; } = "";
        public DateTime Ultima_fecha_liq { get; set; } = DateTime.Now;
        public bool Bloqueado { get; set; } = false;
        public int Devolucion_sap { get; set; } = 0;
        public int IdRuta { get; set; } = 0;

        public clsBeRoad_p_vendedor() { }

        public clsBeRoad_p_vendedor(int IdVendedor, string codigo, string nombre, string clave, string ruta, int nivel, int nivelprecio, string bodega, string subbodega, string cod_vehiculo, string liquidando, DateTime ultima_fecha_liq, bool bloqueado, int devolucion_sap)
        {
            this.IdVendedor = IdVendedor;
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Clave = clave;
            this.Ruta = ruta;
            this.Nivel = nivel;
            this.Nivelprecio = nivelprecio;
            this.Bodega = bodega;
            this.Subbodega = subbodega;
            this.Cod_vehiculo = cod_vehiculo;
            this.Liquidando = liquidando;
            this.Ultima_fecha_liq = ultima_fecha_liq;
            this.Bloqueado = bloqueado;
            this.Devolucion_sap = devolucion_sap;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}