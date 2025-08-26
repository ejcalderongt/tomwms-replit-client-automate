Public Class clsBeTipo_tarima
    Implements ICloneable
    Implements IDisposable

    Public Property IdTipoTarima() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Alto() As Double = 0.0
    Public Property Largo() As Double = 0.0
    Public Property Ancho() As Double = 0.0
    Public Property CargaDinamica() As Double = 0.0
    Public Property CargaEstatica() As Double = 0.0
    Public Property CargaEstanterias() As Double = 0.0
    Public Property EntradasTransPaleta() As Double = 0.0
    Public Property PesoPromedio() As Double = 0.0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As DateTime = Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As DateTime = Now
    Public Property Activo() As Boolean = False
    Public Property Tara() As Double = 0.0

    Sub New()
    End Sub

    Sub New(ByRef IdTipoTarima As Integer, ByVal Nombre As String, ByVal Alto As Double, ByVal Largo As Double, ByVal Ancho As Double, ByVal CargaDinamica As Double, ByVal CargaEstatica As Double, ByVal CargaEstanterias As Double, ByVal EntradasTransPaleta As Double, ByVal PesoPromedio As Double, ByVal user_agr As String, ByVal fec_agr As String, ByVal user_mod As String, ByVal fec_mod As String, ByVal activo As Boolean, ByVal Tara As Double)
        Me.IdTipoTarima = IdTipoTarima
        Me.Nombre = Nombre
        Me.Alto = Alto
        Me.Largo = Largo
        Me.Ancho = Ancho
        Me.CargaDinamica = CargaDinamica
        Me.CargaEstatica = CargaEstatica
        Me.CargaEstanterias = CargaEstanterias
        Me.EntradasTransPaleta = EntradasTransPaleta
        Me.PesoPromedio = PesoPromedio
        Me.User_agr = User_agr
        Me.Fec_agr = Fec_agr
        Me.User_mod = User_mod
        Me.Fec_mod = Fec_mod
        Me.Activo = Activo
        Me.Tara = Tara
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
