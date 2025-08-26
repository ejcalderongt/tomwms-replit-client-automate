Public Class clsBe_Servicio

    Implements ICloneable

    Public Property IdConsolidador As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property Almacen() As String = ""
    Public Property IdCliente() As Integer = 0
    Public Property Nombre_Cliente() As String = ""
    Public Property IdPropietario_Enc() As Integer = 0
    Public Property No_orden() As String = ""
    Public Property Tipo_Transaccion() As String = ""
    Public Property No_Linea() As Integer = 0
    Public Property Codigo_producto() As String = ""
    Public Property Nombre_Producto() As String = ""
    Public Property Cantidad() As Integer = 0
    Public Property Fecha_Servicio() As String = New Date(1900, 1, 1)
    Public Property IdOrdenCompraEnc As Integer = 0

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
