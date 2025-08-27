Public Class clsBeUbicacionSugeridaStock

    Implements ICloneable

    Public Property Codigo As String = ""
    Public Property Nombre As String = ""
    Public Property UMBas() As String = ""
    Public Property CantidadUMBas As Double = 0.0
    Public Property Presentacion As String = ""
    Public Property CantidadPresentacion As Double = 0.0
    Public Property Lote() As String = ""
    Public Property FechaVence As Date = Date.Now
    Public Property Serial() As String = ""
    Public Property IdStock As Integer = 0

    Public Function Clone() As Object Implements ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
