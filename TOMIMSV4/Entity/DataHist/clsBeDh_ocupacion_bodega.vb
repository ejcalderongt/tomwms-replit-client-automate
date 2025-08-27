Public Class clsBeDh_ocupacion_bodega
    Implements ICloneable

    Public Property IdOcupacionBodega() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property Cant_ubicaciones_vacias() As Double = 0
    Public Property Cant_ubicaciones_ocupadas() As Double = 0
    Public Property Fecha() As Date = New Date(1900, 1, 1)

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
