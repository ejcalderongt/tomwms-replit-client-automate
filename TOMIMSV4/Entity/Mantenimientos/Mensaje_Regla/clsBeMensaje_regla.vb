Public Class clsBeMensaje_regla
    Implements ICloneable

    Private mIdMensajeRegla As Integer = 0
    Private mNombre As String = ""
    Private mFec_agr As Date = Date.Now
    Private mUser_agr As String = ""
    Private mFec_mod As Date = Date.Now
    Private mUser_mod As String = ""
    Private mActivo As Boolean = False
    Private mIdReglaRecepcion As Integer = 0

    Public Property IdMensajeRegla() As Integer
        Get
            Return mIdMensajeRegla
        End Get
        Set(ByVal Value As Integer)
            mIdMensajeRegla = Value
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

    Public Property Fec_agr() As Date
        Get
            Return mFec_agr
        End Get
        Set(ByVal Value As Date)
            mFec_agr = Value
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

    Public Property Fec_mod() As Date
        Get
            Return mFec_mod
        End Get
        Set(ByVal Value As Date)
            mFec_mod = Value
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

    Public Property Activo() As Boolean
        Get
            Return mActivo
        End Get
        Set(ByVal Value As Boolean)
            mActivo = Value
        End Set
    End Property

    Public Property IdReglaRecepcion() As Integer
        Get
            Return mIdReglaRecepcion
        End Get
        Set(ByVal Value As Integer)
            mIdReglaRecepcion = Value
        End Set
    End Property

    Sub New()
    End Sub

    Sub New(ByRef IdMensajeRegla As Integer, ByVal Nombre As String, ByVal fec_agr As Date, ByVal user_agr As String, ByVal fec_mod As Date, ByVal user_mod As String, ByVal activo As Boolean)
        mIdMensajeRegla = IdMensajeRegla
        mNombre = Nombre
        mFec_agr = Fec_agr
        mUser_agr = User_agr
        mFec_mod = Fec_mod
        mUser_mod = User_mod
        mActivo = Activo
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
