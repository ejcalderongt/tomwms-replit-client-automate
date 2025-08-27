Public Class clsBeIndice_rotacion
    Implements ICloneable
    Implements IDisposable

    Public Property IdIndiceRotacion() As Integer = 0
    Public Property Descripcion() As String = ""
    Public Property Activo() As Boolean = False
    Public Property IndicePrioridad() As Integer = 0
    Public Property Grupo() As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef IdIndiceRotacion As Integer, ByVal Descripcion As String, ByVal Activo As Boolean, ByVal IndicePrioridad As Integer, ByVal Grupo As Integer)
        Me.IdIndiceRotacion = IdIndiceRotacion
        Me.Descripcion = Descripcion
        Me.Activo = Activo
        Me.IndicePrioridad = IndicePrioridad
        Me.Grupo = Grupo
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
