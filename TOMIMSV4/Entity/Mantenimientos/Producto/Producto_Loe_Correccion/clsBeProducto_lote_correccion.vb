<Serializable>
Public Class clsBeProducto_lote_correccion
    Implements ICloneable

    Public Property IdLoteOrigen() As Integer = 0
    Public Property Codigo() As String = ""
    Public Property UnidadMedida() As String = ""
    Public Property Lote() As String = ""
    Public Property Vence() As Date = New Date(1900, 1, 1)
    Public Property Factor() As Double = 0.0
    Public Property Cantidad() As Double = 0.0
    Public Property IdStock As Integer = 0
    Public Property FechaVenceActual As Date = New Date(1900, 1, 1)

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
