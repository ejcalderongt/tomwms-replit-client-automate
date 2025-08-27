<Serializable>
Public Class clsBeFont_det
    Implements ICloneable
    Implements IDisposable

    Public Property IdFontDet() As Integer = 0
    Public Property IdFontEnc() As Integer = 0
    Public Property Letra() As String = ""
    Public Property Tamaño() As Double = 0.0
    Public Property Negrita() As Boolean = False
    Public Property Cursiva() As Boolean = False
    Public Property Subrayado() As Boolean = False
    Public Property ColorFont() As String = ""
    Public Property ColorFondo() As String = ""

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
