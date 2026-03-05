Partial Public Class clsBeTrans_pe_det
    Implements IDisposable

    Public Property IsNew As Boolean = False
    Public Property Producto As New clsBeProducto()
    Public Property Presentacion As clsBeProducto_Presentacion = New clsBeProducto_Presentacion()
    Public Property UnidadMedida As clsBeUnidad_medida = New clsBeUnidad_medida()
    Public Property ListaStockRes As New List(Of clsBeStock_res)
    Public Property ListaPickingUbic As New List(Of clsBeTrans_picking_ubic)
    Public Property Codigo_Producto() As String
    Public Property NombreProducto() As String
    Public Property ProductoPresentacion() As String
    Public Property ProductoUnidadMedida() As String
    Public Property ProductoEstado() As String
    Public Property BodegaUbicacion() As String
    Public Property CantidadFisica() As Double
    Public Property Factor() As Double
    Public Property CantidadReservada() As Double = 0
    Public Property PesoReservado As Double = 0
    Public Property FechaIngreso As DateTime
    Public Property FechaVence As DateTime

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
        If Presentacion IsNot Nothing Then
            Presentacion.Dispose()
            Presentacion = Nothing
        End If
        If UnidadMedida IsNot Nothing Then
            UnidadMedida.Dispose()
            UnidadMedida = Nothing
        End If
    End Sub
#End Region

End Class
