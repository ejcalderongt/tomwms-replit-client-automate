Public Class clsBeTrans_inv_stock_prod
    Implements ICloneable
    Public Property Idinventario() As Integer = 0
    Public Property Idinvstockprod() As Integer = 0
    Public Property IdProducto() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property Cant() As Double = 0.0
    Public Property Peso() As Double = 0.0
    Public Property IdUnidadMedida() As Integer = 0
    Public Property Lote As String = ""
    Public Property Fecha_vence As DateTime = Now
    Public Property Codigo As String = ""
    Public Property IdBodega As Integer = 0
    Public Property IdUbicacion As Integer = 0

    'GT02122021: se añaden para busqueda en la HH
    Public Property License_plate As String = ""
    Public Property Codigo_variante As String = ""

    Public Property BeProducto As New clsBeProducto()

    'AT20220504: Se agrega IdPropietarioBodega
    Public Property IdPropietarioBodega As Integer = 0

    '#GT24112022: campos DyD en importación inventario inicial
    Public Property Costo As Double = 0.0
    Public Property Precio As Double = 0.0
    Public Property Parametro_a As String = ""
    Public Property Parametro_b As String = ""
    Public Property TipoTeoricoImportacion As Integer = 0
    Public Property Codigo_Area As String = ""
    Public Property Color As String = ""
    Public Property Talla As String = ""
    Sub New()
    End Sub
    Sub New(ByRef idinventario As Integer, ByVal idProducto As Integer, ByVal idPresentacion As Integer, ByVal cant As Double, ByVal peso As Double, ByVal idUnidadMedida As Integer)
        Me.Idinventario = idinventario
        Me.IdProducto = idProducto
        Me.IdPresentacion = idPresentacion
        Me.Cant = cant
        Me.Peso = peso
        Me.IdUnidadMedida = idUnidadMedida
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
