Public Class clsBeUbicacionExcluida
    Implements ICloneable

    Public Property idStock() As Integer = 0
    Public Property idUbicacion() As Integer = 0

    Sub New()
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
