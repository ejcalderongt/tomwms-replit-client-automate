Public Class clsBeRegimen_fiscal
    Implements ICloneable

    Public Property IdRegimen() As Integer = 0
    Public Property Codigo_regimen() As String = ""
    Public Property Descripcion() As String = ""
    Public Property Dias_vencimiento() As Integer = 0
    Public Property Activo As Boolean = False

    Public Property Fec_agr() As Date = Date.Now
    Public Property User_agr() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property User_mod() As String = ""


    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
