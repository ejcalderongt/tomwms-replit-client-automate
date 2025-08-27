Public Class clsBeTrans_ubic_tarima
    Implements ICloneable
    Implements IDisposable

    Public Property IdTarimaTareaUbic() As Integer = 0
    Public Property IdTareaUbicacionEnc() As Integer = 0
    Public Property IdTarima() As Integer = 0
    Public Property Codigo() As String = ""
    Public Property Utilizada() As Boolean = False
    Public Property FechaUtilizacion() As Date = Date.Now
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now

    Sub New()
    End Sub

    Sub New(ByRef IdTarimaTareaUbic As Integer, ByVal IdTareaUbicacionEnc As Integer, ByVal IdTarima As Integer, ByVal Codigo As String, ByVal Utilizada As Boolean, ByVal FechaUtilizacion As Date, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date)
        Me.IdTarimaTareaUbic = IdTarimaTareaUbic
        Me.IdTareaUbicacionEnc = IdTareaUbicacionEnc
        Me.IdTarima = IdTarima
        Me.Codigo = Codigo
        Me.Utilizada = Utilizada
        Me.FechaUtilizacion = FechaUtilizacion
        Me.User_agr = User_agr
        Me.Fec_agr = Fec_agr
        Me.User_mod = User_mod
        Me.Fec_mod = Fec_mod
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
