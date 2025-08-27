Public Class clsBeDiferencias_movimientos
    Implements ICloneable

    Public Property IdDiferencia() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property Codigo() As String = ""
    Public Property Nombre() As String = ""
    Public Property Lote() As String = ""
    Public Property IdProductoEstado() As Integer = 0
    Public Property Estado() As String = ""
    Public Property FechaVence() As Date = New Date(1900, 1, 1)
    Public Property InventarioInicial() As Double = 0.0
    Public Property Ingresos() As Double = 0.0
    Public Property AjustesPositivos() As Double = 0.0
    Public Property AjustesNegativos() As Double = 0.0
    Public Property Salidas() As Double = 0.0
    Public Property ExistenciaAl() As Double = 0.0
    Public Property ExistenciaActual() As Double = 0.0
    Public Property ExistenciaSinEstado() As Double = 0.0
    Public Property FechaGen() As String = New Date(1900, 1, 1)
    Public Property Diferencia() As Double = 0.0
    Public Property FaltaStock() As Boolean = False

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
