using WMSWebAPI.Be;

namespace WMSWebAPI.Entity.Despacho
{
    public class clsBeTrans_despacho_enc : ICloneable, IDisposable
    {
        public int IdDespachoEnc { get; set; } = 0;
        public int IdBodega { get; set; } = 0;
        public int IdPropietarioBodega { get; set; } = 0;
        public int IdVehiculo { get; set; } = 0;
        public int IdPiloto { get; set; } = 0;
        public int IdRuta { get; set; } = 0;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int No_pase { get; set; } = 0;
        public string Observacion { get; set; } = string.Empty;
        public DateTime Hora_ini { get; set; } = DateTime.MinValue;
        public DateTime Hora_fin { get; set; } = DateTime.Now;
        public string Estado { get; set; } = string.Empty;
        public double Numero { get; set; } = 0;
        public string Marchamo { get; set; } = string.Empty;
        public double Cant_bultos { get; set; } = 0.0;
        public string User_agr { get; set; } = string.Empty;
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = string.Empty;
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = false;
        public string No_Documento_Externo { get; set; } = string.Empty;
        public List<clsBeTrans_despacho_det> Detalle { get; set; } = new List<clsBeTrans_despacho_det>();
        public clsBeTrans_despacho_enc() { }

        public clsBeTrans_despacho_enc(
            int idDespachoEnc, int idBodega, int idPropietarioBodega, int idVehiculo, int idPiloto, int idRuta,
            DateTime fecha, int no_pase, string observacion, DateTime hora_ini, DateTime hora_fin,
            string estado, double numero, string marchamo, double cant_bultos,
            string user_agr, DateTime fec_agr, string user_mod, DateTime fec_mod, bool activo)
        {
            IdDespachoEnc = idDespachoEnc;
            IdBodega = idBodega;
            IdPropietarioBodega = idPropietarioBodega;
            IdVehiculo = idVehiculo;
            IdPiloto = idPiloto;
            IdRuta = idRuta;
            Fecha = fecha;
            No_pase = no_pase;
            Observacion = observacion;
            Hora_ini = hora_ini;
            Hora_fin = hora_fin;
            Estado = estado;
            Numero = numero;
            Marchamo = marchamo;
            Cant_bultos = cant_bultos;
            User_agr = user_agr;
            Fec_agr = fec_agr;
            User_mod = user_mod;
            Fec_mod = fec_mod;
            Activo = activo;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void Dispose()
        {
            // Recurso administrado liberado manualmente (si aplica en futuro)
            GC.SuppressFinalize(this);
        }
    }

}
