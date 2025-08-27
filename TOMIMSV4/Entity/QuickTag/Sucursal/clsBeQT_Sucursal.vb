Public Class clsBeQT_Sucursal
    Implements ICloneable

    Public Property IdFinca() As Integer = 0
    Public Property Codigo() As String = ""
    Public Property Descripcion() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As DateTime = New Date(1900, 1, 1)
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As DateTime = New Date(1900, 1, 1)
    Public Property Activo As Boolean = False
    Public Property Predeterminada As Boolean = False
    Public Property IsNew As Boolean = False

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
