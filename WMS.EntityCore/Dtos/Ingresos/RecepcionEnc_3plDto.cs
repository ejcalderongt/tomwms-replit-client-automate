using WMSWebAPI.Be;
using WMSWebAPI.Dtos.Bodega;
using WMSWebAPI.Dtos.Catalogos;

namespace WMSWebAPI.Dtos.Ingresos
{
    public class RecepcionEnc_3plDto
    {
        public int IdRecepcionEnc { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public int? IdMuelle { get; set; }
        public int? IdUbicacionRecepcion { get; set; }
        public string IdTipoTransaccion { get; set; } = string.Empty;
        public DateTime? Fecha_Recepcion { get; set; }
        public DateTime? Hora_Ini_Pc { get; set; }
        public DateTime? Hora_Fin_Pc { get; set; }
        public bool? Muestra_Precio { get; set; }
        public string? Estado { get; set; }
        public string? User_Agr { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public string? User_Mod { get; set; }
        public DateTime? Fec_Mod { get; set; }
        public DateTime? Fecha_Tarea { get; set; }
        public bool? Tomar_Fotos { get; set; }
        public bool? Escanear_Rec_Ubic { get; set; }
        public bool? Para_Por_Codigo { get; set; }
        public string? Observacion { get; set; }
        public byte[]? Firma_Piloto { get; set; }
        public bool? Activo { get; set; }
        public string? NoGuia { get; set; }
        public bool? CorreoEnviado { get; set; }
        public bool? Revision_Inconsistencia { get; set; }
        public bool? Bloqueada { get; set; }
        public string? Bloqueada_Por { get; set; }
        public int? IdUsuarioBloqueo { get; set; }
        public int? IdMotivoAnulacionBodega { get; set; }
        public bool Habilitar_Stock { get; set; }
        public int? IdVehiculo { get; set; }
        public int? IdPiloto { get; set; }
        public string? No_Marchamo { get; set; }
        public bool? Mostrar_Cantidad_Esperada { get; set; }
        public int? IdBodega { get; set; }
        public string? Carta_Cupo { get; set; }
        public int? IdEstado_Defecto_Recepcion { get; set; }
        public string? No_Contenedor { get; set; }

       
    }

}