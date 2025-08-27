Public Class clsBeStock_se_rec
    Implements ICloneable
    Implements IDisposable

    Public Property IdStockSeRec() As Integer = 0
    Public Property IdStockRec() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property NoSerie() As String = ""
    Public Property NoSerieInicial() As String = ""
    Public Property NoSerieFinal() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property Regularizado() As Boolean = False
    Public Property Fecha_regularizacion() As Date = Date.Now

    Sub New()
    End Sub

    Sub New(ByRef IdStockSeRec As Integer, ByVal IdStockRec As Integer, ByVal IdProductoBodega As Integer, ByVal NoSerie As String, ByVal NoSerieInicial As String, ByVal NoSerieFinal As String, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean, ByVal regularizado As Boolean, ByVal fecha_regularizacion As Date)
        Me.IdStockSeRec = IdStockSeRec
        Me.IdStockRec = IdStockRec
        Me.IdProductoBodega = IdProductoBodega
        Me.NoSerie = NoSerie
        Me.NoSerieInicial = NoSerieInicial
        Me.NoSerieFinal = NoSerieFinal
        Me.User_agr = User_agr
        Me.Fec_agr = Fec_agr
        Me.User_mod = User_mod
        Me.Fec_mod = Fec_mod
        Me.Activo = Activo
        Me.Regularizado = Regularizado
        Me.Fecha_regularizacion = Fecha_regularizacion
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
