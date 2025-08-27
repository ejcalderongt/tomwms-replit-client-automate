using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwTransUbicHhDet
    {
        public int IdTareaUbicacionEnc { get; set; }
        public int IdTareaUbicacionDet { get; set; }
        public int? IdStock { get; set; }
        public string Nombre { get; set; }
        public int? IdUbicacionDestino { get; set; }
        public string Descripcion { get; set; }
        public int? IdEstadoOrigen { get; set; }
        public int? IdEstadoDestino { get; set; }
        public int? IdOperadorBodega { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFin { get; set; }
        public bool? Realizado { get; set; }
        public double? Cantidad { get; set; }
        public bool? Activo { get; set; }
        public string Nombres { get; set; }
        public string Codigo { get; set; }
        public bool? Serializado { get; set; }
        public int? IdEstado { get; set; }
        public string NomEstado { get; set; }
        public int? Añada { get; set; }
        public string Lote { get; set; }
        public string FechaIngreso { get; set; }
        public string Presentacion { get; set; }
        public string UnidadMedida { get; set; }
        public DateTime? FechaVence { get; set; }
        public string Serial { get; set; }
        public int? IdPropietario { get; set; }
        public string NombreComercial { get; set; }
        public double? Recibido { get; set; }
        public int? IdProducto { get; set; }
        public int? IdProductoBodega { get; set; }
        public int? IdUnidadMedidaBasica { get; set; }
        public string Estado { get; set; }
        public int? IdUbicacionOrigen { get; set; }
        public int? IdPresentacion { get; set; }
        public int? Nivel { get; set; }
        public int? IndiceX { get; set; }
        public string Tramo { get; set; }
        public int IdBodega { get; set; }
    }
}
