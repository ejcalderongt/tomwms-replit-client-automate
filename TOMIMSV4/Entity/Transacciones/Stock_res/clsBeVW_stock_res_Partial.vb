Partial Public Class clsBeVW_stock_res
    Implements IDisposable

    Public Property IdPedido As Integer = 0
    Public Property IdPedidoDet As Integer = 0
    Public Property IdPicking As Integer = 0

    Public ReadOnly Property VolumenUmBas()
        Get
            Return CantidadUmBas * AltoUMBas * LargoUmBas * AnchoUmBas
        End Get
    End Property

    Public ReadOnly Property VolumenUmBasEnUbicacion()
        Get
            Return VolumenUmBas
        End Get
    End Property

    Public ReadOnly Property VolumenPresEnUbicacion()
        Get
            Return VolumenPresentacion
        End Get
    End Property

    Public ReadOnly Property VolumenPresentacion()
        Get
            Return CantidadPresentacion * BePresentacionProductoEnStock.Alto * BePresentacionProductoEnStock.Largo * BePresentacionProductoEnStock.Ancho
        End Get
    End Property

    Public Property TotalLinea As Double = 0

    Public Property BePresentacionProductoEnStock As New clsBeProducto_Presentacion

    Public Property AltoUMBas As Double = 0
    Public Property LargoUmBas As Double = 0
    Public Property AnchoUmBas As Double = 0

    Public Sub Dispose() Implements IDisposable.Dispose
        If BePresentacionProductoEnStock IsNot Nothing Then
            BePresentacionProductoEnStock.Dispose()
            BePresentacionProductoEnStock = Nothing
        End If
    End Sub

    Public Property acepto As Boolean
    Public Property peso_pickeado As Double
    Public Property peso_verificado As Double
    Public Property Cantidad_Pickeada As Double
    Public Property Cantidad_Verificada As Double
    Public Property Cantidad_Despachada As Double
    Public Property encontrado As Boolean
    Public Property UbicacionActual As New clsBeBodega_ubicacion
    Public Property Cantidad_Res As Double = 0
    Public Property ValorTexto As String
    Public Property ValorNumerico As Double
    Public Property ValorFecha As Date
    Public Property ValorLogico As Boolean = False
    Public Property No_Serie As String
    Public Property No_Serie_Inicial As String
    Public Property No_Serie_Final As String
    Public Property CantidadReservada As Double = 0
    Public Property IdFamilia As Integer = 0
    Public Property IdClasificacion As Integer = 0
    Public Property IdTipoProducto As Integer = 0
    Public Property NombreTipoProducto As String = ""
    Public Property Documento_Ingreso As String = ""
    Public Property Posiciones As Integer = 0
    Public Property codigo_poliza As String = ""
    Public Property Numero_poliza As String = ""
    Public Property ubicacion_picking As Boolean = False
    Public Property CamasPorTarima As Double = 0
    Public Property CajasPorCama As Double = 0
    Public Property es_rack As Boolean = False

    '#EJC20220330: Si el producto está reservado en picking y se quiere cambiar de ubicación, aquí se colocará la ubicación temporal de destino.
    Public Property IdUbicacionVirtual As Integer = 0

    '#CKKF20220627 Campos agregados en la vista para poder ver estos datos en la HH
    Public Property Fecha_Pedido As Date
    Public Property Fecha_Preparacion As Date
    ''' <summary>
    ''' #AT20240621: Se utiliza en la HH para enviar el movimiento generado
    ''' en el cambio de ubicacion para licencias completas 
    ''' </summary>
    ''' <returns></returns>
    Public Property Movimiento As clsBeTrans_movimientos

End Class