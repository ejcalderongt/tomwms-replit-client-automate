Public Class clsBeTrans_re_tr
    Implements ICloneable
    Implements IDisposable

    Public Property IdTipoTransaccion() As String = ""
    Public Property Descripcion() As String = ""
    Public Property Funcionalidad() As String = ""
    Public Property UsaHH() As Boolean? = False
    Public Property DescDev() As String = ""
    Public Property TipoTrans() As String = ""
    Public Property ConRef() As Boolean? = False

    Public Property Activo As Boolean = True

    Sub New()
    End Sub

    Sub New(ByRef IdTipoTransaccion As String, ByVal Descripcion As String, ByVal Funcionalidad As String, ByVal UsaHH As Boolean, ByVal DescDev As String, ByVal TipoTrans As String, ByVal ConRef As Boolean)
        Me.IdTipoTransaccion = IdTipoTransaccion
        Me.Descripcion = Descripcion
        Me.Funcionalidad = Funcionalidad
        Me.UsaHH = UsaHH
        Me.DescDev = DescDev
        Me.TipoTrans = TipoTrans
        Me.ConRef = ConRef
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
