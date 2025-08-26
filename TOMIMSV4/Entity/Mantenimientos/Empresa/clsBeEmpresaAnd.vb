Public Class clsBeEmpresaAnd

    Implements ICloneable
    Implements IDisposable

    Public Property IdEmpresa() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Direccion() As String = ""
    Public Property Telefono() As String = ""
    Public Property Email() As String = ""
    Public Property Razon_social() As String = ""
    Public Property Representante() As String = ""
    Public Property Corr_cod_barra() As Integer = 0
    Public Property Path_printer() As String = ""
    Public Property Activo() As Boolean = False
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property ClienteRapido() As Boolean = False
    Public Property Imagen() As String
    Public Property Operador_logistico() As Boolean = False
    Public Property Puerto_escaner() As Integer = 0
    Public Property Control_presentaciones() As Boolean = False
    Public Property Anulaciones_por_supervisor() As Boolean = False
    Public Property Codigo() As String = ""
    Public Property Clave() As String = ""
    Public Property Intento() As Integer = 0
    Public Property Duracionclave() As Integer = 0
    Public Property Duracionclavetemporal() As Integer = 0
    Public Property codigo_automatico As Boolean = False
    Public Property politica_contraseñas As Boolean = False
    Public Property IdMotivoAjusteInventario() As Integer = 0

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

            ' TODO: free unmanaged resources (unmanaged objects) andoverride Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean)above has code To free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code inDispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement thedisposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean)  above.Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class