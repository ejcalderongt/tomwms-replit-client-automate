Public Class clsBeI_nav_config_enc
    Implements ICloneable
    Public Property Idnavconfigenc() As Integer = 0
    Public Property Idempresa() As Integer = 0
    Public Property Idbodega() As Integer = 0
    Public Property IdPropietario() As Integer = 0
    Public Property IdUsuario() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_agr() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property IdProductoEstado() As Integer = 0
    Public Property Rechazar_pedido_incompleto() As tRechazarPedidoIncompleto = tRechazarPedidoIncompleto.No
    Public Property Despachar_existencia_parcial() As Integer = tDespacharExistenciaParcial.No
    Public Property Convertir_decimales_a_umbas() As Integer = 0
    Public Property Generar_pedido_ingreso_bodega_destino() As Boolean = False
    Public Property Generar_Recepcion_Auto_Bodega_Destino() As Boolean = False
    Public Property Codigo_proveedor_produccion() As String = ""
    Public Property IdFamilia() As Integer = 0
    Public Property Idclasificacion() As Integer = 0
    Public Property IdMarca() As Integer = 0
    Public Property IdTipoProducto() As Integer = 0
    Public Property Control_lote() As Boolean = False
    Public Property Control_vencimiento() As Boolean = False
    Public Property Genera_lp() As Boolean = False
    Public Property Nombre_ejecutable() As String = ""
    Public Property IdTipoDocumentoTransferenciasIngreso As clsDataContractDI.tTipoDocumentoIngreso = clsDataContractDI.tTipoDocumentoIngreso.Transferencia
    Public Property Crear_Recepcion_De_Transferencia_NAV As Boolean = False
    Public Property Crear_Recepcion_De_Compra_NAV As Boolean = False
    Public Property Control_peso() As Boolean = False
    Public Property IdAcuerdoEnc() As Integer = 0
    '#EJC20210716_IdTipoEtiqueta: Cargar/Ins/Upd/frmConfiguracion
    Public Property IdTipoEtiqueta() As Integer = 1
    Public Property Push_Ingreso_NAV_Desde_HH As Boolean = False
    Public Property equiparar_cliente_con_propietario_en_doc_salida As Boolean = False

    '#EJC20220307: En el caso de Cealsa, se requiere configuración de interface para importación, pero en despacho no es necesario llamar por que no envía datos
    'al ERP.
    Public Property Ejecutar_En_Despacho_Automaticamente As Boolean = True

    ''' <summary>
    ''' #EJC20211216: Agregado para reservar de posiciones de picking primero en MERCOPAN.
    ''' </summary>
    ''' <returns></returns>
    Public Property Reservar_UMBas_Primero As Boolean = False

    ''' <summary>
    ''' #EJC20211216_1221: Un triste día de vacaciones para Carolina ocurrió que:
    ''' Determina si cuando el producto se solicita en presentación y solo hay umbas y se corresponde con el factor 
    ''' Es decir: Si la cantidad disponible en umbas dividida por el factor, devuelve unidades manipulables enteras (no fraccionarias)
    ''' Entonces al IdStock se le colocará el Idpresentación correspondiente con la unidad solicitada
    ''' </summary>
    ''' <returns></returns>
    Public Property Implosion_Automatica As Boolean = False

    ''' <summary>
    ''' #EJC20211216_1221: Un triste día de vacaciones para Carolina ocurrió que:
    ''' Determina si cuando el producto se solicita en umbas y solo hay disponible en presentación 
    ''' Si la cantidad disponible en presentación dividida por el factor, devuelve unidades manipulables enteras (no fraccionarias)
    ''' Entonces al IdStock se le quitará el Idpresentación para que quede en UMBAS.
    ''' </summary>
    ''' <returns></returns>
    Public Property Explosion_Automatica As Boolean = False

    ''' <summary>
    ''' #EJC20220523:Explosionar únicamente producto que está en zonas de picking
    ''' Requerimiento de Marcelo en Mercosal, día frío con lluvia, Lunes.
    ''' </summary>
    ''' <returns></returns>
    Public Property Explosion_Automatica_Desde_Ubicacion_Picking As Boolean = True

    ''' <summary>
    ''' #EJC20220523:Explosionar ónicamente desde los niveles 1 de rack producto (BYB, intuición de Erik).
    ''' </summary>
    ''' <returns></returns>
    Public Property Explosion_Automatica_Nivel_Max As Integer = 1

    Public Property Interface_SAP As Boolean = False

    Public Property Valida_Solo_Codigo As Boolean = False
    Public Property Bodega_Faltante As String = ""
    ''' <summary>
    ''' #EJC20190702: Condición de procesamiento de pedido en interface.
    ''' </summary>
    Public Enum tRechazarPedidoIncompleto
        ''' <summary>
        ''' Si alguna línea del pedido no puede ser abastecida por existencia se rechaza el pedido completo
        ''' </summary>
        No = 0
        ''' <summary>
        ''' Si alguna línea del pedido no puede ser abastecida por existencia se procesarón las demás lineas y se dará por válido el pedido
        ''' </summary>
        Si = 1
    End Enum

    ''' <summary>
    ''' ''' #EJC20190702: Condición de procesamiento de línea de pedido en interface.
    ''' </summary>
    Public Enum tDespacharExistenciaParcial
        ''' <summary>
        ''' Se rechazará la línea completa del pedido
        ''' </summary>
        No = 0
        ''' <summary>
        ''' Se despacha la cantidad existente para la línea del pedido
        ''' </summary>
        Si = 1
    End Enum

    ''' <summary>
    ''' #EJC20220330: Tipo de rotación de producto por defecto para interface.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdTipoRotacion As Integer = clsDataContractDI.tTipoRotacion.FEFO

    ''' <summary>
    ''' #EJC202211071651: 
    ''' Paso 1: en la lista de LPs de almacenamiento, ubicar los LPs que al 100% cumplen para ser despachados al cliente (sin stock reservado)
    ''' Paso 2: Una vez revisado el punto 1, revisar en el área de picking las cantidades que hagan falta para completar el pedido
    ''' Paso 3: De ser necesario, volver a revisar en el almacenamiento si se puede completar el pedido (sílo la cantidad requerida)
    ''' </summary>
    ''' <returns></returns>
    Public Property Conservar_Zona_Picking_Clavaud As Boolean = False

    ''' <summary>
    ''' #CKFK20230208: 
    ''' Al obtener el inventario disponible si la bandera está en true se excluirón las ubicaciones de reabasto
    ''' </summary>
    ''' <returns></returns>
    Public Property Excluir_Ubicaciones_Reabasto As Boolean = False

    ''' <summary>
    ''' #CKFK20230209: 
    ''' Permitir reabastecer una ubicación con una sola tarima completa
    ''' </summary>
    ''' <returns></returns>
    Public Property considerar_paletizado_en_reabasto As Boolean = False

    Public Property Considerar_Disponibilidad_Ubicacion_Reabasto As Boolean = False

    ''' <summary>
    ''' #EJC202303031607: Para interface BYB.
    ''' </summary>
    ''' <returns></returns>
    Public Property Dias_Vida_Defecto_Perecederos As Integer = 0

    '''<summary>
    '''#EJC20231017: Codigo_Bodega_ERP_NC - Indica el código (numérico) de la bodega en SAP.
    ''' </summary>
    '''<returns></returns>
    Public Property Codigo_Bodega_ERP_NC As String = "15"
    ''' <summary>
    ''' #EJC20231023: Lote_Defecto_Entrada_NC - Se utiliza para generar la entrada de mercancía por producto faltante en ingresos por importación o con backorder.
    ''' </summary>
    ''' <returns></returns>
    Public Property Lote_Defecto_Entrada_NC As String = "L9999"
    ''' <summary>
    ''' #EJC20231023: Se utiliza para generar la entrada de mercancía por producto faltante en ingresos por importación o con backorder.
    ''' </summary>
    ''' <returns></returns>
    Public Property Vence_Defecto_NC As Date = New Date(1900, 1, 1)

    ''' <summary>
    ''' #EJC20231024: Este valor define el estado con el que el producto ingresa a stock cuando se generan NC'S en SAP por producto faltante teórico....
    ''' </summary>
    ''' <returns></returns>
    Public Property IdProductoEstado_NC As Integer = 0

    ''' <summary>
    ''' #EJC202403271406: Determinar si los documentos de SAP, están sujetos al modelo de aprobación, en este caso hay que considera que al autorizar el documento en SAP
    ''' Ya se ha movido de bodega el producto, en consecuencia WMS, debe ejecutar o replicar el movimiento.
    ''' </summary>
    ''' <returns></returns>
    Public Property SAP_Control_Draft_Ajustes As Boolean = False

    Public Property SAP_Control_Draft_Traslados As Boolean = False
    Public Property IdIndiceRotacion As Integer = 0
    Public Property Rango_Dias_Importacion As Integer = 0

    ''' <summary>
    ''' #EJC202409041701: Si en un pedido de cliente, se repite el No. de línea con el mismo artículo, se infiere, que la segunda línea es la bonificación.
    ''' </summary>
    ''' <returns></returns>
    Public Property Inferir_Bonificacion_Pedido_SAP As Boolean = False
    ''' <summary>
    ''' #EJC202409041701: Determina si la bonificación incompleta se debe rechazar o no.
    ''' </summary>
    ''' <returns></returns>
    Public Property Rechazar_Bonificacion_Incompleta As Boolean = False

    Public Property Equiparar_Productos As Boolean = False

    Public Property Bodega_Facturacion As String = ""

    Public Property Excluir_Recepcion_Picking As Boolean = False
    Public Property Bodega_Prorrateo As String = ""
    Public Property Bodega_Prorrateo1 As String = ""

    Public Property Codigo_Cliente_Virtual As String = ""

    Sub New()
    End Sub

    Sub New(ByRef idnavconfigenc As Integer, ByVal idempresa As Integer, ByVal idbodega As Integer, ByVal idPropietario As Integer, ByVal idUsuario As Integer, ByVal nombre As String, ByVal fec_agr As Date, ByVal user_agr As String, ByVal fec_mod As Date, ByVal user_mod As String)
        Me.Idnavconfigenc = idnavconfigenc
        Me.Idempresa = idempresa
        Me.Idbodega = idbodega
        Me.IdPropietario = idPropietario
        Me.IdUsuario = idUsuario
        Me.Nombre = nombre
        Me.Fec_agr = fec_agr
        Me.User_agr = user_agr
        Me.Fec_mod = fec_mod
        Me.User_mod = user_mod
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class