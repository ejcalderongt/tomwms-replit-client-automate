Public Class clsBeImpresora
    Implements ICloneable
    Implements IDisposable

    Public Property IdImpresora() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Direccion_Ip() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property mac_adress As String = ""
    Public Property IdBodega() As Integer = 0
    Public Property Numero_Serie As String = ""
    Public Property IdImpresoraMarca As Integer = 0
    Public Property IdLenguaje As Integer = 0
    Public Property IdTipoConexion As Integer = 0
    Public Property Puerto As Integer = 0
    Public Property Es_Movil As Boolean = False
    Public Property Velocidad As Double = 0

    Sub New()
    End Sub

    Sub New(ByRef IdImpresora As Integer, ByVal IdEmpresa As Integer, ByVal nombre As String, ByVal direccion_Ip As String, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean, ByVal mac_adress As String, ByVal IdBodega As Integer)
        Me.IdImpresora = IdImpresora
        Me.IdEmpresa = IdEmpresa
        Me.Nombre = nombre
        Me.Direccion_Ip = direccion_Ip
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Activo = activo
        Me.mac_adress = mac_adress
        Me.IdBodega = IdBodega
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
