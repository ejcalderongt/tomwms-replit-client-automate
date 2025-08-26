<Serializable>
Public Class clsBeBodega_muelles
    Implements ICloneable
    Implements IDisposable

    Public Property IdMuelle() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property Codigo_barra() As String = ""
    Public Property Nombre() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Color() As Integer = 0
    Public Property Imagen() As Byte() = Nothing
    Public Property Activo() As Boolean = False
    Public Property Entrada() As Boolean = False
    Public Property Salida() As Boolean = False
    Property IdUbicacionDefecto As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef IdMuelle As Integer, ByVal IdBodega As Integer, ByVal codigo_barra As String, ByVal nombre As String, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal color As Integer, ByVal imagen As Byte(), ByVal activo As Boolean, ByVal Entrada As Boolean, ByVal Salida As Boolean)
        Me.IdMuelle = IdMuelle
        Me.IdBodega = IdBodega
        Me.Codigo_barra = codigo_barra
        Me.Nombre = nombre
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Color = color
        Me.Imagen = imagen
        Me.Activo = activo
        Me.Entrada = Entrada
        Me.Salida = Salida
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
