Public Class clsBeI_nav_transacciones_out
    Implements ICloneable

    Public Property Idtransaccion() As Integer = 0
    Public Property Idempresa() As Integer = 0
    Public Property Idbodega() As Integer = 0
    Public Property Idpropietario() As Integer = 0
    Public Property Idpropietariobodega() As Integer = 0
    Public Property Idordencompra() As Integer = 0
    Public Property Idrecepcionenc() As Integer = 0
    Public Property Idpedidoenc() As Integer = 0
    Public Property Iddespachoenc() As Integer = 0
    Public Property Idproductobodega() As Integer = 0
    Public Property Idproducto() As Integer = 0
    Public Property Idunidadmedida() As Integer = 0
    Public Property Idpresentacion() As Integer = 0
    Public Property Idproductoestado() As Integer = 0
    Public Property Cantidad() As Double = 0.0
    Public Property Cantidad_Esperada As Double = 0
    Public Property Peso() As Double = 0.0
    Public Property Lote() As String = ""
    Public Property Fecha_vence() As Date = "01/01/1900"
    Public Property Fecha_recepcion() As Date = "01/01/1900"
    Public Property No_pedido() As String = ""
    Public Property No_linea() As String = ""
    Public Property Codigo_producto() As String = ""
    Public Property Nombre_producto() As String = ""
    Public Property Codigo_variante() As String = ""
    Public Property Unidad_medida() As String = ""
    Public Property Tipo_transaccion() As String = ""
    Public Property Enviado() As Boolean = False
    ''' <summary>
    ''' #EJC20240917: Determinar que transacciones se enviaron o no correctamente a SAP (por lote)
    ''' </summary>
    ''' <returns></returns>
    Public Property Auditar() As Boolean = False
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_agr() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Lic_Plate As String = ""
    Public Property Uds_Lic_Plate As Double = 0.0
    Public Property Cantidad_Presentacion As Double = 0.0
    ''' <summary>
    ''' Indica el tipo de documento de salida o ingreso efectuado. Pej. Transferencia, Despacho, Devolución.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdTipoDocumento As String = ""
    Public Property Observacion As String = ""
    Public Property Empresa_Transporte As String = ""
    Public Property Piloto_Transporte As String = ""
    Public Property Contacto_Recibe As String = ""
    Public Property Contacto_Entrega As String = ""
    ''' <summary>
    ''' Placa de transporte.
    ''' </summary>
    ''' <returns></returns>
    Public Property Placa_Transporte As String = ""
    ''' <summary>
    ''' Tank Container Number.
    ''' </summary>
    ''' <returns></returns>
    Public Property TCN_Transporte As String = ""
    ''' <summary>
    ''' número de marchamo.
    ''' </summary>
    ''' <returns></returns>
    Public Property Marchamo_No As String = ""
    Public Property Codigo_Bodega_Origen As String = ""
    Public Property Codigo_Bodega_Destino As String = ""
    Public Property Codigo_Cliente As String = ""


    'Campos para reportes fiscales

    Public Property codigo_barra As String = ""
    Public Property valor_aduana As Double = 0.0
    Public Property valor_fob As Double = 0.0
    Public Property valor_iva As Double = 0.0
    Public Property valor_dai As Double = 0.0
    Public Property valor_seguro As Double = 0.0

    Public Property valor_flete As Double = 0.0
    Public Property peso_neto As Double = 0.0
    Public Property peso_bruto As Double = 0.0

    Public Property fecha_despacho As Date = "01/01/1900"

    ''' <summary>
    ''' '#EJC20210617:Devolución Idealsa con referencia.
    ''' </summary>
    ''' <returns></returns>
    Public Property no_documento_salida_ref_devol As String = ""
    Public Property IdPedidoEncDevol As Integer = 0
    Public Property IdDespachoDet As Integer = 0
    Public Property IdRecepcionDet As Integer = 0
    ''' <summary>
    ''' #EJC20240913:Cantidad que se envió a ERP.
    ''' </summary>
    ''' <returns></returns>
    Public Property Cantidad_Enviada As Double = 0
    ''' <summary>
    ''' #EJC20240913:Cantidad pendiente de envío o ajuste en ERP.
    ''' </summary>
    ''' <returns></returns>
    Public Property Cantidad_Pendiente As Double = 0

    Sub New()
    End Sub

    Sub New(ByRef idtransaccion As Integer, ByVal idempresa As Integer, ByVal idbodega As Integer, ByVal idpropietario As Integer,
            ByVal idpropietariobodega As Integer, ByVal idordencompra As Integer, ByVal idrecepcionenc As Integer,
            ByVal idpedidoenc As Integer, ByVal iddespachoenc As Integer, ByVal idproductobodega As Integer, ByVal idproducto As Integer,
            ByVal idunidadmedida As Integer, ByVal idpresentacion As Integer, ByVal idproductoestado As Integer, ByVal cantidad As Double,
            ByVal peso As Double, ByVal lote As String, ByVal fecha_vence As String, ByVal fecha_recepcion As String, ByVal no_pedido As String,
            ByVal no_linea As String, ByVal codigo_producto As String, ByVal nombre_producto As String, ByVal codigo_variante As String,
            ByVal unidad_medida As String, ByVal tipo_transaccion As String, ByVal enviado As Boolean, ByVal fec_agr As Date,
            ByVal user_agr As String, ByVal fec_mod As Date, ByVal user_mod As String, ByVal lic_plate As String, ByVal uds_lic_plate As Double,
            ByVal cantidad_presentacion As Double,
            ByVal codigo_barra As String, ByVal valor_aduana As Double, ByVal valor_fob As Double, ByVal valor_iva As Double, ByVal valor_dai As Double,
            ByVal valor_seguro As Double, ByVal valor_flete As Double, ByVal peso_neto As Double, ByVal peso_bruto As Double, ByVal fecha_despacho As Date)

        Me.Idtransaccion = idtransaccion
        Me.Idempresa = idempresa
        Me.Idbodega = idbodega
        Me.Idpropietario = idpropietario
        Me.Idpropietariobodega = idpropietariobodega
        Me.Idordencompra = idordencompra
        Me.Idrecepcionenc = idrecepcionenc
        Me.Idpedidoenc = idpedidoenc
        Me.Iddespachoenc = iddespachoenc
        Me.Idproductobodega = idproductobodega
        Me.Idproducto = idproducto
        Me.Idunidadmedida = idunidadmedida
        Me.Idpresentacion = idpresentacion
        Me.Idproductoestado = idproductoestado
        Me.Cantidad = cantidad
        Me.Peso = peso
        Me.Lote = lote
        Me.Fecha_vence = fecha_vence
        Me.Fecha_recepcion = fecha_recepcion
        Me.No_pedido = no_pedido
        Me.No_linea = no_linea
        Me.Codigo_producto = codigo_producto
        Me.Nombre_producto = nombre_producto
        Me.Codigo_variante = codigo_variante
        Me.Unidad_medida = unidad_medida
        Me.Tipo_transaccion = tipo_transaccion
        Me.Enviado = enviado
        Me.Fec_agr = fec_agr
        Me.User_agr = user_agr
        Me.Fec_mod = fec_mod
        Me.User_mod = user_mod
        Me.Lic_Plate = lic_plate
        Me.Uds_Lic_Plate = uds_lic_plate
        Me.Cantidad_Presentacion = cantidad_presentacion

        Me.codigo_barra = codigo_barra
        Me.valor_aduana = valor_dai
        Me.valor_fob = valor_fob
        Me.valor_iva = valor_iva
        Me.valor_dai = valor_dai
        Me.valor_seguro = valor_seguro
        Me.valor_flete = valor_flete
        Me.peso_neto = peso_neto
        Me.peso_bruto = peso_bruto
        Me.fecha_despacho = fecha_despacho

    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
