Public Class clsBeProductoUS
    Public Property IdProducto As Integer = 0
    Public Property CodigoProducto As String = ""
    Public Property Lote As String = ""
    Public Property Fecha_Vence As Date = New Date(1900, 1, 1)
    Public Property Cantidad As Double = 0
    Public Property IdProductoEstado As Integer = 0
    Public Property IdTipoProducto As Integer = 0
    Public Property IndiceRotacion As Integer = 0
    Public Property IdTipoRotacion As Integer = 0
    Public Property IdBodega As Integer = 0
    Public Property IdTramo As Integer = 0
    Public Property Distancia As Double = 0
    Public Property IdUnidadMedida As Integer = 0
    Public Property IdPresentacion As Integer = 0
    Public Property IdUbicacion As Integer = 0
End Class

Public Class clsBePalletUS
    Public Property Productos As List(Of clsBeProductoUS)

End Class

Public Class clsBeUbicacionUS
    Public Property IdBodega As Integer = 0
    Public Property IdTramo As Integer = 0
    Public Property IdUbicacion As Integer
    Public Property Nivel As Integer = 0
    Public Property Columna As Integer = 0
    Public Property CapacidadRestante As Double = 0
    Public Property IdTipoProducto As Integer = 0
    Public Property IdUnidadMedida As Integer = 0
    Public Property IdPresentacion As Integer = 0
    Public Property Productos As List(Of clsBeProductoUS)
    Public Property Distancia As Double = 0
End Class
