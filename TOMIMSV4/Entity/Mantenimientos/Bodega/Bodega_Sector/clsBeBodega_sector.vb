<Serializable>
Public Class clsBeBodega_sector
    Implements ICloneable
    Implements IDisposable

    Public Property IdSector() As Integer = 0
    Public Property IdArea() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property Sistema() As Boolean = False
    Public Property Descripcion() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property Alto() As Double = 0.0
    Public Property Largo() As Double = 0.0
    Public Property Ancho() As Double = 0.0
    Public Property Margen_izquierdo() As Double = 0.0
    Public Property Margen_derecho() As Double = 0.0
    Public Property Margen_superior() As Double = 0.0
    Public Property Margen_inferior() As Double = 0.0
    Public Property Codigo() As String = ""
    Public Property IdSectorIzquierda() As Integer = 0
    Public Property IdSectorDerecha() As Integer = 0
    Public Property Horizontal() As Boolean = False
    Public Property Pos_x() As Double = 0.0
    Public Property Pos_y() As Double = 0.0

    Sub New()
    End Sub

    Sub New(ByRef IdSector As Integer, ByVal IdArea As Integer, ByVal sistema As Boolean, ByVal descripcion As String, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean, ByVal alto As Double, ByVal largo As Double, ByVal ancho As Double, ByVal margen_izquierdo As Double, ByVal margen_derecho As Double, ByVal margen_superior As Double, ByVal margen_inferior As Double, ByVal Codigo As String, ByVal IdSectorIzquierda As Integer, ByVal IdSectorDerecha As Integer, ByVal horizontal As Boolean, ByVal pos_x As Double, ByVal pos_y As Double)
        Me.IdSector = IdSector
        Me.IdArea = IdArea
        Me.Sistema = sistema
        Me.Descripcion = descripcion
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Activo = activo
        Me.Alto = alto
        Me.Largo = largo
        Me.Ancho = ancho
        Me.Margen_izquierdo = margen_izquierdo
        Me.Margen_derecho = margen_derecho
        Me.Margen_superior = margen_superior
        Me.Margen_inferior = margen_inferior
        Me.Codigo = Codigo
        Me.IdSectorIzquierda = IdSectorIzquierda
        Me.IdSectorDerecha = IdSectorDerecha
        Me.Horizontal = horizontal
        Me.Pos_x = pos_x
        Me.Pos_y = pos_y
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
