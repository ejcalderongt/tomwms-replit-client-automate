Public Class clsBeI_nav_producto_presentacion
    Implements ICloneable

    Public Property No() As String = ""
    Public Property Codigo_Pres() As String = ""
    Public Property Factor() As Double = 0

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
