Public Class clsBeUnidad_medida_conversion
    Implements ICloneable
    Public Property Activo As Boolean
    Public Property IdConversion() As Integer = 0
    Public Property IdUnidadMedidaOrigen() As Integer = 0
    Public Property IdUnidadMedidaDestino() As Integer = 0
    Public Property Factor() As Double = 0.0

    Sub New()
    End Sub

    Sub New(ByRef IdConversion As Integer, ByVal IdUnidadMedidaOrigen As Integer, ByVal IdUnidadMedidaDestino As Integer, ByVal Factor As Double, ByVal Activo As Boolean)
        Me.IdConversion = IdConversion
        Me.IdUnidadMedidaOrigen = IdUnidadMedidaOrigen
        Me.IdUnidadMedidaDestino = IdUnidadMedidaDestino
        Me.Factor = Factor
        Activo = Activo
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
