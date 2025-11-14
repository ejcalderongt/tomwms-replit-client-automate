Partial Public Class clsBeStock_rec
    Public Property IsNew() As Boolean = True
    Public Property ProductoValidado() As Boolean
    Public Property Presentacion As New clsBeProducto_Presentacion
    Public Property ProductoEstado As New clsBeProducto_estado
    Public Property CantidadEnStock As Double = 0
    Public Property PesoEnStock As Double = 0
    Public Property Cantidad_Nav As Double = 0
End Class
