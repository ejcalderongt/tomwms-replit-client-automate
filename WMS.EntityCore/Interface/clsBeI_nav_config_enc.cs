using System;
using WMS.EntityCore;

public class clsBeI_nav_config_enc : ICloneable
{
    public int Idnavconfigenc { get; set; } = 0;
    public int Idempresa { get; set; } = 0;
    public int Idbodega { get; set; } = 0;
    public int IdPropietario { get; set; } = 0;
    public int IdUsuario { get; set; } = 0;
    public string Nombre { get; set; } = "";
    public DateTime Fec_agr { get; set; } = DateTime.Now;
    public string User_agr { get; set; } = "";
    public DateTime Fec_mod { get; set; } = DateTime.Now;
    public string User_mod { get; set; } = "";
    public int IdProductoEstado { get; set; } = 0;
    public tRechazarPedidoIncompleto Rechazar_pedido_incompleto { get; set; } = tRechazarPedidoIncompleto.No;
    public int Despachar_existencia_parcial { get; set; } = (int)tDespacharExistenciaParcial.No;
    public int Convertir_decimales_a_umbas { get; set; } = 0;
    public bool Generar_pedido_ingreso_bodega_destino { get; set; } = false;
    public bool Generar_Recepcion_Auto_Bodega_Destino { get; set; } = false;
    public string Codigo_proveedor_produccion { get; set; } = "";
    public int IdFamilia { get; set; } = 0;
    public int Idclasificacion { get; set; } = 0;
    public int IdMarca { get; set; } = 0;
    public int IdTipoProducto { get; set; } = 0;
    public bool Control_lote { get; set; } = false;
    public bool Control_vencimiento { get; set; } = false;
    public bool Genera_lp { get; set; } = false;
    public string Nombre_ejecutable { get; set; } = "";
    public clsDataContractDI.tTipoDocumentoIngreso IdTipoDocumentoTransferenciasIngreso { get; set; } = clsDataContractDI.tTipoDocumentoIngreso.Transferencia;
    public bool Crear_Recepcion_De_Transferencia_NAV { get; set; } = false;
    public bool Crear_Recepcion_De_Compra_NAV { get; set; } = false;
    public bool Control_peso { get; set; } = false;
    public int IdAcuerdoEnc { get; set; } = 0;
    public int IdTipoEtiqueta { get; set; } = 1;
    public bool Push_Ingreso_NAV_Desde_HH { get; set; } = false;
    public bool equiparar_cliente_con_propietario_en_doc_salida { get; set; } = false;
    public bool Ejecutar_En_Despacho_Automaticamente { get; set; } = true;
    public bool Reservar_UMBas_Primero { get; set; } = false;
    public bool Implosion_Automatica { get; set; } = false;
    public bool Explosion_Automatica { get; set; } = false;
    public bool Explosion_Automatica_Desde_Ubicacion_Picking { get; set; } = true;
    public int Explosion_Automatica_Nivel_Max { get; set; } = 1;
    public bool Interface_SAP { get; set; } = false;
    public bool Valida_Solo_Codigo { get; set; } = false;
    public string Bodega_Faltante { get; set; } = "";
    public int IdTipoRotacion { get; set; } = (int) clsDataContractDI.tTipoRotacion.FEFO;
    public bool Conservar_Zona_Picking_Clavaud { get; set; } = false;
    public bool Excluir_Ubicaciones_Reabasto { get; set; } = false;
    public bool considerar_paletizado_en_reabasto { get; set; } = false;
    public bool Considerar_Disponibilidad_Ubicacion_Reabasto { get; set; } = false;
    public int Dias_Vida_Defecto_Perecederos { get; set; } = 0;
    public string Codigo_Bodega_ERP_NC { get; set; } = "15";
    public string Lote_Defecto_Entrada_NC { get; set; } = "L9999";
    public DateTime Vence_Defecto_NC { get; set; } = new DateTime(1900, 1, 1);
    public int IdProductoEstado_NC { get; set; } = 0;
    public bool SAP_Control_Draft_Ajustes { get; set; } = false;
    public bool SAP_Control_Draft_Traslados { get; set; } = false;
    public int IdIndiceRotacion { get; set; } = 0;
    public int Rango_Dias_Importacion { get; set; } = 0;
    public bool Inferir_Bonificacion_Pedido_SAP { get; set; } = false;
    public bool Rechazar_Bonificacion_Incompleta { get; set; } = false;
    public bool Equiparar_Productos { get; set; } = false;
    public string Bodega_Facturacion { get; set; } = "";
    public bool Excluir_Recepcion_Picking { get; set; } = false;
    public string Bodega_Prorrateo { get; set; } = "";
    public string Bodega_Prorrateo1 { get; set; } = "";
    public string Codigo_Cliente_Virtual { get; set; } = "";
    public bool Recepcion_genera_historico { get; set; } = false;
    public string Lote_defecto_entrada_mercancia_sap { get; set; } = "";
    public DateTime Fecha_vence_defecto { get; set; } = new DateTime(1900, 1, 1);

    public enum tRechazarPedidoIncompleto
    {
        No = 0,
        Si = 1
    }
        
        public string Bodega_prorrateo1 { get; set; } = "";        
    public enum tDespacharExistenciaParcial
    {
        No = 0,
        Si = 1
    }

    public clsBeI_nav_config_enc() { }

    public clsBeI_nav_config_enc(ref int idnavconfigenc, int idempresa, int idbodega, int idPropietario, int idUsuario, string nombre, DateTime fec_agr, string user_agr, DateTime fec_mod, string user_mod)
    {
        this.Idnavconfigenc = idnavconfigenc;
        this.Idempresa = idempresa;
        this.Idbodega = idbodega;
        this.IdPropietario = idPropietario;
        this.IdUsuario = idUsuario;
        this.Nombre = nombre;
        this.Fec_agr = fec_agr;
        this.User_agr = user_agr;
        this.Fec_mod = fec_mod;
        this.User_mod = user_mod;
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}