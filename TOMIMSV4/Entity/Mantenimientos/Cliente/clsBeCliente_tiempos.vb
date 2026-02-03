Public Class clsBeCliente_tiempos
    Implements ICloneable
    Implements IDisposable

    Public Property IdTiempoCliente() As Integer = 0
    Public Property IdCliente() As Integer = 0
    Public Property IdFamilia() As Integer = 0
    Public Property IdClasificacion() As Integer = 0
    Public Property Familia As clsBeProducto_familia
    Public Property Clasificacion As clsBeProducto_clasificacion
    Public Property Dias_Local() As Integer = 0
    Public Property Dias_Exterior() As Integer = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False

    Public Property Es_Manufactura As Boolean = False

    Sub New()
    End Sub

    Sub New(ByRef IdTiempoCliente As Integer, ByVal IdCliente As Integer, ByVal IdFamilia As Integer, ByVal IdClasificacion As Integer, ByVal Dias_Local As Integer, ByVal Dias_Exterior As Integer, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean)
        Me.IdTiempoCliente = IdTiempoCliente
        Me.IdCliente = IdCliente
        Me.IdFamilia = IdFamilia
        Me.IdClasificacion = IdClasificacion
        Me.Dias_Local = Dias_Local
        Me.Dias_Exterior = Dias_Exterior
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Activo = activo
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

#Region "IDisposable Support"
    ''' <summary>
    ''' The disposed value
    ''' </summary>
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    ''' <summary>
    ''' Releases unmanaged and - optionally - managed resources.
    ''' </summary>
    ''' <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
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
    ''' <summary>
    ''' Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
        If Familia IsNot Nothing Then
            Familia.Dispose()
            Familia = Nothing
        End If
    End Sub
#End Region

End Class
