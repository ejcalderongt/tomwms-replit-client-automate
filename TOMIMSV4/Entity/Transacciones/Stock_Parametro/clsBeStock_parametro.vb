<Serializable>
Public Class clsBeStock_parametro
    Implements ICloneable
    Implements IDisposable

    Public Property IdStockParametro() As Integer = 0
    Public Property IdStock() As Integer = 0
    Public Property IdProductoParametro() As Integer = 0
    Public Property Valor_texto() As String = ""
    Public Property Valor_numerico() As Double = 0.0
    Public Property Valor_fecha() As Date = Date.Now
    Public Property Valor_logico() As Boolean = False
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property Activo() As Boolean = False

    Sub New()
    End Sub

    Sub New(ByRef IdStockParametro As Integer, ByVal IdStock As Integer, ByVal IdProductoParametro As Integer, ByVal valor_texto As String, ByVal valor_numerico As Double, ByVal valor_fecha As Date, ByVal valor_logico As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal activo As Boolean)
        Me.IdStockParametro = IdStockParametro
        Me.IdStock = IdStock
        Me.IdProductoParametro = IdProductoParametro
        Me.Valor_texto = valor_texto
        Me.Valor_numerico = valor_numerico
        Me.Valor_fecha = valor_fecha
        Me.Valor_logico = valor_logico
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.Activo = activo
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
