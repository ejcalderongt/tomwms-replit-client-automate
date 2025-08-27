Partial Public Class clsBeTrans_despacho_det
    Implements IDisposable

    Public Property IsNew As Boolean
    Public Property NombreUbicacion As String
    Public Property ProductoPresentacion() As String
    Public Property ProductoUnidadMedida() As String
    Public Property ProductoEstado() As String
    Public ListObjM As New List(Of clsBeTrans_movimientos)
    Public Property Bodega As String
    Public Property Cliente As String
    Public Property Propietario As String
    Public Property FechaPedido As DateTime
    Public Property Lote As String = ""
    Public Property Lic_plate As String = ""
    Public Property IdProductoTallaColor As Integer = 0
    Public Property Talla As String = ""
    Public Property Color As String = ""


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
