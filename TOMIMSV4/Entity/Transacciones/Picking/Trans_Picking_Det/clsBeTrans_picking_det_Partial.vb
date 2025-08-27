Partial Public Class clsBeTrans_picking_det
    Implements IDisposable

    Public Property Bodega As String
    Public Property Cliente As String
    Public Property Propietario As String
    Public Property FechaPedido As DateTime
    Public Property No_Documento As Integer
    Public Property IdMuelle As Integer
    Public Property Hora_Inicio As DateTime
    Public Property Hora_fin As DateTime
    Public Property Estado As String
    Public Property IsNew() As Boolean
    Public Property UbicacionPicking As String
    Public Property IdPedidoEnc As Integer
    Public Property Referencia As String
    Public Property Codigo As String
    Public Property NombreProducto As String
    Public Property Fecha_Ingreso As Date
    Public Property Fecha_Vence As Date
    Public Property Presetacion As String
    Public Property Factorx As Double
    Public Property CantidadReservada As Double
    Public Property Cantidad_Pickeada As Double
    Public Property Cantidad_Verificada As Double
    Public Property Cantidad_Stock As Double
    Public Property IdUbicacion As Integer
    Public Property UMBas As String
    Public Property Lic_Plate As String = ""
    Public Property Lote As String = ""
    Public Property Producto As clsBeProducto = New clsBeProducto()
    Public Property Presentacion As clsBeProducto_Presentacion = New clsBeProducto_Presentacion()
    Public Property ProductoEstado As clsBeProducto_estado = New clsBeProducto_estado()
    Public Property UnidadMedida As clsBeUnidad_medida = New clsBeUnidad_medida()
    Public Property ListaDetalleParametro As New List(Of clsBeTrans_picking_det_parametros)
    Public Property Bono As String = ""

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
        If Producto IsNot Nothing Then
            Producto.Dispose()
            Producto = Nothing
        End If
    End Sub
#End Region


End Class
