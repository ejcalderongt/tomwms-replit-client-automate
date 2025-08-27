using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwDocConDiferencia
    {
        public string Ordencompra { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public double? Cantidad { get; set; }
        public double? CantidadRecibida { get; set; }
        public string Presentacion { get; set; }
        public double? Diferencia { get; set; }
        public int IdPropietarioBodega { get; set; }
        public string Bodega { get; set; }
        public string Propietario { get; set; }
        public string Poliza { get; set; }
        public int IdProveedorBodega { get; set; }
        public int? IdTipoIngresoOc { get; set; }
        public string NombreIngresooc { get; set; }
        public int IdProductoBodega { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedidaBasica { get; set; }
        public string Umbas { get; set; }
        public string Estado { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int IdPropietario { get; set; }
        public int IdBodega { get; set; }
        public string NoDocumento { get; set; }
    }
}
