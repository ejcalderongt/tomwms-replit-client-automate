Public Class clsBeTrans_series_doc
    Implements ICloneable

    Private mIdCorrelativo As Integer = 0
    Private mSerie As String = ""
    Private mTipo_Doc As String = ""
    Private mIdTipoTrans As Integer = 0
    Private mInicial As Integer = 0
    Private mFinal As Integer = 0
    Private mActual As Integer = 0
    Private mActivo As Boolean = False
    Private mIdBodega As Integer = 0
    Private mUserAgr As String = ""
    Private mFecAgr As Date = Date.Now
    Private mUserMod As String = ""
    Private mFecMod As Date = Date.Now

    Public Property IdTransSerieDoc() As Integer
        Get
            Return mIdCorrelativo
        End Get
        Set(ByVal Value As Integer)
            mIdCorrelativo = Value
        End Set
    End Property

    Public Property Serie() As String
        Get
            Return mSerie
        End Get
        Set(ByVal Value As String)
            mSerie = Value
        End Set
    End Property

    Public Property Tipo_Doc() As String
        Get
            Return mTipo_Doc
        End Get
        Set(ByVal Value As String)
            mTipo_Doc = Value
        End Set
    End Property

    Public Property IdTipoTrans() As Integer
        Get
            Return mIdTipoTrans
        End Get
        Set(ByVal Value As Integer)
            mIdTipoTrans = Value
        End Set
    End Property

    Public Property Inicial() As Integer
        Get
            Return mInicial
        End Get
        Set(ByVal Value As Integer)
            mInicial = Value
        End Set
    End Property

    Public Property Final() As Integer
        Get
            Return mFinal
        End Get
        Set(ByVal Value As Integer)
            mFinal = Value
        End Set
    End Property

    Public Property Actual() As Integer
        Get
            Return mActual
        End Get
        Set(ByVal Value As Integer)
            mActual = Value
        End Set
    End Property

    Public Property Activo() As Boolean
        Get
            Return mActivo
        End Get
        Set(ByVal Value As Boolean)
            mActivo = Value
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

    Public Property UserAgr() As String
        Get
            Return mUserAgr
        End Get
        Set(ByVal Value As String)
            mUserAgr = Value
        End Set
    End Property

    Public Property FecAgr() As Date
        Get
            Return mFecAgr
        End Get
        Set(ByVal Value As Date)
            mFecAgr = Value
        End Set
    End Property

    Public Property UserMod() As String
        Get
            Return mUserMod
        End Get
        Set(ByVal Value As String)
            mUserMod = Value
        End Set
    End Property

    Public Property FecMod() As Date
        Get
            Return mFecMod
        End Get
        Set(ByVal Value As Date)
            mFecMod = Value
        End Set
    End Property

    Sub New()
    End Sub

    Sub New(ByRef IdCorrelativo As Integer, ByVal Serie As String, ByVal Tipo_Doc As String, ByVal IdTipoTrans As Integer, ByVal Inicial As Integer, ByVal Final As Integer, ByVal Actual As Integer, ByVal Activo As Boolean, ByVal IdBodega As Integer, ByVal UserAgr As String, ByVal FecAgr As Date, ByVal UserMod As String, ByVal FecMod As Date)
        mIdCorrelativo = IdCorrelativo
        mSerie = Serie
        mTipo_Doc = Tipo_Doc
        mIdTipoTrans = IdTipoTrans
        mInicial = Inicial
        mFinal = Final
        mActual = Actual
        mActivo = Activo
        mIdBodega = IdBodega
        mUserAgr = UserAgr
        mFecAgr = FecAgr
        mUserMod = UserMod
        mFecMod = FecMod
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
