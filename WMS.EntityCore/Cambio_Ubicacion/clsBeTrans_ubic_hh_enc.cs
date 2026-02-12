
namespace WMS.EntityCore.Cambio_Ubicacion
{
    public class clsBeTrans_ubic_hh_enc : ICloneable
    {
        public int IdTareaUbicacionEnc { get; set; } = 0;
        public int IdPropietarioBodega { get; set; } = 0;
        public int IdMotivoUbicacion { get; set; } = 0;
        public DateTime FechaInicio { get; set; } = DateTime.Now;
        public DateTime HoraInicio { get; set; } = DateTime.Now;
        public DateTime FechaFin { get; set; } = new DateTime(1900, 1, 1);
        public DateTime HoraFin { get; set; } = DateTime.Now;
        public string User_agr { get; set; } = "";
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = "";
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public string Observacion { get; set; } = "";
        public bool Activo { get; set; } = false;
        public bool Operador_por_linea { get; set; } = false;
        public bool Ubicacion_con_hh { get; set; } = false;
        public string Estado { get; set; } = "";
        public bool Cambio_estado { get; set; } = false;
        public int IdReabastecimientoLog { get; set; } = 0;
        public bool Es_Traslado_SAP { get; set; } = false;
        public string No_Documento { get; set; } = "";
        public string Usuario { get; set; } = "";
        public string Rol { get; set; } = "";
        public int IdPrioridad { get; set; } = 0;
        public int IdTipoTarea { get; set; } = 0;
        public int IdBodega { get; set; } = 0;
        public string Asunto { get; set; } = "";
        public string DescripcionMotivo { get; set; } = "";
        public bool IsNew { get; set; }
        public string Nombre_Operador { get; set; } = "";
        public List<clsBeTrans_ubic_hh_det> Detalles { get; set; }= new List<clsBeTrans_ubic_hh_det>();
        public List<clsBeTrans_ubic_hh_stock> StockUbicHH { get; set; }= new List<clsBeTrans_ubic_hh_stock>();
        public string CodigoBodega { get; set; }= "";

        public clsBeTrans_ubic_hh_enc()
        {
        }

        public clsBeTrans_ubic_hh_enc(
            int IdTareaUbicacionEnc,
            int IdPropietarioBodega,
            int IdMotivoUbicacion,
            DateTime FechaInicio,
            DateTime HoraInicio,
            DateTime FechaFin,
            DateTime HoraFin,
            string user_agr,
            DateTime fec_agr,
            string user_mod,
            DateTime fec_mod,
            string Observacion,
            bool activo,
            bool operador_por_linea,
            bool ubicacion_con_hh,
            string estado,
            bool cambio_estado)
        {
            this.IdTareaUbicacionEnc = IdTareaUbicacionEnc;
            this.IdPropietarioBodega = IdPropietarioBodega;
            this.IdMotivoUbicacion = IdMotivoUbicacion;
            this.FechaInicio = FechaInicio;
            this.HoraInicio = HoraInicio;
            this.FechaFin = FechaFin;
            this.HoraFin = HoraFin;
            this.User_agr = user_agr;
            this.Fec_agr = fec_agr;
            this.User_mod = user_mod;
            this.Fec_mod = fec_mod;
            this.Observacion = Observacion;
            this.Activo = activo;
            this.Operador_por_linea = operador_por_linea;
            this.Ubicacion_con_hh = ubicacion_con_hh;
            this.Estado = estado;
            this.Cambio_estado = cambio_estado;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}