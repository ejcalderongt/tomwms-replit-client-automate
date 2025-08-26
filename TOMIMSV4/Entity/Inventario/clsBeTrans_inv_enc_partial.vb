Partial Public Class clsBeTrans_inv_enc
    Public Property Propietario As clsBePropietarios = New clsBePropietarios()
    Public Property Bodega As clsBeBodega = New clsBeBodega()
    Public Property TipoConteo As clsBeTipoConteo = New clsBeTipoConteo()
    Public Property TipoInv As clsBeTipoInventario = New clsBeTipoInventario()
    Public Property IdProducto As Integer
    Public Property IdPresentacion As Integer
    Public Property IdInventarioDet As Integer
    Public Property IdInventarioRes As Integer
    Public Property IdTramo As Integer
    Public Property Ubicacion As clsBeBodega_ubicacion = New clsBeBodega_ubicacion
    Public Property UbicacionCompleta As String
    Public Property Tramo As String
    Public Property Producto As String
    Public Property Codigo As String
    Public Property Presentacion As String
    Public Property Detalle As Double = 0
    Public Property Resumen As Double = 0
    Public Property Stock As Double = 0
    Public Property Peso As Double = 0
    Public Property IsNew As Boolean
    Public Property EstadoResumen As String
    Public Property EstadoDetalle As String
    Public Property OperadorConteo As String
    Public Property OperadorVerifica As String
    Public Property FechaConteo As Date
    Public Property FechaVence As Date
    Public Property FechaVerifica As Date
    Public Property Lote As String
    Public Property UMBas As String

End Class
