using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.EntityCore.I_nav_Ped_Compra;
using WMS.EntityCore.Propietario;
using WMS.EntityCore.Proveedor;
using WMSWebAPI.Be;

namespace WMS.EntityCore.Trans_oc
{
    public class clsBeTrans_oc_enc : ICloneable
    {
        [Column("IdOrdenCompraEnc")]
        [DisplayName("IdOrdenCompraEnc")]
        public int IdOrdenCompraEnc { get; set; } = 0;

        [Column("IdPropietarioBodega")]
        [DisplayName("IdPropietarioBodega")]
        public int IdPropietarioBodega { get; set; } = 0;

        [Column("IdProveedorBodega")]
        [DisplayName("IdProveedorBodega")]
        public int IdProveedorBodega { get; set; } = 0;

        [Column("IdTipoIngresoOC")]
        [DisplayName("IdTipoIngresoOC")]
        public int IdTipoIngresoOC { get; set; } = 0;

        [Column("IdEstadoOC")]
        [DisplayName("IdEstadoOC")]
        public int IdEstadoOC { get; set; } = 0;

        [Column("IdMotivoDevolucion")]
        [DisplayName("IdMotivoDevolucion")]
        public int IdMotivoDevolucion { get; set; } = 0;

        [Column("Fecha_Creacion")]
        [DisplayName("Fecha_Creacion")]
        public DateTime Fecha_Creacion { get; set; } = DateTime.Now;

        [Column("Hora_Creacion")]
        [DisplayName("Hora_Creacion")]
        public DateTime Hora_Creacion { get; set; } = DateTime.Now;

        [Column("No_Documento")]
        [DisplayName("No_Documento")]
        public string No_Documento { get; set; } = "";

        [Column("User_Agr")]
        [DisplayName("User_Agr")]
        public string User_Agr { get; set; } = "";

        [Column("Fec_Agr")]
        [DisplayName("Fec_Agr")]
        public DateTime Fec_Agr { get; set; } = DateTime.Now;

        [Column("User_Mod")]
        [DisplayName("User_Mod")]
        public string User_Mod { get; set; } = "";

        [Column("Fec_Mod")]
        [DisplayName("Fec_Mod")]
        public DateTime Fec_Mod { get; set; } = DateTime.Now;

        [Column("Procedencia")]
        [DisplayName("Procedencia")]
        public string Procedencia { get; set; } = "";

        [Column("No_Marchamo")]
        [DisplayName("No_Marchamo")]
        public string No_Marchamo { get; set; } = "";

        [Column("Referencia")]
        [DisplayName("Referencia")]
        public string Referencia { get; set; } = "";

        [Column("Observacion")]
        [DisplayName("Observacion")]
        public string Observacion { get; set; } = "";

        [Column("Control_Poliza")]
        [DisplayName("Control_Poliza")]
        public bool Control_Poliza { get; set; } = false;

        [Column("Activo")]
        [DisplayName("Activo")]
        public bool Activo { get; set; } = false;

        [Column("Fecha_Recepcion")]
        [DisplayName("Fecha_Recepcion")]
        public DateTime Fecha_Recepcion { get; set; } = DateTime.Now;

        [Column("Hora_Inicio_Recepcion")]
        [DisplayName("Hora_Inicio_Recepcion")]
        public DateTime Hora_Inicio_Recepcion { get; set; } = DateTime.Now;

        [Column("Hora_Fin_Recepcion")]
        [DisplayName("Hora_Fin_Recepcion")]
        public DateTime Hora_Fin_Recepcion { get; set; } = DateTime.Now;

        [Column("IdMuelleRecepcion")]
        [DisplayName("IdMuelleRecepcion")]
        public int IdMuelleRecepcion { get; set; } = 0;

        [Column("Programar_Recepcion")]
        [DisplayName("Programar_Recepcion")]
        public bool Programar_Recepcion { get; set; } = false;

        [Column("IdMotivoAnulacionBodega")]
        [DisplayName("IdMotivoAnulacionBodega")]
        public int IdMotivoAnulacionBodega { get; set; } = 0;

