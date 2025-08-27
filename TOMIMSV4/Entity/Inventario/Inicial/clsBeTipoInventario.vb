Public Class clsBeTipoInventario
    Implements ICloneable

    Public Property IdTipoInv() As Integer = 0
    Public Property Descripcion() As String = ""

    Sub New()
    End Sub

    Sub New(ByRef IdTipoInv As Integer, ByVal Descripcion As String)
        Me.IdTipoInv = IdTipoInv
        Me.Descripcion = Descripcion
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
