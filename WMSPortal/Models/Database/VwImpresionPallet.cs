using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwImpresionPallet
    {
        public int? IdPallet { get; set; }
        public int? RecNo { get; set; }
        public string Empresa { get; set; }
        public string Bodega { get; set; }
        public string PropietarioNombre { get; set; }
        public int? IdProveedorBodega { get; set; }
        public string ProveedorNombre { get; set; }
        public string ProveedorTel { get; set; }
        public string ProveedorCodigo { get; set; }
        public string ProveedorDir { get; set; }
        public string ProductoCodigo { get; set; }
        public bool? Impreso { get; set; }
        public string ProductoNombreLargo { get; set; }
        public string ProductoUm { get; set; }
        public string ProductoPresentacion { get; set; }
        public double? ProductoCantidad { get; set; }
        public DateTime? ProductoVence { get; set; }
        public string ProductoLote { get; set; }
        public string ProductoEstado { get; set; }
        public string Lp { get; set; }
        public string Pc { get; set; }
        public string RefPc { get; set; }
        public DateTime? FechaPc { get; set; }
        public string Observacion { get; set; }
        public int? IdOperadorBodega { get; set; }
        public string UserAgr { get; set; }
        public string Imprimio { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdPropietario { get; set; }
    }
}
