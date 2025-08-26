Public Class clsBeTempComparacionInventario
    Implements ICloneable

    Public Property IdInventario() As Integer = 0

    Public Property IdProductoBodega() As Integer = 0

    Public Property IdProducto() As Integer = 0

    Public Property IdUnidadMedida() As Integer = 0

    Public Property Codigo() As String = ""

    Public Property Producto() As String = ""

    Public Property Cantidad_Stock() As Double = 0.0

    Public Property Cantidad() As Double = 0.0

    Public Property Peso_Stock() As Double = 0.0

    Public Property Peso() As Double = 0.0

    Public Property Entradas_Salidas() As Double = 0.0

    Public Property Entradas() As Double = 0.0

    Public Property Salidas() As Double = 0.0

    Public Property LoteOrigen() As String = ""
    Public Property LoteDestino As String = ""

    Public Property FechaVence() As Date
    Public Property Licencia As String = ""
    Public Property UbicacionOrigen As String = ""
    Public Property UbicacionDestino As String = ""
    Public Property EstadoOrigen As String = ""
    Public Property EstadoDestino As String = ""
    Public Property IdUbicacion As Integer = 0
    Public Property IdUbicacionDestino As Integer = 0
    Public Property Fec_Mod As Date = New Date(1900, 1, 1)
    Public Property IdInvciclico As Integer = 0
    Sub New()
    End Sub

    Sub New(ByRef IdInventario As Integer, ByVal IdProductoBodega As Integer, ByVal IdProducto As Integer, ByVal IdUnidadMedida As Integer, ByVal Codigo As String, ByVal Producto As String, ByVal Cantidad_Stock As Double, ByVal Cantidad As Double, ByVal Peso_Stock As Double, ByVal Peso As Double, ByVal Entradas_Salidas As Double, ByVal Entradas As Double, ByVal Salidas As Double, ByVal Lote As String, ByVal FechaVence As Date)
        Me.IdInventario = IdInventario
        Me.IdProductoBodega = IdProductoBodega
        Me.IdProducto = IdProducto
        Me.IdUnidadMedida = IdUnidadMedida
        Me.Codigo = Codigo
        Me.Producto = Producto
        Me.Cantidad_Stock = Cantidad_Stock
        Me.Cantidad = Cantidad
        Me.Peso_Stock = Peso_Stock
        Me.Peso = Peso
        Me.Entradas_Salidas = Entradas_Salidas
        Me.Entradas = Entradas
        Me.Salidas = Salidas
        LoteOrigen = Lote
        Me.FechaVence = FechaVence
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
