Public Class clsBeProductoUbicacionSugerida

    Public Property IdProducto As Integer = 0
    Public Property IdBodega As Integer = 0
    Public Property IdProductoBodega As Integer = 0
    Public Property Lote As String = ""
    Public Property Fecha_Vence As Date = New Date(199, 1, 1)
    Public Property IdUbicacionOrigen As Integer = 0
    Public Property IdEstadoProducto As Integer = 0
    Public Property IdUnidadMedidaBas As Integer = 0
    Public Property IdPresentacion As Integer = 0
    Public Property Cantidad_A_Ubicar As Double = 0

End Class