Public Class clsBeTrans_re_det_lote_num
    Implements ICloneable

    Private mIdLoteNum As Integer = 0
    Private mIdRecepcionEnc As Integer = 0
    Private mIdProductoBodega As Integer = 0
    Private mCodigo As String = ""
    Private mLote As String = ""
    Private mLoteNum As Integer = 0
    Private mFechaIngreso As Date = Now
    Private mCantidad As Double = 0.0

    Public Property IdLoteNum() As Integer
        Get
            Return mIdLoteNum
        End Get
        Set(ByVal Value As Integer)
            mIdLoteNum = Value
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

    Public Property IdProductoBodega() As Integer
        Get
            Return mIdProductoBodega
        End Get
        Set(ByVal Value As Integer)
            mIdProductoBodega = Value
        End Set
    End Property

    Public Property Codigo() As String
        Get
            Return mCodigo
        End Get
        Set(ByVal Value As String)
            mCodigo = Value
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

    Public Property Lote_Numerico() As Integer
        Get
            Return mLoteNum
        End Get
        Set(ByVal Value As Integer)
            mLoteNum = Value
        End Set
    End Property

    Public Property FechaIngreso() As Date
        Get
            Return mFechaIngreso
        End Get
        Set(ByVal Value As Date)
            mFechaIngreso = Value
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

    Sub New()
    End Sub

    Sub New(ByRef IdLoteNum As Integer, ByVal IdRecepcionEnc As Integer, ByVal IdProductoBodega As Integer, ByVal Codigo As String, ByVal Lote As String, ByVal LoteNum As Integer, ByVal FechaIngreso As Date, ByVal Cantidad As Double)
        mIdLoteNum = IdLoteNum
        mIdRecepcionEnc = IdRecepcionEnc
        mIdProductoBodega = IdProductoBodega
        mCodigo = Codigo
        mLote = Lote
        mLoteNum = LoteNum
        mFechaIngreso = FechaIngreso
        mCantidad = Cantidad
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
