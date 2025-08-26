Public Class clsBeTrans_re_img
    Implements ICloneable
    Implements IDisposable

    Public Property IdImagen() As Integer = 0
    Public Property IdRecepcionEnc() As Integer = 0
    Public Property Imagen() As Byte() = Nothing
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property Observacion() As String = ""

    Sub New()
    End Sub

    Sub New(ByRef IdImagen As Integer,
            ByVal IdRecepcionEnc As Integer,
            ByVal Imagen As Byte(),
            ByVal user_agr As String,
            ByVal fec_agr As Date,
            ByVal observacion As String)
        Me.IdImagen = IdImagen
        Me.IdRecepcionEnc = IdRecepcionEnc
        Me.Imagen = Imagen
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.Observacion = observacion
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
