Public Class clsBeI_nav_servicio
    Implements ICloneable

    Public Property IdServicio() As Integer = 0
    Public Property Codigo_servicio() As String = ""
    Public Property Descripcion() As String = ""
    Public Property Nemonico() As String = ""
    Public Property Activo As Boolean = False

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
