Public Class clsBeTipoConteo
    Implements ICloneable

    Public Property IdTipoConteo() As Integer = 0
    Public Property Descripcion() As String = ""

    Sub New()
    End Sub

    Sub New(ByRef IdTipoConteo As Integer, ByVal Descripcion As String)
        Me.IdTipoConteo = IdTipoConteo
        Me.Descripcion = Descripcion
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
