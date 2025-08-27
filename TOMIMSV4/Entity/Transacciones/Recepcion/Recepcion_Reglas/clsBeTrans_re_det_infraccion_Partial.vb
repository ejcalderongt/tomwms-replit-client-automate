Partial Public Class clsBeTrans_re_det_infraccion
    Implements IDisposable

    Public Property Empresa As String
    Public Property ImagenEmpresa As Byte()
    Public Property IsNew() As Boolean
    Public Property IdRecepcion As Integer
    Public Property Propietario As String
    Public Property IdPropietario As Integer
    Public Property Bodega As String
    Public Property IdBodega As Integer
    Public Property FechaOrdenCompra As DateTime
    Public Property FechaRecepcion As DateTime
    Public Property ReglaInfraccionada As String
    Public Property CodigoProductoInfraccionado As String
    Public Property ProductoInfraccionado As String
    Public Property CantidadSolicitada As Double
    Public Property CantidadRecibida As Double
    Public Property CostoOrdenCompra As Double
    Public Property CostoRecepcion As Double
    Public Property TipoIngreso As String
    Public Property TipoTransaccion As String
    Public Property NombreUsuario As String
    Public Property EsDevolucion() As Boolean
    Public Property IdProveedor() As Integer
    Public Property NombreProveedor() As String
    Public Property ListaFactura As List(Of clsBeTrans_re_fact) = New List(Of clsBeTrans_re_fact)()

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
    End Sub
#End Region

End Class
