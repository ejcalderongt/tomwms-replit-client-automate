Public Class clsBeStock_transito
    Implements ICloneable

    Private mIdStockTransito As Integer = 0
    Private mIdEmpresa As Integer = 0
    Private mIdBodegaOrigen As Integer = 0
    Private mIdBodegaDestino As Integer = 0
    Private mIdStock As Integer = 0
    Private mIdProductoBodega As Integer = 0
    Private mIdProductoEstado As Integer = 0
    Private mIdPresentacion As Integer = 0
    Private mIdUnidadMedida As Integer = 0
    Private mIdUbicacion As Integer = 0
    Private mIdRecepcionEnc As Integer = 0
    Private mIdRecepcionDet As Integer = 0
    Private mIdPedidoEnc As Integer = 0
    Private mIdPickingEnc As Integer = 0
    Private mIdDespachoEnc As Integer = 0
    Private mIdPickingUbic As Integer = 0
    Private mIdPedidoDet As Integer = 0
    Private mLote As String = ""
    Private mLic_Plate As String = ""
    Private mCantidad As Double = 0.0
    Private mFecha_Ingreso As Date = New Date(1900, 1, 1)
    Private mFecha_Vence As String = Nothing
    Private mFecha_Manufactura As Date = New Date(1900, 1, 1)
    Private mCantidad_Recibida As Double = 0.0
    Private mFecha_Agregado As Date = New Date(1900, 1, 1)

    Public Property IdStockTransito() As Integer
        Get
            Return mIdStockTransito
        End Get
        Set(ByVal Value As Integer)
            mIdStockTransito = Value
        End Set
    End Property

    Public Property IdEmpresa() As Integer
        Get
            Return mIdEmpresa
        End Get
        Set(ByVal Value As Integer)
            mIdEmpresa = Value
        End Set
    End Property

    Public Property IdBodegaOrigen() As Integer
        Get
            Return mIdBodegaOrigen
        End Get
        Set(ByVal Value As Integer)
            mIdBodegaOrigen = Value
        End Set
    End Property

    Public Property IdBodegaDestino() As Integer
        Get
            Return mIdBodegaDestino
        End Get
        Set(ByVal Value As Integer)
            mIdBodegaDestino = Value
        End Set
    End Property

    Public Property IdStock() As Integer
        Get
            Return mIdStock
        End Get
        Set(ByVal Value As Integer)
            mIdStock = Value
        End Set
    End Property

    Public Property IdProductoBodegaDestino() As Integer
        Get
            Return mIdProductoBodega
        End Get
        Set(ByVal Value As Integer)
            mIdProductoBodega = Value
        End Set
    End Property

    Public Property IdProductoEstado() As Integer
        Get
            Return mIdProductoEstado
        End Get
        Set(ByVal Value As Integer)
            mIdProductoEstado = Value
        End Set
    End Property

    Public Property IdPresentacion() As Integer
        Get
            Return mIdPresentacion
        End Get
        Set(ByVal Value As Integer)
            mIdPresentacion = Value
        End Set
    End Property

    Public Property IdUnidadMedida() As Integer
        Get
            Return mIdUnidadMedida
        End Get
        Set(ByVal Value As Integer)
            mIdUnidadMedida = Value
        End Set
    End Property

    Public Property IdUbicacion() As Integer
        Get
            Return mIdUbicacion
        End Get
        Set(ByVal Value As Integer)
            mIdUbicacion = Value
        End Set
    End Property

    Public Property IdRecepcionEnc() As Integer
        Get
            Return mIdRecepcionEnc
        End Get
        Set(ByVal Value As Integer)
            mIdRecepcionEnc = Value
        End Set
    End Property

    Public Property IdRecepcionDet() As Integer
        Get
            Return mIdRecepcionDet
        End Get
        Set(ByVal Value As Integer)
            mIdRecepcionDet = Value
        End Set
    End Property

    Public Property IdPedidoEnc() As Integer
        Get
            Return mIdPedidoEnc
        End Get
        Set(ByVal Value As Integer)
            mIdPedidoEnc = Value
        End Set
    End Property

    Public Property IdPickingEnc() As Integer
        Get
            Return mIdPickingEnc
        End Get
        Set(ByVal Value As Integer)
            mIdPickingEnc = Value
        End Set
    End Property

    Public Property IdDespachoEnc() As Integer
        Get
            Return mIdDespachoEnc
        End Get
        Set(ByVal Value As Integer)
            mIdDespachoEnc = Value
        End Set
    End Property

    Public Property IdPickingUbic() As Integer
        Get
            Return mIdPickingUbic
        End Get
        Set(ByVal Value As Integer)
            mIdPickingUbic = Value
        End Set
    End Property

    Public Property IdPedidoDet() As Integer
        Get
            Return mIdPedidoDet
        End Get
        Set(ByVal Value As Integer)
            mIdPedidoDet = Value
        End Set
    End Property

    Public Property Lote() As String
        Get
            Return mLote
        End Get
        Set(ByVal Value As String)
            mLote = Value
        End Set
    End Property

    Public Property Lic_Plate() As String
        Get
            Return mLic_Plate
        End Get
        Set(ByVal Value As String)
            mLic_Plate = Value
        End Set
    End Property

    Public Property Cantidad() As Double
        Get
            Return mCantidad
        End Get
        Set(ByVal Value As Double)
            mCantidad = Value
        End Set
    End Property

    Public Property Fecha_Ingreso() As Date
        Get
            Return mFecha_Ingreso
        End Get
        Set(ByVal Value As Date)
            mFecha_Ingreso = Value
        End Set
    End Property

    Public Property Fecha_Vence() As Date
        Get
            Return mFecha_Vence
        End Get
        Set(ByVal Value As Date)
            mFecha_Vence = Value
        End Set
    End Property

    Public Property Fecha_Manufactura() As Date
        Get
            Return mFecha_Manufactura
        End Get
        Set(ByVal Value As Date)
            mFecha_Manufactura = Value
        End Set
    End Property

    Public Property Cantidad_Recibida() As Double
        Get
            Return mCantidad_Recibida
        End Get
        Set(ByVal Value As Double)
            mCantidad_Recibida = Value
        End Set
    End Property

    Public Property Fecha_Agregado() As Date
        Get
            Return mFecha_Agregado
        End Get
        Set(ByVal Value As Date)
            mFecha_Agregado = Value
        End Set
    End Property

    Public Property IdOrdenCompraEnc_BodDest As Integer = 0
    Public Property IdRecepcionEnc_BodDest As Integer = 0
    Public Property IdProductoBodegaOrigen As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef IdStockTransito As Integer, ByVal IdEmpresa As Integer, ByVal IdBodegaOrigen As Integer, ByVal IdBodegaDestino As Integer, ByVal IdStock As Integer, ByVal IdProductoBodega As Integer, ByVal IdProductoEstado As Integer, ByVal IdPresentacion As Integer, ByVal IdUnidadMedida As Integer, ByVal IdUbicacion As Integer, ByVal IdRecepcionEnc As Integer, ByVal IdRecepcionDet As Integer, ByVal IdPedidoEnc As Integer, ByVal IdPickingEnc As Integer, ByVal IdDespachoEnc As Integer, ByVal IdPickingUbic As Integer, ByVal IdPedidoDet As Integer, ByVal Lote As String, ByVal Lic_Plate As String, ByVal Cantidad As Double, ByVal Fecha_Ingreso As String, ByVal Fecha_Vence As String, ByVal Fecha_Manufactura As String, ByVal Cantidad_Recibida As Double, ByVal Fecha_Agregado As String)
        mIdStockTransito = IdStockTransito
        mIdEmpresa = IdEmpresa
        mIdBodegaOrigen = IdBodegaOrigen
        mIdBodegaDestino = IdBodegaDestino
        mIdStock = IdStock
        mIdProductoBodega = IdProductoBodega
        mIdProductoEstado = IdProductoEstado
        mIdPresentacion = IdPresentacion
        mIdUnidadMedida = IdUnidadMedida
        mIdUbicacion = IdUbicacion
        mIdRecepcionEnc = IdRecepcionEnc
        mIdRecepcionDet = IdRecepcionDet
        mIdPedidoEnc = IdPedidoEnc
        mIdPickingEnc = IdPickingEnc
        mIdDespachoEnc = IdDespachoEnc
        mIdPickingUbic = IdPickingUbic
        mIdPedidoDet = IdPedidoDet
        mLote = Lote
        mLic_Plate = Lic_Plate
        mCantidad = Cantidad
        mFecha_Ingreso = Fecha_Ingreso
        mFecha_Vence = Fecha_Vence
        mFecha_Manufactura = Fecha_Manufactura
        mCantidad_Recibida = Cantidad_Recibida
        mFecha_Agregado = Fecha_Agregado
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
