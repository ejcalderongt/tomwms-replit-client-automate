Partial Public Class clsBeProducto_presentaciones_conversiones
    Implements IDisposable

    Public Property ProductoPresentacionDestino As clsBeProducto_Presentacion = New clsBeProducto_Presentacion
    Public Property ProductoPresentacionOrigen As clsBeProducto_Presentacion = New clsBeProducto_Presentacion
    Public Property IsNew As Boolean

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
        If ProductoPresentacionDestino IsNot Nothing Then
            ProductoPresentacionDestino.Dispose()
            ProductoPresentacionDestino = Nothing
        End If
        If ProductoPresentacionOrigen IsNot Nothing Then
            ProductoPresentacionOrigen.Dispose()
            ProductoPresentacionOrigen = Nothing
        End If
    End Sub
#End Region
End Class
