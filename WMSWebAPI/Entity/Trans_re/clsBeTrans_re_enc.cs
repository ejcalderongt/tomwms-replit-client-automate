using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeTrans_re_enc : ICloneable
    {
        [Column("IdRecepcionEnc")]
        [DisplayName("IdRecepcionEnc")]
        public int IdRecepcionEnc { get; set; } = 0;

        [Column("IdPropietarioBodega")]
        [DisplayName("IdPropietarioBodega")]
        public int IdPropietarioBodega { get; set; } = 0;

        [Column("IdMuelle")]
        [DisplayName("IdMuelle")]
        public int IdMuelle { get; set; } = 0;

        [Column("IdUbicacionRecepcion")]
        [DisplayName("IdUbicacionRecepcion")]
        public int IdUbicacionRecepcion { get; set; } = 0;

        [Column("IdTipoTransaccion")]
        [DisplayName("IdTipoTransaccion")]
        public string IdTipoTransaccion { get; set; } = "";

        [Column("fecha_recepcion")]
        [DisplayName("fecha_recepcion")]
        public DateTime Fecha_recepcion { get; set; } = DateTime.Now;

        [Column("hora_ini_pc")]
        [DisplayName("hora_ini_pc")]
        public DateTime Hora_ini_pc { get; set; } = DateTime.Now;

        [Column("hora_fin_pc")]
        [DisplayName("hora_fin_pc")]
        public DateTime Hora_fin_pc { get; set; } = DateTime.Now;

        [Column("muestra_precio")]
        [DisplayName("muestra_precio")]
        public bool Muestra_precio { get; set; } = false;

        [Column("estado")]
        [DisplayName("estado")]
        public string Estado { get; set; } = "";

        [Column("user_agr")]
        [DisplayName("user_agr")]
        public string User_agr { get; set; } = "";

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;

        [Column("user_mod")]
        [DisplayName("user_mod")]
        public string User_mod { get; set; } = "";

        [Column("fec_mod")]
        [DisplayName("fec_mod")]
        public DateTime Fec_mod { get; set; } = DateTime.Now;

        [Column("fecha_tarea")]
        [DisplayName("fecha_tarea")]
        public DateTime Fecha_tarea { get; set; } = DateTime.Now;

        [Column("tomar_fotos")]
        [DisplayName("tomar_fotos")]
        public bool Tomar_fotos { get; set; } = false;

        [Column("escanear_rec_ubic")]
        [DisplayName("escanear_rec_ubic")]
        public bool Escanear_rec_ubic { get; set; } = false;

        [Column("para_por_codigo")]
        [DisplayName("para_por_codigo")]
        public bool Para_por_codigo { get; set; } = false;

        [Column("observacion")]
        [DisplayName("observacion")]
        public string Observacion { get; set; } = "";

        [Column("firma_piloto")]
        [DisplayName("firma_piloto")]
        public byte[] Firma_piloto { get; set; } = Array.Empty<byte>();

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("NoGuia")]
        [DisplayName("NoGuia")]
        public string NoGuia { get; set; } = "";

        [Column("CorreoEnviado")]
        [DisplayName("CorreoEnviado")]
        public bool CorreoEnviado { get; set; } = false;

        [Column("Revision_Inconsistencia")]
        [DisplayName("Revision_Inconsistencia")]
        public bool Revision_Inconsistencia { get; set; } = false;

        [Column("bloqueada")]
        [DisplayName("bloqueada")]
        public bool Bloqueada { get; set; } = false;

        [Column("bloqueada_por")]
        [DisplayName("bloqueada_por")]
        public string Bloqueada_por { get; set; } = "";

        [Column("idusuariobloqueo")]
        [DisplayName("idusuariobloqueo")]
        public int Idusuariobloqueo { get; set; } = 0;

        [Column("idmotivoanulacionbodega")]
        [DisplayName("idmotivoanulacionbodega")]
        public int Idmotivoanulacionbodega { get; set; } = 0;

        [Column("Habilitar_Stock")]
        [DisplayName("Habilitar_Stock")]
        public bool Habilitar_Stock { get; set; } = false;

        [Column("idvehiculo")]
        [DisplayName("idvehiculo")]
        public int Idvehiculo { get; set; } = 0;

        [Column("idpiloto")]
        [DisplayName("idpiloto")]
        public int Idpiloto { get; set; } = 0;

        [Column("No_Marchamo")]
        [DisplayName("No_Marchamo")]
        public string No_Marchamo { get; set; } = "";

        [Column("mostrar_cantidad_esperada")]
        [DisplayName("mostrar_cantidad_esperada")]
        public bool Mostrar_cantidad_esperada { get; set; } = false;

        [Column("IdBodega")]
        [DisplayName("IdBodega")]
        public int IdBodega { get; set; } = 0;

        [Column("carta_cupo")]
        [DisplayName("carta_cupo")]
        public string Carta_cupo { get; set; } = "";

        [Column("IdEstado_defecto_recepcion")]
        [DisplayName("IdEstado_defecto_recepcion")]
        public int IdEstado_defecto_recepcion { get; set; } = 0;

        [Column("no_contenedor")]
        [DisplayName("no_contenedor")]
        public string No_contenedor { get; set; } = "";

        public clsBeTrans_re_enc() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}