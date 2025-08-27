Public Class clsBeConsolidador
    Implements ICloneable

    Public Property Idconsolidador() As Integer = 0
    Public Property Codigo() As String = ""
    Public Property IdEmpresa() As Integer = 0
    Public Property Nom_comercial() As String = ""
    Public Property Razon_social() As String = ""
    Public Property Nit() As String = ""
    Public Property Telefono() As String = ""
    Public Property Direccion() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
