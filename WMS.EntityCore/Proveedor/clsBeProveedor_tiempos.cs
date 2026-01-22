using WMS.EntityCore.Producto;

namespace WMS.EntityCore.Proveedor
{
    public class clsBeProveedor_tiempos : ICloneable
    {
        public int IdTiempoProveedor { get; set; } = 0;
        public int IdProveedor { get; set; } = 0;
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

        public clsBeProveedor_tiempos()
        {
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
    }
}
