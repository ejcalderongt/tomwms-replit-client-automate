<Serializable>
Public Class clsBeUnidad_medida
    Implements ICloneable
    Implements IDisposable

    Public Property IdUnidadMedida() As Integer = 0
    Public Property IdPropietario() As Integer = 0
    Public Property Propietario As New clsBePropietarios()
    Public Property Codigo As String = ""
    Public Property Nombre() As String = ""
    Public Property Activo() As Boolean = False
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property User_agr() As String = ""
    Public Property IsNew() As Boolean = False
    Public Property es_um_cobro As Boolean = False
    Public Property factor As Double = 1

    Sub New()
    End Sub

    Sub New(ByRef IdUnidadMedida As Integer, ByVal IdPropietario As Integer, ByVal Nombre As String, ByVal activo As Boolean, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal user_agr As String)
        Me.IdUnidadMedida = IdUnidadMedida
        Me.IdPropietario = IdPropietario
        Me.Nombre = Nombre
        Me.Activo = activo
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.User_agr = user_agr
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
        If Propietario IsNot Nothing Then
            Propietario.Dispose()
            Propietario = Nothing
        End If
    End Sub
#End Region

End Class
