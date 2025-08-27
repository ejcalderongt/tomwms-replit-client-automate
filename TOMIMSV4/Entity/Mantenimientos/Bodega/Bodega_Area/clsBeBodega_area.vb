<Serializable>
Public Class clsBeBodega_area
    Implements ICloneable
    Implements IDisposable

    Public Property IdArea() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property Descripcion() As String = ""
    Public Property Sistema() As Boolean = False
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Codigo() As String = ""
    Public Property Activo() As Boolean = False
    Public Property Alto() As Double = 0.0
    Public Property Largo() As Double = 0.0
    Public Property Ancho() As Double = 0.0
    Public Property Margen_izquierdo() As Double = 0.0
    Public Property Margen_derecho() As Double = 0.0
    Public Property Margen_superior() As Double = 0.0
    Public Property Margen_inferior() As Double = 0.0
    Public Property Grupo As String = ""
    Public Property IdUbicacionRef As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef IdArea As Integer, ByVal IdBodega As Integer, ByVal Descripcion As String, ByVal sistema As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal Codigo As String, ByVal activo As Boolean, ByVal alto As Double, ByVal largo As Double, ByVal ancho As Double, ByVal margen_izquierdo As Double, ByVal margen_derecho As Double, ByVal margen_superior As Double, ByVal margen_inferior As Double)
        Me.IdArea = IdArea
        Me.IdBodega = IdBodega
        Me.Descripcion = Descripcion
        Me.Sistema = sistema
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Codigo = Codigo
        Me.Activo = activo
        Me.Alto = alto
        Me.Largo = largo
        Me.Ancho = ancho
        Me.Margen_izquierdo = margen_izquierdo
        Me.Margen_derecho = margen_derecho
        Me.Margen_superior = margen_superior
        Me.Margen_inferior = margen_inferior
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
