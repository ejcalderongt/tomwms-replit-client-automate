<Serializable>
Public Class clsBeProducto_imagen
    Implements ICloneable

    Public Property IdProductoImagen() As Integer = 0
    Public Property IdProducto() As Integer = 0
    Public Property Etiqueta() As String = ""
    Public Property Imagen() As Byte() = Nothing
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property IsNew As Boolean = False

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
