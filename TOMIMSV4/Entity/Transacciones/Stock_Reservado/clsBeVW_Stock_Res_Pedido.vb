Public Class clsBeVW_Stock_Res_Pedido
    Implements ICloneable

    Private mCodigo As String = ""
    Private mNombre As String = ""
    Private mPresentacion As String = ""
    Private mNomEstado As String = ""
    Private mUnidadmedida As String = ""
    Private mPropietario As String = ""
    Private mBodegaubicacion As String = ""
    Private mCantidadfisica As Double = 0.0
    Private mFactor As Double = 0.0
    Private mIdStockRes As Integer = 0
    Private mIdTransaccion As Integer = 0
    Private mIndicador As String = ""
    Private mIdPedidoDet As Integer = 0
    Private mIdStock As Integer = 0
    Private mIdPropietarioBodega As Integer = 0
    Private mIdProductoBodega As Integer = 0
    Private mIdUbicacion As Integer = 0
    Private mEstado As String = ""
    Private mIdPresentacion As Integer = 0
    Private mIdUnidadMedida As Integer = 0
    Private mLote As String = ""
    Private mLic_plate As String = ""
    Private mSerial As String = ""
    Private mCantidad As Double = 0.0
    Private mPeso As Double = 0.0
    Private mFecha_ingreso As Date = Date.Now
    Private mFecha_vence As Date = Date.Now
    Private mUds_lic_plate As Double = 0.0
    Private mUbicacion_ant As String = ""
    Private mNo_bulto As Integer = 0
    Private mIdRecepcion As Integer = 0
    Private mIdPicking As Integer = 0
    Private mIdPedido As Integer = 0
    Private mIdDespacho As Integer = 0
    Private mUser_agr As String = ""
    Private mFec_agr As Date = Date.Now
    Private mUser_mod As String = ""
    Private mFec_mod As Date = Date.Now
    Private mHost As String = ""
    Private mAñada As Integer = 0
    Private mFecha_manufactura As Date = Date.Now
    Private mReferencia As String = ""
    Private mIdBodega As Integer = 0

    Public Property Codigo() As String
        Get
            Return mCodigo
        End Get
        Set(ByVal Value As String)
            mCodigo = Value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return mNombre
        End Get
        Set(ByVal Value As String)
            mNombre = Value
        End Set
    End Property

    Public Property Presentacion() As String
        Get
            Return mPresentacion
        End Get
        Set(ByVal Value As String)
            mPresentacion = Value
        End Set
    End Property

    Public Property NomEstado() As String
        Get
            Return mNomEstado
        End Get
        Set(ByVal Value As String)
            mNomEstado = Value
        End Set
    End Property

    Public Property Unidadmedida() As String
        Get
            Return mUnidadmedida
        End Get
        Set(ByVal Value As String)
            mUnidadmedida = Value
        End Set
    End Property

    Public Property Propietario() As String
        Get
            Return mPropietario
        End Get
        Set(ByVal Value As String)
            mPropietario = Value
        End Set
    End Property

    Public Property Bodegaubicacion() As String
        Get
            Return mBodegaubicacion
        End Get
        Set(ByVal Value As String)
            mBodegaubicacion = Value
        End Set
    End Property

    Public Property Cantidadfisica() As Double
        Get
            Return mCantidadfisica
        End Get
        Set(ByVal Value As Double)
            mCantidadfisica = Value
        End Set
    End Property

    Public Property Factor() As Double
        Get
            Return mFactor
        End Get
        Set(ByVal Value As Double)
            mFactor = Value
        End Set
    End Property

    Public Property IdStockRes() As Integer
        Get
            Return mIdStockRes
        End Get
        Set(ByVal Value As Integer)
            mIdStockRes = Value
        End Set
    End Property

    Public Property IdTransaccion() As Integer
        Get
            Return mIdTransaccion
        End Get
        Set(ByVal Value As Integer)
            mIdTransaccion = Value
        End Set
    End Property

    Public Property Indicador() As String
        Get
            Return mIndicador
        End Get
        Set(ByVal Value As String)
            mIndicador = Value
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

    Public Property IdStock() As Integer
        Get
            Return mIdStock
        End Get
        Set(ByVal Value As Integer)
            mIdStock = Value
        End Set
    End Property

    Public Property IdPropietarioBodega() As Integer
        Get
            Return mIdPropietarioBodega
        End Get
        Set(ByVal Value As Integer)
            mIdPropietarioBodega = Value
        End Set
    End Property

    Public Property IdProductoBodega() As Integer
        Get
            Return mIdProductoBodega
        End Get
        Set(ByVal Value As Integer)
            mIdProductoBodega = Value
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

    Public Property Estado() As String
        Get
            Return mEstado
        End Get
        Set(ByVal Value As String)
            mEstado = Value
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

    Public Property Lote() As String
        Get
            Return mLote
        End Get
        Set(ByVal Value As String)
            mLote = Value
        End Set
    End Property

    Public Property Lic_plate() As String
        Get
            Return mLic_plate
        End Get
        Set(ByVal Value As String)
            mLic_plate = Value
        End Set
    End Property

    Public Property Serial() As String
        Get
            Return mSerial
        End Get
        Set(ByVal Value As String)
            mSerial = Value
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

    Public Property Peso() As Double
        Get
            Return mPeso
        End Get
        Set(ByVal Value As Double)
            mPeso = Value
        End Set
    End Property

    Public Property Fecha_ingreso() As Date
        Get
            Return mFecha_ingreso
        End Get
        Set(ByVal Value As Date)
            mFecha_ingreso = Value
        End Set
    End Property

    Public Property Fecha_vence() As Date
        Get
            Return mFecha_vence
        End Get
        Set(ByVal Value As Date)
            mFecha_vence = Value
        End Set
    End Property

    Public Property Uds_lic_plate() As Double
        Get
            Return mUds_lic_plate
        End Get
        Set(ByVal Value As Double)
            mUds_lic_plate = Value
        End Set
    End Property

    Public Property Ubicacion_ant() As String
        Get
            Return mUbicacion_ant
        End Get
        Set(ByVal Value As String)
            mUbicacion_ant = Value
        End Set
    End Property

    Public Property No_bulto() As Integer
        Get
            Return mNo_bulto
        End Get
        Set(ByVal Value As Integer)
            mNo_bulto = Value
        End Set
    End Property

    Public Property IdRecepcion() As Integer
        Get
            Return mIdRecepcion
        End Get
        Set(ByVal Value As Integer)
            mIdRecepcion = Value
        End Set
    End Property

    Public Property IdPicking() As Integer
        Get
            Return mIdPicking
        End Get
        Set(ByVal Value As Integer)
            mIdPicking = Value
        End Set
    End Property

    Public Property IdPedido() As Integer
        Get
            Return mIdPedido
        End Get
        Set(ByVal Value As Integer)
            mIdPedido = Value
        End Set
    End Property

    Public Property IdDespacho() As Integer
        Get
            Return mIdDespacho
        End Get
        Set(ByVal Value As Integer)
            mIdDespacho = Value
        End Set
    End Property

    Public Property User_agr() As String
        Get
            Return mUser_agr
        End Get
        Set(ByVal Value As String)
            mUser_agr = Value
        End Set
    End Property

    Public Property Fec_agr() As Date
        Get
            Return mFec_agr
        End Get
        Set(ByVal Value As Date)
            mFec_agr = Value
        End Set
    End Property

    Public Property User_mod() As String
        Get
            Return mUser_mod
        End Get
        Set(ByVal Value As String)
            mUser_mod = Value
        End Set
    End Property

    Public Property Fec_mod() As Date
        Get
            Return mFec_mod
        End Get
        Set(ByVal Value As Date)
            mFec_mod = Value
        End Set
    End Property

    Public Property Host() As String
        Get
            Return mHost
        End Get
        Set(ByVal Value As String)
            mHost = Value
        End Set
    End Property

    Public Property Añada() As Integer
        Get
            Return mAñada
        End Get
        Set(ByVal Value As Integer)
            mAñada = Value
        End Set
    End Property

    Public Property Fecha_manufactura() As Date
        Get
            Return mFecha_manufactura
        End Get
        Set(ByVal Value As Date)
            mFecha_manufactura = Value
        End Set
    End Property

    Public Property Referencia() As String
        Get
            Return mReferencia
        End Get
        Set(ByVal Value As String)
            mReferencia = Value
        End Set
    End Property

    Public Property IdBodega() As Integer
        Get
            Return mIdBodega
        End Get
        Set(ByVal Value As Integer)
            mIdBodega = Value
        End Set
    End Property

    Sub New()
    End Sub

    Sub New(ByRef codigo As String, ByVal nombre As String, ByVal presentacion As String, ByVal NomEstado As String, ByVal unidadmedida As String, ByVal propietario As String, ByVal bodegaubicacion As String, ByVal cantidadfisica As Double, ByVal factor As Double, ByVal IdStockRes As Integer, ByVal IdTransaccion As Integer, ByVal Indicador As String, ByVal IdPedidoDet As Integer, ByVal IdStock As Integer, ByVal IdPropietarioBodega As Integer, ByVal IdProductoBodega As Integer, ByVal IdUbicacion As Integer, ByVal estado As String, ByVal IdPresentacion As Integer, ByVal IdUnidadMedida As Integer, ByVal lote As String, ByVal lic_plate As String, ByVal serial As String, ByVal cantidad As Double, ByVal peso As Double, ByVal fecha_ingreso As Date, ByVal fecha_vence As Date, ByVal uds_lic_plate As Double, ByVal ubicacion_ant As String, ByVal no_bulto As Integer, ByVal IdRecepcion As Integer, ByVal IdPicking As Integer, ByVal IdPedido As Integer, ByVal IdDespacho As Integer, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal host As String, ByVal añada As Integer, ByVal fecha_manufactura As Date, ByVal refencia As String, ByVal IdBodega As Integer)
        mCodigo = codigo
        mNombre = nombre
        mPresentacion = presentacion
        mNomEstado = NomEstado
        mUnidadmedida = unidadmedida
        mPropietario = propietario
        mBodegaubicacion = bodegaubicacion
        mCantidadfisica = cantidadfisica
        mFactor = factor
        mIdStockRes = IdStockRes
        mIdTransaccion = IdTransaccion
        mIndicador = Indicador
        mIdPedidoDet = IdPedidoDet
        mIdStock = IdStock
        mIdPropietarioBodega = IdPropietarioBodega
        mIdProductoBodega = IdProductoBodega
        mIdUbicacion = IdUbicacion
        mEstado = estado
        mIdPresentacion = IdPresentacion
        mIdUnidadMedida = IdUnidadMedida
        mLote = lote
        mLic_plate = lic_plate
        mSerial = serial
        mCantidad = cantidad
        mPeso = peso
        mFecha_ingreso = fecha_ingreso
        mFecha_vence = fecha_vence
        mUds_lic_plate = uds_lic_plate
        mUbicacion_ant = ubicacion_ant
        mNo_bulto = no_bulto
        mIdRecepcion = IdRecepcion
        mIdPicking = IdPicking
        mIdPedido = IdPedido
        mIdDespacho = IdDespacho
        mUser_agr = user_agr
        mFec_agr = fec_agr
        mUser_mod = user_mod
        mFec_mod = fec_mod
        mHost = host
        mAñada = añada
        mFecha_manufactura = fecha_manufactura
        mReferencia = refencia
        mIdBodega = IdBodega
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
