using WMS.EntityCore.I_nav_Ped_Compra;
using WMS.EntityCore.Trans_oc;
using WMSWebAPI.Be;
using WMSWebAPI.Dtos.Catalogos;

namespace WMSWebAPI.Dtos.Ingresos
{
    public class OrdenCompraEncDto
    {
        public int IdOrdenCompraEnc { get; set; } = new int();
        public int IdPropietarioBodega { get; set; } = new int();
        public int IdProveedorBodega { get; set; } = new int();
        public int? IdTipoIngresoOC { get; set; }
        public int? IdEstadoOC { get; set; }
        public int? IdMotivoDevolucion { get; set; }
        public DateTime? Fecha_Creacion { get; set; } = new DateTime?();
        public DateTime? Hora_Creacion { get; set; } = new DateTime?();
        public string? No_Documento { get; set; }
        public string? User_Agr { get; set; }
        public DateTime? Fec_Agr { get; set; } = new DateTime?();
        public string? User_Mod { get; set; }
        public DateTime? Fec_Mod { get; set; } = new DateTime?();
        public string? Procedencia { get; set; }
        public string? No_Marchamo { get; set; }
        public string? Referencia { get; set; }
        public string? Observacion { get; set; }
        public bool? Control_Poliza { get; set; }
        public bool? Activo { get; set; }
        public DateTime? Fecha_Recepcion { get; set; } = new DateTime?();
        public DateTime? Hora_Inicio_Recepcion { get; set; } = new DateTime?();
        public DateTime? Hora_Fin_Recepcion { get; set; } = new DateTime?();
        public int? IdMuelleRecepcion { get; set; }
        public bool? Programar_Recepcion { get; set; }
        public int? IdMotivoAnulacionBodega { get; set; }
        public bool? Enviado_A_ERP { get; set; }
        public string? Serie { get; set; }
        public int? Correlativo { get; set; }
        public int? IdDespachoEnc { get; set; }
        public string? No_Ticket_Tms { get; set; }
        public int? IdNoDocumentoRef { get; set; }
        public int? IdAcuerdoComercial { get; set; }
        public int? IdOperadorBodegaDefecto { get; set; }
        public int? IdBodega { get; set; }
        public string? No_Documento_Recepcion_Erp { get; set; }
        public string? No_Documento_Devolucion { get; set; }
        public int? IdPedidoEncDevolucion { get; set; }
        public bool? Push_To_Nav { get; set; }
        public string? No_Documento_Ubicacion_Erp { get; set; }
        public bool? PutAway_Registrado { get; set; }
        public string? Codigo_Empresa_ERP { get; set; }
        public int IdCampaña { get; set; } = new int();
        public string Usr_Documento { get; set; }= string.Empty;
        public string Comentarios { get; set; } = string.Empty;
        public List<OrdenCompraDetDto> DetalleOC { get; set; } = new List<OrdenCompraDetDto>();
        public List<clsBeTrans_oc_det_lote> DetalleLotes { get; set; } = new List<clsBeTrans_oc_det_lote>();        
        public OrdenCompraPolizaDto ObjPoliza { get; set; } = new OrdenCompraPolizaDto();
        public List<clsBeTrans_oc_imagen> ListaImg { get; set; } = new List<clsBeTrans_oc_imagen>();
        public PropietarioBodegaDto PropietarioBodega { get; set; } = new PropietarioBodegaDto();
        public ProveedorBodegaDto ProveedorBodega { get; set; } = new ProveedorBodegaDto();
        public clsBeTrans_oc_estado EstadoOC { get; set; } = new clsBeTrans_oc_estado();
        public bool IsNew { get; set; } = new bool();
        public bool EsDevolucion { get; set; } = new bool();
        public TipoIngresoDto TipoIngreso { get; set; } = new TipoIngresoDto();
        public bool ExisteRecepcionNoFinalizada { get; set; } = new bool();
        public List<clsBeI_nav_barras_pallet> DetallePallets { get; set; } = new List<clsBeI_nav_barras_pallet>();
    }
}