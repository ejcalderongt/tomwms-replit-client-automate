Public Class clsBeMenu_sistema_op
    Implements ICloneable
    Implements IDisposable

    Public Property IdMenuSistemaOP() As String = ""
    Public Property IdTipoTarea() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Nivel() As Integer = 0
    Public Property Padre() As String = ""
    Public Property Posicion() As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef IdMenuSistemaOP As String, ByVal IdTipoTarea As Integer, ByVal Nombre As String, ByVal Nivel As Integer, ByVal Padre As String, ByVal Posicion As Integer)
        Me.IdMenuSistemaOP = IdMenuSistemaOP
        Me.IdTipoTarea = IdTipoTarea
        Me.Nombre = Nombre
        Me.Nivel = Nivel
        Me.Padre = Padre
        Me.Posicion = Posicion
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
