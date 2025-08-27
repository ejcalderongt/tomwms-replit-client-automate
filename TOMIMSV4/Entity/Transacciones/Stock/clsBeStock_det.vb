Public Class clsBeStock_det
    Implements ICloneable

    Public Property IdStock() As Integer = 0
    Public Property Posiciones() As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef IdStock As Integer, ByVal posiciones As Integer)
        Me.IdStock = IdStock
        Me.Posiciones = posiciones
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
