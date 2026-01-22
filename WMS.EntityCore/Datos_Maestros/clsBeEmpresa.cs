using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeEmpresa : ICloneable
    {
        [Column("IdEmpresa")]
        [DisplayName("IdEmpresa")]
        public int IdEmpresa { get; set; } = 0;

        [Column("nombre")]
        [DisplayName("nombre")]
        public string Nombre { get; set; } = "";

        [Column("direccion")]
        [DisplayName("direccion")]
        public string Direccion { get; set; } = "";

        [Column("telefono")]
        [DisplayName("telefono")]
        public string Telefono { get; set; } = "";

        [Column("email")]
        [DisplayName("email")]
        public string Email { get; set; } = "";

        [Column("razon_social")]
        [DisplayName("razon_social")]
        public string Razon_social { get; set; } = "";

        [Column("representante")]
        [DisplayName("representante")]
        public string Representante { get; set; } = "";

        [Column("corr_cod_barra")]
        [DisplayName("corr_cod_barra")]
        public int Corr_cod_barra { get; set; } = 0;

        [Column("path_printer")]
        [DisplayName("path_printer")]
        public string Path_printer { get; set; } = "";

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

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

        [Column("clienteRapido")]
        [DisplayName("clienteRapido")]
        public bool ClienteRapido { get; set; } = false;

        [Column("imagen")]
        [DisplayName("imagen")]
        public byte[] Imagen { get; set; } = Array.Empty<byte>();

        [Column("operador_logistico")]
        [DisplayName("operador_logistico")]
        public bool Operador_logistico { get; set; } = false;

        [Column("puerto_escaner")]
        [DisplayName("puerto_escaner")]
        public int Puerto_escaner { get; set; } = 0;

        [Column("control_presentaciones")]
        [DisplayName("control_presentaciones")]
        public bool Control_presentaciones { get; set; } = false;

        [Column("anulaciones_por_supervisor")]
        [DisplayName("anulaciones_por_supervisor")]
        public bool Anulaciones_por_supervisor { get; set; } = false;

        [Column("codigo")]
        [DisplayName("codigo")]
        public string Codigo { get; set; } = "";

        [Column("clave")]
        [DisplayName("clave")]
        public string Clave { get; set; } = "";

        [Column("intento")]
        [DisplayName("intento")]
        public int Intento { get; set; } = 0;

        [Column("duracionclave")]
        [DisplayName("duracionclave")]
        public int Duracionclave { get; set; } = 0;

        [Column("duracionclavetemporal")]
        [DisplayName("duracionclavetemporal")]
        public int Duracionclavetemporal { get; set; } = 0;

        [Column("codigo_automatico")]
        [DisplayName("codigo_automatico")]
        public bool Codigo_automatico { get; set; } = false;

        [Column("politica_contraseñas")]
        [DisplayName("politica_contraseñas")]
        public bool Politica_contraseñas { get; set; } = false;

        [Column("IdMotivoAjusteInventario")]
        [DisplayName("IdMotivoAjusteInventario")]
        public int IdMotivoAjusteInventario { get; set; } = 0;

        [Column("cantidad_decimales_despliegue")]
        [DisplayName("cantidad_decimales_despliegue")]
        public int Cantidad_decimales_despliegue { get; set; } = 0;

        [Column("cantidad_decimales_calculo")]
        [DisplayName("cantidad_decimales_calculo")]
        public int Cantidad_decimales_calculo { get; set; } = 0;

        [Column("minutos_timer_jornada_sistema")]
        [DisplayName("minutos_timer_jornada_sistema")]
        public double Minutos_timer_jornada_sistema { get; set; } = 0;

        [Column("hora_corte_jornada_sistema")]
        [DisplayName("hora_corte_jornada_sistema")]
        public DateTime Hora_corte_jornada_sistema { get; set; } = DateTime.Now;

        [Column("generar_stock_jornada")]
        [DisplayName("generar_stock_jornada")]
        public bool Generar_stock_jornada { get; set; } = false;

        [Column("buscar_actualizacion_hh")]
        [DisplayName("buscar_actualizacion_hh")]
        public bool Buscar_actualizacion_hh { get; set; } = false;

        [Column("version_bd")]
        [DisplayName("version_bd")]
        public string Version_bd { get; set; } = "";

        [Column("aws_token")]
        [DisplayName("aws_token")]
        public string Aws_token { get; set; } = "";

        public clsBeEmpresa() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}