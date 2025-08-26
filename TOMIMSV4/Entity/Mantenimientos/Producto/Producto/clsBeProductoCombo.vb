<Serializable>
Public Class clsBeProductoCombo
    Implements ICloneable

    Public Property IdProducto() As Integer = 0
    Public Property Nombre() As String = ""

    Sub New()
    End Sub

    Sub New(ByRef IdProducto As Integer, ByVal Nombre As Integer)
        Me.IdProducto = IdProducto
        Me.Nombre = Nombre
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
