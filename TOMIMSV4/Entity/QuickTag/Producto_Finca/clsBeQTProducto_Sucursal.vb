Public Class clsBeQTProducto_Sucursal
    Implements ICloneable

    Public Property IdProducto() As Integer = 0
    Public Property Codigo() As String = ""
    Public Property Descripcion() As String = ""
    Public Property IdFinca() As Integer = 0
    Public Property User_agr As String = ""
    Public Property Fec_agr As DateTime = Now
    Public Property User_mod As String = ""
    Public Property Fec_mod As DateTime = Now
    Public Property Activo As Boolean = False
    Public Property IsNew As Boolean = False

    Public Property Img_Producto As String = ""

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
