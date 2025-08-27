Public Class clsBeUbicacionSugeridaRequest

    Implements ICloneable

    Public Property IdBodega() As Integer = 0
    Public Property IdProducto() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property IdEstadoProd() As Integer = 0
    Public Property Lote() As String = ""
    Public Property Cantidad As Double = 0
    Public Property IdUbicStock() As Integer = 0

    Sub New()
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
