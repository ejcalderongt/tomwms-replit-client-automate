using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwProveedorBodega
    {
        public int? IdBodega { get; set; }
        public int? IdAsignacion { get; set; }
        public string Empresa { get; set; }
        public string Propietario { get; set; }
        public int IdEmpresa { get; set; }
        public int IdPropietario { get; set; }
        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Nit { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Contacto { get; set; }
        public bool? Activo { get; set; }
        public bool? MuestraPrecio { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? ActualizaCostoOc { get; set; }
        public bool? ActivoProveedorBodega { get; set; }
        public string Código { get; set; }
        public int? Idubicacionvirtual { get; set; }
        public bool? EsBodegaRecepcion { get; set; }
        public bool? EsBodegaTraslado { get; set; }
    }
}
