Namespace Dtos

    ''' <summary>
    ''' DTO espejo de TOMWMS.clsBeBodega (Entity.dll del WMS).
    ''' Razón de existir: NO exponer la entidad VB.NET directamente al JSON, para
    '''   - controlar exactamente qué campos viajan en el wire (auditable, versionable)
    '''   - evitar acoplar el contrato HTTP a cualquier cambio interno de la entidad
    '''   - permitir que clientes no-VB.NET (web, móvil, integraciones) consuman el contrato
    ''' Las propiedades acá replican las de clsBeBodega + las heredadas de clsBeBodegaBase
    ''' que el flujo de frmBodega realmente necesita.
    ''' </summary>
    Public Class BodegaDto

        ' --- Heredadas de clsBeBodegaBase (claves + identificación) ---
        Public Property IdBodega As Integer
        Public Property IdPais As Integer
        Public Property IdEmpresa As Integer
        Public Property Codigo As String = ""
        Public Property Nombre As String = ""

        ' --- Propias de clsBeBodega ---
        Public Property Codigo_barra As String = ""
        Public Property Nombre_comercial As String = ""
        Public Property Direccion As String = ""
        Public Property Telefono As String = ""
        Public Property Email As String = ""
        Public Property Encargado As String = ""
        Public Property Ubic_recepcion As String = ""
        Public Property Ubic_picking As String = ""
        Public Property Ubic_despacho As String = ""
        Public Property Ubic_merma As String = ""
        Public Property User_agr As String = ""
        Public Property Fec_agr As Date = Date.Now
        Public Property User_mod As String = ""
        Public Property Fec_mod As Date = Date.Now
        Public Property Activo As Boolean = False
        Public Property Coordenada_x As String = ""
        Public Property Coordenada_y As String = ""
        Public Property Largo As Double = 0.0
        Public Property Ancho As Double = 0.0
        Public Property Alto As Double = 0.0
        Public Property Reservar_stocks_por_linea As Boolean = False
        Public Property Rechazar_pedido_por_stock As Boolean = False
        Public Property IdTipoTransaccion As String = ""
        Public Property Zoom As Double = 0.0
        Public Property IdMotivoUbicacionDanadoPicking As Integer = 0
        Public Property Cambio_ubicacion_auto As Boolean = False
        Public Property Codigo_bodega_erp As String = ""
        Public Property Cuenta_Ingreso_Mercancias As String = ""
        Public Property Cuenta_Egreso_Mercancias As String = ""
        Public Property Notificacion_Voz As Boolean = False
        Public Property Control_Tarifa_Servicios As Boolean = False
        Public Property Id_Motivo_Ubic_Reabasto As Integer = 0
        Public Property Es_Bodega_Fiscal As Boolean = False
        Public Property Habilitar_ingreso_consolidado As Boolean = False
        Public Property Captura_estiba_ingreso As Boolean = False
        Public Property Captura_pallet_no_estandar As Boolean = False
        Public Property Valor_porcentaje_iva As Double = 0
        Public Property Control_banderas_cliente As Boolean = False
        Public Property Permitir_Verificacion_Consolidada As Boolean = False
        Public Property IdTamanoEtiquetaUbicacionDefecto As Integer = 0
        Public Property Ubicar_Tarimas_Completas_Reabasto As Boolean = False
        Public Property IdTipoTransaccionSalida As Integer = 0
        Public Property Permitir_Eliminar_Documento_Salida As Boolean = False
        Public Property Eliminar_Documento_Salida As Boolean = False
        Public Property Operador_Picking_Realiza_Verificacion As Boolean = False
        Public Property Permitir_Cambio_Ubic_Producto_Picking As Boolean = False
        Public Property Permitir_Buen_Estado_En_Reemplazo As Boolean = False
        Public Property Cambio_ubicacion_restrictivo As Boolean = False
        Public Property Permitir_cambio_ubic_indice_menor As Boolean = False
        Public Property Requerir_mismo_producto_posiciones As Boolean = False

    End Class

End Namespace
