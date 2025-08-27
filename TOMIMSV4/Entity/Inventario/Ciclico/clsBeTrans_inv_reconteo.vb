Public Class clsBeTrans_inv_reconteo
    Implements ICloneable

    Private mIdinvreconteo As Integer = 0
    Private mIdreconteo As Integer = 0
    Private mIdinvciclico As Integer = 0
    Private mIdinventarioenc As Integer = 0
    Private mIdStock As Integer = 0
    Private mIdProductoBodega As Integer = 0
    Private mIdProductoEstado As Integer = 0
    Private mIdPresentacion As Integer = 0
    Private mIdUbicacionAnterior As Integer = 0
    Private mIdUbicacion As Integer = 0
    Private mEsNuevo As Boolean = False
    Private mCantidadAnterior As Double = 0.0
    Private mCantidad As Double = 0.0
    Private mLote As String = ""
    Private mFecha_vence As Date = Date.Now
    Private mPesoAnterior As Double = 0.0
    Private mPeso As Double = 0.0
    Private mUser_agr As String = ""
    Private mFec_agr As Date = Date.Now
    Private mIdOperador As Integer = 0
    Private mEsPallet As Boolean = False
    Private mlic_plate As String = ""

    Public Property Idinvreconteo() As Integer
        Get
            Return mIdinvreconteo
        End Get
        Set(ByVal Value As Integer)
            mIdinvreconteo = Value
        End Set
    End Property

    Public Property Idreconteo() As Integer
        Get
            Return mIdreconteo
        End Get
        Set(ByVal Value As Integer)
            mIdreconteo = Value
        End Set
    End Property

    Public Property Idinvciclico() As Integer
        Get
            Return mIdinvciclico
        End Get
        Set(ByVal Value As Integer)
            mIdinvciclico = Value
        End Set
    End Property

    Public Property Idinventarioenc() As Integer
        Get
            Return mIdinventarioenc
        End Get
        Set(ByVal Value As Integer)
            mIdinventarioenc = Value
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

    Public Property IdProductoBodega() As Integer
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

    Public Property IdUbicacionAnterior() As Integer
        Get
            Return mIdUbicacionAnterior
        End Get
        Set(ByVal Value As Integer)
            mIdUbicacionAnterior = Value
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

    Public Property EsNuevo() As Boolean
        Get
            Return mEsNuevo
        End Get
        Set(ByVal Value As Boolean)
            mEsNuevo = Value
        End Set
    End Property

    Public Property CantidadAnterior() As Double
        Get
            Return mCantidadAnterior
        End Get
        Set(ByVal Value As Double)
            mCantidadAnterior = Value
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

    Public Property Lote() As String
        Get
            Return mLote
        End Get
        Set(ByVal Value As String)
            mLote = Value
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

    Public Property PesoAnterior() As Double
        Get
            Return mPesoAnterior
        End Get
        Set(ByVal Value As Double)
            mPesoAnterior = Value
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

    Public Property IdOperador() As Integer
        Get
            Return mIdOperador
        End Get
        Set(ByVal Value As Integer)
            mIdOperador = Value
        End Set
    End Property

    Public Property EsPallet() As Boolean
        Get
            Return mEsPallet
        End Get
        Set(ByVal Value As Boolean)
            mEsPallet = Value
        End Set
    End Property

    Public Property lic_plate() As String
        Get
            Return mlic_plate
        End Get
        Set(ByVal Value As String)
            mlic_plate = Value
        End Set
    End Property

    Sub New()
    End Sub

    Sub New(ByRef idinvreconteo As Integer, ByVal idreconteo As Integer, ByVal idinvciclico As Integer, ByVal idinventarioenc As Integer, ByVal IdStock As Integer, ByVal IdProductoBodega As Integer, ByVal IdProductoEstado As Integer, ByVal IdPresentacion As Integer, ByVal idUbicacionAnterior As Integer, ByVal IdUbicacion As Integer, ByVal EsNuevo As Boolean, ByVal cantidadAnterior As Double, ByVal cantidad As Double, ByVal lote As String, ByVal fecha_vence As Date, ByVal pesoAnterior As Double, ByVal peso As Double, ByVal user_agr As String, ByVal fec_agr As Date, ByVal IdOperador As Integer, ByVal EsPallet As Boolean, ByVal lic_plate As String)
        mIdinvreconteo = idinvreconteo
        mIdreconteo = idreconteo
        mIdinvciclico = idinvciclico
        mIdinventarioenc = idinventarioenc
        mIdStock = IdStock
        mIdProductoBodega = IdProductoBodega
        mIdProductoEstado = IdProductoEstado
        mIdPresentacion = IdPresentacion
        mIdUbicacionAnterior = idUbicacionAnterior
        mIdUbicacion = IdUbicacion
        mEsNuevo = EsNuevo
        mCantidadAnterior = cantidadAnterior
        mCantidad = cantidad
        mLote = lote
        mFecha_vence = fecha_vence
        mPesoAnterior = pesoAnterior
        mPeso = peso
        mUser_agr = user_agr
        mFec_agr = fec_agr
        mIdOperador = IdOperador
        mEsPallet = EsPallet
        mlic_plate = lic_plate
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
