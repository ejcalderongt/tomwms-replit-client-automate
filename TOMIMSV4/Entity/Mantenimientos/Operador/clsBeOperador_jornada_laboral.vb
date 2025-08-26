Public Class clsBeOperador_jornada_laboral
    Implements ICloneable

    Private mIdOperadorJornadaLaboral As Integer = 0
    Private mIdOperador As Integer = 0
    Private mIdBodega As Integer = 0
    Private mIdJornada As Integer = 0
    Private mActivo As Boolean = False
    Private mUser_agr As String = ""
    Private mFec_agr As Date = Date.Now
    Private mUser_mod As String = ""
    Private mFec_mod As Date = Date.Now

    Public Property IdOperadorJornadaLaboral() As Integer
        Get
            Return mIdOperadorJornadaLaboral
        End Get
        Set(ByVal Value As Integer)
            mIdOperadorJornadaLaboral = Value
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

    Public Property IdBodega() As Integer
        Get
            Return mIdBodega
        End Get
        Set(ByVal Value As Integer)
            mIdBodega = Value
        End Set
    End Property

    Public Property IdJornada() As Integer
        Get
            Return mIdJornada
        End Get
        Set(ByVal Value As Integer)
            mIdJornada = Value
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

    Sub New(ByRef IdOperadorJornadaLaboral As Integer, ByVal IdOperador As Integer, ByVal IdBodega As Integer, ByVal IdJornada As Integer, ByVal activo As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date)
        mIdOperadorJornadaLaboral = IdOperadorJornadaLaboral
        mIdOperador = IdOperador
        mIdBodega = IdBodega
        mIdJornada = IdJornada
        mActivo = Activo
        mUser_agr = User_agr
        mFec_agr = Fec_agr
        mUser_mod = User_mod
        mFec_mod = Fec_mod
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
