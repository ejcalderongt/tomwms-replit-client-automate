Public Class clsBeTarifa_tipo_transaccion_det
    Implements ICloneable

    Public Property IdTipoTransaccion() As Integer = 0
    Public Property IdServicio() As Integer = 0
    Public Property Activo As Boolean = False

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
