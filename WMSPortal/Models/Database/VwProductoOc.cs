using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwProductoOc
    {
        public int IdProductoBodega { get; set; }
        public int IdProducto { get; set; }
        public int IdPropietario { get; set; }
        public int? IdClasificacion { get; set; }
        public int? IdFamilia { get; set; }
        public int? IdMarca { get; set; }
        public int? IdTipoProducto { get; set; }
        public int? IdUnidadMedidaBasica { get; set; }
        public int? IdBodega { get; set; }
        public string Propietario { get; set; }
        public string Clasificación { get; set; }
        public string Familia { get; set; }
        public string Marca { get; set; }
        public string TipoProducto { get; set; }
        public string UnidadMedida { get; set; }
        public string Codigo { get; set; }
        public string CodigoBarra { get; set; }
        public string Nombre { get; set; }
        public double? ExistenciaMin { get; set; }
        public double? ExistenciaMax { get; set; }
        public double? Costo { get; set; }
        public double? Precio { get; set; }
        public bool? Activo { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public bool? Activoproductobodega { get; set; }
        public int? IdCamara { get; set; }
        public int? IdTipoRotacion { get; set; }
        public int? IdPerfilSerializado { get; set; }
        public int? IdIndiceRotacion { get; set; }
        public int? IdSimbologia { get; set; }
        public int? IdArancel { get; set; }
        public double? PesoReferencia { get; set; }
        public double? PesoTolerancia { get; set; }
        public double? TemperaturaReferencia { get; set; }
        public double? TemperaturaTolerancia { get; set; }
        public bool? Serializado { get; set; }
        public bool? GeneraLote { get; set; }
        public bool? GeneraLpOld { get; set; }
        public bool? ControlVencimiento { get; set; }
        public bool? ControlLote { get; set; }
        public bool? PesoRecepcion { get; set; }
        public bool? PesoDespacho { get; set; }
        public bool? TemperaturaRecepcion { get; set; }
        public bool? TemperaturaDespacho { get; set; }
        public bool? MateriaPrima { get; set; }
        public bool? Kit { get; set; }
        public int? Tolerancia { get; set; }
        public int? CicloVida { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public string Noserie { get; set; }
        public string Noparte { get; set; }
        public bool? Fechamanufactura { get; set; }
        public bool? CapturaArancel { get; set; }
        public double? Largo { get; set; }
        public bool? EsHardware { get; set; }
        public double? Alto { get; set; }
        public double? Ancho { get; set; }
        public bool? ControlPeso { get; set; }
        public bool? CapturarAniada { get; set; }
    }
}
