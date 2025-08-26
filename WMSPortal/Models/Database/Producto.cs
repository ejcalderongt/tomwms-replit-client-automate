using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Producto
    {
        public Producto()
        {
            ProductoBodegas = new HashSet<ProductoBodega>();
            ProductoParametros = new HashSet<ProductoParametro>();
            ProductoPresentacions = new HashSet<ProductoPresentacion>();
            ProductoSustitutoIdProductoOriginalNavigations = new HashSet<ProductoSustituto>();
            ProductoSustitutoIdProductoReemplazoNavigations = new HashSet<ProductoSustituto>();
            TransTrasDets = new HashSet<TransTrasDet>();
        }

        public int IdProducto { get; set; }
        public int IdPropietario { get; set; }
        public int? IdClasificacion { get; set; }
        public int? IdFamilia { get; set; }
        public int? IdMarca { get; set; }
        public int? IdTipoProducto { get; set; }
        public int? IdUnidadMedidaBasica { get; set; }
        public int? IdCamara { get; set; }
        public int? IdTipoRotacion { get; set; }
        public int? IdPerfilSerializado { get; set; }
        public int? IdIndiceRotacion { get; set; }
        public int? IdSimbologia { get; set; }
        public int? IdArancel { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string CodigoBarra { get; set; }
        public double? Precio { get; set; }
        public double? ExistenciaMin { get; set; }
        public double? ExistenciaMax { get; set; }
        public double? Costo { get; set; }
        public double? PesoReferencia { get; set; }
        public double? PesoTolerancia { get; set; }
        public double? TemperaturaReferencia { get; set; }
        public double? TemperaturaTolerancia { get; set; }
        public bool? Activo { get; set; }
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
        public byte[] Imagen { get; set; }
        public string Noserie { get; set; }
        public string Noparte { get; set; }
        public bool? Fechamanufactura { get; set; }
        public bool? CapturarAniada { get; set; }
        public bool? ControlPeso { get; set; }
        public bool? CapturaArancel { get; set; }
        public bool? EsHardware { get; set; }
        public double? Largo { get; set; }
        public double? Alto { get; set; }
        public double? Ancho { get; set; }
        public int? IdUnidadMedidaCobro { get; set; }
        public int? IdTipoEtiqueta { get; set; }

        public virtual Arancel IdArancelNavigation { get; set; }
        public virtual Camara IdCamaraNavigation { get; set; }
        public virtual ProductoClasificacion IdClasificacionNavigation { get; set; }
        public virtual ProductoFamilium IdFamiliaNavigation { get; set; }
        public virtual IndiceRotacion IdIndiceRotacionNavigation { get; set; }
        public virtual ProductoMarca IdMarcaNavigation { get; set; }
        public virtual PerfilSerializado IdPerfilSerializadoNavigation { get; set; }
        public virtual Propietario IdPropietarioNavigation { get; set; }
        public virtual SimbologiasCodigoBarra IdSimbologiaNavigation { get; set; }
        public virtual ProductoTipo IdTipoProductoNavigation { get; set; }
        public virtual TipoRotacion IdTipoRotacionNavigation { get; set; }
        public virtual UnidadMedidum IdUnidadMedidaBasicaNavigation { get; set; }
        public virtual ICollection<ProductoBodega> ProductoBodegas { get; set; }
        public virtual ICollection<ProductoParametro> ProductoParametros { get; set; }
        public virtual ICollection<ProductoPresentacion> ProductoPresentacions { get; set; }
        public virtual ICollection<ProductoSustituto> ProductoSustitutoIdProductoOriginalNavigations { get; set; }
        public virtual ICollection<ProductoSustituto> ProductoSustitutoIdProductoReemplazoNavigations { get; set; }
        public virtual ICollection<TransTrasDet> TransTrasDets { get; set; }
    }
}
