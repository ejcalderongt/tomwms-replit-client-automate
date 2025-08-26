using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwProductoRellenado
    {
        public int? IdBodega { get; set; }
        public int? IdProductoBodega { get; set; }
        public int? IdProducto { get; set; }
        public string Ubicación { get; set; }
        public string Estado { get; set; }
        public int IdRellenado { get; set; }
        public int? IdPresentacion { get; set; }
        public string Presentación { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdUbicacion { get; set; }
        public int? IdTipoAccion { get; set; }
        public double? Minimo { get; set; }
        public double? Maximo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? Activo { get; set; }
        public int? IdUmBasAbastercerCon { get; set; }
        public int? IdPresentacionAbastercerCon { get; set; }
        public int? IdUnidadMedidaBasica { get; set; }
        public string NomPresentacionRellenarCon { get; set; }
        public string NomUmBasAbastecerCon { get; set; }
        public int? IdPropietario { get; set; }
    }
}
