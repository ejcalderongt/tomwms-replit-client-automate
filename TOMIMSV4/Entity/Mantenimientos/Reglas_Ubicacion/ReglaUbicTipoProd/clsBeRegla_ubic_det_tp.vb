Public Class clsBeRegla_ubic_det_tp
    Implements ICloneable

    Private mIdReglaUbicacoinTP As Integer = 0
    Private mIdReglaUbicacionEnc As Integer = 0
    Private mIdTipoProducto As Integer = 0
    Private mActivo As Boolean = False
    Private mUser_agr As String = ""
    Private mFec_agr As Date = Date.Now
    Private mUser_mod As String = ""
    Private mFec_mod As Date = Date.Now

    Public Property IdReglaUbicacoinTP() As Integer
        Get
            Return mIdReglaUbicacoinTP
        End Get
        Set(ByVal Value As Integer)
            mIdReglaUbicacoinTP = Value
        End Set
    End Property

    Public Property IdReglaUbicacionEnc() As Integer
        Get
            Return mIdReglaUbicacionEnc
        End Get
        Set(ByVal Value As Integer)
            mIdReglaUbicacionEnc = Value
        End Set
    End Property

    Public Property IdTipoProducto() As Integer
        Get
            Return mIdTipoProducto
        End Get
        Set(ByVal Value As Integer)
            mIdTipoProducto = Value
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

    Sub New(ByRef IdReglaUbicacoinTP As Integer, ByVal IdReglaUbicacionEnc As Integer, ByVal IdTipoProducto As Integer, ByVal Activo As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date)
        mIdReglaUbicacoinTP = IdReglaUbicacoinTP
        mIdReglaUbicacionEnc = IdReglaUbicacionEnc
        mIdTipoProducto = IdTipoProducto
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
