Public Class clsBeMotivo_devolucion
    Implements ICloneable
    Implements IDisposable

    Public Property IdMotivoDevolucion() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdPropietario() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property Es_detalle() As Boolean = False

    Sub New()
    End Sub

    Sub New(ByRef IdMotivoDevolucion As Integer, ByVal IdEmpresa As Integer, ByVal IdPropietario As Integer, ByVal Nombre As String, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean, ByVal es_detalle As Boolean)
        Me.IdMotivoDevolucion = IdMotivoDevolucion
        Me.IdEmpresa = IdEmpresa
        Me.IdPropietario = IdPropietario
        Me.Nombre = Nombre
        Me.User_agr = User_agr
        Me.Fec_agr = Fec_agr
        Me.User_mod = User_mod
        Me.Fec_mod = Fec_mod
        Me.Activo = Activo
        Me.Es_detalle = Es_detalle
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
        If Empresa IsNot Nothing Then
            Empresa.Dispose()
            Empresa = Nothing
        End If
        If Empresa IsNot Nothing Then
            Empresa.Dispose()
            Empresa = Nothing
        End If
        If Propietario IsNot Nothing Then
            Propietario.Dispose()
            Propietario = Nothing
        End If
    End Sub
#End Region

End Class
