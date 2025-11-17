namespace WMS.EntityCore.Road
{
    using System;
    public class clsBeRoad_ruta : ICloneable, IDisposable
    {
        public int IdRuta { get; set; } = 0;
        public int IdPropietarioBodega { get; set; } = 0;
        public int IdUbicacionTransito { get; set; } = 0;
        public string CODIGO { get; set; } = "";
        public string NOMBRE { get; set; } = "";
        public string ACTIVO { get; set; } = "";
        public string VENDEDOR { get; set; } = "";
        public string VENTA { get; set; } = "";
        public string FORANIA { get; set; } = "";
        public string SUCURSAL { get; set; } = "";
        public string TIPO { get; set; } = "";
        public string SUBTIPO { get; set; } = "";
        public string BODEGA { get; set; } = "";
        public string SUBBODEGA { get; set; } = "";
        public string DESCUENTO { get; set; } = "";
        public string BONIF { get; set; } = "";
        public string KILOMETRAJE { get; set; } = "";
        public string IMPRESION { get; set; } = "";
        public string RECIBOPROPIO { get; set; } = "";
        public string CELULAR { get; set; } = "";
        public string RENTABIL { get; set; } = "";
        public string OFERTA { get; set; } = "";
        public double PERCRENT { get; set; } = 0.0;
        public string PASARCREDITO { get; set; } = "";
        public string TECLADO { get; set; } = "";
        public string EDITDEVPREC { get; set; } = "";
        public string EDITDESC { get; set; } = "";
        public string PARAMS { get; set; } = "";
        public int SEMANA { get; set; } = 0;
        public int OBJANO { get; set; } = 0;
        public int OBJMES { get; set; } = 0;
        public string SYNCFOLD { get; set; } = "";
        public string WLFOLD { get; set; } = "";
        public string FTPFOLD { get; set; } = "";
        public string EMAIL { get; set; } = "";
        public int LASTIMP { get; set; } = 0;
        public int LASTCOM { get; set; } = 0;
        public int LASTEXP { get; set; } = 0;
        public string IMPSTAT { get; set; } = "";
        public string EXPSTAT { get; set; } = "";
        public string COMSTAT { get; set; } = "";
        public string PARAM1 { get; set; } = "";
        public string PARAM2 { get; set; } = "";
        public double PESOLIM { get; set; } = 0.0;
        public int INTERVALO_MAX { get; set; } = 0;
        public int LECTURAS_VALID { get; set; } = 0;
        public int INTENTOS_LECT { get; set; } = 0;
        public int HORA_INI { get; set; } = 0;
        public int HORA_FIN { get; set; } = 0;
        public int APLICACION_USA { get; set; } = 0;
        public int PUERTO_GPS { get; set; } = 0;
        public bool ES_RUTA_OFICINA { get; set; } = false;
        public bool DILUIR_BON { get; set; } = false;
        public bool PREIMPRESION_FACTURA { get; set; } = false;

        public clsBeRoad_ruta() { }

        public clsBeRoad_ruta(int IdRuta, int IdPropietarioBodega, int IdUbicacionTransito, string CODIGO, string NOMBRE, string ACTIVO, string VENDEDOR, string VENTA, string FORANIA, string SUCURSAL, string TIPO, string SUBTIPO, string BODEGA, string SUBBODEGA, string DESCUENTO, string BONIF, string KILOMETRAJE, string IMPRESION, string RECIBOPROPIO, string CELULAR, string RENTABIL, string OFERTA, double PERCRENT, string PASARCREDITO, string TECLADO, string EDITDEVPREC, string EDITDESC, string PARAMS, int SEMANA, int OBJANO, int OBJMES, string SYNCFOLD, string WLFOLD, string FTPFOLD, string EMAIL, int LASTIMP, int LASTCOM, int LASTEXP, string IMPSTAT, string EXPSTAT, string COMSTAT, string PARAM1, string PARAM2, double PESOLIM, int INTERVALO_MAX, int LECTURAS_VALID, int INTENTOS_LECT, int HORA_INI, int HORA_FIN, int APLICACION_USA, int PUERTO_GPS, bool ES_RUTA_OFICINA, bool DILUIR_BON, bool PREIMPRESION_FACTURA)
        {
            this.IdRuta = IdRuta;
            this.IdPropietarioBodega = IdPropietarioBodega;
            this.IdUbicacionTransito = IdUbicacionTransito;
            this.CODIGO = CODIGO;
            this.NOMBRE = NOMBRE;
            this.ACTIVO = ACTIVO;
            this.VENDEDOR = VENDEDOR;
            this.VENTA = VENTA;
            this.FORANIA = FORANIA;
            this.SUCURSAL = SUCURSAL;
            this.TIPO = TIPO;
            this.SUBTIPO = SUBTIPO;
            this.BODEGA = BODEGA;
            this.SUBBODEGA = SUBBODEGA;
            this.DESCUENTO = DESCUENTO;
            this.BONIF = BONIF;
            this.KILOMETRAJE = KILOMETRAJE;
            this.IMPRESION = IMPRESION;
            this.RECIBOPROPIO = RECIBOPROPIO;
            this.CELULAR = CELULAR;
            this.RENTABIL = RENTABIL;
            this.OFERTA = OFERTA;
            this.PERCRENT = PERCRENT;
            this.PASARCREDITO = PASARCREDITO;
            this.TECLADO = TECLADO;
            this.EDITDEVPREC = EDITDEVPREC;
            this.EDITDESC = EDITDESC;
            this.PARAMS = PARAMS;
            this.SEMANA = SEMANA;
            this.OBJANO = OBJANO;
            this.OBJMES = OBJMES;
            this.SYNCFOLD = SYNCFOLD;
            this.WLFOLD = WLFOLD;
            this.FTPFOLD = FTPFOLD;
            this.EMAIL = EMAIL;
            this.LASTIMP = LASTIMP;
            this.LASTCOM = LASTCOM;
            this.LASTEXP = LASTEXP;
            this.IMPSTAT = IMPSTAT;
            this.EXPSTAT = EXPSTAT;
            this.COMSTAT = COMSTAT;
            this.PARAM1 = PARAM1;
            this.PARAM2 = PARAM2;
            this.PESOLIM = PESOLIM;
            this.INTERVALO_MAX = INTERVALO_MAX;
            this.LECTURAS_VALID = LECTURAS_VALID;
            this.INTENTOS_LECT = INTENTOS_LECT;
            this.HORA_INI = HORA_INI;
            this.HORA_FIN = HORA_FIN;
            this.APLICACION_USA = APLICACION_USA;
            this.PUERTO_GPS = PUERTO_GPS;
            this.ES_RUTA_OFICINA = ES_RUTA_OFICINA;
            this.DILUIR_BON = DILUIR_BON;
            this.PREIMPRESION_FACTURA = PREIMPRESION_FACTURA;
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