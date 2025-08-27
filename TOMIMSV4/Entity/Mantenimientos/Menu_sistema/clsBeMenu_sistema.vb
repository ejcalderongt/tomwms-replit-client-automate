Public Class clsBeMenu_sistema
    Implements ICloneable

    Private mIdMenu As String = ""
    Private mTitulo As String = ""
    Private mNombre_lgco As String = ""
    Private mNivel As Integer = 0
    Private mPadre As String = ""
    Private mSolicitar_clave_autorizacion As Boolean = False

    Public Property IdMenu() As String
        Get
            Return mIdMenu
        End Get
        Set(ByVal Value As String)
            mIdMenu = Value
        End Set
    End Property

    Public Property Titulo() As String
        Get
            Return mTitulo
        End Get
        Set(ByVal Value As String)
            mTitulo = Value
        End Set
    End Property

    Public Property Nombre_lgco() As String
        Get
            Return mNombre_lgco
        End Get
        Set(ByVal Value As String)
            mNombre_lgco = Value
        End Set
    End Property

    Public Property Nivel() As Integer
        Get
            Return mNivel
        End Get
        Set(ByVal Value As Integer)
            mNivel = Value
        End Set
    End Property

    Public Property Padre() As String
        Get
            Return mPadre
        End Get
        Set(ByVal Value As String)
            mPadre = Value
        End Set
    End Property

    Public Property Solicitar_clave_autorizacion() As Boolean
        Get
            Return mSolicitar_clave_autorizacion
        End Get
        Set(ByVal Value As Boolean)
            mSolicitar_clave_autorizacion = Value
        End Set
    End Property

    Sub New()
    End Sub

    Sub New(ByRef IdMenu As String, ByVal titulo As String, ByVal nombre_lgco As String, ByVal nivel As Integer, ByVal padre As String, ByVal solicitar_clave_autorizacion As Boolean)
        mIdMenu = IdMenu
        mTitulo = titulo
        mNombre_lgco = nombre_lgco
        mNivel = nivel
        mPadre = padre
        mSolicitar_clave_autorizacion = solicitar_clave_autorizacion
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class

