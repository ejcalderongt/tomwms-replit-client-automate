Public Class clsBeTarifa_tipo_transaccion
    Implements ICloneable

    Public Property IdTipoTransaccion() As Integer = 0
    Public Property Codigo() As String = ""
    Public Property Nombre() As String = ""
    Public Property Activo() As Boolean = False
    Public Property lDetalleServicios As New List(Of clsBeTarifa_tipo_transaccion_det)

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
