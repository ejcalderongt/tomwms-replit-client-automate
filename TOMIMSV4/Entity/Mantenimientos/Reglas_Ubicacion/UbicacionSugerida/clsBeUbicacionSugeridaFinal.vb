Public Class clsBeUbicacionSugeridaFinal

    Implements ICloneable

    Public Property IdUbicacion() As Integer = 0
    Public Property Descripcion() As String = ""
    Public Property Tramo() As String = ""
    Public Property Nivel() As Integer = 0
    Public Property Cant_Ubicar As Double = 0

    Sub New()
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
