Public Class clsBeTrans_inv_enc_reconteo
    Implements ICloneable

    Private mIdinvencreconteo As Integer = 0
    Private mIdinventarioenc As Integer = 0
    Private mReconteo As Integer = 0
    Private mEstado As String = ""
    Private mHora_ini As Date = Date.Now
    Private mHora_fin As Date = Date.Now
    Private mUser_agr As String = ""
    Private mFec_agr As Date = Date.Now
    Private mUser_mod As String = ""
    Private mFec_mod As Date = Date.Now

    Public Property Idinvencreconteo() As Integer
        Get
            Return mIdinvencreconteo
        End Get
        Set(ByVal Value As Integer)
            mIdinvencreconteo = Value
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

    Public Property Reconteo() As Integer
        Get
            Return mReconteo
        End Get
        Set(ByVal Value As Integer)
            mReconteo = Value
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

    Public Property Hora_ini() As Date
        Get
            Return mHora_ini
        End Get
        Set(ByVal Value As Date)
            mHora_ini = Value
        End Set
    End Property

    Public Property Hora_fin() As Date
        Get
            Return mHora_fin
        End Get
        Set(ByVal Value As Date)
            mHora_fin = Value
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

    Sub New(ByRef idinvencreconteo As Integer, ByVal idinventarioenc As Integer, ByVal reconteo As Integer, ByVal estado As String, ByVal hora_ini As Date, ByVal hora_fin As Date, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date)
        mIdinvencreconteo = Idinvencreconteo
        mIdinventarioenc = Idinventarioenc
        mReconteo = Reconteo
        mEstado = Estado
        mHora_ini = Hora_ini
        mHora_fin = Hora_fin
        mUser_agr = User_agr
        mFec_agr = Fec_agr
        mUser_mod = User_mod
        mFec_mod = Fec_mod
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
