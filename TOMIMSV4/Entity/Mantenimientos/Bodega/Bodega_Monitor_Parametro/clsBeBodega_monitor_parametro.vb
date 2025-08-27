<Serializable>
Public Class clsBeBodega_monitor_parametro
    Implements ICloneable
    Implements IDisposable

    Public Property IdMonitor() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property TiempoActualizacion() As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef IdMonitor As Integer, ByVal IdBodega As Integer, ByVal Nombre As String, ByVal TiempoActualizacion As Integer)
        Me.IdMonitor = IdMonitor
        Me.IdBodega = IdBodega
        Me.Nombre = Nombre
        Me.TiempoActualizacion = TiempoActualizacion
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

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
