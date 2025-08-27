Public Class clsBeEmpresa_transporte
    Implements ICloneable

    Private mIdEmpresaTransporte As Integer = 0
    Private mIdEmpresa As Integer = 0
    Private mNombre As String = ""
    Private mActivo As Boolean = False
    Private mUser_agr As String = ""
    Private mFec_agr As Date = Date.Now
    Private mUser_mod As String = ""
    Private mFec_mod As Date = Date.Now

    Public Property IdEmpresaTransporte() As Integer
        Get
            Return mIdEmpresaTransporte
        End Get
        Set(ByVal Value As Integer)
            mIdEmpresaTransporte = Value
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

    Public Property Nombre() As String
        Get
            Return mNombre
        End Get
        Set(ByVal Value As String)
            mNombre = Value
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

    Sub New(ByRef IdEmpresaTransporte As Integer, ByVal IdEmpresa As Integer, ByVal nombre As String, ByVal activo As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date)
        mIdEmpresaTransporte = IdEmpresaTransporte
        mIdEmpresa = IdEmpresa
        mNombre = Nombre
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
