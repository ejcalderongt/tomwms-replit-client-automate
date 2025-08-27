using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwPropietario
    {
        public string Empresa { get; set; }
        public int IdPropietario { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdTipoActualizacionCosto { get; set; }
        public string Contacto { get; set; }
        public string NombreComercial { get; set; }
        public byte[] Imagen { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public string Email { get; set; }
        public bool? ActualizaCostoOc { get; set; }
        public int? Color { get; set; }
        public string Codigo { get; set; }
        public bool? Sistema { get; set; }
        public string Nit { get; set; }
    }
}
