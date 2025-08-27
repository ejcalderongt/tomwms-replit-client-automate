<Serializable>
Public Class clsBe_Servicio_Logistico
    Public Property IdConsolidador As Integer = 0
    Public Property Almacen() As String = ""
    Public Property IdCliente() As Integer = 0
    Public Property No_orden() As String = ""
    Public Property No_Linea() As Integer = 0
    Public Property Codigo_producto() As String = ""
    Public Property Nombre_Producto() As String = ""
    Public Property Cantidad() As Integer = 0
    Public Property Fecha_Servicio() As String = New Date(1900, 1, 1)
End Class
