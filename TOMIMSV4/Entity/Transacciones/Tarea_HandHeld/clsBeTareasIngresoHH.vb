Public Class clsBeTareasIngresoHH
    Implements IDisposable

    Public Property IdRecepcionEnc() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdPropietario() As Integer = 0
    Public Property NombrePropietario() As String = ""
    Public Property IdProveedor() As Integer = 0
    Public Property NombreProveedor() As String = ""
    Public Property NoDocumentoOc() As String = ""
    Public Property NombreMotivoDevolucion() As String = ""
    Public Property NombreTipoIngresoOC() As String = ""
    Public Property NoReferenciaOC() As String = ""
    Public Property IdOrderCompraEnc() As Integer = 0
    Public Property NombreTipoRecepcion() As String = ""
    Public Property NumPoliza() As String = ""
    Public Property NumOrden() As String = ""

    Public Property RutaDespacho() As String = ""
    Public Property Observacion() As String = ""
    Public Property RequiereTarima() As Boolean = False
    Public Property Muelle() As String = ""

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
