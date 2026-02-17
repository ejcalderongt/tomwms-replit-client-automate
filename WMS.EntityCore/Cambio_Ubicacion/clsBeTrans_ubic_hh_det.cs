using WMS.EntityCore.Operador;
using WMS.EntityCore.Producto;
using WMSWebAPI.Be;

namespace WMS.EntityCore.Cambio_Ubicacion
{
    public class clsBeTrans_ubic_hh_det : ICloneable
    {
        public int IdTareaUbicacionEnc { get; set; } = 0;
        public int IdTareaUbicacionDet { get; set; } = 0;
        public int IdStock { get; set; } = 0;
        public int IdUbicacionOrigen { get; set; } = 0;
        public int IdUbicacionDestino { get; set; } = 0;
        public int IdEstadoOrigen { get; set; } = 0;
        public int IdEstadoDestino { get; set; } = 0;
        public int IdOperadorBodega { get; set; } = 0;
        public DateTime HoraInicio { get; set; } = DateTime.Now;
        public DateTime HoraFin { get; set; } = DateTime.Now;
        public bool Realizado { get; set; } = false;
        public double Cantidad { get; set; } = 0.0;
        public bool Activo { get; set; } = false;
        public double Recibido { get; set; } = 0.0;
        public string Estado { get; set; } = "";
        public string Atributo_variante_1 { get; set; } = "";
        public int IdBodega { get; set; } = 0;
        public int No_Linea { get; set; } = 0;
        public clsBeOperador_bodega Operador { get; set; }= new clsBeOperador_bodega();
        public clsBeBodega_ubicacion UbicacionOrigen { get; set; } = new clsBeBodega_ubicacion();
        public clsBeBodega_ubicacion UbicacionDestino { get; set; } = new clsBeBodega_ubicacion();
        public clsBeTrans_ubic_hh_stock Stock { get; set; }= new clsBeTrans_ubic_hh_stock();
        public string EstadoOrigen { get; set; } = "";
        public string EstadoDestino { get; set; } = "";
        public clsBeProducto Producto { get; set; }= new clsBeProducto();
        public string NombreUbicacionOrigen { get; set; } = "";
        public string NombreUbicacionDestino { get; set; } = "";
        public string CodigoProducto { get; set; } = "";
        public string NombreProducto { get; set; } = "";
        public string Licencia { get; set; } = "";
        public string Lote { get; set; } = "";
        public DateOnly FechaVence { get; set; } = new DateOnly(1900, 1, 1);
        public DateTime FechaIngreso { get; set; } = new DateTime(1900, 1, 1);
        public string Serial { get; set; } = "";
        public string UnidadMedida { get; set; } = "";
        public string Presentacion { get; set; } = "";
        public string NombreOperador { get; set; } = "";

        public clsBeTrans_ubic_hh_det()
        {
        }

        public clsBeTrans_ubic_hh_det(
            int IdTareaUbicacionEnc,
            int IdTareaUbicacionDet,
            int IdStock,
            int IdUbicacionOrigen,
            int IdUbicacionDestino,
            int IdEstadoOrigen,
            int IdEstadoDestino,
            int IdOperador,
            DateTime HoraInicio,
            DateTime HoraFin,
            bool Realizado,
            double cantidad,
            bool activo,
            double recibido,
            string estado,
            string atributo_variante_1,
            int IdBodega)
        {
            this.IdTareaUbicacionEnc = IdTareaUbicacionEnc;
            this.IdTareaUbicacionDet = IdTareaUbicacionDet;
            this.IdStock = IdStock;
            this.IdUbicacionOrigen = IdUbicacionOrigen;
            this.IdUbicacionDestino = IdUbicacionDestino;
            this.IdEstadoOrigen = IdEstadoOrigen;
            this.IdEstadoDestino = IdEstadoDestino;
            this.IdOperadorBodega = IdOperador;
            this.HoraInicio = HoraInicio;
            this.HoraFin = HoraFin;
            this.Realizado = Realizado;
            this.Cantidad = cantidad;
            this.Activo = activo;
            this.Recibido = recibido;
            this.Estado = estado;
            this.Atributo_variante_1 = atributo_variante_1;
            this.IdBodega = IdBodega;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}