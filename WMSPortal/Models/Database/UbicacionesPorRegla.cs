#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class UbicacionesPorRegla
    {
        public int IdReglaUbicacionEnc { get; set; }
        public int IdUbicacion { get; set; }
        public string Descripcion { get; set; }
        public double? Ancho { get; set; }
        public double? Largo { get; set; }
        public double? Alto { get; set; }
        public int? IdTramo { get; set; }
        public int? IndiceX { get; set; }
        public int? Nivel { get; set; }
        public int? IdIndiceRotacion { get; set; }
        public int? IdTipoRotacion { get; set; }
        public bool? Dañado { get; set; }
        public bool? Activo { get; set; }
        public bool? Bloqueada { get; set; }
        public bool? AceptaPallet { get; set; }
        public int IdBodega { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public bool? ReglaUbicDetPropActivo { get; set; }
        public int? IdPropietario { get; set; }
        public int? IdIndiceRotacionRegla { get; set; }
        public int? IdTipoRotacionRegla { get; set; }
        public int? IdTipoProducto { get; set; }
        public bool? ReglaUbicDetTpActivo { get; set; }
        public int? IdEstado { get; set; }
        public bool? ReglaUbicDetPeActivo { get; set; }
        public int? IdPresentacion { get; set; }
    }
}