        [Column("Enviado_A_ERP")]
        [DisplayName("Enviado_A_ERP")]
        public bool Enviado_A_ERP { get; set; } = false;

        [Column("serie")]
        [DisplayName("serie")]
        public string Serie { get; set; } = "";

        [Column("correlativo")]
        [DisplayName("correlativo")]
        public int Correlativo { get; set; } = 0;

        [Column("IdDespachoEnc")]
        [DisplayName("IdDespachoEnc")]
        public int IdDespachoEnc { get; set; } = 0;

        [Column("no_ticket_tms")]
        [DisplayName("no_ticket_tms")]
        public string No_ticket_tms { get; set; } = "";

        [Column("IdNoDocumentoRef")]
        [DisplayName("IdNoDocumentoRef")]
        public int IdNoDocumentoRef { get; set; } = 0;

        [Column("idacuerdocomercial")]
        [DisplayName("idacuerdocomercial")]
        public int Idacuerdocomercial { get; set; } = 0;

        [Column("IdOperadorBodegaDefecto")]
        [DisplayName("IdOperadorBodegaDefecto")]
        public int IdOperadorBodegaDefecto { get; set; } = 0;

        [Column("IdBodega")]
        [DisplayName("IdBodega")]
        public int IdBodega { get; set; } = 0;

        [Column("no_documento_recepcion_erp")]
        [DisplayName("no_documento_recepcion_erp")]
        public string No_documento_recepcion_erp { get; set; } = "";

        [Column("no_documento_devolucion")]
        [DisplayName("no_documento_devolucion")]
        public string No_documento_devolucion { get; set; } = "";

        [Column("IdPedidoEncDevolucion")]
        [DisplayName("IdPedidoEncDevolucion")]
        public int IdPedidoEncDevolucion { get; set; } = 0;

        [Column("push_to_nav")]
        [DisplayName("push_to_nav")]
        public bool Push_to_nav { get; set; } = false;

        [Column("no_documento_ubicacion_erp")]
        [DisplayName("no_documento_ubicacion_erp")]
        public string No_documento_ubicacion_erp { get; set; } = "";

        [Column("PutAway_Registrado")]
        [DisplayName("PutAway_Registrado")]
        public bool PutAway_Registrado { get; set; } = false;

        [Column("Codigo_Empresa_ERP")]
        [DisplayName("Codigo_Empresa_ERP")]
        public string Codigo_Empresa_ERP { get; set; } = "";

        public int IdCampaña { get; set; } = 0;
        public string Usr_Documento { get; set; } = "";
        public string Comentarios { get; set; } = "";
        public List<clsBeTrans_oc_det> DetalleOC { get; set; } = new List<clsBeTrans_oc_det>();
        public List<clsBeTrans_oc_det_lote> DetalleLotes { get; set; } = new List<clsBeTrans_oc_det_lote>();
        public List<clsBeI_nav_barras_pallet> DetallePallets { get; set; } = new List<clsBeI_nav_barras_pallet>();
        public clsBeTrans_oc_pol? ObjPoliza { get; set; } = new clsBeTrans_oc_pol();
        public List<clsBeTrans_oc_imagen> ListaImg { get; set; } = new List<clsBeTrans_oc_imagen>();
        public clsBePropietario_bodega PropietarioBodega { get; set; } = new clsBePropietario_bodega();
        public clsBeProveedor_bodega ProveedorBodega { get; set; } = new clsBeProveedor_bodega();
        public clsBeTrans_oc_estado? EstadoOC { get; set; } = new clsBeTrans_oc_estado();
        public bool IsNew { get; set; }
        public bool EsDevolucion { get; set; }
        public clsBeTrans_oc_ti? TipoIngreso { get; set; } = new clsBeTrans_oc_ti();
        public bool ExisteRecepcionNoFinalizada { get; set; }
        public string No_Documento_Recepcion_ERP { get; set; } = "";

        public clsBeTrans_oc_enc() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}