Public Class clsBeHorario_laboral_enc
    Implements ICloneable

    Private mIdHorarioLaboralEnc As Integer = 0
    Private mIdBodega As Integer = 0
    Private mIdJornada As Integer = 0
    Private mIdTurno As Integer = 0
    Private mNombre As String = ""
    Private mUser_agr As String = ""
    Private mFec_agr As Date = Date.Now
    Private mUser_mod As String = ""
    Private mFec_mod As Date = Date.Now
    Private mActivo As Boolean = False

    Public Property IdHorarioLaboralEnc() As Integer
        Get
            Return mIdHorarioLaboralEnc
        End Get
        Set(ByVal Value As Integer)
            mIdHorarioLaboralEnc = Value
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

    Public Property IdTurno() As Integer
        Get
            Return mIdTurno
        End Get
        Set(ByVal Value As Integer)
            mIdTurno = Value
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

    Public Property Activo() As Boolean
        Get
            Return mActivo
        End Get
        Set(ByVal Value As Boolean)
            mActivo = Value
        End Set
    End Property

    Sub New()
    End Sub

    Sub New(ByRef IdHorarioLaboralEnc As Integer, ByVal IdBodega As Integer, ByVal IdJornada As Integer, ByVal IdTurno As Integer, ByVal nombre As String, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean)
        mIdHorarioLaboralEnc = IdHorarioLaboralEnc
        mIdBodega = IdBodega
        mIdJornada = IdJornada
        mIdTurno = IdTurno
        mNombre = nombre
        mUser_agr = user_agr
        mFec_agr = fec_agr
        mUser_mod = user_mod
        mFec_mod = fec_mod
        mActivo = activo
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
