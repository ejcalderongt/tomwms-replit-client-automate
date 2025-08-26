Public Class clsBeUbicacionSugeridaList

    Implements ICloneable

    Public Property IdUbicacionOrigen As Integer = 0
    Public Property IdUbicacionDestino As Integer = 0
    Public Property Descripcion As String = ""
    Public Property Tramo As String = ""
    Public Property Nivel As Integer = 0
    Public Property Ubicar As Double = 0
    Public Property Maximo As Double = 0
    Public Property IsNew As Boolean = False

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
