<Serializable>
Public Class clsBeBodega_orientacion_pos
    Implements ICloneable

    Private mIdOrientacionPos As Integer = 0
    Private mCodigo As String = ""
    Private mNombre As String = ""

    Public Property IdOrientacionPos() As Integer
        Get
            Return mIdOrientacionPos
        End Get
        Set(ByVal Value As Integer)
            mIdOrientacionPos = Value
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

    Public Property Nombre() As String
        Get
            Return mNombre
        End Get
        Set(ByVal Value As String)
            mNombre = Value
        End Set
    End Property

    Sub New()
    End Sub

    Sub New(ByRef IdOrientacionPos As Integer, ByVal Codigo As String, ByVal Nombre As String)
        mIdOrientacionPos = IdOrientacionPos
        mCodigo = Codigo
        mNombre = Nombre
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
