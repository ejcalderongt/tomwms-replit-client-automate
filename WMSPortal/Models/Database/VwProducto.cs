using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwProducto
    {
        public int IdProducto { get; set; }
        public int IdPropietario { get; set; }
        public int? IdClasificacion { get; set; }
        public int? IdFamilia { get; set; }
        public int? IdMarca { get; set; }
        public int? IdTipoProducto { get; set; }
        public int? IdUnidadMedidaBasica { get; set; }
        public string Propietario { get; set; }
        public string Clasificación { get; set; }
        public string Familia { get; set; }
        public string Marca { get; set; }
        public string TipoProducto { get; set; }
        public string UnidadMedida { get; set; }
        public string Código { get; set; }
        public string CódigoDeBarra { get; set; }
        public string Producto { get; set; }
        public double? ExistenciaMínima { get; set; }
        public double? ExistenciaMáxima { get; set; }
        public double? Costo { get; set; }
        public double? Precio { get; set; }
        public bool? Activo { get; set; }
        public int? IdCamara { get; set; }
        public int? IdTipoRotacion { get; set; }
        public int? IdPerfilSerializado { get; set; }
        public int? IdIndiceRotacion { get; set; }
        public int? IdSimbologia { get; set; }
        public int? IdArancel { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string CodigoBarra { get; set; }
        public double? Expr8 { get; set; }
        public double? ExistenciaMin { get; set; }
        public double? ExistenciaMax { get; set; }
        public double? Expr9 { get; set; }
        public double? PesoReferencia { get; set; }
        public double? PesoTolerancia { get; set; }
        public double? TemperaturaReferencia { get; set; }
        public double? TemperaturaTolerancia { get; set; }
        public bool? Expr10 { get; set; }
        public bool? Serializado { get; set; }
        public bool? GeneraLote { get; set; }
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
        public byte[] Imagen { get; set; }
        public string Noserie { get; set; }
        public string Noparte { get; set; }
        public bool? Fechamanufactura { get; set; }
        public bool? CapturarAniada { get; set; }
        public bool? ControlPeso { get; set; }
        public bool? CapturaArancel { get; set; }
        public bool? EsHardware { get; set; }
    }
}
