Public Class clsBeTrans_ubic_hh_op
    Implements ICloneable

    Private mIdTransUbicHhOp As Integer = 0
    Private mIdTareaUbicacionEnc As Integer = 0
    Private mIdOperadorBodega As Integer = 0
    Private mUser_agr As String = ""
    Private mFec_agr As Date = Date.Now
    Private mUser_mod As String = ""
    Private mFec_mod As Date = Date.Now

    Public Property IdTransUbicHhOp() As Integer
        Get
            Return mIdTransUbicHhOp
        End Get
        Set(ByVal Value As Integer)
            mIdTransUbicHhOp = Value
        End Set
    End Property

    Public Property IdTareaUbicacionEnc() As Integer
        Get
            Return mIdTareaUbicacionEnc
        End Get
        Set(ByVal Value As Integer)
            mIdTareaUbicacionEnc = Value
        End Set
    End Property

    Public Property IdOperadorBodega() As Integer
        Get
            Return mIdOperadorBodega
        End Get
        Set(ByVal Value As Integer)
            mIdOperadorBodega = Value
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

    Sub New()
    End Sub

    Sub New(ByRef IdTransUbicHhOp As Integer, ByVal IdTareaUbicacionEnc As Integer, ByVal IdOperadorBodega As Integer, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date)
        mIdTransUbicHhOp = IdTransUbicHhOp
        mIdTareaUbicacionEnc = IdTareaUbicacionEnc
        mIdOperadorBodega = IdOperadorBodega
        mUser_agr = User_agr
        mFec_agr = Fec_agr
        mUser_mod = User_mod
        mFec_mod = Fec_mod
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
