using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavConfigEnc
    {
        public INavConfigEnc()
        {
            INavConfigDets = new HashSet<INavConfigDet>();
        }

        public int Idnavconfigenc { get; set; }
        public int Idempresa { get; set; }
        public int Idbodega { get; set; }
        public int? IdPropietario { get; set; }
        public int? IdUsuario { get; set; }
        public string Nombre { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecMod { get; set; }
        public string UserMod { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? RechazarPedidoIncompleto { get; set; }
        public int? DespacharExistenciaParcial { get; set; }
        public int? ConvertirDecimalesAUmbas { get; set; }
        public bool? GenerarPedidoIngresoBodegaDestino { get; set; }
        public bool? GenerarRecepcionAutoBodegaDestino { get; set; }
        public string CodigoProveedorProduccion { get; set; }
        public int? IdFamilia { get; set; }
        public int? Idclasificacion { get; set; }
        public int? IdMarca { get; set; }
        public int? IdTipoProducto { get; set; }
        public bool? ControlLote { get; set; }
        public bool? ControlVencimiento { get; set; }
        public bool? GeneraLp { get; set; }
        public string NombreEjecutable { get; set; }
        public int? IdTipoDocumentoTransferenciasIngreso { get; set; }
        public bool? CrearRecepcionDeTransferenciaNav { get; set; }
        public bool? ControlPeso { get; set; }
        public bool? CrearRecepcionDeCompraNav { get; set; }
        public int? IdAcuerdoEnc { get; set; }
        public int? IdTipoEtiqueta { get; set; }
        public bool EquipararClienteConPropietarioEnDocSalida { get; set; }

        public virtual Propietario IdPropietarioNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual Bodega IdbodegaNavigation { get; set; }
        public virtual Empresa IdempresaNavigation { get; set; }
        public virtual ICollection<INavConfigDet> INavConfigDets { get; set; }
    }
}
