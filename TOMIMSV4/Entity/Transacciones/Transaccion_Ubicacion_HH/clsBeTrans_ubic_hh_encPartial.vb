Partial Public Class clsBeTrans_ubic_hh_enc
    Implements ICloneable
    Implements IDisposable

    Public Property IdPrioridad() As Integer = 0
    Public Property IdTipoTarea() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property Asunto() As String = ""
    Public Property DescripcionMotivo As String = ""
    Public Property IsNew As Boolean

    Public Property Nombre_Operador As String = ""


    Sub New(ByRef IdPrioridad As Integer, ByVal IdTipoTarea As Integer, ByVal IdBodega As Integer)
        Me.IdPrioridad = IdPrioridad
        Me.IdTipoTarea = IdTipoTarea
        Me.IdBodega = IdBodega
        Asunto = Asunto
    End Sub

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